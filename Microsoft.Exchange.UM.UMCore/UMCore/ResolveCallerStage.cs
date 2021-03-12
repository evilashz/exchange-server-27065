using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002E1 RID: 737
	internal class ResolveCallerStage : SynchronousPipelineStageBase, IUMNetworkResource
	{
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x0005F4DC File Offset: 0x0005D6DC
		internal override PipelineDispatcher.PipelineResourceType ResourceType
		{
			get
			{
				return PipelineDispatcher.PipelineResourceType.NetworkBound;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x0005F4DF File Offset: 0x0005D6DF
		public string NetworkResourceId
		{
			get
			{
				return base.WorkItem.Message.GetMailboxServerId();
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x0005F4F1 File Offset: 0x0005D6F1
		internal override TimeSpan ExpectedRunTime
		{
			get
			{
				return TimeSpan.FromMinutes(1.0);
			}
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x0005F504 File Offset: 0x0005D704
		internal static void ResolveCaller(PipelineContext pipelineContext)
		{
			ExAssert.RetailAssert(pipelineContext is IUMCAMessage && pipelineContext is IUMResolveCaller, "ResolveCallerStage must operate on PipelineContext which implements IUMCAMessage and IUMResolveCaller. {0}", new object[]
			{
				pipelineContext.GetType().ToString()
			});
			IUMCAMessage iumcamessage = (IUMCAMessage)pipelineContext;
			IUMResolveCaller iumresolveCaller = (IUMResolveCaller)pipelineContext;
			iumresolveCaller.ContactInfo = new DefaultContactInfo();
			UMSubscriber umsubscriber = iumcamessage.CAMessageRecipient as UMSubscriber;
			bool flag = false;
			PIIMessage piimessage = PIIMessage.Create(PIIType._PhoneNumber, pipelineContext.CallerId.ToDial);
			if (!string.IsNullOrEmpty(pipelineContext.CallerAddress))
			{
				IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromUmUser(iumcamessage.CAMessageRecipient);
				IADOrgPerson iadorgPerson = iadrecipientLookup.LookupBySmtpAddress(pipelineContext.CallerAddress) as IADOrgPerson;
				if (iadorgPerson != null)
				{
					iumresolveCaller.ContactInfo = new ADContactInfo(iadorgPerson, umsubscriber.DialPlan, pipelineContext.CallerId);
					PIIMessage[] data = new PIIMessage[]
					{
						piimessage,
						PIIMessage.Create(PIIType._PII, pipelineContext.CallerAddress)
					};
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, null, data, "ResolveCaller:SMTPLookup: callerId:_PhoneNumber callerAddress_PII ADOrgPerson:{0}", new object[]
					{
						iadorgPerson
					});
					flag = true;
				}
			}
			if (!flag)
			{
				ContactInfo contactInfo = ContactInfo.FindContactByCallerId(umsubscriber, pipelineContext.CallerId);
				if (contactInfo != null)
				{
					flag = true;
					iumresolveCaller.ContactInfo = contactInfo;
					PIIMessage[] data2 = new PIIMessage[]
					{
						piimessage,
						PIIMessage.Create(PIIType._UserDisplayName, iumresolveCaller.ContactInfo.DisplayName)
					};
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, null, data2, "ResolveCaller::Contact Search() callerId:_PhoneNumber Contact: '_UserDisplayName'", new object[0]);
				}
			}
			if (GlobCfg.EnableCallerIdDisplayNameResolution && !flag && !string.IsNullOrEmpty(pipelineContext.CallerIdDisplayName))
			{
				iumresolveCaller.ContactInfo = new CallerNameDisplayContactInfo(pipelineContext.CallerIdDisplayName);
				PIIMessage[] data3 = new PIIMessage[]
				{
					piimessage,
					PIIMessage.Create(PIIType._UserDisplayName, iumresolveCaller.ContactInfo.DisplayName)
				};
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, null, data3, "ResolveCaller::CallerID:_PhoneNumber resolved using CallerNameDisplay: '_UserDisplayName'", new object[0]);
			}
			CallContext.UpdateCountersAndPercentages(flag, GeneralCounters.CallerResolutionsSucceeded, GeneralCounters.CallerResolutionsAttempted, GeneralCounters.PercentageSuccessfulCallerResolutions, GeneralCounters.PercentageSuccessfulCallerResolutions_Base);
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x0005F6F8 File Offset: 0x0005D8F8
		internal override bool ShouldRunStage(PipelineWorkItem workItem)
		{
			IUMResolveCaller iumresolveCaller = workItem.Message as IUMResolveCaller;
			return iumresolveCaller.ContactInfo == null;
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0005F71A File Offset: 0x0005D91A
		protected override StageRetryDetails InternalGetRetrySchedule()
		{
			return new StageRetryDetails(StageRetryDetails.FinalAction.SkipStage);
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x0005F724 File Offset: 0x0005D924
		protected override void InternalDoSynchronousWork()
		{
			try
			{
				ResolveCallerStage.ResolveCaller(base.WorkItem.Message);
			}
			catch (Exception ex)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Got exception while resolving caller e={0}", new object[]
				{
					ex
				});
				CallContext.UpdateCountersAndPercentages(false, GeneralCounters.CallerResolutionsSucceeded, GeneralCounters.CallerResolutionsAttempted, GeneralCounters.PercentageSuccessfulCallerResolutions, GeneralCounters.PercentageSuccessfulCallerResolutions_Base);
				throw;
			}
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x0005F798 File Offset: 0x0005D998
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ResolveCallerStage>(this);
		}
	}
}
