using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000DAC RID: 3500
	internal static class GalsyncCounters
	{
		// Token: 0x06008619 RID: 34329 RVA: 0x00224C70 File Offset: 0x00222E70
		public static void GetPerfCounterInfo(XElement element)
		{
			if (GalsyncCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in GalsyncCounters.AllCounters)
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

		// Token: 0x0400412D RID: 16685
		public const string CategoryName = "GALSync";

		// Token: 0x0400412E RID: 16686
		public static readonly ExPerformanceCounter NumberOfMailboxesCreated = new ExPerformanceCounter("GALSync", "Number of mailboxes created", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400412F RID: 16687
		public static readonly ExPerformanceCounter ClientReportedNumberOfMailboxesCreated = new ExPerformanceCounter("GALSync", "Client reported number of mailboxes created", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004130 RID: 16688
		public static readonly ExPerformanceCounter ClientReportedNumberOfMailboxesToCreate = new ExPerformanceCounter("GALSync", "Client reported number of mailboxes to create", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004131 RID: 16689
		public static readonly ExPerformanceCounter ClientReportedMailboxCreationElapsedMilliseconds = new ExPerformanceCounter("GALSync", "Client reported total time used for Mailbox creation in milliseconds", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004132 RID: 16690
		public static readonly ExPerformanceCounter NumberOfExportSyncRuns = new ExPerformanceCounter("GALSync", "Number of export sync runs", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004133 RID: 16691
		public static readonly ExPerformanceCounter NumberOfImportSyncRuns = new ExPerformanceCounter("GALSync", "Number of import sync runs", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004134 RID: 16692
		public static readonly ExPerformanceCounter NumberOfSucessfulExportSyncRuns = new ExPerformanceCounter("GALSync", "Number of sucessful export sync runs", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004135 RID: 16693
		public static readonly ExPerformanceCounter NumberOfSucessfulImportSyncRuns = new ExPerformanceCounter("GALSync", "Number of sucessful import sync runs", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004136 RID: 16694
		public static readonly ExPerformanceCounter NumberOfConnectionErrors = new ExPerformanceCounter("GALSync", "Number of connection errors", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004137 RID: 16695
		public static readonly ExPerformanceCounter NumberOfPermissionErrors = new ExPerformanceCounter("GALSync", "Number of permission errors", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004138 RID: 16696
		public static readonly ExPerformanceCounter NumberOfLiveIdErrors = new ExPerformanceCounter("GALSync", "Number of LiveId errors", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004139 RID: 16697
		public static readonly ExPerformanceCounter NumberOfILMLogicErrors = new ExPerformanceCounter("GALSync", "Number of ILM logic errors", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400413A RID: 16698
		public static readonly ExPerformanceCounter NumberOfILMOtherErrors = new ExPerformanceCounter("GALSync", "Number of ILM other errors", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400413B RID: 16699
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			GalsyncCounters.NumberOfMailboxesCreated,
			GalsyncCounters.ClientReportedNumberOfMailboxesCreated,
			GalsyncCounters.ClientReportedNumberOfMailboxesToCreate,
			GalsyncCounters.ClientReportedMailboxCreationElapsedMilliseconds,
			GalsyncCounters.NumberOfExportSyncRuns,
			GalsyncCounters.NumberOfImportSyncRuns,
			GalsyncCounters.NumberOfSucessfulExportSyncRuns,
			GalsyncCounters.NumberOfSucessfulImportSyncRuns,
			GalsyncCounters.NumberOfConnectionErrors,
			GalsyncCounters.NumberOfPermissionErrors,
			GalsyncCounters.NumberOfLiveIdErrors,
			GalsyncCounters.NumberOfILMLogicErrors,
			GalsyncCounters.NumberOfILMOtherErrors
		};
	}
}
