using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000287 RID: 647
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class SearchRefinerItemType
	{
		// Token: 0x04001097 RID: 4247
		public string Name;

		// Token: 0x04001098 RID: 4248
		public string Value;

		// Token: 0x04001099 RID: 4249
		public long Count;

		// Token: 0x0400109A RID: 4250
		public string Token;
	}
}
