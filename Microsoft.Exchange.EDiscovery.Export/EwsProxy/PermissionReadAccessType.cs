using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200020C RID: 524
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum PermissionReadAccessType
	{
		// Token: 0x04000E5D RID: 3677
		None,
		// Token: 0x04000E5E RID: 3678
		FullDetails
	}
}
