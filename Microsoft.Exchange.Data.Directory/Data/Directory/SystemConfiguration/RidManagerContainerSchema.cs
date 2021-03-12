using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000566 RID: 1382
	internal sealed class RidManagerContainerSchema : ADNonExchangeObjectSchema
	{
		// Token: 0x04002A0F RID: 10767
		public static readonly ADPropertyDefinition FsmoRoleOwner = new ADPropertyDefinition("FsmoRoleOwner", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "fSMORoleOwner", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A10 RID: 10768
		public static readonly ADPropertyDefinition ReplicationAttributeMetadata = new ADPropertyDefinition("ReplicationAttributeMetadata", ExchangeObjectVersion.Exchange2003, typeof(string), "msDS-ReplAttributeMetadata", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
