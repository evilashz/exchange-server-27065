using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200055B RID: 1371
	public class RemoveDatabaseAvailabilityGroupConfigurationCommand : SyntheticCommandWithPipelineInput<DatabaseAvailabilityGroupConfiguration, DatabaseAvailabilityGroupConfiguration>
	{
		// Token: 0x06004896 RID: 18582 RVA: 0x00075900 File Offset: 0x00073B00
		private RemoveDatabaseAvailabilityGroupConfigurationCommand() : base("Remove-DatabaseAvailabilityGroupConfiguration")
		{
		}

		// Token: 0x06004897 RID: 18583 RVA: 0x0007590D File Offset: 0x00073B0D
		public RemoveDatabaseAvailabilityGroupConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004898 RID: 18584 RVA: 0x0007591C File Offset: 0x00073B1C
		public virtual RemoveDatabaseAvailabilityGroupConfigurationCommand SetParameters(RemoveDatabaseAvailabilityGroupConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004899 RID: 18585 RVA: 0x00075926 File Offset: 0x00073B26
		public virtual RemoveDatabaseAvailabilityGroupConfigurationCommand SetParameters(RemoveDatabaseAvailabilityGroupConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200055C RID: 1372
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002999 RID: 10649
			// (set) Token: 0x0600489A RID: 18586 RVA: 0x00075930 File Offset: 0x00073B30
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700299A RID: 10650
			// (set) Token: 0x0600489B RID: 18587 RVA: 0x00075943 File Offset: 0x00073B43
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700299B RID: 10651
			// (set) Token: 0x0600489C RID: 18588 RVA: 0x0007595B File Offset: 0x00073B5B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700299C RID: 10652
			// (set) Token: 0x0600489D RID: 18589 RVA: 0x00075973 File Offset: 0x00073B73
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700299D RID: 10653
			// (set) Token: 0x0600489E RID: 18590 RVA: 0x0007598B File Offset: 0x00073B8B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700299E RID: 10654
			// (set) Token: 0x0600489F RID: 18591 RVA: 0x000759A3 File Offset: 0x00073BA3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700299F RID: 10655
			// (set) Token: 0x060048A0 RID: 18592 RVA: 0x000759BB File Offset: 0x00073BBB
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200055D RID: 1373
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170029A0 RID: 10656
			// (set) Token: 0x060048A2 RID: 18594 RVA: 0x000759DB File Offset: 0x00073BDB
			public virtual DatabaseAvailabilityGroupConfigurationIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170029A1 RID: 10657
			// (set) Token: 0x060048A3 RID: 18595 RVA: 0x000759EE File Offset: 0x00073BEE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170029A2 RID: 10658
			// (set) Token: 0x060048A4 RID: 18596 RVA: 0x00075A01 File Offset: 0x00073C01
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170029A3 RID: 10659
			// (set) Token: 0x060048A5 RID: 18597 RVA: 0x00075A19 File Offset: 0x00073C19
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170029A4 RID: 10660
			// (set) Token: 0x060048A6 RID: 18598 RVA: 0x00075A31 File Offset: 0x00073C31
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170029A5 RID: 10661
			// (set) Token: 0x060048A7 RID: 18599 RVA: 0x00075A49 File Offset: 0x00073C49
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170029A6 RID: 10662
			// (set) Token: 0x060048A8 RID: 18600 RVA: 0x00075A61 File Offset: 0x00073C61
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170029A7 RID: 10663
			// (set) Token: 0x060048A9 RID: 18601 RVA: 0x00075A79 File Offset: 0x00073C79
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
