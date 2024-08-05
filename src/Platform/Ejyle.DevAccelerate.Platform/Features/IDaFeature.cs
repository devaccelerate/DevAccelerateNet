// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using System;
using Ejyle.DevAccelerate.Core;

namespace Ejyle.DevAccelerate.Platform.Features
{
    public interface IDaFeature<TKey> : IDaEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TKey ApplicationId { get; set; }
        string Name { get; set; }
        string Key { get; set; }
        bool IsActive { get; set; }
    }
}
