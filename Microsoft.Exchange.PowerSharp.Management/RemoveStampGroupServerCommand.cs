using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000567 RID: 1383
	public class RemoveStampGroupServerCommand : SyntheticCommandWithPipelineInput<StampGroup, StampGroup>
	{
		// Token: 0x060048EF RID: 18671 RVA: 0x00075FD7 File Offset: 0x000741D7
		private RemoveStampGroupServerCommand() : base("Remove-StampGroupServer")
		{
		}

		// Token: 0x060048F0 RID: 18672 RVA: 0x00075FE4 File Offset: 0x000741E4
		public RemoveStampGroupServerCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060048F1 RID: 18673 RVA: 0x00075FF3 File Offset: 0x000741F3
		public virtual RemoveStampGroupServerCommand SetParameters(RemoveStampGroupServerCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060048F2 RID: 18674 RVA: 0x00075FFD File Offset: 0x000741FD
		public virtual RemoveStampGroupServerCommand SetParameters(RemoveStampGroupServerCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000568 RID: 1384
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170029DA RID: 10714
			// (set) Token: 0x060048F3 RID: 18675 RVA: 0x00076007 File Offset: 0x00074207
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170029DB RID: 10715
			// (set) Token: 0x060048F4 RID: 18676 RVA: 0x0007601A File Offset: 0x0007421A
			public virtual StampGroupIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170029DC RID: 10716
			// (set) Token: 0x060048F5 RID: 18677 RVA: 0x0007602D File Offset: 0x0007422D
			public virtual SwitchParameter ConfigurationOnly
			{
				set
				{
					base.PowerSharpParameters["ConfigurationOnly"] = value;
				}
			}

			// Token: 0x170029DD RID: 10717
			// (set) Token: 0x060048F6 RID: 18678 RVA: 0x00076045 File Offset: 0x00074245
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170029DE RID: 10718
			// (set) Token: 0x060048F7 RID: 18679 RVA: 0x00076058 File Offset: 0x00074258
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170029DF RID: 10719
			// (set) Token: 0x060048F8 RID: 18680 RVA: 0x00076070 File Offset: 0x00074270
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170029E0 RID: 10720
			// (set) Token: 0x060048F9 RID: 18681 RVA: 0x00076088 File Offset: 0x00074288
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170029E1 RID: 10721
			// (set) Token: 0x060048FA RID: 18682 RVA: 0x000760A0 File Offset: 0x000742A0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170029E2 RID: 10722
			// (set) Token: 0x060048FB RID: 18683 RVA: 0x000760B8 File Offset: 0x000742B8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170029E3 RID: 10723
			// (set) Token: 0x060048FC RID: 18684 RVA: 0x000760D0 File Offset: 0x000742D0
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000569 RID: 1385
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170029E4 RID: 10724
			// (set) Token: 0x060048FE RID: 18686 RVA: 0x000760F0 File Offset: 0x000742F0
			public virtual SwitchParameter ConfigurationOnly
			{
				set
				{
					base.PowerSharpParameters["ConfigurationOnly"] = value;
				}
			}

			// Token: 0x170029E5 RID: 10725
			// (set) Token: 0x060048FF RID: 18687 RVA: 0x00076108 File Offset: 0x00074308
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170029E6 RID: 10726
			// (set) Token: 0x06004900 RID: 18688 RVA: 0x0007611B File Offset: 0x0007431B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170029E7 RID: 10727
			// (set) Token: 0x06004901 RID: 18689 RVA: 0x00076133 File Offset: 0x00074333
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170029E8 RID: 10728
			// (set) Token: 0x06004902 RID: 18690 RVA: 0x0007614B File Offset: 0x0007434B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170029E9 RID: 10729
			// (set) Token: 0x06004903 RID: 18691 RVA: 0x00076163 File Offset: 0x00074363
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170029EA RID: 10730
			// (set) Token: 0x06004904 RID: 18692 RVA: 0x0007617B File Offset: 0x0007437B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170029EB RID: 10731
			// (set) Token: 0x06004905 RID: 18693 RVA: 0x00076193 File Offset: 0x00074393
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
