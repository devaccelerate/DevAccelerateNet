// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using System;
using Ejyle.DevAccelerate.Core;

namespace Ejyle.DevAccelerate.MultiTenancy.Tenants
{
    /// <summary>
    /// Represents the core interface for a tenant.
    /// </summary>
    /// <typeparam name="TKey">The type of a non-nullable key of an entity.</typeparam>
    /// <typeparam name="TKey">The type of a nullable key of an entity.</typeparam>
    public interface IDaTenant<TKey> : IDaAuditedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Gets or sets the type of a tenant.
        /// </summary>
        DaTenantType TenantType { get; set; }

        /// <summary>
        /// Gets or sets the name of a tenant.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the friendly name of a tenant.
        /// </summary>
        string FriendlyName { get; set; }

        /// <summary>
        /// Determines if the tenant is part of the system.
        /// </summary>
        bool IsSystemTenant { get; set; }

        /// <summary>
        /// Gets or sets the country of the tenant.
        /// </summary>
        string Country { get; set; }

        /// <summary>
        /// Gets or sets the currency of the tenant.
        /// </summary>
        string Currency { get; set; }

        /// <summary>
        /// Gets or sets the preferred time zone of the tenant.
        /// </summary>
        string TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the preferred date format of the tenant.
        /// </summary>
        string DateFormat { get; set; }

        /// <summary>
        /// Gets or sets the preferred system language of the tenant.
        /// </summary>
        string SystemLanguage { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who owns the tenant.
        /// </summary>
        string OwnerUserId { get; set; }

        /// <summary>
        /// Gets or sets the status of the tenant.
        /// </summary>
        DaTenantStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the billing email of the tenant.
        /// </summary>
        string BillingEmail { get; set; }
    }
}
