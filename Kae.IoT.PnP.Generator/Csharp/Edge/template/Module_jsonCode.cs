// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Kae.IoT.PnP.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.IoT.PnP.Generator.Csharp.Edge.template
{
    partial class Module_json : IVersionedGenerator
    {
        private string nameSpace;
        private string projectName;
        IDictionary<string, string> dockerFileNames;

        public string Version { get; set; }

        public Module_json(string nameSpace, string projectName, IDictionary<string,string> dockerFileNames)
        {
            this.nameSpace = nameSpace;
            this.projectName = projectName;
            this.dockerFileNames = dockerFileNames;
        }

    }
}
