// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Kae.IoT.PnP.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.IoT.PnP.Generator.Csharp.Common.template
{
    partial class IoTApp_cs : IVersionedGenerator
    {
        private string nameSpace;
        private string appConnectorName;
        private string factoryCreationMethod;
        private IDictionary<string, GElemDTTelemetryInfo> dtTelemetries;

        public IoTApp_cs(string nameSpace, string appConnectorName, string factoryCreationMethod, IDictionary<string, GElemDTTelemetryInfo> telemetries)
        {
            this.nameSpace = nameSpace;
            this.appConnectorName = appConnectorName;
            this.factoryCreationMethod = factoryCreationMethod;
            this.dtTelemetries = telemetries;
        }

        public string Version { get; set; }
    }
}
