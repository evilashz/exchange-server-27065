using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Common;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001BA RID: 442
	internal class JobHealthMonitor
	{
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x0005223D File Offset: 0x0005043D
		public int JobAvailabilityPercentage
		{
			get
			{
				return this.jobAvailabilityPercentage;
			}
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00052245 File Offset: 0x00050445
		public JobHealthMonitor(int totalJobs, TimeSpan jobHealthThreshold, ExPerformanceCounter perfCounter)
		{
			this.totalJobs = totalJobs;
			this.executingJobs = new Dictionary<Job, ExDateTime>();
			this.pendingJobs = new Dictionary<Job, ExDateTime>();
			this.jobHealthThreshold = jobHealthThreshold;
			this.perfCounter = perfCounter;
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00052278 File Offset: 0x00050478
		public void JobBeganExecution(Job job)
		{
			lock (this.executingJobs)
			{
				TransportHelpers.AttemptAddToDictionary<Job, ExDateTime>(this.executingJobs, job, ExDateTime.UtcNow, null);
			}
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x000522C8 File Offset: 0x000504C8
		public void JobFinishedExecution(Job job)
		{
			lock (this.executingJobs)
			{
				this.executingJobs.Remove(job);
			}
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00052310 File Offset: 0x00050510
		public void JobMovedFromExecutionToPending(Job job)
		{
			lock (this.executingJobs)
			{
				ExDateTime valueToAdd = this.executingJobs[job];
				TransportHelpers.AttemptAddToDictionary<Job, ExDateTime>(this.pendingJobs, job, valueToAdd, null);
				this.executingJobs.Remove(job);
			}
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x00052374 File Offset: 0x00050574
		public void JobMovedFromPendingToExecution(Job job)
		{
			lock (this.executingJobs)
			{
				ExDateTime valueToAdd = this.pendingJobs[job];
				TransportHelpers.AttemptAddToDictionary<Job, ExDateTime>(this.executingJobs, job, valueToAdd, null);
				this.pendingJobs.Remove(job);
			}
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x000523D8 File Offset: 0x000505D8
		internal void UpdateJobUsagePerfCounter(object state)
		{
			ExDateTime t = ExDateTime.UtcNow - this.jobHealthThreshold;
			int num;
			lock (this.executingJobs)
			{
				num = this.totalJobs - this.executingJobs.Count;
				foreach (KeyValuePair<Job, ExDateTime> keyValuePair in this.executingJobs)
				{
					if (keyValuePair.Value >= t)
					{
						num++;
					}
				}
			}
			this.jobAvailabilityPercentage = (int)((double)num / (double)this.totalJobs * 100.0);
			if (this.perfCounter != null)
			{
				this.perfCounter.RawValue = (long)this.jobAvailabilityPercentage;
			}
		}

		// Token: 0x04000A52 RID: 2642
		private int totalJobs;

		// Token: 0x04000A53 RID: 2643
		private TimeSpan jobHealthThreshold;

		// Token: 0x04000A54 RID: 2644
		private int jobAvailabilityPercentage;

		// Token: 0x04000A55 RID: 2645
		private ExPerformanceCounter perfCounter;

		// Token: 0x04000A56 RID: 2646
		private Dictionary<Job, ExDateTime> executingJobs;

		// Token: 0x04000A57 RID: 2647
		private Dictionary<Job, ExDateTime> pendingJobs;
	}
}
