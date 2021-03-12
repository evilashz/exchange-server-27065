using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000A5 RID: 165
	internal enum OlcMessageCategory
	{
		// Token: 0x04000425 RID: 1061
		NotDefined,
		// Token: 0x04000426 RID: 1062
		Unread,
		// Token: 0x04000427 RID: 1063
		NotFromContact = 3,
		// Token: 0x04000428 RID: 1064
		FromContact = 5,
		// Token: 0x04000429 RID: 1065
		Flagged = 7,
		// Token: 0x0400042A RID: 1066
		HasAttachment = 9,
		// Token: 0x0400042B RID: 1067
		ResponsesToMe = 11,
		// Token: 0x0400042C RID: 1068
		SMS = 17,
		// Token: 0x0400042D RID: 1069
		Chat = 19,
		// Token: 0x0400042E RID: 1070
		MMS = 21,
		// Token: 0x0400042F RID: 1071
		RepliedTo = 27,
		// Token: 0x04000430 RID: 1072
		Newsletter = 15,
		// Token: 0x04000431 RID: 1073
		Photo = 53,
		// Token: 0x04000432 RID: 1074
		SocialNetwork = 55,
		// Token: 0x04000433 RID: 1075
		Comment = 57,
		// Token: 0x04000434 RID: 1076
		Tag = 59,
		// Token: 0x04000435 RID: 1077
		Video = 61,
		// Token: 0x04000436 RID: 1078
		Document = 63,
		// Token: 0x04000437 RID: 1079
		File = 65,
		// Token: 0x04000438 RID: 1080
		MailingList = 67,
		// Token: 0x04000439 RID: 1081
		ShippingNotification = 69,
		// Token: 0x0400043A RID: 1082
		InteractiveLiveView = 71,
		// Token: 0x0400043B RID: 1083
		DocumentsPlus = 73
	}
}
