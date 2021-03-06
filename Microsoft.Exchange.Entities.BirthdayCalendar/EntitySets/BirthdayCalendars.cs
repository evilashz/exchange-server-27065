using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.BirthdayCalendar;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.BirthdayCalendar.EntitySets
{
	// Token: 0x0200000E RID: 14
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BirthdayCalendars : StorageEntitySet<IBirthdayCalendars, IBirthdayCalendar, IMailboxSession>, IBirthdayCalendars, IEntitySet<IBirthdayCalendar>, IStorageEntitySetScope<IMailboxSession>
	{
		// Token: 0x06000050 RID: 80 RVA: 0x000029E6 File Offset: 0x00000BE6
		public BirthdayCalendars(IBirthdaysContainer parentScope, IEntityCommandFactory<IBirthdayCalendars, IBirthdayCalendar> commandFactory = null) : base(parentScope, "BirthdayCalendars", commandFactory)
		{
			this.ParentScope = parentScope;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000029FC File Offset: 0x00000BFC
		public CalendarFolderDataProvider CalendarFolderDataProvider
		{
			get
			{
				CalendarFolderDataProvider result;
				if ((result = this.birthdayCalendarFolderDataProvider) == null)
				{
					result = (this.birthdayCalendarFolderDataProvider = new CalendarFolderDataProvider(this, base.Session.GetDefaultFolderId(DefaultFolderType.Calendar)));
				}
				return result;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002A2E File Offset: 0x00000C2E
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002A36 File Offset: 0x00000C36
		public IBirthdaysContainer ParentScope { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002A40 File Offset: 0x00000C40
		public StoreId BirthdayCalendarFolderId
		{
			get
			{
				ExTraceGlobals.BirthdayCalendarsTracer.TraceDebug<Guid>((long)this.GetHashCode(), "BirthdayCalendars::GetBirthdayCalendarFolderId. GetDefaultFolderId. MailboxGuid:{0}", base.StoreSession.MailboxGuid);
				StoreObjectId defaultFolderId = base.StoreSession.GetDefaultFolderId(DefaultFolderType.BirthdayCalendar);
				ExTraceGlobals.BirthdayCalendarsTracer.TraceDebug<StoreObjectId, Guid>((long)this.GetHashCode(), "BirthdayCalendars::GetBirthdayCalendarFolderId. FolderId: {0} MailboxGuid:{1}", defaultFolderId, base.StoreSession.MailboxGuid);
				return defaultFolderId;
			}
		}

		// Token: 0x04000012 RID: 18
		private CalendarFolderDataProvider birthdayCalendarFolderDataProvider;
	}
}
