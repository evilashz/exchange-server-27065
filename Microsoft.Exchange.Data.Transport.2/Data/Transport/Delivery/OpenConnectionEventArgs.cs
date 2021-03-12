using System;

namespace Microsoft.Exchange.Data.Transport.Delivery
{
	// Token: 0x02000058 RID: 88
	public abstract class OpenConnectionEventArgs
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000203 RID: 515
		public abstract DeliverableMailItem DeliverableMailItem { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000204 RID: 516
		public abstract RoutingDomain NextHopDomain { get; }
	}
}
