using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003B6 RID: 950
	internal class ClientAccessArraySchema : ADConfigurationObjectSchema
	{
		// Token: 0x04001A06 RID: 6662
		public static readonly ADPropertyDefinition ExchangeLegacyDN = new ADPropertyDefinition("ExchangeLegacyDN", ExchangeObjectVersion.Exchange2003, typeof(string), "legacyExchangeDN", ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, new PropertyDefinitionConstraint[]
		{
			new ValidLegacyDNConstraint()
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001A07 RID: 6663
		public static readonly ADPropertyDefinition Site = ServerSchema.ServerSite;

		// Token: 0x04001A08 RID: 6664
		public static readonly ADPropertyDefinition Fqdn = new ADPropertyDefinition("Fqdn", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.NetworkAddress
		}, new CustomFilterBuilderDelegate(Server.FqdnFilterBuilder), new GetterDelegate(Server.FqdnGetter), new SetterDelegate(ClientAccessArray.FqdnSetter), null, null);

		// Token: 0x04001A09 RID: 6665
		internal static readonly ADPropertyDefinition NetworkAddress = ServerSchema.NetworkAddress;

		// Token: 0x04001A0A RID: 6666
		public static readonly ADPropertyDefinition ArrayDefinition = new ADPropertyDefinition("ArrayDefinition", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchServerRedundantMachines", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001A0B RID: 6667
		public static readonly ADPropertyDefinition Servers = new ADPropertyDefinition("Servers", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchServerAssociationBL", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001A0C RID: 6668
		public static readonly ADPropertyDefinition ServerCount = new ADPropertyDefinition("ServerCount", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMigrationLogDirectorySizeQuota", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
