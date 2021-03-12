using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C15 RID: 3093
	public class SetDistributionGroupCommand : SyntheticCommandWithPipelineInputNoOutput<DistributionGroup>
	{
		// Token: 0x0600962E RID: 38446 RVA: 0x000DAAEC File Offset: 0x000D8CEC
		private SetDistributionGroupCommand() : base("Set-DistributionGroup")
		{
		}

		// Token: 0x0600962F RID: 38447 RVA: 0x000DAAF9 File Offset: 0x000D8CF9
		public SetDistributionGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009630 RID: 38448 RVA: 0x000DAB08 File Offset: 0x000D8D08
		public virtual SetDistributionGroupCommand SetParameters(SetDistributionGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009631 RID: 38449 RVA: 0x000DAB12 File Offset: 0x000D8D12
		public virtual SetDistributionGroupCommand SetParameters(SetDistributionGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C16 RID: 3094
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170069BD RID: 27069
			// (set) Token: 0x06009632 RID: 38450 RVA: 0x000DAB1C File Offset: 0x000D8D1C
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x170069BE RID: 27070
			// (set) Token: 0x06009633 RID: 38451 RVA: 0x000DAB34 File Offset: 0x000D8D34
			public virtual SwitchParameter GenerateExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["GenerateExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x170069BF RID: 27071
			// (set) Token: 0x06009634 RID: 38452 RVA: 0x000DAB4C File Offset: 0x000D8D4C
			public virtual string ExpansionServer
			{
				set
				{
					base.PowerSharpParameters["ExpansionServer"] = value;
				}
			}

			// Token: 0x170069C0 RID: 27072
			// (set) Token: 0x06009635 RID: 38453 RVA: 0x000DAB5F File Offset: 0x000D8D5F
			public virtual MultiValuedProperty<GeneralRecipientIdParameter> ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = value;
				}
			}

			// Token: 0x170069C1 RID: 27073
			// (set) Token: 0x06009636 RID: 38454 RVA: 0x000DAB72 File Offset: 0x000D8D72
			public virtual MemberUpdateType MemberJoinRestriction
			{
				set
				{
					base.PowerSharpParameters["MemberJoinRestriction"] = value;
				}
			}

			// Token: 0x170069C2 RID: 27074
			// (set) Token: 0x06009637 RID: 38455 RVA: 0x000DAB8A File Offset: 0x000D8D8A
			public virtual MemberUpdateType MemberDepartRestriction
			{
				set
				{
					base.PowerSharpParameters["MemberDepartRestriction"] = value;
				}
			}

			// Token: 0x170069C3 RID: 27075
			// (set) Token: 0x06009638 RID: 38456 RVA: 0x000DABA2 File Offset: 0x000D8DA2
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x170069C4 RID: 27076
			// (set) Token: 0x06009639 RID: 38457 RVA: 0x000DABBA File Offset: 0x000D8DBA
			public virtual SwitchParameter RoomList
			{
				set
				{
					base.PowerSharpParameters["RoomList"] = value;
				}
			}

			// Token: 0x170069C5 RID: 27077
			// (set) Token: 0x0600963A RID: 38458 RVA: 0x000DABD2 File Offset: 0x000D8DD2
			public virtual SwitchParameter IgnoreNamingPolicy
			{
				set
				{
					base.PowerSharpParameters["IgnoreNamingPolicy"] = value;
				}
			}

			// Token: 0x170069C6 RID: 27078
			// (set) Token: 0x0600963B RID: 38459 RVA: 0x000DABEA File Offset: 0x000D8DEA
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x170069C7 RID: 27079
			// (set) Token: 0x0600963C RID: 38460 RVA: 0x000DABFD File Offset: 0x000D8DFD
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x170069C8 RID: 27080
			// (set) Token: 0x0600963D RID: 38461 RVA: 0x000DAC10 File Offset: 0x000D8E10
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x170069C9 RID: 27081
			// (set) Token: 0x0600963E RID: 38462 RVA: 0x000DAC23 File Offset: 0x000D8E23
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170069CA RID: 27082
			// (set) Token: 0x0600963F RID: 38463 RVA: 0x000DAC41 File Offset: 0x000D8E41
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x170069CB RID: 27083
			// (set) Token: 0x06009640 RID: 38464 RVA: 0x000DAC54 File Offset: 0x000D8E54
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170069CC RID: 27084
			// (set) Token: 0x06009641 RID: 38465 RVA: 0x000DAC67 File Offset: 0x000D8E67
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x170069CD RID: 27085
			// (set) Token: 0x06009642 RID: 38466 RVA: 0x000DAC7A File Offset: 0x000D8E7A
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x170069CE RID: 27086
			// (set) Token: 0x06009643 RID: 38467 RVA: 0x000DAC8D File Offset: 0x000D8E8D
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x170069CF RID: 27087
			// (set) Token: 0x06009644 RID: 38468 RVA: 0x000DACA0 File Offset: 0x000D8EA0
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x170069D0 RID: 27088
			// (set) Token: 0x06009645 RID: 38469 RVA: 0x000DACB3 File Offset: 0x000D8EB3
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x170069D1 RID: 27089
			// (set) Token: 0x06009646 RID: 38470 RVA: 0x000DACCB File Offset: 0x000D8ECB
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170069D2 RID: 27090
			// (set) Token: 0x06009647 RID: 38471 RVA: 0x000DACE3 File Offset: 0x000D8EE3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170069D3 RID: 27091
			// (set) Token: 0x06009648 RID: 38472 RVA: 0x000DACF6 File Offset: 0x000D8EF6
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x170069D4 RID: 27092
			// (set) Token: 0x06009649 RID: 38473 RVA: 0x000DAD09 File Offset: 0x000D8F09
			public virtual bool BypassNestedModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["BypassNestedModerationEnabled"] = value;
				}
			}

			// Token: 0x170069D5 RID: 27093
			// (set) Token: 0x0600964A RID: 38474 RVA: 0x000DAD21 File Offset: 0x000D8F21
			public virtual bool ReportToManagerEnabled
			{
				set
				{
					base.PowerSharpParameters["ReportToManagerEnabled"] = value;
				}
			}

			// Token: 0x170069D6 RID: 27094
			// (set) Token: 0x0600964B RID: 38475 RVA: 0x000DAD39 File Offset: 0x000D8F39
			public virtual bool ReportToOriginatorEnabled
			{
				set
				{
					base.PowerSharpParameters["ReportToOriginatorEnabled"] = value;
				}
			}

			// Token: 0x170069D7 RID: 27095
			// (set) Token: 0x0600964C RID: 38476 RVA: 0x000DAD51 File Offset: 0x000D8F51
			public virtual bool SendOofMessageToOriginatorEnabled
			{
				set
				{
					base.PowerSharpParameters["SendOofMessageToOriginatorEnabled"] = value;
				}
			}

			// Token: 0x170069D8 RID: 27096
			// (set) Token: 0x0600964D RID: 38477 RVA: 0x000DAD69 File Offset: 0x000D8F69
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170069D9 RID: 27097
			// (set) Token: 0x0600964E RID: 38478 RVA: 0x000DAD7C File Offset: 0x000D8F7C
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x170069DA RID: 27098
			// (set) Token: 0x0600964F RID: 38479 RVA: 0x000DAD8F File Offset: 0x000D8F8F
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x170069DB RID: 27099
			// (set) Token: 0x06009650 RID: 38480 RVA: 0x000DADA2 File Offset: 0x000D8FA2
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x170069DC RID: 27100
			// (set) Token: 0x06009651 RID: 38481 RVA: 0x000DADB5 File Offset: 0x000D8FB5
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x170069DD RID: 27101
			// (set) Token: 0x06009652 RID: 38482 RVA: 0x000DADC8 File Offset: 0x000D8FC8
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x170069DE RID: 27102
			// (set) Token: 0x06009653 RID: 38483 RVA: 0x000DADDB File Offset: 0x000D8FDB
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x170069DF RID: 27103
			// (set) Token: 0x06009654 RID: 38484 RVA: 0x000DADEE File Offset: 0x000D8FEE
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x170069E0 RID: 27104
			// (set) Token: 0x06009655 RID: 38485 RVA: 0x000DAE01 File Offset: 0x000D9001
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x170069E1 RID: 27105
			// (set) Token: 0x06009656 RID: 38486 RVA: 0x000DAE14 File Offset: 0x000D9014
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x170069E2 RID: 27106
			// (set) Token: 0x06009657 RID: 38487 RVA: 0x000DAE27 File Offset: 0x000D9027
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x170069E3 RID: 27107
			// (set) Token: 0x06009658 RID: 38488 RVA: 0x000DAE3A File Offset: 0x000D903A
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x170069E4 RID: 27108
			// (set) Token: 0x06009659 RID: 38489 RVA: 0x000DAE4D File Offset: 0x000D904D
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x170069E5 RID: 27109
			// (set) Token: 0x0600965A RID: 38490 RVA: 0x000DAE60 File Offset: 0x000D9060
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x170069E6 RID: 27110
			// (set) Token: 0x0600965B RID: 38491 RVA: 0x000DAE73 File Offset: 0x000D9073
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x170069E7 RID: 27111
			// (set) Token: 0x0600965C RID: 38492 RVA: 0x000DAE86 File Offset: 0x000D9086
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x170069E8 RID: 27112
			// (set) Token: 0x0600965D RID: 38493 RVA: 0x000DAE99 File Offset: 0x000D9099
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x170069E9 RID: 27113
			// (set) Token: 0x0600965E RID: 38494 RVA: 0x000DAEAC File Offset: 0x000D90AC
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x170069EA RID: 27114
			// (set) Token: 0x0600965F RID: 38495 RVA: 0x000DAEBF File Offset: 0x000D90BF
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x170069EB RID: 27115
			// (set) Token: 0x06009660 RID: 38496 RVA: 0x000DAED2 File Offset: 0x000D90D2
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x170069EC RID: 27116
			// (set) Token: 0x06009661 RID: 38497 RVA: 0x000DAEE5 File Offset: 0x000D90E5
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x170069ED RID: 27117
			// (set) Token: 0x06009662 RID: 38498 RVA: 0x000DAEF8 File Offset: 0x000D90F8
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170069EE RID: 27118
			// (set) Token: 0x06009663 RID: 38499 RVA: 0x000DAF0B File Offset: 0x000D910B
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x170069EF RID: 27119
			// (set) Token: 0x06009664 RID: 38500 RVA: 0x000DAF1E File Offset: 0x000D911E
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x170069F0 RID: 27120
			// (set) Token: 0x06009665 RID: 38501 RVA: 0x000DAF36 File Offset: 0x000D9136
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x170069F1 RID: 27121
			// (set) Token: 0x06009666 RID: 38502 RVA: 0x000DAF4E File Offset: 0x000D914E
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x170069F2 RID: 27122
			// (set) Token: 0x06009667 RID: 38503 RVA: 0x000DAF66 File Offset: 0x000D9166
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x170069F3 RID: 27123
			// (set) Token: 0x06009668 RID: 38504 RVA: 0x000DAF7E File Offset: 0x000D917E
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x170069F4 RID: 27124
			// (set) Token: 0x06009669 RID: 38505 RVA: 0x000DAF96 File Offset: 0x000D9196
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170069F5 RID: 27125
			// (set) Token: 0x0600966A RID: 38506 RVA: 0x000DAFAE File Offset: 0x000D91AE
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x170069F6 RID: 27126
			// (set) Token: 0x0600966B RID: 38507 RVA: 0x000DAFC6 File Offset: 0x000D91C6
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x170069F7 RID: 27127
			// (set) Token: 0x0600966C RID: 38508 RVA: 0x000DAFD9 File Offset: 0x000D91D9
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170069F8 RID: 27128
			// (set) Token: 0x0600966D RID: 38509 RVA: 0x000DAFF1 File Offset: 0x000D91F1
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x170069F9 RID: 27129
			// (set) Token: 0x0600966E RID: 38510 RVA: 0x000DB004 File Offset: 0x000D9204
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x170069FA RID: 27130
			// (set) Token: 0x0600966F RID: 38511 RVA: 0x000DB01C File Offset: 0x000D921C
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x170069FB RID: 27131
			// (set) Token: 0x06009670 RID: 38512 RVA: 0x000DB02F File Offset: 0x000D922F
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x170069FC RID: 27132
			// (set) Token: 0x06009671 RID: 38513 RVA: 0x000DB042 File Offset: 0x000D9242
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170069FD RID: 27133
			// (set) Token: 0x06009672 RID: 38514 RVA: 0x000DB055 File Offset: 0x000D9255
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170069FE RID: 27134
			// (set) Token: 0x06009673 RID: 38515 RVA: 0x000DB06D File Offset: 0x000D926D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170069FF RID: 27135
			// (set) Token: 0x06009674 RID: 38516 RVA: 0x000DB085 File Offset: 0x000D9285
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006A00 RID: 27136
			// (set) Token: 0x06009675 RID: 38517 RVA: 0x000DB09D File Offset: 0x000D929D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006A01 RID: 27137
			// (set) Token: 0x06009676 RID: 38518 RVA: 0x000DB0B5 File Offset: 0x000D92B5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C17 RID: 3095
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006A02 RID: 27138
			// (set) Token: 0x06009678 RID: 38520 RVA: 0x000DB0D5 File Offset: 0x000D92D5
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DistributionGroupIdParameter(value) : null);
				}
			}

			// Token: 0x17006A03 RID: 27139
			// (set) Token: 0x06009679 RID: 38521 RVA: 0x000DB0F3 File Offset: 0x000D92F3
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x17006A04 RID: 27140
			// (set) Token: 0x0600967A RID: 38522 RVA: 0x000DB10B File Offset: 0x000D930B
			public virtual SwitchParameter GenerateExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["GenerateExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17006A05 RID: 27141
			// (set) Token: 0x0600967B RID: 38523 RVA: 0x000DB123 File Offset: 0x000D9323
			public virtual string ExpansionServer
			{
				set
				{
					base.PowerSharpParameters["ExpansionServer"] = value;
				}
			}

			// Token: 0x17006A06 RID: 27142
			// (set) Token: 0x0600967C RID: 38524 RVA: 0x000DB136 File Offset: 0x000D9336
			public virtual MultiValuedProperty<GeneralRecipientIdParameter> ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = value;
				}
			}

			// Token: 0x17006A07 RID: 27143
			// (set) Token: 0x0600967D RID: 38525 RVA: 0x000DB149 File Offset: 0x000D9349
			public virtual MemberUpdateType MemberJoinRestriction
			{
				set
				{
					base.PowerSharpParameters["MemberJoinRestriction"] = value;
				}
			}

			// Token: 0x17006A08 RID: 27144
			// (set) Token: 0x0600967E RID: 38526 RVA: 0x000DB161 File Offset: 0x000D9361
			public virtual MemberUpdateType MemberDepartRestriction
			{
				set
				{
					base.PowerSharpParameters["MemberDepartRestriction"] = value;
				}
			}

			// Token: 0x17006A09 RID: 27145
			// (set) Token: 0x0600967F RID: 38527 RVA: 0x000DB179 File Offset: 0x000D9379
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x17006A0A RID: 27146
			// (set) Token: 0x06009680 RID: 38528 RVA: 0x000DB191 File Offset: 0x000D9391
			public virtual SwitchParameter RoomList
			{
				set
				{
					base.PowerSharpParameters["RoomList"] = value;
				}
			}

			// Token: 0x17006A0B RID: 27147
			// (set) Token: 0x06009681 RID: 38529 RVA: 0x000DB1A9 File Offset: 0x000D93A9
			public virtual SwitchParameter IgnoreNamingPolicy
			{
				set
				{
					base.PowerSharpParameters["IgnoreNamingPolicy"] = value;
				}
			}

			// Token: 0x17006A0C RID: 27148
			// (set) Token: 0x06009682 RID: 38530 RVA: 0x000DB1C1 File Offset: 0x000D93C1
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17006A0D RID: 27149
			// (set) Token: 0x06009683 RID: 38531 RVA: 0x000DB1D4 File Offset: 0x000D93D4
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17006A0E RID: 27150
			// (set) Token: 0x06009684 RID: 38532 RVA: 0x000DB1E7 File Offset: 0x000D93E7
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17006A0F RID: 27151
			// (set) Token: 0x06009685 RID: 38533 RVA: 0x000DB1FA File Offset: 0x000D93FA
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006A10 RID: 27152
			// (set) Token: 0x06009686 RID: 38534 RVA: 0x000DB218 File Offset: 0x000D9418
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17006A11 RID: 27153
			// (set) Token: 0x06009687 RID: 38535 RVA: 0x000DB22B File Offset: 0x000D942B
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17006A12 RID: 27154
			// (set) Token: 0x06009688 RID: 38536 RVA: 0x000DB23E File Offset: 0x000D943E
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17006A13 RID: 27155
			// (set) Token: 0x06009689 RID: 38537 RVA: 0x000DB251 File Offset: 0x000D9451
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17006A14 RID: 27156
			// (set) Token: 0x0600968A RID: 38538 RVA: 0x000DB264 File Offset: 0x000D9464
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17006A15 RID: 27157
			// (set) Token: 0x0600968B RID: 38539 RVA: 0x000DB277 File Offset: 0x000D9477
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17006A16 RID: 27158
			// (set) Token: 0x0600968C RID: 38540 RVA: 0x000DB28A File Offset: 0x000D948A
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x17006A17 RID: 27159
			// (set) Token: 0x0600968D RID: 38541 RVA: 0x000DB2A2 File Offset: 0x000D94A2
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006A18 RID: 27160
			// (set) Token: 0x0600968E RID: 38542 RVA: 0x000DB2BA File Offset: 0x000D94BA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006A19 RID: 27161
			// (set) Token: 0x0600968F RID: 38543 RVA: 0x000DB2CD File Offset: 0x000D94CD
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17006A1A RID: 27162
			// (set) Token: 0x06009690 RID: 38544 RVA: 0x000DB2E0 File Offset: 0x000D94E0
			public virtual bool BypassNestedModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["BypassNestedModerationEnabled"] = value;
				}
			}

			// Token: 0x17006A1B RID: 27163
			// (set) Token: 0x06009691 RID: 38545 RVA: 0x000DB2F8 File Offset: 0x000D94F8
			public virtual bool ReportToManagerEnabled
			{
				set
				{
					base.PowerSharpParameters["ReportToManagerEnabled"] = value;
				}
			}

			// Token: 0x17006A1C RID: 27164
			// (set) Token: 0x06009692 RID: 38546 RVA: 0x000DB310 File Offset: 0x000D9510
			public virtual bool ReportToOriginatorEnabled
			{
				set
				{
					base.PowerSharpParameters["ReportToOriginatorEnabled"] = value;
				}
			}

			// Token: 0x17006A1D RID: 27165
			// (set) Token: 0x06009693 RID: 38547 RVA: 0x000DB328 File Offset: 0x000D9528
			public virtual bool SendOofMessageToOriginatorEnabled
			{
				set
				{
					base.PowerSharpParameters["SendOofMessageToOriginatorEnabled"] = value;
				}
			}

			// Token: 0x17006A1E RID: 27166
			// (set) Token: 0x06009694 RID: 38548 RVA: 0x000DB340 File Offset: 0x000D9540
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006A1F RID: 27167
			// (set) Token: 0x06009695 RID: 38549 RVA: 0x000DB353 File Offset: 0x000D9553
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17006A20 RID: 27168
			// (set) Token: 0x06009696 RID: 38550 RVA: 0x000DB366 File Offset: 0x000D9566
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17006A21 RID: 27169
			// (set) Token: 0x06009697 RID: 38551 RVA: 0x000DB379 File Offset: 0x000D9579
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17006A22 RID: 27170
			// (set) Token: 0x06009698 RID: 38552 RVA: 0x000DB38C File Offset: 0x000D958C
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17006A23 RID: 27171
			// (set) Token: 0x06009699 RID: 38553 RVA: 0x000DB39F File Offset: 0x000D959F
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17006A24 RID: 27172
			// (set) Token: 0x0600969A RID: 38554 RVA: 0x000DB3B2 File Offset: 0x000D95B2
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17006A25 RID: 27173
			// (set) Token: 0x0600969B RID: 38555 RVA: 0x000DB3C5 File Offset: 0x000D95C5
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17006A26 RID: 27174
			// (set) Token: 0x0600969C RID: 38556 RVA: 0x000DB3D8 File Offset: 0x000D95D8
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17006A27 RID: 27175
			// (set) Token: 0x0600969D RID: 38557 RVA: 0x000DB3EB File Offset: 0x000D95EB
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17006A28 RID: 27176
			// (set) Token: 0x0600969E RID: 38558 RVA: 0x000DB3FE File Offset: 0x000D95FE
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17006A29 RID: 27177
			// (set) Token: 0x0600969F RID: 38559 RVA: 0x000DB411 File Offset: 0x000D9611
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17006A2A RID: 27178
			// (set) Token: 0x060096A0 RID: 38560 RVA: 0x000DB424 File Offset: 0x000D9624
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17006A2B RID: 27179
			// (set) Token: 0x060096A1 RID: 38561 RVA: 0x000DB437 File Offset: 0x000D9637
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17006A2C RID: 27180
			// (set) Token: 0x060096A2 RID: 38562 RVA: 0x000DB44A File Offset: 0x000D964A
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17006A2D RID: 27181
			// (set) Token: 0x060096A3 RID: 38563 RVA: 0x000DB45D File Offset: 0x000D965D
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17006A2E RID: 27182
			// (set) Token: 0x060096A4 RID: 38564 RVA: 0x000DB470 File Offset: 0x000D9670
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17006A2F RID: 27183
			// (set) Token: 0x060096A5 RID: 38565 RVA: 0x000DB483 File Offset: 0x000D9683
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17006A30 RID: 27184
			// (set) Token: 0x060096A6 RID: 38566 RVA: 0x000DB496 File Offset: 0x000D9696
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17006A31 RID: 27185
			// (set) Token: 0x060096A7 RID: 38567 RVA: 0x000DB4A9 File Offset: 0x000D96A9
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17006A32 RID: 27186
			// (set) Token: 0x060096A8 RID: 38568 RVA: 0x000DB4BC File Offset: 0x000D96BC
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17006A33 RID: 27187
			// (set) Token: 0x060096A9 RID: 38569 RVA: 0x000DB4CF File Offset: 0x000D96CF
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006A34 RID: 27188
			// (set) Token: 0x060096AA RID: 38570 RVA: 0x000DB4E2 File Offset: 0x000D96E2
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17006A35 RID: 27189
			// (set) Token: 0x060096AB RID: 38571 RVA: 0x000DB4F5 File Offset: 0x000D96F5
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17006A36 RID: 27190
			// (set) Token: 0x060096AC RID: 38572 RVA: 0x000DB50D File Offset: 0x000D970D
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x17006A37 RID: 27191
			// (set) Token: 0x060096AD RID: 38573 RVA: 0x000DB525 File Offset: 0x000D9725
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x17006A38 RID: 27192
			// (set) Token: 0x060096AE RID: 38574 RVA: 0x000DB53D File Offset: 0x000D973D
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17006A39 RID: 27193
			// (set) Token: 0x060096AF RID: 38575 RVA: 0x000DB555 File Offset: 0x000D9755
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x17006A3A RID: 27194
			// (set) Token: 0x060096B0 RID: 38576 RVA: 0x000DB56D File Offset: 0x000D976D
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006A3B RID: 27195
			// (set) Token: 0x060096B1 RID: 38577 RVA: 0x000DB585 File Offset: 0x000D9785
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17006A3C RID: 27196
			// (set) Token: 0x060096B2 RID: 38578 RVA: 0x000DB59D File Offset: 0x000D979D
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x17006A3D RID: 27197
			// (set) Token: 0x060096B3 RID: 38579 RVA: 0x000DB5B0 File Offset: 0x000D97B0
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17006A3E RID: 27198
			// (set) Token: 0x060096B4 RID: 38580 RVA: 0x000DB5C8 File Offset: 0x000D97C8
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x17006A3F RID: 27199
			// (set) Token: 0x060096B5 RID: 38581 RVA: 0x000DB5DB File Offset: 0x000D97DB
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x17006A40 RID: 27200
			// (set) Token: 0x060096B6 RID: 38582 RVA: 0x000DB5F3 File Offset: 0x000D97F3
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x17006A41 RID: 27201
			// (set) Token: 0x060096B7 RID: 38583 RVA: 0x000DB606 File Offset: 0x000D9806
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17006A42 RID: 27202
			// (set) Token: 0x060096B8 RID: 38584 RVA: 0x000DB619 File Offset: 0x000D9819
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006A43 RID: 27203
			// (set) Token: 0x060096B9 RID: 38585 RVA: 0x000DB62C File Offset: 0x000D982C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006A44 RID: 27204
			// (set) Token: 0x060096BA RID: 38586 RVA: 0x000DB644 File Offset: 0x000D9844
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006A45 RID: 27205
			// (set) Token: 0x060096BB RID: 38587 RVA: 0x000DB65C File Offset: 0x000D985C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006A46 RID: 27206
			// (set) Token: 0x060096BC RID: 38588 RVA: 0x000DB674 File Offset: 0x000D9874
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006A47 RID: 27207
			// (set) Token: 0x060096BD RID: 38589 RVA: 0x000DB68C File Offset: 0x000D988C
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
