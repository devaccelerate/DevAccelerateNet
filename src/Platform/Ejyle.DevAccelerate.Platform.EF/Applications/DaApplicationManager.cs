// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using Ejyle.DevAccelerate.Platform.Applications;

namespace Ejyle.DevAccelerate.Platform.EF.Applications
{
    public class DaApplicationManager : DaApplicationManager<string, DaApplication>
    {
        public DaApplicationManager(DaAppRepository repository)
            : base(repository)
        { }
    }
}
