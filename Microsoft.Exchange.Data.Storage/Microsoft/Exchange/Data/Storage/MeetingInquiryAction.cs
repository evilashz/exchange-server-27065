using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000211 RID: 529
	public enum MeetingInquiryAction
	{
		// Token: 0x04000F47 RID: 3911
		SendCancellation,
		// Token: 0x04000F48 RID: 3912
		ReviveMeeting,
		// Token: 0x04000F49 RID: 3913
		SendUpdateForMaster,
		// Token: 0x04000F4A RID: 3914
		MeetingAlreadyExists,
		// Token: 0x04000F4B RID: 3915
		ExistingOccurrence,
		// Token: 0x04000F4C RID: 3916
		HasDelegates,
		// Token: 0x04000F4D RID: 3917
		DeletedVersionNotFound,
		// Token: 0x04000F4E RID: 3918
		PairedCancellationFound,
		// Token: 0x04000F4F RID: 3919
		FailedToRevive
	}
}
