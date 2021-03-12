using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200032A RID: 810
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Flags]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum MailTipTypes
	{
		// Token: 0x0400136B RID: 4971
		All = 1,
		// Token: 0x0400136C RID: 4972
		OutOfOfficeMessage = 2,
		// Token: 0x0400136D RID: 4973
		MailboxFullStatus = 4,
		// Token: 0x0400136E RID: 4974
		CustomMailTip = 8,
		// Token: 0x0400136F RID: 4975
		ExternalMemberCount = 16,
		// Token: 0x04001370 RID: 4976
		TotalMemberCount = 32,
		// Token: 0x04001371 RID: 4977
		MaxMessageSize = 64,
		// Token: 0x04001372 RID: 4978
		DeliveryRestriction = 128,
		// Token: 0x04001373 RID: 4979
		ModerationStatus = 256,
		// Token: 0x04001374 RID: 4980
		InvalidRecipient = 512,
		// Token: 0x04001375 RID: 4981
		Scope = 1024
	}
}
