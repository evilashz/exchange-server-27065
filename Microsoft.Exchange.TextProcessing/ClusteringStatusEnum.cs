using System;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x0200001F RID: 31
	[Flags]
	internal enum ClusteringStatusEnum
	{
		// Token: 0x040000CB RID: 203
		None = 0,
		// Token: 0x040000CC RID: 204
		UkOneOrMultiSource = 1,
		// Token: 0x040000CD RID: 205
		OneSource = 2,
		// Token: 0x040000CE RID: 206
		MultiSource = 4,
		// Token: 0x040000CF RID: 207
		SourceMask = 7,
		// Token: 0x040000D0 RID: 208
		OneAndMultiSource = 8,
		// Token: 0x040000D1 RID: 209
		FpFeed = 1048576,
		// Token: 0x040000D2 RID: 210
		ThirdPartyFeed = 16777216,
		// Token: 0x040000D3 RID: 211
		HoneypotFeed = 33554432,
		// Token: 0x040000D4 RID: 212
		FnFeed = 67108864,
		// Token: 0x040000D5 RID: 213
		SenFeed = 134217728,
		// Token: 0x040000D6 RID: 214
		SewrFeed = 268435456,
		// Token: 0x040000D7 RID: 215
		SpamFeedMask = 520093696,
		// Token: 0x040000D8 RID: 216
		SpamVerdict = 536870912
	}
}
