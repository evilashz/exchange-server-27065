using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000440 RID: 1088
	internal sealed class EncryptionSchema : ADLegacyVersionableObjectSchema
	{
		// Token: 0x04002112 RID: 8466
		public static readonly ADPropertyDefinition DefaultMessageFormat = new ADPropertyDefinition("DefaultMessageFormat", ExchangeObjectVersion.Exchange2003, typeof(bool), "defaultMessageFormat", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002113 RID: 8467
		public static readonly ADPropertyDefinition EncryptAlgListNA = new ADPropertyDefinition("EncryptAlgListNA", ExchangeObjectVersion.Exchange2003, typeof(string), "encryptAlgListNA", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002114 RID: 8468
		public static readonly ADPropertyDefinition EncryptAlgListOther = new ADPropertyDefinition("EncryptAlgListOther", ExchangeObjectVersion.Exchange2003, typeof(string), "encryptAlgListOther", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002115 RID: 8469
		public static readonly ADPropertyDefinition EncryptAlgSelectedNA = new ADPropertyDefinition("EncryptAlgSelectedNA", ExchangeObjectVersion.Exchange2003, typeof(string), "encryptAlgSelectedNA", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002116 RID: 8470
		public static readonly ADPropertyDefinition EncryptAlgSelectedOther = new ADPropertyDefinition("EncryptAlgSelectedOther", ExchangeObjectVersion.Exchange2003, typeof(string), "encryptAlgSelectedOther", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002117 RID: 8471
		public static readonly ADPropertyDefinition SMimeAlgListNA = new ADPropertyDefinition("SMimeAlgListNA", ExchangeObjectVersion.Exchange2003, typeof(string), "sMIMEAlgListNA", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002118 RID: 8472
		public static readonly ADPropertyDefinition SMimeAlgListOther = new ADPropertyDefinition("SMimeAlgListOther", ExchangeObjectVersion.Exchange2003, typeof(string), "sMIMEAlgListOther", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002119 RID: 8473
		public static readonly ADPropertyDefinition SMimeAlgSelectedNA = new ADPropertyDefinition("SMimeAlgSelectedNA", ExchangeObjectVersion.Exchange2003, typeof(string), "sMIMEAlgSelectedNA", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400211A RID: 8474
		public static readonly ADPropertyDefinition SMimeAlgSelectedOther = new ADPropertyDefinition("SMimeAlgSelectedOther", ExchangeObjectVersion.Exchange2003, typeof(string), "sMIMEAlgSelectedOther", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400211B RID: 8475
		public static readonly ADPropertyDefinition LegacyExchangeDN = SharedPropertyDefinitions.LegacyExchangeDN;
	}
}
