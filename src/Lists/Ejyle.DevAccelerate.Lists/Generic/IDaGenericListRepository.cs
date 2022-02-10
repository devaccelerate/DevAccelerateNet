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

namespace Ejyle.DevAccelerate.Lists.Generic
{
    public interface IDaGenericListRepository<TKey, TGenericList> : IDaEntityRepository<TKey, TGenericList>
        where TKey : IEquatable<TKey>
        where TGenericList : IDaGenericList<TKey>
    {
        Task CreateAsync(TGenericList genericList);
        Task UpdateAsync(TGenericList genericList);
        Task DeleteAsync(TGenericList genericList);

        Task<TGenericList> FindByIdAsync(TKey id);
        Task<List<TGenericList>> FindAllAsync();
        Task<DaPaginatedEntityList<TKey, TGenericList>> FindAllAsync(DaDataPaginationCriteria paginationCriteria);
        Task<TGenericList> FindByNameAsync(string name);
    }
}
