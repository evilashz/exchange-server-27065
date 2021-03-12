using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000214 RID: 532
	internal sealed class DatabaseAvailabilityGroupNetworkSchema : SimpleProviderObjectSchema
	{
		// Token: 0x060012A5 RID: 4773 RVA: 0x00039577 File Offset: 0x00037777
		private static SimpleProviderPropertyDefinition MakeProperty(string name, ExchangeObjectVersion versionAdded, Type type, PropertyDefinitionFlags flags, object defaultValue, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints)
		{
			return new SimpleProviderPropertyDefinition(name, versionAdded, type, flags, defaultValue, readConstraints, writeConstraints);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x00039588 File Offset: 0x00037788
		private static SimpleProviderPropertyDefinition MakeProperty(string name, Type type, object defaultValue)
		{
			return DatabaseAvailabilityGroupNetworkSchema.MakeProperty(name, ExchangeObjectVersion.Exchange2010, type, PropertyDefinitionFlags.None, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x000395A2 File Offset: 0x000377A2
		private static SimpleProviderPropertyDefinition MakeProperty(string name, Type type)
		{
			return DatabaseAvailabilityGroupNetworkSchema.MakeProperty(name, type, null);
		}

		// Token: 0x04000B13 RID: 2835
		public static readonly SimpleProviderPropertyDefinition Name = DatabaseAvailabilityGroupNetworkSchema.MakeProperty("Name", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new NoLeadingOrTrailingWhitespaceConstraint(),
			new MandatoryStringLengthConstraint(1, 127),
			new CharacterConstraint(new char[]
			{
				'\\',
				'/',
				'=',
				';',
				'\0',
				'\n'
			}, false)
		});

		// Token: 0x04000B14 RID: 2836
		public static readonly SimpleProviderPropertyDefinition Description = DatabaseAvailabilityGroupNetworkSchema.MakeProperty("Description", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 256)
		});

		// Token: 0x04000B15 RID: 2837
		public static readonly SimpleProviderPropertyDefinition Subnets = DatabaseAvailabilityGroupNetworkSchema.MakeProperty("Subnets", ExchangeObjectVersion.Exchange2010, typeof(DatabaseAvailabilityGroupNetworkSubnet), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B16 RID: 2838
		public static readonly SimpleProviderPropertyDefinition ReplicationEnabled = DatabaseAvailabilityGroupNetworkSchema.MakeProperty("ReplicationEnabled", typeof(bool), true);

		// Token: 0x04000B17 RID: 2839
		public static readonly SimpleProviderPropertyDefinition IgnoreNetwork = DatabaseAvailabilityGroupNetworkSchema.MakeProperty("IgnoreNetwork", typeof(bool), false);

		// Token: 0x04000B18 RID: 2840
		public static readonly SimpleProviderPropertyDefinition MapiAccessEnabled = DatabaseAvailabilityGroupNetworkSchema.MakeProperty("MapiAccessEnabled", typeof(bool), true);

		// Token: 0x04000B19 RID: 2841
		public new static readonly SimpleProviderPropertyDefinition Identity = DatabaseAvailabilityGroupNetworkSchema.MakeProperty("Identity", typeof(DagNetworkObjectId));

		// Token: 0x04000B1A RID: 2842
		public static readonly SimpleProviderPropertyDefinition Interfaces = DatabaseAvailabilityGroupNetworkSchema.MakeProperty("Interfaces", ExchangeObjectVersion.Exchange2010, typeof(DatabaseAvailabilityGroupNetworkInterface), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
