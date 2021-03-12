using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Aggregation;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Extension;
using Microsoft.Exchange.Management.MapiTasks;
using Microsoft.Exchange.Management.MapiTasks.Presentation;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Management.SecureMail;
using Microsoft.Exchange.Management.StoreTasks;
using Microsoft.Exchange.Management.Supervision;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop;

namespace Microsoft.Exchange.Management.PSDirectInvoke
{
	// Token: 0x02000003 RID: 3
	internal class CmdletTaskFactory
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		private CmdletTaskFactory()
		{
			foreach (object obj in Enum.GetValues(typeof(TaskModuleKey)))
			{
				TaskModuleKey key = (TaskModuleKey)obj;
				TaskModuleFactory.DisableModule(key);
			}
			TaskModuleFactory.EnableModule(TaskModuleKey.RunspaceServerSettingsInit);
			TaskModuleFactory.EnableModule(TaskModuleKey.RunspaceServerSettingsFinalize);
			string configStringValue = AppConfigLoader.GetConfigStringValue("PSDirectInvokeEnabledModules", string.Empty);
			string[] array = configStringValue.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string value in array)
			{
				TaskModuleKey key2;
				if (Enum.TryParse<TaskModuleKey>(value, true, out key2))
				{
					TaskModuleFactory.EnableModule(key2);
				}
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000021A4 File Offset: 0x000003A4
		public static CmdletTaskFactory Instance
		{
			get
			{
				if (CmdletTaskFactory.instance == null)
				{
					lock (CmdletTaskFactory.syncLock)
					{
						if (CmdletTaskFactory.instance == null)
						{
							CmdletTaskFactory.instance = new CmdletTaskFactory();
						}
					}
				}
				return CmdletTaskFactory.instance;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002204 File Offset: 0x00000404
		public PSLocalTask<EnableApp, object> CreateEnableAppTask(ExchangePrincipal executingUser)
		{
			EnableApp task = new EnableApp();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Enable-App", "Identity");
			return new PSLocalTask<EnableApp, object>(task);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002230 File Offset: 0x00000430
		public PSLocalTask<DisableApp, object> CreateDisableAppTask(ExchangePrincipal executingUser)
		{
			DisableApp task = new DisableApp();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Disable-App", "Identity");
			return new PSLocalTask<DisableApp, object>(task);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000225C File Offset: 0x0000045C
		public PSLocalTask<RemoveApp, object> CreateRemoveAppTask(ExchangePrincipal executingUser)
		{
			RemoveApp task = new RemoveApp();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Remove-App", "Identity");
			return new PSLocalTask<RemoveApp, object>(task);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002288 File Offset: 0x00000488
		public PSLocalTask<NewGroupMailbox, GroupMailbox> CreateNewGroupMailboxTask(ExchangePrincipal executingUser)
		{
			NewGroupMailbox task = new NewGroupMailbox();
			this.InitializeTaskToExecuteInMode(executingUser, task, "New-GroupMailbox", "GroupMailbox");
			return new PSLocalTask<NewGroupMailbox, GroupMailbox>(task);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022B4 File Offset: 0x000004B4
		public PSLocalTask<NewMailbox, Mailbox> CreateNewMonitoringMailboxTask(OrganizationId organizationId, SmtpAddress msaUserMemberName)
		{
			NewMailbox task = new NewMailbox();
			this.InitializeTaskToExecuteInMode(organizationId, null, null, msaUserMemberName, task, "New-Mailbox", "Monitoring");
			return new PSLocalTask<NewMailbox, Mailbox>(task);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022E4 File Offset: 0x000004E4
		public PSLocalTask<NewMailbox, Mailbox> CreateNewMailboxTask(OrganizationId organizationId, SmtpAddress msaUserMemberName, NetID msaUserNetID)
		{
			NewMailbox task = new NewMailbox();
			this.InitializeTaskToExecuteInMode(organizationId, null, msaUserNetID.ToString(), msaUserMemberName, task, "New-Mailbox", "User");
			return new PSLocalTask<NewMailbox, Mailbox>(task);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002318 File Offset: 0x00000518
		public PSLocalTask<RemoveMailbox, Mailbox> CreateRemoveMailboxTask(ExchangePrincipal executingUser, string parameterSet)
		{
			RemoveMailbox task = new RemoveMailbox();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Remove-Mailbox", parameterSet);
			return new PSLocalTask<RemoveMailbox, Mailbox>(task);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002340 File Offset: 0x00000540
		public PSLocalTask<RemoveMailbox, Mailbox> CreateRemoveMailboxTask(OrganizationId organizationId, SmtpAddress msaUserMemberName)
		{
			RemoveMailbox task = new RemoveMailbox();
			this.InitializeTaskToExecuteInMode(organizationId, null, null, msaUserMemberName, task, "Remove-Mailbox", "Identity");
			return new PSLocalTask<RemoveMailbox, Mailbox>(task);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002370 File Offset: 0x00000570
		public PSLocalTask<NewSyncRequest, SyncRequest> CreateNewSyncRequestTask(ExchangePrincipal executingUser, string parameterSet)
		{
			NewSyncRequest task = new NewSyncRequest();
			this.InitializeTaskToExecuteInMode(executingUser, task, "New-SyncRequest", parameterSet);
			return new PSLocalTask<NewSyncRequest, SyncRequest>(task);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002398 File Offset: 0x00000598
		public PSLocalTask<GetCalendarNotification, CalendarNotification> CreateGetCalendarNotificationTask(ExchangePrincipal executingUser)
		{
			GetCalendarNotification task = new GetCalendarNotification();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-CalendarNotification", "User");
			return new PSLocalTask<GetCalendarNotification, CalendarNotification>(task);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023C4 File Offset: 0x000005C4
		public PSLocalTask<SetCalendarNotification, CalendarNotification> CreateSetCalendarNotificationTask(ExchangePrincipal executingUser)
		{
			SetCalendarNotification task = new SetCalendarNotification();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Set-CalendarNotification", "User");
			return new PSLocalTask<SetCalendarNotification, CalendarNotification>(task);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023F0 File Offset: 0x000005F0
		public PSLocalTask<GetCalendarProcessing, CalendarConfiguration> CreateGetCalendarProcessingTask(ExchangePrincipal executingUser)
		{
			GetCalendarProcessing task = new GetCalendarProcessing();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-CalendarProcessing", "User");
			return new PSLocalTask<GetCalendarProcessing, CalendarConfiguration>(task);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000241C File Offset: 0x0000061C
		public PSLocalTask<SetCalendarProcessing, CalendarConfiguration> CreateSetCalendarProcessingTask(ExchangePrincipal executingUser)
		{
			SetCalendarProcessing task = new SetCalendarProcessing();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Set-CalendarProcessing", "User");
			return new PSLocalTask<SetCalendarProcessing, CalendarConfiguration>(task);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002448 File Offset: 0x00000648
		public PSLocalTask<GetCASMailbox, CASMailbox> CreateGetCASMailboxTask(ExchangePrincipal executingUser)
		{
			GetCASMailbox task = new GetCASMailbox();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-CASMailbox", "User");
			return new PSLocalTask<GetCASMailbox, CASMailbox>(task);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002474 File Offset: 0x00000674
		public PSLocalTask<SetCASMailbox, CASMailbox> CreateSetCASMailboxTask(ExchangePrincipal executingUser)
		{
			SetCASMailbox task = new SetCASMailbox();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Set-CASMailbox", "User");
			return new PSLocalTask<SetCASMailbox, CASMailbox>(task);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024A0 File Offset: 0x000006A0
		public PSLocalTask<GetConnectSubscription, ConnectSubscriptionProxy> CreateGetConnectSubscriptionTask(ExchangePrincipal executingUser)
		{
			GetConnectSubscription task = new GetConnectSubscription();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-ConnectSubscription", "Identity");
			return new PSLocalTask<GetConnectSubscription, ConnectSubscriptionProxy>(task);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024CC File Offset: 0x000006CC
		public PSLocalTask<NewConnectSubscription, ConnectSubscriptionProxy> CreateNewConnectSubscriptionTask(ExchangePrincipal executingUser, string parameterSet)
		{
			NewConnectSubscription task = new NewConnectSubscription();
			this.InitializeTaskToExecuteInMode(executingUser, task, "New-ConnectSubscription", parameterSet);
			return new PSLocalTask<NewConnectSubscription, ConnectSubscriptionProxy>(task);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024F4 File Offset: 0x000006F4
		public PSLocalTask<RemoveConnectSubscription, object> CreateRemoveConnectSubscriptionTask(ExchangePrincipal executingUser)
		{
			RemoveConnectSubscription task = new RemoveConnectSubscription();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Remove-ConnectSubscription", "Identity");
			return new PSLocalTask<RemoveConnectSubscription, object>(task);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002520 File Offset: 0x00000720
		public PSLocalTask<SetConnectSubscription, object> CreateSetConnectSubscriptionTask(ExchangePrincipal executingUser, string parameterSet)
		{
			SetConnectSubscription task = new SetConnectSubscription();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Set-ConnectSubscription", parameterSet);
			return new PSLocalTask<SetConnectSubscription, object>(task);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002548 File Offset: 0x00000748
		public PSLocalTask<GetHotmailSubscription, HotmailSubscriptionProxy> CreateGetHotmailSubscriptionTask(ExchangePrincipal executingUser)
		{
			GetHotmailSubscription task = new GetHotmailSubscription();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-HotmailSubscription", "Identity");
			return new PSLocalTask<GetHotmailSubscription, HotmailSubscriptionProxy>(task);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002574 File Offset: 0x00000774
		public PSLocalTask<SetHotmailSubscription, HotmailSubscriptionProxy> CreateSetHotmailSubscriptionTask(ExchangePrincipal executingUser)
		{
			SetHotmailSubscription task = new SetHotmailSubscription();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Set-HotmailSubscription", "Identity");
			return new PSLocalTask<SetHotmailSubscription, HotmailSubscriptionProxy>(task);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000025A0 File Offset: 0x000007A0
		public PSLocalTask<GetImapSubscription, IMAPSubscriptionProxy> CreateGetImapSubscriptionTask(ExchangePrincipal executingUser)
		{
			GetImapSubscription task = new GetImapSubscription();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-ImapSubscription", "Identity");
			return new PSLocalTask<GetImapSubscription, IMAPSubscriptionProxy>(task);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025CC File Offset: 0x000007CC
		public PSLocalTask<NewImapSubscription, IMAPSubscriptionProxy> CreateNewImapSubscriptionTask(ExchangePrincipal executingUser)
		{
			NewImapSubscription task = new NewImapSubscription();
			this.InitializeTaskToExecuteInMode(executingUser, task, "New-ImapSubscription", "Identity");
			return new PSLocalTask<NewImapSubscription, IMAPSubscriptionProxy>(task);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000025F8 File Offset: 0x000007F8
		public PSLocalTask<SetImapSubscription, IMAPSubscriptionProxy> CreateSetImapSubscriptionTask(ExchangePrincipal executingUser)
		{
			SetImapSubscription task = new SetImapSubscription();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Set-ImapSubscription", "Identity");
			return new PSLocalTask<SetImapSubscription, IMAPSubscriptionProxy>(task);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002624 File Offset: 0x00000824
		public PSLocalTask<ImportContactList, ImportContactListResult> CreateImportContactListTask(ExchangePrincipal executingUser)
		{
			ImportContactList task = new ImportContactList();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Import-ContactList", "Identity");
			return new PSLocalTask<ImportContactList, ImportContactListResult>(task);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002650 File Offset: 0x00000850
		public PSLocalTask<DisableInboxRule, InboxRule> CreateDisableInboxRuleTask(ExchangePrincipal executingUser)
		{
			DisableInboxRule task = new DisableInboxRule();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Disable-InboxRule", "Identity");
			return new PSLocalTask<DisableInboxRule, InboxRule>(task);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000267C File Offset: 0x0000087C
		public PSLocalTask<EnableInboxRule, InboxRule> CreateEnableInboxRuleTask(ExchangePrincipal executingUser)
		{
			EnableInboxRule task = new EnableInboxRule();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Enable-InboxRule", "Identity");
			return new PSLocalTask<EnableInboxRule, InboxRule>(task);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000026A8 File Offset: 0x000008A8
		public PSLocalTask<GetInboxRule, InboxRule> CreateGetInboxRuleTask(ExchangePrincipal executingUser)
		{
			GetInboxRule task = new GetInboxRule();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-InboxRule", "Identity");
			return new PSLocalTask<GetInboxRule, InboxRule>(task);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026D4 File Offset: 0x000008D4
		public PSLocalTask<NewInboxRule, InboxRule> CreateNewInboxRuleTask(ExchangePrincipal executingUser)
		{
			NewInboxRule task = new NewInboxRule();
			this.InitializeTaskToExecuteInMode(executingUser, task, "New-InboxRule", "Identity");
			return new PSLocalTask<NewInboxRule, InboxRule>(task);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002700 File Offset: 0x00000900
		public PSLocalTask<RemoveInboxRule, InboxRule> CreateRemoveInboxRuleTask(ExchangePrincipal executingUser)
		{
			RemoveInboxRule task = new RemoveInboxRule();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Remove-InboxRule", "Identity");
			return new PSLocalTask<RemoveInboxRule, InboxRule>(task);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000272C File Offset: 0x0000092C
		public PSLocalTask<SetInboxRule, InboxRule> CreateSetInboxRuleTask(ExchangePrincipal executingUser)
		{
			SetInboxRule task = new SetInboxRule();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Set-InboxRule", "Identity");
			return new PSLocalTask<SetInboxRule, InboxRule>(task);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002758 File Offset: 0x00000958
		public PSLocalTask<GetMailboxCalendarConfiguration, MailboxCalendarConfiguration> CreateGetMailboxCalendarConfigurationTask(ExchangePrincipal executingUser)
		{
			GetMailboxCalendarConfiguration task = new GetMailboxCalendarConfiguration();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-MailboxCalendarConfiguration", "User");
			return new PSLocalTask<GetMailboxCalendarConfiguration, MailboxCalendarConfiguration>(task);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002784 File Offset: 0x00000984
		public PSLocalTask<GetMailboxAutoReplyConfiguration, MailboxAutoReplyConfiguration> CreateGetMailboxAutoReplyConfigurationTask(ExchangePrincipal executingUser)
		{
			GetMailboxAutoReplyConfiguration task = new GetMailboxAutoReplyConfiguration();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-MailboxAutoReplyConfiguration", "User");
			return new PSLocalTask<GetMailboxAutoReplyConfiguration, MailboxAutoReplyConfiguration>(task);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000027B0 File Offset: 0x000009B0
		public PSLocalTask<SetMailboxAutoReplyConfiguration, MailboxAutoReplyConfiguration> CreateSetMailboxAutoReplyConfigurationTask(ExchangePrincipal executingUser)
		{
			SetMailboxAutoReplyConfiguration task = new SetMailboxAutoReplyConfiguration();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Set-MailboxAutoReplyConfiguration", "User");
			return new PSLocalTask<SetMailboxAutoReplyConfiguration, MailboxAutoReplyConfiguration>(task);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000027DC File Offset: 0x000009DC
		public PSLocalTask<SetMailboxCalendarConfiguration, MailboxCalendarConfiguration> CreateSetMailboxCalendarConfigurationTask(ExchangePrincipal executingUser)
		{
			SetMailboxCalendarConfiguration task = new SetMailboxCalendarConfiguration();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Set-MailboxCalendarConfiguration", "User");
			return new PSLocalTask<SetMailboxCalendarConfiguration, MailboxCalendarConfiguration>(task);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002808 File Offset: 0x00000A08
		public PSLocalTask<GetMailboxJunkEmailConfiguration, MailboxJunkEmailConfiguration> CreateGetMailboxJunkEmailConfigurationTask(ExchangePrincipal executingUser)
		{
			GetMailboxJunkEmailConfiguration task = new GetMailboxJunkEmailConfiguration();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-MailboxJunkEmailConfiguration", "Identity");
			return new PSLocalTask<GetMailboxJunkEmailConfiguration, MailboxJunkEmailConfiguration>(task);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002834 File Offset: 0x00000A34
		public PSLocalTask<SetMailboxJunkEmailConfiguration, MailboxJunkEmailConfiguration> CreateSetMailboxJunkEmailConfigurationTask(ExchangePrincipal executingUser)
		{
			SetMailboxJunkEmailConfiguration task = new SetMailboxJunkEmailConfiguration();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Set-MailboxJunkEmailConfiguration", "Identity");
			return new PSLocalTask<SetMailboxJunkEmailConfiguration, MailboxJunkEmailConfiguration>(task);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002860 File Offset: 0x00000A60
		public PSLocalTask<GetMailboxRegionalConfiguration, MailboxRegionalConfiguration> CreateGetMailboxRegionalConfigurationTask(ExchangePrincipal executingUser)
		{
			GetMailboxRegionalConfiguration task = new GetMailboxRegionalConfiguration();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-MailboxRegionalConfiguration", "Identity");
			return new PSLocalTask<GetMailboxRegionalConfiguration, MailboxRegionalConfiguration>(task);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000288C File Offset: 0x00000A8C
		public PSLocalTask<SetMailboxRegionalConfiguration, MailboxRegionalConfiguration> CreateSetMailboxRegionalConfigurationTask(ExchangePrincipal executingUser)
		{
			SetMailboxRegionalConfiguration task = new SetMailboxRegionalConfiguration();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Set-MailboxRegionalConfiguration", "Identity");
			return new PSLocalTask<SetMailboxRegionalConfiguration, MailboxRegionalConfiguration>(task);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000028B8 File Offset: 0x00000AB8
		public PSLocalTask<GetMailboxMessageConfiguration, MailboxMessageConfiguration> CreateGetMailboxMessageConfigurationTask(ExchangePrincipal executingUser)
		{
			GetMailboxMessageConfiguration task = new GetMailboxMessageConfiguration();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-MailboxMessageConfiguration", "User");
			return new PSLocalTask<GetMailboxMessageConfiguration, MailboxMessageConfiguration>(task);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000028E4 File Offset: 0x00000AE4
		public PSLocalTask<SetMailboxMessageConfiguration, MailboxMessageConfiguration> CreateSetMailboxMessageConfigurationTask(ExchangePrincipal executingUser)
		{
			SetMailboxMessageConfiguration task = new SetMailboxMessageConfiguration();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Set-MailboxMessageConfiguration", "User");
			return new PSLocalTask<SetMailboxMessageConfiguration, MailboxMessageConfiguration>(task);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002910 File Offset: 0x00000B10
		public PSLocalTask<GetMessageCategory, MessageCategory> CreateGetMessageCategoryTask(ExchangePrincipal executingUser)
		{
			GetMessageCategory task = new GetMessageCategory();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-MessageCategory", "Identity");
			return new PSLocalTask<GetMessageCategory, MessageCategory>(task);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000293C File Offset: 0x00000B3C
		public PSLocalTask<GetMessageClassification, MessageClassification> CreateGetMessageClassificationTask(ExchangePrincipal executingUser)
		{
			GetMessageClassification task = new GetMessageClassification();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-MessageClassification", "Identity");
			return new PSLocalTask<GetMessageClassification, MessageClassification>(task);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002968 File Offset: 0x00000B68
		public PSLocalTask<GetMailboxStatistics, MailboxStatistics> CreateGetMailboxStatisticsTask(ExchangePrincipal executingUser)
		{
			GetMailboxStatistics task = new GetMailboxStatistics();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-MailboxStatistics", "Identity");
			return new PSLocalTask<GetMailboxStatistics, MailboxStatistics>(task);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002994 File Offset: 0x00000B94
		public PSLocalTask<GetPopSubscription, PopSubscriptionProxy> CreateGetPopSubscriptionTask(ExchangePrincipal executingUser)
		{
			GetPopSubscription task = new GetPopSubscription();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-PopSubscription", "Identity");
			return new PSLocalTask<GetPopSubscription, PopSubscriptionProxy>(task);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000029C0 File Offset: 0x00000BC0
		public PSLocalTask<NewPopSubscription, PopSubscriptionProxy> CreateNewPopSubscriptionTask(ExchangePrincipal executingUser)
		{
			NewPopSubscription task = new NewPopSubscription();
			this.InitializeTaskToExecuteInMode(executingUser, task, "New-PopSubscription", "Identity");
			return new PSLocalTask<NewPopSubscription, PopSubscriptionProxy>(task);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029EC File Offset: 0x00000BEC
		public PSLocalTask<SetPopSubscription, PopSubscriptionProxy> CreateSetPopSubscriptionTask(ExchangePrincipal executingUser)
		{
			SetPopSubscription task = new SetPopSubscription();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Set-PopSubscription", "Identity");
			return new PSLocalTask<SetPopSubscription, PopSubscriptionProxy>(task);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A18 File Offset: 0x00000C18
		public PSLocalTask<GetRetentionPolicyTag, RetentionPolicyTag> CreateGetRetentionPolicyTagTask(ExchangePrincipal executingUser)
		{
			GetRetentionPolicyTag task = new GetRetentionPolicyTag();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-RetentionPolicyTag", "Identity");
			return new PSLocalTask<GetRetentionPolicyTag, RetentionPolicyTag>(task);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A44 File Offset: 0x00000C44
		public PSLocalTask<SetRetentionPolicyTag, RetentionPolicyTag> CreateSetRetentionPolicyTagTask(ExchangePrincipal executingUser)
		{
			SetRetentionPolicyTag task = new SetRetentionPolicyTag();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Set-RetentionPolicyTag", "ParameterSetMailboxTask");
			return new PSLocalTask<SetRetentionPolicyTag, RetentionPolicyTag>(task);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A70 File Offset: 0x00000C70
		public PSLocalTask<GetSendAddress, SendAddress> CreateGetSendAddressTask(ExchangePrincipal executingUser)
		{
			GetSendAddress task = new GetSendAddress();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-SendAddress", "Identity");
			return new PSLocalTask<GetSendAddress, SendAddress>(task);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A9C File Offset: 0x00000C9C
		public PSLocalTask<GetSubscription, PimSubscriptionProxy> CreateGetSubscriptionTask(ExchangePrincipal executingUser)
		{
			GetSubscription task = new GetSubscription();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-Subscription", "Identity");
			return new PSLocalTask<GetSubscription, PimSubscriptionProxy>(task);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public PSLocalTask<NewSubscription, PimSubscriptionProxy> CreateNewSubscriptionTask(ExchangePrincipal executingUser)
		{
			NewSubscription task = new NewSubscription();
			this.InitializeTaskToExecuteInMode(executingUser, task, "New-Subscription", "Identity");
			return new PSLocalTask<NewSubscription, PimSubscriptionProxy>(task);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public PSLocalTask<RemoveSubscription, PimSubscriptionProxy> CreateRemoveSubscriptionTask(ExchangePrincipal executingUser)
		{
			RemoveSubscription task = new RemoveSubscription();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Remove-Subscription", "Identity");
			return new PSLocalTask<RemoveSubscription, PimSubscriptionProxy>(task);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002B20 File Offset: 0x00000D20
		public PSLocalTask<GetUser, User> CreateGetUserTask(ExchangePrincipal executingUser)
		{
			GetUser task = new GetUser();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-User", "Identity");
			return new PSLocalTask<GetUser, User>(task);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002B4C File Offset: 0x00000D4C
		public PSLocalTask<SetUser, User> CreateSetUserTask(ExchangePrincipal executingUser)
		{
			SetUser task = new SetUser();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Set-User", "Identity");
			return new PSLocalTask<SetUser, User>(task);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002B78 File Offset: 0x00000D78
		public PSLocalTask<GetGroupMailbox, GroupMailbox> CreateGetGroupMailboxTask(ExchangePrincipal executingUser)
		{
			GetGroupMailbox task = new GetGroupMailbox();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-GroupMailbox", "GroupMailbox");
			return new PSLocalTask<GetGroupMailbox, GroupMailbox>(task);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002BA4 File Offset: 0x00000DA4
		public PSLocalTask<GetSyncRequest, SyncRequest> CreateGetSyncRequestTask(ExchangePrincipal executingUser)
		{
			GetSyncRequest task = new GetSyncRequest();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-SyncRequest", "Identity");
			return new PSLocalTask<GetSyncRequest, SyncRequest>(task);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public PSLocalTask<GetSyncRequestStatistics, SyncRequestStatistics> CreateGetSyncRequestStatisticsTask(ExchangePrincipal executingUser)
		{
			GetSyncRequestStatistics task = new GetSyncRequestStatistics();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-SyncRequestStatistics", "Identity");
			return new PSLocalTask<GetSyncRequestStatistics, SyncRequestStatistics>(task);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002BFC File Offset: 0x00000DFC
		public PSLocalTask<SetGroupMailbox, object> CreateSetGroupMailboxTask(ExchangePrincipal executingUser)
		{
			SetGroupMailbox task = new SetGroupMailbox();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Set-GroupMailbox", "GroupMailbox");
			return new PSLocalTask<SetGroupMailbox, object>(task);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002C28 File Offset: 0x00000E28
		public PSLocalTask<GetMailbox, Mailbox> CreateGetMailboxTask(ExchangePrincipal executingUser, string parameterSet)
		{
			GetMailbox task = new GetMailbox();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-Mailbox", parameterSet);
			return new PSLocalTask<GetMailbox, Mailbox>(task);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002C50 File Offset: 0x00000E50
		public PSLocalTask<SetMailbox, object> CreateSetMailboxTask(ExchangePrincipal executingUser, string parameterSet)
		{
			SetMailbox task = new SetMailbox();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Set-Mailbox", parameterSet);
			return new PSLocalTask<SetMailbox, object>(task);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002C78 File Offset: 0x00000E78
		public PSLocalTask<SetMailbox, object> CreateSetMailboxTask(OrganizationId monitoringOrganizationId)
		{
			SetMailbox task = new SetMailbox();
			this.InitializeTaskToExecuteInMode(monitoringOrganizationId, null, null, SmtpAddress.Empty, task, "Set-Mailbox", "Identity");
			return new PSLocalTask<SetMailbox, object>(task);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002CAC File Offset: 0x00000EAC
		public PSLocalTask<SetSyncRequest, object> CreateSetSyncRequestTask(ExchangePrincipal executingUser)
		{
			SetSyncRequest task = new SetSyncRequest();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Set-SyncRequest", "Identity");
			return new PSLocalTask<SetSyncRequest, object>(task);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public PSLocalTask<RemoveGroupMailbox, object> CreateRemoveGroupMailboxTask(ExchangePrincipal executingUser)
		{
			RemoveGroupMailbox task = new RemoveGroupMailbox();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Remove-GroupMailbox", "GroupMailbox");
			return new PSLocalTask<RemoveGroupMailbox, object>(task);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002D04 File Offset: 0x00000F04
		public PSLocalTask<RemoveSyncRequest, object> CreateRemoveSyncRequestTask(ExchangePrincipal executingUser)
		{
			RemoveSyncRequest task = new RemoveSyncRequest();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Remove-SyncRequest", "Identity");
			return new PSLocalTask<RemoveSyncRequest, object>(task);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002D30 File Offset: 0x00000F30
		public PSLocalTask<GetMobileDeviceStatistics, MobileDeviceConfiguration> CreateGetMobileDeviceStatisticsTask(ExchangePrincipal executingUser)
		{
			GetMobileDeviceStatistics task = new GetMobileDeviceStatistics();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-MobileDeviceStatistics", "Mailbox");
			return new PSLocalTask<GetMobileDeviceStatistics, MobileDeviceConfiguration>(task);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002D5C File Offset: 0x00000F5C
		public PSLocalTask<RemoveMobileDevice, MobileDevice> CreateRemoveMobileDeviceTask(ExchangePrincipal executingUser)
		{
			RemoveMobileDevice task = new RemoveMobileDevice();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Remove-MobileDevice", "Identity");
			return new PSLocalTask<RemoveMobileDevice, MobileDevice>(task);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002D88 File Offset: 0x00000F88
		public PSLocalTask<ClearMobileDevice, MobileDevice> CreateClearMobileDeviceTask(ExchangePrincipal executingUser)
		{
			ClearMobileDevice task = new ClearMobileDevice();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Clear-MobileDevice", "Identity");
			return new PSLocalTask<ClearMobileDevice, MobileDevice>(task);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public PSLocalTask<ClearTextMessagingAccount, object> CreateClearTextMessagingAccountTask(ExchangePrincipal executingUser)
		{
			ClearTextMessagingAccount task = new ClearTextMessagingAccount();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Clear-TextMessagingAccount", "Identity");
			return new PSLocalTask<ClearTextMessagingAccount, object>(task);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public PSLocalTask<GetTextMessagingAccount, TextMessagingAccount> CreateGetTextMessagingAccountTask(ExchangePrincipal executingUser)
		{
			GetTextMessagingAccount task = new GetTextMessagingAccount();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-TextMessagingAccount", "Identity");
			return new PSLocalTask<GetTextMessagingAccount, TextMessagingAccount>(task);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E0C File Offset: 0x0000100C
		public PSLocalTask<SetTextMessagingAccount, object> CreateSetTextMessagingAccountTask(ExchangePrincipal executingUser)
		{
			SetTextMessagingAccount task = new SetTextMessagingAccount();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Set-TextMessagingAccount", "Identity");
			return new PSLocalTask<SetTextMessagingAccount, object>(task);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002E38 File Offset: 0x00001038
		public PSLocalTask<CompareTextMessagingVerificationCode, object> CreateCompareTextMessagingVerificationCodeTask(ExchangePrincipal executingUser)
		{
			CompareTextMessagingVerificationCode task = new CompareTextMessagingVerificationCode();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Compare-TextMessagingVerificationCode", "Identity");
			return new PSLocalTask<CompareTextMessagingVerificationCode, object>(task);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002E64 File Offset: 0x00001064
		public PSLocalTask<SendTextMessagingVerificationCode, object> CreateSendTextMessagingVerificationCodeTask(ExchangePrincipal executingUser)
		{
			SendTextMessagingVerificationCode task = new SendTextMessagingVerificationCode();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Send-TextMessagingVerificationCode", "Identity");
			return new PSLocalTask<SendTextMessagingVerificationCode, object>(task);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002E90 File Offset: 0x00001090
		public PSLocalTask<GetSupervisionPolicy, SupervisionPolicy> CreateGetSupervisionPolicyTask(ExchangePrincipal executingUser)
		{
			GetSupervisionPolicy task = new GetSupervisionPolicy();
			this.InitializeTaskToExecuteInMode(executingUser, task, "Get-SupervisionPolicy", "Identity");
			return new PSLocalTask<GetSupervisionPolicy, SupervisionPolicy>(task);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002EBC File Offset: 0x000010BC
		private void InitializeTaskToExecuteInMode(OrganizationId executingUserOrganizationId, ADObjectId executingUserId, string executingUserIdentityName, SmtpAddress executingWindowsLiveId, Task task, string cmdletName, string parameterSet)
		{
			task.CurrentTaskContext.UserInfo = new TaskUserInfo(executingUserOrganizationId, executingUserOrganizationId, executingUserId, executingUserIdentityName, executingWindowsLiveId);
			TaskInvocationInfo taskInvocationInfo = TaskInvocationInfo.CreateForDirectTaskInvocation(cmdletName);
			taskInvocationInfo.IsDebugOn = false;
			taskInvocationInfo.IsVerboseOn = false;
			if (parameterSet != null)
			{
				taskInvocationInfo.ParameterSetName = parameterSet;
			}
			task.CurrentTaskContext.InvocationInfo = taskInvocationInfo;
			PSLocalSessionState sessionState = new PSLocalSessionState();
			task.CurrentTaskContext.SessionState = sessionState;
			ExchangePropertyContainer.InitExchangePropertyContainer(sessionState, executingUserOrganizationId, executingUserId);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002F2C File Offset: 0x0000112C
		private void InitializeTaskToExecuteInMode(ExchangePrincipal executingUser, Task task, string cmdletName, string parameterSet)
		{
			SmtpAddress executingWindowsLiveId = SmtpAddress.Empty;
			IUserPrincipal userPrincipal = executingUser as IUserPrincipal;
			if (userPrincipal != null)
			{
				executingWindowsLiveId = userPrincipal.WindowsLiveId;
			}
			this.InitializeTaskToExecuteInMode(executingUser.MailboxInfo.OrganizationId, executingUser.ObjectId, executingUser.ObjectId.Name, executingWindowsLiveId, task, cmdletName, parameterSet);
		}

		// Token: 0x04000048 RID: 72
		private const string PSDirectInvokeEnabledModulesConfigName = "PSDirectInvokeEnabledModules";

		// Token: 0x04000049 RID: 73
		private static readonly object syncLock = new object();

		// Token: 0x0400004A RID: 74
		private static volatile CmdletTaskFactory instance;
	}
}
