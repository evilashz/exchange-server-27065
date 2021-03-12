using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.PoolRpc;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.MapiDisp
{
	// Token: 0x02000011 RID: 17
	public class PoolRpcServer : DisposableBase, IPoolRpcServer
	{
		// Token: 0x06000262 RID: 610 RVA: 0x000373CA File Offset: 0x000355CA
		private PoolRpcServer()
		{
			this.nextContextHandle = 1;
			this.poolContextMap = PoolRpcServer.PoolContextMap.Empty;
			this.periodicWaitExpirationTimer = new Timer(new TimerCallback(this.PeriodicExpireNotificationWait), null, 10000, 10000);
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00037406 File Offset: 0x00035606
		public static PoolRpcServer Instance
		{
			get
			{
				return PoolRpcServer.instance;
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0003740D File Offset: 0x0003560D
		public static void Initialize()
		{
			PoolRpcServer.instance = new PoolRpcServer();
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00037419 File Offset: 0x00035619
		public static void InitializeForTestOnly(uint maxRpcThreadCount)
		{
			PoolRpcServer.Initialize();
			PoolRpcServer.rpcThreadCounter = new LimitedCounter(maxRpcThreadCount);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0003742B File Offset: 0x0003562B
		public static void StopAcceptingCalls()
		{
			if (PoolRpcServer.instance != null)
			{
				PoolRpcServer.instance.StopAcceptingClientRequests();
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0003743E File Offset: 0x0003563E
		public static void Terminate()
		{
			if (PoolRpcServer.instance != null)
			{
				PoolRpcServer.instance.Dispose();
				PoolRpcServer.instance = null;
			}
			PoolRpcServer.rpcThreadCounter = null;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00037460 File Offset: 0x00035660
		internal static bool StartInterface(Guid? instanceGuid, uint maxRpcThreadCount, bool isLocalOnly)
		{
			bool flag = false;
			if (PoolRpcServer.rpcThreadCounter == null)
			{
				PoolRpcServer.rpcThreadCounter = new LimitedCounter(maxRpcThreadCount);
			}
			if (PoolRpcServer.rpcEndpoint == null)
			{
				try
				{
					PoolRpcServer.rpcEndpoint = (PoolRpcServer.PoolRpcServerEndpoint)PoolRpcServerCommonBase.RegisterServerInstance(typeof(PoolRpcServer.PoolRpcServerEndpoint), instanceGuid, isLocalOnly, "EmsmdbPool Interface");
					PoolRpcServer.rpcNotifyEndpoint = (PoolRpcServer.PoolNotifyRpcServerEndpoint)PoolRpcServerCommonBase.RegisterAutoListenServerInstance(typeof(PoolRpcServer.PoolNotifyRpcServerEndpoint), instanceGuid, 65536, isLocalOnly, "EmsmdbPoolNotify Interface");
					flag = true;
				}
				catch (DuplicateRpcEndpointException exception)
				{
					NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
					Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_DuplicatePoolRpcEndpoint, new object[]
					{
						(instanceGuid != null) ? instanceGuid.Value : Guid.Empty,
						DiagnosticsNativeMethods.GetCurrentProcessId().ToString()
					});
				}
				finally
				{
					if (!flag)
					{
						PoolRpcServer.StopInterface();
					}
				}
			}
			return flag;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0003754C File Offset: 0x0003574C
		internal static void StopInterface()
		{
			if (PoolRpcServer.rpcEndpoint != null)
			{
				RpcServerBase.UnregisterInterface(PoolRpcServerBase.RpcIntfHandle, true);
				PoolRpcServer.rpcEndpoint = null;
			}
			if (PoolRpcServer.rpcNotifyEndpoint != null)
			{
				RpcServerBase.UnregisterInterface(PoolNotifyRpcServerBase.RpcIntfHandle, true);
				PoolRpcServer.rpcNotifyEndpoint = null;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00037580 File Offset: 0x00035780
		private static void TraceStartRpcMarker(string rpcName, IntPtr contextHandle)
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("ENTER CALL [MAPI][");
			stringBuilder.Append(rpcName);
			stringBuilder.Append("] context:[");
			if (contextHandle != IntPtr.Zero)
			{
				stringBuilder.Append(contextHandle.ToInt32());
			}
			else
			{
				stringBuilder.Append("new");
			}
			stringBuilder.Append("]");
			ExTraceGlobals.RpcContextPoolTracer.TraceFunction(0L, stringBuilder.ToString());
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000375FC File Offset: 0x000357FC
		private static void TraceEndRpcMarker(string rpcName, IntPtr contextHandle, ErrorCode error)
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("EXIT CALL [MAPI][");
			stringBuilder.Append(rpcName);
			stringBuilder.Append("] context:[");
			if (contextHandle != IntPtr.Zero)
			{
				stringBuilder.Append(contextHandle.ToInt32());
			}
			else
			{
				stringBuilder.Append("end");
			}
			if (error != ErrorCode.NoError)
			{
				stringBuilder.Append("] error:[");
				stringBuilder.Append(error);
			}
			stringBuilder.Append("]");
			ExTraceGlobals.RpcContextPoolTracer.TraceFunction(0L, stringBuilder.ToString());
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000376A0 File Offset: 0x000358A0
		private static ErrorCode BuildRopInputBufferList(ArraySegment<byte> requestIn, out List<ArraySegment<byte>> inputList, out List<byte[]> inputListLeasedBuffers)
		{
			inputList = null;
			inputListLeasedBuffers = null;
			List<ArraySegment<byte>> list = null;
			List<byte[]> list2 = null;
			byte[] array = null;
			ArraySegment<byte>? arraySegment = new ArraySegment<byte>?(requestIn);
			int num = 0;
			try
			{
				while (num < 96 && arraySegment != null)
				{
					ArraySegment<byte>? arraySegment2;
					ArraySegment<byte> item = PoolRpcServerCommonBase.UnpackBuffer(arraySegment.Value, out arraySegment2, out array);
					num++;
					if (num == 96 && arraySegment2 != null)
					{
						return ErrorCode.CreateNetworkError((LID)57752U);
					}
					if (array != null)
					{
						if (list2 == null)
						{
							if (arraySegment2 != null)
							{
								list2 = new List<byte[]>(96);
							}
							else
							{
								list2 = new List<byte[]>(1);
							}
						}
						list2.Add(array);
						array = null;
					}
					if (list == null)
					{
						if (arraySegment2 != null)
						{
							list = new List<ArraySegment<byte>>(96);
						}
						else
						{
							list = new List<ArraySegment<byte>>(1);
						}
					}
					list.Add(item);
					arraySegment = arraySegment2;
				}
				inputList = list;
				inputListLeasedBuffers = list2;
				list2 = null;
			}
			finally
			{
				if (array != null)
				{
					RpcBufferPool.ReleaseBuffer(array);
				}
				if (list2 != null)
				{
					for (int i = 0; i < list2.Count; i++)
					{
						RpcBufferPool.ReleaseBuffer(list2[i]);
					}
				}
			}
			return ErrorCode.NoError;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000377BC File Offset: 0x000359BC
		private static void TracePoolDetails(PoolRpcServer.ContextHandlePool pool)
		{
			StringBuilder stringBuilder = new StringBuilder(80);
			stringBuilder.Append("MARK POOL pool:[");
			stringBuilder.Append(pool.PoolId);
			stringBuilder.Append("] handles:[");
			stringBuilder.Append(pool.ContextHandleCount);
			stringBuilder.Append("]");
			ExTraceGlobals.RpcContextPoolTracer.TraceFunction(0L, stringBuilder.ToString());
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00037898 File Offset: 0x00035A98
		public virtual int EcPoolConnect(uint flags, Guid poolGuid, ArraySegment<byte> auxiliaryIn, IPoolConnectCompletion completion)
		{
			PoolRpcServer.<>c__DisplayClass1 CS$<>8__locals1 = new PoolRpcServer.<>c__DisplayClass1();
			CS$<>8__locals1.flags = flags;
			CS$<>8__locals1.poolGuid = poolGuid;
			CS$<>8__locals1.auxiliaryIn = auxiliaryIn;
			CS$<>8__locals1.completion = completion;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.localErrorCode = -2147467259;
			CS$<>8__locals1.executionDiagnostics = new MapiExecutionDiagnostics();
			WatsonOnUnhandledException.Guard(CS$<>8__locals1.executionDiagnostics, new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<EcPoolConnect>b__0)));
			return CS$<>8__locals1.localErrorCode;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00037904 File Offset: 0x00035B04
		internal int EcPoolConnect_Unwrapped(MapiExecutionDiagnostics executionDiagnostics, uint flags, Guid poolGuid, ArraySegment<byte> auxiliaryIn, IPoolConnectCompletion completion)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			IntPtr contextHandle = IntPtr.Zero;
			PoolRpcServer.PoolContextMap value = null;
			PoolRpcServer.ContextHandlePool contextHandlePool = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			StorePerDatabasePerformanceCountersInstance databaseInstance = PerformanceCounterFactory.GetDatabaseInstance(null);
			bool flag4 = false;
			if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				PoolRpcServer.TraceStartRpcMarker("EcPoolConnect", IntPtr.Zero);
			}
			try
			{
				using (LockManager.Lock(this, executionDiagnostics))
				{
					PoolRpcServer.PoolContextMap contextMap = this.GetContextMap();
					if (contextMap != null)
					{
						if (poolGuid == Guid.Empty)
						{
							poolGuid = Guid.NewGuid();
							contextHandle = new IntPtr(this.GetNextContextHandle(contextMap));
							contextHandlePool = new PoolRpcServer.ContextHandlePool(poolGuid);
							flag4 = true;
							value = PoolRpcServer.PoolContextMap.Create(contextMap.ContextHandles.Add(contextHandle.ToInt32(), contextHandlePool), contextMap.Pools.Add(poolGuid, contextHandlePool));
						}
						else if (contextMap.Pools.TryGetValue(poolGuid, out contextHandlePool))
						{
							if (contextHandlePool.ContextHandleCount < 32)
							{
								contextHandle = new IntPtr(this.GetNextContextHandle(contextMap));
								value = PoolRpcServer.PoolContextMap.Create(contextMap.ContextHandles.Add(contextHandle.ToInt32(), contextHandlePool), contextMap.Pools);
							}
							else
							{
								errorCode = ErrorCode.CreateMaxPoolExceeded((LID)33176U);
								ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, "CALL FAILED: error:[MaxPoolExceeded]. Max handle count exceeded");
							}
						}
						else
						{
							errorCode = ErrorCode.CreateInvalidPool((LID)49560U);
							ExTraceGlobals.RpcContextPoolTracer.TraceDebug<string>(0L, "CALL FAILED: error:[InvalidPool]. Invalid pool guid '{0}'.", poolGuid.ToString());
						}
						if (ErrorCode.NoError == errorCode)
						{
							contextHandlePool.ContextHandleCreated();
							flag2 = true;
							if (this.ecPoolConnectTestHook != null)
							{
								this.ecPoolConnectTestHook();
							}
							Interlocked.Exchange<PoolRpcServer.PoolContextMap>(ref this.poolContextMap, value);
							flag3 = true;
							if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.FunctionTrace))
							{
								PoolRpcServer.TracePoolDetails(contextHandlePool);
							}
						}
					}
					else
					{
						errorCode = ErrorCode.CreateRpcServerUnavailable((LID)49048U);
						ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, "CALL FAILED: error:[Exiting]. Server shutting down.");
					}
				}
				if (ErrorCode.NoError == errorCode)
				{
					completion.CompleteAsyncCall(contextHandle, 0U, 32U, poolGuid, RpcServerBase.EmptyArraySegment);
					if (databaseInstance != null)
					{
						databaseInstance.ContextHandlePoolHandles.Increment();
						if (flag4)
						{
							databaseInstance.ContextHandlePools.Increment();
						}
					}
					flag = true;
				}
				else
				{
					completion.FailAsyncCall((int)errorCode, RpcServerBase.EmptyArraySegment);
				}
			}
			finally
			{
				if (!flag)
				{
					if (flag3)
					{
						ErrorCode.CreateWithLid((LID)56472U, (ErrorCodeValue)this.EcPoolDisconnect(contextHandle));
					}
					else if (flag2)
					{
						contextHandlePool.ContextHandleClosed();
					}
				}
				if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.FunctionTrace))
				{
					PoolRpcServer.TraceEndRpcMarker("EcPoolConnect", contextHandle, errorCode);
				}
			}
			RpcBufferPool.ReleaseBuffer(auxiliaryIn.Array);
			return (int)ErrorCode.NoError;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00037C28 File Offset: 0x00035E28
		public virtual int EcPoolDisconnect(IntPtr contextHandle)
		{
			PoolRpcServer.<>c__DisplayClass4 CS$<>8__locals1 = new PoolRpcServer.<>c__DisplayClass4();
			CS$<>8__locals1.contextHandle = contextHandle;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.localErrorCode = -2147467259;
			CS$<>8__locals1.executionDiagnostics = new MapiExecutionDiagnostics();
			WatsonOnUnhandledException.Guard(CS$<>8__locals1.executionDiagnostics, new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<EcPoolDisconnect>b__3)));
			return CS$<>8__locals1.localErrorCode;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00037C7C File Offset: 0x00035E7C
		internal int EcPoolDisconnect_Unwrapped(MapiExecutionDiagnostics executionDiagnostics, IntPtr contextHandle)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			PoolRpcServer.ContextHandlePool contextHandlePool = null;
			StorePerDatabasePerformanceCountersInstance databaseInstance = PerformanceCounterFactory.GetDatabaseInstance(null);
			if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				PoolRpcServer.TraceStartRpcMarker("EcPoolDisconnect", contextHandle);
			}
			using (LockManager.Lock(this, executionDiagnostics))
			{
				PoolRpcServer.PoolContextMap contextMap = this.GetContextMap();
				if (contextMap != null && contextMap.ContextHandles.TryGetValue(contextHandle.ToInt32(), out contextHandlePool))
				{
					bool flag = false;
					if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.FunctionTrace))
					{
						PoolRpcServer.TracePoolDetails(contextHandlePool);
					}
					contextHandlePool.ContextHandleClosed();
					try
					{
						PoolRpcServer.PoolContextMap value;
						if (contextHandlePool.CanClose)
						{
							value = PoolRpcServer.PoolContextMap.Create(contextMap.ContextHandles.Remove(contextHandle.ToInt32()), contextMap.Pools.Remove(contextHandlePool.PoolId));
						}
						else
						{
							value = PoolRpcServer.PoolContextMap.Create(contextMap.ContextHandles.Remove(contextHandle.ToInt32()), contextMap.Pools);
						}
						if (databaseInstance != null)
						{
							databaseInstance.ContextHandlePoolHandles.Decrement();
						}
						if (this.ecPoolDisconnectTestHook != null)
						{
							this.ecPoolDisconnectTestHook();
						}
						Interlocked.Exchange<PoolRpcServer.PoolContextMap>(ref this.poolContextMap, value);
						flag = true;
						goto IL_153;
					}
					finally
					{
						if (!flag)
						{
							contextHandlePool.ContextHandleCreated();
						}
					}
				}
				if (contextMap == null)
				{
					errorCode = ErrorCode.CreateRpcServerUnavailable((LID)65432U);
					ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, "CALL FAILED: error:[Exiting]. Server shutting down.");
				}
				else
				{
					errorCode = ErrorCode.CreateInvalidParameter((LID)40856U);
					ExTraceGlobals.RpcContextPoolTracer.TraceDebug<int>(0L, "CALL FAILED: Invalid context handle '{0}'.", contextHandle.ToInt32());
				}
				IL_153:;
			}
			if (contextHandlePool != null && contextHandlePool.CanClose)
			{
				ExTraceGlobals.RpcContextPoolTracer.TraceDebug<string>(0L, "Pool '{0}' is closing.", contextHandlePool.PoolId.ToString());
				contextHandlePool.Close();
				if (databaseInstance != null)
				{
					databaseInstance.ContextHandlePools.Decrement();
				}
			}
			if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				PoolRpcServer.TraceEndRpcMarker("EcPoolDisconnect", contextHandle, errorCode);
			}
			return (int)errorCode;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00037F28 File Offset: 0x00036128
		public virtual int EcPoolCreateSession(IntPtr contextHandle, ClientSecurityContext callerSecurityContext, byte[] sessionSecurityContext, uint flags, string userDn, uint connectionMode, uint codePageId, uint localeIdString, uint localeIdSort, short[] clientVersion, ArraySegment<byte> auxiliaryIn, IPoolCreateSessionCompletion completion)
		{
			PoolRpcServer.<>c__DisplayClass7 CS$<>8__locals1 = new PoolRpcServer.<>c__DisplayClass7();
			CS$<>8__locals1.contextHandle = contextHandle;
			CS$<>8__locals1.callerSecurityContext = callerSecurityContext;
			CS$<>8__locals1.sessionSecurityContext = sessionSecurityContext;
			CS$<>8__locals1.flags = flags;
			CS$<>8__locals1.userDn = userDn;
			CS$<>8__locals1.connectionMode = connectionMode;
			CS$<>8__locals1.codePageId = codePageId;
			CS$<>8__locals1.localeIdString = localeIdString;
			CS$<>8__locals1.localeIdSort = localeIdSort;
			CS$<>8__locals1.clientVersion = clientVersion;
			CS$<>8__locals1.auxiliaryIn = auxiliaryIn;
			CS$<>8__locals1.completion = completion;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.localErrorCode = -2147467259;
			CS$<>8__locals1.executionDiagnostics = new MapiExecutionDiagnostics();
			WatsonOnUnhandledException.Guard(CS$<>8__locals1.executionDiagnostics, new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<EcPoolCreateSession>b__6)));
			return CS$<>8__locals1.localErrorCode;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00037FD4 File Offset: 0x000361D4
		internal int EcPoolCreateSession_Unwrapped(MapiExecutionDiagnostics executionDiagnostics, IntPtr contextHandle, ClientSecurityContext callerSecurityContext, byte[] sessionSecurityContext, uint flags, string userDn, uint connectionMode, uint codePageId, uint localeIdString, uint localeIdSort, short[] clientVersion, ArraySegment<byte> auxiliaryIn, IPoolCreateSessionCompletion completion)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			ArraySegment<byte> auxiliaryOut = RpcServerBase.EmptyArraySegment;
			byte[] array = null;
			byte[] array2 = null;
			try
			{
				if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.FunctionTrace))
				{
					PoolRpcServer.TraceStartRpcMarker("EcPoolCreateSession", contextHandle);
				}
				PoolRpcServer.PoolContextMap contextMap = this.GetContextMap();
				PoolRpcServer.ContextHandlePool contextHandlePool;
				if (contextMap == null)
				{
					errorCode = ErrorCode.CreateRpcServerUnavailable((LID)61336U);
					ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, "CALL FAILED: error:[Exiting]. Server shutting down.");
				}
				else if (!contextMap.ContextHandles.TryGetValue(contextHandle.ToInt32(), out contextHandlePool))
				{
					errorCode = ErrorCode.CreateInvalidParameter((LID)44952U);
					ExTraceGlobals.RpcContextPoolTracer.TraceDebug<int>(0L, "CALL FAILED: error:[InvalidParameter]. Invalid context handle '{0}'.", contextHandle.ToInt32());
				}
				else
				{
					if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.FunctionTrace))
					{
						PoolRpcServer.TracePoolDetails(contextHandlePool);
					}
					IntPtr zero = IntPtr.Zero;
					uint num = 0U;
					ArraySegment<byte> auxIn = RpcServerBase.EmptyArraySegment;
					if (auxiliaryIn.Count > 0)
					{
						if (auxiliaryIn.Count < 8)
						{
							errorCode = ErrorCode.CreateNetworkError((LID)57240U);
							goto IL_298;
						}
						ArraySegment<byte>? arraySegment;
						auxIn = PoolRpcServerCommonBase.UnpackBuffer(auxiliaryIn, out arraySegment, out array);
					}
					byte[] array3 = new byte[1024];
					int num2 = 0;
					TimeSpan timeSpan;
					int retryCount;
					TimeSpan timeSpan2;
					string text;
					string displayName;
					errorCode = ErrorCode.CreateWithLid((LID)52376U, (ErrorCodeValue)MapiRpc.Instance.DoConnect(executionDiagnostics, out zero, userDn, callerSecurityContext, sessionSecurityContext, (int)flags, (int)connectionMode, (int)codePageId, (int)localeIdString, (int)localeIdSort, out timeSpan, out retryCount, out timeSpan2, out text, out displayName, clientVersion, auxIn, ref array3, out num2, new Action<int>(contextHandlePool.NotificationPending)));
					try
					{
						if (num2 > 0)
						{
							uint num3 = 0U;
							array2 = RpcBufferPool.GetBuffer(4104);
							PoolRpcServerCommonBase.PackBuffer(new ArraySegment<byte>(array3, 0, num2), new ArraySegment<byte>(array2), false, false, out num3);
							if (num3 > 0U)
							{
								auxiliaryOut = new ArraySegment<byte>(array2, 0, (int)num3);
							}
						}
						if (!(errorCode != ErrorCode.NoError))
						{
							uint maximumPolls = (uint)timeSpan.TotalMilliseconds;
							uint retryDelay = (uint)timeSpan2.TotalMilliseconds;
							errorCode = contextHandlePool.SessionCreated(zero, out num);
							if (!(errorCode != ErrorCode.NoError))
							{
								completion.CompleteAsyncCall(num, displayName, maximumPolls, (uint)retryCount, retryDelay, 0, auxiliaryOut);
								if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.DebugTrace))
								{
									StringBuilder stringBuilder = new StringBuilder(80);
									stringBuilder.Append("New session created. Pool:[");
									stringBuilder.Append(contextHandlePool.PoolId.ToString());
									stringBuilder.Append("] session:[");
									stringBuilder.Append(zero);
									stringBuilder.Append("]");
									ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, stringBuilder.ToString());
								}
								zero = IntPtr.Zero;
								num = 0U;
							}
						}
					}
					finally
					{
						if (zero != IntPtr.Zero)
						{
							MapiRpc.Instance.DoDisconnect(executionDiagnostics, ref zero);
						}
						if (num != 0U)
						{
							contextHandlePool.SessionClosed(num);
						}
					}
				}
				IL_298:
				if (errorCode != ErrorCode.NoError)
				{
					completion.FailAsyncCall((int)errorCode, auxiliaryOut);
				}
				if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.FunctionTrace))
				{
					PoolRpcServer.TraceEndRpcMarker("EcPoolCreateSession", contextHandle, errorCode);
				}
			}
			finally
			{
				if (array != null)
				{
					RpcBufferPool.ReleaseBuffer(array);
				}
				if (array2 != null)
				{
					RpcBufferPool.ReleaseBuffer(array2);
				}
			}
			RpcBufferPool.ReleaseBuffer(auxiliaryIn.Array);
			return (int)ErrorCode.NoError;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00038378 File Offset: 0x00036578
		public virtual int EcPoolCloseSession(IntPtr contextHandle, uint sessionHandle, IPoolCloseSessionCompletion completion)
		{
			PoolRpcServer.<>c__DisplayClassa CS$<>8__locals1 = new PoolRpcServer.<>c__DisplayClassa();
			CS$<>8__locals1.contextHandle = contextHandle;
			CS$<>8__locals1.sessionHandle = sessionHandle;
			CS$<>8__locals1.completion = completion;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.localErrorCode = -2147467259;
			CS$<>8__locals1.executionDiagnostics = new MapiExecutionDiagnostics();
			WatsonOnUnhandledException.Guard(CS$<>8__locals1.executionDiagnostics, new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<EcPoolCloseSession>b__9)));
			return CS$<>8__locals1.localErrorCode;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000383DC File Offset: 0x000365DC
		internal int EcPoolCloseSession_Unwrapped(MapiExecutionDiagnostics executionDiagnostics, IntPtr contextHandle, uint sessionHandle, IPoolCloseSessionCompletion completion)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				PoolRpcServer.TraceStartRpcMarker("EcPoolCloseSession", contextHandle);
			}
			PoolRpcServer.PoolContextMap contextMap = this.GetContextMap();
			if (contextMap != null)
			{
				PoolRpcServer.ContextHandlePool contextHandlePool;
				if (contextMap.ContextHandles.TryGetValue(contextHandle.ToInt32(), out contextHandlePool))
				{
					if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.FunctionTrace))
					{
						PoolRpcServer.TracePoolDetails(contextHandlePool);
					}
					IntPtr zero = IntPtr.Zero;
					errorCode = contextHandlePool.GetSessionHandle(sessionHandle, out zero);
					if (!(errorCode == ErrorCode.NoError))
					{
						goto IL_162;
					}
					try
					{
						errorCode = ErrorCode.CreateWithLid((LID)46232U, (ErrorCodeValue)MapiRpc.Instance.DoDisconnect(executionDiagnostics, ref zero));
						goto IL_162;
					}
					finally
					{
						contextHandlePool.SessionClosed(sessionHandle);
						if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							StringBuilder stringBuilder = new StringBuilder(80);
							stringBuilder.Append("Session closed. Pool:[");
							stringBuilder.Append(contextHandlePool.PoolId.ToString());
							stringBuilder.Append("] session:[");
							stringBuilder.Append(sessionHandle);
							stringBuilder.Append("]");
							ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, stringBuilder.ToString());
						}
					}
				}
				errorCode = ErrorCode.CreateInvalidParameter((LID)36760U);
				ExTraceGlobals.RpcContextPoolTracer.TraceDebug<int>(0L, "CALL FAILED: error:[InvalidParameter]. Invalid context handle '{0}'.", contextHandle.ToInt32());
			}
			else
			{
				errorCode = ErrorCode.CreateRpcServerUnavailable((LID)53144U);
				ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, "CALL FAILED: error:[Exiting]. Server shutting down.");
			}
			IL_162:
			if (errorCode == ErrorCode.NoError)
			{
				completion.CompleteAsyncCall();
			}
			else
			{
				completion.FailAsyncCall((int)errorCode, RpcServerBase.EmptyArraySegment);
			}
			if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				PoolRpcServer.TraceEndRpcMarker("EcPoolCloseSession", contextHandle, errorCode);
			}
			return (int)ErrorCode.NoError;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00038660 File Offset: 0x00036860
		public virtual int EcPoolSessionDoRpc(IntPtr contextHandle, uint sessionHandle, uint flags, uint maximumResponseSize, ArraySegment<byte> request, ArraySegment<byte> auxiliaryIn, IPoolSessionDoRpcCompletion completion)
		{
			PoolRpcServer.<>c__DisplayClassd CS$<>8__locals1 = new PoolRpcServer.<>c__DisplayClassd();
			CS$<>8__locals1.contextHandle = contextHandle;
			CS$<>8__locals1.sessionHandle = sessionHandle;
			CS$<>8__locals1.flags = flags;
			CS$<>8__locals1.maximumResponseSize = maximumResponseSize;
			CS$<>8__locals1.request = request;
			CS$<>8__locals1.auxiliaryIn = auxiliaryIn;
			CS$<>8__locals1.completion = completion;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.localErrorCode = -2147467259;
			try
			{
				PoolRpcServer.<>c__DisplayClassf CS$<>8__locals2 = new PoolRpcServer.<>c__DisplayClassf();
				CS$<>8__locals2.CS$<>8__localse = CS$<>8__locals1;
				if (PoolRpcServer.rpcThreadCounter != null && PoolRpcServer.rpcThreadCounter.IsIncrementedValueOverLimit())
				{
					ErrorCode errorCode = ErrorCode.CreateRpcServerTooBusy((LID)47708U);
					CS$<>8__locals1.localErrorCode = (int)errorCode;
					return CS$<>8__locals1.localErrorCode;
				}
				CS$<>8__locals2.executionDiagnostics = new MapiExecutionDiagnostics();
				WatsonOnUnhandledException.Guard(CS$<>8__locals2.executionDiagnostics, new TryDelegate(CS$<>8__locals2, (UIntPtr)ldftn(<EcPoolSessionDoRpc>b__c)));
			}
			finally
			{
				if (PoolRpcServer.rpcThreadCounter != null)
				{
					PoolRpcServer.rpcThreadCounter.Decrement();
				}
			}
			return CS$<>8__locals1.localErrorCode;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00038750 File Offset: 0x00036950
		internal int EcPoolSessionDoRpc_Unwrapped(MapiExecutionDiagnostics executionDiagnostics, IntPtr contextHandle, uint sessionHandle, uint flags, uint maximumResponseSize, ArraySegment<byte> request, ArraySegment<byte> auxiliaryIn, IPoolSessionDoRpcCompletion completion)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				PoolRpcServer.TraceStartRpcMarker("EcPoolSessionDoRpc", contextHandle);
			}
			PoolRpcServer.PoolContextMap contextMap = this.GetContextMap();
			if (contextMap != null)
			{
				PoolRpcServer.ContextHandlePool contextHandlePool;
				if (contextMap.ContextHandles.TryGetValue(contextHandle.ToInt32(), out contextHandlePool))
				{
					if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.FunctionTrace))
					{
						PoolRpcServer.TracePoolDetails(contextHandlePool);
					}
					IntPtr zero = IntPtr.Zero;
					errorCode = contextHandlePool.GetSessionHandle(sessionHandle, out zero);
					if (errorCode == ErrorCode.NoError)
					{
						errorCode = this.EcDoRpc(executionDiagnostics, ref zero, flags, maximumResponseSize, request, auxiliaryIn, completion);
						if (zero == IntPtr.Zero)
						{
							contextHandlePool.SessionClosed(sessionHandle);
							if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								StringBuilder stringBuilder = new StringBuilder(80);
								stringBuilder.Append("Session closed by server. Pool:[");
								stringBuilder.Append(contextHandlePool.PoolId.ToString());
								stringBuilder.Append("] session:[");
								stringBuilder.Append(sessionHandle);
								stringBuilder.Append("]");
								ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, stringBuilder.ToString());
							}
						}
					}
				}
				else
				{
					errorCode = ErrorCode.CreateInvalidParameter((LID)47000U);
					ExTraceGlobals.RpcContextPoolTracer.TraceDebug<int>(0L, "CALL FAILED: error:[InvalidParameter]. Invalid context handle '{0}'.", contextHandle.ToInt32());
				}
			}
			else
			{
				errorCode = ErrorCode.CreateRpcServerUnavailable((LID)63384U);
				ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, "CALL FAILED: error:[Exiting]. Server shutting down.");
			}
			if (errorCode != ErrorCode.NoError)
			{
				completion.FailAsyncCall((int)errorCode, RpcServerBase.EmptyArraySegment);
			}
			if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				PoolRpcServer.TraceEndRpcMarker("EcPoolSessionDoRpc", contextHandle, errorCode);
			}
			RpcBufferPool.ReleaseBuffer(request.Array);
			RpcBufferPool.ReleaseBuffer(auxiliaryIn.Array);
			return (int)ErrorCode.NoError;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0003894C File Offset: 0x00036B4C
		public virtual int EcPoolWaitForNotificationsAsync(IntPtr contextHandle, IPoolWaitForNotificationsCompletion completion)
		{
			PoolRpcServer.<>c__DisplayClass12 CS$<>8__locals1 = new PoolRpcServer.<>c__DisplayClass12();
			CS$<>8__locals1.contextHandle = contextHandle;
			CS$<>8__locals1.completion = completion;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.localErrorCode = -2147467259;
			CS$<>8__locals1.executionDiagnostics = new MapiExecutionDiagnostics();
			WatsonOnUnhandledException.Guard(CS$<>8__locals1.executionDiagnostics, new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<EcPoolWaitForNotificationsAsync>b__11)));
			return CS$<>8__locals1.localErrorCode;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000389A8 File Offset: 0x00036BA8
		internal int EcPoolWaitForNotificationsAsync_Unwrapped(MapiExecutionDiagnostics executionDiagnostics, IntPtr contextHandle, IPoolWaitForNotificationsCompletion completion)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				PoolRpcServer.TraceStartRpcMarker("EcPoolWaitForNotificationsAsync", contextHandle);
			}
			PoolRpcServer.PoolContextMap contextMap = this.GetContextMap();
			if (contextMap != null)
			{
				PoolRpcServer.ContextHandlePool contextHandlePool;
				if (contextMap.ContextHandles.TryGetValue(contextHandle.ToInt32(), out contextHandlePool))
				{
					if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.FunctionTrace))
					{
						PoolRpcServer.TracePoolDetails(contextHandlePool);
					}
					errorCode = contextHandlePool.QueueNotificationWait(completion);
				}
				else
				{
					errorCode = ErrorCode.CreateInvalidParameter((LID)38808U);
					ExTraceGlobals.RpcContextPoolTracer.TraceDebug<int>(0L, "CALL FAILED: error:[InvalidParameter]. Invalid context handle '{0}'.", contextHandle.ToInt32());
				}
			}
			else
			{
				errorCode = ErrorCode.CreateRpcServerUnavailable((LID)55192U);
				ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, "CALL FAILED: error:[Exiting]. Server shutting down.");
			}
			if (errorCode != ErrorCode.NoError)
			{
				completion.FailAsyncCall((int)errorCode, RpcServerBase.EmptyArraySegment);
			}
			if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				PoolRpcServer.TraceEndRpcMarker("EcPoolWaitForNotificationsAsync", contextHandle, errorCode);
			}
			return (int)ErrorCode.NoError;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00038A9C File Offset: 0x00036C9C
		public virtual ushort GetVersionDelta()
		{
			return 6000;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00038AA4 File Offset: 0x00036CA4
		private int GetNextContextHandle(PoolRpcServer.PoolContextMap map)
		{
			int num;
			do
			{
				num = this.nextContextHandle;
				this.nextContextHandle++;
				if (this.nextContextHandle == 1879048192)
				{
					this.nextContextHandle = 1;
				}
			}
			while (map.ContextHandles.Contains(num));
			return num;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00038AE9 File Offset: 0x00036CE9
		private PoolRpcServer.PoolContextMap GetContextMap()
		{
			return Interlocked.CompareExchange<PoolRpcServer.PoolContextMap>(ref this.poolContextMap, null, null);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00038AF8 File Offset: 0x00036CF8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PoolRpcServer>(this);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00038B00 File Offset: 0x00036D00
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				this.StopAcceptingClientRequests();
				if (this.periodicWaitExpirationTimer != null)
				{
					this.periodicWaitExpirationTimer.Dispose();
					this.periodicWaitExpirationTimer = null;
				}
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00038B28 File Offset: 0x00036D28
		private void StopAcceptingClientRequests()
		{
			PoolRpcServer.PoolContextMap poolContextMap = null;
			if (this.poolContextMap != null)
			{
				using (LockManager.Lock(this))
				{
					poolContextMap = this.GetContextMap();
					Interlocked.Exchange<PoolRpcServer.PoolContextMap>(ref this.poolContextMap, null);
					if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, "Server shutdown initiated.");
					}
				}
				if (poolContextMap != null)
				{
					foreach (PoolRpcServer.ContextHandlePool contextHandlePool in poolContextMap.Pools.GetValuesLmr())
					{
						if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							StringBuilder stringBuilder = new StringBuilder(60);
							stringBuilder.Append("Shutting down pool:[");
							stringBuilder.Append(contextHandlePool.PoolId.ToString());
							stringBuilder.Append("] handles:[");
							stringBuilder.Append(contextHandlePool.ContextHandleCount);
							stringBuilder.Append("]");
							ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, stringBuilder.ToString());
						}
						contextHandlePool.Close();
					}
					StorePerDatabasePerformanceCountersInstance databaseInstance = PerformanceCounterFactory.GetDatabaseInstance(null);
					if (databaseInstance != null)
					{
						databaseInstance.ContextHandlePoolHandles.IncrementBy((long)(-(long)poolContextMap.ContextHandles.Count));
						databaseInstance.ContextHandlePools.IncrementBy((long)(-(long)poolContextMap.Pools.Count));
					}
				}
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00038CA0 File Offset: 0x00036EA0
		private void PeriodicExpireNotificationWait(object state)
		{
			PoolRpcServer.PoolContextMap contextMap = this.GetContextMap();
			if (contextMap == null)
			{
				return;
			}
			DateTime utcNow = DateTime.UtcNow;
			foreach (PoolRpcServer.ContextHandlePool contextHandlePool in contextMap.Pools.GetValuesLmr())
			{
				contextHandlePool.ExpireNotificationWait(ErrorCode.NoError, utcNow);
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00038D08 File Offset: 0x00036F08
		private int SizeSubtractAndCap(uint minuend, uint subtrahend, uint maxResult)
		{
			if (minuend <= subtrahend)
			{
				return 0;
			}
			uint num = minuend - subtrahend;
			if (num > maxResult)
			{
				return (int)maxResult;
			}
			return (int)num;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00038D28 File Offset: 0x00036F28
		private void UpdateHeaderFlags(byte[] response, uint headerIndex, PoolRpcServer.HeaderFlags flagsToSet, PoolRpcServer.HeaderFlags flagsToClear)
		{
			headerIndex += 2U;
			PoolRpcServer.HeaderFlags headerFlags = (PoolRpcServer.HeaderFlags)response[(int)((UIntPtr)headerIndex)];
			headerFlags |= (PoolRpcServer.HeaderFlags)(response[(int)((UIntPtr)(headerIndex + 1U))] << 8);
			headerFlags |= flagsToSet;
			headerFlags &= ~flagsToClear;
			response[(int)((UIntPtr)headerIndex)] = (byte)(headerFlags & (PoolRpcServer.HeaderFlags)255);
			response[(int)((UIntPtr)(headerIndex + 1U))] = (byte)((headerFlags & (PoolRpcServer.HeaderFlags)65280) >> 8);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00038D78 File Offset: 0x00036F78
		private ErrorCode EcDoRpc(MapiExecutionDiagnostics executionDiagnostics, ref IntPtr sessionHandle, uint flags, uint maximumResponseSize, ArraySegment<byte> request, ArraySegment<byte> auxiliaryIn, IPoolSessionDoRpcCompletion completion)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			bool compress = false;
			bool xor = false;
			bool flag = false;
			bool fakeRequest = false;
			uint num = 0U;
			uint count = 0U;
			if (maximumResponseSize <= 8U)
			{
				return ErrorCode.CreateNetworkError((LID)42904U);
			}
			if (maximumResponseSize > 98304U)
			{
				maximumResponseSize = 98304U;
			}
			if (request.Count == 0)
			{
				return ErrorCode.CreateRpcFormat((LID)59288U);
			}
			if (request.Count < 8)
			{
				return ErrorCode.CreateNetworkError((LID)34712U);
			}
			List<ArraySegment<byte>> list = null;
			List<byte[]> list2 = null;
			compress = ((flags & 1U) == 0U);
			xor = ((flags & 2U) == 0U);
			flag = ((flags & 4U) != 0U);
			byte[] array = null;
			byte[] array2 = null;
			byte[] array3 = null;
			try
			{
				ArraySegment<byte> auxIn = RpcServerBase.EmptyArraySegment;
				errorCode = PoolRpcServer.BuildRopInputBufferList(request, out list, out list2);
				if (errorCode != ErrorCode.NoError)
				{
					return errorCode;
				}
				if (auxiliaryIn.Count > 0)
				{
					if (auxiliaryIn.Count < 8)
					{
						return ErrorCode.CreateNetworkError((LID)51096U);
					}
					ArraySegment<byte>? arraySegment;
					auxIn = PoolRpcServerCommonBase.UnpackBuffer(auxiliaryIn, out arraySegment, out array);
				}
				array2 = RpcBufferPool.GetBuffer(98304);
				array3 = RpcBufferPool.GetBuffer(4104);
				int num2 = 4096;
				int num3 = 0;
				for (;;)
				{
					byte[] array4 = null;
					byte[] array5 = null;
					byte[] array6 = null;
					uint headerIndex = 0U;
					try
					{
						int num4 = this.SizeSubtractAndCap(maximumResponseSize, num + 8U, 32767U);
						array4 = RpcBufferPool.GetBuffer(num4);
						array5 = RpcBufferPool.GetBuffer(num2);
						int num5;
						int num6;
						errorCode = ErrorCode.CreateWithLid((LID)38040U, (ErrorCodeValue)MapiRpc.Instance.DoRpc(executionDiagnostics, ref sessionHandle, list, new ArraySegment<byte>(array4, 0, num4), out num5, (flags & 16U) != 0U, auxIn, new ArraySegment<byte>(array5, 0, num2), out num6, fakeRequest, out array6));
						if (num6 > 0)
						{
							if (num6 > num2)
							{
								return ErrorCode.CreateNetworkError((LID)64408U);
							}
							count = 0U;
							PoolRpcServerCommonBase.PackBuffer(new ArraySegment<byte>(array5, 0, num6), new ArraySegment<byte>(array3, 0, 4104), compress, xor, out count);
						}
						if (errorCode != ErrorCode.NoError)
						{
							completion.FailAsyncCall((int)errorCode, new ArraySegment<byte>(array3, 0, (int)count));
							return ErrorCode.NoError;
						}
						if (num5 == 0)
						{
							array6 = null;
							if (num > 0U)
							{
								this.UpdateHeaderFlags(array2, headerIndex, PoolRpcServer.HeaderFlags.Last, PoolRpcServer.HeaderFlags.None);
							}
						}
						else
						{
							if (num5 > num4 || num5 > array4.Length)
							{
								return ErrorCode.CreateNetworkError((LID)48024U);
							}
							uint num7 = 0U;
							PoolRpcServerCommonBase.PackBuffer(new ArraySegment<byte>(array4, 0, num5), new ArraySegment<byte>(array2, (int)num, (int)(maximumResponseSize - num)), compress, xor, out num7);
							headerIndex = num;
							num += num7;
						}
						if (!flag || array6 == null)
						{
							break;
						}
						if (num3 == 95)
						{
							break;
						}
						if (maximumResponseSize <= num)
						{
							break;
						}
						RopId ropId = (RopId)array6[2];
						uint num8;
						if (ropId != RopId.QueryRows)
						{
							if (ropId != RopId.FastTransferSourceGetBuffer && ropId != RopId.FastTransferSourceGetBufferExtended)
							{
								num8 = 8192U;
							}
							else
							{
								num8 = 16391U;
							}
						}
						else
						{
							num8 = 32775U;
						}
						if (maximumResponseSize - num < num8)
						{
							break;
						}
						if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							StringBuilder stringBuilder = new StringBuilder(100);
							stringBuilder.Append("Preparing to compute chained response. rop:[");
							stringBuilder.Append(((RopId)array6[2]).ToString());
							stringBuilder.Append("] bufferSize:[");
							stringBuilder.Append(maximumResponseSize - num);
							stringBuilder.Append("]");
							ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, stringBuilder.ToString());
						}
						this.UpdateHeaderFlags(array2, headerIndex, PoolRpcServer.HeaderFlags.None, PoolRpcServer.HeaderFlags.Last);
						list.Clear();
						list.Add(new ArraySegment<byte>(array6));
						auxIn = RpcServerBase.EmptyArraySegment;
						num2 = 0;
						fakeRequest = true;
					}
					finally
					{
						if (array5 != null)
						{
							RpcBufferPool.ReleaseBuffer(array5);
							array5 = null;
						}
						if (array4 != null)
						{
							RpcBufferPool.ReleaseBuffer(array4);
							array4 = null;
						}
					}
					num3++;
				}
				completion.CompleteAsyncCall(flags, new ArraySegment<byte>(array2, 0, (int)num), new ArraySegment<byte>(array3, 0, (int)count));
			}
			finally
			{
				if (array != null)
				{
					RpcBufferPool.ReleaseBuffer(array);
				}
				if (array2 != null)
				{
					RpcBufferPool.ReleaseBuffer(array2);
				}
				if (array3 != null)
				{
					RpcBufferPool.ReleaseBuffer(array3);
				}
				if (list2 != null)
				{
					for (int i = 0; i < list2.Count; i++)
					{
						RpcBufferPool.ReleaseBuffer(list2[i]);
					}
				}
			}
			return errorCode;
		}

		// Token: 0x0400015B RID: 347
		private const int SizeOfRpcHeaderExt = 8;

		// Token: 0x0400015C RID: 348
		private const int MaximumResponseSize = 98304;

		// Token: 0x0400015D RID: 349
		private const int MaximumBufferSize = 32767;

		// Token: 0x0400015E RID: 350
		private const int MaximumAuxResponseBufferSize = 4104;

		// Token: 0x0400015F RID: 351
		private const int MaximumAuxBufferSize = 4096;

		// Token: 0x04000160 RID: 352
		private const int MaximumChainedBuffers = 96;

		// Token: 0x04000161 RID: 353
		private const uint MinimumChainSize = 8192U;

		// Token: 0x04000162 RID: 354
		private const uint MinimumQueryRowsChainSize = 32775U;

		// Token: 0x04000163 RID: 355
		private const uint MinimumFastTransferChainSize = 16391U;

		// Token: 0x04000164 RID: 356
		private const int WaitExpirationTimerDelay = 10000;

		// Token: 0x04000165 RID: 357
		private const int MaxContextHandles = 1879048192;

		// Token: 0x04000166 RID: 358
		private const int MaxHandlesInPool = 32;

		// Token: 0x04000167 RID: 359
		private const int MaxNotificationCalls = 65536;

		// Token: 0x04000168 RID: 360
		private static readonly TimeSpan WaitExpirationPeriod = TimeSpan.FromSeconds(60.0);

		// Token: 0x04000169 RID: 361
		private static PoolRpcServer instance = null;

		// Token: 0x0400016A RID: 362
		private static PoolRpcServer.PoolRpcServerEndpoint rpcEndpoint = null;

		// Token: 0x0400016B RID: 363
		private static PoolRpcServer.PoolNotifyRpcServerEndpoint rpcNotifyEndpoint = null;

		// Token: 0x0400016C RID: 364
		private static LimitedCounter rpcThreadCounter = null;

		// Token: 0x0400016D RID: 365
		private PoolRpcServer.PoolContextMap poolContextMap;

		// Token: 0x0400016E RID: 366
		private int nextContextHandle;

		// Token: 0x0400016F RID: 367
		private Timer periodicWaitExpirationTimer;

		// Token: 0x04000170 RID: 368
		private Action ecPoolConnectTestHook;

		// Token: 0x04000171 RID: 369
		private Action ecPoolDisconnectTestHook;

		// Token: 0x02000012 RID: 18
		[Flags]
		private enum HeaderFlags : ushort
		{
			// Token: 0x04000173 RID: 371
			None = 0,
			// Token: 0x04000174 RID: 372
			Compressed = 1,
			// Token: 0x04000175 RID: 373
			XorMagic = 2,
			// Token: 0x04000176 RID: 374
			Last = 4
		}

		// Token: 0x02000013 RID: 19
		[Flags]
		private enum DoRpcFlags : uint
		{
			// Token: 0x04000178 RID: 376
			None = 0U,
			// Token: 0x04000179 RID: 377
			NoCompression = 1U,
			// Token: 0x0400017A RID: 378
			NoXorMagic = 2U,
			// Token: 0x0400017B RID: 379
			Chain = 4U,
			// Token: 0x0400017C RID: 380
			ExtendedError = 8U,
			// Token: 0x0400017D RID: 381
			InternalAccessPrivileges = 16U
		}

		// Token: 0x02000014 RID: 20
		internal class ContextHandlePool
		{
			// Token: 0x06000285 RID: 645 RVA: 0x000391F4 File Offset: 0x000373F4
			public ContextHandlePool(Guid guid)
			{
				this.sessionHandleMap = PersistentAvlTree<uint, IntPtr>.Empty;
				this.guid = guid;
				this.contextHandles = 0;
				this.pendingNotificationSet = new HashSet<uint>();
				this.pendingCompletion = null;
				this.completionQueued = false;
				this.waitQueuedTime = null;
			}

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x06000286 RID: 646 RVA: 0x00039245 File Offset: 0x00037445
			public bool CanClose
			{
				get
				{
					return 0 == this.contextHandles;
				}
			}

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x06000287 RID: 647 RVA: 0x00039250 File Offset: 0x00037450
			public int ContextHandleCount
			{
				get
				{
					return this.contextHandles;
				}
			}

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x06000288 RID: 648 RVA: 0x00039258 File Offset: 0x00037458
			public Guid PoolId
			{
				get
				{
					return this.guid;
				}
			}

			// Token: 0x06000289 RID: 649 RVA: 0x00039260 File Offset: 0x00037460
			public void ContextHandleCreated()
			{
				Interlocked.Increment(ref this.contextHandles);
			}

			// Token: 0x0600028A RID: 650 RVA: 0x0003926E File Offset: 0x0003746E
			public void ContextHandleClosed()
			{
				Interlocked.Decrement(ref this.contextHandles);
			}

			// Token: 0x0600028B RID: 651 RVA: 0x0003927C File Offset: 0x0003747C
			public void Close()
			{
				try
				{
					PersistentAvlTree<uint, IntPtr> persistentAvlTree;
					using (LockManager.Lock(this))
					{
						persistentAvlTree = this.sessionHandleMap;
						this.sessionHandleMap = null;
					}
					foreach (IntPtr intPtr in persistentAvlTree.GetValuesLmr())
					{
						IntPtr intPtr2 = intPtr;
						MapiRpc.Instance.DoDisconnect(null, ref intPtr2);
					}
				}
				finally
				{
					this.ExpireNotificationWait(ErrorCode.CreateRpcServerUnavailable((LID)54424U), DateTime.UtcNow);
				}
			}

			// Token: 0x0600028C RID: 652 RVA: 0x00039334 File Offset: 0x00037534
			public ErrorCode GetSessionHandle(uint sessionHandle, out IntPtr handle)
			{
				PersistentAvlTree<uint, IntPtr> persistentAvlTree = Interlocked.CompareExchange<PersistentAvlTree<uint, IntPtr>>(ref this.sessionHandleMap, null, null);
				handle = IntPtr.Zero;
				if (persistentAvlTree == null)
				{
					if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						StringBuilder stringBuilder = new StringBuilder(100);
						stringBuilder.Append("CALL FAILED: Pool is being shut down. pool:[");
						stringBuilder.Append(this.guid.ToString());
						stringBuilder.Append("]");
						ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, stringBuilder.ToString());
					}
					return ErrorCode.CreateInvalidPool((LID)56216U);
				}
				if (!persistentAvlTree.TryGetValue(sessionHandle, out handle))
				{
					if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						StringBuilder stringBuilder2 = new StringBuilder(100);
						stringBuilder2.Append("CALL FAILED: error[RpcInvalidSession]. Invalid pool session handle. pool:[");
						stringBuilder2.Append(this.guid.ToString());
						stringBuilder2.Append("] session:[");
						stringBuilder2.Append(sessionHandle);
						stringBuilder2.Append("]");
						ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, stringBuilder2.ToString());
					}
					return ErrorCode.CreateRpcInvalidSession((LID)39832U);
				}
				return ErrorCode.NoError;
			}

			// Token: 0x0600028D RID: 653 RVA: 0x00039454 File Offset: 0x00037654
			public ErrorCode SessionCreated(IntPtr sessionHandle, out uint handle)
			{
				ErrorCode errorCode = ErrorCode.NoError;
				handle = 0U;
				if (this.sessionHandleMap != null)
				{
					using (LockManager.Lock(this))
					{
						if (this.sessionHandleMap != null)
						{
							handle = (uint)sessionHandle.ToInt32();
							this.sessionHandleMap = this.sessionHandleMap.SetValue(handle, sessionHandle);
						}
						else
						{
							errorCode = ErrorCode.CreateInvalidPool((LID)43928U);
						}
						goto IL_6F;
					}
				}
				errorCode = ErrorCode.CreateInvalidPool((LID)60312U);
				IL_6F:
				if (errorCode == ErrorCodeValue.InvalidPool && ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					StringBuilder stringBuilder = new StringBuilder(100);
					stringBuilder.Append("CALL FAILED: Pool is being shut down. pool:[");
					stringBuilder.Append(this.guid.ToString());
					stringBuilder.Append("]");
					ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, stringBuilder.ToString());
				}
				return errorCode;
			}

			// Token: 0x0600028E RID: 654 RVA: 0x00039548 File Offset: 0x00037748
			public void SessionClosed(uint sessionHandle)
			{
				bool flag = true;
				if (this.sessionHandleMap != null)
				{
					using (LockManager.Lock(this))
					{
						if (this.sessionHandleMap != null)
						{
							flag = false;
							bool flag2;
							this.sessionHandleMap = this.sessionHandleMap.Remove(sessionHandle, out flag2);
						}
					}
				}
				if (flag && ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					StringBuilder stringBuilder = new StringBuilder(100);
					stringBuilder.Append("Pool session map not changed: pool is being shut down. pool:[");
					stringBuilder.Append(this.guid.ToString());
					stringBuilder.Append("]");
					ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, stringBuilder.ToString());
				}
			}

			// Token: 0x0600028F RID: 655 RVA: 0x00039600 File Offset: 0x00037800
			public ErrorCode QueueNotificationWait(IPoolWaitForNotificationsCompletion completion)
			{
				ErrorCode errorCode = ErrorCode.NoError;
				uint[] array = null;
				if (this.sessionHandleMap != null)
				{
					using (LockManager.Lock(this))
					{
						if (this.sessionHandleMap != null)
						{
							if (this.pendingCompletion == null)
							{
								if (this.pendingNotificationSet.Count > 0)
								{
									array = new uint[this.pendingNotificationSet.Count];
									this.pendingNotificationSet.CopyTo(array);
									this.pendingNotificationSet.Clear();
									this.waitQueuedTime = null;
								}
								else
								{
									this.waitQueuedTime = new DateTime?(DateTime.UtcNow);
									this.pendingCompletion = completion;
									completion = null;
								}
							}
							else
							{
								if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.DebugTrace))
								{
									StringBuilder stringBuilder = new StringBuilder(100);
									stringBuilder.Append("CALL FAILED: Another notification wait call is already queued on the pool. pool:[");
									stringBuilder.Append(this.guid.ToString());
									stringBuilder.Append("]");
									ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, stringBuilder.ToString());
								}
								errorCode = ErrorCode.CreateRejected((LID)42136U);
							}
						}
						else
						{
							errorCode = ErrorCode.CreateInvalidPool((LID)50328U);
						}
						goto IL_12A;
					}
				}
				errorCode = ErrorCode.CreateInvalidPool((LID)62616U);
				IL_12A:
				if (errorCode == ErrorCode.NoError)
				{
					if (completion != null)
					{
						completion.CompleteAsyncCall(array);
						if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							StringBuilder stringBuilder2 = new StringBuilder(100);
							stringBuilder2.Append("Notification wait RPC call completed successfully. pool:[");
							stringBuilder2.Append(this.guid.ToString());
							stringBuilder2.Append("]");
							ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, stringBuilder2.ToString());
						}
					}
					else
					{
						if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							StringBuilder stringBuilder3 = new StringBuilder(100);
							stringBuilder3.Append("Notification wait has been registered successfully. pool:[");
							stringBuilder3.Append(this.guid.ToString());
							stringBuilder3.Append("]");
							ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, stringBuilder3.ToString());
						}
						StorePerDatabasePerformanceCountersInstance databaseInstance = PerformanceCounterFactory.GetDatabaseInstance(null);
						if (databaseInstance != null)
						{
							databaseInstance.ContextHandlePoolParkedCalls.Increment();
						}
					}
				}
				else if (errorCode == ErrorCodeValue.InvalidPool && ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					StringBuilder stringBuilder4 = new StringBuilder(100);
					stringBuilder4.Append("CALL FAILED: Pool is being shut down. pool:[");
					stringBuilder4.Append(this.guid.ToString());
					stringBuilder4.Append("]");
					ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, stringBuilder4.ToString());
				}
				return errorCode;
			}

			// Token: 0x06000290 RID: 656 RVA: 0x000398A4 File Offset: 0x00037AA4
			public void ExpireNotificationWait(ErrorCode error, DateTime currentTime)
			{
				IPoolWaitForNotificationsCompletion poolWaitForNotificationsCompletion = null;
				uint[] array = null;
				using (LockManager.Lock(this))
				{
					if (this.pendingCompletion != null && !this.completionQueued && (error != ErrorCode.NoError || (this.waitQueuedTime != null && this.waitQueuedTime.Value < currentTime && currentTime.Subtract(this.waitQueuedTime.Value) >= PoolRpcServer.WaitExpirationPeriod)))
					{
						if (error == ErrorCode.NoError && this.pendingNotificationSet.Count > 0)
						{
							array = new uint[this.pendingNotificationSet.Count];
							this.pendingNotificationSet.CopyTo(array);
							this.pendingNotificationSet.Clear();
						}
						poolWaitForNotificationsCompletion = this.pendingCompletion;
						this.pendingCompletion = null;
						this.waitQueuedTime = null;
					}
				}
				if (poolWaitForNotificationsCompletion != null)
				{
					if (error == ErrorCode.NoError)
					{
						poolWaitForNotificationsCompletion.CompleteAsyncCall(array);
					}
					else
					{
						poolWaitForNotificationsCompletion.FailAsyncCall((int)error, RpcServerBase.EmptyArraySegment);
					}
					if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						StringBuilder stringBuilder = new StringBuilder(100);
						stringBuilder.Append("Notification wait RPC call expired. pool:[");
						stringBuilder.Append(this.guid.ToString());
						stringBuilder.Append("]");
						if (error != ErrorCode.NoError)
						{
							stringBuilder.Append(" error:[");
							stringBuilder.Append(error.ToString());
							stringBuilder.Append("]");
						}
						ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, stringBuilder.ToString());
					}
					StorePerDatabasePerformanceCountersInstance databaseInstance = PerformanceCounterFactory.GetDatabaseInstance(null);
					if (databaseInstance != null)
					{
						databaseInstance.ContextHandlePoolParkedCalls.Decrement();
					}
				}
			}

			// Token: 0x06000291 RID: 657 RVA: 0x00039A74 File Offset: 0x00037C74
			public void NotificationPending(int sessionId)
			{
				bool flag = true;
				if (this.sessionHandleMap != null)
				{
					using (LockManager.Lock(this))
					{
						if (this.sessionHandleMap != null)
						{
							flag = false;
							IntPtr zero = IntPtr.Zero;
							if (this.sessionHandleMap.TryGetValue((uint)sessionId, out zero))
							{
								this.pendingNotificationSet.Add((uint)sessionId);
								if (this.pendingCompletion != null && !this.completionQueued)
								{
									this.completionQueued = ThreadPool.QueueUserWorkItem(new WaitCallback(this.CompleteWait), null);
									if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.DebugTrace))
									{
										StringBuilder stringBuilder = new StringBuilder(100);
										stringBuilder.Append("Async RPC completion is queued to thread pool. pool:[");
										stringBuilder.Append(this.guid.ToString());
										stringBuilder.Append("] session:[");
										stringBuilder.Append((uint)sessionId);
										stringBuilder.Append("]");
										ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, stringBuilder.ToString());
									}
								}
							}
							else if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								StringBuilder stringBuilder2 = new StringBuilder(100);
								stringBuilder2.Append("CALLBACK IGNORED: Invalid pool session handle. pool:[");
								stringBuilder2.Append(this.guid.ToString());
								stringBuilder2.Append("] session:[");
								stringBuilder2.Append(sessionId);
								stringBuilder2.Append("]");
								ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, stringBuilder2.ToString());
							}
						}
					}
				}
				if (flag && ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					StringBuilder stringBuilder3 = new StringBuilder(100);
					stringBuilder3.Append("CALLBACK IGNORED: Pool is closed. pool:[");
					stringBuilder3.Append(this.guid.ToString());
					stringBuilder3.Append("] session:[");
					stringBuilder3.Append(sessionId);
					stringBuilder3.Append("]");
					ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, stringBuilder3.ToString());
				}
			}

			// Token: 0x06000292 RID: 658 RVA: 0x00039C7C File Offset: 0x00037E7C
			private void CompleteWait(object state)
			{
				IPoolWaitForNotificationsCompletion poolWaitForNotificationsCompletion = null;
				uint[] array = null;
				using (LockManager.Lock(this))
				{
					this.completionQueued = false;
					poolWaitForNotificationsCompletion = this.pendingCompletion;
					this.pendingCompletion = null;
					if (poolWaitForNotificationsCompletion != null && this.pendingNotificationSet.Count > 0)
					{
						array = new uint[this.pendingNotificationSet.Count];
						this.pendingNotificationSet.CopyTo(array);
						this.pendingNotificationSet.Clear();
					}
				}
				if (poolWaitForNotificationsCompletion != null)
				{
					poolWaitForNotificationsCompletion.CompleteAsyncCall(array);
					if (ExTraceGlobals.RpcContextPoolTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						StringBuilder stringBuilder = new StringBuilder(100);
						stringBuilder.Append("Notification wait RPC call completed successfully. pool:[");
						stringBuilder.Append(this.guid.ToString());
						stringBuilder.Append("] sessions:[");
						stringBuilder.AppendAsString(array);
						stringBuilder.Append("]");
						ExTraceGlobals.RpcContextPoolTracer.TraceDebug(0L, stringBuilder.ToString());
					}
					StorePerDatabasePerformanceCountersInstance databaseInstance = PerformanceCounterFactory.GetDatabaseInstance(null);
					if (databaseInstance != null)
					{
						databaseInstance.ContextHandlePoolParkedCalls.Decrement();
					}
				}
			}

			// Token: 0x0400017E RID: 382
			private int contextHandles;

			// Token: 0x0400017F RID: 383
			private Guid guid;

			// Token: 0x04000180 RID: 384
			private HashSet<uint> pendingNotificationSet;

			// Token: 0x04000181 RID: 385
			private IPoolWaitForNotificationsCompletion pendingCompletion;

			// Token: 0x04000182 RID: 386
			private DateTime? waitQueuedTime;

			// Token: 0x04000183 RID: 387
			private bool completionQueued;

			// Token: 0x04000184 RID: 388
			private PersistentAvlTree<uint, IntPtr> sessionHandleMap;
		}

		// Token: 0x02000015 RID: 21
		internal class PoolContextMap
		{
			// Token: 0x06000293 RID: 659 RVA: 0x00039D90 File Offset: 0x00037F90
			private PoolContextMap()
			{
				this.ContextHandles = PersistentAvlTree<int, PoolRpcServer.ContextHandlePool>.Empty;
				this.Pools = PersistentAvlTree<Guid, PoolRpcServer.ContextHandlePool>.Empty;
			}

			// Token: 0x06000294 RID: 660 RVA: 0x00039DAE File Offset: 0x00037FAE
			private PoolContextMap(PersistentAvlTree<int, PoolRpcServer.ContextHandlePool> contextHandles, PersistentAvlTree<Guid, PoolRpcServer.ContextHandlePool> pools)
			{
				this.ContextHandles = contextHandles;
				this.Pools = pools;
			}

			// Token: 0x06000295 RID: 661 RVA: 0x00039DC4 File Offset: 0x00037FC4
			public static PoolRpcServer.PoolContextMap Create(PersistentAvlTree<int, PoolRpcServer.ContextHandlePool> contextHandles, PersistentAvlTree<Guid, PoolRpcServer.ContextHandlePool> pools)
			{
				if (contextHandles.Count != 0 || pools.Count != 0)
				{
					return new PoolRpcServer.PoolContextMap(contextHandles, pools);
				}
				return PoolRpcServer.PoolContextMap.Empty;
			}

			// Token: 0x04000185 RID: 389
			public static readonly PoolRpcServer.PoolContextMap Empty = new PoolRpcServer.PoolContextMap();

			// Token: 0x04000186 RID: 390
			public readonly PersistentAvlTree<int, PoolRpcServer.ContextHandlePool> ContextHandles;

			// Token: 0x04000187 RID: 391
			public readonly PersistentAvlTree<Guid, PoolRpcServer.ContextHandlePool> Pools;
		}

		// Token: 0x02000016 RID: 22
		private sealed class PoolRpcServerEndpoint : PoolRpcServerBase
		{
			// Token: 0x06000297 RID: 663 RVA: 0x00039DEF File Offset: 0x00037FEF
			public override int GetInterfaceInstance(Guid instanceGuid, out IPoolRpcServer instance)
			{
				instance = PoolRpcServer.Instance;
				if (instance == null)
				{
					return -2147221227;
				}
				return ErrorCode.NoError;
			}

			// Token: 0x06000298 RID: 664 RVA: 0x00039E0C File Offset: 0x0003800C
			public override void ConnectionDropped(IntPtr contextHandle)
			{
				PoolRpcServer instance = PoolRpcServer.Instance;
				if (instance != null)
				{
					instance.EcPoolDisconnect(contextHandle);
				}
			}
		}

		// Token: 0x02000017 RID: 23
		private sealed class PoolNotifyRpcServerEndpoint : PoolNotifyRpcServerBase
		{
			// Token: 0x0600029A RID: 666 RVA: 0x00039E32 File Offset: 0x00038032
			public override int GetInterfaceInstance(Guid instanceGuid, out IPoolRpcServer instance)
			{
				instance = PoolRpcServer.Instance;
				if (instance == null)
				{
					return -2147221227;
				}
				return ErrorCode.NoError;
			}

			// Token: 0x0600029B RID: 667 RVA: 0x00039E50 File Offset: 0x00038050
			public override void ConnectionDropped(IntPtr contextHandle)
			{
				PoolRpcServer instance = PoolRpcServer.Instance;
				if (instance != null)
				{
					instance.EcPoolDisconnect(contextHandle);
				}
			}
		}
	}
}
