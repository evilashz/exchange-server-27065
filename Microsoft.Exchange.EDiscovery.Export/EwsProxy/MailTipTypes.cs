using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000249 RID: 585
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Flags]
	[Serializable]
	public enum MailTipTypes
	{
		// Token: 0x04000F19 RID: 3865
		All = 1,
		// Token: 0x04000F1A RID: 3866
		OutOfOfficeMessage = 2,
		// Token: 0x04000F1B RID: 3867
		MailboxFullStatus = 4,
		// Token: 0x04000F1C RID: 3868
		CustomMailTip = 8,
		// Token: 0x04000F1D RID: 3869
		ExternalMemberCount = 16,
		// Token: 0x04000F1E RID: 3870
		TotalMemberCount = 32,
		// Token: 0x04000F1F RID: 3871
		MaxMessageSize = 64,
		// Token: 0x04000F20 RID: 3872
		DeliveryRestriction = 128,
		// Token: 0x04000F21 RID: 3873
		ModerationStatus = 256,
		// Token: 0x04000F22 RID: 3874
		InvalidRecipient = 512,
		// Token: 0x04000F23 RID: 3875
		Scope = 1024
	}
}
