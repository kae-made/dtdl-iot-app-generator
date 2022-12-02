// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Microsoft.Azure.DigitalTwins.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Kae.IoT.PnP.Generator.Csharp.App.template;
using Kae.IoT.PnP.Generator.Csharp.Common.template;
using System.Diagnostics;
using Kae.Utility.Logging;

namespace Kae.IoT.PnP.Generator.Csharp
{
    public enum ExeType
    {
        DeviceApp,
        Service,
        Edge
    }

    public abstract class CSharpCodeGenerator : IGenerator, IVersionedGenerator
    {
        public string ModelId { get; set; }
        public string ProjectName { get; set; }
        public string UserSecretsIed { get; set; }
        public string NameSpace { get; set; }
        public string GenFolderPath { get; set; }
        public string ClassName { get; set; }
        public string AppConnectorName { get; set; }
        public string factoryCreationMethod { get; set; }

        public string ProjFolderPath { get; set; }
        public string IndentUnit { get; set; }
        public string Indent { get; set; }
        public string Version { get; set; }

        protected static readonly string currentVersion = "0.1.1";
        protected string genTemplateFolderPath;
        protected string iotFrameworkProjectPath;
        protected bool useNuGetForIoTFW;
        protected IList<string> importLibraries;

        protected ExeType projectExeType;

        protected Logger logger;

        public CSharpCodeGenerator(ExeType exeType, string appName, string iotFrameworkProjectPath, bool useNuGetForIoTFW, IList<string> importLibraries)
        {
            Version = currentVersion;
            this.projectExeType = exeType;
            this.iotFrameworkProjectPath = iotFrameworkProjectPath;
            this.ProjectName = appName;

            this.useNuGetForIoTFW = useNuGetForIoTFW;
            this.importLibraries = importLibraries;

            string codeBase = Assembly.GetExecutingAssembly().Location;
            var dirInfo = new DirectoryInfo(codeBase);
            genTemplateFolderPath = Path.Join(dirInfo.Parent.FullName, Path.Join(templateFolderPath));
        }

        public CSharpCodeGenerator(ExeType exeType, string appName, string iotFrameworkProjectPath, bool useNuGetForIoTFW, IList<string> importLibraries, Logger logger) : this(exeType, appName, iotFrameworkProjectPath, useNuGetForIoTFW, importLibraries)
        {
            this.logger = logger;
        }

        public async Task Generate(
            IDictionary<string, GElemDTInterfaceInfo> dtInterfaces,
            IDictionary<string, GElemDTTelemetryInfo> dtTelemetries,
            IDictionary<string, GElemDTPropertyInfo> dtDesiredProperties,
            IDictionary<string, GElemDTPropertyInfo> dtReporedProperties,
            IDictionary<string, GElemDTCommandInfo> dtSyncDirectMethods,
            IDictionary<string, GElemDTCommandInfo> dtAsyncDirectMethods)
        {
            string codeBase = Assembly.GetExecutingAssembly().Location;
            var dirInfo = new DirectoryInfo(codeBase);

            await CreateProjectEnvironment();

            await GenerateAppIoTDataCS(dtTelemetries, dtDesiredProperties, dtReporedProperties, dtSyncDirectMethods, dtAsyncDirectMethods);

            await GenerateIIoTAppCS(dtSyncDirectMethods, dtAsyncDirectMethods);
            await GenerateIoTAppCS(factoryCreationMethod);
            await GenerateIoTAppCodeCS(dtSyncDirectMethods, dtAsyncDirectMethods);
            await GenerateIoTAppConnectorCS(dtSyncDirectMethods, dtAsyncDirectMethods);
            await GenerateProgramCS();
            await GenerateSpecificCode();
        }

        protected static readonly string[] templateFolderPath = new string[] { "Kae", "IoT", "PnP", "Generator", "Csharp", "DeviceApp", "template" };
        private static readonly string csProjFileTemplateName = "csprojfile.txt";
        protected static readonly string csProjFileExtName = ".csproj";
        private static readonly string configFileTemplateName = "config.yaml.txt";
        protected static readonly string configFileName = "iot-app-config.yaml";

