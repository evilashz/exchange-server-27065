using System;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x02000953 RID: 2387
	internal static class DlpPolicyValidationLimits
	{
		// Token: 0x0400313F RID: 12607
		internal const int MaxNameLength = 64;

		// Token: 0x04003140 RID: 12608
		internal const int MaxVersionLength = 16;

		// Token: 0x04003141 RID: 12609
		internal const int ContentVersion = 16;

		// Token: 0x04003142 RID: 12610
		internal const int MaxPublisherNameLength = 256;

		// Token: 0x04003143 RID: 12611
		internal const int MaxDescriptionLength = 1024;

		// Token: 0x04003144 RID: 12612
		internal const int MaxKeywordLength = 64;

		// Token: 0x04003145 RID: 12613
		internal const int MaxTypeLength = 32;

		// Token: 0x04003146 RID: 12614
		internal const int MaxTokenLength = 32;

		// Token: 0x04003147 RID: 12615
		internal const int MaxPolicyCommandLength = 4096;

		// Token: 0x04003148 RID: 12616
		internal const int MaxPolicyCommandResourceLength = 1024;
	}
}
