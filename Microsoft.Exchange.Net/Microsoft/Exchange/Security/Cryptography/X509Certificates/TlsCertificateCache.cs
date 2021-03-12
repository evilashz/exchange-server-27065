using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000AE8 RID: 2792
	internal class TlsCertificateCache
	{
		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06003C15 RID: 15381 RVA: 0x0009BAB4 File Offset: 0x00099CB4
		// (set) Token: 0x06003C16 RID: 15382 RVA: 0x0009BACC File Offset: 0x00099CCC
		public static ChainEnginePool ChainEnginePool
		{
			get
			{
				if (TlsCertificateCache.chainEnginePool == null)
				{
					TlsCertificateCache.ChainEnginePool = new ChainEnginePool();
				}
				return TlsCertificateCache.chainEnginePool;
			}
			set
			{
				TlsCertificateCache.chainEnginePool = value;
			}
		}

		// Token: 0x06003C17 RID: 15383 RVA: 0x0009BAD4 File Offset: 0x00099CD4
		public void Open(OpenFlags flags)
		{
			this.rootStore.Open(flags);
			this.certStore.Open(flags);
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x0009BAEE File Offset: 0x00099CEE
		public void Close()
		{
			if (this.certStore != null)
			{
				this.certStore.Close();
				this.certStore = null;
				this.certStoreBookmark = -1;
			}
			if (this.rootStore != null)
			{
				this.rootStore.Close();
				this.rootStoreBookmark = -1;
			}
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x0009BB2B File Offset: 0x00099D2B
		public void Reset()
		{
			this.cache.Clear();
			this.certStoreBookmark--;
			this.rootStoreBookmark--;
		}

		// Token: 0x06003C1A RID: 15386 RVA: 0x0009BB54 File Offset: 0x00099D54
		public X509Certificate2 Find(IEnumerable<string> names, CertificateSelectionOption options)
		{
			if (this.certStore == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(256);
			int num = 0;
			foreach (string value in names)
			{
				if (num++ != 0)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(value);
			}
			string text = stringBuilder.ToString();
			X509Certificate2 x509Certificate = null;
			lock (this)
			{
				this.CheckStores();
				bool flag2 = this.cache.TryGetValue(text, out x509Certificate);
				if (flag2)
				{
					if (x509Certificate != null)
					{
						goto IL_13F;
					}
				}
				try
				{
					using (ChainEngine engine = TlsCertificateCache.ChainEnginePool.GetEngine())
					{
						x509Certificate = TlsCertificateInfo.FindCertificate(this.certStore.BaseStore, names, options, engine);
					}
					if (flag2 && x509Certificate != null)
					{
						ExTraceGlobals.CertificateTracer.TraceDebug<string>(0L, "Replacing null certificate for domains: {0}", text);
						this.cache[text] = x509Certificate;
					}
					else
					{
						this.cache.Add(text, x509Certificate);
						ExTraceGlobals.CertificateTracer.TraceDebug<string, string>(0L, "Adding newly found certificate {0} for domains: {1}", (x509Certificate == null) ? "NULL" : x509Certificate.Thumbprint, text);
					}
				}
				catch (ArgumentException ex)
				{
					x509Certificate = null;
					ExTraceGlobals.CertificateTracer.TraceDebug<string, string>(0L, "No certificate returned for domains: {0} due to exception reason: {1}", text, ex.Message);
				}
				IL_13F:;
			}
			return x509Certificate;
		}

		// Token: 0x06003C1B RID: 15387 RVA: 0x0009BCE4 File Offset: 0x00099EE4
		private void Clear()
		{
			this.cache.Clear();
			this.certStoreBookmark = -1;
		}

		// Token: 0x06003C1C RID: 15388 RVA: 0x0009BCF8 File Offset: 0x00099EF8
		private void CheckStores()
		{
			int bookmark = this.certStore.Bookmark;
			int bookmark2 = this.rootStore.Bookmark;
			if (bookmark != this.certStoreBookmark || bookmark2 != this.rootStoreBookmark)
			{
				this.Clear();
				this.certStoreBookmark = bookmark;
				this.rootStoreBookmark = bookmark2;
			}
		}

		// Token: 0x040034B6 RID: 13494
		private CertificateStore certStore = new CertificateStore(StoreName.My, StoreLocation.LocalMachine);

		// Token: 0x040034B7 RID: 13495
		private static ChainEnginePool chainEnginePool = new ChainEnginePool();

		// Token: 0x040034B8 RID: 13496
		private int certStoreBookmark = -1;

		// Token: 0x040034B9 RID: 13497
		private CertificateStore rootStore = new CertificateStore(StoreName.Root, StoreLocation.LocalMachine);

		// Token: 0x040034BA RID: 13498
		private int rootStoreBookmark = -1;

		// Token: 0x040034BB RID: 13499
		private Dictionary<string, X509Certificate2> cache = new Dictionary<string, X509Certificate2>(StringComparer.OrdinalIgnoreCase);
	}
}
