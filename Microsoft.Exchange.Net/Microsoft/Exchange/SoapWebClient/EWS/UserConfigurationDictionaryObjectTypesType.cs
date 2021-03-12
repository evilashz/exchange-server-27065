using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002C2 RID: 706
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum UserConfigurationDictionaryObjectTypesType
	{
		// Token: 0x04001208 RID: 4616
		DateTime,
		// Token: 0x04001209 RID: 4617
		Boolean,
		// Token: 0x0400120A RID: 4618
		Byte,
		// Token: 0x0400120B RID: 4619
		String,
		// Token: 0x0400120C RID: 4620
		Integer32,
		// Token: 0x0400120D RID: 4621
		UnsignedInteger32,
		// Token: 0x0400120E RID: 4622
		Integer64,
		// Token: 0x0400120F RID: 4623
		UnsignedInteger64,
		// Token: 0x04001210 RID: 4624
		StringArray,
		// Token: 0x04001211 RID: 4625
		ByteArray
	}
}
