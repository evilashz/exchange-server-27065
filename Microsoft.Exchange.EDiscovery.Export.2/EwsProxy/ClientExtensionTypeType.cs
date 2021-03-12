using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000110 RID: 272
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ClientExtensionTypeType
	{
		// Token: 0x04000888 RID: 2184
		Default,
		// Token: 0x04000889 RID: 2185
		Private,
		// Token: 0x0400088A RID: 2186
		MarketPlace
	}
}
