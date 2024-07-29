// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

namespace Ejyle.DevAccelerate.MultiTenancy.Tenants
{
    public enum DaDomainVerificationMethod
    {
        HTMLFile = 0,
        MetaTag = 1,
        TXTDNSRecord = 2,
        CNAMEDNSRecord = 3,
        MXDNSRecord = 4,
        Other = 100
    }
}
