using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000331 RID: 817
	public class MoveManagementRoleAssignmentCommand : SyntheticCommandWithPipelineInput<ExchangeRoleAssignment, ExchangeRoleAssignment>
	{
		// Token: 0x06003553 RID: 13651 RVA: 0x0005D0AD File Offset: 0x0005B2AD
		private MoveManagementRoleAssignmentCommand() : base("Move-ManagementRoleAssignment")
		{
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x0005D0BA File Offset: 0x0005B2BA
		public MoveManagementRoleAssignmentCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x0005D0C9 File Offset: 0x0005B2C9
		public virtual MoveManagementRoleAssignmentCommand SetParameters(MoveManagementRoleAssignmentCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000332 RID: 818
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001AAA RID: 6826
			// (set) Token: 0x06003556 RID: 13654 RVA: 0x0005D0D3 File Offset: 0x0005B2D3
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleAssignmentIdParameter(value) : null);
				}
			}

			// Token: 0x17001AAB RID: 6827
			// (set) Token: 0x06003557 RID: 13655 RVA: 0x0005D0F1 File Offset: 0x0005B2F1
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x17001AAC RID: 6828
			// (set) Token: 0x06003558 RID: 13656 RVA: 0x0005D10F File Offset: 0x0005B30F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001AAD RID: 6829
			// (set) Token: 0x06003559 RID: 13657 RVA: 0x0005D122 File Offset: 0x0005B322
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001AAE RID: 6830
			// (set) Token: 0x0600355A RID: 13658 RVA: 0x0005D13A File Offset: 0x0005B33A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001AAF RID: 6831
			// (set) Token: 0x0600355B RID: 13659 RVA: 0x0005D152 File Offset: 0x0005B352
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001AB0 RID: 6832
			// (set) Token: 0x0600355C RID: 13660 RVA: 0x0005D16A File Offset: 0x0005B36A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001AB1 RID: 6833
			// (set) Token: 0x0600355D RID: 13661 RVA: 0x0005D182 File Offset: 0x0005B382
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
