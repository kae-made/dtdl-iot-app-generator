using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kae.IoT.PnP.Generator.Csharp.App.template;

namespace Kae.IoT.PnP.Generator.Csharp
{
    class CsharpCodeGeneratorNonEdge : CSharpCodeGenerator
    {
        public CsharpCodeGeneratorNonEdge(ExeType exeType, string iotFWProjectPath) : base(exeType, iotFWProjectPath)
        {
        }

        protected override async Task CreateProjectEnvironment()
        {
            await CreateProjectEnvironmentCommon();
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
    }
}
