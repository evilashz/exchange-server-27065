using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Diagnostics.Performance;
using Microsoft.Exchange.Net;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Storage.Cluster.DirectoryServices
{
	// Token: 0x0200042D RID: 1069
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class AdObjectLookupCache<TADWrapperObject> : IFindAdObject<TADWrapperObject> where TADWrapperObject : class, IADObjectCommon
	{
		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06002FD8 RID: 12248 RVA: 0x000C4240 File Offset: 0x000C2440
		// (set) Token: 0x06002FD9 RID: 12249 RVA: 0x000C4248 File Offset: 0x000C2448
		public bool MinimizeObjects { get; set; }

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06002FDA RID: 12250 RVA: 0x000C4254 File Offset: 0x000C2454
		private static bool LogAdLatency
		{
			get
			{
				if (AdObjectLookupCache<TADWrapperObject>.s_logAdLatency == null)
				{
					AdObjectLookupCache<TADWrapperObject>.s_logAdLatency = new bool?(false);
					Exception ex = null;
					try
					{
						using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\HA\\AdObjectLookupCache"))
						{
							if (registryKey != null)
							{
								int value = RegistryReader.Instance.GetValue<int>(registryKey, null, "LogADLatency", 0);
								if (value != 0)
								{
									AdObjectLookupCache<TADWrapperObject>.s_logAdLatency = new bool?(true);
								}
							}
						}
					}
					catch (SecurityException ex2)
					{
						ex = ex2;
					}
					catch (IOException ex3)
					{
						ex = ex3;
					}
					catch (UnauthorizedAccessException ex4)
					{
						ex = ex4;
					}
					if (ex != null)
					{
						AdObjectLookupCache<TADWrapperObject>.Tracer.TraceError<Exception>(0L, "LogAdLatency failed to read regkey: {0}", ex);
						AdObjectLookupCache<TADWrapperObject>.s_logAdLatency = null;
					}
				}
				return AdObjectLookupCache<TADWrapperObject>.s_logAdLatency != null && AdObjectLookupCache<TADWrapperObject>.s_logAdLatency.Value;
			}
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x000C4340 File Offset: 0x000C2540
		public AdObjectLookupCache(IADToplogyConfigurationSession adSession) : this(adSession, AdObjectLookupCache<TADWrapperObject>.TimeToLive, AdObjectLookupCache<TADWrapperObject>.TimeToNegativeLive, AdObjectLookupCache<TADWrapperObject>.CacheLockTimeout, AdObjectLookupCache<TADWrapperObject>.AdOperationTimeout, AdObjectLookupCache<TADWrapperObject>.MaximumTimeToLive)
		{
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x000C4362 File Offset: 0x000C2562
		public AdObjectLookupCache(IADToplogyConfigurationSession adSession, TimeSpan timeToLive, TimeSpan timeToNegativeLive) : this(adSession, timeToLive, timeToNegativeLive, AdObjectLookupCache<TADWrapperObject>.CacheLockTimeout, AdObjectLookupCache<TADWrapperObject>.AdOperationTimeout, AdObjectLookupCache<TADWrapperObject>.MaximumTimeToLive)
		{
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x000C437C File Offset: 0x000C257C
		public AdObjectLookupCache(IADToplogyConfigurationSession adSession, TimeSpan timeToLive, TimeSpan timeToNegativeLive, TimeSpan cacheLockTimeout, TimeSpan adOperationTimeout) : this(adSession, timeToLive, timeToNegativeLive, cacheLockTimeout, adOperationTimeout, AdObjectLookupCache<TADWrapperObject>.MaximumTimeToLive)
		{
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x000C4390 File Offset: 0x000C2590
		public AdObjectLookupCache(IADToplogyConfigurationSession adSession, TimeSpan timeToLive, TimeSpan timeToNegativeLive, TimeSpan cacheLockTimeout, TimeSpan adOperationTimeout, TimeSpan maximumCacheTimeout)
		{
			this.AdSession = adSession;
			this.m_timeToLive = timeToLive;
			this.m_timeToNegativeLive = timeToNegativeLive;
			this.m_cacheLockTimeout = cacheLockTimeout;
			this.m_adOperationTimeout = adOperationTimeout;
			this.m_maximumTimeToLive = maximumCacheTimeout;
		}

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06002FDF RID: 12255 RVA: 0x000C4407 File Offset: 0x000C2607
		public static Microsoft.Exchange.Diagnostics.Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ActiveManagerClientTracer;
			}
		}

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06002FE0 RID: 12256 RVA: 0x000C440E File Offset: 0x000C260E
		// (set) Token: 0x06002FE1 RID: 12257 RVA: 0x000C4416 File Offset: 0x000C2616
		public IADToplogyConfigurationSession AdSession { get; set; }

		// Token: 0x06002FE2 RID: 12258 RVA: 0x000C4420 File Offset: 0x000C2620
		public void Clear()
		{
			bool flag = false;
			try
			{
				int num = 0;
				while (num < 2 && !flag)
				{
					flag = this.m_rwLock.TryEnterWriteLock(this.m_cacheLockTimeout);
					num++;
				}
				if (flag)
				{
					this.m_cache.Clear();
				}
				else
				{
					AdObjectLookupCache<TADWrapperObject>.Tracer.TraceError((long)this.GetHashCode(), "AdObjectLookupCache.Clear cound not clear cache due to lock timeout");
				}
			}
			finally
			{
				if (flag)
				{
					this.m_rwLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x000C4498 File Offset: 0x000C2698
		public TADWrapperObject ReadAdObjectByObjectId(ADObjectId objectId)
		{
			Exception ex;
			return this.ReadAdObjectByObjectIdEx(objectId, out ex);
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x000C4578 File Offset: 0x000C2778
		public TADWrapperObject ReadAdObjectByObjectIdEx(ADObjectId objectId, out Exception exception)
		{
			TADWrapperObject result = default(TADWrapperObject);
			string name = objectId.Name;
			Exception tempEx = null;
			exception = null;
			result = this.LookupOrFindAdObject(name, delegate
			{
				TADWrapperObject adObject = default(TADWrapperObject);
				tempEx = ADUtils.RunADOperation(delegate()
				{
					adObject = this.AdSession.ReadADObject<TADWrapperObject>(objectId);
				}, 2);
				if (tempEx != null)
				{
					StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_ErrorActiveManagerClientADError, tempEx.Message, new object[0]);
					AdObjectLookupCache<TADWrapperObject>.Tracer.TraceError<Exception>((long)this.GetHashCode(), "AdObjectLookupCache.ReadAdObjectByObjectIdEx got exception: {0}", tempEx);
				}
				return adObject;
			}, AdObjectLookupFlags.None);
			exception = tempEx;
			return result;
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x000C45F8 File Offset: 0x000C27F8
		public TADWrapperObject FindAdObjectByGuid(Guid objectGuid)
		{
			return this.LookupOrFindAdObject(objectGuid.ToString(), () => SimpleAdObjectLookup<TADWrapperObject>.FindAdObjectTypeByGuidStatic(this.AdSession, objectGuid), AdObjectLookupFlags.None);
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x000C463D File Offset: 0x000C283D
		public TADWrapperObject FindAdObjectByGuidEx(Guid objectGuid, AdObjectLookupFlags flags)
		{
			return this.FindAdObjectByGuidEx(objectGuid, flags, NullPerformanceDataLogger.Instance);
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x000C4674 File Offset: 0x000C2874
		public TADWrapperObject FindAdObjectByGuidEx(Guid objectGuid, AdObjectLookupFlags flags, IPerformanceDataLogger perfLogger)
		{
			return this.LookupOrFindAdObject(objectGuid.ToString(), () => SimpleAdObjectLookup<TADWrapperObject>.FindAdObjectTypeByGuidStatic(this.AdSession, objectGuid, perfLogger), flags);
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x000C46E0 File Offset: 0x000C28E0
		public TADWrapperObject FindAdObjectByQuery(QueryFilter queryFilter)
		{
			return this.LookupOrFindAdObject(queryFilter.ToString(), () => SimpleAdObjectLookup<TADWrapperObject>.FindAdObjectWithQueryStatic(this.AdSession, queryFilter), AdObjectLookupFlags.None);
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x000C4740 File Offset: 0x000C2940
		public TADWrapperObject FindAdObjectByQueryEx(QueryFilter queryFilter, AdObjectLookupFlags flags)
		{
			return this.LookupOrFindAdObject(queryFilter.ToString(), () => SimpleAdObjectLookup<TADWrapperObject>.FindAdObjectWithQueryStatic(this.AdSession, queryFilter), flags);
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x000C4780 File Offset: 0x000C2980
		public TADWrapperObject FindServerByFqdn(string fqdn)
		{
			Exception ex;
			return this.FindServerByFqdnWithException(fqdn, out ex);
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000C47BC File Offset: 0x000C29BC
		public TADWrapperObject FindServerByFqdnWithException(string fqdn, out Exception exception)
		{
			ExAssert.RetailAssert(typeof(TADWrapperObject) == typeof(IADServer), "This function should only be called with Server objects!");
			if (typeof(TADWrapperObject) != typeof(IADServer))
			{
				throw new NotImplementedException("This only works for Server objects.");
			}
			exception = null;
			Exception ex = null;
			AdObjectLookupFlags flags = AdObjectLookupFlags.None;
			string shortName = MachineName.GetNodeNameFromFqdn(fqdn);
			TADWrapperObject result = this.LookupOrFindAdObject(shortName, () => SimpleAdObjectLookup<TADWrapperObject>.FindAdObjectByServerNameStatic(this.AdSession, shortName, out ex), flags);
			exception = ex;
			return result;
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000C485C File Offset: 0x000C2A5C
		internal static ActiveManagerClientPerfmonInstance GetPerfCounters()
		{
			ActiveManagerClientPerfmonInstance instance;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				string instanceName = string.Format("{0} - {1}", currentProcess.ProcessName, currentProcess.Id);
				instance = ActiveManagerClientPerfmon.GetInstance(instanceName);
			}
			return instance;
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000C48B0 File Offset: 0x000C2AB0
		private static bool ShouldExpireCacheEntry(AdObjectCacheEntry<TADWrapperObject> entry)
		{
			return DateTime.UtcNow.CompareTo(entry.TimeToExpire) > 0;
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x000C48D8 File Offset: 0x000C2AD8
		private static bool MaximumTimeToLiveExpired(AdObjectCacheEntry<TADWrapperObject> entry)
		{
			return DateTime.UtcNow.CompareTo(entry.MaximumTimeToExpire) > 0;
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000C4900 File Offset: 0x000C2B00
		internal bool CheckAndSetADLock(string identifyingName)
		{
			bool flag = false;
			bool flag2 = false;
			try
			{
				int num = 0;
				while (num < 2 && !flag2)
				{
					flag2 = this.m_performingADLookupLock.TryEnterUpgradeableReadLock(AdObjectLookupCache<TADWrapperObject>.CacheLockTimeout);
					num++;
				}
				if (flag2)
				{
					flag = this.m_performingADLookup.Contains(identifyingName);
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
							flag3 = this.m_performingADLookupLock.TryEnterWriteLock(AdObjectLookupCache<TADWrapperObject>.CacheLockTimeout);
							num2++;
						}
						if (flag3)
						{
							flag = this.m_performingADLookup.Contains(identifyingName);
							if (!flag)
							{
								this.m_performingADLookup.Add(identifyingName);
							}
						}
						else
						{
							ExAssert.RetailAssert(false, "Timeout waiting for write lock in AdObjectLookupCache.CheckAndSetADLock()");
						}
						goto IL_A6;
					}
					finally
					{
						if (flag3)
						{
							this.m_performingADLookupLock.ExitWriteLock();
						}
					}
				}
				ExAssert.RetailAssert(false, "Timeout waiting for upgradable read lock in AdObjectLookupCache.CheckAndSetADLock()");
				IL_A6:;
			}
			finally
			{
				if (flag2)
				{
					this.m_performingADLookupLock.ExitUpgradeableReadLock();
				}
			}
			return !flag;
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x000C49E4 File Offset: 0x000C2BE4
		internal void ReleaseADLock(string identifyingName)
		{
			bool flag = false;
			try
			{
				int num = 0;
				while (num < 2 && !flag)
				{
					flag = this.m_performingADLookupLock.TryEnterUpgradeableReadLock(AdObjectLookupCache<TADWrapperObject>.CacheLockTimeout);
					num++;
				}
				if (flag)
				{
					if (!this.m_performingADLookup.Contains(identifyingName))
					{
						goto IL_9C;
					}
					bool flag2 = false;
					try
					{
						int num2 = 0;
						while (num2 < 2 && !flag2)
						{
							flag2 = this.m_performingADLookupLock.TryEnterWriteLock(AdObjectLookupCache<TADWrapperObject>.CacheLockTimeout);
							num2++;
						}
						if (flag2)
						{
							if (this.m_performingADLookup.Contains(identifyingName))
							{
								this.m_performingADLookup.Remove(identifyingName);
							}
						}
						else
						{
							ExAssert.RetailAssert(false, "Timeout waiting for write lock in DatabaseLocationCache.ReleaseADLock()");
						}
						goto IL_9C;
					}
					finally
					{
						if (flag2)
						{
							this.m_performingADLookupLock.ExitWriteLock();
						}
					}
				}
				ExAssert.RetailAssert(false, "Timeout waiting for upgradable read lock in DatabaseLocationCache.ReleaseADLock()");
				IL_9C:;
			}
			finally
			{
				if (flag)
				{
					this.m_performingADLookupLock.ExitUpgradeableReadLock();
				}
			}
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000C4AE4 File Offset: 0x000C2CE4
		private TADWrapperObject LookupOrFindAdObject(string identifyingName, AdObjectLookupCache<TADWrapperObject>.FindAdObjectCacheFailure objectLookupFunction, AdObjectLookupFlags flags)
		{
			ExTraceGlobals.ActiveManagerClientTracer.TraceFunction<string>(0L, "LookupOrFindAdObject({0})", identifyingName);
			AdObjectCacheEntry<TADWrapperObject> adObjectCacheEntry = null;
			bool flag = false;
			bool flag2 = (flags & AdObjectLookupFlags.ReadThrough) != AdObjectLookupFlags.None;
			bool flag3 = false;
			try
			{
				int num = 0;
				while (num < 2 && !flag)
				{
					flag = this.m_rwLock.TryEnterReadLock(AdObjectLookupCache<TADWrapperObject>.CacheLockTimeout);
					num++;
				}
				if (flag)
				{
					flag3 = this.m_cache.TryGetValue(identifyingName, out adObjectCacheEntry);
					bool flag4 = false;
					if (flag3)
					{
						flag4 = AdObjectLookupCache<TADWrapperObject>.ShouldExpireCacheEntry(adObjectCacheEntry);
						ExTraceGlobals.ActiveManagerClientTracer.TraceDebug((long)this.GetHashCode(), "LookupOrFindAdObject( {0} ) was found in the cache: {1}, and shouldExpireEntry={2}, flags={3}", new object[]
						{
							identifyingName,
							adObjectCacheEntry,
							flag4,
							flags
						});
					}
					if (!flag3 || flag4)
					{
						flag2 = true;
					}
				}
				else
				{
					AdObjectLookupCache<TADWrapperObject>.Tracer.TraceError((long)this.GetHashCode(), "Timeout waiting for the read lock in AdObjectLookupCache.LookupOrFindAdObject()");
					flag2 = true;
				}
			}
			finally
			{
				if (flag)
				{
					this.m_rwLock.ExitReadLock();
				}
			}
			if (flag2)
			{
				bool flag5 = false;
				bool flag6 = false;
				TADWrapperObject updatedObject = default(TADWrapperObject);
				try
				{
					flag5 = this.CheckAndSetADLock(identifyingName);
					if (flag5)
					{
						InvokeWithTimeout.Invoke(delegate()
						{
							updatedObject = objectLookupFunction();
						}, this.m_adOperationTimeout);
					}
					else if (!flag3 || AdObjectLookupCache<TADWrapperObject>.MaximumTimeToLiveExpired(adObjectCacheEntry))
					{
						int num2 = 10;
						int num3 = 10;
						for (int i = 0; i < num3; i++)
						{
							Thread.Sleep(num2);
							bool flag7 = false;
							try
							{
								int num4 = 0;
								while (num4 < 2 && !flag7)
								{
									flag7 = this.m_rwLock.TryEnterReadLock(AdObjectLookupCache<TADWrapperObject>.CacheLockTimeout);
									num4++;
								}
								if (flag7)
								{
									flag3 = this.m_cache.TryGetValue(identifyingName, out adObjectCacheEntry);
								}
							}
							finally
							{
								if (flag7)
								{
									this.m_rwLock.ExitReadLock();
								}
							}
							if (flag3 && !AdObjectLookupCache<TADWrapperObject>.MaximumTimeToLiveExpired(adObjectCacheEntry))
							{
								if (AdObjectLookupCache<TADWrapperObject>.LogAdLatency)
								{
									StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_ActiveManagerClientAnotherThreadCompleted, identifyingName, new object[]
									{
										identifyingName,
										num2 * i
									});
								}
								AdObjectLookupCache<TADWrapperObject>.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Another thread finished doing query for object {0}.", identifyingName);
								flag6 = true;
								break;
							}
						}
						if (!flag3 || AdObjectLookupCache<TADWrapperObject>.MaximumTimeToLiveExpired(adObjectCacheEntry))
						{
							StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_ActiveManagerClientAnotherThreadInADCallTimeout, identifyingName, new object[]
							{
								identifyingName,
								num2 * num3
							});
							AdObjectLookupCache<TADWrapperObject>.Tracer.TraceDebug<string, int>((long)this.GetHashCode(), "Another thread doing query for object {0}, however this thread didn't complete in {1} msec.", identifyingName, num2 * num3);
							updatedObject = objectLookupFunction();
						}
					}
					else
					{
						if (AdObjectLookupCache<TADWrapperObject>.LogAdLatency)
						{
							StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_ActiveManagerClientAnotherThreadInADCall, identifyingName, new object[]
							{
								identifyingName
							});
						}
						AdObjectLookupCache<TADWrapperObject>.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Another thread doing query for object {0}.", identifyingName);
						flag6 = true;
					}
				}
				catch (TimeoutException ex)
				{
					StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_ErrorActiveManagerClientADTimeout, identifyingName, new object[]
					{
						identifyingName,
						this.m_adOperationTimeout
					});
					AdObjectLookupCache<TADWrapperObject>.Tracer.TraceError<string>((long)this.GetHashCode(), "Timeout on ad query: {0}", ex.Message);
					flag6 = true;
				}
				finally
				{
					if (flag5)
					{
						this.ReleaseADLock(identifyingName);
					}
				}
				if (updatedObject != null && this.MinimizeObjects)
				{
					updatedObject.Minimize();
				}
				adObjectCacheEntry = new AdObjectCacheEntry<TADWrapperObject>(updatedObject, this.m_timeToLive, this.m_timeToNegativeLive, this.m_maximumTimeToLive);
				bool flag8 = false;
				try
				{
					int num5 = 0;
					while (num5 < 2 && !flag8)
					{
						flag8 = this.m_rwLock.TryEnterWriteLock(AdObjectLookupCache<TADWrapperObject>.CacheLockTimeout);
						num5++;
					}
					if (flag8)
					{
						if (updatedObject == null && flag6)
						{
							AdObjectCacheEntry<TADWrapperObject> adObjectCacheEntry2 = null;
							flag3 = this.m_cache.TryGetValue(identifyingName, out adObjectCacheEntry2);
							if (flag3 && !AdObjectLookupCache<TADWrapperObject>.MaximumTimeToLiveExpired(adObjectCacheEntry2))
							{
								adObjectCacheEntry = adObjectCacheEntry2;
								ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<TADWrapperObject, AdObjectCacheEntry<TADWrapperObject>>((long)this.GetHashCode(), "New ad object was not found, but found possibly stale result '{0}' in the cache as {1}.", adObjectCacheEntry.AdObjectData, adObjectCacheEntry);
							}
							else
							{
								flag6 = false;
							}
						}
						if (!flag6)
						{
							this.m_cache[identifyingName] = adObjectCacheEntry;
							ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<TADWrapperObject, AdObjectCacheEntry<TADWrapperObject>>((long)this.GetHashCode(), "Stored object '{0}' in the cache as {1}.", adObjectCacheEntry.AdObjectData, adObjectCacheEntry);
						}
					}
					else
					{
						AdObjectLookupCache<TADWrapperObject>.Tracer.TraceError((long)this.GetHashCode(), "Timeout waiting for write lock in AdObjectLookupCache.LookupOrFindAdObject()");
					}
				}
				finally
				{
					if (flag8)
					{
						this.m_rwLock.ExitWriteLock();
					}
				}
			}
			return adObjectCacheEntry.AdObjectData;
		}

		// Token: 0x04001A11 RID: 6673
		private const string RegistryPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\HA\\AdObjectLookupCache";

		// Token: 0x04001A12 RID: 6674
		private const string RegistryLogAdLatency = "LogADLatency";

		// Token: 0x04001A13 RID: 6675
		private static bool? s_logAdLatency = null;

		// Token: 0x04001A14 RID: 6676
		private readonly TimeSpan m_cacheLockTimeout;

		// Token: 0x04001A15 RID: 6677
		private readonly TimeSpan m_adOperationTimeout;

		// Token: 0x04001A16 RID: 6678
		private readonly HashSet<string> m_performingADLookup = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04001A17 RID: 6679
		private readonly ReaderWriterLockSlim m_performingADLookupLock = new ReaderWriterLockSlim();

		// Token: 0x04001A18 RID: 6680
		internal static readonly TimeSpan CacheLockTimeout = TimeSpan.FromSeconds(60.0);

		// Token: 0x04001A19 RID: 6681
		internal static readonly TimeSpan AdOperationTimeout = TimeSpan.FromSeconds(60.0);

		// Token: 0x04001A1A RID: 6682
		internal static readonly TimeSpan TimeToLive = new TimeSpan(0, 5, 0);

		// Token: 0x04001A1B RID: 6683
		internal static readonly TimeSpan TimeToNegativeLive = new TimeSpan(0, 0, 30);

		// Token: 0x04001A1C RID: 6684
		internal static readonly TimeSpan MaximumTimeToLive = new TimeSpan(0, 10, 0);

		// Token: 0x04001A1D RID: 6685
		private readonly TimeSpan m_maximumTimeToLive;

		// Token: 0x04001A1E RID: 6686
		private readonly TimeSpan m_timeToLive;

		// Token: 0x04001A1F RID: 6687
		private readonly TimeSpan m_timeToNegativeLive;

		// Token: 0x04001A20 RID: 6688
		private Dictionary<string, AdObjectCacheEntry<TADWrapperObject>> m_cache = new Dictionary<string, AdObjectCacheEntry<TADWrapperObject>>(8, StringComparer.OrdinalIgnoreCase);

		// Token: 0x04001A21 RID: 6689
		[NonSerialized]
		private ReaderWriterLockSlim m_rwLock = new ReaderWriterLockSlim();

		// Token: 0x0200042E RID: 1070
		// (Invoke) Token: 0x06002FF4 RID: 12276
		internal delegate TADWrapperObject FindAdObjectCacheFailure();
	}
}
