using System;
using System.Threading;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200004B RID: 75
	internal class PeriodicJobExecuter
	{
		// Token: 0x0600041C RID: 1052 RVA: 0x00019303 File Offset: 0x00017503
		internal PeriodicJobExecuter(string jobName, PeriodicJobExecuter.JobPollerCallback callback, double variationInPeriod)
		{
			this.jobPollerCallback = callback;
			this.jobName = jobName;
			this.variation = variationInPeriod;
			this.jobDoneEvent = new ManualResetEvent(false);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00019340 File Offset: 0x00017540
		internal void Start()
		{
			this.ScheduleWorkItem();
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00019348 File Offset: 0x00017548
		private bool HandleFailure(Exception failure)
		{
			if (failure == null)
			{
				return true;
			}
			if (failure is ServiceIsStoppingPermanentException)
			{
				return false;
			}
			if (CommonUtils.ClassifyException(failure).Length > 0)
			{
				return true;
			}
			MRSService.LogEvent(MRSEventLogConstants.Tuple_PeriodicTaskStoppingExecution, new object[]
			{
				this.jobName,
				CommonUtils.FullExceptionMessage(failure)
			});
			return false;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001939B File Offset: 0x0001759B
		public void ScheduleWorkItem()
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.DoJobAndScheduleNextJob));
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x000193B0 File Offset: 0x000175B0
		public void WaitForJobToBeDone()
		{
			if (!this.jobDoneEvent.WaitOne(TimeSpan.FromSeconds(this.period.TotalSeconds * 3.0), true))
			{
				MrsTracer.Service.Error("{0} job did not finish within the alloted time period of 3 x {1}.", new object[]
				{
					this.jobName,
					this.period
				});
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001943C File Offset: 0x0001763C
		public void DoJobAndScheduleNextJob(object o)
		{
			Exception failure = null;
			CommonUtils.CatchKnownExceptions(delegate
			{
				CommonUtils.CheckForServiceStopping();
				this.period = this.jobPollerCallback();
			}, delegate(Exception e)
			{
				failure = e;
			});
			if (!this.HandleFailure(failure))
			{
				this.jobDoneEvent.Set();
				return;
			}
			int num = (int)CommonUtils.Randomize(this.period.TotalSeconds, this.variation);
			Thread.Sleep(TimeSpan.FromSeconds((double)num));
			this.ScheduleWorkItem();
		}

		// Token: 0x040001B0 RID: 432
		private readonly string jobName;

		// Token: 0x040001B1 RID: 433
		private readonly double variation;

		// Token: 0x040001B2 RID: 434
		private TimeSpan period = TimeSpan.FromMinutes(1.0);

		// Token: 0x040001B3 RID: 435
		private PeriodicJobExecuter.JobPollerCallback jobPollerCallback;

		// Token: 0x040001B4 RID: 436
		private ManualResetEvent jobDoneEvent;

		// Token: 0x0200004C RID: 76
		// (Invoke) Token: 0x06000424 RID: 1060
		internal delegate TimeSpan JobPollerCallback();
	}
}
