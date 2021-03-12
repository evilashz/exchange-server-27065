using System;
using System.Diagnostics;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000027 RID: 39
	internal class TransportFlowStatistics
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000D8B8 File Offset: 0x0000BAB8
		public object SyncRoot
		{
			[DebuggerStepThrough]
			get
			{
				return this.syncRoot;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000D8C0 File Offset: 0x0000BAC0
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000D8C8 File Offset: 0x0000BAC8
		public TimeSpan TotalTimeProcessingMessages { get; private set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000D8D1 File Offset: 0x0000BAD1
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0000D8D9 File Offset: 0x0000BAD9
		public TimeSpan TimeInGetConnection { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000D8E2 File Offset: 0x0000BAE2
		// (set) Token: 0x06000231 RID: 561 RVA: 0x0000D8EA File Offset: 0x0000BAEA
		public TimeSpan TimeInPropertyBagLoad { get; private set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000D8F3 File Offset: 0x0000BAF3
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000D8FB File Offset: 0x0000BAFB
		public TimeSpan TimeInMessageItemConversion { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000D904 File Offset: 0x0000BB04
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0000D90C File Offset: 0x0000BB0C
		public TimeSpan TimeDeterminingAgeOfItem { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000D915 File Offset: 0x0000BB15
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0000D91D File Offset: 0x0000BB1D
		public TimeSpan TimeInMimeConversion { get; private set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000D926 File Offset: 0x0000BB26
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0000D92E File Offset: 0x0000BB2E
		public TimeSpan TimeInShouldAnnotateMessage { get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000D937 File Offset: 0x0000BB37
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000D93F File Offset: 0x0000BB3F
		public TimeSpan TimeProcessingFailedMessages { get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000D948 File Offset: 0x0000BB48
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000D950 File Offset: 0x0000BB50
		public TimeSpan TimeInQueue { get; private set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000D959 File Offset: 0x0000BB59
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0000D961 File Offset: 0x0000BB61
		public TimeSpan TimeInTransportRetriever { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000D96A File Offset: 0x0000BB6A
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000D972 File Offset: 0x0000BB72
		public TimeSpan TimeInDocParser { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000D97B File Offset: 0x0000BB7B
		// (set) Token: 0x06000243 RID: 579 RVA: 0x0000D983 File Offset: 0x0000BB83
		public TimeSpan TimeInWordbreaker { get; private set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000D98C File Offset: 0x0000BB8C
		// (set) Token: 0x06000245 RID: 581 RVA: 0x0000D994 File Offset: 0x0000BB94
		public TimeSpan TimeInNLGSubflow { get; private set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000D99D File Offset: 0x0000BB9D
		// (set) Token: 0x06000247 RID: 583 RVA: 0x0000D9A5 File Offset: 0x0000BBA5
		public int TotalMessagesProcessed { get; private set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000D9AE File Offset: 0x0000BBAE
		// (set) Token: 0x06000249 RID: 585 RVA: 0x0000D9B6 File Offset: 0x0000BBB6
		public int MessagesSuccessfullyAnnotated { get; private set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000D9BF File Offset: 0x0000BBBF
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0000D9C7 File Offset: 0x0000BBC7
		public int ConnectionLevelFailures { get; private set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000D9D0 File Offset: 0x0000BBD0
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0000D9D8 File Offset: 0x0000BBD8
		public int AnnotationSkipped { get; private set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000D9E1 File Offset: 0x0000BBE1
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000D9E9 File Offset: 0x0000BBE9
		public int MessageLevelFailures { get; private set; }

		// Token: 0x06000250 RID: 592 RVA: 0x0000D9F4 File Offset: 0x0000BBF4
		public void UpdateProcessingTimes(TimeSpan elapsed, TransportFlowStatistics.ProcessingStatus status)
		{
			lock (this.syncRoot)
			{
				this.TotalTimeProcessingMessages += elapsed;
				switch (status)
				{
				case TransportFlowStatistics.ProcessingStatus.Success:
					this.TotalMessagesProcessed++;
					this.MessagesSuccessfullyAnnotated++;
					break;
				case TransportFlowStatistics.ProcessingStatus.AnnotationSkipped:
					this.AnnotationSkipped++;
					break;
				case TransportFlowStatistics.ProcessingStatus.FailedToConnect:
					this.TotalMessagesProcessed++;
					this.ConnectionLevelFailures++;
					break;
				case TransportFlowStatistics.ProcessingStatus.FailedToProcess:
					this.TotalMessagesProcessed++;
					this.MessageLevelFailures++;
					this.TimeProcessingFailedMessages += elapsed;
					break;
				default:
					throw new ArgumentException("status");
				}
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000DAE4 File Offset: 0x0000BCE4
		public void UpdateOperatorTimings(TransportFlowOperatorTimings transportFlowOperationResult)
		{
			lock (this.syncRoot)
			{
				this.TimeInQueue += TimeSpan.FromMilliseconds((double)transportFlowOperationResult.TimeInQueueInMsec);
				this.TimeInDocParser += TimeSpan.FromMilliseconds((double)transportFlowOperationResult.TimeInDocParserInMsec);
				this.TimeInWordbreaker += TimeSpan.FromMilliseconds((double)transportFlowOperationResult.TimeInWordbreakerInMsec);
				this.TimeInNLGSubflow += TimeSpan.FromMilliseconds((double)transportFlowOperationResult.TimeInNLGSubflowInMsec);
				this.TimeInTransportRetriever += TimeSpan.FromMilliseconds((double)transportFlowOperationResult.TimeInTransportRetrieverInMsec);
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000DBB0 File Offset: 0x0000BDB0
		public void UpdateClientOperationTimings(ClientSideTimings clientSideTimings)
		{
			lock (this.syncRoot)
			{
				this.TimeInGetConnection += clientSideTimings.TimeInGetConnection;
				this.TimeInPropertyBagLoad += clientSideTimings.TimeInPropertyBagLoad;
				this.TimeInMessageItemConversion += clientSideTimings.TimeInMessageItemConversion;
				this.TimeDeterminingAgeOfItem += clientSideTimings.TimeDeterminingAgeOfItem;
				this.TimeInMimeConversion += clientSideTimings.TimeInMimeConversion;
				this.TimeInShouldAnnotateMessage += clientSideTimings.TimeInShouldAnnotateMessage;
			}
		}

		// Token: 0x04000103 RID: 259
		private readonly object syncRoot = new object();

		// Token: 0x02000028 RID: 40
		public enum ProcessingStatus
		{
			// Token: 0x04000117 RID: 279
			Success,
			// Token: 0x04000118 RID: 280
			AnnotationSkipped,
			// Token: 0x04000119 RID: 281
			FailedToConnect,
			// Token: 0x0400011A RID: 282
			FailedToProcess
		}
	}
}
