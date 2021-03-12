using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000111 RID: 273
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ClientExtensionScopeType
	{
		// Token: 0x0400088C RID: 2188
		None,
		// Token: 0x0400088D RID: 2189
		User,
		// Token: 0x0400088E RID: 2190
		Organization,
		// Token: 0x0400088F RID: 2191
		Default
	}
}
