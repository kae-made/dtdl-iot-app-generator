// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Kae.IoT.PnP.Generator.Csharp;
using Microsoft.Azure.DigitalTwins.Parser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kae.IoT.PnP.Generator.Csharp.App.template;
using Kae.Utility.Logging;

namespace Kae.IoT.PnP.Generator
{
    public class IoTPnPCodeGenerater: IVersionedGenerator
    {
        private List<string> modelJson;
        private Dictionary<string, GElemDTInterfaceInfo> dtInterfaces;
        private Dictionary<string, GElemDTTelemetryInfo> dtTelemetries;
        private Dictionary<string, GElemDTPropertyInfo> dtDesiredProperties;
        private Dictionary<string, GElemDTPropertyInfo> dtReporedProperties;
        private Dictionary<string, GElemDTCommandInfo> dtSyncDirectMethods;
        private Dictionary<string, GElemDTCommandInfo> dtAsyncDirectMethods;
        private Dictionary<string, GElemDTComponentInfo> dtComponents;

        public IDictionary<string, GElemDTInterfaceInfo> DTInterfaces { get { return dtInterfaces; } }
        public IDictionary<string, GElemDTTelemetryInfo> DTTelemetries { get { return dtTelemetries; } }
        public IDictionary<string, GElemDTPropertyInfo> DTDesiredProperties { get { return dtDesiredProperties; } }
        public IDictionary<string, GElemDTPropertyInfo> DTReporetedProperties { get { return dtReporedProperties; } }
        public IDictionary<string, GElemDTCommandInfo> DTDirectMethods { get { return dtSyncDirectMethods; } }
        public IDictionary<string, GElemDTCommandInfo> DTC2DMessages { get { return dtAsyncDirectMethods; } }
        public IDictionary<string, GElemDTComponentInfo> DTComponents { get { return dtComponents; } }

        private string currentVersion = "0.0.1";
        public string Version { get; set; }

        protected Logger logger;

        public IoTPnPCodeGenerater()
        {
            Version = currentVersion;
            modelJson = new List<string>();
            dtInterfaces = new Dictionary<string, GElemDTInterfaceInfo>();
            dtTelemetries = new Dictionary<string, GElemDTTelemetryInfo>();
            dtDesiredProperties = new Dictionary<string, GElemDTPropertyInfo>();
            dtReporedProperties = new Dictionary<string, GElemDTPropertyInfo>();
            dtSyncDirectMethods = new Dictionary<string, GElemDTCommandInfo>();
            dtAsyncDirectMethods = new Dictionary<string, GElemDTCommandInfo>();
            dtComponents = new Dictionary<string, GElemDTComponentInfo>();
        }
        public IoTPnPCodeGenerater(Logger logger) : this()
        {
            this.logger = logger;
        }

        

        public async Task LoadModels(List<string> modelFiles, bool clearModels = false)
        {
            if (clearModels)
                modelJson.Clear();

            foreach (var modelFile in modelFiles)
            {
                await LoadModel(modelFile);
            }
        }

        public async Task LoadModel(string modelDef, bool clearModels = false)
        {
            if (clearModels)
                modelJson.Clear();

            modelJson.Add(modelDef);
        }
        public async Task<bool> Parse()
        {
            dtInterfaces.Clear();
            dtDesiredProperties.Clear();
            dtReporedProperties.Clear();
            dtTelemetries.Clear();
            dtSyncDirectMethods.Clear();
            dtAsyncDirectMethods.Clear();
            dtComponents.Clear();

            var parser = new ModelParser();
            bool result = true;
            try
            {
                var parseResult = await parser.ParseAsync(modelJson);
                PickupInterfaces(parseResult);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                result = false;
            }
            return result;
        }

        protected void PickupInterfaces(IReadOnlyDictionary<Dtmi, DTEntityInfo> parsedModel)
        {
            dtInterfaces.Clear();
            foreach (var item in parsedModel)
            {
                if (item.Value.EntityKind == DTEntityKind.Interface)
                {
                    dtInterfaces.Add(item.Key.AbsolutePath, new GElemDTInterfaceInfo() { Dtmi = item.Key, Info = (DTInterfaceInfo)item.Value });
                }
            }
        }


