using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000473 RID: 1139
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum AffectedTaskOccurrencesType
	{
		// Token: 0x0400176D RID: 5997
		AllOccurrences,
		// Token: 0x0400176E RID: 5998
		SpecifiedOccurrenceOnly
	}
}
