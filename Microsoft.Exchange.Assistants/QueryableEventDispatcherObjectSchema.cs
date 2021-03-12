using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000AB RID: 171
	internal class QueryableEventDispatcherObjectSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04000306 RID: 774
		public static readonly SimpleProviderPropertyDefinition AssistantName = QueryableObjectSchema.AssistantName;

		// Token: 0x04000307 RID: 775
		public static readonly SimpleProviderPropertyDefinition AssistantGuid = QueryableObjectSchema.AssistantGuid;

		// Token: 0x04000308 RID: 776
		public static readonly SimpleProviderPropertyDefinition CommittedWatermark = QueryableObjectSchema.CommittedWatermark;

		// Token: 0x04000309 RID: 777
		public static readonly SimpleProviderPropertyDefinition HighestEventQueued = QueryableObjectSchema.HighestEventQueued;

		// Token: 0x0400030A RID: 778
		public static readonly SimpleProviderPropertyDefinition RecoveryEventCounter = QueryableObjectSchema.RecoveryEventCounter;

		// Token: 0x0400030B RID: 779
		public static readonly SimpleProviderPropertyDefinition IsInRetry = QueryableObjectSchema.IsInRetry;

		// Token: 0x0400030C RID: 780
		public static readonly SimpleProviderPropertyDefinition PendingQueueLength = QueryableObjectSchema.PendingQueueLength;

		// Token: 0x0400030D RID: 781
		public static readonly SimpleProviderPropertyDefinition ActiveQueueLength = QueryableObjectSchema.ActiveQueueLength;

		// Token: 0x0400030E RID: 782
		public static readonly SimpleProviderPropertyDefinition PendingWorkers = QueryableObjectSchema.PendingWorkers;

		// Token: 0x0400030F RID: 783
		public static readonly SimpleProviderPropertyDefinition ActiveWorkers = QueryableObjectSchema.ActiveWorkers;
	}
}
