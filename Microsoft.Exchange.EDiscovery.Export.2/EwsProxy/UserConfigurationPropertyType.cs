using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000356 RID: 854
	[Flags]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum UserConfigurationPropertyType
	{
		// Token: 0x0400125C RID: 4700
		Id = 1,
		// Token: 0x0400125D RID: 4701
		Dictionary = 2,
		// Token: 0x0400125E RID: 4702
		XmlData = 4,
		// Token: 0x0400125F RID: 4703
		BinaryData = 8,
		// Token: 0x04001260 RID: 4704
		All = 16
	}
}
