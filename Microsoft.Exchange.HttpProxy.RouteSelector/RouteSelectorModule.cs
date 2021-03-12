using System;
using System.Globalization;
using System.Text;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.HttpProxy.Routing;
using Microsoft.Exchange.HttpProxy.Routing.Serialization;

namespace Microsoft.Exchange.HttpProxy.RouteSelector
{
	// Token: 0x02000009 RID: 9
	internal class RouteSelectorModule : IHttpModule
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000026D5 File Offset: 0x000008D5
		public RouteSelectorModule() : this(new RouteSelector(), null)
		{
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026E3 File Offset: 0x000008E3
		public RouteSelectorModule(IServerLocatorFactory routeSelector, IRouteSelectorModuleDiagnostics testDiagnostics)
		{
			if (routeSelector == null)
			{
				throw new ArgumentNullException("routeSelector");
			}
			this.routeSelector = routeSelector;
			this.serverLocator = routeSelector.GetServerLocator(HttpProxyGlobals.ProtocolType);
			this.diagnostics = testDiagnostics;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002718 File Offset: 0x00000918
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000271F File Offset: 0x0000091F
		internal static bool IsTesting { get; set; }

		// Token: 0x06000029 RID: 41 RVA: 0x0000273F File Offset: 0x0000093F
		public void Init(HttpApplication application)
		{
			application.PostAuthorizeRequest += delegate(object sender, EventArgs args)
			{
				this.OnPostAuthorizeRequest(new HttpContextWrapper(((HttpApplication)sender).Context));
			};
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002753 File Offset: 0x00000953
		public void Dispose()
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002770 File Offset: 0x00000970
		internal void OnPostAuthorizeRequest(HttpContextBase context)
		{
			Diagnostics.SendWatsonReportOnUnhandledException(delegate()
			{
				this.OnPostAuthorizeInternal(context);
			});
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002AA4 File Offset: 0x00000CA4
		private void OnPostAuthorizeInternal(HttpContextBase context)
		{
			if (!HttpProxySettings.RouteSelectorEnabled.Value && !RouteSelectorModule.IsTesting)
			{
				return;
			}
			if (!RouteSelectorModule.IsTesting)
			{
				RequestLogger logger = RequestLogger.GetLogger(context);
				this.diagnostics = new RouteSelectorDiagnostics(logger);
			}
			this.diagnostics.SaveRoutingLatency(delegate
			{
				ServerLocatorReturn serverLocatorReturn = null;
				IRoutingKey[] array = (IRoutingKey[])context.Items["RoutingKeys"];
				if (array == null)
				{
					return;
				}
				if (array.Length == 0)
				{
					return;
				}
				serverLocatorReturn = this.serverLocator.LocateServer(array, this.diagnostics);
				this.diagnostics.LogLatencies();
				this.diagnostics.ProcessLatencyPerfCounters();
				if (serverLocatorReturn != null && !string.IsNullOrEmpty(serverLocatorReturn.ServerFqdn))
				{
					if (!context.Request.IsProxyTestProbeRequest())
					{
						context.Request.Headers.Add("X-ProxyTargetServer", serverLocatorReturn.ServerFqdn);
						context.Request.Headers.Add("X-ProxyTargetServerVersion", (serverLocatorReturn.ServerVersion != null) ? serverLocatorReturn.ServerVersion.ToString() : string.Empty);
					}
					this.diagnostics.SetTargetServer(serverLocatorReturn.ServerFqdn);
					RouteSelectorDiagnostics.UpdateRoutingFailurePerfCounter(serverLocatorReturn.ServerFqdn, false);
					if (serverLocatorReturn.RoutingEntries.Count > 0)
					{
						StringBuilder stringBuilder = new StringBuilder();
						bool flag = true;
						foreach (IRoutingEntry routingEntry in serverLocatorReturn.RoutingEntries)
						{
							if (!flag)
							{
								stringBuilder.Append(",");
							}
							string value = RoutingEntryHeaderSerializer.Serialize(routingEntry);
							stringBuilder.Append(value);
							flag = false;
							this.diagnostics.AddRoutingEntry(value);
						}
						context.Request.Headers.Add("X-RoutingEntry", stringBuilder.ToString());
					}
					int versionNumber = serverLocatorReturn.ServerVersion ?? 0;
					ServerVersion serverVersion = new ServerVersion(versionNumber);
					string targetServerVersion = string.Format(CultureInfo.InvariantCulture, "{0:d}.{1:d2}.{2:d4}.{3:d3}", new object[]
					{
						serverVersion.Major,
						serverVersion.Minor,
						serverVersion.Build,
						serverVersion.Revision
					});
					this.diagnostics.SetTargetServerVersion(targetServerVersion);
					return;
				}
				string value2 = "RouteNotFoundError";
				this.diagnostics.AddErrorInfo(value2);
				context.Response.StatusCode = 500;
				bool wasFailure = true;
				foreach (IRoutingKey routingKey in array)
				{
					if (routingKey.RoutingItemType == RoutingItemType.LiveIdMemberName || routingKey.RoutingItemType == RoutingItemType.Smtp)
					{
						wasFailure = false;
						context.Response.StatusCode = 404;
						break;
					}
				}
				RouteSelectorDiagnostics.UpdateRoutingFailurePerfCounter(null, wasFailure);
				context.ApplicationInstance.CompleteRequest();
			});
		}

		// Token: 0x04000007 RID: 7
		private readonly IServerLocatorFactory routeSelector;

		// Token: 0x04000008 RID: 8
		private readonly IServerLocator serverLocator;

		// Token: 0x04000009 RID: 9
		private IRouteSelectorModuleDiagnostics diagnostics;
	}
}
