using System;
using System.Management.Automation;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DB6 RID: 3510
	public class SetSyncMailContactCommand : SyntheticCommandWithPipelineInputNoOutput<SyncMailContact>
	{
		// Token: 0x0600CA39 RID: 51769 RVA: 0x00120B65 File Offset: 0x0011ED65
		private SetSyncMailContactCommand() : base("Set-SyncMailContact")
		{
		}

		// Token: 0x0600CA3A RID: 51770 RVA: 0x00120B72 File Offset: 0x0011ED72
		public SetSyncMailContactCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600CA3B RID: 51771 RVA: 0x00120B81 File Offset: 0x0011ED81
		public virtual SetSyncMailContactCommand SetParameters(SetSyncMailContactCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600CA3C RID: 51772 RVA: 0x00120B8B File Offset: 0x0011ED8B
		public virtual SetSyncMailContactCommand SetParameters(SetSyncMailContactCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DB7 RID: 3511
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17009A86 RID: 39558
			// (set) Token: 0x0600CA3D RID: 51773 RVA: 0x00120B95 File Offset: 0x0011ED95
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17009A87 RID: 39559
			// (set) Token: 0x0600CA3E RID: 51774 RVA: 0x00120BB3 File Offset: 0x0011EDB3
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17009A88 RID: 39560
			// (set) Token: 0x0600CA3F RID: 51775 RVA: 0x00120BC6 File Offset: 0x0011EDC6
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17009A89 RID: 39561
			// (set) Token: 0x0600CA40 RID: 51776 RVA: 0x00120BD9 File Offset: 0x0011EDD9
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawAcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["RawAcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17009A8A RID: 39562
			// (set) Token: 0x0600CA41 RID: 51777 RVA: 0x00120BEC File Offset: 0x0011EDEC
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawBypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["RawBypassModerationFrom"] = value;
				}
			}

			// Token: 0x17009A8B RID: 39563
			// (set) Token: 0x0600CA42 RID: 51778 RVA: 0x00120BFF File Offset: 0x0011EDFF
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawRejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RawRejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17009A8C RID: 39564
			// (set) Token: 0x0600CA43 RID: 51779 RVA: 0x00120C12 File Offset: 0x0011EE12
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> RawGrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["RawGrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17009A8D RID: 39565
			// (set) Token: 0x0600CA44 RID: 51780 RVA: 0x00120C25 File Offset: 0x0011EE25
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<ModeratorIDParameter>> RawModeratedBy
			{
				set
				{
					base.PowerSharpParameters["RawModeratedBy"] = value;
				}
			}

			// Token: 0x17009A8E RID: 39566
			// (set) Token: 0x0600CA45 RID: 51781 RVA: 0x00120C38 File Offset: 0x0011EE38
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17009A8F RID: 39567
			// (set) Token: 0x0600CA46 RID: 51782 RVA: 0x00120C4B File Offset: 0x0011EE4B
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17009A90 RID: 39568
			// (set) Token: 0x0600CA47 RID: 51783 RVA: 0x00120C5E File Offset: 0x0011EE5E
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17009A91 RID: 39569
			// (set) Token: 0x0600CA48 RID: 51784 RVA: 0x00120C76 File Offset: 0x0011EE76
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17009A92 RID: 39570
			// (set) Token: 0x0600CA49 RID: 51785 RVA: 0x00120C89 File Offset: 0x0011EE89
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17009A93 RID: 39571
			// (set) Token: 0x0600CA4A RID: 51786 RVA: 0x00120C9C File Offset: 0x0011EE9C
			public virtual string SecondaryAddress
			{
				set
				{
					base.PowerSharpParameters["SecondaryAddress"] = value;
				}
			}

			// Token: 0x17009A94 RID: 39572
			// (set) Token: 0x0600CA4B RID: 51787 RVA: 0x00120CAF File Offset: 0x0011EEAF
			public virtual string SecondaryDialPlan
			{
				set
				{
					base.PowerSharpParameters["SecondaryDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17009A95 RID: 39573
			// (set) Token: 0x0600CA4C RID: 51788 RVA: 0x00120CCD File Offset: 0x0011EECD
			public virtual SwitchParameter RemovePicture
			{
				set
				{
					base.PowerSharpParameters["RemovePicture"] = value;
				}
			}

			// Token: 0x17009A96 RID: 39574
			// (set) Token: 0x0600CA4D RID: 51789 RVA: 0x00120CE5 File Offset: 0x0011EEE5
			public virtual SwitchParameter RemoveSpokenName
			{
				set
				{
					base.PowerSharpParameters["RemoveSpokenName"] = value;
				}
			}

			// Token: 0x17009A97 RID: 39575
			// (set) Token: 0x0600CA4E RID: 51790 RVA: 0x00120CFD File Offset: 0x0011EEFD
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17009A98 RID: 39576
			// (set) Token: 0x0600CA4F RID: 51791 RVA: 0x00120D10 File Offset: 0x0011EF10
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17009A99 RID: 39577
			// (set) Token: 0x0600CA50 RID: 51792 RVA: 0x00120D23 File Offset: 0x0011EF23
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17009A9A RID: 39578
			// (set) Token: 0x0600CA51 RID: 51793 RVA: 0x00120D36 File Offset: 0x0011EF36
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17009A9B RID: 39579
			// (set) Token: 0x0600CA52 RID: 51794 RVA: 0x00120D54 File Offset: 0x0011EF54
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17009A9C RID: 39580
			// (set) Token: 0x0600CA53 RID: 51795 RVA: 0x00120D67 File Offset: 0x0011EF67
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17009A9D RID: 39581
			// (set) Token: 0x0600CA54 RID: 51796 RVA: 0x00120D7A File Offset: 0x0011EF7A
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17009A9E RID: 39582
			// (set) Token: 0x0600CA55 RID: 51797 RVA: 0x00120D8D File Offset: 0x0011EF8D
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17009A9F RID: 39583
			// (set) Token: 0x0600CA56 RID: 51798 RVA: 0x00120DA0 File Offset: 0x0011EFA0
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17009AA0 RID: 39584
			// (set) Token: 0x0600CA57 RID: 51799 RVA: 0x00120DB3 File Offset: 0x0011EFB3
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17009AA1 RID: 39585
			// (set) Token: 0x0600CA58 RID: 51800 RVA: 0x00120DC6 File Offset: 0x0011EFC6
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x17009AA2 RID: 39586
			// (set) Token: 0x0600CA59 RID: 51801 RVA: 0x00120DDE File Offset: 0x0011EFDE
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17009AA3 RID: 39587
			// (set) Token: 0x0600CA5A RID: 51802 RVA: 0x00120DF6 File Offset: 0x0011EFF6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009AA4 RID: 39588
			// (set) Token: 0x0600CA5B RID: 51803 RVA: 0x00120E09 File Offset: 0x0011F009
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17009AA5 RID: 39589
			// (set) Token: 0x0600CA5C RID: 51804 RVA: 0x00120E1C File Offset: 0x0011F01C
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17009AA6 RID: 39590
			// (set) Token: 0x0600CA5D RID: 51805 RVA: 0x00120E34 File Offset: 0x0011F034
			public virtual SecurityIdentifier MasterAccountSid
			{
				set
				{
					base.PowerSharpParameters["MasterAccountSid"] = value;
				}
			}

			// Token: 0x17009AA7 RID: 39591
			// (set) Token: 0x0600CA5E RID: 51806 RVA: 0x00120E47 File Offset: 0x0011F047
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17009AA8 RID: 39592
			// (set) Token: 0x0600CA5F RID: 51807 RVA: 0x00120E5A File Offset: 0x0011F05A
			public virtual RecipientDisplayType? RecipientDisplayType
			{
				set
				{
					base.PowerSharpParameters["RecipientDisplayType"] = value;
				}
			}

			// Token: 0x17009AA9 RID: 39593
			// (set) Token: 0x0600CA60 RID: 51808 RVA: 0x00120E72 File Offset: 0x0011F072
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17009AAA RID: 39594
			// (set) Token: 0x0600CA61 RID: 51809 RVA: 0x00120E8A File Offset: 0x0011F08A
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17009AAB RID: 39595
			// (set) Token: 0x0600CA62 RID: 51810 RVA: 0x00120EA2 File Offset: 0x0011F0A2
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17009AAC RID: 39596
			// (set) Token: 0x0600CA63 RID: 51811 RVA: 0x00120EB5 File Offset: 0x0011F0B5
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17009AAD RID: 39597
			// (set) Token: 0x0600CA64 RID: 51812 RVA: 0x00120EC8 File Offset: 0x0011F0C8
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17009AAE RID: 39598
			// (set) Token: 0x0600CA65 RID: 51813 RVA: 0x00120EDB File Offset: 0x0011F0DB
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17009AAF RID: 39599
			// (set) Token: 0x0600CA66 RID: 51814 RVA: 0x00120EEE File Offset: 0x0011F0EE
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17009AB0 RID: 39600
			// (set) Token: 0x0600CA67 RID: 51815 RVA: 0x00120F06 File Offset: 0x0011F106
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17009AB1 RID: 39601
			// (set) Token: 0x0600CA68 RID: 51816 RVA: 0x00120F19 File Offset: 0x0011F119
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17009AB2 RID: 39602
			// (set) Token: 0x0600CA69 RID: 51817 RVA: 0x00120F2C File Offset: 0x0011F12C
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17009AB3 RID: 39603
			// (set) Token: 0x0600CA6A RID: 51818 RVA: 0x00120F3F File Offset: 0x0011F13F
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17009AB4 RID: 39604
			// (set) Token: 0x0600CA6B RID: 51819 RVA: 0x00120F52 File Offset: 0x0011F152
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009AB5 RID: 39605
			// (set) Token: 0x0600CA6C RID: 51820 RVA: 0x00120F65 File Offset: 0x0011F165
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009AB6 RID: 39606
			// (set) Token: 0x0600CA6D RID: 51821 RVA: 0x00120F78 File Offset: 0x0011F178
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17009AB7 RID: 39607
			// (set) Token: 0x0600CA6E RID: 51822 RVA: 0x00120F8B File Offset: 0x0011F18B
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17009AB8 RID: 39608
			// (set) Token: 0x0600CA6F RID: 51823 RVA: 0x00120F9E File Offset: 0x0011F19E
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17009AB9 RID: 39609
			// (set) Token: 0x0600CA70 RID: 51824 RVA: 0x00120FB1 File Offset: 0x0011F1B1
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17009ABA RID: 39610
			// (set) Token: 0x0600CA71 RID: 51825 RVA: 0x00120FC4 File Offset: 0x0011F1C4
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17009ABB RID: 39611
			// (set) Token: 0x0600CA72 RID: 51826 RVA: 0x00120FD7 File Offset: 0x0011F1D7
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17009ABC RID: 39612
			// (set) Token: 0x0600CA73 RID: 51827 RVA: 0x00120FEA File Offset: 0x0011F1EA
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17009ABD RID: 39613
			// (set) Token: 0x0600CA74 RID: 51828 RVA: 0x00120FFD File Offset: 0x0011F1FD
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17009ABE RID: 39614
			// (set) Token: 0x0600CA75 RID: 51829 RVA: 0x00121010 File Offset: 0x0011F210
			public virtual ExchangeResourceType? ResourceType
			{
				set
				{
					base.PowerSharpParameters["ResourceType"] = value;
				}
			}

			// Token: 0x17009ABF RID: 39615
			// (set) Token: 0x0600CA76 RID: 51830 RVA: 0x00121028 File Offset: 0x0011F228
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17009AC0 RID: 39616
			// (set) Token: 0x0600CA77 RID: 51831 RVA: 0x00121040 File Offset: 0x0011F240
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17009AC1 RID: 39617
			// (set) Token: 0x0600CA78 RID: 51832 RVA: 0x00121053 File Offset: 0x0011F253
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17009AC2 RID: 39618
			// (set) Token: 0x0600CA79 RID: 51833 RVA: 0x00121066 File Offset: 0x0011F266
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17009AC3 RID: 39619
			// (set) Token: 0x0600CA7A RID: 51834 RVA: 0x00121079 File Offset: 0x0011F279
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17009AC4 RID: 39620
			// (set) Token: 0x0600CA7B RID: 51835 RVA: 0x0012108C File Offset: 0x0011F28C
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17009AC5 RID: 39621
			// (set) Token: 0x0600CA7C RID: 51836 RVA: 0x0012109F File Offset: 0x0011F29F
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17009AC6 RID: 39622
			// (set) Token: 0x0600CA7D RID: 51837 RVA: 0x001210B7 File Offset: 0x0011F2B7
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17009AC7 RID: 39623
			// (set) Token: 0x0600CA7E RID: 51838 RVA: 0x001210CA File Offset: 0x0011F2CA
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17009AC8 RID: 39624
			// (set) Token: 0x0600CA7F RID: 51839 RVA: 0x001210DD File Offset: 0x0011F2DD
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17009AC9 RID: 39625
			// (set) Token: 0x0600CA80 RID: 51840 RVA: 0x001210F5 File Offset: 0x0011F2F5
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17009ACA RID: 39626
			// (set) Token: 0x0600CA81 RID: 51841 RVA: 0x00121108 File Offset: 0x0011F308
			public virtual bool ExcludedFromBackSync
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromBackSync"] = value;
				}
			}

			// Token: 0x17009ACB RID: 39627
			// (set) Token: 0x0600CA82 RID: 51842 RVA: 0x00121120 File Offset: 0x0011F320
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x17009ACC RID: 39628
			// (set) Token: 0x0600CA83 RID: 51843 RVA: 0x00121133 File Offset: 0x0011F333
			public virtual Unlimited<int> MaxRecipientPerMessage
			{
				set
				{
					base.PowerSharpParameters["MaxRecipientPerMessage"] = value;
				}
			}

			// Token: 0x17009ACD RID: 39629
			// (set) Token: 0x0600CA84 RID: 51844 RVA: 0x0012114B File Offset: 0x0011F34B
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17009ACE RID: 39630
			// (set) Token: 0x0600CA85 RID: 51845 RVA: 0x00121163 File Offset: 0x0011F363
			public virtual bool UsePreferMessageFormat
			{
				set
				{
					base.PowerSharpParameters["UsePreferMessageFormat"] = value;
				}
			}

			// Token: 0x17009ACF RID: 39631
			// (set) Token: 0x0600CA86 RID: 51846 RVA: 0x0012117B File Offset: 0x0011F37B
			public virtual MessageFormat MessageFormat
			{
				set
				{
					base.PowerSharpParameters["MessageFormat"] = value;
				}
			}

			// Token: 0x17009AD0 RID: 39632
			// (set) Token: 0x0600CA87 RID: 51847 RVA: 0x00121193 File Offset: 0x0011F393
			public virtual MessageBodyFormat MessageBodyFormat
			{
				set
				{
					base.PowerSharpParameters["MessageBodyFormat"] = value;
				}
			}

			// Token: 0x17009AD1 RID: 39633
			// (set) Token: 0x0600CA88 RID: 51848 RVA: 0x001211AB File Offset: 0x0011F3AB
			public virtual MacAttachmentFormat MacAttachmentFormat
			{
				set
				{
					base.PowerSharpParameters["MacAttachmentFormat"] = value;
				}
			}

			// Token: 0x17009AD2 RID: 39634
			// (set) Token: 0x0600CA89 RID: 51849 RVA: 0x001211C3 File Offset: 0x0011F3C3
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17009AD3 RID: 39635
			// (set) Token: 0x0600CA8A RID: 51850 RVA: 0x001211D6 File Offset: 0x0011F3D6
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17009AD4 RID: 39636
			// (set) Token: 0x0600CA8B RID: 51851 RVA: 0x001211E9 File Offset: 0x0011F3E9
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17009AD5 RID: 39637
			// (set) Token: 0x0600CA8C RID: 51852 RVA: 0x001211FC File Offset: 0x0011F3FC
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17009AD6 RID: 39638
			// (set) Token: 0x0600CA8D RID: 51853 RVA: 0x0012120F File Offset: 0x0011F40F
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17009AD7 RID: 39639
			// (set) Token: 0x0600CA8E RID: 51854 RVA: 0x00121222 File Offset: 0x0011F422
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17009AD8 RID: 39640
			// (set) Token: 0x0600CA8F RID: 51855 RVA: 0x00121235 File Offset: 0x0011F435
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17009AD9 RID: 39641
			// (set) Token: 0x0600CA90 RID: 51856 RVA: 0x00121248 File Offset: 0x0011F448
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17009ADA RID: 39642
			// (set) Token: 0x0600CA91 RID: 51857 RVA: 0x0012125B File Offset: 0x0011F45B
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17009ADB RID: 39643
			// (set) Token: 0x0600CA92 RID: 51858 RVA: 0x0012126E File Offset: 0x0011F46E
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17009ADC RID: 39644
			// (set) Token: 0x0600CA93 RID: 51859 RVA: 0x00121281 File Offset: 0x0011F481
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17009ADD RID: 39645
			// (set) Token: 0x0600CA94 RID: 51860 RVA: 0x00121294 File Offset: 0x0011F494
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17009ADE RID: 39646
			// (set) Token: 0x0600CA95 RID: 51861 RVA: 0x001212A7 File Offset: 0x0011F4A7
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17009ADF RID: 39647
			// (set) Token: 0x0600CA96 RID: 51862 RVA: 0x001212BA File Offset: 0x0011F4BA
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17009AE0 RID: 39648
			// (set) Token: 0x0600CA97 RID: 51863 RVA: 0x001212CD File Offset: 0x0011F4CD
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17009AE1 RID: 39649
			// (set) Token: 0x0600CA98 RID: 51864 RVA: 0x001212E0 File Offset: 0x0011F4E0
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17009AE2 RID: 39650
			// (set) Token: 0x0600CA99 RID: 51865 RVA: 0x001212F3 File Offset: 0x0011F4F3
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17009AE3 RID: 39651
			// (set) Token: 0x0600CA9A RID: 51866 RVA: 0x00121306 File Offset: 0x0011F506
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17009AE4 RID: 39652
			// (set) Token: 0x0600CA9B RID: 51867 RVA: 0x00121319 File Offset: 0x0011F519
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17009AE5 RID: 39653
			// (set) Token: 0x0600CA9C RID: 51868 RVA: 0x0012132C File Offset: 0x0011F52C
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17009AE6 RID: 39654
			// (set) Token: 0x0600CA9D RID: 51869 RVA: 0x0012133F File Offset: 0x0011F53F
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17009AE7 RID: 39655
			// (set) Token: 0x0600CA9E RID: 51870 RVA: 0x00121352 File Offset: 0x0011F552
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17009AE8 RID: 39656
			// (set) Token: 0x0600CA9F RID: 51871 RVA: 0x00121365 File Offset: 0x0011F565
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17009AE9 RID: 39657
			// (set) Token: 0x0600CAA0 RID: 51872 RVA: 0x00121378 File Offset: 0x0011F578
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17009AEA RID: 39658
			// (set) Token: 0x0600CAA1 RID: 51873 RVA: 0x0012138B File Offset: 0x0011F58B
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17009AEB RID: 39659
			// (set) Token: 0x0600CAA2 RID: 51874 RVA: 0x0012139E File Offset: 0x0011F59E
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17009AEC RID: 39660
			// (set) Token: 0x0600CAA3 RID: 51875 RVA: 0x001213B6 File Offset: 0x0011F5B6
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x17009AED RID: 39661
			// (set) Token: 0x0600CAA4 RID: 51876 RVA: 0x001213CE File Offset: 0x0011F5CE
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x17009AEE RID: 39662
			// (set) Token: 0x0600CAA5 RID: 51877 RVA: 0x001213E6 File Offset: 0x0011F5E6
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17009AEF RID: 39663
			// (set) Token: 0x0600CAA6 RID: 51878 RVA: 0x001213FE File Offset: 0x0011F5FE
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x17009AF0 RID: 39664
			// (set) Token: 0x0600CAA7 RID: 51879 RVA: 0x00121416 File Offset: 0x0011F616
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17009AF1 RID: 39665
			// (set) Token: 0x0600CAA8 RID: 51880 RVA: 0x0012142E File Offset: 0x0011F62E
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17009AF2 RID: 39666
			// (set) Token: 0x0600CAA9 RID: 51881 RVA: 0x00121446 File Offset: 0x0011F646
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x17009AF3 RID: 39667
			// (set) Token: 0x0600CAAA RID: 51882 RVA: 0x00121459 File Offset: 0x0011F659
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17009AF4 RID: 39668
			// (set) Token: 0x0600CAAB RID: 51883 RVA: 0x00121471 File Offset: 0x0011F671
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x17009AF5 RID: 39669
			// (set) Token: 0x0600CAAC RID: 51884 RVA: 0x00121484 File Offset: 0x0011F684
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x17009AF6 RID: 39670
			// (set) Token: 0x0600CAAD RID: 51885 RVA: 0x0012149C File Offset: 0x0011F69C
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x17009AF7 RID: 39671
			// (set) Token: 0x0600CAAE RID: 51886 RVA: 0x001214AF File Offset: 0x0011F6AF
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17009AF8 RID: 39672
			// (set) Token: 0x0600CAAF RID: 51887 RVA: 0x001214C2 File Offset: 0x0011F6C2
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17009AF9 RID: 39673
			// (set) Token: 0x0600CAB0 RID: 51888 RVA: 0x001214D5 File Offset: 0x0011F6D5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009AFA RID: 39674
			// (set) Token: 0x0600CAB1 RID: 51889 RVA: 0x001214ED File Offset: 0x0011F6ED
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009AFB RID: 39675
			// (set) Token: 0x0600CAB2 RID: 51890 RVA: 0x00121505 File Offset: 0x0011F705
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009AFC RID: 39676
			// (set) Token: 0x0600CAB3 RID: 51891 RVA: 0x0012151D File Offset: 0x0011F71D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009AFD RID: 39677
			// (set) Token: 0x0600CAB4 RID: 51892 RVA: 0x00121535 File Offset: 0x0011F735
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DB8 RID: 3512
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17009AFE RID: 39678
			// (set) Token: 0x0600CAB6 RID: 51894 RVA: 0x00121555 File Offset: 0x0011F755
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailContactIdParameter(value) : null);
				}
			}

			// Token: 0x17009AFF RID: 39679
			// (set) Token: 0x0600CAB7 RID: 51895 RVA: 0x00121573 File Offset: 0x0011F773
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17009B00 RID: 39680
			// (set) Token: 0x0600CAB8 RID: 51896 RVA: 0x00121591 File Offset: 0x0011F791
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17009B01 RID: 39681
			// (set) Token: 0x0600CAB9 RID: 51897 RVA: 0x001215A4 File Offset: 0x0011F7A4
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17009B02 RID: 39682
			// (set) Token: 0x0600CABA RID: 51898 RVA: 0x001215B7 File Offset: 0x0011F7B7
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawAcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["RawAcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17009B03 RID: 39683
			// (set) Token: 0x0600CABB RID: 51899 RVA: 0x001215CA File Offset: 0x0011F7CA
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawBypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["RawBypassModerationFrom"] = value;
				}
			}

			// Token: 0x17009B04 RID: 39684
			// (set) Token: 0x0600CABC RID: 51900 RVA: 0x001215DD File Offset: 0x0011F7DD
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawRejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RawRejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17009B05 RID: 39685
			// (set) Token: 0x0600CABD RID: 51901 RVA: 0x001215F0 File Offset: 0x0011F7F0
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> RawGrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["RawGrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17009B06 RID: 39686
			// (set) Token: 0x0600CABE RID: 51902 RVA: 0x00121603 File Offset: 0x0011F803
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<ModeratorIDParameter>> RawModeratedBy
			{
				set
				{
					base.PowerSharpParameters["RawModeratedBy"] = value;
				}
			}

			// Token: 0x17009B07 RID: 39687
			// (set) Token: 0x0600CABF RID: 51903 RVA: 0x00121616 File Offset: 0x0011F816
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17009B08 RID: 39688
			// (set) Token: 0x0600CAC0 RID: 51904 RVA: 0x00121629 File Offset: 0x0011F829
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17009B09 RID: 39689
			// (set) Token: 0x0600CAC1 RID: 51905 RVA: 0x0012163C File Offset: 0x0011F83C
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17009B0A RID: 39690
			// (set) Token: 0x0600CAC2 RID: 51906 RVA: 0x00121654 File Offset: 0x0011F854
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17009B0B RID: 39691
			// (set) Token: 0x0600CAC3 RID: 51907 RVA: 0x00121667 File Offset: 0x0011F867
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17009B0C RID: 39692
			// (set) Token: 0x0600CAC4 RID: 51908 RVA: 0x0012167A File Offset: 0x0011F87A
			public virtual string SecondaryAddress
			{
				set
				{
					base.PowerSharpParameters["SecondaryAddress"] = value;
				}
			}

			// Token: 0x17009B0D RID: 39693
			// (set) Token: 0x0600CAC5 RID: 51909 RVA: 0x0012168D File Offset: 0x0011F88D
			public virtual string SecondaryDialPlan
			{
				set
				{
					base.PowerSharpParameters["SecondaryDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17009B0E RID: 39694
			// (set) Token: 0x0600CAC6 RID: 51910 RVA: 0x001216AB File Offset: 0x0011F8AB
			public virtual SwitchParameter RemovePicture
			{
				set
				{
					base.PowerSharpParameters["RemovePicture"] = value;
				}
			}

			// Token: 0x17009B0F RID: 39695
			// (set) Token: 0x0600CAC7 RID: 51911 RVA: 0x001216C3 File Offset: 0x0011F8C3
			public virtual SwitchParameter RemoveSpokenName
			{
				set
				{
					base.PowerSharpParameters["RemoveSpokenName"] = value;
				}
			}

			// Token: 0x17009B10 RID: 39696
			// (set) Token: 0x0600CAC8 RID: 51912 RVA: 0x001216DB File Offset: 0x0011F8DB
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17009B11 RID: 39697
			// (set) Token: 0x0600CAC9 RID: 51913 RVA: 0x001216EE File Offset: 0x0011F8EE
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17009B12 RID: 39698
			// (set) Token: 0x0600CACA RID: 51914 RVA: 0x00121701 File Offset: 0x0011F901
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17009B13 RID: 39699
			// (set) Token: 0x0600CACB RID: 51915 RVA: 0x00121714 File Offset: 0x0011F914
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17009B14 RID: 39700
			// (set) Token: 0x0600CACC RID: 51916 RVA: 0x00121732 File Offset: 0x0011F932
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17009B15 RID: 39701
			// (set) Token: 0x0600CACD RID: 51917 RVA: 0x00121745 File Offset: 0x0011F945
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17009B16 RID: 39702
			// (set) Token: 0x0600CACE RID: 51918 RVA: 0x00121758 File Offset: 0x0011F958
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17009B17 RID: 39703
			// (set) Token: 0x0600CACF RID: 51919 RVA: 0x0012176B File Offset: 0x0011F96B
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17009B18 RID: 39704
			// (set) Token: 0x0600CAD0 RID: 51920 RVA: 0x0012177E File Offset: 0x0011F97E
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17009B19 RID: 39705
			// (set) Token: 0x0600CAD1 RID: 51921 RVA: 0x00121791 File Offset: 0x0011F991
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17009B1A RID: 39706
			// (set) Token: 0x0600CAD2 RID: 51922 RVA: 0x001217A4 File Offset: 0x0011F9A4
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x17009B1B RID: 39707
			// (set) Token: 0x0600CAD3 RID: 51923 RVA: 0x001217BC File Offset: 0x0011F9BC
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17009B1C RID: 39708
			// (set) Token: 0x0600CAD4 RID: 51924 RVA: 0x001217D4 File Offset: 0x0011F9D4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009B1D RID: 39709
			// (set) Token: 0x0600CAD5 RID: 51925 RVA: 0x001217E7 File Offset: 0x0011F9E7
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17009B1E RID: 39710
			// (set) Token: 0x0600CAD6 RID: 51926 RVA: 0x001217FA File Offset: 0x0011F9FA
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17009B1F RID: 39711
			// (set) Token: 0x0600CAD7 RID: 51927 RVA: 0x00121812 File Offset: 0x0011FA12
			public virtual SecurityIdentifier MasterAccountSid
			{
				set
				{
					base.PowerSharpParameters["MasterAccountSid"] = value;
				}
			}

			// Token: 0x17009B20 RID: 39712
			// (set) Token: 0x0600CAD8 RID: 51928 RVA: 0x00121825 File Offset: 0x0011FA25
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17009B21 RID: 39713
			// (set) Token: 0x0600CAD9 RID: 51929 RVA: 0x00121838 File Offset: 0x0011FA38
			public virtual RecipientDisplayType? RecipientDisplayType
			{
				set
				{
					base.PowerSharpParameters["RecipientDisplayType"] = value;
				}
			}

			// Token: 0x17009B22 RID: 39714
			// (set) Token: 0x0600CADA RID: 51930 RVA: 0x00121850 File Offset: 0x0011FA50
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17009B23 RID: 39715
			// (set) Token: 0x0600CADB RID: 51931 RVA: 0x00121868 File Offset: 0x0011FA68
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17009B24 RID: 39716
			// (set) Token: 0x0600CADC RID: 51932 RVA: 0x00121880 File Offset: 0x0011FA80
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17009B25 RID: 39717
			// (set) Token: 0x0600CADD RID: 51933 RVA: 0x00121893 File Offset: 0x0011FA93
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17009B26 RID: 39718
			// (set) Token: 0x0600CADE RID: 51934 RVA: 0x001218A6 File Offset: 0x0011FAA6
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17009B27 RID: 39719
			// (set) Token: 0x0600CADF RID: 51935 RVA: 0x001218B9 File Offset: 0x0011FAB9
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17009B28 RID: 39720
			// (set) Token: 0x0600CAE0 RID: 51936 RVA: 0x001218CC File Offset: 0x0011FACC
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17009B29 RID: 39721
			// (set) Token: 0x0600CAE1 RID: 51937 RVA: 0x001218E4 File Offset: 0x0011FAE4
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17009B2A RID: 39722
			// (set) Token: 0x0600CAE2 RID: 51938 RVA: 0x001218F7 File Offset: 0x0011FAF7
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17009B2B RID: 39723
			// (set) Token: 0x0600CAE3 RID: 51939 RVA: 0x0012190A File Offset: 0x0011FB0A
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17009B2C RID: 39724
			// (set) Token: 0x0600CAE4 RID: 51940 RVA: 0x0012191D File Offset: 0x0011FB1D
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17009B2D RID: 39725
			// (set) Token: 0x0600CAE5 RID: 51941 RVA: 0x00121930 File Offset: 0x0011FB30
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009B2E RID: 39726
			// (set) Token: 0x0600CAE6 RID: 51942 RVA: 0x00121943 File Offset: 0x0011FB43
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009B2F RID: 39727
			// (set) Token: 0x0600CAE7 RID: 51943 RVA: 0x00121956 File Offset: 0x0011FB56
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17009B30 RID: 39728
			// (set) Token: 0x0600CAE8 RID: 51944 RVA: 0x00121969 File Offset: 0x0011FB69
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17009B31 RID: 39729
			// (set) Token: 0x0600CAE9 RID: 51945 RVA: 0x0012197C File Offset: 0x0011FB7C
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17009B32 RID: 39730
			// (set) Token: 0x0600CAEA RID: 51946 RVA: 0x0012198F File Offset: 0x0011FB8F
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17009B33 RID: 39731
			// (set) Token: 0x0600CAEB RID: 51947 RVA: 0x001219A2 File Offset: 0x0011FBA2
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17009B34 RID: 39732
			// (set) Token: 0x0600CAEC RID: 51948 RVA: 0x001219B5 File Offset: 0x0011FBB5
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17009B35 RID: 39733
			// (set) Token: 0x0600CAED RID: 51949 RVA: 0x001219C8 File Offset: 0x0011FBC8
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17009B36 RID: 39734
			// (set) Token: 0x0600CAEE RID: 51950 RVA: 0x001219DB File Offset: 0x0011FBDB
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17009B37 RID: 39735
			// (set) Token: 0x0600CAEF RID: 51951 RVA: 0x001219EE File Offset: 0x0011FBEE
			public virtual ExchangeResourceType? ResourceType
			{
				set
				{
					base.PowerSharpParameters["ResourceType"] = value;
				}
			}

			// Token: 0x17009B38 RID: 39736
			// (set) Token: 0x0600CAF0 RID: 51952 RVA: 0x00121A06 File Offset: 0x0011FC06
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17009B39 RID: 39737
			// (set) Token: 0x0600CAF1 RID: 51953 RVA: 0x00121A1E File Offset: 0x0011FC1E
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17009B3A RID: 39738
			// (set) Token: 0x0600CAF2 RID: 51954 RVA: 0x00121A31 File Offset: 0x0011FC31
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17009B3B RID: 39739
			// (set) Token: 0x0600CAF3 RID: 51955 RVA: 0x00121A44 File Offset: 0x0011FC44
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17009B3C RID: 39740
			// (set) Token: 0x0600CAF4 RID: 51956 RVA: 0x00121A57 File Offset: 0x0011FC57
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17009B3D RID: 39741
			// (set) Token: 0x0600CAF5 RID: 51957 RVA: 0x00121A6A File Offset: 0x0011FC6A
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17009B3E RID: 39742
			// (set) Token: 0x0600CAF6 RID: 51958 RVA: 0x00121A7D File Offset: 0x0011FC7D
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17009B3F RID: 39743
			// (set) Token: 0x0600CAF7 RID: 51959 RVA: 0x00121A95 File Offset: 0x0011FC95
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17009B40 RID: 39744
			// (set) Token: 0x0600CAF8 RID: 51960 RVA: 0x00121AA8 File Offset: 0x0011FCA8
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17009B41 RID: 39745
			// (set) Token: 0x0600CAF9 RID: 51961 RVA: 0x00121ABB File Offset: 0x0011FCBB
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17009B42 RID: 39746
			// (set) Token: 0x0600CAFA RID: 51962 RVA: 0x00121AD3 File Offset: 0x0011FCD3
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17009B43 RID: 39747
			// (set) Token: 0x0600CAFB RID: 51963 RVA: 0x00121AE6 File Offset: 0x0011FCE6
			public virtual bool ExcludedFromBackSync
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromBackSync"] = value;
				}
			}

			// Token: 0x17009B44 RID: 39748
			// (set) Token: 0x0600CAFC RID: 51964 RVA: 0x00121AFE File Offset: 0x0011FCFE
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x17009B45 RID: 39749
			// (set) Token: 0x0600CAFD RID: 51965 RVA: 0x00121B11 File Offset: 0x0011FD11
			public virtual Unlimited<int> MaxRecipientPerMessage
			{
				set
				{
					base.PowerSharpParameters["MaxRecipientPerMessage"] = value;
				}
			}

			// Token: 0x17009B46 RID: 39750
			// (set) Token: 0x0600CAFE RID: 51966 RVA: 0x00121B29 File Offset: 0x0011FD29
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17009B47 RID: 39751
			// (set) Token: 0x0600CAFF RID: 51967 RVA: 0x00121B41 File Offset: 0x0011FD41
			public virtual bool UsePreferMessageFormat
			{
				set
				{
					base.PowerSharpParameters["UsePreferMessageFormat"] = value;
				}
			}

			// Token: 0x17009B48 RID: 39752
			// (set) Token: 0x0600CB00 RID: 51968 RVA: 0x00121B59 File Offset: 0x0011FD59
			public virtual MessageFormat MessageFormat
			{
				set
				{
					base.PowerSharpParameters["MessageFormat"] = value;
				}
			}

			// Token: 0x17009B49 RID: 39753
			// (set) Token: 0x0600CB01 RID: 51969 RVA: 0x00121B71 File Offset: 0x0011FD71
			public virtual MessageBodyFormat MessageBodyFormat
			{
				set
				{
					base.PowerSharpParameters["MessageBodyFormat"] = value;
				}
			}

			// Token: 0x17009B4A RID: 39754
			// (set) Token: 0x0600CB02 RID: 51970 RVA: 0x00121B89 File Offset: 0x0011FD89
			public virtual MacAttachmentFormat MacAttachmentFormat
			{
				set
				{
					base.PowerSharpParameters["MacAttachmentFormat"] = value;
				}
			}

			// Token: 0x17009B4B RID: 39755
			// (set) Token: 0x0600CB03 RID: 51971 RVA: 0x00121BA1 File Offset: 0x0011FDA1
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17009B4C RID: 39756
			// (set) Token: 0x0600CB04 RID: 51972 RVA: 0x00121BB4 File Offset: 0x0011FDB4
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17009B4D RID: 39757
			// (set) Token: 0x0600CB05 RID: 51973 RVA: 0x00121BC7 File Offset: 0x0011FDC7
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17009B4E RID: 39758
			// (set) Token: 0x0600CB06 RID: 51974 RVA: 0x00121BDA File Offset: 0x0011FDDA
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17009B4F RID: 39759
			// (set) Token: 0x0600CB07 RID: 51975 RVA: 0x00121BED File Offset: 0x0011FDED
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17009B50 RID: 39760
			// (set) Token: 0x0600CB08 RID: 51976 RVA: 0x00121C00 File Offset: 0x0011FE00
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17009B51 RID: 39761
			// (set) Token: 0x0600CB09 RID: 51977 RVA: 0x00121C13 File Offset: 0x0011FE13
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17009B52 RID: 39762
			// (set) Token: 0x0600CB0A RID: 51978 RVA: 0x00121C26 File Offset: 0x0011FE26
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17009B53 RID: 39763
			// (set) Token: 0x0600CB0B RID: 51979 RVA: 0x00121C39 File Offset: 0x0011FE39
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17009B54 RID: 39764
			// (set) Token: 0x0600CB0C RID: 51980 RVA: 0x00121C4C File Offset: 0x0011FE4C
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17009B55 RID: 39765
			// (set) Token: 0x0600CB0D RID: 51981 RVA: 0x00121C5F File Offset: 0x0011FE5F
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17009B56 RID: 39766
			// (set) Token: 0x0600CB0E RID: 51982 RVA: 0x00121C72 File Offset: 0x0011FE72
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17009B57 RID: 39767
			// (set) Token: 0x0600CB0F RID: 51983 RVA: 0x00121C85 File Offset: 0x0011FE85
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17009B58 RID: 39768
			// (set) Token: 0x0600CB10 RID: 51984 RVA: 0x00121C98 File Offset: 0x0011FE98
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17009B59 RID: 39769
			// (set) Token: 0x0600CB11 RID: 51985 RVA: 0x00121CAB File Offset: 0x0011FEAB
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17009B5A RID: 39770
			// (set) Token: 0x0600CB12 RID: 51986 RVA: 0x00121CBE File Offset: 0x0011FEBE
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17009B5B RID: 39771
			// (set) Token: 0x0600CB13 RID: 51987 RVA: 0x00121CD1 File Offset: 0x0011FED1
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17009B5C RID: 39772
			// (set) Token: 0x0600CB14 RID: 51988 RVA: 0x00121CE4 File Offset: 0x0011FEE4
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17009B5D RID: 39773
			// (set) Token: 0x0600CB15 RID: 51989 RVA: 0x00121CF7 File Offset: 0x0011FEF7
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17009B5E RID: 39774
			// (set) Token: 0x0600CB16 RID: 51990 RVA: 0x00121D0A File Offset: 0x0011FF0A
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17009B5F RID: 39775
			// (set) Token: 0x0600CB17 RID: 51991 RVA: 0x00121D1D File Offset: 0x0011FF1D
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17009B60 RID: 39776
			// (set) Token: 0x0600CB18 RID: 51992 RVA: 0x00121D30 File Offset: 0x0011FF30
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17009B61 RID: 39777
			// (set) Token: 0x0600CB19 RID: 51993 RVA: 0x00121D43 File Offset: 0x0011FF43
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17009B62 RID: 39778
			// (set) Token: 0x0600CB1A RID: 51994 RVA: 0x00121D56 File Offset: 0x0011FF56
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17009B63 RID: 39779
			// (set) Token: 0x0600CB1B RID: 51995 RVA: 0x00121D69 File Offset: 0x0011FF69
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17009B64 RID: 39780
			// (set) Token: 0x0600CB1C RID: 51996 RVA: 0x00121D7C File Offset: 0x0011FF7C
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17009B65 RID: 39781
			// (set) Token: 0x0600CB1D RID: 51997 RVA: 0x00121D94 File Offset: 0x0011FF94
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x17009B66 RID: 39782
			// (set) Token: 0x0600CB1E RID: 51998 RVA: 0x00121DAC File Offset: 0x0011FFAC
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x17009B67 RID: 39783
			// (set) Token: 0x0600CB1F RID: 51999 RVA: 0x00121DC4 File Offset: 0x0011FFC4
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17009B68 RID: 39784
			// (set) Token: 0x0600CB20 RID: 52000 RVA: 0x00121DDC File Offset: 0x0011FFDC
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x17009B69 RID: 39785
			// (set) Token: 0x0600CB21 RID: 52001 RVA: 0x00121DF4 File Offset: 0x0011FFF4
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17009B6A RID: 39786
			// (set) Token: 0x0600CB22 RID: 52002 RVA: 0x00121E0C File Offset: 0x0012000C
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17009B6B RID: 39787
			// (set) Token: 0x0600CB23 RID: 52003 RVA: 0x00121E24 File Offset: 0x00120024
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x17009B6C RID: 39788
			// (set) Token: 0x0600CB24 RID: 52004 RVA: 0x00121E37 File Offset: 0x00120037
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17009B6D RID: 39789
			// (set) Token: 0x0600CB25 RID: 52005 RVA: 0x00121E4F File Offset: 0x0012004F
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x17009B6E RID: 39790
			// (set) Token: 0x0600CB26 RID: 52006 RVA: 0x00121E62 File Offset: 0x00120062
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x17009B6F RID: 39791
			// (set) Token: 0x0600CB27 RID: 52007 RVA: 0x00121E7A File Offset: 0x0012007A
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x17009B70 RID: 39792
			// (set) Token: 0x0600CB28 RID: 52008 RVA: 0x00121E8D File Offset: 0x0012008D
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17009B71 RID: 39793
			// (set) Token: 0x0600CB29 RID: 52009 RVA: 0x00121EA0 File Offset: 0x001200A0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17009B72 RID: 39794
			// (set) Token: 0x0600CB2A RID: 52010 RVA: 0x00121EB3 File Offset: 0x001200B3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009B73 RID: 39795
			// (set) Token: 0x0600CB2B RID: 52011 RVA: 0x00121ECB File Offset: 0x001200CB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009B74 RID: 39796
			// (set) Token: 0x0600CB2C RID: 52012 RVA: 0x00121EE3 File Offset: 0x001200E3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009B75 RID: 39797
			// (set) Token: 0x0600CB2D RID: 52013 RVA: 0x00121EFB File Offset: 0x001200FB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009B76 RID: 39798
			// (set) Token: 0x0600CB2E RID: 52014 RVA: 0x00121F13 File Offset: 0x00120113
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
