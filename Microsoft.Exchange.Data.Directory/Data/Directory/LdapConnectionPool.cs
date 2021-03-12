using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000145 RID: 325
	internal class LdapConnectionPool
	{
		// Token: 0x06000DB6 RID: 3510 RVA: 0x0003E7F8 File Offset: 0x0003C9F8
		internal static ADServerRole RoleFromConnectionPoolType(ConnectionPoolType type)
		{
			switch (type)
			{
			case ConnectionPoolType.DCPool:
			case ConnectionPoolType.UserDCPool:
				return ADServerRole.DomainController;
			case ConnectionPoolType.GCPool:
			case ConnectionPoolType.UserGCPool:
				return ADServerRole.GlobalCatalog;
			case ConnectionPoolType.ConfigDCPool:
			case ConnectionPoolType.ConfigDCNotifyPool:
				return ADServerRole.ConfigurationDomainController;
			default:
				throw new ArgumentException(DirectoryStrings.ExArgumentException("type", type.ToString()), "type");
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0003E851 File Offset: 0x0003CA51
		internal ADServerInfo[] ADServerInfos
		{
			get
			{
				return this.serverInfos;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x0003E859 File Offset: 0x0003CA59
		internal int ConnectedCount
		{
			get
			{
				return this.connectedCount;
			}
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0003E864 File Offset: 0x0003CA64
		internal LdapConnectionPool(ConnectionPoolType type, ADServerInfo[] servers, NetworkCredential credential)
		{
			this.type = type;
			this.poolLock = new ReaderWriterLock();
			this.isActive = true;
			this.serverInfos = servers;
			this.credential = credential;
			this.connectionInfos = new ConnectionInfo[servers.Length];
			for (int i = 0; i < servers.Length; i++)
			{
				this.connectionInfos[i] = new ConnectionInfo(servers[i]);
			}
			ExTraceGlobals.GetConnectionTracer.TraceDebug<int, int>((long)this.GetHashCode(), "Creating LdapConnectionPool {0} with {1} servers", (int)type, this.connectionInfos.Length);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0003E8EC File Offset: 0x0003CAEC
		private void CheckBadConnections()
		{
			bool flag = false;
			try
			{
				this.poolLock.AcquireReaderLock(-1);
				if (!this.isActive)
				{
					ExTraceGlobals.GetConnectionTracer.TraceWarning((long)this.GetHashCode(), "Pool is not active, skipping CheckBadConnections (R)");
					return;
				}
				foreach (ConnectionInfo connectionInfo in this.connectionInfos)
				{
					if (connectionInfo.ConnectionState == ConnectionState.Connected)
					{
						PooledLdapConnection pooledLdapConnection = connectionInfo.PooledLdapConnection;
						if (!pooledLdapConnection.IsUp && !pooledLdapConnection.IsFatalError)
						{
							pooledLdapConnection.RetryDownConnection();
						}
						if (pooledLdapConnection.IsFatalError)
						{
							flag = true;
						}
					}
				}
			}
			finally
			{
				try
				{
					this.poolLock.ReleaseReaderLock();
				}
				catch (ApplicationException)
				{
				}
			}
			if (flag)
			{
				try
				{
					this.poolLock.AcquireWriterLock(-1);
					if (!this.isActive)
					{
						ExTraceGlobals.GetConnectionTracer.TraceWarning((long)this.GetHashCode(), "Pool is not active, skipping CheckBadConnections (W)");
						return;
					}
					foreach (ConnectionInfo connectionInfo2 in this.connectionInfos)
					{
						if (connectionInfo2.ConnectionState == ConnectionState.Connected)
						{
							PooledLdapConnection pooledLdapConnection2 = connectionInfo2.PooledLdapConnection;
							if (pooledLdapConnection2.IsFatalError)
							{
								connectionInfo2.MakeEmpty();
								Interlocked.Decrement(ref this.connectedCount);
							}
						}
					}
				}
				finally
				{
					try
					{
						this.poolLock.ReleaseWriterLock();
					}
					catch (ApplicationException)
					{
					}
				}
				return;
			}
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0003EA50 File Offset: 0x0003CC50
		private bool OpenNewConnection(ConnectionInfo info)
		{
			ArgumentValidator.ThrowIfNull("info", info);
			if (info.ConnectionState != ConnectionState.Empty)
			{
				return false;
			}
			if (!info.TryMakeConnecting())
			{
				return false;
			}
			bool result = false;
			try
			{
				ExTraceGlobals.ConnectionTracer.TraceDebug<string>((long)this.GetHashCode(), "LdapConnectionPool::OpenNewConnection - opening new PooledLdapConnection to {0}", info.ADServerInfo.FqdnPlusPort);
				if (info.TryCreatePooledLdapConnection(LdapConnectionPool.RoleFromConnectionPoolType(this.type), this.type == ConnectionPoolType.ConfigDCNotifyPool, this.credential))
				{
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_NEW_CONNECTION, null, new object[]
					{
						info.ADServerInfo.Fqdn,
						info.ADServerInfo.Port,
						this.type
					});
					if (info.TryBindWithRetry(3) && info.TrySetNamingContexts())
					{
						info.MakeConnected();
						result = true;
						Interlocked.Increment(ref this.connectedCount);
					}
				}
			}
			finally
			{
				if (info.ConnectionState == ConnectionState.Connecting)
				{
					info.MakeDisconnected();
				}
			}
			return result;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0003EB4C File Offset: 0x0003CD4C
		private int CalculateLoadBalancingQuality(int connectionIndex)
		{
			int num = 0;
			ConnectionInfo connectionInfo = this.connectionInfos[connectionIndex];
			int num2 = (connectionInfo.ADServerInfo.DnsWeight > 0) ? connectionInfo.ADServerInfo.DnsWeight : 1;
			int num3 = connectionInfo.ADServerInfo.TotalRequestCount;
			int num4 = this.totalRequestCount;
			num4 = ((num4 > 0) ? num4 : 1);
			int num5 = 100 * num3 / num4;
			num5 = Math.Min(num5, 100);
			num -= 100 * num5 / num2;
			num += ((connectionIndex >= (this.lastUsedConnectionIndex + 1) % this.connectionInfos.Length) ? 10 : 0);
			return num - connectionInfo.PooledLdapConnection.OutstandingRequestCount * 1000;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0003EBF0 File Offset: 0x0003CDF0
		private int CalculateQualityByServerMatch(int connectionIndex, string serverName, int port)
		{
			if (!this.IsServerMatch(connectionIndex, serverName, port))
			{
				return int.MinValue;
			}
			return 1000;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0003EC08 File Offset: 0x0003CE08
		private bool IsDomainMatch(int connectionIndex, ADObjectId domain)
		{
			bool flag = this.connectionInfos[connectionIndex].ADServerInfo.WritableNC.Equals(domain.DistinguishedName, StringComparison.OrdinalIgnoreCase);
			ExTraceGlobals.GetConnectionTracer.TraceDebug((long)this.GetHashCode(), "IsDomainMatch: {0} from {1} is {2} match for domain {3}", new object[]
			{
				this.connectionInfos[connectionIndex].ADServerInfo.FqdnPlusPort,
				this.connectionInfos[connectionIndex].ADServerInfo.WritableNC,
				flag ? "a" : "NO",
				domain.ToDNString()
			});
			return flag;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0003EC98 File Offset: 0x0003CE98
		private bool IsServerMatch(int connectionIndex, string serverName, int port)
		{
			string fqdn = this.connectionInfos[connectionIndex].ADServerInfo.Fqdn;
			int port2 = this.connectionInfos[connectionIndex].ADServerInfo.Port;
			bool flag = false;
			if (fqdn.Equals(serverName, StringComparison.OrdinalIgnoreCase))
			{
				flag = true;
			}
			else if (serverName.IndexOf('.') == -1 && fqdn.IndexOf('.') == serverName.Length && fqdn.Substring(0, serverName.Length).Equals(serverName, StringComparison.OrdinalIgnoreCase))
			{
				flag = true;
			}
			bool flag2 = flag && port == port2;
			ExTraceGlobals.GetConnectionTracer.TraceDebug((long)this.GetHashCode(), "IsServerMatch: {0}:{1} is {2} match for {3}:{4}", new object[]
			{
				fqdn,
				port2,
				flag2 ? "a" : "NO",
				serverName,
				port
			});
			return flag2;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0003ED6B File Offset: 0x0003CF6B
		internal void MoveConnectionsAndDisconnect(int[] serverMapping, LdapConnectionPool sourcePool, List<PooledLdapConnection> connectionsToRelease)
		{
			this.MoveOrDisconnect(serverMapping, sourcePool, connectionsToRelease);
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0003ED7C File Offset: 0x0003CF7C
		internal void DisconnectPool()
		{
			List<PooledLdapConnection> list = new List<PooledLdapConnection>();
			try
			{
				this.MoveOrDisconnect(null, this, list);
			}
			finally
			{
				if (list.Count > 0)
				{
					ExTraceGlobals.ADTopologyTracer.TraceDebug<int>((long)this.GetHashCode(), "Returning {0} connections to a pool. This may result in disposing those connections if they were not moved to a new pool.", list.Count);
					foreach (PooledLdapConnection pooledLdapConnection in list)
					{
						pooledLdapConnection.ReturnToPool();
					}
				}
			}
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0003EE0C File Offset: 0x0003D00C
		private void MoveOrDisconnect(int[] serverMapping, LdapConnectionPool sourcePool, List<PooledLdapConnection> connectionsToRelease)
		{
			bool flag = sourcePool == this;
			try
			{
				sourcePool.poolLock.AcquireWriterLock(-1);
				this.poolLock.AcquireReaderLock(-1);
				if (!sourcePool.isActive)
				{
					ExTraceGlobals.GetConnectionTracer.TraceWarning((long)sourcePool.GetHashCode(), "Source Pool is not active, skipping MoveConnectionsAndDisconnect");
				}
				else
				{
					for (int i = 0; i < sourcePool.connectionInfos.Length; i++)
					{
						ConnectionInfo connectionInfo = sourcePool.connectionInfos[i];
						if (!flag)
						{
							int num = -1;
							for (int j = 0; j < serverMapping.Length; j++)
							{
								if (serverMapping[j] == i)
								{
									num = j;
									break;
								}
							}
							if (num != -1 && this.connectionInfos != null && this.connectionInfos.Length > 0)
							{
								ConnectionInfo connectionInfo2 = this.connectionInfos[num];
								if (connectionInfo.ConnectionState == ConnectionState.Connected && connectionInfo.PooledLdapConnection.IsUp && connectionInfo2.ConnectionState == ConnectionState.Empty)
								{
									this.ExecuteConnectionMove(connectionInfo2, connectionInfo, sourcePool);
								}
								else
								{
									ExTraceGlobals.GetConnectionTracer.TraceWarning<string, int, string>((long)this.GetHashCode(), "Not moving connection to {0} because its state is {1} and IsUp={2}", connectionInfo.ADServerInfo.FqdnPlusPort, (int)connectionInfo.ConnectionState, (connectionInfo.PooledLdapConnection == null) ? "<n/a>" : connectionInfo.PooledLdapConnection.IsUp.ToString());
								}
							}
						}
						if (connectionInfo.ConnectionState == ConnectionState.Connected)
						{
							this.DisconnectSourceAfterConnectionMove(connectionInfo, sourcePool, connectionsToRelease);
						}
						else
						{
							ExTraceGlobals.GetConnectionTracer.TraceWarning<string, int>((long)this.GetHashCode(), "Not Disconnecting connection to {0} because its state is {1}", connectionInfo.ADServerInfo.FqdnPlusPort, (int)connectionInfo.ConnectionState);
						}
					}
					ExTraceGlobals.GetConnectionTracer.TraceDebug((long)sourcePool.GetHashCode(), "Deactivating Source Pool");
					sourcePool.isActive = false;
				}
			}
			finally
			{
				try
				{
					this.poolLock.ReleaseReaderLock();
				}
				catch (ApplicationException)
				{
				}
				try
				{
					sourcePool.poolLock.ReleaseWriterLock();
				}
				catch (ApplicationException)
				{
				}
			}
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0003F004 File Offset: 0x0003D204
		internal void MoveConnectionAcrossPool(LdapConnectionPool sourcePool, List<PooledLdapConnection> connectionsToRelease)
		{
			try
			{
				sourcePool.poolLock.AcquireWriterLock(-1);
				this.poolLock.AcquireReaderLock(-1);
				if (!sourcePool.isActive)
				{
					ExTraceGlobals.GetConnectionTracer.TraceWarning((long)sourcePool.GetHashCode(), "Source Pool is not active, skipping MoveConnectionAcrossPool");
				}
				else if (this.connectionInfos == null || this.connectionInfos.Length <= 0)
				{
					ExTraceGlobals.GetConnectionTracer.TraceWarning((long)this.GetHashCode(), "Target Pool does not have active connections, skipping MoveConnectionAcrossPool");
				}
				else
				{
					for (int i = 0; i < sourcePool.connectionInfos.Length; i++)
					{
						ConnectionInfo connectionInfo = sourcePool.connectionInfos[i];
						if (connectionInfo.ConnectionState == ConnectionState.Connected && connectionInfo.PooledLdapConnection != null && connectionInfo.PooledLdapConnection.IsUp)
						{
							for (int j = 0; j < this.connectionInfos.Length; j++)
							{
								ConnectionInfo connectionInfo2 = this.connectionInfos[j];
								if (connectionInfo2.ConnectionState == ConnectionState.Empty && connectionInfo.ADServerInfo.Equals(connectionInfo2.ADServerInfo))
								{
									this.ExecuteConnectionMove(connectionInfo2, connectionInfo, sourcePool);
									this.DisconnectSourceAfterConnectionMove(connectionInfo, sourcePool, connectionsToRelease);
									break;
								}
							}
						}
					}
				}
			}
			finally
			{
				try
				{
					this.poolLock.ReleaseReaderLock();
				}
				catch (ApplicationException)
				{
				}
				try
				{
					sourcePool.poolLock.ReleaseWriterLock();
				}
				catch (ApplicationException)
				{
				}
			}
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0003F150 File Offset: 0x0003D350
		private void ExecuteConnectionMove(ConnectionInfo targetInfo, ConnectionInfo sourceInfo, LdapConnectionPool sourcePool)
		{
			ExTraceGlobals.GetConnectionTracer.TraceDebug<string, string, int>((long)this.GetHashCode(), "Moving connection to {0}={1} from pool {2}", sourceInfo.ADServerInfo.FqdnPlusPort, targetInfo.ADServerInfo.FqdnPlusPort, sourcePool.GetHashCode());
			targetInfo.TryMakeConnecting();
			targetInfo.PooledLdapConnection = sourceInfo.PooledLdapConnection;
			targetInfo.PooledLdapConnection.BorrowFromPool();
			targetInfo.MakeConnected();
			Interlocked.Increment(ref this.connectedCount);
			ExTraceGlobals.GetConnectionTracer.TraceDebug<string>((long)this.GetHashCode(), "Moved connection to {0} to the new pool", sourceInfo.ADServerInfo.FqdnPlusPort);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0003F1E4 File Offset: 0x0003D3E4
		private void DisconnectSourceAfterConnectionMove(ConnectionInfo sourceInfo, LdapConnectionPool sourcePool, List<PooledLdapConnection> connectionsToRelease)
		{
			ExTraceGlobals.GetConnectionTracer.TraceDebug<string>((long)this.GetHashCode(), "Added connection to {0} to a list of connection to release.", sourceInfo.ADServerInfo.FqdnPlusPort);
			connectionsToRelease.Add(sourceInfo.PooledLdapConnection.BorrowFromPool());
			sourceInfo.MakeDisconnected();
			Interlocked.Decrement(ref sourcePool.connectedCount);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0003F238 File Offset: 0x0003D438
		internal void RemoveEmptyConnectionFromPool()
		{
			if (this.connectedCount == this.connectionInfos.Length)
			{
				return;
			}
			try
			{
				this.poolLock.AcquireWriterLock(-1);
			}
			finally
			{
				try
				{
					this.poolLock.ReleaseWriterLock();
					ADServerInfo[] array = new ADServerInfo[this.connectedCount];
					ConnectionInfo[] array2 = new ConnectionInfo[this.connectedCount];
					int num = 0;
					for (int i = 0; i < this.connectionInfos.Length; i++)
					{
						ConnectionInfo connectionInfo = this.connectionInfos[i];
						if (connectionInfo.ConnectionState == ConnectionState.Connected)
						{
							array[num] = this.serverInfos[i];
							array2[num] = connectionInfo;
							num++;
						}
					}
					this.serverInfos = array;
					this.connectionInfos = array2;
				}
				catch (ApplicationException)
				{
				}
			}
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0003F2F4 File Offset: 0x0003D4F4
		internal void AppendCustomServer(ADServerInfo serverInfo, ref bool presentAndDownOrDisconnected)
		{
			presentAndDownOrDisconnected = false;
			try
			{
				this.poolLock.AcquireWriterLock(-1);
				if (!this.isActive)
				{
					ExTraceGlobals.GetConnectionTracer.TraceWarning((long)this.GetHashCode(), "Pool is not active, skipping AppendCustomServer");
				}
				else
				{
					for (int i = 0; i < this.connectionInfos.Length; i++)
					{
						ConnectionInfo connectionInfo = this.connectionInfos[i];
						if (connectionInfo.ADServerInfo.Equals(serverInfo))
						{
							ExTraceGlobals.GetConnectionTracer.TraceWarning<string>((long)this.GetHashCode(), "AppendCustomServer: {0} is already in the list", serverInfo.FqdnPlusPort);
							if (connectionInfo.ConnectionState == ConnectionState.Disconnected || (connectionInfo.ConnectionState == ConnectionState.Connected && !connectionInfo.PooledLdapConnection.IsUp))
							{
								ExTraceGlobals.GetConnectionTracer.TraceWarning<string, string>((long)this.GetHashCode(), "AppendCustomServer: {0} is {1}", serverInfo.FqdnPlusPort, (connectionInfo.ConnectionState == ConnectionState.Disconnected) ? "Disconnected" : "Down");
								presentAndDownOrDisconnected = true;
							}
							return;
						}
					}
					ExTraceGlobals.GetConnectionTracer.TraceDebug<string>((long)this.GetHashCode(), "Adding custom connection to {0}", serverInfo.Fqdn);
					ADProviderPerf.AddDCInstance(serverInfo.Fqdn);
					ConnectionInfo[] array = new ConnectionInfo[this.connectionInfos.Length + 1];
					this.connectionInfos.CopyTo(array, 0);
					array[array.Length - 1] = new ConnectionInfo(serverInfo);
					this.connectionInfos = array;
					ADServerInfo[] array2 = new ADServerInfo[this.serverInfos.Length + 1];
					this.serverInfos.CopyTo(array2, 0);
					array2[array2.Length - 1] = serverInfo;
					this.serverInfos = array2;
				}
			}
			finally
			{
				try
				{
					this.poolLock.ReleaseWriterLock();
				}
				catch (ApplicationException)
				{
				}
			}
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0003F4A4 File Offset: 0x0003D6A4
		internal void MergePool(LdapConnectionPool sourcePool, List<PooledLdapConnection> connectionsToRelease)
		{
			if (!this.isActive || !sourcePool.isActive)
			{
				ExTraceGlobals.GetConnectionTracer.TraceWarning<bool, bool>((long)this.GetHashCode(), "MergePool - Pool not active. sourcePool = {0}, targetPool = {1}. skipping", sourcePool.isActive, this.isActive);
				return;
			}
			foreach (ConnectionInfo connectionInfo in sourcePool.connectionInfos)
			{
				ADServerInfo serverInfo = new ADServerInfo(connectionInfo.ADServerInfo.Fqdn, connectionInfo.ADServerInfo.PartitionFqdn, connectionInfo.ADServerInfo.Port, connectionInfo.ADServerInfo.WritableNC, connectionInfo.ADServerInfo.DnsWeight, connectionInfo.ADServerInfo.AuthType, true);
				bool flag = false;
				this.AppendCustomServer(serverInfo, ref flag);
			}
			this.MoveConnectionAcrossPool(sourcePool, connectionsToRelease);
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0003F560 File Offset: 0x0003D760
		internal PooledLdapConnection GetConnection(ADObjectId domain, string serverName, int port, ref bool newPoolAvailable, ref bool pendingConnections, ref bool serverConnectionPresentButDownOrDisconnected, ref Exception extraDownOrDisconnectedException)
		{
			PooledLdapConnection result = null;
			newPoolAvailable = false;
			pendingConnections = false;
			serverConnectionPresentButDownOrDisconnected = false;
			int tickCount = Environment.TickCount;
			bool flag = serverName != null;
			bool flag2 = domain != null;
			bool flag3 = !flag2 && !flag;
			ExTraceGlobals.GetConnectionTracer.TraceDebug<ConnectionPoolType, string, string>((long)this.GetHashCode(), "LdapConnectionPool.GetConnection of type {0} by {1} ({2})", this.type, flag ? "Server" : (flag2 ? "Domain" : "Load balancing"), flag ? serverName : (flag2 ? domain.ToDNString() : "Least loaded"));
			this.CheckBadConnections();
			try
			{
				this.poolLock.AcquireReaderLock(-1);
				if (!this.isActive)
				{
					ExTraceGlobals.GetConnectionTracer.TraceWarning((long)this.GetHashCode(), "Pool is not active, skipping GetConnection");
					newPoolAvailable = true;
					return null;
				}
				bool flag4 = false;
				int num = int.MinValue;
				int num2 = -1;
				for (int i = 0; i < this.connectionInfos.Length; i++)
				{
					ConnectionInfo connectionInfo = this.connectionInfos[i];
					if (!flag4 && connectionInfo.ConnectionState == ConnectionState.Empty)
					{
						ExTraceGlobals.GetConnectionTracer.TraceDebug<string>((long)this.GetHashCode(), "GetConnection: Opening connection for {0}.", connectionInfo.ADServerInfo.FqdnPlusPort);
						this.OpenNewConnection(connectionInfo);
					}
					if (connectionInfo.ConnectionState == ConnectionState.Connecting)
					{
						ExTraceGlobals.GetConnectionTracer.TraceDebug<string>((long)this.GetHashCode(), "Connection to {0} is pending, skipping.", connectionInfo.ADServerInfo.FqdnPlusPort);
						pendingConnections = true;
					}
					else if (connectionInfo.ConnectionState != ConnectionState.Connected || !connectionInfo.PooledLdapConnection.IsUp)
					{
						ExTraceGlobals.GetConnectionTracer.TraceDebug<string, int, string>((long)this.GetHashCode(), "Connection to {0} is {1}{2}, skipping.", connectionInfo.ADServerInfo.FqdnPlusPort, (int)connectionInfo.ConnectionState, (connectionInfo.ConnectionState == ConnectionState.Connected) ? (connectionInfo.PooledLdapConnection.IsUp ? "and UP" : "and DOWN") : string.Empty);
						if (flag && this.IsServerMatch(i, serverName, port))
						{
							serverConnectionPresentButDownOrDisconnected = true;
							extraDownOrDisconnectedException = connectionInfo.LastLdapException;
							ExTraceGlobals.GetConnectionTracer.TraceError<string, int>((long)this.GetHashCode(), "LdapConnectionPool.GetConnection: {0}:{1} is present but is down or disconnected", serverName, port);
							break;
						}
					}
					else
					{
						int num3 = int.MinValue;
						if (flag3)
						{
							num3 = this.CalculateLoadBalancingQuality(i);
							flag4 = (connectionInfo.PooledLdapConnection.OutstandingRequestCount == 0);
						}
						else if (flag)
						{
							num3 = this.CalculateQualityByServerMatch(i, serverName, port);
							flag4 = (num3 > num);
						}
						else
						{
							if (this.IsDomainMatch(i, domain))
							{
								num3 = this.CalculateLoadBalancingQuality(i);
							}
							flag4 = (num3 > num);
						}
						ExTraceGlobals.GetConnectionTracer.TraceDebug((long)this.GetHashCode(), "Connection to {0}:{1} has quality {2}. suitableConnectionFound {3}", new object[]
						{
							connectionInfo.ADServerInfo.Fqdn,
							connectionInfo.ADServerInfo.Port,
							num3,
							flag4
						});
						if (num3 > num)
						{
							num = num3;
							num2 = i;
						}
					}
				}
				if (num2 > -1)
				{
					ConnectionInfo connectionInfo2 = this.connectionInfos[num2];
					connectionInfo2.ADServerInfo.IncrementRequestCount();
					Interlocked.Increment(ref this.totalRequestCount);
					Interlocked.Exchange(ref this.lastUsedConnectionIndex, num2);
					result = connectionInfo2.PooledLdapConnection.BorrowFromPool();
					ExTraceGlobals.GetConnectionTracer.TraceDebug<string>((long)this.GetHashCode(), "Returning connection to {0}.", connectionInfo2.ADServerInfo.FqdnPlusPort);
				}
			}
			finally
			{
				try
				{
					this.poolLock.ReleaseReaderLock();
					ExTraceGlobals.GetConnectionTracer.TracePerformance<ulong>((long)this.GetHashCode(), "GetConnection time spend {0} milliseconds.", Globals.GetTickDifference(tickCount, Environment.TickCount));
				}
				catch (ApplicationException)
				{
				}
			}
			return result;
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0003F910 File Offset: 0x0003DB10
		internal bool IsServerInAnyKnownSite(ADServer adServer)
		{
			if (adServer == null)
			{
				throw new ArgumentNullException("adServer");
			}
			for (int i = 0; i < this.ADServerInfos.Length; i++)
			{
				if (!string.IsNullOrEmpty(this.ADServerInfos[i].SiteName) && string.Equals(adServer.Site.Name, this.ADServerInfos[i].SiteName, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0003F978 File Offset: 0x0003DB78
		internal static PooledLdapConnection CreateOneTimeConnection(NetworkCredential networkCredential, ADServerInfo serverInfo, LocatorFlags connectionFlags = LocatorFlags.None)
		{
			string arg = "<null>\\<null>";
			if (networkCredential != null)
			{
				arg = networkCredential.Domain + "\\" + networkCredential.UserName;
			}
			else
			{
				using (WindowsIdentity current = WindowsIdentity.GetCurrent())
				{
					if (current.ImpersonationLevel == TokenImpersonationLevel.Delegation || current.ImpersonationLevel == TokenImpersonationLevel.Impersonation)
					{
						arg = current.Name;
					}
				}
			}
			ExTraceGlobals.ConnectionTracer.TraceDebug<string, string>(0L, "LdapConnectionPool::CreateOneTimeConnection - opening new ONE-TIME PooledLdapConnection to {0} as {1}", serverInfo.FqdnPlusPort, arg);
			ADProviderPerf.AddDCInstance(serverInfo.Fqdn);
			ADServerRole role = (serverInfo.Port == 389) ? ADServerRole.DomainController : ADServerRole.GlobalCatalog;
			bool flag = false;
			PooledLdapConnection pooledLdapConnection = null;
			PooledLdapConnection result;
			try
			{
				pooledLdapConnection = new PooledLdapConnection(serverInfo, role, false, networkCredential);
				if (LocatorFlags.None != connectionFlags)
				{
					pooledLdapConnection.SessionOptions.LocatorFlag |= connectionFlags;
				}
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_NEW_CONNECTION, null, new object[]
				{
					serverInfo.Fqdn,
					serverInfo.Port,
					string.Empty
				});
				pooledLdapConnection.BindWithRetry(3);
				pooledLdapConnection.SetNamingContexts();
				flag = true;
				result = pooledLdapConnection;
			}
			catch (LdapException ex)
			{
				throw new ADTransientException(DirectoryStrings.ExceptionCreateLdapConnection(serverInfo.FqdnPlusPort, ex.Message, (uint)ex.ErrorCode), ex);
			}
			finally
			{
				if (!flag && pooledLdapConnection != null)
				{
					pooledLdapConnection.ReturnToPool();
				}
			}
			return result;
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0003FADC File Offset: 0x0003DCDC
		[Conditional("DEBUG")]
		private void Dbg_VerifyStatesBefore_MakeDisconnected(ConnectionInfo info)
		{
		}

		// Token: 0x04000712 RID: 1810
		private const int WeightDns = 100;

		// Token: 0x04000713 RID: 1811
		private const int WeightRoundRobin = 10;

		// Token: 0x04000714 RID: 1812
		private const int WeightOutstandingRequests = 1000;

		// Token: 0x04000715 RID: 1813
		private const int MaxBindRetryAttempts = 3;

		// Token: 0x04000716 RID: 1814
		private ConnectionPoolType type;

		// Token: 0x04000717 RID: 1815
		private bool isActive;

		// Token: 0x04000718 RID: 1816
		private ADServerInfo[] serverInfos;

		// Token: 0x04000719 RID: 1817
		private ConnectionInfo[] connectionInfos;

		// Token: 0x0400071A RID: 1818
		private ReaderWriterLock poolLock;

		// Token: 0x0400071B RID: 1819
		private NetworkCredential credential;

		// Token: 0x0400071C RID: 1820
		private int connectedCount;

		// Token: 0x0400071D RID: 1821
		private int totalRequestCount;

		// Token: 0x0400071E RID: 1822
		private int lastUsedConnectionIndex;
	}
}
