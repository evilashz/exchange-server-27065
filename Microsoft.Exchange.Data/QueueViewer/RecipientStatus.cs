using System;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x0200028B RID: 651
	[Serializable]
	public enum RecipientStatus
	{
		// Token: 0x04000DB2 RID: 3506
		[LocDescription(DataStrings.IDs.RecipientStatusNone)]
		None,
		// Token: 0x04000DB3 RID: 3507
		[LocDescription(DataStrings.IDs.RecipientStatusComplete)]
		Complete,
		// Token: 0x04000DB4 RID: 3508
		[LocDescription(DataStrings.IDs.RecipientStatusReady)]
		Ready,
		// Token: 0x04000DB5 RID: 3509
		[LocDescription(DataStrings.IDs.RecipientStatusRetry)]
		Retry,
		// Token: 0x04000DB6 RID: 3510
		[LocDescription(DataStrings.IDs.RecipientStatusLocked)]
		Locked
	}
}
