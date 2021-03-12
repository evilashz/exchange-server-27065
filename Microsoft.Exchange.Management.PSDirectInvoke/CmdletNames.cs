using System;

namespace Microsoft.Exchange.Management.PSDirectInvoke
{
	// Token: 0x02000002 RID: 2
	public static class CmdletNames
	{
		// Token: 0x04000001 RID: 1
		public const string DisableApp = "Disable-App";

		// Token: 0x04000002 RID: 2
		public const string EnableApp = "Enable-App";

		// Token: 0x04000003 RID: 3
		public const string RemoveApp = "Remove-App";

		// Token: 0x04000004 RID: 4
		public const string GetCalendarNotification = "Get-CalendarNotification";

		// Token: 0x04000005 RID: 5
		public const string SetCalendarNotification = "Set-CalendarNotification";

		// Token: 0x04000006 RID: 6
		public const string GetCalendarProcessing = "Get-CalendarProcessing";

		// Token: 0x04000007 RID: 7
		public const string SetCalendarProcessing = "Set-CalendarProcessing";

		// Token: 0x04000008 RID: 8
		public const string GetCASMailbox = "Get-CASMailbox";

		// Token: 0x04000009 RID: 9
		public const string SetCASMailbox = "Set-CASMailbox";

		// Token: 0x0400000A RID: 10
		public const string GetConnectSubscription = "Get-ConnectSubscription";

		// Token: 0x0400000B RID: 11
		public const string NewConnectSubscription = "New-ConnectSubscription";

		// Token: 0x0400000C RID: 12
		public const string RemoveConnectSubscription = "Remove-ConnectSubscription";

		// Token: 0x0400000D RID: 13
		public const string SetConnectSubscription = "Set-ConnectSubscription";

		// Token: 0x0400000E RID: 14
		public const string GetHotmailSubscription = "Get-HotmailSubscription";

		// Token: 0x0400000F RID: 15
		public const string SetHotmailSubscription = "Set-HotmailSubscription";

		// Token: 0x04000010 RID: 16
		public const string GetImapSubscription = "Get-ImapSubscription";

		// Token: 0x04000011 RID: 17
		public const string NewImapSubscription = "New-ImapSubscription";

		// Token: 0x04000012 RID: 18
		public const string SetImapSubscription = "Set-ImapSubscription";

		// Token: 0x04000013 RID: 19
		public const string ImportContactList = "Import-ContactList";

		// Token: 0x04000014 RID: 20
		public const string DisableInboxRule = "Disable-InboxRule";

		// Token: 0x04000015 RID: 21
		public const string EnableInboxRule = "Enable-InboxRule";

		// Token: 0x04000016 RID: 22
		public const string GetInboxRule = "Get-InboxRule";

		// Token: 0x04000017 RID: 23
		public const string NewInboxRule = "New-InboxRule";

		// Token: 0x04000018 RID: 24
		public const string RemoveInboxRule = "Remove-InboxRule";

		// Token: 0x04000019 RID: 25
		public const string SetInboxRule = "Set-InboxRule";

		// Token: 0x0400001A RID: 26
		public const string GetMailboxAutoReplyConfiguration = "Get-MailboxAutoReplyConfiguration";

		// Token: 0x0400001B RID: 27
		public const string SetMailboxAutoReplyConfiguration = "Set-MailboxAutoReplyConfiguration";

		// Token: 0x0400001C RID: 28
		public const string GetMailboxCalendarConfiguration = "Get-MailboxCalendarConfiguration";

		// Token: 0x0400001D RID: 29
		public const string SetMailboxCalendarConfiguration = "Set-MailboxCalendarConfiguration";

		// Token: 0x0400001E RID: 30
		public const string GetMailboxJunkEmailConfiguration = "Get-MailboxJunkEmailConfiguration";

		// Token: 0x0400001F RID: 31
		public const string SetMailboxJunkEmailConfiguration = "Set-MailboxJunkEmailConfiguration";

		// Token: 0x04000020 RID: 32
		public const string GetMailboxMessageConfiguration = "Get-MailboxMessageConfiguration";

		// Token: 0x04000021 RID: 33
		public const string SetMailboxMessageConfiguration = "Set-MailboxMessageConfiguration";

