using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000477 RID: 1143
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum MessageDispositionType
	{
		// Token: 0x04001782 RID: 6018
		SaveOnly,
		// Token: 0x04001783 RID: 6019
		SendOnly,
		// Token: 0x04001784 RID: 6020
		SendAndSaveCopy
	}
}
