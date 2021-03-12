using System;
using System.Globalization;
using System.Management.Automation;
using System.Security;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D9D RID: 3485
	public class SetSyncMailboxCommand : SyntheticCommandWithPipelineInputNoOutput<SyncMailbox>
	{
		// Token: 0x0600C6B7 RID: 50871 RVA: 0x0011BFE1 File Offset: 0x0011A1E1
		private SetSyncMailboxCommand() : base("Set-SyncMailbox")
		{
		}

		// Token: 0x0600C6B8 RID: 50872 RVA: 0x0011BFEE File Offset: 0x0011A1EE
		public SetSyncMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600C6B9 RID: 50873 RVA: 0x0011BFFD File Offset: 0x0011A1FD
		public virtual SetSyncMailboxCommand SetParameters(SetSyncMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600C6BA RID: 50874 RVA: 0x0011C007 File Offset: 0x0011A207
		public virtual SetSyncMailboxCommand SetParameters(SetSyncMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D9E RID: 3486
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17009736 RID: 38710
			// (set) Token: 0x0600C6BB RID: 50875 RVA: 0x0011C011 File Offset: 0x0011A211
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17009737 RID: 38711
			// (set) Token: 0x0600C6BC RID: 50876 RVA: 0x0011C029 File Offset: 0x0011A229
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17009738 RID: 38712
			// (set) Token: 0x0600C6BD RID: 50877 RVA: 0x0011C047 File Offset: 0x0011A247
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17009739 RID: 38713
			// (set) Token: 0x0600C6BE RID: 50878 RVA: 0x0011C065 File Offset: 0x0011A265
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x1700973A RID: 38714
			// (set) Token: 0x0600C6BF RID: 50879 RVA: 0x0011C078 File Offset: 0x0011A278
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x1700973B RID: 38715
			// (set) Token: 0x0600C6C0 RID: 50880 RVA: 0x0011C08B File Offset: 0x0011A28B
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawAcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["RawAcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x1700973C RID: 38716
			// (set) Token: 0x0600C6C1 RID: 50881 RVA: 0x0011C09E File Offset: 0x0011A29E
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawBypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["RawBypassModerationFrom"] = value;
				}
			}

			// Token: 0x1700973D RID: 38717
			// (set) Token: 0x0600C6C2 RID: 50882 RVA: 0x0011C0B1 File Offset: 0x0011A2B1
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawRejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RawRejectMessagesFrom"] = value;
				}
			}

			// Token: 0x1700973E RID: 38718
			// (set) Token: 0x0600C6C3 RID: 50883 RVA: 0x0011C0C4 File Offset: 0x0011A2C4
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> RawGrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["RawGrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x1700973F RID: 38719
			// (set) Token: 0x0600C6C4 RID: 50884 RVA: 0x0011C0D7 File Offset: 0x0011A2D7
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<ModeratorIDParameter>> RawModeratedBy
			{
				set
				{
					base.PowerSharpParameters["RawModeratedBy"] = value;
				}
			}

			// Token: 0x17009740 RID: 38720
			// (set) Token: 0x0600C6C5 RID: 50885 RVA: 0x0011C0EA File Offset: 0x0011A2EA
			public virtual RecipientWithAdUserIdParameter<RecipientIdParameter> RawForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["RawForwardingAddress"] = value;
				}
			}

			// Token: 0x17009741 RID: 38721
			// (set) Token: 0x0600C6C6 RID: 50886 RVA: 0x0011C0FD File Offset: 0x0011A2FD
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17009742 RID: 38722
			// (set) Token: 0x0600C6C7 RID: 50887 RVA: 0x0011C110 File Offset: 0x0011A310
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17009743 RID: 38723
			// (set) Token: 0x0600C6C8 RID: 50888 RVA: 0x0011C123 File Offset: 0x0011A323
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17009744 RID: 38724
			// (set) Token: 0x0600C6C9 RID: 50889 RVA: 0x0011C13B File Offset: 0x0011A33B
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17009745 RID: 38725
			// (set) Token: 0x0600C6CA RID: 50890 RVA: 0x0011C153 File Offset: 0x0011A353
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17009746 RID: 38726
			// (set) Token: 0x0600C6CB RID: 50891 RVA: 0x0011C166 File Offset: 0x0011A366
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17009747 RID: 38727
			// (set) Token: 0x0600C6CC RID: 50892 RVA: 0x0011C179 File Offset: 0x0011A379
			public virtual SwitchParameter SoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x17009748 RID: 38728
			// (set) Token: 0x0600C6CD RID: 50893 RVA: 0x0011C191 File Offset: 0x0011A391
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17009749 RID: 38729
			// (set) Token: 0x0600C6CE RID: 50894 RVA: 0x0011C1A9 File Offset: 0x0011A3A9
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x1700974A RID: 38730
			// (set) Token: 0x0600C6CF RID: 50895 RVA: 0x0011C1C1 File Offset: 0x0011A3C1
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x1700974B RID: 38731
			// (set) Token: 0x0600C6D0 RID: 50896 RVA: 0x0011C1D9 File Offset: 0x0011A3D9
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700974C RID: 38732
			// (set) Token: 0x0600C6D1 RID: 50897 RVA: 0x0011C1F7 File Offset: 0x0011A3F7
			public virtual string ForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700974D RID: 38733
			// (set) Token: 0x0600C6D2 RID: 50898 RVA: 0x0011C215 File Offset: 0x0011A415
			public virtual OfflineAddressBookIdParameter OfflineAddressBook
			{
				set
				{
					base.PowerSharpParameters["OfflineAddressBook"] = value;
				}
			}

			// Token: 0x1700974E RID: 38734
			// (set) Token: 0x0600C6D3 RID: 50899 RVA: 0x0011C228 File Offset: 0x0011A428
			public virtual string LinkedMasterAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedMasterAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x1700974F RID: 38735
			// (set) Token: 0x0600C6D4 RID: 50900 RVA: 0x0011C246 File Offset: 0x0011A446
			public virtual string LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x17009750 RID: 38736
			// (set) Token: 0x0600C6D5 RID: 50901 RVA: 0x0011C259 File Offset: 0x0011A459
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x17009751 RID: 38737
			// (set) Token: 0x0600C6D6 RID: 50902 RVA: 0x0011C26C File Offset: 0x0011A46C
			public virtual ConvertibleMailboxSubType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x17009752 RID: 38738
			// (set) Token: 0x0600C6D7 RID: 50903 RVA: 0x0011C284 File Offset: 0x0011A484
			public virtual SwitchParameter ApplyMandatoryProperties
			{
				set
				{
					base.PowerSharpParameters["ApplyMandatoryProperties"] = value;
				}
			}

			// Token: 0x17009753 RID: 38739
			// (set) Token: 0x0600C6D8 RID: 50904 RVA: 0x0011C29C File Offset: 0x0011A49C
			public virtual SwitchParameter RemoveManagedFolderAndPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoveManagedFolderAndPolicy"] = value;
				}
			}

			// Token: 0x17009754 RID: 38740
			// (set) Token: 0x0600C6D9 RID: 50905 RVA: 0x0011C2B4 File Offset: 0x0011A4B4
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009755 RID: 38741
			// (set) Token: 0x0600C6DA RID: 50906 RVA: 0x0011C2D2 File Offset: 0x0011A4D2
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009756 RID: 38742
			// (set) Token: 0x0600C6DB RID: 50907 RVA: 0x0011C2F0 File Offset: 0x0011A4F0
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17009757 RID: 38743
			// (set) Token: 0x0600C6DC RID: 50908 RVA: 0x0011C303 File Offset: 0x0011A503
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009758 RID: 38744
			// (set) Token: 0x0600C6DD RID: 50909 RVA: 0x0011C321 File Offset: 0x0011A521
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17009759 RID: 38745
			// (set) Token: 0x0600C6DE RID: 50910 RVA: 0x0011C339 File Offset: 0x0011A539
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x1700975A RID: 38746
			// (set) Token: 0x0600C6DF RID: 50911 RVA: 0x0011C351 File Offset: 0x0011A551
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x1700975B RID: 38747
			// (set) Token: 0x0600C6E0 RID: 50912 RVA: 0x0011C364 File Offset: 0x0011A564
			public virtual SwitchParameter BypassLiveId
			{
				set
				{
					base.PowerSharpParameters["BypassLiveId"] = value;
				}
			}

			// Token: 0x1700975C RID: 38748
			// (set) Token: 0x0600C6E1 RID: 50913 RVA: 0x0011C37C File Offset: 0x0011A57C
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x1700975D RID: 38749
			// (set) Token: 0x0600C6E2 RID: 50914 RVA: 0x0011C38F File Offset: 0x0011A58F
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700975E RID: 38750
			// (set) Token: 0x0600C6E3 RID: 50915 RVA: 0x0011C3A2 File Offset: 0x0011A5A2
			public virtual string FederatedIdentity
			{
				set
				{
					base.PowerSharpParameters["FederatedIdentity"] = value;
				}
			}

			// Token: 0x1700975F RID: 38751
			// (set) Token: 0x0600C6E4 RID: 50916 RVA: 0x0011C3B5 File Offset: 0x0011A5B5
			public virtual string SecondaryAddress
			{
				set
				{
					base.PowerSharpParameters["SecondaryAddress"] = value;
				}
			}

			// Token: 0x17009760 RID: 38752
			// (set) Token: 0x0600C6E5 RID: 50917 RVA: 0x0011C3C8 File Offset: 0x0011A5C8
			public virtual string SecondaryDialPlan
			{
				set
				{
					base.PowerSharpParameters["SecondaryDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17009761 RID: 38753
			// (set) Token: 0x0600C6E6 RID: 50918 RVA: 0x0011C3E6 File Offset: 0x0011A5E6
			public virtual SwitchParameter RemovePicture
			{
				set
				{
					base.PowerSharpParameters["RemovePicture"] = value;
				}
			}

			// Token: 0x17009762 RID: 38754
			// (set) Token: 0x0600C6E7 RID: 50919 RVA: 0x0011C3FE File Offset: 0x0011A5FE
			public virtual SwitchParameter RemoveSpokenName
			{
				set
				{
					base.PowerSharpParameters["RemoveSpokenName"] = value;
				}
			}

			// Token: 0x17009763 RID: 38755
			// (set) Token: 0x0600C6E8 RID: 50920 RVA: 0x0011C416 File Offset: 0x0011A616
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17009764 RID: 38756
			// (set) Token: 0x0600C6E9 RID: 50921 RVA: 0x0011C429 File Offset: 0x0011A629
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17009765 RID: 38757
			// (set) Token: 0x0600C6EA RID: 50922 RVA: 0x0011C43C File Offset: 0x0011A63C
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17009766 RID: 38758
			// (set) Token: 0x0600C6EB RID: 50923 RVA: 0x0011C44F File Offset: 0x0011A64F
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17009767 RID: 38759
			// (set) Token: 0x0600C6EC RID: 50924 RVA: 0x0011C46D File Offset: 0x0011A66D
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17009768 RID: 38760
			// (set) Token: 0x0600C6ED RID: 50925 RVA: 0x0011C480 File Offset: 0x0011A680
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17009769 RID: 38761
			// (set) Token: 0x0600C6EE RID: 50926 RVA: 0x0011C493 File Offset: 0x0011A693
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x1700976A RID: 38762
			// (set) Token: 0x0600C6EF RID: 50927 RVA: 0x0011C4A6 File Offset: 0x0011A6A6
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x1700976B RID: 38763
			// (set) Token: 0x0600C6F0 RID: 50928 RVA: 0x0011C4B9 File Offset: 0x0011A6B9
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x1700976C RID: 38764
			// (set) Token: 0x0600C6F1 RID: 50929 RVA: 0x0011C4CC File Offset: 0x0011A6CC
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x1700976D RID: 38765
			// (set) Token: 0x0600C6F2 RID: 50930 RVA: 0x0011C4DF File Offset: 0x0011A6DF
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x1700976E RID: 38766
			// (set) Token: 0x0600C6F3 RID: 50931 RVA: 0x0011C4F7 File Offset: 0x0011A6F7
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700976F RID: 38767
			// (set) Token: 0x0600C6F4 RID: 50932 RVA: 0x0011C50F File Offset: 0x0011A70F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009770 RID: 38768
			// (set) Token: 0x0600C6F5 RID: 50933 RVA: 0x0011C522 File Offset: 0x0011A722
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17009771 RID: 38769
			// (set) Token: 0x0600C6F6 RID: 50934 RVA: 0x0011C535 File Offset: 0x0011A735
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17009772 RID: 38770
			// (set) Token: 0x0600C6F7 RID: 50935 RVA: 0x0011C54D File Offset: 0x0011A74D
			public virtual SecurityIdentifier MasterAccountSid
			{
				set
				{
					base.PowerSharpParameters["MasterAccountSid"] = value;
				}
			}

			// Token: 0x17009773 RID: 38771
			// (set) Token: 0x0600C6F8 RID: 50936 RVA: 0x0011C560 File Offset: 0x0011A760
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17009774 RID: 38772
			// (set) Token: 0x0600C6F9 RID: 50937 RVA: 0x0011C573 File Offset: 0x0011A773
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17009775 RID: 38773
			// (set) Token: 0x0600C6FA RID: 50938 RVA: 0x0011C58B File Offset: 0x0011A78B
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17009776 RID: 38774
			// (set) Token: 0x0600C6FB RID: 50939 RVA: 0x0011C5A3 File Offset: 0x0011A7A3
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17009777 RID: 38775
			// (set) Token: 0x0600C6FC RID: 50940 RVA: 0x0011C5BB File Offset: 0x0011A7BB
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x17009778 RID: 38776
			// (set) Token: 0x0600C6FD RID: 50941 RVA: 0x0011C5D3 File Offset: 0x0011A7D3
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17009779 RID: 38777
			// (set) Token: 0x0600C6FE RID: 50942 RVA: 0x0011C5E6 File Offset: 0x0011A7E6
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x1700977A RID: 38778
			// (set) Token: 0x0600C6FF RID: 50943 RVA: 0x0011C5F9 File Offset: 0x0011A7F9
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x1700977B RID: 38779
			// (set) Token: 0x0600C700 RID: 50944 RVA: 0x0011C60C File Offset: 0x0011A80C
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x1700977C RID: 38780
			// (set) Token: 0x0600C701 RID: 50945 RVA: 0x0011C61F File Offset: 0x0011A81F
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x1700977D RID: 38781
			// (set) Token: 0x0600C702 RID: 50946 RVA: 0x0011C637 File Offset: 0x0011A837
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x1700977E RID: 38782
			// (set) Token: 0x0600C703 RID: 50947 RVA: 0x0011C64A File Offset: 0x0011A84A
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x1700977F RID: 38783
			// (set) Token: 0x0600C704 RID: 50948 RVA: 0x0011C65D File Offset: 0x0011A85D
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17009780 RID: 38784
			// (set) Token: 0x0600C705 RID: 50949 RVA: 0x0011C670 File Offset: 0x0011A870
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17009781 RID: 38785
			// (set) Token: 0x0600C706 RID: 50950 RVA: 0x0011C683 File Offset: 0x0011A883
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009782 RID: 38786
			// (set) Token: 0x0600C707 RID: 50951 RVA: 0x0011C696 File Offset: 0x0011A896
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009783 RID: 38787
			// (set) Token: 0x0600C708 RID: 50952 RVA: 0x0011C6A9 File Offset: 0x0011A8A9
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17009784 RID: 38788
			// (set) Token: 0x0600C709 RID: 50953 RVA: 0x0011C6BC File Offset: 0x0011A8BC
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17009785 RID: 38789
			// (set) Token: 0x0600C70A RID: 50954 RVA: 0x0011C6CF File Offset: 0x0011A8CF
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17009786 RID: 38790
			// (set) Token: 0x0600C70B RID: 50955 RVA: 0x0011C6E2 File Offset: 0x0011A8E2
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17009787 RID: 38791
			// (set) Token: 0x0600C70C RID: 50956 RVA: 0x0011C6F5 File Offset: 0x0011A8F5
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17009788 RID: 38792
			// (set) Token: 0x0600C70D RID: 50957 RVA: 0x0011C708 File Offset: 0x0011A908
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17009789 RID: 38793
			// (set) Token: 0x0600C70E RID: 50958 RVA: 0x0011C71B File Offset: 0x0011A91B
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x1700978A RID: 38794
			// (set) Token: 0x0600C70F RID: 50959 RVA: 0x0011C72E File Offset: 0x0011A92E
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x1700978B RID: 38795
			// (set) Token: 0x0600C710 RID: 50960 RVA: 0x0011C741 File Offset: 0x0011A941
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x1700978C RID: 38796
			// (set) Token: 0x0600C711 RID: 50961 RVA: 0x0011C754 File Offset: 0x0011A954
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x1700978D RID: 38797
			// (set) Token: 0x0600C712 RID: 50962 RVA: 0x0011C767 File Offset: 0x0011A967
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x1700978E RID: 38798
			// (set) Token: 0x0600C713 RID: 50963 RVA: 0x0011C77A File Offset: 0x0011A97A
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x1700978F RID: 38799
			// (set) Token: 0x0600C714 RID: 50964 RVA: 0x0011C78D File Offset: 0x0011A98D
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17009790 RID: 38800
			// (set) Token: 0x0600C715 RID: 50965 RVA: 0x0011C7A5 File Offset: 0x0011A9A5
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17009791 RID: 38801
			// (set) Token: 0x0600C716 RID: 50966 RVA: 0x0011C7B8 File Offset: 0x0011A9B8
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17009792 RID: 38802
			// (set) Token: 0x0600C717 RID: 50967 RVA: 0x0011C7CB File Offset: 0x0011A9CB
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17009793 RID: 38803
			// (set) Token: 0x0600C718 RID: 50968 RVA: 0x0011C7E3 File Offset: 0x0011A9E3
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17009794 RID: 38804
			// (set) Token: 0x0600C719 RID: 50969 RVA: 0x0011C7F6 File Offset: 0x0011A9F6
			public virtual bool ExcludedFromBackSync
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromBackSync"] = value;
				}
			}

			// Token: 0x17009795 RID: 38805
			// (set) Token: 0x0600C71A RID: 50970 RVA: 0x0011C80E File Offset: 0x0011AA0E
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17009796 RID: 38806
			// (set) Token: 0x0600C71B RID: 50971 RVA: 0x0011C821 File Offset: 0x0011AA21
			public virtual bool LEOEnabled
			{
				set
				{
					base.PowerSharpParameters["LEOEnabled"] = value;
				}
			}

			// Token: 0x17009797 RID: 38807
			// (set) Token: 0x0600C71C RID: 50972 RVA: 0x0011C839 File Offset: 0x0011AA39
			public virtual bool AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17009798 RID: 38808
			// (set) Token: 0x0600C71D RID: 50973 RVA: 0x0011C851 File Offset: 0x0011AA51
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17009799 RID: 38809
			// (set) Token: 0x0600C71E RID: 50974 RVA: 0x0011C869 File Offset: 0x0011AA69
			public virtual bool UseDatabaseRetentionDefaults
			{
				set
				{
					base.PowerSharpParameters["UseDatabaseRetentionDefaults"] = value;
				}
			}

			// Token: 0x1700979A RID: 38810
			// (set) Token: 0x0600C71F RID: 50975 RVA: 0x0011C881 File Offset: 0x0011AA81
			public virtual bool RetainDeletedItemsUntilBackup
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsUntilBackup"] = value;
				}
			}

			// Token: 0x1700979B RID: 38811
			// (set) Token: 0x0600C720 RID: 50976 RVA: 0x0011C899 File Offset: 0x0011AA99
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x1700979C RID: 38812
			// (set) Token: 0x0600C721 RID: 50977 RVA: 0x0011C8B1 File Offset: 0x0011AAB1
			public virtual bool IsExcludedFromServingHierarchy
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromServingHierarchy"] = value;
				}
			}

			// Token: 0x1700979D RID: 38813
			// (set) Token: 0x0600C722 RID: 50978 RVA: 0x0011C8C9 File Offset: 0x0011AAC9
			public virtual bool IsHierarchyReady
			{
				set
				{
					base.PowerSharpParameters["IsHierarchyReady"] = value;
				}
			}

			// Token: 0x1700979E RID: 38814
			// (set) Token: 0x0600C723 RID: 50979 RVA: 0x0011C8E1 File Offset: 0x0011AAE1
			public virtual bool LitigationHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldEnabled"] = value;
				}
			}

			// Token: 0x1700979F RID: 38815
			// (set) Token: 0x0600C724 RID: 50980 RVA: 0x0011C8F9 File Offset: 0x0011AAF9
			public virtual bool SingleItemRecoveryEnabled
			{
				set
				{
					base.PowerSharpParameters["SingleItemRecoveryEnabled"] = value;
				}
			}

			// Token: 0x170097A0 RID: 38816
			// (set) Token: 0x0600C725 RID: 50981 RVA: 0x0011C911 File Offset: 0x0011AB11
			public virtual bool RetentionHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["RetentionHoldEnabled"] = value;
				}
			}

			// Token: 0x170097A1 RID: 38817
			// (set) Token: 0x0600C726 RID: 50982 RVA: 0x0011C929 File Offset: 0x0011AB29
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x170097A2 RID: 38818
			// (set) Token: 0x0600C727 RID: 50983 RVA: 0x0011C941 File Offset: 0x0011AB41
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x170097A3 RID: 38819
			// (set) Token: 0x0600C728 RID: 50984 RVA: 0x0011C959 File Offset: 0x0011AB59
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x170097A4 RID: 38820
			// (set) Token: 0x0600C729 RID: 50985 RVA: 0x0011C96C File Offset: 0x0011AB6C
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x170097A5 RID: 38821
			// (set) Token: 0x0600C72A RID: 50986 RVA: 0x0011C97F File Offset: 0x0011AB7F
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x170097A6 RID: 38822
			// (set) Token: 0x0600C72B RID: 50987 RVA: 0x0011C997 File Offset: 0x0011AB97
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x170097A7 RID: 38823
			// (set) Token: 0x0600C72C RID: 50988 RVA: 0x0011C9AA File Offset: 0x0011ABAA
			public virtual bool CalendarRepairDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairDisabled"] = value;
				}
			}

			// Token: 0x170097A8 RID: 38824
			// (set) Token: 0x0600C72D RID: 50989 RVA: 0x0011C9C2 File Offset: 0x0011ABC2
			public virtual bool MessageTrackingReadStatusEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingReadStatusEnabled"] = value;
				}
			}

			// Token: 0x170097A9 RID: 38825
			// (set) Token: 0x0600C72E RID: 50990 RVA: 0x0011C9DA File Offset: 0x0011ABDA
			public virtual ExternalOofOptions ExternalOofOptions
			{
				set
				{
					base.PowerSharpParameters["ExternalOofOptions"] = value;
				}
			}

			// Token: 0x170097AA RID: 38826
			// (set) Token: 0x0600C72F RID: 50991 RVA: 0x0011C9F2 File Offset: 0x0011ABF2
			public virtual ProxyAddress ForwardingSmtpAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingSmtpAddress"] = value;
				}
			}

			// Token: 0x170097AB RID: 38827
			// (set) Token: 0x0600C730 RID: 50992 RVA: 0x0011CA05 File Offset: 0x0011AC05
			public virtual EnhancedTimeSpan RetainDeletedItemsFor
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsFor"] = value;
				}
			}

			// Token: 0x170097AC RID: 38828
			// (set) Token: 0x0600C731 RID: 50993 RVA: 0x0011CA1D File Offset: 0x0011AC1D
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x170097AD RID: 38829
			// (set) Token: 0x0600C732 RID: 50994 RVA: 0x0011CA30 File Offset: 0x0011AC30
			public virtual Unlimited<ByteQuantifiedSize> ProhibitSendQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendQuota"] = value;
				}
			}

			// Token: 0x170097AE RID: 38830
			// (set) Token: 0x0600C733 RID: 50995 RVA: 0x0011CA48 File Offset: 0x0011AC48
			public virtual Unlimited<ByteQuantifiedSize> ProhibitSendReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendReceiveQuota"] = value;
				}
			}

			// Token: 0x170097AF RID: 38831
			// (set) Token: 0x0600C734 RID: 50996 RVA: 0x0011CA60 File Offset: 0x0011AC60
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsQuota"] = value;
				}
			}

			// Token: 0x170097B0 RID: 38832
			// (set) Token: 0x0600C735 RID: 50997 RVA: 0x0011CA78 File Offset: 0x0011AC78
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsWarningQuota"] = value;
				}
			}

			// Token: 0x170097B1 RID: 38833
			// (set) Token: 0x0600C736 RID: 50998 RVA: 0x0011CA90 File Offset: 0x0011AC90
			public virtual Unlimited<ByteQuantifiedSize> CalendarLoggingQuota
			{
				set
				{
					base.PowerSharpParameters["CalendarLoggingQuota"] = value;
				}
			}

			// Token: 0x170097B2 RID: 38834
			// (set) Token: 0x0600C737 RID: 50999 RVA: 0x0011CAA8 File Offset: 0x0011ACA8
			public virtual bool DowngradeHighPriorityMessagesEnabled
			{
				set
				{
					base.PowerSharpParameters["DowngradeHighPriorityMessagesEnabled"] = value;
				}
			}

			// Token: 0x170097B3 RID: 38835
			// (set) Token: 0x0600C738 RID: 51000 RVA: 0x0011CAC0 File Offset: 0x0011ACC0
			public virtual Unlimited<int> RecipientLimits
			{
				set
				{
					base.PowerSharpParameters["RecipientLimits"] = value;
				}
			}

			// Token: 0x170097B4 RID: 38836
			// (set) Token: 0x0600C739 RID: 51001 RVA: 0x0011CAD8 File Offset: 0x0011ACD8
			public virtual bool ImListMigrationCompleted
			{
				set
				{
					base.PowerSharpParameters["ImListMigrationCompleted"] = value;
				}
			}

			// Token: 0x170097B5 RID: 38837
			// (set) Token: 0x0600C73A RID: 51002 RVA: 0x0011CAF0 File Offset: 0x0011ACF0
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x170097B6 RID: 38838
			// (set) Token: 0x0600C73B RID: 51003 RVA: 0x0011CB08 File Offset: 0x0011AD08
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x170097B7 RID: 38839
			// (set) Token: 0x0600C73C RID: 51004 RVA: 0x0011CB20 File Offset: 0x0011AD20
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x170097B8 RID: 38840
			// (set) Token: 0x0600C73D RID: 51005 RVA: 0x0011CB33 File Offset: 0x0011AD33
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x170097B9 RID: 38841
			// (set) Token: 0x0600C73E RID: 51006 RVA: 0x0011CB46 File Offset: 0x0011AD46
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x170097BA RID: 38842
			// (set) Token: 0x0600C73F RID: 51007 RVA: 0x0011CB5E File Offset: 0x0011AD5E
			public virtual bool? SCLDeleteEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteEnabled"] = value;
				}
			}

			// Token: 0x170097BB RID: 38843
			// (set) Token: 0x0600C740 RID: 51008 RVA: 0x0011CB76 File Offset: 0x0011AD76
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x170097BC RID: 38844
			// (set) Token: 0x0600C741 RID: 51009 RVA: 0x0011CB8E File Offset: 0x0011AD8E
			public virtual bool? SCLRejectEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLRejectEnabled"] = value;
				}
			}

			// Token: 0x170097BD RID: 38845
			// (set) Token: 0x0600C742 RID: 51010 RVA: 0x0011CBA6 File Offset: 0x0011ADA6
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x170097BE RID: 38846
			// (set) Token: 0x0600C743 RID: 51011 RVA: 0x0011CBBE File Offset: 0x0011ADBE
			public virtual bool? SCLQuarantineEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineEnabled"] = value;
				}
			}

			// Token: 0x170097BF RID: 38847
			// (set) Token: 0x0600C744 RID: 51012 RVA: 0x0011CBD6 File Offset: 0x0011ADD6
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x170097C0 RID: 38848
			// (set) Token: 0x0600C745 RID: 51013 RVA: 0x0011CBEE File Offset: 0x0011ADEE
			public virtual bool? SCLJunkEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLJunkEnabled"] = value;
				}
			}

			// Token: 0x170097C1 RID: 38849
			// (set) Token: 0x0600C746 RID: 51014 RVA: 0x0011CC06 File Offset: 0x0011AE06
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x170097C2 RID: 38850
			// (set) Token: 0x0600C747 RID: 51015 RVA: 0x0011CC1E File Offset: 0x0011AE1E
			public virtual bool? UseDatabaseQuotaDefaults
			{
				set
				{
					base.PowerSharpParameters["UseDatabaseQuotaDefaults"] = value;
				}
			}

			// Token: 0x170097C3 RID: 38851
			// (set) Token: 0x0600C748 RID: 51016 RVA: 0x0011CC36 File Offset: 0x0011AE36
			public virtual Unlimited<ByteQuantifiedSize> IssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["IssueWarningQuota"] = value;
				}
			}

			// Token: 0x170097C4 RID: 38852
			// (set) Token: 0x0600C749 RID: 51017 RVA: 0x0011CC4E File Offset: 0x0011AE4E
			public virtual ByteQuantifiedSize RulesQuota
			{
				set
				{
					base.PowerSharpParameters["RulesQuota"] = value;
				}
			}

			// Token: 0x170097C5 RID: 38853
			// (set) Token: 0x0600C74A RID: 51018 RVA: 0x0011CC66 File Offset: 0x0011AE66
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x170097C6 RID: 38854
			// (set) Token: 0x0600C74B RID: 51019 RVA: 0x0011CC79 File Offset: 0x0011AE79
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x170097C7 RID: 38855
			// (set) Token: 0x0600C74C RID: 51020 RVA: 0x0011CC8C File Offset: 0x0011AE8C
			public virtual int? MaxSafeSenders
			{
				set
				{
					base.PowerSharpParameters["MaxSafeSenders"] = value;
				}
			}

			// Token: 0x170097C8 RID: 38856
			// (set) Token: 0x0600C74D RID: 51021 RVA: 0x0011CCA4 File Offset: 0x0011AEA4
			public virtual int? MaxBlockedSenders
			{
				set
				{
					base.PowerSharpParameters["MaxBlockedSenders"] = value;
				}
			}

			// Token: 0x170097C9 RID: 38857
			// (set) Token: 0x0600C74E RID: 51022 RVA: 0x0011CCBC File Offset: 0x0011AEBC
			public virtual SmtpAddress WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x170097CA RID: 38858
			// (set) Token: 0x0600C74F RID: 51023 RVA: 0x0011CCD4 File Offset: 0x0011AED4
			public virtual SmtpAddress MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x170097CB RID: 38859
			// (set) Token: 0x0600C750 RID: 51024 RVA: 0x0011CCEC File Offset: 0x0011AEEC
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x170097CC RID: 38860
			// (set) Token: 0x0600C751 RID: 51025 RVA: 0x0011CCFF File Offset: 0x0011AEFF
			public virtual SmtpAddress JournalArchiveAddress
			{
				set
				{
					base.PowerSharpParameters["JournalArchiveAddress"] = value;
				}
			}

			// Token: 0x170097CD RID: 38861
			// (set) Token: 0x0600C752 RID: 51026 RVA: 0x0011CD17 File Offset: 0x0011AF17
			public virtual Unlimited<ByteQuantifiedSize> ArchiveQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveQuota"] = value;
				}
			}

			// Token: 0x170097CE RID: 38862
			// (set) Token: 0x0600C753 RID: 51027 RVA: 0x0011CD2F File Offset: 0x0011AF2F
			public virtual Unlimited<ByteQuantifiedSize> ArchiveWarningQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveWarningQuota"] = value;
				}
			}

			// Token: 0x170097CF RID: 38863
			// (set) Token: 0x0600C754 RID: 51028 RVA: 0x0011CD47 File Offset: 0x0011AF47
			public virtual SmtpDomain ArchiveDomain
			{
				set
				{
					base.PowerSharpParameters["ArchiveDomain"] = value;
				}
			}

			// Token: 0x170097D0 RID: 38864
			// (set) Token: 0x0600C755 RID: 51029 RVA: 0x0011CD5A File Offset: 0x0011AF5A
			public virtual ArchiveStatusFlags ArchiveStatus
			{
				set
				{
					base.PowerSharpParameters["ArchiveStatus"] = value;
				}
			}

			// Token: 0x170097D1 RID: 38865
			// (set) Token: 0x0600C756 RID: 51030 RVA: 0x0011CD72 File Offset: 0x0011AF72
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x170097D2 RID: 38866
			// (set) Token: 0x0600C757 RID: 51031 RVA: 0x0011CD8A File Offset: 0x0011AF8A
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x170097D3 RID: 38867
			// (set) Token: 0x0600C758 RID: 51032 RVA: 0x0011CDA2 File Offset: 0x0011AFA2
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x170097D4 RID: 38868
			// (set) Token: 0x0600C759 RID: 51033 RVA: 0x0011CDB5 File Offset: 0x0011AFB5
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x170097D5 RID: 38869
			// (set) Token: 0x0600C75A RID: 51034 RVA: 0x0011CDC8 File Offset: 0x0011AFC8
			public virtual bool CalendarVersionStoreDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarVersionStoreDisabled"] = value;
				}
			}

			// Token: 0x170097D6 RID: 38870
			// (set) Token: 0x0600C75B RID: 51035 RVA: 0x0011CDE0 File Offset: 0x0011AFE0
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x170097D7 RID: 38871
			// (set) Token: 0x0600C75C RID: 51036 RVA: 0x0011CDF3 File Offset: 0x0011AFF3
			public virtual bool? SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x170097D8 RID: 38872
			// (set) Token: 0x0600C75D RID: 51037 RVA: 0x0011CE0B File Offset: 0x0011B00B
			public virtual bool AuditEnabled
			{
				set
				{
					base.PowerSharpParameters["AuditEnabled"] = value;
				}
			}

			// Token: 0x170097D9 RID: 38873
			// (set) Token: 0x0600C75E RID: 51038 RVA: 0x0011CE23 File Offset: 0x0011B023
			public virtual EnhancedTimeSpan AuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["AuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x170097DA RID: 38874
			// (set) Token: 0x0600C75F RID: 51039 RVA: 0x0011CE3B File Offset: 0x0011B03B
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditAdmin
			{
				set
				{
					base.PowerSharpParameters["AuditAdmin"] = value;
				}
			}

			// Token: 0x170097DB RID: 38875
			// (set) Token: 0x0600C760 RID: 51040 RVA: 0x0011CE4E File Offset: 0x0011B04E
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditDelegate
			{
				set
				{
					base.PowerSharpParameters["AuditDelegate"] = value;
				}
			}

			// Token: 0x170097DC RID: 38876
			// (set) Token: 0x0600C761 RID: 51041 RVA: 0x0011CE61 File Offset: 0x0011B061
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditOwner
			{
				set
				{
					base.PowerSharpParameters["AuditOwner"] = value;
				}
			}

			// Token: 0x170097DD RID: 38877
			// (set) Token: 0x0600C762 RID: 51042 RVA: 0x0011CE74 File Offset: 0x0011B074
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x170097DE RID: 38878
			// (set) Token: 0x0600C763 RID: 51043 RVA: 0x0011CE87 File Offset: 0x0011B087
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170097DF RID: 38879
			// (set) Token: 0x0600C764 RID: 51044 RVA: 0x0011CE9A File Offset: 0x0011B09A
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x170097E0 RID: 38880
			// (set) Token: 0x0600C765 RID: 51045 RVA: 0x0011CEAD File Offset: 0x0011B0AD
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x170097E1 RID: 38881
			// (set) Token: 0x0600C766 RID: 51046 RVA: 0x0011CEC0 File Offset: 0x0011B0C0
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x170097E2 RID: 38882
			// (set) Token: 0x0600C767 RID: 51047 RVA: 0x0011CED3 File Offset: 0x0011B0D3
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x170097E3 RID: 38883
			// (set) Token: 0x0600C768 RID: 51048 RVA: 0x0011CEE6 File Offset: 0x0011B0E6
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x170097E4 RID: 38884
			// (set) Token: 0x0600C769 RID: 51049 RVA: 0x0011CEF9 File Offset: 0x0011B0F9
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x170097E5 RID: 38885
			// (set) Token: 0x0600C76A RID: 51050 RVA: 0x0011CF0C File Offset: 0x0011B10C
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x170097E6 RID: 38886
			// (set) Token: 0x0600C76B RID: 51051 RVA: 0x0011CF1F File Offset: 0x0011B11F
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x170097E7 RID: 38887
			// (set) Token: 0x0600C76C RID: 51052 RVA: 0x0011CF32 File Offset: 0x0011B132
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x170097E8 RID: 38888
			// (set) Token: 0x0600C76D RID: 51053 RVA: 0x0011CF45 File Offset: 0x0011B145
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x170097E9 RID: 38889
			// (set) Token: 0x0600C76E RID: 51054 RVA: 0x0011CF58 File Offset: 0x0011B158
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x170097EA RID: 38890
			// (set) Token: 0x0600C76F RID: 51055 RVA: 0x0011CF6B File Offset: 0x0011B16B
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x170097EB RID: 38891
			// (set) Token: 0x0600C770 RID: 51056 RVA: 0x0011CF7E File Offset: 0x0011B17E
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x170097EC RID: 38892
			// (set) Token: 0x0600C771 RID: 51057 RVA: 0x0011CF91 File Offset: 0x0011B191
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x170097ED RID: 38893
			// (set) Token: 0x0600C772 RID: 51058 RVA: 0x0011CFA4 File Offset: 0x0011B1A4
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x170097EE RID: 38894
			// (set) Token: 0x0600C773 RID: 51059 RVA: 0x0011CFB7 File Offset: 0x0011B1B7
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x170097EF RID: 38895
			// (set) Token: 0x0600C774 RID: 51060 RVA: 0x0011CFCA File Offset: 0x0011B1CA
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x170097F0 RID: 38896
			// (set) Token: 0x0600C775 RID: 51061 RVA: 0x0011CFDD File Offset: 0x0011B1DD
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x170097F1 RID: 38897
			// (set) Token: 0x0600C776 RID: 51062 RVA: 0x0011CFF0 File Offset: 0x0011B1F0
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x170097F2 RID: 38898
			// (set) Token: 0x0600C777 RID: 51063 RVA: 0x0011D003 File Offset: 0x0011B203
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x170097F3 RID: 38899
			// (set) Token: 0x0600C778 RID: 51064 RVA: 0x0011D016 File Offset: 0x0011B216
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170097F4 RID: 38900
			// (set) Token: 0x0600C779 RID: 51065 RVA: 0x0011D029 File Offset: 0x0011B229
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x170097F5 RID: 38901
			// (set) Token: 0x0600C77A RID: 51066 RVA: 0x0011D03C File Offset: 0x0011B23C
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x170097F6 RID: 38902
			// (set) Token: 0x0600C77B RID: 51067 RVA: 0x0011D054 File Offset: 0x0011B254
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x170097F7 RID: 38903
			// (set) Token: 0x0600C77C RID: 51068 RVA: 0x0011D06C File Offset: 0x0011B26C
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x170097F8 RID: 38904
			// (set) Token: 0x0600C77D RID: 51069 RVA: 0x0011D084 File Offset: 0x0011B284
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x170097F9 RID: 38905
			// (set) Token: 0x0600C77E RID: 51070 RVA: 0x0011D09C File Offset: 0x0011B29C
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x170097FA RID: 38906
			// (set) Token: 0x0600C77F RID: 51071 RVA: 0x0011D0B4 File Offset: 0x0011B2B4
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170097FB RID: 38907
			// (set) Token: 0x0600C780 RID: 51072 RVA: 0x0011D0CC File Offset: 0x0011B2CC
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x170097FC RID: 38908
			// (set) Token: 0x0600C781 RID: 51073 RVA: 0x0011D0E4 File Offset: 0x0011B2E4
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x170097FD RID: 38909
			// (set) Token: 0x0600C782 RID: 51074 RVA: 0x0011D0F7 File Offset: 0x0011B2F7
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170097FE RID: 38910
			// (set) Token: 0x0600C783 RID: 51075 RVA: 0x0011D10F File Offset: 0x0011B30F
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x170097FF RID: 38911
			// (set) Token: 0x0600C784 RID: 51076 RVA: 0x0011D122 File Offset: 0x0011B322
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x17009800 RID: 38912
			// (set) Token: 0x0600C785 RID: 51077 RVA: 0x0011D13A File Offset: 0x0011B33A
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x17009801 RID: 38913
			// (set) Token: 0x0600C786 RID: 51078 RVA: 0x0011D14D File Offset: 0x0011B34D
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17009802 RID: 38914
			// (set) Token: 0x0600C787 RID: 51079 RVA: 0x0011D160 File Offset: 0x0011B360
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17009803 RID: 38915
			// (set) Token: 0x0600C788 RID: 51080 RVA: 0x0011D173 File Offset: 0x0011B373
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009804 RID: 38916
			// (set) Token: 0x0600C789 RID: 51081 RVA: 0x0011D18B File Offset: 0x0011B38B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009805 RID: 38917
			// (set) Token: 0x0600C78A RID: 51082 RVA: 0x0011D1A3 File Offset: 0x0011B3A3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009806 RID: 38918
			// (set) Token: 0x0600C78B RID: 51083 RVA: 0x0011D1BB File Offset: 0x0011B3BB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009807 RID: 38919
			// (set) Token: 0x0600C78C RID: 51084 RVA: 0x0011D1D3 File Offset: 0x0011B3D3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D9F RID: 3487
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17009808 RID: 38920
			// (set) Token: 0x0600C78E RID: 51086 RVA: 0x0011D1F3 File Offset: 0x0011B3F3
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17009809 RID: 38921
			// (set) Token: 0x0600C78F RID: 51087 RVA: 0x0011D211 File Offset: 0x0011B411
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x1700980A RID: 38922
			// (set) Token: 0x0600C790 RID: 51088 RVA: 0x0011D229 File Offset: 0x0011B429
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x1700980B RID: 38923
			// (set) Token: 0x0600C791 RID: 51089 RVA: 0x0011D247 File Offset: 0x0011B447
			public virtual string MailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x1700980C RID: 38924
			// (set) Token: 0x0600C792 RID: 51090 RVA: 0x0011D265 File Offset: 0x0011B465
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x1700980D RID: 38925
			// (set) Token: 0x0600C793 RID: 51091 RVA: 0x0011D278 File Offset: 0x0011B478
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x1700980E RID: 38926
			// (set) Token: 0x0600C794 RID: 51092 RVA: 0x0011D28B File Offset: 0x0011B48B
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawAcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["RawAcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x1700980F RID: 38927
			// (set) Token: 0x0600C795 RID: 51093 RVA: 0x0011D29E File Offset: 0x0011B49E
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawBypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["RawBypassModerationFrom"] = value;
				}
			}

			// Token: 0x17009810 RID: 38928
			// (set) Token: 0x0600C796 RID: 51094 RVA: 0x0011D2B1 File Offset: 0x0011B4B1
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawRejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RawRejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17009811 RID: 38929
			// (set) Token: 0x0600C797 RID: 51095 RVA: 0x0011D2C4 File Offset: 0x0011B4C4
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> RawGrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["RawGrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17009812 RID: 38930
			// (set) Token: 0x0600C798 RID: 51096 RVA: 0x0011D2D7 File Offset: 0x0011B4D7
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<ModeratorIDParameter>> RawModeratedBy
			{
				set
				{
					base.PowerSharpParameters["RawModeratedBy"] = value;
				}
			}

			// Token: 0x17009813 RID: 38931
			// (set) Token: 0x0600C799 RID: 51097 RVA: 0x0011D2EA File Offset: 0x0011B4EA
			public virtual RecipientWithAdUserIdParameter<RecipientIdParameter> RawForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["RawForwardingAddress"] = value;
				}
			}

			// Token: 0x17009814 RID: 38932
			// (set) Token: 0x0600C79A RID: 51098 RVA: 0x0011D2FD File Offset: 0x0011B4FD
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17009815 RID: 38933
			// (set) Token: 0x0600C79B RID: 51099 RVA: 0x0011D310 File Offset: 0x0011B510
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17009816 RID: 38934
			// (set) Token: 0x0600C79C RID: 51100 RVA: 0x0011D323 File Offset: 0x0011B523
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17009817 RID: 38935
			// (set) Token: 0x0600C79D RID: 51101 RVA: 0x0011D33B File Offset: 0x0011B53B
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17009818 RID: 38936
			// (set) Token: 0x0600C79E RID: 51102 RVA: 0x0011D353 File Offset: 0x0011B553
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17009819 RID: 38937
			// (set) Token: 0x0600C79F RID: 51103 RVA: 0x0011D366 File Offset: 0x0011B566
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x1700981A RID: 38938
			// (set) Token: 0x0600C7A0 RID: 51104 RVA: 0x0011D379 File Offset: 0x0011B579
			public virtual SwitchParameter SoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x1700981B RID: 38939
			// (set) Token: 0x0600C7A1 RID: 51105 RVA: 0x0011D391 File Offset: 0x0011B591
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700981C RID: 38940
			// (set) Token: 0x0600C7A2 RID: 51106 RVA: 0x0011D3A9 File Offset: 0x0011B5A9
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x1700981D RID: 38941
			// (set) Token: 0x0600C7A3 RID: 51107 RVA: 0x0011D3C1 File Offset: 0x0011B5C1
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x1700981E RID: 38942
			// (set) Token: 0x0600C7A4 RID: 51108 RVA: 0x0011D3D9 File Offset: 0x0011B5D9
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700981F RID: 38943
			// (set) Token: 0x0600C7A5 RID: 51109 RVA: 0x0011D3F7 File Offset: 0x0011B5F7
			public virtual string ForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17009820 RID: 38944
			// (set) Token: 0x0600C7A6 RID: 51110 RVA: 0x0011D415 File Offset: 0x0011B615
			public virtual OfflineAddressBookIdParameter OfflineAddressBook
			{
				set
				{
					base.PowerSharpParameters["OfflineAddressBook"] = value;
				}
			}

			// Token: 0x17009821 RID: 38945
			// (set) Token: 0x0600C7A7 RID: 51111 RVA: 0x0011D428 File Offset: 0x0011B628
			public virtual string LinkedMasterAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedMasterAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17009822 RID: 38946
			// (set) Token: 0x0600C7A8 RID: 51112 RVA: 0x0011D446 File Offset: 0x0011B646
			public virtual string LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x17009823 RID: 38947
			// (set) Token: 0x0600C7A9 RID: 51113 RVA: 0x0011D459 File Offset: 0x0011B659
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x17009824 RID: 38948
			// (set) Token: 0x0600C7AA RID: 51114 RVA: 0x0011D46C File Offset: 0x0011B66C
			public virtual ConvertibleMailboxSubType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x17009825 RID: 38949
			// (set) Token: 0x0600C7AB RID: 51115 RVA: 0x0011D484 File Offset: 0x0011B684
			public virtual SwitchParameter ApplyMandatoryProperties
			{
				set
				{
					base.PowerSharpParameters["ApplyMandatoryProperties"] = value;
				}
			}

			// Token: 0x17009826 RID: 38950
			// (set) Token: 0x0600C7AC RID: 51116 RVA: 0x0011D49C File Offset: 0x0011B69C
			public virtual SwitchParameter RemoveManagedFolderAndPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoveManagedFolderAndPolicy"] = value;
				}
			}

			// Token: 0x17009827 RID: 38951
			// (set) Token: 0x0600C7AD RID: 51117 RVA: 0x0011D4B4 File Offset: 0x0011B6B4
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009828 RID: 38952
			// (set) Token: 0x0600C7AE RID: 51118 RVA: 0x0011D4D2 File Offset: 0x0011B6D2
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17009829 RID: 38953
			// (set) Token: 0x0600C7AF RID: 51119 RVA: 0x0011D4F0 File Offset: 0x0011B6F0
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x1700982A RID: 38954
			// (set) Token: 0x0600C7B0 RID: 51120 RVA: 0x0011D503 File Offset: 0x0011B703
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700982B RID: 38955
			// (set) Token: 0x0600C7B1 RID: 51121 RVA: 0x0011D521 File Offset: 0x0011B721
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x1700982C RID: 38956
			// (set) Token: 0x0600C7B2 RID: 51122 RVA: 0x0011D539 File Offset: 0x0011B739
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x1700982D RID: 38957
			// (set) Token: 0x0600C7B3 RID: 51123 RVA: 0x0011D551 File Offset: 0x0011B751
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x1700982E RID: 38958
			// (set) Token: 0x0600C7B4 RID: 51124 RVA: 0x0011D564 File Offset: 0x0011B764
			public virtual SwitchParameter BypassLiveId
			{
				set
				{
					base.PowerSharpParameters["BypassLiveId"] = value;
				}
			}

			// Token: 0x1700982F RID: 38959
			// (set) Token: 0x0600C7B5 RID: 51125 RVA: 0x0011D57C File Offset: 0x0011B77C
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x17009830 RID: 38960
			// (set) Token: 0x0600C7B6 RID: 51126 RVA: 0x0011D58F File Offset: 0x0011B78F
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17009831 RID: 38961
			// (set) Token: 0x0600C7B7 RID: 51127 RVA: 0x0011D5A2 File Offset: 0x0011B7A2
			public virtual string FederatedIdentity
			{
				set
				{
					base.PowerSharpParameters["FederatedIdentity"] = value;
				}
			}

			// Token: 0x17009832 RID: 38962
			// (set) Token: 0x0600C7B8 RID: 51128 RVA: 0x0011D5B5 File Offset: 0x0011B7B5
			public virtual string SecondaryAddress
			{
				set
				{
					base.PowerSharpParameters["SecondaryAddress"] = value;
				}
			}

			// Token: 0x17009833 RID: 38963
			// (set) Token: 0x0600C7B9 RID: 51129 RVA: 0x0011D5C8 File Offset: 0x0011B7C8
			public virtual string SecondaryDialPlan
			{
				set
				{
					base.PowerSharpParameters["SecondaryDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17009834 RID: 38964
			// (set) Token: 0x0600C7BA RID: 51130 RVA: 0x0011D5E6 File Offset: 0x0011B7E6
			public virtual SwitchParameter RemovePicture
			{
				set
				{
					base.PowerSharpParameters["RemovePicture"] = value;
				}
			}

			// Token: 0x17009835 RID: 38965
			// (set) Token: 0x0600C7BB RID: 51131 RVA: 0x0011D5FE File Offset: 0x0011B7FE
			public virtual SwitchParameter RemoveSpokenName
			{
				set
				{
					base.PowerSharpParameters["RemoveSpokenName"] = value;
				}
			}

			// Token: 0x17009836 RID: 38966
			// (set) Token: 0x0600C7BC RID: 51132 RVA: 0x0011D616 File Offset: 0x0011B816
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17009837 RID: 38967
			// (set) Token: 0x0600C7BD RID: 51133 RVA: 0x0011D629 File Offset: 0x0011B829
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17009838 RID: 38968
			// (set) Token: 0x0600C7BE RID: 51134 RVA: 0x0011D63C File Offset: 0x0011B83C
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17009839 RID: 38969
			// (set) Token: 0x0600C7BF RID: 51135 RVA: 0x0011D64F File Offset: 0x0011B84F
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700983A RID: 38970
			// (set) Token: 0x0600C7C0 RID: 51136 RVA: 0x0011D66D File Offset: 0x0011B86D
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x1700983B RID: 38971
			// (set) Token: 0x0600C7C1 RID: 51137 RVA: 0x0011D680 File Offset: 0x0011B880
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700983C RID: 38972
			// (set) Token: 0x0600C7C2 RID: 51138 RVA: 0x0011D693 File Offset: 0x0011B893
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x1700983D RID: 38973
			// (set) Token: 0x0600C7C3 RID: 51139 RVA: 0x0011D6A6 File Offset: 0x0011B8A6
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x1700983E RID: 38974
			// (set) Token: 0x0600C7C4 RID: 51140 RVA: 0x0011D6B9 File Offset: 0x0011B8B9
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x1700983F RID: 38975
			// (set) Token: 0x0600C7C5 RID: 51141 RVA: 0x0011D6CC File Offset: 0x0011B8CC
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17009840 RID: 38976
			// (set) Token: 0x0600C7C6 RID: 51142 RVA: 0x0011D6DF File Offset: 0x0011B8DF
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x17009841 RID: 38977
			// (set) Token: 0x0600C7C7 RID: 51143 RVA: 0x0011D6F7 File Offset: 0x0011B8F7
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17009842 RID: 38978
			// (set) Token: 0x0600C7C8 RID: 51144 RVA: 0x0011D70F File Offset: 0x0011B90F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009843 RID: 38979
			// (set) Token: 0x0600C7C9 RID: 51145 RVA: 0x0011D722 File Offset: 0x0011B922
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17009844 RID: 38980
			// (set) Token: 0x0600C7CA RID: 51146 RVA: 0x0011D735 File Offset: 0x0011B935
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17009845 RID: 38981
			// (set) Token: 0x0600C7CB RID: 51147 RVA: 0x0011D74D File Offset: 0x0011B94D
			public virtual SecurityIdentifier MasterAccountSid
			{
				set
				{
					base.PowerSharpParameters["MasterAccountSid"] = value;
				}
			}

			// Token: 0x17009846 RID: 38982
			// (set) Token: 0x0600C7CC RID: 51148 RVA: 0x0011D760 File Offset: 0x0011B960
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17009847 RID: 38983
			// (set) Token: 0x0600C7CD RID: 51149 RVA: 0x0011D773 File Offset: 0x0011B973
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17009848 RID: 38984
			// (set) Token: 0x0600C7CE RID: 51150 RVA: 0x0011D78B File Offset: 0x0011B98B
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17009849 RID: 38985
			// (set) Token: 0x0600C7CF RID: 51151 RVA: 0x0011D7A3 File Offset: 0x0011B9A3
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x1700984A RID: 38986
			// (set) Token: 0x0600C7D0 RID: 51152 RVA: 0x0011D7BB File Offset: 0x0011B9BB
			public virtual byte SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x1700984B RID: 38987
			// (set) Token: 0x0600C7D1 RID: 51153 RVA: 0x0011D7D3 File Offset: 0x0011B9D3
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x1700984C RID: 38988
			// (set) Token: 0x0600C7D2 RID: 51154 RVA: 0x0011D7E6 File Offset: 0x0011B9E6
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x1700984D RID: 38989
			// (set) Token: 0x0600C7D3 RID: 51155 RVA: 0x0011D7F9 File Offset: 0x0011B9F9
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x1700984E RID: 38990
			// (set) Token: 0x0600C7D4 RID: 51156 RVA: 0x0011D80C File Offset: 0x0011BA0C
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x1700984F RID: 38991
			// (set) Token: 0x0600C7D5 RID: 51157 RVA: 0x0011D81F File Offset: 0x0011BA1F
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17009850 RID: 38992
			// (set) Token: 0x0600C7D6 RID: 51158 RVA: 0x0011D837 File Offset: 0x0011BA37
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17009851 RID: 38993
			// (set) Token: 0x0600C7D7 RID: 51159 RVA: 0x0011D84A File Offset: 0x0011BA4A
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17009852 RID: 38994
			// (set) Token: 0x0600C7D8 RID: 51160 RVA: 0x0011D85D File Offset: 0x0011BA5D
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17009853 RID: 38995
			// (set) Token: 0x0600C7D9 RID: 51161 RVA: 0x0011D870 File Offset: 0x0011BA70
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17009854 RID: 38996
			// (set) Token: 0x0600C7DA RID: 51162 RVA: 0x0011D883 File Offset: 0x0011BA83
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009855 RID: 38997
			// (set) Token: 0x0600C7DB RID: 51163 RVA: 0x0011D896 File Offset: 0x0011BA96
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009856 RID: 38998
			// (set) Token: 0x0600C7DC RID: 51164 RVA: 0x0011D8A9 File Offset: 0x0011BAA9
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17009857 RID: 38999
			// (set) Token: 0x0600C7DD RID: 51165 RVA: 0x0011D8BC File Offset: 0x0011BABC
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17009858 RID: 39000
			// (set) Token: 0x0600C7DE RID: 51166 RVA: 0x0011D8CF File Offset: 0x0011BACF
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17009859 RID: 39001
			// (set) Token: 0x0600C7DF RID: 51167 RVA: 0x0011D8E2 File Offset: 0x0011BAE2
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x1700985A RID: 39002
			// (set) Token: 0x0600C7E0 RID: 51168 RVA: 0x0011D8F5 File Offset: 0x0011BAF5
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x1700985B RID: 39003
			// (set) Token: 0x0600C7E1 RID: 51169 RVA: 0x0011D908 File Offset: 0x0011BB08
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x1700985C RID: 39004
			// (set) Token: 0x0600C7E2 RID: 51170 RVA: 0x0011D91B File Offset: 0x0011BB1B
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x1700985D RID: 39005
			// (set) Token: 0x0600C7E3 RID: 51171 RVA: 0x0011D92E File Offset: 0x0011BB2E
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x1700985E RID: 39006
			// (set) Token: 0x0600C7E4 RID: 51172 RVA: 0x0011D941 File Offset: 0x0011BB41
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x1700985F RID: 39007
			// (set) Token: 0x0600C7E5 RID: 51173 RVA: 0x0011D954 File Offset: 0x0011BB54
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17009860 RID: 39008
			// (set) Token: 0x0600C7E6 RID: 51174 RVA: 0x0011D967 File Offset: 0x0011BB67
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17009861 RID: 39009
			// (set) Token: 0x0600C7E7 RID: 51175 RVA: 0x0011D97A File Offset: 0x0011BB7A
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17009862 RID: 39010
			// (set) Token: 0x0600C7E8 RID: 51176 RVA: 0x0011D98D File Offset: 0x0011BB8D
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17009863 RID: 39011
			// (set) Token: 0x0600C7E9 RID: 51177 RVA: 0x0011D9A5 File Offset: 0x0011BBA5
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17009864 RID: 39012
			// (set) Token: 0x0600C7EA RID: 51178 RVA: 0x0011D9B8 File Offset: 0x0011BBB8
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17009865 RID: 39013
			// (set) Token: 0x0600C7EB RID: 51179 RVA: 0x0011D9CB File Offset: 0x0011BBCB
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17009866 RID: 39014
			// (set) Token: 0x0600C7EC RID: 51180 RVA: 0x0011D9E3 File Offset: 0x0011BBE3
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17009867 RID: 39015
			// (set) Token: 0x0600C7ED RID: 51181 RVA: 0x0011D9F6 File Offset: 0x0011BBF6
			public virtual bool ExcludedFromBackSync
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromBackSync"] = value;
				}
			}

			// Token: 0x17009868 RID: 39016
			// (set) Token: 0x0600C7EE RID: 51182 RVA: 0x0011DA0E File Offset: 0x0011BC0E
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17009869 RID: 39017
			// (set) Token: 0x0600C7EF RID: 51183 RVA: 0x0011DA21 File Offset: 0x0011BC21
			public virtual bool LEOEnabled
			{
				set
				{
					base.PowerSharpParameters["LEOEnabled"] = value;
				}
			}

			// Token: 0x1700986A RID: 39018
			// (set) Token: 0x0600C7F0 RID: 51184 RVA: 0x0011DA39 File Offset: 0x0011BC39
			public virtual bool AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x1700986B RID: 39019
			// (set) Token: 0x0600C7F1 RID: 51185 RVA: 0x0011DA51 File Offset: 0x0011BC51
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x1700986C RID: 39020
			// (set) Token: 0x0600C7F2 RID: 51186 RVA: 0x0011DA69 File Offset: 0x0011BC69
			public virtual bool UseDatabaseRetentionDefaults
			{
				set
				{
					base.PowerSharpParameters["UseDatabaseRetentionDefaults"] = value;
				}
			}

			// Token: 0x1700986D RID: 39021
			// (set) Token: 0x0600C7F3 RID: 51187 RVA: 0x0011DA81 File Offset: 0x0011BC81
			public virtual bool RetainDeletedItemsUntilBackup
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsUntilBackup"] = value;
				}
			}

			// Token: 0x1700986E RID: 39022
			// (set) Token: 0x0600C7F4 RID: 51188 RVA: 0x0011DA99 File Offset: 0x0011BC99
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x1700986F RID: 39023
			// (set) Token: 0x0600C7F5 RID: 51189 RVA: 0x0011DAB1 File Offset: 0x0011BCB1
			public virtual bool IsExcludedFromServingHierarchy
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromServingHierarchy"] = value;
				}
			}

			// Token: 0x17009870 RID: 39024
			// (set) Token: 0x0600C7F6 RID: 51190 RVA: 0x0011DAC9 File Offset: 0x0011BCC9
			public virtual bool IsHierarchyReady
			{
				set
				{
					base.PowerSharpParameters["IsHierarchyReady"] = value;
				}
			}

			// Token: 0x17009871 RID: 39025
			// (set) Token: 0x0600C7F7 RID: 51191 RVA: 0x0011DAE1 File Offset: 0x0011BCE1
			public virtual bool LitigationHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldEnabled"] = value;
				}
			}

			// Token: 0x17009872 RID: 39026
			// (set) Token: 0x0600C7F8 RID: 51192 RVA: 0x0011DAF9 File Offset: 0x0011BCF9
			public virtual bool SingleItemRecoveryEnabled
			{
				set
				{
					base.PowerSharpParameters["SingleItemRecoveryEnabled"] = value;
				}
			}

			// Token: 0x17009873 RID: 39027
			// (set) Token: 0x0600C7F9 RID: 51193 RVA: 0x0011DB11 File Offset: 0x0011BD11
			public virtual bool RetentionHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["RetentionHoldEnabled"] = value;
				}
			}

			// Token: 0x17009874 RID: 39028
			// (set) Token: 0x0600C7FA RID: 51194 RVA: 0x0011DB29 File Offset: 0x0011BD29
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17009875 RID: 39029
			// (set) Token: 0x0600C7FB RID: 51195 RVA: 0x0011DB41 File Offset: 0x0011BD41
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17009876 RID: 39030
			// (set) Token: 0x0600C7FC RID: 51196 RVA: 0x0011DB59 File Offset: 0x0011BD59
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x17009877 RID: 39031
			// (set) Token: 0x0600C7FD RID: 51197 RVA: 0x0011DB6C File Offset: 0x0011BD6C
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x17009878 RID: 39032
			// (set) Token: 0x0600C7FE RID: 51198 RVA: 0x0011DB7F File Offset: 0x0011BD7F
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x17009879 RID: 39033
			// (set) Token: 0x0600C7FF RID: 51199 RVA: 0x0011DB97 File Offset: 0x0011BD97
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x1700987A RID: 39034
			// (set) Token: 0x0600C800 RID: 51200 RVA: 0x0011DBAA File Offset: 0x0011BDAA
			public virtual bool CalendarRepairDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairDisabled"] = value;
				}
			}

			// Token: 0x1700987B RID: 39035
			// (set) Token: 0x0600C801 RID: 51201 RVA: 0x0011DBC2 File Offset: 0x0011BDC2
			public virtual bool MessageTrackingReadStatusEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingReadStatusEnabled"] = value;
				}
			}

			// Token: 0x1700987C RID: 39036
			// (set) Token: 0x0600C802 RID: 51202 RVA: 0x0011DBDA File Offset: 0x0011BDDA
			public virtual ExternalOofOptions ExternalOofOptions
			{
				set
				{
					base.PowerSharpParameters["ExternalOofOptions"] = value;
				}
			}

			// Token: 0x1700987D RID: 39037
			// (set) Token: 0x0600C803 RID: 51203 RVA: 0x0011DBF2 File Offset: 0x0011BDF2
			public virtual ProxyAddress ForwardingSmtpAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingSmtpAddress"] = value;
				}
			}

			// Token: 0x1700987E RID: 39038
			// (set) Token: 0x0600C804 RID: 51204 RVA: 0x0011DC05 File Offset: 0x0011BE05
			public virtual EnhancedTimeSpan RetainDeletedItemsFor
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsFor"] = value;
				}
			}

			// Token: 0x1700987F RID: 39039
			// (set) Token: 0x0600C805 RID: 51205 RVA: 0x0011DC1D File Offset: 0x0011BE1D
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17009880 RID: 39040
			// (set) Token: 0x0600C806 RID: 51206 RVA: 0x0011DC30 File Offset: 0x0011BE30
			public virtual Unlimited<ByteQuantifiedSize> ProhibitSendQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendQuota"] = value;
				}
			}

			// Token: 0x17009881 RID: 39041
			// (set) Token: 0x0600C807 RID: 51207 RVA: 0x0011DC48 File Offset: 0x0011BE48
			public virtual Unlimited<ByteQuantifiedSize> ProhibitSendReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendReceiveQuota"] = value;
				}
			}

			// Token: 0x17009882 RID: 39042
			// (set) Token: 0x0600C808 RID: 51208 RVA: 0x0011DC60 File Offset: 0x0011BE60
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsQuota"] = value;
				}
			}

			// Token: 0x17009883 RID: 39043
			// (set) Token: 0x0600C809 RID: 51209 RVA: 0x0011DC78 File Offset: 0x0011BE78
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsWarningQuota"] = value;
				}
			}

			// Token: 0x17009884 RID: 39044
			// (set) Token: 0x0600C80A RID: 51210 RVA: 0x0011DC90 File Offset: 0x0011BE90
			public virtual Unlimited<ByteQuantifiedSize> CalendarLoggingQuota
			{
				set
				{
					base.PowerSharpParameters["CalendarLoggingQuota"] = value;
				}
			}

			// Token: 0x17009885 RID: 39045
			// (set) Token: 0x0600C80B RID: 51211 RVA: 0x0011DCA8 File Offset: 0x0011BEA8
			public virtual bool DowngradeHighPriorityMessagesEnabled
			{
				set
				{
					base.PowerSharpParameters["DowngradeHighPriorityMessagesEnabled"] = value;
				}
			}

			// Token: 0x17009886 RID: 39046
			// (set) Token: 0x0600C80C RID: 51212 RVA: 0x0011DCC0 File Offset: 0x0011BEC0
			public virtual Unlimited<int> RecipientLimits
			{
				set
				{
					base.PowerSharpParameters["RecipientLimits"] = value;
				}
			}

			// Token: 0x17009887 RID: 39047
			// (set) Token: 0x0600C80D RID: 51213 RVA: 0x0011DCD8 File Offset: 0x0011BED8
			public virtual bool ImListMigrationCompleted
			{
				set
				{
					base.PowerSharpParameters["ImListMigrationCompleted"] = value;
				}
			}

			// Token: 0x17009888 RID: 39048
			// (set) Token: 0x0600C80E RID: 51214 RVA: 0x0011DCF0 File Offset: 0x0011BEF0
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17009889 RID: 39049
			// (set) Token: 0x0600C80F RID: 51215 RVA: 0x0011DD08 File Offset: 0x0011BF08
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x1700988A RID: 39050
			// (set) Token: 0x0600C810 RID: 51216 RVA: 0x0011DD20 File Offset: 0x0011BF20
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x1700988B RID: 39051
			// (set) Token: 0x0600C811 RID: 51217 RVA: 0x0011DD33 File Offset: 0x0011BF33
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700988C RID: 39052
			// (set) Token: 0x0600C812 RID: 51218 RVA: 0x0011DD46 File Offset: 0x0011BF46
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x1700988D RID: 39053
			// (set) Token: 0x0600C813 RID: 51219 RVA: 0x0011DD5E File Offset: 0x0011BF5E
			public virtual bool? SCLDeleteEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteEnabled"] = value;
				}
			}

			// Token: 0x1700988E RID: 39054
			// (set) Token: 0x0600C814 RID: 51220 RVA: 0x0011DD76 File Offset: 0x0011BF76
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x1700988F RID: 39055
			// (set) Token: 0x0600C815 RID: 51221 RVA: 0x0011DD8E File Offset: 0x0011BF8E
			public virtual bool? SCLRejectEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLRejectEnabled"] = value;
				}
			}

			// Token: 0x17009890 RID: 39056
			// (set) Token: 0x0600C816 RID: 51222 RVA: 0x0011DDA6 File Offset: 0x0011BFA6
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x17009891 RID: 39057
			// (set) Token: 0x0600C817 RID: 51223 RVA: 0x0011DDBE File Offset: 0x0011BFBE
			public virtual bool? SCLQuarantineEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineEnabled"] = value;
				}
			}

			// Token: 0x17009892 RID: 39058
			// (set) Token: 0x0600C818 RID: 51224 RVA: 0x0011DDD6 File Offset: 0x0011BFD6
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17009893 RID: 39059
			// (set) Token: 0x0600C819 RID: 51225 RVA: 0x0011DDEE File Offset: 0x0011BFEE
			public virtual bool? SCLJunkEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLJunkEnabled"] = value;
				}
			}

			// Token: 0x17009894 RID: 39060
			// (set) Token: 0x0600C81A RID: 51226 RVA: 0x0011DE06 File Offset: 0x0011C006
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x17009895 RID: 39061
			// (set) Token: 0x0600C81B RID: 51227 RVA: 0x0011DE1E File Offset: 0x0011C01E
			public virtual bool? UseDatabaseQuotaDefaults
			{
				set
				{
					base.PowerSharpParameters["UseDatabaseQuotaDefaults"] = value;
				}
			}

			// Token: 0x17009896 RID: 39062
			// (set) Token: 0x0600C81C RID: 51228 RVA: 0x0011DE36 File Offset: 0x0011C036
			public virtual Unlimited<ByteQuantifiedSize> IssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["IssueWarningQuota"] = value;
				}
			}

			// Token: 0x17009897 RID: 39063
			// (set) Token: 0x0600C81D RID: 51229 RVA: 0x0011DE4E File Offset: 0x0011C04E
			public virtual ByteQuantifiedSize RulesQuota
			{
				set
				{
					base.PowerSharpParameters["RulesQuota"] = value;
				}
			}

			// Token: 0x17009898 RID: 39064
			// (set) Token: 0x0600C81E RID: 51230 RVA: 0x0011DE66 File Offset: 0x0011C066
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17009899 RID: 39065
			// (set) Token: 0x0600C81F RID: 51231 RVA: 0x0011DE79 File Offset: 0x0011C079
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x1700989A RID: 39066
			// (set) Token: 0x0600C820 RID: 51232 RVA: 0x0011DE8C File Offset: 0x0011C08C
			public virtual int? MaxSafeSenders
			{
				set
				{
					base.PowerSharpParameters["MaxSafeSenders"] = value;
				}
			}

			// Token: 0x1700989B RID: 39067
			// (set) Token: 0x0600C821 RID: 51233 RVA: 0x0011DEA4 File Offset: 0x0011C0A4
			public virtual int? MaxBlockedSenders
			{
				set
				{
					base.PowerSharpParameters["MaxBlockedSenders"] = value;
				}
			}

			// Token: 0x1700989C RID: 39068
			// (set) Token: 0x0600C822 RID: 51234 RVA: 0x0011DEBC File Offset: 0x0011C0BC
			public virtual SmtpAddress WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x1700989D RID: 39069
			// (set) Token: 0x0600C823 RID: 51235 RVA: 0x0011DED4 File Offset: 0x0011C0D4
			public virtual SmtpAddress MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x1700989E RID: 39070
			// (set) Token: 0x0600C824 RID: 51236 RVA: 0x0011DEEC File Offset: 0x0011C0EC
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x1700989F RID: 39071
			// (set) Token: 0x0600C825 RID: 51237 RVA: 0x0011DEFF File Offset: 0x0011C0FF
			public virtual SmtpAddress JournalArchiveAddress
			{
				set
				{
					base.PowerSharpParameters["JournalArchiveAddress"] = value;
				}
			}

			// Token: 0x170098A0 RID: 39072
			// (set) Token: 0x0600C826 RID: 51238 RVA: 0x0011DF17 File Offset: 0x0011C117
			public virtual Unlimited<ByteQuantifiedSize> ArchiveQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveQuota"] = value;
				}
			}

			// Token: 0x170098A1 RID: 39073
			// (set) Token: 0x0600C827 RID: 51239 RVA: 0x0011DF2F File Offset: 0x0011C12F
			public virtual Unlimited<ByteQuantifiedSize> ArchiveWarningQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveWarningQuota"] = value;
				}
			}

			// Token: 0x170098A2 RID: 39074
			// (set) Token: 0x0600C828 RID: 51240 RVA: 0x0011DF47 File Offset: 0x0011C147
			public virtual SmtpDomain ArchiveDomain
			{
				set
				{
					base.PowerSharpParameters["ArchiveDomain"] = value;
				}
			}

			// Token: 0x170098A3 RID: 39075
			// (set) Token: 0x0600C829 RID: 51241 RVA: 0x0011DF5A File Offset: 0x0011C15A
			public virtual ArchiveStatusFlags ArchiveStatus
			{
				set
				{
					base.PowerSharpParameters["ArchiveStatus"] = value;
				}
			}

			// Token: 0x170098A4 RID: 39076
			// (set) Token: 0x0600C82A RID: 51242 RVA: 0x0011DF72 File Offset: 0x0011C172
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x170098A5 RID: 39077
			// (set) Token: 0x0600C82B RID: 51243 RVA: 0x0011DF8A File Offset: 0x0011C18A
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x170098A6 RID: 39078
			// (set) Token: 0x0600C82C RID: 51244 RVA: 0x0011DFA2 File Offset: 0x0011C1A2
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x170098A7 RID: 39079
			// (set) Token: 0x0600C82D RID: 51245 RVA: 0x0011DFB5 File Offset: 0x0011C1B5
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x170098A8 RID: 39080
			// (set) Token: 0x0600C82E RID: 51246 RVA: 0x0011DFC8 File Offset: 0x0011C1C8
			public virtual bool CalendarVersionStoreDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarVersionStoreDisabled"] = value;
				}
			}

			// Token: 0x170098A9 RID: 39081
			// (set) Token: 0x0600C82F RID: 51247 RVA: 0x0011DFE0 File Offset: 0x0011C1E0
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x170098AA RID: 39082
			// (set) Token: 0x0600C830 RID: 51248 RVA: 0x0011DFF3 File Offset: 0x0011C1F3
			public virtual bool? SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x170098AB RID: 39083
			// (set) Token: 0x0600C831 RID: 51249 RVA: 0x0011E00B File Offset: 0x0011C20B
			public virtual bool AuditEnabled
			{
				set
				{
					base.PowerSharpParameters["AuditEnabled"] = value;
				}
			}

			// Token: 0x170098AC RID: 39084
			// (set) Token: 0x0600C832 RID: 51250 RVA: 0x0011E023 File Offset: 0x0011C223
			public virtual EnhancedTimeSpan AuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["AuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x170098AD RID: 39085
			// (set) Token: 0x0600C833 RID: 51251 RVA: 0x0011E03B File Offset: 0x0011C23B
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditAdmin
			{
				set
				{
					base.PowerSharpParameters["AuditAdmin"] = value;
				}
			}

			// Token: 0x170098AE RID: 39086
			// (set) Token: 0x0600C834 RID: 51252 RVA: 0x0011E04E File Offset: 0x0011C24E
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditDelegate
			{
				set
				{
					base.PowerSharpParameters["AuditDelegate"] = value;
				}
			}

			// Token: 0x170098AF RID: 39087
			// (set) Token: 0x0600C835 RID: 51253 RVA: 0x0011E061 File Offset: 0x0011C261
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditOwner
			{
				set
				{
					base.PowerSharpParameters["AuditOwner"] = value;
				}
			}

			// Token: 0x170098B0 RID: 39088
			// (set) Token: 0x0600C836 RID: 51254 RVA: 0x0011E074 File Offset: 0x0011C274
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x170098B1 RID: 39089
			// (set) Token: 0x0600C837 RID: 51255 RVA: 0x0011E087 File Offset: 0x0011C287
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170098B2 RID: 39090
			// (set) Token: 0x0600C838 RID: 51256 RVA: 0x0011E09A File Offset: 0x0011C29A
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x170098B3 RID: 39091
			// (set) Token: 0x0600C839 RID: 51257 RVA: 0x0011E0AD File Offset: 0x0011C2AD
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x170098B4 RID: 39092
			// (set) Token: 0x0600C83A RID: 51258 RVA: 0x0011E0C0 File Offset: 0x0011C2C0
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x170098B5 RID: 39093
			// (set) Token: 0x0600C83B RID: 51259 RVA: 0x0011E0D3 File Offset: 0x0011C2D3
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x170098B6 RID: 39094
			// (set) Token: 0x0600C83C RID: 51260 RVA: 0x0011E0E6 File Offset: 0x0011C2E6
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x170098B7 RID: 39095
			// (set) Token: 0x0600C83D RID: 51261 RVA: 0x0011E0F9 File Offset: 0x0011C2F9
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x170098B8 RID: 39096
			// (set) Token: 0x0600C83E RID: 51262 RVA: 0x0011E10C File Offset: 0x0011C30C
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x170098B9 RID: 39097
			// (set) Token: 0x0600C83F RID: 51263 RVA: 0x0011E11F File Offset: 0x0011C31F
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x170098BA RID: 39098
			// (set) Token: 0x0600C840 RID: 51264 RVA: 0x0011E132 File Offset: 0x0011C332
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x170098BB RID: 39099
			// (set) Token: 0x0600C841 RID: 51265 RVA: 0x0011E145 File Offset: 0x0011C345
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x170098BC RID: 39100
			// (set) Token: 0x0600C842 RID: 51266 RVA: 0x0011E158 File Offset: 0x0011C358
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x170098BD RID: 39101
			// (set) Token: 0x0600C843 RID: 51267 RVA: 0x0011E16B File Offset: 0x0011C36B
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x170098BE RID: 39102
			// (set) Token: 0x0600C844 RID: 51268 RVA: 0x0011E17E File Offset: 0x0011C37E
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x170098BF RID: 39103
			// (set) Token: 0x0600C845 RID: 51269 RVA: 0x0011E191 File Offset: 0x0011C391
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x170098C0 RID: 39104
			// (set) Token: 0x0600C846 RID: 51270 RVA: 0x0011E1A4 File Offset: 0x0011C3A4
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x170098C1 RID: 39105
			// (set) Token: 0x0600C847 RID: 51271 RVA: 0x0011E1B7 File Offset: 0x0011C3B7
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x170098C2 RID: 39106
			// (set) Token: 0x0600C848 RID: 51272 RVA: 0x0011E1CA File Offset: 0x0011C3CA
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x170098C3 RID: 39107
			// (set) Token: 0x0600C849 RID: 51273 RVA: 0x0011E1DD File Offset: 0x0011C3DD
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x170098C4 RID: 39108
			// (set) Token: 0x0600C84A RID: 51274 RVA: 0x0011E1F0 File Offset: 0x0011C3F0
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x170098C5 RID: 39109
			// (set) Token: 0x0600C84B RID: 51275 RVA: 0x0011E203 File Offset: 0x0011C403
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x170098C6 RID: 39110
			// (set) Token: 0x0600C84C RID: 51276 RVA: 0x0011E216 File Offset: 0x0011C416
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170098C7 RID: 39111
			// (set) Token: 0x0600C84D RID: 51277 RVA: 0x0011E229 File Offset: 0x0011C429
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x170098C8 RID: 39112
			// (set) Token: 0x0600C84E RID: 51278 RVA: 0x0011E23C File Offset: 0x0011C43C
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x170098C9 RID: 39113
			// (set) Token: 0x0600C84F RID: 51279 RVA: 0x0011E254 File Offset: 0x0011C454
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x170098CA RID: 39114
			// (set) Token: 0x0600C850 RID: 51280 RVA: 0x0011E26C File Offset: 0x0011C46C
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x170098CB RID: 39115
			// (set) Token: 0x0600C851 RID: 51281 RVA: 0x0011E284 File Offset: 0x0011C484
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x170098CC RID: 39116
			// (set) Token: 0x0600C852 RID: 51282 RVA: 0x0011E29C File Offset: 0x0011C49C
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x170098CD RID: 39117
			// (set) Token: 0x0600C853 RID: 51283 RVA: 0x0011E2B4 File Offset: 0x0011C4B4
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170098CE RID: 39118
			// (set) Token: 0x0600C854 RID: 51284 RVA: 0x0011E2CC File Offset: 0x0011C4CC
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x170098CF RID: 39119
			// (set) Token: 0x0600C855 RID: 51285 RVA: 0x0011E2E4 File Offset: 0x0011C4E4
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x170098D0 RID: 39120
			// (set) Token: 0x0600C856 RID: 51286 RVA: 0x0011E2F7 File Offset: 0x0011C4F7
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170098D1 RID: 39121
			// (set) Token: 0x0600C857 RID: 51287 RVA: 0x0011E30F File Offset: 0x0011C50F
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x170098D2 RID: 39122
			// (set) Token: 0x0600C858 RID: 51288 RVA: 0x0011E322 File Offset: 0x0011C522
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x170098D3 RID: 39123
			// (set) Token: 0x0600C859 RID: 51289 RVA: 0x0011E33A File Offset: 0x0011C53A
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x170098D4 RID: 39124
			// (set) Token: 0x0600C85A RID: 51290 RVA: 0x0011E34D File Offset: 0x0011C54D
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x170098D5 RID: 39125
			// (set) Token: 0x0600C85B RID: 51291 RVA: 0x0011E360 File Offset: 0x0011C560
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170098D6 RID: 39126
			// (set) Token: 0x0600C85C RID: 51292 RVA: 0x0011E373 File Offset: 0x0011C573
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170098D7 RID: 39127
			// (set) Token: 0x0600C85D RID: 51293 RVA: 0x0011E38B File Offset: 0x0011C58B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170098D8 RID: 39128
			// (set) Token: 0x0600C85E RID: 51294 RVA: 0x0011E3A3 File Offset: 0x0011C5A3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170098D9 RID: 39129
			// (set) Token: 0x0600C85F RID: 51295 RVA: 0x0011E3BB File Offset: 0x0011C5BB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170098DA RID: 39130
			// (set) Token: 0x0600C860 RID: 51296 RVA: 0x0011E3D3 File Offset: 0x0011C5D3
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
