using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics.Audit;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x020002F9 RID: 761
	[Cmdlet("Uninstall", "Audit")]
	public sealed class UnInstallAuditTask : Task
	{
		// Token: 0x06001A14 RID: 6676 RVA: 0x00074061 File Offset: 0x00072261
		protected override void InternalValidate()
		{
			base.InternalValidate();
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x00074069 File Offset: 0x00072269
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			EventSourceInstaller.UninstallSecurityEventSource("MSExchange Messaging Policies");
			TaskLogger.LogExit();
		}
	}
}
