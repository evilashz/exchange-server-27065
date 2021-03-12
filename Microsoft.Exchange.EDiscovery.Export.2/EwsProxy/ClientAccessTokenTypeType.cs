using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000254 RID: 596
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ClientAccessTokenTypeType
	{
		// Token: 0x04000F40 RID: 3904
		CallerIdentity,
		// Token: 0x04000F41 RID: 3905
		ExtensionCallback,
		// Token: 0x04000F42 RID: 3906
		ScopedToken
	}
}
