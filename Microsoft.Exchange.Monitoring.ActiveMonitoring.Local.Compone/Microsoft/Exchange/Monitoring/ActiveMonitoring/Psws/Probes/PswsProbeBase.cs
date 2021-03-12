using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Psws.Probes
{
	// Token: 0x02000514 RID: 1300
	public class PswsProbeBase : ProbeWorkItem
	{
		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001FFA RID: 8186 RVA: 0x000C33DD File Offset: 0x000C15DD
		protected virtual string ComponentId
		{
			get
			{
				return "PswsProbeBase";
			}
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x000C33E4 File Offset: 0x000C15E4
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.PswsTracer, base.TraceContext, "Entering Dowork", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsProbeBase.cs", 93);
			try
			{
				this.DoInitialize();
				this.DoTask(cancellationToken);
			}
			catch (ApplicationException)
			{
				throw;
			}
			catch (Exception innerException)
			{
				throw new ApplicationException(this.GetProbeInfo() + "Failed in Psws DoWork.", innerException);
			}
			WTFDiagnostics.TraceFunction(ExTraceGlobals.PswsTracer, base.TraceContext, "Leaving Dowork", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsProbeBase.cs", 109);
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x000C347C File Offset: 0x000C167C
		protected virtual void DoInitialize()
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.PswsTracer, base.TraceContext, "Entering DoInitialize", null, "DoInitialize", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsProbeBase.cs", 117);
			this.url = base.Definition.Endpoint;
			this.urlSuffix = base.Definition.Attributes["PswsMailboxUrlSuffix"];
			this.requestUrl = string.Format("{0}{1}", this.url, this.urlSuffix);
			this.probeInfo = this.GetProbeInfo();
			WTFDiagnostics.TraceFunction(ExTraceGlobals.PswsTracer, base.TraceContext, "Leaving DoInitialize", null, "DoInitialize", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsProbeBase.cs", 125);
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x000C3548 File Offset: 0x000C1748
		private void DoTask(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.PswsTracer, base.TraceContext, "Entering DoTask", null, "DoTask", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsProbeBase.cs", 134);
			HttpWebRequest request = this.GetRequest();
			Task<WebResponse> task = Task.Factory.FromAsync<WebResponse>(delegate(AsyncCallback asyncCallback, object state)
			{
				this.startTime = DateTime.UtcNow;
				return request.BeginGetResponse(asyncCallback, state);
			}, new Func<IAsyncResult, WebResponse>(request.EndGetResponse), request);
			task.Continue(new Func<Task<WebResponse>, string>(this.GetResponse), cancellationToken, TaskContinuationOptions.NotOnCanceled);
			WTFDiagnostics.TraceFunction(ExTraceGlobals.PswsTracer, base.TraceContext, "Leaving DoTask", null, "DoTask", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsProbeBase.cs", 149);
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x000C3600 File Offset: 0x000C1800
		private string GetResponse(Task<WebResponse> task)
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.PswsTracer, base.TraceContext, "Entering GetResponse", null, "GetResponse", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsProbeBase.cs", 159);
			string text = null;
			if (!task.IsFaulted)
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)task.Result;
				if (httpWebResponse.StatusCode == HttpStatusCode.OK)
				{
					try
					{
						using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
						{
							text = streamReader.ReadToEnd();
						}
						base.Result.SampleValue = (DateTime.UtcNow - this.startTime).TotalMilliseconds;
						string text2 = string.Format("Psws request success. status : {0} , X-FEServer : {1} , X-CalculatedBETarget : {2} , Response : {3}", new object[]
						{
							httpWebResponse.StatusCode,
							httpWebResponse.Headers["X-FEServer"],
							httpWebResponse.Headers["X-CalculatedBETarget"],
							text
						});
						WTFDiagnostics.TraceInformation(ExTraceGlobals.PswsTracer, base.TraceContext, text2, null, "GetResponse", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsProbeBase.cs", 192);
						base.Result.StateAttribute1 = text2;
						goto IL_1A4;
					}
					catch (Exception ex)
					{
						base.Result.SampleValue = (DateTime.UtcNow - this.startTime).TotalMilliseconds;
						throw new ApplicationException(string.Format("probe information : {0} Read success response body failed , message : {1} . ", this.probeInfo, ex.ToString()));
					}
				}
				base.Result.SampleValue = (DateTime.UtcNow - this.startTime).TotalMilliseconds;
				string failedResponseMessage = this.GetFailedResponseMessage(httpWebResponse, string.Empty);
				throw new ApplicationException(string.Format("Psws response code is not ok , message : ", failedResponseMessage));
			}
			this.HandleFailedRequest(task);
			IL_1A4:
			WTFDiagnostics.TraceFunction(ExTraceGlobals.PswsTracer, base.TraceContext, "Leaving GetResponse", null, "GetResponse", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsProbeBase.cs", 209);
			return text;
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x000C37F4 File Offset: 0x000C19F4
		private void HandleFailedRequest(Task task)
		{
			WTFDiagnostics.TraceError<AggregateException>(ExTraceGlobals.PswsTracer, base.TraceContext, "HandleFailedRequest : Task exception= {0}", task.Exception, null, "HandleFailedRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsProbeBase.cs", 219);
			if (task.Exception == null)
			{
				WTFDiagnostics.TraceFunction(ExTraceGlobals.PswsTracer, base.TraceContext, "Leaving HandleFailedRequest", null, "HandleFailedRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsProbeBase.cs", 239);
				return;
			}
			WebException ex = task.Exception.Flatten().InnerException as WebException;
			if (ex != null && ex.Response != null && ex.Response is HttpWebResponse)
			{
				HttpWebResponse response = ex.Response as HttpWebResponse;
				string failedResponseMessage = this.GetFailedResponseMessage(response, ex.ToString());
				throw new ApplicationException(failedResponseMessage, ex);
			}
			throw new ApplicationException(string.Format("probe information : {0} UnWebException catched! Message : {1}", this.probeInfo, task.Exception.Flatten().ToString()));
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x000C38D0 File Offset: 0x000C1AD0
		private string GetFailedResponseMessage(HttpWebResponse response, string exceptionMessage)
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.PswsTracer, base.TraceContext, "Entering GetFailedResponseMessage", null, "GetFailedResponseMessage", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsProbeBase.cs", 248);
			string text = string.Empty;
			try
			{
				using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
				{
					text = streamReader.ReadToEnd();
				}
			}
			catch (Exception ex)
			{
				text = string.Format("Read exception response body failed , message : {0} . ", ex.ToString());
			}
			string str = string.Format("Psws request error. Error status : {0} , X-FEServer : {1} , X-CalculatedBETarget : {2} , X-DiagInfo : {3} , Error message : {4} , Exception string: {5}", new object[]
			{
				response.StatusCode,
				response.Headers["X-FEServer"],
				response.Headers["X-CalculatedBETarget"],
				response.Headers["X-DiagInfo"],
				text,
				exceptionMessage
			});
			WTFDiagnostics.TraceFunction(ExTraceGlobals.PswsTracer, base.TraceContext, "Leaving GetFailedResponseMessage", null, "GetFailedResponseMessage", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsProbeBase.cs", 274);
			return this.probeInfo + str;
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x000C39F4 File Offset: 0x000C1BF4
		private string GetProbeInfo()
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.PswsTracer, base.TraceContext, "Entering GetProbeInfo", null, "GetProbeInfo", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsProbeBase.cs", 284);
			string result = string.Format("Probe Information : Endpoint = {0} , Account = {1} , RequestURL = {2}. ", base.Definition.Endpoint, base.Definition.Account, this.requestUrl);
			WTFDiagnostics.TraceFunction(ExTraceGlobals.PswsTracer, base.TraceContext, "Leaving GetProbeInfo", null, "GetProbeInfo", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsProbeBase.cs", 293);
			return result;
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x000C3A74 File Offset: 0x000C1C74
		protected virtual HttpWebRequest GetRequest()
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.requestUrl);
			httpWebRequest.UserAgent = PswsProbeBase.DefaultUserAgent;
			httpWebRequest.AllowAutoRedirect = false;
			httpWebRequest.PreAuthenticate = true;
			httpWebRequest.Headers.Add(PswsProbeBase.ClientApplication, ExchangeRunspaceConfigurationSettings.ExchangeApplication.ActiveMonitor.ToString());
			return httpWebRequest;
		}

		// Token: 0x04001775 RID: 6005
		public static readonly string PswsMailboxUrlSuffixAttrName = "PswsMailboxUrlSuffix";

		// Token: 0x04001776 RID: 6006
		public static readonly string AccessTokenTypeAttrName = "AccessTokenType";

		// Token: 0x04001777 RID: 6007
		internal static readonly string DefaultUserAgent = "PswsMonitor/1.0";

		// Token: 0x04001778 RID: 6008
		internal static readonly string ClientApplication = "clientApplication";

		// Token: 0x04001779 RID: 6009
		protected string probeInfo;

		// Token: 0x0400177A RID: 6010
		protected string url;

		// Token: 0x0400177B RID: 6011
		protected string urlSuffix;

		// Token: 0x0400177C RID: 6012
		protected string requestUrl;

		// Token: 0x0400177D RID: 6013
		protected DateTime startTime;
	}
}
