using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security;
using System.Threading;

namespace System.Globalization
{
	// Token: 0x02000382 RID: 898
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DateTimeFormatInfo : ICloneable, IFormatProvider
	{
		// Token: 0x06002D92 RID: 11666 RVA: 0x000ADB0C File Offset: 0x000ABD0C
		[SecuritySafeCritical]
		private static bool InitPreferExistingTokens()
		{
			return DateTime.LegacyParseMode();
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06002D93 RID: 11667 RVA: 0x000ADB22 File Offset: 0x000ABD22
		private string CultureName
		{
			get
			{
				if (this.m_name == null)
				{
					this.m_name = this.m_cultureData.CultureName;
				}
				return this.m_name;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06002D94 RID: 11668 RVA: 0x000ADB43 File Offset: 0x000ABD43
		private CultureInfo Culture
		{
			get
			{
				if (this.m_cultureInfo == null)
				{
					this.m_cultureInfo = CultureInfo.GetCultureInfo(this.CultureName);
				}
				return this.m_cultureInfo;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06002D95 RID: 11669 RVA: 0x000ADB64 File Offset: 0x000ABD64
		private string LanguageName
		{
			[SecurityCritical]
			get
			{
				if (this.m_langName == null)
				{
					this.m_langName = this.m_cultureData.SISO639LANGNAME;
				}
				return this.m_langName;
			}
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x000ADB85 File Offset: 0x000ABD85
		private string[] internalGetAbbreviatedDayOfWeekNames()
		{
			if (this.abbreviatedDayNames == null)
			{
				this.abbreviatedDayNames = this.m_cultureData.AbbreviatedDayNames(this.Calendar.ID);
			}
			return this.abbreviatedDayNames;
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x000ADBB1 File Offset: 0x000ABDB1
		private string[] internalGetSuperShortDayNames()
		{
			if (this.m_superShortDayNames == null)
			{
				this.m_superShortDayNames = this.m_cultureData.SuperShortDayNames(this.Calendar.ID);
			}
			return this.m_superShortDayNames;
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x000ADBDD File Offset: 0x000ABDDD
		private string[] internalGetDayOfWeekNames()
		{
			if (this.dayNames == null)
			{
				this.dayNames = this.m_cultureData.DayNames(this.Calendar.ID);
			}
			return this.dayNames;
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x000ADC09 File Offset: 0x000ABE09
		private string[] internalGetAbbreviatedMonthNames()
		{
			if (this.abbreviatedMonthNames == null)
			{
				this.abbreviatedMonthNames = this.m_cultureData.AbbreviatedMonthNames(this.Calendar.ID);
			}
			return this.abbreviatedMonthNames;
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x000ADC35 File Offset: 0x000ABE35
		private string[] internalGetMonthNames()
		{
			if (this.monthNames == null)
			{
				this.monthNames = this.m_cultureData.MonthNames(this.Calendar.ID);
			}
			return this.monthNames;
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x000ADC61 File Offset: 0x000ABE61
		[__DynamicallyInvokable]
		public DateTimeFormatInfo() : this(CultureInfo.InvariantCulture.m_cultureData, GregorianCalendar.GetDefaultInstance())
		{
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x000ADC78 File Offset: 0x000ABE78
		internal DateTimeFormatInfo(CultureData cultureData, Calendar cal)
		{
			this.m_cultureData = cultureData;
			this.Calendar = cal;
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x000ADCA4 File Offset: 0x000ABEA4
		[SecuritySafeCritical]
		private void InitializeOverridableProperties(CultureData cultureData, int calendarID)
		{
			if (this.firstDayOfWeek == -1)
			{
				this.firstDayOfWeek = cultureData.IFIRSTDAYOFWEEK;
			}
			if (this.calendarWeekRule == -1)
			{
				this.calendarWeekRule = cultureData.IFIRSTWEEKOFYEAR;
			}
			if (this.amDesignator == null)
			{
				this.amDesignator = cultureData.SAM1159;
			}
			if (this.pmDesignator == null)
			{
				this.pmDesignator = cultureData.SPM2359;
			}
			if (this.timeSeparator == null)
			{
				this.timeSeparator = cultureData.TimeSeparator;
			}
			if (this.dateSeparator == null)
			{
				this.dateSeparator = cultureData.DateSeparator(calendarID);
			}
			this.allLongTimePatterns = this.m_cultureData.LongTimes;
			this.allShortTimePatterns = this.m_cultureData.ShortTimes;
			this.allLongDatePatterns = cultureData.LongDates(calendarID);
			this.allShortDatePatterns = cultureData.ShortDates(calendarID);
			this.allYearMonthPatterns = cultureData.YearMonths(calendarID);
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x000ADD78 File Offset: 0x000ABF78
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_name != null)
			{
				this.m_cultureData = CultureData.GetCultureData(this.m_name, this.m_useUserOverride);
				if (this.m_cultureData == null)
				{
					throw new CultureNotFoundException("m_name", this.m_name, Environment.GetResourceString("Argument_CultureNotSupported"));
				}
			}
			else
			{
				this.m_cultureData = CultureData.GetCultureData(this.CultureID, this.m_useUserOverride);
			}
			if (this.calendar == null)
			{
				this.calendar = (Calendar)GregorianCalendar.GetDefaultInstance().Clone();
				this.calendar.SetReadOnlyState(this.m_isReadOnly);
			}
			else
			{
				CultureInfo.CheckDomainSafetyObject(this.calendar, this);
			}
			this.InitializeOverridableProperties(this.m_cultureData, this.calendar.ID);
			bool isReadOnly = this.m_isReadOnly;
			this.m_isReadOnly = false;
			if (this.longDatePattern != null)
			{
				this.LongDatePattern = this.longDatePattern;
			}
			if (this.shortDatePattern != null)
			{
				this.ShortDatePattern = this.shortDatePattern;
			}
			if (this.yearMonthPattern != null)
			{
				this.YearMonthPattern = this.yearMonthPattern;
			}
			if (this.longTimePattern != null)
			{
				this.LongTimePattern = this.longTimePattern;
			}
			if (this.shortTimePattern != null)
			{
				this.ShortTimePattern = this.shortTimePattern;
			}
			this.m_isReadOnly = isReadOnly;
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x000ADEAC File Offset: 0x000AC0AC
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.CultureID = this.m_cultureData.ILANGUAGE;
			this.m_useUserOverride = this.m_cultureData.UseUserOverride;
			this.m_name = this.CultureName;
			if (DateTimeFormatInfo.s_calendarNativeNames == null)
			{
				DateTimeFormatInfo.s_calendarNativeNames = new Hashtable();
			}
			object obj = this.LongTimePattern;
			obj = this.LongDatePattern;
			obj = this.ShortTimePattern;
			obj = this.ShortDatePattern;
			obj = this.YearMonthPattern;
			obj = this.AllLongTimePatterns;
			obj = this.AllLongDatePatterns;
			obj = this.AllShortTimePatterns;
			obj = this.AllShortDatePatterns;
			obj = this.AllYearMonthPatterns;
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06002DA0 RID: 11680 RVA: 0x000ADF44 File Offset: 0x000AC144
		[__DynamicallyInvokable]
		public static DateTimeFormatInfo InvariantInfo
		{
			[__DynamicallyInvokable]
			get
			{
				if (DateTimeFormatInfo.invariantInfo == null)
				{
					DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo();
					dateTimeFormatInfo.Calendar.SetReadOnlyState(true);
					dateTimeFormatInfo.m_isReadOnly = true;
					DateTimeFormatInfo.invariantInfo = dateTimeFormatInfo;
				}
				return DateTimeFormatInfo.invariantInfo;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06002DA1 RID: 11681 RVA: 0x000ADF84 File Offset: 0x000AC184
		[__DynamicallyInvokable]
		public static DateTimeFormatInfo CurrentInfo
		{
			[__DynamicallyInvokable]
			get
			{
				CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
				if (!currentCulture.m_isInherited)
				{
					DateTimeFormatInfo dateTimeInfo = currentCulture.dateTimeInfo;
					if (dateTimeInfo != null)
					{
						return dateTimeInfo;
					}
				}
				return (DateTimeFormatInfo)currentCulture.GetFormat(typeof(DateTimeFormatInfo));
			}
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x000ADFC8 File Offset: 0x000AC1C8
		[__DynamicallyInvokable]
		public static DateTimeFormatInfo GetInstance(IFormatProvider provider)
		{
			CultureInfo cultureInfo = provider as CultureInfo;
			if (cultureInfo != null && !cultureInfo.m_isInherited)
			{
				return cultureInfo.DateTimeFormat;
			}
			DateTimeFormatInfo dateTimeFormatInfo = provider as DateTimeFormatInfo;
			if (dateTimeFormatInfo != null)
			{
				return dateTimeFormatInfo;
			}
			if (provider != null)
			{
				dateTimeFormatInfo = (provider.GetFormat(typeof(DateTimeFormatInfo)) as DateTimeFormatInfo);
				if (dateTimeFormatInfo != null)
				{
					return dateTimeFormatInfo;
				}
			}
			return DateTimeFormatInfo.CurrentInfo;
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x000AE01D File Offset: 0x000AC21D
		[__DynamicallyInvokable]
		public object GetFormat(Type formatType)
		{
			if (!(formatType == typeof(DateTimeFormatInfo)))
			{
				return null;
			}
			return this;
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x000AE034 File Offset: 0x000AC234
		[__DynamicallyInvokable]
		public object Clone()
		{
			DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)base.MemberwiseClone();
			dateTimeFormatInfo.calendar = (Calendar)this.Calendar.Clone();
			dateTimeFormatInfo.m_isReadOnly = false;
			return dateTimeFormatInfo;
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06002DA5 RID: 11685 RVA: 0x000AE06B File Offset: 0x000AC26B
		// (set) Token: 0x06002DA6 RID: 11686 RVA: 0x000AE073 File Offset: 0x000AC273
		[__DynamicallyInvokable]
		public string AMDesignator
		{
			[__DynamicallyInvokable]
			get
			{
				return this.amDesignator;
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.ClearTokenHashTable();
				this.amDesignator = value;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06002DA7 RID: 11687 RVA: 0x000AE0B2 File Offset: 0x000AC2B2
		// (set) Token: 0x06002DA8 RID: 11688 RVA: 0x000AE0BC File Offset: 0x000AC2BC
		[__DynamicallyInvokable]
		public Calendar Calendar
		{
			[__DynamicallyInvokable]
			get
			{
				return this.calendar;
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Obj"));
				}
				if (value == this.calendar)
				{
					return;
				}
				CultureInfo.CheckDomainSafetyObject(value, this);
				for (int i = 0; i < this.OptionalCalendars.Length; i++)
				{
					if (this.OptionalCalendars[i] == value.ID)
					{
						if (this.calendar != null)
						{
							this.m_eraNames = null;
							this.m_abbrevEraNames = null;
							this.m_abbrevEnglishEraNames = null;
							this.monthDayPattern = null;
							this.dayNames = null;
							this.abbreviatedDayNames = null;
							this.m_superShortDayNames = null;
							this.monthNames = null;
							this.abbreviatedMonthNames = null;
							this.genitiveMonthNames = null;
							this.m_genitiveAbbreviatedMonthNames = null;
							this.leapYearMonthNames = null;
							this.formatFlags = DateTimeFormatFlags.NotInitialized;
							this.allShortDatePatterns = null;
							this.allLongDatePatterns = null;
							this.allYearMonthPatterns = null;
							this.dateTimeOffsetPattern = null;
							this.longDatePattern = null;
							this.shortDatePattern = null;
							this.yearMonthPattern = null;
							this.fullDateTimePattern = null;
							this.generalShortTimePattern = null;
							this.generalLongTimePattern = null;
							this.dateSeparator = null;
							this.ClearTokenHashTable();
						}
						this.calendar = value;
						this.InitializeOverridableProperties(this.m_cultureData, this.calendar.ID);
						return;
					}
				}
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Argument_InvalidCalendar"));
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x000AE222 File Offset: 0x000AC422
		private int[] OptionalCalendars
		{
			get
			{
				if (this.optionalCalendars == null)
				{
					this.optionalCalendars = this.m_cultureData.CalendarIds;
				}
				return this.optionalCalendars;
			}
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000AE244 File Offset: 0x000AC444
		[__DynamicallyInvokable]
		public int GetEra(string eraName)
		{
			if (eraName == null)
			{
				throw new ArgumentNullException("eraName", Environment.GetResourceString("ArgumentNull_String"));
			}
			if (eraName.Length == 0)
			{
				return -1;
			}
			for (int i = 0; i < this.EraNames.Length; i++)
			{
				if (this.m_eraNames[i].Length > 0 && string.Compare(eraName, this.m_eraNames[i], this.Culture, CompareOptions.IgnoreCase) == 0)
				{
					return i + 1;
				}
			}
			for (int j = 0; j < this.AbbreviatedEraNames.Length; j++)
			{
				if (string.Compare(eraName, this.m_abbrevEraNames[j], this.Culture, CompareOptions.IgnoreCase) == 0)
				{
					return j + 1;
				}
			}
			for (int k = 0; k < this.AbbreviatedEnglishEraNames.Length; k++)
			{
				if (string.Compare(eraName, this.m_abbrevEnglishEraNames[k], StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return k + 1;
				}
			}
			return -1;
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06002DAB RID: 11691 RVA: 0x000AE308 File Offset: 0x000AC508
		internal string[] EraNames
		{
			get
			{
				if (this.m_eraNames == null)
				{
					this.m_eraNames = this.m_cultureData.EraNames(this.Calendar.ID);
				}
				return this.m_eraNames;
			}
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x000AE334 File Offset: 0x000AC534
		[__DynamicallyInvokable]
		public string GetEraName(int era)
		{
			if (era == 0)
			{
				era = this.Calendar.CurrentEraValue;
			}
			if (--era < this.EraNames.Length && era >= 0)
			{
				return this.m_eraNames[era];
			}
			throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06002DAD RID: 11693 RVA: 0x000AE382 File Offset: 0x000AC582
		internal string[] AbbreviatedEraNames
		{
			get
			{
				if (this.m_abbrevEraNames == null)
				{
					this.m_abbrevEraNames = this.m_cultureData.AbbrevEraNames(this.Calendar.ID);
				}
				return this.m_abbrevEraNames;
			}
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x000AE3B0 File Offset: 0x000AC5B0
		[__DynamicallyInvokable]
		public string GetAbbreviatedEraName(int era)
		{
			if (this.AbbreviatedEraNames.Length == 0)
			{
				return this.GetEraName(era);
			}
			if (era == 0)
			{
				era = this.Calendar.CurrentEraValue;
			}
			if (--era < this.m_abbrevEraNames.Length && era >= 0)
			{
				return this.m_abbrevEraNames[era];
			}
			throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06002DAF RID: 11695 RVA: 0x000AE40F File Offset: 0x000AC60F
		internal string[] AbbreviatedEnglishEraNames
		{
			get
			{
				if (this.m_abbrevEnglishEraNames == null)
				{
					this.m_abbrevEnglishEraNames = this.m_cultureData.AbbreviatedEnglishEraNames(this.Calendar.ID);
				}
				return this.m_abbrevEnglishEraNames;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x000AE43B File Offset: 0x000AC63B
		// (set) Token: 0x06002DB1 RID: 11697 RVA: 0x000AE443 File Offset: 0x000AC643
		public string DateSeparator
		{
			get
			{
				return this.dateSeparator;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.ClearTokenHashTable();
				this.dateSeparator = value;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x000AE482 File Offset: 0x000AC682
		// (set) Token: 0x06002DB3 RID: 11699 RVA: 0x000AE48C File Offset: 0x000AC68C
		[__DynamicallyInvokable]
		public DayOfWeek FirstDayOfWeek
		{
			[__DynamicallyInvokable]
			get
			{
				return (DayOfWeek)this.firstDayOfWeek;
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value >= DayOfWeek.Sunday && value <= DayOfWeek.Saturday)
				{
					this.firstDayOfWeek = (int)value;
					return;
				}
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					DayOfWeek.Sunday,
					DayOfWeek.Saturday
				}));
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06002DB4 RID: 11700 RVA: 0x000AE4ED File Offset: 0x000AC6ED
		// (set) Token: 0x06002DB5 RID: 11701 RVA: 0x000AE4F8 File Offset: 0x000AC6F8
		[__DynamicallyInvokable]
		public CalendarWeekRule CalendarWeekRule
		{
			[__DynamicallyInvokable]
			get
			{
				return (CalendarWeekRule)this.calendarWeekRule;
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value >= CalendarWeekRule.FirstDay && value <= CalendarWeekRule.FirstFourDayWeek)
				{
					this.calendarWeekRule = (int)value;
					return;
				}
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					CalendarWeekRule.FirstDay,
					CalendarWeekRule.FirstFourDayWeek
				}));
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06002DB6 RID: 11702 RVA: 0x000AE559 File Offset: 0x000AC759
		// (set) Token: 0x06002DB7 RID: 11703 RVA: 0x000AE585 File Offset: 0x000AC785
		[__DynamicallyInvokable]
		public string FullDateTimePattern
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.fullDateTimePattern == null)
				{
					this.fullDateTimePattern = this.LongDatePattern + " " + this.LongTimePattern;
				}
				return this.fullDateTimePattern;
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.fullDateTimePattern = value;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06002DB8 RID: 11704 RVA: 0x000AE5BE File Offset: 0x000AC7BE
		// (set) Token: 0x06002DB9 RID: 11705 RVA: 0x000AE5DC File Offset: 0x000AC7DC
		[__DynamicallyInvokable]
		public string LongDatePattern
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.longDatePattern == null)
				{
					this.longDatePattern = this.UnclonedLongDatePatterns[0];
				}
				return this.longDatePattern;
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.longDatePattern = value;
				this.ClearTokenHashTable();
				this.fullDateTimePattern = null;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06002DBA RID: 11706 RVA: 0x000AE62D File Offset: 0x000AC82D
		// (set) Token: 0x06002DBB RID: 11707 RVA: 0x000AE64C File Offset: 0x000AC84C
		[__DynamicallyInvokable]
		public string LongTimePattern
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.longTimePattern == null)
				{
					this.longTimePattern = this.UnclonedLongTimePatterns[0];
				}
				return this.longTimePattern;
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.longTimePattern = value;
				this.ClearTokenHashTable();
				this.fullDateTimePattern = null;
				this.generalLongTimePattern = null;
				this.dateTimeOffsetPattern = null;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06002DBC RID: 11708 RVA: 0x000AE6AB File Offset: 0x000AC8AB
		// (set) Token: 0x06002DBD RID: 11709 RVA: 0x000AE6D7 File Offset: 0x000AC8D7
		[__DynamicallyInvokable]
		public string MonthDayPattern
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.monthDayPattern == null)
				{
					this.monthDayPattern = this.m_cultureData.MonthDay(this.Calendar.ID);
				}
				return this.monthDayPattern;
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.monthDayPattern = value;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06002DBE RID: 11710 RVA: 0x000AE710 File Offset: 0x000AC910
		// (set) Token: 0x06002DBF RID: 11711 RVA: 0x000AE718 File Offset: 0x000AC918
		[__DynamicallyInvokable]
		public string PMDesignator
		{
			[__DynamicallyInvokable]
			get
			{
				return this.pmDesignator;
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.ClearTokenHashTable();
				this.pmDesignator = value;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06002DC0 RID: 11712 RVA: 0x000AE757 File Offset: 0x000AC957
		[__DynamicallyInvokable]
		public string RFC1123Pattern
		{
			[__DynamicallyInvokable]
			get
			{
				return "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06002DC1 RID: 11713 RVA: 0x000AE75E File Offset: 0x000AC95E
		// (set) Token: 0x06002DC2 RID: 11714 RVA: 0x000AE77C File Offset: 0x000AC97C
		[__DynamicallyInvokable]
		public string ShortDatePattern
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.shortDatePattern == null)
				{
					this.shortDatePattern = this.UnclonedShortDatePatterns[0];
				}
				return this.shortDatePattern;
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.shortDatePattern = value;
				this.ClearTokenHashTable();
				this.generalLongTimePattern = null;
				this.generalShortTimePattern = null;
				this.dateTimeOffsetPattern = null;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06002DC3 RID: 11715 RVA: 0x000AE7DB File Offset: 0x000AC9DB
		// (set) Token: 0x06002DC4 RID: 11716 RVA: 0x000AE7FC File Offset: 0x000AC9FC
		[__DynamicallyInvokable]
		public string ShortTimePattern
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.shortTimePattern == null)
				{
					this.shortTimePattern = this.UnclonedShortTimePatterns[0];
				}
				return this.shortTimePattern;
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.shortTimePattern = value;
				this.ClearTokenHashTable();
				this.generalShortTimePattern = null;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06002DC5 RID: 11717 RVA: 0x000AE84D File Offset: 0x000ACA4D
		[__DynamicallyInvokable]
		public string SortableDateTimePattern
		{
			[__DynamicallyInvokable]
			get
			{
				return "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06002DC6 RID: 11718 RVA: 0x000AE854 File Offset: 0x000ACA54
		internal string GeneralShortTimePattern
		{
			get
			{
				if (this.generalShortTimePattern == null)
				{
					this.generalShortTimePattern = this.ShortDatePattern + " " + this.ShortTimePattern;
				}
				return this.generalShortTimePattern;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06002DC7 RID: 11719 RVA: 0x000AE880 File Offset: 0x000ACA80
		internal string GeneralLongTimePattern
		{
			get
			{
				if (this.generalLongTimePattern == null)
				{
					this.generalLongTimePattern = this.ShortDatePattern + " " + this.LongTimePattern;
				}
				return this.generalLongTimePattern;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x000AE8AC File Offset: 0x000ACAAC
		internal string DateTimeOffsetPattern
		{
			get
			{
				if (this.dateTimeOffsetPattern == null)
				{
					this.dateTimeOffsetPattern = this.ShortDatePattern + " " + this.LongTimePattern;
					bool flag = false;
					bool flag2 = false;
					char c = '\'';
					int num = 0;
					while (!flag && num < this.LongTimePattern.Length)
					{
						char c2 = this.LongTimePattern[num];
						if (c2 <= '%')
						{
							if (c2 == '"')
							{
								goto IL_6D;
							}
							if (c2 == '%')
							{
								goto IL_97;
							}
						}
						else
						{
							if (c2 == '\'')
							{
								goto IL_6D;
							}
							if (c2 == '\\')
							{
								goto IL_97;
							}
							if (c2 == 'z')
							{
								flag = !flag2;
							}
						}
						IL_9B:
						num++;
						continue;
						IL_6D:
						if (flag2 && c == this.LongTimePattern[num])
						{
							flag2 = false;
							goto IL_9B;
						}
						if (!flag2)
						{
							c = this.LongTimePattern[num];
							flag2 = true;
							goto IL_9B;
						}
						goto IL_9B;
						IL_97:
						num++;
						goto IL_9B;
					}
					if (!flag)
					{
						this.dateTimeOffsetPattern += " zzz";
					}
				}
				return this.dateTimeOffsetPattern;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06002DC9 RID: 11721 RVA: 0x000AE988 File Offset: 0x000ACB88
		// (set) Token: 0x06002DCA RID: 11722 RVA: 0x000AE990 File Offset: 0x000ACB90
		public string TimeSeparator
		{
			get
			{
				return this.timeSeparator;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.ClearTokenHashTable();
				this.timeSeparator = value;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06002DCB RID: 11723 RVA: 0x000AE9CF File Offset: 0x000ACBCF
		[__DynamicallyInvokable]
		public string UniversalSortableDateTimePattern
		{
			[__DynamicallyInvokable]
			get
			{
				return "yyyy'-'MM'-'dd HH':'mm':'ss'Z'";
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06002DCC RID: 11724 RVA: 0x000AE9D6 File Offset: 0x000ACBD6
		// (set) Token: 0x06002DCD RID: 11725 RVA: 0x000AE9F4 File Offset: 0x000ACBF4
		[__DynamicallyInvokable]
		public string YearMonthPattern
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.yearMonthPattern == null)
				{
					this.yearMonthPattern = this.UnclonedYearMonthPatterns[0];
				}
				return this.yearMonthPattern;
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.yearMonthPattern = value;
				this.ClearTokenHashTable();
			}
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x000AEA34 File Offset: 0x000ACC34
		private static void CheckNullValue(string[] values, int length)
		{
			for (int i = 0; i < length; i++)
			{
				if (values[i] == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_ArrayValue"));
				}
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06002DCF RID: 11727 RVA: 0x000AEA67 File Offset: 0x000ACC67
		// (set) Token: 0x06002DD0 RID: 11728 RVA: 0x000AEA7C File Offset: 0x000ACC7C
		[__DynamicallyInvokable]
		public string[] AbbreviatedDayNames
		{
			[__DynamicallyInvokable]
			get
			{
				return (string[])this.internalGetAbbreviatedDayOfWeekNames().Clone();
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Array"));
				}
				if (value.Length != 7)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidArrayLength", new object[]
					{
						7
					}), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length);
				this.ClearTokenHashTable();
				this.abbreviatedDayNames = value;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06002DD1 RID: 11729 RVA: 0x000AEAF9 File Offset: 0x000ACCF9
		// (set) Token: 0x06002DD2 RID: 11730 RVA: 0x000AEB0C File Offset: 0x000ACD0C
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public string[] ShortestDayNames
		{
			[__DynamicallyInvokable]
			get
			{
				return (string[])this.internalGetSuperShortDayNames().Clone();
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Array"));
				}
				if (value.Length != 7)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidArrayLength", new object[]
					{
						7
					}), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length);
				this.m_superShortDayNames = value;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06002DD3 RID: 11731 RVA: 0x000AEB83 File Offset: 0x000ACD83
		// (set) Token: 0x06002DD4 RID: 11732 RVA: 0x000AEB98 File Offset: 0x000ACD98
		[__DynamicallyInvokable]
		public string[] DayNames
		{
			[__DynamicallyInvokable]
			get
			{
				return (string[])this.internalGetDayOfWeekNames().Clone();
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Array"));
				}
				if (value.Length != 7)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidArrayLength", new object[]
					{
						7
					}), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length);
				this.ClearTokenHashTable();
				this.dayNames = value;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06002DD5 RID: 11733 RVA: 0x000AEC15 File Offset: 0x000ACE15
		// (set) Token: 0x06002DD6 RID: 11734 RVA: 0x000AEC28 File Offset: 0x000ACE28
		[__DynamicallyInvokable]
		public string[] AbbreviatedMonthNames
		{
			[__DynamicallyInvokable]
			get
			{
				return (string[])this.internalGetAbbreviatedMonthNames().Clone();
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Array"));
				}
				if (value.Length != 13)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidArrayLength", new object[]
					{
						13
					}), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length - 1);
				this.ClearTokenHashTable();
				this.abbreviatedMonthNames = value;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06002DD7 RID: 11735 RVA: 0x000AECA9 File Offset: 0x000ACEA9
		// (set) Token: 0x06002DD8 RID: 11736 RVA: 0x000AECBC File Offset: 0x000ACEBC
		[__DynamicallyInvokable]
		public string[] MonthNames
		{
			[__DynamicallyInvokable]
			get
			{
				return (string[])this.internalGetMonthNames().Clone();
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Array"));
				}
				if (value.Length != 13)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidArrayLength", new object[]
					{
						13
					}), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length - 1);
				this.monthNames = value;
				this.ClearTokenHashTable();
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06002DD9 RID: 11737 RVA: 0x000AED3D File Offset: 0x000ACF3D
		internal bool HasSpacesInMonthNames
		{
			get
			{
				return (this.FormatFlags & DateTimeFormatFlags.UseSpacesInMonthNames) > DateTimeFormatFlags.None;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06002DDA RID: 11738 RVA: 0x000AED4A File Offset: 0x000ACF4A
		internal bool HasSpacesInDayNames
		{
			get
			{
				return (this.FormatFlags & DateTimeFormatFlags.UseSpacesInDayNames) > DateTimeFormatFlags.None;
			}
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x000AED58 File Offset: 0x000ACF58
		internal string internalGetMonthName(int month, MonthNameStyles style, bool abbreviated)
		{
			string[] array;
			if (style != MonthNameStyles.Genitive)
			{
				if (style != MonthNameStyles.LeapYear)
				{
					array = (abbreviated ? this.internalGetAbbreviatedMonthNames() : this.internalGetMonthNames());
				}
				else
				{
					array = this.internalGetLeapYearMonthNames();
				}
			}
			else
			{
				array = this.internalGetGenitiveMonthNames(abbreviated);
			}
			if (month < 1 || month > array.Length)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					1,
					array.Length
				}));
			}
			return array[month - 1];
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x000AEDD4 File Offset: 0x000ACFD4
		private string[] internalGetGenitiveMonthNames(bool abbreviated)
		{
			if (abbreviated)
			{
				if (this.m_genitiveAbbreviatedMonthNames == null)
				{
					this.m_genitiveAbbreviatedMonthNames = this.m_cultureData.AbbreviatedGenitiveMonthNames(this.Calendar.ID);
				}
				return this.m_genitiveAbbreviatedMonthNames;
			}
			if (this.genitiveMonthNames == null)
			{
				this.genitiveMonthNames = this.m_cultureData.GenitiveMonthNames(this.Calendar.ID);
			}
			return this.genitiveMonthNames;
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x000AEE39 File Offset: 0x000AD039
		internal string[] internalGetLeapYearMonthNames()
		{
			if (this.leapYearMonthNames == null)
			{
				this.leapYearMonthNames = this.m_cultureData.LeapYearMonthNames(this.Calendar.ID);
			}
			return this.leapYearMonthNames;
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x000AEE65 File Offset: 0x000AD065
		[__DynamicallyInvokable]
		public string GetAbbreviatedDayName(DayOfWeek dayofweek)
		{
			if (dayofweek < DayOfWeek.Sunday || dayofweek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException("dayofweek", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					DayOfWeek.Sunday,
					DayOfWeek.Saturday
				}));
			}
			return this.internalGetAbbreviatedDayOfWeekNames()[(int)dayofweek];
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x000AEEA4 File Offset: 0x000AD0A4
		[ComVisible(false)]
		public string GetShortestDayName(DayOfWeek dayOfWeek)
		{
			if (dayOfWeek < DayOfWeek.Sunday || dayOfWeek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException("dayOfWeek", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					DayOfWeek.Sunday,
					DayOfWeek.Saturday
				}));
			}
			return this.internalGetSuperShortDayNames()[(int)dayOfWeek];
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x000AEEE4 File Offset: 0x000AD0E4
		private static string[] GetCombinedPatterns(string[] patterns1, string[] patterns2, string connectString)
		{
			string[] array = new string[patterns1.Length * patterns2.Length];
			int num = 0;
			for (int i = 0; i < patterns1.Length; i++)
			{
				for (int j = 0; j < patterns2.Length; j++)
				{
					array[num++] = patterns1[i] + connectString + patterns2[j];
				}
			}
			return array;
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x000AEF30 File Offset: 0x000AD130
		public string[] GetAllDateTimePatterns()
		{
			List<string> list = new List<string>(132);
			for (int i = 0; i < DateTimeFormat.allStandardFormats.Length; i++)
			{
				string[] allDateTimePatterns = this.GetAllDateTimePatterns(DateTimeFormat.allStandardFormats[i]);
				for (int j = 0; j < allDateTimePatterns.Length; j++)
				{
					list.Add(allDateTimePatterns[j]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x000AEF88 File Offset: 0x000AD188
		public string[] GetAllDateTimePatterns(char format)
		{
			if (format <= 'U')
			{
				switch (format)
				{
				case 'D':
					return this.AllLongDatePatterns;
				case 'E':
					goto IL_1AF;
				case 'F':
					break;
				case 'G':
					return DateTimeFormatInfo.GetCombinedPatterns(this.AllShortDatePatterns, this.AllLongTimePatterns, " ");
				default:
					switch (format)
					{
					case 'M':
						goto IL_13D;
					case 'N':
					case 'P':
					case 'Q':
					case 'S':
						goto IL_1AF;
					case 'O':
						goto IL_14F;
					case 'R':
						goto IL_160;
					case 'T':
						return this.AllLongTimePatterns;
					case 'U':
						break;
					default:
						goto IL_1AF;
					}
					break;
				}
				return DateTimeFormatInfo.GetCombinedPatterns(this.AllLongDatePatterns, this.AllLongTimePatterns, " ");
			}
			if (format != 'Y')
			{
				switch (format)
				{
				case 'd':
					return this.AllShortDatePatterns;
				case 'e':
					goto IL_1AF;
				case 'f':
					return DateTimeFormatInfo.GetCombinedPatterns(this.AllLongDatePatterns, this.AllShortTimePatterns, " ");
				case 'g':
					return DateTimeFormatInfo.GetCombinedPatterns(this.AllShortDatePatterns, this.AllShortTimePatterns, " ");
				default:
					switch (format)
					{
					case 'm':
						goto IL_13D;
					case 'n':
					case 'p':
					case 'q':
					case 'v':
					case 'w':
					case 'x':
						goto IL_1AF;
					case 'o':
						goto IL_14F;
					case 'r':
						goto IL_160;
					case 's':
						return new string[]
						{
							"yyyy'-'MM'-'dd'T'HH':'mm':'ss"
						};
					case 't':
						return this.AllShortTimePatterns;
					case 'u':
						return new string[]
						{
							this.UniversalSortableDateTimePattern
						};
					case 'y':
						break;
					default:
						goto IL_1AF;
					}
					break;
				}
			}
			return this.AllYearMonthPatterns;
			IL_13D:
			return new string[]
			{
				this.MonthDayPattern
			};
			IL_14F:
			return new string[]
			{
				"yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK"
			};
			IL_160:
			return new string[]
			{
				"ddd, dd MMM yyyy HH':'mm':'ss 'GMT'"
			};
			IL_1AF:
			throw new ArgumentException(Environment.GetResourceString("Format_BadFormatSpecifier"), "format");
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x000AF15A File Offset: 0x000AD35A
		[__DynamicallyInvokable]
		public string GetDayName(DayOfWeek dayofweek)
		{
			if (dayofweek < DayOfWeek.Sunday || dayofweek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException("dayofweek", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					DayOfWeek.Sunday,
					DayOfWeek.Saturday
				}));
			}
			return this.internalGetDayOfWeekNames()[(int)dayofweek];
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x000AF19C File Offset: 0x000AD39C
		[__DynamicallyInvokable]
		public string GetAbbreviatedMonthName(int month)
		{
			if (month < 1 || month > 13)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					1,
					13
				}));
			}
			return this.internalGetAbbreviatedMonthNames()[month - 1];
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x000AF1EC File Offset: 0x000AD3EC
		[__DynamicallyInvokable]
		public string GetMonthName(int month)
		{
			if (month < 1 || month > 13)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					1,
					13
				}));
			}
			return this.internalGetMonthNames()[month - 1];
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x000AF23C File Offset: 0x000AD43C
		private static string[] GetMergedPatterns(string[] patterns, string defaultPattern)
		{
			if (defaultPattern == patterns[0])
			{
				return (string[])patterns.Clone();
			}
			int num = 0;
			while (num < patterns.Length && !(defaultPattern == patterns[num]))
			{
				num++;
			}
			string[] array;
			if (num < patterns.Length)
			{
				array = (string[])patterns.Clone();
				array[num] = array[0];
			}
			else
			{
				array = new string[patterns.Length + 1];
				Array.Copy(patterns, 0, array, 1, patterns.Length);
			}
			array[0] = defaultPattern;
			return array;
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06002DE7 RID: 11751 RVA: 0x000AF2AF File Offset: 0x000AD4AF
		private string[] AllYearMonthPatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedYearMonthPatterns, this.YearMonthPattern);
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06002DE8 RID: 11752 RVA: 0x000AF2C2 File Offset: 0x000AD4C2
		private string[] AllShortDatePatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedShortDatePatterns, this.ShortDatePattern);
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06002DE9 RID: 11753 RVA: 0x000AF2D5 File Offset: 0x000AD4D5
		private string[] AllShortTimePatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedShortTimePatterns, this.ShortTimePattern);
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06002DEA RID: 11754 RVA: 0x000AF2E8 File Offset: 0x000AD4E8
		private string[] AllLongDatePatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedLongDatePatterns, this.LongDatePattern);
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06002DEB RID: 11755 RVA: 0x000AF2FB File Offset: 0x000AD4FB
		private string[] AllLongTimePatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedLongTimePatterns, this.LongTimePattern);
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06002DEC RID: 11756 RVA: 0x000AF30E File Offset: 0x000AD50E
		private string[] UnclonedYearMonthPatterns
		{
			get
			{
				if (this.allYearMonthPatterns == null)
				{
					this.allYearMonthPatterns = this.m_cultureData.YearMonths(this.Calendar.ID);
				}
				return this.allYearMonthPatterns;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06002DED RID: 11757 RVA: 0x000AF33A File Offset: 0x000AD53A
		private string[] UnclonedShortDatePatterns
		{
			get
			{
				if (this.allShortDatePatterns == null)
				{
					this.allShortDatePatterns = this.m_cultureData.ShortDates(this.Calendar.ID);
				}
				return this.allShortDatePatterns;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06002DEE RID: 11758 RVA: 0x000AF366 File Offset: 0x000AD566
		private string[] UnclonedLongDatePatterns
		{
			get
			{
				if (this.allLongDatePatterns == null)
				{
					this.allLongDatePatterns = this.m_cultureData.LongDates(this.Calendar.ID);
				}
				return this.allLongDatePatterns;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06002DEF RID: 11759 RVA: 0x000AF392 File Offset: 0x000AD592
		private string[] UnclonedShortTimePatterns
		{
			get
			{
				if (this.allShortTimePatterns == null)
				{
					this.allShortTimePatterns = this.m_cultureData.ShortTimes;
				}
				return this.allShortTimePatterns;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06002DF0 RID: 11760 RVA: 0x000AF3B3 File Offset: 0x000AD5B3
		private string[] UnclonedLongTimePatterns
		{
			get
			{
				if (this.allLongTimePatterns == null)
				{
					this.allLongTimePatterns = this.m_cultureData.LongTimes;
				}
				return this.allLongTimePatterns;
			}
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x000AF3D4 File Offset: 0x000AD5D4
		[__DynamicallyInvokable]
		public static DateTimeFormatInfo ReadOnly(DateTimeFormatInfo dtfi)
		{
			if (dtfi == null)
			{
				throw new ArgumentNullException("dtfi", Environment.GetResourceString("ArgumentNull_Obj"));
			}
			if (dtfi.IsReadOnly)
			{
				return dtfi;
			}
			DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)dtfi.MemberwiseClone();
			dateTimeFormatInfo.calendar = Calendar.ReadOnly(dtfi.Calendar);
			dateTimeFormatInfo.m_isReadOnly = true;
			return dateTimeFormatInfo;
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06002DF2 RID: 11762 RVA: 0x000AF428 File Offset: 0x000AD628
		[__DynamicallyInvokable]
		public bool IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_isReadOnly;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x000AF430 File Offset: 0x000AD630
		[ComVisible(false)]
		public string NativeCalendarName
		{
			get
			{
				return this.m_cultureData.CalendarName(this.Calendar.ID);
			}
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x000AF448 File Offset: 0x000AD648
		[ComVisible(false)]
		public void SetAllDateTimePatterns(string[] patterns, char format)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
			}
			if (patterns == null)
			{
				throw new ArgumentNullException("patterns", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (patterns.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ArrayZeroError"), "patterns");
			}
			for (int i = 0; i < patterns.Length; i++)
			{
				if (patterns[i] == null)
				{
					throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_ArrayValue"));
				}
			}
			if (format <= 'Y')
			{
				if (format == 'D')
				{
					this.allLongDatePatterns = patterns;
					this.longDatePattern = this.allLongDatePatterns[0];
					goto IL_11E;
				}
				if (format == 'T')
				{
					this.allLongTimePatterns = patterns;
					this.longTimePattern = this.allLongTimePatterns[0];
					goto IL_11E;
				}
				if (format != 'Y')
				{
					goto IL_109;
				}
			}
			else
			{
				if (format == 'd')
				{
					this.allShortDatePatterns = patterns;
					this.shortDatePattern = this.allShortDatePatterns[0];
					goto IL_11E;
				}
				if (format == 't')
				{
					this.allShortTimePatterns = patterns;
					this.shortTimePattern = this.allShortTimePatterns[0];
					goto IL_11E;
				}
				if (format != 'y')
				{
					goto IL_109;
				}
			}
			this.allYearMonthPatterns = patterns;
			this.yearMonthPattern = this.allYearMonthPatterns[0];
			goto IL_11E;
			IL_109:
			throw new ArgumentException(Environment.GetResourceString("Format_BadFormatSpecifier"), "format");
			IL_11E:
			this.ClearTokenHashTable();
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06002DF5 RID: 11765 RVA: 0x000AF579 File Offset: 0x000AD779
		// (set) Token: 0x06002DF6 RID: 11766 RVA: 0x000AF58C File Offset: 0x000AD78C
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public string[] AbbreviatedMonthGenitiveNames
		{
			[__DynamicallyInvokable]
			get
			{
				return (string[])this.internalGetGenitiveMonthNames(true).Clone();
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Array"));
				}
				if (value.Length != 13)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidArrayLength", new object[]
					{
						13
					}), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length - 1);
				this.ClearTokenHashTable();
				this.m_genitiveAbbreviatedMonthNames = value;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06002DF7 RID: 11767 RVA: 0x000AF60D File Offset: 0x000AD80D
		// (set) Token: 0x06002DF8 RID: 11768 RVA: 0x000AF620 File Offset: 0x000AD820
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public string[] MonthGenitiveNames
		{
			[__DynamicallyInvokable]
			get
			{
				return (string[])this.internalGetGenitiveMonthNames(false).Clone();
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Array"));
				}
				if (value.Length != 13)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidArrayLength", new object[]
					{
						13
					}), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length - 1);
				this.genitiveMonthNames = value;
				this.ClearTokenHashTable();
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06002DF9 RID: 11769 RVA: 0x000AF6A4 File Offset: 0x000AD8A4
		internal string FullTimeSpanPositivePattern
		{
			get
			{
				if (this.m_fullTimeSpanPositivePattern == null)
				{
					CultureData cultureData;
					if (this.m_cultureData.UseUserOverride)
					{
						cultureData = CultureData.GetCultureData(this.m_cultureData.CultureName, false);
					}
					else
					{
						cultureData = this.m_cultureData;
					}
					string numberDecimalSeparator = new NumberFormatInfo(cultureData).NumberDecimalSeparator;
					this.m_fullTimeSpanPositivePattern = "d':'h':'mm':'ss'" + numberDecimalSeparator + "'FFFFFFF";
				}
				return this.m_fullTimeSpanPositivePattern;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06002DFA RID: 11770 RVA: 0x000AF709 File Offset: 0x000AD909
		internal string FullTimeSpanNegativePattern
		{
			get
			{
				if (this.m_fullTimeSpanNegativePattern == null)
				{
					this.m_fullTimeSpanNegativePattern = "'-'" + this.FullTimeSpanPositivePattern;
				}
				return this.m_fullTimeSpanNegativePattern;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06002DFB RID: 11771 RVA: 0x000AF72F File Offset: 0x000AD92F
		internal CompareInfo CompareInfo
		{
			get
			{
				if (this.m_compareInfo == null)
				{
					this.m_compareInfo = CompareInfo.GetCompareInfo(this.m_cultureData.SCOMPAREINFO);
				}
				return this.m_compareInfo;
			}
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x000AF758 File Offset: 0x000AD958
		internal static void ValidateStyles(DateTimeStyles style, string parameterName)
		{
			if ((style & ~(DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal | DateTimeStyles.RoundtripKind)) != DateTimeStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDateTimeStyles"), parameterName);
			}
			if ((style & DateTimeStyles.AssumeLocal) != DateTimeStyles.None && (style & DateTimeStyles.AssumeUniversal) != DateTimeStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ConflictingDateTimeStyles"), parameterName);
			}
			if ((style & DateTimeStyles.RoundtripKind) != DateTimeStyles.None && (style & (DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal)) != DateTimeStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ConflictingDateTimeRoundtripStyles"), parameterName);
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06002DFD RID: 11773 RVA: 0x000AF7BC File Offset: 0x000AD9BC
		internal DateTimeFormatFlags FormatFlags
		{
			get
			{
				if (this.formatFlags == DateTimeFormatFlags.NotInitialized)
				{
					this.formatFlags = DateTimeFormatFlags.None;
					this.formatFlags |= (DateTimeFormatFlags)DateTimeFormatInfoScanner.GetFormatFlagGenitiveMonth(this.MonthNames, this.internalGetGenitiveMonthNames(false), this.AbbreviatedMonthNames, this.internalGetGenitiveMonthNames(true));
					this.formatFlags |= (DateTimeFormatFlags)DateTimeFormatInfoScanner.GetFormatFlagUseSpaceInMonthNames(this.MonthNames, this.internalGetGenitiveMonthNames(false), this.AbbreviatedMonthNames, this.internalGetGenitiveMonthNames(true));
					this.formatFlags |= (DateTimeFormatFlags)DateTimeFormatInfoScanner.GetFormatFlagUseSpaceInDayNames(this.DayNames, this.AbbreviatedDayNames);
					this.formatFlags |= (DateTimeFormatFlags)DateTimeFormatInfoScanner.GetFormatFlagUseHebrewCalendar(this.Calendar.ID);
				}
				return this.formatFlags;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06002DFE RID: 11774 RVA: 0x000AF878 File Offset: 0x000ADA78
		internal bool HasForceTwoDigitYears
		{
			get
			{
				int id = this.calendar.ID;
				return id - 3 <= 1;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06002DFF RID: 11775 RVA: 0x000AF89A File Offset: 0x000ADA9A
		internal bool HasYearMonthAdjustment
		{
			get
			{
				return (this.FormatFlags & DateTimeFormatFlags.UseHebrewRule) > DateTimeFormatFlags.None;
			}
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x000AF8A8 File Offset: 0x000ADAA8
		internal bool YearMonthAdjustment(ref int year, ref int month, bool parsedMonthName)
		{
			if ((this.FormatFlags & DateTimeFormatFlags.UseHebrewRule) != DateTimeFormatFlags.None)
			{
				if (year < 1000)
				{
					year += 5000;
				}
				if (year < this.Calendar.GetYear(this.Calendar.MinSupportedDateTime) || year > this.Calendar.GetYear(this.Calendar.MaxSupportedDateTime))
				{
					return false;
				}
				if (parsedMonthName && !this.Calendar.IsLeapYear(year))
				{
					if (month >= 8)
					{
						month--;
					}
					else if (month == 7)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x000AF930 File Offset: 0x000ADB30
		internal static DateTimeFormatInfo GetJapaneseCalendarDTFI()
		{
			DateTimeFormatInfo dateTimeFormat = DateTimeFormatInfo.s_jajpDTFI;
			if (dateTimeFormat == null)
			{
				dateTimeFormat = new CultureInfo("ja-JP", false).DateTimeFormat;
				dateTimeFormat.Calendar = JapaneseCalendar.GetDefaultInstance();
				DateTimeFormatInfo.s_jajpDTFI = dateTimeFormat;
			}
			return dateTimeFormat;
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x000AF970 File Offset: 0x000ADB70
		internal static DateTimeFormatInfo GetTaiwanCalendarDTFI()
		{
			DateTimeFormatInfo dateTimeFormat = DateTimeFormatInfo.s_zhtwDTFI;
			if (dateTimeFormat == null)
			{
				dateTimeFormat = new CultureInfo("zh-TW", false).DateTimeFormat;
				dateTimeFormat.Calendar = TaiwanCalendar.GetDefaultInstance();
				DateTimeFormatInfo.s_zhtwDTFI = dateTimeFormat;
			}
			return dateTimeFormat;
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x000AF9AD File Offset: 0x000ADBAD
		private void ClearTokenHashTable()
		{
			this.m_dtfiTokenHash = null;
			this.formatFlags = DateTimeFormatFlags.NotInitialized;
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x000AF9C0 File Offset: 0x000ADBC0
		[SecurityCritical]
		internal TokenHashValue[] CreateTokenHashTable()
		{
			TokenHashValue[] array = this.m_dtfiTokenHash;
			if (array == null)
			{
				array = new TokenHashValue[199];
				bool flag = this.LanguageName.Equals("ko");
				string b = this.TimeSeparator.Trim();
				if ("," != b)
				{
					this.InsertHash(array, ",", TokenType.IgnorableSymbol, 0);
				}
				if ("." != b)
				{
					this.InsertHash(array, ".", TokenType.IgnorableSymbol, 0);
				}
				if ("시" != b && "時" != b && "时" != b)
				{
					this.InsertHash(array, this.TimeSeparator, TokenType.SEP_Time, 0);
				}
				this.InsertHash(array, this.AMDesignator, (TokenType)1027, 0);
				this.InsertHash(array, this.PMDesignator, (TokenType)1284, 1);
				if (this.LanguageName.Equals("sq"))
				{
					this.InsertHash(array, "." + this.AMDesignator, (TokenType)1027, 0);
					this.InsertHash(array, "." + this.PMDesignator, (TokenType)1284, 1);
				}
				this.InsertHash(array, "年", TokenType.SEP_YearSuff, 0);
				this.InsertHash(array, "년", TokenType.SEP_YearSuff, 0);
				this.InsertHash(array, "月", TokenType.SEP_MonthSuff, 0);
				this.InsertHash(array, "월", TokenType.SEP_MonthSuff, 0);
				this.InsertHash(array, "日", TokenType.SEP_DaySuff, 0);
				this.InsertHash(array, "일", TokenType.SEP_DaySuff, 0);
				this.InsertHash(array, "時", TokenType.SEP_HourSuff, 0);
				this.InsertHash(array, "时", TokenType.SEP_HourSuff, 0);
				this.InsertHash(array, "分", TokenType.SEP_MinuteSuff, 0);
				this.InsertHash(array, "秒", TokenType.SEP_SecondSuff, 0);
				if (!AppContextSwitches.EnforceLegacyJapaneseDateParsing && this.Calendar.ID == 3)
				{
					this.InsertHash(array, "元", TokenType.YearNumberToken, 1);
					this.InsertHash(array, "(", TokenType.IgnorableSymbol, 0);
					this.InsertHash(array, ")", TokenType.IgnorableSymbol, 0);
				}
				if (flag)
				{
					this.InsertHash(array, "시", TokenType.SEP_HourSuff, 0);
					this.InsertHash(array, "분", TokenType.SEP_MinuteSuff, 0);
					this.InsertHash(array, "초", TokenType.SEP_SecondSuff, 0);
				}
				if (this.LanguageName.Equals("ky"))
				{
					this.InsertHash(array, "-", TokenType.IgnorableSymbol, 0);
				}
				else
				{
					this.InsertHash(array, "-", TokenType.SEP_DateOrOffset, 0);
				}
				DateTimeFormatInfoScanner dateTimeFormatInfoScanner = new DateTimeFormatInfoScanner();
				string[] array2 = this.m_dateWords = dateTimeFormatInfoScanner.GetDateWordsOfDTFI(this);
				DateTimeFormatFlags dateTimeFormatFlags = this.FormatFlags;
				bool flag2 = false;
				if (array2 != null)
				{
					for (int i = 0; i < array2.Length; i++)
					{
						char c = array2[i][0];
						if (c != '')
						{
							if (c != '')
							{
								this.InsertHash(array, array2[i], TokenType.DateWordToken, 0);
								if (this.LanguageName.Equals("eu"))
								{
									this.InsertHash(array, "." + array2[i], TokenType.DateWordToken, 0);
								}
							}
							else
							{
								string text = array2[i].Substring(1);
								this.InsertHash(array, text, TokenType.IgnorableSymbol, 0);
								if (this.DateSeparator.Trim(null).Equals(text))
								{
									flag2 = true;
								}
							}
						}
						else
						{
							string monthPostfix = array2[i].Substring(1);
							this.AddMonthNames(array, monthPostfix);
						}
					}
				}
				if (!flag2)
				{
					this.InsertHash(array, this.DateSeparator, TokenType.SEP_Date, 0);
				}
				this.AddMonthNames(array, null);
				for (int j = 1; j <= 13; j++)
				{
					this.InsertHash(array, this.GetAbbreviatedMonthName(j), TokenType.MonthToken, j);
				}
				if ((this.FormatFlags & DateTimeFormatFlags.UseGenitiveMonth) != DateTimeFormatFlags.None)
				{
					for (int k = 1; k <= 13; k++)
					{
						string str = this.internalGetMonthName(k, MonthNameStyles.Genitive, false);
						this.InsertHash(array, str, TokenType.MonthToken, k);
					}
				}
				if ((this.FormatFlags & DateTimeFormatFlags.UseLeapYearMonth) != DateTimeFormatFlags.None)
				{
					for (int l = 1; l <= 13; l++)
					{
						string str2 = this.internalGetMonthName(l, MonthNameStyles.LeapYear, false);
						this.InsertHash(array, str2, TokenType.MonthToken, l);
					}
				}
				for (int m = 0; m < 7; m++)
				{
					string str3 = this.GetDayName((DayOfWeek)m);
					this.InsertHash(array, str3, TokenType.DayOfWeekToken, m);
					str3 = this.GetAbbreviatedDayName((DayOfWeek)m);
					this.InsertHash(array, str3, TokenType.DayOfWeekToken, m);
				}
				int[] eras = this.calendar.Eras;
				for (int n = 1; n <= eras.Length; n++)
				{
					this.InsertHash(array, this.GetEraName(n), TokenType.EraToken, n);
					this.InsertHash(array, this.GetAbbreviatedEraName(n), TokenType.EraToken, n);
				}
				if (this.LanguageName.Equals("ja"))
				{
					for (int num = 0; num < 7; num++)
					{
						string str4 = "(" + this.GetAbbreviatedDayName((DayOfWeek)num) + ")";
						this.InsertHash(array, str4, TokenType.DayOfWeekToken, num);
					}
					if (this.Calendar.GetType() != typeof(JapaneseCalendar))
					{
						DateTimeFormatInfo japaneseCalendarDTFI = DateTimeFormatInfo.GetJapaneseCalendarDTFI();
						for (int num2 = 1; num2 <= japaneseCalendarDTFI.Calendar.Eras.Length; num2++)
						{
							this.InsertHash(array, japaneseCalendarDTFI.GetEraName(num2), TokenType.JapaneseEraToken, num2);
							this.InsertHash(array, japaneseCalendarDTFI.GetAbbreviatedEraName(num2), TokenType.JapaneseEraToken, num2);
							this.InsertHash(array, japaneseCalendarDTFI.AbbreviatedEnglishEraNames[num2 - 1], TokenType.JapaneseEraToken, num2);
						}
					}
				}
				else if (this.CultureName.Equals("zh-TW"))
				{
					DateTimeFormatInfo taiwanCalendarDTFI = DateTimeFormatInfo.GetTaiwanCalendarDTFI();
					for (int num3 = 1; num3 <= taiwanCalendarDTFI.Calendar.Eras.Length; num3++)
					{
						if (taiwanCalendarDTFI.GetEraName(num3).Length > 0)
						{
							this.InsertHash(array, taiwanCalendarDTFI.GetEraName(num3), TokenType.TEraToken, num3);
						}
					}
				}
				this.InsertHash(array, DateTimeFormatInfo.InvariantInfo.AMDesignator, (TokenType)1027, 0);
				this.InsertHash(array, DateTimeFormatInfo.InvariantInfo.PMDesignator, (TokenType)1284, 1);
				for (int num4 = 1; num4 <= 12; num4++)
				{
					string str5 = DateTimeFormatInfo.InvariantInfo.GetMonthName(num4);
					this.InsertHash(array, str5, TokenType.MonthToken, num4);
					str5 = DateTimeFormatInfo.InvariantInfo.GetAbbreviatedMonthName(num4);
					this.InsertHash(array, str5, TokenType.MonthToken, num4);
				}
				for (int num5 = 0; num5 < 7; num5++)
				{
					string str6 = DateTimeFormatInfo.InvariantInfo.GetDayName((DayOfWeek)num5);
					this.InsertHash(array, str6, TokenType.DayOfWeekToken, num5);
					str6 = DateTimeFormatInfo.InvariantInfo.GetAbbreviatedDayName((DayOfWeek)num5);
					this.InsertHash(array, str6, TokenType.DayOfWeekToken, num5);
				}
				for (int num6 = 0; num6 < this.AbbreviatedEnglishEraNames.Length; num6++)
				{
					this.InsertHash(array, this.AbbreviatedEnglishEraNames[num6], TokenType.EraToken, num6 + 1);
				}
				this.InsertHash(array, "T", TokenType.SEP_LocalTimeMark, 0);
				this.InsertHash(array, "GMT", TokenType.TimeZoneToken, 0);
				this.InsertHash(array, "Z", TokenType.TimeZoneToken, 0);
				this.InsertHash(array, "/", TokenType.SEP_Date, 0);
				this.InsertHash(array, ":", TokenType.SEP_Time, 0);
				this.m_dtfiTokenHash = array;
			}
			return array;
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x000B00C4 File Offset: 0x000AE2C4
		private void AddMonthNames(TokenHashValue[] temp, string monthPostfix)
		{
			for (int i = 1; i <= 13; i++)
			{
				string text = this.GetMonthName(i);
				if (text.Length > 0)
				{
					if (monthPostfix != null)
					{
						this.InsertHash(temp, text + monthPostfix, TokenType.MonthToken, i);
					}
					else
					{
						this.InsertHash(temp, text, TokenType.MonthToken, i);
					}
				}
				text = this.GetAbbreviatedMonthName(i);
				this.InsertHash(temp, text, TokenType.MonthToken, i);
			}
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x000B0120 File Offset: 0x000AE320
		private static bool TryParseHebrewNumber(ref __DTString str, out bool badFormat, out int number)
		{
			number = -1;
			badFormat = false;
			int index = str.Index;
			if (!HebrewNumber.IsDigit(str.Value[index]))
			{
				return false;
			}
			HebrewNumberParsingContext hebrewNumberParsingContext = new HebrewNumberParsingContext(0);
			HebrewNumberParsingState hebrewNumberParsingState;
			for (;;)
			{
				hebrewNumberParsingState = HebrewNumber.ParseByChar(str.Value[index++], ref hebrewNumberParsingContext);
				if (hebrewNumberParsingState <= HebrewNumberParsingState.NotHebrewDigit)
				{
					break;
				}
				if (index >= str.Value.Length || hebrewNumberParsingState == HebrewNumberParsingState.FoundEndOfHebrewNumber)
				{
					goto IL_5A;
				}
			}
			return false;
			IL_5A:
			if (hebrewNumberParsingState != HebrewNumberParsingState.FoundEndOfHebrewNumber)
			{
				return false;
			}
			str.Advance(index - str.Index);
			number = hebrewNumberParsingContext.result;
			return true;
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x000B01A5 File Offset: 0x000AE3A5
		private static bool IsHebrewChar(char ch)
		{
			return ch >= '֐' && ch <= '׿';
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x000B01BC File Offset: 0x000AE3BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool IsAllowedJapaneseTokenFollowedByNonSpaceLetter(string tokenString, char nextCh)
		{
			return !AppContextSwitches.EnforceLegacyJapaneseDateParsing && this.Calendar.ID == 3 && (nextCh == "元"[0] || (tokenString == "元" && nextCh == "年"[0]));
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x000B020C File Offset: 0x000AE40C
		[SecurityCritical]
		internal bool Tokenize(TokenType TokenMask, out TokenType tokenType, out int tokenValue, ref __DTString str)
		{
			tokenType = TokenType.UnknownToken;
			tokenValue = 0;
			char c = str.m_current;
			bool flag = char.IsLetter(c);
			if (flag)
			{
				c = char.ToLower(c, this.Culture);
				bool flag2;
				if (DateTimeFormatInfo.IsHebrewChar(c) && TokenMask == TokenType.RegularTokenMask && DateTimeFormatInfo.TryParseHebrewNumber(ref str, out flag2, out tokenValue))
				{
					if (flag2)
					{
						tokenType = TokenType.UnknownToken;
						return false;
					}
					tokenType = TokenType.HebrewNumber;
					return true;
				}
			}
			int num = (int)(c % 'Ç');
			int num2 = (int)('\u0001' + c % 'Å');
			int num3 = str.len - str.Index;
			int num4 = 0;
			TokenHashValue[] array = this.m_dtfiTokenHash;
			if (array == null)
			{
				array = this.CreateTokenHashTable();
			}
			TokenHashValue tokenHashValue;
			int count;
			int count2;
			for (;;)
			{
				tokenHashValue = array[num];
				if (tokenHashValue == null)
				{
					return false;
				}
				if ((tokenHashValue.tokenType & TokenMask) > (TokenType)0 && tokenHashValue.tokenString.Length <= num3)
				{
					if (string.Compare(str.Value, str.Index, tokenHashValue.tokenString, 0, tokenHashValue.tokenString.Length, this.Culture, CompareOptions.IgnoreCase) == 0)
					{
						break;
					}
					if (tokenHashValue.tokenType == TokenType.MonthToken && this.HasSpacesInMonthNames)
					{
						count = 0;
						if (str.MatchSpecifiedWords(tokenHashValue.tokenString, true, ref count))
						{
							goto Block_17;
						}
					}
					else if (tokenHashValue.tokenType == TokenType.DayOfWeekToken && this.HasSpacesInDayNames)
					{
						count2 = 0;
						if (str.MatchSpecifiedWords(tokenHashValue.tokenString, true, ref count2))
						{
							goto Block_20;
						}
					}
				}
				num4++;
				num += num2;
				if (num >= 199)
				{
					num -= 199;
				}
				if (num4 >= 199)
				{
					return false;
				}
			}
			int index;
			if (flag && (index = str.Index + tokenHashValue.tokenString.Length) < str.len)
			{
				char c2 = str.Value[index];
				if (char.IsLetter(c2) && !this.IsAllowedJapaneseTokenFollowedByNonSpaceLetter(tokenHashValue.tokenString, c2))
				{
					return false;
				}
			}
			tokenType = (tokenHashValue.tokenType & TokenMask);
			tokenValue = tokenHashValue.tokenValue;
			str.Advance(tokenHashValue.tokenString.Length);
			return true;
			Block_17:
			tokenType = (tokenHashValue.tokenType & TokenMask);
			tokenValue = tokenHashValue.tokenValue;
			str.Advance(count);
			return true;
			Block_20:
			tokenType = (tokenHashValue.tokenType & TokenMask);
			tokenValue = tokenHashValue.tokenValue;
			str.Advance(count2);
			return true;
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x000B0424 File Offset: 0x000AE624
		private void InsertAtCurrentHashNode(TokenHashValue[] hashTable, string str, char ch, TokenType tokenType, int tokenValue, int pos, int hashcode, int hashProbe)
		{
			TokenHashValue tokenHashValue = hashTable[hashcode];
			hashTable[hashcode] = new TokenHashValue(str, tokenType, tokenValue);
			while (++pos < 199)
			{
				hashcode += hashProbe;
				if (hashcode >= 199)
				{
					hashcode -= 199;
				}
				TokenHashValue tokenHashValue2 = hashTable[hashcode];
				if (tokenHashValue2 == null || char.ToLower(tokenHashValue2.tokenString[0], this.Culture) == ch)
				{
					hashTable[hashcode] = tokenHashValue;
					if (tokenHashValue2 == null)
					{
						return;
					}
					tokenHashValue = tokenHashValue2;
				}
			}
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x000B049C File Offset: 0x000AE69C
		private void InsertHash(TokenHashValue[] hashTable, string str, TokenType tokenType, int tokenValue)
		{
			if (str == null || str.Length == 0)
			{
				return;
			}
			int num = 0;
			if (char.IsWhiteSpace(str[0]) || char.IsWhiteSpace(str[str.Length - 1]))
			{
				str = str.Trim(null);
				if (str.Length == 0)
				{
					return;
				}
			}
			char c = char.ToLower(str[0], this.Culture);
			int num2 = (int)(c % 'Ç');
			int num3 = (int)('\u0001' + c % 'Å');
			for (;;)
			{
				TokenHashValue tokenHashValue = hashTable[num2];
				if (tokenHashValue == null)
				{
					break;
				}
				if (str.Length >= tokenHashValue.tokenString.Length && string.Compare(str, 0, tokenHashValue.tokenString, 0, tokenHashValue.tokenString.Length, this.Culture, CompareOptions.IgnoreCase) == 0)
				{
					if (str.Length > tokenHashValue.tokenString.Length)
					{
						goto Block_7;
					}
					int tokenType2 = (int)tokenHashValue.tokenType;
					if (DateTimeFormatInfo.preferExistingTokens || BinaryCompatibility.TargetsAtLeast_Desktop_V4_5_1)
					{
						if (((tokenType2 & 255) == 0 && (tokenType & TokenType.RegularTokenMask) != (TokenType)0) || ((tokenType2 & 65280) == 0 && (tokenType & TokenType.SeparatorTokenMask) != (TokenType)0))
						{
							tokenHashValue.tokenType |= tokenType;
							if (tokenValue != 0)
							{
								tokenHashValue.tokenValue = tokenValue;
							}
						}
					}
					else if (((tokenType | (TokenType)tokenType2) & TokenType.RegularTokenMask) == tokenType || ((tokenType | (TokenType)tokenType2) & TokenType.SeparatorTokenMask) == tokenType)
					{
						tokenHashValue.tokenType |= tokenType;
						if (tokenValue != 0)
						{
							tokenHashValue.tokenValue = tokenValue;
						}
					}
				}
				num++;
				num2 += num3;
				if (num2 >= 199)
				{
					num2 -= 199;
				}
				if (num >= 199)
				{
					return;
				}
			}
			hashTable[num2] = new TokenHashValue(str, tokenType, tokenValue);
			return;
			Block_7:
			this.InsertAtCurrentHashNode(hashTable, str, c, tokenType, tokenValue, num, num2, num3);
		}

		// Token: 0x040012BE RID: 4798
		private static volatile DateTimeFormatInfo invariantInfo;

		// Token: 0x040012BF RID: 4799
		[NonSerialized]
		private CultureData m_cultureData;

		// Token: 0x040012C0 RID: 4800
		[OptionalField(VersionAdded = 2)]
		internal string m_name;

		// Token: 0x040012C1 RID: 4801
		[NonSerialized]
		private string m_langName;

		// Token: 0x040012C2 RID: 4802
		[NonSerialized]
		private CompareInfo m_compareInfo;

		// Token: 0x040012C3 RID: 4803
		[NonSerialized]
		private CultureInfo m_cultureInfo;

		// Token: 0x040012C4 RID: 4804
		internal string amDesignator;

		// Token: 0x040012C5 RID: 4805
		internal string pmDesignator;

		// Token: 0x040012C6 RID: 4806
		[OptionalField(VersionAdded = 1)]
		internal string dateSeparator;

		// Token: 0x040012C7 RID: 4807
		[OptionalField(VersionAdded = 1)]
		internal string generalShortTimePattern;

		// Token: 0x040012C8 RID: 4808
		[OptionalField(VersionAdded = 1)]
		internal string generalLongTimePattern;

		// Token: 0x040012C9 RID: 4809
		[OptionalField(VersionAdded = 1)]
		internal string timeSeparator;

		// Token: 0x040012CA RID: 4810
		internal string monthDayPattern;

		// Token: 0x040012CB RID: 4811
		[OptionalField(VersionAdded = 2)]
		internal string dateTimeOffsetPattern;

		// Token: 0x040012CC RID: 4812
		internal const string rfc1123Pattern = "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";

		// Token: 0x040012CD RID: 4813
		internal const string sortableDateTimePattern = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";

		// Token: 0x040012CE RID: 4814
		internal const string universalSortableDateTimePattern = "yyyy'-'MM'-'dd HH':'mm':'ss'Z'";

		// Token: 0x040012CF RID: 4815
		internal Calendar calendar;

		// Token: 0x040012D0 RID: 4816
		internal int firstDayOfWeek = -1;

		// Token: 0x040012D1 RID: 4817
		internal int calendarWeekRule = -1;

		// Token: 0x040012D2 RID: 4818
		[OptionalField(VersionAdded = 1)]
		internal string fullDateTimePattern;

		// Token: 0x040012D3 RID: 4819
		internal string[] abbreviatedDayNames;

		// Token: 0x040012D4 RID: 4820
		[OptionalField(VersionAdded = 2)]
		internal string[] m_superShortDayNames;

		// Token: 0x040012D5 RID: 4821
		internal string[] dayNames;

		// Token: 0x040012D6 RID: 4822
		internal string[] abbreviatedMonthNames;

		// Token: 0x040012D7 RID: 4823
		internal string[] monthNames;

		// Token: 0x040012D8 RID: 4824
		[OptionalField(VersionAdded = 2)]
		internal string[] genitiveMonthNames;

		// Token: 0x040012D9 RID: 4825
		[OptionalField(VersionAdded = 2)]
		internal string[] m_genitiveAbbreviatedMonthNames;

		// Token: 0x040012DA RID: 4826
		[OptionalField(VersionAdded = 2)]
		internal string[] leapYearMonthNames;

		// Token: 0x040012DB RID: 4827
		internal string longDatePattern;

		// Token: 0x040012DC RID: 4828
		internal string shortDatePattern;

		// Token: 0x040012DD RID: 4829
		internal string yearMonthPattern;

		// Token: 0x040012DE RID: 4830
		internal string longTimePattern;

		// Token: 0x040012DF RID: 4831
		internal string shortTimePattern;

		// Token: 0x040012E0 RID: 4832
		[OptionalField(VersionAdded = 3)]
		private string[] allYearMonthPatterns;

		// Token: 0x040012E1 RID: 4833
		internal string[] allShortDatePatterns;

		// Token: 0x040012E2 RID: 4834
		internal string[] allLongDatePatterns;

		// Token: 0x040012E3 RID: 4835
		internal string[] allShortTimePatterns;

		// Token: 0x040012E4 RID: 4836
		internal string[] allLongTimePatterns;

		// Token: 0x040012E5 RID: 4837
		internal string[] m_eraNames;

		// Token: 0x040012E6 RID: 4838
		internal string[] m_abbrevEraNames;

		// Token: 0x040012E7 RID: 4839
		internal string[] m_abbrevEnglishEraNames;

		// Token: 0x040012E8 RID: 4840
		internal int[] optionalCalendars;

		// Token: 0x040012E9 RID: 4841
		private const int DEFAULT_ALL_DATETIMES_SIZE = 132;

		// Token: 0x040012EA RID: 4842
		internal bool m_isReadOnly;

		// Token: 0x040012EB RID: 4843
		[OptionalField(VersionAdded = 2)]
		internal DateTimeFormatFlags formatFlags = DateTimeFormatFlags.NotInitialized;

		// Token: 0x040012EC RID: 4844
		internal static bool preferExistingTokens = DateTimeFormatInfo.InitPreferExistingTokens();

		// Token: 0x040012ED RID: 4845
		[OptionalField(VersionAdded = 1)]
		private int CultureID;

		// Token: 0x040012EE RID: 4846
		[OptionalField(VersionAdded = 1)]
		private bool m_useUserOverride;

		// Token: 0x040012EF RID: 4847
		[OptionalField(VersionAdded = 1)]
		private bool bUseCalendarInfo;

		// Token: 0x040012F0 RID: 4848
		[OptionalField(VersionAdded = 1)]
		private int nDataItem;

		// Token: 0x040012F1 RID: 4849
		[OptionalField(VersionAdded = 2)]
		internal bool m_isDefaultCalendar;

		// Token: 0x040012F2 RID: 4850
		[OptionalField(VersionAdded = 2)]
		private static volatile Hashtable s_calendarNativeNames;

		// Token: 0x040012F3 RID: 4851
		[OptionalField(VersionAdded = 1)]
		internal string[] m_dateWords;

		// Token: 0x040012F4 RID: 4852
		private static char[] MonthSpaces = new char[]
		{
			' ',
			'\u00a0'
		};

		// Token: 0x040012F5 RID: 4853
		[NonSerialized]
		private string m_fullTimeSpanPositivePattern;

		// Token: 0x040012F6 RID: 4854
		[NonSerialized]
		private string m_fullTimeSpanNegativePattern;

		// Token: 0x040012F7 RID: 4855
		internal const DateTimeStyles InvalidDateTimeStyles = ~(DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal | DateTimeStyles.RoundtripKind);

		// Token: 0x040012F8 RID: 4856
		[NonSerialized]
		private TokenHashValue[] m_dtfiTokenHash;

		// Token: 0x040012F9 RID: 4857
		private const int TOKEN_HASH_SIZE = 199;

		// Token: 0x040012FA RID: 4858
		private const int SECOND_PRIME = 197;

		// Token: 0x040012FB RID: 4859
		private const string dateSeparatorOrTimeZoneOffset = "-";

		// Token: 0x040012FC RID: 4860
		private const string invariantDateSeparator = "/";

		// Token: 0x040012FD RID: 4861
		private const string invariantTimeSeparator = ":";

		// Token: 0x040012FE RID: 4862
		internal const string IgnorablePeriod = ".";

		// Token: 0x040012FF RID: 4863
		internal const string IgnorableComma = ",";

		// Token: 0x04001300 RID: 4864
		internal const string CJKYearSuff = "年";

		// Token: 0x04001301 RID: 4865
		internal const string CJKMonthSuff = "月";

		// Token: 0x04001302 RID: 4866
		internal const string CJKDaySuff = "日";

		// Token: 0x04001303 RID: 4867
		internal const string KoreanYearSuff = "년";

		// Token: 0x04001304 RID: 4868
		internal const string KoreanMonthSuff = "월";

		// Token: 0x04001305 RID: 4869
		internal const string KoreanDaySuff = "일";

		// Token: 0x04001306 RID: 4870
		internal const string KoreanHourSuff = "시";

		// Token: 0x04001307 RID: 4871
		internal const string KoreanMinuteSuff = "분";

		// Token: 0x04001308 RID: 4872
		internal const string KoreanSecondSuff = "초";

		// Token: 0x04001309 RID: 4873
		internal const string CJKHourSuff = "時";

		// Token: 0x0400130A RID: 4874
		internal const string ChineseHourSuff = "时";

		// Token: 0x0400130B RID: 4875
		internal const string CJKMinuteSuff = "分";

		// Token: 0x0400130C RID: 4876
		internal const string CJKSecondSuff = "秒";

		// Token: 0x0400130D RID: 4877
		internal const string JapaneseEraStart = "元";

		// Token: 0x0400130E RID: 4878
		internal const string LocalTimeMark = "T";

		// Token: 0x0400130F RID: 4879
		internal const string KoreanLangName = "ko";

		// Token: 0x04001310 RID: 4880
		internal const string JapaneseLangName = "ja";

		// Token: 0x04001311 RID: 4881
		internal const string EnglishLangName = "en";

		// Token: 0x04001312 RID: 4882
		private static volatile DateTimeFormatInfo s_jajpDTFI;

		// Token: 0x04001313 RID: 4883
		private static volatile DateTimeFormatInfo s_zhtwDTFI;
	}
}
