using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000704 RID: 1796
	public class GetHostedConnectionFilterPolicyCommand : SyntheticCommandWithPipelineInput<HostedConnectionFilterPolicy, HostedConnectionFilterPolicy>
	{
		// Token: 0x06005CD8 RID: 23768 RVA: 0x00090176 File Offset: 0x0008E376
		private GetHostedConnectionFilterPolicyCommand() : base("Get-HostedConnectionFilterPolicy")
		{
		}

		// Token: 0x06005CD9 RID: 23769 RVA: 0x00090183 File Offset: 0x0008E383
		public GetHostedConnectionFilterPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005CDA RID: 23770 RVA: 0x00090192 File Offset: 0x0008E392
		public virtual GetHostedConnectionFilterPolicyCommand SetParameters(GetHostedConnectionFilterPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005CDB RID: 23771 RVA: 0x0009019C File Offset: 0x0008E39C
		public virtual GetHostedConnectionFilterPolicyCommand SetParameters(GetHostedConnectionFilterPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000705 RID: 1797
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003A89 RID: 14985
			// (set) Token: 0x06005CDC RID: 23772 RVA: 0x000901A6 File Offset: 0x0008E3A6
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17003A8A RID: 14986
			// (set) Token: 0x06005CDD RID: 23773 RVA: 0x000901BE File Offset: 0x0008E3BE
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003A8B RID: 14987
			// (set) Token: 0x06005CDE RID: 23774 RVA: 0x000901DC File Offset: 0x0008E3DC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003A8C RID: 14988
			// (set) Token: 0x06005CDF RID: 23775 RVA: 0x000901EF File Offset: 0x0008E3EF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003A8D RID: 14989
			// (set) Token: 0x06005CE0 RID: 23776 RVA: 0x00090207 File Offset: 0x0008E407
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003A8E RID: 14990
			// (set) Token: 0x06005CE1 RID: 23777 RVA: 0x0009021F File Offset: 0x0008E41F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003A8F RID: 14991
			// (set) Token: 0x06005CE2 RID: 23778 RVA: 0x00090237 File Offset: 0x0008E437
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000706 RID: 1798
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003A90 RID: 14992
			// (set) Token: 0x06005CE4 RID: 23780 RVA: 0x00090257 File Offset: 0x0008E457
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new HostedConnectionFilterPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17003A91 RID: 14993
			// (set) Token: 0x06005CE5 RID: 23781 RVA: 0x00090275 File Offset: 0x0008E475
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17003A92 RID: 14994
			// (set) Token: 0x06005CE6 RID: 23782 RVA: 0x0009028D File Offset: 0x0008E48D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003A93 RID: 14995
			// (set) Token: 0x06005CE7 RID: 23783 RVA: 0x000902AB File Offset: 0x0008E4AB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003A94 RID: 14996
			// (set) Token: 0x06005CE8 RID: 23784 RVA: 0x000902BE File Offset: 0x0008E4BE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003A95 RID: 14997
			// (set) Token: 0x06005CE9 RID: 23785 RVA: 0x000902D6 File Offset: 0x0008E4D6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003A96 RID: 14998
			// (set) Token: 0x06005CEA RID: 23786 RVA: 0x000902EE File Offset: 0x0008E4EE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003A97 RID: 14999
			// (set) Token: 0x06005CEB RID: 23787 RVA: 0x00090306 File Offset: 0x0008E506
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
