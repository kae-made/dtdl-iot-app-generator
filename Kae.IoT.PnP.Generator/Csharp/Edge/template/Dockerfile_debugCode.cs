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

        public string Version { get; set; }
        public Dockerfile_debug(string nameSpace, string dockerSdkImage, string dockerRuntimeImage)
        {
            this.nameSpace = nameSpace;
            this.dockerSdkImage = dockerSdkImage;
            this.dockerRuntimeImage = dockerRuntimeImage;
        }
    }
}
