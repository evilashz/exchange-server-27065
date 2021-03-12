using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002D7 RID: 727
	public class NewPartnerApplicationCommand : SyntheticCommandWithPipelineInput<PartnerApplication, PartnerApplication>
	{
		// Token: 0x060031DC RID: 12764 RVA: 0x00058964 File Offset: 0x00056B64
		private NewPartnerApplicationCommand() : base("New-PartnerApplication")
		{
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x00058971 File Offset: 0x00056B71
		public NewPartnerApplicationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x00058980 File Offset: 0x00056B80
		public virtual NewPartnerApplicationCommand SetParameters(NewPartnerApplicationCommand.ACSTrustApplicationParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x0005898A File Offset: 0x00056B8A
		public virtual NewPartnerApplicationCommand SetParameters(NewPartnerApplicationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x00058994 File Offset: 0x00056B94
		public virtual NewPartnerApplicationCommand SetParameters(NewPartnerApplicationCommand.AuthMetadataUrlParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002D8 RID: 728
		public class ACSTrustApplicationParameterSetParameters : ParametersBase
		{
			// Token: 0x170017E7 RID: 6119
			// (set) Token: 0x060031E1 RID: 12769 RVA: 0x0005899E File Offset: 0x00056B9E
			public virtual string ApplicationIdentifier
			{
				set
				{
					base.PowerSharpParameters["ApplicationIdentifier"] = value;
				}
			}

			// Token: 0x170017E8 RID: 6120
			// (set) Token: 0x060031E2 RID: 12770 RVA: 0x000589B1 File Offset: 0x00056BB1
			public virtual string Realm
			{
				set
				{
					base.PowerSharpParameters["Realm"] = value;
				}
			}

			// Token: 0x170017E9 RID: 6121
			// (set) Token: 0x060031E3 RID: 12771 RVA: 0x000589C4 File Offset: 0x00056BC4
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170017EA RID: 6122
			// (set) Token: 0x060031E4 RID: 12772 RVA: 0x000589DC File Offset: 0x00056BDC
			public virtual bool AcceptSecurityIdentifierInformation
			{
				set
				{
					base.PowerSharpParameters["AcceptSecurityIdentifierInformation"] = value;
				}
			}

			// Token: 0x170017EB RID: 6123
			// (set) Token: 0x060031E5 RID: 12773 RVA: 0x000589F4 File Offset: 0x00056BF4
			public virtual string LinkedAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x170017EC RID: 6124
			// (set) Token: 0x060031E6 RID: 12774 RVA: 0x00058A12 File Offset: 0x00056C12
			public virtual string IssuerIdentifier
			{
				set
				{
					base.PowerSharpParameters["IssuerIdentifier"] = value;
				}
			}

			// Token: 0x170017ED RID: 6125
			// (set) Token: 0x060031E7 RID: 12775 RVA: 0x00058A25 File Offset: 0x00056C25
			public virtual string AppOnlyPermissions
			{
				set
				{
					base.PowerSharpParameters["AppOnlyPermissions"] = value;
				}
			}

			// Token: 0x170017EE RID: 6126
			// (set) Token: 0x060031E8 RID: 12776 RVA: 0x00058A38 File Offset: 0x00056C38
			public virtual string ActAsPermissions
			{
				set
				{
					base.PowerSharpParameters["ActAsPermissions"] = value;
				}
			}

			// Token: 0x170017EF RID: 6127
			// (set) Token: 0x060031E9 RID: 12777 RVA: 0x00058A4B File Offset: 0x00056C4B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170017F0 RID: 6128
			// (set) Token: 0x060031EA RID: 12778 RVA: 0x00058A69 File Offset: 0x00056C69
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170017F1 RID: 6129
			// (set) Token: 0x060031EB RID: 12779 RVA: 0x00058A7C File Offset: 0x00056C7C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170017F2 RID: 6130
			// (set) Token: 0x060031EC RID: 12780 RVA: 0x00058A8F File Offset: 0x00056C8F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170017F3 RID: 6131
			// (set) Token: 0x060031ED RID: 12781 RVA: 0x00058AA7 File Offset: 0x00056CA7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170017F4 RID: 6132
			// (set) Token: 0x060031EE RID: 12782 RVA: 0x00058ABF File Offset: 0x00056CBF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170017F5 RID: 6133
			// (set) Token: 0x060031EF RID: 12783 RVA: 0x00058AD7 File Offset: 0x00056CD7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170017F6 RID: 6134
			// (set) Token: 0x060031F0 RID: 12784 RVA: 0x00058AEF File Offset: 0x00056CEF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020002D9 RID: 729
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170017F7 RID: 6135
			// (set) Token: 0x060031F2 RID: 12786 RVA: 0x00058B0F File Offset: 0x00056D0F
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170017F8 RID: 6136
			// (set) Token: 0x060031F3 RID: 12787 RVA: 0x00058B27 File Offset: 0x00056D27
			public virtual bool AcceptSecurityIdentifierInformation
			{
				set
				{
					base.PowerSharpParameters["AcceptSecurityIdentifierInformation"] = value;
				}
			}

			// Token: 0x170017F9 RID: 6137
			// (set) Token: 0x060031F4 RID: 12788 RVA: 0x00058B3F File Offset: 0x00056D3F
			public virtual string LinkedAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x170017FA RID: 6138
			// (set) Token: 0x060031F5 RID: 12789 RVA: 0x00058B5D File Offset: 0x00056D5D
			public virtual string IssuerIdentifier
			{
				set
				{
					base.PowerSharpParameters["IssuerIdentifier"] = value;
				}
			}

			// Token: 0x170017FB RID: 6139
			// (set) Token: 0x060031F6 RID: 12790 RVA: 0x00058B70 File Offset: 0x00056D70
			public virtual string AppOnlyPermissions
			{
				set
				{
					base.PowerSharpParameters["AppOnlyPermissions"] = value;
				}
			}

			// Token: 0x170017FC RID: 6140
			// (set) Token: 0x060031F7 RID: 12791 RVA: 0x00058B83 File Offset: 0x00056D83
			public virtual string ActAsPermissions
			{
				set
				{
					base.PowerSharpParameters["ActAsPermissions"] = value;
				}
			}

			// Token: 0x170017FD RID: 6141
			// (set) Token: 0x060031F8 RID: 12792 RVA: 0x00058B96 File Offset: 0x00056D96
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170017FE RID: 6142
			// (set) Token: 0x060031F9 RID: 12793 RVA: 0x00058BB4 File Offset: 0x00056DB4
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170017FF RID: 6143
			// (set) Token: 0x060031FA RID: 12794 RVA: 0x00058BC7 File Offset: 0x00056DC7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001800 RID: 6144
			// (set) Token: 0x060031FB RID: 12795 RVA: 0x00058BDA File Offset: 0x00056DDA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001801 RID: 6145
			// (set) Token: 0x060031FC RID: 12796 RVA: 0x00058BF2 File Offset: 0x00056DF2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001802 RID: 6146
			// (set) Token: 0x060031FD RID: 12797 RVA: 0x00058C0A File Offset: 0x00056E0A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001803 RID: 6147
			// (set) Token: 0x060031FE RID: 12798 RVA: 0x00058C22 File Offset: 0x00056E22
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001804 RID: 6148
			// (set) Token: 0x060031FF RID: 12799 RVA: 0x00058C3A File Offset: 0x00056E3A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020002DA RID: 730
		public class AuthMetadataUrlParameterSetParameters : ParametersBase
		{
			// Token: 0x17001805 RID: 6149
			// (set) Token: 0x06003201 RID: 12801 RVA: 0x00058C5A File Offset: 0x00056E5A
			public virtual string AuthMetadataUrl
			{
				set
				{
					base.PowerSharpParameters["AuthMetadataUrl"] = value;
				}
			}

			// Token: 0x17001806 RID: 6150
			// (set) Token: 0x06003202 RID: 12802 RVA: 0x00058C6D File Offset: 0x00056E6D
			public virtual SwitchParameter TrustAnySSLCertificate
			{
				set
				{
					base.PowerSharpParameters["TrustAnySSLCertificate"] = value;
				}
			}

			// Token: 0x17001807 RID: 6151
			// (set) Token: 0x06003203 RID: 12803 RVA: 0x00058C85 File Offset: 0x00056E85
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17001808 RID: 6152
			// (set) Token: 0x06003204 RID: 12804 RVA: 0x00058C9D File Offset: 0x00056E9D
			public virtual bool AcceptSecurityIdentifierInformation
			{
				set
				{
					base.PowerSharpParameters["AcceptSecurityIdentifierInformation"] = value;
				}
			}

			// Token: 0x17001809 RID: 6153
			// (set) Token: 0x06003205 RID: 12805 RVA: 0x00058CB5 File Offset: 0x00056EB5
			public virtual string LinkedAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x1700180A RID: 6154
			// (set) Token: 0x06003206 RID: 12806 RVA: 0x00058CD3 File Offset: 0x00056ED3
			public virtual string IssuerIdentifier
			{
				set
				{
					base.PowerSharpParameters["IssuerIdentifier"] = value;
				}
			}

			// Token: 0x1700180B RID: 6155
			// (set) Token: 0x06003207 RID: 12807 RVA: 0x00058CE6 File Offset: 0x00056EE6
			public virtual string AppOnlyPermissions
			{
				set
				{
					base.PowerSharpParameters["AppOnlyPermissions"] = value;
				}
			}

			// Token: 0x1700180C RID: 6156
			// (set) Token: 0x06003208 RID: 12808 RVA: 0x00058CF9 File Offset: 0x00056EF9
			public virtual string ActAsPermissions
			{
				set
				{
					base.PowerSharpParameters["ActAsPermissions"] = value;
				}
			}

			// Token: 0x1700180D RID: 6157
			// (set) Token: 0x06003209 RID: 12809 RVA: 0x00058D0C File Offset: 0x00056F0C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700180E RID: 6158
			// (set) Token: 0x0600320A RID: 12810 RVA: 0x00058D2A File Offset: 0x00056F2A
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700180F RID: 6159
			// (set) Token: 0x0600320B RID: 12811 RVA: 0x00058D3D File Offset: 0x00056F3D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001810 RID: 6160
			// (set) Token: 0x0600320C RID: 12812 RVA: 0x00058D50 File Offset: 0x00056F50
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001811 RID: 6161
			// (set) Token: 0x0600320D RID: 12813 RVA: 0x00058D68 File Offset: 0x00056F68
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001812 RID: 6162
			// (set) Token: 0x0600320E RID: 12814 RVA: 0x00058D80 File Offset: 0x00056F80
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001813 RID: 6163
			// (set) Token: 0x0600320F RID: 12815 RVA: 0x00058D98 File Offset: 0x00056F98
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001814 RID: 6164
			// (set) Token: 0x06003210 RID: 12816 RVA: 0x00058DB0 File Offset: 0x00056FB0
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
