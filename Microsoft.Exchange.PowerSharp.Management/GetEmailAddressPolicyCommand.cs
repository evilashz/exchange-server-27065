using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200083D RID: 2109
	public class GetEmailAddressPolicyCommand : SyntheticCommandWithPipelineInput<EmailAddressPolicy, EmailAddressPolicy>
	{
		// Token: 0x060068F9 RID: 26873 RVA: 0x0009FB16 File Offset: 0x0009DD16
		private GetEmailAddressPolicyCommand() : base("Get-EmailAddressPolicy")
		{
		}

		// Token: 0x060068FA RID: 26874 RVA: 0x0009FB23 File Offset: 0x0009DD23
		public GetEmailAddressPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060068FB RID: 26875 RVA: 0x0009FB32 File Offset: 0x0009DD32
		public virtual GetEmailAddressPolicyCommand SetParameters(GetEmailAddressPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060068FC RID: 26876 RVA: 0x0009FB3C File Offset: 0x0009DD3C
		public virtual GetEmailAddressPolicyCommand SetParameters(GetEmailAddressPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200083E RID: 2110
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004438 RID: 17464
			// (set) Token: 0x060068FD RID: 26877 RVA: 0x0009FB46 File Offset: 0x0009DD46
			public virtual SwitchParameter IncludeMailboxSettingOnlyPolicy
			{
				set
				{
					base.PowerSharpParameters["IncludeMailboxSettingOnlyPolicy"] = value;
				}
			}

			// Token: 0x17004439 RID: 17465
			// (set) Token: 0x060068FE RID: 26878 RVA: 0x0009FB5E File Offset: 0x0009DD5E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700443A RID: 17466
			// (set) Token: 0x060068FF RID: 26879 RVA: 0x0009FB7C File Offset: 0x0009DD7C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700443B RID: 17467
			// (set) Token: 0x06006900 RID: 26880 RVA: 0x0009FB8F File Offset: 0x0009DD8F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700443C RID: 17468
			// (set) Token: 0x06006901 RID: 26881 RVA: 0x0009FBA7 File Offset: 0x0009DDA7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700443D RID: 17469
			// (set) Token: 0x06006902 RID: 26882 RVA: 0x0009FBBF File Offset: 0x0009DDBF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700443E RID: 17470
			// (set) Token: 0x06006903 RID: 26883 RVA: 0x0009FBD7 File Offset: 0x0009DDD7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200083F RID: 2111
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700443F RID: 17471
			// (set) Token: 0x06006905 RID: 26885 RVA: 0x0009FBF7 File Offset: 0x0009DDF7
			public virtual EmailAddressPolicyIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004440 RID: 17472
			// (set) Token: 0x06006906 RID: 26886 RVA: 0x0009FC0A File Offset: 0x0009DE0A
			public virtual SwitchParameter IncludeMailboxSettingOnlyPolicy
			{
				set
				{
					base.PowerSharpParameters["IncludeMailboxSettingOnlyPolicy"] = value;
				}
			}

			// Token: 0x17004441 RID: 17473
			// (set) Token: 0x06006907 RID: 26887 RVA: 0x0009FC22 File Offset: 0x0009DE22
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17004442 RID: 17474
			// (set) Token: 0x06006908 RID: 26888 RVA: 0x0009FC40 File Offset: 0x0009DE40
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004443 RID: 17475
			// (set) Token: 0x06006909 RID: 26889 RVA: 0x0009FC53 File Offset: 0x0009DE53
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004444 RID: 17476
			// (set) Token: 0x0600690A RID: 26890 RVA: 0x0009FC6B File Offset: 0x0009DE6B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004445 RID: 17477
			// (set) Token: 0x0600690B RID: 26891 RVA: 0x0009FC83 File Offset: 0x0009DE83
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004446 RID: 17478
			// (set) Token: 0x0600690C RID: 26892 RVA: 0x0009FC9B File Offset: 0x0009DE9B
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
