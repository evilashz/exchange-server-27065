using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002BB RID: 699
	internal class CreateProtectedMessageStage : SynchronousPipelineStageBase, IUMNetworkResource
	{
		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x0005AD10 File Offset: 0x00058F10
		internal override PipelineDispatcher.PipelineResourceType ResourceType
		{
			get
			{
				return PipelineDispatcher.PipelineResourceType.NetworkBound;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x0005AD13 File Offset: 0x00058F13
		public string NetworkResourceId
		{
			get
			{
				return base.WorkItem.Message.GetMailboxServerId();
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x0005AD25 File Offset: 0x00058F25
		internal override TimeSpan ExpectedRunTime
		{
			get
			{
				return TimeSpan.FromMinutes(5.0);
			}
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0005AD38 File Offset: 0x00058F38
		protected override void ReportFailure()
		{
			IUMCreateMessage message = base.WorkItem.Message;
			ExAssert.RetailAssert(message != null, "CreateProtectedMessageStage must operate on PipelineContext which implements IUMCreateMessage");
			message.PrepareNDRForFailureToGenerateProtectedMessage();
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0005AD68 File Offset: 0x00058F68
		protected override StageRetryDetails InternalGetRetrySchedule()
		{
			return new StageRetryDetails(StageRetryDetails.FinalAction.SkipStage, TimeSpan.FromMinutes(10.0), TimeSpan.FromDays(1.0));
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0005AD8C File Offset: 0x00058F8C
		protected override void InternalDoSynchronousWork()
		{
			IUMCreateMessage message = base.WorkItem.Message;
			ExAssert.RetailAssert(message != null, "CreateProtectedMessageStage must operate on PipelineContext which implements IUMCreateMessage");
			message.PrepareProtectedMessage();
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0005ADBC File Offset: 0x00058FBC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CreateProtectedMessageStage>(this);
		}
	}
}
