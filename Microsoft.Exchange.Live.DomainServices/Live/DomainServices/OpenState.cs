using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x0200000C RID: 12
	[XmlType(Namespace = "http://domains.live.com/Service/DomainServices/V1.0")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum OpenState
	{
		// Token: 0x04000085 RID: 133
		Disabled,
		// Token: 0x04000086 RID: 134
		Closed,
		// Token: 0x04000087 RID: 135
		Open,
		// Token: 0x04000088 RID: 136
		PendingClose,
		// Token: 0x04000089 RID: 137
		PendingOpen
	}
}
