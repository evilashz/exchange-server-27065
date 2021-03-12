using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200045C RID: 1116
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetClientAccessTokenType : BaseRequestType
	{
		// Token: 0x04001708 RID: 5896
		[XmlArrayItem("TokenRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ClientAccessTokenRequestType[] TokenRequests;
	}
}
