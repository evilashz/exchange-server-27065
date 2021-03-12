using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200014B RID: 331
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum OnlineMeetingAccessLevelType
	{
		// Token: 0x040009E4 RID: 2532
		Locked,
		// Token: 0x040009E5 RID: 2533
		Invited,
		// Token: 0x040009E6 RID: 2534
		Internal,
		// Token: 0x040009E7 RID: 2535
		Everyone
	}
}
