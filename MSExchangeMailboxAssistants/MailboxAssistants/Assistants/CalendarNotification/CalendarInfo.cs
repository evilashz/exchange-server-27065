using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000DA RID: 218
	internal sealed class CalendarInfo
	{
		// Token: 0x0600092B RID: 2347 RVA: 0x0003E615 File Offset: 0x0003C815
		private CalendarInfo()
		{
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0003E61D File Offset: 0x0003C81D
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x0003E625 File Offset: 0x0003C825
		public StoreObjectId CalendarItemIdentity { get; private set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0003E62E File Offset: 0x0003C82E
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x0003E636 File Offset: 0x0003C836
		public StoreObjectId CalendarItemOccurrenceIdentity { get; private set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x0003E63F File Offset: 0x0003C83F
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x0003E647 File Offset: 0x0003C847
		public CalendarItemType CalendarItemType { get; private set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x0003E650 File Offset: 0x0003C850
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x0003E658 File Offset: 0x0003C858
		public string NormalizedSubject { get; private set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0003E661 File Offset: 0x0003C861
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x0003E669 File Offset: 0x0003C869
		public string Location { get; private set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0003E672 File Offset: 0x0003C872
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x0003E67A File Offset: 0x0003C87A
		public bool ReminderIsSet { get; private set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x0003E683 File Offset: 0x0003C883
		// (set) Token: 0x06000939 RID: 2361 RVA: 0x0003E68B File Offset: 0x0003C88B
		public ExDateTime ReminderTime { get; private set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x0003E694 File Offset: 0x0003C894
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x0003E69C File Offset: 0x0003C89C
		public int ReminderMinutesBeforeStart { get; private set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0003E6A5 File Offset: 0x0003C8A5
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x0003E6AD File Offset: 0x0003C8AD
		public bool IsVoiceReminderEnabled { get; private set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0003E6B6 File Offset: 0x0003C8B6
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x0003E6BE File Offset: 0x0003C8BE
		public string VoiceReminderPhoneNumber { get; private set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x0003E6C7 File Offset: 0x0003C8C7
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x0003E6CF File Offset: 0x0003C8CF
		public ExDateTime StartTime { get; private set; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0003E6D8 File Offset: 0x0003C8D8
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x0003E6E0 File Offset: 0x0003C8E0
		public ExDateTime EndTime { get; private set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x0003E6E9 File Offset: 0x0003C8E9
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x0003E6F1 File Offset: 0x0003C8F1
		public BusyType FreeBusyStatus { get; private set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x0003E6FA File Offset: 0x0003C8FA
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x0003E702 File Offset: 0x0003C902
		public ChangeHighlightProperties ChangeHighlight { get; private set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x0003E70B File Offset: 0x0003C90B
		// (set) Token: 0x06000949 RID: 2377 RVA: 0x0003E713 File Offset: 0x0003C913
		public AppointmentStateFlags AppointmentState { get; private set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x0003E71C File Offset: 0x0003C91C
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x0003E724 File Offset: 0x0003C924
		public ExDateTime CreationRequestTime { get; private set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0003E72D File Offset: 0x0003C92D
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x0003E735 File Offset: 0x0003C935
		public ResponseType ResponseType { get; private set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x0003E73E File Offset: 0x0003C93E
		// (set) Token: 0x0600094F RID: 2383 RVA: 0x0003E746 File Offset: 0x0003C946
		public ExDateTime? OldStartTime { get; private set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0003E74F File Offset: 0x0003C94F
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x0003E757 File Offset: 0x0003C957
		public ExDateTime? OldEndTime { get; private set; }

		// Token: 0x06000952 RID: 2386 RVA: 0x0003E760 File Offset: 0x0003C960
		public static CalendarInfo FromInterestedProperties(ExDateTime creationRequestTime, ExTimeZone timeZoneAdjustment, MailboxSession session, bool online, object[] propVals)
		{
			CalendarInfo calendarInfo = new CalendarInfo();
			calendarInfo.CreationRequestTime = creationRequestTime;
			calendarInfo.CalendarItemType = CalendarInfo.GetProperty<CalendarItemType>(propVals[8], CalendarItemType.Single);
			VersionedId property = CalendarInfo.GetProperty<VersionedId>(propVals[0], null);
			if (property != null)
			{
				StoreObjectId objectId = property.ObjectId;
				if (calendarInfo.CalendarItemType != CalendarItemType.Single && CalendarItemType.RecurringMaster != calendarInfo.CalendarItemType)
				{
					calendarInfo.CalendarItemOccurrenceIdentity = objectId;
					if (!online)
					{
						goto IL_79;
					}
					using (CalendarItemOccurrence calendarItemOccurrence = CalendarItemOccurrence.Bind(session, objectId))
					{
						calendarInfo.CalendarItemIdentity = calendarItemOccurrence.MasterId.ObjectId;
						goto IL_79;
					}
				}
				calendarInfo.CalendarItemIdentity = objectId;
			}
			IL_79:
			calendarInfo.Location = CalendarInfo.GetProperty<string>(propVals[4], string.Empty);
			calendarInfo.ReminderIsSet = CalendarInfo.GetProperty<bool>(propVals[3], false);
			calendarInfo.StartTime = CalendarInfo.GetProperty<ExDateTime>(propVals[5], ExDateTime.MinValue);
			calendarInfo.EndTime = CalendarInfo.GetProperty<ExDateTime>(propVals[6], ExDateTime.MinValue);
			calendarInfo.FreeBusyStatus = (BusyType)CalendarInfo.GetProperty<int>(propVals[7], -1);
			calendarInfo.ChangeHighlight = (ChangeHighlightProperties)CalendarInfo.GetProperty<int>(propVals[1], 0);
			calendarInfo.AppointmentState = (AppointmentStateFlags)CalendarInfo.GetProperty<int>(propVals[2], 0);
			calendarInfo.NormalizedSubject = CalendarInfo.GetProperty<string>(propVals[9], string.Empty);
			calendarInfo.ResponseType = CalendarInfo.GetProperty<ResponseType>(propVals[10], ResponseType.None);
			calendarInfo.ReminderMinutesBeforeStart = (calendarInfo.ReminderIsSet ? CalendarInfo.GetProperty<int>(propVals[11], 0) : 0);
			calendarInfo.IsVoiceReminderEnabled = CalendarInfo.GetProperty<bool>(propVals[14], false);
			calendarInfo.VoiceReminderPhoneNumber = CalendarInfo.GetProperty<string>(propVals[15], string.Empty);
			calendarInfo.ReminderTime = ((ExDateTime.MinValue < calendarInfo.StartTime) ? (calendarInfo.StartTime - TimeSpan.FromMinutes((double)calendarInfo.ReminderMinutesBeforeStart)) : ExDateTime.MinValue);
			calendarInfo.AdjustTimeZone(timeZoneAdjustment);
			return calendarInfo;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0003E918 File Offset: 0x0003CB18
		public static CalendarInfo FromMasterCalendarItemAndOccurrenceInfo(ExDateTime creationRequestTime, ExTimeZone timeZoneAdjustment, CalendarItem masterItem, OccurrenceInfo occInfo)
		{
			CalendarInfo calendarInfo = CalendarInfo.FromCalendarItemBase(creationRequestTime, timeZoneAdjustment, masterItem);
			if (occInfo != null)
			{
				calendarInfo.CalendarItemOccurrenceIdentity = ((occInfo.VersionedId == null) ? null : occInfo.VersionedId.ObjectId);
				calendarInfo.StartTime = occInfo.StartTime;
				calendarInfo.EndTime = occInfo.EndTime;
			}
			return calendarInfo;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0003E968 File Offset: 0x0003CB68
		public static CalendarInfo FromCalendarItemBase(ExDateTime creationRequestTime, ExTimeZone timeZoneAdjustment, CalendarItemBase cal)
		{
			CalendarInfo calendarInfo = new CalendarInfo();
			calendarInfo.CreationRequestTime = creationRequestTime;
			calendarInfo.CalendarItemType = cal.CalendarItemType;
			CalendarItemOccurrence calendarItemOccurrence = cal as CalendarItemOccurrence;
			if (cal.Id != null)
			{
				if (calendarInfo.CalendarItemType == CalendarItemType.Single || CalendarItemType.RecurringMaster == calendarInfo.CalendarItemType)
				{
					calendarInfo.CalendarItemIdentity = cal.Id.ObjectId;
				}
				else
				{
					calendarInfo.CalendarItemOccurrenceIdentity = cal.Id.ObjectId;
					if (calendarItemOccurrence.MasterId != null)
					{
						calendarInfo.CalendarItemIdentity = calendarItemOccurrence.MasterId.ObjectId;
					}
				}
			}
			calendarInfo.Location = cal.Location;
			calendarInfo.ReminderIsSet = cal.Reminder.IsSet;
			calendarInfo.StartTime = cal.StartTime;
			calendarInfo.EndTime = cal.EndTime;
			calendarInfo.FreeBusyStatus = cal.FreeBusyStatus;
			calendarInfo.ResponseType = cal.ResponseType;
			calendarInfo.ReminderMinutesBeforeStart = (calendarInfo.ReminderIsSet ? cal.Reminder.MinutesBeforeStart : 0);
			calendarInfo.IsVoiceReminderEnabled = cal.IsVoiceReminderEnabled;
			calendarInfo.VoiceReminderPhoneNumber = cal.VoiceReminderPhoneNumber;
			calendarInfo.ReminderTime = ((ExDateTime.MinValue < calendarInfo.StartTime) ? (calendarInfo.StartTime - TimeSpan.FromMinutes((double)calendarInfo.ReminderMinutesBeforeStart)) : ExDateTime.MinValue);
			calendarInfo.ChangeHighlight = cal.GetValueOrDefault<ChangeHighlightProperties>(CalendarItemBaseSchema.ChangeHighlight);
			calendarInfo.AppointmentState = cal.GetValueOrDefault<AppointmentStateFlags>(CalendarItemBaseSchema.AppointmentState);
			calendarInfo.NormalizedSubject = cal.GetValueOrDefault<string>(ItemSchema.NormalizedSubject);
			calendarInfo.AdjustTimeZone(timeZoneAdjustment);
			return calendarInfo;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0003EADD File Offset: 0x0003CCDD
		public static void GetOldFields(MeetingRequest mtgReq, out ExDateTime? oldStartTime, out ExDateTime? oldEndTime)
		{
			oldStartTime = mtgReq.GetValueAsNullable<ExDateTime>(MeetingRequestSchema.OldStartWhole);
			oldEndTime = mtgReq.GetValueAsNullable<ExDateTime>(MeetingRequestSchema.OldEndWhole);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0003EB01 File Offset: 0x0003CD01
		public void UpdateOldFields(ExDateTime? oldStartTime, ExDateTime? oldEndTime)
		{
			this.OldStartTime = oldStartTime;
			this.OldEndTime = oldEndTime;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0003EB14 File Offset: 0x0003CD14
		public void UpdateOldFields(MeetingRequest mtgReq)
		{
			ExDateTime? oldStartTime = null;
			ExDateTime? oldEndTime = null;
			CalendarInfo.GetOldFields(mtgReq, out oldStartTime, out oldEndTime);
			this.UpdateOldFields(oldStartTime, oldEndTime);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0003EB44 File Offset: 0x0003CD44
		public void CopyFrom(CalendarInfo other)
		{
			this.CalendarItemIdentity = other.CalendarItemIdentity;
			this.CalendarItemOccurrenceIdentity = other.CalendarItemOccurrenceIdentity;
			this.CalendarItemType = other.CalendarItemType;
			this.Location = other.Location;
			this.ReminderIsSet = other.ReminderIsSet;
			this.ReminderTime = other.ReminderTime;
			this.StartTime = other.StartTime;
			this.EndTime = other.EndTime;
			this.FreeBusyStatus = other.FreeBusyStatus;
			this.ChangeHighlight = other.ChangeHighlight;
			this.AppointmentState = other.AppointmentState;
			this.CreationRequestTime = other.CreationRequestTime;
			this.NormalizedSubject = other.NormalizedSubject;
			this.ResponseType = other.ResponseType;
			this.OldStartTime = other.OldStartTime;
			this.OldEndTime = other.OldEndTime;
			this.IsVoiceReminderEnabled = other.IsVoiceReminderEnabled;
			this.VoiceReminderPhoneNumber = other.VoiceReminderPhoneNumber;
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0003EC2C File Offset: 0x0003CE2C
		public bool IsInteresting(CalendarNotificationType type)
		{
			switch (type)
			{
			case CalendarNotificationType.Summary:
			case CalendarNotificationType.Reminder:
				return ResponseType.Decline != this.ResponseType && AppointmentStateFlags.None == (AppointmentStateFlags.Cancelled & this.AppointmentState);
			case CalendarNotificationType.NewUpdate:
			case CalendarNotificationType.ChangedUpdate:
				return true;
			case CalendarNotificationType.DeletedUpdate:
				return ResponseType.Decline != this.ResponseType;
			default:
				return false;
			}
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0003EC80 File Offset: 0x0003CE80
		private static T GetProperty<T>(object propVal, T defVal)
		{
			if (!(propVal is T))
			{
				return defVal;
			}
			return (T)((object)propVal);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0003EC94 File Offset: 0x0003CE94
		private void AdjustTimeZone(ExTimeZone timeZone)
		{
			this.ReminderTime = timeZone.ConvertDateTime(this.ReminderTime);
			this.StartTime = timeZone.ConvertDateTime(this.StartTime);
			this.EndTime = timeZone.ConvertDateTime(this.EndTime);
			this.CreationRequestTime = timeZone.ConvertDateTime(this.CreationRequestTime);
		}

		// Token: 0x0400062D RID: 1581
		internal static readonly PropertyDefinition[] InterestedProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			CalendarItemBaseSchema.ChangeHighlight,
			CalendarItemBaseSchema.AppointmentState,
			ItemSchema.ReminderIsSet,
			CalendarItemBaseSchema.Location,
			CalendarItemInstanceSchema.StartTime,
			CalendarItemInstanceSchema.EndTime,
			CalendarItemBaseSchema.FreeBusyStatus,
			CalendarItemBaseSchema.CalendarItemType,
			ItemSchema.NormalizedSubject,
			CalendarItemBaseSchema.ResponseType,
			ItemSchema.ReminderMinutesBeforeStart,
			MeetingRequestSchema.OldStartWhole,
			MeetingRequestSchema.OldEndWhole,
			ItemSchema.IsVoiceReminderEnabled,
			ItemSchema.VoiceReminderPhoneNumber
		};

		// Token: 0x020000DB RID: 219
		internal enum InterestedPropertyIndex
		{
			// Token: 0x04000642 RID: 1602
			Id,
			// Token: 0x04000643 RID: 1603
			ChangeHighlight,
			// Token: 0x04000644 RID: 1604
			AppointmentState,
			// Token: 0x04000645 RID: 1605
			ReminderIsSet,
			// Token: 0x04000646 RID: 1606
			Location,
			// Token: 0x04000647 RID: 1607
			StartTime,
			// Token: 0x04000648 RID: 1608
			EndTime,
			// Token: 0x04000649 RID: 1609
			FreeBusyStatus,
			// Token: 0x0400064A RID: 1610
			CalendarItemType,
			// Token: 0x0400064B RID: 1611
			NormalizedSubject,
			// Token: 0x0400064C RID: 1612
			ResponseType,
			// Token: 0x0400064D RID: 1613
			ReminderMinutesBeforeStart,
			// Token: 0x0400064E RID: 1614
			OldStartWhole,
			// Token: 0x0400064F RID: 1615
			OldEndWhole,
			// Token: 0x04000650 RID: 1616
			IsVoiceReminderEnabled,
			// Token: 0x04000651 RID: 1617
			VoiceReminderPhoneNumber
		}
	}
}
