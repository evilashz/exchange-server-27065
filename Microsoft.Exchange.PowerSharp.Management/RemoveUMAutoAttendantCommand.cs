using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B80 RID: 2944
	public class RemoveUMAutoAttendantCommand : SyntheticCommandWithPipelineInput<UMAutoAttendant, UMAutoAttendant>
	{
		// Token: 0x06008E4E RID: 36430 RVA: 0x000D0652 File Offset: 0x000CE852
		private RemoveUMAutoAttendantCommand() : base("Remove-UMAutoAttendant")
		{
		}

		// Token: 0x06008E4F RID: 36431 RVA: 0x000D065F File Offset: 0x000CE85F
		public RemoveUMAutoAttendantCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008E50 RID: 36432 RVA: 0x000D066E File Offset: 0x000CE86E
		public virtual RemoveUMAutoAttendantCommand SetParameters(RemoveUMAutoAttendantCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008E51 RID: 36433 RVA: 0x000D0678 File Offset: 0x000CE878
		public virtual RemoveUMAutoAttendantCommand SetParameters(RemoveUMAutoAttendantCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B81 RID: 2945
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006307 RID: 25351
			// (set) Token: 0x06008E52 RID: 36434 RVA: 0x000D0682 File Offset: 0x000CE882
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006308 RID: 25352
			// (set) Token: 0x06008E53 RID: 36435 RVA: 0x000D0695 File Offset: 0x000CE895
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006309 RID: 25353
			// (set) Token: 0x06008E54 RID: 36436 RVA: 0x000D06AD File Offset: 0x000CE8AD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700630A RID: 25354
			// (set) Token: 0x06008E55 RID: 36437 RVA: 0x000D06C5 File Offset: 0x000CE8C5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700630B RID: 25355
			// (set) Token: 0x06008E56 RID: 36438 RVA: 0x000D06DD File Offset: 0x000CE8DD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700630C RID: 25356
			// (set) Token: 0x06008E57 RID: 36439 RVA: 0x000D06F5 File Offset: 0x000CE8F5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700630D RID: 25357
			// (set) Token: 0x06008E58 RID: 36440 RVA: 0x000D070D File Offset: 0x000CE90D
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000B82 RID: 2946
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700630E RID: 25358
			// (set) Token: 0x06008E5A RID: 36442 RVA: 0x000D072D File Offset: 0x000CE92D
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMAutoAttendantIdParameter(value) : null);
				}
			}

			// Token: 0x1700630F RID: 25359
			// (set) Token: 0x06008E5B RID: 36443 RVA: 0x000D074B File Offset: 0x000CE94B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006310 RID: 25360
			// (set) Token: 0x06008E5C RID: 36444 RVA: 0x000D075E File Offset: 0x000CE95E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006311 RID: 25361
			// (set) Token: 0x06008E5D RID: 36445 RVA: 0x000D0776 File Offset: 0x000CE976
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006312 RID: 25362
			// (set) Token: 0x06008E5E RID: 36446 RVA: 0x000D078E File Offset: 0x000CE98E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006313 RID: 25363
			// (set) Token: 0x06008E5F RID: 36447 RVA: 0x000D07A6 File Offset: 0x000CE9A6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006314 RID: 25364
			// (set) Token: 0x06008E60 RID: 36448 RVA: 0x000D07BE File Offset: 0x000CE9BE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006315 RID: 25365
			// (set) Token: 0x06008E61 RID: 36449 RVA: 0x000D07D6 File Offset: 0x000CE9D6
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
