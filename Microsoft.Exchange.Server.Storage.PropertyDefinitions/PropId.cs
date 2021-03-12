using System;

namespace Microsoft.Exchange.Server.Storage.PropTags
{
	// Token: 0x02000003 RID: 3
	public static class PropId
	{
		// Token: 0x04000016 RID: 22
		public const ushort NULL = 0;

		// Token: 0x04000017 RID: 23
		public const ushort AcknowledgementMode = 1;

		// Token: 0x04000018 RID: 24
		public const ushort TestTest = 1;

		// Token: 0x04000019 RID: 25
		public const ushort AlternateRecipientAllowed = 2;

		// Token: 0x0400001A RID: 26
		public const ushort AuthorizingUsers = 3;

		// Token: 0x0400001B RID: 27
		public const ushort AutoForwardComment = 4;

		// Token: 0x0400001C RID: 28
		public const ushort AutoForwarded = 5;

		// Token: 0x0400001D RID: 29
		public const ushort ContentConfidentialityAlgorithmId = 6;

		// Token: 0x0400001E RID: 30
		public const ushort ContentCorrelator = 7;

		// Token: 0x0400001F RID: 31
		public const ushort ContentIdentifier = 8;

		// Token: 0x04000020 RID: 32
		public const ushort ContentLength = 9;

		// Token: 0x04000021 RID: 33
		public const ushort ContentReturnRequested = 10;

		// Token: 0x04000022 RID: 34
		public const ushort ConversationKey = 11;

		// Token: 0x04000023 RID: 35
		public const ushort ConversionEits = 12;

		// Token: 0x04000024 RID: 36
		public const ushort ConversionWithLossProhibited = 13;

		// Token: 0x04000025 RID: 37
		public const ushort ConvertedEits = 14;

		// Token: 0x04000026 RID: 38
		public const ushort DeferredDeliveryTime = 15;

		// Token: 0x04000027 RID: 39
		public const ushort DeliverTime = 16;

		// Token: 0x04000028 RID: 40
		public const ushort DiscardReason = 17;

		// Token: 0x04000029 RID: 41
		public const ushort DisclosureOfRecipients = 18;

		// Token: 0x0400002A RID: 42
		public const ushort DLExpansionHistory = 19;

		// Token: 0x0400002B RID: 43
		public const ushort DLExpansionProhibited = 20;

		// Token: 0x0400002C RID: 44
		public const ushort ExpiryTime = 21;

		// Token: 0x0400002D RID: 45
		public const ushort ImplicitConversionProhibited = 22;

		// Token: 0x0400002E RID: 46
		public const ushort Importance = 23;

		// Token: 0x0400002F RID: 47
		public const ushort IPMID = 24;

		// Token: 0x04000030 RID: 48
		public const ushort LatestDeliveryTime = 25;

		// Token: 0x04000031 RID: 49
		public const ushort MessageClass = 26;

		// Token: 0x04000032 RID: 50
		public const ushort MessageDeliveryId = 27;

		// Token: 0x04000033 RID: 51
		public const ushort MessageSecurityLabel = 30;

		// Token: 0x04000034 RID: 52
		public const ushort ObsoletedIPMS = 31;

		// Token: 0x04000035 RID: 53
		public const ushort OriginallyIntendedRecipientName = 32;

		// Token: 0x04000036 RID: 54
		public const ushort OriginalEITS = 33;

		// Token: 0x04000037 RID: 55
		public const ushort OriginatorCertificate = 34;

		// Token: 0x04000038 RID: 56
		public const ushort DeliveryReportRequested = 35;

		// Token: 0x04000039 RID: 57
		public const ushort OriginatorReturnAddress = 36;

		// Token: 0x0400003A RID: 58
		public const ushort ParentKey = 37;

		// Token: 0x0400003B RID: 59
		public const ushort Priority = 38;

		// Token: 0x0400003C RID: 60
		public const ushort OriginCheck = 39;

		// Token: 0x0400003D RID: 61
		public const ushort ProofOfSubmissionRequested = 40;

		// Token: 0x0400003E RID: 62
		public const ushort ReadReceiptRequested = 41;

		// Token: 0x0400003F RID: 63
		public const ushort ReceiptTime = 42;

		// Token: 0x04000040 RID: 64
		public const ushort RecipientReassignmentProhibited = 43;

		// Token: 0x04000041 RID: 65
		public const ushort RedirectionHistory = 44;

		// Token: 0x04000042 RID: 66
		public const ushort RelatedIPMS = 45;

		// Token: 0x04000043 RID: 67
		public const ushort OriginalSensitivity = 46;

		// Token: 0x04000044 RID: 68
		public const ushort Languages = 47;

		// Token: 0x04000045 RID: 69
		public const ushort ReplyTime = 48;

		// Token: 0x04000046 RID: 70
		public const ushort ReportTag = 49;

		// Token: 0x04000047 RID: 71
		public const ushort ReportTime = 50;

		// Token: 0x04000048 RID: 72
		public const ushort ReturnedIPM = 51;

		// Token: 0x04000049 RID: 73
		public const ushort Security = 52;

		// Token: 0x0400004A RID: 74
		public const ushort IncompleteCopy = 53;

		// Token: 0x0400004B RID: 75
		public const ushort Sensitivity = 54;

		// Token: 0x0400004C RID: 76
		public const ushort Subject = 55;

		// Token: 0x0400004D RID: 77
		public const ushort SubjectIPM = 56;

		// Token: 0x0400004E RID: 78
		public const ushort ClientSubmitTime = 57;

		// Token: 0x0400004F RID: 79
		public const ushort ReportName = 58;

		// Token: 0x04000050 RID: 80
		public const ushort SentRepresentingSearchKey = 59;

		// Token: 0x04000051 RID: 81
		public const ushort X400ContentType = 60;

		// Token: 0x04000052 RID: 82
		public const ushort SubjectPrefix = 61;

		// Token: 0x04000053 RID: 83
		public const ushort NonReceiptReason = 62;

		// Token: 0x04000054 RID: 84
		public const ushort ReceivedByEntryId = 63;

		// Token: 0x04000055 RID: 85
		public const ushort ReceivedByName = 64;

		// Token: 0x04000056 RID: 86
		public const ushort SentRepresentingEntryId = 65;

		// Token: 0x04000057 RID: 87
		public const ushort SentRepresentingName = 66;

		// Token: 0x04000058 RID: 88
		public const ushort ReceivedRepresentingEntryId = 67;

		// Token: 0x04000059 RID: 89
		public const ushort ReceivedRepresentingName = 68;

		// Token: 0x0400005A RID: 90
		public const ushort ReportEntryId = 69;

		// Token: 0x0400005B RID: 91
		public const ushort ReadReceiptEntryId = 70;

		// Token: 0x0400005C RID: 92
		public const ushort MessageSubmissionId = 71;

		// Token: 0x0400005D RID: 93
		public const ushort ProviderSubmitTime = 72;

		// Token: 0x0400005E RID: 94
		public const ushort OriginalSubject = 73;

		// Token: 0x0400005F RID: 95
		public const ushort DiscVal = 74;

		// Token: 0x04000060 RID: 96
		public const ushort OriginalMessageClass = 75;

		// Token: 0x04000061 RID: 97
		public const ushort OriginalAuthorEntryId = 76;

		// Token: 0x04000062 RID: 98
		public const ushort OriginalAuthorName = 77;

		// Token: 0x04000063 RID: 99
		public const ushort OriginalSubmitTime = 78;

		// Token: 0x04000064 RID: 100
		public const ushort ReplyRecipientEntries = 79;

		// Token: 0x04000065 RID: 101
		public const ushort ReplyRecipientNames = 80;

		// Token: 0x04000066 RID: 102
		public const ushort ReceivedBySearchKey = 81;

		// Token: 0x04000067 RID: 103
		public const ushort ReceivedRepresentingSearchKey = 82;

		// Token: 0x04000068 RID: 104
		public const ushort ReadReceiptSearchKey = 83;

		// Token: 0x04000069 RID: 105
		public const ushort ReportSearchKey = 84;

		// Token: 0x0400006A RID: 106
		public const ushort OriginalDeliveryTime = 85;

		// Token: 0x0400006B RID: 107
		public const ushort OriginalAuthorSearchKey = 86;

		// Token: 0x0400006C RID: 108
		public const ushort MessageToMe = 87;

		// Token: 0x0400006D RID: 109
		public const ushort MessageCCMe = 88;

		// Token: 0x0400006E RID: 110
		public const ushort MessageRecipMe = 89;

		// Token: 0x0400006F RID: 111
		public const ushort OriginalSenderName = 90;

		// Token: 0x04000070 RID: 112
		public const ushort OriginalSenderEntryId = 91;

		// Token: 0x04000071 RID: 113
		public const ushort OriginalSenderSearchKey = 92;

		// Token: 0x04000072 RID: 114
		public const ushort OriginalSentRepresentingName = 93;

		// Token: 0x04000073 RID: 115
		public const ushort OriginalSentRepresentingEntryId = 94;

		// Token: 0x04000074 RID: 116
		public const ushort OriginalSentRepresentingSearchKey = 95;

		// Token: 0x04000075 RID: 117
		public const ushort StartDate = 96;

		// Token: 0x04000076 RID: 118
		public const ushort EndDate = 97;

		// Token: 0x04000077 RID: 119
		public const ushort OwnerApptId = 98;

		// Token: 0x04000078 RID: 120
		public const ushort ResponseRequested = 99;

		// Token: 0x04000079 RID: 121
		public const ushort SentRepresentingAddressType = 100;

		// Token: 0x0400007A RID: 122
		public const ushort SentRepresentingEmailAddress = 101;

		// Token: 0x0400007B RID: 123
		public const ushort OriginalSenderAddressType = 102;

		// Token: 0x0400007C RID: 124
		public const ushort OriginalSenderEmailAddress = 103;

		// Token: 0x0400007D RID: 125
		public const ushort OriginalSentRepresentingAddressType = 104;

		// Token: 0x0400007E RID: 126
		public const ushort OriginalSentRepresentingEmailAddress = 105;

		// Token: 0x0400007F RID: 127
		public const ushort ConversationTopic = 112;

		// Token: 0x04000080 RID: 128
		public const ushort ConversationIndex = 113;

		// Token: 0x04000081 RID: 129
		public const ushort OriginalDisplayBcc = 114;

		// Token: 0x04000082 RID: 130
		public const ushort OriginalDisplayCc = 115;

		// Token: 0x04000083 RID: 131
		public const ushort OriginalDisplayTo = 116;

		// Token: 0x04000084 RID: 132
		public const ushort ReceivedByAddressType = 117;

		// Token: 0x04000085 RID: 133
		public const ushort ReceivedByEmailAddress = 118;

		// Token: 0x04000086 RID: 134
		public const ushort ReceivedRepresentingAddressType = 119;

		// Token: 0x04000087 RID: 135
		public const ushort ReceivedRepresentingEmailAddress = 120;

		// Token: 0x04000088 RID: 136
		public const ushort OriginalAuthorAddressType = 121;

		// Token: 0x04000089 RID: 137
		public const ushort OriginalAuthorEmailAddress = 122;

		// Token: 0x0400008A RID: 138
		public const ushort OriginallyIntendedRecipientAddressType = 124;

		// Token: 0x0400008B RID: 139
		public const ushort TransportMessageHeaders = 125;

		// Token: 0x0400008C RID: 140
		public const ushort Delegation = 126;

		// Token: 0x0400008D RID: 141
		public const ushort TNEFCorrelationKey = 127;

		// Token: 0x0400008E RID: 142
		public const ushort ReportDisposition = 128;

		// Token: 0x0400008F RID: 143
		public const ushort ReportDispositionMode = 129;

		// Token: 0x04000090 RID: 144
		public const ushort ReportOriginalSender = 130;

		// Token: 0x04000091 RID: 145
		public const ushort ReportDispositionToNames = 131;

		// Token: 0x04000092 RID: 146
		public const ushort ReportDispositionToEmailAddress = 132;

		// Token: 0x04000093 RID: 147
		public const ushort ReportDispositionOptions = 133;

		// Token: 0x04000094 RID: 148
		public const ushort RichContent = 134;

		// Token: 0x04000095 RID: 149
		public const ushort AdministratorEMail = 256;

		// Token: 0x04000096 RID: 150
		public const ushort ContentIntegrityCheck = 3072;

		// Token: 0x04000097 RID: 151
		public const ushort ExplicitConversion = 3073;

		// Token: 0x04000098 RID: 152
		public const ushort ReturnRequested = 3074;

		// Token: 0x04000099 RID: 153
		public const ushort MessageToken = 3075;

		// Token: 0x0400009A RID: 154
		public const ushort NDRReasonCode = 3076;

		// Token: 0x0400009B RID: 155
		public const ushort NDRDiagCode = 3077;

		// Token: 0x0400009C RID: 156
		public const ushort NonReceiptNotificationRequested = 3078;

		// Token: 0x0400009D RID: 157
		public const ushort DeliveryPoint = 3079;

		// Token: 0x0400009E RID: 158
		public const ushort NonDeliveryReportRequested = 3080;

		// Token: 0x0400009F RID: 159
		public const ushort OriginatorRequestedAlterateRecipient = 3081;

		// Token: 0x040000A0 RID: 160
		public const ushort PhysicalDeliveryBureauFaxDelivery = 3082;

		// Token: 0x040000A1 RID: 161
		public const ushort PhysicalDeliveryMode = 3083;

		// Token: 0x040000A2 RID: 162
		public const ushort PhysicalDeliveryReportRequest = 3084;

		// Token: 0x040000A3 RID: 163
		public const ushort PhysicalForwardingAddress = 3085;

		// Token: 0x040000A4 RID: 164
		public const ushort PhysicalForwardingAddressRequested = 3086;

		// Token: 0x040000A5 RID: 165
		public const ushort PhysicalForwardingProhibited = 3087;

		// Token: 0x040000A6 RID: 166
		public const ushort PhysicalRenditionAttributes = 3088;

		// Token: 0x040000A7 RID: 167
		public const ushort ProofOfDelivery = 3089;

		// Token: 0x040000A8 RID: 168
		public const ushort ProofOfDeliveryRequested = 3090;

		// Token: 0x040000A9 RID: 169
		public const ushort RecipientCertificate = 3091;

		// Token: 0x040000AA RID: 170
		public const ushort RecipientNumberForAdvice = 3092;

		// Token: 0x040000AB RID: 171
		public const ushort RecipientType = 3093;

		// Token: 0x040000AC RID: 172
		public const ushort RegisteredMailType = 3094;

		// Token: 0x040000AD RID: 173
		public const ushort ReplyRequested = 3095;

		// Token: 0x040000AE RID: 174
		public const ushort RequestedDeliveryMethod = 3096;

		// Token: 0x040000AF RID: 175
		public const ushort SenderEntryId = 3097;

		// Token: 0x040000B0 RID: 176
		public const ushort SenderName = 3098;

		// Token: 0x040000B1 RID: 177
		public const ushort SupplementaryInfo = 3099;

		// Token: 0x040000B2 RID: 178
		public const ushort TypeOfMTSUser = 3100;

		// Token: 0x040000B3 RID: 179
		public const ushort SenderSearchKey = 3101;

		// Token: 0x040000B4 RID: 180
		public const ushort SenderAddressType = 3102;

		// Token: 0x040000B5 RID: 181
		public const ushort SenderEmailAddress = 3103;

		// Token: 0x040000B6 RID: 182
		public const ushort ParticipantSID = 3108;

		// Token: 0x040000B7 RID: 183
		public const ushort ParticipantGuid = 3109;

		// Token: 0x040000B8 RID: 184
		public const ushort ToGroupExpansionRecipients = 3110;

		// Token: 0x040000B9 RID: 185
		public const ushort CcGroupExpansionRecipients = 3111;

		// Token: 0x040000BA RID: 186
		public const ushort BccGroupExpansionRecipients = 3112;

		// Token: 0x040000BB RID: 187
		public const ushort CurrentVersion = 3584;

		// Token: 0x040000BC RID: 188
		public const ushort DeleteAfterSubmit = 3585;

		// Token: 0x040000BD RID: 189
		public const ushort DisplayBcc = 3586;

		// Token: 0x040000BE RID: 190
		public const ushort DisplayCc = 3587;

		// Token: 0x040000BF RID: 191
		public const ushort DisplayTo = 3588;

		// Token: 0x040000C0 RID: 192
		public const ushort ParentDisplay = 3589;

		// Token: 0x040000C1 RID: 193
		public const ushort MessageDeliveryTime = 3590;

		// Token: 0x040000C2 RID: 194
		public const ushort MessageFlags = 3591;

		// Token: 0x040000C3 RID: 195
		public const ushort MessageSize = 3592;

		// Token: 0x040000C4 RID: 196
		public const ushort MessageSize32 = 3592;

		// Token: 0x040000C5 RID: 197
		public const ushort ParentEntryId = 3593;

		// Token: 0x040000C6 RID: 198
		public const ushort ParentEntryIdSvrEid = 3593;

		// Token: 0x040000C7 RID: 199
		public const ushort SentMailEntryId = 3594;

		// Token: 0x040000C8 RID: 200
		public const ushort Correlate = 3596;

		// Token: 0x040000C9 RID: 201
		public const ushort CorrelateMTSID = 3597;

		// Token: 0x040000CA RID: 202
		public const ushort DiscreteValues = 3598;

		// Token: 0x040000CB RID: 203
		public const ushort Responsibility = 3599;

		// Token: 0x040000CC RID: 204
		public const ushort SpoolerStatus = 3600;

		// Token: 0x040000CD RID: 205
		public const ushort TransportStatus = 3601;

		// Token: 0x040000CE RID: 206
		public const ushort MessageRecipients = 3602;

		// Token: 0x040000CF RID: 207
		public const ushort MessageRecipientsMVBin = 3602;

		// Token: 0x040000D0 RID: 208
		public const ushort MessageAttachments = 3603;

		// Token: 0x040000D1 RID: 209
		public const ushort ItemSubobjectsBin = 3603;

		// Token: 0x040000D2 RID: 210
		public const ushort SubmitFlags = 3604;

		// Token: 0x040000D3 RID: 211
		public const ushort RecipientStatus = 3605;

		// Token: 0x040000D4 RID: 212
		public const ushort TransportKey = 3606;

		// Token: 0x040000D5 RID: 213
		public const ushort MsgStatus = 3607;

		// Token: 0x040000D6 RID: 214
		public const ushort MessageDownloadTime = 3608;

		// Token: 0x040000D7 RID: 215
		public const ushort CreationVersion = 3609;

		// Token: 0x040000D8 RID: 216
		public const ushort ModifyVersion = 3610;

		// Token: 0x040000D9 RID: 217
		public const ushort HasAttach = 3611;

		// Token: 0x040000DA RID: 218
		public const ushort BodyCRC = 3612;

		// Token: 0x040000DB RID: 219
		public const ushort NormalizedSubject = 3613;

		// Token: 0x040000DC RID: 220
		public const ushort RTFInSync = 3615;

		// Token: 0x040000DD RID: 221
		public const ushort AttachSize = 3616;

		// Token: 0x040000DE RID: 222
		public const ushort AttachSizeInt64 = 3616;

		// Token: 0x040000DF RID: 223
		public const ushort AttachNum = 3617;

		// Token: 0x040000E0 RID: 224
		public const ushort Preprocess = 3618;

		// Token: 0x040000E1 RID: 225
		public const ushort FolderInternetId = 3619;

		// Token: 0x040000E2 RID: 226
		public const ushort HighestFolderInternetId = 3619;

		// Token: 0x040000E3 RID: 227
		public const ushort InternetArticleNumber = 3619;

		// Token: 0x040000E4 RID: 228
		public const ushort OriginatingMTACertificate = 3621;

		// Token: 0x040000E5 RID: 229
		public const ushort ProofOfSubmission = 3622;

		// Token: 0x040000E6 RID: 230
		public const ushort NTSecurityDescriptor = 3623;

		// Token: 0x040000E7 RID: 231
		public const ushort PrimarySendAccount = 3624;

		// Token: 0x040000E8 RID: 232
		public const ushort NextSendAccount = 3625;

		// Token: 0x040000E9 RID: 233
		public const ushort TodoItemFlags = 3627;

		// Token: 0x040000EA RID: 234
		public const ushort SwappedTODOStore = 3628;

		// Token: 0x040000EB RID: 235
		public const ushort SwappedTODOData = 3629;

		// Token: 0x040000EC RID: 236
		public const ushort IMAPId = 3631;

		// Token: 0x040000ED RID: 237
		public const ushort OriginalSourceServerVersion = 3633;

		// Token: 0x040000EE RID: 238
		public const ushort ReplFlags = 3640;

		// Token: 0x040000EF RID: 239
		public const ushort MessageDeepAttachments = 3642;

		// Token: 0x040000F0 RID: 240
		public const ushort AclTableAndSecurityDescriptor = 3647;

		// Token: 0x040000F1 RID: 241
		public const ushort SenderGuid = 3648;

		// Token: 0x040000F2 RID: 242
		public const ushort SentRepresentingGuid = 3649;

		// Token: 0x040000F3 RID: 243
		public const ushort OriginalSenderGuid = 3650;

		// Token: 0x040000F4 RID: 244
		public const ushort OriginalSentRepresentingGuid = 3651;

		// Token: 0x040000F5 RID: 245
		public const ushort ReadReceiptGuid = 3652;

		// Token: 0x040000F6 RID: 246
		public const ushort ReportGuid = 3653;

		// Token: 0x040000F7 RID: 247
		public const ushort OriginatorGuid = 3654;

		// Token: 0x040000F8 RID: 248
		public const ushort ReportDestinationGuid = 3655;

		// Token: 0x040000F9 RID: 249
		public const ushort OriginalAuthorGuid = 3656;

		// Token: 0x040000FA RID: 250
		public const ushort ReceivedByGuid = 3657;

		// Token: 0x040000FB RID: 251
		public const ushort ReceivedRepresentingGuid = 3658;

