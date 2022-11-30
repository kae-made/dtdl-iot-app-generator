// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Kae.IoT.PnP.Generator.Csharp.App.template;
using Kae.IoT.PnP.Generator.Csharp.Edge.template;
using Kae.Utility.Logging;

namespace Kae.IoT.PnP.Generator.Csharp
{
    class CsharpCodeGeneratorEdge : CSharpCodeGenerator
    {
        private static IDictionary<string, string> dockerSdkImages = new Dictionary<string, string> {
            { "amd64", "mcr.microsoft.com/dotnet/sdk/sdk:6.0.403-jammy-amd64" },
            {"arm32v7","mcr.microsoft.com/dotnet/sdk:6.0.403-bullseye-slim-arm32v7" },
            {"arm64v8","mcr.microsoft.com/dotnet/sdk:6.0.403-bullseye-slim-arm64v8," },
            {"windows-amd64","mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1809" }
        };
        private static IDictionary<string, string> dockerRuntimeImages = new Dictionary<string, string>
        {
            {"amd64","mcr.microsoft.com/dotnet/runtime:6.0.11-jammy-amd64" },
            {"arm32v7","mcr.microsoft.com/dotnet/runtime:6.0.11-bullseye-slim-arm32v7" },
            {"arm64v8","mcr.microsoft.com/dotnet/runtime:6.0.11-bullseye-slim-arm64v8" },
            {"windows-amd64","mcr.microsoft.com/dotnet/core/runtime:3.1-nanoserver-1809" }
        };
        private static IEnumerable<string> archNames = new List<string> { "amd64", "arm32v7", "arm64v8", "windows-amd64" };
        private static IEnumerable<string> archDebugNames = new List<string> { "amd64", "arm32v7", "arm64v8" };
        private static IDictionary<string, bool> addUserForArch = new Dictionary<string, bool>
        {
            {"amd64",true },{"arm32v7",true},{"arm64v8",true },{"windows-amd64",false }
        };

        private static readonly string moduleJsonFileName = "module.json";
        private static readonly string dockerignoreOrigFileName = "dockerignore.txt";
        private static readonly string gitignoreOrigFileName = "gitignore.txt";
        private static readonly string dockerignoreFileName = ".dockerignore";
        private static readonly string gitignoreFileName = ".gitignore";

        protected static readonly string[] origFilesFolderPath = new string[] { "Csharp", "Edge", "template" };
        protected string origContentsBasePath = "";

        public CsharpCodeGeneratorEdge(string appName, string iotFWProjectPath) : base(ExeType.Edge, appName, iotFWProjectPath)
        {
            string codeBase = Assembly.GetExecutingAssembly().Location;
            var dirInfo = new DirectoryInfo(codeBase);
            genTemplateFolderPath = Path.Join(dirInfo.Parent.FullName, Path.Join(origFilesFolderPath));
        }

        public CsharpCodeGeneratorEdge(string appName, string iotFWProjectPath, Logger logger) :base(ExeType.Edge,appName,iotFWProjectPath,logger)
        {
            this.factoryCreationMethod = "CreateIoTClientForEdge";

            string codeBase = Assembly.GetExecutingAssembly().Location;
            var dirInfo = new DirectoryInfo(codeBase);
            genTemplateFolderPath = Path.Join(dirInfo.Parent.FullName, Path.Join(origFilesFolderPath));
        }

        protected override async Task CreateProjectEnvironment()
        {
            await CreateProjectEnvironmentCommon();

            await CreateDockerItems();


            string destIoTFWDllPath = Path.Join(ProjFolderPath, iotFWDllFileName);
            string destLoggingFWDllPath = Path.Join(ProjFolderPath, loggingFWDllFileName);
            if ((!File.Exists(destIoTFWDllPath)) && (!File.Exists(destLoggingFWDllPath)))
            {
                if (BuildIoTFWLibrary())
                {
                    var iotFWDllPath = GetIoTFrameworkDllPath();
                    var loggingDllPath = GetLoggingDllPath();
                    File.Copy(iotFWDllPath, destIoTFWDllPath);
                    File.Copy(loggingDllPath, destLoggingFWDllPath);
                }
            }
        }


        protected async Task CreateDockerItems()
        {
            foreach (var arch in archNames)
            {
                var dockerFileGenerator = new Dockerfile(NameSpace, GetProjectNameOnCode(), dockerSdkImages[arch], dockerRuntimeImages[arch], addUserForArch[arch]) { Version = currentVersion };
                var content = dockerFileGenerator.TransformText();
                var fileName = DockerFileName(arch);
                await WriteToFileAsync(fileName, content);
            }
            foreach(var arch in archDebugNames)
            {
                var generator = new Dockerfile_debug(NameSpace, GetProjectNameOnCode(), dockerSdkImages[arch], dockerRuntimeImages[arch]) { Version = currentVersion };
                var content = generator.TransformText();
                var fileName = DockerFileName(arch, true);
                await WriteToFileAsync(fileName, content);
            }
            var dockerfileNames = new Dictionary<string, string>();
            bool existed = true;
            int index = 0;
            while (existed)
            {
                if (index < archNames.Count())
                {
                    string archName = archNames.ElementAt(index);
                    dockerfileNames.Add(archName, $"./{DockerFileName(archName)}");
                }
                if (index < archDebugNames.Count())
                {
                    string archName = archDebugNames.ElementAt(index);
                    dockerfileNames.Add($"{archName}.debug", $"./{DockerFileName(archName, true)}");
                }
                index++;
                if (index>=archNames.Count() && index >= archDebugNames.Count())
                {
                    break;
                }
            }
            var moduleGenerator = new Module_json(NameSpace, GetProjectNameOnCode(), dockerfileNames) { Version = currentVersion };
            var moduleContent = moduleGenerator.TransformText();
            await WriteToFileAsync(moduleJsonFileName, moduleContent);

            var origDockerignoreFileName = Path.Join(genTemplateFolderPath, dockerignoreOrigFileName);
            await BuildProjectFileItem(ProjFolderPath, dockerignoreFileName, origDockerignoreFileName);

            var origGitignoreFileName = Path.Join(genTemplateFolderPath, gitignoreOrigFileName);
            await BuildProjectFileItem(ProjFolderPath, gitignoreFileName, origGitignoreFileName);
        }

        private void Prototype()
        {
            var dockerFileNames = new Dictionary<string, string>();
            string postFix = ",";
            foreach (var arch in dockerFileNames.Keys)
            {
                var fileName = dockerFileNames[arch];
                if (arch == dockerFileNames.Keys.Last())
                {
                    postFix = "";
                }
            }
        }

        protected string DockerFileName(string archName, bool debug = false)
        {
            string fileName = $"Dockerfile.{archName}";
            if (debug) fileName += ".debug";
            return fileName;
        }


        protected override string GenerateProgramCSContent()
        {
            var generator = new ProgramEdge_cs(NameSpace) { Version = currentVersion };
            return generator.TransformText();
        }

        protected override async Task GenerateSpecificCode()
        {
            // Do nothing
        }
    }
}
