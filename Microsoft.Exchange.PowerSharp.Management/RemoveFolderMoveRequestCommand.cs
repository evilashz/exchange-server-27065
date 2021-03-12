using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009BF RID: 2495
	public class RemoveFolderMoveRequestCommand : SyntheticCommandWithPipelineInput<FolderMoveRequestIdParameter, FolderMoveRequestIdParameter>
	{
		// Token: 0x06007D35 RID: 32053 RVA: 0x000BA471 File Offset: 0x000B8671
		private RemoveFolderMoveRequestCommand() : base("Remove-FolderMoveRequest")
		{
		}

		// Token: 0x06007D36 RID: 32054 RVA: 0x000BA47E File Offset: 0x000B867E
		public RemoveFolderMoveRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007D37 RID: 32055 RVA: 0x000BA48D File Offset: 0x000B868D
		public virtual RemoveFolderMoveRequestCommand SetParameters(RemoveFolderMoveRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007D38 RID: 32056 RVA: 0x000BA497 File Offset: 0x000B8697
		public virtual RemoveFolderMoveRequestCommand SetParameters(RemoveFolderMoveRequestCommand.MigrationRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007D39 RID: 32057 RVA: 0x000BA4A1 File Offset: 0x000B86A1
		public virtual RemoveFolderMoveRequestCommand SetParameters(RemoveFolderMoveRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009C0 RID: 2496
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005570 RID: 21872
			// (set) Token: 0x06007D3A RID: 32058 RVA: 0x000BA4AB File Offset: 0x000B86AB
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new FolderMoveRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005571 RID: 21873
			// (set) Token: 0x06007D3B RID: 32059 RVA: 0x000BA4C9 File Offset: 0x000B86C9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005572 RID: 21874
			// (set) Token: 0x06007D3C RID: 32060 RVA: 0x000BA4DC File Offset: 0x000B86DC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005573 RID: 21875
			// (set) Token: 0x06007D3D RID: 32061 RVA: 0x000BA4F4 File Offset: 0x000B86F4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005574 RID: 21876
			// (set) Token: 0x06007D3E RID: 32062 RVA: 0x000BA50C File Offset: 0x000B870C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005575 RID: 21877
			// (set) Token: 0x06007D3F RID: 32063 RVA: 0x000BA524 File Offset: 0x000B8724
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005576 RID: 21878
			// (set) Token: 0x06007D40 RID: 32064 RVA: 0x000BA53C File Offset: 0x000B873C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005577 RID: 21879
			// (set) Token: 0x06007D41 RID: 32065 RVA: 0x000BA554 File Offset: 0x000B8754
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020009C1 RID: 2497
		public class MigrationRequestQueueParameters : ParametersBase
		{
			// Token: 0x17005578 RID: 21880
			// (set) Token: 0x06007D43 RID: 32067 RVA: 0x000BA574 File Offset: 0x000B8774
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005579 RID: 21881
			// (set) Token: 0x06007D44 RID: 32068 RVA: 0x000BA587 File Offset: 0x000B8787
			public virtual Guid RequestGuid
			{
				set
				{
					base.PowerSharpParameters["RequestGuid"] = value;
				}
			}

			// Token: 0x1700557A RID: 21882
			// (set) Token: 0x06007D45 RID: 32069 RVA: 0x000BA59F File Offset: 0x000B879F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700557B RID: 21883
			// (set) Token: 0x06007D46 RID: 32070 RVA: 0x000BA5B2 File Offset: 0x000B87B2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700557C RID: 21884
			// (set) Token: 0x06007D47 RID: 32071 RVA: 0x000BA5CA File Offset: 0x000B87CA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700557D RID: 21885
			// (set) Token: 0x06007D48 RID: 32072 RVA: 0x000BA5E2 File Offset: 0x000B87E2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700557E RID: 21886
			// (set) Token: 0x06007D49 RID: 32073 RVA: 0x000BA5FA File Offset: 0x000B87FA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700557F RID: 21887
			// (set) Token: 0x06007D4A RID: 32074 RVA: 0x000BA612 File Offset: 0x000B8812
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005580 RID: 21888
			// (set) Token: 0x06007D4B RID: 32075 RVA: 0x000BA62A File Offset: 0x000B882A
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020009C2 RID: 2498
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005581 RID: 21889
			// (set) Token: 0x06007D4D RID: 32077 RVA: 0x000BA64A File Offset: 0x000B884A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005582 RID: 21890
			// (set) Token: 0x06007D4E RID: 32078 RVA: 0x000BA65D File Offset: 0x000B885D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005583 RID: 21891
			// (set) Token: 0x06007D4F RID: 32079 RVA: 0x000BA675 File Offset: 0x000B8875
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005584 RID: 21892
			// (set) Token: 0x06007D50 RID: 32080 RVA: 0x000BA68D File Offset: 0x000B888D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005585 RID: 21893
			// (set) Token: 0x06007D51 RID: 32081 RVA: 0x000BA6A5 File Offset: 0x000B88A5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005586 RID: 21894
			// (set) Token: 0x06007D52 RID: 32082 RVA: 0x000BA6BD File Offset: 0x000B88BD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005587 RID: 21895
			// (set) Token: 0x06007D53 RID: 32083 RVA: 0x000BA6D5 File Offset: 0x000B88D5
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
