using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200014E RID: 334
	public sealed class ItemClassType
	{
		// Token: 0x06000B7F RID: 2943 RVA: 0x0005032A File Offset: 0x0004E52A
		public static bool IsReportType(string itemType)
		{
			if (itemType == null)
			{
				throw new ArgumentNullException("itemType");
			}
			return itemType.IndexOf("REPORT", StringComparison.OrdinalIgnoreCase) != -1;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0005034C File Offset: 0x0004E54C
		public static bool IsMeetingType(string itemType)
		{
			if (itemType == null)
			{
				throw new ArgumentNullException("itemType");
			}
			return itemType.IndexOf("IPM.Schedule.Meeting", StringComparison.OrdinalIgnoreCase) != -1;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0005036E File Offset: 0x0004E56E
		public static bool IsSmsType(string itemType)
		{
			if (itemType == null)
			{
				throw new ArgumentNullException("itemType");
			}
			return itemType.IndexOf("IPM.Note.Mobile.SMS", StringComparison.OrdinalIgnoreCase) != -1;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00050390 File Offset: 0x0004E590
		public static string GetDisplayString(string itemType)
		{
			switch (itemType)
			{
			case "IPM.Appointment":
				return LocalizedStrings.GetNonEncoded(-1218353654);
			case "IPM.Note.Microsoft.Approval.Request":
				return LocalizedStrings.GetNonEncoded(-1921998649);
			case "IPM.Note.Microsoft.Approval.Reply.Approve":
				return LocalizedStrings.GetNonEncoded(2134275567);
			case "IPM.Note.Microsoft.Approval.Reply.Reject":
				return LocalizedStrings.GetNonEncoded(617284623);
			case "IPM.Conflict.Folder":
				return LocalizedStrings.GetNonEncoded(-949566239);
			case "IPM.Conflict.Message":
				return LocalizedStrings.GetNonEncoded(-1125004178);
			case "IPM.Contact":
				return LocalizedStrings.GetNonEncoded(1212144717);
			case "REPORT.IPM.Note.DR":
				return LocalizedStrings.GetNonEncoded(-673778217);
			case "REPORT.REPORT.IPM.Note.DR.NDR":
				return LocalizedStrings.GetNonEncoded(-1214411457);
			case "IPM.NOTE.SECURE.SIGN":
				return LocalizedStrings.GetNonEncoded(-1744898725);
			case "IPM.Note.Secure.Sign.Reply":
				return LocalizedStrings.GetNonEncoded(-1638959343);
			case "IPM.DistList":
				return LocalizedStrings.GetNonEncoded(-257188171);
			case "IPM.Document":
				return LocalizedStrings.GetNonEncoded(1894440736);
			case "IPM.Document.Outlook.Template":
				return LocalizedStrings.GetNonEncoded(936058579);
			case "IPM.Note.Exchange.Security.Enrollment":
				return LocalizedStrings.GetNonEncoded(-1728547674);
			case "IPM.Note.Microsoft.Fax.CA":
				return LocalizedStrings.GetNonEncoded(441553720);
			case "IPM.Schedule.Meeting.Resp.Pos":
				return LocalizedStrings.GetNonEncoded(-1480422595);
			case "REPORT.IPM.Schedule.Meeting.Resp.Pos.NDR":
				return LocalizedStrings.GetNonEncoded(1578126677);
			case "IPM.Schedule.Meeting.Canceled":
				return LocalizedStrings.GetNonEncoded(-1395325573);
			case "REPORT.IPM.Schedule.Meeting.Canceled.NDR":
				return LocalizedStrings.GetNonEncoded(2117886819);
			case "IPM.Schedule.Meeting.Resp.Neg":
				return LocalizedStrings.GetNonEncoded(1577758192);
			case "REPORT.IPM.Schedule.Meeting.Resp.Neg.NDR":
				return LocalizedStrings.GetNonEncoded(501159136);
			case "IPM.Schedule.Meeting.Request":
				return LocalizedStrings.GetNonEncoded(715990345);
			case "REPORT.IPM.Schedule.Meeting.Request.DR":
				return LocalizedStrings.GetNonEncoded(1722709255);
			case "REPORT.IPM.Schedule.Meeting.Request.NDR":
				return LocalizedStrings.GetNonEncoded(723650401);
			case "REPORT.IPM.Schedule.Meeting.Request.IPNRN":
				return LocalizedStrings.GetNonEncoded(247246403);
			case "IPM.Schedule.Meeting.Resp.Tent":
				return LocalizedStrings.GetNonEncoded(418650720);
			case "REPORT.IPM.Schedule.Meeting.Resp.Tent.NDR":
				return LocalizedStrings.GetNonEncoded(1657585888);
			case "IPM.Note":
				return LocalizedStrings.GetNonEncoded(375540844);
			case "IPM.Microsoft.Answer":
				return LocalizedStrings.GetNonEncoded(1418057561);
			case "REPORT.IPM.Microsoft.Answer.NDR":
				return LocalizedStrings.GetNonEncoded(-2092364623);
			case "IPM.Document.Microsoft Internet Mail Message":
				return LocalizedStrings.GetNonEncoded(569182364);
			case "REPORT.IPM.Note.NDR":
				return LocalizedStrings.GetNonEncoded(-240308911);
			case "IPM.OCTEL.VOICE":
				return LocalizedStrings.GetNonEncoded(1324666722);
			case "REPORT.IPM.OCTEL.VOICE.NDR":
				return LocalizedStrings.GetNonEncoded(1751917506);
			case "IPM.Note.Rules.OofTemplate.Microsoft":
				return LocalizedStrings.GetNonEncoded(-445260260);
			case "IPM.Outlook.Recall":
				return LocalizedStrings.GetNonEncoded(-1730664879);
			case "IPM.Post":
				return LocalizedStrings.GetNonEncoded(1671058613);
			case "REPORT.IPM.Note.IPNNRN":
				return LocalizedStrings.GetNonEncoded(1317782997);
			case "REPORT.REPORT.IPM.Note.IPNNRN.NDR":
				return LocalizedStrings.GetNonEncoded(-383020123);
			case "REPORT.IPM.Note.IPNRN":
				return LocalizedStrings.GetNonEncoded(-44453782);
			case "IPM.Recall":
				return LocalizedStrings.GetNonEncoded(-1875477896);
			case "IPM.Recall.Report.Failure":
				return LocalizedStrings.GetNonEncoded(-2103299596);
			case "IPM.Recall.Report.Success":
				return LocalizedStrings.GetNonEncoded(-1247003399);
			case "IPM.Sharing":
				return LocalizedStrings.GetNonEncoded(-958660555);
			case "IPM.Note.SMIME":
				return LocalizedStrings.GetNonEncoded(-1021771534);
			case "IPM.Note.SMIME.MultipartSigned":
				return LocalizedStrings.GetNonEncoded(-246092698);
			case "IPM.Note.Mobile.SMS":
				return LocalizedStrings.GetNonEncoded(629771022);
			case "IPM.Task":
				return LocalizedStrings.GetNonEncoded(-2113219524);
			case "IPM.TaskRequest":
				return LocalizedStrings.GetNonEncoded(2041022245);
			case "IPM.TaskRequest.Accept":
				return LocalizedStrings.GetNonEncoded(-791766735);
			case "REPORT.IPM.TaskRequest.Accept.NDR":
				return LocalizedStrings.GetNonEncoded(-1588693551);
			case "IPM.TaskRequest.Decline":
				return LocalizedStrings.GetNonEncoded(1476481161);
			case "REPORT.IPM.TaskRequest.Decline.NDR":
				return LocalizedStrings.GetNonEncoded(61479553);
			case "REPORT.IPM.TaskRequest.NDR":
				return LocalizedStrings.GetNonEncoded(561961253);
			case "IPM.TaskRequest.Update":
				return LocalizedStrings.GetNonEncoded(332739048);
			case "REPORT.IPM.TaskRequest.Update.NDR":
				return LocalizedStrings.GetNonEncoded(-707557640);
			case "IPM.Note.Microsoft.Voicemail.UM":
			case "IPM.Note.Microsoft.Voicemail.UM.CA":
			case "IPM.Note.Microsoft.Exchange.Voice.UM":
			case "IPM.Note.Microsoft.Exchange.Voice.UM.CA":
			case "IPM.Note.rpmsg.Microsoft.Voicemail.UM":
			case "IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA":
				return LocalizedStrings.GetNonEncoded(-1757037630);
			}
			return LocalizedStrings.GetNonEncoded(-1718015515);
		}

		// Token: 0x04000806 RID: 2054
		public const string ActiveSyncNote = "IPM.Note.Exchange.ActiveSync";

		// Token: 0x04000807 RID: 2055
		public const string Activity = "IPM.Activity";

		// Token: 0x04000808 RID: 2056
		public const string Appointment = "IPM.Appointment";

		// Token: 0x04000809 RID: 2057
		public const string ADUser = "AD.RecipientType.User";

		// Token: 0x0400080A RID: 2058
		public const string ADMailUser = "AD.RecipientType.MailboxUser";

		// Token: 0x0400080B RID: 2059
		public const string ADMailEnabledUser = "AD.RecipientType.MailEnabledUser";

		// Token: 0x0400080C RID: 2060
		public const string ADContact = "AD.RecipientType.Contact";

		// Token: 0x0400080D RID: 2061
		public const string ADMailEnabledContact = "AD.RecipientType.MailEnabledContact";

		// Token: 0x0400080E RID: 2062
		public const string ADPublicFolder = "AD.RecipientType.PublicFolder";

		// Token: 0x0400080F RID: 2063
		public const string ADGroup = "AD.RecipientType.Group";

		// Token: 0x04000810 RID: 2064
		public const string ADDynamicDL = "AD.RecipientType.DynamicDL";

		// Token: 0x04000811 RID: 2065
		public const string ADMailEnabledUniversalDistributionGroup = "AD.RecipientType.MailEnabledUniversalDistributionGroup";

		// Token: 0x04000812 RID: 2066
		public const string ADMailEnabledUniversalSecurityGroup = "AD.RecipientType.MailEnabledUniversalSecurityGroup";

		// Token: 0x04000813 RID: 2067
		public const string ADMailEnabledNonUniversalGroup = "AD.RecipientType.MailEnabledNonUniversalGroup";

		// Token: 0x04000814 RID: 2068
		public const string ADPublicDatabase = "AD.RecipientType.PublicDatabase";

		// Token: 0x04000815 RID: 2069
		public const string ADAttendantMailbox = "AD.RecipientType.SystemAttendantMailbox";

		// Token: 0x04000816 RID: 2070
		public const string ADRoom = "AD.ResourceType.Room";

		// Token: 0x04000817 RID: 2071
		public const string ADInvalidUser = "AD.RecipientType.Invalid";

		// Token: 0x04000818 RID: 2072
		public const string ApprovalRequest = "IPM.Note.Microsoft.Approval.Request";

		// Token: 0x04000819 RID: 2073
		public const string ApprovalReplyApprove = "IPM.Note.Microsoft.Approval.Reply.Approve";

		// Token: 0x0400081A RID: 2074
		public const string ApprovalReplyReject = "IPM.Note.Microsoft.Approval.Reply.Reject";

		// Token: 0x0400081B RID: 2075
		public const string Contact = "IPM.Contact";

		// Token: 0x0400081C RID: 2076
		public const string ConflictFolder = "IPM.Conflict.Folder";

		// Token: 0x0400081D RID: 2077
		public const string ConflictMessage = "IPM.Conflict.Message";

		// Token: 0x0400081E RID: 2078
		public const string ContentClassDefinition = "IPM.ContentClassDef";

		// Token: 0x0400081F RID: 2079
		public const string DeliveryReport = "REPORT.IPM.Note.DR";

		// Token: 0x04000820 RID: 2080
		public const string DeliveryReportNDR = "REPORT.REPORT.IPM.Note.DR.NDR";

		// Token: 0x04000821 RID: 2081
		public const string EncryptedUnsignedMessage = "IPM.NOTE.SECURE";

		// Token: 0x04000822 RID: 2082
		public const string DigitallySignedMessage = "IPM.NOTE.SECURE.SIGN";

		// Token: 0x04000823 RID: 2083
		public const string DigitallySignedMessageReply = "IPM.Note.Secure.Sign.Reply";

		// Token: 0x04000824 RID: 2084
		public const string DistributionList = "IPM.DistList";

		// Token: 0x04000825 RID: 2085
		public const string Document = "IPM.Document";

		// Token: 0x04000826 RID: 2086
		public const string DocumentOutlookTemplate = "IPM.Document.Outlook.Template";

		// Token: 0x04000827 RID: 2087
		public const string ExchangeSecurityEnrollment = "IPM.Note.Exchange.Security.Enrollment";

		// Token: 0x04000828 RID: 2088
		public const string Fax = "IPM.Note.Microsoft.Fax.CA";

		// Token: 0x04000829 RID: 2089
		public const string FreeBusyData = "IPM.Microsoft.ScheduleData.FreeBusy";

		// Token: 0x0400082A RID: 2090
		public const string E12Beta1Fax = "IPM.Note.Microsoft.Exchange.Fax.CA";

		// Token: 0x0400082B RID: 2091
		public const string InfoPathForm = "IPM.InfoPathForm";

		// Token: 0x0400082C RID: 2092
		public const string InkNodes = "IPM.InkNodes";

		// Token: 0x0400082D RID: 2093
		public const string MeetingAcceptance = "IPM.Schedule.Meeting.Resp.Pos";

		// Token: 0x0400082E RID: 2094
		public const string MeetingAcceptanceNDR = "REPORT.IPM.Schedule.Meeting.Resp.Pos.NDR";

		// Token: 0x0400082F RID: 2095
		public const string MeetingCancelled = "IPM.Schedule.Meeting.Canceled";

		// Token: 0x04000830 RID: 2096
		public const string MeetingCancelledNDR = "REPORT.IPM.Schedule.Meeting.Canceled.NDR";

		// Token: 0x04000831 RID: 2097
		public const string MeetingDecline = "IPM.Schedule.Meeting.Resp.Neg";

		// Token: 0x04000832 RID: 2098
		public const string MeetingDeclineNDR = "REPORT.IPM.Schedule.Meeting.Resp.Neg.NDR";

		// Token: 0x04000833 RID: 2099
		public const string MeetingRequest = "IPM.Schedule.Meeting.Request";

		// Token: 0x04000834 RID: 2100
		public const string MeetingRequestDR = "REPORT.IPM.Schedule.Meeting.Request.DR";

		// Token: 0x04000835 RID: 2101
		public const string MeetingRequestNDR = "REPORT.IPM.Schedule.Meeting.Request.NDR";

		// Token: 0x04000836 RID: 2102
		public const string MeetingRequestReadReceipt = "REPORT.IPM.Schedule.Meeting.Request.IPNRN";

		// Token: 0x04000837 RID: 2103
		public const string MeetingTentative = "IPM.Schedule.Meeting.Resp.Tent";

		// Token: 0x04000838 RID: 2104
		public const string MeetingTentativeNDR = "REPORT.IPM.Schedule.Meeting.Resp.Tent.NDR";

		// Token: 0x04000839 RID: 2105
		public const string Message = "IPM.Note";

		// Token: 0x0400083A RID: 2106
		public const string MicrosoftAnswer = "IPM.Microsoft.Answer";

		// Token: 0x0400083B RID: 2107
		public const string MicrosoftAnswerNDR = "REPORT.IPM.Microsoft.Answer.NDR";

		// Token: 0x0400083C RID: 2108
		public const string MicrosoftInternetMailMessage = "IPM.Document.Microsoft Internet Mail Message";

		// Token: 0x0400083D RID: 2109
		public const string NoteNDR = "REPORT.IPM.Note.NDR";

		// Token: 0x0400083E RID: 2110
		public const string OctelVoice = "IPM.OCTEL.VOICE";

		// Token: 0x0400083F RID: 2111
		public const string OctelVoiceNDR = "REPORT.IPM.OCTEL.VOICE.NDR";

		// Token: 0x04000840 RID: 2112
		public const string OofAutoReply = "IPM.Note.Rules.OofTemplate.Microsoft";

		// Token: 0x04000841 RID: 2113
		public const string OutlookRecall = "IPM.Outlook.Recall";

		// Token: 0x04000842 RID: 2114
		public const string Post = "IPM.Post";

		// Token: 0x04000843 RID: 2115
		public const string ReadReceiptFail = "REPORT.IPM.Note.IPNNRN";

		// Token: 0x04000844 RID: 2116
		public const string ReadReceiptFailNDR = "REPORT.REPORT.IPM.Note.IPNNRN.NDR";

		// Token: 0x04000845 RID: 2117
		public const string ReadReceiptSuccess = "REPORT.IPM.Note.IPNRN";

		// Token: 0x04000846 RID: 2118
		public const string Recall = "IPM.Recall";

		// Token: 0x04000847 RID: 2119
		public const string RecallFailureReport = "IPM.Recall.Report.Failure";

		// Token: 0x04000848 RID: 2120
		public const string RecallSuccessReport = "IPM.Recall.Report.Success";

		// Token: 0x04000849 RID: 2121
		public const string RulesNote = "IPM.Note.Rules";

		// Token: 0x0400084A RID: 2122
		public const string Sharing = "IPM.Sharing";

		// Token: 0x0400084B RID: 2123
		public const string SMIME = "IPM.Note.SMIME";

		// Token: 0x0400084C RID: 2124
		public const string SMIMESigned = "IPM.Note.SMIME.MultipartSigned";

		// Token: 0x0400084D RID: 2125
		public const string SMS = "IPM.Note.Mobile.SMS";

		// Token: 0x0400084E RID: 2126
		public const string Task = "IPM.Task";

		// Token: 0x0400084F RID: 2127
		public const string TaskRequest = "IPM.TaskRequest";

		// Token: 0x04000850 RID: 2128
		public const string TaskRequestNDR = "REPORT.IPM.TaskRequest.NDR";

		// Token: 0x04000851 RID: 2129
		public const string TaskRequestAccept = "IPM.TaskRequest.Accept";

		// Token: 0x04000852 RID: 2130
		public const string TaskRequestAcceptNDR = "REPORT.IPM.TaskRequest.Accept.NDR";

		// Token: 0x04000853 RID: 2131
		public const string TaskRequestDecline = "IPM.TaskRequest.Decline";

		// Token: 0x04000854 RID: 2132
		public const string TaskRequestDeclineNDR = "REPORT.IPM.TaskRequest.Decline.NDR";

		// Token: 0x04000855 RID: 2133
		public const string TaskRequestUpdate = "IPM.TaskRequest.Update";

		// Token: 0x04000856 RID: 2134
		public const string TaskRequestUpdateNDR = "REPORT.IPM.TaskRequest.Update.NDR";

		// Token: 0x04000857 RID: 2135
		public const string VoiceMail = "IPM.Note.Microsoft.Voicemail.UM";

		// Token: 0x04000858 RID: 2136
		public const string VoiceMailCA = "IPM.Note.Microsoft.Voicemail.UM.CA";

		// Token: 0x04000859 RID: 2137
		public const string E12Beta1VoiceMail = "IPM.Note.Microsoft.Exchange.Voice.UM";

		// Token: 0x0400085A RID: 2138
		public const string E12Beta1VoiceMailCA = "IPM.Note.Microsoft.Exchange.Voice.UM.CA";

		// Token: 0x0400085B RID: 2139
		public const string ProtectedVoiceMail = "IPM.Note.rpmsg.Microsoft.Voicemail.UM";

		// Token: 0x0400085C RID: 2140
		public const string ProtectedVoiceMailCA = "IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA";
	}
}
