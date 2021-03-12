using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000130 RID: 304
	public enum InstantMessageAlert
	{
		// Token: 0x0400076C RID: 1900
		None,
		// Token: 0x0400076D RID: 1901
		Typing,
		// Token: 0x0400076E RID: 1902
		StoppedTyping,
		// Token: 0x0400076F RID: 1903
		TwoOrMoreTyping,
		// Token: 0x04000770 RID: 1904
		YouJoined,
		// Token: 0x04000771 RID: 1905
		LastMessageTime,
		// Token: 0x04000772 RID: 1906
		ExternalUser,
		// Token: 0x04000773 RID: 1907
		Subject = 8,
		// Token: 0x04000774 RID: 1908
		SomeoneJoined,
		// Token: 0x04000775 RID: 1909
		SomeoneLeft,
		// Token: 0x04000776 RID: 1910
		FailedInvite,
		// Token: 0x04000777 RID: 1911
		FailedDelivery,
		// Token: 0x04000778 RID: 1912
		FailedDeliveryDueToServerPolicy,
		// Token: 0x04000779 RID: 1913
		AllParticipantsLeftChat
	}
}
