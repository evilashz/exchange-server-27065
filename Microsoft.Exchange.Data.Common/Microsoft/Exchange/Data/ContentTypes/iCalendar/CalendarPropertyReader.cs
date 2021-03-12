using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.ContentTypes.Internal;

namespace Microsoft.Exchange.Data.ContentTypes.iCalendar
{
	// Token: 0x020000B1 RID: 177
	public struct CalendarPropertyReader
	{
		// Token: 0x060006FF RID: 1791 RVA: 0x00027DA3 File Offset: 0x00025FA3
		internal CalendarPropertyReader(ContentLineReader reader)
		{
			this.reader = reader;
			this.lastSeparator = CalendarValueSeparators.None;
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x00027DB3 File Offset: 0x00025FB3
		internal CalendarValueSeparators LastValueSeparator
		{
			get
			{
				return this.lastSeparator;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x00027DBB File Offset: 0x00025FBB
		public PropertyId PropertyId
		{
			get
			{
				this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
				return CalendarCommon.GetPropertyEnum(this.reader.PropertyName);
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x00027DDC File Offset: 0x00025FDC
		public CalendarValueType ValueType
		{
			get
			{
				this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
				CalendarValueTypeContainer calendarValueTypeContainer = this.reader.ValueType as CalendarValueTypeContainer;
				return calendarValueTypeContainer.ValueType;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00027E0D File Offset: 0x0002600D
		public string Name
		{
			get
			{
				this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
				return this.reader.PropertyName;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x00027E27 File Offset: 0x00026027
		public CalendarParameterReader ParameterReader
		{
			get
			{
				this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
				return new CalendarParameterReader(this.reader);
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00027E41 File Offset: 0x00026041
		internal object ReadValue(CalendarValueSeparators expectedSeparators)
		{
			return this.ReadValue(new CalendarValueSeparators?(expectedSeparators));
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00027E50 File Offset: 0x00026050
		private object ReadValue(CalendarValueSeparators? expectedSeparators)
		{
			while (this.reader.ReadNextParameter())
			{
			}
			CalendarValueType valueType = this.ValueType;
			if (valueType <= CalendarValueType.Float)
			{
				if (valueType <= CalendarValueType.Date)
				{
					switch (valueType)
					{
					case CalendarValueType.Binary:
						return this.ReadValueAsBytes();
					case CalendarValueType.Unknown | CalendarValueType.Binary:
						break;
					case CalendarValueType.Boolean:
						return this.ReadValueAsBoolean(expectedSeparators);
					default:
						if (valueType == CalendarValueType.Date)
						{
							return this.ReadValueAsDateTime(expectedSeparators);
						}
						break;
					}
				}
				else
				{
					if (valueType == CalendarValueType.DateTime)
					{
						return this.ReadValueAsDateTime(expectedSeparators);
					}
					if (valueType == CalendarValueType.Duration)
					{
						return this.ReadValueAsTimeSpan(expectedSeparators);
					}
					if (valueType == CalendarValueType.Float)
					{
						return this.ReadValueAsFloat(expectedSeparators);
					}
				}
			}
			else if (valueType <= CalendarValueType.Period)
			{
				if (valueType == CalendarValueType.Integer)
				{
					return this.ReadValueAsInt32(expectedSeparators);
				}
				if (valueType == CalendarValueType.Period)
				{
					return this.ReadValueAsCalendarPeriod(expectedSeparators);
				}
			}
			else
			{
				if (valueType == CalendarValueType.Recurrence)
				{
					return this.ReadValueAsRecurrence(expectedSeparators);
				}
				if (valueType == CalendarValueType.Time)
				{
					return this.ReadValueAsCalendarTime(expectedSeparators);
				}
				if (valueType == CalendarValueType.UtcOffset)
				{
					return this.ReadValueAsTimeSpan(expectedSeparators);
				}
			}
			return this.ReadValueAsString(expectedSeparators);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00027F88 File Offset: 0x00026188
		public object ReadValue()
		{
			return this.ReadValue(null);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00027FA4 File Offset: 0x000261A4
		public byte[] ReadValueAsBytes()
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(256))
			{
				using (Stream valueReadStream = this.GetValueReadStream())
				{
					byte[] array = new byte[256];
					for (int i = valueReadStream.Read(array, 0, array.Length); i > 0; i = valueReadStream.Read(array, 0, array.Length))
					{
						memoryStream.Write(array, 0, i);
					}
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00028040 File Offset: 0x00026240
		private string ReadValueAsString(CalendarValueSeparators? expectedSeparators)
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
			ContentLineParser.Separators separators;
			string result;
			if (expectedSeparators != null)
			{
				result = this.reader.ReadPropertyValue(true, (ContentLineParser.Separators)expectedSeparators.Value, false, out separators);
			}
			else
			{
				result = this.reader.ReadPropertyValue(true, ContentLineParser.Separators.None, true, out separators);
			}
			this.lastSeparator = (CalendarValueSeparators)separators;
			return result;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00028097 File Offset: 0x00026297
		internal string ReadValueAsString(CalendarValueSeparators expectedSeparators)
		{
			return this.ReadValueAsString(new CalendarValueSeparators?(expectedSeparators));
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x000280A8 File Offset: 0x000262A8
		public string ReadValueAsString()
		{
			return this.ReadValueAsString(null);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x000280C4 File Offset: 0x000262C4
		private CalendarTime ReadValueAsCalendarTime(CalendarValueSeparators? expectedSeparators)
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
			string s = this.ReadValueAsString(expectedSeparators).Trim();
			this.CheckType(CalendarValueType.Time);
			return new CalendarTime(s, this.reader.ComplianceTracker);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00028107 File Offset: 0x00026307
		internal CalendarTime ReadValueAsCalendarTime(CalendarValueSeparators expectedSeparators)
		{
			return this.ReadValueAsCalendarTime(new CalendarValueSeparators?(expectedSeparators));
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00028118 File Offset: 0x00026318
		public CalendarTime ReadValueAsCalendarTime()
		{
			return this.ReadValueAsCalendarTime(null);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00028134 File Offset: 0x00026334
		private DateTime ReadValueAsDateTime(CalendarValueSeparators? expectedSeparators)
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
			string s = this.ReadValueAsString(expectedSeparators).Trim();
			if (CalendarValueType.Date == this.ValueType)
			{
				return CalendarCommon.ParseDate(s, this.reader.ComplianceTracker);
			}
			this.CheckType(CalendarValueType.DateTime);
			return CalendarCommon.ParseDateTime(s, this.reader.ComplianceTracker);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00028190 File Offset: 0x00026390
		internal DateTime ReadValueAsDateTime(CalendarValueSeparators expectedSeparators)
		{
			return this.ReadValueAsDateTime(new CalendarValueSeparators?(expectedSeparators));
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x000281A0 File Offset: 0x000263A0
		public DateTime ReadValueAsDateTime()
		{
			return this.ReadValueAsDateTime(null);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x000281BC File Offset: 0x000263BC
		private DateTime ReadValueAsDateTime(CalendarValueType valueType, CalendarValueSeparators? expectedSeparators)
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
			string s = this.ReadValueAsString(expectedSeparators).Trim();
			if (CalendarValueType.DateTime == valueType || CalendarValueType.Text == valueType)
			{
				this.CheckType(CalendarValueType.DateTime);
				return CalendarCommon.ParseDateTime(s, this.reader.ComplianceTracker);
			}
			if (CalendarValueType.Date == valueType)
			{
				this.CheckType(CalendarValueType.Date);
				return CalendarCommon.ParseDate(s, this.reader.ComplianceTracker);
			}
			throw new ArgumentOutOfRangeException("valueType");
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00028233 File Offset: 0x00026433
		internal DateTime ReadValueAsDateTime(CalendarValueType valueType, CalendarValueSeparators expectedSeparators)
		{
			return this.ReadValueAsDateTime(valueType, new CalendarValueSeparators?(expectedSeparators));
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00028244 File Offset: 0x00026444
		public DateTime ReadValueAsDateTime(CalendarValueType valueType)
		{
			return this.ReadValueAsDateTime(valueType, null);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00028264 File Offset: 0x00026464
		private TimeSpan ReadValueAsTimeSpan(CalendarValueSeparators? expectedSeparators)
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
			string s = this.ReadValueAsString(expectedSeparators).Trim();
			if (CalendarValueType.UtcOffset == this.ValueType)
			{
				return CalendarCommon.ParseUtcOffset(s, this.reader.ComplianceTracker);
			}
			this.CheckType(CalendarValueType.Duration);
			return CalendarCommon.ParseDuration(s, this.reader.ComplianceTracker);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x000282C3 File Offset: 0x000264C3
		internal TimeSpan ReadValueAsTimeSpan(CalendarValueSeparators expectedSeparators)
		{
			return this.ReadValueAsTimeSpan(new CalendarValueSeparators?(expectedSeparators));
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x000282D4 File Offset: 0x000264D4
		public TimeSpan ReadValueAsTimeSpan()
		{
			return this.ReadValueAsTimeSpan(null);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x000282F0 File Offset: 0x000264F0
		private TimeSpan ReadValueAsTimeSpan(CalendarValueType valueType, CalendarValueSeparators? expectedSeparators)
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
			string s = this.ReadValueAsString(expectedSeparators).Trim();
			if (CalendarValueType.Duration == valueType)
			{
				this.CheckType(CalendarValueType.Duration);
				return CalendarCommon.ParseDuration(s, this.reader.ComplianceTracker);
			}
			if (CalendarValueType.UtcOffset == valueType)
			{
				this.CheckType(CalendarValueType.UtcOffset);
				return CalendarCommon.ParseUtcOffset(s, this.reader.ComplianceTracker);
			}
			throw new ArgumentOutOfRangeException("valueType");
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00028365 File Offset: 0x00026565
		internal TimeSpan ReadValueAsTimeSpan(CalendarValueType valueType, CalendarValueSeparators expectedSeparators)
		{
			return this.ReadValueAsTimeSpan(valueType, new CalendarValueSeparators?(expectedSeparators));
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00028374 File Offset: 0x00026574
		public TimeSpan ReadValueAsTimeSpan(CalendarValueType valueType)
		{
			return this.ReadValueAsTimeSpan(valueType, null);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00028394 File Offset: 0x00026594
		private bool ReadValueAsBoolean(CalendarValueSeparators? expectedSeparators)
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
			string value = this.ReadValueAsString(expectedSeparators).Trim();
			this.CheckType(CalendarValueType.Boolean);
			bool result;
			if (!bool.TryParse(value, out result))
			{
				this.reader.ComplianceTracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidValueFormat);
			}
			return result;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x000283E7 File Offset: 0x000265E7
		internal bool ReadValueAsBoolean(CalendarValueSeparators expectedSeparators)
		{
			return this.ReadValueAsBoolean(new CalendarValueSeparators?(expectedSeparators));
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x000283F8 File Offset: 0x000265F8
		public bool ReadValueAsBoolean()
		{
			return this.ReadValueAsBoolean(null);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00028414 File Offset: 0x00026614
		private float ReadValueAsFloat(CalendarValueSeparators? expectedSeparators)
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
			string s = this.ReadValueAsString(expectedSeparators).Trim();
			this.CheckType(CalendarValueType.Float);
			float result;
			if (!float.TryParse(s, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out result))
			{
				this.reader.ComplianceTracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidValueFormat);
			}
			return result;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00028475 File Offset: 0x00026675
		internal float ReadValueAsFloat(CalendarValueSeparators expectedSeparators)
		{
			return this.ReadValueAsFloat(new CalendarValueSeparators?(expectedSeparators));
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00028484 File Offset: 0x00026684
		public float ReadValueAsFloat()
		{
			return this.ReadValueAsFloat(null);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x000284A0 File Offset: 0x000266A0
		private double ReadValueAsDouble(CalendarValueSeparators? expectedSeparators)
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
			string s = this.ReadValueAsString(expectedSeparators).Trim();
			this.CheckType(CalendarValueType.Float);
			double result;
			if (!double.TryParse(s, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out result))
			{
				this.reader.ComplianceTracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidValueFormat);
			}
			return result;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00028501 File Offset: 0x00026701
		internal double ReadValueAsDouble(CalendarValueSeparators expectedSeparators)
		{
			return this.ReadValueAsDouble(new CalendarValueSeparators?(expectedSeparators));
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00028510 File Offset: 0x00026710
		public double ReadValueAsDouble()
		{
			return this.ReadValueAsDouble(null);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0002852C File Offset: 0x0002672C
		private int ReadValueAsInt32(CalendarValueSeparators? expectedSeparators)
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
			string s = this.ReadValueAsString(expectedSeparators).Trim();
			this.CheckType(CalendarValueType.Integer);
			int result;
			if (!int.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				this.reader.ComplianceTracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidValueFormat);
			}
			return result;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00028589 File Offset: 0x00026789
		internal int ReadValueAsInt32(CalendarValueSeparators expectedSeparators)
		{
			return this.ReadValueAsInt32(new CalendarValueSeparators?(expectedSeparators));
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00028598 File Offset: 0x00026798
		public int ReadValueAsInt32()
		{
			return this.ReadValueAsInt32(null);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x000285B4 File Offset: 0x000267B4
		private CalendarPeriod ReadValueAsCalendarPeriod(CalendarValueSeparators? expectedSeparators)
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
			string s = this.ReadValueAsString(expectedSeparators).Trim();
			this.CheckType(CalendarValueType.Period);
			return new CalendarPeriod(s, this.reader.ComplianceTracker);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000285F7 File Offset: 0x000267F7
		internal CalendarPeriod ReadValueAsCalendarPeriod(CalendarValueSeparators expectedSeparators)
		{
			return this.ReadValueAsCalendarPeriod(new CalendarValueSeparators?(expectedSeparators));
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00028608 File Offset: 0x00026808
		public CalendarPeriod ReadValueAsCalendarPeriod()
		{
			return this.ReadValueAsCalendarPeriod(null);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00028624 File Offset: 0x00026824
		private Recurrence ReadValueAsRecurrence(CalendarValueSeparators? expectedSeparators)
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
			string s = this.ReadValueAsString(expectedSeparators).Trim();
			this.CheckType(CalendarValueType.Recurrence);
			return new Recurrence(s, this.reader.ComplianceTracker);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00028667 File Offset: 0x00026867
		internal Recurrence ReadValueAsRecurrence(CalendarValueSeparators expectedSeparators)
		{
			return this.ReadValueAsRecurrence(new CalendarValueSeparators?(expectedSeparators));
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00028678 File Offset: 0x00026878
		public Recurrence ReadValueAsRecurrence()
		{
			return this.ReadValueAsRecurrence(null);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00028694 File Offset: 0x00026894
		public bool ReadNextValue()
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property | ContentLineNodeType.DocumentEnd);
			return this.reader.ReadNextPropertyValue();
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x000286AE File Offset: 0x000268AE
		public bool ReadNextProperty()
		{
			this.reader.AssertValidState(ContentLineNodeType.ComponentStart | ContentLineNodeType.ComponentEnd | ContentLineNodeType.Parameter | ContentLineNodeType.Property | ContentLineNodeType.BeforeComponentStart | ContentLineNodeType.BeforeComponentEnd | ContentLineNodeType.DocumentEnd);
			return this.reader.ReadNextProperty();
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x000286C8 File Offset: 0x000268C8
		public Stream GetValueReadStream()
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
			this.lastSeparator = CalendarValueSeparators.None;
			return this.reader.GetValueReadStream();
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x000286E9 File Offset: 0x000268E9
		private void CheckType(CalendarValueType type)
		{
			if (this.ValueType != type && this.ValueType != CalendarValueType.Text)
			{
				this.reader.ComplianceTracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidValueFormat);
			}
		}

		// Token: 0x040005DB RID: 1499
		private ContentLineReader reader;

		// Token: 0x040005DC RID: 1500
		private CalendarValueSeparators lastSeparator;
	}
}
