// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Ejyle.DevAccelerate.Core;
using Ejyle.DevAccelerate.Core.Data;
using Ejyle.DevAccelerate.Core.Utils;

namespace Ejyle.DevAccelerate.Platform.Applications
{
    public class DaApplicationManager<TKey, TApplication> : DaEntityManagerBase<TKey, TApplication>
        where TKey : IEquatable<TKey>
        where TApplication : IDaApplication<TKey>
    {
        public DaApplicationManager(IDaApplicationRepository<TKey, TApplication> repository) : base(repository)
        { }        

        protected virtual IDaApplicationRepository<TKey, TApplication> Repository
        {
            get
            {
                return GetRepository<IDaApplicationRepository<TKey, TApplication>>();
            }
        }

        public virtual async Task CreateAsync(TApplication application)
        {
            ThrowIfDisposed();
            ThrowIfArgumentIsNull(application, nameof(application));

            application.Key = await CreateValidApplicationKeyAsync(application);
            await Repository.CreateAsync(application);
        }

        public virtual void Create(TApplication application)
        {
            DaAsyncHelper.RunSync(() => CreateAsync(application));
        }

        public virtual async Task UpdateAsync(TApplication application)
        {
            ThrowIfDisposed();
            ThrowIfArgumentIsNull(application, nameof(application));

            await Repository.UpdateAsync(application);
        }

        public virtual void Update(TApplication application)
        {
            DaAsyncHelper.RunSync(() => UpdateAsync(application));
        }

        public virtual async Task DeleteAsync(TApplication application)
        {
            ThrowIfDisposed();
            ThrowIfArgumentIsNull(application, nameof(application));

            await Repository.DeleteAsync(application);
        }

        public virtual void Delete(TApplication application)
        {
            DaAsyncHelper.RunSync(() => DeleteAsync(application));
        }

        public virtual TApplication FindById(TKey id)
        {
            return DaAsyncHelper.RunSync(() => FindByIdAsync(id));
        }

        public virtual Task<TApplication> FindByIdAsync(TKey id)
        {
            ThrowIfDisposed();
            return Repository.FindByIdAsync(id);
        }

        public virtual TApplication FindByKey(string key)
        {
            return DaAsyncHelper.RunSync(() => FindByKeyAsync(key));
        }

        public virtual Task<TApplication> FindByKeyAsync(string key)
        {
            ThrowIfDisposed();
            ThrowIfArgumentIsNull(key, nameof(key));

            return Repository.FindByKeyAsync(key);
        }

        public virtual Task<DaPaginatedEntityList<TKey, TApplication>> FindAllAsync(DaDataPaginationCriteria paginationCriteria)
        {
            ThrowIfDisposed();
            return Repository.FindAllAsync(paginationCriteria);
        }

        public virtual DaPaginatedEntityList<TKey, TApplication> FindAll(DaDataPaginationCriteria paginationCriteria)
        {
            return DaAsyncHelper.RunSync(() => FindAllAsync(paginationCriteria));
        }

        public virtual string CreateValidApplicationKey(TApplication application)
        {
            return DaAsyncHelper.RunSync(() => CreateValidApplicationKeyAsync(application));
        }

        public virtual async Task<string> CreateValidApplicationKeyAsync(TApplication application)
        {
            var key = application.Name.Replace(" ", "-");
            key = key.ToLower();

            var duplicateKey = await IsKeyExistsAsync(key);

            if (duplicateKey)
            {
                key = key + "-" + DaRandomNumberUtil.GenerateInt();
            }

            return key;
        }

        private async Task<bool> IsKeyExistsAsync(string key)
        {
            var application = await FindByKeyAsync(key);
            return application != null;
        }
    }
}
