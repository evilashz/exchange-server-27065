using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200042C RID: 1068
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Flags]
	[Serializable]
	public enum ServiceConfigurationType
	{
		// Token: 0x0400168F RID: 5775
		MailTips = 1,
		// Token: 0x04001690 RID: 5776
		UnifiedMessagingConfiguration = 2,
		// Token: 0x04001691 RID: 5777
		ProtectionRules = 4,
		// Token: 0x04001692 RID: 5778
		PolicyNudges = 8
	}
}
