using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.E4E
{
	// Token: 0x02000329 RID: 809
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EncryptionConfigurationSchema : ObjectSchema
	{
		// Token: 0x04000BDB RID: 3035
		public static readonly SimpleProviderPropertyDefinition Image = new SimpleProviderPropertyDefinition("Image", ExchangeObjectVersion.Exchange2012, typeof(byte[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000BDC RID: 3036
		public static readonly SimpleProviderPropertyDefinition EmailText = new SimpleProviderPropertyDefinition("EmailText", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000BDD RID: 3037
		public static readonly SimpleProviderPropertyDefinition PortalText = new SimpleProviderPropertyDefinition("PortalText", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000BDE RID: 3038
		public static readonly SimpleProviderPropertyDefinition DisclaimerText = new SimpleProviderPropertyDefinition("DisclaimerText", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000BDF RID: 3039
		public static readonly SimpleProviderPropertyDefinition OTPEnabled = new SimpleProviderPropertyDefinition("OTPEnabled", ExchangeObjectVersion.Exchange2012, typeof(bool), PropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000BE0 RID: 3040
		public static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2012, typeof(OMEConfigurationId), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			EncryptionConfigurationSchema.Image,
			EncryptionConfigurationSchema.EmailText,
			EncryptionConfigurationSchema.PortalText
		}, null, new GetterDelegate(EncryptionConfiguration.IdentityGetter), null);
	}
}
