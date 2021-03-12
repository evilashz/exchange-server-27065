using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200020F RID: 527
	public enum CalendarInconsistencyFlag
	{
		// Token: 0x04000F23 RID: 3875
		None,
		// Token: 0x04000F24 RID: 3876
		StoreObjectValidation,
		// Token: 0x04000F25 RID: 3877
		StorageException,
		// Token: 0x04000F26 RID: 3878
		UserNotFound,
		// Token: 0x04000F27 RID: 3879
		LegacyUser,
		// Token: 0x04000F28 RID: 3880
		LargeDL,
		// Token: 0x04000F29 RID: 3881
		OrphanedMeeting,
		// Token: 0x04000F2A RID: 3882
		VersionInfo,
		// Token: 0x04000F2B RID: 3883
		TimeOverlap,
		// Token: 0x04000F2C RID: 3884
		StartTime,
		// Token: 0x04000F2D RID: 3885
		EndTime,
		// Token: 0x04000F2E RID: 3886
		StartTimeZone,
		// Token: 0x04000F2F RID: 3887
		RecurringTimeZone,
		// Token: 0x04000F30 RID: 3888
		Location,
		// Token: 0x04000F31 RID: 3889
		Cancellation,
		// Token: 0x04000F32 RID: 3890
		RecurrenceBlob,
		// Token: 0x04000F33 RID: 3891
		RecurrenceAnomaly,
		// Token: 0x04000F34 RID: 3892
		RecurringException,
		// Token: 0x04000F35 RID: 3893
		ModifiedOccurrenceMatch,
		// Token: 0x04000F36 RID: 3894
		MissingOccurrenceDeletion,
		// Token: 0x04000F37 RID: 3895
		ExtraOccurrenceDeletion,
		// Token: 0x04000F38 RID: 3896
		MissingItem,
		// Token: 0x04000F39 RID: 3897
		DuplicatedItem,
		// Token: 0x04000F3A RID: 3898
		Response,
		// Token: 0x04000F3B RID: 3899
		Organizer,
		// Token: 0x04000F3C RID: 3900
		MissingCvs
	}
}
