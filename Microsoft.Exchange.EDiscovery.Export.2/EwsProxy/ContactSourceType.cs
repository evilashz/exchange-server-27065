using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000156 RID: 342
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ContactSourceType
	{
		// Token: 0x04000A64 RID: 2660
		ActiveDirectory,
		// Token: 0x04000A65 RID: 2661
		Store
	}
}
