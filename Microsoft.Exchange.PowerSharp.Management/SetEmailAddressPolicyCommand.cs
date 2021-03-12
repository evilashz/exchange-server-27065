using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000849 RID: 2121
	public class SetEmailAddressPolicyCommand : SyntheticCommandWithPipelineInputNoOutput<EmailAddressPolicy>
	{
		// Token: 0x0600698F RID: 27023 RVA: 0x000A06F8 File Offset: 0x0009E8F8
		private SetEmailAddressPolicyCommand() : base("Set-EmailAddressPolicy")
		{
		}

		// Token: 0x06006990 RID: 27024 RVA: 0x000A0705 File Offset: 0x0009E905
		public SetEmailAddressPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006991 RID: 27025 RVA: 0x000A0714 File Offset: 0x0009E914
		public virtual SetEmailAddressPolicyCommand SetParameters(SetEmailAddressPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006992 RID: 27026 RVA: 0x000A071E File Offset: 0x0009E91E
		public virtual SetEmailAddressPolicyCommand SetParameters(SetEmailAddressPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200084A RID: 2122
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170044B6 RID: 17590
			// (set) Token: 0x06006993 RID: 27027 RVA: 0x000A0728 File Offset: 0x0009E928
			public virtual string RecipientFilter
			{
				set
				{
					base.PowerSharpParameters["RecipientFilter"] = value;
				}
			}

			// Token: 0x170044B7 RID: 17591
			// (set) Token: 0x06006994 RID: 27028 RVA: 0x000A073B File Offset: 0x0009E93B
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170044B8 RID: 17592
			// (set) Token: 0x06006995 RID: 27029 RVA: 0x000A0759 File Offset: 0x0009E959
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x170044B9 RID: 17593
			// (set) Token: 0x06006996 RID: 27030 RVA: 0x000A0771 File Offset: 0x0009E971
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170044BA RID: 17594
			// (set) Token: 0x06006997 RID: 27031 RVA: 0x000A0784 File Offset: 0x0009E984
			public virtual WellKnownRecipientType? IncludedRecipients
			{
				set
				{
					base.PowerSharpParameters["IncludedRecipients"] = value;
				}
			}

			// Token: 0x170044BB RID: 17595
			// (set) Token: 0x06006998 RID: 27032 RVA: 0x000A079C File Offset: 0x0009E99C
			public virtual MultiValuedProperty<string> ConditionalDepartment
			{
				set
				{
					base.PowerSharpParameters["ConditionalDepartment"] = value;
				}
			}

			// Token: 0x170044BC RID: 17596
			// (set) Token: 0x06006999 RID: 27033 RVA: 0x000A07AF File Offset: 0x0009E9AF
			public virtual MultiValuedProperty<string> ConditionalCompany
			{
				set
				{
					base.PowerSharpParameters["ConditionalCompany"] = value;
				}
			}

			// Token: 0x170044BD RID: 17597
			// (set) Token: 0x0600699A RID: 27034 RVA: 0x000A07C2 File Offset: 0x0009E9C2
			public virtual MultiValuedProperty<string> ConditionalStateOrProvince
			{
				set
				{
					base.PowerSharpParameters["ConditionalStateOrProvince"] = value;
				}
			}

			// Token: 0x170044BE RID: 17598
			// (set) Token: 0x0600699B RID: 27035 RVA: 0x000A07D5 File Offset: 0x0009E9D5
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute1"] = value;
				}
			}

			// Token: 0x170044BF RID: 17599
			// (set) Token: 0x0600699C RID: 27036 RVA: 0x000A07E8 File Offset: 0x0009E9E8
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute2"] = value;
				}
			}

			// Token: 0x170044C0 RID: 17600
			// (set) Token: 0x0600699D RID: 27037 RVA: 0x000A07FB File Offset: 0x0009E9FB
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute3"] = value;
				}
			}

			// Token: 0x170044C1 RID: 17601
			// (set) Token: 0x0600699E RID: 27038 RVA: 0x000A080E File Offset: 0x0009EA0E
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute4"] = value;
				}
			}

			// Token: 0x170044C2 RID: 17602
			// (set) Token: 0x0600699F RID: 27039 RVA: 0x000A0821 File Offset: 0x0009EA21
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute5"] = value;
				}
			}

			// Token: 0x170044C3 RID: 17603
			// (set) Token: 0x060069A0 RID: 27040 RVA: 0x000A0834 File Offset: 0x0009EA34
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute6"] = value;
				}
			}

			// Token: 0x170044C4 RID: 17604
			// (set) Token: 0x060069A1 RID: 27041 RVA: 0x000A0847 File Offset: 0x0009EA47
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute7"] = value;
				}
			}

			// Token: 0x170044C5 RID: 17605
			// (set) Token: 0x060069A2 RID: 27042 RVA: 0x000A085A File Offset: 0x0009EA5A
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute8"] = value;
				}
			}

			// Token: 0x170044C6 RID: 17606
			// (set) Token: 0x060069A3 RID: 27043 RVA: 0x000A086D File Offset: 0x0009EA6D
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute9"] = value;
				}
			}

			// Token: 0x170044C7 RID: 17607
			// (set) Token: 0x060069A4 RID: 27044 RVA: 0x000A0880 File Offset: 0x0009EA80
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute10"] = value;
				}
			}

			// Token: 0x170044C8 RID: 17608
			// (set) Token: 0x060069A5 RID: 27045 RVA: 0x000A0893 File Offset: 0x0009EA93
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute11"] = value;
				}
			}

			// Token: 0x170044C9 RID: 17609
			// (set) Token: 0x060069A6 RID: 27046 RVA: 0x000A08A6 File Offset: 0x0009EAA6
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute12"] = value;
				}
			}

			// Token: 0x170044CA RID: 17610
			// (set) Token: 0x060069A7 RID: 27047 RVA: 0x000A08B9 File Offset: 0x0009EAB9
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute13"] = value;
				}
			}

			// Token: 0x170044CB RID: 17611
			// (set) Token: 0x060069A8 RID: 27048 RVA: 0x000A08CC File Offset: 0x0009EACC
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute14"] = value;
				}
			}

			// Token: 0x170044CC RID: 17612
			// (set) Token: 0x060069A9 RID: 27049 RVA: 0x000A08DF File Offset: 0x0009EADF
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute15"] = value;
				}
			}

			// Token: 0x170044CD RID: 17613
			// (set) Token: 0x060069AA RID: 27050 RVA: 0x000A08F2 File Offset: 0x0009EAF2
			public virtual EmailAddressPolicyPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x170044CE RID: 17614
			// (set) Token: 0x060069AB RID: 27051 RVA: 0x000A090A File Offset: 0x0009EB0A
			public virtual string EnabledPrimarySMTPAddressTemplate
			{
				set
				{
					base.PowerSharpParameters["EnabledPrimarySMTPAddressTemplate"] = value;
				}
			}

			// Token: 0x170044CF RID: 17615
			// (set) Token: 0x060069AC RID: 27052 RVA: 0x000A091D File Offset: 0x0009EB1D
			public virtual ProxyAddressTemplateCollection EnabledEmailAddressTemplates
			{
				set
				{
					base.PowerSharpParameters["EnabledEmailAddressTemplates"] = value;
				}
			}

			// Token: 0x170044D0 RID: 17616
			// (set) Token: 0x060069AD RID: 27053 RVA: 0x000A0930 File Offset: 0x0009EB30
			public virtual ProxyAddressTemplateCollection DisabledEmailAddressTemplates
			{
				set
				{
					base.PowerSharpParameters["DisabledEmailAddressTemplates"] = value;
				}
			}

			// Token: 0x170044D1 RID: 17617
			// (set) Token: 0x060069AE RID: 27054 RVA: 0x000A0943 File Offset: 0x0009EB43
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170044D2 RID: 17618
			// (set) Token: 0x060069AF RID: 27055 RVA: 0x000A0956 File Offset: 0x0009EB56
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170044D3 RID: 17619
			// (set) Token: 0x060069B0 RID: 27056 RVA: 0x000A096E File Offset: 0x0009EB6E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170044D4 RID: 17620
			// (set) Token: 0x060069B1 RID: 27057 RVA: 0x000A0986 File Offset: 0x0009EB86
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170044D5 RID: 17621
			// (set) Token: 0x060069B2 RID: 27058 RVA: 0x000A099E File Offset: 0x0009EB9E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170044D6 RID: 17622
			// (set) Token: 0x060069B3 RID: 27059 RVA: 0x000A09B6 File Offset: 0x0009EBB6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200084B RID: 2123
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170044D7 RID: 17623
			// (set) Token: 0x060069B5 RID: 27061 RVA: 0x000A09D6 File Offset: 0x0009EBD6
			public virtual EmailAddressPolicyIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170044D8 RID: 17624
			// (set) Token: 0x060069B6 RID: 27062 RVA: 0x000A09E9 File Offset: 0x0009EBE9
			public virtual string RecipientFilter
			{
				set
				{
					base.PowerSharpParameters["RecipientFilter"] = value;
				}
			}

			// Token: 0x170044D9 RID: 17625
			// (set) Token: 0x060069B7 RID: 27063 RVA: 0x000A09FC File Offset: 0x0009EBFC
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170044DA RID: 17626
			// (set) Token: 0x060069B8 RID: 27064 RVA: 0x000A0A1A File Offset: 0x0009EC1A
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x170044DB RID: 17627
			// (set) Token: 0x060069B9 RID: 27065 RVA: 0x000A0A32 File Offset: 0x0009EC32
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170044DC RID: 17628
			// (set) Token: 0x060069BA RID: 27066 RVA: 0x000A0A45 File Offset: 0x0009EC45
			public virtual WellKnownRecipientType? IncludedRecipients
			{
				set
				{
					base.PowerSharpParameters["IncludedRecipients"] = value;
				}
			}

			// Token: 0x170044DD RID: 17629
			// (set) Token: 0x060069BB RID: 27067 RVA: 0x000A0A5D File Offset: 0x0009EC5D
			public virtual MultiValuedProperty<string> ConditionalDepartment
			{
				set
				{
					base.PowerSharpParameters["ConditionalDepartment"] = value;
				}
			}

			// Token: 0x170044DE RID: 17630
			// (set) Token: 0x060069BC RID: 27068 RVA: 0x000A0A70 File Offset: 0x0009EC70
			public virtual MultiValuedProperty<string> ConditionalCompany
			{
				set
				{
					base.PowerSharpParameters["ConditionalCompany"] = value;
				}
			}

			// Token: 0x170044DF RID: 17631
			// (set) Token: 0x060069BD RID: 27069 RVA: 0x000A0A83 File Offset: 0x0009EC83
			public virtual MultiValuedProperty<string> ConditionalStateOrProvince
			{
				set
				{
					base.PowerSharpParameters["ConditionalStateOrProvince"] = value;
				}
			}

			// Token: 0x170044E0 RID: 17632
			// (set) Token: 0x060069BE RID: 27070 RVA: 0x000A0A96 File Offset: 0x0009EC96
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute1"] = value;
				}
			}

			// Token: 0x170044E1 RID: 17633
			// (set) Token: 0x060069BF RID: 27071 RVA: 0x000A0AA9 File Offset: 0x0009ECA9
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute2"] = value;
				}
			}

			// Token: 0x170044E2 RID: 17634
			// (set) Token: 0x060069C0 RID: 27072 RVA: 0x000A0ABC File Offset: 0x0009ECBC
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute3"] = value;
				}
			}

			// Token: 0x170044E3 RID: 17635
			// (set) Token: 0x060069C1 RID: 27073 RVA: 0x000A0ACF File Offset: 0x0009ECCF
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute4"] = value;
				}
			}

			// Token: 0x170044E4 RID: 17636
			// (set) Token: 0x060069C2 RID: 27074 RVA: 0x000A0AE2 File Offset: 0x0009ECE2
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute5"] = value;
				}
			}

			// Token: 0x170044E5 RID: 17637
			// (set) Token: 0x060069C3 RID: 27075 RVA: 0x000A0AF5 File Offset: 0x0009ECF5
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute6"] = value;
				}
			}

			// Token: 0x170044E6 RID: 17638
			// (set) Token: 0x060069C4 RID: 27076 RVA: 0x000A0B08 File Offset: 0x0009ED08
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute7"] = value;
				}
			}

			// Token: 0x170044E7 RID: 17639
			// (set) Token: 0x060069C5 RID: 27077 RVA: 0x000A0B1B File Offset: 0x0009ED1B
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute8"] = value;
				}
			}

			// Token: 0x170044E8 RID: 17640
			// (set) Token: 0x060069C6 RID: 27078 RVA: 0x000A0B2E File Offset: 0x0009ED2E
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute9"] = value;
				}
			}

			// Token: 0x170044E9 RID: 17641
			// (set) Token: 0x060069C7 RID: 27079 RVA: 0x000A0B41 File Offset: 0x0009ED41
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute10"] = value;
				}
			}

			// Token: 0x170044EA RID: 17642
			// (set) Token: 0x060069C8 RID: 27080 RVA: 0x000A0B54 File Offset: 0x0009ED54
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute11"] = value;
				}
			}

			// Token: 0x170044EB RID: 17643
			// (set) Token: 0x060069C9 RID: 27081 RVA: 0x000A0B67 File Offset: 0x0009ED67
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute12"] = value;
				}
			}

			// Token: 0x170044EC RID: 17644
			// (set) Token: 0x060069CA RID: 27082 RVA: 0x000A0B7A File Offset: 0x0009ED7A
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute13"] = value;
				}
			}

			// Token: 0x170044ED RID: 17645
			// (set) Token: 0x060069CB RID: 27083 RVA: 0x000A0B8D File Offset: 0x0009ED8D
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute14"] = value;
				}
			}

			// Token: 0x170044EE RID: 17646
			// (set) Token: 0x060069CC RID: 27084 RVA: 0x000A0BA0 File Offset: 0x0009EDA0
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute15"] = value;
				}
			}

			// Token: 0x170044EF RID: 17647
			// (set) Token: 0x060069CD RID: 27085 RVA: 0x000A0BB3 File Offset: 0x0009EDB3
			public virtual EmailAddressPolicyPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x170044F0 RID: 17648
			// (set) Token: 0x060069CE RID: 27086 RVA: 0x000A0BCB File Offset: 0x0009EDCB
			public virtual string EnabledPrimarySMTPAddressTemplate
			{
				set
				{
					base.PowerSharpParameters["EnabledPrimarySMTPAddressTemplate"] = value;
				}
			}

			// Token: 0x170044F1 RID: 17649
			// (set) Token: 0x060069CF RID: 27087 RVA: 0x000A0BDE File Offset: 0x0009EDDE
			public virtual ProxyAddressTemplateCollection EnabledEmailAddressTemplates
			{
				set
				{
					base.PowerSharpParameters["EnabledEmailAddressTemplates"] = value;
				}
			}

			// Token: 0x170044F2 RID: 17650
			// (set) Token: 0x060069D0 RID: 27088 RVA: 0x000A0BF1 File Offset: 0x0009EDF1
			public virtual ProxyAddressTemplateCollection DisabledEmailAddressTemplates
			{
				set
				{
					base.PowerSharpParameters["DisabledEmailAddressTemplates"] = value;
				}
			}

			// Token: 0x170044F3 RID: 17651
			// (set) Token: 0x060069D1 RID: 27089 RVA: 0x000A0C04 File Offset: 0x0009EE04
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170044F4 RID: 17652
			// (set) Token: 0x060069D2 RID: 27090 RVA: 0x000A0C17 File Offset: 0x0009EE17
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170044F5 RID: 17653
			// (set) Token: 0x060069D3 RID: 27091 RVA: 0x000A0C2F File Offset: 0x0009EE2F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170044F6 RID: 17654
			// (set) Token: 0x060069D4 RID: 27092 RVA: 0x000A0C47 File Offset: 0x0009EE47
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170044F7 RID: 17655
			// (set) Token: 0x060069D5 RID: 27093 RVA: 0x000A0C5F File Offset: 0x0009EE5F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170044F8 RID: 17656
			// (set) Token: 0x060069D6 RID: 27094 RVA: 0x000A0C77 File Offset: 0x0009EE77
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
