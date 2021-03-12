using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000CE4 RID: 3300
	public class DisableMailContactCommand : SyntheticCommandWithPipelineInput<ADContact, ADContact>
	{
		// Token: 0x0600AD6E RID: 44398 RVA: 0x000FAAB5 File Offset: 0x000F8CB5
		private DisableMailContactCommand() : base("Disable-MailContact")
		{
		}

		// Token: 0x0600AD6F RID: 44399 RVA: 0x000FAAC2 File Offset: 0x000F8CC2
		public DisableMailContactCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600AD70 RID: 44400 RVA: 0x000FAAD1 File Offset: 0x000F8CD1
		public virtual DisableMailContactCommand SetParameters(DisableMailContactCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AD71 RID: 44401 RVA: 0x000FAADB File Offset: 0x000F8CDB
		public virtual DisableMailContactCommand SetParameters(DisableMailContactCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000CE5 RID: 3301
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17007F5F RID: 32607
			// (set) Token: 0x0600AD72 RID: 44402 RVA: 0x000FAAE5 File Offset: 0x000F8CE5
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17007F60 RID: 32608
			// (set) Token: 0x0600AD73 RID: 44403 RVA: 0x000FAAFD File Offset: 0x000F8CFD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007F61 RID: 32609
			// (set) Token: 0x0600AD74 RID: 44404 RVA: 0x000FAB10 File Offset: 0x000F8D10
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007F62 RID: 32610
			// (set) Token: 0x0600AD75 RID: 44405 RVA: 0x000FAB28 File Offset: 0x000F8D28
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007F63 RID: 32611
			// (set) Token: 0x0600AD76 RID: 44406 RVA: 0x000FAB40 File Offset: 0x000F8D40
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007F64 RID: 32612
			// (set) Token: 0x0600AD77 RID: 44407 RVA: 0x000FAB58 File Offset: 0x000F8D58
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007F65 RID: 32613
			// (set) Token: 0x0600AD78 RID: 44408 RVA: 0x000FAB70 File Offset: 0x000F8D70
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17007F66 RID: 32614
			// (set) Token: 0x0600AD79 RID: 44409 RVA: 0x000FAB88 File Offset: 0x000F8D88
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000CE6 RID: 3302
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17007F67 RID: 32615
			// (set) Token: 0x0600AD7B RID: 44411 RVA: 0x000FABA8 File Offset: 0x000F8DA8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailContactIdParameter(value) : null);
				}
			}

			// Token: 0x17007F68 RID: 32616
			// (set) Token: 0x0600AD7C RID: 44412 RVA: 0x000FABC6 File Offset: 0x000F8DC6
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17007F69 RID: 32617
			// (set) Token: 0x0600AD7D RID: 44413 RVA: 0x000FABDE File Offset: 0x000F8DDE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007F6A RID: 32618
			// (set) Token: 0x0600AD7E RID: 44414 RVA: 0x000FABF1 File Offset: 0x000F8DF1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007F6B RID: 32619
			// (set) Token: 0x0600AD7F RID: 44415 RVA: 0x000FAC09 File Offset: 0x000F8E09
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007F6C RID: 32620
			// (set) Token: 0x0600AD80 RID: 44416 RVA: 0x000FAC21 File Offset: 0x000F8E21
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007F6D RID: 32621
			// (set) Token: 0x0600AD81 RID: 44417 RVA: 0x000FAC39 File Offset: 0x000F8E39
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007F6E RID: 32622
			// (set) Token: 0x0600AD82 RID: 44418 RVA: 0x000FAC51 File Offset: 0x000F8E51
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17007F6F RID: 32623
			// (set) Token: 0x0600AD83 RID: 44419 RVA: 0x000FAC69 File Offset: 0x000F8E69
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
