using System;
using System.Net;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000D5 RID: 213
	internal sealed class TargetForestConfiguration
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00018168 File Offset: 0x00016368
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00018170 File Offset: 0x00016370
		public NetworkCredential Credentials
		{
			get
			{
				return this.networkCredential;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00018178 File Offset: 0x00016378
		public string FullDnsDomainName
		{
			get
			{
				return this.fullDnsDomainName;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00018180 File Offset: 0x00016380
		public AvailabilityAccessMethod AccessMethod
		{
			get
			{
				return this.accessMethod;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00018188 File Offset: 0x00016388
		public bool IsPerUserAuthorizationSupported
		{
			get
			{
				return this.accessMethod == AvailabilityAccessMethod.PerUserFB;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00018193 File Offset: 0x00016393
		public Uri AutoDiscoverUrl
		{
			get
			{
				return this.autoDiscoverUrl;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0001819B File Offset: 0x0001639B
		public AutodiscoverUrlSource AutodiscoverUrlSource
		{
			get
			{
				return this.autodiscoverUrlSource;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x000181A3 File Offset: 0x000163A3
		public LocalizedException Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x000181AB File Offset: 0x000163AB
		public void SetAutodiscoverUrl(Uri autoDiscoverUrl, AutodiscoverUrlSource autodiscoverUrlSource)
		{
			this.autoDiscoverUrl = autoDiscoverUrl;
			this.autodiscoverUrlSource = autodiscoverUrlSource;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x000181BB File Offset: 0x000163BB
		internal TargetForestConfiguration(string id, string fullDnsDomainName, AvailabilityAccessMethod accessMethod, NetworkCredential networkCredential, Uri autoDiscoverUrl, AutodiscoverUrlSource autodiscoverUrlSource)
		{
			this.id = id;
			this.fullDnsDomainName = fullDnsDomainName;
			this.accessMethod = accessMethod;
			this.networkCredential = networkCredential;
			this.autoDiscoverUrl = autoDiscoverUrl;
			this.autodiscoverUrlSource = autodiscoverUrlSource;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x000181F0 File Offset: 0x000163F0
		internal TargetForestConfiguration(string id, string fullDnsDomainName, LocalizedException exception)
		{
			this.id = id;
			this.fullDnsDomainName = fullDnsDomainName;
			this.exception = exception;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00018210 File Offset: 0x00016410
		internal CredentialCache GetCredentialCache(Uri uri)
		{
			CredentialCache credentialCache = null;
			if (this.AccessMethod == AvailabilityAccessMethod.OrgWideFBBasic && this.Credentials != null)
			{
				credentialCache = new CredentialCache();
				credentialCache.Add(uri, "Basic", this.Credentials);
			}
			return credentialCache;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001824C File Offset: 0x0001644C
		internal ProxyAuthenticator GetProxyAuthenticatorForAutoDiscover(Uri uri, SerializedSecurityContext serializedSecurityContext, string messageId)
		{
			if (this.AccessMethod == AvailabilityAccessMethod.OrgWideFBBasic && this.Credentials != null)
			{
				return ProxyAuthenticator.Create(new CredentialCache
				{
					{
						uri,
						"Basic",
						this.Credentials
					}
				}, serializedSecurityContext, messageId);
			}
			return ProxyAuthenticator.Create(this.Credentials, serializedSecurityContext, messageId);
		}

		// Token: 0x04000335 RID: 821
		private LocalizedException exception;

		// Token: 0x04000336 RID: 822
		private string id;

		// Token: 0x04000337 RID: 823
		private string fullDnsDomainName;

		// Token: 0x04000338 RID: 824
		private AvailabilityAccessMethod accessMethod;

		// Token: 0x04000339 RID: 825
		private NetworkCredential networkCredential;

		// Token: 0x0400033A RID: 826
		private Uri autoDiscoverUrl;

		// Token: 0x0400033B RID: 827
		private AutodiscoverUrlSource autodiscoverUrlSource;
	}
}
