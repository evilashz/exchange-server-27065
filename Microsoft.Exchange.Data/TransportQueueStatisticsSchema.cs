using System;
using System.Data;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000293 RID: 659
	internal class TransportQueueStatisticsSchema : ObjectSchema
	{
		// Token: 0x04000E18 RID: 3608
		internal static readonly SimpleProviderPropertyDefinition ServerNameProperty = TransportQueueSchema.ServerNameProperty;

		// Token: 0x04000E19 RID: 3609
		internal static readonly SimpleProviderPropertyDefinition TlsDomainProperty = TransportQueueSchema.TlsDomainProperty;

		// Token: 0x04000E1A RID: 3610
		internal static readonly SimpleProviderPropertyDefinition NextHopDomainProperty = TransportQueueSchema.NextHopDomainProperty;

		// Token: 0x04000E1B RID: 3611
		internal static readonly SimpleProviderPropertyDefinition NextHopKeyProperty = TransportQueueSchema.NextHopKeyProperty;

		// Token: 0x04000E1C RID: 3612
		internal static readonly SimpleProviderPropertyDefinition NextHopCategoryProperty = TransportQueueSchema.NextHopCategoryProperty;

		// Token: 0x04000E1D RID: 3613
		internal static readonly SimpleProviderPropertyDefinition DeliveryTypeProperty = TransportQueueSchema.DeliveryTypeProperty;

		// Token: 0x04000E1E RID: 3614
		internal static readonly SimpleProviderPropertyDefinition RiskLevelProperty = TransportQueueSchema.RiskLevelProperty;

		// Token: 0x04000E1F RID: 3615
		internal static readonly SimpleProviderPropertyDefinition OutboundIPPoolProperty = TransportQueueSchema.OutboundIPPoolProperty;

		// Token: 0x04000E20 RID: 3616
		internal static readonly SimpleProviderPropertyDefinition StatusProperty = TransportQueueSchema.StatusProperty;

		// Token: 0x04000E21 RID: 3617
		internal static readonly SimpleProviderPropertyDefinition LastErrorProperty = TransportQueueSchema.LastErrorProperty;

		// Token: 0x04000E22 RID: 3618
		internal static readonly SimpleProviderPropertyDefinition QueueCountProperty = TransportQueueSchema.QueueCountProperty;

		// Token: 0x04000E23 RID: 3619
		internal static readonly SimpleProviderPropertyDefinition MessageCountProperty = TransportQueueSchema.MessageCountProperty;

		// Token: 0x04000E24 RID: 3620
		internal static readonly SimpleProviderPropertyDefinition DeferredMessageCountProperty = TransportQueueSchema.DeferredMessageCountProperty;

		// Token: 0x04000E25 RID: 3621
		internal static readonly SimpleProviderPropertyDefinition LockedMessageCountProperty = TransportQueueSchema.LockedMessageCountProperty;

		// Token: 0x04000E26 RID: 3622
		internal static readonly SimpleProviderPropertyDefinition IncomingRateProperty = TransportQueueSchema.IncomingRateProperty;

		// Token: 0x04000E27 RID: 3623
		internal static readonly SimpleProviderPropertyDefinition OutgoingRateProperty = TransportQueueSchema.OutgoingRateProperty;

		// Token: 0x04000E28 RID: 3624
		internal static readonly SimpleProviderPropertyDefinition VelocityProperty = TransportQueueSchema.VelocityProperty;

		// Token: 0x04000E29 RID: 3625
		internal static readonly SimpleProviderPropertyDefinition TransportQueueLogsProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("QueueLogs", typeof(string));

		// Token: 0x04000E2A RID: 3626
		internal static readonly SimpleProviderPropertyDefinition ForestIdQueryProperty = TransportQueueSchema.ForestIdProperty;

		// Token: 0x04000E2B RID: 3627
		internal static readonly SimpleProviderPropertyDefinition ServerQueryProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("ServerFilters", typeof(DataTable));

		// Token: 0x04000E2C RID: 3628
		internal static readonly SimpleProviderPropertyDefinition DagQueryProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("DagFilters", typeof(DataTable));

		// Token: 0x04000E2D RID: 3629
		internal static readonly SimpleProviderPropertyDefinition ADSiteQueryProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("ADSiteFilters", typeof(DataTable));

		// Token: 0x04000E2E RID: 3630
		internal static readonly SimpleProviderPropertyDefinition DataFilterQueryProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("DataFilters", typeof(DataTable));

		// Token: 0x04000E2F RID: 3631
		internal static readonly SimpleProviderPropertyDefinition AggregatedByQueryProperty = TransportQueueQuerySchema.AggregatedByQueryProperty;

		// Token: 0x04000E30 RID: 3632
		internal static readonly SimpleProviderPropertyDefinition DetailsLevelQueryProperty = TransportQueueQuerySchema.DetailsLevelQueryProperty;

		// Token: 0x04000E31 RID: 3633
		internal static readonly SimpleProviderPropertyDefinition PageSizeQueryProperty = TransportQueueQuerySchema.PageSizeQueryProperty;

		// Token: 0x04000E32 RID: 3634
		internal static readonly SimpleProviderPropertyDefinition DetailsResultSizeQueryProperty = TransportQueueQuerySchema.DetailsResultSizeQueryProperty;

		// Token: 0x04000E33 RID: 3635
		internal static readonly SimpleProviderPropertyDefinition FreshnessCutoffTimeSecondsProperty = TransportQueueQuerySchema.FreshnessCutoffTimeSecondsProperty;
	}
}
