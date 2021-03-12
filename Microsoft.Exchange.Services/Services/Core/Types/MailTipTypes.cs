using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200043D RID: 1085
	[XmlType(TypeName = "MailTipTypes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Flags]
	[Serializable]
	public enum MailTipTypes
	{
		// Token: 0x04001405 RID: 5125
		All = 1,
		// Token: 0x04001406 RID: 5126
		OutOfOfficeMessage = 2,
		// Token: 0x04001407 RID: 5127
		MailboxFullStatus = 4,
		// Token: 0x04001408 RID: 5128
		CustomMailTip = 8,
		// Token: 0x04001409 RID: 5129
		ExternalMemberCount = 16,
		// Token: 0x0400140A RID: 5130
		TotalMemberCount = 32,
		// Token: 0x0400140B RID: 5131
		MaxMessageSize = 64,
		// Token: 0x0400140C RID: 5132
		DeliveryRestriction = 128,
		// Token: 0x0400140D RID: 5133
		ModerationStatus = 256,
		// Token: 0x0400140E RID: 5134
		InvalidRecipient = 512,
		// Token: 0x0400140F RID: 5135
		Scope = 1024
	}
}
