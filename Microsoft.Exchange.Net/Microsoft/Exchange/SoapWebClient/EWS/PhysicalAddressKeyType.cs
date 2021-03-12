using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000234 RID: 564
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum PhysicalAddressKeyType
	{
		// Token: 0x04000E9C RID: 3740
		Home,
		// Token: 0x04000E9D RID: 3741
		Business,
		// Token: 0x04000E9E RID: 3742
		Other
	}
}
