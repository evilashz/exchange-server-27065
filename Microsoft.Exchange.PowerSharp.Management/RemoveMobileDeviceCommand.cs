using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200004C RID: 76
	public class RemoveMobileDeviceCommand : SyntheticCommandWithPipelineInput<MobileDevice, MobileDevice>
	{
		// Token: 0x060016AC RID: 5804 RVA: 0x0003525C File Offset: 0x0003345C
		private RemoveMobileDeviceCommand() : base("Remove-MobileDevice")
		{
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x00035269 File Offset: 0x00033469
		public RemoveMobileDeviceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00035278 File Offset: 0x00033478
		public virtual RemoveMobileDeviceCommand SetParameters(RemoveMobileDeviceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00035282 File Offset: 0x00033482
		public virtual RemoveMobileDeviceCommand SetParameters(RemoveMobileDeviceCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200004D RID: 77
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170001CD RID: 461
			// (set) Token: 0x060016B0 RID: 5808 RVA: 0x0003528C File Offset: 0x0003348C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170001CE RID: 462
			// (set) Token: 0x060016B1 RID: 5809 RVA: 0x0003529F File Offset: 0x0003349F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170001CF RID: 463
			// (set) Token: 0x060016B2 RID: 5810 RVA: 0x000352B7 File Offset: 0x000334B7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170001D0 RID: 464
			// (set) Token: 0x060016B3 RID: 5811 RVA: 0x000352CF File Offset: 0x000334CF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170001D1 RID: 465
			// (set) Token: 0x060016B4 RID: 5812 RVA: 0x000352E7 File Offset: 0x000334E7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170001D2 RID: 466
			// (set) Token: 0x060016B5 RID: 5813 RVA: 0x000352FF File Offset: 0x000334FF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170001D3 RID: 467
			// (set) Token: 0x060016B6 RID: 5814 RVA: 0x00035317 File Offset: 0x00033517
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200004E RID: 78
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170001D4 RID: 468
			// (set) Token: 0x060016B8 RID: 5816 RVA: 0x00035337 File Offset: 0x00033537
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MobileDeviceIdParameter(value) : null);
				}
			}

			// Token: 0x170001D5 RID: 469
			// (set) Token: 0x060016B9 RID: 5817 RVA: 0x00035355 File Offset: 0x00033555
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170001D6 RID: 470
			// (set) Token: 0x060016BA RID: 5818 RVA: 0x00035368 File Offset: 0x00033568
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170001D7 RID: 471
			// (set) Token: 0x060016BB RID: 5819 RVA: 0x00035380 File Offset: 0x00033580
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170001D8 RID: 472
			// (set) Token: 0x060016BC RID: 5820 RVA: 0x00035398 File Offset: 0x00033598
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170001D9 RID: 473
			// (set) Token: 0x060016BD RID: 5821 RVA: 0x000353B0 File Offset: 0x000335B0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170001DA RID: 474
			// (set) Token: 0x060016BE RID: 5822 RVA: 0x000353C8 File Offset: 0x000335C8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170001DB RID: 475
			// (set) Token: 0x060016BF RID: 5823 RVA: 0x000353E0 File Offset: 0x000335E0
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
