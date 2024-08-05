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

namespace Ejyle.DevAccelerate.Platform.Applications
{
    public interface IDaApplicationRepository<TKey, TApplication> : IDaEntityRepository<TKey, TApplication>
        where TKey : IEquatable<TKey>
        where TApplication : IDaApplication<TKey>
    {
        Task CreateAsync(TApplication application);
        Task<TApplication> FindByIdAsync(TKey id);
        Task<TApplication> FindByKeyAsync(string key);
        Task<DaPaginatedEntityList<TKey, TApplication>> FindAllAsync(DaDataPaginationCriteria paginationCriteria);
        Task UpdateAsync(TApplication application);
        Task DeleteAsync(TApplication application);
    }
}
