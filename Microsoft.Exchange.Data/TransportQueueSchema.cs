using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000290 RID: 656
	internal class TransportQueueSchema
	{
		// Token: 0x04000DC8 RID: 3528
		internal static readonly SimpleProviderPropertyDefinition SnapshotDatetimeProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("SnapshotDatetime", typeof(DateTime), DateTime.MinValue, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000DC9 RID: 3529
		internal static readonly SimpleProviderPropertyDefinition ServerIdProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("ServerId", typeof(Guid));

		// Token: 0x04000DCA RID: 3530
		internal static readonly SimpleProviderPropertyDefinition ServerNameProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("ServerName", typeof(string));

		// Token: 0x04000DCB RID: 3531
		internal static readonly SimpleProviderPropertyDefinition DagIdProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("DagId", typeof(Guid));

		// Token: 0x04000DCC RID: 3532
		internal static readonly SimpleProviderPropertyDefinition DagNameProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("DagName", typeof(string));

		// Token: 0x04000DCD RID: 3533
		internal static readonly SimpleProviderPropertyDefinition ADSiteIdProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("ADSiteId", typeof(Guid));

		// Token: 0x04000DCE RID: 3534
		internal static readonly SimpleProviderPropertyDefinition ADSiteNameProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("ADSiteName", typeof(string));

		// Token: 0x04000DCF RID: 3535
		internal static readonly SimpleProviderPropertyDefinition ForestIdProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("ForestId", typeof(Guid));

		// Token: 0x04000DD0 RID: 3536
		internal static readonly SimpleProviderPropertyDefinition ForestNameProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("ForestName", typeof(string));

		// Token: 0x04000DD1 RID: 3537
		internal static readonly SimpleProviderPropertyDefinition QueueIdProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("QueueId", typeof(Guid));

		// Token: 0x04000DD2 RID: 3538
		internal static readonly SimpleProviderPropertyDefinition QueueNameProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("QueueName", typeof(string));

		// Token: 0x04000DD3 RID: 3539
		internal static readonly SimpleProviderPropertyDefinition TlsDomainProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("TlsDomain", typeof(string));

		// Token: 0x04000DD4 RID: 3540
		internal static readonly SimpleProviderPropertyDefinition NextHopDomainProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("NextHopDomain", typeof(string));

		// Token: 0x04000DD5 RID: 3541
		internal static readonly SimpleProviderPropertyDefinition NextHopKeyProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("NextHopKey", typeof(string));

		// Token: 0x04000DD6 RID: 3542
		internal static readonly SimpleProviderPropertyDefinition NextHopConnectorProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("NextHopConnector", typeof(Guid));

		// Token: 0x04000DD7 RID: 3543
		internal static readonly SimpleProviderPropertyDefinition NextHopCategoryProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("NextHopCategory", typeof(string));

		// Token: 0x04000DD8 RID: 3544
		internal static readonly SimpleProviderPropertyDefinition DeliveryTypeProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("DeliveryType", typeof(string));

		// Token: 0x04000DD9 RID: 3545
		internal static readonly SimpleProviderPropertyDefinition RiskLevelProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("RiskLevel", typeof(string));

		// Token: 0x04000DDA RID: 3546
		internal static readonly SimpleProviderPropertyDefinition OutboundIPPoolProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("OutboundIPPool", typeof(string));

		// Token: 0x04000DDB RID: 3547
		internal static readonly SimpleProviderPropertyDefinition StatusProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("Status", typeof(string));

		// Token: 0x04000DDC RID: 3548
		internal static readonly SimpleProviderPropertyDefinition LastErrorProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("LastError", typeof(string));

		// Token: 0x04000DDD RID: 3549
		internal static readonly SimpleProviderPropertyDefinition QueueCountProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("QueueCount", typeof(int), 0, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000DDE RID: 3550
		internal static readonly SimpleProviderPropertyDefinition MessageCountProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("MessageCount", typeof(int), 0, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000DDF RID: 3551
		internal static readonly SimpleProviderPropertyDefinition DeferredMessageCountProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("DeferredMessageCount", typeof(int), 0, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000DE0 RID: 3552
		internal static readonly SimpleProviderPropertyDefinition LockedMessageCountProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("LockedMessageCount", typeof(int), 0, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000DE1 RID: 3553
		internal static readonly SimpleProviderPropertyDefinition IncomingRateProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("IncomingRate", typeof(double), 0.0, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000DE2 RID: 3554
		internal static readonly SimpleProviderPropertyDefinition OutgoingRateProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("OutgoingRate", typeof(double), 0.0, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000DE3 RID: 3555
		internal static readonly SimpleProviderPropertyDefinition VelocityProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("Velocity", typeof(double), 0.0, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000DE4 RID: 3556
		internal static readonly SimpleProviderPropertyDefinition FilterNameProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("FilterName", typeof(string));

		// Token: 0x04000DE5 RID: 3557
		internal static readonly SimpleProviderPropertyDefinition OperatorProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("Operator", typeof(string));

		// Token: 0x04000DE6 RID: 3558
		internal static readonly SimpleProviderPropertyDefinition FilterValueProperty = TransportQueueSchemaHelper.CreatePropertyDefinition("FilterValue", typeof(string));
	}
}
