using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003AF RID: 943
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class EmailAddress
	{
		// Token: 0x040014D6 RID: 5334
		public string Name;

		// Token: 0x040014D7 RID: 5335
		public string Address;

		// Token: 0x040014D8 RID: 5336
		public string RoutingType;
	}
}
