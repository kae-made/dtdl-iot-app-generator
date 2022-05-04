using Kae.IoT.PnP.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.IoT.PnP.Generator.Csharp.Common.template
{
    partial class IoTApp_cs : IVersionedGenerator
    {
        private string nameSpace;
        private string appConnectorName;

        public IoTApp_cs(string nameSpace, string appConnectorName)
        {
            this.nameSpace = nameSpace;
            this.appConnectorName = appConnectorName;
        }

        public string Version { get; set; }
    }
}
