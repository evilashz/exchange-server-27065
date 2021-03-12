using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200083B RID: 2107
	public class SetProvisioningReconciliationConfigCommand : SyntheticCommandWithPipelineInputNoOutput<ProvisioningReconciliationConfig>
	{
		// Token: 0x060068EE RID: 26862 RVA: 0x0009FA4A File Offset: 0x0009DC4A
		private SetProvisioningReconciliationConfigCommand() : base("Set-ProvisioningReconciliationConfig")
		{
		}

		// Token: 0x060068EF RID: 26863 RVA: 0x0009FA57 File Offset: 0x0009DC57
		public SetProvisioningReconciliationConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060068F0 RID: 26864 RVA: 0x0009FA66 File Offset: 0x0009DC66
		public virtual SetProvisioningReconciliationConfigCommand SetParameters(SetProvisioningReconciliationConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200083C RID: 2108
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004431 RID: 17457
			// (set) Token: 0x060068F1 RID: 26865 RVA: 0x0009FA70 File Offset: 0x0009DC70
			public virtual MultiValuedProperty<ReconciliationCookie> ReconciliationCookies
			{
				set
				{
					base.PowerSharpParameters["ReconciliationCookies"] = value;
				}
			}

			// Token: 0x17004432 RID: 17458
			// (set) Token: 0x060068F2 RID: 26866 RVA: 0x0009FA83 File Offset: 0x0009DC83
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004433 RID: 17459
			// (set) Token: 0x060068F3 RID: 26867 RVA: 0x0009FA96 File Offset: 0x0009DC96
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004434 RID: 17460
			// (set) Token: 0x060068F4 RID: 26868 RVA: 0x0009FAAE File Offset: 0x0009DCAE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004435 RID: 17461
			// (set) Token: 0x060068F5 RID: 26869 RVA: 0x0009FAC6 File Offset: 0x0009DCC6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004436 RID: 17462
			// (set) Token: 0x060068F6 RID: 26870 RVA: 0x0009FADE File Offset: 0x0009DCDE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004437 RID: 17463
			// (set) Token: 0x060068F7 RID: 26871 RVA: 0x0009FAF6 File Offset: 0x0009DCF6
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
