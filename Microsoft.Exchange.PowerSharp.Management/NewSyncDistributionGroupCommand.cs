using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D68 RID: 3432
	public class NewSyncDistributionGroupCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x0600B65A RID: 46682 RVA: 0x0010663E File Offset: 0x0010483E
		private NewSyncDistributionGroupCommand() : base("New-SyncDistributionGroup")
		{
		}

		// Token: 0x0600B65B RID: 46683 RVA: 0x0010664B File Offset: 0x0010484B
		public NewSyncDistributionGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B65C RID: 46684 RVA: 0x0010665A File Offset: 0x0010485A
		public virtual NewSyncDistributionGroupCommand SetParameters(NewSyncDistributionGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D69 RID: 3433
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17008743 RID: 34627
			// (set) Token: 0x0600B65D RID: 46685 RVA: 0x00106664 File Offset: 0x00104864
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17008744 RID: 34628
			// (set) Token: 0x0600B65E RID: 46686 RVA: 0x00106677 File Offset: 0x00104877
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17008745 RID: 34629
			// (set) Token: 0x0600B65F RID: 46687 RVA: 0x0010668A File Offset: 0x0010488A
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17008746 RID: 34630
			// (set) Token: 0x0600B660 RID: 46688 RVA: 0x001066A2 File Offset: 0x001048A2
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17008747 RID: 34631
			// (set) Token: 0x0600B661 RID: 46689 RVA: 0x001066B5 File Offset: 0x001048B5
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17008748 RID: 34632
			// (set) Token: 0x0600B662 RID: 46690 RVA: 0x001066C8 File Offset: 0x001048C8
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17008749 RID: 34633
			// (set) Token: 0x0600B663 RID: 46691 RVA: 0x001066DB File Offset: 0x001048DB
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x1700874A RID: 34634
			// (set) Token: 0x0600B664 RID: 46692 RVA: 0x001066EE File Offset: 0x001048EE
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x1700874B RID: 34635
			// (set) Token: 0x0600B665 RID: 46693 RVA: 0x00106701 File Offset: 0x00104901
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x1700874C RID: 34636
			// (set) Token: 0x0600B666 RID: 46694 RVA: 0x00106714 File Offset: 0x00104914
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x1700874D RID: 34637
			// (set) Token: 0x0600B667 RID: 46695 RVA: 0x00106727 File Offset: 0x00104927
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x1700874E RID: 34638
			// (set) Token: 0x0600B668 RID: 46696 RVA: 0x0010673A File Offset: 0x0010493A
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x1700874F RID: 34639
			// (set) Token: 0x0600B669 RID: 46697 RVA: 0x0010674D File Offset: 0x0010494D
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17008750 RID: 34640
			// (set) Token: 0x0600B66A RID: 46698 RVA: 0x00106760 File Offset: 0x00104960
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17008751 RID: 34641
			// (set) Token: 0x0600B66B RID: 46699 RVA: 0x00106773 File Offset: 0x00104973
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17008752 RID: 34642
			// (set) Token: 0x0600B66C RID: 46700 RVA: 0x00106786 File Offset: 0x00104986
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17008753 RID: 34643
			// (set) Token: 0x0600B66D RID: 46701 RVA: 0x00106799 File Offset: 0x00104999
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17008754 RID: 34644
			// (set) Token: 0x0600B66E RID: 46702 RVA: 0x001067AC File Offset: 0x001049AC
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17008755 RID: 34645
			// (set) Token: 0x0600B66F RID: 46703 RVA: 0x001067BF File Offset: 0x001049BF
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17008756 RID: 34646
			// (set) Token: 0x0600B670 RID: 46704 RVA: 0x001067D2 File Offset: 0x001049D2
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17008757 RID: 34647
			// (set) Token: 0x0600B671 RID: 46705 RVA: 0x001067E5 File Offset: 0x001049E5
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17008758 RID: 34648
			// (set) Token: 0x0600B672 RID: 46706 RVA: 0x001067F8 File Offset: 0x001049F8
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17008759 RID: 34649
			// (set) Token: 0x0600B673 RID: 46707 RVA: 0x0010680B File Offset: 0x00104A0B
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x1700875A RID: 34650
			// (set) Token: 0x0600B674 RID: 46708 RVA: 0x0010681E File Offset: 0x00104A1E
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x1700875B RID: 34651
			// (set) Token: 0x0600B675 RID: 46709 RVA: 0x00106831 File Offset: 0x00104A31
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x1700875C RID: 34652
			// (set) Token: 0x0600B676 RID: 46710 RVA: 0x00106844 File Offset: 0x00104A44
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x1700875D RID: 34653
			// (set) Token: 0x0600B677 RID: 46711 RVA: 0x00106857 File Offset: 0x00104A57
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x1700875E RID: 34654
			// (set) Token: 0x0600B678 RID: 46712 RVA: 0x0010686A File Offset: 0x00104A6A
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x1700875F RID: 34655
			// (set) Token: 0x0600B679 RID: 46713 RVA: 0x0010687D File Offset: 0x00104A7D
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17008760 RID: 34656
			// (set) Token: 0x0600B67A RID: 46714 RVA: 0x00106895 File Offset: 0x00104A95
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17008761 RID: 34657
			// (set) Token: 0x0600B67B RID: 46715 RVA: 0x001068A8 File Offset: 0x00104AA8
			public virtual RecipientDisplayType? RecipientDisplayType
			{
				set
				{
					base.PowerSharpParameters["RecipientDisplayType"] = value;
				}
			}

			// Token: 0x17008762 RID: 34658
			// (set) Token: 0x0600B67C RID: 46716 RVA: 0x001068C0 File Offset: 0x00104AC0
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17008763 RID: 34659
			// (set) Token: 0x0600B67D RID: 46717 RVA: 0x001068D8 File Offset: 0x00104AD8
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17008764 RID: 34660
			// (set) Token: 0x0600B67E RID: 46718 RVA: 0x001068F0 File Offset: 0x00104AF0
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17008765 RID: 34661
			// (set) Token: 0x0600B67F RID: 46719 RVA: 0x00106903 File Offset: 0x00104B03
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17008766 RID: 34662
			// (set) Token: 0x0600B680 RID: 46720 RVA: 0x00106916 File Offset: 0x00104B16
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17008767 RID: 34663
			// (set) Token: 0x0600B681 RID: 46721 RVA: 0x0010692E File Offset: 0x00104B2E
			public virtual bool ReportToManagerEnabled
			{
				set
				{
					base.PowerSharpParameters["ReportToManagerEnabled"] = value;
				}
			}

			// Token: 0x17008768 RID: 34664
			// (set) Token: 0x0600B682 RID: 46722 RVA: 0x00106946 File Offset: 0x00104B46
			public virtual bool ReportToOriginatorEnabled
			{
				set
				{
					base.PowerSharpParameters["ReportToOriginatorEnabled"] = value;
				}
			}

			// Token: 0x17008769 RID: 34665
			// (set) Token: 0x0600B683 RID: 46723 RVA: 0x0010695E File Offset: 0x00104B5E
			public virtual bool SendOofMessageToOriginatorEnabled
			{
				set
				{
					base.PowerSharpParameters["SendOofMessageToOriginatorEnabled"] = value;
				}
			}

			// Token: 0x1700876A RID: 34666
			// (set) Token: 0x0600B684 RID: 46724 RVA: 0x00106976 File Offset: 0x00104B76
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x1700876B RID: 34667
			// (set) Token: 0x0600B685 RID: 46725 RVA: 0x0010698E File Offset: 0x00104B8E
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x1700876C RID: 34668
			// (set) Token: 0x0600B686 RID: 46726 RVA: 0x001069A1 File Offset: 0x00104BA1
			public virtual bool IsHierarchicalGroup
			{
				set
				{
					base.PowerSharpParameters["IsHierarchicalGroup"] = value;
				}
			}

			// Token: 0x1700876D RID: 34669
			// (set) Token: 0x0600B687 RID: 46727 RVA: 0x001069B9 File Offset: 0x00104BB9
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x1700876E RID: 34670
			// (set) Token: 0x0600B688 RID: 46728 RVA: 0x001069CC File Offset: 0x00104BCC
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x1700876F RID: 34671
			// (set) Token: 0x0600B689 RID: 46729 RVA: 0x001069E4 File Offset: 0x00104BE4
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17008770 RID: 34672
			// (set) Token: 0x0600B68A RID: 46730 RVA: 0x001069F7 File Offset: 0x00104BF7
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17008771 RID: 34673
			// (set) Token: 0x0600B68B RID: 46731 RVA: 0x00106A0A File Offset: 0x00104C0A
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17008772 RID: 34674
			// (set) Token: 0x0600B68C RID: 46732 RVA: 0x00106A1D File Offset: 0x00104C1D
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17008773 RID: 34675
			// (set) Token: 0x0600B68D RID: 46733 RVA: 0x00106A35 File Offset: 0x00104C35
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17008774 RID: 34676
			// (set) Token: 0x0600B68E RID: 46734 RVA: 0x00106A48 File Offset: 0x00104C48
			public virtual GroupType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x17008775 RID: 34677
			// (set) Token: 0x0600B68F RID: 46735 RVA: 0x00106A60 File Offset: 0x00104C60
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008776 RID: 34678
			// (set) Token: 0x0600B690 RID: 46736 RVA: 0x00106A73 File Offset: 0x00104C73
			public virtual MultiValuedProperty<GeneralRecipientIdParameter> ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = value;
				}
			}

			// Token: 0x17008777 RID: 34679
			// (set) Token: 0x0600B691 RID: 46737 RVA: 0x00106A86 File Offset: 0x00104C86
			public virtual MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>> Members
			{
				set
				{
					base.PowerSharpParameters["Members"] = value;
				}
			}

			// Token: 0x17008778 RID: 34680
			// (set) Token: 0x0600B692 RID: 46738 RVA: 0x00106A99 File Offset: 0x00104C99
			public virtual MemberUpdateType MemberJoinRestriction
			{
				set
				{
					base.PowerSharpParameters["MemberJoinRestriction"] = value;
				}
			}

			// Token: 0x17008779 RID: 34681
			// (set) Token: 0x0600B693 RID: 46739 RVA: 0x00106AB1 File Offset: 0x00104CB1
			public virtual MemberUpdateType MemberDepartRestriction
			{
				set
				{
					base.PowerSharpParameters["MemberDepartRestriction"] = value;
				}
			}

			// Token: 0x1700877A RID: 34682
			// (set) Token: 0x0600B694 RID: 46740 RVA: 0x00106AC9 File Offset: 0x00104CC9
			public virtual bool BypassNestedModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["BypassNestedModerationEnabled"] = value;
				}
			}

			// Token: 0x1700877B RID: 34683
			// (set) Token: 0x0600B695 RID: 46741 RVA: 0x00106AE1 File Offset: 0x00104CE1
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x1700877C RID: 34684
			// (set) Token: 0x0600B696 RID: 46742 RVA: 0x00106AF4 File Offset: 0x00104CF4
			public virtual SwitchParameter CopyOwnerToMember
			{
				set
				{
					base.PowerSharpParameters["CopyOwnerToMember"] = value;
				}
			}

			// Token: 0x1700877D RID: 34685
			// (set) Token: 0x0600B697 RID: 46743 RVA: 0x00106B0C File Offset: 0x00104D0C
			public virtual SwitchParameter RoomList
			{
				set
				{
					base.PowerSharpParameters["RoomList"] = value;
				}
			}

			// Token: 0x1700877E RID: 34686
			// (set) Token: 0x0600B698 RID: 46744 RVA: 0x00106B24 File Offset: 0x00104D24
			public virtual SwitchParameter IgnoreNamingPolicy
			{
				set
				{
					base.PowerSharpParameters["IgnoreNamingPolicy"] = value;
				}
			}

			// Token: 0x1700877F RID: 34687
			// (set) Token: 0x0600B699 RID: 46745 RVA: 0x00106B3C File Offset: 0x00104D3C
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008780 RID: 34688
			// (set) Token: 0x0600B69A RID: 46746 RVA: 0x00106B5A File Offset: 0x00104D5A
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17008781 RID: 34689
			// (set) Token: 0x0600B69B RID: 46747 RVA: 0x00106B6D File Offset: 0x00104D6D
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17008782 RID: 34690
			// (set) Token: 0x0600B69C RID: 46748 RVA: 0x00106B85 File Offset: 0x00104D85
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17008783 RID: 34691
			// (set) Token: 0x0600B69D RID: 46749 RVA: 0x00106B9D File Offset: 0x00104D9D
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008784 RID: 34692
			// (set) Token: 0x0600B69E RID: 46750 RVA: 0x00106BB5 File Offset: 0x00104DB5
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008785 RID: 34693
			// (set) Token: 0x0600B69F RID: 46751 RVA: 0x00106BC8 File Offset: 0x00104DC8
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008786 RID: 34694
			// (set) Token: 0x0600B6A0 RID: 46752 RVA: 0x00106BE0 File Offset: 0x00104DE0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17008787 RID: 34695
			// (set) Token: 0x0600B6A1 RID: 46753 RVA: 0x00106BF3 File Offset: 0x00104DF3
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008788 RID: 34696
			// (set) Token: 0x0600B6A2 RID: 46754 RVA: 0x00106C06 File Offset: 0x00104E06
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008789 RID: 34697
			// (set) Token: 0x0600B6A3 RID: 46755 RVA: 0x00106C24 File Offset: 0x00104E24
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700878A RID: 34698
			// (set) Token: 0x0600B6A4 RID: 46756 RVA: 0x00106C37 File Offset: 0x00104E37
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700878B RID: 34699
			// (set) Token: 0x0600B6A5 RID: 46757 RVA: 0x00106C55 File Offset: 0x00104E55
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700878C RID: 34700
			// (set) Token: 0x0600B6A6 RID: 46758 RVA: 0x00106C68 File Offset: 0x00104E68
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700878D RID: 34701
			// (set) Token: 0x0600B6A7 RID: 46759 RVA: 0x00106C80 File Offset: 0x00104E80
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700878E RID: 34702
			// (set) Token: 0x0600B6A8 RID: 46760 RVA: 0x00106C98 File Offset: 0x00104E98
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700878F RID: 34703
			// (set) Token: 0x0600B6A9 RID: 46761 RVA: 0x00106CB0 File Offset: 0x00104EB0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008790 RID: 34704
			// (set) Token: 0x0600B6AA RID: 46762 RVA: 0x00106CC8 File Offset: 0x00104EC8
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
