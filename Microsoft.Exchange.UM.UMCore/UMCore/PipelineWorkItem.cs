using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002DD RID: 733
	internal class PipelineWorkItem : DisposableBase
	{
		// Token: 0x06001641 RID: 5697 RVA: 0x0005EF1C File Offset: 0x0005D11C
		private PipelineWorkItem(PipelineContext msg, Guid workId)
		{
			this.workId = workId;
			this.message = msg;
			this.statisticsLogRow.SentTime = this.Message.SentTime.UniversalTime;
			this.statisticsLogRow.WorkId = this.WorkId;
			this.statisticsLogRow.MessageType = this.Message.MessageType;
			this.InitializePipeline();
			this.creationTime = new ExDateTime(ExTimeZone.CurrentTimeZone, File.GetCreationTime(this.HeaderFilename));
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x0005EFC4 File Offset: 0x0005D1C4
		internal PipelineStageBase CurrentStage
		{
			get
			{
				return this.myPipeline[this.pipelineStageNum];
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x0005EFD7 File Offset: 0x0005D1D7
		internal TimeSpan ExpectedRunTime
		{
			get
			{
				return this.expectedRunTime;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x0005EFDF File Offset: 0x0005D1DF
		// (set) Token: 0x06001645 RID: 5701 RVA: 0x0005EFE7 File Offset: 0x0005D1E7
		internal bool SLARecorded { get; set; }

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x0005EFF0 File Offset: 0x0005D1F0
		// (set) Token: 0x06001647 RID: 5703 RVA: 0x0005EFF8 File Offset: 0x0005D1F8
		internal bool IsRejected { get; set; }

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x0005F001 File Offset: 0x0005D201
		internal Guid WorkId
		{
			get
			{
				return this.workId;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x0005F009 File Offset: 0x0005D209
		internal string HeaderFilename
		{
			get
			{
				return this.message.HeaderFileName;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0005F016 File Offset: 0x0005D216
		internal PipelineContext Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x0005F01E File Offset: 0x0005D21E
		internal bool IsComplete
		{
			get
			{
				return this.pipelineStageNum >= this.myPipeline.Count;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x0005F036 File Offset: 0x0005D236
		// (set) Token: 0x0600164D RID: 5709 RVA: 0x0005F03E File Offset: 0x0005D23E
		internal bool IsRunning
		{
			get
			{
				return this.isRunning;
			}
			set
			{
				this.isRunning = value;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x0005F048 File Offset: 0x0005D248
		internal bool HeaderFileExists
		{
			get
			{
				bool result;
				try
				{
					result = File.Exists(this.HeaderFilename);
				}
				catch (IOException)
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x0005F07C File Offset: 0x0005D27C
		internal TimeSpan TimeInQueue
		{
			get
			{
				return ExDateTime.Now.Subtract(this.creationTime);
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x0005F09C File Offset: 0x0005D29C
		internal TranscriptionContext TranscriptionContext
		{
			get
			{
				return this.transcriptionContext;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x0005F0A4 File Offset: 0x0005D2A4
		internal PipelineStatisticsLogger.PipelineStatisticsLogRow PipelineStatisticsLogRow
		{
			get
			{
				return this.statisticsLogRow;
			}
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x0005F0AC File Offset: 0x0005D2AC
		public override bool Equals(object obj)
		{
			PipelineWorkItem pipelineWorkItem = obj as PipelineWorkItem;
			return pipelineWorkItem != null && this.WorkId.Equals(pipelineWorkItem.WorkId);
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x0005F0DC File Offset: 0x0005D2DC
		public override int GetHashCode()
		{
			return this.WorkId.GetHashCode();
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x0005F0FD File Offset: 0x0005D2FD
		public PipelineDispatcher.WIThrottleData GetThrottlingData()
		{
			if (this.message != null)
			{
				return this.message.GetThrottlingData();
			}
			return null;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0005F114 File Offset: 0x0005D314
		internal static bool TryCreate(FileInfo diskQueueItem, Guid workId, out PipelineWorkItem workItem)
		{
			workItem = null;
			PipelineContext pipelineContext = null;
			try
			{
				pipelineContext = PipelineContext.FromHeaderFile(diskQueueItem.FullName);
				if (pipelineContext != null)
				{
					if (pipelineContext.ProcessedCount >= PipelineWorkItem.ProcessedCountMax)
					{
						throw new ReachMaxProcessedTimesException(PipelineWorkItem.ProcessedCountMax.ToString(CultureInfo.InvariantCulture));
					}
					workItem = new PipelineWorkItem(pipelineContext, workId);
				}
			}
			finally
			{
				if (workItem == null && pipelineContext != null)
				{
					pipelineContext.Dispose();
					pipelineContext = null;
				}
			}
			return null != workItem;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x0005F190 File Offset: 0x0005D390
		internal void AdvanceToNextStage()
		{
			do
			{
				this.pipelineStageNum++;
			}
			while (!this.IsComplete && !this.CurrentStage.ShouldRunStage(this));
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x0005F1B8 File Offset: 0x0005D3B8
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.myPipeline != null)
				{
					foreach (PipelineStageBase pipelineStageBase in this.myPipeline)
					{
						pipelineStageBase.Dispose();
					}
				}
				if (this.message != null)
				{
					this.message.Dispose();
				}
				TranscriptionStage.UpdateBacklog(-this.transcriptionContext.BacklogContribution);
				this.transcriptionContext.BacklogContribution = TimeSpan.Zero;
			}
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0005F250 File Offset: 0x0005D450
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PipelineWorkItem>(this);
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x0005F258 File Offset: 0x0005D458
		private void InitializePipeline()
		{
			this.myPipeline = new List<PipelineStageBase>(this.message.Pipeline.Count + 1);
			for (int i = 0; i < this.message.Pipeline.Count; i++)
			{
				IPipelineStageFactory factory = this.message.Pipeline[i];
				this.AddStage(factory);
			}
			this.AddStage(PipelineWorkItem.PostCompletionStage.Factory);
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0005F2C4 File Offset: 0x0005D4C4
		private void AddStage(IPipelineStageFactory factory)
		{
			PipelineStageBase pipelineStageBase = factory.CreateStage(this);
			this.expectedRunTime += pipelineStageBase.ExpectedRunTime;
			this.myPipeline.Add(pipelineStageBase);
		}

		// Token: 0x04000D46 RID: 3398
		internal static readonly int ProcessedCountMax = 6;

		// Token: 0x04000D47 RID: 3399
		private Guid workId = Guid.Empty;

		// Token: 0x04000D48 RID: 3400
		private int pipelineStageNum;

		// Token: 0x04000D49 RID: 3401
		private bool isRunning;

		// Token: 0x04000D4A RID: 3402
		private PipelineContext message;

		// Token: 0x04000D4B RID: 3403
		private List<PipelineStageBase> myPipeline;

		// Token: 0x04000D4C RID: 3404
		private TranscriptionContext transcriptionContext = new TranscriptionContext();

		// Token: 0x04000D4D RID: 3405
		private PipelineStatisticsLogger.PipelineStatisticsLogRow statisticsLogRow = new PipelineStatisticsLogger.PipelineStatisticsLogRow();

		// Token: 0x04000D4E RID: 3406
		private ExDateTime creationTime;

		// Token: 0x04000D4F RID: 3407
		private TimeSpan expectedRunTime;

		// Token: 0x020002DE RID: 734
		private class PostCompletionStage : SynchronousPipelineStageBase, IUMNetworkResource
		{
			// Token: 0x170005A6 RID: 1446
			// (get) Token: 0x0600165C RID: 5724 RVA: 0x0005F304 File Offset: 0x0005D504
			public static IPipelineStageFactory Factory
			{
				get
				{
					return PipelineWorkItem.PostCompletionStage.factory;
				}
			}

			// Token: 0x170005A7 RID: 1447
			// (get) Token: 0x0600165D RID: 5725 RVA: 0x0005F30B File Offset: 0x0005D50B
			internal override PipelineDispatcher.PipelineResourceType ResourceType
			{
				get
				{
					return PipelineDispatcher.PipelineResourceType.NetworkBound;
				}
			}

			// Token: 0x170005A8 RID: 1448
			// (get) Token: 0x0600165E RID: 5726 RVA: 0x0005F30E File Offset: 0x0005D50E
			public string NetworkResourceId
			{
				get
				{
					return base.WorkItem.Message.GetMailboxServerId();
				}
			}

			// Token: 0x170005A9 RID: 1449
			// (get) Token: 0x0600165F RID: 5727 RVA: 0x0005F320 File Offset: 0x0005D520
			internal override TimeSpan ExpectedRunTime
			{
				get
				{
					return TimeSpan.FromMinutes(1.0);
				}
			}

			// Token: 0x06001660 RID: 5728 RVA: 0x0005F330 File Offset: 0x0005D530
			protected override StageRetryDetails InternalGetRetrySchedule()
			{
				return new StageRetryDetails(StageRetryDetails.FinalAction.SkipStage);
			}

			// Token: 0x06001661 RID: 5729 RVA: 0x0005F338 File Offset: 0x0005D538
			protected override void InternalDoSynchronousWork()
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "PostCompletionStage - InternalDoSynchronousWork", new object[0]);
				base.WorkItem.Message.PostCompletion();
			}

			// Token: 0x06001662 RID: 5730 RVA: 0x0005F36A File Offset: 0x0005D56A
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<PipelineWorkItem.PostCompletionStage>(this);
			}

			// Token: 0x06001663 RID: 5731 RVA: 0x0005F374 File Offset: 0x0005D574
			protected override void InternalDispose(bool disposing)
			{
				if (disposing)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "PostCompletionStage.InternalDispose", new object[0]);
					MessageItem messageToSubmit = base.WorkItem.Message.MessageToSubmit;
					if (messageToSubmit != null)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "PostCompletionStage - Disposing message item", new object[0]);
						messageToSubmit.Dispose();
					}
				}
				base.InternalDispose(disposing);
			}

			// Token: 0x04000D52 RID: 3410
			private static IPipelineStageFactory factory = new PipelineStageFactory<PipelineWorkItem.PostCompletionStage>();
		}
	}
}
