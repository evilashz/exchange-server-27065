using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B76 RID: 2934
	public class RemoveUMDialPlanCommand : SyntheticCommandWithPipelineInput<UMDialPlan, UMDialPlan>
	{
		// Token: 0x06008E03 RID: 36355 RVA: 0x000D0063 File Offset: 0x000CE263
		private RemoveUMDialPlanCommand() : base("Remove-UMDialPlan")
		{
		}

		// Token: 0x06008E04 RID: 36356 RVA: 0x000D0070 File Offset: 0x000CE270
		public RemoveUMDialPlanCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008E05 RID: 36357 RVA: 0x000D007F File Offset: 0x000CE27F
		public virtual RemoveUMDialPlanCommand SetParameters(RemoveUMDialPlanCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008E06 RID: 36358 RVA: 0x000D0089 File Offset: 0x000CE289
		public virtual RemoveUMDialPlanCommand SetParameters(RemoveUMDialPlanCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B77 RID: 2935
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170062D0 RID: 25296
			// (set) Token: 0x06008E07 RID: 36359 RVA: 0x000D0093 File Offset: 0x000CE293
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170062D1 RID: 25297
			// (set) Token: 0x06008E08 RID: 36360 RVA: 0x000D00A6 File Offset: 0x000CE2A6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170062D2 RID: 25298
			// (set) Token: 0x06008E09 RID: 36361 RVA: 0x000D00BE File Offset: 0x000CE2BE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170062D3 RID: 25299
			// (set) Token: 0x06008E0A RID: 36362 RVA: 0x000D00D6 File Offset: 0x000CE2D6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170062D4 RID: 25300
			// (set) Token: 0x06008E0B RID: 36363 RVA: 0x000D00EE File Offset: 0x000CE2EE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170062D5 RID: 25301
			// (set) Token: 0x06008E0C RID: 36364 RVA: 0x000D0106 File Offset: 0x000CE306
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170062D6 RID: 25302
			// (set) Token: 0x06008E0D RID: 36365 RVA: 0x000D011E File Offset: 0x000CE31E
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000B78 RID: 2936
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170062D7 RID: 25303
			// (set) Token: 0x06008E0F RID: 36367 RVA: 0x000D013E File Offset: 0x000CE33E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170062D8 RID: 25304
			// (set) Token: 0x06008E10 RID: 36368 RVA: 0x000D015C File Offset: 0x000CE35C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170062D9 RID: 25305
			// (set) Token: 0x06008E11 RID: 36369 RVA: 0x000D016F File Offset: 0x000CE36F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170062DA RID: 25306
			// (set) Token: 0x06008E12 RID: 36370 RVA: 0x000D0187 File Offset: 0x000CE387
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170062DB RID: 25307
			// (set) Token: 0x06008E13 RID: 36371 RVA: 0x000D019F File Offset: 0x000CE39F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170062DC RID: 25308
			// (set) Token: 0x06008E14 RID: 36372 RVA: 0x000D01B7 File Offset: 0x000CE3B7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170062DD RID: 25309
			// (set) Token: 0x06008E15 RID: 36373 RVA: 0x000D01CF File Offset: 0x000CE3CF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170062DE RID: 25310
			// (set) Token: 0x06008E16 RID: 36374 RVA: 0x000D01E7 File Offset: 0x000CE3E7
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
