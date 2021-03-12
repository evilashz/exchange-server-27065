using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200022D RID: 557
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum PresentersType
	{
		// Token: 0x04000E3B RID: 3643
		Disabled,
		// Token: 0x04000E3C RID: 3644
		Internal,
		// Token: 0x04000E3D RID: 3645
		Everyone
	}
}
