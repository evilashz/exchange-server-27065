using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002C0 RID: 704
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum SetClientExtensionActionIdType
	{
		// Token: 0x04001058 RID: 4184
		Install,
		// Token: 0x04001059 RID: 4185
		Uninstall,
		// Token: 0x0400105A RID: 4186
		Configure
	}
}
