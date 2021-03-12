using System;
using System.Collections.Specialized;
using System.Web;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x0200021C RID: 540
	internal class PswsUrlRewriteModule : IHttpModule
	{
		// Token: 0x060012CC RID: 4812 RVA: 0x0003CF9D File Offset: 0x0003B19D
		public void Init(HttpApplication context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			context.BeginRequest += this.OnBeginRequest;
			context.PreSendRequestHeaders += this.MakeResponseNoCacheNoStore;
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x0003CFD1 File Offset: 0x0003B1D1
		void IHttpModule.Dispose()
		{
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0003CFD4 File Offset: 0x0003B1D4
		private void OnBeginRequest(object sender, EventArgs e)
		{
			ExTraceGlobals.AccessCheckTracer.TraceDebug((long)this.GetHashCode(), "[PswsUrlRewriteModule.OnBeginRequest] Enter");
			HttpApplication httpApplication = (HttpApplication)sender;
			HttpContext context = httpApplication.Context;
			HttpRequest request = context.Request;
			string text;
			string text2;
			string text3;
			string value;
			if (PswsUrlRewriteModule.TestWhetherUriNeedsModification(request.Url, request.Headers, out text, out text2, out text3, out value))
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[PswsUrlRewriteModule.OnBeginRequest] Url is rewritten. Path = \"{0}\", Query String = \"{1}\".", text, text2);
				context.RewritePath(text, null, text2, false);
			}
			if (!string.IsNullOrEmpty(text3))
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug<string>((long)this.GetHashCode(), "[PswsUrlRewriteModule.OnBeginRequest] Organization = \"{0}\".", text3);
				context.Request.Headers["organization"] = text3;
			}
			if (!string.IsNullOrEmpty(value))
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug<string>((long)this.GetHashCode(), "[PswsUrlRewriteModule.OnBeginRequest] DelegatedOrg = \"{0}\".", text3);
				context.Request.Headers["msExchTargetTenant"] = value;
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug((long)this.GetHashCode(), "[PswsUrlRewriteModule.OnBeginRequest] Leave");
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0003D0D4 File Offset: 0x0003B2D4
		private static bool TestWhetherUriNeedsModification(Uri requestUri, NameValueCollection httpHeaders, out string modifiedPath, out string queryString, out string organization, out string delegatedOrg)
		{
			bool flag = false;
			modifiedPath = null;
			queryString = null;
			delegatedOrg = null;
			UriBuilder uriBuilder = new UriBuilder(requestUri);
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uriBuilder.Query);
			organization = nameValueCollection["organization"];
			if (!string.IsNullOrWhiteSpace(organization))
			{
				string value = httpHeaders["X-Psws-KeepOrgInUrl"];
				bool flag2;
				if (bool.TryParse(value, out flag2) && flag2)
				{
					ExTraceGlobals.AccessCheckTracer.TraceDebug(0L, "[PswsUrlRewriteModule.TestWhetherUriNeedsModification] X-Psws-KeepOrgInUrl exists in header and organization is kept in the URL.");
				}
				else
				{
					flag = true;
					nameValueCollection.Remove("organization");
					uriBuilder.Query = nameValueCollection.ToString();
					HttpLogger.SafeAppendGenericInfo("UrlRewrite", "Organization:" + organization + " removed");
				}
			}
			delegatedOrg = nameValueCollection["DelegatedOrg"];
			if (!string.IsNullOrEmpty(delegatedOrg))
			{
				flag = true;
				nameValueCollection.Remove("DelegatedOrg");
				uriBuilder.Query = nameValueCollection.ToString();
				HttpLogger.SafeAppendGenericInfo("UrlRewrite", "DelegatedOrg:" + delegatedOrg + " removed");
			}
			if (uriBuilder.Path.EndsWith(".svc", StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(uriBuilder.Query))
			{
				flag = true;
				uriBuilder.Path += '/';
			}
			if (flag)
			{
				modifiedPath = uriBuilder.Path;
				queryString = uriBuilder.Query;
				if (!string.IsNullOrWhiteSpace(queryString) && queryString.StartsWith("?"))
				{
					queryString = queryString.Substring(1);
				}
			}
			return flag;
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0003D238 File Offset: 0x0003B438
		private void MakeResponseNoCacheNoStore(object sender, EventArgs e)
		{
			HttpApplication httpApplication = (HttpApplication)sender;
			HttpContext context = httpApplication.Context;
			HttpResponse response = context.Response;
			response.Cache.SetCacheability(HttpCacheability.NoCache);
			response.Cache.SetNoStore();
		}

		// Token: 0x040004AD RID: 1197
		internal const string PswsOrganizationItemKey = "X-Psws-Organization";

		// Token: 0x040004AE RID: 1198
		internal const string PswsKeepOrgInUrlHeaderKey = "X-Psws-KeepOrgInUrl";
	}
}
