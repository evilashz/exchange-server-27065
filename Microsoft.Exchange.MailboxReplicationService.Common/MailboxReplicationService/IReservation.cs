using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200026D RID: 621
	internal interface IReservation : IDisposable
	{
		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06001F3D RID: 7997
		Guid Id { get; }

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06001F3E RID: 7998
		ReservationFlags Flags { get; }

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06001F3F RID: 7999
		Guid ResourceId { get; }
	}
}
