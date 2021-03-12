using System;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200047F RID: 1151
	internal abstract class AddressBookItemEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002C51 RID: 11345
		protected abstract void BindToData();

		// Token: 0x06002C52 RID: 11346
		protected abstract void LoadRecipientsToItem(Item item, AddressBookItemEventHandler.ItemTypeToPeople itemType);

		// Token: 0x06002C53 RID: 11347 RVA: 0x000F6A38 File Offset: 0x000F4C38
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(ContactItemEventHandler));
			OwaEventRegistry.RegisterHandler(typeof(DirectoryItemEventHandler));
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x000F6A58 File Offset: 0x000F4C58
		[OwaEventParameter("it", typeof(AddressBookItemEventHandler.ItemTypeToPeople), false)]
		[OwaEvent("MsgToPeople")]
		[OwaEventParameter("id", typeof(ObjectId), true)]
		public void NewItemToPeople()
		{
			ExTraceGlobals.ContactsCallTracer.TraceDebug((long)this.GetHashCode(), "AddressBookEventHandler.NewItemToPeople");
			this.BindToData();
			if (base.UserContext.UserOptions.ViewRowCount < this.itemIds.Length)
			{
				throw new OwaInvalidOperationException(string.Format("Sending message to more than {0} item(s) in a single request is not supported", this.itemIds.Length));
			}
			AddressBookItemEventHandler.ItemTypeToPeople itemTypeToPeople = (AddressBookItemEventHandler.ItemTypeToPeople)base.GetParameter("it");
			if (!Enum.IsDefined(typeof(AddressBookItemEventHandler.ItemTypeToPeople), itemTypeToPeople))
			{
				throw new OwaInvalidOperationException(string.Format("Invalid item type '{0}' passed in.", itemTypeToPeople));
			}
			using (Item item = (itemTypeToPeople == AddressBookItemEventHandler.ItemTypeToPeople.Meeting) ? CalendarItem.Create(base.UserContext.MailboxSession, base.UserContext.CalendarFolderId) : MessageItem.Create(base.UserContext.MailboxSession, base.UserContext.DraftsFolderId))
			{
				item[ItemSchema.ConversationIndexTracking] = true;
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.ItemsCreated.Increment();
				}
				this.LoadRecipientsToItem(item, itemTypeToPeople);
				if (itemTypeToPeople == AddressBookItemEventHandler.ItemTypeToPeople.Meeting)
				{
					CalendarItemBase calendarItemBase = (CalendarItemBase)item;
					calendarItemBase[ItemSchema.ReminderIsSet] = base.UserContext.UserOptions.EnableReminders;
					calendarItemBase[ItemSchema.ReminderMinutesBeforeStart] = base.UserContext.CalendarSettings.DefaultReminderTime;
					calendarItemBase.Save(SaveMode.ResolveConflicts);
					calendarItemBase.Load();
					this.Writer.Write("?ae=Item&a=New&t=IPM.Appointment&exdltdrft=1&id=");
					this.Writer.Write(HttpUtility.UrlEncode(calendarItemBase.Id.ObjectId.ToBase64String()));
				}
				else
				{
					MessageItem messageItem = (MessageItem)item;
					if (itemTypeToPeople == AddressBookItemEventHandler.ItemTypeToPeople.TextMessage)
					{
						messageItem[StoreObjectSchema.ItemClass] = "IPM.Note.Mobile.SMS";
					}
					messageItem.Save(SaveMode.ResolveConflicts);
					messageItem.Load();
					this.Writer.Write((itemTypeToPeople == AddressBookItemEventHandler.ItemTypeToPeople.TextMessage) ? "?ae=Item&a=Reply&t=IPM.Note.Mobile.SMS&exdltdrft=1&id=" : "?ae=Item&a=New&t=IPM.Note&exdltdrft=1&id=");
					this.Writer.Write(HttpUtility.UrlEncode(messageItem.Id.ObjectId.ToBase64String()));
				}
			}
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x000F6C78 File Offset: 0x000F4E78
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AddressBookItemEventHandler>(this);
		}

		// Token: 0x04001D2C RID: 7468
		public const string MethodNewItemToPeople = "MsgToPeople";

		// Token: 0x04001D2D RID: 7469
		public const string Id = "id";

		// Token: 0x04001D2E RID: 7470
		public const string ItemType = "it";

		// Token: 0x04001D2F RID: 7471
		protected ObjectId[] itemIds;

		// Token: 0x02000480 RID: 1152
		internal enum ItemTypeToPeople
		{
			// Token: 0x04001D31 RID: 7473
			Message,
			// Token: 0x04001D32 RID: 7474
			TextMessage,
			// Token: 0x04001D33 RID: 7475
			Meeting
		}
	}
}
