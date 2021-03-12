using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000215 RID: 533
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class RestrictionType
	{
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x000261E1 File Offset: 0x000243E1
		// (set) Token: 0x0600151A RID: 5402 RVA: 0x000261E9 File Offset: 0x000243E9
		[XmlElement("Not", typeof(NotType))]
		[XmlElement("IsLessThan", typeof(IsLessThanType))]
		[XmlElement("And", typeof(AndType))]
		[XmlElement("IsLessThanOrEqualTo", typeof(IsLessThanOrEqualToType))]
		[XmlElement("IsNotEqualTo", typeof(IsNotEqualToType))]
		[XmlElement("Or", typeof(OrType))]
		[XmlElement("IsGreaterThan", typeof(IsGreaterThanType))]
		[XmlElement("IsEqualTo", typeof(IsEqualToType))]
		[XmlElement("SearchExpression", typeof(SearchExpressionType))]
		[XmlElement("IsGreaterThanOrEqualTo", typeof(IsGreaterThanOrEqualToType))]
		[XmlElement("Contains", typeof(ContainsExpressionType))]
		[XmlElement("Excludes", typeof(ExcludesType))]
		[XmlElement("Exists", typeof(ExistsType))]
		public SearchExpressionType Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x04000E88 RID: 3720
		private SearchExpressionType itemField;
	}
}
