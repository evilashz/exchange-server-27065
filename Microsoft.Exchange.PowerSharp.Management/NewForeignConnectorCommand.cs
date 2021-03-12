using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008A0 RID: 2208
	public class NewForeignConnectorCommand : SyntheticCommandWithPipelineInput<ForeignConnector, ForeignConnector>
	{
		// Token: 0x06006D78 RID: 28024 RVA: 0x000A5990 File Offset: 0x000A3B90
		private NewForeignConnectorCommand() : base("New-ForeignConnector")
		{
		}

		// Token: 0x06006D79 RID: 28025 RVA: 0x000A599D File Offset: 0x000A3B9D
		public NewForeignConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006D7A RID: 28026 RVA: 0x000A59AC File Offset: 0x000A3BAC
		public virtual NewForeignConnectorCommand SetParameters(NewForeignConnectorCommand.AddressSpacesParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006D7B RID: 28027 RVA: 0x000A59B6 File Offset: 0x000A3BB6
		public virtual NewForeignConnectorCommand SetParameters(NewForeignConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008A1 RID: 2209
		public class AddressSpacesParameters : ParametersBase
		{
			// Token: 0x170047F1 RID: 18417
			// (set) Token: 0x06006D7C RID: 28028 RVA: 0x000A59C0 File Offset: 0x000A3BC0
			public virtual MultiValuedProperty<AddressSpace> AddressSpaces
			{
				set
				{
					base.PowerSharpParameters["AddressSpaces"] = value;
				}
			}

			// Token: 0x170047F2 RID: 18418
			// (set) Token: 0x06006D7D RID: 28029 RVA: 0x000A59D3 File Offset: 0x000A3BD3
			public virtual MultiValuedProperty<ServerIdParameter> SourceTransportServers
			{
				set
				{
					base.PowerSharpParameters["SourceTransportServers"] = value;
				}
			}

			// Token: 0x170047F3 RID: 18419
			// (set) Token: 0x06006D7E RID: 28030 RVA: 0x000A59E6 File Offset: 0x000A3BE6
			public virtual bool IsScopedConnector
			{
				set
				{
					base.PowerSharpParameters["IsScopedConnector"] = value;
				}
			}

			// Token: 0x170047F4 RID: 18420
			// (set) Token: 0x06006D7F RID: 28031 RVA: 0x000A59FE File Offset: 0x000A3BFE
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170047F5 RID: 18421
			// (set) Token: 0x06006D80 RID: 28032 RVA: 0x000A5A11 File Offset: 0x000A3C11
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170047F6 RID: 18422
			// (set) Token: 0x06006D81 RID: 28033 RVA: 0x000A5A24 File Offset: 0x000A3C24
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170047F7 RID: 18423
			// (set) Token: 0x06006D82 RID: 28034 RVA: 0x000A5A3C File Offset: 0x000A3C3C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170047F8 RID: 18424
			// (set) Token: 0x06006D83 RID: 28035 RVA: 0x000A5A54 File Offset: 0x000A3C54
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170047F9 RID: 18425
			// (set) Token: 0x06006D84 RID: 28036 RVA: 0x000A5A6C File Offset: 0x000A3C6C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170047FA RID: 18426
			// (set) Token: 0x06006D85 RID: 28037 RVA: 0x000A5A84 File Offset: 0x000A3C84
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020008A2 RID: 2210
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170047FB RID: 18427
			// (set) Token: 0x06006D87 RID: 28039 RVA: 0x000A5AA4 File Offset: 0x000A3CA4
			public virtual MultiValuedProperty<ServerIdParameter> SourceTransportServers
			{
				set
				{
					base.PowerSharpParameters["SourceTransportServers"] = value;
				}
			}

			// Token: 0x170047FC RID: 18428
			// (set) Token: 0x06006D88 RID: 28040 RVA: 0x000A5AB7 File Offset: 0x000A3CB7
			public virtual bool IsScopedConnector
			{
				set
				{
					base.PowerSharpParameters["IsScopedConnector"] = value;
				}
			}

			// Token: 0x170047FD RID: 18429
			// (set) Token: 0x06006D89 RID: 28041 RVA: 0x000A5ACF File Offset: 0x000A3CCF
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170047FE RID: 18430
			// (set) Token: 0x06006D8A RID: 28042 RVA: 0x000A5AE2 File Offset: 0x000A3CE2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170047FF RID: 18431
			// (set) Token: 0x06006D8B RID: 28043 RVA: 0x000A5AF5 File Offset: 0x000A3CF5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004800 RID: 18432
			// (set) Token: 0x06006D8C RID: 28044 RVA: 0x000A5B0D File Offset: 0x000A3D0D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004801 RID: 18433
			// (set) Token: 0x06006D8D RID: 28045 RVA: 0x000A5B25 File Offset: 0x000A3D25
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004802 RID: 18434
			// (set) Token: 0x06006D8E RID: 28046 RVA: 0x000A5B3D File Offset: 0x000A3D3D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004803 RID: 18435
			// (set) Token: 0x06006D8F RID: 28047 RVA: 0x000A5B55 File Offset: 0x000A3D55
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
