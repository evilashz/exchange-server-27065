using System;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000559 RID: 1369
	public static class HttpContextExtensions
	{
		// Token: 0x06004003 RID: 16387 RVA: 0x000C18D3 File Offset: 0x000BFAD3
		public static bool IsLogoffRequest(this HttpContext context)
		{
			return context.Request.FilePath.EndsWith("logoff.aspx", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06004004 RID: 16388 RVA: 0x000C18EB File Offset: 0x000BFAEB
		public static bool IsWebServiceRequest(this HttpContext context)
		{
			return context.Request.HttpMethod == "POST" && context.Request.FilePath.Contains(".svc");
		}

		// Token: 0x06004005 RID: 16389 RVA: 0x000C191B File Offset: 0x000BFB1B
		public static bool IsUploadRequest(this HttpContext context)
		{
			return context.Request.HttpMethod == "POST" && context.Request.FilePath.EndsWith("UploadHandler.ashx");
		}

		// Token: 0x06004006 RID: 16390 RVA: 0x000C194C File Offset: 0x000BFB4C
		public static bool IsNarrowPage(this HttpContext context)
		{
			string text = HttpContext.Current.Request.QueryString["isNarrow"];
			return text != null && text == "t";
		}

		// Token: 0x06004007 RID: 16391 RVA: 0x000C1984 File Offset: 0x000BFB84
		public static Exception GetError(this HttpContext context)
		{
			if (context.Error != null)
			{
				return context.Error;
			}
			Exception[] allErrors = context.AllErrors;
			if (allErrors.IsNullOrEmpty())
			{
				return null;
			}
			return allErrors[0];
		}

		// Token: 0x06004008 RID: 16392 RVA: 0x000C19B4 File Offset: 0x000BFBB4
		public static string GetExplicitUser(this HttpContext context)
		{
			return context.Request.Headers["msExchEcpESOUser"] ?? string.Empty;
		}

		// Token: 0x06004009 RID: 16393 RVA: 0x000C19D4 File Offset: 0x000BFBD4
		public static string GetOwaNavigationParameter(this HttpContext context)
		{
			return context.Request.QueryString["owaparam"] ?? string.Empty;
		}

		// Token: 0x0600400A RID: 16394 RVA: 0x000C19F4 File Offset: 0x000BFBF4
		public static bool IsExplicitSignOn(this HttpContext context)
		{
			return !string.IsNullOrEmpty(context.GetExplicitUser());
		}

		// Token: 0x0600400B RID: 16395 RVA: 0x000C1A04 File Offset: 0x000BFC04
		public static string GetTargetTenant(this HttpContext context)
		{
			string result;
			if ((result = context.Request.Headers[RequestFilterModule.TargetTenantKey]) == null)
			{
				result = (context.Request.Headers[RequestFilterModule.OrganizationContextKey] ?? string.Empty);
			}
			return result;
		}

		// Token: 0x0600400C RID: 16396 RVA: 0x000C1A3D File Offset: 0x000BFC3D
		public static bool HasTargetTenant(this HttpContext context)
		{
			return !string.IsNullOrEmpty(context.GetTargetTenant());
		}

		// Token: 0x0600400D RID: 16397 RVA: 0x000C1A4D File Offset: 0x000BFC4D
		public static bool HasOrganizationContext(this HttpContext context)
		{
			return !string.IsNullOrEmpty(context.Request.Headers[RequestFilterModule.OrganizationContextKey]);
		}

		// Token: 0x0600400E RID: 16398 RVA: 0x000C1A6C File Offset: 0x000BFC6C
		public static string CurrentUserLiveID()
		{
			string text = string.Empty;
			if (HttpContext.Current.Items.Contains("RPSMemberName"))
			{
				text = (HttpContext.Current.Items["RPSMemberName"] as string);
			}
			else
			{
				text = HttpContext.Current.Request.Headers["RPSMemberName"];
			}
			if (!string.IsNullOrEmpty(text))
			{
				SmtpAddress smtpAddress = new SmtpAddress(text);
				if (!smtpAddress.IsValidAddress)
				{
					text = string.Empty;
				}
			}
			return text;
		}

		// Token: 0x0600400F RID: 16399 RVA: 0x000C1AEC File Offset: 0x000BFCEC
		public static string GetCurrentLiveIDEnvironment()
		{
			string text = HttpContext.Current.Request.Headers["RPSEnv"];
			if (string.IsNullOrEmpty(text))
			{
				if (HttpContext.Current.Items.Contains("RPSEnv"))
				{
					text = (HttpContext.Current.Items["RPSEnv"] as string);
				}
				else
				{
					text = string.Empty;
				}
			}
			return text;
		}

		// Token: 0x06004010 RID: 16400 RVA: 0x000C1B54 File Offset: 0x000BFD54
		public static string GetSessionID(this HttpContext context)
		{
			HttpCookie httpCookie = context.Request.Cookies[RbacModule.SessionStateCookieName];
			if (httpCookie == null)
			{
				httpCookie = context.Response.Cookies[RbacModule.SessionStateCookieName];
				if (httpCookie == null)
				{
					throw new InvalidOperationException("Session cookie hasn't been set.");
				}
			}
			return httpCookie.Value;
		}

		// Token: 0x06004011 RID: 16401 RVA: 0x000C1BA4 File Offset: 0x000BFDA4
		public static Uri GetRequestUrl(this HttpContext context)
		{
			Uri uri = (Uri)context.Items["RequestUrl"];
			if (uri == null || uri.PathAndQuery != context.Request.Url.PathAndQuery)
			{
				string text = context.Request.Headers["msExchProxyUri"];
				if (!string.IsNullOrEmpty(text))
				{
					uri = new Uri(new Uri(text), context.Request.Url.PathAndQuery);
				}
				else
				{
					uri = context.Request.Url;
				}
				context.Items["RequestUrl"] = uri;
			}
			return uri;
		}

		// Token: 0x06004012 RID: 16402 RVA: 0x000C1C47 File Offset: 0x000BFE47
		public static string GetRequestUrlPathAndQuery(this HttpContext context)
		{
			return context.Request.Url.PathAndQuery;
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x000C1C59 File Offset: 0x000BFE59
		public static string GetRequestUrlAbsolutePath(this HttpContext context)
		{
			return context.Request.Url.AbsolutePath;
		}

		// Token: 0x06004014 RID: 16404 RVA: 0x000C1C6C File Offset: 0x000BFE6C
		public static string GetRequestUrlForLog(this HttpContext context)
		{
			string text = context.Request.Headers["msExchProxyUri"];
			if (!string.IsNullOrEmpty(text))
			{
				return string.Format("{0}({1})", context.Request.Url, text);
			}
			return context.Request.Url.ToString();
		}

		// Token: 0x06004015 RID: 16405 RVA: 0x000C1CC0 File Offset: 0x000BFEC0
		public static bool TargetServerOrVersionSpecifiedInUrlOrCookie(this HttpContext context)
		{
			return !string.IsNullOrEmpty(context.Request.QueryString["ExchClientVer"]) || !string.IsNullOrEmpty(context.Request.QueryString["TargetServer"]) || context.Request.Cookies["ExchClientVer"] != null || context.Request.Cookies["TargetServer"] != null;
		}

		// Token: 0x06004016 RID: 16406 RVA: 0x000C1D39 File Offset: 0x000BFF39
		public static void ThrowIfViewOptionsWithBEParam(this HttpContext context, FeatureSet featureSet)
		{
			if (featureSet == FeatureSet.Options && context.TargetServerOrVersionSpecifiedInUrlOrCookie() && !context.IsExplicitSignOn())
			{
				throw new CannotAccessOptionsWithBEParamOrCookieException();
			}
		}

		// Token: 0x06004017 RID: 16407 RVA: 0x000C1D58 File Offset: 0x000BFF58
		public static bool IsAcsOAuthRequest(this HttpContext context)
		{
			if (context.User == null)
			{
				return false;
			}
			RbacSession rbacSession = context.User as RbacSession;
			IIdentity identity;
			if (rbacSession != null)
			{
				identity = rbacSession.Settings.LogonUserIdentity;
			}
			else
			{
				LogoffSession logoffSession = context.User as LogoffSession;
				if (logoffSession != null)
				{
					identity = logoffSession.OriginalIdentity;
				}
				else
				{
					identity = context.User.Identity;
				}
			}
			return identity is OAuthIdentity || identity is SidOAuthIdentity || context.Items["LogonUserIdentity"] is SidOAuthIdentity;
		}

		// Token: 0x04002AAF RID: 10927
		internal const string LogoffPage = "logoff.aspx";

		// Token: 0x04002AB0 RID: 10928
		internal const string OwaNavigationParameter = "owaparam";

		// Token: 0x04002AB1 RID: 10929
		internal const string LogonUserIdentityKey = "LogonUserIdentity";
	}
}
