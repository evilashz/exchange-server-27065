using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000291 RID: 657
	internal class TransportQueueQuerySchema
	{
		// Token: 0x04000DE7 RID: 3559
		internal static readonly SimpleProviderPropertyDefinition ForestIdQueryProperty = TransportQueueSchema.ForestIdProperty;

		// Token: 0x04000DE8 RID: 3560
		internal static readonly SimpleProviderPropertyDefinition AggregatedByQueryProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("AggregatedBy", typeof(string));

		// Token: 0x04000DE9 RID: 3561
		internal static readonly SimpleProviderPropertyDefinition DetailsLevelQueryProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("DetailsLevel", typeof(int), 0, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000DEA RID: 3562
		internal static readonly SimpleProviderPropertyDefinition PageSizeQueryProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("PageSize", typeof(int), 100, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000DEB RID: 3563
		internal static readonly SimpleProviderPropertyDefinition DetailsResultSizeQueryProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("DetailsResultSize", typeof(int), 20, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000DEC RID: 3564
		internal static readonly SimpleProviderPropertyDefinition FreshnessCutoffTimeSecondsProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("cutOffTimeSeconds", typeof(int), 0, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000DED RID: 3565
		internal static readonly SimpleProviderPropertyDefinition TlsDomainQueryProperty = TransportQueueSchema.TlsDomainProperty;

		// Token: 0x04000DEE RID: 3566
		internal static readonly SimpleProviderPropertyDefinition NextHopDomainQueryProperty = TransportQueueSchema.NextHopDomainProperty;

		// Token: 0x04000DEF RID: 3567
		internal static readonly SimpleProviderPropertyDefinition NextHopKeyQueryProperty = TransportQueueSchema.NextHopKeyProperty;

		// Token: 0x04000DF0 RID: 3568
		internal static readonly SimpleProviderPropertyDefinition NextHopConnectorQueryProperty = TransportQueueSchema.NextHopConnectorProperty;

		// Token: 0x04000DF1 RID: 3569
		internal static readonly SimpleProviderPropertyDefinition NextHopCategoryQueryProperty = TransportQueueSchema.NextHopCategoryProperty;

		// Token: 0x04000DF2 RID: 3570
		internal static readonly SimpleProviderPropertyDefinition DeliveryTypeQueryProperty = TransportQueueSchema.DeliveryTypeProperty;

		// Token: 0x04000DF3 RID: 3571
		internal static readonly SimpleProviderPropertyDefinition RiskLevelQueryProperty = TransportQueueSchema.RiskLevelProperty;

		// Token: 0x04000DF4 RID: 3572
		internal static readonly SimpleProviderPropertyDefinition OutboundIPPoolQueryProperty = TransportQueueSchema.OutboundIPPoolProperty;

		// Token: 0x04000DF5 RID: 3573
		internal static readonly SimpleProviderPropertyDefinition StatusQueryProperty = TransportQueueSchema.StatusProperty;

		// Token: 0x04000DF6 RID: 3574
		internal static readonly SimpleProviderPropertyDefinition LastErrorQueryProperty = TransportQueueSchema.LastErrorProperty;

		// Token: 0x04000DF7 RID: 3575
		internal static readonly SimpleProviderPropertyDefinition MessageCountQueryProperty = TransportQueueSchema.MessageCountProperty;

		// Token: 0x04000DF8 RID: 3576
		internal static readonly SimpleProviderPropertyDefinition DeferredMessageCountQueryProperty = TransportQueueSchema.DeferredMessageCountProperty;

		// Token: 0x04000DF9 RID: 3577
		internal static readonly SimpleProviderPropertyDefinition LockedMessageCountQueryProperty = TransportQueueSchema.LockedMessageCountProperty;

		// Token: 0x04000DFA RID: 3578
		internal static readonly SimpleProviderPropertyDefinition IncomingRateQueryProperty = TransportQueueSchema.IncomingRateProperty;

		// Token: 0x04000DFB RID: 3579
		internal static readonly SimpleProviderPropertyDefinition OutgoingRateQueryProperty = TransportQueueSchema.OutgoingRateProperty;

		// Token: 0x04000DFC RID: 3580
		internal static readonly SimpleProviderPropertyDefinition VelocityQueryProperty = TransportQueueSchema.VelocityProperty;
	}
}
