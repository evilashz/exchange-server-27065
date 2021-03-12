using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008C0 RID: 2240
	public class RemoveOutboundConnectorCommand : SyntheticCommandWithPipelineInput<TenantOutboundConnector, TenantOutboundConnector>
	{
		// Token: 0x06007026 RID: 28710 RVA: 0x000A93CD File Offset: 0x000A75CD
		private RemoveOutboundConnectorCommand() : base("Remove-OutboundConnector")
		{
		}

		// Token: 0x06007027 RID: 28711 RVA: 0x000A93DA File Offset: 0x000A75DA
		public RemoveOutboundConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007028 RID: 28712 RVA: 0x000A93E9 File Offset: 0x000A75E9
		public virtual RemoveOutboundConnectorCommand SetParameters(RemoveOutboundConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007029 RID: 28713 RVA: 0x000A93F3 File Offset: 0x000A75F3
		public virtual RemoveOutboundConnectorCommand SetParameters(RemoveOutboundConnectorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008C1 RID: 2241
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004A5F RID: 19039
			// (set) Token: 0x0600702A RID: 28714 RVA: 0x000A93FD File Offset: 0x000A75FD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004A60 RID: 19040
			// (set) Token: 0x0600702B RID: 28715 RVA: 0x000A9410 File Offset: 0x000A7610
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004A61 RID: 19041
			// (set) Token: 0x0600702C RID: 28716 RVA: 0x000A9428 File Offset: 0x000A7628
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004A62 RID: 19042
			// (set) Token: 0x0600702D RID: 28717 RVA: 0x000A9440 File Offset: 0x000A7640
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004A63 RID: 19043
			// (set) Token: 0x0600702E RID: 28718 RVA: 0x000A9458 File Offset: 0x000A7658
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004A64 RID: 19044
			// (set) Token: 0x0600702F RID: 28719 RVA: 0x000A9470 File Offset: 0x000A7670
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004A65 RID: 19045
			// (set) Token: 0x06007030 RID: 28720 RVA: 0x000A9488 File Offset: 0x000A7688
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020008C2 RID: 2242
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004A66 RID: 19046
			// (set) Token: 0x06007032 RID: 28722 RVA: 0x000A94A8 File Offset: 0x000A76A8
			public virtual OutboundConnectorIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004A67 RID: 19047
			// (set) Token: 0x06007033 RID: 28723 RVA: 0x000A94BB File Offset: 0x000A76BB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004A68 RID: 19048
			// (set) Token: 0x06007034 RID: 28724 RVA: 0x000A94CE File Offset: 0x000A76CE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004A69 RID: 19049
			// (set) Token: 0x06007035 RID: 28725 RVA: 0x000A94E6 File Offset: 0x000A76E6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004A6A RID: 19050
			// (set) Token: 0x06007036 RID: 28726 RVA: 0x000A94FE File Offset: 0x000A76FE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004A6B RID: 19051
			// (set) Token: 0x06007037 RID: 28727 RVA: 0x000A9516 File Offset: 0x000A7716
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004A6C RID: 19052
			// (set) Token: 0x06007038 RID: 28728 RVA: 0x000A952E File Offset: 0x000A772E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004A6D RID: 19053
			// (set) Token: 0x06007039 RID: 28729 RVA: 0x000A9546 File Offset: 0x000A7746
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
