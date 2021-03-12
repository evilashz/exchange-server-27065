using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003B9 RID: 953
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum BodyTypeResponseType
	{
		// Token: 0x04001503 RID: 5379
		Best,
		// Token: 0x04001504 RID: 5380
		HTML,
		// Token: 0x04001505 RID: 5381
		Text
	}
}
