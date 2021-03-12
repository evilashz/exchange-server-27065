using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Search.Performance
{
	// Token: 0x0200003A RID: 58
	internal sealed class TransportCtsFlowCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x060002BD RID: 701 RVA: 0x0000F554 File Offset: 0x0000D754
		internal TransportCtsFlowCountersInstance(string instanceName, TransportCtsFlowCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeSearch Transport CTS Flow")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Total Bytes Sent Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.BytesSent = new ExPerformanceCounter(base.CategoryName, "Bytes Sent", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.BytesSent);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Total Bytes Received Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.BytesReceived = new ExPerformanceCounter(base.CategoryName, "Bytes Received", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.BytesReceived);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Document Processing Rate Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Document Failure Rate Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter(base.CategoryName, "Document Skip Rate Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.LanguageDetectionFailures = new ExPerformanceCounter(base.CategoryName, "Language Detection Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LanguageDetectionFailures);
				this.NumberOfFailedDocuments = new ExPerformanceCounter(base.CategoryName, "Number Of Failed Documents", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.NumberOfFailedDocuments);
				this.NumberOfProcessedDocuments = new ExPerformanceCounter(base.CategoryName, "Number Of Processed Documents", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.NumberOfProcessedDocuments);
				this.NumberOfSkippedNlg = new ExPerformanceCounter(base.CategoryName, "Nlg Documents Skipped", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfSkippedNlg);
				this.TimeInGetConnectionInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: GetConnection", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInGetConnectionInMsec);
				this.TimeInPropertyBagLoadInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: PropertyBagLoad", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInPropertyBagLoadInMsec);
				this.TimeInMessageItemConversionInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: MessageItem Conversion", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInMessageItemConversionInMsec);
				this.TimeDeterminingAgeOfItemInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: Determining age of Item", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeDeterminingAgeOfItemInMsec);
				this.TimeInMimeConversionInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: Mime Conversion", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInMimeConversionInMsec);
				this.TimeInShouldAnnotateMessageInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: ShouldAnnotateMessage", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInShouldAnnotateMessageInMsec);
				this.TimeInTransportRetrieverInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: TransportRetriever", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInTransportRetrieverInMsec);
				this.TimeInDocParserInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: DocParser", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInDocParserInMsec);
				this.TimeInNLGSubflowInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: NlgSubflow", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInNLGSubflowInMsec);
				this.TimeInQueueInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: Waiting In Queue", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInQueueInMsec);
				this.TimeInWordbreakerInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: WordBreaker", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInWordbreakerInMsec);
				this.TimeSpentWaitingForConnectInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: Waiting For Connect", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeSpentWaitingForConnectInMsec);
				this.TotalDocuments = new ExPerformanceCounter(base.CategoryName, "Number Of Documents", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalDocuments);
				this.TotalSkippedDocuments = new ExPerformanceCounter(base.CategoryName, "Number Of Skipped Documents", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter5
				});
				list.Add(this.TotalSkippedDocuments);
				this.TotalTimeProcessingMessageInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: Processing Messages", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalTimeProcessingMessageInMsec);
				this.TotalTimeProcessingFailedMessageInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: Processing Messages That Failed", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalTimeProcessingFailedMessageInMsec);
				this.ProcessedUnder250ms = new ExPerformanceCounter(base.CategoryName, "Documents Processed Under 250ms", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessedUnder250ms);
				this.ProcessedUnder500ms = new ExPerformanceCounter(base.CategoryName, "Documents Processed Under 500ms", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessedUnder500ms);
				this.ProcessedUnder1000ms = new ExPerformanceCounter(base.CategoryName, "Documents Processed Under 1000ms", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessedUnder1000ms);
				this.ProcessedUnder2000ms = new ExPerformanceCounter(base.CategoryName, "Documents Processed Under 2000ms", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessedUnder2000ms);
				this.ProcessedUnder5000ms = new ExPerformanceCounter(base.CategoryName, "Documents Processed Under 5000ms", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessedUnder5000ms);
				this.ProcessedOver5000ms = new ExPerformanceCounter(base.CategoryName, "Documents processed over 5000ms", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessedOver5000ms);
				long num = this.BytesSent.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter6 in list)
					{
						exPerformanceCounter6.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000FB80 File Offset: 0x0000DD80
		internal TransportCtsFlowCountersInstance(string instanceName) : base(instanceName, "MSExchangeSearch Transport CTS Flow")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Total Bytes Sent Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.BytesSent = new ExPerformanceCounter(base.CategoryName, "Bytes Sent", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.BytesSent);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Total Bytes Received Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.BytesReceived = new ExPerformanceCounter(base.CategoryName, "Bytes Received", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.BytesReceived);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Document Processing Rate Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Document Failure Rate Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter(base.CategoryName, "Document Skip Rate Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.LanguageDetectionFailures = new ExPerformanceCounter(base.CategoryName, "Language Detection Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LanguageDetectionFailures);
				this.NumberOfFailedDocuments = new ExPerformanceCounter(base.CategoryName, "Number Of Failed Documents", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.NumberOfFailedDocuments);
				this.NumberOfProcessedDocuments = new ExPerformanceCounter(base.CategoryName, "Number Of Processed Documents", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.NumberOfProcessedDocuments);
				this.NumberOfSkippedNlg = new ExPerformanceCounter(base.CategoryName, "Nlg Documents Skipped", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfSkippedNlg);
				this.TimeInGetConnectionInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: GetConnection", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInGetConnectionInMsec);
				this.TimeInPropertyBagLoadInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: PropertyBagLoad", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInPropertyBagLoadInMsec);
				this.TimeInMessageItemConversionInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: MessageItem Conversion", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInMessageItemConversionInMsec);
				this.TimeDeterminingAgeOfItemInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: Determining age of Item", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeDeterminingAgeOfItemInMsec);
				this.TimeInMimeConversionInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: Mime Conversion", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInMimeConversionInMsec);
				this.TimeInShouldAnnotateMessageInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: ShouldAnnotateMessage", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInShouldAnnotateMessageInMsec);
				this.TimeInTransportRetrieverInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: TransportRetriever", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInTransportRetrieverInMsec);
				this.TimeInDocParserInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: DocParser", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInDocParserInMsec);
				this.TimeInNLGSubflowInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: NlgSubflow", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInNLGSubflowInMsec);
				this.TimeInQueueInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: Waiting In Queue", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInQueueInMsec);
				this.TimeInWordbreakerInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: WordBreaker", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInWordbreakerInMsec);
				this.TimeSpentWaitingForConnectInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: Waiting For Connect", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeSpentWaitingForConnectInMsec);
				this.TotalDocuments = new ExPerformanceCounter(base.CategoryName, "Number Of Documents", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalDocuments);
				this.TotalSkippedDocuments = new ExPerformanceCounter(base.CategoryName, "Number Of Skipped Documents", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter5
				});
				list.Add(this.TotalSkippedDocuments);
				this.TotalTimeProcessingMessageInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: Processing Messages", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalTimeProcessingMessageInMsec);
				this.TotalTimeProcessingFailedMessageInMsec = new ExPerformanceCounter(base.CategoryName, "Time Spent Msec: Processing Messages That Failed", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalTimeProcessingFailedMessageInMsec);
				this.ProcessedUnder250ms = new ExPerformanceCounter(base.CategoryName, "Documents Processed Under 250ms", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessedUnder250ms);
				this.ProcessedUnder500ms = new ExPerformanceCounter(base.CategoryName, "Documents Processed Under 500ms", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessedUnder500ms);
				this.ProcessedUnder1000ms = new ExPerformanceCounter(base.CategoryName, "Documents Processed Under 1000ms", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessedUnder1000ms);
				this.ProcessedUnder2000ms = new ExPerformanceCounter(base.CategoryName, "Documents Processed Under 2000ms", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessedUnder2000ms);
				this.ProcessedUnder5000ms = new ExPerformanceCounter(base.CategoryName, "Documents Processed Under 5000ms", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessedUnder5000ms);
				this.ProcessedOver5000ms = new ExPerformanceCounter(base.CategoryName, "Documents processed over 5000ms", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessedOver5000ms);
				long num = this.BytesSent.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter6 in list)
					{
						exPerformanceCounter6.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000101AC File Offset: 0x0000E3AC
		public override void GetPerfCounterDiagnosticsInfo(XElement topElement)
		{
			XElement xelement = null;
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				try
				{
					if (xelement == null)
					{
						xelement = new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.InstanceName));
						topElement.Add(xelement);
					}
					xelement.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					topElement.Add(content);
				}
			}
		}

		// Token: 0x0400014B RID: 331
		public readonly ExPerformanceCounter BytesSent;

		// Token: 0x0400014C RID: 332
		public readonly ExPerformanceCounter BytesReceived;

		// Token: 0x0400014D RID: 333
		public readonly ExPerformanceCounter LanguageDetectionFailures;

		// Token: 0x0400014E RID: 334
		public readonly ExPerformanceCounter NumberOfFailedDocuments;

		// Token: 0x0400014F RID: 335
		public readonly ExPerformanceCounter NumberOfProcessedDocuments;

		// Token: 0x04000150 RID: 336
		public readonly ExPerformanceCounter NumberOfSkippedNlg;

		// Token: 0x04000151 RID: 337
		public readonly ExPerformanceCounter TimeInGetConnectionInMsec;

		// Token: 0x04000152 RID: 338
		public readonly ExPerformanceCounter TimeInPropertyBagLoadInMsec;

		// Token: 0x04000153 RID: 339
		public readonly ExPerformanceCounter TimeInMessageItemConversionInMsec;

		// Token: 0x04000154 RID: 340
		public readonly ExPerformanceCounter TimeDeterminingAgeOfItemInMsec;

		// Token: 0x04000155 RID: 341
		public readonly ExPerformanceCounter TimeInMimeConversionInMsec;

		// Token: 0x04000156 RID: 342
		public readonly ExPerformanceCounter TimeInShouldAnnotateMessageInMsec;

		// Token: 0x04000157 RID: 343
		public readonly ExPerformanceCounter TimeInTransportRetrieverInMsec;

		// Token: 0x04000158 RID: 344
		public readonly ExPerformanceCounter TimeInDocParserInMsec;

		// Token: 0x04000159 RID: 345
		public readonly ExPerformanceCounter TimeInNLGSubflowInMsec;

		// Token: 0x0400015A RID: 346
		public readonly ExPerformanceCounter TimeInQueueInMsec;

		// Token: 0x0400015B RID: 347
		public readonly ExPerformanceCounter TimeInWordbreakerInMsec;

		// Token: 0x0400015C RID: 348
		public readonly ExPerformanceCounter TimeSpentWaitingForConnectInMsec;

		// Token: 0x0400015D RID: 349
		public readonly ExPerformanceCounter TotalDocuments;

		// Token: 0x0400015E RID: 350
		public readonly ExPerformanceCounter TotalSkippedDocuments;

		// Token: 0x0400015F RID: 351
		public readonly ExPerformanceCounter TotalTimeProcessingMessageInMsec;

		// Token: 0x04000160 RID: 352
		public readonly ExPerformanceCounter TotalTimeProcessingFailedMessageInMsec;

		// Token: 0x04000161 RID: 353
		public readonly ExPerformanceCounter ProcessedUnder250ms;

		// Token: 0x04000162 RID: 354
		public readonly ExPerformanceCounter ProcessedUnder500ms;

		// Token: 0x04000163 RID: 355
		public readonly ExPerformanceCounter ProcessedUnder1000ms;

		// Token: 0x04000164 RID: 356
		public readonly ExPerformanceCounter ProcessedUnder2000ms;

		// Token: 0x04000165 RID: 357
		public readonly ExPerformanceCounter ProcessedUnder5000ms;

		// Token: 0x04000166 RID: 358
		public readonly ExPerformanceCounter ProcessedOver5000ms;
	}
}
