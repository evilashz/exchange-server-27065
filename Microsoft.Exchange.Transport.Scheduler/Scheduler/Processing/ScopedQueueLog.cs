using System;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x0200002D RID: 45
	internal class ScopedQueueLog : QueueLog
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00004ED0 File Offset: 0x000030D0
		public ScopedQueueLog(DateTime lockTime)
		{
			this.lastLockTime = lockTime;
			this.locked = true;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004EFC File Offset: 0x000030FC
		public void Lock(DateTime lockTime)
		{
			if (!this.locked)
			{
				this.locked = true;
				this.lastLockTime = lockTime;
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004F14 File Offset: 0x00003114
		public void Unlock(DateTime unlockTime)
		{
			if (this.locked)
			{
				this.locked = false;
				this.lockDuration = this.lockDuration.Add(unlockTime - this.lastLockTime);
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004F44 File Offset: 0x00003144
		protected override void FlushInternal(DateTime timestamp, QueueLogInfo info)
		{
			info.TotalLockTime = this.lockDuration;
			if (this.locked && timestamp > this.lastLockTime)
			{
				info.TotalLockTime = info.TotalLockTime.Add(timestamp - this.lastLockTime);
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004F93 File Offset: 0x00003193
		protected override void ResetInternal(DateTime timetamp)
		{
			if (this.locked)
			{
				this.lastLockTime = timetamp;
			}
			this.lockDuration = TimeSpan.Zero;
		}

		// Token: 0x04000099 RID: 153
		private TimeSpan lockDuration = TimeSpan.Zero;

		// Token: 0x0400009A RID: 154
		private DateTime lastLockTime = DateTime.MinValue;

		// Token: 0x0400009B RID: 155
		private bool locked;
	}
}