        protected void PickupTelemetries(GElemDTInterfaceInfo dtInterface)
        {
            dtTelemetries.Clear();
            var itemTelemetries = dtInterface.Info.Contents.Where(item => item.Value.EntityKind == DTEntityKind.Telemetry);
            foreach (var item in itemTelemetries)
            {
                dtTelemetries.Add(item.Key, new GElemDTTelemetryInfo() { Info = (DTTelemetryInfo)item.Value });
            }
        }

        protected void PickupProperties(GElemDTInterfaceInfo dtInterface)
        {
            dtDesiredProperties.Clear();
            dtReporedProperties.Clear();
            var itemDesiredProperties = dtInterface.Info.Contents.Where(item => item.Value.EntityKind == DTEntityKind.Property);
            foreach (var item in itemDesiredProperties)
            {
                var prop = (DTPropertyInfo)item.Value;
                if (prop.Writable)
                {
                    dtDesiredProperties.Add(item.Key, new GElemDTPropertyInfo() { Info = prop });
                }
                else
                {
                    dtReporedProperties.Add(item.Key, new GElemDTPropertyInfo() { Info = prop });
                }
            }
        }

        protected void PickupCommands(GElemDTInterfaceInfo dTInterface)
        {
            dtAsyncDirectMethods.Clear();
            dtSyncDirectMethods.Clear();
            var itemCommands = dTInterface.Info.Contents.Where(item => item.Value.EntityKind == DTEntityKind.Command);
            foreach (var item in itemCommands)
            {
                var command = (DTCommandInfo)item.Value;
                if (command.Response is null)
                {
                    dtAsyncDirectMethods.Add(item.Key, new GElemDTCommandInfo() { Info = command });
                }
                else
                {
                    dtSyncDirectMethods.Add(item.Key, new GElemDTCommandInfo() { Info = command });
                }
            }
        }

        protected void PickupComponent(IReadOnlyDictionary<Dtmi, DTEntityInfo> parsedModel)
        {
            dtComponents.Clear();
            foreach (var item in parsedModel)
            {
                if (item.Value.EntityKind == DTEntityKind.Component)
                {
                    dtComponents.Add(item.Key.AbsolutePath,new GElemDTComponentInfo() { Dtmi = item.Key, Info = (DTComponentInfo)item.Value });
                }
            }
        }

        public async Task GenerateProject(string interfaceName, string genFolderPath, string appName, string nameSpace, string exeType, string ioTFrameworkProjectPath)
        {
            try
            {
                GElemDTInterfaceInfo itemInterface = null;
                foreach (var item in dtInterfaces.Keys)
                {
                    if (item == interfaceName)
                    {
                        itemInterface = dtInterfaces[item];
                        break;
                    }
                }
                if (itemInterface != null)
                {
                    logger?.LogInfo("Picking telemetries...");
                    PickupTelemetries(itemInterface);
                    logger?.LogInfo("Picking properties...");
                    PickupProperties(itemInterface);
                    //Prototype();
                    logger?.LogInfo("Picking commands...");
                    PickupCommands(itemInterface);

                    var csharpGenerator = CSharpCodeGenerator.CreateGenerator(genFolderPath, interfaceName, appName, nameSpace, exeType, ioTFrameworkProjectPath, logger);
                    logger?.LogInfo("Generating project...");
                    await csharpGenerator.Generate(dtInterfaces, dtTelemetries, dtDesiredProperties, dtReporedProperties, dtSyncDirectMethods, dtAsyncDirectMethods);
                    logger?.LogInfo("Generate project done.");
                }
                else
                {
                    logger?.LogWarning($"{interfaceName} doesn't exist.");
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
            }
        }

        public void Prototype()
        {
            foreach(var p in dtReporedProperties.Values)
            {
                var schema = p.Info.Schema;
                if (schema is DTEnumInfo)
                {
                    var enumSchema = (DTEnumInfo)schema;
                    var valueSchema = enumSchema.ValueSchema;
                    foreach (var enumValue in enumSchema.EnumValues)
                    {
                        var name = enumValue.Name;
                        var value = enumValue.EnumValue;
                    }
                }
            }
        }
    }
}
