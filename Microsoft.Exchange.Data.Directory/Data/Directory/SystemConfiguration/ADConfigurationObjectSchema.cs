using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002A9 RID: 681
	internal abstract class ADConfigurationObjectSchema : ADObjectSchema
	{
		// Token: 0x040012C3 RID: 4803
		public static readonly ADPropertyDefinition AdminDisplayName = new ADPropertyDefinition("AdminDisplayName", ExchangeObjectVersion.Exchange2003, typeof(string), "adminDisplayName", ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, MbxRecipientSchema.AdminDisplayName);

		// Token: 0x040012C4 RID: 4804
		public static readonly ADPropertyDefinition SystemFlags = new ADPropertyDefinition("SystemFlags", ExchangeObjectVersion.Exchange2003, typeof(SystemFlagsEnum), "systemFlags", ADPropertyDefinitionFlags.WriteOnce, SystemFlagsEnum.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
