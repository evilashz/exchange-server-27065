using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.GroupMetricsGenerator
{
	// Token: 0x020001A8 RID: 424
	internal enum GroupMetricsFault
	{
		// Token: 0x04000A7E RID: 2686
		None,
		// Token: 0x04000A7F RID: 2687
		DistributionDirectoryCreationException,
		// Token: 0x04000A80 RID: 2688
		UnreadableChangedGroupFile,
		// Token: 0x04000A81 RID: 2689
		TransientExceptionInExpansion,
		// Token: 0x04000A82 RID: 2690
		InvalidCredentialExceptionInExpansion,
		// Token: 0x04000A83 RID: 2691
		PermanentExceptionInExpansion
	}
}
