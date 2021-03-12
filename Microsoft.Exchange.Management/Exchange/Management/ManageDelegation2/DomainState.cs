using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.ManageDelegation2
{
	// Token: 0x02000DBD RID: 3517
	[XmlType(Namespace = "http://domains.live.com/Service/ManageDelegation2/V1.0")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum DomainState
	{
		// Token: 0x04004150 RID: 16720
		PendingActivation,
		// Token: 0x04004151 RID: 16721
		Active,
		// Token: 0x04004152 RID: 16722
		PendingRelease
	}
}
