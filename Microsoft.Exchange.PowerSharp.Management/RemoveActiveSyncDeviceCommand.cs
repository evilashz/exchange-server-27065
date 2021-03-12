using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200004F RID: 79
	public class RemoveActiveSyncDeviceCommand : SyntheticCommandWithPipelineInputNoOutput<MobileDeviceIdParameter>
	{
		// Token: 0x060016C1 RID: 5825 RVA: 0x00035400 File Offset: 0x00033600
		private RemoveActiveSyncDeviceCommand() : base("Remove-ActiveSyncDevice")
		{
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0003540D File Offset: 0x0003360D
		public RemoveActiveSyncDeviceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0003541C File Offset: 0x0003361C
		public virtual RemoveActiveSyncDeviceCommand SetParameters(RemoveActiveSyncDeviceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x00035426 File Offset: 0x00033626
		public virtual RemoveActiveSyncDeviceCommand SetParameters(RemoveActiveSyncDeviceCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000050 RID: 80
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170001DC RID: 476
			// (set) Token: 0x060016C5 RID: 5829 RVA: 0x00035430 File Offset: 0x00033630
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170001DD RID: 477
			// (set) Token: 0x060016C6 RID: 5830 RVA: 0x00035443 File Offset: 0x00033643
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170001DE RID: 478
			// (set) Token: 0x060016C7 RID: 5831 RVA: 0x0003545B File Offset: 0x0003365B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170001DF RID: 479
			// (set) Token: 0x060016C8 RID: 5832 RVA: 0x00035473 File Offset: 0x00033673
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170001E0 RID: 480
			// (set) Token: 0x060016C9 RID: 5833 RVA: 0x0003548B File Offset: 0x0003368B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170001E1 RID: 481
			// (set) Token: 0x060016CA RID: 5834 RVA: 0x000354A3 File Offset: 0x000336A3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170001E2 RID: 482
			// (set) Token: 0x060016CB RID: 5835 RVA: 0x000354BB File Offset: 0x000336BB
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000051 RID: 81
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170001E3 RID: 483
			// (set) Token: 0x060016CD RID: 5837 RVA: 0x000354DB File Offset: 0x000336DB
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MobileDeviceIdParameter(value) : null);
				}
			}

			// Token: 0x170001E4 RID: 484
			// (set) Token: 0x060016CE RID: 5838 RVA: 0x000354F9 File Offset: 0x000336F9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170001E5 RID: 485
			// (set) Token: 0x060016CF RID: 5839 RVA: 0x0003550C File Offset: 0x0003370C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170001E6 RID: 486
			// (set) Token: 0x060016D0 RID: 5840 RVA: 0x00035524 File Offset: 0x00033724
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170001E7 RID: 487
			// (set) Token: 0x060016D1 RID: 5841 RVA: 0x0003553C File Offset: 0x0003373C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170001E8 RID: 488
			// (set) Token: 0x060016D2 RID: 5842 RVA: 0x00035554 File Offset: 0x00033754
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170001E9 RID: 489
			// (set) Token: 0x060016D3 RID: 5843 RVA: 0x0003556C File Offset: 0x0003376C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170001EA RID: 490
			// (set) Token: 0x060016D4 RID: 5844 RVA: 0x00035584 File Offset: 0x00033784
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
