using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000357 RID: 855
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum SuggestionQuality
	{
		// Token: 0x04001421 RID: 5153
		Excellent,
		// Token: 0x04001422 RID: 5154
		Good,
		// Token: 0x04001423 RID: 5155
		Fair,
		// Token: 0x04001424 RID: 5156
		Poor
	}
}
