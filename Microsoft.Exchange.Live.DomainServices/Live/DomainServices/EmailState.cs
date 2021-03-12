using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x0200000D RID: 13
	[XmlType(Namespace = "http://domains.live.com/Service/DomainServices/V1.0")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum EmailState
	{
		// Token: 0x0400008B RID: 139
		Disabled,
		// Token: 0x0400008C RID: 140
		PendingActivation,
		// Token: 0x0400008D RID: 141
		Active,
		// Token: 0x0400008E RID: 142
		PendingDisable,
		// Token: 0x0400008F RID: 143
		PendingDisableDomain
	}
}
