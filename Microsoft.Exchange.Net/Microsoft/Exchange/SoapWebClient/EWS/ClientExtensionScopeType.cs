using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001F2 RID: 498
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ClientExtensionScopeType
	{
		// Token: 0x04000CDE RID: 3294
		None,
		// Token: 0x04000CDF RID: 3295
		User,
		// Token: 0x04000CE0 RID: 3296
		Organization,
		// Token: 0x04000CE1 RID: 3297
		Default
	}
}
