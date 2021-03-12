using System;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.Replay.Dumpster;
using Microsoft.Exchange.Cluster.Shared;

// Token: 0x0200017C RID: 380
internal static class SafetyNetRequestBackoff
{
	// Token: 0x06000F5C RID: 3932 RVA: 0x000422E8 File Offset: 0x000404E8
	static SafetyNetRequestBackoff()
	{
		SafetyNetRequestBackoff.s_backOffEntries[0] = new SafetyNetRequestBackoff.BackoffEntry(TimeSpan.FromMinutes(30.0), TimeSpan.FromSeconds(100.0));
		SafetyNetRequestBackoff.s_backOffEntries[1] = new SafetyNetRequestBackoff.BackoffEntry(TimeSpan.FromHours(4.0), TimeSpan.FromMinutes(15.0));
		SafetyNetRequestBackoff.s_backOffEntries[2] = new SafetyNetRequestBackoff.BackoffEntry(TimeSpan.Zero, TimeSpan.FromHours(1.0));
	}

	// Token: 0x06000F5D RID: 3933 RVA: 0x0004238C File Offset: 0x0004058C
	public static DateTime GetNextDueTime(SafetyNetRequestKey snKey, SafetyNetInfo snInfo, bool inPrimaryPhase)
	{
		if (RegistryParameters.DumpsterRedeliveryIgnoreBackoff)
		{
			return DateTime.UtcNow;
		}
		TimeSpan t = DateTimeHelper.SafeSubtract(DateTime.UtcNow, inPrimaryPhase ? snKey.RequestCreationTimeUtc : snInfo.ShadowRequestCreateTimeUtc);
		for (int i = 0; i < SafetyNetRequestBackoff.s_backOffEntries.Length; i++)
		{
			SafetyNetRequestBackoff.BackoffEntry backoffEntry = SafetyNetRequestBackoff.s_backOffEntries[i];
			if (i == SafetyNetRequestBackoff.s_backOffEntries.Length - 1)
			{
				return snInfo.RequestLastAttemptedTimeUtc.Add(backoffEntry.DueTime);
			}
			if (t <= backoffEntry.AgeLimit)
			{
				return snInfo.RequestLastAttemptedTimeUtc.Add(backoffEntry.DueTime);
			}
		}
		return DateTime.UtcNow;
	}

	// Token: 0x04000650 RID: 1616
	private static SafetyNetRequestBackoff.BackoffEntry[] s_backOffEntries = new SafetyNetRequestBackoff.BackoffEntry[3];

	// Token: 0x0200017D RID: 381
	private struct BackoffEntry
	{
		// Token: 0x06000F5E RID: 3934 RVA: 0x00042432 File Offset: 0x00040632
		public BackoffEntry(TimeSpan ageLimit, TimeSpan dueTime)
		{
			this.AgeLimit = ageLimit;
			this.DueTime = dueTime;
		}

		// Token: 0x04000651 RID: 1617
		public readonly TimeSpan AgeLimit;

		// Token: 0x04000652 RID: 1618
		public readonly TimeSpan DueTime;
	}
}
