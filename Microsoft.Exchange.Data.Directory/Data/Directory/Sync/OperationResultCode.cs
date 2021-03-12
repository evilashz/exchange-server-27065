using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000968 RID: 2408
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public enum OperationResultCode
	{
		// Token: 0x0400493C RID: 18748
		Success,
		// Token: 0x0400493D RID: 18749
		PartitionUnavailable,
		// Token: 0x0400493E RID: 18750
		ContextNotFound,
		// Token: 0x0400493F RID: 18751
		ContextOutOfScope,
		// Token: 0x04004940 RID: 18752
		MergeFailedIncompleteGetChangesSequence,
		// Token: 0x04004941 RID: 18753
		MergeFailedIncompleteGetContextSequence,
		// Token: 0x04004942 RID: 18754
		MergeDelayedRetryAfterSometime,
		// Token: 0x04004943 RID: 18755
		UnspecifiedError
	}
}
