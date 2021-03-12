using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200061B RID: 1563
	public class RemoveClientAccessArrayCommand : SyntheticCommandWithPipelineInput<ClientAccessArray, ClientAccessArray>
	{
		// Token: 0x06005004 RID: 20484 RVA: 0x0007EFE4 File Offset: 0x0007D1E4
		private RemoveClientAccessArrayCommand() : base("Remove-ClientAccessArray")
		{
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x0007EFF1 File Offset: 0x0007D1F1
		public RemoveClientAccessArrayCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x0007F000 File Offset: 0x0007D200
		public virtual RemoveClientAccessArrayCommand SetParameters(RemoveClientAccessArrayCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x0007F00A File Offset: 0x0007D20A
		public virtual RemoveClientAccessArrayCommand SetParameters(RemoveClientAccessArrayCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200061C RID: 1564
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002F87 RID: 12167
			// (set) Token: 0x06005008 RID: 20488 RVA: 0x0007F014 File Offset: 0x0007D214
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002F88 RID: 12168
			// (set) Token: 0x06005009 RID: 20489 RVA: 0x0007F027 File Offset: 0x0007D227
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002F89 RID: 12169
			// (set) Token: 0x0600500A RID: 20490 RVA: 0x0007F03F File Offset: 0x0007D23F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002F8A RID: 12170
			// (set) Token: 0x0600500B RID: 20491 RVA: 0x0007F057 File Offset: 0x0007D257
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002F8B RID: 12171
			// (set) Token: 0x0600500C RID: 20492 RVA: 0x0007F06F File Offset: 0x0007D26F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002F8C RID: 12172
			// (set) Token: 0x0600500D RID: 20493 RVA: 0x0007F087 File Offset: 0x0007D287
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002F8D RID: 12173
			// (set) Token: 0x0600500E RID: 20494 RVA: 0x0007F09F File Offset: 0x0007D29F
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200061D RID: 1565
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002F8E RID: 12174
			// (set) Token: 0x06005010 RID: 20496 RVA: 0x0007F0BF File Offset: 0x0007D2BF
			public virtual ClientAccessArrayIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002F8F RID: 12175
			// (set) Token: 0x06005011 RID: 20497 RVA: 0x0007F0D2 File Offset: 0x0007D2D2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002F90 RID: 12176
			// (set) Token: 0x06005012 RID: 20498 RVA: 0x0007F0E5 File Offset: 0x0007D2E5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002F91 RID: 12177
			// (set) Token: 0x06005013 RID: 20499 RVA: 0x0007F0FD File Offset: 0x0007D2FD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002F92 RID: 12178
			// (set) Token: 0x06005014 RID: 20500 RVA: 0x0007F115 File Offset: 0x0007D315
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002F93 RID: 12179
			// (set) Token: 0x06005015 RID: 20501 RVA: 0x0007F12D File Offset: 0x0007D32D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002F94 RID: 12180
			// (set) Token: 0x06005016 RID: 20502 RVA: 0x0007F145 File Offset: 0x0007D345
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002F95 RID: 12181
			// (set) Token: 0x06005017 RID: 20503 RVA: 0x0007F15D File Offset: 0x0007D35D
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
