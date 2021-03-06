using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001BC RID: 444
	internal class FaxRequest : CommonActivity
	{
		// Token: 0x06000CF0 RID: 3312 RVA: 0x00038B51 File Offset: 0x00036D51
		internal FaxRequest(ActivityManager manager, ActivityConfig config) : base(manager, config)
		{
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00038B5C File Offset: 0x00036D5C
		internal override void StartActivity(BaseUMCallSession callSession, string refInfo)
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_FsmActivityStart, null, new object[]
			{
				this.ToString(),
				callSession.CallId
			});
			this.subscriber = (callSession.CurrentCallContext.CalleeInfo as UMSubscriber);
			PIIMessage data = PIIMessage.Create(PIIType._User, callSession.CurrentCallContext.CalleeInfo.MailAddress);
			if (!this.IsMailboxFaxEnabled(callSession))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, data, "User _User is not fax enabled. disconnecting call.", new object[0]);
				UmGlobals.ExEvent.LogEvent(this.subscriber.DialPlan.OrganizationId, UMEventLogConstants.Tuple_UMUserNotConfiguredForFax, callSession.CurrentCallContext.CalleeInfo.DisplayName, callSession.CurrentCallContext.CalleeInfo.DisplayName, callSession.CurrentCallContext.CallerId, callSession.CurrentCallContext.CalleeId);
				callSession.IncrementCounter(FaxCounters.TotalNumberOfInvalidFaxCalls);
				base.Manager.DropCall(callSession, DropCallReason.UserError);
				return;
			}
			this.partnerFaxServer = this.subscriber.UMMailboxPolicy.FaxServerURI;
			if (string.IsNullOrEmpty(this.partnerFaxServer))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, data, "User _User hasn't set up the fax server URI", new object[0]);
				UmGlobals.ExEvent.LogEvent(this.subscriber.DialPlan.OrganizationId, UMEventLogConstants.Tuple_UserFaxServerSetupFailure, this.subscriber.UMMailboxPolicy.Name, callSession.CurrentCallContext.CalleeInfo.MailAddress, this.subscriber.UMMailboxPolicy.Name);
				CallContext.UpdateCountersAndPercentages(false, FaxCounters.TotalNumberOfSuccessfulValidFaxCalls, FaxCounters.TotalNumberOfValidFaxCalls, FaxCounters.PercentageSuccessfulValidFaxCalls, FaxCounters.PercentageSuccessfulValidFaxCalls_Base);
				base.Manager.DropCall(callSession, DropCallReason.UserError);
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Calling the Accept fax method from FaxRequest.", new object[0]);
			callSession.AcceptFax();
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00038D24 File Offset: 0x00036F24
		internal void OnFaxRequestReceive(BaseUMCallSession callSession)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Entering OnFaxRequestReceive eventhandler for fax request activity.", new object[0]);
			this.SetTransferTarget();
			this.SetReferredByHeader(callSession);
			this.SendOutEventToTransferFax(callSession);
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00038D50 File Offset: 0x00036F50
		private void SetTransferTarget()
		{
			this.partnerFaxServer = Utils.RemoveSIPPrefix(this.partnerFaxServer);
			PhoneNumber phoneNumber = new PhoneNumber(this.partnerFaxServer, PhoneNumberKind.Unknown, UMUriType.SipName);
			base.Manager.TargetPhoneNumber = phoneNumber;
			PIIMessage data = PIIMessage.Create(PIIType._PhoneNumber, phoneNumber.Number);
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, data, "TargetPhoneNumber = _PhoneNumber", new object[0]);
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00038DB0 File Offset: 0x00036FB0
		private void SetReferredByHeader(BaseUMCallSession callSession)
		{
			UMPartnerFaxContext umpartnerFaxContext = UMPartnerContext.Create<UMPartnerFaxContext>();
			umpartnerFaxContext.CallerId = callSession.CurrentCallContext.CallerId.ToDial;
			ExAssert.RetailAssert(!string.IsNullOrEmpty(callSession.CallId), "CallId should not be null or empty");
			umpartnerFaxContext.CallId = callSession.CallId;
			umpartnerFaxContext.PhoneContext = callSession.CurrentCallContext.DialPlan.PhoneContext;
			umpartnerFaxContext.Extension = this.subscriber.Extension;
			umpartnerFaxContext.CallerIdDisplayName = callSession.CurrentCallContext.CallerIdDisplayName;
			PIIMessage[] data = new PIIMessage[]
			{
				PIIMessage.Create(PIIType._PhoneNumber, umpartnerFaxContext.Extension),
				PIIMessage.Create(PIIType._UserDisplayName, umpartnerFaxContext.CallerIdDisplayName)
			};
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, data, "Context: {0}, callerId = {1}, CallId = {2}, Extension = _PhoneNumber, PhoneContext = {3}, CallerIdDisplayName = _UserDisplayName.", new object[]
			{
				umpartnerFaxContext.ToString(),
				umpartnerFaxContext.CallerId,
				umpartnerFaxContext.CallId,
				umpartnerFaxContext.PhoneContext
			});
			string context = Uri.EscapeDataString(umpartnerFaxContext.ToString());
			string recipient = string.Format(CultureInfo.InvariantCulture, "smtp:{0}", new object[]
			{
				this.subscriber.ADRecipient.PrimarySmtpAddress.ToString()
			});
			FaxTransferReferredByHeaderHandler faxTransferReferredByHeaderHandler = new FaxTransferReferredByHeaderHandler();
			PlatformSipUri varValue = faxTransferReferredByHeaderHandler.SerializeFaxTransferUri(recipient, context);
			base.Manager.WriteVariable("ReferredByUri", varValue);
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00038F14 File Offset: 0x00037114
		private void SendOutEventToTransferFax(BaseUMCallSession callSession)
		{
			TransitionBase transition = this.GetTransition("faxRequestAccepted");
			if (transition != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Fax Activity making FaxRequestAccepted transition: {0}.", new object[]
				{
					transition
				});
				transition.Execute(base.Manager, callSession);
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Fax Activity can not make FaxRequestAccepted transition. We drop the call", new object[0]);
			base.Manager.DropCall(callSession, DropCallReason.UserError);
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00038F80 File Offset: 0x00037180
		private bool IsMailboxFaxEnabled(BaseUMCallSession callSession)
		{
			if (this.subscriber != null && this.subscriber.IsFaxEnabled)
			{
				PIIMessage data = PIIMessage.Create(PIIType._EmailAddress, callSession.CurrentCallContext.CalleeInfo.MailAddress);
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, data, "User _EmailAddress is  fax enabled.", new object[0]);
				return true;
			}
			return false;
		}

		// Token: 0x04000A64 RID: 2660
		private UMSubscriber subscriber;

		// Token: 0x04000A65 RID: 2661
		private string partnerFaxServer;
	}
}
