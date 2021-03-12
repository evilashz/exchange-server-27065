using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000437 RID: 1079
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Flags]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum UserConfigurationPropertyType
	{
		// Token: 0x040016AE RID: 5806
		Id = 1,
		// Token: 0x040016AF RID: 5807
		Dictionary = 2,
		// Token: 0x040016B0 RID: 5808
		XmlData = 4,
		// Token: 0x040016B1 RID: 5809
		BinaryData = 8,
		// Token: 0x040016B2 RID: 5810
		All = 16
	}
}
