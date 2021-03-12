using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x0200016D RID: 365
	internal interface ISplitOperationState
	{
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000EB4 RID: 3764
		// (set) Token: 0x06000EB5 RID: 3765
		DateTime StartTime { get; set; }

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000EB6 RID: 3766
		// (set) Token: 0x06000EB7 RID: 3767
		DateTime CompletedTime { get; set; }

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000EB8 RID: 3768
		// (set) Token: 0x06000EB9 RID: 3769
		Exception Error { get; set; }

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000EBA RID: 3770
		// (set) Token: 0x06000EBB RID: 3771
		string ErrorDetails { get; set; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000EBC RID: 3772
		// (set) Token: 0x06000EBD RID: 3773
		bool PartialStep { get; set; }

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000EBE RID: 3774
		// (set) Token: 0x06000EBF RID: 3775
		byte PartialStepCount { get; set; }

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000EC0 RID: 3776
		// (set) Token: 0x06000EC1 RID: 3777
		byte RetryCount { get; set; }
	}
}
