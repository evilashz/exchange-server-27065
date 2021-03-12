using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.RpcProxy;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.AdminRpc;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.DirectoryServices;
using Microsoft.Exchange.Server.Storage.WorkerManager;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Server.Storage.RpcProxy
{
	// Token: 0x02000019 RID: 25
	internal class RpcInstanceManager : IRpcInstanceManager
	{
		// Token: 0x060000BF RID: 191 RVA: 0x0000A53C File Offset: 0x0000873C
		public RpcInstanceManager(IWorkerManager workerManager, int nonRecoveryDatabasesMax, int recoveryDatabasesMax, int activeDatabasesMax)
		{
			this.instances = PersistentAvlTree<Guid, RpcInstanceManager.RpcInstance>.Empty;
			this.workerManager = workerManager;
			this.workerComplete = new Action<IWorkerProcess>(this.OnWorkerComplete);
			this.notificationQueue = new Queue<RpcInstanceManager.NotificationLoop>(16);
			this.eventsProcessing = false;
			this.instanceLimitChecker = new RpcInstanceManager.InstancesLimitsChecker(nonRecoveryDatabasesMax, recoveryDatabasesMax, activeDatabasesMax, this.workerManager);
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060000C0 RID: 192 RVA: 0x0000A5A8 File Offset: 0x000087A8
		// (remove) Token: 0x060000C1 RID: 193 RVA: 0x0000A5E0 File Offset: 0x000087E0
		public event OnPoolNotificationsReceivedCallback NotificationsReceived;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060000C2 RID: 194 RVA: 0x0000A618 File Offset: 0x00008818
		// (remove) Token: 0x060000C3 RID: 195 RVA: 0x0000A650 File Offset: 0x00008850
		public event OnRpcInstanceClosedCallback RpcInstanceClosed;

		// Token: 0x060000C4 RID: 196 RVA: 0x0000A688 File Offset: 0x00008888
		public virtual void StopAcceptingCalls()
		{
			PersistentAvlTree<Guid, RpcInstanceManager.RpcInstance> persistentAvlTree = null;
			using (LockManager.Lock(this.syncRoot))
			{
				persistentAvlTree = this.GetInstanceMap();
				Interlocked.Exchange<PersistentAvlTree<Guid, RpcInstanceManager.RpcInstance>>(ref this.instances, null);
			}
			if (persistentAvlTree != null)
			{
				foreach (RpcInstanceManager.RpcInstance rpcInstance in persistentAvlTree.GetValuesLmr())
				{
					using (LockManager.Lock(this.syncRoot))
					{
						rpcInstance.InstanceNotificationLoop.Cancelled = true;
						this.notificationQueue.Enqueue(rpcInstance.InstanceNotificationLoop);
					}
					if (rpcInstance.AdminClient != null)
					{
						rpcInstance.AdminClient.Close();
					}
					this.OnPoolNotificationReceived();
					if (rpcInstance.PoolClient != null)
					{
						rpcInstance.PoolClient.Close();
					}
				}
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000A78C File Offset: 0x0000898C
		public virtual ErrorCode StartInstance(Guid instanceId, uint flags, ref bool isNewInstanceStarted, CancellationToken cancellationToken)
		{
			IWorkerProcess workerProcess = null;
			ErrorCode errorCode = ErrorCode.NoError;
			bool flag = false;
			RpcInstanceManager.NotificationLoop notificationLoop = null;
			bool flag2 = false;
			DatabaseType databaseType = DatabaseType.None;
			isNewInstanceStarted = false;
			try
			{
				if (ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection))
				{
					ExTraceGlobals.FaultInjectionTracer.TraceTest<Guid>(2462461245U, ref instanceId);
				}
				DatabaseType databaseType2 = DatabaseType.None;
				((IRpcProxyDirectory)Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory).RefreshDatabaseInfo(NullExecutionContext.Instance, instanceId);
				DatabaseInfo databaseInfo = ((IRpcProxyDirectory)Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory).GetDatabaseInfo(NullExecutionContext.Instance, instanceId);
				string mdbName = databaseInfo.MdbName;
				bool flag3 = (flags & 8U) == 8U;
				databaseType2 = (databaseInfo.IsRecoveryDatabase ? DatabaseType.Recovery : (flag3 ? DatabaseType.Passive : DatabaseType.Active));
				using (LockManager.Lock(this.syncRoot))
				{
					if (this.IsInstanceStarted(instanceId))
					{
						flag2 = true;
						errorCode = this.workerManager.GetWorker(instanceId, out workerProcess);
						if (ErrorCode.NoError != errorCode)
						{
							return errorCode.Propagate((LID)48416U);
						}
						databaseType = workerProcess.InstanceDBType;
						workerProcess = null;
						errorCode = this.UpdateDatabaseTypeOnWorker(instanceId, mdbName, databaseType2);
						if (ErrorCode.NoError != errorCode)
						{
							return errorCode.Propagate((LID)35408U);
						}
					}
				}
				if (!flag3 && (!flag2 || (flag2 && databaseType == DatabaseType.Passive)))
				{
					NotificationItem notificationItem = new EventNotificationItem(ExchangeComponent.Store.Name, "DatabaseMountingTrigger", mdbName, ResultSeverityLevel.Error);
					notificationItem.Publish(false);
				}
				if (flag2)
				{
					return ErrorCode.NoError;
				}
				errorCode = this.instanceLimitChecker.EcCheckDatabasesLimits(instanceId, mdbName, databaseType2, false);
				if (ErrorCode.NoError != errorCode)
				{
					return errorCode.Propagate((LID)35388U);
				}
				DiagnosticContext.TraceDword((LID)33856U, (uint)Environment.TickCount);
				errorCode = this.workerManager.StartWorker(RpcInstanceManager.GetWorkerProcessPathName(RpcInstanceManager.workerProcessName), instanceId, databaseInfo.DagOrServerGuid, mdbName, this.workerComplete, cancellationToken, out workerProcess);
				DiagnosticContext.TraceDword((LID)50240U, (uint)Environment.TickCount);
				if (ErrorCode.NoError == errorCode)
				{
					using (LockManager.Lock(this.syncRoot))
					{
						errorCode = this.EcCheckDatabasesLimits(instanceId, mdbName, databaseType2, false);
						if (ErrorCode.NoError == errorCode)
						{
							workerProcess.InstanceDBType = databaseType2;
							if (ErrorCode.NoError == errorCode)
							{
								notificationLoop = this.RegisterInstance(instanceId, workerProcess);
							}
							flag = true;
						}
						else
						{
							if (ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.ErrorTrace))
							{
								ExTraceGlobals.ProxyAdminTracer.TraceError(0L, "Failed to pass databases limits check");
							}
							DatabaseFailureItem databaseFailureItem = new DatabaseFailureItem(FailureNameSpace.Store, FailureTag.GenericMountFailure, instanceId)
							{
								ComponentName = "RpcProxy",
								InstanceName = mdbName
							};
							databaseFailureItem.Publish();
						}
					}
					if (notificationLoop != null)
					{
						notificationLoop.StateMachine.MoveNext();
					}
				}
			}
			catch (DatabaseNotFoundException ex)
			{
				NullExecutionContext.Instance.Diagnostics.OnExceptionCatch(ex);
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_NoDatabase, new object[]
				{
					ex
				});
				errorCode = ErrorCode.CreateNotFound((LID)55536U);
			}
			catch (DirectoryTransientErrorException ex2)
			{
				NullExecutionContext.Instance.Diagnostics.OnExceptionCatch(ex2);
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_TransientADError, new object[]
				{
					ex2
				});
				errorCode = ErrorCode.CreateAdUnavailable((LID)51440U);
			}
			catch (DirectoryPermanentErrorException ex3)
			{
				NullExecutionContext.Instance.Diagnostics.OnExceptionCatch(ex3);
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_PermanentADError, new object[]
				{
					ex3
				});
				errorCode = ErrorCode.CreateADPropertyError((LID)45296U);
			}
			catch (StoreException ex4)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(ex4);
				throw new FailRpcException(ex4.Message, (int)ex4.Error);
			}
			finally
			{
				if (!flag && workerProcess != null)
				{
					this.workerManager.StopWorker(workerProcess.InstanceId, cancellationToken.IsCancellationRequested);
				}
			}
			isNewInstanceStarted = flag;
			return errorCode;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000AC04 File Offset: 0x00008E04
		public virtual void StopInstance(Guid instanceId, bool terminate)
		{
			RpcInstanceManager.RpcClient<AdminRpcClient> rpcClient = null;
			RpcInstanceManager.RpcClient<RpcInstancePool> rpcClient2 = null;
			try
			{
				using (LockManager.Lock(this.syncRoot))
				{
					PersistentAvlTree<Guid, RpcInstanceManager.RpcInstance> persistentAvlTree = this.GetInstanceMap();
					RpcInstanceManager.RpcInstance rpcInstance;
					if (persistentAvlTree != null && persistentAvlTree.TryGetValue(instanceId, out rpcInstance))
					{
						persistentAvlTree = persistentAvlTree.Remove(instanceId);
						Interlocked.Exchange<PersistentAvlTree<Guid, RpcInstanceManager.RpcInstance>>(ref this.instances, persistentAvlTree);
						rpcClient = rpcInstance.AdminClient;
						rpcClient2 = rpcInstance.PoolClient;
						rpcInstance.InstanceNotificationLoop.Cancelled = true;
						this.notificationQueue.Enqueue(rpcInstance.InstanceNotificationLoop);
					}
				}
				DiagnosticContext.TraceDword((LID)47168U, (uint)Environment.TickCount);
				this.workerManager.StopWorker(instanceId, terminate);
				DiagnosticContext.TraceDword((LID)63552U, (uint)Environment.TickCount);
			}
			finally
			{
				if (rpcClient != null)
				{
					rpcClient.Close();
				}
				this.OnPoolNotificationReceived();
				if (rpcClient2 != null)
				{
					rpcClient2.Close();
				}
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000ACF8 File Offset: 0x00008EF8
		public virtual bool IsInstanceStarted(Guid instanceId)
		{
			IWorkerProcess workerProcess;
			ErrorCode worker = this.workerManager.GetWorker(instanceId, out workerProcess);
			if (ErrorCode.NoError == worker)
			{
				PersistentAvlTree<Guid, RpcInstanceManager.RpcInstance> instanceMap = this.GetInstanceMap();
				return instanceMap != null && instanceMap.Contains(instanceId);
			}
			return false;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000AD38 File Offset: 0x00008F38
		public virtual string GetInstanceDisplayName(Guid instanceId)
		{
			IWorkerProcess workerProcess;
			ErrorCode worker = this.workerManager.GetWorker(instanceId, out workerProcess);
			if (ErrorCode.NoError == worker)
			{
				return workerProcess.InstanceName;
			}
			return instanceId.ToString();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000AD78 File Offset: 0x00008F78
		public virtual RpcInstanceManager.AdminCallGuard GetAdminRpcClient(Guid instanceId, string functionName, out AdminRpcClient adminRpc)
		{
			adminRpc = null;
			RpcInstanceManager.RpcClient<AdminRpcClient> client = null;
			int workerPid = -1;
			IWorkerProcess workerProcess;
			ErrorCode worker = this.workerManager.GetWorker(instanceId, out workerProcess);
			if (ErrorCode.NoError == worker)
			{
				workerPid = workerProcess.ProcessId;
				PersistentAvlTree<Guid, RpcInstanceManager.RpcInstance> instanceMap = this.GetInstanceMap();
				RpcInstanceManager.RpcInstance rpcInstance;
				if (instanceMap != null && instanceMap.TryGetValue(instanceId, out rpcInstance))
				{
					client = rpcInstance.AdminClient.BeginCall(out adminRpc);
				}
			}
			return new RpcInstanceManager.AdminCallGuard(client, functionName, instanceId, workerPid);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000ADE0 File Offset: 0x00008FE0
		public virtual RpcInstanceManager.RpcClient<RpcInstancePool> GetPoolRpcClient(Guid instanceId, ref int generation, out RpcInstancePool rpcClient)
		{
			rpcClient = null;
			IWorkerProcess workerProcess;
			ErrorCode worker = this.workerManager.GetWorker(instanceId, out workerProcess);
			if (ErrorCode.NoError != worker)
			{
				return null;
			}
			if (generation != 0 && generation != workerProcess.Generation)
			{
				return null;
			}
			PersistentAvlTree<Guid, RpcInstanceManager.RpcInstance> instanceMap = this.GetInstanceMap();
			RpcInstanceManager.RpcInstance rpcInstance;
			if (instanceMap == null || !instanceMap.TryGetValue(instanceId, out rpcInstance))
			{
				return null;
			}
			if (generation != 0 && rpcInstance.Generation != workerProcess.Generation)
			{
				return null;
			}
			generation = workerProcess.Generation;
			return rpcInstance.PoolClient.BeginCall(out rpcClient);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000B008 File Offset: 0x00009208
		public virtual IEnumerable<Guid> GetActiveInstances()
		{
			PersistentAvlTree<Guid, RpcInstanceManager.RpcInstance> map = this.GetInstanceMap();
			if (map != null)
			{
				foreach (Guid id in map.GetKeysLmr())
				{
					yield return id;
				}
			}
			yield break;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000B028 File Offset: 0x00009228
		protected virtual RpcInstanceManager.NotificationLoop RegisterInstance(Guid instanceId, IWorkerProcess worker)
		{
			RpcInstanceManager.NotificationLoop notificationLoop = null;
			PersistentAvlTree<Guid, RpcInstanceManager.RpcInstance> persistentAvlTree = this.GetInstanceMap();
			if (persistentAvlTree != null && !persistentAvlTree.Contains(instanceId))
			{
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					AdminRpcClient adminRpcClient = new AdminRpcClient("localhost", instanceId);
					disposeGuard.Add<AdminRpcClient>(adminRpcClient);
					RpcInstancePool rpcInstancePool = new RpcInstancePool(instanceId, worker.Generation);
					disposeGuard.Add<RpcInstancePool>(rpcInstancePool);
					notificationLoop = new RpcInstanceManager.NotificationLoop();
					notificationLoop.StateMachine = this.InstanceNotificationLoop(instanceId, worker.Generation, notificationLoop);
					persistentAvlTree = persistentAvlTree.Add(instanceId, new RpcInstanceManager.RpcInstance(worker.Generation, adminRpcClient, rpcInstancePool, notificationLoop));
					Interlocked.Exchange<PersistentAvlTree<Guid, RpcInstanceManager.RpcInstance>>(ref this.instances, persistentAvlTree);
					disposeGuard.Success();
				}
			}
			return notificationLoop;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000B0F0 File Offset: 0x000092F0
		private static string GetWorkerProcessPathName(string imageFileName)
		{
			string fileName = Process.GetCurrentProcess().MainModule.FileName;
			string directoryName = Path.GetDirectoryName(fileName);
			return directoryName + "\\" + imageFileName;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000B120 File Offset: 0x00009320
		private ErrorCode EcCheckDatabasesLimits(Guid mdbGuid, string databaseName, DatabaseType databaseType, bool databaseStateTransition)
		{
			return this.instanceLimitChecker.EcCheckDatabasesLimits(mdbGuid, databaseName, databaseType, databaseStateTransition);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000B134 File Offset: 0x00009334
		private ErrorCode UpdateDatabaseTypeOnWorker(Guid instanceId, string workerName, DatabaseType databaseTypeFinal)
		{
			ErrorCode second = ErrorCode.NoError;
			IWorkerProcess workerProcess;
			second = this.workerManager.GetWorker(instanceId, out workerProcess);
			if (ErrorCode.NoError != second)
			{
				return second.Propagate((LID)51792U);
			}
			if (DatabaseType.Passive == workerProcess.InstanceDBType && DatabaseType.Active == databaseTypeFinal)
			{
				second = this.instanceLimitChecker.EcCheckDatabasesLimits(instanceId, workerName, databaseTypeFinal, true);
				if (ErrorCode.NoError != second)
				{
					return second.Propagate((LID)48432U);
				}
				workerProcess.InstanceDBType = databaseTypeFinal;
			}
			else if (ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				ExTraceGlobals.ProxyAdminTracer.TraceFunction(0L, string.Format("Database state transition: {0} -> {1}.", workerProcess.InstanceDBType, databaseTypeFinal));
			}
			return ErrorCode.NoError;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000B1F3 File Offset: 0x000093F3
		private PersistentAvlTree<Guid, RpcInstanceManager.RpcInstance> GetInstanceMap()
		{
			return Interlocked.CompareExchange<PersistentAvlTree<Guid, RpcInstanceManager.RpcInstance>>(ref this.instances, null, null);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000B204 File Offset: 0x00009404
		private void OnWorkerComplete(IWorkerProcess worker)
		{
			RpcInstanceManager.RpcClient<AdminRpcClient> rpcClient = null;
			RpcInstanceManager.RpcClient<RpcInstancePool> rpcClient2 = null;
			try
			{
				using (LockManager.Lock(this.syncRoot))
				{
					PersistentAvlTree<Guid, RpcInstanceManager.RpcInstance> persistentAvlTree = this.GetInstanceMap();
					RpcInstanceManager.RpcInstance rpcInstance;
					if (persistentAvlTree != null && persistentAvlTree.TryGetValue(worker.InstanceId, out rpcInstance) && worker.Generation == rpcInstance.Generation)
					{
						persistentAvlTree = persistentAvlTree.Remove(worker.InstanceId);
						Interlocked.Exchange<PersistentAvlTree<Guid, RpcInstanceManager.RpcInstance>>(ref this.instances, persistentAvlTree);
						rpcClient = rpcInstance.AdminClient;
						rpcClient2 = rpcInstance.PoolClient;
					}
				}
			}
			finally
			{
				if (rpcClient != null)
				{
					rpcClient.Close();
				}
				if (rpcClient2 != null)
				{
					rpcClient2.Close();
				}
				OnRpcInstanceClosedCallback rpcInstanceClosed = this.RpcInstanceClosed;
				if (rpcInstanceClosed != null)
				{
					rpcInstanceClosed(worker.InstanceId, worker.Generation);
				}
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000B608 File Offset: 0x00009808
		private IEnumerator<ErrorCode> InstanceNotificationLoop(Guid workerInstance, int generation, RpcInstanceManager.NotificationLoop loop)
		{
			RpcInstanceManager.<InstanceNotificationLoop>d__a <InstanceNotificationLoop>d__a = new RpcInstanceManager.<InstanceNotificationLoop>d__a(0);
			<InstanceNotificationLoop>d__a.<>4__this = this;
			<InstanceNotificationLoop>d__a.workerInstance = workerInstance;
			<InstanceNotificationLoop>d__a.generation = generation;
			<InstanceNotificationLoop>d__a.loop = loop;
			return <InstanceNotificationLoop>d__a;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000B63C File Offset: 0x0000983C
		private void OnPoolNotificationReceived()
		{
			RpcInstanceManager.NotificationLoop notificationLoop = null;
			using (LockManager.Lock(this.syncRoot))
			{
				if (this.eventsProcessing)
				{
					return;
				}
				if (this.notificationQueue.Count == 0)
				{
					return;
				}
				notificationLoop = this.notificationQueue.Dequeue();
				this.eventsProcessing = true;
			}
			bool flag = false;
			do
			{
				try
				{
					notificationLoop.StateMachine.MoveNext();
				}
				finally
				{
					using (LockManager.Lock(this.syncRoot))
					{
						if (this.notificationQueue.Count > 0)
						{
							notificationLoop = this.notificationQueue.Dequeue();
						}
						else
						{
							this.eventsProcessing = false;
							flag = true;
						}
					}
				}
			}
			while (!flag);
		}

		// Token: 0x04000066 RID: 102
		private static string workerProcessName = "Microsoft.Exchange.Store.Worker.exe";

		// Token: 0x04000067 RID: 103
		private PersistentAvlTree<Guid, RpcInstanceManager.RpcInstance> instances;

		// Token: 0x04000068 RID: 104
		private object syncRoot = new object();

		// Token: 0x04000069 RID: 105
		private Action<IWorkerProcess> workerComplete;

		// Token: 0x0400006A RID: 106
		private IWorkerManager workerManager;

		// Token: 0x0400006B RID: 107
		private Queue<RpcInstanceManager.NotificationLoop> notificationQueue;

		// Token: 0x0400006C RID: 108
		private bool eventsProcessing;

		// Token: 0x0400006D RID: 109
		private RpcInstanceManager.InstancesLimitsChecker instanceLimitChecker;

		// Token: 0x0200001A RID: 26
		internal struct AdminCallGuard : IDisposable
		{
			// Token: 0x060000D6 RID: 214 RVA: 0x0000B720 File Offset: 0x00009920
			internal AdminCallGuard(RpcInstanceManager.RpcClient<AdminRpcClient> client, string rpcName, Guid workerInstance, int workerPid)
			{
				this.client = client;
				this.rpcName = rpcName;
				this.workerInstance = workerInstance;
				this.workerPid = workerPid;
				if (client != null && ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.FunctionTrace))
				{
					StringBuilder stringBuilder = new StringBuilder(100);
					stringBuilder.Append("ENTER CALL PROXY [ADMIN][");
					stringBuilder.Append(rpcName);
					stringBuilder.Append("] pid:[");
					stringBuilder.Append(workerPid);
					stringBuilder.Append("] database:[");
					stringBuilder.Append(workerInstance.ToString());
					stringBuilder.Append("]");
					ExTraceGlobals.ProxyAdminTracer.TraceFunction(0L, stringBuilder.ToString());
				}
			}

			// Token: 0x060000D7 RID: 215 RVA: 0x0000B7CC File Offset: 0x000099CC
			public void Dispose()
			{
				if (this.client != null)
				{
					this.client.EndCall();
					if (ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.FunctionTrace))
					{
						StringBuilder stringBuilder = new StringBuilder(100);
						stringBuilder.Append("EXIT CALL PROXY [ADMIN][");
						stringBuilder.Append(this.rpcName);
						stringBuilder.Append(this.rpcName);
						stringBuilder.Append("] pid:[");
						stringBuilder.Append(this.workerPid);
						stringBuilder.Append("] database:[");
						stringBuilder.Append(this.workerInstance.ToString());
						stringBuilder.Append("]");
						ExTraceGlobals.ProxyAdminTracer.TraceFunction(0L, stringBuilder.ToString());
					}
				}
			}

			// Token: 0x04000070 RID: 112
			private RpcInstanceManager.RpcClient<AdminRpcClient> client;

			// Token: 0x04000071 RID: 113
			private string rpcName;

			// Token: 0x04000072 RID: 114
			private Guid workerInstance;

			// Token: 0x04000073 RID: 115
			private int workerPid;
		}

		// Token: 0x0200001B RID: 27
		private struct RpcInstance
		{
			// Token: 0x060000D8 RID: 216 RVA: 0x0000B888 File Offset: 0x00009A88
			public RpcInstance(int generation, AdminRpcClient adminClient, RpcInstancePool poolClient, RpcInstanceManager.NotificationLoop notificationLoop)
			{
				this.Generation = generation;
				this.AdminClient = new RpcInstanceManager.RpcClient<AdminRpcClient>(adminClient);
				this.PoolClient = new RpcInstanceManager.RpcClient<RpcInstancePool>(poolClient);
				this.InstanceNotificationLoop = notificationLoop;
			}

			// Token: 0x04000074 RID: 116
			public RpcInstanceManager.RpcClient<AdminRpcClient> AdminClient;

			// Token: 0x04000075 RID: 117
			public RpcInstanceManager.RpcClient<RpcInstancePool> PoolClient;

			// Token: 0x04000076 RID: 118
			public int Generation;

			// Token: 0x04000077 RID: 119
			public RpcInstanceManager.NotificationLoop InstanceNotificationLoop;
		}

		// Token: 0x0200001C RID: 28
		internal class RpcClient<TClient> where TClient : class, IDisposable
		{
			// Token: 0x060000D9 RID: 217 RVA: 0x0000B8B1 File Offset: 0x00009AB1
			public RpcClient(TClient client)
			{
				this.activeCalls = 0L;
				this.client = client;
				this.closed = false;
			}

			// Token: 0x060000DA RID: 218 RVA: 0x0000B8D0 File Offset: 0x00009AD0
			public RpcInstanceManager.RpcClient<TClient> BeginCall(out TClient client)
			{
				client = default(TClient);
				using (LockManager.Lock(this))
				{
					if (!this.closed)
					{
						client = this.client;
						this.activeCalls += 1L;
						return this;
					}
				}
				return null;
			}

			// Token: 0x060000DB RID: 219 RVA: 0x0000B938 File Offset: 0x00009B38
			public void EndCall()
			{
				using (LockManager.Lock(this))
				{
					this.activeCalls -= 1L;
					if (this.closed && this.client != null && this.activeCalls == 0L)
					{
						this.client.Dispose();
						this.client = default(TClient);
					}
				}
			}

			// Token: 0x060000DC RID: 220 RVA: 0x0000B9B8 File Offset: 0x00009BB8
			public void Close()
			{
				using (LockManager.Lock(this))
				{
					if (!this.closed)
					{
						this.closed = true;
						if (this.activeCalls == 0L)
						{
							this.client.Dispose();
							this.client = default(TClient);
						}
					}
				}
			}

			// Token: 0x04000078 RID: 120
			private long activeCalls;

			// Token: 0x04000079 RID: 121
			private TClient client;

			// Token: 0x0400007A RID: 122
			private bool closed;
		}

		// Token: 0x0200001D RID: 29
		internal class NotificationLoop
		{
			// Token: 0x17000008 RID: 8
			// (get) Token: 0x060000DD RID: 221 RVA: 0x0000BA24 File Offset: 0x00009C24
			// (set) Token: 0x060000DE RID: 222 RVA: 0x0000BA2C File Offset: 0x00009C2C
			public IEnumerator<ErrorCode> StateMachine { get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x060000DF RID: 223 RVA: 0x0000BA35 File Offset: 0x00009C35
			// (set) Token: 0x060000E0 RID: 224 RVA: 0x0000BA3D File Offset: 0x00009C3D
			public RpcException LastRpcException { get; set; }

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000BA46 File Offset: 0x00009C46
			// (set) Token: 0x060000E2 RID: 226 RVA: 0x0000BA53 File Offset: 0x00009C53
			public bool Cancelled
			{
				get
				{
					return this.cancellation.IsCancellationRequested;
				}
				set
				{
					this.cancellation.Cancel();
				}
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x060000E3 RID: 227 RVA: 0x0000BA60 File Offset: 0x00009C60
			public CancellationToken Token
			{
				get
				{
					return this.cancellation.Token;
				}
			}

			// Token: 0x0400007B RID: 123
			private CancellationTokenSource cancellation = new CancellationTokenSource();
		}

		// Token: 0x0200001E RID: 30
		private class InstancesLimitsChecker
		{
			// Token: 0x060000E5 RID: 229 RVA: 0x0000BA80 File Offset: 0x00009C80
			public InstancesLimitsChecker(int nonRecoveryDatabasesMax, int recoveryDatabasesMax, int activeDatabasesMax, IWorkerManager workermanager)
			{
				this.nonRecoveryDatabasesMax = nonRecoveryDatabasesMax;
				this.recoveryDatabasesMax = recoveryDatabasesMax;
				this.activeDatabasesMax = activeDatabasesMax;
				this.workerManager = workermanager;
			}

			// Token: 0x060000E6 RID: 230 RVA: 0x0000BAA8 File Offset: 0x00009CA8
			public ErrorCode EcCheckDatabasesLimits(Guid instanceId, string databaseName, DatabaseType databaseType, bool databaseStateTransition)
			{
				ErrorCode errorCode = ErrorCode.NoError;
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				foreach (IWorkerProcess workerProcess in this.workerManager.GetActiveWorkers())
				{
					if (!databaseStateTransition || !(workerProcess.InstanceId == instanceId))
					{
						switch (workerProcess.InstanceDBType)
						{
						case DatabaseType.Active:
							num2++;
							break;
						case DatabaseType.Passive:
							num3++;
							break;
						case DatabaseType.Recovery:
							num++;
							break;
						}
					}
				}
				int num4 = num2 + num3;
				ErrorHelper.AssertRetail(num >= 0 && num <= this.recoveryDatabasesMax, "Invalid number of recovey databases.");
				ErrorHelper.AssertRetail(num2 >= 0 && num2 <= this.activeDatabasesMax, "Invalid number of active databases.");
				ErrorHelper.AssertRetail(num4 >= 0 && num4 <= this.nonRecoveryDatabasesMax, "Invalid number of non-recovery databases.");
				if (databaseType == DatabaseType.Recovery)
				{
					errorCode = this.EcCheckRecoveryDatabasesCount(instanceId, databaseName, num);
					if (ErrorCode.NoError != errorCode)
					{
						return errorCode.Propagate((LID)35056U);
					}
				}
				else
				{
					if (DatabaseType.Active == databaseType)
					{
						errorCode = this.EcCheckActiveDatabasesCount(instanceId, databaseName, num2);
						if (ErrorCode.NoError != errorCode)
						{
							return errorCode.Propagate((LID)59632U);
						}
					}
					errorCode = this.EcCheckNonRecoveryDatabasesCount(instanceId, databaseName, num4);
					if (ErrorCode.NoError != errorCode)
					{
						return errorCode.Propagate((LID)43248U);
					}
				}
				return errorCode;
			}

			// Token: 0x060000E7 RID: 231 RVA: 0x0000BC34 File Offset: 0x00009E34
			private ErrorCode EcCheckActiveDatabasesCount(Guid mdbGuid, string databaseName, int activeDatabasesCount)
			{
				ErrorCode result = ErrorCode.NoError;
				if (activeDatabasesCount >= this.activeDatabasesMax)
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_TooManyActiveMDBsMounted, new object[]
					{
						this.activeDatabasesMax
					});
					this.PublishFailureItem(mdbGuid, databaseName, FailureTag.ExceededMaxActiveDatabases);
					result = ErrorCode.CreateWithLid((LID)42480U, ErrorCodeValue.TooManyMountedDatabases);
				}
				return result;
			}

			// Token: 0x060000E8 RID: 232 RVA: 0x0000BC90 File Offset: 0x00009E90
			private ErrorCode EcCheckNonRecoveryDatabasesCount(Guid mdbGuid, string databaseName, int nonRecoveryDatabasesCount)
			{
				ErrorCode result = ErrorCode.NoError;
				if (nonRecoveryDatabasesCount >= this.nonRecoveryDatabasesMax)
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_TooManyMDBsMounted, new object[]
					{
						this.nonRecoveryDatabasesMax
					});
					this.PublishFailureItem(mdbGuid, databaseName, FailureTag.ExceededMaxDatabases);
					result = ErrorCode.CreateWithLid((LID)58864U, ErrorCodeValue.TooManyMountedDatabases);
				}
				return result;
			}

			// Token: 0x060000E9 RID: 233 RVA: 0x0000BCEC File Offset: 0x00009EEC
			private ErrorCode EcCheckRecoveryDatabasesCount(Guid mdbGuid, string databaseName, int recoveryDatabasesCount)
			{
				ErrorCode result = ErrorCode.NoError;
				if (recoveryDatabasesCount >= this.recoveryDatabasesMax)
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_TooManyRecoveryMDBsMounted, new object[]
					{
						this.recoveryDatabasesMax
					});
					this.PublishFailureItem(mdbGuid, databaseName, FailureTag.ExceededMaxDatabases);
					result = ErrorCode.CreateWithLid((LID)54768U, ErrorCodeValue.TooManyMountedDatabases);
				}
				return result;
			}

			// Token: 0x060000EA RID: 234 RVA: 0x0000BD48 File Offset: 0x00009F48
			private void PublishFailureItem(Guid databaseGuid, string databaseName, FailureTag failureTag)
			{
				DatabaseFailureItem databaseFailureItem = new DatabaseFailureItem(FailureNameSpace.Store, failureTag, databaseGuid)
				{
					ComponentName = "RpcProxy",
					InstanceName = databaseName
				};
				databaseFailureItem.Publish();
			}

			// Token: 0x0400007E RID: 126
			private readonly int nonRecoveryDatabasesMax;

			// Token: 0x0400007F RID: 127
			private readonly int recoveryDatabasesMax;

			// Token: 0x04000080 RID: 128
			private readonly int activeDatabasesMax;

			// Token: 0x04000081 RID: 129
			private IWorkerManager workerManager;
		}
	}
}
