using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000ACA RID: 2762
	public class RemoveSyncRequestCommand : SyntheticCommandWithPipelineInput<SyncRequestIdParameter, SyncRequestIdParameter>
	{
		// Token: 0x060088AE RID: 34990 RVA: 0x000C93A5 File Offset: 0x000C75A5
		private RemoveSyncRequestCommand() : base("Remove-SyncRequest")
		{
		}

		// Token: 0x060088AF RID: 34991 RVA: 0x000C93B2 File Offset: 0x000C75B2
		public RemoveSyncRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060088B0 RID: 34992 RVA: 0x000C93C1 File Offset: 0x000C75C1
		public virtual RemoveSyncRequestCommand SetParameters(RemoveSyncRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060088B1 RID: 34993 RVA: 0x000C93CB File Offset: 0x000C75CB
		public virtual RemoveSyncRequestCommand SetParameters(RemoveSyncRequestCommand.MigrationRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060088B2 RID: 34994 RVA: 0x000C93D5 File Offset: 0x000C75D5
		public virtual RemoveSyncRequestCommand SetParameters(RemoveSyncRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000ACB RID: 2763
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005ED3 RID: 24275
			// (set) Token: 0x060088B3 RID: 34995 RVA: 0x000C93DF File Offset: 0x000C75DF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SyncRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005ED4 RID: 24276
			// (set) Token: 0x060088B4 RID: 34996 RVA: 0x000C93FD File Offset: 0x000C75FD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005ED5 RID: 24277
			// (set) Token: 0x060088B5 RID: 34997 RVA: 0x000C9410 File Offset: 0x000C7610
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005ED6 RID: 24278
			// (set) Token: 0x060088B6 RID: 34998 RVA: 0x000C9428 File Offset: 0x000C7628
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005ED7 RID: 24279
			// (set) Token: 0x060088B7 RID: 34999 RVA: 0x000C9440 File Offset: 0x000C7640
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005ED8 RID: 24280
			// (set) Token: 0x060088B8 RID: 35000 RVA: 0x000C9458 File Offset: 0x000C7658
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005ED9 RID: 24281
			// (set) Token: 0x060088B9 RID: 35001 RVA: 0x000C9470 File Offset: 0x000C7670
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005EDA RID: 24282
			// (set) Token: 0x060088BA RID: 35002 RVA: 0x000C9488 File Offset: 0x000C7688
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000ACC RID: 2764
		public class MigrationRequestQueueParameters : ParametersBase
		{
			// Token: 0x17005EDB RID: 24283
			// (set) Token: 0x060088BC RID: 35004 RVA: 0x000C94A8 File Offset: 0x000C76A8
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005EDC RID: 24284
			// (set) Token: 0x060088BD RID: 35005 RVA: 0x000C94BB File Offset: 0x000C76BB
			public virtual Guid RequestGuid
			{
				set
				{
					base.PowerSharpParameters["RequestGuid"] = value;
				}
			}

			// Token: 0x17005EDD RID: 24285
			// (set) Token: 0x060088BE RID: 35006 RVA: 0x000C94D3 File Offset: 0x000C76D3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005EDE RID: 24286
			// (set) Token: 0x060088BF RID: 35007 RVA: 0x000C94E6 File Offset: 0x000C76E6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005EDF RID: 24287
			// (set) Token: 0x060088C0 RID: 35008 RVA: 0x000C94FE File Offset: 0x000C76FE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005EE0 RID: 24288
			// (set) Token: 0x060088C1 RID: 35009 RVA: 0x000C9516 File Offset: 0x000C7716
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005EE1 RID: 24289
			// (set) Token: 0x060088C2 RID: 35010 RVA: 0x000C952E File Offset: 0x000C772E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005EE2 RID: 24290
			// (set) Token: 0x060088C3 RID: 35011 RVA: 0x000C9546 File Offset: 0x000C7746
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005EE3 RID: 24291
			// (set) Token: 0x060088C4 RID: 35012 RVA: 0x000C955E File Offset: 0x000C775E
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000ACD RID: 2765
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005EE4 RID: 24292
			// (set) Token: 0x060088C6 RID: 35014 RVA: 0x000C957E File Offset: 0x000C777E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005EE5 RID: 24293
			// (set) Token: 0x060088C7 RID: 35015 RVA: 0x000C9591 File Offset: 0x000C7791
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005EE6 RID: 24294
			// (set) Token: 0x060088C8 RID: 35016 RVA: 0x000C95A9 File Offset: 0x000C77A9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005EE7 RID: 24295
			// (set) Token: 0x060088C9 RID: 35017 RVA: 0x000C95C1 File Offset: 0x000C77C1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005EE8 RID: 24296
			// (set) Token: 0x060088CA RID: 35018 RVA: 0x000C95D9 File Offset: 0x000C77D9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005EE9 RID: 24297
			// (set) Token: 0x060088CB RID: 35019 RVA: 0x000C95F1 File Offset: 0x000C77F1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005EEA RID: 24298
			// (set) Token: 0x060088CC RID: 35020 RVA: 0x000C9609 File Offset: 0x000C7809
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
