using System;
using System.Web;
using System.Web.Configuration;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000CD RID: 205
	public class RpcHttpRequestHandler : IHttpHandler
	{
		// Token: 0x06000719 RID: 1817 RVA: 0x0002D4C6 File Offset: 0x0002B6C6
		internal RpcHttpRequestHandler() : this(RpcHttpProxyRules.DefaultRpcHttpProxyRules)
		{
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0002D4D3 File Offset: 0x0002B6D3
		internal RpcHttpRequestHandler(RpcHttpProxyRules rule)
		{
			this.proxyRules = rule;
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0002D4E2 File Offset: 0x0002B6E2
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0002D4E8 File Offset: 0x0002B6E8
		private bool AllowDiagnostics
		{
			get
			{
				string value = WebConfigurationManager.AppSettings["EnableDiagnostics"];
				bool result;
				bool.TryParse(value, out result);
				return result;
			}
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0002D50F File Offset: 0x0002B70F
		public static bool CanHandleRequest(HttpRequest request)
		{
			return string.IsNullOrEmpty(request.Url.Query) || !RpcHttpRequestHandler.IsRpcProxyRequest(request) || RpcHttpRequestHandler.IsProxyPreAuthenticationRequest(request) || RpcHttpRequestHandler.IsHttpProxyRequest(request);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0002D53C File Offset: 0x0002B73C
		public void ProcessRequest(HttpContext context)
		{
			if (!context.Request.IsAuthenticated)
			{
				context.Response.StatusCode = 401;
				return;
			}
			if (RpcHttpRequestHandler.IsProxyPreAuthenticationRequest(context.Request) || RpcHttpRequestHandler.IsHttpProxyRequest(context.Request))
			{
				context.Response.StatusCode = 400;
				context.Response.StatusDescription = "Detected request from another HttpProxy";
				return;
			}
			if (RpcHttpRequestHandler.IsRpcProxyRequest(context.Request) && string.IsNullOrEmpty(context.Request.Url.Query))
			{
				context.Response.StatusCode = 200;
				return;
			}
			if (context.Request.Url.AbsolutePath.StartsWith("/rpc/diagnostics/", StringComparison.OrdinalIgnoreCase) && this.AllowDiagnostics)
			{
				this.ProcessDiagnosticsRequest(context);
				return;
			}
			context.Response.StatusCode = 404;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0002D614 File Offset: 0x0002B814
		private static bool IsProxyPreAuthenticationRequest(HttpRequest request)
		{
			return string.Equals(request.Headers[WellKnownHeader.PreAuthRequest], "true", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0002D631 File Offset: 0x0002B831
		private static bool IsHttpProxyRequest(HttpRequest request)
		{
			return string.Equals(request.Headers[Constants.XIsFromCafe], Constants.IsFromCafeHeaderValue, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0002D64E File Offset: 0x0002B84E
		private static bool IsRpcProxyRequest(HttpRequest request)
		{
			return string.Equals(request.Url.AbsolutePath, "/rpc/rpcproxy.dll", StringComparison.OrdinalIgnoreCase) || string.Equals(request.Url.AbsolutePath, "/rpcwithcert/rpcproxy.dll", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0002D680 File Offset: 0x0002B880
		private void ProcessDiagnosticsRequest(HttpContext context)
		{
			if (string.Equals(context.Request.Url.AbsolutePath, "/rpc/diagnostics/", StringComparison.OrdinalIgnoreCase))
			{
				context.Response.Output.WriteLine("<HTML><BODY>");
				context.Response.Output.WriteLine("<A HREF=\"proxyrules.txt\">Proxy Rules</A>");
				context.Response.Output.WriteLine("</BODY></HTML>");
				return;
			}
			if (string.Equals(context.Request.Url.AbsolutePath, "/rpc/diagnostics/proxyrules.txt", StringComparison.OrdinalIgnoreCase))
			{
				context.Response.AddHeader("Content-Type", "text/plain");
				context.Response.AddHeader("Cache-Control", "no-cache");
				context.Response.Output.WriteLine(this.proxyRules.ToString());
				return;
			}
			context.Response.StatusCode = 404;
		}

		// Token: 0x040004BC RID: 1212
		private const string AppSettingsEnableDiagnostics = "EnableDiagnostics";

		// Token: 0x040004BD RID: 1213
		private const string RpcProxyPath = "/rpc/rpcproxy.dll";

		// Token: 0x040004BE RID: 1214
		private const string RpcWithCertProxyPath = "/rpcwithcert/rpcproxy.dll";

		// Token: 0x040004BF RID: 1215
		private const string DiagnosticsPathBase = "/rpc/diagnostics/";

		// Token: 0x040004C0 RID: 1216
		private readonly RpcHttpProxyRules proxyRules;
	}
}
