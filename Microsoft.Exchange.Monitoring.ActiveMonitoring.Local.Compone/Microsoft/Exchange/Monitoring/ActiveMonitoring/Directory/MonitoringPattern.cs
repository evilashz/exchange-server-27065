using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Directory
{
	// Token: 0x02000147 RID: 327
	public class MonitoringPattern
	{
		// Token: 0x06000984 RID: 2436 RVA: 0x0003A558 File Offset: 0x00038758
		public MonitoringPattern(int recurrance = 120, int monitoringInterval = 600, int monitoringThreshold = 4, int timeout = 120)
		{
			this.recurrenceInSeconds = recurrance;
			this.monitoringIntervalInSeconds = monitoringInterval;
			this.monitoringThreshold = monitoringThreshold;
			if (timeout < recurrance)
			{
				this.timeoutInSeconds = timeout;
				return;
			}
			this.timeoutInSeconds = recurrance;
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x0003A5B7 File Offset: 0x000387B7
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x0003A5BF File Offset: 0x000387BF
		public int RecurrenceInSeconds
		{
			get
			{
				return this.recurrenceInSeconds;
			}
			set
			{
				this.recurrenceInSeconds = value;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0003A5C8 File Offset: 0x000387C8
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x0003A5D0 File Offset: 0x000387D0
		public int TimeoutInSeconds
		{
			get
			{
				return this.timeoutInSeconds;
			}
			set
			{
				this.timeoutInSeconds = value;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0003A5D9 File Offset: 0x000387D9
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x0003A5E1 File Offset: 0x000387E1
		public int MonitoringIntervalInSeconds
		{
			get
			{
				return this.monitoringIntervalInSeconds;
			}
			set
			{
				this.monitoringIntervalInSeconds = value;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x0003A5EA File Offset: 0x000387EA
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x0003A5F2 File Offset: 0x000387F2
		public int MonitoringThreshold
		{
			get
			{
				return this.monitoringThreshold;
			}
			set
			{
				this.monitoringThreshold = value;
			}
		}

		// Token: 0x040006E7 RID: 1767
		private int recurrenceInSeconds = 120;

		// Token: 0x040006E8 RID: 1768
		private int timeoutInSeconds = 120;

		// Token: 0x040006E9 RID: 1769
		private int monitoringIntervalInSeconds = 300;

		// Token: 0x040006EA RID: 1770
		private int monitoringThreshold = 2;
	}
}
