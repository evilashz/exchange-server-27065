using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002FC RID: 764
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ContainmentModeType
	{
		// Token: 0x040012E4 RID: 4836
		FullString,
		// Token: 0x040012E5 RID: 4837
		Prefixed,
		// Token: 0x040012E6 RID: 4838
		Substring,
		// Token: 0x040012E7 RID: 4839
		PrefixOnWords,
		// Token: 0x040012E8 RID: 4840
		ExactPhrase
	}
}
