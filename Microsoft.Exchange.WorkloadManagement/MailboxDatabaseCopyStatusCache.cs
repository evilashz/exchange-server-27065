using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Exchange.WorkloadManagement.EventLogs;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200001C RID: 28
	internal class MailboxDatabaseCopyStatusCache
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00004D46 File Offset: 0x00002F46
		protected MailboxDatabaseCopyStatusCache()
		{
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004D64 File Offset: 0x00002F64
		public static Hookable<MailboxDatabaseCopyStatusCache> Instance
		{
			get
			{
				return MailboxDatabaseCopyStatusCache.instance;
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004DE4 File Offset: 0x00002FE4
		public virtual RpcDatabaseCopyStatus2 TryGetCopyStatus(string serverFqdn, Guid mdbGuid)
		{
			MailboxDatabaseCopyStatusCache.CopyStatusCacheEntry copyStatusCacheEntry = null;
			try
			{
				this.lockObject.EnterUpgradeableReadLock();
				if (!this.data.TryGetValue(serverFqdn, out copyStatusCacheEntry))
				{
					try
					{
						this.lockObject.EnterWriteLock();
						if (!this.data.TryGetValue(serverFqdn, out copyStatusCacheEntry))
						{
							copyStatusCacheEntry = new MailboxDatabaseCopyStatusCache.CopyStatusCacheEntry();
							this.data.Add(serverFqdn, copyStatusCacheEntry);
						}
					}
					finally
					{
						try
						{
							this.lockObject.ExitWriteLock();
						}
						catch (SynchronizationLockException)
						{
						}
					}
				}
			}
			finally
			{
				try
				{
					this.lockObject.ExitUpgradeableReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			RpcDatabaseCopyStatus2 result;
			try
			{
				copyStatusCacheEntry.LockObject.EnterUpgradeableReadLock();
				if (copyStatusCacheEntry.UpdateTime > ExDateTime.UtcNow - CiHealthMonitorConfiguration.RefreshInterval)
				{
					result = copyStatusCacheEntry.TryGetCopyStatus(mdbGuid);
				}
				else
				{
					try
					{
						copyStatusCacheEntry.LockObject.EnterWriteLock();
						if (copyStatusCacheEntry.UpdateTime > ExDateTime.UtcNow - CiHealthMonitorConfiguration.RefreshInterval)
						{
							return copyStatusCacheEntry.TryGetCopyStatus(mdbGuid);
						}
						RpcDatabaseCopyStatus2[] results = null;
						RpcErrorExceptionInfo errorInfo = null;
						try
						{
							AmRpcExceptionWrapper.Instance.ClientRetryableOperation(serverFqdn, delegate
							{
								using (ReplayRpcClient replayRpcClient = new ReplayRpcClient(serverFqdn))
								{
									replayRpcClient.SetTimeOut((int)CiHealthMonitorConfiguration.RpcTimeout.TotalMilliseconds);
									errorInfo = replayRpcClient.RpccGetCopyStatusEx4(RpcGetDatabaseCopyStatusFlags2.None, MailboxDatabaseCopyStatusCache.dbGuidsForCachedCopyStatus, ref results);
								}
							});
							if (!errorInfo.IsFailed())
							{
								copyStatusCacheEntry.Update(results);
							}
							else
							{
								ExTraceGlobals.ResourceHealthManagerTracer.TraceError<string>((long)this.GetHashCode(), "[MailboxDatabaseCopyStatusCache::TryGetCopyStatus] RPC call failed, error: {0}", errorInfo.ErrorMessage);
								WorkloadManagerEventLogger.LogEvent(WorkloadManagementEventLogConstants.Tuple_CiMdbCopyStatusFailure, string.Empty, new object[]
								{
									serverFqdn,
									errorInfo.ErrorMessage
								});
								copyStatusCacheEntry.Update();
							}
						}
						catch (AmServerException ex)
						{
							ExTraceGlobals.ResourceHealthManagerTracer.TraceError<AmServerException>((long)this.GetHashCode(), "[MailboxDatabaseCopyStatusCache::TryGetCopyStatus] Failed to execute RPC call, exception: {0}", ex);
							if (!(ex is AmReplayServiceDownException))
							{
								WorkloadManagerEventLogger.LogEvent(WorkloadManagementEventLogConstants.Tuple_CiMdbCopyStatusFailure, string.Empty, new object[]
								{
									serverFqdn,
									ex.ToString()
								});
							}
							copyStatusCacheEntry.Update();
						}
					}
					finally
					{
						try
						{
							copyStatusCacheEntry.LockObject.ExitWriteLock();
						}
						catch (SynchronizationLockException)
						{
						}
					}
					result = copyStatusCacheEntry.TryGetCopyStatus(mdbGuid);
				}
			}
			finally
			{
				try
				{
					copyStatusCacheEntry.LockObject.ExitUpgradeableReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return result;
		}

		// Token: 0x04000076 RID: 118
		private static readonly Hookable<MailboxDatabaseCopyStatusCache> instance = Hookable<MailboxDatabaseCopyStatusCache>.Create(true, new MailboxDatabaseCopyStatusCache());

		// Token: 0x04000077 RID: 119
		private static readonly Guid[] dbGuidsForCachedCopyStatus = new Guid[]
		{
			Guid.Empty
		};

		// Token: 0x04000078 RID: 120
		private readonly Dictionary<string, MailboxDatabaseCopyStatusCache.CopyStatusCacheEntry> data = new Dictionary<string, MailboxDatabaseCopyStatusCache.CopyStatusCacheEntry>();

		// Token: 0x04000079 RID: 121
		private readonly ReaderWriterLockSlim lockObject = new ReaderWriterLockSlim();

		// Token: 0x0200001D RID: 29
		private class CopyStatusCacheEntry
		{
			// Token: 0x06000107 RID: 263 RVA: 0x00005143 File Offset: 0x00003343
			public CopyStatusCacheEntry()
			{
				this.UpdateTime = ExDateTime.MinValue;
				this.LockObject = new ReaderWriterLockSlim();
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x06000108 RID: 264 RVA: 0x0000516C File Offset: 0x0000336C
			// (set) Token: 0x06000109 RID: 265 RVA: 0x00005174 File Offset: 0x00003374
			public ExDateTime UpdateTime { get; private set; }

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x0600010A RID: 266 RVA: 0x0000517D File Offset: 0x0000337D
			// (set) Token: 0x0600010B RID: 267 RVA: 0x00005185 File Offset: 0x00003385
			public ReaderWriterLockSlim LockObject { get; private set; }

			// Token: 0x0600010C RID: 268 RVA: 0x00005190 File Offset: 0x00003390
			public RpcDatabaseCopyStatus2 TryGetCopyStatus(Guid mdbGuid)
			{
				RpcDatabaseCopyStatus2 result = null;
				if (this.data.TryGetValue(mdbGuid, out result))
				{
					return result;
				}
				return null;
			}

			// Token: 0x0600010D RID: 269 RVA: 0x000051B4 File Offset: 0x000033B4
			public void Update(IEnumerable<RpcDatabaseCopyStatus2> data)
			{
				this.UpdateTime = ExDateTime.UtcNow;
				this.data.Clear();
				foreach (RpcDatabaseCopyStatus2 rpcDatabaseCopyStatus in data)
				{
					this.data[rpcDatabaseCopyStatus.DBGuid] = rpcDatabaseCopyStatus;
				}
			}

			// Token: 0x0600010E RID: 270 RVA: 0x00005220 File Offset: 0x00003420
			public void Update()
			{
				this.UpdateTime = ExDateTime.UtcNow;
				this.data.Clear();
			}

			// Token: 0x0400007A RID: 122
			private readonly Dictionary<Guid, RpcDatabaseCopyStatus2> data = new Dictionary<Guid, RpcDatabaseCopyStatus2>();
		}
	}
}
