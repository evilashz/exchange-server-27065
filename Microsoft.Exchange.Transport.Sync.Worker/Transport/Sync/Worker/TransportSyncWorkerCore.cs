using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Worker
{
	// Token: 0x02000240 RID: 576
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class TransportSyncWorkerCore
	{
		// Token: 0x060014EB RID: 5355 RVA: 0x0004BCD4 File Offset: 0x00049ED4
		public static void GetPerfCounterInfo(XElement element)
		{
			if (TransportSyncWorkerCore.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in TransportSyncWorkerCore.AllCounters)
			{
				try
				{
					element.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					element.Add(content);
				}
			}
		}

		// Token: 0x04000B02 RID: 2818
		public const string CategoryName = "MSExchange Transport Sync Worker Core";

		// Token: 0x04000B03 RID: 2819
		private static readonly ExPerformanceCounter DownloadSpeed = new ExPerformanceCounter("MSExchange Transport Sync Worker Core", "Download Speed (Bytes/sec)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000B04 RID: 2820
		public static readonly ExPerformanceCounter OutstandingJobs = new ExPerformanceCounter("MSExchange Transport Sync Worker Core", "Sync Scheduler - Total On Server", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000B05 RID: 2821
		public static readonly ExPerformanceCounter OutstandingJobsInRetry = new ExPerformanceCounter("MSExchange Transport Sync Worker Core", "Sync Scheduler - Total In Retry Queue", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000B06 RID: 2822
		private static readonly ExPerformanceCounter RateOfMessagesSubmittedToPipeline = new ExPerformanceCounter("MSExchange Transport Sync Worker Core", "Messages Submitted to Pipeline - Rate (per second)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000B07 RID: 2823
		public static readonly ExPerformanceCounter MessagesSubmittedToPipeline = new ExPerformanceCounter("MSExchange Transport Sync Worker Core", "Messages Submitted to Pipeline - Total Count", string.Empty, null, new ExPerformanceCounter[]
		{
			TransportSyncWorkerCore.RateOfMessagesSubmittedToPipeline
		});

		// Token: 0x04000B08 RID: 2824
		public static readonly ExPerformanceCounter TotalDownloadedBytes = new ExPerformanceCounter("MSExchange Transport Sync Worker Core", "Total bytes downloaded", string.Empty, null, new ExPerformanceCounter[]
		{
			TransportSyncWorkerCore.DownloadSpeed
		});

		// Token: 0x04000B09 RID: 2825
		public static readonly ExPerformanceCounter TotalSubscriptionsAggregated = new ExPerformanceCounter("MSExchange Transport Sync Worker Core", "Total subscriptions aggregated", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000B0A RID: 2826
		private static readonly ExPerformanceCounter UploadSpeed = new ExPerformanceCounter("MSExchange Transport Sync Worker Core", "Upload speed (bytes/sec)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000B0B RID: 2827
		public static readonly ExPerformanceCounter TotalUploadedBytes = new ExPerformanceCounter("MSExchange Transport Sync Worker Core", "Total bytes uploaded", string.Empty, null, new ExPerformanceCounter[]
		{
			TransportSyncWorkerCore.UploadSpeed
		});

		// Token: 0x04000B0C RID: 2828
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			TransportSyncWorkerCore.MessagesSubmittedToPipeline,
			TransportSyncWorkerCore.OutstandingJobs,
			TransportSyncWorkerCore.OutstandingJobsInRetry,
			TransportSyncWorkerCore.TotalDownloadedBytes,
			TransportSyncWorkerCore.TotalSubscriptionsAggregated,
			TransportSyncWorkerCore.TotalUploadedBytes
		};
	}
}
