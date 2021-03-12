using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000009 RID: 9
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Flags]
	[XmlType(Namespace = "http://domains.live.com/Service/DomainServices/V1.0")]
	[Serializable]
	public enum PermissionFlags
	{
		// Token: 0x04000074 RID: 116
		IsPasswordResetAllowed = 1,
		// Token: 0x04000075 RID: 117
		IsPreProvisionInboxAllowed = 2,
		// Token: 0x04000076 RID: 118
		IsBlockEmailAllowed = 4
	}
}
