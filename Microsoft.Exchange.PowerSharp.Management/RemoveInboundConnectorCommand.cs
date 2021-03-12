using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008BA RID: 2234
	public class RemoveInboundConnectorCommand : SyntheticCommandWithPipelineInput<TenantInboundConnector, TenantInboundConnector>
	{
		// Token: 0x06006FFC RID: 28668 RVA: 0x000A9090 File Offset: 0x000A7290
		private RemoveInboundConnectorCommand() : base("Remove-InboundConnector")
		{
		}

		// Token: 0x06006FFD RID: 28669 RVA: 0x000A909D File Offset: 0x000A729D
		public RemoveInboundConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006FFE RID: 28670 RVA: 0x000A90AC File Offset: 0x000A72AC
		public virtual RemoveInboundConnectorCommand SetParameters(RemoveInboundConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006FFF RID: 28671 RVA: 0x000A90B6 File Offset: 0x000A72B6
		public virtual RemoveInboundConnectorCommand SetParameters(RemoveInboundConnectorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008BB RID: 2235
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004A41 RID: 19009
			// (set) Token: 0x06007000 RID: 28672 RVA: 0x000A90C0 File Offset: 0x000A72C0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004A42 RID: 19010
			// (set) Token: 0x06007001 RID: 28673 RVA: 0x000A90D3 File Offset: 0x000A72D3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004A43 RID: 19011
			// (set) Token: 0x06007002 RID: 28674 RVA: 0x000A90EB File Offset: 0x000A72EB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004A44 RID: 19012
			// (set) Token: 0x06007003 RID: 28675 RVA: 0x000A9103 File Offset: 0x000A7303
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004A45 RID: 19013
			// (set) Token: 0x06007004 RID: 28676 RVA: 0x000A911B File Offset: 0x000A731B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004A46 RID: 19014
			// (set) Token: 0x06007005 RID: 28677 RVA: 0x000A9133 File Offset: 0x000A7333
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004A47 RID: 19015
			// (set) Token: 0x06007006 RID: 28678 RVA: 0x000A914B File Offset: 0x000A734B
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020008BC RID: 2236
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004A48 RID: 19016
			// (set) Token: 0x06007008 RID: 28680 RVA: 0x000A916B File Offset: 0x000A736B
			public virtual InboundConnectorIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004A49 RID: 19017
			// (set) Token: 0x06007009 RID: 28681 RVA: 0x000A917E File Offset: 0x000A737E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004A4A RID: 19018
			// (set) Token: 0x0600700A RID: 28682 RVA: 0x000A9191 File Offset: 0x000A7391
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004A4B RID: 19019
			// (set) Token: 0x0600700B RID: 28683 RVA: 0x000A91A9 File Offset: 0x000A73A9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004A4C RID: 19020
			// (set) Token: 0x0600700C RID: 28684 RVA: 0x000A91C1 File Offset: 0x000A73C1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004A4D RID: 19021
			// (set) Token: 0x0600700D RID: 28685 RVA: 0x000A91D9 File Offset: 0x000A73D9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004A4E RID: 19022
			// (set) Token: 0x0600700E RID: 28686 RVA: 0x000A91F1 File Offset: 0x000A73F1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004A4F RID: 19023
			// (set) Token: 0x0600700F RID: 28687 RVA: 0x000A9209 File Offset: 0x000A7409
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
