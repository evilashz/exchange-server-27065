using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005FC RID: 1532
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class MimeConstants
	{
		// Token: 0x06003F29 RID: 16169 RVA: 0x00106C74 File Offset: 0x00104E74
		internal static bool IsInReservedHeaderNamespace(string headerName)
		{
			foreach (string value in MimeConstants.reservedHeaderPrefixes)
			{
				if (headerName.StartsWith(value, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x00106CAC File Offset: 0x00104EAC
		internal static bool IsReservedHeaderAllowedOnDelivery(string headerName)
		{
			foreach (string value in MimeConstants.reservedHeadersAllowedOnDelivery)
			{
				if (headerName.Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040021B0 RID: 8624
		internal const string MessageRFC822Type = "message/rfc822";

		// Token: 0x040021B1 RID: 8625
		internal const string MessageExternalBody = "message/external-body";

		// Token: 0x040021B2 RID: 8626
		internal const string MessageHeadersRFC822Type = "text/rfc822-headers";

		// Token: 0x040021B3 RID: 8627
		internal const string MessagePartial = "message/partial";

		// Token: 0x040021B4 RID: 8628
		internal const string MessageDeliveryStatus = "message/delivery-status";

		// Token: 0x040021B5 RID: 8629
		internal const string MessageDispositionNotification = "message/disposition-notification";

		// Token: 0x040021B6 RID: 8630
		internal const string TextPlainBodyType = "text/plain";

		// Token: 0x040021B7 RID: 8631
		internal const string TextEnrichedBodyType = "text/enriched";

		// Token: 0x040021B8 RID: 8632
		internal const string TextHtmlBodyType = "text/html";

		// Token: 0x040021B9 RID: 8633
		internal const string TextCalendarType = "text/calendar";

		// Token: 0x040021BA RID: 8634
		internal const string TextDirectoryType = "text/directory";

		// Token: 0x040021BB RID: 8635
		internal const string TextVCardType = "text/x-vcard";

		// Token: 0x040021BC RID: 8636
		internal const string TextMediaType = "text";

		// Token: 0x040021BD RID: 8637
		internal const string MultipartMediaType = "multipart";

		// Token: 0x040021BE RID: 8638
		internal const string ApplicationOpenMailType = "application/x-openmail";

		// Token: 0x040021BF RID: 8639
		internal const string ApplicationMsTnefType = "application/ms-tnef";

		// Token: 0x040021C0 RID: 8640
		internal const string ApplicationXPkcs7MimeType = "application/x-pkcs7-mime";

		// Token: 0x040021C1 RID: 8641
		internal const string ApplicationPkcs7MimeType = "application/pkcs7-mime";

		// Token: 0x040021C2 RID: 8642
		internal const string ApplicationOctetStreamType = "application/octet-stream";

		// Token: 0x040021C3 RID: 8643
		internal const string ApplicationMacBinHex40 = "application/mac-binhex40";

		// Token: 0x040021C4 RID: 8644
		internal const string ApplicationAppleFile = "application/applefile";

		// Token: 0x040021C5 RID: 8645
		internal const string ImageContentType = "image/";

		// Token: 0x040021C6 RID: 8646
		internal const string ImageJpegContentType = "image/jpeg";

		// Token: 0x040021C7 RID: 8647
		internal const string ImagePjpegContentType = "image/pjpeg";

		// Token: 0x040021C8 RID: 8648
		internal const string ImageGifContentType = "image/gif";

		// Token: 0x040021C9 RID: 8649
		internal const string ImageBmpContentType = "image/bmp";

		// Token: 0x040021CA RID: 8650
		internal const string ImagePngContentType = "image/png";

		// Token: 0x040021CB RID: 8651
		internal const string ImageXpngContentType = "image/x-png";

		// Token: 0x040021CC RID: 8652
		internal const string MultiPart = "multipart/";

		// Token: 0x040021CD RID: 8653
		internal const string MultiPartAlternative = "multipart/alternative";

		// Token: 0x040021CE RID: 8654
		internal const string MultiPartMixed = "multipart/mixed";

		// Token: 0x040021CF RID: 8655
		internal const string MultiPartRelated = "multipart/related";

		// Token: 0x040021D0 RID: 8656
		internal const string MultiPartParallel = "multipart/parallel";

		// Token: 0x040021D1 RID: 8657
		internal const string MultiPartDigest = "multipart/digest";

		// Token: 0x040021D2 RID: 8658
		internal const string MultiPartSigned = "multipart/signed";

		// Token: 0x040021D3 RID: 8659
		internal const string MultiPartReport = "multipart/report";

		// Token: 0x040021D4 RID: 8660
		internal const string MultiPartFormData = "multipart/form-data";

		// Token: 0x040021D5 RID: 8661
		internal const string MultiPartAppleDouble = "multipart/appledouble";

		// Token: 0x040021D6 RID: 8662
		internal const string ContentClassVoice = "voice";

		// Token: 0x040021D7 RID: 8663
		internal const string ContentClassVoiceCa = "voice-ca";

		// Token: 0x040021D8 RID: 8664
		internal const string ContentClassVoiceUc = "voice-uc";

		// Token: 0x040021D9 RID: 8665
		internal const string ContentClassTranscription = "voice+transcript";

		// Token: 0x040021DA RID: 8666
		internal const string ContentClassUMPartner = "MS-Exchange-UM-Partner";

		// Token: 0x040021DB RID: 8667
		internal const string ContentClassFax = "fax";

		// Token: 0x040021DC RID: 8668
		internal const string ContentClassFaxCa = "fax-ca";

		// Token: 0x040021DD RID: 8669
		internal const string ContentClassMissedCall = "missedcall";

		// Token: 0x040021DE RID: 8670
		internal const string ContentClassRss = "RSS";

		// Token: 0x040021DF RID: 8671
		internal const string ContentClassSharing = "Sharing";

		// Token: 0x040021E0 RID: 8672
		internal const string ContentClassCustomPrefix = "urn:content-class:custom.";

		// Token: 0x040021E1 RID: 8673
		internal const string InfoPathContentClassPrefix = "InfoPathForm.";

		// Token: 0x040021E2 RID: 8674
		internal const string ThreadTopic = "Thread-Topic";

		// Token: 0x040021E3 RID: 8675
		internal const string ThreadIndex = "Thread-Index";

		// Token: 0x040021E4 RID: 8676
		internal const string TnefCorrelator = "X-MS-TNEF-Correlator";

		// Token: 0x040021E5 RID: 8677
		internal const string MsHasAttach = "X-MS-Has-Attach";

		// Token: 0x040021E6 RID: 8678
		internal const string XMimeOle = "X-MimeOle";

		// Token: 0x040021E7 RID: 8679
		internal const string XAutoResponseSuppress = "X-Auto-Response-Suppress";

		// Token: 0x040021E8 RID: 8680
		internal const string XSendOutlookRecallReport = "X-MS-Exchange-Send-Outlook-Recall-Report";

		// Token: 0x040021E9 RID: 8681
		internal const string AcceptLanguage = "Accept-Language";

		// Token: 0x040021EA RID: 8682
		internal const string XAcceptLanguage = "X-Accept-Language";

		// Token: 0x040021EB RID: 8683
		internal const string XNotesItem = "X-Notes-Item";

		// Token: 0x040021EC RID: 8684
		internal const string XMicrosoftClassified = "x-microsoft-classified";

		// Token: 0x040021ED RID: 8685
		internal const string XMicrosoftClassification = "x-microsoft-classification";

		// Token: 0x040021EE RID: 8686
		internal const string XMicrosoftClassificationDescription = "x-microsoft-classDesc";

		// Token: 0x040021EF RID: 8687
		internal const string XMicrosoftClassificationGuid = "x-microsoft-classID";

		// Token: 0x040021F0 RID: 8688
		internal const string XMicrosoftClassificationKeep = "X-microsoft-classKeep";

		// Token: 0x040021F1 RID: 8689
		internal const string XQuarantineOriginalSender = "X-MS-Exchange-Organization-Original-Sender";

		// Token: 0x040021F2 RID: 8690
		internal const string XPayloadProviderGuid = "X-Payload-Provider-Guid";

		// Token: 0x040021F3 RID: 8691
		internal const string XPayloadClass = "X-Payload-Class";

		// Token: 0x040021F4 RID: 8692
		internal const string CallingTelephoneNumber = "X-CallingTelephoneNumber";

		// Token: 0x040021F5 RID: 8693
		internal const string VoiceMessageDuration = "X-VoiceMessageDuration";

		// Token: 0x040021F6 RID: 8694
		internal const string VoiceMessageSenderName = "X-VoiceMessageSenderName";

		// Token: 0x040021F7 RID: 8695
		internal const string FaxNumberOfPages = "X-FaxNumberOfPages";

		// Token: 0x040021F8 RID: 8696
		internal const string AttachmentOrder = "X-AttachmentOrder";

		// Token: 0x040021F9 RID: 8697
		internal const string CallId = "X-CallID";

		// Token: 0x040021FA RID: 8698
		internal const string RequireProtectedPlayOnPhone = "X-RequireProtectedPlayOnPhone";

		// Token: 0x040021FB RID: 8699
		internal const string XMSJournalReport = "X-MS-Journal-Report";

		// Token: 0x040021FC RID: 8700
		internal const string XMSOutlookProtectionRuleVersion = "X-MS-Outlook-Client-Rule-Addin-Version";

		// Token: 0x040021FD RID: 8701
		internal const string XMSOutlookProtectionRuleTimestamp = "X-MS-Outlook-Client-Rule-Config-Timestamp";

		// Token: 0x040021FE RID: 8702
		internal const string XMSOutlookProtectionRuleOverridden = "X-MS-Outlook-Client-Rule-Overridden";

		// Token: 0x040021FF RID: 8703
		internal const string XMessageFlag = "X-Message-Flag";

		// Token: 0x04002200 RID: 8704
		internal const string XListHelp = "X-List-Help";

		// Token: 0x04002201 RID: 8705
		internal const string XListSubscribe = "X-List-Subscribe";

		// Token: 0x04002202 RID: 8706
		internal const string XListUnsubscribe = "X-List-Unsubscribe";

		// Token: 0x04002203 RID: 8707
		internal const string MimeSkeletonContentId = "X-Exchange-Mime-Skeleton-Content-Id";

		// Token: 0x04002204 RID: 8708
		internal const string XLoop = "X-MS-Exchange-Inbox-Rules-Loop";

		// Token: 0x04002205 RID: 8709
		internal const string XSharingBrowseUrl = "x-sharing-browse-url";

		// Token: 0x04002206 RID: 8710
		internal const string XSharingCapabilities = "x-sharing-capabilities";

		// Token: 0x04002207 RID: 8711
		internal const string XSharingFlavor = "x-sharing-flavor";

		// Token: 0x04002208 RID: 8712
		internal const string XSharingInstanceGuid = "x-sharing-instance-guid";

		// Token: 0x04002209 RID: 8713
		internal const string XSharingLocalType = "x-sharing-local-type";

		// Token: 0x0400220A RID: 8714
		internal const string XSharingProviderGuid = "x-sharing-provider-guid";

		// Token: 0x0400220B RID: 8715
		internal const string XSharingProviderName = "x-sharing-provider-name";

		// Token: 0x0400220C RID: 8716
		internal const string XSharingProviderUrl = "x-sharing-provider-url";

		// Token: 0x0400220D RID: 8717
		internal const string XSharingRemoteName = "x-sharing-remote-name";

		// Token: 0x0400220E RID: 8718
		internal const string XSharingRemotePath = "x-sharing-remote-path";

		// Token: 0x0400220F RID: 8719
		internal const string XSharingRemoteType = "x-sharing-remote-type";

		// Token: 0x04002210 RID: 8720
		internal const string XExchangeApplicationFlags = "X-MS-Exchange-ApplicationFlags";

		// Token: 0x04002211 RID: 8721
		internal const string XGroupMailboxId = "X-MS-Exchange-GroupMailbox-Id";

		// Token: 0x04002212 RID: 8722
		internal const string XMessageSentRepresentingType = "X-MS-Exchange-MessageSentRepresentingType";

		// Token: 0x04002213 RID: 8723
		internal const string XMSExchangeSharedMailboxSentItemsRoutingAgentProcessed = "X-MS-Exchange-SharedMailbox-RoutingAgent-Processed";

		// Token: 0x04002214 RID: 8724
		internal const string XMSExchangeSharedMailboxSentItemMessage = "X-MS-Exchange-SharedMailbox-SentItem-Message";

		// Token: 0x04002215 RID: 8725
		internal const int XLoopMaximumLength = 1000;

		// Token: 0x04002216 RID: 8726
		internal const int XLoopMaximumCount = 3;

		// Token: 0x04002217 RID: 8727
		internal const int XLoopDatacenterMaximumCount = 1;

		// Token: 0x04002218 RID: 8728
		internal const string XMsExchangeOrganizationAutoforwarded = "X-MS-Exchange-Organization-AutoForwarded";

		// Token: 0x04002219 RID: 8729
		internal const string XMsExchOrganizationAntispamReport = "X-MS-Exchange-Organization-Antispam-Report";

		// Token: 0x0400221A RID: 8730
		internal const string XMsExchOrganizationPrd = "X-MS-Exchange-Organization-PRD";

		// Token: 0x0400221B RID: 8731
		internal const string XMsExchOrganizationScl = "X-MS-Exchange-Organization-SCL";

		// Token: 0x0400221C RID: 8732
		internal const string XMsExchOrganizationPcl = "X-MS-Exchange-Organization-PCL";

		// Token: 0x0400221D RID: 8733
		internal const string XMSExchOrganizationSenderIdResult = "X-MS-Exchange-Organization-SenderIdResult";

		// Token: 0x0400221E RID: 8734
		internal const string XMsExchOrganizationAuthDomain = "X-MS-Exchange-Organization-AuthDomain";

		// Token: 0x0400221F RID: 8735
		internal const string XMsExchOrganizationAuthMechanism = "X-MS-Exchange-Organization-AuthMechanism";

		// Token: 0x04002220 RID: 8736
		internal const string XMsExchOrganizationAuthSource = "X-MS-Exchange-Organization-AuthSource";

		// Token: 0x04002221 RID: 8737
		internal const string XMsExchOrganizationAuthAs = "X-MS-Exchange-Organization-AuthAs";

		// Token: 0x04002222 RID: 8738
		internal const string XMsExchangeOrganizationRightsProtectMessage = "X-MS-Exchange-Organization-RightsProtectMessage";

		// Token: 0x04002223 RID: 8739
		internal const string XMsExchangeOrganizationCrossPremiseDecrypted = "X-MS-Exchange-Organization-CrossPremiseDecrypted";

		// Token: 0x04002224 RID: 8740
		internal const string XMSExchangeForestTransportDecryptionActionHeader = "X-MS-Exchange-Forest-TransportDecryption-Action";

		// Token: 0x04002225 RID: 8741
		internal const string XMsExchOrganizationAVStampMailbox = "X-MS-Exchange-Organization-AVStamp-Mailbox";

		// Token: 0x04002226 RID: 8742
		internal const string XMsExchOrganizationOriginalScl = "X-MS-Exchange-Organization-Original-SCL";

		// Token: 0x04002227 RID: 8743
		internal const string XMSExchangeOrganizationDecisionMakers = "X-MS-Exchange-Organization-Approval-Allowed-Decision-Makers";

		// Token: 0x04002228 RID: 8744
		internal const string XMSExchangeOrganizationApprovalRequestor = "X-MS-Exchange-Organization-Approval-Requestor";

		// Token: 0x04002229 RID: 8745
		internal const string XMSExchangeJournalingRemoteAccounts = "X-MS-Exchange-Organization-Journaling-Remote-Accounts";

		// Token: 0x0400222A RID: 8746
		internal const string XMSExchangeOrganizationRecipientP2Type = "X-MS-Exchange-Organization-Recipient-P2-Type";

		// Token: 0x0400222B RID: 8747
		internal const string XMsExchOrganizationOriginalReceivedTime = "X-MS-Exchange-Organization-Original-Received-Time";

		// Token: 0x0400222C RID: 8748
		internal const string XMSExchangeOrganizationNetworkMessageId = "X-MS-Exchange-Organization-Network-Message-Id";

		// Token: 0x0400222D RID: 8749
		internal const string XMsExchOrganizationSharingInstanceGuid = "X-MS-Exchange-Organization-Sharing-Instance-Guid";

		// Token: 0x0400222E RID: 8750
		internal const string XMsExchOrganizationCloudId = "X-MS-Exchange-Organization-Cloud-Id";

		// Token: 0x0400222F RID: 8751
		internal const string XMsExchOrganizationCloudVersion = "X-MS-Exchange-Organization-Cloud-Version";

		// Token: 0x04002230 RID: 8752
		internal const string XMsExchangeUMPartnerAssignedID = "X-MS-Exchange-UM-PartnerAssignedID";

		// Token: 0x04002231 RID: 8753
		internal const string XMsExchangeUMPartnerContent = "X-MS-Exchange-UM-PartnerContent";

		// Token: 0x04002232 RID: 8754
		internal const string XMsExchangeUMPartnerContext = "X-MS-Exchange-UM-PartnerContext";

		// Token: 0x04002233 RID: 8755
		internal const string XMsExchangeUMPartnerStatus = "X-MS-Exchange-UM-PartnerStatus";

		// Token: 0x04002234 RID: 8756
		internal const string XMsExchangeUMDialPlanLanguage = "X-MS-Exchange-UM-DialPlanLanguage";

		// Token: 0x04002235 RID: 8757
		internal const string XMsExchangeUMCallerInformedOfAnalysis = "X-MS-Exchange-UM-CallerInformedOfAnalysis";

		// Token: 0x04002236 RID: 8758
		internal const string ReceivedSPF = "Received-SPF";

		// Token: 0x04002237 RID: 8759
		private const string XMsExchOrganizationPrefix = "X-MS-Exchange-Organization-";

		// Token: 0x04002238 RID: 8760
		private const string XMsExchForestPrefix = "X-MS-Exchange-Forest-";

		// Token: 0x04002239 RID: 8761
		private const string XMsExchCrossPremisesPrefix = "X-MS-Exchange-CrossPremises-";

		// Token: 0x0400223A RID: 8762
		internal const string XMSExchangeOutlookProtectionRuleVersion = "X-MS-Exchange-Organization-Outlook-Protection-Rule-Addin-Version";

		// Token: 0x0400223B RID: 8763
		internal const string XMSExchangeOutlookProtectionRuleConfigTimestamp = "X-MS-Exchange-Organization-Outlook-Protection-Rule-Config-Timestamp";

		// Token: 0x0400223C RID: 8764
		internal const string XMSExchangeOutlookProtectionRuleOverridden = "X-MS-Exchange-Organization-Outlook-Protection-Rule-Overridden";

		// Token: 0x0400223D RID: 8765
		internal const string XMsExchOrganizationDeliverAsRead = "X-MS-Exchange-Organization-DeliverAsRead";

		// Token: 0x0400223E RID: 8766
		internal const string XMsExchOrganizationMailReplied = "X-MS-Exchange-Organization-MailReplied";

		// Token: 0x0400223F RID: 8767
		internal const string XMsExchOrganizationMailForwarded = "X-MS-Exchange-Organization-MailForwarded";

		// Token: 0x04002240 RID: 8768
		internal const string XMsExchOrganizationCategory = "X-MS-Exchange-Organization-Category";

		// Token: 0x04002241 RID: 8769
		internal const string XMSExchangeOrganizationDirectionalityHeader = "X-MS-Exchange-Organization-MessageDirectionality";

		// Token: 0x04002242 RID: 8770
		internal const string XMSExchangeCalendarOriginatorIdHeader = "X-MS-Exchange-Calendar-Originator-Id";

		// Token: 0x04002243 RID: 8771
		internal const string XMSExchangeCalendarSeriesSequenceNumberHeader = "X-MS-Exchange-Calendar-Series-Sequence-Number";

		// Token: 0x04002244 RID: 8772
		internal const string XMSExchangeCalendarSeriesIdHeader = "X-MS-Exchange-Calendar-Series-Id";

		// Token: 0x04002245 RID: 8773
		internal const string XMSExchangeCalendarSeriesInstanceIdHeader = "X-MS-Exchange-Calendar-Series-Instance-Id";

		// Token: 0x04002246 RID: 8774
		internal const string XMSExchangeCalendarSeriesMasterIdHeader = "X-MS-Exchange-Calendar-Series-Master-Id";

		// Token: 0x04002247 RID: 8775
		internal const string XMSExchangeCalendarSeriesInstanceUnparkedHeader = "X-MS-Exchange-Calendar-Series-Instance-Unparked";

		// Token: 0x04002248 RID: 8776
		internal const string XMSExchangeCalendarSeriesInstanceCalendarItemIdHeader = "X-MS-Exchange-Calendar-Series-Instance-Calendar-Item-Id";

		// Token: 0x04002249 RID: 8777
		internal const string XMsExchImapAppendStamp = "X-MS-Exchange-ImapAppendStamp";

		// Token: 0x0400224A RID: 8778
		internal const string XmsExchOrganizationDlpPrefix = "X-Ms-Exchange-Organization-Dlp-";

		// Token: 0x0400224B RID: 8779
		internal const string XMsExchOrganizationDlpSenderOverride = "X-Ms-Exchange-Organization-Dlp-SenderOverrideJustification";

		// Token: 0x0400224C RID: 8780
		internal const string XMsExchOrganizationDlpFalsePositive = "X-Ms-Exchange-Organization-Dlp-FalsePositive";

		// Token: 0x0400224D RID: 8781
		internal const string XMsExchOrganizationDlpDetectedClassifications = "X-Ms-Exchange-Organization-Dlp-DetectedClassifications";

		// Token: 0x0400224E RID: 8782
		internal const string XMSExchangeOrganizationAVStampServiceName = "X-MS-Exchange-Organization-AVStamp-Service";

		// Token: 0x0400224F RID: 8783
		internal const string XMSExchangeOrganizationAVStampEnterpriseName = "X-MS-Exchange-Organization-AVStamp-Enterprise";

		// Token: 0x04002250 RID: 8784
		public const string XMmsMesageId = "X-MmsMessageId";

		// Token: 0x04002251 RID: 8785
		internal const string XSimSlotNumber = "X-SimSlotNumber";

		// Token: 0x04002252 RID: 8786
		public const string XSentTime = "X-SentTime";

		// Token: 0x04002253 RID: 8787
		public const string XSentItem = "X-SentItem";

		// Token: 0x04002254 RID: 8788
		internal const string RecipientP2TypeBcc = "Bcc";

		// Token: 0x04002255 RID: 8789
		internal const string High = "high";

		// Token: 0x04002256 RID: 8790
		internal const string Low = "low";

		// Token: 0x04002257 RID: 8791
		internal const string Normal = "normal";

		// Token: 0x04002258 RID: 8792
		internal const string XPriority5 = "5";

		// Token: 0x04002259 RID: 8793
		internal const string XPriority3 = "3";

		// Token: 0x0400225A RID: 8794
		internal const string XPriority1 = "1";

		// Token: 0x0400225B RID: 8795
		internal const string Personal = "personal";

		// Token: 0x0400225C RID: 8796
		internal const string Private = "private";

		// Token: 0x0400225D RID: 8797
		internal const string CompanyConfidential = "company-confidential";

		// Token: 0x0400225E RID: 8798
		internal const string Urgent = "urgent";

		// Token: 0x0400225F RID: 8799
		internal const string NonUrgent = "non-urgent";

		// Token: 0x04002260 RID: 8800
		internal const string Inline = "inline";

		// Token: 0x04002261 RID: 8801
		internal const string Attachment = "attachment";

		// Token: 0x04002262 RID: 8802
		internal const string CreationDate = "creation-date";

		// Token: 0x04002263 RID: 8803
		internal const string ModificationDate = "modification-date";

		// Token: 0x04002264 RID: 8804
		internal const string ReadDate = "read-date";

		// Token: 0x04002265 RID: 8805
		internal const string Charset = "charset";

		// Token: 0x04002266 RID: 8806
		internal const string Profile = "profile";

		// Token: 0x04002267 RID: 8807
		internal const string VCard = "vCard";

		// Token: 0x04002268 RID: 8808
		internal const string CharsetUtf8 = "utf-8";

		// Token: 0x04002269 RID: 8809
		internal const string CharsetUSAscii = "us-ascii";

		// Token: 0x0400226A RID: 8810
		internal const string Name = "name";

		// Token: 0x0400226B RID: 8811
		internal const string Size = "size";

		// Token: 0x0400226C RID: 8812
		internal const string Type = "type";

		// Token: 0x0400226D RID: 8813
		internal const string Boundary = "boundary";

		// Token: 0x0400226E RID: 8814
		internal const string ReportType = "report-type";

		// Token: 0x0400226F RID: 8815
		internal const string Filename = "filename";

		// Token: 0x04002270 RID: 8816
		internal const string DeliveryStatus = "delivery-status";

		// Token: 0x04002271 RID: 8817
		internal const string DispositionNotification = "disposition-notification";

		// Token: 0x04002272 RID: 8818
		internal const string BinhexEncoding = "binhex";

		// Token: 0x04002273 RID: 8819
		internal const string Base64Encoding = "base64";

		// Token: 0x04002274 RID: 8820
		internal const string QPEncoding = "quoted-printable";

		// Token: 0x04002275 RID: 8821
		internal const string SevenBitEncoding = "7-bit";

		// Token: 0x04002276 RID: 8822
		internal const string EmptyDateHeader = "<empty>";

		// Token: 0x04002277 RID: 8823
		internal const string DefaultSmimeAttachmentName = "smime.p7m";

		// Token: 0x04002278 RID: 8824
		internal const string HeaderTrue = "true";

		// Token: 0x04002279 RID: 8825
		internal const string HeaderFalse = "false";

		// Token: 0x0400227A RID: 8826
		internal const string MethodHeader = "method";

		// Token: 0x0400227B RID: 8827
		internal const string AccessTypeParameter = "access-type";

		// Token: 0x0400227C RID: 8828
		internal const string AccessTypeAnonFtp = "anon-ftp";

		// Token: 0x0400227D RID: 8829
		internal const string SiteParameter = "site";

		// Token: 0x0400227E RID: 8830
		internal const string DirectoryParameter = "directory";

		// Token: 0x0400227F RID: 8831
		internal const string UrlExtension = ".url";

		// Token: 0x04002280 RID: 8832
		internal const string FtpShortcutPrefix = "[InternetShortcut]\r\nURL=ftp://";

		// Token: 0x04002281 RID: 8833
		internal const string FtpShortcutSuffix = "\r\n";

		// Token: 0x04002282 RID: 8834
		internal const string ModeParameter = "mode";

		// Token: 0x04002283 RID: 8835
		internal const string ModeAscii = "ascii";

		// Token: 0x04002284 RID: 8836
		internal const string FtpShortcutTypeAscii = ";type=a";

		// Token: 0x04002285 RID: 8837
		internal const string ModeImage = "image";

		// Token: 0x04002286 RID: 8838
		internal const string FtpShortcutTypeImage = ";type=i";

		// Token: 0x04002287 RID: 8839
		internal const string Yes = "yes";

		// Token: 0x04002288 RID: 8840
		internal const string XMimeOleExchange = "Microsoft Exchange";

		// Token: 0x04002289 RID: 8841
		internal const string SmimeType = "smime-type";

		// Token: 0x0400228A RID: 8842
		internal const string SmimeTypeEncrypted = "enveloped-data";

		// Token: 0x0400228B RID: 8843
		internal const string SmimeTypeSigned = "signed-data";

		// Token: 0x0400228C RID: 8844
		internal const string SmimeTypeCerts = "certs-only";

		// Token: 0x0400228D RID: 8845
		internal const int MaxReferencesHeaderLength = 65536;

		// Token: 0x0400228E RID: 8846
		internal const string SmtpAddressType = "SMTP";

		// Token: 0x0400228F RID: 8847
		internal const string ImceaAddressPrefix = "IMCEA";

		// Token: 0x04002290 RID: 8848
		internal const string FileNameWinMailDat = "winmail.dat";

		// Token: 0x04002291 RID: 8849
		internal const string UndisclosedRecipientsGroup = "undisclosed recipients";

		// Token: 0x04002292 RID: 8850
		internal const string OleAttachmentDefaultFilenameExtension = "jpg";

		// Token: 0x04002293 RID: 8851
		internal const string OleAttachmentDefaultFilename = "{0}.jpg";

		// Token: 0x04002294 RID: 8852
		internal const string OleAttachmentComputedFilename = "{0} {1}.jpg";

		// Token: 0x04002295 RID: 8853
		internal const string Rfc822 = "RFC822";

		// Token: 0x04002296 RID: 8854
		internal const string Unknown = "unknown";

		// Token: 0x04002297 RID: 8855
		internal const string ExtensionPrefix = "X-";

		// Token: 0x04002298 RID: 8856
		internal const string DefaultMdnDispositionTypeAndModifier = "automatic-action/MDN-sent-automatically";

		// Token: 0x04002299 RID: 8857
		internal const string ExtensionMsg = ".msg";

		// Token: 0x0400229A RID: 8858
		internal const string ExtensionVcf = ".vcf";

		// Token: 0x0400229B RID: 8859
		internal const string FailedJournalReportName = "corrupt.eml";

		// Token: 0x0400229C RID: 8860
		internal const string ProtectedAttachmentFilename = "message.rpmsg";

		// Token: 0x0400229D RID: 8861
		internal const string ProtectedAttachmentContentType = "application/x-microsoft-rpmsg-message";

		// Token: 0x0400229E RID: 8862
		internal const uint TnefNamedPropertyTag = 2147483648U;

		// Token: 0x0400229F RID: 8863
		internal const uint MultiValuedPropertyFlag = 4096U;

		// Token: 0x040022A0 RID: 8864
		private static string[] reservedHeaderPrefixes = new string[]
		{
			"X-MS-Exchange-Organization-",
			"X-MS-Exchange-Forest-",
			"X-MS-Exchange-CrossPremises-"
		};

		// Token: 0x040022A1 RID: 8865
		private static string[] reservedHeadersAllowedOnDelivery = new string[]
		{
			"X-MS-Exchange-Organization-Antispam-Report",
			"X-MS-Exchange-Organization-AVStamp-Mailbox",
			"X-MS-Exchange-Organization-Approval-Requestor",
			"X-MS-Exchange-Organization-AuthAs",
			"X-MS-Exchange-Organization-AuthDomain",
			"X-MS-Exchange-Organization-AuthMechanism",
			"X-MS-Exchange-Organization-AuthSource",
			"X-MS-Exchange-Organization-CrossPremiseDecrypted",
			"X-MS-Exchange-Organization-Approval-Allowed-Decision-Makers",
			"X-MS-Exchange-Organization-Journaling-Remote-Accounts",
			"X-MS-Exchange-Organization-Original-SCL",
			"X-MS-Exchange-Organization-PCL",
			"X-MS-Exchange-Organization-PRD",
			"X-MS-Exchange-Organization-Recipient-P2-Type",
			"X-MS-Exchange-Organization-SCL",
			"X-MS-Exchange-Organization-SenderIdResult",
			"X-MS-Exchange-Organization-Sharing-Instance-Guid",
			"X-MS-Exchange-Forest-TransportDecryption-Action",
			"X-MS-Exchange-Organization-DeliverAsRead",
			"X-MS-Exchange-Organization-MailReplied",
			"X-MS-Exchange-Organization-MailForwarded",
			"X-MS-Exchange-Organization-Category",
			"X-MS-Exchange-Organization-Network-Message-Id",
			"X-MS-Exchange-Organization-AVStamp-Service",
			"X-MS-Exchange-Organization-AVStamp-Enterprise",
			"X-MS-Exchange-Organization-MessageDirectionality"
		};

		// Token: 0x040022A2 RID: 8866
		internal static readonly Guid IID_IStorage = new Guid(11, 0, 0, 192, 0, 0, 0, 0, 0, 0, 70);
	}
}
