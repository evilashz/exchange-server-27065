using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C9A RID: 3226
	internal static class TlsCredentialCache
	{
		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06004711 RID: 18193 RVA: 0x000BF3E1 File Offset: 0x000BD5E1
		internal static int MaxEntries
		{
			get
			{
				return TlsCredentialCache.maxEntries;
			}
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06004712 RID: 18194 RVA: 0x000BF3E8 File Offset: 0x000BD5E8
		internal static int MaxPerHost
		{
			get
			{
				return TlsCredentialCache.maxPerHost;
			}
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x06004713 RID: 18195 RVA: 0x000BF3EF File Offset: 0x000BD5EF
		internal static int Count
		{
			get
			{
				return TlsCredentialCache.cacheSize;
			}
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x000BF3F6 File Offset: 0x000BD5F6
		internal static void Initialize(int maxEntries, int maxPerHost)
		{
			if (TlsCredentialCache.cache != null)
			{
				throw new InvalidOperationException();
			}
			TlsCredentialCache.maxEntries = maxEntries;
			TlsCredentialCache.maxPerHost = maxPerHost;
			TlsCredentialCache.cache = new Dictionary<TlsCredentialCache.CacheKey, TlsCredentialCache.CacheValue>(maxEntries / maxPerHost);
		}

		// Token: 0x06004715 RID: 18197 RVA: 0x000BF420 File Offset: 0x000BD620
		internal static void Shutdown()
		{
			Dictionary<TlsCredentialCache.CacheKey, TlsCredentialCache.CacheValue> dictionary;
			lock (TlsCredentialCache.syncRoot)
			{
				if (TlsCredentialCache.cache == null)
				{
					return;
				}
				dictionary = TlsCredentialCache.cache;
				TlsCredentialCache.cache = null;
				TlsCredentialCache.cacheSize = 0;
			}
			foreach (KeyValuePair<TlsCredentialCache.CacheKey, TlsCredentialCache.CacheValue> keyValuePair in dictionary)
			{
				keyValuePair.Value.DeleteAll();
			}
		}

		// Token: 0x06004716 RID: 18198 RVA: 0x000BF4B8 File Offset: 0x000BD6B8
		internal static SafeCredentialsHandle Find(X509Certificate cert, string targetName)
		{
			if (TlsCredentialCache.cache == null)
			{
				return null;
			}
			SafeCredentialsHandle result;
			lock (TlsCredentialCache.syncRoot)
			{
				if (TlsCredentialCache.cache == null)
				{
					result = null;
				}
				else
				{
					TlsCredentialCache.CacheKey key = new TlsCredentialCache.CacheKey(cert, targetName);
					TlsCredentialCache.CacheValue cacheValue;
					if (!TlsCredentialCache.cache.TryGetValue(key, out cacheValue))
					{
						result = null;
					}
					else
					{
						SafeCredentialsHandle safeCredentialsHandle = cacheValue.Pop();
						TlsCredentialCache.cacheSize--;
						if (cacheValue.Count == 0)
						{
							TlsCredentialCache.cache.Remove(key);
						}
						result = safeCredentialsHandle;
					}
				}
			}
			return result;
		}

		// Token: 0x06004717 RID: 18199 RVA: 0x000BF550 File Offset: 0x000BD750
		internal static void Add(X509Certificate cert, string targetName, SafeCredentialsHandle handle)
		{
			if (TlsCredentialCache.cache != null)
			{
				lock (TlsCredentialCache.syncRoot)
				{
					if (TlsCredentialCache.cache != null)
					{
						TlsCredentialCache.CacheKey key = new TlsCredentialCache.CacheKey(cert, targetName);
						TlsCredentialCache.CacheValue cacheValue;
						if (!TlsCredentialCache.cache.TryGetValue(key, out cacheValue))
						{
							cacheValue = new TlsCredentialCache.CacheValue();
							TlsCredentialCache.cache.Add(key, cacheValue);
						}
						if (cacheValue.Push(handle))
						{
							TlsCredentialCache.cacheSize++;
							if (TlsCredentialCache.cacheSize > TlsCredentialCache.maxEntries)
							{
								TlsCredentialCache.ExpireCache(10);
							}
						}
						return;
					}
				}
			}
			handle.Dispose();
		}

		// Token: 0x06004718 RID: 18200 RVA: 0x000BF5F0 File Offset: 0x000BD7F0
		internal static void Flush()
		{
			lock (TlsCredentialCache.syncRoot)
			{
				foreach (KeyValuePair<TlsCredentialCache.CacheKey, TlsCredentialCache.CacheValue> keyValuePair in TlsCredentialCache.cache)
				{
					keyValuePair.Value.DeleteAll();
				}
				TlsCredentialCache.cache.Clear();
				TlsCredentialCache.cacheSize = 0;
			}
		}

		// Token: 0x06004719 RID: 18201 RVA: 0x000BF680 File Offset: 0x000BD880
		private static void ExpireCache(int purgePercentage)
		{
			ExTraceGlobals.NetworkTracer.TraceDebug(0L, "Expiring TLS credential cache");
			List<TlsCredentialCache.CacheExpireEntry> list = new List<TlsCredentialCache.CacheExpireEntry>(TlsCredentialCache.cacheSize);
			foreach (KeyValuePair<TlsCredentialCache.CacheKey, TlsCredentialCache.CacheValue> keyValuePair in TlsCredentialCache.cache)
			{
				foreach (TlsCredentialCache.CachedCredential cachedCredential in keyValuePair.Value.Credentials)
				{
					TlsCredentialCache.CacheExpireEntry cacheExpireEntry = new TlsCredentialCache.CacheExpireEntry(keyValuePair.Key, cachedCredential.Created);
					int num = list.BinarySearch(cacheExpireEntry, cacheExpireEntry);
					if (num < 0)
					{
						num = ~num;
					}
					list.Insert(num, cacheExpireEntry);
				}
			}
			int num2 = TlsCredentialCache.cacheSize * purgePercentage / 100;
			foreach (TlsCredentialCache.CacheExpireEntry cacheExpireEntry2 in list)
			{
				if (num2-- <= 0)
				{
					break;
				}
				ExTraceGlobals.NetworkTracer.TraceDebug<string, DateTime>(0L, "Purging {0}, created {1}", cacheExpireEntry2.Key.TargetName, cacheExpireEntry2.Created);
				TlsCredentialCache.ExpireEntry(cacheExpireEntry2);
			}
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x000BF7D4 File Offset: 0x000BD9D4
		private static void ExpireEntry(TlsCredentialCache.CacheExpireEntry entry)
		{
			TlsCredentialCache.CacheValue cacheValue = TlsCredentialCache.cache[entry.Key];
			cacheValue.DeleteOldest();
			TlsCredentialCache.cacheSize--;
			if (cacheValue.Count == 0)
			{
				TlsCredentialCache.cache.Remove(entry.Key);
			}
		}

		// Token: 0x04003C2F RID: 15407
		private const int PurgePercentage = 10;

		// Token: 0x04003C30 RID: 15408
		private static int maxEntries;

		// Token: 0x04003C31 RID: 15409
		private static int maxPerHost;

		// Token: 0x04003C32 RID: 15410
		private static int cacheSize;

		// Token: 0x04003C33 RID: 15411
		private static object syncRoot = new object();

		// Token: 0x04003C34 RID: 15412
		private static Dictionary<TlsCredentialCache.CacheKey, TlsCredentialCache.CacheValue> cache;

		// Token: 0x02000C9B RID: 3227
		private class CacheKey
		{
			// Token: 0x0600471C RID: 18204 RVA: 0x000BF829 File Offset: 0x000BDA29
			internal CacheKey(X509Certificate certificate, string targetName)
			{
				this.certificate = certificate;
				this.targetName = targetName;
			}

			// Token: 0x170011DB RID: 4571
			// (get) Token: 0x0600471D RID: 18205 RVA: 0x000BF83F File Offset: 0x000BDA3F
			internal X509Certificate Certificate
			{
				get
				{
					return this.certificate;
				}
			}

			// Token: 0x170011DC RID: 4572
			// (get) Token: 0x0600471E RID: 18206 RVA: 0x000BF847 File Offset: 0x000BDA47
			internal string TargetName
			{
				get
				{
					return this.targetName;
				}
			}

			// Token: 0x0600471F RID: 18207 RVA: 0x000BF84F File Offset: 0x000BDA4F
			public override int GetHashCode()
			{
				return ((this.certificate == null) ? 0 : this.certificate.GetHashCode()) ^ ((this.targetName == null) ? 0 : this.targetName.GetHashCode());
			}

			// Token: 0x06004720 RID: 18208 RVA: 0x000BF880 File Offset: 0x000BDA80
			public override bool Equals(object obj)
			{
				if (obj == this)
				{
					return true;
				}
				TlsCredentialCache.CacheKey cacheKey = obj as TlsCredentialCache.CacheKey;
				return cacheKey != null && this.targetName == cacheKey.targetName && this.certificate == cacheKey.certificate;
			}

			// Token: 0x06004721 RID: 18209 RVA: 0x000BF8C2 File Offset: 0x000BDAC2
			public override string ToString()
			{
				return this.targetName + " " + this.certificate;
			}

			// Token: 0x04003C35 RID: 15413
			private readonly X509Certificate certificate;

			// Token: 0x04003C36 RID: 15414
			private readonly string targetName;
		}

		// Token: 0x02000C9C RID: 3228
		private class CacheValue
		{
			// Token: 0x170011DD RID: 4573
			// (get) Token: 0x06004722 RID: 18210 RVA: 0x000BF8DA File Offset: 0x000BDADA
			internal int Count
			{
				get
				{
					return this.credentials.Count;
				}
			}

			// Token: 0x170011DE RID: 4574
			// (get) Token: 0x06004723 RID: 18211 RVA: 0x000BF8E7 File Offset: 0x000BDAE7
			internal List<TlsCredentialCache.CachedCredential> Credentials
			{
				get
				{
					return this.credentials;
				}
			}

			// Token: 0x06004724 RID: 18212 RVA: 0x000BF8F0 File Offset: 0x000BDAF0
			internal bool Push(SafeCredentialsHandle handle)
			{
				bool result = true;
				if (this.credentials.Count == TlsCredentialCache.MaxPerHost)
				{
					this.DeleteOldest();
					result = false;
				}
				this.credentials.Add(new TlsCredentialCache.CachedCredential(handle));
				return result;
			}

			// Token: 0x06004725 RID: 18213 RVA: 0x000BF92C File Offset: 0x000BDB2C
			internal SafeCredentialsHandle Pop()
			{
				if (this.credentials.Count == 0)
				{
					throw new InvalidOperationException();
				}
				SafeCredentialsHandle handle = this.credentials[this.credentials.Count - 1].Handle;
				this.credentials.RemoveAt(this.credentials.Count - 1);
				return handle;
			}

			// Token: 0x06004726 RID: 18214 RVA: 0x000BF983 File Offset: 0x000BDB83
			internal void DeleteOldest()
			{
				if (this.credentials.Count == 0)
				{
					throw new InvalidOperationException();
				}
				this.credentials[0].Handle.Dispose();
				this.credentials.RemoveAt(0);
			}

			// Token: 0x06004727 RID: 18215 RVA: 0x000BF9BC File Offset: 0x000BDBBC
			internal void DeleteAll()
			{
				foreach (TlsCredentialCache.CachedCredential cachedCredential in this.credentials)
				{
					cachedCredential.Handle.Dispose();
				}
				this.credentials.Clear();
			}

			// Token: 0x04003C37 RID: 15415
			private List<TlsCredentialCache.CachedCredential> credentials = new List<TlsCredentialCache.CachedCredential>(TlsCredentialCache.MaxPerHost);
		}

		// Token: 0x02000C9D RID: 3229
		private class CachedCredential
		{
			// Token: 0x06004729 RID: 18217 RVA: 0x000BFA38 File Offset: 0x000BDC38
			internal CachedCredential(SafeCredentialsHandle handle)
			{
				this.handle = handle;
			}

			// Token: 0x170011DF RID: 4575
			// (get) Token: 0x0600472A RID: 18218 RVA: 0x000BFA52 File Offset: 0x000BDC52
			internal SafeCredentialsHandle Handle
			{
				get
				{
					return this.handle;
				}
			}

			// Token: 0x170011E0 RID: 4576
			// (get) Token: 0x0600472B RID: 18219 RVA: 0x000BFA5A File Offset: 0x000BDC5A
			internal DateTime Created
			{
				get
				{
					return this.created;
				}
			}

			// Token: 0x04003C38 RID: 15416
			private readonly SafeCredentialsHandle handle;

			// Token: 0x04003C39 RID: 15417
			private readonly DateTime created = DateTime.UtcNow;
		}

		// Token: 0x02000C9E RID: 3230
		private class CacheExpireEntry : IComparer<TlsCredentialCache.CacheExpireEntry>
		{
			// Token: 0x0600472C RID: 18220 RVA: 0x000BFA62 File Offset: 0x000BDC62
			internal CacheExpireEntry(TlsCredentialCache.CacheKey key, DateTime created)
			{
				this.created = created;
				this.key = key;
			}

			// Token: 0x170011E1 RID: 4577
			// (get) Token: 0x0600472D RID: 18221 RVA: 0x000BFA78 File Offset: 0x000BDC78
			internal TlsCredentialCache.CacheKey Key
			{
				get
				{
					return this.key;
				}
			}

			// Token: 0x170011E2 RID: 4578
			// (get) Token: 0x0600472E RID: 18222 RVA: 0x000BFA80 File Offset: 0x000BDC80
			internal DateTime Created
			{
				get
				{
					return this.created;
				}
			}

			// Token: 0x0600472F RID: 18223 RVA: 0x000BFA88 File Offset: 0x000BDC88
			public int Compare(TlsCredentialCache.CacheExpireEntry x, TlsCredentialCache.CacheExpireEntry y)
			{
				if (x == y)
				{
					return 0;
				}
				if (x == null)
				{
					return -1;
				}
				if (y == null)
				{
					return 1;
				}
				if (x.created < y.created)
				{
					return -1;
				}
				if (!(x.created > y.created))
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x04003C3A RID: 15418
			private readonly TlsCredentialCache.CacheKey key;

			// Token: 0x04003C3B RID: 15419
			private readonly DateTime created;
		}
	}
}
