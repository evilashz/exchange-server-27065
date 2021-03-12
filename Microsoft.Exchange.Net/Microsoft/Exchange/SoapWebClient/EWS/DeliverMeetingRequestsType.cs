using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002D4 RID: 724
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum DeliverMeetingRequestsType
	{
		// Token: 0x04001246 RID: 4678
		DelegatesOnly,
		// Token: 0x04001247 RID: 4679
		DelegatesAndMe,
		// Token: 0x04001248 RID: 4680
		DelegatesAndSendInformationToMe,
		// Token: 0x04001249 RID: 4681
		NoForward
	}
}