		// Token: 0x040000FC RID: 252
		public const ushort CreatorGuid = 3659;

		// Token: 0x040000FD RID: 253
		public const ushort LastModifierGuid = 3660;

		// Token: 0x040000FE RID: 254
		public const ushort SenderSID = 3661;

		// Token: 0x040000FF RID: 255
		public const ushort SentRepresentingSID = 3662;

		// Token: 0x04000100 RID: 256
		public const ushort OriginalSenderSid = 3663;

		// Token: 0x04000101 RID: 257
		public const ushort OriginalSentRepresentingSid = 3664;

		// Token: 0x04000102 RID: 258
		public const ushort ReadReceiptSid = 3665;

		// Token: 0x04000103 RID: 259
		public const ushort ReportSid = 3666;

		// Token: 0x04000104 RID: 260
		public const ushort OriginatorSid = 3667;

		// Token: 0x04000105 RID: 261
		public const ushort ReportDestinationSid = 3668;

		// Token: 0x04000106 RID: 262
		public const ushort OriginalAuthorSid = 3669;

		// Token: 0x04000107 RID: 263
		public const ushort RcvdBySid = 3670;

		// Token: 0x04000108 RID: 264
		public const ushort RcvdRepresentingSid = 3671;

		// Token: 0x04000109 RID: 265
		public const ushort CreatorSID = 3672;

		// Token: 0x0400010A RID: 266
		public const ushort LastModifierSid = 3673;

		// Token: 0x0400010B RID: 267
		public const ushort RecipientCAI = 3674;

		// Token: 0x0400010C RID: 268
		public const ushort ConversationCreatorSID = 3675;

		// Token: 0x0400010D RID: 269
		public const ushort Catalog = 3675;

		// Token: 0x0400010E RID: 270
		public const ushort CISearchEnabled = 3676;

		// Token: 0x0400010F RID: 271
		public const ushort CINotificationEnabled = 3677;

		// Token: 0x04000110 RID: 272
		public const ushort MaxIndices = 3678;

		// Token: 0x04000111 RID: 273
		public const ushort SourceFid = 3679;

		// Token: 0x04000112 RID: 274
		public const ushort PFContactsGuid = 3680;

		// Token: 0x04000113 RID: 275
		public const ushort URLCompNamePostfix = 3681;

		// Token: 0x04000114 RID: 276
		public const ushort URLCompNameSet = 3682;

		// Token: 0x04000115 RID: 277
		public const ushort SubfolderCount = 3683;

		// Token: 0x04000116 RID: 278
		public const ushort DeletedSubfolderCt = 3684;

		// Token: 0x04000117 RID: 279
		public const ushort MaxCachedViews = 3688;

		// Token: 0x04000118 RID: 280
		public const ushort Read = 3689;

		// Token: 0x04000119 RID: 281
		public const ushort NTSecurityDescriptorAsXML = 3690;

		// Token: 0x0400011A RID: 282
		public const ushort AdminNTSecurityDescriptorAsXML = 3691;

		// Token: 0x0400011B RID: 283
		public const ushort CreatorSidAsXML = 3692;

		// Token: 0x0400011C RID: 284
		public const ushort LastModifierSidAsXML = 3693;

		// Token: 0x0400011D RID: 285
		public const ushort SenderSIDAsXML = 3694;

		// Token: 0x0400011E RID: 286
		public const ushort SentRepresentingSidAsXML = 3695;

		// Token: 0x0400011F RID: 287
		public const ushort OriginalSenderSIDAsXML = 3696;

		// Token: 0x04000120 RID: 288
		public const ushort OriginalSentRepresentingSIDAsXML = 3697;

		// Token: 0x04000121 RID: 289
		public const ushort ReadReceiptSIDAsXML = 3698;

		// Token: 0x04000122 RID: 290
		public const ushort ReportSIDAsXML = 3699;

		// Token: 0x04000123 RID: 291
		public const ushort OriginatorSidAsXML = 3700;

		// Token: 0x04000124 RID: 292
		public const ushort ReportDestinationSIDAsXML = 3701;

		// Token: 0x04000125 RID: 293
		public const ushort OriginalAuthorSIDAsXML = 3702;

		// Token: 0x04000126 RID: 294
		public const ushort ReceivedBySIDAsXML = 3703;

		// Token: 0x04000127 RID: 295
		public const ushort ReceivedRepersentingSIDAsXML = 3704;

		// Token: 0x04000128 RID: 296
		public const ushort TrustSender = 3705;

		// Token: 0x04000129 RID: 297
		public const ushort MergeMidsetDeleted = 3706;

		// Token: 0x0400012A RID: 298
		public const ushort ReserveRangeOfIDs = 3707;

		// Token: 0x0400012B RID: 299
		public const ushort SenderSMTPAddress = 3721;

		// Token: 0x0400012C RID: 300
		public const ushort SentRepresentingSMTPAddress = 3722;

		// Token: 0x0400012D RID: 301
		public const ushort OriginalSenderSMTPAddress = 3723;

		// Token: 0x0400012E RID: 302
		public const ushort OriginalSentRepresentingSMTPAddress = 3724;

		// Token: 0x0400012F RID: 303
		public const ushort ReadReceiptSMTPAddress = 3725;

		// Token: 0x04000130 RID: 304
		public const ushort ReportSMTPAddress = 3726;

		// Token: 0x04000131 RID: 305
		public const ushort OriginatorSMTPAddress = 3727;

		// Token: 0x04000132 RID: 306
		public const ushort ReportDestinationSMTPAddress = 3728;

		// Token: 0x04000133 RID: 307
		public const ushort OriginalAuthorSMTPAddress = 3729;

		// Token: 0x04000134 RID: 308
		public const ushort ReceivedBySMTPAddress = 3730;

		// Token: 0x04000135 RID: 309
		public const ushort ReceivedRepresentingSMTPAddress = 3731;

		// Token: 0x04000136 RID: 310
		public const ushort CreatorSMTPAddress = 3732;

		// Token: 0x04000137 RID: 311
		public const ushort LastModifierSMTPAddress = 3733;

		// Token: 0x04000138 RID: 312
		public const ushort VirusScannerStamp = 3734;

		// Token: 0x04000139 RID: 313
		public const ushort VirusTransportStamp = 3734;

		// Token: 0x0400013A RID: 314
		public const ushort AddrTo = 3735;

		// Token: 0x0400013B RID: 315
		public const ushort AddrCc = 3736;

		// Token: 0x0400013C RID: 316
		public const ushort ExtendedRuleActions = 3737;

		// Token: 0x0400013D RID: 317
		public const ushort ExtendedRuleCondition = 3738;

		// Token: 0x0400013E RID: 318
		public const ushort ExtendedRuleSizeLimit = 3739;

		// Token: 0x0400013F RID: 319
		public const ushort EntourageSentHistory = 3743;

		// Token: 0x04000140 RID: 320
		public const ushort ProofInProgress = 3746;

		// Token: 0x04000141 RID: 321
		public const ushort SearchAttachmentsOLK = 3749;

		// Token: 0x04000142 RID: 322
		public const ushort SearchRecipEmailTo = 3750;

		// Token: 0x04000143 RID: 323
		public const ushort SearchRecipEmailCc = 3751;

		// Token: 0x04000144 RID: 324
		public const ushort SearchRecipEmailBcc = 3752;

		// Token: 0x04000145 RID: 325
		public const ushort SFGAOFlags = 3754;

		// Token: 0x04000146 RID: 326
		public const ushort SearchFullTextSubject = 3756;

		// Token: 0x04000147 RID: 327
		public const ushort SearchFullTextBody = 3757;

		// Token: 0x04000148 RID: 328
		public const ushort FullTextConversationIndex = 3758;

		// Token: 0x04000149 RID: 329
		public const ushort SearchAllIndexedProps = 3759;

		// Token: 0x0400014A RID: 330
		public const ushort SearchRecipients = 3761;

		// Token: 0x0400014B RID: 331
		public const ushort SearchRecipientsTo = 3762;

		// Token: 0x0400014C RID: 332
		public const ushort SearchRecipientsCc = 3763;

		// Token: 0x0400014D RID: 333
		public const ushort SearchRecipientsBcc = 3764;

		// Token: 0x0400014E RID: 334
		public const ushort SearchAccountTo = 3765;

		// Token: 0x0400014F RID: 335
		public const ushort SearchAccountCc = 3766;

		// Token: 0x04000150 RID: 336
		public const ushort SearchAccountBcc = 3767;

		// Token: 0x04000151 RID: 337
		public const ushort SearchEmailAddressTo = 3768;

		// Token: 0x04000152 RID: 338
		public const ushort SearchEmailAddressCc = 3769;

		// Token: 0x04000153 RID: 339
		public const ushort SearchEmailAddressBcc = 3770;

		// Token: 0x04000154 RID: 340
		public const ushort SearchSmtpAddressTo = 3771;

		// Token: 0x04000155 RID: 341
		public const ushort SearchSmtpAddressCc = 3772;

		// Token: 0x04000156 RID: 342
		public const ushort SearchSmtpAddressBcc = 3773;

		// Token: 0x04000157 RID: 343
		public const ushort SearchSender = 3774;

		// Token: 0x04000158 RID: 344
		public const ushort IsIRMMessage = 3789;

		// Token: 0x04000159 RID: 345
		public const ushort SearchIsPartiallyIndexed = 3790;

		// Token: 0x0400015A RID: 346
		public const ushort FreeBusyNTSD = 3840;

		// Token: 0x0400015B RID: 347
		public const ushort RenewTime = 3841;

		// Token: 0x0400015C RID: 348
		public const ushort DeliveryOrRenewTime = 3842;

		// Token: 0x0400015D RID: 349
		public const ushort ConversationFamilyId = 3843;

		// Token: 0x0400015E RID: 350
		public const ushort LikeCount = 3844;

		// Token: 0x0400015F RID: 351
		public const ushort RichContentDeprecated = 3845;

		// Token: 0x04000160 RID: 352
		public const ushort PeopleCentricConversationId = 3846;

		// Token: 0x04000161 RID: 353
		public const ushort DiscoveryAnnotation = 3854;

		// Token: 0x04000162 RID: 354
		public const ushort Access = 4084;

		// Token: 0x04000163 RID: 355
		public const ushort RowType = 4085;

		// Token: 0x04000164 RID: 356
		public const ushort InstanceKey = 4086;

		// Token: 0x04000165 RID: 357
		public const ushort InstanceKeySvrEid = 4086;

		// Token: 0x04000166 RID: 358
		public const ushort AccessLevel = 4087;

		// Token: 0x04000167 RID: 359
		public const ushort MappingSignature = 4088;

		// Token: 0x04000168 RID: 360
		public const ushort RecordKey = 4089;

		// Token: 0x04000169 RID: 361
		public const ushort RecordKeySvrEid = 4089;

		// Token: 0x0400016A RID: 362
		public const ushort StoreRecordKey = 4090;

		// Token: 0x0400016B RID: 363
		public const ushort StoreEntryId = 4091;

		// Token: 0x0400016C RID: 364
		public const ushort MiniIcon = 4092;

		// Token: 0x0400016D RID: 365
		public const ushort Icon = 4093;

		// Token: 0x0400016E RID: 366
		public const ushort ObjectType = 4094;

		// Token: 0x0400016F RID: 367
		public const ushort EntryId = 4095;

		// Token: 0x04000170 RID: 368
		public const ushort EntryIdSvrEid = 4095;

		// Token: 0x04000171 RID: 369
		public const ushort BodyUnicode = 4096;

		// Token: 0x04000172 RID: 370
		public const ushort IsIntegJobMailboxGuid = 4096;

		// Token: 0x04000173 RID: 371
		public const ushort ReportText = 4097;

		// Token: 0x04000174 RID: 372
		public const ushort IsIntegJobGuid = 4097;

		// Token: 0x04000175 RID: 373
		public const ushort OriginatorAndDLExpansionHistory = 4098;

		// Token: 0x04000176 RID: 374
		public const ushort IsIntegJobFlags = 4098;

		// Token: 0x04000177 RID: 375
		public const ushort ReportingDLName = 4099;

		// Token: 0x04000178 RID: 376
		public const ushort IsIntegJobTask = 4099;

		// Token: 0x04000179 RID: 377
		public const ushort ReportingMTACertificate = 4100;

		// Token: 0x0400017A RID: 378
		public const ushort IsIntegJobState = 4100;

		// Token: 0x0400017B RID: 379
		public const ushort IsIntegJobCreationTime = 4101;

		// Token: 0x0400017C RID: 380
		public const ushort RtfSyncBodyCrc = 4102;

		// Token: 0x0400017D RID: 381
		public const ushort IsIntegJobCompletedTime = 4102;

		// Token: 0x0400017E RID: 382
		public const ushort RtfSyncBodyCount = 4103;

		// Token: 0x0400017F RID: 383
		public const ushort IsIntegJobLastExecutionTime = 4103;

		// Token: 0x04000180 RID: 384
		public const ushort RtfSyncBodyTag = 4104;

		// Token: 0x04000181 RID: 385
		public const ushort IsIntegJobCorruptionsDetected = 4104;

		// Token: 0x04000182 RID: 386
		public const ushort RtfCompressed = 4105;

		// Token: 0x04000183 RID: 387
		public const ushort IsIntegJobCorruptionsFixed = 4105;

		// Token: 0x04000184 RID: 388
		public const ushort AlternateBestBody = 4106;

		// Token: 0x04000185 RID: 389
		public const ushort IsIntegJobRequestGuid = 4106;

		// Token: 0x04000186 RID: 390
		public const ushort IsIntegJobProgress = 4107;

		// Token: 0x04000187 RID: 391
		public const ushort IsIntegJobCorruptions = 4108;

		// Token: 0x04000188 RID: 392
		public const ushort IsIntegJobSource = 4109;

		// Token: 0x04000189 RID: 393
		public const ushort IsIntegJobPriority = 4110;

		// Token: 0x0400018A RID: 394
		public const ushort IsIntegJobTimeInServer = 4111;

		// Token: 0x0400018B RID: 395
		public const ushort RtfSyncPrefixCount = 4112;

		// Token: 0x0400018C RID: 396
		public const ushort IsIntegJobMailboxNumber = 4112;

		// Token: 0x0400018D RID: 397
		public const ushort RtfSyncTrailingCount = 4113;

		// Token: 0x0400018E RID: 398
		public const ushort IsIntegJobError = 4113;

		// Token: 0x0400018F RID: 399
		public const ushort OriginallyIntendedRecipientEntryId = 4114;

		// Token: 0x04000190 RID: 400
		public const ushort BodyHtml = 4115;

		// Token: 0x04000191 RID: 401
		public const ushort BodyHtmlUnicode = 4115;

		// Token: 0x04000192 RID: 402
		public const ushort BodyContentLocation = 4116;

		// Token: 0x04000193 RID: 403
		public const ushort BodyContentId = 4117;

		// Token: 0x04000194 RID: 404
		public const ushort NativeBodyInfo = 4118;

		// Token: 0x04000195 RID: 405
		public const ushort NativeBodyType = 4118;

		// Token: 0x04000196 RID: 406
		public const ushort NativeBody = 4118;

		// Token: 0x04000197 RID: 407
		public const ushort AnnotationToken = 4119;

		// Token: 0x04000198 RID: 408
		public const ushort InternetApproved = 4144;

		// Token: 0x04000199 RID: 409
		public const ushort InternetFollowupTo = 4147;

		// Token: 0x0400019A RID: 410
		public const ushort InternetMessageId = 4149;

		// Token: 0x0400019B RID: 411
		public const ushort InetNewsgroups = 4150;

		// Token: 0x0400019C RID: 412
		public const ushort InternetReferences = 4153;

		// Token: 0x0400019D RID: 413
		public const ushort PostReplyFolderEntries = 4157;

		// Token: 0x0400019E RID: 414
		public const ushort NNTPXRef = 4160;

		// Token: 0x0400019F RID: 415
		public const ushort InReplyToId = 4162;

		// Token: 0x040001A0 RID: 416
		public const ushort OriginalInternetMessageId = 4166;

		// Token: 0x040001A1 RID: 417
		public const ushort IconIndex = 4224;

		// Token: 0x040001A2 RID: 418
		public const ushort LastVerbExecuted = 4225;

		// Token: 0x040001A3 RID: 419
		public const ushort LastVerbExecutionTime = 4226;

		// Token: 0x040001A4 RID: 420
		public const ushort Relevance = 4228;

		// Token: 0x040001A5 RID: 421
		public const ushort FlagStatus = 4240;

		// Token: 0x040001A6 RID: 422
		public const ushort FlagCompleteTime = 4241;

		// Token: 0x040001A7 RID: 423
		public const ushort FormatPT = 4242;

		// Token: 0x040001A8 RID: 424
		public const ushort FollowupIcon = 4245;

		// Token: 0x040001A9 RID: 425
		public const ushort BlockStatus = 4246;

		// Token: 0x040001AA RID: 426
		public const ushort ItemTempFlags = 4247;

		// Token: 0x040001AB RID: 427
		public const ushort SMTPTempTblData = 4288;

		// Token: 0x040001AC RID: 428
		public const ushort SMTPTempTblData2 = 4289;

		// Token: 0x040001AD RID: 429
		public const ushort SMTPTempTblData3 = 4290;

		// Token: 0x040001AE RID: 430
		public const ushort DAVSubmitData = 4294;

		// Token: 0x040001AF RID: 431
		public const ushort ImapCachedMsgSize = 4336;

		// Token: 0x040001B0 RID: 432
		public const ushort DisableFullFidelity = 4338;

		// Token: 0x040001B1 RID: 433
		public const ushort URLCompName = 4339;

		// Token: 0x040001B2 RID: 434
		public const ushort AttrHidden = 4340;

		// Token: 0x040001B3 RID: 435
		public const ushort AttrSystem = 4341;

		// Token: 0x040001B4 RID: 436
		public const ushort AttrReadOnly = 4342;

		// Token: 0x040001B5 RID: 437
		public const ushort PredictedActions = 4612;

		// Token: 0x040001B6 RID: 438
		public const ushort GroupingActions = 4613;

		// Token: 0x040001B7 RID: 439
		public const ushort PredictedActionsSummary = 4614;

		// Token: 0x040001B8 RID: 440
		public const ushort PredictedActionsThresholds = 4615;

		// Token: 0x040001B9 RID: 441
		public const ushort IsClutter = 4615;

		// Token: 0x040001BA RID: 442
		public const ushort InferencePredictedReplyForwardReasons = 4616;

		// Token: 0x040001BB RID: 443
		public const ushort InferencePredictedDeleteReasons = 4617;

		// Token: 0x040001BC RID: 444
		public const ushort InferencePredictedIgnoreReasons = 4618;

		// Token: 0x040001BD RID: 445
		public const ushort OriginalDeliveryFolderInfo = 4619;

		// Token: 0x040001BE RID: 446
		public const ushort RowId = 12288;

		// Token: 0x040001BF RID: 447
		public const ushort UserInformationGuid = 12288;

		// Token: 0x040001C0 RID: 448
		public const ushort DisplayName = 12289;

		// Token: 0x040001C1 RID: 449
		public const ushort UserInformationDisplayName = 12289;

		// Token: 0x040001C2 RID: 450
		public const ushort AddressType = 12290;

		// Token: 0x040001C3 RID: 451
		public const ushort UserInformationCreationTime = 12290;

		// Token: 0x040001C4 RID: 452
		public const ushort EmailAddress = 12291;

		// Token: 0x040001C5 RID: 453
		public const ushort UserInformationLastModificationTime = 12291;

		// Token: 0x040001C6 RID: 454
		public const ushort Comment = 12292;

		// Token: 0x040001C7 RID: 455
		public const ushort UserInformationChangeNumber = 12292;

		// Token: 0x040001C8 RID: 456
		public const ushort Depth = 12293;

		// Token: 0x040001C9 RID: 457
		public const ushort UserInformationLastInteractiveLogonTime = 12293;

		// Token: 0x040001CA RID: 458
		public const ushort ProviderDisplay = 12294;

		// Token: 0x040001CB RID: 459
		public const ushort UserInformationActiveSyncAllowedDeviceIDs = 12294;

		// Token: 0x040001CC RID: 460
		public const ushort CreationTime = 12295;

		// Token: 0x040001CD RID: 461
		public const ushort UserInformationActiveSyncBlockedDeviceIDs = 12295;

		// Token: 0x040001CE RID: 462
		public const ushort LastModificationTime = 12296;

		// Token: 0x040001CF RID: 463
		public const ushort UserInformationActiveSyncDebugLogging = 12296;

		// Token: 0x040001D0 RID: 464
		public const ushort ResourceFlags = 12297;

		// Token: 0x040001D1 RID: 465
		public const ushort UserInformationActiveSyncEnabled = 12297;

		// Token: 0x040001D2 RID: 466
		public const ushort ProviderDllName = 12298;

		// Token: 0x040001D3 RID: 467
		public const ushort UserInformationAdminDisplayName = 12298;

		// Token: 0x040001D4 RID: 468
		public const ushort SearchKey = 12299;

		// Token: 0x040001D5 RID: 469
		public const ushort SearchKeySvrEid = 12299;

		// Token: 0x040001D6 RID: 470
		public const ushort UserInformationAggregationSubscriptionCredential = 12299;

		// Token: 0x040001D7 RID: 471
		public const ushort ProviderUID = 12300;

		// Token: 0x040001D8 RID: 472
		public const ushort UserInformationAllowArchiveAddressSync = 12300;

		// Token: 0x040001D9 RID: 473
		public const ushort ProviderOrdinal = 12301;

		// Token: 0x040001DA RID: 474
		public const ushort UserInformationAltitude = 12301;

		// Token: 0x040001DB RID: 475
		public const ushort UserInformationAntispamBypassEnabled = 12302;

		// Token: 0x040001DC RID: 476
		public const ushort UserInformationArchiveDomain = 12303;

		// Token: 0x040001DD RID: 477
		public const ushort TargetEntryId = 12304;

		// Token: 0x040001DE RID: 478
		public const ushort UserInformationArchiveGuid = 12304;

