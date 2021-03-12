using System;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Sharing;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x02000259 RID: 601
	internal abstract class ItemSynchronizer
	{
		// Token: 0x06001157 RID: 4439 RVA: 0x0004FBDF File Offset: 0x0004DDDF
		public ItemSynchronizer(LocalFolder localFolder)
		{
			this.localFolder = localFolder;
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0004FBF0 File Offset: 0x0004DDF0
		public static ItemSynchronizer Create(LocalFolder localFolder, CultureInfo clientCulture)
		{
			switch (localFolder.Type)
			{
			case StoreObjectType.CalendarFolder:
				ItemSynchronizer.Tracer.TraceDebug(0L, "ItemSynchronizer.Create: Creating synchronizer for Calendar folder.");
				return new CalendarItemSynchronizer(localFolder, clientCulture);
			case StoreObjectType.ContactsFolder:
				ItemSynchronizer.Tracer.TraceDebug(0L, "ItemSynchronizer.Create: Creating synchronizer for Contacts folder.");
				return new ContactItemSynchronizer(localFolder);
			default:
				ItemSynchronizer.Tracer.TraceWarning<StoreObjectType>(0L, "ItemSynchronizer.Create: No custom synchronizer for type {0}. Creating default synchronizer.", localFolder.Type);
				return new DefaultItemSynchronizer(localFolder);
			}
		}

		// Token: 0x06001159 RID: 4441
		public abstract void Sync(ItemType item, MailboxSession mailboxSession, ExchangeService exchangeService);

		// Token: 0x0600115A RID: 4442
		protected abstract Item Bind(MailboxSession mailboxSession, StoreId storeId);

		// Token: 0x0600115B RID: 4443 RVA: 0x0004FC64 File Offset: 0x0004DE64
		public virtual void EnforceLevelOfDetails(MailboxSession mailboxSession, StoreId itemId, LevelOfDetails levelOfDetails)
		{
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0004FC68 File Offset: 0x0004DE68
		protected Item FindLocalCopy(string itemId, MailboxSession mailboxSession)
		{
			StoreId localIdFromRemoteId = this.localFolder.GetLocalIdFromRemoteId(itemId);
			if (localIdFromRemoteId == null)
			{
				return null;
			}
			Item item = null;
			Exception ex = null;
			try
			{
				item = this.Bind(mailboxSession, localIdFromRemoteId);
				bool flag = false;
				try
				{
					item.OpenAsReadWrite();
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						item.Dispose();
					}
				}
			}
			catch (ObjectNotFoundException ex2)
			{
				ex = ex2;
			}
			catch (CorruptDataException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				ItemSynchronizer.Tracer.TraceError<ItemSynchronizer, StoreId, Exception>((long)this.GetHashCode(), "{0}: Error binding local item {1}. Exception: {2}", this, localIdFromRemoteId, ex);
				if (ex is CorruptDataException)
				{
					this.localFolder.SelectItemToDelete(localIdFromRemoteId);
					return null;
				}
			}
			return item;
		}

		// Token: 0x04000B92 RID: 2962
		protected static readonly Trace Tracer = ExTraceGlobals.SharingEngineTracer;

		// Token: 0x04000B93 RID: 2963
		protected LocalFolder localFolder;
	}
}
