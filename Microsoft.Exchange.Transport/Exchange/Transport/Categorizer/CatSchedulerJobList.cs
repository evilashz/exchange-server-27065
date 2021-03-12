using System;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001B6 RID: 438
	internal class CatSchedulerJobList : JobList
	{
		// Token: 0x06001444 RID: 5188 RVA: 0x00051CD0 File Offset: 0x0004FED0
		public CatSchedulerJobList(CatScheduler scheduler, JobHealthMonitor monitor) : base(monitor)
		{
			this.scheduler = scheduler;
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x00051CF0 File Offset: 0x0004FEF0
		public Job GetNextJobToRun()
		{
			lock (base.SyncRoot)
			{
				if (this.executingJobs.Count >= CatScheduler.MaxExecutingJobs)
				{
					ExTraceGlobals.SchedulerTracer.TraceDebug(0L, "GetNextJobToRun: max executing jobs count reached");
					return null;
				}
				if (this.retired)
				{
					ExTraceGlobals.SchedulerTracer.TraceDebug(0L, "Scheduler is retired -  don't add new jobs");
					return null;
				}
				if (this.pendingJobs.Count > 0)
				{
					Job job = this.pendingJobs.Dequeue();
					this.executingJobs.Add(job);
					this.monitor.JobMovedFromPendingToExecution(job);
					ExTraceGlobals.SchedulerTracer.TraceDebug<Job>(0L, "Found pending job {0}", job);
					return job;
				}
			}
			return base.RegisterNewJob(() => this.scheduler.CreateNewJob());
		}

		// Token: 0x04000A49 RID: 2633
		private CatScheduler scheduler;
	}
}
