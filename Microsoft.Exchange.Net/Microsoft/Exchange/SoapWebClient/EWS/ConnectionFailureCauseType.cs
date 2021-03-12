using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000325 RID: 805
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ConnectionFailureCauseType
	{
		// Token: 0x0400134E RID: 4942
		None,
		// Token: 0x0400134F RID: 4943
		UserBusy,
		// Token: 0x04001350 RID: 4944
		NoAnswer,
		// Token: 0x04001351 RID: 4945
		Unavailable,
		// Token: 0x04001352 RID: 4946
		Other
	}
}
