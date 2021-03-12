using System;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x0200000F RID: 15
	internal enum Pop3CommandType
	{
		// Token: 0x04000064 RID: 100
		Quit,
		// Token: 0x04000065 RID: 101
		Stat,
		// Token: 0x04000066 RID: 102
		List,
		// Token: 0x04000067 RID: 103
		Retr,
		// Token: 0x04000068 RID: 104
		Dele,
		// Token: 0x04000069 RID: 105
		Noop,
		// Token: 0x0400006A RID: 106
		Rset,
		// Token: 0x0400006B RID: 107
		Top,
		// Token: 0x0400006C RID: 108
		Uidl,
		// Token: 0x0400006D RID: 109
		User,
		// Token: 0x0400006E RID: 110
		Pass,
		// Token: 0x0400006F RID: 111
		Auth,
		// Token: 0x04000070 RID: 112
		Blob,
		// Token: 0x04000071 RID: 113
		Capa,
		// Token: 0x04000072 RID: 114
		Stls
	}
}
