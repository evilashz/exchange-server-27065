using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.RpcProxy;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.ExchangeServer;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.RpcProxy
{
	// Token: 0x02000010 RID: 16
	internal class ProxySession
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00007C1C File Offset: 0x00005E1C
		public ProxySession(IRpcInstanceManager manager, byte[] securityContext, uint handle, CreateSessionInfo createInfo)
		{
			this.manager = manager;
			this.securityContext = securityContext;
			this.ProxySessionHandle = handle;
			this.createInfo = createInfo;
			this.instanceSessionHandle = 0U;
			this.instanceGeneration = 0;
			this.instance = null;
			this.state = ProxySession.ProxySessionState.Unbound;
			this.eventQueue = new Queue<ProxySession.ProxySessionEvent>(2);
			this.eventsProcessing = false;
			this.requestInfo = null;
			this.waitQueuedTime = null;
			if (this.createInfo.NotificationPendingCallback == null)
			{
				this.createInfo.NotificationPendingCallback = new Action<ErrorCode, uint>(this.OnNotificationPending);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00007CBC File Offset: 0x00005EBC
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00007CC4 File Offset: 0x00005EC4
		public uint ProxySessionHandle { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00007CD0 File Offset: 0x00005ED0
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00007D0C File Offset: 0x00005F0C
		private ProxySession.ProxySessionState CurrentState
		{
			get
			{
				ProxySession.ProxySessionState result;
				using (LockManager.Lock(this))
				{
					result = this.state;
				}
				return result;
			}
			set
			{
				using (LockManager.Lock(this))
				{
					this.state = value;
				}
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00007D48 File Offset: 0x00005F48
		public ErrorCode QueueDoRpcRequest(uint flags, uint maximumResponseSize, ArraySegment<byte> request, ArraySegment<byte> auxiliaryIn, DoRpcCompleteCallback completionCallback, Action<RpcException> exceptionCallback)
		{
			this.TraceMarkerStart();
			ErrorCode noError;
			try
			{
				using (LockManager.Lock(this))
				{
					if (this.state != ProxySession.ProxySessionState.Unbound && this.state != ProxySession.ProxySessionState.Idle)
					{
						ExTraceGlobals.ProxyMapiTracer.TraceDebug(0L, "Request rejected: session not ready.");
						return ErrorCode.CreateMdbNotInitialized((LID)40824U);
					}
					if (this.requestInfo != null)
					{
						ExTraceGlobals.ProxyMapiTracer.TraceDebug(0L, "Request rejected: another request is outstanding.");
						return ErrorCode.CreateCallFailed((LID)65400U);
					}
					this.requestInfo = new DoRpcRequest?(new DoRpcRequest
					{
						Flags = flags,
						MaximumResponseSize = maximumResponseSize,
						Request = request,
						AuxiliaryIn = auxiliaryIn,
						CompletionCallback = completionCallback,
						ExceptionCallback = exceptionCallback
					});
					this.eventQueue.Enqueue(ProxySession.ProxySessionEvent.RequestReceived);
					ExTraceGlobals.ProxyMapiTracer.TraceDebug(0L, "Request queued for processing.");
				}
				this.ProcessEvents();
				noError = ErrorCode.NoError;
			}
			finally
			{
				this.TraceMarkerEnd();
			}
			return noError;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00007E74 File Offset: 0x00006074
		public void RequestClose(Action onCloseComplete)
		{
			this.TraceMarkerStart();
			try
			{
				bool flag = false;
				bool flag2 = false;
				using (LockManager.Lock(this))
				{
					if (this.state != ProxySession.ProxySessionState.Closed && this.state != ProxySession.ProxySessionState.CloseRequested && this.closeCompleteCallback == null)
					{
						this.closeCompleteCallback = onCloseComplete;
						this.eventQueue.Enqueue(ProxySession.ProxySessionEvent.NeedToClose);
						flag2 = true;
						ExTraceGlobals.ProxyMapiTracer.TraceDebug(0L, "Request to close queued for processing.");
					}
					else
					{
						flag = (onCloseComplete != null);
					}
				}
				if (flag2)
				{
					this.ProcessEvents();
				}
				if (flag)
				{
					ExTraceGlobals.ProxyMapiTracer.TraceDebug(0L, "Running callback inline on closed session.");
					onCloseComplete();
				}
			}
			finally
			{
				this.TraceMarkerEnd();
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00007F34 File Offset: 0x00006134
		public void OnNotificationReceived(Guid instanceId, int generation, ErrorCode errorCode, uint[] sessions)
		{
			uint num = 0U;
			using (LockManager.Lock(this))
			{
				switch (this.state)
				{
				case ProxySession.ProxySessionState.RequestOutstanding:
				case ProxySession.ProxySessionState.Idle:
					if (instanceId == this.instance.Value && this.instanceGeneration == generation)
					{
						num = this.instanceSessionHandle;
					}
					break;
				}
			}
			if (num != 0U && this.createInfo.NotificationPendingCallback != null)
			{
				if (ErrorCode.NoError == errorCode && sessions != null && sessions.Length > 0)
				{
					foreach (uint num2 in sessions)
					{
						if (num == num2)
						{
							this.TraceMarkerStart();
							try
							{
								this.createInfo.NotificationPendingCallback(errorCode, this.ProxySessionHandle);
								return;
							}
							finally
							{
								this.TraceMarkerEnd();
							}
						}
					}
					return;
				}
				if (ErrorCode.NoError != errorCode)
				{
					this.TraceMarkerStart();
					try
					{
						this.createInfo.NotificationPendingCallback(errorCode, this.ProxySessionHandle);
					}
					finally
					{
						this.TraceMarkerEnd();
					}
				}
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00008068 File Offset: 0x00006268
		public bool IsBoundToRpcInstance(Guid instanceId, int generation)
		{
			bool result;
			using (LockManager.Lock(this))
			{
				switch (this.state)
				{
				case ProxySession.ProxySessionState.RequestOutstanding:
				case ProxySession.ProxySessionState.Idle:
					result = (instanceId == this.instance.Value && this.instanceGeneration == generation);
					break;
				default:
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000080DC File Offset: 0x000062DC
		public void QueueNotificationWait(IProxyAsyncWaitCompletion completion)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			bool flag = false;
			this.TraceMarkerStart();
			try
			{
				using (LockManager.Lock(this))
				{
					if (this.pendingCompletion != null)
					{
						errorCode = ErrorCode.CreateRejected((LID)50296U);
						ExTraceGlobals.ProxyMapiTracer.TraceFunction(0L, "Notification rejected: a wait call is already pending");
					}
					else if (this.notificationsPending)
					{
						flag = true;
						this.notificationsPending = false;
						this.waitQueuedTime = null;
					}
					else
					{
						this.pendingCompletion = completion;
						this.notificationsPending = false;
						this.waitQueuedTime = new DateTime?(DateTime.UtcNow);
						completion = null;
						ExTraceGlobals.ProxyMapiTracer.TraceFunction(0L, "Notification wait queued on the session");
					}
				}
				if (completion != null)
				{
					completion.CompleteAsyncCall(flag, (int)errorCode);
					ExTraceGlobals.ProxyMapiTracer.TraceFunction(0L, "Wait call completed inline");
				}
			}
			finally
			{
				this.TraceMarkerEnd();
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000081D0 File Offset: 0x000063D0
		public void ExpireNotificationWait(DateTime currentTime)
		{
			this.TraceMarkerStart();
			try
			{
				IProxyAsyncWaitCompletion proxyAsyncWaitCompletion = null;
				using (LockManager.Lock(this))
				{
					if (this.pendingCompletion != null && this.waitQueuedTime != null && this.waitQueuedTime.Value < currentTime && currentTime.Subtract(this.waitQueuedTime.Value) >= ProxySession.WaitExpirationPeriod)
					{
						proxyAsyncWaitCompletion = this.pendingCompletion;
						this.pendingCompletion = null;
						this.notificationsPending = false;
						this.waitQueuedTime = null;
					}
				}
				if (proxyAsyncWaitCompletion != null)
				{
					proxyAsyncWaitCompletion.CompleteAsyncCall(false, (int)ErrorCode.NoError);
					ExTraceGlobals.ProxyMapiTracer.TraceFunction(0L, "Notification wait expired");
				}
			}
			finally
			{
				this.TraceMarkerEnd();
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000082B0 File Offset: 0x000064B0
		private static bool IsFatalBindingError(ErrorCode ec)
		{
			ErrorCodeValue errorCodeValue = ec;
			if (errorCodeValue <= ErrorCodeValue.Exiting)
			{
				if (errorCodeValue == ErrorCodeValue.NoError)
				{
					return false;
				}
				if (errorCodeValue != ErrorCodeValue.Exiting)
				{
					return true;
				}
			}
			else if (errorCodeValue != ErrorCodeValue.MdbNotInitialized && errorCodeValue != ErrorCodeValue.InvalidPool)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000082F4 File Offset: 0x000064F4
		private void ProcessEvents()
		{
			ProxySession.ProxySessionEvent proxySessionEvent = ProxySession.ProxySessionEvent.None;
			using (LockManager.Lock(this))
			{
				if (this.eventsProcessing)
				{
					return;
				}
				if (this.eventQueue.Count == 0)
				{
					return;
				}
				proxySessionEvent = this.eventQueue.Dequeue();
				this.eventsProcessing = true;
			}
			this.TraceMarkerStart();
			try
			{
				do
				{
					try
					{
						switch (proxySessionEvent)
						{
						case ProxySession.ProxySessionEvent.RequestReceived:
							this.OnRequestReceived();
							break;
						case ProxySession.ProxySessionEvent.BindStepComplete:
							this.OnBindStepComplete();
							break;
						case ProxySession.ProxySessionEvent.RequestComplete:
							this.OnRequestComplete();
							break;
						case ProxySession.ProxySessionEvent.NeedToClose:
							this.OnNeedToClose();
							break;
						}
					}
					finally
					{
						proxySessionEvent = ProxySession.ProxySessionEvent.None;
						using (LockManager.Lock(this))
						{
							if (this.eventQueue.Count > 0)
							{
								proxySessionEvent = this.eventQueue.Dequeue();
							}
							else
							{
								this.eventsProcessing = false;
							}
						}
					}
				}
				while (proxySessionEvent != ProxySession.ProxySessionEvent.None);
			}
			finally
			{
				this.TraceMarkerEnd();
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000840C File Offset: 0x0000660C
		private void OnRequestReceived()
		{
			ProxySession.ProxySessionState currentState = this.CurrentState;
			ProxySession.ProxySessionState proxySessionState = currentState;
			if (proxySessionState != ProxySession.ProxySessionState.Unbound)
			{
				switch (proxySessionState)
				{
				case ProxySession.ProxySessionState.Idle:
					break;
				case ProxySession.ProxySessionState.CloseRequested:
					return;
				case ProxySession.ProxySessionState.Closed:
					goto IL_D0;
				default:
					return;
				}
			}
			ExTraceGlobals.ProxyMapiTracer.TraceDebug(0L, "Request processing started.");
			ErrorCode first = ErrorCode.CreateMdbNotInitialized((LID)57208U);
			if (currentState == ProxySession.ProxySessionState.Unbound)
			{
				this.CurrentState = ProxySession.ProxySessionState.BindOutstanding;
			}
			else
			{
				this.CurrentState = ProxySession.ProxySessionState.RequestOutstanding;
				first = ErrorCode.CreateCallFailed((LID)44920U);
			}
			try
			{
				this.requestExecutionSteps = ((currentState == ProxySession.ProxySessionState.Unbound) ? this.BindToInstance() : this.ExecuteRequest());
				if (this.requestExecutionSteps.MoveNext())
				{
					first = this.requestExecutionSteps.Current;
				}
				return;
			}
			finally
			{
				if (first != ErrorCode.NoError)
				{
					this.CurrentState = ((currentState == ProxySession.ProxySessionState.Unbound) ? ProxySession.ProxySessionState.Closed : currentState);
					if (this.requestExecutionSteps != null)
					{
						this.requestExecutionSteps.Dispose();
						this.requestExecutionSteps = null;
					}
				}
			}
			IL_D0:
			DoRpcRequest? doRpcRequest = null;
			using (LockManager.Lock(this))
			{
				currentState = this.state;
				doRpcRequest = this.requestInfo;
			}
			if (doRpcRequest != null)
			{
				ExTraceGlobals.ProxyMapiTracer.TraceDebug(0L, "Request rejected: session closed.");
				doRpcRequest.Value.CompletionCallback(ErrorCode.CreateMdbNotInitialized((LID)61304U), 0U, RpcServerBase.EmptyArraySegment, RpcServerBase.EmptyArraySegment);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00008578 File Offset: 0x00006778
		private void OnBindStepComplete()
		{
			this.OnStepComplete(ProxySession.ProxySessionState.BindOutstanding, this.CurrentState, ProxySession.ProxySessionState.Closed);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00008588 File Offset: 0x00006788
		private void OnRequestComplete()
		{
			this.OnStepComplete(ProxySession.ProxySessionState.RequestOutstanding, this.CurrentState, ProxySession.ProxySessionState.Idle);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00008598 File Offset: 0x00006798
		private void OnStepComplete(ProxySession.ProxySessionState initialState, ProxySession.ProxySessionState currentState, ProxySession.ProxySessionState rollBackState)
		{
			switch (currentState)
			{
			case ProxySession.ProxySessionState.BindOutstanding:
			case ProxySession.ProxySessionState.RequestOutstanding:
			case ProxySession.ProxySessionState.CloseRequested:
			{
				ErrorCode first = ErrorCode.CreateMdbNotInitialized((LID)36728U);
				try
				{
					if (this.requestExecutionSteps.MoveNext())
					{
						first = this.requestExecutionSteps.Current;
					}
					else
					{
						first = ErrorCode.NoError;
						this.requestExecutionSteps.Dispose();
						this.requestExecutionSteps = null;
					}
				}
				finally
				{
					if (first != ErrorCode.NoError)
					{
						this.CurrentState = ((initialState == currentState) ? rollBackState : ProxySession.ProxySessionState.Closed);
						this.requestExecutionSteps.Dispose();
						this.requestExecutionSteps = null;
					}
					if (currentState == ProxySession.ProxySessionState.CloseRequested)
					{
						this.InternalClose(initialState == ProxySession.ProxySessionState.RequestOutstanding);
					}
				}
				break;
			}
			case ProxySession.ProxySessionState.Idle:
				break;
			default:
				return;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00008654 File Offset: 0x00006854
		private void OnNeedToClose()
		{
			ProxySession.ProxySessionState currentState = this.CurrentState;
			switch (currentState)
			{
			case ProxySession.ProxySessionState.Unbound:
			case ProxySession.ProxySessionState.Idle:
				this.CurrentState = ProxySession.ProxySessionState.Closed;
				this.InternalClose(currentState == ProxySession.ProxySessionState.Idle);
				return;
			case ProxySession.ProxySessionState.BindOutstanding:
			case ProxySession.ProxySessionState.RequestOutstanding:
				this.CurrentState = ProxySession.ProxySessionState.CloseRequested;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000094E4 File Offset: 0x000076E4
		private IEnumerator<ErrorCode> BindToInstance()
		{
			ProxySession.<BindToInstance>d__3 <BindToInstance>d__ = new ProxySession.<BindToInstance>d__3(0);
			<BindToInstance>d__.<>4__this = this;
			return <BindToInstance>d__;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00009B40 File Offset: 0x00007D40
		private IEnumerator<ErrorCode> ExecuteRequest()
		{
			ErrorCode ec = ErrorCode.NoError;
			AsyncCallback stepCompleteCallback = delegate(IAsyncResult ar)
			{
				ProxySession proxySession = (ProxySession)ar.AsyncState;
				using (LockManager.Lock(proxySession))
				{
					proxySession.eventQueue.Enqueue(ProxySession.ProxySessionEvent.RequestComplete);
				}
				proxySession.ProcessEvents();
			};
			RpcInstanceManager.RpcClient<RpcInstancePool> rpcClient = null;
			try
			{
				RpcInstancePool pool = null;
				int generation = this.instanceGeneration;
				rpcClient = this.manager.GetPoolRpcClient(this.instance.Value, ref generation, out pool);
				if (rpcClient == null)
				{
					ExTraceGlobals.ProxyMapiTracer.TraceDebug(0L, "Request rejected: RPC instance closed.");
					ec = ErrorCode.CreateMdbNotInitialized((LID)34680U);
					DoRpcRequest? doRpcRequest = null;
					using (LockManager.Lock(this))
					{
						doRpcRequest = this.requestInfo;
						this.requestInfo = null;
						this.state = ProxySession.ProxySessionState.Idle;
					}
					doRpcRequest.Value.CompletionCallback(ec, 0U, RpcServerBase.EmptyArraySegment, RpcServerBase.EmptyArraySegment);
					yield break;
				}
				DoRpcRequest request = this.requestInfo.Value;
				IRpcAsyncResult ar2;
				try
				{
					ar2 = pool.BeginEcPoolSessionDoRpc(this.instanceSessionHandle, request.Flags, request.MaximumResponseSize, request.Request, request.AuxiliaryIn, stepCompleteCallback, this);
					if (ExTraceGlobals.ProxyMapiTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						StringBuilder stringBuilder = new StringBuilder(100);
						stringBuilder.Append("EcPoolSessionDoRpc sent to worker. ID:[");
						stringBuilder.Append(this.instance.Value);
						stringBuilder.Append("] generation:[");
						stringBuilder.Append(this.instanceGeneration);
						stringBuilder.Append("]");
						ExTraceGlobals.ProxyMapiTracer.TraceDebug(0L, stringBuilder.ToString());
					}
				}
				catch (RpcException ex)
				{
					NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
					ec = ErrorCode.CreateWithLid((LID)61520U, (ErrorCodeValue)ex.ErrorCode);
					DoRpcRequest? doRpcRequest2 = null;
					using (LockManager.Lock(this))
					{
						doRpcRequest2 = this.requestInfo;
						this.requestInfo = null;
						this.state = ProxySession.ProxySessionState.Idle;
					}
					doRpcRequest2.Value.ExceptionCallback(ex);
					yield break;
				}
				yield return ErrorCode.NoError;
				if (ExTraceGlobals.ProxyMapiTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					StringBuilder stringBuilder2 = new StringBuilder(100);
					stringBuilder2.Append("EcPoolSessionDoRpc completed by worker. ID:[");
					stringBuilder2.Append(this.instance.Value);
					stringBuilder2.Append("] generation:[");
					stringBuilder2.Append(this.instanceGeneration);
					stringBuilder2.Append("]");
					ExTraceGlobals.ProxyMapiTracer.TraceDebug(0L, stringBuilder2.ToString());
				}
				try
				{
					uint flags = 0U;
					ArraySegment<byte> emptyArraySegment = RpcServerBase.EmptyArraySegment;
					ArraySegment<byte> emptyArraySegment2 = RpcServerBase.EmptyArraySegment;
					ec = pool.EndEcPoolSessionDoRpc(ar2, out flags, out emptyArraySegment, out emptyArraySegment2);
					DoRpcRequest? doRpcRequest3 = null;
					using (LockManager.Lock(this))
					{
						doRpcRequest3 = this.requestInfo;
						this.requestInfo = null;
						this.state = ProxySession.ProxySessionState.Idle;
					}
					doRpcRequest3.Value.CompletionCallback(ec, flags, emptyArraySegment, emptyArraySegment2);
				}
				catch (RpcException ex2)
				{
					NullExecutionDiagnostics.Instance.OnExceptionCatch(ex2);
					ec = ErrorCode.CreateWithLid((LID)39800U, (ErrorCodeValue)ex2.ErrorCode);
					DoRpcRequest? doRpcRequest4 = null;
					using (LockManager.Lock(this))
					{
						doRpcRequest4 = this.requestInfo;
						this.requestInfo = null;
						this.state = ProxySession.ProxySessionState.Idle;
					}
					doRpcRequest4.Value.ExceptionCallback(ex2);
				}
			}
			finally
			{
				if (rpcClient != null)
				{
					rpcClient.EndCall();
				}
			}
			yield break;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00009C04 File Offset: 0x00007E04
		private void CloseInstanceSession(Guid instance, int generation, uint sessionHandle, Action closeCompleteCallback)
		{
			RpcInstanceManager.RpcClient<RpcInstancePool> rpcClient = null;
			try
			{
				RpcInstancePool rpcInstancePool = null;
				rpcClient = this.manager.GetPoolRpcClient(instance, ref generation, out rpcInstancePool);
				if (rpcClient != null)
				{
					RpcInstanceManager.RpcClient<RpcInstancePool> localRpcClient = rpcClient;
					rpcInstancePool.BeginEcPoolCloseSession(sessionHandle, delegate(object _)
					{
						this.TraceMarkerStart();
						try
						{
							localRpcClient.EndCall();
						}
						catch (RpcException ex2)
						{
							NullExecutionDiagnostics.Instance.OnExceptionCatch(ex2);
							this.TraceRpcException(ex2);
						}
						finally
						{
							if (closeCompleteCallback != null)
							{
								closeCompleteCallback();
							}
							this.TraceMarkerEnd();
						}
					}, null);
					rpcClient = null;
					if (ExTraceGlobals.ProxyMapiTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						StringBuilder stringBuilder = new StringBuilder(100);
						stringBuilder.Append("EcPoolCloseSession sent to worker. ID:[");
						stringBuilder.Append(instance);
						stringBuilder.Append("] generation:[");
						stringBuilder.Append(generation);
						stringBuilder.Append("] instanceHandle:[");
						stringBuilder.Append(sessionHandle);
						stringBuilder.Append("]");
						ExTraceGlobals.ProxyMapiTracer.TraceDebug(0L, stringBuilder.ToString());
					}
				}
			}
			catch (RpcException ex)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
				this.TraceRpcException(ex);
			}
			finally
			{
				if (rpcClient != null)
				{
					rpcClient.EndCall();
					if (closeCompleteCallback != null)
					{
						closeCompleteCallback();
					}
				}
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00009D54 File Offset: 0x00007F54
		private void InternalClose(bool bound)
		{
			try
			{
				if (bound)
				{
					this.CloseInstanceSession(this.instance.Value, this.instanceGeneration, this.instanceSessionHandle, delegate
					{
						if (this.closeCompleteCallback != null)
						{
							this.closeCompleteCallback();
						}
					});
				}
				else if (this.closeCompleteCallback != null)
				{
					this.closeCompleteCallback();
				}
			}
			finally
			{
				this.createInfo.NotificationPendingCallback(ErrorCode.CreateExiting((LID)64776U), this.ProxySessionHandle);
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00009DE4 File Offset: 0x00007FE4
		private void TraceMarkerStart()
		{
			if (ExTraceGlobals.ProxyMapiTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append("ENTER PROXY SESSION session:[");
				stringBuilder.Append(this.ProxySessionHandle);
				stringBuilder.Append("]");
				ExTraceGlobals.ProxyMapiTracer.TraceFunction(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00009E40 File Offset: 0x00008040
		private void TraceMarkerEnd()
		{
			if (ExTraceGlobals.ProxyMapiTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append("EXIT PROXY SESSION session:[");
				stringBuilder.Append(this.ProxySessionHandle);
				stringBuilder.Append("]");
				ExTraceGlobals.ProxyMapiTracer.TraceFunction(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00009E9C File Offset: 0x0000809C
		private void TraceRpcException(RpcException e)
		{
			if (ExTraceGlobals.ProxyMapiTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append("RPC Exception:[");
				stringBuilder.Append(e.ToString());
				stringBuilder.Append("]");
				ExTraceGlobals.ProxyMapiTracer.TraceDebug(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00009EF8 File Offset: 0x000080F8
		private void OnNotificationPending(ErrorCode errorCode, uint sessionHandle)
		{
			IProxyAsyncWaitCompletion proxyAsyncWaitCompletion = null;
			using (LockManager.Lock(this))
			{
				if (this.pendingCompletion != null)
				{
					proxyAsyncWaitCompletion = this.pendingCompletion;
					this.pendingCompletion = null;
					this.notificationsPending = false;
					this.waitQueuedTime = null;
				}
				else
				{
					this.notificationsPending = true;
					ExTraceGlobals.ProxyMapiTracer.TraceFunction(0L, "Notification is pending but wait call is not queued");
				}
			}
			if (proxyAsyncWaitCompletion != null)
			{
				proxyAsyncWaitCompletion.CompleteAsyncCall(ErrorCode.NoError == errorCode, (int)errorCode);
				ExTraceGlobals.ProxyMapiTracer.TraceFunction(0L, "Notification wait completed");
			}
		}

		// Token: 0x0400003E RID: 62
		internal static readonly TimeSpan WaitExpirationPeriod = TimeSpan.FromSeconds(60.0);

		// Token: 0x0400003F RID: 63
		private byte[] securityContext;

		// Token: 0x04000040 RID: 64
		private IRpcInstanceManager manager;

		// Token: 0x04000041 RID: 65
		private IEnumerator<ErrorCode> requestExecutionSteps;

		// Token: 0x04000042 RID: 66
		private Guid? instance;

		// Token: 0x04000043 RID: 67
		private CreateSessionInfo createInfo;

		// Token: 0x04000044 RID: 68
		private uint instanceSessionHandle;

		// Token: 0x04000045 RID: 69
		private int instanceGeneration;

		// Token: 0x04000046 RID: 70
		private ProxySession.ProxySessionState state;

		// Token: 0x04000047 RID: 71
		private Queue<ProxySession.ProxySessionEvent> eventQueue;

		// Token: 0x04000048 RID: 72
		private bool eventsProcessing;

		// Token: 0x04000049 RID: 73
		private DoRpcRequest? requestInfo;

		// Token: 0x0400004A RID: 74
		private Action closeCompleteCallback;

		// Token: 0x0400004B RID: 75
		private IProxyAsyncWaitCompletion pendingCompletion;

		// Token: 0x0400004C RID: 76
		private bool notificationsPending;

		// Token: 0x0400004D RID: 77
		private DateTime? waitQueuedTime;

		// Token: 0x02000011 RID: 17
		private enum ProxySessionState
		{
			// Token: 0x04000052 RID: 82
			Unbound,
			// Token: 0x04000053 RID: 83
			BindOutstanding,
			// Token: 0x04000054 RID: 84
			RequestOutstanding,
			// Token: 0x04000055 RID: 85
			Idle,
			// Token: 0x04000056 RID: 86
			CloseRequested,
			// Token: 0x04000057 RID: 87
			Closed
		}

		// Token: 0x02000012 RID: 18
		private enum ProxySessionEvent
		{
			// Token: 0x04000059 RID: 89
			None,
			// Token: 0x0400005A RID: 90
			RequestReceived,
			// Token: 0x0400005B RID: 91
			BindStepComplete,
			// Token: 0x0400005C RID: 92
			RequestComplete,
			// Token: 0x0400005D RID: 93
			NeedToClose
		}
	}
}
