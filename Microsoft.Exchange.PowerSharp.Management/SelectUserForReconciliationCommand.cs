using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BDC RID: 3036
	public class SelectUserForReconciliationCommand : SyntheticCommandWithPipelineInputNoOutput<ADObjectId>
	{
		// Token: 0x060092F2 RID: 37618 RVA: 0x000D6868 File Offset: 0x000D4A68
		private SelectUserForReconciliationCommand() : base("Select-UserForReconciliation")
		{
		}

		// Token: 0x060092F3 RID: 37619 RVA: 0x000D6875 File Offset: 0x000D4A75
		public SelectUserForReconciliationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060092F4 RID: 37620 RVA: 0x000D6884 File Offset: 0x000D4A84
		public virtual SelectUserForReconciliationCommand SetParameters(SelectUserForReconciliationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BDD RID: 3037
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170066F3 RID: 26355
			// (set) Token: 0x060092F5 RID: 37621 RVA: 0x000D688E File Offset: 0x000D4A8E
			public virtual ADObjectId User
			{
				set
				{
					base.PowerSharpParameters["User"] = value;
				}
			}

			// Token: 0x170066F4 RID: 26356
			// (set) Token: 0x060092F6 RID: 37622 RVA: 0x000D68A1 File Offset: 0x000D4AA1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170066F5 RID: 26357
			// (set) Token: 0x060092F7 RID: 37623 RVA: 0x000D68B9 File Offset: 0x000D4AB9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170066F6 RID: 26358
			// (set) Token: 0x060092F8 RID: 37624 RVA: 0x000D68D1 File Offset: 0x000D4AD1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170066F7 RID: 26359
			// (set) Token: 0x060092F9 RID: 37625 RVA: 0x000D68E9 File Offset: 0x000D4AE9
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
