using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000640 RID: 1600
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "Not")]
	[Serializable]
	public class NotType : SearchExpressionType, INonLeafSearchExpressionType
	{
		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x060031D5 RID: 12757 RVA: 0x000B756E File Offset: 0x000B576E
		// (set) Token: 0x060031D6 RID: 12758 RVA: 0x000B7576 File Offset: 0x000B5776
		[XmlElement("IsEqualTo", typeof(IsEqualToType))]
		[DataMember(EmitDefaultValue = false)]
		[XmlElement("IsGreaterThanOrEqualTo", typeof(IsGreaterThanOrEqualToType))]
		[XmlElement("SearchExpression", typeof(SearchExpressionType))]
		[XmlElement("Contains", typeof(ContainsExpressionType))]
		[XmlElement("Excludes", typeof(ExcludesType))]
		[XmlElement("Exists", typeof(ExistsType))]
		[XmlElement("IsGreaterThan", typeof(IsGreaterThanType))]
		[XmlElement("And", typeof(AndType))]
		[XmlElement("IsLessThan", typeof(IsLessThanType))]
		[XmlElement("IsLessThanOrEqualTo", typeof(IsLessThanOrEqualToType))]
		[XmlElement("IsNotEqualTo", typeof(IsNotEqualToType))]
		[XmlElement("Not", typeof(NotType))]
		[XmlElement("Or", typeof(OrType))]
		public SearchExpressionType Item { get; set; }

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x060031D7 RID: 12759 RVA: 0x000B7580 File Offset: 0x000B5780
		// (set) Token: 0x060031D8 RID: 12760 RVA: 0x000B759E File Offset: 0x000B579E
		[XmlIgnore]
		[IgnoreDataMember]
		public SearchExpressionType[] Items
		{
			get
			{
				return new SearchExpressionType[]
				{
					this.Item
				};
			}
			set
			{
				this.Item = value[0];
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x060031D9 RID: 12761 RVA: 0x000B75A9 File Offset: 0x000B57A9
		internal override string FilterType
		{
			get
			{
				return "Not";
			}
		}
	}
}
