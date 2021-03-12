using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005F9 RID: 1529
	public class RemoveMailboxDatabaseCommand : SyntheticCommandWithPipelineInput<MailboxDatabase, MailboxDatabase>
	{
		// Token: 0x06004E7F RID: 20095 RVA: 0x0007D05F File Offset: 0x0007B25F
		private RemoveMailboxDatabaseCommand() : base("Remove-MailboxDatabase")
		{
		}

		// Token: 0x06004E80 RID: 20096 RVA: 0x0007D06C File Offset: 0x0007B26C
		public RemoveMailboxDatabaseCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004E81 RID: 20097 RVA: 0x0007D07B File Offset: 0x0007B27B
		public virtual RemoveMailboxDatabaseCommand SetParameters(RemoveMailboxDatabaseCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004E82 RID: 20098 RVA: 0x0007D085 File Offset: 0x0007B285
		public virtual RemoveMailboxDatabaseCommand SetParameters(RemoveMailboxDatabaseCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005FA RID: 1530
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002E46 RID: 11846
			// (set) Token: 0x06004E83 RID: 20099 RVA: 0x0007D08F File Offset: 0x0007B28F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002E47 RID: 11847
			// (set) Token: 0x06004E84 RID: 20100 RVA: 0x0007D0A2 File Offset: 0x0007B2A2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002E48 RID: 11848
			// (set) Token: 0x06004E85 RID: 20101 RVA: 0x0007D0BA File Offset: 0x0007B2BA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002E49 RID: 11849
			// (set) Token: 0x06004E86 RID: 20102 RVA: 0x0007D0D2 File Offset: 0x0007B2D2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002E4A RID: 11850
			// (set) Token: 0x06004E87 RID: 20103 RVA: 0x0007D0EA File Offset: 0x0007B2EA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002E4B RID: 11851
			// (set) Token: 0x06004E88 RID: 20104 RVA: 0x0007D102 File Offset: 0x0007B302
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002E4C RID: 11852
			// (set) Token: 0x06004E89 RID: 20105 RVA: 0x0007D11A File Offset: 0x0007B31A
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005FB RID: 1531
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002E4D RID: 11853
			// (set) Token: 0x06004E8B RID: 20107 RVA: 0x0007D13A File Offset: 0x0007B33A
			public virtual DatabaseIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002E4E RID: 11854
			// (set) Token: 0x06004E8C RID: 20108 RVA: 0x0007D14D File Offset: 0x0007B34D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002E4F RID: 11855
			// (set) Token: 0x06004E8D RID: 20109 RVA: 0x0007D160 File Offset: 0x0007B360
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002E50 RID: 11856
			// (set) Token: 0x06004E8E RID: 20110 RVA: 0x0007D178 File Offset: 0x0007B378
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002E51 RID: 11857
			// (set) Token: 0x06004E8F RID: 20111 RVA: 0x0007D190 File Offset: 0x0007B390
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002E52 RID: 11858
			// (set) Token: 0x06004E90 RID: 20112 RVA: 0x0007D1A8 File Offset: 0x0007B3A8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002E53 RID: 11859
			// (set) Token: 0x06004E91 RID: 20113 RVA: 0x0007D1C0 File Offset: 0x0007B3C0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002E54 RID: 11860
			// (set) Token: 0x06004E92 RID: 20114 RVA: 0x0007D1D8 File Offset: 0x0007B3D8
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
