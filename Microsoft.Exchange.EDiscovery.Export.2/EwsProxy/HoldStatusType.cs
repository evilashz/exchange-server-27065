using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000197 RID: 407
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum HoldStatusType
	{
		// Token: 0x04000BF6 RID: 3062
		NotOnHold,
		// Token: 0x04000BF7 RID: 3063
		Pending,
		// Token: 0x04000BF8 RID: 3064
		OnHold,
		// Token: 0x04000BF9 RID: 3065
		PartialHold,
		// Token: 0x04000BFA RID: 3066
		Failed
	}
}
