using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002D8 RID: 728
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum BodyTypeResponseType
	{
		// Token: 0x040010B1 RID: 4273
		Best,
		// Token: 0x040010B2 RID: 4274
		HTML,
		// Token: 0x040010B3 RID: 4275
		Text
	}
}
