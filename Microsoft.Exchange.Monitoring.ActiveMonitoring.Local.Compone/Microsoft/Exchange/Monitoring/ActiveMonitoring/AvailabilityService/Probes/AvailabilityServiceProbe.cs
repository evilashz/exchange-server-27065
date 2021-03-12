using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Calendar.Probes;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Ews.Probes;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.AvailabilityService.Probes
{
	// Token: 0x02000018 RID: 24
	public class AvailabilityServiceProbe : CalendarProbeBase
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00008B92 File Offset: 0x00006D92
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00008B9A File Offset: 0x00006D9A
		public string ProbeErrorMessage { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00008BA3 File Offset: 0x00006DA3
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00008BAB File Offset: 0x00006DAB
		public bool IsProbeFailed { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00008BB4 File Offset: 0x00006DB4
		protected override string ComponentId
		{
			get
			{
				return "AvailabilityService_AM_Probe";
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00008BBB File Offset: 0x00006DBB
		protected override void Configure()
		{
			base.Configure();
			this.AppendKnownErrorCodes();
			base.ConfigureResultName();
			this.traceListener = new EWSCommon.TraceListener(base.TraceContext, ExTraceGlobals.AvailabilityServiceTracer);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00008BE5 File Offset: 0x00006DE5
		protected override void DoWork(CancellationToken cancellationToken)
		{
			base.Initialize(ExTraceGlobals.AvailabilityServiceTracer);
			this.DoWorkInternal(cancellationToken);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00008BFC File Offset: 0x00006DFC
		protected virtual void DoWorkInternal(CancellationToken cancellationToken)
		{
			try
			{
				base.LogTrace(string.Format("Executing FreeBusy AM probe with {0}", base.EffectiveAuthN));
				base.LatencyMeasurement.Start();
				this.ExecuteFreeBusyProbe(base.Definition.Endpoint);
			}
			catch (Exception innerException)
			{
				while (innerException.InnerException != null)
				{
					innerException = innerException.InnerException;
				}
				ProbeResult result = base.Result;
				result.ExecutionContext += innerException.Message;
				if (string.IsNullOrEmpty(this.probeErrorCode))
				{
					this.probeErrorCode = innerException.GetType().Name;
				}
				if (string.IsNullOrEmpty(this.ProbeErrorMessage))
				{
					this.ProbeErrorMessage = innerException.Message;
				}
			}
			finally
			{
				this.VerifyFreeBusyProbeError();
				this.UpdateProbeResultAttributes();
				this.ThrowProbeError();
			}
			base.LogTrace("FreeBusyProbe succeeded");
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00008CE8 File Offset: 0x00006EE8
		protected virtual void VerifyFreeBusyProbeError()
		{
			if (this.probeErrorCode == 0.ToString())
			{
				this.IsProbeFailed = false;
				return;
			}
			this.BucketIssues();
			if (base.Result.FailureCategory == 7 || base.Result.FailureCategory == 2 || base.Result.FailureCategory == 5)
			{
				this.IsProbeFailed = false;
				return;
			}
			this.IsProbeFailed = true;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00008D54 File Offset: 0x00006F54
		private void BucketIssues()
		{
			if (string.IsNullOrEmpty(this.ProbeErrorMessage))
			{
				base.Result.FailureCategory = 1;
				return;
			}
			if (this.ProbeErrorMessage.ToLower().Contains("the request was aborted") || this.ProbeErrorMessage.ToLower().Contains("the server cannot service this request right now") || this.ProbeErrorMessage.ToLower().Contains("the xml document ended unexpectedly"))
			{
				this.probeErrorCode = "Server Too Busy";
			}
			if (this.ProbeErrorMessage.ToLower().Contains("(401) unauthorized") && this.ProbeErrorMessage.ToLower().Contains("autodiscover"))
			{
				this.probeErrorCode = "AutoDiscover Failed";
			}
			base.LatencyMeasurement.Stop();
			long elapsedMilliseconds = base.LatencyMeasurement.ElapsedMilliseconds;
			if (elapsedMilliseconds > (long)(base.ProbeTimeLimit - 1000) && (string.IsNullOrEmpty(this.probeErrorCode) || string.Compare(this.probeErrorCode, "Unknown", true) == 0))
			{
				this.probeErrorCode = "Probe Time Out";
			}
			if (AvailabilityServiceProbeUtil.KnownErrors.ContainsKey(this.probeErrorCode))
			{
				base.Result.FailureCategory = (int)AvailabilityServiceProbeUtil.KnownErrors[this.probeErrorCode];
				return;
			}
			base.Result.FailureCategory = 1;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00008E90 File Offset: 0x00007090
		protected virtual void UpdateProbeResultAttributes()
		{
			if (!this.probeErrorCode.Equals(string.Empty))
			{
				base.Result.StateAttribute1 = ((AvailabilityServiceProbeUtil.FailingComponent)base.Result.FailureCategory).ToString();
				base.Result.StateAttribute2 = this.probeErrorCode;
			}
			base.Result.StateAttribute3 = base.Result.StateAttribute12;
			base.Result.StateAttribute12 = base.Definition.Account;
			base.Result.StateAttribute4 = base.Result.StateAttribute13;
			base.Result.StateAttribute13 = base.Definition.SecondaryAccount;
			base.Result.StateAttribute5 = base.Definition.TargetResource;
			ProbeResult result = base.Result;
			result.ExecutionContext += base.TransformResultPairsToString(base.VitalResultPairs);
			if (base.Verbose)
			{
				ProbeResult result2 = base.Result;
				result2.ExecutionContext += base.TransformResultPairsToString(base.VerboseResultPairs);
				ProbeResult result3 = base.Result;
				result3.FailureContext += base.TraceBuilder;
			}
			if (this.exchangeService == null)
			{
				WTFDiagnostics.TraceError(base.Tracer, base.TraceContext, "[AvailabilityServiceProbe.UpdateProbeResultAttributes] this.exchangeService should not have been null!", null, "UpdateProbeResultAttributes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\AvailabilityService\\availabilityserviceprobe.cs", 280);
			}
			else
			{
				base.Result.StateAttribute24 = this.exchangeService.ClientRequestId;
			}
			base.LatencyMeasurement.Stop();
			base.Result.SampleValue = (double)base.LatencyMeasurement.ElapsedMilliseconds;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000901E File Offset: 0x0000721E
		protected virtual void ThrowProbeError()
		{
			if (this.IsProbeFailed)
			{
				throw new AvailabilityServiceValidationException(this.ProbeErrorMessage);
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00009034 File Offset: 0x00007234
		private void ExecuteFreeBusyProbe(string endPoint)
		{
			base.LogTrace(string.Format("Starting FreeBusyProbe with Username: {0} ", base.Definition.Account));
			this.ConfigureExchangeVersion(ExTraceGlobals.AvailabilityServiceTracer);
			this.exchangeService = base.GetExchangeService(base.Definition.Account, base.Definition.AccountPassword, this.traceListener, base.GetEwsUrl(this.traceListener, "SourceEwsUrl"), base.ExchangeServerVersion);
			this.exchangeService.SetComponentId(this.ComponentId);
			this.exchangeService.HttpHeaders.Add("X-ProbeType", "X-MS-Backend-AvailabilityService-Probe");
			TimeWindow window = new TimeWindow(DateTime.UtcNow, DateTime.UtcNow.AddDays(1.0));
			IEnumerable<AttendeeInfo> attendees = new List<AttendeeInfo>
			{
				new AttendeeInfo(base.Definition.SecondaryAccount)
			};
			this.PerformFreeBusyCall(window, attendees);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00009268 File Offset: 0x00007468
		private void PerformFreeBusyCall(TimeWindow window, IEnumerable<AttendeeInfo> attendees)
		{
			this.exchangeService.ClientRequestId = Guid.NewGuid().ToString();
			this.exchangeService.ReturnClientRequestId = true;
			base.RetrySoapActionAndThrow(delegate()
			{
				GetUserAvailabilityResults userAvailability = this.exchangeService.GetUserAvailability(attendees, window, 2);
				if (userAvailability != null && userAvailability.AttendeesAvailability != null && userAvailability.AttendeesAvailability.Count > 0)
				{
					this.probeErrorCode = userAvailability.AttendeesAvailability[0].ErrorCode.ToString();
					if (userAvailability.AttendeesAvailability[0].Result != null)
					{
						this.ProbeErrorMessage = userAvailability.AttendeesAvailability[0].ErrorMessage;
						int val = 1024;
						string text = this.exchangeService.HttpResponseHeaders["X-DEBUG_BE_ASGenericInfo"];
						if (!string.IsNullOrEmpty(text))
						{
							text = HttpUtility.HtmlDecode(text);
							this.Result.StateAttribute15 = text.Substring(0, Math.Min(val, text.Length));
						}
						text = this.exchangeService.HttpResponseHeaders["X-DEBUG_BE_GenericErrors"];
						if (!string.IsNullOrEmpty(text))
						{
							text = HttpUtility.HtmlDecode(text);
							this.Result.StateAttribute22 = text.Substring(0, Math.Min(val, text.Length));
						}
					}
				}
			}, "GetUserAvailability", this.exchangeService);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000092FC File Offset: 0x000074FC
		private void AppendKnownErrorCodes()
		{
			string text = this.ReadAttribute("KnownErrorCodes", string.Empty);
			List<string> errorCodesToBeIgnored = new List<string>();
			if (!string.IsNullOrEmpty(text))
			{
				char[] separator = new char[]
				{
					','
				};
				text.Split(separator, 100).ToList<string>().ForEach(delegate(string r)
				{
					errorCodesToBeIgnored.Add(r.Trim());
				});
				errorCodesToBeIgnored.RemoveAll((string r) => string.IsNullOrWhiteSpace(r));
			}
			foreach (string key in errorCodesToBeIgnored)
			{
				AvailabilityServiceProbeUtil.KnownErrors.Add(key, AvailabilityServiceProbeUtil.FailingComponent.Ignore);
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000093E4 File Offset: 0x000075E4
		private void ConfigureExchangeVersion(Trace tracer)
		{
			base.ExchangeServerVersion = 0;
			if (base.Definition.Attributes != null && base.Definition.Attributes.ContainsKey("ExchangeVersion"))
			{
				string text = base.Definition.Attributes["ExchangeVersion"] ?? "Invalid";
				if (!text.Equals(this.exchVerExchange2013))
				{
					throw new ArgumentException("An invalid ExchangeVersion was passed in from the extended attributes. '" + text + "'", "ExchangeVersion");
				}
				base.ExchangeServerVersion = 4;
			}
			WTFDiagnostics.TraceInformation(tracer, base.TraceContext, "Probe ExchangeVersion: " + base.ExchangeServerVersion.ToString(), null, "ConfigureExchangeVersion", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\AvailabilityService\\availabilityserviceprobe.cs", 419);
		}

		// Token: 0x040000AC RID: 172
		private const string OperationName = "GetUserAvailability";

		// Token: 0x040000AD RID: 173
		private const string ClientRequestIDName = "client-request-id";

		// Token: 0x040000AE RID: 174
		public const string ProbeHeaderName = "X-ProbeType";

		// Token: 0x040000AF RID: 175
		public const string ProbeHeaderValue = "X-MS-Backend-AvailabilityService-Probe";

		// Token: 0x040000B0 RID: 176
		private readonly string exchVerExchange2013 = 4.ToString();

		// Token: 0x040000B1 RID: 177
		private ExchangeService exchangeService;

		// Token: 0x040000B2 RID: 178
		private EWSCommon.TraceListener traceListener;

		// Token: 0x040000B3 RID: 179
		protected string probeErrorCode = string.Empty;
	}
}
