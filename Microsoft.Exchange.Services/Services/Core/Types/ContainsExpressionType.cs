using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200063E RID: 1598
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "Contains")]
	[Serializable]
	public class ContainsExpressionType : SearchExpressionType
	{
		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x060031C0 RID: 12736 RVA: 0x000B7488 File Offset: 0x000B5688
		// (set) Token: 0x060031C1 RID: 12737 RVA: 0x000B7490 File Offset: 0x000B5690
		[XmlElement("FieldURI", typeof(PropertyUri))]
		[DataMember(EmitDefaultValue = false, Order = 1)]
		[XmlElement("ExtendedFieldURI", typeof(ExtendedPropertyUri))]
		[XmlElement("IndexedFieldURI", typeof(DictionaryPropertyUri))]
		public PropertyPath Item { get; set; }

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x000B7499 File Offset: 0x000B5699
		// (set) Token: 0x060031C3 RID: 12739 RVA: 0x000B74A1 File Offset: 0x000B56A1
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public ConstantValueType Constant { get; set; }

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x060031C4 RID: 12740 RVA: 0x000B74AA File Offset: 0x000B56AA
		// (set) Token: 0x060031C5 RID: 12741 RVA: 0x000B74B2 File Offset: 0x000B56B2
		[IgnoreDataMember]
		[XmlAttribute]
		public ContainmentModeType ContainmentMode { get; set; }

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x000B74BB File Offset: 0x000B56BB
		// (set) Token: 0x060031C7 RID: 12743 RVA: 0x000B74C3 File Offset: 0x000B56C3
		[IgnoreDataMember]
		[XmlIgnore]
		public bool ContainmentModeSpecified { get; set; }

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x060031C8 RID: 12744 RVA: 0x000B74CC File Offset: 0x000B56CC
		// (set) Token: 0x060031C9 RID: 12745 RVA: 0x000B74E3 File Offset: 0x000B56E3
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Name = "ContainmentMode", Order = 0)]
		public string ContainmentModeString
		{
			get
			{
				if (!this.ContainmentModeSpecified)
				{
					return null;
				}
				return EnumUtilities.ToString<ContainmentModeType>(this.ContainmentMode);
			}
			set
			{
				this.ContainmentMode = EnumUtilities.Parse<ContainmentModeType>(value);
				this.ContainmentModeSpecified = true;
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x060031CA RID: 12746 RVA: 0x000B74F8 File Offset: 0x000B56F8
		// (set) Token: 0x060031CB RID: 12747 RVA: 0x000B7500 File Offset: 0x000B5700
		[XmlAttribute]
		[IgnoreDataMember]
		public ContainmentComparisonType ContainmentComparison { get; set; }

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x060031CC RID: 12748 RVA: 0x000B7509 File Offset: 0x000B5709
		// (set) Token: 0x060031CD RID: 12749 RVA: 0x000B7511 File Offset: 0x000B5711
		[IgnoreDataMember]
		[XmlIgnore]
		public bool ContainmentComparisonSpecified { get; set; }

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x060031CE RID: 12750 RVA: 0x000B751A File Offset: 0x000B571A
		// (set) Token: 0x060031CF RID: 12751 RVA: 0x000B7531 File Offset: 0x000B5731
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Name = "ContainmentComparison", Order = 0)]
		public string ContainmentComparisonString
		{
			get
			{
				if (!this.ContainmentComparisonSpecified)
				{
					return null;
				}
				return EnumUtilities.ToString<ContainmentComparisonType>(this.ContainmentComparison);
			}
			set
			{
				this.ContainmentComparison = EnumUtilities.Parse<ContainmentComparisonType>(value);
				this.ContainmentComparisonSpecified = true;
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x060031D0 RID: 12752 RVA: 0x000B7546 File Offset: 0x000B5746
		internal override string FilterType
		{
			get
			{
				return "Contains";
			}
		}
	}
}
