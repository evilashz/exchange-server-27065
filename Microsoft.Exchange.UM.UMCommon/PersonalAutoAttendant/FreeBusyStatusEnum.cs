using System;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x020000F5 RID: 245
	[Flags]
	internal enum FreeBusyStatusEnum : uint
	{
		// Token: 0x0400048B RID: 1163
		None = 0U,
		// Token: 0x0400048C RID: 1164
		Free = 1U,
		// Token: 0x0400048D RID: 1165
		Tentative = 2U,
		// Token: 0x0400048E RID: 1166
		Busy = 4U,
		// Token: 0x0400048F RID: 1167
		OutOfOffice = 8U,
		// Token: 0x04000490 RID: 1168
		NotAvailable = 268435456U
	}
}
