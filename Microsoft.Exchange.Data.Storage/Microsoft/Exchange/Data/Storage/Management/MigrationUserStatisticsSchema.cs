using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A44 RID: 2628
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationUserStatisticsSchema : MigrationUserSchema
	{
		// Token: 0x040036A7 RID: 13991
		public new static readonly ProviderPropertyDefinition Identity = MigrationUserSchema.Identity;

		// Token: 0x040036A8 RID: 13992
		public static readonly ProviderPropertyDefinition TotalItemsInSourceMailboxCount = new SimpleProviderPropertyDefinition("TotalItemsInSourceMailboxCount", ExchangeObjectVersion.Exchange2010, typeof(long?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040036A9 RID: 13993
		public static readonly ProviderPropertyDefinition TotalQueuedDuration = new SimpleProviderPropertyDefinition("TotalQueuedDuration", ExchangeObjectVersion.Exchange2010, typeof(EnhancedTimeSpan?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040036AA RID: 13994
		public static readonly ProviderPropertyDefinition TotalInProgressDuration = new SimpleProviderPropertyDefinition("TotalInProgressDuration", ExchangeObjectVersion.Exchange2010, typeof(EnhancedTimeSpan?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040036AB RID: 13995
		public static readonly ProviderPropertyDefinition TotalSyncedDuration = new SimpleProviderPropertyDefinition("TotalSyncedDuration", ExchangeObjectVersion.Exchange2010, typeof(EnhancedTimeSpan?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040036AC RID: 13996
		public static readonly ProviderPropertyDefinition TotalStalledDuration = new SimpleProviderPropertyDefinition("TotalStalledDuration", ExchangeObjectVersion.Exchange2010, typeof(EnhancedTimeSpan?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040036AD RID: 13997
		public static readonly ProviderPropertyDefinition Error = new SimpleProviderPropertyDefinition("Error", ExchangeObjectVersion.Exchange2010, typeof(object), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040036AE RID: 13998
		public static readonly ProviderPropertyDefinition DiagnosticInfo = new SimpleProviderPropertyDefinition("DiagnosticInfo", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040036AF RID: 13999
		public static readonly ProviderPropertyDefinition MigrationType = new SimpleProviderPropertyDefinition("MigrationType", ExchangeObjectVersion.Exchange2010, typeof(MigrationType), PropertyDefinitionFlags.None, Microsoft.Exchange.Data.Storage.Management.MigrationType.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040036B0 RID: 14000
		public static readonly ProviderPropertyDefinition EstimatedTotalTransferSize = new SimpleProviderPropertyDefinition("EstimatedTotalTransferSize", ExchangeObjectVersion.Exchange2010, typeof(ByteQuantifiedSize?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040036B1 RID: 14001
		public static readonly ProviderPropertyDefinition EstimatedTotalTransferCount = new SimpleProviderPropertyDefinition("EstimatedTotalTransferCount", ExchangeObjectVersion.Exchange2010, typeof(ulong?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040036B2 RID: 14002
		public static readonly ProviderPropertyDefinition BytesTransferred = new SimpleProviderPropertyDefinition("BytesTransferred", ExchangeObjectVersion.Exchange2010, typeof(ByteQuantifiedSize?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040036B3 RID: 14003
		public static readonly ProviderPropertyDefinition AverageBytesTransferredPerHour = new SimpleProviderPropertyDefinition("AverageBytesTransferredPerHour", ExchangeObjectVersion.Exchange2010, typeof(ByteQuantifiedSize?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040036B4 RID: 14004
		public static readonly ProviderPropertyDefinition CurrentBytesTransferredPerMinute = new SimpleProviderPropertyDefinition("CurrentBytesTransferredPerMinute", ExchangeObjectVersion.Exchange2010, typeof(ByteQuantifiedSize?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040036B5 RID: 14005
		public static readonly ProviderPropertyDefinition PercentageComplete = new SimpleProviderPropertyDefinition("PercentageComplete", ExchangeObjectVersion.Exchange2010, typeof(int?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040036B6 RID: 14006
		public static readonly ProviderPropertyDefinition SkippedItems = new SimpleProviderPropertyDefinition("SkippedItems", ExchangeObjectVersion.Exchange2010, typeof(MigrationUserSkippedItem), PropertyDefinitionFlags.MultiValued | PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
