using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009B2 RID: 2482
	public class GetSecurityPrincipalCommand : SyntheticCommandWithPipelineInput<ExtendedSecurityPrincipal, ExtendedSecurityPrincipal>
	{
		// Token: 0x06007CB1 RID: 31921 RVA: 0x000B99A8 File Offset: 0x000B7BA8
		private GetSecurityPrincipalCommand() : base("Get-SecurityPrincipal")
		{
		}

		// Token: 0x06007CB2 RID: 31922 RVA: 0x000B99B5 File Offset: 0x000B7BB5
		public GetSecurityPrincipalCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007CB3 RID: 31923 RVA: 0x000B99C4 File Offset: 0x000B7BC4
		public virtual GetSecurityPrincipalCommand SetParameters(GetSecurityPrincipalCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007CB4 RID: 31924 RVA: 0x000B99CE File Offset: 0x000B7BCE
		public virtual GetSecurityPrincipalCommand SetParameters(GetSecurityPrincipalCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009B3 RID: 2483
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005506 RID: 21766
			// (set) Token: 0x06007CB5 RID: 31925 RVA: 0x000B99D8 File Offset: 0x000B7BD8
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new ExtendedOrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17005507 RID: 21767
			// (set) Token: 0x06007CB6 RID: 31926 RVA: 0x000B99F6 File Offset: 0x000B7BF6
			public virtual SmtpDomain IncludeDomainLocalFrom
			{
				set
				{
					base.PowerSharpParameters["IncludeDomainLocalFrom"] = value;
				}
			}

			// Token: 0x17005508 RID: 21768
			// (set) Token: 0x06007CB7 RID: 31927 RVA: 0x000B9A09 File Offset: 0x000B7C09
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17005509 RID: 21769
			// (set) Token: 0x06007CB8 RID: 31928 RVA: 0x000B9A21 File Offset: 0x000B7C21
			public virtual MultiValuedProperty<SecurityPrincipalType> Types
			{
				set
				{
					base.PowerSharpParameters["Types"] = value;
				}
			}

			// Token: 0x1700550A RID: 21770
			// (set) Token: 0x06007CB9 RID: 31929 RVA: 0x000B9A34 File Offset: 0x000B7C34
			public virtual SwitchParameter RoleGroupAssignable
			{
				set
				{
					base.PowerSharpParameters["RoleGroupAssignable"] = value;
				}
			}

			// Token: 0x1700550B RID: 21771
			// (set) Token: 0x06007CBA RID: 31930 RVA: 0x000B9A4C File Offset: 0x000B7C4C
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700550C RID: 21772
			// (set) Token: 0x06007CBB RID: 31931 RVA: 0x000B9A5F File Offset: 0x000B7C5F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700550D RID: 21773
			// (set) Token: 0x06007CBC RID: 31932 RVA: 0x000B9A7D File Offset: 0x000B7C7D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700550E RID: 21774
			// (set) Token: 0x06007CBD RID: 31933 RVA: 0x000B9A90 File Offset: 0x000B7C90
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700550F RID: 21775
			// (set) Token: 0x06007CBE RID: 31934 RVA: 0x000B9AA8 File Offset: 0x000B7CA8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005510 RID: 21776
			// (set) Token: 0x06007CBF RID: 31935 RVA: 0x000B9AC0 File Offset: 0x000B7CC0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005511 RID: 21777
			// (set) Token: 0x06007CC0 RID: 31936 RVA: 0x000B9AD8 File Offset: 0x000B7CD8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020009B4 RID: 2484
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005512 RID: 21778
			// (set) Token: 0x06007CC2 RID: 31938 RVA: 0x000B9AF8 File Offset: 0x000B7CF8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExtendedSecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x17005513 RID: 21779
			// (set) Token: 0x06007CC3 RID: 31939 RVA: 0x000B9B16 File Offset: 0x000B7D16
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new ExtendedOrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17005514 RID: 21780
			// (set) Token: 0x06007CC4 RID: 31940 RVA: 0x000B9B34 File Offset: 0x000B7D34
			public virtual SmtpDomain IncludeDomainLocalFrom
			{
				set
				{
					base.PowerSharpParameters["IncludeDomainLocalFrom"] = value;
				}
			}

			// Token: 0x17005515 RID: 21781
			// (set) Token: 0x06007CC5 RID: 31941 RVA: 0x000B9B47 File Offset: 0x000B7D47
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17005516 RID: 21782
			// (set) Token: 0x06007CC6 RID: 31942 RVA: 0x000B9B5F File Offset: 0x000B7D5F
			public virtual MultiValuedProperty<SecurityPrincipalType> Types
			{
				set
				{
					base.PowerSharpParameters["Types"] = value;
				}
			}

			// Token: 0x17005517 RID: 21783
			// (set) Token: 0x06007CC7 RID: 31943 RVA: 0x000B9B72 File Offset: 0x000B7D72
			public virtual SwitchParameter RoleGroupAssignable
			{
				set
				{
					base.PowerSharpParameters["RoleGroupAssignable"] = value;
				}
			}

			// Token: 0x17005518 RID: 21784
			// (set) Token: 0x06007CC8 RID: 31944 RVA: 0x000B9B8A File Offset: 0x000B7D8A
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17005519 RID: 21785
			// (set) Token: 0x06007CC9 RID: 31945 RVA: 0x000B9B9D File Offset: 0x000B7D9D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700551A RID: 21786
			// (set) Token: 0x06007CCA RID: 31946 RVA: 0x000B9BBB File Offset: 0x000B7DBB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700551B RID: 21787
			// (set) Token: 0x06007CCB RID: 31947 RVA: 0x000B9BCE File Offset: 0x000B7DCE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700551C RID: 21788
			// (set) Token: 0x06007CCC RID: 31948 RVA: 0x000B9BE6 File Offset: 0x000B7DE6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700551D RID: 21789
			// (set) Token: 0x06007CCD RID: 31949 RVA: 0x000B9BFE File Offset: 0x000B7DFE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700551E RID: 21790
			// (set) Token: 0x06007CCE RID: 31950 RVA: 0x000B9C16 File Offset: 0x000B7E16
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
