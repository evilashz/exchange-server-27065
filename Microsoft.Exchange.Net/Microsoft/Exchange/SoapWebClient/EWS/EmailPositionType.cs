using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200025E RID: 606
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum EmailPositionType
	{
		// Token: 0x04000FA2 RID: 4002
		LatestReply,
		// Token: 0x04000FA3 RID: 4003
		Other,
		// Token: 0x04000FA4 RID: 4004
		Subject,
		// Token: 0x04000FA5 RID: 4005
		Signature
	}
}
