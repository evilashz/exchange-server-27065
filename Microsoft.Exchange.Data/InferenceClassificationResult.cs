using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200008D RID: 141
	[Flags]
	internal enum InferenceClassificationResult
	{
		// Token: 0x040001F7 RID: 503
		None = 0,
		// Token: 0x040001F8 RID: 504
		IsClutterFinal = 1,
		// Token: 0x040001F9 RID: 505
		IsClutterModel = 2,
		// Token: 0x040001FA RID: 506
		IsOverridden = 28,
		// Token: 0x040001FB RID: 507
		ConversationActionOverride = 4,
		// Token: 0x040001FC RID: 508
		NeverClutterOverride = 8,
		// Token: 0x040001FD RID: 509
		AlwaysClutterOverride = 16,
		// Token: 0x040001FE RID: 510
		StopProcessingRulesOverride = 32
	}
}
