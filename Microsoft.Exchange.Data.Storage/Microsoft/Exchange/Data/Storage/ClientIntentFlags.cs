using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200020E RID: 526
	[Flags]
	public enum ClientIntentFlags
	{
		// Token: 0x04000F0F RID: 3855
		None = 0,
		// Token: 0x04000F10 RID: 3856
		Principal = 1,
		// Token: 0x04000F11 RID: 3857
		Delegate = 2,
		// Token: 0x04000F12 RID: 3858
		DeletedWithNoResponse = 4,
		// Token: 0x04000F13 RID: 3859
		DeletedExceptionWithNoResponse = 8,
		// Token: 0x04000F14 RID: 3860
		RespondedTentative = 16,
		// Token: 0x04000F15 RID: 3861
		RespondedAccept = 32,
		// Token: 0x04000F16 RID: 3862
		RespondedDecline = 64,
		// Token: 0x04000F17 RID: 3863
		ModifiedStartTime = 128,
		// Token: 0x04000F18 RID: 3864
		ModifiedEndTime = 256,
		// Token: 0x04000F19 RID: 3865
		ModifiedTime = 384,
		// Token: 0x04000F1A RID: 3866
		ModifiedLocation = 512,
		// Token: 0x04000F1B RID: 3867
		RespondedExceptionDecline = 1024,
		// Token: 0x04000F1C RID: 3868
		MeetingCanceled = 2048,
		// Token: 0x04000F1D RID: 3869
		MeetingExceptionCanceled = 4096,
		// Token: 0x04000F1E RID: 3870
		MeetingHistoryCreatedByPatternChange = 8192,
		// Token: 0x04000F1F RID: 3871
		MeetingConvertedToHistoryByRemove = 16384,
		// Token: 0x04000F20 RID: 3872
		MeetingResponseFailedButMessageContentSent = 32768,
		// Token: 0x04000F21 RID: 3873
		MeetingResponseFailedNoContentToSend = 65536
	}
}
