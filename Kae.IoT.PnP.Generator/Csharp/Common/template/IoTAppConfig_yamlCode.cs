// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
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
            if (!this.modelId.StartsWith("dtmi:"))
            {
                this.modelId = $"dtmi:{modelId}";
            }
        }

        private bool IsEdge() { return exeType == ExeType.Edge; }
    }
}
