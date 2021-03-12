using System;
using System.Collections.Generic;
using Microsoft.Exchange.Transport.MessageDepot;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001C0 RID: 448
	internal class ReusableJob : Job
	{
		// Token: 0x06001497 RID: 5271 RVA: 0x00053000 File Offset: 0x00051200
		private ReusableJob(long id, ThrottlingContext context, QueuedRecipientsByAgeToken token, CatScheduler scheduler) : base(id, context, token, ReusableJob.GetStages(scheduler))
		{
			this.scheduler = scheduler;
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0005301A File Offset: 0x0005121A
		public static ReusableJob NewJob(CatScheduler scheduler, ThrottlingContext context, QueuedRecipientsByAgeToken token)
		{
			long nextJobId = Job.nextJobId;
			Job.nextJobId = nextJobId + 1L;
			return new ReusableJob(nextJobId, context, token, scheduler);
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00053032 File Offset: 0x00051232
		public override bool TryGetDeferToken(TransportMailItem mailItem, out AcquireToken deferToken)
		{
			deferToken = null;
			return false;
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x00053038 File Offset: 0x00051238
		public override void MarkDeferred(TransportMailItem mailItem)
		{
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x0005303A File Offset: 0x0005123A
		protected override bool IsRetired
		{
			get
			{
				return this.scheduler.Retired;
			}
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x00053047 File Offset: 0x00051247
		protected override void CompletedInternal(TransportMailItem mailItem)
		{
			this.scheduler.RunningJobCompleted(this, mailItem);
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x00053056 File Offset: 0x00051256
		protected override void PendingInternal()
		{
			this.scheduler.MoveRunningJobToPending(this);
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x00053064 File Offset: 0x00051264
		protected override void GoneAsyncInternal()
		{
			this.scheduler.CheckAndScheduleJobThread();
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x00053071 File Offset: 0x00051271
		protected override void RetireInternal(TransportMailItem mailItem)
		{
			this.scheduler.RunningJobRetired(this, mailItem);
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00053080 File Offset: 0x00051280
		private static IList<StageInfo> GetStages(CatScheduler scheduler)
		{
			if (scheduler != null)
			{
				return scheduler.Stages;
			}
			return null;
		}

		// Token: 0x04000A6A RID: 2666
		private readonly CatScheduler scheduler;
	}
}
