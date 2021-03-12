using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.ContentTypes.Internal;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.Data.ContentTypes.iCalendar
{
	// Token: 0x020000B5 RID: 181
	public class CalendarWriter : IDisposable
	{
		// Token: 0x0600073E RID: 1854 RVA: 0x00028906 File Offset: 0x00026B06
		public CalendarWriter(Stream stream) : this(stream, "utf-8")
		{
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00028914 File Offset: 0x00026B14
		public CalendarWriter(Stream stream, string encodingName)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException(CalendarStrings.StreamMustAllowWrite, "stream");
			}
			if (encodingName == null)
			{
				throw new ArgumentNullException("encodingName");
			}
			this.writer = new ContentLineWriter(stream, Charset.GetEncoding(encodingName));
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00028994 File Offset: 0x00026B94
		public void Flush()
		{
			this.AssertValidState(WriteState.Component | WriteState.Property | WriteState.Parameter);
			this.writer.Flush();
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x000289A9 File Offset: 0x00026BA9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x000289B8 File Offset: 0x00026BB8
		public virtual void Close()
		{
			this.Dispose();
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x000289C0 File Offset: 0x00026BC0
		public void StartComponent(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.validate && name.Length == 0)
			{
				throw new ArgumentException();
			}
			this.EndProperty();
			this.AssertValidState(WriteState.Start | WriteState.Component);
			this.Save();
			this.componentName = name.ToUpper();
			this.componentId = CalendarCommon.GetComponentEnum(name);
			this.writer.WriteProperty("BEGIN", this.componentName);
			this.state = WriteState.Component;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00028A39 File Offset: 0x00026C39
		public void StartComponent(ComponentId componentId)
		{
			if (componentId == ComponentId.Unknown || componentId == ComponentId.None)
			{
				throw new ArgumentException(CalendarStrings.InvalidComponentId);
			}
			this.StartComponent(CalendarCommon.GetComponentString(componentId));
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00028A59 File Offset: 0x00026C59
		public void EndComponent()
		{
			this.EndProperty();
			this.AssertValidState(WriteState.Component);
			this.writer.WriteProperty("END", this.componentName);
			this.Load();
			this.state = WriteState.Component;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00028A8C File Offset: 0x00026C8C
		public void StartProperty(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.validate && name.Length == 0)
			{
				throw new ArgumentException();
			}
			this.EndProperty();
			this.AssertValidState(WriteState.Component);
			PropertyId propertyEnum = CalendarCommon.GetPropertyEnum(name);
			this.propertyName = name.ToUpper();
			this.property = propertyEnum;
			this.Save();
			this.valueType = CalendarCommon.GetDefaultValueType(propertyEnum);
			this.writer.StartProperty(this.propertyName);
			this.firstPropertyValue = true;
			this.state = WriteState.Property;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00028B18 File Offset: 0x00026D18
		public void StartProperty(PropertyId propertyId)
		{
			string propertyString = CalendarCommon.GetPropertyString(propertyId);
			if (propertyString == null)
			{
				throw new ArgumentException(CalendarStrings.InvalidPropertyId);
			}
			this.StartProperty(propertyString);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00028B44 File Offset: 0x00026D44
		public void StartParameter(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.validate && name.Length == 0)
			{
				throw new ArgumentException();
			}
			this.EndParameter();
			this.AssertValidState(WriteState.Property);
			this.parameter = CalendarCommon.GetParameterEnum(name);
			this.writer.StartParameter(name);
			this.firstParameterValue = true;
			this.state = WriteState.Parameter;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00028BA8 File Offset: 0x00026DA8
		public void StartParameter(ParameterId parameterId)
		{
			string parameterString = CalendarCommon.GetParameterString(parameterId);
			if (parameterString == null)
			{
				throw new ArgumentException(CalendarStrings.InvalidParameterId);
			}
			this.StartParameter(parameterString);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00028BD4 File Offset: 0x00026DD4
		public void WriteParameterValue(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.AssertValidState(WriteState.Parameter);
			if (this.firstParameterValue)
			{
				this.writer.WriteStartValue();
				this.firstParameterValue = false;
			}
			else
			{
				this.writer.WriteNextValue(ContentLineParser.Separators.Comma);
			}
			if (this.parameter == ParameterId.ValueType && value.Length > 0)
			{
				this.valueType = CalendarCommon.GetValueTypeEnum(value);
			}
			bool flag = this.IsQuotingRequired(value);
			if (flag)
			{
				this.writer.WriteToStream(34);
			}
			this.writer.WriteToStream(value);
			if (flag)
			{
				this.writer.WriteToStream(34);
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00028C74 File Offset: 0x00026E74
		public void WritePropertyValue(string value)
		{
			this.WritePropertyValue(value, CalendarValueSeparators.Comma);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00028C7E File Offset: 0x00026E7E
		public void WritePropertyValue(object value)
		{
			this.WritePropertyValue(value, CalendarValueSeparators.Comma);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00028C88 File Offset: 0x00026E88
		public void WritePropertyValue(CalendarPeriod value)
		{
			this.WritePropertyValue(value.ToString());
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00028C9D File Offset: 0x00026E9D
		public void WritePropertyValue(Recurrence value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.WritePropertyValue(value.ToString());
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00028CB9 File Offset: 0x00026EB9
		public void WritePropertyValue(int value)
		{
			this.WritePropertyValue(value.ToString(NumberFormatInfo.InvariantInfo));
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00028CCD File Offset: 0x00026ECD
		public void WritePropertyValue(float value)
		{
			this.WritePropertyValue(value.ToString(NumberFormatInfo.InvariantInfo));
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00028CE1 File Offset: 0x00026EE1
		public void WritePropertyValue(bool value)
		{
			this.WritePropertyValue(value ? "TRUE" : "FALSE");
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00028CF8 File Offset: 0x00026EF8
		public void WritePropertyValue(DateTime value)
		{
			this.WritePropertyValue(value, this.valueType);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00028D07 File Offset: 0x00026F07
		public void WritePropertyValue(DateTime value, CalendarValueType valueType)
		{
			this.WritePropertyValue(value, valueType, CalendarValueSeparators.Comma);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00028D12 File Offset: 0x00026F12
		public void WritePropertyValue(CalendarTime value)
		{
			this.WritePropertyValue(value.ToString());
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00028D27 File Offset: 0x00026F27
		public void WritePropertyValue(TimeSpan value)
		{
			this.WritePropertyValue(value, this.valueType, CalendarValueSeparators.Comma);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00028D38 File Offset: 0x00026F38
		public void WriteComponent(CalendarReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.Depth > 100)
			{
				return;
			}
			this.StartComponent(reader.ComponentName);
			CalendarPropertyReader propertyReader = reader.PropertyReader;
			while (propertyReader.ReadNextProperty())
			{
				this.WriteProperty(propertyReader);
			}
			if (reader.ReadFirstChildComponent())
			{
				this.WriteComponent(reader);
				while (reader.ReadNextSiblingComponent())
				{
					this.WriteComponent(reader);
				}
			}
			this.EndComponent();
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00028DAC File Offset: 0x00026FAC
		public void WriteProperty(CalendarPropertyReader reader)
		{
			CalendarParameterReader parameterReader = reader.ParameterReader;
			this.StartProperty(reader.Name);
			while (parameterReader.ReadNextParameter())
			{
				this.WriteParameter(parameterReader);
			}
			CalendarValueSeparators separator = CalendarValueSeparators.None;
			while (reader.ReadNextValue())
			{
				this.WritePropertyValue(reader.ReadValue(CalendarValueSeparators.Comma | CalendarValueSeparators.Semicolon), separator);
				separator = reader.LastValueSeparator;
			}
			this.EndProperty();
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00028E0A File Offset: 0x0002700A
		public void WriteProperty(string name, string value)
		{
			this.StartProperty(name);
			this.WritePropertyValue(value);
			this.EndProperty();
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00028E20 File Offset: 0x00027020
		public void WriteProperty(PropertyId propertyId, string value)
		{
			this.StartProperty(propertyId);
			this.WritePropertyValue(value);
			this.EndProperty();
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00028E36 File Offset: 0x00027036
		public void WriteParameter(CalendarParameterReader reader)
		{
			this.StartParameter(reader.Name);
			while (reader.ReadNextValue())
			{
				this.WriteParameterValue(reader.ReadValue());
			}
			if (this.firstParameterValue)
			{
				this.WriteParameterValue(string.Empty);
			}
			this.EndParameter();
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00028E76 File Offset: 0x00027076
		public void WriteParameter(string name, string value)
		{
			this.StartParameter(name);
			this.WriteParameterValue(value);
			this.EndParameter();
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00028E8C File Offset: 0x0002708C
		public void WriteParameter(ParameterId parameterId, string value)
		{
			this.StartParameter(parameterId);
			this.WriteParameterValue(value);
			this.EndParameter();
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00028EA2 File Offset: 0x000270A2
		internal void WritePropertyValue(string value, CalendarValueSeparators separator)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.PrepareStartPropertyValue((ContentLineParser.Separators)separator);
			if (CalendarValueType.Text == this.valueType)
			{
				value = CalendarWriter.GetEscapedText(value);
			}
			this.writer.WriteToStream(value);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00028EDC File Offset: 0x000270DC
		internal void WritePropertyValue(object value, CalendarValueSeparators separator)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			CalendarValueType calendarValueType = this.valueType;
			if (calendarValueType <= CalendarValueType.Float)
			{
				if (calendarValueType <= CalendarValueType.Date)
				{
					switch (calendarValueType)
					{
					case CalendarValueType.Unknown:
						break;
					case CalendarValueType.Binary:
					{
						byte[] array = value as byte[];
						if (array == null)
						{
							throw new ArgumentException(CalendarStrings.InvalidValueTypeForProperty);
						}
						this.PrepareStartPropertyValue(ContentLineParser.Separators.None);
						this.writer.WriteToStream(array);
						this.EndProperty();
						return;
					}
					case CalendarValueType.Unknown | CalendarValueType.Binary:
						goto IL_276;
					case CalendarValueType.Boolean:
						if (!(value is bool))
						{
							throw new ArgumentException(CalendarStrings.InvalidValueTypeForProperty);
						}
						this.WritePropertyValue((bool)value, separator);
						return;
					default:
						if (calendarValueType != CalendarValueType.CalAddress)
						{
							if (calendarValueType != CalendarValueType.Date)
							{
								goto IL_276;
							}
							if (!(value is DateTime))
							{
								throw new ArgumentException(CalendarStrings.InvalidValueTypeForProperty);
							}
							this.WritePropertyValue((DateTime)value, CalendarValueType.Date, separator);
							return;
						}
						break;
					}
				}
				else if (calendarValueType != CalendarValueType.DateTime)
				{
					if (calendarValueType != CalendarValueType.Duration)
					{
						if (calendarValueType != CalendarValueType.Float)
						{
							goto IL_276;
						}
						if (!(value is float))
						{
							throw new ArgumentException(CalendarStrings.InvalidValueTypeForProperty);
						}
						this.WritePropertyValue((float)value, separator);
						return;
					}
					else
					{
						if (!(value is TimeSpan))
						{
							throw new ArgumentException(CalendarStrings.InvalidValueTypeForProperty);
						}
						this.WritePropertyValue((TimeSpan)value, CalendarValueType.Duration, separator);
						return;
					}
				}
				else
				{
					if (!(value is DateTime))
					{
						throw new ArgumentException(CalendarStrings.InvalidValueTypeForProperty);
					}
					this.WritePropertyValue((DateTime)value, separator);
					return;
				}
			}
			else if (calendarValueType <= CalendarValueType.Recurrence)
			{
				if (calendarValueType != CalendarValueType.Integer)
				{
					if (calendarValueType != CalendarValueType.Period)
					{
						if (calendarValueType != CalendarValueType.Recurrence)
						{
							goto IL_276;
						}
						Recurrence recurrence = value as Recurrence;
						if (recurrence == null)
						{
							throw new ArgumentException(CalendarStrings.InvalidValueTypeForProperty);
						}
						this.WritePropertyValue(recurrence);
						return;
					}
					else
					{
						if (!(value is CalendarPeriod))
						{
							throw new ArgumentException(CalendarStrings.InvalidValueTypeForProperty);
						}
						this.WritePropertyValue((CalendarPeriod)value, separator);
						return;
					}
				}
				else
				{
					if (!(value is int))
					{
						throw new ArgumentException(CalendarStrings.InvalidValueTypeForProperty);
					}
					this.WritePropertyValue((int)value, separator);
					return;
				}
			}
			else if (calendarValueType <= CalendarValueType.Time)
			{
				if (calendarValueType != CalendarValueType.Text)
				{
					if (calendarValueType != CalendarValueType.Time)
					{
						goto IL_276;
					}
					if (!(value is CalendarTime))
					{
						throw new ArgumentException(CalendarStrings.InvalidValueTypeForProperty);
					}
					this.WritePropertyValue((CalendarTime)value, separator);
					return;
				}
			}
			else if (calendarValueType != CalendarValueType.Uri)
			{
				if (calendarValueType != CalendarValueType.UtcOffset)
				{
					goto IL_276;
				}
				if (!(value is TimeSpan))
				{
					throw new ArgumentException(CalendarStrings.InvalidValueTypeForProperty);
				}
				this.WritePropertyValue((TimeSpan)value, CalendarValueType.UtcOffset, separator);
				return;
			}
			string text = value as string;
			if (text == null)
			{
				throw new ArgumentException(CalendarStrings.InvalidValueTypeForProperty);
			}
			this.WritePropertyValue(text, separator);
			return;
			IL_276:
			throw new InvalidDataException(CalendarStrings.InvalidValueTypeForProperty);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00029169 File Offset: 0x00027369
		internal void WritePropertyValue(CalendarPeriod value, CalendarValueSeparators separator)
		{
			this.WritePropertyValue(value.ToString(), separator);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0002917F File Offset: 0x0002737F
		internal void WritePropertyValue(int value, CalendarValueSeparators separator)
		{
			this.WritePropertyValue(value.ToString(NumberFormatInfo.InvariantInfo), separator);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00029194 File Offset: 0x00027394
		internal void WritePropertyValue(float value, CalendarValueSeparators separator)
		{
			this.WritePropertyValue(value.ToString(NumberFormatInfo.InvariantInfo), separator);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x000291A9 File Offset: 0x000273A9
		internal void WritePropertyValue(bool value, CalendarValueSeparators separator)
		{
			this.WritePropertyValue(value ? "TRUE" : "FALSE", separator);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000291C1 File Offset: 0x000273C1
		internal void WritePropertyValue(DateTime value, CalendarValueSeparators separator)
		{
			this.WritePropertyValue(value, this.valueType, separator);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x000291D1 File Offset: 0x000273D1
		internal void WritePropertyValue(CalendarTime value, CalendarValueSeparators separator)
		{
			this.WritePropertyValue(value.ToString(), separator);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x000291E7 File Offset: 0x000273E7
		internal void WritePropertyValue(TimeSpan value, CalendarValueSeparators separator)
		{
			this.WritePropertyValue(value, this.valueType, separator);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x000291F7 File Offset: 0x000273F7
		internal void SetLooseMode()
		{
			this.validate = false;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00029200 File Offset: 0x00027400
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.state != WriteState.Closed)
			{
				this.writer.Dispose();
			}
			this.state = WriteState.Closed;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00029224 File Offset: 0x00027424
		private static string GetEscapedText(string data)
		{
			int num = data.IndexOfAny(CalendarWriter.PropertyValueSpecials);
			if (-1 == num)
			{
				return data;
			}
			int length = data.Length;
			StringBuilder stringBuilder = new StringBuilder(data, 0, num, length);
			for (;;)
			{
				stringBuilder.Append('\\');
				bool flag = '\r' == data[num];
				if (flag || '\n' == data[num])
				{
					stringBuilder.Append('n');
					num++;
					if (flag && num < data.Length && '\n' == data[num])
					{
						num++;
					}
				}
				else
				{
					stringBuilder.Append(data[num++]);
				}
				if (num == data.Length)
				{
					goto IL_C1;
				}
				int num2 = data.IndexOfAny(CalendarWriter.PropertyValueSpecials, num);
				if (-1 == num2)
				{
					break;
				}
				stringBuilder.Append(data, num, num2 - num);
				num = num2;
			}
			stringBuilder.Append(data, num, length - num);
			IL_C1:
			return stringBuilder.ToString();
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x000292F8 File Offset: 0x000274F8
		private void WritePropertyValue(DateTime value, CalendarValueType valueType, CalendarValueSeparators separator)
		{
			string value2;
			if (CalendarValueType.DateTime == valueType || CalendarValueType.Text == valueType)
			{
				value2 = CalendarCommon.FormatDateTime(value);
			}
			else
			{
				if (CalendarValueType.Date != valueType)
				{
					throw new ArgumentOutOfRangeException("valueType");
				}
				value2 = CalendarCommon.FormatDate(value);
			}
			this.WritePropertyValue(value2, separator);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0002933C File Offset: 0x0002753C
		private void WritePropertyValue(TimeSpan value, CalendarValueType valueType, CalendarValueSeparators separator)
		{
			string value2;
			if (CalendarValueType.Duration == valueType)
			{
				value2 = CalendarCommon.FormatDuration(value);
			}
			else
			{
				if (CalendarValueType.UtcOffset != valueType)
				{
					throw new ArgumentOutOfRangeException("valueType");
				}
				if (value.Days > 0 && this.validate)
				{
					throw new ArgumentException(CalendarStrings.UtcOffsetTimespanCannotContainDays, "value");
				}
				value2 = CalendarCommon.FormatUtcOffset(value);
			}
			this.WritePropertyValue(value2, separator);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0002939D File Offset: 0x0002759D
		private void EndProperty()
		{
			this.EndParameter();
			if (this.state == WriteState.Property)
			{
				if (this.writer.State != ContentLineWriteState.PropertyValue)
				{
					this.writer.WriteStartValue();
				}
				this.writer.EndProperty();
				this.Load();
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000293D8 File Offset: 0x000275D8
		private void EndParameter()
		{
			if (this.state == WriteState.Parameter)
			{
				this.writer.EndParameter();
				this.state = WriteState.Property;
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000293F8 File Offset: 0x000275F8
		private bool IsQuotingRequired(string value)
		{
			int num = value.IndexOfAny(CalendarWriter.ParameterValueSpecials);
			if (-1 == num)
			{
				return false;
			}
			if (-1 != value.IndexOf('"', num) && this.validate)
			{
				throw new ArgumentException(CalendarStrings.ParameterValuesCannotContainDoubleQuote);
			}
			return true;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00029437 File Offset: 0x00027637
		private void PrepareStartPropertyValue(ContentLineParser.Separators separator)
		{
			this.EndParameter();
			this.AssertValidState(WriteState.Property);
			if (this.firstPropertyValue)
			{
				this.writer.WriteStartValue();
				this.firstPropertyValue = false;
				return;
			}
			this.writer.WriteNextValue(separator);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0002946D File Offset: 0x0002766D
		private void AssertValidState(WriteState state)
		{
			if (this.state == WriteState.Closed)
			{
				throw new ObjectDisposedException("CalendarWriter");
			}
			if ((state & this.state) == (WriteState)0)
			{
				throw new InvalidOperationException(CalendarStrings.InvalidStateForOperation);
			}
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0002949C File Offset: 0x0002769C
		private void Save()
		{
			CalendarWriter.WriterState item;
			item.ComponentId = this.componentId;
			item.Property = this.property;
			item.PropertyName = this.propertyName;
			item.ComponentName = this.componentName;
			item.ValueType = this.valueType;
			item.State = this.state;
			this.stateStack.Push(item);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00029504 File Offset: 0x00027704
		private void Load()
		{
			CalendarWriter.WriterState writerState = this.stateStack.Pop();
			this.componentId = writerState.ComponentId;
			this.property = writerState.Property;
			this.propertyName = writerState.PropertyName;
			this.componentName = writerState.ComponentName;
			this.valueType = writerState.ValueType;
			this.state = writerState.State;
		}

		// Token: 0x040005E8 RID: 1512
		private const char CR = '\r';

		// Token: 0x040005E9 RID: 1513
		private const char LF = '\n';

		// Token: 0x040005EA RID: 1514
		private const string ComponentStartTag = "BEGIN";

		// Token: 0x040005EB RID: 1515
		private const string ComponentEndTag = "END";

		// Token: 0x040005EC RID: 1516
		private static readonly char[] ParameterValueSpecials = new char[]
		{
			',',
			':',
			';',
			'"'
		};

		// Token: 0x040005ED RID: 1517
		private static readonly char[] PropertyValueSpecials = new char[]
		{
			',',
			';',
			'\\',
			'\r',
			'\n'
		};

		// Token: 0x040005EE RID: 1518
		private ComponentId componentId;

		// Token: 0x040005EF RID: 1519
		private PropertyId property;

		// Token: 0x040005F0 RID: 1520
		private ParameterId parameter = ParameterId.Unknown;

		// Token: 0x040005F1 RID: 1521
		private CalendarValueType valueType = CalendarValueType.Unknown;

		// Token: 0x040005F2 RID: 1522
		private string componentName;

		// Token: 0x040005F3 RID: 1523
		private string propertyName;

		// Token: 0x040005F4 RID: 1524
		private WriteState state = WriteState.Start;

		// Token: 0x040005F5 RID: 1525
		private ContentLineWriter writer;

		// Token: 0x040005F6 RID: 1526
		private Stack<CalendarWriter.WriterState> stateStack = new Stack<CalendarWriter.WriterState>();

		// Token: 0x040005F7 RID: 1527
		private bool firstPropertyValue;

		// Token: 0x040005F8 RID: 1528
		private bool firstParameterValue;

		// Token: 0x040005F9 RID: 1529
		private bool validate = true;

		// Token: 0x020000B6 RID: 182
		private struct WriterState
		{
			// Token: 0x040005FA RID: 1530
			public ComponentId ComponentId;

			// Token: 0x040005FB RID: 1531
			public PropertyId Property;

			// Token: 0x040005FC RID: 1532
			public CalendarValueType ValueType;

			// Token: 0x040005FD RID: 1533
			public string ComponentName;

			// Token: 0x040005FE RID: 1534
			public string PropertyName;

			// Token: 0x040005FF RID: 1535
			public WriteState State;
		}
	}
}
