using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200063A RID: 1594
	[KnownType(typeof(TwoOperandExpressionType))]
	[KnownType(typeof(IsLessThanType))]
	[KnownType(typeof(IsGreaterThanOrEqualToType))]
	[KnownType(typeof(IsGreaterThanType))]
	[KnownType(typeof(IsNotEqualToType))]
	[KnownType(typeof(IsEqualToType))]
	[KnownType(typeof(ExistsType))]
	[XmlInclude(typeof(TwoOperandExpressionType))]
	[KnownType(typeof(ExcludesType))]
	[XmlInclude(typeof(IsGreaterThanOrEqualToType))]
	[KnownType(typeof(ContainsExpressionType))]
	[KnownType(typeof(IsLessThanOrEqualToType))]
	[XmlInclude(typeof(MultipleOperandBooleanExpressionType))]
	[XmlInclude(typeof(OrType))]
	[XmlInclude(typeof(AndType))]
	[XmlInclude(typeof(NotType))]
	[XmlInclude(typeof(ContainsExpressionType))]
	[XmlInclude(typeof(ExcludesType))]
	[XmlInclude(typeof(IsLessThanOrEqualToType))]
	[XmlInclude(typeof(IsLessThanType))]
	[XmlInclude(typeof(IsGreaterThanType))]
	[XmlInclude(typeof(IsNotEqualToType))]
	[XmlInclude(typeof(IsEqualToType))]
	[XmlInclude(typeof(ExistsType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(MultipleOperandBooleanExpressionType))]
	[KnownType(typeof(OrType))]
	[KnownType(typeof(AndType))]
	[KnownType(typeof(NotType))]
	[Serializable]
	public abstract class SearchExpressionType
	{
		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x060031B7 RID: 12727
		[IgnoreDataMember]
		[XmlIgnore]
		internal abstract string FilterType { get; }
	}
}
