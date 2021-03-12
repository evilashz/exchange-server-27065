using System;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A31 RID: 2609
	[Flags]
	internal enum DenyRuleAuthenticationType : short
	{
		// Token: 0x040030C4 RID: 12484
		Anonymous = 1,
		// Token: 0x040030C5 RID: 12485
		Basic = 2,
		// Token: 0x040030C6 RID: 12486
		Digest = 4,
		// Token: 0x040030C7 RID: 12487
		Forms = 8,
		// Token: 0x040030C8 RID: 12488
		Windows = 16
	}
}
