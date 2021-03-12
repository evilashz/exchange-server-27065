using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001AB RID: 427
	[Serializable]
	public sealed class Throttles
	{
		// Token: 0x0600101F RID: 4127 RVA: 0x0002600C File Offset: 0x0002420C
		internal Throttles(ThrottleDurations throttleDurations)
		{
			this.MdbThrottle = this.GetEnhancedTimeSpanFromTicks(throttleDurations.MdbThrottleTicks);
			this.CpuThrottle = this.GetEnhancedTimeSpanFromTicks(throttleDurations.CpuThrottleTicks);
			this.MdbReplicationThrottle = this.GetEnhancedTimeSpanFromTicks(throttleDurations.MdbReplicationThrottleTicks);
			this.ContentIndexingThrottle = this.GetEnhancedTimeSpanFromTicks(throttleDurations.ContentIndexingThrottleTicks);
			this.UnknownThrottle = this.GetEnhancedTimeSpanFromTicks(throttleDurations.UnknownThrottleTicks);
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x00026079 File Offset: 0x00024279
		// (set) Token: 0x06001021 RID: 4129 RVA: 0x00026081 File Offset: 0x00024281
		public EnhancedTimeSpan MdbThrottle { get; set; }

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x0002608A File Offset: 0x0002428A
		// (set) Token: 0x06001023 RID: 4131 RVA: 0x00026092 File Offset: 0x00024292
		public EnhancedTimeSpan CpuThrottle { get; set; }

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0002609B File Offset: 0x0002429B
		// (set) Token: 0x06001025 RID: 4133 RVA: 0x000260A3 File Offset: 0x000242A3
		public EnhancedTimeSpan MdbReplicationThrottle { get; set; }

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x000260AC File Offset: 0x000242AC
		// (set) Token: 0x06001027 RID: 4135 RVA: 0x000260B4 File Offset: 0x000242B4
		public EnhancedTimeSpan ContentIndexingThrottle { get; set; }

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x000260BD File Offset: 0x000242BD
		// (set) Token: 0x06001029 RID: 4137 RVA: 0x000260C5 File Offset: 0x000242C5
		public EnhancedTimeSpan UnknownThrottle { get; set; }

		// Token: 0x0600102A RID: 4138 RVA: 0x000260D0 File Offset: 0x000242D0
		public override string ToString()
		{
			return MrsStrings.ReportThrottles(this.MdbThrottle.ToString(), this.CpuThrottle.ToString(), this.MdbReplicationThrottle.ToString(), this.ContentIndexingThrottle.ToString(), this.UnknownThrottle.ToString());
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0002614C File Offset: 0x0002434C
		private EnhancedTimeSpan GetEnhancedTimeSpanFromTicks(long ticks)
		{
			return new TimeSpan(ticks - ticks % 10000000L);
		}
	}
}
