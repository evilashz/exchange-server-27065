using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200044A RID: 1098
	public enum RoleType
	{
		// Token: 0x0400215C RID: 8540
		[Description("Custom")]
		Custom = -2147483648,
		// Token: 0x0400215D RID: 8541
		[Description("UnScoped")]
		UnScoped = 256,
		// Token: 0x0400215E RID: 8542
		[Obsolete("Role type deprecated. Use instead: Mail Recipients,Federated Sharing,Database Availability Groups,Databases,Public Folders,Address Lists,CAS Mailbox Policies,Disaster Recovery,Monitoring,Database Copies,Unified Messaging,Journaling,Remote and Accepted Domains,E-Mail Address Policies,Transport Rules,Send Connectors,Edge Subscriptions,Organization Transport Settings,Exchange Servers,Exchange Virtual Directories,Exchange Server Certificates,POP3 And IMAP4 Protocols,Receive Connectors,UM Mailboxes,User Options,Security Group Creation and Membership,Mail Recipient Creation,Message Tracking,Role Management,View-Only Recipients,View-Only Configuration,Distribution Groups,Mail Enabled Public Folders,Move Mailboxes,Reset Password,Retention Management,Audit Logs,Support Diagnostics,Mailbox Search,Information Rights Management,Legal Hold,Mail Tips,Public Folder Replication,Active Directory Permissions,UM Prompts,Migration,Transport Hygiene,DataCenter Operations,Transport Queues,Supervision,Cmdlet Extension Agents,Organization Configuration,Organization Client Access,Exchange Connectors,Transport Agents")]
		[Description("OrganizationManagement")]
		OrganizationManagement = 1,
		// Token: 0x0400215F RID: 8543
		[Description("RecipientManagement")]
		[Obsolete("Role type deprecated. Use instead: Mail Recipients,Distribution Groups,Mail Enabled Public Folders,Move Mailboxes,Reset Password,Mail Recipient Creation,Message Tracking,CAS Mailbox Policies,Migration.")]
		RecipientManagement = 3,
		// Token: 0x04002160 RID: 8544
		[Description("ViewOnlyOrganizationManagement")]
		[Obsolete("Role type deprecated. Use instead: View-Only Recipients, View-Only Configuration or Monitoring.")]
		ViewOnlyOrganizationManagement,
		// Token: 0x04002161 RID: 8545
		[Description("DistributionGroupManagement")]
		DistributionGroupManagement,
		// Token: 0x04002162 RID: 8546
		[Description("MyDistributionGroups")]
		MyDistributionGroups,
		// Token: 0x04002163 RID: 8547
		[Description("MyDistributionGroupMembership")]
		MyDistributionGroupMembership,
		// Token: 0x04002164 RID: 8548
		[Obsolete("Role type deprecated. Use instead: Unified Messaging, UM Mailboxes, UM Prompts.")]
		[Description("UmManagement")]
		UmManagement,
		// Token: 0x04002165 RID: 8549
		[Description("RecordsManagement")]
		[Obsolete("Role type deprecated. Use instead: Retention Management, Journaling, Transport Rules, Message Tracking.")]
		RecordsManagement,
		// Token: 0x04002166 RID: 8550
		[Description("MyBaseOptions")]
		MyBaseOptions,
		// Token: 0x04002167 RID: 8551
		[Description("UmRecipientManagement")]
		[Obsolete("Role type deprecated. Use instead: Unified Messaging, UM Mailboxes, UM Prompts.")]
		UmRecipientManagement,
		// Token: 0x04002168 RID: 8552
		[Description("HelpdeskRecipientManagement")]
		HelpdeskRecipientManagement,
		// Token: 0x04002169 RID: 8553
		[Description("GALSynchronizationManagement")]
		GALSynchronizationManagement,
		// Token: 0x0400216A RID: 8554
		[Description("ApplicationImpersonation")]
		ApplicationImpersonation,
		// Token: 0x0400216B RID: 8555
		[Obsolete("Role type deprecated. Use instead: Unified Messaging, UM Mailboxes, UM Prompts.")]
		[Description("UMPromptManagement")]
		UMPromptManagement,
		// Token: 0x0400216C RID: 8556
		[Description("PartnerDelegatedTenantManagement")]
		PartnerDelegatedTenantManagement,
		// Token: 0x0400216D RID: 8557
		[Obsolete("Role type deprecated. Use instead: Mailbox Search, Legal Hold.")]
		[Description("DiscoveryManagement")]
		DiscoveryManagement,
		// Token: 0x0400216E RID: 8558
		[Description("CentralAdminManagement")]
		CentralAdminManagement,
		// Token: 0x0400216F RID: 8559
		[Description("UnScopedRoleManagement")]
		UnScopedRoleManagement,
		// Token: 0x04002170 RID: 8560
		[Description("MyContactInformation")]
		MyContactInformation,
		// Token: 0x04002171 RID: 8561
		[Description("MyProfileInformation")]
		MyProfileInformation,
		// Token: 0x04002172 RID: 8562
		[Description("MyVoiceMail")]
		MyVoiceMail,
		// Token: 0x04002173 RID: 8563
		[Description("MyTextMessaging")]
		MyTextMessaging,
		// Token: 0x04002174 RID: 8564
		[Description("MyMailSubscriptions")]
		MyMailSubscriptions,
		// Token: 0x04002175 RID: 8565
		[Description("MyRetentionPolicies")]
		MyRetentionPolicies,
		// Token: 0x04002176 RID: 8566
		[Obsolete("Role type deprecated. Use instead: MyBaseOptions, MyContactInformation, MyProfileInformation, MyVoiceMail, MyTextMessaging, MyMailSubscriptions or MyRetentionPolicies.")]
		[Description("MyOptions")]
		MyOptions = 2,
		// Token: 0x04002177 RID: 8567
		[Description("Mail Recipients")]
		MailRecipients = 26,
		// Token: 0x04002178 RID: 8568
		[Description("Federated Sharing")]
		FederatedSharing,
		// Token: 0x04002179 RID: 8569
		[Description("Database Availability Groups")]
		DatabaseAvailabilityGroups,
		// Token: 0x0400217A RID: 8570
		[Description("Databases")]
		Databases,
		// Token: 0x0400217B RID: 8571
		[Description("Public Folders")]
		PublicFolders,
		// Token: 0x0400217C RID: 8572
		[Description("Address Lists")]
		AddressLists,
		// Token: 0x0400217D RID: 8573
		[Description("Recipient Policies")]
		RecipientPolicies,
		// Token: 0x0400217E RID: 8574
		[Description("Disaster Recovery")]
		DisasterRecovery,
		// Token: 0x0400217F RID: 8575
		[Description("Monitoring")]
		Monitoring,
		// Token: 0x04002180 RID: 8576
		[Description("Database Copies")]
		DatabaseCopies,
		// Token: 0x04002181 RID: 8577
		[Description("Unified Messaging")]
		UnifiedMessaging,
		// Token: 0x04002182 RID: 8578
		[Description("Journaling")]
		Journaling,
		// Token: 0x04002183 RID: 8579
		[Description("Remote and Accepted Domains")]
		RemoteAndAcceptedDomains,
		// Token: 0x04002184 RID: 8580
		[Description("E-Mail Address Policies")]
		EmailAddressPolicies,
		// Token: 0x04002185 RID: 8581
		[Description("Transport Rules")]
		TransportRules,
		// Token: 0x04002186 RID: 8582
		[Description("Send Connectors")]
		SendConnectors,
		// Token: 0x04002187 RID: 8583
		[Description("Edge Subscriptions")]
		EdgeSubscriptions,
		// Token: 0x04002188 RID: 8584
		[Description("Organization Transport Settings")]
		OrganizationTransportSettings,
		// Token: 0x04002189 RID: 8585
		[Description("Exchange Servers")]
		ExchangeServers,
		// Token: 0x0400218A RID: 8586
		[Description("Exchange Virtual Directories")]
		ExchangeVirtualDirectories,
		// Token: 0x0400218B RID: 8587
		[Description("Exchange Server Certificates")]
		ExchangeServerCertificates,
		// Token: 0x0400218C RID: 8588
		[Description("POP3 And IMAP4 Protocols")]
		POP3AndIMAP4Protocols,
		// Token: 0x0400218D RID: 8589
		[Description("Receive Connectors")]
		ReceiveConnectors,
		// Token: 0x0400218E RID: 8590
		[Description("UM Mailboxes")]
		UMMailboxes,
		// Token: 0x0400218F RID: 8591
		[Description("User Options")]
		UserOptions,
		// Token: 0x04002190 RID: 8592
		[Description("Security Group Creation and Membership")]
		SecurityGroupCreationAndMembership,
		// Token: 0x04002191 RID: 8593
		[Description("Mail Recipient Creation")]
		MailRecipientCreation,
		// Token: 0x04002192 RID: 8594
		[Description("Message Tracking")]
		MessageTracking,
		// Token: 0x04002193 RID: 8595
		[Description("Role Management")]
		RoleManagement,
		// Token: 0x04002194 RID: 8596
		[Description("View-Only Recipients")]
		ViewOnlyRecipients,
		// Token: 0x04002195 RID: 8597
		[Description("View-Only Configuration")]
		ViewOnlyConfiguration,
		// Token: 0x04002196 RID: 8598
		[Description("Distribution Groups")]
		DistributionGroups,
		// Token: 0x04002197 RID: 8599
		[Description("Mail Enabled Public Folders")]
		MailEnabledPublicFolders,
		// Token: 0x04002198 RID: 8600
		[Description("Move Mailboxes")]
		MoveMailboxes,
		// Token: 0x04002199 RID: 8601
		[Description("WorkloadManagement")]
		WorkloadManagement,
		// Token: 0x0400219A RID: 8602
		[Description("Reset Password")]
		ResetPassword,
		// Token: 0x0400219B RID: 8603
		[Description("Audit Logs")]
		AuditLogs,
		// Token: 0x0400219C RID: 8604
		[Description("Retention Management")]
		RetentionManagement,
		// Token: 0x0400219D RID: 8605
		[Description("Support Diagnostics")]
		SupportDiagnostics,
		// Token: 0x0400219E RID: 8606
		[Description("Mailbox Search")]
		MailboxSearch,
		// Token: 0x0400219F RID: 8607
		[Description("Legal Hold")]
		LegalHold,
		// Token: 0x040021A0 RID: 8608
		[Description("Mail Tips")]
		MailTips,
		// Token: 0x040021A1 RID: 8609
		[Obsolete("Role type deprecated. Use instead: Mail Enabled Public Folders,Public Folders")]
		[Description("Public Folder Replication")]
		PublicFolderReplication,
		// Token: 0x040021A2 RID: 8610
		[Description("Active Directory Permissions")]
		ActiveDirectoryPermissions,
		// Token: 0x040021A3 RID: 8611
		[Description("UM Prompts")]
		UMPrompts,
		// Token: 0x040021A4 RID: 8612
		[Description("Migration")]
		Migration,
		// Token: 0x040021A5 RID: 8613
		[Description("DataCenter Operations")]
		DataCenterOperations,
		// Token: 0x040021A6 RID: 8614
		[Description("Transport Hygiene")]
		TransportHygiene,
		// Token: 0x040021A7 RID: 8615
		[Description("Transport Queues")]
		TransportQueues,
		// Token: 0x040021A8 RID: 8616
		[Description("Supervision")]
		Supervision,
		// Token: 0x040021A9 RID: 8617
		[Description("Cmdlet Extension Agents")]
		CmdletExtensionAgents,
		// Token: 0x040021AA RID: 8618
		[Description("Organization Configuration")]
		OrganizationConfiguration,
		// Token: 0x040021AB RID: 8619
		[Description("Organization Client Access")]
		OrganizationClientAccess,
		// Token: 0x040021AC RID: 8620
		[Description("Exchange Connectors")]
		ExchangeConnectors,
		// Token: 0x040021AD RID: 8621
		[Description("Mailbox Import Export")]
		MailboxImportExport,
		// Token: 0x040021AE RID: 8622
		[Description("View-Only Central Admin Management")]
		ViewOnlyCentralAdminManagement,
		// Token: 0x040021AF RID: 8623
		[Description("View-Only Central Admin Support")]
		ViewOnlyCentralAdminSupport,
		// Token: 0x040021B0 RID: 8624
		[Description("View-Only Role Management")]
		ViewOnlyRoleManagement,
		// Token: 0x040021B1 RID: 8625
		[Description("Reporting")]
		Reporting,
		// Token: 0x040021B2 RID: 8626
		[Description("View-Only Audit Logs")]
		ViewOnlyAuditLogs,
		// Token: 0x040021B3 RID: 8627
		[Description("Transport Agents")]
		TransportAgents,
		// Token: 0x040021B4 RID: 8628
		[Description("DataCenter Destructive Operations")]
		DataCenterDestructiveOperations,
		// Token: 0x040021B5 RID: 8629
		[Description("Information Rights Management")]
		InformationRightsManagement,
		// Token: 0x040021B6 RID: 8630
		[Description("Law Enforcement Requests")]
		LawEnforcementRequests,
		// Token: 0x040021B7 RID: 8631
		[Description("MyDiagnostics")]
		MyDiagnostics,
		// Token: 0x040021B8 RID: 8632
		[Description("MyMailboxDelegation")]
		MyMailboxDelegation,
		// Token: 0x040021B9 RID: 8633
		[Description("TeamMailboxes")]
		TeamMailboxes,
		// Token: 0x040021BA RID: 8634
		[Description("MyTeamMailboxes")]
		MyTeamMailboxes,
		// Token: 0x040021BB RID: 8635
		[Description("ActiveMonitoring")]
		ActiveMonitoring,
		// Token: 0x040021BC RID: 8636
		[Description("DataLossPrevention")]
		DataLossPrevention,
		// Token: 0x040021BD RID: 8637
		[Obsolete("Availability of Facebook feature is governed by OWA mailbox policy.")]
		[Description("MyFacebookEnabled")]
		MyFacebookEnabled,
		// Token: 0x040021BE RID: 8638
		[Obsolete("Availability of LinkedIn feature is governed by OWA mailbox policy.")]
		[Description("MyLinkedInEnabled")]
		MyLinkedInEnabled,
		// Token: 0x040021BF RID: 8639
		[Description("UserApplication")]
		UserApplication = 99,
		// Token: 0x040021C0 RID: 8640
		[Description("ArchiveApplication")]
		ArchiveApplication,
		// Token: 0x040021C1 RID: 8641
		[Description("LegalHoldApplication")]
		LegalHoldApplication,
		// Token: 0x040021C2 RID: 8642
		[Description("OfficeExtensionApplication")]
		OfficeExtensionApplication,
		// Token: 0x040021C3 RID: 8643
		[Description("TeamMailboxLifecycleApplication")]
		TeamMailboxLifecycleApplication,
		// Token: 0x040021C4 RID: 8644
		[Description("CentralAdminCredentialManagement")]
		CentralAdminCredentialManagement,
		// Token: 0x040021C5 RID: 8645
		[Description("PersonallyIdentifiableInformation")]
		PersonallyIdentifiableInformation,
		// Token: 0x040021C6 RID: 8646
		[Description("MailboxSearchApplication")]
		MailboxSearchApplication,
		// Token: 0x040021C7 RID: 8647
		[Description("MyMarketplaceApps")]
		MyMarketplaceApps,
		// Token: 0x040021C8 RID: 8648
		[Description("MyCustomApps")]
		MyCustomApps,
		// Token: 0x040021C9 RID: 8649
		[Description("OrgMarketplaceApps")]
		OrgMarketplaceApps,
		// Token: 0x040021CA RID: 8650
		[Description("OrgCustomApps")]
		OrgCustomApps,
		// Token: 0x040021CB RID: 8651
		[Description("ExchangeCrossServiceIntegration")]
		ExchangeCrossServiceIntegration,
		// Token: 0x040021CC RID: 8652
		[Description("NetworkingManagement")]
		NetworkingManagement,
		// Token: 0x040021CD RID: 8653
		[Description("Access To Customer Data - DC Only")]
		AccessToCustomerDataDCOnly,
		// Token: 0x040021CE RID: 8654
		[Description("Datacenter Operations - DC Only")]
		DatacenterOperationsDCOnly,
		// Token: 0x040021CF RID: 8655
		[Description("My ReadWriteMailbox Apps")]
		MyReadWriteMailboxApps
	}
}
