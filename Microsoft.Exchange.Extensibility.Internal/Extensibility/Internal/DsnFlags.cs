using System;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x02000065 RID: 101
	[Flags]
	internal enum DsnFlags
	{
		// Token: 0x040003E9 RID: 1001
		None = 0,
		// Token: 0x040003EA RID: 1002
		Delivery = 1,
		// Token: 0x040003EB RID: 1003
		Delay = 2,
		// Token: 0x040003EC RID: 1004
		Failure = 4,
		// Token: 0x040003ED RID: 1005
		Relay = 8,
		// Token: 0x040003EE RID: 1006
		Expansion = 16,
		// Token: 0x040003EF RID: 1007
		Quarantine = 32,
		// Token: 0x040003F0 RID: 1008
		AllFlags = 63
	}
}
