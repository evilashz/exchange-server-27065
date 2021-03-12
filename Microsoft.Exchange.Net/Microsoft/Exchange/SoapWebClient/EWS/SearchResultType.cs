using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000283 RID: 643
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum SearchResultType
	{
		// Token: 0x04001074 RID: 4212
		StatisticsOnly,
		// Token: 0x04001075 RID: 4213
		PreviewOnly
	}
}
