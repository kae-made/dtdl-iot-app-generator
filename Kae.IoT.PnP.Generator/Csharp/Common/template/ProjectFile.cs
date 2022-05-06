﻿// ------------------------------------------------------------------------------
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
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\ProjectFile.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class ProjectFile : ProjectFileBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            
            #line 1 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\ProjectFile.tt"

  // Copyright (c) Knowledge & Experience. All rights reserved.
  // Licensed under the MIT license. See LICENSE file in the project root for full license information.

            
            #line default
            #line hidden
            this.Write("<Project Sdk=\"");
            
            #line 10 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\ProjectFile.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sdkName));
            
            #line default
            #line hidden
            this.Write("\">\r\n\r\n  <PropertyGroup>\r\n");
            
            #line 13 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\ProjectFile.tt"

            if (!string.IsNullOrEmpty(outputType))
            { 
            
            #line default
            #line hidden
            this.Write("    <OutputType>");
            
            #line 16 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\ProjectFile.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(outputType));
            
            #line default
            #line hidden
            this.Write("</OutputType>\r\n");
            
            #line 17 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\ProjectFile.tt"
          }

            
            #line default
            #line hidden
            this.Write("    <TargetFramework>");
            
            #line 19 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\ProjectFile.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(targetFramework));
            
            #line default
            #line hidden
            this.Write("</TargetFramework>\r\n");
            
            #line 20 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\ProjectFile.tt"

            if (!string.IsNullOrEmpty(userSecretsId))
            { 
            
            #line default
            #line hidden
            this.Write("    <UserSecretsId>");
            
            #line 23 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\ProjectFile.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(userSecretsId));
            
            #line default
            #line hidden
            this.Write("</UserSecretsId>\r\n");
            
            #line 24 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\ProjectFile.tt"
          }

            
            #line default
            #line hidden
            this.Write("  </PropertyGroup>\r\n\r\n");
            
            #line 28 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\ProjectFile.tt"

            if (IsEdge())
            { 
            
            #line default
            #line hidden
            this.Write(@"  
  <PropertyGroup Condition=""'$(Configuration)|$(TargetFramework)|$(Platform)'=='Debug|netcoreapp3.1|AnyCPU'"">
        <TreatSpecificWarningsAsErrors />
  </PropertyGroup>

  <ItemGroup>
    <ProjectCapability Include=""AzureIoTEdgeModule"" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include=""Microsoft.Azure.Devices.Client"" Version=""1.40.0"" />
    <PackageReference Include=""Newtonsoft.Json"" Version=""13.0.1"" />
    <PackageReference Include=""Microsoft.Extensions.Configuration"" Version=""2.1.1"" />
    <PackageReference Include=""Microsoft.Extensions.Configuration.Abstractions"" Version=""2.1.1"" />
    <PackageReference Include=""Microsoft.Extensions.Configuration.Binder"" Version=""2.1.1"" />
    <PackageReference Include=""Microsoft.Extensions.Configuration.EnvironmentVariables"" Version=""2.1.1"" />
    <PackageReference Include=""Microsoft.Extensions.Configuration.FileExtensions"" Version=""2.1.1"" />
    <PackageReference Include=""Microsoft.Extensions.Configuration.Json"" Version=""2.0.0"" />
    <PackageReference Include=""System.Runtime.Loader"" Version=""4.3.0"" />
    <PackageReference Include=""YamlDotNet"" Version=""11.2.1"" />
  </ItemGroup>

");
            
            #line 52 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\ProjectFile.tt"
          }
            if (IsDeviceApp())
            { 
            
            #line default
            #line hidden
            this.Write("  <ItemGroup>\r\n    <PackageReference Include=\"Microsoft.Azure.Devices.Client\" Ver" +
                    "sion=\"1.40.0\" />\r\n    <PackageReference Include=\"Newtonsoft.Json\" Version=\"13.0." +
                    "1\" />\r\n    <PackageReference Include=\"YamlDotNet\" Version=\"11.2.1\" />\r\n  </ItemG" +
                    "roup>\r\n\r\n");
            
            #line 61 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\ProjectFile.tt"
          }
            if (IsService())
            { 
            
            #line default
            #line hidden
            this.Write(@"  <ItemGroup>
    <PackageReference Include=""Microsoft.Azure.Devices.Client"" Version=""1.40.0"" />
    <PackageReference Include=""Newtonsoft.Json"" Version=""13.0.1"" />
    <PackageReference Include=""Microsoft.Extensions.Hosting"" Version=""5.0.0"" />
    <PackageReference Include=""Microsoft.Extensions.Hosting.WindowsServices"" Version=""5.0.1"" />
    <PackageReference Include=""System.Diagnostics.PerformanceCounter"" Version=""6.0.1"" />
    <PackageReference Include=""YamlDotNet"" Version=""11.2.1"" />
  </ItemGroup>

");
            
            #line 73 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\ProjectFile.tt"
          }
            if (IsDeviceApp() || IsService())
            { 
            
            #line default
            #line hidden
            this.Write("  <ItemGroup>\r\n    <Reference Include=\"Kae.IoT.Framework\">\r\n      <HintPath>");
            
            #line 78 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\ProjectFile.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(iotFrameworkProjectPath));
            
            #line default
            #line hidden
            this.Write("\\out\\Kae.IoT.Framework.dll</HintPath>\r\n    </Reference>\r\n    <Reference Include=\"" +
                    "Kae.Utility.Logging\">\r\n      <HintPath>");
            
            #line 81 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\ProjectFile.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(iotFrameworkProjectPath));
            
            #line default
            #line hidden
            this.Write("\\out\\Kae.Utility.Logging.dll</HintPath>\r\n    </Reference>\r\n  </ItemGroup>\r\n\r\n  <I" +
                    "temGroup>\r\n    <None Update=\"");
            
            #line 86 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\ProjectFile.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(configFileName));
            
            #line default
            #line hidden
            this.Write("\">\r\n      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>\r\n    </No" +
                    "ne>\r\n  </ItemGroup>\r\n");
            
            #line 90 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\ProjectFile.tt"
            }

            
            #line default
            #line hidden
            
            #line 92 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\ProjectFile.tt"

            if (IsEdge())
            { 
            
            #line default
            #line hidden
            this.Write(@"  <ItemGroup>
    <Reference Include=""Kae.IoT.Framework"">
      <HintPath>Kae.IoT.Framework.dll</HintPath>
    </Reference>
    <Reference Include=""Kae.Utility.Logging"">
      <HintPath>Kae.Utility.Logging.dll</HintPath>
    </Reference>
  </ItemGroup>
");
            
            #line 103 "C:\Users\kae-m\source\repos\DTDLIoTPnPIoTAppGeneratorEnv\Kae.IoT.PnP.Generator\Csharp\Common\template\ProjectFile.tt"
          }

            
            #line default
            #line hidden
            this.Write("\r\n</Project>");
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
    public class ProjectFileBase
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
