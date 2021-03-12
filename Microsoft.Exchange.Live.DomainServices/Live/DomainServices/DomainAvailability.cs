using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000011 RID: 17
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://domains.live.com/Service/DomainServices/V1.0")]
	[Serializable]
	public enum DomainAvailability
	{
		// Token: 0x04000099 RID: 153
		Available,
		// Token: 0x0400009A RID: 154
		Reserved,
		// Token: 0x0400009B RID: 155
		ReservedOther,
		// Token: 0x0400009C RID: 156
		Unavailable
	}
}
