﻿// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ejyle.DevAccelerate.Core;

namespace Ejyle.DevAccelerate.EnterpriseSecurity.Apps
{
    public class DaAppAttribute : DaAppAttribute<int, DaApp>
    {
        public DaAppAttribute() : base()
        { }
    }

    public class DaAppAttribute<TKey, TApp> : DaEntityBase<TKey>, IDaAppAttribute<TKey>
        where TKey : IEquatable<TKey>
    {
        [Required]
        public TKey AppId { get; set; }
        public virtual TApp App { get; set; }

        [Required]
        [StringLength(256)]
        public string AttributeName { get; set; } 
        public string? AttributeValue { get; set; } 
    }
}
