using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000072 RID: 114
	internal abstract class BaseUMCallSession : DisposableBase
	{
		// Token: 0x060004C8 RID: 1224 RVA: 0x00017510 File Offset: 0x00015710
		internal BaseUMCallSession()
		{
			this.sessionGuid = Guid.NewGuid();
			this.taskCallType = CommonConstants.TaskCallType.Voice;
			this.appState = CommonConstants.ApplicationState.Idle;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060004C9 RID: 1225
		// (remove) Token: 0x060004CA RID: 1226
		internal abstract event UMCallSessionHandler<EventArgs> OnCallConnected;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060004CB RID: 1227
		// (remove) Token: 0x060004CC RID: 1228
		internal abstract event UMCallSessionHandler<OutboundCallDetailsEventArgs> OnOutboundCallRequestCompleted;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060004CD RID: 1229
		// (remove) Token: 0x060004CE RID: 1230
		internal abstract event UMCallSessionHandler<UMCallSessionEventArgs> OnDtmf;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060004CF RID: 1231
		// (remove) Token: 0x060004D0 RID: 1232
		internal abstract event UMCallSessionHandler<UMCallSessionEventArgs> OnHangup;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060004D1 RID: 1233
		// (remove) Token: 0x060004D2 RID: 1234
		internal abstract event UMCallSessionHandler<UMCallSessionEventArgs> OnStateInfoSent;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060004D3 RID: 1235
		// (remove) Token: 0x060004D4 RID: 1236
		internal abstract event UMCallSessionHandler<UMCallSessionEventArgs> OnComplete;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060004D5 RID: 1237
		// (remove) Token: 0x060004D6 RID: 1238
		internal abstract event UMCallSessionHandler<UMCallSessionEventArgs> OnTimeout;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060004D7 RID: 1239
		// (remove) Token: 0x060004D8 RID: 1240
		internal abstract event UMCallSessionHandler<UMCallSessionEventArgs> OnTransferComplete;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060004D9 RID: 1241
		// (remove) Token: 0x060004DA RID: 1242
		internal abstract event UMCallSessionHandler<UMCallSessionEventArgs> OnHold;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060004DB RID: 1243
		// (remove) Token: 0x060004DC RID: 1244
		internal abstract event UMCallSessionHandler<UMCallSessionEventArgs> OnResume;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060004DD RID: 1245
		// (remove) Token: 0x060004DE RID: 1246
		internal abstract event UMCallSessionHandler<UMCallSessionEventArgs> OnError;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060004DF RID: 1247
		// (remove) Token: 0x060004E0 RID: 1248
		internal abstract event UMCallSessionHandler<UMCallSessionEventArgs> OnFaxRequestReceive;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060004E1 RID: 1249
		// (remove) Token: 0x060004E2 RID: 1250
		internal abstract event UMCallSessionHandler<UMCallSessionEventArgs> OnCancelled;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060004E3 RID: 1251
		// (remove) Token: 0x060004E4 RID: 1252
		internal abstract event UMCallSessionHandler<HeavyBlockingOperationEventArgs> OnHeavyBlockingOperation;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060004E5 RID: 1253
		// (remove) Token: 0x060004E6 RID: 1254
		internal abstract event UMCallSessionHandler<InfoMessage.MessageReceivedEventArgs> OnMessageReceived;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060004E7 RID: 1255
		// (remove) Token: 0x060004E8 RID: 1256
		internal abstract event UMCallSessionHandler<EventArgs> OnMessageSent;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060004E9 RID: 1257
		// (remove) Token: 0x060004EA RID: 1258
		internal abstract event UMCallSessionHandler<EventArgs> OnDispose;

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00017547 File Offset: 0x00015747
		internal static MovingAverage AverageUserResponseLatency
		{
			get
			{
				return BaseUMCallSession.averageUserResponseLatency;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0001754E File Offset: 0x0001574E
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x00017556 File Offset: 0x00015756
		internal string CallId
		{
			get
			{
				return this.callId;
			}
			set
			{
				this.callId = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x0001755F File Offset: 0x0001575F
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x00017567 File Offset: 0x00015767
		internal CommonConstants.ApplicationState AppState
		{
			get
			{
				return this.appState;
			}
			set
			{
				this.appState = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00017570 File Offset: 0x00015770
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x00017578 File Offset: 0x00015778
		internal CommonConstants.TaskCallType TaskCallType
		{
			get
			{
				return this.taskCallType;
			}
			set
			{
				this.taskCallType = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00017581 File Offset: 0x00015781
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x00017589 File Offset: 0x00015789
		internal CallContext CurrentCallContext
		{
			get
			{
				return this.currentCallContext;
			}
			set
			{
				this.currentCallContext = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00017592 File Offset: 0x00015792
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x0001759A File Offset: 0x0001579A
		internal DependentSessionDetails DependentSessionDetails
		{
			get
			{
				return this.dependentSessionDetails;
			}
			set
			{
				this.dependentSessionDetails = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x000175A3 File Offset: 0x000157A3
		internal Guid SessionGuid
		{
			get
			{
				return this.sessionGuid;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x000175AB File Offset: 0x000157AB
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x000175B3 File Offset: 0x000157B3
		internal string PlayOnPhoneSMTPAddress
		{
			get
			{
				return this.playOnPhoneSMTPAddress;
			}
			set
			{
				this.playOnPhoneSMTPAddress = value;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x000175BC File Offset: 0x000157BC
		internal string ToTag
		{
			get
			{
				return this.CurrentCallContext.CallInfo.ToTag;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x000175CE File Offset: 0x000157CE
		internal ReadOnlyCollection<PlatformSignalingHeader> RemoteHeaders
		{
			get
			{
				return this.CurrentCallContext.CallInfo.RemoteHeaders;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x000175E0 File Offset: 0x000157E0
		internal string FromTag
		{
			get
			{
				return this.CurrentCallContext.CallInfo.FromTag;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x000175F4 File Offset: 0x000157F4
		internal bool WaitForSourcePartyInfo
		{
			get
			{
				return this.CurrentCallContext.GatewayConfig.DelayedSourcePartyInfoEnabled && this.CurrentCallContext.IsAnonymousCaller && this.CurrentCallContext.DialPlan.URIType == UMUriType.TelExtn && string.IsNullOrEmpty(this.CurrentCallContext.Extension);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004FD RID: 1277
		internal abstract UMCallState State { get; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004FE RID: 1278
		internal abstract bool IsClosing { get; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004FF RID: 1279
		internal abstract string CallLegId { get; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000500 RID: 1280
		protected abstract bool IsDependentSession { get; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000501 RID: 1281
		internal abstract IUMSpeechRecognizer SpeechRecognizer { get; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000502 RID: 1282
		internal abstract UMLoggingManager LoggingManager { get; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00017645 File Offset: 0x00015845
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x0001764D File Offset: 0x0001584D
		protected X509Certificate RemoteCertificate { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00017656 File Offset: 0x00015856
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x0001765E File Offset: 0x0001585E
		protected string RemoteMatchedFQDN { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00017667 File Offset: 0x00015867
		protected List<BaseUMAsyncTimer> Timers
		{
			get
			{
				return this.timerList;
			}
		}

		// Token: 0x06000508 RID: 1288
		public abstract void RebufferDigits(byte[] dtmfDigits);

		// Token: 0x06000509 RID: 1289
		internal abstract BaseUMAsyncTimer StartTimer(BaseUMAsyncTimer.UMTimerCallback callback, int dueTime);

		// Token: 0x0600050A RID: 1290
		internal abstract void TransferAsync();

		// Token: 0x0600050B RID: 1291
		internal abstract void TransferAsync(string phoneNumber);

		// Token: 0x0600050C RID: 1292
		internal abstract void TransferAsync(PlatformSipUri target, IList<PlatformSignalingHeader> headers);

		// Token: 0x0600050D RID: 1293
		internal abstract void TransferAsync(PlatformSipUri target);

		// Token: 0x0600050E RID: 1294
		internal abstract void CancelPendingOperations();

		// Token: 0x0600050F RID: 1295 RVA: 0x0001766F File Offset: 0x0001586F
		internal void DisconnectCall()
		{
			this.DisconnectCall(null);
		}

		// Token: 0x06000510 RID: 1296
		internal abstract void DisconnectCall(PlatformSignalingHeader diagnosticHeader);

		// Token: 0x06000511 RID: 1297
		internal abstract void CloseSession();

		// Token: 0x06000512 RID: 1298
		internal abstract void Redirect(string host, int port, int code);

		// Token: 0x06000513 RID: 1299
		internal abstract void PlayPrompts(ArrayList prompts, int minDigits, int maxDigits, int timeout, string stopTones, int interDigitTimeout, StopPatterns stopPatterns, int startIdx, TimeSpan offset, bool stopPromptOnBargeIn, string turnName, int initalSilenceTimeout);

		// Token: 0x06000514 RID: 1300
		internal abstract void PlayUninterruptiblePrompts(ArrayList prompts);

		// Token: 0x06000515 RID: 1301
		internal abstract void RecordFile(string fileName, int maxSilence, int maxSeconds, string stopTones, bool append, ArrayList comfortPrompts);

		// Token: 0x06000516 RID: 1302
		internal abstract void SendStateInfo(string callId, string state);

		// Token: 0x06000517 RID: 1303
		internal abstract void ClearDigits(int sleepMsec);

		// Token: 0x06000518 RID: 1304
		internal abstract void AcceptFax();

		// Token: 0x06000519 RID: 1305
		internal abstract void RunHeavyBlockingOperation(IUMHeavyBlockingOperation operation, ArrayList prompts);

		// Token: 0x0600051A RID: 1306
		internal abstract void SendMessage(InfoMessage message);

		// Token: 0x0600051B RID: 1307
		internal abstract void SendDtmf(string dtmfSequence, TimeSpan initialSilence);

		// Token: 0x0600051C RID: 1308
		internal abstract bool IsDuringPlayback();

		// Token: 0x0600051D RID: 1309
		internal abstract void StopPlayback();

		// Token: 0x0600051E RID: 1310
		internal abstract void StopPlaybackAndCancelRecognition();

		// Token: 0x0600051F RID: 1311
		internal abstract void Skip(TimeSpan timeToSkip);

		// Token: 0x06000520 RID: 1312
		internal abstract void InitializeConnectedCall(OutboundCallDetailsEventArgs args);

		// Token: 0x06000521 RID: 1313 RVA: 0x00017678 File Offset: 0x00015878
		internal void PlayPrompts(ArrayList prompts, int minDigits, int maxDigits, int timeout, string stopTones, int interDigitTimeout, bool stopPromptOnBargeIn, string turnName, int initialSilenceTimeout)
		{
			this.PlayPrompts(prompts, minDigits, maxDigits, timeout, stopTones, interDigitTimeout, StopPatterns.Empty, 0, TimeSpan.Zero, stopPromptOnBargeIn, turnName, initialSilenceTimeout);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000176A8 File Offset: 0x000158A8
		internal void PlayPrompts(ArrayList prompts, int minDigits, int maxDigits, int timeout, string stopTones, int interDigitTimeout, StopPatterns stopPatterns, bool stopPromptOnBargeIn, string turnName, int initialSilenceTimeout)
		{
			this.PlayPrompts(prompts, minDigits, maxDigits, timeout, stopTones, interDigitTimeout, stopPatterns, 0, TimeSpan.Zero, stopPromptOnBargeIn, turnName, initialSilenceTimeout);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000176D4 File Offset: 0x000158D4
		internal void PlayPrompts(ArrayList prompts, int minDigits, int maxDigits, int timeout, string stopTones, int interDigitTimeout, int startIdx, TimeSpan offset, bool stopPromptOnBargeIn, string turnName, int initialSilenceTimeout)
		{
			this.PlayPrompts(prompts, minDigits, maxDigits, timeout, stopTones, interDigitTimeout, StopPatterns.Empty, startIdx, offset, stopPromptOnBargeIn, turnName, initialSilenceTimeout);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000176FF File Offset: 0x000158FF
		internal void HandleFailedOutboundCall(OutboundCallDetailsEventArgs eventArgs)
		{
			this.CurrentCallContext.ReasonForDisconnect = DropCallReason.OutboundFailedCall;
			UmServiceGlobals.VoipPlatform.DisconnectedOutboundCalls[this.SessionGuid] = eventArgs.CallInfoEx;
			this.DisconnectCall();
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00017730 File Offset: 0x00015930
		internal void HandleIncomingCall(IList<PlatformSignalingHeader> headers)
		{
			this.CurrentCallContext.OnSessionReceived(headers);
			if (this.CurrentCallContext.OfferResult == OfferResult.Redirect)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "HandleIncomingCall: Call will be redirected, CallId = {0}", new object[]
				{
					this.CallId
				});
				this.RedirectCallToDifferentServer();
				return;
			}
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CallReceived, null, new object[]
			{
				CommonUtil.ToEventLogString(this.CurrentCallContext.CallerId.ToDial),
				CommonUtil.ToEventLogString(this.CurrentCallContext.Extension),
				this.CallId
			});
			this.LoggingManager.LogApplicationInformation("HandleIncomingCall: CallId={0}, CalleeInfo={1}, CallerInfo={2}", new object[]
			{
				this.CallLegId,
				(this.CurrentCallContext.CalleeInfo != null) ? this.CurrentCallContext.CalleeInfo.DisplayName : this.CurrentCallContext.CalleeId.ToDial,
				(this.CurrentCallContext.CallerInfo != null) ? this.CurrentCallContext.CallerInfo.DisplayName : this.CurrentCallContext.CallerId.ToDial
			});
			this.CurrentCallContext.OfferResult = OfferResult.Answer;
			this.SetContactUriForAccept();
			this.InternalAcceptCall();
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001786C File Offset: 0x00015A6C
		internal void OpenAsync(PlatformSipUri toAddress, PlatformSipUri fromAddress, UMSipPeer outboundProxy, IList<PlatformSignalingHeader> headers)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this.GetHashCode(), "toaddr {0} fromaddr {1} proxy {2}", new object[]
			{
				toAddress,
				fromAddress,
				outboundProxy
			});
			BaseUMCallSession.OutboundCallInfo info = new BaseUMCallSession.OutboundCallInfo
			{
				CalledParty = toAddress.ToString(),
				CallingParty = fromAddress.ToString(),
				Gateway = outboundProxy
			};
			this.IncrementCounter(AvailabilityCounters.TotalWorkerProcessCallCount);
			this.InternalOpenAsync(info, headers);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000178E8 File Offset: 0x00015AE8
		internal BaseUMCallSession CreateDependentSession(UMCallSessionHandler<OutboundCallDetailsEventArgs> onoutboundCallRequestCompleted, UMSubscriber caller, string callerId, PhoneNumber numberToCall)
		{
			ExAssert.RetailAssert(!this.IsDependentSession, "CreateDependentSession called on dependent session!");
			PIIMessage[] data = new PIIMessage[]
			{
				PIIMessage.Create(PIIType._Caller, callerId),
				PIIMessage.Create(PIIType._PhoneNumber, numberToCall.ToDial)
			};
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this.GetHashCode(), data, "CreateDependentSession . Call-ID: {0}, caller-id=_Caller, numberToCall=_PhoneNumber", new object[]
			{
				this.CallId
			});
			UMIPGatewaySipPeer umipgatewaySipPeer = new UMIPGatewaySipPeer(this.CurrentCallContext.GatewayConfig, this.CurrentCallContext.DialPlan);
			CallContext callContext = new CallContext();
			callContext.DialPlan = this.CurrentCallContext.DialPlan;
			callContext.GatewayConfig = this.CurrentCallContext.GatewayConfig;
			callContext.CallLoggingHelper.ParentCallIdentifier = this.CurrentCallContext.CallId;
			if (umipgatewaySipPeer.UseMutualTLS && umipgatewaySipPeer.Address.IsIPAddress && !string.IsNullOrEmpty(this.RemoteMatchedFQDN))
			{
				umipgatewaySipPeer.Address = new UMSmartHost(this.RemoteMatchedFQDN);
			}
			if (!this.CurrentCallContext.RoutingHelper.SupportsMsOrganizationRouting && SipRoutingHelper.UseGlobalSBCSettingsForOutbound(this.CurrentCallContext.GatewayConfig))
			{
				umipgatewaySipPeer.NextHopForOutboundRouting = SipPeerManager.Instance.SBCService;
			}
			this.DisposeDependentSession();
			this.DependentSessionDetails = new DependentSessionDetails(onoutboundCallRequestCompleted, caller, callerId, umipgatewaySipPeer, numberToCall, this);
			BaseUMCallSession baseUMCallSession = this.InternalCreateDependentSession(this.DependentSessionDetails, callContext);
			this.DependentSessionDetails.DependentUMCallSession = baseUMCallSession;
			return baseUMCallSession;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00017A54 File Offset: 0x00015C54
		internal void DisconnectDependentUMCallSession()
		{
			ExAssert.RetailAssert(!this.IsDependentSession, "DisconnectDependentSession called on dependent session");
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this.GetHashCode(), "DisconnectDependentUMCallSession . Call-ID: {0}", new object[]
			{
				this.CallId
			});
			this.DependentSessionDetails.DependentUMCallSession.DisconnectCall();
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00017AB0 File Offset: 0x00015CB0
		internal void TerminateDependentSession()
		{
			ExAssert.RetailAssert(!this.IsDependentSession, "TerminateDependentSession called on dependent session");
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this.GetHashCode(), "TerminateDependentSession . Call-ID: {0}", new object[]
			{
				this.CallId
			});
			try
			{
				this.DependentSessionDetails.DependentUMCallSession.CloseSession();
			}
			catch (InvalidOperationException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this.GetHashCode(), "TerminateDependentSession . Call-ID: {0}. The call is already disconnected. Exception={1}", new object[]
				{
					this.CallId,
					ex.ToString()
				});
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00017B54 File Offset: 0x00015D54
		internal void MakeNewDependentSessionCall()
		{
			ExAssert.RetailAssert(this.IsDependentSession, "MakeNewDependentSessionCall called on non-dependent session");
			IList<PlatformSignalingHeader> list = new List<PlatformSignalingHeader>();
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this.GetHashCode(), "MakeCallOnDependentSession fired.", new object[0]);
			list.Add(Platform.Builder.CreateSignalingHeader("X-MSUM-Originating-Session-Call-Id", this.DependentSessionDetails.ParentUMCallSession.CallLegId));
			if (this.DependentSessionDetails.Caller.DialPlan.URIType == UMUriType.SipName)
			{
				string str = this.DependentSessionDetails.Caller.IsVirtualNumberEnabled ? this.DependentSessionDetails.Caller.VirtualNumber.Trim() : this.DependentSessionDetails.Caller.Extension.Trim();
				PlatformSipUri platformSipUri = Platform.Builder.CreateSipUri("SIP:" + str);
				list.Add(Platform.Builder.CreateSignalingHeader("Referred-By", "<" + platformSipUri.ToString() + ">"));
			}
			OutCallingHandlerForUser outCallingHandlerForUser = new OutCallingHandlerForUser(this.DependentSessionDetails.Caller, this, this.DependentSessionDetails.RemotePeerToUse, TypeOfOutboundCall.FindMe);
			outCallingHandlerForUser.MakeCall(this.DependentSessionDetails.CallerID, this.DependentSessionDetails.NumberToCall.ToDial, list);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00017C94 File Offset: 0x00015E94
		internal void BlindTransferToPhone(PhoneNumber phone, ContactInfo contactInfo)
		{
			this.SetStateForTransfer("BlindTransfer", phone, contactInfo);
			BlindTransferToPhone blindTransferToPhone = new BlindTransferToPhone(this, this.CurrentCallContext, phone);
			blindTransferToPhone.Transfer();
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00017CC4 File Offset: 0x00015EC4
		internal void BlindTransferToHost(PhoneNumber phone, PlatformSipUri referredByUri)
		{
			this.SetStateForTransfer("BlindTransferToHost", phone, null);
			BlindTransferToHost blindTransferToHost = new BlindTransferToHost(this, this.CurrentCallContext, phone, referredByUri);
			blindTransferToHost.Transfer();
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00017CF4 File Offset: 0x00015EF4
		internal void SupervisedTransfer()
		{
			this.SetStateForTransfer("SupervisedTransfer", this.DependentSessionDetails.NumberToCall, null);
			SupervisedTransfer supervisedTransfer = new SupervisedTransfer(this, this.CurrentCallContext, this.DependentSessionDetails.NumberToCall, this.DependentSessionDetails.Caller);
			supervisedTransfer.Transfer();
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00017D41 File Offset: 0x00015F41
		internal void IncrementCounter(ExPerformanceCounter counter)
		{
			this.IncrementCounter(counter, 1L);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00017D4C File Offset: 0x00015F4C
		internal void IncrementCounter(ExPerformanceCounter counter, long count)
		{
			if (this.CurrentCallContext != null)
			{
				this.CurrentCallContext.IncrementCounter(counter, count);
				return;
			}
			Util.IncrementCounter(counter, count);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00017D6B File Offset: 0x00015F6B
		internal void DecrementCounter(ExPerformanceCounter counter)
		{
			if (this.CurrentCallContext != null)
			{
				this.CurrentCallContext.DecrementCounter(counter);
				return;
			}
			Util.DecrementCounter(counter);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00017D88 File Offset: 0x00015F88
		internal void SetCounter(ExPerformanceCounter counter, long value)
		{
			if (this.CurrentCallContext != null)
			{
				this.CurrentCallContext.SetCounter(counter, value);
				return;
			}
			Util.SetCounter(counter, value);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00017DA8 File Offset: 0x00015FA8
		protected static UMEventCause GetUMEventCause(int sipCode)
		{
			if (sipCode == 480)
			{
				return UMEventCause.NoAnswer;
			}
			switch (sipCode)
			{
			case 486:
				return UMEventCause.UserBusy;
			case 487:
				return UMEventCause.None;
			default:
				if (sipCode != 600)
				{
					return UMEventCause.Other;
				}
				return UMEventCause.UserBusy;
			}
		}

		// Token: 0x06000533 RID: 1331
		protected abstract void InternalAcceptCall();

		// Token: 0x06000534 RID: 1332
		protected abstract void SetContactUriForAccept();

		// Token: 0x06000535 RID: 1333
		protected abstract void DeclineCall(PlatformSignalingHeader diagnosticHeader);

		// Token: 0x06000536 RID: 1334
		protected abstract BaseUMCallSession InternalCreateDependentSession(DependentSessionDetails details, CallContext context);

		// Token: 0x06000537 RID: 1335
		protected abstract void InternalOpenAsync(BaseUMCallSession.OutboundCallInfo info, IList<PlatformSignalingHeader> headers);

		// Token: 0x06000538 RID: 1336 RVA: 0x00017DE6 File Offset: 0x00015FE6
		protected virtual void SetStateForTransfer(string transferType, PhoneNumber number, ContactInfo contactInfo)
		{
			if (this.CurrentCallContext != null)
			{
				this.CurrentCallContext.CallLoggingHelper.SetTransferTarget(number, contactInfo);
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00017E02 File Offset: 0x00016002
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "BaseUMCallSession disposing.", new object[0]);
				UmServiceGlobals.VoipPlatform.HandleCallDisposed(this);
				this.DisposeDependentSession();
				this.TeardownCurrentCall();
				UmServiceGlobals.VoipPlatform.UnRegisterCall(this);
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00017E3F File Offset: 0x0001603F
		protected virtual void TeardownCurrentCall()
		{
			TempFileFactory.DisposeSessionFiles(this.CallId);
			this.TeardownTimers();
			this.TeardownContext();
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00017E58 File Offset: 0x00016058
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<BaseUMCallSession>(this);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00017E60 File Offset: 0x00016060
		protected void ConditionalUpdateCallerId(InfoMessage.MessageReceivedEventArgs args)
		{
			if (this.CurrentCallContext.GatewayConfig != null && this.CurrentCallContext.GatewayConfig.DelayedSourcePartyInfoEnabled && this.CurrentCallContext.DialPlan != null && this.CurrentCallContext.DialPlan.URIType == UMUriType.TelExtn)
			{
				this.CurrentCallContext.UpdateCallerInfo(args);
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00017EB8 File Offset: 0x000160B8
		protected void UpdateCallerId(PlatformTelephonyAddress callerAddress)
		{
			this.CurrentCallContext.InitializeCallerId(callerAddress, false);
			if (PhoneNumber.IsNullOrEmpty(this.CurrentCallContext.CallerId))
			{
				this.CurrentCallContext.CallerId = PhoneNumber.Empty;
				return;
			}
			this.CurrentCallContext.ConsumeUpdateForCallerInformation();
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00017EF8 File Offset: 0x000160F8
		protected void LogOutboundCallFailed(BaseUMCallSession.OutboundCallInfo outboundCallInfo, string sipError, string exceptionMessage)
		{
			switch (this.CurrentCallContext.DialPlan.GlobalCallRoutingScheme)
			{
			case UMGlobalCallRoutingScheme.None:
			case UMGlobalCallRoutingScheme.E164:
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_OutboundCallFailed, null, new object[]
				{
					CommonUtil.ToEventLogString(outboundCallInfo.CalledParty),
					CommonUtil.ToEventLogString(outboundCallInfo.Gateway),
					CommonUtil.ToEventLogString(sipError),
					CommonUtil.ToEventLogString(outboundCallInfo.CallingParty),
					CommonUtil.ToEventLogString(exceptionMessage)
				});
				return;
			case UMGlobalCallRoutingScheme.GatewayGuid:
				UmGlobals.ExEvent.LogEvent(this.CurrentCallContext.DialPlan.OrganizationId, UMEventLogConstants.Tuple_OutboundCallFailedForOnPremiseGateway, outboundCallInfo.Gateway.Name, new object[]
				{
					CommonUtil.ToEventLogString(outboundCallInfo.CalledParty),
					CommonUtil.ToEventLogString(outboundCallInfo.Gateway),
					CommonUtil.ToEventLogString(sipError),
					CommonUtil.ToEventLogString(outboundCallInfo.CallingParty),
					CommonUtil.ToEventLogString(exceptionMessage)
				});
				return;
			default:
				throw new InvalidOperationException("Invalid value for DialPlan.GlobalCallRoutingScheme: " + this.CurrentCallContext.DialPlan.GlobalCallRoutingScheme.ToString());
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00018024 File Offset: 0x00016224
		protected string BuildPromptInfoMessageString(string turnName, ArrayList prompts)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			stringBuilder.AppendFormat("Call-Id: {0}\r\nCall-Prompts: \r\n<State>{1}</State>\r\n", this.CallId, turnName);
			for (int i = 0; i < prompts.Count; i++)
			{
				stringBuilder.AppendFormat("<Prompt>{0}</Prompt>\r\n", Uri.EscapeUriString(prompts[i].ToString()));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00018084 File Offset: 0x00016284
		protected PlatformSipUri GetRedirectContactUri(string host, int port)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoipPlatformTracer, this, "Using RoutingHelper:{0}", new object[]
			{
				this.CurrentCallContext.RoutingHelper.GetType().Name
			});
			PlatformSipUri redirectContactUri = RouterUtils.GetRedirectContactUri(this.CurrentCallContext.RequestUriOfCall, this.CurrentCallContext.RoutingHelper, host, port, this.CurrentCallContext.IsSecuredCall ? TransportParameter.Tls : TransportParameter.Tcp, this.CurrentCallContext.TenantGuid.ToString());
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "FormatContactUri: {0}", new object[]
			{
				redirectContactUri
			});
			return redirectContactUri;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00018128 File Offset: 0x00016328
		private static void LogCallRedirected(CallContext cc, string callId, string serverFqdn)
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CallRedirectedToServer, null, new object[]
			{
				cc.ServerPicker.SubscriberLogId,
				serverFqdn,
				callId
			});
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00018164 File Offset: 0x00016364
		private void TeardownContext()
		{
			if (this.currentCallContext != null)
			{
				this.currentCallContext.Dispose();
				this.currentCallContext = null;
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00018180 File Offset: 0x00016380
		private void DisposeDependentSession()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "DisposeDependentSession", new object[0]);
			if (this.DependentSessionDetails != null)
			{
				BaseUMCallSession dependentUMCallSession = this.DependentSessionDetails.DependentUMCallSession;
				if (dependentUMCallSession != null)
				{
					dependentUMCallSession.Dispose();
				}
			}
			this.DependentSessionDetails = null;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x000181C8 File Offset: 0x000163C8
		private void TeardownTimers()
		{
			foreach (BaseUMAsyncTimer baseUMAsyncTimer in this.timerList)
			{
				baseUMAsyncTimer.Dispose();
			}
			this.timerList.Clear();
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00018228 File Offset: 0x00016428
		private void RedirectCallToDifferentServer()
		{
			IRedirectTargetChooser serverPicker = this.CurrentCallContext.ServerPicker;
			ExAssert.RetailAssert(serverPicker != null, "serverPicker is null");
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "MSSCallSession::RedirectCallToDifferentServer() - user/extension '{0}'", new object[]
			{
				serverPicker.SubscriberLogId
			});
			string text = null;
			int port;
			if (!serverPicker.GetTargetServer(out text, out port))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "Did not find a valid server to redirect the call. The call will be rejected", new object[0]);
				serverPicker.HandleServerNotFound();
				this.DeclineCall(CallRejectedException.RenderDiagnosticHeader(CallEndingReason.UserRoutingIssue, null, new object[0]));
				this.DisconnectCall();
				return;
			}
			int redirectResponseCode = this.CurrentCallContext.RoutingHelper.RedirectResponseCode;
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "Redirecting the call to server {0} - code:{1}", new object[]
			{
				text,
				redirectResponseCode
			});
			BaseUMCallSession.LogCallRedirected(this.CurrentCallContext, this.CallId, text);
			this.Redirect(text, port, redirectResponseCode);
			this.DisconnectCall();
		}

		// Token: 0x040001C5 RID: 453
		private static MovingAverage averageUserResponseLatency = new MovingAverage(25);

		// Token: 0x040001C6 RID: 454
		private CommonConstants.ApplicationState appState;

		// Token: 0x040001C7 RID: 455
		private CommonConstants.TaskCallType taskCallType;

		// Token: 0x040001C8 RID: 456
		private string callId = string.Empty;

		// Token: 0x040001C9 RID: 457
		private CallContext currentCallContext;

		// Token: 0x040001CA RID: 458
		private Guid sessionGuid;

		// Token: 0x040001CB RID: 459
		private DependentSessionDetails dependentSessionDetails;

		// Token: 0x040001CC RID: 460
		private string playOnPhoneSMTPAddress;

		// Token: 0x040001CD RID: 461
		private List<BaseUMAsyncTimer> timerList = new List<BaseUMAsyncTimer>();

		// Token: 0x02000073 RID: 115
		internal struct OutboundCallInfo
		{
			// Token: 0x06000547 RID: 1351 RVA: 0x00018327 File Offset: 0x00016527
			internal OutboundCallInfo(string callingParty, string calledParty, UMSipPeer gateway)
			{
				this.Gateway = gateway;
				this.CalledParty = calledParty;
				this.CallingParty = callingParty;
			}

			// Token: 0x040001D0 RID: 464
			internal UMSipPeer Gateway;

			// Token: 0x040001D1 RID: 465
			internal string CalledParty;

			// Token: 0x040001D2 RID: 466
			internal string CallingParty;
		}
	}
}
