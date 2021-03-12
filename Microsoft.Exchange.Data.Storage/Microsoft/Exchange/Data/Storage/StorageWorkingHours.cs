using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EDE RID: 3806
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class StorageWorkingHours
	{
		// Token: 0x06008365 RID: 33637 RVA: 0x0023B51C File Offset: 0x0023971C
		public static StorageWorkingHours LoadFrom(MailboxSession session, StoreId folderId)
		{
			WorkHoursInCalendar fromCalendar = WorkHoursInCalendar.GetFromCalendar(session, folderId);
			if (fromCalendar == null || fromCalendar.WorkHoursVersion1 == null)
			{
				return null;
			}
			if (fromCalendar.WorkHoursVersion1.TimeSlot == null)
			{
				throw new WorkingHoursXmlMalformedException(ServerStrings.NullWorkHours);
			}
			ExTimeZone exTimeZone = null;
			try
			{
				if (!string.IsNullOrEmpty(fromCalendar.WorkHoursVersion1.TimeZone.Name))
				{
					if (ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(fromCalendar.WorkHoursVersion1.TimeZone.Name, out exTimeZone))
					{
						WorkHoursTimeZone workHoursTimeZone = fromCalendar.WorkHoursVersion1.TimeZone;
						if (!workHoursTimeZone.IsSameTimeZoneInfo(TimeZoneHelper.RegTimeZoneInfoFromExTimeZone(exTimeZone)))
						{
							exTimeZone = null;
						}
					}
					else
					{
						exTimeZone = null;
					}
				}
				if (exTimeZone == null)
				{
					exTimeZone = TimeZoneHelper.CreateCustomExTimeZoneFromRegTimeZoneInfo(fromCalendar.WorkHoursVersion1.TimeZone.TimeZoneInfo, "tzone://Microsoft/Custom", "Customized Time Zone");
				}
			}
			catch (InvalidTimeZoneException ex)
			{
				throw new WorkingHoursXmlMalformedException(ServerStrings.MalformedTimeZoneWorkingHours(session.MailboxOwner.MailboxInfo.DisplayName, ex.ToString()), ex);
			}
			return new StorageWorkingHours(exTimeZone, fromCalendar.WorkHoursVersion1.WorkDays, fromCalendar.WorkHoursVersion1.TimeSlot.StartTimeInMinutes, fromCalendar.WorkHoursVersion1.TimeSlot.EndTimeInMinutes);
		}

		// Token: 0x06008366 RID: 33638 RVA: 0x0023B638 File Offset: 0x00239838
		public static StorageWorkingHours Create(ExTimeZone timeZone)
		{
			return new StorageWorkingHours(timeZone, DaysOfWeek.Weekdays, 480, 1020);
		}

		// Token: 0x06008367 RID: 33639 RVA: 0x0023B64C File Offset: 0x0023984C
		public static StorageWorkingHours Create(ExTimeZone timeZone, int daysOfWeek, int startTimeInMinutes, int endTimeInMinutes)
		{
			EnumValidator.IsValidValue<DaysOfWeek>((DaysOfWeek)daysOfWeek);
			return new StorageWorkingHours(timeZone, (DaysOfWeek)daysOfWeek, startTimeInMinutes, endTimeInMinutes);
		}

		// Token: 0x06008368 RID: 33640 RVA: 0x0023B65E File Offset: 0x0023985E
		public static bool RemoveWorkingHoursFrom(MailboxSession session, StoreId folderId)
		{
			return WorkHoursInCalendar.DeleteFromCalendar(session, folderId);
		}

		// Token: 0x170022E2 RID: 8930
		// (get) Token: 0x06008369 RID: 33641 RVA: 0x0023B667 File Offset: 0x00239867
		public int StartTimeInMinutes
		{
			get
			{
				return this.startTimeInMinutes;
			}
		}

		// Token: 0x170022E3 RID: 8931
		// (get) Token: 0x0600836A RID: 33642 RVA: 0x0023B66F File Offset: 0x0023986F
		public DaysOfWeek DaysOfWeek
		{
			get
			{
				return this.daysOfWeek;
			}
		}

		// Token: 0x0600836B RID: 33643 RVA: 0x0023B677 File Offset: 0x00239877
		public void UpdateWorkingPeriod(DaysOfWeek daysOfWeek, int startTimeInMinutes, int endTimeInMinutes)
		{
			EnumValidator.ThrowIfInvalid<DaysOfWeek>(daysOfWeek, "daysOfWeek");
			this.startTimeInMinutes = startTimeInMinutes;
			this.endTimeInMinutes = endTimeInMinutes;
			this.daysOfWeek = daysOfWeek;
		}

		// Token: 0x170022E4 RID: 8932
		// (get) Token: 0x0600836C RID: 33644 RVA: 0x0023B699 File Offset: 0x00239899
		public int EndTimeInMinutes
		{
			get
			{
				return this.endTimeInMinutes;
			}
		}

		// Token: 0x0600836D RID: 33645 RVA: 0x0023B6A4 File Offset: 0x002398A4
		public void SaveTo(MailboxSession session, StoreId folderId)
		{
			WorkHoursInCalendar workHoursInCalendar = WorkHoursInCalendar.Create(this.timeZone, (int)this.daysOfWeek, this.startTimeInMinutes, this.endTimeInMinutes);
			workHoursInCalendar.SaveToCalendar(session, folderId);
		}

		// Token: 0x0600836E RID: 33646 RVA: 0x0023B6D7 File Offset: 0x002398D7
		private StorageWorkingHours(ExTimeZone timeZone, DaysOfWeek daysOfWeek, int startTimeInMinutes, int endTimeInMinutes)
		{
			if (timeZone == null)
			{
				throw new ArgumentException("timeZone");
			}
			this.TimeZone = timeZone;
			this.daysOfWeek = daysOfWeek;
			this.startTimeInMinutes = startTimeInMinutes;
			this.endTimeInMinutes = endTimeInMinutes;
		}

		// Token: 0x170022E5 RID: 8933
		// (get) Token: 0x0600836F RID: 33647 RVA: 0x0023B70A File Offset: 0x0023990A
		// (set) Token: 0x06008370 RID: 33648 RVA: 0x0023B712 File Offset: 0x00239912
		public ExTimeZone TimeZone
		{
			get
			{
				return this.timeZone;
			}
			set
			{
				this.timeZone = value;
			}
		}

		// Token: 0x06008371 RID: 33649 RVA: 0x0023B71B File Offset: 0x0023991B
		public bool IsWorkingDay(DayOfWeek dayOfWeek)
		{
			return StorageWorkingHours.IsBitOn((int)StorageWorkingHours.ToDaysOfWeek(dayOfWeek), (int)this.daysOfWeek);
		}

		// Token: 0x06008372 RID: 33650 RVA: 0x0023B730 File Offset: 0x00239930
		public bool IsInWorkingHours(ExDateTime dateTime)
		{
			TimeSpan timeOfDay = this.TimeZone.ConvertDateTime(dateTime).TimeOfDay;
			return this.IsWorkingDay(dateTime.DayOfWeek) && timeOfDay.TotalMinutes >= (double)this.StartTimeInMinutes && timeOfDay.TotalMinutes <= (double)this.EndTimeInMinutes;
		}

		// Token: 0x06008373 RID: 33651 RVA: 0x0023B788 File Offset: 0x00239988
		public override string ToString()
		{
			return string.Format("TimeZone = {0}; WorkingPeriod: {1} {2} {3}", new object[]
			{
				this.timeZone,
				this.daysOfWeek,
				this.startTimeInMinutes,
				this.endTimeInMinutes
			});
		}

		// Token: 0x06008374 RID: 33652 RVA: 0x0023B7DA File Offset: 0x002399DA
		internal static bool IsBitOn(int val, int mask)
		{
			return (val & mask) == val;
		}

		// Token: 0x06008375 RID: 33653 RVA: 0x0023B7E4 File Offset: 0x002399E4
		internal static DaysOfWeek ToDaysOfWeek(DayOfWeek dayOfWeek)
		{
			switch (dayOfWeek)
			{
			case DayOfWeek.Sunday:
				return DaysOfWeek.Sunday;
			case DayOfWeek.Monday:
				return DaysOfWeek.Monday;
			case DayOfWeek.Tuesday:
				return DaysOfWeek.Tuesday;
			case DayOfWeek.Wednesday:
				return DaysOfWeek.Wednesday;
			case DayOfWeek.Thursday:
				return DaysOfWeek.Thursday;
			case DayOfWeek.Friday:
				return DaysOfWeek.Friday;
			case DayOfWeek.Saturday:
				return DaysOfWeek.Saturday;
			default:
				return DaysOfWeek.None;
			}
		}

		// Token: 0x040057F6 RID: 22518
		private ExTimeZone timeZone;

		// Token: 0x040057F7 RID: 22519
		private DaysOfWeek daysOfWeek;

		// Token: 0x040057F8 RID: 22520
		private int startTimeInMinutes;

		// Token: 0x040057F9 RID: 22521
		private int endTimeInMinutes;
	}
}
