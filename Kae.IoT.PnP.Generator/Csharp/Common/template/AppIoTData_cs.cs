// ------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン: 16.0.0.0
//  
//     このファイルへの変更は、正しくない動作の原因になる可能性があり、
//     コードが再生成されると失われます。
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Kae.IoT.PnP.Generator.Csharp.Common.template
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.Azure.DigitalTwins.Parser;
    using Kae.IoT.PnP.Generator.Csharp;
    using Kae.IoT.PnP.Generator;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class AppIoTData_cs : AppIoTData_csBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            
            #line 1 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"

  // Copyright (c) Knowledge & Experience. All rights reserved.
  // Licensed under the MIT license. See LICENSE file in the project root for full license information.

            
            #line default
            #line hidden
            this.Write("// ------------------------------------------------------------------------------" +
                    "\r\n// <auto-generated>\r\n//     このコードはツールによって生成されました。\r\n//     ランタイム バージョン: ");
            
            #line 16 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Version));
            
            #line default
            #line hidden
            this.Write(@"
//  
//     このファイルへの変更は、正しくない動作の原因になる可能性があり、
//     コードが再生成されると失われます。
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ");
            
            #line 29 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(nameSpace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    class ");
            
            #line 31 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(d2cDataTypeName));
            
            #line default
            #line hidden
            this.Write(" : Kae.IoT.Framework.IoTDataWithProperties\r\n    {\r\n");
            
            #line 33 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
 
    indent = "        ";
    foreach (var telemetryInfo in dtTelemetries.Values) {
        var telemetry = telemetryInfo.Info;
        var fieldDecl = CreateFieldDecl(indentUnit, indent, telemetry.Name, telemetry.Schema);
        var fieldDeclContent = fieldDecl.TransformText(); 
            
            #line default
            #line hidden
            
            #line 39 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldDeclContent));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 40 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
  }

            
            #line default
            #line hidden
            this.Write("\r\n        public override ");
            
            #line 43 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(d2cDataTypeNameForDeserialize));
            
            #line default
            #line hidden
            this.Write(@" Deserialize(string json)
        {
            return (D2CData) Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(D2CData));
        }

        public override string Serialize()
        {
            var content = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            return content;
        }
    }

    class ");
            
            #line 55 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(dpDataTypeName));
            
            #line default
            #line hidden
            this.Write(" : Kae.IoT.Framework.IoTData\r\n    {\r\n");
            
            #line 57 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"

    foreach (var propInfo in dtDesiredProperties.Values) {
        var prop = propInfo.Info;
        var fieldDecl = CreateFieldDecl(indentUnit, indent, prop.Name, prop.Schema);
        var fieldDeclContent = fieldDecl.TransformText(); 
            
            #line default
            #line hidden
            
            #line 62 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldDeclContent));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 63 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
  }

            
            #line default
            #line hidden
            this.Write("\r\n        public override ");
            
            #line 66 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(dpDataTypeNameForDeserialize));
            
            #line default
            #line hidden
            this.Write(@" Deserialize(string json)
        {
            return (AppDTDesiredProperties) Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(AppDTDesiredProperties));
        }

        public override string Serialize()
        {
            var content = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            return content;
        }
    }

    class ");
            
            #line 78 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rpDataTypeName));
            
            #line default
            #line hidden
            this.Write(" : Kae.IoT.Framework.IoTData\r\n    {\r\n");
            
            #line 80 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"

    foreach (var propInfo in dtReporedProperties.Values) {
        var prop = propInfo.Info;
        var fieldDecl = CreateFieldDecl(indentUnit, indent, prop.Name, prop.Schema);
        var fieldDeclContent = fieldDecl.TransformText(); 
            
            #line default
            #line hidden
            
            #line 85 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(fieldDeclContent));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 86 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
  }

            
            #line default
            #line hidden
            this.Write("\r\n        public override ");
            
            #line 89 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rpDataTypeNameForDeserialize));
            
            #line default
            #line hidden
            this.Write(@" Deserialize(string json)
        {
            return (AppDTReporetedProperties) Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(AppDTReporetedProperties));
        }

        public override string Serialize()
        {
            var content = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            return content;
        }
    }

");
            
            #line 101 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"

    foreach (var dmsyncInfo in dtSyncDirectMethods.Values)
    {
        var dmsync = dmsyncInfo.Info;
        var parentTypeName = $"Command_{dmsync.Name}";
        var requestPayload = dmsync.Request;
        var responsePayload = dmsync.Response;
        string genRequest = null;
        string genResponse = null;
        if (requestPayload is not null){
            string typeName = $"{GetMethodName(dmsync)}_Request_${requestPayload.Name}";
            genRequest = TransformCommandPayload(requestPayload, typeName);
        }
        if (responsePayload is not null)
        {
            string typeName = $"{GetMethodName(dmsync)}_Response_${responsePayload.Name}";
            genResponse = TransformCommandPayload(requestPayload, typeName);
        }
        if ((!string.IsNullOrEmpty(genRequest)) || (!string.IsNullOrEmpty(genResponse)))
        { 
            
            #line default
            #line hidden
            this.Write("public class ");
            
            #line 121 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(parentTypeName));
            
            #line default
            #line hidden
            this.Write(" {\r\n");
            
            #line 122 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
          if (!string.IsNullOrEmpty(genRequest))
            { 
            
            #line default
            #line hidden
            
            #line 124 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(genRequest));
            
            #line default
            #line hidden
            this.Write("          \r\n");
            
            #line 125 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
          }
            if (!string.IsNullOrEmpty(genResponse))
            { 
            
            #line default
            #line hidden
            
            #line 128 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(genResponse));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 129 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
          } 
            
            #line default
            #line hidden
            this.Write("}\r\n");
            
            #line 131 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\AppIoTData_cs.tt"
      }
    }

            
            #line default
            #line hidden
            this.Write("}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class AppIoTData_csBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
