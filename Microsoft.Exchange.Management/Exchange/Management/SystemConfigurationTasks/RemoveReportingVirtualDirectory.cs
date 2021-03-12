using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C3E RID: 3134
	[Cmdlet("Remove", "ReportingVirtualDirectory", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveReportingVirtualDirectory : ManageReportingVirtualDirectory
	{
		// Token: 0x17002482 RID: 9346
		// (get) Token: 0x0600767E RID: 30334 RVA: 0x001E3B47 File Offset: 0x001E1D47
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveReportingVirtualDirectory;
			}
		}

		// Token: 0x0600767F RID: 30335 RVA: 0x001E3B50 File Offset: 0x001E1D50
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			DeleteVirtualDirectory deleteVirtualDirectory = new DeleteVirtualDirectory();
			deleteVirtualDirectory.Name = "Reporting";
			deleteVirtualDirectory.Parent = "IIS://localhost/W3SVC/1/Root";
			deleteVirtualDirectory.Initialize();
			deleteVirtualDirectory.Execute();
			TaskLogger.LogExit();
		}
	}
}
