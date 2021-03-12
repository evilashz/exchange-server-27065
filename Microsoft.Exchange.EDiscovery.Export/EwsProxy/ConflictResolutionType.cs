using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000395 RID: 917
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ConflictResolutionType
	{
		// Token: 0x0400132C RID: 4908
		NeverOverwrite,
		// Token: 0x0400132D RID: 4909
		AutoResolve,
		// Token: 0x0400132E RID: 4910
		AlwaysOverwrite
	}
}
