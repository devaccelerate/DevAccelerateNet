// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using System;
using Ejyle.DevAccelerate.Core;

namespace Ejyle.DevAccelerate.Platform.Applications
{
    public class DaApplicationAttribute : DaApplicationAttribute<string, DaApplication>
    {
        public DaApplicationAttribute() : base()
        { }
    }

    public class DaApplicationAttribute<TKey, TApplication> : DaEntityBase<TKey>, IDaApplicationAttribute<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey ApplicationId { get; set; }
        public virtual TApplication Application { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
    }
}
