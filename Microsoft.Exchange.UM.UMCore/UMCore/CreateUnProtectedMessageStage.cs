using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002B9 RID: 697
	internal class CreateUnProtectedMessageStage : SynchronousPipelineStageBase, IUMNetworkResource
	{
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x0005AC6E File Offset: 0x00058E6E
		internal override PipelineDispatcher.PipelineResourceType ResourceType
		{
			get
			{
				return PipelineDispatcher.PipelineResourceType.NetworkBound;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x0005AC71 File Offset: 0x00058E71
		public string NetworkResourceId
		{
			get
			{
				return base.WorkItem.Message.GetMailboxServerId();
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x0005AC83 File Offset: 0x00058E83
		internal override TimeSpan ExpectedRunTime
		{
			get
			{
				return TimeSpan.FromMinutes(3.0);
			}
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0005AC94 File Offset: 0x00058E94
		protected override void InternalDoSynchronousWork()
		{
			IUMCreateMessage message = base.WorkItem.Message;
			ExAssert.RetailAssert(message != null, "CreateProtectedMessageStage must operate on PipelineContext which implements IUMCreateMessage");
			message.PrepareUnProtectedMessage();
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0005ACC4 File Offset: 0x00058EC4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CreateUnProtectedMessageStage>(this);
		}
	}
}
