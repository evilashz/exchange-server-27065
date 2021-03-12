using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.CalendarSharing.Probes;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Ews.Probes;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.OnlineMeeting.Probes
{
	// Token: 0x02000256 RID: 598
	internal abstract class OnlineMeetingCreateProbe : AutodiscoverCommon
	{
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060010C4 RID: 4292 RVA: 0x0006F9E4 File Offset: 0x0006DBE4
		// (set) Token: 0x060010C5 RID: 4293 RVA: 0x0006F9EC File Offset: 0x0006DBEC
		protected HttpWebRequestUtility WebRequestUtil { get; set; }

		// Token: 0x060010C6 RID: 4294 RVA: 0x0006F9F8 File Offset: 0x0006DBF8
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				base.Initialize(ExTraceGlobals.OnlineMeetingTracer);
				this.traceListener = new EWSCommon.TraceListener(base.TraceContext, ExTraceGlobals.OnlineMeetingTracer);
				string ewsUrl;
				try
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.OnlineMeetingTracer, base.TraceContext, "Getting Source EWS Url", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OnlineMeeting\\OnlineMeetingCreateProbe.cs", 66);
					ewsUrl = base.GetEwsUrl(this.traceListener, base.Definition.Endpoint);
					WTFDiagnostics.TraceInformation(ExTraceGlobals.OnlineMeetingTracer, base.TraceContext, "Returned Source EWS Url: " + ewsUrl, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OnlineMeeting\\OnlineMeetingCreateProbe.cs", 68);
				}
				catch
				{
					base.Result.StateAttribute5 = "Autodiscover failed";
					throw;
				}
				ExchangeService exchangeService = base.GetExchangeService(base.Definition.Account, base.Definition.AccountPassword, this.traceListener, ewsUrl, base.ExchangeServerVersion);
				if (!string.IsNullOrEmpty(this.ComponentId) && !base.IsOutsideInMonitoring)
				{
					exchangeService.SetComponentId(this.ComponentId);
				}
				Appointment appointment = CalendarSharingUtils.CreateTestAppointment(exchangeService);
				string userSipUri = base.Definition.Attributes["UMMbxAccountSipUri"];
				NetworkCredential credential = this.GetCredential(base.Definition.Account, base.Definition.AccountPassword);
				this.CreateOnlineMeeting(credential, appointment.Id.ToString(), userSipUri);
			}
			catch (AggregateException ex)
			{
				WebException ex2 = ex.Flatten().InnerException as WebException;
				HttpWebResponse httpWebResponse = (HttpWebResponse)ex2.Response;
				base.Result.StateAttribute5 = httpWebResponse.StatusCode.ToString();
				throw ex2;
			}
			catch (Exception ex3)
			{
				base.Result.StateAttribute15 = "Exception = " + ex3.ToString();
				throw ex3;
			}
			finally
			{
				base.Result.StateAttribute11 = string.Format("BE Server: {0} FE Server: {1}", "X-DiagInfo", "X-FEServer");
			}
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0006FE84 File Offset: 0x0006E084
		private void CreateOnlineMeeting(NetworkCredential credential, string calendarItemId, string userSipUri)
		{
			HttpWebRequest ewsRequest = this.GetEwsRequest(credential, calendarItemId, userSipUri);
			OnlineMeetingCreateProbe.RequestState requestState = new OnlineMeetingCreateProbe.RequestState
			{
				WebRequest = ewsRequest,
				SentTimeUtc = DateTime.UtcNow
			};
			ewsRequest.BeginGetResponse(delegate(IAsyncResult x)
			{
				OnlineMeetingCreateProbe.RequestState requestState2 = (OnlineMeetingCreateProbe.RequestState)x.AsyncState;
				try
				{
					using (HttpWebResponse httpWebResponse = (HttpWebResponse)requestState2.WebRequest.EndGetResponse(x))
					{
						requestState2.ResponseReceivedTimeUtc = DateTime.UtcNow;
						requestState2.StatusCode = httpWebResponse.StatusCode.ToString();
						string text = "X-Exchange-GetUserPhoto-Traces";
						if (httpWebResponse.Headers.AllKeys != null && httpWebResponse.Headers.AllKeys.Count<string>() != 0 && httpWebResponse.Headers.AllKeys.Contains(text))
						{
							base.Result.ExecutionContext = httpWebResponse.Headers[text];
						}
						base.Result.StateAttribute1 = base.Definition.Endpoint;
						base.Result.StateAttribute5 = httpWebResponse.StatusCode.ToString();
						using (Stream responseStream = httpWebResponse.GetResponseStream())
						{
							StreamReader streamReader = new StreamReader(responseStream);
							base.Result.StateAttribute2 = streamReader.ReadToEnd();
						}
					}
				}
				catch (WebException ex)
				{
					requestState2.StatusCode = ex.Status.ToString();
					requestState2.ResponseReceivedTimeUtc = DateTime.UtcNow;
					requestState2.Exception = ex;
				}
				catch (Exception exception)
				{
					requestState2.StatusCode = WebExceptionStatus.UnknownError.ToString();
					requestState2.ResponseReceivedTimeUtc = DateTime.UtcNow;
					requestState2.Exception = exception;
				}
				finally
				{
					base.Result.StateAttribute12 = "Sent Time = " + requestState2.SentTimeUtc.ToString();
					base.Result.StateAttribute13 = "Received Time = " + requestState2.ResponseReceivedTimeUtc.ToString();
					base.Result.StateAttribute14 = "Status Code = " + requestState2.StatusCode.ToString();
					this.allDone.Set();
				}
			}, requestState);
			this.allDone.WaitOne();
			if (requestState.Exception != null)
			{
				throw requestState.Exception;
			}
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0006FEE4 File Offset: 0x0006E0E4
		private NetworkCredential GetCredential(string userName, string password)
		{
			NetworkCredential networkCredential = new NetworkCredential
			{
				UserName = userName,
				Password = password
			};
			if (userName.Contains("@"))
			{
				string[] array = userName.Split(new char[]
				{
					'@'
				});
				if (array.Length == 2)
				{
					networkCredential.UserName = array[0];
					networkCredential.Domain = array[1];
				}
			}
			return networkCredential;
		}

		// Token: 0x060010C9 RID: 4297
		protected abstract HttpWebRequest GetEwsRequest(NetworkCredential credential, string calendarItemId, string userSipUri);

		// Token: 0x04000C93 RID: 3219
		protected const string CompleteEndpointStr = "{0}?sipUri={1},itemId={2}";

		// Token: 0x04000C94 RID: 3220
		public const string MailboxDatabaseSipUriParameterName = "UMMbxAccountSipUri";

		// Token: 0x04000C95 RID: 3221
		protected ManualResetEvent allDone = new ManualResetEvent(false);

		// Token: 0x04000C96 RID: 3222
		protected EWSCommon.TraceListener traceListener;

		// Token: 0x02000257 RID: 599
		private class RequestState
		{
			// Token: 0x17000338 RID: 824
			// (get) Token: 0x060010CC RID: 4300 RVA: 0x0006FF55 File Offset: 0x0006E155
			// (set) Token: 0x060010CD RID: 4301 RVA: 0x0006FF5D File Offset: 0x0006E15D
			public DateTime SentTimeUtc { get; set; }

			// Token: 0x17000339 RID: 825
			// (get) Token: 0x060010CE RID: 4302 RVA: 0x0006FF66 File Offset: 0x0006E166
			// (set) Token: 0x060010CF RID: 4303 RVA: 0x0006FF6E File Offset: 0x0006E16E
			public DateTime ResponseReceivedTimeUtc { get; set; }

			// Token: 0x1700033A RID: 826
			// (get) Token: 0x060010D0 RID: 4304 RVA: 0x0006FF77 File Offset: 0x0006E177
			// (set) Token: 0x060010D1 RID: 4305 RVA: 0x0006FF7F File Offset: 0x0006E17F
			public HttpWebRequest WebRequest { get; set; }

			// Token: 0x1700033B RID: 827
			// (get) Token: 0x060010D2 RID: 4306 RVA: 0x0006FF88 File Offset: 0x0006E188
			// (set) Token: 0x060010D3 RID: 4307 RVA: 0x0006FF90 File Offset: 0x0006E190
			public Exception Exception { get; set; }

			// Token: 0x1700033C RID: 828
			// (get) Token: 0x060010D4 RID: 4308 RVA: 0x0006FF99 File Offset: 0x0006E199
			// (set) Token: 0x060010D5 RID: 4309 RVA: 0x0006FFA1 File Offset: 0x0006E1A1
			public string StatusCode { get; set; }
		}
	}
}
