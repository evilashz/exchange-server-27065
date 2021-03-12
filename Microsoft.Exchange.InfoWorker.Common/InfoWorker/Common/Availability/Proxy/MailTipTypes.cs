using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x02000130 RID: 304
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Flags]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public enum MailTipTypes
	{
		// Token: 0x04000674 RID: 1652
		All = 1,
		// Token: 0x04000675 RID: 1653
		OutOfOfficeMessage = 2,
		// Token: 0x04000676 RID: 1654
		MailboxFullStatus = 4,
		// Token: 0x04000677 RID: 1655
		CustomMailTip = 8,
		// Token: 0x04000678 RID: 1656
		ExternalMemberCount = 16,
		// Token: 0x04000679 RID: 1657
		TotalMemberCount = 32,
		// Token: 0x0400067A RID: 1658
		MaxMessageSize = 64,
		// Token: 0x0400067B RID: 1659
		DeliveryRestriction = 128,
		// Token: 0x0400067C RID: 1660
		ModerationStatus = 256,
		// Token: 0x0400067D RID: 1661
		InvalidRecipient = 512,
		// Token: 0x0400067E RID: 1662
		Scope = 1024
	}
}
