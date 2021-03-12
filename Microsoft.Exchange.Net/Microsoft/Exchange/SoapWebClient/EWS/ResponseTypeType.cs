using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200020E RID: 526
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ResponseTypeType
	{
		// Token: 0x04000DE2 RID: 3554
		Unknown,
		// Token: 0x04000DE3 RID: 3555
		Organizer,
		// Token: 0x04000DE4 RID: 3556
		Tentative,
		// Token: 0x04000DE5 RID: 3557
		Accept,
		// Token: 0x04000DE6 RID: 3558
		Decline,
		// Token: 0x04000DE7 RID: 3559
		NoResponseReceived
	}
}
