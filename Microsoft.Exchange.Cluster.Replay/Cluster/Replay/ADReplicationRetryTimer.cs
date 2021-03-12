using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000150 RID: 336
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ADReplicationRetryTimer : RetryTimer
	{
		// Token: 0x06000D13 RID: 3347 RVA: 0x00039A64 File Offset: 0x00037C64
		public static TimeSpan GetMaxWait()
		{
			return new TimeSpan(0, 0, RegistryParameters.MaxADReplicationWaitInSec);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00039A72 File Offset: 0x00037C72
		public static TimeSpan GetSleepTime()
		{
			return new TimeSpan(0, 0, RegistryParameters.ADReplicationSleepInSec);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00039A80 File Offset: 0x00037C80
		public ADReplicationRetryTimer() : base(ADReplicationRetryTimer.GetMaxWait())
		{
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x00039A8D File Offset: 0x00037C8D
		public override TimeSpan SleepTime
		{
			get
			{
				return ADReplicationRetryTimer.GetSleepTime();
			}
		}
	}
}
