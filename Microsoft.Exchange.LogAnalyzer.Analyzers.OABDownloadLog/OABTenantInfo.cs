using System;
using Microsoft.Exchange.LogAnalyzer.Extensions.OABDownloadLog;

namespace Microsoft.Exchange.LogAnalyzer.Analyzers.OABDownloadLog
{
	// Token: 0x02000005 RID: 5
	public sealed class OABTenantInfo
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000025C0 File Offset: 0x000007C0
		public OABTenantInfo(string organization, OABDownloadLogLine logline, TimeSpan monitoringInterval)
		{
			this.organization = organization;
			this.logLine = logline;
			this.monitoringInterval = monitoringInterval;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000025DD File Offset: 0x000007DD
		public string Organization
		{
			get
			{
				return this.organization;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000025E5 File Offset: 0x000007E5
		public OABDownloadLogLine LogLine
		{
			get
			{
				return this.logLine;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000025ED File Offset: 0x000007ED
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000025F5 File Offset: 0x000007F5
		public DateTime LastAlertTime { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000025FE File Offset: 0x000007FE
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002606 File Offset: 0x00000806
		public int NoOfRequests { get; set; }

		// Token: 0x0600000F RID: 15 RVA: 0x0000260F File Offset: 0x0000080F
		public bool IsAlert(DateTime currentTime, int threshold, TimeSpan recurrenceInterval)
		{
			return currentTime - this.LastAlertTime > this.monitoringInterval && this.NoOfRequests > threshold && currentTime - this.logLine.Timestamp > recurrenceInterval;
		}

		// Token: 0x0400000F RID: 15
		private readonly string organization;

		// Token: 0x04000010 RID: 16
		private readonly TimeSpan monitoringInterval;

		// Token: 0x04000011 RID: 17
		private readonly OABDownloadLogLine logLine;
	}
}
