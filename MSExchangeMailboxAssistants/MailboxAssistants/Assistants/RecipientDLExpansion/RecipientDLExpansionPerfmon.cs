using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.RecipientDLExpansion
{
	// Token: 0x02000267 RID: 615
	internal static class RecipientDLExpansionPerfmon
	{
		// Token: 0x060016CC RID: 5836 RVA: 0x00080674 File Offset: 0x0007E874
		public static void GetPerfCounterInfo(XElement element)
		{
			if (RecipientDLExpansionPerfmon.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in RecipientDLExpansionPerfmon.AllCounters)
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

		// Token: 0x04000D47 RID: 3399
		public const string CategoryName = "MSExchange Recipient DL Expansion Assistant";

		// Token: 0x04000D48 RID: 3400
		public static readonly ExPerformanceCounter TotalDLExpansionMessages = new ExPerformanceCounter("MSExchange Recipient DL Expansion Assistant", "DL Expansion Message Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000D49 RID: 3401
		public static readonly ExPerformanceCounter TotalRecipientDLsInMessage = new ExPerformanceCounter("MSExchange Recipient DL Expansion Assistant", "Message Recipient DLs Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000D4A RID: 3402
		public static readonly ExPerformanceCounter TotalExpandedNestedDLs = new ExPerformanceCounter("MSExchange Recipient DL Expansion Assistant", "Expanded Recipient DLs Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000D4B RID: 3403
		public static readonly ExPerformanceCounter AverageMessageDLExpansionProcessing = new ExPerformanceCounter("MSExchange Recipient DL Expansion Assistant", "DL Expansion Message Average Processing Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000D4C RID: 3404
		public static readonly ExPerformanceCounter AverageMessageDLExpansionProcessingBase = new ExPerformanceCounter("MSExchange Recipient DL Expansion Assistant", "DL Expansion Message Average processing Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000D4D RID: 3405
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			RecipientDLExpansionPerfmon.TotalDLExpansionMessages,
			RecipientDLExpansionPerfmon.TotalRecipientDLsInMessage,
			RecipientDLExpansionPerfmon.TotalExpandedNestedDLs,
			RecipientDLExpansionPerfmon.AverageMessageDLExpansionProcessing,
			RecipientDLExpansionPerfmon.AverageMessageDLExpansionProcessingBase
		};
	}
}
