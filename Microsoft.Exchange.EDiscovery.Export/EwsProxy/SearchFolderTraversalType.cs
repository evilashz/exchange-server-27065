using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200022A RID: 554
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum SearchFolderTraversalType
	{
		// Token: 0x04000EA9 RID: 3753
		Shallow,
		// Token: 0x04000EAA RID: 3754
		Deep
	}
}
