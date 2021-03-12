using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000639 RID: 1593
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class RestrictionType
	{
		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x060031B4 RID: 12724 RVA: 0x000B7430 File Offset: 0x000B5630
		// (set) Token: 0x060031B5 RID: 12725 RVA: 0x000B7438 File Offset: 0x000B5638
		[XmlElement("Excludes", typeof(ExcludesType))]
		[XmlElement("Exists", typeof(ExistsType))]
		[XmlElement("IsEqualTo", typeof(IsEqualToType))]
		[XmlElement("IsGreaterThanOrEqualTo", typeof(IsGreaterThanOrEqualToType))]
		[XmlElement("IsLessThan", typeof(IsLessThanType))]
		[DataMember(Name = "Item", IsRequired = true, EmitDefaultValue = false)]
		[XmlElement("And", typeof(AndType))]
		[XmlElement("Contains", typeof(ContainsExpressionType))]
		[XmlElement("IsGreaterThan", typeof(IsGreaterThanType))]
		[XmlElement("IsLessThanOrEqualTo", typeof(IsLessThanOrEqualToType))]
		[XmlElement("IsNotEqualTo", typeof(IsNotEqualToType))]
		[XmlElement("Not", typeof(NotType))]
		[XmlElement("Or", typeof(OrType))]
		[XmlElement("SearchExpression", typeof(SearchExpressionType))]
		public SearchExpressionType Item { get; set; }
	}
}
