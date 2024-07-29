// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using Ejyle.DevAccelerate.Core;
using System;

namespace Ejyle.DevAccelerate.MultiTenancy.Tenants
{
    public class DaMSPTenantMember : DaMSPTenantMember<string, DaTenant, DaMSPTenant>
    {
        public DaMSPTenantMember() : base()
        { }
    }

    public class DaMSPTenantMember<TKey, TTenant, TMSPTenant> : DaAuditedEntityBase<TKey>, IDaMSPTenantMember<TKey>
        where TKey : IEquatable<TKey>
        where TTenant : IDaTenant<TKey>
    {
        public TKey MSPTenantId { get; set; }
        public TKey TenantId { get; set; }
        public int MSPMemberNumber { get; set; }
        public bool IsActive { get; set; }
        public virtual TMSPTenant MSPTenant { get; set; }
        public virtual TTenant Tenant { get; set; }
    }
}