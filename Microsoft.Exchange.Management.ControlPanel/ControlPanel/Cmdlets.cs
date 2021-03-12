using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200069D RID: 1693
	internal static class Cmdlets
	{
		// Token: 0x040030C6 RID: 12486
		public const string GetRecipient = "Get-Recipient";

		// Token: 0x040030C7 RID: 12487
		public const string GetUser = "Get-User";

		// Token: 0x040030C8 RID: 12488
		public const string SetUser = "Set-User";

		// Token: 0x040030C9 RID: 12489
		public const string GetGroup = "Get-Group";

		// Token: 0x040030CA RID: 12490
		public const string SetGroup = "Set-Group";

		// Token: 0x040030CB RID: 12491
		public const string GetDistributionGroup = "Get-DistributionGroup";

		// Token: 0x040030CC RID: 12492
		public const string SetDistributionGroup = "Set-DistributionGroup";

		// Token: 0x040030CD RID: 12493
		public const string NewDistributionGroup = "New-DistributionGroup";

		// Token: 0x040030CE RID: 12494
		public const string RemoveDistributionGroup = "Remove-DistributionGroup";

		// Token: 0x040030CF RID: 12495
		public const string UpdateDistributionGroupMember = "Update-DistributionGroupMember";

		// Token: 0x040030D0 RID: 12496
		public const string AddDistributionGroupMember = "Add-DistributionGroupMember";

		// Token: 0x040030D1 RID: 12497
		public const string RemoveDistributionGroupMember = "Remove-DistributionGroupMember";

		// Token: 0x040030D2 RID: 12498
		public const string GetMailbox = "Get-Mailbox";

		// Token: 0x040030D3 RID: 12499
		public const string SetMailbox = "Set-Mailbox";

		// Token: 0x040030D4 RID: 12500
		public const string NewMailbox = "New-Mailbox";

		// Token: 0x040030D5 RID: 12501
		public const string RemoveMailbox = "Remove-Mailbox";

		// Token: 0x040030D6 RID: 12502
		public const string EnableMailbox = "Enable-Mailbox";

		// Token: 0x040030D7 RID: 12503
		public const string DisableMailbox = "Disable-Mailbox";

		// Token: 0x040030D8 RID: 12504
		public const string GetRemovedMailbox = "Get-RemovedMailbox";

		// Token: 0x040030D9 RID: 12505
		public const string GetCasMailbox = "Get-CasMailbox";

		// Token: 0x040030DA RID: 12506
		public const string SetCasMailbox = "Set-CasMailbox";

		// Token: 0x040030DB RID: 12507
		public const string GetMailboxStatistics = "Get-MailboxStatistics";

		// Token: 0x040030DC RID: 12508
		public const string GetRetentionPolicyTag = "Get-RetentionPolicyTag";

		// Token: 0x040030DD RID: 12509
		public const string SetRetentionPolicyTag = "Set-RetentionPolicyTag";

		// Token: 0x040030DE RID: 12510
		public const string SetPopSubscription = "Set-PopSubscription";

		// Token: 0x040030DF RID: 12511
		public const string SetImapSubscription = "Set-ImapSubscription";

		// Token: 0x040030E0 RID: 12512
		public const string SetContact = "Set-Contact";

		// Token: 0x040030E1 RID: 12513
		public const string SetMailContact = "Set-MailContact";

		// Token: 0x040030E2 RID: 12514
		public const string NewMailContact = "New-MailContact";

		// Token: 0x040030E3 RID: 12515
		public const string GetContact = "Get-Contact";

		// Token: 0x040030E4 RID: 12516
		public const string GetMailContact = "Get-MailContact";

		// Token: 0x040030E5 RID: 12517
		public const string RemoveMailContact = "Remove-MailContact";

		// Token: 0x040030E6 RID: 12518
		public const string GetAdPermission = "Get-ADPermission";

		// Token: 0x040030E7 RID: 12519
		public const string AddAdPermission = "Add-ADPermission";

		// Token: 0x040030E8 RID: 12520
		public const string RemoveAdPermission = "Remove-ADPermission";

		// Token: 0x040030E9 RID: 12521
		public const string GetRecipientPermission = "Get-RecipientPermission";

		// Token: 0x040030EA RID: 12522
		public const string AddRecipientPermission = "Add-RecipientPermission";

		// Token: 0x040030EB RID: 12523
		public const string RemoveRecipientPermission = "Remove-RecipientPermission";

		// Token: 0x040030EC RID: 12524
		public const string SetUMMailbox = "Set-UMMailbox";

		// Token: 0x040030ED RID: 12525
		public const string GetUMMailbox = "Get-UMMailbox";

		// Token: 0x040030EE RID: 12526
		public const string EnableUMMailbox = "Enable-UMMailbox";

		// Token: 0x040030EF RID: 12527
		public const string DisableUMMailbox = "Disable-UMMailbox";

		// Token: 0x040030F0 RID: 12528
		public const string SetUMMailboxPin = "Set-UMMailboxPin";

		// Token: 0x040030F1 RID: 12529
		public const string GetUMMailboxPin = "Get-UMMailboxPin";

		// Token: 0x040030F2 RID: 12530
		public const string GetUMDialPlan = "Get-UMDialPlan";

		// Token: 0x040030F3 RID: 12531
		public const string NewUMMailboxPolicy = "New-UMMailboxPolicy";

		// Token: 0x040030F4 RID: 12532
		public const string GetUMMailboxPolicy = "Get-UMMailboxPolicy";

		// Token: 0x040030F5 RID: 12533
		public const string SetUMMailboxPolicy = "Set-UMMailboxPolicy";

		// Token: 0x040030F6 RID: 12534
		public const string RemoveUMMailboxPolicy = "Remove-UMMailboxPolicy";

		// Token: 0x040030F7 RID: 12535
		public const string GetUMIPGateway = "Get-UMIPGateway";

		// Token: 0x040030F8 RID: 12536
		public const string NewUMIPGateway = "New-UMIPGateway";

		// Token: 0x040030F9 RID: 12537
		public const string SetUMIPGateway = "Set-UMIPGateway";

		// Token: 0x040030FA RID: 12538
		public const string RemoveUMIPGateway = "Remove-UMIPGateway";

		// Token: 0x040030FB RID: 12539
		public const string DisableUMIPGateway = "Disable-UMIPGateway";

		// Token: 0x040030FC RID: 12540
		public const string EnableUMIPGateway = "Enable-UMIPGateway";

		// Token: 0x040030FD RID: 12541
		public const string GetUMHuntGroup = "Get-UMHuntGroup";

		// Token: 0x040030FE RID: 12542
		public const string NewUMHuntGroup = "New-UMHuntGroup";

		// Token: 0x040030FF RID: 12543
		public const string RemoveUMHuntGroup = "Remove-UMHuntGroup";

		// Token: 0x04003100 RID: 12544
		public const string NewUMDialPlan = "New-UMDialPlan";

		// Token: 0x04003101 RID: 12545
		public const string SetUMDialPlan = "Set-UMDialPlan";

		// Token: 0x04003102 RID: 12546
		public const string RemoveUMDialPlan = "Remove-UMDialPlan";

		// Token: 0x04003103 RID: 12547
		public const string ImportUMPrompt = "Import-UMPrompt";

		// Token: 0x04003104 RID: 12548
		public const string NewUMAutoAttendant = "New-UMAutoAttendant";

		// Token: 0x04003105 RID: 12549
		public const string SetUMAutoAttendant = "Set-UMAutoAttendant";

		// Token: 0x04003106 RID: 12550
		public const string RemoveUMAutoAttendant = "Remove-UMAutoAttendant";

		// Token: 0x04003107 RID: 12551
		public const string GetUMAutoAttendant = "Get-UMAutoAttendant";

		// Token: 0x04003108 RID: 12552
		public const string DisableUMAutoAttendant = "Disable-UMAutoAttendant";

		// Token: 0x04003109 RID: 12553
		public const string EnableUMAutoAttendant = "Enable-UMAutoAttendant";

		// Token: 0x0400310A RID: 12554
		public const string UndoSoftDeletedMailbox = "Undo-SoftDeletedMailbox";

		// Token: 0x0400310B RID: 12555
		public const string SetOwaMailboxPolicy = "Set-OwaMailboxPolicy";
	}
}
