using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001B5 RID: 437
	internal class JobList
	{
		// Token: 0x0600143A RID: 5178 RVA: 0x000519EA File Offset: 0x0004FBEA
		public JobList(JobHealthMonitor monitor)
		{
			this.executingJobs = new List<Job>();
			this.pendingJobs = new Queue<Job>();
			this.monitor = monitor;
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x00051A1A File Offset: 0x0004FC1A
		public int ExecutingJobCount
		{
			get
			{
				return this.executingJobs.Count;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x00051A27 File Offset: 0x0004FC27
		public int PendingJobCount
		{
			get
			{
				return this.pendingJobs.Count;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x00051A34 File Offset: 0x0004FC34
		protected object SyncRoot
		{
			get
			{
				return this.syncRoot;
			}
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x00051A3C File Offset: 0x0004FC3C
		public void MoveRunningJobToPending(Job job)
		{
			ExTraceGlobals.SchedulerTracer.TraceDebug<Job>(0L, "Move running Job({0}) to pending", job);
			lock (this.SyncRoot)
			{
				this.executingJobs.Remove(job);
				this.pendingJobs.Enqueue(job);
				this.monitor.JobMovedFromExecutionToPending(job);
			}
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x00051AB0 File Offset: 0x0004FCB0
		public bool RemoveExecutingJob(Job job)
		{
			bool result;
			lock (this.SyncRoot)
			{
				bool flag2 = this.executingJobs.Remove(job);
				this.monitor.JobFinishedExecution(job);
				result = flag2;
			}
			return result;
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x00051B08 File Offset: 0x0004FD08
		public void Retire()
		{
			this.retired = true;
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x00051B14 File Offset: 0x0004FD14
		public Job[] ToArray()
		{
			Job[] result;
			lock (this.SyncRoot)
			{
				Job[] array = new Job[this.executingJobs.Count + this.pendingJobs.Count];
				this.executingJobs.CopyTo(array);
				this.pendingJobs.CopyTo(array, this.executingJobs.Count);
				result = array;
			}
			return result;
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x00051B94 File Offset: 0x0004FD94
		public void Stop()
		{
			this.Retire();
			while (this.ExecutingJobCount > 0)
			{
				Thread.Sleep(100);
			}
			using (Queue<Job>.Enumerator enumerator = this.pendingJobs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Job job = enumerator.Current;
					job.Retire();
				}
				goto IL_54;
			}
			IL_4D:
			Thread.Sleep(100);
			IL_54:
			if (this.pendingJobCreation <= 0)
			{
				return;
			}
			goto IL_4D;
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x00051C10 File Offset: 0x0004FE10
		protected Job RegisterNewJob(Func<Job> newJobProviderFunc)
		{
			Job result;
			try
			{
				Interlocked.Increment(ref this.pendingJobCreation);
				if (this.retired)
				{
					result = null;
				}
				else
				{
					Job job = newJobProviderFunc();
					if (job == null)
					{
						result = null;
					}
					else
					{
						lock (this.SyncRoot)
						{
							if (this.retired)
							{
								ExTraceGlobals.SchedulerTracer.TraceDebug(0L, "Scheduler is retired - Dispose new Job");
								job.Retire();
								return null;
							}
							this.executingJobs.Add(job);
							this.monitor.JobBeganExecution(job);
						}
						result = job;
					}
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.pendingJobCreation);
			}
			return result;
		}

		// Token: 0x04000A43 RID: 2627
		protected List<Job> executingJobs;

		// Token: 0x04000A44 RID: 2628
		protected Queue<Job> pendingJobs;

		// Token: 0x04000A45 RID: 2629
		protected volatile bool retired;

		// Token: 0x04000A46 RID: 2630
		protected volatile int pendingJobCreation;

		// Token: 0x04000A47 RID: 2631
		private object syncRoot = new object();

		// Token: 0x04000A48 RID: 2632
		protected JobHealthMonitor monitor;
	}
}
