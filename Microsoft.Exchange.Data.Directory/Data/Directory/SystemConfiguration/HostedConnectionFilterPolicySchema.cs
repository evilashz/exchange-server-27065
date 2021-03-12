using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200046D RID: 1133
	internal class HostedConnectionFilterPolicySchema : ADConfigurationObjectSchema
	{
		// Token: 0x06003268 RID: 12904 RVA: 0x000CBD5A File Offset: 0x000C9F5A
		private static QueryFilter IsDefaultFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(HostedConnectionFilterPolicySchema.ConnectionFilterFlags, 1UL));
		}

		// Token: 0x0400229F RID: 8863
		internal const int IsDefaultShift = 0;

		// Token: 0x040022A0 RID: 8864
		internal const int EnableSafeListShift = 2;

		// Token: 0x040022A1 RID: 8865
		internal const int DirectoryBasedEdgeBlockModeShift = 3;

		// Token: 0x040022A2 RID: 8866
		internal const int DirectoryBasedEdgeBlockModeLength = 2;

		// Token: 0x040022A3 RID: 8867
		public static readonly ADPropertyDefinition ConnectionFilterFlags = new ADPropertyDefinition("ConnectionFilterFlags", ExchangeObjectVersion.Exchange2012, typeof(int), "msExchSpamFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040022A4 RID: 8868
		public static readonly ADPropertyDefinition IPAllowList = new ADPropertyDefinition("IPAllowList", ExchangeObjectVersion.Exchange2012, typeof(IPRange), "msExchSpamAllowedIPRanges", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new IPv6AddressesNotAllowedConstraint(),
			new IPRangeConstraint(256UL)
		}, null, null);

		// Token: 0x040022A5 RID: 8869
		public static readonly ADPropertyDefinition IPBlockList = new ADPropertyDefinition("IPBlockList", ExchangeObjectVersion.Exchange2012, typeof(IPRange), "msExchSpamBlockedIPRanges", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new IPv6AddressesNotAllowedConstraint(),
			new IPRangeConstraint(256UL)
		}, null, null);

		// Token: 0x040022A6 RID: 8870
		public static readonly ADPropertyDefinition IsDefault = new ADPropertyDefinition("IsDefault", ExchangeObjectVersion.Exchange2012, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedConnectionFilterPolicySchema.ConnectionFilterFlags
		}, new CustomFilterBuilderDelegate(HostedConnectionFilterPolicySchema.IsDefaultFilterBuilder), ADObject.FlagGetterDelegate(HostedConnectionFilterPolicySchema.ConnectionFilterFlags, 1), ADObject.FlagSetterDelegate(HostedConnectionFilterPolicySchema.ConnectionFilterFlags, 1), null, null);

		// Token: 0x040022A7 RID: 8871
		public static readonly ADPropertyDefinition EnableSafeList = ADObject.BitfieldProperty("EnableSafeList", 2, HostedConnectionFilterPolicySchema.ConnectionFilterFlags);

		// Token: 0x040022A8 RID: 8872
		public static readonly ADPropertyDefinition DirectoryBasedEdgeBlockMode = ADObject.BitfieldProperty("DirectoryBasedEdgeBlockMode", 3, 2, HostedConnectionFilterPolicySchema.ConnectionFilterFlags);
	}
}
