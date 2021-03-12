using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Migration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000299 RID: 665
	public class RemoveMigrationEndpointCommand : SyntheticCommandWithPipelineInput<MigrationEndpoint, MigrationEndpoint>
	{
		// Token: 0x06002FC9 RID: 12233 RVA: 0x00055FEA File Offset: 0x000541EA
		private RemoveMigrationEndpointCommand() : base("Remove-MigrationEndpoint")
		{
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x00055FF7 File Offset: 0x000541F7
		public RemoveMigrationEndpointCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x00056006 File Offset: 0x00054206
		public virtual RemoveMigrationEndpointCommand SetParameters(RemoveMigrationEndpointCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x00056010 File Offset: 0x00054210
		public virtual RemoveMigrationEndpointCommand SetParameters(RemoveMigrationEndpointCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200029A RID: 666
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001650 RID: 5712
			// (set) Token: 0x06002FCD RID: 12237 RVA: 0x0005601A File Offset: 0x0005421A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001651 RID: 5713
			// (set) Token: 0x06002FCE RID: 12238 RVA: 0x00056038 File Offset: 0x00054238
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001652 RID: 5714
			// (set) Token: 0x06002FCF RID: 12239 RVA: 0x00056056 File Offset: 0x00054256
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001653 RID: 5715
			// (set) Token: 0x06002FD0 RID: 12240 RVA: 0x00056069 File Offset: 0x00054269
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001654 RID: 5716
			// (set) Token: 0x06002FD1 RID: 12241 RVA: 0x00056081 File Offset: 0x00054281
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001655 RID: 5717
			// (set) Token: 0x06002FD2 RID: 12242 RVA: 0x00056099 File Offset: 0x00054299
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001656 RID: 5718
			// (set) Token: 0x06002FD3 RID: 12243 RVA: 0x000560B1 File Offset: 0x000542B1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001657 RID: 5719
			// (set) Token: 0x06002FD4 RID: 12244 RVA: 0x000560C9 File Offset: 0x000542C9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001658 RID: 5720
			// (set) Token: 0x06002FD5 RID: 12245 RVA: 0x000560E1 File Offset: 0x000542E1
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200029B RID: 667
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001659 RID: 5721
			// (set) Token: 0x06002FD7 RID: 12247 RVA: 0x00056101 File Offset: 0x00054301
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MigrationEndpointIdParameter(value) : null);
				}
			}

			// Token: 0x1700165A RID: 5722
			// (set) Token: 0x06002FD8 RID: 12248 RVA: 0x0005611F File Offset: 0x0005431F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700165B RID: 5723
			// (set) Token: 0x06002FD9 RID: 12249 RVA: 0x0005613D File Offset: 0x0005433D
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700165C RID: 5724
			// (set) Token: 0x06002FDA RID: 12250 RVA: 0x0005615B File Offset: 0x0005435B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700165D RID: 5725
			// (set) Token: 0x06002FDB RID: 12251 RVA: 0x0005616E File Offset: 0x0005436E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700165E RID: 5726
			// (set) Token: 0x06002FDC RID: 12252 RVA: 0x00056186 File Offset: 0x00054386
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700165F RID: 5727
			// (set) Token: 0x06002FDD RID: 12253 RVA: 0x0005619E File Offset: 0x0005439E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001660 RID: 5728
			// (set) Token: 0x06002FDE RID: 12254 RVA: 0x000561B6 File Offset: 0x000543B6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001661 RID: 5729
			// (set) Token: 0x06002FDF RID: 12255 RVA: 0x000561CE File Offset: 0x000543CE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001662 RID: 5730
			// (set) Token: 0x06002FE0 RID: 12256 RVA: 0x000561E6 File Offset: 0x000543E6
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
