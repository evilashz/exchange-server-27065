using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.Calendaring.EntitySets;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.Calendaring
{
	// Token: 0x02000017 RID: 23
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendaringContainer : StorageEntitySetScope<IMailboxSession>, ICalendaringContainer
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00002EFF File Offset: 0x000010FF
		public CalendaringContainer(IStorageEntitySetScope<IMailboxSession> parentScope) : base(parentScope)
		{
			this.description = string.Format("{0}.Calendaring", parentScope);
			this.calendarGroups = new CalendarGroups(this, null);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002F26 File Offset: 0x00001126
		public CalendaringContainer(IStoreSession session, IXSOFactory xsoFactory = null) : this(new StorageEntitySetScope<IMailboxSession>((IMailboxSession)session, session.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid), xsoFactory ?? XSOFactory.Default, null))
		{
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002F4C File Offset: 0x0000114C
		public ICalendarGroups CalendarGroups
		{
			get
			{
				return this.calendarGroups;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002F54 File Offset: 0x00001154
		public IMailboxCalendars Calendars
		{
			get
			{
				MailboxCalendars result;
				if ((result = this.calendars) == null)
				{
					result = (this.calendars = new MailboxCalendars(this, this.calendarGroups.MyCalendars));
				}
				return result;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002F88 File Offset: 0x00001188
		public IMeetingRequestMessages MeetingRequestMessages
		{
			get
			{
				MeetingRequestMessages result;
				if ((result = this.meetingRequestMessages) == null)
				{
					result = (this.meetingRequestMessages = new MeetingRequestMessages(this, this.Calendars.Default.Events));
				}
				return result;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002FBE File Offset: 0x000011BE
		public override string ToString()
		{
			return this.description;
		}

		// Token: 0x04000039 RID: 57
		private readonly string description;

		// Token: 0x0400003A RID: 58
		private readonly CalendarGroups calendarGroups;

		// Token: 0x0400003B RID: 59
		private MailboxCalendars calendars;

		// Token: 0x0400003C RID: 60
		private MeetingRequestMessages meetingRequestMessages;
	}
}
