using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.VersionedXml;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A60 RID: 2656
	[Serializable]
	public sealed class CalendarNotification : VersionedXmlConfigurationObject
	{
		// Token: 0x17001AAA RID: 6826
		// (get) Token: 0x060060E3 RID: 24803 RVA: 0x00198D25 File Offset: 0x00196F25
		internal override XsoMailboxConfigurationObjectSchema Schema
		{
			get
			{
				return CalendarNotification.schema;
			}
		}

		// Token: 0x060060E4 RID: 24804 RVA: 0x00198D2C File Offset: 0x00196F2C
		public CalendarNotification()
		{
			((SimplePropertyBag)this.propertyBag).SetObjectIdentityPropertyDefinition(VersionedXmlConfigurationObjectSchema.Identity);
		}

		// Token: 0x17001AAB RID: 6827
		// (get) Token: 0x060060E5 RID: 24805 RVA: 0x00198D49 File Offset: 0x00196F49
		public override ObjectId Identity
		{
			get
			{
				return (ADObjectId)this[VersionedXmlConfigurationObjectSchema.Identity];
			}
		}

		// Token: 0x17001AAC RID: 6828
		// (get) Token: 0x060060E6 RID: 24806 RVA: 0x00198D5B File Offset: 0x00196F5B
		// (set) Token: 0x060060E7 RID: 24807 RVA: 0x00198D6D File Offset: 0x00196F6D
		[Parameter]
		public bool CalendarUpdateNotification
		{
			get
			{
				return (bool)this[CalendarNotificationSchema.CalendarUpdateNotification];
			}
			set
			{
				this[CalendarNotificationSchema.CalendarUpdateNotification] = value;
			}
		}

		// Token: 0x17001AAD RID: 6829
		// (get) Token: 0x060060E8 RID: 24808 RVA: 0x00198D80 File Offset: 0x00196F80
		// (set) Token: 0x060060E9 RID: 24809 RVA: 0x00198D92 File Offset: 0x00196F92
		[Parameter]
		public int NextDays
		{
			get
			{
				return (int)this[CalendarNotificationSchema.NextDays];
			}
			set
			{
				this[CalendarNotificationSchema.NextDays] = value;
			}
		}

		// Token: 0x17001AAE RID: 6830
		// (get) Token: 0x060060EA RID: 24810 RVA: 0x00198DA5 File Offset: 0x00196FA5
		// (set) Token: 0x060060EB RID: 24811 RVA: 0x00198DB7 File Offset: 0x00196FB7
		[Parameter]
		public bool CalendarUpdateSendDuringWorkHour
		{
			get
			{
				return (bool)this[CalendarNotificationSchema.CalendarUpdateSendDuringWorkHour];
			}
			set
			{
				this[CalendarNotificationSchema.CalendarUpdateSendDuringWorkHour] = value;
			}
		}

		// Token: 0x17001AAF RID: 6831
		// (get) Token: 0x060060EC RID: 24812 RVA: 0x00198DCA File Offset: 0x00196FCA
		// (set) Token: 0x060060ED RID: 24813 RVA: 0x00198DDC File Offset: 0x00196FDC
		[Parameter]
		public bool MeetingReminderNotification
		{
			get
			{
				return (bool)this[CalendarNotificationSchema.MeetingReminderNotification];
			}
			set
			{
				this[CalendarNotificationSchema.MeetingReminderNotification] = value;
			}
		}

		// Token: 0x17001AB0 RID: 6832
		// (get) Token: 0x060060EE RID: 24814 RVA: 0x00198DEF File Offset: 0x00196FEF
		// (set) Token: 0x060060EF RID: 24815 RVA: 0x00198E01 File Offset: 0x00197001
		[Parameter]
		public bool MeetingReminderSendDuringWorkHour
		{
			get
			{
				return (bool)this[CalendarNotificationSchema.MeetingReminderSendDuringWorkHour];
			}
			set
			{
				this[CalendarNotificationSchema.MeetingReminderSendDuringWorkHour] = value;
			}
		}

		// Token: 0x17001AB1 RID: 6833
		// (get) Token: 0x060060F0 RID: 24816 RVA: 0x00198E14 File Offset: 0x00197014
		// (set) Token: 0x060060F1 RID: 24817 RVA: 0x00198E26 File Offset: 0x00197026
		[Parameter]
		public bool DailyAgendaNotification
		{
			get
			{
				return (bool)this[CalendarNotificationSchema.DailyAgendaNotification];
			}
			set
			{
				this[CalendarNotificationSchema.DailyAgendaNotification] = value;
			}
		}

		// Token: 0x17001AB2 RID: 6834
		// (get) Token: 0x060060F2 RID: 24818 RVA: 0x00198E39 File Offset: 0x00197039
		// (set) Token: 0x060060F3 RID: 24819 RVA: 0x00198E4B File Offset: 0x0019704B
		[Parameter]
		public TimeSpan DailyAgendaNotificationSendTime
		{
			get
			{
				return (TimeSpan)this[CalendarNotificationSchema.DailyAgendaNotificationSendTime];
			}
			set
			{
				this[CalendarNotificationSchema.DailyAgendaNotificationSendTime] = value;
			}
		}

		// Token: 0x17001AB3 RID: 6835
		// (get) Token: 0x060060F4 RID: 24820 RVA: 0x00198E5E File Offset: 0x0019705E
		// (set) Token: 0x060060F5 RID: 24821 RVA: 0x00198E70 File Offset: 0x00197070
		internal E164Number TextMessagingPhoneNumber
		{
			get
			{
				return (E164Number)this[CalendarNotificationSchema.TextMessagingPhoneNumber];
			}
			set
			{
				this[CalendarNotificationSchema.TextMessagingPhoneNumber] = value;
			}
		}

		// Token: 0x17001AB4 RID: 6836
		// (get) Token: 0x060060F6 RID: 24822 RVA: 0x00198E7E File Offset: 0x0019707E
		// (set) Token: 0x060060F7 RID: 24823 RVA: 0x00198E90 File Offset: 0x00197090
		internal CalendarNotificationSettingsVersion1Point0 CalendarNotificationSettings
		{
			get
			{
				return (CalendarNotificationSettingsVersion1Point0)this[CalendarNotificationSchema.CalendarNotificationSettings];
			}
			set
			{
				this[CalendarNotificationSchema.CalendarNotificationSettings] = value;
			}
		}

		// Token: 0x060060F8 RID: 24824 RVA: 0x00198EA0 File Offset: 0x001970A0
		internal static object CalendarNotificationSettingsGetter(IPropertyBag propertyBag)
		{
			if (propertyBag[CalendarNotificationSchema.RawCalendarNotificationSettings] == null)
			{
				bool flag = ((PropertyBag)propertyBag).IsChanged(CalendarNotificationSchema.RawCalendarNotificationSettings);
				propertyBag[CalendarNotificationSchema.RawCalendarNotificationSettings] = new CalendarNotificationSettingsVersion1Point0(new TimeSlotMonitoringSettings(false, false, DateTime.MinValue, DateTime.MinValue + TimeSpan.FromDays(1.0) - TimeSpan.FromTicks(1L), new Duration(DurationType.Days, 1U, false, DateTime.MinValue, DateTime.MinValue + TimeSpan.FromDays(1.0) - TimeSpan.FromTicks(1L), false)), new TimeSlotMonitoringSettings(false, false, DateTime.MinValue, DateTime.MinValue + TimeSpan.FromDays(1.0) - TimeSpan.FromTicks(1L), new Duration(DurationType.Days, 1U, false, DateTime.MinValue, DateTime.MinValue + TimeSpan.FromDays(1.0) - TimeSpan.FromTicks(1L), false)), new TimePointScaningSettings(false, DateTime.MinValue + TimeSpan.FromHours(8.0), new Duration(DurationType.Days, 1U, false, DateTime.MinValue, DateTime.MinValue + TimeSpan.FromDays(1.0) - TimeSpan.FromTicks(1L), false), new Recurrence(RecurrenceType.Daily, 1U, 0U, DaysOfWeek.None, WeekOrderInMonth.None, 0U)), null);
				if (!flag)
				{
					((PropertyBag)propertyBag).ResetChangeTracking(CalendarNotificationSchema.RawCalendarNotificationSettings);
				}
			}
			return (CalendarNotificationSettingsVersion1Point0)propertyBag[CalendarNotificationSchema.RawCalendarNotificationSettings];
		}

		// Token: 0x060060F9 RID: 24825 RVA: 0x00199021 File Offset: 0x00197221
		internal static void CalendarNotificationSettingsSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[CalendarNotificationSchema.CalendarNotificationSettings] = CloneHelper.SerializeObj(value);
		}

		// Token: 0x060060FA RID: 24826 RVA: 0x00199034 File Offset: 0x00197234
		internal static object CalendarUpdateNotificationGetter(IPropertyBag propertyBag)
		{
			CalendarNotificationSettingsVersion1Point0 calendarNotificationSettingsVersion1Point = (CalendarNotificationSettingsVersion1Point0)propertyBag[CalendarNotificationSchema.CalendarNotificationSettings];
			return calendarNotificationSettingsVersion1Point.UpdateSettings.Enabled;
		}

		// Token: 0x060060FB RID: 24827 RVA: 0x00199064 File Offset: 0x00197264
		internal static void CalendarUpdateNotificationSetter(object value, IPropertyBag propertyBag)
		{
			CalendarNotificationSettingsVersion1Point0 calendarNotificationSettingsVersion1Point = (CalendarNotificationSettingsVersion1Point0)propertyBag[CalendarNotificationSchema.CalendarNotificationSettings];
			calendarNotificationSettingsVersion1Point.UpdateSettings.Enabled = (bool)value;
		}

		// Token: 0x060060FC RID: 24828 RVA: 0x00199094 File Offset: 0x00197294
		internal static object NextDaysGetter(IPropertyBag propertyBag)
		{
			CalendarNotificationSettingsVersion1Point0 calendarNotificationSettingsVersion1Point = (CalendarNotificationSettingsVersion1Point0)propertyBag[CalendarNotificationSchema.CalendarNotificationSettings];
			if (DurationType.Days != calendarNotificationSettingsVersion1Point.UpdateSettings.Duration.Type)
			{
				throw new DataValidationException(new PropertyValidationError(ServerStrings.ErrorCorruptedData(CalendarNotificationSchema.NextDays.Name), CalendarNotificationSchema.NextDays, calendarNotificationSettingsVersion1Point.UpdateSettings.Duration.Type));
			}
			return (int)calendarNotificationSettingsVersion1Point.UpdateSettings.Duration.Interval;
		}

		// Token: 0x060060FD RID: 24829 RVA: 0x00199110 File Offset: 0x00197310
		internal static void NextDaysSetter(object value, IPropertyBag propertyBag)
		{
			CalendarNotificationSettingsVersion1Point0 calendarNotificationSettingsVersion1Point = (CalendarNotificationSettingsVersion1Point0)propertyBag[CalendarNotificationSchema.CalendarNotificationSettings];
			if (DurationType.Days != calendarNotificationSettingsVersion1Point.UpdateSettings.Duration.Type)
			{
				throw new DataValidationException(new PropertyValidationError(ServerStrings.ErrorCorruptedData(CalendarNotificationSchema.NextDays.Name), CalendarNotificationSchema.NextDays, calendarNotificationSettingsVersion1Point.UpdateSettings.Duration.Type));
			}
			calendarNotificationSettingsVersion1Point.UpdateSettings.Duration.Interval = (uint)((int)value);
		}

		// Token: 0x060060FE RID: 24830 RVA: 0x0019918C File Offset: 0x0019738C
		internal static object CalendarUpdateSendDuringWorkHourGetter(IPropertyBag propertyBag)
		{
			CalendarNotificationSettingsVersion1Point0 calendarNotificationSettingsVersion1Point = (CalendarNotificationSettingsVersion1Point0)propertyBag[CalendarNotificationSchema.CalendarNotificationSettings];
			return calendarNotificationSettingsVersion1Point.UpdateSettings.Duration.NonWorkHoursExcluded;
		}

		// Token: 0x060060FF RID: 24831 RVA: 0x001991C0 File Offset: 0x001973C0
		internal static void CalendarUpdateSendDuringWorkHourSetter(object value, IPropertyBag propertyBag)
		{
			CalendarNotificationSettingsVersion1Point0 calendarNotificationSettingsVersion1Point = (CalendarNotificationSettingsVersion1Point0)propertyBag[CalendarNotificationSchema.CalendarNotificationSettings];
			calendarNotificationSettingsVersion1Point.UpdateSettings.Duration.NonWorkHoursExcluded = (bool)value;
		}

		// Token: 0x06006100 RID: 24832 RVA: 0x001991F4 File Offset: 0x001973F4
		internal static object MeetingReminderNotificationGetter(IPropertyBag propertyBag)
		{
			CalendarNotificationSettingsVersion1Point0 calendarNotificationSettingsVersion1Point = (CalendarNotificationSettingsVersion1Point0)propertyBag[CalendarNotificationSchema.CalendarNotificationSettings];
			return calendarNotificationSettingsVersion1Point.ReminderSettings.Enabled;
		}

		// Token: 0x06006101 RID: 24833 RVA: 0x00199224 File Offset: 0x00197424
		internal static void MeetingReminderNotificationSetter(object value, IPropertyBag propertyBag)
		{
			CalendarNotificationSettingsVersion1Point0 calendarNotificationSettingsVersion1Point = (CalendarNotificationSettingsVersion1Point0)propertyBag[CalendarNotificationSchema.CalendarNotificationSettings];
			calendarNotificationSettingsVersion1Point.ReminderSettings.Enabled = (bool)value;
		}

		// Token: 0x06006102 RID: 24834 RVA: 0x00199254 File Offset: 0x00197454
		internal static object MeetingReminderSendDuringWorkHourGetter(IPropertyBag propertyBag)
		{
			CalendarNotificationSettingsVersion1Point0 calendarNotificationSettingsVersion1Point = (CalendarNotificationSettingsVersion1Point0)propertyBag[CalendarNotificationSchema.CalendarNotificationSettings];
			return calendarNotificationSettingsVersion1Point.ReminderSettings.Duration.NonWorkHoursExcluded;
		}

		// Token: 0x06006103 RID: 24835 RVA: 0x00199288 File Offset: 0x00197488
		internal static void MeetingReminderSendDuringWorkHourSetter(object value, IPropertyBag propertyBag)
		{
			CalendarNotificationSettingsVersion1Point0 calendarNotificationSettingsVersion1Point = (CalendarNotificationSettingsVersion1Point0)propertyBag[CalendarNotificationSchema.CalendarNotificationSettings];
			calendarNotificationSettingsVersion1Point.ReminderSettings.Duration.NonWorkHoursExcluded = (bool)value;
		}

		// Token: 0x06006104 RID: 24836 RVA: 0x001992BC File Offset: 0x001974BC
		internal static object DailyAgendaNotificationGetter(IPropertyBag propertyBag)
		{
			CalendarNotificationSettingsVersion1Point0 calendarNotificationSettingsVersion1Point = (CalendarNotificationSettingsVersion1Point0)propertyBag[CalendarNotificationSchema.CalendarNotificationSettings];
			return calendarNotificationSettingsVersion1Point.SummarySettings.Enabled;
		}

		// Token: 0x06006105 RID: 24837 RVA: 0x001992EC File Offset: 0x001974EC
		internal static void DailyAgendaNotificationSetter(object value, IPropertyBag propertyBag)
		{
			CalendarNotificationSettingsVersion1Point0 calendarNotificationSettingsVersion1Point = (CalendarNotificationSettingsVersion1Point0)propertyBag[CalendarNotificationSchema.CalendarNotificationSettings];
			calendarNotificationSettingsVersion1Point.SummarySettings.Enabled = (bool)value;
		}

		// Token: 0x06006106 RID: 24838 RVA: 0x0019931C File Offset: 0x0019751C
		internal static object DailyAgendaNotificationSendTimeGetter(IPropertyBag propertyBag)
		{
			CalendarNotificationSettingsVersion1Point0 calendarNotificationSettingsVersion1Point = (CalendarNotificationSettingsVersion1Point0)propertyBag[CalendarNotificationSchema.CalendarNotificationSettings];
			return calendarNotificationSettingsVersion1Point.SummarySettings.NotifyingTimeInDay - calendarNotificationSettingsVersion1Point.SummarySettings.NotifyingTimeInDay.Date;
		}

		// Token: 0x06006107 RID: 24839 RVA: 0x00199364 File Offset: 0x00197564
		internal static void DailyAgendaNotificationSendTimeSetter(object value, IPropertyBag propertyBag)
		{
			CalendarNotificationSettingsVersion1Point0 calendarNotificationSettingsVersion1Point = (CalendarNotificationSettingsVersion1Point0)propertyBag[CalendarNotificationSchema.CalendarNotificationSettings];
			calendarNotificationSettingsVersion1Point.SummarySettings.NotifyingTimeInDay = DateTime.MinValue.Date + (TimeSpan)value;
		}

		// Token: 0x06006108 RID: 24840 RVA: 0x001993A8 File Offset: 0x001975A8
		internal static object TextMessagingPhoneNumberGetter(IPropertyBag propertyBag)
		{
			CalendarNotificationSettingsVersion1Point0 calendarNotificationSettingsVersion1Point = (CalendarNotificationSettingsVersion1Point0)propertyBag[CalendarNotificationSchema.CalendarNotificationSettings];
			Emitter emitter = null;
			foreach (Emitter emitter2 in calendarNotificationSettingsVersion1Point.Emitters)
			{
				if (EmitterType.TextMessaging == emitter2.Type)
				{
					emitter = emitter2;
					break;
				}
			}
			if (emitter == null || emitter.PhoneNumbers.Count == 0)
			{
				return null;
			}
			return emitter.PhoneNumbers[0];
		}

		// Token: 0x06006109 RID: 24841 RVA: 0x00199434 File Offset: 0x00197634
		internal static void TextMessagingPhoneNumberSetter(object value, IPropertyBag propertyBag)
		{
			CalendarNotificationSettingsVersion1Point0 calendarNotificationSettingsVersion1Point = (CalendarNotificationSettingsVersion1Point0)propertyBag[CalendarNotificationSchema.CalendarNotificationSettings];
			Emitter emitter = null;
			foreach (Emitter emitter2 in calendarNotificationSettingsVersion1Point.Emitters)
			{
				if (EmitterType.TextMessaging == emitter2.Type)
				{
					emitter = emitter2;
					break;
				}
			}
			if (value == null)
			{
				if (emitter != null)
				{
					calendarNotificationSettingsVersion1Point.Emitters.Remove(emitter);
					return;
				}
			}
			else
			{
				if (emitter == null)
				{
					calendarNotificationSettingsVersion1Point.Emitters.Add(new Emitter(EmitterType.TextMessaging, 0, true, new E164Number[]
					{
						(E164Number)value
					}));
					return;
				}
				emitter.PhoneNumbers.Clear();
				emitter.PhoneNumbers.Add((E164Number)value);
			}
		}

		// Token: 0x0600610A RID: 24842 RVA: 0x001994FC File Offset: 0x001976FC
		public override string ToString()
		{
			if (this.Identity != null)
			{
				return this.Identity.ToString();
			}
			return base.ToString();
		}

		// Token: 0x17001AB5 RID: 6837
		// (get) Token: 0x0600610B RID: 24843 RVA: 0x00199518 File Offset: 0x00197718
		internal override string UserConfigurationName
		{
			get
			{
				return "CalendarNotification.001";
			}
		}

		// Token: 0x17001AB6 RID: 6838
		// (get) Token: 0x0600610C RID: 24844 RVA: 0x0019951F File Offset: 0x0019771F
		internal override ProviderPropertyDefinition RawVersionedXmlPropertyDefinition
		{
			get
			{
				return CalendarNotificationSchema.RawCalendarNotificationSettings;
			}
		}

		// Token: 0x04003714 RID: 14100
		internal const string ConfigurationName = "CalendarNotification.001";

		// Token: 0x04003715 RID: 14101
		private static XsoMailboxConfigurationObjectSchema schema = ObjectSchema.GetInstance<CalendarNotificationSchema>();
	}
}
