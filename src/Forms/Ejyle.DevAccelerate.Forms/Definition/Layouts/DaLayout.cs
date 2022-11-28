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
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Ejyle.DevAccelerate.Forms.Definition.Layouts
{
    public class DaLayout<TKey, TNullableKey, TFormSection, TLayoutColumn, TTabPanelTab> : DaAuditedEntityBase<TKey>, IDaAuditedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public DaLayout()
        {
            ChildLayoutColumns = new HashSet<TLayoutColumn>();
        }

        string Name { get; set; }
        string Key { get; set; }
        public TNullableKey FormSectionId { get; set; }
        public TNullableKey ParentLayoutColumnId { get; set; }
        public TNullableKey ParentTabPanelTabId { get; set; }
        public virtual TFormSection FormSection { get; set; }
        public virtual TLayoutColumn ParentLayoutColumn { get; set; }
        public virtual TTabPanelTab ParentTabPanelTab { get; set; }
        public virtual ICollection<TLayoutColumn> ChildLayoutColumns { get; set; }
    }
}
