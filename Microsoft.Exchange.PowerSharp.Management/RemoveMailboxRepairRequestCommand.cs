using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BC4 RID: 3012
	public class RemoveMailboxRepairRequestCommand : SyntheticCommandWithPipelineInput<StoreIntegrityCheckJob, StoreIntegrityCheckJob>
	{
		// Token: 0x06009177 RID: 37239 RVA: 0x000D4834 File Offset: 0x000D2A34
		private RemoveMailboxRepairRequestCommand() : base("Remove-MailboxRepairRequest")
		{
		}

		// Token: 0x06009178 RID: 37240 RVA: 0x000D4841 File Offset: 0x000D2A41
		public RemoveMailboxRepairRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009179 RID: 37241 RVA: 0x000D4850 File Offset: 0x000D2A50
		public virtual RemoveMailboxRepairRequestCommand SetParameters(RemoveMailboxRepairRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600917A RID: 37242 RVA: 0x000D485A File Offset: 0x000D2A5A
		public virtual RemoveMailboxRepairRequestCommand SetParameters(RemoveMailboxRepairRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BC5 RID: 3013
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170065A8 RID: 26024
			// (set) Token: 0x0600917B RID: 37243 RVA: 0x000D4864 File Offset: 0x000D2A64
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170065A9 RID: 26025
			// (set) Token: 0x0600917C RID: 37244 RVA: 0x000D4877 File Offset: 0x000D2A77
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170065AA RID: 26026
			// (set) Token: 0x0600917D RID: 37245 RVA: 0x000D488F File Offset: 0x000D2A8F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170065AB RID: 26027
			// (set) Token: 0x0600917E RID: 37246 RVA: 0x000D48A7 File Offset: 0x000D2AA7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170065AC RID: 26028
			// (set) Token: 0x0600917F RID: 37247 RVA: 0x000D48BF File Offset: 0x000D2ABF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170065AD RID: 26029
			// (set) Token: 0x06009180 RID: 37248 RVA: 0x000D48D7 File Offset: 0x000D2AD7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170065AE RID: 26030
			// (set) Token: 0x06009181 RID: 37249 RVA: 0x000D48EF File Offset: 0x000D2AEF
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000BC6 RID: 3014
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170065AF RID: 26031
			// (set) Token: 0x06009183 RID: 37251 RVA: 0x000D490F File Offset: 0x000D2B0F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new StoreIntegrityCheckJobIdParameter(value) : null);
				}
			}

			// Token: 0x170065B0 RID: 26032
			// (set) Token: 0x06009184 RID: 37252 RVA: 0x000D492D File Offset: 0x000D2B2D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170065B1 RID: 26033
			// (set) Token: 0x06009185 RID: 37253 RVA: 0x000D4940 File Offset: 0x000D2B40
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170065B2 RID: 26034
			// (set) Token: 0x06009186 RID: 37254 RVA: 0x000D4958 File Offset: 0x000D2B58
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170065B3 RID: 26035
			// (set) Token: 0x06009187 RID: 37255 RVA: 0x000D4970 File Offset: 0x000D2B70
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170065B4 RID: 26036
			// (set) Token: 0x06009188 RID: 37256 RVA: 0x000D4988 File Offset: 0x000D2B88
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170065B5 RID: 26037
			// (set) Token: 0x06009189 RID: 37257 RVA: 0x000D49A0 File Offset: 0x000D2BA0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170065B6 RID: 26038
			// (set) Token: 0x0600918A RID: 37258 RVA: 0x000D49B8 File Offset: 0x000D2BB8
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
