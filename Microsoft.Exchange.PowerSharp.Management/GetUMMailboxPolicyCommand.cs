using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001EC RID: 492
	public class GetUMMailboxPolicyCommand : SyntheticCommandWithPipelineInput<UMMailboxPolicy, UMMailboxPolicy>
	{
		// Token: 0x060027FD RID: 10237 RVA: 0x0004BA3A File Offset: 0x00049C3A
		private GetUMMailboxPolicyCommand() : base("Get-UMMailboxPolicy")
		{
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x0004BA47 File Offset: 0x00049C47
		public GetUMMailboxPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x0004BA56 File Offset: 0x00049C56
		public virtual GetUMMailboxPolicyCommand SetParameters(GetUMMailboxPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x0004BA60 File Offset: 0x00049C60
		public virtual GetUMMailboxPolicyCommand SetParameters(GetUMMailboxPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001ED RID: 493
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000FDE RID: 4062
			// (set) Token: 0x06002801 RID: 10241 RVA: 0x0004BA6A File Offset: 0x00049C6A
			public virtual string UMDialPlan
			{
				set
				{
					base.PowerSharpParameters["UMDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17000FDF RID: 4063
			// (set) Token: 0x06002802 RID: 10242 RVA: 0x0004BA88 File Offset: 0x00049C88
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000FE0 RID: 4064
			// (set) Token: 0x06002803 RID: 10243 RVA: 0x0004BAA6 File Offset: 0x00049CA6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000FE1 RID: 4065
			// (set) Token: 0x06002804 RID: 10244 RVA: 0x0004BAB9 File Offset: 0x00049CB9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000FE2 RID: 4066
			// (set) Token: 0x06002805 RID: 10245 RVA: 0x0004BAD1 File Offset: 0x00049CD1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000FE3 RID: 4067
			// (set) Token: 0x06002806 RID: 10246 RVA: 0x0004BAE9 File Offset: 0x00049CE9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000FE4 RID: 4068
			// (set) Token: 0x06002807 RID: 10247 RVA: 0x0004BB01 File Offset: 0x00049D01
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020001EE RID: 494
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000FE5 RID: 4069
			// (set) Token: 0x06002809 RID: 10249 RVA: 0x0004BB21 File Offset: 0x00049D21
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000FE6 RID: 4070
			// (set) Token: 0x0600280A RID: 10250 RVA: 0x0004BB3F File Offset: 0x00049D3F
			public virtual string UMDialPlan
			{
				set
				{
					base.PowerSharpParameters["UMDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17000FE7 RID: 4071
			// (set) Token: 0x0600280B RID: 10251 RVA: 0x0004BB5D File Offset: 0x00049D5D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000FE8 RID: 4072
			// (set) Token: 0x0600280C RID: 10252 RVA: 0x0004BB7B File Offset: 0x00049D7B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000FE9 RID: 4073
			// (set) Token: 0x0600280D RID: 10253 RVA: 0x0004BB8E File Offset: 0x00049D8E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000FEA RID: 4074
			// (set) Token: 0x0600280E RID: 10254 RVA: 0x0004BBA6 File Offset: 0x00049DA6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000FEB RID: 4075
			// (set) Token: 0x0600280F RID: 10255 RVA: 0x0004BBBE File Offset: 0x00049DBE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000FEC RID: 4076
			// (set) Token: 0x06002810 RID: 10256 RVA: 0x0004BBD6 File Offset: 0x00049DD6
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
