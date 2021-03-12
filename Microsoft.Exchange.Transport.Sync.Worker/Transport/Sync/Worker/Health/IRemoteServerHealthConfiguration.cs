using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Worker.Health
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IRemoteServerHealthConfiguration
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001C RID: 28
		bool RemoteServerHealthManagementEnabled { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001D RID: 29
		bool RemoteServerPoisonMarkingEnabled { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001E RID: 30
		TimeSpan RemoteServerLatencySlidingCounterWindowSize { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001F RID: 31
		TimeSpan RemoteServerLatencySlidingCounterBucketLength { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000020 RID: 32
		TimeSpan RemoteServerLatencyThreshold { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000021 RID: 33
		int RemoteServerBackoffCountLimit { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000022 RID: 34
		TimeSpan RemoteServerBackoffTimeSpan { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000023 RID: 35
		TimeSpan RemoteServerHealthDataExpiryPeriod { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000024 RID: 36
		TimeSpan RemoteServerHealthDataExpiryAndPersistanceFrequency { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000025 RID: 37
		double RemoteServerAllowedCapacityUsagePercentage { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000026 RID: 38
		TimeSpan RemoteServerCapacityUsageThreshold { get; }
	}
}
