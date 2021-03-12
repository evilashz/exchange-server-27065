using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002E9 RID: 745
	internal class TranscriptionConfigurationStage : SynchronousPipelineStageBase, IUMNetworkResource
	{
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x0006005C File Offset: 0x0005E25C
		internal override PipelineDispatcher.PipelineResourceType ResourceType
		{
			get
			{
				return PipelineDispatcher.PipelineResourceType.NetworkBound;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x0006005F File Offset: 0x0005E25F
		public string NetworkResourceId
		{
			get
			{
				return base.WorkItem.Message.GetMailboxServerId();
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x00060071 File Offset: 0x0005E271
		internal override TimeSpan ExpectedRunTime
		{
			get
			{
				return TimeSpan.FromMinutes(1.0);
			}
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00060081 File Offset: 0x0005E281
		protected override StageRetryDetails InternalGetRetrySchedule()
		{
			return new StageRetryDetails(StageRetryDetails.FinalAction.SkipStage);
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00060089 File Offset: 0x0005E289
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<TranscriptionConfigurationStage>(this);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00060091 File Offset: 0x0005E291
		protected override void InternalDoSynchronousWork()
		{
			this.BuildTranscriptionConfiguration();
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0006009C File Offset: 0x0005E29C
		private bool ShouldRunTranscriptionStage(UMSubscriber subscriber, TranscriptionEnabledSetting transcriptionEnabled, out RecoResultType recoResultType, out RecoErrorType recoErrorType, out CultureInfo culture)
		{
			bool result = false;
			culture = null;
			recoResultType = RecoResultType.Skipped;
			recoErrorType = RecoErrorType.Other;
			try
			{
				culture = Util.GetDefaultCulture(subscriber.DialPlan);
				if (transcriptionEnabled == TranscriptionEnabledSetting.Unknown)
				{
					PIIMessage data = PIIMessage.Create(PIIType._User, subscriber.ADRecipient.DistinguishedName);
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), data, "Could not retrieve transcription enabled setting in mailbox of user '_User'", new object[0]);
					recoResultType = RecoResultType.Skipped;
					recoErrorType = RecoErrorType.ErrorReadingSettings;
				}
				else if (!this.IsTranscriptionLanguageSupported(culture))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Language {0} is NOT supported for transcription", new object[]
					{
						culture
					});
					recoResultType = RecoResultType.Skipped;
					recoErrorType = RecoErrorType.LanguageNotSupported;
				}
				else if (this.IsMessageTooLongForTranscription())
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Message is too long. Skipping", new object[0]);
					recoResultType = RecoResultType.Skipped;
					recoErrorType = RecoErrorType.MessageTooLong;
				}
				else if (this.ShouldThrottleTranscription())
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Transcription throttle check failed. Skipping", new object[0]);
					recoResultType = RecoResultType.Skipped;
					recoErrorType = RecoErrorType.Throttled;
				}
				else
				{
					result = true;
				}
			}
			catch (LocalizedException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Error determining if transcription should be run. e='{0}'", new object[]
				{
					ex
				});
				recoResultType = RecoResultType.Skipped;
				recoErrorType = RecoErrorType.Other;
			}
			return result;
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x000601F4 File Offset: 0x0005E3F4
		private void BuildTranscriptionConfiguration()
		{
			IUMTranscribeAudio iumtranscribeAudio = base.WorkItem.Message as IUMTranscribeAudio;
			ExAssert.RetailAssert(iumtranscribeAudio != null, "TranscriptionStage must operate only on PipelineContext which implements IUMTranscribeAudio");
			if (iumtranscribeAudio.Duration.TotalSeconds <= 0.0 || iumtranscribeAudio.AttachmentPath == null)
			{
				base.WorkItem.TranscriptionContext.ShouldRunTranscriptionStage = false;
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "There is no recording to perform transcription upon", new object[0]);
				return;
			}
			if (iumtranscribeAudio.TranscriptionData != null)
			{
				base.WorkItem.TranscriptionContext.ShouldRunTranscriptionStage = false;
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "TranscriptionData already available, skipping EVM stage.", new object[0]);
				return;
			}
			RecoResultType recognitionResult = RecoResultType.Skipped;
			RecoErrorType recognitionError = RecoErrorType.Other;
			CultureInfo cultureInfo = null;
			TranscriptionEnabledSetting transcriptionEnabledSetting = this.IsTranscriptionEnabled();
			if (transcriptionEnabledSetting == TranscriptionEnabledSetting.Disabled)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Transcription for subscriber '{0}' and this message is not enabled. ", new object[]
				{
					iumtranscribeAudio.TranscriptionUser
				});
				return;
			}
			bool flag = this.ShouldRunTranscriptionStage(iumtranscribeAudio.TranscriptionUser, transcriptionEnabledSetting, out recognitionResult, out recognitionError, out cultureInfo);
			if (flag)
			{
				base.WorkItem.TranscriptionContext.Culture = cultureInfo;
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Message will be transcribed.  Updating transcription backlog with '{0}'.", new object[]
				{
					iumtranscribeAudio.Duration
				});
				base.WorkItem.TranscriptionContext.BacklogContribution = iumtranscribeAudio.Duration;
				TranscriptionStage.UpdateBacklog(iumtranscribeAudio.Duration);
				OffensiveWordsFilter offensiveWordsFilter;
				if (!OffensiveWordsFilter.TryGet(cultureInfo, out offensiveWordsFilter))
				{
					ExAssert.RetailAssert(false, "Couldn't find offensive words for transcription language '{0}'", new object[]
					{
						cultureInfo
					});
				}
				base.WorkItem.TranscriptionContext.ShouldRunTranscriptionStage = flag;
				base.WorkItem.TranscriptionContext.TopN = (iumtranscribeAudio.EnableTopNGrammar ? TopNData.Create(iumtranscribeAudio.TranscriptionUser, offensiveWordsFilter) : null);
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Message will NOT be transcribed.  Setting default transcription data.", new object[0]);
			TranscriptionData transcriptionData = new TranscriptionData(recognitionResult, recognitionError, cultureInfo, new List<IUMTranscriptionResult>());
			transcriptionData.UpdatePerfCounters();
			iumtranscribeAudio.TranscriptionData = transcriptionData;
			base.WorkItem.TranscriptionContext.ShouldRunTranscriptionStage = false;
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00060424 File Offset: 0x0005E624
		private TranscriptionEnabledSetting IsTranscriptionEnabled()
		{
			IUMTranscribeAudio iumtranscribeAudio = base.WorkItem.Message as IUMTranscribeAudio;
			ExAssert.RetailAssert(iumtranscribeAudio != null, "TranscriptionStage must operate only on PipelineContext which implements IUMTranscribeAudio");
			if (iumtranscribeAudio.TranscriptionUser == null)
			{
				return TranscriptionEnabledSetting.Disabled;
			}
			VoiceMailTypeEnum voiceMailType = (base.WorkItem.Message is IUMCAMessage) ? VoiceMailTypeEnum.ReceivedVoiceMails : VoiceMailTypeEnum.SentVoiceMails;
			TranscriptionEnabledSetting transcriptionEnabledInMailboxConfig = iumtranscribeAudio.TranscriptionUser.IsTranscriptionEnabledInMailboxConfig(voiceMailType);
			return UMSubscriber.IsTranscriptionEnabled(iumtranscribeAudio.TranscriptionUser.UMMailboxPolicy, transcriptionEnabledInMailboxConfig);
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x00060494 File Offset: 0x0005E694
		private bool IsTranscriptionLanguageSupported(CultureInfo c)
		{
			if (Platform.Utilities.IsTranscriptionLanguageSupported(c))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "The culture '{0}' is supported for transcription", new object[]
				{
					c
				});
				return true;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "The culture '{0}' is NOT enabled for transcription", new object[]
			{
				c
			});
			return false;
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00060500 File Offset: 0x0005E700
		private bool IsMessageTooLongForTranscription()
		{
			IUMTranscribeAudio iumtranscribeAudio = base.WorkItem.Message as IUMTranscribeAudio;
			return iumtranscribeAudio.Duration >= GlobCfg.TranscriptionMaximumMessageLength;
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x00060530 File Offset: 0x0005E730
		private bool ShouldThrottleTranscription()
		{
			IUMTranscribeAudio iumtranscribeAudio = base.WorkItem.Message as IUMTranscribeAudio;
			TimeSpan backlog = TranscriptionStage.GetBacklog();
			TimeSpan timeSpan = backlog + iumtranscribeAudio.Duration;
			int totalResourceCount = PipelineDispatcher.Instance.GetTotalResourceCount(PipelineDispatcher.PipelineResourceType.CpuBound);
			double num = GlobCfg.TranscriptionMaximumBacklogPerCore.TotalSeconds * (double)totalResourceCount;
			bool result;
			if (timeSpan.TotalSeconds > num)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "The duration of the message '{0}' exceeds the maximum transcription backlog value '{1}'.", new object[]
				{
					iumtranscribeAudio.Duration,
					num
				});
				result = true;
			}
			else
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "The message passes the throttle check for transcription.", new object[0]);
				result = false;
			}
			return result;
		}
	}
}
