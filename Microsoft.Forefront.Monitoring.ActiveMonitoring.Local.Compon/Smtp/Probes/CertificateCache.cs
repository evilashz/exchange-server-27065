using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000213 RID: 531
	internal class CertificateCache
	{
		// Token: 0x0600108A RID: 4234 RVA: 0x0002FF78 File Offset: 0x0002E178
		public CertificateCache(ChainEnginePool pool)
		{
			if (pool == null)
			{
				throw new ArgumentNullException("pool");
			}
			this.pool = pool;
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0002FFE8 File Offset: 0x0002E1E8
		public void Open(OpenFlags flags)
		{
			this.rootStore.Open(flags);
			this.certStore.Open(flags);
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00030002 File Offset: 0x0002E202
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

		// Token: 0x0600108D RID: 4237 RVA: 0x0003003F File Offset: 0x0002E23F
		public void Reset()
		{
			this.cache.Clear();
			this.certStoreBookmark--;
			this.rootStoreBookmark--;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00030068 File Offset: 0x0002E268
		public X509Certificate2 Find(IEnumerable<string> names, bool wildcardAllowed)
		{
			return this.Find(names, wildcardAllowed, WildcardMatchType.MultiLevel);
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00030074 File Offset: 0x0002E274
		public X509Certificate2 Find(IEnumerable<string> names, bool wildcardAllowed, WildcardMatchType wildcardMatchType)
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
			string key = stringBuilder.ToString();
			X509Certificate2 x509Certificate = null;
			lock (this)
			{
				this.CheckStores();
				if (!this.cache.TryGetValue(key, out x509Certificate))
				{
					CertificateSelectionOption certificateSelectionOption = wildcardAllowed ? CertificateSelectionOption.WildcardAllowed : CertificateSelectionOption.None;
					certificateSelectionOption |= CertificateSelectionOption.PreferedNonSelfSigned;
					try
					{
						using (ChainEngine engine = this.pool.GetEngine())
						{
							x509Certificate = TlsCertificateInfo.FindCertificate(this.certStore.BaseStore, names, certificateSelectionOption, wildcardMatchType, engine);
						}
						this.cache.Add(key, x509Certificate);
					}
					catch (ArgumentException)
					{
						x509Certificate = null;
					}
				}
			}
			if (x509Certificate != null)
			{
				ExTraceGlobals.CertificateTracer.Information<string, EnumerableTracer<string>>((long)names.GetHashCode(), "Certificate search found [{0}] which has one of the following FQDN's : {1} ", x509Certificate.Thumbprint, new EnumerableTracer<string>(names));
			}
			else
			{
				ExTraceGlobals.CertificateTracer.Information<EnumerableTracer<string>>((long)names.GetHashCode(), "No certificate search found which has one of the following FQDN's : {0} ", new EnumerableTracer<string>(names));
			}
			return x509Certificate;
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x000301E8 File Offset: 0x0002E3E8
		public X509Certificate2 Find(string thumbPrint)
		{
			if (this.certStore == null)
			{
				return null;
			}
			X509Certificate2 x509Certificate = null;
			lock (this)
			{
				this.CheckStores();
				if (!this.thumbprintBasedCache.TryGetValue(thumbPrint, out x509Certificate))
				{
					x509Certificate = TlsCertificateInfo.FindCertificate(this.certStore.BaseStore, X509FindType.FindByThumbprint, thumbPrint);
					this.thumbprintBasedCache.Add(thumbPrint, x509Certificate);
				}
			}
			if (x509Certificate != null)
			{
				ExTraceGlobals.CertificateTracer.Information<string>(0L, "A certificate with thumbprint [{0}] has been found.", thumbPrint);
			}
			else
			{
				ExTraceGlobals.CertificateTracer.TraceError<string>(0L, "A certificate with thumbprint [{0}] has not been found.", thumbPrint);
			}
			return x509Certificate;
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0003028C File Offset: 0x0002E48C
		private void Clear()
		{
			this.cache.Clear();
			this.thumbprintBasedCache.Clear();
			this.certStoreBookmark = -1;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x000302AC File Offset: 0x0002E4AC
		private void CheckStores()
		{
			int bookmark = this.certStore.Bookmark;
			int bookmark2 = this.rootStore.Bookmark;
			if (bookmark != this.certStoreBookmark || bookmark2 != this.rootStoreBookmark)
			{
				ExTraceGlobals.CertificateTracer.Information(0L, "Certificate store My | Root has changed - flushing certificate cache");
				this.Clear();
				this.certStoreBookmark = bookmark;
				this.rootStoreBookmark = bookmark2;
			}
		}

		// Token: 0x040007E0 RID: 2016
		private CertificateStore certStore = new CertificateStore(StoreName.My, StoreLocation.LocalMachine);

		// Token: 0x040007E1 RID: 2017
		private int certStoreBookmark = -1;

		// Token: 0x040007E2 RID: 2018
		private CertificateStore rootStore = new CertificateStore(StoreName.Root, StoreLocation.LocalMachine);

		// Token: 0x040007E3 RID: 2019
		private int rootStoreBookmark = -1;

		// Token: 0x040007E4 RID: 2020
		private Dictionary<string, X509Certificate2> cache = new Dictionary<string, X509Certificate2>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040007E5 RID: 2021
		private Dictionary<string, X509Certificate2> thumbprintBasedCache = new Dictionary<string, X509Certificate2>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040007E6 RID: 2022
		private ChainEnginePool pool;
	}
}
