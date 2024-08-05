// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ejyle.DevAccelerate.Core;
using Ejyle.DevAccelerate.Core.Data;
using Ejyle.DevAccelerate.Core.Utils;

namespace Ejyle.DevAccelerate.Platform.Features
{
    public class DaFeatureManager<TKey, TFeature> : DaEntityManagerBase<TKey, TFeature>
        where TKey : IEquatable<TKey>
        where TFeature : IDaFeature<TKey>
    {
        public DaFeatureManager(IDaFeatureRepository<TKey, TFeature> repository)
            : base(repository)
        {
        }

        protected virtual IDaFeatureRepository<TKey, TFeature> Repository
        {
            get
            {
                return GetRepository<IDaFeatureRepository<TKey, TFeature>>();
            }
        }

        public virtual async Task CreateAsync(TFeature feature)
        {
            ThrowIfDisposed();
            ThrowIfArgumentIsNull(feature, nameof(feature));

            feature.Key = await AssignValidFeatureKeyAsync(feature);
            await Repository.CreateAsync(feature);
        }

        public virtual void Create(TFeature feature)
        {
            DaAsyncHelper.RunSync(() => CreateAsync(feature));
        }

        public virtual async Task UpdateAsync(TFeature feature)
        {
            ThrowIfDisposed();
            ThrowIfArgumentIsNull(feature, nameof(feature));

            await Repository.UpdateAsync(feature);
        }

        public virtual void Update(TFeature feature)
        {
            DaAsyncHelper.RunSync(() => UpdateAsync(feature));
        }

        public virtual async Task DeleteAsync(TFeature feature)
        {
            ThrowIfDisposed();
            ThrowIfArgumentIsNull(feature, nameof(feature));

            await Repository.DeleteAsync(feature);
        }

        public virtual void Delete(TFeature feature)
        {
            DaAsyncHelper.RunSync(() => DeleteAsync(feature));
        }

        public virtual TFeature FindById(TKey id)
        {
            return DaAsyncHelper.RunSync(() => FindByIdAsync(id));
        }

        public virtual Task<TFeature> FindByIdAsync(TKey id)
        {
            ThrowIfDisposed();
            return Repository.FindByIdAsync(id);
        }

        public Task<List<TFeature>> FindByApplicationIdAsync(TKey applicationId)
        {
            ThrowIfDisposed();
            return Repository.FindByApplicationIdAsync(applicationId);
        }

        public Task<TFeature> FindByKeyAsync(TKey applicationId, string key)
        {
            ThrowIfDisposed();
            return Repository.FindByKeyAsync(applicationId, key);
        }

        public virtual Task<DaPaginatedEntityList<TKey, TFeature>> FindAllAsync(DaDataPaginationCriteria paginationCriteria)
        {
            ThrowIfDisposed();
            return Repository.FindAllAsync(paginationCriteria);
        }

        public virtual DaPaginatedEntityList<TKey, TFeature> FindAll(DaDataPaginationCriteria paginationCriteria)
        {
            return DaAsyncHelper.RunSync(() => FindAllAsync(paginationCriteria));
        }

        public virtual string CreateValidFeatureKey(TFeature feature)
        {
            return DaAsyncHelper.RunSync(() => AssignValidFeatureKeyAsync(feature));
        }

        public virtual async Task<string> AssignValidFeatureKeyAsync(TFeature feature)
        {
            var key = feature.Key;

            if (string.IsNullOrEmpty(key))
            {
                key = feature.Name.Replace(" ", "-");
                key = key.ToLower();
            }

            var duplicateFeatureName = await IsFeatureKeyExistsAsync(feature.ApplicationId, key);

            if (duplicateFeatureName)
            {
                key = key + "-" + DaRandomNumberUtil.GenerateInt();
            }

            return key;
        }

        private async Task<bool> IsFeatureKeyExistsAsync(TKey applicationId, string key)
        {
            var app = await FindByKeyAsync(applicationId, key);
            return app != null;
        }
    }
}