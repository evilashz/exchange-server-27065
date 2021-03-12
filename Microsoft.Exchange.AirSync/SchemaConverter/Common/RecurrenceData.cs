using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters.Recurrence;
using Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000191 RID: 401
	[Serializable]
	internal class RecurrenceData : INestedData
	{
		// Token: 0x06001142 RID: 4418 RVA: 0x0005EB85 File Offset: 0x0005CD85
		public RecurrenceData(TypeOfRecurrence type, int protocolVersion)
		{
			this.subProperties = new Hashtable();
			this.recurrenceType = type;
			this.protocolVersion = protocolVersion;
			this.Clear();
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x0005EBAC File Offset: 0x0005CDAC
		public int ProtocolVersion
		{
			get
			{
				return this.protocolVersion;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x0005EBB4 File Offset: 0x0005CDB4
		// (set) Token: 0x06001145 RID: 4421 RVA: 0x0005EBD5 File Offset: 0x0005CDD5
		public byte DayOfMonth
		{
			get
			{
				return byte.Parse((string)this.subProperties["DayOfMonth"], CultureInfo.InvariantCulture);
			}
			set
			{
				this.subProperties["DayOfMonth"] = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x0005EBF3 File Offset: 0x0005CDF3
		// (set) Token: 0x06001147 RID: 4423 RVA: 0x0005EC14 File Offset: 0x0005CE14
		public byte DayOfWeek
		{
			get
			{
				return byte.Parse((string)this.subProperties["DayOfWeek"], CultureInfo.InvariantCulture);
			}
			set
			{
				this.subProperties["DayOfWeek"] = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x0005EC34 File Offset: 0x0005CE34
		// (set) Token: 0x06001149 RID: 4425 RVA: 0x0005EC58 File Offset: 0x0005CE58
		public ISet<DayOfWeek> DaysOfWeek
		{
			get
			{
				DaysOfWeek dayOfWeek = (DaysOfWeek)this.DayOfWeek;
				return RecurrenceData.dayOfWeekConverter.Convert(dayOfWeek);
			}
			set
			{
				DaysOfWeek daysOfWeek = RecurrenceData.dayOfWeekConverter.Convert(value);
				this.DayOfWeek = (byte)daysOfWeek;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x0005EC7C File Offset: 0x0005CE7C
		// (set) Token: 0x0600114B RID: 4427 RVA: 0x0005ECA4 File Offset: 0x0005CEA4
		public bool DeadOccur
		{
			get
			{
				return Convert.ToBoolean(byte.Parse((string)this.subProperties["DeadOccur"], CultureInfo.InvariantCulture));
			}
			set
			{
				this.subProperties["DeadOccur"] = Convert.ToByte(value).ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x0005ECD4 File Offset: 0x0005CED4
		// (set) Token: 0x0600114D RID: 4429 RVA: 0x0005ECF5 File Offset: 0x0005CEF5
		public ushort Interval
		{
			get
			{
				return ushort.Parse((string)this.subProperties["Interval"], CultureInfo.InvariantCulture);
			}
			set
			{
				this.subProperties["Interval"] = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x0005ED13 File Offset: 0x0005CF13
		public string[] Keys
		{
			get
			{
				return this.keys;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x0005ED1B File Offset: 0x0005CF1B
		// (set) Token: 0x06001150 RID: 4432 RVA: 0x0005ED3C File Offset: 0x0005CF3C
		public byte MonthOfYear
		{
			get
			{
				return byte.Parse((string)this.subProperties["MonthOfYear"], CultureInfo.InvariantCulture);
			}
			set
			{
				this.subProperties["MonthOfYear"] = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x0005ED5A File Offset: 0x0005CF5A
		// (set) Token: 0x06001152 RID: 4434 RVA: 0x0005ED7B File Offset: 0x0005CF7B
		public ushort Occurrences
		{
			get
			{
				return ushort.Parse((string)this.subProperties["Occurrences"], CultureInfo.InvariantCulture);
			}
			set
			{
				this.subProperties["Occurrences"] = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x0005ED99 File Offset: 0x0005CF99
		// (set) Token: 0x06001154 RID: 4436 RVA: 0x0005EDC0 File Offset: 0x0005CFC0
		public bool Regenerate
		{
			get
			{
				return Convert.ToBoolean(byte.Parse((string)this.subProperties["Regenerate"], CultureInfo.InvariantCulture));
			}
			set
			{
				this.subProperties["Regenerate"] = Convert.ToByte(value).ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x0005EDF0 File Offset: 0x0005CFF0
		// (set) Token: 0x06001156 RID: 4438 RVA: 0x0005EE45 File Offset: 0x0005D045
		public ExDateTime Start
		{
			get
			{
				ExDateTime result;
				if (!ExDateTime.TryParseExact((string)this.subProperties["Start"], this.formatString, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out result))
				{
					throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidDateTime, null, false)
					{
						ErrorStringForProtocolLogger = "InvalidStartDateTimeInRecurrenceData"
					};
				}
				return result;
			}
			set
			{
				this.subProperties["Start"] = value.ToString(this.formatString, DateTimeFormatInfo.InvariantInfo);
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x0005EE69 File Offset: 0x0005D069
		public IDictionary SubProperties
		{
			get
			{
				return this.subProperties;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x0005EE74 File Offset: 0x0005D074
		// (set) Token: 0x06001159 RID: 4441 RVA: 0x0005EEB8 File Offset: 0x0005D0B8
		public RecurrenceData.RecurrenceType Type
		{
			get
			{
				RecurrenceData.RecurrenceType recurrenceType = (RecurrenceData.RecurrenceType)byte.Parse((string)this.subProperties["Type"], CultureInfo.InvariantCulture);
				if (!EnumValidator.IsValidValue<RecurrenceData.RecurrenceType>(recurrenceType))
				{
					throw new ConversionException("RecurrenceType value is invalid.");
				}
				return recurrenceType;
			}
			set
			{
				IDictionary dictionary = this.subProperties;
				object key = "Type";
				int num = (int)value;
				dictionary[key] = num.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x0005EEE4 File Offset: 0x0005D0E4
		// (set) Token: 0x0600115B RID: 4443 RVA: 0x0005EF70 File Offset: 0x0005D170
		public ExDateTime Until
		{
			get
			{
				if (this.protocolVersion >= 160 && this.recurrenceType == TypeOfRecurrence.Calendar)
				{
					return TimeZoneConverter.Parse((string)this.subProperties["Until"], "RecurrenceDataUntil");
				}
				ExDateTime result;
				if (!ExDateTime.TryParseExact((string)this.subProperties["Until"], this.formatString, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out result))
				{
					throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidDateTime, null, false)
					{
						ErrorStringForProtocolLogger = "InvalidUntilDateTimeInRecurrenceData"
					};
				}
				return result;
			}
			set
			{
				if (this.protocolVersion < 160 || this.recurrenceType != TypeOfRecurrence.Calendar)
				{
					this.subProperties["Until"] = value.ToString(this.formatString, DateTimeFormatInfo.InvariantInfo);
					return;
				}
				this.subProperties["Until"] = TimeZoneConverter.ToString(value);
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x0005EFCB File Offset: 0x0005D1CB
		// (set) Token: 0x0600115D RID: 4445 RVA: 0x0005EFEC File Offset: 0x0005D1EC
		public byte WeekOfMonth
		{
			get
			{
				return byte.Parse((string)this.subProperties["WeekOfMonth"], CultureInfo.InvariantCulture);
			}
			set
			{
				this.subProperties["WeekOfMonth"] = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x0005F00C File Offset: 0x0005D20C
		// (set) Token: 0x0600115F RID: 4447 RVA: 0x0005F064 File Offset: 0x0005D264
		public CalendarType CalendarType
		{
			get
			{
				if (this.subProperties["CalendarType"] == null)
				{
					return CalendarType.Default;
				}
				CalendarType calendarType = (CalendarType)byte.Parse((string)this.subProperties["CalendarType"], CultureInfo.InvariantCulture);
				if (!EnumValidator.IsValidValue<CalendarType>(calendarType))
				{
					throw new ConversionException("CalendarType value is invalid.");
				}
				return calendarType;
			}
			set
			{
				IDictionary dictionary = this.subProperties;
				object key = "CalendarType";
				int num = (int)value;
				dictionary[key] = num.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x0005F08F File Offset: 0x0005D28F
		// (set) Token: 0x06001161 RID: 4449 RVA: 0x0005F0C5 File Offset: 0x0005D2C5
		public bool IsLeapMonth
		{
			get
			{
				return this.subProperties["IsLeapMonth"] != null && ((string)this.subProperties["IsLeapMonth"]).Equals("1", StringComparison.OrdinalIgnoreCase);
			}
			set
			{
				this.subProperties["IsLeapMonth"] = (value ? "1" : "0");
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x0005F0E8 File Offset: 0x0005D2E8
		// (set) Token: 0x06001163 RID: 4451 RVA: 0x0005F268 File Offset: 0x0005D468
		public DayOfWeek FirstDayOfWeek
		{
			get
			{
				if (this.subProperties["FirstDayOfWeek"] == null)
				{
					MailboxSession mailboxSession = Command.CurrentCommand.MailboxSession;
					DayOfWeek firstDayOfWeek = mailboxSession.PreferedCulture.DateTimeFormat.FirstDayOfWeek;
					UserConfigurationManager userConfigurationManager = mailboxSession.UserConfigurationManager;
					if (userConfigurationManager == null)
					{
						return firstDayOfWeek;
					}
					try
					{
						using (UserConfiguration mailboxConfiguration = userConfigurationManager.GetMailboxConfiguration("OWA.UserOptions", UserConfigurationTypes.Dictionary))
						{
							IDictionary dictionary = mailboxConfiguration.GetDictionary();
							if (!dictionary.Contains("weekstartday"))
							{
								return firstDayOfWeek;
							}
							object obj = dictionary["weekstartday"];
							if (obj == null || !(obj is int))
							{
								return firstDayOfWeek;
							}
							if (!EnumValidator.IsValidValue<DayOfWeek>((DayOfWeek)obj))
							{
								return firstDayOfWeek;
							}
							return (DayOfWeek)obj;
						}
					}
					catch (ObjectNotFoundException arg)
					{
						AirSyncDiagnostics.TraceError<ObjectNotFoundException>(ExTraceGlobals.RequestsTracer, null, "ObjectNotFoundException when getting FirstDayOfWeekProperty from MailboxConfiguration: {0}", arg);
						return firstDayOfWeek;
					}
					catch (CorruptDataException arg2)
					{
						AirSyncDiagnostics.TraceError<CorruptDataException>(ExTraceGlobals.RequestsTracer, null, "CorruptDataException when getting FirstDayOfWeekProperty from MailboxConfiguration: {0}", arg2);
						return firstDayOfWeek;
					}
					catch (AccessDeniedException arg3)
					{
						AirSyncDiagnostics.TraceError<AccessDeniedException>(ExTraceGlobals.RequestsTracer, null, "AccessDeniedException when getting FirstDayOfWeekProperty from MailboxConfiguration: {0}", arg3);
						return firstDayOfWeek;
					}
				}
				DayOfWeek dayOfWeek = (DayOfWeek)byte.Parse((string)this.subProperties["FirstDayOfWeek"], CultureInfo.InvariantCulture);
				if (!EnumValidator.IsValidValue<DayOfWeek>(dayOfWeek))
				{
					throw new ConversionException("FirstDayOfWeek value is invalid.");
				}
				return dayOfWeek;
			}
			set
			{
				IDictionary dictionary = this.subProperties;
				object key = "FirstDayOfWeek";
				int num = (int)value;
				dictionary[key] = num.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x0005F293 File Offset: 0x0005D493
		public RecurrenceOrderType RecurrenceOrderType
		{
			get
			{
				if (this.WeekOfMonth != 5)
				{
					return (RecurrenceOrderType)this.WeekOfMonth;
				}
				return RecurrenceOrderType.Last;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x0005F2A8 File Offset: 0x0005D4A8
		// (set) Token: 0x06001166 RID: 4454 RVA: 0x0005F2C8 File Offset: 0x0005D4C8
		public WeekIndex WeekIndex
		{
			get
			{
				return RecurrenceData.weekIndexConverter.Convert(this.RecurrenceOrderType);
			}
			set
			{
				switch (value)
				{
				case WeekIndex.First:
					this.WeekOfMonth = 1;
					return;
				case WeekIndex.Second:
					this.WeekOfMonth = 2;
					return;
				case WeekIndex.Third:
					this.WeekOfMonth = 3;
					return;
				case WeekIndex.Fourth:
					this.WeekOfMonth = 4;
					return;
				case WeekIndex.Last:
					this.WeekOfMonth = 5;
					return;
				default:
					throw new InvalidOperationException(string.Format("Unable to set invalid value {0} to WeekOfMonth.", value));
				}
			}
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0005F330 File Offset: 0x0005D530
		public static string GetEmailNamespaceForKey(int keyIndex)
		{
			if (keyIndex < RecurrenceData.calKeys.Length)
			{
				return "Email:";
			}
			if (keyIndex < RecurrenceData.calKeys141.Length)
			{
				return "Email2:";
			}
			throw new InvalidOperationException(string.Format("keyIndex value {0} is invalid.", keyIndex));
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0005F367 File Offset: 0x0005D567
		public bool HasInterval()
		{
			return this.subProperties["Interval"] != null && this.Interval != 0;
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x0005F389 File Offset: 0x0005D589
		public bool HasOccurences()
		{
			return null != this.subProperties["Occurrences"];
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x0005F3A1 File Offset: 0x0005D5A1
		public bool HasUntil()
		{
			return null != this.subProperties["Until"];
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x0005F3B9 File Offset: 0x0005D5B9
		public bool HasWeekOfMonth()
		{
			return null != this.subProperties["WeekOfMonth"];
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0005F3D4 File Offset: 0x0005D5D4
		public void Validate()
		{
			bool flag = this.recurrenceType == TypeOfRecurrence.Calendar || (this.recurrenceType == TypeOfRecurrence.Task && !this.Regenerate);
			switch (this.Type)
			{
			case RecurrenceData.RecurrenceType.Daily:
			case RecurrenceData.RecurrenceType.Weekly:
				this.CheckNull("DayOfMonth");
				this.CheckNull("WeekOfMonth");
				this.CheckNull("MonthOfYear");
				this.CheckNull("CalendarType");
				this.CheckNull("IsLeapMonth");
				if (!flag)
				{
					this.CheckNull("DayOfWeek");
					return;
				}
				if (this.Type == RecurrenceData.RecurrenceType.Weekly)
				{
					this.CheckNotNull("DayOfWeek");
					return;
				}
				return;
			case RecurrenceData.RecurrenceType.Monthly:
				this.CheckNull("DayOfWeek");
				this.CheckNull("WeekOfMonth");
				this.CheckNull("MonthOfYear");
				this.CheckNull("IsLeapMonth");
				if (flag)
				{
					this.CheckNotNull("DayOfMonth");
					return;
				}
				this.CheckNull("DayOfMonth");
				return;
			case RecurrenceData.RecurrenceType.MonthlyTh:
				this.CheckNull("DayOfMonth");
				this.CheckNull("MonthOfYear");
				this.CheckNull("IsLeapMonth");
				if (flag)
				{
					this.CheckNotNull("WeekOfMonth");
					this.CheckNotNull("DayOfWeek");
				}
				else
				{
					this.CheckNull("WeekOfMonth");
					this.CheckNull("DayOfWeek");
				}
				if (!EnumValidator.IsValidValue<RecurrenceOrderType>(this.RecurrenceOrderType))
				{
					throw new ConversionException(string.Format(CultureInfo.InvariantCulture, "WeekOfMonth value {0} is invalid", new object[]
					{
						this.RecurrenceOrderType
					}));
				}
				return;
			case RecurrenceData.RecurrenceType.Yearly:
				this.CheckNull("DayOfWeek");
				this.CheckNull("WeekOfMonth");
				if (flag)
				{
					this.CheckNotNull("DayOfMonth");
					this.CheckNotNull("MonthOfYear");
					return;
				}
				this.CheckNull("WeekOfMonth");
				this.CheckNull("MonthOfYear");
				return;
			case RecurrenceData.RecurrenceType.YearlyTh:
				this.CheckNull("DayOfMonth");
				if (!flag)
				{
					throw new ConversionException(string.Format(CultureInfo.InvariantCulture, "Unsupported recurrence {0}, should have been caught in a higher validation layer", new object[]
					{
						this.Type
					}));
				}
				this.CheckNotNull("WeekOfMonth");
				this.CheckNotNull("DayOfWeek");
				this.CheckNotNull("MonthOfYear");
				if (!EnumValidator.IsValidValue<RecurrenceOrderType>(this.RecurrenceOrderType))
				{
					throw new ConversionException(string.Format(CultureInfo.InvariantCulture, "WeekOfMonth value {0} is invalid", new object[]
					{
						this.RecurrenceOrderType
					}));
				}
				return;
			}
			throw new ConversionException(string.Format(CultureInfo.InvariantCulture, "Unexpected recurrence type {0}, should have been caught in a higher validation layer", new object[]
			{
				this.Type
			}));
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0005F670 File Offset: 0x0005D870
		public void Clear()
		{
			this.subProperties.Clear();
			switch (this.recurrenceType)
			{
			case TypeOfRecurrence.Calendar:
				this.formatString = "yyyyMMdd\\THHmmss\\Z";
				if (this.protocolVersion < 140)
				{
					this.keys = RecurrenceData.calKeys;
					return;
				}
				if (this.protocolVersion == 140)
				{
					this.keys = RecurrenceData.calKeys14;
					return;
				}
				this.keys = RecurrenceData.calKeys141;
				return;
			case TypeOfRecurrence.Task:
				this.formatString = "yyyy-MM-dd\\THH:mm:ss.fff\\Z";
				if (this.protocolVersion < 140)
				{
					this.keys = RecurrenceData.taskKeys;
				}
				else if (this.protocolVersion == 140)
				{
					this.keys = RecurrenceData.taskKeys14;
				}
				else
				{
					this.keys = RecurrenceData.taskKeys141;
				}
				this.DeadOccur = false;
				this.Regenerate = false;
				return;
			default:
				return;
			}
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0005F740 File Offset: 0x0005D940
		private void CheckNull(string propertyName)
		{
			if (this.SubProperties[propertyName] != null)
			{
				throw new ConversionException(string.Format(CultureInfo.InvariantCulture, "{0} is not expected with recurrence type {1}", new object[]
				{
					propertyName,
					this.Type
				}));
			}
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0005F78C File Offset: 0x0005D98C
		private void CheckNotNull(string propertyName)
		{
			if (this.SubProperties[propertyName] == null)
			{
				throw new ConversionException(string.Format(CultureInfo.InvariantCulture, "{0} is expected with recurrence type {1}", new object[]
				{
					propertyName,
					this.Type
				}));
			}
		}

		// Token: 0x04000B1C RID: 2844
		public const int MinProperties = 2;

		// Token: 0x04000B1D RID: 2845
		public const string CalFormatString = "yyyyMMdd\\THHmmss\\Z";

		// Token: 0x04000B1E RID: 2846
		public const string TaskFormatString = "yyyy-MM-dd\\THH:mm:ss.fff\\Z";

		// Token: 0x04000B1F RID: 2847
		private const string ConfigurationName = "OWA.UserOptions";

		// Token: 0x04000B20 RID: 2848
		private const string FirstDayOfWeekPropertyName = "weekstartday";

		// Token: 0x04000B21 RID: 2849
		private const byte lastWeekOfAMonth = 5;

		// Token: 0x04000B22 RID: 2850
		private static readonly string[] calKeys = new string[]
		{
			"Type",
			"Interval",
			"Until",
			"Occurrences",
			"WeekOfMonth",
			"DayOfMonth",
			"DayOfWeek",
			"MonthOfYear"
		};

		// Token: 0x04000B23 RID: 2851
		private static readonly string[] calKeys14 = new string[]
		{
			"Type",
			"Interval",
			"Until",
			"Occurrences",
			"WeekOfMonth",
			"DayOfMonth",
			"DayOfWeek",
			"MonthOfYear",
			"CalendarType",
			"IsLeapMonth"
		};

		// Token: 0x04000B24 RID: 2852
		private static readonly string[] calKeys141 = new string[]
		{
			"Type",
			"Interval",
			"Until",
			"Occurrences",
			"WeekOfMonth",
			"DayOfMonth",
			"DayOfWeek",
			"MonthOfYear",
			"CalendarType",
			"IsLeapMonth",
			"FirstDayOfWeek"
		};

		// Token: 0x04000B25 RID: 2853
		private static readonly string[] taskKeys = new string[]
		{
			"Regenerate",
			"DeadOccur",
			"Type",
			"Start",
			"Occurrences",
			"Until",
			"Interval",
			"WeekOfMonth",
			"DayOfMonth",
			"DayOfWeek",
			"MonthOfYear"
		};

		// Token: 0x04000B26 RID: 2854
		private static readonly string[] taskKeys14 = new string[]
		{
			"Regenerate",
			"DeadOccur",
			"Type",
			"Start",
			"Occurrences",
			"Until",
			"Interval",
			"WeekOfMonth",
			"DayOfMonth",
			"DayOfWeek",
			"MonthOfYear",
			"CalendarType",
			"IsLeapMonth"
		};

		// Token: 0x04000B27 RID: 2855
		private static readonly string[] taskKeys141 = new string[]
		{
			"Regenerate",
			"DeadOccur",
			"Type",
			"Start",
			"Occurrences",
			"Until",
			"Interval",
			"WeekOfMonth",
			"DayOfMonth",
			"DayOfWeek",
			"MonthOfYear",
			"CalendarType",
			"IsLeapMonth",
			"FirstDayOfWeek"
		};

		// Token: 0x04000B28 RID: 2856
		private static readonly DayOfWeekConverter dayOfWeekConverter = default(DayOfWeekConverter);

		// Token: 0x04000B29 RID: 2857
		private static readonly WeekIndexConverter weekIndexConverter = default(WeekIndexConverter);

		// Token: 0x04000B2A RID: 2858
		private int protocolVersion;

		// Token: 0x04000B2B RID: 2859
		private string formatString;

		// Token: 0x04000B2C RID: 2860
		private string[] keys;

		// Token: 0x04000B2D RID: 2861
		private IDictionary subProperties;

		// Token: 0x04000B2E RID: 2862
		private TypeOfRecurrence recurrenceType;

		// Token: 0x02000192 RID: 402
		public enum RecurrenceType
		{
			// Token: 0x04000B30 RID: 2864
			Daily,
			// Token: 0x04000B31 RID: 2865
			Weekly,
			// Token: 0x04000B32 RID: 2866
			Monthly,
			// Token: 0x04000B33 RID: 2867
			MonthlyTh,
			// Token: 0x04000B34 RID: 2868
			Yearly = 5,
			// Token: 0x04000B35 RID: 2869
			YearlyTh
		}
	}
}
