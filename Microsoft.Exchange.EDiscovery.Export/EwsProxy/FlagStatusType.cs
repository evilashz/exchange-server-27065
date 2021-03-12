using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000179 RID: 377
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum FlagStatusType
	{
		// Token: 0x04000B43 RID: 2883
		NotFlagged,
		// Token: 0x04000B44 RID: 2884
		Flagged,
		// Token: 0x04000B45 RID: 2885
		Complete
	}
}
