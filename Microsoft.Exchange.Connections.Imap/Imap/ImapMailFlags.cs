using System;

namespace Microsoft.Exchange.Connections.Imap
{
	// Token: 0x0200000F RID: 15
	[Flags]
	public enum ImapMailFlags
	{
		// Token: 0x0400008D RID: 141
		None = 0,
		// Token: 0x0400008E RID: 142
		Answered = 1,
		// Token: 0x0400008F RID: 143
		Flagged = 2,
		// Token: 0x04000090 RID: 144
		Deleted = 4,
		// Token: 0x04000091 RID: 145
		Seen = 8,
		// Token: 0x04000092 RID: 146
		Draft = 16,
		// Token: 0x04000093 RID: 147
		All = 31
	}
}
