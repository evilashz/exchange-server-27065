using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Security.AntiXss;
using System.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon.MessageContent;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000A9 RID: 169
	internal class HtmlContentBuilder : MessageContentBuilder
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x0001759C File Offset: 0x0001579C
		private string TdStyle
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "font-family: {0}; color: #000000; border-width: 0in; font-size:10pt; vertical-align: top; padding-left: 10px; padding-right: 10px;", new object[]
				{
					Strings.Font1.ToString(base.Culture)
				});
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x000175D6 File Offset: 0x000157D6
		private string IndentedStyle
		{
			get
			{
				return "margin-left: 12px;";
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x000175E0 File Offset: 0x000157E0
		private string NoTranscriptionStyle
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "font-family: {0}; font-size: 10pt; color: #000066; font-weight: bold;", new object[]
				{
					Strings.Font2.ToString(base.Culture)
				});
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0001761C File Offset: 0x0001581C
		private string NoTranscriptionDetailsStyle
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "font-family: {0}; font-size: 10pt; color: #3b3b3b;", new object[]
				{
					Strings.Font2.ToString(base.Culture)
				});
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00017658 File Offset: 0x00015858
		private string DefaultStyle
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "font-family: {0}; background-color: #ffffff; color: #000000; font-size:10pt;", new object[]
				{
					Strings.Font1.ToString(base.Culture)
				});
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00017694 File Offset: 0x00015894
		private string CallInfoHeaderStyle
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "font-family: {0}; color: #686a6b; font-size:10pt;border-width: 0in;", new object[]
				{
					Strings.Font1.ToString(base.Culture)
				});
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x000176D0 File Offset: 0x000158D0
		private string TitleStyle
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "font-family: {0}; color: #000066; margin: 0in; font-size: 10pt; font-weight:bold; ", new object[]
				{
					Strings.Font2.ToString(base.Culture)
				});
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0001770C File Offset: 0x0001590C
		internal string MessageHeaderStyle
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "font-family: {0}; font-size: 10pt; color:#000066; font-weight: bold;", new object[]
				{
					Strings.Font2.ToString(base.Culture)
				});
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00017746 File Offset: 0x00015946
		internal string DividerMarginStyle
		{
			get
			{
				return "margin-bottom:10px";
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001774D File Offset: 0x0001594D
		internal HtmlContentBuilder(CultureInfo c, UMDialPlan rcptDialPlan) : base(c, rcptDialPlan)
		{
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00017757 File Offset: 0x00015957
		internal HtmlContentBuilder(CultureInfo c) : this(c, null)
		{
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00017761 File Offset: 0x00015961
		protected override string EmailHeaderLine
		{
			get
			{
				return "<hr/>";
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00017768 File Offset: 0x00015968
		protected override string CalendarHeaderLine
		{
			get
			{
				return "<hr/>";
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001776F File Offset: 0x0001596F
		internal override void AddVoicemailBody(PhoneNumber callerId, ContactInfo resolvedCallerInfo, string additionalText, ITranscriptionData transcriptionData)
		{
			this.AddVoicemailBody(callerId, true, resolvedCallerInfo, additionalText, transcriptionData);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001777D File Offset: 0x0001597D
		internal override void AddVoicemailBody(ContactInfo resolvedCallerInfo, string additionalText)
		{
			this.AddVoicemailBody(PhoneNumber.Empty, false, resolvedCallerInfo, additionalText, null);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00017790 File Offset: 0x00015990
		internal override void AddNDRBodyForCADRM(PhoneNumber callerId, ContactInfo resolvedCallerInfo, ExDateTime sentTime)
		{
			LocalizedString message;
			if (!string.IsNullOrEmpty(resolvedCallerInfo.DisplayName))
			{
				message = Strings.CallAnsweringNDRForDRMCallerResolved(resolvedCallerInfo.DisplayName);
			}
			else
			{
				message = Strings.CallAnsweringNDRForDRMCallerUnResolved(MessageContentBuilder.FormatCallerId(callerId, base.Culture));
			}
			this.AddUMMessageBody(callerId, true, resolvedCallerInfo, message, Strings.CallAnsweringNDRForDRMFooter.ToString(base.Culture), null, HtmlContentBuilder.TypeOfUMMessage.CallAnsweringNDR, sentTime.ToString("f", base.Culture), null);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000177FC File Offset: 0x000159FC
		internal override void AddNDRBodyForInterpersonalDRM(UMSubscriber caller, RecipientDetails recipients, ExDateTime sentTime)
		{
			List<string> list = new List<string>();
			if (recipients.Count > 1)
			{
				foreach (Participant participant in recipients.Participants)
				{
					list.Add(string.Concat(new string[]
					{
						participant.DisplayName,
						"(<a style=\"color: #3399ff; \" href=\"mailto:",
						participant.EmailAddress,
						"\">",
						participant.EmailAddress,
						"</a>)"
					}));
				}
				this.AddNDRBodyForInterpersonalDRM(list, sentTime);
				return;
			}
			if (recipients.IsDistributionList)
			{
				list.Add(string.Concat(new string[]
				{
					recipients.Participants[0].DisplayName,
					"(<a style=\"color: #3399ff; \" href=\"mailto:",
					recipients.Participants[0].EmailAddress,
					"\">",
					recipients.Participants[0].EmailAddress,
					"</a>)"
				}));
				this.AddNDRBodyForInterpersonalDRM(list, sentTime);
				return;
			}
			if (recipients.IsPersonalDistributionList)
			{
				list.Add(recipients.Participants[0].DisplayName);
				this.AddNDRBodyForInterpersonalDRM(list, sentTime);
				return;
			}
			ContactInfo resolvedRecipientInfo = ContactInfo.FindByParticipant(caller, recipients.Participants[0]);
			this.AddNDRBodyForInterpersonalDRM(resolvedRecipientInfo, sentTime);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00017974 File Offset: 0x00015B74
		internal override void AddFaxBody(PhoneNumber callerId, ContactInfo resolvedCallerInfo, string additionalText)
		{
			LocalizedString faxBodyDisplayLabel = resolvedCallerInfo.GetFaxBodyDisplayLabel(callerId, base.Culture);
			this.AddUMMessageBody(callerId, resolvedCallerInfo, faxBodyDisplayLabel, additionalText);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001799C File Offset: 0x00015B9C
		internal override void AddMissedCallBody(PhoneNumber callerId, ContactInfo resolvedCallerInfo)
		{
			LocalizedString missedCallBodyDisplayLabel = resolvedCallerInfo.GetMissedCallBodyDisplayLabel(callerId, base.Culture);
			this.AddUMMessageBody(callerId, resolvedCallerInfo, missedCallBodyDisplayLabel, null);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000179C4 File Offset: 0x00015BC4
		internal override void AddTeamPickUpBody(string answeredBy, PhoneNumber callerId, ContactInfo callerInfo)
		{
			LocalizedString message = Strings.TeamPickUpBody(this.GetDisplayNameOrCallerId(callerInfo, callerId), answeredBy);
			this.AddUMMessageBody(callerId, callerInfo, message, null);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000179EC File Offset: 0x00015BEC
		internal override void AddCallNotForwardedBody(string target, PhoneNumber callerId, ContactInfo callerInfo)
		{
			LocalizedString message = Strings.CallNotForwardedBody(this.GetDisplayNameOrCallerId(callerInfo, callerId), target);
			this.AddUMMessageBody(callerId, callerInfo, message, Strings.CallNotForwardedText);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00017A18 File Offset: 0x00015C18
		internal override void AddCallForwardedBody(string target, PhoneNumber callerId, ContactInfo callerInfo)
		{
			LocalizedString message = Strings.CallForwardedBody(this.GetDisplayNameOrCallerId(callerInfo, callerId), target);
			this.AddUMMessageBody(callerId, callerInfo, message, null);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00017A40 File Offset: 0x00015C40
		internal override void AddIncomingCallLogBody(PhoneNumber callerId, ContactInfo callerInfo)
		{
			string displayName = this.GetDisplayName(callerInfo);
			LocalizedString message;
			if (!string.IsNullOrEmpty(displayName))
			{
				message = (callerInfo.ResolvesToMultipleContacts ? Strings.IncomingCallLogBodyCallerMultipleResolved(displayName) : Strings.IncomingCallLogBodyCallerResolved(displayName, MessageContentBuilder.FormatCallerId(callerId, base.Culture)));
			}
			else
			{
				message = Strings.IncomingCallLogBodyCallerUnresolved(MessageContentBuilder.FormatCallerId(callerId, base.Culture));
			}
			this.AddUMMessageBody(callerId, callerInfo, message, null);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00017AA0 File Offset: 0x00015CA0
		internal override void AddOutgoingCallLogBody(PhoneNumber targetPhone, ContactInfo calledPartyInfo)
		{
			string displayName = this.GetDisplayName(calledPartyInfo);
			LocalizedString message;
			if (!string.IsNullOrEmpty(displayName))
			{
				message = (calledPartyInfo.ResolvesToMultipleContacts ? Strings.OutgoingCallLogBodyTargetMultipleResolved(displayName) : Strings.OutgoingCallLogBodyTargetResolved(displayName, MessageContentBuilder.FormatCallerId(targetPhone, base.Culture)));
			}
			else
			{
				message = Strings.OutgoingCallLogBodyTargetUnresolved(MessageContentBuilder.FormatCallerId(targetPhone, base.Culture));
			}
			this.AddUMMessageBody(targetPhone, calledPartyInfo, message, null);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00017AFE File Offset: 0x00015CFE
		internal override void AddEnterpriseNotifyMailBody(LocalizedString messageHeader, string[] accessNumbers, string extension, string pin, string additionalText)
		{
			this.AddNotifyMailBody(messageHeader, Strings.AccessMailText, accessNumbers, extension, pin, additionalText);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00017B12 File Offset: 0x00015D12
		internal override void AddConsumerNotifyMailBody(LocalizedString messageHeader, string[] accessNumbers, string extension, string pin, string additionalText)
		{
			this.AddNotifyMailBody(messageHeader, Strings.AccessMailTextConsumer, accessNumbers, extension, pin, additionalText);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00017B26 File Offset: 0x00015D26
		internal override void AddLateForMeetingBody(CalendarItemBase cal, ExTimeZone timeZone, LocalizedString lateInfo)
		{
			this.AddUMMessageBodyPrefix(lateInfo);
			this.AddNewLine();
			this.AddCalendarHeader(cal, timeZone, false);
			this.AddDocumentEnd();
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00017B44 File Offset: 0x00015D44
		internal override void AddRecordedReplyText(string displayName)
		{
			this.AddRecordedMessageText(Strings.ReplyWithRecording(displayName));
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00017B52 File Offset: 0x00015D52
		internal override void AddRecordedForwardText(string displayName)
		{
			this.AddRecordedMessageText(Strings.ForwardWithRecording(displayName));
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00017B68 File Offset: 0x00015D68
		internal override void AddAudioPreview(ITranscriptionData transcriptionData)
		{
			this.AddAudioPreviewHelper(transcriptionData, delegate
			{
				this.AddNewLine();
			});
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00017B80 File Offset: 0x00015D80
		protected override string FormatHeaderName(LocalizedString headerName)
		{
			return string.Format(CultureInfo.InvariantCulture, "<b><font size=2 face={0}><span style='font-size:10.0pt;font-family:{0};font-weight:bold'>{1}</span></font></b>", new object[]
			{
				Strings.Font1.ToString(base.Culture),
				AntiXssEncoder.HtmlEncode(headerName.ToString(base.Culture), false)
			});
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00017BD0 File Offset: 0x00015DD0
		protected override string FormatHeaderValue(string headerValue)
		{
			return string.Format(CultureInfo.InvariantCulture, "<font size=2 face={0}><span style='font-size:10.0pt;font-family:{0}'>{1}</span></font>", new object[]
			{
				Strings.Font1.ToString(base.Culture),
				AntiXssEncoder.HtmlEncode(headerValue, false)
			});
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00017C14 File Offset: 0x00015E14
		protected override void AddNewLine()
		{
			base.AppendCultureInvariantText("<br/>");
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00017C21 File Offset: 0x00015E21
		protected override void AddDivider()
		{
			base.AppendCultureInvariantText("<hr/>");
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00017C2E File Offset: 0x00015E2E
		protected override void AddDocumentEnd()
		{
			base.AppendCultureInvariantText("</div></BODY></HTML>");
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00017C3B File Offset: 0x00015E3B
		protected override void AddStart()
		{
			if (base.Culture.TextInfo.IsRightToLeft)
			{
				base.AppendCultureInvariantText("<HTML dir=\"rtl\">");
				return;
			}
			base.AppendCultureInvariantText("<HTML>");
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00017C68 File Offset: 0x00015E68
		private void AddNotifyMailBody(LocalizedString messageHeader, LocalizedString accessMailText, string[] accessNumbers, string extension, string pin, string additionalText)
		{
			this.AddUMMessageBodyPrefix(messageHeader);
			this.AddText(accessMailText);
			this.AddNewLine();
			this.AddNewLine();
			this.AddAccessInformation(accessNumbers, extension, pin);
			this.AddVoicemailSettingsDiscoverability();
			if (additionalText.Length > 0)
			{
				this.AddNewLine();
				base.AppendCultureInvariantText(additionalText);
				this.AddNewLine();
			}
			this.AddUMMessageBodySuffix();
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00017CC4 File Offset: 0x00015EC4
		private void AddVoicemailSettingsDiscoverability()
		{
			this.AddNewLine();
			this.AddNewLine();
			this.AddText(Strings.ConfigureVoicemailSettings);
			base.AppendFormat("<ul dir=\"{0}\">", new object[]
			{
				base.Culture.TextInfo.IsRightToLeft ? "rtl" : "ltr"
			});
			this.AddListItem(Strings.VoicemailSettingsInstruction1);
			this.AddListItem(Strings.VoicemailSettingsInstruction2);
			this.AddListItem(Strings.VoicemailSettingsInstruction3);
			base.AppendCultureInvariantText("</ul>");
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00017D48 File Offset: 0x00015F48
		private void AddAudioPreviewHelper(ITranscriptionData transcriptionData, Action callerInfoDelegate)
		{
			if (transcriptionData == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "transcriptionData == null. Likely means that User/policy/dialplan must have not been enabled for transcription.", new object[0]);
				if (callerInfoDelegate != null)
				{
					callerInfoDelegate();
				}
				return;
			}
			if (transcriptionData.RecognitionError == RecoErrorType.LanguageNotSupported)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "Language is not supported for transcription", new object[0]);
				if (callerInfoDelegate != null)
				{
					callerInfoDelegate();
				}
				return;
			}
			if (transcriptionData.RecognitionResult != RecoResultType.Skipped)
			{
				this.AddTranscriptionDetailMessage(transcriptionData);
				if (callerInfoDelegate != null)
				{
					this.AddDividerMargin();
					callerInfoDelegate();
				}
				return;
			}
			if (callerInfoDelegate != null)
			{
				callerInfoDelegate();
				this.AddDividerMargin();
				this.AddTranscriptionSkippedMessage(transcriptionData);
				return;
			}
			this.AddTranscriptionSkippedMessage(transcriptionData);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00017DE0 File Offset: 0x00015FE0
		private void AddTranscriptionDetailMessage(ITranscriptionData transcriptionData)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "Transcription was successful. Adding audio preview to voicemail message body.", new object[0]);
			base.AppendFormat("<span id=\"UM-ASR-text\" lang=\"{0}\" style=\"{1}\">", new object[]
			{
				base.Culture.TwoLetterISOLanguageName,
				this.DefaultStyle
			});
			base.AppendFormat("<span lang=\"{0}\" dir=\"{1}\">", new object[]
			{
				transcriptionData.Language.TwoLetterISOLanguageName,
				transcriptionData.Language.TextInfo.IsRightToLeft ? "rtl" : "ltr"
			});
			EvmTranscriptWriter evmTranscriptWriter = HtmlContentBuilder.HtmlEvmTranscriptWriter.Create(this, transcriptionData.Language);
			XmlElement documentElement = transcriptionData.TranscriptionXml.DocumentElement;
			evmTranscriptWriter.WriteTranscript(documentElement);
			base.AppendCultureInvariantText("</span>");
			base.AppendCultureInvariantText("</span>");
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00017EA8 File Offset: 0x000160A8
		private void AddTranscriptionSkippedMessage(ITranscriptionData transcriptionData)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "Skipped is set on the ASR data. RecognitionError:{0} ErrorInfo:{1}.", new object[]
			{
				transcriptionData.RecognitionError,
				transcriptionData.ErrorInformation
			});
			this.AddMessageHeader(Strings.NoTranscription);
			LocalizedString localizedText;
			switch (transcriptionData.RecognitionError)
			{
			case RecoErrorType.AudioQualityPoor:
				localizedText = Strings.TranscriptionSkippedDueToPoorAudioQualityDetails;
				goto IL_C7;
			case RecoErrorType.Rejected:
				localizedText = Strings.TranscriptionSkippedDueToRejectionDetails;
				goto IL_C7;
			case RecoErrorType.BadRequest:
				localizedText = Strings.TranscriptionSkippedDueToBadRequestDetails;
				goto IL_C7;
			case RecoErrorType.SystemError:
				localizedText = Strings.TranscriptionSkippedDueToSystemErrorDetails;
				goto IL_C7;
			case RecoErrorType.Timeout:
				localizedText = Strings.TranscriptionSkippedDueToTimeoutDetails;
				goto IL_C7;
			case RecoErrorType.MessageTooLong:
				localizedText = Strings.TranscriptionSkippedDueToLongMessageDetails;
				goto IL_C7;
			case RecoErrorType.ProtectedVoiceMail:
				localizedText = Strings.TranscriptionSkippedDueToProtectedVoiceMail;
				goto IL_C7;
			case RecoErrorType.Throttled:
				localizedText = Strings.TranscriptionSkippedDueToThrottlingDetails;
				goto IL_C7;
			case RecoErrorType.ErrorReadingSettings:
				localizedText = Strings.TranscriptionSkippedDueToErrorReadingSettings;
				goto IL_C7;
			}
			localizedText = Strings.TranscriptionSkippedDefaultDetails;
			IL_C7:
			base.AppendFormat("<div style=\"{0}\">", new object[]
			{
				this.IndentedStyle + this.DefaultStyle
			});
			this.AddTextSpan(localizedText, this.NoTranscriptionDetailsStyle);
			EvmTranscriptWriter evmTranscriptWriter = HtmlContentBuilder.HtmlEvmTranscriptWriter.Create(this, transcriptionData.Language);
			XmlElement documentElement = transcriptionData.TranscriptionXml.DocumentElement;
			evmTranscriptWriter.WriteErrorInformationOnly(documentElement);
			base.AppendCultureInvariantText("</div>");
			this.AddNewLine();
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00017FE4 File Offset: 0x000161E4
		private void AddDividerMargin()
		{
			base.AppendFormat("<div style=\"{0}\">", new object[]
			{
				this.DividerMarginStyle
			});
			this.AddDivider();
			base.AppendCultureInvariantText("</div>");
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0001801E File Offset: 0x0001621E
		private void AddMessageHeader(LocalizedString messageHeader)
		{
			this.AddMessageHeader(messageHeader, true);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00018028 File Offset: 0x00016228
		private void AddMessageHeader(LocalizedString messageHeader, bool escape)
		{
			base.AppendFormat("<div style=\"{0}\">", new object[]
			{
				this.MessageHeaderStyle
			});
			string message;
			if (escape)
			{
				message = AntiXssEncoder.HtmlEncode(messageHeader.ToString(base.Culture), false);
			}
			else
			{
				message = messageHeader.ToString(base.Culture);
			}
			base.AppendCultureInvariantText(message);
			base.AppendFormat("</div><br/>", new object[0]);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00018094 File Offset: 0x00016294
		private string RenderStyle(string styleDefinition, LocalizedString fontName)
		{
			return string.Format(CultureInfo.InvariantCulture, styleDefinition, new object[]
			{
				fontName.ToString(base.Culture)
			});
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000180C4 File Offset: 0x000162C4
		private void AddRecordedMessageText(LocalizedString text)
		{
			base.AppendFormat("<div style=\"{0}\">", new object[]
			{
				this.TitleStyle
			});
			this.AddText(text);
			base.AppendCultureInvariantText("</div><br/>");
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00018100 File Offset: 0x00016300
		private void AddVoicemailBody(PhoneNumber callerId, bool addCallerId, ContactInfo resolvedCallerInfo, string additionalText, ITranscriptionData transcriptionData)
		{
			LocalizedString voicemailBodyDisplayLabel = resolvedCallerInfo.GetVoicemailBodyDisplayLabel(callerId, base.Culture);
			this.AddUMMessageBody(callerId, addCallerId, resolvedCallerInfo, voicemailBodyDisplayLabel, additionalText, transcriptionData, HtmlContentBuilder.TypeOfUMMessage.NormalMessage, null, null);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001812C File Offset: 0x0001632C
		private void AddNDRBodyForInterpersonalDRM(ContactInfo resolvedRecipientInfo, ExDateTime sentTime)
		{
			this.AddUMMessageBody(PhoneNumber.Empty, false, resolvedRecipientInfo, Strings.InterpersonalNDRForDRM, Strings.InterpersonalNDRForDRMFooter.ToString(base.Culture), null, HtmlContentBuilder.TypeOfUMMessage.InterpersonalNDR, sentTime.ToString("f", base.Culture), null);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00018174 File Offset: 0x00016374
		private void AddNDRBodyForInterpersonalDRM(List<string> originalRecipients, ExDateTime sentTime)
		{
			this.AddUMMessageBody(PhoneNumber.Empty, false, null, Strings.InterpersonalNDRForDRM, Strings.InterpersonalNDRForDRMFooter.ToString(base.Culture), null, HtmlContentBuilder.TypeOfUMMessage.InterpersonalNDR, sentTime.ToString("f", base.Culture), originalRecipients);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000181BC File Offset: 0x000163BC
		private void AddUMMessageBody(PhoneNumber callerId, ContactInfo resolvedCaller, LocalizedString message, LocalizedString additionalText)
		{
			this.AddUMMessageBody(callerId, true, resolvedCaller, message, additionalText.ToString(base.Culture), null, HtmlContentBuilder.TypeOfUMMessage.NormalMessage, null, null);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000181E4 File Offset: 0x000163E4
		private void AddUMMessageBody(PhoneNumber callerId, ContactInfo resolvedCaller, LocalizedString message, string additionalText)
		{
			this.AddUMMessageBody(callerId, true, resolvedCaller, message, additionalText, null, HtmlContentBuilder.TypeOfUMMessage.NormalMessage, null, null);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001827C File Offset: 0x0001647C
		private void AddUMMessageBody(PhoneNumber callerId, bool addCallerId, ContactInfo resolvedContact, LocalizedString message, string additionalText, ITranscriptionData transcriptionData, HtmlContentBuilder.TypeOfUMMessage messageType, string sentTime, List<string> originalListOfRecipients)
		{
			Action callerInfoDelegate;
			if (resolvedContact != null)
			{
				callerInfoDelegate = delegate()
				{
					this.AddCallerHeaderInfo(message);
					this.AddCallerInformation(callerId, addCallerId, resolvedContact, messageType, sentTime);
					this.AddCallerHeaderInfoEnd();
				};
			}
			else
			{
				callerInfoDelegate = delegate()
				{
					this.AddCallerHeaderInfo(message);
					this.AddCallerHeaderInfoEnd();
				};
			}
			this.AddUMMessageBodyPrefix(transcriptionData, callerInfoDelegate);
			if (messageType == HtmlContentBuilder.TypeOfUMMessage.InterpersonalNDR && originalListOfRecipients != null)
			{
				this.AddInterpersonalSpecialNDR(originalListOfRecipients, sentTime);
			}
			if (!string.IsNullOrEmpty(additionalText))
			{
				this.AddNewLine();
				base.AppendCultureInvariantText(additionalText);
				this.AddNewLine();
			}
			this.AddUMMessageBodySuffix();
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00018340 File Offset: 0x00016540
		private void AddCallerHeaderInfo(LocalizedString message)
		{
			base.AppendFormat("<div id=\"UM-call-info\" lang=\"{0}\">", new object[]
			{
				base.Culture.TwoLetterISOLanguageName
			});
			this.AddMessageHeader(message, false);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00018376 File Offset: 0x00016576
		private void AddCallerHeaderInfoEnd()
		{
			base.AppendCultureInvariantText("</div>");
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00018384 File Offset: 0x00016584
		private void AddInterpersonalSpecialNDR(List<string> originalListOfRecipients, string sentTime)
		{
			base.AppendCultureInvariantText("<table border=\"0\" width=\"100%\">");
			if (originalListOfRecipients.Count == 1)
			{
				this.AddTableEntry(Strings.Recipient, originalListOfRecipients[0]);
			}
			else
			{
				this.AddTableEntry(Strings.Recipients, originalListOfRecipients[0]);
				for (int i = 1; i < originalListOfRecipients.Count; i++)
				{
					this.AddTableEntry(LocalizedString.Empty, originalListOfRecipients[i]);
				}
			}
			this.AddTableEntry(Strings.Sent, sentTime);
			base.AppendCultureInvariantText("</table>");
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001842C File Offset: 0x0001662C
		private void AddUMMessageBodyPrefix(LocalizedString message)
		{
			this.AddUMMessageBodyPrefix(null, delegate()
			{
				this.AddCallerHeaderInfo(message);
				this.AddCallerHeaderInfoEnd();
			});
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00018460 File Offset: 0x00016660
		private void AddStylesheet()
		{
			base.AppendCultureInvariantText("<style type=\"text/css\"> a:link { color: #3399ff; } a:visited { color: #3366cc; } a:active { color: #ff9900; } </style>");
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00018470 File Offset: 0x00016670
		private void AddUMMessageBodyPrefix(ITranscriptionData transcriptionData, Action callerInfoDelegate)
		{
			this.AddStart();
			base.AppendCultureInvariantText("<head>");
			this.AddStylesheet();
			base.AppendCultureInvariantText("</head>");
			base.AppendCultureInvariantText("<body>");
			this.AddStylesheet();
			base.AppendFormat("<div style=\"{0}\">", new object[]
			{
				this.DefaultStyle
			});
			this.AddAudioPreviewHelper(transcriptionData, callerInfoDelegate);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x000184D4 File Offset: 0x000166D4
		private void AddUMMessageBodySuffix()
		{
			this.AddDocumentEnd();
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x000184DC File Offset: 0x000166DC
		private void AddTelOrSipUriEntry(LocalizedString label, string phoneOrSipUri)
		{
			PhoneNumber phoneNumber;
			if (PhoneNumber.TryParse(base.RecipientDialPlan, phoneOrSipUri, out phoneNumber))
			{
				this.AddTelOrSipUriEntry(label, phoneNumber);
				return;
			}
			this.AddTableEntry(Strings.CallerId, phoneOrSipUri);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00018510 File Offset: 0x00016710
		private void AddTelOrSipUriEntry(LocalizedString label, PhoneNumber phoneNumber)
		{
			string anchor = string.Empty;
			string display = string.Empty;
			if (phoneNumber.UriType == UMUriType.SipName)
			{
				string text = Utils.RemoveSIPPrefix(phoneNumber.ToDial);
				anchor = text;
				display = text;
			}
			else
			{
				anchor = AntiXssEncoder.HtmlEncode(phoneNumber.ToDial, false);
				display = AntiXssEncoder.HtmlEncode(phoneNumber.ToDisplay, false);
			}
			this.AddTableEntry(label, MessageContentBuilder.FormatTelOrSipEntryAsAnchor(anchor, display, phoneNumber.UriType));
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00018574 File Offset: 0x00016774
		private void AddCallerInformation(PhoneNumber callerId, bool addCallerId, ContactInfo contactInfo, HtmlContentBuilder.TypeOfUMMessage messageType, string sentTime)
		{
			base.AppendCultureInvariantText("<table border=\"0\" style=\"width:100%; table-layout:auto;\">");
			if (contactInfo != null)
			{
				if (addCallerId)
				{
					if (!callerId.IsEmpty)
					{
						this.AddTelOrSipUriEntry(Strings.CallerId, callerId);
					}
					else
					{
						this.AddTableEntry(Strings.CallerId, MessageContentBuilder.FormatCallerId(callerId, base.Culture));
					}
				}
				if (messageType == HtmlContentBuilder.TypeOfUMMessage.InterpersonalNDR && !string.IsNullOrEmpty(contactInfo.DisplayName))
				{
					this.AddTableEntry(Strings.Recipient, contactInfo.DisplayName);
				}
				if (!string.IsNullOrEmpty(contactInfo.Title))
				{
					this.AddTableEntry(Strings.JobTitle, contactInfo.Title);
				}
				if (!string.IsNullOrEmpty(contactInfo.Company))
				{
					this.AddTableEntry(Strings.Company, contactInfo.Company);
				}
				if (!string.IsNullOrEmpty(contactInfo.BusinessPhone))
				{
					this.AddTelOrSipUriEntry(Strings.WorkPhone, contactInfo.BusinessPhone);
				}
				if (!string.IsNullOrEmpty(contactInfo.MobilePhone))
				{
					this.AddTelOrSipUriEntry(Strings.MobilePhone, contactInfo.MobilePhone);
				}
				if (!string.IsNullOrEmpty(contactInfo.HomePhone))
				{
					this.AddTelOrSipUriEntry(Strings.HomePhone, contactInfo.HomePhone);
				}
				if (!string.IsNullOrEmpty(contactInfo.EMailAddress))
				{
					this.AddTableEntry(Strings.Email, string.Concat(new string[]
					{
						"<a style=\"color: #3399ff; \" href=\"mailto:",
						contactInfo.EMailAddress,
						"\">",
						contactInfo.EMailAddress,
						"</a>"
					}));
				}
				if (messageType == HtmlContentBuilder.TypeOfUMMessage.CallAnsweringNDR || messageType == HtmlContentBuilder.TypeOfUMMessage.InterpersonalNDR)
				{
					this.AddTableEntry(Strings.Sent, sentTime);
				}
			}
			base.AppendCultureInvariantText("</table>");
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000186EC File Offset: 0x000168EC
		private void AddTableEntry(LocalizedString name, string value)
		{
			base.AppendFormat("<tr><td width=\"15%\" nowrap style=\"{0}\">", new object[]
			{
				this.CallInfoHeaderStyle
			});
			if (!name.IsEmpty)
			{
				base.Append(name);
				base.AppendCultureInvariantText(":</td>");
			}
			else
			{
				base.AppendCultureInvariantText("</td>");
			}
			base.AppendFormat("<td width=\"85%\" style=\"{0}\">", new object[]
			{
				this.TdStyle
			});
			base.AppendCultureInvariantText(value);
			base.AppendCultureInvariantText("</td></tr>");
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001876C File Offset: 0x0001696C
		private void AddTableEntry(LocalizedString name, string[] values)
		{
			StringBuilder stringBuilder = new StringBuilder(values[0]);
			for (int i = 1; i < values.Length; i++)
			{
				stringBuilder.Append(" " + Strings.AccessNumberSeparator.ToString(base.Culture) + " " + values[i]);
			}
			this.AddTableEntry(name, stringBuilder.ToString());
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x000187C9 File Offset: 0x000169C9
		private void AddListItem(string value)
		{
			base.AppendCultureInvariantText("<li>");
			base.AppendCultureInvariantText(AntiXssEncoder.HtmlEncode(value, false));
			base.AppendCultureInvariantText("</li>");
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000187EE File Offset: 0x000169EE
		private void AddListItem(LocalizedString localizedValue)
		{
			this.AddListItem(localizedValue.ToString(base.Culture));
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00018804 File Offset: 0x00016A04
		private void AddAccessInformation(string[] accessNumbers, string extension, string pin)
		{
			base.AppendCultureInvariantText("<table border=\"0\" style=\"width:100%; table-layout:auto;\">");
			if (accessNumbers != null && accessNumbers.Length > 0)
			{
				this.AddTableEntry(Strings.AccessNumber, accessNumbers);
			}
			if (extension != null && extension.Length > 0)
			{
				this.AddTableEntry(Strings.Extension, extension);
			}
			if (pin != null && pin.Length > 0)
			{
				this.AddTableEntry(Strings.Pin, pin);
			}
			base.AppendCultureInvariantText("</table>");
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001886C File Offset: 0x00016A6C
		private void AddTextSpan(string text, string spanStyle)
		{
			this.AddTextSpan(text, spanStyle, true);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00018878 File Offset: 0x00016A78
		private void AddTextSpan(string text, string spanStyle, bool escape)
		{
			if (escape)
			{
				text = AntiXssEncoder.HtmlEncode(text, false);
			}
			text = text.Replace("  ", "&nbsp;&nbsp;");
			base.AppendFormat("<span style=\"{0}\">{1}</span>", new object[]
			{
				spanStyle,
				text
			});
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000188BE File Offset: 0x00016ABE
		private void AddTextSpan(LocalizedString localizedText, string spanStyle)
		{
			this.AddTextSpan(localizedText.ToString(base.Culture), spanStyle);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x000188D4 File Offset: 0x00016AD4
		private void AddText(string text)
		{
			base.AppendCultureInvariantText(AntiXssEncoder.HtmlEncode(text, false));
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000188E3 File Offset: 0x00016AE3
		private void AddText(LocalizedString localizedText)
		{
			this.AddText(localizedText.ToString(base.Culture));
		}

		// Token: 0x04000380 RID: 896
		public const string UMCallInfoDiv = "<div id=\"UM-call-info\" lang=\"{0}\">";

		// Token: 0x04000381 RID: 897
		public const string UMCallInfoDivEnd = "</div>";

		// Token: 0x04000382 RID: 898
		private const string TranscriptionTextSpan = "<span id=\"UM-ASR-text\" lang=\"{0}\" style=\"{1}\">";

		// Token: 0x04000383 RID: 899
		private const string TranscriptionTextSpanEnd = "</span>";

		// Token: 0x04000384 RID: 900
		private const string UMInfoTable = "<table border=\"0\" style=\"width:100%; table-layout:auto;\">";

		// Token: 0x04000385 RID: 901
		private const string UMInfoTableEnd = "</table>";

		// Token: 0x04000386 RID: 902
		private const string ListInfo = "<ul dir=\"{0}\">";

		// Token: 0x04000387 RID: 903
		private const string ListInfoEnd = "</ul>";

		// Token: 0x04000388 RID: 904
		private const string RightToLeft = "rtl";

		// Token: 0x04000389 RID: 905
		private const string LeftToRight = "ltr";

		// Token: 0x0400038A RID: 906
		private const string AnchorStyleSheet = "<style type=\"text/css\"> a:link { color: #3399ff; } a:visited { color: #3366cc; } a:active { color: #ff9900; } </style>";

		// Token: 0x020000AA RID: 170
		internal enum TypeOfUMMessage
		{
			// Token: 0x0400038C RID: 908
			NormalMessage,
			// Token: 0x0400038D RID: 909
			InterpersonalNDR,
			// Token: 0x0400038E RID: 910
			CallAnsweringNDR
		}

		// Token: 0x020000AB RID: 171
		private class HtmlEvmTranscriptWriter : EvmTranscriptWriter
		{
			// Token: 0x17000157 RID: 343
			// (get) Token: 0x06000635 RID: 1589 RVA: 0x000188F8 File Offset: 0x00016AF8
			private string TranscriptionInformationStyle
			{
				get
				{
					return string.Format(CultureInfo.InvariantCulture, "font-family: {0}; color: #686a6b; font-size:7.5pt; margin-top:12px;", new object[]
					{
						Strings.Font2.ToString(base.Language)
					});
				}
			}

			// Token: 0x06000636 RID: 1590 RVA: 0x00018932 File Offset: 0x00016B32
			private HtmlEvmTranscriptWriter(HtmlContentBuilder builder, CultureInfo language) : base(language)
			{
				this.builder = builder;
			}

			// Token: 0x06000637 RID: 1591 RVA: 0x00018942 File Offset: 0x00016B42
			public static EvmTranscriptWriter Create(HtmlContentBuilder builder, CultureInfo language)
			{
				return new HtmlContentBuilder.HtmlEvmTranscriptWriter(builder, language);
			}

			// Token: 0x06000638 RID: 1592 RVA: 0x0001894C File Offset: 0x00016B4C
			private void WriteText(string text)
			{
				string text2 = text.TrimEnd(new char[0]);
				this.lastTextEndsWithPunctuation = (text2.Length > 0 && char.IsPunctuation(text2[text2.Length - 1]));
				this.builder.AddText(text);
			}

			// Token: 0x06000639 RID: 1593 RVA: 0x00018998 File Offset: 0x00016B98
			protected override void WriteEndOfParagraph()
			{
				if (!this.lastTextEndsWithPunctuation)
				{
					this.WriteText(Strings.EndOfParagraphMarker.ToString(base.Language));
				}
			}

			// Token: 0x0600063A RID: 1594 RVA: 0x000189C8 File Offset: 0x00016BC8
			protected override void WriteInformation(XmlElement informationElement)
			{
				string text = informationElement.InnerText;
				if (string.IsNullOrEmpty(text))
				{
					return;
				}
				CultureInfo cultureInfo = null;
				try
				{
					cultureInfo = new CultureInfo(informationElement.Attributes["lang"].Value);
				}
				catch (Exception ex)
				{
					if (ex is ArgumentException || ex is ArgumentNullException)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "Cant determine the culture of information element. Caught an exception e {0}.", new object[]
						{
							ex.ToString()
						});
						return;
					}
					throw;
				}
				string text2 = (informationElement.Attributes["linkURL"] == null) ? null : informationElement.Attributes["linkURL"].Value;
				string text3 = (informationElement.Attributes["linkText"] == null) ? null : informationElement.Attributes["linkText"].Value;
				if (!string.IsNullOrEmpty(text2))
				{
					string val;
					if (string.IsNullOrEmpty(text3))
					{
						val = text2;
					}
					else
					{
						val = string.Concat(new string[]
						{
							"<a style=\"color: #3399ff; \" href=\"",
							text2,
							"\">",
							AntiXssEncoder.HtmlEncode(text3, false),
							"</a>"
						});
					}
					text = Strings.InformationTextWithLink(text, val).ToString(this.builder.Culture);
				}
				this.builder.AppendFormat("<div lang=\"{0}\" dir=\"{1}\" style=\"{2}\">{3}</div>", new object[]
				{
					cultureInfo.Name,
					cultureInfo.TextInfo.IsRightToLeft ? "rtl" : "ltr",
					this.TranscriptionInformationStyle,
					text
				});
			}

			// Token: 0x0600063B RID: 1595 RVA: 0x00018B6C File Offset: 0x00016D6C
			protected override void WritePhoneNumber(XmlElement element)
			{
				string str = element.Attributes["reference"].Value;
				StringBuilder stringBuilder = new StringBuilder(32);
				foreach (object obj in element.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					stringBuilder.Append(xmlNode.InnerText);
				}
				string text = stringBuilder.ToString();
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				while (num3 < text.Length && EvmTranscriptWriter.IgnoreLeadingOrTrailingCharInTelAnchor(text[num3++]))
				{
					num++;
				}
				num3 = text.Length - 1;
				while (num3 >= num && EvmTranscriptWriter.IgnoreLeadingOrTrailingCharInTelAnchor(text[num3--]))
				{
					num2++;
				}
				int num4 = text.Length - num - num2;
				string text2 = (num4 == text.Length) ? text : text.Substring(num, num4);
				string input = (num == 0) ? string.Empty : text.Substring(0, num);
				string input2 = (num2 == 0) ? string.Empty : text.Substring(text.Length - num2);
				PhoneNumber phoneNumber = null;
				if (PhoneNumber.TryParse(text2, out phoneNumber))
				{
					str = phoneNumber.ToDial;
				}
				string str2 = MessageContentBuilder.FormatTelOrSipEntryAsAnchor(HttpUtility.UrlEncode(str), AntiXssEncoder.HtmlEncode(text2, false), UMUriType.TelExtn);
				string message = AntiXssEncoder.HtmlEncode(input, false) + str2 + AntiXssEncoder.HtmlEncode(input2, false);
				this.builder.AppendCultureInvariantText(message);
			}

			// Token: 0x0600063C RID: 1596 RVA: 0x00018D00 File Offset: 0x00016F00
			protected override void WriteGenericFeature(XmlElement element)
			{
				foreach (object obj in element.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					this.WriteText(xmlNode.InnerText);
				}
			}

			// Token: 0x0600063D RID: 1597 RVA: 0x00018D60 File Offset: 0x00016F60
			protected override void WriteGenericTextElement(XmlElement element)
			{
				this.WriteText(element.InnerText);
			}

			// Token: 0x0600063E RID: 1598 RVA: 0x00018D70 File Offset: 0x00016F70
			protected override void WriteBreakElement(XmlElement element)
			{
				XmlAttribute xmlAttribute = element.Attributes["wt"];
				string a = (xmlAttribute == null) ? "low" : xmlAttribute.Value;
				if (string.Equals(a, "high", StringComparison.OrdinalIgnoreCase))
				{
					this.WriteEndOfParagraph();
					int num = SafeConvert.ToInt32(Strings.ParagraphEndNewLines.ToString(base.Language), 0, 6, 2);
					for (int i = 0; i < num; i++)
					{
						this.builder.AddNewLine();
					}
					return;
				}
				string text = Strings.EndOfSentenceMarker.ToString(base.Language);
				int num2 = SafeConvert.ToInt32(Strings.NumSpaceBeforeEOS.ToString(base.Language), 0, 3, 1);
				int num3 = SafeConvert.ToInt32(Strings.NumSpaceAfterEOS.ToString(base.Language), 0, 3, 1);
				StringBuilder stringBuilder = new StringBuilder(num2 + num3 + text.Length);
				for (int j = 0; j <= num2; j++)
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(text);
				for (int k = 0; k <= num3; k++)
				{
					stringBuilder.Append(" ");
				}
				this.WriteText(stringBuilder.ToString());
			}

			// Token: 0x0400038F RID: 911
			private HtmlContentBuilder builder;

			// Token: 0x04000390 RID: 912
			private bool lastTextEndsWithPunctuation;
		}
	}
}
