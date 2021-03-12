using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Agent.SharedMailboxSentItemsRoutingAgent
{
	// Token: 0x02000009 RID: 9
	internal class SharedMailboxConfigurationFactory : ISharedMailboxConfigurationFactory
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002324 File Offset: 0x00000524
		public SharedMailboxConfiguration GetSharedMailboxConfiguration(MailItem transportMailItem, string sender)
		{
			ADRawEntry adrawEntryByEmailAddress = SharedMailboxConfigurationFactory.GetADRawEntryByEmailAddress(transportMailItem);
			if (adrawEntryByEmailAddress != null && SharedMailboxConfigurationFactory.IsSenderSharedMailbox(adrawEntryByEmailAddress))
			{
				SharedMailboxSentItemBehavior messageCopyForSentAs = SharedMailboxConfigurationFactory.GetMessageCopyForSentAs(adrawEntryByEmailAddress);
				SharedMailboxSentItemBehavior messageCopyForSendOnBehalf = SharedMailboxConfigurationFactory.GetMessageCopyForSendOnBehalf(adrawEntryByEmailAddress);
				return new SharedMailboxConfiguration(true, messageCopyForSentAs, messageCopyForSendOnBehalf);
			}
			return new SharedMailboxConfiguration(false, SharedMailboxSentItemBehavior.None, SharedMailboxSentItemBehavior.None);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002364 File Offset: 0x00000564
		private static ADRawEntry GetADRawEntryByEmailAddress(MailItem mailItem)
		{
			string smtpAddress = mailItem.Message.From.SmtpAddress;
			if (!ProxyAddressBase.IsAddressStringValid(smtpAddress) || !RoutingAddress.IsValidAddress(smtpAddress))
			{
				return null;
			}
			IADRecipientCache iadrecipientCache = (IADRecipientCache)((ITransportMailItemWrapperFacade)mailItem).TransportMailItem.ADRecipientCacheAsObject;
			SmtpProxyAddress proxyAddress = new SmtpProxyAddress(smtpAddress, true);
			Result<ADRawEntry> result = iadrecipientCache.FindAndCacheRecipient(proxyAddress);
			if (result.Error == ProviderError.NotFound)
			{
				return null;
			}
			return result.Data;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023D4 File Offset: 0x000005D4
		private static bool IsSenderSharedMailbox(ADRawEntry recipientDetailsEntry)
		{
			object obj;
			if (recipientDetailsEntry.TryGetValueWithoutDefault(ADRecipientSchema.RecipientTypeDetails, out obj) && obj is RecipientTypeDetails)
			{
				RecipientTypeDetails recipientTypeDetails = (RecipientTypeDetails)obj;
				if (recipientTypeDetails == RecipientTypeDetails.SharedMailbox || recipientTypeDetails == RecipientTypeDetails.RemoteSharedMailbox)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002414 File Offset: 0x00000614
		private static SharedMailboxSentItemBehavior GetMessageCopyForSentAs(ADRawEntry recipientDetailsEntry)
		{
			object obj;
			if (!recipientDetailsEntry.TryGetValueWithoutDefault(MailboxSchema.MessageCopyForSentAsEnabled, out obj) || !(obj is bool))
			{
				return SharedMailboxSentItemBehavior.None;
			}
			if (!(bool)obj)
			{
				return SharedMailboxSentItemBehavior.None;
			}
			return SharedMailboxSentItemBehavior.CopyToSharedMailbox;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002448 File Offset: 0x00000648
		private static SharedMailboxSentItemBehavior GetMessageCopyForSendOnBehalf(ADRawEntry recipientDetailsEntry)
		{
			object obj;
			if (!recipientDetailsEntry.TryGetValueWithoutDefault(MailboxSchema.MessageCopyForSendOnBehalfEnabled, out obj) || !(obj is bool))
			{
				return SharedMailboxSentItemBehavior.None;
			}
			if (!(bool)obj)
			{
				return SharedMailboxSentItemBehavior.None;
			}
			return SharedMailboxSentItemBehavior.CopyToSharedMailbox;
		}
	}
}
