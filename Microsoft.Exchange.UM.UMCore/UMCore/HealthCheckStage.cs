using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002BF RID: 703
	internal class HealthCheckStage : SynchronousPipelineStageBase, IUMNetworkResource
	{
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x0005B5B0 File Offset: 0x000597B0
		internal override PipelineDispatcher.PipelineResourceType ResourceType
		{
			get
			{
				if (this.resourceType == null)
				{
					this.resourceType = new PipelineDispatcher.PipelineResourceType?(this.HealthContext.ResourceBeingChecked);
				}
				return this.resourceType.Value;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x0005B5E0 File Offset: 0x000597E0
		public string NetworkResourceId
		{
			get
			{
				return base.WorkItem.Message.GetMailboxServerId();
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x0005B5F2 File Offset: 0x000597F2
		internal override TimeSpan ExpectedRunTime
		{
			get
			{
				return TimeSpan.FromMinutes(1.0);
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x0005B602 File Offset: 0x00059802
		private HealthCheckPipelineContext HealthContext
		{
			get
			{
				return (HealthCheckPipelineContext)base.WorkItem.Message;
			}
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0005B614 File Offset: 0x00059814
		protected override void InternalDoSynchronousWork()
		{
			this.HealthContext.Passed(this.ResourceType);
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "HealthCheckStage passed for {0}", new object[]
			{
				this.ResourceType
			});
		}

		// Token: 0x04000CD4 RID: 3284
		private PipelineDispatcher.PipelineResourceType? resourceType = null;
	}
}
