using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Search.Performance
{
	// Token: 0x02000026 RID: 38
	internal sealed class MailboxOperatorsInstance : PerformanceCounterInstance
	{
		// Token: 0x060001EA RID: 490 RVA: 0x00009A1C File Offset: 0x00007C1C
		internal MailboxOperatorsInstance(string instanceName, MailboxOperatorsInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeSearch Mailbox Operators")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.RetrieverNumberOfItemsProcessedIn0to50Milliseconds = new ExPerformanceCounter(base.CategoryName, "Retriever: Items processed 0-50 ms", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RetrieverNumberOfItemsProcessedIn0to50Milliseconds, new ExPerformanceCounter[0]);
				list.Add(this.RetrieverNumberOfItemsProcessedIn0to50Milliseconds);
				this.RetrieverNumberOfItemsProcessedIn50to100Milliseconds = new ExPerformanceCounter(base.CategoryName, "Retriever: Items processed 50-100 ms", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RetrieverNumberOfItemsProcessedIn50to100Milliseconds, new ExPerformanceCounter[0]);
				list.Add(this.RetrieverNumberOfItemsProcessedIn50to100Milliseconds);
				this.RetrieverNumberOfItemsProcessedIn100to2000Milliseconds = new ExPerformanceCounter(base.CategoryName, "Retriever: Items processed 100-2000 ms", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RetrieverNumberOfItemsProcessedIn100to2000Milliseconds, new ExPerformanceCounter[0]);
				list.Add(this.RetrieverNumberOfItemsProcessedIn100to2000Milliseconds);
				this.RetrieverNumberOfItemsProcessedInGreaterThan2000Milliseconds = new ExPerformanceCounter(base.CategoryName, "Retriever: Items processed in greater than 2000 ms", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RetrieverNumberOfItemsProcessedInGreaterThan2000Milliseconds, new ExPerformanceCounter[0]);
				list.Add(this.RetrieverNumberOfItemsProcessedInGreaterThan2000Milliseconds);
				this.RetrieverNumberOfItemsWithValidAnnotationToken = new ExPerformanceCounter(base.CategoryName, "Retriever: Items with valid annotation token", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RetrieverNumberOfItemsWithValidAnnotationToken, new ExPerformanceCounter[0]);
				list.Add(this.RetrieverNumberOfItemsWithValidAnnotationToken);
				this.RetrieverNumberOfItemsWithoutAnnotationToken = new ExPerformanceCounter(base.CategoryName, "Retriever: Items without annotation token", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RetrieverNumberOfItemsWithoutAnnotationToken, new ExPerformanceCounter[0]);
				list.Add(this.RetrieverNumberOfItemsWithoutAnnotationToken);
				this.RetrieverItemsWithRightsManagementAttempted = new ExPerformanceCounter(base.CategoryName, "Retriever: Items with Rights Management attempted for processing", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RetrieverItemsWithRightsManagementAttempted, new ExPerformanceCounter[0]);
				list.Add(this.RetrieverItemsWithRightsManagementAttempted);
				this.RetrieverItemsWithRightsManagementPartiallyProcessed = new ExPerformanceCounter(base.CategoryName, "Retriever: Items with Rights Management partially processed", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RetrieverItemsWithRightsManagementPartiallyProcessed, new ExPerformanceCounter[0]);
				list.Add(this.RetrieverItemsWithRightsManagementPartiallyProcessed);
				this.RetrieverItemsWithRightsManagementSkipped = new ExPerformanceCounter(base.CategoryName, "Retriever: Items with Rights Management skipped", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RetrieverItemsWithRightsManagementSkipped, new ExPerformanceCounter[0]);
				list.Add(this.RetrieverItemsWithRightsManagementSkipped);
				this.TotalAnnotationTokenBytes = new ExPerformanceCounter(base.CategoryName, "Total Annotation Token Bytes Read", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalAnnotationTokenBytes, new ExPerformanceCounter[0]);
				list.Add(this.TotalAnnotationTokenBytes);
				this.TotalAttachmentBytes = new ExPerformanceCounter(base.CategoryName, "Total Attachment Bytes Read", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalAttachmentBytes, new ExPerformanceCounter[0]);
				list.Add(this.TotalAttachmentBytes);
				this.TotalAttachmentsRead = new ExPerformanceCounter(base.CategoryName, "Total Attachments Read", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalAttachmentsRead, new ExPerformanceCounter[0]);
				list.Add(this.TotalAttachmentsRead);
				this.TotalBodyChars = new ExPerformanceCounter(base.CategoryName, "Total Message Body Chars Read", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalBodyChars, new ExPerformanceCounter[0]);
				list.Add(this.TotalBodyChars);
				this.IndexablePropertiesSize = new ExPerformanceCounter(base.CategoryName, "Total Wordbroken Content Size In Bytes", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.IndexablePropertiesSize, new ExPerformanceCounter[0]);
				list.Add(this.IndexablePropertiesSize);
				this.TotalPoisonDocumentsProcessed = new ExPerformanceCounter(base.CategoryName, "Total Poison Documents", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalPoisonDocumentsProcessed, new ExPerformanceCounter[0]);
				list.Add(this.TotalPoisonDocumentsProcessed);
				this.TotalTimeSpentConvertingToTextual = new ExPerformanceCounter(base.CategoryName, "Time Spent: Converting To Textual Msec", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalTimeSpentConvertingToTextual, new ExPerformanceCounter[0]);
				list.Add(this.TotalTimeSpentConvertingToTextual);
				this.TotalTimeSpentDocParser = new ExPerformanceCounter(base.CategoryName, "Time Spent: Document Parsing Msec", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalTimeSpentDocParser, new ExPerformanceCounter[0]);
				list.Add(this.TotalTimeSpentDocParser);
				this.TotalTimeSpentLanguageDetection = new ExPerformanceCounter(base.CategoryName, "Time Spent: Language Detection Msec", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalTimeSpentLanguageDetection, new ExPerformanceCounter[0]);
				list.Add(this.TotalTimeSpentLanguageDetection);
				this.TotalTimeSpentProcessingDocuments = new ExPerformanceCounter(base.CategoryName, "Time Spent: Processing Documents Msec", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalTimeSpentProcessingDocuments, new ExPerformanceCounter[0]);
				list.Add(this.TotalTimeSpentProcessingDocuments);
				this.TotalTimeSpentTokenDeserializer = new ExPerformanceCounter(base.CategoryName, "Time Spent: Token Deserializer Msec", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalTimeSpentTokenDeserializer, new ExPerformanceCounter[0]);
				list.Add(this.TotalTimeSpentTokenDeserializer);
				this.TotalTimeSpentWorkBreaking = new ExPerformanceCounter(base.CategoryName, "Time Spent: Wordbreaking Msec", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalTimeSpentWorkBreaking, new ExPerformanceCounter[0]);
				list.Add(this.TotalTimeSpentWorkBreaking);
				long num = this.RetrieverNumberOfItemsProcessedIn0to50Milliseconds.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00009F38 File Offset: 0x00008138
		internal MailboxOperatorsInstance(string instanceName) : base(instanceName, "MSExchangeSearch Mailbox Operators")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.RetrieverNumberOfItemsProcessedIn0to50Milliseconds = new ExPerformanceCounter(base.CategoryName, "Retriever: Items processed 0-50 ms", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RetrieverNumberOfItemsProcessedIn0to50Milliseconds);
				this.RetrieverNumberOfItemsProcessedIn50to100Milliseconds = new ExPerformanceCounter(base.CategoryName, "Retriever: Items processed 50-100 ms", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RetrieverNumberOfItemsProcessedIn50to100Milliseconds);
				this.RetrieverNumberOfItemsProcessedIn100to2000Milliseconds = new ExPerformanceCounter(base.CategoryName, "Retriever: Items processed 100-2000 ms", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RetrieverNumberOfItemsProcessedIn100to2000Milliseconds);
				this.RetrieverNumberOfItemsProcessedInGreaterThan2000Milliseconds = new ExPerformanceCounter(base.CategoryName, "Retriever: Items processed in greater than 2000 ms", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RetrieverNumberOfItemsProcessedInGreaterThan2000Milliseconds);
				this.RetrieverNumberOfItemsWithValidAnnotationToken = new ExPerformanceCounter(base.CategoryName, "Retriever: Items with valid annotation token", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RetrieverNumberOfItemsWithValidAnnotationToken);
				this.RetrieverNumberOfItemsWithoutAnnotationToken = new ExPerformanceCounter(base.CategoryName, "Retriever: Items without annotation token", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RetrieverNumberOfItemsWithoutAnnotationToken);
				this.RetrieverItemsWithRightsManagementAttempted = new ExPerformanceCounter(base.CategoryName, "Retriever: Items with Rights Management attempted for processing", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RetrieverItemsWithRightsManagementAttempted);
				this.RetrieverItemsWithRightsManagementPartiallyProcessed = new ExPerformanceCounter(base.CategoryName, "Retriever: Items with Rights Management partially processed", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RetrieverItemsWithRightsManagementPartiallyProcessed);
				this.RetrieverItemsWithRightsManagementSkipped = new ExPerformanceCounter(base.CategoryName, "Retriever: Items with Rights Management skipped", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RetrieverItemsWithRightsManagementSkipped);
				this.TotalAnnotationTokenBytes = new ExPerformanceCounter(base.CategoryName, "Total Annotation Token Bytes Read", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalAnnotationTokenBytes);
				this.TotalAttachmentBytes = new ExPerformanceCounter(base.CategoryName, "Total Attachment Bytes Read", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalAttachmentBytes);
				this.TotalAttachmentsRead = new ExPerformanceCounter(base.CategoryName, "Total Attachments Read", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalAttachmentsRead);
				this.TotalBodyChars = new ExPerformanceCounter(base.CategoryName, "Total Message Body Chars Read", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalBodyChars);
				this.IndexablePropertiesSize = new ExPerformanceCounter(base.CategoryName, "Total Wordbroken Content Size In Bytes", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.IndexablePropertiesSize);
				this.TotalPoisonDocumentsProcessed = new ExPerformanceCounter(base.CategoryName, "Total Poison Documents", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalPoisonDocumentsProcessed);
				this.TotalTimeSpentConvertingToTextual = new ExPerformanceCounter(base.CategoryName, "Time Spent: Converting To Textual Msec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalTimeSpentConvertingToTextual);
				this.TotalTimeSpentDocParser = new ExPerformanceCounter(base.CategoryName, "Time Spent: Document Parsing Msec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalTimeSpentDocParser);
				this.TotalTimeSpentLanguageDetection = new ExPerformanceCounter(base.CategoryName, "Time Spent: Language Detection Msec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalTimeSpentLanguageDetection);
				this.TotalTimeSpentProcessingDocuments = new ExPerformanceCounter(base.CategoryName, "Time Spent: Processing Documents Msec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalTimeSpentProcessingDocuments);
				this.TotalTimeSpentTokenDeserializer = new ExPerformanceCounter(base.CategoryName, "Time Spent: Token Deserializer Msec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalTimeSpentTokenDeserializer);
				this.TotalTimeSpentWorkBreaking = new ExPerformanceCounter(base.CategoryName, "Time Spent: Wordbreaking Msec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalTimeSpentWorkBreaking);
				long num = this.RetrieverNumberOfItemsProcessedIn0to50Milliseconds.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000A36C File Offset: 0x0000856C
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

		// Token: 0x04000162 RID: 354
		public readonly ExPerformanceCounter RetrieverNumberOfItemsProcessedIn0to50Milliseconds;

		// Token: 0x04000163 RID: 355
		public readonly ExPerformanceCounter RetrieverNumberOfItemsProcessedIn50to100Milliseconds;

		// Token: 0x04000164 RID: 356
		public readonly ExPerformanceCounter RetrieverNumberOfItemsProcessedIn100to2000Milliseconds;

		// Token: 0x04000165 RID: 357
		public readonly ExPerformanceCounter RetrieverNumberOfItemsProcessedInGreaterThan2000Milliseconds;

		// Token: 0x04000166 RID: 358
		public readonly ExPerformanceCounter RetrieverNumberOfItemsWithValidAnnotationToken;

		// Token: 0x04000167 RID: 359
		public readonly ExPerformanceCounter RetrieverNumberOfItemsWithoutAnnotationToken;

		// Token: 0x04000168 RID: 360
		public readonly ExPerformanceCounter RetrieverItemsWithRightsManagementAttempted;

		// Token: 0x04000169 RID: 361
		public readonly ExPerformanceCounter RetrieverItemsWithRightsManagementPartiallyProcessed;

		// Token: 0x0400016A RID: 362
		public readonly ExPerformanceCounter RetrieverItemsWithRightsManagementSkipped;

		// Token: 0x0400016B RID: 363
		public readonly ExPerformanceCounter TotalAnnotationTokenBytes;

		// Token: 0x0400016C RID: 364
		public readonly ExPerformanceCounter TotalAttachmentBytes;

		// Token: 0x0400016D RID: 365
		public readonly ExPerformanceCounter TotalAttachmentsRead;

		// Token: 0x0400016E RID: 366
		public readonly ExPerformanceCounter TotalBodyChars;

		// Token: 0x0400016F RID: 367
		public readonly ExPerformanceCounter IndexablePropertiesSize;

		// Token: 0x04000170 RID: 368
		public readonly ExPerformanceCounter TotalPoisonDocumentsProcessed;

		// Token: 0x04000171 RID: 369
		public readonly ExPerformanceCounter TotalTimeSpentConvertingToTextual;

		// Token: 0x04000172 RID: 370
		public readonly ExPerformanceCounter TotalTimeSpentDocParser;

		// Token: 0x04000173 RID: 371
		public readonly ExPerformanceCounter TotalTimeSpentLanguageDetection;

		// Token: 0x04000174 RID: 372
		public readonly ExPerformanceCounter TotalTimeSpentProcessingDocuments;

		// Token: 0x04000175 RID: 373
		public readonly ExPerformanceCounter TotalTimeSpentTokenDeserializer;

		// Token: 0x04000176 RID: 374
		public readonly ExPerformanceCounter TotalTimeSpentWorkBreaking;
	}
}