		// Token: 0x040001DF RID: 479
		public const ushort UserInformationArchiveName = 12305;

		// Token: 0x040001E0 RID: 480
		public const ushort UserInformationArchiveQuota = 12306;

		// Token: 0x040001E1 RID: 481
		public const ushort ConversationId = 12307;

		// Token: 0x040001E2 RID: 482
		public const ushort UserInformationArchiveRelease = 12307;

		// Token: 0x040001E3 RID: 483
		public const ushort BodyTag = 12308;

		// Token: 0x040001E4 RID: 484
		public const ushort UserInformationArchiveStatus = 12308;

		// Token: 0x040001E5 RID: 485
		public const ushort ConversationIndexTrackingObsolete = 12309;

		// Token: 0x040001E6 RID: 486
		public const ushort UserInformationArchiveWarningQuota = 12309;

		// Token: 0x040001E7 RID: 487
		public const ushort ConversationIndexTracking = 12310;

		// Token: 0x040001E8 RID: 488
		public const ushort UserInformationAssistantName = 12310;

		// Token: 0x040001E9 RID: 489
		public const ushort UserInformationBirthdate = 12311;

		// Token: 0x040001EA RID: 490
		public const ushort ArchiveTag = 12312;

		// Token: 0x040001EB RID: 491
		public const ushort UserInformationBypassNestedModerationEnabled = 12312;

		// Token: 0x040001EC RID: 492
		public const ushort PolicyTag = 12313;

		// Token: 0x040001ED RID: 493
		public const ushort UserInformationC = 12313;

		// Token: 0x040001EE RID: 494
		public const ushort RetentionPeriod = 12314;

		// Token: 0x040001EF RID: 495
		public const ushort UserInformationCalendarLoggingQuota = 12314;

		// Token: 0x040001F0 RID: 496
		public const ushort StartDateEtc = 12315;

		// Token: 0x040001F1 RID: 497
		public const ushort UserInformationCalendarRepairDisabled = 12315;

		// Token: 0x040001F2 RID: 498
		public const ushort RetentionDate = 12316;

		// Token: 0x040001F3 RID: 499
		public const ushort UserInformationCalendarVersionStoreDisabled = 12316;

		// Token: 0x040001F4 RID: 500
		public const ushort RetentionFlags = 12317;

		// Token: 0x040001F5 RID: 501
		public const ushort UserInformationCity = 12317;

		// Token: 0x040001F6 RID: 502
		public const ushort ArchivePeriod = 12318;

		// Token: 0x040001F7 RID: 503
		public const ushort UserInformationCountry = 12318;

		// Token: 0x040001F8 RID: 504
		public const ushort ArchiveDate = 12319;

		// Token: 0x040001F9 RID: 505
		public const ushort UserInformationCountryCode = 12319;

		// Token: 0x040001FA RID: 506
		public const ushort UserInformationCountryOrRegion = 12320;

		// Token: 0x040001FB RID: 507
		public const ushort UserInformationDefaultMailTip = 12321;

		// Token: 0x040001FC RID: 508
		public const ushort UserInformationDeliverToMailboxAndForward = 12322;

		// Token: 0x040001FD RID: 509
		public const ushort UserInformationDescription = 12323;

		// Token: 0x040001FE RID: 510
		public const ushort UserInformationDisabledArchiveGuid = 12324;

		// Token: 0x040001FF RID: 511
		public const ushort UserInformationDowngradeHighPriorityMessagesEnabled = 12325;

		// Token: 0x04000200 RID: 512
		public const ushort UserInformationECPEnabled = 12326;

		// Token: 0x04000201 RID: 513
		public const ushort UserInformationEmailAddressPolicyEnabled = 12327;

		// Token: 0x04000202 RID: 514
		public const ushort UserInformationEwsAllowEntourage = 12328;

		// Token: 0x04000203 RID: 515
		public const ushort UserInformationEwsAllowMacOutlook = 12329;

		// Token: 0x04000204 RID: 516
		public const ushort UserInformationEwsAllowOutlook = 12330;

		// Token: 0x04000205 RID: 517
		public const ushort UserInformationEwsApplicationAccessPolicy = 12331;

		// Token: 0x04000206 RID: 518
		public const ushort UserInformationEwsEnabled = 12332;

		// Token: 0x04000207 RID: 519
		public const ushort UserInformationEwsExceptions = 12333;

		// Token: 0x04000208 RID: 520
		public const ushort UserInformationEwsWellKnownApplicationAccessPolicies = 12334;

		// Token: 0x04000209 RID: 521
		public const ushort UserInformationExchangeGuid = 12335;

		// Token: 0x0400020A RID: 522
		public const ushort UserInformationExternalOofOptions = 12336;

		// Token: 0x0400020B RID: 523
		public const ushort UserInformationFirstName = 12337;

		// Token: 0x0400020C RID: 524
		public const ushort UserInformationForwardingSmtpAddress = 12338;

		// Token: 0x0400020D RID: 525
		public const ushort UserInformationGender = 12339;

		// Token: 0x0400020E RID: 526
		public const ushort UserInformationGenericForwardingAddress = 12340;

		// Token: 0x0400020F RID: 527
		public const ushort UserInformationGeoCoordinates = 12341;

		// Token: 0x04000210 RID: 528
		public const ushort UserInformationHABSeniorityIndex = 12342;

		// Token: 0x04000211 RID: 529
		public const ushort UserInformationHasActiveSyncDevicePartnership = 12343;

		// Token: 0x04000212 RID: 530
		public const ushort UserInformationHiddenFromAddressListsEnabled = 12344;

		// Token: 0x04000213 RID: 531
		public const ushort UserInformationHiddenFromAddressListsValue = 12345;

		// Token: 0x04000214 RID: 532
		public const ushort UserInformationHomePhone = 12346;

		// Token: 0x04000215 RID: 533
		public const ushort UserInformationImapEnabled = 12347;

		// Token: 0x04000216 RID: 534
		public const ushort UserInformationImapEnableExactRFC822Size = 12348;

		// Token: 0x04000217 RID: 535
		public const ushort UserInformationImapForceICalForCalendarRetrievalOption = 12349;

		// Token: 0x04000218 RID: 536
		public const ushort UserInformationImapMessagesRetrievalMimeFormat = 12350;

		// Token: 0x04000219 RID: 537
		public const ushort UserInformationImapProtocolLoggingEnabled = 12351;

		// Token: 0x0400021A RID: 538
		public const ushort UserInformationImapSuppressReadReceipt = 12352;

		// Token: 0x0400021B RID: 539
		public const ushort UserInformationImapUseProtocolDefaults = 12353;

		// Token: 0x0400021C RID: 540
		public const ushort UserInformationIncludeInGarbageCollection = 12354;

		// Token: 0x0400021D RID: 541
		public const ushort UserInformationInitials = 12355;

		// Token: 0x0400021E RID: 542
		public const ushort UserInformationInPlaceHolds = 12356;

		// Token: 0x0400021F RID: 543
		public const ushort UserInformationInternalOnly = 12357;

		// Token: 0x04000220 RID: 544
		public const ushort UserInformationInternalUsageLocation = 12358;

		// Token: 0x04000221 RID: 545
		public const ushort UserInformationInternetEncoding = 12359;

		// Token: 0x04000222 RID: 546
		public const ushort UserInformationIsCalculatedTargetAddress = 12360;

		// Token: 0x04000223 RID: 547
		public const ushort UserInformationIsExcludedFromServingHierarchy = 12361;

		// Token: 0x04000224 RID: 548
		public const ushort UserInformationIsHierarchyReady = 12362;

		// Token: 0x04000225 RID: 549
		public const ushort UserInformationIsInactiveMailbox = 12363;

		// Token: 0x04000226 RID: 550
		public const ushort UserInformationIsSoftDeletedByDisable = 12364;

		// Token: 0x04000227 RID: 551
		public const ushort UserInformationIsSoftDeletedByRemove = 12365;

		// Token: 0x04000228 RID: 552
		public const ushort UserInformationIssueWarningQuota = 12366;

		// Token: 0x04000229 RID: 553
		public const ushort UserInformationJournalArchiveAddress = 12367;

		// Token: 0x0400022A RID: 554
		public const ushort UserInformationLanguages = 12368;

		// Token: 0x0400022B RID: 555
		public const ushort UserInformationLastExchangeChangedTime = 12369;

		// Token: 0x0400022C RID: 556
		public const ushort UserInformationLastName = 12370;

		// Token: 0x0400022D RID: 557
		public const ushort UserInformationLatitude = 12371;

		// Token: 0x0400022E RID: 558
		public const ushort UserInformationLEOEnabled = 12372;

		// Token: 0x0400022F RID: 559
		public const ushort UserInformationLocaleID = 12373;

		// Token: 0x04000230 RID: 560
		public const ushort UserInformationLongitude = 12374;

		// Token: 0x04000231 RID: 561
		public const ushort UserInformationMacAttachmentFormat = 12375;

		// Token: 0x04000232 RID: 562
		public const ushort UserInformationMailboxContainerGuid = 12376;

		// Token: 0x04000233 RID: 563
		public const ushort UserInformationMailboxMoveBatchName = 12377;

		// Token: 0x04000234 RID: 564
		public const ushort UserInformationMailboxMoveRemoteHostName = 12378;

		// Token: 0x04000235 RID: 565
		public const ushort UserInformationMailboxMoveStatus = 12379;

		// Token: 0x04000236 RID: 566
		public const ushort UserInformationMailboxRelease = 12380;

		// Token: 0x04000237 RID: 567
		public const ushort UserInformationMailTipTranslations = 12381;

		// Token: 0x04000238 RID: 568
		public const ushort UserInformationMAPIBlockOutlookNonCachedMode = 12382;

		// Token: 0x04000239 RID: 569
		public const ushort UserInformationMAPIBlockOutlookRpcHttp = 12383;

		// Token: 0x0400023A RID: 570
		public const ushort UserInformationMAPIBlockOutlookVersions = 12384;

		// Token: 0x0400023B RID: 571
		public const ushort UserInformationMAPIEnabled = 12385;

		// Token: 0x0400023C RID: 572
		public const ushort UserInformationMapiRecipient = 12386;

		// Token: 0x0400023D RID: 573
		public const ushort UserInformationMaxBlockedSenders = 12387;

		// Token: 0x0400023E RID: 574
		public const ushort UserInformationMaxReceiveSize = 12388;

		// Token: 0x0400023F RID: 575
		public const ushort UserInformationMaxSafeSenders = 12389;

		// Token: 0x04000240 RID: 576
		public const ushort UserInformationMaxSendSize = 12390;

		// Token: 0x04000241 RID: 577
		public const ushort UserInformationMemberName = 12391;

		// Token: 0x04000242 RID: 578
		public const ushort UserInformationMessageBodyFormat = 12392;

		// Token: 0x04000243 RID: 579
		public const ushort UserInformationMessageFormat = 12393;

		// Token: 0x04000244 RID: 580
		public const ushort UserInformationMessageTrackingReadStatusDisabled = 12394;

		// Token: 0x04000245 RID: 581
		public const ushort UserInformationMobileFeaturesEnabled = 12395;

		// Token: 0x04000246 RID: 582
		public const ushort UserInformationMobilePhone = 12396;

		// Token: 0x04000247 RID: 583
		public const ushort UserInformationModerationFlags = 12397;

		// Token: 0x04000248 RID: 584
		public const ushort UserInformationNotes = 12398;

		// Token: 0x04000249 RID: 585
		public const ushort UserInformationOccupation = 12399;

		// Token: 0x0400024A RID: 586
		public const ushort UserInformationOpenDomainRoutingDisabled = 12400;

		// Token: 0x0400024B RID: 587
		public const ushort UserInformationOtherHomePhone = 12401;

		// Token: 0x0400024C RID: 588
		public const ushort UserInformationOtherMobile = 12402;

		// Token: 0x0400024D RID: 589
		public const ushort UserInformationOtherTelephone = 12403;

		// Token: 0x0400024E RID: 590
		public const ushort UserInformationOWAEnabled = 12404;

		// Token: 0x0400024F RID: 591
		public const ushort UserInformationOWAforDevicesEnabled = 12405;

		// Token: 0x04000250 RID: 592
		public const ushort UserInformationPager = 12406;

		// Token: 0x04000251 RID: 593
		public const ushort UserInformationPersistedCapabilities = 12407;

		// Token: 0x04000252 RID: 594
		public const ushort UserInformationPhone = 12408;

		// Token: 0x04000253 RID: 595
		public const ushort UserInformationPhoneProviderId = 12409;

		// Token: 0x04000254 RID: 596
		public const ushort UserInformationPopEnabled = 12410;

		// Token: 0x04000255 RID: 597
		public const ushort UserInformationPopEnableExactRFC822Size = 12411;

		// Token: 0x04000256 RID: 598
		public const ushort UserInformationPopForceICalForCalendarRetrievalOption = 12412;

		// Token: 0x04000257 RID: 599
		public const ushort UserInformationPopMessagesRetrievalMimeFormat = 12413;

		// Token: 0x04000258 RID: 600
		public const ushort UserInformationPopProtocolLoggingEnabled = 12414;

		// Token: 0x04000259 RID: 601
		public const ushort UserInformationPopSuppressReadReceipt = 12415;

		// Token: 0x0400025A RID: 602
		public const ushort UserInformationPopUseProtocolDefaults = 12416;

		// Token: 0x0400025B RID: 603
		public const ushort UserInformationPostalCode = 12417;

		// Token: 0x0400025C RID: 604
		public const ushort UserInformationPostOfficeBox = 12418;

		// Token: 0x0400025D RID: 605
		public const ushort UserInformationPreviousExchangeGuid = 12419;

		// Token: 0x0400025E RID: 606
		public const ushort UserInformationPreviousRecipientTypeDetails = 12420;

		// Token: 0x0400025F RID: 607
		public const ushort UserInformationProhibitSendQuota = 12421;

		// Token: 0x04000260 RID: 608
		public const ushort UserInformationProhibitSendReceiveQuota = 12422;

		// Token: 0x04000261 RID: 609
		public const ushort UserInformationQueryBaseDNRestrictionEnabled = 12423;

		// Token: 0x04000262 RID: 610
		public const ushort UserInformationRecipientDisplayType = 12424;

		// Token: 0x04000263 RID: 611
		public const ushort UserInformationRecipientLimits = 12425;

		// Token: 0x04000264 RID: 612
		public const ushort UserInformationRecipientSoftDeletedStatus = 12426;

		// Token: 0x04000265 RID: 613
		public const ushort UserInformationRecoverableItemsQuota = 12427;

		// Token: 0x04000266 RID: 614
		public const ushort UserInformationRecoverableItemsWarningQuota = 12428;

		// Token: 0x04000267 RID: 615
		public const ushort UserInformationRegion = 12429;

		// Token: 0x04000268 RID: 616
		public const ushort UserInformationRemotePowerShellEnabled = 12430;

		// Token: 0x04000269 RID: 617
		public const ushort UserInformationRemoteRecipientType = 12431;

		// Token: 0x0400026A RID: 618
		public const ushort UserInformationRequireAllSendersAreAuthenticated = 12432;

		// Token: 0x0400026B RID: 619
		public const ushort UserInformationResetPasswordOnNextLogon = 12433;

		// Token: 0x0400026C RID: 620
		public const ushort UserInformationRetainDeletedItemsFor = 12434;

		// Token: 0x0400026D RID: 621
		public const ushort UserInformationRetainDeletedItemsUntilBackup = 12435;

		// Token: 0x0400026E RID: 622
		public const ushort UserInformationRulesQuota = 12436;

		// Token: 0x0400026F RID: 623
		public const ushort UserInformationShouldUseDefaultRetentionPolicy = 12437;

		// Token: 0x04000270 RID: 624
		public const ushort UserInformationSimpleDisplayName = 12438;

		// Token: 0x04000271 RID: 625
		public const ushort UserInformationSingleItemRecoveryEnabled = 12439;

		// Token: 0x04000272 RID: 626
		public const ushort UserInformationStateOrProvince = 12440;

		// Token: 0x04000273 RID: 627
		public const ushort UserInformationStreetAddress = 12441;

		// Token: 0x04000274 RID: 628
		public const ushort UserInformationSubscriberAccessEnabled = 12442;

		// Token: 0x04000275 RID: 629
		public const ushort UserInformationTextEncodedORAddress = 12443;

		// Token: 0x04000276 RID: 630
		public const ushort UserInformationTextMessagingState = 12444;

		// Token: 0x04000277 RID: 631
		public const ushort UserInformationTimezone = 12445;

		// Token: 0x04000278 RID: 632
		public const ushort UserInformationUCSImListMigrationCompleted = 12446;

		// Token: 0x04000279 RID: 633
		public const ushort UserInformationUpgradeDetails = 12447;

		// Token: 0x0400027A RID: 634
		public const ushort UserInformationUpgradeMessage = 12448;

		// Token: 0x0400027B RID: 635
		public const ushort UserInformationUpgradeRequest = 12449;

		// Token: 0x0400027C RID: 636
		public const ushort UserInformationUpgradeStage = 12450;

		// Token: 0x0400027D RID: 637
		public const ushort UserInformationUpgradeStageTimeStamp = 12451;

		// Token: 0x0400027E RID: 638
		public const ushort UserInformationUpgradeStatus = 12452;

		// Token: 0x0400027F RID: 639
		public const ushort UserInformationUsageLocation = 12453;

		// Token: 0x04000280 RID: 640
		public const ushort UserInformationUseMapiRichTextFormat = 12454;

		// Token: 0x04000281 RID: 641
		public const ushort UserInformationUsePreferMessageFormat = 12455;

		// Token: 0x04000282 RID: 642
		public const ushort UserInformationUseUCCAuditConfig = 12456;

		// Token: 0x04000283 RID: 643
		public const ushort UserInformationWebPage = 12457;

		// Token: 0x04000284 RID: 644
		public const ushort UserInformationWhenMailboxCreated = 12458;

		// Token: 0x04000285 RID: 645
		public const ushort UserInformationWhenSoftDeleted = 12459;

		// Token: 0x04000286 RID: 646
		public const ushort UserInformationBirthdayPrecision = 12460;

		// Token: 0x04000287 RID: 647
		public const ushort UserInformationNameVersion = 12461;

		// Token: 0x04000288 RID: 648
		public const ushort UserInformationOptInUser = 12462;

		// Token: 0x04000289 RID: 649
		public const ushort UserInformationIsMigratedConsumerMailbox = 12463;

		// Token: 0x0400028A RID: 650
		public const ushort UserInformationMigrationDryRun = 12464;

		// Token: 0x0400028B RID: 651
		public const ushort UserInformationIsPremiumConsumerMailbox = 12465;

		// Token: 0x0400028C RID: 652
		public const ushort UserInformationAlternateSupportEmailAddresses = 12466;

		// Token: 0x0400028D RID: 653
		public const ushort UserInformationEmailAddresses = 12467;

		// Token: 0x0400028E RID: 654
		public const ushort UserInformationMapiHttpEnabled = 12502;

		// Token: 0x0400028F RID: 655
		public const ushort UserInformationMAPIBlockOutlookExternalConnectivity = 12503;

		// Token: 0x04000290 RID: 656
		public const ushort FormVersion = 13057;

		// Token: 0x04000291 RID: 657
		public const ushort FormCLSID = 13058;

		// Token: 0x04000292 RID: 658
		public const ushort FormContactName = 13059;

		// Token: 0x04000293 RID: 659
		public const ushort FormCategory = 13060;

		// Token: 0x04000294 RID: 660
		public const ushort FormCategorySub = 13061;

		// Token: 0x04000295 RID: 661
		public const ushort FormHidden = 13063;

		// Token: 0x04000296 RID: 662
		public const ushort FormDesignerName = 13064;

		// Token: 0x04000297 RID: 663
		public const ushort FormDesignerGuid = 13065;

		// Token: 0x04000298 RID: 664
		public const ushort FormMessageBehavior = 13066;

		// Token: 0x04000299 RID: 665
		public const ushort MessageTableTotalPages = 13313;

		// Token: 0x0400029A RID: 666
		public const ushort MessageTableAvailablePages = 13314;

		// Token: 0x0400029B RID: 667
		public const ushort OtherTablesTotalPages = 13315;

		// Token: 0x0400029C RID: 668
		public const ushort OtherTablesAvailablePages = 13316;

		// Token: 0x0400029D RID: 669
		public const ushort AttachmentTableTotalPages = 13317;

		// Token: 0x0400029E RID: 670
		public const ushort AttachmentTableAvailablePages = 13318;

		// Token: 0x0400029F RID: 671
		public const ushort MailboxTypeVersion = 13319;

		// Token: 0x040002A0 RID: 672
		public const ushort MailboxPartitionMailboxGuids = 13320;

		// Token: 0x040002A1 RID: 673
		public const ushort StoreSupportMask = 13325;

		// Token: 0x040002A2 RID: 674
		public const ushort StoreState = 13326;

		// Token: 0x040002A3 RID: 675
		public const ushort IPMSubtreeSearchKey = 13328;

		// Token: 0x040002A4 RID: 676
		public const ushort IPMOutboxSearchKey = 13329;

		// Token: 0x040002A5 RID: 677
		public const ushort IPMWastebasketSearchKey = 13330;

		// Token: 0x040002A6 RID: 678
		public const ushort IPMSentmailSearchKey = 13331;

		// Token: 0x040002A7 RID: 679
		public const ushort MdbProvider = 13332;

		// Token: 0x040002A8 RID: 680
		public const ushort ReceiveFolderSettings = 13333;

		// Token: 0x040002A9 RID: 681
		public const ushort LocalDirectoryEntryID = 13334;

		// Token: 0x040002AA RID: 682
		public const ushort ProviderDisplayIcon = 13335;

		// Token: 0x040002AB RID: 683
		public const ushort ProviderDisplayName = 13336;

		// Token: 0x040002AC RID: 684
		public const ushort ControlDataForCalendarRepairAssistant = 13344;

