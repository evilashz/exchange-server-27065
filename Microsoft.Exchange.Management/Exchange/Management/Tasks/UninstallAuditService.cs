using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200009D RID: 157
	[Cmdlet("Uninstall", "AuditService")]
	public class UninstallAuditService : ManageAuditService
	{
		// Token: 0x0600051D RID: 1309 RVA: 0x00015885 File Offset: 0x00013A85
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
