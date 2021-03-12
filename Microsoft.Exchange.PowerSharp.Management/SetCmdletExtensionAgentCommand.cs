using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AEC RID: 2796
	public class SetCmdletExtensionAgentCommand : SyntheticCommandWithPipelineInputNoOutput<CmdletExtensionAgent>
	{
		// Token: 0x060089CC RID: 35276 RVA: 0x000CAA7A File Offset: 0x000C8C7A
		private SetCmdletExtensionAgentCommand() : base("Set-CmdletExtensionAgent")
		{
		}

		// Token: 0x060089CD RID: 35277 RVA: 0x000CAA87 File Offset: 0x000C8C87
		public SetCmdletExtensionAgentCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060089CE RID: 35278 RVA: 0x000CAA96 File Offset: 0x000C8C96
		public virtual SetCmdletExtensionAgentCommand SetParameters(SetCmdletExtensionAgentCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060089CF RID: 35279 RVA: 0x000CAAA0 File Offset: 0x000C8CA0
		public virtual SetCmdletExtensionAgentCommand SetParameters(SetCmdletExtensionAgentCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AED RID: 2797
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005FAD RID: 24493
			// (set) Token: 0x060089D0 RID: 35280 RVA: 0x000CAAAA File Offset: 0x000C8CAA
			public virtual byte Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005FAE RID: 24494
			// (set) Token: 0x060089D1 RID: 35281 RVA: 0x000CAAC2 File Offset: 0x000C8CC2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005FAF RID: 24495
			// (set) Token: 0x060089D2 RID: 35282 RVA: 0x000CAAD5 File Offset: 0x000C8CD5
			public virtual bool IsSystem
			{
				set
				{
					base.PowerSharpParameters["IsSystem"] = value;
				}
			}

			// Token: 0x17005FB0 RID: 24496
			// (set) Token: 0x060089D3 RID: 35283 RVA: 0x000CAAED File Offset: 0x000C8CED
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005FB1 RID: 24497
			// (set) Token: 0x060089D4 RID: 35284 RVA: 0x000CAB00 File Offset: 0x000C8D00
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005FB2 RID: 24498
			// (set) Token: 0x060089D5 RID: 35285 RVA: 0x000CAB18 File Offset: 0x000C8D18
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005FB3 RID: 24499
			// (set) Token: 0x060089D6 RID: 35286 RVA: 0x000CAB30 File Offset: 0x000C8D30
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005FB4 RID: 24500
			// (set) Token: 0x060089D7 RID: 35287 RVA: 0x000CAB48 File Offset: 0x000C8D48
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005FB5 RID: 24501
			// (set) Token: 0x060089D8 RID: 35288 RVA: 0x000CAB60 File Offset: 0x000C8D60
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000AEE RID: 2798
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005FB6 RID: 24502
			// (set) Token: 0x060089DA RID: 35290 RVA: 0x000CAB80 File Offset: 0x000C8D80
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new CmdletExtensionAgentIdParameter(value) : null);
				}
			}

			// Token: 0x17005FB7 RID: 24503
			// (set) Token: 0x060089DB RID: 35291 RVA: 0x000CAB9E File Offset: 0x000C8D9E
			public virtual byte Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005FB8 RID: 24504
			// (set) Token: 0x060089DC RID: 35292 RVA: 0x000CABB6 File Offset: 0x000C8DB6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005FB9 RID: 24505
			// (set) Token: 0x060089DD RID: 35293 RVA: 0x000CABC9 File Offset: 0x000C8DC9
			public virtual bool IsSystem
			{
				set
				{
					base.PowerSharpParameters["IsSystem"] = value;
				}
			}

			// Token: 0x17005FBA RID: 24506
			// (set) Token: 0x060089DE RID: 35294 RVA: 0x000CABE1 File Offset: 0x000C8DE1
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005FBB RID: 24507
			// (set) Token: 0x060089DF RID: 35295 RVA: 0x000CABF4 File Offset: 0x000C8DF4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005FBC RID: 24508
			// (set) Token: 0x060089E0 RID: 35296 RVA: 0x000CAC0C File Offset: 0x000C8E0C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005FBD RID: 24509
			// (set) Token: 0x060089E1 RID: 35297 RVA: 0x000CAC24 File Offset: 0x000C8E24
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005FBE RID: 24510
			// (set) Token: 0x060089E2 RID: 35298 RVA: 0x000CAC3C File Offset: 0x000C8E3C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005FBF RID: 24511
			// (set) Token: 0x060089E3 RID: 35299 RVA: 0x000CAC54 File Offset: 0x000C8E54
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
