using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B12 RID: 2834
	public class DisableUMMailboxCommand : SyntheticCommandWithPipelineInput<MailboxIdParameter, MailboxIdParameter>
	{
		// Token: 0x06008AF3 RID: 35571 RVA: 0x000CC234 File Offset: 0x000CA434
		private DisableUMMailboxCommand() : base("Disable-UMMailbox")
		{
		}

		// Token: 0x06008AF4 RID: 35572 RVA: 0x000CC241 File Offset: 0x000CA441
		public DisableUMMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008AF5 RID: 35573 RVA: 0x000CC250 File Offset: 0x000CA450
		public virtual DisableUMMailboxCommand SetParameters(DisableUMMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008AF6 RID: 35574 RVA: 0x000CC25A File Offset: 0x000CA45A
		public virtual DisableUMMailboxCommand SetParameters(DisableUMMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B13 RID: 2835
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006088 RID: 24712
			// (set) Token: 0x06008AF7 RID: 35575 RVA: 0x000CC264 File Offset: 0x000CA464
			public virtual bool KeepProperties
			{
				set
				{
					base.PowerSharpParameters["KeepProperties"] = value;
				}
			}

			// Token: 0x17006089 RID: 24713
			// (set) Token: 0x06008AF8 RID: 35576 RVA: 0x000CC27C File Offset: 0x000CA47C
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700608A RID: 24714
			// (set) Token: 0x06008AF9 RID: 35577 RVA: 0x000CC294 File Offset: 0x000CA494
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700608B RID: 24715
			// (set) Token: 0x06008AFA RID: 35578 RVA: 0x000CC2A7 File Offset: 0x000CA4A7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700608C RID: 24716
			// (set) Token: 0x06008AFB RID: 35579 RVA: 0x000CC2BF File Offset: 0x000CA4BF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700608D RID: 24717
			// (set) Token: 0x06008AFC RID: 35580 RVA: 0x000CC2D7 File Offset: 0x000CA4D7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700608E RID: 24718
			// (set) Token: 0x06008AFD RID: 35581 RVA: 0x000CC2EF File Offset: 0x000CA4EF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700608F RID: 24719
			// (set) Token: 0x06008AFE RID: 35582 RVA: 0x000CC307 File Offset: 0x000CA507
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006090 RID: 24720
			// (set) Token: 0x06008AFF RID: 35583 RVA: 0x000CC31F File Offset: 0x000CA51F
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000B14 RID: 2836
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006091 RID: 24721
			// (set) Token: 0x06008B01 RID: 35585 RVA: 0x000CC33F File Offset: 0x000CA53F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006092 RID: 24722
			// (set) Token: 0x06008B02 RID: 35586 RVA: 0x000CC35D File Offset: 0x000CA55D
			public virtual bool KeepProperties
			{
				set
				{
					base.PowerSharpParameters["KeepProperties"] = value;
				}
			}

			// Token: 0x17006093 RID: 24723
			// (set) Token: 0x06008B03 RID: 35587 RVA: 0x000CC375 File Offset: 0x000CA575
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006094 RID: 24724
			// (set) Token: 0x06008B04 RID: 35588 RVA: 0x000CC38D File Offset: 0x000CA58D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006095 RID: 24725
			// (set) Token: 0x06008B05 RID: 35589 RVA: 0x000CC3A0 File Offset: 0x000CA5A0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006096 RID: 24726
			// (set) Token: 0x06008B06 RID: 35590 RVA: 0x000CC3B8 File Offset: 0x000CA5B8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006097 RID: 24727
			// (set) Token: 0x06008B07 RID: 35591 RVA: 0x000CC3D0 File Offset: 0x000CA5D0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006098 RID: 24728
			// (set) Token: 0x06008B08 RID: 35592 RVA: 0x000CC3E8 File Offset: 0x000CA5E8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006099 RID: 24729
			// (set) Token: 0x06008B09 RID: 35593 RVA: 0x000CC400 File Offset: 0x000CA600
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700609A RID: 24730
			// (set) Token: 0x06008B0A RID: 35594 RVA: 0x000CC418 File Offset: 0x000CA618
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
