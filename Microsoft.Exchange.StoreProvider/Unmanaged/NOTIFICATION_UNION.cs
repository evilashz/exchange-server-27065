using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002A9 RID: 681
	[ClassAccessLevel(AccessLevel.Implementation)]
	[StructLayout(LayoutKind.Explicit)]
	internal struct NOTIFICATION_UNION
	{
		// Token: 0x0400117B RID: 4475
		[FieldOffset(0)]
		internal ERROR_NOTIFICATION err;

		// Token: 0x0400117C RID: 4476
		[FieldOffset(0)]
		internal NEWMAIL_NOTIFICATION newmail;

		// Token: 0x0400117D RID: 4477
		[FieldOffset(0)]
		internal OBJECT_NOTIFICATION obj;

		// Token: 0x0400117E RID: 4478
		[FieldOffset(0)]
		internal TABLE_NOTIFICATION tab;

		// Token: 0x0400117F RID: 4479
		[FieldOffset(0)]
		internal EXTENDED_NOTIFICATION ext;

		// Token: 0x04001180 RID: 4480
		[FieldOffset(0)]
		internal STATUS_OBJECT_NOTIFICATION statobj;

		// Token: 0x04001181 RID: 4481
		[FieldOffset(0)]
		internal CONNECTION_DROPPED_NOTIFICATION drop;
	}
}
