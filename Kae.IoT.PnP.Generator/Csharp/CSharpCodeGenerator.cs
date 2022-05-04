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

        public string ProjFolderPath { get; set; }
        public string IndentUnit { get; set; }
        public string Indent { get; set; }
        public string Version { get; set; }

        protected static readonly string currentVersion = "0.0.1";
        protected string genTemplateFolderPath;

        protected ExeType projectExeType;

        public CSharpCodeGenerator(ExeType exeType)
        {
            Version = currentVersion;
            this.projectExeType = exeType;

            string codeBase = Assembly.GetExecutingAssembly().Location;
            var dirInfo = new DirectoryInfo(codeBase);
            genTemplateFolderPath = Path.Join(dirInfo.Parent.FullName, Path.Join(templateFolderPath));
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
            await GenerateIoTAppCS();
            await GenerateIoTAppCodeCS(dtSyncDirectMethods, dtAsyncDirectMethods);
            await GenerateIoTAppConnectorCS(dtSyncDirectMethods, dtAsyncDirectMethods);
            await GenerateProgramCS();
        }

        protected static readonly string [] templateFolderPath = new string[] { "Kae", "IoT", "PnP", "Generator", "Csharp", "DeviceApp", "template" };
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

        protected static readonly string D2CDataTypeName = "D2CData";
        protected static readonly string DPDataTypeName = "AppDTDesiredProperties";
        protected static readonly string RPDataTypeName = "AppDTReporetedProperties";
        protected static readonly string FWIoTDataTypeName = "IoTData";

        public static CSharpCodeGenerator CreateGenerator(string genFolderPath, string appName, string nameSpace, string exeTypeParam)
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
                    generator = new CsharpCodeGeneratorNonEdge(exeType) { GenFolderPath=genFolderPath, ProjectName=appName, NameSpace=nameSpace };
                    break;
                case ExeType.Edge:
                    generator = new CsharpCodeGeneratorEdge() { GenFolderPath = genFolderPath, ProjectName = appName, NameSpace = nameSpace };
                    break;
            }
            return generator;
        }

        protected abstract Task CreateProjectEnvironment();
        protected abstract string GenerateProgramCSContent();
        protected async Task CreateProjectEnvironmentCommon()
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

            var projFileGenerator = new ProjectFile(projectExeType, configFileName, UserSecretsIed);
            var content = projFileGenerator.TransformText();
            await WriteToFileAsync(csharpProjFileName, content);

            var configFileGenerator = new IoTAppConfig_yaml(projectExeType, ModelId);
            content = configFileGenerator.TransformText();
            await WriteToFileAsync(configFileName, content);

            ClassName = projName;
            AppConnectorName = $"{projName}AppConnector";
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
            if (projectExeType== ExeType.Edge)
            {
                d2cDTNameForDesirialize = FWIoTDataTypeName;
                dpDTNameForDesirialize = FWIoTDataTypeName;
                rpDTNameForDesirialize = FWIoTDataTypeName;
            }
            
            var generator = new AppIoTData_cs(IndentUnit, Indent, NameSpace,
                D2CDataTypeName,d2cDTNameForDesirialize,DPDataTypeName,dpDTNameForDesirialize,RPDataTypeName, rpDTNameForDesirialize,
                telemetries, desiredProperties, reportedProperties, syncDirectMethods, asyncDirectMethods) { Version = currentVersion };
            var codeContent = generator.TransformText();
            await WriteToFileAsync(AppIoTData_cs_FileName, codeContent);
        }

        public async Task GenerateIIoTAppCS(IDictionary<string, GElemDTCommandInfo> syncDirectMethods, IDictionary<string, GElemDTCommandInfo> asyncDirectMethods)
        {
            var generator = new IIoTApp_cs(NameSpace, syncDirectMethods, asyncDirectMethods) { Version = currentVersion };
            var codeContent = generator.TransformText();
            await WriteToFileAsync(IIoTApp_cs_FileName, codeContent);
        }

        public async Task GenerateIoTAppCS()
        {
            var generator = new IoTApp_cs(NameSpace, AppConnectorName) { Version = currentVersion };
            var codeContent = generator.TransformText();
            await WriteToFileAsync(IoTApp_cs_FileName, codeContent);
        }

        public async Task GenerateIoTAppCodeCS(IDictionary<string, GElemDTCommandInfo> syncDirectMethods, IDictionary<string, GElemDTCommandInfo> asyncDirectMethods)
        {
            var generator = new IoTAppCode_cs(NameSpace, syncDirectMethods, asyncDirectMethods) { Version = currentVersion };
            var codeContent = generator.TransformText();
            await WriteToFileAsync(IoTApp_code_cs_FileName, codeContent);
        }

        public async Task GenerateIoTAppConnectorCS(IDictionary<string, GElemDTCommandInfo> syncDirectMethods, IDictionary<string, GElemDTCommandInfo> asyncDirectMethods)
        {
            var generator = new MyAppConnector_cs(NameSpace, ClassName, syncDirectMethods, asyncDirectMethods) { Version = currentVersion };
            var codeContent = generator.TransformText();
            await WriteToFileAsync($"{AppConnectorName}.cs", codeContent);
        }

        public  async Task GenerateProgramCS()
        {
            //var generator = new ProgramDeviceApp_cs(NameSpace) { Version = currentVersion };
            //var codeContent = generator.TransformText();
            var codeContent = GenerateProgramCSContent();
            await WriteToFileAsync(Program_cs_FileName, codeContent);
        }

        protected async Task WriteToFileAsync(string fileName, string content)
        {
            string fileAbsolutePath = Path.Join(ProjFolderPath, fileName);
            using (var writer = new StreamWriter(fileAbsolutePath))
            {
                await writer.WriteAsync(content);
                await writer.FlushAsync();
            }
        }

        public static string GetDataTypeProgramName(DTPrimitiveSchemaInfo dataSchema)
        {
            string typeName = "unknown";
            if (dataSchema is DTStringInfo)
            {
                typeName = "string";
            } else if (dataSchema is DTBooleanInfo)
            {
                typeName = "bool";
            } else if (dataSchema is DTIntegerInfo)
            {
                typeName = "int";
            } else if (dataSchema is DTLongInfo)
            {
                typeName = "long";
            } else if (dataSchema is DTFloatInfo)
            {
                typeName = "float";
            }else if (dataSchema is DTDoubleInfo)
            {
                typeName = "double";
            }else if (dataSchema is DTDateInfo || dataSchema is DTTimeInfo ||dataSchema is DTDateTimeInfo)
            {
                typeName = "DateTime";
            }else if (dataSchema is DTDurationInfo)
            {
                typeName = "TimeSpan";
            }
            return typeName;
        }

        public static string GetMethodName(DTCommandInfo commandInfo)
        {
            string name = commandInfo.Name;
            return name.Substring(0, 1).ToUpper() + name.Substring(1);
        }
    }
}
