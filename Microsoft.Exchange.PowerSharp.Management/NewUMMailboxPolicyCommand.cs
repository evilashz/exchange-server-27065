using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001E7 RID: 487
	public class NewUMMailboxPolicyCommand : SyntheticCommandWithPipelineInput<UMMailboxPolicy, UMMailboxPolicy>
	{
		// Token: 0x060027DA RID: 10202 RVA: 0x0004B776 File Offset: 0x00049976
		private NewUMMailboxPolicyCommand() : base("New-UMMailboxPolicy")
		{
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x0004B783 File Offset: 0x00049983
		public NewUMMailboxPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x0004B792 File Offset: 0x00049992
		public virtual NewUMMailboxPolicyCommand SetParameters(NewUMMailboxPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001E8 RID: 488
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000FC5 RID: 4037
			// (set) Token: 0x060027DD RID: 10205 RVA: 0x0004B79C File Offset: 0x0004999C
			public virtual string UMDialPlan
			{
				set
				{
					base.PowerSharpParameters["UMDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17000FC6 RID: 4038
			// (set) Token: 0x060027DE RID: 10206 RVA: 0x0004B7BA File Offset: 0x000499BA
			public virtual SwitchParameter SharedUMDialPlan
			{
				set
				{
					base.PowerSharpParameters["SharedUMDialPlan"] = value;
				}
			}

			// Token: 0x17000FC7 RID: 4039
			// (set) Token: 0x060027DF RID: 10207 RVA: 0x0004B7D2 File Offset: 0x000499D2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000FC8 RID: 4040
			// (set) Token: 0x060027E0 RID: 10208 RVA: 0x0004B7F0 File Offset: 0x000499F0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000FC9 RID: 4041
			// (set) Token: 0x060027E1 RID: 10209 RVA: 0x0004B803 File Offset: 0x00049A03
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000FCA RID: 4042
			// (set) Token: 0x060027E2 RID: 10210 RVA: 0x0004B816 File Offset: 0x00049A16
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000FCB RID: 4043
			// (set) Token: 0x060027E3 RID: 10211 RVA: 0x0004B82E File Offset: 0x00049A2E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000FCC RID: 4044
			// (set) Token: 0x060027E4 RID: 10212 RVA: 0x0004B846 File Offset: 0x00049A46
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000FCD RID: 4045
			// (set) Token: 0x060027E5 RID: 10213 RVA: 0x0004B85E File Offset: 0x00049A5E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000FCE RID: 4046
			// (set) Token: 0x060027E6 RID: 10214 RVA: 0x0004B876 File Offset: 0x00049A76
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
