using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200021D RID: 541
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class NotType : SearchExpressionType
	{
		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x000262AA File Offset: 0x000244AA
		// (set) Token: 0x06001532 RID: 5426 RVA: 0x000262B2 File Offset: 0x000244B2
		[XmlElement("SearchExpression", typeof(SearchExpressionType))]
		[XmlElement("IsNotEqualTo", typeof(IsNotEqualToType))]
		[XmlElement("IsLessThanOrEqualTo", typeof(IsLessThanOrEqualToType))]
		[XmlElement("Contains", typeof(ContainsExpressionType))]
		[XmlElement("Excludes", typeof(ExcludesType))]
		[XmlElement("Exists", typeof(ExistsType))]
		[XmlElement("IsEqualTo", typeof(IsEqualToType))]
		[XmlElement("Or", typeof(OrType))]
		[XmlElement("IsGreaterThan", typeof(IsGreaterThanType))]
		[XmlElement("IsGreaterThanOrEqualTo", typeof(IsGreaterThanOrEqualToType))]
		[XmlElement("IsLessThan", typeof(IsLessThanType))]
		[XmlElement("And", typeof(AndType))]
		[XmlElement("Not", typeof(NotType))]
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

		// Token: 0x04000EA0 RID: 3744
		private SearchExpressionType itemField;
	}
}
