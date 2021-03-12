using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000024 RID: 36
	public enum JobPickupResult
	{
		// Token: 0x04000097 RID: 151
		JobPickedUp = 1,
		// Token: 0x04000098 RID: 152
		CompletedJobCleanedUp,
		// Token: 0x04000099 RID: 153
		CompletedJobSkipped = 100,
		// Token: 0x0400009A RID: 154
		JobIsPostponed,
		// Token: 0x0400009B RID: 155
		JobAlreadyActive,
		// Token: 0x0400009C RID: 156
		DisabledJobPickup,
		// Token: 0x0400009D RID: 157
		PostponeCancel,
		// Token: 0x0400009E RID: 158
		ReservationFailure = 200,
		// Token: 0x0400009F RID: 159
		ProxyBackoff,
		// Token: 0x040000A0 RID: 160
		PickupFailure,
		// Token: 0x040000A1 RID: 161
		UnknownJobType = 300,
		// Token: 0x040000A2 RID: 162
		InvalidJob,
		// Token: 0x040000A3 RID: 163
		PoisonedJob,
		// Token: 0x040000A4 RID: 164
		JobOwnedByTransportSync
	}
}
