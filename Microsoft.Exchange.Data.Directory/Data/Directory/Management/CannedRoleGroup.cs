using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200074D RID: 1869
	internal enum CannedRoleGroup
	{
		// Token: 0x04003CEF RID: 15599
		[Description("Organization Management")]
		OrganizationManagement,
		// Token: 0x04003CF0 RID: 15600
		[Description("Recipient Management")]
		RecipientManagement,
		// Token: 0x04003CF1 RID: 15601
		[Description("View-Only Organization Management")]
		ViewOnlyOrganizationManagement,
		// Token: 0x04003CF2 RID: 15602
		[Description("Public Folder Management")]
		PublicFolderManagement,
		// Token: 0x04003CF3 RID: 15603
		[Description("UM Management")]
		UMManagement,
		// Token: 0x04003CF4 RID: 15604
		[Description("Help Desk")]
		HelpDesk,
		// Token: 0x04003CF5 RID: 15605
		[Description("Records Management")]
		RecordsManagement,
		// Token: 0x04003CF6 RID: 15606
		[Description("Discovery Management")]
		DiscoveryManagement,
		// Token: 0x04003CF7 RID: 15607
		[Description("Server Management")]
		ServerManagement,
		// Token: 0x04003CF8 RID: 15608
		[Description("Delegated Setup")]
		DelegatedSetup,
		// Token: 0x04003CF9 RID: 15609
		[Description("Hygiene Management")]
		HygieneManagement,
		// Token: 0x04003CFA RID: 15610
		[Description("Management Forest Operator")]
		ManagementForestOperator,
		// Token: 0x04003CFB RID: 15611
		[Description("Management Forest Tier 1 Support")]
		ManagementForestTier1Support,
		// Token: 0x04003CFC RID: 15612
		[Description("View-Only Mgmt Forest Operator")]
		ViewOnlyManagementForestOperator,
		// Token: 0x04003CFD RID: 15613
		[Description("Management Forest Monitoring")]
		ManagementForestMonitoring,
		// Token: 0x04003CFE RID: 15614
		[Description("DataCenter Management")]
		DataCenterManagement,
		// Token: 0x04003CFF RID: 15615
		[Description("View-Only Local Server Access")]
		ViewOnlyLocalServerAccess,
		// Token: 0x04003D00 RID: 15616
		[Description("Destructive Access")]
		DestructiveAccess,
		// Token: 0x04003D01 RID: 15617
		[Description("Elevated Permissions")]
		ElevatedPermissions,
		// Token: 0x04003D02 RID: 15618
		[Description("Service Accounts")]
		ServiceAccounts,
		// Token: 0x04003D03 RID: 15619
		[Description("Operations")]
		Operations,
		// Token: 0x04003D04 RID: 15620
		[Description("View-Only")]
		ViewOnly,
		// Token: 0x04003D05 RID: 15621
		[Description("Compliance Management")]
		ComplianceManagement,
		// Token: 0x04003D06 RID: 15622
		[Description("View-Only PII")]
		ViewOnlyPII,
		// Token: 0x04003D07 RID: 15623
		[Description("Capacity Destructive Access")]
		CapacityDestructiveAccess,
		// Token: 0x04003D08 RID: 15624
		[Description("Capacity Server Admins")]
		CapacityServerAdmins,
		// Token: 0x04003D09 RID: 15625
		[Description("Customer Change Access")]
		CustomerChangeAccess,
		// Token: 0x04003D0A RID: 15626
		[Description("Customer Data Access")]
		CustomerDataAccess,
		// Token: 0x04003D0B RID: 15627
		[Description("Customer Destructive Access")]
		CustomerDestructiveAccess,
		// Token: 0x04003D0C RID: 15628
		[Description("Customer PII Access")]
		CustomerPIIAccess,
		// Token: 0x04003D0D RID: 15629
		[Description("Management Admin Access")]
		ManagementAdminAccess,
		// Token: 0x04003D0E RID: 15630
		[Description("Management Server Admins")]
		ManagementServerAdmins,
		// Token: 0x04003D0F RID: 15631
		[Description("Management Change Access")]
		ManagementChangeAccess,
		// Token: 0x04003D10 RID: 15632
		[Description("Capacity DC Admins")]
		CapacityDCAdmins,
		// Token: 0x04003D11 RID: 15633
		[Description("Networking Admin Access")]
		NetworkingAdminAccess,
		// Token: 0x04003D12 RID: 15634
		[Description("Management Destructive Access")]
		ManagementDestructiveAccess,
		// Token: 0x04003D13 RID: 15635
		[Description("Management CA Core Admin")]
		ManagementCACoreAdmin,
		// Token: 0x04003D14 RID: 15636
		[Description("Mailbox Management")]
		MailboxManagement,
		// Token: 0x04003D15 RID: 15637
		[Description("Cafe Server Admins")]
		CapacityFrontendServerAdmin,
		// Token: 0x04003D16 RID: 15638
		[Description("Ffo AntiSpam Admins")]
		FfoAntiSpamAdmins,
		// Token: 0x04003D17 RID: 15639
		[Description("Dedicated Support Access")]
		DedicatedSupportAccess,
		// Token: 0x04003D18 RID: 15640
		[Description("Networking Change Access")]
		NetworkingChangeAccess,
		// Token: 0x04003D19 RID: 15641
		[Description("AppLocker Exemption")]
		AppLockerExemption = 48,
		// Token: 0x04003D1A RID: 15642
		[Description("ECS Admin - Server Access")]
		ECSAdminServerAccess,
		// Token: 0x04003D1B RID: 15643
		[Description("ECS PII Access - Server Access")]
		ECSPIIAccessServerAccess,
		// Token: 0x04003D1C RID: 15644
		[Description("ECS Admin")]
		ECSAdmin,
		// Token: 0x04003D1D RID: 15645
		[Description("ECS PII Access")]
		ECSPIIAccess,
		// Token: 0x04003D1E RID: 15646
		[Description("Access To Customer Data - DC Only")]
		AccessToCustomerDataDCOnly,
		// Token: 0x04003D1F RID: 15647
		[Description("Datacenter Operations - DC Only")]
		DatacenterOperationsDCOnly
	}
}
