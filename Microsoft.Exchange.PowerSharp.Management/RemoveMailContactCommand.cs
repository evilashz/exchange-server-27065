using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000CF0 RID: 3312
	public class RemoveMailContactCommand : SyntheticCommandWithPipelineInputNoOutput<MailContactIdParameter>
	{
		// Token: 0x0600AE02 RID: 44546 RVA: 0x000FB6E9 File Offset: 0x000F98E9
		private RemoveMailContactCommand() : base("Remove-MailContact")
		{
		}

		// Token: 0x0600AE03 RID: 44547 RVA: 0x000FB6F6 File Offset: 0x000F98F6
		public RemoveMailContactCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600AE04 RID: 44548 RVA: 0x000FB705 File Offset: 0x000F9905
		public virtual RemoveMailContactCommand SetParameters(RemoveMailContactCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AE05 RID: 44549 RVA: 0x000FB70F File Offset: 0x000F990F
		public virtual RemoveMailContactCommand SetParameters(RemoveMailContactCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000CF1 RID: 3313
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17007FDB RID: 32731
			// (set) Token: 0x0600AE06 RID: 44550 RVA: 0x000FB719 File Offset: 0x000F9919
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x17007FDC RID: 32732
			// (set) Token: 0x0600AE07 RID: 44551 RVA: 0x000FB731 File Offset: 0x000F9931
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17007FDD RID: 32733
			// (set) Token: 0x0600AE08 RID: 44552 RVA: 0x000FB749 File Offset: 0x000F9949
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007FDE RID: 32734
			// (set) Token: 0x0600AE09 RID: 44553 RVA: 0x000FB75C File Offset: 0x000F995C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007FDF RID: 32735
			// (set) Token: 0x0600AE0A RID: 44554 RVA: 0x000FB774 File Offset: 0x000F9974
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007FE0 RID: 32736
			// (set) Token: 0x0600AE0B RID: 44555 RVA: 0x000FB78C File Offset: 0x000F998C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007FE1 RID: 32737
			// (set) Token: 0x0600AE0C RID: 44556 RVA: 0x000FB7A4 File Offset: 0x000F99A4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007FE2 RID: 32738
			// (set) Token: 0x0600AE0D RID: 44557 RVA: 0x000FB7BC File Offset: 0x000F99BC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17007FE3 RID: 32739
			// (set) Token: 0x0600AE0E RID: 44558 RVA: 0x000FB7D4 File Offset: 0x000F99D4
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000CF2 RID: 3314
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17007FE4 RID: 32740
			// (set) Token: 0x0600AE10 RID: 44560 RVA: 0x000FB7F4 File Offset: 0x000F99F4
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailContactIdParameter(value) : null);
				}
			}

			// Token: 0x17007FE5 RID: 32741
			// (set) Token: 0x0600AE11 RID: 44561 RVA: 0x000FB812 File Offset: 0x000F9A12
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x17007FE6 RID: 32742
			// (set) Token: 0x0600AE12 RID: 44562 RVA: 0x000FB82A File Offset: 0x000F9A2A
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17007FE7 RID: 32743
			// (set) Token: 0x0600AE13 RID: 44563 RVA: 0x000FB842 File Offset: 0x000F9A42
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007FE8 RID: 32744
			// (set) Token: 0x0600AE14 RID: 44564 RVA: 0x000FB855 File Offset: 0x000F9A55
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007FE9 RID: 32745
			// (set) Token: 0x0600AE15 RID: 44565 RVA: 0x000FB86D File Offset: 0x000F9A6D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007FEA RID: 32746
			// (set) Token: 0x0600AE16 RID: 44566 RVA: 0x000FB885 File Offset: 0x000F9A85
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007FEB RID: 32747
			// (set) Token: 0x0600AE17 RID: 44567 RVA: 0x000FB89D File Offset: 0x000F9A9D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007FEC RID: 32748
			// (set) Token: 0x0600AE18 RID: 44568 RVA: 0x000FB8B5 File Offset: 0x000F9AB5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17007FED RID: 32749
			// (set) Token: 0x0600AE19 RID: 44569 RVA: 0x000FB8CD File Offset: 0x000F9ACD
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
