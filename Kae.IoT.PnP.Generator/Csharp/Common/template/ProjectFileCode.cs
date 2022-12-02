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
    partial class ProjectFile
    {
        private ExeType exeType;
        private string configFileName;
        private string userSecretsId;
        private string sdkName;
        private string targetFramework;
        private string outputType;
        private string iotFrameworkProjectPath;
        private IList<string> importLibraries;
        private bool useNuGetForIoTFW;

        public ProjectFile(ExeType exeType,string configFileName, string ioTFrameworkProjectPath, IList<string> importLibraries, bool useNuGetForIoTFW, string userSecretsId = null)
        {
            this.exeType = exeType;
            this.configFileName = configFileName;
            this.userSecretsId = userSecretsId;
            this.iotFrameworkProjectPath = ioTFrameworkProjectPath;
            this.importLibraries = importLibraries;
            this.useNuGetForIoTFW = useNuGetForIoTFW;

            switch (exeType)
            {
                case ExeType.DeviceApp:
                    this.sdkName = "Microsoft.NET.Sdk";
                    this.targetFramework = "net6.0";
                    this.outputType = "Exe";
                    break;
                case ExeType.Service:
                    this.sdkName = "Microsoft.NET.Sdk.Worker";
                    this.targetFramework = "net6.0";
                    break;
                case ExeType.Edge:
                    this.sdkName = "Microsoft.NET.Sdk";
                    this.targetFramework = "net6.0";
                    this.outputType = "Exe";
                    break;
            }

            this.importLibraries = importLibraries;
        }

        private bool IsDeviceApp() { return exeType == ExeType.DeviceApp; }
        private bool IsService() { return exeType == ExeType.Service; }
        private bool IsEdge() { return exeType == ExeType.Edge; }

        public void Prototype()
        {
            switch (exeType)
            {
                case ExeType.DeviceApp:
                    break;
                case ExeType.Service:
                    break;
                case ExeType.Edge:
                    break;
            }

            if (!string.IsNullOrEmpty(outputType))
            {

            }
            if (!string.IsNullOrEmpty(userSecretsId))
            {

            }
            if (exeType == ExeType.Edge)
            {

            }
            if (IsDeviceApp() || IsService())
            {

            }

            foreach (var ilib in importLibraries)
            {
                string[] lib = ilib.Split(new char[] { ';' });
            }
        }
    }
}
