using System;
using System.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200016D RID: 365
	internal class MessageTraceDataSetSchema
	{
		// Token: 0x040006DA RID: 1754
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = CommonMessageTraceSchema.OrganizationalUnitRootProperty;

		// Token: 0x040006DB RID: 1755
		internal static readonly HygienePropertyDefinition ExMessageIdProperty = CommonMessageTraceSchema.ExMessageIdProperty;

		// Token: 0x040006DC RID: 1756
		internal static readonly HygienePropertyDefinition MessagesTableProperty = new HygienePropertyDefinition("tvp_Messages", typeof(DataTable));

		// Token: 0x040006DD RID: 1757
		internal static readonly HygienePropertyDefinition MessagePropertiesTableProperty = new HygienePropertyDefinition("tvp_MessageProperties", typeof(DataTable));

		// Token: 0x040006DE RID: 1758
		internal static readonly HygienePropertyDefinition MessageActionTableProperty = new HygienePropertyDefinition("tvp_MessageActions", typeof(DataTable));

		// Token: 0x040006DF RID: 1759
		internal static readonly HygienePropertyDefinition MessageActionPropertiesTableProperty = new HygienePropertyDefinition("tvp_MessageActionProperties", typeof(DataTable));

		// Token: 0x040006E0 RID: 1760
		internal static readonly HygienePropertyDefinition MessageClassificationsTableProperty = new HygienePropertyDefinition("tvp_MessageClassifications", typeof(DataTable));

		// Token: 0x040006E1 RID: 1761
		internal static readonly HygienePropertyDefinition MessageClassificationPropertiesTableProperty = new HygienePropertyDefinition("tvp_MessageClassificationProperties", typeof(DataTable));

		// Token: 0x040006E2 RID: 1762
		internal static readonly HygienePropertyDefinition MessageClientInformationTableProperty = new HygienePropertyDefinition("tvp_MessageClientInformation", typeof(DataTable));

		// Token: 0x040006E3 RID: 1763
		internal static readonly HygienePropertyDefinition MessageClientInformationPropertiesTableProperty = new HygienePropertyDefinition("tvp_MessageClientInformationProperties", typeof(DataTable));

		// Token: 0x040006E4 RID: 1764
		internal static readonly HygienePropertyDefinition MessageEventsTableProperty = new HygienePropertyDefinition("tvp_MessageEvents", typeof(DataTable));

		// Token: 0x040006E5 RID: 1765
		internal static readonly HygienePropertyDefinition MessageEventPropertiesTableProperty = new HygienePropertyDefinition("tvp_MessageEventProperties", typeof(DataTable));

		// Token: 0x040006E6 RID: 1766
		internal static readonly HygienePropertyDefinition MessageEventRulesTableProperty = new HygienePropertyDefinition("tvp_MessageEventRules", typeof(DataTable));

		// Token: 0x040006E7 RID: 1767
		internal static readonly HygienePropertyDefinition MessageEventRulePropertiesTableProperty = new HygienePropertyDefinition("tvp_MessageEventRuleProperties", typeof(DataTable));

		// Token: 0x040006E8 RID: 1768
		internal static readonly HygienePropertyDefinition MessageEventRuleClassificationsTableProperty = new HygienePropertyDefinition("tvp_MessageEventRuleClassifications", typeof(DataTable));

		// Token: 0x040006E9 RID: 1769
		internal static readonly HygienePropertyDefinition MessageEventRuleClassificationPropertiesTableProperty = new HygienePropertyDefinition("tvp_MessageEventRuleClassificationProperties", typeof(DataTable));

		// Token: 0x040006EA RID: 1770
		internal static readonly HygienePropertyDefinition MessageEventSourceItemsTableProperty = new HygienePropertyDefinition("tvp_MessageEventSourceItems", typeof(DataTable));

		// Token: 0x040006EB RID: 1771
		internal static readonly HygienePropertyDefinition MessageEventSourceItemPropertiesTableProperty = new HygienePropertyDefinition("tvp_MessageEventSourceItemProperties", typeof(DataTable));

		// Token: 0x040006EC RID: 1772
		internal static readonly HygienePropertyDefinition MessageRecipientsTableProperty = new HygienePropertyDefinition("tvp_MessageRecipients", typeof(DataTable));

		// Token: 0x040006ED RID: 1773
		internal static readonly HygienePropertyDefinition MessageRecipientPropertiesTableProperty = new HygienePropertyDefinition("tvp_MessageRecipientProperties", typeof(DataTable));

		// Token: 0x040006EE RID: 1774
		internal static readonly HygienePropertyDefinition MessageRecipientStatusTableProperty = new HygienePropertyDefinition("tvp_MessageRecipientStatus", typeof(DataTable));

		// Token: 0x040006EF RID: 1775
		internal static readonly HygienePropertyDefinition MessageRecipientStatusPropertiesTableProperty = new HygienePropertyDefinition("tvp_MessageRecipientStatusProperties", typeof(DataTable));
	}
}
