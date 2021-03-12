using System;
using Microsoft.Exchange.Data.Storage.VersionedXml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A5E RID: 2654
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarNotificationSchema : VersionedXmlConfigurationObjectSchema
	{
		// Token: 0x0400370A RID: 14090
		public static readonly SimpleProviderPropertyDefinition RawCalendarNotificationSettings = new SimpleProviderPropertyDefinition("RawCalendarNotificationSettings", ExchangeObjectVersion.Exchange2010, typeof(CalendarNotificationSettingsVersion1Point0), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400370B RID: 14091
		public static readonly SimpleProviderPropertyDefinition CalendarNotificationSettings = new SimpleProviderPropertyDefinition("CalendarNotificationSettings", ExchangeObjectVersion.Exchange2010, typeof(CalendarNotificationSettingsVersion1Point0), PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			CalendarNotificationSchema.RawCalendarNotificationSettings
		}, null, new GetterDelegate(CalendarNotification.CalendarNotificationSettingsGetter), new SetterDelegate(CalendarNotification.CalendarNotificationSettingsSetter));

		// Token: 0x0400370C RID: 14092
		public static readonly SimpleProviderPropertyDefinition CalendarUpdateNotification = new SimpleProviderPropertyDefinition("CalendarUpdateNotification", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			CalendarNotificationSchema.RawCalendarNotificationSettings
		}, null, new GetterDelegate(CalendarNotification.CalendarUpdateNotificationGetter), new SetterDelegate(CalendarNotification.CalendarUpdateNotificationSetter));

		// Token: 0x0400370D RID: 14093
		public static readonly SimpleProviderPropertyDefinition NextDays = new SimpleProviderPropertyDefinition("NextDays", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.Calculated, 1, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 7)
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			CalendarNotificationSchema.RawCalendarNotificationSettings
		}, null, new GetterDelegate(CalendarNotification.NextDaysGetter), new SetterDelegate(CalendarNotification.NextDaysSetter));

		// Token: 0x0400370E RID: 14094
		public static readonly SimpleProviderPropertyDefinition CalendarUpdateSendDuringWorkHour = new SimpleProviderPropertyDefinition("CalendarUpdateSendDuringWorkHour", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			CalendarNotificationSchema.RawCalendarNotificationSettings
		}, null, new GetterDelegate(CalendarNotification.CalendarUpdateSendDuringWorkHourGetter), new SetterDelegate(CalendarNotification.CalendarUpdateSendDuringWorkHourSetter));

		// Token: 0x0400370F RID: 14095
		public static readonly SimpleProviderPropertyDefinition MeetingReminderNotification = new SimpleProviderPropertyDefinition("MeetingReminderNotification", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			CalendarNotificationSchema.RawCalendarNotificationSettings
		}, null, new GetterDelegate(CalendarNotification.MeetingReminderNotificationGetter), new SetterDelegate(CalendarNotification.MeetingReminderNotificationSetter));

		// Token: 0x04003710 RID: 14096
		public static readonly SimpleProviderPropertyDefinition MeetingReminderSendDuringWorkHour = new SimpleProviderPropertyDefinition("MeetingReminderSendDuringWorkHour", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			CalendarNotificationSchema.RawCalendarNotificationSettings
		}, null, new GetterDelegate(CalendarNotification.MeetingReminderSendDuringWorkHourGetter), new SetterDelegate(CalendarNotification.MeetingReminderSendDuringWorkHourSetter));

		// Token: 0x04003711 RID: 14097
		public static readonly SimpleProviderPropertyDefinition DailyAgendaNotification = new SimpleProviderPropertyDefinition("DailyAgendaNotification", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			CalendarNotificationSchema.RawCalendarNotificationSettings
		}, null, new GetterDelegate(CalendarNotification.DailyAgendaNotificationGetter), new SetterDelegate(CalendarNotification.DailyAgendaNotificationSetter));

		// Token: 0x04003712 RID: 14098
		public static readonly SimpleProviderPropertyDefinition DailyAgendaNotificationSendTime = new SimpleProviderPropertyDefinition("DailyAgendaNotificationSendTime", ExchangeObjectVersion.Exchange2010, typeof(TimeSpan), PropertyDefinitionFlags.Calculated, TimeSpan.Zero, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<TimeSpan>(TimeSpan.Zero, TimeSpan.FromDays(1.0) - TimeSpan.FromTicks(1L))
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			CalendarNotificationSchema.RawCalendarNotificationSettings
		}, null, new GetterDelegate(CalendarNotification.DailyAgendaNotificationSendTimeGetter), new SetterDelegate(CalendarNotification.DailyAgendaNotificationSendTimeSetter));

		// Token: 0x04003713 RID: 14099
		public static readonly SimpleProviderPropertyDefinition TextMessagingPhoneNumber = new SimpleProviderPropertyDefinition("TextMessagingPhoneNumber", ExchangeObjectVersion.Exchange2010, typeof(E164Number), PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			CalendarNotificationSchema.RawCalendarNotificationSettings
		}, null, new GetterDelegate(CalendarNotification.TextMessagingPhoneNumberGetter), new SetterDelegate(CalendarNotification.TextMessagingPhoneNumberSetter));
	}
}
