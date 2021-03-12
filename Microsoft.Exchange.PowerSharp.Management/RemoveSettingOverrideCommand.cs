using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005C2 RID: 1474
	public class RemoveSettingOverrideCommand : SyntheticCommandWithPipelineInputNoOutput<SettingOverrideIdParameter>
	{
		// Token: 0x06004CBA RID: 19642 RVA: 0x0007ACE6 File Offset: 0x00078EE6
		private RemoveSettingOverrideCommand() : base("Remove-SettingOverride")
		{
		}

		// Token: 0x06004CBB RID: 19643 RVA: 0x0007ACF3 File Offset: 0x00078EF3
		public RemoveSettingOverrideCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004CBC RID: 19644 RVA: 0x0007AD02 File Offset: 0x00078F02
		public virtual RemoveSettingOverrideCommand SetParameters(RemoveSettingOverrideCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004CBD RID: 19645 RVA: 0x0007AD0C File Offset: 0x00078F0C
		public virtual RemoveSettingOverrideCommand SetParameters(RemoveSettingOverrideCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005C3 RID: 1475
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002CEF RID: 11503
			// (set) Token: 0x06004CBE RID: 19646 RVA: 0x0007AD16 File Offset: 0x00078F16
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002CF0 RID: 11504
			// (set) Token: 0x06004CBF RID: 19647 RVA: 0x0007AD29 File Offset: 0x00078F29
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002CF1 RID: 11505
			// (set) Token: 0x06004CC0 RID: 19648 RVA: 0x0007AD41 File Offset: 0x00078F41
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002CF2 RID: 11506
			// (set) Token: 0x06004CC1 RID: 19649 RVA: 0x0007AD59 File Offset: 0x00078F59
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002CF3 RID: 11507
			// (set) Token: 0x06004CC2 RID: 19650 RVA: 0x0007AD71 File Offset: 0x00078F71
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002CF4 RID: 11508
			// (set) Token: 0x06004CC3 RID: 19651 RVA: 0x0007AD89 File Offset: 0x00078F89
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002CF5 RID: 11509
			// (set) Token: 0x06004CC4 RID: 19652 RVA: 0x0007ADA1 File Offset: 0x00078FA1
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005C4 RID: 1476
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002CF6 RID: 11510
			// (set) Token: 0x06004CC6 RID: 19654 RVA: 0x0007ADC1 File Offset: 0x00078FC1
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SettingOverrideIdParameter(value) : null);
				}
			}

			// Token: 0x17002CF7 RID: 11511
			// (set) Token: 0x06004CC7 RID: 19655 RVA: 0x0007ADDF File Offset: 0x00078FDF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002CF8 RID: 11512
			// (set) Token: 0x06004CC8 RID: 19656 RVA: 0x0007ADF2 File Offset: 0x00078FF2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002CF9 RID: 11513
			// (set) Token: 0x06004CC9 RID: 19657 RVA: 0x0007AE0A File Offset: 0x0007900A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002CFA RID: 11514
			// (set) Token: 0x06004CCA RID: 19658 RVA: 0x0007AE22 File Offset: 0x00079022
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002CFB RID: 11515
			// (set) Token: 0x06004CCB RID: 19659 RVA: 0x0007AE3A File Offset: 0x0007903A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002CFC RID: 11516
			// (set) Token: 0x06004CCC RID: 19660 RVA: 0x0007AE52 File Offset: 0x00079052
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002CFD RID: 11517
			// (set) Token: 0x06004CCD RID: 19661 RVA: 0x0007AE6A File Offset: 0x0007906A
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
