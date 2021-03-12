using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net.MapiHttp;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Handler;
using Microsoft.Exchange.RpcClientAccess.Messages;
using Microsoft.Exchange.RpcClientAccess.Monitoring;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.RpcClientAccess.Server;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000013 RID: 19
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmsmdbHttpHandler : MapiHttpHandler
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000060A0 File Offset: 0x000042A0
		internal static IExchangeAsyncDispatch ExchangeAsyncDispatch
		{
			get
			{
				return EmsmdbHttpHandler.exchangeAsyncDispatch;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000060A7 File Offset: 0x000042A7
		internal override string EndpointVdirPath
		{
			get
			{
				return MapiHttpEndpoints.VdirPathEmsmdb;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000060AE File Offset: 0x000042AE
		internal override IAsyncOperationFactory OperationFactory
		{
			get
			{
				return EmsmdbHttpHandler.operationFactory;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000060B8 File Offset: 0x000042B8
		internal override bool TryEnsureHandlerIsInitialized()
		{
			if (EmsmdbHttpHandler.shutdownTime != null)
			{
				return false;
			}
			if (!EmsmdbHttpHandler.initialized)
			{
				lock (EmsmdbHttpHandler.initializeLock)
				{
					if (EmsmdbHttpHandler.shutdownTime != null)
					{
						return false;
					}
					if (!EmsmdbHttpHandler.initialized)
					{
						MapiHttpHandler.IsValidContextHandleDelegate = new Func<object, bool>(EmsmdbHttpHandler.InternalIsValidContextHandle);
						MapiHttpHandler.TryContextHandleRundownDelegate = new Func<object, bool>(EmsmdbHttpHandler.InternalTryContextHandleRundown);
						MapiHttpHandler.QueueDroppedConnectionDelegate = new Action<object>(EmsmdbHttpHandler.InternalQueueDroppedConnection);
						MapiHttpHandler.ShutdownHandlerDelegate = new Action(EmsmdbHttpHandler.InternalShutdownHandler);
						MapiHttpHandler.NeedTokenRehydrationDelegate = new Func<string, bool>(EmsmdbHttpHandler.InternalNeedTokenRehydration);
						EmsmdbHttpHandler.InitializeRpcClientAccess();
						EmsmdbHttpHandler.initialized = true;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00006184 File Offset: 0x00004384
		internal override void LogFailure(IList<string> requestIds, IList<string> cookies, string message, string userName, string protocolSequence, string clientAddress, string organization, Exception exception, Microsoft.Exchange.Diagnostics.Trace trace)
		{
			ProtocolLog.LogMapiHttpProtocolFailure(requestIds, cookies, "mapihttp: failure", message ?? string.Empty, exception, userName ?? string.Empty, organization ?? string.Empty, protocolSequence, clientAddress ?? string.Empty, trace);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000061D0 File Offset: 0x000043D0
		private static void InternalShutdownHandler()
		{
			try
			{
				if (EmsmdbHttpHandler.shutdownTime == null)
				{
					EmsmdbHttpHandler.shutdownTime = new ExDateTime?(ExDateTime.Now);
					lock (EmsmdbHttpHandler.initializeLock)
					{
						if (EmsmdbHttpHandler.initialized)
						{
							EmsmdbHttpHandler.ShutdownRpcClientAccess();
							EmsmdbHttpHandler.initialized = false;
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000624C File Offset: 0x0000444C
		private static bool InternalIsValidContextHandle(object contextHandle)
		{
			if (contextHandle == null)
			{
				return false;
			}
			IntPtr? intPtr = contextHandle as IntPtr?;
			return intPtr != null && !(intPtr.Value == IntPtr.Zero);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000062AC File Offset: 0x000044AC
		private static bool InternalTryContextHandleRundown(object contextHandle)
		{
			IntPtr? localContextHandle = contextHandle as IntPtr?;
			if (localContextHandle == null)
			{
				return true;
			}
			if (localContextHandle.Value == IntPtr.Zero)
			{
				return true;
			}
			MapiHttpHandler.DispatchCallSync(delegate
			{
				EmsmdbHttpHandler.ExchangeAsyncDispatch.ContextHandleRundown(localContextHandle.Value);
			});
			return true;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000630C File Offset: 0x0000450C
		private static void InternalQueueDroppedConnection(object contextHandle)
		{
			IntPtr? intPtr = contextHandle as IntPtr?;
			if (intPtr == null)
			{
				return;
			}
			if (intPtr.Value == IntPtr.Zero)
			{
				return;
			}
			bool flag = false;
			lock (EmsmdbHttpHandler.connectionDroppedQueueLock)
			{
				EmsmdbHttpHandler.connectionDroppedQueue.Enqueue(intPtr.Value);
				if (!EmsmdbHttpHandler.isConnectionDroppedThreadRunning)
				{
					EmsmdbHttpHandler.isConnectionDroppedThreadRunning = true;
					if (!ThreadPool.QueueUserWorkItem(new WaitCallback(EmsmdbHttpHandler.ProcessConnectionDroppedQueue)))
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
				EmsmdbHttpHandler.ProcessConnectionDroppedQueue(null);
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000063CC File Offset: 0x000045CC
		private static void ProcessConnectionDroppedQueue(object state)
		{
			bool flag = false;
			try
			{
				for (;;)
				{
					IntPtr contextHandle;
					lock (EmsmdbHttpHandler.connectionDroppedQueueLock)
					{
						if (EmsmdbHttpHandler.connectionDroppedQueue.Count <= 0)
						{
							EmsmdbHttpHandler.isConnectionDroppedThreadRunning = false;
							flag = true;
							break;
						}
						contextHandle = EmsmdbHttpHandler.connectionDroppedQueue.Dequeue();
					}
					MapiHttpHandler.DispatchCallSync(delegate
					{
						EmsmdbHttpHandler.ExchangeAsyncDispatch.DroppedConnection(contextHandle);
					});
				}
			}
			finally
			{
				if (!flag)
				{
					EmsmdbHttpHandler.isConnectionDroppedThreadRunning = false;
				}
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006460 File Offset: 0x00004660
		private static bool InternalNeedTokenRehydration(string requestType)
		{
			return !string.IsNullOrWhiteSpace(requestType) && (string.Compare(requestType, "Connect", true) == 0 || string.Compare(requestType, "EcDoConnectEx", true) == 0 || string.Compare(requestType, "Dummy", true) == 0 || string.Compare(requestType, "EcDoDummy", true) == 0);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000064B0 File Offset: 0x000046B0
		private static IRpcDispatch CreateRpcDispatch()
		{
			IRpcDispatch result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				RpcDispatch rpcDispatch = new RpcDispatch(ConnectionHandler.Factory, new DriverFactory());
				disposeGuard.Add<RpcDispatch>(rpcDispatch);
				IRpcDispatch rpcDispatch2 = new WatsonOnUnhandledExceptionDispatch(rpcDispatch);
				disposeGuard.Success();
				result = rpcDispatch2;
			}
			return result;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00006510 File Offset: 0x00004710
		private static IExchangeDispatch CreateExchangeDispatch(IRpcDispatch rpcDispatch)
		{
			return new ExchangeDispatch(rpcDispatch);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006518 File Offset: 0x00004718
		private static IExchangeAsyncDispatch CreateExchangeAsyncDispatch(IExchangeDispatch exchangeDispatch, DispatchPool dispatchPool)
		{
			return new ExchangeAsyncDispatch(exchangeDispatch, dispatchPool);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00006524 File Offset: 0x00004724
		private static DispatchPool CreateDispatchPool()
		{
			return new DispatchPool("RpcClientAccess-MapiHttp", Configuration.ServiceConfiguration.MaximumRpcTasks, Configuration.ServiceConfiguration.MaximumRpcThreads, Configuration.ServiceConfiguration.MinimumRpcThreads, RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.DispatchTaskQueueLength, RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.DispatchTaskThreads, RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.DispatchTaskActiveThreads, RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.DispatchTaskOperationsRate);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00006594 File Offset: 0x00004794
		private static void LogConfigurationEventConfig(ExEventLog.EventTuple tuple, params object[] args)
		{
			string periodicKey = null;
			if (tuple.Period == ExEventLog.EventPeriod.LogPeriodic)
			{
				periodicKey = args.Aggregate(tuple.EventId.GetHashCode(), (int hashCode, object arg) => hashCode ^= ((arg != null) ? arg.GetHashCode() : 0)).ToString();
			}
			EmsmdbHttpHandler.eventLogger.LogEvent(tuple, periodicKey, args);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000065F8 File Offset: 0x000047F8
		private static void InitializeRpcClientAccess()
		{
			ExMonHandler.IsEnabled = false;
			Configuration.AppConfigFileName = string.Format("{0}ClientAccess\\mapi\\emsmdb\\web.config", ExchangeSetupContext.InstallPath);
			ProtocolLogConfiguration.SetDefaults(string.Format("{0}Logging\\MAPI Client Access\\", ExchangeSetupContext.InstallPath), "MAPI Mailbox Protocol Logs", "MAPIMB_", "MAPIMailboxProtocolLogs");
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				EmsmdbHttpHandler.processId = currentProcess.Id;
			}
			EmsmdbHttpHandler.eventLogger = new ExEventLog(EmsmdbHttpHandler.ComponentGuid, "MSExchangeMapiMailboxAppPool");
			EmsmdbHttpHandler.eventLogger.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_StartingMSExchangeMapiMailboxAppPool, string.Empty, new object[]
			{
				EmsmdbHttpHandler.processId,
				"Microsoft Exchange",
				"15.00.1497.010"
			});
			Configuration.EventLogger = new ConfigurationSchema.EventLogger(EmsmdbHttpHandler.LogConfigurationEventConfig);
			int num = Privileges.RemoveAllExcept(new string[]
			{
				"SeAuditPrivilege",
				"SeChangeNotifyPrivilege",
				"SeCreateGlobalPrivilege",
				"SeTcbPrivilege"
			}, "MSExchangeMapiMailboxAppPool");
			if (num != 0)
			{
				EmsmdbHttpHandler.eventLogger.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_MapiMailboxRemovingPrivilegeErrorOnStart, string.Empty, new object[0]);
				string failureDescription = string.Format("Failed to remove privileges from {0}, error code = {1}", "MSExchangeMapiMailboxAppPool", num);
				throw ProtocolException.FromResponseCode((LID)48028, failureDescription, ResponseCode.EndpointDisabled, null);
			}
			EmsmdbHttpHandler.configurationManager = new ConfigurationManager();
			EmsmdbHttpHandler.configurationManager.LoadAndRegisterForNotifications();
			ProtocolLog.Initialize();
			RpcClientAccessPerformanceCountersWrapper.Initialize(new EmsmdbPerformanceCountersWrapper(), new NullRpcHttpConnectionRegistrationPerformanceCounters(), new NullXtcPerformanceCounters());
			EmsmdbHttpHandler.dispatchPool = EmsmdbHttpHandler.CreateDispatchPool();
			EmsmdbHttpHandler.rpcDispatch = EmsmdbHttpHandler.CreateRpcDispatch();
			EmsmdbHttpHandler.exchangeDispatch = EmsmdbHttpHandler.CreateExchangeDispatch(EmsmdbHttpHandler.rpcDispatch);
			EmsmdbHttpHandler.exchangeAsyncDispatch = EmsmdbHttpHandler.CreateExchangeAsyncDispatch(EmsmdbHttpHandler.exchangeDispatch, EmsmdbHttpHandler.dispatchPool);
			EmsmdbHttpHandler.eventLogger.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_MSExchangeMapiMailboxAppPoolStartSuccess, string.Empty, new object[]
			{
				EmsmdbHttpHandler.processId,
				"Microsoft Exchange",
				"15.00.1497.010"
			});
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000067F0 File Offset: 0x000049F0
		private static void ShutdownRpcClientAccess()
		{
			EmsmdbHttpHandler.eventLogger.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_StoppingMSExchangeMapiMailboxAppPool, string.Empty, new object[]
			{
				EmsmdbHttpHandler.processId,
				"Microsoft Exchange",
				"15.00.1497.010"
			});
			if (EmsmdbHttpHandler.dispatchPool != null)
			{
				while (ExDateTime.Now - EmsmdbHttpHandler.shutdownTime.Value < EmsmdbHttpHandler.waitDrainOnShutdown)
				{
					Thread.Sleep(500);
					if (EmsmdbHttpHandler.dispatchPool.ActiveThreads == 0)
					{
						break;
					}
				}
			}
			Util.DisposeIfPresent(EmsmdbHttpHandler.dispatchPool);
			Util.DisposeIfPresent(EmsmdbHttpHandler.rpcDispatch);
			Util.DisposeIfPresent(EmsmdbHttpHandler.configurationManager);
			ProtocolLog.Shutdown();
			EmsmdbHttpHandler.eventLogger.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_MSExchangeMapiMailboxAppPoolStopSuccess, string.Empty, new object[]
			{
				EmsmdbHttpHandler.processId,
				"Microsoft Exchange",
				"15.00.1497.010"
			});
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00006924 File Offset: 0x00004B24
		// Note: this type is marked as 'beforefieldinit'.
		static EmsmdbHttpHandler()
		{
			Dictionary<string, Func<HttpContextBase, AsyncOperation>> dictionary = new Dictionary<string, Func<HttpContextBase, AsyncOperation>>();
			dictionary.Add("Connect", (HttpContextBase context) => new EmsmdbConnectAsyncOperation(context));
			dictionary.Add("Disconnect", (HttpContextBase context) => new EmsmdbDisconnectAsyncOperation(context));
			dictionary.Add("Execute", (HttpContextBase context) => new EmsmdbExecuteAsyncOperation(context));
			dictionary.Add("NotificationWait", (HttpContextBase context) => new EmsmdbNotificationWaitAsyncOperation(context));
			dictionary.Add("Dummy", (HttpContextBase context) => new EmsmdbDummyAsyncOperation(context));
			dictionary.Add("EcDoConnectEx", (HttpContextBase context) => new EmsmdbLegacyConnectAsyncOperation(context));
			dictionary.Add("EcDoDisconnect", (HttpContextBase context) => new EmsmdbLegacyDisconnectAsyncOperation(context));
			dictionary.Add("EcDoRpcExt2", (HttpContextBase context) => new EmsmdbLegacyExecuteAsyncOperation(context));
			dictionary.Add("EcDoAsyncWaitEx", (HttpContextBase context) => new EmsmdbLegacyNotificationWaitAsyncOperation(context));
			dictionary.Add("EcDoDummy", (HttpContextBase context) => new EmsmdbDummyAsyncOperation(context));
			EmsmdbHttpHandler.operationFactory = new DictionaryBasedOperationFactory(dictionary);
			EmsmdbHttpHandler.waitDrainOnShutdown = TimeSpan.FromSeconds(30.0);
			EmsmdbHttpHandler.initializeLock = new object();
			EmsmdbHttpHandler.connectionDroppedQueueLock = new object();
			EmsmdbHttpHandler.connectionDroppedQueue = new Queue<IntPtr>();
			EmsmdbHttpHandler.initialized = false;
			EmsmdbHttpHandler.isConnectionDroppedThreadRunning = false;
			EmsmdbHttpHandler.shutdownTime = null;
			EmsmdbHttpHandler.configurationManager = null;
		}

		// Token: 0x0400006F RID: 111
		public const string ApplicationPoolName = "MSExchangeMapiMailboxAppPool";

		// Token: 0x04000070 RID: 112
		private const string LogTypeName = "MAPI Mailbox Protocol Logs";

		// Token: 0x04000071 RID: 113
		private const string LogFilePrefix = "MAPIMB_";

		// Token: 0x04000072 RID: 114
		private const string LogComponent = "MAPIMailboxProtocolLogs";

		// Token: 0x04000073 RID: 115
		private static readonly Guid ComponentGuid = new Guid("84036911-D9A7-4CD5-8162-861C43E08CA5");

		// Token: 0x04000074 RID: 116
		private static readonly IAsyncOperationFactory operationFactory;

		// Token: 0x04000075 RID: 117
		private static readonly TimeSpan waitDrainOnShutdown;

		// Token: 0x04000076 RID: 118
		private static readonly object initializeLock;

		// Token: 0x04000077 RID: 119
		private static readonly object connectionDroppedQueueLock;

		// Token: 0x04000078 RID: 120
		private static readonly Queue<IntPtr> connectionDroppedQueue;

		// Token: 0x04000079 RID: 121
		private static bool initialized;

		// Token: 0x0400007A RID: 122
		private static bool isConnectionDroppedThreadRunning;

		// Token: 0x0400007B RID: 123
		private static ExDateTime? shutdownTime;

		// Token: 0x0400007C RID: 124
		private static ConfigurationManager configurationManager;

		// Token: 0x0400007D RID: 125
		private static ExEventLog eventLogger;

		// Token: 0x0400007E RID: 126
		private static IRpcDispatch rpcDispatch;

		// Token: 0x0400007F RID: 127
		private static IExchangeDispatch exchangeDispatch;

		// Token: 0x04000080 RID: 128
		private static IExchangeAsyncDispatch exchangeAsyncDispatch;

		// Token: 0x04000081 RID: 129
		private static DispatchPool dispatchPool;

		// Token: 0x04000082 RID: 130
		private static int processId;
	}
}
