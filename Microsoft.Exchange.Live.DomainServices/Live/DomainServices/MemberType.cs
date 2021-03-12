using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000012 RID: 18
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://domains.live.com/Service/DomainServices/V1.0")]
	[Serializable]
	public enum MemberType
	{
		// Token: 0x0400009E RID: 158
		MemberUnknown,
		// Token: 0x0400009F RID: 159
		MemberUnmanaged,
		// Token: 0x040000A0 RID: 160
		MemberManaged
	}
}
