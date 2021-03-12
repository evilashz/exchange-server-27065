using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000234 RID: 564
	[Flags]
	internal enum CalendarProcessingSteps
	{
		// Token: 0x040010C7 RID: 4295
		None = 0,
		// Token: 0x040010C8 RID: 4296
		PropsCheck = 1,
		// Token: 0x040010C9 RID: 4297
		PrincipalWantsCopyChecked = 2,
		// Token: 0x040010CA RID: 4298
		NeedsCustomForm = 4,
		// Token: 0x040010CB RID: 4299
		PrincipalHadTombstone = 8,
		// Token: 0x040010CC RID: 4300
		CreatedOnPrincipal = 16,
		// Token: 0x040010CD RID: 4301
		LookedForOutOfDate = 32,
		// Token: 0x040010CE RID: 4302
		ChangedMtgType = 64,
		// Token: 0x040010CF RID: 4303
		UpdatedCalItem = 128,
		// Token: 0x040010D0 RID: 4304
		CopiedOldProps = 256,
		// Token: 0x040010D1 RID: 4305
		CounterProposalSet = 512,
		// Token: 0x040010D2 RID: 4306
		SendAutoResponse = 1024,
		// Token: 0x040010D3 RID: 4307
		RevivedException = 2048,
		// Token: 0x040010D4 RID: 4308
		ProcessedMeetingForwardNotification = 4096
	}
}
