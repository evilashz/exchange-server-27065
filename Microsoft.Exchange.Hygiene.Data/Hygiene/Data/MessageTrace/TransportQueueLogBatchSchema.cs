using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200019D RID: 413
	internal class TransportQueueLogBatchSchema
	{
		// Token: 0x04000849 RID: 2121
		internal static readonly SimpleProviderPropertyDefinition SnapshotDatetimeProperty = TransportQueueSchema.SnapshotDatetimeProperty;

		// Token: 0x0400084A RID: 2122
		internal static readonly SimpleProviderPropertyDefinition ServerIdProperty = TransportQueueSchema.ServerIdProperty;

		// Token: 0x0400084B RID: 2123
		internal static readonly SimpleProviderPropertyDefinition ServerNameProperty = TransportQueueSchema.ServerNameProperty;

		// Token: 0x0400084C RID: 2124
		internal static readonly SimpleProviderPropertyDefinition DagIdProperty = TransportQueueSchema.DagIdProperty;

		// Token: 0x0400084D RID: 2125
		internal static readonly SimpleProviderPropertyDefinition DagNameProperty = TransportQueueSchema.DagNameProperty;

		// Token: 0x0400084E RID: 2126
		internal static readonly SimpleProviderPropertyDefinition ADSiteIdProperty = TransportQueueSchema.ADSiteIdProperty;

		// Token: 0x0400084F RID: 2127
		internal static readonly SimpleProviderPropertyDefinition ADSiteNameProperty = TransportQueueSchema.ADSiteNameProperty;

		// Token: 0x04000850 RID: 2128
		internal static readonly SimpleProviderPropertyDefinition ForestIdProperty = TransportQueueSchema.ForestIdProperty;

		// Token: 0x04000851 RID: 2129
		internal static readonly SimpleProviderPropertyDefinition ForestNameProperty = TransportQueueSchema.ForestNameProperty;

		// Token: 0x04000852 RID: 2130
		internal static readonly HygienePropertyDefinition QueueLogProperty = new HygienePropertyDefinition("queueLogProperties", typeof(object), null, ADPropertyDefinitionFlags.MultiValued);
	}
}
