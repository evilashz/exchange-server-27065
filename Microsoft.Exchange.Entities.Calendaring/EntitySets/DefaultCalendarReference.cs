using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets
{
	// Token: 0x02000034 RID: 52
	internal class DefaultCalendarReference : CalendarReference
	{
		// Token: 0x06000125 RID: 293 RVA: 0x00005AB9 File Offset: 0x00003CB9
		public DefaultCalendarReference(MailboxCalendars calendars) : base(calendars)
		{
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005AC2 File Offset: 0x00003CC2
		protected override string GetRelativeDescription()
		{
			return ".Default";
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005AC9 File Offset: 0x00003CC9
		protected override StoreId ResolveCalendarFolderId()
		{
			return base.EntitySet.StoreSession.GetDefaultFolderId(DefaultFolderType.Calendar);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005ADC File Offset: 0x00003CDC
		protected override StoreId ResolveReference()
		{
			StoreId calendarId;
			using (ICalendarGroupEntry calendarGroupEntry = base.XsoFactory.BindToCalendarGroupEntry(base.StoreSession, base.GetCalendarFolderId()))
			{
				calendarId = calendarGroupEntry.CalendarId;
			}
			return calendarId;
		}
	}
}
