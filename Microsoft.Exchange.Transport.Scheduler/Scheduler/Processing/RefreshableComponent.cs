using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x0200000E RID: 14
	internal abstract class RefreshableComponent
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002A08 File Offset: 0x00000C08
		protected RefreshableComponent(TimeSpan updateInterval, Func<DateTime> timeProvider)
		{
			ArgumentValidator.ThrowIfNull("timeProvider", timeProvider);
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("updateInterval", updateInterval, TimeSpan.Zero.Add(TimeSpan.FromTicks(1L)), TimeSpan.MaxValue);
			this.UpdateInterval = updateInterval;
			this.timeProvider = timeProvider;
			this.LastUpdated = this.GetCurrentTime();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002A6B File Offset: 0x00000C6B
		protected RefreshableComponent(TimeSpan updateInterval) : this(updateInterval, () => DateTime.UtcNow)
		{
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002A91 File Offset: 0x00000C91
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002A99 File Offset: 0x00000C99
		public DateTime LastUpdated { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002AA2 File Offset: 0x00000CA2
		protected Func<DateTime> TimeProvider
		{
			get
			{
				return this.timeProvider;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002AAA File Offset: 0x00000CAA
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002AB2 File Offset: 0x00000CB2
		private TimeSpan UpdateInterval { get; set; }

		// Token: 0x06000035 RID: 53 RVA: 0x00002ABC File Offset: 0x00000CBC
		public void RefreshIfNecessary()
		{
			DateTime currentTime = this.GetCurrentTime();
			if (this.LastUpdated.Add(this.UpdateInterval) < currentTime)
			{
				this.Refresh(currentTime);
				this.LastUpdated = currentTime;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002AFA File Offset: 0x00000CFA
		protected DateTime GetCurrentTime()
		{
			if (this.timeProvider == null)
			{
				return DateTime.UtcNow;
			}
			return this.timeProvider();
		}

		// Token: 0x06000037 RID: 55
		protected abstract void Refresh(DateTime timestamp);

		// Token: 0x04000024 RID: 36
		private readonly Func<DateTime> timeProvider;
	}
}
