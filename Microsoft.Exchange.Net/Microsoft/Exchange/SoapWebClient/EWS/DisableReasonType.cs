using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200040E RID: 1038
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum DisableReasonType
	{
		// Token: 0x040015F8 RID: 5624
		NoReason,
		// Token: 0x040015F9 RID: 5625
		OutlookClientPerformance,
		// Token: 0x040015FA RID: 5626
		OWAClientPerformance,
		// Token: 0x040015FB RID: 5627
		MobileClientPerformance
	}
}
