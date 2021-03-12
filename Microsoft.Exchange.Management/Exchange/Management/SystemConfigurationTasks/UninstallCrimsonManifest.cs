using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200093A RID: 2362
	[Cmdlet("Uninstall", "CrimsonManifest")]
	public class UninstallCrimsonManifest : Task
	{
		// Token: 0x17001907 RID: 6407
		// (get) Token: 0x06005402 RID: 21506 RVA: 0x0015B03F File Offset: 0x0015923F
		// (set) Token: 0x06005403 RID: 21507 RVA: 0x0015B056 File Offset: 0x00159256
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

		// Token: 0x06005404 RID: 21508 RVA: 0x0015B06C File Offset: 0x0015926C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			string manifestName = Path.Combine(ConfigurationContext.Setup.InstallPath, this.DefinitionXml);
			try
			{
				ManageEventManifest.Uninstall(manifestName);
			}
			catch (InvalidOperationException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}
	}
}
