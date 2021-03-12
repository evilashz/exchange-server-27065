using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x020002F2 RID: 754
	[LocDescription(Strings.IDs.InstallAdamSchemaTask)]
	[Cmdlet("Install", "AdamSchema")]
	public sealed class InstallAdamSchemaTask : Task
	{
		// Token: 0x060019D8 RID: 6616 RVA: 0x00073074 File Offset: 0x00071274
		public InstallAdamSchemaTask()
		{
			this.InstanceName = "MSExchange";
			this.MacroName = "<SchemaContainerDN>";
			this.MacroValue = "#schemaNamingContext";
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x0007309D File Offset: 0x0007129D
		// (set) Token: 0x060019DA RID: 6618 RVA: 0x000730B4 File Offset: 0x000712B4
		[Parameter(Mandatory = false)]
		public string InstanceName
		{
			get
			{
				return (string)base.Fields["InstanceName"];
			}
			set
			{
				base.Fields["InstanceName"] = value;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x000730C7 File Offset: 0x000712C7
		// (set) Token: 0x060019DC RID: 6620 RVA: 0x000730DE File Offset: 0x000712DE
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

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x000730F1 File Offset: 0x000712F1
		// (set) Token: 0x060019DE RID: 6622 RVA: 0x00073108 File Offset: 0x00071308
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

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x060019DF RID: 6623 RVA: 0x0007311B File Offset: 0x0007131B
		// (set) Token: 0x060019E0 RID: 6624 RVA: 0x00073132 File Offset: 0x00071332
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

		// Token: 0x060019E1 RID: 6625 RVA: 0x00073148 File Offset: 0x00071348
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (!AdamServiceSettings.GetSettingsExist(this.InstanceName))
			{
				base.WriteError(new InvalidAdamInstanceNameException(this.InstanceName), ErrorCategory.InvalidArgument, null);
			}
			if (!File.Exists(this.LdapFileName))
			{
				base.WriteError(new InvalidLdapFileNameException(), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00073198 File Offset: 0x00071398
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.InstanceName
			});
			try
			{
				ManageAdamService.ImportAdamSchema(this.InstanceName, this.LdapFileName, this.MacroName, this.MacroValue);
			}
			catch (AdamSchemaImportProcessFailureException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04000B38 RID: 2872
		public const string InstanceParamName = "InstanceName";

		// Token: 0x04000B39 RID: 2873
		public const string LdapSchemaFileNameParamName = "LdapFileName";

		// Token: 0x04000B3A RID: 2874
		public const string MacroNameParamName = "MacroName";

		// Token: 0x04000B3B RID: 2875
		public const string MacroValueParamName = "MacroValue";

		// Token: 0x04000B3C RID: 2876
		public const string MacroNameParamDefaultValue = "<SchemaContainerDN>";

		// Token: 0x04000B3D RID: 2877
		public const string PredefSchemaNamingContextMacro = "#schemaNamingContext";

		// Token: 0x04000B3E RID: 2878
		public const string PredefConfigNamingContextMacro = "#configurationNamingContext";

		// Token: 0x04000B3F RID: 2879
		public static readonly string AdamSchemaImportCumulativeLogFilePath = ManageAdamService.AdamSchemaImportCumulativeLogFilePath;

		// Token: 0x04000B40 RID: 2880
		public static readonly string AdamSchemaImportCumulativeErrorFilePath = ManageAdamService.AdamSchemaImportCumulativeErrorFilePath;
	}
}
