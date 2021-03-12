using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200018E RID: 398
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ResponseClassType
	{
		// Token: 0x0400098E RID: 2446
		Success,
		// Token: 0x0400098F RID: 2447
		Warning,
		// Token: 0x04000990 RID: 2448
		Error
	}
}
