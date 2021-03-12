using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002F6 RID: 758
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class RestrictionType
	{
		// Token: 0x040012DA RID: 4826
		[XmlElement("IsGreaterThanOrEqualTo", typeof(IsGreaterThanOrEqualToType))]
		[XmlElement("IsGreaterThan", typeof(IsGreaterThanType))]
		[XmlElement("IsLessThanOrEqualTo", typeof(IsLessThanOrEqualToType))]
		[XmlElement("IsNotEqualTo", typeof(IsNotEqualToType))]
		[XmlElement("Not", typeof(NotType))]
		[XmlElement("Or", typeof(OrType))]
		[XmlElement("Contains", typeof(ContainsExpressionType))]
		[XmlElement("Excludes", typeof(ExcludesType))]
		[XmlElement("Exists", typeof(ExistsType))]
		[XmlElement("IsEqualTo", typeof(IsEqualToType))]
		[XmlElement("And", typeof(AndType))]
		[XmlElement("SearchExpression", typeof(SearchExpressionType))]
		[XmlElement("IsLessThan", typeof(IsLessThanType))]
		public SearchExpressionType Item;
	}
}
