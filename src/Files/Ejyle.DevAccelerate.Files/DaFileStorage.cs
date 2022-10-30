﻿// ----------------------------------------------------------------------------------------------------------------------
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

namespace Ejyle.DevAccelerate.Files
{
    public class DaFileStorage : DaFileStorage<int>
    { }

    public class DaFileStorage<TKey> : DaEntityBase<TKey>, IDaFileStorage<TKey>
        where TKey : IEquatable<TKey>
    {
        public string Name { get; set; }
        public DaFileStorageType StorageType { get; set; }
        public string Platform { get; set; }
    }
}
