using System;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000017 RID: 23
	[Flags]
	internal enum Imap4Flags
	{
		// Token: 0x04000097 RID: 151
		None = 0,
		// Token: 0x04000098 RID: 152
		Recent = 1,
		// Token: 0x04000099 RID: 153
		Seen = 2,
		// Token: 0x0400009A RID: 154
		Deleted = 4,
		// Token: 0x0400009B RID: 155
		Answered = 8,
		// Token: 0x0400009C RID: 156
		Draft = 16,
		// Token: 0x0400009D RID: 157
		Flagged = 32,
		// Token: 0x0400009E RID: 158
		MdnSent = 64,
		// Token: 0x0400009F RID: 159
		Wildcard = 128,
		// Token: 0x040000A0 RID: 160
		MimeFailed = 256,
		// Token: 0x040000A1 RID: 161
		ItemStatus = 108,
		// Token: 0x040000A2 RID: 162
		ItemFlags = 16
	}
}
