using System;
using System.Web;
using System.Web.Configuration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000284 RID: 644
	public class PerformanceConsoleModule : IHttpModule
	{
		// Token: 0x06002A42 RID: 10818 RVA: 0x000842D4 File Offset: 0x000824D4
		void IHttpModule.Init(HttpApplication application)
		{
			this.showPerformanceConsole = StringComparer.OrdinalIgnoreCase.Equals("true", WebConfigurationManager.AppSettings["ShowPerformanceConsole"]);
			if (this.showPerformanceConsole)
			{
				EcpEventLogConstants.Tuple_EcpPerformanceConsoleEnabled.LogEvent(new object[0]);
			}
			this.logPerformanceData = StringComparer.OrdinalIgnoreCase.Equals("true", WebConfigurationManager.AppSettings["LogPerformanceData"]);
			if (this.logPerformanceData)
			{
				EcpEventLogConstants.Tuple_EcpPerformanceIisLogEnabled.LogEvent(new object[0]);
			}
			if (EcpEventLogConstants.Tuple_EcpPerformanceEventLogHighEnabled.IsEnabled())
			{
				EcpEventLogConstants.Tuple_EcpPerformanceEventLogHighEnabled.LogEvent(new object[0]);
			}
			else
			{
				EcpEventLogConstants.Tuple_EcpPerformanceEventLogMediumEnabled.LogEvent(new object[0]);
			}
			application.BeginRequest += this.Application_BeginRequest;
			application.PostAuthenticateRequest += this.Application_PostAuthenticateRequest;
			application.PostAuthorizeRequest += this.Application_PostAuthorizeRequest;
			application.PostResolveRequestCache += this.Application_PostResolveRequestCache;
			application.PostMapRequestHandler += this.Application_PostMapRequestHandler;
			application.PostAcquireRequestState += this.Application_PostAcquireRequestState;
			application.PostRequestHandlerExecute += this.Application_PostRequestHandlerExecute;
			application.PostReleaseRequestState += this.Application_PostReleaseRequestState;
			application.PostUpdateRequestCache += this.Application_PostUpdateRequestCache;
			application.PostLogRequest += this.Application_PostLogRequest;
			application.EndRequest += this.Application_EndRequest;
			application.PreSendRequestContent += this.Application_PreSendRequestContent;
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x0008445F File Offset: 0x0008265F
		void IHttpModule.Dispose()
		{
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x00084461 File Offset: 0x00082661
		private void Application_BeginRequest(object sender, EventArgs e)
		{
			PerfRecord.Current = new PerfRecord(HttpContext.Current.Request.Path);
			PerfRecord.Current.StepStarted(RequestNotification.AuthenticateRequest);
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x00084487 File Offset: 0x00082687
		private void Application_PostAuthenticateRequest(object sender, EventArgs e)
		{
			PerfRecord.Current.StepStarted(RequestNotification.AuthorizeRequest);
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x00084494 File Offset: 0x00082694
		private void Application_PostAuthorizeRequest(object sender, EventArgs e)
		{
			PerfRecord.Current.StepStarted(RequestNotification.ResolveRequestCache);
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x000844A1 File Offset: 0x000826A1
		private void Application_PostResolveRequestCache(object sender, EventArgs e)
		{
			PerfRecord.Current.StepStarted(RequestNotification.MapRequestHandler);
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x000844AF File Offset: 0x000826AF
		private void Application_PostMapRequestHandler(object sender, EventArgs e)
		{
			PerfRecord.Current.StepStarted(RequestNotification.AcquireRequestState);
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x000844BD File Offset: 0x000826BD
		private void Application_PostAcquireRequestState(object sender, EventArgs e)
		{
			PerfRecord.Current.StepStarted(RequestNotification.ExecuteRequestHandler);
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x000844CE File Offset: 0x000826CE
		private void Application_PostRequestHandlerExecute(object sender, EventArgs e)
		{
			PerfRecord.Current.StepStarted(RequestNotification.ReleaseRequestState);
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x000844DF File Offset: 0x000826DF
		private void Application_PostReleaseRequestState(object sender, EventArgs e)
		{
			PerfRecord.Current.StepStarted(RequestNotification.UpdateRequestCache);
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x000844F0 File Offset: 0x000826F0
		private void Application_PostUpdateRequestCache(object sender, EventArgs e)
		{
			PerfRecord.Current.StepStarted(RequestNotification.LogRequest);
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x00084501 File Offset: 0x00082701
		private void Application_PostLogRequest(object sender, EventArgs e)
		{
			if (PerfRecord.Current != null)
			{
				PerfRecord.Current.StepCompleted();
			}
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x00084514 File Offset: 0x00082714
		private void Application_EndRequest(object sender, EventArgs e)
		{
			if (PerfRecord.Current != null)
			{
				PerfRecord.Current.EndRequest();
				if (this.logPerformanceData)
				{
					PerfRecord.Current.AppendToIisLog();
				}
			}
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x0008453C File Offset: 0x0008273C
		private void Application_PreSendRequestContent(object sender, EventArgs e)
		{
			if (!this.showPerformanceConsole)
			{
				if (!this.logPerformanceData)
				{
					return;
				}
			}
			try
			{
				if (PerfRecord.Current != null)
				{
					HttpContext.Current.Response.AppendHeader("X-msExchPerf", PerfRecord.Current.ToString());
				}
			}
			catch (HttpException)
			{
			}
		}

		// Token: 0x04002124 RID: 8484
		private bool logPerformanceData;

		// Token: 0x04002125 RID: 8485
		private bool showPerformanceConsole;
	}
}
