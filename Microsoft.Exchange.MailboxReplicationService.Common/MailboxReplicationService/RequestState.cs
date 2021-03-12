using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000217 RID: 535
	public enum RequestState
	{
		// Token: 0x04000BC3 RID: 3011
		None,
		// Token: 0x04000BC4 RID: 3012
		OverallMove,
		// Token: 0x04000BC5 RID: 3013
		Queued,
		// Token: 0x04000BC6 RID: 3014
		InProgress,
		// Token: 0x04000BC7 RID: 3015
		InitializingMove,
		// Token: 0x04000BC8 RID: 3016
		InitialSeeding,
		// Token: 0x04000BC9 RID: 3017
		CreatingMailbox,
		// Token: 0x04000BCA RID: 3018
		CreatingFolderHierarchy,
		// Token: 0x04000BCB RID: 3019
		CreatingInitialSyncCheckpoint,
		// Token: 0x04000BCC RID: 3020
		LoadingMessages,
		// Token: 0x04000BCD RID: 3021
		CopyingMessages,
		// Token: 0x04000BCE RID: 3022
		Completion = 20,
		// Token: 0x04000BCF RID: 3023
		IncrementalSync,
		// Token: 0x04000BD0 RID: 3024
		Finalization,
		// Token: 0x04000BD1 RID: 3025
		DataReplicationWait = 24,
		// Token: 0x04000BD2 RID: 3026
		ADUpdate,
		// Token: 0x04000BD3 RID: 3027
		Cleanup = 23,
		// Token: 0x04000BD4 RID: 3028
		Stalled = 30,
		// Token: 0x04000BD5 RID: 3029
		StalledDueToHA,
		// Token: 0x04000BD6 RID: 3030
		StalledDueToCI,
		// Token: 0x04000BD7 RID: 3031
		StalledDueToMailboxLock,
		// Token: 0x04000BD8 RID: 3032
		StalledDueToReadThrottle,
		// Token: 0x04000BD9 RID: 3033
		StalledDueToWriteThrottle,
		// Token: 0x04000BDA RID: 3034
		StalledDueToReadCpu,
		// Token: 0x04000BDB RID: 3035
		StalledDueToWriteCpu,
		// Token: 0x04000BDC RID: 3036
		StalledDueToReadUnknown,
		// Token: 0x04000BDD RID: 3037
		StalledDueToWriteUnknown,
		// Token: 0x04000BDE RID: 3038
		TransientFailure,
		// Token: 0x04000BDF RID: 3039
		NetworkFailure,
		// Token: 0x04000BE0 RID: 3040
		MDBOffline,
		// Token: 0x04000BE1 RID: 3041
		ProxyBackoff,
		// Token: 0x04000BE2 RID: 3042
		ServerBusyBackoff,
		// Token: 0x04000BE3 RID: 3043
		Suspended = 50,
		// Token: 0x04000BE4 RID: 3044
		AutoSuspended,
		// Token: 0x04000BE5 RID: 3045
		InitialSeedingComplete,
		// Token: 0x04000BE6 RID: 3046
		Idle = 57,
		// Token: 0x04000BE7 RID: 3047
		Relinquished = 60,
		// Token: 0x04000BE8 RID: 3048
		RelinquishedMDBFailover,
		// Token: 0x04000BE9 RID: 3049
		RelinquishedDataGuarantee,
		// Token: 0x04000BEA RID: 3050
		RelinquishedHAStall,
		// Token: 0x04000BEB RID: 3051
		RelinquishedCIStall,
		// Token: 0x04000BEC RID: 3052
		RelinquishedWlmStall,
		// Token: 0x04000BED RID: 3053
		Failed = 70,
		// Token: 0x04000BEE RID: 3054
		FailedBadItemLimit,
		// Token: 0x04000BEF RID: 3055
		FailedNetwork,
		// Token: 0x04000BF0 RID: 3056
		FailedStallDueToHA,
		// Token: 0x04000BF1 RID: 3057
		FailedStallDueToCI,
		// Token: 0x04000BF2 RID: 3058
		FailedMAPI,
		// Token: 0x04000BF3 RID: 3059
		FailedOther,
		// Token: 0x04000BF4 RID: 3060
		FailedStuck,
		// Token: 0x04000BF5 RID: 3061
		Completed = 100,
		// Token: 0x04000BF6 RID: 3062
		CompletedWithWarnings,
		// Token: 0x04000BF7 RID: 3063
		Canceled = 200,
		// Token: 0x04000BF8 RID: 3064
		Removed = 300
	}
}
