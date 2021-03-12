using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000564 RID: 1380
	public class RemoveStampGroupCommand : SyntheticCommandWithPipelineInput<StampGroup, StampGroup>
	{
		// Token: 0x060048DA RID: 18650 RVA: 0x00075E3E File Offset: 0x0007403E
		private RemoveStampGroupCommand() : base("Remove-StampGroup")
		{
		}

		// Token: 0x060048DB RID: 18651 RVA: 0x00075E4B File Offset: 0x0007404B
		public RemoveStampGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060048DC RID: 18652 RVA: 0x00075E5A File Offset: 0x0007405A
		public virtual RemoveStampGroupCommand SetParameters(RemoveStampGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060048DD RID: 18653 RVA: 0x00075E64 File Offset: 0x00074064
		public virtual RemoveStampGroupCommand SetParameters(RemoveStampGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000565 RID: 1381
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170029CB RID: 10699
			// (set) Token: 0x060048DE RID: 18654 RVA: 0x00075E6E File Offset: 0x0007406E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170029CC RID: 10700
			// (set) Token: 0x060048DF RID: 18655 RVA: 0x00075E81 File Offset: 0x00074081
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170029CD RID: 10701
			// (set) Token: 0x060048E0 RID: 18656 RVA: 0x00075E99 File Offset: 0x00074099
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170029CE RID: 10702
			// (set) Token: 0x060048E1 RID: 18657 RVA: 0x00075EB1 File Offset: 0x000740B1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170029CF RID: 10703
			// (set) Token: 0x060048E2 RID: 18658 RVA: 0x00075EC9 File Offset: 0x000740C9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170029D0 RID: 10704
			// (set) Token: 0x060048E3 RID: 18659 RVA: 0x00075EE1 File Offset: 0x000740E1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170029D1 RID: 10705
			// (set) Token: 0x060048E4 RID: 18660 RVA: 0x00075EF9 File Offset: 0x000740F9
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000566 RID: 1382
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170029D2 RID: 10706
			// (set) Token: 0x060048E6 RID: 18662 RVA: 0x00075F19 File Offset: 0x00074119
			public virtual StampGroupIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170029D3 RID: 10707
			// (set) Token: 0x060048E7 RID: 18663 RVA: 0x00075F2C File Offset: 0x0007412C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170029D4 RID: 10708
			// (set) Token: 0x060048E8 RID: 18664 RVA: 0x00075F3F File Offset: 0x0007413F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170029D5 RID: 10709
			// (set) Token: 0x060048E9 RID: 18665 RVA: 0x00075F57 File Offset: 0x00074157
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170029D6 RID: 10710
			// (set) Token: 0x060048EA RID: 18666 RVA: 0x00075F6F File Offset: 0x0007416F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170029D7 RID: 10711
			// (set) Token: 0x060048EB RID: 18667 RVA: 0x00075F87 File Offset: 0x00074187
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170029D8 RID: 10712
			// (set) Token: 0x060048EC RID: 18668 RVA: 0x00075F9F File Offset: 0x0007419F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170029D9 RID: 10713
			// (set) Token: 0x060048ED RID: 18669 RVA: 0x00075FB7 File Offset: 0x000741B7
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
