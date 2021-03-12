using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Security.OAuth.OAuthProtocols;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x0200009D RID: 157
	internal sealed class ACSTokenBuildRequest
	{
		// Token: 0x0600053F RID: 1343 RVA: 0x0002A7FC File Offset: 0x000289FC
		public ACSTokenBuildRequest(X509Certificate2 signingKey, string localIssuerId, AuthServer authServer, string tenantId, string resource, string caller = null)
		{
			this.signingKey = signingKey;
			this.localIssuerId = localIssuerId;
			this.acsTokenIssuerMetadata = IssuerMetadata.Create(authServer);
			this.acsTokenIssuingEndpoint = authServer.TokenIssuingEndpoint;
			this.selfKey = string.Format("L:{0}-AS:{1}", this.localIssuerId, this.acsTokenIssuerMetadata);
			this.tenantId = tenantId;
			this.resource = resource;
			this.caller = caller;
			this.partnerKey = string.Format("T:{0}-R:{1}", this.tenantId.ToLowerInvariant(), this.resource);
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0002A8AB File Offset: 0x00028AAB
		public string SelfKey
		{
			get
			{
				return this.selfKey;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0002A8B3 File Offset: 0x00028AB3
		public string PartnerKey
		{
			get
			{
				return this.partnerKey;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0002A8BB File Offset: 0x00028ABB
		public TokenResult TokenResult
		{
			get
			{
				return this.tokenResult;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0002A8C3 File Offset: 0x00028AC3
		public string TenantId
		{
			get
			{
				return this.tenantId;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0002A8CB File Offset: 0x00028ACB
		public string Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0002A8D3 File Offset: 0x00028AD3
		public string Caller
		{
			get
			{
				return this.caller;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0002A8DB File Offset: 0x00028ADB
		public string ACSTokenIssuingEndpoint
		{
			get
			{
				return this.acsTokenIssuingEndpoint;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0002A8E3 File Offset: 0x00028AE3
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x0002A8F4 File Offset: 0x00028AF4
		public IOutboundTracer Tracer
		{
			get
			{
				return this.tracer ?? DefaultOutboundTracer.Instance;
			}
			set
			{
				this.tracer = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0002A8FD File Offset: 0x00028AFD
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x0002A905 File Offset: 0x00028B05
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

		// Token: 0x0600054B RID: 1355 RVA: 0x0002A910 File Offset: 0x00028B10
		internal void BuildToken(bool throwOnError = true)
		{
			this.tracer.LogInformation(this.GetHashCode(), "[ACSTokenBuildRequest:BuildToken] started", new object[0]);
			TokenResult actorTokenFromAuthServer = this.GetActorTokenFromAuthServer(throwOnError);
			if (actorTokenFromAuthServer != null)
			{
				this.tokenResult = actorTokenFromAuthServer;
			}
			this.tracer.LogInformation(this.GetHashCode(), "[ACSTokenBuildRequest:BuildToken] finished", new object[0]);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0002A968 File Offset: 0x00028B68
		internal void RefreshTokenIfNeed()
		{
			bool isRecentlyRefreshed = this.IsRecentlyRefreshed;
			this.tracer.LogInformation(this.GetHashCode(), "[ACSTokenBuildRequest:RefreshTokenIfNeed] the last time we tried to get the ACS token was {0}, it {1} recently tried.", new object[]
			{
				this.lastRefreshDateTime,
				isRecentlyRefreshed ? "is" : "is not"
			});
			if (!isRecentlyRefreshed)
			{
				lock (this.lockObj)
				{
					if (!this.IsRecentlyRefreshed)
					{
						this.lastRefreshDateTime = DateTime.UtcNow;
						this.tracer.LogInformation(this.GetHashCode(), "[ACSTokenBuildRequest:RefreshTokenIfNeed] started", new object[0]);
						this.BuildToken(false);
					}
				}
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0002AA20 File Offset: 0x00028C20
		private bool IsRecentlyRefreshed
		{
			get
			{
				return DateTime.UtcNow - this.lastRefreshDateTime < ACSTokenBuildRequest.ACSTokenRequestInterval;
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0002AA3C File Offset: 0x00028C3C
		internal TokenResult GetActorTokenFromAuthServer(bool throwOnError)
		{
			TokenResult tokenResult = null;
			string acsAudience = string.Format(CultureInfo.InvariantCulture, "{0}/{1}@{2}", new object[]
			{
				this.acsTokenIssuerMetadata.Id,
				new Uri(this.acsTokenIssuingEndpoint).Authority,
				this.tenantId
			});
			LocalTokenIssuer localTokenIssuer = new LocalTokenIssuer(this.signingKey ?? ConfigProvider.Instance.Configuration.SigningKey, new LocalTokenIssuerMetadata(this.localIssuerId, this.tenantId));
			TokenResult tokenForACS = localTokenIssuer.GetTokenForACS(this.tenantId, acsAudience);
			this.Tracer.LogInformation(this.GetHashCode(), "[ACSTokenBuildRequest:GetActorTokenFromAuthServer] Sending token request to '{0}' for the resource '{1}' with token: {2}", new object[]
			{
				this.acsTokenIssuingEndpoint,
				this.resource,
				tokenForACS
			});
			OAuth2AccessTokenRequest oauth2AccessTokenRequest = OAuth2MessageFactory.CreateAccessTokenRequestWithAssertion(tokenForACS.Token, this.resource);
			Stopwatch stopwatch = Stopwatch.StartNew();
			string text = string.Empty;
			string text2 = string.Empty;
			try
			{
				OAuthCommon.PerfCounters.NumberOfAuthServerTokenRequests.Increment();
				string text3 = oauth2AccessTokenRequest.ToString();
				WebRequest webRequest = WebRequest.Create(this.acsTokenIssuingEndpoint);
				webRequest.AuthenticationLevel = AuthenticationLevel.None;
				webRequest.ContentLength = (long)text3.Length;
				webRequest.ContentType = "application/x-www-form-urlencoded";
				webRequest.Method = "POST";
				webRequest.Timeout = (int)ACSTokenBuildRequest.ACSTokenRequestTimeout.TotalMilliseconds;
				Server localServer = LocalServerCache.LocalServer;
				if (localServer != null && localServer.InternetWebProxy != null)
				{
					webRequest.Proxy = new WebProxy(localServer.InternetWebProxy);
					this.Tracer.LogInformation(this.GetHashCode(), "Using custom InternetWebProxy {0}.", new object[]
					{
						localServer.InternetWebProxy
					});
				}
				if (this.clientRequestId != null)
				{
					webRequest.Headers["client-request-id"] = this.clientRequestId.Value.ToString();
					webRequest.Headers["return-client-request-id"] = bool.TrueString;
				}
				using (Stream stream = webRequest.EndGetRequestStream(webRequest.BeginGetRequestStream(null, null)))
				{
					using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.ASCII))
					{
						streamWriter.Write(text3);
					}
				}
				OAuth2AccessTokenResponse response = null;
				using (WebResponse response2 = webRequest.GetResponse())
				{
					this.Tracer.LogInformation(0, "[ACSTokenBuildRequest:GetActorTokenFromAuthServer] response headers was \n{0}", new object[]
					{
						response2.Headers
					});
					using (Stream responseStream = response2.GetResponseStream())
					{
						using (StreamReader streamReader = new StreamReader(responseStream))
						{
							response = (OAuth2MessageFactory.CreateFromEncodedResponse(streamReader) as OAuth2AccessTokenResponse);
						}
					}
				}
				tokenResult = new TokenResult(response);
				ACSTokenLifeTime.Instance.SetValue(tokenResult.RemainingTokenLifeTime);
			}
			catch (WebException ex)
			{
				string errorDescription = this.GetErrorDescription(ex);
				string text4 = ex.Status.ToString();
				Exception ex2 = null;
				if (!string.IsNullOrEmpty(errorDescription))
				{
					try
					{
						Dictionary<string, object> dictionary = errorDescription.DeserializeFromJson<Dictionary<string, object>>();
						if (dictionary != null)
						{
							object obj;
							text = ((dictionary.TryGetValue("error", out obj) && obj != null) ? obj.ToString() : text4);
							text2 = ((dictionary.TryGetValue("error_description", out obj) && obj != null) ? obj.ToString() : errorDescription);
						}
					}
					catch (ArgumentException ex3)
					{
						ex2 = ex3;
					}
					catch (InvalidOperationException ex4)
					{
						ex2 = ex4;
					}
					if (ex2 != null)
					{
						this.Tracer.LogInformation(this.GetHashCode(), "[ACSTokenBuildRequest:GetActorTokenFromAuthServer] fail to deserialize the ACS error string. Exception: {0}", new object[]
						{
							ex2
						});
					}
				}
				this.Tracer.LogError(this.GetHashCode(), "[ACSTokenBuildRequest:GetActorTokenFromAuthServer] Unable to get the token from auth server '{0}'. The request has token {1}, the error from ACS is {2}, the exception is {3}", new object[]
				{
					this.acsTokenIssuingEndpoint,
					tokenForACS,
					errorDescription,
					ex
				});
				if (ex.Status == WebExceptionStatus.Timeout)
				{
					OAuthCommon.PerfCounters.NumberOfAuthServerTimeoutTokenRequests.Increment();
				}
				if (ex.InnerException != null)
				{
					this.Tracer.LogError(this.GetHashCode(), "[ACSTokenBuildRequest:GetActorTokenFromAuthServer] the inner exception is {0}", new object[]
					{
						ex.InnerException
					});
				}
				if (throwOnError)
				{
					throw new OAuthTokenRequestFailedException(OAuthOutboundErrorCodes.UnableToGetTokenFromACS, new string[]
					{
						text,
						text2
					}, ex);
				}
			}
			finally
			{
				long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
				OAuthCommon.UpdateMovingAveragePerformanceCounter(OAuthCommon.PerfCounters.AverageAuthServerResponseTime, elapsedMilliseconds);
				ExTraceGlobals.OAuthTracer.TracePerformance<long>((long)this.GetHashCode(), "[ACSTokenBuildRequest:GetActorTokenFromAuthServer] Request token from ACS took {0} ms", elapsedMilliseconds);
				OutboundProtocolLog.BeginAppend(throwOnError ? "GetNewACSToken" : "RefreshACSToken", (tokenResult != null) ? "ok" : "fail", elapsedMilliseconds, this.caller, this.clientRequestId, this.acsTokenIssuingEndpoint, this.tenantId, this.resource, (tokenResult != null) ? null : OAuthOutboundErrorCodes.UnableToGetTokenFromACS.ToString(), (tokenResult != null) ? null : OAuthOutboundErrorsUtil.GetDescription(OAuthOutboundErrorCodes.UnableToGetTokenFromACS, new string[]
				{
					text,
					text2
				}), null, (this.tokenResult == null) ? TimeSpan.Zero : this.tokenResult.RemainingTokenLifeTime, tokenResult);
			}
			return tokenResult;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0002B030 File Offset: 0x00029230
		private string GetErrorDescription(WebException webException)
		{
			string result = string.Empty;
			WebResponse response = webException.Response;
			if (response != null)
			{
				if (response.Headers != null)
				{
					this.Tracer.LogInformation(0, "[ACSTokenBuildRequest:GetErrorDescription] response headers was\n{0}", new object[]
					{
						response.Headers
					});
				}
				try
				{
					using (Stream responseStream = response.GetResponseStream())
					{
						using (StreamReader streamReader = new StreamReader(responseStream))
						{
							result = streamReader.ReadToEnd();
						}
					}
				}
				catch (Exception ex)
				{
					this.Tracer.LogInformation(0, "[ACSTokenBuildRequest:GetErrorDescription] hit exception: {0}", new object[]
					{
						ex
					});
				}
			}
			return result;
		}

		// Token: 0x0400057D RID: 1405
		private static readonly TimeSpan ACSTokenRequestTimeout = TimeSpan.FromMinutes(2.0);

		// Token: 0x0400057E RID: 1406
		private static readonly TimeSpan ACSTokenRequestInterval = TimeSpan.FromMinutes(10.0);

		// Token: 0x0400057F RID: 1407
		private readonly object lockObj = new object();

		// Token: 0x04000580 RID: 1408
		private readonly string localIssuerId;

		// Token: 0x04000581 RID: 1409
		private readonly X509Certificate2 signingKey;

		// Token: 0x04000582 RID: 1410
		private readonly IssuerMetadata acsTokenIssuerMetadata;

		// Token: 0x04000583 RID: 1411
		private readonly string acsTokenIssuingEndpoint;

		// Token: 0x04000584 RID: 1412
		private readonly string tenantId;

		// Token: 0x04000585 RID: 1413
		private readonly string resource;

		// Token: 0x04000586 RID: 1414
		private readonly string caller;

		// Token: 0x04000587 RID: 1415
		private readonly string selfKey;

		// Token: 0x04000588 RID: 1416
		private readonly string partnerKey;

		// Token: 0x04000589 RID: 1417
		private IOutboundTracer tracer = DefaultOutboundTracer.Instance;

		// Token: 0x0400058A RID: 1418
		private Guid? clientRequestId;

		// Token: 0x0400058B RID: 1419
		private DateTime lastRefreshDateTime = DateTime.MinValue;

		// Token: 0x0400058C RID: 1420
		private TokenResult tokenResult;
	}
}
