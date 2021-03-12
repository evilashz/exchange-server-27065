using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D6A RID: 3434
	public class SetSyncDistributionGroupCommand : SyntheticCommandWithPipelineInputNoOutput<SyncDistributionGroup>
	{
		// Token: 0x0600B6AC RID: 46764 RVA: 0x00106CE8 File Offset: 0x00104EE8
		private SetSyncDistributionGroupCommand() : base("Set-SyncDistributionGroup")
		{
		}

		// Token: 0x0600B6AD RID: 46765 RVA: 0x00106CF5 File Offset: 0x00104EF5
		public SetSyncDistributionGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B6AE RID: 46766 RVA: 0x00106D04 File Offset: 0x00104F04
		public virtual SetSyncDistributionGroupCommand SetParameters(SetSyncDistributionGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B6AF RID: 46767 RVA: 0x00106D0E File Offset: 0x00104F0E
		public virtual SetSyncDistributionGroupCommand SetParameters(SetSyncDistributionGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D6B RID: 3435
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17008791 RID: 34705
			// (set) Token: 0x0600B6B0 RID: 46768 RVA: 0x00106D18 File Offset: 0x00104F18
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17008792 RID: 34706
			// (set) Token: 0x0600B6B1 RID: 46769 RVA: 0x00106D2B File Offset: 0x00104F2B
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17008793 RID: 34707
			// (set) Token: 0x0600B6B2 RID: 46770 RVA: 0x00106D3E File Offset: 0x00104F3E
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawAcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["RawAcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17008794 RID: 34708
			// (set) Token: 0x0600B6B3 RID: 46771 RVA: 0x00106D51 File Offset: 0x00104F51
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawBypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["RawBypassModerationFrom"] = value;
				}
			}

			// Token: 0x17008795 RID: 34709
			// (set) Token: 0x0600B6B4 RID: 46772 RVA: 0x00106D64 File Offset: 0x00104F64
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawRejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RawRejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17008796 RID: 34710
			// (set) Token: 0x0600B6B5 RID: 46773 RVA: 0x00106D77 File Offset: 0x00104F77
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> RawGrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["RawGrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17008797 RID: 34711
			// (set) Token: 0x0600B6B6 RID: 46774 RVA: 0x00106D8A File Offset: 0x00104F8A
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<ModeratorIDParameter>> RawModeratedBy
			{
				set
				{
					base.PowerSharpParameters["RawModeratedBy"] = value;
				}
			}

			// Token: 0x17008798 RID: 34712
			// (set) Token: 0x0600B6B7 RID: 46775 RVA: 0x00106D9D File Offset: 0x00104F9D
			public virtual MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>> RawMembers
			{
				set
				{
					base.PowerSharpParameters["RawMembers"] = value;
				}
			}

			// Token: 0x17008799 RID: 34713
			// (set) Token: 0x0600B6B8 RID: 46776 RVA: 0x00106DB0 File Offset: 0x00104FB0
			public virtual RecipientWithAdUserIdParameter<RecipientIdParameter> RawManagedBy
			{
				set
				{
					base.PowerSharpParameters["RawManagedBy"] = value;
				}
			}

			// Token: 0x1700879A RID: 34714
			// (set) Token: 0x0600B6B9 RID: 46777 RVA: 0x00106DC3 File Offset: 0x00104FC3
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> RawCoManagedBy
			{
				set
				{
					base.PowerSharpParameters["RawCoManagedBy"] = value;
				}
			}

			// Token: 0x1700879B RID: 34715
			// (set) Token: 0x0600B6BA RID: 46778 RVA: 0x00106DD6 File Offset: 0x00104FD6
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x1700879C RID: 34716
			// (set) Token: 0x0600B6BB RID: 46779 RVA: 0x00106DEE File Offset: 0x00104FEE
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x1700879D RID: 34717
			// (set) Token: 0x0600B6BC RID: 46780 RVA: 0x00106E01 File Offset: 0x00105001
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x1700879E RID: 34718
			// (set) Token: 0x0600B6BD RID: 46781 RVA: 0x00106E14 File Offset: 0x00105014
			public virtual GroupType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x1700879F RID: 34719
			// (set) Token: 0x0600B6BE RID: 46782 RVA: 0x00106E2C File Offset: 0x0010502C
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x170087A0 RID: 34720
			// (set) Token: 0x0600B6BF RID: 46783 RVA: 0x00106E44 File Offset: 0x00105044
			public virtual string ExpansionServer
			{
				set
				{
					base.PowerSharpParameters["ExpansionServer"] = value;
				}
			}

			// Token: 0x170087A1 RID: 34721
			// (set) Token: 0x0600B6C0 RID: 46784 RVA: 0x00106E57 File Offset: 0x00105057
			public virtual MultiValuedProperty<GeneralRecipientIdParameter> ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = value;
				}
			}

			// Token: 0x170087A2 RID: 34722
			// (set) Token: 0x0600B6C1 RID: 46785 RVA: 0x00106E6A File Offset: 0x0010506A
			public virtual MemberUpdateType MemberJoinRestriction
			{
				set
				{
					base.PowerSharpParameters["MemberJoinRestriction"] = value;
				}
			}

			// Token: 0x170087A3 RID: 34723
			// (set) Token: 0x0600B6C2 RID: 46786 RVA: 0x00106E82 File Offset: 0x00105082
			public virtual MemberUpdateType MemberDepartRestriction
			{
				set
				{
					base.PowerSharpParameters["MemberDepartRestriction"] = value;
				}
			}

			// Token: 0x170087A4 RID: 34724
			// (set) Token: 0x0600B6C3 RID: 46787 RVA: 0x00106E9A File Offset: 0x0010509A
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x170087A5 RID: 34725
			// (set) Token: 0x0600B6C4 RID: 46788 RVA: 0x00106EB2 File Offset: 0x001050B2
			public virtual SwitchParameter RoomList
			{
				set
				{
					base.PowerSharpParameters["RoomList"] = value;
				}
			}

			// Token: 0x170087A6 RID: 34726
			// (set) Token: 0x0600B6C5 RID: 46789 RVA: 0x00106ECA File Offset: 0x001050CA
			public virtual SwitchParameter IgnoreNamingPolicy
			{
				set
				{
					base.PowerSharpParameters["IgnoreNamingPolicy"] = value;
				}
			}

			// Token: 0x170087A7 RID: 34727
			// (set) Token: 0x0600B6C6 RID: 46790 RVA: 0x00106EE2 File Offset: 0x001050E2
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x170087A8 RID: 34728
			// (set) Token: 0x0600B6C7 RID: 46791 RVA: 0x00106EF5 File Offset: 0x001050F5
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x170087A9 RID: 34729
			// (set) Token: 0x0600B6C8 RID: 46792 RVA: 0x00106F08 File Offset: 0x00105108
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x170087AA RID: 34730
			// (set) Token: 0x0600B6C9 RID: 46793 RVA: 0x00106F1B File Offset: 0x0010511B
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170087AB RID: 34731
			// (set) Token: 0x0600B6CA RID: 46794 RVA: 0x00106F39 File Offset: 0x00105139
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x170087AC RID: 34732
			// (set) Token: 0x0600B6CB RID: 46795 RVA: 0x00106F4C File Offset: 0x0010514C
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170087AD RID: 34733
			// (set) Token: 0x0600B6CC RID: 46796 RVA: 0x00106F5F File Offset: 0x0010515F
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x170087AE RID: 34734
			// (set) Token: 0x0600B6CD RID: 46797 RVA: 0x00106F72 File Offset: 0x00105172
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x170087AF RID: 34735
			// (set) Token: 0x0600B6CE RID: 46798 RVA: 0x00106F85 File Offset: 0x00105185
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x170087B0 RID: 34736
			// (set) Token: 0x0600B6CF RID: 46799 RVA: 0x00106F98 File Offset: 0x00105198
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x170087B1 RID: 34737
			// (set) Token: 0x0600B6D0 RID: 46800 RVA: 0x00106FAB File Offset: 0x001051AB
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x170087B2 RID: 34738
			// (set) Token: 0x0600B6D1 RID: 46801 RVA: 0x00106FC3 File Offset: 0x001051C3
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170087B3 RID: 34739
			// (set) Token: 0x0600B6D2 RID: 46802 RVA: 0x00106FDB File Offset: 0x001051DB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170087B4 RID: 34740
			// (set) Token: 0x0600B6D3 RID: 46803 RVA: 0x00106FEE File Offset: 0x001051EE
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x170087B5 RID: 34741
			// (set) Token: 0x0600B6D4 RID: 46804 RVA: 0x00107006 File Offset: 0x00105206
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x170087B6 RID: 34742
			// (set) Token: 0x0600B6D5 RID: 46805 RVA: 0x00107019 File Offset: 0x00105219
			public virtual RecipientDisplayType? RecipientDisplayType
			{
				set
				{
					base.PowerSharpParameters["RecipientDisplayType"] = value;
				}
			}

			// Token: 0x170087B7 RID: 34743
			// (set) Token: 0x0600B6D6 RID: 46806 RVA: 0x00107031 File Offset: 0x00105231
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x170087B8 RID: 34744
			// (set) Token: 0x0600B6D7 RID: 46807 RVA: 0x00107049 File Offset: 0x00105249
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x170087B9 RID: 34745
			// (set) Token: 0x0600B6D8 RID: 46808 RVA: 0x00107061 File Offset: 0x00105261
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x170087BA RID: 34746
			// (set) Token: 0x0600B6D9 RID: 46809 RVA: 0x00107074 File Offset: 0x00105274
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x170087BB RID: 34747
			// (set) Token: 0x0600B6DA RID: 46810 RVA: 0x0010708C File Offset: 0x0010528C
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x170087BC RID: 34748
			// (set) Token: 0x0600B6DB RID: 46811 RVA: 0x0010709F File Offset: 0x0010529F
			public virtual bool IsHierarchicalGroup
			{
				set
				{
					base.PowerSharpParameters["IsHierarchicalGroup"] = value;
				}
			}

			// Token: 0x170087BD RID: 34749
			// (set) Token: 0x0600B6DC RID: 46812 RVA: 0x001070B7 File Offset: 0x001052B7
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x170087BE RID: 34750
			// (set) Token: 0x0600B6DD RID: 46813 RVA: 0x001070CA File Offset: 0x001052CA
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x170087BF RID: 34751
			// (set) Token: 0x0600B6DE RID: 46814 RVA: 0x001070E2 File Offset: 0x001052E2
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x170087C0 RID: 34752
			// (set) Token: 0x0600B6DF RID: 46815 RVA: 0x001070F5 File Offset: 0x001052F5
			public virtual bool ExcludedFromBackSync
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromBackSync"] = value;
				}
			}

			// Token: 0x170087C1 RID: 34753
			// (set) Token: 0x0600B6E0 RID: 46816 RVA: 0x0010710D File Offset: 0x0010530D
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x170087C2 RID: 34754
			// (set) Token: 0x0600B6E1 RID: 46817 RVA: 0x00107120 File Offset: 0x00105320
			public virtual bool BypassNestedModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["BypassNestedModerationEnabled"] = value;
				}
			}

			// Token: 0x170087C3 RID: 34755
			// (set) Token: 0x0600B6E2 RID: 46818 RVA: 0x00107138 File Offset: 0x00105338
			public virtual bool ReportToManagerEnabled
			{
				set
				{
					base.PowerSharpParameters["ReportToManagerEnabled"] = value;
				}
			}

			// Token: 0x170087C4 RID: 34756
			// (set) Token: 0x0600B6E3 RID: 46819 RVA: 0x00107150 File Offset: 0x00105350
			public virtual bool ReportToOriginatorEnabled
			{
				set
				{
					base.PowerSharpParameters["ReportToOriginatorEnabled"] = value;
				}
			}

			// Token: 0x170087C5 RID: 34757
			// (set) Token: 0x0600B6E4 RID: 46820 RVA: 0x00107168 File Offset: 0x00105368
			public virtual bool SendOofMessageToOriginatorEnabled
			{
				set
				{
					base.PowerSharpParameters["SendOofMessageToOriginatorEnabled"] = value;
				}
			}

			// Token: 0x170087C6 RID: 34758
			// (set) Token: 0x0600B6E5 RID: 46821 RVA: 0x00107180 File Offset: 0x00105380
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170087C7 RID: 34759
			// (set) Token: 0x0600B6E6 RID: 46822 RVA: 0x00107193 File Offset: 0x00105393
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x170087C8 RID: 34760
			// (set) Token: 0x0600B6E7 RID: 46823 RVA: 0x001071A6 File Offset: 0x001053A6
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x170087C9 RID: 34761
			// (set) Token: 0x0600B6E8 RID: 46824 RVA: 0x001071B9 File Offset: 0x001053B9
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x170087CA RID: 34762
			// (set) Token: 0x0600B6E9 RID: 46825 RVA: 0x001071CC File Offset: 0x001053CC
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x170087CB RID: 34763
			// (set) Token: 0x0600B6EA RID: 46826 RVA: 0x001071DF File Offset: 0x001053DF
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x170087CC RID: 34764
			// (set) Token: 0x0600B6EB RID: 46827 RVA: 0x001071F2 File Offset: 0x001053F2
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x170087CD RID: 34765
			// (set) Token: 0x0600B6EC RID: 46828 RVA: 0x00107205 File Offset: 0x00105405
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x170087CE RID: 34766
			// (set) Token: 0x0600B6ED RID: 46829 RVA: 0x00107218 File Offset: 0x00105418
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x170087CF RID: 34767
			// (set) Token: 0x0600B6EE RID: 46830 RVA: 0x0010722B File Offset: 0x0010542B
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x170087D0 RID: 34768
			// (set) Token: 0x0600B6EF RID: 46831 RVA: 0x0010723E File Offset: 0x0010543E
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x170087D1 RID: 34769
			// (set) Token: 0x0600B6F0 RID: 46832 RVA: 0x00107251 File Offset: 0x00105451
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x170087D2 RID: 34770
			// (set) Token: 0x0600B6F1 RID: 46833 RVA: 0x00107264 File Offset: 0x00105464
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x170087D3 RID: 34771
			// (set) Token: 0x0600B6F2 RID: 46834 RVA: 0x00107277 File Offset: 0x00105477
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x170087D4 RID: 34772
			// (set) Token: 0x0600B6F3 RID: 46835 RVA: 0x0010728A File Offset: 0x0010548A
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x170087D5 RID: 34773
			// (set) Token: 0x0600B6F4 RID: 46836 RVA: 0x0010729D File Offset: 0x0010549D
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x170087D6 RID: 34774
			// (set) Token: 0x0600B6F5 RID: 46837 RVA: 0x001072B0 File Offset: 0x001054B0
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x170087D7 RID: 34775
			// (set) Token: 0x0600B6F6 RID: 46838 RVA: 0x001072C3 File Offset: 0x001054C3
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x170087D8 RID: 34776
			// (set) Token: 0x0600B6F7 RID: 46839 RVA: 0x001072D6 File Offset: 0x001054D6
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x170087D9 RID: 34777
			// (set) Token: 0x0600B6F8 RID: 46840 RVA: 0x001072E9 File Offset: 0x001054E9
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x170087DA RID: 34778
			// (set) Token: 0x0600B6F9 RID: 46841 RVA: 0x001072FC File Offset: 0x001054FC
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x170087DB RID: 34779
			// (set) Token: 0x0600B6FA RID: 46842 RVA: 0x0010730F File Offset: 0x0010550F
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170087DC RID: 34780
			// (set) Token: 0x0600B6FB RID: 46843 RVA: 0x00107322 File Offset: 0x00105522
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x170087DD RID: 34781
			// (set) Token: 0x0600B6FC RID: 46844 RVA: 0x00107335 File Offset: 0x00105535
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x170087DE RID: 34782
			// (set) Token: 0x0600B6FD RID: 46845 RVA: 0x0010734D File Offset: 0x0010554D
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x170087DF RID: 34783
			// (set) Token: 0x0600B6FE RID: 46846 RVA: 0x00107365 File Offset: 0x00105565
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x170087E0 RID: 34784
			// (set) Token: 0x0600B6FF RID: 46847 RVA: 0x0010737D File Offset: 0x0010557D
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x170087E1 RID: 34785
			// (set) Token: 0x0600B700 RID: 46848 RVA: 0x00107395 File Offset: 0x00105595
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x170087E2 RID: 34786
			// (set) Token: 0x0600B701 RID: 46849 RVA: 0x001073AD File Offset: 0x001055AD
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170087E3 RID: 34787
			// (set) Token: 0x0600B702 RID: 46850 RVA: 0x001073C5 File Offset: 0x001055C5
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x170087E4 RID: 34788
			// (set) Token: 0x0600B703 RID: 46851 RVA: 0x001073DD File Offset: 0x001055DD
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x170087E5 RID: 34789
			// (set) Token: 0x0600B704 RID: 46852 RVA: 0x001073F0 File Offset: 0x001055F0
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170087E6 RID: 34790
			// (set) Token: 0x0600B705 RID: 46853 RVA: 0x00107408 File Offset: 0x00105608
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x170087E7 RID: 34791
			// (set) Token: 0x0600B706 RID: 46854 RVA: 0x0010741B File Offset: 0x0010561B
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x170087E8 RID: 34792
			// (set) Token: 0x0600B707 RID: 46855 RVA: 0x00107433 File Offset: 0x00105633
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x170087E9 RID: 34793
			// (set) Token: 0x0600B708 RID: 46856 RVA: 0x00107446 File Offset: 0x00105646
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x170087EA RID: 34794
			// (set) Token: 0x0600B709 RID: 46857 RVA: 0x00107459 File Offset: 0x00105659
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170087EB RID: 34795
			// (set) Token: 0x0600B70A RID: 46858 RVA: 0x0010746C File Offset: 0x0010566C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170087EC RID: 34796
			// (set) Token: 0x0600B70B RID: 46859 RVA: 0x00107484 File Offset: 0x00105684
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170087ED RID: 34797
			// (set) Token: 0x0600B70C RID: 46860 RVA: 0x0010749C File Offset: 0x0010569C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170087EE RID: 34798
			// (set) Token: 0x0600B70D RID: 46861 RVA: 0x001074B4 File Offset: 0x001056B4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170087EF RID: 34799
			// (set) Token: 0x0600B70E RID: 46862 RVA: 0x001074CC File Offset: 0x001056CC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D6C RID: 3436
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170087F0 RID: 34800
			// (set) Token: 0x0600B710 RID: 46864 RVA: 0x001074EC File Offset: 0x001056EC
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DistributionGroupIdParameter(value) : null);
				}
			}

			// Token: 0x170087F1 RID: 34801
			// (set) Token: 0x0600B711 RID: 46865 RVA: 0x0010750A File Offset: 0x0010570A
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x170087F2 RID: 34802
			// (set) Token: 0x0600B712 RID: 46866 RVA: 0x0010751D File Offset: 0x0010571D
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x170087F3 RID: 34803
			// (set) Token: 0x0600B713 RID: 46867 RVA: 0x00107530 File Offset: 0x00105730
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawAcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["RawAcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x170087F4 RID: 34804
			// (set) Token: 0x0600B714 RID: 46868 RVA: 0x00107543 File Offset: 0x00105743
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawBypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["RawBypassModerationFrom"] = value;
				}
			}

			// Token: 0x170087F5 RID: 34805
			// (set) Token: 0x0600B715 RID: 46869 RVA: 0x00107556 File Offset: 0x00105756
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawRejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RawRejectMessagesFrom"] = value;
				}
			}

			// Token: 0x170087F6 RID: 34806
			// (set) Token: 0x0600B716 RID: 46870 RVA: 0x00107569 File Offset: 0x00105769
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> RawGrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["RawGrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x170087F7 RID: 34807
			// (set) Token: 0x0600B717 RID: 46871 RVA: 0x0010757C File Offset: 0x0010577C
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<ModeratorIDParameter>> RawModeratedBy
			{
				set
				{
					base.PowerSharpParameters["RawModeratedBy"] = value;
				}
			}

			// Token: 0x170087F8 RID: 34808
			// (set) Token: 0x0600B718 RID: 46872 RVA: 0x0010758F File Offset: 0x0010578F
			public virtual MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>> RawMembers
			{
				set
				{
					base.PowerSharpParameters["RawMembers"] = value;
				}
			}

			// Token: 0x170087F9 RID: 34809
			// (set) Token: 0x0600B719 RID: 46873 RVA: 0x001075A2 File Offset: 0x001057A2
			public virtual RecipientWithAdUserIdParameter<RecipientIdParameter> RawManagedBy
			{
				set
				{
					base.PowerSharpParameters["RawManagedBy"] = value;
				}
			}

			// Token: 0x170087FA RID: 34810
			// (set) Token: 0x0600B71A RID: 46874 RVA: 0x001075B5 File Offset: 0x001057B5
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> RawCoManagedBy
			{
				set
				{
					base.PowerSharpParameters["RawCoManagedBy"] = value;
				}
			}

			// Token: 0x170087FB RID: 34811
			// (set) Token: 0x0600B71B RID: 46875 RVA: 0x001075C8 File Offset: 0x001057C8
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x170087FC RID: 34812
			// (set) Token: 0x0600B71C RID: 46876 RVA: 0x001075E0 File Offset: 0x001057E0
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x170087FD RID: 34813
			// (set) Token: 0x0600B71D RID: 46877 RVA: 0x001075F3 File Offset: 0x001057F3
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x170087FE RID: 34814
			// (set) Token: 0x0600B71E RID: 46878 RVA: 0x00107606 File Offset: 0x00105806
			public virtual GroupType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x170087FF RID: 34815
			// (set) Token: 0x0600B71F RID: 46879 RVA: 0x0010761E File Offset: 0x0010581E
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17008800 RID: 34816
			// (set) Token: 0x0600B720 RID: 46880 RVA: 0x00107636 File Offset: 0x00105836
			public virtual string ExpansionServer
			{
				set
				{
					base.PowerSharpParameters["ExpansionServer"] = value;
				}
			}

			// Token: 0x17008801 RID: 34817
			// (set) Token: 0x0600B721 RID: 46881 RVA: 0x00107649 File Offset: 0x00105849
			public virtual MultiValuedProperty<GeneralRecipientIdParameter> ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = value;
				}
			}

			// Token: 0x17008802 RID: 34818
			// (set) Token: 0x0600B722 RID: 46882 RVA: 0x0010765C File Offset: 0x0010585C
			public virtual MemberUpdateType MemberJoinRestriction
			{
				set
				{
					base.PowerSharpParameters["MemberJoinRestriction"] = value;
				}
			}

			// Token: 0x17008803 RID: 34819
			// (set) Token: 0x0600B723 RID: 46883 RVA: 0x00107674 File Offset: 0x00105874
			public virtual MemberUpdateType MemberDepartRestriction
			{
				set
				{
					base.PowerSharpParameters["MemberDepartRestriction"] = value;
				}
			}

			// Token: 0x17008804 RID: 34820
			// (set) Token: 0x0600B724 RID: 46884 RVA: 0x0010768C File Offset: 0x0010588C
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x17008805 RID: 34821
			// (set) Token: 0x0600B725 RID: 46885 RVA: 0x001076A4 File Offset: 0x001058A4
			public virtual SwitchParameter RoomList
			{
				set
				{
					base.PowerSharpParameters["RoomList"] = value;
				}
			}

			// Token: 0x17008806 RID: 34822
			// (set) Token: 0x0600B726 RID: 46886 RVA: 0x001076BC File Offset: 0x001058BC
			public virtual SwitchParameter IgnoreNamingPolicy
			{
				set
				{
					base.PowerSharpParameters["IgnoreNamingPolicy"] = value;
				}
			}

			// Token: 0x17008807 RID: 34823
			// (set) Token: 0x0600B727 RID: 46887 RVA: 0x001076D4 File Offset: 0x001058D4
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17008808 RID: 34824
			// (set) Token: 0x0600B728 RID: 46888 RVA: 0x001076E7 File Offset: 0x001058E7
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17008809 RID: 34825
			// (set) Token: 0x0600B729 RID: 46889 RVA: 0x001076FA File Offset: 0x001058FA
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x1700880A RID: 34826
			// (set) Token: 0x0600B72A RID: 46890 RVA: 0x0010770D File Offset: 0x0010590D
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700880B RID: 34827
			// (set) Token: 0x0600B72B RID: 46891 RVA: 0x0010772B File Offset: 0x0010592B
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x1700880C RID: 34828
			// (set) Token: 0x0600B72C RID: 46892 RVA: 0x0010773E File Offset: 0x0010593E
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700880D RID: 34829
			// (set) Token: 0x0600B72D RID: 46893 RVA: 0x00107751 File Offset: 0x00105951
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x1700880E RID: 34830
			// (set) Token: 0x0600B72E RID: 46894 RVA: 0x00107764 File Offset: 0x00105964
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x1700880F RID: 34831
			// (set) Token: 0x0600B72F RID: 46895 RVA: 0x00107777 File Offset: 0x00105977
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17008810 RID: 34832
			// (set) Token: 0x0600B730 RID: 46896 RVA: 0x0010778A File Offset: 0x0010598A
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17008811 RID: 34833
			// (set) Token: 0x0600B731 RID: 46897 RVA: 0x0010779D File Offset: 0x0010599D
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x17008812 RID: 34834
			// (set) Token: 0x0600B732 RID: 46898 RVA: 0x001077B5 File Offset: 0x001059B5
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17008813 RID: 34835
			// (set) Token: 0x0600B733 RID: 46899 RVA: 0x001077CD File Offset: 0x001059CD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008814 RID: 34836
			// (set) Token: 0x0600B734 RID: 46900 RVA: 0x001077E0 File Offset: 0x001059E0
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17008815 RID: 34837
			// (set) Token: 0x0600B735 RID: 46901 RVA: 0x001077F8 File Offset: 0x001059F8
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17008816 RID: 34838
			// (set) Token: 0x0600B736 RID: 46902 RVA: 0x0010780B File Offset: 0x00105A0B
			public virtual RecipientDisplayType? RecipientDisplayType
			{
				set
				{
					base.PowerSharpParameters["RecipientDisplayType"] = value;
				}
			}

			// Token: 0x17008817 RID: 34839
			// (set) Token: 0x0600B737 RID: 46903 RVA: 0x00107823 File Offset: 0x00105A23
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17008818 RID: 34840
			// (set) Token: 0x0600B738 RID: 46904 RVA: 0x0010783B File Offset: 0x00105A3B
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17008819 RID: 34841
			// (set) Token: 0x0600B739 RID: 46905 RVA: 0x00107853 File Offset: 0x00105A53
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x1700881A RID: 34842
			// (set) Token: 0x0600B73A RID: 46906 RVA: 0x00107866 File Offset: 0x00105A66
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x1700881B RID: 34843
			// (set) Token: 0x0600B73B RID: 46907 RVA: 0x0010787E File Offset: 0x00105A7E
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x1700881C RID: 34844
			// (set) Token: 0x0600B73C RID: 46908 RVA: 0x00107891 File Offset: 0x00105A91
			public virtual bool IsHierarchicalGroup
			{
				set
				{
					base.PowerSharpParameters["IsHierarchicalGroup"] = value;
				}
			}

			// Token: 0x1700881D RID: 34845
			// (set) Token: 0x0600B73D RID: 46909 RVA: 0x001078A9 File Offset: 0x00105AA9
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x1700881E RID: 34846
			// (set) Token: 0x0600B73E RID: 46910 RVA: 0x001078BC File Offset: 0x00105ABC
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x1700881F RID: 34847
			// (set) Token: 0x0600B73F RID: 46911 RVA: 0x001078D4 File Offset: 0x00105AD4
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17008820 RID: 34848
			// (set) Token: 0x0600B740 RID: 46912 RVA: 0x001078E7 File Offset: 0x00105AE7
			public virtual bool ExcludedFromBackSync
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromBackSync"] = value;
				}
			}

			// Token: 0x17008821 RID: 34849
			// (set) Token: 0x0600B741 RID: 46913 RVA: 0x001078FF File Offset: 0x00105AFF
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008822 RID: 34850
			// (set) Token: 0x0600B742 RID: 46914 RVA: 0x00107912 File Offset: 0x00105B12
			public virtual bool BypassNestedModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["BypassNestedModerationEnabled"] = value;
				}
			}

			// Token: 0x17008823 RID: 34851
			// (set) Token: 0x0600B743 RID: 46915 RVA: 0x0010792A File Offset: 0x00105B2A
			public virtual bool ReportToManagerEnabled
			{
				set
				{
					base.PowerSharpParameters["ReportToManagerEnabled"] = value;
				}
			}

			// Token: 0x17008824 RID: 34852
			// (set) Token: 0x0600B744 RID: 46916 RVA: 0x00107942 File Offset: 0x00105B42
			public virtual bool ReportToOriginatorEnabled
			{
				set
				{
					base.PowerSharpParameters["ReportToOriginatorEnabled"] = value;
				}
			}

			// Token: 0x17008825 RID: 34853
			// (set) Token: 0x0600B745 RID: 46917 RVA: 0x0010795A File Offset: 0x00105B5A
			public virtual bool SendOofMessageToOriginatorEnabled
			{
				set
				{
					base.PowerSharpParameters["SendOofMessageToOriginatorEnabled"] = value;
				}
			}

			// Token: 0x17008826 RID: 34854
			// (set) Token: 0x0600B746 RID: 46918 RVA: 0x00107972 File Offset: 0x00105B72
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008827 RID: 34855
			// (set) Token: 0x0600B747 RID: 46919 RVA: 0x00107985 File Offset: 0x00105B85
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17008828 RID: 34856
			// (set) Token: 0x0600B748 RID: 46920 RVA: 0x00107998 File Offset: 0x00105B98
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17008829 RID: 34857
			// (set) Token: 0x0600B749 RID: 46921 RVA: 0x001079AB File Offset: 0x00105BAB
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x1700882A RID: 34858
			// (set) Token: 0x0600B74A RID: 46922 RVA: 0x001079BE File Offset: 0x00105BBE
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x1700882B RID: 34859
			// (set) Token: 0x0600B74B RID: 46923 RVA: 0x001079D1 File Offset: 0x00105BD1
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x1700882C RID: 34860
			// (set) Token: 0x0600B74C RID: 46924 RVA: 0x001079E4 File Offset: 0x00105BE4
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x1700882D RID: 34861
			// (set) Token: 0x0600B74D RID: 46925 RVA: 0x001079F7 File Offset: 0x00105BF7
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x1700882E RID: 34862
			// (set) Token: 0x0600B74E RID: 46926 RVA: 0x00107A0A File Offset: 0x00105C0A
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x1700882F RID: 34863
			// (set) Token: 0x0600B74F RID: 46927 RVA: 0x00107A1D File Offset: 0x00105C1D
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17008830 RID: 34864
			// (set) Token: 0x0600B750 RID: 46928 RVA: 0x00107A30 File Offset: 0x00105C30
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17008831 RID: 34865
			// (set) Token: 0x0600B751 RID: 46929 RVA: 0x00107A43 File Offset: 0x00105C43
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17008832 RID: 34866
			// (set) Token: 0x0600B752 RID: 46930 RVA: 0x00107A56 File Offset: 0x00105C56
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17008833 RID: 34867
			// (set) Token: 0x0600B753 RID: 46931 RVA: 0x00107A69 File Offset: 0x00105C69
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17008834 RID: 34868
			// (set) Token: 0x0600B754 RID: 46932 RVA: 0x00107A7C File Offset: 0x00105C7C
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17008835 RID: 34869
			// (set) Token: 0x0600B755 RID: 46933 RVA: 0x00107A8F File Offset: 0x00105C8F
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17008836 RID: 34870
			// (set) Token: 0x0600B756 RID: 46934 RVA: 0x00107AA2 File Offset: 0x00105CA2
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17008837 RID: 34871
			// (set) Token: 0x0600B757 RID: 46935 RVA: 0x00107AB5 File Offset: 0x00105CB5
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17008838 RID: 34872
			// (set) Token: 0x0600B758 RID: 46936 RVA: 0x00107AC8 File Offset: 0x00105CC8
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17008839 RID: 34873
			// (set) Token: 0x0600B759 RID: 46937 RVA: 0x00107ADB File Offset: 0x00105CDB
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x1700883A RID: 34874
			// (set) Token: 0x0600B75A RID: 46938 RVA: 0x00107AEE File Offset: 0x00105CEE
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x1700883B RID: 34875
			// (set) Token: 0x0600B75B RID: 46939 RVA: 0x00107B01 File Offset: 0x00105D01
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700883C RID: 34876
			// (set) Token: 0x0600B75C RID: 46940 RVA: 0x00107B14 File Offset: 0x00105D14
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x1700883D RID: 34877
			// (set) Token: 0x0600B75D RID: 46941 RVA: 0x00107B27 File Offset: 0x00105D27
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x1700883E RID: 34878
			// (set) Token: 0x0600B75E RID: 46942 RVA: 0x00107B3F File Offset: 0x00105D3F
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x1700883F RID: 34879
			// (set) Token: 0x0600B75F RID: 46943 RVA: 0x00107B57 File Offset: 0x00105D57
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x17008840 RID: 34880
			// (set) Token: 0x0600B760 RID: 46944 RVA: 0x00107B6F File Offset: 0x00105D6F
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17008841 RID: 34881
			// (set) Token: 0x0600B761 RID: 46945 RVA: 0x00107B87 File Offset: 0x00105D87
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x17008842 RID: 34882
			// (set) Token: 0x0600B762 RID: 46946 RVA: 0x00107B9F File Offset: 0x00105D9F
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008843 RID: 34883
			// (set) Token: 0x0600B763 RID: 46947 RVA: 0x00107BB7 File Offset: 0x00105DB7
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17008844 RID: 34884
			// (set) Token: 0x0600B764 RID: 46948 RVA: 0x00107BCF File Offset: 0x00105DCF
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x17008845 RID: 34885
			// (set) Token: 0x0600B765 RID: 46949 RVA: 0x00107BE2 File Offset: 0x00105DE2
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17008846 RID: 34886
			// (set) Token: 0x0600B766 RID: 46950 RVA: 0x00107BFA File Offset: 0x00105DFA
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x17008847 RID: 34887
			// (set) Token: 0x0600B767 RID: 46951 RVA: 0x00107C0D File Offset: 0x00105E0D
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x17008848 RID: 34888
			// (set) Token: 0x0600B768 RID: 46952 RVA: 0x00107C25 File Offset: 0x00105E25
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x17008849 RID: 34889
			// (set) Token: 0x0600B769 RID: 46953 RVA: 0x00107C38 File Offset: 0x00105E38
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x1700884A RID: 34890
			// (set) Token: 0x0600B76A RID: 46954 RVA: 0x00107C4B File Offset: 0x00105E4B
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700884B RID: 34891
			// (set) Token: 0x0600B76B RID: 46955 RVA: 0x00107C5E File Offset: 0x00105E5E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700884C RID: 34892
			// (set) Token: 0x0600B76C RID: 46956 RVA: 0x00107C76 File Offset: 0x00105E76
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700884D RID: 34893
			// (set) Token: 0x0600B76D RID: 46957 RVA: 0x00107C8E File Offset: 0x00105E8E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700884E RID: 34894
			// (set) Token: 0x0600B76E RID: 46958 RVA: 0x00107CA6 File Offset: 0x00105EA6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700884F RID: 34895
			// (set) Token: 0x0600B76F RID: 46959 RVA: 0x00107CBE File Offset: 0x00105EBE
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
