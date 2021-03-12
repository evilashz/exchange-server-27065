using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000197 RID: 407
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class RootItemIdType : BaseItemIdType
	{
		// Token: 0x040009A9 RID: 2473
		[XmlAttribute]
		public string RootItemId;

		// Token: 0x040009AA RID: 2474
		[XmlAttribute]
		public string RootItemChangeKey;
	}
}