		// Token: 0x040002AD RID: 685
		public const ushort ControlDataForSharingPolicyAssistant = 13345;

		// Token: 0x040002AE RID: 686
		public const ushort ControlDataForElcAssistant = 13346;

		// Token: 0x040002AF RID: 687
		public const ushort ControlDataForTopNWordsAssistant = 13347;

		// Token: 0x040002B0 RID: 688
		public const ushort ControlDataForJunkEmailAssistant = 13348;

		// Token: 0x040002B1 RID: 689
		public const ushort ControlDataForCalendarSyncAssistant = 13349;

		// Token: 0x040002B2 RID: 690
		public const ushort ExternalSharingCalendarSubscriptionCount = 13350;

		// Token: 0x040002B3 RID: 691
		public const ushort ControlDataForUMReportingAssistant = 13351;

		// Token: 0x040002B4 RID: 692
		public const ushort HasUMReportData = 13352;

		// Token: 0x040002B5 RID: 693
		public const ushort InternetCalendarSubscriptionCount = 13353;

		// Token: 0x040002B6 RID: 694
		public const ushort ExternalSharingContactSubscriptionCount = 13354;

		// Token: 0x040002B7 RID: 695
		public const ushort JunkEmailSafeListDirty = 13355;

		// Token: 0x040002B8 RID: 696
		public const ushort IsTopNEnabled = 13356;

		// Token: 0x040002B9 RID: 697
		public const ushort LastSharingPolicyAppliedId = 13357;

		// Token: 0x040002BA RID: 698
		public const ushort LastSharingPolicyAppliedHash = 13358;

		// Token: 0x040002BB RID: 699
		public const ushort LastSharingPolicyAppliedTime = 13359;

		// Token: 0x040002BC RID: 700
		public const ushort OofScheduleStart = 13360;

		// Token: 0x040002BD RID: 701
		public const ushort OofScheduleEnd = 13361;

		// Token: 0x040002BE RID: 702
		public const ushort ControlDataForDirectoryProcessorAssistant = 13362;

		// Token: 0x040002BF RID: 703
		public const ushort NeedsDirectoryProcessor = 13363;

		// Token: 0x040002C0 RID: 704
		public const ushort RetentionQueryIds = 13364;

		// Token: 0x040002C1 RID: 705
		public const ushort RetentionQueryInfo = 13365;

		// Token: 0x040002C2 RID: 706
		public const ushort ControlDataForPublicFolderAssistant = 13367;

		// Token: 0x040002C3 RID: 707
		public const ushort ControlDataForInferenceTrainingAssistant = 13368;

		// Token: 0x040002C4 RID: 708
		public const ushort InferenceEnabled = 13369;

		// Token: 0x040002C5 RID: 709
		public const ushort ControlDataForContactLinkingAssistant = 13370;

		// Token: 0x040002C6 RID: 710
		public const ushort ContactLinking = 13371;

		// Token: 0x040002C7 RID: 711
		public const ushort ControlDataForOABGeneratorAssistant = 13372;

		// Token: 0x040002C8 RID: 712
		public const ushort ContactSaveVersion = 13373;

		// Token: 0x040002C9 RID: 713
		public const ushort ControlDataForOrgContactsSyncAssistant = 13374;

		// Token: 0x040002CA RID: 714
		public const ushort OrgContactsSyncTimestamp = 13375;

		// Token: 0x040002CB RID: 715
		public const ushort PushNotificationSubscriptionType = 13376;

		// Token: 0x040002CC RID: 716
		public const ushort OrgContactsSyncADWatermark = 13377;

		// Token: 0x040002CD RID: 717
		public const ushort ControlDataForInferenceDataCollectionAssistant = 13378;

		// Token: 0x040002CE RID: 718
		public const ushort InferenceDataCollectionProcessingState = 13379;

		// Token: 0x040002CF RID: 719
		public const ushort ControlDataForPeopleRelevanceAssistant = 13380;

		// Token: 0x040002D0 RID: 720
		public const ushort SiteMailboxInternalState = 13381;

		// Token: 0x040002D1 RID: 721
		public const ushort ControlDataForSiteMailboxAssistant = 13382;

		// Token: 0x040002D2 RID: 722
		public const ushort InferenceTrainingLastContentCount = 13383;

		// Token: 0x040002D3 RID: 723
		public const ushort InferenceTrainingLastAttemptTimestamp = 13384;

		// Token: 0x040002D4 RID: 724
		public const ushort InferenceTrainingLastSuccessTimestamp = 13385;

		// Token: 0x040002D5 RID: 725
		public const ushort InferenceUserCapabilityFlags = 13386;

		// Token: 0x040002D6 RID: 726
		public const ushort ControlDataForMailboxAssociationReplicationAssistant = 13387;

		// Token: 0x040002D7 RID: 727
		public const ushort MailboxAssociationNextReplicationTime = 13388;

		// Token: 0x040002D8 RID: 728
		public const ushort MailboxAssociationProcessingFlags = 13389;

		// Token: 0x040002D9 RID: 729
		public const ushort ControlDataForSharePointSignalStoreAssistant = 13390;

		// Token: 0x040002DA RID: 730
		public const ushort ControlDataForPeopleCentricTriageAssistant = 13391;

		// Token: 0x040002DB RID: 731
		public const ushort NotificationBrokerSubscriptions = 13392;

		// Token: 0x040002DC RID: 732
		public const ushort GroupMailboxPermissionsVersion = 13393;

		// Token: 0x040002DD RID: 733
		public const ushort ElcLastRunTotalProcessingTime = 13394;

		// Token: 0x040002DE RID: 734
		public const ushort ElcLastRunSubAssistantProcessingTime = 13395;

		// Token: 0x040002DF RID: 735
		public const ushort ElcLastRunUpdatedFolderCount = 13396;

		// Token: 0x040002E0 RID: 736
		public const ushort ElcLastRunTaggedFolderCount = 13397;

		// Token: 0x040002E1 RID: 737
		public const ushort ElcLastRunUpdatedItemCount = 13398;

		// Token: 0x040002E2 RID: 738
		public const ushort ElcLastRunTaggedWithArchiveItemCount = 13399;

		// Token: 0x040002E3 RID: 739
		public const ushort ElcLastRunTaggedWithExpiryItemCount = 13400;

		// Token: 0x040002E4 RID: 740
		public const ushort ElcLastRunDeletedFromRootItemCount = 13401;

		// Token: 0x040002E5 RID: 741
		public const ushort ElcLastRunDeletedFromDumpsterItemCount = 13402;

		// Token: 0x040002E6 RID: 742
		public const ushort ElcLastRunArchivedFromRootItemCount = 13403;

		// Token: 0x040002E7 RID: 743
		public const ushort ElcLastRunArchivedFromDumpsterItemCount = 13404;

		// Token: 0x040002E8 RID: 744
		public const ushort ScheduledISIntegLastFinished = 13405;

		// Token: 0x040002E9 RID: 745
		public const ushort ControlDataForSearchIndexRepairAssistant = 13406;

		// Token: 0x040002EA RID: 746
		public const ushort ELCLastSuccessTimestamp = 13407;

		// Token: 0x040002EB RID: 747
		public const ushort EventEmailReminderTimer = 13408;

		// Token: 0x040002EC RID: 748
		public const ushort InferenceTruthLoggingLastAttemptTimestamp = 13409;

		// Token: 0x040002ED RID: 749
		public const ushort InferenceTruthLoggingLastSuccessTimestamp = 13410;

		// Token: 0x040002EE RID: 750
		public const ushort ControlDataForGroupMailboxAssistant = 13411;

		// Token: 0x040002EF RID: 751
		public const ushort ItemsPendingUpgrade = 13412;

		// Token: 0x040002F0 RID: 752
		public const ushort ConsumerSharingCalendarSubscriptionCount = 13413;

		// Token: 0x040002F1 RID: 753
		public const ushort GroupMailboxGeneratedPhotoVersion = 13414;

		// Token: 0x040002F2 RID: 754
		public const ushort GroupMailboxGeneratedPhotoSignature = 13415;

		// Token: 0x040002F3 RID: 755
		public const ushort GroupMailboxExchangeResourcesPublishedVersion = 13416;

		// Token: 0x040002F4 RID: 756
		public const ushort ValidFolderMask = 13791;

		// Token: 0x040002F5 RID: 757
		public const ushort IPMSubtreeEntryId = 13792;

		// Token: 0x040002F6 RID: 758
		public const ushort IPMOutboxEntryId = 13794;

		// Token: 0x040002F7 RID: 759
		public const ushort IPMWastebasketEntryId = 13795;

		// Token: 0x040002F8 RID: 760
		public const ushort IPMSentmailEntryId = 13796;

		// Token: 0x040002F9 RID: 761
		public const ushort IPMViewsEntryId = 13797;

		// Token: 0x040002FA RID: 762
		public const ushort IPMCommonViewsEntryId = 13798;

		// Token: 0x040002FB RID: 763
		public const ushort IPMConversationsEntryId = 13804;

		// Token: 0x040002FC RID: 764
		public const ushort IPMAllItemsEntryId = 13806;

		// Token: 0x040002FD RID: 765
		public const ushort IPMSharingEntryId = 13807;

		// Token: 0x040002FE RID: 766
		public const ushort AdminDataEntryId = 13821;

		// Token: 0x040002FF RID: 767
		public const ushort UnsearchableItems = 13822;

		// Token: 0x04000300 RID: 768
		public const ushort ContainerFlags = 13824;

		// Token: 0x04000301 RID: 769
		public const ushort IPMFinderEntryId = 13824;

		// Token: 0x04000302 RID: 770
		public const ushort FolderType = 13825;

		// Token: 0x04000303 RID: 771
		public const ushort ContentCount = 13826;

		// Token: 0x04000304 RID: 772
		public const ushort ContentCountInt64 = 13826;

		// Token: 0x04000305 RID: 773
		public const ushort UnreadCount = 13827;

		// Token: 0x04000306 RID: 774
		public const ushort UnreadCountInt64 = 13827;

		// Token: 0x04000307 RID: 775
		public const ushort DetailsTable = 13829;

		// Token: 0x04000308 RID: 776
		public const ushort Search = 13831;

		// Token: 0x04000309 RID: 777
		public const ushort Selectable = 13833;

		// Token: 0x0400030A RID: 778
		public const ushort Subfolders = 13834;

		// Token: 0x0400030B RID: 779
		public const ushort FolderStatus = 13835;

		// Token: 0x0400030C RID: 780
		public const ushort AmbiguousNameResolution = 13836;

		// Token: 0x0400030D RID: 781
		public const ushort ContentsSortOrder = 13837;

		// Token: 0x0400030E RID: 782
		public const ushort ContainerHierarchy = 13838;

		// Token: 0x0400030F RID: 783
		public const ushort ContainerContents = 13839;

		// Token: 0x04000310 RID: 784
		public const ushort FolderAssociatedContents = 13840;

		// Token: 0x04000311 RID: 785
		public const ushort ContainerClass = 13843;

		// Token: 0x04000312 RID: 786
		public const ushort ContainerModifyVersion = 13844;

		// Token: 0x04000313 RID: 787
		public const ushort ABProviderId = 13845;

		// Token: 0x04000314 RID: 788
		public const ushort DefaultViewEntryId = 13846;

		// Token: 0x04000315 RID: 789
		public const ushort AssociatedContentCount = 13847;

		// Token: 0x04000316 RID: 790
		public const ushort AssociatedContentCountInt64 = 13847;

		// Token: 0x04000317 RID: 791
		public const ushort PackedNamedProps = 13852;

		// Token: 0x04000318 RID: 792
		public const ushort AllowAgeOut = 13855;

		// Token: 0x04000319 RID: 793
		public const ushort SearchFolderMsgCount = 13892;

		// Token: 0x0400031A RID: 794
		public const ushort PartOfContentIndexing = 13893;

		// Token: 0x0400031B RID: 795
		public const ushort OwnerLogonUserConfigurationCache = 13894;

		// Token: 0x0400031C RID: 796
		public const ushort SearchFolderAgeOutTimeout = 13895;

		// Token: 0x0400031D RID: 797
		public const ushort SearchFolderPopulationResult = 13896;

		// Token: 0x0400031E RID: 798
		public const ushort SearchFolderPopulationDiagnostics = 13897;

		// Token: 0x0400031F RID: 799
		public const ushort ConversationTopicHashEntries = 13920;

		// Token: 0x04000320 RID: 800
		public const ushort ContentAggregationFlags = 13967;

		// Token: 0x04000321 RID: 801
		public const ushort TransportRulesSnapshot = 13968;

		// Token: 0x04000322 RID: 802
		public const ushort TransportRulesSnapshotId = 13969;

		// Token: 0x04000323 RID: 803
		public const ushort CurrentIPMWasteBasketContainerEntryId = 14031;

		// Token: 0x04000324 RID: 804
		public const ushort IPMAppointmentEntryId = 14032;

		// Token: 0x04000325 RID: 805
		public const ushort IPMContactEntryId = 14033;

		// Token: 0x04000326 RID: 806
		public const ushort IPMJournalEntryId = 14034;

		// Token: 0x04000327 RID: 807
		public const ushort IPMNoteEntryId = 14035;

		// Token: 0x04000328 RID: 808
		public const ushort IPMTaskEntryId = 14036;

		// Token: 0x04000329 RID: 809
		public const ushort REMOnlineEntryId = 14037;

		// Token: 0x0400032A RID: 810
		public const ushort IPMOfflineEntryId = 14038;

		// Token: 0x0400032B RID: 811
		public const ushort IPMDraftsEntryId = 14039;

		// Token: 0x0400032C RID: 812
		public const ushort AdditionalRENEntryIds = 14040;

		// Token: 0x0400032D RID: 813
		public const ushort AdditionalRENEntryIdsExtended = 14041;

		// Token: 0x0400032E RID: 814
		public const ushort AdditionalRENEntryIdsExtendedMV = 14041;

		// Token: 0x0400032F RID: 815
		public const ushort ExtendedFolderFlags = 14042;

		// Token: 0x04000330 RID: 816
		public const ushort ContainerTimestamp = 14043;

		// Token: 0x04000331 RID: 817
		public const ushort AppointmentColorName = 14044;

		// Token: 0x04000332 RID: 818
		public const ushort INetUnread = 14045;

		// Token: 0x04000333 RID: 819
		public const ushort NetFolderFlags = 14046;

		// Token: 0x04000334 RID: 820
		public const ushort FolderWebViewInfo = 14047;

		// Token: 0x04000335 RID: 821
		public const ushort FolderWebViewInfoExtended = 14048;

		// Token: 0x04000336 RID: 822
		public const ushort FolderViewFlags = 14049;

		// Token: 0x04000337 RID: 823
		public const ushort FreeBusyEntryIds = 14052;

		// Token: 0x04000338 RID: 824
		public const ushort DefaultPostMsgClass = 14053;

		// Token: 0x04000339 RID: 825
		public const ushort DefaultPostDisplayName = 14054;

		// Token: 0x0400033A RID: 826
		public const ushort FolderViewList = 14059;

		// Token: 0x0400033B RID: 827
		public const ushort AgingPeriod = 14060;

		// Token: 0x0400033C RID: 828
		public const ushort AgingGranularity = 14062;

		// Token: 0x0400033D RID: 829
		public const ushort DefaultFoldersLocaleId = 14064;

		// Token: 0x0400033E RID: 830
		public const ushort InternalAccess = 14065;

		// Token: 0x0400033F RID: 831
		public const ushort AttachmentX400Parameters = 14080;

		// Token: 0x04000340 RID: 832
		public const ushort Content = 14081;

		// Token: 0x04000341 RID: 833
		public const ushort ContentObj = 14081;

		// Token: 0x04000342 RID: 834
		public const ushort AttachmentEncoding = 14082;

		// Token: 0x04000343 RID: 835
		public const ushort ContentId = 14083;

		// Token: 0x04000344 RID: 836
		public const ushort ContentType = 14084;

		// Token: 0x04000345 RID: 837
		public const ushort AttachMethod = 14085;

		// Token: 0x04000346 RID: 838
		public const ushort MimeUrl = 14087;

		// Token: 0x04000347 RID: 839
		public const ushort AttachmentPathName = 14088;

		// Token: 0x04000348 RID: 840
		public const ushort AttachRendering = 14089;

		// Token: 0x04000349 RID: 841
		public const ushort AttachTag = 14090;

		// Token: 0x0400034A RID: 842
		public const ushort RenderingPosition = 14091;

		// Token: 0x0400034B RID: 843
		public const ushort AttachTransportName = 14092;

		// Token: 0x0400034C RID: 844
		public const ushort AttachmentLongPathName = 14093;

		// Token: 0x0400034D RID: 845
		public const ushort AttachmentMimeTag = 14094;

		// Token: 0x0400034E RID: 846
		public const ushort AttachAdditionalInfo = 14095;

		// Token: 0x0400034F RID: 847
		public const ushort AttachmentMimeSequence = 14096;

		// Token: 0x04000350 RID: 848
		public const ushort AttachContentBase = 14097;

		// Token: 0x04000351 RID: 849
		public const ushort AttachContentId = 14098;

		// Token: 0x04000352 RID: 850
		public const ushort AttachContentLocation = 14099;

		// Token: 0x04000353 RID: 851
		public const ushort AttachmentFlags = 14100;

		// Token: 0x04000354 RID: 852
		public const ushort AttachDisposition = 14102;

		// Token: 0x04000355 RID: 853
		public const ushort AttachPayloadProviderGuidString = 14105;

		// Token: 0x04000356 RID: 854
		public const ushort AttachPayloadClass = 14106;

		// Token: 0x04000357 RID: 855
		public const ushort TextAttachmentCharset = 14107;

		// Token: 0x04000358 RID: 856
		public const ushort SyncEventSuppressGuid = 14464;

		// Token: 0x04000359 RID: 857
		public const ushort DisplayType = 14592;

		// Token: 0x0400035A RID: 858
		public const ushort TemplateId = 14594;

		// Token: 0x0400035B RID: 859
		public const ushort CapabilitiesTable = 14595;

		// Token: 0x0400035C RID: 860
		public const ushort PrimaryCapability = 14596;

		// Token: 0x0400035D RID: 861
		public const ushort EMSABDisplayTypeEx = 14597;

		// Token: 0x0400035E RID: 862
		public const ushort SmtpAddress = 14846;

		// Token: 0x0400035F RID: 863
		public const ushort EMSABDisplayNamePrintable = 14847;

		// Token: 0x04000360 RID: 864
		public const ushort SimpleDisplayName = 14847;

		// Token: 0x04000361 RID: 865
		public const ushort Account = 14848;

		// Token: 0x04000362 RID: 866
		public const ushort AlternateRecipient = 14849;

		// Token: 0x04000363 RID: 867
		public const ushort CallbackTelephoneNumber = 14850;

		// Token: 0x04000364 RID: 868
		public const ushort ConversionProhibited = 14851;

		// Token: 0x04000365 RID: 869
		public const ushort Generation = 14853;

		// Token: 0x04000366 RID: 870
		public const ushort GivenName = 14854;

		// Token: 0x04000367 RID: 871
		public const ushort GovernmentIDNumber = 14855;

		// Token: 0x04000368 RID: 872
		public const ushort BusinessTelephoneNumber = 14856;

		// Token: 0x04000369 RID: 873
		public const ushort HomeTelephoneNumber = 14857;

		// Token: 0x0400036A RID: 874
		public const ushort Initials = 14858;

		// Token: 0x0400036B RID: 875
		public const ushort Keyword = 14859;

		// Token: 0x0400036C RID: 876
		public const ushort Language = 14860;

		// Token: 0x0400036D RID: 877
		public const ushort Location = 14861;

		// Token: 0x0400036E RID: 878
		public const ushort MailPermission = 14862;

		// Token: 0x0400036F RID: 879
		public const ushort MHSCommonName = 14863;

		// Token: 0x04000370 RID: 880
		public const ushort OrganizationalIDNumber = 14864;

		// Token: 0x04000371 RID: 881
		public const ushort SurName = 14865;

		// Token: 0x04000372 RID: 882
		public const ushort OriginalEntryId = 14866;

		// Token: 0x04000373 RID: 883
		public const ushort OriginalDisplayName = 14867;

		// Token: 0x04000374 RID: 884
		public const ushort OriginalSearchKey = 14868;

		// Token: 0x04000375 RID: 885
		public const ushort PostalAddress = 14869;

		// Token: 0x04000376 RID: 886
		public const ushort CompanyName = 14870;

		// Token: 0x04000377 RID: 887
		public const ushort Title = 14871;

		// Token: 0x04000378 RID: 888
		public const ushort DepartmentName = 14872;

		// Token: 0x04000379 RID: 889
		public const ushort OfficeLocation = 14873;

		// Token: 0x0400037A RID: 890
		public const ushort PrimaryTelephoneNumber = 14874;

		// Token: 0x0400037B RID: 891
		public const ushort Business2TelephoneNumber = 14875;

		// Token: 0x0400037C RID: 892
		public const ushort Business2TelephoneNumberMv = 14875;

		// Token: 0x0400037D RID: 893
		public const ushort MobileTelephoneNumber = 14876;

		// Token: 0x0400037E RID: 894
		public const ushort RadioTelephoneNumber = 14877;

		// Token: 0x0400037F RID: 895
		public const ushort CarTelephoneNumber = 14878;

		// Token: 0x04000380 RID: 896
		public const ushort OtherTelephoneNumber = 14879;

		// Token: 0x04000381 RID: 897
		public const ushort TransmitableDisplayName = 14880;

		// Token: 0x04000382 RID: 898
		public const ushort PagerTelephoneNumber = 14881;

		// Token: 0x04000383 RID: 899
		public const ushort UserCertificate = 14882;

		// Token: 0x04000384 RID: 900
		public const ushort PrimaryFaxNumber = 14883;

		// Token: 0x04000385 RID: 901
		public const ushort BusinessFaxNumber = 14884;

		// Token: 0x04000386 RID: 902
		public const ushort HomeFaxNumber = 14885;

		// Token: 0x04000387 RID: 903
		public const ushort Country = 14886;

