using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001E1 RID: 481
	public class GetSiteMailboxProvisioningPolicyCommand : SyntheticCommandWithPipelineInput<TeamMailboxProvisioningPolicy, TeamMailboxProvisioningPolicy>
	{
		// Token: 0x060027A2 RID: 10146 RVA: 0x0004B2E6 File Offset: 0x000494E6
		private GetSiteMailboxProvisioningPolicyCommand() : base("Get-SiteMailboxProvisioningPolicy")
		{
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x0004B2F3 File Offset: 0x000494F3
		public GetSiteMailboxProvisioningPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x0004B302 File Offset: 0x00049502
		public virtual GetSiteMailboxProvisioningPolicyCommand SetParameters(GetSiteMailboxProvisioningPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x0004B30C File Offset: 0x0004950C
		public virtual GetSiteMailboxProvisioningPolicyCommand SetParameters(GetSiteMailboxProvisioningPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001E2 RID: 482
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000F99 RID: 3993
			// (set) Token: 0x060027A6 RID: 10150 RVA: 0x0004B316 File Offset: 0x00049516
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17000F9A RID: 3994
			// (set) Token: 0x060027A7 RID: 10151 RVA: 0x0004B32E File Offset: 0x0004952E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000F9B RID: 3995
			// (set) Token: 0x060027A8 RID: 10152 RVA: 0x0004B34C File Offset: 0x0004954C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000F9C RID: 3996
			// (set) Token: 0x060027A9 RID: 10153 RVA: 0x0004B35F File Offset: 0x0004955F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000F9D RID: 3997
			// (set) Token: 0x060027AA RID: 10154 RVA: 0x0004B377 File Offset: 0x00049577
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000F9E RID: 3998
			// (set) Token: 0x060027AB RID: 10155 RVA: 0x0004B38F File Offset: 0x0004958F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000F9F RID: 3999
			// (set) Token: 0x060027AC RID: 10156 RVA: 0x0004B3A7 File Offset: 0x000495A7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020001E3 RID: 483
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000FA0 RID: 4000
			// (set) Token: 0x060027AE RID: 10158 RVA: 0x0004B3C7 File Offset: 0x000495C7
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000FA1 RID: 4001
			// (set) Token: 0x060027AF RID: 10159 RVA: 0x0004B3E5 File Offset: 0x000495E5
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17000FA2 RID: 4002
			// (set) Token: 0x060027B0 RID: 10160 RVA: 0x0004B3FD File Offset: 0x000495FD
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000FA3 RID: 4003
			// (set) Token: 0x060027B1 RID: 10161 RVA: 0x0004B41B File Offset: 0x0004961B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000FA4 RID: 4004
			// (set) Token: 0x060027B2 RID: 10162 RVA: 0x0004B42E File Offset: 0x0004962E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000FA5 RID: 4005
			// (set) Token: 0x060027B3 RID: 10163 RVA: 0x0004B446 File Offset: 0x00049646
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000FA6 RID: 4006
			// (set) Token: 0x060027B4 RID: 10164 RVA: 0x0004B45E File Offset: 0x0004965E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000FA7 RID: 4007
			// (set) Token: 0x060027B5 RID: 10165 RVA: 0x0004B476 File Offset: 0x00049676
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
