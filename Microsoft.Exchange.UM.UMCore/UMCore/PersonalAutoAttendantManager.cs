using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.PersonalAutoAttendant;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200029C RID: 668
	internal class PersonalAutoAttendantManager : PAAManagerBase, IPAAParent, IPAACommonInterface
	{
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x000577D6 File Offset: 0x000559D6
		// (set) Token: 0x06001410 RID: 5136 RVA: 0x000577DE File Offset: 0x000559DE
		internal bool FindMeSuccessful { get; private set; }

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x000577E7 File Offset: 0x000559E7
		// (set) Token: 0x06001412 RID: 5138 RVA: 0x000577EF File Offset: 0x000559EF
		internal bool IsFirstFindMeTry { get; private set; }

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x000577F8 File Offset: 0x000559F8
		internal bool AreThereMoreFindMeNumbers
		{
			get
			{
				return this.index < this.findMeNumbers.Count;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x0005780D File Offset: 0x00055A0D
		// (set) Token: 0x06001415 RID: 5141 RVA: 0x00057824 File Offset: 0x00055A24
		internal IPAAChild SubscriberManager
		{
			get
			{
				return (IPAAChild)base.CallSession.CurrentCallContext.LinkedManagerPointer;
			}
			set
			{
				base.CallSession.CurrentCallContext.LinkedManagerPointer = value;
			}
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x00057837 File Offset: 0x00055A37
		public object GetCallerRecordedName()
		{
			if (this.RecordedNameOfCaller != null)
			{
				return this.RecordedNameOfCaller;
			}
			return this.ReadVariable("recording");
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x00057853 File Offset: 0x00055A53
		public object GetCalleeRecordedName()
		{
			return base.RecordedName;
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0005785B File Offset: 0x00055A5B
		public void TerminateFindMe()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: TerminateFindMe() ", new object[0]);
			this.SubscriberManager = null;
			this.TransferToPAAMainMenu();
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x00057880 File Offset: 0x00055A80
		public void DisconnectChildCall()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: DisconnectChildCall() ", new object[0]);
			base.CallSession.TerminateDependentSession();
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x000578A3 File Offset: 0x00055AA3
		public void SetPointerToChild(IPAAChild pointer)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: SetPointerToChild()", new object[0]);
			base.CallSession.CurrentCallContext.LinkedManagerPointer = pointer;
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x000578CC File Offset: 0x00055ACC
		public void ContinueFindMe()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: ContinueFindMe() ", new object[0]);
			if (this.state != FindMeState.FindMe || this.FindMeSuccessful)
			{
				return;
			}
			this.SubscriberManager = null;
			this.DoNextFindMe();
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x00057903 File Offset: 0x00055B03
		public void AcceptCall()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: AcceptCall() ", new object[0]);
			base.TargetPhoneNumber = this.currentFindmeNumber;
			this.FindMeSuccessful = true;
			base.CallSession.StopPlaybackAndCancelRecognition();
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0005793C File Offset: 0x00055B3C
		internal string StartFindMe(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: StartFindMe() ", new object[0]);
			this.currentFindmeNumber = null;
			this.IsFirstFindMeTry = true;
			this.GetCalleeRecordedName();
			this.index = 0;
			FindMe givenNumber;
			if (!this.TryGetNextFindMeNumber(out givenNumber))
			{
				throw new InvalidOperationException();
			}
			return this.FindMe(givenNumber);
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00057994 File Offset: 0x00055B94
		internal string TerminateFindMe(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: TerminateFindMe(vo) ", new object[0]);
			this.state = FindMeState.None;
			this.CancelOutBoundCallTimer();
			if (this.SubscriberManager == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: calling vo.TerminateDependentSession() ", new object[0]);
				this.DropChildCall();
			}
			else
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: calling this.SubscriberManager.TerminateCall() ", new object[0]);
				this.SubscriberManager.TerminateCall();
				this.SubscriberManager = null;
			}
			return null;
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x00057A13 File Offset: 0x00055C13
		internal string CleanupFindMe(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: CleanupFindMe() ", new object[0]);
			if (this.state != FindMeState.FindMe)
			{
				return null;
			}
			return this.TerminateFindMe(null);
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x00057A40 File Offset: 0x00055C40
		internal string ContinueFindMe(BaseUMCallSession vo)
		{
			string result = "moreFindMeNumbersLeft";
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: ContinueFindMe(vo) ", new object[0]);
			bool findMeSuccessful = this.FindMeSuccessful;
			this.FindMeSuccessful = false;
			this.IsFirstFindMeTry = false;
			if (findMeSuccessful || !this.AreThereMoreFindMeNumbers)
			{
				this.state = FindMeState.None;
				result = null;
			}
			this.SubscriberManager.TerminateCallToTryNextNumberTransfer();
			this.SubscriberManager = null;
			return result;
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x00057AA8 File Offset: 0x00055CA8
		internal override void OnOutBoundCallRequestCompleted(BaseUMCallSession outboundCallSession, OutboundCallDetailsEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: OnOutBoundCallRequestCompleted(), mystate = {0}, outcome ={1}", new object[]
			{
				this.state,
				callSessionEventArgs.CallOutcome
			});
			if (this.state != FindMeState.FindMe)
			{
				outboundCallSession.DisconnectCall();
				return;
			}
			this.CancelOutBoundCallTimer();
			outboundCallSession.CurrentCallContext.CallType = 9;
			if (callSessionEventArgs.CallOutcome == OutboundCallDetailsEventArgs.OutboundCallOutcome.Failure)
			{
				outboundCallSession.HandleFailedOutboundCall(callSessionEventArgs);
				this.state = FindMeState.None;
				this.DoNextFindMe();
				return;
			}
			outboundCallSession.CurrentCallContext.LinkedManagerPointer = this;
			outboundCallSession.CurrentCallContext.CallerInfo = new UMSubscriber(base.CallSession.CurrentCallContext.CalleeInfo.ADRecipient);
			outboundCallSession.CurrentCallContext.CallerInfo.IsAuthenticated = true;
			outboundCallSession.CurrentCallContext.DialPlan = base.CallSession.CurrentCallContext.DialPlan;
			outboundCallSession.InitializeConnectedCall(callSessionEventArgs);
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x00057B8E File Offset: 0x00055D8E
		private void DropChildCall()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: DropChildCall() ", new object[0]);
			base.CallSession.DisconnectDependentUMCallSession();
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x00057BB1 File Offset: 0x00055DB1
		private void CancelOutBoundCallTimer()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: CancelOutBoundCallTimer() ", new object[0]);
			if (this.outboundCallTimer != null && this.outboundCallTimer.IsActive)
			{
				this.outboundCallTimer.Dispose();
				this.outboundCallTimer = null;
			}
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x00057BF0 File Offset: 0x00055DF0
		private void MaximumOutboundCallConnectTimeExceeded(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: MaximumOutboundCallConnectTimeExceeded(vo) ", new object[0]);
			this.CancelOutBoundCallTimer();
			this.DisconnectChildCall();
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x00057C14 File Offset: 0x00055E14
		private string FindMe(FindMe givenNumber)
		{
			this.state = FindMeState.FindMe;
			bool flag = false;
			FindMe findMe = givenNumber;
			BaseUMCallSession baseUMCallSession = null;
			while (!flag)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: FindMe(), number= {0}, Timeout ={1} secs, currentCalls ={2}, maxcallsAllowed={3}", new object[]
				{
					findMe.Number,
					findMe.Timeout,
					GeneralCounters.CurrentCalls.RawValue,
					(CommonConstants.MaxCallsAllowed != null) ? CommonConstants.MaxCallsAllowed.Value.ToString() : "not set"
				});
				if (Util.MaxCallLimitExceeded())
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_FindMeFailedSinceMaximumCallsLimitReached, null, new object[]
					{
						CommonConstants.MaxCallsAllowed.Value,
						base.CallSession.CallId
					});
					this.state = FindMeState.None;
					return "maxAllowedCallsLimitReached";
				}
				this.currentFindmeNumber = findMe.PhoneNumber;
				try
				{
					UmServiceGlobals.VoipPlatform.CreateAndMakeCallOnDependentSession(base.CallSession, new UMCallSessionHandler<OutboundCallDetailsEventArgs>(this.OnOutBoundCallRequestCompleted), base.Subscriber, this.GetCallerIdToUseForFindMe(), this.currentFindmeNumber, out baseUMCallSession);
					baseUMCallSession.CurrentCallContext.CallLoggingHelper.FindMeDialedString = this.currentFindmeNumber.ToDial;
					flag = true;
				}
				catch (InvalidPhoneNumberException)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_FindMeInvalidPhoneNumber, null, new object[]
					{
						CommonUtil.ToEventLogString(base.CallSession.CurrentCallContext.CalleeInfo.ADRecipient.Name),
						CommonUtil.ToEventLogString(this.currentFindmeNumber.ToDial),
						base.CallSession.CallId
					});
					if (!this.HandleDialingFailed(out findMe))
					{
						return "dialingRulesCheckFailed";
					}
				}
				catch (DialingRulesException)
				{
					if (!this.HandleDialingFailed(out findMe))
					{
						return "dialingRulesCheckFailed";
					}
				}
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: Creating outboundCall Timer", new object[0]);
			this.outboundCallTimer = base.CallSession.StartTimer(new BaseUMAsyncTimer.UMTimerCallback(this.MaximumOutboundCallConnectTimeExceeded), findMe.Timeout);
			return null;
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x00057E48 File Offset: 0x00056048
		private bool HandleDialingFailed(out FindMe number)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: HandleDialingFailed", new object[0]);
			if (!this.TryGetNextFindMeNumber(out number))
			{
				base.CallSession.TerminateDependentSession();
				this.state = FindMeState.None;
				return false;
			}
			return true;
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x00057E7E File Offset: 0x0005607E
		private void DropFindMeOutboundCall()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: DropFindMeOutboundCall", new object[0]);
			if (this.state == FindMeState.FindMe)
			{
				this.TerminateFindMe(null);
			}
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x00057EA8 File Offset: 0x000560A8
		private bool TryGetNextFindMeNumber(out FindMe number)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: TryGetNextFindMeNumber()", new object[0]);
			if (this.index >= this.findMeNumbers.Count)
			{
				number = null;
				return false;
			}
			PhoneNumber phoneNumber;
			bool flag;
			for (;;)
			{
				number = this.findMeNumbers[this.index];
				flag = PhoneNumber.TryParse(base.CallSession.CurrentCallContext.DialPlan, number.Number, out phoneNumber);
				this.index++;
				if (flag)
				{
					break;
				}
				if (this.index >= this.findMeNumbers.Count)
				{
					return flag;
				}
			}
			number.PhoneNumber = phoneNumber;
			return flag;
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x00057F48 File Offset: 0x00056148
		private string GetCallerIdToUseForFindMe()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: GetCallerIdToUseForFindMe()", new object[0]);
			if (base.CallSession.CurrentCallContext.IsVirtualNumberCall)
			{
				UMSubscriber umsubscriber = base.CallSession.CurrentCallContext.CalleeInfo as UMSubscriber;
				if (!string.IsNullOrEmpty(umsubscriber.VirtualNumber))
				{
					return umsubscriber.VirtualNumber;
				}
			}
			if (base.CallSession.CurrentCallContext.DialPlan.URIType == UMUriType.SipName)
			{
				return base.CallSession.CurrentCallContext.FromUriOfCall.ToString();
			}
			if (base.CallSession.CurrentCallContext.IsAnonymousCaller)
			{
				return "Anonymous";
			}
			return base.CallSession.CurrentCallContext.CallerId.ToDial;
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x00058004 File Offset: 0x00056204
		private void DoNextFindMe()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: DoNextFindMe()", new object[0]);
			FindMe givenNumber;
			if (!this.TryGetNextFindMeNumber(out givenNumber))
			{
				this.TransferToPAAMainMenu();
				return;
			}
			if (this.FindMe(givenNumber) != null)
			{
				this.TransferToPAAMainMenu();
			}
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x00058047 File Offset: 0x00056247
		private void TransferToPAAMainMenu()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager: TransferToPAAMainMenu()", new object[0]);
			this.state = FindMeState.None;
			this.CancelOutBoundCallTimer();
			base.CallSession.StopPlaybackAndCancelRecognition();
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x00058077 File Offset: 0x00056277
		internal PersonalAutoAttendantManager(ActivityManager manager, PersonalAutoAttendantManager.ConfigClass config) : base(manager, config)
		{
			this.findMeEnabled = false;
			this.state = FindMeState.None;
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager::ctor()", new object[0]);
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x000580A5 File Offset: 0x000562A5
		public bool Timeout
		{
			get
			{
				return this.timeout;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x000580AD File Offset: 0x000562AD
		public bool HavePersonalOperator
		{
			get
			{
				return this.operatorNumber != null;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x000580BB File Offset: 0x000562BB
		// (set) Token: 0x06001430 RID: 5168 RVA: 0x000580C3 File Offset: 0x000562C3
		public bool ExecuteBlindTransfer
		{
			get
			{
				return this.executeBlindTransfer;
			}
			set
			{
				this.executeBlindTransfer = value;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x000580CC File Offset: 0x000562CC
		public bool FindMeEnabled
		{
			get
			{
				return this.findMeEnabled;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x000580D4 File Offset: 0x000562D4
		public bool ExecuteTransferToMailbox
		{
			get
			{
				return this.executeTransferToMailbox;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x000580DC File Offset: 0x000562DC
		// (set) Token: 0x06001434 RID: 5172 RVA: 0x000580E4 File Offset: 0x000562E4
		public bool ExecuteTransferToVoiceMessage
		{
			get
			{
				return this.executeTransferToVoiceMessage;
			}
			set
			{
				this.executeTransferToVoiceMessage = value;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x000580ED File Offset: 0x000562ED
		// (set) Token: 0x06001436 RID: 5174 RVA: 0x000580F5 File Offset: 0x000562F5
		public bool PermissionCheckFailure
		{
			get
			{
				return this.permissionCheckFailure;
			}
			set
			{
				this.permissionCheckFailure = value;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x000580FE File Offset: 0x000562FE
		// (set) Token: 0x06001438 RID: 5176 RVA: 0x00058106 File Offset: 0x00056306
		public bool InvalidADContact
		{
			get
			{
				return this.invalidADContact;
			}
			set
			{
				this.invalidADContact = value;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x0005810F File Offset: 0x0005630F
		internal string EvaluationStatus
		{
			get
			{
				return this.evaluationStatus.ToString();
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x00058121 File Offset: 0x00056321
		// (set) Token: 0x0600143B RID: 5179 RVA: 0x00058129 File Offset: 0x00056329
		internal object RecordedNameOfCaller
		{
			get
			{
				return this.recordedNameOfCaller;
			}
			set
			{
				this.recordedNameOfCaller = value;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x00058132 File Offset: 0x00056332
		// (set) Token: 0x0600143D RID: 5181 RVA: 0x0005813A File Offset: 0x0005633A
		internal bool CallerIsResolvedToADContact
		{
			get
			{
				return this.callerIsResolvedToADContact;
			}
			set
			{
				this.callerIsResolvedToADContact = value;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x00058143 File Offset: 0x00056343
		internal bool TimeOut
		{
			get
			{
				return this.timeout;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x0005814B File Offset: 0x0005634B
		internal bool TargetHasValidPAA
		{
			get
			{
				return this.targetHasValidPAA;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x00058153 File Offset: 0x00056353
		internal bool TargetPAAInDifferentSite
		{
			get
			{
				return this.targetPAAInDifferentSite && !this.targetHasValidPAA;
			}
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x00058168 File Offset: 0x00056368
		internal static bool TryGetTargetPAA(CallContext callcontext, ADRecipient mailboxTransferTarget, UMDialPlan originatorDialPlan, PhoneNumber callerId, out PersonalAutoAttendant paa, out bool requiresTransferToAnotherServer, out BricksRoutingBasedServerChooser serverPicker)
		{
			paa = null;
			requiresTransferToAnotherServer = false;
			serverPicker = null;
			bool result;
			using (UMSubscriber umsubscriber = UMRecipient.Factory.FromADRecipient<UMSubscriber>(mailboxTransferTarget))
			{
				if (umsubscriber == null || !umsubscriber.DialPlan.Identity.Equals(originatorDialPlan.Identity))
				{
					result = false;
				}
				else
				{
					serverPicker = new BricksRoutingBasedServerChooser(callcontext, umsubscriber, 4);
					if (serverPicker.IsRedirectionNeeded)
					{
						requiresTransferToAnotherServer = true;
						result = false;
					}
					else
					{
						using (IPAAEvaluator ipaaevaluator = EvaluateUserPAA.Create(umsubscriber, callerId, umsubscriber.Extension))
						{
							if (!ipaaevaluator.GetEffectivePAA(out paa))
							{
								CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, null, "PersonalAutoAttendantManager::TryGetTargetPAA: Target = {0} did not find valid PAA. Transferring to voicemail maybe", new object[]
								{
									mailboxTransferTarget.DisplayName
								});
								result = false;
							}
							else
							{
								PIIMessage data = PIIMessage.Create(PIIType._UserDisplayName, mailboxTransferTarget.DisplayName);
								CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, null, data, "PersonalAutoAttendantManager::TryGetTargetPAA: Target = _UserDisplayName found valid PAA {0}", new object[]
								{
									paa.Identity.ToString()
								});
								result = true;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x00058284 File Offset: 0x00056484
		public override void SetADTransferTarget(ADRecipient mailboxTransferTarget)
		{
			this.mailboxTransferTarget = mailboxTransferTarget;
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0005828D File Offset: 0x0005648D
		public override void SetBlindTransferEnabled(bool enabled, PhoneNumber target)
		{
			this.executeBlindTransfer = enabled;
			base.TargetPhoneNumber = target;
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0005829D File Offset: 0x0005649D
		public override void SetPermissionCheckFailure()
		{
			this.permissionCheckFailure = true;
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x000582A6 File Offset: 0x000564A6
		public override void SetTransferToMailboxEnabled()
		{
			this.executeTransferToMailbox = true;
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x000582AF File Offset: 0x000564AF
		public override void SetTransferToVoiceMessageEnabled()
		{
			this.executeTransferToVoiceMessage = true;
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x000582B8 File Offset: 0x000564B8
		public override void SetInvalidADContact()
		{
			this.invalidADContact = true;
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x000582C4 File Offset: 0x000564C4
		public override void SetFindMeNumbers(FindMe[] numbers)
		{
			List<FindMe> list = new List<FindMe>();
			foreach (FindMe findMe in numbers)
			{
				if (findMe.ValidationResult == PAAValidationResult.Valid)
				{
					list.Add(findMe);
				}
			}
			if (list.Count > 0)
			{
				this.findMeEnabled = true;
				this.findMeNumbers = list;
				return;
			}
			this.findMeEnabled = false;
			this.SetPermissionCheckFailure();
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x00058320 File Offset: 0x00056520
		internal override void Start(BaseUMCallSession vo, string refInfo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager::Start()", new object[0]);
			this.paaEvents = new PersonalAutoAttendantManager.PAAEvents(vo);
			base.Subscriber = (UMSubscriber)vo.CurrentCallContext.CalleeInfo;
			this.callId = vo.CallId;
			this.operatorNumber = Util.SA_GetOperatorNumber(vo.CurrentCallContext.DialPlan, base.Subscriber);
			base.Start(vo, refInfo);
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x00058398 File Offset: 0x00056598
		internal string Reset(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager::Reset()", new object[0]);
			this.executeBlindTransfer = false;
			this.permissionCheckFailure = false;
			this.executeTransferToVoiceMessage = false;
			this.findMeEnabled = false;
			this.executeTransferToMailbox = false;
			this.invalidADContact = false;
			this.findMeNumbers = null;
			this.keySelected = -1;
			base.RecordContext.Reset();
			return null;
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x00058400 File Offset: 0x00056600
		internal string GetAutoAttendant(BaseUMCallSession vo)
		{
			this.evaluationStatus = PAAEvaluationStatus.Failure;
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager::GetAutoAttendant()", new object[0]);
			if (!base.Subscriber.IsPAAEnabled)
			{
				PIIMessage data = PIIMessage.Create(PIIType._UserDisplayName, vo.CurrentCallContext.CalleeInfo.DisplayName);
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "PersonalAutoAttendantManager::GetAutoAttendant() user _UserDisplayName is not enabled for PAA", new object[0]);
				this.evaluationStatus = PAAEvaluationStatus.Failure;
				return null;
			}
			PersonalAutoAttendant personalAutoAttendant = (PersonalAutoAttendant)this.GlobalManager.ReadVariable("TargetPAA");
			if (personalAutoAttendant == null)
			{
				personalAutoAttendant = vo.CurrentCallContext.UmSubscriberData.PersonalAutoAttendant;
				if (personalAutoAttendant == null)
				{
					this.evaluationStatus = (vo.CurrentCallContext.UmSubscriberData.TimedOut ? PAAEvaluationStatus.Timeout : PAAEvaluationStatus.Failure);
					CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager::GetAutoAttendant() Did not get PAA. EvaluationStatus = {0}", new object[]
					{
						this.evaluationStatus
					});
				}
				else
				{
					this.evaluationStatus = PAAEvaluationStatus.Success;
					base.PersonalAutoAttendant = personalAutoAttendant;
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CallAnsweredByPAA, null, new object[]
					{
						vo.CallId,
						personalAutoAttendant.Identity.ToString(),
						vo.CurrentCallContext.Extension
					});
					this.reader = new NonBlockingReader(new NonBlockingReader.Operation(this.GetGreetingCallback), personalAutoAttendant, PAAConstants.PAAGreetingDownloadTimeout, new NonBlockingReader.TimeoutCallback(this.TimedOutGetGreeting));
					this.reader.StartAsyncOperation();
					CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager::GetAutoAttendant() Got PAA ID={0} Enabled={1} Version={2} Valid={3}", new object[]
					{
						base.PersonalAutoAttendant.Identity.ToString(),
						base.PersonalAutoAttendant.Enabled,
						base.PersonalAutoAttendant.Version.ToString(),
						base.PersonalAutoAttendant.Valid
					});
				}
				this.paaEvents.EvaluatingPAAComplete(this.evaluationStatus, vo.CurrentCallContext.UmSubscriberData.SubscriberHasPAAConfigured, vo.CurrentCallContext.UmSubscriberData.PAAEvaluationTime);
				return null;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager::GetAutoAttendant() [PAA->PAA TRANSFER] Got PAA ID={0} Enabled={1} Version={2} Valid={3}", new object[]
			{
				personalAutoAttendant.Identity.ToString(),
				personalAutoAttendant.Enabled,
				personalAutoAttendant.Version.ToString(),
				personalAutoAttendant.Valid
			});
			if (PAAUtils.IsCompatible(personalAutoAttendant.Version) && personalAutoAttendant.Enabled)
			{
				this.evaluationStatus = PAAEvaluationStatus.Success;
				base.PersonalAutoAttendant = personalAutoAttendant;
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CallAnsweredByPAA, null, new object[]
				{
					vo.CallId,
					personalAutoAttendant.Identity.ToString(),
					vo.CurrentCallContext.Extension
				});
				this.reader = new NonBlockingReader(new NonBlockingReader.Operation(this.GetGreetingCallback), personalAutoAttendant, PAAConstants.PAAGreetingDownloadTimeout, new NonBlockingReader.TimeoutCallback(this.TimedOutGetGreeting));
				this.reader.StartAsyncOperation();
				return null;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager::GetAutoAttendant() [PAA->PAA TRANSFER] PAA ID={0} Is not compatible, or not valid", new object[]
			{
				personalAutoAttendant.Identity.ToString()
			});
			this.evaluationStatus = PAAEvaluationStatus.Failure;
			base.PersonalAutoAttendant = null;
			return null;
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x00058768 File Offset: 0x00056968
		internal void TimedOutGetGreeting(object state)
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_TimedOutRetrievingMailboxData, null, new object[]
			{
				base.Subscriber.MailAddress,
				this.callId,
				base.Subscriber.ExchangePrincipal.MailboxInfo.Location.ServerFqdn,
				base.Subscriber.ExchangePrincipal.MailboxInfo.MailboxDatabase.ToString(),
				CommonUtil.ToEventLogString(new StackTrace(true))
			});
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x000587ED File Offset: 0x000569ED
		internal override string GetGreeting(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager::GetGreeting()", new object[0]);
			this.reader.WaitForCompletion();
			return null;
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x00058814 File Offset: 0x00056A14
		internal string ResolveCallingLineIdToGalContact(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager::ResolveCallingLineIdToGalContact()", new object[0]);
			this.RecordedNameOfCaller = null;
			this.CallerIsResolvedToADContact = false;
			IADRecipient iadrecipient = null;
			if (vo.CurrentCallContext.CallerInfo == null)
			{
				ADContactInfo adcontactInfo = null;
				ADContactInfo.TryFindCallerByCallerId(base.Subscriber, vo.CurrentCallContext.CallerId, out adcontactInfo);
				if (adcontactInfo != null)
				{
					iadrecipient = adcontactInfo.ADOrgPerson;
				}
			}
			else
			{
				iadrecipient = vo.CurrentCallContext.CallerInfo.ADRecipient;
			}
			if (iadrecipient == null)
			{
				return null;
			}
			this.CallerIsResolvedToADContact = true;
			this.recordedNameOfCaller = base.GetRecordedName(iadrecipient);
			return null;
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x000588A6 File Offset: 0x00056AA6
		internal string SetOperatorNumber(BaseUMCallSession vo)
		{
			base.TargetPhoneNumber = this.operatorNumber;
			return null;
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x000588B5 File Offset: 0x00056AB5
		internal string PrepareForVoiceMail(BaseUMCallSession vo)
		{
			return null;
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x000588B8 File Offset: 0x00056AB8
		internal string ProcessSelection(BaseUMCallSession vo)
		{
			string dtmfDigits = base.DtmfDigits;
			int key;
			if (dtmfDigits == "#")
			{
				key = 10;
			}
			else
			{
				key = int.Parse(dtmfDigits, CultureInfo.InvariantCulture);
			}
			this.SelectMenu(key);
			return null;
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x000588F4 File Offset: 0x00056AF4
		internal string SelectNextAction(BaseUMCallSession vo)
		{
			if (this.actionsIterator == null)
			{
				this.actionsIterator = base.PersonalAutoAttendant.AutoActionsList.SortedMenu.GetEnumerator();
			}
			if (this.actionsIterator.MoveNext())
			{
				KeyMappingBase keyMappingBase = this.actionsIterator.Current;
				this.SelectMenu(keyMappingBase.Key);
				return null;
			}
			return "noActionLeft";
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x00058958 File Offset: 0x00056B58
		internal string HandleTimeout(BaseUMCallSession vo)
		{
			this.numFailures++;
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager::HandleTimeout() NumFailures={0} MaxAllowed = {1}.", new object[]
			{
				this.numFailures,
				3
			});
			if (this.numFailures >= 3)
			{
				return "menuRetriesExceeded";
			}
			return null;
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x000589B2 File Offset: 0x00056BB2
		internal string PrepareForTransfer(BaseUMCallSession vo)
		{
			return null;
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x000589B5 File Offset: 0x00056BB5
		internal string PrepareForFindMe(BaseUMCallSession vo)
		{
			return this.ResolveCallingLineIdToGalContact(vo);
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x000589BE File Offset: 0x00056BBE
		internal string TransferToPAASiteFailed(BaseUMCallSession vo)
		{
			this.GlobalManager.WriteVariable("directorySearchResult", ContactSearchItem.CreateFromRecipient(this.mailboxTransferTarget));
			return null;
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x000589DC File Offset: 0x00056BDC
		internal string PrepareForTransferToMailbox(BaseUMCallSession vo)
		{
			PIIMessage data = PIIMessage.Create(PIIType._UserDisplayName, this.mailboxTransferTarget.DisplayName);
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "PersonalAutoAttendantManager::PrepareForTransferToMailbox: Target = _UserDisplayName", new object[0]);
			PersonalAutoAttendant personalAutoAttendant = null;
			BricksRoutingBasedServerChooser bricksRoutingBasedServerChooser = null;
			this.targetHasValidPAA = PersonalAutoAttendantManager.TryGetTargetPAA(vo.CurrentCallContext, this.mailboxTransferTarget, base.Subscriber.DialPlan, vo.CurrentCallContext.CallerId, out personalAutoAttendant, out this.targetPAAInDifferentSite, out bricksRoutingBasedServerChooser);
			this.SendMissedCall(vo);
			if (!this.targetHasValidPAA)
			{
				if (!this.targetPAAInDifferentSite)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "PersonalAutoAttendantManager::PrepareForTransferToMailbox: Target = _DisplayName did not find valid PAA. Transferring to voicemail", new object[0]);
					this.GlobalManager.WriteVariable("directorySearchResult", ContactSearchItem.CreateFromRecipient(this.mailboxTransferTarget));
				}
				else
				{
					ExAssert.RetailAssert(bricksRoutingBasedServerChooser != null, "ServerPicker cannot be null if transferToAnotherServer is needed");
					vo.CurrentCallContext.ServerPicker = bricksRoutingBasedServerChooser;
					string referredByHostUri = null;
					if (vo.CurrentCallContext.CallInfo.DiversionInfo.Count > 0)
					{
						referredByHostUri = vo.CurrentCallContext.CallInfo.DiversionInfo[0].UserAtHost;
					}
					UserTransferWithContext userTransferWithContext = new UserTransferWithContext(referredByHostUri);
					this.GlobalManager.WriteVariable("ReferredByUri", userTransferWithContext.SerializeCACallTransferWithContextUri(this.mailboxTransferTarget.UMExtension, vo.CurrentCallContext.DialPlan.PhoneContext));
				}
			}
			else
			{
				this.targetPAA = personalAutoAttendant;
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "PersonalAutoAttendantManager::PrepareForTransferToMailbox: Target = _DisplayName found valid PAA {0}", new object[]
				{
					this.targetPAA.Identity.ToString()
				});
				vo.CurrentCallContext.CalleeInfo = UMRecipient.Factory.FromADRecipient<UMRecipient>(this.mailboxTransferTarget);
			}
			return null;
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x00058B84 File Offset: 0x00056D84
		internal string PrepareForTransferToPaa(BaseUMCallSession vo)
		{
			if (this.targetPAA == null)
			{
				throw new InvalidOperationException("Got a NULL TargetPAA");
			}
			PIIMessage data = PIIMessage.Create(PIIType._UserDisplayName, this.mailboxTransferTarget.DisplayName);
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "PersonalAutoAttendantManager::PrepareForTransferToPaa: Target Mailbox = _UserDisplayName, Target PAA = {0}", new object[]
			{
				this.targetPAA.Identity.ToString()
			});
			this.GlobalManager.WriteVariable("TargetPAA", this.targetPAA);
			return null;
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x00058C02 File Offset: 0x00056E02
		internal string PrepareForTransferToVoicemail(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager::PrepareForTransferToVoicemail()", new object[0]);
			return null;
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x00058C1C File Offset: 0x00056E1C
		internal override void OnTransferComplete(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager : OnTransferComplete() called, state = {0} Error = {1}", new object[]
			{
				this.state,
				callSessionEventArgs.Error
			});
			if (this.state == FindMeState.FindMe)
			{
				this.SubscriberManager.TerminateCall();
				this.SubscriberManager = null;
				this.state = FindMeState.None;
			}
			base.OnTransferComplete(vo, callSessionEventArgs);
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x00058C82 File Offset: 0x00056E82
		internal override void CheckAuthorization(UMSubscriber u)
		{
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x00058C84 File Offset: 0x00056E84
		internal override void DropCall(BaseUMCallSession vo, DropCallReason reason)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager : DropCall() called, state = {0} ", new object[]
			{
				this.state
			});
			this.DropFindMeOutboundCall();
			this.SendMissedCall(vo);
			base.DropCall(vo, reason);
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x00058CCC File Offset: 0x00056ECC
		internal override void OnUserHangup(BaseUMCallSession vo, UMCallSessionEventArgs voiceEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager : OnUserHangup() called, state = {0} ", new object[]
			{
				this.state
			});
			this.SendMissedCall(vo);
			this.DropFindMeOutboundCall();
			base.OnUserHangup(vo, voiceEventArgs);
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x00058D14 File Offset: 0x00056F14
		internal override void OnTimeout(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager::OnTimeout()", new object[0]);
			this.timeout = true;
			base.OnTimeout(vo, callSessionEventArgs);
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x00058D3B File Offset: 0x00056F3B
		internal override void OnInput(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager::OnInput()", new object[0]);
			this.timeout = false;
			base.OnInput(vo, callSessionEventArgs);
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x00058D64 File Offset: 0x00056F64
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this.reader != null)
					{
						this.reader.Dispose();
					}
					if (this.actionsIterator != null)
					{
						this.actionsIterator.Dispose();
					}
				}
			}
			finally
			{
				base.InternalDispose(disposing);
			}
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x00058DB4 File Offset: 0x00056FB4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PersonalAutoAttendantManager>(this);
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x00058DBC File Offset: 0x00056FBC
		private void SendMissedCall(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager::SendMissedCall() this.sendMissedCall = {0}", new object[]
			{
				this.sentMissedCall
			});
			int num = Interlocked.Increment(ref this.sentMissedCall);
			if (num == 1)
			{
				RecordVoicemailManager.MessageSubmissionHelper messageSubmissionHelper = RecordVoicemailManager.MessageSubmissionHelper.Create(vo);
				messageSubmissionHelper.SubmitMissedCall(false);
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager::SendMissedCall() MissedCall was already sent. Returning.", new object[0]);
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x00058E28 File Offset: 0x00057028
		private void GetGreetingCallback(object state)
		{
			using (new CallId(this.callId))
			{
				this.GetGreetingCallbackWorker(state);
			}
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x00058E64 File Offset: 0x00057064
		private void GetGreetingCallbackWorker(object state)
		{
			PersonalAutoAttendant personalAutoAttendant = (PersonalAutoAttendant)state;
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "GetGreetingCallback() PAA {0}", new object[]
			{
				personalAutoAttendant.Identity
			});
			base.LoadGreetingForPAA(personalAutoAttendant);
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x00058EA8 File Offset: 0x000570A8
		private void SelectMenu(int key)
		{
			this.keySelected = key;
			PAAMenuItem paamenuItem = base.PAAMenuItems[this.keySelected];
			this.findMeEnabled = false;
			PAAManagerBase.PAAPresentationObject paapresentationObject = base.Menu[key];
			paapresentationObject.SetVariablesForTransfer(this);
		}

		// Token: 0x04000C7D RID: 3197
		private const int MaxAllowedFailures = 3;

		// Token: 0x04000C7E RID: 3198
		private int index;

		// Token: 0x04000C7F RID: 3199
		private BaseUMAsyncTimer outboundCallTimer;

		// Token: 0x04000C80 RID: 3200
		private PhoneNumber currentFindmeNumber;

		// Token: 0x04000C81 RID: 3201
		private FindMeState state;

		// Token: 0x04000C82 RID: 3202
		private int numFailures;

		// Token: 0x04000C83 RID: 3203
		private PhoneNumber operatorNumber;

		// Token: 0x04000C84 RID: 3204
		private bool findMeEnabled;

		// Token: 0x04000C85 RID: 3205
		private bool executeBlindTransfer;

		// Token: 0x04000C86 RID: 3206
		private bool executeTransferToMailbox;

		// Token: 0x04000C87 RID: 3207
		private bool executeTransferToVoiceMessage;

		// Token: 0x04000C88 RID: 3208
		private bool permissionCheckFailure;

		// Token: 0x04000C89 RID: 3209
		private bool invalidADContact;

		// Token: 0x04000C8A RID: 3210
		private PAAEvaluationStatus evaluationStatus;

		// Token: 0x04000C8B RID: 3211
		private bool targetHasValidPAA;

		// Token: 0x04000C8C RID: 3212
		private bool targetPAAInDifferentSite;

		// Token: 0x04000C8D RID: 3213
		private bool timeout;

		// Token: 0x04000C8E RID: 3214
		private ADRecipient mailboxTransferTarget;

		// Token: 0x04000C8F RID: 3215
		private PersonalAutoAttendant targetPAA;

		// Token: 0x04000C90 RID: 3216
		private int keySelected;

		// Token: 0x04000C91 RID: 3217
		private PersonalAutoAttendantManager.PAAEvents paaEvents;

		// Token: 0x04000C92 RID: 3218
		private List<FindMe> findMeNumbers;

		// Token: 0x04000C93 RID: 3219
		private NonBlockingReader reader;

		// Token: 0x04000C94 RID: 3220
		private string callId;

		// Token: 0x04000C95 RID: 3221
		private object recordedNameOfCaller;

		// Token: 0x04000C96 RID: 3222
		private bool callerIsResolvedToADContact;

		// Token: 0x04000C97 RID: 3223
		private int sentMissedCall;

		// Token: 0x04000C98 RID: 3224
		private IEnumerator<KeyMappingBase> actionsIterator;

		// Token: 0x0200029D RID: 669
		internal class ConfigClass : ActivityManagerConfig
		{
			// Token: 0x06001466 RID: 5222 RVA: 0x00058EE5 File Offset: 0x000570E5
			public ConfigClass(ActivityManagerConfig manager) : base(manager)
			{
			}

			// Token: 0x06001467 RID: 5223 RVA: 0x00058EEE File Offset: 0x000570EE
			internal override ActivityManager CreateActivityManager(ActivityManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "Constructing PersonalAutoAttendant activity manager.", new object[0]);
				return new PersonalAutoAttendantManager(manager, this);
			}

			// Token: 0x06001468 RID: 5224 RVA: 0x00058F0D File Offset: 0x0005710D
			internal override void Load(XmlNode rootNode)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "Loading a new PersonalAutoAttendantConfig.", new object[0]);
				base.Load(rootNode);
			}
		}

		// Token: 0x0200029F RID: 671
		internal class PAAEvents : IPAAEvents
		{
			// Token: 0x0600146B RID: 5227 RVA: 0x00058F2C File Offset: 0x0005712C
			internal PAAEvents(BaseUMCallSession vo)
			{
				this.vo = vo;
			}

			// Token: 0x0600146C RID: 5228 RVA: 0x00058F3B File Offset: 0x0005713B
			public void OnBeginEvaluatingPAA()
			{
				this.startTime = ExDateTime.UtcNow;
			}

			// Token: 0x0600146D RID: 5229 RVA: 0x00058F48 File Offset: 0x00057148
			public void OnEndEvaluatingPAA(PAAEvaluationStatus status, bool subscriberHasConfiguredPAA)
			{
				TimeSpan elapsedTime = ExDateTime.UtcNow - this.startTime;
				this.EvaluatingPAAComplete(status, subscriberHasConfiguredPAA, elapsedTime);
			}

			// Token: 0x0600146E RID: 5230 RVA: 0x00058F70 File Offset: 0x00057170
			public void EvaluatingPAAComplete(PAAEvaluationStatus status, bool subscriberHasConfiguredPAA, TimeSpan elapsedTime)
			{
				if (!subscriberHasConfiguredPAA)
				{
					return;
				}
				this.vo.IncrementCounter(CallAnswerCounters.CallsForSubscribersHavingOneOrMoreCARConfigured);
				switch (status)
				{
				case PAAEvaluationStatus.Success:
					this.vo.IncrementCounter(CallAnswerCounters.TotalCARCalls);
					this.vo.SetCounter(CallAnswerCounters.CAREvaluationAverageTime, PersonalAutoAttendantManager.PAAEvents.averageEvaluationTime.Update((long)elapsedTime.TotalSeconds));
					return;
				case PAAEvaluationStatus.Failure:
					break;
				case PAAEvaluationStatus.Timeout:
					this.vo.IncrementCounter(CallAnswerCounters.CARTimedOutEvaluations);
					break;
				default:
					return;
				}
			}

			// Token: 0x04000C9B RID: 3227
			private static MovingAverage averageEvaluationTime = new MovingAverage(50);

			// Token: 0x04000C9C RID: 3228
			private BaseUMCallSession vo;

			// Token: 0x04000C9D RID: 3229
			private ExDateTime startTime;
		}
	}
}
