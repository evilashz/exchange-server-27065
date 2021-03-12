using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.ContentTypes.Internal;
using Microsoft.Exchange.Data.Mime.Encoders;

namespace Microsoft.Exchange.Data.ContentTypes.vCard
{
	// Token: 0x020000C1 RID: 193
	public struct ContactPropertyReader
	{
		// Token: 0x0600079E RID: 1950 RVA: 0x0002A287 File Offset: 0x00028487
		internal ContactPropertyReader(ContentLineReader reader)
		{
			this.reader = reader;
			this.lastSeparator = ContactValueSeparators.None;
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x0002A297 File Offset: 0x00028497
		public ContactValueSeparators LastValueSeparator
		{
			get
			{
				return this.lastSeparator;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x0002A29F File Offset: 0x0002849F
		public PropertyId PropertyId
		{
			get
			{
				this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
				return ContactCommon.GetPropertyEnum(this.reader.PropertyName);
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0002A2C0 File Offset: 0x000284C0
		public ContactValueType ValueType
		{
			get
			{
				this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
				ContactValueTypeContainer contactValueTypeContainer = this.reader.ValueType as ContactValueTypeContainer;
				return contactValueTypeContainer.ValueType;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x0002A2F1 File Offset: 0x000284F1
		public string Name
		{
			get
			{
				this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
				return this.reader.PropertyName;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0002A30B File Offset: 0x0002850B
		public ContactParameterReader ParameterReader
		{
			get
			{
				this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
				return new ContactParameterReader(this.reader);
			}
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0002A325 File Offset: 0x00028525
		public object ReadValue(ContactValueSeparators expectedSeparators)
		{
			return this.ReadValue(new ContactValueSeparators?(expectedSeparators));
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0002A334 File Offset: 0x00028534
		private object ReadValue(ContactValueSeparators? expectedSeparators)
		{
			while (this.reader.ReadNextParameter())
			{
			}
			switch (this.ValueType)
			{
			case ContactValueType.Binary:
				return this.ReadValueAsBytes();
			case ContactValueType.Boolean:
				return this.ReadValueAsBoolean(expectedSeparators);
			case ContactValueType.Date:
			case ContactValueType.DateTime:
			case ContactValueType.Time:
				return this.ReadValueAsDateTime(this.ValueType, expectedSeparators);
			case ContactValueType.Float:
				return this.ReadValueAsDouble(expectedSeparators);
			case ContactValueType.Integer:
				return this.ReadValueAsInt32(expectedSeparators);
			case ContactValueType.UtcOffset:
				return this.ReadValueAsTimeSpan(expectedSeparators);
			}
			return this.ReadValueAsString(expectedSeparators);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0002A3E8 File Offset: 0x000285E8
		public object ReadValue()
		{
			return this.ReadValue(null);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0002A404 File Offset: 0x00028604
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

		// Token: 0x060007A8 RID: 1960 RVA: 0x0002A4A0 File Offset: 0x000286A0
		private string ReadValueAsString(ContactValueSeparators? expectedSeparators)
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
			this.lastSeparator = (ContactValueSeparators)separators;
			return result;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0002A4F7 File Offset: 0x000286F7
		public string ReadValueAsString(ContactValueSeparators expectedSeparators)
		{
			return this.ReadValueAsString(new ContactValueSeparators?(expectedSeparators));
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0002A508 File Offset: 0x00028708
		public string ReadValueAsString()
		{
			return this.ReadValueAsString(null);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0002A524 File Offset: 0x00028724
		private DateTime ReadValueAsDateTime(ContactValueType type, ContactValueSeparators? expectedSeparators)
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
			string s = this.ReadValueAsString(expectedSeparators).Trim();
			this.CheckType(type);
			if (type == ContactValueType.DateTime)
			{
				return ContactCommon.ParseDateTime(s, this.reader.ComplianceTracker);
			}
			if (type == ContactValueType.Time)
			{
				return ContactCommon.ParseTime(s, this.reader.ComplianceTracker);
			}
			return ContactCommon.ParseDate(s, this.reader.ComplianceTracker);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0002A58F File Offset: 0x0002878F
		public DateTime ReadValueAsDateTime(ContactValueType type, ContactValueSeparators expectedSeparators)
		{
			return this.ReadValueAsDateTime(type, new ContactValueSeparators?(expectedSeparators));
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0002A5A0 File Offset: 0x000287A0
		public DateTime ReadValueAsDateTime(ContactValueType type)
		{
			return this.ReadValueAsDateTime(type, null);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0002A5C0 File Offset: 0x000287C0
		private TimeSpan ReadValueAsTimeSpan(ContactValueSeparators? expectedSeparators)
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
			string s = this.ReadValueAsString(expectedSeparators).Trim();
			this.CheckType(ContactValueType.UtcOffset);
			return ContactCommon.ParseUtcOffset(s, this.reader.ComplianceTracker);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0002A600 File Offset: 0x00028800
		public TimeSpan ReadValueAsTimeSpan(ContactValueSeparators expectedSeparators)
		{
			return this.ReadValueAsTimeSpan(new ContactValueSeparators?(expectedSeparators));
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0002A610 File Offset: 0x00028810
		public TimeSpan ReadValueAsTimeSpan()
		{
			return this.ReadValueAsTimeSpan(null);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0002A62C File Offset: 0x0002882C
		private bool ReadValueAsBoolean(ContactValueSeparators? expectedSeparators)
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
			string value = this.ReadValueAsString(expectedSeparators).Trim();
			this.CheckType(ContactValueType.Boolean);
			bool result;
			if (!bool.TryParse(value, out result))
			{
				this.reader.ComplianceTracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidValueFormat);
			}
			return result;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0002A67F File Offset: 0x0002887F
		public bool ReadValueAsBoolean(ContactValueSeparators expectedSeparators)
		{
			return this.ReadValueAsBoolean(new ContactValueSeparators?(expectedSeparators));
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0002A690 File Offset: 0x00028890
		public bool ReadValueAsBoolean()
		{
			return this.ReadValueAsBoolean(null);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0002A6AC File Offset: 0x000288AC
		private double ReadValueAsDouble(ContactValueSeparators? expectedSeparators)
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
			string s = this.ReadValueAsString(expectedSeparators).Trim();
			this.CheckType(ContactValueType.Float);
			double result;
			if (!double.TryParse(s, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out result))
			{
				this.reader.ComplianceTracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidValueFormat);
			}
			return result;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0002A709 File Offset: 0x00028909
		public double ReadValueAsDouble(ContactValueSeparators expectedSeparators)
		{
			return this.ReadValueAsDouble(new ContactValueSeparators?(expectedSeparators));
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0002A718 File Offset: 0x00028918
		public double ReadValueAsDouble()
		{
			return this.ReadValueAsDouble(null);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0002A734 File Offset: 0x00028934
		private int ReadValueAsInt32(ContactValueSeparators? expectedSeparators)
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
			string s = this.ReadValueAsString(expectedSeparators).Trim();
			this.CheckType(ContactValueType.Integer);
			int result;
			if (!int.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				this.reader.ComplianceTracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidValueFormat);
			}
			return result;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0002A78D File Offset: 0x0002898D
		public int ReadValueAsInt32(ContactValueSeparators expectedSeparators)
		{
			return this.ReadValueAsInt32(new ContactValueSeparators?(expectedSeparators));
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0002A79C File Offset: 0x0002899C
		public int ReadValueAsInt32()
		{
			return this.ReadValueAsInt32(null);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0002A7B8 File Offset: 0x000289B8
		public bool ReadNextValue()
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property | ContentLineNodeType.DocumentEnd);
			return this.reader.ReadNextPropertyValue();
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0002A7D2 File Offset: 0x000289D2
		public bool ReadNextProperty()
		{
			this.reader.AssertValidState(ContentLineNodeType.ComponentStart | ContentLineNodeType.ComponentEnd | ContentLineNodeType.Parameter | ContentLineNodeType.Property | ContentLineNodeType.BeforeComponentStart | ContentLineNodeType.BeforeComponentEnd | ContentLineNodeType.DocumentEnd);
			while (this.reader.ReadNextProperty())
			{
				if (this.Name != string.Empty)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0002A803 File Offset: 0x00028A03
		public void ApplyValueOverrides(Encoding charset, ByteEncoder decoder)
		{
			this.reader.ApplyValueOverrides(charset, decoder);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0002A812 File Offset: 0x00028A12
		public Stream GetValueReadStream()
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property);
			this.lastSeparator = ContactValueSeparators.None;
			return this.reader.GetValueReadStream();
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0002A833 File Offset: 0x00028A33
		private void CheckType(ContactValueType type)
		{
			if (this.ValueType != type && this.ValueType != ContactValueType.Text)
			{
				this.reader.ComplianceTracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidValueFormat);
			}
		}

		// Token: 0x04000670 RID: 1648
		private ContentLineReader reader;

		// Token: 0x04000671 RID: 1649
		private ContactValueSeparators lastSeparator;
	}
}
