using System;
using System.Collections.Specialized;
using System.Web;
using Microsoft.Exchange.Configuration.DiagnosticsModules.EventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.DiagnosticsModules;

namespace Microsoft.Exchange.Configuration.DiagnosticsModules
{
	// Token: 0x02000002 RID: 2
	public class ClientDiagnosticsModule : IHttpModule
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		void IHttpModule.Init(HttpApplication application)
		{
			application.BeginRequest += ClientDiagnosticsModule.OnBeginRequest;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E4 File Offset: 0x000002E4
		void IHttpModule.Dispose()
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020E8 File Offset: 0x000002E8
		private static void OnBeginRequest(object source, EventArgs args)
		{
			Logger.EnterFunction(ExTraceGlobals.ClientDiagnosticsModuleTracer, "ClientDiagnosticsModule.OnBeginRequest");
			HttpContext httpContext = HttpContext.Current;
			string text = httpContext.Request.Headers[ClientDiagnosticsModule.MsExchProxyUri];
			if (string.IsNullOrEmpty(text))
			{
				ClientDiagnosticsModule.LogVerbose("Get orginal url directly since it is not come from CAFE. Request url: {0}", new object[]
				{
					httpContext.Request.Url
				});
				text = httpContext.Request.Url.ToString();
			}
			NameValueCollection urlProperties = DiagnosticsHelper.GetUrlProperties(new Uri(text));
			if (ClientDiagnosticsModule.NeedAddDiagnostics(urlProperties))
			{
				string value = urlProperties["BEServer"];
				if (ClientDiagnosticsModule.AddBEServerInformationIfNeeded(ref value))
				{
					UriBuilder uriBuilder = new UriBuilder(text);
					urlProperties["BEServer"] = value;
					uriBuilder.Query = urlProperties.ToString().Replace("&", ";").Trim();
					string text2 = uriBuilder.ToString();
					HttpLogger.SafeAppendGenericInfo("DiagRedirect", text + " TO " + text2);
					ClientDiagnosticsModule.LogVerbose("Add diagnositcs information to request: Orginal url is {0}, Redirect url is {1}.", new object[]
					{
						text,
						text2
					});
					Logger.LogEvent(ClientDiagnosticsModule.eventLogger, TaskEventLogConstants.Tuple_ClientDiagnostics_RedirectWithDiagnosticsInformation, null, new object[]
					{
						text,
						httpContext.Server.UrlDecode(text2)
					});
					httpContext.Response.Redirect(text2);
				}
			}
			Logger.ExitFunction(ExTraceGlobals.ClientDiagnosticsModuleTracer, "ClientDiagnosticsModule.OnBeginRequest");
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000224C File Offset: 0x0000044C
		private static bool NeedAddDiagnostics(NameValueCollection urlProperties)
		{
			string a = urlProperties.Get("diag");
			return string.Equals(a, "true", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002274 File Offset: 0x00000474
		private static bool AddBEServerInformationIfNeeded(ref string beServers)
		{
			if (string.IsNullOrEmpty(beServers))
			{
				beServers = string.Format("{0}", Environment.MachineName);
				return true;
			}
			string[] array = beServers.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length > 0 && array[array.Length - 1].Equals(Environment.MachineName, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			beServers = string.Format("{0},{1}", beServers, Environment.MachineName);
			return true;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000022E3 File Offset: 0x000004E3
		private static void LogVerbose(string message, params object[] args)
		{
			Logger.TraceInformation(ExTraceGlobals.ClientDiagnosticsModuleTracer, message, args);
		}

		// Token: 0x04000001 RID: 1
		internal const string DiagnosticsSwitch = "diag";

		// Token: 0x04000002 RID: 2
		internal const string BEServerKey = "BEServer";

		// Token: 0x04000003 RID: 3
		private static readonly string MsExchProxyUri = "msExchProxyUri";

		// Token: 0x04000004 RID: 4
		private static readonly ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.ClientDiagnosticsModuleTracer.Category, "MSExchange Client Diagnostics Module", "MSExchange Management");
	}
}
