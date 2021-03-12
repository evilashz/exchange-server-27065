using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000A9 RID: 169
	internal class QueryableEventControllerObjectSchema : SimpleProviderObjectSchema
	{
		// Token: 0x040002FA RID: 762
		public static readonly SimpleProviderPropertyDefinition ShutdownState = QueryableObjectSchema.ShutdownState;

		// Token: 0x040002FB RID: 763
		public static readonly SimpleProviderPropertyDefinition TimeToSaveWatermarks = QueryableObjectSchema.TimeToSaveWatermarks;

		// Token: 0x040002FC RID: 764
		public static readonly SimpleProviderPropertyDefinition HighestEventPolled = QueryableObjectSchema.HighestEventPolled;

		// Token: 0x040002FD RID: 765
		public static readonly SimpleProviderPropertyDefinition NumberEventsInQueueCurrent = QueryableObjectSchema.NumberEventsInQueueCurrent;

		// Token: 0x040002FE RID: 766
		public static readonly SimpleProviderPropertyDefinition EventFilter = QueryableObjectSchema.EventFilter;

		// Token: 0x040002FF RID: 767
		public static readonly SimpleProviderPropertyDefinition RestartRequired = QueryableObjectSchema.RestartRequired;

		// Token: 0x04000300 RID: 768
		public static readonly SimpleProviderPropertyDefinition TimeToUpdateIdleWatermarks = QueryableObjectSchema.TimeToUpdateIdleWatermarks;

		// Token: 0x04000301 RID: 769
		public static readonly SimpleProviderPropertyDefinition ActiveMailboxes = QueryableObjectSchema.ActiveMailboxes;

		// Token: 0x04000302 RID: 770
		public static readonly SimpleProviderPropertyDefinition UpToDateMailboxes = QueryableObjectSchema.UpToDateMailboxes;

		// Token: 0x04000303 RID: 771
		public static readonly SimpleProviderPropertyDefinition DeadMailboxes = QueryableObjectSchema.DeadMailboxes;

		// Token: 0x04000304 RID: 772
		public static readonly SimpleProviderPropertyDefinition RecoveryEventDispatcheres = QueryableObjectSchema.RecoveryEventDispatcheres;

		// Token: 0x04000305 RID: 773
		public static readonly SimpleProviderPropertyDefinition Governor = QueryableObjectSchema.Governor;
	}
}
