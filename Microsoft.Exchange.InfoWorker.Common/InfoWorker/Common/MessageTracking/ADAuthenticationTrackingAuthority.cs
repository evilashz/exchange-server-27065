using System;
using System.Net;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.SoapWebClient;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002BD RID: 701
	internal abstract class ADAuthenticationTrackingAuthority : WebServiceTrackingAuthority
	{
		// Token: 0x0600138A RID: 5002 RVA: 0x0005AB4D File Offset: 0x00058D4D
		protected override void SetAuthenticationMechanism(ExchangeServiceBinding ewsBinding)
		{
			if (ADAuthenticationTrackingAuthority.CanImpersonateNetworkService)
			{
				ewsBinding.Authenticator = SoapHttpClientAuthenticator.CreateNetworkService();
				return;
			}
			ewsBinding.Credentials = CredentialCache.DefaultNetworkCredentials;
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x0005AB70 File Offset: 0x00058D70
		private static bool CanImpersonateNetworkService
		{
			get
			{
				if (ADAuthenticationTrackingAuthority.canImpersonateNetworkService == null)
				{
					lock (ADAuthenticationTrackingAuthority.initLock)
					{
						if (ADAuthenticationTrackingAuthority.canImpersonateNetworkService == null)
						{
							NetworkServiceImpersonator.Initialize();
							ADAuthenticationTrackingAuthority.canImpersonateNetworkService = new bool?(NetworkServiceImpersonator.Exception == null);
						}
					}
				}
				return ADAuthenticationTrackingAuthority.canImpersonateNetworkService.Value;
			}
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0005ABE4 File Offset: 0x00058DE4
		protected ADAuthenticationTrackingAuthority(TrackingAuthorityKind responsibleTracker, Uri uri) : base(responsibleTracker, uri)
		{
		}

		// Token: 0x04000D04 RID: 3332
		private static object initLock = new object();

		// Token: 0x04000D05 RID: 3333
		private static bool? canImpersonateNetworkService = null;
	}
}
