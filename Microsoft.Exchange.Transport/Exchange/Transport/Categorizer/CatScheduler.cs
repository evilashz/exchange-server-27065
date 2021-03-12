using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001B4 RID: 436
	internal class CatScheduler
	{
		// Token: 0x06001421 RID: 5153 RVA: 0x000512A8 File Offset: 0x0004F4A8
		internal CatScheduler(StageInfo[] stages, SubmitMessageQueue submitMessageQueue, IProcessingQuotaComponent processingQuotaComponent)
		{
			this.stages = new ReadOnlyCollection<StageInfo>(stages);
			this.submitMessageQueue = submitMessageQueue;
			ExPerformanceCounter perfCounter = (Components.QueueManager.PerfCountersTotal != null) ? Components.QueueManager.PerfCountersTotal.CategorizerJobAvailability : null;
			this.monitor = new JobHealthMonitor(Math.Min(CatScheduler.maxExecutingJobs, CatScheduler.maxJobThreads), Components.TransportAppConfig.Resolver.JobHealthTimeThreshold, perfCounter);
			this.jobs = new CatSchedulerJobList(this, this.monitor);
			this.jobThreadEntry = new WaitCallback(this.JobThreadEntry);
			this.throttlingEnabled = (Components.IsBridgehead && (Components.TransportAppConfig.ThrottlingConfig.CategorizerTenantThrottlingEnabled || Components.TransportAppConfig.ThrottlingConfig.CategorizerSenderThrottlingEnabled));
			if (this.throttlingEnabled)
			{
				int maxExecutingThreadsLimit = Math.Min(CatScheduler.maxExecutingJobs, CatScheduler.maxJobThreads) + Math.Abs(CatScheduler.maxExecutingJobs - CatScheduler.maxJobThreads) * Components.TransportAppConfig.ThrottlingConfig.CategorizerThrottlingAsyncThreadPercentage / 100;
				this.conditionManager = new SingleQueueWaitConditionManager(this.submitMessageQueue, NextHopSolutionKey.Submission, maxExecutingThreadsLimit, Components.TransportAppConfig.ThrottlingConfig.GetConfig(true), new CostFactory(), processingQuotaComponent, null, ExTraceGlobals.QueuingTracer);
				this.submitMessageQueue.SetConditionManager(this.conditionManager);
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x000513EE File Offset: 0x0004F5EE
		public static ExEventLog EventLogger
		{
			get
			{
				return CatScheduler.eventLogger;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x000513F5 File Offset: 0x0004F5F5
		public static int MaxExecutingJobs
		{
			get
			{
				return CatScheduler.maxExecutingJobs;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x000513FC File Offset: 0x0004F5FC
		public IList<StageInfo> Stages
		{
			get
			{
				return this.stages;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x00051404 File Offset: 0x0004F604
		public bool Retired
		{
			get
			{
				return this.retired;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x0005140E File Offset: 0x0004F60E
		public JobList JobList
		{
			get
			{
				return this.jobs;
			}
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x00051416 File Offset: 0x0004F616
		public void Retire()
		{
			ExTraceGlobals.SchedulerTracer.TraceDebug(0L, "Retire Categorizer Scheduler");
			this.retired = true;
			this.jobs.Retire();
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0005143D File Offset: 0x0004F63D
		public void Stop()
		{
			if (!this.retired)
			{
				this.Retire();
			}
			ExTraceGlobals.SchedulerTracer.TraceDebug(0L, "Stop Categorizer Scheduler");
			this.jobs.Stop();
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0005146C File Offset: 0x0004F66C
		public void CheckAndScheduleJobThread()
		{
			if (this.jobs.PendingJobCount > 0 || (!this.submitMessageQueue.Suspended && (this.submitMessageQueue.ActiveCount > 0 || (this.throttlingEnabled && this.submitMessageQueue.LockedCount > 0 && this.conditionManager.MapStateChanged))))
			{
				if (this.retired)
				{
					ExTraceGlobals.SchedulerTracer.TraceDebug(0L, "Cat Scheduler is retired - don't start any more threads");
					return;
				}
				ExTraceGlobals.SchedulerTracer.TraceDebug(0L, "More work to do - try and schedule a thread");
				this.ScheduleJobThread();
			}
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x000514F8 File Offset: 0x0004F6F8
		public void MonitorJobs()
		{
			this.monitor.UpdateJobUsagePerfCounter(null);
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x00051508 File Offset: 0x0004F708
		public void JobThreadEntry(object ignored)
		{
			ExTraceGlobals.SchedulerTracer.TraceDebug(0L, "Start JobThreadEntry to run a job");
			for (int i = 0; i < CatScheduler.maxJobsPerThread; i++)
			{
				if (this.retired)
				{
					ExTraceGlobals.SchedulerTracer.TraceDebug(0L, "Categorizer is retired - stop running more jobs");
					break;
				}
				Job nextJobToRun = this.jobs.GetNextJobToRun();
				if (nextJobToRun == null)
				{
					ExTraceGlobals.SchedulerTracer.TraceDebug(0L, "no more work to do or unable to run more jobs currently");
					break;
				}
				nextJobToRun.ExecutePendingTasks();
			}
			Interlocked.Decrement(ref this.executingJobThreads);
			this.CheckAndScheduleJobThread();
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x00051590 File Offset: 0x0004F790
		public void RunningJobCompleted(Job job, TransportMailItem mailItem)
		{
			ExTraceGlobals.SchedulerTracer.TraceDebug<Job>(0L, "Job({0}) completed", job);
			this.jobs.RemoveExecutingJob(job);
			this.MessageCompleted(mailItem, job.GetThrottlingContext(), job.GetQueuedRecipientsByAgeToken());
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x000515C4 File Offset: 0x0004F7C4
		public void RunningJobRetired(Job job, TransportMailItem mailItem)
		{
			ExTraceGlobals.SchedulerTracer.TraceDebug<Job>(0L, "Job({0}) retired", job);
			bool flag = this.jobs.RemoveExecutingJob(job);
			if (flag)
			{
				this.MessageCompleted(mailItem, job.GetThrottlingContext(), job.GetQueuedRecipientsByAgeToken());
			}
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x00051606 File Offset: 0x0004F806
		public void MoveRunningJobToPending(Job job)
		{
			this.jobs.MoveRunningJobToPending(job);
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x00051620 File Offset: 0x0004F820
		public Job CreateNewJob()
		{
			ExTraceGlobals.FaultInjectionTracer.TraceTest(2621844797U);
			ExTraceGlobals.FaultInjectionTracer.TraceTest(3674615101U);
			TransportMailItem transportMailItem = (TransportMailItem)this.submitMessageQueue.Dequeue();
			if (transportMailItem == null)
			{
				return null;
			}
			ThrottlingContext context2;
			Job job = CategorizerJobsUtil.SetupNewJob(transportMailItem, this.stages, (QueuedRecipientsByAgeToken ageToken, ThrottlingContext context, IList<StageInfo> catStages) => ReusableJob.NewJob(this, context, ageToken), out context2);
			if (job == null)
			{
				this.NotifyConditionManagerMessageCompleted(context2);
			}
			return job;
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x00051688 File Offset: 0x0004F888
		internal CategorizerItem GetScheduledCategorizerItemById(long mailItemId)
		{
			Job[] array = this.jobs.ToArray();
			foreach (Job job in array)
			{
				CategorizerItem categorizerItemById = job.GetCategorizerItemById(mailItemId);
				if (categorizerItemById != null)
				{
					return categorizerItemById;
				}
			}
			return null;
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x000516D0 File Offset: 0x0004F8D0
		internal void VisitCategorizerItems(Func<CategorizerItem, bool> visitor)
		{
			Job[] array = this.jobs.ToArray();
			foreach (Job job in array)
			{
				job.VisitCategorizerItems(visitor);
			}
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x00051704 File Offset: 0x0004F904
		internal int GetMailItemCount()
		{
			Job[] array = this.jobs.ToArray();
			int num = 0;
			foreach (Job job in array)
			{
				num += job.GetMailItemCount();
			}
			return num;
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x00051742 File Offset: 0x0004F942
		internal void TimedUpdate()
		{
			if (!this.throttlingEnabled)
			{
				return;
			}
			this.conditionManager.TimedUpdate();
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x00051758 File Offset: 0x0004F958
		internal XElement GetDiagnosticInfo(bool verbose, bool conditionalQueuing)
		{
			XElement xelement = new XElement("Categorizer");
			xelement.Add(new XElement("ExecutingJobs", this.jobs.ExecutingJobCount));
			xelement.Add(new XElement("PendingJobs", this.jobs.PendingJobCount));
			if (verbose)
			{
				Job[] array = this.JobList.ToArray();
				XElement xelement2 = new XElement("jobs");
				foreach (Job job in array)
				{
					XElement xelement3 = new XElement("job");
					xelement3.Add(new XElement("Id", job.Id));
					xelement3.Add(new XElement("MailItemCount", job.GetMailItemCount()));
					xelement2.Add(xelement3);
				}
				xelement.Add(xelement2);
			}
			if (conditionalQueuing && this.conditionManager != null)
			{
				xelement.Add(this.conditionManager.GetDiagnosticInfo(verbose));
			}
			xelement.Add(this.submitMessageQueue.GetDiagnosticInfo(verbose, conditionalQueuing));
			return xelement;
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x00051890 File Offset: 0x0004FA90
		private void ScheduleJobThread()
		{
			if (this.jobs.ExecutingJobCount >= CatScheduler.MaxExecutingJobs)
			{
				ExTraceGlobals.SchedulerTracer.TraceDebug(0L, "ScheduleJobThread: max # executing jobs limit reached");
				return;
			}
			int num = Interlocked.Increment(ref this.executingJobThreads);
			if (num > CatScheduler.maxJobThreads)
			{
				Interlocked.Decrement(ref this.executingJobThreads);
				ExTraceGlobals.SchedulerTracer.TraceDebug(0L, "ScheduleJobThread: max executing thread count reached");
				return;
			}
			if (this.retired)
			{
				Interlocked.Decrement(ref this.executingJobThreads);
				ExTraceGlobals.SchedulerTracer.TraceDebug(0L, "ScheduleJobThread: Categorizer is retiring");
				return;
			}
			ExTraceGlobals.SchedulerTracer.TraceDebug(0L, "ScheduleJobThread: queue JobThreadEntry to run a job");
			ThreadPool.QueueUserWorkItem(this.jobThreadEntry);
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0005193A File Offset: 0x0004FB3A
		private void MessageCompleted(TransportMailItem mailItem, ThrottlingContext context, QueuedRecipientsByAgeToken token)
		{
			Components.QueueManager.GetQueuedRecipientsByAge().TrackExitingCategorizer(token);
			this.NotifyConditionManagerMessageCompleted(context);
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x00051953 File Offset: 0x0004FB53
		private void NotifyConditionManagerMessageCompleted(ThrottlingContext context)
		{
			if (!this.throttlingEnabled)
			{
				return;
			}
			if (context == null)
			{
				return;
			}
			context.AddBreadcrumb(CategorizerBreadcrumb.NotifyFinished);
			this.conditionManager.MessageCompleted(context.CreationTime, context.Cost.Condition);
		}

		// Token: 0x04000A35 RID: 2613
		private const int MaxJobsPerThread = 1;

		// Token: 0x04000A36 RID: 2614
		private static readonly int maxExecutingJobs = Components.TransportAppConfig.Resolver.MaxExecutingJobs;

		// Token: 0x04000A37 RID: 2615
		private static readonly int maxJobThreads = Components.TransportAppConfig.Resolver.MaxJobThreads;

		// Token: 0x04000A38 RID: 2616
		private static readonly int maxJobsPerThread = Components.TransportAppConfig.Resolver.MaxJobsPerThread;

		// Token: 0x04000A39 RID: 2617
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.SchedulerTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x04000A3A RID: 2618
		private readonly IList<StageInfo> stages;

		// Token: 0x04000A3B RID: 2619
		private readonly JobHealthMonitor monitor;

		// Token: 0x04000A3C RID: 2620
		private readonly bool throttlingEnabled;

		// Token: 0x04000A3D RID: 2621
		private CatSchedulerJobList jobs;

		// Token: 0x04000A3E RID: 2622
		private int executingJobThreads;

		// Token: 0x04000A3F RID: 2623
		private SubmitMessageQueue submitMessageQueue;

		// Token: 0x04000A40 RID: 2624
		private WaitCallback jobThreadEntry;

		// Token: 0x04000A41 RID: 2625
		private volatile bool retired;

		// Token: 0x04000A42 RID: 2626
		private SingleQueueWaitConditionManager conditionManager;
	}
}
