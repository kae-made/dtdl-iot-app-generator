// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Kae.IoT.PnP.Generator;
using Microsoft.Azure.DigitalTwins.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.IoT.PnP.Generator.Csharp.Common.template
{
    partial class IIoTApp_cs : IVersionedGenerator
    {
        private string nameSpace;
        private IDictionary<string, GElemDTTelemetryInfo> dtTelemetries;
        private IDictionary<string, GElemDTCommandInfo> syncDirectMethods;
        private IDictionary<string, GElemDTCommandInfo> asyncDirectMethods;

        public IIoTApp_cs(string nameSpace, IDictionary<string,GElemDTTelemetryInfo> telemetries, IDictionary<string, GElemDTCommandInfo> syncDirectMethods, IDictionary<string, GElemDTCommandInfo> asyncDirectMethods)
        {
            this.nameSpace = nameSpace;
            this.dtTelemetries = telemetries;
            this.syncDirectMethods = syncDirectMethods;
            this.asyncDirectMethods = asyncDirectMethods;
        }

        public string Version { get; set; }
    }
}
