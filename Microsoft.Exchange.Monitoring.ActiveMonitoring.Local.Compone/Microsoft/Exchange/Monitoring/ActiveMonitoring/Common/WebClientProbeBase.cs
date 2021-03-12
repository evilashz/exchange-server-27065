using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Net.MonitoringWebClient;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200009F RID: 159
	public abstract class WebClientProbeBase : ProbeWorkItem
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x00021924 File Offset: 0x0001FB24
		internal virtual SslValidationOptions DefaultSslValidationOptions
		{
			get
			{
				return SslValidationOptions.NoSslValidation;
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00021928 File Offset: 0x0001FB28
		public override void PopulateDefinition<ProbeDefinition>(ProbeDefinition pDef, Dictionary<string, string> propertyBag)
		{
			if (pDef == null)
			{
				throw new ArgumentException("Please specify a value for probeDefinition");
			}
			if (propertyBag.ContainsKey("SslValidationOptions"))
			{
				pDef.Attributes["SslValidationOptions"] = propertyBag["SslValidationOptions"].ToString().Trim();
				return;
			}
			throw new ArgumentException("Please specify value forSslValidationOptions");
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0002198C File Offset: 0x0001FB8C
		internal virtual IHttpSession CreateHttpSession()
		{
			IRequestAdapter requestAdapter = this.CreateRequestAdapter();
			IExceptionAnalyzer exceptionAnalyzer = this.CreateExceptionAnalyzer();
			IResponseTracker responseTracker = this.CreateResponseTracker();
			IHttpSession httpSession = new HttpSession(requestAdapter, exceptionAnalyzer, responseTracker);
			httpSession.UserAgent = "Mozilla/4.0 (compatible; MSIE 9.0; Windows NT 6.1; MSEXCHMON; ACTIVEMONITORING)";
			httpSession.PersistentHeaders[WellKnownHeader.FrontEndToBackEndTimeout] = WebClientProbeBase.FrontEndToBackEndTimeout.TotalSeconds.ToString();
			SslValidationOptions sslValidationOptions;
			if (base.Definition.Attributes.ContainsKey("SslValidationOptions") && Enum.TryParse<SslValidationOptions>(base.Definition.Attributes["SslValidationOptions"], true, out sslValidationOptions))
			{
				httpSession.SslValidationOptions = sslValidationOptions;
			}
			else
			{
				httpSession.SslValidationOptions = this.DefaultSslValidationOptions;
			}
			return httpSession;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00021A38 File Offset: 0x0001FC38
		internal virtual IRequestAdapter CreateRequestAdapter()
		{
			return new RequestAdapter
			{
				RequestTimeout = WebClientProbeBase.RequestTimeout
			};
		}

		// Token: 0x06000590 RID: 1424
		internal abstract IExceptionAnalyzer CreateExceptionAnalyzer();

		// Token: 0x06000591 RID: 1425 RVA: 0x00021A57 File Offset: 0x0001FC57
		internal virtual IResponseTracker CreateResponseTracker()
		{
			return new ResponseTracker();
		}

		// Token: 0x06000592 RID: 1426
		internal abstract Task ExecuteScenario(IHttpSession session);

		// Token: 0x06000593 RID: 1427
		internal abstract void ScenarioSucceeded(Task scenarioTask);

		// Token: 0x06000594 RID: 1428
		internal abstract void ScenarioFailed(Task scenarioTask);

		// Token: 0x06000595 RID: 1429
		internal abstract void ScenarioCancelled(Task scenarioTask);

		// Token: 0x06000596 RID: 1430 RVA: 0x00021A60 File Offset: 0x0001FC60
		internal virtual void TestStepStarted(object sender, TestEventArgs eventArgs)
		{
			this.TraceInformation("Test step started: {0}", new object[]
			{
				eventArgs.TestId
			});
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00021A90 File Offset: 0x0001FC90
		internal virtual void TestStepFinished(object sender, TestEventArgs eventArgs)
		{
			this.TraceInformation("Test step finished: {0}", new object[]
			{
				eventArgs.TestId
			});
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00021AC0 File Offset: 0x0001FCC0
		internal virtual void RequestSent(object sender, HttpWebEventArgs eventArgs)
		{
			this.TraceInformation("Request being sent to url: {0}{2}{2}{1}", new object[]
			{
				eventArgs.Request.RequestUri.AbsoluteUri,
				eventArgs.Request.ToStringNoBody(),
				Environment.NewLine
			});
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00021B0C File Offset: 0x0001FD0C
		internal virtual void ResponseReceived(object sender, HttpWebEventArgs eventArgs)
		{
			this.TraceInformation("Response received from url {0}, Status Code = {1}, Responding Server = {2}:{4}{4}{3}", new object[]
			{
				eventArgs.Request.RequestUri.AbsoluteUri,
				eventArgs.Response.StatusCode,
				eventArgs.Response.RespondingFrontEndServer,
				eventArgs.Response.ToStringNoBody(),
				Environment.NewLine
			});
		}

		// Token: 0x0600059A RID: 1434
		internal abstract void TraceInformation(string message, params object[] parameters);

		// Token: 0x0600059B RID: 1435 RVA: 0x00021B78 File Offset: 0x0001FD78
		protected sealed override void DoWork(CancellationToken cancellationToken)
		{
			IHttpSession httpSession = this.CreateHttpSession();
			httpSession.TestStarted += this.TestStepStarted;
			httpSession.TestFinished += this.TestStepFinished;
			httpSession.SendingRequest += this.RequestSent;
			httpSession.ResponseReceived += this.ResponseReceived;
			Task task = this.ExecuteScenario(httpSession);
			task.ContinueWith(new Action<Task>(this.ScenarioSucceeded), TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
			task.ContinueWith(new Action<Task>(this.ScenarioFailed), TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnCanceled);
			task.ContinueWith(new Action<Task>(this.ScenarioCancelled), TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnFaulted);
		}

		// Token: 0x04000392 RID: 914
		public const string SslValidationOptionsParameterName = "SslValidationOptions";

		// Token: 0x04000393 RID: 915
		protected static readonly TimeSpan RequestTimeout = TimeSpan.FromMinutes(2.0);

		// Token: 0x04000394 RID: 916
		protected static readonly TimeSpan FrontEndToBackEndTimeout = TimeSpan.FromSeconds(100.0);
	}
}
