using System;
using Microsoft.Exchange.Data.HA;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000322 RID: 802
	internal class StoreRpcControllerNoTimeout : StoreRpcController
	{
		// Token: 0x060020FA RID: 8442 RVA: 0x00097E0B File Offset: 0x0009600B
		public StoreRpcControllerNoTimeout(string serverNameOrFqdn) : base(serverNameOrFqdn, null)
		{
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x060020FB RID: 8443 RVA: 0x00097E15 File Offset: 0x00096015
		public override TimeSpan ConnectivityTimeout
		{
			get
			{
				return InvokeWithTimeout.InfiniteTimeSpan;
			}
		}
	}
}
