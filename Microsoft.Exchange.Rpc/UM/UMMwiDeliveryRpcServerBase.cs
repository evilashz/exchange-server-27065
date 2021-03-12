using System;

namespace Microsoft.Exchange.Rpc.UM
{
	// Token: 0x02000408 RID: 1032
	internal abstract class UMMwiDeliveryRpcServerBase : RpcServerBase
	{
		// Token: 0x0600119B RID: 4507
		public abstract int SendMwiMessage(Guid mailboxGuid, Guid dialPlanGuid, string userExtension, string userName, int unreadVoicemailCount, int totalVoicemailCount, int assistantLatencyMsec, Guid tenantGuid);

		// Token: 0x0600119C RID: 4508 RVA: 0x00058934 File Offset: 0x00057D34
		public UMMwiDeliveryRpcServerBase()
		{
		}

		// Token: 0x0400103B RID: 4155
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.IUMMwiDelivery_v2_0_s_ifspec;
	}
}
