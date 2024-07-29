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
    public class DaTenant : DaTenant<string, DaTenantUser, DaTenantAttribute, DaMSPTenant, DaMSPTenantMember, DaTenantDomain>
    {
        public DaTenant() : base()
        { }
    }

    public class DaTenant<TKey, TTenantUser, TTenantAttribute, TMSPTenant, TMSPTenantMember, TTenantDomain> : DaAuditedEntityBase<TKey>, IDaTenant<TKey>
        where TKey : IEquatable<TKey>
        where TTenantUser : IDaTenantUser<TKey>
        where TTenantAttribute : IDaTenantAttribute<TKey>
        where TMSPTenant : IDaMSPTenant<TKey>
        where TMSPTenantMember : IDaMSPTenantMember<TKey>
        where TTenantDomain : IDaTenantDomain<TKey>
    {
        public DaTenant()
        {
            TenantUsers = new HashSet<TTenantUser>();
            MSPTenantMembers = new HashSet<TMSPTenantMember>();
            MSPTenants = new HashSet<TMSPTenant>();
            Domains = new HashSet<TTenantDomain>();
        }

        public virtual ICollection<TTenantUser> TenantUsers { get; set; }
        public virtual ICollection<TTenantAttribute> Attributes { get; set; }
        public DaTenantType TenantType { get; set; }
        public string OwnerUserId { get; set; }
        public string Name { get; set; }
        public string FriendlyName { get; set; }
        public bool IsSystemTenant { get; set; }
        public DaTenantStatus Status { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public string TimeZone { get; set; }
        public string BillingEmail { get; set; }
        public string DateFormat { get; set; }
        public string SystemLanguage { get; set; }
        public ICollection<TTenantDomain> Domains { get; set; }
        public virtual ICollection<TMSPTenant> MSPTenants { get; set; }
        public virtual ICollection<TMSPTenantMember> MSPTenantMembers { get; set; }
    }
}