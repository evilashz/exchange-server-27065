using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007DF RID: 2015
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UnseenItemsReader : DisposableObject, IUnseenItemsReader, IDisposable
	{
		// Token: 0x06004B82 RID: 19330 RVA: 0x0013AAE3 File Offset: 0x00138CE3
		private UnseenItemsReader(IFolder inboxFolder)
		{
			ArgumentValidator.ThrowIfNull("inboxFolder", inboxFolder);
			this.inboxFolder = inboxFolder;
		}

		// Token: 0x06004B83 RID: 19331 RVA: 0x0013AB0A File Offset: 0x00138D0A
		public static IUnseenItemsReader Create(IMailboxSession mailboxSession)
		{
			return UnseenItemsReader.Create(mailboxSession, XSOFactory.Default);
		}

		// Token: 0x06004B84 RID: 19332 RVA: 0x0013AB18 File Offset: 0x00138D18
		public static IUnseenItemsReader Create(IMailboxSession mailboxSession, IXSOFactory xsoFactory)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("xsoFactory", xsoFactory);
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
			IFolder folder = xsoFactory.BindToFolder(mailboxSession, defaultFolderId);
			return new UnseenItemsReader(folder);
		}

		// Token: 0x06004B85 RID: 19333 RVA: 0x0013AB54 File Offset: 0x00138D54
		public void LoadLastNItemReceiveDates(IMailboxSession mailboxSession)
		{
			this.lastNItemReceiveDatesUtc.Clear();
			using (IQueryResult queryResult = this.inboxFolder.IConversationItemQuery(null, UnseenItemsReader.UnseenSortBy, UnseenItemsReader.UnseenItemProperties))
			{
				IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(100);
				if (propertyBags.Length > 0)
				{
					foreach (IStorePropertyBag storePropertyBag in propertyBags)
					{
						ExDateTime item = storePropertyBag.GetValueOrDefault<ExDateTime>(ConversationItemSchema.ConversationLastDeliveryTime, ExDateTime.MinValue).ToUtc();
						this.lastNItemReceiveDatesUtc.Add(item);
					}
				}
			}
		}

		// Token: 0x06004B86 RID: 19334 RVA: 0x0013AC08 File Offset: 0x00138E08
		public int GetUnseenItemCount(ExDateTime lastVisitedDate)
		{
			ExDateTime lastVisitedDateUtc = lastVisitedDate.ToUtc();
			int num = this.lastNItemReceiveDatesUtc.FindIndex((ExDateTime itemReceiveDate) => itemReceiveDate <= lastVisitedDateUtc);
			if (num >= 0)
			{
				return num;
			}
			return this.lastNItemReceiveDatesUtc.Count;
		}

		// Token: 0x06004B87 RID: 19335 RVA: 0x0013AC51 File Offset: 0x00138E51
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<UnseenItemsReader>(this);
		}

		// Token: 0x06004B88 RID: 19336 RVA: 0x0013AC59 File Offset: 0x00138E59
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.inboxFolder != null)
			{
				this.inboxFolder.Dispose();
				this.inboxFolder = null;
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x040028F3 RID: 10483
		private const int MaxUnseenItems = 100;

		// Token: 0x040028F4 RID: 10484
		private static readonly PropertyDefinition[] UnseenItemProperties = new PropertyDefinition[]
		{
			ConversationItemSchema.ConversationId,
			ConversationItemSchema.ConversationLastDeliveryTime
		};

		// Token: 0x040028F5 RID: 10485
		private static readonly SortBy[] UnseenSortBy = new SortBy[]
		{
			new SortBy(ConversationItemSchema.ConversationLastDeliveryTime, SortOrder.Descending)
		};

		// Token: 0x040028F6 RID: 10486
		private IFolder inboxFolder;

		// Token: 0x040028F7 RID: 10487
		private List<ExDateTime> lastNItemReceiveDatesUtc = new List<ExDateTime>(100);
	}
}