		// Token: 0x04000388 RID: 904
		public const ushort Locality = 14887;

		// Token: 0x04000389 RID: 905
		public const ushort StateOrProvince = 14888;

		// Token: 0x0400038A RID: 906
		public const ushort StreetAddress = 14889;

		// Token: 0x0400038B RID: 907
		public const ushort PostalCode = 14890;

		// Token: 0x0400038C RID: 908
		public const ushort PostOfficeBox = 14891;

		// Token: 0x0400038D RID: 909
		public const ushort TelexNumber = 14892;

		// Token: 0x0400038E RID: 910
		public const ushort ISDNNumber = 14893;

		// Token: 0x0400038F RID: 911
		public const ushort AssistantTelephoneNumber = 14894;

		// Token: 0x04000390 RID: 912
		public const ushort Home2TelephoneNumber = 14895;

		// Token: 0x04000391 RID: 913
		public const ushort Home2TelephoneNumberMv = 14895;

		// Token: 0x04000392 RID: 914
		public const ushort Assistant = 14896;

		// Token: 0x04000393 RID: 915
		public const ushort SendRichInfo = 14912;

		// Token: 0x04000394 RID: 916
		public const ushort WeddingAnniversary = 14913;

		// Token: 0x04000395 RID: 917
		public const ushort Birthday = 14914;

		// Token: 0x04000396 RID: 918
		public const ushort Hobbies = 14915;

		// Token: 0x04000397 RID: 919
		public const ushort MiddleName = 14916;

		// Token: 0x04000398 RID: 920
		public const ushort DisplayNamePrefix = 14917;

		// Token: 0x04000399 RID: 921
		public const ushort Profession = 14918;

		// Token: 0x0400039A RID: 922
		public const ushort ReferredByName = 14919;

		// Token: 0x0400039B RID: 923
		public const ushort SpouseName = 14920;

		// Token: 0x0400039C RID: 924
		public const ushort ComputerNetworkName = 14921;

		// Token: 0x0400039D RID: 925
		public const ushort CustomerId = 14922;

		// Token: 0x0400039E RID: 926
		public const ushort TTYTDDPhoneNumber = 14923;

		// Token: 0x0400039F RID: 927
		public const ushort FTPSite = 14924;

		// Token: 0x040003A0 RID: 928
		public const ushort Gender = 14925;

		// Token: 0x040003A1 RID: 929
		public const ushort ManagerName = 14926;

		// Token: 0x040003A2 RID: 930
		public const ushort NickName = 14927;

		// Token: 0x040003A3 RID: 931
		public const ushort PersonalHomePage = 14928;

		// Token: 0x040003A4 RID: 932
		public const ushort BusinessHomePage = 14929;

		// Token: 0x040003A5 RID: 933
		public const ushort ContactVersion = 14930;

		// Token: 0x040003A6 RID: 934
		public const ushort ContactEntryIds = 14931;

		// Token: 0x040003A7 RID: 935
		public const ushort ContactAddressTypes = 14932;

		// Token: 0x040003A8 RID: 936
		public const ushort ContactDefaultAddressIndex = 14933;

		// Token: 0x040003A9 RID: 937
		public const ushort ContactEmailAddress = 14934;

		// Token: 0x040003AA RID: 938
		public const ushort CompanyMainPhoneNumber = 14935;

		// Token: 0x040003AB RID: 939
		public const ushort ChildrensNames = 14936;

		// Token: 0x040003AC RID: 940
		public const ushort HomeAddressCity = 14937;

		// Token: 0x040003AD RID: 941
		public const ushort HomeAddressCountry = 14938;

		// Token: 0x040003AE RID: 942
		public const ushort HomeAddressPostalCode = 14939;

		// Token: 0x040003AF RID: 943
		public const ushort HomeAddressStateOrProvince = 14940;

		// Token: 0x040003B0 RID: 944
		public const ushort HomeAddressStreet = 14941;

		// Token: 0x040003B1 RID: 945
		public const ushort HomeAddressPostOfficeBox = 14942;

		// Token: 0x040003B2 RID: 946
		public const ushort OtherAddressCity = 14943;

		// Token: 0x040003B3 RID: 947
		public const ushort OtherAddressCountry = 14944;

		// Token: 0x040003B4 RID: 948
		public const ushort OtherAddressPostalCode = 14945;

		// Token: 0x040003B5 RID: 949
		public const ushort OtherAddressStateOrProvince = 14946;

		// Token: 0x040003B6 RID: 950
		public const ushort OtherAddressStreet = 14947;

		// Token: 0x040003B7 RID: 951
		public const ushort OtherAddressPostOfficeBox = 14948;

		// Token: 0x040003B8 RID: 952
		public const ushort UserX509CertificateABSearchPath = 14960;

		// Token: 0x040003B9 RID: 953
		public const ushort SendInternetEncoding = 14961;

		// Token: 0x040003BA RID: 954
		public const ushort PartnerNetworkId = 14966;

		// Token: 0x040003BB RID: 955
		public const ushort PartnerNetworkUserId = 14967;

		// Token: 0x040003BC RID: 956
		public const ushort PartnerNetworkThumbnailPhotoUrl = 14968;

		// Token: 0x040003BD RID: 957
		public const ushort PartnerNetworkProfilePhotoUrl = 14969;

		// Token: 0x040003BE RID: 958
		public const ushort PartnerNetworkContactType = 14970;

		// Token: 0x040003BF RID: 959
		public const ushort RelevanceScore = 14971;

		// Token: 0x040003C0 RID: 960
		public const ushort IsDistributionListContact = 14972;

		// Token: 0x040003C1 RID: 961
		public const ushort IsPromotedContact = 14973;

		// Token: 0x040003C2 RID: 962
		public const ushort OrgUnitName = 15358;

		// Token: 0x040003C3 RID: 963
		public const ushort OrganizationName = 15359;

		// Token: 0x040003C4 RID: 964
		public const ushort TestBlobProperty = 15616;

		// Token: 0x040003C5 RID: 965
		public const ushort StoreProviders = 15616;

		// Token: 0x040003C6 RID: 966
		public const ushort AddressBookProviders = 15617;

		// Token: 0x040003C7 RID: 967
		public const ushort TransportProviders = 15618;

		// Token: 0x040003C8 RID: 968
		public const ushort FilteringHooks = 15624;

		// Token: 0x040003C9 RID: 969
		public const ushort ServiceName = 15625;

		// Token: 0x040003CA RID: 970
		public const ushort ServiceDLLName = 15626;

		// Token: 0x040003CB RID: 971
		public const ushort ServiceEntryName = 15627;

		// Token: 0x040003CC RID: 972
		public const ushort ServiceUid = 15628;

		// Token: 0x040003CD RID: 973
		public const ushort ServiceExtraUid = 15629;

		// Token: 0x040003CE RID: 974
		public const ushort Services = 15630;

		// Token: 0x040003CF RID: 975
		public const ushort ServiceSupportFiles = 15631;

		// Token: 0x040003D0 RID: 976
		public const ushort ServiceDeleteFiles = 15632;

		// Token: 0x040003D1 RID: 977
		public const ushort ProfileName = 15634;

		// Token: 0x040003D2 RID: 978
		public const ushort AdminSecurityDescriptor = 15649;

		// Token: 0x040003D3 RID: 979
		public const ushort Win32NTSecurityDescriptor = 15650;

		// Token: 0x040003D4 RID: 980
		public const ushort NonWin32ACL = 15651;

		// Token: 0x040003D5 RID: 981
		public const ushort ItemLevelACL = 15652;

		// Token: 0x040003D6 RID: 982
		public const ushort ICSGid = 15662;

		// Token: 0x040003D7 RID: 983
		public const ushort SystemFolderFlags = 15663;

		// Token: 0x040003D8 RID: 984
		public const ushort MaterializedRestrictionSearchRoot = 15772;

		// Token: 0x040003D9 RID: 985
		public const ushort ScheduledISIntegCorruptionCount = 15773;

		// Token: 0x040003DA RID: 986
		public const ushort ScheduledISIntegExecutionTime = 15774;

		// Token: 0x040003DB RID: 987
		public const ushort MailboxPartitionNumber = 15775;

		// Token: 0x040003DC RID: 988
		public const ushort MailboxNumberInternal = 15776;

		// Token: 0x040003DD RID: 989
		public const ushort QueryCriteriaInternal = 15777;

		// Token: 0x040003DE RID: 990
		public const ushort LastQuotaNotificationTime = 15778;

		// Token: 0x040003DF RID: 991
		public const ushort PropertyPromotionInProgressHiddenItems = 15779;

		// Token: 0x040003E0 RID: 992
		public const ushort PropertyPromotionInProgressNormalItems = 15780;

		// Token: 0x040003E1 RID: 993
		public const ushort VirtualParentDisplay = 15781;

		// Token: 0x040003E2 RID: 994
		public const ushort MailboxTypeDetail = 15782;

		// Token: 0x040003E3 RID: 995
		public const ushort InternalTenantHint = 15783;

		// Token: 0x040003E4 RID: 996
		public const ushort InternalConversationIndexTracking = 15784;

		// Token: 0x040003E5 RID: 997
		public const ushort InternalConversationIndex = 15785;

		// Token: 0x040003E6 RID: 998
		public const ushort ConversationItemConversationId = 15786;

		// Token: 0x040003E7 RID: 999
		public const ushort VirtualUnreadMessageCount = 15787;

		// Token: 0x040003E8 RID: 1000
		public const ushort VirtualIsRead = 15788;

		// Token: 0x040003E9 RID: 1001
		public const ushort IsReadColumn = 15789;

		// Token: 0x040003EA RID: 1002
		public const ushort TenantHint = 15790;

		// Token: 0x040003EB RID: 1003
		public const ushort Internal9ByteChangeNumber = 15791;

		// Token: 0x040003EC RID: 1004
		public const ushort Internal9ByteReadCnNew = 15792;

		// Token: 0x040003ED RID: 1005
		public const ushort CategoryHeaderLevelStub1 = 15793;

		// Token: 0x040003EE RID: 1006
		public const ushort CategoryHeaderLevelStub2 = 15794;

		// Token: 0x040003EF RID: 1007
		public const ushort CategoryHeaderLevelStub3 = 15795;

		// Token: 0x040003F0 RID: 1008
		public const ushort CategoryHeaderAggregateProp0 = 15796;

		// Token: 0x040003F1 RID: 1009
		public const ushort CategoryHeaderAggregateProp1 = 15797;

		// Token: 0x040003F2 RID: 1010
		public const ushort CategoryHeaderAggregateProp2 = 15798;

		// Token: 0x040003F3 RID: 1011
		public const ushort CategoryHeaderAggregateProp3 = 15799;

		// Token: 0x040003F4 RID: 1012
		public const ushort MaintenanceId = 15803;

		// Token: 0x040003F5 RID: 1013
		public const ushort MailboxType = 15804;

		// Token: 0x040003F6 RID: 1014
		public const ushort MessageFlagsActual = 15805;

		// Token: 0x040003F7 RID: 1015
		public const ushort InternalChangeKey = 15806;

		// Token: 0x040003F8 RID: 1016
		public const ushort InternalSourceKey = 15807;

		// Token: 0x040003F9 RID: 1017
		public const ushort CorrelationId = 15825;

		// Token: 0x040003FA RID: 1018
		public const ushort IdentityDisplay = 15872;

		// Token: 0x040003FB RID: 1019
		public const ushort IdentityEntryId = 15873;

		// Token: 0x040003FC RID: 1020
		public const ushort ResourceMethods = 15874;

		// Token: 0x040003FD RID: 1021
		public const ushort ResourceType = 15875;

		// Token: 0x040003FE RID: 1022
		public const ushort StatusCode = 15876;

		// Token: 0x040003FF RID: 1023
		public const ushort IdentitySearchKey = 15877;

		// Token: 0x04000400 RID: 1024
		public const ushort OwnStoreEntryId = 15878;

		// Token: 0x04000401 RID: 1025
		public const ushort ResourcePath = 15879;

		// Token: 0x04000402 RID: 1026
		public const ushort StatusString = 15880;

		// Token: 0x04000403 RID: 1027
		public const ushort X400DeferredDeliveryCancel = 15881;

		// Token: 0x04000404 RID: 1028
		public const ushort HeaderFolderEntryId = 15882;

		// Token: 0x04000405 RID: 1029
		public const ushort RemoteProgress = 15883;

		// Token: 0x04000406 RID: 1030
		public const ushort RemoteProgressText = 15884;

		// Token: 0x04000407 RID: 1031
		public const ushort RemoteValidateOK = 15885;

		// Token: 0x04000408 RID: 1032
		public const ushort ControlFlags = 16128;

		// Token: 0x04000409 RID: 1033
		public const ushort ControlStructure = 16129;

		// Token: 0x0400040A RID: 1034
		public const ushort ControlType = 16130;

		// Token: 0x0400040B RID: 1035
		public const ushort DeltaX = 16131;

		// Token: 0x0400040C RID: 1036
		public const ushort DeltaY = 16132;

		// Token: 0x0400040D RID: 1037
		public const ushort XPos = 16133;

		// Token: 0x0400040E RID: 1038
		public const ushort YPos = 16134;

		// Token: 0x0400040F RID: 1039
		public const ushort ControlId = 16135;

		// Token: 0x04000410 RID: 1040
		public const ushort InitialDetailsPane = 16136;

		// Token: 0x04000411 RID: 1041
		public const ushort AttachmentId = 16264;

		// Token: 0x04000412 RID: 1042
		public const ushort AttachmentIdBin = 16264;

		// Token: 0x04000413 RID: 1043
		public const ushort VID = 16264;

		// Token: 0x04000414 RID: 1044
		public const ushort GVid = 16265;

		// Token: 0x04000415 RID: 1045
		public const ushort GDID = 16266;

		// Token: 0x04000416 RID: 1046
		public const ushort XVid = 16277;

		// Token: 0x04000417 RID: 1047
		public const ushort GDefVid = 16278;

		// Token: 0x04000418 RID: 1048
		public const ushort PrimaryMailboxOverQuota = 16322;

		// Token: 0x04000419 RID: 1049
		public const ushort ReplicaChangeNumber = 16328;

		// Token: 0x0400041A RID: 1050
		public const ushort LastConflict = 16329;

		// Token: 0x0400041B RID: 1051
		public const ushort RMI = 16340;

		// Token: 0x0400041C RID: 1052
		public const ushort InternalPostReply = 16341;

		// Token: 0x0400041D RID: 1053
		public const ushort NTSDModificationTime = 16342;

		// Token: 0x0400041E RID: 1054
		public const ushort ACLDataChecksum = 16343;

		// Token: 0x0400041F RID: 1055
		public const ushort PreviewUnread = 16344;

		// Token: 0x04000420 RID: 1056
		public const ushort Preview = 16345;

		// Token: 0x04000421 RID: 1057
		public const ushort InternetCPID = 16350;

		// Token: 0x04000422 RID: 1058
		public const ushort AutoResponseSuppress = 16351;

		// Token: 0x04000423 RID: 1059
		public const ushort ACLData = 16352;

		// Token: 0x04000424 RID: 1060
		public const ushort ACLTable = 16352;

		// Token: 0x04000425 RID: 1061
		public const ushort RulesData = 16353;

		// Token: 0x04000426 RID: 1062
		public const ushort RulesTable = 16353;

		// Token: 0x04000427 RID: 1063
		public const ushort OofHistory = 16355;

		// Token: 0x04000428 RID: 1064
		public const ushort DesignInProgress = 16356;

		// Token: 0x04000429 RID: 1065
		public const ushort SecureOrigination = 16357;

		// Token: 0x0400042A RID: 1066
		public const ushort PublishInAddressBook = 16358;

		// Token: 0x0400042B RID: 1067
		public const ushort ResolveMethod = 16359;

		// Token: 0x0400042C RID: 1068
		public const ushort AddressBookDisplayName = 16360;

		// Token: 0x0400042D RID: 1069
		public const ushort EFormsLocaleId = 16361;

		// Token: 0x0400042E RID: 1070
		public const ushort HasDAMs = 16362;

		// Token: 0x0400042F RID: 1071
		public const ushort DeferredSendNumber = 16363;

		// Token: 0x04000430 RID: 1072
		public const ushort DeferredSendUnits = 16364;

		// Token: 0x04000431 RID: 1073
		public const ushort ExpiryNumber = 16365;

		// Token: 0x04000432 RID: 1074
		public const ushort ExpiryUnits = 16366;

		// Token: 0x04000433 RID: 1075
		public const ushort DeferredSendTime = 16367;

		// Token: 0x04000434 RID: 1076
		public const ushort BackfillTimeout = 16368;

		// Token: 0x04000435 RID: 1077
		public const ushort MessageLocaleId = 16369;

		// Token: 0x04000436 RID: 1078
		public const ushort RuleTriggerHistory = 16370;

		// Token: 0x04000437 RID: 1079
		public const ushort MoveToStoreEid = 16371;

		// Token: 0x04000438 RID: 1080
		public const ushort MoveToFolderEid = 16372;

		// Token: 0x04000439 RID: 1081
		public const ushort StorageQuotaLimit = 16373;

		// Token: 0x0400043A RID: 1082
		public const ushort ExcessStorageUsed = 16374;

		// Token: 0x0400043B RID: 1083
		public const ushort ServerGeneratingQuotaMsg = 16375;

		// Token: 0x0400043C RID: 1084
		public const ushort CreatorName = 16376;

		// Token: 0x0400043D RID: 1085
		public const ushort CreatorEntryId = 16377;

		// Token: 0x0400043E RID: 1086
		public const ushort LastModifierName = 16378;

		// Token: 0x0400043F RID: 1087
		public const ushort LastModifierEntryId = 16379;

		// Token: 0x04000440 RID: 1088
		public const ushort MessageCodePage = 16381;

		// Token: 0x04000441 RID: 1089
		public const ushort QuotaType = 16382;

		// Token: 0x04000442 RID: 1090
		public const ushort ExtendedACLData = 16382;

		// Token: 0x04000443 RID: 1091
		public const ushort RulesSize = 16383;

		// Token: 0x04000444 RID: 1092
		public const ushort IsPublicFolderQuotaMessage = 16383;

		// Token: 0x04000445 RID: 1093
		public const ushort NewAttach = 16384;

		// Token: 0x04000446 RID: 1094
		public const ushort StartEmbed = 16385;

		// Token: 0x04000447 RID: 1095
		public const ushort EndEmbed = 16386;

		// Token: 0x04000448 RID: 1096
		public const ushort StartRecip = 16387;

		// Token: 0x04000449 RID: 1097
		public const ushort EndRecip = 16388;

		// Token: 0x0400044A RID: 1098
		public const ushort EndCcRecip = 16389;

		// Token: 0x0400044B RID: 1099
		public const ushort EndBccRecip = 16390;

		// Token: 0x0400044C RID: 1100
		public const ushort EndP1Recip = 16391;

		// Token: 0x0400044D RID: 1101
		public const ushort DNPrefix = 16392;

		// Token: 0x0400044E RID: 1102
		public const ushort StartTopFolder = 16393;

		// Token: 0x0400044F RID: 1103
		public const ushort StartSubFolder = 16394;

		// Token: 0x04000450 RID: 1104
		public const ushort EndFolder = 16395;

		// Token: 0x04000451 RID: 1105
		public const ushort StartMessage = 16396;

		// Token: 0x04000452 RID: 1106
		public const ushort EndMessage = 16397;

		// Token: 0x04000453 RID: 1107
		public const ushort EndAttach = 16398;

		// Token: 0x04000454 RID: 1108
		public const ushort EcWarning = 16399;

		// Token: 0x04000455 RID: 1109
		public const ushort StartFAIMessage = 16400;

		// Token: 0x04000456 RID: 1110
		public const ushort NewFXFolder = 16401;

		// Token: 0x04000457 RID: 1111
		public const ushort IncrSyncChange = 16402;

		// Token: 0x04000458 RID: 1112
		public const ushort IncrSyncDelete = 16403;

		// Token: 0x04000459 RID: 1113
		public const ushort IncrSyncEnd = 16404;

		// Token: 0x0400045A RID: 1114
		public const ushort IncrSyncMessage = 16405;

		// Token: 0x0400045B RID: 1115
		public const ushort FastTransferDelProp = 16406;

		// Token: 0x0400045C RID: 1116
		public const ushort IdsetGiven = 16407;

		// Token: 0x0400045D RID: 1117
		public const ushort IdsetGivenInt32 = 16407;

		// Token: 0x0400045E RID: 1118
		public const ushort FastTransferErrorInfo = 16408;

		// Token: 0x0400045F RID: 1119
		public const ushort SenderFlags = 16409;

		// Token: 0x04000460 RID: 1120
		public const ushort SentRepresentingFlags = 16410;

		// Token: 0x04000461 RID: 1121
		public const ushort RcvdByFlags = 16411;

		// Token: 0x04000462 RID: 1122
		public const ushort RcvdRepresentingFlags = 16412;

		// Token: 0x04000463 RID: 1123
		public const ushort OriginalSenderFlags = 16413;

		// Token: 0x04000464 RID: 1124
		public const ushort OriginalSentRepresentingFlags = 16414;

		// Token: 0x04000465 RID: 1125
		public const ushort ReportFlags = 16415;

		// Token: 0x04000466 RID: 1126
		public const ushort ReadReceiptFlags = 16416;

		// Token: 0x04000467 RID: 1127
		public const ushort SoftDeletes = 16417;

		// Token: 0x04000468 RID: 1128
		public const ushort CreatorAddressType = 16418;

		// Token: 0x04000469 RID: 1129
		public const ushort CreatorEmailAddr = 16419;

		// Token: 0x0400046A RID: 1130
		public const ushort LastModifierAddressType = 16420;

		// Token: 0x0400046B RID: 1131
		public const ushort LastModifierEmailAddr = 16421;

		// Token: 0x0400046C RID: 1132
		public const ushort ReportAddressType = 16422;

		// Token: 0x0400046D RID: 1133
		public const ushort ReportEmailAddress = 16423;

