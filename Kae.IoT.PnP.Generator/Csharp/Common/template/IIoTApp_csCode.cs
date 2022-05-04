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
        private IDictionary<string, GElemDTCommandInfo> syncDirectMethods;
        private IDictionary<string, GElemDTCommandInfo> asyncDirectMethods;

        public IIoTApp_cs(string nameSpace, IDictionary<string, GElemDTCommandInfo> syncDirectMethods, IDictionary<string, GElemDTCommandInfo> asyncDirectMethods)
        {
            this.nameSpace = nameSpace;
            this.syncDirectMethods = syncDirectMethods;
            this.asyncDirectMethods = asyncDirectMethods;
        }

        public string Version { get; set; }
    }
}
