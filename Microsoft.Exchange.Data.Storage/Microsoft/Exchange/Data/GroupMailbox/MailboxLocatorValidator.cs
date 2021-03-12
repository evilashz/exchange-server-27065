using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000801 RID: 2049
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class MailboxLocatorValidator
	{
		// Token: 0x06004C75 RID: 19573 RVA: 0x0013CA6E File Offset: 0x0013AC6E
		public static bool IsValidUserLocator(ADUser user)
		{
			return MailboxLocatorValidator.IsValidUserLocator(user.LegacyExchangeDN, user.ExchangeVersion, user.RecipientTypeDetails);
		}

		// Token: 0x06004C76 RID: 19574 RVA: 0x0013CA87 File Offset: 0x0013AC87
		public static bool IsValidUserLocator(string legacyExchangeDn, ExchangeObjectVersion objectVersion, RecipientTypeDetails recipientTypeDetails)
		{
			return MailboxLocatorValidator.IsValidMailboxLocator(legacyExchangeDn, objectVersion) && (recipientTypeDetails == RecipientTypeDetails.UserMailbox || recipientTypeDetails == RecipientTypeDetails.MailUser);
		}

		// Token: 0x06004C77 RID: 19575 RVA: 0x0013CAA4 File Offset: 0x0013ACA4
		public static bool IsValidGroupLocator(ADUser user)
		{
			return MailboxLocatorValidator.IsValidGroupLocator(user.LegacyExchangeDN, user.ExchangeVersion, user.RecipientTypeDetails);
		}

		// Token: 0x06004C78 RID: 19576 RVA: 0x0013CABD File Offset: 0x0013ACBD
		public static bool IsValidGroupLocator(string legacyExchangeDn, ExchangeObjectVersion objectVersion, RecipientTypeDetails recipientTypeDetails)
		{
			return MailboxLocatorValidator.IsValidMailboxLocator(legacyExchangeDn, objectVersion) && recipientTypeDetails == RecipientTypeDetails.GroupMailbox;
		}

		// Token: 0x06004C79 RID: 19577 RVA: 0x0013CAD6 File Offset: 0x0013ACD6
		private static bool IsValidMailboxLocator(string legacyExchangeDn, ExchangeObjectVersion objectVersion)
		{
			return !string.IsNullOrWhiteSpace(legacyExchangeDn) && objectVersion != null;
		}
	}
}
