using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C18 RID: 3096
	public class SetDynamicDistributionGroupCommand : SyntheticCommandWithPipelineInputNoOutput<DynamicDistributionGroup>
	{
		// Token: 0x060096BF RID: 38591 RVA: 0x000DB6AC File Offset: 0x000D98AC
		private SetDynamicDistributionGroupCommand() : base("Set-DynamicDistributionGroup")
		{
		}

		// Token: 0x060096C0 RID: 38592 RVA: 0x000DB6B9 File Offset: 0x000D98B9
		public SetDynamicDistributionGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060096C1 RID: 38593 RVA: 0x000DB6C8 File Offset: 0x000D98C8
		public virtual SetDynamicDistributionGroupCommand SetParameters(SetDynamicDistributionGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060096C2 RID: 38594 RVA: 0x000DB6D2 File Offset: 0x000D98D2
		public virtual SetDynamicDistributionGroupCommand SetParameters(SetDynamicDistributionGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C19 RID: 3097
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006A48 RID: 27208
			// (set) Token: 0x060096C3 RID: 38595 RVA: 0x000DB6DC File Offset: 0x000D98DC
			public virtual string RecipientFilter
			{
				set
				{
					base.PowerSharpParameters["RecipientFilter"] = value;
				}
			}

			// Token: 0x17006A49 RID: 27209
			// (set) Token: 0x060096C4 RID: 38596 RVA: 0x000DB6EF File Offset: 0x000D98EF
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006A4A RID: 27210
			// (set) Token: 0x060096C5 RID: 38597 RVA: 0x000DB70D File Offset: 0x000D990D
			public virtual string ExpansionServer
			{
				set
				{
					base.PowerSharpParameters["ExpansionServer"] = value;
				}
			}

			// Token: 0x17006A4B RID: 27211
			// (set) Token: 0x060096C6 RID: 38598 RVA: 0x000DB720 File Offset: 0x000D9920
			public virtual string ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = ((value != null) ? new GeneralRecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17006A4C RID: 27212
			// (set) Token: 0x060096C7 RID: 38599 RVA: 0x000DB73E File Offset: 0x000D993E
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x17006A4D RID: 27213
			// (set) Token: 0x060096C8 RID: 38600 RVA: 0x000DB756 File Offset: 0x000D9956
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17006A4E RID: 27214
			// (set) Token: 0x060096C9 RID: 38601 RVA: 0x000DB769 File Offset: 0x000D9969
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17006A4F RID: 27215
			// (set) Token: 0x060096CA RID: 38602 RVA: 0x000DB77C File Offset: 0x000D997C
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17006A50 RID: 27216
			// (set) Token: 0x060096CB RID: 38603 RVA: 0x000DB78F File Offset: 0x000D998F
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006A51 RID: 27217
			// (set) Token: 0x060096CC RID: 38604 RVA: 0x000DB7AD File Offset: 0x000D99AD
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17006A52 RID: 27218
			// (set) Token: 0x060096CD RID: 38605 RVA: 0x000DB7C0 File Offset: 0x000D99C0
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17006A53 RID: 27219
			// (set) Token: 0x060096CE RID: 38606 RVA: 0x000DB7D3 File Offset: 0x000D99D3
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17006A54 RID: 27220
			// (set) Token: 0x060096CF RID: 38607 RVA: 0x000DB7E6 File Offset: 0x000D99E6
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17006A55 RID: 27221
			// (set) Token: 0x060096D0 RID: 38608 RVA: 0x000DB7F9 File Offset: 0x000D99F9
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17006A56 RID: 27222
			// (set) Token: 0x060096D1 RID: 38609 RVA: 0x000DB80C File Offset: 0x000D9A0C
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17006A57 RID: 27223
			// (set) Token: 0x060096D2 RID: 38610 RVA: 0x000DB81F File Offset: 0x000D9A1F
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x17006A58 RID: 27224
			// (set) Token: 0x060096D3 RID: 38611 RVA: 0x000DB837 File Offset: 0x000D9A37
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006A59 RID: 27225
			// (set) Token: 0x060096D4 RID: 38612 RVA: 0x000DB84F File Offset: 0x000D9A4F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006A5A RID: 27226
			// (set) Token: 0x060096D5 RID: 38613 RVA: 0x000DB862 File Offset: 0x000D9A62
			public virtual WellKnownRecipientType? IncludedRecipients
			{
				set
				{
					base.PowerSharpParameters["IncludedRecipients"] = value;
				}
			}

			// Token: 0x17006A5B RID: 27227
			// (set) Token: 0x060096D6 RID: 38614 RVA: 0x000DB87A File Offset: 0x000D9A7A
			public virtual MultiValuedProperty<string> ConditionalDepartment
			{
				set
				{
					base.PowerSharpParameters["ConditionalDepartment"] = value;
				}
			}

			// Token: 0x17006A5C RID: 27228
			// (set) Token: 0x060096D7 RID: 38615 RVA: 0x000DB88D File Offset: 0x000D9A8D
			public virtual MultiValuedProperty<string> ConditionalCompany
			{
				set
				{
					base.PowerSharpParameters["ConditionalCompany"] = value;
				}
			}

			// Token: 0x17006A5D RID: 27229
			// (set) Token: 0x060096D8 RID: 38616 RVA: 0x000DB8A0 File Offset: 0x000D9AA0
			public virtual MultiValuedProperty<string> ConditionalStateOrProvince
			{
				set
				{
					base.PowerSharpParameters["ConditionalStateOrProvince"] = value;
				}
			}

			// Token: 0x17006A5E RID: 27230
			// (set) Token: 0x060096D9 RID: 38617 RVA: 0x000DB8B3 File Offset: 0x000D9AB3
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute1"] = value;
				}
			}

			// Token: 0x17006A5F RID: 27231
			// (set) Token: 0x060096DA RID: 38618 RVA: 0x000DB8C6 File Offset: 0x000D9AC6
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute2"] = value;
				}
			}

			// Token: 0x17006A60 RID: 27232
			// (set) Token: 0x060096DB RID: 38619 RVA: 0x000DB8D9 File Offset: 0x000D9AD9
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute3"] = value;
				}
			}

			// Token: 0x17006A61 RID: 27233
			// (set) Token: 0x060096DC RID: 38620 RVA: 0x000DB8EC File Offset: 0x000D9AEC
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute4"] = value;
				}
			}

			// Token: 0x17006A62 RID: 27234
			// (set) Token: 0x060096DD RID: 38621 RVA: 0x000DB8FF File Offset: 0x000D9AFF
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute5"] = value;
				}
			}

			// Token: 0x17006A63 RID: 27235
			// (set) Token: 0x060096DE RID: 38622 RVA: 0x000DB912 File Offset: 0x000D9B12
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute6"] = value;
				}
			}

			// Token: 0x17006A64 RID: 27236
			// (set) Token: 0x060096DF RID: 38623 RVA: 0x000DB925 File Offset: 0x000D9B25
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute7"] = value;
				}
			}

			// Token: 0x17006A65 RID: 27237
			// (set) Token: 0x060096E0 RID: 38624 RVA: 0x000DB938 File Offset: 0x000D9B38
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute8"] = value;
				}
			}

			// Token: 0x17006A66 RID: 27238
			// (set) Token: 0x060096E1 RID: 38625 RVA: 0x000DB94B File Offset: 0x000D9B4B
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute9"] = value;
				}
			}

			// Token: 0x17006A67 RID: 27239
			// (set) Token: 0x060096E2 RID: 38626 RVA: 0x000DB95E File Offset: 0x000D9B5E
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute10"] = value;
				}
			}

			// Token: 0x17006A68 RID: 27240
			// (set) Token: 0x060096E3 RID: 38627 RVA: 0x000DB971 File Offset: 0x000D9B71
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute11"] = value;
				}
			}

			// Token: 0x17006A69 RID: 27241
			// (set) Token: 0x060096E4 RID: 38628 RVA: 0x000DB984 File Offset: 0x000D9B84
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute12"] = value;
				}
			}

			// Token: 0x17006A6A RID: 27242
			// (set) Token: 0x060096E5 RID: 38629 RVA: 0x000DB997 File Offset: 0x000D9B97
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute13"] = value;
				}
			}

			// Token: 0x17006A6B RID: 27243
			// (set) Token: 0x060096E6 RID: 38630 RVA: 0x000DB9AA File Offset: 0x000D9BAA
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute14"] = value;
				}
			}

			// Token: 0x17006A6C RID: 27244
			// (set) Token: 0x060096E7 RID: 38631 RVA: 0x000DB9BD File Offset: 0x000D9BBD
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute15"] = value;
				}
			}

			// Token: 0x17006A6D RID: 27245
			// (set) Token: 0x060096E8 RID: 38632 RVA: 0x000DB9D0 File Offset: 0x000D9BD0
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17006A6E RID: 27246
			// (set) Token: 0x060096E9 RID: 38633 RVA: 0x000DB9E3 File Offset: 0x000D9BE3
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17006A6F RID: 27247
			// (set) Token: 0x060096EA RID: 38634 RVA: 0x000DB9F6 File Offset: 0x000D9BF6
			public virtual bool ReportToManagerEnabled
			{
				set
				{
					base.PowerSharpParameters["ReportToManagerEnabled"] = value;
				}
			}

			// Token: 0x17006A70 RID: 27248
			// (set) Token: 0x060096EB RID: 38635 RVA: 0x000DBA0E File Offset: 0x000D9C0E
			public virtual bool ReportToOriginatorEnabled
			{
				set
				{
					base.PowerSharpParameters["ReportToOriginatorEnabled"] = value;
				}
			}

			// Token: 0x17006A71 RID: 27249
			// (set) Token: 0x060096EC RID: 38636 RVA: 0x000DBA26 File Offset: 0x000D9C26
			public virtual bool SendOofMessageToOriginatorEnabled
			{
				set
				{
					base.PowerSharpParameters["SendOofMessageToOriginatorEnabled"] = value;
				}
			}

			// Token: 0x17006A72 RID: 27250
			// (set) Token: 0x060096ED RID: 38637 RVA: 0x000DBA3E File Offset: 0x000D9C3E
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006A73 RID: 27251
			// (set) Token: 0x060096EE RID: 38638 RVA: 0x000DBA51 File Offset: 0x000D9C51
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17006A74 RID: 27252
			// (set) Token: 0x060096EF RID: 38639 RVA: 0x000DBA64 File Offset: 0x000D9C64
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17006A75 RID: 27253
			// (set) Token: 0x060096F0 RID: 38640 RVA: 0x000DBA77 File Offset: 0x000D9C77
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17006A76 RID: 27254
			// (set) Token: 0x060096F1 RID: 38641 RVA: 0x000DBA8A File Offset: 0x000D9C8A
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17006A77 RID: 27255
			// (set) Token: 0x060096F2 RID: 38642 RVA: 0x000DBA9D File Offset: 0x000D9C9D
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17006A78 RID: 27256
			// (set) Token: 0x060096F3 RID: 38643 RVA: 0x000DBAB0 File Offset: 0x000D9CB0
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17006A79 RID: 27257
			// (set) Token: 0x060096F4 RID: 38644 RVA: 0x000DBAC3 File Offset: 0x000D9CC3
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17006A7A RID: 27258
			// (set) Token: 0x060096F5 RID: 38645 RVA: 0x000DBAD6 File Offset: 0x000D9CD6
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17006A7B RID: 27259
			// (set) Token: 0x060096F6 RID: 38646 RVA: 0x000DBAE9 File Offset: 0x000D9CE9
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17006A7C RID: 27260
			// (set) Token: 0x060096F7 RID: 38647 RVA: 0x000DBAFC File Offset: 0x000D9CFC
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17006A7D RID: 27261
			// (set) Token: 0x060096F8 RID: 38648 RVA: 0x000DBB0F File Offset: 0x000D9D0F
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17006A7E RID: 27262
			// (set) Token: 0x060096F9 RID: 38649 RVA: 0x000DBB22 File Offset: 0x000D9D22
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17006A7F RID: 27263
			// (set) Token: 0x060096FA RID: 38650 RVA: 0x000DBB35 File Offset: 0x000D9D35
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17006A80 RID: 27264
			// (set) Token: 0x060096FB RID: 38651 RVA: 0x000DBB48 File Offset: 0x000D9D48
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17006A81 RID: 27265
			// (set) Token: 0x060096FC RID: 38652 RVA: 0x000DBB5B File Offset: 0x000D9D5B
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17006A82 RID: 27266
			// (set) Token: 0x060096FD RID: 38653 RVA: 0x000DBB6E File Offset: 0x000D9D6E
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17006A83 RID: 27267
			// (set) Token: 0x060096FE RID: 38654 RVA: 0x000DBB81 File Offset: 0x000D9D81
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17006A84 RID: 27268
			// (set) Token: 0x060096FF RID: 38655 RVA: 0x000DBB94 File Offset: 0x000D9D94
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17006A85 RID: 27269
			// (set) Token: 0x06009700 RID: 38656 RVA: 0x000DBBA7 File Offset: 0x000D9DA7
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17006A86 RID: 27270
			// (set) Token: 0x06009701 RID: 38657 RVA: 0x000DBBBA File Offset: 0x000D9DBA
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17006A87 RID: 27271
			// (set) Token: 0x06009702 RID: 38658 RVA: 0x000DBBCD File Offset: 0x000D9DCD
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006A88 RID: 27272
			// (set) Token: 0x06009703 RID: 38659 RVA: 0x000DBBE0 File Offset: 0x000D9DE0
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17006A89 RID: 27273
			// (set) Token: 0x06009704 RID: 38660 RVA: 0x000DBBF3 File Offset: 0x000D9DF3
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17006A8A RID: 27274
			// (set) Token: 0x06009705 RID: 38661 RVA: 0x000DBC0B File Offset: 0x000D9E0B
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x17006A8B RID: 27275
			// (set) Token: 0x06009706 RID: 38662 RVA: 0x000DBC23 File Offset: 0x000D9E23
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x17006A8C RID: 27276
			// (set) Token: 0x06009707 RID: 38663 RVA: 0x000DBC3B File Offset: 0x000D9E3B
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17006A8D RID: 27277
			// (set) Token: 0x06009708 RID: 38664 RVA: 0x000DBC53 File Offset: 0x000D9E53
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x17006A8E RID: 27278
			// (set) Token: 0x06009709 RID: 38665 RVA: 0x000DBC6B File Offset: 0x000D9E6B
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006A8F RID: 27279
			// (set) Token: 0x0600970A RID: 38666 RVA: 0x000DBC83 File Offset: 0x000D9E83
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17006A90 RID: 27280
			// (set) Token: 0x0600970B RID: 38667 RVA: 0x000DBC9B File Offset: 0x000D9E9B
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x17006A91 RID: 27281
			// (set) Token: 0x0600970C RID: 38668 RVA: 0x000DBCAE File Offset: 0x000D9EAE
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17006A92 RID: 27282
			// (set) Token: 0x0600970D RID: 38669 RVA: 0x000DBCC6 File Offset: 0x000D9EC6
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x17006A93 RID: 27283
			// (set) Token: 0x0600970E RID: 38670 RVA: 0x000DBCD9 File Offset: 0x000D9ED9
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x17006A94 RID: 27284
			// (set) Token: 0x0600970F RID: 38671 RVA: 0x000DBCF1 File Offset: 0x000D9EF1
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x17006A95 RID: 27285
			// (set) Token: 0x06009710 RID: 38672 RVA: 0x000DBD04 File Offset: 0x000D9F04
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17006A96 RID: 27286
			// (set) Token: 0x06009711 RID: 38673 RVA: 0x000DBD17 File Offset: 0x000D9F17
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006A97 RID: 27287
			// (set) Token: 0x06009712 RID: 38674 RVA: 0x000DBD2A File Offset: 0x000D9F2A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006A98 RID: 27288
			// (set) Token: 0x06009713 RID: 38675 RVA: 0x000DBD42 File Offset: 0x000D9F42
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006A99 RID: 27289
			// (set) Token: 0x06009714 RID: 38676 RVA: 0x000DBD5A File Offset: 0x000D9F5A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006A9A RID: 27290
			// (set) Token: 0x06009715 RID: 38677 RVA: 0x000DBD72 File Offset: 0x000D9F72
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006A9B RID: 27291
			// (set) Token: 0x06009716 RID: 38678 RVA: 0x000DBD8A File Offset: 0x000D9F8A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C1A RID: 3098
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006A9C RID: 27292
			// (set) Token: 0x06009718 RID: 38680 RVA: 0x000DBDAA File Offset: 0x000D9FAA
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DynamicGroupIdParameter(value) : null);
				}
			}

			// Token: 0x17006A9D RID: 27293
			// (set) Token: 0x06009719 RID: 38681 RVA: 0x000DBDC8 File Offset: 0x000D9FC8
			public virtual string RecipientFilter
			{
				set
				{
					base.PowerSharpParameters["RecipientFilter"] = value;
				}
			}

			// Token: 0x17006A9E RID: 27294
			// (set) Token: 0x0600971A RID: 38682 RVA: 0x000DBDDB File Offset: 0x000D9FDB
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006A9F RID: 27295
			// (set) Token: 0x0600971B RID: 38683 RVA: 0x000DBDF9 File Offset: 0x000D9FF9
			public virtual string ExpansionServer
			{
				set
				{
					base.PowerSharpParameters["ExpansionServer"] = value;
				}
			}

			// Token: 0x17006AA0 RID: 27296
			// (set) Token: 0x0600971C RID: 38684 RVA: 0x000DBE0C File Offset: 0x000DA00C
			public virtual string ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = ((value != null) ? new GeneralRecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17006AA1 RID: 27297
			// (set) Token: 0x0600971D RID: 38685 RVA: 0x000DBE2A File Offset: 0x000DA02A
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x17006AA2 RID: 27298
			// (set) Token: 0x0600971E RID: 38686 RVA: 0x000DBE42 File Offset: 0x000DA042
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17006AA3 RID: 27299
			// (set) Token: 0x0600971F RID: 38687 RVA: 0x000DBE55 File Offset: 0x000DA055
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17006AA4 RID: 27300
			// (set) Token: 0x06009720 RID: 38688 RVA: 0x000DBE68 File Offset: 0x000DA068
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17006AA5 RID: 27301
			// (set) Token: 0x06009721 RID: 38689 RVA: 0x000DBE7B File Offset: 0x000DA07B
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006AA6 RID: 27302
			// (set) Token: 0x06009722 RID: 38690 RVA: 0x000DBE99 File Offset: 0x000DA099
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17006AA7 RID: 27303
			// (set) Token: 0x06009723 RID: 38691 RVA: 0x000DBEAC File Offset: 0x000DA0AC
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17006AA8 RID: 27304
			// (set) Token: 0x06009724 RID: 38692 RVA: 0x000DBEBF File Offset: 0x000DA0BF
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17006AA9 RID: 27305
			// (set) Token: 0x06009725 RID: 38693 RVA: 0x000DBED2 File Offset: 0x000DA0D2
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17006AAA RID: 27306
			// (set) Token: 0x06009726 RID: 38694 RVA: 0x000DBEE5 File Offset: 0x000DA0E5
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17006AAB RID: 27307
			// (set) Token: 0x06009727 RID: 38695 RVA: 0x000DBEF8 File Offset: 0x000DA0F8
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17006AAC RID: 27308
			// (set) Token: 0x06009728 RID: 38696 RVA: 0x000DBF0B File Offset: 0x000DA10B
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x17006AAD RID: 27309
			// (set) Token: 0x06009729 RID: 38697 RVA: 0x000DBF23 File Offset: 0x000DA123
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006AAE RID: 27310
			// (set) Token: 0x0600972A RID: 38698 RVA: 0x000DBF3B File Offset: 0x000DA13B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006AAF RID: 27311
			// (set) Token: 0x0600972B RID: 38699 RVA: 0x000DBF4E File Offset: 0x000DA14E
			public virtual WellKnownRecipientType? IncludedRecipients
			{
				set
				{
					base.PowerSharpParameters["IncludedRecipients"] = value;
				}
			}

			// Token: 0x17006AB0 RID: 27312
			// (set) Token: 0x0600972C RID: 38700 RVA: 0x000DBF66 File Offset: 0x000DA166
			public virtual MultiValuedProperty<string> ConditionalDepartment
			{
				set
				{
					base.PowerSharpParameters["ConditionalDepartment"] = value;
				}
			}

			// Token: 0x17006AB1 RID: 27313
			// (set) Token: 0x0600972D RID: 38701 RVA: 0x000DBF79 File Offset: 0x000DA179
			public virtual MultiValuedProperty<string> ConditionalCompany
			{
				set
				{
					base.PowerSharpParameters["ConditionalCompany"] = value;
				}
			}

			// Token: 0x17006AB2 RID: 27314
			// (set) Token: 0x0600972E RID: 38702 RVA: 0x000DBF8C File Offset: 0x000DA18C
			public virtual MultiValuedProperty<string> ConditionalStateOrProvince
			{
				set
				{
					base.PowerSharpParameters["ConditionalStateOrProvince"] = value;
				}
			}

			// Token: 0x17006AB3 RID: 27315
			// (set) Token: 0x0600972F RID: 38703 RVA: 0x000DBF9F File Offset: 0x000DA19F
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute1"] = value;
				}
			}

			// Token: 0x17006AB4 RID: 27316
			// (set) Token: 0x06009730 RID: 38704 RVA: 0x000DBFB2 File Offset: 0x000DA1B2
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute2"] = value;
				}
			}

			// Token: 0x17006AB5 RID: 27317
			// (set) Token: 0x06009731 RID: 38705 RVA: 0x000DBFC5 File Offset: 0x000DA1C5
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute3"] = value;
				}
			}

			// Token: 0x17006AB6 RID: 27318
			// (set) Token: 0x06009732 RID: 38706 RVA: 0x000DBFD8 File Offset: 0x000DA1D8
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute4"] = value;
				}
			}

			// Token: 0x17006AB7 RID: 27319
			// (set) Token: 0x06009733 RID: 38707 RVA: 0x000DBFEB File Offset: 0x000DA1EB
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute5"] = value;
				}
			}

			// Token: 0x17006AB8 RID: 27320
			// (set) Token: 0x06009734 RID: 38708 RVA: 0x000DBFFE File Offset: 0x000DA1FE
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute6"] = value;
				}
			}

			// Token: 0x17006AB9 RID: 27321
			// (set) Token: 0x06009735 RID: 38709 RVA: 0x000DC011 File Offset: 0x000DA211
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute7"] = value;
				}
			}

			// Token: 0x17006ABA RID: 27322
			// (set) Token: 0x06009736 RID: 38710 RVA: 0x000DC024 File Offset: 0x000DA224
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute8"] = value;
				}
			}

			// Token: 0x17006ABB RID: 27323
			// (set) Token: 0x06009737 RID: 38711 RVA: 0x000DC037 File Offset: 0x000DA237
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute9"] = value;
				}
			}

			// Token: 0x17006ABC RID: 27324
			// (set) Token: 0x06009738 RID: 38712 RVA: 0x000DC04A File Offset: 0x000DA24A
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute10"] = value;
				}
			}

			// Token: 0x17006ABD RID: 27325
			// (set) Token: 0x06009739 RID: 38713 RVA: 0x000DC05D File Offset: 0x000DA25D
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute11"] = value;
				}
			}

			// Token: 0x17006ABE RID: 27326
			// (set) Token: 0x0600973A RID: 38714 RVA: 0x000DC070 File Offset: 0x000DA270
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute12"] = value;
				}
			}

			// Token: 0x17006ABF RID: 27327
			// (set) Token: 0x0600973B RID: 38715 RVA: 0x000DC083 File Offset: 0x000DA283
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute13"] = value;
				}
			}

			// Token: 0x17006AC0 RID: 27328
			// (set) Token: 0x0600973C RID: 38716 RVA: 0x000DC096 File Offset: 0x000DA296
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute14"] = value;
				}
			}

			// Token: 0x17006AC1 RID: 27329
			// (set) Token: 0x0600973D RID: 38717 RVA: 0x000DC0A9 File Offset: 0x000DA2A9
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute15"] = value;
				}
			}

			// Token: 0x17006AC2 RID: 27330
			// (set) Token: 0x0600973E RID: 38718 RVA: 0x000DC0BC File Offset: 0x000DA2BC
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17006AC3 RID: 27331
			// (set) Token: 0x0600973F RID: 38719 RVA: 0x000DC0CF File Offset: 0x000DA2CF
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17006AC4 RID: 27332
			// (set) Token: 0x06009740 RID: 38720 RVA: 0x000DC0E2 File Offset: 0x000DA2E2
			public virtual bool ReportToManagerEnabled
			{
				set
				{
					base.PowerSharpParameters["ReportToManagerEnabled"] = value;
				}
			}

			// Token: 0x17006AC5 RID: 27333
			// (set) Token: 0x06009741 RID: 38721 RVA: 0x000DC0FA File Offset: 0x000DA2FA
			public virtual bool ReportToOriginatorEnabled
			{
				set
				{
					base.PowerSharpParameters["ReportToOriginatorEnabled"] = value;
				}
			}

			// Token: 0x17006AC6 RID: 27334
			// (set) Token: 0x06009742 RID: 38722 RVA: 0x000DC112 File Offset: 0x000DA312
			public virtual bool SendOofMessageToOriginatorEnabled
			{
				set
				{
					base.PowerSharpParameters["SendOofMessageToOriginatorEnabled"] = value;
				}
			}

			// Token: 0x17006AC7 RID: 27335
			// (set) Token: 0x06009743 RID: 38723 RVA: 0x000DC12A File Offset: 0x000DA32A
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006AC8 RID: 27336
			// (set) Token: 0x06009744 RID: 38724 RVA: 0x000DC13D File Offset: 0x000DA33D
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17006AC9 RID: 27337
			// (set) Token: 0x06009745 RID: 38725 RVA: 0x000DC150 File Offset: 0x000DA350
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17006ACA RID: 27338
			// (set) Token: 0x06009746 RID: 38726 RVA: 0x000DC163 File Offset: 0x000DA363
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17006ACB RID: 27339
			// (set) Token: 0x06009747 RID: 38727 RVA: 0x000DC176 File Offset: 0x000DA376
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17006ACC RID: 27340
			// (set) Token: 0x06009748 RID: 38728 RVA: 0x000DC189 File Offset: 0x000DA389
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17006ACD RID: 27341
			// (set) Token: 0x06009749 RID: 38729 RVA: 0x000DC19C File Offset: 0x000DA39C
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17006ACE RID: 27342
			// (set) Token: 0x0600974A RID: 38730 RVA: 0x000DC1AF File Offset: 0x000DA3AF
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17006ACF RID: 27343
			// (set) Token: 0x0600974B RID: 38731 RVA: 0x000DC1C2 File Offset: 0x000DA3C2
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17006AD0 RID: 27344
			// (set) Token: 0x0600974C RID: 38732 RVA: 0x000DC1D5 File Offset: 0x000DA3D5
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17006AD1 RID: 27345
			// (set) Token: 0x0600974D RID: 38733 RVA: 0x000DC1E8 File Offset: 0x000DA3E8
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17006AD2 RID: 27346
			// (set) Token: 0x0600974E RID: 38734 RVA: 0x000DC1FB File Offset: 0x000DA3FB
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17006AD3 RID: 27347
			// (set) Token: 0x0600974F RID: 38735 RVA: 0x000DC20E File Offset: 0x000DA40E
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17006AD4 RID: 27348
			// (set) Token: 0x06009750 RID: 38736 RVA: 0x000DC221 File Offset: 0x000DA421
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17006AD5 RID: 27349
			// (set) Token: 0x06009751 RID: 38737 RVA: 0x000DC234 File Offset: 0x000DA434
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17006AD6 RID: 27350
			// (set) Token: 0x06009752 RID: 38738 RVA: 0x000DC247 File Offset: 0x000DA447
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17006AD7 RID: 27351
			// (set) Token: 0x06009753 RID: 38739 RVA: 0x000DC25A File Offset: 0x000DA45A
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17006AD8 RID: 27352
			// (set) Token: 0x06009754 RID: 38740 RVA: 0x000DC26D File Offset: 0x000DA46D
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17006AD9 RID: 27353
			// (set) Token: 0x06009755 RID: 38741 RVA: 0x000DC280 File Offset: 0x000DA480
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17006ADA RID: 27354
			// (set) Token: 0x06009756 RID: 38742 RVA: 0x000DC293 File Offset: 0x000DA493
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17006ADB RID: 27355
			// (set) Token: 0x06009757 RID: 38743 RVA: 0x000DC2A6 File Offset: 0x000DA4A6
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17006ADC RID: 27356
			// (set) Token: 0x06009758 RID: 38744 RVA: 0x000DC2B9 File Offset: 0x000DA4B9
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006ADD RID: 27357
			// (set) Token: 0x06009759 RID: 38745 RVA: 0x000DC2CC File Offset: 0x000DA4CC
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17006ADE RID: 27358
			// (set) Token: 0x0600975A RID: 38746 RVA: 0x000DC2DF File Offset: 0x000DA4DF
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17006ADF RID: 27359
			// (set) Token: 0x0600975B RID: 38747 RVA: 0x000DC2F7 File Offset: 0x000DA4F7
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x17006AE0 RID: 27360
			// (set) Token: 0x0600975C RID: 38748 RVA: 0x000DC30F File Offset: 0x000DA50F
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x17006AE1 RID: 27361
			// (set) Token: 0x0600975D RID: 38749 RVA: 0x000DC327 File Offset: 0x000DA527
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17006AE2 RID: 27362
			// (set) Token: 0x0600975E RID: 38750 RVA: 0x000DC33F File Offset: 0x000DA53F
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x17006AE3 RID: 27363
			// (set) Token: 0x0600975F RID: 38751 RVA: 0x000DC357 File Offset: 0x000DA557
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006AE4 RID: 27364
			// (set) Token: 0x06009760 RID: 38752 RVA: 0x000DC36F File Offset: 0x000DA56F
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17006AE5 RID: 27365
			// (set) Token: 0x06009761 RID: 38753 RVA: 0x000DC387 File Offset: 0x000DA587
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x17006AE6 RID: 27366
			// (set) Token: 0x06009762 RID: 38754 RVA: 0x000DC39A File Offset: 0x000DA59A
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17006AE7 RID: 27367
			// (set) Token: 0x06009763 RID: 38755 RVA: 0x000DC3B2 File Offset: 0x000DA5B2
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x17006AE8 RID: 27368
			// (set) Token: 0x06009764 RID: 38756 RVA: 0x000DC3C5 File Offset: 0x000DA5C5
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x17006AE9 RID: 27369
			// (set) Token: 0x06009765 RID: 38757 RVA: 0x000DC3DD File Offset: 0x000DA5DD
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x17006AEA RID: 27370
			// (set) Token: 0x06009766 RID: 38758 RVA: 0x000DC3F0 File Offset: 0x000DA5F0
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17006AEB RID: 27371
			// (set) Token: 0x06009767 RID: 38759 RVA: 0x000DC403 File Offset: 0x000DA603
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006AEC RID: 27372
			// (set) Token: 0x06009768 RID: 38760 RVA: 0x000DC416 File Offset: 0x000DA616
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006AED RID: 27373
			// (set) Token: 0x06009769 RID: 38761 RVA: 0x000DC42E File Offset: 0x000DA62E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006AEE RID: 27374
			// (set) Token: 0x0600976A RID: 38762 RVA: 0x000DC446 File Offset: 0x000DA646
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006AEF RID: 27375
			// (set) Token: 0x0600976B RID: 38763 RVA: 0x000DC45E File Offset: 0x000DA65E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006AF0 RID: 27376
			// (set) Token: 0x0600976C RID: 38764 RVA: 0x000DC476 File Offset: 0x000DA676
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
