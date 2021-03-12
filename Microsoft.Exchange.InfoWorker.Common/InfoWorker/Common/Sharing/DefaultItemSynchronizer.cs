using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x0200025D RID: 605
	internal class DefaultItemSynchronizer : ItemSynchronizer
	{
		// Token: 0x06001174 RID: 4468 RVA: 0x00050A32 File Offset: 0x0004EC32
		public DefaultItemSynchronizer(LocalFolder localFolder) : base(localFolder)
		{
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00050A3B File Offset: 0x0004EC3B
		public override void Sync(ItemType item, MailboxSession mailboxSession, ExchangeService exchangeService)
		{
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x00050A3D File Offset: 0x0004EC3D
		protected override Item Bind(MailboxSession mailboxSession, StoreId storeId)
		{
			return null;
		}
	}
}
