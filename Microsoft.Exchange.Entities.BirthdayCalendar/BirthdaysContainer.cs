using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.BirthdayCalendar.EntitySets;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.BirthdayCalendar
{
	// Token: 0x02000010 RID: 16
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class BirthdaysContainer : StorageEntitySetScope<IMailboxSession>, IBirthdaysContainer, IStorageEntitySetScope<IMailboxSession>
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002A9F File Offset: 0x00000C9F
		public BirthdaysContainer(IStorageEntitySetScope<IMailboxSession> parentScope) : base(parentScope)
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002AA8 File Offset: 0x00000CA8
		public BirthdaysContainer(IStoreSession session, IXSOFactory xsoFactory = null) : this(new StorageEntitySetScope<IMailboxSession>((IMailboxSession)session, session.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid), xsoFactory ?? XSOFactory.Default, null))
		{
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public IBirthdayCalendars Calendars
		{
			get
			{
				BirthdayCalendars result;
				if ((result = this.calendars) == null)
				{
					result = (this.calendars = new BirthdayCalendars(this, null));
				}
				return result;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public virtual IBirthdayEvents Events
		{
			get
			{
				IBirthdayEvents result;
				if ((result = this.events) == null)
				{
					result = (this.events = new BirthdayEvents(this.Calendars));
				}
				return result;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002B24 File Offset: 0x00000D24
		public IBirthdayContacts Contacts
		{
			get
			{
				IBirthdayContacts result;
				if ((result = this.contacts) == null)
				{
					result = (this.contacts = new BirthdayContacts(this));
				}
				return result;
			}
		}

		// Token: 0x04000014 RID: 20
		private BirthdayCalendars calendars;

		// Token: 0x04000015 RID: 21
		private IBirthdayEvents events;

		// Token: 0x04000016 RID: 22
		private IBirthdayContacts contacts;
	}
}