        protected static readonly string IIoTApp_cs_FileName = "IIoTApp.cs";
        protected static readonly string IoTApp_cs_FileName = "IoTApp.cs";
        protected static readonly string IoTApp_code_cs_FileName = "IoTApp.code.cs";
        protected static readonly string Program_cs_FileName = "Program.cs";
        protected static readonly string AppIoTData_cs_FileName = "AppIoTData.cs";
        protected static readonly string IoTAppConnector_Fragment_FileName = "IoTAppConnector.cs";
        protected static readonly string IoTAppConnector_Classname_PostFix = "IoTAppConnector";

        protected static readonly string D2CDataTypeName = "D2CData";
        protected static readonly string DPDataTypeName = "AppDTDesiredProperties";
        protected static readonly string RPDataTypeName = "AppDTReporetedProperties";
        protected static readonly string FWIoTDataTypeName = "Kae.IoT.Framework.IoTData";

        protected static readonly string BuildDLLPath = "out";
        protected static readonly string iotFWDllFileName = "Kae.IoT.Framework.dll";
        protected static readonly string loggingFWDllFileName = "Kae.Utility.Logging.dll";


        public static CSharpCodeGenerator CreateGenerator(string genFolderPath, string modelId, string appName, string nameSpace, string exeTypeParam, string ioTFrameworkProjectPath, bool useNuGetForIoTFW, IList<string> importLibraries, Logger logger = null)
        {
            ExeType exeType = ExeType.DeviceApp;
            switch (exeTypeParam.ToLower())
            {
                case "deviceapp":
                    exeType = ExeType.DeviceApp;
                    break;
                case "service":
                    exeType = ExeType.Service;
                    break;
                case "edge":
                    exeType = ExeType.Edge;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("exeTypeParam should be 'DeviceApp' or 'Service' or 'Edge'!");
            }
            CSharpCodeGenerator generator = null;

            switch (exeType)
            {
                case ExeType.DeviceApp:
                case ExeType.Service:
                    generator = new CsharpCodeGeneratorNonEdge(exeType, appName, ioTFrameworkProjectPath, useNuGetForIoTFW, importLibraries, logger) { GenFolderPath = genFolderPath, ModelId = modelId };
                    break;
                case ExeType.Edge:
                    generator = new CsharpCodeGeneratorEdge(appName, ioTFrameworkProjectPath, useNuGetForIoTFW, importLibraries, logger) { GenFolderPath = genFolderPath, ModelId = modelId };
                    break;
            }
            generator.NameSpace = $"{nameSpace}.{generator.GetProjectNameOnCode()}";
            return generator;
        }

        protected abstract Task CreateProjectEnvironment();
        protected abstract string GenerateProgramCSContent();
        protected async Task CreateProjectEnvironmentCommon()
        {
            var projName = GetProjectNameOnCode();
            ProjFolderPath = Path.Join(GenFolderPath, projName);
            if (!Directory.Exists(ProjFolderPath))
            {
                Directory.CreateDirectory(ProjFolderPath);
            }
            var csharpProjFileName = projName + csProjFileExtName;

            var projFileGenerator = new ProjectFile(projectExeType, configFileName, iotFrameworkProjectPath, importLibraries, useNuGetForIoTFW, UserSecretsIed);
            var content = projFileGenerator.TransformText();
            await WriteToFileAsync(csharpProjFileName, content);

            var configFileGenerator = new IoTAppConfig_yaml(projectExeType, ModelId);
            content = configFileGenerator.TransformText();
            await WriteToFileAsync(configFileName, content);

            ClassName = projName;
            AppConnectorName = $"{projName}AppConnector";
        }

        public string GetProjectNameOnCode()
        {
            var resultName = "";
            var pns = ProjectName.Split(new char[] { ' ' });
            foreach (var pnf in pns)
            {
                resultName += pnf.Substring(0, 1).ToUpper() + pnf.Substring(1);
            }
            return resultName;
        }

