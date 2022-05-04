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
        IDictionary<string, string> dockerFileNames;

        public string Version { get; set; }

        public Module_json(string nameSpace, IDictionary<string,string> dockerFileNames)
        {
            this.nameSpace = nameSpace;
            this.dockerFileNames = dockerFileNames;
        }

    }
}
