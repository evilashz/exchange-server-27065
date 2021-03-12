using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000715 RID: 1813
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FacebookAuthenticator
	{
		// Token: 0x06002234 RID: 8756 RVA: 0x00046444 File Offset: 0x00044644
		public FacebookAuthenticator(FacebookAuthenticatorConfig config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			this.config = config;
			this.hash = this.GetHashCode();
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x00046470 File Offset: 0x00044670
		public Uri GetAppAuthorizationUri()
		{
			UriBuilder uriBuilder = new UriBuilder(this.config.AuthorizationEndpoint)
			{
				Query = string.Format("client_id={0}&locale={1}&scope={2}&redirect_uri={3}", new object[]
				{
					this.config.AppId,
					this.config.Locale,
					this.config.Scope,
					HttpUtility.UrlEncode(this.config.RedirectUri)
				})
			};
			return uriBuilder.Uri;
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x000464EC File Offset: 0x000446EC
		public static AppAuthorizationResponse ParseAppAuthorizationResponse(NameValueCollection requestParameters)
		{
			ArgumentValidator.ThrowIfNull("requestParameters", requestParameters);
			return new AppAuthorizationResponse
			{
				AppAuthorizationCode = requestParameters["code"],
				Error = requestParameters["error"],
				ErrorDescription = requestParameters["error_description"],
				ErrorReason = requestParameters["error_reason"]
			};
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x0004654F File Offset: 0x0004474F
		public static bool IsRedirectFromFacebook(AppAuthorizationResponse response)
		{
			ArgumentValidator.ThrowIfNull("response", response);
			return !string.IsNullOrEmpty(response.AppAuthorizationCode) || !string.IsNullOrEmpty(response.Error);
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x0004657C File Offset: 0x0004477C
		public bool IsAuthorizationGranted(AppAuthorizationResponse response)
		{
			ArgumentValidator.ThrowIfNull("response", response);
			bool flag = !string.IsNullOrEmpty(response.AppAuthorizationCode) && string.IsNullOrEmpty(response.Error);
			if (!flag)
			{
				FacebookAuthenticator.Tracer.TraceDebug((long)this.hash, "Authorization denied. Code: {0}, Error: {1}, Description: {2}, Reason: {3}", new object[]
				{
					response.AppAuthorizationCode,
					response.Error,
					response.ErrorDescription,
					response.ErrorReason
				});
			}
			return flag;
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x000465F8 File Offset: 0x000447F8
		public string ExchangeAppAuthorizationCodeForAccessToken(string code)
		{
			if (string.IsNullOrEmpty(code))
			{
				throw new ArgumentNullException("code");
			}
			string result;
			try
			{
				Uri accessTokenEndpoint = this.BuildTokenEndpointUri(code);
				AuthenticateApplicationResponse authenticateApplicationResponse = this.AuthenticateApplication(accessTokenEndpoint);
				HttpStatusCode code2 = authenticateApplicationResponse.Code;
				if (code2 != HttpStatusCode.OK)
				{
					if (code2 != HttpStatusCode.BadRequest)
					{
						FacebookAuthenticator.Tracer.TraceError<HttpStatusCode, string>((long)this.hash, "AuthenticateApplication returned an unexpected status code.  Code: {0};  Body: {1}", authenticateApplicationResponse.Code, authenticateApplicationResponse.Body);
						throw new FacebookAuthenticationException(NetServerException.UnexpectedAppAuthenticationResponse(authenticateApplicationResponse.Code, authenticateApplicationResponse.Body, this.config));
					}
					FacebookAuthenticator.Tracer.TraceDebug<string, FacebookAuthenticatorConfig>((long)this.hash, "AuthenticateApplication returned BadRequest.  Body: {0}.  Configuration: {1}", authenticateApplicationResponse.Body, this.config);
					throw new FacebookAuthenticationException(NetServerException.InvalidAppAuthorizationCode(authenticateApplicationResponse.Body, this.config));
				}
				else
				{
					result = this.ExtractAccessToken(authenticateApplicationResponse.Body);
				}
			}
			catch (WebException ex)
			{
				if (FacebookAuthenticator.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					FacebookAuthenticator.Tracer.TraceError<WebException, string, FacebookAuthenticatorConfig>((long)this.hash, "ExchangeAppAuthorizationCodeForAccessToken: caught WebException {0}.  Response: {1}.  Configuration: {2}", ex, this.GetResponseBodyForTracing(ex), this.config);
				}
				throw new FacebookAuthenticationException(NetServerException.FailedToAuthenticateApp(this.config), ex);
			}
			catch (ProtocolViolationException ex2)
			{
				FacebookAuthenticator.Tracer.TraceError<ProtocolViolationException>((long)this.hash, "ExchangeAppAuthorizationCodeForAccessToken: caught ProtocolViolationException {0}", ex2);
				throw new FacebookAuthenticationException(NetServerException.FailedToAuthenticateApp(this.config), ex2);
			}
			catch (IOException ex3)
			{
				FacebookAuthenticator.Tracer.TraceError<IOException>((long)this.hash, "ExchangeAppAuthorizationCodeForAccessToken: caught IOException {0}", ex3);
				throw new FacebookAuthenticationException(NetServerException.FailedToAuthenticateApp(this.config), ex3);
			}
			return result;
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x00046794 File Offset: 0x00044994
		private string ExtractAccessToken(string authenticateAppResponse)
		{
			if (string.IsNullOrEmpty(authenticateAppResponse))
			{
				FacebookAuthenticator.Tracer.TraceError((long)this.hash, "ExtractAccessToken: authentication response is empty.");
				throw new FacebookAuthenticationException(NetServerException.EmptyAppAuthenticationResponse);
			}
			string text = HttpUtility.ParseQueryString(authenticateAppResponse)["access_token"];
			if (string.IsNullOrEmpty(text))
			{
				FacebookAuthenticator.Tracer.TraceError((long)this.hash, "ExtractAccessToken: authentication response does not contain an access token.");
				throw new FacebookAuthenticationException(NetServerException.InvalidAppAuthenticationResponse(authenticateAppResponse, this.config));
			}
			return text;
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x0004680C File Offset: 0x00044A0C
		private Uri BuildTokenEndpointUri(string code)
		{
			UriBuilder uriBuilder = new UriBuilder(this.config.GraphTokenEndpoint)
			{
				Query = string.Format("client_id={0}&redirect_uri={1}&client_secret={2}&code={3}", new object[]
				{
					this.config.AppId,
					HttpUtility.UrlEncode(this.config.RedirectUri),
					this.config.AppSecret,
					code
				})
			};
			return uriBuilder.Uri;
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x00046880 File Offset: 0x00044A80
		private AuthenticateApplicationResponse AuthenticateApplication(Uri accessTokenEndpoint)
		{
			AuthenticateApplicationResponse result;
			try
			{
				result = this.config.AuthenticationClient.AuthenticateApplication(accessTokenEndpoint, this.config.WebRequestTimeout);
			}
			catch (IOException arg)
			{
				FacebookAuthenticator.Tracer.TraceError<IOException, FacebookAuthenticatorConfig>((long)this.hash, "AuthenticateApplication: caught IOException.  Will retry.  Exception: {0}.  Configuration: {1}", arg, this.config);
				result = this.config.AuthenticationClient.AuthenticateApplication(accessTokenEndpoint, this.config.WebRequestTimeout);
			}
			return result;
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x000468FC File Offset: 0x00044AFC
		private string GetResponseBodyForTracing(WebException e)
		{
			if (e == null || e.Response == null)
			{
				return string.Empty;
			}
			string result;
			try
			{
				using (Stream responseStream = e.Response.GetResponseStream())
				{
					if (!responseStream.CanRead)
					{
						result = string.Empty;
					}
					else
					{
						char[] array = new char[1024];
						int num = 0;
						using (StreamReader streamReader = new StreamReader(responseStream))
						{
							num = streamReader.Read(array, 0, 1024);
						}
						if (num <= 0)
						{
							result = string.Empty;
						}
						else
						{
							result = new string(array, 0, num);
						}
					}
				}
			}
			catch (Exception ex)
			{
				result = ex.ToString();
			}
			return result;
		}

		// Token: 0x040020C1 RID: 8385
		private static readonly Trace Tracer = ExTraceGlobals.FacebookTracer;

		// Token: 0x040020C2 RID: 8386
		private readonly FacebookAuthenticatorConfig config;

		// Token: 0x040020C3 RID: 8387
		private readonly int hash;
	}
}
