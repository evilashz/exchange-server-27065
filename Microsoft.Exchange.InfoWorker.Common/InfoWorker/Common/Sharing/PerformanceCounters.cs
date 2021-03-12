using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x0200031A RID: 794
	internal static class PerformanceCounters
	{
		// Token: 0x06001766 RID: 5990 RVA: 0x0006D224 File Offset: 0x0006B424
		public static void GetPerfCounterInfo(XElement element)
		{
			if (PerformanceCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in PerformanceCounters.AllCounters)
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

		// Token: 0x04000F1A RID: 3866
		public const string CategoryName = "MSExchange Sharing Engine";

		// Token: 0x04000F1B RID: 3867
		public static readonly ExPerformanceCounter CalendarItemsSynced = new ExPerformanceCounter("MSExchange Sharing Engine", "Calendar Items Synchronized", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F1C RID: 3868
		public static readonly ExPerformanceCounter ContactItemsSynced = new ExPerformanceCounter("MSExchange Sharing Engine", "Contact Items Synchronized", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F1D RID: 3869
		public static readonly ExPerformanceCounter FolderSynchronizationFailures = new ExPerformanceCounter("MSExchange Sharing Engine", "Folder Synchronization Failures", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F1E RID: 3870
		public static readonly ExPerformanceCounter FoldersProcessedSynchronously = new ExPerformanceCounter("MSExchange Sharing Engine", "Folders Processed Synchronously", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F1F RID: 3871
		public static readonly ExPerformanceCounter SynchronizationTimeouts = new ExPerformanceCounter("MSExchange Sharing Engine", "Folders Synchronization Timeouts", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F20 RID: 3872
		public static readonly ExPerformanceCounter LastFolderSynchronizationTime = new ExPerformanceCounter("MSExchange Sharing Engine", "Last Folder Synchronization Time (in seconds)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F21 RID: 3873
		public static readonly ExPerformanceCounter AverageFolderSynchronizationTime = new ExPerformanceCounter("MSExchange Sharing Engine", "Average Folder Synchronization Time (in seconds)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F22 RID: 3874
		public static readonly ExPerformanceCounter AverageFolderSynchronizationTimeBase = new ExPerformanceCounter("MSExchange Sharing Engine", "Base for Average Time to Synchronize a Folder", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F23 RID: 3875
		public static readonly ExPerformanceCounter AverageExternalAuthenticationTokenRequestTime = new ExPerformanceCounter("MSExchange Sharing Engine", "Average Time to Request a Token for an External Authentication", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F24 RID: 3876
		public static readonly ExPerformanceCounter AverageExternalAuthenticationTokenRequestTimeBase = new ExPerformanceCounter("MSExchange Sharing Engine", "Base for Average Time to Request a Token for an External Authentication", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F25 RID: 3877
		public static readonly ExPerformanceCounter SuccessfulExternalAuthenticationTokenRequests = new ExPerformanceCounter("MSExchange Sharing Engine", "Number of Successful Token Requests for External Authentication", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F26 RID: 3878
		public static readonly ExPerformanceCounter FailedExternalAuthenticationTokenRequests = new ExPerformanceCounter("MSExchange Sharing Engine", "Number of Failed Token Requests for External Authentication", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F27 RID: 3879
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			PerformanceCounters.CalendarItemsSynced,
			PerformanceCounters.ContactItemsSynced,
			PerformanceCounters.FolderSynchronizationFailures,
			PerformanceCounters.FoldersProcessedSynchronously,
			PerformanceCounters.SynchronizationTimeouts,
			PerformanceCounters.LastFolderSynchronizationTime,
			PerformanceCounters.AverageFolderSynchronizationTime,
			PerformanceCounters.AverageFolderSynchronizationTimeBase,
			PerformanceCounters.AverageExternalAuthenticationTokenRequestTime,
			PerformanceCounters.AverageExternalAuthenticationTokenRequestTimeBase,
			PerformanceCounters.SuccessfulExternalAuthenticationTokenRequests,
			PerformanceCounters.FailedExternalAuthenticationTokenRequests
		};
	}
}
