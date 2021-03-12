using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200026E RID: 622
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EntitySyncItem : MailboxSyncItem
	{
		// Token: 0x06001722 RID: 5922 RVA: 0x0008A618 File Offset: 0x00088818
		protected EntitySyncItem(Item item) : base(item)
		{
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x0008A621 File Offset: 0x00088821
		protected EntitySyncItem(IItem item) : base(null)
		{
			this.Item = item;
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x0008A634 File Offset: 0x00088834
		// (set) Token: 0x06001725 RID: 5925 RVA: 0x0008A678 File Offset: 0x00088878
		public IItem Item
		{
			get
			{
				base.CheckDisposed("get_Item");
				if (this.item == null)
				{
					Item item = base.NativeItem as Item;
					if (item == null)
					{
						return null;
					}
					this.item = EntitySyncItem.GetItem(item);
				}
				return this.item;
			}
			private set
			{
				if (!object.ReferenceEquals(this.item, value))
				{
					IDisposable disposable = this.item as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
					this.item = value;
				}
			}
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x0008A6AF File Offset: 0x000888AF
		public new static EntitySyncItem Bind(Item item)
		{
			return new EntitySyncItem(item);
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x0008A6B7 File Offset: 0x000888B7
		public static EntitySyncItem Bind(IItem item)
		{
			return new EntitySyncItem(item);
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0008A6C0 File Offset: 0x000888C0
		public static IItem GetItem(Item xsoItem)
		{
			string key = EntitySyncItem.GetKey(xsoItem.Session.MailboxGuid, xsoItem.Id.ObjectId);
			IEvents events = EntitySyncItem.GetEvents(xsoItem.Session, xsoItem.Id.ObjectId);
			return events.Read(key, null);
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0008A708 File Offset: 0x00088908
		public static string GetKey(Guid mailboxGuid, StoreId itemId)
		{
			return StoreId.StoreIdToEwsId(mailboxGuid, itemId);
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0008A711 File Offset: 0x00088911
		public static IEvents GetEvents(IStoreSession storeSession, StoreObjectId itemId)
		{
			return EntitySyncItem.GetEvents(new CalendaringContainer(storeSession, null), storeSession, itemId);
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0008A724 File Offset: 0x00088924
		public static IEvents GetEvents(ICalendaringContainer calendaringContainer, IStoreSession storeSession, StoreObjectId itemId)
		{
			StoreObjectId parentFolderId = storeSession.GetParentFolderId(itemId);
			IdConverter instance = IdConverter.Instance;
			string calendarId = instance.ToStringId(parentFolderId, storeSession);
			return calendaringContainer.Calendars[calendarId].Events;
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x0008A75C File Offset: 0x0008895C
		public void Reload()
		{
			Item item = base.NativeItem as Item;
			if (item != null)
			{
				StoreObjectId objectId = item.Id.ObjectId;
				StoreSession session = item.Session;
				item.Dispose();
				base.NativeItem = Microsoft.Exchange.Data.Storage.Item.Bind(session, objectId, EntitySyncItem.WatermarkProperties);
			}
			this.Item = null;
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x0008A7AC File Offset: 0x000889AC
		public void UpdateId(EntitySyncProviderFactory factory, string itemId)
		{
			base.CheckDisposed("get_NativeItem");
			if (base.NativeItem != null)
			{
				throw new InvalidOperationException("The sync item already has NativeItem");
			}
			StoreObjectId storeId = StoreId.EwsIdToFolderStoreObjectId(itemId);
			base.NativeItem = Microsoft.Exchange.Data.Storage.Item.Bind(factory.StoreSession, storeId, EntitySyncItem.WatermarkProperties);
			this.Item = null;
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x0008A7FC File Offset: 0x000889FC
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.Item = null;
				}
			}
			finally
			{
				base.InternalDispose(disposing);
			}
		}

		// Token: 0x04000E2E RID: 3630
		internal static readonly PropertyDefinition[] WatermarkProperties = new PropertyDefinition[]
		{
			ItemSchema.ArticleId,
			MessageItemSchema.IsRead
		};

		// Token: 0x04000E2F RID: 3631
		private IItem item;
	}
}
