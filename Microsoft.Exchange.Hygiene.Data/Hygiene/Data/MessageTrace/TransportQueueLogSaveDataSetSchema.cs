using System;
using System.Data;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200019F RID: 415
	internal class TransportQueueLogSaveDataSetSchema
	{
		// Token: 0x04000854 RID: 2132
		internal static readonly SimpleProviderPropertyDefinition ForestIdProperty = TransportQueueSchema.ForestIdProperty;

		// Token: 0x04000855 RID: 2133
		internal static readonly SimpleProviderPropertyDefinition ServerIdProperty = TransportQueueSchema.ServerIdProperty;

		// Token: 0x04000856 RID: 2134
		internal static readonly SimpleProviderPropertyDefinition SnapshotDatetimeProperty = TransportQueueSchema.SnapshotDatetimeProperty;

		// Token: 0x04000857 RID: 2135
		internal static readonly HygienePropertyDefinition ServerPropertiesTableProperty = new HygienePropertyDefinition("properties", typeof(DataTable));

		// Token: 0x04000858 RID: 2136
		internal static readonly HygienePropertyDefinition QueueLogPropertiesTableProperty = new HygienePropertyDefinition("queueLogProperties", typeof(DataTable));
	}
}
