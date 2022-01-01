﻿// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using Ejyle.DevAccelerate.EnterpriseSecurity.Apps;
using Microsoft.Extensions.Options;

namespace Ejyle.DevAccelerate.EnterpriseSecurity.EF.Apps
{
    public class DaAppManager : DaAppManager<int, DaApp>
    {
        public DaAppManager(IOptions<DaAppSettings> options, DaAppRepository repository)
            : base(options, repository)
        { }

        public DaAppManager(DaAppRepository repository)
            : base(repository)
        { }
    }
}
