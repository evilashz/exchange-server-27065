using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002F0 RID: 752
	internal class XSOSubmitStage : SubmitStage, IUMNetworkResource
	{
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x00062514 File Offset: 0x00060714
		internal override PipelineDispatcher.PipelineResourceType ResourceType
		{
			get
			{
				return PipelineDispatcher.PipelineResourceType.NetworkBound;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x00062517 File Offset: 0x00060717
		public string NetworkResourceId
		{
			get
			{
				return base.WorkItem.Message.GetMailboxServerId();
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x00062529 File Offset: 0x00060729
		internal override TimeSpan ExpectedRunTime
		{
			get
			{
				return TimeSpan.FromMinutes(3.0);
			}
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x0006253C File Offset: 0x0006073C
		protected override void InternalDoSynchronousWork()
		{
			XSOVoiceMessagePipelineContext xsovoiceMessagePipelineContext = base.WorkItem.Message as XSOVoiceMessagePipelineContext;
			ExAssert.RetailAssert(xsovoiceMessagePipelineContext != null, "XSOSubmitStage must operate on PipelineContext which is an instance of XSOVoiceMessagePipelineContext");
			UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = null;
			try
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Inside XSOSubmitStage", new object[0]);
				mailboxSessionLock = xsovoiceMessagePipelineContext.Caller.CreateSessionLock();
				if (mailboxSessionLock.Session == null || mailboxSessionLock.UnderlyingStoreRPCSessionDisconnected)
				{
					throw new WorkItemNeedsToBeRequeuedException();
				}
				base.MessageToSubmit.Send();
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Successfully submitted the message via xso.", new object[0]);
			}
			finally
			{
				if (mailboxSessionLock != null)
				{
					mailboxSessionLock.Dispose();
				}
			}
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x000625F8 File Offset: 0x000607F8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<XSOSubmitStage>(this);
		}
	}
}
