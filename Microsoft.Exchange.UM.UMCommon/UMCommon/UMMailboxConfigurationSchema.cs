using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200017A RID: 378
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UMMailboxConfigurationSchema : SimpleProviderObjectSchema
	{
		// Token: 0x06000BF8 RID: 3064 RVA: 0x0002BDBE File Offset: 0x00029FBE
		private static SimpleProviderPropertyDefinition CreatePropertyDefinition(string propertyName, Type propertyType, object defaultValue)
		{
			return new SimpleProviderPropertyDefinition(propertyName, ExchangeObjectVersion.Exchange2010, propertyType, PropertyDefinitionFlags.None, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x04000681 RID: 1665
		public static SimpleProviderPropertyDefinition Greeting = UMMailboxConfigurationSchema.CreatePropertyDefinition("Greeting", typeof(MailboxGreetingEnum), MailboxGreetingEnum.Voicemail);

		// Token: 0x04000682 RID: 1666
		public static SimpleProviderPropertyDefinition FolderToReadEmailsFrom = UMMailboxConfigurationSchema.CreatePropertyDefinition("FolderToReadEmailsFrom", typeof(MailboxFolder), null);

		// Token: 0x04000683 RID: 1667
		public static SimpleProviderPropertyDefinition ReadOldestUnreadVoiceMessagesFirst = UMMailboxConfigurationSchema.CreatePropertyDefinition("ReadOldestUnreadVoiceMessageFirst", typeof(bool), false);

		// Token: 0x04000684 RID: 1668
		public static SimpleProviderPropertyDefinition DefaultPlayOnPhoneNumber = UMMailboxConfigurationSchema.CreatePropertyDefinition("DefaultPlayOnPhoneNumber", typeof(string), null);

		// Token: 0x04000685 RID: 1669
		public static SimpleProviderPropertyDefinition ReceivedVoiceMailPreviewEnabled = UMMailboxConfigurationSchema.CreatePropertyDefinition("ReceivedVoiceMailPreviewEnabled", typeof(bool), true);

		// Token: 0x04000686 RID: 1670
		public static SimpleProviderPropertyDefinition SentVoiceMailPreviewEnabled = UMMailboxConfigurationSchema.CreatePropertyDefinition("SentVoiceMailPreviewEnabled", typeof(bool), true);
	}
}
