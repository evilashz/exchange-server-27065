using System;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000F0 RID: 240
	public enum OnDemandQueryType : byte
	{
		// Token: 0x040004F6 RID: 1270
		MTSummary = 1,
		// Token: 0x040004F7 RID: 1271
		MTDetail,
		// Token: 0x040004F8 RID: 1272
		DLP,
		// Token: 0x040004F9 RID: 1273
		Rule,
		// Token: 0x040004FA RID: 1274
		AntiSpam,
		// Token: 0x040004FB RID: 1275
		AntiVirus
	}
}
