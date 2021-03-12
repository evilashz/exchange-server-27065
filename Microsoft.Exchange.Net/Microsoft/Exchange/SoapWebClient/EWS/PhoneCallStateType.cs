using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000324 RID: 804
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum PhoneCallStateType
	{
		// Token: 0x04001345 RID: 4933
		Idle,
		// Token: 0x04001346 RID: 4934
		Connecting,
		// Token: 0x04001347 RID: 4935
		Alerted,
		// Token: 0x04001348 RID: 4936
		Connected,
		// Token: 0x04001349 RID: 4937
		Disconnected,
		// Token: 0x0400134A RID: 4938
		Incoming,
		// Token: 0x0400134B RID: 4939
		Transferring,
		// Token: 0x0400134C RID: 4940
		Forwarding
	}
}
