using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200023A RID: 570
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum PhysicalAddressIndexType
	{
		// Token: 0x04000EBF RID: 3775
		None,
		// Token: 0x04000EC0 RID: 3776
		Home,
		// Token: 0x04000EC1 RID: 3777
		Business,
		// Token: 0x04000EC2 RID: 3778
		Other
	}
}
