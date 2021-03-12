using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000419 RID: 1049
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ConversationNodeSortOrder
	{
		// Token: 0x04001616 RID: 5654
		TreeOrderAscending,
		// Token: 0x04001617 RID: 5655
		TreeOrderDescending,
		// Token: 0x04001618 RID: 5656
		DateOrderAscending,
		// Token: 0x04001619 RID: 5657
		DateOrderDescending
	}
}
