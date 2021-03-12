using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x020002FB RID: 763
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DatabaseLocationCache
	{
		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06002293 RID: 8851 RVA: 0x0008AD1B File Offset: 0x00088F1B
		internal int Count
		{
			get
			{
				return this.m_cache.Count;
			}
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x0008AD28 File Offset: 0x00088F28
		internal DatabaseLocationInfo Find(Guid databaseId)
		{
			bool flag = false;
			try
			{
				int num = 0;
				while (num < 2 && !flag)
				{
					flag = this.m_rwlock.TryEnterReadLock(DatabaseLocationCache.s_cacheLockTimeout);
					num++;
				}
				if (flag)
				{
					TimedDbInfo timedDbInfo;
					if (this.m_cache.TryGetValue(databaseId, out timedDbInfo))
					{
						timedDbInfo.ResetExpiringCounter();
						return timedDbInfo.DbLocationInfo;
					}
				}
				else
				{
					ExAssert.RetailAssert(false, "Timeout waiting for write lock in DatabaseLocationCache.Find()");
				}
			}
			finally
			{
				if (flag)
				{
					this.m_rwlock.ExitReadLock();
				}
			}
			return null;
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x0008ADA8 File Offset: 0x00088FA8
		internal void Add(Guid databaseId, DatabaseLocationInfo dbLocationInfo)
		{
			bool flag = false;
			try
			{
				int num = 0;
				while (num < 2 && !flag)
				{
					flag = this.m_rwlock.TryEnterWriteLock(DatabaseLocationCache.s_cacheLockTimeout);
					num++;
				}
				if (flag)
				{
					this.m_cache[databaseId] = new TimedDbInfo(dbLocationInfo);
				}
				else
				{
					ExAssert.RetailAssert(false, "Timeout waiting for write lock in DatabaseLocationCache.Add()");
				}
			}
			finally
			{
				if (flag)
				{
					this.m_rwlock.ExitWriteLock();
				}
			}
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x0008AE1C File Offset: 0x0008901C
		internal Guid[] CopyDatabaseGuids()
		{
			bool flag = false;
			Guid[] result;
			try
			{
				int num = 0;
				while (num < 2 && !flag)
				{
					flag = this.m_rwlock.TryEnterReadLock(DatabaseLocationCache.s_cacheLockTimeout);
					num++;
				}
				if (flag)
				{
					Guid[] array = new Guid[this.m_cache.Keys.Count];
					this.m_cache.Keys.CopyTo(array, 0);
					result = array;
				}
				else
				{
					ExAssert.RetailAssert(false, "Timeout waiting for reader lock in DatabaseLocationCache.CopyDatabaseGuids()");
					result = null;
				}
			}
			finally
			{
				if (flag)
				{
					this.m_rwlock.ExitReadLock();
				}
			}
			return result;
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x0008AEAC File Offset: 0x000890AC
		internal bool CheckAndSetRPCLock(Guid databaseId)
		{
			bool flag = false;
			bool flag2 = false;
			try
			{
				int num = 0;
				while (num < 2 && !flag2)
				{
					flag2 = this.m_rpcRWLock.TryEnterUpgradeableReadLock(DatabaseLocationCache.s_cacheLockTimeout);
					num++;
				}
				if (flag2)
				{
					flag = this.m_inRPCcache.Contains(databaseId);
					if (flag)
					{
						goto IL_A6;
					}
					bool flag3 = false;
					try
					{
						int num2 = 0;
						while (num2 < 2 && !flag3)
						{
							flag3 = this.m_rpcRWLock.TryEnterWriteLock(DatabaseLocationCache.s_cacheLockTimeout);
							num2++;
						}
						if (flag3)
						{
							flag = this.m_inRPCcache.Contains(databaseId);
							if (!flag)
							{
								this.m_inRPCcache.Add(databaseId);
							}
						}
						else
						{
							ExAssert.RetailAssert(false, "Timeout waiting for write lock in DatabaseLocationCache.CheckAndSetRPCLock()");
						}
						goto IL_A6;
					}
					finally
					{
						if (flag3)
						{
							this.m_rpcRWLock.ExitWriteLock();
						}
					}
				}
				ExAssert.RetailAssert(false, "Timeout waiting for upgradable read lock in DatabaseLocationCache.CheckAndSetRPCLock()");
				IL_A6:;
			}
			finally
			{
				if (flag2)
				{
					this.m_rpcRWLock.ExitUpgradeableReadLock();
				}
			}
			return !flag;
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x0008AF90 File Offset: 0x00089190
		internal void ReleaseRPCLock(Guid databaseId)
		{
			bool flag = false;
			try
			{
				int num = 0;
				while (num < 2 && !flag)
				{
					flag = this.m_rpcRWLock.TryEnterUpgradeableReadLock(DatabaseLocationCache.s_cacheLockTimeout);
					num++;
				}
				if (flag)
				{
					if (!this.m_inRPCcache.Contains(databaseId))
					{
						goto IL_9C;
					}
					bool flag2 = false;
					try
					{
						int num2 = 0;
						while (num2 < 2 && !flag2)
						{
							flag2 = this.m_rpcRWLock.TryEnterWriteLock(DatabaseLocationCache.s_cacheLockTimeout);
							num2++;
						}
						if (flag2)
						{
							if (this.m_inRPCcache.Contains(databaseId))
							{
								this.m_inRPCcache.Remove(databaseId);
							}
						}
						else
						{
							ExAssert.RetailAssert(false, "Timeout waiting for write lock in DatabaseLocationCache.ReleaseRPCLock()");
						}
						goto IL_9C;
					}
					finally
					{
						if (flag2)
						{
							this.m_rpcRWLock.ExitWriteLock();
						}
					}
				}
				ExAssert.RetailAssert(false, "Timeout waiting for upgradable read lock in DatabaseLocationCache.ReleaseRPCLock()");
				IL_9C:;
			}
			finally
			{
				if (flag)
				{
					this.m_rpcRWLock.ExitUpgradeableReadLock();
				}
			}
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x0008B068 File Offset: 0x00089268
		internal void Update(Dictionary<Guid, DatabaseLocationInfo> newCache, int expiryThreshold, Dictionary<Guid, int> negativeExpiryThresholds)
		{
			bool flag = false;
			try
			{
				int num = 0;
				while (num < 2 && !flag)
				{
					flag = this.m_rwlock.TryEnterWriteLock(DatabaseLocationCache.s_cacheLockTimeout);
					num++;
				}
				if (flag)
				{
					foreach (KeyValuePair<Guid, DatabaseLocationInfo> keyValuePair in newCache)
					{
						Guid key = keyValuePair.Key;
						DatabaseLocationInfo value = keyValuePair.Value;
						TimedDbInfo timedDbInfo = null;
						if (this.m_cache.TryGetValue(key, out timedDbInfo))
						{
							timedDbInfo.ExpiringCounter++;
							if (!timedDbInfo.IsExpired(expiryThreshold) && value != null)
							{
								if (!value.Equals(timedDbInfo.DbLocationInfo))
								{
									timedDbInfo.DbLocationInfo = value;
								}
							}
							else if (value == null)
							{
								timedDbInfo.NegativeExpiringCounter++;
								if (timedDbInfo.IsNegativeCacheExpired(negativeExpiryThresholds[key]))
								{
									ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<Guid>((long)this.GetHashCode(), "RPC to find database {0} was not succesful and negative expire threshold was reached, removing database from cache.", key);
									this.m_cache.Remove(key);
								}
								else
								{
									ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<Guid>((long)this.GetHashCode(), "RPC to find database {0} was not succesful but negative expire threshold is not reached, keeping stale info in the cache.", key);
								}
							}
							else
							{
								ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Removing database {0} from cache since the expiry threshold has reached", key);
								this.m_cache.Remove(key);
							}
						}
					}
					ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<int>((long)this.GetHashCode(), "Total number of elements in database cache = {0}", this.m_cache.Count);
				}
				else
				{
					ExAssert.RetailAssert(false, "Timeout waiting for write lock in DatabaseLocationCache.Update()");
				}
			}
			finally
			{
				if (flag)
				{
					this.m_rwlock.ExitWriteLock();
				}
			}
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x0008B22C File Offset: 0x0008942C
		internal void Clear()
		{
			bool flag = false;
			try
			{
				int num = 0;
				while (num < 2 && !flag)
				{
					flag = this.m_rwlock.TryEnterWriteLock(DatabaseLocationCache.s_cacheLockTimeout);
					num++;
				}
				if (flag)
				{
					this.m_cache.Clear();
					ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<int>((long)this.GetHashCode(), "Cleared cache. Total number of elements in database cache = {0}", this.m_cache.Count);
				}
				else
				{
					ExAssert.RetailAssert(false, "Timeout waiting for write lock in DatabaseLocationCache.Clear()");
				}
			}
			finally
			{
				if (flag)
				{
					this.m_rwlock.ExitWriteLock();
				}
			}
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x0008B2B8 File Offset: 0x000894B8
		internal void ForceExpire()
		{
			bool flag = false;
			try
			{
				int num = 0;
				while (num < 2 && !flag)
				{
					flag = this.m_rwlock.TryEnterWriteLock(DatabaseLocationCache.s_cacheLockTimeout);
					num++;
				}
				if (flag)
				{
					this.m_cache.Clear();
				}
				else
				{
					ExAssert.RetailAssert(false, "Timeout waiting for write lock in DatabaseLocationCache.ForceExpire()");
				}
			}
			finally
			{
				if (!flag)
				{
					this.m_rwlock.ExitWriteLock();
				}
			}
		}

		// Token: 0x04001413 RID: 5139
		private readonly Dictionary<Guid, TimedDbInfo> m_cache = new Dictionary<Guid, TimedDbInfo>(16);

		// Token: 0x04001414 RID: 5140
		private readonly HashSet<Guid> m_inRPCcache = new HashSet<Guid>();

		// Token: 0x04001415 RID: 5141
		private readonly ReaderWriterLockSlim m_rwlock = new ReaderWriterLockSlim();

		// Token: 0x04001416 RID: 5142
		private readonly ReaderWriterLockSlim m_rpcRWLock = new ReaderWriterLockSlim();

		// Token: 0x04001417 RID: 5143
		private static readonly TimeSpan s_cacheLockTimeout = TimeSpan.FromSeconds(7.5);
	}
}
