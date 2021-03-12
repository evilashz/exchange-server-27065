using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x0200000B RID: 11
	[XmlType(Namespace = "http://domains.live.com/Service/DomainServices/V1.0")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum NamespaceState
	{
		// Token: 0x0400007F RID: 127
		Unknown,
		// Token: 0x04000080 RID: 128
		Unreserved,
		// Token: 0x04000081 RID: 129
		Reserved,
		// Token: 0x04000082 RID: 130
		ReservedExternal,
		// Token: 0x04000083 RID: 131
		Unavailable
	}
}
