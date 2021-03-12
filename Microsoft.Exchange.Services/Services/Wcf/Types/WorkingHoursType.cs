using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A3F RID: 2623
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class WorkingHoursType
	{
		// Token: 0x06004A10 RID: 18960 RVA: 0x00103188 File Offset: 0x00101388
		internal WorkingHoursType(int startTime, int endTime, int workDays, ExTimeZone displayTimeZone, ExTimeZone originalTimeZone)
		{
			this.displayTimeZone = displayTimeZone;
			this.originalTimeZone = originalTimeZone;
			this.WorkHoursStartTimeInMinutes = this.ConvertToDisplayTimeZone(startTime, ExDateTime.Now);
			this.WorkHoursEndTimeInMinutes = this.ConvertToDisplayTimeZone(endTime, ExDateTime.Now);
			this.WorkDays = workDays;
		}

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x06004A11 RID: 18961 RVA: 0x001031D6 File Offset: 0x001013D6
		// (set) Token: 0x06004A12 RID: 18962 RVA: 0x001031DE File Offset: 0x001013DE
		[DataMember]
		public int WorkHoursStartTimeInMinutes
		{
			get
			{
				return this.workHoursStartTimeInMinutes;
			}
			private set
			{
				this.workHoursStartTimeInMinutes = value;
			}
		}

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x06004A13 RID: 18963 RVA: 0x001031E7 File Offset: 0x001013E7
		// (set) Token: 0x06004A14 RID: 18964 RVA: 0x001031EF File Offset: 0x001013EF
		[DataMember]
		public int WorkHoursEndTimeInMinutes
		{
			get
			{
				return this.workHoursEndTimeInMinutes;
			}
			private set
			{
				this.workHoursEndTimeInMinutes = value;
			}
		}

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x06004A15 RID: 18965 RVA: 0x001031F8 File Offset: 0x001013F8
		// (set) Token: 0x06004A16 RID: 18966 RVA: 0x00103200 File Offset: 0x00101400
		[DataMember]
		public int WorkDays
		{
			get
			{
				return this.workDays;
			}
			private set
			{
				this.workDays = value;
			}
		}

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x06004A17 RID: 18967 RVA: 0x00103209 File Offset: 0x00101409
		public ExTimeZone WorkingHoursTimeZone
		{
			get
			{
				return this.originalTimeZone;
			}
		}

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x06004A18 RID: 18968 RVA: 0x00103211 File Offset: 0x00101411
		public bool IsTimeZoneDifferent
		{
			get
			{
				return this.displayTimeZone != this.originalTimeZone && TimeZoneHelper.RegTimeZoneInfoFromExTimeZone(this.displayTimeZone) != TimeZoneHelper.RegTimeZoneInfoFromExTimeZone(this.originalTimeZone);
			}
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x00103240 File Offset: 0x00101440
		internal static WorkingHoursType Load(MailboxSession mailboxSession, string displayTimeZoneKey)
		{
			if (string.IsNullOrEmpty(displayTimeZoneKey))
			{
				throw new ArgumentException("mailboxTimeZoneKey");
			}
			ExTimeZone exTimeZone = null;
			if (!ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(displayTimeZoneKey, out exTimeZone))
			{
				ExTraceGlobals.UserOptionsTracer.TraceError<string>(0L, "Failed to resolve target time zone in a call to WorkingHoursType.Load. {0}. - Will revert to default working hours.", displayTimeZoneKey);
				return WorkingHoursType.GetDefaultWorkingHoursInTimeZone(ExTimeZone.UtcTimeZone);
			}
			StorageWorkingHours storageWorkingHours;
			WorkingHoursType.LoadResult loadResult = WorkingHoursType.LoadInternal(mailboxSession, out storageWorkingHours);
			WorkingHoursType workingHoursType;
			if (loadResult != WorkingHoursType.LoadResult.Success)
			{
				ExTraceGlobals.UserOptionsTracer.TraceDebug<IExchangePrincipal>(0L, "Could not retrieve working hours - returning defaults instead.User {0}", mailboxSession.MailboxOwner);
				workingHoursType = WorkingHoursType.GetDefaultWorkingHoursInTimeZone(exTimeZone);
				if (loadResult == WorkingHoursType.LoadResult.Corrupt)
				{
					WorkingHoursType.DeleteWorkingHoursMessage(mailboxSession);
				}
				if (loadResult == WorkingHoursType.LoadResult.Missing || loadResult == WorkingHoursType.LoadResult.Corrupt)
				{
					ExTraceGlobals.UserOptionsTracer.TraceDebug<IExchangePrincipal>(0L, "Working hours are missing or corrupted. Performing recovery. User:{0}", mailboxSession.MailboxOwner);
					workingHoursType.CommitChanges(mailboxSession);
				}
			}
			else
			{
				workingHoursType = new WorkingHoursType(storageWorkingHours.StartTimeInMinutes, storageWorkingHours.EndTimeInMinutes, (int)storageWorkingHours.DaysOfWeek, exTimeZone, storageWorkingHours.TimeZone ?? exTimeZone);
			}
			return workingHoursType;
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x00103310 File Offset: 0x00101510
		internal static WorkingHoursType GetDefaultWorkingHoursInTimeZone(ExTimeZone timeZone)
		{
			return new WorkingHoursType(480, 1020, 62, timeZone, timeZone);
		}

		// Token: 0x06004A1B RID: 18971 RVA: 0x00103328 File Offset: 0x00101528
		internal static void MoveWorkingHoursToTimeZone(MailboxSession mailboxSession, ExTimeZone newTimeZone)
		{
			StorageWorkingHours storageWorkingHours = null;
			if (WorkingHoursType.LoadInternal(mailboxSession, out storageWorkingHours) == WorkingHoursType.LoadResult.Success)
			{
				storageWorkingHours.TimeZone = newTimeZone;
				storageWorkingHours.SaveTo(mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar));
			}
		}

		// Token: 0x06004A1C RID: 18972 RVA: 0x00103358 File Offset: 0x00101558
		private static void DeleteWorkingHoursMessage(MailboxSession mailboxSession)
		{
			try
			{
				ExTraceGlobals.UserOptionsTracer.TraceDebug<IExchangePrincipal>(0L, "Working hours are corrupted. Deleting old message before saving new working hours. User:{0}", mailboxSession.MailboxOwner);
				StorageWorkingHours.RemoveWorkingHoursFrom(mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar));
			}
			catch (StorageTransientException arg)
			{
				ExTraceGlobals.UserOptionsTracer.TraceError<IExchangePrincipal, StorageTransientException>(0L, "Issues while trying to delete working hours message. StorageTransient. User:{0} Exception:{1}", mailboxSession.MailboxOwner, arg);
			}
			catch (StoragePermanentException arg2)
			{
				ExTraceGlobals.UserOptionsTracer.TraceError<IExchangePrincipal, StoragePermanentException>(0L, "Issues while trying to delete working hours message. StoragePermanent. User:{0} Exception:{1}", mailboxSession.MailboxOwner, arg2);
			}
		}

		// Token: 0x06004A1D RID: 18973 RVA: 0x001033E0 File Offset: 0x001015E0
		private static WorkingHoursType.LoadResult LoadInternal(MailboxSession mailboxSession, out StorageWorkingHours workingHours)
		{
			workingHours = null;
			try
			{
				workingHours = StorageWorkingHours.LoadFrom(mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar));
			}
			catch (AccessDeniedException ex)
			{
				ExTraceGlobals.UserOptionsTracer.TraceError<string>(0L, "AccessDenied in a call of loading working hours : {0}", ex.Message);
				return WorkingHoursType.LoadResult.AccessDenied;
			}
			catch (ArgumentNullException ex2)
			{
				ExTraceGlobals.UserOptionsTracer.TraceError<string>(0L, "Argument exception in a call of loading working hours : {0}", ex2.Message);
				return WorkingHoursType.LoadResult.AccessDenied;
			}
			catch (ObjectNotFoundException ex3)
			{
				ExTraceGlobals.UserOptionsTracer.TraceError<string>(0L, "ObjectNotFoundException exception in a call of loading working hours : {0}", ex3.Message);
				return WorkingHoursType.LoadResult.AccessDenied;
			}
			catch (WorkingHoursXmlMalformedException ex4)
			{
				ExTraceGlobals.UserOptionsTracer.TraceError<string>(0L, "WorkingHoursXmlMalformedException exception in a call of loading working hours : {0}", ex4.Message);
				return WorkingHoursType.LoadResult.Corrupt;
			}
			catch (CorruptDataException ex5)
			{
				ExTraceGlobals.UserOptionsTracer.TraceError<string>(0L, "CorruptDataException exception in a call of loading working hours : {0}", ex5.Message);
				return WorkingHoursType.LoadResult.Corrupt;
			}
			if (workingHours == null)
			{
				return WorkingHoursType.LoadResult.Missing;
			}
			if (!WorkingHoursType.ValidateWorkingHours(workingHours.StartTimeInMinutes, workingHours.EndTimeInMinutes))
			{
				return WorkingHoursType.LoadResult.Corrupt;
			}
			return WorkingHoursType.LoadResult.Success;
		}

		// Token: 0x06004A1E RID: 18974 RVA: 0x001034FC File Offset: 0x001016FC
		private static bool ValidateWorkingHours(int startTime, int endTime)
		{
			return startTime >= 0 && startTime <= 1439 && endTime >= 1 && endTime <= 1440;
		}

		// Token: 0x06004A1F RID: 18975 RVA: 0x0010351C File Offset: 0x0010171C
		private static ExDateTime GetDateTimeFromMinutes(int minutes, ExDateTime date)
		{
			return date.Date.AddMinutes((double)minutes);
		}

		// Token: 0x06004A20 RID: 18976 RVA: 0x0010353C File Offset: 0x0010173C
		private static int GetMinutesFromDateTime(DateTime time, DateTime date)
		{
			return (int)(time - date.Date).TotalMinutes;
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x00103560 File Offset: 0x00101760
		private int ConvertToDisplayTimeZone(int minutesPastMidnight, ExDateTime day)
		{
			if (!this.IsTimeZoneDifferent)
			{
				return minutesPastMidnight;
			}
			ExDateTime dateTimeFromMinutes = WorkingHoursType.GetDateTimeFromMinutes(minutesPastMidnight, day.Date);
			dateTimeFromMinutes = new ExDateTime(this.originalTimeZone, (DateTime)dateTimeFromMinutes);
			ExDateTime exDateTime = this.displayTimeZone.ConvertDateTime(dateTimeFromMinutes);
			return WorkingHoursType.GetMinutesFromDateTime((DateTime)exDateTime, (DateTime)day.Date);
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x001035BC File Offset: 0x001017BC
		private bool CommitChanges(MailboxSession mailboxSession)
		{
			StorageWorkingHours storageWorkingHours = StorageWorkingHours.Create(this.displayTimeZone, this.WorkDays, this.workHoursStartTimeInMinutes, this.WorkHoursEndTimeInMinutes);
			try
			{
				StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar);
				storageWorkingHours.SaveTo(mailboxSession, defaultFolderId);
			}
			catch (WorkingHoursSaveFailedException ex)
			{
				ExTraceGlobals.UserOptionsTracer.TraceError<string>(0L, "Failed to save working hours in WorkingHour.Commit call: {0}", ex.Message);
				return false;
			}
			return true;
		}

		// Token: 0x04002A41 RID: 10817
		private const int FirstTimeDefaultWorkDayStartTime = 480;

		// Token: 0x04002A42 RID: 10818
		private const int FirstTimeDefaultWorkDayEndTime = 1020;

		// Token: 0x04002A43 RID: 10819
		private const int FirstTimeDefaultWorkDays = 62;

		// Token: 0x04002A44 RID: 10820
		private int workHoursStartTimeInMinutes;

		// Token: 0x04002A45 RID: 10821
		private int workHoursEndTimeInMinutes;

		// Token: 0x04002A46 RID: 10822
		private int workDays;

		// Token: 0x04002A47 RID: 10823
		private ExTimeZone displayTimeZone;

		// Token: 0x04002A48 RID: 10824
		private ExTimeZone originalTimeZone;

		// Token: 0x02000A40 RID: 2624
		private enum LoadResult
		{
			// Token: 0x04002A4A RID: 10826
			Success,
			// Token: 0x04002A4B RID: 10827
			Missing,
			// Token: 0x04002A4C RID: 10828
			AccessDenied,
			// Token: 0x04002A4D RID: 10829
			Corrupt
		}
	}
}
