using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000476 RID: 1142
	[Cmdlet("Install", "DeliveryService")]
	[LocDescription(Strings.IDs.InstallMailboxTransportDeliveryServiceTask)]
	public sealed class InstallMailboxTransportDeliveryService : ManageMailboxTransportDeliveryService
	{
		// Token: 0x0600283F RID: 10303 RVA: 0x0009E9BA File Offset: 0x0009CBBA
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
