using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B0F RID: 2831
	public class DisableUMIPGatewayCommand : SyntheticCommandWithPipelineInput<UMIPGateway, UMIPGateway>
	{
		// Token: 0x06008ADC RID: 35548 RVA: 0x000CC060 File Offset: 0x000CA260
		private DisableUMIPGatewayCommand() : base("Disable-UMIPGateway")
		{
		}

		// Token: 0x06008ADD RID: 35549 RVA: 0x000CC06D File Offset: 0x000CA26D
		public DisableUMIPGatewayCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008ADE RID: 35550 RVA: 0x000CC07C File Offset: 0x000CA27C
		public virtual DisableUMIPGatewayCommand SetParameters(DisableUMIPGatewayCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008ADF RID: 35551 RVA: 0x000CC086 File Offset: 0x000CA286
		public virtual DisableUMIPGatewayCommand SetParameters(DisableUMIPGatewayCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B10 RID: 2832
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006077 RID: 24695
			// (set) Token: 0x06008AE0 RID: 35552 RVA: 0x000CC090 File Offset: 0x000CA290
			public virtual bool Immediate
			{
				set
				{
					base.PowerSharpParameters["Immediate"] = value;
				}
			}

			// Token: 0x17006078 RID: 24696
			// (set) Token: 0x06008AE1 RID: 35553 RVA: 0x000CC0A8 File Offset: 0x000CA2A8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006079 RID: 24697
			// (set) Token: 0x06008AE2 RID: 35554 RVA: 0x000CC0BB File Offset: 0x000CA2BB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700607A RID: 24698
			// (set) Token: 0x06008AE3 RID: 35555 RVA: 0x000CC0D3 File Offset: 0x000CA2D3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700607B RID: 24699
			// (set) Token: 0x06008AE4 RID: 35556 RVA: 0x000CC0EB File Offset: 0x000CA2EB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700607C RID: 24700
			// (set) Token: 0x06008AE5 RID: 35557 RVA: 0x000CC103 File Offset: 0x000CA303
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700607D RID: 24701
			// (set) Token: 0x06008AE6 RID: 35558 RVA: 0x000CC11B File Offset: 0x000CA31B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700607E RID: 24702
			// (set) Token: 0x06008AE7 RID: 35559 RVA: 0x000CC133 File Offset: 0x000CA333
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000B11 RID: 2833
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700607F RID: 24703
			// (set) Token: 0x06008AE9 RID: 35561 RVA: 0x000CC153 File Offset: 0x000CA353
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMIPGatewayIdParameter(value) : null);
				}
			}

			// Token: 0x17006080 RID: 24704
			// (set) Token: 0x06008AEA RID: 35562 RVA: 0x000CC171 File Offset: 0x000CA371
			public virtual bool Immediate
			{
				set
				{
					base.PowerSharpParameters["Immediate"] = value;
				}
			}

			// Token: 0x17006081 RID: 24705
			// (set) Token: 0x06008AEB RID: 35563 RVA: 0x000CC189 File Offset: 0x000CA389
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006082 RID: 24706
			// (set) Token: 0x06008AEC RID: 35564 RVA: 0x000CC19C File Offset: 0x000CA39C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006083 RID: 24707
			// (set) Token: 0x06008AED RID: 35565 RVA: 0x000CC1B4 File Offset: 0x000CA3B4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006084 RID: 24708
			// (set) Token: 0x06008AEE RID: 35566 RVA: 0x000CC1CC File Offset: 0x000CA3CC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006085 RID: 24709
			// (set) Token: 0x06008AEF RID: 35567 RVA: 0x000CC1E4 File Offset: 0x000CA3E4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006086 RID: 24710
			// (set) Token: 0x06008AF0 RID: 35568 RVA: 0x000CC1FC File Offset: 0x000CA3FC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006087 RID: 24711
			// (set) Token: 0x06008AF1 RID: 35569 RVA: 0x000CC214 File Offset: 0x000CA414
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
