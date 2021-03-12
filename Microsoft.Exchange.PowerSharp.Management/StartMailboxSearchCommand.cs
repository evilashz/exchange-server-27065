using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200040C RID: 1036
	public class StartMailboxSearchCommand : SyntheticCommandWithPipelineInput<MailboxDiscoverySearch, MailboxDiscoverySearch>
	{
		// Token: 0x06003D3B RID: 15675 RVA: 0x0006744D File Offset: 0x0006564D
		private StartMailboxSearchCommand() : base("Start-MailboxSearch")
		{
		}

		// Token: 0x06003D3C RID: 15676 RVA: 0x0006745A File Offset: 0x0006565A
		public StartMailboxSearchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x00067469 File Offset: 0x00065669
		public virtual StartMailboxSearchCommand SetParameters(StartMailboxSearchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003D3E RID: 15678 RVA: 0x00067473 File Offset: 0x00065673
		public virtual StartMailboxSearchCommand SetParameters(StartMailboxSearchCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200040D RID: 1037
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170020DC RID: 8412
			// (set) Token: 0x06003D3F RID: 15679 RVA: 0x0006747D File Offset: 0x0006567D
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170020DD RID: 8413
			// (set) Token: 0x06003D40 RID: 15680 RVA: 0x00067495 File Offset: 0x00065695
			public virtual SwitchParameter Resume
			{
				set
				{
					base.PowerSharpParameters["Resume"] = value;
				}
			}

			// Token: 0x170020DE RID: 8414
			// (set) Token: 0x06003D41 RID: 15681 RVA: 0x000674AD File Offset: 0x000656AD
			public virtual int StatisticsStartIndex
			{
				set
				{
					base.PowerSharpParameters["StatisticsStartIndex"] = value;
				}
			}

			// Token: 0x170020DF RID: 8415
			// (set) Token: 0x06003D42 RID: 15682 RVA: 0x000674C5 File Offset: 0x000656C5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170020E0 RID: 8416
			// (set) Token: 0x06003D43 RID: 15683 RVA: 0x000674D8 File Offset: 0x000656D8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170020E1 RID: 8417
			// (set) Token: 0x06003D44 RID: 15684 RVA: 0x000674F0 File Offset: 0x000656F0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170020E2 RID: 8418
			// (set) Token: 0x06003D45 RID: 15685 RVA: 0x00067508 File Offset: 0x00065708
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170020E3 RID: 8419
			// (set) Token: 0x06003D46 RID: 15686 RVA: 0x00067520 File Offset: 0x00065720
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170020E4 RID: 8420
			// (set) Token: 0x06003D47 RID: 15687 RVA: 0x00067538 File Offset: 0x00065738
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200040E RID: 1038
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170020E5 RID: 8421
			// (set) Token: 0x06003D49 RID: 15689 RVA: 0x00067558 File Offset: 0x00065758
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new EwsStoreObjectIdParameter(value) : null);
				}
			}

			// Token: 0x170020E6 RID: 8422
			// (set) Token: 0x06003D4A RID: 15690 RVA: 0x00067576 File Offset: 0x00065776
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170020E7 RID: 8423
			// (set) Token: 0x06003D4B RID: 15691 RVA: 0x0006758E File Offset: 0x0006578E
			public virtual SwitchParameter Resume
			{
				set
				{
					base.PowerSharpParameters["Resume"] = value;
				}
			}

			// Token: 0x170020E8 RID: 8424
			// (set) Token: 0x06003D4C RID: 15692 RVA: 0x000675A6 File Offset: 0x000657A6
			public virtual int StatisticsStartIndex
			{
				set
				{
					base.PowerSharpParameters["StatisticsStartIndex"] = value;
				}
			}

			// Token: 0x170020E9 RID: 8425
			// (set) Token: 0x06003D4D RID: 15693 RVA: 0x000675BE File Offset: 0x000657BE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170020EA RID: 8426
			// (set) Token: 0x06003D4E RID: 15694 RVA: 0x000675D1 File Offset: 0x000657D1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170020EB RID: 8427
			// (set) Token: 0x06003D4F RID: 15695 RVA: 0x000675E9 File Offset: 0x000657E9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170020EC RID: 8428
			// (set) Token: 0x06003D50 RID: 15696 RVA: 0x00067601 File Offset: 0x00065801
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170020ED RID: 8429
			// (set) Token: 0x06003D51 RID: 15697 RVA: 0x00067619 File Offset: 0x00065819
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170020EE RID: 8430
			// (set) Token: 0x06003D52 RID: 15698 RVA: 0x00067631 File Offset: 0x00065831
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
