using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Worker.Health;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200021E RID: 542
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SyncStorageProviderConnectionStatistics
	{
		// Token: 0x0600134C RID: 4940 RVA: 0x00041799 File Offset: 0x0003F999
		internal SyncStorageProviderConnectionStatistics()
		{
			this.totalSuccessfulRoundtrips = 0;
			this.totalTimeForAllSuccesfulRoundtrips = TimeSpan.Zero;
			this.totalUnsuccessfulRoundtrips = 0;
			this.totalTimeForAllUnsuccesfulRoundtrips = TimeSpan.Zero;
			this.throttlingStatistics = new ThrottlingStatistics();
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x000417D0 File Offset: 0x0003F9D0
		public int TotalSuccessfulRoundtrips
		{
			get
			{
				return this.totalSuccessfulRoundtrips;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x000417D8 File Offset: 0x0003F9D8
		internal TimeSpan AverageSuccessfulRoundtripTime
		{
			get
			{
				double value = (this.totalSuccessfulRoundtrips != 0) ? (this.totalTimeForAllSuccesfulRoundtrips.TotalMilliseconds / (double)this.totalSuccessfulRoundtrips) : 0.0;
				return TimeSpan.FromMilliseconds(value);
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x00041812 File Offset: 0x0003FA12
		public int TotalUnsuccessfulRoundtrips
		{
			get
			{
				return this.totalUnsuccessfulRoundtrips;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x0004181C File Offset: 0x0003FA1C
		internal TimeSpan AverageUnsuccessfulRoundtripTime
		{
			get
			{
				double value = (this.totalUnsuccessfulRoundtrips != 0) ? (this.totalTimeForAllUnsuccesfulRoundtrips.TotalMilliseconds / (double)this.totalUnsuccessfulRoundtrips) : 0.0;
				return TimeSpan.FromMilliseconds(value);
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x00041858 File Offset: 0x0003FA58
		internal TimeSpan AverageBackoffTime
		{
			get
			{
				int num = this.totalSuccessfulRoundtrips + this.totalUnsuccessfulRoundtrips;
				double value = (num != 0) ? (this.throttlingStatistics.TotalBackoffTime.TotalMilliseconds / (double)num) : 0.0;
				return TimeSpan.FromMilliseconds(value);
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x0004189E File Offset: 0x0003FA9E
		internal ThrottlingStatistics ThrottlingStatistics
		{
			get
			{
				return this.throttlingStatistics;
			}
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x000418A8 File Offset: 0x0003FAA8
		internal void OnRoundtripComplete(object sender, RoundtripCompleteEventArgs e)
		{
			SyncUtilities.ThrowIfArgumentNull("RoundtripCompleteEventArgs", e != null);
			if (e.RoundtripSuccessful)
			{
				this.totalSuccessfulRoundtrips++;
				this.totalTimeForAllSuccesfulRoundtrips += e.RoundtripTime;
			}
			else
			{
				this.totalUnsuccessfulRoundtrips++;
				this.totalTimeForAllUnsuccesfulRoundtrips += e.RoundtripTime;
			}
			this.throttlingStatistics.Add(e.ThrottlingInfo);
		}

		// Token: 0x04000A33 RID: 2611
		private int totalSuccessfulRoundtrips;

		// Token: 0x04000A34 RID: 2612
		private TimeSpan totalTimeForAllSuccesfulRoundtrips;

		// Token: 0x04000A35 RID: 2613
		private int totalUnsuccessfulRoundtrips;

		// Token: 0x04000A36 RID: 2614
		private TimeSpan totalTimeForAllUnsuccesfulRoundtrips;

		// Token: 0x04000A37 RID: 2615
		private ThrottlingStatistics throttlingStatistics;
	}
}
