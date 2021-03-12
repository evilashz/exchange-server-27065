using System;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x02000055 RID: 85
	internal class CacheLockInfo
	{
		// Token: 0x06000368 RID: 872 RVA: 0x0000A0D4 File Offset: 0x000082D4
		internal CacheLockInfo(int numLockAttempts, TimeSpan lockTimeout, TimeSpan lockSleepTime)
		{
			this.NumLockAttempts = numLockAttempts;
			this.LockTimeout = lockTimeout;
			this.LockSleepTime = lockSleepTime;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000A0F1 File Offset: 0x000082F1
		// (set) Token: 0x0600036A RID: 874 RVA: 0x0000A0F9 File Offset: 0x000082F9
		internal int NumLockAttempts { get; private set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000A102 File Offset: 0x00008302
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0000A10A File Offset: 0x0000830A
		internal TimeSpan LockTimeout { get; private set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000A113 File Offset: 0x00008313
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0000A11B File Offset: 0x0000831B
		internal TimeSpan LockSleepTime { get; private set; }
	}
}
