using System;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001EC RID: 492
	[Cmdlet("Install", "ExchangeSchema")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class InstallExchangeSchema : Task
	{
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x0004A340 File Offset: 0x00048540
		// (set) Token: 0x060010CC RID: 4300 RVA: 0x0004A357 File Offset: 0x00048557
		[Parameter(Mandatory = true)]
		public string LdapFileName
		{
			get
			{
				return base.Fields["LdapFileName"] as string;
			}
			set
			{
				base.Fields["LdapFileName"] = value;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x0004A36A File Offset: 0x0004856A
		// (set) Token: 0x060010CE RID: 4302 RVA: 0x0004A381 File Offset: 0x00048581
		[Parameter(Mandatory = false)]
		public string MacroName
		{
			get
			{
				return base.Fields["MacroName"] as string;
			}
			set
			{
				base.Fields["MacroName"] = value;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x0004A394 File Offset: 0x00048594
		// (set) Token: 0x060010D0 RID: 4304 RVA: 0x0004A3AB File Offset: 0x000485AB
		[Parameter(Mandatory = false)]
		public string MacroValue
		{
			get
			{
				return base.Fields["MacroValue"] as string;
			}
			set
			{
				base.Fields["MacroValue"] = value;
			}
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0004A3C0 File Offset: 0x000485C0
		protected override void InternalBeginProcessing()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 99, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\InstallExchangeSchema.cs");
			try
			{
				this.schemaMasterDC = topologyConfigurationSession.GetSchemaMasterDC();
			}
			catch (SchemaMasterDCNotFoundException exception)
			{
				base.ThrowTerminatingError(exception, ErrorCategory.ResourceUnavailable, null);
			}
			topologyConfigurationSession.DomainController = this.schemaMasterDC;
			this.MacroName = "<SchemaContainerDN>";
			this.MacroValue = topologyConfigurationSession.SchemaNamingContext.DistinguishedName;
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0004A440 File Offset: 0x00048640
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.HasErrors)
			{
				this.ldapFilePath = Path.Combine(ConfigurationContext.Setup.InstallPath, this.LdapFileName);
				if (!File.Exists(this.ldapFilePath))
				{
					base.WriteError(new FileNotFoundException(this.ldapFilePath), ErrorCategory.InvalidData, null);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0004A49B File Offset: 0x0004869B
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.ImportSchemaFile(this.schemaMasterDC, this.ldapFilePath, this.MacroName, this.MacroValue, new WriteVerboseDelegate(base.WriteVerbose));
			TaskLogger.LogExit();
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0004A4D4 File Offset: 0x000486D4
		private void ImportSchemaFile(string schemaMasterServer, string schemaFilePath, string macroName, string macroValue, WriteVerboseDelegate writeVerbose)
		{
			TaskLogger.LogEnter();
			string fileName = Path.Combine(Environment.SystemDirectory, "ldifde.exe");
			string text = Path.GetTempPath();
			if (text[text.Length - 1] == '\\')
			{
				text = text.Substring(0, text.Length - 1);
			}
			string arguments = string.Format("-i -s \"{0}\" -f \"{1}\" -j \"{2}\" -c \"{3}\" \"{4}\"", new object[]
			{
				schemaMasterServer,
				schemaFilePath,
				text,
				macroName,
				macroValue.Replace("\"", "\\\\\"")
			});
			int num = InstallExchangeSchema.RunProcess(fileName, arguments, writeVerbose);
			if (num != 0)
			{
				base.WriteError(new TaskException(Strings.SchemaImportProcessFailure(schemaFilePath, "ldifde.exe", num, Path.Combine(text, "ldif.err"))), ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0004A594 File Offset: 0x00048794
		private static int RunProcess(string fileName, string arguments, WriteVerboseDelegate writeVerbose)
		{
			TaskLogger.LogEnter();
			writeVerbose(Strings.LogRunningCommand(fileName, arguments));
			int exitCode;
			using (Process process = Process.Start(new ProcessStartInfo
			{
				FileName = fileName,
				Arguments = arguments,
				CreateNoWindow = true,
				WindowStyle = ProcessWindowStyle.Hidden,
				UseShellExecute = false
			}))
			{
				process.WaitForExit();
				writeVerbose(Strings.LogProcessExitCode(fileName, process.ExitCode));
				TaskLogger.LogExit();
				exitCode = process.ExitCode;
			}
			return exitCode;
		}

		// Token: 0x04000783 RID: 1923
		private const string MacroNameParamDefaultValue = "<SchemaContainerDN>";

		// Token: 0x04000784 RID: 1924
		private const string SchemaImportExportExeFileName = "ldifde.exe";

		// Token: 0x04000785 RID: 1925
		private const string SchemaImportExportLogFileName = "ldif.log";

		// Token: 0x04000786 RID: 1926
		private const string SchemaImportExportErrorFileName = "ldif.err";

		// Token: 0x04000787 RID: 1927
		private string schemaMasterDC;

		// Token: 0x04000788 RID: 1928
		private string ldapFilePath;
	}
}
