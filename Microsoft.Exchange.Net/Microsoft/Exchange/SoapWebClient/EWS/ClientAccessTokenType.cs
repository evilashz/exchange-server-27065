using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000334 RID: 820
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class ClientAccessTokenType
	{
		// Token: 0x0400138D RID: 5005
		public string Id;

		// Token: 0x0400138E RID: 5006
		public ClientAccessTokenTypeType TokenType;

		// Token: 0x0400138F RID: 5007
		public string TokenValue;

		// Token: 0x04001390 RID: 5008
		[XmlElement(DataType = "integer")]
		public string TTL;
	}
}
