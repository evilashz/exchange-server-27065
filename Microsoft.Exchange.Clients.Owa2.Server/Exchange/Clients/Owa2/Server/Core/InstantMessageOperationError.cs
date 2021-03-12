using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200013F RID: 319
	public enum InstantMessageOperationError
	{
		// Token: 0x04000725 RID: 1829
		Success,
		// Token: 0x04000726 RID: 1830
		UnknownDoNotUse = -1,
		// Token: 0x04000727 RID: 1831
		NotEnabled = -2,
		// Token: 0x04000728 RID: 1832
		NotConfigured = -3,
		// Token: 0x04000729 RID: 1833
		SessionDisconnected = -4,
		// Token: 0x0400072A RID: 1834
		EmptyMessage = -5,
		// Token: 0x0400072B RID: 1835
		NoRecipients = -6,
		// Token: 0x0400072C RID: 1836
		InternalErrorInstantMessagingNotSupported = -7,
		// Token: 0x0400072D RID: 1837
		UnableToCreateConversation = -8,
		// Token: 0x0400072E RID: 1838
		ConversationEnded = -9,
		// Token: 0x0400072F RID: 1839
		SelfPresenceNotEstablished = -10,
		// Token: 0x04000730 RID: 1840
		NotSignedIn = -11,
		// Token: 0x04000731 RID: 1841
		UnableToCreateProvider = -12,
		// Token: 0x04000732 RID: 1842
		InitializationInProgress = -13
	}
}
