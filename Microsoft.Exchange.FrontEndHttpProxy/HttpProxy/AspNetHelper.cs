using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000081 RID: 129
	internal static class AspNetHelper
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x000176F0 File Offset: 0x000158F0
		public static void EndResponse(HttpContext httpContext, HttpStatusCode statusCode)
		{
			ExTraceGlobals.VerboseTracer.TraceDebug<int>(0L, "[AspNetHelper::EndResponse]: statusCode={0}", (int)statusCode);
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			httpContext.Response.StatusCode = (int)statusCode;
			try
			{
				httpContext.Response.Flush();
				httpContext.ApplicationInstance.CompleteRequest();
			}
			catch (HttpException arg)
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<HttpException>(0L, "Failed to flush and send response to client. {0}", arg);
			}
			httpContext.Response.End();
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00017774 File Offset: 0x00015974
		public static CommonAccessToken FixupCommonAccessToken(HttpContext httpContext, int targetVersion)
		{
			if (!httpContext.Request.IsAuthenticated)
			{
				return null;
			}
			CommonAccessToken commonAccessToken = null;
			try
			{
				if (httpContext.User.Identity is OAuthIdentity)
				{
					OAuthIdentity oauthIdentity = httpContext.User.Identity as OAuthIdentity;
					commonAccessToken = oauthIdentity.ToCommonAccessToken(targetVersion);
				}
				else if (httpContext.User is DelegatedPrincipal)
				{
					commonAccessToken = new CommonAccessToken(AccessTokenType.RemotePowerShellDelegated);
					commonAccessToken.ExtensionData["DelegatedData"] = ((DelegatedPrincipal)httpContext.User).Identity.GetSafeName(true);
				}
				else
				{
					CommonAccessToken commonAccessToken2 = httpContext.Items["Item-CommonAccessToken"] as CommonAccessToken;
					if (commonAccessToken2 != null)
					{
						return commonAccessToken2;
					}
					if (httpContext.User.Identity is WindowsIdentity)
					{
						WindowsIdentity windowsIdentity = httpContext.User.Identity as WindowsIdentity;
						string value = httpContext.Items[Constants.WLIDMemberName] as string;
						if (!string.IsNullOrEmpty(value))
						{
							commonAccessToken = new CommonAccessToken(AccessTokenType.LiveIdNego2);
							commonAccessToken.ExtensionData["UserSid"] = windowsIdentity.User.ToString();
							commonAccessToken.ExtensionData["MemberName"] = value;
						}
						else
						{
							commonAccessToken = new CommonAccessToken(windowsIdentity);
						}
					}
				}
			}
			catch (CommonAccessTokenException arg)
			{
				ExTraceGlobals.BriefTracer.TraceError<string, CommonAccessTokenException>(0L, "[AspNetHelper::FixupCommonAccessToken] Error encountered when creating CommonAccessToken from current logong identity. User: {0} Exception: {1}.", httpContext.User.Identity.GetSafeName(true), arg);
				throw new HttpProxyException(HttpStatusCode.Unauthorized, HttpProxySubErrorCode.UserNotFound, string.Format("Error encountered when creating common access token. User: {0}", httpContext.User.Identity.GetSafeName(true)));
			}
			return commonAccessToken;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00017918 File Offset: 0x00015B18
		public static bool IsExceptionExpectedWhenDisconnected(Exception e)
		{
			if (e is IOException)
			{
				return true;
			}
			HttpException ex = e as HttpException;
			if (ex == null)
			{
				return false;
			}
			int errorCode = ex.ErrorCode;
			int num = 0;
			if (ex.InnerException != null && ex.InnerException is COMException)
			{
				num = ((COMException)ex.InnerException).ErrorCode;
			}
			return errorCode == -2147023667 || num == -2147023667 || errorCode == -2147023901 || num == -2147023901 || errorCode == -2147024832 || num == -2147024832 || errorCode == -2147024890 || num == -2147024890 || errorCode == -2147024809 || num == -2147024809 || errorCode == -2147024874 || num == -2147024874 || errorCode == -2147024895 || num == -2147024895;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000179DC File Offset: 0x00015BDC
		public static void TransferToErrorPage(HttpContext httpContext, ErrorFE.FEErrorCodes errorCode)
		{
			ExTraceGlobals.VerboseTracer.TraceDebug<ErrorFE.FEErrorCodes>(0L, "[AspNetHelper.TransferToErrorPage] Transferring to error page with error code {0}", errorCode);
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			httpContext.Items["CafeError"] = errorCode;
			httpContext.Server.Transfer(OwaUrl.CafeErrorPage.ImplicitUrl);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00017A34 File Offset: 0x00015C34
		public static void TransferToRedirectPage(HttpContext httpContext, string redirectUrl, LegacyRedirectTypeOptions redirectType)
		{
			ExTraceGlobals.VerboseTracer.TraceDebug(0L, "[AspNetHelper.TransferToRedirectPage] Transferring to redirect page...");
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (string.IsNullOrEmpty(redirectUrl))
			{
				throw new ArgumentNullException("redirectUrl");
			}
			httpContext.Items.Add("redirectUrl", redirectUrl);
			httpContext.Items.Add("redirectType", redirectType);
			httpContext.Server.Transfer(OwaUrl.CafeErrorPage.ImplicitUrl);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00017AAF File Offset: 0x00015CAF
		public static void MakePageNoCacheNoStore(HttpResponse response)
		{
			response.Cache.SetCacheability(HttpCacheability.NoCache);
			response.Cache.SetNoStore();
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00017AC8 File Offset: 0x00015CC8
		public static void SetCacheability(HttpResponse response, string cacheControlHeaderValue)
		{
			string[] separator = new string[]
			{
				"\r\n",
				" ",
				"\t",
				","
			};
			foreach (string text in cacheControlHeaderValue.Split(separator, StringSplitOptions.RemoveEmptyEntries))
			{
				if (text.Equals("private", StringComparison.OrdinalIgnoreCase))
				{
					response.Cache.SetCacheability(HttpCacheability.Private);
				}
				else if (text.Equals("public", StringComparison.OrdinalIgnoreCase))
				{
					response.Cache.SetCacheability(HttpCacheability.Public);
				}
				else if (text.Equals("no-cache", StringComparison.OrdinalIgnoreCase))
				{
					response.Cache.SetCacheability(HttpCacheability.NoCache);
				}
				else if (text.Equals("no-store", StringComparison.OrdinalIgnoreCase))
				{
					response.Cache.SetNoStore();
				}
				else if (text.StartsWith("max-age=", StringComparison.OrdinalIgnoreCase))
				{
					uint seconds = 0U;
					if (text.Length > "max-age=".Length && uint.TryParse(text.Substring("max-age=".Length), out seconds))
					{
						response.Cache.SetMaxAge(new TimeSpan(0, 0, (int)seconds));
					}
					else
					{
						ExTraceGlobals.VerboseTracer.TraceDebug<string>(0L, "[AspNetHelper::SetCacheability] Cannot parse max-age token {0}", text);
					}
				}
				else
				{
					ExTraceGlobals.VerboseTracer.TraceDebug<string>(0L, "[AspNetHelper::SetCacheability] Unknown Cache-Control token {0}", text);
				}
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00017C18 File Offset: 0x00015E18
		public static void AddTimestampHeaderIfNecessary(NameValueCollection headers, string headerName)
		{
			try
			{
				if (headers["X-OWA-CorrelationId"] != null)
				{
					headers[headerName] = ExDateTime.Now.ToString(Constants.ISO8601DateTimeMsPattern);
				}
			}
			catch (HttpException)
			{
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00017C60 File Offset: 0x00015E60
		public static string GetRequestCorrelationId(HttpContext httpContext)
		{
			ExAssert.RetailAssert(httpContext != null, "httpContext is null");
			string text = httpContext.Request.Headers["X-OWA-CorrelationId"];
			if (string.IsNullOrEmpty(text))
			{
				text = "<empty>";
			}
			return text;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00017CA4 File Offset: 0x00015EA4
		public static void TerminateRequestWithSslRequiredResponse(HttpApplication httpApplication)
		{
			HttpResponse response = httpApplication.Context.Response;
			response.Clear();
			response.StatusCode = 403;
			response.SubStatusCode = 4;
			response.StatusDescription = "SSL required.";
			httpApplication.CompleteRequest();
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00017CE6 File Offset: 0x00015EE6
		public static string GetClientIpAsProxyHeader(HttpRequest httpRequest)
		{
			return AspNetHelper.GetClientIpAsProxyHeader(httpRequest);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00017CEE File Offset: 0x00015EEE
		public static string GetClientPortAsProxyHeader(HttpContext httpContext)
		{
			return AspNetHelper.GetClientPortAsProxyHeader(httpContext);
		}
	}
}
