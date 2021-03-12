using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000716 RID: 1814
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FacebookAuthenticatorConfig
	{
		// Token: 0x0600223F RID: 8767 RVA: 0x000469CC File Offset: 0x00044BCC
		private FacebookAuthenticatorConfig()
		{
			this.Scope = FacebookAuthenticatorConfig.DefaultPermissions;
			this.WebRequestTimeout = FacebookAuthenticatorConfig.DefaultWebRequestTimeout;
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x000469EC File Offset: 0x00044BEC
		public static FacebookAuthenticatorConfig CreateForAppAuthorization(string appId, string redirectUri, string authorizationEndpoint, CultureInfo locale, DateTime readTimeUtc)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("appId", appId);
			ArgumentValidator.ThrowIfNullOrEmpty("redirectUri", redirectUri);
			ArgumentValidator.ThrowIfNullOrEmpty("authorizationEndpoint", authorizationEndpoint);
			ArgumentValidator.ThrowIfNull("locale", locale);
			return new FacebookAuthenticatorConfig
			{
				AppId = appId,
				RedirectUri = redirectUri,
				AuthorizationEndpoint = authorizationEndpoint,
				Locale = FacebookAuthenticatorConfig.GetFacebookLocaleName(locale),
				ReadTimeUtc = readTimeUtc
			};
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x00046A58 File Offset: 0x00044C58
		public static FacebookAuthenticatorConfig CreateForAppAuthentication(string appId, string appSecret, string redirectUri, string graphTokenEndpoint, IFacebookAuthenticationWebClient authClient, TimeSpan webRequestTimeout, DateTime readTimeUtc)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("appId", appId);
			ArgumentValidator.ThrowIfNullOrEmpty("appSecret", appSecret);
			ArgumentValidator.ThrowIfNullOrEmpty("redirectUri", redirectUri);
			ArgumentValidator.ThrowIfNullOrEmpty("graphTokenEndpoint", graphTokenEndpoint);
			ArgumentValidator.ThrowIfNull("authClient", authClient);
			FacebookAuthenticatorConfig facebookAuthenticatorConfig = new FacebookAuthenticatorConfig();
			facebookAuthenticatorConfig.AppId = appId;
			facebookAuthenticatorConfig.AppSecret = appSecret;
			facebookAuthenticatorConfig.RedirectUri = redirectUri;
			facebookAuthenticatorConfig.GraphTokenEndpoint = graphTokenEndpoint;
			facebookAuthenticatorConfig.AuthenticationClient = authClient;
			if (!TimeSpan.Zero.Equals(webRequestTimeout))
			{
				facebookAuthenticatorConfig.WebRequestTimeout = webRequestTimeout;
			}
			facebookAuthenticatorConfig.ReadTimeUtc = readTimeUtc;
			return facebookAuthenticatorConfig;
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x00046AE9 File Offset: 0x00044CE9
		// (set) Token: 0x06002243 RID: 8771 RVA: 0x00046AF1 File Offset: 0x00044CF1
		public string AuthorizationEndpoint { get; private set; }

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06002244 RID: 8772 RVA: 0x00046AFA File Offset: 0x00044CFA
		// (set) Token: 0x06002245 RID: 8773 RVA: 0x00046B02 File Offset: 0x00044D02
		public string GraphTokenEndpoint { get; private set; }

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x00046B0B File Offset: 0x00044D0B
		// (set) Token: 0x06002247 RID: 8775 RVA: 0x00046B13 File Offset: 0x00044D13
		public string AppId { get; private set; }

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x00046B1C File Offset: 0x00044D1C
		// (set) Token: 0x06002249 RID: 8777 RVA: 0x00046B24 File Offset: 0x00044D24
		public string AppSecret { get; private set; }

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x00046B2D File Offset: 0x00044D2D
		// (set) Token: 0x0600224B RID: 8779 RVA: 0x00046B35 File Offset: 0x00044D35
		public string Locale { get; private set; }

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x00046B3E File Offset: 0x00044D3E
		// (set) Token: 0x0600224D RID: 8781 RVA: 0x00046B46 File Offset: 0x00044D46
		public string RedirectUri { get; private set; }

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x00046B4F File Offset: 0x00044D4F
		// (set) Token: 0x0600224F RID: 8783 RVA: 0x00046B57 File Offset: 0x00044D57
		public string Scope { get; private set; }

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x00046B60 File Offset: 0x00044D60
		// (set) Token: 0x06002251 RID: 8785 RVA: 0x00046B68 File Offset: 0x00044D68
		public IFacebookAuthenticationWebClient AuthenticationClient { get; private set; }

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x00046B71 File Offset: 0x00044D71
		// (set) Token: 0x06002253 RID: 8787 RVA: 0x00046B79 File Offset: 0x00044D79
		public TimeSpan WebRequestTimeout { get; private set; }

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x00046B82 File Offset: 0x00044D82
		// (set) Token: 0x06002255 RID: 8789 RVA: 0x00046B8A File Offset: 0x00044D8A
		public DateTime ReadTimeUtc { get; private set; }

		// Token: 0x06002256 RID: 8790 RVA: 0x00046B94 File Offset: 0x00044D94
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{{ App id: {0};  Authorization endpoint: {1};  Graph token endpoint: {2};  Redirect URI: {3};  Web request timeout: {4}; Locale {5};  App secret hash: {6:X8};  Read time: {7:u} }}", new object[]
			{
				this.AppId,
				this.AuthorizationEndpoint,
				this.GraphTokenEndpoint,
				this.RedirectUri,
				this.WebRequestTimeout,
				this.Locale,
				this.GetAppSecretHashCode(),
				this.ReadTimeUtc
			});
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x00046C0F File Offset: 0x00044E0F
		private static string GetFacebookLocaleName(CultureInfo culture)
		{
			return culture.Name.Replace('-', '_');
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x00046C20 File Offset: 0x00044E20
		private int GetAppSecretHashCode()
		{
			if (this.AppSecret == null)
			{
				return 0;
			}
			return this.AppSecret.GetHashCode();
		}

		// Token: 0x040020C4 RID: 8388
		internal static readonly string DefaultPermissions = string.Join(",", new string[]
		{
			"offline_access",
			"user_about_me",
			"friends_about_me",
			"email",
			"user_activities",
			"friends_activities",
			"user_birthday",
			"friends_birthday",
			"user_education_history",
			"friends_education_history",
			"user_hometown",
			"friends_hometown",
			"user_interests",
			"friends_interests",
			"user_website",
			"friends_website",
			"user_work_history",
			"friends_work_history",
			"user_status",
			"friends_status",
			"user_photo_video_tags",
			"friends_photo_video_tags",
			"user_photos",
			"friends_photos",
			"user_videos",
			"friends_videos",
			"friends_location",
			"friends_interests"
		});

		// Token: 0x040020C5 RID: 8389
		private static readonly TimeSpan DefaultWebRequestTimeout = TimeSpan.FromSeconds(10.0);
	}
}
