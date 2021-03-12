using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B18 RID: 2840
	public class DisableUMServiceCommand : SyntheticCommandWithPipelineInput<Server, Server>
	{
		// Token: 0x06008B25 RID: 35621 RVA: 0x000CC63C File Offset: 0x000CA83C
		private DisableUMServiceCommand() : base("Disable-UMService")
		{
		}

		// Token: 0x06008B26 RID: 35622 RVA: 0x000CC649 File Offset: 0x000CA849
		public DisableUMServiceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008B27 RID: 35623 RVA: 0x000CC658 File Offset: 0x000CA858
		public virtual DisableUMServiceCommand SetParameters(DisableUMServiceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008B28 RID: 35624 RVA: 0x000CC662 File Offset: 0x000CA862
		public virtual DisableUMServiceCommand SetParameters(DisableUMServiceCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B19 RID: 2841
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170060AE RID: 24750
			// (set) Token: 0x06008B29 RID: 35625 RVA: 0x000CC66C File Offset: 0x000CA86C
			public virtual bool Immediate
			{
				set
				{
					base.PowerSharpParameters["Immediate"] = value;
				}
			}

			// Token: 0x170060AF RID: 24751
			// (set) Token: 0x06008B2A RID: 35626 RVA: 0x000CC684 File Offset: 0x000CA884
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170060B0 RID: 24752
			// (set) Token: 0x06008B2B RID: 35627 RVA: 0x000CC697 File Offset: 0x000CA897
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170060B1 RID: 24753
			// (set) Token: 0x06008B2C RID: 35628 RVA: 0x000CC6AF File Offset: 0x000CA8AF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170060B2 RID: 24754
			// (set) Token: 0x06008B2D RID: 35629 RVA: 0x000CC6C7 File Offset: 0x000CA8C7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170060B3 RID: 24755
			// (set) Token: 0x06008B2E RID: 35630 RVA: 0x000CC6DF File Offset: 0x000CA8DF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170060B4 RID: 24756
			// (set) Token: 0x06008B2F RID: 35631 RVA: 0x000CC6F7 File Offset: 0x000CA8F7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170060B5 RID: 24757
			// (set) Token: 0x06008B30 RID: 35632 RVA: 0x000CC70F File Offset: 0x000CA90F
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000B1A RID: 2842
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170060B6 RID: 24758
			// (set) Token: 0x06008B32 RID: 35634 RVA: 0x000CC72F File Offset: 0x000CA92F
			public virtual UMServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170060B7 RID: 24759
			// (set) Token: 0x06008B33 RID: 35635 RVA: 0x000CC742 File Offset: 0x000CA942
			public virtual bool Immediate
			{
				set
				{
					base.PowerSharpParameters["Immediate"] = value;
				}
			}

			// Token: 0x170060B8 RID: 24760
			// (set) Token: 0x06008B34 RID: 35636 RVA: 0x000CC75A File Offset: 0x000CA95A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170060B9 RID: 24761
			// (set) Token: 0x06008B35 RID: 35637 RVA: 0x000CC76D File Offset: 0x000CA96D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170060BA RID: 24762
			// (set) Token: 0x06008B36 RID: 35638 RVA: 0x000CC785 File Offset: 0x000CA985
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170060BB RID: 24763
			// (set) Token: 0x06008B37 RID: 35639 RVA: 0x000CC79D File Offset: 0x000CA99D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170060BC RID: 24764
			// (set) Token: 0x06008B38 RID: 35640 RVA: 0x000CC7B5 File Offset: 0x000CA9B5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170060BD RID: 24765
			// (set) Token: 0x06008B39 RID: 35641 RVA: 0x000CC7CD File Offset: 0x000CA9CD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170060BE RID: 24766
			// (set) Token: 0x06008B3A RID: 35642 RVA: 0x000CC7E5 File Offset: 0x000CA9E5
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
