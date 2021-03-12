using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.ActiveMonitoring
{
	// Token: 0x02000003 RID: 3
	public class ExchangeRpcConfiguration : RpcConfiguration
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000344D File Offset: 0x0000164D
		public override ILamRpc LamRpc
		{
			get
			{
				return new LamRpc();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00003454 File Offset: 0x00001654
		public override IThrottleHelper ThrottleHelper
		{
			get
			{
				return new ThrottleHelper();
			}
		}
	}
}
