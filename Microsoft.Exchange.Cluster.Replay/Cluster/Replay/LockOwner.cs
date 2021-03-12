using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000159 RID: 345
	internal enum LockOwner
	{
		// Token: 0x040005C0 RID: 1472
		[LocDescription(ReplayStrings.IDs.LockOwnerSuspend)]
		Suspend = 40,
		// Token: 0x040005C1 RID: 1473
		[LocDescription(ReplayStrings.IDs.LockOwnerAttemptCopyLastLogs)]
		AttemptCopyLastLogs = 30,
		// Token: 0x040005C2 RID: 1474
		[LocDescription(ReplayStrings.IDs.LockOwnerBackup)]
		Backup = 21,
		// Token: 0x040005C3 RID: 1475
		[LocDescription(ReplayStrings.IDs.LockOwnerComponent)]
		Component = 10,
		// Token: 0x040005C4 RID: 1476
		[LocDescription(ReplayStrings.IDs.LockOwnerConfigChecker)]
		ConfigChecker,
		// Token: 0x040005C5 RID: 1477
		[LocDescription(ReplayStrings.IDs.LockOwnerIdle)]
		Idle = 0
	}
}
