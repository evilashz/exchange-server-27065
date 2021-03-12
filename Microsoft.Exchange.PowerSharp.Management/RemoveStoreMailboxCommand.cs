using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.StoreTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000450 RID: 1104
	public class RemoveStoreMailboxCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06003FCC RID: 16332 RVA: 0x0006A93B File Offset: 0x00068B3B
		private RemoveStoreMailboxCommand() : base("Remove-StoreMailbox")
		{
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x0006A948 File Offset: 0x00068B48
		public RemoveStoreMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x0006A957 File Offset: 0x00068B57
		public virtual RemoveStoreMailboxCommand SetParameters(RemoveStoreMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000451 RID: 1105
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170022E5 RID: 8933
			// (set) Token: 0x06003FCF RID: 16335 RVA: 0x0006A961 File Offset: 0x00068B61
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170022E6 RID: 8934
			// (set) Token: 0x06003FD0 RID: 16336 RVA: 0x0006A974 File Offset: 0x00068B74
			public virtual StoreMailboxIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170022E7 RID: 8935
			// (set) Token: 0x06003FD1 RID: 16337 RVA: 0x0006A987 File Offset: 0x00068B87
			public virtual MailboxStateParameter MailboxState
			{
				set
				{
					base.PowerSharpParameters["MailboxState"] = value;
				}
			}

			// Token: 0x170022E8 RID: 8936
			// (set) Token: 0x06003FD2 RID: 16338 RVA: 0x0006A99F File Offset: 0x00068B9F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170022E9 RID: 8937
			// (set) Token: 0x06003FD3 RID: 16339 RVA: 0x0006A9B7 File Offset: 0x00068BB7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170022EA RID: 8938
			// (set) Token: 0x06003FD4 RID: 16340 RVA: 0x0006A9CF File Offset: 0x00068BCF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170022EB RID: 8939
			// (set) Token: 0x06003FD5 RID: 16341 RVA: 0x0006A9E7 File Offset: 0x00068BE7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170022EC RID: 8940
			// (set) Token: 0x06003FD6 RID: 16342 RVA: 0x0006A9FF File Offset: 0x00068BFF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170022ED RID: 8941
			// (set) Token: 0x06003FD7 RID: 16343 RVA: 0x0006AA17 File Offset: 0x00068C17
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
