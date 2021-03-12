using System;
using System.Diagnostics;
using System.IdentityModel.Tokens;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000D9 RID: 217
	public class OAuthHttpModule : IHttpModule
	{
		// Token: 0x06000763 RID: 1891 RVA: 0x00033D1C File Offset: 0x00031F1C
		static OAuthHttpModule()
		{
			ConfigProvider.Instance.AutoRefresh = true;
			ConfigProvider.Instance.LoadTrustedIssuers = true;
			StringAppSettingsEntry stringAppSettingsEntry = new StringAppSettingsEntry("OAuthHttpModule.EnableBEAuthVersion", string.Empty, ExTraceGlobals.AuthenticationTracer);
			OAuthHttpModule.shouldAttemptPreAuth = !string.IsNullOrEmpty(stringAppSettingsEntry.Value);
			BoolAppSettingsEntry boolAppSettingsEntry = new BoolAppSettingsEntry("OAuthHttpModule.RunningAtBackend", false, ExTraceGlobals.AuthenticationTracer);
			OAuthHttpModule.runningAtBackend = boolAppSettingsEntry.Value;
			BoolAppSettingsEntry boolAppSettingsEntry2 = new BoolAppSettingsEntry("OAuthHttpModule.AddToHttpContextItems", false, ExTraceGlobals.AuthenticationTracer);
			OAuthHttpModule.shouldAddCatToHttpContextItems = boolAppSettingsEntry2.Value;
			BoolAppSettingsEntry boolAppSettingsEntry3 = new BoolAppSettingsEntry("OAuthHttpModule.AddToRequestHeaders", false, ExTraceGlobals.AuthenticationTracer);
			OAuthHttpModule.shouldAddCatToRequestHeaders = boolAppSettingsEntry3.Value;
			StringAppSettingsEntry stringAppSettingsEntry2 = new StringAppSettingsEntry("OAuthHttpModule.WebAppAuthEnabled", string.Empty, ExTraceGlobals.AuthenticationTracer);
			OAuthHttpModule.webAppAuthEnabled = !string.IsNullOrEmpty(stringAppSettingsEntry2.Value);
			OAuthHttpModule.oAuthExtensionsManager = new OAuthExtensionsManager();
			if (OAuthHttpModule.webAppAuthEnabled)
			{
				OAuthHttpModule.oAuthExtensionsManager.AppendHandlerToChain(new OAuthCookieExtensionAuthenticationHandler());
				OAuthHttpModule.oAuthExtensionsManager.AppendHandlerToChain(new OAuthOwaExtensionAuthenticationHandler());
			}
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00033E14 File Offset: 0x00032014
		public void Init(HttpApplication application)
		{
			application.AuthenticateRequest += this.OnAuthenticateRequest;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00033E28 File Offset: 0x00032028
		public void Dispose()
		{
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x00033E5B File Offset: 0x0003205B
		internal static Lazy<bool> IsModuleLoaded
		{
			get
			{
				return new Lazy<bool>(delegate()
				{
					HttpApplication applicationInstance = HttpContext.Current.ApplicationInstance;
					return applicationInstance.Modules["OAuthAuthModule"] != null;
				});
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00033E80 File Offset: 0x00032080
		private void OnAuthenticateRequest(object source, EventArgs args)
		{
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			if (context.Request.IsAuthenticated)
			{
				return;
			}
			this.InternalOnAuthenticate(context);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00033EB0 File Offset: 0x000320B0
		private void InternalOnAuthenticate(HttpContext context)
		{
			ExTraceGlobals.OAuthTracer.TraceFunction((long)this.GetHashCode(), "[OAuthHttpModule::InternalOnAuthenticate] Entering");
			int num = 0;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<int>(2177248573U, ref num);
			if (num != 0)
			{
				Thread.Sleep(num);
			}
			OAuthExtensionContext context2 = new OAuthExtensionContext
			{
				HttpContext = context
			};
			bool flag = true;
			OAuthHttpModule.oAuthExtensionsManager.TryHandleRequestPreAuthentication(context2, out flag);
			if (!flag)
			{
				return;
			}
			string text = context.Request.Headers["Authorization"];
			if (string.IsNullOrEmpty(text))
			{
				OAuthHttpModule.oAuthExtensionsManager.TryGetBearerToken(context2, out text);
				if (string.IsNullOrEmpty(text))
				{
					return;
				}
			}
			text = text.Trim();
			if (!text.StartsWith(Constants.BearerAuthenticationType, StringComparison.OrdinalIgnoreCase))
			{
				return;
			}
			this.rawTokenOrSerializedIdentity = text.Substring(Constants.BearerAuthenticationType.Length).Trim();
			context.Items["AuthType"] = Constants.BearerAuthenticationType;
			if (string.IsNullOrEmpty(this.rawTokenOrSerializedIdentity))
			{
				string text2 = context.Request.Headers["X-User-Identity"];
				if (!string.IsNullOrEmpty(text2))
				{
					text2 = text2.Trim();
					SmtpAddress smtpAddress = new SmtpAddress(text2);
					if (smtpAddress.IsValidAddress && this.ShouldReturnFlightingNotEnabledHint(smtpAddress.Domain))
					{
						this.CompleteRequestOnError(context, HttpStatusCode.Unauthorized, OAuthErrorCategory.OAuthNotAvailable, string.Format(OAuthErrorsUtil.GetDescription(OAuthErrors.FlightingNotEnabled), smtpAddress.Domain), null, null);
						return;
					}
				}
				context.Response.StatusCode = 401;
				context.Items[Constants.RequestCompletedHttpContextKeyName] = true;
				context.Response.AppendHeader(Constants.WWWAuthenticateHeader, this.GetChallengeResponseValue());
				context.ApplicationInstance.CompleteRequest();
				return;
			}
			if (FaultInjection.TraceTest<bool>((FaultInjection.LIDs)2697342269U))
			{
				context.Items["OAuthToken"] = this.rawTokenOrSerializedIdentity;
				OAuthIdentity identity = OAuthIdentitySerializer.DeserializeOAuthIdentity(this.rawTokenOrSerializedIdentity);
				CompositeIdentityBuilder.AddIdentity(context, identity, AuthenticationAuthority.ORGID);
				context.User = new GenericPrincipal(identity, null);
				return;
			}
			OAuthCommon.PerfCounters.NumberOfAuthRequests.Increment();
			OAuthErrorCategory errorCategory = OAuthErrorCategory.InternalError;
			string errorDescription = string.Empty;
			if (ConfigProvider.Instance.Configuration == null)
			{
				errorDescription = "OAuth configuration not loaded.";
				this.CompleteRequestOnError(context, HttpStatusCode.InternalServerError, errorCategory, errorDescription, null, null);
				return;
			}
			string text3 = null;
			Exception ex = null;
			try
			{
				Uri targetUri = context.Request.Url;
				if (OAuthHttpModule.runningAtBackend)
				{
					targetUri = new Uri(context.Request.Headers[WellKnownHeader.MsExchProxyUri]);
				}
				this.handler = OAuthTokenHandler.CreateTokenHandler(this.rawTokenOrSerializedIdentity, targetUri, out text3);
			}
			catch (InvalidOAuthTokenException ex2)
			{
				ex = ex2;
				errorCategory = ex2.ErrorCategory;
				errorDescription = ex2.Message;
			}
			catch (Exception ex3)
			{
				ex = ex3;
				errorCategory = OAuthErrorCategory.InvalidToken;
				errorDescription = string.Format(OAuthErrorsUtil.GetDescription(OAuthErrors.UnableToReadToken), ex3.Message);
			}
			finally
			{
				if (text3 != null)
				{
					context.Items["OAuthToken"] = Convert.ToBase64String(Encoding.UTF8.GetBytes(text3));
				}
			}
			if (ex != null)
			{
				OAuthCommon.PerfCounters.NumberOfFailedAuthRequests.Increment();
				ExTraceGlobals.OAuthTracer.TraceWarning<string, Exception>((long)this.GetHashCode(), "[OAuthHttpModule::InternalOnAuthenticate] creating token handler for the token {0}, hits exception: {1}", text3, ex);
				this.CompleteRequestOnError(context, HttpStatusCode.Unauthorized, errorCategory, errorDescription, ex.ToString(), null);
				return;
			}
			if (OAuthHttpModule.runningAtBackend || !OAuthHttpModule.shouldAttemptPreAuth || !this.TryPreAuth(context))
			{
				this.DoFullAuth(context);
			}
			ExTraceGlobals.OAuthTracer.TraceFunction((long)this.GetHashCode(), "[OAuthHttpModule::InternalOnAuthenticate] Exiting");
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00034240 File Offset: 0x00032440
		private void CompleteRequestOnError(HttpContext context, HttpStatusCode statusCode, OAuthErrorCategory errorCategory, string errorDescription, string oauthError = null, string extraLoggingInfo = null)
		{
			context.Response.StatusCode = (int)statusCode;
			context.Items["OAuthErrorCategory"] = errorCategory.ToString();
			if (oauthError == null)
			{
				oauthError = errorDescription;
			}
			string text = context.Items["OAuthError"] as string;
			if (!string.IsNullOrEmpty(text))
			{
				oauthError = text + oauthError;
			}
			context.Items["OAuthError"] = oauthError;
			if (!string.IsNullOrEmpty(extraLoggingInfo))
			{
				context.Items["OAuthExtraInfo"] = extraLoggingInfo;
			}
			string value = string.Format("{0}, {1}=\"invalid_token\"", this.GetChallengeResponseValue(), Constants.ChallengeTokens.Error);
			string value2 = MSDiagnosticsHeader.GenerateDiagnosticsString(errorCategory, errorDescription);
			context.Response.AppendHeader(MSDiagnosticsHeader.HeaderName, value2);
			context.Response.AppendHeader(Constants.WWWAuthenticateHeader, value);
			context.Items[Constants.RequestCompletedHttpContextKeyName] = true;
			context.ApplicationInstance.CompleteRequest();
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00034334 File Offset: 0x00032534
		private bool TryPreAuth(HttpContext context)
		{
			context.Items["AuthType"] = Constants.BearerPreAuthenticationType;
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				OAuthPreAuthIdentity preAuthIdentity = this.handler.GetPreAuthIdentity();
				if (preAuthIdentity != null)
				{
					context.User = new GenericPrincipal(preAuthIdentity, null);
					context.Items["OAuthHttpModuleThisKey"] = this;
					return true;
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.OAuthTracer.TraceWarning<Exception>((long)this.GetHashCode(), "[OAuthHttpModule::TryPreAuth] hits exception: {0}", ex);
				context.Items["OAuthError"] = ex.ToString();
			}
			finally
			{
				context.Items["AuthModuleLatency"] = stopwatch.ElapsedMilliseconds;
			}
			return false;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00034400 File Offset: 0x00032600
		private void DoFullAuth(HttpContext context)
		{
			context.Items["AuthType"] = Constants.BearerAuthenticationType;
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				OAuthIdentity oauthIdentity = null;
				IIdentity identity = null;
				Exception ex = null;
				HttpStatusCode statusCode = HttpStatusCode.Unauthorized;
				OAuthErrorCategory errorCategory = OAuthErrorCategory.InternalError;
				string text = string.Empty;
				bool flag = false;
				string text2 = string.Empty;
				string text3 = string.Empty;
				string text4 = this.rawTokenOrSerializedIdentity;
				try
				{
					oauthIdentity = this.handler.GetOAuthIdentity();
					if (!oauthIdentity.IsAppOnly && OAuthHttpModule.runningAtBackend)
					{
						oauthIdentity.ActAsUser.EnsureUserIsVerified();
					}
					oauthIdentity.IsAuthenticatedAtBackend = OAuthHttpModule.runningAtBackend;
					identity = oauthIdentity;
					if (OAuthHttpModule.runningAtBackend)
					{
						if (OAuthHttpModule.shouldAddCatToHttpContextItems || OAuthHttpModule.shouldAddCatToRequestHeaders)
						{
							CommonAccessToken commonAccessToken = oauthIdentity.ToCommonAccessTokenVersion2();
							if (OAuthHttpModule.shouldAddCatToHttpContextItems)
							{
								context.Items["Item-CommonAccessToken"] = commonAccessToken;
							}
							if (OAuthHttpModule.shouldAddCatToRequestHeaders)
							{
								context.Request.Headers["X-CommonAccessToken"] = commonAccessToken.Serialize();
							}
						}
						else
						{
							identity = oauthIdentity.ConvertIdentityIfNeed();
						}
					}
				}
				catch (NullReferenceException ex2)
				{
					errorCategory = OAuthErrorCategory.InvalidToken;
					ex = ex2;
				}
				catch (ArgumentException ex3)
				{
					errorCategory = OAuthErrorCategory.InvalidToken;
					ex = ex3;
				}
				catch (InvalidOperationException ex4)
				{
					ex = ex4;
				}
				catch (SecurityTokenValidationException ex5)
				{
					text = ex5.Message;
					ex = ex5;
					if (text.StartsWith("Jwt10305", StringComparison.OrdinalIgnoreCase) || text.StartsWith("Jwt10306", StringComparison.OrdinalIgnoreCase))
					{
						errorCategory = OAuthErrorCategory.TokenExpired;
						text = OAuthErrorsUtil.GetDescription(OAuthErrors.TokenExpired);
					}
					else
					{
						errorCategory = OAuthErrorCategory.InvalidToken;
					}
				}
				catch (SecurityTokenException ex6)
				{
					errorCategory = OAuthErrorCategory.InvalidToken;
					text = ex6.Message;
					ex = ex6;
				}
				catch (InvalidOAuthTokenException ex7)
				{
					errorCategory = ex7.ErrorCategory;
					text = ex7.Message;
					ex = ex7;
					flag = ex7.LogEvent;
					text2 = ex7.LogPeriodicKey;
					text3 = ex7.ExtraData;
				}
				catch (OAuthIdentityDeserializationException ex8)
				{
					ex = ex8;
				}
				catch (InvalidOAuthLinkedAccountException ex9)
				{
					statusCode = HttpStatusCode.Forbidden;
					errorCategory = OAuthErrorCategory.InvalidToken;
					text = ex9.Message;
					ex = ex9;
				}
				catch (LocalizedException ex10)
				{
					statusCode = HttpStatusCode.InternalServerError;
					text = ex10.LocalizedString.ToString();
					ex = ex10;
				}
				finally
				{
					long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
					OAuthCommon.UpdateMovingAveragePerformanceCounter(OAuthCommon.PerfCounters.AverageResponseTime, elapsedMilliseconds);
					ExTraceGlobals.OAuthTracer.TracePerformance<long>((long)this.GetHashCode(), "[OAuthHttpModule::OnAuthenticateRequest] GetOAuthIdentity took {0} ms", elapsedMilliseconds);
				}
				string text5 = null;
				if (this.handler != null)
				{
					text5 = this.handler.GetExtraLoggingInfo();
				}
				if (ex != null)
				{
					OAuthCommon.PerfCounters.NumberOfFailedAuthRequests.Increment();
					ExTraceGlobals.OAuthTracer.TraceWarning<string, Exception>((long)this.GetHashCode(), "[OAuthHttpModule::OnAuthenticateRequest] validating the token {0}, hits exception: {1}", text4, ex);
					if (flag)
					{
						OAuthCommon.EventLogger.LogEvent(SecurityEventLogConstants.Tuple_OAuthFailToAuthenticateToken, text2 ?? string.Empty, new object[]
						{
							text4,
							context.Request.Headers["client-request-id"],
							ex
						});
					}
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(ex);
					if (!string.IsNullOrEmpty(text3))
					{
						stringBuilder.AppendFormat("Extra:{0}", text3);
					}
					if (!(ex is InvalidOAuthTokenException))
					{
						text5 = text5 + "ErrorCode:" + ex.GetType().Name;
					}
					this.CompleteRequestOnError(context, statusCode, errorCategory, text, stringBuilder.ToString(), text5);
				}
				else
				{
					ExTraceGlobals.OAuthTracer.TraceDebug<string>((long)this.GetHashCode(), "[OAuthHttpModule::OnAuthenticateRequest] resolved identity is : {0}", oauthIdentity.OAuthApplication.Id);
					context.Items["OAuthExtraInfo"] = text5;
					CompositeIdentityBuilder.AddIdentity(context, identity as GenericIdentity, this.handler.AuthenticationAuthority);
					context.User = new GenericPrincipal(identity, null);
					OAuthExtensionContext context2 = new OAuthExtensionContext
					{
						HttpContext = context,
						TokenHandler = this.handler
					};
					OAuthHttpModule.oAuthExtensionsManager.TryHandleRequestPostAuthentication(context2);
				}
			}
			finally
			{
				context.Items["AuthModuleLatency"] = stopwatch.ElapsedMilliseconds;
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000348E0 File Offset: 0x00032AE0
		internal static void ContinueOnAuthenticate(object source, AsyncCallback callback)
		{
			HttpApplication httpApplication = (HttpApplication)source;
			OAuthHttpModule oauthHttpModule = (OAuthHttpModule)httpApplication.Context.Items["OAuthHttpModuleThisKey"];
			if (oauthHttpModule == null)
			{
				return;
			}
			httpApplication.Context.Items.Remove("OAuthHttpModuleThisKey");
			oauthHttpModule.DoFullAuth(httpApplication.Context);
			callback(null);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0003493B File Offset: 0x00032B3B
		private string GetChallengeResponseValue()
		{
			return ConfigProvider.Instance.Configuration.ChallengeResponseStringWithClientProfileEnabled;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0003494C File Offset: 0x00032B4C
		private bool ShouldReturnFlightingNotEnabledHint(string domain)
		{
			if (!AuthCommon.IsMultiTenancyEnabled)
			{
				return this.ShouldReturnFlightingNotEnabledHintViaAD(domain);
			}
			if (GlsMServDirectorySession.GlsLookupMode != GlsLookupMode.BothGLSAndMServ && GlsMServDirectorySession.GlsLookupMode != GlsLookupMode.GlsOnly)
			{
				return false;
			}
			Exception ex = null;
			try
			{
				IGlobalDirectorySession globalSession = DirectorySessionFactory.GetGlobalSession(null);
				bool flag;
				if (globalSession.TryGetDomainFlag(domain, GlsDomainFlags.OAuth2ClientProfileEnabled, out flag))
				{
					return !flag;
				}
			}
			catch (MServTransientException ex2)
			{
				ex = ex2;
			}
			catch (MServPermanentException ex3)
			{
				ex = ex3;
			}
			catch (GlsTransientException ex4)
			{
				ex = ex4;
			}
			catch (GlsPermanentException ex5)
			{
				ex = ex5;
			}
			catch (GlsTenantNotFoundException ex6)
			{
				ex = ex6;
			}
			catch (InvalidOperationException ex7)
			{
				ex = ex7;
			}
			catch (TransientException ex8)
			{
				ex = ex8;
			}
			if (ex != null)
			{
				ExTraceGlobals.OAuthTracer.TraceWarning<string, Exception>((long)this.GetHashCode(), "[ReturnOAuth2ClientProfileStatusForThisUser] hitting exception for domain {0}: {1}", domain, ex);
			}
			return false;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00034A74 File Offset: 0x00032C74
		private bool ShouldReturnFlightingNotEnabledHintViaAD(string domain)
		{
			OrganizationId organizationId = OAuthCommon.ResolveOrganizationByDomain(domain);
			if (organizationId == null)
			{
				return false;
			}
			IConfigurationSession configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 757, "ShouldReturnFlightingNotEnabledHintViaAD", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\OAuth\\OAuthHttpModule.cs");
			ADOrganizationConfig orgConfig = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ADObjectId orgContainerId = configurationSession.GetOrgContainerId();
				orgConfig = configurationSession.Read<ADOrganizationConfig>(orgContainerId);
			});
			return adoperationResult == ADOperationResult.Success && orgConfig != null && !orgConfig.OAuth2ClientProfileEnabled;
		}

		// Token: 0x04000709 RID: 1801
		internal const string AuthModuleName = "OAuthAuthModule";

		// Token: 0x0400070A RID: 1802
		private const string XUserIdentity = "X-User-Identity";

		// Token: 0x0400070B RID: 1803
		private const string OAuthHttpModuleThisKey = "OAuthHttpModuleThisKey";

		// Token: 0x0400070C RID: 1804
		private static bool shouldAttemptPreAuth;

		// Token: 0x0400070D RID: 1805
		private static bool runningAtBackend;

		// Token: 0x0400070E RID: 1806
		private static bool shouldAddCatToHttpContextItems;

		// Token: 0x0400070F RID: 1807
		private static bool shouldAddCatToRequestHeaders;

		// Token: 0x04000710 RID: 1808
		private static bool webAppAuthEnabled;

		// Token: 0x04000711 RID: 1809
		private static OAuthExtensionsManager oAuthExtensionsManager;

		// Token: 0x04000712 RID: 1810
		private string rawTokenOrSerializedIdentity;

		// Token: 0x04000713 RID: 1811
		private OAuthTokenHandler handler;
	}
}
