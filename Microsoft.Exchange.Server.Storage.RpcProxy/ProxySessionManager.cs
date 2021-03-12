using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.RpcProxy;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.ExchangeServer;
using Microsoft.Exchange.Rpc.PoolRpc;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.MapiDisp;

namespace Microsoft.Exchange.Server.Storage.RpcProxy
{
	// Token: 0x02000015 RID: 21
	internal class ProxySessionManager : IPoolSessionManager
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00009FB4 File Offset: 0x000081B4
		public ProxySessionManager(IRpcInstanceManager manager)
		{
			this.sessionMap = PersistentAvlTree<uint, ProxySession>.Empty;
			this.manager = manager;
			this.manager.NotificationsReceived += this.OnNotificationsReceived;
			this.manager.RpcInstanceClosed += this.OnRpcInstanceClosed;
			this.periodicWaitExpirationTimer = new Timer(new TimerCallback(this.PeriodicExpireNotificationWait), null, ProxySessionManager.WaitExpirationTimerDelay, ProxySessionManager.WaitExpirationTimerDelay);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000A03B File Offset: 0x0000823B
		private static byte[] DefaultConnectAuxiliaryBuffer
		{
			get
			{
				if (ProxySessionManager.defaultConnectAuxiliaryBuffer == null)
				{
					ProxySessionManager.defaultConnectAuxiliaryBuffer = ProxySessionManager.ComputeDefaultConnectAuxiliaryOutBuffer();
				}
				return ProxySessionManager.defaultConnectAuxiliaryBuffer;
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000A054 File Offset: 0x00008254
		public ErrorCode CreateProxySession(ClientSecurityContext callerSecurityContext, uint flags, string userDn, uint connectionMode, uint codePageId, uint localeIdString, uint localeIdSort, short[] clientVersion, byte[] auxiliaryIn, Action<ErrorCode, uint> notificationPendingCallback, out uint sessionHandle, out byte[] auxiliaryOut)
		{
			sessionHandle = 0U;
			auxiliaryOut = ProxySessionManager.DefaultConnectAuxiliaryBuffer;
			using (LockManager.Lock(this.syncRoot))
			{
				PersistentAvlTree<uint, ProxySession> persistentAvlTree = this.GetSessionMap();
				if (persistentAvlTree == null)
				{
					return ErrorCode.CreateMdbNotInitialized((LID)47992U);
				}
				CreateSessionInfo createInfo = new CreateSessionInfo
				{
					Flags = flags,
					UserDn = userDn,
					ConnectionMode = connectionMode,
					CodePageId = codePageId,
					LocaleIdString = localeIdString,
					LocaleIdSort = localeIdSort,
					ClientVersion = clientVersion,
					AuxiliaryIn = auxiliaryIn,
					NotificationPendingCallback = notificationPendingCallback
				};
				ProxySession proxySession = new ProxySession(this.manager, AuthenticationContextFactory.CreateSerialization(string.Empty, callerSecurityContext), this.NextSessionId(), createInfo);
				persistentAvlTree = persistentAvlTree.Add(proxySession.ProxySessionHandle, proxySession);
				Interlocked.Exchange<PersistentAvlTree<uint, ProxySession>>(ref this.sessionMap, persistentAvlTree);
				sessionHandle = proxySession.ProxySessionHandle;
			}
			return ErrorCode.NoError;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000A160 File Offset: 0x00008360
		public ErrorCode BeginPoolDoRpc(ref uint sessionHandle, uint flags, uint maximumResponseSize, ArraySegment<byte> request, ArraySegment<byte> auxiliaryIn, DoRpcCompleteCallback callback, Action<RpcException> exceptionCallback)
		{
			PersistentAvlTree<uint, ProxySession> persistentAvlTree = this.GetSessionMap();
			ProxySession proxySession = null;
			if (persistentAvlTree == null || !persistentAvlTree.TryGetValue(sessionHandle, out proxySession))
			{
				ExTraceGlobals.ProxyMapiTracer.TraceDebug(0L, "Request rejected: session handle was not found on the session manager.");
				sessionHandle = 0U;
				return ErrorCode.CreateMdbNotInitialized((LID)64376U);
			}
			return proxySession.QueueDoRpcRequest(flags, maximumResponseSize, request, auxiliaryIn, callback, exceptionCallback);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000A1BC File Offset: 0x000083BC
		public ErrorCode QueueNotificationWait(ref uint sessionHandle, IProxyAsyncWaitCompletion completion)
		{
			PersistentAvlTree<uint, ProxySession> persistentAvlTree = this.GetSessionMap();
			ProxySession proxySession = null;
			if (persistentAvlTree == null || !persistentAvlTree.TryGetValue(sessionHandle, out proxySession))
			{
				ExTraceGlobals.ProxyMapiTracer.TraceDebug(0L, "Notification rejected: session handle was not found on the session manager.");
				sessionHandle = 0U;
				return ErrorCode.CreateMdbNotInitialized((LID)33912U);
			}
			proxySession.QueueNotificationWait(completion);
			return ErrorCode.NoError;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000A214 File Offset: 0x00008414
		public void CloseSession(uint sessionHandle)
		{
			ProxySession proxySession = null;
			try
			{
				using (LockManager.Lock(this.syncRoot))
				{
					PersistentAvlTree<uint, ProxySession> persistentAvlTree = this.GetSessionMap();
					if (persistentAvlTree != null && persistentAvlTree.TryGetValue(sessionHandle, out proxySession))
					{
						persistentAvlTree = persistentAvlTree.Remove(sessionHandle);
						Interlocked.Exchange<PersistentAvlTree<uint, ProxySession>>(ref this.sessionMap, persistentAvlTree);
					}
				}
			}
			finally
			{
				if (proxySession != null)
				{
					proxySession.RequestClose(null);
				}
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000A294 File Offset: 0x00008494
		public void StopAcceptingClientRequests()
		{
			PersistentAvlTree<uint, ProxySession> persistentAvlTree = null;
			Timer timer = null;
			using (LockManager.Lock(this.syncRoot))
			{
				timer = this.periodicWaitExpirationTimer;
				this.periodicWaitExpirationTimer = null;
				persistentAvlTree = this.GetSessionMap();
				Interlocked.Exchange<PersistentAvlTree<uint, ProxySession>>(ref this.sessionMap, null);
			}
			try
			{
				if (persistentAvlTree != null)
				{
					foreach (ProxySession proxySession in persistentAvlTree.GetValuesLmr())
					{
						proxySession.RequestClose(null);
					}
				}
			}
			finally
			{
				if (timer != null)
				{
					timer.Dispose();
				}
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000A354 File Offset: 0x00008554
		private static byte[] ComputeDefaultConnectAuxiliaryOutBuffer()
		{
			byte[] array = MapiRpc.DefaultConnectAuxiliaryBuffer;
			if (array == null || array.Length == 0)
			{
				return null;
			}
			byte[] array2 = new byte[array.Length + 20];
			uint num = 0U;
			PoolRpcServerCommonBase.PackBuffer(new ArraySegment<byte>(array), new ArraySegment<byte>(array2), false, false, out num);
			if (num > 0U)
			{
				byte[] array3 = new byte[num];
				Buffer.BlockCopy(array2, 0, array3, 0, (int)num);
				return array3;
			}
			return null;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000A3AD File Offset: 0x000085AD
		private PersistentAvlTree<uint, ProxySession> GetSessionMap()
		{
			return Interlocked.CompareExchange<PersistentAvlTree<uint, ProxySession>>(ref this.sessionMap, null, null);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000A3BC File Offset: 0x000085BC
		private uint NextSessionId()
		{
			uint num;
			do
			{
				num = this.nextSessionId;
				this.nextSessionId += 1U;
				if (this.nextSessionId == 1073741824U)
				{
					this.nextSessionId = 1U;
				}
			}
			while (this.sessionMap.Contains(num));
			return num;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000A404 File Offset: 0x00008604
		private void PeriodicExpireNotificationWait(object state)
		{
			PersistentAvlTree<uint, ProxySession> persistentAvlTree = this.GetSessionMap();
			if (persistentAvlTree == null)
			{
				return;
			}
			foreach (ProxySession proxySession in persistentAvlTree.GetValuesLmr())
			{
				proxySession.ExpireNotificationWait(DateTime.UtcNow);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000A460 File Offset: 0x00008660
		private void OnNotificationsReceived(Guid instanceId, int generation, ErrorCode errorCode, uint[] sessions)
		{
			PersistentAvlTree<uint, ProxySession> persistentAvlTree = this.GetSessionMap();
			if (persistentAvlTree == null)
			{
				return;
			}
			foreach (ProxySession proxySession in persistentAvlTree.GetValuesLmr())
			{
				proxySession.OnNotificationReceived(instanceId, generation, errorCode, sessions);
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000A4BC File Offset: 0x000086BC
		private void OnRpcInstanceClosed(Guid instanceId, int generation)
		{
			PersistentAvlTree<uint, ProxySession> persistentAvlTree = this.GetSessionMap();
			if (persistentAvlTree == null)
			{
				return;
			}
			foreach (ProxySession proxySession in persistentAvlTree.GetValuesLmr())
			{
				if (proxySession.IsBoundToRpcInstance(instanceId, generation))
				{
					this.CloseSession(proxySession.ProxySessionHandle);
				}
			}
		}

		// Token: 0x0400005E RID: 94
		public const int MaxSessionId = 1073741824;

		// Token: 0x0400005F RID: 95
		private static readonly TimeSpan WaitExpirationTimerDelay = TimeSpan.FromSeconds(10.0);

		// Token: 0x04000060 RID: 96
		private static byte[] defaultConnectAuxiliaryBuffer;

		// Token: 0x04000061 RID: 97
		private PersistentAvlTree<uint, ProxySession> sessionMap;

		// Token: 0x04000062 RID: 98
		private object syncRoot = new object();

		// Token: 0x04000063 RID: 99
		private IRpcInstanceManager manager;

		// Token: 0x04000064 RID: 100
		private uint nextSessionId = 1U;

		// Token: 0x04000065 RID: 101
		private Timer periodicWaitExpirationTimer;
	}
}
