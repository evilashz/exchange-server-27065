using System;
using System.Web;
using System.Web.Hosting;
using Microsoft.Exchange.Diagnostics.Components.Common;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000050 RID: 80
	public sealed class ExWebHealthModule : IHttpModule
	{
		// Token: 0x06000196 RID: 406 RVA: 0x000081E3 File Offset: 0x000063E3
		public ExWebHealthModule()
		{
			this.Handler = new ExWebHealthHandler(HostingEnvironment.ApplicationVirtualPath.Substring(1));
			this.TimeoutReportHandler = new ExWebTimeoutReportHandler();
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000820C File Offset: 0x0000640C
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00008214 File Offset: 0x00006414
		internal ExWebHealthHandler Handler { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000821D File Offset: 0x0000641D
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00008225 File Offset: 0x00006425
		internal ExWebTimeoutReportHandler TimeoutReportHandler { get; private set; }

		// Token: 0x0600019B RID: 411 RVA: 0x0000822E File Offset: 0x0000642E
		public void Dispose()
		{
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00008230 File Offset: 0x00006430
		public void Init(HttpApplication context)
		{
			ExTraceGlobals.WebHealthTracer.TraceDebug(0L, "ExWebHealthModule.Init()");
			context.BeginRequest += this.OnBeginRequest;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00008258 File Offset: 0x00006458
		private void OnBeginRequest(object sender, EventArgs e)
		{
			ExTraceGlobals.WebHealthTracer.TraceDebug(0L, "ExWebHealthModule.OnBeginRequest()");
			HttpContext context = ((HttpApplication)sender).Context;
			if (this.IsLocalRequest(context.Request) && this.IsHealthPageRequest(context.Request))
			{
				try
				{
					try
					{
						ExTraceGlobals.WebHealthTracer.TraceDebug<string>(0L, "ExWebHealthModule.OnBeginRequest() Start request for {0}", HostingEnvironment.ApplicationVirtualPath);
						this.Handler.ProcessHealth(new ExWebHealthResponseWrapper(context.Response));
					}
					catch (Exception arg)
					{
						ExTraceGlobals.WebHealthTracer.TraceError<string, Exception>(0L, "ExWebHealthModule.OnBeginRequest() Encountered exception request for {0}. Error:{1}", HostingEnvironment.ApplicationVirtualPath, arg);
					}
					return;
				}
				finally
				{
					ExTraceGlobals.WebHealthTracer.TraceDebug<string>(0L, "ExWebHealthModule.OnBeginRequest() End request for {0}", HostingEnvironment.ApplicationVirtualPath);
					context.Response.End();
				}
			}
			if (this.IsReportTimeoutPageRequest(context.Request))
			{
				try
				{
					ExTraceGlobals.WebHealthTracer.TraceDebug<string>(0L, "ExWebHealthModule.OnBeginRequest() Start request processing for report timeout page: {0}", context.Request.FilePath);
					this.TimeoutReportHandler.Process(context);
				}
				catch (Exception arg2)
				{
					ExTraceGlobals.WebHealthTracer.TraceError<string, Exception>(0L, "ExWebHealthModule.OnBeginRequest() Encountered exception request for {0}. Error:{1}", context.Request.FilePath, arg2);
				}
				finally
				{
					ExTraceGlobals.WebHealthTracer.TraceDebug<string>(0L, "ExWebHealthModule.OnBeginRequest() End request for {0}", context.Request.FilePath);
					context.Response.End();
				}
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000083C4 File Offset: 0x000065C4
		private bool IsLocalRequest(HttpRequest webRequest)
		{
			return webRequest.IsLocal && string.Equals("localhost", webRequest.Url.Host, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000083E6 File Offset: 0x000065E6
		private bool IsHealthPageRequest(HttpRequest webRequest)
		{
			return string.Equals(webRequest.AppRelativeCurrentExecutionFilePath, "~/exhealth.check", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000083FC File Offset: 0x000065FC
		private bool IsReportTimeoutPageRequest(HttpRequest webRequest)
		{
			string text = webRequest.QueryString["realm"];
			return !string.IsNullOrEmpty(text) && text.Equals("exhealth.reporttimeout", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04000183 RID: 387
		internal const string HealthPageName = "exhealth.check";

		// Token: 0x04000184 RID: 388
		internal const string ReportTimeoutPageName = "exhealth.reporttimeout";

		// Token: 0x04000185 RID: 389
		internal const string HealthPage = "~/exhealth.check";

		// Token: 0x04000186 RID: 390
		private const string LocalHost = "localhost";
	}
}
