// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Kae.IoT.PnP.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.IoT.PnP.Generator.Csharp.App.template
{
    partial class ProgramDeviceApp_cs : IVersionedGenerator
    {
        private string nameSpace;
        private string configFilename;

        public ProgramDeviceApp_cs(string nameSpace, string configFilename)
        {
            this.nameSpace = nameSpace;
            this.configFilename = configFilename;
        }

        public string Version { get; set; }
    }
}
