using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000083 RID: 131
	internal abstract class CAMessageSubmissionManager : ActivityManager
	{
		// Token: 0x060005E5 RID: 1509 RVA: 0x0001986B File Offset: 0x00017A6B
		internal CAMessageSubmissionManager(ActivityManager manager, ActivityManagerConfig config) : base(manager, config)
		{
			this.mbxRecipient = (manager.CallSession.CurrentCallContext.CalleeInfo as UMMailboxRecipient);
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00019897 File Offset: 0x00017A97
		protected bool IsMailboxOverQuota
		{
			get
			{
				return this.isMailboxOverQuota;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0001989F File Offset: 0x00017A9F
		protected bool PipelineHealthy
		{
			get
			{
				return this.pipelineHealthy;
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000198A8 File Offset: 0x00017AA8
		internal override TransitionBase ExecuteAction(string action, BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "CAMessageSubmissionManager class asked to ExecuteAction={0}.", new object[]
			{
				action
			});
			string input;
			if (string.Equals(action, "isQuotaExceeded", StringComparison.OrdinalIgnoreCase))
			{
				input = this.IsMailboxQuotaExceeded(vo);
			}
			else if (string.Equals(action, "isPipelineHealthy", StringComparison.OrdinalIgnoreCase))
			{
				input = this.IsPipelineHealthy(vo);
			}
			else if (string.Equals(action, "canAnnonLeaveMessage", StringComparison.OrdinalIgnoreCase))
			{
				input = this.CanAnnonLeaveMessage(vo);
			}
			else
			{
				if (!string.Equals(action, "HandleFailedTransfer", StringComparison.OrdinalIgnoreCase))
				{
					return base.ExecuteAction(action, vo);
				}
				input = this.HandleFailedTransfer(vo);
			}
			return base.CurrentActivity.GetTransition(input);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00019948 File Offset: 0x00017B48
		internal virtual string HandleFailedTransfer(BaseUMCallSession callSession)
		{
			return null;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001994B File Offset: 0x00017B4B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CAMessageSubmissionManager>(this);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00019954 File Offset: 0x00017B54
		private string IsMailboxQuotaExceeded(BaseUMCallSession vo)
		{
			string result = null;
			if (vo.CurrentCallContext.IsDiagnosticCall)
			{
				return "quotaNotExceeded";
			}
			if (vo.CurrentCallContext.UmSubscriberData != null && vo.CurrentCallContext.UmSubscriberData.IsQuotaExceeded)
			{
				this.isMailboxOverQuota = true;
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_QuotaExceededFailedSubmit, null, new object[]
				{
					vo.CurrentCallContext.CalleeInfo,
					vo.CallId
				});
				PIIMessage data = PIIMessage.Create(PIIType._Callee, vo.CurrentCallContext.CalleeInfo);
				CallIdTracer.TraceWarning(ExTraceGlobals.VoiceMailTracer, this, data, "Quota Exceeded. Failed to submit message for user _Callee.", new object[0]);
			}
			else
			{
				result = "quotaNotExceeded";
			}
			return result;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00019A00 File Offset: 0x00017C00
		private string IsPipelineHealthy(BaseUMCallSession vo)
		{
			string result = null;
			if (vo.CurrentCallContext.IsDiagnosticCall)
			{
				return "pipelineHealthy";
			}
			PipelineSubmitStatus pipelineSubmitStatus = PipelineDispatcher.Instance.CanSubmitWorkItem((this.mbxRecipient != null) ? this.mbxRecipient.ADUser.ServerLegacyDN : "af360a7e-e6d4-494a-ac69-6ae14896d16b", (this.mbxRecipient != null) ? this.mbxRecipient.ADUser.DistinguishedName : "455e5330-ce1f-48d1-b6b1-2e318d2ff2c4", PipelineDispatcher.ThrottledWorkItemType.NonCDRWorkItem);
			this.pipelineHealthy = (pipelineSubmitStatus == PipelineSubmitStatus.Ok);
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "IsPipelineHealthy {0}  SubmitStatus {1}", new object[]
			{
				this.pipelineHealthy,
				pipelineSubmitStatus
			});
			if (this.pipelineHealthy)
			{
				result = "pipelineHealthy";
				UMEventNotificationHelper.PublishUMSuccessEventNotificationItem(ExchangeComponent.UMProtocol, UMNotificationEvent.UMPipelineFull.ToString());
			}
			else
			{
				vo.IncrementCounter(CallAnswerCounters.CallsMissedBecausePipelineNotHealthy);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_FailedSubmitSincePipelineIsFull, null, new object[]
				{
					vo.CurrentCallContext.CalleeInfo.ADRecipient.DistinguishedName,
					vo.CallId
				});
				CallIdTracer.TraceWarning(ExTraceGlobals.VoiceMailTracer, this, "IsPipelineHealthy {0}", new object[]
				{
					this.pipelineHealthy
				});
				if (pipelineSubmitStatus == PipelineSubmitStatus.PipelineFull)
				{
					UMEventNotificationHelper.PublishUMFailureEventNotificationItem(ExchangeComponent.UMProtocol, UMNotificationEvent.UMPipelineFull.ToString());
				}
			}
			return result;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00019B58 File Offset: 0x00017D58
		private string CanAnnonLeaveMessage(BaseUMCallSession vo)
		{
			string result = null;
			UMSubscriber umsubscriber = this.mbxRecipient as UMSubscriber;
			if (umsubscriber == null || vo.CurrentCallContext.IsDiagnosticCall || umsubscriber.CanAnonymousCallersLeaveMessage)
			{
				result = "annonCanLeaveMessage";
			}
			return result;
		}

		// Token: 0x0400025D RID: 605
		private bool isMailboxOverQuota;

		// Token: 0x0400025E RID: 606
		private bool pipelineHealthy = true;

		// Token: 0x0400025F RID: 607
		private UMMailboxRecipient mbxRecipient;
	}
}
