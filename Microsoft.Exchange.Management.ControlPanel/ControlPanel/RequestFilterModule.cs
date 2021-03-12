using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Exchange.Configuration.DelegatedAuthentication;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200020A RID: 522
	internal sealed class RequestFilterModule : IHttpModule
	{
		// Token: 0x17001BF4 RID: 7156
		// (get) Token: 0x060026C9 RID: 9929 RVA: 0x000789E0 File Offset: 0x00076BE0
		private static bool BlockNonCafeRequest
		{
			get
			{
				if (RequestFilterModule.blockNonCafeRequest == null)
				{
					lock (RequestFilterModule.blockNonCafeRequestLock)
					{
						if (RequestFilterModule.blockNonCafeRequest == null)
						{
							RequestFilterModule.blockNonCafeRequest = new bool?(!string.Equals(ConfigurationManager.AppSettings["AllowDirectBERequest"], "true", StringComparison.OrdinalIgnoreCase) && !string.Equals(ConfigurationManager.AppSettings["IsOSPEnvironment"], "true", StringComparison.OrdinalIgnoreCase) && !DatacenterRegistry.IsForefrontForOffice());
						}
					}
				}
				return RequestFilterModule.blockNonCafeRequest.Value;
			}
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x00078A88 File Offset: 0x00076C88
		public void Init(HttpApplication application)
		{
			application.BeginRequest += this.OnBeginRequest;
			application.AuthenticateRequest += this.OnAuthenticationRequest;
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x00078AAE File Offset: 0x00076CAE
		public void Dispose()
		{
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x00078AB0 File Offset: 0x00076CB0
		private void OnBeginRequest(object sender, EventArgs e)
		{
			HttpContext httpContext = HttpContext.Current;
			HttpRequest request = httpContext.Request;
			if (RequestFilterModule.BlockNonCafeRequest)
			{
				string a = HttpContext.Current.Request.Headers[WellKnownHeader.XIsFromCafe];
				if (!string.Equals(a, "1"))
				{
					httpContext.Response.Headers.Set("X-BlockDirectBERequest", "1");
					throw new BadRequestException();
				}
			}
			if (Utility.IsResourceRequest(request.Url.LocalPath))
			{
				return;
			}
			httpContext.Response.Headers.Set("X-Content-Type-Options", "nosniff");
			this.ProcessFeatureRedirection(httpContext, request);
			RequestTypeInfo requestTypeInfo = RequestFilterModule.DetermineRequestType(request);
			this.StampTokenToHeader(httpContext, request, requestTypeInfo);
			this.HandleRedirection(httpContext, request, requestTypeInfo);
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x00078B68 File Offset: 0x00076D68
		private void HandleRedirection(HttpContext httpContext, HttpRequest request, RequestTypeInfo requestTypeInfo)
		{
			string text = null;
			if (requestTypeInfo.Need302Redirect)
			{
				text = request.RawUrl.Insert(request.FilePath.Length, "/");
			}
			else if (requestTypeInfo.NeedRedirectTargetTenant)
			{
				text = EcpUrl.ProcessUrl(request.RawUrl, true);
				text = EcpUrl.RemoveQueryParameter(text, requestTypeInfo.IsDelegatedAdminRequest ? "delegatedorg" : "organizationcontext", false);
			}
			else if (requestTypeInfo.UseImplicitPathRewrite && requestTypeInfo.IsSecurityTokenPresented)
			{
				text = request.Headers[RequestFilterModule.OriginalUrlKey];
			}
			if (text != null)
			{
				ExTraceGlobals.RedirectTracer.TraceInformation<string>(0, 0L, "[RequestFilterModule::HandleRedirection] Redirect to {0}).", text);
				httpContext.Response.Redirect(text, true);
			}
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x00078C1C File Offset: 0x00076E1C
		internal static RequestTypeInfo DetermineRequestType(HttpRequest httpRequest)
		{
			string filePath = httpRequest.FilePath;
			NameValueCollection queryString = httpRequest.QueryString;
			RequestTypeInfo result = default(RequestTypeInfo);
			string text = queryString["delegatedorg"];
			if (!string.IsNullOrEmpty(text))
			{
				result.NeedRedirectTargetTenant = true;
				result.IsDelegatedAdminRequest = true;
				result.TargetTenant = text;
			}
			string text2 = queryString["organizationcontext"];
			if (!string.IsNullOrEmpty(text2))
			{
				result.NeedRedirectTargetTenant = true;
				result.IsByoidAdmin = true;
				result.TargetTenant = text2;
			}
			if (result.IsDelegatedAdminRequest && result.IsByoidAdmin)
			{
				throw new BadRequestException(new Exception("Both delegatedorg and organizationcontext parameters are specified in request url."));
			}
			Match match = RequestFilterModule.regex.Match(filePath);
			if (match.Success)
			{
				Group group = match.Groups["isOrgContext"];
				Group group2 = match.Groups["targetTenant"];
				Group group3 = match.Groups["esoAddress"];
				Group group4 = match.Groups["closeSlash"];
				if (group2.Success)
				{
					if (result.NeedRedirectTargetTenant)
					{
						throw new BadRequestException(new Exception("Both '/@' style and parameter style are used in request url."));
					}
					if (group.Success)
					{
						result.IsByoidAdmin = true;
					}
					else
					{
						result.IsDelegatedAdminRequest = true;
						if (filePath.EndsWith("/", StringComparison.InvariantCulture))
						{
							result.UseImplicitPathRewrite = true;
						}
						result.IsSecurityTokenPresented = DelegatedAuthenticationModule.IsSecurityTokenPresented(httpRequest);
					}
					result.TargetTenant = group2.Value;
				}
				if (group3.Success)
				{
					result.IsEsoRequest = true;
					result.EsoMailboxSmtpAddress = group3.Value;
				}
				if (!group4.Success)
				{
					result.Need302Redirect = true;
				}
			}
			return result;
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x00078DC0 File Offset: 0x00076FC0
		private void StampTokenToHeader(HttpContext httpContext, HttpRequest request, RequestTypeInfo requestTypeInfo)
		{
			if (requestTypeInfo.IsDelegatedAdminRequest)
			{
				request.Headers.Set(RequestFilterModule.TargetTenantKey, requestTypeInfo.TargetTenant);
				string text = request.RawUrl;
				if (requestTypeInfo.UseImplicitPathRewrite)
				{
					text = text.Insert(request.FilePath.Length, "default.aspx");
				}
				request.Headers.Set(RequestFilterModule.OriginalUrlKey, text);
				if (requestTypeInfo.IsSecurityTokenPresented)
				{
					httpContext.Items[RequestFilterModule.NoResolveIdKey] = "1";
				}
			}
			else if (requestTypeInfo.IsByoidAdmin)
			{
				request.Headers.Set(RequestFilterModule.OrganizationContextKey, requestTypeInfo.TargetTenant);
			}
			if (requestTypeInfo.IsEsoRequest)
			{
				request.Headers.Set("msExchEcpESOUser", requestTypeInfo.EsoMailboxSmtpAddress);
			}
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x00078E88 File Offset: 0x00077088
		private void OnAuthenticationRequest(object sender, EventArgs e)
		{
			HttpContext httpContext = HttpContext.Current;
			HttpRequest request = httpContext.Request;
			if (httpContext.Request.IsAuthenticated)
			{
				httpContext.Items["LogonUserIdentity"] = httpContext.User.Identity;
			}
			if (!string.IsNullOrEmpty(request.Headers["msExchPathRewritten"]))
			{
				return;
			}
			int num = 0;
			string text = request.Headers[RequestFilterModule.TargetTenantKey];
			if (!string.IsNullOrEmpty(text))
			{
				num += text.Length + 2;
			}
			else
			{
				string text2 = request.Headers[RequestFilterModule.OrganizationContextKey];
				if (!string.IsNullOrEmpty(text2))
				{
					num += text2.Length + 3;
				}
			}
			string text3 = request.Headers["msExchEcpESOUser"];
			if (!string.IsNullOrEmpty(text3))
			{
				num += text3.Length + 1;
			}
			if (num > 0)
			{
				string empty = RequestFilterModule.appDomainAppVirtualPath;
				if (empty.Length == 1 && empty[0] == '/')
				{
					empty = string.Empty;
				}
				string text4 = empty + request.RawUrl.Substring(empty.Length + num);
				if (!this.ShouldRedirectRequest(request))
				{
					httpContext.RewritePath(text4);
					request.Headers.Set("msExchPathRewritten", "1");
					ExTraceGlobals.EventLogTracer.TraceInformation<string, string>(0, 0L, "Path rewritten from '{0}' to '{1}'.", request.RawUrl, text4);
					return;
				}
				ExTraceGlobals.RedirectTracer.TraceInformation<string>(0, 0L, "[RequestFilterModule::OnAuthenticateRequest] Redirect to {0}).", text4);
				httpContext.Response.Redirect(text4, true);
			}
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x00079003 File Offset: 0x00077203
		private bool ShouldRedirectRequest(HttpRequest request)
		{
			return request.FilePath.EndsWith(".csv", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x00079018 File Offset: 0x00077218
		private void ProcessFeatureRedirection(HttpContext httpContext, HttpRequest request)
		{
			string text = request.QueryString["ftr"];
			if (!text.IsNullOrBlank())
			{
				try
				{
					EcpFeature ecpFeature = (EcpFeature)Enum.Parse(typeof(EcpFeature), text, true);
					string text2 = "ftr=" + text;
					int num = request.Url.Query.IndexOf(text2, StringComparison.InvariantCultureIgnoreCase);
					string text3 = request.Url.Query.Substring(0, num) + request.Url.Query.Substring(num + text2.Length);
					EcpFeatureDescriptor featureDescriptor = ecpFeature.GetFeatureDescriptor();
					string query = featureDescriptor.AbsoluteUrl.Query;
					text3 = ((text3.Length > 1 && !string.IsNullOrEmpty(query) && query.Contains("?")) ? ("&" + text3.Substring(1)) : ((text3 == "?") ? string.Empty : text3));
					string text4 = (featureDescriptor.UseAbsoluteUrl ? featureDescriptor.AbsoluteUrl.ToEscapedString() : (request.Url.LocalPath + query)) + text3;
					Uri uri = new Uri(text4);
					NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uri.Query);
					StringBuilder stringBuilder = new StringBuilder();
					foreach (object obj in nameValueCollection)
					{
						string text5 = (string)obj;
						if (!string.IsNullOrEmpty(text5))
						{
							stringBuilder.AppendFormat("{0}={1}&", text5, nameValueCollection[text5].Split(new char[]
							{
								','
							})[0]);
						}
					}
					text4 = text4.Replace(uri.Query, "?" + stringBuilder.ToString().TrimEnd(new char[]
					{
						'&'
					}));
					ExTraceGlobals.RedirectTracer.TraceInformation<string>(0, 0L, "[RequestFilterModule::ProcessFeatureRedirection] Redirect to {0}).", text4);
					httpContext.Response.Redirect(text4);
				}
				catch (ArgumentException innerException)
				{
					throw new BadQueryParameterException("ftr", innerException);
				}
			}
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x0007926C File Offset: 0x0007746C
		internal static void AddTargetTenantToHeader(string tenantName)
		{
			HttpContext.Current.Request.Headers.Add(RequestFilterModule.TargetTenantKey, tenantName);
		}

		// Token: 0x04001F89 RID: 8073
		public const string ExplicitLogonUserKey = "msExchEcpESOUser";

		// Token: 0x04001F8A RID: 8074
		public const char TargetTenantPrefix = '@';

		// Token: 0x04001F8B RID: 8075
		public const char OrganizationContextPrefix = '.';

		// Token: 0x04001F8C RID: 8076
		private const string PathRewrittenKey = "msExchPathRewritten";

		// Token: 0x04001F8D RID: 8077
		private const string DelegatedOrgParameter = "delegatedorg";

		// Token: 0x04001F8E RID: 8078
		private const string OrganizationContextParameter = "organizationcontext";

		// Token: 0x04001F8F RID: 8079
		private const string AllowDirectBERequestKey = "AllowDirectBERequest";

		// Token: 0x04001F90 RID: 8080
		private const string IsOSPEnvironmentKey = "IsOSPEnvironment";

		// Token: 0x04001F91 RID: 8081
		public static readonly string TargetTenantKey = "msExchTargetTenant";

		// Token: 0x04001F92 RID: 8082
		public static readonly string OrganizationContextKey = "msExchOrganizationContext";

		// Token: 0x04001F93 RID: 8083
		public static readonly string NoResolveIdKey = "msExchNoResolveId";

		// Token: 0x04001F94 RID: 8084
		public static readonly string OriginalUrlKey = "msExchOriginalUrl";

		// Token: 0x04001F95 RID: 8085
		private static readonly string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath ?? string.Empty;

		// Token: 0x04001F96 RID: 8086
		private static readonly int appDomainAppVirtualPathLength = RequestFilterModule.appDomainAppVirtualPath.Length;

		// Token: 0x04001F97 RID: 8087
		private static readonly Regex regex = new Regex("^" + RequestFilterModule.appDomainAppVirtualPath + "(/@                             # Target tenant or OrganizationContext start with '@' (TargetTenantPrefix)\r\n                 (?<isOrgContext>\\.)?           # OrganizationContext follow by '.' (OrganizationContextPrefix)\r\n                 (?<targetTenant>[^./][^/]+)    # follow by the domain name\r\n                )?                              # \r\n                (/                              #\r\n                 (?<esoAddress>[^@/]+@[^@/]+)   # ESO Email address contains one '@' in the middle\r\n                )?                              #\r\n                (?<closeSlash>/)?               # close slash. Without it, the request need 302 redirect\r\n                ", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant);

		// Token: 0x04001F98 RID: 8088
		private static bool? blockNonCafeRequest;

		// Token: 0x04001F99 RID: 8089
		private static object blockNonCafeRequestLock = new object();
	}
}