		// Token: 0x0400046E RID: 1134
		public const ushort ReportDisplayName = 16424;

		// Token: 0x0400046F RID: 1135
		public const ushort ReadReceiptAddressType = 16425;

		// Token: 0x04000470 RID: 1136
		public const ushort ReadReceiptEmailAddress = 16426;

		// Token: 0x04000471 RID: 1137
		public const ushort ReadReceiptDisplayName = 16427;

		// Token: 0x04000472 RID: 1138
		public const ushort IdsetRead = 16429;

		// Token: 0x04000473 RID: 1139
		public const ushort IdsetUnread = 16430;

		// Token: 0x04000474 RID: 1140
		public const ushort IncrSyncRead = 16431;

		// Token: 0x04000475 RID: 1141
		public const ushort SenderSimpleDisplayName = 16432;

		// Token: 0x04000476 RID: 1142
		public const ushort SentRepresentingSimpleDisplayName = 16433;

		// Token: 0x04000477 RID: 1143
		public const ushort OriginalSenderSimpleDisplayName = 16434;

		// Token: 0x04000478 RID: 1144
		public const ushort OriginalSentRepresentingSimpleDisplayName = 16435;

		// Token: 0x04000479 RID: 1145
		public const ushort ReceivedBySimpleDisplayName = 16436;

		// Token: 0x0400047A RID: 1146
		public const ushort ReceivedRepresentingSimpleDisplayName = 16437;

		// Token: 0x0400047B RID: 1147
		public const ushort ReadReceiptSimpleDisplayName = 16438;

		// Token: 0x0400047C RID: 1148
		public const ushort ReportSimpleDisplayName = 16439;

		// Token: 0x0400047D RID: 1149
		public const ushort CreatorSimpleDisplayName = 16440;

		// Token: 0x0400047E RID: 1150
		public const ushort LastModifierSimpleDisplayName = 16441;

		// Token: 0x0400047F RID: 1151
		public const ushort IncrSyncStateBegin = 16442;

		// Token: 0x04000480 RID: 1152
		public const ushort IncrSyncStateEnd = 16443;

		// Token: 0x04000481 RID: 1153
		public const ushort IncrSyncImailStream = 16444;

		// Token: 0x04000482 RID: 1154
		public const ushort SenderOrgAddressType = 16447;

		// Token: 0x04000483 RID: 1155
		public const ushort SenderOrgEmailAddr = 16448;

		// Token: 0x04000484 RID: 1156
		public const ushort SentRepresentingOrgAddressType = 16449;

		// Token: 0x04000485 RID: 1157
		public const ushort SentRepresentingOrgEmailAddr = 16450;

		// Token: 0x04000486 RID: 1158
		public const ushort OriginalSenderOrgAddressType = 16451;

		// Token: 0x04000487 RID: 1159
		public const ushort OriginalSenderOrgEmailAddr = 16452;

		// Token: 0x04000488 RID: 1160
		public const ushort OriginalSentRepresentingOrgAddressType = 16453;

		// Token: 0x04000489 RID: 1161
		public const ushort OriginalSentRepresentingOrgEmailAddr = 16454;

		// Token: 0x0400048A RID: 1162
		public const ushort RcvdByOrgAddressType = 16455;

		// Token: 0x0400048B RID: 1163
		public const ushort RcvdByOrgEmailAddr = 16456;

		// Token: 0x0400048C RID: 1164
		public const ushort RcvdRepresentingOrgAddressType = 16457;

		// Token: 0x0400048D RID: 1165
		public const ushort RcvdRepresentingOrgEmailAddr = 16458;

		// Token: 0x0400048E RID: 1166
		public const ushort ReadReceiptOrgAddressType = 16459;

		// Token: 0x0400048F RID: 1167
		public const ushort ReadReceiptOrgEmailAddr = 16460;

		// Token: 0x04000490 RID: 1168
		public const ushort ReportOrgAddressType = 16461;

		// Token: 0x04000491 RID: 1169
		public const ushort ReportOrgEmailAddr = 16462;

		// Token: 0x04000492 RID: 1170
		public const ushort CreatorOrgAddressType = 16463;

		// Token: 0x04000493 RID: 1171
		public const ushort CreatorOrgEmailAddr = 16464;

		// Token: 0x04000494 RID: 1172
		public const ushort LastModifierOrgAddressType = 16465;

		// Token: 0x04000495 RID: 1173
		public const ushort LastModifierOrgEmailAddr = 16466;

		// Token: 0x04000496 RID: 1174
		public const ushort OriginatorOrgAddressType = 16467;

		// Token: 0x04000497 RID: 1175
		public const ushort OriginatorOrgEmailAddr = 16468;

		// Token: 0x04000498 RID: 1176
		public const ushort ReportDestinationOrgEmailType = 16469;

		// Token: 0x04000499 RID: 1177
		public const ushort ReportDestinationOrgEmailAddr = 16470;

		// Token: 0x0400049A RID: 1178
		public const ushort OriginalAuthorOrgAddressType = 16471;

		// Token: 0x0400049B RID: 1179
		public const ushort OriginalAuthorOrgEmailAddr = 16472;

		// Token: 0x0400049C RID: 1180
		public const ushort CreatorFlags = 16473;

		// Token: 0x0400049D RID: 1181
		public const ushort LastModifierFlags = 16474;

		// Token: 0x0400049E RID: 1182
		public const ushort OriginatorFlags = 16475;

		// Token: 0x0400049F RID: 1183
		public const ushort ReportDestinationFlags = 16476;

		// Token: 0x040004A0 RID: 1184
		public const ushort OriginalAuthorFlags = 16477;

		// Token: 0x040004A1 RID: 1185
		public const ushort OriginatorSimpleDisplayName = 16478;

		// Token: 0x040004A2 RID: 1186
		public const ushort ReportDestinationSimpleDisplayName = 16479;

		// Token: 0x040004A3 RID: 1187
		public const ushort OriginalAuthorSimpleDispName = 16480;

		// Token: 0x040004A4 RID: 1188
		public const ushort OriginatorSearchKey = 16481;

		// Token: 0x040004A5 RID: 1189
		public const ushort ReportDestinationAddressType = 16482;

		// Token: 0x040004A6 RID: 1190
		public const ushort ReportDestinationEmailAddress = 16483;

		// Token: 0x040004A7 RID: 1191
		public const ushort ReportDestinationSearchKey = 16484;

		// Token: 0x040004A8 RID: 1192
		public const ushort IncrSyncImailStreamContinue = 16486;

		// Token: 0x040004A9 RID: 1193
		public const ushort IncrSyncImailStreamCancel = 16487;

		// Token: 0x040004AA RID: 1194
		public const ushort IncrSyncImailStream2Continue = 16497;

		// Token: 0x040004AB RID: 1195
		public const ushort IncrSyncProgressMode = 16500;

		// Token: 0x040004AC RID: 1196
		public const ushort SyncProgressPerMsg = 16501;

		// Token: 0x040004AD RID: 1197
		public const ushort ContentFilterSCL = 16502;

		// Token: 0x040004AE RID: 1198
		public const ushort IncrSyncMsgPartial = 16506;

		// Token: 0x040004AF RID: 1199
		public const ushort IncrSyncGroupInfo = 16507;

		// Token: 0x040004B0 RID: 1200
		public const ushort IncrSyncGroupId = 16508;

		// Token: 0x040004B1 RID: 1201
		public const ushort IncrSyncChangePartial = 16509;

		// Token: 0x040004B2 RID: 1202
		public const ushort HierRev = 16514;

		// Token: 0x040004B3 RID: 1203
		public const ushort ContentFilterPCL = 16516;

		// Token: 0x040004B4 RID: 1204
		public const ushort DeliverAsRead = 22534;

		// Token: 0x040004B5 RID: 1205
		public const ushort InetMailOverrideFormat = 22786;

		// Token: 0x040004B6 RID: 1206
		public const ushort MessageEditorFormat = 22793;

		// Token: 0x040004B7 RID: 1207
		public const ushort SenderSMTPAddressXSO = 23809;

		// Token: 0x040004B8 RID: 1208
		public const ushort SentRepresentingSMTPAddressXSO = 23810;

		// Token: 0x040004B9 RID: 1209
		public const ushort OriginalSenderSMTPAddressXSO = 23811;

		// Token: 0x040004BA RID: 1210
		public const ushort OriginalSentRepresentingSMTPAddressXSO = 23812;

		// Token: 0x040004BB RID: 1211
		public const ushort ReadReceiptSMTPAddressXSO = 23813;

		// Token: 0x040004BC RID: 1212
		public const ushort OriginalAuthorSMTPAddressXSO = 23814;

		// Token: 0x040004BD RID: 1213
		public const ushort ReceivedBySMTPAddressXSO = 23815;

		// Token: 0x040004BE RID: 1214
		public const ushort ReceivedRepresentingSMTPAddressXSO = 23816;

		// Token: 0x040004BF RID: 1215
		public const ushort RecipientOrder = 24543;

		// Token: 0x040004C0 RID: 1216
		public const ushort RecipientSipUri = 24549;

		// Token: 0x040004C1 RID: 1217
		public const ushort RecipientDisplayName = 24566;

		// Token: 0x040004C2 RID: 1218
		public const ushort RecipientEntryId = 24567;

		// Token: 0x040004C3 RID: 1219
		public const ushort RecipientFlags = 24573;

		// Token: 0x040004C4 RID: 1220
		public const ushort RecipientTrackStatus = 24575;

		// Token: 0x040004C5 RID: 1221
		public const ushort DotStuffState = 24577;

		// Token: 0x040004C6 RID: 1222
		public const ushort InternetMessageIdHash = 25088;

		// Token: 0x040004C7 RID: 1223
		public const ushort ConversationTopicHash = 25089;

		// Token: 0x040004C8 RID: 1224
		public const ushort MimeSkeleton = 25840;

		// Token: 0x040004C9 RID: 1225
		public const ushort ReplyTemplateId = 26050;

		// Token: 0x040004CA RID: 1226
		public const ushort SecureSubmitFlags = 26054;

		// Token: 0x040004CB RID: 1227
		public const ushort SourceKey = 26080;

		// Token: 0x040004CC RID: 1228
		public const ushort ParentSourceKey = 26081;

		// Token: 0x040004CD RID: 1229
		public const ushort ChangeKey = 26082;

		// Token: 0x040004CE RID: 1230
		public const ushort PredecessorChangeList = 26083;

		// Token: 0x040004CF RID: 1231
		public const ushort RuleMsgState = 26089;

		// Token: 0x040004D0 RID: 1232
		public const ushort RuleMsgUserFlags = 26090;

		// Token: 0x040004D1 RID: 1233
		public const ushort RuleMsgProvider = 26091;

		// Token: 0x040004D2 RID: 1234
		public const ushort RuleMsgName = 26092;

		// Token: 0x040004D3 RID: 1235
		public const ushort RuleMsgLevel = 26093;

		// Token: 0x040004D4 RID: 1236
		public const ushort RuleMsgProviderData = 26094;

		// Token: 0x040004D5 RID: 1237
		public const ushort RuleMsgActions = 26095;

		// Token: 0x040004D6 RID: 1238
		public const ushort RuleMsgCondition = 26096;

		// Token: 0x040004D7 RID: 1239
		public const ushort RuleMsgConditionLCID = 26097;

		// Token: 0x040004D8 RID: 1240
		public const ushort RuleMsgVersion = 26098;

		// Token: 0x040004D9 RID: 1241
		public const ushort RuleMsgSequence = 26099;

		// Token: 0x040004DA RID: 1242
		public const ushort PreventMsgCreate = 26100;

		// Token: 0x040004DB RID: 1243
		public const ushort IMAPSubscribeList = 26102;

		// Token: 0x040004DC RID: 1244
		public const ushort LISSD = 26105;

		// Token: 0x040004DD RID: 1245
		public const ushort ProfileVersion = 26112;

		// Token: 0x040004DE RID: 1246
		public const ushort ProfileConfigFlags = 26113;

		// Token: 0x040004DF RID: 1247
		public const ushort ProfileHomeServer = 26114;

		// Token: 0x040004E0 RID: 1248
		public const ushort ProfileUser = 26115;

		// Token: 0x040004E1 RID: 1249
		public const ushort ProfileConnectFlags = 26116;

		// Token: 0x040004E2 RID: 1250
		public const ushort ProfileTransportFlags = 26117;

		// Token: 0x040004E3 RID: 1251
		public const ushort ProfileUIState = 26118;

		// Token: 0x040004E4 RID: 1252
		public const ushort ProfileUnresolvedName = 26119;

		// Token: 0x040004E5 RID: 1253
		public const ushort ProfileUnresolvedServer = 26120;

		// Token: 0x040004E6 RID: 1254
		public const ushort ProfileBindingOrder = 26121;

		// Token: 0x040004E7 RID: 1255
		public const ushort ProfileMaxRestrict = 26125;

		// Token: 0x040004E8 RID: 1256
		public const ushort ProfileABFilesPath = 26126;

		// Token: 0x040004E9 RID: 1257
		public const ushort ProfileFavFolderDisplayName = 26127;

		// Token: 0x040004EA RID: 1258
		public const ushort ProfileOfflineStorePath = 26128;

		// Token: 0x040004EB RID: 1259
		public const ushort ProfileOfflineInfo = 26129;

		// Token: 0x040004EC RID: 1260
		public const ushort ProfileHomeServerDN = 26130;

		// Token: 0x040004ED RID: 1261
		public const ushort ProfileHomeServerAddrs = 26131;

		// Token: 0x040004EE RID: 1262
		public const ushort ProfileServerDN = 26132;

		// Token: 0x040004EF RID: 1263
		public const ushort ProfileAllPubDisplayName = 26134;

		// Token: 0x040004F0 RID: 1264
		public const ushort ProfileAllPubComment = 26135;

		// Token: 0x040004F1 RID: 1265
		public const ushort InTransitState = 26136;

		// Token: 0x040004F2 RID: 1266
		public const ushort InTransitStatus = 26136;

		// Token: 0x040004F3 RID: 1267
		public const ushort UserEntryId = 26137;

		// Token: 0x040004F4 RID: 1268
		public const ushort UserName = 26138;

		// Token: 0x040004F5 RID: 1269
		public const ushort MailboxOwnerEntryId = 26139;

		// Token: 0x040004F6 RID: 1270
		public const ushort MailboxOwnerName = 26140;

		// Token: 0x040004F7 RID: 1271
		public const ushort OofState = 26141;

		// Token: 0x040004F8 RID: 1272
		public const ushort TestLineSpeed = 26155;

		// Token: 0x040004F9 RID: 1273
		public const ushort FavoritesDefaultName = 26165;

		// Token: 0x040004FA RID: 1274
		public const ushort FolderChildCount = 26168;

		// Token: 0x040004FB RID: 1275
		public const ushort FolderChildCountInt64 = 26168;

		// Token: 0x040004FC RID: 1276
		public const ushort SerializedReplidGuidMap = 26168;

		// Token: 0x040004FD RID: 1277
		public const ushort Rights = 26169;

		// Token: 0x040004FE RID: 1278
		public const ushort HasRules = 26170;

		// Token: 0x040004FF RID: 1279
		public const ushort AddressBookEntryId = 26171;

		// Token: 0x04000500 RID: 1280
		public const ushort HierarchyChangeNumber = 26174;

		// Token: 0x04000501 RID: 1281
		public const ushort HasModeratorRules = 26175;

		// Token: 0x04000502 RID: 1282
		public const ushort ModeratorRuleCount = 26175;

		// Token: 0x04000503 RID: 1283
		public const ushort DeletedMsgCount = 26176;

		// Token: 0x04000504 RID: 1284
		public const ushort DeletedMsgCountInt64 = 26176;

		// Token: 0x04000505 RID: 1285
		public const ushort DeletedFolderCount = 26177;

		// Token: 0x04000506 RID: 1286
		public const ushort DeletedAssocMsgCount = 26179;

		// Token: 0x04000507 RID: 1287
		public const ushort DeletedAssocMsgCountInt64 = 26179;

		// Token: 0x04000508 RID: 1288
		public const ushort ReplicaServer = 26180;

		// Token: 0x04000509 RID: 1289
		public const ushort PromotedProperties = 26181;

		// Token: 0x0400050A RID: 1290
		public const ushort HiddenPromotedProperties = 26182;

		// Token: 0x0400050B RID: 1291
		public const ushort DAMOriginalEntryId = 26182;

		// Token: 0x0400050C RID: 1292
		public const ushort LinkedSiteAuthorityUrl = 26183;

		// Token: 0x0400050D RID: 1293
		public const ushort HasNamedProperties = 26186;

		// Token: 0x0400050E RID: 1294
		public const ushort FidMid = 26188;

		// Token: 0x0400050F RID: 1295
		public const ushort ActiveUserEntryId = 26194;

		// Token: 0x04000510 RID: 1296
		public const ushort ICSChangeKey = 26197;

		// Token: 0x04000511 RID: 1297
		public const ushort SetPropsCondition = 26199;

		// Token: 0x04000512 RID: 1298
		public const ushort InternetContent = 26201;

		// Token: 0x04000513 RID: 1299
		public const ushort OriginatorName = 26203;

		// Token: 0x04000514 RID: 1300
		public const ushort OriginatorEmailAddress = 26204;

		// Token: 0x04000515 RID: 1301
		public const ushort OriginatorAddressType = 26205;

		// Token: 0x04000516 RID: 1302
		public const ushort OriginatorEntryId = 26206;

		// Token: 0x04000517 RID: 1303
		public const ushort RecipientNumber = 26210;

		// Token: 0x04000518 RID: 1304
		public const ushort ReportDestinationName = 26212;

		// Token: 0x04000519 RID: 1305
		public const ushort ReportDestinationEntryId = 26213;

		// Token: 0x0400051A RID: 1306
		public const ushort ProhibitReceiveQuota = 26218;

		// Token: 0x0400051B RID: 1307
		public const ushort MaxSubmitMessageSize = 26221;

		// Token: 0x0400051C RID: 1308
		public const ushort SearchAttachments = 26221;

		// Token: 0x0400051D RID: 1309
		public const ushort ProhibitSendQuota = 26222;

		// Token: 0x0400051E RID: 1310
		public const ushort SubmittedByAdmin = 26223;

		// Token: 0x0400051F RID: 1311
		public const ushort LongTermEntryIdFromTable = 26224;

		// Token: 0x04000520 RID: 1312
		public const ushort MemberId = 26225;

		// Token: 0x04000521 RID: 1313
		public const ushort MemberName = 26226;

		// Token: 0x04000522 RID: 1314
		public const ushort MemberRights = 26227;

		// Token: 0x04000523 RID: 1315
		public const ushort MemberSecurityIdentifier = 26228;

		// Token: 0x04000524 RID: 1316
		public const ushort RuleId = 26228;

		// Token: 0x04000525 RID: 1317
		public const ushort MemberIsGroup = 26229;

		// Token: 0x04000526 RID: 1318
		public const ushort RuleIds = 26229;

		// Token: 0x04000527 RID: 1319
		public const ushort RuleSequence = 26230;

		// Token: 0x04000528 RID: 1320
		public const ushort RuleState = 26231;

		// Token: 0x04000529 RID: 1321
		public const ushort RuleUserFlags = 26232;

		// Token: 0x0400052A RID: 1322
		public const ushort RuleCondition = 26233;

		// Token: 0x0400052B RID: 1323
		public const ushort RuleMsgConditionOld = 26233;

		// Token: 0x0400052C RID: 1324
		public const ushort RuleActions = 26240;

		// Token: 0x0400052D RID: 1325
		public const ushort RuleMsgActionsOld = 26240;

		// Token: 0x0400052E RID: 1326
		public const ushort RuleProvider = 26241;

		// Token: 0x0400052F RID: 1327
		public const ushort RuleName = 26242;

		// Token: 0x04000530 RID: 1328
		public const ushort RuleLevel = 26243;

		// Token: 0x04000531 RID: 1329
		public const ushort RuleProviderData = 26244;

		// Token: 0x04000532 RID: 1330
		public const ushort DeletedOn = 26255;

		// Token: 0x04000533 RID: 1331
		public const ushort ReplicationStyle = 26256;

		// Token: 0x04000534 RID: 1332
		public const ushort ReplicationTIB = 26257;

		// Token: 0x04000535 RID: 1333
		public const ushort ReplicationMsgPriority = 26258;

		// Token: 0x04000536 RID: 1334
		public const ushort WorkerProcessId = 26263;

		// Token: 0x04000537 RID: 1335
		public const ushort MinimumDatabaseSchemaVersion = 26264;

		// Token: 0x04000538 RID: 1336
		public const ushort ReplicaList = 26264;

		// Token: 0x04000539 RID: 1337
		public const ushort OverallAgeLimit = 26265;

		// Token: 0x0400053A RID: 1338
		public const ushort MaximumDatabaseSchemaVersion = 26265;

		// Token: 0x0400053B RID: 1339
		public const ushort CurrentDatabaseSchemaVersion = 26266;

		// Token: 0x0400053C RID: 1340
		public const ushort MailboxDatabaseVersion = 26266;

		// Token: 0x0400053D RID: 1341
		public const ushort DeletedMessageSize = 26267;

		// Token: 0x0400053E RID: 1342
		public const ushort DeletedMessageSize32 = 26267;

		// Token: 0x0400053F RID: 1343
		public const ushort RequestedDatabaseSchemaVersion = 26267;

		// Token: 0x04000540 RID: 1344
		public const ushort DeletedNormalMessageSize = 26268;

		// Token: 0x04000541 RID: 1345
		public const ushort DeletedNormalMessageSize32 = 26268;

		// Token: 0x04000542 RID: 1346
		public const ushort DeletedAssociatedMessageSize = 26269;

		// Token: 0x04000543 RID: 1347
		public const ushort DeletedAssociatedMessageSize32 = 26269;

		// Token: 0x04000544 RID: 1348
		public const ushort SecureInSite = 26270;

		// Token: 0x04000545 RID: 1349
		public const ushort NTUsername = 26272;

