using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AE9 RID: 2793
	public class RemoveCmdletExtensionAgentCommand : SyntheticCommandWithPipelineInput<CmdletExtensionAgent, CmdletExtensionAgent>
	{
		// Token: 0x060089B7 RID: 35255 RVA: 0x000CA8D6 File Offset: 0x000C8AD6
		private RemoveCmdletExtensionAgentCommand() : base("Remove-CmdletExtensionAgent")
		{
		}

		// Token: 0x060089B8 RID: 35256 RVA: 0x000CA8E3 File Offset: 0x000C8AE3
		public RemoveCmdletExtensionAgentCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060089B9 RID: 35257 RVA: 0x000CA8F2 File Offset: 0x000C8AF2
		public virtual RemoveCmdletExtensionAgentCommand SetParameters(RemoveCmdletExtensionAgentCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060089BA RID: 35258 RVA: 0x000CA8FC File Offset: 0x000C8AFC
		public virtual RemoveCmdletExtensionAgentCommand SetParameters(RemoveCmdletExtensionAgentCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AEA RID: 2794
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005F9E RID: 24478
			// (set) Token: 0x060089BB RID: 35259 RVA: 0x000CA906 File Offset: 0x000C8B06
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005F9F RID: 24479
			// (set) Token: 0x060089BC RID: 35260 RVA: 0x000CA919 File Offset: 0x000C8B19
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005FA0 RID: 24480
			// (set) Token: 0x060089BD RID: 35261 RVA: 0x000CA931 File Offset: 0x000C8B31
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005FA1 RID: 24481
			// (set) Token: 0x060089BE RID: 35262 RVA: 0x000CA949 File Offset: 0x000C8B49
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005FA2 RID: 24482
			// (set) Token: 0x060089BF RID: 35263 RVA: 0x000CA961 File Offset: 0x000C8B61
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005FA3 RID: 24483
			// (set) Token: 0x060089C0 RID: 35264 RVA: 0x000CA979 File Offset: 0x000C8B79
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005FA4 RID: 24484
			// (set) Token: 0x060089C1 RID: 35265 RVA: 0x000CA991 File Offset: 0x000C8B91
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000AEB RID: 2795
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005FA5 RID: 24485
			// (set) Token: 0x060089C3 RID: 35267 RVA: 0x000CA9B1 File Offset: 0x000C8BB1
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new CmdletExtensionAgentIdParameter(value) : null);
				}
			}

			// Token: 0x17005FA6 RID: 24486
			// (set) Token: 0x060089C4 RID: 35268 RVA: 0x000CA9CF File Offset: 0x000C8BCF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005FA7 RID: 24487
			// (set) Token: 0x060089C5 RID: 35269 RVA: 0x000CA9E2 File Offset: 0x000C8BE2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005FA8 RID: 24488
			// (set) Token: 0x060089C6 RID: 35270 RVA: 0x000CA9FA File Offset: 0x000C8BFA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005FA9 RID: 24489
			// (set) Token: 0x060089C7 RID: 35271 RVA: 0x000CAA12 File Offset: 0x000C8C12
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005FAA RID: 24490
			// (set) Token: 0x060089C8 RID: 35272 RVA: 0x000CAA2A File Offset: 0x000C8C2A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005FAB RID: 24491
			// (set) Token: 0x060089C9 RID: 35273 RVA: 0x000CAA42 File Offset: 0x000C8C42
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005FAC RID: 24492
			// (set) Token: 0x060089CA RID: 35274 RVA: 0x000CAA5A File Offset: 0x000C8C5A
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
