// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ejyle.DevAccelerate.Core.EF;
using Microsoft.EntityFrameworkCore;
using Ejyle.DevAccelerate.Core.Data;
using Ejyle.DevAccelerate.Platform.Features;
using Ejyle.DevAccelerate.Platform.Applications;

namespace Ejyle.DevAccelerate.Platform.EF.Applications
{
    public class DaAppRepository : DaApplicationRepository<string, DaApplication, DaApplicationAttribute, DaFeature, DaFeatureAction, DbContext>
    {
        public DaAppRepository(DbContext dbContext)
            : base(dbContext)
        { }
    }


    public class DaApplicationRepository<TKey, TApplication, TAppAttribute, TFeature, TFeatureAction, TDbContext>
        : DaEntityRepositoryBase<TKey, TApplication, TDbContext>, IDaApplicationRepository<TKey, TApplication>
        where TKey : IEquatable<TKey>
        where TApplication : DaApplication<TKey, TAppAttribute, TFeature>
        where TAppAttribute : DaApplicationAttribute<TKey, TApplication>
        where TFeatureAction : DaFeatureAction<TKey, TFeature>
        where TFeature : DaFeature<TKey, TApplication, TFeatureAction>
        where TDbContext : DbContext
    {
        public DaApplicationRepository(TDbContext dbContext)
            : base(dbContext)
        { }

        private DbSet<TApplication> Applications { get { return DbContext.Set<TApplication>(); } }

        public Task CreateAsync(TApplication application)
        {
            Applications.Add(application);
            return SaveChangesAsync();
        }

        public Task DeleteAsync(TApplication app)
        {
            Applications.Remove(app);
            return SaveChangesAsync();
        }

        public Task<TApplication> FindByIdAsync(TKey id)
        {
            return Applications.Where(m => m.Id.Equals(id))
                .Include(m => m.Attributes)
                .Include(m => m.Features)
                .SingleOrDefaultAsync();
        }

        public Task<TApplication> FindByKeyAsync(string key)
        {
            return Applications.Where(m => m.Key == key)
                .Include(m => m.Attributes)
                .Include(m => m.Features)
                .SingleOrDefaultAsync();
        }

        public Task UpdateAsync(TApplication app)
        {
            DbContext.Entry(app).State = EntityState.Modified;
            return SaveChangesAsync();
        }

        public async Task<DaPaginatedEntityList<TKey, TApplication>> FindAllAsync(DaDataPaginationCriteria paginationCriteria)
        {
            var totalCount = await Applications.CountAsync();

            if (totalCount <= 0)
            {
                return null;
            }

            var query = Applications
                .Skip((paginationCriteria.PageIndex - 1) * paginationCriteria.PageSize)
                .Take(paginationCriteria.PageSize)
                .Include(m => m.Attributes)
                .Include(m => m.Features)
                .AsQueryable();

            var result = await query.ToListAsync();

            return new DaPaginatedEntityList<TKey, TApplication>(result
                , new DaDataPaginationResult(paginationCriteria, totalCount));
        }
    }
}
