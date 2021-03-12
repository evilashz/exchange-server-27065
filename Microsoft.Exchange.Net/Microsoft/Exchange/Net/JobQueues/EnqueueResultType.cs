using System;

namespace Microsoft.Exchange.Net.JobQueues
{
	// Token: 0x0200073E RID: 1854
	public enum EnqueueResultType
	{
		// Token: 0x040021D1 RID: 8657
		Successful,
		// Token: 0x040021D2 RID: 8658
		AlreadyPending,
		// Token: 0x040021D3 RID: 8659
		QueueLengthLimitReached,
		// Token: 0x040021D4 RID: 8660
		QueueServerNotInitialized,
		// Token: 0x040021D5 RID: 8661
		QueueServerShutDown,
		// Token: 0x040021D6 RID: 8662
		InvalidData,
		// Token: 0x040021D7 RID: 8663
		DirectoryError,
		// Token: 0x040021D8 RID: 8664
		StorageError,
		// Token: 0x040021D9 RID: 8665
		RpcError,
		// Token: 0x040021DA RID: 8666
		ClientInitError,
		// Token: 0x040021DB RID: 8667
		UnexpectedServerError,
		// Token: 0x040021DC RID: 8668
		RequestThrottled,
		// Token: 0x040021DD RID: 8669
		UnlinkedTeamMailbox,
		// Token: 0x040021DE RID: 8670
		WrongServer,
		// Token: 0x040021DF RID: 8671
		UnknownError,
		// Token: 0x040021E0 RID: 8672
		PendingDeleteTeamMailbox,
		// Token: 0x040021E1 RID: 8673
		ClosedTeamMailbox,
		// Token: 0x040021E2 RID: 8674
		NonexistentTeamMailbox
	}
}
