using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000B6 RID: 182
	internal class QueryableThrottleObjectSchema : SimpleProviderObjectSchema
	{
		// Token: 0x0400034E RID: 846
		public static readonly SimpleProviderPropertyDefinition ThrottleName = QueryableObjectSchema.ThrottleName;

		// Token: 0x0400034F RID: 847
		public static readonly SimpleProviderPropertyDefinition CurrentThrottle = QueryableObjectSchema.CurrentThrottle;

		// Token: 0x04000350 RID: 848
		public static readonly SimpleProviderPropertyDefinition ActiveWorkItems = QueryableObjectSchema.ActiveWorkItems;

		// Token: 0x04000351 RID: 849
		public static readonly SimpleProviderPropertyDefinition OverThrottle = QueryableObjectSchema.OverThrottle;

		// Token: 0x04000352 RID: 850
		public static readonly SimpleProviderPropertyDefinition PendingWorkItemsOnBase = QueryableObjectSchema.PendingWorkItemsOnBase;

		// Token: 0x04000353 RID: 851
		public static readonly SimpleProviderPropertyDefinition QueueLength = QueryableObjectSchema.QueueLength;
	}
}
