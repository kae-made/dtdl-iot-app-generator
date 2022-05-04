using Kae.IoT.PnP.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.IoT.PnP.Generator.Csharp.App.template
{
    partial class ProgramService_cs : IVersionedGenerator
    {
        private string nameSpace;
        public string Version { get; set; }

        public ProgramService_cs(string nameSpace)
        {
            this.nameSpace = nameSpace;
        }
    }
}
