// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Microsoft.Azure.DigitalTwins.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.IoT.PnP.Generator
{
    public interface IGenerator
    {
        string ProjectName { get; set; }
        string GenFolderPath { get; set; }
        Task Generate(
            IDictionary<string, GElemDTInterfaceInfo> dtInterfaces,
            IDictionary<string, GElemDTTelemetryInfo> dtTelemetries,
            IDictionary<string, GElemDTPropertyInfo> dtDesiredProperties,
            IDictionary<string, GElemDTPropertyInfo> dtReporedProperties,
            IDictionary<string, GElemDTCommandInfo> dtSyncDirectMethods,
            IDictionary<string, GElemDTCommandInfo> dtAsyncDirectMethods);
    }
}
