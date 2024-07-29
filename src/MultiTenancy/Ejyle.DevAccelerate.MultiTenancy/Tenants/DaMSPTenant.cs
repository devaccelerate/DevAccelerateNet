// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using Ejyle.DevAccelerate.Core;
using System;
using System.Collections.Generic;

namespace Ejyle.DevAccelerate.MultiTenancy.Tenants
{
    public class DaMSPTenant : DaMSPTenant<string, DaTenant, DaMSPTenantMember>
    {
        public DaMSPTenant() : base()
        { }
    }

    public class DaMSPTenant<TKey, TTenant, TMSPTenantMember> : DaAuditedEntityBase<TKey>, IDaMSPTenant<TKey>
        where TKey : IEquatable<TKey>
        where TTenant : IDaTenant<TKey>
        where TMSPTenantMember : IDaMSPTenantMember<TKey>
    {
        public TKey TenantId { get; set; }
        public int MSPNumber { get; set; }
        public virtual TTenant Tenant { get; set; }
        public virtual ICollection<TMSPTenantMember> Members { get; set; } = new HashSet<TMSPTenantMember>();
    }
}