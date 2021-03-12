using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200001D RID: 29
	internal static class StoreProvider
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00009134 File Offset: 0x00007334
		// (set) Token: 0x0600012B RID: 299 RVA: 0x0000914C File Offset: 0x0000734C
		public static IStoreProvider StoreProviderInstance
		{
			get
			{
				if (StoreProvider.storeProvider == null)
				{
					StoreProvider.storeProvider = ADStoreProvider.Instance;
				}
				return StoreProvider.storeProvider;
			}
			set
			{
				StoreProvider.storeProvider = value;
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00009154 File Offset: 0x00007354
		public static TObject FindByExchangeGuidIncludingAlternate<TObject>(Guid mailboxGuid, TenantPartitionHint tenantPartitionHint) where TObject : ADObject, new()
		{
			return StoreProvider.StoreProviderInstance.FindByExchangeGuidIncludingAlternate<TObject>(mailboxGuid, tenantPartitionHint);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00009164 File Offset: 0x00007364
		public static MailboxSession OpenStore(OrganizationId organizationId, string displayName, string mailboxFqdn, string mailboxServerDN, Guid mailboxGuid, Guid mdbGuid, MultiValuedProperty<CultureInfo> senderLocales, MultiValuedProperty<Guid> aggregatedMailboxGuids)
		{
			return StoreProvider.StoreProviderInstance.OpenStore(organizationId, displayName, mailboxFqdn, mailboxServerDN, mailboxGuid, mdbGuid, senderLocales, aggregatedMailboxGuids);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00009187 File Offset: 0x00007387
		public static PublicFolderSession OpenStore(OrganizationId organizationId, Guid mailboxGuid)
		{
			return StoreProvider.StoreProviderInstance.OpenStore(organizationId, mailboxGuid);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00009195 File Offset: 0x00007395
		public static MessageItem GetMessageItem(StoreSession storeSession, StoreId storeId, StorePropertyDefinition[] contentConversionProperties)
		{
			return StoreProvider.StoreProviderInstance.GetMessageItem(storeSession, storeId, contentConversionProperties);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000091A4 File Offset: 0x000073A4
		public static Exception CallDoneWithMessageWithRetry(StoreSession session, MessageItem item, int retryCount, MailItemSubmitter context)
		{
			return StoreProvider.StoreProviderInstance.CallDoneWithMessageWithRetry(session, item, retryCount, context);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000091B4 File Offset: 0x000073B4
		public static bool TryGetSendAsSubscription(MessageItem item, SendAsManager sendAsManager, out ISendAsSource subscription)
		{
			return StoreProvider.StoreProviderInstance.TryGetSendAsSubscription(item, sendAsManager, out subscription);
		}

		// Token: 0x04000092 RID: 146
		private static IStoreProvider storeProvider;
	}
}
