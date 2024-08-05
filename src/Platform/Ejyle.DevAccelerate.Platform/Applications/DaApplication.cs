// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using Ejyle.DevAccelerate.Platform.Features;
using Ejyle.DevAccelerate.Core;
using System;
using System.Collections.Generic;

namespace Ejyle.DevAccelerate.Platform.Applications
{
    public class DaApplication : DaApplication<string, DaApplicationAttribute, DaFeature>
    {
        public DaApplication() : base()
        { }
    }

    public class DaApplication<TKey, TApplicationAttribute, TFeature> : DaEntityBase<TKey>, IDaApplication<TKey>
        where TKey : IEquatable<TKey>
        where TApplicationAttribute : IDaApplicationAttribute<TKey>
        where TFeature : IDaFeature<TKey>
    {
        public DaApplication()
        {
            Attributes = new HashSet<TApplicationAttribute>();
            Features = new HashSet<TFeature>();
        }

        public string Name { get; set; }

        public string Key { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<TApplicationAttribute> Attributes { get; set; }

        public virtual ICollection<TFeature> Features { get; set; }
    }
}
