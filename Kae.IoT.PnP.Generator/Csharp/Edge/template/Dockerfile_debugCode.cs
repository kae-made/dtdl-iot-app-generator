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
    partial class Dockerfile_debug : IVersionedGenerator
    {
        private string dockerSdkImage;
        private string dockerRuntimeImage;
        private string nameSpace;
        private string projectName;

        public string Version { get; set; }
        public Dockerfile_debug(string nameSpace, string projectName, string dockerSdkImage, string dockerRuntimeImage)
        {
            this.nameSpace = nameSpace;
            this.projectName = projectName;
            this.dockerSdkImage = dockerSdkImage;
            this.dockerRuntimeImage = dockerRuntimeImage;
        }
    }
}
