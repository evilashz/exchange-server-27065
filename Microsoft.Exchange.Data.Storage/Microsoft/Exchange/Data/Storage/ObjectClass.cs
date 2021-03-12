using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000856 RID: 2134
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ObjectClass
	{
		// Token: 0x0600500F RID: 20495 RVA: 0x0014D5E5 File Offset: 0x0014B7E5
		public static bool IsApprovalMessage(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Note.Microsoft.Approval");
		}

		// Token: 0x06005010 RID: 20496 RVA: 0x0014D5F2 File Offset: 0x0014B7F2
		public static bool IsCalendarItemOccurrence(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Appointment.Occurrence");
		}

		// Token: 0x06005011 RID: 20497 RVA: 0x0014D5FF File Offset: 0x0014B7FF
		public static bool IsCalendarItemCalendarItemOccurrenceOrRecurrenceException(string itemClass)
		{
			return ObjectClass.IsCalendarItemOrOccurrence(itemClass) || ObjectClass.IsRecurrenceException(itemClass);
		}

		// Token: 0x06005012 RID: 20498 RVA: 0x0014D611 File Offset: 0x0014B811
		public static bool IsRecurrenceException(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.OLE.CLASS.{00061055-0000-0000-C000-000000000046}");
		}

		// Token: 0x06005013 RID: 20499 RVA: 0x0014D61E File Offset: 0x0014B81E
		public static bool IsCalendarItem(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Appointment");
		}

		// Token: 0x06005014 RID: 20500 RVA: 0x0014D62B File Offset: 0x0014B82B
		public static bool IsCalendarItemSeries(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.AppointmentSeries");
		}

		// Token: 0x06005015 RID: 20501 RVA: 0x0014D638 File Offset: 0x0014B838
		public static bool IsCalendarItemOrOccurrence(string itemClass)
		{
			return ObjectClass.IsCalendarItem(itemClass) || ObjectClass.IsCalendarItemOccurrence(itemClass);
		}

		// Token: 0x06005016 RID: 20502 RVA: 0x0014D64A File Offset: 0x0014B84A
		public static bool IsFolderTreeData(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Microsoft.WunderBar.Link", false);
		}

		// Token: 0x06005017 RID: 20503 RVA: 0x0014D658 File Offset: 0x0014B858
		public static bool IsMessage(string itemClass, bool includeCalendaringMessages = false)
		{
			bool flag = ObjectClass.IsOfClass(itemClass, "IPM.Note") || ObjectClass.IsOfClass(itemClass, "REPORT");
			if (includeCalendaringMessages)
			{
				flag |= (ObjectClass.IsMeetingMessage(itemClass) || ObjectClass.IsMeetingMessageSeries(itemClass));
			}
			return flag;
		}

		// Token: 0x06005018 RID: 20504 RVA: 0x0014D699 File Offset: 0x0014B899
		public static bool IsPost(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Post");
		}

		// Token: 0x06005019 RID: 20505 RVA: 0x0014D6A6 File Offset: 0x0014B8A6
		public static bool IsReport(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "REPORT") || ObjectClass.ReportClasses.IsReportOfSpecialCasedClass(itemClass);
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x0014D6BD File Offset: 0x0014B8BD
		public static bool IsReport(string itemClass, string reportSuffixOrClass)
		{
			return (ObjectClass.IsOfClass(itemClass, "REPORT") && ObjectClass.HasSuffix(itemClass, reportSuffixOrClass)) || (ObjectClass.ReportClasses.IsReportOfSpecialCasedClass(reportSuffixOrClass) && ObjectClass.IsOfClass(itemClass, reportSuffixOrClass));
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x0014D6E8 File Offset: 0x0014B8E8
		public static bool IsDsn(string itemClass)
		{
			return ObjectClass.IsDsnNegative(itemClass) || ObjectClass.IsDsnPositive(itemClass);
		}

		// Token: 0x0600501C RID: 20508 RVA: 0x0014D6FA File Offset: 0x0014B8FA
		public static bool IsDsnPositive(string itemClass)
		{
			return ObjectClass.IsReport(itemClass, "DR");
		}

		// Token: 0x0600501D RID: 20509 RVA: 0x0014D707 File Offset: 0x0014B907
		public static bool IsDsnNegative(string itemClass)
		{
			return ObjectClass.IsReport(itemClass, "NDR");
		}

		// Token: 0x0600501E RID: 20510 RVA: 0x0014D714 File Offset: 0x0014B914
		public static bool IsMdn(string itemClass)
		{
			return ObjectClass.IsReport(itemClass, "IPNRN") || ObjectClass.IsReport(itemClass, "IPNNRN");
		}

		// Token: 0x0600501F RID: 20511 RVA: 0x0014D730 File Offset: 0x0014B930
		public static bool IsMeetingMessageSeries(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.MeetingMessageSeries");
		}

		// Token: 0x06005020 RID: 20512 RVA: 0x0014D73D File Offset: 0x0014B93D
		public static bool IsMeetingMessage(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Schedule.Meeting") || ObjectClass.IsOfClass(itemClass, "IPM.Schedule.Meeting.Notification") || ObjectClass.IsOfClass(itemClass, "IPM.Notification.Meeting");
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x0014D766 File Offset: 0x0014B966
		public static bool IsMeetingRequest(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Schedule.Meeting.Request");
		}

		// Token: 0x06005022 RID: 20514 RVA: 0x0014D773 File Offset: 0x0014B973
		public static bool IsMeetingRequestSeries(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.MeetingMessageSeries.Request");
		}

		// Token: 0x06005023 RID: 20515 RVA: 0x0014D780 File Offset: 0x0014B980
		public static bool IsMeetingInquiry(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Schedule.Inquiry");
		}

		// Token: 0x06005024 RID: 20516 RVA: 0x0014D78D File Offset: 0x0014B98D
		public static bool IsMiddleTierRulesMessage(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Rule.Version2.Message");
		}

		// Token: 0x06005025 RID: 20517 RVA: 0x0014D79A File Offset: 0x0014B99A
		public static bool IsFailedInboundICal(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Note.NotSupportedICal");
		}

		// Token: 0x06005026 RID: 20518 RVA: 0x0014D7A7 File Offset: 0x0014B9A7
		public static bool IsMeetingResponse(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Schedule.Meeting.Resp");
		}

		// Token: 0x06005027 RID: 20519 RVA: 0x0014D7B4 File Offset: 0x0014B9B4
		public static bool IsMeetingResponseSeries(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.MeetingMessageSeries.Resp");
		}

		// Token: 0x06005028 RID: 20520 RVA: 0x0014D7C1 File Offset: 0x0014B9C1
		public static bool IsMeetingPositiveResponse(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Schedule.Meeting.Resp.Pos") || ObjectClass.IsOfClass(itemClass, "IPM.MeetingMessageSeries.Resp.Pos");
		}

		// Token: 0x06005029 RID: 20521 RVA: 0x0014D7DD File Offset: 0x0014B9DD
		public static bool IsMeetingNegativeResponse(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Schedule.Meeting.Resp.Neg") || ObjectClass.IsOfClass(itemClass, "IPM.MeetingMessageSeries.Resp.Neg");
		}

		// Token: 0x0600502A RID: 20522 RVA: 0x0014D7F9 File Offset: 0x0014B9F9
		public static bool IsMeetingTentativeResponse(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Schedule.Meeting.Resp.Tent") || ObjectClass.IsOfClass(itemClass, "IPM.MeetingMessageSeries.Resp.Tent");
		}

		// Token: 0x0600502B RID: 20523 RVA: 0x0014D815 File Offset: 0x0014BA15
		public static bool IsExternalSharingSubscription(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.ExternalSharingSubscription");
		}

		// Token: 0x0600502C RID: 20524 RVA: 0x0014D822 File Offset: 0x0014BA22
		public static bool IsSharingFolderBindingMessage(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Sharing.Binding.In");
		}

		// Token: 0x0600502D RID: 20525 RVA: 0x0014D82F File Offset: 0x0014BA2F
		public static bool IsOutlookRecall(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Outlook.Recall");
		}

		// Token: 0x0600502E RID: 20526 RVA: 0x0014D83C File Offset: 0x0014BA3C
		public static bool IsMeetingCancellation(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Schedule.Meeting.Canceled");
		}

		// Token: 0x0600502F RID: 20527 RVA: 0x0014D849 File Offset: 0x0014BA49
		public static bool IsMeetingCancellationSeries(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.MeetingMessageSeries.Canceled");
		}

		// Token: 0x06005030 RID: 20528 RVA: 0x0014D856 File Offset: 0x0014BA56
		public static bool IsMeetingForwardNotification(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Schedule.Meeting.Notification.Forward") || ObjectClass.IsOfClass(itemClass, "IPM.Notification.Meeting.Forward");
		}

		// Token: 0x06005031 RID: 20529 RVA: 0x0014D872 File Offset: 0x0014BA72
		public static bool IsMeetingForwardNotificationSeries(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.MeetingMessageSeries.Notification.Forward");
		}

		// Token: 0x06005032 RID: 20530 RVA: 0x0014D87F File Offset: 0x0014BA7F
		public static bool IsContact(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Contact");
		}

		// Token: 0x06005033 RID: 20531 RVA: 0x0014D88C File Offset: 0x0014BA8C
		public static bool IsUserPhoto(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.UserPhoto");
		}

		// Token: 0x06005034 RID: 20532 RVA: 0x0014D899 File Offset: 0x0014BA99
		public static bool IsUserPhotoPreview(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.UserPhoto.Preview");
		}

		// Token: 0x06005035 RID: 20533 RVA: 0x0014D8A6 File Offset: 0x0014BAA6
		public static bool IsPlace(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Contact.Place");
		}

		// Token: 0x06005036 RID: 20534 RVA: 0x0014D8B3 File Offset: 0x0014BAB3
		public static bool IsDistributionList(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.DistList");
		}

		// Token: 0x06005037 RID: 20535 RVA: 0x0014D8C0 File Offset: 0x0014BAC0
		public static bool IsSmimeClearSigned(string itemClass)
		{
			return ObjectClass.HasSuffix(itemClass, "SMIME.MultipartSigned");
		}

		// Token: 0x06005038 RID: 20536 RVA: 0x0014D8D0 File Offset: 0x0014BAD0
		public static bool IsSmime(string itemClass)
		{
			return ObjectClass.HasSuffix(itemClass, "SMIME") || ObjectClass.HasSuffix(itemClass, "SMIME.MultipartSigned") || ObjectClass.HasSuffix(itemClass, "SMIME.Encrypted") || ObjectClass.HasSuffix(itemClass, "SMIME.SignedEncrypted") || ObjectClass.HasSuffix(itemClass, "SMIME.Signed");
		}

		// Token: 0x06005039 RID: 20537 RVA: 0x0014D91E File Offset: 0x0014BB1E
		public static bool IsNotesItem(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.StickyNote");
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x0014D92B File Offset: 0x0014BB2B
		public static bool IsTask(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Task");
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x0014D938 File Offset: 0x0014BB38
		public static bool IsTaskRequest(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.TaskRequest");
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x0014D945 File Offset: 0x0014BB45
		public static bool IsGenericMessage(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM");
		}

		// Token: 0x0600503D RID: 20541 RVA: 0x0014D952 File Offset: 0x0014BB52
		public static bool IsJournalItem(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Activity");
		}

		// Token: 0x0600503E RID: 20542 RVA: 0x0014D960 File Offset: 0x0014BB60
		public static bool IsUMMessage(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Note.Microsoft.Missed.Voice") || ObjectClass.IsOfClass(itemClass, "IPM.Note.Microsoft.Voicemail.UM") || ObjectClass.IsOfClass(itemClass, "IPM.Note.Microsoft.Fax") || ObjectClass.IsOfClass(itemClass, "IPM.Note.Microsoft.Partner.UM") || ObjectClass.HasSuffix(itemClass, "Microsoft.Voicemail") || itemClass.IndexOf(".Microsoft.Voicemail.", StringComparison.OrdinalIgnoreCase) != -1;
		}

		// Token: 0x0600503F RID: 20543 RVA: 0x0014D9C4 File Offset: 0x0014BBC4
		public static bool IsVoiceMessage(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Note.Microsoft.Voicemail.UM.CA") || ObjectClass.IsOfClass(itemClass, "IPM.Note.Microsoft.Voicemail.UM") || ObjectClass.IsOfClass(itemClass, "IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA") || ObjectClass.IsOfClass(itemClass, "IPM.Note.rpmsg.Microsoft.Voicemail.UM") || ObjectClass.IsOfClass(itemClass, "IPM.Note.Microsoft.Exchange.Voice.UM.CA") || ObjectClass.IsOfClass(itemClass, "IPM.Note.Microsoft.Exchange.Voice.UM");
		}

		// Token: 0x06005040 RID: 20544 RVA: 0x0014DA1F File Offset: 0x0014BC1F
		public static bool IsRightsManagedContentClass(string contentClass)
		{
			return ObjectClass.IsOfClass(contentClass, "rpmsg.message") || ObjectClass.IsOfClass(contentClass, "IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA") || ObjectClass.IsOfClass(contentClass, "IPM.Note.rpmsg.Microsoft.Voicemail.UM");
		}

		// Token: 0x06005041 RID: 20545 RVA: 0x0014DA48 File Offset: 0x0014BC48
		public static bool IsUMPartnerMessage(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Note.Microsoft.Partner.UM");
		}

		// Token: 0x06005042 RID: 20546 RVA: 0x0014DA55 File Offset: 0x0014BC55
		public static bool IsUMTranscriptionRequest(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Note.Microsoft.Partner.UM.TranscriptionRequest");
		}

		// Token: 0x06005043 RID: 20547 RVA: 0x0014DA62 File Offset: 0x0014BC62
		public static bool IsUMCDRMessage(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Note.Microsoft.CDR.UM");
		}

		// Token: 0x06005044 RID: 20548 RVA: 0x0014DA6F File Offset: 0x0014BC6F
		public static bool IsMissedCall(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Note.Microsoft.Missed.Voice");
		}

		// Token: 0x06005045 RID: 20549 RVA: 0x0014DA7C File Offset: 0x0014BC7C
		public static bool IsVoicemailSearchFolder(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPF.Note.Microsoft.Voicemail");
		}

		// Token: 0x06005046 RID: 20550 RVA: 0x0014DA89 File Offset: 0x0014BC89
		public static bool IsShortcutMessageEntry(int favLevelMask)
		{
			return 1 == favLevelMask;
		}

		// Token: 0x06005047 RID: 20551 RVA: 0x0014DA8F File Offset: 0x0014BC8F
		public static bool IsSmsMessage(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Note.Mobile.SMS");
		}

		// Token: 0x06005048 RID: 20552 RVA: 0x0014DA9C File Offset: 0x0014BC9C
		public static bool IsNonSendableWithRecipients(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Appointment") || ObjectClass.IsOfClass(itemClass, "IPM.Task") || ObjectClass.IsOfClass(itemClass, "IPM.OLE.CLASS.{00061055-0000-0000-C000-000000000046}");
		}

		// Token: 0x06005049 RID: 20553 RVA: 0x0014DAC5 File Offset: 0x0014BCC5
		public static bool IsToDoMessage(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Note.QuickCapture");
		}

		// Token: 0x0600504A RID: 20554 RVA: 0x0014DAD2 File Offset: 0x0014BCD2
		public static bool IsEventReminderMessage(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Note.Reminder.Event");
		}

		// Token: 0x0600504B RID: 20555 RVA: 0x0014DADF File Offset: 0x0014BCDF
		public static bool IsReminderMessage(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Note.Reminder");
		}

		// Token: 0x0600504C RID: 20556 RVA: 0x0014DAEC File Offset: 0x0014BCEC
		public static bool IsConfigurationItem(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Configuration");
		}

		// Token: 0x0600504D RID: 20557 RVA: 0x0014DAF9 File Offset: 0x0014BCF9
		public static bool IsGenericFolder(string containerClass)
		{
			return ObjectClass.IsOfClass(containerClass, "IPF");
		}

		// Token: 0x0600504E RID: 20558 RVA: 0x0014DB06 File Offset: 0x0014BD06
		public static bool IsMessageFolder(string containerClass)
		{
			return ObjectClass.IsOfClass(containerClass, "IPF.Note");
		}

		// Token: 0x0600504F RID: 20559 RVA: 0x0014DB13 File Offset: 0x0014BD13
		public static bool IsTaskFolder(string containerClass)
		{
			return ObjectClass.IsOfClass(containerClass, "IPF.Task");
		}

		// Token: 0x06005050 RID: 20560 RVA: 0x0014DB20 File Offset: 0x0014BD20
		public static bool IsNotesFolder(string containerClass)
		{
			return ObjectClass.IsOfClass(containerClass, "IPF.StickyNote");
		}

		// Token: 0x06005051 RID: 20561 RVA: 0x0014DB2D File Offset: 0x0014BD2D
		public static bool IsJournalFolder(string containerClass)
		{
			return ObjectClass.IsOfClass(containerClass, "IPF.Journal");
		}

		// Token: 0x06005052 RID: 20562 RVA: 0x0014DB3A File Offset: 0x0014BD3A
		public static bool IsShortcutFolder(string containerClass)
		{
			return ObjectClass.IsOfClass(containerClass, "IPF.ShortcutFolder");
		}

		// Token: 0x06005053 RID: 20563 RVA: 0x0014DB47 File Offset: 0x0014BD47
		public static bool IsCalendarFolder(string containerClass)
		{
			return ObjectClass.IsOfClass(containerClass, "IPF.Appointment");
		}

		// Token: 0x06005054 RID: 20564 RVA: 0x0014DB54 File Offset: 0x0014BD54
		public static bool IsInfoPathFormFolder(string containerClass)
		{
			return ObjectClass.IsOfClass(containerClass, "IPF.Note.InfoPathForm");
		}

		// Token: 0x06005055 RID: 20565 RVA: 0x0014DB61 File Offset: 0x0014BD61
		public static bool IsBirthdayCalendarFolder(string containerClass)
		{
			return ObjectClass.IsOfClass(containerClass, "IPF.Appointment.Birthday");
		}

		// Token: 0x06005056 RID: 20566 RVA: 0x0014DB6E File Offset: 0x0014BD6E
		public static bool IsContactsFolder(string containerClass)
		{
			return ObjectClass.IsOfClass(containerClass, "IPF.Contact");
		}

		// Token: 0x06005057 RID: 20567 RVA: 0x0014DB7B File Offset: 0x0014BD7B
		public static bool IsMailboxDiscoverySearchRequest(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Configuration.MailboxDiscoverySearchRequest");
		}

		// Token: 0x06005058 RID: 20568 RVA: 0x0014DB88 File Offset: 0x0014BD88
		public static bool IsSubscriptionDataItem(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "Exchange.PushNotification.Subscription");
		}

		// Token: 0x06005059 RID: 20569 RVA: 0x0014DB95 File Offset: 0x0014BD95
		public static bool IsOutlookServiceSubscriptionDataItem(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "OutlookService.Notification.Subscription");
		}

		// Token: 0x0600505A RID: 20570 RVA: 0x0014DBA2 File Offset: 0x0014BDA2
		public static bool IsParkedMeetingMessage(string itemClass)
		{
			return ObjectClass.IsOfClass(itemClass, "IPM.Parked.MeetingMessage");
		}

		// Token: 0x0600505B RID: 20571 RVA: 0x0014DBB0 File Offset: 0x0014BDB0
		public static StoreObjectType GetObjectType(string itemClass)
		{
			if (ObjectClass.IsOfClass(itemClass, "IPM.Note.Rules.OofTemplate.Microsoft", false))
			{
				return StoreObjectType.OofMessage;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.Note.Rules.ExternalOofTemplate.Microsoft", false))
			{
				return StoreObjectType.ExternalOofMessage;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.Note.Reminder"))
			{
				return StoreObjectType.ReminderMessage;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.Note"))
			{
				return StoreObjectType.Message;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.Post"))
			{
				return StoreObjectType.Post;
			}
			if (ObjectClass.IsReport(itemClass))
			{
				return StoreObjectType.Report;
			}
			if (ObjectClass.IsCalendarItem(itemClass))
			{
				return StoreObjectType.CalendarItem;
			}
			if (ObjectClass.IsCalendarItemSeries(itemClass))
			{
				return StoreObjectType.CalendarItemSeries;
			}
			if (ObjectClass.IsMeetingRequestSeries(itemClass))
			{
				return StoreObjectType.MeetingRequestSeries;
			}
			if (ObjectClass.IsMeetingRequest(itemClass))
			{
				return StoreObjectType.MeetingRequest;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.MeetingMessageSeries.Resp"))
			{
				return StoreObjectType.MeetingResponseSeries;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.Schedule.Meeting.Resp"))
			{
				return StoreObjectType.MeetingResponse;
			}
			if (ObjectClass.IsMeetingCancellationSeries(itemClass))
			{
				return StoreObjectType.MeetingCancellationSeries;
			}
			if (ObjectClass.IsMeetingCancellation(itemClass))
			{
				return StoreObjectType.MeetingCancellation;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.Schedule.Meeting.Notification.Forward"))
			{
				return StoreObjectType.MeetingForwardNotification;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.MeetingMessageSeries.Notification.Forward"))
			{
				return StoreObjectType.MeetingForwardNotificationSeries;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.Notification.Meeting"))
			{
				return StoreObjectType.MeetingForwardNotification;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.Schedule.Inquiry"))
			{
				return StoreObjectType.MeetingInquiryMessage;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.Contact.Place"))
			{
				return StoreObjectType.Place;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.Contact"))
			{
				return StoreObjectType.Contact;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.UserPhoto.Preview"))
			{
				return StoreObjectType.UserPhotoPreview;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.UserPhoto"))
			{
				return StoreObjectType.UserPhoto;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.DistList"))
			{
				return StoreObjectType.DistributionList;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.Task"))
			{
				return StoreObjectType.Task;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.TaskRequest"))
			{
				return StoreObjectType.TaskRequest;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.ConversationAction"))
			{
				return StoreObjectType.ConversationActionItem;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA"))
			{
				return StoreObjectType.Message;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.Note.rpmsg.Microsoft.Voicemail.UM"))
			{
				return StoreObjectType.Message;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.Sharing", false))
			{
				return StoreObjectType.SharingMessage;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.GroupMailbox.JoinRequest"))
			{
				return StoreObjectType.GroupMailboxRequestMessage;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.MailboxAssociation.Group"))
			{
				return StoreObjectType.MailboxAssociationGroup;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.MailboxAssociation.User"))
			{
				return StoreObjectType.MailboxAssociationUser;
			}
			if (ObjectClass.IsOfClass(itemClass, "IPM.HierarchySync.Metadata"))
			{
				return StoreObjectType.HierarchySyncMetadata;
			}
			if (ObjectClass.IsSubscriptionDataItem(itemClass))
			{
				return StoreObjectType.PushNotificationSubscription;
			}
			if (ObjectClass.IsOutlookServiceSubscriptionDataItem(itemClass))
			{
				return StoreObjectType.OutlookServiceSubscription;
			}
			if (ObjectClass.IsConfigurationItem(itemClass))
			{
				return StoreObjectType.Configuration;
			}
			if (ObjectClass.IsParkedMeetingMessage(itemClass))
			{
				return StoreObjectType.ParkedMeetingMessage;
			}
			if (ObjectClass.IsGenericMessage(itemClass))
			{
				return StoreObjectType.Message;
			}
			if (ObjectClass.IsCalendarFolder(itemClass))
			{
				return StoreObjectType.CalendarFolder;
			}
			if (ObjectClass.IsContactsFolder(itemClass))
			{
				return StoreObjectType.ContactsFolder;
			}
			if (ObjectClass.IsTaskFolder(itemClass))
			{
				return StoreObjectType.TasksFolder;
			}
			if (ObjectClass.IsNotesFolder(itemClass))
			{
				return StoreObjectType.NotesFolder;
			}
			if (ObjectClass.IsJournalFolder(itemClass))
			{
				return StoreObjectType.JournalFolder;
			}
			if (ObjectClass.IsShortcutFolder(itemClass))
			{
				return StoreObjectType.ShortcutFolder;
			}
			if (ObjectClass.IsGenericFolder(itemClass))
			{
				return StoreObjectType.Folder;
			}
			return StoreObjectType.Unknown;
		}

		// Token: 0x0600505C RID: 20572 RVA: 0x0014DE2C File Offset: 0x0014C02C
		public static Schema GetSchema(StoreObject storeObject)
		{
			return ObjectClass.GetSchema(storeObject.ClassName);
		}

		// Token: 0x0600505D RID: 20573 RVA: 0x0014DE3C File Offset: 0x0014C03C
		public static Schema GetSchema(string className)
		{
			if (ObjectClass.IsOfClass(className, "IPM.OLE.CLASS.{00061055-0000-0000-C000-000000000046}") || ObjectClass.IsOfClass(className, "IPM.Appointment.Occurrence"))
			{
				return CalendarItemOccurrenceSchema.Instance;
			}
			StoreObjectType objectType = ObjectClass.GetObjectType(className);
			return ObjectClass.GetSchema(objectType);
		}

		// Token: 0x0600505E RID: 20574 RVA: 0x0014DE76 File Offset: 0x0014C076
		public static bool HasSuffix(string itemClass, string suffix)
		{
			return itemClass != null && itemClass.EndsWith(suffix, StringComparison.OrdinalIgnoreCase) && itemClass.Length > suffix.Length && itemClass[itemClass.Length - suffix.Length - 1] == '.';
		}

		// Token: 0x0600505F RID: 20575 RVA: 0x0014DEAE File Offset: 0x0014C0AE
		internal static bool IsDerivedClass(string itemClass, string baseClass)
		{
			return itemClass != null && itemClass.StartsWith(baseClass, StringComparison.OrdinalIgnoreCase) && itemClass.Length > baseClass.Length && itemClass[baseClass.Length] == '.';
		}

		// Token: 0x06005060 RID: 20576 RVA: 0x0014DEE0 File Offset: 0x0014C0E0
		internal static bool IsOfClass(string itemClass, string baseClass, bool orDerivedClass)
		{
			return itemClass != null && itemClass.StartsWith(baseClass, StringComparison.OrdinalIgnoreCase) && (itemClass.Length == baseClass.Length || (orDerivedClass && itemClass.Length > baseClass.Length && itemClass[baseClass.Length] == '.'));
		}

		// Token: 0x06005061 RID: 20577 RVA: 0x0014DF2F File Offset: 0x0014C12F
		public static bool IsOfClass(string itemClass, string baseClass)
		{
			return ObjectClass.IsOfClass(itemClass, baseClass, true);
		}

		// Token: 0x06005062 RID: 20578 RVA: 0x0014DF3C File Offset: 0x0014C13C
		public static string MakeReportClassName(string itemClass, string reportSuffixOrClass)
		{
			if (ObjectClass.ReportClasses.IsReportOfSpecialCasedClass(reportSuffixOrClass))
			{
				return reportSuffixOrClass;
			}
			return string.Join(".", new string[]
			{
				"REPORT",
				itemClass,
				reportSuffixOrClass
			});
		}

		// Token: 0x06005063 RID: 20579 RVA: 0x0014DF78 File Offset: 0x0014C178
		public static string GetContainerMessageClass(StoreObjectType type)
		{
			EnumValidator.ThrowIfInvalid<StoreObjectType>(type, "type");
			string result;
			ObjectClass.tableContainerMessageClass.TryGetValue(type, out result);
			return result;
		}

		// Token: 0x06005064 RID: 20580 RVA: 0x0014DFA0 File Offset: 0x0014C1A0
		private static Schema GetSchema(StoreObjectType objectType)
		{
			switch (objectType)
			{
			case StoreObjectType.Folder:
			case StoreObjectType.CalendarFolder:
			case StoreObjectType.ContactsFolder:
			case StoreObjectType.TasksFolder:
			case StoreObjectType.NotesFolder:
			case StoreObjectType.JournalFolder:
			case StoreObjectType.SearchFolder:
			case StoreObjectType.OutlookSearchFolder:
			case StoreObjectType.ShortcutFolder:
				return FolderSchema.Instance;
			case StoreObjectType.Message:
			case StoreObjectType.ConflictMessage:
			case StoreObjectType.TaskRequest:
			case StoreObjectType.Note:
			case StoreObjectType.OofMessage:
			case StoreObjectType.ExternalOofMessage:
				return MessageItemSchema.Instance;
			case StoreObjectType.MeetingMessage:
			case StoreObjectType.MeetingCancellation:
				return MeetingMessageInstanceSchema.Instance;
			case StoreObjectType.MeetingRequest:
				return MeetingRequestSchema.Instance;
			case StoreObjectType.MeetingResponse:
				return MeetingResponseSchema.Instance;
			case StoreObjectType.CalendarItem:
				return CalendarItemSchema.Instance;
			case StoreObjectType.CalendarItemOccurrence:
				return CalendarItemOccurrenceSchema.Instance;
			case StoreObjectType.Contact:
				return ContactSchema.Instance;
			case StoreObjectType.DistributionList:
				return DistributionListSchema.Instance;
			case StoreObjectType.Task:
				return TaskSchema.Instance;
			case StoreObjectType.Post:
				return PostItemSchema.Instance;
			case StoreObjectType.Report:
				return ReportMessageSchema.Instance;
			case StoreObjectType.MeetingForwardNotification:
				return MeetingForwardNotificationSchema.Instance;
			case StoreObjectType.ConversationActionItem:
				return ConversationActionItemSchema.Instance;
			case StoreObjectType.SharingMessage:
				return SharingMessageItemSchema.Instance;
			case StoreObjectType.MeetingInquiryMessage:
				return MeetingInquiryMessageSchema.Instance;
			case StoreObjectType.MailboxAssociationGroup:
				return MailboxAssociationGroupSchema.Instance;
			case StoreObjectType.MailboxAssociationUser:
				return MailboxAssociationUserSchema.Instance;
			case StoreObjectType.GroupMailboxRequestMessage:
				return GroupMailboxJoinRequestMessageSchema.Instance;
			case StoreObjectType.ReminderMessage:
				return ReminderMessageSchema.Instance;
			case StoreObjectType.Configuration:
				return ConfigurationItemSchema.Instance;
			case StoreObjectType.HierarchySyncMetadata:
				return HierarchySyncMetadataItemSchema.Instance;
			}
			return ItemSchema.Instance;
		}

		// Token: 0x06005065 RID: 20581 RVA: 0x0014E134 File Offset: 0x0014C334
		private static Dictionary<StoreObjectType, string> BuildContainerMessageClassTable(Dictionary<StoreObjectType, string> tableClass)
		{
			return new Dictionary<StoreObjectType, string>
			{
				{
					StoreObjectType.Folder,
					"IPF.Note"
				},
				{
					StoreObjectType.CalendarFolder,
					"IPF.Appointment"
				},
				{
					StoreObjectType.ContactsFolder,
					"IPF.Contact"
				},
				{
					StoreObjectType.TasksFolder,
					"IPF.Task"
				},
				{
					StoreObjectType.NotesFolder,
					"IPF.StickyNote"
				},
				{
					StoreObjectType.ShortcutFolder,
					"IPF.ShortcutFolder"
				},
				{
					StoreObjectType.SearchFolder,
					"IPF.Note"
				},
				{
					StoreObjectType.OutlookSearchFolder,
					"IPF.Note"
				},
				{
					StoreObjectType.Message,
					"IPM.Note"
				},
				{
					StoreObjectType.MeetingMessage,
					"IPM.Schedule.Meeting"
				},
				{
					StoreObjectType.MeetingRequest,
					"IPM.Schedule.Meeting.Request"
				},
				{
					StoreObjectType.MeetingResponse,
					"IPM.Schedule.Meeting.Resp"
				},
				{
					StoreObjectType.MeetingCancellation,
					"IPM.Schedule.Meeting.Canceled"
				},
				{
					StoreObjectType.MeetingInquiryMessage,
					"IPM.Schedule.Inquiry"
				},
				{
					StoreObjectType.ConflictMessage,
					"IPM.Conflict.Message"
				},
				{
					StoreObjectType.CalendarItem,
					"IPM.Appointment"
				},
				{
					StoreObjectType.CalendarItemOccurrence,
					"IPM.Appointment.Occurrence"
				},
				{
					StoreObjectType.Contact,
					"IPM.Contact"
				},
				{
					StoreObjectType.Place,
					"IPM.Contact.Place"
				},
				{
					StoreObjectType.DistributionList,
					"IPM.DistList"
				},
				{
					StoreObjectType.Task,
					"IPM.Task"
				},
				{
					StoreObjectType.TaskRequest,
					"IPM.TaskRequest"
				},
				{
					StoreObjectType.ReminderMessage,
					"IPM.Note.Reminder"
				},
				{
					StoreObjectType.Note,
					"IPM.StickyNote"
				},
				{
					StoreObjectType.Post,
					"IPM.Post"
				},
				{
					StoreObjectType.Report,
					"REPORT"
				},
				{
					StoreObjectType.SharingMessage,
					"IPM.Sharing"
				},
				{
					StoreObjectType.GroupMailboxRequestMessage,
					"IPM.GroupMailbox.JoinRequest"
				},
				{
					StoreObjectType.OofMessage,
					"IPM.Note.Rules.OofTemplate.Microsoft"
				},
				{
					StoreObjectType.ExternalOofMessage,
					"IPM.Note.Rules.ExternalOofTemplate.Microsoft"
				},
				{
					StoreObjectType.UserPhoto,
					"IPM.UserPhoto"
				},
				{
					StoreObjectType.UserPhotoPreview,
					"IPM.UserPhoto.Preview"
				},
				{
					StoreObjectType.MailboxAssociationGroup,
					"IPM.MailboxAssociation.Group"
				},
				{
					StoreObjectType.MailboxAssociationUser,
					"IPM.MailboxAssociation.User"
				},
				{
					StoreObjectType.HierarchySyncMetadata,
					"IPM.HierarchySync.Metadata"
				}
			};
		}

		// Token: 0x04002B68 RID: 11112
		public const int PublicFolderFavLevelMask = 1;

		// Token: 0x04002B69 RID: 11113
		public const string GenericItem = "IPM";

		// Token: 0x04002B6A RID: 11114
		public const string Message = "IPM.Note";

		// Token: 0x04002B6B RID: 11115
		public const string MapiSubmitLAMProbe = "IPM.Note.MapiSubmitLAMProbe";

		// Token: 0x04002B6C RID: 11116
		public const string MapiSubmitSystemProbe = "IPM.Note.MapiSubmitSystemProbe";

		// Token: 0x04002B6D RID: 11117
		public const string Post = "IPM.Post";

		// Token: 0x04002B6E RID: 11118
		public const string Report = "REPORT";

		// Token: 0x04002B6F RID: 11119
		public const string Appointment = "IPM.Appointment";

		// Token: 0x04002B70 RID: 11120
		public const string CalendarItemSeries = "IPM.AppointmentSeries";

		// Token: 0x04002B71 RID: 11121
		public const string Contact = "IPM.Contact";

		// Token: 0x04002B72 RID: 11122
		public const string UserPhoto = "IPM.UserPhoto";

		// Token: 0x04002B73 RID: 11123
		public const string UserPhotoPreview = "IPM.UserPhoto.Preview";

		// Token: 0x04002B74 RID: 11124
		public const string UserPhotoDeletedNotification = "IPM.UserPhoto.DeletedNotification";

		// Token: 0x04002B75 RID: 11125
		public const string Place = "IPM.Contact.Place";

		// Token: 0x04002B76 RID: 11126
		public const string DistributionList = "IPM.DistList";

		// Token: 0x04002B77 RID: 11127
		public const string ScheduleMeeting = "IPM.Schedule.Meeting";

		// Token: 0x04002B78 RID: 11128
		public const string MeetingNotification = "IPM.Schedule.Meeting.Notification";

		// Token: 0x04002B79 RID: 11129
		public const string E12RTMMeetingNotification = "IPM.Notification.Meeting";

		// Token: 0x04002B7A RID: 11130
		public const string MeetingMessageSeries = "IPM.MeetingMessageSeries";

		// Token: 0x04002B7B RID: 11131
		public const string MeetingRequest = "IPM.Schedule.Meeting.Request";

		// Token: 0x04002B7C RID: 11132
		public const string MeetingRequestSeries = "IPM.MeetingMessageSeries.Request";

		// Token: 0x04002B7D RID: 11133
		public const string MeetingInquiry = "IPM.Schedule.Inquiry";

		// Token: 0x04002B7E RID: 11134
		public const string MeetingCancellation = "IPM.Schedule.Meeting.Canceled";

		// Token: 0x04002B7F RID: 11135
		public const string MeetingCancellationSeries = "IPM.MeetingMessageSeries.Canceled";

		// Token: 0x04002B80 RID: 11136
		public const string MeetingResponsePrefix = "IPM.Schedule.Meeting.Resp";

		// Token: 0x04002B81 RID: 11137
		public const string MeetingResponseSeriesPrefix = "IPM.MeetingMessageSeries.Resp";

		// Token: 0x04002B82 RID: 11138
		public const string MiddleTierRules = "IPM.Rule.Version2.Message";

		// Token: 0x04002B83 RID: 11139
		public const string PositiveMeetingResponseSuffix = "Pos";

		// Token: 0x04002B84 RID: 11140
		public const string NegativeMeetingResponseSuffix = "Neg";

		// Token: 0x04002B85 RID: 11141
		public const string TentativeMeetingResponseSuffix = "Tent";

		// Token: 0x04002B86 RID: 11142
		public const string PositiveMeetingResponse = "IPM.Schedule.Meeting.Resp.Pos";

		// Token: 0x04002B87 RID: 11143
		public const string PositiveMeetingResponseSeries = "IPM.MeetingMessageSeries.Resp.Pos";

		// Token: 0x04002B88 RID: 11144
		public const string NegativeMeetingResponse = "IPM.Schedule.Meeting.Resp.Neg";

		// Token: 0x04002B89 RID: 11145
		public const string NegativeMeetingResponseSeries = "IPM.MeetingMessageSeries.Resp.Neg";

		// Token: 0x04002B8A RID: 11146
		public const string TentativeMeetingResponse = "IPM.Schedule.Meeting.Resp.Tent";

		// Token: 0x04002B8B RID: 11147
		public const string TentativeMeetingResponseSeries = "IPM.MeetingMessageSeries.Resp.Tent";

		// Token: 0x04002B8C RID: 11148
		public const string MeetingForwardNotification = "IPM.Schedule.Meeting.Notification.Forward";

		// Token: 0x04002B8D RID: 11149
		public const string MeetingForwardNotificationSeries = "IPM.MeetingMessageSeries.Notification.Forward";

		// Token: 0x04002B8E RID: 11150
		public const string E12RTMMeetingForwardNotification = "IPM.Notification.Meeting.Forward";

		// Token: 0x04002B8F RID: 11151
		public const string NotSupportedICal = "IPM.Note.NotSupportedICal";

		// Token: 0x04002B90 RID: 11152
		public const string ContentsSyncData = "Exchange.ContentsSyncData";

		// Token: 0x04002B91 RID: 11153
		public const string SecureEncrypted = "IPM.Note.Secure";

		// Token: 0x04002B92 RID: 11154
		public const string SmimeEncrypted = "IPM.Note.SMIME";

		// Token: 0x04002B93 RID: 11155
		internal const string SmimeSuffix = "SMIME";

		// Token: 0x04002B94 RID: 11156
		internal const string SmimeMultipartSignedSuffix = "SMIME.MultipartSigned";

		// Token: 0x04002B95 RID: 11157
		internal const string SmimeSignedSuffix = "SMIME.Signed";

		// Token: 0x04002B96 RID: 11158
		internal const string SmimeSignedEncryptedSuffix = "SMIME.SignedEncrypted";

		// Token: 0x04002B97 RID: 11159
		internal const string SmimeEncryptedSuffix = "SMIME.Encrypted";

		// Token: 0x04002B98 RID: 11160
		public const string SecureSign = "IPM.Note.Secure.Sign";

		// Token: 0x04002B99 RID: 11161
		public const string SmimeSigned = "IPM.Note.SMIME.MultipartSigned";

		// Token: 0x04002B9A RID: 11162
		public const string StickyNote = "IPM.StickyNote";

		// Token: 0x04002B9B RID: 11163
		public const string Task = "IPM.Task";

		// Token: 0x04002B9C RID: 11164
		internal const string CustomTask = "IPM.Task.";

		// Token: 0x04002B9D RID: 11165
		public const string TaskRequest = "IPM.TaskRequest";

		// Token: 0x04002B9E RID: 11166
		internal const string ConflictMessage = "IPM.Conflict.Message";

		// Token: 0x04002B9F RID: 11167
		internal const string ConflictMessagePrefix = "IPM.Conflict";

		// Token: 0x04002BA0 RID: 11168
		internal const string ConflictResolution = "IPM.Conflict.Resolution.Message";

		// Token: 0x04002BA1 RID: 11169
		public const string ElcJournalMsg = "IPM.Note.JournalReport.Msg";

		// Token: 0x04002BA2 RID: 11170
		public const string ElcJournalTnef = "IPM.Note.JournalReport.Tnef";

		// Token: 0x04002BA3 RID: 11171
		internal const string RpmsgMessageContentClass = "rpmsg.message";

		// Token: 0x04002BA4 RID: 11172
		internal const string RecurrenceException = "IPM.OLE.CLASS.{00061055-0000-0000-C000-000000000046}";

		// Token: 0x04002BA5 RID: 11173
		internal const string AppointmentOccurrence = "IPM.Appointment.Occurrence";

		// Token: 0x04002BA6 RID: 11174
		internal const string OutlookRecall = "IPM.Outlook.Recall";

		// Token: 0x04002BA7 RID: 11175
		public const string OutlookJournalItem = "IPM.Activity";

		// Token: 0x04002BA8 RID: 11176
		public const string UserActivityItem = "IPM.Activity";

		// Token: 0x04002BA9 RID: 11177
		public const string ExchangeUMBetaPureAudioClass = "IPM.Note.Microsoft.Exchange.Voice.UM";

		// Token: 0x04002BAA RID: 11178
		public const string ExchangeUMPureAudioClass = "IPM.Note.Microsoft.Voicemail.UM";

		// Token: 0x04002BAB RID: 11179
		public const string ExchangeUMBetaMessageClass = "IPM.Note.Microsoft.Exchange.Voice.UM.CA";

		// Token: 0x04002BAC RID: 11180
		public const string ExchangeUMMessageClass = "IPM.Note.Microsoft.Voicemail.UM.CA";

		// Token: 0x04002BAD RID: 11181
		public const string ExchangeUMPartnerClass = "IPM.Note.Microsoft.Partner.UM";

		// Token: 0x04002BAE RID: 11182
		public const string ExchangeUMTranscriptionRequestClass = "IPM.Note.Microsoft.Partner.UM.TranscriptionRequest";

		// Token: 0x04002BAF RID: 11183
		public const string ExchangeUMProtectedMessageClass = "IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA";

		// Token: 0x04002BB0 RID: 11184
		public const string ExchangeUMProtectedPureAudioClass = "IPM.Note.rpmsg.Microsoft.Voicemail.UM";

		// Token: 0x04002BB1 RID: 11185
		public const string ExchangeUMCDRMessageClass = "IPM.Note.Microsoft.CDR.UM";

		// Token: 0x04002BB2 RID: 11186
		public const string ApprovalNotificationMessageClass = "IPM.Note.Microsoft.Approval.Reply";

		// Token: 0x04002BB3 RID: 11187
		public const string ApprovalApproveNotificationMessageClass = "IPM.Note.Microsoft.Approval.Reply.Approve";

		// Token: 0x04002BB4 RID: 11188
		public const string ApprovalRejectNotificationMessageClass = "IPM.Note.Microsoft.Approval.Reply.Reject";

		// Token: 0x04002BB5 RID: 11189
		public const string ApprovalRequestMessageClass = "IPM.Note.Microsoft.Approval.Request";

		// Token: 0x04002BB6 RID: 11190
		public const string ApprovalMessageClassPrefix = "IPM.Note.Microsoft.Approval";

		// Token: 0x04002BB7 RID: 11191
		public const string ApprovalInitiationMessageClass = "IPM.Microsoft.Approval.Initiation";

		// Token: 0x04002BB8 RID: 11192
		internal const string OutlookWunderBarLinkMessageClass = "IPM.Microsoft.WunderBar.Link";

		// Token: 0x04002BB9 RID: 11193
		internal const string FaxMessageClass = "IPM.Note.Microsoft.Fax";

		// Token: 0x04002BBA RID: 11194
		internal const string FaxCaMessageClass = "IPM.Note.Microsoft.Fax.CA";

		// Token: 0x04002BBB RID: 11195
		internal const string MissedCallMessageClass = "IPM.Note.Microsoft.Missed.Voice";

		// Token: 0x04002BBC RID: 11196
		public const string ExchangeUMVoiceUcClass = "IPM.Note.Microsoft.Conversation.Voice";

		// Token: 0x04002BBD RID: 11197
		internal const string ExchangeUMMessageSuffix = "Microsoft.Voicemail";

		// Token: 0x04002BBE RID: 11198
		internal const string ExchangeVoicemailMessageTag = ".Microsoft.Voicemail.";

		// Token: 0x04002BBF RID: 11199
		internal const string CustomMessageClass = "IPM.Note.Custom";

		// Token: 0x04002BC0 RID: 11200
		public const string InfopathMessageClass = "IPM.InfoPathForm";

		// Token: 0x04002BC1 RID: 11201
		internal const string ReplicationMessageClass = "IPM.Replication";

		// Token: 0x04002BC2 RID: 11202
		internal const string ConversationActionItemMessageClass = "IPM.ConversationAction";

		// Token: 0x04002BC3 RID: 11203
		internal const string SharingMessageClass = "IPM.Sharing";

		// Token: 0x04002BC4 RID: 11204
		internal const string GroupMailboxJoinRequestClass = "IPM.GroupMailbox.JoinRequest";

		// Token: 0x04002BC5 RID: 11205
		internal const string GroupMailboxWelcomeMessageClass = "IPM.Note.GroupMailbox.WelcomeEmail";

		// Token: 0x04002BC6 RID: 11206
		internal const string SharingFolderIndexMessageClass = "IPM.Sharing.Index.In";

		// Token: 0x04002BC7 RID: 11207
		internal const string AggregationPopMessageClass = "IPM.Aggregation.Pop";

		// Token: 0x04002BC8 RID: 11208
		internal const string AggregationDavMessageClass = "IPM.Aggregation.Dav";

		// Token: 0x04002BC9 RID: 11209
		internal const string AggregationDeltaSyncMessageClass = "IPM.Aggregation.DeltaSync";

		// Token: 0x04002BCA RID: 11210
		internal const string AggregationIMAPMessageClass = "IPM.Aggregation.IMAP";

		// Token: 0x04002BCB RID: 11211
		internal const string AggregationFacebookMessageClass = "IPM.Aggregation.Facebook";

		// Token: 0x04002BCC RID: 11212
		internal const string AggregationLinkedInMessageClass = "IPM.Aggregation.LinkedIn";

		// Token: 0x04002BCD RID: 11213
		internal const string SharingFolderBindingMessageClass = "IPM.Sharing.Binding.In";

		// Token: 0x04002BCE RID: 11214
		internal const string RssServerLockMessageClass = "IPM.Microsoft.RssLock";

		// Token: 0x04002BCF RID: 11215
		internal const string AggregationCacheSubscriptionsMessageClass = "IPM.Aggregation.Cache.Subscriptions";

		// Token: 0x04002BD0 RID: 11216
		internal const string RssPostMessageClass = "IPM.Post.RSS";

		// Token: 0x04002BD1 RID: 11217
		internal const string OofMessageClass = "IPM.Note.Rules.OofTemplate.Microsoft";

		// Token: 0x04002BD2 RID: 11218
		internal const string ExternalOofMessageClass = "IPM.Note.Rules.ExternalOofTemplate.Microsoft";

		// Token: 0x04002BD3 RID: 11219
		internal const string FreeBusyDataMessageClass = "IPM.Microsoft.ScheduleData.FreeBusy";

		// Token: 0x04002BD4 RID: 11220
		public const string MmsMessageClass = "IPM.Note.Mobile.MMS";

		// Token: 0x04002BD5 RID: 11221
		public const string SmsMessageClass = "IPM.Note.Mobile.SMS";

		// Token: 0x04002BD6 RID: 11222
		public const string SmsAlertMessageClass = "IPM.Note.Mobile.SMS.Alert";

		// Token: 0x04002BD7 RID: 11223
		public const string SmsAlertCalendarMessageClass = "IPM.Note.Mobile.SMS.Alert.Calendar";

		// Token: 0x04002BD8 RID: 11224
		public const string SmsAlertVoicemailMessageClass = "IPM.Note.Mobile.SMS.Alert.Voicemail";

		// Token: 0x04002BD9 RID: 11225
		public const string SmsUndercurrentMessageClass = "IPM.Note.Mobile.SMS.Undercurrent";

		// Token: 0x04002BDA RID: 11226
		public const string SmsAlertInfoMessageClass = "IPM.Note.Mobile.SMS.Alert.Info";

		// Token: 0x04002BDB RID: 11227
		internal const string Document = "IPM.Document";

		// Token: 0x04002BDC RID: 11228
		internal const string LiveMeetingRequest = "IPM.Appointment.Live Meeting Request";

		// Token: 0x04002BDD RID: 11229
		internal const string MessageRecallReport = "IPM.Recall.Report";

		// Token: 0x04002BDE RID: 11230
		internal const string MultimediaMessage = "IPM.Note.Mobile.MMS";

		// Token: 0x04002BDF RID: 11231
		internal const string Remote = "IPM.Remote";

		// Token: 0x04002BE0 RID: 11232
		internal const string Resend = "IPM.Resend";

		// Token: 0x04002BE1 RID: 11233
		internal const string RuleReplyTemplate = "IPM.Note.Rules.ReplyTemplate.Microsoft";

		// Token: 0x04002BE2 RID: 11234
		internal const string SmimeReceipt = "IPM.Note.RECEIPT.SMIME";

		// Token: 0x04002BE3 RID: 11235
		internal const string TaskAccept = "IPM.TaskRequest.Accept";

		// Token: 0x04002BE4 RID: 11236
		internal const string TaskUpdate = "IPM.TaskRequest.Update";

		// Token: 0x04002BE5 RID: 11237
		internal const string TaskDecline = "IPM.TaskRequest.Decline";

		// Token: 0x04002BE6 RID: 11238
		internal const string UserUserOofSettings = "IPM.Microsoft.OOF.UserOofSettings";

		// Token: 0x04002BE7 RID: 11239
		internal const string ExternalSharingSubscription = "IPM.ExternalSharingSubscription";

		// Token: 0x04002BE8 RID: 11240
		internal const string PublishingSubscription = "IPM.PublishingSubscription";

		// Token: 0x04002BE9 RID: 11241
		internal const string RuleMessage = "IPM.Rule.Message";

		// Token: 0x04002BEA RID: 11242
		internal const string MiddleTierRuleMessage = "IPM.Rule.Version2.Message";

		// Token: 0x04002BEB RID: 11243
		internal const string ExtendedRuleMessage = "IPM.ExtendedRule.Message";

		// Token: 0x04002BEC RID: 11244
		internal const string AuditLog = "IPM.AuditLog";

		// Token: 0x04002BED RID: 11245
		internal const string PeopleConnectNotificationConnected = "IPM.Note.PeopleConnect.Notification.Connected";

		// Token: 0x04002BEE RID: 11246
		internal const string PeopleConnectNotificationDisconnected = "IPM.Note.PeopleConnect.Notification.Disconnected";

		// Token: 0x04002BEF RID: 11247
		internal const string PeopleConnectNotificationNewTokenNeeded = "IPM.Note.PeopleConnect.Notification.NewTokenNeeded";

		// Token: 0x04002BF0 RID: 11248
		internal const string PeopleConnectNotificationInitialSyncCompleted = "IPM.Note.PeopleConnect.Notification.InitialSyncCompleted";

		// Token: 0x04002BF1 RID: 11249
		internal const string MailboxDiscoverySearchConfiguration = "IPM.Configuration.MailboxDiscoverySearch";

		// Token: 0x04002BF2 RID: 11250
		internal const string MailboxDiscoverySearchRequest = "IPM.Configuration.MailboxDiscoverySearchRequest";

		// Token: 0x04002BF3 RID: 11251
		internal const string MailboxAssociationGroup = "IPM.MailboxAssociation.Group";

		// Token: 0x04002BF4 RID: 11252
		internal const string MailboxAssociationUser = "IPM.MailboxAssociation.User";

		// Token: 0x04002BF5 RID: 11253
		internal const string HierarchySyncMetadata = "IPM.HierarchySync.Metadata";

		// Token: 0x04002BF6 RID: 11254
		internal const string PushNotificationSubscriptionDataItem = "Exchange.PushNotification.Subscription";

		// Token: 0x04002BF7 RID: 11255
		internal const string ToDoMessageClass = "IPM.Note.QuickCapture";

		// Token: 0x04002BF8 RID: 11256
		internal const string ReminderMessageClass = "IPM.Note.Reminder";

		// Token: 0x04002BF9 RID: 11257
		internal const string EventReminderMessageClass = "IPM.Note.Reminder.Event";

		// Token: 0x04002BFA RID: 11258
		internal const string ModernReminderMessageClass = "IPM.Note.Reminder.Modern";

		// Token: 0x04002BFB RID: 11259
		internal const string OutlookServiceSubscriptionDataItem = "OutlookService.Notification.Subscription";

		// Token: 0x04002BFC RID: 11260
		internal const string ConfigurationItemClass = "IPM.Configuration";

		// Token: 0x04002BFD RID: 11261
		internal const string ParkedMeetingMessage = "IPM.Parked.MeetingMessage";

		// Token: 0x04002BFE RID: 11262
		public const string GenericFolder = "IPF";

		// Token: 0x04002BFF RID: 11263
		public const string MessageFolder = "IPF.Note";

		// Token: 0x04002C00 RID: 11264
		public const string CalendarFolder = "IPF.Appointment";

		// Token: 0x04002C01 RID: 11265
		public const string ContactFolder = "IPF.Contact";

		// Token: 0x04002C02 RID: 11266
		public const string TaskFolder = "IPF.Task";

		// Token: 0x04002C03 RID: 11267
		public const string JournalFolder = "IPF.Journal";

		// Token: 0x04002C04 RID: 11268
		public const string ShortcutFolder = "IPF.ShortcutFolder";

		// Token: 0x04002C05 RID: 11269
		public const string StickyNoteFolder = "IPF.StickyNote";

		// Token: 0x04002C06 RID: 11270
		public const string RecipientCacheFolder = "IPF.Contact.RecipientCache";

		// Token: 0x04002C07 RID: 11271
		public const string GalContactsFolder = "IPF.Contact.GalContacts";

		// Token: 0x04002C08 RID: 11272
		public const string ConfigurationFolder = "IPF.Configuration";

		// Token: 0x04002C09 RID: 11273
		public const string BirthdayCalendarFolder = "IPF.Appointment.Birthday";

		// Token: 0x04002C0A RID: 11274
		public const string SmsAndChatsSyncFolder = "IPF.SmsAndChatsSync";

		// Token: 0x04002C0B RID: 11275
		internal const string IPFReminder = "Outlook.Reminder";

		// Token: 0x04002C0C RID: 11276
		internal const string IPFNoteInfoPathFormFolder = "IPF.Note.InfoPathForm";

		// Token: 0x04002C0D RID: 11277
		internal const string IPFNoteOutlookHomepage = "IPF.Note.OutlookHomepage";

		// Token: 0x04002C0E RID: 11278
		internal const string ExchangeUMVoiceMailSearchFolderClass = "IPF.Note.Microsoft.Voicemail";

		// Token: 0x04002C0F RID: 11279
		internal const string ExchangeUMFaxSearchFolderClass = "IPF.Note.Microsoft.Fax";

		// Token: 0x04002C10 RID: 11280
		internal const string CommunicatorHistoryFolderClass = "IPF.Note.Microsoft.Conversation";

		// Token: 0x04002C11 RID: 11281
		internal const string OscContactSync = "IPM.Microsoft.OSC.ContactSync";

		// Token: 0x04002C12 RID: 11282
		internal const string OscSyncLockPrefix = "IPM.Microsoft.OSC.SyncLock.";

		// Token: 0x04002C13 RID: 11283
		internal const string OutlookContactLinkTimeStamp = "IPM.Microsoft.ContactLink.TimeStamp";

		// Token: 0x04002C14 RID: 11284
		internal const string QuickContactsFolder = "IPF.Contact.MOC.QuickContacts";

		// Token: 0x04002C15 RID: 11285
		internal const string ImContactListFolder = "IPF.Contact.MOC.ImContactList";

		// Token: 0x04002C16 RID: 11286
		internal const string OrganizationalContactsFolder = "IPF.Contact.OrganizationalContacts";

		// Token: 0x04002C17 RID: 11287
		private static Dictionary<StoreObjectType, string> tableContainerMessageClass = ObjectClass.BuildContainerMessageClassTable(ObjectClass.tableContainerMessageClass);

		// Token: 0x02000857 RID: 2135
		internal static class ReportSuffixes
		{
			// Token: 0x04002C18 RID: 11288
			internal const string DsnFailed = "NDR";

			// Token: 0x04002C19 RID: 11289
			internal const string DsnDelivered = "DR";

			// Token: 0x04002C1A RID: 11290
			internal const string DsnDelayed = "Delayed.DR";

			// Token: 0x04002C1B RID: 11291
			internal const string DsnRelayed = "Relayed.DR";

			// Token: 0x04002C1C RID: 11292
			internal const string DsnExpanded = "Expanded.DR";

			// Token: 0x04002C1D RID: 11293
			internal const string MdnRead = "IPNRN";

			// Token: 0x04002C1E RID: 11294
			internal const string MdnNotRead = "IPNNRN";
		}

		// Token: 0x02000858 RID: 2136
		internal static class ReportClasses
		{
			// Token: 0x06005067 RID: 20583 RVA: 0x0014E31A File Offset: 0x0014C51A
			internal static bool IsReportOfSpecialCasedClass(string itemClass)
			{
				return ObjectClass.IsOfClass(itemClass, "IPM.Note.Exchange.ActiveSync.Report");
			}

			// Token: 0x04002C1F RID: 11295
			internal const string AirsyncBadItem = "IPM.Note.Exchange.ActiveSync.Report";
		}
	}
}
