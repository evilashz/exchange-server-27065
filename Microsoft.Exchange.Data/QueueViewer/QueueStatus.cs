using System;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x02000287 RID: 647
	[Serializable]
	public enum QueueStatus
	{
		// Token: 0x04000D54 RID: 3412
		[LocDescription(DataStrings.IDs.QueueStatusNone)]
		None,
		// Token: 0x04000D55 RID: 3413
		[LocDescription(DataStrings.IDs.QueueStatusActive)]
		Active,
		// Token: 0x04000D56 RID: 3414
		[LocDescription(DataStrings.IDs.QueueStatusReady)]
		Ready,
		// Token: 0x04000D57 RID: 3415
		[LocDescription(DataStrings.IDs.QueueStatusRetry)]
		Retry,
		// Token: 0x04000D58 RID: 3416
		[LocDescription(DataStrings.IDs.QueueStatusSuspended)]
		Suspended,
		// Token: 0x04000D59 RID: 3417
		[LocDescription(DataStrings.IDs.QueueStatusConnecting)]
		Connecting
	}
}