        public string GetIoTFrameworkDllPath()
        {
            return Path.Join(iotFrameworkProjectPath, BuildDLLPath, iotFWDllFileName);
        }

        public string GetLoggingDllPath()
        {
            return Path.Join(iotFrameworkProjectPath, BuildDLLPath, loggingFWDllFileName); ;
        }

        public async Task CreateProjectEnvironmentOld()
        {
            var projName = "";
            var pns = ProjectName.Split(new char[] { ' ' });
            foreach (var pnf in pns)
            {
                projName += pnf.Substring(0, 1).ToUpper() + pnf.Substring(1);
            }
            ProjFolderPath = Path.Join(GenFolderPath, projName);
            if (!Directory.Exists(ProjFolderPath))
            {
                Directory.CreateDirectory(ProjFolderPath);
            }
            var csharpProjFileName = projName + csProjFileExtName;
            var originalProjFileName = Path.Join(genTemplateFolderPath, csProjFileTemplateName);

            await BuildProjectFileItem(ProjFolderPath, csharpProjFileName, originalProjFileName);
            var originalConfigFileName = Path.Join(genTemplateFolderPath, configFileTemplateName);
            await BuildProjectFileItem(ProjFolderPath, configFileName, originalConfigFileName);

            ClassName = projName;
            AppConnectorName = $"{projName}AppConnector";
        }

        protected async Task BuildProjectFileItem(string folderName, string generatedFileName, string originalFileName)
        {

            var generatedFilePath = Path.Join(folderName, generatedFileName);
            if (!File.Exists(generatedFilePath))
            {
                using (var reader = new StreamReader(originalFileName))
                {
                    using (var writer = new StreamWriter(generatedFilePath))
                    {
                        var content = await reader.ReadToEndAsync();
                        await writer.WriteAsync(content);
                        await writer.FlushAsync();
                    }
                }
            }
        }

