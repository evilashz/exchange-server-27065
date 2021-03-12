using System;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x0200004E RID: 78
	public abstract class RpcConfiguration
	{
		// Token: 0x0600033E RID: 830 RVA: 0x0000B7BE File Offset: 0x000099BE
		public RpcConfiguration()
		{
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600033F RID: 831
		public abstract ILamRpc LamRpc { get; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000340 RID: 832
		public abstract IThrottleHelper ThrottleHelper { get; }

		// Token: 0x06000341 RID: 833 RVA: 0x0000B7C6 File Offset: 0x000099C6
		public void Initialize()
		{
			Dependencies.RegisterInterfaces(this.LamRpc, this.ThrottleHelper);
		}
	}
}
