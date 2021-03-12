using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200014C RID: 332
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum PresentersType
	{
		// Token: 0x040009E9 RID: 2537
		Disabled,
		// Token: 0x040009EA RID: 2538
		Internal,
		// Token: 0x040009EB RID: 2539
		Everyone
	}
}