        protected bool BuildIoTFWLibrary()
        {
            string iotFWDllPath = GetIoTFrameworkDllPath();
            string loggingDllPath = GetLoggingDllPath();
            bool result = true;
            if ((!File.Exists(iotFWDllPath)) && (!File.Exists(loggingDllPath)))
            {
                using (var fwBuildProcess = new Process())
                {
                    fwBuildProcess.StartInfo.WorkingDirectory = iotFrameworkProjectPath;
                    fwBuildProcess.StartInfo.Arguments = $"publish -c Release -o {BuildDLLPath}";
                    fwBuildProcess.StartInfo.FileName = "dotnet";
                    if (fwBuildProcess.Start())
                    {
                        logger?.LogInfo("Succeeded to build IoT Framework Library.");
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }


        public async Task GenerateAppIoTDataCS(
            IDictionary<string, GElemDTTelemetryInfo> telemetries,
            IDictionary<string, GElemDTPropertyInfo> desiredProperties,
            IDictionary<string, GElemDTPropertyInfo> reportedProperties,
            IDictionary<string, GElemDTCommandInfo> syncDirectMethods,
            IDictionary<string, GElemDTCommandInfo> asyncDirectMethods
            )
        {
            Indent = "";
            if (string.IsNullOrEmpty(IndentUnit))
            {
                IndentUnit = "    ";
            }

            string d2cDTNameForDesirialize = D2CDataTypeName;
            string dpDTNameForDesirialize = DPDataTypeName;
            string rpDTNameForDesirialize = RPDataTypeName;
            if (projectExeType == ExeType.Edge)
            {
                d2cDTNameForDesirialize = FWIoTDataTypeName;
                dpDTNameForDesirialize = FWIoTDataTypeName;
                rpDTNameForDesirialize = FWIoTDataTypeName;
            }

            var generator = new AppIoTData_cs(IndentUnit, Indent, NameSpace,
                D2CDataTypeName, d2cDTNameForDesirialize, DPDataTypeName, dpDTNameForDesirialize, RPDataTypeName, rpDTNameForDesirialize,
                telemetries, desiredProperties, reportedProperties, syncDirectMethods, asyncDirectMethods)
            { Version = currentVersion };
            var codeContent = generator.TransformText();
            await WriteToFileAsync(AppIoTData_cs_FileName, codeContent);
        }

        public async Task GenerateIIoTAppCS(IDictionary<string, GElemDTCommandInfo> syncDirectMethods, IDictionary<string, GElemDTCommandInfo> asyncDirectMethods)
        {
            var generator = new IIoTApp_cs(NameSpace, syncDirectMethods, asyncDirectMethods) { Version = currentVersion };
            var codeContent = generator.TransformText();
            await WriteToFileAsync(IIoTApp_cs_FileName, codeContent);
        }

        public async Task GenerateIoTAppCS(string factoryCreationMethodName)
        {
            var generator = new IoTApp_cs(NameSpace, AppConnectorName, factoryCreationMethodName) { Version = currentVersion };
            var codeContent = generator.TransformText();
            await WriteToFileAsync(IoTApp_cs_FileName, codeContent);
        }

        public async Task GenerateIoTAppCodeCS(IDictionary<string, GElemDTCommandInfo> syncDirectMethods, IDictionary<string, GElemDTCommandInfo> asyncDirectMethods)
        {
            var generator = new IoTAppCode_cs(NameSpace, syncDirectMethods, asyncDirectMethods) { Version = currentVersion };
            var codeContent = generator.TransformText();
            await WriteToFileAsync(IoTApp_code_cs_FileName, codeContent, false);
        }

        public async Task GenerateIoTAppConnectorCS(IDictionary<string, GElemDTCommandInfo> syncDirectMethods, IDictionary<string, GElemDTCommandInfo> asyncDirectMethods)
        {
            var generator = new MyAppConnector_cs(NameSpace, AppConnectorName, syncDirectMethods, asyncDirectMethods) { Version = currentVersion };
            var codeContent = generator.TransformText();
            await WriteToFileAsync($"{AppConnectorName}.cs", codeContent);
        }

        public async Task GenerateProgramCS()
        {
            //var generator = new ProgramDeviceApp_cs(NameSpace) { Version = currentVersion };
            //var codeContent = generator.TransformText();
            var codeContent = GenerateProgramCSContent();
            await WriteToFileAsync(Program_cs_FileName, codeContent);
        }

        protected async Task WriteToFileAsync(string fileName, string content, bool overwrite = true)
        {
            string fileAbsolutePath = Path.Join(ProjFolderPath, fileName);
            bool isUpdate = true;
            if (overwrite == false)
            {
                if (File.Exists(fileAbsolutePath))
                {
                    isUpdate = false;
                }
            }
            if (isUpdate)
            {
                using (var writer = new StreamWriter(fileAbsolutePath))
                {
                    await writer.WriteAsync(content);
                    await writer.FlushAsync();
                }
                logger?.LogInfo($"generated {fileName}");
            }
        }

        public static string GetDataTypeProgramName(DTPrimitiveSchemaInfo dataSchema)
        {
            string typeName = "unknown";
            if (dataSchema is DTStringInfo)
            {
                typeName = "string";
            }
            else if (dataSchema is DTBooleanInfo)
            {
                typeName = "bool";
            }
            else if (dataSchema is DTIntegerInfo)
            {
                typeName = "int";
            }
            else if (dataSchema is DTLongInfo)
            {
                typeName = "long";
            }
            else if (dataSchema is DTFloatInfo)
            {
                typeName = "float";
            }
            else if (dataSchema is DTDoubleInfo)
            {
                typeName = "double";
            }
            else if (dataSchema is DTDateInfo || dataSchema is DTTimeInfo || dataSchema is DTDateTimeInfo)
            {
                typeName = "DateTime";
            }
            else if (dataSchema is DTDurationInfo)
            {
                typeName = "TimeSpan";
            }
            return typeName;
        }

        protected abstract Task GenerateSpecificCode();

        public static string GetMethodName(DTCommandInfo commandInfo)
        {
            string name = commandInfo.Name;
            return name.Substring(0, 1).ToUpper() + name.Substring(1);
        }

    }
}
