using System;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002ED RID: 749
	internal class VoiceMessageCollectionStage : SynchronousPipelineStageBase
	{
		// Token: 0x060016C9 RID: 5833 RVA: 0x00061528 File Offset: 0x0005F728
		public VoiceMessageCollectionStage()
		{
			if (Utils.RunningInTestMode)
			{
				string text = null;
				using (Domain currentDomain = Domain.GetCurrentDomain())
				{
					text = currentDomain.Name;
				}
				if (text.EndsWith(".extest.microsoft.com", StringComparison.InvariantCultureIgnoreCase))
				{
					this.VoiceMessageCollectionAddress = string.Format(CultureInfo.InvariantCulture, "telexdb2user1@{0}", new object[]
					{
						text
					});
				}
				else
				{
					this.VoiceMessageCollectionAddress = string.Empty;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "VoiceMessageCollectionStage - Collection address = '{0}', Domain = '{1}'", new object[]
				{
					this.VoiceMessageCollectionAddress,
					text
				});
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x000615E8 File Offset: 0x0005F7E8
		internal override PipelineDispatcher.PipelineResourceType ResourceType
		{
			get
			{
				return PipelineDispatcher.PipelineResourceType.CpuBound;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x000615EB File Offset: 0x0005F7EB
		internal override TimeSpan ExpectedRunTime
		{
			get
			{
				return TimeSpan.FromMinutes(5.0);
			}
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x000615FC File Offset: 0x0005F7FC
		internal override bool ShouldRunStage(PipelineWorkItem workItem)
		{
			ExAssert.RetailAssert(!string.IsNullOrEmpty(this.VoiceMessageCollectionAddress), "VoiceMessageCollectionAddress should be non-empty");
			IUMCAMessage iumcamessage = base.WorkItem.Message as IUMCAMessage;
			ExAssert.RetailAssert(iumcamessage != null, "VoiceMessageCollectionStage must operate on PipelineContext which implements IUMCAMessage");
			if (!iumcamessage.CollectMessageForAnalysis)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "VoiceMessageCollectionStage.ShouldRunStage - Message should not be collected for analysis", new object[0]);
				return false;
			}
			return true;
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x0006166E File Offset: 0x0005F86E
		protected override StageRetryDetails InternalGetRetrySchedule()
		{
			return new StageRetryDetails(StageRetryDetails.FinalAction.SkipStage);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x00061678 File Offset: 0x0005F878
		protected override void InternalDoSynchronousWork()
		{
			IUMCompressAudio iumcompressAudio = base.WorkItem.Message as IUMCompressAudio;
			ExAssert.RetailAssert(iumcompressAudio != null, "VoiceMessageCollectionStage must operate on PipelineContext that implements IUMCompressAudio.");
			IUMCAMessage iumcamessage = base.WorkItem.Message as IUMCAMessage;
			ExAssert.RetailAssert(iumcamessage != null, "VoiceMessageCollectionStage must operate on PipelineContext which implements IUMCAMessage");
			UMSubscriber umsubscriber = iumcamessage.CAMessageRecipient as UMSubscriber;
			ExAssert.RetailAssert(umsubscriber != null, "VoiceMessageCollectionStage should be run only if message recipient is UM enabled");
			SmtpAddress voiceMessageCollectionAddress = new SmtpAddress(this.VoiceMessageCollectionAddress);
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(umsubscriber.ADRecipient.OrganizationId);
			MicrosoftExchangeRecipient microsoftExchangeRecipient = iadsystemConfigurationLookup.GetMicrosoftExchangeRecipient();
			SmtpAddress primarySmtpAddress = microsoftExchangeRecipient.PrimarySmtpAddress;
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "VoiceMessageCollectionStage - Compress PCM", new object[0]);
			if (iumcompressAudio.FileToCompressPath == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "VoiceMessageCollectionStage - No recording", new object[0]);
				return;
			}
			using (ITempFile tempFile = MediaMethods.FromPcm(iumcompressAudio.FileToCompressPath, AudioCodecEnum.G711))
			{
				string requestId = Path.GetFileNameWithoutExtension(base.WorkItem.HeaderFilename) + "-collection";
				using (MessageItem messageItem = MessageItem.CreateInMemory(StoreObjectSchema.ContentConversionProperties))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "VoiceMessageCollectionStage - Set message properties", new object[0]);
					this.SetMessageProperties(messageItem, microsoftExchangeRecipient, voiceMessageCollectionAddress, tempFile, umsubscriber, base.WorkItem.Message.CallerId, base.WorkItem.Message.MessageToSubmit);
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "VoiceMessageCollectionStage - Send message via SMTP", new object[0]);
					this.SendMessageForCollection(messageItem, primarySmtpAddress, base.WorkItem.Message.TenantGuid, voiceMessageCollectionAddress, umsubscriber, requestId);
				}
			}
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x0006185C File Offset: 0x0005FA5C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<VoiceMessageCollectionStage>(this);
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x00061864 File Offset: 0x0005FA64
		private void SetMessageProperties(MessageItem message, MicrosoftExchangeRecipient sender, SmtpAddress voiceMessageCollectionAddress, ITempFile compressedAudioFile, UMSubscriber subscriber, PhoneNumber callerId, MessageItem originalVoiceMessage)
		{
			string text = voiceMessageCollectionAddress.ToString();
			message.Recipients.Add(new Participant(text, text, "SMTP"), RecipientItemType.To);
			message.From = new Participant(subscriber.ADRecipient);
			message.Sender = new Participant(sender);
			message.Subject = originalVoiceMessage.Subject;
			message.ClassName = "IPM.Note.Microsoft.Partner.UM";
			message[MessageItemSchema.XMsExchangeUMPartnerContent] = "voice";
			message[MessageItemSchema.XMsExchangeUMPartnerContext] = string.Empty;
			if (callerId != null)
			{
				message[MessageItemSchema.SenderTelephoneNumber] = (callerId.ToDial ?? string.Empty);
			}
			message[MessageItemSchema.VoiceMessageDuration] = originalVoiceMessage[MessageItemSchema.VoiceMessageDuration];
			message[MessageItemSchema.VoiceMessageSenderName] = originalVoiceMessage[MessageItemSchema.VoiceMessageSenderName];
			message[MessageItemSchema.XMsExchangeUMDialPlanLanguage] = subscriber.DialPlan.DefaultLanguage.Culture.Name;
			message[MessageItemSchema.XMsExchangeUMCallerInformedOfAnalysis] = (subscriber.UMMailboxPolicy.InformCallerOfVoiceMailAnalysis ? "true" : "false");
			message.AutoResponseSuppress = AutoResponseSuppress.All;
			XsoUtil.AddAttachment(message.AttachmentCollection, compressedAudioFile, Path.GetFileName(compressedAudioFile.FilePath), AudioCodec.GetMimeType(AudioCodecEnum.G711));
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x000619A8 File Offset: 0x0005FBA8
		private void SendMessageForCollection(MessageItem message, SmtpAddress senderAddress, Guid senderOrgGuid, SmtpAddress voiceMessageCollectionAddress, UMSubscriber subscriber, string requestId)
		{
			OutboundConversionOptions outboundConversionOptions = XsoUtil.GetOutboundConversionOptions(subscriber);
			SmtpSubmissionHelper.SubmitMessage(message, senderAddress.ToString(), senderOrgGuid, voiceMessageCollectionAddress.ToString(), outboundConversionOptions, requestId);
		}

		// Token: 0x04000D77 RID: 3447
		private readonly string VoiceMessageCollectionAddress = "vmailng@microsoft.com";
	}
}
