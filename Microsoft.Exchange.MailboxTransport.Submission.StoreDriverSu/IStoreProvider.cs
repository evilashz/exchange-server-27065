using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200001C RID: 28
	internal interface IStoreProvider
	{
		// Token: 0x06000124 RID: 292
		TObject FindByExchangeGuidIncludingAlternate<TObject>(Guid mailboxGuid, TenantPartitionHint tenantPartitionHint) where TObject : ADObject, new();

		// Token: 0x06000125 RID: 293
		MailboxSession OpenStore(OrganizationId organizationId, string displayName, string mailboxFqdn, string mailboxServerDN, Guid mailboxGuid, Guid mdbGuid, MultiValuedProperty<CultureInfo> senderLocales, MultiValuedProperty<Guid> aggregatedMailboxGuids);

		// Token: 0x06000126 RID: 294
		PublicFolderSession OpenStore(OrganizationId organizationId, Guid mailboxGuid);

		// Token: 0x06000127 RID: 295
		MessageItem GetMessageItem(StoreSession storeSession, StoreId storeId, StorePropertyDefinition[] contentConversionProperties);

		// Token: 0x06000128 RID: 296
		Exception CallDoneWithMessageWithRetry(StoreSession session, MessageItem item, int retryCount, MailItemSubmitter context);

		// Token: 0x06000129 RID: 297
		bool TryGetSendAsSubscription(MessageItem item, SendAsManager sendAsManager, out ISendAsSource subscription);
	}
}
