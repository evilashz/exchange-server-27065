using System;
using System.Runtime.Caching;
using System.Threading;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x02000095 RID: 149
	internal sealed class CacheManager
	{
		// Token: 0x060008AD RID: 2221 RVA: 0x000270C4 File Offset: 0x000252C4
		private CacheManager()
		{
			this.Initialize();
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x000270D2 File Offset: 0x000252D2
		private void Initialize()
		{
			this.keyTable = new MemoryCache("KeyTable", null);
			this.adObjectCache = new MemoryCache("ADObjectCache", null);
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x000270F8 File Offset: 0x000252F8
		public static CacheManager Instance
		{
			get
			{
				if (CacheManager.singleton == null)
				{
					CacheManager value = new CacheManager();
					Interlocked.CompareExchange<CacheManager>(ref CacheManager.singleton, value, null);
				}
				return CacheManager.singleton;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00027124 File Offset: 0x00025324
		public MemoryCache KeyTable
		{
			get
			{
				return this.keyTable;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x0002712C File Offset: 0x0002532C
		public MemoryCache ADObjectCache
		{
			get
			{
				return this.adObjectCache;
			}
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00027134 File Offset: 0x00025334
		internal void ResetAllCaches()
		{
			MemoryCache memoryCache = this.KeyTable;
			MemoryCache adobjectCache = this.ADObjectCache;
			this.Initialize();
			memoryCache.Dispose();
			adobjectCache.Dispose();
		}

		// Token: 0x040002B6 RID: 694
		private static CacheManager singleton;

		// Token: 0x040002B7 RID: 695
		private MemoryCache keyTable;

		// Token: 0x040002B8 RID: 696
		private MemoryCache adObjectCache;
	}
}
