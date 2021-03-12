using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets
{
	// Token: 0x02000031 RID: 49
	internal class CalendarReference : StorageEntityReference<Calendars, Calendar, IMailboxSession>, ILocalCalendarReference, ICalendarReference, IEntityReference<Calendar>
	{
		// Token: 0x06000116 RID: 278 RVA: 0x0000594A File Offset: 0x00003B4A
		public CalendarReference(Calendars calendars, string calendarId) : base(calendars, calendarId)
		{
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005954 File Offset: 0x00003B54
		protected CalendarReference(Calendars calendars) : base(calendars)
		{
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000595D File Offset: 0x00003B5D
		public IEvents Events
		{
			get
			{
				return new Events(this, this);
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005966 File Offset: 0x00003B66
		public StoreId GetCalendarFolderId()
		{
			if (this.calendarFolderId == null)
			{
				this.calendarFolderId = this.ResolveCalendarFolderId();
			}
			return this.calendarFolderId;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005984 File Offset: 0x00003B84
		protected virtual StoreId ResolveCalendarFolderId()
		{
			StoreId storeId = base.GetStoreId();
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(storeId);
			if (storeObjectId.IsFolderId)
			{
				return storeId;
			}
			StoreId calendarId;
			using (ICalendarGroupEntry calendarGroupEntry = base.XsoFactory.BindToCalendarGroupEntry(base.StoreSession, storeObjectId))
			{
				calendarId = calendarGroupEntry.CalendarId;
			}
			return calendarId;
		}

		// Token: 0x04000057 RID: 87
		private StoreId calendarFolderId;
	}
}
