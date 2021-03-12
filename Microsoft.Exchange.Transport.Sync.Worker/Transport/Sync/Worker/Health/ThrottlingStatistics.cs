using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Worker.Throttling;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Transport.Sync.Worker.Health
{
	// Token: 0x0200002D RID: 45
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ThrottlingStatistics
	{
		// Token: 0x0600023D RID: 573 RVA: 0x0000ADC4 File Offset: 0x00008FC4
		internal ThrottlingStatistics()
		{
			this.throttleInfo = new List<ThrottlingInfo>();
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000ADE2 File Offset: 0x00008FE2
		public TimeSpan TotalCpuUnhealthyBackoffTime
		{
			get
			{
				return this.GetTotalBackOffTime(SyncResourceMonitorType.MailboxCPU, ResourceLoadState.Critical);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000ADEC File Offset: 0x00008FEC
		public TimeSpan TotalCpuFairBackoffTime
		{
			get
			{
				return this.GetTotalBackOffTime(SyncResourceMonitorType.MailboxCPU, ResourceLoadState.Overloaded);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000ADF6 File Offset: 0x00008FF6
		public TimeSpan TotalCpuUnknownBackoffTime
		{
			get
			{
				return this.GetTotalBackOffTime(SyncResourceMonitorType.MailboxCPU, ResourceLoadState.Unknown);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000AE00 File Offset: 0x00009000
		public TimeSpan TotalDatabaseRPCLatencyUnhealthyBackoffTime
		{
			get
			{
				return this.GetTotalBackOffTime(SyncResourceMonitorType.DatabaseRPCLatency, ResourceLoadState.Critical);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000AE0A File Offset: 0x0000900A
		public TimeSpan TotalDatabaseRPCLatencyFairBackoffTime
		{
			get
			{
				return this.GetTotalBackOffTime(SyncResourceMonitorType.DatabaseRPCLatency, ResourceLoadState.Overloaded);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000AE14 File Offset: 0x00009014
		public TimeSpan TotalDatabaseRPCLatencyUnknownBackoffTime
		{
			get
			{
				return this.GetTotalBackOffTime(SyncResourceMonitorType.DatabaseRPCLatency, ResourceLoadState.Unknown);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000AE1E File Offset: 0x0000901E
		public TimeSpan TotalDatabaseReplicationLogUnhealthyBackoffTime
		{
			get
			{
				return this.GetTotalBackOffTime(SyncResourceMonitorType.DatabaseReplicationLog, ResourceLoadState.Critical);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000AE28 File Offset: 0x00009028
		public TimeSpan TotalDatabaseReplicationLogFairBackoffTime
		{
			get
			{
				return this.GetTotalBackOffTime(SyncResourceMonitorType.DatabaseReplicationLog, ResourceLoadState.Overloaded);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000AE32 File Offset: 0x00009032
		public TimeSpan TotalDatabaseReplicationLogUnknownBackoffTime
		{
			get
			{
				return this.GetTotalBackOffTime(SyncResourceMonitorType.DatabaseReplicationLog, ResourceLoadState.Unknown);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000AE3C File Offset: 0x0000903C
		public TimeSpan TotalUnknownUnhealthyBackoffTime
		{
			get
			{
				return this.GetTotalBackOffTime(SyncResourceMonitorType.Unknown, ResourceLoadState.Critical);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000AE46 File Offset: 0x00009046
		public TimeSpan TotalUnknownFairBackoffTime
		{
			get
			{
				return this.GetTotalBackOffTime(SyncResourceMonitorType.Unknown, ResourceLoadState.Overloaded);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000AE50 File Offset: 0x00009050
		public TimeSpan TotalUnknownUnknownBackoffTime
		{
			get
			{
				return this.GetTotalBackOffTime(SyncResourceMonitorType.Unknown, ResourceLoadState.Unknown);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000AE5A File Offset: 0x0000905A
		public TimeSpan TotalBackoffTime
		{
			get
			{
				return this.GetTotalBackOffTime();
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000AE62 File Offset: 0x00009062
		internal void Add(ThrottlingInfo info)
		{
			if (info != null)
			{
				this.throttleInfo.Add(info);
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000AE74 File Offset: 0x00009074
		internal void Update(ThrottlingStatistics stats)
		{
			List<ThrottlingInfo> list = new List<ThrottlingInfo>(stats.throttleInfo);
			lock (this.lockObject)
			{
				if (stats != null && stats.throttleInfo != null)
				{
					foreach (ThrottlingInfo info in list)
					{
						this.Add(info);
					}
				}
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000AF04 File Offset: 0x00009104
		private TimeSpan GetTotalBackOffTime(SyncResourceMonitorType monitor, ResourceLoadState resourceLoadState)
		{
			TimeSpan timeSpan = TimeSpan.Zero;
			foreach (ThrottlingInfo throttlingInfo in this.throttleInfo)
			{
				timeSpan += throttlingInfo.Cache[monitor][resourceLoadState];
			}
			return timeSpan;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000AF70 File Offset: 0x00009170
		private TimeSpan GetTotalBackOffTime()
		{
			TimeSpan timeSpan = TimeSpan.Zero;
			foreach (ThrottlingInfo throttlingInfo in this.throttleInfo)
			{
				timeSpan += throttlingInfo.BackOffTime;
			}
			return timeSpan;
		}

		// Token: 0x04000138 RID: 312
		private readonly List<ThrottlingInfo> throttleInfo;

		// Token: 0x04000139 RID: 313
		private object lockObject = new object();
	}
}
