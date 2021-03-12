using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001E1 RID: 481
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum UserConfigurationDictionaryObjectTypesType
	{
		// Token: 0x04000DB6 RID: 3510
		DateTime,
		// Token: 0x04000DB7 RID: 3511
		Boolean,
		// Token: 0x04000DB8 RID: 3512
		Byte,
		// Token: 0x04000DB9 RID: 3513
		String,
		// Token: 0x04000DBA RID: 3514
		Integer32,
		// Token: 0x04000DBB RID: 3515
		UnsignedInteger32,
		// Token: 0x04000DBC RID: 3516
		Integer64,
		// Token: 0x04000DBD RID: 3517
		UnsignedInteger64,
		// Token: 0x04000DBE RID: 3518
		StringArray,
		// Token: 0x04000DBF RID: 3519
		ByteArray
	}
}
