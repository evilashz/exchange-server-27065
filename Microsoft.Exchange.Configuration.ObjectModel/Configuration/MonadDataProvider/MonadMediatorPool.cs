using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001D1 RID: 465
	internal sealed class MonadMediatorPool : IDisposable
	{
		// Token: 0x060010A3 RID: 4259 RVA: 0x00032D58 File Offset: 0x00030F58
		public MonadMediatorPool() : this(1)
		{
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00032D61 File Offset: 0x00030F61
		public MonadMediatorPool(int runspaceMediatorSize) : this(runspaceMediatorSize, TimeSpan.Zero)
		{
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00032D6F File Offset: 0x00030F6F
		public MonadMediatorPool(int runspaceMediatorSize, TimeSpan inactivityCacheCleanupThreshold)
		{
			this.runspaceMediatorSize = runspaceMediatorSize;
			this.inactivityCacheCleanupThreshold = inactivityCacheCleanupThreshold;
			this.lastUpdatedKey = -1;
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060010A6 RID: 4262 RVA: 0x00032D97 File Offset: 0x00030F97
		internal MonadRunspaceCache[] RunspaceCache
		{
			get
			{
				if (!Environment.StackTrace.Contains("RemoteMonadCommandTester"))
				{
					throw new NotSupportedException("RunspaceCache property should never be accessed from Production Code.");
				}
				return this.connectionPooledInstanceCache;
			}
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00032DBC File Offset: 0x00030FBC
		public void Dispose()
		{
			if (this.connectionPooledInstanceCache != null)
			{
				foreach (MonadRunspaceCache monadRunspaceCache in this.connectionPooledInstanceCache)
				{
					try
					{
						monadRunspaceCache.Dispose();
					}
					catch (Exception)
					{
					}
				}
			}
			this.currentKey = null;
			this.connectionPooledInstace = null;
			this.connectionPooledInstanceCache = null;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00032E1C File Offset: 0x0003101C
		internal RunspaceMediator GetRunspacePooledMediatorInstance()
		{
			if (this.pooledInstance == null)
			{
				lock (this.syncInstance)
				{
					if (this.pooledInstance == null)
					{
						this.pooledInstance = new RunspaceMediator(MonadRunspaceFactory.GetInstance(), new MonadRunspaceCache());
					}
				}
			}
			return this.pooledInstance;
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00032E84 File Offset: 0x00031084
		internal RunspaceMediator GetRunspaceNonPooledMediatorInstance()
		{
			if (this.nonPooledInstance == null)
			{
				lock (this.syncInstance)
				{
					if (this.nonPooledInstance == null)
					{
						this.nonPooledInstance = new RunspaceMediator(MonadRunspaceFactory.GetInstance(), new EmptyRunspaceCache());
					}
				}
			}
			return this.nonPooledInstance;
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00032EEC File Offset: 0x000310EC
		internal RunspaceMediator GetRunspacePooledMediatorInstance(MonadMediatorPoolKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this.connectionPooledInstace != null)
			{
				RunspaceMediator runspaceMediator = null;
				MonadRunspaceCache result = null;
				for (int i = 0; i < this.runspaceMediatorSize; i++)
				{
					if (key.Equals(this.currentKey[i]) && !this.connectionPooledInstanceCache[i].IsDisposed)
					{
						runspaceMediator = this.connectionPooledInstace[i];
						result = this.connectionPooledInstanceCache[i];
						break;
					}
				}
				if (runspaceMediator != null)
				{
					this.CleanUpCache(this.connectionPooledInstanceCache, this.inactivityCacheCleanupThreshold, result);
					return runspaceMediator;
				}
			}
			else
			{
				lock (this.syncInstance)
				{
					if (this.connectionPooledInstace == null)
					{
						this.connectionPooledInstace = new RunspaceMediator[this.runspaceMediatorSize];
						this.connectionPooledInstanceCache = new MonadRunspaceCache[this.runspaceMediatorSize];
						this.currentKey = new MonadMediatorPoolKey[this.runspaceMediatorSize];
					}
				}
			}
			RunspaceMediator result2;
			lock (this.syncInstance)
			{
				this.CleanUpCache(this.connectionPooledInstanceCache, this.inactivityCacheCleanupThreshold, null);
				int num = this.ResolveNextCacheToReplace();
				if (this.connectionPooledInstanceCache[num] != null && !this.connectionPooledInstanceCache[num].IsDisposed)
				{
					this.connectionPooledInstanceCache[num].Dispose();
				}
				this.connectionPooledInstanceCache[num] = new MonadRunspaceCache();
				this.connectionPooledInstace[num] = new RunspaceMediator(new MonadRemoteRunspaceFactory(key.ConnectionInfo, key.ServerSettings), this.connectionPooledInstanceCache[num]);
				this.currentKey[num] = key;
				this.lastUpdatedKey = num;
				result2 = this.connectionPooledInstace[num];
			}
			return result2;
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x000330AC File Offset: 0x000312AC
		private int ResolveNextCacheToReplace()
		{
			int num = (this.lastUpdatedKey + 1) % this.runspaceMediatorSize;
			if (this.connectionPooledInstanceCache[num] == null || this.connectionPooledInstanceCache[num].IsDisposed)
			{
				return num;
			}
			for (int i = 0; i < this.runspaceMediatorSize; i++)
			{
				if (this.connectionPooledInstanceCache[i] == null || this.connectionPooledInstanceCache[i].IsDisposed)
				{
					return i;
				}
			}
			return num;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00033114 File Offset: 0x00031314
		private void CleanUpCache(MonadRunspaceCache[] connectionPooledInstanceCache, TimeSpan inactivityCacheCleanupThreshold, MonadRunspaceCache result)
		{
			if (connectionPooledInstanceCache == null)
			{
				return;
			}
			if (inactivityCacheCleanupThreshold == TimeSpan.Zero)
			{
				return;
			}
			for (int i = 0; i < connectionPooledInstanceCache.Length; i++)
			{
				if (connectionPooledInstanceCache[i] != null && (result == null || result != connectionPooledInstanceCache[i]) && !connectionPooledInstanceCache[i].IsDisposed)
				{
					TimeSpan t = ExDateTime.UtcNow - connectionPooledInstanceCache[i].LastTimeCacheUsed;
					if (t > inactivityCacheCleanupThreshold)
					{
						connectionPooledInstanceCache[i].Dispose();
					}
				}
			}
		}

		// Token: 0x04000399 RID: 921
		private const int DefaultRunspaceMediatorSize = 1;

		// Token: 0x0400039A RID: 922
		private object syncInstance = new object();

		// Token: 0x0400039B RID: 923
		private RunspaceMediator pooledInstance;

		// Token: 0x0400039C RID: 924
		private RunspaceMediator nonPooledInstance;

		// Token: 0x0400039D RID: 925
		private MonadRunspaceCache[] connectionPooledInstanceCache;

		// Token: 0x0400039E RID: 926
		private volatile RunspaceMediator[] connectionPooledInstace;

		// Token: 0x0400039F RID: 927
		private MonadMediatorPoolKey[] currentKey;

		// Token: 0x040003A0 RID: 928
		private int runspaceMediatorSize;

		// Token: 0x040003A1 RID: 929
		private int lastUpdatedKey;

		// Token: 0x040003A2 RID: 930
		private TimeSpan inactivityCacheCleanupThreshold;
	}
}
