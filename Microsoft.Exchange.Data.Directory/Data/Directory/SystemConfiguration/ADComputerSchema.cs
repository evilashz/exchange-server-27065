using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000328 RID: 808
	internal sealed class ADComputerSchema : ADNonExchangeObjectSchema
	{
		// Token: 0x06002573 RID: 9587 RVA: 0x0009ED50 File Offset: 0x0009CF50
		internal static GetterDelegate OutOfServiceGetterDelegate(ProviderPropertyDefinition propertyDefinition)
		{
			return delegate(IPropertyBag bag)
			{
				int num = (int)bag[propertyDefinition];
				return 0 != num;
			};
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x0009ED78 File Offset: 0x0009CF78
		internal static QueryFilter IsOutOfServiceFilterBuilder(SinglePropertyFilter filter)
		{
			if (!(filter is ComparisonFilter))
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
			ComparisonFilter comparisonFilter = (ComparisonFilter)filter;
			if (comparisonFilter.ComparisonOperator != ComparisonOperator.Equal && ComparisonOperator.NotEqual != comparisonFilter.ComparisonOperator)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(comparisonFilter.Property.Name, comparisonFilter.ComparisonOperator.ToString()));
			}
			ComparisonOperator comparisonOperator = ComparisonOperator.Equal;
			if ((comparisonFilter.ComparisonOperator == ComparisonOperator.Equal && (bool)comparisonFilter.PropertyValue) || (ComparisonOperator.NotEqual == comparisonFilter.ComparisonOperator && !(bool)comparisonFilter.PropertyValue))
			{
				comparisonOperator = ComparisonOperator.NotEqual;
			}
			return new ComparisonFilter(comparisonOperator, SharedPropertyDefinitions.ProvisioningFlags, 0);
		}

		// Token: 0x040016F6 RID: 5878
		public static readonly ADPropertyDefinition ServicePrincipalName = new ADPropertyDefinition("ServicePrincipalName", ExchangeObjectVersion.Exchange2003, typeof(string), "servicePrincipalName", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040016F7 RID: 5879
		public static readonly ADPropertyDefinition Sid = IADSecurityPrincipalSchema.Sid;

		// Token: 0x040016F8 RID: 5880
		public static readonly ADPropertyDefinition OperatingSystemVersion = new ADPropertyDefinition("OperatingSystemVersion", ExchangeObjectVersion.Exchange2003, typeof(string), "operatingSystemVersion", ADPropertyDefinitionFlags.ReadOnly, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040016F9 RID: 5881
		public static readonly ADPropertyDefinition OperatingSystemServicePack = new ADPropertyDefinition("OperatingSystemServicePack", ExchangeObjectVersion.Exchange2003, typeof(string), "operatingSystemServicePack", ADPropertyDefinitionFlags.ReadOnly, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040016FA RID: 5882
		public static readonly ADPropertyDefinition UserAccountControl = new ADPropertyDefinition("UserAccountControl", ExchangeObjectVersion.Exchange2003, typeof(UserAccountControlFlags), "userAccountControl", ADPropertyDefinitionFlags.DoNotProvisionalClone, UserAccountControlFlags.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040016FB RID: 5883
		public static readonly ADPropertyDefinition DnsHostName = ADServerSchema.DnsHostName;

		// Token: 0x040016FC RID: 5884
		public static readonly ADPropertyDefinition ThrottlingPolicy = new ADPropertyDefinition("ThrottlingPolicy", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchThrottlingPolicyDN", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040016FD RID: 5885
		public static readonly ADPropertyDefinition IsOutOfService = new ADPropertyDefinition("IsOutOfService", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SharedPropertyDefinitions.ProvisioningFlags
		}, new CustomFilterBuilderDelegate(ADComputerSchema.IsOutOfServiceFilterBuilder), ADComputerSchema.OutOfServiceGetterDelegate(SharedPropertyDefinitions.ProvisioningFlags), null, null, null);

		// Token: 0x040016FE RID: 5886
		public static readonly ADPropertyDefinition ComponentStates = new ADPropertyDefinition("ComponentStates", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchComponentStates", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040016FF RID: 5887
		public static readonly ADPropertyDefinition MonitoringGroup = new ADPropertyDefinition("MonitoringGroup", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchShadowDisplayName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001700 RID: 5888
		public static readonly ADPropertyDefinition MonitoringInstalled = new ADPropertyDefinition("MonitoringInstalled", ExchangeObjectVersion.Exchange2003, typeof(int), "msExchCapabilityIdentifiers", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