		// Token: 0x04000022 RID: 34
		public const string GetMailboxRegionalConfiguration = "Get-MailboxRegionalConfiguration";

		// Token: 0x04000023 RID: 35
		public const string SetMailboxRegionalConfiguration = "Set-MailboxRegionalConfiguration";

		// Token: 0x04000024 RID: 36
		public const string GetMailboxStatistics = "Get-MailboxStatistics";

		// Token: 0x04000025 RID: 37
		public const string GetMessageCategory = "Get-MessageCategory";

		// Token: 0x04000026 RID: 38
		public const string GetMessageClassification = "Get-MessageClassification";

		// Token: 0x04000027 RID: 39
		public const string GetPopSubscription = "Get-PopSubscription";

		// Token: 0x04000028 RID: 40
		public const string NewPopSubscription = "New-PopSubscription";

		// Token: 0x04000029 RID: 41
		public const string SetPopSubscription = "Set-PopSubscription";

		// Token: 0x0400002A RID: 42
		public const string GetRetentionPolicyTag = "Get-RetentionPolicyTag";

		// Token: 0x0400002B RID: 43
		public const string SetRetentionPolicyTag = "Set-RetentionPolicyTag";

		// Token: 0x0400002C RID: 44
		public const string GetSendAddress = "Get-SendAddress";

		// Token: 0x0400002D RID: 45
		public const string GetSubscription = "Get-Subscription";

		// Token: 0x0400002E RID: 46
		public const string GetSupervisionPolicy = "Get-SupervisionPolicy";

		// Token: 0x0400002F RID: 47
		public const string NewSubscription = "New-Subscription";

		// Token: 0x04000030 RID: 48
		public const string RemoveSubscription = "Remove-Subscription";

		// Token: 0x04000031 RID: 49
		public const string GetUser = "Get-User";

		// Token: 0x04000032 RID: 50
		public const string SetUser = "Set-User";

		// Token: 0x04000033 RID: 51
		public const string NewGroupMailbox = "New-GroupMailbox";

		// Token: 0x04000034 RID: 52
		public const string GetMailbox = "Get-Mailbox";

		// Token: 0x04000035 RID: 53
		public const string NewMailbox = "New-Mailbox";

		// Token: 0x04000036 RID: 54
		public const string SetMailbox = "Set-Mailbox";

		// Token: 0x04000037 RID: 55
		public const string RemoveMailbox = "Remove-Mailbox";

		// Token: 0x04000038 RID: 56
		public const string NewSyncRequest = "New-SyncRequest";

		// Token: 0x04000039 RID: 57
		public const string GetGroupMailbox = "Get-GroupMailbox";

		// Token: 0x0400003A RID: 58
		public const string GetSyncRequest = "Get-SyncRequest";

		// Token: 0x0400003B RID: 59
		public const string GetSyncRequestStatistics = "Get-SyncRequestStatistics";

		// Token: 0x0400003C RID: 60
		public const string SetGroupMailbox = "Set-GroupMailbox";

		// Token: 0x0400003D RID: 61
		public const string SetSyncRequest = "Set-SyncRequest";

		// Token: 0x0400003E RID: 62
		public const string RemoveGroupMailbox = "Remove-GroupMailbox";

		// Token: 0x0400003F RID: 63
		public const string RemoveSyncRequest = "Remove-SyncRequest";

		// Token: 0x04000040 RID: 64
		public const string GetMobileDeviceStatistics = "Get-MobileDeviceStatistics";

		// Token: 0x04000041 RID: 65
		public const string RemoveMobileDevice = "Remove-MobileDevice";

		// Token: 0x04000042 RID: 66
		public const string ClearMobileDevice = "Clear-MobileDevice";

		// Token: 0x04000043 RID: 67
		public const string ClearTextMessagingAccount = "Clear-TextMessagingAccount";

		// Token: 0x04000044 RID: 68
		public const string GetTextMessagingAccount = "Get-TextMessagingAccount";

		// Token: 0x04000045 RID: 69
		public const string SetTextMessagingAccount = "Set-TextMessagingAccount";

		// Token: 0x04000046 RID: 70
		public const string CompareTextMessagingVerificationCode = "Compare-TextMessagingVerificationCode";

		// Token: 0x04000047 RID: 71
		public const string SendTextMessagingVerificationCode = "Send-TextMessagingVerificationCode";
	}
}
