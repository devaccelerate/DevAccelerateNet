﻿// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ejyle.DevAccelerate.SimpleWorkflow.Xml
{
    public class DaXmlSimpleWorkflowRepository : DaSimpleWorkflowXmlRepository<DaXmlSimpleWorkflow, DaXmlSimpleWorkflowItem, DaXmlSimpleWorkflowItemSetting>
    { }

    public class DaSimpleWorkflowXmlRepository<TSimpleWorkflow, TSimpleWorkflowItem, TSimpleWorkflowItemSetting> : IDaSimpleWorkflowRepository<TSimpleWorkflow, TSimpleWorkflowItem, TSimpleWorkflowItemSetting>
        where TSimpleWorkflow : IDaSimpleWorkflow<TSimpleWorkflowItem, TSimpleWorkflowItemSetting>
        where TSimpleWorkflowItem : IDaSimpleWorkflowItem<TSimpleWorkflowItemSetting>
        where TSimpleWorkflowItemSetting : IDaSimpleWorkflowItemSetting
    {
        public string Location
        {
            get;
            private set;
        }

        public TSimpleWorkflow GetWorkflow(string name)
        {
            TSimpleWorkflow result = default(TSimpleWorkflow);

            try
            {
                var serializer = new XmlSerializer(typeof(DaXmlSimpleWorkflow));
                var path = $"{Location}\\{name}.xml";                
                var reader = new StreamReader(path);
                result = (TSimpleWorkflow)serializer.Deserialize(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Serialization error.", ex);
            }

            return result;
        }

        public void SetLocation(string location)
        {
            Location = location;
        }
    }
}
