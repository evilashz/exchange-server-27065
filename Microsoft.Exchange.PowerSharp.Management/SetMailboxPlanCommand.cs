using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C95 RID: 3221
	public class SetMailboxPlanCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxPlan>
	{
		// Token: 0x0600A1DA RID: 41434 RVA: 0x000EA75E File Offset: 0x000E895E
		private SetMailboxPlanCommand() : base("Set-MailboxPlan")
		{
		}

		// Token: 0x0600A1DB RID: 41435 RVA: 0x000EA76B File Offset: 0x000E896B
		public SetMailboxPlanCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600A1DC RID: 41436 RVA: 0x000EA77A File Offset: 0x000E897A
		public virtual SetMailboxPlanCommand SetParameters(SetMailboxPlanCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A1DD RID: 41437 RVA: 0x000EA784 File Offset: 0x000E8984
		public virtual SetMailboxPlanCommand SetParameters(SetMailboxPlanCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C96 RID: 3222
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17007469 RID: 29801
			// (set) Token: 0x0600A1DE RID: 41438 RVA: 0x000EA78E File Offset: 0x000E898E
			public virtual SwitchParameter IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x1700746A RID: 29802
			// (set) Token: 0x0600A1DF RID: 41439 RVA: 0x000EA7A6 File Offset: 0x000E89A6
			public virtual SwitchParameter IsDefaultForPreviousVersion
			{
				set
				{
					base.PowerSharpParameters["IsDefaultForPreviousVersion"] = value;
				}
			}

			// Token: 0x1700746B RID: 29803
			// (set) Token: 0x0600A1E0 RID: 41440 RVA: 0x000EA7BE File Offset: 0x000E89BE
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x1700746C RID: 29804
			// (set) Token: 0x0600A1E1 RID: 41441 RVA: 0x000EA7D1 File Offset: 0x000E89D1
			public virtual MailboxPlanRelease MailboxPlanRelease
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanRelease"] = value;
				}
			}

			// Token: 0x1700746D RID: 29805
			// (set) Token: 0x0600A1E2 RID: 41442 RVA: 0x000EA7E9 File Offset: 0x000E89E9
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700746E RID: 29806
			// (set) Token: 0x0600A1E3 RID: 41443 RVA: 0x000EA801 File Offset: 0x000E8A01
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700746F RID: 29807
			// (set) Token: 0x0600A1E4 RID: 41444 RVA: 0x000EA81F File Offset: 0x000E8A1F
			public virtual OfflineAddressBookIdParameter OfflineAddressBook
			{
				set
				{
					base.PowerSharpParameters["OfflineAddressBook"] = value;
				}
			}

			// Token: 0x17007470 RID: 29808
			// (set) Token: 0x0600A1E5 RID: 41445 RVA: 0x000EA832 File Offset: 0x000E8A32
			public virtual SwitchParameter ApplyMandatoryProperties
			{
				set
				{
					base.PowerSharpParameters["ApplyMandatoryProperties"] = value;
				}
			}

			// Token: 0x17007471 RID: 29809
			// (set) Token: 0x0600A1E6 RID: 41446 RVA: 0x000EA84A File Offset: 0x000E8A4A
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007472 RID: 29810
			// (set) Token: 0x0600A1E7 RID: 41447 RVA: 0x000EA868 File Offset: 0x000E8A68
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007473 RID: 29811
			// (set) Token: 0x0600A1E8 RID: 41448 RVA: 0x000EA886 File Offset: 0x000E8A86
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007474 RID: 29812
			// (set) Token: 0x0600A1E9 RID: 41449 RVA: 0x000EA899 File Offset: 0x000E8A99
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007475 RID: 29813
			// (set) Token: 0x0600A1EA RID: 41450 RVA: 0x000EA8B7 File Offset: 0x000E8AB7
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17007476 RID: 29814
			// (set) Token: 0x0600A1EB RID: 41451 RVA: 0x000EA8CF File Offset: 0x000E8ACF
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17007477 RID: 29815
			// (set) Token: 0x0600A1EC RID: 41452 RVA: 0x000EA8E7 File Offset: 0x000E8AE7
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17007478 RID: 29816
			// (set) Token: 0x0600A1ED RID: 41453 RVA: 0x000EA8FA File Offset: 0x000E8AFA
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17007479 RID: 29817
			// (set) Token: 0x0600A1EE RID: 41454 RVA: 0x000EA912 File Offset: 0x000E8B12
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700747A RID: 29818
			// (set) Token: 0x0600A1EF RID: 41455 RVA: 0x000EA925 File Offset: 0x000E8B25
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x1700747B RID: 29819
			// (set) Token: 0x0600A1F0 RID: 41456 RVA: 0x000EA93D File Offset: 0x000E8B3D
			public virtual bool UseDatabaseRetentionDefaults
			{
				set
				{
					base.PowerSharpParameters["UseDatabaseRetentionDefaults"] = value;
				}
			}

			// Token: 0x1700747C RID: 29820
			// (set) Token: 0x0600A1F1 RID: 41457 RVA: 0x000EA955 File Offset: 0x000E8B55
			public virtual bool RetainDeletedItemsUntilBackup
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsUntilBackup"] = value;
				}
			}

			// Token: 0x1700747D RID: 29821
			// (set) Token: 0x0600A1F2 RID: 41458 RVA: 0x000EA96D File Offset: 0x000E8B6D
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x1700747E RID: 29822
			// (set) Token: 0x0600A1F3 RID: 41459 RVA: 0x000EA985 File Offset: 0x000E8B85
			public virtual bool IsExcludedFromServingHierarchy
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromServingHierarchy"] = value;
				}
			}

			// Token: 0x1700747F RID: 29823
			// (set) Token: 0x0600A1F4 RID: 41460 RVA: 0x000EA99D File Offset: 0x000E8B9D
			public virtual bool IsHierarchyReady
			{
				set
				{
					base.PowerSharpParameters["IsHierarchyReady"] = value;
				}
			}

			// Token: 0x17007480 RID: 29824
			// (set) Token: 0x0600A1F5 RID: 41461 RVA: 0x000EA9B5 File Offset: 0x000E8BB5
			public virtual bool LitigationHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldEnabled"] = value;
				}
			}

			// Token: 0x17007481 RID: 29825
			// (set) Token: 0x0600A1F6 RID: 41462 RVA: 0x000EA9CD File Offset: 0x000E8BCD
			public virtual bool SingleItemRecoveryEnabled
			{
				set
				{
					base.PowerSharpParameters["SingleItemRecoveryEnabled"] = value;
				}
			}

			// Token: 0x17007482 RID: 29826
			// (set) Token: 0x0600A1F7 RID: 41463 RVA: 0x000EA9E5 File Offset: 0x000E8BE5
			public virtual bool RetentionHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["RetentionHoldEnabled"] = value;
				}
			}

			// Token: 0x17007483 RID: 29827
			// (set) Token: 0x0600A1F8 RID: 41464 RVA: 0x000EA9FD File Offset: 0x000E8BFD
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17007484 RID: 29828
			// (set) Token: 0x0600A1F9 RID: 41465 RVA: 0x000EAA15 File Offset: 0x000E8C15
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17007485 RID: 29829
			// (set) Token: 0x0600A1FA RID: 41466 RVA: 0x000EAA2D File Offset: 0x000E8C2D
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x17007486 RID: 29830
			// (set) Token: 0x0600A1FB RID: 41467 RVA: 0x000EAA40 File Offset: 0x000E8C40
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x17007487 RID: 29831
			// (set) Token: 0x0600A1FC RID: 41468 RVA: 0x000EAA53 File Offset: 0x000E8C53
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x17007488 RID: 29832
			// (set) Token: 0x0600A1FD RID: 41469 RVA: 0x000EAA6B File Offset: 0x000E8C6B
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x17007489 RID: 29833
			// (set) Token: 0x0600A1FE RID: 41470 RVA: 0x000EAA7E File Offset: 0x000E8C7E
			public virtual bool CalendarRepairDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairDisabled"] = value;
				}
			}

			// Token: 0x1700748A RID: 29834
			// (set) Token: 0x0600A1FF RID: 41471 RVA: 0x000EAA96 File Offset: 0x000E8C96
			public virtual bool MessageTrackingReadStatusEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingReadStatusEnabled"] = value;
				}
			}

			// Token: 0x1700748B RID: 29835
			// (set) Token: 0x0600A200 RID: 41472 RVA: 0x000EAAAE File Offset: 0x000E8CAE
			public virtual ExternalOofOptions ExternalOofOptions
			{
				set
				{
					base.PowerSharpParameters["ExternalOofOptions"] = value;
				}
			}

			// Token: 0x1700748C RID: 29836
			// (set) Token: 0x0600A201 RID: 41473 RVA: 0x000EAAC6 File Offset: 0x000E8CC6
			public virtual EnhancedTimeSpan RetainDeletedItemsFor
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsFor"] = value;
				}
			}

			// Token: 0x1700748D RID: 29837
			// (set) Token: 0x0600A202 RID: 41474 RVA: 0x000EAADE File Offset: 0x000E8CDE
			public virtual Unlimited<ByteQuantifiedSize> ProhibitSendQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendQuota"] = value;
				}
			}

			// Token: 0x1700748E RID: 29838
			// (set) Token: 0x0600A203 RID: 41475 RVA: 0x000EAAF6 File Offset: 0x000E8CF6
			public virtual Unlimited<ByteQuantifiedSize> ProhibitSendReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendReceiveQuota"] = value;
				}
			}

			// Token: 0x1700748F RID: 29839
			// (set) Token: 0x0600A204 RID: 41476 RVA: 0x000EAB0E File Offset: 0x000E8D0E
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsQuota"] = value;
				}
			}

			// Token: 0x17007490 RID: 29840
			// (set) Token: 0x0600A205 RID: 41477 RVA: 0x000EAB26 File Offset: 0x000E8D26
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsWarningQuota"] = value;
				}
			}

			// Token: 0x17007491 RID: 29841
			// (set) Token: 0x0600A206 RID: 41478 RVA: 0x000EAB3E File Offset: 0x000E8D3E
			public virtual Unlimited<ByteQuantifiedSize> CalendarLoggingQuota
			{
				set
				{
					base.PowerSharpParameters["CalendarLoggingQuota"] = value;
				}
			}

			// Token: 0x17007492 RID: 29842
			// (set) Token: 0x0600A207 RID: 41479 RVA: 0x000EAB56 File Offset: 0x000E8D56
			public virtual bool DowngradeHighPriorityMessagesEnabled
			{
				set
				{
					base.PowerSharpParameters["DowngradeHighPriorityMessagesEnabled"] = value;
				}
			}

			// Token: 0x17007493 RID: 29843
			// (set) Token: 0x0600A208 RID: 41480 RVA: 0x000EAB6E File Offset: 0x000E8D6E
			public virtual Unlimited<int> RecipientLimits
			{
				set
				{
					base.PowerSharpParameters["RecipientLimits"] = value;
				}
			}

			// Token: 0x17007494 RID: 29844
			// (set) Token: 0x0600A209 RID: 41481 RVA: 0x000EAB86 File Offset: 0x000E8D86
			public virtual bool ImListMigrationCompleted
			{
				set
				{
					base.PowerSharpParameters["ImListMigrationCompleted"] = value;
				}
			}

			// Token: 0x17007495 RID: 29845
			// (set) Token: 0x0600A20A RID: 41482 RVA: 0x000EAB9E File Offset: 0x000E8D9E
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007496 RID: 29846
			// (set) Token: 0x0600A20B RID: 41483 RVA: 0x000EABB6 File Offset: 0x000E8DB6
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x17007497 RID: 29847
			// (set) Token: 0x0600A20C RID: 41484 RVA: 0x000EABCE File Offset: 0x000E8DCE
			public virtual bool? SCLDeleteEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteEnabled"] = value;
				}
			}

			// Token: 0x17007498 RID: 29848
			// (set) Token: 0x0600A20D RID: 41485 RVA: 0x000EABE6 File Offset: 0x000E8DE6
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x17007499 RID: 29849
			// (set) Token: 0x0600A20E RID: 41486 RVA: 0x000EABFE File Offset: 0x000E8DFE
			public virtual bool? SCLRejectEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLRejectEnabled"] = value;
				}
			}

			// Token: 0x1700749A RID: 29850
			// (set) Token: 0x0600A20F RID: 41487 RVA: 0x000EAC16 File Offset: 0x000E8E16
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x1700749B RID: 29851
			// (set) Token: 0x0600A210 RID: 41488 RVA: 0x000EAC2E File Offset: 0x000E8E2E
			public virtual bool? SCLQuarantineEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineEnabled"] = value;
				}
			}

			// Token: 0x1700749C RID: 29852
			// (set) Token: 0x0600A211 RID: 41489 RVA: 0x000EAC46 File Offset: 0x000E8E46
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x1700749D RID: 29853
			// (set) Token: 0x0600A212 RID: 41490 RVA: 0x000EAC5E File Offset: 0x000E8E5E
			public virtual bool? SCLJunkEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLJunkEnabled"] = value;
				}
			}

			// Token: 0x1700749E RID: 29854
			// (set) Token: 0x0600A213 RID: 41491 RVA: 0x000EAC76 File Offset: 0x000E8E76
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x1700749F RID: 29855
			// (set) Token: 0x0600A214 RID: 41492 RVA: 0x000EAC8E File Offset: 0x000E8E8E
			public virtual bool? UseDatabaseQuotaDefaults
			{
				set
				{
					base.PowerSharpParameters["UseDatabaseQuotaDefaults"] = value;
				}
			}

			// Token: 0x170074A0 RID: 29856
			// (set) Token: 0x0600A215 RID: 41493 RVA: 0x000EACA6 File Offset: 0x000E8EA6
			public virtual Unlimited<ByteQuantifiedSize> IssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["IssueWarningQuota"] = value;
				}
			}

			// Token: 0x170074A1 RID: 29857
			// (set) Token: 0x0600A216 RID: 41494 RVA: 0x000EACBE File Offset: 0x000E8EBE
			public virtual ByteQuantifiedSize RulesQuota
			{
				set
				{
					base.PowerSharpParameters["RulesQuota"] = value;
				}
			}

			// Token: 0x170074A2 RID: 29858
			// (set) Token: 0x0600A217 RID: 41495 RVA: 0x000EACD6 File Offset: 0x000E8ED6
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x170074A3 RID: 29859
			// (set) Token: 0x0600A218 RID: 41496 RVA: 0x000EACE9 File Offset: 0x000E8EE9
			public virtual int? MaxSafeSenders
			{
				set
				{
					base.PowerSharpParameters["MaxSafeSenders"] = value;
				}
			}

			// Token: 0x170074A4 RID: 29860
			// (set) Token: 0x0600A219 RID: 41497 RVA: 0x000EAD01 File Offset: 0x000E8F01
			public virtual int? MaxBlockedSenders
			{
				set
				{
					base.PowerSharpParameters["MaxBlockedSenders"] = value;
				}
			}

			// Token: 0x170074A5 RID: 29861
			// (set) Token: 0x0600A21A RID: 41498 RVA: 0x000EAD19 File Offset: 0x000E8F19
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x170074A6 RID: 29862
			// (set) Token: 0x0600A21B RID: 41499 RVA: 0x000EAD2C File Offset: 0x000E8F2C
			public virtual SmtpAddress JournalArchiveAddress
			{
				set
				{
					base.PowerSharpParameters["JournalArchiveAddress"] = value;
				}
			}

			// Token: 0x170074A7 RID: 29863
			// (set) Token: 0x0600A21C RID: 41500 RVA: 0x000EAD44 File Offset: 0x000E8F44
			public virtual Unlimited<ByteQuantifiedSize> ArchiveQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveQuota"] = value;
				}
			}

			// Token: 0x170074A8 RID: 29864
			// (set) Token: 0x0600A21D RID: 41501 RVA: 0x000EAD5C File Offset: 0x000E8F5C
			public virtual Unlimited<ByteQuantifiedSize> ArchiveWarningQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveWarningQuota"] = value;
				}
			}

			// Token: 0x170074A9 RID: 29865
			// (set) Token: 0x0600A21E RID: 41502 RVA: 0x000EAD74 File Offset: 0x000E8F74
			public virtual SmtpDomain ArchiveDomain
			{
				set
				{
					base.PowerSharpParameters["ArchiveDomain"] = value;
				}
			}

			// Token: 0x170074AA RID: 29866
			// (set) Token: 0x0600A21F RID: 41503 RVA: 0x000EAD87 File Offset: 0x000E8F87
			public virtual ArchiveStatusFlags ArchiveStatus
			{
				set
				{
					base.PowerSharpParameters["ArchiveStatus"] = value;
				}
			}

			// Token: 0x170074AB RID: 29867
			// (set) Token: 0x0600A220 RID: 41504 RVA: 0x000EAD9F File Offset: 0x000E8F9F
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x170074AC RID: 29868
			// (set) Token: 0x0600A221 RID: 41505 RVA: 0x000EADB7 File Offset: 0x000E8FB7
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x170074AD RID: 29869
			// (set) Token: 0x0600A222 RID: 41506 RVA: 0x000EADCF File Offset: 0x000E8FCF
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x170074AE RID: 29870
			// (set) Token: 0x0600A223 RID: 41507 RVA: 0x000EADE2 File Offset: 0x000E8FE2
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x170074AF RID: 29871
			// (set) Token: 0x0600A224 RID: 41508 RVA: 0x000EADF5 File Offset: 0x000E8FF5
			public virtual bool CalendarVersionStoreDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarVersionStoreDisabled"] = value;
				}
			}

			// Token: 0x170074B0 RID: 29872
			// (set) Token: 0x0600A225 RID: 41509 RVA: 0x000EAE0D File Offset: 0x000E900D
			public virtual bool AuditEnabled
			{
				set
				{
					base.PowerSharpParameters["AuditEnabled"] = value;
				}
			}

			// Token: 0x170074B1 RID: 29873
			// (set) Token: 0x0600A226 RID: 41510 RVA: 0x000EAE25 File Offset: 0x000E9025
			public virtual EnhancedTimeSpan AuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["AuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x170074B2 RID: 29874
			// (set) Token: 0x0600A227 RID: 41511 RVA: 0x000EAE3D File Offset: 0x000E903D
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditAdmin
			{
				set
				{
					base.PowerSharpParameters["AuditAdmin"] = value;
				}
			}

			// Token: 0x170074B3 RID: 29875
			// (set) Token: 0x0600A228 RID: 41512 RVA: 0x000EAE50 File Offset: 0x000E9050
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditDelegate
			{
				set
				{
					base.PowerSharpParameters["AuditDelegate"] = value;
				}
			}

			// Token: 0x170074B4 RID: 29876
			// (set) Token: 0x0600A229 RID: 41513 RVA: 0x000EAE63 File Offset: 0x000E9063
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditOwner
			{
				set
				{
					base.PowerSharpParameters["AuditOwner"] = value;
				}
			}

			// Token: 0x170074B5 RID: 29877
			// (set) Token: 0x0600A22A RID: 41514 RVA: 0x000EAE76 File Offset: 0x000E9076
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170074B6 RID: 29878
			// (set) Token: 0x0600A22B RID: 41515 RVA: 0x000EAE89 File Offset: 0x000E9089
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x170074B7 RID: 29879
			// (set) Token: 0x0600A22C RID: 41516 RVA: 0x000EAE9C File Offset: 0x000E909C
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170074B8 RID: 29880
			// (set) Token: 0x0600A22D RID: 41517 RVA: 0x000EAEAF File Offset: 0x000E90AF
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x170074B9 RID: 29881
			// (set) Token: 0x0600A22E RID: 41518 RVA: 0x000EAEC7 File Offset: 0x000E90C7
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x170074BA RID: 29882
			// (set) Token: 0x0600A22F RID: 41519 RVA: 0x000EAEDF File Offset: 0x000E90DF
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x170074BB RID: 29883
			// (set) Token: 0x0600A230 RID: 41520 RVA: 0x000EAEF7 File Offset: 0x000E90F7
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x170074BC RID: 29884
			// (set) Token: 0x0600A231 RID: 41521 RVA: 0x000EAF0F File Offset: 0x000E910F
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170074BD RID: 29885
			// (set) Token: 0x0600A232 RID: 41522 RVA: 0x000EAF22 File Offset: 0x000E9122
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170074BE RID: 29886
			// (set) Token: 0x0600A233 RID: 41523 RVA: 0x000EAF3A File Offset: 0x000E913A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170074BF RID: 29887
			// (set) Token: 0x0600A234 RID: 41524 RVA: 0x000EAF52 File Offset: 0x000E9152
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170074C0 RID: 29888
			// (set) Token: 0x0600A235 RID: 41525 RVA: 0x000EAF6A File Offset: 0x000E916A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170074C1 RID: 29889
			// (set) Token: 0x0600A236 RID: 41526 RVA: 0x000EAF82 File Offset: 0x000E9182
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C97 RID: 3223
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170074C2 RID: 29890
			// (set) Token: 0x0600A238 RID: 41528 RVA: 0x000EAFA2 File Offset: 0x000E91A2
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170074C3 RID: 29891
			// (set) Token: 0x0600A239 RID: 41529 RVA: 0x000EAFC0 File Offset: 0x000E91C0
			public virtual SwitchParameter IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x170074C4 RID: 29892
			// (set) Token: 0x0600A23A RID: 41530 RVA: 0x000EAFD8 File Offset: 0x000E91D8
			public virtual SwitchParameter IsDefaultForPreviousVersion
			{
				set
				{
					base.PowerSharpParameters["IsDefaultForPreviousVersion"] = value;
				}
			}

			// Token: 0x170074C5 RID: 29893
			// (set) Token: 0x0600A23B RID: 41531 RVA: 0x000EAFF0 File Offset: 0x000E91F0
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170074C6 RID: 29894
			// (set) Token: 0x0600A23C RID: 41532 RVA: 0x000EB003 File Offset: 0x000E9203
			public virtual MailboxPlanRelease MailboxPlanRelease
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanRelease"] = value;
				}
			}

			// Token: 0x170074C7 RID: 29895
			// (set) Token: 0x0600A23D RID: 41533 RVA: 0x000EB01B File Offset: 0x000E921B
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170074C8 RID: 29896
			// (set) Token: 0x0600A23E RID: 41534 RVA: 0x000EB033 File Offset: 0x000E9233
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170074C9 RID: 29897
			// (set) Token: 0x0600A23F RID: 41535 RVA: 0x000EB051 File Offset: 0x000E9251
			public virtual OfflineAddressBookIdParameter OfflineAddressBook
			{
				set
				{
					base.PowerSharpParameters["OfflineAddressBook"] = value;
				}
			}

			// Token: 0x170074CA RID: 29898
			// (set) Token: 0x0600A240 RID: 41536 RVA: 0x000EB064 File Offset: 0x000E9264
			public virtual SwitchParameter ApplyMandatoryProperties
			{
				set
				{
					base.PowerSharpParameters["ApplyMandatoryProperties"] = value;
				}
			}

			// Token: 0x170074CB RID: 29899
			// (set) Token: 0x0600A241 RID: 41537 RVA: 0x000EB07C File Offset: 0x000E927C
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170074CC RID: 29900
			// (set) Token: 0x0600A242 RID: 41538 RVA: 0x000EB09A File Offset: 0x000E929A
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170074CD RID: 29901
			// (set) Token: 0x0600A243 RID: 41539 RVA: 0x000EB0B8 File Offset: 0x000E92B8
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x170074CE RID: 29902
			// (set) Token: 0x0600A244 RID: 41540 RVA: 0x000EB0CB File Offset: 0x000E92CB
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170074CF RID: 29903
			// (set) Token: 0x0600A245 RID: 41541 RVA: 0x000EB0E9 File Offset: 0x000E92E9
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x170074D0 RID: 29904
			// (set) Token: 0x0600A246 RID: 41542 RVA: 0x000EB101 File Offset: 0x000E9301
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x170074D1 RID: 29905
			// (set) Token: 0x0600A247 RID: 41543 RVA: 0x000EB119 File Offset: 0x000E9319
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x170074D2 RID: 29906
			// (set) Token: 0x0600A248 RID: 41544 RVA: 0x000EB12C File Offset: 0x000E932C
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170074D3 RID: 29907
			// (set) Token: 0x0600A249 RID: 41545 RVA: 0x000EB144 File Offset: 0x000E9344
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170074D4 RID: 29908
			// (set) Token: 0x0600A24A RID: 41546 RVA: 0x000EB157 File Offset: 0x000E9357
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x170074D5 RID: 29909
			// (set) Token: 0x0600A24B RID: 41547 RVA: 0x000EB16F File Offset: 0x000E936F
			public virtual bool UseDatabaseRetentionDefaults
			{
				set
				{
					base.PowerSharpParameters["UseDatabaseRetentionDefaults"] = value;
				}
			}

			// Token: 0x170074D6 RID: 29910
			// (set) Token: 0x0600A24C RID: 41548 RVA: 0x000EB187 File Offset: 0x000E9387
			public virtual bool RetainDeletedItemsUntilBackup
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsUntilBackup"] = value;
				}
			}

			// Token: 0x170074D7 RID: 29911
			// (set) Token: 0x0600A24D RID: 41549 RVA: 0x000EB19F File Offset: 0x000E939F
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x170074D8 RID: 29912
			// (set) Token: 0x0600A24E RID: 41550 RVA: 0x000EB1B7 File Offset: 0x000E93B7
			public virtual bool IsExcludedFromServingHierarchy
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromServingHierarchy"] = value;
				}
			}

			// Token: 0x170074D9 RID: 29913
			// (set) Token: 0x0600A24F RID: 41551 RVA: 0x000EB1CF File Offset: 0x000E93CF
			public virtual bool IsHierarchyReady
			{
				set
				{
					base.PowerSharpParameters["IsHierarchyReady"] = value;
				}
			}

			// Token: 0x170074DA RID: 29914
			// (set) Token: 0x0600A250 RID: 41552 RVA: 0x000EB1E7 File Offset: 0x000E93E7
			public virtual bool LitigationHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldEnabled"] = value;
				}
			}

			// Token: 0x170074DB RID: 29915
			// (set) Token: 0x0600A251 RID: 41553 RVA: 0x000EB1FF File Offset: 0x000E93FF
			public virtual bool SingleItemRecoveryEnabled
			{
				set
				{
					base.PowerSharpParameters["SingleItemRecoveryEnabled"] = value;
				}
			}

			// Token: 0x170074DC RID: 29916
			// (set) Token: 0x0600A252 RID: 41554 RVA: 0x000EB217 File Offset: 0x000E9417
			public virtual bool RetentionHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["RetentionHoldEnabled"] = value;
				}
			}

			// Token: 0x170074DD RID: 29917
			// (set) Token: 0x0600A253 RID: 41555 RVA: 0x000EB22F File Offset: 0x000E942F
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x170074DE RID: 29918
			// (set) Token: 0x0600A254 RID: 41556 RVA: 0x000EB247 File Offset: 0x000E9447
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x170074DF RID: 29919
			// (set) Token: 0x0600A255 RID: 41557 RVA: 0x000EB25F File Offset: 0x000E945F
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x170074E0 RID: 29920
			// (set) Token: 0x0600A256 RID: 41558 RVA: 0x000EB272 File Offset: 0x000E9472
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x170074E1 RID: 29921
			// (set) Token: 0x0600A257 RID: 41559 RVA: 0x000EB285 File Offset: 0x000E9485
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x170074E2 RID: 29922
			// (set) Token: 0x0600A258 RID: 41560 RVA: 0x000EB29D File Offset: 0x000E949D
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x170074E3 RID: 29923
			// (set) Token: 0x0600A259 RID: 41561 RVA: 0x000EB2B0 File Offset: 0x000E94B0
			public virtual bool CalendarRepairDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairDisabled"] = value;
				}
			}

			// Token: 0x170074E4 RID: 29924
			// (set) Token: 0x0600A25A RID: 41562 RVA: 0x000EB2C8 File Offset: 0x000E94C8
			public virtual bool MessageTrackingReadStatusEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingReadStatusEnabled"] = value;
				}
			}

			// Token: 0x170074E5 RID: 29925
			// (set) Token: 0x0600A25B RID: 41563 RVA: 0x000EB2E0 File Offset: 0x000E94E0
			public virtual ExternalOofOptions ExternalOofOptions
			{
				set
				{
					base.PowerSharpParameters["ExternalOofOptions"] = value;
				}
			}

			// Token: 0x170074E6 RID: 29926
			// (set) Token: 0x0600A25C RID: 41564 RVA: 0x000EB2F8 File Offset: 0x000E94F8
			public virtual EnhancedTimeSpan RetainDeletedItemsFor
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsFor"] = value;
				}
			}

			// Token: 0x170074E7 RID: 29927
			// (set) Token: 0x0600A25D RID: 41565 RVA: 0x000EB310 File Offset: 0x000E9510
			public virtual Unlimited<ByteQuantifiedSize> ProhibitSendQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendQuota"] = value;
				}
			}

			// Token: 0x170074E8 RID: 29928
			// (set) Token: 0x0600A25E RID: 41566 RVA: 0x000EB328 File Offset: 0x000E9528
			public virtual Unlimited<ByteQuantifiedSize> ProhibitSendReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendReceiveQuota"] = value;
				}
			}

			// Token: 0x170074E9 RID: 29929
			// (set) Token: 0x0600A25F RID: 41567 RVA: 0x000EB340 File Offset: 0x000E9540
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsQuota"] = value;
				}
			}

			// Token: 0x170074EA RID: 29930
			// (set) Token: 0x0600A260 RID: 41568 RVA: 0x000EB358 File Offset: 0x000E9558
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsWarningQuota"] = value;
				}
			}

			// Token: 0x170074EB RID: 29931
			// (set) Token: 0x0600A261 RID: 41569 RVA: 0x000EB370 File Offset: 0x000E9570
			public virtual Unlimited<ByteQuantifiedSize> CalendarLoggingQuota
			{
				set
				{
					base.PowerSharpParameters["CalendarLoggingQuota"] = value;
				}
			}

			// Token: 0x170074EC RID: 29932
			// (set) Token: 0x0600A262 RID: 41570 RVA: 0x000EB388 File Offset: 0x000E9588
			public virtual bool DowngradeHighPriorityMessagesEnabled
			{
				set
				{
					base.PowerSharpParameters["DowngradeHighPriorityMessagesEnabled"] = value;
				}
			}

			// Token: 0x170074ED RID: 29933
			// (set) Token: 0x0600A263 RID: 41571 RVA: 0x000EB3A0 File Offset: 0x000E95A0
			public virtual Unlimited<int> RecipientLimits
			{
				set
				{
					base.PowerSharpParameters["RecipientLimits"] = value;
				}
			}

			// Token: 0x170074EE RID: 29934
			// (set) Token: 0x0600A264 RID: 41572 RVA: 0x000EB3B8 File Offset: 0x000E95B8
			public virtual bool ImListMigrationCompleted
			{
				set
				{
					base.PowerSharpParameters["ImListMigrationCompleted"] = value;
				}
			}

			// Token: 0x170074EF RID: 29935
			// (set) Token: 0x0600A265 RID: 41573 RVA: 0x000EB3D0 File Offset: 0x000E95D0
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x170074F0 RID: 29936
			// (set) Token: 0x0600A266 RID: 41574 RVA: 0x000EB3E8 File Offset: 0x000E95E8
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x170074F1 RID: 29937
			// (set) Token: 0x0600A267 RID: 41575 RVA: 0x000EB400 File Offset: 0x000E9600
			public virtual bool? SCLDeleteEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteEnabled"] = value;
				}
			}

			// Token: 0x170074F2 RID: 29938
			// (set) Token: 0x0600A268 RID: 41576 RVA: 0x000EB418 File Offset: 0x000E9618
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x170074F3 RID: 29939
			// (set) Token: 0x0600A269 RID: 41577 RVA: 0x000EB430 File Offset: 0x000E9630
			public virtual bool? SCLRejectEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLRejectEnabled"] = value;
				}
			}

			// Token: 0x170074F4 RID: 29940
			// (set) Token: 0x0600A26A RID: 41578 RVA: 0x000EB448 File Offset: 0x000E9648
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x170074F5 RID: 29941
			// (set) Token: 0x0600A26B RID: 41579 RVA: 0x000EB460 File Offset: 0x000E9660
			public virtual bool? SCLQuarantineEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineEnabled"] = value;
				}
			}

			// Token: 0x170074F6 RID: 29942
			// (set) Token: 0x0600A26C RID: 41580 RVA: 0x000EB478 File Offset: 0x000E9678
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x170074F7 RID: 29943
			// (set) Token: 0x0600A26D RID: 41581 RVA: 0x000EB490 File Offset: 0x000E9690
			public virtual bool? SCLJunkEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLJunkEnabled"] = value;
				}
			}

			// Token: 0x170074F8 RID: 29944
			// (set) Token: 0x0600A26E RID: 41582 RVA: 0x000EB4A8 File Offset: 0x000E96A8
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x170074F9 RID: 29945
			// (set) Token: 0x0600A26F RID: 41583 RVA: 0x000EB4C0 File Offset: 0x000E96C0
			public virtual bool? UseDatabaseQuotaDefaults
			{
				set
				{
					base.PowerSharpParameters["UseDatabaseQuotaDefaults"] = value;
				}
			}

			// Token: 0x170074FA RID: 29946
			// (set) Token: 0x0600A270 RID: 41584 RVA: 0x000EB4D8 File Offset: 0x000E96D8
			public virtual Unlimited<ByteQuantifiedSize> IssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["IssueWarningQuota"] = value;
				}
			}

			// Token: 0x170074FB RID: 29947
			// (set) Token: 0x0600A271 RID: 41585 RVA: 0x000EB4F0 File Offset: 0x000E96F0
			public virtual ByteQuantifiedSize RulesQuota
			{
				set
				{
					base.PowerSharpParameters["RulesQuota"] = value;
				}
			}

			// Token: 0x170074FC RID: 29948
			// (set) Token: 0x0600A272 RID: 41586 RVA: 0x000EB508 File Offset: 0x000E9708
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x170074FD RID: 29949
			// (set) Token: 0x0600A273 RID: 41587 RVA: 0x000EB51B File Offset: 0x000E971B
			public virtual int? MaxSafeSenders
			{
				set
				{
					base.PowerSharpParameters["MaxSafeSenders"] = value;
				}
			}

			// Token: 0x170074FE RID: 29950
			// (set) Token: 0x0600A274 RID: 41588 RVA: 0x000EB533 File Offset: 0x000E9733
			public virtual int? MaxBlockedSenders
			{
				set
				{
					base.PowerSharpParameters["MaxBlockedSenders"] = value;
				}
			}

			// Token: 0x170074FF RID: 29951
			// (set) Token: 0x0600A275 RID: 41589 RVA: 0x000EB54B File Offset: 0x000E974B
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17007500 RID: 29952
			// (set) Token: 0x0600A276 RID: 41590 RVA: 0x000EB55E File Offset: 0x000E975E
			public virtual SmtpAddress JournalArchiveAddress
			{
				set
				{
					base.PowerSharpParameters["JournalArchiveAddress"] = value;
				}
			}

			// Token: 0x17007501 RID: 29953
			// (set) Token: 0x0600A277 RID: 41591 RVA: 0x000EB576 File Offset: 0x000E9776
			public virtual Unlimited<ByteQuantifiedSize> ArchiveQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveQuota"] = value;
				}
			}

			// Token: 0x17007502 RID: 29954
			// (set) Token: 0x0600A278 RID: 41592 RVA: 0x000EB58E File Offset: 0x000E978E
			public virtual Unlimited<ByteQuantifiedSize> ArchiveWarningQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveWarningQuota"] = value;
				}
			}

			// Token: 0x17007503 RID: 29955
			// (set) Token: 0x0600A279 RID: 41593 RVA: 0x000EB5A6 File Offset: 0x000E97A6
			public virtual SmtpDomain ArchiveDomain
			{
				set
				{
					base.PowerSharpParameters["ArchiveDomain"] = value;
				}
			}

			// Token: 0x17007504 RID: 29956
			// (set) Token: 0x0600A27A RID: 41594 RVA: 0x000EB5B9 File Offset: 0x000E97B9
			public virtual ArchiveStatusFlags ArchiveStatus
			{
				set
				{
					base.PowerSharpParameters["ArchiveStatus"] = value;
				}
			}

			// Token: 0x17007505 RID: 29957
			// (set) Token: 0x0600A27B RID: 41595 RVA: 0x000EB5D1 File Offset: 0x000E97D1
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17007506 RID: 29958
			// (set) Token: 0x0600A27C RID: 41596 RVA: 0x000EB5E9 File Offset: 0x000E97E9
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007507 RID: 29959
			// (set) Token: 0x0600A27D RID: 41597 RVA: 0x000EB601 File Offset: 0x000E9801
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17007508 RID: 29960
			// (set) Token: 0x0600A27E RID: 41598 RVA: 0x000EB614 File Offset: 0x000E9814
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17007509 RID: 29961
			// (set) Token: 0x0600A27F RID: 41599 RVA: 0x000EB627 File Offset: 0x000E9827
			public virtual bool CalendarVersionStoreDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarVersionStoreDisabled"] = value;
				}
			}

			// Token: 0x1700750A RID: 29962
			// (set) Token: 0x0600A280 RID: 41600 RVA: 0x000EB63F File Offset: 0x000E983F
			public virtual bool AuditEnabled
			{
				set
				{
					base.PowerSharpParameters["AuditEnabled"] = value;
				}
			}

			// Token: 0x1700750B RID: 29963
			// (set) Token: 0x0600A281 RID: 41601 RVA: 0x000EB657 File Offset: 0x000E9857
			public virtual EnhancedTimeSpan AuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["AuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x1700750C RID: 29964
			// (set) Token: 0x0600A282 RID: 41602 RVA: 0x000EB66F File Offset: 0x000E986F
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditAdmin
			{
				set
				{
					base.PowerSharpParameters["AuditAdmin"] = value;
				}
			}

			// Token: 0x1700750D RID: 29965
			// (set) Token: 0x0600A283 RID: 41603 RVA: 0x000EB682 File Offset: 0x000E9882
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditDelegate
			{
				set
				{
					base.PowerSharpParameters["AuditDelegate"] = value;
				}
			}

			// Token: 0x1700750E RID: 29966
			// (set) Token: 0x0600A284 RID: 41604 RVA: 0x000EB695 File Offset: 0x000E9895
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditOwner
			{
				set
				{
					base.PowerSharpParameters["AuditOwner"] = value;
				}
			}

			// Token: 0x1700750F RID: 29967
			// (set) Token: 0x0600A285 RID: 41605 RVA: 0x000EB6A8 File Offset: 0x000E98A8
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007510 RID: 29968
			// (set) Token: 0x0600A286 RID: 41606 RVA: 0x000EB6BB File Offset: 0x000E98BB
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17007511 RID: 29969
			// (set) Token: 0x0600A287 RID: 41607 RVA: 0x000EB6CE File Offset: 0x000E98CE
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007512 RID: 29970
			// (set) Token: 0x0600A288 RID: 41608 RVA: 0x000EB6E1 File Offset: 0x000E98E1
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x17007513 RID: 29971
			// (set) Token: 0x0600A289 RID: 41609 RVA: 0x000EB6F9 File Offset: 0x000E98F9
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x17007514 RID: 29972
			// (set) Token: 0x0600A28A RID: 41610 RVA: 0x000EB711 File Offset: 0x000E9911
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x17007515 RID: 29973
			// (set) Token: 0x0600A28B RID: 41611 RVA: 0x000EB729 File Offset: 0x000E9929
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17007516 RID: 29974
			// (set) Token: 0x0600A28C RID: 41612 RVA: 0x000EB741 File Offset: 0x000E9941
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007517 RID: 29975
			// (set) Token: 0x0600A28D RID: 41613 RVA: 0x000EB754 File Offset: 0x000E9954
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007518 RID: 29976
			// (set) Token: 0x0600A28E RID: 41614 RVA: 0x000EB76C File Offset: 0x000E996C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007519 RID: 29977
			// (set) Token: 0x0600A28F RID: 41615 RVA: 0x000EB784 File Offset: 0x000E9984
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700751A RID: 29978
			// (set) Token: 0x0600A290 RID: 41616 RVA: 0x000EB79C File Offset: 0x000E999C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700751B RID: 29979
			// (set) Token: 0x0600A291 RID: 41617 RVA: 0x000EB7B4 File Offset: 0x000E99B4
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
