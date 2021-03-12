using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005D9 RID: 1497
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class ExtendedPropertyType
	{
		// Token: 0x06002D0F RID: 11535 RVA: 0x000B1C10 File Offset: 0x000AFE10
		public ExtendedPropertyType()
		{
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x000B1C18 File Offset: 0x000AFE18
		public ExtendedPropertyType(ExtendedPropertyUri propertyUri, string value)
		{
			this.ExtendedFieldURI = propertyUri;
			this.singleValue = value;
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000B1C2E File Offset: 0x000AFE2E
		public ExtendedPropertyType(ExtendedPropertyUri propertyUri, string[] values)
		{
			this.ExtendedFieldURI = propertyUri;
			this.arrayValue = new NonEmptyArrayOfPropertyValues(values);
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06002D12 RID: 11538 RVA: 0x000B1C49 File Offset: 0x000AFE49
		// (set) Token: 0x06002D13 RID: 11539 RVA: 0x000B1C51 File Offset: 0x000AFE51
		[DataMember(EmitDefaultValue = false)]
		public ExtendedPropertyUri ExtendedFieldURI { get; set; }

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06002D14 RID: 11540 RVA: 0x000B1C5A File Offset: 0x000AFE5A
		// (set) Token: 0x06002D15 RID: 11541 RVA: 0x000B1C71 File Offset: 0x000AFE71
		[IgnoreDataMember]
		[XmlElement("Value", typeof(string))]
		[XmlElement("Values", typeof(NonEmptyArrayOfPropertyValues))]
		public object Item
		{
			get
			{
				if (this.singleValue != null)
				{
					return this.singleValue;
				}
				return this.arrayValue;
			}
			set
			{
				if (value is NonEmptyArrayOfPropertyValues)
				{
					this.arrayValue = (NonEmptyArrayOfPropertyValues)value;
					return;
				}
				this.singleValue = value;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06002D16 RID: 11542 RVA: 0x000B1C8F File Offset: 0x000AFE8F
		// (set) Token: 0x06002D17 RID: 11543 RVA: 0x000B1C97 File Offset: 0x000AFE97
		[XmlIgnore]
		[DataMember(Name = "Value", EmitDefaultValue = false)]
		public object Value
		{
			get
			{
				return this.singleValue;
			}
			set
			{
				this.singleValue = value;
				this.arrayValue = null;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06002D18 RID: 11544 RVA: 0x000B1CA7 File Offset: 0x000AFEA7
		// (set) Token: 0x06002D19 RID: 11545 RVA: 0x000B1CBE File Offset: 0x000AFEBE
		[XmlIgnore]
		[DataMember(Name = "Values", EmitDefaultValue = false)]
		public string[] Values
		{
			get
			{
				if (this.arrayValue == null)
				{
					return null;
				}
				return this.arrayValue.Values;
			}
			set
			{
				this.arrayValue = new NonEmptyArrayOfPropertyValues(value);
				this.singleValue = null;
			}
		}

		// Token: 0x04001B12 RID: 6930
		private object singleValue;

		// Token: 0x04001B13 RID: 6931
		private NonEmptyArrayOfPropertyValues arrayValue;
	}
}
