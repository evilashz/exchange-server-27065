using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000ADD RID: 2781
	public class DisableCmdletExtensionAgentCommand : SyntheticCommandWithPipelineInputNoOutput<CmdletExtensionAgentIdParameter>
	{
		// Token: 0x06008965 RID: 35173 RVA: 0x000CA290 File Offset: 0x000C8490
		private DisableCmdletExtensionAgentCommand() : base("Disable-CmdletExtensionAgent")
		{
		}

		// Token: 0x06008966 RID: 35174 RVA: 0x000CA29D File Offset: 0x000C849D
		public DisableCmdletExtensionAgentCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008967 RID: 35175 RVA: 0x000CA2AC File Offset: 0x000C84AC
		public virtual DisableCmdletExtensionAgentCommand SetParameters(DisableCmdletExtensionAgentCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008968 RID: 35176 RVA: 0x000CA2B6 File Offset: 0x000C84B6
		public virtual DisableCmdletExtensionAgentCommand SetParameters(DisableCmdletExtensionAgentCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000ADE RID: 2782
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005F64 RID: 24420
			// (set) Token: 0x06008969 RID: 35177 RVA: 0x000CA2C0 File Offset: 0x000C84C0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005F65 RID: 24421
			// (set) Token: 0x0600896A RID: 35178 RVA: 0x000CA2D3 File Offset: 0x000C84D3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005F66 RID: 24422
			// (set) Token: 0x0600896B RID: 35179 RVA: 0x000CA2EB File Offset: 0x000C84EB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005F67 RID: 24423
			// (set) Token: 0x0600896C RID: 35180 RVA: 0x000CA303 File Offset: 0x000C8503
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005F68 RID: 24424
			// (set) Token: 0x0600896D RID: 35181 RVA: 0x000CA31B File Offset: 0x000C851B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005F69 RID: 24425
			// (set) Token: 0x0600896E RID: 35182 RVA: 0x000CA333 File Offset: 0x000C8533
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005F6A RID: 24426
			// (set) Token: 0x0600896F RID: 35183 RVA: 0x000CA34B File Offset: 0x000C854B
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000ADF RID: 2783
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005F6B RID: 24427
			// (set) Token: 0x06008971 RID: 35185 RVA: 0x000CA36B File Offset: 0x000C856B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new CmdletExtensionAgentIdParameter(value) : null);
				}
			}

			// Token: 0x17005F6C RID: 24428
			// (set) Token: 0x06008972 RID: 35186 RVA: 0x000CA389 File Offset: 0x000C8589
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005F6D RID: 24429
			// (set) Token: 0x06008973 RID: 35187 RVA: 0x000CA39C File Offset: 0x000C859C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005F6E RID: 24430
			// (set) Token: 0x06008974 RID: 35188 RVA: 0x000CA3B4 File Offset: 0x000C85B4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005F6F RID: 24431
			// (set) Token: 0x06008975 RID: 35189 RVA: 0x000CA3CC File Offset: 0x000C85CC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005F70 RID: 24432
			// (set) Token: 0x06008976 RID: 35190 RVA: 0x000CA3E4 File Offset: 0x000C85E4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005F71 RID: 24433
			// (set) Token: 0x06008977 RID: 35191 RVA: 0x000CA3FC File Offset: 0x000C85FC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005F72 RID: 24434
			// (set) Token: 0x06008978 RID: 35192 RVA: 0x000CA414 File Offset: 0x000C8614
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
