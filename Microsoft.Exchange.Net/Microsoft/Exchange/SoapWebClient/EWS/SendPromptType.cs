using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000202 RID: 514
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum SendPromptType
	{
		// Token: 0x04000D62 RID: 3426
		None,
		// Token: 0x04000D63 RID: 3427
		Send,
		// Token: 0x04000D64 RID: 3428
		VotingOption
	}
}
