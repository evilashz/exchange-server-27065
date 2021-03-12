using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Performance;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x02000016 RID: 22
	internal class MailboxFlowPerformanceCounters : IOperatorPerfCounter
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00006861 File Offset: 0x00004A61
		public MailboxFlowPerformanceCounters(string instanceName)
		{
			this.mailboxOperatorsInstance = MailboxOperators.GetInstance(instanceName);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006875 File Offset: 0x00004A75
		public void IncrementPerfcounter(OperatorPerformanceCounter performanceCounter)
		{
			this.GetExPerformanceCounter(performanceCounter).Increment();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006884 File Offset: 0x00004A84
		public void IncrementPerfcounterBy(OperatorPerformanceCounter performanceCounter, long value)
		{
			this.GetExPerformanceCounter(performanceCounter).IncrementBy(value);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006894 File Offset: 0x00004A94
		public void DecrementPerfcounter(OperatorPerformanceCounter performanceCounter)
		{
			this.GetExPerformanceCounter(performanceCounter).Decrement();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000068A4 File Offset: 0x00004AA4
		private ExPerformanceCounter GetExPerformanceCounter(OperatorPerformanceCounter performanceCounter)
		{
			switch (performanceCounter)
			{
			case OperatorPerformanceCounter.RetrieverNumberOfItemsProcessedIn0to50Milliseconds:
				return this.mailboxOperatorsInstance.RetrieverNumberOfItemsProcessedIn0to50Milliseconds;
			case OperatorPerformanceCounter.RetrieverNumberOfItemsProcessedIn50to100Milliseconds:
				return this.mailboxOperatorsInstance.RetrieverNumberOfItemsProcessedIn50to100Milliseconds;
			case OperatorPerformanceCounter.RetrieverNumberOfItemsProcessedIn100to2000Milliseconds:
				return this.mailboxOperatorsInstance.RetrieverNumberOfItemsProcessedIn100to2000Milliseconds;
			case OperatorPerformanceCounter.RetrieverNumberOfItemsProcessedInGreaterThan2000Milliseconds:
				return this.mailboxOperatorsInstance.RetrieverNumberOfItemsProcessedInGreaterThan2000Milliseconds;
			case OperatorPerformanceCounter.RetrieverNumberOfItemsWithValidAnnotationToken:
				return this.mailboxOperatorsInstance.RetrieverNumberOfItemsWithValidAnnotationToken;
			case OperatorPerformanceCounter.RetrieverNumberOfItemsWithoutAnnotationToken:
				return this.mailboxOperatorsInstance.RetrieverNumberOfItemsWithoutAnnotationToken;
			case OperatorPerformanceCounter.RetrieverItemsWithRightsManagementAttempted:
				return this.mailboxOperatorsInstance.RetrieverItemsWithRightsManagementAttempted;
			case OperatorPerformanceCounter.RetrieverItemsWithRightsManagementPartiallyProcessed:
				return this.mailboxOperatorsInstance.RetrieverItemsWithRightsManagementPartiallyProcessed;
			case OperatorPerformanceCounter.RetrieverItemsWithRightsManagementSkipped:
				return this.mailboxOperatorsInstance.RetrieverItemsWithRightsManagementSkipped;
			case OperatorPerformanceCounter.TotalAnnotationTokenBytes:
				return this.mailboxOperatorsInstance.TotalAnnotationTokenBytes;
			case OperatorPerformanceCounter.TotalAttachmentBytes:
				return this.mailboxOperatorsInstance.TotalAttachmentBytes;
			case OperatorPerformanceCounter.TotalAttachmentsRead:
				return this.mailboxOperatorsInstance.TotalAttachmentsRead;
			case OperatorPerformanceCounter.TotalBodyChars:
				return this.mailboxOperatorsInstance.TotalBodyChars;
			case OperatorPerformanceCounter.IndexablePropertiesSize:
				return this.mailboxOperatorsInstance.IndexablePropertiesSize;
			case OperatorPerformanceCounter.TotalPoisonDocumentsProcessed:
				return this.mailboxOperatorsInstance.TotalPoisonDocumentsProcessed;
			case OperatorPerformanceCounter.TotalTimeSpentConvertingToTextual:
				return this.mailboxOperatorsInstance.TotalTimeSpentConvertingToTextual;
			case OperatorPerformanceCounter.TotalTimeSpentDocParser:
				return this.mailboxOperatorsInstance.TotalTimeSpentDocParser;
			case OperatorPerformanceCounter.TotalTimeSpentLanguageDetection:
				return this.mailboxOperatorsInstance.TotalTimeSpentLanguageDetection;
			case OperatorPerformanceCounter.TotalTimeSpentProcessingDocuments:
				return this.mailboxOperatorsInstance.TotalTimeSpentProcessingDocuments;
			case OperatorPerformanceCounter.TotalTimeSpentTokenDeserializer:
				return this.mailboxOperatorsInstance.TotalTimeSpentTokenDeserializer;
			case OperatorPerformanceCounter.TotalTimeSpentWorkBreaking:
				return this.mailboxOperatorsInstance.TotalTimeSpentWorkBreaking;
			default:
				throw new ArgumentException("performanceCounter");
			}
		}

		// Token: 0x040000E3 RID: 227
		private readonly MailboxOperatorsInstance mailboxOperatorsInstance;
	}
}
