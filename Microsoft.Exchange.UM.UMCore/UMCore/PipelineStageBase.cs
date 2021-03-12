using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002A9 RID: 681
	internal abstract class PipelineStageBase : DisposableBase
	{
		// Token: 0x060014A0 RID: 5280 RVA: 0x0005951D File Offset: 0x0005771D
		internal PipelineStageBase()
		{
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x00059525 File Offset: 0x00057725
		// (set) Token: 0x060014A2 RID: 5282 RVA: 0x0005952D File Offset: 0x0005772D
		internal bool MarkedForLastChanceHandling { get; set; }

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x00059536 File Offset: 0x00057736
		internal PipelineWorkItem WorkItem
		{
			get
			{
				return this.workItem;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x0005953E File Offset: 0x0005773E
		internal StageCompletionCallback StageCompletionCallback
		{
			get
			{
				return this.callback;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060014A5 RID: 5285
		internal abstract PipelineDispatcher.PipelineResourceType ResourceType { get; }

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060014A6 RID: 5286
		internal abstract TimeSpan ExpectedRunTime { get; }

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x00059546 File Offset: 0x00057746
		internal virtual StageRetryDetails RetrySchedule
		{
			get
			{
				if (this.retrySchedule == null)
				{
					this.retrySchedule = this.InternalGetRetrySchedule();
				}
				return this.retrySchedule;
			}
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x00059562 File Offset: 0x00057762
		internal virtual bool ShouldRunStage(PipelineWorkItem workItem)
		{
			return true;
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00059565 File Offset: 0x00057765
		internal virtual void Initialize(PipelineWorkItem workItem)
		{
			this.workItem = workItem;
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x0005956E File Offset: 0x0005776E
		internal void DispatchWorkAsync(StageCompletionCallback completionCallback)
		{
			this.callback = completionCallback;
			this.RetrySchedule.UpgdateStageRunTimestamp();
			this.InternalDispatchWorkAsync();
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00059588 File Offset: 0x00057788
		protected virtual void ReportFailure()
		{
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0005958A File Offset: 0x0005778A
		protected virtual StageRetryDetails InternalGetRetrySchedule()
		{
			return new StageRetryDetails(StageRetryDetails.FinalAction.DropMessage, TimeSpan.FromMinutes(10.0), TimeSpan.FromDays(1.0));
		}

		// Token: 0x060014AD RID: 5293
		protected abstract void InternalDispatchWorkAsync();

		// Token: 0x060014AE RID: 5294 RVA: 0x000595AE File Offset: 0x000577AE
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x04000CAF RID: 3247
		private PipelineWorkItem workItem;

		// Token: 0x04000CB0 RID: 3248
		private StageCompletionCallback callback;

		// Token: 0x04000CB1 RID: 3249
		private StageRetryDetails retrySchedule;
	}
}