		// Token: 0x04000546 RID: 1350
		public const ushort NTUserSid = 26272;

		// Token: 0x04000547 RID: 1351
		public const ushort LocaleId = 26273;

		// Token: 0x04000548 RID: 1352
		public const ushort LastLogonTime = 26274;

		// Token: 0x04000549 RID: 1353
		public const ushort LastLogoffTime = 26275;

		// Token: 0x0400054A RID: 1354
		public const ushort StorageLimitInformation = 26276;

		// Token: 0x0400054B RID: 1355
		public const ushort InternetMdns = 26277;

		// Token: 0x0400054C RID: 1356
		public const ushort MailboxStatus = 26277;

		// Token: 0x0400054D RID: 1357
		public const ushort MailboxFlags = 26279;

		// Token: 0x0400054E RID: 1358
		public const ushort FolderFlags = 26280;

		// Token: 0x0400054F RID: 1359
		public const ushort PreservingMailboxSignature = 26280;

		// Token: 0x04000550 RID: 1360
		public const ushort MRSPreservingMailboxSignature = 26281;

		// Token: 0x04000551 RID: 1361
		public const ushort LastAccessTime = 26281;

		// Token: 0x04000552 RID: 1362
		public const ushort MailboxMessagesPerFolderCountWarningQuota = 26283;

		// Token: 0x04000553 RID: 1363
		public const ushort MailboxMessagesPerFolderCountReceiveQuota = 26284;

		// Token: 0x04000554 RID: 1364
		public const ushort NormalMsgWithAttachCount = 26285;

		// Token: 0x04000555 RID: 1365
		public const ushort NormalMsgWithAttachCountInt64 = 26285;

		// Token: 0x04000556 RID: 1366
		public const ushort DumpsterMessagesPerFolderCountWarningQuota = 26285;

		// Token: 0x04000557 RID: 1367
		public const ushort AssocMsgWithAttachCount = 26286;

		// Token: 0x04000558 RID: 1368
		public const ushort AssocMsgWithAttachCountInt64 = 26286;

		// Token: 0x04000559 RID: 1369
		public const ushort DumpsterMessagesPerFolderCountReceiveQuota = 26286;

		// Token: 0x0400055A RID: 1370
		public const ushort RecipientOnNormalMsgCount = 26287;

		// Token: 0x0400055B RID: 1371
		public const ushort RecipientOnNormalMsgCountInt64 = 26287;

		// Token: 0x0400055C RID: 1372
		public const ushort FolderHierarchyChildrenCountWarningQuota = 26287;

		// Token: 0x0400055D RID: 1373
		public const ushort RecipientOnAssocMsgCount = 26288;

		// Token: 0x0400055E RID: 1374
		public const ushort RecipientOnAssocMsgCountInt64 = 26288;

		// Token: 0x0400055F RID: 1375
		public const ushort FolderHierarchyChildrenCountReceiveQuota = 26288;

		// Token: 0x04000560 RID: 1376
		public const ushort AttachOnNormalMsgCt = 26289;

		// Token: 0x04000561 RID: 1377
		public const ushort AttachOnNormalMsgCtInt64 = 26289;

		// Token: 0x04000562 RID: 1378
		public const ushort FolderHierarchyDepthWarningQuota = 26289;

		// Token: 0x04000563 RID: 1379
		public const ushort AttachOnAssocMsgCt = 26290;

		// Token: 0x04000564 RID: 1380
		public const ushort AttachOnAssocMsgCtInt64 = 26290;

		// Token: 0x04000565 RID: 1381
		public const ushort FolderHierarchyDepthReceiveQuota = 26290;

		// Token: 0x04000566 RID: 1382
		public const ushort NormalMessageSize = 26291;

		// Token: 0x04000567 RID: 1383
		public const ushort NormalMessageSize32 = 26291;

		// Token: 0x04000568 RID: 1384
		public const ushort AssociatedMessageSize = 26292;

		// Token: 0x04000569 RID: 1385
		public const ushort AssociatedMessageSize32 = 26292;

		// Token: 0x0400056A RID: 1386
		public const ushort FolderPathName = 26293;

		// Token: 0x0400056B RID: 1387
		public const ushort FoldersCountWarningQuota = 26293;

		// Token: 0x0400056C RID: 1388
		public const ushort OwnerCount = 26294;

		// Token: 0x0400056D RID: 1389
		public const ushort FoldersCountReceiveQuota = 26294;

		// Token: 0x0400056E RID: 1390
		public const ushort ContactCount = 26295;

		// Token: 0x0400056F RID: 1391
		public const ushort NamedPropertiesCountQuota = 26295;

		// Token: 0x04000570 RID: 1392
		public const ushort CodePageId = 26307;

		// Token: 0x04000571 RID: 1393
		public const ushort RetentionAgeLimit = 26308;

		// Token: 0x04000572 RID: 1394
		public const ushort DisablePerUserRead = 26309;

		// Token: 0x04000573 RID: 1395
		public const ushort UserDN = 26314;

		// Token: 0x04000574 RID: 1396
		public const ushort UserDisplayName = 26315;

		// Token: 0x04000575 RID: 1397
		public const ushort ServerDN = 26336;

		// Token: 0x04000576 RID: 1398
		public const ushort BackfillRanking = 26337;

		// Token: 0x04000577 RID: 1399
		public const ushort LastTransmissionTime = 26338;

		// Token: 0x04000578 RID: 1400
		public const ushort StatusSendTime = 26339;

		// Token: 0x04000579 RID: 1401
		public const ushort BackfillEntryCount = 26340;

		// Token: 0x0400057A RID: 1402
		public const ushort NextBroadcastTime = 26341;

		// Token: 0x0400057B RID: 1403
		public const ushort NextBackfillTime = 26342;

		// Token: 0x0400057C RID: 1404
		public const ushort LastCNBroadcast = 26343;

		// Token: 0x0400057D RID: 1405
		public const ushort BackfillId = 26347;

		// Token: 0x0400057E RID: 1406
		public const ushort LastShortCNBroadcast = 26356;

		// Token: 0x0400057F RID: 1407
		public const ushort AverageTransmissionTime = 26363;

		// Token: 0x04000580 RID: 1408
		public const ushort ReplicationStatus = 26364;

		// Token: 0x04000581 RID: 1409
		public const ushort LastDataReceivalTime = 26365;

		// Token: 0x04000582 RID: 1410
		public const ushort AdminDisplayName = 26366;

		// Token: 0x04000583 RID: 1411
		public const ushort WizardNoPSTPage = 26368;

		// Token: 0x04000584 RID: 1412
		public const ushort WizardNoPABPage = 26369;

		// Token: 0x04000585 RID: 1413
		public const ushort SortLocaleId = 26373;

		// Token: 0x04000586 RID: 1414
		public const ushort MailboxDSGuid = 26375;

		// Token: 0x04000587 RID: 1415
		public const ushort MailboxDSGuidGuid = 26375;

		// Token: 0x04000588 RID: 1416
		public const ushort URLName = 26375;

		// Token: 0x04000589 RID: 1417
		public const ushort DateDiscoveredAbsentInDS = 26376;

		// Token: 0x0400058A RID: 1418
		public const ushort UnifiedMailboxGuidGuid = 26376;

		// Token: 0x0400058B RID: 1419
		public const ushort LocalCommitTime = 26377;

		// Token: 0x0400058C RID: 1420
		public const ushort LocalCommitTimeMax = 26378;

		// Token: 0x0400058D RID: 1421
		public const ushort DeletedCountTotal = 26379;

		// Token: 0x0400058E RID: 1422
		public const ushort DeletedCountTotalInt64 = 26379;

		// Token: 0x0400058F RID: 1423
		public const ushort AutoReset = 26380;

		// Token: 0x04000590 RID: 1424
		public const ushort ScopeFIDs = 26386;

		// Token: 0x04000591 RID: 1425
		public const ushort ELCAutoCopyTag = 26390;

		// Token: 0x04000592 RID: 1426
		public const ushort ELCMoveDate = 26391;

		// Token: 0x04000593 RID: 1427
		public const ushort PFAdminDescription = 26391;

		// Token: 0x04000594 RID: 1428
		public const ushort PFProxy = 26397;

		// Token: 0x04000595 RID: 1429
		public const ushort PFPlatinumHomeMdb = 26398;

		// Token: 0x04000596 RID: 1430
		public const ushort PFProxyRequired = 26399;

		// Token: 0x04000597 RID: 1431
		public const ushort PFOverHardQuotaLimit = 26401;

		// Token: 0x04000598 RID: 1432
		public const ushort QuotaWarningThreshold = 26401;

		// Token: 0x04000599 RID: 1433
		public const ushort PFMsgSizeLimit = 26402;

		// Token: 0x0400059A RID: 1434
		public const ushort QuotaSendThreshold = 26402;

		// Token: 0x0400059B RID: 1435
		public const ushort PFDisallowMdbWideExpiry = 26403;

		// Token: 0x0400059C RID: 1436
		public const ushort QuotaReceiveThreshold = 26403;

		// Token: 0x0400059D RID: 1437
		public const ushort FolderAdminFlags = 26413;

		// Token: 0x0400059E RID: 1438
		public const ushort TimeInServer = 26413;

		// Token: 0x0400059F RID: 1439
		public const ushort TimeInCpu = 26414;

		// Token: 0x040005A0 RID: 1440
		public const ushort ProvisionedFID = 26415;

		// Token: 0x040005A1 RID: 1441
		public const ushort RopCount = 26415;

		// Token: 0x040005A2 RID: 1442
		public const ushort ELCFolderSize = 26416;

		// Token: 0x040005A3 RID: 1443
		public const ushort PageRead = 26416;

		// Token: 0x040005A4 RID: 1444
		public const ushort ELCFolderQuota = 26417;

		// Token: 0x040005A5 RID: 1445
		public const ushort PagePreread = 26417;

		// Token: 0x040005A6 RID: 1446
		public const ushort ELCPolicyId = 26418;

		// Token: 0x040005A7 RID: 1447
		public const ushort LogRecordCount = 26418;

		// Token: 0x040005A8 RID: 1448
		public const ushort ELCPolicyComment = 26419;

		// Token: 0x040005A9 RID: 1449
		public const ushort LogRecordBytes = 26419;

		// Token: 0x040005AA RID: 1450
		public const ushort PropertyGroupMappingId = 26420;

		// Token: 0x040005AB RID: 1451
		public const ushort LdapReads = 26420;

		// Token: 0x040005AC RID: 1452
		public const ushort LdapSearches = 26421;

		// Token: 0x040005AD RID: 1453
		public const ushort DigestCategory = 26422;

		// Token: 0x040005AE RID: 1454
		public const ushort SampleId = 26423;

		// Token: 0x040005AF RID: 1455
		public const ushort SampleTime = 26424;

		// Token: 0x040005B0 RID: 1456
		public const ushort PropGroupInfo = 26430;

		// Token: 0x040005B1 RID: 1457
		public const ushort PropertyGroupChangeMask = 26430;

		// Token: 0x040005B2 RID: 1458
		public const ushort ReadCnNewExport = 26431;

		// Token: 0x040005B3 RID: 1459
		public const ushort SentMailSvrEID = 26432;

		// Token: 0x040005B4 RID: 1460
		public const ushort SentMailSvrEIDBin = 26432;

		// Token: 0x040005B5 RID: 1461
		public const ushort LocallyDelivered = 26437;

		// Token: 0x040005B6 RID: 1462
		public const ushort MimeSize = 26438;

		// Token: 0x040005B7 RID: 1463
		public const ushort MimeSize32 = 26438;

		// Token: 0x040005B8 RID: 1464
		public const ushort FileSize = 26439;

		// Token: 0x040005B9 RID: 1465
		public const ushort FileSize32 = 26439;

		// Token: 0x040005BA RID: 1466
		public const ushort Fid = 26440;

		// Token: 0x040005BB RID: 1467
		public const ushort FidBin = 26440;

		// Token: 0x040005BC RID: 1468
		public const ushort ParentFid = 26441;

		// Token: 0x040005BD RID: 1469
		public const ushort ParentFidBin = 26441;

		// Token: 0x040005BE RID: 1470
		public const ushort Mid = 26442;

		// Token: 0x040005BF RID: 1471
		public const ushort MidBin = 26442;

		// Token: 0x040005C0 RID: 1472
		public const ushort CategID = 26443;

		// Token: 0x040005C1 RID: 1473
		public const ushort ParentCategID = 26444;

		// Token: 0x040005C2 RID: 1474
		public const ushort InstanceId = 26445;

		// Token: 0x040005C3 RID: 1475
		public const ushort InstanceNum = 26446;

		// Token: 0x040005C4 RID: 1476
		public const ushort ChangeType = 26448;

		// Token: 0x040005C5 RID: 1477
		public const ushort ArticleNumNext = 26449;

		// Token: 0x040005C6 RID: 1478
		public const ushort RequiresRefResolve = 26449;

		// Token: 0x040005C7 RID: 1479
		public const ushort ImapLastArticleId = 26450;

		// Token: 0x040005C8 RID: 1480
		public const ushort LTID = 26456;

		// Token: 0x040005C9 RID: 1481
		public const ushort CnExport = 26457;

		// Token: 0x040005CA RID: 1482
		public const ushort PclExport = 26458;

		// Token: 0x040005CB RID: 1483
		public const ushort CnMvExport = 26459;

		// Token: 0x040005CC RID: 1484
		public const ushort MidsetDeletedExport = 26460;

		// Token: 0x040005CD RID: 1485
		public const ushort ArticleNumMic = 26461;

		// Token: 0x040005CE RID: 1486
		public const ushort ArticleNumMost = 26462;

		// Token: 0x040005CF RID: 1487
		public const ushort RulesSync = 26464;

		// Token: 0x040005D0 RID: 1488
		public const ushort ReplicaListR = 26465;

		// Token: 0x040005D1 RID: 1489
		public const ushort SortOrder = 26465;

		// Token: 0x040005D2 RID: 1490
		public const ushort LocalIdNext = 26465;

		// Token: 0x040005D3 RID: 1491
		public const ushort ReplicaListRC = 26466;

		// Token: 0x040005D4 RID: 1492
		public const ushort ReplicaListRBUG = 26467;

		// Token: 0x040005D5 RID: 1493
		public const ushort RootFid = 26468;

		// Token: 0x040005D6 RID: 1494
		public const ushort FIDC = 26470;

		// Token: 0x040005D7 RID: 1495
		public const ushort EventMailboxGuid = 26474;

		// Token: 0x040005D8 RID: 1496
		public const ushort MdbDSGuid = 26474;

		// Token: 0x040005D9 RID: 1497
		public const ushort MailboxOwnerDN = 26475;

		// Token: 0x040005DA RID: 1498
		public const ushort MailboxGuid = 26476;

		// Token: 0x040005DB RID: 1499
		public const ushort MapiEntryIdGuid = 26476;

		// Token: 0x040005DC RID: 1500
		public const ushort MapiEntryIdGuidGuid = 26476;

		// Token: 0x040005DD RID: 1501
		public const ushort ImapCachedBodystructure = 26477;

		// Token: 0x040005DE RID: 1502
		public const ushort Localized = 26477;

		// Token: 0x040005DF RID: 1503
		public const ushort LCID = 26478;

		// Token: 0x040005E0 RID: 1504
		public const ushort AltRecipientDN = 26479;

		// Token: 0x040005E1 RID: 1505
		public const ushort NoLocalDelivery = 26480;

		// Token: 0x040005E2 RID: 1506
		public const ushort SoftDeleted = 26480;

		// Token: 0x040005E3 RID: 1507
		public const ushort DeliveryContentLength = 26481;

		// Token: 0x040005E4 RID: 1508
		public const ushort AutoReply = 26482;

		// Token: 0x040005E5 RID: 1509
		public const ushort MailboxOwnerDisplayName = 26483;

		// Token: 0x040005E6 RID: 1510
		public const ushort MailboxLastUpdated = 26484;

		// Token: 0x040005E7 RID: 1511
		public const ushort AdminSurName = 26485;

		// Token: 0x040005E8 RID: 1512
		public const ushort AdminGivenName = 26486;

		// Token: 0x040005E9 RID: 1513
		public const ushort ActiveSearchCount = 26487;

		// Token: 0x040005EA RID: 1514
		public const ushort AdminNickname = 26488;

		// Token: 0x040005EB RID: 1515
		public const ushort QuotaStyle = 26489;

		// Token: 0x040005EC RID: 1516
		public const ushort OverQuotaLimit = 26490;

		// Token: 0x040005ED RID: 1517
		public const ushort StorageQuota = 26491;

		// Token: 0x040005EE RID: 1518
		public const ushort SubmitContentLength = 26492;

		// Token: 0x040005EF RID: 1519
		public const ushort ReservedIdCounterRangeUpperLimit = 26494;

		// Token: 0x040005F0 RID: 1520
		public const ushort FolderPropTagArray = 26494;

		// Token: 0x040005F1 RID: 1521
		public const ushort ReservedCnCounterRangeUpperLimit = 26495;

		// Token: 0x040005F2 RID: 1522
		public const ushort MsgFolderPropTagArray = 26495;

		// Token: 0x040005F3 RID: 1523
		public const ushort SetReceiveCount = 26496;

		// Token: 0x040005F4 RID: 1524
		public const ushort SubmittedCount = 26498;

		// Token: 0x040005F5 RID: 1525
		public const ushort CreatorToken = 26499;

		// Token: 0x040005F6 RID: 1526
		public const ushort SearchState = 26499;

		// Token: 0x040005F7 RID: 1527
		public const ushort SearchRestriction = 26500;

		// Token: 0x040005F8 RID: 1528
		public const ushort SearchFIDs = 26501;

		// Token: 0x040005F9 RID: 1529
		public const ushort RecursiveSearchFIDs = 26502;

		// Token: 0x040005FA RID: 1530
		public const ushort SearchBacklinks = 26503;

		// Token: 0x040005FB RID: 1531
		public const ushort LCIDRestriction = 26504;

		// Token: 0x040005FC RID: 1532
		public const ushort CategFIDs = 26506;

		// Token: 0x040005FD RID: 1533
		public const ushort FolderCDN = 26509;

		// Token: 0x040005FE RID: 1534
		public const ushort MidSegmentStart = 26513;

		// Token: 0x040005FF RID: 1535
		public const ushort MidsetDeleted = 26514;

		// Token: 0x04000600 RID: 1536
		public const ushort FolderIdsetIn = 26514;

		// Token: 0x04000601 RID: 1537
		public const ushort MidsetExpired = 26515;

		// Token: 0x04000602 RID: 1538
		public const ushort CnsetIn = 26516;

		// Token: 0x04000603 RID: 1539
		public const ushort CnsetBackfill = 26518;

		// Token: 0x04000604 RID: 1540
		public const ushort CnsetSeen = 26518;

		// Token: 0x04000605 RID: 1541
		public const ushort MidsetTombstones = 26520;

		// Token: 0x04000606 RID: 1542
		public const ushort GWFolder = 26522;

		// Token: 0x04000607 RID: 1543
		public const ushort IPMFolder = 26523;

		// Token: 0x04000608 RID: 1544
		public const ushort PublicFolderPath = 26524;

		// Token: 0x04000609 RID: 1545
		public const ushort MidSegmentIndex = 26527;

		// Token: 0x0400060A RID: 1546
		public const ushort MidSegmentSize = 26528;

		// Token: 0x0400060B RID: 1547
		public const ushort CnSegmentStart = 26529;

		// Token: 0x0400060C RID: 1548
		public const ushort CnSegmentIndex = 26530;

		// Token: 0x0400060D RID: 1549
		public const ushort CnSegmentSize = 26531;

		// Token: 0x0400060E RID: 1550
		public const ushort ChangeNumber = 26532;

		// Token: 0x0400060F RID: 1551
		public const ushort ChangeNumberBin = 26532;

		// Token: 0x04000610 RID: 1552
		public const ushort PCL = 26533;

		// Token: 0x04000611 RID: 1553
		public const ushort CnMv = 26534;

		// Token: 0x04000612 RID: 1554
		public const ushort FolderTreeRootFID = 26535;

		// Token: 0x04000613 RID: 1555
		public const ushort SourceEntryId = 26536;

		// Token: 0x04000614 RID: 1556
		public const ushort MailFlags = 26537;

		// Token: 0x04000615 RID: 1557
		public const ushort Associated = 26538;

		// Token: 0x04000616 RID: 1558
		public const ushort SubmitResponsibility = 26539;

		// Token: 0x04000617 RID: 1559
		public const ushort SharedReceiptHandling = 26541;

		// Token: 0x04000618 RID: 1560
		public const ushort Inid = 26544;

		// Token: 0x04000619 RID: 1561
		public const ushort ViewRestriction = 26544;

		// Token: 0x0400061A RID: 1562
		public const ushort MessageAttachList = 26547;

		// Token: 0x0400061B RID: 1563
		public const ushort SenderCAI = 26549;

		// Token: 0x0400061C RID: 1564
		public const ushort SentRepresentingCAI = 26550;

		// Token: 0x0400061D RID: 1565
		public const ushort OriginalSenderCAI = 26551;

		// Token: 0x0400061E RID: 1566
		public const ushort OriginalSentRepresentingCAI = 26552;

		// Token: 0x0400061F RID: 1567
		public const ushort ReceivedByCAI = 26553;

		// Token: 0x04000620 RID: 1568
		public const ushort ReceivedRepresentingCAI = 26554;

		// Token: 0x04000621 RID: 1569
		public const ushort ReadReceiptCAI = 26555;

		// Token: 0x04000622 RID: 1570
		public const ushort ReportCAI = 26556;

		// Token: 0x04000623 RID: 1571
		public const ushort CreatorCAI = 26557;

		// Token: 0x04000624 RID: 1572
		public const ushort LastModifierCAI = 26558;

		// Token: 0x04000625 RID: 1573
		public const ushort AnonymousRights = 26564;

		// Token: 0x04000626 RID: 1574
		public const ushort SearchGUID = 26574;

