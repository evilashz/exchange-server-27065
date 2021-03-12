using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AE0 RID: 2784
	public class EnableCmdletExtensionAgentCommand : SyntheticCommandWithPipelineInputNoOutput<CmdletExtensionAgentIdParameter>
	{
		// Token: 0x0600897A RID: 35194 RVA: 0x000CA434 File Offset: 0x000C8634
		private EnableCmdletExtensionAgentCommand() : base("Enable-CmdletExtensionAgent")
		{
		}

		// Token: 0x0600897B RID: 35195 RVA: 0x000CA441 File Offset: 0x000C8641
		public EnableCmdletExtensionAgentCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600897C RID: 35196 RVA: 0x000CA450 File Offset: 0x000C8650
		public virtual EnableCmdletExtensionAgentCommand SetParameters(EnableCmdletExtensionAgentCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600897D RID: 35197 RVA: 0x000CA45A File Offset: 0x000C865A
		public virtual EnableCmdletExtensionAgentCommand SetParameters(EnableCmdletExtensionAgentCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AE1 RID: 2785
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005F73 RID: 24435
			// (set) Token: 0x0600897E RID: 35198 RVA: 0x000CA464 File Offset: 0x000C8664
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005F74 RID: 24436
			// (set) Token: 0x0600897F RID: 35199 RVA: 0x000CA477 File Offset: 0x000C8677
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005F75 RID: 24437
			// (set) Token: 0x06008980 RID: 35200 RVA: 0x000CA48F File Offset: 0x000C868F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005F76 RID: 24438
			// (set) Token: 0x06008981 RID: 35201 RVA: 0x000CA4A7 File Offset: 0x000C86A7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005F77 RID: 24439
			// (set) Token: 0x06008982 RID: 35202 RVA: 0x000CA4BF File Offset: 0x000C86BF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005F78 RID: 24440
			// (set) Token: 0x06008983 RID: 35203 RVA: 0x000CA4D7 File Offset: 0x000C86D7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000AE2 RID: 2786
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005F79 RID: 24441
			// (set) Token: 0x06008985 RID: 35205 RVA: 0x000CA4F7 File Offset: 0x000C86F7
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new CmdletExtensionAgentIdParameter(value) : null);
				}
			}

			// Token: 0x17005F7A RID: 24442
			// (set) Token: 0x06008986 RID: 35206 RVA: 0x000CA515 File Offset: 0x000C8715
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005F7B RID: 24443
			// (set) Token: 0x06008987 RID: 35207 RVA: 0x000CA528 File Offset: 0x000C8728
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005F7C RID: 24444
			// (set) Token: 0x06008988 RID: 35208 RVA: 0x000CA540 File Offset: 0x000C8740
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005F7D RID: 24445
			// (set) Token: 0x06008989 RID: 35209 RVA: 0x000CA558 File Offset: 0x000C8758
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005F7E RID: 24446
			// (set) Token: 0x0600898A RID: 35210 RVA: 0x000CA570 File Offset: 0x000C8770
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005F7F RID: 24447
			// (set) Token: 0x0600898B RID: 35211 RVA: 0x000CA588 File Offset: 0x000C8788
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
