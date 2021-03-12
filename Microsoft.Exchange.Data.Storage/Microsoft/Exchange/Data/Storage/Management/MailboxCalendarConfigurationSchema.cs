using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A01 RID: 2561
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxCalendarConfigurationSchema : UserConfigurationObjectSchema
	{
		// Token: 0x04003461 RID: 13409
		public static readonly SimplePropertyDefinition WorkDays = WorkingHoursSchema.WorkDays;

		// Token: 0x04003462 RID: 13410
		public static readonly SimplePropertyDefinition WorkingHoursStartTime = WorkingHoursSchema.WorkingHoursStartTime;

		// Token: 0x04003463 RID: 13411
		public static readonly SimplePropertyDefinition WorkingHoursEndTime = WorkingHoursSchema.WorkingHoursEndTime;

		// Token: 0x04003464 RID: 13412
		public static readonly SimplePropertyDefinition WorkingHoursTimeZone = WorkingHoursSchema.WorkingHoursTimeZone;

		// Token: 0x04003465 RID: 13413
		public static readonly SimplePropertyDefinition RawWeekStartDay = new SimplePropertyDefinition("rawweekstartday", ExchangeObjectVersion.Exchange2007, typeof(DayOfWeek), PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003466 RID: 13414
		public static readonly SimplePropertyDefinition WeekStartDay = new SimplePropertyDefinition("weekstartday", ExchangeObjectVersion.Exchange2007, typeof(DayOfWeek), PropertyDefinitionFlags.Calculated, DayOfWeek.Sunday, DayOfWeek.Sunday, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new SimplePropertyDefinition[]
		{
			MailboxCalendarConfigurationSchema.RawWeekStartDay
		}, null, new GetterDelegate(MailboxCalendarConfiguration.WeekStartDayGetter), new SetterDelegate(MailboxCalendarConfiguration.WeekStartDaySetter));

		// Token: 0x04003467 RID: 13415
		public static readonly SimplePropertyDefinition ShowWeekNumbers = new SimplePropertyDefinition("showweeknumbers", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003468 RID: 13416
		public static readonly SimplePropertyDefinition RawFirstWeekOfYear = new SimplePropertyDefinition("rawfirstweekofyear", ExchangeObjectVersion.Exchange2007, typeof(FirstWeekRules), PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003469 RID: 13417
		public static readonly SimplePropertyDefinition FirstWeekOfYear = new SimplePropertyDefinition("firstweekofyear", ExchangeObjectVersion.Exchange2007, typeof(FirstWeekRules), PropertyDefinitionFlags.Calculated, FirstWeekRules.FirstDay, FirstWeekRules.FirstDay, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new SimplePropertyDefinition[]
		{
			MailboxCalendarConfigurationSchema.RawFirstWeekOfYear
		}, null, new GetterDelegate(MailboxCalendarConfiguration.FirstWeekOfYearGetter), new SetterDelegate(MailboxCalendarConfiguration.FirstWeekOfYearSetter));

		// Token: 0x0400346A RID: 13418
		public static readonly SimplePropertyDefinition Language = new SimplePropertyDefinition("language", ExchangeObjectVersion.Exchange2010, typeof(CultureInfo), PropertyDefinitionFlags.None, null, null, new PropertyDefinitionConstraint[]
		{
			new DelegateConstraint(new ValidationDelegate(ConstraintDelegates.ValidateNonNeutralCulture))
		}, new PropertyDefinitionConstraint[]
		{
			new DelegateConstraint(new ValidationDelegate(ConstraintDelegates.ValidateNonNeutralCulture))
		});

		// Token: 0x0400346B RID: 13419
		public static readonly SimplePropertyDefinition TimeIncrement = new SimplePropertyDefinition("hourincrement", ExchangeObjectVersion.Exchange2007, typeof(HourIncrement), PropertyDefinitionFlags.None, HourIncrement.ThirtyMinutes, HourIncrement.ThirtyMinutes, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400346C RID: 13420
		public static readonly SimplePropertyDefinition RemindersEnabled = new SimplePropertyDefinition("enablereminders", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, true, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400346D RID: 13421
		public static readonly SimplePropertyDefinition ReminderSoundEnabled = new SimplePropertyDefinition("enableremindersound", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, true, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400346E RID: 13422
		public static readonly SimplePropertyDefinition DefaultReminderTime = new SimplePropertyDefinition("piRemindDefault", ExchangeObjectVersion.Exchange2007, typeof(TimeSpan), PropertyDefinitionFlags.None, new TimeSpan(0, 0, 15, 0), new TimeSpan(0, 0, 15, 0), PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new CalendarReminderTimeSpanConstraint()
		});

		// Token: 0x0400346F RID: 13423
		public static readonly SimplePropertyDefinition WeatherEnabled = new SimplePropertyDefinition("WeatherEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, true, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003470 RID: 13424
		public static readonly SimplePropertyDefinition WeatherUnit = new SimplePropertyDefinition("WeatherUnit", ExchangeObjectVersion.Exchange2007, typeof(WeatherTemperatureUnit), PropertyDefinitionFlags.None, WeatherTemperatureUnit.Default, WeatherTemperatureUnit.Default, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003471 RID: 13425
		public static readonly SimplePropertyDefinition WeatherLocations = new SimplePropertyDefinition("WeatherLocations", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.MultiValued, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