		// Token: 0x04000627 RID: 1575
		public const ushort CnsetRead = 26578;

		// Token: 0x04000628 RID: 1576
		public const ushort CnsetBackfillFAI = 26586;

		// Token: 0x04000629 RID: 1577
		public const ushort CnsetSeenFAI = 26586;

		// Token: 0x0400062A RID: 1578
		public const ushort ReplMsgVersion = 26590;

		// Token: 0x0400062B RID: 1579
		public const ushort IdSetDeleted = 26597;

		// Token: 0x0400062C RID: 1580
		public const ushort FolderMessages = 26598;

		// Token: 0x0400062D RID: 1581
		public const ushort SenderReplid = 26599;

		// Token: 0x0400062E RID: 1582
		public const ushort CnMin = 26600;

		// Token: 0x0400062F RID: 1583
		public const ushort CnMax = 26601;

		// Token: 0x04000630 RID: 1584
		public const ushort ReplMsgType = 26602;

		// Token: 0x04000631 RID: 1585
		public const ushort RgszDNResponders = 26603;

		// Token: 0x04000632 RID: 1586
		public const ushort ViewCoveringPropertyTags = 26610;

		// Token: 0x04000633 RID: 1587
		public const ushort ViewAccessTime = 26611;

		// Token: 0x04000634 RID: 1588
		public const ushort ICSViewFilter = 26612;

		// Token: 0x04000635 RID: 1589
		public const ushort ModifiedCount = 26614;

		// Token: 0x04000636 RID: 1590
		public const ushort DeletedState = 26615;

		// Token: 0x04000637 RID: 1591
		public const ushort OriginatorCAI = 26616;

		// Token: 0x04000638 RID: 1592
		public const ushort ReportDestinationCAI = 26617;

		// Token: 0x04000639 RID: 1593
		public const ushort OriginalAuthorCAI = 26618;

		// Token: 0x0400063A RID: 1594
		public const ushort ReadCnNew = 26622;

		// Token: 0x0400063B RID: 1595
		public const ushort ReadCnNewBin = 26622;

		// Token: 0x0400063C RID: 1596
		public const ushort SenderTelephoneNumber = 26626;

		// Token: 0x0400063D RID: 1597
		public const ushort ShutoffQuota = 26628;

		// Token: 0x0400063E RID: 1598
		public const ushort VoiceMessageAttachmentOrder = 26629;

		// Token: 0x0400063F RID: 1599
		public const ushort MailboxMiscFlags = 26630;

		// Token: 0x04000640 RID: 1600
		public const ushort EventCounter = 26631;

		// Token: 0x04000641 RID: 1601
		public const ushort EventMask = 26632;

		// Token: 0x04000642 RID: 1602
		public const ushort EventFid = 26633;

		// Token: 0x04000643 RID: 1603
		public const ushort EventMid = 26634;

		// Token: 0x04000644 RID: 1604
		public const ushort EventFidParent = 26635;

		// Token: 0x04000645 RID: 1605
		public const ushort MailboxInCreation = 26635;

		// Token: 0x04000646 RID: 1606
		public const ushort EventFidOld = 26636;

		// Token: 0x04000647 RID: 1607
		public const ushort EventMidOld = 26637;

		// Token: 0x04000648 RID: 1608
		public const ushort ObjectClassFlags = 26637;

		// Token: 0x04000649 RID: 1609
		public const ushort EventFidOldParent = 26638;

		// Token: 0x0400064A RID: 1610
		public const ushort ptagMsgHeaderTableFID = 26638;

		// Token: 0x0400064B RID: 1611
		public const ushort EventCreatedTime = 26639;

		// Token: 0x0400064C RID: 1612
		public const ushort EventMessageClass = 26640;

		// Token: 0x0400064D RID: 1613
		public const ushort OOFStateEx = 26640;

		// Token: 0x0400064E RID: 1614
		public const ushort EventItemCount = 26641;

		// Token: 0x0400064F RID: 1615
		public const ushort EventFidRoot = 26642;

		// Token: 0x04000650 RID: 1616
		public const ushort EventUnreadCount = 26643;

		// Token: 0x04000651 RID: 1617
		public const ushort OofStateUserChangeTime = 26643;

		// Token: 0x04000652 RID: 1618
		public const ushort EventTransacId = 26644;

		// Token: 0x04000653 RID: 1619
		public const ushort UserOofSettingsItemId = 26644;

		// Token: 0x04000654 RID: 1620
		public const ushort DocumentId = 26645;

		// Token: 0x04000655 RID: 1621
		public const ushort EventFlags = 26645;

		// Token: 0x04000656 RID: 1622
		public const ushort EventExtendedFlags = 26648;

		// Token: 0x04000657 RID: 1623
		public const ushort EventClientType = 26649;

		// Token: 0x04000658 RID: 1624
		public const ushort SoftDeletedFilter = 26649;

		// Token: 0x04000659 RID: 1625
		public const ushort EventSid = 26650;

		// Token: 0x0400065A RID: 1626
		public const ushort AssociatedFilter = 26650;

		// Token: 0x0400065B RID: 1627
		public const ushort MailboxQuarantined = 26650;

		// Token: 0x0400065C RID: 1628
		public const ushort EventDocId = 26651;

		// Token: 0x0400065D RID: 1629
		public const ushort ConversationsFilter = 26651;

		// Token: 0x0400065E RID: 1630
		public const ushort MailboxQuarantineDescription = 26651;

		// Token: 0x0400065F RID: 1631
		public const ushort MailboxQuarantineLastCrash = 26652;

		// Token: 0x04000660 RID: 1632
		public const ushort InstanceGuid = 26653;

		// Token: 0x04000661 RID: 1633
		public const ushort MailboxQuarantineEnd = 26653;

		// Token: 0x04000662 RID: 1634
		public const ushort MailboxNum = 26655;

		// Token: 0x04000663 RID: 1635
		public const ushort InferenceActivityId = 26656;

		// Token: 0x04000664 RID: 1636
		public const ushort InferenceClientId = 26657;

		// Token: 0x04000665 RID: 1637
		public const ushort InferenceItemId = 26658;

		// Token: 0x04000666 RID: 1638
		public const ushort InferenceTimeStamp = 26659;

		// Token: 0x04000667 RID: 1639
		public const ushort InferenceWindowId = 26660;

		// Token: 0x04000668 RID: 1640
		public const ushort InferenceSessionId = 26661;

		// Token: 0x04000669 RID: 1641
		public const ushort InferenceFolderId = 26662;

		// Token: 0x0400066A RID: 1642
		public const ushort ConversationDocumentId = 26662;

		// Token: 0x0400066B RID: 1643
		public const ushort ConversationIdHash = 26663;

		// Token: 0x0400066C RID: 1644
		public const ushort InferenceOofEnabled = 26663;

		// Token: 0x0400066D RID: 1645
		public const ushort InferenceDeleteType = 26664;

		// Token: 0x0400066E RID: 1646
		public const ushort LocalDirectoryBlob = 26664;

		// Token: 0x0400066F RID: 1647
		public const ushort InferenceBrowser = 26665;

		// Token: 0x04000670 RID: 1648
		public const ushort MemberEmail = 26665;

		// Token: 0x04000671 RID: 1649
		public const ushort InferenceLocaleId = 26666;

		// Token: 0x04000672 RID: 1650
		public const ushort MemberExternalId = 26666;

		// Token: 0x04000673 RID: 1651
		public const ushort InferenceLocation = 26667;

		// Token: 0x04000674 RID: 1652
		public const ushort MemberSID = 26667;

		// Token: 0x04000675 RID: 1653
		public const ushort InferenceConversationId = 26668;

		// Token: 0x04000676 RID: 1654
		public const ushort InferenceIpAddress = 26669;

		// Token: 0x04000677 RID: 1655
		public const ushort MaxMessageSize = 26669;

		// Token: 0x04000678 RID: 1656
		public const ushort InferenceTimeZone = 26670;

		// Token: 0x04000679 RID: 1657
		public const ushort InferenceCategory = 26671;

		// Token: 0x0400067A RID: 1658
		public const ushort InferenceAttachmentId = 26672;

		// Token: 0x0400067B RID: 1659
		public const ushort LastUserAccessTime = 26672;

		// Token: 0x0400067C RID: 1660
		public const ushort InferenceGlobalObjectId = 26673;

		// Token: 0x0400067D RID: 1661
		public const ushort LastUserModificationTime = 26673;

		// Token: 0x0400067E RID: 1662
		public const ushort InferenceModuleSelected = 26674;

		// Token: 0x0400067F RID: 1663
		public const ushort InferenceLayoutType = 26675;

		// Token: 0x04000680 RID: 1664
		public const ushort ViewStyle = 26676;

		// Token: 0x04000681 RID: 1665
		public const ushort InferenceClientActivityFlags = 26676;

		// Token: 0x04000682 RID: 1666
		public const ushort InferenceOWAUserActivityLoggingEnabledDeprecated = 26677;

		// Token: 0x04000683 RID: 1667
		public const ushort InferenceOLKUserActivityLoggingEnabled = 26678;

		// Token: 0x04000684 RID: 1668
		public const ushort FreebusyEMA = 26697;

		// Token: 0x04000685 RID: 1669
		public const ushort WunderbarLinkEntryID = 26700;

		// Token: 0x04000686 RID: 1670
		public const ushort WunderbarLinkStoreEntryId = 26702;

		// Token: 0x04000687 RID: 1671
		public const ushort SchdInfoFreebusyMerged = 26704;

		// Token: 0x04000688 RID: 1672
		public const ushort WunderbarLinkGroupClsId = 26704;

		// Token: 0x04000689 RID: 1673
		public const ushort WunderbarLinkGroupName = 26705;

		// Token: 0x0400068A RID: 1674
		public const ushort WunderbarLinkSection = 26706;

		// Token: 0x0400068B RID: 1675
		public const ushort NavigationNodeCalendarColor = 26707;

		// Token: 0x0400068C RID: 1676
		public const ushort NavigationNodeAddressbookEntryId = 26708;

		// Token: 0x0400068D RID: 1677
		public const ushort AgingDeleteItems = 26709;

		// Token: 0x0400068E RID: 1678
		public const ushort AgingFileName9AndPrev = 26710;

		// Token: 0x0400068F RID: 1679
		public const ushort AgingAgeFolder = 26711;

		// Token: 0x04000690 RID: 1680
		public const ushort AgingDontAgeMe = 26712;

		// Token: 0x04000691 RID: 1681
		public const ushort AgingFileNameAfter9 = 26713;

		// Token: 0x04000692 RID: 1682
		public const ushort AgingWhenDeletedOnServer = 26715;

		// Token: 0x04000693 RID: 1683
		public const ushort AgingWaitUntilExpired = 26716;

		// Token: 0x04000694 RID: 1684
		public const ushort InferenceTrainedModelVersionBreadCrumb = 26739;

		// Token: 0x04000695 RID: 1685
		public const ushort ConversationMvFrom = 26752;

		// Token: 0x04000696 RID: 1686
		public const ushort ConversationMvFromMailboxWide = 26753;

		// Token: 0x04000697 RID: 1687
		public const ushort ConversationMvTo = 26754;

		// Token: 0x04000698 RID: 1688
		public const ushort ConversationMvToMailboxWide = 26755;

		// Token: 0x04000699 RID: 1689
		public const ushort ConversationMessageDeliveryTime = 26756;

		// Token: 0x0400069A RID: 1690
		public const ushort ConversationMessageDeliveryTimeMailboxWide = 26757;

		// Token: 0x0400069B RID: 1691
		public const ushort ConversationCategories = 26758;

		// Token: 0x0400069C RID: 1692
		public const ushort ConversationCategoriesMailboxWide = 26759;

		// Token: 0x0400069D RID: 1693
		public const ushort ConversationFlagStatus = 26760;

		// Token: 0x0400069E RID: 1694
		public const ushort ConversationFlagStatusMailboxWide = 26761;

		// Token: 0x0400069F RID: 1695
		public const ushort ConversationFlagCompleteTime = 26762;

		// Token: 0x040006A0 RID: 1696
		public const ushort ConversationFlagCompleteTimeMailboxWide = 26763;

		// Token: 0x040006A1 RID: 1697
		public const ushort ConversationHasAttach = 26764;

		// Token: 0x040006A2 RID: 1698
		public const ushort ConversationHasAttachMailboxWide = 26765;

		// Token: 0x040006A3 RID: 1699
		public const ushort ConversationContentCount = 26766;

		// Token: 0x040006A4 RID: 1700
		public const ushort ConversationContentCountMailboxWide = 26767;

		// Token: 0x040006A5 RID: 1701
		public const ushort ConversationContentUnread = 26768;

		// Token: 0x040006A6 RID: 1702
		public const ushort ConversationContentUnreadMailboxWide = 26769;

		// Token: 0x040006A7 RID: 1703
		public const ushort ConversationMessageSize = 26770;

		// Token: 0x040006A8 RID: 1704
		public const ushort ConversationMessageSizeMailboxWide = 26771;

		// Token: 0x040006A9 RID: 1705
		public const ushort ConversationMessageClasses = 26772;

		// Token: 0x040006AA RID: 1706
		public const ushort ConversationMessageClassesMailboxWide = 26773;

		// Token: 0x040006AB RID: 1707
		public const ushort ConversationReplyForwardState = 26774;

		// Token: 0x040006AC RID: 1708
		public const ushort ConversationReplyForwardStateMailboxWide = 26775;

		// Token: 0x040006AD RID: 1709
		public const ushort ConversationImportance = 26776;

		// Token: 0x040006AE RID: 1710
		public const ushort ConversationImportanceMailboxWide = 26777;

		// Token: 0x040006AF RID: 1711
		public const ushort ConversationMvFromUnread = 26778;

		// Token: 0x040006B0 RID: 1712
		public const ushort ConversationMvFromUnreadMailboxWide = 26779;

		// Token: 0x040006B1 RID: 1713
		public const ushort CategCount = 26782;

		// Token: 0x040006B2 RID: 1714
		public const ushort ConversationMvItemIds = 26784;

		// Token: 0x040006B3 RID: 1715
		public const ushort ConversationMvItemIdsMailboxWide = 26785;

		// Token: 0x040006B4 RID: 1716
		public const ushort ConversationHasIrm = 26786;

		// Token: 0x040006B5 RID: 1717
		public const ushort ConversationHasIrmMailboxWide = 26787;

		// Token: 0x040006B6 RID: 1718
		public const ushort PersonCompanyNameMailboxWide = 26788;

		// Token: 0x040006B7 RID: 1719
		public const ushort PersonDisplayNameMailboxWide = 26789;

		// Token: 0x040006B8 RID: 1720
		public const ushort PersonGivenNameMailboxWide = 26790;

		// Token: 0x040006B9 RID: 1721
		public const ushort PersonSurnameMailboxWide = 26791;

		// Token: 0x040006BA RID: 1722
		public const ushort PersonPhotoContactEntryIdMailboxWide = 26792;

		// Token: 0x040006BB RID: 1723
		public const ushort ConversationInferredImportanceInternal = 26794;

		// Token: 0x040006BC RID: 1724
		public const ushort ConversationInferredImportanceOverride = 26795;

		// Token: 0x040006BD RID: 1725
		public const ushort ConversationInferredUnimportanceInternal = 26796;

		// Token: 0x040006BE RID: 1726
		public const ushort ConversationInferredImportanceInternalMailboxWide = 26797;

		// Token: 0x040006BF RID: 1727
		public const ushort ConversationInferredImportanceOverrideMailboxWide = 26798;

		// Token: 0x040006C0 RID: 1728
		public const ushort ConversationInferredUnimportanceInternalMailboxWide = 26799;

		// Token: 0x040006C1 RID: 1729
		public const ushort PersonFileAsMailboxWide = 26800;

		// Token: 0x040006C2 RID: 1730
		public const ushort PersonRelevanceScoreMailboxWide = 26801;

		// Token: 0x040006C3 RID: 1731
		public const ushort PersonIsDistributionListMailboxWide = 26802;

		// Token: 0x040006C4 RID: 1732
		public const ushort PersonHomeCityMailboxWide = 26803;

		// Token: 0x040006C5 RID: 1733
		public const ushort PersonCreationTimeMailboxWide = 26804;

		// Token: 0x040006C6 RID: 1734
		public const ushort PersonGALLinkIDMailboxWide = 26807;

		// Token: 0x040006C7 RID: 1735
		public const ushort PersonMvEmailAddressMailboxWide = 26810;

		// Token: 0x040006C8 RID: 1736
		public const ushort PersonMvEmailDisplayNameMailboxWide = 26811;

		// Token: 0x040006C9 RID: 1737
		public const ushort PersonMvEmailRoutingTypeMailboxWide = 26812;

		// Token: 0x040006CA RID: 1738
		public const ushort PersonImAddressMailboxWide = 26813;

		// Token: 0x040006CB RID: 1739
		public const ushort PersonWorkCityMailboxWide = 26814;

		// Token: 0x040006CC RID: 1740
		public const ushort ConversationGroupingActions = 26814;

		// Token: 0x040006CD RID: 1741
		public const ushort PersonDisplayNameFirstLastMailboxWide = 26815;

		// Token: 0x040006CE RID: 1742
		public const ushort ConversationGroupingActionsMailboxWide = 26815;

		// Token: 0x040006CF RID: 1743
		public const ushort PersonDisplayNameLastFirstMailboxWide = 26816;

		// Token: 0x040006D0 RID: 1744
		public const ushort ConversationPredictedActionsSummary = 26816;

		// Token: 0x040006D1 RID: 1745
		public const ushort ConversationPredictedActionsSummaryMailboxWide = 26817;

		// Token: 0x040006D2 RID: 1746
		public const ushort ConversationHasClutter = 26818;

		// Token: 0x040006D3 RID: 1747
		public const ushort ConversationHasClutterMailboxWide = 26819;

		// Token: 0x040006D4 RID: 1748
		public const ushort ConversationLastMemberDocumentId = 26880;

		// Token: 0x040006D5 RID: 1749
		public const ushort ConversationPreview = 26881;

		// Token: 0x040006D6 RID: 1750
		public const ushort ConversationLastMemberDocumentIdMailboxWide = 26882;

		// Token: 0x040006D7 RID: 1751
		public const ushort ConversationInitialMemberDocumentId = 26883;

		// Token: 0x040006D8 RID: 1752
		public const ushort ConversationMemberDocumentIds = 26884;

		// Token: 0x040006D9 RID: 1753
		public const ushort ConversationMessageDeliveryOrRenewTimeMailboxWide = 26885;

		// Token: 0x040006DA RID: 1754
		public const ushort NDRFromName = 26885;

		// Token: 0x040006DB RID: 1755
		public const ushort FamilyId = 26886;

		// Token: 0x040006DC RID: 1756
		public const ushort ConversationMessageRichContentMailboxWide = 26887;

		// Token: 0x040006DD RID: 1757
		public const ushort ConversationPreviewMailboxWide = 26888;

		// Token: 0x040006DE RID: 1758
		public const ushort ConversationMessageDeliveryOrRenewTime = 26889;

		// Token: 0x040006DF RID: 1759
		public const ushort AttachEXCLIVersion = 26889;

		// Token: 0x040006E0 RID: 1760
		public const ushort ConversationWorkingSetSourcePartition = 26890;

		// Token: 0x040006E1 RID: 1761
		public const ushort SecurityFlags = 28161;

		// Token: 0x040006E2 RID: 1762
		public const ushort SecurityReceiptRequestProcessed = 28164;

		// Token: 0x040006E3 RID: 1763
		public const ushort FavoriteDisplayName = 31744;

		// Token: 0x040006E4 RID: 1764
		public const ushort FavoriteDisplayAlias = 31745;

		// Token: 0x040006E5 RID: 1765
		public const ushort FavPublicSourceKey = 31746;

		// Token: 0x040006E6 RID: 1766
		public const ushort SyncCustomState = 31746;

		// Token: 0x040006E7 RID: 1767
		public const ushort SyncFolderSourceKey = 31747;

		// Token: 0x040006E8 RID: 1768
		public const ushort SyncFolderChangeKey = 31748;

		// Token: 0x040006E9 RID: 1769
		public const ushort SyncFolderLastModificationTime = 31749;

		// Token: 0x040006EA RID: 1770
		public const ushort UserConfigurationDataType = 31750;

		// Token: 0x040006EB RID: 1771
		public const ushort UserConfigurationXmlStream = 31752;

		// Token: 0x040006EC RID: 1772
		public const ushort UserConfigurationStream = 31753;

		// Token: 0x040006ED RID: 1773
		public const ushort ptagSyncState = 31754;

		// Token: 0x040006EE RID: 1774
		public const ushort ReplyFwdStatus = 31755;

		// Token: 0x040006EF RID: 1775
		public const ushort UserPhotoCacheId = 31770;

		// Token: 0x040006F0 RID: 1776
		public const ushort UserPhotoPreviewCacheId = 31771;

		// Token: 0x040006F1 RID: 1777
		public const ushort OscSyncEnabledOnServer = 31780;

		// Token: 0x040006F2 RID: 1778
		public const ushort Processed = 32001;

		// Token: 0x040006F3 RID: 1779
		public const ushort FavLevelMask = 32003;

		// Token: 0x040006F4 RID: 1780
		public const ushort HasDlpDetectedAttachmentClassifications = 32760;

		// Token: 0x040006F5 RID: 1781
		public const ushort SExceptionReplaceTime = 32761;

		// Token: 0x040006F6 RID: 1782
		public const ushort AttachmentLinkId = 32762;

		// Token: 0x040006F7 RID: 1783
		public const ushort ExceptionStartTime = 32763;

		// Token: 0x040006F8 RID: 1784
		public const ushort ExceptionEndTime = 32764;

		// Token: 0x040006F9 RID: 1785
		public const ushort AttachmentFlags2 = 32765;

		// Token: 0x040006FA RID: 1786
		public const ushort AttachmentHidden = 32766;

		// Token: 0x040006FB RID: 1787
		public const ushort AttachmentContactPhoto = 32767;
	}
}
