using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000042 RID: 66
	internal enum OperatorPerformanceCounter
	{
		// Token: 0x0400005E RID: 94
		RetrieverNumberOfItemsProcessedIn0to50Milliseconds,
		// Token: 0x0400005F RID: 95
		RetrieverNumberOfItemsProcessedIn50to100Milliseconds,
		// Token: 0x04000060 RID: 96
		RetrieverNumberOfItemsProcessedIn100to2000Milliseconds,
		// Token: 0x04000061 RID: 97
		RetrieverNumberOfItemsProcessedInGreaterThan2000Milliseconds,
		// Token: 0x04000062 RID: 98
		RetrieverNumberOfItemsWithValidAnnotationToken,
		// Token: 0x04000063 RID: 99
		RetrieverNumberOfItemsWithoutAnnotationToken,
		// Token: 0x04000064 RID: 100
		RetrieverItemsWithRightsManagementAttempted,
		// Token: 0x04000065 RID: 101
		RetrieverItemsWithRightsManagementPartiallyProcessed,
		// Token: 0x04000066 RID: 102
		RetrieverItemsWithRightsManagementSkipped,
		// Token: 0x04000067 RID: 103
		TotalAnnotationTokenBytes,
		// Token: 0x04000068 RID: 104
		TotalAttachmentBytes,
		// Token: 0x04000069 RID: 105
		TotalAttachmentsRead,
		// Token: 0x0400006A RID: 106
		TotalBodyChars,
		// Token: 0x0400006B RID: 107
		IndexablePropertiesSize,
		// Token: 0x0400006C RID: 108
		TotalPoisonDocumentsProcessed,
		// Token: 0x0400006D RID: 109
		TotalTimeSpentConvertingToTextual,
		// Token: 0x0400006E RID: 110
		TotalTimeSpentDocParser,
		// Token: 0x0400006F RID: 111
		TotalTimeSpentLanguageDetection,
		// Token: 0x04000070 RID: 112
		TotalTimeSpentProcessingDocuments,
		// Token: 0x04000071 RID: 113
		TotalTimeSpentTokenDeserializer,
		// Token: 0x04000072 RID: 114
		TotalTimeSpentWorkBreaking
	}
}
