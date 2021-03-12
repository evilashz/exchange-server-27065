using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000079 RID: 121
	internal class DataLatencyDetailSchema
	{
		// Token: 0x040002EF RID: 751
		public static readonly HygienePropertyDefinition Identity = new HygienePropertyDefinition("Identity", typeof(ADObjectId));

		// Token: 0x040002F0 RID: 752
		public static readonly HygienePropertyDefinition TemporalPartition = new HygienePropertyDefinition("TemporalPartition", typeof(int), -1, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040002F1 RID: 753
		public static readonly HygienePropertyDefinition RowCount = new HygienePropertyDefinition("RowCount", typeof(long), -1L, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040002F2 RID: 754
		public static readonly HygienePropertyDefinition StartDateParameter = new HygienePropertyDefinition("startDate", typeof(DateTime?));

		// Token: 0x040002F3 RID: 755
		public static readonly HygienePropertyDefinition PartitionCountParameter = new HygienePropertyDefinition("partitionCount", typeof(int), 7, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
