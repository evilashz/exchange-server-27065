using System;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x0200002E RID: 46
	public enum ConsistencyCheckType
	{
		// Token: 0x0400010B RID: 267
		None,
		// Token: 0x0400010C RID: 268
		CanValidateOwnerCheck,
		// Token: 0x0400010D RID: 269
		MeetingExistenceCheck,
		// Token: 0x0400010E RID: 270
		ValidateStoreObjectCheck,
		// Token: 0x0400010F RID: 271
		MeetingCancellationCheck,
		// Token: 0x04000110 RID: 272
		AttendeeOnListCheck,
		// Token: 0x04000111 RID: 273
		CorrectResponseCheck,
		// Token: 0x04000112 RID: 274
		MeetingPropertiesMatchCheck,
		// Token: 0x04000113 RID: 275
		RecurrenceBlobsConsistentCheck,
		// Token: 0x04000114 RID: 276
		RecurrencesMatchCheck,
		// Token: 0x04000115 RID: 277
		TimeZoneMatchCheck
	}
}
