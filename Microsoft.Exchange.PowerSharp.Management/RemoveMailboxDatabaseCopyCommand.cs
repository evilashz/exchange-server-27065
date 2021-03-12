using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005FC RID: 1532
	public class RemoveMailboxDatabaseCopyCommand : SyntheticCommandWithPipelineInput<DatabaseCopy, DatabaseCopy>
	{
		// Token: 0x06004E94 RID: 20116 RVA: 0x0007D1F8 File Offset: 0x0007B3F8
		private RemoveMailboxDatabaseCopyCommand() : base("Remove-MailboxDatabaseCopy")
		{
		}

		// Token: 0x06004E95 RID: 20117 RVA: 0x0007D205 File Offset: 0x0007B405
		public RemoveMailboxDatabaseCopyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004E96 RID: 20118 RVA: 0x0007D214 File Offset: 0x0007B414
		public virtual RemoveMailboxDatabaseCopyCommand SetParameters(RemoveMailboxDatabaseCopyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004E97 RID: 20119 RVA: 0x0007D21E File Offset: 0x0007B41E
		public virtual RemoveMailboxDatabaseCopyCommand SetParameters(RemoveMailboxDatabaseCopyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005FD RID: 1533
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002E55 RID: 11861
			// (set) Token: 0x06004E98 RID: 20120 RVA: 0x0007D228 File Offset: 0x0007B428
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002E56 RID: 11862
			// (set) Token: 0x06004E99 RID: 20121 RVA: 0x0007D23B File Offset: 0x0007B43B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002E57 RID: 11863
			// (set) Token: 0x06004E9A RID: 20122 RVA: 0x0007D253 File Offset: 0x0007B453
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002E58 RID: 11864
			// (set) Token: 0x06004E9B RID: 20123 RVA: 0x0007D26B File Offset: 0x0007B46B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002E59 RID: 11865
			// (set) Token: 0x06004E9C RID: 20124 RVA: 0x0007D283 File Offset: 0x0007B483
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002E5A RID: 11866
			// (set) Token: 0x06004E9D RID: 20125 RVA: 0x0007D29B File Offset: 0x0007B49B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002E5B RID: 11867
			// (set) Token: 0x06004E9E RID: 20126 RVA: 0x0007D2B3 File Offset: 0x0007B4B3
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005FE RID: 1534
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002E5C RID: 11868
			// (set) Token: 0x06004EA0 RID: 20128 RVA: 0x0007D2D3 File Offset: 0x0007B4D3
			public virtual DatabaseCopyIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002E5D RID: 11869
			// (set) Token: 0x06004EA1 RID: 20129 RVA: 0x0007D2E6 File Offset: 0x0007B4E6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002E5E RID: 11870
			// (set) Token: 0x06004EA2 RID: 20130 RVA: 0x0007D2F9 File Offset: 0x0007B4F9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002E5F RID: 11871
			// (set) Token: 0x06004EA3 RID: 20131 RVA: 0x0007D311 File Offset: 0x0007B511
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002E60 RID: 11872
			// (set) Token: 0x06004EA4 RID: 20132 RVA: 0x0007D329 File Offset: 0x0007B529
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002E61 RID: 11873
			// (set) Token: 0x06004EA5 RID: 20133 RVA: 0x0007D341 File Offset: 0x0007B541
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002E62 RID: 11874
			// (set) Token: 0x06004EA6 RID: 20134 RVA: 0x0007D359 File Offset: 0x0007B559
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002E63 RID: 11875
			// (set) Token: 0x06004EA7 RID: 20135 RVA: 0x0007D371 File Offset: 0x0007B571
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
