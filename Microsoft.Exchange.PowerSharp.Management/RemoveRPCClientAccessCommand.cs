using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200061E RID: 1566
	public class RemoveRPCClientAccessCommand : SyntheticCommandWithPipelineInput<ExchangeRpcClientAccess, ExchangeRpcClientAccess>
	{
		// Token: 0x06005019 RID: 20505 RVA: 0x0007F17D File Offset: 0x0007D37D
		private RemoveRPCClientAccessCommand() : base("Remove-RPCClientAccess")
		{
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x0007F18A File Offset: 0x0007D38A
		public RemoveRPCClientAccessCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x0007F199 File Offset: 0x0007D399
		public virtual RemoveRPCClientAccessCommand SetParameters(RemoveRPCClientAccessCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200061F RID: 1567
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002F96 RID: 12182
			// (set) Token: 0x0600501C RID: 20508 RVA: 0x0007F1A3 File Offset: 0x0007D3A3
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17002F97 RID: 12183
			// (set) Token: 0x0600501D RID: 20509 RVA: 0x0007F1B6 File Offset: 0x0007D3B6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002F98 RID: 12184
			// (set) Token: 0x0600501E RID: 20510 RVA: 0x0007F1C9 File Offset: 0x0007D3C9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002F99 RID: 12185
			// (set) Token: 0x0600501F RID: 20511 RVA: 0x0007F1E1 File Offset: 0x0007D3E1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002F9A RID: 12186
			// (set) Token: 0x06005020 RID: 20512 RVA: 0x0007F1F9 File Offset: 0x0007D3F9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002F9B RID: 12187
			// (set) Token: 0x06005021 RID: 20513 RVA: 0x0007F211 File Offset: 0x0007D411
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002F9C RID: 12188
			// (set) Token: 0x06005022 RID: 20514 RVA: 0x0007F229 File Offset: 0x0007D429
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002F9D RID: 12189
			// (set) Token: 0x06005023 RID: 20515 RVA: 0x0007F241 File Offset: 0x0007D441
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
