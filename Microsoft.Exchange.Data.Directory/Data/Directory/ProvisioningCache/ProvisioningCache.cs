using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory.ProvisioningCache
{
	// Token: 0x020007A9 RID: 1961
	public class ProvisioningCache
	{
		// Token: 0x06006162 RID: 24930 RVA: 0x0014B9D5 File Offset: 0x00149BD5
		protected ProvisioningCache(string identification, bool enabled)
		{
			this.InitializeSettings(identification, enabled);
		}

		// Token: 0x06006163 RID: 24931 RVA: 0x0014B9F0 File Offset: 0x00149BF0
		protected ProvisioningCache()
		{
			this.CreateBasicCache();
		}

		// Token: 0x170022CD RID: 8909
		// (get) Token: 0x06006164 RID: 24932 RVA: 0x0014BA0C File Offset: 0x00149C0C
		public static ProvisioningCache Instance
		{
			get
			{
				if (!ProvisioningCache.appRegistryInitialized)
				{
					if (ProvisioningCache.disabledInstance == null)
					{
						lock (ProvisioningCache.disabledInstanceLock)
						{
							if (ProvisioningCache.disabledInstance == null)
							{
								ProvisioningCache.disabledInstance = new ProvisioningCache(null, false);
							}
						}
					}
					return ProvisioningCache.disabledInstance;
				}
				if (ProvisioningCache.cacheInstance == null)
				{
					lock (ProvisioningCache.cacheInstanceLock)
					{
						if (ProvisioningCache.cacheInstance == null)
						{
							ProvisioningCache.cacheInstance = new ProvisioningCache(ProvisioningCache.cacheIdentification, ProvisioningCache.configuredEnable);
						}
					}
				}
				return ProvisioningCache.cacheInstance;
			}
		}

		// Token: 0x170022CE RID: 8910
		// (get) Token: 0x06006165 RID: 24933 RVA: 0x0014BABC File Offset: 0x00149CBC
		private static MSExchangeProvisioningCacheInstance PerfCounters
		{
			get
			{
				return ProvisioningCache.perfCounters;
			}
		}

		// Token: 0x06006166 RID: 24934 RVA: 0x0014BAD0 File Offset: 0x00149CD0
		private static void IncreaseCacheHitCount()
		{
			ProvisioningCache.IncreaseCacheHitOrMissCount(CmdletThreadStaticData.CacheHitCount, delegate(int value)
			{
				CmdletThreadStaticData.CacheHitCount = new int?(value);
			}, RpsCmdletMetadata.CacheHitCount);
		}

		// Token: 0x06006167 RID: 24935 RVA: 0x0014BB0D File Offset: 0x00149D0D
		private static void IncreaseCacheMissCount()
		{
			ProvisioningCache.IncreaseCacheHitOrMissCount(CmdletThreadStaticData.CacheMissCount, delegate(int value)
			{
				CmdletThreadStaticData.CacheMissCount = new int?(value);
			}, RpsCmdletMetadata.CacheMissCount);
		}

		// Token: 0x06006168 RID: 24936 RVA: 0x0014BB40 File Offset: 0x00149D40
		private static void IncreaseCacheHitOrMissCount(int? valueGetter, Action<int> valueSetter, Enum logMetadata)
		{
			int? num = valueGetter;
			if (num == null)
			{
				return;
			}
			int num2 = num.Value + 1;
			valueSetter(num2);
			CmdletLogger.SafeSetLogger(logMetadata, num2);
		}

		// Token: 0x170022CF RID: 8911
		// (get) Token: 0x06006169 RID: 24937 RVA: 0x0014BB76 File Offset: 0x00149D76
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
		}

		// Token: 0x0600616A RID: 24938 RVA: 0x0014BB80 File Offset: 0x00149D80
		private static void GetAppRegistry(string identification, out Dictionary<Guid, TimeSpan> configuredExpirations, out bool enabled)
		{
			configuredExpirations = null;
			enabled = false;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(ProvisioningCache.MSExchangeProvisioningCacheRegistryPath))
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(ProvisioningCache.ProvisioningCacheExpiration))
					{
						if (registryKey2 != null)
						{
							string text = registryKey2.GetValue(identification) as string;
							if (!string.IsNullOrWhiteSpace(text))
							{
								configuredExpirations = ProvisioningCache.ConvertToExpirations(text);
							}
						}
					}
					object value = registryKey.GetValue(ProvisioningCache.ProvisioningCacheEnabled);
					enabled = (value == null || StringComparer.OrdinalIgnoreCase.Equals("true", value.ToString()));
				}
			}
			catch (ArgumentNullException ex)
			{
				ProvisioningCache.ProvisioningCacheInitializationFailedHandler(identification, ex.Message);
			}
			catch (ObjectDisposedException ex2)
			{
				ProvisioningCache.ProvisioningCacheInitializationFailedHandler(identification, ex2.Message);
			}
			catch (SecurityException ex3)
			{
				ProvisioningCache.ProvisioningCacheInitializationFailedHandler(identification, ex3.Message);
			}
			catch (ArgumentException ex4)
			{
				ProvisioningCache.ProvisioningCacheInitializationFailedHandler(identification, ex4.Message);
			}
			catch (Exception ex5)
			{
				ProvisioningCache.ProvisioningCacheInitializationFailedHandler(identification, ex5.Message);
			}
		}

		// Token: 0x0600616B RID: 24939 RVA: 0x0014BCC0 File Offset: 0x00149EC0
		private static void ProvisioningCacheInitializationFailedHandler(string identification, string errorMsg)
		{
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCProvisioningCacheInitializationFailed, identification, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x0600616C RID: 24940 RVA: 0x0014BCE8 File Offset: 0x00149EE8
		public static void InitializeAppRegistrySettings(string identification)
		{
			if (ProvisioningCache.appRegistryInitialized)
			{
				return;
			}
			Dictionary<Guid, TimeSpan> dictionary;
			bool flag;
			ProvisioningCache.GetAppRegistry(identification, out dictionary, out flag);
			if (flag && !string.IsNullOrEmpty(identification))
			{
				Dictionary<Guid, TimeSpan> dictionary2 = new Dictionary<Guid, TimeSpan>();
				if (dictionary != null && dictionary.Count != 0)
				{
					foreach (KeyValuePair<Guid, TimeSpan> keyValuePair in dictionary)
					{
						Guid key = keyValuePair.Key;
						if (CannedProvisioningExpirationTime.AllCannedProvisioningCacheKeys.Contains(key))
						{
							TimeSpan value = keyValuePair.Value;
							dictionary2.Add(key, value);
						}
					}
				}
				ProvisioningCache.InitializeAppRegistrySettings(identification, flag, dictionary2);
			}
		}

		// Token: 0x0600616D RID: 24941 RVA: 0x0014BD90 File Offset: 0x00149F90
		private static Dictionary<Guid, TimeSpan> ConvertToExpirations(string expirationString)
		{
			Dictionary<Guid, TimeSpan> result;
			try
			{
				Dictionary<Guid, TimeSpan> dictionary = new Dictionary<Guid, TimeSpan>();
				string[] array = expirationString.Split(new char[]
				{
					';'
				});
				foreach (string text in array)
				{
					if (!string.IsNullOrWhiteSpace(text))
					{
						string[] array3 = text.Split(new char[]
						{
							','
						});
						Guid key;
						try
						{
							key = Guid.Parse(array3[0]);
						}
						catch (Exception ex)
						{
							throw new ArgumentException(ex.Message);
						}
						TimeSpan value;
						try
						{
							value = TimeSpan.Parse(array3[1]);
						}
						catch (Exception ex2)
						{
							throw new ArgumentException(ex2.Message);
						}
						dictionary.Add(key, value);
					}
				}
				result = dictionary;
			}
			catch (Exception ex3)
			{
				throw new ArgumentException(ex3.Message);
			}
			return result;
		}

		// Token: 0x0600616E RID: 24942 RVA: 0x0014BE74 File Offset: 0x0014A074
		private static void InitializeAppRegistrySettings(string identification, bool enabled, Dictionary<Guid, TimeSpan> configuredExpirations)
		{
			if (ProvisioningCache.appRegistryInitialized)
			{
				return;
			}
			lock (ProvisioningCache.appRegistryInitializedLockObj)
			{
				if (!ProvisioningCache.appRegistryInitialized)
				{
					ProvisioningCache.cacheIdentification = identification;
					ProvisioningCache.configuredEnable = enabled;
					if (configuredExpirations != null)
					{
						ProvisioningCache.configuredExpirationTime = new Dictionary<Guid, TimeSpan>(configuredExpirations);
					}
					ProvisioningCache.appRegistryInitialized = true;
				}
			}
		}

		// Token: 0x0600616F RID: 24943 RVA: 0x0014BEE0 File Offset: 0x0014A0E0
		public static void InvalidCacheEntry(object changedObject)
		{
			IProvisioningCacheInvalidation provisioningCacheInvalidation = changedObject as IProvisioningCacheInvalidation;
			if (provisioningCacheInvalidation == null)
			{
				return;
			}
			OrganizationId organizationId = null;
			Guid[] array = null;
			if (provisioningCacheInvalidation.ShouldInvalidProvisioningCache(out organizationId, out array) && array != null && array.Length > 0)
			{
				if (InvalidationMessage.IsGlobalCacheEntry(array))
				{
					ProvisioningCache.Instance.RemoveGlobalDatas(array);
				}
				else if (OrganizationId.ForestWideOrgId.Equals(organizationId))
				{
					ProvisioningCache.Instance.RemoveOrganizationDatas(Guid.Empty, array);
				}
				else
				{
					ProvisioningCache.Instance.RemoveOrganizationDatas(organizationId.ConfigurationUnit.ObjectGuid, array);
				}
				ProvisioningCache.Instance.BroadcastInvalidationMessage(organizationId, array);
			}
		}

		// Token: 0x06006170 RID: 24944 RVA: 0x0014BF67 File Offset: 0x0014A167
		private static void InitializePerfCounters()
		{
			ProvisioningCache.perfCounters = MSExchangeProvisioningCache.GetInstance(ProvisioningCache.cacheIdentification);
		}

		// Token: 0x06006171 RID: 24945 RVA: 0x0014BF78 File Offset: 0x0014A178
		public T TryAddAndGetGlobalData<T>(Guid key, ProvisioningCache.CacheObjectGetterDelegate getter)
		{
			if (getter == null)
			{
				throw new ArgumentNullException("getter");
			}
			if (!this.enabled)
			{
				return (T)((object)getter());
			}
			return this.TryAddAndGetGlobalData<T>(key, this.GetExpirationTime(key), getter);
		}

		// Token: 0x06006172 RID: 24946 RVA: 0x0014BFAC File Offset: 0x0014A1AC
		public T TryAddAndGetGlobalData<T>(Guid key, TimeSpan expirationTime, ProvisioningCache.CacheObjectGetterDelegate getter)
		{
			if (getter == null)
			{
				throw new ArgumentNullException("getter");
			}
			if (!this.enabled)
			{
				return (T)((object)getter());
			}
			if (expirationTime < TimeSpan.Zero)
			{
				throw new ArgumentException("expirationTime");
			}
			if (!this.globalLocks.ContainsKey(key))
			{
				throw new ArgumentException("The key is not a valid global entry cache key.");
			}
			T result = default(T);
			if (this.TryEnterReadLock(this.globalLocks[key], key, Guid.Empty))
			{
				bool flag = true;
				try
				{
					if (!this.globalData.ContainsKey(key) || ProvisioningCache.NeedWriteLockForCachedObject<T>(this.globalData[key], out result))
					{
						this.globalLocks[key].ExitReadLock();
						flag = false;
						ProvisioningCache.IncrementGlobalMissesCounter();
						if (this.TryEnterWriteLock(this.globalLocks[key], key, Guid.Empty))
						{
							try
							{
								if (!this.globalData.ContainsKey(key))
								{
									this.globalData[key] = new ExpiringCacheObject(expirationTime);
									ProvisioningCache.IncrementByTotalCachedObjectNum(1L);
								}
								result = ProvisioningCache.RetrieveData<T>(key, this.globalData[key], getter);
								goto IL_130;
							}
							finally
							{
								this.globalLocks[key].ExitWriteLock();
							}
						}
						return ProvisioningCache.CallGetterWithTrace<T>(key, getter);
					}
					ProvisioningCache.IncrementGlobalHitsCounter();
					IL_130:
					return result;
				}
				finally
				{
					if (flag)
					{
						this.globalLocks[key].ExitReadLock();
					}
				}
			}
			ProvisioningCache.IncrementGlobalMissesCounter();
			return ProvisioningCache.CallGetterWithTrace<T>(key, getter);
		}

		// Token: 0x06006173 RID: 24947 RVA: 0x0014C12C File Offset: 0x0014A32C
		public T TryAddAndGetGlobalDictionaryValue<T, K>(Guid key, K subKey, ProvisioningCache.CacheObjectGetterDelegate getter)
		{
			if (getter == null)
			{
				throw new ArgumentNullException("getter");
			}
			if (!this.enabled)
			{
				return (T)((object)getter());
			}
			return this.TryAddAndGetGlobalDictionaryValue<T, K>(key, subKey, this.GetExpirationTime(key), getter);
		}

		// Token: 0x06006174 RID: 24948 RVA: 0x0014C160 File Offset: 0x0014A360
		public T TryAddAndGetGlobalDictionaryValue<T, K>(Guid key, K subKey, TimeSpan expirationTime, ProvisioningCache.CacheObjectGetterDelegate getter)
		{
			if (getter == null)
			{
				throw new ArgumentNullException("getter");
			}
			if (!this.enabled)
			{
				return (T)((object)getter());
			}
			if (subKey == null)
			{
				throw new ArgumentNullException("subKey");
			}
			if (expirationTime < TimeSpan.Zero)
			{
				throw new ArgumentException("expirationTime");
			}
			if (!this.globalLocks.ContainsKey(key))
			{
				throw new ArgumentException("The key is not a valid global entry cache key.");
			}
			T result = default(T);
			if (this.TryEnterReadLock(this.globalLocks[key], key, Guid.Empty))
			{
				bool flag = true;
				try
				{
					if (!this.globalData.ContainsKey(key) || ProvisioningCache.NeedWriteLockForCachedDictionary<K, T>(this.globalData[key], subKey, out result))
					{
						this.globalLocks[key].ExitReadLock();
						flag = false;
						ProvisioningCache.IncrementGlobalMissesCounter();
						if (this.TryEnterWriteLock(this.globalLocks[key], key, Guid.Empty))
						{
							try
							{
								if (!this.globalData.ContainsKey(key))
								{
									this.globalData[key] = new ExpiringCacheObject(expirationTime, new Dictionary<K, T>());
								}
								result = ProvisioningCache.RetrieveDataFromDictionary<T, K>(key, subKey, this.globalData[key], getter);
								goto IL_148;
							}
							finally
							{
								this.globalLocks[key].ExitWriteLock();
							}
						}
						return ProvisioningCache.CallGetterWithTrace<T, K>(key, subKey, getter);
					}
					ProvisioningCache.IncrementGlobalHitsCounter();
					IL_148:
					return result;
				}
				finally
				{
					if (flag)
					{
						this.globalLocks[key].ExitReadLock();
					}
				}
			}
			ProvisioningCache.IncrementGlobalMissesCounter();
			return ProvisioningCache.CallGetterWithTrace<T, K>(key, subKey, getter);
		}

		// Token: 0x06006175 RID: 24949 RVA: 0x0014C2FC File Offset: 0x0014A4FC
		public T TryAddAndGetOrganizationData<T>(Guid key, OrganizationId organizationId, ProvisioningCache.CacheObjectGetterDelegate getter)
		{
			if (getter == null)
			{
				throw new ArgumentNullException("getter");
			}
			if (!this.enabled)
			{
				return (T)((object)getter());
			}
			return this.TryAddAndGetOrganizationData<T>(key, organizationId, this.GetExpirationTime(key), getter);
		}

		// Token: 0x06006176 RID: 24950 RVA: 0x0014C330 File Offset: 0x0014A530
		public T TryAddAndGetOrganizationData<T>(Guid key, OrganizationId organizationId, TimeSpan expirationTime, ProvisioningCache.CacheObjectGetterDelegate getter)
		{
			if (getter == null)
			{
				throw new ArgumentNullException("getter");
			}
			if (!this.enabled)
			{
				return (T)((object)getter());
			}
			if (key == Guid.Empty)
			{
				throw new ArgumentException("key");
			}
			if (expirationTime < TimeSpan.Zero)
			{
				throw new ArgumentException("expirationTime");
			}
			T result = default(T);
			Guid guid;
			if (organizationId == null || organizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				guid = Guid.Empty;
			}
			else
			{
				guid = organizationId.ConfigurationUnit.ObjectGuid;
				if (guid.Equals(Guid.Empty))
				{
					return (T)((object)getter());
				}
			}
			ReaderWriterLockSlim readerWriterLockSlim = this.RetrieveOrganizationLock(guid);
			if (readerWriterLockSlim == null)
			{
				ProvisioningCache.IncrementOrganizationMissesCounter();
				return ProvisioningCache.CallGetterWithTrace<T>(key, getter);
			}
			if (this.TryEnterReadLock(readerWriterLockSlim, key, guid))
			{
				bool flag = true;
				try
				{
					Dictionary<Guid, ExpiringCacheObject> dictionary = this.organizationData[guid];
					if (ProvisioningCache.NeedWriteLockForCachedOrgData<T>(key, dictionary, out result))
					{
						readerWriterLockSlim.ExitReadLock();
						flag = false;
						ProvisioningCache.IncrementOrganizationMissesCounter();
						if (this.TryEnterWriteLock(readerWriterLockSlim, key, guid))
						{
							try
							{
								if (!dictionary.ContainsKey(key))
								{
									dictionary[key] = new ExpiringCacheObject(expirationTime);
									ProvisioningCache.IncrementByTotalCachedObjectNum(1L);
								}
								result = ProvisioningCache.RetrieveData<T>(key, dictionary[key], getter);
								goto IL_146;
							}
							finally
							{
								readerWriterLockSlim.ExitWriteLock();
							}
						}
						return ProvisioningCache.CallGetterWithTrace<T>(key, getter);
					}
					ProvisioningCache.IncrementOrganizationHitsCounter();
					IL_146:
					return result;
				}
				finally
				{
					if (flag)
					{
						readerWriterLockSlim.ExitReadLock();
					}
				}
			}
			ProvisioningCache.IncrementOrganizationMissesCounter();
			return ProvisioningCache.CallGetterWithTrace<T>(key, getter);
		}

		// Token: 0x06006177 RID: 24951 RVA: 0x0014C4C0 File Offset: 0x0014A6C0
		public T TryAddAndGetOrganizationDictionaryValue<T, K>(Guid key, OrganizationId organizationId, K subKey, ProvisioningCache.CacheObjectGetterDelegate getter)
		{
			if (getter == null)
			{
				throw new ArgumentNullException("getter");
			}
			if (!this.enabled)
			{
				return (T)((object)getter());
			}
			return this.TryAddAndGetOrganizationDictionaryValue<T, K>(key, organizationId, subKey, this.GetExpirationTime(key), getter);
		}

		// Token: 0x06006178 RID: 24952 RVA: 0x0014C4F8 File Offset: 0x0014A6F8
		public T TryAddAndGetOrganizationDictionaryValue<T, K>(Guid key, OrganizationId organizationId, K subKey, TimeSpan expirationTime, ProvisioningCache.CacheObjectGetterDelegate getter)
		{
			if (getter == null)
			{
				throw new ArgumentNullException("getter");
			}
			if (!this.enabled)
			{
				return (T)((object)getter());
			}
			if (key == Guid.Empty)
			{
				throw new ArgumentException("key");
			}
			if (subKey == null)
			{
				throw new ArgumentNullException("subKey");
			}
			if (expirationTime < TimeSpan.Zero)
			{
				throw new ArgumentException("expirationTime");
			}
			Guid guid;
			if (organizationId == null || organizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				guid = Guid.Empty;
			}
			else
			{
				guid = organizationId.ConfigurationUnit.ObjectGuid;
				if (guid.Equals(Guid.Empty))
				{
					return (T)((object)getter());
				}
			}
			ReaderWriterLockSlim readerWriterLockSlim = this.RetrieveOrganizationLock(guid);
			if (readerWriterLockSlim == null)
			{
				ProvisioningCache.IncrementOrganizationMissesCounter();
				return (T)((object)getter());
			}
			T result = default(T);
			if (this.TryEnterReadLock(readerWriterLockSlim, key, guid))
			{
				bool flag = true;
				try
				{
					Dictionary<Guid, ExpiringCacheObject> dictionary = this.organizationData[guid];
					if (ProvisioningCache.NeedWriteLockForCachedOrgDictionary<K, T>(key, dictionary, subKey, out result))
					{
						readerWriterLockSlim.ExitReadLock();
						flag = false;
						ProvisioningCache.IncrementOrganizationMissesCounter();
						if (this.TryEnterWriteLock(readerWriterLockSlim, key, guid))
						{
							try
							{
								if (!dictionary.ContainsKey(key))
								{
									dictionary[key] = new ExpiringCacheObject(expirationTime, new Dictionary<K, T>());
								}
								result = ProvisioningCache.RetrieveDataFromDictionary<T, K>(key, subKey, dictionary[key], getter);
								goto IL_160;
							}
							finally
							{
								readerWriterLockSlim.ExitWriteLock();
							}
						}
						return ProvisioningCache.CallGetterWithTrace<T, K>(key, subKey, getter);
					}
					ProvisioningCache.IncrementOrganizationHitsCounter();
					IL_160:
					return result;
				}
				finally
				{
					if (flag)
					{
						readerWriterLockSlim.ExitReadLock();
					}
				}
			}
			ProvisioningCache.IncrementOrganizationMissesCounter();
			return ProvisioningCache.CallGetterWithTrace<T, K>(key, subKey, getter);
		}

		// Token: 0x06006179 RID: 24953 RVA: 0x0014C6A0 File Offset: 0x0014A8A0
		public void RemoveGlobalData(Guid key)
		{
			if (!this.enabled)
			{
				return;
			}
			if (!this.globalLocks.ContainsKey(key))
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCBadGlobalCacheKeyReceived, key.ToString(), new object[]
				{
					key.ToString()
				});
				return;
			}
			try
			{
				if (this.TryEnterEntryRemovalWriteLock(this.globalLocks[key], key, Guid.Empty) && this.globalData.ContainsKey(key))
				{
					int num = -ProvisioningCache.GetActualObjectNum(this.globalData[key].Data);
					this.globalData.Remove(key);
					ProvisioningCache.IncrementByTotalCachedObjectNum((long)num);
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCGlobalDataInvalidated, key.ToString(), new object[]
					{
						key.ToString(),
						ProvisioningCache.cacheIdentification
					});
				}
			}
			finally
			{
				try
				{
					this.globalLocks[key].ExitWriteLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
		}

		// Token: 0x0600617A RID: 24954 RVA: 0x0014C7B8 File Offset: 0x0014A9B8
		public void RemoveGlobalDatas(ICollection<Guid> keys)
		{
			if (!this.enabled)
			{
				return;
			}
			if (keys == null)
			{
				return;
			}
			if (keys.Count == 0)
			{
				this.ResetGlobalData();
				return;
			}
			foreach (Guid key in keys)
			{
				this.RemoveGlobalData(key);
			}
		}

		// Token: 0x0600617B RID: 24955 RVA: 0x0014C81C File Offset: 0x0014AA1C
		public void RemoveOrganizationData(OrganizationId organizationId, Guid key)
		{
			Guid orgId;
			if (organizationId == null || organizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				orgId = Guid.Empty;
			}
			else
			{
				orgId = organizationId.ConfigurationUnit.ObjectGuid;
			}
			this.RemoveOrganizationData(orgId, key);
		}

		// Token: 0x0600617C RID: 24956 RVA: 0x0014C85C File Offset: 0x0014AA5C
		public void RemoveOrganizationData(Guid orgId, Guid key)
		{
			if (!this.enabled)
			{
				return;
			}
			ReaderWriterLockSlim readerWriterLockSlim = this.RetrieveOrganizationLock(orgId);
			if (readerWriterLockSlim == null)
			{
				return;
			}
			try
			{
				if (this.TryEnterEntryRemovalWriteLock(readerWriterLockSlim, key, orgId))
				{
					Dictionary<Guid, ExpiringCacheObject> dictionary = this.organizationData[orgId];
					if (dictionary.ContainsKey(key) && dictionary[key] != null && !dictionary[key].IsExpired)
					{
						int num = -ProvisioningCache.GetActualObjectNum(dictionary[key]);
						dictionary.Remove(key);
						ProvisioningCache.IncrementByTotalCachedObjectNum((long)num);
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCOrganizationDataInvalidated, key.ToString(), new object[]
						{
							key.ToString(),
							orgId.ToString(),
							ProvisioningCache.cacheIdentification
						});
					}
				}
			}
			finally
			{
				try
				{
					readerWriterLockSlim.ExitWriteLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
		}

		// Token: 0x0600617D RID: 24957 RVA: 0x0014C948 File Offset: 0x0014AB48
		public void RemoveOrganizationDatas(Guid orgId, ICollection<Guid> keys)
		{
			if (!this.enabled)
			{
				return;
			}
			ReaderWriterLockSlim readerWriterLockSlim = this.RetrieveOrganizationLock(orgId);
			if (readerWriterLockSlim == null)
			{
				return;
			}
			try
			{
				if (this.TryEnterEntryRemovalWriteLock(readerWriterLockSlim, Guid.Empty, orgId))
				{
					Dictionary<Guid, ExpiringCacheObject> dictionary = this.organizationData[orgId];
					if (keys.Count == 0)
					{
						int num = 0;
						foreach (object obj in this.organizationData[orgId].Values)
						{
							num -= ProvisioningCache.GetActualObjectNum(obj);
						}
						this.organizationData[orgId].Clear();
						ProvisioningCache.IncrementByTotalCachedObjectNum((long)num);
					}
					else
					{
						foreach (Guid key in keys)
						{
							if (dictionary.ContainsKey(key) && dictionary[key] != null && !dictionary[key].IsExpired)
							{
								int num2 = -ProvisioningCache.GetActualObjectNum(dictionary[key]);
								dictionary.Remove(key);
								ProvisioningCache.IncrementByTotalCachedObjectNum((long)num2);
								Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCOrganizationDataInvalidated, key.ToString(), new object[]
								{
									key.ToString(),
									orgId.ToString(),
									ProvisioningCache.cacheIdentification
								});
							}
						}
					}
				}
			}
			finally
			{
				try
				{
					readerWriterLockSlim.ExitWriteLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
		}

		// Token: 0x0600617E RID: 24958 RVA: 0x0014CB2C File Offset: 0x0014AD2C
		internal void ResetGlobalData()
		{
			if (!this.enabled)
			{
				return;
			}
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCResettingGlobalData, ProvisioningCache.cacheIdentification, new object[]
			{
				ProvisioningCache.cacheIdentification
			});
			foreach (Guid key in this.globalLocks.Keys)
			{
				this.RemoveGlobalData(key);
			}
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCResettingGlobalDataFinished, ProvisioningCache.cacheIdentification, new object[]
			{
				ProvisioningCache.cacheIdentification
			});
		}

		// Token: 0x0600617F RID: 24959 RVA: 0x0014CBD0 File Offset: 0x0014ADD0
		internal void ResetOrganizationData()
		{
			if (!this.enabled)
			{
				return;
			}
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCResettingOrganizationData, ProvisioningCache.cacheIdentification, new object[]
			{
				ProvisioningCache.cacheIdentification
			});
			Guid[] array = null;
			try
			{
				if (this.TryEnterEntryRemovalReadLock(this.organizationTableLock))
				{
					array = new Guid[this.organizationLocks.Count];
					this.organizationLocks.Keys.CopyTo(array, 0);
				}
			}
			finally
			{
				try
				{
					this.organizationTableLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			if (array != null && array.Length > 0)
			{
				foreach (Guid guid in array)
				{
					try
					{
						if (this.TryEnterEntryRemovalWriteLock(this.organizationLocks[guid], Guid.Empty, guid))
						{
							int num = 0;
							foreach (object obj in this.organizationData[guid].Values)
							{
								num -= ProvisioningCache.GetActualObjectNum(obj);
							}
							this.organizationData[guid].Clear();
							ProvisioningCache.IncrementByTotalCachedObjectNum((long)num);
						}
					}
					finally
					{
						try
						{
							this.organizationLocks[guid].ExitWriteLock();
						}
						catch (SynchronizationLockException)
						{
						}
					}
				}
			}
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCResettingOrganizationDataFinished, ProvisioningCache.cacheIdentification, new object[]
			{
				ProvisioningCache.cacheIdentification
			});
		}

		// Token: 0x06006180 RID: 24960 RVA: 0x0014CD80 File Offset: 0x0014AF80
		internal IEnumerable<CachedEntryObject> DumpCachedEntries(ICollection<Guid> orgs, ICollection<Guid> keys)
		{
			if (orgs == null)
			{
				return this.DumpGlobalCachedEntries(keys);
			}
			return this.DumpOrganizationCachedEntries(orgs, keys);
		}

		// Token: 0x06006181 RID: 24961 RVA: 0x0014CD98 File Offset: 0x0014AF98
		internal void BroadcastInvalidationMessage(OrganizationId orgId, Guid[] keys)
		{
			if (this.broadcaster == null)
			{
				lock (this.cacheBroadcasterLock)
				{
					if (this.broadcaster == null)
					{
						this.broadcaster = new CacheBroadcaster(9050U);
					}
				}
			}
			this.broadcaster.BroadcastInvalidationMessage(orgId, keys);
		}

		// Token: 0x06006182 RID: 24962 RVA: 0x0014CE00 File Offset: 0x0014B000
		internal void ClearExpireOrganizations()
		{
			if (!this.enabled)
			{
				return;
			}
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCClearingExpiredOrganizations, ProvisioningCache.cacheIdentification, new object[]
			{
				ProvisioningCache.cacheIdentification
			});
			Guid[] array = null;
			int num = 0;
			try
			{
				if (this.TryEnterEntryRemovalReadLock(this.organizationTableLock))
				{
					array = new Guid[this.organizationLocks.Count];
					this.organizationLocks.Keys.CopyTo(array, 0);
				}
			}
			finally
			{
				try
				{
					this.organizationTableLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			if (array != null && array.Length > 0)
			{
				foreach (Guid guid in array)
				{
					try
					{
						if (this.TryEnterEntryRemovalWriteLock(this.organizationLocks[guid], Guid.Empty, guid))
						{
							Dictionary<Guid, ExpiringCacheObject> dictionary = this.organizationData[guid];
							bool flag = true;
							foreach (Guid key in dictionary.Keys)
							{
								if (!dictionary[key].IsExpired)
								{
									flag = false;
									break;
								}
							}
							if (flag)
							{
								dictionary.Clear();
								num++;
							}
						}
					}
					finally
					{
						try
						{
							this.organizationLocks[guid].ExitWriteLock();
						}
						catch (SynchronizationLockException)
						{
						}
					}
				}
			}
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCClearingExpiredOrganizationsFinished, ProvisioningCache.cacheIdentification, new object[]
			{
				ProvisioningCache.cacheIdentification,
				num
			});
		}

		// Token: 0x06006183 RID: 24963 RVA: 0x0014CFC0 File Offset: 0x0014B1C0
		internal void Reset()
		{
			if (this.enabled)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCResettingWholeProvisioningCache, ProvisioningCache.cacheIdentification, new object[]
				{
					ProvisioningCache.cacheIdentification
				});
				this.ResetGlobalData();
				this.ResetOrganizationData();
			}
		}

		// Token: 0x06006184 RID: 24964 RVA: 0x0014D004 File Offset: 0x0014B204
		internal void StopProvisioningCacheActivities()
		{
			if (!this.Enabled)
			{
				return;
			}
			if (this.invalidationActivity != null)
			{
				this.invalidationActivity.StopExecute();
			}
			if (this.cleanerActivity != null)
			{
				this.cleanerActivity.StopExecute();
			}
			if (this.diagnosticActivity != null)
			{
				this.diagnosticActivity.StopExecute();
			}
		}

		// Token: 0x06006185 RID: 24965 RVA: 0x0014D053 File Offset: 0x0014B253
		private static bool NeedWriteLockForCachedObject<T>(ExpiringCacheObject entry, out T result)
		{
			result = default(T);
			if (entry == null || entry.IsExpired)
			{
				return true;
			}
			result = (T)((object)entry.Data);
			return false;
		}

		// Token: 0x06006186 RID: 24966 RVA: 0x0014D07C File Offset: 0x0014B27C
		private static bool NeedWriteLockForCachedDictionary<K, T>(ExpiringCacheObject entry, K subKey, out T result)
		{
			result = default(T);
			if (entry == null || entry.IsExpired)
			{
				return true;
			}
			Dictionary<K, T> dictionary = (Dictionary<K, T>)entry.Data;
			if (dictionary == null || !dictionary.ContainsKey(subKey) || dictionary[subKey] == null)
			{
				return true;
			}
			result = dictionary[subKey];
			return false;
		}

		// Token: 0x06006187 RID: 24967 RVA: 0x0014D0D3 File Offset: 0x0014B2D3
		private static bool NeedWriteLockForCachedOrgData<T>(Guid key, Dictionary<Guid, ExpiringCacheObject> orgEntry, out T result)
		{
			result = default(T);
			return !orgEntry.ContainsKey(key) || orgEntry[key] == null || ProvisioningCache.NeedWriteLockForCachedObject<T>(orgEntry[key], out result);
		}

		// Token: 0x06006188 RID: 24968 RVA: 0x0014D0FD File Offset: 0x0014B2FD
		private static bool NeedWriteLockForCachedOrgDictionary<K, T>(Guid key, Dictionary<Guid, ExpiringCacheObject> orgEntry, K subKey, out T result)
		{
			result = default(T);
			return !orgEntry.ContainsKey(key) || orgEntry[key] == null || ProvisioningCache.NeedWriteLockForCachedDictionary<K, T>(orgEntry[key], subKey, out result);
		}

		// Token: 0x06006189 RID: 24969 RVA: 0x0014D128 File Offset: 0x0014B328
		private static T AddToDictionary<T, K>(Guid key, Dictionary<K, T> dict, K subKey, ProvisioningCache.CacheObjectGetterDelegate getter)
		{
			T t;
			if (!dict.ContainsKey(subKey) || dict[subKey] == null)
			{
				t = ProvisioningCache.CallGetterWithTrace<T, K>(key, subKey, getter);
				dict[subKey] = t;
				ProvisioningCache.IncrementByTotalCachedObjectNum(1L);
			}
			else
			{
				t = dict[subKey];
			}
			return t;
		}

		// Token: 0x0600618A RID: 24970 RVA: 0x0014D170 File Offset: 0x0014B370
		private static T CallGetterWithTrace<T>(Guid key, ProvisioningCache.CacheObjectGetterDelegate getter)
		{
			return (T)((object)getter());
		}

		// Token: 0x0600618B RID: 24971 RVA: 0x0014D18C File Offset: 0x0014B38C
		private static T CallGetterWithTrace<T, K>(Guid key, K subKey, ProvisioningCache.CacheObjectGetterDelegate getter)
		{
			return (T)((object)getter());
		}

		// Token: 0x0600618C RID: 24972 RVA: 0x0014D1A6 File Offset: 0x0014B3A6
		private static T RetrieveData<T>(Guid key, ExpiringCacheObject entry, ProvisioningCache.CacheObjectGetterDelegate getter)
		{
			if (entry.IsExpired)
			{
				entry.Data = ProvisioningCache.CallGetterWithTrace<T>(key, getter);
			}
			return (T)((object)entry.Data);
		}

		// Token: 0x0600618D RID: 24973 RVA: 0x0014D1D0 File Offset: 0x0014B3D0
		private static T RetrieveDataFromDictionary<T, K>(Guid key, K subKey, ExpiringCacheObject entry, ProvisioningCache.CacheObjectGetterDelegate getter)
		{
			if (entry.IsExpired)
			{
				int num = -ProvisioningCache.GetActualObjectNum(entry.Data);
				entry.Data = new Dictionary<K, T>();
				ProvisioningCache.IncrementByTotalCachedObjectNum((long)num);
			}
			Dictionary<K, T> dict = (Dictionary<K, T>)entry.Data;
			return ProvisioningCache.AddToDictionary<T, K>(key, dict, subKey, getter);
		}

		// Token: 0x0600618E RID: 24974 RVA: 0x0014D219 File Offset: 0x0014B419
		private static void IncrementGlobalHitsCounter()
		{
			if (LoggerSettings.LogEnabled)
			{
				ProvisioningCache.IncreaseCacheHitCount();
			}
			if (ProvisioningCache.PerfCounters != null)
			{
				ProvisioningCache.PerfCounters.GlobalAggregateHits.Increment();
			}
		}

		// Token: 0x0600618F RID: 24975 RVA: 0x0014D23E File Offset: 0x0014B43E
		private static void IncrementGlobalMissesCounter()
		{
			if (LoggerSettings.LogEnabled)
			{
				ProvisioningCache.IncreaseCacheMissCount();
			}
			if (ProvisioningCache.PerfCounters != null)
			{
				ProvisioningCache.PerfCounters.GlobalAggregateMisses.Increment();
			}
		}

		// Token: 0x06006190 RID: 24976 RVA: 0x0014D263 File Offset: 0x0014B463
		private static void IncrementOrganizationHitsCounter()
		{
			if (LoggerSettings.LogEnabled)
			{
				ProvisioningCache.IncreaseCacheHitCount();
			}
			if (ProvisioningCache.PerfCounters != null)
			{
				ProvisioningCache.PerfCounters.OrganizationAggregateHits.Increment();
			}
		}

		// Token: 0x06006191 RID: 24977 RVA: 0x0014D288 File Offset: 0x0014B488
		private static void IncrementOrganizationMissesCounter()
		{
			if (LoggerSettings.LogEnabled)
			{
				ProvisioningCache.IncreaseCacheMissCount();
			}
			if (ProvisioningCache.PerfCounters != null)
			{
				ProvisioningCache.PerfCounters.OrganizationAggregateMisses.Increment();
			}
		}

		// Token: 0x06006192 RID: 24978 RVA: 0x0014D2AD File Offset: 0x0014B4AD
		internal static void IncrementReceivedInvalidationMsgNum()
		{
			if (ProvisioningCache.PerfCounters != null)
			{
				ProvisioningCache.PerfCounters.ReceivedInvalidationMsgNum.Increment();
			}
		}

		// Token: 0x06006193 RID: 24979 RVA: 0x0014D2C6 File Offset: 0x0014B4C6
		private static void IncrementByTotalCachedObjectNum(long increment)
		{
			if (ProvisioningCache.PerfCounters != null)
			{
				ProvisioningCache.PerfCounters.TotalCachedObjectNum.IncrementBy(increment);
			}
		}

		// Token: 0x06006194 RID: 24980 RVA: 0x0014D2E0 File Offset: 0x0014B4E0
		private static int GetActualObjectNum(object obj)
		{
			int result = 1;
			IDictionary dictionary = obj as IDictionary;
			if (dictionary != null)
			{
				result = dictionary.Count;
			}
			return result;
		}

		// Token: 0x06006195 RID: 24981 RVA: 0x0014D304 File Offset: 0x0014B504
		private void InitializeSettings(string identification, bool enabled)
		{
			if (enabled && (string.Equals(identification, "Powershell", StringComparison.OrdinalIgnoreCase) || string.Equals(identification, "Powershell-LiveId", StringComparison.OrdinalIgnoreCase) || string.Equals(identification, "Powershell-Proxy", StringComparison.OrdinalIgnoreCase) || string.Equals(identification, "PowershellLiveId-Proxy", StringComparison.OrdinalIgnoreCase) || string.Equals(identification, "Psws", StringComparison.OrdinalIgnoreCase) || string.Equals(identification, "Ecp", StringComparison.OrdinalIgnoreCase) || string.Equals(identification, "Owa", StringComparison.OrdinalIgnoreCase) || string.Equals(identification, "HxS", StringComparison.OrdinalIgnoreCase)))
			{
				this.CreateBasicCache();
				this.broadcaster = new CacheBroadcaster(9050U);
				try
				{
					this.invalidationActivity = new InvalidationRecvActivity(this, 9050U);
				}
				catch (SocketException ex)
				{
					this.enabled = false;
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCSocketExceptionDisabledProvisioningCache, identification, new object[]
					{
						identification,
						ex.Message
					});
					return;
				}
				this.cleanerActivity = new ObsoluteOrgCleanerActivity(this);
				try
				{
					this.diagnosticActivity = new DiagnosticActivity(this, CacheApplicationManager.GetAppPipeName(identification));
				}
				catch (IOException)
				{
				}
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCProvisioningCacheEnabled, identification, new object[]
				{
					identification
				});
				this.invalidationActivity.ExecuteAsync(new Action<Activity, Exception>(this.OnActivityCompleted));
				this.cleanerActivity.ExecuteAsync(new Action<Activity, Exception>(this.OnActivityCompleted));
				if (this.diagnosticActivity != null)
				{
					this.diagnosticActivity.ExecuteAsync(new Action<Activity, Exception>(this.OnActivityCompleted));
				}
				ProvisioningCache.InitializePerfCounters();
			}
		}

		// Token: 0x06006196 RID: 24982 RVA: 0x0014D720 File Offset: 0x0014B920
		private IEnumerable<CachedEntryObject> DumpGlobalCachedEntries(ICollection<Guid> keys)
		{
			if (keys.Count == 0)
			{
				keys = CannedProvisioningCacheKeys.GlobalCacheKeys;
			}
			foreach (Guid key in keys)
			{
				object value = null;
				if (this.globalLocks.ContainsKey(key))
				{
					try
					{
						if (this.TryEnterReadLock(this.globalLocks[key], key, Guid.Empty) && this.globalData.ContainsKey(key))
						{
							ExpiringCacheObject expiringCacheObject = this.globalData[key];
							if (!expiringCacheObject.IsExpired)
							{
								value = expiringCacheObject.Data;
							}
						}
					}
					finally
					{
						this.globalLocks[key].ExitReadLock();
					}
					yield return new CachedEntryObject(key, value);
				}
			}
			yield break;
		}

		// Token: 0x06006197 RID: 24983 RVA: 0x0014DC70 File Offset: 0x0014BE70
		private IEnumerable<CachedEntryObject> DumpOrganizationCachedEntries(ICollection<Guid> orgs, ICollection<Guid> keys)
		{
			Guid[] allOrgs = null;
			bool dumpAll = keys.Count == 0;
			try
			{
				if (this.TryEnterEntryRemovalReadLock(this.organizationTableLock))
				{
					allOrgs = new Guid[this.organizationLocks.Count];
					this.organizationLocks.Keys.CopyTo(allOrgs, 0);
				}
			}
			finally
			{
				try
				{
					this.organizationTableLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			if (orgs.Count == 0)
			{
				orgs = allOrgs;
			}
			foreach (Guid orgId in orgs)
			{
				if (this.organizationLocks.ContainsKey(orgId) && this.TryEnterReadLock(this.organizationLocks[orgId], Guid.Empty, orgId))
				{
					try
					{
						Dictionary<Guid, ExpiringCacheObject> dict = this.organizationData[orgId];
						if (dumpAll)
						{
							foreach (Guid guid in dict.Keys)
							{
								if (!dict[guid].IsExpired)
								{
									yield return new CachedEntryObject(guid, orgId, dict[guid].Data);
								}
							}
						}
						else
						{
							foreach (Guid guid2 in keys)
							{
								if (dict.ContainsKey(guid2) && !dict[guid2].IsExpired)
								{
									yield return new CachedEntryObject(guid2, orgId, dict[guid2].Data);
								}
							}
						}
					}
					finally
					{
						this.organizationLocks[orgId].ExitReadLock();
					}
				}
			}
			yield break;
		}

		// Token: 0x06006198 RID: 24984 RVA: 0x0014DC9C File Offset: 0x0014BE9C
		private void CreateBasicCache()
		{
			this.enabled = true;
			this.globalLocks = new Dictionary<Guid, ReaderWriterLockSlim>();
			foreach (Guid key in CannedProvisioningCacheKeys.GlobalCacheKeys)
			{
				this.globalLocks[key] = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
			}
			this.globalData = new Dictionary<Guid, ExpiringCacheObject>();
			this.organizationTableLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
			this.organizationLocks = new Dictionary<Guid, ReaderWriterLockSlim>();
			this.organizationData = new Dictionary<Guid, Dictionary<Guid, ExpiringCacheObject>>();
		}

		// Token: 0x06006199 RID: 24985 RVA: 0x0014DD1C File Offset: 0x0014BF1C
		private ReaderWriterLockSlim RetrieveOrganizationLock(Guid orgId)
		{
			ReaderWriterLockSlim result = null;
			if (this.TryEnterReadLock(this.organizationTableLock, Guid.Empty, orgId))
			{
				bool flag = true;
				try
				{
					if (!this.organizationLocks.ContainsKey(orgId))
					{
						this.organizationTableLock.ExitReadLock();
						flag = false;
						if (!this.TryEnterWriteLock(this.organizationTableLock, Guid.Empty, orgId))
						{
							goto IL_A3;
						}
						try
						{
							if (!this.organizationLocks.ContainsKey(orgId))
							{
								this.organizationLocks[orgId] = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
								this.organizationData[orgId] = new Dictionary<Guid, ExpiringCacheObject>();
							}
							result = this.organizationLocks[orgId];
							goto IL_A3;
						}
						finally
						{
							this.organizationTableLock.ExitWriteLock();
						}
					}
					result = this.organizationLocks[orgId];
					IL_A3:;
				}
				finally
				{
					if (flag)
					{
						this.organizationTableLock.ExitReadLock();
					}
				}
			}
			return result;
		}

		// Token: 0x0600619A RID: 24986 RVA: 0x0014DDFC File Offset: 0x0014BFFC
		private TimeSpan GetExpirationTime(Guid key)
		{
			TimeSpan zero = TimeSpan.Zero;
			if (ProvisioningCache.configuredExpirationTime.TryGetValue(key, out zero))
			{
				return zero;
			}
			return CannedProvisioningExpirationTime.GetDefaultExpirationTime(key);
		}

		// Token: 0x0600619B RID: 24987 RVA: 0x0014DE28 File Offset: 0x0014C028
		private bool TryEnterReadLock(ReaderWriterLockSlim lockSlim, Guid key, Guid orgId)
		{
			if (lockSlim.TryEnterReadLock(ProvisioningCache.lockEnterExpirationTime))
			{
				return true;
			}
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCEnterReadLockFailed, key.ToString(), new object[]
			{
				ProvisioningCache.lockEnterExpirationTime,
				key.ToString(),
				orgId.ToString()
			});
			return false;
		}

		// Token: 0x0600619C RID: 24988 RVA: 0x0014DE94 File Offset: 0x0014C094
		private bool TryEnterWriteLock(ReaderWriterLockSlim lockSlim, Guid key, Guid orgId)
		{
			if (lockSlim.TryEnterWriteLock(ProvisioningCache.lockEnterExpirationTime))
			{
				return true;
			}
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCEnterWriteLockFailed, key.ToString(), new object[]
			{
				ProvisioningCache.lockEnterExpirationTime,
				key.ToString(),
				orgId.ToString()
			});
			return false;
		}

		// Token: 0x0600619D RID: 24989 RVA: 0x0014DF00 File Offset: 0x0014C100
		private bool TryEnterEntryRemovalReadLock(ReaderWriterLockSlim lockSlim)
		{
			if (lockSlim.TryEnterReadLock(ProvisioningCache.lockEnterExpirationTimeForEntryRemoval))
			{
				return true;
			}
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCEnterReadLockForOrgRemovalFailed, lockSlim.ToString(), new object[]
			{
				ProvisioningCache.lockEnterExpirationTimeForEntryRemoval
			});
			return false;
		}

		// Token: 0x0600619E RID: 24990 RVA: 0x0014DF44 File Offset: 0x0014C144
		private bool TryEnterEntryRemovalWriteLock(ReaderWriterLockSlim lockSlim, Guid key, Guid orgId)
		{
			if (lockSlim.TryEnterWriteLock(ProvisioningCache.lockEnterExpirationTimeForEntryRemoval))
			{
				return true;
			}
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCEnterWriteLockForOrgDataRemovalFailed, key.ToString(), new object[]
			{
				ProvisioningCache.lockEnterExpirationTimeForEntryRemoval,
				key.ToString(),
				orgId.ToString()
			});
			return false;
		}

		// Token: 0x0600619F RID: 24991 RVA: 0x0014DFB0 File Offset: 0x0014C1B0
		private void OnActivityCompleted(Activity activity, Exception exception)
		{
			if (exception != null)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCUnhandledExceptionInActivity, activity.Name, new object[]
				{
					activity.Name,
					exception.Message
				});
				if (!activity.GotStopSignalFromTestCode)
				{
					throw exception;
				}
			}
		}

		// Token: 0x0400415F RID: 16735
		private const uint BroadcastPort = 9050U;

		// Token: 0x04004160 RID: 16736
		private static readonly string MSExchangeProvisioningCacheRegistryPath = "SOFTWARE\\Microsoft\\ExchangeServer\\ProvisioningCache";

		// Token: 0x04004161 RID: 16737
		private static readonly string ProvisioningCacheExpiration = "ProvisioningCacheExpiration";

		// Token: 0x04004162 RID: 16738
		private static readonly string ProvisioningCacheEnabled = "ProvisioningCacheEnabled";

		// Token: 0x04004163 RID: 16739
		private static readonly TimeSpan lockEnterExpirationTime = new TimeSpan(0, 0, 1);

		// Token: 0x04004164 RID: 16740
		private static readonly TimeSpan lockEnterExpirationTimeForEntryRemoval = new TimeSpan(0, 0, 15);

		// Token: 0x04004165 RID: 16741
		private static ProvisioningCache cacheInstance = null;

		// Token: 0x04004166 RID: 16742
		private static ProvisioningCache disabledInstance = null;

		// Token: 0x04004167 RID: 16743
		private static object cacheInstanceLock = new object();

		// Token: 0x04004168 RID: 16744
		private static object disabledInstanceLock = new object();

		// Token: 0x04004169 RID: 16745
		private static string cacheIdentification = null;

		// Token: 0x0400416A RID: 16746
		private static bool configuredEnable = false;

		// Token: 0x0400416B RID: 16747
		private static Dictionary<Guid, TimeSpan> configuredExpirationTime = new Dictionary<Guid, TimeSpan>();

		// Token: 0x0400416C RID: 16748
		private static bool appRegistryInitialized = false;

		// Token: 0x0400416D RID: 16749
		private static object appRegistryInitializedLockObj = new object();

		// Token: 0x0400416E RID: 16750
		private static MSExchangeProvisioningCacheInstance perfCounters = null;

		// Token: 0x0400416F RID: 16751
		private object cacheBroadcasterLock = new object();

		// Token: 0x04004170 RID: 16752
		private bool enabled;

		// Token: 0x04004171 RID: 16753
		private Dictionary<Guid, ReaderWriterLockSlim> globalLocks;

		// Token: 0x04004172 RID: 16754
		private Dictionary<Guid, ExpiringCacheObject> globalData;

		// Token: 0x04004173 RID: 16755
		private ReaderWriterLockSlim organizationTableLock;

		// Token: 0x04004174 RID: 16756
		private Dictionary<Guid, ReaderWriterLockSlim> organizationLocks;

		// Token: 0x04004175 RID: 16757
		private Dictionary<Guid, Dictionary<Guid, ExpiringCacheObject>> organizationData;

		// Token: 0x04004176 RID: 16758
		private InvalidationRecvActivity invalidationActivity;

		// Token: 0x04004177 RID: 16759
		private ObsoluteOrgCleanerActivity cleanerActivity;

		// Token: 0x04004178 RID: 16760
		private DiagnosticActivity diagnosticActivity;

		// Token: 0x04004179 RID: 16761
		private CacheBroadcaster broadcaster;

		// Token: 0x020007AA RID: 1962
		// (Invoke) Token: 0x060061A4 RID: 24996
		public delegate object CacheObjectGetterDelegate();
	}
}
