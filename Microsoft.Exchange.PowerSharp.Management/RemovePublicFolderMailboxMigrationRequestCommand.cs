using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AAD RID: 2733
	public class RemovePublicFolderMailboxMigrationRequestCommand : SyntheticCommandWithPipelineInput<PublicFolderMailboxMigrationRequestIdParameter, PublicFolderMailboxMigrationRequestIdParameter>
	{
		// Token: 0x06008727 RID: 34599 RVA: 0x000C732F File Offset: 0x000C552F
		private RemovePublicFolderMailboxMigrationRequestCommand() : base("Remove-PublicFolderMailboxMigrationRequest")
		{
		}

		// Token: 0x06008728 RID: 34600 RVA: 0x000C733C File Offset: 0x000C553C
		public RemovePublicFolderMailboxMigrationRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008729 RID: 34601 RVA: 0x000C734B File Offset: 0x000C554B
		public virtual RemovePublicFolderMailboxMigrationRequestCommand SetParameters(RemovePublicFolderMailboxMigrationRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600872A RID: 34602 RVA: 0x000C7355 File Offset: 0x000C5555
		public virtual RemovePublicFolderMailboxMigrationRequestCommand SetParameters(RemovePublicFolderMailboxMigrationRequestCommand.MigrationRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600872B RID: 34603 RVA: 0x000C735F File Offset: 0x000C555F
		public virtual RemovePublicFolderMailboxMigrationRequestCommand SetParameters(RemovePublicFolderMailboxMigrationRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AAE RID: 2734
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005D86 RID: 23942
			// (set) Token: 0x0600872C RID: 34604 RVA: 0x000C7369 File Offset: 0x000C5569
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMailboxMigrationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005D87 RID: 23943
			// (set) Token: 0x0600872D RID: 34605 RVA: 0x000C7387 File Offset: 0x000C5587
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005D88 RID: 23944
			// (set) Token: 0x0600872E RID: 34606 RVA: 0x000C739A File Offset: 0x000C559A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005D89 RID: 23945
			// (set) Token: 0x0600872F RID: 34607 RVA: 0x000C73B2 File Offset: 0x000C55B2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005D8A RID: 23946
			// (set) Token: 0x06008730 RID: 34608 RVA: 0x000C73CA File Offset: 0x000C55CA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005D8B RID: 23947
			// (set) Token: 0x06008731 RID: 34609 RVA: 0x000C73E2 File Offset: 0x000C55E2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005D8C RID: 23948
			// (set) Token: 0x06008732 RID: 34610 RVA: 0x000C73FA File Offset: 0x000C55FA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005D8D RID: 23949
			// (set) Token: 0x06008733 RID: 34611 RVA: 0x000C7412 File Offset: 0x000C5612
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000AAF RID: 2735
		public class MigrationRequestQueueParameters : ParametersBase
		{
			// Token: 0x17005D8E RID: 23950
			// (set) Token: 0x06008735 RID: 34613 RVA: 0x000C7432 File Offset: 0x000C5632
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005D8F RID: 23951
			// (set) Token: 0x06008736 RID: 34614 RVA: 0x000C7445 File Offset: 0x000C5645
			public virtual Guid RequestGuid
			{
				set
				{
					base.PowerSharpParameters["RequestGuid"] = value;
				}
			}

			// Token: 0x17005D90 RID: 23952
			// (set) Token: 0x06008737 RID: 34615 RVA: 0x000C745D File Offset: 0x000C565D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005D91 RID: 23953
			// (set) Token: 0x06008738 RID: 34616 RVA: 0x000C7470 File Offset: 0x000C5670
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005D92 RID: 23954
			// (set) Token: 0x06008739 RID: 34617 RVA: 0x000C7488 File Offset: 0x000C5688
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005D93 RID: 23955
			// (set) Token: 0x0600873A RID: 34618 RVA: 0x000C74A0 File Offset: 0x000C56A0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005D94 RID: 23956
			// (set) Token: 0x0600873B RID: 34619 RVA: 0x000C74B8 File Offset: 0x000C56B8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005D95 RID: 23957
			// (set) Token: 0x0600873C RID: 34620 RVA: 0x000C74D0 File Offset: 0x000C56D0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005D96 RID: 23958
			// (set) Token: 0x0600873D RID: 34621 RVA: 0x000C74E8 File Offset: 0x000C56E8
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000AB0 RID: 2736
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005D97 RID: 23959
			// (set) Token: 0x0600873F RID: 34623 RVA: 0x000C7508 File Offset: 0x000C5708
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005D98 RID: 23960
			// (set) Token: 0x06008740 RID: 34624 RVA: 0x000C751B File Offset: 0x000C571B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005D99 RID: 23961
			// (set) Token: 0x06008741 RID: 34625 RVA: 0x000C7533 File Offset: 0x000C5733
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005D9A RID: 23962
			// (set) Token: 0x06008742 RID: 34626 RVA: 0x000C754B File Offset: 0x000C574B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005D9B RID: 23963
			// (set) Token: 0x06008743 RID: 34627 RVA: 0x000C7563 File Offset: 0x000C5763
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005D9C RID: 23964
			// (set) Token: 0x06008744 RID: 34628 RVA: 0x000C757B File Offset: 0x000C577B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005D9D RID: 23965
			// (set) Token: 0x06008745 RID: 34629 RVA: 0x000C7593 File Offset: 0x000C5793
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
