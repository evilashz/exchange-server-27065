using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000007 RID: 7
	internal static class AdminLogCmdletSkipList
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000273E File Offset: 0x0000093E
		internal static int Count
		{
			get
			{
				return AdminLogCmdletSkipList.GetSkippedCmdletList().Count;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000274C File Offset: 0x0000094C
		private static HashSet<string> GetSkippedCmdletList()
		{
			if (AdminLogCmdletSkipList.cmdletsToBeSkipped == null)
			{
				HashSet<string> defaultValue = AdminLogCmdletSkipList.PopulateBlockedCmdletList();
				Interlocked.CompareExchange<Hookable<HashSet<string>>>(ref AdminLogCmdletSkipList.cmdletsToBeSkipped, Hookable<HashSet<string>>.Create(true, defaultValue), null);
			}
			return AdminLogCmdletSkipList.cmdletsToBeSkipped.Value;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002783 File Offset: 0x00000983
		internal static bool ShouldSkipCmdlet(string cmdlet)
		{
			return AdminLogCmdletSkipList.GetSkippedCmdletList().Contains(cmdlet);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002790 File Offset: 0x00000990
		internal static IDisposable SetCmdletBlockListTestHook(HashSet<string> cmdletList)
		{
			return AdminLogCmdletSkipList.cmdletsToBeSkipped.SetTestHook(cmdletList);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000027A0 File Offset: 0x000009A0
		private static HashSet<string> PopulateBlockedCmdletList()
		{
			return new HashSet<string>(StringComparer.OrdinalIgnoreCase)
			{
				"New-MoveRequest",
				"Set-CentralAdminDropBoxMachineEntry",
				"New-CentralAdminOperation",
				"Remove-CentralAdminParameter",
				"Remove-CentralAdminParameterDefinition",
				"New-CentralAdminParameterDefinition",
				"Add-CentralAdminParameter",
				"Set-CentralAdminMachine",
				"New-ServiceAlert",
				"Set-CentralAdminDropBoxPodEntry",
				"Set-ServiceAlert",
				"Set-CentralAdminDropBoxFailedMachineEntry",
				"Remove-MoveRequest",
				"Set-CentralAdminVlan",
				"Set-CentralAdminRouter",
				"New-CentralAdminAlertMapping",
				"New-CentralAdminLoadBalancerVirtualServer",
				"Remove-CentralAdminLock",
				"Set-CentralAdminPod",
				"New-CentralAdminPod",
				"New-CentralAdminIPDefinition",
				"Set-CentralAdminIPDefinition",
				"New-CentralAdminRouter",
				"Resume-MoveRequest",
				"New-CentralAdminLock",
				"New-ServiceChangeRequest",
				"New-CentralAdminRack",
				"Set-Organization",
				"New-GroupMailbox",
				"Set-GroupMailbox",
				"Set-MailboxPlan",
				"Set-CASMailboxPlan",
				"Set-OrganizationFlags",
				"Set-MServSyncConfigFlags",
				"New-ApprovalApplication",
				"Install-GlobalAddressLists",
				"install-TransportConfigContainer",
				"install-PerimeterConfigContainer",
				"Install-EmailAddressPolicy",
				"Install-GlobalSettingsContainer",
				"Install-InternetMessageFormat",
				"New-MicrosoftExchangeRecipient",
				"Install-FederationContainer",
				"Install-ActiveSyncDeviceClassContainer",
				"New-ApprovalApplicationContainer",
				"Set-PerimeterConfig",
				"Set-ManagementSiteLink",
				"Install-CannedAddressLists",
				"Install-DlpPolicyCollection",
				"Remove-SyncUser",
				"Remove-ArbitrationMailbox",
				"Set-TenantRelocationRequest",
				"New-TenantRelocationRequest",
				"install-Container",
				"Install-RuleCollection",
				"Set-CalendarNotification",
				"Set-CalendarProcessing",
				"Set-MailboxCalendarConfiguration",
				"Set-MailboxAutoReplyConfiguration",
				"Set-MailboxJunkEmailConfiguration",
				"Set-MailboxMessageConfiguration",
				"New-HotmailSubscription",
				"New-ImapSubscription",
				"New-PopSubscription",
				"New-Subscription",
				"Remove-Subscription",
				"Set-HotmailSubscription",
				"Set-ImapSubscription",
				"Set-PopSubscription",
				"Import-ContactList",
				"New-ConnectSubscription",
				"Remove-ConnectSubscription",
				"Set-ConnectSubscription",
				"Remove-UserPhoto",
				"Set-UserPhoto",
				"Set-MailboxRegionalConfiguration",
				"Disable-UMCallAnsweringRule",
				"Enable-UMCallAnsweringRule",
				"New-UMCallAnsweringRule",
				"Remove-UMCallAnsweringRule",
				"Set-UMCallAnsweringRule",
				"Set-UMMailbox",
				"Set-UMMailboxConfiguration",
				"Start-UMPhoneSession",
				"Stop-UMPhoneSession"
			};
		}

		// Token: 0x04000022 RID: 34
		private static Hookable<HashSet<string>> cmdletsToBeSkipped;
	}
}
