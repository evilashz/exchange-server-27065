using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200024D RID: 589
	public class SetMailPublicFolderCommand : SyntheticCommandWithPipelineInputNoOutput<MailPublicFolder>
	{
		// Token: 0x06002BBB RID: 11195 RVA: 0x00050847 File Offset: 0x0004EA47
		private SetMailPublicFolderCommand() : base("Set-MailPublicFolder")
		{
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x00050854 File Offset: 0x0004EA54
		public SetMailPublicFolderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x00050863 File Offset: 0x0004EA63
		public virtual SetMailPublicFolderCommand SetParameters(SetMailPublicFolderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x0005086D File Offset: 0x0004EA6D
		public virtual SetMailPublicFolderCommand SetParameters(SetMailPublicFolderCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200024E RID: 590
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170012DA RID: 4826
			// (set) Token: 0x06002BBF RID: 11199 RVA: 0x00050877 File Offset: 0x0004EA77
			public virtual RecipientIdParameter Contacts
			{
				set
				{
					base.PowerSharpParameters["Contacts"] = value;
				}
			}

			// Token: 0x170012DB RID: 4827
			// (set) Token: 0x06002BC0 RID: 11200 RVA: 0x0005088A File Offset: 0x0004EA8A
			public virtual string ForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x170012DC RID: 4828
			// (set) Token: 0x06002BC1 RID: 11201 RVA: 0x000508A8 File Offset: 0x0004EAA8
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x170012DD RID: 4829
			// (set) Token: 0x06002BC2 RID: 11202 RVA: 0x000508BB File Offset: 0x0004EABB
			public virtual string EntryId
			{
				set
				{
					base.PowerSharpParameters["EntryId"] = value;
				}
			}

			// Token: 0x170012DE RID: 4830
			// (set) Token: 0x06002BC3 RID: 11203 RVA: 0x000508CE File Offset: 0x0004EACE
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x170012DF RID: 4831
			// (set) Token: 0x06002BC4 RID: 11204 RVA: 0x000508E1 File Offset: 0x0004EAE1
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x170012E0 RID: 4832
			// (set) Token: 0x06002BC5 RID: 11205 RVA: 0x000508F4 File Offset: 0x0004EAF4
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x170012E1 RID: 4833
			// (set) Token: 0x06002BC6 RID: 11206 RVA: 0x00050907 File Offset: 0x0004EB07
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170012E2 RID: 4834
			// (set) Token: 0x06002BC7 RID: 11207 RVA: 0x00050925 File Offset: 0x0004EB25
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x170012E3 RID: 4835
			// (set) Token: 0x06002BC8 RID: 11208 RVA: 0x00050938 File Offset: 0x0004EB38
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170012E4 RID: 4836
			// (set) Token: 0x06002BC9 RID: 11209 RVA: 0x0005094B File Offset: 0x0004EB4B
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x170012E5 RID: 4837
			// (set) Token: 0x06002BCA RID: 11210 RVA: 0x0005095E File Offset: 0x0004EB5E
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x170012E6 RID: 4838
			// (set) Token: 0x06002BCB RID: 11211 RVA: 0x00050971 File Offset: 0x0004EB71
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x170012E7 RID: 4839
			// (set) Token: 0x06002BCC RID: 11212 RVA: 0x00050984 File Offset: 0x0004EB84
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x170012E8 RID: 4840
			// (set) Token: 0x06002BCD RID: 11213 RVA: 0x00050997 File Offset: 0x0004EB97
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x170012E9 RID: 4841
			// (set) Token: 0x06002BCE RID: 11214 RVA: 0x000509AF File Offset: 0x0004EBAF
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170012EA RID: 4842
			// (set) Token: 0x06002BCF RID: 11215 RVA: 0x000509C7 File Offset: 0x0004EBC7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170012EB RID: 4843
			// (set) Token: 0x06002BD0 RID: 11216 RVA: 0x000509DA File Offset: 0x0004EBDA
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x170012EC RID: 4844
			// (set) Token: 0x06002BD1 RID: 11217 RVA: 0x000509F2 File Offset: 0x0004EBF2
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x170012ED RID: 4845
			// (set) Token: 0x06002BD2 RID: 11218 RVA: 0x00050A05 File Offset: 0x0004EC05
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170012EE RID: 4846
			// (set) Token: 0x06002BD3 RID: 11219 RVA: 0x00050A18 File Offset: 0x0004EC18
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x170012EF RID: 4847
			// (set) Token: 0x06002BD4 RID: 11220 RVA: 0x00050A2B File Offset: 0x0004EC2B
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x170012F0 RID: 4848
			// (set) Token: 0x06002BD5 RID: 11221 RVA: 0x00050A3E File Offset: 0x0004EC3E
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x170012F1 RID: 4849
			// (set) Token: 0x06002BD6 RID: 11222 RVA: 0x00050A51 File Offset: 0x0004EC51
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x170012F2 RID: 4850
			// (set) Token: 0x06002BD7 RID: 11223 RVA: 0x00050A64 File Offset: 0x0004EC64
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x170012F3 RID: 4851
			// (set) Token: 0x06002BD8 RID: 11224 RVA: 0x00050A77 File Offset: 0x0004EC77
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x170012F4 RID: 4852
			// (set) Token: 0x06002BD9 RID: 11225 RVA: 0x00050A8A File Offset: 0x0004EC8A
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x170012F5 RID: 4853
			// (set) Token: 0x06002BDA RID: 11226 RVA: 0x00050A9D File Offset: 0x0004EC9D
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x170012F6 RID: 4854
			// (set) Token: 0x06002BDB RID: 11227 RVA: 0x00050AB0 File Offset: 0x0004ECB0
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x170012F7 RID: 4855
			// (set) Token: 0x06002BDC RID: 11228 RVA: 0x00050AC3 File Offset: 0x0004ECC3
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x170012F8 RID: 4856
			// (set) Token: 0x06002BDD RID: 11229 RVA: 0x00050AD6 File Offset: 0x0004ECD6
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x170012F9 RID: 4857
			// (set) Token: 0x06002BDE RID: 11230 RVA: 0x00050AE9 File Offset: 0x0004ECE9
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x170012FA RID: 4858
			// (set) Token: 0x06002BDF RID: 11231 RVA: 0x00050AFC File Offset: 0x0004ECFC
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x170012FB RID: 4859
			// (set) Token: 0x06002BE0 RID: 11232 RVA: 0x00050B0F File Offset: 0x0004ED0F
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x170012FC RID: 4860
			// (set) Token: 0x06002BE1 RID: 11233 RVA: 0x00050B22 File Offset: 0x0004ED22
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x170012FD RID: 4861
			// (set) Token: 0x06002BE2 RID: 11234 RVA: 0x00050B35 File Offset: 0x0004ED35
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x170012FE RID: 4862
			// (set) Token: 0x06002BE3 RID: 11235 RVA: 0x00050B48 File Offset: 0x0004ED48
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x170012FF RID: 4863
			// (set) Token: 0x06002BE4 RID: 11236 RVA: 0x00050B5B File Offset: 0x0004ED5B
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17001300 RID: 4864
			// (set) Token: 0x06002BE5 RID: 11237 RVA: 0x00050B6E File Offset: 0x0004ED6E
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17001301 RID: 4865
			// (set) Token: 0x06002BE6 RID: 11238 RVA: 0x00050B81 File Offset: 0x0004ED81
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17001302 RID: 4866
			// (set) Token: 0x06002BE7 RID: 11239 RVA: 0x00050B94 File Offset: 0x0004ED94
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17001303 RID: 4867
			// (set) Token: 0x06002BE8 RID: 11240 RVA: 0x00050BA7 File Offset: 0x0004EDA7
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17001304 RID: 4868
			// (set) Token: 0x06002BE9 RID: 11241 RVA: 0x00050BBA File Offset: 0x0004EDBA
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17001305 RID: 4869
			// (set) Token: 0x06002BEA RID: 11242 RVA: 0x00050BD2 File Offset: 0x0004EDD2
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x17001306 RID: 4870
			// (set) Token: 0x06002BEB RID: 11243 RVA: 0x00050BEA File Offset: 0x0004EDEA
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x17001307 RID: 4871
			// (set) Token: 0x06002BEC RID: 11244 RVA: 0x00050C02 File Offset: 0x0004EE02
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17001308 RID: 4872
			// (set) Token: 0x06002BED RID: 11245 RVA: 0x00050C1A File Offset: 0x0004EE1A
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x17001309 RID: 4873
			// (set) Token: 0x06002BEE RID: 11246 RVA: 0x00050C32 File Offset: 0x0004EE32
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700130A RID: 4874
			// (set) Token: 0x06002BEF RID: 11247 RVA: 0x00050C4A File Offset: 0x0004EE4A
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x1700130B RID: 4875
			// (set) Token: 0x06002BF0 RID: 11248 RVA: 0x00050C62 File Offset: 0x0004EE62
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x1700130C RID: 4876
			// (set) Token: 0x06002BF1 RID: 11249 RVA: 0x00050C75 File Offset: 0x0004EE75
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700130D RID: 4877
			// (set) Token: 0x06002BF2 RID: 11250 RVA: 0x00050C8D File Offset: 0x0004EE8D
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x1700130E RID: 4878
			// (set) Token: 0x06002BF3 RID: 11251 RVA: 0x00050CA0 File Offset: 0x0004EEA0
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x1700130F RID: 4879
			// (set) Token: 0x06002BF4 RID: 11252 RVA: 0x00050CB8 File Offset: 0x0004EEB8
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x17001310 RID: 4880
			// (set) Token: 0x06002BF5 RID: 11253 RVA: 0x00050CCB File Offset: 0x0004EECB
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17001311 RID: 4881
			// (set) Token: 0x06002BF6 RID: 11254 RVA: 0x00050CDE File Offset: 0x0004EEDE
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001312 RID: 4882
			// (set) Token: 0x06002BF7 RID: 11255 RVA: 0x00050CF1 File Offset: 0x0004EEF1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001313 RID: 4883
			// (set) Token: 0x06002BF8 RID: 11256 RVA: 0x00050D09 File Offset: 0x0004EF09
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001314 RID: 4884
			// (set) Token: 0x06002BF9 RID: 11257 RVA: 0x00050D21 File Offset: 0x0004EF21
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001315 RID: 4885
			// (set) Token: 0x06002BFA RID: 11258 RVA: 0x00050D39 File Offset: 0x0004EF39
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001316 RID: 4886
			// (set) Token: 0x06002BFB RID: 11259 RVA: 0x00050D51 File Offset: 0x0004EF51
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200024F RID: 591
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001317 RID: 4887
			// (set) Token: 0x06002BFD RID: 11261 RVA: 0x00050D71 File Offset: 0x0004EF71
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailPublicFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17001318 RID: 4888
			// (set) Token: 0x06002BFE RID: 11262 RVA: 0x00050D8F File Offset: 0x0004EF8F
			public virtual RecipientIdParameter Contacts
			{
				set
				{
					base.PowerSharpParameters["Contacts"] = value;
				}
			}

			// Token: 0x17001319 RID: 4889
			// (set) Token: 0x06002BFF RID: 11263 RVA: 0x00050DA2 File Offset: 0x0004EFA2
			public virtual string ForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700131A RID: 4890
			// (set) Token: 0x06002C00 RID: 11264 RVA: 0x00050DC0 File Offset: 0x0004EFC0
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x1700131B RID: 4891
			// (set) Token: 0x06002C01 RID: 11265 RVA: 0x00050DD3 File Offset: 0x0004EFD3
			public virtual string EntryId
			{
				set
				{
					base.PowerSharpParameters["EntryId"] = value;
				}
			}

			// Token: 0x1700131C RID: 4892
			// (set) Token: 0x06002C02 RID: 11266 RVA: 0x00050DE6 File Offset: 0x0004EFE6
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x1700131D RID: 4893
			// (set) Token: 0x06002C03 RID: 11267 RVA: 0x00050DF9 File Offset: 0x0004EFF9
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x1700131E RID: 4894
			// (set) Token: 0x06002C04 RID: 11268 RVA: 0x00050E0C File Offset: 0x0004F00C
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x1700131F RID: 4895
			// (set) Token: 0x06002C05 RID: 11269 RVA: 0x00050E1F File Offset: 0x0004F01F
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001320 RID: 4896
			// (set) Token: 0x06002C06 RID: 11270 RVA: 0x00050E3D File Offset: 0x0004F03D
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17001321 RID: 4897
			// (set) Token: 0x06002C07 RID: 11271 RVA: 0x00050E50 File Offset: 0x0004F050
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17001322 RID: 4898
			// (set) Token: 0x06002C08 RID: 11272 RVA: 0x00050E63 File Offset: 0x0004F063
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17001323 RID: 4899
			// (set) Token: 0x06002C09 RID: 11273 RVA: 0x00050E76 File Offset: 0x0004F076
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17001324 RID: 4900
			// (set) Token: 0x06002C0A RID: 11274 RVA: 0x00050E89 File Offset: 0x0004F089
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17001325 RID: 4901
			// (set) Token: 0x06002C0B RID: 11275 RVA: 0x00050E9C File Offset: 0x0004F09C
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17001326 RID: 4902
			// (set) Token: 0x06002C0C RID: 11276 RVA: 0x00050EAF File Offset: 0x0004F0AF
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x17001327 RID: 4903
			// (set) Token: 0x06002C0D RID: 11277 RVA: 0x00050EC7 File Offset: 0x0004F0C7
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17001328 RID: 4904
			// (set) Token: 0x06002C0E RID: 11278 RVA: 0x00050EDF File Offset: 0x0004F0DF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001329 RID: 4905
			// (set) Token: 0x06002C0F RID: 11279 RVA: 0x00050EF2 File Offset: 0x0004F0F2
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x1700132A RID: 4906
			// (set) Token: 0x06002C10 RID: 11280 RVA: 0x00050F0A File Offset: 0x0004F10A
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x1700132B RID: 4907
			// (set) Token: 0x06002C11 RID: 11281 RVA: 0x00050F1D File Offset: 0x0004F11D
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700132C RID: 4908
			// (set) Token: 0x06002C12 RID: 11282 RVA: 0x00050F30 File Offset: 0x0004F130
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x1700132D RID: 4909
			// (set) Token: 0x06002C13 RID: 11283 RVA: 0x00050F43 File Offset: 0x0004F143
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x1700132E RID: 4910
			// (set) Token: 0x06002C14 RID: 11284 RVA: 0x00050F56 File Offset: 0x0004F156
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x1700132F RID: 4911
			// (set) Token: 0x06002C15 RID: 11285 RVA: 0x00050F69 File Offset: 0x0004F169
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17001330 RID: 4912
			// (set) Token: 0x06002C16 RID: 11286 RVA: 0x00050F7C File Offset: 0x0004F17C
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17001331 RID: 4913
			// (set) Token: 0x06002C17 RID: 11287 RVA: 0x00050F8F File Offset: 0x0004F18F
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17001332 RID: 4914
			// (set) Token: 0x06002C18 RID: 11288 RVA: 0x00050FA2 File Offset: 0x0004F1A2
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17001333 RID: 4915
			// (set) Token: 0x06002C19 RID: 11289 RVA: 0x00050FB5 File Offset: 0x0004F1B5
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17001334 RID: 4916
			// (set) Token: 0x06002C1A RID: 11290 RVA: 0x00050FC8 File Offset: 0x0004F1C8
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17001335 RID: 4917
			// (set) Token: 0x06002C1B RID: 11291 RVA: 0x00050FDB File Offset: 0x0004F1DB
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17001336 RID: 4918
			// (set) Token: 0x06002C1C RID: 11292 RVA: 0x00050FEE File Offset: 0x0004F1EE
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17001337 RID: 4919
			// (set) Token: 0x06002C1D RID: 11293 RVA: 0x00051001 File Offset: 0x0004F201
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17001338 RID: 4920
			// (set) Token: 0x06002C1E RID: 11294 RVA: 0x00051014 File Offset: 0x0004F214
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17001339 RID: 4921
			// (set) Token: 0x06002C1F RID: 11295 RVA: 0x00051027 File Offset: 0x0004F227
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x1700133A RID: 4922
			// (set) Token: 0x06002C20 RID: 11296 RVA: 0x0005103A File Offset: 0x0004F23A
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x1700133B RID: 4923
			// (set) Token: 0x06002C21 RID: 11297 RVA: 0x0005104D File Offset: 0x0004F24D
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x1700133C RID: 4924
			// (set) Token: 0x06002C22 RID: 11298 RVA: 0x00051060 File Offset: 0x0004F260
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x1700133D RID: 4925
			// (set) Token: 0x06002C23 RID: 11299 RVA: 0x00051073 File Offset: 0x0004F273
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x1700133E RID: 4926
			// (set) Token: 0x06002C24 RID: 11300 RVA: 0x00051086 File Offset: 0x0004F286
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x1700133F RID: 4927
			// (set) Token: 0x06002C25 RID: 11301 RVA: 0x00051099 File Offset: 0x0004F299
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17001340 RID: 4928
			// (set) Token: 0x06002C26 RID: 11302 RVA: 0x000510AC File Offset: 0x0004F2AC
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17001341 RID: 4929
			// (set) Token: 0x06002C27 RID: 11303 RVA: 0x000510BF File Offset: 0x0004F2BF
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17001342 RID: 4930
			// (set) Token: 0x06002C28 RID: 11304 RVA: 0x000510D2 File Offset: 0x0004F2D2
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17001343 RID: 4931
			// (set) Token: 0x06002C29 RID: 11305 RVA: 0x000510EA File Offset: 0x0004F2EA
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x17001344 RID: 4932
			// (set) Token: 0x06002C2A RID: 11306 RVA: 0x00051102 File Offset: 0x0004F302
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x17001345 RID: 4933
			// (set) Token: 0x06002C2B RID: 11307 RVA: 0x0005111A File Offset: 0x0004F31A
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17001346 RID: 4934
			// (set) Token: 0x06002C2C RID: 11308 RVA: 0x00051132 File Offset: 0x0004F332
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x17001347 RID: 4935
			// (set) Token: 0x06002C2D RID: 11309 RVA: 0x0005114A File Offset: 0x0004F34A
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17001348 RID: 4936
			// (set) Token: 0x06002C2E RID: 11310 RVA: 0x00051162 File Offset: 0x0004F362
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17001349 RID: 4937
			// (set) Token: 0x06002C2F RID: 11311 RVA: 0x0005117A File Offset: 0x0004F37A
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x1700134A RID: 4938
			// (set) Token: 0x06002C30 RID: 11312 RVA: 0x0005118D File Offset: 0x0004F38D
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700134B RID: 4939
			// (set) Token: 0x06002C31 RID: 11313 RVA: 0x000511A5 File Offset: 0x0004F3A5
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x1700134C RID: 4940
			// (set) Token: 0x06002C32 RID: 11314 RVA: 0x000511B8 File Offset: 0x0004F3B8
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x1700134D RID: 4941
			// (set) Token: 0x06002C33 RID: 11315 RVA: 0x000511D0 File Offset: 0x0004F3D0
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x1700134E RID: 4942
			// (set) Token: 0x06002C34 RID: 11316 RVA: 0x000511E3 File Offset: 0x0004F3E3
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x1700134F RID: 4943
			// (set) Token: 0x06002C35 RID: 11317 RVA: 0x000511F6 File Offset: 0x0004F3F6
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001350 RID: 4944
			// (set) Token: 0x06002C36 RID: 11318 RVA: 0x00051209 File Offset: 0x0004F409
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001351 RID: 4945
			// (set) Token: 0x06002C37 RID: 11319 RVA: 0x00051221 File Offset: 0x0004F421
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001352 RID: 4946
			// (set) Token: 0x06002C38 RID: 11320 RVA: 0x00051239 File Offset: 0x0004F439
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001353 RID: 4947
			// (set) Token: 0x06002C39 RID: 11321 RVA: 0x00051251 File Offset: 0x0004F451
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001354 RID: 4948
			// (set) Token: 0x06002C3A RID: 11322 RVA: 0x00051269 File Offset: 0x0004F469
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
