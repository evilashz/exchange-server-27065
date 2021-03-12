using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000378 RID: 888
	public class RemoveManagementScopeCommand : SyntheticCommandWithPipelineInput<ManagementScope, ManagementScope>
	{
		// Token: 0x0600382B RID: 14379 RVA: 0x00060B94 File Offset: 0x0005ED94
		private RemoveManagementScopeCommand() : base("Remove-ManagementScope")
		{
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x00060BA1 File Offset: 0x0005EDA1
		public RemoveManagementScopeCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x00060BB0 File Offset: 0x0005EDB0
		public virtual RemoveManagementScopeCommand SetParameters(RemoveManagementScopeCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x00060BBA File Offset: 0x0005EDBA
		public virtual RemoveManagementScopeCommand SetParameters(RemoveManagementScopeCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000379 RID: 889
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001CF4 RID: 7412
			// (set) Token: 0x0600382F RID: 14383 RVA: 0x00060BC4 File Offset: 0x0005EDC4
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001CF5 RID: 7413
			// (set) Token: 0x06003830 RID: 14384 RVA: 0x00060BDC File Offset: 0x0005EDDC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001CF6 RID: 7414
			// (set) Token: 0x06003831 RID: 14385 RVA: 0x00060BEF File Offset: 0x0005EDEF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001CF7 RID: 7415
			// (set) Token: 0x06003832 RID: 14386 RVA: 0x00060C07 File Offset: 0x0005EE07
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001CF8 RID: 7416
			// (set) Token: 0x06003833 RID: 14387 RVA: 0x00060C1F File Offset: 0x0005EE1F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001CF9 RID: 7417
			// (set) Token: 0x06003834 RID: 14388 RVA: 0x00060C37 File Offset: 0x0005EE37
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001CFA RID: 7418
			// (set) Token: 0x06003835 RID: 14389 RVA: 0x00060C4F File Offset: 0x0005EE4F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001CFB RID: 7419
			// (set) Token: 0x06003836 RID: 14390 RVA: 0x00060C67 File Offset: 0x0005EE67
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200037A RID: 890
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001CFC RID: 7420
			// (set) Token: 0x06003838 RID: 14392 RVA: 0x00060C87 File Offset: 0x0005EE87
			public virtual ManagementScopeIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17001CFD RID: 7421
			// (set) Token: 0x06003839 RID: 14393 RVA: 0x00060C9A File Offset: 0x0005EE9A
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001CFE RID: 7422
			// (set) Token: 0x0600383A RID: 14394 RVA: 0x00060CB2 File Offset: 0x0005EEB2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001CFF RID: 7423
			// (set) Token: 0x0600383B RID: 14395 RVA: 0x00060CC5 File Offset: 0x0005EEC5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001D00 RID: 7424
			// (set) Token: 0x0600383C RID: 14396 RVA: 0x00060CDD File Offset: 0x0005EEDD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001D01 RID: 7425
			// (set) Token: 0x0600383D RID: 14397 RVA: 0x00060CF5 File Offset: 0x0005EEF5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001D02 RID: 7426
			// (set) Token: 0x0600383E RID: 14398 RVA: 0x00060D0D File Offset: 0x0005EF0D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001D03 RID: 7427
			// (set) Token: 0x0600383F RID: 14399 RVA: 0x00060D25 File Offset: 0x0005EF25
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001D04 RID: 7428
			// (set) Token: 0x06003840 RID: 14400 RVA: 0x00060D3D File Offset: 0x0005EF3D
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
