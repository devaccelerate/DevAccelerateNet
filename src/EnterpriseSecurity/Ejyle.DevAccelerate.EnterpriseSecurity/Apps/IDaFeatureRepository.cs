﻿// ----------------------------------------------------------------------------------------------------------------------
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

namespace Ejyle.DevAccelerate.EnterpriseSecurity.Apps
{
    public interface IDaFeatureRepository<TKey, TFeature> : IDaEntityRepository<TKey, TFeature>
        where TKey : IEquatable<TKey>
        where TFeature : IDaFeature<TKey>
    {
        Task CreateAsync(TFeature feature);
        Task<TFeature> FindByIdAsync(TKey id);
        Task<TFeature> FindByKeyAsync(string key);
        Task<DaPaginatedEntityList<TKey, TFeature>> FindAllAsync(DaDataPaginationCriteria paginationCriteria);
        Task<List<TFeature>> FindByAppIdAsync(TKey appId);
        Task UpdateAsync(TFeature feature);
        Task DeleteAsync(TFeature feature);
    }
}
