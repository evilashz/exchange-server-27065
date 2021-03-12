using System;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000D2 RID: 210
	public enum EdgeBlockMode : byte
	{
		// Token: 0x0400043A RID: 1082
		None,
		// Token: 0x0400043B RID: 1083
		Reject,
		// Token: 0x0400043C RID: 1084
		PassThrough,
		// Token: 0x0400043D RID: 1085
		Test,
		// Token: 0x0400043E RID: 1086
		Grouping,
		// Token: 0x0400043F RID: 1087
		Disabled
	}
}
