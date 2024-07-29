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

using Ejyle.DevAccelerate.MultiTenancy.Tenants;

namespace Ejyle.DevAccelerate.MultiTenancy.EF.Tenants
{
    public class DaTenantRepository : DaTenantRepository<string, DaTenant, DaTenantUser, DaTenantAttribute, DaMSPTenant, DaMSPTenantMember, DaTenantDomain, DbContext>
    {
        public DaTenantRepository(DbContext dbContext)
            : base(dbContext)
        { }
    }

    public class DaTenantRepository<TKey, TTenant, TTenantUser, TTenantAttribute, TMSPTenant, TMSPTenantMember, TTenantDomain, TDbContext>
         : DaEntityRepositoryBase<TKey, TTenant, DbContext>, IDaTenantRepository<TKey, TTenant, TTenantUser, TMSPTenant, TMSPTenantMember>
        where TKey : IEquatable<TKey>
        where TTenant : DaTenant<TKey, TTenantUser, TTenantAttribute, TMSPTenant, TMSPTenantMember, TTenantDomain>
        where TMSPTenant : DaMSPTenant<TKey, TTenant, TMSPTenantMember>
        where TMSPTenantMember : DaMSPTenantMember<TKey, TTenant, TMSPTenant>, new()
        where TTenantUser : DaTenantUser<TKey, TTenant>
        where TTenantAttribute : DaTenantAttribute<TKey, TTenant>
        where TTenantDomain : DaTenantDomain<TKey, TTenant>
    {
        public DaTenantRepository(DbContext dbContext)
            : base(dbContext)
        { }

        private DbSet<TMSPTenant> MSPTenantsSet { get { return DbContext.Set<TMSPTenant>(); } }
        private DbSet<TTenant> TenantsSet { get { return DbContext.Set<TTenant>(); } }
        private DbSet<TTenantUser> TenantUsersSet { get { return DbContext.Set<TTenantUser>(); } }
        private DbSet<TMSPTenantMember> MSPTenantMembersSet { get { return DbContext.Set<TMSPTenantMember>(); } }
        public IQueryable<TTenant> Tenants => TenantsSet.AsQueryable();
        public IQueryable<TTenantUser> TenantUsers => TenantUsersSet.AsQueryable();
        public IQueryable<TMSPTenant> MSPTenants => MSPTenantsSet.AsQueryable();
        public IQueryable<TMSPTenantMember> MSPTenantMembers => MSPTenantMembersSet.AsQueryable();

        public Task CreateAsync(TTenant tenant)
        {
            TenantsSet.Add(tenant);
            return SaveChangesAsync();
        }

        public Task<TTenant> FindByIdAsync(TKey tenantId)
        {
            return TenantsSet.Where(m => m.Id.Equals(tenantId))
                .Include(x => x.TenantUsers)
                .Include(x => x.Domains)
                .Include(x => x.Attributes)
                .SingleOrDefaultAsync();
        }

        public Task UpdateAsync(TTenant tenant)
        {
            DbContext.Entry(tenant).State = EntityState.Modified;
            return SaveChangesAsync();
        }

        public Task CreateTenantUserAsync(TTenantUser tenantUser)
        {
            TenantUsersSet.Add(tenantUser);
            return SaveChangesAsync();
        }

        public Task<List<TTenant>> FindByUserIdAsync(TKey userId)
        {
            return TenantsSet.Where(m => m.TenantUsers.Any(x => x.UserId.Equals(userId)))
                .Include(x => x.TenantUsers)
                .Include(x => x.Domains)
                .Include(x => x.Attributes)
                .ToListAsync();
        }

        public async Task<bool> CheckTenantUserActiveAssociationAsync(TKey tenantId, TKey userId)
        {
            var tenantUser = await TenantUsersSet.Where(m => m.TenantId.Equals(tenantId) && m.UserId.Equals(userId)).SingleOrDefaultAsync();

            if (!tenantUser.IsActive)
            {
                return false;
            }

            return tenantUser != null;
        }

        public Task<List<TTenant>> FindByAttributeAsync(string attributeName, string attributeValue)
        {
            return TenantsSet.Where(m => m.Attributes.Any(x => x.AttributeName == attributeName && x.AttributeValue == attributeValue))
                .Include(x => x.TenantUsers)
                .Include(x => x.Domains)
                .Include(x => x.Attributes)
                .ToListAsync();
        }

        public Task<TTenant> FindByNameAsync(string name)
        {
            return TenantsSet.Where(m => m.Name == name)
                .Include(x => x.TenantUsers)
                .Include(x => x.Domains)
                .Include(x => x.Attributes)
                .SingleOrDefaultAsync();
        }

        public async Task CreateAsync(TTenant tenant, TKey mspTenantId)
        {
            TenantsSet.Add(tenant);

            var mspTenant = await MSPTenantsSet.Where(m => m.Id.Equals(mspTenantId)).SingleOrDefaultAsync();   

            if(mspTenant == null)
            {
                throw new InvalidOperationException("MSP tenant not found.");
            }

            TenantsSet.Add(tenant);

            var mspTenantMember = new TMSPTenantMember()
            {
                IsActive = true,
                MSPTenantId = mspTenantId,
                TenantId = tenant.Id,
                Tenant = tenant,
                MSPTenant = mspTenant,
                CreatedBy = tenant.CreatedBy,
                LastUpdatedBy = tenant.LastUpdatedBy,
                CreatedDateUtc = tenant.CreatedDateUtc,
                LastUpdatedDateUtc = tenant.LastUpdatedDateUtc
            };

            MSPTenantMembersSet.Add(mspTenantMember);
            await SaveChangesAsync();
        }

        public async Task CreateMSPTenantAsync(TMSPTenant mspTenant)
        {
            MSPTenantsSet.Add(mspTenant);
            await SaveChangesAsync();
        }

        public async Task UpdateMSPTenantAsync(TMSPTenant mspTenant)
        {
            MSPTenantsSet.Update(mspTenant);
            await SaveChangesAsync();
        }

        public Task<TMSPTenant> FindMSPTenantByIdAsync(TKey mspTenatId)
        {
            return MSPTenantsSet.Where(m => m.Id.Equals(mspTenatId)).SingleOrDefaultAsync();
        }

        public async Task CreateMSPTenantMemberAsync(TMSPTenantMember mspMember)
        {
            MSPTenantMembersSet.Add(mspMember);
            await SaveChangesAsync();
        }

        public async Task UpdateMSPTenantMemberAsync(TMSPTenantMember mspMember)
        {
            MSPTenantMembersSet.Update(mspMember);
            await SaveChangesAsync();
        }

        public Task<TMSPTenantMember> FindMSPTenantMemberByIdAsync(TKey mspTenatMemberId)
        {
            return MSPTenantMembersSet.Where(m => m.Id.Equals(mspTenatMemberId)).SingleOrDefaultAsync();    
        }
    }
}
