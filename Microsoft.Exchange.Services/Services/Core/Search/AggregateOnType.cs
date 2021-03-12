using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000253 RID: 595
	[XmlType(TypeName = "AggregateOnType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class AggregateOnType
	{
		// Token: 0x06000F90 RID: 3984 RVA: 0x0004CA5A File Offset: 0x0004AC5A
		public AggregateOnType()
		{
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0004CA62 File Offset: 0x0004AC62
		internal AggregateOnType(PropertyPath aggregationProperty, AggregateType aggregateType)
		{
			this.AggregationProperty = aggregationProperty;
			this.Aggregate = aggregateType;
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x0004CA78 File Offset: 0x0004AC78
		// (set) Token: 0x06000F93 RID: 3987 RVA: 0x0004CA80 File Offset: 0x0004AC80
		[XmlElement("ExtendedFieldURI", typeof(ExtendedPropertyUri), IsNullable = false)]
		[XmlElement("FieldURI", typeof(PropertyUri), IsNullable = false)]
		[DataMember(Name = "AggregationProperty", Order = 1)]
		[XmlElement("IndexedFieldURI", typeof(DictionaryPropertyUri), IsNullable = false)]
		public PropertyPath AggregationProperty { get; set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x0004CA89 File Offset: 0x0004AC89
		// (set) Token: 0x06000F95 RID: 3989 RVA: 0x0004CA91 File Offset: 0x0004AC91
		[IgnoreDataMember]
		[XmlAttribute("Aggregate")]
		public AggregateType Aggregate { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0004CA9A File Offset: 0x0004AC9A
		// (set) Token: 0x06000F97 RID: 3991 RVA: 0x0004CAA2 File Offset: 0x0004ACA2
		[XmlIgnore]
		[IgnoreDataMember]
		public bool AggregateSpecified { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x0004CAAB File Offset: 0x0004ACAB
		// (set) Token: 0x06000F99 RID: 3993 RVA: 0x0004CAB8 File Offset: 0x0004ACB8
		[XmlIgnore]
		[DataMember(Name = "Aggregate", EmitDefaultValue = false, Order = 2)]
		public string AggregateString
		{
			get
			{
				return EnumUtilities.ToString<AggregateType>(this.Aggregate);
			}
			set
			{
				this.Aggregate = EnumUtilities.Parse<AggregateType>(value);
				this.AggregateSpecified = true;
			}
		}
	}
}
