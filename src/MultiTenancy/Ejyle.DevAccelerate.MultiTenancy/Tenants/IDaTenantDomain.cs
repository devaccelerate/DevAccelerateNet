// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using Ejyle.DevAccelerate.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejyle.DevAccelerate.MultiTenancy.Tenants
{
    public interface IDaTenantDomain<TKey> : IDaAuditedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TKey TenantId { get; set; }

        string Domain
        {
            get; set;
        }

        bool? IsDomainVerified { get; set; }
        DaDomainVerificationMethod? VerificationMethod { get; set; }
        string VerificationValue { get; set; }
        string VerificationFileName { get; set; }
        DateTime? VerificationCodeCreatedDateUtc { get; set; }
        DateTime? VerificationCodeExpiryDateUtc { get; set; }
        bool IsPrimary { get; set; }
    }
}
