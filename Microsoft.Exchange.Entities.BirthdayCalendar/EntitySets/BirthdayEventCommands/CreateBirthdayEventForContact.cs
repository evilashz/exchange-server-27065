using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.BirthdayCalendar;
using Microsoft.Exchange.Entities.DataModel.BirthdayCalendar;
using Microsoft.Exchange.Entities.EntitySets.Commands;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.BirthdayCalendar.EntitySets.BirthdayEventCommands
{
	// Token: 0x02000017 RID: 23
	internal class CreateBirthdayEventForContact : EntityCommand<IBirthdayEvents, IBirthdayEvent>, IBirthdayEventCommand
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00003664 File Offset: 0x00001864
		internal CreateBirthdayEventForContact(IBirthdayContact birthdayContact, IBirthdayEvents scope)
		{
			this.Trace.TraceDebug((long)this.GetHashCode(), "CreateBirthdayEventForContact:Constructor");
			this.Contact = birthdayContact;
			this.Scope = scope;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003691 File Offset: 0x00001891
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003699 File Offset: 0x00001899
		public IBirthdayContact Contact { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000036A2 File Offset: 0x000018A2
		protected override ITracer Trace
		{
			get
			{
				return CreateBirthdayEventForContact.CreateBirthdayEventTracer;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000036AC File Offset: 0x000018AC
		public BirthdayEventCommandResult ExecuteAndGetResult()
		{
			BirthdayEventCommandResult birthdayEventCommandResult = new BirthdayEventCommandResult();
			IBirthdayEvent birthdayEvent = base.Execute(null);
			if (birthdayEvent != null)
			{
				birthdayEventCommandResult.CreatedEvents.Add(birthdayEvent);
			}
			return birthdayEventCommandResult;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000036D8 File Offset: 0x000018D8
		protected override IBirthdayEvent OnExecute()
		{
			IBirthdayEvent result;
			try
			{
				this.Scope.BirthdayEventDataProvider.BeforeStoreObjectSaved += this.OnBeforeStoreObjectSaved;
				this.Scope.BirthdayEventDataProvider.StoreObjectSaved += CreateBirthdayEventForContact.OnStoreObjectSaved;
				result = this.CreateNewBirthdayEventForContact(this.Contact);
			}
			finally
			{
				this.Scope.BirthdayEventDataProvider.StoreObjectSaved -= CreateBirthdayEventForContact.OnStoreObjectSaved;
				this.Scope.BirthdayEventDataProvider.BeforeStoreObjectSaved -= this.OnBeforeStoreObjectSaved;
			}
			return result;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003778 File Offset: 0x00001978
		private static void OnStoreObjectSaved(object sender, ICalendarItemBase calendarItemBase)
		{
			CreateBirthdayEventForContact.CreateBirthdayEventTracer.TraceDebug<ICalendarItemBase>(0L, "CreateBirthdayEventForContact::OnStoreObjectSaved: The birthday event calendar item was saved successfully: {0}", calendarItemBase);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000378C File Offset: 0x0000198C
		private void OnBeforeStoreObjectSaved(BirthdayEvent birthdayEvent, ICalendarItemBase calendarItemBase)
		{
			CreateBirthdayEventForContact.CreateBirthdayEventTracer.TraceDebug<BirthdayEvent, ICalendarItemBase>(0L, "CreateBirthdayEventForContact::OnBeforeStoreObjectSaved: The birthday event {0} was created, and the calendar item {1} will be saved", birthdayEvent, calendarItemBase);
			calendarItemBase.FreeBusyStatus = BusyType.Free;
			calendarItemBase.IsAllDayEvent = true;
			ExDateTime exDateTime = birthdayEvent.Birthday.ToUtc();
			RecurrencePattern pattern = new YearlyRecurrencePattern(exDateTime.Day, exDateTime.Month);
			int year = birthdayEvent.IsBirthYearKnown ? exDateTime.Year : ExDateTime.Today.Year;
			ExTimeZone timeZone = this.DetermineRecurrenceStartTimeZone();
			ExDateTime startDate = new ExDateTime(timeZone, year, exDateTime.Month, exDateTime.Day);
			RecurrenceRange range = new NoEndRecurrenceRange(startDate);
			ICalendarItem calendarItem = calendarItemBase as ICalendarItem;
			if (calendarItem == null)
			{
				throw new NotSupportedException("Must be able to cast base to calendar item to specify recurrence.");
			}
			calendarItem.Recurrence = new Recurrence(pattern, range);
			calendarItem.ReminderMinutesBeforeStart = 1080;
			CreateBirthdayEventForContact.CreateBirthdayEventTracer.TraceDebug<ExDateTime, ExTimeZone>(0L, "CreateBirthdayEventForContact::OnBeforeStoreObjectSaved: recurrence start date is {0}, read time zone is {1}", calendarItem.Recurrence.Range.StartDate, calendarItem.Recurrence.ReadExTimeZone);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003888 File Offset: 0x00001A88
		private ExTimeZone DetermineRecurrenceStartTimeZone()
		{
			MailboxSession mailboxSession = this.Scope.StoreSession as MailboxSession;
			return (mailboxSession == null) ? ExTimeZone.UtcTimeZone : TimeZoneHelper.GetUserTimeZone(mailboxSession);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000038B8 File Offset: 0x00001AB8
		private BirthdayEvent CreateNewBirthdayEventForContact(IBirthdayContact contact)
		{
			if (contact == null || contact.Birthday == null)
			{
				this.Trace.TraceDebug<IBirthdayContact>((long)this.GetHashCode(), "CreateBirthdayEventForContact::CreateNewBirthdayEvent: don't need to create a birthday for contact {0}", this.Contact);
				return null;
			}
			ExDateTime value = contact.Birthday.Value;
			this.Trace.TraceDebug<ExDateTime, TimeSpan>((long)this.GetHashCode(), "CreateBirthdayEventForContact::CreateNewBirthdayEvent: birthday value is {0}, time zone bias is {1}", value, value.Bias);
			BirthdayEvent birthdayEvent = new BirthdayEvent
			{
				Birthday = value,
				Subject = contact.DisplayName,
				Attribution = contact.Attribution,
				IsWritable = contact.IsWritable
			};
			IBirthdayEventInternal birthdayEventInternal = birthdayEvent;
			IBirthdayContactInternal birthdayContactInternal = (IBirthdayContactInternal)this.Contact;
			birthdayEventInternal.PersonId = birthdayContactInternal.PersonId;
			birthdayEventInternal.ContactId = StoreId.GetStoreObjectId(birthdayContactInternal.StoreId);
			return this.Scope.BirthdayEventDataProvider.CreateBirthday(birthdayEvent);
		}

		// Token: 0x0400001C RID: 28
		private static readonly ITracer CreateBirthdayEventTracer = ExTraceGlobals.CreateBirthdayEventForContactTracer;
	}
}
