using System;
using System.Net;
using Microsoft.Exchange.SoapWebClient;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200005C RID: 92
	internal sealed class AutoDiscoverAuthenticator
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000A018 File Offset: 0x00008218
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000A020 File Offset: 0x00008220
		public NetworkCredential Credentials { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000A029 File Offset: 0x00008229
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000A031 File Offset: 0x00008231
		public CredentialCache CredentialCache { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000A03A File Offset: 0x0000823A
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000A042 File Offset: 0x00008242
		public ProxyAuthenticator ProxyAuthenticator { get; private set; }

		// Token: 0x06000211 RID: 529 RVA: 0x0000A04B File Offset: 0x0000824B
		public AutoDiscoverAuthenticator(NetworkCredential credentials)
		{
			if (Testability.WebServiceCredentials == null)
			{
				this.Credentials = credentials;
			}
			else
			{
				this.Credentials = Testability.WebServiceCredentials;
			}
			this.CredentialCache = null;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000A075 File Offset: 0x00008275
		public AutoDiscoverAuthenticator(CredentialCache cache, NetworkCredential credentials)
		{
			if (Testability.WebServiceCredentials == null)
			{
				this.CredentialCache = cache;
				this.Credentials = credentials;
				return;
			}
			this.Credentials = Testability.WebServiceCredentials;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000A09E File Offset: 0x0000829E
		public AutoDiscoverAuthenticator(ProxyAuthenticator proxyAuthenticator)
		{
			this.ProxyAuthenticator = proxyAuthenticator;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000A0B0 File Offset: 0x000082B0
		public void Authenticate(CustomSoapHttpClientProtocol client)
		{
			if (this.ProxyAuthenticator == null)
			{
				if (this.CredentialCache != null)
				{
					this.ProxyAuthenticator = ProxyAuthenticator.Create(this.CredentialCache, null, null);
				}
				else
				{
					this.ProxyAuthenticator = ProxyAuthenticator.Create(this.Credentials, null, null);
				}
			}
			this.ProxyAuthenticator.Authenticate(client);
		}
	}
}
