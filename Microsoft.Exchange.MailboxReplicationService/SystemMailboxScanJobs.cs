using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000062 RID: 98
	internal abstract class SystemMailboxScanJobs : SystemMailboxJobs
	{
		// Token: 0x060004EE RID: 1262 RVA: 0x0001DB4C File Offset: 0x0001BD4C
		protected SystemMailboxScanJobs(Guid mdbGuid) : base(mdbGuid)
		{
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0001DB55 File Offset: 0x0001BD55
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x0001DB5D File Offset: 0x0001BD5D
		public string ScanFailure { get; private set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0001DB66 File Offset: 0x0001BD66
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x0001DB6E File Offset: 0x0001BD6E
		public List<JobPickupRec> ScanResults { get; private set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0001DB77 File Offset: 0x0001BD77
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x0001DB7F File Offset: 0x0001BD7F
		public DateTime RecommendedNextScan { get; private set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0001DB88 File Offset: 0x0001BD88
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x0001DB90 File Offset: 0x0001BD90
		public int QueuedJobsCount { get; private set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0001DB99 File Offset: 0x0001BD99
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x0001DBA1 File Offset: 0x0001BDA1
		public int InProgressJobsCount { get; private set; }

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001DBAC File Offset: 0x0001BDAC
		public void PickupJobs()
		{
			this.ScanFailure = null;
			this.QueuedJobsCount = 0;
			this.InProgressJobsCount = 0;
			this.RecommendedNextScan = DateTime.MaxValue;
			this.ScanResults = new List<JobPickupRec>();
			string scanFailure;
			base.PickupJobs(out scanFailure);
			this.ScanFailure = scanFailure;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001DBF4 File Offset: 0x0001BDF4
		protected override void PerformPickupAccounting(RequestStatus status, JobPickupRec jobPickupRec)
		{
			switch (status)
			{
			case RequestStatus.Queued:
				if (jobPickupRec.PickupResult != JobPickupResult.CompletedJobCleanedUp && jobPickupRec.PickupResult != JobPickupResult.CompletedJobSkipped && jobPickupRec.PickupResult != JobPickupResult.InvalidJob)
				{
					this.QueuedJobsCount++;
					return;
				}
				break;
			case RequestStatus.InProgress:
				if (jobPickupRec.PickupResult == JobPickupResult.JobPickedUp || jobPickupRec.PickupResult == JobPickupResult.JobAlreadyActive)
				{
					this.InProgressJobsCount++;
				}
				if (jobPickupRec.PickupResult == JobPickupResult.JobIsPostponed)
				{
					this.QueuedJobsCount++;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001DC7C File Offset: 0x0001BE7C
		protected override void ProcessPickupResults(JobPickupRec jobPickupRec)
		{
			this.ScanResults.Add(jobPickupRec);
			if (jobPickupRec.NextRecommendedPickup < this.RecommendedNextScan)
			{
				this.RecommendedNextScan = jobPickupRec.NextRecommendedPickup;
			}
		}
	}
}
