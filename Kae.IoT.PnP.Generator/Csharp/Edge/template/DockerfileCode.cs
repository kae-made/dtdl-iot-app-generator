using Kae.IoT.PnP.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.IoT.PnP.Generator.Csharp.Edge.template
{
    partial class Dockerfile : IVersionedGenerator
    {
        private string dockerSdkImage;
        private string dockerRuntimeImage;
        private string nameSpace;
        private bool adduser;

        public string Version { get; set; }

        public Dockerfile(string nameSpace, string dockerSdkImage, string dockerRuntimeImage, bool adduser=true)
        {
            this.nameSpace = nameSpace;
            this.dockerSdkImage = dockerSdkImage;
            this.dockerRuntimeImage = dockerRuntimeImage;
            this.adduser = adduser;
        }
    }
}
