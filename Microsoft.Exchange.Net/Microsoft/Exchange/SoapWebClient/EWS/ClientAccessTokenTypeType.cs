using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000335 RID: 821
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ClientAccessTokenTypeType
	{
		// Token: 0x04001392 RID: 5010
		CallerIdentity,
		// Token: 0x04001393 RID: 5011
		ExtensionCallback,
		// Token: 0x04001394 RID: 5012
		ScopedToken
	}
}
