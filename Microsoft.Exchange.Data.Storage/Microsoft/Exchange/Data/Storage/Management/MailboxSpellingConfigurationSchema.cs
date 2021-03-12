using System;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A0F RID: 2575
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxSpellingConfigurationSchema : UserConfigurationObjectSchema
	{
		// Token: 0x040034AA RID: 13482
		public static readonly SimplePropertyDefinition CheckBeforeSend = new SimplePropertyDefinition("spellingcheckbeforesend", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040034AB RID: 13483
		public static readonly SimplePropertyDefinition DictionaryLanguage = new SimplePropertyDefinition("spellingdictionarylanguage", ExchangeObjectVersion.Exchange2007, typeof(SpellcheckerSupportedLanguage), PropertyDefinitionFlags.None, SpellcheckerSupportedLanguage.EnglishUnitedStates, SpellcheckerSupportedLanguage.EnglishUnitedStates, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040034AC RID: 13484
		public static readonly SimplePropertyDefinition IgnoreUppercase = new SimplePropertyDefinition("spellingignoreuppercase", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040034AD RID: 13485
		public static readonly SimplePropertyDefinition IgnoreMixedDigits = new SimplePropertyDefinition("spellingignoremixeddigits", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
