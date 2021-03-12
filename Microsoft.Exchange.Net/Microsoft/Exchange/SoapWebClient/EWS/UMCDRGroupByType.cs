using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003F7 RID: 1015
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum UMCDRGroupByType
	{
		// Token: 0x040015BA RID: 5562
		Day,
		// Token: 0x040015BB RID: 5563
		Month,
		// Token: 0x040015BC RID: 5564
		Total
	}
}
