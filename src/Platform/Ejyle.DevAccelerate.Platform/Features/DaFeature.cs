// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using Ejyle.DevAccelerate.Platform.Applications;
using Ejyle.DevAccelerate.Core;
using System;
using System.Collections.Generic;

namespace Ejyle.DevAccelerate.Platform.Features
{
    public class DaFeature : DaFeature<string, DaApplication, DaFeatureAction>
    {
        public DaFeature() : base()
        { }
    }

    public class DaFeature<TKey, TApplication, TFeatureAction>
        : DaEntityBase<TKey>, IDaFeature<TKey>
        where TKey : IEquatable<TKey>
        where TApplication : IDaApplication<TKey>
        where TFeatureAction : IDaFeatureAction<TKey>
    {
        public DaFeature()
            : base()
        {
            FeatureActions = new HashSet<TFeatureAction>();
        }

        public string Name { get; set; }

        public string Key { get; set; }

        public TKey ApplicationId { get; set; }

        public bool IsActive { get; set; }

        public virtual TApplication Application { get; set; }

        public virtual ICollection<TFeatureAction> FeatureActions { get; set; }
    }
}
