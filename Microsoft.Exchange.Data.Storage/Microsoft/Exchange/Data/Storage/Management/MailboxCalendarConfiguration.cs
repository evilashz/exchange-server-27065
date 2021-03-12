using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A02 RID: 2562
	[Serializable]
	public class MailboxCalendarConfiguration : UserConfigurationObject, IWorkingHours
	{
		// Token: 0x170019C1 RID: 6593
		// (get) Token: 0x06005DDC RID: 24028 RVA: 0x0018CFFC File Offset: 0x0018B1FC
		internal override UserConfigurationObjectSchema Schema
		{
			get
			{
				return MailboxCalendarConfiguration.schema;
			}
		}

		// Token: 0x170019C2 RID: 6594
		// (get) Token: 0x06005DDE RID: 24030 RVA: 0x0018D00B File Offset: 0x0018B20B
		// (set) Token: 0x06005DDF RID: 24031 RVA: 0x0018D01D File Offset: 0x0018B21D
		[Parameter(Mandatory = false)]
		public DaysOfWeek WorkDays
		{
			get
			{
				return (DaysOfWeek)this[MailboxCalendarConfigurationSchema.WorkDays];
			}
			set
			{
				this[MailboxCalendarConfigurationSchema.WorkDays] = value;
			}
		}

		// Token: 0x170019C3 RID: 6595
		// (get) Token: 0x06005DE0 RID: 24032 RVA: 0x0018D030 File Offset: 0x0018B230
		// (set) Token: 0x06005DE1 RID: 24033 RVA: 0x0018D042 File Offset: 0x0018B242
		[Parameter(Mandatory = false)]
		public TimeSpan WorkingHoursStartTime
		{
			get
			{
				return (TimeSpan)this[MailboxCalendarConfigurationSchema.WorkingHoursStartTime];
			}
			set
			{
				this[MailboxCalendarConfigurationSchema.WorkingHoursStartTime] = value;
			}
		}

		// Token: 0x170019C4 RID: 6596
		// (get) Token: 0x06005DE2 RID: 24034 RVA: 0x0018D055 File Offset: 0x0018B255
		// (set) Token: 0x06005DE3 RID: 24035 RVA: 0x0018D067 File Offset: 0x0018B267
		[Parameter(Mandatory = false)]
		public TimeSpan WorkingHoursEndTime
		{
			get
			{
				return (TimeSpan)this[MailboxCalendarConfigurationSchema.WorkingHoursEndTime];
			}
			set
			{
				this[MailboxCalendarConfigurationSchema.WorkingHoursEndTime] = value;
			}
		}

		// Token: 0x170019C5 RID: 6597
		// (get) Token: 0x06005DE4 RID: 24036 RVA: 0x0018D07A File Offset: 0x0018B27A
		// (set) Token: 0x06005DE5 RID: 24037 RVA: 0x0018D08C File Offset: 0x0018B28C
		[Parameter(Mandatory = false)]
		public ExTimeZoneValue WorkingHoursTimeZone
		{
			get
			{
				return (ExTimeZoneValue)this[MailboxCalendarConfigurationSchema.WorkingHoursTimeZone];
			}
			set
			{
				this[MailboxCalendarConfigurationSchema.WorkingHoursTimeZone] = value;
			}
		}

		// Token: 0x170019C6 RID: 6598
		// (get) Token: 0x06005DE6 RID: 24038 RVA: 0x0018D09A File Offset: 0x0018B29A
		// (set) Token: 0x06005DE7 RID: 24039 RVA: 0x0018D0AC File Offset: 0x0018B2AC
		[Parameter(Mandatory = false)]
		public DayOfWeek WeekStartDay
		{
			get
			{
				return (DayOfWeek)this[MailboxCalendarConfigurationSchema.WeekStartDay];
			}
			set
			{
				this[MailboxCalendarConfigurationSchema.WeekStartDay] = value;
			}
		}

		// Token: 0x170019C7 RID: 6599
		// (get) Token: 0x06005DE8 RID: 24040 RVA: 0x0018D0BF File Offset: 0x0018B2BF
		// (set) Token: 0x06005DE9 RID: 24041 RVA: 0x0018D0D1 File Offset: 0x0018B2D1
		[Parameter(Mandatory = false)]
		public bool ShowWeekNumbers
		{
			get
			{
				return (bool)this[MailboxCalendarConfigurationSchema.ShowWeekNumbers];
			}
			set
			{
				this[MailboxCalendarConfigurationSchema.ShowWeekNumbers] = value;
			}
		}

		// Token: 0x170019C8 RID: 6600
		// (get) Token: 0x06005DEA RID: 24042 RVA: 0x0018D0E4 File Offset: 0x0018B2E4
		// (set) Token: 0x06005DEB RID: 24043 RVA: 0x0018D0F6 File Offset: 0x0018B2F6
		[Parameter(Mandatory = false)]
		public FirstWeekRules FirstWeekOfYear
		{
			get
			{
				return (FirstWeekRules)this[MailboxCalendarConfigurationSchema.FirstWeekOfYear];
			}
			set
			{
				this[MailboxCalendarConfigurationSchema.FirstWeekOfYear] = value;
			}
		}

		// Token: 0x170019C9 RID: 6601
		// (get) Token: 0x06005DEC RID: 24044 RVA: 0x0018D109 File Offset: 0x0018B309
		// (set) Token: 0x06005DED RID: 24045 RVA: 0x0018D11B File Offset: 0x0018B31B
		[Parameter(Mandatory = false)]
		public HourIncrement TimeIncrement
		{
			get
			{
				return (HourIncrement)this[MailboxCalendarConfigurationSchema.TimeIncrement];
			}
			set
			{
				this[MailboxCalendarConfigurationSchema.TimeIncrement] = value;
			}
		}

		// Token: 0x170019CA RID: 6602
		// (get) Token: 0x06005DEE RID: 24046 RVA: 0x0018D12E File Offset: 0x0018B32E
		// (set) Token: 0x06005DEF RID: 24047 RVA: 0x0018D140 File Offset: 0x0018B340
		[Parameter(Mandatory = false)]
		public bool RemindersEnabled
		{
			get
			{
				return (bool)this[MailboxCalendarConfigurationSchema.RemindersEnabled];
			}
			set
			{
				this[MailboxCalendarConfigurationSchema.RemindersEnabled] = value;
			}
		}

		// Token: 0x170019CB RID: 6603
		// (get) Token: 0x06005DF0 RID: 24048 RVA: 0x0018D153 File Offset: 0x0018B353
		// (set) Token: 0x06005DF1 RID: 24049 RVA: 0x0018D165 File Offset: 0x0018B365
		[Parameter(Mandatory = false)]
		public bool ReminderSoundEnabled
		{
			get
			{
				return (bool)this[MailboxCalendarConfigurationSchema.ReminderSoundEnabled];
			}
			set
			{
				this[MailboxCalendarConfigurationSchema.ReminderSoundEnabled] = value;
			}
		}

		// Token: 0x170019CC RID: 6604
		// (get) Token: 0x06005DF2 RID: 24050 RVA: 0x0018D178 File Offset: 0x0018B378
		// (set) Token: 0x06005DF3 RID: 24051 RVA: 0x0018D18A File Offset: 0x0018B38A
		[Parameter(Mandatory = false)]
		public TimeSpan DefaultReminderTime
		{
			get
			{
				return (TimeSpan)this[MailboxCalendarConfigurationSchema.DefaultReminderTime];
			}
			set
			{
				this[MailboxCalendarConfigurationSchema.DefaultReminderTime] = value;
			}
		}

		// Token: 0x170019CD RID: 6605
		// (get) Token: 0x06005DF4 RID: 24052 RVA: 0x0018D19D File Offset: 0x0018B39D
		// (set) Token: 0x06005DF5 RID: 24053 RVA: 0x0018D1AF File Offset: 0x0018B3AF
		[Parameter(Mandatory = false)]
		public bool WeatherEnabled
		{
			get
			{
				return (bool)this[MailboxCalendarConfigurationSchema.WeatherEnabled];
			}
			set
			{
				this[MailboxCalendarConfigurationSchema.WeatherEnabled] = value;
			}
		}

		// Token: 0x170019CE RID: 6606
		// (get) Token: 0x06005DF6 RID: 24054 RVA: 0x0018D1C2 File Offset: 0x0018B3C2
		// (set) Token: 0x06005DF7 RID: 24055 RVA: 0x0018D1D4 File Offset: 0x0018B3D4
		[Parameter(Mandatory = false)]
		public WeatherTemperatureUnit WeatherUnit
		{
			get
			{
				return (WeatherTemperatureUnit)this[MailboxCalendarConfigurationSchema.WeatherUnit];
			}
			set
			{
				this[MailboxCalendarConfigurationSchema.WeatherUnit] = value;
			}
		}

		// Token: 0x170019CF RID: 6607
		// (get) Token: 0x06005DF8 RID: 24056 RVA: 0x0018D1E7 File Offset: 0x0018B3E7
		// (set) Token: 0x06005DF9 RID: 24057 RVA: 0x0018D1F9 File Offset: 0x0018B3F9
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> WeatherLocations
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxCalendarConfigurationSchema.WeatherLocations];
			}
			set
			{
				this[MailboxCalendarConfigurationSchema.WeatherLocations] = value;
			}
		}

		// Token: 0x06005DFA RID: 24058 RVA: 0x0018D207 File Offset: 0x0018B407
		internal static void WeekStartDaySetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[MailboxCalendarConfigurationSchema.RawWeekStartDay] = value;
		}

		// Token: 0x06005DFB RID: 24059 RVA: 0x0018D218 File Offset: 0x0018B418
		internal static object WeekStartDayGetter(IPropertyBag propertyBag)
		{
			object obj = propertyBag[MailboxCalendarConfigurationSchema.RawWeekStartDay];
			if (obj != null && obj is DayOfWeek)
			{
				DayOfWeek dayOfWeek = (DayOfWeek)obj;
				if (dayOfWeek >= DayOfWeek.Sunday && dayOfWeek <= DayOfWeek.Saturday)
				{
					return dayOfWeek;
				}
			}
			CultureInfo cultureInfo = (CultureInfo)propertyBag[MailboxCalendarConfigurationSchema.Language];
			if (cultureInfo != null)
			{
				return (DayOfWeek)cultureInfo.DateTimeFormat.FirstDayOfWeek;
			}
			return DayOfWeek.Sunday;
		}

		// Token: 0x06005DFC RID: 24060 RVA: 0x0018D27D File Offset: 0x0018B47D
		internal static void FirstWeekOfYearSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[MailboxCalendarConfigurationSchema.RawFirstWeekOfYear] = value;
		}

		// Token: 0x06005DFD RID: 24061 RVA: 0x0018D28C File Offset: 0x0018B48C
		internal static object FirstWeekOfYearGetter(IPropertyBag propertyBag)
		{
			object obj = propertyBag[MailboxCalendarConfigurationSchema.RawFirstWeekOfYear];
			if (obj != null && obj is FirstWeekRules)
			{
				FirstWeekRules firstWeekRules = (FirstWeekRules)obj;
				if (firstWeekRules >= FirstWeekRules.LegacyNotSet && firstWeekRules <= FirstWeekRules.FirstFullWeek)
				{
					return firstWeekRules;
				}
			}
			CultureInfo cultureInfo = (CultureInfo)propertyBag[MailboxCalendarConfigurationSchema.Language];
			if (cultureInfo != null)
			{
				return cultureInfo.DateTimeFormat.CalendarWeekRule.ToFirstWeekRules();
			}
			return FirstWeekRules.FirstDay;
		}

		// Token: 0x06005DFE RID: 24062 RVA: 0x0018D2F6 File Offset: 0x0018B4F6
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (this.WorkingHoursStartTime > this.WorkingHoursEndTime)
			{
				errors.Add(new ObjectValidationError(ServerStrings.ErrorWorkingHoursEndTimeSmaller, this.Identity, string.Empty));
			}
		}

		// Token: 0x06005DFF RID: 24063 RVA: 0x0018D330 File Offset: 0x0018B530
		public override IConfigurable Read(MailboxStoreTypeProvider session, ObjectId identity)
		{
			base.Principal = ExchangePrincipal.FromADUser(session.ADUser, null);
			using (WorkingHoursAdapter<MailboxCalendarConfiguration> workingHoursAdapter = new WorkingHoursAdapter<MailboxCalendarConfiguration>(session.MailboxSession))
			{
				using (UserConfigurationDictionaryAdapter<MailboxCalendarConfiguration> userConfigurationDictionaryAdapter = new UserConfigurationDictionaryAdapter<MailboxCalendarConfiguration>(session.MailboxSession, "OWA.UserOptions", new GetUserConfigurationDelegate(UserConfigurationHelper.GetMailboxConfiguration), MailboxCalendarConfiguration.mailboxProperties))
				{
					using (UserConfigurationDictionaryAdapter<MailboxCalendarConfiguration> userConfigurationDictionaryAdapter2 = new UserConfigurationDictionaryAdapter<MailboxCalendarConfiguration>(session.MailboxSession, "Calendar", new GetUserConfigurationDelegate(UserConfigurationHelper.GetCalendarConfiguration), MailboxCalendarConfiguration.calendarProperties))
					{
						workingHoursAdapter.Fill(this);
						userConfigurationDictionaryAdapter.Fill(this);
						userConfigurationDictionaryAdapter2.Fill(this);
					}
				}
			}
			if (base.Principal.PreferredCultures.Any<CultureInfo>())
			{
				this[MailboxCalendarConfigurationSchema.Language] = base.Principal.PreferredCultures.First<CultureInfo>();
			}
			return this;
		}

		// Token: 0x06005E00 RID: 24064 RVA: 0x0018D430 File Offset: 0x0018B630
		public override void Save(MailboxStoreTypeProvider session)
		{
			using (WorkingHoursAdapter<MailboxCalendarConfiguration> workingHoursAdapter = new WorkingHoursAdapter<MailboxCalendarConfiguration>(session.MailboxSession))
			{
				using (UserConfigurationDictionaryAdapter<MailboxCalendarConfiguration> userConfigurationDictionaryAdapter = new UserConfigurationDictionaryAdapter<MailboxCalendarConfiguration>(session.MailboxSession, "OWA.UserOptions", new GetUserConfigurationDelegate(UserConfigurationHelper.GetMailboxConfiguration), MailboxCalendarConfiguration.mailboxProperties))
				{
					using (UserConfigurationDictionaryAdapter<MailboxCalendarConfiguration> userConfigurationDictionaryAdapter2 = new UserConfigurationDictionaryAdapter<MailboxCalendarConfiguration>(session.MailboxSession, "Calendar", new GetUserConfigurationDelegate(UserConfigurationHelper.GetCalendarConfiguration), MailboxCalendarConfiguration.calendarProperties))
					{
						workingHoursAdapter.Save(this);
						userConfigurationDictionaryAdapter.Save(this);
						userConfigurationDictionaryAdapter2.Save(this);
						base.ResetChangeTracking();
					}
				}
			}
		}

		// Token: 0x04003472 RID: 13426
		private static MailboxCalendarConfigurationSchema schema = ObjectSchema.GetInstance<MailboxCalendarConfigurationSchema>();

		// Token: 0x04003473 RID: 13427
		private static SimplePropertyDefinition[] mailboxProperties = new SimplePropertyDefinition[]
		{
			MailboxCalendarConfigurationSchema.WeekStartDay,
			MailboxCalendarConfigurationSchema.ShowWeekNumbers,
			MailboxCalendarConfigurationSchema.FirstWeekOfYear,
			MailboxCalendarConfigurationSchema.TimeIncrement,
			MailboxCalendarConfigurationSchema.RemindersEnabled,
			MailboxCalendarConfigurationSchema.ReminderSoundEnabled,
			MailboxCalendarConfigurationSchema.WeatherEnabled,
			MailboxCalendarConfigurationSchema.WeatherUnit,
			MailboxCalendarConfigurationSchema.WeatherLocations
		};

		// Token: 0x04003474 RID: 13428
		private static SimplePropertyDefinition[] calendarProperties = new SimplePropertyDefinition[]
		{
			MailboxCalendarConfigurationSchema.DefaultReminderTime
		};
	}
}
