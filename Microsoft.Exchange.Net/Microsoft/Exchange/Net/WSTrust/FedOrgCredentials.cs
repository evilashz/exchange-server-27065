using System;
using System.Text;
using Microsoft.Exchange.Common.Cache;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B69 RID: 2921
	internal sealed class FedOrgCredentials
	{
		// Token: 0x06003E80 RID: 16000 RVA: 0x000A30E4 File Offset: 0x000A12E4
		public FedOrgCredentials(DelegationTokenRequest request, SecurityTokenService tokenService)
		{
			if (request.Target == null || request.Offer == null || request.EmailAddress == null || request.FederatedIdentity == null)
			{
				throw new ArgumentException();
			}
			this.request = request;
			this.tokenService = tokenService;
			StringBuilder stringBuilder = new StringBuilder(this.request.Target.ToString().ToLowerInvariant());
			stringBuilder.Append(this.request.FederatedIdentity.Identity.ToLowerInvariant());
			stringBuilder.Append(this.request.EmailAddress.ToLowerInvariant());
			stringBuilder.Append(this.request.Offer.Name);
			if (this.request.Policy != null)
			{
				stringBuilder.Append(this.request.Policy.ToLowerInvariant());
			}
			this.cacheKey = stringBuilder.ToString();
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x000A31C0 File Offset: 0x000A13C0
		public RequestedToken GetToken()
		{
			FedOrgCredentials.CacheableRequestedToken cacheableRequestedToken = null;
			bool flag = false;
			if (!FedOrgCredentials.tokenCache.TryGetValue(this.cacheKey, out cacheableRequestedToken, out flag) || flag)
			{
				cacheableRequestedToken = this.GetTokenForTarget();
			}
			return cacheableRequestedToken.RequestedToken;
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x000A31F8 File Offset: 0x000A13F8
		private FedOrgCredentials.CacheableRequestedToken GetTokenForTarget()
		{
			FedOrgCredentials.CacheableRequestedToken cacheableRequestedToken = new FedOrgCredentials.CacheableRequestedToken(this.tokenService.IssueToken(this.request), this.request.Offer);
			FedOrgCredentials.tokenCache.TryAdd(this.cacheKey, cacheableRequestedToken);
			return cacheableRequestedToken;
		}

		// Token: 0x0400366A RID: 13930
		private static Cache<string, FedOrgCredentials.CacheableRequestedToken> tokenCache = new Cache<string, FedOrgCredentials.CacheableRequestedToken>(32L, TimeSpan.FromHours(8.0), TimeSpan.FromHours(0.0), TimeSpan.FromMinutes(1.0), null, null);

		// Token: 0x0400366B RID: 13931
		private DelegationTokenRequest request;

		// Token: 0x0400366C RID: 13932
		private SecurityTokenService tokenService;

		// Token: 0x0400366D RID: 13933
		private string cacheKey;

		// Token: 0x02000B6A RID: 2922
		private sealed class CacheableRequestedToken : CachableItem
		{
			// Token: 0x06003E84 RID: 16004 RVA: 0x000A3275 File Offset: 0x000A1475
			public CacheableRequestedToken(RequestedToken token, Offer offer)
			{
				this.token = token;
				this.offer = offer;
			}

			// Token: 0x17000F61 RID: 3937
			// (get) Token: 0x06003E85 RID: 16005 RVA: 0x000A328B File Offset: 0x000A148B
			public override long ItemSize
			{
				get
				{
					return 1L;
				}
			}

			// Token: 0x06003E86 RID: 16006 RVA: 0x000A3290 File Offset: 0x000A1490
			public override bool IsExpired(DateTime currentTime)
			{
				long num = currentTime.ToUniversalTime().Ticks - this.token.Lifetime.Created.ToUniversalTime().Ticks;
				long ticks = this.offer.Duration.Ticks;
				return (double)num / (double)ticks > 0.9;
			}

			// Token: 0x17000F62 RID: 3938
			// (get) Token: 0x06003E87 RID: 16007 RVA: 0x000A32F4 File Offset: 0x000A14F4
			public RequestedToken RequestedToken
			{
				get
				{
					return this.token;
				}
			}

			// Token: 0x0400366E RID: 13934
			private RequestedToken token;

			// Token: 0x0400366F RID: 13935
			private Offer offer;
		}
	}
}
