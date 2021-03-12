using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000005 RID: 5
	internal sealed class ConnectionList : BaseObject
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002D38 File Offset: 0x00000F38
		public ConnectionList()
		{
			this.idleConnectionsScavengerTimer = new MaintenanceJobTimer(new Action(this.ScavengeIdleConnections), () => Configuration.ServiceConfiguration.IdleConnectionCheckPeriod != TimeSpan.Zero, Configuration.ServiceConfiguration.MaintenanceJobTimerCheckPeriod, TimeSpan.Zero);
			this.logConnectionLatencyTimer = new MaintenanceJobTimer(new Action(this.LogConnectionLatency), () => Configuration.ServiceConfiguration.LogConnectionLatencyCheckPeriod != TimeSpan.Zero, Configuration.ServiceConfiguration.MaintenanceJobTimerCheckPeriod, TimeSpan.FromMilliseconds(Configuration.ServiceConfiguration.MaintenanceJobTimerCheckPeriod.TotalMilliseconds / 2.0));
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002E03 File Offset: 0x00001003
		public int MaximumConnections
		{
			get
			{
				return Configuration.ServiceConfiguration.MaximumConnections;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002E0F File Offset: 0x0000100F
		public static int GetAsyncConnectionHandle(int connectionId)
		{
			return connectionId | 1073741824;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002E18 File Offset: 0x00001018
		public static int GetSyncConnectionHandle(int asyncConnectionId)
		{
			return asyncConnectionId & 1073741823;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E24 File Offset: 0x00001024
		public void AddConnection(Connection connection)
		{
			if (this.connectionMap.Count >= this.MaximumConnections)
			{
				throw new OutOfMemoryException();
			}
			try
			{
				this.connectionListLock.EnterWriteLock();
				this.connectionMap.Add(Connection.GetConnectionId(connection), connection);
			}
			finally
			{
				try
				{
					this.connectionListLock.ExitWriteLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.ConnectionCount.Increment();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002EA8 File Offset: 0x000010A8
		public void RemoveAndDisposeConnection(int connectionId, DisconnectReason disconnectReason)
		{
			string reason;
			switch (disconnectReason)
			{
			case DisconnectReason.ClientDisconnect:
				reason = "client disconnected";
				break;
			case DisconnectReason.ServerDropped:
				reason = "server dropped connection";
				break;
			case DisconnectReason.NetworkRundown:
				reason = "network rundown";
				break;
			default:
				throw new InvalidOperationException(string.Format("Invalid DisconnectReason; disconnectReason={0}", disconnectReason));
			}
			Connection connection;
			try
			{
				this.connectionListLock.EnterWriteLock();
				if (this.connectionMap.TryGetValue(connectionId, out connection))
				{
					this.connectionMap.Remove(connectionId);
				}
			}
			finally
			{
				try
				{
					this.connectionListLock.ExitWriteLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			if (connection != null)
			{
				using (connection)
				{
					ExTraceGlobals.ConnectRpcTracer.TraceDebug<Connection>(Activity.TraceId, "RemoveAndDisposeConnection. Connection = {0}.", connection);
					connection.CompleteAction(reason, false, RpcErrorCode.None);
					RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.ConnectionCount.Decrement();
				}
			}
			ProtocolLog.LogDisconnect(disconnectReason);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002FA4 File Offset: 0x000011A4
		public bool TryGetConnection(int connectionId, out Connection connection)
		{
			connection = null;
			bool result;
			try
			{
				this.connectionListLock.EnterReadLock();
				result = this.connectionMap.TryGetValue(connectionId, out connection);
			}
			finally
			{
				try
				{
					this.connectionListLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return result;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003000 File Offset: 0x00001200
		public Connection GetConnection(int connectionId)
		{
			Connection result;
			if (!this.TryGetConnection(connectionId, out result))
			{
				throw new ServerUnavailableException("contextHandle no longer valid - the connection must have been dropped during the previous RPC. Simulate a server failure.", null);
			}
			return result;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003028 File Offset: 0x00001228
		public int NextConnectionId()
		{
			int result;
			for (;;)
			{
				uint num = (uint)Interlocked.Increment(ref this.lastConnectionId);
				int num2 = (int)(1U + num % 268435456U);
				try
				{
					this.connectionListLock.EnterReadLock();
					if (this.connectionMap.ContainsKey(num2))
					{
						continue;
					}
					result = num2;
				}
				finally
				{
					try
					{
						this.connectionListLock.ExitReadLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
				break;
			}
			return result;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003098 File Offset: 0x00001298
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ConnectionList>(this);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000030C8 File Offset: 0x000012C8
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.idleConnectionsScavengerTimer);
			Util.DisposeIfPresent(this.logConnectionLatencyTimer);
			try
			{
				this.connectionListLock.EnterWriteLock();
				using (Dictionary<int, Connection>.ValueCollection.Enumerator enumerator = this.connectionMap.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Connection connection = enumerator.Current;
						if (connection.TryIncrementUsageCount())
						{
							connection.MarkForRemoval(delegate
							{
								connection.CompleteAction("shutdown", false, RpcErrorCode.None);
								connection.Dispose();
							});
							connection.DecrementUsageCount();
						}
					}
				}
				this.connectionMap.Clear();
				RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.ConnectionCount.RawValue = 0L;
			}
			finally
			{
				try
				{
					this.connectionListLock.ExitWriteLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			base.InternalDispose();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000031C8 File Offset: 0x000013C8
		private void ForEachValidConnection(Predicate<Connection> shouldInclude, Action<Connection> executeDelegate)
		{
			List<int> list = new List<int>();
			try
			{
				this.connectionListLock.EnterReadLock();
				foreach (KeyValuePair<int, Connection> keyValuePair in this.connectionMap)
				{
					if (shouldInclude(keyValuePair.Value))
					{
						list.Add(keyValuePair.Key);
					}
				}
			}
			finally
			{
				try
				{
					this.connectionListLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			foreach (int connectionId in list)
			{
				Connection connection;
				if (this.TryGetConnection(connectionId, out connection) && connection.TryIncrementUsageCount())
				{
					try
					{
						executeDelegate(connection);
					}
					finally
					{
						connection.DecrementUsageCount();
					}
				}
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003348 File Offset: 0x00001548
		private void ScavengeIdleConnections()
		{
			TimeSpan timePeriod = Configuration.ServiceConfiguration.IdleConnectionCheckPeriod;
			ExDateTime utcNow = ExDateTime.UtcNow;
			this.ForEachValidConnection((Connection x) => x.LastAccessTime + timePeriod < utcNow, delegate(Connection connection)
			{
				using (Activity.Guard guard = new Activity.Guard())
				{
					guard.AssociateWithCurrentThread(connection.Activity, false);
					connection.CompleteAction("idle", false, RpcErrorCode.None);
				}
			});
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003460 File Offset: 0x00001660
		private void LogConnectionLatency()
		{
			TimeSpan timePeriod = Configuration.ServiceConfiguration.LogConnectionLatencyCheckPeriod;
			ExDateTime utcNow = ExDateTime.UtcNow;
			this.ForEachValidConnection((Connection x) => x.LastLogTime + timePeriod < utcNow, delegate(Connection connection)
			{
				IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
				ReferencedActivityScope referencedActivityScope = null;
				try
				{
					referencedActivityScope = connection.GetReferencedActivityScope();
					ActivityContext.SetThreadScope(referencedActivityScope.ActivityScope);
					using (Activity.Guard guard = new Activity.Guard())
					{
						guard.AssociateWithCurrentThread(connection.Activity, false);
						connection.UpdateBudgetBalance();
						ProtocolLog.LogConnectionRpcProcessingTime();
						connection.LastLogTime = utcNow;
						connection.StartNewActivityScope();
					}
				}
				finally
				{
					ActivityContext.SetThreadScope(currentActivityScope);
					if (referencedActivityScope != null)
					{
						referencedActivityScope.Release();
					}
				}
			});
		}

		// Token: 0x0400001C RID: 28
		private const int MaxConnectionId = 268435456;

		// Token: 0x0400001D RID: 29
		private const int ConnectionIdKeyMask = 1073741823;

		// Token: 0x0400001E RID: 30
		private const uint AsynchronousConnectionIdFlag = 1073741824U;

		// Token: 0x0400001F RID: 31
		private readonly ReaderWriterLockSlim connectionListLock = new ReaderWriterLockSlim();

		// Token: 0x04000020 RID: 32
		private readonly Dictionary<int, Connection> connectionMap = new Dictionary<int, Connection>();

		// Token: 0x04000021 RID: 33
		private readonly MaintenanceJobTimer idleConnectionsScavengerTimer;

		// Token: 0x04000022 RID: 34
		private readonly MaintenanceJobTimer logConnectionLatencyTimer;

		// Token: 0x04000023 RID: 35
		private int lastConnectionId;
	}
}
