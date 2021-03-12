using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200039E RID: 926
	[Flags]
	public enum DatapointConsumer
	{
		// Token: 0x040010AF RID: 4271
		None = 0,
		// Token: 0x040010B0 RID: 4272
		Analytics = 1,
		// Token: 0x040010B1 RID: 4273
		Inference = 2,
		// Token: 0x040010B2 RID: 4274
		Diagnostics = 4,
		// Token: 0x040010B3 RID: 4275
		Watson = 8
	}
}
