using System;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000018 RID: 24
	[Flags]
	internal enum MessageRights
	{
		// Token: 0x040000A4 RID: 164
		ReadAny = 1,
		// Token: 0x040000A5 RID: 165
		Create = 2,
		// Token: 0x040000A6 RID: 166
		EditOwned = 8,
		// Token: 0x040000A7 RID: 167
		DeleteOwned = 16,
		// Token: 0x040000A8 RID: 168
		EditAny = 32,
		// Token: 0x040000A9 RID: 169
		DeleteAny = 64,
		// Token: 0x040000AA RID: 170
		CreateSubfolder = 128,
		// Token: 0x040000AB RID: 171
		Owner = 256,
		// Token: 0x040000AC RID: 172
		Contact = 512,
		// Token: 0x040000AD RID: 173
		Visible = 1024,
		// Token: 0x040000AE RID: 174
		None = 0,
		// Token: 0x040000AF RID: 175
		ReadOnly = 1,
		// Token: 0x040000B0 RID: 176
		ReadWrite = 33,
		// Token: 0x040000B1 RID: 177
		All = 1531
	}
}
