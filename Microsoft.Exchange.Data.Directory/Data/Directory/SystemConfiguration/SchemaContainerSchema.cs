using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000576 RID: 1398
	internal sealed class SchemaContainerSchema : ADNonExchangeObjectSchema
	{
		// Token: 0x04002A51 RID: 10833
		public static readonly ADPropertyDefinition FsmoRoleOwner = new ADPropertyDefinition("FsmoRoleOwner", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "fSMORoleOwner", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
