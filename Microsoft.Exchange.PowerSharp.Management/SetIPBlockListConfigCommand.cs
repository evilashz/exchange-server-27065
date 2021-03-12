using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000761 RID: 1889
	public class SetIPBlockListConfigCommand : SyntheticCommandWithPipelineInputNoOutput<IPBlockListConfig>
	{
		// Token: 0x06006014 RID: 24596 RVA: 0x00094345 File Offset: 0x00092545
		private SetIPBlockListConfigCommand() : base("Set-IPBlockListConfig")
		{
		}

		// Token: 0x06006015 RID: 24597 RVA: 0x00094352 File Offset: 0x00092552
		public SetIPBlockListConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006016 RID: 24598 RVA: 0x00094361 File Offset: 0x00092561
		public virtual SetIPBlockListConfigCommand SetParameters(SetIPBlockListConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000762 RID: 1890
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003D0B RID: 15627
			// (set) Token: 0x06006017 RID: 24599 RVA: 0x0009436B File Offset: 0x0009256B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003D0C RID: 15628
			// (set) Token: 0x06006018 RID: 24600 RVA: 0x0009437E File Offset: 0x0009257E
			public virtual AsciiString MachineEntryRejectionResponse
			{
				set
				{
					base.PowerSharpParameters["MachineEntryRejectionResponse"] = value;
				}
			}

			// Token: 0x17003D0D RID: 15629
			// (set) Token: 0x06006019 RID: 24601 RVA: 0x00094391 File Offset: 0x00092591
			public virtual AsciiString StaticEntryRejectionResponse
			{
				set
				{
					base.PowerSharpParameters["StaticEntryRejectionResponse"] = value;
				}
			}

			// Token: 0x17003D0E RID: 15630
			// (set) Token: 0x0600601A RID: 24602 RVA: 0x000943A4 File Offset: 0x000925A4
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17003D0F RID: 15631
			// (set) Token: 0x0600601B RID: 24603 RVA: 0x000943BC File Offset: 0x000925BC
			public virtual bool ExternalMailEnabled
			{
				set
				{
					base.PowerSharpParameters["ExternalMailEnabled"] = value;
				}
			}

			// Token: 0x17003D10 RID: 15632
			// (set) Token: 0x0600601C RID: 24604 RVA: 0x000943D4 File Offset: 0x000925D4
			public virtual bool InternalMailEnabled
			{
				set
				{
					base.PowerSharpParameters["InternalMailEnabled"] = value;
				}
			}

			// Token: 0x17003D11 RID: 15633
			// (set) Token: 0x0600601D RID: 24605 RVA: 0x000943EC File Offset: 0x000925EC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003D12 RID: 15634
			// (set) Token: 0x0600601E RID: 24606 RVA: 0x00094404 File Offset: 0x00092604
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003D13 RID: 15635
			// (set) Token: 0x0600601F RID: 24607 RVA: 0x0009441C File Offset: 0x0009261C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003D14 RID: 15636
			// (set) Token: 0x06006020 RID: 24608 RVA: 0x00094434 File Offset: 0x00092634
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003D15 RID: 15637
			// (set) Token: 0x06006021 RID: 24609 RVA: 0x0009444C File Offset: 0x0009264C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
