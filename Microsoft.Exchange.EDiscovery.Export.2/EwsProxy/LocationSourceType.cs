using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000FB RID: 251
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum LocationSourceType
	{
		// Token: 0x04000858 RID: 2136
		None,
		// Token: 0x04000859 RID: 2137
		LocationServices,
		// Token: 0x0400085A RID: 2138
		PhonebookServices,
		// Token: 0x0400085B RID: 2139
		Device,
		// Token: 0x0400085C RID: 2140
		Contact,
		// Token: 0x0400085D RID: 2141
		Resource
	}
}
