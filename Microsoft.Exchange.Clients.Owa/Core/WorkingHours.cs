using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000294 RID: 660
	public class WorkingHours
	{
		// Token: 0x0600195E RID: 6494 RVA: 0x0009465C File Offset: 0x0009285C
		internal static WorkingHours CreateForSession(MailboxSession mailBoxSession, ExTimeZone timeZone)
		{
			if (mailBoxSession == null)
			{
				throw new ArgumentNullException("mailBoxSession");
			}
			if (timeZone == null)
			{
				throw new ArgumentNullException("timeZone");
			}
			WorkingHours result;
			if (WorkingHours.LoadFromMailbox(mailBoxSession, out result) != WorkingHours.LoadResult.Success)
			{
				result = new WorkingHours(0, 1440, 127, timeZone);
			}
			return result;
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0009469F File Offset: 0x0009289F
		public static WorkingHours CreateFromAvailabilityWorkingHours(ISessionContext sessionContext, WorkingHours availabilityWorkingHours)
		{
			if (availabilityWorkingHours != null)
			{
				return new WorkingHours(availabilityWorkingHours.StartTimeInMinutes, availabilityWorkingHours.EndTimeInMinutes, (int)availabilityWorkingHours.DaysOfWeek, availabilityWorkingHours.ExTimeZone);
			}
			return new WorkingHours(0, 1440, 127, sessionContext.TimeZone);
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x000946D8 File Offset: 0x000928D8
		internal static bool StampOnMailboxIfMissing(MailboxSession mailboxSession, string timeZoneKeyName)
		{
			WorkingHours workingHours;
			if (WorkingHours.LoadFromMailbox(mailboxSession, out workingHours) != WorkingHours.LoadResult.Missing)
			{
				return true;
			}
			ExTimeZone exTimeZone;
			if (!ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(timeZoneKeyName, out exTimeZone))
			{
				return false;
			}
			workingHours = new WorkingHours(480, 1020, 62, exTimeZone);
			return workingHours.CommitChanges(mailboxSession);
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x00094720 File Offset: 0x00092920
		private static ExDateTime GetDateTimeFromMinutes(int minutes, ExDateTime date)
		{
			return date.Date.AddMinutes((double)minutes);
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x00094740 File Offset: 0x00092940
		private static int GetMinutesFromDateTime(ExDateTime time, ExDateTime date)
		{
			return (int)(time - date.Date).TotalMinutes;
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x00094764 File Offset: 0x00092964
		private static WorkingHours.LoadResult LoadFromMailbox(MailboxSession mailboxSession, out WorkingHours workingHours)
		{
			StorageWorkingHours storageWorkingHours = null;
			try
			{
				storageWorkingHours = StorageWorkingHours.LoadFrom(mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar));
			}
			catch (AccessDeniedException)
			{
				workingHours = null;
				return WorkingHours.LoadResult.AccessDenied;
			}
			catch (ArgumentNullException)
			{
				workingHours = null;
				return WorkingHours.LoadResult.AccessDenied;
			}
			catch (ObjectNotFoundException)
			{
				workingHours = null;
				return WorkingHours.LoadResult.AccessDenied;
			}
			catch (WorkingHoursXmlMalformedException)
			{
				workingHours = null;
				return WorkingHours.LoadResult.Corrupt;
			}
			if (storageWorkingHours == null)
			{
				workingHours = null;
				return WorkingHours.LoadResult.Missing;
			}
			workingHours = new WorkingHours(storageWorkingHours.StartTimeInMinutes, storageWorkingHours.EndTimeInMinutes, (int)storageWorkingHours.DaysOfWeek, storageWorkingHours.TimeZone);
			return WorkingHours.LoadResult.Success;
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x00094804 File Offset: 0x00092A04
		private WorkingHours(int startTime, int endTime, int workDays, ExTimeZone timeZone)
		{
			this.SetWorkDayTimesInWorkingHoursTimeZone(startTime, endTime);
			this.WorkDays = workDays;
			this.TimeZone = timeZone;
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x00094823 File Offset: 0x00092A23
		public int GetWorkDayStartTime(ExDateTime day)
		{
			return this.ConvertToUserTimeZone(this.workDayStartTimeInWorkingHoursTimeZone, day);
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x00094832 File Offset: 0x00092A32
		public int GetWorkDayEndTime(ExDateTime day)
		{
			return this.ConvertToUserTimeZone(this.workDayEndTimeInWorkingHoursTimeZone, day);
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x00094841 File Offset: 0x00092A41
		public int WorkDayStartTimeInWorkingHoursTimeZone
		{
			get
			{
				return this.workDayStartTimeInWorkingHoursTimeZone;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001968 RID: 6504 RVA: 0x00094849 File Offset: 0x00092A49
		public int WorkDayEndTimeInWorkingHoursTimeZone
		{
			get
			{
				return this.workDayEndTimeInWorkingHoursTimeZone;
			}
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x00094854 File Offset: 0x00092A54
		public void SetWorkDayTimesInWorkingHoursTimeZone(int startTime, int endTime)
		{
			int num = startTime;
			int num2 = endTime;
			if (startTime < 0 || startTime > 1439 || endTime < 1 || endTime > 1440 || startTime > endTime)
			{
				num = 0;
				num2 = 1440;
			}
			if (num != this.workDayStartTimeInWorkingHoursTimeZone || num2 != this.workDayEndTimeInWorkingHoursTimeZone)
			{
				this.workDayStartTimeInWorkingHoursTimeZone = num;
				this.workDayEndTimeInWorkingHoursTimeZone = num2;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x000948A9 File Offset: 0x00092AA9
		// (set) Token: 0x0600196B RID: 6507 RVA: 0x000948B1 File Offset: 0x00092AB1
		public int WorkDays
		{
			get
			{
				return this.workDays;
			}
			set
			{
				if (value < 0 || value > 127)
				{
					value = 127;
				}
				if (value != this.workDays)
				{
					this.workDays = value;
				}
			}
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x000948D0 File Offset: 0x00092AD0
		public bool IsWorkDay(DayOfWeek day)
		{
			return (this.WorkDays >> (int)day & 1) > 0;
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600196D RID: 6509 RVA: 0x000948E2 File Offset: 0x00092AE2
		// (set) Token: 0x0600196E RID: 6510 RVA: 0x000948EA File Offset: 0x00092AEA
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

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x000948F3 File Offset: 0x00092AF3
		public bool IsTimeZoneDifferent
		{
			get
			{
				return TimeZoneHelper.RegTimeZoneInfoFromExTimeZone(OwaContext.Current.SessionContext.TimeZone) != TimeZoneHelper.RegTimeZoneInfoFromExTimeZone(this.timeZone);
			}
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x0009491C File Offset: 0x00092B1C
		internal bool CommitChanges(MailboxSession mailboxSession)
		{
			StorageWorkingHours storageWorkingHours = StorageWorkingHours.Create(this.timeZone, this.workDays, this.workDayStartTimeInWorkingHoursTimeZone, this.workDayEndTimeInWorkingHoursTimeZone);
			try
			{
				StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar);
				storageWorkingHours.SaveTo(mailboxSession, defaultFolderId);
			}
			catch (WorkingHoursSaveFailedException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x00094974 File Offset: 0x00092B74
		public WorkingHours.WorkingPeriod[] GetWorkingHoursOnDay(ExDateTime day)
		{
			List<WorkingHours.WorkingPeriod> list = new List<WorkingHours.WorkingPeriod>(2);
			this.AddWorkingPeriodForDay(list, day, -2);
			this.AddWorkingPeriodForDay(list, day, -1);
			this.AddWorkingPeriodForDay(list, day, 0);
			this.AddWorkingPeriodForDay(list, day, 1);
			this.AddWorkingPeriodForDay(list, day, 2);
			return list.ToArray();
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x000949BC File Offset: 0x00092BBC
		private void AddWorkingPeriodForDay(List<WorkingHours.WorkingPeriod> workingPeriods, ExDateTime day, int offset)
		{
			ExDateTime day2 = day.IncrementDays(offset);
			if (!this.IsWorkDay(day2.DayOfWeek))
			{
				return;
			}
			int val = this.GetWorkDayStartTime(day2) + offset * 1440;
			int val2 = this.GetWorkDayEndTime(day2) + offset * 1440;
			WorkingHours.WorkingPeriod workingPeriod = new WorkingHours.WorkingPeriod();
			workingPeriod.Start = day.AddMinutes((double)Math.Max(0, Math.Min(1440, val)));
			workingPeriod.End = day.AddMinutes((double)Math.Max(0, Math.Min(1440, val2)));
			if (workingPeriod.Start < workingPeriod.End)
			{
				workingPeriods.Add(workingPeriod);
			}
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x00094A60 File Offset: 0x00092C60
		private int ConvertToUserTimeZone(int minutesPastMidnight, ExDateTime day)
		{
			if (!this.IsTimeZoneDifferent)
			{
				return minutesPastMidnight;
			}
			ExDateTime exDateTime = WorkingHours.GetDateTimeFromMinutes(minutesPastMidnight, day.Date);
			exDateTime = new ExDateTime(this.timeZone, (DateTime)exDateTime);
			exDateTime = UserContextManager.GetUserContext().TimeZone.ConvertDateTime(exDateTime);
			return WorkingHours.GetMinutesFromDateTime(exDateTime, day.Date);
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x00094AB8 File Offset: 0x00092CB8
		public string CreateHomeTimeZoneString()
		{
			return this.TimeZone.LocalizableDisplayName.ToString(Thread.CurrentThread.CurrentUICulture);
		}

		// Token: 0x04001286 RID: 4742
		private const int MinutesPerDay = 1440;

		// Token: 0x04001287 RID: 4743
		private const int FirstTimeDefaultWorkDayStartTime = 480;

		// Token: 0x04001288 RID: 4744
		private const int FirstTimeDefaultWorkDayEndTime = 1020;

		// Token: 0x04001289 RID: 4745
		private const int FirstTimeDefaultWorkDays = 62;

		// Token: 0x0400128A RID: 4746
		private const int SessionDefaultWorkDayStartTime = 0;

		// Token: 0x0400128B RID: 4747
		private const int SessionDefaultWorkDayEndTime = 1440;

		// Token: 0x0400128C RID: 4748
		private const int SessionDefaultWorkDays = 127;

		// Token: 0x0400128D RID: 4749
		private int workDayStartTimeInWorkingHoursTimeZone;

		// Token: 0x0400128E RID: 4750
		private int workDayEndTimeInWorkingHoursTimeZone;

		// Token: 0x0400128F RID: 4751
		private int workDays;

		// Token: 0x04001290 RID: 4752
		private ExTimeZone timeZone;

		// Token: 0x02000295 RID: 661
		public class WorkingPeriod
		{
			// Token: 0x04001291 RID: 4753
			public ExDateTime Start;

			// Token: 0x04001292 RID: 4754
			public ExDateTime End;
		}

		// Token: 0x02000296 RID: 662
		private enum LoadResult
		{
			// Token: 0x04001294 RID: 4756
			Success,
			// Token: 0x04001295 RID: 4757
			Missing,
			// Token: 0x04001296 RID: 4758
			AccessDenied,
			// Token: 0x04001297 RID: 4759
			Corrupt
		}
	}
}
