using System;
using System.Diagnostics.Eventing.Reader;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.FfoExchangeFileUploader
{
	// Token: 0x02000074 RID: 116
	public class FfoExchangeFileUploaderProbe : ProbeWorkItem
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x00011A40 File Offset: 0x0000FC40
		protected override void DoWork(CancellationToken cancellationToken)
		{
			base.Result.ExecutionContext = string.Format("FfoExchangeFileUploaderProbe started at {0}.{1}", DateTime.UtcNow, Environment.NewLine);
			WTFDiagnostics.TraceFunction(ExTraceGlobals.CentralAdminTracer, base.TraceContext, "In FfoExchangeFileUploaderProbe", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Datamining\\Probes\\FfoExchangeFileUploaderProbe.cs", 52);
			this.CheckTransformationState();
			this.CheckCosmosDataLoaderState();
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00011AA0 File Offset: 0x0000FCA0
		private static string SearchEventLog(string providerName, int eventId, TimeSpan timeSpan)
		{
			string query = string.Format("<QueryList><Query Id='0' Path='Application'><Select Path='Application'>*[System[Provider[@Name='{0}'] and (EventID={1}) and TimeCreated[timediff(@SystemTime) &lt;= {2}]]]</Select></Query></QueryList>", providerName, eventId, timeSpan.TotalMilliseconds);
			EventLogQuery eventQuery = new EventLogQuery("Application", PathType.LogName, query);
			using (EventLogReader eventLogReader = new EventLogReader(eventQuery))
			{
				using (EventRecord eventRecord = eventLogReader.ReadEvent())
				{
					if (eventRecord != null)
					{
						return eventRecord.ToXml();
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00011B30 File Offset: 0x0000FD30
		private void CheckTransformationState()
		{
			string text = FfoExchangeFileUploaderProbe.SearchEventLog("Exchange File Uploader", 1012, FfoExchangeFileUploaderProbe.alertFrame);
			if (!string.IsNullOrEmpty(text))
			{
				ProbeResult result = base.Result;
				result.ExecutionContext += string.Format("Last Transformation Log Event: {0}.\r\n", text);
				return;
			}
			string value = FfoExchangeFileUploaderProbe.SearchEventLog("MSExchangeFileUpload", 0, FfoExchangeFileUploaderProbe.alertFrame);
			if (string.IsNullOrEmpty(value))
			{
				base.Result.FailureContext = "MSExchangeFileUpload service has not logged a transformation event in the last hour";
				base.Result.Error = "MSExchangeFileUpload service has not logged a transformation event in the last hour";
				throw new Exception("MSExchangeFileUpload service has not logged a transformation event in the last hour");
			}
			ProbeResult result2 = base.Result;
			result2.ExecutionContext += string.Format("Uploader Start Log Event: {0}.\r\n", text);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00011BE4 File Offset: 0x0000FDE4
		private void CheckCosmosDataLoaderState()
		{
			string text = FfoExchangeFileUploaderProbe.SearchEventLog("Exchange File Uploader", 1000, FfoExchangeFileUploaderProbe.alertFrame);
			if (!string.IsNullOrEmpty(text))
			{
				ProbeResult result = base.Result;
				result.ExecutionContext += string.Format("Last CosmosDataLoader Event: {0}.\r\n", text);
				return;
			}
			string value = FfoExchangeFileUploaderProbe.SearchEventLog("MSExchangeFileUpload", 0, FfoExchangeFileUploaderProbe.alertFrame);
			if (string.IsNullOrEmpty(value))
			{
				base.Result.FailureContext = "MSExchangeFileUpload service has not invoked CosmosDataLoader.exe in the last hour";
				base.Result.Error = "MSExchangeFileUpload service has not invoked CosmosDataLoader.exe in the last hour";
				throw new Exception("MSExchangeFileUpload service has not invoked CosmosDataLoader.exe in the last hour");
			}
			ProbeResult result2 = base.Result;
			result2.ExecutionContext += string.Format("Uploader Start Log Event: {0}.\r\n", text);
		}

		// Token: 0x040001B8 RID: 440
		private const string NoTransformationInLastHourError = "MSExchangeFileUpload service has not logged a transformation event in the last hour";

		// Token: 0x040001B9 RID: 441
		private const string NoCosmosDataLoaderInLastHourError = "MSExchangeFileUpload service has not invoked CosmosDataLoader.exe in the last hour";

		// Token: 0x040001BA RID: 442
		private static TimeSpan alertFrame = TimeSpan.FromHours(2.0);
	}
}
