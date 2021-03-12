using System;
using System.Diagnostics;
using System.Net;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Security;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000D3 RID: 211
	internal sealed class OAuthCredentials : ICredentials
	{
		// Token: 0x0600073E RID: 1854 RVA: 0x00033190 File Offset: 0x00031390
		static OAuthCredentials()
		{
			ExTraceGlobals.OAuthTracer.TraceFunction(0L, "[OAuthCredentials.cctor] Entering");
			AuthenticationManager.Register(new OAuthCredentials.OAuthAuthenticationModule());
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x000331F0 File Offset: 0x000313F0
		private OAuthCredentials(OrganizationId organizationId, string userDomain)
		{
			OAuthCommon.VerifyNonNullArgument("organizationId", organizationId);
			OAuthCommon.VerifyNonNullArgument("userDomain", userDomain);
			this.organizationId = organizationId;
			this.userDomain = userDomain;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00033258 File Offset: 0x00031458
		private OAuthCredentials(OrganizationId organizationId, ADUser actAsUser, string userDomain = null)
		{
			OAuthCommon.VerifyNonNullArgument("organizationId", organizationId);
			OAuthCommon.VerifyNonNullArgument("actAsUser", actAsUser);
			ExTraceGlobals.OAuthTracer.TraceDebug<ADUser, string>(0L, "[OAuthCredentials:ctor] actAsUser is {0}, userDomain is {1}", actAsUser, userDomain);
			this.organizationId = organizationId;
			this.userDomain = (userDomain ?? ((SmtpAddress)actAsUser[ADRecipientSchema.PrimarySmtpAddress]).Domain);
			this.adUser = actAsUser;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x000332F8 File Offset: 0x000314F8
		private OAuthCredentials(OrganizationId organizationId, MiniRecipient actAsUser, string userDomain = null)
		{
			OAuthCommon.VerifyNonNullArgument("organizationId", organizationId);
			OAuthCommon.VerifyNonNullArgument("actAsUser", actAsUser);
			ExTraceGlobals.OAuthTracer.TraceDebug<MiniRecipient, string>(0L, "[OAuthCredentials:ctor] actAsUser is {0}, userDomain is {1}", actAsUser, userDomain);
			this.organizationId = organizationId;
			SmtpAddress primarySmtpAddress = actAsUser.PrimarySmtpAddress;
			this.userDomain = (userDomain ?? primarySmtpAddress.Domain);
			this.miniRecipient = actAsUser;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0003338B File Offset: 0x0003158B
		public static OAuthCredentials GetOAuthCredentialsForAppToken(OrganizationId organizationId, string userDomain)
		{
			return new OAuthCredentials(organizationId, userDomain);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00033394 File Offset: 0x00031594
		public static OAuthCredentials GetOAuthCredentialsForAppActAsToken(OrganizationId organizationId, ADUser actAsUser, string userDomain = null)
		{
			return new OAuthCredentials(organizationId, actAsUser, userDomain);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0003339E File Offset: 0x0003159E
		public static OAuthCredentials GetOAuthCredentialsForAppActAsToken(OrganizationId organizationId, MiniRecipient actAsUser, string userDomain = null)
		{
			return new OAuthCredentials(organizationId, actAsUser, userDomain);
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x000333A8 File Offset: 0x000315A8
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x000333B0 File Offset: 0x000315B0
		public LocalConfiguration LocalConfiguration
		{
			get
			{
				return this.localConfiguration;
			}
			set
			{
				OAuthCommon.VerifyNonNullArgument("localConfiguration", value);
				this.localConfiguration = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x000333C5 File Offset: 0x000315C5
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x000333CD File Offset: 0x000315CD
		public IOutboundTracer Tracer
		{
			get
			{
				return this.tracer;
			}
			set
			{
				OAuthCommon.VerifyNonNullArgument("tracer", value);
				this.tracer = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x000333E2 File Offset: 0x000315E2
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x000333EA File Offset: 0x000315EA
		public Guid? ClientRequestId
		{
			get
			{
				return this.clientRequestId;
			}
			set
			{
				this.clientRequestId = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x000333F3 File Offset: 0x000315F3
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x0003340F File Offset: 0x0003160F
		public bool IncludeNameIdOnly
		{
			get
			{
				return this.includeNameIdOnly != null && this.includeNameIdOnly.Value;
			}
			set
			{
				if (this.includeNameIdOnly != null)
				{
					throw new InvalidOperationException("can not modify the value once set");
				}
				this.includeNameIdOnly = new bool?(value);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x00033435 File Offset: 0x00031635
		public string UserDomain
		{
			get
			{
				return this.userDomain;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0003343D File Offset: 0x0003163D
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x00033445 File Offset: 0x00031645
		public string Caller
		{
			get
			{
				return this.caller;
			}
			set
			{
				this.caller = value;
			}
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0003344E File Offset: 0x0003164E
		internal static void ClearCache()
		{
			OAuthCredentials.challengeCache.Clear();
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0003345C File Offset: 0x0003165C
		internal Authorization Authenticate(string challengeString, WebRequest webRequest, bool preAuthenticate)
		{
			this.Tracer.LogInformation(this.GetHashCode(), "[OAuthCredentials:Authenticate] entering", new object[0]);
			if (webRequest == null)
			{
				throw new ArgumentNullException("request");
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			Uri requestUri = webRequest.RequestUri;
			string text = requestUri.ToString();
			this.caller = webRequest.Headers[HttpRequestHeader.UserAgent];
			HttpAuthenticationChallenge httpAuthenticationChallenge = null;
			HttpAuthenticationChallenge httpAuthenticationChallenge2 = null;
			if (preAuthenticate)
			{
				this.Tracer.LogInformation(this.GetHashCode(), "[OAuthCredentials:Authenticate] trying to build the token for '{0}' as pre-auth is specified.", new object[]
				{
					requestUri
				});
				if (OAuthCredentials.challengeCache.TryGetValue(webRequest.RequestUri, out httpAuthenticationChallenge2))
				{
					this.Tracer.LogInformation(this.GetHashCode(), "[OAuthCredentials:Authenticate] the challenge for '{0}' found in the cache: {1}.", new object[]
					{
						text,
						httpAuthenticationChallenge2
					});
					httpAuthenticationChallenge = httpAuthenticationChallenge2;
				}
				else
				{
					this.Tracer.LogInformation(this.GetHashCode(), "[OAuthCredentials:Authenticate] the challenge for '{0}' not found in the cache.", new object[]
					{
						text
					});
				}
			}
			else
			{
				this.Tracer.LogInformation(this.GetHashCode(), "[OAuthCredentials:Authenticate] challenge from '{0}' received: {1} ", new object[]
				{
					text,
					challengeString
				});
				HttpAuthenticationResponseHeader httpAuthenticationResponseHeader = HttpAuthenticationResponseHeader.Parse(challengeString);
				httpAuthenticationChallenge = httpAuthenticationResponseHeader.FindFirstChallenge(Constants.BearerAuthenticationType);
				if (httpAuthenticationChallenge != null)
				{
					OAuthCredentials.challengeCache.InsertAbsolute(requestUri, httpAuthenticationChallenge, OAuthCredentials.ChallengeObjectAbsoluteExpiration, null);
				}
			}
			if (httpAuthenticationChallenge != null)
			{
				string authority = webRequest.RequestUri.Authority;
				lock (this.lockObj)
				{
					if (authority == this.lastRequestUriAuthority && this.localConfiguration == this.lastLocalConfiguration && httpAuthenticationChallenge.Equals(this.lastChallengeObject) && this.lastTokenResult != null)
					{
						TimeSpan remainingTokenLifeTime = this.lastTokenResult.RemainingTokenLifeTime;
						this.Tracer.LogInformation(this.GetHashCode(), "[OAuthCredentials:Authenticate] the last token remaining life time is : {0}", new object[]
						{
							remainingTokenLifeTime
						});
						bool flag2 = remainingTokenLifeTime > OAuthCredentials.RemainingLifetimeLimitToReuseLastToken;
						if (flag2)
						{
							this.Tracer.LogInformation(this.GetHashCode(), "[OAuthCredentials:Authenticate] re-use the last token: {0}", new object[]
							{
								this.lastTokenResult
							});
							this.Tracer.LogToken(this.GetHashCode(), this.lastTokenResult.ToString());
							OutboundProtocolLog.BeginAppend("SendCached", "ok", stopwatch.ElapsedMilliseconds, this.caller, this.clientRequestId, text, this.userDomain, null, null, null, null, remainingTokenLifeTime, this.lastTokenResult);
							return new Authorization(OAuthCommon.WriteAuthorizationHeader(this.lastTokenResult.TokenString), true);
						}
					}
				}
				TokenResult tokenResult = null;
				try
				{
					tokenResult = this.GetToken(webRequest, httpAuthenticationChallenge);
				}
				catch (OAuthTokenRequestFailedException ex)
				{
					string message = ex.Message;
					this.Tracer.LogError(this.GetHashCode(), "{0}", new object[]
					{
						message
					});
					OutboundProtocolLog.BeginAppend("SendNew", "fail", stopwatch.ElapsedMilliseconds, this.caller, this.clientRequestId, text, this.userDomain, null, ex.GetKeyForErrorCode(), message, null, TimeSpan.Zero, tokenResult);
					throw;
				}
				OutboundProtocolLog.BeginAppend("SendNew", "ok", stopwatch.ElapsedMilliseconds, this.caller, this.clientRequestId, text, this.userDomain, null, null, null, null, tokenResult.RemainingTokenLifeTime, tokenResult);
				lock (this.lockObj)
				{
					this.lastRequestUriAuthority = authority;
					this.lastChallengeObject = httpAuthenticationChallenge;
					this.lastLocalConfiguration = this.localConfiguration;
					this.lastTokenResult = tokenResult;
				}
				this.Tracer.LogInformation(this.GetHashCode(), "[OAuthCredentials:Authenticate] send request to '{0}' with the bearer token: '{1}'", new object[]
				{
					text,
					tokenResult
				});
				this.Tracer.LogToken(this.GetHashCode(), tokenResult.ToString());
				return new Authorization(OAuthCommon.WriteAuthorizationHeader(tokenResult.TokenString), true);
			}
			string text2 = webRequest.Headers[HttpRequestHeader.Authorization];
			if (!string.IsNullOrEmpty(text2) && text2.TrimStart(new char[0]).StartsWith(Constants.BearerAuthenticationType, StringComparison.OrdinalIgnoreCase))
			{
				this.Tracer.LogError(this.GetHashCode(), "[OAuthCredentials:Authenticate] the authorization header was '{0}', but no challenge returned from '{1}'. That url may not support OAuth", new object[]
				{
					text2,
					text
				});
				throw new OAuthTokenRequestFailedException(OAuthOutboundErrorCodes.InvalidOAuthEndpoint, requestUri.AbsoluteUri, null);
			}
			this.Tracer.LogInformation(this.GetHashCode(), "[OAuthCredentials:Authenticate] send requst to '{0}' with empty bearer token", new object[]
			{
				text
			});
			OutboundProtocolLog.BeginAppend("SendEmpty", "ok", stopwatch.ElapsedMilliseconds, this.caller, this.clientRequestId, text, this.userDomain, null, null, null, null, TimeSpan.Zero, null);
			return new Authorization(OAuthCommon.WriteAuthorizationHeader(string.Empty), false);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00033970 File Offset: 0x00031B70
		private TokenResult GetToken(WebRequest webRequest, HttpAuthenticationChallenge challengeObject)
		{
			string authority = webRequest.RequestUri.Authority;
			string clientId = challengeObject.ClientId;
			string realm = challengeObject.Realm;
			string trustedIssuers = challengeObject.TrustedIssuers;
			this.Tracer.LogInformation(this.GetHashCode(), "[OAuthCredentials:GetToken] client-id: '{0}', realm: '{1}', trusted_issuer: '{2}'", new object[]
			{
				clientId,
				realm,
				trustedIssuers
			});
			this.Tracer.LogInformation(this.GetHashCode(), "[OAuthCredentials:GetToken] start building a token for the user domain '{0}'", new object[]
			{
				this.userDomain
			});
			OAuthTokenBuilder oauthTokenBuilder = new OAuthTokenBuilder(this.organizationId, this.localConfiguration, this.caller);
			oauthTokenBuilder.Tracer = this.Tracer;
			oauthTokenBuilder.IncludeNameIdOnly = this.IncludeNameIdOnly;
			TokenResult result;
			if (this.adUser != null)
			{
				result = oauthTokenBuilder.GetAppWithUserToken(clientId, authority, realm, trustedIssuers, this.userDomain, this.adUser);
			}
			else if (this.miniRecipient != null)
			{
				result = oauthTokenBuilder.GetAppWithUserToken(clientId, authority, realm, trustedIssuers, this.userDomain, this.miniRecipient);
			}
			else
			{
				result = oauthTokenBuilder.GetAppToken(clientId, authority, realm, trustedIssuers, this.userDomain);
			}
			return result;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00033A88 File Offset: 0x00031C88
		public NetworkCredential GetCredential(Uri uri, string authType)
		{
			return null;
		}

		// Token: 0x040006A3 RID: 1699
		private static readonly TimeSpan ChallengeObjectAbsoluteExpiration = TimeSpan.FromMinutes(60.0);

		// Token: 0x040006A4 RID: 1700
		private static readonly TimeoutCache<Uri, HttpAuthenticationChallenge> challengeCache = new TimeoutCache<Uri, HttpAuthenticationChallenge>(2, 500, false);

		// Token: 0x040006A5 RID: 1701
		private static readonly TimeSpan RemainingLifetimeLimitToReuseLastToken = TimeSpan.FromHours(7.0);

		// Token: 0x040006A6 RID: 1702
		private readonly OrganizationId organizationId;

		// Token: 0x040006A7 RID: 1703
		private readonly ADUser adUser;

		// Token: 0x040006A8 RID: 1704
		private readonly MiniRecipient miniRecipient;

		// Token: 0x040006A9 RID: 1705
		private readonly string userDomain;

		// Token: 0x040006AA RID: 1706
		private IOutboundTracer tracer = DefaultOutboundTracer.Instance;

		// Token: 0x040006AB RID: 1707
		private LocalConfiguration localConfiguration;

		// Token: 0x040006AC RID: 1708
		private Guid? clientRequestId = null;

		// Token: 0x040006AD RID: 1709
		private bool? includeNameIdOnly = null;

		// Token: 0x040006AE RID: 1710
		private string caller;

		// Token: 0x040006AF RID: 1711
		private object lockObj = new object();

		// Token: 0x040006B0 RID: 1712
		private LocalConfiguration lastLocalConfiguration;

		// Token: 0x040006B1 RID: 1713
		private string lastRequestUriAuthority;

		// Token: 0x040006B2 RID: 1714
		private HttpAuthenticationChallenge lastChallengeObject;

		// Token: 0x040006B3 RID: 1715
		private TokenResult lastTokenResult;

		// Token: 0x020000D4 RID: 212
		public class OAuthAuthenticationModule : IAuthenticationModule
		{
			// Token: 0x1700018F RID: 399
			// (get) Token: 0x06000754 RID: 1876 RVA: 0x00033A8B File Offset: 0x00031C8B
			public string AuthenticationType
			{
				get
				{
					return Constants.BearerAuthenticationType;
				}
			}

			// Token: 0x17000190 RID: 400
			// (get) Token: 0x06000755 RID: 1877 RVA: 0x00033A92 File Offset: 0x00031C92
			public bool CanPreAuthenticate
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000756 RID: 1878 RVA: 0x00033A98 File Offset: 0x00031C98
			public Authorization Authenticate(string challenge, WebRequest request, ICredentials credentials)
			{
				ExTraceGlobals.OAuthTracer.TraceFunction((long)this.GetHashCode(), "[OAuthAuthenticationModule:Authenticate] Entering");
				OAuthCredentials oauthCredentials = credentials as OAuthCredentials;
				if (oauthCredentials == null)
				{
					ExTraceGlobals.OAuthTracer.TraceDebug<Type>((long)this.GetHashCode(), "[OAuthAuthenticationModule:Authenticate] Leaving since the credentials is of type {0}", credentials.GetType());
					return null;
				}
				return oauthCredentials.Authenticate(challenge, request, false);
			}

			// Token: 0x06000757 RID: 1879 RVA: 0x00033AEC File Offset: 0x00031CEC
			public Authorization PreAuthenticate(WebRequest request, ICredentials credentials)
			{
				ExTraceGlobals.OAuthTracer.TraceFunction((long)this.GetHashCode(), "[OAuthAuthenticationModule:PreAuthenticate] Entering");
				OAuthCredentials oauthCredentials = credentials as OAuthCredentials;
				if (oauthCredentials == null)
				{
					ExTraceGlobals.OAuthTracer.TraceDebug<Type>((long)this.GetHashCode(), "[OAuthAuthenticationModule:PreAuthenticate] Leaving since the credentials is of type {0}", credentials.GetType());
					return null;
				}
				return oauthCredentials.Authenticate(null, request, true);
			}
		}
	}
}
