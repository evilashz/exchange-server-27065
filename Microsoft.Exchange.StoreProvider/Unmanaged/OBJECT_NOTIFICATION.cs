using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002A4 RID: 676
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct OBJECT_NOTIFICATION
	{
		// Token: 0x04001161 RID: 4449
		internal int cbEntryID;

		// Token: 0x04001162 RID: 4450
		internal IntPtr lpEntryID;

		// Token: 0x04001163 RID: 4451
		internal int ulObjType;

		// Token: 0x04001164 RID: 4452
		internal int cbParentID;

		// Token: 0x04001165 RID: 4453
		internal IntPtr lpParentID;

		// Token: 0x04001166 RID: 4454
		internal int cbOldID;

		// Token: 0x04001167 RID: 4455
		internal IntPtr lpOldID;

		// Token: 0x04001168 RID: 4456
		internal int cbOldParentID;

		// Token: 0x04001169 RID: 4457
		internal IntPtr lpOldParentID;

		// Token: 0x0400116A RID: 4458
		internal unsafe int* lpPropTagArray;
	}
}
