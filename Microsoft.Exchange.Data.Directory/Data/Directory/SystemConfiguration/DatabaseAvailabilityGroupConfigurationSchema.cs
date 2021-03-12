using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003E3 RID: 995
	internal class DatabaseAvailabilityGroupConfigurationSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04001EB0 RID: 7856
		public new static readonly ADPropertyDefinition Name = new ADPropertyDefinition("Name", ExchangeObjectVersion.Exchange2010, typeof(string), "name", ADPropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.RawName
		}, new CustomFilterBuilderDelegate(ADObject.DummyCustomFilterBuilderDelegate), new GetterDelegate(DatabaseAvailabilityGroupConfiguration.DagConfigNameGetter), new SetterDelegate(DatabaseAvailabilityGroupConfiguration.DagConfigNameSetter), null, null);

		// Token: 0x04001EB1 RID: 7857
		public static readonly ADPropertyDefinition ConfigurationXML = new ADPropertyDefinition("ConfigurationXML", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchConfigurationXML", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001EB2 RID: 7858
		public static readonly ADPropertyDefinition Dags = new ADPropertyDefinition("Dags", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchMDBAvailabilityGroupConfigurationBL", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
