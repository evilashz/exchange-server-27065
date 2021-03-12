using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003B7 RID: 951
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ClientAccessTokenRequestType
	{
		// Token: 0x040014F8 RID: 5368
		public string Id;

		// Token: 0x040014F9 RID: 5369
		public ClientAccessTokenTypeType TokenType;

		// Token: 0x040014FA RID: 5370
		public string Scope;
	}
}
