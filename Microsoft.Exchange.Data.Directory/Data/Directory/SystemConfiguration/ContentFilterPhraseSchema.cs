using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004C0 RID: 1216
	internal class ContentFilterPhraseSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04002567 RID: 9575
		public static readonly SimpleProviderPropertyDefinition Phrase = new SimpleProviderPropertyDefinition("Phrase", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002568 RID: 9576
		public static readonly SimpleProviderPropertyDefinition Influence = new SimpleProviderPropertyDefinition("Influence", ExchangeObjectVersion.Exchange2007, typeof(Influence), PropertyDefinitionFlags.None, Microsoft.Exchange.Data.Directory.SystemConfiguration.Influence.GoodWord, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
