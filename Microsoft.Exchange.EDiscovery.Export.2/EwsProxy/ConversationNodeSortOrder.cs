using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000338 RID: 824
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ConversationNodeSortOrder
	{
		// Token: 0x040011C4 RID: 4548
		TreeOrderAscending,
		// Token: 0x040011C5 RID: 4549
		TreeOrderDescending,
		// Token: 0x040011C6 RID: 4550
		DateOrderAscending,
		// Token: 0x040011C7 RID: 4551
		DateOrderDescending
	}
}
