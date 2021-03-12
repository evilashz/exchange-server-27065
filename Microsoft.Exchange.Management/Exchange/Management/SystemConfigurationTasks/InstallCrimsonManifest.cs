using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000929 RID: 2345
	[Cmdlet("Install", "CrimsonManifest")]
	public class InstallCrimsonManifest : Task
	{
		// Token: 0x170018F5 RID: 6389
		// (get) Token: 0x060053AF RID: 21423 RVA: 0x00159C95 File Offset: 0x00157E95
		// (set) Token: 0x060053B0 RID: 21424 RVA: 0x00159CAC File Offset: 0x00157EAC
		[Parameter(Mandatory = true)]
		public string DefinitionXml
		{
			get
			{
				return (string)base.Fields["DefinitionXml"];
			}
			set
			{
				base.Fields["DefinitionXml"] = value;
			}
		}

		// Token: 0x170018F6 RID: 6390
		// (get) Token: 0x060053B1 RID: 21425 RVA: 0x00159CBF File Offset: 0x00157EBF
		// (set) Token: 0x060053B2 RID: 21426 RVA: 0x00159CD6 File Offset: 0x00157ED6
		[Parameter(Mandatory = true)]
		public string MessageDll
		{
			get
			{
				return (string)base.Fields["MessageDll"];
			}
			set
			{
				base.Fields["MessageDll"] = value;
			}
		}

		// Token: 0x170018F7 RID: 6391
		// (get) Token: 0x060053B3 RID: 21427 RVA: 0x00159CE9 File Offset: 0x00157EE9
		// (set) Token: 0x060053B4 RID: 21428 RVA: 0x00159D00 File Offset: 0x00157F00
		[Parameter(Mandatory = true)]
		public string ProviderName
		{
			get
			{
				return (string)base.Fields["ProviderName"];
			}
			set
			{
				base.Fields["ProviderName"] = value;
			}
		}

		// Token: 0x060053B5 RID: 21429 RVA: 0x00159D14 File Offset: 0x00157F14
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			string text = Path.Combine(ConfigurationContext.Setup.InstallPath, this.DefinitionXml);
			string text2 = Path.Combine(ConfigurationContext.Setup.InstallPath, this.MessageDll);
			try
			{
				if (ManageEventManifest.UpdateMessageDllPath(text, text2, this.ProviderName))
				{
					ManageEventManifest.Install(text);
				}
				else
				{
					base.WriteError(new InvalidOperationException(Strings.EventManifestNotUpdated(text, text2, this.ProviderName)), ErrorCategory.InvalidOperation, null);
				}
			}
			catch (InvalidOperationException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}
	}
}
