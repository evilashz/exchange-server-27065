using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000468 RID: 1128
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ConversationQueryTraversalType
	{
		// Token: 0x0400173E RID: 5950
		Shallow,
		// Token: 0x0400173F RID: 5951
		Deep
	}
}
