using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002A3 RID: 675
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct NEWMAIL_NOTIFICATION
	{
		// Token: 0x0400115A RID: 4442
		internal int cbEntryID;

		// Token: 0x0400115B RID: 4443
		internal IntPtr lpEntryID;

		// Token: 0x0400115C RID: 4444
		internal int cbParentID;

		// Token: 0x0400115D RID: 4445
		internal IntPtr lpParentID;

		// Token: 0x0400115E RID: 4446
		internal int ulFlags;

		// Token: 0x0400115F RID: 4447
		internal IntPtr lpszMessageClass;

		// Token: 0x04001160 RID: 4448
		internal int ulMessageFlags;
	}
}
