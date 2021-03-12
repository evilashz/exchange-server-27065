using System;
using System.Globalization;
using Microsoft.Exchange.Data.ApplicationLogic.UM;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002EC RID: 748
	internal static class UMMessageSubmission
	{
		// Token: 0x060016BF RID: 5823 RVA: 0x00060E58 File Offset: 0x0005F058
		internal static void SubmitVoiceMail(string callId, PhoneNumber callerId, UMSubscriber caller, UMRecipient recipient, CultureInfo dialPlanCultureInfo, AudioCodecEnum codec, int messageLength, string waveFile, bool imp, string subject, bool callAnswering, bool privacyMarked, TranscriptionEnabledSetting transcriptionEnabledInMailboxConfig, ITranscriptionData partnerTranscription, string callerIdDisplayName, Guid tenantGuid)
		{
			string displayName = recipient.ADRecipient.DisplayName;
			UMSubscriber umsubscriber = recipient as UMSubscriber;
			PIIMessage data = PIIMessage.Create(PIIType._EmailAddress, recipient.MailAddress);
			SubmissionHelper helper = new SubmissionHelper(callId, callerId, recipient.ADRecipient.Guid, displayName, (umsubscriber != null) ? umsubscriber.MessageSubmissionCulture.Name : dialPlanCultureInfo.Name, (caller != null) ? caller.MailAddress : null, (caller != null) ? caller.DisplayName : null, callerIdDisplayName, tenantGuid);
			bool flag = UMMessageSubmission.ShouldSendToPartnerForTranscription(umsubscriber, true, messageLength, privacyMarked, transcriptionEnabledInMailboxConfig, ref partnerTranscription);
			if (flag)
			{
				using (PartnerTranscriptionRequestPipelineContext partnerTranscriptionRequestPipelineContext = new PartnerTranscriptionRequestPipelineContext(helper, umsubscriber, caller, waveFile, null, null, callAnswering, codec, imp, subject, messageLength))
				{
					partnerTranscriptionRequestPipelineContext.SaveMessage();
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, data, "Successfully saved PartnerTranscriptionRequestPipelineContext for  _EmailAddress, object GUID = {0}", new object[]
					{
						recipient.ADRecipient.Guid
					});
					return;
				}
			}
			using (VoiceMessagePipelineContext voiceMessagePipelineContext = new VoiceMessagePipelineContext(codec, helper, waveFile, messageLength, imp, subject, callAnswering, privacyMarked, recipient))
			{
				voiceMessagePipelineContext.TranscriptionData = partnerTranscription;
				voiceMessagePipelineContext.SaveMessage();
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, data, "Successfully saved voice mail message for  _EmailAddress, object GUID = {0}", new object[]
				{
					recipient.ADRecipient.Guid
				});
			}
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x00060FC8 File Offset: 0x0005F1C8
		internal static void SubmitMissedCall(string callId, PhoneNumber callerId, UMSubscriber caller, UMSubscriber recipient, bool important, string subject, string callerIdDisplayName, Guid tenantGuid)
		{
			UMMessageSubmission.SubmitMissedCall(callId, callerId, caller, recipient, important, subject, null, ExDateTime.MinValue, callerIdDisplayName, tenantGuid);
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x00060FEC File Offset: 0x0005F1EC
		internal static void SubmitMissedCall(string callId, PhoneNumber callerId, UMSubscriber caller, UMSubscriber recipient, bool important, string subject, string messageID, ExDateTime sentTime, string callerIdDisplayName, Guid tenantGuid)
		{
			SubmissionHelper helper = new SubmissionHelper(callId, callerId, recipient.ADRecipient.Guid, recipient.DisplayName, recipient.MessageSubmissionCulture.Name, (caller != null) ? caller.MailAddress : null, (caller != null) ? caller.DisplayName : null, callerIdDisplayName, tenantGuid);
			using (MissedCallPipelineContext missedCallPipelineContext = new MissedCallPipelineContext(helper, important, subject, recipient, messageID, sentTime))
			{
				missedCallPipelineContext.SaveMessage();
				PIIMessage data = PIIMessage.Create(PIIType._EmailAddress, recipient.MailAddress);
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, data, "Succesfully saved missed call message for  _EmailAddress", new object[0]);
			}
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00061098 File Offset: 0x0005F298
		internal static void SubmitIncomingCallLog(string callId, PhoneNumber callerId, UMSubscriber recipient, Guid tenantGuid)
		{
			SubmissionHelper helper = new SubmissionHelper(callId, callerId, recipient.ADRecipient.Guid, recipient.DisplayName, recipient.MessageSubmissionCulture.Name, null, null, tenantGuid);
			using (IncomingCallLogPipelineContext incomingCallLogPipelineContext = new IncomingCallLogPipelineContext(helper, recipient))
			{
				incomingCallLogPipelineContext.SaveMessage();
				PIIMessage data = PIIMessage.Create(PIIType._EmailAddress, recipient.MailAddress);
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, data, "Succesfully saved call log message for  _EmailAddress", new object[0]);
			}
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00061120 File Offset: 0x0005F320
		internal static void SubmitOutgoingCallLog(string callId, PhoneNumber targetPhone, ContactInfo targetContact, UMSubscriber recipient, Guid tenantGuid)
		{
			SubmissionHelper helper = new SubmissionHelper(callId, targetPhone, recipient.ADRecipient.Guid, recipient.DisplayName, recipient.MessageSubmissionCulture.Name, null, null, tenantGuid);
			using (OutgoingCallLogPipelineContext outgoingCallLogPipelineContext = new OutgoingCallLogPipelineContext(helper, targetContact, recipient))
			{
				outgoingCallLogPipelineContext.SaveMessage();
				PIIMessage data = PIIMessage.Create(PIIType._EmailAddress, recipient.MailAddress);
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, data, "Succesfully saved call log message for  _EmailAddress", new object[0]);
			}
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x000611AC File Offset: 0x0005F3AC
		internal static void SubmitFax(string callId, PhoneNumber callerId, UMSubscriber caller, UMSubscriber recipient, uint numPages, string tifFile, bool iscomplete, string messageID, ExDateTime sentTime, string callerIdDisplayName, Guid tenantGuid)
		{
			SubmissionHelper helper = new SubmissionHelper(callId, callerId, recipient.ADRecipient.Guid, recipient.DisplayName, recipient.MessageSubmissionCulture.Name, (caller != null) ? caller.MailAddress : null, (caller != null) ? caller.DisplayName : null, callerIdDisplayName, tenantGuid);
			using (FaxPipelineContext faxPipelineContext = new FaxPipelineContext(helper, tifFile, numPages, !iscomplete, recipient, messageID, sentTime))
			{
				faxPipelineContext.SaveMessage();
				PIIMessage data = PIIMessage.Create(PIIType._EmailAddress, recipient.MailAddress);
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, data, "Succesfully saved fax message for  _EmailAddress", new object[0]);
			}
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x0006125C File Offset: 0x0005F45C
		internal static void SubmitOCSMessage(string xmlData)
		{
			using (OCSPipelineContext ocspipelineContext = new OCSPipelineContext(xmlData))
			{
				ocspipelineContext.SaveMessage();
			}
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x00061294 File Offset: 0x0005F494
		internal static void SubmitXSOVoiceMail(string callId, PhoneNumber callerId, UMSubscriber caller, string waveFilePath, int duration, AudioCodecEnum codec, string attachmentName, CultureInfo cultureInfo, MessageItem message, ITranscriptionData partnerTranscription, string callerIdDisplayName, Guid tenantGuid)
		{
			SubmissionHelper helper = new SubmissionHelper(callId, callerId, Guid.Empty, null, cultureInfo.Name, caller.MailAddress, caller.DisplayName, callerIdDisplayName, tenantGuid);
			TranscriptionEnabledSetting transcriptionEnabledInMailboxConfig = caller.IsTranscriptionEnabledInMailboxConfig(VoiceMailTypeEnum.SentVoiceMails);
			bool flag = UMMessageSubmission.ShouldSendToPartnerForTranscription(caller, false, duration, message.Sensitivity == Sensitivity.Private, transcriptionEnabledInMailboxConfig, ref partnerTranscription);
			if (flag)
			{
				using (PartnerTranscriptionRequestPipelineContext partnerTranscriptionRequestPipelineContext = new PartnerTranscriptionRequestPipelineContext(helper, caller, caller, waveFilePath, message, attachmentName, false, codec, false, null, duration))
				{
					partnerTranscriptionRequestPipelineContext.SaveMessage();
					goto IL_9E;
				}
			}
			using (XSOVoiceMessagePipelineContext xsovoiceMessagePipelineContext = new XSOVoiceMessagePipelineContext(helper, waveFilePath, duration, codec, attachmentName, message, caller))
			{
				xsovoiceMessagePipelineContext.TranscriptionData = partnerTranscription;
				xsovoiceMessagePipelineContext.SaveMessage();
			}
			IL_9E:
			PIIMessage data = PIIMessage.Create(PIIType._EmailAddress, caller.MailAddress);
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, data, "Succesfully saved XSO voice message with caller _EmailAddress", new object[0]);
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x00061388 File Offset: 0x0005F588
		public static void SubmitCDREmail(CDRData cdrData)
		{
			using (CDRPipelineContext cdrpipelineContext = new CDRPipelineContext(cdrData))
			{
				cdrpipelineContext.SaveMessage();
			}
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x000613C0 File Offset: 0x0005F5C0
		private static bool ShouldSendToPartnerForTranscription(UMSubscriber subscriber, bool callAnswering, int durationInSeconds, bool messageMarkedPrivate, TranscriptionEnabledSetting transcriptionEnabledInMailboxConfig, ref ITranscriptionData partnerTranscription)
		{
			if (subscriber == null)
			{
				return false;
			}
			if (durationInSeconds <= 0)
			{
				return false;
			}
			if (partnerTranscription != null)
			{
				return false;
			}
			bool flag = true;
			TranscriptionEnabledSetting transcriptionEnabledSetting = UMSubscriber.IsPartnerTranscriptionEnabled(subscriber.UMMailboxPolicy, transcriptionEnabledInMailboxConfig);
			PIIMessage data = PIIMessage.Create(PIIType._User, subscriber);
			switch (transcriptionEnabledSetting)
			{
			case TranscriptionEnabledSetting.Disabled:
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, data, "Partner transcription for user _User is not enabled.", new object[0]);
				flag = false;
				break;
			case TranscriptionEnabledSetting.Enabled:
				if (messageMarkedPrivate || subscriber.ShouldMessageBeProtected(callAnswering, messageMarkedPrivate))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, data, "Partner transcription for user _User is enabled but it is protected voicemail. ", new object[0]);
					partnerTranscription = new PartnerTranscriptionData(VoiceMailPreviewSchema.InternalXml.ProtectedVoiceMailTranscription);
					flag = false;
				}
				else if (durationInSeconds > subscriber.UMMailboxPolicy.VoiceMailPreviewPartnerMaxMessageDuration)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, data, "Partner transcription for user _User is enabled but the message is too long. ", new object[0]);
					partnerTranscription = new PartnerTranscriptionData(VoiceMailPreviewSchema.InternalXml.MessageTooLongTranscription);
					flag = false;
				}
				break;
			case TranscriptionEnabledSetting.Unknown:
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, data, "Unable to retrieve transcription enabled setting for mailbox _User, message will not be sent to partner", new object[0]);
				partnerTranscription = new PartnerTranscriptionData(VoiceMailPreviewSchema.InternalXml.ErrorReadingSettingsTranscription);
				flag = false;
				break;
			default:
				ExAssert.RetailAssert(false, "Invalid value for partnerTranscriptionEnabled = {0}", new object[]
				{
					transcriptionEnabledSetting.ToString()
				});
				break;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, data, "ShouldSendToPartnerForTranscription(user=_User): result = {0}", new object[]
			{
				flag
			});
			return flag;
		}
	}
}
