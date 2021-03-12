using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003A3 RID: 931
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum FolderQueryTraversalType
	{
		// Token: 0x04001353 RID: 4947
		Shallow,
		// Token: 0x04001354 RID: 4948
		Deep,
		// Token: 0x04001355 RID: 4949
		SoftDeleted
	}
}
