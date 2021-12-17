﻿// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using Ejyle.DevAccelerate.Core;
using System;

namespace Ejyle.DevAccelerate.EnterpriseSecurity.Subscriptions
{
    public interface IDaSubscriptionFeatureUser<TKey> : IDaEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TKey SubscriptionFeatureId { get; set; }

        TKey UserId { get; set; }

        bool IsEnabled { get; set; }
    }
}
