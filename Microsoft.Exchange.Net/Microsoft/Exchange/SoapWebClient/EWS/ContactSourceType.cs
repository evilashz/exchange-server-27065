using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000237 RID: 567
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ContactSourceType
	{
		// Token: 0x04000EB6 RID: 3766
		ActiveDirectory,
		// Token: 0x04000EB7 RID: 3767
		Store
	}
}
