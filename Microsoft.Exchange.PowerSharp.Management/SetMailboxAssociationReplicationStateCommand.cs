using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001F9 RID: 505
	public class SetMailboxAssociationReplicationStateCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxAssociationReplicationStatePresentationObject>
	{
		// Token: 0x060028D7 RID: 10455 RVA: 0x0004CCC6 File Offset: 0x0004AEC6
		private SetMailboxAssociationReplicationStateCommand() : base("Set-MailboxAssociationReplicationState")
		{
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x0004CCD3 File Offset: 0x0004AED3
		public SetMailboxAssociationReplicationStateCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x0004CCE2 File Offset: 0x0004AEE2
		public virtual SetMailboxAssociationReplicationStateCommand SetParameters(SetMailboxAssociationReplicationStateCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001FA RID: 506
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700109E RID: 4254
			// (set) Token: 0x060028DA RID: 10458 RVA: 0x0004CCEC File Offset: 0x0004AEEC
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700109F RID: 4255
			// (set) Token: 0x060028DB RID: 10459 RVA: 0x0004CD0A File Offset: 0x0004AF0A
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170010A0 RID: 4256
			// (set) Token: 0x060028DC RID: 10460 RVA: 0x0004CD22 File Offset: 0x0004AF22
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170010A1 RID: 4257
			// (set) Token: 0x060028DD RID: 10461 RVA: 0x0004CD35 File Offset: 0x0004AF35
			public virtual ExDateTime? NextReplicationTime
			{
				set
				{
					base.PowerSharpParameters["NextReplicationTime"] = value;
				}
			}

			// Token: 0x170010A2 RID: 4258
			// (set) Token: 0x060028DE RID: 10462 RVA: 0x0004CD4D File Offset: 0x0004AF4D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170010A3 RID: 4259
			// (set) Token: 0x060028DF RID: 10463 RVA: 0x0004CD65 File Offset: 0x0004AF65
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170010A4 RID: 4260
			// (set) Token: 0x060028E0 RID: 10464 RVA: 0x0004CD7D File Offset: 0x0004AF7D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170010A5 RID: 4261
			// (set) Token: 0x060028E1 RID: 10465 RVA: 0x0004CD95 File Offset: 0x0004AF95
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170010A6 RID: 4262
			// (set) Token: 0x060028E2 RID: 10466 RVA: 0x0004CDAD File Offset: 0x0004AFAD
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
