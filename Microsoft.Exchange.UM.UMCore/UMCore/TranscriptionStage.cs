using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.Prompts.Config;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002EB RID: 747
	internal class TranscriptionStage : AsynchronousPipelineStageBase
	{
		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x00060653 File Offset: 0x0005E853
		internal override PipelineDispatcher.PipelineResourceType ResourceType
		{
			get
			{
				return PipelineDispatcher.PipelineResourceType.LowPriorityCpuBound;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x00060656 File Offset: 0x0005E856
		internal override TimeSpan ExpectedRunTime
		{
			get
			{
				return TimeSpan.FromMinutes(3.0);
			}
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00060668 File Offset: 0x0005E868
		internal static TimeSpan GetBacklog()
		{
			TimeSpan result;
			lock (TranscriptionStage.staticLock)
			{
				result = TranscriptionStage.transcriptionBacklog;
			}
			return result;
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x000606A8 File Offset: 0x0005E8A8
		internal static void UpdateBacklog(TimeSpan amount)
		{
			lock (TranscriptionStage.staticLock)
			{
				TranscriptionStage.transcriptionBacklog = TranscriptionStage.transcriptionBacklog.Add(amount);
			}
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x000606F4 File Offset: 0x0005E8F4
		internal override bool ShouldRunStage(PipelineWorkItem workItem)
		{
			return base.WorkItem.TranscriptionContext.ShouldRunTranscriptionStage;
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x00060706 File Offset: 0x0005E906
		protected override StageRetryDetails InternalGetRetrySchedule()
		{
			return new StageRetryDetails(StageRetryDetails.FinalAction.SkipStage);
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x00060710 File Offset: 0x0005E910
		protected override void InternalStartAsynchronousWork()
		{
			IUMTranscribeAudio iumtranscribeAudio = base.WorkItem.Message as IUMTranscribeAudio;
			ExAssert.RetailAssert(iumtranscribeAudio != null, "TranscriptionStage must operate only on PipelineContext which implements IUMTranscribeAudio");
			TranscriptionStage.UpdateBacklog(-base.WorkItem.TranscriptionContext.BacklogContribution);
			base.WorkItem.TranscriptionContext.BacklogContribution = TimeSpan.Zero;
			iumtranscribeAudio.TranscriptionData = new TranscriptionData(RecoResultType.Skipped, RecoErrorType.Other, base.WorkItem.TranscriptionContext.Culture, new List<IUMTranscriptionResult>());
			if (!Platform.Builder.TryCreateOfflineTranscriber(base.WorkItem.TranscriptionContext.Culture, out this.transcriber))
			{
				throw new InvalidOperationException("Transcriber not supported, but transcription configuration stage indicated it was supported");
			}
			this.transcriber.TranscribeCompleted += this.OnTranscribeCompleted;
			IUMResolveCaller iumresolveCaller = base.WorkItem.Message as IUMResolveCaller;
			if (iumresolveCaller != null)
			{
				this.transcriber.CallerInfo = iumresolveCaller.ContactInfo;
			}
			this.transcriber.TranscriptionUser = iumtranscribeAudio.TranscriptionUser;
			this.transcriber.CallingLineId = base.WorkItem.Message.CallerId.Number;
			ExAssert.RetailAssert(iumtranscribeAudio.EnableTopNGrammar || null == this.transcriber.TopN, "TopN data should only exist if the topN grammar is enabled.");
			this.transcriber.TopN = base.WorkItem.TranscriptionContext.TopN;
			this.audioFile = this.PrepareFileForTranscription(iumtranscribeAudio.AttachmentPath);
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Start transcribing file: {0}. Time allowed  for transcription: {1}", new object[]
			{
				this.audioFile.FilePath,
				GlobCfg.MessageTranscriptionTimeout
			});
			this.transcriber.TranscribeFile(this.audioFile.FilePath);
			this.startTime = ExDateTime.UtcNow;
			TranscriptionCountersInstance instance = TranscriptionCounters.GetInstance(base.WorkItem.TranscriptionContext.Culture.Name);
			Util.IncrementCounter(instance.CurrentSessions);
			Util.IncrementCounter(AvailabilityCounters.PercentageTranscriptionFailures_Base);
			PipelineDispatcher.Instance.OnShutdown += this.OnShutdown;
			this.transcriptionTimer = new Timer(new TimerCallback(this.OnTimeout), null, GlobCfg.MessageTranscriptionTimeout, TimeSpan.Zero);
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x00060941 File Offset: 0x0005EB41
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<TranscriptionStage>(this);
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x0006094C File Offset: 0x0005EB4C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Entering TranscriptionStage.Dispose", new object[0]);
				PipelineDispatcher.Instance.OnShutdown -= this.OnShutdown;
				if (this.transcriber != null)
				{
					this.transcriber.TranscribeCompleted -= this.OnTranscribeCompleted;
					this.transcriber.Dispose();
					this.transcriber = null;
				}
				if (this.transcriptionTimer != null)
				{
					this.transcriptionTimer.Dispose();
					this.transcriptionTimer = null;
				}
				if (this.audioFile != null)
				{
					this.audioFile.Dispose();
					this.audioFile = null;
				}
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x00060A01 File Offset: 0x0005EC01
		private void OnShutdown(object sender, EventArgs e)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Entering TranscriptionStage.OnShutdown", new object[0]);
			this.CancelTranscription();
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x00060A29 File Offset: 0x0005EC29
		private void OnTimeout(object state)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Entering TranscriptionStage.OnTimeout", new object[0]);
			this.CancelTranscription();
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x00060A54 File Offset: 0x0005EC54
		private void CancelTranscription()
		{
			try
			{
				lock (this)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Entering TranscriptionStage.CancelTranscription", new object[0]);
					if (!this.completed && !this.cancelled)
					{
						this.transcriber.CancelTranscription();
						this.cancelled = true;
					}
				}
			}
			catch (InvalidOperationException)
			{
				BaseUMOfflineTranscriber.TranscribeCompletedEventArgs tcea = new BaseUMOfflineTranscriber.TranscribeCompletedEventArgs(new List<IUMTranscriptionResult>(), null, true, null);
				this.OnTranscribeCompleted(this, tcea);
			}
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00060AF4 File Offset: 0x0005ECF4
		private void OnTranscribeCompleted(object sender, BaseUMOfflineTranscriber.TranscribeCompletedEventArgs tcea)
		{
			Exception ex = null;
			try
			{
				IUMTranscribeAudio iumtranscribeAudio = base.WorkItem.Message as IUMTranscribeAudio;
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Entering TranscriptionStage.OnTranscribeCompleted", new object[0]);
				lock (this)
				{
					if (this.completed)
					{
						return;
					}
					this.completed = true;
				}
				TranscriptionCountersInstance instance = TranscriptionCounters.GetInstance(base.WorkItem.TranscriptionContext.Culture.Name);
				Util.DecrementCounter(instance.CurrentSessions);
				if (tcea.Error != null)
				{
					CallIdTracer.TraceWarning(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "TranscribeCompleted completed with an error: {0}. Deliver the voicemail with no transcription.", new object[]
					{
						tcea.Error
					});
					this.recoResultType = RecoResultType.Skipped;
					this.recoErrorType = RecoErrorType.Other;
				}
				else
				{
					this.transcriptionResults.AddRange(tcea.TranscriptionResults);
					if (tcea.Cancelled)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Transcription was cancelled", new object[0]);
						this.recoResultType = RecoResultType.Partial;
						this.recoErrorType = RecoErrorType.Success;
					}
					else
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Transcription completed successfully", new object[0]);
						this.recoResultType = RecoResultType.Attempted;
						this.recoErrorType = RecoErrorType.Success;
					}
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "StageComplete(error='{0}')", new object[]
				{
					ex
				});
				TranscriptionData transcriptionData = new TranscriptionData(this.recoResultType, this.recoErrorType, base.WorkItem.TranscriptionContext.Culture, this.transcriptionResults);
				if (base.WorkItem.TranscriptionContext.TopN != null)
				{
					base.WorkItem.TranscriptionContext.TopN.TryCache();
				}
				transcriptionData.UpdatePerfCounters();
				transcriptionData.UpdateStatistics(base.WorkItem.PipelineStatisticsLogRow);
				base.WorkItem.PipelineStatisticsLogRow.TranscriptionElapsedTime = ExDateTime.UtcNow - this.startTime;
				iumtranscribeAudio.TranscriptionData = transcriptionData;
			}
			catch (Exception ex2)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Error happened in OnTranscribeCompleted e='{0}'", new object[]
				{
					ex2
				});
				ex = ex2;
			}
			finally
			{
				base.StageCompletionCallback(this, base.WorkItem, ex);
			}
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00060D94 File Offset: 0x0005EF94
		private ITempWavFile PrepareFileForTranscription(string attachmentPath)
		{
			CultureInfo culture = base.WorkItem.TranscriptionContext.Culture;
			double noiseFloorLevelDB = SafeConvert.ToDouble(Strings.TranscriptionNoiseFloorLevelDB.ToString(culture), -100.0, 0.0, -78.0);
			double normalizationLevelDB = SafeConvert.ToDouble(Strings.TranscriptionNormalizationLevelDB.ToString(culture), -25.0, 0.0, -10.0);
			return MediaMethods.NormalizeAudio(attachmentPath, noiseFloorLevelDB, normalizationLevelDB);
		}

		// Token: 0x04000D6B RID: 3435
		private static TimeSpan lazyTranscriptionTimeout = TimeSpan.MinValue;

		// Token: 0x04000D6C RID: 3436
		private static TimeSpan transcriptionBacklog = new TimeSpan(0, 0, 0);

		// Token: 0x04000D6D RID: 3437
		private static object staticLock = new object();

		// Token: 0x04000D6E RID: 3438
		private BaseUMOfflineTranscriber transcriber;

		// Token: 0x04000D6F RID: 3439
		private List<IUMTranscriptionResult> transcriptionResults = new List<IUMTranscriptionResult>();

		// Token: 0x04000D70 RID: 3440
		private RecoResultType recoResultType = RecoResultType.Skipped;

		// Token: 0x04000D71 RID: 3441
		private RecoErrorType recoErrorType = RecoErrorType.Other;

		// Token: 0x04000D72 RID: 3442
		private Timer transcriptionTimer;

		// Token: 0x04000D73 RID: 3443
		private bool cancelled;

		// Token: 0x04000D74 RID: 3444
		private bool completed;

		// Token: 0x04000D75 RID: 3445
		private ExDateTime startTime;

		// Token: 0x04000D76 RID: 3446
		private ITempWavFile audioFile;
	}
}
