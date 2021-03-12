using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200005C RID: 92
	[DataContract(Name = "lobbyBypassForPhoneUsers")]
	internal enum LobbyBypassForPhoneUsers
	{
		// Token: 0x040001C4 RID: 452
		[EnumMember]
		Disabled,
		// Token: 0x040001C5 RID: 453
		[EnumMember]
		Enabled
	}
}
