using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200038A RID: 906
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ItemQueryTraversalType
	{
		// Token: 0x04001302 RID: 4866
		Shallow,
		// Token: 0x04001303 RID: 4867
		SoftDeleted,
		// Token: 0x04001304 RID: 4868
		Associated
	}
}
