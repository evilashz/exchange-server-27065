using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000457 RID: 1111
	public class UpdateStoreMailboxStateCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06003FFB RID: 16379 RVA: 0x0006ACF1 File Offset: 0x00068EF1
		private UpdateStoreMailboxStateCommand() : base("Update-StoreMailboxState")
		{
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x0006ACFE File Offset: 0x00068EFE
		public UpdateStoreMailboxStateCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003FFD RID: 16381 RVA: 0x0006AD0D File Offset: 0x00068F0D
		public virtual UpdateStoreMailboxStateCommand SetParameters(UpdateStoreMailboxStateCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000458 RID: 1112
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002306 RID: 8966
			// (set) Token: 0x06003FFE RID: 16382 RVA: 0x0006AD17 File Offset: 0x00068F17
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17002307 RID: 8967
			// (set) Token: 0x06003FFF RID: 16383 RVA: 0x0006AD2A File Offset: 0x00068F2A
			public virtual StoreMailboxIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002308 RID: 8968
			// (set) Token: 0x06004000 RID: 16384 RVA: 0x0006AD3D File Offset: 0x00068F3D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002309 RID: 8969
			// (set) Token: 0x06004001 RID: 16385 RVA: 0x0006AD55 File Offset: 0x00068F55
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700230A RID: 8970
			// (set) Token: 0x06004002 RID: 16386 RVA: 0x0006AD6D File Offset: 0x00068F6D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700230B RID: 8971
			// (set) Token: 0x06004003 RID: 16387 RVA: 0x0006AD85 File Offset: 0x00068F85
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700230C RID: 8972
			// (set) Token: 0x06004004 RID: 16388 RVA: 0x0006AD9D File Offset: 0x00068F9D
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
