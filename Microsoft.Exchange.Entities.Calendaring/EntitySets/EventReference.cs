using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets
{
	// Token: 0x02000058 RID: 88
	internal class EventReference : ItemReference<Event>, IEventReference, IItemReference<Event>, IEntityReference<Event>, ILocalCalendarReference, ICalendarReference, IEntityReference<Calendar>
	{
		// Token: 0x0600024D RID: 589 RVA: 0x00009522 File Offset: 0x00007722
		public EventReference(IStorageEntitySetScope<IStoreSession> parentScope, string eventId) : base(parentScope, eventId.AssertNotNull("eventId"))
		{
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009536 File Offset: 0x00007736
		internal EventReference(IXSOFactory xsoFactory, ICalendarItemBase calendarItem) : base(new StorageEntitySetScope<IStoreSession>(calendarItem.Session, calendarItem.Session.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid), xsoFactory, null), calendarItem.Id.AssertNotNull("calendarItem.Id"), calendarItem.Session)
		{
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000956E File Offset: 0x0000776E
		internal EventReference(IXSOFactory xsoFactory, IStoreSession session, string id) : this(new StorageEntitySetScope<IStoreSession>(session, session.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid), xsoFactory, null), id)
		{
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00009587 File Offset: 0x00007787
		public ICalendarReference Calendar
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000958C File Offset: 0x0000778C
		public IEvents Events
		{
			get
			{
				IEvents result;
				if ((result = this.events) == null)
				{
					result = (this.events = new Events(this, this));
				}
				return result;
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000095B3 File Offset: 0x000077B3
		public StoreId GetCalendarFolderId()
		{
			return base.GetContainingFolderId();
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000095BC File Offset: 0x000077BC
		Calendar IEntityReference<Calendar>.Read(CommandContext context)
		{
			IMailboxSession storeSession = (IMailboxSession)base.StoreSession;
			StorageEntitySetScope<IMailboxSession> storageEntitySetScope = new StorageEntitySetScope<IMailboxSession>(storeSession, base.RecipientSession, base.XsoFactory, null);
			string key = this.Calendar.GetKey();
			MailboxCalendars mailboxCalendars = new MailboxCalendars(storageEntitySetScope, new CalendarGroups(storageEntitySetScope, null).MyCalendars);
			return mailboxCalendars.Read(key, context);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00009610 File Offset: 0x00007810
		Event IEntityReference<Event>.Read(CommandContext context)
		{
			return this.Events.Read(base.ItemKey, context);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00009624 File Offset: 0x00007824
		string IEntityReference<Calendar>.GetKey()
		{
			return base.IdConverter.ToStringId(base.GetContainingFolderId(), base.StoreSession);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000963D File Offset: 0x0000783D
		protected override AttachmentDataProvider GetAttachmentDataProvider()
		{
			return new EventAttachmentDataProvider(this, base.IdConverter.ToStoreObjectId(base.ItemKey));
		}

		// Token: 0x040000A4 RID: 164
		private IEvents events;
	}
}
