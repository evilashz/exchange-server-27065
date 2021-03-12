using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003EA RID: 1002
	internal class StampGroupSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04001EF7 RID: 7927
		public new static readonly ADPropertyDefinition Name = new ADPropertyDefinition("Name", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchMDBAvailabilityGroupName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ADObjectNameStringLengthConstraint(1, 15),
			ComputerNameCharacterConstraint.DefaultConstraint
		}, null, null);

		// Token: 0x04001EF8 RID: 7928
		public static readonly ADPropertyDefinition Servers = new ADPropertyDefinition("Servers", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchMDBAvailabilityGroupBL", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
