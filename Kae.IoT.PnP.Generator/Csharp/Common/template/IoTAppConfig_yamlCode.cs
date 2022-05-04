using Kae.IoT.PnP.Generator.Csharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.IoT.PnP.Generator.Csharp.Common.template
{
    partial class IoTAppConfig_yaml
    {
        private ExeType exeType;
        private string modelId;
        public IoTAppConfig_yaml(ExeType exeType, string modelId)
        {
            this.exeType = exeType;
            this.modelId = modelId;
        }

        private bool IsEdge() { return exeType == ExeType.Edge; }
    }
}
