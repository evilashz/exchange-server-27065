using System;
using System.Globalization;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C90 RID: 3216
	public class SetMailboxCommand : SyntheticCommandWithPipelineInputNoOutput<Mailbox>
	{
		// Token: 0x06009EB5 RID: 40629 RVA: 0x000E6108 File Offset: 0x000E4308
		private SetMailboxCommand() : base("Set-Mailbox")
		{
		}

		// Token: 0x06009EB6 RID: 40630 RVA: 0x000E6115 File Offset: 0x000E4315
		public SetMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009EB7 RID: 40631 RVA: 0x000E6124 File Offset: 0x000E4324
		public virtual SetMailboxCommand SetParameters(SetMailboxCommand.RemoveAggregatedMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009EB8 RID: 40632 RVA: 0x000E612E File Offset: 0x000E432E
		public virtual SetMailboxCommand SetParameters(SetMailboxCommand.AddAggregatedMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009EB9 RID: 40633 RVA: 0x000E6138 File Offset: 0x000E4338
		public virtual SetMailboxCommand SetParameters(SetMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009EBA RID: 40634 RVA: 0x000E6142 File Offset: 0x000E4342
		public virtual SetMailboxCommand SetParameters(SetMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C91 RID: 3217
		public class RemoveAggregatedMailboxParameters : ParametersBase
		{
			// Token: 0x1700714E RID: 29006
			// (set) Token: 0x06009EBB RID: 40635 RVA: 0x000E614C File Offset: 0x000E434C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700714F RID: 29007
			// (set) Token: 0x06009EBC RID: 40636 RVA: 0x000E616A File Offset: 0x000E436A
			public virtual SwitchParameter RemoveAggregatedAccount
			{
				set
				{
					base.PowerSharpParameters["RemoveAggregatedAccount"] = value;
				}
			}

			// Token: 0x17007150 RID: 29008
			// (set) Token: 0x06009EBD RID: 40637 RVA: 0x000E6182 File Offset: 0x000E4382
			public virtual Guid AggregatedMailboxGuid
			{
				set
				{
					base.PowerSharpParameters["AggregatedMailboxGuid"] = value;
				}
			}

			// Token: 0x17007151 RID: 29009
			// (set) Token: 0x06009EBE RID: 40638 RVA: 0x000E619A File Offset: 0x000E439A
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007152 RID: 29010
			// (set) Token: 0x06009EBF RID: 40639 RVA: 0x000E61B8 File Offset: 0x000E43B8
			public virtual bool EnableRoomMailboxAccount
			{
				set
				{
					base.PowerSharpParameters["EnableRoomMailboxAccount"] = value;
				}
			}

			// Token: 0x17007153 RID: 29011
			// (set) Token: 0x06009EC0 RID: 40640 RVA: 0x000E61D0 File Offset: 0x000E43D0
			public virtual SecureString RoomMailboxPassword
			{
				set
				{
					base.PowerSharpParameters["RoomMailboxPassword"] = value;
				}
			}

			// Token: 0x17007154 RID: 29012
			// (set) Token: 0x06009EC1 RID: 40641 RVA: 0x000E61E3 File Offset: 0x000E43E3
			public virtual SwitchParameter SkipMailboxProvisioningConstraintValidation
			{
				set
				{
					base.PowerSharpParameters["SkipMailboxProvisioningConstraintValidation"] = value;
				}
			}

			// Token: 0x17007155 RID: 29013
			// (set) Token: 0x06009EC2 RID: 40642 RVA: 0x000E61FB File Offset: 0x000E43FB
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007156 RID: 29014
			// (set) Token: 0x06009EC3 RID: 40643 RVA: 0x000E620E File Offset: 0x000E440E
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007157 RID: 29015
			// (set) Token: 0x06009EC4 RID: 40644 RVA: 0x000E6221 File Offset: 0x000E4421
			public virtual SwitchParameter EvictLiveId
			{
				set
				{
					base.PowerSharpParameters["EvictLiveId"] = value;
				}
			}

			// Token: 0x17007158 RID: 29016
			// (set) Token: 0x06009EC5 RID: 40645 RVA: 0x000E6239 File Offset: 0x000E4439
			public virtual bool RequireSecretQA
			{
				set
				{
					base.PowerSharpParameters["RequireSecretQA"] = value;
				}
			}

			// Token: 0x17007159 RID: 29017
			// (set) Token: 0x06009EC6 RID: 40646 RVA: 0x000E6251 File Offset: 0x000E4451
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x1700715A RID: 29018
			// (set) Token: 0x06009EC7 RID: 40647 RVA: 0x000E6264 File Offset: 0x000E4464
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x1700715B RID: 29019
			// (set) Token: 0x06009EC8 RID: 40648 RVA: 0x000E6277 File Offset: 0x000E4477
			public virtual Unlimited<EnhancedTimeSpan> LitigationHoldDuration
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDuration"] = value;
				}
			}

			// Token: 0x1700715C RID: 29020
			// (set) Token: 0x06009EC9 RID: 40649 RVA: 0x000E628F File Offset: 0x000E448F
			public virtual bool UMDataStorage
			{
				set
				{
					base.PowerSharpParameters["UMDataStorage"] = value;
				}
			}

			// Token: 0x1700715D RID: 29021
			// (set) Token: 0x06009ECA RID: 40650 RVA: 0x000E62A7 File Offset: 0x000E44A7
			public virtual bool UMGrammar
			{
				set
				{
					base.PowerSharpParameters["UMGrammar"] = value;
				}
			}

			// Token: 0x1700715E RID: 29022
			// (set) Token: 0x06009ECB RID: 40651 RVA: 0x000E62BF File Offset: 0x000E44BF
			public virtual bool OABGen
			{
				set
				{
					base.PowerSharpParameters["OABGen"] = value;
				}
			}

			// Token: 0x1700715F RID: 29023
			// (set) Token: 0x06009ECC RID: 40652 RVA: 0x000E62D7 File Offset: 0x000E44D7
			public virtual bool GMGen
			{
				set
				{
					base.PowerSharpParameters["GMGen"] = value;
				}
			}

			// Token: 0x17007160 RID: 29024
			// (set) Token: 0x06009ECD RID: 40653 RVA: 0x000E62EF File Offset: 0x000E44EF
			public virtual bool ClientExtensions
			{
				set
				{
					base.PowerSharpParameters["ClientExtensions"] = value;
				}
			}

			// Token: 0x17007161 RID: 29025
			// (set) Token: 0x06009ECE RID: 40654 RVA: 0x000E6307 File Offset: 0x000E4507
			public virtual bool MailRouting
			{
				set
				{
					base.PowerSharpParameters["MailRouting"] = value;
				}
			}

			// Token: 0x17007162 RID: 29026
			// (set) Token: 0x06009ECF RID: 40655 RVA: 0x000E631F File Offset: 0x000E451F
			public virtual bool Management
			{
				set
				{
					base.PowerSharpParameters["Management"] = value;
				}
			}

			// Token: 0x17007163 RID: 29027
			// (set) Token: 0x06009ED0 RID: 40656 RVA: 0x000E6337 File Offset: 0x000E4537
			public virtual bool TenantUpgrade
			{
				set
				{
					base.PowerSharpParameters["TenantUpgrade"] = value;
				}
			}

			// Token: 0x17007164 RID: 29028
			// (set) Token: 0x06009ED1 RID: 40657 RVA: 0x000E634F File Offset: 0x000E454F
			public virtual bool Migration
			{
				set
				{
					base.PowerSharpParameters["Migration"] = value;
				}
			}

			// Token: 0x17007165 RID: 29029
			// (set) Token: 0x06009ED2 RID: 40658 RVA: 0x000E6367 File Offset: 0x000E4567
			public virtual bool MessageTracking
			{
				set
				{
					base.PowerSharpParameters["MessageTracking"] = value;
				}
			}

			// Token: 0x17007166 RID: 29030
			// (set) Token: 0x06009ED3 RID: 40659 RVA: 0x000E637F File Offset: 0x000E457F
			public virtual bool OMEncryption
			{
				set
				{
					base.PowerSharpParameters["OMEncryption"] = value;
				}
			}

			// Token: 0x17007167 RID: 29031
			// (set) Token: 0x06009ED4 RID: 40660 RVA: 0x000E6397 File Offset: 0x000E4597
			public virtual bool PstProvider
			{
				set
				{
					base.PowerSharpParameters["PstProvider"] = value;
				}
			}

			// Token: 0x17007168 RID: 29032
			// (set) Token: 0x06009ED5 RID: 40661 RVA: 0x000E63AF File Offset: 0x000E45AF
			public virtual bool SuiteServiceStorage
			{
				set
				{
					base.PowerSharpParameters["SuiteServiceStorage"] = value;
				}
			}

			// Token: 0x17007169 RID: 29033
			// (set) Token: 0x06009ED6 RID: 40662 RVA: 0x000E63C7 File Offset: 0x000E45C7
			public virtual SecureString OldPassword
			{
				set
				{
					base.PowerSharpParameters["OldPassword"] = value;
				}
			}

			// Token: 0x1700716A RID: 29034
			// (set) Token: 0x06009ED7 RID: 40663 RVA: 0x000E63DA File Offset: 0x000E45DA
			public virtual SecureString NewPassword
			{
				set
				{
					base.PowerSharpParameters["NewPassword"] = value;
				}
			}

			// Token: 0x1700716B RID: 29035
			// (set) Token: 0x06009ED8 RID: 40664 RVA: 0x000E63ED File Offset: 0x000E45ED
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x1700716C RID: 29036
			// (set) Token: 0x06009ED9 RID: 40665 RVA: 0x000E6400 File Offset: 0x000E4600
			public virtual string QueryBaseDN
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDN"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700716D RID: 29037
			// (set) Token: 0x06009EDA RID: 40666 RVA: 0x000E641E File Offset: 0x000E461E
			public virtual string DefaultPublicFolderMailbox
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMailbox"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700716E RID: 29038
			// (set) Token: 0x06009EDB RID: 40667 RVA: 0x000E643C File Offset: 0x000E463C
			public virtual int? MailboxMessagesPerFolderCountWarningQuota
			{
				set
				{
					base.PowerSharpParameters["MailboxMessagesPerFolderCountWarningQuota"] = value;
				}
			}

			// Token: 0x1700716F RID: 29039
			// (set) Token: 0x06009EDC RID: 40668 RVA: 0x000E6454 File Offset: 0x000E4654
			public virtual int? MailboxMessagesPerFolderCountReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["MailboxMessagesPerFolderCountReceiveQuota"] = value;
				}
			}

			// Token: 0x17007170 RID: 29040
			// (set) Token: 0x06009EDD RID: 40669 RVA: 0x000E646C File Offset: 0x000E466C
			public virtual int? DumpsterMessagesPerFolderCountWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DumpsterMessagesPerFolderCountWarningQuota"] = value;
				}
			}

			// Token: 0x17007171 RID: 29041
			// (set) Token: 0x06009EDE RID: 40670 RVA: 0x000E6484 File Offset: 0x000E4684
			public virtual int? DumpsterMessagesPerFolderCountReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["DumpsterMessagesPerFolderCountReceiveQuota"] = value;
				}
			}

			// Token: 0x17007172 RID: 29042
			// (set) Token: 0x06009EDF RID: 40671 RVA: 0x000E649C File Offset: 0x000E469C
			public virtual int? FolderHierarchyChildrenCountWarningQuota
			{
				set
				{
					base.PowerSharpParameters["FolderHierarchyChildrenCountWarningQuota"] = value;
				}
			}

			// Token: 0x17007173 RID: 29043
			// (set) Token: 0x06009EE0 RID: 40672 RVA: 0x000E64B4 File Offset: 0x000E46B4
			public virtual int? FolderHierarchyChildrenCountReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["FolderHierarchyChildrenCountReceiveQuota"] = value;
				}
			}

			// Token: 0x17007174 RID: 29044
			// (set) Token: 0x06009EE1 RID: 40673 RVA: 0x000E64CC File Offset: 0x000E46CC
			public virtual int? FolderHierarchyDepthWarningQuota
			{
				set
				{
					base.PowerSharpParameters["FolderHierarchyDepthWarningQuota"] = value;
				}
			}

			// Token: 0x17007175 RID: 29045
			// (set) Token: 0x06009EE2 RID: 40674 RVA: 0x000E64E4 File Offset: 0x000E46E4
			public virtual int? FolderHierarchyDepthReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["FolderHierarchyDepthReceiveQuota"] = value;
				}
			}

			// Token: 0x17007176 RID: 29046
			// (set) Token: 0x06009EE3 RID: 40675 RVA: 0x000E64FC File Offset: 0x000E46FC
			public virtual int? FoldersCountWarningQuota
			{
				set
				{
					base.PowerSharpParameters["FoldersCountWarningQuota"] = value;
				}
			}

			// Token: 0x17007177 RID: 29047
			// (set) Token: 0x06009EE4 RID: 40676 RVA: 0x000E6514 File Offset: 0x000E4714
			public virtual int? FoldersCountReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["FoldersCountReceiveQuota"] = value;
				}
			}

			// Token: 0x17007178 RID: 29048
			// (set) Token: 0x06009EE5 RID: 40677 RVA: 0x000E652C File Offset: 0x000E472C
			public virtual int? ExtendedPropertiesCountQuota
			{
				set
				{
					base.PowerSharpParameters["ExtendedPropertiesCountQuota"] = value;
				}
			}

			// Token: 0x17007179 RID: 29049
			// (set) Token: 0x06009EE6 RID: 40678 RVA: 0x000E6544 File Offset: 0x000E4744
			public virtual Guid? MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x1700717A RID: 29050
			// (set) Token: 0x06009EE7 RID: 40679 RVA: 0x000E655C File Offset: 0x000E475C
			public virtual CrossTenantObjectId UnifiedMailbox
			{
				set
				{
					base.PowerSharpParameters["UnifiedMailbox"] = value;
				}
			}

			// Token: 0x1700717B RID: 29051
			// (set) Token: 0x06009EE8 RID: 40680 RVA: 0x000E656F File Offset: 0x000E476F
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x1700717C RID: 29052
			// (set) Token: 0x06009EE9 RID: 40681 RVA: 0x000E6587 File Offset: 0x000E4787
			public virtual bool MessageCopyForSentAsEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageCopyForSentAsEnabled"] = value;
				}
			}

			// Token: 0x1700717D RID: 29053
			// (set) Token: 0x06009EEA RID: 40682 RVA: 0x000E659F File Offset: 0x000E479F
			public virtual bool MessageCopyForSendOnBehalfEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageCopyForSendOnBehalfEnabled"] = value;
				}
			}

			// Token: 0x1700717E RID: 29054
			// (set) Token: 0x06009EEB RID: 40683 RVA: 0x000E65B7 File Offset: 0x000E47B7
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700717F RID: 29055
			// (set) Token: 0x06009EEC RID: 40684 RVA: 0x000E65CF File Offset: 0x000E47CF
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17007180 RID: 29056
			// (set) Token: 0x06009EED RID: 40685 RVA: 0x000E65E7 File Offset: 0x000E47E7
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17007181 RID: 29057
			// (set) Token: 0x06009EEE RID: 40686 RVA: 0x000E65FF File Offset: 0x000E47FF
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007182 RID: 29058
			// (set) Token: 0x06009EEF RID: 40687 RVA: 0x000E661D File Offset: 0x000E481D
			public virtual string ForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17007183 RID: 29059
			// (set) Token: 0x06009EF0 RID: 40688 RVA: 0x000E663B File Offset: 0x000E483B
			public virtual OfflineAddressBookIdParameter OfflineAddressBook
			{
				set
				{
					base.PowerSharpParameters["OfflineAddressBook"] = value;
				}
			}

			// Token: 0x17007184 RID: 29060
			// (set) Token: 0x06009EF1 RID: 40689 RVA: 0x000E664E File Offset: 0x000E484E
			public virtual string LinkedMasterAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedMasterAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17007185 RID: 29061
			// (set) Token: 0x06009EF2 RID: 40690 RVA: 0x000E666C File Offset: 0x000E486C
			public virtual string LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x17007186 RID: 29062
			// (set) Token: 0x06009EF3 RID: 40691 RVA: 0x000E667F File Offset: 0x000E487F
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x17007187 RID: 29063
			// (set) Token: 0x06009EF4 RID: 40692 RVA: 0x000E6692 File Offset: 0x000E4892
			public virtual ConvertibleMailboxSubType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x17007188 RID: 29064
			// (set) Token: 0x06009EF5 RID: 40693 RVA: 0x000E66AA File Offset: 0x000E48AA
			public virtual SwitchParameter ApplyMandatoryProperties
			{
				set
				{
					base.PowerSharpParameters["ApplyMandatoryProperties"] = value;
				}
			}

			// Token: 0x17007189 RID: 29065
			// (set) Token: 0x06009EF6 RID: 40694 RVA: 0x000E66C2 File Offset: 0x000E48C2
			public virtual SwitchParameter RemoveManagedFolderAndPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoveManagedFolderAndPolicy"] = value;
				}
			}

			// Token: 0x1700718A RID: 29066
			// (set) Token: 0x06009EF7 RID: 40695 RVA: 0x000E66DA File Offset: 0x000E48DA
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700718B RID: 29067
			// (set) Token: 0x06009EF8 RID: 40696 RVA: 0x000E66F8 File Offset: 0x000E48F8
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700718C RID: 29068
			// (set) Token: 0x06009EF9 RID: 40697 RVA: 0x000E6716 File Offset: 0x000E4916
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700718D RID: 29069
			// (set) Token: 0x06009EFA RID: 40698 RVA: 0x000E6734 File Offset: 0x000E4934
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x1700718E RID: 29070
			// (set) Token: 0x06009EFB RID: 40699 RVA: 0x000E6747 File Offset: 0x000E4947
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700718F RID: 29071
			// (set) Token: 0x06009EFC RID: 40700 RVA: 0x000E6765 File Offset: 0x000E4965
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17007190 RID: 29072
			// (set) Token: 0x06009EFD RID: 40701 RVA: 0x000E677D File Offset: 0x000E497D
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17007191 RID: 29073
			// (set) Token: 0x06009EFE RID: 40702 RVA: 0x000E6795 File Offset: 0x000E4995
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17007192 RID: 29074
			// (set) Token: 0x06009EFF RID: 40703 RVA: 0x000E67A8 File Offset: 0x000E49A8
			public virtual SwitchParameter BypassLiveId
			{
				set
				{
					base.PowerSharpParameters["BypassLiveId"] = value;
				}
			}

			// Token: 0x17007193 RID: 29075
			// (set) Token: 0x06009F00 RID: 40704 RVA: 0x000E67C0 File Offset: 0x000E49C0
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x17007194 RID: 29076
			// (set) Token: 0x06009F01 RID: 40705 RVA: 0x000E67D3 File Offset: 0x000E49D3
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007195 RID: 29077
			// (set) Token: 0x06009F02 RID: 40706 RVA: 0x000E67E6 File Offset: 0x000E49E6
			public virtual string FederatedIdentity
			{
				set
				{
					base.PowerSharpParameters["FederatedIdentity"] = value;
				}
			}

			// Token: 0x17007196 RID: 29078
			// (set) Token: 0x06009F03 RID: 40707 RVA: 0x000E67F9 File Offset: 0x000E49F9
			public virtual string SecondaryAddress
			{
				set
				{
					base.PowerSharpParameters["SecondaryAddress"] = value;
				}
			}

			// Token: 0x17007197 RID: 29079
			// (set) Token: 0x06009F04 RID: 40708 RVA: 0x000E680C File Offset: 0x000E4A0C
			public virtual string SecondaryDialPlan
			{
				set
				{
					base.PowerSharpParameters["SecondaryDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007198 RID: 29080
			// (set) Token: 0x06009F05 RID: 40709 RVA: 0x000E682A File Offset: 0x000E4A2A
			public virtual SwitchParameter RemovePicture
			{
				set
				{
					base.PowerSharpParameters["RemovePicture"] = value;
				}
			}

			// Token: 0x17007199 RID: 29081
			// (set) Token: 0x06009F06 RID: 40710 RVA: 0x000E6842 File Offset: 0x000E4A42
			public virtual SwitchParameter RemoveSpokenName
			{
				set
				{
					base.PowerSharpParameters["RemoveSpokenName"] = value;
				}
			}

			// Token: 0x1700719A RID: 29082
			// (set) Token: 0x06009F07 RID: 40711 RVA: 0x000E685A File Offset: 0x000E4A5A
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x1700719B RID: 29083
			// (set) Token: 0x06009F08 RID: 40712 RVA: 0x000E686D File Offset: 0x000E4A6D
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x1700719C RID: 29084
			// (set) Token: 0x06009F09 RID: 40713 RVA: 0x000E6880 File Offset: 0x000E4A80
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x1700719D RID: 29085
			// (set) Token: 0x06009F0A RID: 40714 RVA: 0x000E6893 File Offset: 0x000E4A93
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700719E RID: 29086
			// (set) Token: 0x06009F0B RID: 40715 RVA: 0x000E68B1 File Offset: 0x000E4AB1
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x1700719F RID: 29087
			// (set) Token: 0x06009F0C RID: 40716 RVA: 0x000E68C4 File Offset: 0x000E4AC4
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170071A0 RID: 29088
			// (set) Token: 0x06009F0D RID: 40717 RVA: 0x000E68D7 File Offset: 0x000E4AD7
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x170071A1 RID: 29089
			// (set) Token: 0x06009F0E RID: 40718 RVA: 0x000E68EA File Offset: 0x000E4AEA
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x170071A2 RID: 29090
			// (set) Token: 0x06009F0F RID: 40719 RVA: 0x000E68FD File Offset: 0x000E4AFD
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x170071A3 RID: 29091
			// (set) Token: 0x06009F10 RID: 40720 RVA: 0x000E6910 File Offset: 0x000E4B10
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x170071A4 RID: 29092
			// (set) Token: 0x06009F11 RID: 40721 RVA: 0x000E6923 File Offset: 0x000E4B23
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x170071A5 RID: 29093
			// (set) Token: 0x06009F12 RID: 40722 RVA: 0x000E693B File Offset: 0x000E4B3B
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170071A6 RID: 29094
			// (set) Token: 0x06009F13 RID: 40723 RVA: 0x000E6953 File Offset: 0x000E4B53
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170071A7 RID: 29095
			// (set) Token: 0x06009F14 RID: 40724 RVA: 0x000E6966 File Offset: 0x000E4B66
			public virtual bool UseDatabaseRetentionDefaults
			{
				set
				{
					base.PowerSharpParameters["UseDatabaseRetentionDefaults"] = value;
				}
			}

			// Token: 0x170071A8 RID: 29096
			// (set) Token: 0x06009F15 RID: 40725 RVA: 0x000E697E File Offset: 0x000E4B7E
			public virtual bool RetainDeletedItemsUntilBackup
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsUntilBackup"] = value;
				}
			}

			// Token: 0x170071A9 RID: 29097
			// (set) Token: 0x06009F16 RID: 40726 RVA: 0x000E6996 File Offset: 0x000E4B96
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x170071AA RID: 29098
			// (set) Token: 0x06009F17 RID: 40727 RVA: 0x000E69AE File Offset: 0x000E4BAE
			public virtual bool IsExcludedFromServingHierarchy
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromServingHierarchy"] = value;
				}
			}

			// Token: 0x170071AB RID: 29099
			// (set) Token: 0x06009F18 RID: 40728 RVA: 0x000E69C6 File Offset: 0x000E4BC6
			public virtual bool IsHierarchyReady
			{
				set
				{
					base.PowerSharpParameters["IsHierarchyReady"] = value;
				}
			}

			// Token: 0x170071AC RID: 29100
			// (set) Token: 0x06009F19 RID: 40729 RVA: 0x000E69DE File Offset: 0x000E4BDE
			public virtual bool LitigationHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldEnabled"] = value;
				}
			}

			// Token: 0x170071AD RID: 29101
			// (set) Token: 0x06009F1A RID: 40730 RVA: 0x000E69F6 File Offset: 0x000E4BF6
			public virtual bool SingleItemRecoveryEnabled
			{
				set
				{
					base.PowerSharpParameters["SingleItemRecoveryEnabled"] = value;
				}
			}

			// Token: 0x170071AE RID: 29102
			// (set) Token: 0x06009F1B RID: 40731 RVA: 0x000E6A0E File Offset: 0x000E4C0E
			public virtual bool RetentionHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["RetentionHoldEnabled"] = value;
				}
			}

			// Token: 0x170071AF RID: 29103
			// (set) Token: 0x06009F1C RID: 40732 RVA: 0x000E6A26 File Offset: 0x000E4C26
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x170071B0 RID: 29104
			// (set) Token: 0x06009F1D RID: 40733 RVA: 0x000E6A3E File Offset: 0x000E4C3E
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x170071B1 RID: 29105
			// (set) Token: 0x06009F1E RID: 40734 RVA: 0x000E6A56 File Offset: 0x000E4C56
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x170071B2 RID: 29106
			// (set) Token: 0x06009F1F RID: 40735 RVA: 0x000E6A69 File Offset: 0x000E4C69
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x170071B3 RID: 29107
			// (set) Token: 0x06009F20 RID: 40736 RVA: 0x000E6A7C File Offset: 0x000E4C7C
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x170071B4 RID: 29108
			// (set) Token: 0x06009F21 RID: 40737 RVA: 0x000E6A94 File Offset: 0x000E4C94
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x170071B5 RID: 29109
			// (set) Token: 0x06009F22 RID: 40738 RVA: 0x000E6AA7 File Offset: 0x000E4CA7
			public virtual bool CalendarRepairDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairDisabled"] = value;
				}
			}

			// Token: 0x170071B6 RID: 29110
			// (set) Token: 0x06009F23 RID: 40739 RVA: 0x000E6ABF File Offset: 0x000E4CBF
			public virtual bool MessageTrackingReadStatusEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingReadStatusEnabled"] = value;
				}
			}

			// Token: 0x170071B7 RID: 29111
			// (set) Token: 0x06009F24 RID: 40740 RVA: 0x000E6AD7 File Offset: 0x000E4CD7
			public virtual ExternalOofOptions ExternalOofOptions
			{
				set
				{
					base.PowerSharpParameters["ExternalOofOptions"] = value;
				}
			}

			// Token: 0x170071B8 RID: 29112
			// (set) Token: 0x06009F25 RID: 40741 RVA: 0x000E6AEF File Offset: 0x000E4CEF
			public virtual ProxyAddress ForwardingSmtpAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingSmtpAddress"] = value;
				}
			}

			// Token: 0x170071B9 RID: 29113
			// (set) Token: 0x06009F26 RID: 40742 RVA: 0x000E6B02 File Offset: 0x000E4D02
			public virtual EnhancedTimeSpan RetainDeletedItemsFor
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsFor"] = value;
				}
			}

			// Token: 0x170071BA RID: 29114
			// (set) Token: 0x06009F27 RID: 40743 RVA: 0x000E6B1A File Offset: 0x000E4D1A
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x170071BB RID: 29115
			// (set) Token: 0x06009F28 RID: 40744 RVA: 0x000E6B2D File Offset: 0x000E4D2D
			public virtual Unlimited<ByteQuantifiedSize> ProhibitSendQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendQuota"] = value;
				}
			}

			// Token: 0x170071BC RID: 29116
			// (set) Token: 0x06009F29 RID: 40745 RVA: 0x000E6B45 File Offset: 0x000E4D45
			public virtual Unlimited<ByteQuantifiedSize> ProhibitSendReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendReceiveQuota"] = value;
				}
			}

			// Token: 0x170071BD RID: 29117
			// (set) Token: 0x06009F2A RID: 40746 RVA: 0x000E6B5D File Offset: 0x000E4D5D
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsQuota"] = value;
				}
			}

			// Token: 0x170071BE RID: 29118
			// (set) Token: 0x06009F2B RID: 40747 RVA: 0x000E6B75 File Offset: 0x000E4D75
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsWarningQuota"] = value;
				}
			}

			// Token: 0x170071BF RID: 29119
			// (set) Token: 0x06009F2C RID: 40748 RVA: 0x000E6B8D File Offset: 0x000E4D8D
			public virtual Unlimited<ByteQuantifiedSize> CalendarLoggingQuota
			{
				set
				{
					base.PowerSharpParameters["CalendarLoggingQuota"] = value;
				}
			}

			// Token: 0x170071C0 RID: 29120
			// (set) Token: 0x06009F2D RID: 40749 RVA: 0x000E6BA5 File Offset: 0x000E4DA5
			public virtual bool DowngradeHighPriorityMessagesEnabled
			{
				set
				{
					base.PowerSharpParameters["DowngradeHighPriorityMessagesEnabled"] = value;
				}
			}

			// Token: 0x170071C1 RID: 29121
			// (set) Token: 0x06009F2E RID: 40750 RVA: 0x000E6BBD File Offset: 0x000E4DBD
			public virtual Unlimited<int> RecipientLimits
			{
				set
				{
					base.PowerSharpParameters["RecipientLimits"] = value;
				}
			}

			// Token: 0x170071C2 RID: 29122
			// (set) Token: 0x06009F2F RID: 40751 RVA: 0x000E6BD5 File Offset: 0x000E4DD5
			public virtual bool ImListMigrationCompleted
			{
				set
				{
					base.PowerSharpParameters["ImListMigrationCompleted"] = value;
				}
			}

			// Token: 0x170071C3 RID: 29123
			// (set) Token: 0x06009F30 RID: 40752 RVA: 0x000E6BED File Offset: 0x000E4DED
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x170071C4 RID: 29124
			// (set) Token: 0x06009F31 RID: 40753 RVA: 0x000E6C05 File Offset: 0x000E4E05
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x170071C5 RID: 29125
			// (set) Token: 0x06009F32 RID: 40754 RVA: 0x000E6C1D File Offset: 0x000E4E1D
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x170071C6 RID: 29126
			// (set) Token: 0x06009F33 RID: 40755 RVA: 0x000E6C30 File Offset: 0x000E4E30
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x170071C7 RID: 29127
			// (set) Token: 0x06009F34 RID: 40756 RVA: 0x000E6C43 File Offset: 0x000E4E43
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x170071C8 RID: 29128
			// (set) Token: 0x06009F35 RID: 40757 RVA: 0x000E6C5B File Offset: 0x000E4E5B
			public virtual bool? SCLDeleteEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteEnabled"] = value;
				}
			}

			// Token: 0x170071C9 RID: 29129
			// (set) Token: 0x06009F36 RID: 40758 RVA: 0x000E6C73 File Offset: 0x000E4E73
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x170071CA RID: 29130
			// (set) Token: 0x06009F37 RID: 40759 RVA: 0x000E6C8B File Offset: 0x000E4E8B
			public virtual bool? SCLRejectEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLRejectEnabled"] = value;
				}
			}

			// Token: 0x170071CB RID: 29131
			// (set) Token: 0x06009F38 RID: 40760 RVA: 0x000E6CA3 File Offset: 0x000E4EA3
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x170071CC RID: 29132
			// (set) Token: 0x06009F39 RID: 40761 RVA: 0x000E6CBB File Offset: 0x000E4EBB
			public virtual bool? SCLQuarantineEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineEnabled"] = value;
				}
			}

			// Token: 0x170071CD RID: 29133
			// (set) Token: 0x06009F3A RID: 40762 RVA: 0x000E6CD3 File Offset: 0x000E4ED3
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x170071CE RID: 29134
			// (set) Token: 0x06009F3B RID: 40763 RVA: 0x000E6CEB File Offset: 0x000E4EEB
			public virtual bool? SCLJunkEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLJunkEnabled"] = value;
				}
			}

			// Token: 0x170071CF RID: 29135
			// (set) Token: 0x06009F3C RID: 40764 RVA: 0x000E6D03 File Offset: 0x000E4F03
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x170071D0 RID: 29136
			// (set) Token: 0x06009F3D RID: 40765 RVA: 0x000E6D1B File Offset: 0x000E4F1B
			public virtual bool? UseDatabaseQuotaDefaults
			{
				set
				{
					base.PowerSharpParameters["UseDatabaseQuotaDefaults"] = value;
				}
			}

			// Token: 0x170071D1 RID: 29137
			// (set) Token: 0x06009F3E RID: 40766 RVA: 0x000E6D33 File Offset: 0x000E4F33
			public virtual Unlimited<ByteQuantifiedSize> IssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["IssueWarningQuota"] = value;
				}
			}

			// Token: 0x170071D2 RID: 29138
			// (set) Token: 0x06009F3F RID: 40767 RVA: 0x000E6D4B File Offset: 0x000E4F4B
			public virtual ByteQuantifiedSize RulesQuota
			{
				set
				{
					base.PowerSharpParameters["RulesQuota"] = value;
				}
			}

			// Token: 0x170071D3 RID: 29139
			// (set) Token: 0x06009F40 RID: 40768 RVA: 0x000E6D63 File Offset: 0x000E4F63
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x170071D4 RID: 29140
			// (set) Token: 0x06009F41 RID: 40769 RVA: 0x000E6D76 File Offset: 0x000E4F76
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x170071D5 RID: 29141
			// (set) Token: 0x06009F42 RID: 40770 RVA: 0x000E6D89 File Offset: 0x000E4F89
			public virtual int? MaxSafeSenders
			{
				set
				{
					base.PowerSharpParameters["MaxSafeSenders"] = value;
				}
			}

			// Token: 0x170071D6 RID: 29142
			// (set) Token: 0x06009F43 RID: 40771 RVA: 0x000E6DA1 File Offset: 0x000E4FA1
			public virtual int? MaxBlockedSenders
			{
				set
				{
					base.PowerSharpParameters["MaxBlockedSenders"] = value;
				}
			}

			// Token: 0x170071D7 RID: 29143
			// (set) Token: 0x06009F44 RID: 40772 RVA: 0x000E6DB9 File Offset: 0x000E4FB9
			public virtual SmtpAddress WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x170071D8 RID: 29144
			// (set) Token: 0x06009F45 RID: 40773 RVA: 0x000E6DD1 File Offset: 0x000E4FD1
			public virtual SmtpAddress MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x170071D9 RID: 29145
			// (set) Token: 0x06009F46 RID: 40774 RVA: 0x000E6DE9 File Offset: 0x000E4FE9
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x170071DA RID: 29146
			// (set) Token: 0x06009F47 RID: 40775 RVA: 0x000E6DFC File Offset: 0x000E4FFC
			public virtual SmtpAddress JournalArchiveAddress
			{
				set
				{
					base.PowerSharpParameters["JournalArchiveAddress"] = value;
				}
			}

			// Token: 0x170071DB RID: 29147
			// (set) Token: 0x06009F48 RID: 40776 RVA: 0x000E6E14 File Offset: 0x000E5014
			public virtual Unlimited<ByteQuantifiedSize> ArchiveQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveQuota"] = value;
				}
			}

			// Token: 0x170071DC RID: 29148
			// (set) Token: 0x06009F49 RID: 40777 RVA: 0x000E6E2C File Offset: 0x000E502C
			public virtual Unlimited<ByteQuantifiedSize> ArchiveWarningQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveWarningQuota"] = value;
				}
			}

			// Token: 0x170071DD RID: 29149
			// (set) Token: 0x06009F4A RID: 40778 RVA: 0x000E6E44 File Offset: 0x000E5044
			public virtual SmtpDomain ArchiveDomain
			{
				set
				{
					base.PowerSharpParameters["ArchiveDomain"] = value;
				}
			}

			// Token: 0x170071DE RID: 29150
			// (set) Token: 0x06009F4B RID: 40779 RVA: 0x000E6E57 File Offset: 0x000E5057
			public virtual ArchiveStatusFlags ArchiveStatus
			{
				set
				{
					base.PowerSharpParameters["ArchiveStatus"] = value;
				}
			}

			// Token: 0x170071DF RID: 29151
			// (set) Token: 0x06009F4C RID: 40780 RVA: 0x000E6E6F File Offset: 0x000E506F
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x170071E0 RID: 29152
			// (set) Token: 0x06009F4D RID: 40781 RVA: 0x000E6E87 File Offset: 0x000E5087
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x170071E1 RID: 29153
			// (set) Token: 0x06009F4E RID: 40782 RVA: 0x000E6E9F File Offset: 0x000E509F
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x170071E2 RID: 29154
			// (set) Token: 0x06009F4F RID: 40783 RVA: 0x000E6EB2 File Offset: 0x000E50B2
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x170071E3 RID: 29155
			// (set) Token: 0x06009F50 RID: 40784 RVA: 0x000E6EC5 File Offset: 0x000E50C5
			public virtual bool CalendarVersionStoreDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarVersionStoreDisabled"] = value;
				}
			}

			// Token: 0x170071E4 RID: 29156
			// (set) Token: 0x06009F51 RID: 40785 RVA: 0x000E6EDD File Offset: 0x000E50DD
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x170071E5 RID: 29157
			// (set) Token: 0x06009F52 RID: 40786 RVA: 0x000E6EF0 File Offset: 0x000E50F0
			public virtual bool? SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x170071E6 RID: 29158
			// (set) Token: 0x06009F53 RID: 40787 RVA: 0x000E6F08 File Offset: 0x000E5108
			public virtual bool AuditEnabled
			{
				set
				{
					base.PowerSharpParameters["AuditEnabled"] = value;
				}
			}

			// Token: 0x170071E7 RID: 29159
			// (set) Token: 0x06009F54 RID: 40788 RVA: 0x000E6F20 File Offset: 0x000E5120
			public virtual EnhancedTimeSpan AuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["AuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x170071E8 RID: 29160
			// (set) Token: 0x06009F55 RID: 40789 RVA: 0x000E6F38 File Offset: 0x000E5138
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditAdmin
			{
				set
				{
					base.PowerSharpParameters["AuditAdmin"] = value;
				}
			}

			// Token: 0x170071E9 RID: 29161
			// (set) Token: 0x06009F56 RID: 40790 RVA: 0x000E6F4B File Offset: 0x000E514B
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditDelegate
			{
				set
				{
					base.PowerSharpParameters["AuditDelegate"] = value;
				}
			}

			// Token: 0x170071EA RID: 29162
			// (set) Token: 0x06009F57 RID: 40791 RVA: 0x000E6F5E File Offset: 0x000E515E
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditOwner
			{
				set
				{
					base.PowerSharpParameters["AuditOwner"] = value;
				}
			}

			// Token: 0x170071EB RID: 29163
			// (set) Token: 0x06009F58 RID: 40792 RVA: 0x000E6F71 File Offset: 0x000E5171
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x170071EC RID: 29164
			// (set) Token: 0x06009F59 RID: 40793 RVA: 0x000E6F84 File Offset: 0x000E5184
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170071ED RID: 29165
			// (set) Token: 0x06009F5A RID: 40794 RVA: 0x000E6F97 File Offset: 0x000E5197
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x170071EE RID: 29166
			// (set) Token: 0x06009F5B RID: 40795 RVA: 0x000E6FAA File Offset: 0x000E51AA
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x170071EF RID: 29167
			// (set) Token: 0x06009F5C RID: 40796 RVA: 0x000E6FBD File Offset: 0x000E51BD
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x170071F0 RID: 29168
			// (set) Token: 0x06009F5D RID: 40797 RVA: 0x000E6FD0 File Offset: 0x000E51D0
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x170071F1 RID: 29169
			// (set) Token: 0x06009F5E RID: 40798 RVA: 0x000E6FE3 File Offset: 0x000E51E3
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x170071F2 RID: 29170
			// (set) Token: 0x06009F5F RID: 40799 RVA: 0x000E6FF6 File Offset: 0x000E51F6
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x170071F3 RID: 29171
			// (set) Token: 0x06009F60 RID: 40800 RVA: 0x000E7009 File Offset: 0x000E5209
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x170071F4 RID: 29172
			// (set) Token: 0x06009F61 RID: 40801 RVA: 0x000E701C File Offset: 0x000E521C
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x170071F5 RID: 29173
			// (set) Token: 0x06009F62 RID: 40802 RVA: 0x000E702F File Offset: 0x000E522F
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x170071F6 RID: 29174
			// (set) Token: 0x06009F63 RID: 40803 RVA: 0x000E7042 File Offset: 0x000E5242
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x170071F7 RID: 29175
			// (set) Token: 0x06009F64 RID: 40804 RVA: 0x000E7055 File Offset: 0x000E5255
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x170071F8 RID: 29176
			// (set) Token: 0x06009F65 RID: 40805 RVA: 0x000E7068 File Offset: 0x000E5268
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x170071F9 RID: 29177
			// (set) Token: 0x06009F66 RID: 40806 RVA: 0x000E707B File Offset: 0x000E527B
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x170071FA RID: 29178
			// (set) Token: 0x06009F67 RID: 40807 RVA: 0x000E708E File Offset: 0x000E528E
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x170071FB RID: 29179
			// (set) Token: 0x06009F68 RID: 40808 RVA: 0x000E70A1 File Offset: 0x000E52A1
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x170071FC RID: 29180
			// (set) Token: 0x06009F69 RID: 40809 RVA: 0x000E70B4 File Offset: 0x000E52B4
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x170071FD RID: 29181
			// (set) Token: 0x06009F6A RID: 40810 RVA: 0x000E70C7 File Offset: 0x000E52C7
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x170071FE RID: 29182
			// (set) Token: 0x06009F6B RID: 40811 RVA: 0x000E70DA File Offset: 0x000E52DA
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x170071FF RID: 29183
			// (set) Token: 0x06009F6C RID: 40812 RVA: 0x000E70ED File Offset: 0x000E52ED
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17007200 RID: 29184
			// (set) Token: 0x06009F6D RID: 40813 RVA: 0x000E7100 File Offset: 0x000E5300
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17007201 RID: 29185
			// (set) Token: 0x06009F6E RID: 40814 RVA: 0x000E7113 File Offset: 0x000E5313
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007202 RID: 29186
			// (set) Token: 0x06009F6F RID: 40815 RVA: 0x000E7126 File Offset: 0x000E5326
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17007203 RID: 29187
			// (set) Token: 0x06009F70 RID: 40816 RVA: 0x000E7139 File Offset: 0x000E5339
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17007204 RID: 29188
			// (set) Token: 0x06009F71 RID: 40817 RVA: 0x000E7151 File Offset: 0x000E5351
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x17007205 RID: 29189
			// (set) Token: 0x06009F72 RID: 40818 RVA: 0x000E7169 File Offset: 0x000E5369
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x17007206 RID: 29190
			// (set) Token: 0x06009F73 RID: 40819 RVA: 0x000E7181 File Offset: 0x000E5381
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007207 RID: 29191
			// (set) Token: 0x06009F74 RID: 40820 RVA: 0x000E7199 File Offset: 0x000E5399
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x17007208 RID: 29192
			// (set) Token: 0x06009F75 RID: 40821 RVA: 0x000E71B1 File Offset: 0x000E53B1
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007209 RID: 29193
			// (set) Token: 0x06009F76 RID: 40822 RVA: 0x000E71C9 File Offset: 0x000E53C9
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x1700720A RID: 29194
			// (set) Token: 0x06009F77 RID: 40823 RVA: 0x000E71E1 File Offset: 0x000E53E1
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x1700720B RID: 29195
			// (set) Token: 0x06009F78 RID: 40824 RVA: 0x000E71F4 File Offset: 0x000E53F4
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700720C RID: 29196
			// (set) Token: 0x06009F79 RID: 40825 RVA: 0x000E720C File Offset: 0x000E540C
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x1700720D RID: 29197
			// (set) Token: 0x06009F7A RID: 40826 RVA: 0x000E721F File Offset: 0x000E541F
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x1700720E RID: 29198
			// (set) Token: 0x06009F7B RID: 40827 RVA: 0x000E7237 File Offset: 0x000E5437
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x1700720F RID: 29199
			// (set) Token: 0x06009F7C RID: 40828 RVA: 0x000E724A File Offset: 0x000E544A
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17007210 RID: 29200
			// (set) Token: 0x06009F7D RID: 40829 RVA: 0x000E725D File Offset: 0x000E545D
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007211 RID: 29201
			// (set) Token: 0x06009F7E RID: 40830 RVA: 0x000E7270 File Offset: 0x000E5470
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007212 RID: 29202
			// (set) Token: 0x06009F7F RID: 40831 RVA: 0x000E7288 File Offset: 0x000E5488
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007213 RID: 29203
			// (set) Token: 0x06009F80 RID: 40832 RVA: 0x000E72A0 File Offset: 0x000E54A0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007214 RID: 29204
			// (set) Token: 0x06009F81 RID: 40833 RVA: 0x000E72B8 File Offset: 0x000E54B8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007215 RID: 29205
			// (set) Token: 0x06009F82 RID: 40834 RVA: 0x000E72D0 File Offset: 0x000E54D0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C92 RID: 3218
		public class AddAggregatedMailboxParameters : ParametersBase
		{
			// Token: 0x17007216 RID: 29206
			// (set) Token: 0x06009F84 RID: 40836 RVA: 0x000E72F0 File Offset: 0x000E54F0
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007217 RID: 29207
			// (set) Token: 0x06009F85 RID: 40837 RVA: 0x000E730E File Offset: 0x000E550E
			public virtual SwitchParameter AddAggregatedAccount
			{
				set
				{
					base.PowerSharpParameters["AddAggregatedAccount"] = value;
				}
			}

			// Token: 0x17007218 RID: 29208
			// (set) Token: 0x06009F86 RID: 40838 RVA: 0x000E7326 File Offset: 0x000E5526
			public virtual Guid AggregatedMailboxGuid
			{
				set
				{
					base.PowerSharpParameters["AggregatedMailboxGuid"] = value;
				}
			}

			// Token: 0x17007219 RID: 29209
			// (set) Token: 0x06009F87 RID: 40839 RVA: 0x000E733E File Offset: 0x000E553E
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x1700721A RID: 29210
			// (set) Token: 0x06009F88 RID: 40840 RVA: 0x000E735C File Offset: 0x000E555C
			public virtual bool EnableRoomMailboxAccount
			{
				set
				{
					base.PowerSharpParameters["EnableRoomMailboxAccount"] = value;
				}
			}

			// Token: 0x1700721B RID: 29211
			// (set) Token: 0x06009F89 RID: 40841 RVA: 0x000E7374 File Offset: 0x000E5574
			public virtual SecureString RoomMailboxPassword
			{
				set
				{
					base.PowerSharpParameters["RoomMailboxPassword"] = value;
				}
			}

			// Token: 0x1700721C RID: 29212
			// (set) Token: 0x06009F8A RID: 40842 RVA: 0x000E7387 File Offset: 0x000E5587
			public virtual SwitchParameter SkipMailboxProvisioningConstraintValidation
			{
				set
				{
					base.PowerSharpParameters["SkipMailboxProvisioningConstraintValidation"] = value;
				}
			}

			// Token: 0x1700721D RID: 29213
			// (set) Token: 0x06009F8B RID: 40843 RVA: 0x000E739F File Offset: 0x000E559F
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x1700721E RID: 29214
			// (set) Token: 0x06009F8C RID: 40844 RVA: 0x000E73B2 File Offset: 0x000E55B2
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x1700721F RID: 29215
			// (set) Token: 0x06009F8D RID: 40845 RVA: 0x000E73C5 File Offset: 0x000E55C5
			public virtual SwitchParameter EvictLiveId
			{
				set
				{
					base.PowerSharpParameters["EvictLiveId"] = value;
				}
			}

			// Token: 0x17007220 RID: 29216
			// (set) Token: 0x06009F8E RID: 40846 RVA: 0x000E73DD File Offset: 0x000E55DD
			public virtual bool RequireSecretQA
			{
				set
				{
					base.PowerSharpParameters["RequireSecretQA"] = value;
				}
			}

			// Token: 0x17007221 RID: 29217
			// (set) Token: 0x06009F8F RID: 40847 RVA: 0x000E73F5 File Offset: 0x000E55F5
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x17007222 RID: 29218
			// (set) Token: 0x06009F90 RID: 40848 RVA: 0x000E7408 File Offset: 0x000E5608
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17007223 RID: 29219
			// (set) Token: 0x06009F91 RID: 40849 RVA: 0x000E741B File Offset: 0x000E561B
			public virtual Unlimited<EnhancedTimeSpan> LitigationHoldDuration
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDuration"] = value;
				}
			}

			// Token: 0x17007224 RID: 29220
			// (set) Token: 0x06009F92 RID: 40850 RVA: 0x000E7433 File Offset: 0x000E5633
			public virtual bool UMDataStorage
			{
				set
				{
					base.PowerSharpParameters["UMDataStorage"] = value;
				}
			}

			// Token: 0x17007225 RID: 29221
			// (set) Token: 0x06009F93 RID: 40851 RVA: 0x000E744B File Offset: 0x000E564B
			public virtual bool UMGrammar
			{
				set
				{
					base.PowerSharpParameters["UMGrammar"] = value;
				}
			}

			// Token: 0x17007226 RID: 29222
			// (set) Token: 0x06009F94 RID: 40852 RVA: 0x000E7463 File Offset: 0x000E5663
			public virtual bool OABGen
			{
				set
				{
					base.PowerSharpParameters["OABGen"] = value;
				}
			}

			// Token: 0x17007227 RID: 29223
			// (set) Token: 0x06009F95 RID: 40853 RVA: 0x000E747B File Offset: 0x000E567B
			public virtual bool GMGen
			{
				set
				{
					base.PowerSharpParameters["GMGen"] = value;
				}
			}

			// Token: 0x17007228 RID: 29224
			// (set) Token: 0x06009F96 RID: 40854 RVA: 0x000E7493 File Offset: 0x000E5693
			public virtual bool ClientExtensions
			{
				set
				{
					base.PowerSharpParameters["ClientExtensions"] = value;
				}
			}

			// Token: 0x17007229 RID: 29225
			// (set) Token: 0x06009F97 RID: 40855 RVA: 0x000E74AB File Offset: 0x000E56AB
			public virtual bool MailRouting
			{
				set
				{
					base.PowerSharpParameters["MailRouting"] = value;
				}
			}

			// Token: 0x1700722A RID: 29226
			// (set) Token: 0x06009F98 RID: 40856 RVA: 0x000E74C3 File Offset: 0x000E56C3
			public virtual bool Management
			{
				set
				{
					base.PowerSharpParameters["Management"] = value;
				}
			}

			// Token: 0x1700722B RID: 29227
			// (set) Token: 0x06009F99 RID: 40857 RVA: 0x000E74DB File Offset: 0x000E56DB
			public virtual bool TenantUpgrade
			{
				set
				{
					base.PowerSharpParameters["TenantUpgrade"] = value;
				}
			}

			// Token: 0x1700722C RID: 29228
			// (set) Token: 0x06009F9A RID: 40858 RVA: 0x000E74F3 File Offset: 0x000E56F3
			public virtual bool Migration
			{
				set
				{
					base.PowerSharpParameters["Migration"] = value;
				}
			}

			// Token: 0x1700722D RID: 29229
			// (set) Token: 0x06009F9B RID: 40859 RVA: 0x000E750B File Offset: 0x000E570B
			public virtual bool MessageTracking
			{
				set
				{
					base.PowerSharpParameters["MessageTracking"] = value;
				}
			}

			// Token: 0x1700722E RID: 29230
			// (set) Token: 0x06009F9C RID: 40860 RVA: 0x000E7523 File Offset: 0x000E5723
			public virtual bool OMEncryption
			{
				set
				{
					base.PowerSharpParameters["OMEncryption"] = value;
				}
			}

			// Token: 0x1700722F RID: 29231
			// (set) Token: 0x06009F9D RID: 40861 RVA: 0x000E753B File Offset: 0x000E573B
			public virtual bool PstProvider
			{
				set
				{
					base.PowerSharpParameters["PstProvider"] = value;
				}
			}

			// Token: 0x17007230 RID: 29232
			// (set) Token: 0x06009F9E RID: 40862 RVA: 0x000E7553 File Offset: 0x000E5753
			public virtual bool SuiteServiceStorage
			{
				set
				{
					base.PowerSharpParameters["SuiteServiceStorage"] = value;
				}
			}

			// Token: 0x17007231 RID: 29233
			// (set) Token: 0x06009F9F RID: 40863 RVA: 0x000E756B File Offset: 0x000E576B
			public virtual SecureString OldPassword
			{
				set
				{
					base.PowerSharpParameters["OldPassword"] = value;
				}
			}

			// Token: 0x17007232 RID: 29234
			// (set) Token: 0x06009FA0 RID: 40864 RVA: 0x000E757E File Offset: 0x000E577E
			public virtual SecureString NewPassword
			{
				set
				{
					base.PowerSharpParameters["NewPassword"] = value;
				}
			}

			// Token: 0x17007233 RID: 29235
			// (set) Token: 0x06009FA1 RID: 40865 RVA: 0x000E7591 File Offset: 0x000E5791
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007234 RID: 29236
			// (set) Token: 0x06009FA2 RID: 40866 RVA: 0x000E75A4 File Offset: 0x000E57A4
			public virtual string QueryBaseDN
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDN"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007235 RID: 29237
			// (set) Token: 0x06009FA3 RID: 40867 RVA: 0x000E75C2 File Offset: 0x000E57C2
			public virtual string DefaultPublicFolderMailbox
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMailbox"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17007236 RID: 29238
			// (set) Token: 0x06009FA4 RID: 40868 RVA: 0x000E75E0 File Offset: 0x000E57E0
			public virtual int? MailboxMessagesPerFolderCountWarningQuota
			{
				set
				{
					base.PowerSharpParameters["MailboxMessagesPerFolderCountWarningQuota"] = value;
				}
			}

			// Token: 0x17007237 RID: 29239
			// (set) Token: 0x06009FA5 RID: 40869 RVA: 0x000E75F8 File Offset: 0x000E57F8
			public virtual int? MailboxMessagesPerFolderCountReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["MailboxMessagesPerFolderCountReceiveQuota"] = value;
				}
			}

			// Token: 0x17007238 RID: 29240
			// (set) Token: 0x06009FA6 RID: 40870 RVA: 0x000E7610 File Offset: 0x000E5810
			public virtual int? DumpsterMessagesPerFolderCountWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DumpsterMessagesPerFolderCountWarningQuota"] = value;
				}
			}

			// Token: 0x17007239 RID: 29241
			// (set) Token: 0x06009FA7 RID: 40871 RVA: 0x000E7628 File Offset: 0x000E5828
			public virtual int? DumpsterMessagesPerFolderCountReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["DumpsterMessagesPerFolderCountReceiveQuota"] = value;
				}
			}

			// Token: 0x1700723A RID: 29242
			// (set) Token: 0x06009FA8 RID: 40872 RVA: 0x000E7640 File Offset: 0x000E5840
			public virtual int? FolderHierarchyChildrenCountWarningQuota
			{
				set
				{
					base.PowerSharpParameters["FolderHierarchyChildrenCountWarningQuota"] = value;
				}
			}

			// Token: 0x1700723B RID: 29243
			// (set) Token: 0x06009FA9 RID: 40873 RVA: 0x000E7658 File Offset: 0x000E5858
			public virtual int? FolderHierarchyChildrenCountReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["FolderHierarchyChildrenCountReceiveQuota"] = value;
				}
			}

			// Token: 0x1700723C RID: 29244
			// (set) Token: 0x06009FAA RID: 40874 RVA: 0x000E7670 File Offset: 0x000E5870
			public virtual int? FolderHierarchyDepthWarningQuota
			{
				set
				{
					base.PowerSharpParameters["FolderHierarchyDepthWarningQuota"] = value;
				}
			}

			// Token: 0x1700723D RID: 29245
			// (set) Token: 0x06009FAB RID: 40875 RVA: 0x000E7688 File Offset: 0x000E5888
			public virtual int? FolderHierarchyDepthReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["FolderHierarchyDepthReceiveQuota"] = value;
				}
			}

			// Token: 0x1700723E RID: 29246
			// (set) Token: 0x06009FAC RID: 40876 RVA: 0x000E76A0 File Offset: 0x000E58A0
			public virtual int? FoldersCountWarningQuota
			{
				set
				{
					base.PowerSharpParameters["FoldersCountWarningQuota"] = value;
				}
			}

			// Token: 0x1700723F RID: 29247
			// (set) Token: 0x06009FAD RID: 40877 RVA: 0x000E76B8 File Offset: 0x000E58B8
			public virtual int? FoldersCountReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["FoldersCountReceiveQuota"] = value;
				}
			}

			// Token: 0x17007240 RID: 29248
			// (set) Token: 0x06009FAE RID: 40878 RVA: 0x000E76D0 File Offset: 0x000E58D0
			public virtual int? ExtendedPropertiesCountQuota
			{
				set
				{
					base.PowerSharpParameters["ExtendedPropertiesCountQuota"] = value;
				}
			}

			// Token: 0x17007241 RID: 29249
			// (set) Token: 0x06009FAF RID: 40879 RVA: 0x000E76E8 File Offset: 0x000E58E8
			public virtual Guid? MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x17007242 RID: 29250
			// (set) Token: 0x06009FB0 RID: 40880 RVA: 0x000E7700 File Offset: 0x000E5900
			public virtual CrossTenantObjectId UnifiedMailbox
			{
				set
				{
					base.PowerSharpParameters["UnifiedMailbox"] = value;
				}
			}

			// Token: 0x17007243 RID: 29251
			// (set) Token: 0x06009FB1 RID: 40881 RVA: 0x000E7713 File Offset: 0x000E5913
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17007244 RID: 29252
			// (set) Token: 0x06009FB2 RID: 40882 RVA: 0x000E772B File Offset: 0x000E592B
			public virtual bool MessageCopyForSentAsEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageCopyForSentAsEnabled"] = value;
				}
			}

			// Token: 0x17007245 RID: 29253
			// (set) Token: 0x06009FB3 RID: 40883 RVA: 0x000E7743 File Offset: 0x000E5943
			public virtual bool MessageCopyForSendOnBehalfEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageCopyForSendOnBehalfEnabled"] = value;
				}
			}

			// Token: 0x17007246 RID: 29254
			// (set) Token: 0x06009FB4 RID: 40884 RVA: 0x000E775B File Offset: 0x000E595B
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007247 RID: 29255
			// (set) Token: 0x06009FB5 RID: 40885 RVA: 0x000E7773 File Offset: 0x000E5973
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17007248 RID: 29256
			// (set) Token: 0x06009FB6 RID: 40886 RVA: 0x000E778B File Offset: 0x000E598B
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17007249 RID: 29257
			// (set) Token: 0x06009FB7 RID: 40887 RVA: 0x000E77A3 File Offset: 0x000E59A3
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700724A RID: 29258
			// (set) Token: 0x06009FB8 RID: 40888 RVA: 0x000E77C1 File Offset: 0x000E59C1
			public virtual string ForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700724B RID: 29259
			// (set) Token: 0x06009FB9 RID: 40889 RVA: 0x000E77DF File Offset: 0x000E59DF
			public virtual OfflineAddressBookIdParameter OfflineAddressBook
			{
				set
				{
					base.PowerSharpParameters["OfflineAddressBook"] = value;
				}
			}

			// Token: 0x1700724C RID: 29260
			// (set) Token: 0x06009FBA RID: 40890 RVA: 0x000E77F2 File Offset: 0x000E59F2
			public virtual string LinkedMasterAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedMasterAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x1700724D RID: 29261
			// (set) Token: 0x06009FBB RID: 40891 RVA: 0x000E7810 File Offset: 0x000E5A10
			public virtual string LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x1700724E RID: 29262
			// (set) Token: 0x06009FBC RID: 40892 RVA: 0x000E7823 File Offset: 0x000E5A23
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x1700724F RID: 29263
			// (set) Token: 0x06009FBD RID: 40893 RVA: 0x000E7836 File Offset: 0x000E5A36
			public virtual ConvertibleMailboxSubType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x17007250 RID: 29264
			// (set) Token: 0x06009FBE RID: 40894 RVA: 0x000E784E File Offset: 0x000E5A4E
			public virtual SwitchParameter ApplyMandatoryProperties
			{
				set
				{
					base.PowerSharpParameters["ApplyMandatoryProperties"] = value;
				}
			}

			// Token: 0x17007251 RID: 29265
			// (set) Token: 0x06009FBF RID: 40895 RVA: 0x000E7866 File Offset: 0x000E5A66
			public virtual SwitchParameter RemoveManagedFolderAndPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoveManagedFolderAndPolicy"] = value;
				}
			}

			// Token: 0x17007252 RID: 29266
			// (set) Token: 0x06009FC0 RID: 40896 RVA: 0x000E787E File Offset: 0x000E5A7E
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007253 RID: 29267
			// (set) Token: 0x06009FC1 RID: 40897 RVA: 0x000E789C File Offset: 0x000E5A9C
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007254 RID: 29268
			// (set) Token: 0x06009FC2 RID: 40898 RVA: 0x000E78BA File Offset: 0x000E5ABA
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007255 RID: 29269
			// (set) Token: 0x06009FC3 RID: 40899 RVA: 0x000E78D8 File Offset: 0x000E5AD8
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007256 RID: 29270
			// (set) Token: 0x06009FC4 RID: 40900 RVA: 0x000E78EB File Offset: 0x000E5AEB
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007257 RID: 29271
			// (set) Token: 0x06009FC5 RID: 40901 RVA: 0x000E7909 File Offset: 0x000E5B09
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17007258 RID: 29272
			// (set) Token: 0x06009FC6 RID: 40902 RVA: 0x000E7921 File Offset: 0x000E5B21
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17007259 RID: 29273
			// (set) Token: 0x06009FC7 RID: 40903 RVA: 0x000E7939 File Offset: 0x000E5B39
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x1700725A RID: 29274
			// (set) Token: 0x06009FC8 RID: 40904 RVA: 0x000E794C File Offset: 0x000E5B4C
			public virtual SwitchParameter BypassLiveId
			{
				set
				{
					base.PowerSharpParameters["BypassLiveId"] = value;
				}
			}

			// Token: 0x1700725B RID: 29275
			// (set) Token: 0x06009FC9 RID: 40905 RVA: 0x000E7964 File Offset: 0x000E5B64
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x1700725C RID: 29276
			// (set) Token: 0x06009FCA RID: 40906 RVA: 0x000E7977 File Offset: 0x000E5B77
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700725D RID: 29277
			// (set) Token: 0x06009FCB RID: 40907 RVA: 0x000E798A File Offset: 0x000E5B8A
			public virtual string FederatedIdentity
			{
				set
				{
					base.PowerSharpParameters["FederatedIdentity"] = value;
				}
			}

			// Token: 0x1700725E RID: 29278
			// (set) Token: 0x06009FCC RID: 40908 RVA: 0x000E799D File Offset: 0x000E5B9D
			public virtual string SecondaryAddress
			{
				set
				{
					base.PowerSharpParameters["SecondaryAddress"] = value;
				}
			}

			// Token: 0x1700725F RID: 29279
			// (set) Token: 0x06009FCD RID: 40909 RVA: 0x000E79B0 File Offset: 0x000E5BB0
			public virtual string SecondaryDialPlan
			{
				set
				{
					base.PowerSharpParameters["SecondaryDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007260 RID: 29280
			// (set) Token: 0x06009FCE RID: 40910 RVA: 0x000E79CE File Offset: 0x000E5BCE
			public virtual SwitchParameter RemovePicture
			{
				set
				{
					base.PowerSharpParameters["RemovePicture"] = value;
				}
			}

			// Token: 0x17007261 RID: 29281
			// (set) Token: 0x06009FCF RID: 40911 RVA: 0x000E79E6 File Offset: 0x000E5BE6
			public virtual SwitchParameter RemoveSpokenName
			{
				set
				{
					base.PowerSharpParameters["RemoveSpokenName"] = value;
				}
			}

			// Token: 0x17007262 RID: 29282
			// (set) Token: 0x06009FD0 RID: 40912 RVA: 0x000E79FE File Offset: 0x000E5BFE
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17007263 RID: 29283
			// (set) Token: 0x06009FD1 RID: 40913 RVA: 0x000E7A11 File Offset: 0x000E5C11
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17007264 RID: 29284
			// (set) Token: 0x06009FD2 RID: 40914 RVA: 0x000E7A24 File Offset: 0x000E5C24
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17007265 RID: 29285
			// (set) Token: 0x06009FD3 RID: 40915 RVA: 0x000E7A37 File Offset: 0x000E5C37
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007266 RID: 29286
			// (set) Token: 0x06009FD4 RID: 40916 RVA: 0x000E7A55 File Offset: 0x000E5C55
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17007267 RID: 29287
			// (set) Token: 0x06009FD5 RID: 40917 RVA: 0x000E7A68 File Offset: 0x000E5C68
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007268 RID: 29288
			// (set) Token: 0x06009FD6 RID: 40918 RVA: 0x000E7A7B File Offset: 0x000E5C7B
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17007269 RID: 29289
			// (set) Token: 0x06009FD7 RID: 40919 RVA: 0x000E7A8E File Offset: 0x000E5C8E
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x1700726A RID: 29290
			// (set) Token: 0x06009FD8 RID: 40920 RVA: 0x000E7AA1 File Offset: 0x000E5CA1
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x1700726B RID: 29291
			// (set) Token: 0x06009FD9 RID: 40921 RVA: 0x000E7AB4 File Offset: 0x000E5CB4
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x1700726C RID: 29292
			// (set) Token: 0x06009FDA RID: 40922 RVA: 0x000E7AC7 File Offset: 0x000E5CC7
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x1700726D RID: 29293
			// (set) Token: 0x06009FDB RID: 40923 RVA: 0x000E7ADF File Offset: 0x000E5CDF
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700726E RID: 29294
			// (set) Token: 0x06009FDC RID: 40924 RVA: 0x000E7AF7 File Offset: 0x000E5CF7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700726F RID: 29295
			// (set) Token: 0x06009FDD RID: 40925 RVA: 0x000E7B0A File Offset: 0x000E5D0A
			public virtual bool UseDatabaseRetentionDefaults
			{
				set
				{
					base.PowerSharpParameters["UseDatabaseRetentionDefaults"] = value;
				}
			}

			// Token: 0x17007270 RID: 29296
			// (set) Token: 0x06009FDE RID: 40926 RVA: 0x000E7B22 File Offset: 0x000E5D22
			public virtual bool RetainDeletedItemsUntilBackup
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsUntilBackup"] = value;
				}
			}

			// Token: 0x17007271 RID: 29297
			// (set) Token: 0x06009FDF RID: 40927 RVA: 0x000E7B3A File Offset: 0x000E5D3A
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17007272 RID: 29298
			// (set) Token: 0x06009FE0 RID: 40928 RVA: 0x000E7B52 File Offset: 0x000E5D52
			public virtual bool IsExcludedFromServingHierarchy
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromServingHierarchy"] = value;
				}
			}

			// Token: 0x17007273 RID: 29299
			// (set) Token: 0x06009FE1 RID: 40929 RVA: 0x000E7B6A File Offset: 0x000E5D6A
			public virtual bool IsHierarchyReady
			{
				set
				{
					base.PowerSharpParameters["IsHierarchyReady"] = value;
				}
			}

			// Token: 0x17007274 RID: 29300
			// (set) Token: 0x06009FE2 RID: 40930 RVA: 0x000E7B82 File Offset: 0x000E5D82
			public virtual bool LitigationHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldEnabled"] = value;
				}
			}

			// Token: 0x17007275 RID: 29301
			// (set) Token: 0x06009FE3 RID: 40931 RVA: 0x000E7B9A File Offset: 0x000E5D9A
			public virtual bool SingleItemRecoveryEnabled
			{
				set
				{
					base.PowerSharpParameters["SingleItemRecoveryEnabled"] = value;
				}
			}

			// Token: 0x17007276 RID: 29302
			// (set) Token: 0x06009FE4 RID: 40932 RVA: 0x000E7BB2 File Offset: 0x000E5DB2
			public virtual bool RetentionHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["RetentionHoldEnabled"] = value;
				}
			}

			// Token: 0x17007277 RID: 29303
			// (set) Token: 0x06009FE5 RID: 40933 RVA: 0x000E7BCA File Offset: 0x000E5DCA
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17007278 RID: 29304
			// (set) Token: 0x06009FE6 RID: 40934 RVA: 0x000E7BE2 File Offset: 0x000E5DE2
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17007279 RID: 29305
			// (set) Token: 0x06009FE7 RID: 40935 RVA: 0x000E7BFA File Offset: 0x000E5DFA
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x1700727A RID: 29306
			// (set) Token: 0x06009FE8 RID: 40936 RVA: 0x000E7C0D File Offset: 0x000E5E0D
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x1700727B RID: 29307
			// (set) Token: 0x06009FE9 RID: 40937 RVA: 0x000E7C20 File Offset: 0x000E5E20
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x1700727C RID: 29308
			// (set) Token: 0x06009FEA RID: 40938 RVA: 0x000E7C38 File Offset: 0x000E5E38
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x1700727D RID: 29309
			// (set) Token: 0x06009FEB RID: 40939 RVA: 0x000E7C4B File Offset: 0x000E5E4B
			public virtual bool CalendarRepairDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairDisabled"] = value;
				}
			}

			// Token: 0x1700727E RID: 29310
			// (set) Token: 0x06009FEC RID: 40940 RVA: 0x000E7C63 File Offset: 0x000E5E63
			public virtual bool MessageTrackingReadStatusEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingReadStatusEnabled"] = value;
				}
			}

			// Token: 0x1700727F RID: 29311
			// (set) Token: 0x06009FED RID: 40941 RVA: 0x000E7C7B File Offset: 0x000E5E7B
			public virtual ExternalOofOptions ExternalOofOptions
			{
				set
				{
					base.PowerSharpParameters["ExternalOofOptions"] = value;
				}
			}

			// Token: 0x17007280 RID: 29312
			// (set) Token: 0x06009FEE RID: 40942 RVA: 0x000E7C93 File Offset: 0x000E5E93
			public virtual ProxyAddress ForwardingSmtpAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingSmtpAddress"] = value;
				}
			}

			// Token: 0x17007281 RID: 29313
			// (set) Token: 0x06009FEF RID: 40943 RVA: 0x000E7CA6 File Offset: 0x000E5EA6
			public virtual EnhancedTimeSpan RetainDeletedItemsFor
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsFor"] = value;
				}
			}

			// Token: 0x17007282 RID: 29314
			// (set) Token: 0x06009FF0 RID: 40944 RVA: 0x000E7CBE File Offset: 0x000E5EBE
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007283 RID: 29315
			// (set) Token: 0x06009FF1 RID: 40945 RVA: 0x000E7CD1 File Offset: 0x000E5ED1
			public virtual Unlimited<ByteQuantifiedSize> ProhibitSendQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendQuota"] = value;
				}
			}

			// Token: 0x17007284 RID: 29316
			// (set) Token: 0x06009FF2 RID: 40946 RVA: 0x000E7CE9 File Offset: 0x000E5EE9
			public virtual Unlimited<ByteQuantifiedSize> ProhibitSendReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendReceiveQuota"] = value;
				}
			}

			// Token: 0x17007285 RID: 29317
			// (set) Token: 0x06009FF3 RID: 40947 RVA: 0x000E7D01 File Offset: 0x000E5F01
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsQuota"] = value;
				}
			}

			// Token: 0x17007286 RID: 29318
			// (set) Token: 0x06009FF4 RID: 40948 RVA: 0x000E7D19 File Offset: 0x000E5F19
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsWarningQuota"] = value;
				}
			}

			// Token: 0x17007287 RID: 29319
			// (set) Token: 0x06009FF5 RID: 40949 RVA: 0x000E7D31 File Offset: 0x000E5F31
			public virtual Unlimited<ByteQuantifiedSize> CalendarLoggingQuota
			{
				set
				{
					base.PowerSharpParameters["CalendarLoggingQuota"] = value;
				}
			}

			// Token: 0x17007288 RID: 29320
			// (set) Token: 0x06009FF6 RID: 40950 RVA: 0x000E7D49 File Offset: 0x000E5F49
			public virtual bool DowngradeHighPriorityMessagesEnabled
			{
				set
				{
					base.PowerSharpParameters["DowngradeHighPriorityMessagesEnabled"] = value;
				}
			}

			// Token: 0x17007289 RID: 29321
			// (set) Token: 0x06009FF7 RID: 40951 RVA: 0x000E7D61 File Offset: 0x000E5F61
			public virtual Unlimited<int> RecipientLimits
			{
				set
				{
					base.PowerSharpParameters["RecipientLimits"] = value;
				}
			}

			// Token: 0x1700728A RID: 29322
			// (set) Token: 0x06009FF8 RID: 40952 RVA: 0x000E7D79 File Offset: 0x000E5F79
			public virtual bool ImListMigrationCompleted
			{
				set
				{
					base.PowerSharpParameters["ImListMigrationCompleted"] = value;
				}
			}

			// Token: 0x1700728B RID: 29323
			// (set) Token: 0x06009FF9 RID: 40953 RVA: 0x000E7D91 File Offset: 0x000E5F91
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700728C RID: 29324
			// (set) Token: 0x06009FFA RID: 40954 RVA: 0x000E7DA9 File Offset: 0x000E5FA9
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x1700728D RID: 29325
			// (set) Token: 0x06009FFB RID: 40955 RVA: 0x000E7DC1 File Offset: 0x000E5FC1
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x1700728E RID: 29326
			// (set) Token: 0x06009FFC RID: 40956 RVA: 0x000E7DD4 File Offset: 0x000E5FD4
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700728F RID: 29327
			// (set) Token: 0x06009FFD RID: 40957 RVA: 0x000E7DE7 File Offset: 0x000E5FE7
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x17007290 RID: 29328
			// (set) Token: 0x06009FFE RID: 40958 RVA: 0x000E7DFF File Offset: 0x000E5FFF
			public virtual bool? SCLDeleteEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteEnabled"] = value;
				}
			}

			// Token: 0x17007291 RID: 29329
			// (set) Token: 0x06009FFF RID: 40959 RVA: 0x000E7E17 File Offset: 0x000E6017
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x17007292 RID: 29330
			// (set) Token: 0x0600A000 RID: 40960 RVA: 0x000E7E2F File Offset: 0x000E602F
			public virtual bool? SCLRejectEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLRejectEnabled"] = value;
				}
			}

			// Token: 0x17007293 RID: 29331
			// (set) Token: 0x0600A001 RID: 40961 RVA: 0x000E7E47 File Offset: 0x000E6047
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x17007294 RID: 29332
			// (set) Token: 0x0600A002 RID: 40962 RVA: 0x000E7E5F File Offset: 0x000E605F
			public virtual bool? SCLQuarantineEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineEnabled"] = value;
				}
			}

			// Token: 0x17007295 RID: 29333
			// (set) Token: 0x0600A003 RID: 40963 RVA: 0x000E7E77 File Offset: 0x000E6077
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17007296 RID: 29334
			// (set) Token: 0x0600A004 RID: 40964 RVA: 0x000E7E8F File Offset: 0x000E608F
			public virtual bool? SCLJunkEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLJunkEnabled"] = value;
				}
			}

			// Token: 0x17007297 RID: 29335
			// (set) Token: 0x0600A005 RID: 40965 RVA: 0x000E7EA7 File Offset: 0x000E60A7
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x17007298 RID: 29336
			// (set) Token: 0x0600A006 RID: 40966 RVA: 0x000E7EBF File Offset: 0x000E60BF
			public virtual bool? UseDatabaseQuotaDefaults
			{
				set
				{
					base.PowerSharpParameters["UseDatabaseQuotaDefaults"] = value;
				}
			}

			// Token: 0x17007299 RID: 29337
			// (set) Token: 0x0600A007 RID: 40967 RVA: 0x000E7ED7 File Offset: 0x000E60D7
			public virtual Unlimited<ByteQuantifiedSize> IssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["IssueWarningQuota"] = value;
				}
			}

			// Token: 0x1700729A RID: 29338
			// (set) Token: 0x0600A008 RID: 40968 RVA: 0x000E7EEF File Offset: 0x000E60EF
			public virtual ByteQuantifiedSize RulesQuota
			{
				set
				{
					base.PowerSharpParameters["RulesQuota"] = value;
				}
			}

			// Token: 0x1700729B RID: 29339
			// (set) Token: 0x0600A009 RID: 40969 RVA: 0x000E7F07 File Offset: 0x000E6107
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x1700729C RID: 29340
			// (set) Token: 0x0600A00A RID: 40970 RVA: 0x000E7F1A File Offset: 0x000E611A
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x1700729D RID: 29341
			// (set) Token: 0x0600A00B RID: 40971 RVA: 0x000E7F2D File Offset: 0x000E612D
			public virtual int? MaxSafeSenders
			{
				set
				{
					base.PowerSharpParameters["MaxSafeSenders"] = value;
				}
			}

			// Token: 0x1700729E RID: 29342
			// (set) Token: 0x0600A00C RID: 40972 RVA: 0x000E7F45 File Offset: 0x000E6145
			public virtual int? MaxBlockedSenders
			{
				set
				{
					base.PowerSharpParameters["MaxBlockedSenders"] = value;
				}
			}

			// Token: 0x1700729F RID: 29343
			// (set) Token: 0x0600A00D RID: 40973 RVA: 0x000E7F5D File Offset: 0x000E615D
			public virtual SmtpAddress WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x170072A0 RID: 29344
			// (set) Token: 0x0600A00E RID: 40974 RVA: 0x000E7F75 File Offset: 0x000E6175
			public virtual SmtpAddress MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x170072A1 RID: 29345
			// (set) Token: 0x0600A00F RID: 40975 RVA: 0x000E7F8D File Offset: 0x000E618D
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x170072A2 RID: 29346
			// (set) Token: 0x0600A010 RID: 40976 RVA: 0x000E7FA0 File Offset: 0x000E61A0
			public virtual SmtpAddress JournalArchiveAddress
			{
				set
				{
					base.PowerSharpParameters["JournalArchiveAddress"] = value;
				}
			}

			// Token: 0x170072A3 RID: 29347
			// (set) Token: 0x0600A011 RID: 40977 RVA: 0x000E7FB8 File Offset: 0x000E61B8
			public virtual Unlimited<ByteQuantifiedSize> ArchiveQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveQuota"] = value;
				}
			}

			// Token: 0x170072A4 RID: 29348
			// (set) Token: 0x0600A012 RID: 40978 RVA: 0x000E7FD0 File Offset: 0x000E61D0
			public virtual Unlimited<ByteQuantifiedSize> ArchiveWarningQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveWarningQuota"] = value;
				}
			}

			// Token: 0x170072A5 RID: 29349
			// (set) Token: 0x0600A013 RID: 40979 RVA: 0x000E7FE8 File Offset: 0x000E61E8
			public virtual SmtpDomain ArchiveDomain
			{
				set
				{
					base.PowerSharpParameters["ArchiveDomain"] = value;
				}
			}

			// Token: 0x170072A6 RID: 29350
			// (set) Token: 0x0600A014 RID: 40980 RVA: 0x000E7FFB File Offset: 0x000E61FB
			public virtual ArchiveStatusFlags ArchiveStatus
			{
				set
				{
					base.PowerSharpParameters["ArchiveStatus"] = value;
				}
			}

			// Token: 0x170072A7 RID: 29351
			// (set) Token: 0x0600A015 RID: 40981 RVA: 0x000E8013 File Offset: 0x000E6213
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x170072A8 RID: 29352
			// (set) Token: 0x0600A016 RID: 40982 RVA: 0x000E802B File Offset: 0x000E622B
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x170072A9 RID: 29353
			// (set) Token: 0x0600A017 RID: 40983 RVA: 0x000E8043 File Offset: 0x000E6243
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x170072AA RID: 29354
			// (set) Token: 0x0600A018 RID: 40984 RVA: 0x000E8056 File Offset: 0x000E6256
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x170072AB RID: 29355
			// (set) Token: 0x0600A019 RID: 40985 RVA: 0x000E8069 File Offset: 0x000E6269
			public virtual bool CalendarVersionStoreDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarVersionStoreDisabled"] = value;
				}
			}

			// Token: 0x170072AC RID: 29356
			// (set) Token: 0x0600A01A RID: 40986 RVA: 0x000E8081 File Offset: 0x000E6281
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x170072AD RID: 29357
			// (set) Token: 0x0600A01B RID: 40987 RVA: 0x000E8094 File Offset: 0x000E6294
			public virtual bool? SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x170072AE RID: 29358
			// (set) Token: 0x0600A01C RID: 40988 RVA: 0x000E80AC File Offset: 0x000E62AC
			public virtual bool AuditEnabled
			{
				set
				{
					base.PowerSharpParameters["AuditEnabled"] = value;
				}
			}

			// Token: 0x170072AF RID: 29359
			// (set) Token: 0x0600A01D RID: 40989 RVA: 0x000E80C4 File Offset: 0x000E62C4
			public virtual EnhancedTimeSpan AuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["AuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x170072B0 RID: 29360
			// (set) Token: 0x0600A01E RID: 40990 RVA: 0x000E80DC File Offset: 0x000E62DC
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditAdmin
			{
				set
				{
					base.PowerSharpParameters["AuditAdmin"] = value;
				}
			}

			// Token: 0x170072B1 RID: 29361
			// (set) Token: 0x0600A01F RID: 40991 RVA: 0x000E80EF File Offset: 0x000E62EF
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditDelegate
			{
				set
				{
					base.PowerSharpParameters["AuditDelegate"] = value;
				}
			}

			// Token: 0x170072B2 RID: 29362
			// (set) Token: 0x0600A020 RID: 40992 RVA: 0x000E8102 File Offset: 0x000E6302
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditOwner
			{
				set
				{
					base.PowerSharpParameters["AuditOwner"] = value;
				}
			}

			// Token: 0x170072B3 RID: 29363
			// (set) Token: 0x0600A021 RID: 40993 RVA: 0x000E8115 File Offset: 0x000E6315
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x170072B4 RID: 29364
			// (set) Token: 0x0600A022 RID: 40994 RVA: 0x000E8128 File Offset: 0x000E6328
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170072B5 RID: 29365
			// (set) Token: 0x0600A023 RID: 40995 RVA: 0x000E813B File Offset: 0x000E633B
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x170072B6 RID: 29366
			// (set) Token: 0x0600A024 RID: 40996 RVA: 0x000E814E File Offset: 0x000E634E
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x170072B7 RID: 29367
			// (set) Token: 0x0600A025 RID: 40997 RVA: 0x000E8161 File Offset: 0x000E6361
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x170072B8 RID: 29368
			// (set) Token: 0x0600A026 RID: 40998 RVA: 0x000E8174 File Offset: 0x000E6374
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x170072B9 RID: 29369
			// (set) Token: 0x0600A027 RID: 40999 RVA: 0x000E8187 File Offset: 0x000E6387
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x170072BA RID: 29370
			// (set) Token: 0x0600A028 RID: 41000 RVA: 0x000E819A File Offset: 0x000E639A
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x170072BB RID: 29371
			// (set) Token: 0x0600A029 RID: 41001 RVA: 0x000E81AD File Offset: 0x000E63AD
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x170072BC RID: 29372
			// (set) Token: 0x0600A02A RID: 41002 RVA: 0x000E81C0 File Offset: 0x000E63C0
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x170072BD RID: 29373
			// (set) Token: 0x0600A02B RID: 41003 RVA: 0x000E81D3 File Offset: 0x000E63D3
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x170072BE RID: 29374
			// (set) Token: 0x0600A02C RID: 41004 RVA: 0x000E81E6 File Offset: 0x000E63E6
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x170072BF RID: 29375
			// (set) Token: 0x0600A02D RID: 41005 RVA: 0x000E81F9 File Offset: 0x000E63F9
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x170072C0 RID: 29376
			// (set) Token: 0x0600A02E RID: 41006 RVA: 0x000E820C File Offset: 0x000E640C
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x170072C1 RID: 29377
			// (set) Token: 0x0600A02F RID: 41007 RVA: 0x000E821F File Offset: 0x000E641F
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x170072C2 RID: 29378
			// (set) Token: 0x0600A030 RID: 41008 RVA: 0x000E8232 File Offset: 0x000E6432
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x170072C3 RID: 29379
			// (set) Token: 0x0600A031 RID: 41009 RVA: 0x000E8245 File Offset: 0x000E6445
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x170072C4 RID: 29380
			// (set) Token: 0x0600A032 RID: 41010 RVA: 0x000E8258 File Offset: 0x000E6458
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x170072C5 RID: 29381
			// (set) Token: 0x0600A033 RID: 41011 RVA: 0x000E826B File Offset: 0x000E646B
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x170072C6 RID: 29382
			// (set) Token: 0x0600A034 RID: 41012 RVA: 0x000E827E File Offset: 0x000E647E
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x170072C7 RID: 29383
			// (set) Token: 0x0600A035 RID: 41013 RVA: 0x000E8291 File Offset: 0x000E6491
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x170072C8 RID: 29384
			// (set) Token: 0x0600A036 RID: 41014 RVA: 0x000E82A4 File Offset: 0x000E64A4
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x170072C9 RID: 29385
			// (set) Token: 0x0600A037 RID: 41015 RVA: 0x000E82B7 File Offset: 0x000E64B7
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170072CA RID: 29386
			// (set) Token: 0x0600A038 RID: 41016 RVA: 0x000E82CA File Offset: 0x000E64CA
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x170072CB RID: 29387
			// (set) Token: 0x0600A039 RID: 41017 RVA: 0x000E82DD File Offset: 0x000E64DD
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x170072CC RID: 29388
			// (set) Token: 0x0600A03A RID: 41018 RVA: 0x000E82F5 File Offset: 0x000E64F5
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x170072CD RID: 29389
			// (set) Token: 0x0600A03B RID: 41019 RVA: 0x000E830D File Offset: 0x000E650D
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x170072CE RID: 29390
			// (set) Token: 0x0600A03C RID: 41020 RVA: 0x000E8325 File Offset: 0x000E6525
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x170072CF RID: 29391
			// (set) Token: 0x0600A03D RID: 41021 RVA: 0x000E833D File Offset: 0x000E653D
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x170072D0 RID: 29392
			// (set) Token: 0x0600A03E RID: 41022 RVA: 0x000E8355 File Offset: 0x000E6555
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170072D1 RID: 29393
			// (set) Token: 0x0600A03F RID: 41023 RVA: 0x000E836D File Offset: 0x000E656D
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x170072D2 RID: 29394
			// (set) Token: 0x0600A040 RID: 41024 RVA: 0x000E8385 File Offset: 0x000E6585
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x170072D3 RID: 29395
			// (set) Token: 0x0600A041 RID: 41025 RVA: 0x000E8398 File Offset: 0x000E6598
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170072D4 RID: 29396
			// (set) Token: 0x0600A042 RID: 41026 RVA: 0x000E83B0 File Offset: 0x000E65B0
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x170072D5 RID: 29397
			// (set) Token: 0x0600A043 RID: 41027 RVA: 0x000E83C3 File Offset: 0x000E65C3
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x170072D6 RID: 29398
			// (set) Token: 0x0600A044 RID: 41028 RVA: 0x000E83DB File Offset: 0x000E65DB
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x170072D7 RID: 29399
			// (set) Token: 0x0600A045 RID: 41029 RVA: 0x000E83EE File Offset: 0x000E65EE
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x170072D8 RID: 29400
			// (set) Token: 0x0600A046 RID: 41030 RVA: 0x000E8401 File Offset: 0x000E6601
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170072D9 RID: 29401
			// (set) Token: 0x0600A047 RID: 41031 RVA: 0x000E8414 File Offset: 0x000E6614
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170072DA RID: 29402
			// (set) Token: 0x0600A048 RID: 41032 RVA: 0x000E842C File Offset: 0x000E662C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170072DB RID: 29403
			// (set) Token: 0x0600A049 RID: 41033 RVA: 0x000E8444 File Offset: 0x000E6644
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170072DC RID: 29404
			// (set) Token: 0x0600A04A RID: 41034 RVA: 0x000E845C File Offset: 0x000E665C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170072DD RID: 29405
			// (set) Token: 0x0600A04B RID: 41035 RVA: 0x000E8474 File Offset: 0x000E6674
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C93 RID: 3219
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170072DE RID: 29406
			// (set) Token: 0x0600A04D RID: 41037 RVA: 0x000E8494 File Offset: 0x000E6694
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170072DF RID: 29407
			// (set) Token: 0x0600A04E RID: 41038 RVA: 0x000E84B2 File Offset: 0x000E66B2
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170072E0 RID: 29408
			// (set) Token: 0x0600A04F RID: 41039 RVA: 0x000E84D0 File Offset: 0x000E66D0
			public virtual bool EnableRoomMailboxAccount
			{
				set
				{
					base.PowerSharpParameters["EnableRoomMailboxAccount"] = value;
				}
			}

			// Token: 0x170072E1 RID: 29409
			// (set) Token: 0x0600A050 RID: 41040 RVA: 0x000E84E8 File Offset: 0x000E66E8
			public virtual SecureString RoomMailboxPassword
			{
				set
				{
					base.PowerSharpParameters["RoomMailboxPassword"] = value;
				}
			}

			// Token: 0x170072E2 RID: 29410
			// (set) Token: 0x0600A051 RID: 41041 RVA: 0x000E84FB File Offset: 0x000E66FB
			public virtual SwitchParameter SkipMailboxProvisioningConstraintValidation
			{
				set
				{
					base.PowerSharpParameters["SkipMailboxProvisioningConstraintValidation"] = value;
				}
			}

			// Token: 0x170072E3 RID: 29411
			// (set) Token: 0x0600A052 RID: 41042 RVA: 0x000E8513 File Offset: 0x000E6713
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170072E4 RID: 29412
			// (set) Token: 0x0600A053 RID: 41043 RVA: 0x000E8526 File Offset: 0x000E6726
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170072E5 RID: 29413
			// (set) Token: 0x0600A054 RID: 41044 RVA: 0x000E8539 File Offset: 0x000E6739
			public virtual SwitchParameter EvictLiveId
			{
				set
				{
					base.PowerSharpParameters["EvictLiveId"] = value;
				}
			}

			// Token: 0x170072E6 RID: 29414
			// (set) Token: 0x0600A055 RID: 41045 RVA: 0x000E8551 File Offset: 0x000E6751
			public virtual bool RequireSecretQA
			{
				set
				{
					base.PowerSharpParameters["RequireSecretQA"] = value;
				}
			}

			// Token: 0x170072E7 RID: 29415
			// (set) Token: 0x0600A056 RID: 41046 RVA: 0x000E8569 File Offset: 0x000E6769
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x170072E8 RID: 29416
			// (set) Token: 0x0600A057 RID: 41047 RVA: 0x000E857C File Offset: 0x000E677C
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170072E9 RID: 29417
			// (set) Token: 0x0600A058 RID: 41048 RVA: 0x000E858F File Offset: 0x000E678F
			public virtual Unlimited<EnhancedTimeSpan> LitigationHoldDuration
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDuration"] = value;
				}
			}

			// Token: 0x170072EA RID: 29418
			// (set) Token: 0x0600A059 RID: 41049 RVA: 0x000E85A7 File Offset: 0x000E67A7
			public virtual bool UMDataStorage
			{
				set
				{
					base.PowerSharpParameters["UMDataStorage"] = value;
				}
			}

			// Token: 0x170072EB RID: 29419
			// (set) Token: 0x0600A05A RID: 41050 RVA: 0x000E85BF File Offset: 0x000E67BF
			public virtual bool UMGrammar
			{
				set
				{
					base.PowerSharpParameters["UMGrammar"] = value;
				}
			}

			// Token: 0x170072EC RID: 29420
			// (set) Token: 0x0600A05B RID: 41051 RVA: 0x000E85D7 File Offset: 0x000E67D7
			public virtual bool OABGen
			{
				set
				{
					base.PowerSharpParameters["OABGen"] = value;
				}
			}

			// Token: 0x170072ED RID: 29421
			// (set) Token: 0x0600A05C RID: 41052 RVA: 0x000E85EF File Offset: 0x000E67EF
			public virtual bool GMGen
			{
				set
				{
					base.PowerSharpParameters["GMGen"] = value;
				}
			}

			// Token: 0x170072EE RID: 29422
			// (set) Token: 0x0600A05D RID: 41053 RVA: 0x000E8607 File Offset: 0x000E6807
			public virtual bool ClientExtensions
			{
				set
				{
					base.PowerSharpParameters["ClientExtensions"] = value;
				}
			}

			// Token: 0x170072EF RID: 29423
			// (set) Token: 0x0600A05E RID: 41054 RVA: 0x000E861F File Offset: 0x000E681F
			public virtual bool MailRouting
			{
				set
				{
					base.PowerSharpParameters["MailRouting"] = value;
				}
			}

			// Token: 0x170072F0 RID: 29424
			// (set) Token: 0x0600A05F RID: 41055 RVA: 0x000E8637 File Offset: 0x000E6837
			public virtual bool Management
			{
				set
				{
					base.PowerSharpParameters["Management"] = value;
				}
			}

			// Token: 0x170072F1 RID: 29425
			// (set) Token: 0x0600A060 RID: 41056 RVA: 0x000E864F File Offset: 0x000E684F
			public virtual bool TenantUpgrade
			{
				set
				{
					base.PowerSharpParameters["TenantUpgrade"] = value;
				}
			}

			// Token: 0x170072F2 RID: 29426
			// (set) Token: 0x0600A061 RID: 41057 RVA: 0x000E8667 File Offset: 0x000E6867
			public virtual bool Migration
			{
				set
				{
					base.PowerSharpParameters["Migration"] = value;
				}
			}

			// Token: 0x170072F3 RID: 29427
			// (set) Token: 0x0600A062 RID: 41058 RVA: 0x000E867F File Offset: 0x000E687F
			public virtual bool MessageTracking
			{
				set
				{
					base.PowerSharpParameters["MessageTracking"] = value;
				}
			}

			// Token: 0x170072F4 RID: 29428
			// (set) Token: 0x0600A063 RID: 41059 RVA: 0x000E8697 File Offset: 0x000E6897
			public virtual bool OMEncryption
			{
				set
				{
					base.PowerSharpParameters["OMEncryption"] = value;
				}
			}

			// Token: 0x170072F5 RID: 29429
			// (set) Token: 0x0600A064 RID: 41060 RVA: 0x000E86AF File Offset: 0x000E68AF
			public virtual bool PstProvider
			{
				set
				{
					base.PowerSharpParameters["PstProvider"] = value;
				}
			}

			// Token: 0x170072F6 RID: 29430
			// (set) Token: 0x0600A065 RID: 41061 RVA: 0x000E86C7 File Offset: 0x000E68C7
			public virtual bool SuiteServiceStorage
			{
				set
				{
					base.PowerSharpParameters["SuiteServiceStorage"] = value;
				}
			}

			// Token: 0x170072F7 RID: 29431
			// (set) Token: 0x0600A066 RID: 41062 RVA: 0x000E86DF File Offset: 0x000E68DF
			public virtual SecureString OldPassword
			{
				set
				{
					base.PowerSharpParameters["OldPassword"] = value;
				}
			}

			// Token: 0x170072F8 RID: 29432
			// (set) Token: 0x0600A067 RID: 41063 RVA: 0x000E86F2 File Offset: 0x000E68F2
			public virtual SecureString NewPassword
			{
				set
				{
					base.PowerSharpParameters["NewPassword"] = value;
				}
			}

			// Token: 0x170072F9 RID: 29433
			// (set) Token: 0x0600A068 RID: 41064 RVA: 0x000E8705 File Offset: 0x000E6905
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x170072FA RID: 29434
			// (set) Token: 0x0600A069 RID: 41065 RVA: 0x000E8718 File Offset: 0x000E6918
			public virtual string QueryBaseDN
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDN"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170072FB RID: 29435
			// (set) Token: 0x0600A06A RID: 41066 RVA: 0x000E8736 File Offset: 0x000E6936
			public virtual string DefaultPublicFolderMailbox
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMailbox"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x170072FC RID: 29436
			// (set) Token: 0x0600A06B RID: 41067 RVA: 0x000E8754 File Offset: 0x000E6954
			public virtual int? MailboxMessagesPerFolderCountWarningQuota
			{
				set
				{
					base.PowerSharpParameters["MailboxMessagesPerFolderCountWarningQuota"] = value;
				}
			}

			// Token: 0x170072FD RID: 29437
			// (set) Token: 0x0600A06C RID: 41068 RVA: 0x000E876C File Offset: 0x000E696C
			public virtual int? MailboxMessagesPerFolderCountReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["MailboxMessagesPerFolderCountReceiveQuota"] = value;
				}
			}

			// Token: 0x170072FE RID: 29438
			// (set) Token: 0x0600A06D RID: 41069 RVA: 0x000E8784 File Offset: 0x000E6984
			public virtual int? DumpsterMessagesPerFolderCountWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DumpsterMessagesPerFolderCountWarningQuota"] = value;
				}
			}

			// Token: 0x170072FF RID: 29439
			// (set) Token: 0x0600A06E RID: 41070 RVA: 0x000E879C File Offset: 0x000E699C
			public virtual int? DumpsterMessagesPerFolderCountReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["DumpsterMessagesPerFolderCountReceiveQuota"] = value;
				}
			}

			// Token: 0x17007300 RID: 29440
			// (set) Token: 0x0600A06F RID: 41071 RVA: 0x000E87B4 File Offset: 0x000E69B4
			public virtual int? FolderHierarchyChildrenCountWarningQuota
			{
				set
				{
					base.PowerSharpParameters["FolderHierarchyChildrenCountWarningQuota"] = value;
				}
			}

			// Token: 0x17007301 RID: 29441
			// (set) Token: 0x0600A070 RID: 41072 RVA: 0x000E87CC File Offset: 0x000E69CC
			public virtual int? FolderHierarchyChildrenCountReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["FolderHierarchyChildrenCountReceiveQuota"] = value;
				}
			}

			// Token: 0x17007302 RID: 29442
			// (set) Token: 0x0600A071 RID: 41073 RVA: 0x000E87E4 File Offset: 0x000E69E4
			public virtual int? FolderHierarchyDepthWarningQuota
			{
				set
				{
					base.PowerSharpParameters["FolderHierarchyDepthWarningQuota"] = value;
				}
			}

			// Token: 0x17007303 RID: 29443
			// (set) Token: 0x0600A072 RID: 41074 RVA: 0x000E87FC File Offset: 0x000E69FC
			public virtual int? FolderHierarchyDepthReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["FolderHierarchyDepthReceiveQuota"] = value;
				}
			}

			// Token: 0x17007304 RID: 29444
			// (set) Token: 0x0600A073 RID: 41075 RVA: 0x000E8814 File Offset: 0x000E6A14
			public virtual int? FoldersCountWarningQuota
			{
				set
				{
					base.PowerSharpParameters["FoldersCountWarningQuota"] = value;
				}
			}

			// Token: 0x17007305 RID: 29445
			// (set) Token: 0x0600A074 RID: 41076 RVA: 0x000E882C File Offset: 0x000E6A2C
			public virtual int? FoldersCountReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["FoldersCountReceiveQuota"] = value;
				}
			}

			// Token: 0x17007306 RID: 29446
			// (set) Token: 0x0600A075 RID: 41077 RVA: 0x000E8844 File Offset: 0x000E6A44
			public virtual int? ExtendedPropertiesCountQuota
			{
				set
				{
					base.PowerSharpParameters["ExtendedPropertiesCountQuota"] = value;
				}
			}

			// Token: 0x17007307 RID: 29447
			// (set) Token: 0x0600A076 RID: 41078 RVA: 0x000E885C File Offset: 0x000E6A5C
			public virtual Guid? MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x17007308 RID: 29448
			// (set) Token: 0x0600A077 RID: 41079 RVA: 0x000E8874 File Offset: 0x000E6A74
			public virtual CrossTenantObjectId UnifiedMailbox
			{
				set
				{
					base.PowerSharpParameters["UnifiedMailbox"] = value;
				}
			}

			// Token: 0x17007309 RID: 29449
			// (set) Token: 0x0600A078 RID: 41080 RVA: 0x000E8887 File Offset: 0x000E6A87
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x1700730A RID: 29450
			// (set) Token: 0x0600A079 RID: 41081 RVA: 0x000E889F File Offset: 0x000E6A9F
			public virtual bool MessageCopyForSentAsEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageCopyForSentAsEnabled"] = value;
				}
			}

			// Token: 0x1700730B RID: 29451
			// (set) Token: 0x0600A07A RID: 41082 RVA: 0x000E88B7 File Offset: 0x000E6AB7
			public virtual bool MessageCopyForSendOnBehalfEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageCopyForSendOnBehalfEnabled"] = value;
				}
			}

			// Token: 0x1700730C RID: 29452
			// (set) Token: 0x0600A07B RID: 41083 RVA: 0x000E88CF File Offset: 0x000E6ACF
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700730D RID: 29453
			// (set) Token: 0x0600A07C RID: 41084 RVA: 0x000E88E7 File Offset: 0x000E6AE7
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x1700730E RID: 29454
			// (set) Token: 0x0600A07D RID: 41085 RVA: 0x000E88FF File Offset: 0x000E6AFF
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x1700730F RID: 29455
			// (set) Token: 0x0600A07E RID: 41086 RVA: 0x000E8917 File Offset: 0x000E6B17
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007310 RID: 29456
			// (set) Token: 0x0600A07F RID: 41087 RVA: 0x000E8935 File Offset: 0x000E6B35
			public virtual string ForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17007311 RID: 29457
			// (set) Token: 0x0600A080 RID: 41088 RVA: 0x000E8953 File Offset: 0x000E6B53
			public virtual OfflineAddressBookIdParameter OfflineAddressBook
			{
				set
				{
					base.PowerSharpParameters["OfflineAddressBook"] = value;
				}
			}

			// Token: 0x17007312 RID: 29458
			// (set) Token: 0x0600A081 RID: 41089 RVA: 0x000E8966 File Offset: 0x000E6B66
			public virtual string LinkedMasterAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedMasterAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17007313 RID: 29459
			// (set) Token: 0x0600A082 RID: 41090 RVA: 0x000E8984 File Offset: 0x000E6B84
			public virtual string LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x17007314 RID: 29460
			// (set) Token: 0x0600A083 RID: 41091 RVA: 0x000E8997 File Offset: 0x000E6B97
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x17007315 RID: 29461
			// (set) Token: 0x0600A084 RID: 41092 RVA: 0x000E89AA File Offset: 0x000E6BAA
			public virtual ConvertibleMailboxSubType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x17007316 RID: 29462
			// (set) Token: 0x0600A085 RID: 41093 RVA: 0x000E89C2 File Offset: 0x000E6BC2
			public virtual SwitchParameter ApplyMandatoryProperties
			{
				set
				{
					base.PowerSharpParameters["ApplyMandatoryProperties"] = value;
				}
			}

			// Token: 0x17007317 RID: 29463
			// (set) Token: 0x0600A086 RID: 41094 RVA: 0x000E89DA File Offset: 0x000E6BDA
			public virtual SwitchParameter RemoveManagedFolderAndPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoveManagedFolderAndPolicy"] = value;
				}
			}

			// Token: 0x17007318 RID: 29464
			// (set) Token: 0x0600A087 RID: 41095 RVA: 0x000E89F2 File Offset: 0x000E6BF2
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007319 RID: 29465
			// (set) Token: 0x0600A088 RID: 41096 RVA: 0x000E8A10 File Offset: 0x000E6C10
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700731A RID: 29466
			// (set) Token: 0x0600A089 RID: 41097 RVA: 0x000E8A2E File Offset: 0x000E6C2E
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700731B RID: 29467
			// (set) Token: 0x0600A08A RID: 41098 RVA: 0x000E8A4C File Offset: 0x000E6C4C
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x1700731C RID: 29468
			// (set) Token: 0x0600A08B RID: 41099 RVA: 0x000E8A5F File Offset: 0x000E6C5F
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700731D RID: 29469
			// (set) Token: 0x0600A08C RID: 41100 RVA: 0x000E8A7D File Offset: 0x000E6C7D
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x1700731E RID: 29470
			// (set) Token: 0x0600A08D RID: 41101 RVA: 0x000E8A95 File Offset: 0x000E6C95
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x1700731F RID: 29471
			// (set) Token: 0x0600A08E RID: 41102 RVA: 0x000E8AAD File Offset: 0x000E6CAD
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17007320 RID: 29472
			// (set) Token: 0x0600A08F RID: 41103 RVA: 0x000E8AC0 File Offset: 0x000E6CC0
			public virtual SwitchParameter BypassLiveId
			{
				set
				{
					base.PowerSharpParameters["BypassLiveId"] = value;
				}
			}

			// Token: 0x17007321 RID: 29473
			// (set) Token: 0x0600A090 RID: 41104 RVA: 0x000E8AD8 File Offset: 0x000E6CD8
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x17007322 RID: 29474
			// (set) Token: 0x0600A091 RID: 41105 RVA: 0x000E8AEB File Offset: 0x000E6CEB
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007323 RID: 29475
			// (set) Token: 0x0600A092 RID: 41106 RVA: 0x000E8AFE File Offset: 0x000E6CFE
			public virtual string FederatedIdentity
			{
				set
				{
					base.PowerSharpParameters["FederatedIdentity"] = value;
				}
			}

			// Token: 0x17007324 RID: 29476
			// (set) Token: 0x0600A093 RID: 41107 RVA: 0x000E8B11 File Offset: 0x000E6D11
			public virtual string SecondaryAddress
			{
				set
				{
					base.PowerSharpParameters["SecondaryAddress"] = value;
				}
			}

			// Token: 0x17007325 RID: 29477
			// (set) Token: 0x0600A094 RID: 41108 RVA: 0x000E8B24 File Offset: 0x000E6D24
			public virtual string SecondaryDialPlan
			{
				set
				{
					base.PowerSharpParameters["SecondaryDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007326 RID: 29478
			// (set) Token: 0x0600A095 RID: 41109 RVA: 0x000E8B42 File Offset: 0x000E6D42
			public virtual SwitchParameter RemovePicture
			{
				set
				{
					base.PowerSharpParameters["RemovePicture"] = value;
				}
			}

			// Token: 0x17007327 RID: 29479
			// (set) Token: 0x0600A096 RID: 41110 RVA: 0x000E8B5A File Offset: 0x000E6D5A
			public virtual SwitchParameter RemoveSpokenName
			{
				set
				{
					base.PowerSharpParameters["RemoveSpokenName"] = value;
				}
			}

			// Token: 0x17007328 RID: 29480
			// (set) Token: 0x0600A097 RID: 41111 RVA: 0x000E8B72 File Offset: 0x000E6D72
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17007329 RID: 29481
			// (set) Token: 0x0600A098 RID: 41112 RVA: 0x000E8B85 File Offset: 0x000E6D85
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x1700732A RID: 29482
			// (set) Token: 0x0600A099 RID: 41113 RVA: 0x000E8B98 File Offset: 0x000E6D98
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x1700732B RID: 29483
			// (set) Token: 0x0600A09A RID: 41114 RVA: 0x000E8BAB File Offset: 0x000E6DAB
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700732C RID: 29484
			// (set) Token: 0x0600A09B RID: 41115 RVA: 0x000E8BC9 File Offset: 0x000E6DC9
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x1700732D RID: 29485
			// (set) Token: 0x0600A09C RID: 41116 RVA: 0x000E8BDC File Offset: 0x000E6DDC
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700732E RID: 29486
			// (set) Token: 0x0600A09D RID: 41117 RVA: 0x000E8BEF File Offset: 0x000E6DEF
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x1700732F RID: 29487
			// (set) Token: 0x0600A09E RID: 41118 RVA: 0x000E8C02 File Offset: 0x000E6E02
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17007330 RID: 29488
			// (set) Token: 0x0600A09F RID: 41119 RVA: 0x000E8C15 File Offset: 0x000E6E15
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17007331 RID: 29489
			// (set) Token: 0x0600A0A0 RID: 41120 RVA: 0x000E8C28 File Offset: 0x000E6E28
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17007332 RID: 29490
			// (set) Token: 0x0600A0A1 RID: 41121 RVA: 0x000E8C3B File Offset: 0x000E6E3B
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x17007333 RID: 29491
			// (set) Token: 0x0600A0A2 RID: 41122 RVA: 0x000E8C53 File Offset: 0x000E6E53
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17007334 RID: 29492
			// (set) Token: 0x0600A0A3 RID: 41123 RVA: 0x000E8C6B File Offset: 0x000E6E6B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007335 RID: 29493
			// (set) Token: 0x0600A0A4 RID: 41124 RVA: 0x000E8C7E File Offset: 0x000E6E7E
			public virtual bool UseDatabaseRetentionDefaults
			{
				set
				{
					base.PowerSharpParameters["UseDatabaseRetentionDefaults"] = value;
				}
			}

			// Token: 0x17007336 RID: 29494
			// (set) Token: 0x0600A0A5 RID: 41125 RVA: 0x000E8C96 File Offset: 0x000E6E96
			public virtual bool RetainDeletedItemsUntilBackup
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsUntilBackup"] = value;
				}
			}

			// Token: 0x17007337 RID: 29495
			// (set) Token: 0x0600A0A6 RID: 41126 RVA: 0x000E8CAE File Offset: 0x000E6EAE
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17007338 RID: 29496
			// (set) Token: 0x0600A0A7 RID: 41127 RVA: 0x000E8CC6 File Offset: 0x000E6EC6
			public virtual bool IsExcludedFromServingHierarchy
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromServingHierarchy"] = value;
				}
			}

			// Token: 0x17007339 RID: 29497
			// (set) Token: 0x0600A0A8 RID: 41128 RVA: 0x000E8CDE File Offset: 0x000E6EDE
			public virtual bool IsHierarchyReady
			{
				set
				{
					base.PowerSharpParameters["IsHierarchyReady"] = value;
				}
			}

			// Token: 0x1700733A RID: 29498
			// (set) Token: 0x0600A0A9 RID: 41129 RVA: 0x000E8CF6 File Offset: 0x000E6EF6
			public virtual bool LitigationHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldEnabled"] = value;
				}
			}

			// Token: 0x1700733B RID: 29499
			// (set) Token: 0x0600A0AA RID: 41130 RVA: 0x000E8D0E File Offset: 0x000E6F0E
			public virtual bool SingleItemRecoveryEnabled
			{
				set
				{
					base.PowerSharpParameters["SingleItemRecoveryEnabled"] = value;
				}
			}

			// Token: 0x1700733C RID: 29500
			// (set) Token: 0x0600A0AB RID: 41131 RVA: 0x000E8D26 File Offset: 0x000E6F26
			public virtual bool RetentionHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["RetentionHoldEnabled"] = value;
				}
			}

			// Token: 0x1700733D RID: 29501
			// (set) Token: 0x0600A0AC RID: 41132 RVA: 0x000E8D3E File Offset: 0x000E6F3E
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x1700733E RID: 29502
			// (set) Token: 0x0600A0AD RID: 41133 RVA: 0x000E8D56 File Offset: 0x000E6F56
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x1700733F RID: 29503
			// (set) Token: 0x0600A0AE RID: 41134 RVA: 0x000E8D6E File Offset: 0x000E6F6E
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x17007340 RID: 29504
			// (set) Token: 0x0600A0AF RID: 41135 RVA: 0x000E8D81 File Offset: 0x000E6F81
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x17007341 RID: 29505
			// (set) Token: 0x0600A0B0 RID: 41136 RVA: 0x000E8D94 File Offset: 0x000E6F94
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x17007342 RID: 29506
			// (set) Token: 0x0600A0B1 RID: 41137 RVA: 0x000E8DAC File Offset: 0x000E6FAC
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x17007343 RID: 29507
			// (set) Token: 0x0600A0B2 RID: 41138 RVA: 0x000E8DBF File Offset: 0x000E6FBF
			public virtual bool CalendarRepairDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairDisabled"] = value;
				}
			}

			// Token: 0x17007344 RID: 29508
			// (set) Token: 0x0600A0B3 RID: 41139 RVA: 0x000E8DD7 File Offset: 0x000E6FD7
			public virtual bool MessageTrackingReadStatusEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingReadStatusEnabled"] = value;
				}
			}

			// Token: 0x17007345 RID: 29509
			// (set) Token: 0x0600A0B4 RID: 41140 RVA: 0x000E8DEF File Offset: 0x000E6FEF
			public virtual ExternalOofOptions ExternalOofOptions
			{
				set
				{
					base.PowerSharpParameters["ExternalOofOptions"] = value;
				}
			}

			// Token: 0x17007346 RID: 29510
			// (set) Token: 0x0600A0B5 RID: 41141 RVA: 0x000E8E07 File Offset: 0x000E7007
			public virtual ProxyAddress ForwardingSmtpAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingSmtpAddress"] = value;
				}
			}

			// Token: 0x17007347 RID: 29511
			// (set) Token: 0x0600A0B6 RID: 41142 RVA: 0x000E8E1A File Offset: 0x000E701A
			public virtual EnhancedTimeSpan RetainDeletedItemsFor
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsFor"] = value;
				}
			}

			// Token: 0x17007348 RID: 29512
			// (set) Token: 0x0600A0B7 RID: 41143 RVA: 0x000E8E32 File Offset: 0x000E7032
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007349 RID: 29513
			// (set) Token: 0x0600A0B8 RID: 41144 RVA: 0x000E8E45 File Offset: 0x000E7045
			public virtual Unlimited<ByteQuantifiedSize> ProhibitSendQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendQuota"] = value;
				}
			}

			// Token: 0x1700734A RID: 29514
			// (set) Token: 0x0600A0B9 RID: 41145 RVA: 0x000E8E5D File Offset: 0x000E705D
			public virtual Unlimited<ByteQuantifiedSize> ProhibitSendReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendReceiveQuota"] = value;
				}
			}

			// Token: 0x1700734B RID: 29515
			// (set) Token: 0x0600A0BA RID: 41146 RVA: 0x000E8E75 File Offset: 0x000E7075
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsQuota"] = value;
				}
			}

			// Token: 0x1700734C RID: 29516
			// (set) Token: 0x0600A0BB RID: 41147 RVA: 0x000E8E8D File Offset: 0x000E708D
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsWarningQuota"] = value;
				}
			}

			// Token: 0x1700734D RID: 29517
			// (set) Token: 0x0600A0BC RID: 41148 RVA: 0x000E8EA5 File Offset: 0x000E70A5
			public virtual Unlimited<ByteQuantifiedSize> CalendarLoggingQuota
			{
				set
				{
					base.PowerSharpParameters["CalendarLoggingQuota"] = value;
				}
			}

			// Token: 0x1700734E RID: 29518
			// (set) Token: 0x0600A0BD RID: 41149 RVA: 0x000E8EBD File Offset: 0x000E70BD
			public virtual bool DowngradeHighPriorityMessagesEnabled
			{
				set
				{
					base.PowerSharpParameters["DowngradeHighPriorityMessagesEnabled"] = value;
				}
			}

			// Token: 0x1700734F RID: 29519
			// (set) Token: 0x0600A0BE RID: 41150 RVA: 0x000E8ED5 File Offset: 0x000E70D5
			public virtual Unlimited<int> RecipientLimits
			{
				set
				{
					base.PowerSharpParameters["RecipientLimits"] = value;
				}
			}

			// Token: 0x17007350 RID: 29520
			// (set) Token: 0x0600A0BF RID: 41151 RVA: 0x000E8EED File Offset: 0x000E70ED
			public virtual bool ImListMigrationCompleted
			{
				set
				{
					base.PowerSharpParameters["ImListMigrationCompleted"] = value;
				}
			}

			// Token: 0x17007351 RID: 29521
			// (set) Token: 0x0600A0C0 RID: 41152 RVA: 0x000E8F05 File Offset: 0x000E7105
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007352 RID: 29522
			// (set) Token: 0x0600A0C1 RID: 41153 RVA: 0x000E8F1D File Offset: 0x000E711D
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17007353 RID: 29523
			// (set) Token: 0x0600A0C2 RID: 41154 RVA: 0x000E8F35 File Offset: 0x000E7135
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17007354 RID: 29524
			// (set) Token: 0x0600A0C3 RID: 41155 RVA: 0x000E8F48 File Offset: 0x000E7148
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007355 RID: 29525
			// (set) Token: 0x0600A0C4 RID: 41156 RVA: 0x000E8F5B File Offset: 0x000E715B
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x17007356 RID: 29526
			// (set) Token: 0x0600A0C5 RID: 41157 RVA: 0x000E8F73 File Offset: 0x000E7173
			public virtual bool? SCLDeleteEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteEnabled"] = value;
				}
			}

			// Token: 0x17007357 RID: 29527
			// (set) Token: 0x0600A0C6 RID: 41158 RVA: 0x000E8F8B File Offset: 0x000E718B
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x17007358 RID: 29528
			// (set) Token: 0x0600A0C7 RID: 41159 RVA: 0x000E8FA3 File Offset: 0x000E71A3
			public virtual bool? SCLRejectEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLRejectEnabled"] = value;
				}
			}

			// Token: 0x17007359 RID: 29529
			// (set) Token: 0x0600A0C8 RID: 41160 RVA: 0x000E8FBB File Offset: 0x000E71BB
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x1700735A RID: 29530
			// (set) Token: 0x0600A0C9 RID: 41161 RVA: 0x000E8FD3 File Offset: 0x000E71D3
			public virtual bool? SCLQuarantineEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineEnabled"] = value;
				}
			}

			// Token: 0x1700735B RID: 29531
			// (set) Token: 0x0600A0CA RID: 41162 RVA: 0x000E8FEB File Offset: 0x000E71EB
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x1700735C RID: 29532
			// (set) Token: 0x0600A0CB RID: 41163 RVA: 0x000E9003 File Offset: 0x000E7203
			public virtual bool? SCLJunkEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLJunkEnabled"] = value;
				}
			}

			// Token: 0x1700735D RID: 29533
			// (set) Token: 0x0600A0CC RID: 41164 RVA: 0x000E901B File Offset: 0x000E721B
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x1700735E RID: 29534
			// (set) Token: 0x0600A0CD RID: 41165 RVA: 0x000E9033 File Offset: 0x000E7233
			public virtual bool? UseDatabaseQuotaDefaults
			{
				set
				{
					base.PowerSharpParameters["UseDatabaseQuotaDefaults"] = value;
				}
			}

			// Token: 0x1700735F RID: 29535
			// (set) Token: 0x0600A0CE RID: 41166 RVA: 0x000E904B File Offset: 0x000E724B
			public virtual Unlimited<ByteQuantifiedSize> IssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["IssueWarningQuota"] = value;
				}
			}

			// Token: 0x17007360 RID: 29536
			// (set) Token: 0x0600A0CF RID: 41167 RVA: 0x000E9063 File Offset: 0x000E7263
			public virtual ByteQuantifiedSize RulesQuota
			{
				set
				{
					base.PowerSharpParameters["RulesQuota"] = value;
				}
			}

			// Token: 0x17007361 RID: 29537
			// (set) Token: 0x0600A0D0 RID: 41168 RVA: 0x000E907B File Offset: 0x000E727B
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17007362 RID: 29538
			// (set) Token: 0x0600A0D1 RID: 41169 RVA: 0x000E908E File Offset: 0x000E728E
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007363 RID: 29539
			// (set) Token: 0x0600A0D2 RID: 41170 RVA: 0x000E90A1 File Offset: 0x000E72A1
			public virtual int? MaxSafeSenders
			{
				set
				{
					base.PowerSharpParameters["MaxSafeSenders"] = value;
				}
			}

			// Token: 0x17007364 RID: 29540
			// (set) Token: 0x0600A0D3 RID: 41171 RVA: 0x000E90B9 File Offset: 0x000E72B9
			public virtual int? MaxBlockedSenders
			{
				set
				{
					base.PowerSharpParameters["MaxBlockedSenders"] = value;
				}
			}

			// Token: 0x17007365 RID: 29541
			// (set) Token: 0x0600A0D4 RID: 41172 RVA: 0x000E90D1 File Offset: 0x000E72D1
			public virtual SmtpAddress WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17007366 RID: 29542
			// (set) Token: 0x0600A0D5 RID: 41173 RVA: 0x000E90E9 File Offset: 0x000E72E9
			public virtual SmtpAddress MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x17007367 RID: 29543
			// (set) Token: 0x0600A0D6 RID: 41174 RVA: 0x000E9101 File Offset: 0x000E7301
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17007368 RID: 29544
			// (set) Token: 0x0600A0D7 RID: 41175 RVA: 0x000E9114 File Offset: 0x000E7314
			public virtual SmtpAddress JournalArchiveAddress
			{
				set
				{
					base.PowerSharpParameters["JournalArchiveAddress"] = value;
				}
			}

			// Token: 0x17007369 RID: 29545
			// (set) Token: 0x0600A0D8 RID: 41176 RVA: 0x000E912C File Offset: 0x000E732C
			public virtual Unlimited<ByteQuantifiedSize> ArchiveQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveQuota"] = value;
				}
			}

			// Token: 0x1700736A RID: 29546
			// (set) Token: 0x0600A0D9 RID: 41177 RVA: 0x000E9144 File Offset: 0x000E7344
			public virtual Unlimited<ByteQuantifiedSize> ArchiveWarningQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveWarningQuota"] = value;
				}
			}

			// Token: 0x1700736B RID: 29547
			// (set) Token: 0x0600A0DA RID: 41178 RVA: 0x000E915C File Offset: 0x000E735C
			public virtual SmtpDomain ArchiveDomain
			{
				set
				{
					base.PowerSharpParameters["ArchiveDomain"] = value;
				}
			}

			// Token: 0x1700736C RID: 29548
			// (set) Token: 0x0600A0DB RID: 41179 RVA: 0x000E916F File Offset: 0x000E736F
			public virtual ArchiveStatusFlags ArchiveStatus
			{
				set
				{
					base.PowerSharpParameters["ArchiveStatus"] = value;
				}
			}

			// Token: 0x1700736D RID: 29549
			// (set) Token: 0x0600A0DC RID: 41180 RVA: 0x000E9187 File Offset: 0x000E7387
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x1700736E RID: 29550
			// (set) Token: 0x0600A0DD RID: 41181 RVA: 0x000E919F File Offset: 0x000E739F
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x1700736F RID: 29551
			// (set) Token: 0x0600A0DE RID: 41182 RVA: 0x000E91B7 File Offset: 0x000E73B7
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17007370 RID: 29552
			// (set) Token: 0x0600A0DF RID: 41183 RVA: 0x000E91CA File Offset: 0x000E73CA
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17007371 RID: 29553
			// (set) Token: 0x0600A0E0 RID: 41184 RVA: 0x000E91DD File Offset: 0x000E73DD
			public virtual bool CalendarVersionStoreDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarVersionStoreDisabled"] = value;
				}
			}

			// Token: 0x17007372 RID: 29554
			// (set) Token: 0x0600A0E1 RID: 41185 RVA: 0x000E91F5 File Offset: 0x000E73F5
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007373 RID: 29555
			// (set) Token: 0x0600A0E2 RID: 41186 RVA: 0x000E9208 File Offset: 0x000E7408
			public virtual bool? SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17007374 RID: 29556
			// (set) Token: 0x0600A0E3 RID: 41187 RVA: 0x000E9220 File Offset: 0x000E7420
			public virtual bool AuditEnabled
			{
				set
				{
					base.PowerSharpParameters["AuditEnabled"] = value;
				}
			}

			// Token: 0x17007375 RID: 29557
			// (set) Token: 0x0600A0E4 RID: 41188 RVA: 0x000E9238 File Offset: 0x000E7438
			public virtual EnhancedTimeSpan AuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["AuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x17007376 RID: 29558
			// (set) Token: 0x0600A0E5 RID: 41189 RVA: 0x000E9250 File Offset: 0x000E7450
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditAdmin
			{
				set
				{
					base.PowerSharpParameters["AuditAdmin"] = value;
				}
			}

			// Token: 0x17007377 RID: 29559
			// (set) Token: 0x0600A0E6 RID: 41190 RVA: 0x000E9263 File Offset: 0x000E7463
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditDelegate
			{
				set
				{
					base.PowerSharpParameters["AuditDelegate"] = value;
				}
			}

			// Token: 0x17007378 RID: 29560
			// (set) Token: 0x0600A0E7 RID: 41191 RVA: 0x000E9276 File Offset: 0x000E7476
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditOwner
			{
				set
				{
					base.PowerSharpParameters["AuditOwner"] = value;
				}
			}

			// Token: 0x17007379 RID: 29561
			// (set) Token: 0x0600A0E8 RID: 41192 RVA: 0x000E9289 File Offset: 0x000E7489
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x1700737A RID: 29562
			// (set) Token: 0x0600A0E9 RID: 41193 RVA: 0x000E929C File Offset: 0x000E749C
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700737B RID: 29563
			// (set) Token: 0x0600A0EA RID: 41194 RVA: 0x000E92AF File Offset: 0x000E74AF
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x1700737C RID: 29564
			// (set) Token: 0x0600A0EB RID: 41195 RVA: 0x000E92C2 File Offset: 0x000E74C2
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x1700737D RID: 29565
			// (set) Token: 0x0600A0EC RID: 41196 RVA: 0x000E92D5 File Offset: 0x000E74D5
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x1700737E RID: 29566
			// (set) Token: 0x0600A0ED RID: 41197 RVA: 0x000E92E8 File Offset: 0x000E74E8
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x1700737F RID: 29567
			// (set) Token: 0x0600A0EE RID: 41198 RVA: 0x000E92FB File Offset: 0x000E74FB
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17007380 RID: 29568
			// (set) Token: 0x0600A0EF RID: 41199 RVA: 0x000E930E File Offset: 0x000E750E
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17007381 RID: 29569
			// (set) Token: 0x0600A0F0 RID: 41200 RVA: 0x000E9321 File Offset: 0x000E7521
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17007382 RID: 29570
			// (set) Token: 0x0600A0F1 RID: 41201 RVA: 0x000E9334 File Offset: 0x000E7534
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17007383 RID: 29571
			// (set) Token: 0x0600A0F2 RID: 41202 RVA: 0x000E9347 File Offset: 0x000E7547
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17007384 RID: 29572
			// (set) Token: 0x0600A0F3 RID: 41203 RVA: 0x000E935A File Offset: 0x000E755A
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17007385 RID: 29573
			// (set) Token: 0x0600A0F4 RID: 41204 RVA: 0x000E936D File Offset: 0x000E756D
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17007386 RID: 29574
			// (set) Token: 0x0600A0F5 RID: 41205 RVA: 0x000E9380 File Offset: 0x000E7580
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17007387 RID: 29575
			// (set) Token: 0x0600A0F6 RID: 41206 RVA: 0x000E9393 File Offset: 0x000E7593
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17007388 RID: 29576
			// (set) Token: 0x0600A0F7 RID: 41207 RVA: 0x000E93A6 File Offset: 0x000E75A6
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17007389 RID: 29577
			// (set) Token: 0x0600A0F8 RID: 41208 RVA: 0x000E93B9 File Offset: 0x000E75B9
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x1700738A RID: 29578
			// (set) Token: 0x0600A0F9 RID: 41209 RVA: 0x000E93CC File Offset: 0x000E75CC
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x1700738B RID: 29579
			// (set) Token: 0x0600A0FA RID: 41210 RVA: 0x000E93DF File Offset: 0x000E75DF
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x1700738C RID: 29580
			// (set) Token: 0x0600A0FB RID: 41211 RVA: 0x000E93F2 File Offset: 0x000E75F2
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x1700738D RID: 29581
			// (set) Token: 0x0600A0FC RID: 41212 RVA: 0x000E9405 File Offset: 0x000E7605
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x1700738E RID: 29582
			// (set) Token: 0x0600A0FD RID: 41213 RVA: 0x000E9418 File Offset: 0x000E7618
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x1700738F RID: 29583
			// (set) Token: 0x0600A0FE RID: 41214 RVA: 0x000E942B File Offset: 0x000E762B
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007390 RID: 29584
			// (set) Token: 0x0600A0FF RID: 41215 RVA: 0x000E943E File Offset: 0x000E763E
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17007391 RID: 29585
			// (set) Token: 0x0600A100 RID: 41216 RVA: 0x000E9451 File Offset: 0x000E7651
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17007392 RID: 29586
			// (set) Token: 0x0600A101 RID: 41217 RVA: 0x000E9469 File Offset: 0x000E7669
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x17007393 RID: 29587
			// (set) Token: 0x0600A102 RID: 41218 RVA: 0x000E9481 File Offset: 0x000E7681
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x17007394 RID: 29588
			// (set) Token: 0x0600A103 RID: 41219 RVA: 0x000E9499 File Offset: 0x000E7699
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007395 RID: 29589
			// (set) Token: 0x0600A104 RID: 41220 RVA: 0x000E94B1 File Offset: 0x000E76B1
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x17007396 RID: 29590
			// (set) Token: 0x0600A105 RID: 41221 RVA: 0x000E94C9 File Offset: 0x000E76C9
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007397 RID: 29591
			// (set) Token: 0x0600A106 RID: 41222 RVA: 0x000E94E1 File Offset: 0x000E76E1
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17007398 RID: 29592
			// (set) Token: 0x0600A107 RID: 41223 RVA: 0x000E94F9 File Offset: 0x000E76F9
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x17007399 RID: 29593
			// (set) Token: 0x0600A108 RID: 41224 RVA: 0x000E950C File Offset: 0x000E770C
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700739A RID: 29594
			// (set) Token: 0x0600A109 RID: 41225 RVA: 0x000E9524 File Offset: 0x000E7724
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x1700739B RID: 29595
			// (set) Token: 0x0600A10A RID: 41226 RVA: 0x000E9537 File Offset: 0x000E7737
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x1700739C RID: 29596
			// (set) Token: 0x0600A10B RID: 41227 RVA: 0x000E954F File Offset: 0x000E774F
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x1700739D RID: 29597
			// (set) Token: 0x0600A10C RID: 41228 RVA: 0x000E9562 File Offset: 0x000E7762
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x1700739E RID: 29598
			// (set) Token: 0x0600A10D RID: 41229 RVA: 0x000E9575 File Offset: 0x000E7775
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700739F RID: 29599
			// (set) Token: 0x0600A10E RID: 41230 RVA: 0x000E9588 File Offset: 0x000E7788
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170073A0 RID: 29600
			// (set) Token: 0x0600A10F RID: 41231 RVA: 0x000E95A0 File Offset: 0x000E77A0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170073A1 RID: 29601
			// (set) Token: 0x0600A110 RID: 41232 RVA: 0x000E95B8 File Offset: 0x000E77B8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170073A2 RID: 29602
			// (set) Token: 0x0600A111 RID: 41233 RVA: 0x000E95D0 File Offset: 0x000E77D0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170073A3 RID: 29603
			// (set) Token: 0x0600A112 RID: 41234 RVA: 0x000E95E8 File Offset: 0x000E77E8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C94 RID: 3220
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170073A4 RID: 29604
			// (set) Token: 0x0600A114 RID: 41236 RVA: 0x000E9608 File Offset: 0x000E7808
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170073A5 RID: 29605
			// (set) Token: 0x0600A115 RID: 41237 RVA: 0x000E9626 File Offset: 0x000E7826
			public virtual bool EnableRoomMailboxAccount
			{
				set
				{
					base.PowerSharpParameters["EnableRoomMailboxAccount"] = value;
				}
			}

			// Token: 0x170073A6 RID: 29606
			// (set) Token: 0x0600A116 RID: 41238 RVA: 0x000E963E File Offset: 0x000E783E
			public virtual SecureString RoomMailboxPassword
			{
				set
				{
					base.PowerSharpParameters["RoomMailboxPassword"] = value;
				}
			}

			// Token: 0x170073A7 RID: 29607
			// (set) Token: 0x0600A117 RID: 41239 RVA: 0x000E9651 File Offset: 0x000E7851
			public virtual SwitchParameter SkipMailboxProvisioningConstraintValidation
			{
				set
				{
					base.PowerSharpParameters["SkipMailboxProvisioningConstraintValidation"] = value;
				}
			}

			// Token: 0x170073A8 RID: 29608
			// (set) Token: 0x0600A118 RID: 41240 RVA: 0x000E9669 File Offset: 0x000E7869
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170073A9 RID: 29609
			// (set) Token: 0x0600A119 RID: 41241 RVA: 0x000E967C File Offset: 0x000E787C
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170073AA RID: 29610
			// (set) Token: 0x0600A11A RID: 41242 RVA: 0x000E968F File Offset: 0x000E788F
			public virtual SwitchParameter EvictLiveId
			{
				set
				{
					base.PowerSharpParameters["EvictLiveId"] = value;
				}
			}

			// Token: 0x170073AB RID: 29611
			// (set) Token: 0x0600A11B RID: 41243 RVA: 0x000E96A7 File Offset: 0x000E78A7
			public virtual bool RequireSecretQA
			{
				set
				{
					base.PowerSharpParameters["RequireSecretQA"] = value;
				}
			}

			// Token: 0x170073AC RID: 29612
			// (set) Token: 0x0600A11C RID: 41244 RVA: 0x000E96BF File Offset: 0x000E78BF
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x170073AD RID: 29613
			// (set) Token: 0x0600A11D RID: 41245 RVA: 0x000E96D2 File Offset: 0x000E78D2
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170073AE RID: 29614
			// (set) Token: 0x0600A11E RID: 41246 RVA: 0x000E96E5 File Offset: 0x000E78E5
			public virtual Unlimited<EnhancedTimeSpan> LitigationHoldDuration
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDuration"] = value;
				}
			}

			// Token: 0x170073AF RID: 29615
			// (set) Token: 0x0600A11F RID: 41247 RVA: 0x000E96FD File Offset: 0x000E78FD
			public virtual bool UMDataStorage
			{
				set
				{
					base.PowerSharpParameters["UMDataStorage"] = value;
				}
			}

			// Token: 0x170073B0 RID: 29616
			// (set) Token: 0x0600A120 RID: 41248 RVA: 0x000E9715 File Offset: 0x000E7915
			public virtual bool UMGrammar
			{
				set
				{
					base.PowerSharpParameters["UMGrammar"] = value;
				}
			}

			// Token: 0x170073B1 RID: 29617
			// (set) Token: 0x0600A121 RID: 41249 RVA: 0x000E972D File Offset: 0x000E792D
			public virtual bool OABGen
			{
				set
				{
					base.PowerSharpParameters["OABGen"] = value;
				}
			}

			// Token: 0x170073B2 RID: 29618
			// (set) Token: 0x0600A122 RID: 41250 RVA: 0x000E9745 File Offset: 0x000E7945
			public virtual bool GMGen
			{
				set
				{
					base.PowerSharpParameters["GMGen"] = value;
				}
			}

			// Token: 0x170073B3 RID: 29619
			// (set) Token: 0x0600A123 RID: 41251 RVA: 0x000E975D File Offset: 0x000E795D
			public virtual bool ClientExtensions
			{
				set
				{
					base.PowerSharpParameters["ClientExtensions"] = value;
				}
			}

			// Token: 0x170073B4 RID: 29620
			// (set) Token: 0x0600A124 RID: 41252 RVA: 0x000E9775 File Offset: 0x000E7975
			public virtual bool MailRouting
			{
				set
				{
					base.PowerSharpParameters["MailRouting"] = value;
				}
			}

			// Token: 0x170073B5 RID: 29621
			// (set) Token: 0x0600A125 RID: 41253 RVA: 0x000E978D File Offset: 0x000E798D
			public virtual bool Management
			{
				set
				{
					base.PowerSharpParameters["Management"] = value;
				}
			}

			// Token: 0x170073B6 RID: 29622
			// (set) Token: 0x0600A126 RID: 41254 RVA: 0x000E97A5 File Offset: 0x000E79A5
			public virtual bool TenantUpgrade
			{
				set
				{
					base.PowerSharpParameters["TenantUpgrade"] = value;
				}
			}

			// Token: 0x170073B7 RID: 29623
			// (set) Token: 0x0600A127 RID: 41255 RVA: 0x000E97BD File Offset: 0x000E79BD
			public virtual bool Migration
			{
				set
				{
					base.PowerSharpParameters["Migration"] = value;
				}
			}

			// Token: 0x170073B8 RID: 29624
			// (set) Token: 0x0600A128 RID: 41256 RVA: 0x000E97D5 File Offset: 0x000E79D5
			public virtual bool MessageTracking
			{
				set
				{
					base.PowerSharpParameters["MessageTracking"] = value;
				}
			}

			// Token: 0x170073B9 RID: 29625
			// (set) Token: 0x0600A129 RID: 41257 RVA: 0x000E97ED File Offset: 0x000E79ED
			public virtual bool OMEncryption
			{
				set
				{
					base.PowerSharpParameters["OMEncryption"] = value;
				}
			}

			// Token: 0x170073BA RID: 29626
			// (set) Token: 0x0600A12A RID: 41258 RVA: 0x000E9805 File Offset: 0x000E7A05
			public virtual bool PstProvider
			{
				set
				{
					base.PowerSharpParameters["PstProvider"] = value;
				}
			}

			// Token: 0x170073BB RID: 29627
			// (set) Token: 0x0600A12B RID: 41259 RVA: 0x000E981D File Offset: 0x000E7A1D
			public virtual bool SuiteServiceStorage
			{
				set
				{
					base.PowerSharpParameters["SuiteServiceStorage"] = value;
				}
			}

			// Token: 0x170073BC RID: 29628
			// (set) Token: 0x0600A12C RID: 41260 RVA: 0x000E9835 File Offset: 0x000E7A35
			public virtual SecureString OldPassword
			{
				set
				{
					base.PowerSharpParameters["OldPassword"] = value;
				}
			}

			// Token: 0x170073BD RID: 29629
			// (set) Token: 0x0600A12D RID: 41261 RVA: 0x000E9848 File Offset: 0x000E7A48
			public virtual SecureString NewPassword
			{
				set
				{
					base.PowerSharpParameters["NewPassword"] = value;
				}
			}

			// Token: 0x170073BE RID: 29630
			// (set) Token: 0x0600A12E RID: 41262 RVA: 0x000E985B File Offset: 0x000E7A5B
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x170073BF RID: 29631
			// (set) Token: 0x0600A12F RID: 41263 RVA: 0x000E986E File Offset: 0x000E7A6E
			public virtual string QueryBaseDN
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDN"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170073C0 RID: 29632
			// (set) Token: 0x0600A130 RID: 41264 RVA: 0x000E988C File Offset: 0x000E7A8C
			public virtual string DefaultPublicFolderMailbox
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMailbox"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x170073C1 RID: 29633
			// (set) Token: 0x0600A131 RID: 41265 RVA: 0x000E98AA File Offset: 0x000E7AAA
			public virtual int? MailboxMessagesPerFolderCountWarningQuota
			{
				set
				{
					base.PowerSharpParameters["MailboxMessagesPerFolderCountWarningQuota"] = value;
				}
			}

			// Token: 0x170073C2 RID: 29634
			// (set) Token: 0x0600A132 RID: 41266 RVA: 0x000E98C2 File Offset: 0x000E7AC2
			public virtual int? MailboxMessagesPerFolderCountReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["MailboxMessagesPerFolderCountReceiveQuota"] = value;
				}
			}

			// Token: 0x170073C3 RID: 29635
			// (set) Token: 0x0600A133 RID: 41267 RVA: 0x000E98DA File Offset: 0x000E7ADA
			public virtual int? DumpsterMessagesPerFolderCountWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DumpsterMessagesPerFolderCountWarningQuota"] = value;
				}
			}

			// Token: 0x170073C4 RID: 29636
			// (set) Token: 0x0600A134 RID: 41268 RVA: 0x000E98F2 File Offset: 0x000E7AF2
			public virtual int? DumpsterMessagesPerFolderCountReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["DumpsterMessagesPerFolderCountReceiveQuota"] = value;
				}
			}

			// Token: 0x170073C5 RID: 29637
			// (set) Token: 0x0600A135 RID: 41269 RVA: 0x000E990A File Offset: 0x000E7B0A
			public virtual int? FolderHierarchyChildrenCountWarningQuota
			{
				set
				{
					base.PowerSharpParameters["FolderHierarchyChildrenCountWarningQuota"] = value;
				}
			}

			// Token: 0x170073C6 RID: 29638
			// (set) Token: 0x0600A136 RID: 41270 RVA: 0x000E9922 File Offset: 0x000E7B22
			public virtual int? FolderHierarchyChildrenCountReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["FolderHierarchyChildrenCountReceiveQuota"] = value;
				}
			}

			// Token: 0x170073C7 RID: 29639
			// (set) Token: 0x0600A137 RID: 41271 RVA: 0x000E993A File Offset: 0x000E7B3A
			public virtual int? FolderHierarchyDepthWarningQuota
			{
				set
				{
					base.PowerSharpParameters["FolderHierarchyDepthWarningQuota"] = value;
				}
			}

			// Token: 0x170073C8 RID: 29640
			// (set) Token: 0x0600A138 RID: 41272 RVA: 0x000E9952 File Offset: 0x000E7B52
			public virtual int? FolderHierarchyDepthReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["FolderHierarchyDepthReceiveQuota"] = value;
				}
			}

			// Token: 0x170073C9 RID: 29641
			// (set) Token: 0x0600A139 RID: 41273 RVA: 0x000E996A File Offset: 0x000E7B6A
			public virtual int? FoldersCountWarningQuota
			{
				set
				{
					base.PowerSharpParameters["FoldersCountWarningQuota"] = value;
				}
			}

			// Token: 0x170073CA RID: 29642
			// (set) Token: 0x0600A13A RID: 41274 RVA: 0x000E9982 File Offset: 0x000E7B82
			public virtual int? FoldersCountReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["FoldersCountReceiveQuota"] = value;
				}
			}

			// Token: 0x170073CB RID: 29643
			// (set) Token: 0x0600A13B RID: 41275 RVA: 0x000E999A File Offset: 0x000E7B9A
			public virtual int? ExtendedPropertiesCountQuota
			{
				set
				{
					base.PowerSharpParameters["ExtendedPropertiesCountQuota"] = value;
				}
			}

			// Token: 0x170073CC RID: 29644
			// (set) Token: 0x0600A13C RID: 41276 RVA: 0x000E99B2 File Offset: 0x000E7BB2
			public virtual Guid? MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x170073CD RID: 29645
			// (set) Token: 0x0600A13D RID: 41277 RVA: 0x000E99CA File Offset: 0x000E7BCA
			public virtual CrossTenantObjectId UnifiedMailbox
			{
				set
				{
					base.PowerSharpParameters["UnifiedMailbox"] = value;
				}
			}

			// Token: 0x170073CE RID: 29646
			// (set) Token: 0x0600A13E RID: 41278 RVA: 0x000E99DD File Offset: 0x000E7BDD
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x170073CF RID: 29647
			// (set) Token: 0x0600A13F RID: 41279 RVA: 0x000E99F5 File Offset: 0x000E7BF5
			public virtual bool MessageCopyForSentAsEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageCopyForSentAsEnabled"] = value;
				}
			}

			// Token: 0x170073D0 RID: 29648
			// (set) Token: 0x0600A140 RID: 41280 RVA: 0x000E9A0D File Offset: 0x000E7C0D
			public virtual bool MessageCopyForSendOnBehalfEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageCopyForSendOnBehalfEnabled"] = value;
				}
			}

			// Token: 0x170073D1 RID: 29649
			// (set) Token: 0x0600A141 RID: 41281 RVA: 0x000E9A25 File Offset: 0x000E7C25
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170073D2 RID: 29650
			// (set) Token: 0x0600A142 RID: 41282 RVA: 0x000E9A3D File Offset: 0x000E7C3D
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x170073D3 RID: 29651
			// (set) Token: 0x0600A143 RID: 41283 RVA: 0x000E9A55 File Offset: 0x000E7C55
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x170073D4 RID: 29652
			// (set) Token: 0x0600A144 RID: 41284 RVA: 0x000E9A6D File Offset: 0x000E7C6D
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170073D5 RID: 29653
			// (set) Token: 0x0600A145 RID: 41285 RVA: 0x000E9A8B File Offset: 0x000E7C8B
			public virtual string ForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x170073D6 RID: 29654
			// (set) Token: 0x0600A146 RID: 41286 RVA: 0x000E9AA9 File Offset: 0x000E7CA9
			public virtual OfflineAddressBookIdParameter OfflineAddressBook
			{
				set
				{
					base.PowerSharpParameters["OfflineAddressBook"] = value;
				}
			}

			// Token: 0x170073D7 RID: 29655
			// (set) Token: 0x0600A147 RID: 41287 RVA: 0x000E9ABC File Offset: 0x000E7CBC
			public virtual string LinkedMasterAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedMasterAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x170073D8 RID: 29656
			// (set) Token: 0x0600A148 RID: 41288 RVA: 0x000E9ADA File Offset: 0x000E7CDA
			public virtual string LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x170073D9 RID: 29657
			// (set) Token: 0x0600A149 RID: 41289 RVA: 0x000E9AED File Offset: 0x000E7CED
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x170073DA RID: 29658
			// (set) Token: 0x0600A14A RID: 41290 RVA: 0x000E9B00 File Offset: 0x000E7D00
			public virtual ConvertibleMailboxSubType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x170073DB RID: 29659
			// (set) Token: 0x0600A14B RID: 41291 RVA: 0x000E9B18 File Offset: 0x000E7D18
			public virtual SwitchParameter ApplyMandatoryProperties
			{
				set
				{
					base.PowerSharpParameters["ApplyMandatoryProperties"] = value;
				}
			}

			// Token: 0x170073DC RID: 29660
			// (set) Token: 0x0600A14C RID: 41292 RVA: 0x000E9B30 File Offset: 0x000E7D30
			public virtual SwitchParameter RemoveManagedFolderAndPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoveManagedFolderAndPolicy"] = value;
				}
			}

			// Token: 0x170073DD RID: 29661
			// (set) Token: 0x0600A14D RID: 41293 RVA: 0x000E9B48 File Offset: 0x000E7D48
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170073DE RID: 29662
			// (set) Token: 0x0600A14E RID: 41294 RVA: 0x000E9B66 File Offset: 0x000E7D66
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170073DF RID: 29663
			// (set) Token: 0x0600A14F RID: 41295 RVA: 0x000E9B84 File Offset: 0x000E7D84
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170073E0 RID: 29664
			// (set) Token: 0x0600A150 RID: 41296 RVA: 0x000E9BA2 File Offset: 0x000E7DA2
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x170073E1 RID: 29665
			// (set) Token: 0x0600A151 RID: 41297 RVA: 0x000E9BB5 File Offset: 0x000E7DB5
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170073E2 RID: 29666
			// (set) Token: 0x0600A152 RID: 41298 RVA: 0x000E9BD3 File Offset: 0x000E7DD3
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x170073E3 RID: 29667
			// (set) Token: 0x0600A153 RID: 41299 RVA: 0x000E9BEB File Offset: 0x000E7DEB
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x170073E4 RID: 29668
			// (set) Token: 0x0600A154 RID: 41300 RVA: 0x000E9C03 File Offset: 0x000E7E03
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x170073E5 RID: 29669
			// (set) Token: 0x0600A155 RID: 41301 RVA: 0x000E9C16 File Offset: 0x000E7E16
			public virtual SwitchParameter BypassLiveId
			{
				set
				{
					base.PowerSharpParameters["BypassLiveId"] = value;
				}
			}

			// Token: 0x170073E6 RID: 29670
			// (set) Token: 0x0600A156 RID: 41302 RVA: 0x000E9C2E File Offset: 0x000E7E2E
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x170073E7 RID: 29671
			// (set) Token: 0x0600A157 RID: 41303 RVA: 0x000E9C41 File Offset: 0x000E7E41
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x170073E8 RID: 29672
			// (set) Token: 0x0600A158 RID: 41304 RVA: 0x000E9C54 File Offset: 0x000E7E54
			public virtual string FederatedIdentity
			{
				set
				{
					base.PowerSharpParameters["FederatedIdentity"] = value;
				}
			}

			// Token: 0x170073E9 RID: 29673
			// (set) Token: 0x0600A159 RID: 41305 RVA: 0x000E9C67 File Offset: 0x000E7E67
			public virtual string SecondaryAddress
			{
				set
				{
					base.PowerSharpParameters["SecondaryAddress"] = value;
				}
			}

			// Token: 0x170073EA RID: 29674
			// (set) Token: 0x0600A15A RID: 41306 RVA: 0x000E9C7A File Offset: 0x000E7E7A
			public virtual string SecondaryDialPlan
			{
				set
				{
					base.PowerSharpParameters["SecondaryDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170073EB RID: 29675
			// (set) Token: 0x0600A15B RID: 41307 RVA: 0x000E9C98 File Offset: 0x000E7E98
			public virtual SwitchParameter RemovePicture
			{
				set
				{
					base.PowerSharpParameters["RemovePicture"] = value;
				}
			}

			// Token: 0x170073EC RID: 29676
			// (set) Token: 0x0600A15C RID: 41308 RVA: 0x000E9CB0 File Offset: 0x000E7EB0
			public virtual SwitchParameter RemoveSpokenName
			{
				set
				{
					base.PowerSharpParameters["RemoveSpokenName"] = value;
				}
			}

			// Token: 0x170073ED RID: 29677
			// (set) Token: 0x0600A15D RID: 41309 RVA: 0x000E9CC8 File Offset: 0x000E7EC8
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x170073EE RID: 29678
			// (set) Token: 0x0600A15E RID: 41310 RVA: 0x000E9CDB File Offset: 0x000E7EDB
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x170073EF RID: 29679
			// (set) Token: 0x0600A15F RID: 41311 RVA: 0x000E9CEE File Offset: 0x000E7EEE
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x170073F0 RID: 29680
			// (set) Token: 0x0600A160 RID: 41312 RVA: 0x000E9D01 File Offset: 0x000E7F01
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170073F1 RID: 29681
			// (set) Token: 0x0600A161 RID: 41313 RVA: 0x000E9D1F File Offset: 0x000E7F1F
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x170073F2 RID: 29682
			// (set) Token: 0x0600A162 RID: 41314 RVA: 0x000E9D32 File Offset: 0x000E7F32
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170073F3 RID: 29683
			// (set) Token: 0x0600A163 RID: 41315 RVA: 0x000E9D45 File Offset: 0x000E7F45
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x170073F4 RID: 29684
			// (set) Token: 0x0600A164 RID: 41316 RVA: 0x000E9D58 File Offset: 0x000E7F58
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x170073F5 RID: 29685
			// (set) Token: 0x0600A165 RID: 41317 RVA: 0x000E9D6B File Offset: 0x000E7F6B
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x170073F6 RID: 29686
			// (set) Token: 0x0600A166 RID: 41318 RVA: 0x000E9D7E File Offset: 0x000E7F7E
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x170073F7 RID: 29687
			// (set) Token: 0x0600A167 RID: 41319 RVA: 0x000E9D91 File Offset: 0x000E7F91
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x170073F8 RID: 29688
			// (set) Token: 0x0600A168 RID: 41320 RVA: 0x000E9DA9 File Offset: 0x000E7FA9
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170073F9 RID: 29689
			// (set) Token: 0x0600A169 RID: 41321 RVA: 0x000E9DC1 File Offset: 0x000E7FC1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170073FA RID: 29690
			// (set) Token: 0x0600A16A RID: 41322 RVA: 0x000E9DD4 File Offset: 0x000E7FD4
			public virtual bool UseDatabaseRetentionDefaults
			{
				set
				{
					base.PowerSharpParameters["UseDatabaseRetentionDefaults"] = value;
				}
			}

			// Token: 0x170073FB RID: 29691
			// (set) Token: 0x0600A16B RID: 41323 RVA: 0x000E9DEC File Offset: 0x000E7FEC
			public virtual bool RetainDeletedItemsUntilBackup
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsUntilBackup"] = value;
				}
			}

			// Token: 0x170073FC RID: 29692
			// (set) Token: 0x0600A16C RID: 41324 RVA: 0x000E9E04 File Offset: 0x000E8004
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x170073FD RID: 29693
			// (set) Token: 0x0600A16D RID: 41325 RVA: 0x000E9E1C File Offset: 0x000E801C
			public virtual bool IsExcludedFromServingHierarchy
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromServingHierarchy"] = value;
				}
			}

			// Token: 0x170073FE RID: 29694
			// (set) Token: 0x0600A16E RID: 41326 RVA: 0x000E9E34 File Offset: 0x000E8034
			public virtual bool IsHierarchyReady
			{
				set
				{
					base.PowerSharpParameters["IsHierarchyReady"] = value;
				}
			}

			// Token: 0x170073FF RID: 29695
			// (set) Token: 0x0600A16F RID: 41327 RVA: 0x000E9E4C File Offset: 0x000E804C
			public virtual bool LitigationHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldEnabled"] = value;
				}
			}

			// Token: 0x17007400 RID: 29696
			// (set) Token: 0x0600A170 RID: 41328 RVA: 0x000E9E64 File Offset: 0x000E8064
			public virtual bool SingleItemRecoveryEnabled
			{
				set
				{
					base.PowerSharpParameters["SingleItemRecoveryEnabled"] = value;
				}
			}

			// Token: 0x17007401 RID: 29697
			// (set) Token: 0x0600A171 RID: 41329 RVA: 0x000E9E7C File Offset: 0x000E807C
			public virtual bool RetentionHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["RetentionHoldEnabled"] = value;
				}
			}

			// Token: 0x17007402 RID: 29698
			// (set) Token: 0x0600A172 RID: 41330 RVA: 0x000E9E94 File Offset: 0x000E8094
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17007403 RID: 29699
			// (set) Token: 0x0600A173 RID: 41331 RVA: 0x000E9EAC File Offset: 0x000E80AC
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17007404 RID: 29700
			// (set) Token: 0x0600A174 RID: 41332 RVA: 0x000E9EC4 File Offset: 0x000E80C4
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x17007405 RID: 29701
			// (set) Token: 0x0600A175 RID: 41333 RVA: 0x000E9ED7 File Offset: 0x000E80D7
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x17007406 RID: 29702
			// (set) Token: 0x0600A176 RID: 41334 RVA: 0x000E9EEA File Offset: 0x000E80EA
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x17007407 RID: 29703
			// (set) Token: 0x0600A177 RID: 41335 RVA: 0x000E9F02 File Offset: 0x000E8102
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x17007408 RID: 29704
			// (set) Token: 0x0600A178 RID: 41336 RVA: 0x000E9F15 File Offset: 0x000E8115
			public virtual bool CalendarRepairDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarRepairDisabled"] = value;
				}
			}

			// Token: 0x17007409 RID: 29705
			// (set) Token: 0x0600A179 RID: 41337 RVA: 0x000E9F2D File Offset: 0x000E812D
			public virtual bool MessageTrackingReadStatusEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingReadStatusEnabled"] = value;
				}
			}

			// Token: 0x1700740A RID: 29706
			// (set) Token: 0x0600A17A RID: 41338 RVA: 0x000E9F45 File Offset: 0x000E8145
			public virtual ExternalOofOptions ExternalOofOptions
			{
				set
				{
					base.PowerSharpParameters["ExternalOofOptions"] = value;
				}
			}

			// Token: 0x1700740B RID: 29707
			// (set) Token: 0x0600A17B RID: 41339 RVA: 0x000E9F5D File Offset: 0x000E815D
			public virtual ProxyAddress ForwardingSmtpAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingSmtpAddress"] = value;
				}
			}

			// Token: 0x1700740C RID: 29708
			// (set) Token: 0x0600A17C RID: 41340 RVA: 0x000E9F70 File Offset: 0x000E8170
			public virtual EnhancedTimeSpan RetainDeletedItemsFor
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsFor"] = value;
				}
			}

			// Token: 0x1700740D RID: 29709
			// (set) Token: 0x0600A17D RID: 41341 RVA: 0x000E9F88 File Offset: 0x000E8188
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700740E RID: 29710
			// (set) Token: 0x0600A17E RID: 41342 RVA: 0x000E9F9B File Offset: 0x000E819B
			public virtual Unlimited<ByteQuantifiedSize> ProhibitSendQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendQuota"] = value;
				}
			}

			// Token: 0x1700740F RID: 29711
			// (set) Token: 0x0600A17F RID: 41343 RVA: 0x000E9FB3 File Offset: 0x000E81B3
			public virtual Unlimited<ByteQuantifiedSize> ProhibitSendReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendReceiveQuota"] = value;
				}
			}

			// Token: 0x17007410 RID: 29712
			// (set) Token: 0x0600A180 RID: 41344 RVA: 0x000E9FCB File Offset: 0x000E81CB
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsQuota"] = value;
				}
			}

			// Token: 0x17007411 RID: 29713
			// (set) Token: 0x0600A181 RID: 41345 RVA: 0x000E9FE3 File Offset: 0x000E81E3
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsWarningQuota"] = value;
				}
			}

			// Token: 0x17007412 RID: 29714
			// (set) Token: 0x0600A182 RID: 41346 RVA: 0x000E9FFB File Offset: 0x000E81FB
			public virtual Unlimited<ByteQuantifiedSize> CalendarLoggingQuota
			{
				set
				{
					base.PowerSharpParameters["CalendarLoggingQuota"] = value;
				}
			}

			// Token: 0x17007413 RID: 29715
			// (set) Token: 0x0600A183 RID: 41347 RVA: 0x000EA013 File Offset: 0x000E8213
			public virtual bool DowngradeHighPriorityMessagesEnabled
			{
				set
				{
					base.PowerSharpParameters["DowngradeHighPriorityMessagesEnabled"] = value;
				}
			}

			// Token: 0x17007414 RID: 29716
			// (set) Token: 0x0600A184 RID: 41348 RVA: 0x000EA02B File Offset: 0x000E822B
			public virtual Unlimited<int> RecipientLimits
			{
				set
				{
					base.PowerSharpParameters["RecipientLimits"] = value;
				}
			}

			// Token: 0x17007415 RID: 29717
			// (set) Token: 0x0600A185 RID: 41349 RVA: 0x000EA043 File Offset: 0x000E8243
			public virtual bool ImListMigrationCompleted
			{
				set
				{
					base.PowerSharpParameters["ImListMigrationCompleted"] = value;
				}
			}

			// Token: 0x17007416 RID: 29718
			// (set) Token: 0x0600A186 RID: 41350 RVA: 0x000EA05B File Offset: 0x000E825B
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007417 RID: 29719
			// (set) Token: 0x0600A187 RID: 41351 RVA: 0x000EA073 File Offset: 0x000E8273
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17007418 RID: 29720
			// (set) Token: 0x0600A188 RID: 41352 RVA: 0x000EA08B File Offset: 0x000E828B
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17007419 RID: 29721
			// (set) Token: 0x0600A189 RID: 41353 RVA: 0x000EA09E File Offset: 0x000E829E
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700741A RID: 29722
			// (set) Token: 0x0600A18A RID: 41354 RVA: 0x000EA0B1 File Offset: 0x000E82B1
			public virtual int? SCLDeleteThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteThreshold"] = value;
				}
			}

			// Token: 0x1700741B RID: 29723
			// (set) Token: 0x0600A18B RID: 41355 RVA: 0x000EA0C9 File Offset: 0x000E82C9
			public virtual bool? SCLDeleteEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLDeleteEnabled"] = value;
				}
			}

			// Token: 0x1700741C RID: 29724
			// (set) Token: 0x0600A18C RID: 41356 RVA: 0x000EA0E1 File Offset: 0x000E82E1
			public virtual int? SCLRejectThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLRejectThreshold"] = value;
				}
			}

			// Token: 0x1700741D RID: 29725
			// (set) Token: 0x0600A18D RID: 41357 RVA: 0x000EA0F9 File Offset: 0x000E82F9
			public virtual bool? SCLRejectEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLRejectEnabled"] = value;
				}
			}

			// Token: 0x1700741E RID: 29726
			// (set) Token: 0x0600A18E RID: 41358 RVA: 0x000EA111 File Offset: 0x000E8311
			public virtual int? SCLQuarantineThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineThreshold"] = value;
				}
			}

			// Token: 0x1700741F RID: 29727
			// (set) Token: 0x0600A18F RID: 41359 RVA: 0x000EA129 File Offset: 0x000E8329
			public virtual bool? SCLQuarantineEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLQuarantineEnabled"] = value;
				}
			}

			// Token: 0x17007420 RID: 29728
			// (set) Token: 0x0600A190 RID: 41360 RVA: 0x000EA141 File Offset: 0x000E8341
			public virtual int? SCLJunkThreshold
			{
				set
				{
					base.PowerSharpParameters["SCLJunkThreshold"] = value;
				}
			}

			// Token: 0x17007421 RID: 29729
			// (set) Token: 0x0600A191 RID: 41361 RVA: 0x000EA159 File Offset: 0x000E8359
			public virtual bool? SCLJunkEnabled
			{
				set
				{
					base.PowerSharpParameters["SCLJunkEnabled"] = value;
				}
			}

			// Token: 0x17007422 RID: 29730
			// (set) Token: 0x0600A192 RID: 41362 RVA: 0x000EA171 File Offset: 0x000E8371
			public virtual bool AntispamBypassEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamBypassEnabled"] = value;
				}
			}

			// Token: 0x17007423 RID: 29731
			// (set) Token: 0x0600A193 RID: 41363 RVA: 0x000EA189 File Offset: 0x000E8389
			public virtual bool? UseDatabaseQuotaDefaults
			{
				set
				{
					base.PowerSharpParameters["UseDatabaseQuotaDefaults"] = value;
				}
			}

			// Token: 0x17007424 RID: 29732
			// (set) Token: 0x0600A194 RID: 41364 RVA: 0x000EA1A1 File Offset: 0x000E83A1
			public virtual Unlimited<ByteQuantifiedSize> IssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["IssueWarningQuota"] = value;
				}
			}

			// Token: 0x17007425 RID: 29733
			// (set) Token: 0x0600A195 RID: 41365 RVA: 0x000EA1B9 File Offset: 0x000E83B9
			public virtual ByteQuantifiedSize RulesQuota
			{
				set
				{
					base.PowerSharpParameters["RulesQuota"] = value;
				}
			}

			// Token: 0x17007426 RID: 29734
			// (set) Token: 0x0600A196 RID: 41366 RVA: 0x000EA1D1 File Offset: 0x000E83D1
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17007427 RID: 29735
			// (set) Token: 0x0600A197 RID: 41367 RVA: 0x000EA1E4 File Offset: 0x000E83E4
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007428 RID: 29736
			// (set) Token: 0x0600A198 RID: 41368 RVA: 0x000EA1F7 File Offset: 0x000E83F7
			public virtual int? MaxSafeSenders
			{
				set
				{
					base.PowerSharpParameters["MaxSafeSenders"] = value;
				}
			}

			// Token: 0x17007429 RID: 29737
			// (set) Token: 0x0600A199 RID: 41369 RVA: 0x000EA20F File Offset: 0x000E840F
			public virtual int? MaxBlockedSenders
			{
				set
				{
					base.PowerSharpParameters["MaxBlockedSenders"] = value;
				}
			}

			// Token: 0x1700742A RID: 29738
			// (set) Token: 0x0600A19A RID: 41370 RVA: 0x000EA227 File Offset: 0x000E8427
			public virtual SmtpAddress WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x1700742B RID: 29739
			// (set) Token: 0x0600A19B RID: 41371 RVA: 0x000EA23F File Offset: 0x000E843F
			public virtual SmtpAddress MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x1700742C RID: 29740
			// (set) Token: 0x0600A19C RID: 41372 RVA: 0x000EA257 File Offset: 0x000E8457
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x1700742D RID: 29741
			// (set) Token: 0x0600A19D RID: 41373 RVA: 0x000EA26A File Offset: 0x000E846A
			public virtual SmtpAddress JournalArchiveAddress
			{
				set
				{
					base.PowerSharpParameters["JournalArchiveAddress"] = value;
				}
			}

			// Token: 0x1700742E RID: 29742
			// (set) Token: 0x0600A19E RID: 41374 RVA: 0x000EA282 File Offset: 0x000E8482
			public virtual Unlimited<ByteQuantifiedSize> ArchiveQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveQuota"] = value;
				}
			}

			// Token: 0x1700742F RID: 29743
			// (set) Token: 0x0600A19F RID: 41375 RVA: 0x000EA29A File Offset: 0x000E849A
			public virtual Unlimited<ByteQuantifiedSize> ArchiveWarningQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveWarningQuota"] = value;
				}
			}

			// Token: 0x17007430 RID: 29744
			// (set) Token: 0x0600A1A0 RID: 41376 RVA: 0x000EA2B2 File Offset: 0x000E84B2
			public virtual SmtpDomain ArchiveDomain
			{
				set
				{
					base.PowerSharpParameters["ArchiveDomain"] = value;
				}
			}

			// Token: 0x17007431 RID: 29745
			// (set) Token: 0x0600A1A1 RID: 41377 RVA: 0x000EA2C5 File Offset: 0x000E84C5
			public virtual ArchiveStatusFlags ArchiveStatus
			{
				set
				{
					base.PowerSharpParameters["ArchiveStatus"] = value;
				}
			}

			// Token: 0x17007432 RID: 29746
			// (set) Token: 0x0600A1A2 RID: 41378 RVA: 0x000EA2DD File Offset: 0x000E84DD
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17007433 RID: 29747
			// (set) Token: 0x0600A1A3 RID: 41379 RVA: 0x000EA2F5 File Offset: 0x000E84F5
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007434 RID: 29748
			// (set) Token: 0x0600A1A4 RID: 41380 RVA: 0x000EA30D File Offset: 0x000E850D
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17007435 RID: 29749
			// (set) Token: 0x0600A1A5 RID: 41381 RVA: 0x000EA320 File Offset: 0x000E8520
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17007436 RID: 29750
			// (set) Token: 0x0600A1A6 RID: 41382 RVA: 0x000EA333 File Offset: 0x000E8533
			public virtual bool CalendarVersionStoreDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarVersionStoreDisabled"] = value;
				}
			}

			// Token: 0x17007437 RID: 29751
			// (set) Token: 0x0600A1A7 RID: 41383 RVA: 0x000EA34B File Offset: 0x000E854B
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007438 RID: 29752
			// (set) Token: 0x0600A1A8 RID: 41384 RVA: 0x000EA35E File Offset: 0x000E855E
			public virtual bool? SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17007439 RID: 29753
			// (set) Token: 0x0600A1A9 RID: 41385 RVA: 0x000EA376 File Offset: 0x000E8576
			public virtual bool AuditEnabled
			{
				set
				{
					base.PowerSharpParameters["AuditEnabled"] = value;
				}
			}

			// Token: 0x1700743A RID: 29754
			// (set) Token: 0x0600A1AA RID: 41386 RVA: 0x000EA38E File Offset: 0x000E858E
			public virtual EnhancedTimeSpan AuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["AuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x1700743B RID: 29755
			// (set) Token: 0x0600A1AB RID: 41387 RVA: 0x000EA3A6 File Offset: 0x000E85A6
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditAdmin
			{
				set
				{
					base.PowerSharpParameters["AuditAdmin"] = value;
				}
			}

			// Token: 0x1700743C RID: 29756
			// (set) Token: 0x0600A1AC RID: 41388 RVA: 0x000EA3B9 File Offset: 0x000E85B9
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditDelegate
			{
				set
				{
					base.PowerSharpParameters["AuditDelegate"] = value;
				}
			}

			// Token: 0x1700743D RID: 29757
			// (set) Token: 0x0600A1AD RID: 41389 RVA: 0x000EA3CC File Offset: 0x000E85CC
			public virtual MultiValuedProperty<MailboxAuditOperations> AuditOwner
			{
				set
				{
					base.PowerSharpParameters["AuditOwner"] = value;
				}
			}

			// Token: 0x1700743E RID: 29758
			// (set) Token: 0x0600A1AE RID: 41390 RVA: 0x000EA3DF File Offset: 0x000E85DF
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x1700743F RID: 29759
			// (set) Token: 0x0600A1AF RID: 41391 RVA: 0x000EA3F2 File Offset: 0x000E85F2
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007440 RID: 29760
			// (set) Token: 0x0600A1B0 RID: 41392 RVA: 0x000EA405 File Offset: 0x000E8605
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17007441 RID: 29761
			// (set) Token: 0x0600A1B1 RID: 41393 RVA: 0x000EA418 File Offset: 0x000E8618
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17007442 RID: 29762
			// (set) Token: 0x0600A1B2 RID: 41394 RVA: 0x000EA42B File Offset: 0x000E862B
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17007443 RID: 29763
			// (set) Token: 0x0600A1B3 RID: 41395 RVA: 0x000EA43E File Offset: 0x000E863E
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17007444 RID: 29764
			// (set) Token: 0x0600A1B4 RID: 41396 RVA: 0x000EA451 File Offset: 0x000E8651
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17007445 RID: 29765
			// (set) Token: 0x0600A1B5 RID: 41397 RVA: 0x000EA464 File Offset: 0x000E8664
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17007446 RID: 29766
			// (set) Token: 0x0600A1B6 RID: 41398 RVA: 0x000EA477 File Offset: 0x000E8677
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17007447 RID: 29767
			// (set) Token: 0x0600A1B7 RID: 41399 RVA: 0x000EA48A File Offset: 0x000E868A
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17007448 RID: 29768
			// (set) Token: 0x0600A1B8 RID: 41400 RVA: 0x000EA49D File Offset: 0x000E869D
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17007449 RID: 29769
			// (set) Token: 0x0600A1B9 RID: 41401 RVA: 0x000EA4B0 File Offset: 0x000E86B0
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x1700744A RID: 29770
			// (set) Token: 0x0600A1BA RID: 41402 RVA: 0x000EA4C3 File Offset: 0x000E86C3
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x1700744B RID: 29771
			// (set) Token: 0x0600A1BB RID: 41403 RVA: 0x000EA4D6 File Offset: 0x000E86D6
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x1700744C RID: 29772
			// (set) Token: 0x0600A1BC RID: 41404 RVA: 0x000EA4E9 File Offset: 0x000E86E9
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x1700744D RID: 29773
			// (set) Token: 0x0600A1BD RID: 41405 RVA: 0x000EA4FC File Offset: 0x000E86FC
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x1700744E RID: 29774
			// (set) Token: 0x0600A1BE RID: 41406 RVA: 0x000EA50F File Offset: 0x000E870F
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x1700744F RID: 29775
			// (set) Token: 0x0600A1BF RID: 41407 RVA: 0x000EA522 File Offset: 0x000E8722
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17007450 RID: 29776
			// (set) Token: 0x0600A1C0 RID: 41408 RVA: 0x000EA535 File Offset: 0x000E8735
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17007451 RID: 29777
			// (set) Token: 0x0600A1C1 RID: 41409 RVA: 0x000EA548 File Offset: 0x000E8748
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17007452 RID: 29778
			// (set) Token: 0x0600A1C2 RID: 41410 RVA: 0x000EA55B File Offset: 0x000E875B
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17007453 RID: 29779
			// (set) Token: 0x0600A1C3 RID: 41411 RVA: 0x000EA56E File Offset: 0x000E876E
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17007454 RID: 29780
			// (set) Token: 0x0600A1C4 RID: 41412 RVA: 0x000EA581 File Offset: 0x000E8781
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007455 RID: 29781
			// (set) Token: 0x0600A1C5 RID: 41413 RVA: 0x000EA594 File Offset: 0x000E8794
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17007456 RID: 29782
			// (set) Token: 0x0600A1C6 RID: 41414 RVA: 0x000EA5A7 File Offset: 0x000E87A7
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17007457 RID: 29783
			// (set) Token: 0x0600A1C7 RID: 41415 RVA: 0x000EA5BF File Offset: 0x000E87BF
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x17007458 RID: 29784
			// (set) Token: 0x0600A1C8 RID: 41416 RVA: 0x000EA5D7 File Offset: 0x000E87D7
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x17007459 RID: 29785
			// (set) Token: 0x0600A1C9 RID: 41417 RVA: 0x000EA5EF File Offset: 0x000E87EF
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700745A RID: 29786
			// (set) Token: 0x0600A1CA RID: 41418 RVA: 0x000EA607 File Offset: 0x000E8807
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x1700745B RID: 29787
			// (set) Token: 0x0600A1CB RID: 41419 RVA: 0x000EA61F File Offset: 0x000E881F
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700745C RID: 29788
			// (set) Token: 0x0600A1CC RID: 41420 RVA: 0x000EA637 File Offset: 0x000E8837
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x1700745D RID: 29789
			// (set) Token: 0x0600A1CD RID: 41421 RVA: 0x000EA64F File Offset: 0x000E884F
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x1700745E RID: 29790
			// (set) Token: 0x0600A1CE RID: 41422 RVA: 0x000EA662 File Offset: 0x000E8862
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700745F RID: 29791
			// (set) Token: 0x0600A1CF RID: 41423 RVA: 0x000EA67A File Offset: 0x000E887A
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x17007460 RID: 29792
			// (set) Token: 0x0600A1D0 RID: 41424 RVA: 0x000EA68D File Offset: 0x000E888D
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x17007461 RID: 29793
			// (set) Token: 0x0600A1D1 RID: 41425 RVA: 0x000EA6A5 File Offset: 0x000E88A5
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x17007462 RID: 29794
			// (set) Token: 0x0600A1D2 RID: 41426 RVA: 0x000EA6B8 File Offset: 0x000E88B8
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17007463 RID: 29795
			// (set) Token: 0x0600A1D3 RID: 41427 RVA: 0x000EA6CB File Offset: 0x000E88CB
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007464 RID: 29796
			// (set) Token: 0x0600A1D4 RID: 41428 RVA: 0x000EA6DE File Offset: 0x000E88DE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007465 RID: 29797
			// (set) Token: 0x0600A1D5 RID: 41429 RVA: 0x000EA6F6 File Offset: 0x000E88F6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007466 RID: 29798
			// (set) Token: 0x0600A1D6 RID: 41430 RVA: 0x000EA70E File Offset: 0x000E890E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007467 RID: 29799
			// (set) Token: 0x0600A1D7 RID: 41431 RVA: 0x000EA726 File Offset: 0x000E8926
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007468 RID: 29800
			// (set) Token: 0x0600A1D8 RID: 41432 RVA: 0x000EA73E File Offset: 0x000E893E
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
