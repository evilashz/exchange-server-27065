using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.ContentTypes.Internal;

namespace Microsoft.Exchange.Data.ContentTypes.vCard
{
	// Token: 0x020000C4 RID: 196
	public class ContactWriter : IDisposable
	{
		// Token: 0x060007C5 RID: 1989 RVA: 0x0002A8EF File Offset: 0x00028AEF
		public ContactWriter(Stream stream) : this(stream, Encoding.UTF8)
		{
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0002A900 File Offset: 0x00028B00
		public ContactWriter(Stream stream, Encoding encoding)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException(CalendarStrings.StreamMustAllowWrite, "stream");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			this.writer = new ContentLineWriter(stream, encoding);
			this.encoding = encoding;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0002A969 File Offset: 0x00028B69
		public void Flush()
		{
			this.AssertValidState(WriteState.Component | WriteState.Property | WriteState.Parameter);
			this.writer.Flush();
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0002A97E File Offset: 0x00028B7E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0002A98D File Offset: 0x00028B8D
		public virtual void Close()
		{
			this.Dispose();
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0002A995 File Offset: 0x00028B95
		public void StartVCard()
		{
			this.EndProperty();
			this.AssertValidState(WriteState.Start);
			this.writer.WriteProperty("BEGIN", "VCARD");
			this.state = WriteState.Component;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0002A9C0 File Offset: 0x00028BC0
		public void EndVCard()
		{
			this.EndProperty();
			this.AssertValidState(WriteState.Component);
			this.writer.WriteProperty("END", "VCARD");
			this.state = WriteState.Start;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0002A9EC File Offset: 0x00028BEC
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
			PropertyId propertyEnum = ContactCommon.GetPropertyEnum(name);
			this.StartProperty(name, propertyEnum);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0002AA2C File Offset: 0x00028C2C
		public void StartProperty(PropertyId propertyId)
		{
			string propertyString = ContactCommon.GetPropertyString(propertyId);
			if (propertyString == null)
			{
				throw new ArgumentException(CalendarStrings.InvalidPropertyId);
			}
			this.StartProperty(propertyString, propertyId);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0002AA58 File Offset: 0x00028C58
		public void StartParameter(string name)
		{
			if (this.validate)
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				if (name.Length == 0)
				{
					throw new ArgumentException();
				}
			}
			this.EndParameter();
			this.AssertValidState(WriteState.Property);
			this.parameter = ContactCommon.GetParameterEnum(name);
			this.writer.StartParameter(name);
			this.firstParameterValue = true;
			this.state = WriteState.Parameter;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0002AABC File Offset: 0x00028CBC
		public void StartParameter(ParameterId parameterId)
		{
			string parameterString = ContactCommon.GetParameterString(parameterId);
			if (parameterString == null)
			{
				throw new ArgumentException(CalendarStrings.InvalidParameterId);
			}
			this.StartParameter(parameterString);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0002AAE8 File Offset: 0x00028CE8
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
				this.valueType = ContactCommon.GetValueTypeEnum(value);
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

		// Token: 0x060007D1 RID: 2001 RVA: 0x0002AB84 File Offset: 0x00028D84
		public void WritePropertyValue(string value, ContactValueSeparators separator)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.PrepareStartPropertyValue((ContentLineParser.Separators)separator);
			if (this.valueType == ContactValueType.Text || this.valueType == ContactValueType.PhoneNumber || this.valueType == ContactValueType.VCard)
			{
				value = ContactWriter.GetEscapedText(value);
			}
			this.writer.WriteToStream(value);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0002ABD7 File Offset: 0x00028DD7
		public void WritePropertyValue(string value)
		{
			this.WritePropertyValue(value, ContactValueSeparators.Comma);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0002ABE4 File Offset: 0x00028DE4
		public void WritePropertyValue(object value, ContactValueSeparators separator)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			switch (this.valueType)
			{
			case ContactValueType.Unknown:
			case ContactValueType.Text:
			case ContactValueType.Uri:
			case ContactValueType.VCard:
			case ContactValueType.PhoneNumber:
			{
				string text = value as string;
				if (text == null)
				{
					throw new ArgumentException(CalendarStrings.InvalidValueTypeForProperty);
				}
				this.WritePropertyValue(text, separator);
				return;
			}
			case ContactValueType.Binary:
			{
				byte[] array = value as byte[];
				if (array == null)
				{
					throw new ArgumentException(CalendarStrings.InvalidValueTypeForProperty);
				}
				this.WritePropertyValue(array);
				return;
			}
			case ContactValueType.Boolean:
				if (!(value is bool))
				{
					throw new ArgumentException(CalendarStrings.InvalidValueTypeForProperty);
				}
				this.WritePropertyValue((bool)value, separator);
				return;
			case ContactValueType.Date:
				if (!(value is DateTime))
				{
					throw new ArgumentException(CalendarStrings.InvalidValueTypeForProperty);
				}
				this.WritePropertyValue((DateTime)value, ContactValueType.Date, separator);
				return;
			case ContactValueType.DateTime:
				if (!(value is DateTime))
				{
					throw new ArgumentException(CalendarStrings.InvalidValueTypeForProperty);
				}
				this.WritePropertyValue((DateTime)value, separator);
				return;
			case ContactValueType.Float:
				if (!(value is double))
				{
					throw new ArgumentException(CalendarStrings.InvalidValueTypeForProperty);
				}
				this.WritePropertyValue((double)value, separator);
				return;
			case ContactValueType.Integer:
				if (!(value is int))
				{
					throw new ArgumentException(CalendarStrings.InvalidValueTypeForProperty);
				}
				this.WritePropertyValue((int)value, separator);
				return;
			case ContactValueType.Time:
				if (!(value is DateTime))
				{
					throw new ArgumentException(CalendarStrings.InvalidValueTypeForProperty);
				}
				this.WritePropertyValue((DateTime)value, ContactValueType.Time, separator);
				return;
			case ContactValueType.UtcOffset:
				if (!(value is TimeSpan))
				{
					throw new ArgumentException(CalendarStrings.InvalidValueTypeForProperty);
				}
				this.WritePropertyValue((TimeSpan)value, separator);
				return;
			default:
				throw new InvalidDataException(CalendarStrings.InvalidValueTypeForProperty);
			}
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0002AD73 File Offset: 0x00028F73
		public void WritePropertyValue(object value)
		{
			this.WritePropertyValue(value, ContactValueSeparators.Comma);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0002AD7D File Offset: 0x00028F7D
		public void WritePropertyValue(int value, ContactValueSeparators separator)
		{
			this.WritePropertyValue(value.ToString(NumberFormatInfo.InvariantInfo), separator);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0002AD92 File Offset: 0x00028F92
		public void WritePropertyValue(int value)
		{
			this.WritePropertyValue(value.ToString(NumberFormatInfo.InvariantInfo));
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0002ADA6 File Offset: 0x00028FA6
		public void WritePropertyValue(double value, ContactValueSeparators separator)
		{
			this.WritePropertyValue(value.ToString(NumberFormatInfo.InvariantInfo), separator);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0002ADBB File Offset: 0x00028FBB
		public void WritePropertyValue(double value)
		{
			this.WritePropertyValue(value.ToString(NumberFormatInfo.InvariantInfo));
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0002ADCF File Offset: 0x00028FCF
		public void WritePropertyValue(bool value, ContactValueSeparators separator)
		{
			this.WritePropertyValue(value ? "TRUE" : "FALSE", separator);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0002ADE7 File Offset: 0x00028FE7
		public void WritePropertyValue(bool value)
		{
			this.WritePropertyValue(value ? "TRUE" : "FALSE");
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0002ADFE File Offset: 0x00028FFE
		public void WritePropertyValue(DateTime value, ContactValueSeparators separator)
		{
			this.WritePropertyValue(value, this.valueType, separator);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0002AE0E File Offset: 0x0002900E
		public void WritePropertyValue(DateTime value)
		{
			this.WritePropertyValue(value, this.valueType, ContactValueSeparators.Comma);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0002AE1E File Offset: 0x0002901E
		public void WritePropertyValue(byte[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.PrepareStartPropertyValue(ContentLineParser.Separators.None);
			this.writer.WriteToStream(value);
			this.EndProperty();
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0002AE48 File Offset: 0x00029048
		public void WritePropertyValue(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException();
			}
			this.PrepareStartPropertyValue(ContentLineParser.Separators.None);
			if (this.valueType == ContactValueType.Binary)
			{
				byte[] array = new byte[4096];
				for (;;)
				{
					int num = stream.Read(array, 0, array.Length);
					if (num == 0)
					{
						break;
					}
					this.writer.WriteToStream(array, 0, num);
				}
			}
			else
			{
				StreamReader streamReader = new StreamReader(stream, this.encoding);
				char[] array2 = new char[256];
				char[] array3 = new char[array2.Length * 2];
				bool flag = false;
				for (;;)
				{
					int num2 = streamReader.ReadBlock(array2, 0, array2.Length);
					if (num2 == 0)
					{
						break;
					}
					int size = 0;
					int i = 0;
					while (i < num2)
					{
						char c = array2[i];
						if (c <= '\r')
						{
							if (c != '\n')
							{
								if (c != '\r')
								{
									goto IL_12A;
								}
								array3[size++] = '\\';
								array3[size++] = 'n';
								flag = true;
							}
							else
							{
								if (!flag)
								{
									array3[size++] = '\\';
									array3[size++] = 'n';
								}
								flag = false;
							}
						}
						else
						{
							if (c != ',' && c != ';' && c != '\\')
							{
								goto IL_12A;
							}
							array3[size++] = '\\';
							array3[size++] = array2[i];
							flag = false;
						}
						IL_13B:
						i++;
						continue;
						IL_12A:
						array3[size++] = array2[i];
						flag = false;
						goto IL_13B;
					}
					this.writer.WriteChars(array3, 0, size);
				}
			}
			this.EndProperty();
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0002AFBC File Offset: 0x000291BC
		public void WriteContact(ContactReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.ComplianceMode == ContactComplianceMode.Loose)
			{
				this.SetLooseMode();
			}
			while (reader.ReadNext())
			{
				this.StartVCard();
				ContactPropertyReader propertyReader = reader.PropertyReader;
				while (propertyReader.ReadNextProperty())
				{
					this.WriteProperty(propertyReader);
				}
				this.EndVCard();
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0002B014 File Offset: 0x00029214
		public void WriteProperty(ContactPropertyReader reader)
		{
			this.StartProperty(reader.Name);
			ContactParameterReader parameterReader = reader.ParameterReader;
			while (parameterReader.ReadNextParameter())
			{
				this.WriteParameter(parameterReader);
			}
			ContactValueSeparators separator = ContactValueSeparators.None;
			ContactValueSeparators expectedSeparators = ContactValueSeparators.Comma | ContactValueSeparators.Semicolon;
			ContactValueType contactValueType = reader.ValueType;
			switch (contactValueType)
			{
			case ContactValueType.Binary:
				expectedSeparators = ContactValueSeparators.None;
				break;
			case ContactValueType.Boolean:
				break;
			case ContactValueType.Date:
			case ContactValueType.DateTime:
				goto IL_55;
			default:
				if (contactValueType == ContactValueType.Time)
				{
					goto IL_55;
				}
				break;
			}
			IL_70:
			while (reader.ReadNextValue())
			{
				this.WritePropertyValue(reader.ReadValue(expectedSeparators), separator);
				separator = reader.LastValueSeparator;
			}
			this.EndProperty();
			return;
			IL_55:
			expectedSeparators = ContactValueSeparators.Semicolon;
			goto IL_70;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0002B0A0 File Offset: 0x000292A0
		public void WriteProperty(string name, string value)
		{
			this.StartProperty(name);
			this.WritePropertyValue(value);
			this.EndProperty();
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0002B0B6 File Offset: 0x000292B6
		public void WriteProperty(PropertyId propertyId, string value)
		{
			this.StartProperty(propertyId);
			this.WritePropertyValue(value);
			this.EndProperty();
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0002B0CC File Offset: 0x000292CC
		public void WriteParameter(ContactParameterReader reader)
		{
			this.StartParameter(reader.Name);
			if (reader.Name != null)
			{
				while (reader.ReadNextValue())
				{
					this.WriteParameterValue(reader.ReadValue());
				}
			}
			else
			{
				this.WriteParameterValue(reader.ReadValue());
			}
			this.EndParameter();
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0002B11C File Offset: 0x0002931C
		public void WriteParameter(string name, string value)
		{
			this.StartParameter(name);
			this.WriteParameterValue(value);
			this.EndParameter();
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0002B132 File Offset: 0x00029332
		public void WriteValueTypeParameter(ContactValueType type)
		{
			this.StartParameter(ParameterId.ValueType);
			this.WriteParameterValue(ContactCommon.GetValueTypeString(type));
			this.EndParameter();
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0002B14D File Offset: 0x0002934D
		public void WriteParameter(ParameterId parameterId, string value)
		{
			this.StartParameter(parameterId);
			this.WriteParameterValue(value);
			this.EndParameter();
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0002B163 File Offset: 0x00029363
		internal void SetLooseMode()
		{
			this.validate = false;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0002B16C File Offset: 0x0002936C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.state != WriteState.Closed)
			{
				this.writer.Dispose();
			}
			this.state = WriteState.Closed;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0002B190 File Offset: 0x00029390
		private static string GetEscapedText(string data)
		{
			int num = data.IndexOfAny(ContactWriter.PropertyValueSpecials);
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
				int num2 = data.IndexOfAny(ContactWriter.PropertyValueSpecials, num);
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

		// Token: 0x060007EA RID: 2026 RVA: 0x0002B264 File Offset: 0x00029464
		private void StartProperty(string name, PropertyId p)
		{
			this.EndProperty();
			this.AssertValidState(WriteState.Component);
			this.propertyName = name.ToUpper();
			this.valueType = ContactCommon.GetDefaultValueType(p);
			this.writer.StartProperty(this.propertyName);
			this.firstPropertyValue = true;
			this.state = WriteState.Property;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0002B2B8 File Offset: 0x000294B8
		private void WritePropertyValue(DateTime value, ContactValueType valueType, ContactValueSeparators separator)
		{
			string value2;
			if (ContactValueType.DateTime == valueType || ContactValueType.Text == valueType)
			{
				value2 = ContactCommon.FormatDateTime(value);
			}
			else if (ContactValueType.Date == valueType)
			{
				value2 = ContactCommon.FormatDate(value);
			}
			else
			{
				if (ContactValueType.Time != valueType)
				{
					throw new ArgumentOutOfRangeException("valueType");
				}
				value2 = ContactCommon.FormatTime(value);
			}
			this.WritePropertyValue(value2, separator);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0002B304 File Offset: 0x00029504
		private void WritePropertyValue(TimeSpan value, ContactValueSeparators separator)
		{
			if (ContactValueType.UtcOffset != this.valueType)
			{
				throw new ArgumentOutOfRangeException("valueType");
			}
			if (value.Days > 0 && this.validate)
			{
				throw new ArgumentException(CalendarStrings.UtcOffsetTimespanCannotContainDays, "value");
			}
			string value2 = ContactCommon.FormatUtcOffset(value);
			this.WritePropertyValue(value2, separator);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0002B359 File Offset: 0x00029559
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
				this.state = WriteState.Component;
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0002B395 File Offset: 0x00029595
		private void EndParameter()
		{
			if (this.state == WriteState.Parameter)
			{
				this.writer.EndParameter();
				this.state = WriteState.Property;
			}
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0002B3B4 File Offset: 0x000295B4
		private bool IsQuotingRequired(string value)
		{
			int num = value.IndexOfAny(ContactWriter.ParameterValueSpecials);
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

		// Token: 0x060007F0 RID: 2032 RVA: 0x0002B3F3 File Offset: 0x000295F3
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

		// Token: 0x060007F1 RID: 2033 RVA: 0x0002B429 File Offset: 0x00029629
		private void AssertValidState(WriteState state)
		{
			if (this.state == WriteState.Closed)
			{
				throw new ObjectDisposedException("ContactWriter");
			}
			if ((state & this.state) == (WriteState)0)
			{
				throw new InvalidOperationException(CalendarStrings.InvalidStateForOperation);
			}
		}

		// Token: 0x04000679 RID: 1657
		private const char CR = '\r';

		// Token: 0x0400067A RID: 1658
		private const char LF = '\n';

		// Token: 0x0400067B RID: 1659
		private const string ComponentStartTag = "BEGIN";

		// Token: 0x0400067C RID: 1660
		private const string ComponentEndTag = "END";

		// Token: 0x0400067D RID: 1661
		private static readonly char[] ParameterValueSpecials = new char[]
		{
			',',
			':',
			';',
			'"'
		};

		// Token: 0x0400067E RID: 1662
		private static readonly char[] PropertyValueSpecials = new char[]
		{
			',',
			';',
			'\\',
			'\r',
			'\n'
		};

		// Token: 0x0400067F RID: 1663
		private ParameterId parameter;

		// Token: 0x04000680 RID: 1664
		private ContactValueType valueType;

		// Token: 0x04000681 RID: 1665
		private string propertyName;

		// Token: 0x04000682 RID: 1666
		private WriteState state = WriteState.Start;

		// Token: 0x04000683 RID: 1667
		private ContentLineWriter writer;

		// Token: 0x04000684 RID: 1668
		private bool firstPropertyValue;

		// Token: 0x04000685 RID: 1669
		private bool firstParameterValue;

		// Token: 0x04000686 RID: 1670
		private bool validate = true;

		// Token: 0x04000687 RID: 1671
		private Encoding encoding;
	}
}
