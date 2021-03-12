using System;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C6F RID: 3183
	internal enum BufferType
	{
		// Token: 0x04003ADF RID: 15071
		Empty,
		// Token: 0x04003AE0 RID: 15072
		Data,
		// Token: 0x04003AE1 RID: 15073
		Token,
		// Token: 0x04003AE2 RID: 15074
		Parameters,
		// Token: 0x04003AE3 RID: 15075
		Missing,
		// Token: 0x04003AE4 RID: 15076
		Extra,
		// Token: 0x04003AE5 RID: 15077
		Trailer,
		// Token: 0x04003AE6 RID: 15078
		Header,
		// Token: 0x04003AE7 RID: 15079
		Padding = 9,
		// Token: 0x04003AE8 RID: 15080
		Stream,
		// Token: 0x04003AE9 RID: 15081
		ChannelBindings = 14,
		// Token: 0x04003AEA RID: 15082
		ReadOnlyFlag = -2147483648,
		// Token: 0x04003AEB RID: 15083
		ReadOnlyWithChecksum = 268435456
	}
}
