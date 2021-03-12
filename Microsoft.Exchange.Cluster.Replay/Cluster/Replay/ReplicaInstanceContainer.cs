using System;
using System.Threading;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200030A RID: 778
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplicaInstanceContainer : DisposeTrackableBase
	{
		// Token: 0x06001FDD RID: 8157 RVA: 0x000932BC File Offset: 0x000914BC
		public ReplicaInstanceContainer(ReplicaInstance instance, ReplicaInstanceActionQueue queue, IPerfmonCounters perfCounters)
		{
			this.ReplicaInstance = instance;
			this.Queue = queue;
			this.PerformanceCounters = perfCounters;
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x0009330A File Offset: 0x0009150A
		internal void UpdateReplicaInstance(ReplicaInstance newInstance)
		{
			this.ReplicaInstance = newInstance;
			if (this.m_cachedStatus != null)
			{
				this.m_cachedStatus.ForceRefresh = true;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06001FDF RID: 8159 RVA: 0x00093327 File Offset: 0x00091527
		// (set) Token: 0x06001FE0 RID: 8160 RVA: 0x0009332F File Offset: 0x0009152F
		internal ReplicaInstance ReplicaInstance { get; private set; }

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x00093338 File Offset: 0x00091538
		// (set) Token: 0x06001FE2 RID: 8162 RVA: 0x00093340 File Offset: 0x00091540
		internal ReplicaInstanceActionQueue Queue { get; private set; }

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x00093349 File Offset: 0x00091549
		// (set) Token: 0x06001FE4 RID: 8164 RVA: 0x00093351 File Offset: 0x00091551
		internal IPerfmonCounters PerformanceCounters { get; private set; }

		// Token: 0x06001FE5 RID: 8165 RVA: 0x0009335C File Offset: 0x0009155C
		internal RpcDatabaseCopyStatus2 GetCopyStatus(RpcGetDatabaseCopyStatusFlags2 flags)
		{
			if ((flags & RpcGetDatabaseCopyStatusFlags2.ReadThrough) == RpcGetDatabaseCopyStatusFlags2.None)
			{
				return this.GetCachedCopyStatus(flags);
			}
			RpcDatabaseCopyStatus2 copyStatusWithTimeout = this.GetCopyStatusWithTimeout(flags);
			CopyStatusServerCachedEntry copyStatusServerCachedEntry = this.UpdateCachedCopyStatus(copyStatusWithTimeout);
			return copyStatusServerCachedEntry.CopyStatus;
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x0009338C File Offset: 0x0009158C
		private RpcDatabaseCopyStatus2 GetCachedCopyStatus(RpcGetDatabaseCopyStatusFlags2 flags)
		{
			CopyStatusServerCachedEntry copyStatusServerCachedEntry = this.m_cachedStatus;
			if (this.IsCopyStatusReadThroughNeeded(copyStatusServerCachedEntry))
			{
				TimeSpan copyStatusServerTimeout = ReplicaInstanceContainer.CopyStatusServerTimeout;
				bool flag = Monitor.TryEnter(this.m_statusCallLocker);
				try
				{
					if (!flag)
					{
						ManualOneShotEvent.Result result = this.m_firstCachedStatusCallCompleted.WaitOne(copyStatusServerTimeout);
						if (result != ManualOneShotEvent.Result.Success)
						{
							throw new ReplayServiceRpcCopyStatusTimeoutException(this.ReplicaInstance.Configuration.DisplayName, (int)copyStatusServerTimeout.TotalSeconds);
						}
						lock (this.m_statusCacheLocker)
						{
							copyStatusServerCachedEntry = this.m_cachedStatus;
							if (copyStatusServerCachedEntry == null)
							{
								throw new ReplayServiceRpcCopyStatusTimeoutException(this.ReplicaInstance.Configuration.DisplayName, (int)copyStatusServerTimeout.TotalSeconds);
							}
							if (copyStatusServerCachedEntry.CreateTimeUtc < DateTime.UtcNow - ReplicaInstanceContainer.CopyStatusStaleTimeout)
							{
								Exception ex = new ReplayServiceRpcCopyStatusTimeoutException(this.ReplicaInstance.Configuration.DisplayName, (int)ReplicaInstanceContainer.CopyStatusStaleTimeout.TotalSeconds);
								copyStatusServerCachedEntry.CopyStatus.CopyStatus = CopyStatusEnum.Failed;
								copyStatusServerCachedEntry.CopyStatus.ErrorMessage = ex.Message;
								copyStatusServerCachedEntry.CopyStatus.ExtendedErrorInfo = new ExtendedErrorInfo(ex);
							}
							return copyStatusServerCachedEntry.CopyStatus;
						}
					}
					copyStatusServerCachedEntry = this.m_cachedStatus;
					if (this.IsCopyStatusReadThroughNeeded(copyStatusServerCachedEntry))
					{
						try
						{
							ExTraceGlobals.ReplayServiceRpcTracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "GetCachedCopyStatus() for DB '{0}' ({1}): Cache TTL expired or force refresh requested.", this.ReplicaInstance.Configuration.DisplayName, this.ReplicaInstance.Configuration.IdentityGuid);
							RpcDatabaseCopyStatus2 copyStatusWithTimeout = this.GetCopyStatusWithTimeout(flags);
							copyStatusServerCachedEntry = this.UpdateCachedCopyStatus(copyStatusWithTimeout);
						}
						catch (TimeoutException arg)
						{
							ExTraceGlobals.ReplayServiceRpcTracer.TraceError<string, TimeoutException>((long)this.GetHashCode(), "GetCachedCopyStatus() Timeout for DB '{0}': {1}", this.ReplicaInstance.Configuration.DisplayName, arg);
							throw new ReplayServiceRpcCopyStatusTimeoutException(this.ReplicaInstance.Configuration.DisplayName, (int)copyStatusServerTimeout.TotalSeconds);
						}
						finally
						{
							this.m_firstCachedStatusCallCompleted.Set();
						}
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(this.m_statusCallLocker);
					}
				}
			}
			return copyStatusServerCachedEntry.CopyStatus;
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x000935EC File Offset: 0x000917EC
		private CopyStatusServerCachedEntry UpdateCachedCopyStatus(RpcDatabaseCopyStatus2 status)
		{
			CopyStatusServerCachedEntry copyStatusServerCachedEntry = new CopyStatusServerCachedEntry(status);
			CopyStatusServerCachedEntry cachedStatus;
			lock (this.m_statusCacheLocker)
			{
				if (CopyStatusHelper.CheckCopyStatusNewer(copyStatusServerCachedEntry, this.m_cachedStatus))
				{
					this.m_cachedStatus = copyStatusServerCachedEntry;
				}
				cachedStatus = this.m_cachedStatus;
			}
			return cachedStatus;
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x00093674 File Offset: 0x00091874
		private RpcDatabaseCopyStatus2 GetCopyStatusWithTimeout(RpcGetDatabaseCopyStatusFlags2 flags)
		{
			RpcDatabaseCopyStatus2 status = null;
			TimeSpan invokeTimeout = InvokeWithTimeout.InfiniteTimeSpan;
			if (RegistryParameters.GetCopyStatusServerTimeoutEnabled)
			{
				invokeTimeout = ReplicaInstanceContainer.CopyStatusServerTimeout;
			}
			InvokeWithTimeout.Invoke(delegate()
			{
				status = this.ReplicaInstance.GetCopyStatus(flags);
			}, null, invokeTimeout, true, null);
			return status;
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x000936CA File Offset: 0x000918CA
		private bool IsCopyStatusReadThroughNeeded(CopyStatusServerCachedEntry cachedStatus)
		{
			return cachedStatus == null || cachedStatus.ForceRefresh || cachedStatus.CreateTimeUtc < DateTime.UtcNow - ReplicaInstanceContainer.CopyStatusCacheTTL;
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x000936F3 File Offset: 0x000918F3
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.m_firstCachedStatusCallCompleted != null)
			{
				this.m_firstCachedStatusCallCompleted.Close();
			}
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x0009370B File Offset: 0x0009190B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ReplicaInstanceContainer>(this);
		}

		// Token: 0x04000D19 RID: 3353
		private const string FirstStatusCallCompletedEventName = "FirstStatusCallCompletedEvent";

		// Token: 0x04000D1A RID: 3354
		private static readonly TimeSpan CopyStatusCacheTTL = TimeSpan.FromSeconds((double)RegistryParameters.GetCopyStatusRpcCacheTTLInSec);

		// Token: 0x04000D1B RID: 3355
		private static readonly TimeSpan CopyStatusServerTimeout = TimeSpan.FromSeconds((double)RegistryParameters.GetCopyStatusServerTimeoutSec);

		// Token: 0x04000D1C RID: 3356
		private static readonly TimeSpan CopyStatusStaleTimeout = TimeSpan.FromSeconds((double)RegistryParameters.GetCopyStatusServerCachedEntryStaleTimeoutSec);

		// Token: 0x04000D1D RID: 3357
		private CopyStatusServerCachedEntry m_cachedStatus;

		// Token: 0x04000D1E RID: 3358
		private object m_statusCacheLocker = new object();

		// Token: 0x04000D1F RID: 3359
		private object m_statusCallLocker = new object();

		// Token: 0x04000D20 RID: 3360
		private ManualOneShotEvent m_firstCachedStatusCallCompleted = new ManualOneShotEvent("FirstStatusCallCompletedEvent");
	}
}
