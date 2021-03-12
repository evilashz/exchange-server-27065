using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000216 RID: 534
	[Flags]
	internal enum DeleteItemFlags
	{
		// Token: 0x04000F6A RID: 3946
		None = 0,
		// Token: 0x04000F6B RID: 3947
		SoftDelete = 1,
		// Token: 0x04000F6C RID: 3948
		HardDelete = 2,
		// Token: 0x04000F6D RID: 3949
		MoveToDeletedItems = 4,
		// Token: 0x04000F6E RID: 3950
		NormalizedDeleteFlags = 7,
		// Token: 0x04000F6F RID: 3951
		SuppressReadReceipt = 256,
		// Token: 0x04000F70 RID: 3952
		DeclineCalendarItemWithResponse = 4096,
		// Token: 0x04000F71 RID: 3953
		DeclineCalendarItemWithoutResponse = 8192,
		// Token: 0x04000F72 RID: 3954
		CancelCalendarItem = 16384,
		// Token: 0x04000F73 RID: 3955
		EmptyFolder = 65536,
		// Token: 0x04000F74 RID: 3956
		DeleteAllClutter = 131072,
		// Token: 0x04000F75 RID: 3957
		ClutterActionByUserOverride = 262144
	}
}
