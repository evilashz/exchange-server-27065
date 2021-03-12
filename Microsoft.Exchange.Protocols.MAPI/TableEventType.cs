using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200008F RID: 143
	public enum TableEventType
	{
		// Token: 0x04000303 RID: 771
		Changed = 1,
		// Token: 0x04000304 RID: 772
		Error,
		// Token: 0x04000305 RID: 773
		RowAdded,
		// Token: 0x04000306 RID: 774
		RowDeleted,
		// Token: 0x04000307 RID: 775
		RowModified,
		// Token: 0x04000308 RID: 776
		SortDone,
		// Token: 0x04000309 RID: 777
		RestrictDone,
		// Token: 0x0400030A RID: 778
		SetcolDone,
		// Token: 0x0400030B RID: 779
		Reload
	}
}
