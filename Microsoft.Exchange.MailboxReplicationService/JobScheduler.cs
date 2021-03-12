using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000029 RID: 41
	internal class JobScheduler : SystemWorkloadBase
	{
		// Token: 0x060001BB RID: 443 RVA: 0x00009784 File Offset: 0x00007984
		public static void ScheduleJob(IJob job)
		{
			CommonUtils.CheckForServiceStopping();
			job.ResetJob();
			lock (JobScheduler.staticLock)
			{
				JobScheduler.RunnableJobs.Add(job);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001BC RID: 444 RVA: 0x000097D4 File Offset: 0x000079D4
		private static ICollection<IJob> RunnableJobs
		{
			get
			{
				return JobScheduler.runnableJobsInstance.Value;
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000097E0 File Offset: 0x000079E0
		public JobScheduler(WorkloadType workloadType)
		{
			this.JobStateChanged = null;
			this.wakeupJobsTimer = new Timer(new TimerCallback(this.WakeupJobs), null, 1000, 1000);
			this.workloadType = workloadType;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00009844 File Offset: 0x00007A44
		public override WorkloadType WorkloadType
		{
			get
			{
				return this.workloadType;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000984C File Offset: 0x00007A4C
		public override string Id
		{
			get
			{
				return this.WorkloadType.ToString();
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00009860 File Offset: 0x00007A60
		public override int TaskCount
		{
			get
			{
				int result;
				lock (this.instanceLock)
				{
					lock (JobScheduler.staticLock)
					{
						result = this.waitingJobs.Count + JobScheduler.RunnableJobs.Count + this.runningJobs.Count;
					}
				}
				return result;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000098E8 File Offset: 0x00007AE8
		public override int BlockedTaskCount
		{
			get
			{
				int result;
				lock (this.instanceLock)
				{
					lock (JobScheduler.staticLock)
					{
						result = JobScheduler.RunnableJobs.Count + this.waitingJobs.Count;
					}
				}
				return result;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060001C2 RID: 450 RVA: 0x00009964 File Offset: 0x00007B64
		// (remove) Token: 0x060001C3 RID: 451 RVA: 0x0000999C File Offset: 0x00007B9C
		public event EventHandler<JobEventArgs> JobStateChanged;

		// Token: 0x060001C4 RID: 452 RVA: 0x000099D1 File Offset: 0x00007BD1
		public void Start()
		{
			SystemWorkloadManager.RegisterWorkload(this);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000099DC File Offset: 0x00007BDC
		public void Stop()
		{
			List<IJob> list;
			lock (this.instanceLock)
			{
				lock (JobScheduler.staticLock)
				{
					list = new List<IJob>(this.waitingJobs.Count + JobScheduler.RunnableJobs.Count);
					list.AddRange(this.waitingJobs);
					list.AddRange(JobScheduler.RunnableJobs);
				}
			}
			foreach (IJob job in list)
			{
				job.WaitForJobToBeDone();
			}
			this.wakeupJobsTimer.Dispose();
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00009AC0 File Offset: 0x00007CC0
		protected override SystemTaskBase GetTask(ResourceReservationContext context)
		{
			MrsSystemTask mrsSystemTask = null;
			SystemTaskBase result;
			lock (this.instanceLock)
			{
				lock (JobScheduler.staticLock)
				{
					if (JobScheduler.RunnableJobs.Count > 0)
					{
						List<IJob> list = new List<IJob>();
						List<IJob> list2 = new List<IJob>();
						foreach (IJob job in JobScheduler.RunnableJobs)
						{
							mrsSystemTask = job.GetTask(this, context);
							if (mrsSystemTask != null)
							{
								break;
							}
							switch (job.State)
							{
							case JobState.Waiting:
								list2.Add(job);
								break;
							case JobState.Finished:
								list.Add(job);
								break;
							}
						}
						if (mrsSystemTask != null)
						{
							JobScheduler.RunnableJobs.Remove(mrsSystemTask.Job);
							this.runningJobs.Add(mrsSystemTask.Job);
						}
						foreach (IJob job2 in list)
						{
							JobScheduler.RunnableJobs.Remove(job2);
							job2.RevertToPreviousUnthrottledState();
							this.OnJobStateChanged(job2, job2.State);
						}
						foreach (IJob item in list2)
						{
							JobScheduler.RunnableJobs.Remove(item);
							this.waitingJobs.Add(item);
						}
					}
					result = mrsSystemTask;
				}
			}
			return result;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00009CCC File Offset: 0x00007ECC
		protected override void CompleteTask(SystemTaskBase task)
		{
			MrsSystemTask mrsSystemTask = task as MrsSystemTask;
			IJob job = mrsSystemTask.Job;
			job.ProcessTaskExecutionResult(mrsSystemTask);
			switch (job.State)
			{
			case JobState.Runnable:
				lock (this.instanceLock)
				{
					lock (JobScheduler.staticLock)
					{
						this.runningJobs.Remove(job);
						JobScheduler.RunnableJobs.Add(job);
					}
				}
				return;
			case JobState.Failed:
				lock (this.instanceLock)
				{
					job.RevertToPreviousUnthrottledState();
					this.runningJobs.Remove(job);
				}
				this.OnJobStateChanged(job, job.State);
				return;
			default:
				return;
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00009DC8 File Offset: 0x00007FC8
		protected virtual void OnJobStateChanged(IJob job, JobState state)
		{
			if (this.JobStateChanged != null)
			{
				JobEventArgs e = new JobEventArgs(job, state);
				this.JobStateChanged(this, e);
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00009DF4 File Offset: 0x00007FF4
		private void WakeupJobs(object state)
		{
			if (this.waitingJobs.Count > 0)
			{
				lock (this.instanceLock)
				{
					if (this.waitingJobs.Count > 0)
					{
						for (int i = this.waitingJobs.Count - 1; i >= 0; i--)
						{
							IJob job = this.waitingJobs[i];
							lock (JobScheduler.staticLock)
							{
								if (job.ShouldWakeup)
								{
									this.waitingJobs.RemoveAt(i);
									job.ResetJob();
									JobScheduler.RunnableJobs.Add(job);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x040000BD RID: 189
		private const int WakeupJobsPeriodMilliseconds = 1000;

		// Token: 0x040000BE RID: 190
		private static readonly Lazy<ICollection<IJob>> runnableJobsInstance = new Lazy<ICollection<IJob>>(delegate()
		{
			if (MoveJob.CacheJobQueues)
			{
				return new SortedSet<IJob>(JobScheduler.JobsComparer.Instance);
			}
			return new LinkedList<IJob>();
		});

		// Token: 0x040000BF RID: 191
		private static readonly object staticLock = new object();

		// Token: 0x040000C0 RID: 192
		protected WorkloadType workloadType;

		// Token: 0x040000C1 RID: 193
		private List<IJob> waitingJobs = new List<IJob>();

		// Token: 0x040000C2 RID: 194
		private HashSet<IJob> runningJobs = new HashSet<IJob>();

		// Token: 0x040000C3 RID: 195
		private Timer wakeupJobsTimer;

		// Token: 0x040000C4 RID: 196
		private object instanceLock = new object();

		// Token: 0x0200002A RID: 42
		private class JobsComparer : IComparer<IJob>
		{
			// Token: 0x060001CC RID: 460 RVA: 0x00009F10 File Offset: 0x00008110
			private JobsComparer()
			{
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x060001CD RID: 461 RVA: 0x00009F18 File Offset: 0x00008118
			public static JobScheduler.JobsComparer Instance
			{
				get
				{
					return JobScheduler.JobsComparer.defaultInstance;
				}
			}

			// Token: 0x060001CE RID: 462 RVA: 0x00009F1F File Offset: 0x0000811F
			public int Compare(IJob x, IJob y)
			{
				if (object.ReferenceEquals(x, y))
				{
					return 0;
				}
				return x.JobSortKey.CompareTo(y.JobSortKey);
			}

			// Token: 0x040000C7 RID: 199
			private static readonly JobScheduler.JobsComparer defaultInstance = new JobScheduler.JobsComparer();
		}
	}
}
