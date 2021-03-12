using System;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.LinkedIn
{
	// Token: 0x02000749 RID: 1865
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LinkedInConfig
	{
		// Token: 0x06002458 RID: 9304 RVA: 0x0004C28C File Offset: 0x0004A48C
		private LinkedInConfig(string appId, string appSecret, string requestTokenEndpoint, string accessTokenEndpoint, string connectionsEndpoint, TimeSpan webRequestTimeout, string webProxyUri, DateTime readTimeUtc)
		{
			this.AppId = appId;
			this.AppSecret = appSecret;
			this.RequestTokenEndpoint = requestTokenEndpoint;
			this.AccessTokenEndpoint = accessTokenEndpoint;
			this.ConnectionsEndpoint = connectionsEndpoint;
			this.WebRequestTimeout = (TimeSpan.Zero.Equals(webRequestTimeout) ? LinkedInConfig.DefaultWebRequestTimeout : webRequestTimeout);
			this.WebProxy = (string.IsNullOrWhiteSpace(webProxyUri) ? null : new WebProxy(webProxyUri));
			this.ReadTimeUtc = readTimeUtc;
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x0004C308 File Offset: 0x0004A508
		public static LinkedInConfig CreateForAppAuth(string appId, string appSecret, string requestTokenEndpoint, string accessTokenEndpoint, TimeSpan webRequestTimeout, string webProxyUri, DateTime readTimeUtc)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("appId", appId);
			ArgumentValidator.ThrowIfNullOrEmpty("appSecret", appSecret);
			ArgumentValidator.ThrowIfNullOrEmpty("requestTokenEndpoint", requestTokenEndpoint);
			ArgumentValidator.ThrowIfNullOrEmpty("accessTokenEndpoint", accessTokenEndpoint);
			return new LinkedInConfig(appId, appSecret, requestTokenEndpoint, accessTokenEndpoint, string.Empty, webRequestTimeout, webProxyUri, readTimeUtc);
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x0600245A RID: 9306 RVA: 0x0004C355 File Offset: 0x0004A555
		// (set) Token: 0x0600245B RID: 9307 RVA: 0x0004C35D File Offset: 0x0004A55D
		public string RequestTokenEndpoint { get; private set; }

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x0600245C RID: 9308 RVA: 0x0004C366 File Offset: 0x0004A566
		// (set) Token: 0x0600245D RID: 9309 RVA: 0x0004C36E File Offset: 0x0004A56E
		public string AccessTokenEndpoint { get; private set; }

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x0600245E RID: 9310 RVA: 0x0004C377 File Offset: 0x0004A577
		// (set) Token: 0x0600245F RID: 9311 RVA: 0x0004C37F File Offset: 0x0004A57F
		public string ConnectionsEndpoint { get; private set; }

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06002460 RID: 9312 RVA: 0x0004C388 File Offset: 0x0004A588
		// (set) Token: 0x06002461 RID: 9313 RVA: 0x0004C390 File Offset: 0x0004A590
		public string AppId { get; private set; }

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06002462 RID: 9314 RVA: 0x0004C399 File Offset: 0x0004A599
		// (set) Token: 0x06002463 RID: 9315 RVA: 0x0004C3A1 File Offset: 0x0004A5A1
		public string AppSecret { get; private set; }

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06002464 RID: 9316 RVA: 0x0004C3AA File Offset: 0x0004A5AA
		// (set) Token: 0x06002465 RID: 9317 RVA: 0x0004C3B2 File Offset: 0x0004A5B2
		public IWebProxy WebProxy { get; private set; }

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06002466 RID: 9318 RVA: 0x0004C3BB File Offset: 0x0004A5BB
		// (set) Token: 0x06002467 RID: 9319 RVA: 0x0004C3C3 File Offset: 0x0004A5C3
		public TimeSpan WebRequestTimeout { get; private set; }

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06002468 RID: 9320 RVA: 0x0004C3CC File Offset: 0x0004A5CC
		// (set) Token: 0x06002469 RID: 9321 RVA: 0x0004C3D4 File Offset: 0x0004A5D4
		public DateTime ReadTimeUtc { get; private set; }

		// Token: 0x0600246A RID: 9322 RVA: 0x0004C3E0 File Offset: 0x0004A5E0
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{{ App id: {0};  Access token endpoint: {1};  Request token endpoint: {2};  Connections endpoint: {3};  Web request timeout: {4};  App secret hash: {5:X8};  Read time: {6:u} }}", new object[]
			{
				this.AppId,
				this.AccessTokenEndpoint,
				this.RequestTokenEndpoint,
				this.ConnectionsEndpoint,
				this.WebRequestTimeout,
				this.GetAppSecretHashCode(),
				this.ReadTimeUtc
			});
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x0004C452 File Offset: 0x0004A652
		private int GetAppSecretHashCode()
		{
			if (this.AppSecret == null)
			{
				return 0;
			}
			return this.AppSecret.GetHashCode();
		}

		// Token: 0x04002217 RID: 8727
		private static readonly TimeSpan DefaultWebRequestTimeout = TimeSpan.FromSeconds(10.0);
	}
}
