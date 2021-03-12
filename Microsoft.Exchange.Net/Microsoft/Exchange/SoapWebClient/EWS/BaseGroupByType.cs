using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003CC RID: 972
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(DistinguishedGroupByType))]
	[XmlInclude(typeof(GroupByType))]
	[DebuggerStepThrough]
	[Serializable]
	public abstract class BaseGroupByType
	{
		// Token: 0x04001561 RID: 5473
		[XmlAttribute]
		public SortDirectionType Order;
	}
}
