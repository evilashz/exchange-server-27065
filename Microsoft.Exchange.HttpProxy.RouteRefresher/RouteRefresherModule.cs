using System;
using System.Web;
using Microsoft.Exchange.HttpProxy.Common;

namespace Microsoft.Exchange.HttpProxy.RouteRefresher
{
	// Token: 0x02000006 RID: 6
	internal class RouteRefresherModule : IHttpModule
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000240E File Offset: 0x0000060E
		public RouteRefresherModule()
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002416 File Offset: 0x00000616
		public RouteRefresherModule(IRouteRefresher routeRefresher, IRouteRefresherDiagnostics routeRefresherDiagnostics)
		{
			if (routeRefresher == null)
			{
				throw new ArgumentNullException("routeRefresher");
			}
			if (routeRefresherDiagnostics == null)
			{
				throw new ArgumentNullException("routeRefresherDiagnostics");
			}
			this.routeRefresher = routeRefresher;
			this.diagnostics = routeRefresherDiagnostics;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002460 File Offset: 0x00000660
		public void Init(HttpApplication application)
		{
			application.PreSendRequestHeaders += delegate(object sender, EventArgs args)
			{
				this.OnPreSendRequestHeaders(new HttpContextWrapper(((HttpApplication)sender).Context));
			};
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002474 File Offset: 0x00000674
		void IHttpModule.Dispose()
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002494 File Offset: 0x00000694
		internal void OnPreSendRequestHeaders(HttpContextBase context)
		{
			Diagnostics.SendWatsonReportOnUnhandledException(delegate()
			{
				this.OnPreSendRequestHeadersInternal(context);
			});
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024C6 File Offset: 0x000006C6
		internal void OnPreSendRequestHeadersInternal(HttpContextBase context)
		{
			if (!HttpProxySettings.RouteRefresherEnabled.Value)
			{
				return;
			}
			this.CheckForRoutingUpdates(context);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024FC File Offset: 0x000006FC
		internal void CheckForRoutingUpdates(HttpContextBase context)
		{
			HttpResponseBase response = context.Response;
			string routingUpdatesHeaderValue = response.Headers["X-RoutingEntryUpdate"];
			if (string.IsNullOrEmpty(routingUpdatesHeaderValue))
			{
				return;
			}
			response.Headers.Remove("X-RoutingEntryUpdate");
			if (this.diagnostics == null)
			{
				RequestLogger logger = RequestLogger.GetLogger(context);
				this.diagnostics = new RouteRefresherDiagnostics(logger);
			}
			if (this.routeRefresher == null)
			{
				this.routeRefresher = new RouteRefresher(this.diagnostics);
			}
			this.diagnostics.LogRouteRefresherLatency(delegate
			{
				this.routeRefresher.ProcessRoutingUpdates(routingUpdatesHeaderValue);
			});
			this.routeRefresher = null;
			this.diagnostics = null;
		}

		// Token: 0x04000009 RID: 9
		private IRouteRefresher routeRefresher;

		// Token: 0x0400000A RID: 10
		private IRouteRefresherDiagnostics diagnostics;
	}
}
