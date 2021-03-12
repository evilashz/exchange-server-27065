using System;
using System.Globalization;
using System.Management.Automation;
using System.Security;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DC1 RID: 3521
	public class NewSyncMailUserCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x0600CB89 RID: 52105 RVA: 0x00122682 File Offset: 0x00120882
		private NewSyncMailUserCommand() : base("New-SyncMailUser")
		{
		}

		// Token: 0x0600CB8A RID: 52106 RVA: 0x0012268F File Offset: 0x0012088F
		public NewSyncMailUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600CB8B RID: 52107 RVA: 0x0012269E File Offset: 0x0012089E
		public virtual NewSyncMailUserCommand SetParameters(NewSyncMailUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600CB8C RID: 52108 RVA: 0x001226A8 File Offset: 0x001208A8
		public virtual NewSyncMailUserCommand SetParameters(NewSyncMailUserCommand.EnabledUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600CB8D RID: 52109 RVA: 0x001226B2 File Offset: 0x001208B2
		public virtual NewSyncMailUserCommand SetParameters(NewSyncMailUserCommand.MicrosoftOnlineServicesIDParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600CB8E RID: 52110 RVA: 0x001226BC File Offset: 0x001208BC
		public virtual NewSyncMailUserCommand SetParameters(NewSyncMailUserCommand.WindowsLiveIDParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600CB8F RID: 52111 RVA: 0x001226C6 File Offset: 0x001208C6
		public virtual NewSyncMailUserCommand SetParameters(NewSyncMailUserCommand.FederatedUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600CB90 RID: 52112 RVA: 0x001226D0 File Offset: 0x001208D0
		public virtual NewSyncMailUserCommand SetParameters(NewSyncMailUserCommand.WindowsLiveCustomDomainsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600CB91 RID: 52113 RVA: 0x001226DA File Offset: 0x001208DA
		public virtual NewSyncMailUserCommand SetParameters(NewSyncMailUserCommand.ImportLiveIdParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600CB92 RID: 52114 RVA: 0x001226E4 File Offset: 0x001208E4
		public virtual NewSyncMailUserCommand SetParameters(NewSyncMailUserCommand.DisabledUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600CB93 RID: 52115 RVA: 0x001226EE File Offset: 0x001208EE
		public virtual NewSyncMailUserCommand SetParameters(NewSyncMailUserCommand.EnableRoomMailboxAccountParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600CB94 RID: 52116 RVA: 0x001226F8 File Offset: 0x001208F8
		public virtual NewSyncMailUserCommand SetParameters(NewSyncMailUserCommand.MicrosoftOnlineServicesFederatedUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DC2 RID: 3522
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17009BC0 RID: 39872
			// (set) Token: 0x0600CB95 RID: 52117 RVA: 0x00122702 File Offset: 0x00120902
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17009BC1 RID: 39873
			// (set) Token: 0x0600CB96 RID: 52118 RVA: 0x00122715 File Offset: 0x00120915
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17009BC2 RID: 39874
			// (set) Token: 0x0600CB97 RID: 52119 RVA: 0x00122728 File Offset: 0x00120928
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17009BC3 RID: 39875
			// (set) Token: 0x0600CB98 RID: 52120 RVA: 0x0012273B File Offset: 0x0012093B
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17009BC4 RID: 39876
			// (set) Token: 0x0600CB99 RID: 52121 RVA: 0x0012274E File Offset: 0x0012094E
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17009BC5 RID: 39877
			// (set) Token: 0x0600CB9A RID: 52122 RVA: 0x00122761 File Offset: 0x00120961
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17009BC6 RID: 39878
			// (set) Token: 0x0600CB9B RID: 52123 RVA: 0x00122774 File Offset: 0x00120974
			public virtual Guid ExchangeGuid
			{
				set
				{
					base.PowerSharpParameters["ExchangeGuid"] = value;
				}
			}

			// Token: 0x17009BC7 RID: 39879
			// (set) Token: 0x0600CB9C RID: 52124 RVA: 0x0012278C File Offset: 0x0012098C
			public virtual string ForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17009BC8 RID: 39880
			// (set) Token: 0x0600CB9D RID: 52125 RVA: 0x001227AA File Offset: 0x001209AA
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17009BC9 RID: 39881
			// (set) Token: 0x0600CB9E RID: 52126 RVA: 0x001227C2 File Offset: 0x001209C2
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17009BCA RID: 39882
			// (set) Token: 0x0600CB9F RID: 52127 RVA: 0x001227D5 File Offset: 0x001209D5
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17009BCB RID: 39883
			// (set) Token: 0x0600CBA0 RID: 52128 RVA: 0x001227E8 File Offset: 0x001209E8
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17009BCC RID: 39884
			// (set) Token: 0x0600CBA1 RID: 52129 RVA: 0x00122800 File Offset: 0x00120A00
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17009BCD RID: 39885
			// (set) Token: 0x0600CBA2 RID: 52130 RVA: 0x00122813 File Offset: 0x00120A13
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17009BCE RID: 39886
			// (set) Token: 0x0600CBA3 RID: 52131 RVA: 0x00122826 File Offset: 0x00120A26
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17009BCF RID: 39887
			// (set) Token: 0x0600CBA4 RID: 52132 RVA: 0x00122839 File Offset: 0x00120A39
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17009BD0 RID: 39888
			// (set) Token: 0x0600CBA5 RID: 52133 RVA: 0x0012284C File Offset: 0x00120A4C
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17009BD1 RID: 39889
			// (set) Token: 0x0600CBA6 RID: 52134 RVA: 0x0012285F File Offset: 0x00120A5F
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17009BD2 RID: 39890
			// (set) Token: 0x0600CBA7 RID: 52135 RVA: 0x00122872 File Offset: 0x00120A72
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17009BD3 RID: 39891
			// (set) Token: 0x0600CBA8 RID: 52136 RVA: 0x00122885 File Offset: 0x00120A85
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17009BD4 RID: 39892
			// (set) Token: 0x0600CBA9 RID: 52137 RVA: 0x00122898 File Offset: 0x00120A98
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17009BD5 RID: 39893
			// (set) Token: 0x0600CBAA RID: 52138 RVA: 0x001228AB File Offset: 0x00120AAB
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17009BD6 RID: 39894
			// (set) Token: 0x0600CBAB RID: 52139 RVA: 0x001228BE File Offset: 0x00120ABE
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17009BD7 RID: 39895
			// (set) Token: 0x0600CBAC RID: 52140 RVA: 0x001228D1 File Offset: 0x00120AD1
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17009BD8 RID: 39896
			// (set) Token: 0x0600CBAD RID: 52141 RVA: 0x001228E4 File Offset: 0x00120AE4
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17009BD9 RID: 39897
			// (set) Token: 0x0600CBAE RID: 52142 RVA: 0x001228F7 File Offset: 0x00120AF7
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17009BDA RID: 39898
			// (set) Token: 0x0600CBAF RID: 52143 RVA: 0x0012290A File Offset: 0x00120B0A
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17009BDB RID: 39899
			// (set) Token: 0x0600CBB0 RID: 52144 RVA: 0x0012291D File Offset: 0x00120B1D
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17009BDC RID: 39900
			// (set) Token: 0x0600CBB1 RID: 52145 RVA: 0x00122930 File Offset: 0x00120B30
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17009BDD RID: 39901
			// (set) Token: 0x0600CBB2 RID: 52146 RVA: 0x00122943 File Offset: 0x00120B43
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17009BDE RID: 39902
			// (set) Token: 0x0600CBB3 RID: 52147 RVA: 0x00122956 File Offset: 0x00120B56
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17009BDF RID: 39903
			// (set) Token: 0x0600CBB4 RID: 52148 RVA: 0x00122969 File Offset: 0x00120B69
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17009BE0 RID: 39904
			// (set) Token: 0x0600CBB5 RID: 52149 RVA: 0x0012297C File Offset: 0x00120B7C
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17009BE1 RID: 39905
			// (set) Token: 0x0600CBB6 RID: 52150 RVA: 0x0012298F File Offset: 0x00120B8F
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17009BE2 RID: 39906
			// (set) Token: 0x0600CBB7 RID: 52151 RVA: 0x001229A7 File Offset: 0x00120BA7
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17009BE3 RID: 39907
			// (set) Token: 0x0600CBB8 RID: 52152 RVA: 0x001229BA File Offset: 0x00120BBA
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17009BE4 RID: 39908
			// (set) Token: 0x0600CBB9 RID: 52153 RVA: 0x001229CD File Offset: 0x00120BCD
			public virtual RecipientDisplayType? RecipientDisplayType
			{
				set
				{
					base.PowerSharpParameters["RecipientDisplayType"] = value;
				}
			}

			// Token: 0x17009BE5 RID: 39909
			// (set) Token: 0x0600CBBA RID: 52154 RVA: 0x001229E5 File Offset: 0x00120BE5
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17009BE6 RID: 39910
			// (set) Token: 0x0600CBBB RID: 52155 RVA: 0x001229FD File Offset: 0x00120BFD
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17009BE7 RID: 39911
			// (set) Token: 0x0600CBBC RID: 52156 RVA: 0x00122A15 File Offset: 0x00120C15
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17009BE8 RID: 39912
			// (set) Token: 0x0600CBBD RID: 52157 RVA: 0x00122A2D File Offset: 0x00120C2D
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17009BE9 RID: 39913
			// (set) Token: 0x0600CBBE RID: 52158 RVA: 0x00122A45 File Offset: 0x00120C45
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17009BEA RID: 39914
			// (set) Token: 0x0600CBBF RID: 52159 RVA: 0x00122A58 File Offset: 0x00120C58
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17009BEB RID: 39915
			// (set) Token: 0x0600CBC0 RID: 52160 RVA: 0x00122A6B File Offset: 0x00120C6B
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17009BEC RID: 39916
			// (set) Token: 0x0600CBC1 RID: 52161 RVA: 0x00122A7E File Offset: 0x00120C7E
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17009BED RID: 39917
			// (set) Token: 0x0600CBC2 RID: 52162 RVA: 0x00122A91 File Offset: 0x00120C91
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17009BEE RID: 39918
			// (set) Token: 0x0600CBC3 RID: 52163 RVA: 0x00122AA4 File Offset: 0x00120CA4
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17009BEF RID: 39919
			// (set) Token: 0x0600CBC4 RID: 52164 RVA: 0x00122AB7 File Offset: 0x00120CB7
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17009BF0 RID: 39920
			// (set) Token: 0x0600CBC5 RID: 52165 RVA: 0x00122ACF File Offset: 0x00120CCF
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17009BF1 RID: 39921
			// (set) Token: 0x0600CBC6 RID: 52166 RVA: 0x00122AE2 File Offset: 0x00120CE2
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17009BF2 RID: 39922
			// (set) Token: 0x0600CBC7 RID: 52167 RVA: 0x00122AF5 File Offset: 0x00120CF5
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17009BF3 RID: 39923
			// (set) Token: 0x0600CBC8 RID: 52168 RVA: 0x00122B08 File Offset: 0x00120D08
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17009BF4 RID: 39924
			// (set) Token: 0x0600CBC9 RID: 52169 RVA: 0x00122B1B File Offset: 0x00120D1B
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17009BF5 RID: 39925
			// (set) Token: 0x0600CBCA RID: 52170 RVA: 0x00122B2E File Offset: 0x00120D2E
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17009BF6 RID: 39926
			// (set) Token: 0x0600CBCB RID: 52171 RVA: 0x00122B4C File Offset: 0x00120D4C
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17009BF7 RID: 39927
			// (set) Token: 0x0600CBCC RID: 52172 RVA: 0x00122B5F File Offset: 0x00120D5F
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17009BF8 RID: 39928
			// (set) Token: 0x0600CBCD RID: 52173 RVA: 0x00122B72 File Offset: 0x00120D72
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17009BF9 RID: 39929
			// (set) Token: 0x0600CBCE RID: 52174 RVA: 0x00122B85 File Offset: 0x00120D85
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17009BFA RID: 39930
			// (set) Token: 0x0600CBCF RID: 52175 RVA: 0x00122B98 File Offset: 0x00120D98
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17009BFB RID: 39931
			// (set) Token: 0x0600CBD0 RID: 52176 RVA: 0x00122BAB File Offset: 0x00120DAB
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17009BFC RID: 39932
			// (set) Token: 0x0600CBD1 RID: 52177 RVA: 0x00122BBE File Offset: 0x00120DBE
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17009BFD RID: 39933
			// (set) Token: 0x0600CBD2 RID: 52178 RVA: 0x00122BD1 File Offset: 0x00120DD1
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17009BFE RID: 39934
			// (set) Token: 0x0600CBD3 RID: 52179 RVA: 0x00122BE4 File Offset: 0x00120DE4
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17009BFF RID: 39935
			// (set) Token: 0x0600CBD4 RID: 52180 RVA: 0x00122BF7 File Offset: 0x00120DF7
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17009C00 RID: 39936
			// (set) Token: 0x0600CBD5 RID: 52181 RVA: 0x00122C0A File Offset: 0x00120E0A
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17009C01 RID: 39937
			// (set) Token: 0x0600CBD6 RID: 52182 RVA: 0x00122C1D File Offset: 0x00120E1D
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17009C02 RID: 39938
			// (set) Token: 0x0600CBD7 RID: 52183 RVA: 0x00122C30 File Offset: 0x00120E30
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17009C03 RID: 39939
			// (set) Token: 0x0600CBD8 RID: 52184 RVA: 0x00122C43 File Offset: 0x00120E43
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17009C04 RID: 39940
			// (set) Token: 0x0600CBD9 RID: 52185 RVA: 0x00122C5B File Offset: 0x00120E5B
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17009C05 RID: 39941
			// (set) Token: 0x0600CBDA RID: 52186 RVA: 0x00122C6E File Offset: 0x00120E6E
			public virtual SecurityIdentifier MasterAccountSid
			{
				set
				{
					base.PowerSharpParameters["MasterAccountSid"] = value;
				}
			}

			// Token: 0x17009C06 RID: 39942
			// (set) Token: 0x0600CBDB RID: 52187 RVA: 0x00122C81 File Offset: 0x00120E81
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17009C07 RID: 39943
			// (set) Token: 0x0600CBDC RID: 52188 RVA: 0x00122C99 File Offset: 0x00120E99
			public virtual string IntendedMailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["IntendedMailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17009C08 RID: 39944
			// (set) Token: 0x0600CBDD RID: 52189 RVA: 0x00122CB7 File Offset: 0x00120EB7
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17009C09 RID: 39945
			// (set) Token: 0x0600CBDE RID: 52190 RVA: 0x00122CCF File Offset: 0x00120ECF
			public virtual ExchangeResourceType? ResourceType
			{
				set
				{
					base.PowerSharpParameters["ResourceType"] = value;
				}
			}

			// Token: 0x17009C0A RID: 39946
			// (set) Token: 0x0600CBDF RID: 52191 RVA: 0x00122CE7 File Offset: 0x00120EE7
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17009C0B RID: 39947
			// (set) Token: 0x0600CBE0 RID: 52192 RVA: 0x00122CFF File Offset: 0x00120EFF
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17009C0C RID: 39948
			// (set) Token: 0x0600CBE1 RID: 52193 RVA: 0x00122D12 File Offset: 0x00120F12
			public virtual MultiValuedProperty<string> ResourceMetaData
			{
				set
				{
					base.PowerSharpParameters["ResourceMetaData"] = value;
				}
			}

			// Token: 0x17009C0D RID: 39949
			// (set) Token: 0x0600CBE2 RID: 52194 RVA: 0x00122D25 File Offset: 0x00120F25
			public virtual string ResourcePropertiesDisplay
			{
				set
				{
					base.PowerSharpParameters["ResourcePropertiesDisplay"] = value;
				}
			}

			// Token: 0x17009C0E RID: 39950
			// (set) Token: 0x0600CBE3 RID: 52195 RVA: 0x00122D38 File Offset: 0x00120F38
			public virtual MultiValuedProperty<string> ResourceSearchProperties
			{
				set
				{
					base.PowerSharpParameters["ResourceSearchProperties"] = value;
				}
			}

			// Token: 0x17009C0F RID: 39951
			// (set) Token: 0x0600CBE4 RID: 52196 RVA: 0x00122D4B File Offset: 0x00120F4B
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17009C10 RID: 39952
			// (set) Token: 0x0600CBE5 RID: 52197 RVA: 0x00122D63 File Offset: 0x00120F63
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17009C11 RID: 39953
			// (set) Token: 0x0600CBE6 RID: 52198 RVA: 0x00122D76 File Offset: 0x00120F76
			public virtual bool IsCalculatedTargetAddress
			{
				set
				{
					base.PowerSharpParameters["IsCalculatedTargetAddress"] = value;
				}
			}

			// Token: 0x17009C12 RID: 39954
			// (set) Token: 0x0600CBE7 RID: 52199 RVA: 0x00122D8E File Offset: 0x00120F8E
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17009C13 RID: 39955
			// (set) Token: 0x0600CBE8 RID: 52200 RVA: 0x00122DA1 File Offset: 0x00120FA1
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17009C14 RID: 39956
			// (set) Token: 0x0600CBE9 RID: 52201 RVA: 0x00122DB9 File Offset: 0x00120FB9
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17009C15 RID: 39957
			// (set) Token: 0x0600CBEA RID: 52202 RVA: 0x00122DCC File Offset: 0x00120FCC
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17009C16 RID: 39958
			// (set) Token: 0x0600CBEB RID: 52203 RVA: 0x00122DDF File Offset: 0x00120FDF
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17009C17 RID: 39959
			// (set) Token: 0x0600CBEC RID: 52204 RVA: 0x00122DF2 File Offset: 0x00120FF2
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17009C18 RID: 39960
			// (set) Token: 0x0600CBED RID: 52205 RVA: 0x00122E0A File Offset: 0x0012100A
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17009C19 RID: 39961
			// (set) Token: 0x0600CBEE RID: 52206 RVA: 0x00122E22 File Offset: 0x00121022
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17009C1A RID: 39962
			// (set) Token: 0x0600CBEF RID: 52207 RVA: 0x00122E35 File Offset: 0x00121035
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17009C1B RID: 39963
			// (set) Token: 0x0600CBF0 RID: 52208 RVA: 0x00122E48 File Offset: 0x00121048
			public virtual ElcMailboxFlags ElcMailboxFlags
			{
				set
				{
					base.PowerSharpParameters["ElcMailboxFlags"] = value;
				}
			}

			// Token: 0x17009C1C RID: 39964
			// (set) Token: 0x0600CBF1 RID: 52209 RVA: 0x00122E60 File Offset: 0x00121060
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17009C1D RID: 39965
			// (set) Token: 0x0600CBF2 RID: 52210 RVA: 0x00122E78 File Offset: 0x00121078
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17009C1E RID: 39966
			// (set) Token: 0x0600CBF3 RID: 52211 RVA: 0x00122E90 File Offset: 0x00121090
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x17009C1F RID: 39967
			// (set) Token: 0x0600CBF4 RID: 52212 RVA: 0x00122EA3 File Offset: 0x001210A3
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x17009C20 RID: 39968
			// (set) Token: 0x0600CBF5 RID: 52213 RVA: 0x00122EB6 File Offset: 0x001210B6
			public virtual bool MailboxAuditEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditEnabled"] = value;
				}
			}

			// Token: 0x17009C21 RID: 39969
			// (set) Token: 0x0600CBF6 RID: 52214 RVA: 0x00122ECE File Offset: 0x001210CE
			public virtual EnhancedTimeSpan MailboxAuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x17009C22 RID: 39970
			// (set) Token: 0x0600CBF7 RID: 52215 RVA: 0x00122EE6 File Offset: 0x001210E6
			public virtual MailboxAuditOperations AuditAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditAdminOperations"] = value;
				}
			}

			// Token: 0x17009C23 RID: 39971
			// (set) Token: 0x0600CBF8 RID: 52216 RVA: 0x00122EFE File Offset: 0x001210FE
			public virtual MailboxAuditOperations AuditDelegateAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateAdminOperations"] = value;
				}
			}

			// Token: 0x17009C24 RID: 39972
			// (set) Token: 0x0600CBF9 RID: 52217 RVA: 0x00122F16 File Offset: 0x00121116
			public virtual MailboxAuditOperations AuditDelegateOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateOperations"] = value;
				}
			}

			// Token: 0x17009C25 RID: 39973
			// (set) Token: 0x0600CBFA RID: 52218 RVA: 0x00122F2E File Offset: 0x0012112E
			public virtual MailboxAuditOperations AuditOwnerOperations
			{
				set
				{
					base.PowerSharpParameters["AuditOwnerOperations"] = value;
				}
			}

			// Token: 0x17009C26 RID: 39974
			// (set) Token: 0x0600CBFB RID: 52219 RVA: 0x00122F46 File Offset: 0x00121146
			public virtual bool BypassAudit
			{
				set
				{
					base.PowerSharpParameters["BypassAudit"] = value;
				}
			}

			// Token: 0x17009C27 RID: 39975
			// (set) Token: 0x0600CBFC RID: 52220 RVA: 0x00122F5E File Offset: 0x0012115E
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x17009C28 RID: 39976
			// (set) Token: 0x0600CBFD RID: 52221 RVA: 0x00122F71 File Offset: 0x00121171
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x17009C29 RID: 39977
			// (set) Token: 0x0600CBFE RID: 52222 RVA: 0x00122F89 File Offset: 0x00121189
			public virtual RecipientIdParameter SiteMailboxOwners
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxOwners"] = value;
				}
			}

			// Token: 0x17009C2A RID: 39978
			// (set) Token: 0x0600CBFF RID: 52223 RVA: 0x00122F9C File Offset: 0x0012119C
			public virtual RecipientIdParameter SiteMailboxUsers
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxUsers"] = value;
				}
			}

			// Token: 0x17009C2B RID: 39979
			// (set) Token: 0x0600CC00 RID: 52224 RVA: 0x00122FAF File Offset: 0x001211AF
			public virtual DateTime? SiteMailboxClosedTime
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxClosedTime"] = value;
				}
			}

			// Token: 0x17009C2C RID: 39980
			// (set) Token: 0x0600CC01 RID: 52225 RVA: 0x00122FC7 File Offset: 0x001211C7
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x17009C2D RID: 39981
			// (set) Token: 0x0600CC02 RID: 52226 RVA: 0x00122FDA File Offset: 0x001211DA
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17009C2E RID: 39982
			// (set) Token: 0x0600CC03 RID: 52227 RVA: 0x00122FF2 File Offset: 0x001211F2
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17009C2F RID: 39983
			// (set) Token: 0x0600CC04 RID: 52228 RVA: 0x0012300A File Offset: 0x0012120A
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17009C30 RID: 39984
			// (set) Token: 0x0600CC05 RID: 52229 RVA: 0x00123022 File Offset: 0x00121222
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17009C31 RID: 39985
			// (set) Token: 0x0600CC06 RID: 52230 RVA: 0x00123035 File Offset: 0x00121235
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17009C32 RID: 39986
			// (set) Token: 0x0600CC07 RID: 52231 RVA: 0x00123048 File Offset: 0x00121248
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17009C33 RID: 39987
			// (set) Token: 0x0600CC08 RID: 52232 RVA: 0x0012305B File Offset: 0x0012125B
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009C34 RID: 39988
			// (set) Token: 0x0600CC09 RID: 52233 RVA: 0x0012306E File Offset: 0x0012126E
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009C35 RID: 39989
			// (set) Token: 0x0600CC0A RID: 52234 RVA: 0x00123081 File Offset: 0x00121281
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17009C36 RID: 39990
			// (set) Token: 0x0600CC0B RID: 52235 RVA: 0x00123094 File Offset: 0x00121294
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17009C37 RID: 39991
			// (set) Token: 0x0600CC0C RID: 52236 RVA: 0x001230AC File Offset: 0x001212AC
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17009C38 RID: 39992
			// (set) Token: 0x0600CC0D RID: 52237 RVA: 0x001230BF File Offset: 0x001212BF
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17009C39 RID: 39993
			// (set) Token: 0x0600CC0E RID: 52238 RVA: 0x001230D7 File Offset: 0x001212D7
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17009C3A RID: 39994
			// (set) Token: 0x0600CC0F RID: 52239 RVA: 0x001230EA File Offset: 0x001212EA
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17009C3B RID: 39995
			// (set) Token: 0x0600CC10 RID: 52240 RVA: 0x00123102 File Offset: 0x00121302
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17009C3C RID: 39996
			// (set) Token: 0x0600CC11 RID: 52241 RVA: 0x00123115 File Offset: 0x00121315
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17009C3D RID: 39997
			// (set) Token: 0x0600CC12 RID: 52242 RVA: 0x00123133 File Offset: 0x00121333
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17009C3E RID: 39998
			// (set) Token: 0x0600CC13 RID: 52243 RVA: 0x00123146 File Offset: 0x00121346
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17009C3F RID: 39999
			// (set) Token: 0x0600CC14 RID: 52244 RVA: 0x0012315E File Offset: 0x0012135E
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17009C40 RID: 40000
			// (set) Token: 0x0600CC15 RID: 52245 RVA: 0x00123176 File Offset: 0x00121376
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17009C41 RID: 40001
			// (set) Token: 0x0600CC16 RID: 52246 RVA: 0x0012318E File Offset: 0x0012138E
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17009C42 RID: 40002
			// (set) Token: 0x0600CC17 RID: 52247 RVA: 0x001231A1 File Offset: 0x001213A1
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17009C43 RID: 40003
			// (set) Token: 0x0600CC18 RID: 52248 RVA: 0x001231B9 File Offset: 0x001213B9
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17009C44 RID: 40004
			// (set) Token: 0x0600CC19 RID: 52249 RVA: 0x001231CC File Offset: 0x001213CC
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17009C45 RID: 40005
			// (set) Token: 0x0600CC1A RID: 52250 RVA: 0x001231DF File Offset: 0x001213DF
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17009C46 RID: 40006
			// (set) Token: 0x0600CC1B RID: 52251 RVA: 0x001231FD File Offset: 0x001213FD
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17009C47 RID: 40007
			// (set) Token: 0x0600CC1C RID: 52252 RVA: 0x00123210 File Offset: 0x00121410
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009C48 RID: 40008
			// (set) Token: 0x0600CC1D RID: 52253 RVA: 0x0012322E File Offset: 0x0012142E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009C49 RID: 40009
			// (set) Token: 0x0600CC1E RID: 52254 RVA: 0x00123241 File Offset: 0x00121441
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009C4A RID: 40010
			// (set) Token: 0x0600CC1F RID: 52255 RVA: 0x00123259 File Offset: 0x00121459
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009C4B RID: 40011
			// (set) Token: 0x0600CC20 RID: 52256 RVA: 0x00123271 File Offset: 0x00121471
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009C4C RID: 40012
			// (set) Token: 0x0600CC21 RID: 52257 RVA: 0x00123289 File Offset: 0x00121489
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009C4D RID: 40013
			// (set) Token: 0x0600CC22 RID: 52258 RVA: 0x001232A1 File Offset: 0x001214A1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DC3 RID: 3523
		public class EnabledUserParameters : ParametersBase
		{
			// Token: 0x17009C4E RID: 40014
			// (set) Token: 0x0600CC24 RID: 52260 RVA: 0x001232C1 File Offset: 0x001214C1
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17009C4F RID: 40015
			// (set) Token: 0x0600CC25 RID: 52261 RVA: 0x001232D4 File Offset: 0x001214D4
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17009C50 RID: 40016
			// (set) Token: 0x0600CC26 RID: 52262 RVA: 0x001232E7 File Offset: 0x001214E7
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x17009C51 RID: 40017
			// (set) Token: 0x0600CC27 RID: 52263 RVA: 0x001232FA File Offset: 0x001214FA
			public virtual bool UsePreferMessageFormat
			{
				set
				{
					base.PowerSharpParameters["UsePreferMessageFormat"] = value;
				}
			}

			// Token: 0x17009C52 RID: 40018
			// (set) Token: 0x0600CC28 RID: 52264 RVA: 0x00123312 File Offset: 0x00121512
			public virtual MessageFormat MessageFormat
			{
				set
				{
					base.PowerSharpParameters["MessageFormat"] = value;
				}
			}

			// Token: 0x17009C53 RID: 40019
			// (set) Token: 0x0600CC29 RID: 52265 RVA: 0x0012332A File Offset: 0x0012152A
			public virtual MessageBodyFormat MessageBodyFormat
			{
				set
				{
					base.PowerSharpParameters["MessageBodyFormat"] = value;
				}
			}

			// Token: 0x17009C54 RID: 40020
			// (set) Token: 0x0600CC2A RID: 52266 RVA: 0x00123342 File Offset: 0x00121542
			public virtual MacAttachmentFormat MacAttachmentFormat
			{
				set
				{
					base.PowerSharpParameters["MacAttachmentFormat"] = value;
				}
			}

			// Token: 0x17009C55 RID: 40021
			// (set) Token: 0x0600CC2B RID: 52267 RVA: 0x0012335A File Offset: 0x0012155A
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17009C56 RID: 40022
			// (set) Token: 0x0600CC2C RID: 52268 RVA: 0x0012336D File Offset: 0x0012156D
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17009C57 RID: 40023
			// (set) Token: 0x0600CC2D RID: 52269 RVA: 0x00123380 File Offset: 0x00121580
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17009C58 RID: 40024
			// (set) Token: 0x0600CC2E RID: 52270 RVA: 0x00123393 File Offset: 0x00121593
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17009C59 RID: 40025
			// (set) Token: 0x0600CC2F RID: 52271 RVA: 0x001233A6 File Offset: 0x001215A6
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17009C5A RID: 40026
			// (set) Token: 0x0600CC30 RID: 52272 RVA: 0x001233B9 File Offset: 0x001215B9
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17009C5B RID: 40027
			// (set) Token: 0x0600CC31 RID: 52273 RVA: 0x001233CC File Offset: 0x001215CC
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17009C5C RID: 40028
			// (set) Token: 0x0600CC32 RID: 52274 RVA: 0x001233DF File Offset: 0x001215DF
			public virtual Guid ExchangeGuid
			{
				set
				{
					base.PowerSharpParameters["ExchangeGuid"] = value;
				}
			}

			// Token: 0x17009C5D RID: 40029
			// (set) Token: 0x0600CC33 RID: 52275 RVA: 0x001233F7 File Offset: 0x001215F7
			public virtual string ForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17009C5E RID: 40030
			// (set) Token: 0x0600CC34 RID: 52276 RVA: 0x00123415 File Offset: 0x00121615
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17009C5F RID: 40031
			// (set) Token: 0x0600CC35 RID: 52277 RVA: 0x0012342D File Offset: 0x0012162D
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17009C60 RID: 40032
			// (set) Token: 0x0600CC36 RID: 52278 RVA: 0x00123440 File Offset: 0x00121640
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17009C61 RID: 40033
			// (set) Token: 0x0600CC37 RID: 52279 RVA: 0x00123453 File Offset: 0x00121653
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17009C62 RID: 40034
			// (set) Token: 0x0600CC38 RID: 52280 RVA: 0x0012346B File Offset: 0x0012166B
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17009C63 RID: 40035
			// (set) Token: 0x0600CC39 RID: 52281 RVA: 0x0012347E File Offset: 0x0012167E
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17009C64 RID: 40036
			// (set) Token: 0x0600CC3A RID: 52282 RVA: 0x00123491 File Offset: 0x00121691
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17009C65 RID: 40037
			// (set) Token: 0x0600CC3B RID: 52283 RVA: 0x001234A4 File Offset: 0x001216A4
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17009C66 RID: 40038
			// (set) Token: 0x0600CC3C RID: 52284 RVA: 0x001234B7 File Offset: 0x001216B7
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17009C67 RID: 40039
			// (set) Token: 0x0600CC3D RID: 52285 RVA: 0x001234CA File Offset: 0x001216CA
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17009C68 RID: 40040
			// (set) Token: 0x0600CC3E RID: 52286 RVA: 0x001234DD File Offset: 0x001216DD
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17009C69 RID: 40041
			// (set) Token: 0x0600CC3F RID: 52287 RVA: 0x001234F0 File Offset: 0x001216F0
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17009C6A RID: 40042
			// (set) Token: 0x0600CC40 RID: 52288 RVA: 0x00123503 File Offset: 0x00121703
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17009C6B RID: 40043
			// (set) Token: 0x0600CC41 RID: 52289 RVA: 0x00123516 File Offset: 0x00121716
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17009C6C RID: 40044
			// (set) Token: 0x0600CC42 RID: 52290 RVA: 0x00123529 File Offset: 0x00121729
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17009C6D RID: 40045
			// (set) Token: 0x0600CC43 RID: 52291 RVA: 0x0012353C File Offset: 0x0012173C
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17009C6E RID: 40046
			// (set) Token: 0x0600CC44 RID: 52292 RVA: 0x0012354F File Offset: 0x0012174F
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17009C6F RID: 40047
			// (set) Token: 0x0600CC45 RID: 52293 RVA: 0x00123562 File Offset: 0x00121762
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17009C70 RID: 40048
			// (set) Token: 0x0600CC46 RID: 52294 RVA: 0x00123575 File Offset: 0x00121775
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17009C71 RID: 40049
			// (set) Token: 0x0600CC47 RID: 52295 RVA: 0x00123588 File Offset: 0x00121788
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17009C72 RID: 40050
			// (set) Token: 0x0600CC48 RID: 52296 RVA: 0x0012359B File Offset: 0x0012179B
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17009C73 RID: 40051
			// (set) Token: 0x0600CC49 RID: 52297 RVA: 0x001235AE File Offset: 0x001217AE
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17009C74 RID: 40052
			// (set) Token: 0x0600CC4A RID: 52298 RVA: 0x001235C1 File Offset: 0x001217C1
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17009C75 RID: 40053
			// (set) Token: 0x0600CC4B RID: 52299 RVA: 0x001235D4 File Offset: 0x001217D4
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17009C76 RID: 40054
			// (set) Token: 0x0600CC4C RID: 52300 RVA: 0x001235E7 File Offset: 0x001217E7
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17009C77 RID: 40055
			// (set) Token: 0x0600CC4D RID: 52301 RVA: 0x001235FA File Offset: 0x001217FA
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17009C78 RID: 40056
			// (set) Token: 0x0600CC4E RID: 52302 RVA: 0x00123612 File Offset: 0x00121812
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17009C79 RID: 40057
			// (set) Token: 0x0600CC4F RID: 52303 RVA: 0x00123625 File Offset: 0x00121825
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17009C7A RID: 40058
			// (set) Token: 0x0600CC50 RID: 52304 RVA: 0x00123638 File Offset: 0x00121838
			public virtual RecipientDisplayType? RecipientDisplayType
			{
				set
				{
					base.PowerSharpParameters["RecipientDisplayType"] = value;
				}
			}

			// Token: 0x17009C7B RID: 40059
			// (set) Token: 0x0600CC51 RID: 52305 RVA: 0x00123650 File Offset: 0x00121850
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17009C7C RID: 40060
			// (set) Token: 0x0600CC52 RID: 52306 RVA: 0x00123668 File Offset: 0x00121868
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17009C7D RID: 40061
			// (set) Token: 0x0600CC53 RID: 52307 RVA: 0x00123680 File Offset: 0x00121880
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17009C7E RID: 40062
			// (set) Token: 0x0600CC54 RID: 52308 RVA: 0x00123698 File Offset: 0x00121898
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17009C7F RID: 40063
			// (set) Token: 0x0600CC55 RID: 52309 RVA: 0x001236B0 File Offset: 0x001218B0
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17009C80 RID: 40064
			// (set) Token: 0x0600CC56 RID: 52310 RVA: 0x001236C3 File Offset: 0x001218C3
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17009C81 RID: 40065
			// (set) Token: 0x0600CC57 RID: 52311 RVA: 0x001236D6 File Offset: 0x001218D6
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17009C82 RID: 40066
			// (set) Token: 0x0600CC58 RID: 52312 RVA: 0x001236E9 File Offset: 0x001218E9
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17009C83 RID: 40067
			// (set) Token: 0x0600CC59 RID: 52313 RVA: 0x001236FC File Offset: 0x001218FC
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17009C84 RID: 40068
			// (set) Token: 0x0600CC5A RID: 52314 RVA: 0x0012370F File Offset: 0x0012190F
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17009C85 RID: 40069
			// (set) Token: 0x0600CC5B RID: 52315 RVA: 0x00123722 File Offset: 0x00121922
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17009C86 RID: 40070
			// (set) Token: 0x0600CC5C RID: 52316 RVA: 0x0012373A File Offset: 0x0012193A
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17009C87 RID: 40071
			// (set) Token: 0x0600CC5D RID: 52317 RVA: 0x0012374D File Offset: 0x0012194D
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17009C88 RID: 40072
			// (set) Token: 0x0600CC5E RID: 52318 RVA: 0x00123760 File Offset: 0x00121960
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17009C89 RID: 40073
			// (set) Token: 0x0600CC5F RID: 52319 RVA: 0x00123773 File Offset: 0x00121973
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17009C8A RID: 40074
			// (set) Token: 0x0600CC60 RID: 52320 RVA: 0x00123786 File Offset: 0x00121986
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17009C8B RID: 40075
			// (set) Token: 0x0600CC61 RID: 52321 RVA: 0x00123799 File Offset: 0x00121999
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17009C8C RID: 40076
			// (set) Token: 0x0600CC62 RID: 52322 RVA: 0x001237B7 File Offset: 0x001219B7
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17009C8D RID: 40077
			// (set) Token: 0x0600CC63 RID: 52323 RVA: 0x001237CA File Offset: 0x001219CA
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17009C8E RID: 40078
			// (set) Token: 0x0600CC64 RID: 52324 RVA: 0x001237DD File Offset: 0x001219DD
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17009C8F RID: 40079
			// (set) Token: 0x0600CC65 RID: 52325 RVA: 0x001237F0 File Offset: 0x001219F0
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17009C90 RID: 40080
			// (set) Token: 0x0600CC66 RID: 52326 RVA: 0x00123803 File Offset: 0x00121A03
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17009C91 RID: 40081
			// (set) Token: 0x0600CC67 RID: 52327 RVA: 0x00123816 File Offset: 0x00121A16
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17009C92 RID: 40082
			// (set) Token: 0x0600CC68 RID: 52328 RVA: 0x00123829 File Offset: 0x00121A29
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17009C93 RID: 40083
			// (set) Token: 0x0600CC69 RID: 52329 RVA: 0x0012383C File Offset: 0x00121A3C
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17009C94 RID: 40084
			// (set) Token: 0x0600CC6A RID: 52330 RVA: 0x0012384F File Offset: 0x00121A4F
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17009C95 RID: 40085
			// (set) Token: 0x0600CC6B RID: 52331 RVA: 0x00123862 File Offset: 0x00121A62
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17009C96 RID: 40086
			// (set) Token: 0x0600CC6C RID: 52332 RVA: 0x00123875 File Offset: 0x00121A75
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17009C97 RID: 40087
			// (set) Token: 0x0600CC6D RID: 52333 RVA: 0x00123888 File Offset: 0x00121A88
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17009C98 RID: 40088
			// (set) Token: 0x0600CC6E RID: 52334 RVA: 0x0012389B File Offset: 0x00121A9B
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17009C99 RID: 40089
			// (set) Token: 0x0600CC6F RID: 52335 RVA: 0x001238AE File Offset: 0x00121AAE
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17009C9A RID: 40090
			// (set) Token: 0x0600CC70 RID: 52336 RVA: 0x001238C6 File Offset: 0x00121AC6
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17009C9B RID: 40091
			// (set) Token: 0x0600CC71 RID: 52337 RVA: 0x001238D9 File Offset: 0x00121AD9
			public virtual SecurityIdentifier MasterAccountSid
			{
				set
				{
					base.PowerSharpParameters["MasterAccountSid"] = value;
				}
			}

			// Token: 0x17009C9C RID: 40092
			// (set) Token: 0x0600CC72 RID: 52338 RVA: 0x001238EC File Offset: 0x00121AEC
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17009C9D RID: 40093
			// (set) Token: 0x0600CC73 RID: 52339 RVA: 0x00123904 File Offset: 0x00121B04
			public virtual string IntendedMailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["IntendedMailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17009C9E RID: 40094
			// (set) Token: 0x0600CC74 RID: 52340 RVA: 0x00123922 File Offset: 0x00121B22
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17009C9F RID: 40095
			// (set) Token: 0x0600CC75 RID: 52341 RVA: 0x0012393A File Offset: 0x00121B3A
			public virtual ExchangeResourceType? ResourceType
			{
				set
				{
					base.PowerSharpParameters["ResourceType"] = value;
				}
			}

			// Token: 0x17009CA0 RID: 40096
			// (set) Token: 0x0600CC76 RID: 52342 RVA: 0x00123952 File Offset: 0x00121B52
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17009CA1 RID: 40097
			// (set) Token: 0x0600CC77 RID: 52343 RVA: 0x0012396A File Offset: 0x00121B6A
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17009CA2 RID: 40098
			// (set) Token: 0x0600CC78 RID: 52344 RVA: 0x0012397D File Offset: 0x00121B7D
			public virtual MultiValuedProperty<string> ResourceMetaData
			{
				set
				{
					base.PowerSharpParameters["ResourceMetaData"] = value;
				}
			}

			// Token: 0x17009CA3 RID: 40099
			// (set) Token: 0x0600CC79 RID: 52345 RVA: 0x00123990 File Offset: 0x00121B90
			public virtual string ResourcePropertiesDisplay
			{
				set
				{
					base.PowerSharpParameters["ResourcePropertiesDisplay"] = value;
				}
			}

			// Token: 0x17009CA4 RID: 40100
			// (set) Token: 0x0600CC7A RID: 52346 RVA: 0x001239A3 File Offset: 0x00121BA3
			public virtual MultiValuedProperty<string> ResourceSearchProperties
			{
				set
				{
					base.PowerSharpParameters["ResourceSearchProperties"] = value;
				}
			}

			// Token: 0x17009CA5 RID: 40101
			// (set) Token: 0x0600CC7B RID: 52347 RVA: 0x001239B6 File Offset: 0x00121BB6
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17009CA6 RID: 40102
			// (set) Token: 0x0600CC7C RID: 52348 RVA: 0x001239CE File Offset: 0x00121BCE
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17009CA7 RID: 40103
			// (set) Token: 0x0600CC7D RID: 52349 RVA: 0x001239E1 File Offset: 0x00121BE1
			public virtual bool IsCalculatedTargetAddress
			{
				set
				{
					base.PowerSharpParameters["IsCalculatedTargetAddress"] = value;
				}
			}

			// Token: 0x17009CA8 RID: 40104
			// (set) Token: 0x0600CC7E RID: 52350 RVA: 0x001239F9 File Offset: 0x00121BF9
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17009CA9 RID: 40105
			// (set) Token: 0x0600CC7F RID: 52351 RVA: 0x00123A0C File Offset: 0x00121C0C
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17009CAA RID: 40106
			// (set) Token: 0x0600CC80 RID: 52352 RVA: 0x00123A24 File Offset: 0x00121C24
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17009CAB RID: 40107
			// (set) Token: 0x0600CC81 RID: 52353 RVA: 0x00123A37 File Offset: 0x00121C37
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17009CAC RID: 40108
			// (set) Token: 0x0600CC82 RID: 52354 RVA: 0x00123A4A File Offset: 0x00121C4A
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17009CAD RID: 40109
			// (set) Token: 0x0600CC83 RID: 52355 RVA: 0x00123A5D File Offset: 0x00121C5D
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17009CAE RID: 40110
			// (set) Token: 0x0600CC84 RID: 52356 RVA: 0x00123A75 File Offset: 0x00121C75
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17009CAF RID: 40111
			// (set) Token: 0x0600CC85 RID: 52357 RVA: 0x00123A8D File Offset: 0x00121C8D
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17009CB0 RID: 40112
			// (set) Token: 0x0600CC86 RID: 52358 RVA: 0x00123AA0 File Offset: 0x00121CA0
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17009CB1 RID: 40113
			// (set) Token: 0x0600CC87 RID: 52359 RVA: 0x00123AB3 File Offset: 0x00121CB3
			public virtual ElcMailboxFlags ElcMailboxFlags
			{
				set
				{
					base.PowerSharpParameters["ElcMailboxFlags"] = value;
				}
			}

			// Token: 0x17009CB2 RID: 40114
			// (set) Token: 0x0600CC88 RID: 52360 RVA: 0x00123ACB File Offset: 0x00121CCB
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17009CB3 RID: 40115
			// (set) Token: 0x0600CC89 RID: 52361 RVA: 0x00123AE3 File Offset: 0x00121CE3
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17009CB4 RID: 40116
			// (set) Token: 0x0600CC8A RID: 52362 RVA: 0x00123AFB File Offset: 0x00121CFB
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x17009CB5 RID: 40117
			// (set) Token: 0x0600CC8B RID: 52363 RVA: 0x00123B0E File Offset: 0x00121D0E
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x17009CB6 RID: 40118
			// (set) Token: 0x0600CC8C RID: 52364 RVA: 0x00123B21 File Offset: 0x00121D21
			public virtual bool MailboxAuditEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditEnabled"] = value;
				}
			}

			// Token: 0x17009CB7 RID: 40119
			// (set) Token: 0x0600CC8D RID: 52365 RVA: 0x00123B39 File Offset: 0x00121D39
			public virtual EnhancedTimeSpan MailboxAuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x17009CB8 RID: 40120
			// (set) Token: 0x0600CC8E RID: 52366 RVA: 0x00123B51 File Offset: 0x00121D51
			public virtual MailboxAuditOperations AuditAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditAdminOperations"] = value;
				}
			}

			// Token: 0x17009CB9 RID: 40121
			// (set) Token: 0x0600CC8F RID: 52367 RVA: 0x00123B69 File Offset: 0x00121D69
			public virtual MailboxAuditOperations AuditDelegateAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateAdminOperations"] = value;
				}
			}

			// Token: 0x17009CBA RID: 40122
			// (set) Token: 0x0600CC90 RID: 52368 RVA: 0x00123B81 File Offset: 0x00121D81
			public virtual MailboxAuditOperations AuditDelegateOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateOperations"] = value;
				}
			}

			// Token: 0x17009CBB RID: 40123
			// (set) Token: 0x0600CC91 RID: 52369 RVA: 0x00123B99 File Offset: 0x00121D99
			public virtual MailboxAuditOperations AuditOwnerOperations
			{
				set
				{
					base.PowerSharpParameters["AuditOwnerOperations"] = value;
				}
			}

			// Token: 0x17009CBC RID: 40124
			// (set) Token: 0x0600CC92 RID: 52370 RVA: 0x00123BB1 File Offset: 0x00121DB1
			public virtual bool BypassAudit
			{
				set
				{
					base.PowerSharpParameters["BypassAudit"] = value;
				}
			}

			// Token: 0x17009CBD RID: 40125
			// (set) Token: 0x0600CC93 RID: 52371 RVA: 0x00123BC9 File Offset: 0x00121DC9
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x17009CBE RID: 40126
			// (set) Token: 0x0600CC94 RID: 52372 RVA: 0x00123BDC File Offset: 0x00121DDC
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x17009CBF RID: 40127
			// (set) Token: 0x0600CC95 RID: 52373 RVA: 0x00123BF4 File Offset: 0x00121DF4
			public virtual RecipientIdParameter SiteMailboxOwners
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxOwners"] = value;
				}
			}

			// Token: 0x17009CC0 RID: 40128
			// (set) Token: 0x0600CC96 RID: 52374 RVA: 0x00123C07 File Offset: 0x00121E07
			public virtual RecipientIdParameter SiteMailboxUsers
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxUsers"] = value;
				}
			}

			// Token: 0x17009CC1 RID: 40129
			// (set) Token: 0x0600CC97 RID: 52375 RVA: 0x00123C1A File Offset: 0x00121E1A
			public virtual DateTime? SiteMailboxClosedTime
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxClosedTime"] = value;
				}
			}

			// Token: 0x17009CC2 RID: 40130
			// (set) Token: 0x0600CC98 RID: 52376 RVA: 0x00123C32 File Offset: 0x00121E32
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x17009CC3 RID: 40131
			// (set) Token: 0x0600CC99 RID: 52377 RVA: 0x00123C45 File Offset: 0x00121E45
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17009CC4 RID: 40132
			// (set) Token: 0x0600CC9A RID: 52378 RVA: 0x00123C5D File Offset: 0x00121E5D
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17009CC5 RID: 40133
			// (set) Token: 0x0600CC9B RID: 52379 RVA: 0x00123C75 File Offset: 0x00121E75
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17009CC6 RID: 40134
			// (set) Token: 0x0600CC9C RID: 52380 RVA: 0x00123C8D File Offset: 0x00121E8D
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17009CC7 RID: 40135
			// (set) Token: 0x0600CC9D RID: 52381 RVA: 0x00123CA0 File Offset: 0x00121EA0
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17009CC8 RID: 40136
			// (set) Token: 0x0600CC9E RID: 52382 RVA: 0x00123CB3 File Offset: 0x00121EB3
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17009CC9 RID: 40137
			// (set) Token: 0x0600CC9F RID: 52383 RVA: 0x00123CC6 File Offset: 0x00121EC6
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009CCA RID: 40138
			// (set) Token: 0x0600CCA0 RID: 52384 RVA: 0x00123CD9 File Offset: 0x00121ED9
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009CCB RID: 40139
			// (set) Token: 0x0600CCA1 RID: 52385 RVA: 0x00123CEC File Offset: 0x00121EEC
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17009CCC RID: 40140
			// (set) Token: 0x0600CCA2 RID: 52386 RVA: 0x00123CFF File Offset: 0x00121EFF
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17009CCD RID: 40141
			// (set) Token: 0x0600CCA3 RID: 52387 RVA: 0x00123D17 File Offset: 0x00121F17
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17009CCE RID: 40142
			// (set) Token: 0x0600CCA4 RID: 52388 RVA: 0x00123D2A File Offset: 0x00121F2A
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17009CCF RID: 40143
			// (set) Token: 0x0600CCA5 RID: 52389 RVA: 0x00123D42 File Offset: 0x00121F42
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17009CD0 RID: 40144
			// (set) Token: 0x0600CCA6 RID: 52390 RVA: 0x00123D55 File Offset: 0x00121F55
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17009CD1 RID: 40145
			// (set) Token: 0x0600CCA7 RID: 52391 RVA: 0x00123D6D File Offset: 0x00121F6D
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17009CD2 RID: 40146
			// (set) Token: 0x0600CCA8 RID: 52392 RVA: 0x00123D80 File Offset: 0x00121F80
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17009CD3 RID: 40147
			// (set) Token: 0x0600CCA9 RID: 52393 RVA: 0x00123D9E File Offset: 0x00121F9E
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17009CD4 RID: 40148
			// (set) Token: 0x0600CCAA RID: 52394 RVA: 0x00123DB1 File Offset: 0x00121FB1
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17009CD5 RID: 40149
			// (set) Token: 0x0600CCAB RID: 52395 RVA: 0x00123DC9 File Offset: 0x00121FC9
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17009CD6 RID: 40150
			// (set) Token: 0x0600CCAC RID: 52396 RVA: 0x00123DE1 File Offset: 0x00121FE1
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17009CD7 RID: 40151
			// (set) Token: 0x0600CCAD RID: 52397 RVA: 0x00123DF9 File Offset: 0x00121FF9
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17009CD8 RID: 40152
			// (set) Token: 0x0600CCAE RID: 52398 RVA: 0x00123E0C File Offset: 0x0012200C
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17009CD9 RID: 40153
			// (set) Token: 0x0600CCAF RID: 52399 RVA: 0x00123E24 File Offset: 0x00122024
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17009CDA RID: 40154
			// (set) Token: 0x0600CCB0 RID: 52400 RVA: 0x00123E37 File Offset: 0x00122037
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17009CDB RID: 40155
			// (set) Token: 0x0600CCB1 RID: 52401 RVA: 0x00123E4A File Offset: 0x0012204A
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17009CDC RID: 40156
			// (set) Token: 0x0600CCB2 RID: 52402 RVA: 0x00123E68 File Offset: 0x00122068
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17009CDD RID: 40157
			// (set) Token: 0x0600CCB3 RID: 52403 RVA: 0x00123E7B File Offset: 0x0012207B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009CDE RID: 40158
			// (set) Token: 0x0600CCB4 RID: 52404 RVA: 0x00123E99 File Offset: 0x00122099
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009CDF RID: 40159
			// (set) Token: 0x0600CCB5 RID: 52405 RVA: 0x00123EAC File Offset: 0x001220AC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009CE0 RID: 40160
			// (set) Token: 0x0600CCB6 RID: 52406 RVA: 0x00123EC4 File Offset: 0x001220C4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009CE1 RID: 40161
			// (set) Token: 0x0600CCB7 RID: 52407 RVA: 0x00123EDC File Offset: 0x001220DC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009CE2 RID: 40162
			// (set) Token: 0x0600CCB8 RID: 52408 RVA: 0x00123EF4 File Offset: 0x001220F4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009CE3 RID: 40163
			// (set) Token: 0x0600CCB9 RID: 52409 RVA: 0x00123F0C File Offset: 0x0012210C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DC4 RID: 3524
		public class MicrosoftOnlineServicesIDParameters : ParametersBase
		{
			// Token: 0x17009CE4 RID: 40164
			// (set) Token: 0x0600CCBB RID: 52411 RVA: 0x00123F2C File Offset: 0x0012212C
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17009CE5 RID: 40165
			// (set) Token: 0x0600CCBC RID: 52412 RVA: 0x00123F3F File Offset: 0x0012213F
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x17009CE6 RID: 40166
			// (set) Token: 0x0600CCBD RID: 52413 RVA: 0x00123F52 File Offset: 0x00122152
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17009CE7 RID: 40167
			// (set) Token: 0x0600CCBE RID: 52414 RVA: 0x00123F65 File Offset: 0x00122165
			public virtual WindowsLiveId MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x17009CE8 RID: 40168
			// (set) Token: 0x0600CCBF RID: 52415 RVA: 0x00123F78 File Offset: 0x00122178
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17009CE9 RID: 40169
			// (set) Token: 0x0600CCC0 RID: 52416 RVA: 0x00123F8B File Offset: 0x0012218B
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17009CEA RID: 40170
			// (set) Token: 0x0600CCC1 RID: 52417 RVA: 0x00123F9E File Offset: 0x0012219E
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17009CEB RID: 40171
			// (set) Token: 0x0600CCC2 RID: 52418 RVA: 0x00123FB1 File Offset: 0x001221B1
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17009CEC RID: 40172
			// (set) Token: 0x0600CCC3 RID: 52419 RVA: 0x00123FC4 File Offset: 0x001221C4
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17009CED RID: 40173
			// (set) Token: 0x0600CCC4 RID: 52420 RVA: 0x00123FD7 File Offset: 0x001221D7
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17009CEE RID: 40174
			// (set) Token: 0x0600CCC5 RID: 52421 RVA: 0x00123FEA File Offset: 0x001221EA
			public virtual Guid ExchangeGuid
			{
				set
				{
					base.PowerSharpParameters["ExchangeGuid"] = value;
				}
			}

			// Token: 0x17009CEF RID: 40175
			// (set) Token: 0x0600CCC6 RID: 52422 RVA: 0x00124002 File Offset: 0x00122202
			public virtual string ForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17009CF0 RID: 40176
			// (set) Token: 0x0600CCC7 RID: 52423 RVA: 0x00124020 File Offset: 0x00122220
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17009CF1 RID: 40177
			// (set) Token: 0x0600CCC8 RID: 52424 RVA: 0x00124038 File Offset: 0x00122238
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17009CF2 RID: 40178
			// (set) Token: 0x0600CCC9 RID: 52425 RVA: 0x0012404B File Offset: 0x0012224B
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17009CF3 RID: 40179
			// (set) Token: 0x0600CCCA RID: 52426 RVA: 0x0012405E File Offset: 0x0012225E
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17009CF4 RID: 40180
			// (set) Token: 0x0600CCCB RID: 52427 RVA: 0x00124076 File Offset: 0x00122276
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17009CF5 RID: 40181
			// (set) Token: 0x0600CCCC RID: 52428 RVA: 0x00124089 File Offset: 0x00122289
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17009CF6 RID: 40182
			// (set) Token: 0x0600CCCD RID: 52429 RVA: 0x0012409C File Offset: 0x0012229C
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17009CF7 RID: 40183
			// (set) Token: 0x0600CCCE RID: 52430 RVA: 0x001240AF File Offset: 0x001222AF
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17009CF8 RID: 40184
			// (set) Token: 0x0600CCCF RID: 52431 RVA: 0x001240C2 File Offset: 0x001222C2
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17009CF9 RID: 40185
			// (set) Token: 0x0600CCD0 RID: 52432 RVA: 0x001240D5 File Offset: 0x001222D5
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17009CFA RID: 40186
			// (set) Token: 0x0600CCD1 RID: 52433 RVA: 0x001240E8 File Offset: 0x001222E8
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17009CFB RID: 40187
			// (set) Token: 0x0600CCD2 RID: 52434 RVA: 0x001240FB File Offset: 0x001222FB
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17009CFC RID: 40188
			// (set) Token: 0x0600CCD3 RID: 52435 RVA: 0x0012410E File Offset: 0x0012230E
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17009CFD RID: 40189
			// (set) Token: 0x0600CCD4 RID: 52436 RVA: 0x00124121 File Offset: 0x00122321
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17009CFE RID: 40190
			// (set) Token: 0x0600CCD5 RID: 52437 RVA: 0x00124134 File Offset: 0x00122334
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17009CFF RID: 40191
			// (set) Token: 0x0600CCD6 RID: 52438 RVA: 0x00124147 File Offset: 0x00122347
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17009D00 RID: 40192
			// (set) Token: 0x0600CCD7 RID: 52439 RVA: 0x0012415A File Offset: 0x0012235A
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17009D01 RID: 40193
			// (set) Token: 0x0600CCD8 RID: 52440 RVA: 0x0012416D File Offset: 0x0012236D
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17009D02 RID: 40194
			// (set) Token: 0x0600CCD9 RID: 52441 RVA: 0x00124180 File Offset: 0x00122380
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17009D03 RID: 40195
			// (set) Token: 0x0600CCDA RID: 52442 RVA: 0x00124193 File Offset: 0x00122393
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17009D04 RID: 40196
			// (set) Token: 0x0600CCDB RID: 52443 RVA: 0x001241A6 File Offset: 0x001223A6
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17009D05 RID: 40197
			// (set) Token: 0x0600CCDC RID: 52444 RVA: 0x001241B9 File Offset: 0x001223B9
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17009D06 RID: 40198
			// (set) Token: 0x0600CCDD RID: 52445 RVA: 0x001241CC File Offset: 0x001223CC
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17009D07 RID: 40199
			// (set) Token: 0x0600CCDE RID: 52446 RVA: 0x001241DF File Offset: 0x001223DF
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17009D08 RID: 40200
			// (set) Token: 0x0600CCDF RID: 52447 RVA: 0x001241F2 File Offset: 0x001223F2
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17009D09 RID: 40201
			// (set) Token: 0x0600CCE0 RID: 52448 RVA: 0x00124205 File Offset: 0x00122405
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17009D0A RID: 40202
			// (set) Token: 0x0600CCE1 RID: 52449 RVA: 0x0012421D File Offset: 0x0012241D
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17009D0B RID: 40203
			// (set) Token: 0x0600CCE2 RID: 52450 RVA: 0x00124230 File Offset: 0x00122430
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17009D0C RID: 40204
			// (set) Token: 0x0600CCE3 RID: 52451 RVA: 0x00124243 File Offset: 0x00122443
			public virtual RecipientDisplayType? RecipientDisplayType
			{
				set
				{
					base.PowerSharpParameters["RecipientDisplayType"] = value;
				}
			}

			// Token: 0x17009D0D RID: 40205
			// (set) Token: 0x0600CCE4 RID: 52452 RVA: 0x0012425B File Offset: 0x0012245B
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17009D0E RID: 40206
			// (set) Token: 0x0600CCE5 RID: 52453 RVA: 0x00124273 File Offset: 0x00122473
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17009D0F RID: 40207
			// (set) Token: 0x0600CCE6 RID: 52454 RVA: 0x0012428B File Offset: 0x0012248B
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17009D10 RID: 40208
			// (set) Token: 0x0600CCE7 RID: 52455 RVA: 0x001242A3 File Offset: 0x001224A3
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17009D11 RID: 40209
			// (set) Token: 0x0600CCE8 RID: 52456 RVA: 0x001242BB File Offset: 0x001224BB
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17009D12 RID: 40210
			// (set) Token: 0x0600CCE9 RID: 52457 RVA: 0x001242CE File Offset: 0x001224CE
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17009D13 RID: 40211
			// (set) Token: 0x0600CCEA RID: 52458 RVA: 0x001242E1 File Offset: 0x001224E1
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17009D14 RID: 40212
			// (set) Token: 0x0600CCEB RID: 52459 RVA: 0x001242F4 File Offset: 0x001224F4
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17009D15 RID: 40213
			// (set) Token: 0x0600CCEC RID: 52460 RVA: 0x00124307 File Offset: 0x00122507
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17009D16 RID: 40214
			// (set) Token: 0x0600CCED RID: 52461 RVA: 0x0012431A File Offset: 0x0012251A
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17009D17 RID: 40215
			// (set) Token: 0x0600CCEE RID: 52462 RVA: 0x0012432D File Offset: 0x0012252D
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17009D18 RID: 40216
			// (set) Token: 0x0600CCEF RID: 52463 RVA: 0x00124345 File Offset: 0x00122545
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17009D19 RID: 40217
			// (set) Token: 0x0600CCF0 RID: 52464 RVA: 0x00124358 File Offset: 0x00122558
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17009D1A RID: 40218
			// (set) Token: 0x0600CCF1 RID: 52465 RVA: 0x0012436B File Offset: 0x0012256B
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17009D1B RID: 40219
			// (set) Token: 0x0600CCF2 RID: 52466 RVA: 0x0012437E File Offset: 0x0012257E
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17009D1C RID: 40220
			// (set) Token: 0x0600CCF3 RID: 52467 RVA: 0x00124391 File Offset: 0x00122591
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17009D1D RID: 40221
			// (set) Token: 0x0600CCF4 RID: 52468 RVA: 0x001243A4 File Offset: 0x001225A4
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17009D1E RID: 40222
			// (set) Token: 0x0600CCF5 RID: 52469 RVA: 0x001243C2 File Offset: 0x001225C2
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17009D1F RID: 40223
			// (set) Token: 0x0600CCF6 RID: 52470 RVA: 0x001243D5 File Offset: 0x001225D5
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17009D20 RID: 40224
			// (set) Token: 0x0600CCF7 RID: 52471 RVA: 0x001243E8 File Offset: 0x001225E8
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17009D21 RID: 40225
			// (set) Token: 0x0600CCF8 RID: 52472 RVA: 0x001243FB File Offset: 0x001225FB
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17009D22 RID: 40226
			// (set) Token: 0x0600CCF9 RID: 52473 RVA: 0x0012440E File Offset: 0x0012260E
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17009D23 RID: 40227
			// (set) Token: 0x0600CCFA RID: 52474 RVA: 0x00124421 File Offset: 0x00122621
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17009D24 RID: 40228
			// (set) Token: 0x0600CCFB RID: 52475 RVA: 0x00124434 File Offset: 0x00122634
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17009D25 RID: 40229
			// (set) Token: 0x0600CCFC RID: 52476 RVA: 0x00124447 File Offset: 0x00122647
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17009D26 RID: 40230
			// (set) Token: 0x0600CCFD RID: 52477 RVA: 0x0012445A File Offset: 0x0012265A
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17009D27 RID: 40231
			// (set) Token: 0x0600CCFE RID: 52478 RVA: 0x0012446D File Offset: 0x0012266D
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17009D28 RID: 40232
			// (set) Token: 0x0600CCFF RID: 52479 RVA: 0x00124480 File Offset: 0x00122680
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17009D29 RID: 40233
			// (set) Token: 0x0600CD00 RID: 52480 RVA: 0x00124493 File Offset: 0x00122693
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17009D2A RID: 40234
			// (set) Token: 0x0600CD01 RID: 52481 RVA: 0x001244A6 File Offset: 0x001226A6
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17009D2B RID: 40235
			// (set) Token: 0x0600CD02 RID: 52482 RVA: 0x001244B9 File Offset: 0x001226B9
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17009D2C RID: 40236
			// (set) Token: 0x0600CD03 RID: 52483 RVA: 0x001244D1 File Offset: 0x001226D1
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17009D2D RID: 40237
			// (set) Token: 0x0600CD04 RID: 52484 RVA: 0x001244E4 File Offset: 0x001226E4
			public virtual SecurityIdentifier MasterAccountSid
			{
				set
				{
					base.PowerSharpParameters["MasterAccountSid"] = value;
				}
			}

			// Token: 0x17009D2E RID: 40238
			// (set) Token: 0x0600CD05 RID: 52485 RVA: 0x001244F7 File Offset: 0x001226F7
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17009D2F RID: 40239
			// (set) Token: 0x0600CD06 RID: 52486 RVA: 0x0012450F File Offset: 0x0012270F
			public virtual string IntendedMailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["IntendedMailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17009D30 RID: 40240
			// (set) Token: 0x0600CD07 RID: 52487 RVA: 0x0012452D File Offset: 0x0012272D
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17009D31 RID: 40241
			// (set) Token: 0x0600CD08 RID: 52488 RVA: 0x00124545 File Offset: 0x00122745
			public virtual ExchangeResourceType? ResourceType
			{
				set
				{
					base.PowerSharpParameters["ResourceType"] = value;
				}
			}

			// Token: 0x17009D32 RID: 40242
			// (set) Token: 0x0600CD09 RID: 52489 RVA: 0x0012455D File Offset: 0x0012275D
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17009D33 RID: 40243
			// (set) Token: 0x0600CD0A RID: 52490 RVA: 0x00124575 File Offset: 0x00122775
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17009D34 RID: 40244
			// (set) Token: 0x0600CD0B RID: 52491 RVA: 0x00124588 File Offset: 0x00122788
			public virtual MultiValuedProperty<string> ResourceMetaData
			{
				set
				{
					base.PowerSharpParameters["ResourceMetaData"] = value;
				}
			}

			// Token: 0x17009D35 RID: 40245
			// (set) Token: 0x0600CD0C RID: 52492 RVA: 0x0012459B File Offset: 0x0012279B
			public virtual string ResourcePropertiesDisplay
			{
				set
				{
					base.PowerSharpParameters["ResourcePropertiesDisplay"] = value;
				}
			}

			// Token: 0x17009D36 RID: 40246
			// (set) Token: 0x0600CD0D RID: 52493 RVA: 0x001245AE File Offset: 0x001227AE
			public virtual MultiValuedProperty<string> ResourceSearchProperties
			{
				set
				{
					base.PowerSharpParameters["ResourceSearchProperties"] = value;
				}
			}

			// Token: 0x17009D37 RID: 40247
			// (set) Token: 0x0600CD0E RID: 52494 RVA: 0x001245C1 File Offset: 0x001227C1
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17009D38 RID: 40248
			// (set) Token: 0x0600CD0F RID: 52495 RVA: 0x001245D9 File Offset: 0x001227D9
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17009D39 RID: 40249
			// (set) Token: 0x0600CD10 RID: 52496 RVA: 0x001245EC File Offset: 0x001227EC
			public virtual bool IsCalculatedTargetAddress
			{
				set
				{
					base.PowerSharpParameters["IsCalculatedTargetAddress"] = value;
				}
			}

			// Token: 0x17009D3A RID: 40250
			// (set) Token: 0x0600CD11 RID: 52497 RVA: 0x00124604 File Offset: 0x00122804
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17009D3B RID: 40251
			// (set) Token: 0x0600CD12 RID: 52498 RVA: 0x00124617 File Offset: 0x00122817
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17009D3C RID: 40252
			// (set) Token: 0x0600CD13 RID: 52499 RVA: 0x0012462F File Offset: 0x0012282F
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17009D3D RID: 40253
			// (set) Token: 0x0600CD14 RID: 52500 RVA: 0x00124642 File Offset: 0x00122842
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17009D3E RID: 40254
			// (set) Token: 0x0600CD15 RID: 52501 RVA: 0x00124655 File Offset: 0x00122855
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17009D3F RID: 40255
			// (set) Token: 0x0600CD16 RID: 52502 RVA: 0x00124668 File Offset: 0x00122868
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17009D40 RID: 40256
			// (set) Token: 0x0600CD17 RID: 52503 RVA: 0x00124680 File Offset: 0x00122880
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17009D41 RID: 40257
			// (set) Token: 0x0600CD18 RID: 52504 RVA: 0x00124698 File Offset: 0x00122898
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17009D42 RID: 40258
			// (set) Token: 0x0600CD19 RID: 52505 RVA: 0x001246AB File Offset: 0x001228AB
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17009D43 RID: 40259
			// (set) Token: 0x0600CD1A RID: 52506 RVA: 0x001246BE File Offset: 0x001228BE
			public virtual ElcMailboxFlags ElcMailboxFlags
			{
				set
				{
					base.PowerSharpParameters["ElcMailboxFlags"] = value;
				}
			}

			// Token: 0x17009D44 RID: 40260
			// (set) Token: 0x0600CD1B RID: 52507 RVA: 0x001246D6 File Offset: 0x001228D6
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17009D45 RID: 40261
			// (set) Token: 0x0600CD1C RID: 52508 RVA: 0x001246EE File Offset: 0x001228EE
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17009D46 RID: 40262
			// (set) Token: 0x0600CD1D RID: 52509 RVA: 0x00124706 File Offset: 0x00122906
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x17009D47 RID: 40263
			// (set) Token: 0x0600CD1E RID: 52510 RVA: 0x00124719 File Offset: 0x00122919
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x17009D48 RID: 40264
			// (set) Token: 0x0600CD1F RID: 52511 RVA: 0x0012472C File Offset: 0x0012292C
			public virtual bool MailboxAuditEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditEnabled"] = value;
				}
			}

			// Token: 0x17009D49 RID: 40265
			// (set) Token: 0x0600CD20 RID: 52512 RVA: 0x00124744 File Offset: 0x00122944
			public virtual EnhancedTimeSpan MailboxAuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x17009D4A RID: 40266
			// (set) Token: 0x0600CD21 RID: 52513 RVA: 0x0012475C File Offset: 0x0012295C
			public virtual MailboxAuditOperations AuditAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditAdminOperations"] = value;
				}
			}

			// Token: 0x17009D4B RID: 40267
			// (set) Token: 0x0600CD22 RID: 52514 RVA: 0x00124774 File Offset: 0x00122974
			public virtual MailboxAuditOperations AuditDelegateAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateAdminOperations"] = value;
				}
			}

			// Token: 0x17009D4C RID: 40268
			// (set) Token: 0x0600CD23 RID: 52515 RVA: 0x0012478C File Offset: 0x0012298C
			public virtual MailboxAuditOperations AuditDelegateOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateOperations"] = value;
				}
			}

			// Token: 0x17009D4D RID: 40269
			// (set) Token: 0x0600CD24 RID: 52516 RVA: 0x001247A4 File Offset: 0x001229A4
			public virtual MailboxAuditOperations AuditOwnerOperations
			{
				set
				{
					base.PowerSharpParameters["AuditOwnerOperations"] = value;
				}
			}

			// Token: 0x17009D4E RID: 40270
			// (set) Token: 0x0600CD25 RID: 52517 RVA: 0x001247BC File Offset: 0x001229BC
			public virtual bool BypassAudit
			{
				set
				{
					base.PowerSharpParameters["BypassAudit"] = value;
				}
			}

			// Token: 0x17009D4F RID: 40271
			// (set) Token: 0x0600CD26 RID: 52518 RVA: 0x001247D4 File Offset: 0x001229D4
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x17009D50 RID: 40272
			// (set) Token: 0x0600CD27 RID: 52519 RVA: 0x001247E7 File Offset: 0x001229E7
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x17009D51 RID: 40273
			// (set) Token: 0x0600CD28 RID: 52520 RVA: 0x001247FF File Offset: 0x001229FF
			public virtual RecipientIdParameter SiteMailboxOwners
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxOwners"] = value;
				}
			}

			// Token: 0x17009D52 RID: 40274
			// (set) Token: 0x0600CD29 RID: 52521 RVA: 0x00124812 File Offset: 0x00122A12
			public virtual RecipientIdParameter SiteMailboxUsers
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxUsers"] = value;
				}
			}

			// Token: 0x17009D53 RID: 40275
			// (set) Token: 0x0600CD2A RID: 52522 RVA: 0x00124825 File Offset: 0x00122A25
			public virtual DateTime? SiteMailboxClosedTime
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxClosedTime"] = value;
				}
			}

			// Token: 0x17009D54 RID: 40276
			// (set) Token: 0x0600CD2B RID: 52523 RVA: 0x0012483D File Offset: 0x00122A3D
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x17009D55 RID: 40277
			// (set) Token: 0x0600CD2C RID: 52524 RVA: 0x00124850 File Offset: 0x00122A50
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17009D56 RID: 40278
			// (set) Token: 0x0600CD2D RID: 52525 RVA: 0x00124868 File Offset: 0x00122A68
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17009D57 RID: 40279
			// (set) Token: 0x0600CD2E RID: 52526 RVA: 0x00124880 File Offset: 0x00122A80
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17009D58 RID: 40280
			// (set) Token: 0x0600CD2F RID: 52527 RVA: 0x00124898 File Offset: 0x00122A98
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17009D59 RID: 40281
			// (set) Token: 0x0600CD30 RID: 52528 RVA: 0x001248AB File Offset: 0x00122AAB
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17009D5A RID: 40282
			// (set) Token: 0x0600CD31 RID: 52529 RVA: 0x001248BE File Offset: 0x00122ABE
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17009D5B RID: 40283
			// (set) Token: 0x0600CD32 RID: 52530 RVA: 0x001248D1 File Offset: 0x00122AD1
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009D5C RID: 40284
			// (set) Token: 0x0600CD33 RID: 52531 RVA: 0x001248E4 File Offset: 0x00122AE4
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009D5D RID: 40285
			// (set) Token: 0x0600CD34 RID: 52532 RVA: 0x001248F7 File Offset: 0x00122AF7
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17009D5E RID: 40286
			// (set) Token: 0x0600CD35 RID: 52533 RVA: 0x0012490A File Offset: 0x00122B0A
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17009D5F RID: 40287
			// (set) Token: 0x0600CD36 RID: 52534 RVA: 0x00124922 File Offset: 0x00122B22
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17009D60 RID: 40288
			// (set) Token: 0x0600CD37 RID: 52535 RVA: 0x00124935 File Offset: 0x00122B35
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17009D61 RID: 40289
			// (set) Token: 0x0600CD38 RID: 52536 RVA: 0x0012494D File Offset: 0x00122B4D
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17009D62 RID: 40290
			// (set) Token: 0x0600CD39 RID: 52537 RVA: 0x00124960 File Offset: 0x00122B60
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17009D63 RID: 40291
			// (set) Token: 0x0600CD3A RID: 52538 RVA: 0x00124978 File Offset: 0x00122B78
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17009D64 RID: 40292
			// (set) Token: 0x0600CD3B RID: 52539 RVA: 0x0012498B File Offset: 0x00122B8B
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17009D65 RID: 40293
			// (set) Token: 0x0600CD3C RID: 52540 RVA: 0x001249A9 File Offset: 0x00122BA9
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17009D66 RID: 40294
			// (set) Token: 0x0600CD3D RID: 52541 RVA: 0x001249BC File Offset: 0x00122BBC
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17009D67 RID: 40295
			// (set) Token: 0x0600CD3E RID: 52542 RVA: 0x001249D4 File Offset: 0x00122BD4
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17009D68 RID: 40296
			// (set) Token: 0x0600CD3F RID: 52543 RVA: 0x001249EC File Offset: 0x00122BEC
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17009D69 RID: 40297
			// (set) Token: 0x0600CD40 RID: 52544 RVA: 0x00124A04 File Offset: 0x00122C04
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17009D6A RID: 40298
			// (set) Token: 0x0600CD41 RID: 52545 RVA: 0x00124A17 File Offset: 0x00122C17
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17009D6B RID: 40299
			// (set) Token: 0x0600CD42 RID: 52546 RVA: 0x00124A2F File Offset: 0x00122C2F
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17009D6C RID: 40300
			// (set) Token: 0x0600CD43 RID: 52547 RVA: 0x00124A42 File Offset: 0x00122C42
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17009D6D RID: 40301
			// (set) Token: 0x0600CD44 RID: 52548 RVA: 0x00124A55 File Offset: 0x00122C55
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17009D6E RID: 40302
			// (set) Token: 0x0600CD45 RID: 52549 RVA: 0x00124A73 File Offset: 0x00122C73
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17009D6F RID: 40303
			// (set) Token: 0x0600CD46 RID: 52550 RVA: 0x00124A86 File Offset: 0x00122C86
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009D70 RID: 40304
			// (set) Token: 0x0600CD47 RID: 52551 RVA: 0x00124AA4 File Offset: 0x00122CA4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009D71 RID: 40305
			// (set) Token: 0x0600CD48 RID: 52552 RVA: 0x00124AB7 File Offset: 0x00122CB7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009D72 RID: 40306
			// (set) Token: 0x0600CD49 RID: 52553 RVA: 0x00124ACF File Offset: 0x00122CCF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009D73 RID: 40307
			// (set) Token: 0x0600CD4A RID: 52554 RVA: 0x00124AE7 File Offset: 0x00122CE7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009D74 RID: 40308
			// (set) Token: 0x0600CD4B RID: 52555 RVA: 0x00124AFF File Offset: 0x00122CFF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009D75 RID: 40309
			// (set) Token: 0x0600CD4C RID: 52556 RVA: 0x00124B17 File Offset: 0x00122D17
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DC5 RID: 3525
		public class WindowsLiveIDParameters : ParametersBase
		{
			// Token: 0x17009D76 RID: 40310
			// (set) Token: 0x0600CD4E RID: 52558 RVA: 0x00124B37 File Offset: 0x00122D37
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17009D77 RID: 40311
			// (set) Token: 0x0600CD4F RID: 52559 RVA: 0x00124B4A File Offset: 0x00122D4A
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x17009D78 RID: 40312
			// (set) Token: 0x0600CD50 RID: 52560 RVA: 0x00124B5D File Offset: 0x00122D5D
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17009D79 RID: 40313
			// (set) Token: 0x0600CD51 RID: 52561 RVA: 0x00124B70 File Offset: 0x00122D70
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17009D7A RID: 40314
			// (set) Token: 0x0600CD52 RID: 52562 RVA: 0x00124B83 File Offset: 0x00122D83
			public virtual SwitchParameter EvictLiveId
			{
				set
				{
					base.PowerSharpParameters["EvictLiveId"] = value;
				}
			}

			// Token: 0x17009D7B RID: 40315
			// (set) Token: 0x0600CD53 RID: 52563 RVA: 0x00124B9B File Offset: 0x00122D9B
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17009D7C RID: 40316
			// (set) Token: 0x0600CD54 RID: 52564 RVA: 0x00124BAE File Offset: 0x00122DAE
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17009D7D RID: 40317
			// (set) Token: 0x0600CD55 RID: 52565 RVA: 0x00124BC1 File Offset: 0x00122DC1
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17009D7E RID: 40318
			// (set) Token: 0x0600CD56 RID: 52566 RVA: 0x00124BD4 File Offset: 0x00122DD4
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17009D7F RID: 40319
			// (set) Token: 0x0600CD57 RID: 52567 RVA: 0x00124BE7 File Offset: 0x00122DE7
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17009D80 RID: 40320
			// (set) Token: 0x0600CD58 RID: 52568 RVA: 0x00124BFA File Offset: 0x00122DFA
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17009D81 RID: 40321
			// (set) Token: 0x0600CD59 RID: 52569 RVA: 0x00124C0D File Offset: 0x00122E0D
			public virtual Guid ExchangeGuid
			{
				set
				{
					base.PowerSharpParameters["ExchangeGuid"] = value;
				}
			}

			// Token: 0x17009D82 RID: 40322
			// (set) Token: 0x0600CD5A RID: 52570 RVA: 0x00124C25 File Offset: 0x00122E25
			public virtual string ForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17009D83 RID: 40323
			// (set) Token: 0x0600CD5B RID: 52571 RVA: 0x00124C43 File Offset: 0x00122E43
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17009D84 RID: 40324
			// (set) Token: 0x0600CD5C RID: 52572 RVA: 0x00124C5B File Offset: 0x00122E5B
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17009D85 RID: 40325
			// (set) Token: 0x0600CD5D RID: 52573 RVA: 0x00124C6E File Offset: 0x00122E6E
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17009D86 RID: 40326
			// (set) Token: 0x0600CD5E RID: 52574 RVA: 0x00124C81 File Offset: 0x00122E81
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17009D87 RID: 40327
			// (set) Token: 0x0600CD5F RID: 52575 RVA: 0x00124C99 File Offset: 0x00122E99
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17009D88 RID: 40328
			// (set) Token: 0x0600CD60 RID: 52576 RVA: 0x00124CAC File Offset: 0x00122EAC
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17009D89 RID: 40329
			// (set) Token: 0x0600CD61 RID: 52577 RVA: 0x00124CBF File Offset: 0x00122EBF
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17009D8A RID: 40330
			// (set) Token: 0x0600CD62 RID: 52578 RVA: 0x00124CD2 File Offset: 0x00122ED2
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17009D8B RID: 40331
			// (set) Token: 0x0600CD63 RID: 52579 RVA: 0x00124CE5 File Offset: 0x00122EE5
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17009D8C RID: 40332
			// (set) Token: 0x0600CD64 RID: 52580 RVA: 0x00124CF8 File Offset: 0x00122EF8
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17009D8D RID: 40333
			// (set) Token: 0x0600CD65 RID: 52581 RVA: 0x00124D0B File Offset: 0x00122F0B
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17009D8E RID: 40334
			// (set) Token: 0x0600CD66 RID: 52582 RVA: 0x00124D1E File Offset: 0x00122F1E
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17009D8F RID: 40335
			// (set) Token: 0x0600CD67 RID: 52583 RVA: 0x00124D31 File Offset: 0x00122F31
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17009D90 RID: 40336
			// (set) Token: 0x0600CD68 RID: 52584 RVA: 0x00124D44 File Offset: 0x00122F44
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17009D91 RID: 40337
			// (set) Token: 0x0600CD69 RID: 52585 RVA: 0x00124D57 File Offset: 0x00122F57
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17009D92 RID: 40338
			// (set) Token: 0x0600CD6A RID: 52586 RVA: 0x00124D6A File Offset: 0x00122F6A
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17009D93 RID: 40339
			// (set) Token: 0x0600CD6B RID: 52587 RVA: 0x00124D7D File Offset: 0x00122F7D
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17009D94 RID: 40340
			// (set) Token: 0x0600CD6C RID: 52588 RVA: 0x00124D90 File Offset: 0x00122F90
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17009D95 RID: 40341
			// (set) Token: 0x0600CD6D RID: 52589 RVA: 0x00124DA3 File Offset: 0x00122FA3
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17009D96 RID: 40342
			// (set) Token: 0x0600CD6E RID: 52590 RVA: 0x00124DB6 File Offset: 0x00122FB6
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17009D97 RID: 40343
			// (set) Token: 0x0600CD6F RID: 52591 RVA: 0x00124DC9 File Offset: 0x00122FC9
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17009D98 RID: 40344
			// (set) Token: 0x0600CD70 RID: 52592 RVA: 0x00124DDC File Offset: 0x00122FDC
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17009D99 RID: 40345
			// (set) Token: 0x0600CD71 RID: 52593 RVA: 0x00124DEF File Offset: 0x00122FEF
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17009D9A RID: 40346
			// (set) Token: 0x0600CD72 RID: 52594 RVA: 0x00124E02 File Offset: 0x00123002
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17009D9B RID: 40347
			// (set) Token: 0x0600CD73 RID: 52595 RVA: 0x00124E15 File Offset: 0x00123015
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17009D9C RID: 40348
			// (set) Token: 0x0600CD74 RID: 52596 RVA: 0x00124E28 File Offset: 0x00123028
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17009D9D RID: 40349
			// (set) Token: 0x0600CD75 RID: 52597 RVA: 0x00124E40 File Offset: 0x00123040
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17009D9E RID: 40350
			// (set) Token: 0x0600CD76 RID: 52598 RVA: 0x00124E53 File Offset: 0x00123053
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17009D9F RID: 40351
			// (set) Token: 0x0600CD77 RID: 52599 RVA: 0x00124E66 File Offset: 0x00123066
			public virtual RecipientDisplayType? RecipientDisplayType
			{
				set
				{
					base.PowerSharpParameters["RecipientDisplayType"] = value;
				}
			}

			// Token: 0x17009DA0 RID: 40352
			// (set) Token: 0x0600CD78 RID: 52600 RVA: 0x00124E7E File Offset: 0x0012307E
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17009DA1 RID: 40353
			// (set) Token: 0x0600CD79 RID: 52601 RVA: 0x00124E96 File Offset: 0x00123096
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17009DA2 RID: 40354
			// (set) Token: 0x0600CD7A RID: 52602 RVA: 0x00124EAE File Offset: 0x001230AE
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17009DA3 RID: 40355
			// (set) Token: 0x0600CD7B RID: 52603 RVA: 0x00124EC6 File Offset: 0x001230C6
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17009DA4 RID: 40356
			// (set) Token: 0x0600CD7C RID: 52604 RVA: 0x00124EDE File Offset: 0x001230DE
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17009DA5 RID: 40357
			// (set) Token: 0x0600CD7D RID: 52605 RVA: 0x00124EF1 File Offset: 0x001230F1
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17009DA6 RID: 40358
			// (set) Token: 0x0600CD7E RID: 52606 RVA: 0x00124F04 File Offset: 0x00123104
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17009DA7 RID: 40359
			// (set) Token: 0x0600CD7F RID: 52607 RVA: 0x00124F17 File Offset: 0x00123117
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17009DA8 RID: 40360
			// (set) Token: 0x0600CD80 RID: 52608 RVA: 0x00124F2A File Offset: 0x0012312A
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17009DA9 RID: 40361
			// (set) Token: 0x0600CD81 RID: 52609 RVA: 0x00124F3D File Offset: 0x0012313D
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17009DAA RID: 40362
			// (set) Token: 0x0600CD82 RID: 52610 RVA: 0x00124F50 File Offset: 0x00123150
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17009DAB RID: 40363
			// (set) Token: 0x0600CD83 RID: 52611 RVA: 0x00124F68 File Offset: 0x00123168
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17009DAC RID: 40364
			// (set) Token: 0x0600CD84 RID: 52612 RVA: 0x00124F7B File Offset: 0x0012317B
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17009DAD RID: 40365
			// (set) Token: 0x0600CD85 RID: 52613 RVA: 0x00124F8E File Offset: 0x0012318E
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17009DAE RID: 40366
			// (set) Token: 0x0600CD86 RID: 52614 RVA: 0x00124FA1 File Offset: 0x001231A1
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17009DAF RID: 40367
			// (set) Token: 0x0600CD87 RID: 52615 RVA: 0x00124FB4 File Offset: 0x001231B4
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17009DB0 RID: 40368
			// (set) Token: 0x0600CD88 RID: 52616 RVA: 0x00124FC7 File Offset: 0x001231C7
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17009DB1 RID: 40369
			// (set) Token: 0x0600CD89 RID: 52617 RVA: 0x00124FE5 File Offset: 0x001231E5
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17009DB2 RID: 40370
			// (set) Token: 0x0600CD8A RID: 52618 RVA: 0x00124FF8 File Offset: 0x001231F8
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17009DB3 RID: 40371
			// (set) Token: 0x0600CD8B RID: 52619 RVA: 0x0012500B File Offset: 0x0012320B
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17009DB4 RID: 40372
			// (set) Token: 0x0600CD8C RID: 52620 RVA: 0x0012501E File Offset: 0x0012321E
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17009DB5 RID: 40373
			// (set) Token: 0x0600CD8D RID: 52621 RVA: 0x00125031 File Offset: 0x00123231
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17009DB6 RID: 40374
			// (set) Token: 0x0600CD8E RID: 52622 RVA: 0x00125044 File Offset: 0x00123244
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17009DB7 RID: 40375
			// (set) Token: 0x0600CD8F RID: 52623 RVA: 0x00125057 File Offset: 0x00123257
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17009DB8 RID: 40376
			// (set) Token: 0x0600CD90 RID: 52624 RVA: 0x0012506A File Offset: 0x0012326A
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17009DB9 RID: 40377
			// (set) Token: 0x0600CD91 RID: 52625 RVA: 0x0012507D File Offset: 0x0012327D
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17009DBA RID: 40378
			// (set) Token: 0x0600CD92 RID: 52626 RVA: 0x00125090 File Offset: 0x00123290
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17009DBB RID: 40379
			// (set) Token: 0x0600CD93 RID: 52627 RVA: 0x001250A3 File Offset: 0x001232A3
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17009DBC RID: 40380
			// (set) Token: 0x0600CD94 RID: 52628 RVA: 0x001250B6 File Offset: 0x001232B6
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17009DBD RID: 40381
			// (set) Token: 0x0600CD95 RID: 52629 RVA: 0x001250C9 File Offset: 0x001232C9
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17009DBE RID: 40382
			// (set) Token: 0x0600CD96 RID: 52630 RVA: 0x001250DC File Offset: 0x001232DC
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17009DBF RID: 40383
			// (set) Token: 0x0600CD97 RID: 52631 RVA: 0x001250F4 File Offset: 0x001232F4
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17009DC0 RID: 40384
			// (set) Token: 0x0600CD98 RID: 52632 RVA: 0x00125107 File Offset: 0x00123307
			public virtual SecurityIdentifier MasterAccountSid
			{
				set
				{
					base.PowerSharpParameters["MasterAccountSid"] = value;
				}
			}

			// Token: 0x17009DC1 RID: 40385
			// (set) Token: 0x0600CD99 RID: 52633 RVA: 0x0012511A File Offset: 0x0012331A
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17009DC2 RID: 40386
			// (set) Token: 0x0600CD9A RID: 52634 RVA: 0x00125132 File Offset: 0x00123332
			public virtual string IntendedMailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["IntendedMailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17009DC3 RID: 40387
			// (set) Token: 0x0600CD9B RID: 52635 RVA: 0x00125150 File Offset: 0x00123350
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17009DC4 RID: 40388
			// (set) Token: 0x0600CD9C RID: 52636 RVA: 0x00125168 File Offset: 0x00123368
			public virtual ExchangeResourceType? ResourceType
			{
				set
				{
					base.PowerSharpParameters["ResourceType"] = value;
				}
			}

			// Token: 0x17009DC5 RID: 40389
			// (set) Token: 0x0600CD9D RID: 52637 RVA: 0x00125180 File Offset: 0x00123380
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17009DC6 RID: 40390
			// (set) Token: 0x0600CD9E RID: 52638 RVA: 0x00125198 File Offset: 0x00123398
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17009DC7 RID: 40391
			// (set) Token: 0x0600CD9F RID: 52639 RVA: 0x001251AB File Offset: 0x001233AB
			public virtual MultiValuedProperty<string> ResourceMetaData
			{
				set
				{
					base.PowerSharpParameters["ResourceMetaData"] = value;
				}
			}

			// Token: 0x17009DC8 RID: 40392
			// (set) Token: 0x0600CDA0 RID: 52640 RVA: 0x001251BE File Offset: 0x001233BE
			public virtual string ResourcePropertiesDisplay
			{
				set
				{
					base.PowerSharpParameters["ResourcePropertiesDisplay"] = value;
				}
			}

			// Token: 0x17009DC9 RID: 40393
			// (set) Token: 0x0600CDA1 RID: 52641 RVA: 0x001251D1 File Offset: 0x001233D1
			public virtual MultiValuedProperty<string> ResourceSearchProperties
			{
				set
				{
					base.PowerSharpParameters["ResourceSearchProperties"] = value;
				}
			}

			// Token: 0x17009DCA RID: 40394
			// (set) Token: 0x0600CDA2 RID: 52642 RVA: 0x001251E4 File Offset: 0x001233E4
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17009DCB RID: 40395
			// (set) Token: 0x0600CDA3 RID: 52643 RVA: 0x001251FC File Offset: 0x001233FC
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17009DCC RID: 40396
			// (set) Token: 0x0600CDA4 RID: 52644 RVA: 0x0012520F File Offset: 0x0012340F
			public virtual bool IsCalculatedTargetAddress
			{
				set
				{
					base.PowerSharpParameters["IsCalculatedTargetAddress"] = value;
				}
			}

			// Token: 0x17009DCD RID: 40397
			// (set) Token: 0x0600CDA5 RID: 52645 RVA: 0x00125227 File Offset: 0x00123427
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17009DCE RID: 40398
			// (set) Token: 0x0600CDA6 RID: 52646 RVA: 0x0012523A File Offset: 0x0012343A
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17009DCF RID: 40399
			// (set) Token: 0x0600CDA7 RID: 52647 RVA: 0x00125252 File Offset: 0x00123452
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17009DD0 RID: 40400
			// (set) Token: 0x0600CDA8 RID: 52648 RVA: 0x00125265 File Offset: 0x00123465
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17009DD1 RID: 40401
			// (set) Token: 0x0600CDA9 RID: 52649 RVA: 0x00125278 File Offset: 0x00123478
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17009DD2 RID: 40402
			// (set) Token: 0x0600CDAA RID: 52650 RVA: 0x0012528B File Offset: 0x0012348B
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17009DD3 RID: 40403
			// (set) Token: 0x0600CDAB RID: 52651 RVA: 0x001252A3 File Offset: 0x001234A3
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17009DD4 RID: 40404
			// (set) Token: 0x0600CDAC RID: 52652 RVA: 0x001252BB File Offset: 0x001234BB
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17009DD5 RID: 40405
			// (set) Token: 0x0600CDAD RID: 52653 RVA: 0x001252CE File Offset: 0x001234CE
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17009DD6 RID: 40406
			// (set) Token: 0x0600CDAE RID: 52654 RVA: 0x001252E1 File Offset: 0x001234E1
			public virtual ElcMailboxFlags ElcMailboxFlags
			{
				set
				{
					base.PowerSharpParameters["ElcMailboxFlags"] = value;
				}
			}

			// Token: 0x17009DD7 RID: 40407
			// (set) Token: 0x0600CDAF RID: 52655 RVA: 0x001252F9 File Offset: 0x001234F9
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17009DD8 RID: 40408
			// (set) Token: 0x0600CDB0 RID: 52656 RVA: 0x00125311 File Offset: 0x00123511
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17009DD9 RID: 40409
			// (set) Token: 0x0600CDB1 RID: 52657 RVA: 0x00125329 File Offset: 0x00123529
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x17009DDA RID: 40410
			// (set) Token: 0x0600CDB2 RID: 52658 RVA: 0x0012533C File Offset: 0x0012353C
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x17009DDB RID: 40411
			// (set) Token: 0x0600CDB3 RID: 52659 RVA: 0x0012534F File Offset: 0x0012354F
			public virtual bool MailboxAuditEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditEnabled"] = value;
				}
			}

			// Token: 0x17009DDC RID: 40412
			// (set) Token: 0x0600CDB4 RID: 52660 RVA: 0x00125367 File Offset: 0x00123567
			public virtual EnhancedTimeSpan MailboxAuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x17009DDD RID: 40413
			// (set) Token: 0x0600CDB5 RID: 52661 RVA: 0x0012537F File Offset: 0x0012357F
			public virtual MailboxAuditOperations AuditAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditAdminOperations"] = value;
				}
			}

			// Token: 0x17009DDE RID: 40414
			// (set) Token: 0x0600CDB6 RID: 52662 RVA: 0x00125397 File Offset: 0x00123597
			public virtual MailboxAuditOperations AuditDelegateAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateAdminOperations"] = value;
				}
			}

			// Token: 0x17009DDF RID: 40415
			// (set) Token: 0x0600CDB7 RID: 52663 RVA: 0x001253AF File Offset: 0x001235AF
			public virtual MailboxAuditOperations AuditDelegateOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateOperations"] = value;
				}
			}

			// Token: 0x17009DE0 RID: 40416
			// (set) Token: 0x0600CDB8 RID: 52664 RVA: 0x001253C7 File Offset: 0x001235C7
			public virtual MailboxAuditOperations AuditOwnerOperations
			{
				set
				{
					base.PowerSharpParameters["AuditOwnerOperations"] = value;
				}
			}

			// Token: 0x17009DE1 RID: 40417
			// (set) Token: 0x0600CDB9 RID: 52665 RVA: 0x001253DF File Offset: 0x001235DF
			public virtual bool BypassAudit
			{
				set
				{
					base.PowerSharpParameters["BypassAudit"] = value;
				}
			}

			// Token: 0x17009DE2 RID: 40418
			// (set) Token: 0x0600CDBA RID: 52666 RVA: 0x001253F7 File Offset: 0x001235F7
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x17009DE3 RID: 40419
			// (set) Token: 0x0600CDBB RID: 52667 RVA: 0x0012540A File Offset: 0x0012360A
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x17009DE4 RID: 40420
			// (set) Token: 0x0600CDBC RID: 52668 RVA: 0x00125422 File Offset: 0x00123622
			public virtual RecipientIdParameter SiteMailboxOwners
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxOwners"] = value;
				}
			}

			// Token: 0x17009DE5 RID: 40421
			// (set) Token: 0x0600CDBD RID: 52669 RVA: 0x00125435 File Offset: 0x00123635
			public virtual RecipientIdParameter SiteMailboxUsers
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxUsers"] = value;
				}
			}

			// Token: 0x17009DE6 RID: 40422
			// (set) Token: 0x0600CDBE RID: 52670 RVA: 0x00125448 File Offset: 0x00123648
			public virtual DateTime? SiteMailboxClosedTime
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxClosedTime"] = value;
				}
			}

			// Token: 0x17009DE7 RID: 40423
			// (set) Token: 0x0600CDBF RID: 52671 RVA: 0x00125460 File Offset: 0x00123660
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x17009DE8 RID: 40424
			// (set) Token: 0x0600CDC0 RID: 52672 RVA: 0x00125473 File Offset: 0x00123673
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17009DE9 RID: 40425
			// (set) Token: 0x0600CDC1 RID: 52673 RVA: 0x0012548B File Offset: 0x0012368B
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17009DEA RID: 40426
			// (set) Token: 0x0600CDC2 RID: 52674 RVA: 0x001254A3 File Offset: 0x001236A3
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17009DEB RID: 40427
			// (set) Token: 0x0600CDC3 RID: 52675 RVA: 0x001254BB File Offset: 0x001236BB
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17009DEC RID: 40428
			// (set) Token: 0x0600CDC4 RID: 52676 RVA: 0x001254CE File Offset: 0x001236CE
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17009DED RID: 40429
			// (set) Token: 0x0600CDC5 RID: 52677 RVA: 0x001254E1 File Offset: 0x001236E1
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17009DEE RID: 40430
			// (set) Token: 0x0600CDC6 RID: 52678 RVA: 0x001254F4 File Offset: 0x001236F4
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009DEF RID: 40431
			// (set) Token: 0x0600CDC7 RID: 52679 RVA: 0x00125507 File Offset: 0x00123707
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009DF0 RID: 40432
			// (set) Token: 0x0600CDC8 RID: 52680 RVA: 0x0012551A File Offset: 0x0012371A
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17009DF1 RID: 40433
			// (set) Token: 0x0600CDC9 RID: 52681 RVA: 0x0012552D File Offset: 0x0012372D
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17009DF2 RID: 40434
			// (set) Token: 0x0600CDCA RID: 52682 RVA: 0x00125545 File Offset: 0x00123745
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17009DF3 RID: 40435
			// (set) Token: 0x0600CDCB RID: 52683 RVA: 0x00125558 File Offset: 0x00123758
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17009DF4 RID: 40436
			// (set) Token: 0x0600CDCC RID: 52684 RVA: 0x00125570 File Offset: 0x00123770
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17009DF5 RID: 40437
			// (set) Token: 0x0600CDCD RID: 52685 RVA: 0x00125583 File Offset: 0x00123783
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17009DF6 RID: 40438
			// (set) Token: 0x0600CDCE RID: 52686 RVA: 0x0012559B File Offset: 0x0012379B
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17009DF7 RID: 40439
			// (set) Token: 0x0600CDCF RID: 52687 RVA: 0x001255AE File Offset: 0x001237AE
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17009DF8 RID: 40440
			// (set) Token: 0x0600CDD0 RID: 52688 RVA: 0x001255CC File Offset: 0x001237CC
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17009DF9 RID: 40441
			// (set) Token: 0x0600CDD1 RID: 52689 RVA: 0x001255DF File Offset: 0x001237DF
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17009DFA RID: 40442
			// (set) Token: 0x0600CDD2 RID: 52690 RVA: 0x001255F7 File Offset: 0x001237F7
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17009DFB RID: 40443
			// (set) Token: 0x0600CDD3 RID: 52691 RVA: 0x0012560F File Offset: 0x0012380F
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17009DFC RID: 40444
			// (set) Token: 0x0600CDD4 RID: 52692 RVA: 0x00125627 File Offset: 0x00123827
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17009DFD RID: 40445
			// (set) Token: 0x0600CDD5 RID: 52693 RVA: 0x0012563A File Offset: 0x0012383A
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17009DFE RID: 40446
			// (set) Token: 0x0600CDD6 RID: 52694 RVA: 0x00125652 File Offset: 0x00123852
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17009DFF RID: 40447
			// (set) Token: 0x0600CDD7 RID: 52695 RVA: 0x00125665 File Offset: 0x00123865
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17009E00 RID: 40448
			// (set) Token: 0x0600CDD8 RID: 52696 RVA: 0x00125678 File Offset: 0x00123878
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17009E01 RID: 40449
			// (set) Token: 0x0600CDD9 RID: 52697 RVA: 0x00125696 File Offset: 0x00123896
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17009E02 RID: 40450
			// (set) Token: 0x0600CDDA RID: 52698 RVA: 0x001256A9 File Offset: 0x001238A9
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009E03 RID: 40451
			// (set) Token: 0x0600CDDB RID: 52699 RVA: 0x001256C7 File Offset: 0x001238C7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009E04 RID: 40452
			// (set) Token: 0x0600CDDC RID: 52700 RVA: 0x001256DA File Offset: 0x001238DA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009E05 RID: 40453
			// (set) Token: 0x0600CDDD RID: 52701 RVA: 0x001256F2 File Offset: 0x001238F2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009E06 RID: 40454
			// (set) Token: 0x0600CDDE RID: 52702 RVA: 0x0012570A File Offset: 0x0012390A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009E07 RID: 40455
			// (set) Token: 0x0600CDDF RID: 52703 RVA: 0x00125722 File Offset: 0x00123922
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009E08 RID: 40456
			// (set) Token: 0x0600CDE0 RID: 52704 RVA: 0x0012573A File Offset: 0x0012393A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DC6 RID: 3526
		public class FederatedUserParameters : ParametersBase
		{
			// Token: 0x17009E09 RID: 40457
			// (set) Token: 0x0600CDE2 RID: 52706 RVA: 0x0012575A File Offset: 0x0012395A
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x17009E0A RID: 40458
			// (set) Token: 0x0600CDE3 RID: 52707 RVA: 0x0012576D File Offset: 0x0012396D
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17009E0B RID: 40459
			// (set) Token: 0x0600CDE4 RID: 52708 RVA: 0x00125780 File Offset: 0x00123980
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x17009E0C RID: 40460
			// (set) Token: 0x0600CDE5 RID: 52709 RVA: 0x00125793 File Offset: 0x00123993
			public virtual SwitchParameter EvictLiveId
			{
				set
				{
					base.PowerSharpParameters["EvictLiveId"] = value;
				}
			}

			// Token: 0x17009E0D RID: 40461
			// (set) Token: 0x0600CDE6 RID: 52710 RVA: 0x001257AB File Offset: 0x001239AB
			public virtual string FederatedIdentity
			{
				set
				{
					base.PowerSharpParameters["FederatedIdentity"] = value;
				}
			}

			// Token: 0x17009E0E RID: 40462
			// (set) Token: 0x0600CDE7 RID: 52711 RVA: 0x001257BE File Offset: 0x001239BE
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17009E0F RID: 40463
			// (set) Token: 0x0600CDE8 RID: 52712 RVA: 0x001257D1 File Offset: 0x001239D1
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17009E10 RID: 40464
			// (set) Token: 0x0600CDE9 RID: 52713 RVA: 0x001257E4 File Offset: 0x001239E4
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17009E11 RID: 40465
			// (set) Token: 0x0600CDEA RID: 52714 RVA: 0x001257F7 File Offset: 0x001239F7
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17009E12 RID: 40466
			// (set) Token: 0x0600CDEB RID: 52715 RVA: 0x0012580A File Offset: 0x00123A0A
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17009E13 RID: 40467
			// (set) Token: 0x0600CDEC RID: 52716 RVA: 0x0012581D File Offset: 0x00123A1D
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17009E14 RID: 40468
			// (set) Token: 0x0600CDED RID: 52717 RVA: 0x00125830 File Offset: 0x00123A30
			public virtual Guid ExchangeGuid
			{
				set
				{
					base.PowerSharpParameters["ExchangeGuid"] = value;
				}
			}

			// Token: 0x17009E15 RID: 40469
			// (set) Token: 0x0600CDEE RID: 52718 RVA: 0x00125848 File Offset: 0x00123A48
			public virtual string ForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17009E16 RID: 40470
			// (set) Token: 0x0600CDEF RID: 52719 RVA: 0x00125866 File Offset: 0x00123A66
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17009E17 RID: 40471
			// (set) Token: 0x0600CDF0 RID: 52720 RVA: 0x0012587E File Offset: 0x00123A7E
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17009E18 RID: 40472
			// (set) Token: 0x0600CDF1 RID: 52721 RVA: 0x00125891 File Offset: 0x00123A91
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17009E19 RID: 40473
			// (set) Token: 0x0600CDF2 RID: 52722 RVA: 0x001258A4 File Offset: 0x00123AA4
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17009E1A RID: 40474
			// (set) Token: 0x0600CDF3 RID: 52723 RVA: 0x001258BC File Offset: 0x00123ABC
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17009E1B RID: 40475
			// (set) Token: 0x0600CDF4 RID: 52724 RVA: 0x001258CF File Offset: 0x00123ACF
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17009E1C RID: 40476
			// (set) Token: 0x0600CDF5 RID: 52725 RVA: 0x001258E2 File Offset: 0x00123AE2
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17009E1D RID: 40477
			// (set) Token: 0x0600CDF6 RID: 52726 RVA: 0x001258F5 File Offset: 0x00123AF5
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17009E1E RID: 40478
			// (set) Token: 0x0600CDF7 RID: 52727 RVA: 0x00125908 File Offset: 0x00123B08
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17009E1F RID: 40479
			// (set) Token: 0x0600CDF8 RID: 52728 RVA: 0x0012591B File Offset: 0x00123B1B
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17009E20 RID: 40480
			// (set) Token: 0x0600CDF9 RID: 52729 RVA: 0x0012592E File Offset: 0x00123B2E
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17009E21 RID: 40481
			// (set) Token: 0x0600CDFA RID: 52730 RVA: 0x00125941 File Offset: 0x00123B41
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17009E22 RID: 40482
			// (set) Token: 0x0600CDFB RID: 52731 RVA: 0x00125954 File Offset: 0x00123B54
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17009E23 RID: 40483
			// (set) Token: 0x0600CDFC RID: 52732 RVA: 0x00125967 File Offset: 0x00123B67
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17009E24 RID: 40484
			// (set) Token: 0x0600CDFD RID: 52733 RVA: 0x0012597A File Offset: 0x00123B7A
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17009E25 RID: 40485
			// (set) Token: 0x0600CDFE RID: 52734 RVA: 0x0012598D File Offset: 0x00123B8D
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17009E26 RID: 40486
			// (set) Token: 0x0600CDFF RID: 52735 RVA: 0x001259A0 File Offset: 0x00123BA0
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17009E27 RID: 40487
			// (set) Token: 0x0600CE00 RID: 52736 RVA: 0x001259B3 File Offset: 0x00123BB3
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17009E28 RID: 40488
			// (set) Token: 0x0600CE01 RID: 52737 RVA: 0x001259C6 File Offset: 0x00123BC6
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17009E29 RID: 40489
			// (set) Token: 0x0600CE02 RID: 52738 RVA: 0x001259D9 File Offset: 0x00123BD9
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17009E2A RID: 40490
			// (set) Token: 0x0600CE03 RID: 52739 RVA: 0x001259EC File Offset: 0x00123BEC
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17009E2B RID: 40491
			// (set) Token: 0x0600CE04 RID: 52740 RVA: 0x001259FF File Offset: 0x00123BFF
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17009E2C RID: 40492
			// (set) Token: 0x0600CE05 RID: 52741 RVA: 0x00125A12 File Offset: 0x00123C12
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17009E2D RID: 40493
			// (set) Token: 0x0600CE06 RID: 52742 RVA: 0x00125A25 File Offset: 0x00123C25
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17009E2E RID: 40494
			// (set) Token: 0x0600CE07 RID: 52743 RVA: 0x00125A38 File Offset: 0x00123C38
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17009E2F RID: 40495
			// (set) Token: 0x0600CE08 RID: 52744 RVA: 0x00125A4B File Offset: 0x00123C4B
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17009E30 RID: 40496
			// (set) Token: 0x0600CE09 RID: 52745 RVA: 0x00125A63 File Offset: 0x00123C63
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17009E31 RID: 40497
			// (set) Token: 0x0600CE0A RID: 52746 RVA: 0x00125A76 File Offset: 0x00123C76
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17009E32 RID: 40498
			// (set) Token: 0x0600CE0B RID: 52747 RVA: 0x00125A89 File Offset: 0x00123C89
			public virtual RecipientDisplayType? RecipientDisplayType
			{
				set
				{
					base.PowerSharpParameters["RecipientDisplayType"] = value;
				}
			}

			// Token: 0x17009E33 RID: 40499
			// (set) Token: 0x0600CE0C RID: 52748 RVA: 0x00125AA1 File Offset: 0x00123CA1
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17009E34 RID: 40500
			// (set) Token: 0x0600CE0D RID: 52749 RVA: 0x00125AB9 File Offset: 0x00123CB9
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17009E35 RID: 40501
			// (set) Token: 0x0600CE0E RID: 52750 RVA: 0x00125AD1 File Offset: 0x00123CD1
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17009E36 RID: 40502
			// (set) Token: 0x0600CE0F RID: 52751 RVA: 0x00125AE9 File Offset: 0x00123CE9
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17009E37 RID: 40503
			// (set) Token: 0x0600CE10 RID: 52752 RVA: 0x00125B01 File Offset: 0x00123D01
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17009E38 RID: 40504
			// (set) Token: 0x0600CE11 RID: 52753 RVA: 0x00125B14 File Offset: 0x00123D14
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17009E39 RID: 40505
			// (set) Token: 0x0600CE12 RID: 52754 RVA: 0x00125B27 File Offset: 0x00123D27
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17009E3A RID: 40506
			// (set) Token: 0x0600CE13 RID: 52755 RVA: 0x00125B3A File Offset: 0x00123D3A
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17009E3B RID: 40507
			// (set) Token: 0x0600CE14 RID: 52756 RVA: 0x00125B4D File Offset: 0x00123D4D
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17009E3C RID: 40508
			// (set) Token: 0x0600CE15 RID: 52757 RVA: 0x00125B60 File Offset: 0x00123D60
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17009E3D RID: 40509
			// (set) Token: 0x0600CE16 RID: 52758 RVA: 0x00125B73 File Offset: 0x00123D73
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17009E3E RID: 40510
			// (set) Token: 0x0600CE17 RID: 52759 RVA: 0x00125B8B File Offset: 0x00123D8B
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17009E3F RID: 40511
			// (set) Token: 0x0600CE18 RID: 52760 RVA: 0x00125B9E File Offset: 0x00123D9E
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17009E40 RID: 40512
			// (set) Token: 0x0600CE19 RID: 52761 RVA: 0x00125BB1 File Offset: 0x00123DB1
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17009E41 RID: 40513
			// (set) Token: 0x0600CE1A RID: 52762 RVA: 0x00125BC4 File Offset: 0x00123DC4
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17009E42 RID: 40514
			// (set) Token: 0x0600CE1B RID: 52763 RVA: 0x00125BD7 File Offset: 0x00123DD7
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17009E43 RID: 40515
			// (set) Token: 0x0600CE1C RID: 52764 RVA: 0x00125BEA File Offset: 0x00123DEA
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17009E44 RID: 40516
			// (set) Token: 0x0600CE1D RID: 52765 RVA: 0x00125C08 File Offset: 0x00123E08
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17009E45 RID: 40517
			// (set) Token: 0x0600CE1E RID: 52766 RVA: 0x00125C1B File Offset: 0x00123E1B
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17009E46 RID: 40518
			// (set) Token: 0x0600CE1F RID: 52767 RVA: 0x00125C2E File Offset: 0x00123E2E
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17009E47 RID: 40519
			// (set) Token: 0x0600CE20 RID: 52768 RVA: 0x00125C41 File Offset: 0x00123E41
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17009E48 RID: 40520
			// (set) Token: 0x0600CE21 RID: 52769 RVA: 0x00125C54 File Offset: 0x00123E54
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17009E49 RID: 40521
			// (set) Token: 0x0600CE22 RID: 52770 RVA: 0x00125C67 File Offset: 0x00123E67
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17009E4A RID: 40522
			// (set) Token: 0x0600CE23 RID: 52771 RVA: 0x00125C7A File Offset: 0x00123E7A
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17009E4B RID: 40523
			// (set) Token: 0x0600CE24 RID: 52772 RVA: 0x00125C8D File Offset: 0x00123E8D
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17009E4C RID: 40524
			// (set) Token: 0x0600CE25 RID: 52773 RVA: 0x00125CA0 File Offset: 0x00123EA0
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17009E4D RID: 40525
			// (set) Token: 0x0600CE26 RID: 52774 RVA: 0x00125CB3 File Offset: 0x00123EB3
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17009E4E RID: 40526
			// (set) Token: 0x0600CE27 RID: 52775 RVA: 0x00125CC6 File Offset: 0x00123EC6
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17009E4F RID: 40527
			// (set) Token: 0x0600CE28 RID: 52776 RVA: 0x00125CD9 File Offset: 0x00123ED9
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17009E50 RID: 40528
			// (set) Token: 0x0600CE29 RID: 52777 RVA: 0x00125CEC File Offset: 0x00123EEC
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17009E51 RID: 40529
			// (set) Token: 0x0600CE2A RID: 52778 RVA: 0x00125CFF File Offset: 0x00123EFF
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17009E52 RID: 40530
			// (set) Token: 0x0600CE2B RID: 52779 RVA: 0x00125D17 File Offset: 0x00123F17
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17009E53 RID: 40531
			// (set) Token: 0x0600CE2C RID: 52780 RVA: 0x00125D2A File Offset: 0x00123F2A
			public virtual SecurityIdentifier MasterAccountSid
			{
				set
				{
					base.PowerSharpParameters["MasterAccountSid"] = value;
				}
			}

			// Token: 0x17009E54 RID: 40532
			// (set) Token: 0x0600CE2D RID: 52781 RVA: 0x00125D3D File Offset: 0x00123F3D
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17009E55 RID: 40533
			// (set) Token: 0x0600CE2E RID: 52782 RVA: 0x00125D55 File Offset: 0x00123F55
			public virtual string IntendedMailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["IntendedMailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17009E56 RID: 40534
			// (set) Token: 0x0600CE2F RID: 52783 RVA: 0x00125D73 File Offset: 0x00123F73
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17009E57 RID: 40535
			// (set) Token: 0x0600CE30 RID: 52784 RVA: 0x00125D8B File Offset: 0x00123F8B
			public virtual ExchangeResourceType? ResourceType
			{
				set
				{
					base.PowerSharpParameters["ResourceType"] = value;
				}
			}

			// Token: 0x17009E58 RID: 40536
			// (set) Token: 0x0600CE31 RID: 52785 RVA: 0x00125DA3 File Offset: 0x00123FA3
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17009E59 RID: 40537
			// (set) Token: 0x0600CE32 RID: 52786 RVA: 0x00125DBB File Offset: 0x00123FBB
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17009E5A RID: 40538
			// (set) Token: 0x0600CE33 RID: 52787 RVA: 0x00125DCE File Offset: 0x00123FCE
			public virtual MultiValuedProperty<string> ResourceMetaData
			{
				set
				{
					base.PowerSharpParameters["ResourceMetaData"] = value;
				}
			}

			// Token: 0x17009E5B RID: 40539
			// (set) Token: 0x0600CE34 RID: 52788 RVA: 0x00125DE1 File Offset: 0x00123FE1
			public virtual string ResourcePropertiesDisplay
			{
				set
				{
					base.PowerSharpParameters["ResourcePropertiesDisplay"] = value;
				}
			}

			// Token: 0x17009E5C RID: 40540
			// (set) Token: 0x0600CE35 RID: 52789 RVA: 0x00125DF4 File Offset: 0x00123FF4
			public virtual MultiValuedProperty<string> ResourceSearchProperties
			{
				set
				{
					base.PowerSharpParameters["ResourceSearchProperties"] = value;
				}
			}

			// Token: 0x17009E5D RID: 40541
			// (set) Token: 0x0600CE36 RID: 52790 RVA: 0x00125E07 File Offset: 0x00124007
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17009E5E RID: 40542
			// (set) Token: 0x0600CE37 RID: 52791 RVA: 0x00125E1F File Offset: 0x0012401F
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17009E5F RID: 40543
			// (set) Token: 0x0600CE38 RID: 52792 RVA: 0x00125E32 File Offset: 0x00124032
			public virtual bool IsCalculatedTargetAddress
			{
				set
				{
					base.PowerSharpParameters["IsCalculatedTargetAddress"] = value;
				}
			}

			// Token: 0x17009E60 RID: 40544
			// (set) Token: 0x0600CE39 RID: 52793 RVA: 0x00125E4A File Offset: 0x0012404A
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17009E61 RID: 40545
			// (set) Token: 0x0600CE3A RID: 52794 RVA: 0x00125E5D File Offset: 0x0012405D
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17009E62 RID: 40546
			// (set) Token: 0x0600CE3B RID: 52795 RVA: 0x00125E75 File Offset: 0x00124075
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17009E63 RID: 40547
			// (set) Token: 0x0600CE3C RID: 52796 RVA: 0x00125E88 File Offset: 0x00124088
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17009E64 RID: 40548
			// (set) Token: 0x0600CE3D RID: 52797 RVA: 0x00125E9B File Offset: 0x0012409B
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17009E65 RID: 40549
			// (set) Token: 0x0600CE3E RID: 52798 RVA: 0x00125EAE File Offset: 0x001240AE
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17009E66 RID: 40550
			// (set) Token: 0x0600CE3F RID: 52799 RVA: 0x00125EC6 File Offset: 0x001240C6
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17009E67 RID: 40551
			// (set) Token: 0x0600CE40 RID: 52800 RVA: 0x00125EDE File Offset: 0x001240DE
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17009E68 RID: 40552
			// (set) Token: 0x0600CE41 RID: 52801 RVA: 0x00125EF1 File Offset: 0x001240F1
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17009E69 RID: 40553
			// (set) Token: 0x0600CE42 RID: 52802 RVA: 0x00125F04 File Offset: 0x00124104
			public virtual ElcMailboxFlags ElcMailboxFlags
			{
				set
				{
					base.PowerSharpParameters["ElcMailboxFlags"] = value;
				}
			}

			// Token: 0x17009E6A RID: 40554
			// (set) Token: 0x0600CE43 RID: 52803 RVA: 0x00125F1C File Offset: 0x0012411C
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17009E6B RID: 40555
			// (set) Token: 0x0600CE44 RID: 52804 RVA: 0x00125F34 File Offset: 0x00124134
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17009E6C RID: 40556
			// (set) Token: 0x0600CE45 RID: 52805 RVA: 0x00125F4C File Offset: 0x0012414C
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x17009E6D RID: 40557
			// (set) Token: 0x0600CE46 RID: 52806 RVA: 0x00125F5F File Offset: 0x0012415F
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x17009E6E RID: 40558
			// (set) Token: 0x0600CE47 RID: 52807 RVA: 0x00125F72 File Offset: 0x00124172
			public virtual bool MailboxAuditEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditEnabled"] = value;
				}
			}

			// Token: 0x17009E6F RID: 40559
			// (set) Token: 0x0600CE48 RID: 52808 RVA: 0x00125F8A File Offset: 0x0012418A
			public virtual EnhancedTimeSpan MailboxAuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x17009E70 RID: 40560
			// (set) Token: 0x0600CE49 RID: 52809 RVA: 0x00125FA2 File Offset: 0x001241A2
			public virtual MailboxAuditOperations AuditAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditAdminOperations"] = value;
				}
			}

			// Token: 0x17009E71 RID: 40561
			// (set) Token: 0x0600CE4A RID: 52810 RVA: 0x00125FBA File Offset: 0x001241BA
			public virtual MailboxAuditOperations AuditDelegateAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateAdminOperations"] = value;
				}
			}

			// Token: 0x17009E72 RID: 40562
			// (set) Token: 0x0600CE4B RID: 52811 RVA: 0x00125FD2 File Offset: 0x001241D2
			public virtual MailboxAuditOperations AuditDelegateOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateOperations"] = value;
				}
			}

			// Token: 0x17009E73 RID: 40563
			// (set) Token: 0x0600CE4C RID: 52812 RVA: 0x00125FEA File Offset: 0x001241EA
			public virtual MailboxAuditOperations AuditOwnerOperations
			{
				set
				{
					base.PowerSharpParameters["AuditOwnerOperations"] = value;
				}
			}

			// Token: 0x17009E74 RID: 40564
			// (set) Token: 0x0600CE4D RID: 52813 RVA: 0x00126002 File Offset: 0x00124202
			public virtual bool BypassAudit
			{
				set
				{
					base.PowerSharpParameters["BypassAudit"] = value;
				}
			}

			// Token: 0x17009E75 RID: 40565
			// (set) Token: 0x0600CE4E RID: 52814 RVA: 0x0012601A File Offset: 0x0012421A
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x17009E76 RID: 40566
			// (set) Token: 0x0600CE4F RID: 52815 RVA: 0x0012602D File Offset: 0x0012422D
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x17009E77 RID: 40567
			// (set) Token: 0x0600CE50 RID: 52816 RVA: 0x00126045 File Offset: 0x00124245
			public virtual RecipientIdParameter SiteMailboxOwners
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxOwners"] = value;
				}
			}

			// Token: 0x17009E78 RID: 40568
			// (set) Token: 0x0600CE51 RID: 52817 RVA: 0x00126058 File Offset: 0x00124258
			public virtual RecipientIdParameter SiteMailboxUsers
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxUsers"] = value;
				}
			}

			// Token: 0x17009E79 RID: 40569
			// (set) Token: 0x0600CE52 RID: 52818 RVA: 0x0012606B File Offset: 0x0012426B
			public virtual DateTime? SiteMailboxClosedTime
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxClosedTime"] = value;
				}
			}

			// Token: 0x17009E7A RID: 40570
			// (set) Token: 0x0600CE53 RID: 52819 RVA: 0x00126083 File Offset: 0x00124283
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x17009E7B RID: 40571
			// (set) Token: 0x0600CE54 RID: 52820 RVA: 0x00126096 File Offset: 0x00124296
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17009E7C RID: 40572
			// (set) Token: 0x0600CE55 RID: 52821 RVA: 0x001260AE File Offset: 0x001242AE
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17009E7D RID: 40573
			// (set) Token: 0x0600CE56 RID: 52822 RVA: 0x001260C6 File Offset: 0x001242C6
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17009E7E RID: 40574
			// (set) Token: 0x0600CE57 RID: 52823 RVA: 0x001260DE File Offset: 0x001242DE
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17009E7F RID: 40575
			// (set) Token: 0x0600CE58 RID: 52824 RVA: 0x001260F1 File Offset: 0x001242F1
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17009E80 RID: 40576
			// (set) Token: 0x0600CE59 RID: 52825 RVA: 0x00126104 File Offset: 0x00124304
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17009E81 RID: 40577
			// (set) Token: 0x0600CE5A RID: 52826 RVA: 0x00126117 File Offset: 0x00124317
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009E82 RID: 40578
			// (set) Token: 0x0600CE5B RID: 52827 RVA: 0x0012612A File Offset: 0x0012432A
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009E83 RID: 40579
			// (set) Token: 0x0600CE5C RID: 52828 RVA: 0x0012613D File Offset: 0x0012433D
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17009E84 RID: 40580
			// (set) Token: 0x0600CE5D RID: 52829 RVA: 0x00126150 File Offset: 0x00124350
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17009E85 RID: 40581
			// (set) Token: 0x0600CE5E RID: 52830 RVA: 0x00126168 File Offset: 0x00124368
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17009E86 RID: 40582
			// (set) Token: 0x0600CE5F RID: 52831 RVA: 0x0012617B File Offset: 0x0012437B
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17009E87 RID: 40583
			// (set) Token: 0x0600CE60 RID: 52832 RVA: 0x00126193 File Offset: 0x00124393
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17009E88 RID: 40584
			// (set) Token: 0x0600CE61 RID: 52833 RVA: 0x001261A6 File Offset: 0x001243A6
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17009E89 RID: 40585
			// (set) Token: 0x0600CE62 RID: 52834 RVA: 0x001261BE File Offset: 0x001243BE
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17009E8A RID: 40586
			// (set) Token: 0x0600CE63 RID: 52835 RVA: 0x001261D1 File Offset: 0x001243D1
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17009E8B RID: 40587
			// (set) Token: 0x0600CE64 RID: 52836 RVA: 0x001261EF File Offset: 0x001243EF
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17009E8C RID: 40588
			// (set) Token: 0x0600CE65 RID: 52837 RVA: 0x00126202 File Offset: 0x00124402
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17009E8D RID: 40589
			// (set) Token: 0x0600CE66 RID: 52838 RVA: 0x0012621A File Offset: 0x0012441A
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17009E8E RID: 40590
			// (set) Token: 0x0600CE67 RID: 52839 RVA: 0x00126232 File Offset: 0x00124432
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17009E8F RID: 40591
			// (set) Token: 0x0600CE68 RID: 52840 RVA: 0x0012624A File Offset: 0x0012444A
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17009E90 RID: 40592
			// (set) Token: 0x0600CE69 RID: 52841 RVA: 0x0012625D File Offset: 0x0012445D
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17009E91 RID: 40593
			// (set) Token: 0x0600CE6A RID: 52842 RVA: 0x00126275 File Offset: 0x00124475
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17009E92 RID: 40594
			// (set) Token: 0x0600CE6B RID: 52843 RVA: 0x00126288 File Offset: 0x00124488
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17009E93 RID: 40595
			// (set) Token: 0x0600CE6C RID: 52844 RVA: 0x0012629B File Offset: 0x0012449B
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17009E94 RID: 40596
			// (set) Token: 0x0600CE6D RID: 52845 RVA: 0x001262B9 File Offset: 0x001244B9
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17009E95 RID: 40597
			// (set) Token: 0x0600CE6E RID: 52846 RVA: 0x001262CC File Offset: 0x001244CC
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009E96 RID: 40598
			// (set) Token: 0x0600CE6F RID: 52847 RVA: 0x001262EA File Offset: 0x001244EA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009E97 RID: 40599
			// (set) Token: 0x0600CE70 RID: 52848 RVA: 0x001262FD File Offset: 0x001244FD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009E98 RID: 40600
			// (set) Token: 0x0600CE71 RID: 52849 RVA: 0x00126315 File Offset: 0x00124515
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009E99 RID: 40601
			// (set) Token: 0x0600CE72 RID: 52850 RVA: 0x0012632D File Offset: 0x0012452D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009E9A RID: 40602
			// (set) Token: 0x0600CE73 RID: 52851 RVA: 0x00126345 File Offset: 0x00124545
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009E9B RID: 40603
			// (set) Token: 0x0600CE74 RID: 52852 RVA: 0x0012635D File Offset: 0x0012455D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DC7 RID: 3527
		public class WindowsLiveCustomDomainsParameters : ParametersBase
		{
			// Token: 0x17009E9C RID: 40604
			// (set) Token: 0x0600CE76 RID: 52854 RVA: 0x0012637D File Offset: 0x0012457D
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x17009E9D RID: 40605
			// (set) Token: 0x0600CE77 RID: 52855 RVA: 0x00126390 File Offset: 0x00124590
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17009E9E RID: 40606
			// (set) Token: 0x0600CE78 RID: 52856 RVA: 0x001263A3 File Offset: 0x001245A3
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17009E9F RID: 40607
			// (set) Token: 0x0600CE79 RID: 52857 RVA: 0x001263B6 File Offset: 0x001245B6
			public virtual SwitchParameter UseExistingLiveId
			{
				set
				{
					base.PowerSharpParameters["UseExistingLiveId"] = value;
				}
			}

			// Token: 0x17009EA0 RID: 40608
			// (set) Token: 0x0600CE7A RID: 52858 RVA: 0x001263CE File Offset: 0x001245CE
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x17009EA1 RID: 40609
			// (set) Token: 0x0600CE7B RID: 52859 RVA: 0x001263E1 File Offset: 0x001245E1
			public virtual SwitchParameter BypassLiveId
			{
				set
				{
					base.PowerSharpParameters["BypassLiveId"] = value;
				}
			}

			// Token: 0x17009EA2 RID: 40610
			// (set) Token: 0x0600CE7C RID: 52860 RVA: 0x001263F9 File Offset: 0x001245F9
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17009EA3 RID: 40611
			// (set) Token: 0x0600CE7D RID: 52861 RVA: 0x0012640C File Offset: 0x0012460C
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17009EA4 RID: 40612
			// (set) Token: 0x0600CE7E RID: 52862 RVA: 0x0012641F File Offset: 0x0012461F
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17009EA5 RID: 40613
			// (set) Token: 0x0600CE7F RID: 52863 RVA: 0x00126432 File Offset: 0x00124632
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17009EA6 RID: 40614
			// (set) Token: 0x0600CE80 RID: 52864 RVA: 0x00126445 File Offset: 0x00124645
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17009EA7 RID: 40615
			// (set) Token: 0x0600CE81 RID: 52865 RVA: 0x00126458 File Offset: 0x00124658
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17009EA8 RID: 40616
			// (set) Token: 0x0600CE82 RID: 52866 RVA: 0x0012646B File Offset: 0x0012466B
			public virtual Guid ExchangeGuid
			{
				set
				{
					base.PowerSharpParameters["ExchangeGuid"] = value;
				}
			}

			// Token: 0x17009EA9 RID: 40617
			// (set) Token: 0x0600CE83 RID: 52867 RVA: 0x00126483 File Offset: 0x00124683
			public virtual string ForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17009EAA RID: 40618
			// (set) Token: 0x0600CE84 RID: 52868 RVA: 0x001264A1 File Offset: 0x001246A1
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17009EAB RID: 40619
			// (set) Token: 0x0600CE85 RID: 52869 RVA: 0x001264B9 File Offset: 0x001246B9
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17009EAC RID: 40620
			// (set) Token: 0x0600CE86 RID: 52870 RVA: 0x001264CC File Offset: 0x001246CC
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17009EAD RID: 40621
			// (set) Token: 0x0600CE87 RID: 52871 RVA: 0x001264DF File Offset: 0x001246DF
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17009EAE RID: 40622
			// (set) Token: 0x0600CE88 RID: 52872 RVA: 0x001264F7 File Offset: 0x001246F7
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17009EAF RID: 40623
			// (set) Token: 0x0600CE89 RID: 52873 RVA: 0x0012650A File Offset: 0x0012470A
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17009EB0 RID: 40624
			// (set) Token: 0x0600CE8A RID: 52874 RVA: 0x0012651D File Offset: 0x0012471D
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17009EB1 RID: 40625
			// (set) Token: 0x0600CE8B RID: 52875 RVA: 0x00126530 File Offset: 0x00124730
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17009EB2 RID: 40626
			// (set) Token: 0x0600CE8C RID: 52876 RVA: 0x00126543 File Offset: 0x00124743
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17009EB3 RID: 40627
			// (set) Token: 0x0600CE8D RID: 52877 RVA: 0x00126556 File Offset: 0x00124756
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17009EB4 RID: 40628
			// (set) Token: 0x0600CE8E RID: 52878 RVA: 0x00126569 File Offset: 0x00124769
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17009EB5 RID: 40629
			// (set) Token: 0x0600CE8F RID: 52879 RVA: 0x0012657C File Offset: 0x0012477C
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17009EB6 RID: 40630
			// (set) Token: 0x0600CE90 RID: 52880 RVA: 0x0012658F File Offset: 0x0012478F
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17009EB7 RID: 40631
			// (set) Token: 0x0600CE91 RID: 52881 RVA: 0x001265A2 File Offset: 0x001247A2
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17009EB8 RID: 40632
			// (set) Token: 0x0600CE92 RID: 52882 RVA: 0x001265B5 File Offset: 0x001247B5
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17009EB9 RID: 40633
			// (set) Token: 0x0600CE93 RID: 52883 RVA: 0x001265C8 File Offset: 0x001247C8
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17009EBA RID: 40634
			// (set) Token: 0x0600CE94 RID: 52884 RVA: 0x001265DB File Offset: 0x001247DB
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17009EBB RID: 40635
			// (set) Token: 0x0600CE95 RID: 52885 RVA: 0x001265EE File Offset: 0x001247EE
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17009EBC RID: 40636
			// (set) Token: 0x0600CE96 RID: 52886 RVA: 0x00126601 File Offset: 0x00124801
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17009EBD RID: 40637
			// (set) Token: 0x0600CE97 RID: 52887 RVA: 0x00126614 File Offset: 0x00124814
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17009EBE RID: 40638
			// (set) Token: 0x0600CE98 RID: 52888 RVA: 0x00126627 File Offset: 0x00124827
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17009EBF RID: 40639
			// (set) Token: 0x0600CE99 RID: 52889 RVA: 0x0012663A File Offset: 0x0012483A
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17009EC0 RID: 40640
			// (set) Token: 0x0600CE9A RID: 52890 RVA: 0x0012664D File Offset: 0x0012484D
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17009EC1 RID: 40641
			// (set) Token: 0x0600CE9B RID: 52891 RVA: 0x00126660 File Offset: 0x00124860
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17009EC2 RID: 40642
			// (set) Token: 0x0600CE9C RID: 52892 RVA: 0x00126673 File Offset: 0x00124873
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17009EC3 RID: 40643
			// (set) Token: 0x0600CE9D RID: 52893 RVA: 0x00126686 File Offset: 0x00124886
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17009EC4 RID: 40644
			// (set) Token: 0x0600CE9E RID: 52894 RVA: 0x0012669E File Offset: 0x0012489E
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17009EC5 RID: 40645
			// (set) Token: 0x0600CE9F RID: 52895 RVA: 0x001266B1 File Offset: 0x001248B1
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17009EC6 RID: 40646
			// (set) Token: 0x0600CEA0 RID: 52896 RVA: 0x001266C4 File Offset: 0x001248C4
			public virtual RecipientDisplayType? RecipientDisplayType
			{
				set
				{
					base.PowerSharpParameters["RecipientDisplayType"] = value;
				}
			}

			// Token: 0x17009EC7 RID: 40647
			// (set) Token: 0x0600CEA1 RID: 52897 RVA: 0x001266DC File Offset: 0x001248DC
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17009EC8 RID: 40648
			// (set) Token: 0x0600CEA2 RID: 52898 RVA: 0x001266F4 File Offset: 0x001248F4
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17009EC9 RID: 40649
			// (set) Token: 0x0600CEA3 RID: 52899 RVA: 0x0012670C File Offset: 0x0012490C
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17009ECA RID: 40650
			// (set) Token: 0x0600CEA4 RID: 52900 RVA: 0x00126724 File Offset: 0x00124924
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17009ECB RID: 40651
			// (set) Token: 0x0600CEA5 RID: 52901 RVA: 0x0012673C File Offset: 0x0012493C
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17009ECC RID: 40652
			// (set) Token: 0x0600CEA6 RID: 52902 RVA: 0x0012674F File Offset: 0x0012494F
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17009ECD RID: 40653
			// (set) Token: 0x0600CEA7 RID: 52903 RVA: 0x00126762 File Offset: 0x00124962
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17009ECE RID: 40654
			// (set) Token: 0x0600CEA8 RID: 52904 RVA: 0x00126775 File Offset: 0x00124975
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17009ECF RID: 40655
			// (set) Token: 0x0600CEA9 RID: 52905 RVA: 0x00126788 File Offset: 0x00124988
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17009ED0 RID: 40656
			// (set) Token: 0x0600CEAA RID: 52906 RVA: 0x0012679B File Offset: 0x0012499B
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17009ED1 RID: 40657
			// (set) Token: 0x0600CEAB RID: 52907 RVA: 0x001267AE File Offset: 0x001249AE
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17009ED2 RID: 40658
			// (set) Token: 0x0600CEAC RID: 52908 RVA: 0x001267C6 File Offset: 0x001249C6
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17009ED3 RID: 40659
			// (set) Token: 0x0600CEAD RID: 52909 RVA: 0x001267D9 File Offset: 0x001249D9
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17009ED4 RID: 40660
			// (set) Token: 0x0600CEAE RID: 52910 RVA: 0x001267EC File Offset: 0x001249EC
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17009ED5 RID: 40661
			// (set) Token: 0x0600CEAF RID: 52911 RVA: 0x001267FF File Offset: 0x001249FF
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17009ED6 RID: 40662
			// (set) Token: 0x0600CEB0 RID: 52912 RVA: 0x00126812 File Offset: 0x00124A12
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17009ED7 RID: 40663
			// (set) Token: 0x0600CEB1 RID: 52913 RVA: 0x00126825 File Offset: 0x00124A25
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17009ED8 RID: 40664
			// (set) Token: 0x0600CEB2 RID: 52914 RVA: 0x00126843 File Offset: 0x00124A43
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17009ED9 RID: 40665
			// (set) Token: 0x0600CEB3 RID: 52915 RVA: 0x00126856 File Offset: 0x00124A56
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17009EDA RID: 40666
			// (set) Token: 0x0600CEB4 RID: 52916 RVA: 0x00126869 File Offset: 0x00124A69
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17009EDB RID: 40667
			// (set) Token: 0x0600CEB5 RID: 52917 RVA: 0x0012687C File Offset: 0x00124A7C
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17009EDC RID: 40668
			// (set) Token: 0x0600CEB6 RID: 52918 RVA: 0x0012688F File Offset: 0x00124A8F
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17009EDD RID: 40669
			// (set) Token: 0x0600CEB7 RID: 52919 RVA: 0x001268A2 File Offset: 0x00124AA2
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17009EDE RID: 40670
			// (set) Token: 0x0600CEB8 RID: 52920 RVA: 0x001268B5 File Offset: 0x00124AB5
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17009EDF RID: 40671
			// (set) Token: 0x0600CEB9 RID: 52921 RVA: 0x001268C8 File Offset: 0x00124AC8
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17009EE0 RID: 40672
			// (set) Token: 0x0600CEBA RID: 52922 RVA: 0x001268DB File Offset: 0x00124ADB
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17009EE1 RID: 40673
			// (set) Token: 0x0600CEBB RID: 52923 RVA: 0x001268EE File Offset: 0x00124AEE
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17009EE2 RID: 40674
			// (set) Token: 0x0600CEBC RID: 52924 RVA: 0x00126901 File Offset: 0x00124B01
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17009EE3 RID: 40675
			// (set) Token: 0x0600CEBD RID: 52925 RVA: 0x00126914 File Offset: 0x00124B14
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17009EE4 RID: 40676
			// (set) Token: 0x0600CEBE RID: 52926 RVA: 0x00126927 File Offset: 0x00124B27
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17009EE5 RID: 40677
			// (set) Token: 0x0600CEBF RID: 52927 RVA: 0x0012693A File Offset: 0x00124B3A
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17009EE6 RID: 40678
			// (set) Token: 0x0600CEC0 RID: 52928 RVA: 0x00126952 File Offset: 0x00124B52
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17009EE7 RID: 40679
			// (set) Token: 0x0600CEC1 RID: 52929 RVA: 0x00126965 File Offset: 0x00124B65
			public virtual SecurityIdentifier MasterAccountSid
			{
				set
				{
					base.PowerSharpParameters["MasterAccountSid"] = value;
				}
			}

			// Token: 0x17009EE8 RID: 40680
			// (set) Token: 0x0600CEC2 RID: 52930 RVA: 0x00126978 File Offset: 0x00124B78
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17009EE9 RID: 40681
			// (set) Token: 0x0600CEC3 RID: 52931 RVA: 0x00126990 File Offset: 0x00124B90
			public virtual string IntendedMailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["IntendedMailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17009EEA RID: 40682
			// (set) Token: 0x0600CEC4 RID: 52932 RVA: 0x001269AE File Offset: 0x00124BAE
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17009EEB RID: 40683
			// (set) Token: 0x0600CEC5 RID: 52933 RVA: 0x001269C6 File Offset: 0x00124BC6
			public virtual ExchangeResourceType? ResourceType
			{
				set
				{
					base.PowerSharpParameters["ResourceType"] = value;
				}
			}

			// Token: 0x17009EEC RID: 40684
			// (set) Token: 0x0600CEC6 RID: 52934 RVA: 0x001269DE File Offset: 0x00124BDE
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17009EED RID: 40685
			// (set) Token: 0x0600CEC7 RID: 52935 RVA: 0x001269F6 File Offset: 0x00124BF6
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17009EEE RID: 40686
			// (set) Token: 0x0600CEC8 RID: 52936 RVA: 0x00126A09 File Offset: 0x00124C09
			public virtual MultiValuedProperty<string> ResourceMetaData
			{
				set
				{
					base.PowerSharpParameters["ResourceMetaData"] = value;
				}
			}

			// Token: 0x17009EEF RID: 40687
			// (set) Token: 0x0600CEC9 RID: 52937 RVA: 0x00126A1C File Offset: 0x00124C1C
			public virtual string ResourcePropertiesDisplay
			{
				set
				{
					base.PowerSharpParameters["ResourcePropertiesDisplay"] = value;
				}
			}

			// Token: 0x17009EF0 RID: 40688
			// (set) Token: 0x0600CECA RID: 52938 RVA: 0x00126A2F File Offset: 0x00124C2F
			public virtual MultiValuedProperty<string> ResourceSearchProperties
			{
				set
				{
					base.PowerSharpParameters["ResourceSearchProperties"] = value;
				}
			}

			// Token: 0x17009EF1 RID: 40689
			// (set) Token: 0x0600CECB RID: 52939 RVA: 0x00126A42 File Offset: 0x00124C42
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17009EF2 RID: 40690
			// (set) Token: 0x0600CECC RID: 52940 RVA: 0x00126A5A File Offset: 0x00124C5A
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17009EF3 RID: 40691
			// (set) Token: 0x0600CECD RID: 52941 RVA: 0x00126A6D File Offset: 0x00124C6D
			public virtual bool IsCalculatedTargetAddress
			{
				set
				{
					base.PowerSharpParameters["IsCalculatedTargetAddress"] = value;
				}
			}

			// Token: 0x17009EF4 RID: 40692
			// (set) Token: 0x0600CECE RID: 52942 RVA: 0x00126A85 File Offset: 0x00124C85
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17009EF5 RID: 40693
			// (set) Token: 0x0600CECF RID: 52943 RVA: 0x00126A98 File Offset: 0x00124C98
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17009EF6 RID: 40694
			// (set) Token: 0x0600CED0 RID: 52944 RVA: 0x00126AB0 File Offset: 0x00124CB0
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17009EF7 RID: 40695
			// (set) Token: 0x0600CED1 RID: 52945 RVA: 0x00126AC3 File Offset: 0x00124CC3
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17009EF8 RID: 40696
			// (set) Token: 0x0600CED2 RID: 52946 RVA: 0x00126AD6 File Offset: 0x00124CD6
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17009EF9 RID: 40697
			// (set) Token: 0x0600CED3 RID: 52947 RVA: 0x00126AE9 File Offset: 0x00124CE9
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17009EFA RID: 40698
			// (set) Token: 0x0600CED4 RID: 52948 RVA: 0x00126B01 File Offset: 0x00124D01
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17009EFB RID: 40699
			// (set) Token: 0x0600CED5 RID: 52949 RVA: 0x00126B19 File Offset: 0x00124D19
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17009EFC RID: 40700
			// (set) Token: 0x0600CED6 RID: 52950 RVA: 0x00126B2C File Offset: 0x00124D2C
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17009EFD RID: 40701
			// (set) Token: 0x0600CED7 RID: 52951 RVA: 0x00126B3F File Offset: 0x00124D3F
			public virtual ElcMailboxFlags ElcMailboxFlags
			{
				set
				{
					base.PowerSharpParameters["ElcMailboxFlags"] = value;
				}
			}

			// Token: 0x17009EFE RID: 40702
			// (set) Token: 0x0600CED8 RID: 52952 RVA: 0x00126B57 File Offset: 0x00124D57
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17009EFF RID: 40703
			// (set) Token: 0x0600CED9 RID: 52953 RVA: 0x00126B6F File Offset: 0x00124D6F
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17009F00 RID: 40704
			// (set) Token: 0x0600CEDA RID: 52954 RVA: 0x00126B87 File Offset: 0x00124D87
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x17009F01 RID: 40705
			// (set) Token: 0x0600CEDB RID: 52955 RVA: 0x00126B9A File Offset: 0x00124D9A
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x17009F02 RID: 40706
			// (set) Token: 0x0600CEDC RID: 52956 RVA: 0x00126BAD File Offset: 0x00124DAD
			public virtual bool MailboxAuditEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditEnabled"] = value;
				}
			}

			// Token: 0x17009F03 RID: 40707
			// (set) Token: 0x0600CEDD RID: 52957 RVA: 0x00126BC5 File Offset: 0x00124DC5
			public virtual EnhancedTimeSpan MailboxAuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x17009F04 RID: 40708
			// (set) Token: 0x0600CEDE RID: 52958 RVA: 0x00126BDD File Offset: 0x00124DDD
			public virtual MailboxAuditOperations AuditAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditAdminOperations"] = value;
				}
			}

			// Token: 0x17009F05 RID: 40709
			// (set) Token: 0x0600CEDF RID: 52959 RVA: 0x00126BF5 File Offset: 0x00124DF5
			public virtual MailboxAuditOperations AuditDelegateAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateAdminOperations"] = value;
				}
			}

			// Token: 0x17009F06 RID: 40710
			// (set) Token: 0x0600CEE0 RID: 52960 RVA: 0x00126C0D File Offset: 0x00124E0D
			public virtual MailboxAuditOperations AuditDelegateOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateOperations"] = value;
				}
			}

			// Token: 0x17009F07 RID: 40711
			// (set) Token: 0x0600CEE1 RID: 52961 RVA: 0x00126C25 File Offset: 0x00124E25
			public virtual MailboxAuditOperations AuditOwnerOperations
			{
				set
				{
					base.PowerSharpParameters["AuditOwnerOperations"] = value;
				}
			}

			// Token: 0x17009F08 RID: 40712
			// (set) Token: 0x0600CEE2 RID: 52962 RVA: 0x00126C3D File Offset: 0x00124E3D
			public virtual bool BypassAudit
			{
				set
				{
					base.PowerSharpParameters["BypassAudit"] = value;
				}
			}

			// Token: 0x17009F09 RID: 40713
			// (set) Token: 0x0600CEE3 RID: 52963 RVA: 0x00126C55 File Offset: 0x00124E55
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x17009F0A RID: 40714
			// (set) Token: 0x0600CEE4 RID: 52964 RVA: 0x00126C68 File Offset: 0x00124E68
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x17009F0B RID: 40715
			// (set) Token: 0x0600CEE5 RID: 52965 RVA: 0x00126C80 File Offset: 0x00124E80
			public virtual RecipientIdParameter SiteMailboxOwners
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxOwners"] = value;
				}
			}

			// Token: 0x17009F0C RID: 40716
			// (set) Token: 0x0600CEE6 RID: 52966 RVA: 0x00126C93 File Offset: 0x00124E93
			public virtual RecipientIdParameter SiteMailboxUsers
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxUsers"] = value;
				}
			}

			// Token: 0x17009F0D RID: 40717
			// (set) Token: 0x0600CEE7 RID: 52967 RVA: 0x00126CA6 File Offset: 0x00124EA6
			public virtual DateTime? SiteMailboxClosedTime
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxClosedTime"] = value;
				}
			}

			// Token: 0x17009F0E RID: 40718
			// (set) Token: 0x0600CEE8 RID: 52968 RVA: 0x00126CBE File Offset: 0x00124EBE
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x17009F0F RID: 40719
			// (set) Token: 0x0600CEE9 RID: 52969 RVA: 0x00126CD1 File Offset: 0x00124ED1
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17009F10 RID: 40720
			// (set) Token: 0x0600CEEA RID: 52970 RVA: 0x00126CE9 File Offset: 0x00124EE9
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17009F11 RID: 40721
			// (set) Token: 0x0600CEEB RID: 52971 RVA: 0x00126D01 File Offset: 0x00124F01
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17009F12 RID: 40722
			// (set) Token: 0x0600CEEC RID: 52972 RVA: 0x00126D19 File Offset: 0x00124F19
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17009F13 RID: 40723
			// (set) Token: 0x0600CEED RID: 52973 RVA: 0x00126D2C File Offset: 0x00124F2C
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17009F14 RID: 40724
			// (set) Token: 0x0600CEEE RID: 52974 RVA: 0x00126D3F File Offset: 0x00124F3F
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17009F15 RID: 40725
			// (set) Token: 0x0600CEEF RID: 52975 RVA: 0x00126D52 File Offset: 0x00124F52
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009F16 RID: 40726
			// (set) Token: 0x0600CEF0 RID: 52976 RVA: 0x00126D65 File Offset: 0x00124F65
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009F17 RID: 40727
			// (set) Token: 0x0600CEF1 RID: 52977 RVA: 0x00126D78 File Offset: 0x00124F78
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17009F18 RID: 40728
			// (set) Token: 0x0600CEF2 RID: 52978 RVA: 0x00126D8B File Offset: 0x00124F8B
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17009F19 RID: 40729
			// (set) Token: 0x0600CEF3 RID: 52979 RVA: 0x00126DA3 File Offset: 0x00124FA3
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17009F1A RID: 40730
			// (set) Token: 0x0600CEF4 RID: 52980 RVA: 0x00126DB6 File Offset: 0x00124FB6
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17009F1B RID: 40731
			// (set) Token: 0x0600CEF5 RID: 52981 RVA: 0x00126DCE File Offset: 0x00124FCE
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17009F1C RID: 40732
			// (set) Token: 0x0600CEF6 RID: 52982 RVA: 0x00126DE1 File Offset: 0x00124FE1
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17009F1D RID: 40733
			// (set) Token: 0x0600CEF7 RID: 52983 RVA: 0x00126DF9 File Offset: 0x00124FF9
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17009F1E RID: 40734
			// (set) Token: 0x0600CEF8 RID: 52984 RVA: 0x00126E0C File Offset: 0x0012500C
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17009F1F RID: 40735
			// (set) Token: 0x0600CEF9 RID: 52985 RVA: 0x00126E2A File Offset: 0x0012502A
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17009F20 RID: 40736
			// (set) Token: 0x0600CEFA RID: 52986 RVA: 0x00126E3D File Offset: 0x0012503D
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17009F21 RID: 40737
			// (set) Token: 0x0600CEFB RID: 52987 RVA: 0x00126E55 File Offset: 0x00125055
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17009F22 RID: 40738
			// (set) Token: 0x0600CEFC RID: 52988 RVA: 0x00126E6D File Offset: 0x0012506D
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17009F23 RID: 40739
			// (set) Token: 0x0600CEFD RID: 52989 RVA: 0x00126E85 File Offset: 0x00125085
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17009F24 RID: 40740
			// (set) Token: 0x0600CEFE RID: 52990 RVA: 0x00126E98 File Offset: 0x00125098
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17009F25 RID: 40741
			// (set) Token: 0x0600CEFF RID: 52991 RVA: 0x00126EB0 File Offset: 0x001250B0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17009F26 RID: 40742
			// (set) Token: 0x0600CF00 RID: 52992 RVA: 0x00126EC3 File Offset: 0x001250C3
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17009F27 RID: 40743
			// (set) Token: 0x0600CF01 RID: 52993 RVA: 0x00126ED6 File Offset: 0x001250D6
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17009F28 RID: 40744
			// (set) Token: 0x0600CF02 RID: 52994 RVA: 0x00126EF4 File Offset: 0x001250F4
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17009F29 RID: 40745
			// (set) Token: 0x0600CF03 RID: 52995 RVA: 0x00126F07 File Offset: 0x00125107
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009F2A RID: 40746
			// (set) Token: 0x0600CF04 RID: 52996 RVA: 0x00126F25 File Offset: 0x00125125
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009F2B RID: 40747
			// (set) Token: 0x0600CF05 RID: 52997 RVA: 0x00126F38 File Offset: 0x00125138
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009F2C RID: 40748
			// (set) Token: 0x0600CF06 RID: 52998 RVA: 0x00126F50 File Offset: 0x00125150
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009F2D RID: 40749
			// (set) Token: 0x0600CF07 RID: 52999 RVA: 0x00126F68 File Offset: 0x00125168
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009F2E RID: 40750
			// (set) Token: 0x0600CF08 RID: 53000 RVA: 0x00126F80 File Offset: 0x00125180
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009F2F RID: 40751
			// (set) Token: 0x0600CF09 RID: 53001 RVA: 0x00126F98 File Offset: 0x00125198
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DC8 RID: 3528
		public class ImportLiveIdParameters : ParametersBase
		{
			// Token: 0x17009F30 RID: 40752
			// (set) Token: 0x0600CF0B RID: 53003 RVA: 0x00126FB8 File Offset: 0x001251B8
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x17009F31 RID: 40753
			// (set) Token: 0x0600CF0C RID: 53004 RVA: 0x00126FCB File Offset: 0x001251CB
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17009F32 RID: 40754
			// (set) Token: 0x0600CF0D RID: 53005 RVA: 0x00126FDE File Offset: 0x001251DE
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17009F33 RID: 40755
			// (set) Token: 0x0600CF0E RID: 53006 RVA: 0x00126FF1 File Offset: 0x001251F1
			public virtual SwitchParameter ImportLiveId
			{
				set
				{
					base.PowerSharpParameters["ImportLiveId"] = value;
				}
			}

			// Token: 0x17009F34 RID: 40756
			// (set) Token: 0x0600CF0F RID: 53007 RVA: 0x00127009 File Offset: 0x00125209
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17009F35 RID: 40757
			// (set) Token: 0x0600CF10 RID: 53008 RVA: 0x0012701C File Offset: 0x0012521C
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17009F36 RID: 40758
			// (set) Token: 0x0600CF11 RID: 53009 RVA: 0x0012702F File Offset: 0x0012522F
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17009F37 RID: 40759
			// (set) Token: 0x0600CF12 RID: 53010 RVA: 0x00127042 File Offset: 0x00125242
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17009F38 RID: 40760
			// (set) Token: 0x0600CF13 RID: 53011 RVA: 0x00127055 File Offset: 0x00125255
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17009F39 RID: 40761
			// (set) Token: 0x0600CF14 RID: 53012 RVA: 0x00127068 File Offset: 0x00125268
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17009F3A RID: 40762
			// (set) Token: 0x0600CF15 RID: 53013 RVA: 0x0012707B File Offset: 0x0012527B
			public virtual Guid ExchangeGuid
			{
				set
				{
					base.PowerSharpParameters["ExchangeGuid"] = value;
				}
			}

			// Token: 0x17009F3B RID: 40763
			// (set) Token: 0x0600CF16 RID: 53014 RVA: 0x00127093 File Offset: 0x00125293
			public virtual string ForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17009F3C RID: 40764
			// (set) Token: 0x0600CF17 RID: 53015 RVA: 0x001270B1 File Offset: 0x001252B1
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17009F3D RID: 40765
			// (set) Token: 0x0600CF18 RID: 53016 RVA: 0x001270C9 File Offset: 0x001252C9
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17009F3E RID: 40766
			// (set) Token: 0x0600CF19 RID: 53017 RVA: 0x001270DC File Offset: 0x001252DC
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17009F3F RID: 40767
			// (set) Token: 0x0600CF1A RID: 53018 RVA: 0x001270EF File Offset: 0x001252EF
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17009F40 RID: 40768
			// (set) Token: 0x0600CF1B RID: 53019 RVA: 0x00127107 File Offset: 0x00125307
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17009F41 RID: 40769
			// (set) Token: 0x0600CF1C RID: 53020 RVA: 0x0012711A File Offset: 0x0012531A
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17009F42 RID: 40770
			// (set) Token: 0x0600CF1D RID: 53021 RVA: 0x0012712D File Offset: 0x0012532D
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17009F43 RID: 40771
			// (set) Token: 0x0600CF1E RID: 53022 RVA: 0x00127140 File Offset: 0x00125340
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17009F44 RID: 40772
			// (set) Token: 0x0600CF1F RID: 53023 RVA: 0x00127153 File Offset: 0x00125353
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17009F45 RID: 40773
			// (set) Token: 0x0600CF20 RID: 53024 RVA: 0x00127166 File Offset: 0x00125366
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17009F46 RID: 40774
			// (set) Token: 0x0600CF21 RID: 53025 RVA: 0x00127179 File Offset: 0x00125379
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17009F47 RID: 40775
			// (set) Token: 0x0600CF22 RID: 53026 RVA: 0x0012718C File Offset: 0x0012538C
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17009F48 RID: 40776
			// (set) Token: 0x0600CF23 RID: 53027 RVA: 0x0012719F File Offset: 0x0012539F
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17009F49 RID: 40777
			// (set) Token: 0x0600CF24 RID: 53028 RVA: 0x001271B2 File Offset: 0x001253B2
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17009F4A RID: 40778
			// (set) Token: 0x0600CF25 RID: 53029 RVA: 0x001271C5 File Offset: 0x001253C5
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17009F4B RID: 40779
			// (set) Token: 0x0600CF26 RID: 53030 RVA: 0x001271D8 File Offset: 0x001253D8
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17009F4C RID: 40780
			// (set) Token: 0x0600CF27 RID: 53031 RVA: 0x001271EB File Offset: 0x001253EB
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17009F4D RID: 40781
			// (set) Token: 0x0600CF28 RID: 53032 RVA: 0x001271FE File Offset: 0x001253FE
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17009F4E RID: 40782
			// (set) Token: 0x0600CF29 RID: 53033 RVA: 0x00127211 File Offset: 0x00125411
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17009F4F RID: 40783
			// (set) Token: 0x0600CF2A RID: 53034 RVA: 0x00127224 File Offset: 0x00125424
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17009F50 RID: 40784
			// (set) Token: 0x0600CF2B RID: 53035 RVA: 0x00127237 File Offset: 0x00125437
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17009F51 RID: 40785
			// (set) Token: 0x0600CF2C RID: 53036 RVA: 0x0012724A File Offset: 0x0012544A
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17009F52 RID: 40786
			// (set) Token: 0x0600CF2D RID: 53037 RVA: 0x0012725D File Offset: 0x0012545D
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17009F53 RID: 40787
			// (set) Token: 0x0600CF2E RID: 53038 RVA: 0x00127270 File Offset: 0x00125470
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17009F54 RID: 40788
			// (set) Token: 0x0600CF2F RID: 53039 RVA: 0x00127283 File Offset: 0x00125483
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17009F55 RID: 40789
			// (set) Token: 0x0600CF30 RID: 53040 RVA: 0x00127296 File Offset: 0x00125496
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17009F56 RID: 40790
			// (set) Token: 0x0600CF31 RID: 53041 RVA: 0x001272AE File Offset: 0x001254AE
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17009F57 RID: 40791
			// (set) Token: 0x0600CF32 RID: 53042 RVA: 0x001272C1 File Offset: 0x001254C1
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17009F58 RID: 40792
			// (set) Token: 0x0600CF33 RID: 53043 RVA: 0x001272D4 File Offset: 0x001254D4
			public virtual RecipientDisplayType? RecipientDisplayType
			{
				set
				{
					base.PowerSharpParameters["RecipientDisplayType"] = value;
				}
			}

			// Token: 0x17009F59 RID: 40793
			// (set) Token: 0x0600CF34 RID: 53044 RVA: 0x001272EC File Offset: 0x001254EC
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17009F5A RID: 40794
			// (set) Token: 0x0600CF35 RID: 53045 RVA: 0x00127304 File Offset: 0x00125504
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17009F5B RID: 40795
			// (set) Token: 0x0600CF36 RID: 53046 RVA: 0x0012731C File Offset: 0x0012551C
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17009F5C RID: 40796
			// (set) Token: 0x0600CF37 RID: 53047 RVA: 0x00127334 File Offset: 0x00125534
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17009F5D RID: 40797
			// (set) Token: 0x0600CF38 RID: 53048 RVA: 0x0012734C File Offset: 0x0012554C
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17009F5E RID: 40798
			// (set) Token: 0x0600CF39 RID: 53049 RVA: 0x0012735F File Offset: 0x0012555F
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17009F5F RID: 40799
			// (set) Token: 0x0600CF3A RID: 53050 RVA: 0x00127372 File Offset: 0x00125572
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17009F60 RID: 40800
			// (set) Token: 0x0600CF3B RID: 53051 RVA: 0x00127385 File Offset: 0x00125585
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17009F61 RID: 40801
			// (set) Token: 0x0600CF3C RID: 53052 RVA: 0x00127398 File Offset: 0x00125598
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17009F62 RID: 40802
			// (set) Token: 0x0600CF3D RID: 53053 RVA: 0x001273AB File Offset: 0x001255AB
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17009F63 RID: 40803
			// (set) Token: 0x0600CF3E RID: 53054 RVA: 0x001273BE File Offset: 0x001255BE
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17009F64 RID: 40804
			// (set) Token: 0x0600CF3F RID: 53055 RVA: 0x001273D6 File Offset: 0x001255D6
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17009F65 RID: 40805
			// (set) Token: 0x0600CF40 RID: 53056 RVA: 0x001273E9 File Offset: 0x001255E9
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17009F66 RID: 40806
			// (set) Token: 0x0600CF41 RID: 53057 RVA: 0x001273FC File Offset: 0x001255FC
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17009F67 RID: 40807
			// (set) Token: 0x0600CF42 RID: 53058 RVA: 0x0012740F File Offset: 0x0012560F
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17009F68 RID: 40808
			// (set) Token: 0x0600CF43 RID: 53059 RVA: 0x00127422 File Offset: 0x00125622
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17009F69 RID: 40809
			// (set) Token: 0x0600CF44 RID: 53060 RVA: 0x00127435 File Offset: 0x00125635
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17009F6A RID: 40810
			// (set) Token: 0x0600CF45 RID: 53061 RVA: 0x00127453 File Offset: 0x00125653
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17009F6B RID: 40811
			// (set) Token: 0x0600CF46 RID: 53062 RVA: 0x00127466 File Offset: 0x00125666
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17009F6C RID: 40812
			// (set) Token: 0x0600CF47 RID: 53063 RVA: 0x00127479 File Offset: 0x00125679
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x17009F6D RID: 40813
			// (set) Token: 0x0600CF48 RID: 53064 RVA: 0x0012748C File Offset: 0x0012568C
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x17009F6E RID: 40814
			// (set) Token: 0x0600CF49 RID: 53065 RVA: 0x0012749F File Offset: 0x0012569F
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x17009F6F RID: 40815
			// (set) Token: 0x0600CF4A RID: 53066 RVA: 0x001274B2 File Offset: 0x001256B2
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x17009F70 RID: 40816
			// (set) Token: 0x0600CF4B RID: 53067 RVA: 0x001274C5 File Offset: 0x001256C5
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17009F71 RID: 40817
			// (set) Token: 0x0600CF4C RID: 53068 RVA: 0x001274D8 File Offset: 0x001256D8
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17009F72 RID: 40818
			// (set) Token: 0x0600CF4D RID: 53069 RVA: 0x001274EB File Offset: 0x001256EB
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17009F73 RID: 40819
			// (set) Token: 0x0600CF4E RID: 53070 RVA: 0x001274FE File Offset: 0x001256FE
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17009F74 RID: 40820
			// (set) Token: 0x0600CF4F RID: 53071 RVA: 0x00127511 File Offset: 0x00125711
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x17009F75 RID: 40821
			// (set) Token: 0x0600CF50 RID: 53072 RVA: 0x00127524 File Offset: 0x00125724
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17009F76 RID: 40822
			// (set) Token: 0x0600CF51 RID: 53073 RVA: 0x00127537 File Offset: 0x00125737
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17009F77 RID: 40823
			// (set) Token: 0x0600CF52 RID: 53074 RVA: 0x0012754A File Offset: 0x0012574A
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17009F78 RID: 40824
			// (set) Token: 0x0600CF53 RID: 53075 RVA: 0x00127562 File Offset: 0x00125762
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17009F79 RID: 40825
			// (set) Token: 0x0600CF54 RID: 53076 RVA: 0x00127575 File Offset: 0x00125775
			public virtual SecurityIdentifier MasterAccountSid
			{
				set
				{
					base.PowerSharpParameters["MasterAccountSid"] = value;
				}
			}

			// Token: 0x17009F7A RID: 40826
			// (set) Token: 0x0600CF55 RID: 53077 RVA: 0x00127588 File Offset: 0x00125788
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x17009F7B RID: 40827
			// (set) Token: 0x0600CF56 RID: 53078 RVA: 0x001275A0 File Offset: 0x001257A0
			public virtual string IntendedMailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["IntendedMailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17009F7C RID: 40828
			// (set) Token: 0x0600CF57 RID: 53079 RVA: 0x001275BE File Offset: 0x001257BE
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17009F7D RID: 40829
			// (set) Token: 0x0600CF58 RID: 53080 RVA: 0x001275D6 File Offset: 0x001257D6
			public virtual ExchangeResourceType? ResourceType
			{
				set
				{
					base.PowerSharpParameters["ResourceType"] = value;
				}
			}

			// Token: 0x17009F7E RID: 40830
			// (set) Token: 0x0600CF59 RID: 53081 RVA: 0x001275EE File Offset: 0x001257EE
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17009F7F RID: 40831
			// (set) Token: 0x0600CF5A RID: 53082 RVA: 0x00127606 File Offset: 0x00125806
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x17009F80 RID: 40832
			// (set) Token: 0x0600CF5B RID: 53083 RVA: 0x00127619 File Offset: 0x00125819
			public virtual MultiValuedProperty<string> ResourceMetaData
			{
				set
				{
					base.PowerSharpParameters["ResourceMetaData"] = value;
				}
			}

			// Token: 0x17009F81 RID: 40833
			// (set) Token: 0x0600CF5C RID: 53084 RVA: 0x0012762C File Offset: 0x0012582C
			public virtual string ResourcePropertiesDisplay
			{
				set
				{
					base.PowerSharpParameters["ResourcePropertiesDisplay"] = value;
				}
			}

			// Token: 0x17009F82 RID: 40834
			// (set) Token: 0x0600CF5D RID: 53085 RVA: 0x0012763F File Offset: 0x0012583F
			public virtual MultiValuedProperty<string> ResourceSearchProperties
			{
				set
				{
					base.PowerSharpParameters["ResourceSearchProperties"] = value;
				}
			}

			// Token: 0x17009F83 RID: 40835
			// (set) Token: 0x0600CF5E RID: 53086 RVA: 0x00127652 File Offset: 0x00125852
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17009F84 RID: 40836
			// (set) Token: 0x0600CF5F RID: 53087 RVA: 0x0012766A File Offset: 0x0012586A
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17009F85 RID: 40837
			// (set) Token: 0x0600CF60 RID: 53088 RVA: 0x0012767D File Offset: 0x0012587D
			public virtual bool IsCalculatedTargetAddress
			{
				set
				{
					base.PowerSharpParameters["IsCalculatedTargetAddress"] = value;
				}
			}

			// Token: 0x17009F86 RID: 40838
			// (set) Token: 0x0600CF61 RID: 53089 RVA: 0x00127695 File Offset: 0x00125895
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x17009F87 RID: 40839
			// (set) Token: 0x0600CF62 RID: 53090 RVA: 0x001276A8 File Offset: 0x001258A8
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x17009F88 RID: 40840
			// (set) Token: 0x0600CF63 RID: 53091 RVA: 0x001276C0 File Offset: 0x001258C0
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x17009F89 RID: 40841
			// (set) Token: 0x0600CF64 RID: 53092 RVA: 0x001276D3 File Offset: 0x001258D3
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x17009F8A RID: 40842
			// (set) Token: 0x0600CF65 RID: 53093 RVA: 0x001276E6 File Offset: 0x001258E6
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x17009F8B RID: 40843
			// (set) Token: 0x0600CF66 RID: 53094 RVA: 0x001276F9 File Offset: 0x001258F9
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x17009F8C RID: 40844
			// (set) Token: 0x0600CF67 RID: 53095 RVA: 0x00127711 File Offset: 0x00125911
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x17009F8D RID: 40845
			// (set) Token: 0x0600CF68 RID: 53096 RVA: 0x00127729 File Offset: 0x00125929
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x17009F8E RID: 40846
			// (set) Token: 0x0600CF69 RID: 53097 RVA: 0x0012773C File Offset: 0x0012593C
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x17009F8F RID: 40847
			// (set) Token: 0x0600CF6A RID: 53098 RVA: 0x0012774F File Offset: 0x0012594F
			public virtual ElcMailboxFlags ElcMailboxFlags
			{
				set
				{
					base.PowerSharpParameters["ElcMailboxFlags"] = value;
				}
			}

			// Token: 0x17009F90 RID: 40848
			// (set) Token: 0x0600CF6B RID: 53099 RVA: 0x00127767 File Offset: 0x00125967
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17009F91 RID: 40849
			// (set) Token: 0x0600CF6C RID: 53100 RVA: 0x0012777F File Offset: 0x0012597F
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x17009F92 RID: 40850
			// (set) Token: 0x0600CF6D RID: 53101 RVA: 0x00127797 File Offset: 0x00125997
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x17009F93 RID: 40851
			// (set) Token: 0x0600CF6E RID: 53102 RVA: 0x001277AA File Offset: 0x001259AA
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x17009F94 RID: 40852
			// (set) Token: 0x0600CF6F RID: 53103 RVA: 0x001277BD File Offset: 0x001259BD
			public virtual bool MailboxAuditEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditEnabled"] = value;
				}
			}

			// Token: 0x17009F95 RID: 40853
			// (set) Token: 0x0600CF70 RID: 53104 RVA: 0x001277D5 File Offset: 0x001259D5
			public virtual EnhancedTimeSpan MailboxAuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x17009F96 RID: 40854
			// (set) Token: 0x0600CF71 RID: 53105 RVA: 0x001277ED File Offset: 0x001259ED
			public virtual MailboxAuditOperations AuditAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditAdminOperations"] = value;
				}
			}

			// Token: 0x17009F97 RID: 40855
			// (set) Token: 0x0600CF72 RID: 53106 RVA: 0x00127805 File Offset: 0x00125A05
			public virtual MailboxAuditOperations AuditDelegateAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateAdminOperations"] = value;
				}
			}

			// Token: 0x17009F98 RID: 40856
			// (set) Token: 0x0600CF73 RID: 53107 RVA: 0x0012781D File Offset: 0x00125A1D
			public virtual MailboxAuditOperations AuditDelegateOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateOperations"] = value;
				}
			}

			// Token: 0x17009F99 RID: 40857
			// (set) Token: 0x0600CF74 RID: 53108 RVA: 0x00127835 File Offset: 0x00125A35
			public virtual MailboxAuditOperations AuditOwnerOperations
			{
				set
				{
					base.PowerSharpParameters["AuditOwnerOperations"] = value;
				}
			}

			// Token: 0x17009F9A RID: 40858
			// (set) Token: 0x0600CF75 RID: 53109 RVA: 0x0012784D File Offset: 0x00125A4D
			public virtual bool BypassAudit
			{
				set
				{
					base.PowerSharpParameters["BypassAudit"] = value;
				}
			}

			// Token: 0x17009F9B RID: 40859
			// (set) Token: 0x0600CF76 RID: 53110 RVA: 0x00127865 File Offset: 0x00125A65
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x17009F9C RID: 40860
			// (set) Token: 0x0600CF77 RID: 53111 RVA: 0x00127878 File Offset: 0x00125A78
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x17009F9D RID: 40861
			// (set) Token: 0x0600CF78 RID: 53112 RVA: 0x00127890 File Offset: 0x00125A90
			public virtual RecipientIdParameter SiteMailboxOwners
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxOwners"] = value;
				}
			}

			// Token: 0x17009F9E RID: 40862
			// (set) Token: 0x0600CF79 RID: 53113 RVA: 0x001278A3 File Offset: 0x00125AA3
			public virtual RecipientIdParameter SiteMailboxUsers
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxUsers"] = value;
				}
			}

			// Token: 0x17009F9F RID: 40863
			// (set) Token: 0x0600CF7A RID: 53114 RVA: 0x001278B6 File Offset: 0x00125AB6
			public virtual DateTime? SiteMailboxClosedTime
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxClosedTime"] = value;
				}
			}

			// Token: 0x17009FA0 RID: 40864
			// (set) Token: 0x0600CF7B RID: 53115 RVA: 0x001278CE File Offset: 0x00125ACE
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x17009FA1 RID: 40865
			// (set) Token: 0x0600CF7C RID: 53116 RVA: 0x001278E1 File Offset: 0x00125AE1
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17009FA2 RID: 40866
			// (set) Token: 0x0600CF7D RID: 53117 RVA: 0x001278F9 File Offset: 0x00125AF9
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x17009FA3 RID: 40867
			// (set) Token: 0x0600CF7E RID: 53118 RVA: 0x00127911 File Offset: 0x00125B11
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17009FA4 RID: 40868
			// (set) Token: 0x0600CF7F RID: 53119 RVA: 0x00127929 File Offset: 0x00125B29
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17009FA5 RID: 40869
			// (set) Token: 0x0600CF80 RID: 53120 RVA: 0x0012793C File Offset: 0x00125B3C
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17009FA6 RID: 40870
			// (set) Token: 0x0600CF81 RID: 53121 RVA: 0x0012794F File Offset: 0x00125B4F
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17009FA7 RID: 40871
			// (set) Token: 0x0600CF82 RID: 53122 RVA: 0x00127962 File Offset: 0x00125B62
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17009FA8 RID: 40872
			// (set) Token: 0x0600CF83 RID: 53123 RVA: 0x00127975 File Offset: 0x00125B75
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17009FA9 RID: 40873
			// (set) Token: 0x0600CF84 RID: 53124 RVA: 0x00127988 File Offset: 0x00125B88
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17009FAA RID: 40874
			// (set) Token: 0x0600CF85 RID: 53125 RVA: 0x0012799B File Offset: 0x00125B9B
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17009FAB RID: 40875
			// (set) Token: 0x0600CF86 RID: 53126 RVA: 0x001279B3 File Offset: 0x00125BB3
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17009FAC RID: 40876
			// (set) Token: 0x0600CF87 RID: 53127 RVA: 0x001279C6 File Offset: 0x00125BC6
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17009FAD RID: 40877
			// (set) Token: 0x0600CF88 RID: 53128 RVA: 0x001279DE File Offset: 0x00125BDE
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17009FAE RID: 40878
			// (set) Token: 0x0600CF89 RID: 53129 RVA: 0x001279F1 File Offset: 0x00125BF1
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17009FAF RID: 40879
			// (set) Token: 0x0600CF8A RID: 53130 RVA: 0x00127A09 File Offset: 0x00125C09
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17009FB0 RID: 40880
			// (set) Token: 0x0600CF8B RID: 53131 RVA: 0x00127A1C File Offset: 0x00125C1C
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17009FB1 RID: 40881
			// (set) Token: 0x0600CF8C RID: 53132 RVA: 0x00127A3A File Offset: 0x00125C3A
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17009FB2 RID: 40882
			// (set) Token: 0x0600CF8D RID: 53133 RVA: 0x00127A4D File Offset: 0x00125C4D
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17009FB3 RID: 40883
			// (set) Token: 0x0600CF8E RID: 53134 RVA: 0x00127A65 File Offset: 0x00125C65
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17009FB4 RID: 40884
			// (set) Token: 0x0600CF8F RID: 53135 RVA: 0x00127A7D File Offset: 0x00125C7D
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17009FB5 RID: 40885
			// (set) Token: 0x0600CF90 RID: 53136 RVA: 0x00127A95 File Offset: 0x00125C95
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17009FB6 RID: 40886
			// (set) Token: 0x0600CF91 RID: 53137 RVA: 0x00127AA8 File Offset: 0x00125CA8
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17009FB7 RID: 40887
			// (set) Token: 0x0600CF92 RID: 53138 RVA: 0x00127AC0 File Offset: 0x00125CC0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17009FB8 RID: 40888
			// (set) Token: 0x0600CF93 RID: 53139 RVA: 0x00127AD3 File Offset: 0x00125CD3
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17009FB9 RID: 40889
			// (set) Token: 0x0600CF94 RID: 53140 RVA: 0x00127AE6 File Offset: 0x00125CE6
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17009FBA RID: 40890
			// (set) Token: 0x0600CF95 RID: 53141 RVA: 0x00127B04 File Offset: 0x00125D04
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17009FBB RID: 40891
			// (set) Token: 0x0600CF96 RID: 53142 RVA: 0x00127B17 File Offset: 0x00125D17
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009FBC RID: 40892
			// (set) Token: 0x0600CF97 RID: 53143 RVA: 0x00127B35 File Offset: 0x00125D35
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009FBD RID: 40893
			// (set) Token: 0x0600CF98 RID: 53144 RVA: 0x00127B48 File Offset: 0x00125D48
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009FBE RID: 40894
			// (set) Token: 0x0600CF99 RID: 53145 RVA: 0x00127B60 File Offset: 0x00125D60
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009FBF RID: 40895
			// (set) Token: 0x0600CF9A RID: 53146 RVA: 0x00127B78 File Offset: 0x00125D78
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009FC0 RID: 40896
			// (set) Token: 0x0600CF9B RID: 53147 RVA: 0x00127B90 File Offset: 0x00125D90
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009FC1 RID: 40897
			// (set) Token: 0x0600CF9C RID: 53148 RVA: 0x00127BA8 File Offset: 0x00125DA8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DC9 RID: 3529
		public class DisabledUserParameters : ParametersBase
		{
			// Token: 0x17009FC2 RID: 40898
			// (set) Token: 0x0600CF9E RID: 53150 RVA: 0x00127BC8 File Offset: 0x00125DC8
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x17009FC3 RID: 40899
			// (set) Token: 0x0600CF9F RID: 53151 RVA: 0x00127BDB File Offset: 0x00125DDB
			public virtual bool UsePreferMessageFormat
			{
				set
				{
					base.PowerSharpParameters["UsePreferMessageFormat"] = value;
				}
			}

			// Token: 0x17009FC4 RID: 40900
			// (set) Token: 0x0600CFA0 RID: 53152 RVA: 0x00127BF3 File Offset: 0x00125DF3
			public virtual MessageFormat MessageFormat
			{
				set
				{
					base.PowerSharpParameters["MessageFormat"] = value;
				}
			}

			// Token: 0x17009FC5 RID: 40901
			// (set) Token: 0x0600CFA1 RID: 53153 RVA: 0x00127C0B File Offset: 0x00125E0B
			public virtual MessageBodyFormat MessageBodyFormat
			{
				set
				{
					base.PowerSharpParameters["MessageBodyFormat"] = value;
				}
			}

			// Token: 0x17009FC6 RID: 40902
			// (set) Token: 0x0600CFA2 RID: 53154 RVA: 0x00127C23 File Offset: 0x00125E23
			public virtual MacAttachmentFormat MacAttachmentFormat
			{
				set
				{
					base.PowerSharpParameters["MacAttachmentFormat"] = value;
				}
			}

			// Token: 0x17009FC7 RID: 40903
			// (set) Token: 0x0600CFA3 RID: 53155 RVA: 0x00127C3B File Offset: 0x00125E3B
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17009FC8 RID: 40904
			// (set) Token: 0x0600CFA4 RID: 53156 RVA: 0x00127C4E File Offset: 0x00125E4E
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x17009FC9 RID: 40905
			// (set) Token: 0x0600CFA5 RID: 53157 RVA: 0x00127C61 File Offset: 0x00125E61
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x17009FCA RID: 40906
			// (set) Token: 0x0600CFA6 RID: 53158 RVA: 0x00127C74 File Offset: 0x00125E74
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17009FCB RID: 40907
			// (set) Token: 0x0600CFA7 RID: 53159 RVA: 0x00127C87 File Offset: 0x00125E87
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17009FCC RID: 40908
			// (set) Token: 0x0600CFA8 RID: 53160 RVA: 0x00127C9A File Offset: 0x00125E9A
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17009FCD RID: 40909
			// (set) Token: 0x0600CFA9 RID: 53161 RVA: 0x00127CAD File Offset: 0x00125EAD
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17009FCE RID: 40910
			// (set) Token: 0x0600CFAA RID: 53162 RVA: 0x00127CC0 File Offset: 0x00125EC0
			public virtual Guid ExchangeGuid
			{
				set
				{
					base.PowerSharpParameters["ExchangeGuid"] = value;
				}
			}

			// Token: 0x17009FCF RID: 40911
			// (set) Token: 0x0600CFAB RID: 53163 RVA: 0x00127CD8 File Offset: 0x00125ED8
			public virtual string ForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17009FD0 RID: 40912
			// (set) Token: 0x0600CFAC RID: 53164 RVA: 0x00127CF6 File Offset: 0x00125EF6
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x17009FD1 RID: 40913
			// (set) Token: 0x0600CFAD RID: 53165 RVA: 0x00127D0E File Offset: 0x00125F0E
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x17009FD2 RID: 40914
			// (set) Token: 0x0600CFAE RID: 53166 RVA: 0x00127D21 File Offset: 0x00125F21
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17009FD3 RID: 40915
			// (set) Token: 0x0600CFAF RID: 53167 RVA: 0x00127D34 File Offset: 0x00125F34
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x17009FD4 RID: 40916
			// (set) Token: 0x0600CFB0 RID: 53168 RVA: 0x00127D4C File Offset: 0x00125F4C
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17009FD5 RID: 40917
			// (set) Token: 0x0600CFB1 RID: 53169 RVA: 0x00127D5F File Offset: 0x00125F5F
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17009FD6 RID: 40918
			// (set) Token: 0x0600CFB2 RID: 53170 RVA: 0x00127D72 File Offset: 0x00125F72
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17009FD7 RID: 40919
			// (set) Token: 0x0600CFB3 RID: 53171 RVA: 0x00127D85 File Offset: 0x00125F85
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17009FD8 RID: 40920
			// (set) Token: 0x0600CFB4 RID: 53172 RVA: 0x00127D98 File Offset: 0x00125F98
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17009FD9 RID: 40921
			// (set) Token: 0x0600CFB5 RID: 53173 RVA: 0x00127DAB File Offset: 0x00125FAB
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17009FDA RID: 40922
			// (set) Token: 0x0600CFB6 RID: 53174 RVA: 0x00127DBE File Offset: 0x00125FBE
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17009FDB RID: 40923
			// (set) Token: 0x0600CFB7 RID: 53175 RVA: 0x00127DD1 File Offset: 0x00125FD1
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17009FDC RID: 40924
			// (set) Token: 0x0600CFB8 RID: 53176 RVA: 0x00127DE4 File Offset: 0x00125FE4
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17009FDD RID: 40925
			// (set) Token: 0x0600CFB9 RID: 53177 RVA: 0x00127DF7 File Offset: 0x00125FF7
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17009FDE RID: 40926
			// (set) Token: 0x0600CFBA RID: 53178 RVA: 0x00127E0A File Offset: 0x0012600A
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17009FDF RID: 40927
			// (set) Token: 0x0600CFBB RID: 53179 RVA: 0x00127E1D File Offset: 0x0012601D
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17009FE0 RID: 40928
			// (set) Token: 0x0600CFBC RID: 53180 RVA: 0x00127E30 File Offset: 0x00126030
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17009FE1 RID: 40929
			// (set) Token: 0x0600CFBD RID: 53181 RVA: 0x00127E43 File Offset: 0x00126043
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17009FE2 RID: 40930
			// (set) Token: 0x0600CFBE RID: 53182 RVA: 0x00127E56 File Offset: 0x00126056
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17009FE3 RID: 40931
			// (set) Token: 0x0600CFBF RID: 53183 RVA: 0x00127E69 File Offset: 0x00126069
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17009FE4 RID: 40932
			// (set) Token: 0x0600CFC0 RID: 53184 RVA: 0x00127E7C File Offset: 0x0012607C
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17009FE5 RID: 40933
			// (set) Token: 0x0600CFC1 RID: 53185 RVA: 0x00127E8F File Offset: 0x0012608F
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17009FE6 RID: 40934
			// (set) Token: 0x0600CFC2 RID: 53186 RVA: 0x00127EA2 File Offset: 0x001260A2
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17009FE7 RID: 40935
			// (set) Token: 0x0600CFC3 RID: 53187 RVA: 0x00127EB5 File Offset: 0x001260B5
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17009FE8 RID: 40936
			// (set) Token: 0x0600CFC4 RID: 53188 RVA: 0x00127EC8 File Offset: 0x001260C8
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17009FE9 RID: 40937
			// (set) Token: 0x0600CFC5 RID: 53189 RVA: 0x00127EDB File Offset: 0x001260DB
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17009FEA RID: 40938
			// (set) Token: 0x0600CFC6 RID: 53190 RVA: 0x00127EF3 File Offset: 0x001260F3
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17009FEB RID: 40939
			// (set) Token: 0x0600CFC7 RID: 53191 RVA: 0x00127F06 File Offset: 0x00126106
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17009FEC RID: 40940
			// (set) Token: 0x0600CFC8 RID: 53192 RVA: 0x00127F19 File Offset: 0x00126119
			public virtual RecipientDisplayType? RecipientDisplayType
			{
				set
				{
					base.PowerSharpParameters["RecipientDisplayType"] = value;
				}
			}

			// Token: 0x17009FED RID: 40941
			// (set) Token: 0x0600CFC9 RID: 53193 RVA: 0x00127F31 File Offset: 0x00126131
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x17009FEE RID: 40942
			// (set) Token: 0x0600CFCA RID: 53194 RVA: 0x00127F49 File Offset: 0x00126149
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x17009FEF RID: 40943
			// (set) Token: 0x0600CFCB RID: 53195 RVA: 0x00127F61 File Offset: 0x00126161
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x17009FF0 RID: 40944
			// (set) Token: 0x0600CFCC RID: 53196 RVA: 0x00127F79 File Offset: 0x00126179
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17009FF1 RID: 40945
			// (set) Token: 0x0600CFCD RID: 53197 RVA: 0x00127F91 File Offset: 0x00126191
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x17009FF2 RID: 40946
			// (set) Token: 0x0600CFCE RID: 53198 RVA: 0x00127FA4 File Offset: 0x001261A4
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17009FF3 RID: 40947
			// (set) Token: 0x0600CFCF RID: 53199 RVA: 0x00127FB7 File Offset: 0x001261B7
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17009FF4 RID: 40948
			// (set) Token: 0x0600CFD0 RID: 53200 RVA: 0x00127FCA File Offset: 0x001261CA
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17009FF5 RID: 40949
			// (set) Token: 0x0600CFD1 RID: 53201 RVA: 0x00127FDD File Offset: 0x001261DD
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x17009FF6 RID: 40950
			// (set) Token: 0x0600CFD2 RID: 53202 RVA: 0x00127FF0 File Offset: 0x001261F0
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x17009FF7 RID: 40951
			// (set) Token: 0x0600CFD3 RID: 53203 RVA: 0x00128003 File Offset: 0x00126203
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x17009FF8 RID: 40952
			// (set) Token: 0x0600CFD4 RID: 53204 RVA: 0x0012801B File Offset: 0x0012621B
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17009FF9 RID: 40953
			// (set) Token: 0x0600CFD5 RID: 53205 RVA: 0x0012802E File Offset: 0x0012622E
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17009FFA RID: 40954
			// (set) Token: 0x0600CFD6 RID: 53206 RVA: 0x00128041 File Offset: 0x00126241
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17009FFB RID: 40955
			// (set) Token: 0x0600CFD7 RID: 53207 RVA: 0x00128054 File Offset: 0x00126254
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17009FFC RID: 40956
			// (set) Token: 0x0600CFD8 RID: 53208 RVA: 0x00128067 File Offset: 0x00126267
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17009FFD RID: 40957
			// (set) Token: 0x0600CFD9 RID: 53209 RVA: 0x0012807A File Offset: 0x0012627A
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17009FFE RID: 40958
			// (set) Token: 0x0600CFDA RID: 53210 RVA: 0x00128098 File Offset: 0x00126298
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17009FFF RID: 40959
			// (set) Token: 0x0600CFDB RID: 53211 RVA: 0x001280AB File Offset: 0x001262AB
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x1700A000 RID: 40960
			// (set) Token: 0x0600CFDC RID: 53212 RVA: 0x001280BE File Offset: 0x001262BE
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x1700A001 RID: 40961
			// (set) Token: 0x0600CFDD RID: 53213 RVA: 0x001280D1 File Offset: 0x001262D1
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x1700A002 RID: 40962
			// (set) Token: 0x0600CFDE RID: 53214 RVA: 0x001280E4 File Offset: 0x001262E4
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x1700A003 RID: 40963
			// (set) Token: 0x0600CFDF RID: 53215 RVA: 0x001280F7 File Offset: 0x001262F7
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x1700A004 RID: 40964
			// (set) Token: 0x0600CFE0 RID: 53216 RVA: 0x0012810A File Offset: 0x0012630A
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x1700A005 RID: 40965
			// (set) Token: 0x0600CFE1 RID: 53217 RVA: 0x0012811D File Offset: 0x0012631D
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x1700A006 RID: 40966
			// (set) Token: 0x0600CFE2 RID: 53218 RVA: 0x00128130 File Offset: 0x00126330
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x1700A007 RID: 40967
			// (set) Token: 0x0600CFE3 RID: 53219 RVA: 0x00128143 File Offset: 0x00126343
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x1700A008 RID: 40968
			// (set) Token: 0x0600CFE4 RID: 53220 RVA: 0x00128156 File Offset: 0x00126356
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x1700A009 RID: 40969
			// (set) Token: 0x0600CFE5 RID: 53221 RVA: 0x00128169 File Offset: 0x00126369
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x1700A00A RID: 40970
			// (set) Token: 0x0600CFE6 RID: 53222 RVA: 0x0012817C File Offset: 0x0012637C
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x1700A00B RID: 40971
			// (set) Token: 0x0600CFE7 RID: 53223 RVA: 0x0012818F File Offset: 0x0012638F
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x1700A00C RID: 40972
			// (set) Token: 0x0600CFE8 RID: 53224 RVA: 0x001281A7 File Offset: 0x001263A7
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x1700A00D RID: 40973
			// (set) Token: 0x0600CFE9 RID: 53225 RVA: 0x001281BA File Offset: 0x001263BA
			public virtual SecurityIdentifier MasterAccountSid
			{
				set
				{
					base.PowerSharpParameters["MasterAccountSid"] = value;
				}
			}

			// Token: 0x1700A00E RID: 40974
			// (set) Token: 0x0600CFEA RID: 53226 RVA: 0x001281CD File Offset: 0x001263CD
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x1700A00F RID: 40975
			// (set) Token: 0x0600CFEB RID: 53227 RVA: 0x001281E5 File Offset: 0x001263E5
			public virtual string IntendedMailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["IntendedMailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x1700A010 RID: 40976
			// (set) Token: 0x0600CFEC RID: 53228 RVA: 0x00128203 File Offset: 0x00126403
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x1700A011 RID: 40977
			// (set) Token: 0x0600CFED RID: 53229 RVA: 0x0012821B File Offset: 0x0012641B
			public virtual ExchangeResourceType? ResourceType
			{
				set
				{
					base.PowerSharpParameters["ResourceType"] = value;
				}
			}

			// Token: 0x1700A012 RID: 40978
			// (set) Token: 0x0600CFEE RID: 53230 RVA: 0x00128233 File Offset: 0x00126433
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x1700A013 RID: 40979
			// (set) Token: 0x0600CFEF RID: 53231 RVA: 0x0012824B File Offset: 0x0012644B
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x1700A014 RID: 40980
			// (set) Token: 0x0600CFF0 RID: 53232 RVA: 0x0012825E File Offset: 0x0012645E
			public virtual MultiValuedProperty<string> ResourceMetaData
			{
				set
				{
					base.PowerSharpParameters["ResourceMetaData"] = value;
				}
			}

			// Token: 0x1700A015 RID: 40981
			// (set) Token: 0x0600CFF1 RID: 53233 RVA: 0x00128271 File Offset: 0x00126471
			public virtual string ResourcePropertiesDisplay
			{
				set
				{
					base.PowerSharpParameters["ResourcePropertiesDisplay"] = value;
				}
			}

			// Token: 0x1700A016 RID: 40982
			// (set) Token: 0x0600CFF2 RID: 53234 RVA: 0x00128284 File Offset: 0x00126484
			public virtual MultiValuedProperty<string> ResourceSearchProperties
			{
				set
				{
					base.PowerSharpParameters["ResourceSearchProperties"] = value;
				}
			}

			// Token: 0x1700A017 RID: 40983
			// (set) Token: 0x0600CFF3 RID: 53235 RVA: 0x00128297 File Offset: 0x00126497
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x1700A018 RID: 40984
			// (set) Token: 0x0600CFF4 RID: 53236 RVA: 0x001282AF File Offset: 0x001264AF
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x1700A019 RID: 40985
			// (set) Token: 0x0600CFF5 RID: 53237 RVA: 0x001282C2 File Offset: 0x001264C2
			public virtual bool IsCalculatedTargetAddress
			{
				set
				{
					base.PowerSharpParameters["IsCalculatedTargetAddress"] = value;
				}
			}

			// Token: 0x1700A01A RID: 40986
			// (set) Token: 0x0600CFF6 RID: 53238 RVA: 0x001282DA File Offset: 0x001264DA
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x1700A01B RID: 40987
			// (set) Token: 0x0600CFF7 RID: 53239 RVA: 0x001282ED File Offset: 0x001264ED
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x1700A01C RID: 40988
			// (set) Token: 0x0600CFF8 RID: 53240 RVA: 0x00128305 File Offset: 0x00126505
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x1700A01D RID: 40989
			// (set) Token: 0x0600CFF9 RID: 53241 RVA: 0x00128318 File Offset: 0x00126518
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x1700A01E RID: 40990
			// (set) Token: 0x0600CFFA RID: 53242 RVA: 0x0012832B File Offset: 0x0012652B
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x1700A01F RID: 40991
			// (set) Token: 0x0600CFFB RID: 53243 RVA: 0x0012833E File Offset: 0x0012653E
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x1700A020 RID: 40992
			// (set) Token: 0x0600CFFC RID: 53244 RVA: 0x00128356 File Offset: 0x00126556
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x1700A021 RID: 40993
			// (set) Token: 0x0600CFFD RID: 53245 RVA: 0x0012836E File Offset: 0x0012656E
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x1700A022 RID: 40994
			// (set) Token: 0x0600CFFE RID: 53246 RVA: 0x00128381 File Offset: 0x00126581
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x1700A023 RID: 40995
			// (set) Token: 0x0600CFFF RID: 53247 RVA: 0x00128394 File Offset: 0x00126594
			public virtual ElcMailboxFlags ElcMailboxFlags
			{
				set
				{
					base.PowerSharpParameters["ElcMailboxFlags"] = value;
				}
			}

			// Token: 0x1700A024 RID: 40996
			// (set) Token: 0x0600D000 RID: 53248 RVA: 0x001283AC File Offset: 0x001265AC
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x1700A025 RID: 40997
			// (set) Token: 0x0600D001 RID: 53249 RVA: 0x001283C4 File Offset: 0x001265C4
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x1700A026 RID: 40998
			// (set) Token: 0x0600D002 RID: 53250 RVA: 0x001283DC File Offset: 0x001265DC
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x1700A027 RID: 40999
			// (set) Token: 0x0600D003 RID: 53251 RVA: 0x001283EF File Offset: 0x001265EF
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x1700A028 RID: 41000
			// (set) Token: 0x0600D004 RID: 53252 RVA: 0x00128402 File Offset: 0x00126602
			public virtual bool MailboxAuditEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditEnabled"] = value;
				}
			}

			// Token: 0x1700A029 RID: 41001
			// (set) Token: 0x0600D005 RID: 53253 RVA: 0x0012841A File Offset: 0x0012661A
			public virtual EnhancedTimeSpan MailboxAuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x1700A02A RID: 41002
			// (set) Token: 0x0600D006 RID: 53254 RVA: 0x00128432 File Offset: 0x00126632
			public virtual MailboxAuditOperations AuditAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditAdminOperations"] = value;
				}
			}

			// Token: 0x1700A02B RID: 41003
			// (set) Token: 0x0600D007 RID: 53255 RVA: 0x0012844A File Offset: 0x0012664A
			public virtual MailboxAuditOperations AuditDelegateAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateAdminOperations"] = value;
				}
			}

			// Token: 0x1700A02C RID: 41004
			// (set) Token: 0x0600D008 RID: 53256 RVA: 0x00128462 File Offset: 0x00126662
			public virtual MailboxAuditOperations AuditDelegateOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateOperations"] = value;
				}
			}

			// Token: 0x1700A02D RID: 41005
			// (set) Token: 0x0600D009 RID: 53257 RVA: 0x0012847A File Offset: 0x0012667A
			public virtual MailboxAuditOperations AuditOwnerOperations
			{
				set
				{
					base.PowerSharpParameters["AuditOwnerOperations"] = value;
				}
			}

			// Token: 0x1700A02E RID: 41006
			// (set) Token: 0x0600D00A RID: 53258 RVA: 0x00128492 File Offset: 0x00126692
			public virtual bool BypassAudit
			{
				set
				{
					base.PowerSharpParameters["BypassAudit"] = value;
				}
			}

			// Token: 0x1700A02F RID: 41007
			// (set) Token: 0x0600D00B RID: 53259 RVA: 0x001284AA File Offset: 0x001266AA
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x1700A030 RID: 41008
			// (set) Token: 0x0600D00C RID: 53260 RVA: 0x001284BD File Offset: 0x001266BD
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x1700A031 RID: 41009
			// (set) Token: 0x0600D00D RID: 53261 RVA: 0x001284D5 File Offset: 0x001266D5
			public virtual RecipientIdParameter SiteMailboxOwners
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxOwners"] = value;
				}
			}

			// Token: 0x1700A032 RID: 41010
			// (set) Token: 0x0600D00E RID: 53262 RVA: 0x001284E8 File Offset: 0x001266E8
			public virtual RecipientIdParameter SiteMailboxUsers
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxUsers"] = value;
				}
			}

			// Token: 0x1700A033 RID: 41011
			// (set) Token: 0x0600D00F RID: 53263 RVA: 0x001284FB File Offset: 0x001266FB
			public virtual DateTime? SiteMailboxClosedTime
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxClosedTime"] = value;
				}
			}

			// Token: 0x1700A034 RID: 41012
			// (set) Token: 0x0600D010 RID: 53264 RVA: 0x00128513 File Offset: 0x00126713
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x1700A035 RID: 41013
			// (set) Token: 0x0600D011 RID: 53265 RVA: 0x00128526 File Offset: 0x00126726
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x1700A036 RID: 41014
			// (set) Token: 0x0600D012 RID: 53266 RVA: 0x0012853E File Offset: 0x0012673E
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x1700A037 RID: 41015
			// (set) Token: 0x0600D013 RID: 53267 RVA: 0x00128556 File Offset: 0x00126756
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700A038 RID: 41016
			// (set) Token: 0x0600D014 RID: 53268 RVA: 0x0012856E File Offset: 0x0012676E
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x1700A039 RID: 41017
			// (set) Token: 0x0600D015 RID: 53269 RVA: 0x00128581 File Offset: 0x00126781
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x1700A03A RID: 41018
			// (set) Token: 0x0600D016 RID: 53270 RVA: 0x00128594 File Offset: 0x00126794
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x1700A03B RID: 41019
			// (set) Token: 0x0600D017 RID: 53271 RVA: 0x001285A7 File Offset: 0x001267A7
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700A03C RID: 41020
			// (set) Token: 0x0600D018 RID: 53272 RVA: 0x001285BA File Offset: 0x001267BA
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700A03D RID: 41021
			// (set) Token: 0x0600D019 RID: 53273 RVA: 0x001285CD File Offset: 0x001267CD
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700A03E RID: 41022
			// (set) Token: 0x0600D01A RID: 53274 RVA: 0x001285E0 File Offset: 0x001267E0
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700A03F RID: 41023
			// (set) Token: 0x0600D01B RID: 53275 RVA: 0x001285F8 File Offset: 0x001267F8
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700A040 RID: 41024
			// (set) Token: 0x0600D01C RID: 53276 RVA: 0x0012860B File Offset: 0x0012680B
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x1700A041 RID: 41025
			// (set) Token: 0x0600D01D RID: 53277 RVA: 0x00128623 File Offset: 0x00126823
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x1700A042 RID: 41026
			// (set) Token: 0x0600D01E RID: 53278 RVA: 0x00128636 File Offset: 0x00126836
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x1700A043 RID: 41027
			// (set) Token: 0x0600D01F RID: 53279 RVA: 0x0012864E File Offset: 0x0012684E
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700A044 RID: 41028
			// (set) Token: 0x0600D020 RID: 53280 RVA: 0x00128661 File Offset: 0x00126861
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A045 RID: 41029
			// (set) Token: 0x0600D021 RID: 53281 RVA: 0x0012867F File Offset: 0x0012687F
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700A046 RID: 41030
			// (set) Token: 0x0600D022 RID: 53282 RVA: 0x00128692 File Offset: 0x00126892
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700A047 RID: 41031
			// (set) Token: 0x0600D023 RID: 53283 RVA: 0x001286AA File Offset: 0x001268AA
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700A048 RID: 41032
			// (set) Token: 0x0600D024 RID: 53284 RVA: 0x001286C2 File Offset: 0x001268C2
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700A049 RID: 41033
			// (set) Token: 0x0600D025 RID: 53285 RVA: 0x001286DA File Offset: 0x001268DA
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700A04A RID: 41034
			// (set) Token: 0x0600D026 RID: 53286 RVA: 0x001286ED File Offset: 0x001268ED
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700A04B RID: 41035
			// (set) Token: 0x0600D027 RID: 53287 RVA: 0x00128705 File Offset: 0x00126905
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A04C RID: 41036
			// (set) Token: 0x0600D028 RID: 53288 RVA: 0x00128718 File Offset: 0x00126918
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A04D RID: 41037
			// (set) Token: 0x0600D029 RID: 53289 RVA: 0x0012872B File Offset: 0x0012692B
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A04E RID: 41038
			// (set) Token: 0x0600D02A RID: 53290 RVA: 0x00128749 File Offset: 0x00126949
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700A04F RID: 41039
			// (set) Token: 0x0600D02B RID: 53291 RVA: 0x0012875C File Offset: 0x0012695C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A050 RID: 41040
			// (set) Token: 0x0600D02C RID: 53292 RVA: 0x0012877A File Offset: 0x0012697A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A051 RID: 41041
			// (set) Token: 0x0600D02D RID: 53293 RVA: 0x0012878D File Offset: 0x0012698D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A052 RID: 41042
			// (set) Token: 0x0600D02E RID: 53294 RVA: 0x001287A5 File Offset: 0x001269A5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A053 RID: 41043
			// (set) Token: 0x0600D02F RID: 53295 RVA: 0x001287BD File Offset: 0x001269BD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A054 RID: 41044
			// (set) Token: 0x0600D030 RID: 53296 RVA: 0x001287D5 File Offset: 0x001269D5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A055 RID: 41045
			// (set) Token: 0x0600D031 RID: 53297 RVA: 0x001287ED File Offset: 0x001269ED
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DCA RID: 3530
		public class EnableRoomMailboxAccountParameters : ParametersBase
		{
			// Token: 0x1700A056 RID: 41046
			// (set) Token: 0x0600D033 RID: 53299 RVA: 0x0012880D File Offset: 0x00126A0D
			public virtual WindowsLiveId MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x1700A057 RID: 41047
			// (set) Token: 0x0600D034 RID: 53300 RVA: 0x00128820 File Offset: 0x00126A20
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x1700A058 RID: 41048
			// (set) Token: 0x0600D035 RID: 53301 RVA: 0x00128833 File Offset: 0x00126A33
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x1700A059 RID: 41049
			// (set) Token: 0x0600D036 RID: 53302 RVA: 0x00128846 File Offset: 0x00126A46
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x1700A05A RID: 41050
			// (set) Token: 0x0600D037 RID: 53303 RVA: 0x00128859 File Offset: 0x00126A59
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x1700A05B RID: 41051
			// (set) Token: 0x0600D038 RID: 53304 RVA: 0x0012886C File Offset: 0x00126A6C
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x1700A05C RID: 41052
			// (set) Token: 0x0600D039 RID: 53305 RVA: 0x0012887F File Offset: 0x00126A7F
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x1700A05D RID: 41053
			// (set) Token: 0x0600D03A RID: 53306 RVA: 0x00128892 File Offset: 0x00126A92
			public virtual Guid ExchangeGuid
			{
				set
				{
					base.PowerSharpParameters["ExchangeGuid"] = value;
				}
			}

			// Token: 0x1700A05E RID: 41054
			// (set) Token: 0x0600D03B RID: 53307 RVA: 0x001288AA File Offset: 0x00126AAA
			public virtual string ForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700A05F RID: 41055
			// (set) Token: 0x0600D03C RID: 53308 RVA: 0x001288C8 File Offset: 0x00126AC8
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x1700A060 RID: 41056
			// (set) Token: 0x0600D03D RID: 53309 RVA: 0x001288E0 File Offset: 0x00126AE0
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x1700A061 RID: 41057
			// (set) Token: 0x0600D03E RID: 53310 RVA: 0x001288F3 File Offset: 0x00126AF3
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x1700A062 RID: 41058
			// (set) Token: 0x0600D03F RID: 53311 RVA: 0x00128906 File Offset: 0x00126B06
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x1700A063 RID: 41059
			// (set) Token: 0x0600D040 RID: 53312 RVA: 0x0012891E File Offset: 0x00126B1E
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x1700A064 RID: 41060
			// (set) Token: 0x0600D041 RID: 53313 RVA: 0x00128931 File Offset: 0x00126B31
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x1700A065 RID: 41061
			// (set) Token: 0x0600D042 RID: 53314 RVA: 0x00128944 File Offset: 0x00126B44
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x1700A066 RID: 41062
			// (set) Token: 0x0600D043 RID: 53315 RVA: 0x00128957 File Offset: 0x00126B57
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x1700A067 RID: 41063
			// (set) Token: 0x0600D044 RID: 53316 RVA: 0x0012896A File Offset: 0x00126B6A
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x1700A068 RID: 41064
			// (set) Token: 0x0600D045 RID: 53317 RVA: 0x0012897D File Offset: 0x00126B7D
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x1700A069 RID: 41065
			// (set) Token: 0x0600D046 RID: 53318 RVA: 0x00128990 File Offset: 0x00126B90
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x1700A06A RID: 41066
			// (set) Token: 0x0600D047 RID: 53319 RVA: 0x001289A3 File Offset: 0x00126BA3
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x1700A06B RID: 41067
			// (set) Token: 0x0600D048 RID: 53320 RVA: 0x001289B6 File Offset: 0x00126BB6
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x1700A06C RID: 41068
			// (set) Token: 0x0600D049 RID: 53321 RVA: 0x001289C9 File Offset: 0x00126BC9
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x1700A06D RID: 41069
			// (set) Token: 0x0600D04A RID: 53322 RVA: 0x001289DC File Offset: 0x00126BDC
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x1700A06E RID: 41070
			// (set) Token: 0x0600D04B RID: 53323 RVA: 0x001289EF File Offset: 0x00126BEF
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x1700A06F RID: 41071
			// (set) Token: 0x0600D04C RID: 53324 RVA: 0x00128A02 File Offset: 0x00126C02
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x1700A070 RID: 41072
			// (set) Token: 0x0600D04D RID: 53325 RVA: 0x00128A15 File Offset: 0x00126C15
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x1700A071 RID: 41073
			// (set) Token: 0x0600D04E RID: 53326 RVA: 0x00128A28 File Offset: 0x00126C28
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x1700A072 RID: 41074
			// (set) Token: 0x0600D04F RID: 53327 RVA: 0x00128A3B File Offset: 0x00126C3B
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x1700A073 RID: 41075
			// (set) Token: 0x0600D050 RID: 53328 RVA: 0x00128A4E File Offset: 0x00126C4E
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x1700A074 RID: 41076
			// (set) Token: 0x0600D051 RID: 53329 RVA: 0x00128A61 File Offset: 0x00126C61
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x1700A075 RID: 41077
			// (set) Token: 0x0600D052 RID: 53330 RVA: 0x00128A74 File Offset: 0x00126C74
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x1700A076 RID: 41078
			// (set) Token: 0x0600D053 RID: 53331 RVA: 0x00128A87 File Offset: 0x00126C87
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x1700A077 RID: 41079
			// (set) Token: 0x0600D054 RID: 53332 RVA: 0x00128A9A File Offset: 0x00126C9A
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x1700A078 RID: 41080
			// (set) Token: 0x0600D055 RID: 53333 RVA: 0x00128AAD File Offset: 0x00126CAD
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x1700A079 RID: 41081
			// (set) Token: 0x0600D056 RID: 53334 RVA: 0x00128AC5 File Offset: 0x00126CC5
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x1700A07A RID: 41082
			// (set) Token: 0x0600D057 RID: 53335 RVA: 0x00128AD8 File Offset: 0x00126CD8
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x1700A07B RID: 41083
			// (set) Token: 0x0600D058 RID: 53336 RVA: 0x00128AEB File Offset: 0x00126CEB
			public virtual RecipientDisplayType? RecipientDisplayType
			{
				set
				{
					base.PowerSharpParameters["RecipientDisplayType"] = value;
				}
			}

			// Token: 0x1700A07C RID: 41084
			// (set) Token: 0x0600D059 RID: 53337 RVA: 0x00128B03 File Offset: 0x00126D03
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x1700A07D RID: 41085
			// (set) Token: 0x0600D05A RID: 53338 RVA: 0x00128B1B File Offset: 0x00126D1B
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x1700A07E RID: 41086
			// (set) Token: 0x0600D05B RID: 53339 RVA: 0x00128B33 File Offset: 0x00126D33
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x1700A07F RID: 41087
			// (set) Token: 0x0600D05C RID: 53340 RVA: 0x00128B4B File Offset: 0x00126D4B
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x1700A080 RID: 41088
			// (set) Token: 0x0600D05D RID: 53341 RVA: 0x00128B63 File Offset: 0x00126D63
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x1700A081 RID: 41089
			// (set) Token: 0x0600D05E RID: 53342 RVA: 0x00128B76 File Offset: 0x00126D76
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x1700A082 RID: 41090
			// (set) Token: 0x0600D05F RID: 53343 RVA: 0x00128B89 File Offset: 0x00126D89
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x1700A083 RID: 41091
			// (set) Token: 0x0600D060 RID: 53344 RVA: 0x00128B9C File Offset: 0x00126D9C
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x1700A084 RID: 41092
			// (set) Token: 0x0600D061 RID: 53345 RVA: 0x00128BAF File Offset: 0x00126DAF
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x1700A085 RID: 41093
			// (set) Token: 0x0600D062 RID: 53346 RVA: 0x00128BC2 File Offset: 0x00126DC2
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x1700A086 RID: 41094
			// (set) Token: 0x0600D063 RID: 53347 RVA: 0x00128BD5 File Offset: 0x00126DD5
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x1700A087 RID: 41095
			// (set) Token: 0x0600D064 RID: 53348 RVA: 0x00128BED File Offset: 0x00126DED
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x1700A088 RID: 41096
			// (set) Token: 0x0600D065 RID: 53349 RVA: 0x00128C00 File Offset: 0x00126E00
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x1700A089 RID: 41097
			// (set) Token: 0x0600D066 RID: 53350 RVA: 0x00128C13 File Offset: 0x00126E13
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x1700A08A RID: 41098
			// (set) Token: 0x0600D067 RID: 53351 RVA: 0x00128C26 File Offset: 0x00126E26
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x1700A08B RID: 41099
			// (set) Token: 0x0600D068 RID: 53352 RVA: 0x00128C39 File Offset: 0x00126E39
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x1700A08C RID: 41100
			// (set) Token: 0x0600D069 RID: 53353 RVA: 0x00128C4C File Offset: 0x00126E4C
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x1700A08D RID: 41101
			// (set) Token: 0x0600D06A RID: 53354 RVA: 0x00128C6A File Offset: 0x00126E6A
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x1700A08E RID: 41102
			// (set) Token: 0x0600D06B RID: 53355 RVA: 0x00128C7D File Offset: 0x00126E7D
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x1700A08F RID: 41103
			// (set) Token: 0x0600D06C RID: 53356 RVA: 0x00128C90 File Offset: 0x00126E90
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x1700A090 RID: 41104
			// (set) Token: 0x0600D06D RID: 53357 RVA: 0x00128CA3 File Offset: 0x00126EA3
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x1700A091 RID: 41105
			// (set) Token: 0x0600D06E RID: 53358 RVA: 0x00128CB6 File Offset: 0x00126EB6
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x1700A092 RID: 41106
			// (set) Token: 0x0600D06F RID: 53359 RVA: 0x00128CC9 File Offset: 0x00126EC9
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x1700A093 RID: 41107
			// (set) Token: 0x0600D070 RID: 53360 RVA: 0x00128CDC File Offset: 0x00126EDC
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x1700A094 RID: 41108
			// (set) Token: 0x0600D071 RID: 53361 RVA: 0x00128CEF File Offset: 0x00126EEF
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x1700A095 RID: 41109
			// (set) Token: 0x0600D072 RID: 53362 RVA: 0x00128D02 File Offset: 0x00126F02
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x1700A096 RID: 41110
			// (set) Token: 0x0600D073 RID: 53363 RVA: 0x00128D15 File Offset: 0x00126F15
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x1700A097 RID: 41111
			// (set) Token: 0x0600D074 RID: 53364 RVA: 0x00128D28 File Offset: 0x00126F28
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x1700A098 RID: 41112
			// (set) Token: 0x0600D075 RID: 53365 RVA: 0x00128D3B File Offset: 0x00126F3B
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x1700A099 RID: 41113
			// (set) Token: 0x0600D076 RID: 53366 RVA: 0x00128D4E File Offset: 0x00126F4E
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x1700A09A RID: 41114
			// (set) Token: 0x0600D077 RID: 53367 RVA: 0x00128D61 File Offset: 0x00126F61
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x1700A09B RID: 41115
			// (set) Token: 0x0600D078 RID: 53368 RVA: 0x00128D79 File Offset: 0x00126F79
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x1700A09C RID: 41116
			// (set) Token: 0x0600D079 RID: 53369 RVA: 0x00128D8C File Offset: 0x00126F8C
			public virtual SecurityIdentifier MasterAccountSid
			{
				set
				{
					base.PowerSharpParameters["MasterAccountSid"] = value;
				}
			}

			// Token: 0x1700A09D RID: 41117
			// (set) Token: 0x0600D07A RID: 53370 RVA: 0x00128D9F File Offset: 0x00126F9F
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x1700A09E RID: 41118
			// (set) Token: 0x0600D07B RID: 53371 RVA: 0x00128DB7 File Offset: 0x00126FB7
			public virtual string IntendedMailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["IntendedMailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x1700A09F RID: 41119
			// (set) Token: 0x0600D07C RID: 53372 RVA: 0x00128DD5 File Offset: 0x00126FD5
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x1700A0A0 RID: 41120
			// (set) Token: 0x0600D07D RID: 53373 RVA: 0x00128DED File Offset: 0x00126FED
			public virtual ExchangeResourceType? ResourceType
			{
				set
				{
					base.PowerSharpParameters["ResourceType"] = value;
				}
			}

			// Token: 0x1700A0A1 RID: 41121
			// (set) Token: 0x0600D07E RID: 53374 RVA: 0x00128E05 File Offset: 0x00127005
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x1700A0A2 RID: 41122
			// (set) Token: 0x0600D07F RID: 53375 RVA: 0x00128E1D File Offset: 0x0012701D
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x1700A0A3 RID: 41123
			// (set) Token: 0x0600D080 RID: 53376 RVA: 0x00128E30 File Offset: 0x00127030
			public virtual MultiValuedProperty<string> ResourceMetaData
			{
				set
				{
					base.PowerSharpParameters["ResourceMetaData"] = value;
				}
			}

			// Token: 0x1700A0A4 RID: 41124
			// (set) Token: 0x0600D081 RID: 53377 RVA: 0x00128E43 File Offset: 0x00127043
			public virtual string ResourcePropertiesDisplay
			{
				set
				{
					base.PowerSharpParameters["ResourcePropertiesDisplay"] = value;
				}
			}

			// Token: 0x1700A0A5 RID: 41125
			// (set) Token: 0x0600D082 RID: 53378 RVA: 0x00128E56 File Offset: 0x00127056
			public virtual MultiValuedProperty<string> ResourceSearchProperties
			{
				set
				{
					base.PowerSharpParameters["ResourceSearchProperties"] = value;
				}
			}

			// Token: 0x1700A0A6 RID: 41126
			// (set) Token: 0x0600D083 RID: 53379 RVA: 0x00128E69 File Offset: 0x00127069
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x1700A0A7 RID: 41127
			// (set) Token: 0x0600D084 RID: 53380 RVA: 0x00128E81 File Offset: 0x00127081
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x1700A0A8 RID: 41128
			// (set) Token: 0x0600D085 RID: 53381 RVA: 0x00128E94 File Offset: 0x00127094
			public virtual bool IsCalculatedTargetAddress
			{
				set
				{
					base.PowerSharpParameters["IsCalculatedTargetAddress"] = value;
				}
			}

			// Token: 0x1700A0A9 RID: 41129
			// (set) Token: 0x0600D086 RID: 53382 RVA: 0x00128EAC File Offset: 0x001270AC
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x1700A0AA RID: 41130
			// (set) Token: 0x0600D087 RID: 53383 RVA: 0x00128EBF File Offset: 0x001270BF
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x1700A0AB RID: 41131
			// (set) Token: 0x0600D088 RID: 53384 RVA: 0x00128ED7 File Offset: 0x001270D7
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x1700A0AC RID: 41132
			// (set) Token: 0x0600D089 RID: 53385 RVA: 0x00128EEA File Offset: 0x001270EA
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x1700A0AD RID: 41133
			// (set) Token: 0x0600D08A RID: 53386 RVA: 0x00128EFD File Offset: 0x001270FD
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x1700A0AE RID: 41134
			// (set) Token: 0x0600D08B RID: 53387 RVA: 0x00128F10 File Offset: 0x00127110
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x1700A0AF RID: 41135
			// (set) Token: 0x0600D08C RID: 53388 RVA: 0x00128F28 File Offset: 0x00127128
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x1700A0B0 RID: 41136
			// (set) Token: 0x0600D08D RID: 53389 RVA: 0x00128F40 File Offset: 0x00127140
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x1700A0B1 RID: 41137
			// (set) Token: 0x0600D08E RID: 53390 RVA: 0x00128F53 File Offset: 0x00127153
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x1700A0B2 RID: 41138
			// (set) Token: 0x0600D08F RID: 53391 RVA: 0x00128F66 File Offset: 0x00127166
			public virtual ElcMailboxFlags ElcMailboxFlags
			{
				set
				{
					base.PowerSharpParameters["ElcMailboxFlags"] = value;
				}
			}

			// Token: 0x1700A0B3 RID: 41139
			// (set) Token: 0x0600D090 RID: 53392 RVA: 0x00128F7E File Offset: 0x0012717E
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x1700A0B4 RID: 41140
			// (set) Token: 0x0600D091 RID: 53393 RVA: 0x00128F96 File Offset: 0x00127196
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x1700A0B5 RID: 41141
			// (set) Token: 0x0600D092 RID: 53394 RVA: 0x00128FAE File Offset: 0x001271AE
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x1700A0B6 RID: 41142
			// (set) Token: 0x0600D093 RID: 53395 RVA: 0x00128FC1 File Offset: 0x001271C1
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x1700A0B7 RID: 41143
			// (set) Token: 0x0600D094 RID: 53396 RVA: 0x00128FD4 File Offset: 0x001271D4
			public virtual bool MailboxAuditEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditEnabled"] = value;
				}
			}

			// Token: 0x1700A0B8 RID: 41144
			// (set) Token: 0x0600D095 RID: 53397 RVA: 0x00128FEC File Offset: 0x001271EC
			public virtual EnhancedTimeSpan MailboxAuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x1700A0B9 RID: 41145
			// (set) Token: 0x0600D096 RID: 53398 RVA: 0x00129004 File Offset: 0x00127204
			public virtual MailboxAuditOperations AuditAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditAdminOperations"] = value;
				}
			}

			// Token: 0x1700A0BA RID: 41146
			// (set) Token: 0x0600D097 RID: 53399 RVA: 0x0012901C File Offset: 0x0012721C
			public virtual MailboxAuditOperations AuditDelegateAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateAdminOperations"] = value;
				}
			}

			// Token: 0x1700A0BB RID: 41147
			// (set) Token: 0x0600D098 RID: 53400 RVA: 0x00129034 File Offset: 0x00127234
			public virtual MailboxAuditOperations AuditDelegateOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateOperations"] = value;
				}
			}

			// Token: 0x1700A0BC RID: 41148
			// (set) Token: 0x0600D099 RID: 53401 RVA: 0x0012904C File Offset: 0x0012724C
			public virtual MailboxAuditOperations AuditOwnerOperations
			{
				set
				{
					base.PowerSharpParameters["AuditOwnerOperations"] = value;
				}
			}

			// Token: 0x1700A0BD RID: 41149
			// (set) Token: 0x0600D09A RID: 53402 RVA: 0x00129064 File Offset: 0x00127264
			public virtual bool BypassAudit
			{
				set
				{
					base.PowerSharpParameters["BypassAudit"] = value;
				}
			}

			// Token: 0x1700A0BE RID: 41150
			// (set) Token: 0x0600D09B RID: 53403 RVA: 0x0012907C File Offset: 0x0012727C
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x1700A0BF RID: 41151
			// (set) Token: 0x0600D09C RID: 53404 RVA: 0x0012908F File Offset: 0x0012728F
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x1700A0C0 RID: 41152
			// (set) Token: 0x0600D09D RID: 53405 RVA: 0x001290A7 File Offset: 0x001272A7
			public virtual RecipientIdParameter SiteMailboxOwners
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxOwners"] = value;
				}
			}

			// Token: 0x1700A0C1 RID: 41153
			// (set) Token: 0x0600D09E RID: 53406 RVA: 0x001290BA File Offset: 0x001272BA
			public virtual RecipientIdParameter SiteMailboxUsers
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxUsers"] = value;
				}
			}

			// Token: 0x1700A0C2 RID: 41154
			// (set) Token: 0x0600D09F RID: 53407 RVA: 0x001290CD File Offset: 0x001272CD
			public virtual DateTime? SiteMailboxClosedTime
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxClosedTime"] = value;
				}
			}

			// Token: 0x1700A0C3 RID: 41155
			// (set) Token: 0x0600D0A0 RID: 53408 RVA: 0x001290E5 File Offset: 0x001272E5
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x1700A0C4 RID: 41156
			// (set) Token: 0x0600D0A1 RID: 53409 RVA: 0x001290F8 File Offset: 0x001272F8
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x1700A0C5 RID: 41157
			// (set) Token: 0x0600D0A2 RID: 53410 RVA: 0x00129110 File Offset: 0x00127310
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x1700A0C6 RID: 41158
			// (set) Token: 0x0600D0A3 RID: 53411 RVA: 0x00129128 File Offset: 0x00127328
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700A0C7 RID: 41159
			// (set) Token: 0x0600D0A4 RID: 53412 RVA: 0x00129140 File Offset: 0x00127340
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x1700A0C8 RID: 41160
			// (set) Token: 0x0600D0A5 RID: 53413 RVA: 0x00129153 File Offset: 0x00127353
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x1700A0C9 RID: 41161
			// (set) Token: 0x0600D0A6 RID: 53414 RVA: 0x00129166 File Offset: 0x00127366
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x1700A0CA RID: 41162
			// (set) Token: 0x0600D0A7 RID: 53415 RVA: 0x00129179 File Offset: 0x00127379
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700A0CB RID: 41163
			// (set) Token: 0x0600D0A8 RID: 53416 RVA: 0x0012918C File Offset: 0x0012738C
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700A0CC RID: 41164
			// (set) Token: 0x0600D0A9 RID: 53417 RVA: 0x0012919F File Offset: 0x0012739F
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700A0CD RID: 41165
			// (set) Token: 0x0600D0AA RID: 53418 RVA: 0x001291B2 File Offset: 0x001273B2
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700A0CE RID: 41166
			// (set) Token: 0x0600D0AB RID: 53419 RVA: 0x001291CA File Offset: 0x001273CA
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700A0CF RID: 41167
			// (set) Token: 0x0600D0AC RID: 53420 RVA: 0x001291DD File Offset: 0x001273DD
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x1700A0D0 RID: 41168
			// (set) Token: 0x0600D0AD RID: 53421 RVA: 0x001291F5 File Offset: 0x001273F5
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x1700A0D1 RID: 41169
			// (set) Token: 0x0600D0AE RID: 53422 RVA: 0x00129208 File Offset: 0x00127408
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x1700A0D2 RID: 41170
			// (set) Token: 0x0600D0AF RID: 53423 RVA: 0x00129220 File Offset: 0x00127420
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700A0D3 RID: 41171
			// (set) Token: 0x0600D0B0 RID: 53424 RVA: 0x00129233 File Offset: 0x00127433
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A0D4 RID: 41172
			// (set) Token: 0x0600D0B1 RID: 53425 RVA: 0x00129251 File Offset: 0x00127451
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700A0D5 RID: 41173
			// (set) Token: 0x0600D0B2 RID: 53426 RVA: 0x00129264 File Offset: 0x00127464
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700A0D6 RID: 41174
			// (set) Token: 0x0600D0B3 RID: 53427 RVA: 0x0012927C File Offset: 0x0012747C
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700A0D7 RID: 41175
			// (set) Token: 0x0600D0B4 RID: 53428 RVA: 0x00129294 File Offset: 0x00127494
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700A0D8 RID: 41176
			// (set) Token: 0x0600D0B5 RID: 53429 RVA: 0x001292AC File Offset: 0x001274AC
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700A0D9 RID: 41177
			// (set) Token: 0x0600D0B6 RID: 53430 RVA: 0x001292BF File Offset: 0x001274BF
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700A0DA RID: 41178
			// (set) Token: 0x0600D0B7 RID: 53431 RVA: 0x001292D7 File Offset: 0x001274D7
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A0DB RID: 41179
			// (set) Token: 0x0600D0B8 RID: 53432 RVA: 0x001292EA File Offset: 0x001274EA
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A0DC RID: 41180
			// (set) Token: 0x0600D0B9 RID: 53433 RVA: 0x001292FD File Offset: 0x001274FD
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A0DD RID: 41181
			// (set) Token: 0x0600D0BA RID: 53434 RVA: 0x0012931B File Offset: 0x0012751B
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700A0DE RID: 41182
			// (set) Token: 0x0600D0BB RID: 53435 RVA: 0x0012932E File Offset: 0x0012752E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A0DF RID: 41183
			// (set) Token: 0x0600D0BC RID: 53436 RVA: 0x0012934C File Offset: 0x0012754C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A0E0 RID: 41184
			// (set) Token: 0x0600D0BD RID: 53437 RVA: 0x0012935F File Offset: 0x0012755F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A0E1 RID: 41185
			// (set) Token: 0x0600D0BE RID: 53438 RVA: 0x00129377 File Offset: 0x00127577
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A0E2 RID: 41186
			// (set) Token: 0x0600D0BF RID: 53439 RVA: 0x0012938F File Offset: 0x0012758F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A0E3 RID: 41187
			// (set) Token: 0x0600D0C0 RID: 53440 RVA: 0x001293A7 File Offset: 0x001275A7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A0E4 RID: 41188
			// (set) Token: 0x0600D0C1 RID: 53441 RVA: 0x001293BF File Offset: 0x001275BF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DCB RID: 3531
		public class MicrosoftOnlineServicesFederatedUserParameters : ParametersBase
		{
			// Token: 0x1700A0E5 RID: 41189
			// (set) Token: 0x0600D0C3 RID: 53443 RVA: 0x001293DF File Offset: 0x001275DF
			public virtual WindowsLiveId MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x1700A0E6 RID: 41190
			// (set) Token: 0x0600D0C4 RID: 53444 RVA: 0x001293F2 File Offset: 0x001275F2
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x1700A0E7 RID: 41191
			// (set) Token: 0x0600D0C5 RID: 53445 RVA: 0x00129405 File Offset: 0x00127605
			public virtual string FederatedIdentity
			{
				set
				{
					base.PowerSharpParameters["FederatedIdentity"] = value;
				}
			}

			// Token: 0x1700A0E8 RID: 41192
			// (set) Token: 0x0600D0C6 RID: 53446 RVA: 0x00129418 File Offset: 0x00127618
			public virtual DeliveryRecipientIdParameter BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x1700A0E9 RID: 41193
			// (set) Token: 0x0600D0C7 RID: 53447 RVA: 0x0012942B File Offset: 0x0012762B
			public virtual DeliveryRecipientIdParameter BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x1700A0EA RID: 41194
			// (set) Token: 0x0600D0C8 RID: 53448 RVA: 0x0012943E File Offset: 0x0012763E
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x1700A0EB RID: 41195
			// (set) Token: 0x0600D0C9 RID: 53449 RVA: 0x00129451 File Offset: 0x00127651
			public virtual DeliveryRecipientIdParameter AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x1700A0EC RID: 41196
			// (set) Token: 0x0600D0CA RID: 53450 RVA: 0x00129464 File Offset: 0x00127664
			public virtual DeliveryRecipientIdParameter RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x1700A0ED RID: 41197
			// (set) Token: 0x0600D0CB RID: 53451 RVA: 0x00129477 File Offset: 0x00127677
			public virtual DeliveryRecipientIdParameter RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x1700A0EE RID: 41198
			// (set) Token: 0x0600D0CC RID: 53452 RVA: 0x0012948A File Offset: 0x0012768A
			public virtual Guid ExchangeGuid
			{
				set
				{
					base.PowerSharpParameters["ExchangeGuid"] = value;
				}
			}

			// Token: 0x1700A0EF RID: 41199
			// (set) Token: 0x0600D0CD RID: 53453 RVA: 0x001294A2 File Offset: 0x001276A2
			public virtual string ForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700A0F0 RID: 41200
			// (set) Token: 0x0600D0CE RID: 53454 RVA: 0x001294C0 File Offset: 0x001276C0
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x1700A0F1 RID: 41201
			// (set) Token: 0x0600D0CF RID: 53455 RVA: 0x001294D8 File Offset: 0x001276D8
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x1700A0F2 RID: 41202
			// (set) Token: 0x0600D0D0 RID: 53456 RVA: 0x001294EB File Offset: 0x001276EB
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x1700A0F3 RID: 41203
			// (set) Token: 0x0600D0D1 RID: 53457 RVA: 0x001294FE File Offset: 0x001276FE
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x1700A0F4 RID: 41204
			// (set) Token: 0x0600D0D2 RID: 53458 RVA: 0x00129516 File Offset: 0x00127716
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x1700A0F5 RID: 41205
			// (set) Token: 0x0600D0D3 RID: 53459 RVA: 0x00129529 File Offset: 0x00127729
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x1700A0F6 RID: 41206
			// (set) Token: 0x0600D0D4 RID: 53460 RVA: 0x0012953C File Offset: 0x0012773C
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x1700A0F7 RID: 41207
			// (set) Token: 0x0600D0D5 RID: 53461 RVA: 0x0012954F File Offset: 0x0012774F
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x1700A0F8 RID: 41208
			// (set) Token: 0x0600D0D6 RID: 53462 RVA: 0x00129562 File Offset: 0x00127762
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x1700A0F9 RID: 41209
			// (set) Token: 0x0600D0D7 RID: 53463 RVA: 0x00129575 File Offset: 0x00127775
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x1700A0FA RID: 41210
			// (set) Token: 0x0600D0D8 RID: 53464 RVA: 0x00129588 File Offset: 0x00127788
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x1700A0FB RID: 41211
			// (set) Token: 0x0600D0D9 RID: 53465 RVA: 0x0012959B File Offset: 0x0012779B
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x1700A0FC RID: 41212
			// (set) Token: 0x0600D0DA RID: 53466 RVA: 0x001295AE File Offset: 0x001277AE
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x1700A0FD RID: 41213
			// (set) Token: 0x0600D0DB RID: 53467 RVA: 0x001295C1 File Offset: 0x001277C1
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x1700A0FE RID: 41214
			// (set) Token: 0x0600D0DC RID: 53468 RVA: 0x001295D4 File Offset: 0x001277D4
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x1700A0FF RID: 41215
			// (set) Token: 0x0600D0DD RID: 53469 RVA: 0x001295E7 File Offset: 0x001277E7
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x1700A100 RID: 41216
			// (set) Token: 0x0600D0DE RID: 53470 RVA: 0x001295FA File Offset: 0x001277FA
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x1700A101 RID: 41217
			// (set) Token: 0x0600D0DF RID: 53471 RVA: 0x0012960D File Offset: 0x0012780D
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x1700A102 RID: 41218
			// (set) Token: 0x0600D0E0 RID: 53472 RVA: 0x00129620 File Offset: 0x00127820
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x1700A103 RID: 41219
			// (set) Token: 0x0600D0E1 RID: 53473 RVA: 0x00129633 File Offset: 0x00127833
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x1700A104 RID: 41220
			// (set) Token: 0x0600D0E2 RID: 53474 RVA: 0x00129646 File Offset: 0x00127846
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x1700A105 RID: 41221
			// (set) Token: 0x0600D0E3 RID: 53475 RVA: 0x00129659 File Offset: 0x00127859
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x1700A106 RID: 41222
			// (set) Token: 0x0600D0E4 RID: 53476 RVA: 0x0012966C File Offset: 0x0012786C
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x1700A107 RID: 41223
			// (set) Token: 0x0600D0E5 RID: 53477 RVA: 0x0012967F File Offset: 0x0012787F
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x1700A108 RID: 41224
			// (set) Token: 0x0600D0E6 RID: 53478 RVA: 0x00129692 File Offset: 0x00127892
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x1700A109 RID: 41225
			// (set) Token: 0x0600D0E7 RID: 53479 RVA: 0x001296A5 File Offset: 0x001278A5
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x1700A10A RID: 41226
			// (set) Token: 0x0600D0E8 RID: 53480 RVA: 0x001296BD File Offset: 0x001278BD
			public virtual RecipientIdParameter GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x1700A10B RID: 41227
			// (set) Token: 0x0600D0E9 RID: 53481 RVA: 0x001296D0 File Offset: 0x001278D0
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x1700A10C RID: 41228
			// (set) Token: 0x0600D0EA RID: 53482 RVA: 0x001296E3 File Offset: 0x001278E3
			public virtual RecipientDisplayType? RecipientDisplayType
			{
				set
				{
					base.PowerSharpParameters["RecipientDisplayType"] = value;
				}
			}

			// Token: 0x1700A10D RID: 41229
			// (set) Token: 0x0600D0EB RID: 53483 RVA: 0x001296FB File Offset: 0x001278FB
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x1700A10E RID: 41230
			// (set) Token: 0x0600D0EC RID: 53484 RVA: 0x00129713 File Offset: 0x00127913
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x1700A10F RID: 41231
			// (set) Token: 0x0600D0ED RID: 53485 RVA: 0x0012972B File Offset: 0x0012792B
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x1700A110 RID: 41232
			// (set) Token: 0x0600D0EE RID: 53486 RVA: 0x00129743 File Offset: 0x00127943
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x1700A111 RID: 41233
			// (set) Token: 0x0600D0EF RID: 53487 RVA: 0x0012975B File Offset: 0x0012795B
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x1700A112 RID: 41234
			// (set) Token: 0x0600D0F0 RID: 53488 RVA: 0x0012976E File Offset: 0x0012796E
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x1700A113 RID: 41235
			// (set) Token: 0x0600D0F1 RID: 53489 RVA: 0x00129781 File Offset: 0x00127981
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x1700A114 RID: 41236
			// (set) Token: 0x0600D0F2 RID: 53490 RVA: 0x00129794 File Offset: 0x00127994
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x1700A115 RID: 41237
			// (set) Token: 0x0600D0F3 RID: 53491 RVA: 0x001297A7 File Offset: 0x001279A7
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x1700A116 RID: 41238
			// (set) Token: 0x0600D0F4 RID: 53492 RVA: 0x001297BA File Offset: 0x001279BA
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x1700A117 RID: 41239
			// (set) Token: 0x0600D0F5 RID: 53493 RVA: 0x001297CD File Offset: 0x001279CD
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x1700A118 RID: 41240
			// (set) Token: 0x0600D0F6 RID: 53494 RVA: 0x001297E5 File Offset: 0x001279E5
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x1700A119 RID: 41241
			// (set) Token: 0x0600D0F7 RID: 53495 RVA: 0x001297F8 File Offset: 0x001279F8
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x1700A11A RID: 41242
			// (set) Token: 0x0600D0F8 RID: 53496 RVA: 0x0012980B File Offset: 0x00127A0B
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x1700A11B RID: 41243
			// (set) Token: 0x0600D0F9 RID: 53497 RVA: 0x0012981E File Offset: 0x00127A1E
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x1700A11C RID: 41244
			// (set) Token: 0x0600D0FA RID: 53498 RVA: 0x00129831 File Offset: 0x00127A31
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x1700A11D RID: 41245
			// (set) Token: 0x0600D0FB RID: 53499 RVA: 0x00129844 File Offset: 0x00127A44
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x1700A11E RID: 41246
			// (set) Token: 0x0600D0FC RID: 53500 RVA: 0x00129862 File Offset: 0x00127A62
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x1700A11F RID: 41247
			// (set) Token: 0x0600D0FD RID: 53501 RVA: 0x00129875 File Offset: 0x00127A75
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x1700A120 RID: 41248
			// (set) Token: 0x0600D0FE RID: 53502 RVA: 0x00129888 File Offset: 0x00127A88
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x1700A121 RID: 41249
			// (set) Token: 0x0600D0FF RID: 53503 RVA: 0x0012989B File Offset: 0x00127A9B
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x1700A122 RID: 41250
			// (set) Token: 0x0600D100 RID: 53504 RVA: 0x001298AE File Offset: 0x00127AAE
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x1700A123 RID: 41251
			// (set) Token: 0x0600D101 RID: 53505 RVA: 0x001298C1 File Offset: 0x00127AC1
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x1700A124 RID: 41252
			// (set) Token: 0x0600D102 RID: 53506 RVA: 0x001298D4 File Offset: 0x00127AD4
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x1700A125 RID: 41253
			// (set) Token: 0x0600D103 RID: 53507 RVA: 0x001298E7 File Offset: 0x00127AE7
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x1700A126 RID: 41254
			// (set) Token: 0x0600D104 RID: 53508 RVA: 0x001298FA File Offset: 0x00127AFA
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x1700A127 RID: 41255
			// (set) Token: 0x0600D105 RID: 53509 RVA: 0x0012990D File Offset: 0x00127B0D
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x1700A128 RID: 41256
			// (set) Token: 0x0600D106 RID: 53510 RVA: 0x00129920 File Offset: 0x00127B20
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x1700A129 RID: 41257
			// (set) Token: 0x0600D107 RID: 53511 RVA: 0x00129933 File Offset: 0x00127B33
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x1700A12A RID: 41258
			// (set) Token: 0x0600D108 RID: 53512 RVA: 0x00129946 File Offset: 0x00127B46
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x1700A12B RID: 41259
			// (set) Token: 0x0600D109 RID: 53513 RVA: 0x00129959 File Offset: 0x00127B59
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x1700A12C RID: 41260
			// (set) Token: 0x0600D10A RID: 53514 RVA: 0x00129971 File Offset: 0x00127B71
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x1700A12D RID: 41261
			// (set) Token: 0x0600D10B RID: 53515 RVA: 0x00129984 File Offset: 0x00127B84
			public virtual SecurityIdentifier MasterAccountSid
			{
				set
				{
					base.PowerSharpParameters["MasterAccountSid"] = value;
				}
			}

			// Token: 0x1700A12E RID: 41262
			// (set) Token: 0x0600D10C RID: 53516 RVA: 0x00129997 File Offset: 0x00127B97
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x1700A12F RID: 41263
			// (set) Token: 0x0600D10D RID: 53517 RVA: 0x001299AF File Offset: 0x00127BAF
			public virtual string IntendedMailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["IntendedMailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x1700A130 RID: 41264
			// (set) Token: 0x0600D10E RID: 53518 RVA: 0x001299CD File Offset: 0x00127BCD
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x1700A131 RID: 41265
			// (set) Token: 0x0600D10F RID: 53519 RVA: 0x001299E5 File Offset: 0x00127BE5
			public virtual ExchangeResourceType? ResourceType
			{
				set
				{
					base.PowerSharpParameters["ResourceType"] = value;
				}
			}

			// Token: 0x1700A132 RID: 41266
			// (set) Token: 0x0600D110 RID: 53520 RVA: 0x001299FD File Offset: 0x00127BFD
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x1700A133 RID: 41267
			// (set) Token: 0x0600D111 RID: 53521 RVA: 0x00129A15 File Offset: 0x00127C15
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x1700A134 RID: 41268
			// (set) Token: 0x0600D112 RID: 53522 RVA: 0x00129A28 File Offset: 0x00127C28
			public virtual MultiValuedProperty<string> ResourceMetaData
			{
				set
				{
					base.PowerSharpParameters["ResourceMetaData"] = value;
				}
			}

			// Token: 0x1700A135 RID: 41269
			// (set) Token: 0x0600D113 RID: 53523 RVA: 0x00129A3B File Offset: 0x00127C3B
			public virtual string ResourcePropertiesDisplay
			{
				set
				{
					base.PowerSharpParameters["ResourcePropertiesDisplay"] = value;
				}
			}

			// Token: 0x1700A136 RID: 41270
			// (set) Token: 0x0600D114 RID: 53524 RVA: 0x00129A4E File Offset: 0x00127C4E
			public virtual MultiValuedProperty<string> ResourceSearchProperties
			{
				set
				{
					base.PowerSharpParameters["ResourceSearchProperties"] = value;
				}
			}

			// Token: 0x1700A137 RID: 41271
			// (set) Token: 0x0600D115 RID: 53525 RVA: 0x00129A61 File Offset: 0x00127C61
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x1700A138 RID: 41272
			// (set) Token: 0x0600D116 RID: 53526 RVA: 0x00129A79 File Offset: 0x00127C79
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x1700A139 RID: 41273
			// (set) Token: 0x0600D117 RID: 53527 RVA: 0x00129A8C File Offset: 0x00127C8C
			public virtual bool IsCalculatedTargetAddress
			{
				set
				{
					base.PowerSharpParameters["IsCalculatedTargetAddress"] = value;
				}
			}

			// Token: 0x1700A13A RID: 41274
			// (set) Token: 0x0600D118 RID: 53528 RVA: 0x00129AA4 File Offset: 0x00127CA4
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x1700A13B RID: 41275
			// (set) Token: 0x0600D119 RID: 53529 RVA: 0x00129AB7 File Offset: 0x00127CB7
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x1700A13C RID: 41276
			// (set) Token: 0x0600D11A RID: 53530 RVA: 0x00129ACF File Offset: 0x00127CCF
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x1700A13D RID: 41277
			// (set) Token: 0x0600D11B RID: 53531 RVA: 0x00129AE2 File Offset: 0x00127CE2
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x1700A13E RID: 41278
			// (set) Token: 0x0600D11C RID: 53532 RVA: 0x00129AF5 File Offset: 0x00127CF5
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x1700A13F RID: 41279
			// (set) Token: 0x0600D11D RID: 53533 RVA: 0x00129B08 File Offset: 0x00127D08
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x1700A140 RID: 41280
			// (set) Token: 0x0600D11E RID: 53534 RVA: 0x00129B20 File Offset: 0x00127D20
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x1700A141 RID: 41281
			// (set) Token: 0x0600D11F RID: 53535 RVA: 0x00129B38 File Offset: 0x00127D38
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x1700A142 RID: 41282
			// (set) Token: 0x0600D120 RID: 53536 RVA: 0x00129B4B File Offset: 0x00127D4B
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x1700A143 RID: 41283
			// (set) Token: 0x0600D121 RID: 53537 RVA: 0x00129B5E File Offset: 0x00127D5E
			public virtual ElcMailboxFlags ElcMailboxFlags
			{
				set
				{
					base.PowerSharpParameters["ElcMailboxFlags"] = value;
				}
			}

			// Token: 0x1700A144 RID: 41284
			// (set) Token: 0x0600D122 RID: 53538 RVA: 0x00129B76 File Offset: 0x00127D76
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x1700A145 RID: 41285
			// (set) Token: 0x0600D123 RID: 53539 RVA: 0x00129B8E File Offset: 0x00127D8E
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x1700A146 RID: 41286
			// (set) Token: 0x0600D124 RID: 53540 RVA: 0x00129BA6 File Offset: 0x00127DA6
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x1700A147 RID: 41287
			// (set) Token: 0x0600D125 RID: 53541 RVA: 0x00129BB9 File Offset: 0x00127DB9
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x1700A148 RID: 41288
			// (set) Token: 0x0600D126 RID: 53542 RVA: 0x00129BCC File Offset: 0x00127DCC
			public virtual bool MailboxAuditEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditEnabled"] = value;
				}
			}

			// Token: 0x1700A149 RID: 41289
			// (set) Token: 0x0600D127 RID: 53543 RVA: 0x00129BE4 File Offset: 0x00127DE4
			public virtual EnhancedTimeSpan MailboxAuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x1700A14A RID: 41290
			// (set) Token: 0x0600D128 RID: 53544 RVA: 0x00129BFC File Offset: 0x00127DFC
			public virtual MailboxAuditOperations AuditAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditAdminOperations"] = value;
				}
			}

			// Token: 0x1700A14B RID: 41291
			// (set) Token: 0x0600D129 RID: 53545 RVA: 0x00129C14 File Offset: 0x00127E14
			public virtual MailboxAuditOperations AuditDelegateAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateAdminOperations"] = value;
				}
			}

			// Token: 0x1700A14C RID: 41292
			// (set) Token: 0x0600D12A RID: 53546 RVA: 0x00129C2C File Offset: 0x00127E2C
			public virtual MailboxAuditOperations AuditDelegateOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateOperations"] = value;
				}
			}

			// Token: 0x1700A14D RID: 41293
			// (set) Token: 0x0600D12B RID: 53547 RVA: 0x00129C44 File Offset: 0x00127E44
			public virtual MailboxAuditOperations AuditOwnerOperations
			{
				set
				{
					base.PowerSharpParameters["AuditOwnerOperations"] = value;
				}
			}

			// Token: 0x1700A14E RID: 41294
			// (set) Token: 0x0600D12C RID: 53548 RVA: 0x00129C5C File Offset: 0x00127E5C
			public virtual bool BypassAudit
			{
				set
				{
					base.PowerSharpParameters["BypassAudit"] = value;
				}
			}

			// Token: 0x1700A14F RID: 41295
			// (set) Token: 0x0600D12D RID: 53549 RVA: 0x00129C74 File Offset: 0x00127E74
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x1700A150 RID: 41296
			// (set) Token: 0x0600D12E RID: 53550 RVA: 0x00129C87 File Offset: 0x00127E87
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x1700A151 RID: 41297
			// (set) Token: 0x0600D12F RID: 53551 RVA: 0x00129C9F File Offset: 0x00127E9F
			public virtual RecipientIdParameter SiteMailboxOwners
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxOwners"] = value;
				}
			}

			// Token: 0x1700A152 RID: 41298
			// (set) Token: 0x0600D130 RID: 53552 RVA: 0x00129CB2 File Offset: 0x00127EB2
			public virtual RecipientIdParameter SiteMailboxUsers
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxUsers"] = value;
				}
			}

			// Token: 0x1700A153 RID: 41299
			// (set) Token: 0x0600D131 RID: 53553 RVA: 0x00129CC5 File Offset: 0x00127EC5
			public virtual DateTime? SiteMailboxClosedTime
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxClosedTime"] = value;
				}
			}

			// Token: 0x1700A154 RID: 41300
			// (set) Token: 0x0600D132 RID: 53554 RVA: 0x00129CDD File Offset: 0x00127EDD
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x1700A155 RID: 41301
			// (set) Token: 0x0600D133 RID: 53555 RVA: 0x00129CF0 File Offset: 0x00127EF0
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x1700A156 RID: 41302
			// (set) Token: 0x0600D134 RID: 53556 RVA: 0x00129D08 File Offset: 0x00127F08
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x1700A157 RID: 41303
			// (set) Token: 0x0600D135 RID: 53557 RVA: 0x00129D20 File Offset: 0x00127F20
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700A158 RID: 41304
			// (set) Token: 0x0600D136 RID: 53558 RVA: 0x00129D38 File Offset: 0x00127F38
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x1700A159 RID: 41305
			// (set) Token: 0x0600D137 RID: 53559 RVA: 0x00129D4B File Offset: 0x00127F4B
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x1700A15A RID: 41306
			// (set) Token: 0x0600D138 RID: 53560 RVA: 0x00129D5E File Offset: 0x00127F5E
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x1700A15B RID: 41307
			// (set) Token: 0x0600D139 RID: 53561 RVA: 0x00129D71 File Offset: 0x00127F71
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700A15C RID: 41308
			// (set) Token: 0x0600D13A RID: 53562 RVA: 0x00129D84 File Offset: 0x00127F84
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700A15D RID: 41309
			// (set) Token: 0x0600D13B RID: 53563 RVA: 0x00129D97 File Offset: 0x00127F97
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700A15E RID: 41310
			// (set) Token: 0x0600D13C RID: 53564 RVA: 0x00129DAA File Offset: 0x00127FAA
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700A15F RID: 41311
			// (set) Token: 0x0600D13D RID: 53565 RVA: 0x00129DC2 File Offset: 0x00127FC2
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700A160 RID: 41312
			// (set) Token: 0x0600D13E RID: 53566 RVA: 0x00129DD5 File Offset: 0x00127FD5
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x1700A161 RID: 41313
			// (set) Token: 0x0600D13F RID: 53567 RVA: 0x00129DED File Offset: 0x00127FED
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x1700A162 RID: 41314
			// (set) Token: 0x0600D140 RID: 53568 RVA: 0x00129E00 File Offset: 0x00128000
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x1700A163 RID: 41315
			// (set) Token: 0x0600D141 RID: 53569 RVA: 0x00129E18 File Offset: 0x00128018
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700A164 RID: 41316
			// (set) Token: 0x0600D142 RID: 53570 RVA: 0x00129E2B File Offset: 0x0012802B
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A165 RID: 41317
			// (set) Token: 0x0600D143 RID: 53571 RVA: 0x00129E49 File Offset: 0x00128049
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700A166 RID: 41318
			// (set) Token: 0x0600D144 RID: 53572 RVA: 0x00129E5C File Offset: 0x0012805C
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700A167 RID: 41319
			// (set) Token: 0x0600D145 RID: 53573 RVA: 0x00129E74 File Offset: 0x00128074
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700A168 RID: 41320
			// (set) Token: 0x0600D146 RID: 53574 RVA: 0x00129E8C File Offset: 0x0012808C
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700A169 RID: 41321
			// (set) Token: 0x0600D147 RID: 53575 RVA: 0x00129EA4 File Offset: 0x001280A4
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700A16A RID: 41322
			// (set) Token: 0x0600D148 RID: 53576 RVA: 0x00129EB7 File Offset: 0x001280B7
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700A16B RID: 41323
			// (set) Token: 0x0600D149 RID: 53577 RVA: 0x00129ECF File Offset: 0x001280CF
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A16C RID: 41324
			// (set) Token: 0x0600D14A RID: 53578 RVA: 0x00129EE2 File Offset: 0x001280E2
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A16D RID: 41325
			// (set) Token: 0x0600D14B RID: 53579 RVA: 0x00129EF5 File Offset: 0x001280F5
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A16E RID: 41326
			// (set) Token: 0x0600D14C RID: 53580 RVA: 0x00129F13 File Offset: 0x00128113
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700A16F RID: 41327
			// (set) Token: 0x0600D14D RID: 53581 RVA: 0x00129F26 File Offset: 0x00128126
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A170 RID: 41328
			// (set) Token: 0x0600D14E RID: 53582 RVA: 0x00129F44 File Offset: 0x00128144
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A171 RID: 41329
			// (set) Token: 0x0600D14F RID: 53583 RVA: 0x00129F57 File Offset: 0x00128157
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A172 RID: 41330
			// (set) Token: 0x0600D150 RID: 53584 RVA: 0x00129F6F File Offset: 0x0012816F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A173 RID: 41331
			// (set) Token: 0x0600D151 RID: 53585 RVA: 0x00129F87 File Offset: 0x00128187
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A174 RID: 41332
			// (set) Token: 0x0600D152 RID: 53586 RVA: 0x00129F9F File Offset: 0x0012819F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A175 RID: 41333
			// (set) Token: 0x0600D153 RID: 53587 RVA: 0x00129FB7 File Offset: 0x001281B7
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
