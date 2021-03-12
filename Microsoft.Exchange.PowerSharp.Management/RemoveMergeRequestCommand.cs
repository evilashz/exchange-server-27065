using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009FD RID: 2557
	public class RemoveMergeRequestCommand : SyntheticCommandWithPipelineInput<MergeRequestIdParameter, MergeRequestIdParameter>
	{
		// Token: 0x0600803C RID: 32828 RVA: 0x000BE47E File Offset: 0x000BC67E
		private RemoveMergeRequestCommand() : base("Remove-MergeRequest")
		{
		}

		// Token: 0x0600803D RID: 32829 RVA: 0x000BE48B File Offset: 0x000BC68B
		public RemoveMergeRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600803E RID: 32830 RVA: 0x000BE49A File Offset: 0x000BC69A
		public virtual RemoveMergeRequestCommand SetParameters(RemoveMergeRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600803F RID: 32831 RVA: 0x000BE4A4 File Offset: 0x000BC6A4
		public virtual RemoveMergeRequestCommand SetParameters(RemoveMergeRequestCommand.MigrationRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008040 RID: 32832 RVA: 0x000BE4AE File Offset: 0x000BC6AE
		public virtual RemoveMergeRequestCommand SetParameters(RemoveMergeRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009FE RID: 2558
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170057FB RID: 22523
			// (set) Token: 0x06008041 RID: 32833 RVA: 0x000BE4B8 File Offset: 0x000BC6B8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MergeRequestIdParameter(value) : null);
				}
			}

			// Token: 0x170057FC RID: 22524
			// (set) Token: 0x06008042 RID: 32834 RVA: 0x000BE4D6 File Offset: 0x000BC6D6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170057FD RID: 22525
			// (set) Token: 0x06008043 RID: 32835 RVA: 0x000BE4E9 File Offset: 0x000BC6E9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170057FE RID: 22526
			// (set) Token: 0x06008044 RID: 32836 RVA: 0x000BE501 File Offset: 0x000BC701
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170057FF RID: 22527
			// (set) Token: 0x06008045 RID: 32837 RVA: 0x000BE519 File Offset: 0x000BC719
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005800 RID: 22528
			// (set) Token: 0x06008046 RID: 32838 RVA: 0x000BE531 File Offset: 0x000BC731
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005801 RID: 22529
			// (set) Token: 0x06008047 RID: 32839 RVA: 0x000BE549 File Offset: 0x000BC749
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005802 RID: 22530
			// (set) Token: 0x06008048 RID: 32840 RVA: 0x000BE561 File Offset: 0x000BC761
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020009FF RID: 2559
		public class MigrationRequestQueueParameters : ParametersBase
		{
			// Token: 0x17005803 RID: 22531
			// (set) Token: 0x0600804A RID: 32842 RVA: 0x000BE581 File Offset: 0x000BC781
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005804 RID: 22532
			// (set) Token: 0x0600804B RID: 32843 RVA: 0x000BE594 File Offset: 0x000BC794
			public virtual Guid RequestGuid
			{
				set
				{
					base.PowerSharpParameters["RequestGuid"] = value;
				}
			}

			// Token: 0x17005805 RID: 22533
			// (set) Token: 0x0600804C RID: 32844 RVA: 0x000BE5AC File Offset: 0x000BC7AC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005806 RID: 22534
			// (set) Token: 0x0600804D RID: 32845 RVA: 0x000BE5BF File Offset: 0x000BC7BF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005807 RID: 22535
			// (set) Token: 0x0600804E RID: 32846 RVA: 0x000BE5D7 File Offset: 0x000BC7D7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005808 RID: 22536
			// (set) Token: 0x0600804F RID: 32847 RVA: 0x000BE5EF File Offset: 0x000BC7EF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005809 RID: 22537
			// (set) Token: 0x06008050 RID: 32848 RVA: 0x000BE607 File Offset: 0x000BC807
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700580A RID: 22538
			// (set) Token: 0x06008051 RID: 32849 RVA: 0x000BE61F File Offset: 0x000BC81F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700580B RID: 22539
			// (set) Token: 0x06008052 RID: 32850 RVA: 0x000BE637 File Offset: 0x000BC837
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000A00 RID: 2560
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700580C RID: 22540
			// (set) Token: 0x06008054 RID: 32852 RVA: 0x000BE657 File Offset: 0x000BC857
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700580D RID: 22541
			// (set) Token: 0x06008055 RID: 32853 RVA: 0x000BE66A File Offset: 0x000BC86A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700580E RID: 22542
			// (set) Token: 0x06008056 RID: 32854 RVA: 0x000BE682 File Offset: 0x000BC882
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700580F RID: 22543
			// (set) Token: 0x06008057 RID: 32855 RVA: 0x000BE69A File Offset: 0x000BC89A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005810 RID: 22544
			// (set) Token: 0x06008058 RID: 32856 RVA: 0x000BE6B2 File Offset: 0x000BC8B2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005811 RID: 22545
			// (set) Token: 0x06008059 RID: 32857 RVA: 0x000BE6CA File Offset: 0x000BC8CA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005812 RID: 22546
			// (set) Token: 0x0600805A RID: 32858 RVA: 0x000BE6E2 File Offset: 0x000BC8E2
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
