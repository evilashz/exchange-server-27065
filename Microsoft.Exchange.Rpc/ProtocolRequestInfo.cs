using System;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000012 RID: 18
	public abstract class ProtocolRequestInfo
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000607 RID: 1543
		public abstract string[] RequestIds { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000608 RID: 1544
		public abstract string[] Cookies { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000609 RID: 1545
		public abstract string ClientAddress { get; }

		// Token: 0x0600060A RID: 1546 RVA: 0x0000169C File Offset: 0x00000A9C
		public ProtocolRequestInfo()
		{
		}
	}
}
