// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kae.IoT.PnP.Generator.Csharp.App.template;
using Kae.IoT.PnP.Generator.Csharp.Service.template;
using Kae.Utility.Logging;

namespace Kae.IoT.PnP.Generator.Csharp
{
    class CsharpCodeGeneratorNonEdge : CSharpCodeGenerator
    {
        protected static readonly string Worker_cs_FileName = "Worker.cs";
        protected static readonly string ServicePropertiesDirName = "Properties";
        protected static readonly string ServiceLaunchSettingsJsonFileName = "launchSettings.json";

        public CsharpCodeGeneratorNonEdge(ExeType exeType, string appName, string iotFWProjectPath) : base(exeType, appName, iotFWProjectPath)
        {
        }
        public CsharpCodeGeneratorNonEdge(ExeType exeType, string appName, string iotFWProjectPath, Logger logger) : base(exeType, appName, iotFWProjectPath, logger)
        {
        }

        protected override async Task CreateProjectEnvironment()
        {
            if (projectExeType== ExeType.Service)
            {
                this.UserSecretsIed = $"dotnet-{GetProjectNameOnCode()}-{Guid.NewGuid().ToString("D").ToUpper()}";
            }
            await CreateProjectEnvironmentCommon();
            if (projectExeType== ExeType.Service)
            {
                var lsSJsonGenerator = new LaunchSettings_json(ClassName);
                var lsSJsonContent = lsSJsonGenerator.TransformText();
                var propDirPath = Path.Join(ProjFolderPath, ServicePropertiesDirName);
                if (!Directory.Exists(propDirPath))
                {
                    Directory.CreateDirectory(propDirPath);
                }
                var lsSJsonFilePath = Path.Join(ServicePropertiesDirName, ServiceLaunchSettingsJsonFileName);
                await WriteToFileAsync(lsSJsonFilePath, lsSJsonContent);

            }

            BuildIoTFWLibrary();
        }

        protected override string GenerateProgramCSContent()
        {
            string content=null;
            switch (projectExeType)
            {
                case ExeType.DeviceApp:
                    var generatorDeviceApp = new ProgramDeviceApp_cs(NameSpace) { Version = currentVersion };
                    content = generatorDeviceApp.TransformText();
                    break;
                case ExeType.Service:
                    var generatorService = new ProgramService_cs(NameSpace) { Version = currentVersion } ;
                    content = generatorService.TransformText();
                    break;
            }
            return content;
        }

        protected override async Task GenerateSpecificCode()
        {
            if (projectExeType == ExeType.Service)
            {
                var generator = new Worker_cs(NameSpace) { Version = currentVersion };
                var content = generator.TransformText();
                await WriteToFileAsync(Worker_cs_FileName, content);
            }
        }
    }
}
