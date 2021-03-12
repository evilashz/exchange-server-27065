using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200014E RID: 334
	public enum UserActivityType
	{
		// Token: 0x040007AF RID: 1967
		None,
		// Token: 0x040007B0 RID: 1968
		Typing,
		// Token: 0x040007B1 RID: 1969
		StoppedTyping,
		// Token: 0x040007B2 RID: 1970
		TwoOrMoreTyping,
		// Token: 0x040007B3 RID: 1971
		YouJoined,
		// Token: 0x040007B4 RID: 1972
		LastMessageTime,
		// Token: 0x040007B5 RID: 1973
		ExternalUser,
		// Token: 0x040007B6 RID: 1974
		Subject = 8,
		// Token: 0x040007B7 RID: 1975
		SomeoneJoined,
		// Token: 0x040007B8 RID: 1976
		SomeoneLeft,
		// Token: 0x040007B9 RID: 1977
		FailedInvite,
		// Token: 0x040007BA RID: 1978
		FailedDelivery,
		// Token: 0x040007BB RID: 1979
		FailedDeliveryDueToServerPolicy,
		// Token: 0x040007BC RID: 1980
		AllParticipantsLeftChat,
		// Token: 0x040007BD RID: 1981
		DeliveryTimeout
	}
}
