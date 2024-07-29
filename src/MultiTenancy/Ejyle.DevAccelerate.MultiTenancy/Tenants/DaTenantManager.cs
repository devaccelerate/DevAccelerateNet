// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ejyle.DevAccelerate.Core;
using System.Linq;

namespace Ejyle.DevAccelerate.MultiTenancy.Tenants
{
    public class DaTenantManager<TKey, TTenant, TTenantUser, TMSPTenant, TMSPTenantMember>
        : DaEntityManagerBase<TKey, TTenant>
        where TKey : IEquatable<TKey>
        where TTenant : IDaTenant<TKey>
        where TTenantUser : IDaTenantUser<TKey>
        where TMSPTenant : IDaMSPTenant<TKey>
        where TMSPTenantMember : IDaMSPTenantMember<TKey>
    {
        public DaTenantManager(IDaTenantRepository<TKey, TTenant, TTenantUser, TMSPTenant, TMSPTenantMember> repository)
            : base(repository)
        { }

        private IDaTenantRepository<TKey, TTenant, TTenantUser, TMSPTenant, TMSPTenantMember> GetRepository()
        {
            return GetRepository<IDaTenantRepository<TKey, TTenant, TTenantUser, TMSPTenant, TMSPTenantMember>>();
        }

        public Task CreateAsync(TTenant tenant)
        {
            ThrowIfDisposed();
            ThrowIfArgumentIsNull(tenant, nameof(tenant));
            return GetRepository().CreateAsync(tenant);
        }

        public Task CreateAsync(TTenant tenant, TKey mtpTenantId)
        {
            ThrowIfDisposed();
            ThrowIfArgumentIsNull(tenant, nameof(tenant));
            return GetRepository().CreateAsync(tenant, mtpTenantId);
        }

        public Task<TTenant> FindByIdAsync(TKey tenantId)
        {
            ThrowIfDisposed();
            return GetRepository().FindByIdAsync(tenantId);
        }

        public void Create(TTenant tenant)
        {
            ThrowIfDisposed();
            ThrowIfArgumentIsNull(tenant, nameof(tenant));
            DaAsyncHelper.RunSync(() => CreateAsync(tenant));
        }

        public void Create(TTenant tenant, TKey mtpTenantId)
        {
            ThrowIfDisposed();
            ThrowIfArgumentIsNull(tenant, nameof(tenant));
            DaAsyncHelper.RunSync(() => CreateAsync(tenant, mtpTenantId));
        }

        public void Update(TTenant tenant)
        {
            DaAsyncHelper.RunSync(() => UpdateAsync(tenant));
        }

        public TTenant FindById(TKey tenantId)
        {
            return DaAsyncHelper.RunSync(() => FindByIdAsync(tenantId));
        }

        public Task<TTenant> FindByNameAsync(string name)
        {
            ThrowIfDisposed();
            ThrowIfArgumentIsNullOrEmpty(name, nameof(name));
            return GetRepository().FindByNameAsync(name);
        }

        public TTenant FindByName(string name)
        {
            return DaAsyncHelper.RunSync(() => FindByNameAsync(name));
        }

        public Task UpdateAsync(TTenant tenant)
        {
            ThrowIfDisposed();
            ThrowIfArgumentIsNull(tenant, nameof(tenant));
            return GetRepository().UpdateAsync(tenant);
        }

        public Task<List<TTenant>> FindByUserIdAsync(TKey userId)
        {
            ThrowIfDisposed();
            return GetRepository().FindByUserIdAsync(userId);
        }

        public List<TTenant> FindByUserId(TKey userId)
        {
            return DaAsyncHelper.RunSync(() => FindByUserIdAsync(userId));
        }

        public IQueryable<TTenant> Tenants
        {
            get
            {
                return GetRepository().Tenants;
            }
        }

        public IQueryable<TTenantUser> TenantUsers
        {
            get
            {
                return GetRepository().TenantUsers;
            }
        }

        public Task<bool> CheckTenantUserActiveAssociationAsync(TKey tenantId, TKey userId)
        {
            ThrowIfDisposed();
            return GetRepository().CheckTenantUserActiveAssociationAsync(tenantId, userId);
        }

        public bool CheckTenantUserActiveAssociation(TKey tenantId, TKey userId)
        {
            return DaAsyncHelper.RunSync(() => CheckTenantUserActiveAssociationAsync(tenantId, userId));
        }
    }
}
