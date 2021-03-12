using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200033E RID: 830
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum HoldActionType
	{
		// Token: 0x040011E5 RID: 4581
		Create,
		// Token: 0x040011E6 RID: 4582
		Update,
		// Token: 0x040011E7 RID: 4583
		Remove
	}
}
