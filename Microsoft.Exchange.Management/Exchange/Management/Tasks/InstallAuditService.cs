using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200009C RID: 156
	[Cmdlet("Install", "AuditService")]
	public class InstallAuditService : ManageAuditService
	{
		// Token: 0x0600051B RID: 1307 RVA: 0x0001586B File Offset: 0x00013A6B
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
