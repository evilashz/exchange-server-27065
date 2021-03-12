using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.OCS;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200023E RID: 574
	internal abstract class BaseUMVoipPlatform
	{
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060010C5 RID: 4293
		// (remove) Token: 0x060010C6 RID: 4294
		internal abstract event VoipPlatformEventHandler<SendNotifyMessageCompletedEventArgs> OnSendNotifyMessageCompleted;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060010C7 RID: 4295
		// (remove) Token: 0x060010C8 RID: 4296
		internal abstract event VoipPlatformEventHandler<InfoMessage.PlatformMessageReceivedEventArgs> OnMessageReceived;

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x0004B338 File Offset: 0x00049538
		// (set) Token: 0x060010CA RID: 4298 RVA: 0x0004B340 File Offset: 0x00049540
		internal UserToCallsMap UsersPhoneCalls { get; private set; }

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x0004B349 File Offset: 0x00049549
		// (set) Token: 0x060010CC RID: 4300 RVA: 0x0004B351 File Offset: 0x00049551
		internal Hashtable CallSessionHashTable { get; private set; }

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x0004B35A File Offset: 0x0004955A
		// (set) Token: 0x060010CE RID: 4302 RVA: 0x0004B362 File Offset: 0x00049562
		internal CallInfoCache DisconnectedOutboundCalls { get; private set; }

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x0004B36B File Offset: 0x0004956B
		// (set) Token: 0x060010D0 RID: 4304 RVA: 0x0004B373 File Offset: 0x00049573
		private protected bool ShutdownInProgress { protected get; private set; }

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x0004B37C File Offset: 0x0004957C
		// (set) Token: 0x060010D2 RID: 4306 RVA: 0x0004B384 File Offset: 0x00049584
		private protected UMCallSessionHandler<EventArgs> CallHandler { protected get; private set; }

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x0004B38D File Offset: 0x0004958D
		// (set) Token: 0x060010D4 RID: 4308 RVA: 0x0004B395 File Offset: 0x00049595
		private protected int SipPort { protected get; private set; }

		// Token: 0x060010D5 RID: 4309
		internal abstract void Start();

		// Token: 0x060010D6 RID: 4310
		internal abstract void SendNotifyMessageAsync(PlatformSipUri sipUri, UMSipPeer nextHop, System.Net.Mime.ContentType contentType, byte[] body, string eventHeader, IList<PlatformSignalingHeader> headers, object asyncState);

		// Token: 0x060010D7 RID: 4311 RVA: 0x0004B3A0 File Offset: 0x000495A0
		internal void CreateAndMakeCallOnDependentSession(BaseUMCallSession parentCallSession, UMCallSessionHandler<OutboundCallDetailsEventArgs> onoutboundCallRequestCompleted, UMSubscriber caller, string callerIdToUse, PhoneNumber numberToCall, out BaseUMCallSession outBoundCallSession)
		{
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				outBoundCallSession = parentCallSession.CreateDependentSession(onoutboundCallRequestCompleted, caller, callerIdToUse, numberToCall);
				disposeGuard.Add<BaseUMCallSession>(outBoundCallSession);
				this.RegisterCall(outBoundCallSession);
				outBoundCallSession.MakeNewDependentSessionCall();
				disposeGuard.Success();
			}
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0004B408 File Offset: 0x00049608
		internal virtual void Initialize(int sipPort, UMCallSessionHandler<EventArgs> callHandler)
		{
			this.isRetired = false;
			ExWatson.Init();
			this.CallSessionHashTable = new Hashtable();
			this.CallHandler = callHandler;
			this.SipPort = sipPort;
			this.activeCallsEnded = new ManualResetEvent(false);
			this.DisconnectedOutboundCalls = new CallInfoCache();
			this.UsersPhoneCalls = new UserToCallsMap();
			SipNotifyMwiTarget.Initialize();
			UserNotificationEvent.Initialize();
			SipPeerManager.Instance.SipPeerListChanged += this.SipPeerListChanged;
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0004B480 File Offset: 0x00049680
		internal virtual void Shutdown()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoipPlatformTracer, null, "In platform shutdown", new object[0]);
			this.ShutdownInProgress = true;
			Hashtable hashtable;
			lock (this.CallSessionHashTable.SyncRoot)
			{
				hashtable = (Hashtable)this.CallSessionHashTable.Clone();
			}
			foreach (object obj in hashtable.Values)
			{
				BaseUMCallSession baseUMCallSession = (BaseUMCallSession)obj;
				baseUMCallSession.DisconnectCall();
			}
			lock (this.CallSessionHashTable.SyncRoot)
			{
				this.SignalWaitingThreadIfFinalCallEnded();
			}
			this.WaitForActiveCallsToEnd(UmServiceGlobals.ComponentStoptime);
			SipNotifyMwiTarget.Uninitialize();
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0004B584 File Offset: 0x00049784
		internal UMCallInfoEx GetCallInfo(Guid sessionGuid)
		{
			UMCallInfoEx umcallInfoEx = null;
			try
			{
				BaseUMCallSession baseUMCallSession = this.FindSession(sessionGuid);
				if (baseUMCallSession != null)
				{
					lock (baseUMCallSession)
					{
						CallContext currentCallContext = baseUMCallSession.CurrentCallContext;
						if (currentCallContext != null)
						{
							umcallInfoEx = new UMCallInfoEx();
							umcallInfoEx.CallState = baseUMCallSession.State;
							umcallInfoEx.EndResult = currentCallContext.WebServiceRequest.EndResult;
						}
					}
				}
			}
			catch (ObjectDisposedException)
			{
				umcallInfoEx = null;
			}
			if (umcallInfoEx == null)
			{
				umcallInfoEx = this.DisconnectedOutboundCalls[sessionGuid];
			}
			return umcallInfoEx;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0004B61C File Offset: 0x0004981C
		internal void CloseSession(Guid sessionGuid)
		{
			BaseUMCallSession session = this.FindSession(sessionGuid);
			this.CloseSession(session);
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0004B638 File Offset: 0x00049838
		internal void Retire(BaseUMVoipPlatform.FinalCallEndedDelegate finalCallEndedDelegate)
		{
			Utils.ThreadPoolQueueUserWorkItem(new WaitCallback(this.InternalRetire), finalCallEndedDelegate);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0004B64C File Offset: 0x0004984C
		internal BaseUMCallSession FindSession(Guid sessionGuid)
		{
			BaseUMCallSession result = null;
			lock (this.CallSessionHashTable.SyncRoot)
			{
				result = (BaseUMCallSession)this.CallSessionHashTable[sessionGuid];
			}
			return result;
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0004B6A8 File Offset: 0x000498A8
		internal BaseUMCallSession MakeCallForUser(UMSubscriber caller, string calledParty, UMSipPeer outboundProxy, CallContext context, UMCallSessionHandler<OutboundCallDetailsEventArgs> onoutboundCallRequestCompleted)
		{
			BaseUMCallSession result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				BaseUMCallSession baseUMCallSession = this.CreateOutboundCallSession(context, onoutboundCallRequestCompleted);
				disposeGuard.Add<BaseUMCallSession>(baseUMCallSession);
				OutCallingHandlerForUser outCallingHandlerForUser = new OutCallingHandlerForUser(caller, baseUMCallSession, outboundProxy, TypeOfOutboundCall.PlayOnPhone);
				outCallingHandlerForUser.MakeCall(caller.OutboundCallingLineId, calledParty, null);
				disposeGuard.Success();
				result = baseUMCallSession;
			}
			return result;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0004B714 File Offset: 0x00049914
		internal BaseUMCallSession MakeCallForAA(UMAutoAttendant caller, string calledParty, UMSipPeer outboundProxy, CallContext context, UMCallSessionHandler<OutboundCallDetailsEventArgs> onOutboundCallRequestCompleted)
		{
			BaseUMCallSession result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				BaseUMCallSession baseUMCallSession = this.CreateOutboundCallSession(context, onOutboundCallRequestCompleted);
				disposeGuard.Add<BaseUMCallSession>(baseUMCallSession);
				OutCallingHandlerForAA outCallingHandlerForAA = new OutCallingHandlerForAA(caller, baseUMCallSession, outboundProxy);
				outCallingHandlerForAA.MakeCall(calledParty, null);
				disposeGuard.Success();
				result = baseUMCallSession;
			}
			return result;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0004B778 File Offset: 0x00049978
		internal void UnRegisterCall(BaseUMCallSession callSession)
		{
			lock (this.CallSessionHashTable.SyncRoot)
			{
				if (this.CallSessionHashTable.Contains(callSession.SessionGuid))
				{
					this.CallSessionHashTable.Remove(callSession.SessionGuid);
					this.SignalWaitingThreadIfFinalCallEnded();
				}
			}
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0004B7EC File Offset: 0x000499EC
		internal void HandleCallDisposed(BaseUMCallSession callSession)
		{
			this.UsersPhoneCalls.RemovePhoneCall(callSession.PlayOnPhoneSMTPAddress, callSession.SessionGuid);
			CallType callType = (callSession.CurrentCallContext == null) ? 0 : callSession.CurrentCallContext.CallType;
			if ((callType == 5 || callType == 10) && this.DisconnectedOutboundCalls[callSession.SessionGuid] == null)
			{
				this.AddToExpiredPOPCallCache(callSession);
			}
		}

		// Token: 0x060010E2 RID: 4322
		protected abstract BaseUMCallSession InternalCreateOutboundCallSession(CallContext context, UMCallSessionHandler<OutboundCallDetailsEventArgs> handler, UMVoIPSecurityType security);

		// Token: 0x060010E3 RID: 4323
		protected abstract void SipPeerListChanged(object sender, EventArgs args);

		// Token: 0x060010E4 RID: 4324 RVA: 0x0004B84C File Offset: 0x00049A4C
		protected BaseUMCallSession CreateOutboundCallSession(CallContext context, UMCallSessionHandler<OutboundCallDetailsEventArgs> handler)
		{
			BaseUMCallSession baseUMCallSession = this.InternalCreateOutboundCallSession(context, handler, context.DialPlan.VoIPSecurity);
			this.RegisterCall(baseUMCallSession);
			baseUMCallSession.OnOutboundCallRequestCompleted += handler;
			return baseUMCallSession;
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0004B87C File Offset: 0x00049A7C
		protected void RegisterAndHandleCall(BaseUMCallSession callSession, IList<PlatformSignalingHeader> headers)
		{
			this.RegisterCall(callSession);
			callSession.HandleIncomingCall(headers);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0004B88C File Offset: 0x00049A8C
		protected void RegisterCall(BaseUMCallSession callSession)
		{
			callSession.OnCallConnected += this.CallHandler.Invoke;
			lock (this.CallSessionHashTable.SyncRoot)
			{
				this.CallSessionHashTable.Add(callSession.SessionGuid, callSession);
			}
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0004B8FC File Offset: 0x00049AFC
		protected void Fire<TArgs>(VoipPlatformEventHandler<TArgs> handler, TArgs args) where TArgs : EventArgs
		{
			if (handler != null)
			{
				handler(this, args);
			}
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0004B90C File Offset: 0x00049B0C
		private void InternalRetire(object state)
		{
			BaseUMVoipPlatform.FinalCallEndedDelegate finalCallEndedDelegate = (BaseUMVoipPlatform.FinalCallEndedDelegate)state;
			lock (this.CallSessionHashTable.SyncRoot)
			{
				this.isRetired = true;
				this.SignalWaitingThreadIfFinalCallEnded();
			}
			this.WaitForActiveCallsToEnd();
			finalCallEndedDelegate();
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0004B96C File Offset: 0x00049B6C
		private void CloseSession(BaseUMCallSession session)
		{
			try
			{
				if (session != null)
				{
					session.CloseSession();
				}
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0004B998 File Offset: 0x00049B98
		private void AddToExpiredPOPCallCache(BaseUMCallSession callSession)
		{
			UMCallInfoEx umcallInfoEx = new UMCallInfoEx();
			umcallInfoEx.CallState = UMCallState.Disconnected;
			if (callSession.CurrentCallContext.WebServiceRequest.EndResult != UMOperationResult.Failure)
			{
				umcallInfoEx.EndResult = UMOperationResult.Success;
			}
			else
			{
				umcallInfoEx.EndResult = callSession.CurrentCallContext.WebServiceRequest.EndResult;
			}
			this.DisconnectedOutboundCalls[callSession.SessionGuid] = umcallInfoEx;
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0004B9F5 File Offset: 0x00049BF5
		private void WaitForActiveCallsToEnd()
		{
			this.WaitForActiveCallsToEnd(TimeSpan.FromMilliseconds(-1.0));
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0004BA0C File Offset: 0x00049C0C
		private void WaitForActiveCallsToEnd(TimeSpan timeout)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStopTracer, null, "Wait for all active calls to end...", new object[0]);
			if (this.activeCallsEnded.WaitOne(timeout))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStopTracer, null, "All active calls have ended", new object[0]);
				return;
			}
			CallIdTracer.TraceWarning(ExTraceGlobals.ServiceStopTracer, null, "Waiting for active calls has timed out after waiting for {0}", new object[]
			{
				timeout
			});
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0004BA78 File Offset: 0x00049C78
		private void SignalWaitingThreadIfFinalCallEnded()
		{
			if (this.CallSessionHashTable.Count == 0 && (this.isRetired || this.ShutdownInProgress))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStopTracer, null, "Last call has ended when the worker process has been asked to go down", new object[0]);
				this.activeCallsEnded.Set();
			}
		}

		// Token: 0x04000BA3 RID: 2979
		private bool isRetired;

		// Token: 0x04000BA4 RID: 2980
		private ManualResetEvent activeCallsEnded;

		// Token: 0x0200023F RID: 575
		// (Invoke) Token: 0x060010F0 RID: 4336
		internal delegate void FinalCallEndedDelegate();
	}
}
