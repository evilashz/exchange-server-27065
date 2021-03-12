using System;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000DE RID: 222
	internal static class MessageClasses
	{
		// Token: 0x04000348 RID: 840
		public const string IpmNote = "IPM.Note";

		// Token: 0x04000349 RID: 841
		public const string IpmForm = "IPM.Form";

		// Token: 0x0400034A RID: 842
		public const string IpmNoteSMime = "IPM.Note.SMIME";

		// Token: 0x0400034B RID: 843
		public const string SmimeEncryptedSuffix = ".SMIME";

		// Token: 0x0400034C RID: 844
		public const string IpmNoteSMimeMultipartSigned = "IPM.Note.SMIME.MultipartSigned";

		// Token: 0x0400034D RID: 845
		public const string SmimeSignedSuffix = ".SMIME.MultipartSigned";

		// Token: 0x0400034E RID: 846
		public const string IpmNoteSecureSign = "IPM.Note.Secure.Sign";

		// Token: 0x0400034F RID: 847
		public const string IpmNoteSecure = "IPM.Note.Secure";

		// Token: 0x04000350 RID: 848
		public const string IpmAppointment = "IPM.Appointment";

		// Token: 0x04000351 RID: 849
		public const string IpmTaskRequest = "IPM.TaskRequest";

		// Token: 0x04000352 RID: 850
		public const string IpmVoice = "IPM.Note.Microsoft.Voicemail.UM";

		// Token: 0x04000353 RID: 851
		public const string IpmVoiceCa = "IPM.Note.Microsoft.Voicemail.UM.CA";

		// Token: 0x04000354 RID: 852
		public const string IpmVoiceProtectedCa = "IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA";

		// Token: 0x04000355 RID: 853
		public const string IpmVoiceProtected = "IPM.Note.rpmsg.Microsoft.Voicemail.UM";

		// Token: 0x04000356 RID: 854
		public const string IpmFax = "IPM.Note.Microsoft.Fax";

		// Token: 0x04000357 RID: 855
		public const string IpmFaxCa = "IPM.Note.Microsoft.Fax.CA";

		// Token: 0x04000358 RID: 856
		public const string IpmMissedCall = "IPM.Note.Microsoft.Missed.Voice";

		// Token: 0x04000359 RID: 857
		public const string IpmVoiceUc = "IPM.Note.Microsoft.Conversation.Voice";

		// Token: 0x0400035A RID: 858
		public const string IpmUMPartner = "IPM.Note.Microsoft.Partner.UM";

		// Token: 0x0400035B RID: 859
		public const string InfoPathMessageClass = "IPM.InfoPathForm";

		// Token: 0x0400035C RID: 860
		public const string PrefixIpmNoteMobile = "IPM.Note.Mobile.";

		// Token: 0x0400035D RID: 861
		public const string IpmNoteMobileSms = "IPM.Note.Mobile.SMS";

		// Token: 0x0400035E RID: 862
		public const string IpmNoteMobileMms = "IPM.Note.Mobile.MMS";

		// Token: 0x0400035F RID: 863
		internal const string CustomMessageClass = "IPM.Note.Custom";

		// Token: 0x04000360 RID: 864
		public const string IpmReplication = "IPM.Replication";

		// Token: 0x04000361 RID: 865
		public const string IpmConflictMessage = "IPM.Conflict.Message";

		// Token: 0x04000362 RID: 866
		public const string IpmConflictFolder = "IPM.Conflict.Folder";

		// Token: 0x04000363 RID: 867
		public const string IpmOutlookRecall = "IPM.Outlook.Recall";

		// Token: 0x04000364 RID: 868
		public const string PrefixIpmForm = "IPM.Form.";

		// Token: 0x04000365 RID: 869
		public const string PrefixIpmNoteRulesReplyTemplate = "IPM.Note.Rules.ReplyTemplate.";

		// Token: 0x04000366 RID: 870
		public const string PrefixIpmNoteRulesExternalOofTemplate = "IPM.Note.Rules.ExternalOofTemplate.";

		// Token: 0x04000367 RID: 871
		public const string PrefixIpmNoteRulesOofTemplate = "IPM.Note.Rules.OofTemplate.";

		// Token: 0x04000368 RID: 872
		public const string PrefixReportIpmNote = "Report.IPM.Note.";

		// Token: 0x04000369 RID: 873
		public const string SrvInfoExpiry = "SrvInfo.Expiry";

		// Token: 0x0400036A RID: 874
		public const string RssPost = "IPM.Post.RSS";

		// Token: 0x0400036B RID: 875
		public const string IpmSharing = "IPM.Sharing";

		// Token: 0x0400036C RID: 876
		public const string PrefixIpmDocument = "IPM.Document.";

		// Token: 0x0400036D RID: 877
		public const string PrefixIpmRecallReport = "IPM.Recall.Report.";

		// Token: 0x0400036E RID: 878
		public const string PrefixIpmMailbeatBounce = "IPM.Mailbeat.Bounce.";

		// Token: 0x0400036F RID: 879
		public const string IpmNoteStorageQuotaWarning = "IPM.Note.StorageQuotaWarning";

		// Token: 0x04000370 RID: 880
		public const string PrefixIpmNoteStorageQuotaWarning = "IPM.Note.StorageQuotaWarning.";

		// Token: 0x04000371 RID: 881
		public const string IpmNoteStorageQuotaWarningWarning = "IPM.Note.StorageQuotaWarning.Warning";

		// Token: 0x04000372 RID: 882
		public const string IpmNoteStorageQuotaWarningSend = "IPM.Note.StorageQuotaWarning.Send";

		// Token: 0x04000373 RID: 883
		public const string IpmNoteStorageQuotaWarningSendReceive = "IPM.Note.StorageQuotaWarning.SendReceive";

		// Token: 0x04000374 RID: 884
		public const string PrefixIpmScheduleMeeting = "IPM.Schedule.Meeting.";

		// Token: 0x04000375 RID: 885
		public const string IpmScheduleMeetingRequest = "IPM.Schedule.Meeting.Request";

		// Token: 0x04000376 RID: 886
		public const string IpmScheduleMeetingRespNeg = "IPM.Schedule.Meeting.Resp.Neg";

		// Token: 0x04000377 RID: 887
		public const string IpmScheduleMeetingRespPos = "IPM.Schedule.Meeting.Resp.Pos";

		// Token: 0x04000378 RID: 888
		public const string IpmScheduleMeetingRespTent = "IPM.Schedule.Meeting.Resp.Tent";

		// Token: 0x04000379 RID: 889
		public const string IpmScheduleMeetingCanceled = "IPM.Schedule.Meeting.Canceled";

		// Token: 0x0400037A RID: 890
		public const string ApprovalInitiationMessageClass = "IPM.Microsoft.Approval.Initiation";

		// Token: 0x0400037B RID: 891
		public const string PrefixApprovalMessageClass = "IPM.Note.Microsoft.Approval.";

		// Token: 0x020000DF RID: 223
		public static class MobileMessageSuffix
		{
			// Token: 0x0400037C RID: 892
			public const string ShortMessage = "SMS";

			// Token: 0x0400037D RID: 893
			public const string MultimediaMessage = "MMS";
		}

		// Token: 0x020000E0 RID: 224
		public static class ReportSuffix
		{
			// Token: 0x0400037E RID: 894
			public const string DsnFailed = "NDR";

			// Token: 0x0400037F RID: 895
			public const string DsnDelivered = "DR";

			// Token: 0x04000380 RID: 896
			public const string DsnDelayed = "Delayed.DR";

			// Token: 0x04000381 RID: 897
			public const string DsnRelayed = "Relayed.DR";

			// Token: 0x04000382 RID: 898
			public const string DsnExpanded = "Expanded.DR";

			// Token: 0x04000383 RID: 899
			public const string MdnRead = "IPNRN";

			// Token: 0x04000384 RID: 900
			public const string MdnNotRead = "IPNNRN";
		}

		// Token: 0x020000E1 RID: 225
		public static class RecallReportSuffix
		{
			// Token: 0x04000385 RID: 901
			public const string Success = "Success";

			// Token: 0x04000386 RID: 902
			public const string Failure = "Failure";
		}

		// Token: 0x020000E2 RID: 226
		public static class MailbeatBounceSuffix
		{
			// Token: 0x04000387 RID: 903
			public const string Reply = "Reply";

			// Token: 0x04000388 RID: 904
			public const string Request = "Request";
		}

		// Token: 0x020000E3 RID: 227
		public static class StorageQuotaWarningSuffix
		{
			// Token: 0x04000389 RID: 905
			public const string Warning = "Warning";

			// Token: 0x0400038A RID: 906
			public const string Send = "Send";

			// Token: 0x0400038B RID: 907
			public const string SendReceive = "SendReceive";
		}

		// Token: 0x020000E4 RID: 228
		public static class ScheduleSuffix
		{
			// Token: 0x0400038C RID: 908
			public const string Request = "Request";

			// Token: 0x0400038D RID: 909
			public const string Canceled = "Canceled";

			// Token: 0x0400038E RID: 910
			public const string RespNeg = "Resp.Neg";

			// Token: 0x0400038F RID: 911
			public const string RespPos = "Resp.Pos";

			// Token: 0x04000390 RID: 912
			public const string RespTent = "Resp.Tent";
		}
	}
}
