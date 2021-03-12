using System;
using System.Management.Automation;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DB4 RID: 3508
	public class NewSyncMailContactCommand : SyntheticCommandWithPipelineInputNoOutput<ProxyAddress>
	{
		// Token: 0x0600C9D0 RID: 51664 RVA: 0x0012030F File Offset: 0x0011E50F
		private NewSyncMailContactCommand() : base("New-SyncMailContact")
		{
		}

		// Token: 0x0600C9D1 RID: 51665 RVA: 0x0012031C File Offset: 0x0011E51C
		public NewSyncMailContactCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600C9D2 RID: 51666 RVA: 0x0012032B File Offset: 0x0011E52B
		public virtual NewSyncMailContactCommand SetParameters(NewSyncMailContactCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DB5 RID: 3509
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17009A21 RID: 39457
			// (set) Token: 0x0600C9D3 RID: 51667 RVA: 0x00120335 File Offset: 0x0011E535
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17009A22 RID: 39458
			// (set) Token: 0x0600C9D4 RID: 51668 RVA: 0x00120348 File Offset: 0x0011E548
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17009A23 RID: 39459
			// (set) Token: 0x0600C9D5 RID: 51669 RVA: 0x0012035B File Offset: 0x0011E55B
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17009A24 RID: 39460
			// (set) Token: 0x0600C9D6 RID: 51670 RVA: 0x0012036E File Offset: 0x0011E56E
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17009A25 RID: 39461
			// (set) Token: 0x0600C9D7 RID: 51671 RVA: 0x00120381 File Offset: 0x0011E581
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17009A26 RID: 39462
			// (set) Token: 0x0600C9D8 RID: 51672 RVA: 0x00120394 File Offset: 0x0011E594
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17009A27 RID: 39463
			// (set) Token: 0x0600C9D9 RID: 51673 RVA: 0x001203AC File Offset: 0x0011E5AC
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17009A28 RID: 39464
			// (set) Token: 0x0600C9DA RID: 51674 RVA: 0x001203BF File Offset: 0x0011E5BF
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17009A29 RID: 39465
			// (set) Token: 0x0600C9DB RID: 51675 RVA: 0x001203D2 File Offset: 0x0011E5D2
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17009A2A RID: 39466
			// (set) Token: 0x0600C9DC RID: 51676 RVA: 0x001203E5 File Offset: 0x0011E5E5
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17009A2B RID: 39467
			// (set) Token: 0x0600C9DD RID: 51677 RVA: 0x001203F8 File Offset: 0x0011E5F8
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17009A2C RID: 39468
			// (set) Token: 0x0600C9DE RID: 51678 RVA: 0x0012040B File Offset: 0x0011E60B
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17009A2D RID: 39469
			// (set) Token: 0x0600C9DF RID: 51679 RVA: 0x0012041E File Offset: 0x0011E61E
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17009A2E RID: 39470
			// (set) Token: 0x0600C9E0 RID: 51680 RVA: 0x00120431 File Offset: 0x0011E631
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17009A2F RID: 39471
			// (set) Token: 0x0600C9E1 RID: 51681 RVA: 0x00120444 File Offset: 0x0011E644
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17009A30 RID: 39472
			// (set) Token: 0x0600C9E2 RID: 51682 RVA: 0x00120457 File Offset: 0x0011E657
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17009A31 RID: 39473
			// (set) Token: 0x0600C9E3 RID: 51683 RVA: 0x0012046A File Offset: 0x0011E66A
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17009A32 RID: 39474
			// (set) Token: 0x0600C9E4 RID: 51684 RVA: 0x0012047D File Offset: 0x0011E67D
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17009A33 RID: 39475
			// (set) Token: 0x0600C9E5 RID: 51685 RVA: 0x00120490 File Offset: 0x0011E690
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17009A34 RID: 39476
			// (set) Token: 0x0600C9E6 RID: 51686 RVA: 0x001204A3 File Offset: 0x0011E6A3
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17009A35 RID: 39477
			// (set) Token: 0x0600C9E7 RID: 51687 RVA: 0x001204B6 File Offset: 0x0011E6B6
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17009A36 RID: 39478
			// (set) Token: 0x0600C9E8 RID: 51688 RVA: 0x001204C9 File Offset: 0x0011E6C9
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17009A37 RID: 39479
			// (set) Token: 0x0600C9E9 RID: 51689 RVA: 0x001204DC File Offset: 0x0011E6DC
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17009A38 RID: 39480
			// (set) Token: 0x0600C9EA RID: 51690 RVA: 0x001204EF File Offset: 0x0011E6EF
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17009A39 RID: 39481
			// (set) Token: 0x0600C9EB RID: 51691 RVA: 0x00120502 File Offset: 0x0011E702
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17009A3A RID: 39482
			// (set) Token: 0x0600C9EC RID: 51692 RVA: 0x00120515 File Offset: 0x0011E715
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17009A3B RID: 39483
			// (set) Token: 0x0600C9ED RID: 51693 RVA: 0x00120528 File Offset: 0x0011E728
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17009A3C RID: 39484
			// (set) Token: 0x0600C9EE RID: 51694 RVA: 0x0012053B File Offset: 0x0011E73B
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17009A3D RID: 39485
			// (set) Token: 0x0600C9EF RID: 51695 RVA: 0x0012054E File Offset: 0x0011E74E
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17009A3E RID: 39486
			// (set) Token: 0x0600C9F0 RID: 51696 RVA: 0x00120561 File Offset: 0x0011E761
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17009A3F RID: 39487
			// (set) Token: 0x0600C9F1 RID: 51697 RVA: 0x00120574 File Offset: 0x0011E774
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17009A40 RID: 39488
			// (set) Token: 0x0600C9F2 RID: 51698 RVA: 0x0012058C File Offset: 0x0011E78C
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17009A41 RID: 39489
			// (set) Token: 0x0600C9F3 RID: 51699 RVA: 0x0012059F File Offset: 0x0011E79F
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17009A42 RID: 39490
			// (set) Token: 0x0600C9F4 RID: 51700 RVA: 0x001205B2 File Offset: 0x0011E7B2
			public virtual RecipientDisplayType? RecipientDisplayType
			{
				set
				{
					base.PowerSharpParameters["RecipientDisplayType"] = value;
				}
			}

			// Token: 0x17009A43 RID: 39491
			// (set) Token: 0x0600C9F5 RID: 51701 RVA: 0x001205CA File Offset: 0x0011E7CA
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17009A44 RID: 39492
			// (set) Token: 0x0600C9F6 RID: 51702 RVA: 0x001205E2 File Offset: 0x0011E7E2
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17009A45 RID: 39493
			// (set) Token: 0x0600C9F7 RID: 51703 RVA: 0x001205FA File Offset: 0x0011E7FA
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17009A46 RID: 39494
			// (set) Token: 0x0600C9F8 RID: 51704 RVA: 0x0012060D File Offset: 0x0011E80D
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17009A47 RID: 39495
			// (set) Token: 0x0600C9F9 RID: 51705 RVA: 0x00120620 File Offset: 0x0011E820
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17009A48 RID: 39496
			// (set) Token: 0x0600C9FA RID: 51706 RVA: 0x00120633 File Offset: 0x0011E833
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17009A49 RID: 39497
			// (set) Token: 0x0600C9FB RID: 51707 RVA: 0x00120646 File Offset: 0x0011E846
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17009A4A RID: 39498
			// (set) Token: 0x0600C9FC RID: 51708 RVA: 0x00120659 File Offset: 0x0011E859
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17009A4B RID: 39499
			// (set) Token: 0x0600C9FD RID: 51709 RVA: 0x0012066C File Offset: 0x0011E86C
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17009A4C RID: 39500
			// (set) Token: 0x0600C9FE RID: 51710 RVA: 0x00120684 File Offset: 0x0011E884
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17009A4D RID: 39501
			// (set) Token: 0x0600C9FF RID: 51711 RVA: 0x00120697 File Offset: 0x0011E897
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17009A4E RID: 39502
			// (set) Token: 0x0600CA00 RID: 51712 RVA: 0x001206AA File Offset: 0x0011E8AA
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17009A4F RID: 39503
			// (set) Token: 0x0600CA01 RID: 51713 RVA: 0x001206BD File Offset: 0x0011E8BD
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17009A50 RID: 39504
			// (set) Token: 0x0600CA02 RID: 51714 RVA: 0x001206DB File Offset: 0x0011E8DB
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17009A51 RID: 39505
			// (set) Token: 0x0600CA03 RID: 51715 RVA: 0x001206EE File Offset: 0x0011E8EE
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17009A52 RID: 39506
			// (set) Token: 0x0600CA04 RID: 51716 RVA: 0x00120701 File Offset: 0x0011E901
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17009A53 RID: 39507
			// (set) Token: 0x0600CA05 RID: 51717 RVA: 0x00120714 File Offset: 0x0011E914
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17009A54 RID: 39508
			// (set) Token: 0x0600CA06 RID: 51718 RVA: 0x00120727 File Offset: 0x0011E927
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17009A55 RID: 39509
			// (set) Token: 0x0600CA07 RID: 51719 RVA: 0x0012073A File Offset: 0x0011E93A
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17009A56 RID: 39510
			// (set) Token: 0x0600CA08 RID: 51720 RVA: 0x0012074D File Offset: 0x0011E94D
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17009A57 RID: 39511
			// (set) Token: 0x0600CA09 RID: 51721 RVA: 0x00120760 File Offset: 0x0011E960
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17009A58 RID: 39512
			// (set) Token: 0x0600CA0A RID: 51722 RVA: 0x00120773 File Offset: 0x0011E973
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17009A59 RID: 39513
			// (set) Token: 0x0600CA0B RID: 51723 RVA: 0x00120786 File Offset: 0x0011E986
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17009A5A RID: 39514
			// (set) Token: 0x0600CA0C RID: 51724 RVA: 0x00120799 File Offset: 0x0011E999
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17009A5B RID: 39515
			// (set) Token: 0x0600CA0D RID: 51725 RVA: 0x001207AC File Offset: 0x0011E9AC
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17009A5C RID: 39516
			// (set) Token: 0x0600CA0E RID: 51726 RVA: 0x001207BF File Offset: 0x0011E9BF
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17009A5D RID: 39517
			// (set) Token: 0x0600CA0F RID: 51727 RVA: 0x001207D2 File Offset: 0x0011E9D2
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17009A5E RID: 39518
			// (set) Token: 0x0600CA10 RID: 51728 RVA: 0x001207E5 File Offset: 0x0011E9E5
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17009A5F RID: 39519
			// (set) Token: 0x0600CA11 RID: 51729 RVA: 0x001207F8 File Offset: 0x0011E9F8
			public virtual SecurityIdentifier MasterAccountSid
			{
				set
				{
					base.PowerSharpParameters["MasterAccountSid"] = value;
				}
			}

			// Token: 0x17009A60 RID: 39520
			// (set) Token: 0x0600CA12 RID: 51730 RVA: 0x0012080B File Offset: 0x0011EA0B
			public virtual ExchangeResourceType? ResourceType
			{
				set
				{
					base.PowerSharpParameters["ResourceType"] = value;
				}
			}

			// Token: 0x17009A61 RID: 39521
			// (set) Token: 0x0600CA13 RID: 51731 RVA: 0x00120823 File Offset: 0x0011EA23
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17009A62 RID: 39522
			// (set) Token: 0x0600CA14 RID: 51732 RVA: 0x0012083B File Offset: 0x0011EA3B
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17009A63 RID: 39523
			// (set) Token: 0x0600CA15 RID: 51733 RVA: 0x00120853 File Offset: 0x0011EA53
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17009A64 RID: 39524
			// (set) Token: 0x0600CA16 RID: 51734 RVA: 0x0012086B File Offset: 0x0011EA6B
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17009A65 RID: 39525
			// (set) Token: 0x0600CA17 RID: 51735 RVA: 0x0012087E File Offset: 0x0011EA7E
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17009A66 RID: 39526
			// (set) Token: 0x0600CA18 RID: 51736 RVA: 0x00120891 File Offset: 0x0011EA91
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17009A67 RID: 39527
			// (set) Token: 0x0600CA19 RID: 51737 RVA: 0x001208A9 File Offset: 0x0011EAA9
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17009A68 RID: 39528
			// (set) Token: 0x0600CA1A RID: 51738 RVA: 0x001208BC File Offset: 0x0011EABC
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17009A69 RID: 39529
			// (set) Token: 0x0600CA1B RID: 51739 RVA: 0x001208D4 File Offset: 0x0011EAD4
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17009A6A RID: 39530
			// (set) Token: 0x0600CA1C RID: 51740 RVA: 0x001208E7 File Offset: 0x0011EAE7
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17009A6B RID: 39531
			// (set) Token: 0x0600CA1D RID: 51741 RVA: 0x001208FA File Offset: 0x0011EAFA
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17009A6C RID: 39532
			// (set) Token: 0x0600CA1E RID: 51742 RVA: 0x0012090D File Offset: 0x0011EB0D
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x17009A6D RID: 39533
			// (set) Token: 0x0600CA1F RID: 51743 RVA: 0x00120920 File Offset: 0x0011EB20
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17009A6E RID: 39534
			// (set) Token: 0x0600CA20 RID: 51744 RVA: 0x00120933 File Offset: 0x0011EB33
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009A6F RID: 39535
			// (set) Token: 0x0600CA21 RID: 51745 RVA: 0x00120946 File Offset: 0x0011EB46
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009A70 RID: 39536
			// (set) Token: 0x0600CA22 RID: 51746 RVA: 0x00120959 File Offset: 0x0011EB59
			public virtual bool UsePreferMessageFormat
			{
				set
				{
					base.PowerSharpParameters["UsePreferMessageFormat"] = value;
				}
			}

			// Token: 0x17009A71 RID: 39537
			// (set) Token: 0x0600CA23 RID: 51747 RVA: 0x00120971 File Offset: 0x0011EB71
			public virtual MessageFormat MessageFormat
			{
				set
				{
					base.PowerSharpParameters["MessageFormat"] = value;
				}
			}

			// Token: 0x17009A72 RID: 39538
			// (set) Token: 0x0600CA24 RID: 51748 RVA: 0x00120989 File Offset: 0x0011EB89
			public virtual MessageBodyFormat MessageBodyFormat
			{
				set
				{
					base.PowerSharpParameters["MessageBodyFormat"] = value;
				}
			}

			// Token: 0x17009A73 RID: 39539
			// (set) Token: 0x0600CA25 RID: 51749 RVA: 0x001209A1 File Offset: 0x0011EBA1
			public virtual MacAttachmentFormat MacAttachmentFormat
			{
				set
				{
					base.PowerSharpParameters["MacAttachmentFormat"] = value;
				}
			}

			// Token: 0x17009A74 RID: 39540
			// (set) Token: 0x0600CA26 RID: 51750 RVA: 0x001209B9 File Offset: 0x0011EBB9
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17009A75 RID: 39541
			// (set) Token: 0x0600CA27 RID: 51751 RVA: 0x001209D7 File Offset: 0x0011EBD7
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17009A76 RID: 39542
			// (set) Token: 0x0600CA28 RID: 51752 RVA: 0x001209EA File Offset: 0x0011EBEA
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17009A77 RID: 39543
			// (set) Token: 0x0600CA29 RID: 51753 RVA: 0x00120A02 File Offset: 0x0011EC02
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17009A78 RID: 39544
			// (set) Token: 0x0600CA2A RID: 51754 RVA: 0x00120A1A File Offset: 0x0011EC1A
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17009A79 RID: 39545
			// (set) Token: 0x0600CA2B RID: 51755 RVA: 0x00120A32 File Offset: 0x0011EC32
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17009A7A RID: 39546
			// (set) Token: 0x0600CA2C RID: 51756 RVA: 0x00120A45 File Offset: 0x0011EC45
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17009A7B RID: 39547
			// (set) Token: 0x0600CA2D RID: 51757 RVA: 0x00120A5D File Offset: 0x0011EC5D
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17009A7C RID: 39548
			// (set) Token: 0x0600CA2E RID: 51758 RVA: 0x00120A70 File Offset: 0x0011EC70
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17009A7D RID: 39549
			// (set) Token: 0x0600CA2F RID: 51759 RVA: 0x00120A83 File Offset: 0x0011EC83
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17009A7E RID: 39550
			// (set) Token: 0x0600CA30 RID: 51760 RVA: 0x00120AA1 File Offset: 0x0011ECA1
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17009A7F RID: 39551
			// (set) Token: 0x0600CA31 RID: 51761 RVA: 0x00120AB4 File Offset: 0x0011ECB4
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009A80 RID: 39552
			// (set) Token: 0x0600CA32 RID: 51762 RVA: 0x00120AD2 File Offset: 0x0011ECD2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009A81 RID: 39553
			// (set) Token: 0x0600CA33 RID: 51763 RVA: 0x00120AE5 File Offset: 0x0011ECE5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009A82 RID: 39554
			// (set) Token: 0x0600CA34 RID: 51764 RVA: 0x00120AFD File Offset: 0x0011ECFD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009A83 RID: 39555
			// (set) Token: 0x0600CA35 RID: 51765 RVA: 0x00120B15 File Offset: 0x0011ED15
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009A84 RID: 39556
			// (set) Token: 0x0600CA36 RID: 51766 RVA: 0x00120B2D File Offset: 0x0011ED2D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009A85 RID: 39557
			// (set) Token: 0x0600CA37 RID: 51767 RVA: 0x00120B45 File Offset: 0x0011ED45
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
