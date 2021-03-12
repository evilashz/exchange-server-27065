using System;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x0200027C RID: 636
	[Serializable]
	public enum MessageStatus
	{
		// Token: 0x04000CB9 RID: 3257
		[LocDescription(DataStrings.IDs.MessageStatusNone)]
		None,
		// Token: 0x04000CBA RID: 3258
		[LocDescription(DataStrings.IDs.MessageStatusActive)]
		Active,
		// Token: 0x04000CBB RID: 3259
		[LocDescription(DataStrings.IDs.MessageStatusPendingRemove)]
		PendingRemove,
		// Token: 0x04000CBC RID: 3260
		[LocDescription(DataStrings.IDs.MessageStatusPendingSuspend)]
		PendingSuspend,
		// Token: 0x04000CBD RID: 3261
		[LocDescription(DataStrings.IDs.MessageStatusReady)]
		Ready,
		// Token: 0x04000CBE RID: 3262
		[LocDescription(DataStrings.IDs.MessageStatusRetry)]
		Retry,
		// Token: 0x04000CBF RID: 3263
		[LocDescription(DataStrings.IDs.MessageStatusSuspended)]
		Suspended,
		// Token: 0x04000CC0 RID: 3264
		[LocDescription(DataStrings.IDs.MessageStatusLocked)]
		Locked
	}
}
