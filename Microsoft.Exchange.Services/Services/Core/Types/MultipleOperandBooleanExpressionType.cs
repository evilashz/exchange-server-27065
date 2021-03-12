using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200063B RID: 1595
	[XmlInclude(typeof(AndType))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(OrType))]
	[KnownType(typeof(AndType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(OrType))]
	[Serializable]
	public abstract class MultipleOperandBooleanExpressionType : SearchExpressionType, INonLeafSearchExpressionType
	{
		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x060031B9 RID: 12729 RVA: 0x000B7451 File Offset: 0x000B5651
		// (set) Token: 0x060031BA RID: 12730 RVA: 0x000B7459 File Offset: 0x000B5659
		[XmlElement("Contains", typeof(ContainsExpressionType))]
		[XmlElement("Or", typeof(OrType))]
		[XmlElement("IsEqualTo", typeof(IsEqualToType))]
		[XmlElement("Exists", typeof(ExistsType))]
		[DataMember(EmitDefaultValue = false)]
		[XmlElement("IsNotEqualTo", typeof(IsNotEqualToType))]
		[XmlElement("SearchExpression", typeof(SearchExpressionType))]
		[XmlElement("And", typeof(AndType))]
		[XmlElement("IsGreaterThan", typeof(IsGreaterThanType))]
		[XmlElement("IsGreaterThanOrEqualTo", typeof(IsGreaterThanOrEqualToType))]
		[XmlElement("Excludes", typeof(ExcludesType))]
		[XmlElement("Not", typeof(NotType))]
		[XmlElement("IsLessThan", typeof(IsLessThanType))]
		[XmlElement("IsLessThanOrEqualTo", typeof(IsLessThanOrEqualToType))]
		public SearchExpressionType[] Items { get; set; }
	}
}
