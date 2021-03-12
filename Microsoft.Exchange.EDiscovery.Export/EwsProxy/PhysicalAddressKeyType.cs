using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000153 RID: 339
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum PhysicalAddressKeyType
	{
		// Token: 0x04000A4A RID: 2634
		Home,
		// Token: 0x04000A4B RID: 2635
		Business,
		// Token: 0x04000A4C RID: 2636
		Other
	}
}
