using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003AA RID: 938
	internal sealed class AttachmentFilteringConfigSchema : ADConfigurationObjectSchema
	{
		// Token: 0x040019D9 RID: 6617
		public static readonly ADPropertyDefinition RejectResponse = new ADPropertyDefinition("RejectResponse", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchAttachmentFilteringRejectResponse", ADPropertyDefinitionFlags.None, string.Empty, AttachmentFilteringDefinitions.RejectResponseConstraints, AttachmentFilteringDefinitions.RejectResponseConstraints, null, null);

		// Token: 0x040019DA RID: 6618
		public static readonly ADPropertyDefinition AdminMessage = new ADPropertyDefinition("AdminMessage", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchAttachmentFilteringAdminMessage", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040019DB RID: 6619
		public static readonly ADPropertyDefinition FilterAction = new ADPropertyDefinition("FilterAction", ExchangeObjectVersion.Exchange2007, typeof(FilterActions), "msExchAttachmentFilteringFilterAction", ADPropertyDefinitionFlags.None, FilterActions.Strip, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040019DC RID: 6620
		public static readonly ADPropertyDefinition AttachmentNames = new ADPropertyDefinition("AttachmentNames", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchAttachmentFilteringAttachmentNames", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040019DD RID: 6621
		public static readonly ADPropertyDefinition ExceptionConnectors = new ADPropertyDefinition("ExceptionConnectors", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchAttachmentFilteringExceptionConnectorsLink", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
