using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001FB RID: 507
	public class StartMailboxAssociationReplicationCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x060028E4 RID: 10468 RVA: 0x0004CDCD File Offset: 0x0004AFCD
		private StartMailboxAssociationReplicationCommand() : base("Start-MailboxAssociationReplication")
		{
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x0004CDDA File Offset: 0x0004AFDA
		public StartMailboxAssociationReplicationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x0004CDE9 File Offset: 0x0004AFE9
		public virtual StartMailboxAssociationReplicationCommand SetParameters(StartMailboxAssociationReplicationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x0004CDF3 File Offset: 0x0004AFF3
		public virtual StartMailboxAssociationReplicationCommand SetParameters(StartMailboxAssociationReplicationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001FC RID: 508
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170010A7 RID: 4263
			// (set) Token: 0x060028E8 RID: 10472 RVA: 0x0004CDFD File Offset: 0x0004AFFD
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170010A8 RID: 4264
			// (set) Token: 0x060028E9 RID: 10473 RVA: 0x0004CE1B File Offset: 0x0004B01B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170010A9 RID: 4265
			// (set) Token: 0x060028EA RID: 10474 RVA: 0x0004CE2E File Offset: 0x0004B02E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170010AA RID: 4266
			// (set) Token: 0x060028EB RID: 10475 RVA: 0x0004CE46 File Offset: 0x0004B046
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170010AB RID: 4267
			// (set) Token: 0x060028EC RID: 10476 RVA: 0x0004CE5E File Offset: 0x0004B05E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170010AC RID: 4268
			// (set) Token: 0x060028ED RID: 10477 RVA: 0x0004CE76 File Offset: 0x0004B076
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170010AD RID: 4269
			// (set) Token: 0x060028EE RID: 10478 RVA: 0x0004CE8E File Offset: 0x0004B08E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020001FD RID: 509
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170010AE RID: 4270
			// (set) Token: 0x060028F0 RID: 10480 RVA: 0x0004CEAE File Offset: 0x0004B0AE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170010AF RID: 4271
			// (set) Token: 0x060028F1 RID: 10481 RVA: 0x0004CEC1 File Offset: 0x0004B0C1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170010B0 RID: 4272
			// (set) Token: 0x060028F2 RID: 10482 RVA: 0x0004CED9 File Offset: 0x0004B0D9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170010B1 RID: 4273
			// (set) Token: 0x060028F3 RID: 10483 RVA: 0x0004CEF1 File Offset: 0x0004B0F1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170010B2 RID: 4274
			// (set) Token: 0x060028F4 RID: 10484 RVA: 0x0004CF09 File Offset: 0x0004B109
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170010B3 RID: 4275
			// (set) Token: 0x060028F5 RID: 10485 RVA: 0x0004CF21 File Offset: 0x0004B121
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
