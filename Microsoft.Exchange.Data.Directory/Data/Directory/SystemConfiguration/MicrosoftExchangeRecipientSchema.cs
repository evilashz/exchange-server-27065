using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004DC RID: 1244
	internal class MicrosoftExchangeRecipientSchema : ADConfigurationObjectSchema
	{
		// Token: 0x040025B2 RID: 9650
		public static readonly ADPropertyDefinition EmailAddresses = ADRecipientSchema.EmailAddresses;

		// Token: 0x040025B3 RID: 9651
		public static readonly ADPropertyDefinition PrimarySmtpAddress = ADRecipientSchema.PrimarySmtpAddress;

		// Token: 0x040025B4 RID: 9652
		public static readonly ADPropertyDefinition Alias = ADRecipientSchema.Alias;

		// Token: 0x040025B5 RID: 9653
		public static readonly ADPropertyDefinition DisplayName = ADRecipientSchema.DisplayName;

		// Token: 0x040025B6 RID: 9654
		public static readonly ADPropertyDefinition HiddenFromAddressListsEnabled = ADRecipientSchema.HiddenFromAddressListsEnabled;

		// Token: 0x040025B7 RID: 9655
		public static readonly ADPropertyDefinition EmailAddressPolicyEnabled = ADRecipientSchema.EmailAddressPolicyEnabled;

		// Token: 0x040025B8 RID: 9656
		public static readonly ADPropertyDefinition LegacyExchangeDN = ADRecipientSchema.LegacyExchangeDN;

		// Token: 0x040025B9 RID: 9657
		public static readonly ADPropertyDefinition ForwardingAddress = ADRecipientSchema.ForwardingAddress;
	}
}
