using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000A91 RID: 2705
	internal class ChainEnginePool : IDisposable, IEnginePool
	{
		// Token: 0x06003A64 RID: 14948 RVA: 0x00095391 File Offset: 0x00093591
		public ChainEnginePool() : this(10, ChainEngineOptions.CacheEndCert | ChainEngineOptions.UseLocalMachineStore | ChainEngineOptions.EnableCacheAutoUpdate | ChainEngineOptions.EnableShareStore, ChainEnginePool.DefaultTimeout, 0, null, false)
		{
		}

		// Token: 0x06003A65 RID: 14949 RVA: 0x000953A8 File Offset: 0x000935A8
		public ChainEnginePool(int count, ChainEngineOptions options, TimeSpan timeout, int cacheLimit, X509Store store = null, bool exclusiveTrustMode = false)
		{
			this.configuration = new ChainEnginePool.ChainEngineConfig(options, (int)timeout.TotalMilliseconds, cacheLimit);
			if (store != null && exclusiveTrustMode)
			{
				this.configuration.HExclusiveRoot = store.StoreHandle;
			}
			this.engines = new Stack<SafeChainEngineHandle>(count);
		}

		// Token: 0x06003A66 RID: 14950 RVA: 0x000953F7 File Offset: 0x000935F7
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x06003A67 RID: 14951 RVA: 0x000953FF File Offset: 0x000935FF
		public void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003A68 RID: 14952 RVA: 0x00095410 File Offset: 0x00093610
		public ChainEngine GetEngine()
		{
			lock (this)
			{
				if (this.disposed)
				{
					return new ChainEngine(null, SafeChainEngineHandle.Create(this.configuration));
				}
				if (this.engines.Count > 0)
				{
					return new ChainEngine(this, this.engines.Pop());
				}
			}
			return new ChainEngine(this, SafeChainEngineHandle.Create(this.configuration));
		}

		// Token: 0x06003A69 RID: 14953 RVA: 0x00095498 File Offset: 0x00093698
		void IEnginePool.ReturnTo(SafeChainEngineHandle item)
		{
			if (item == null)
			{
				return;
			}
			if (this.disposed)
			{
				item.Close();
				return;
			}
			lock (this)
			{
				if (this.disposed)
				{
					item.Close();
				}
				else
				{
					this.engines.Push(item);
				}
			}
		}

		// Token: 0x06003A6A RID: 14954 RVA: 0x000954FC File Offset: 0x000936FC
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (this)
				{
					this.disposed = true;
				}
				Stack<SafeChainEngineHandle> stack = Interlocked.CompareExchange<Stack<SafeChainEngineHandle>>(ref this.engines, null, null);
				if (stack != null)
				{
					while (stack.Count > 0)
					{
						SafeChainEngineHandle safeChainEngineHandle = stack.Pop();
						safeChainEngineHandle.Close();
					}
				}
			}
		}

		// Token: 0x04003294 RID: 12948
		private const int DefaultCacheLimit = 0;

		// Token: 0x04003295 RID: 12949
		private const ChainEngineOptions DefaultOptions = ChainEngineOptions.CacheEndCert | ChainEngineOptions.UseLocalMachineStore | ChainEngineOptions.EnableCacheAutoUpdate | ChainEngineOptions.EnableShareStore;

		// Token: 0x04003296 RID: 12950
		private static readonly TimeSpan DefaultTimeout = TimeSpan.FromMilliseconds(10000.0);

		// Token: 0x04003297 RID: 12951
		private ChainEnginePool.ChainEngineConfig configuration;

		// Token: 0x04003298 RID: 12952
		private bool disposed;

		// Token: 0x04003299 RID: 12953
		private Stack<SafeChainEngineHandle> engines;

		// Token: 0x02000A92 RID: 2706
		internal struct ChainEngineConfig
		{
			// Token: 0x06003A6C RID: 14956 RVA: 0x0009557C File Offset: 0x0009377C
			public ChainEngineConfig(ChainEngineOptions flags, int timeout, int maximum)
			{
				this.size = Marshal.SizeOf(typeof(ChainEnginePool.ChainEngineConfig));
				this.restrictedRootHandle = IntPtr.Zero;
				this.restrictedTrustHandle = IntPtr.Zero;
				this.restrictedOtherHandle = IntPtr.Zero;
				this.additionalStoresCount = 0;
				this.additionalStores = IntPtr.Zero;
				this.options = flags;
				this.urlRetrievalTimeout = timeout;
				this.maximumCachedCertificates = maximum;
				this.cycleDetectionModulus = 0;
				this.hExclusiveRoot = IntPtr.Zero;
				this.hExclusiveTrustedPeople = IntPtr.Zero;
			}

			// Token: 0x17000E8C RID: 3724
			// (get) Token: 0x06003A6D RID: 14957 RVA: 0x00095603 File Offset: 0x00093803
			// (set) Token: 0x06003A6E RID: 14958 RVA: 0x0009560B File Offset: 0x0009380B
			public IntPtr HExclusiveRoot
			{
				get
				{
					return this.hExclusiveRoot;
				}
				set
				{
					this.hExclusiveRoot = value;
				}
			}

			// Token: 0x17000E8D RID: 3725
			// (get) Token: 0x06003A6F RID: 14959 RVA: 0x00095614 File Offset: 0x00093814
			public ChainEngineOptions Options
			{
				get
				{
					return this.options;
				}
			}

			// Token: 0x17000E8E RID: 3726
			// (get) Token: 0x06003A70 RID: 14960 RVA: 0x0009561C File Offset: 0x0009381C
			public int UrlRetrievalTimeout
			{
				get
				{
					return this.urlRetrievalTimeout;
				}
			}

			// Token: 0x17000E8F RID: 3727
			// (get) Token: 0x06003A71 RID: 14961 RVA: 0x00095624 File Offset: 0x00093824
			public int MaximumCachedCertificates
			{
				get
				{
					return this.maximumCachedCertificates;
				}
			}

			// Token: 0x0400329A RID: 12954
			private int size;

			// Token: 0x0400329B RID: 12955
			private IntPtr restrictedRootHandle;

			// Token: 0x0400329C RID: 12956
			private IntPtr restrictedTrustHandle;

			// Token: 0x0400329D RID: 12957
			private IntPtr restrictedOtherHandle;

			// Token: 0x0400329E RID: 12958
			private int additionalStoresCount;

			// Token: 0x0400329F RID: 12959
			private IntPtr additionalStores;

			// Token: 0x040032A0 RID: 12960
			private ChainEngineOptions options;

			// Token: 0x040032A1 RID: 12961
			private int urlRetrievalTimeout;

			// Token: 0x040032A2 RID: 12962
			private int maximumCachedCertificates;

			// Token: 0x040032A3 RID: 12963
			private int cycleDetectionModulus;

			// Token: 0x040032A4 RID: 12964
			private IntPtr hExclusiveRoot;

			// Token: 0x040032A5 RID: 12965
			private IntPtr hExclusiveTrustedPeople;
		}
	}
}
