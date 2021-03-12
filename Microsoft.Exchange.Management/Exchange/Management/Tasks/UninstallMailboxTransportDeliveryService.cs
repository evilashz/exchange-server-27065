using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000477 RID: 1143
	[Cmdlet("Uninstall", "DeliveryService")]
	[LocDescription(Strings.IDs.UninstallMailboxTransportDeliveryServiceTask)]
	public sealed class UninstallMailboxTransportDeliveryService : ManageMailboxTransportDeliveryService
	{
		// Token: 0x06002841 RID: 10305 RVA: 0x0009E9D4 File Offset: 0x0009CBD4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
