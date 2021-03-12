using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.ManageDelegation1
{
	// Token: 0x02000DAF RID: 3503
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://domains.live.com/Service/ManageDelegation/V1.0")]
	[Serializable]
	public enum DomainState
	{
		// Token: 0x04004142 RID: 16706
		PendingActivation,
		// Token: 0x04004143 RID: 16707
		Active,
		// Token: 0x04004144 RID: 16708
		PendingRelease
	}
}
