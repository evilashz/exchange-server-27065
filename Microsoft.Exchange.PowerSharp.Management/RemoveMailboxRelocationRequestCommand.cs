using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A3B RID: 2619
	public class RemoveMailboxRelocationRequestCommand : SyntheticCommandWithPipelineInput<MailboxRelocationRequestIdParameter, MailboxRelocationRequestIdParameter>
	{
		// Token: 0x060082A4 RID: 33444 RVA: 0x000C160A File Offset: 0x000BF80A
		private RemoveMailboxRelocationRequestCommand() : base("Remove-MailboxRelocationRequest")
		{
		}

		// Token: 0x060082A5 RID: 33445 RVA: 0x000C1617 File Offset: 0x000BF817
		public RemoveMailboxRelocationRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060082A6 RID: 33446 RVA: 0x000C1626 File Offset: 0x000BF826
		public virtual RemoveMailboxRelocationRequestCommand SetParameters(RemoveMailboxRelocationRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060082A7 RID: 33447 RVA: 0x000C1630 File Offset: 0x000BF830
		public virtual RemoveMailboxRelocationRequestCommand SetParameters(RemoveMailboxRelocationRequestCommand.MigrationRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060082A8 RID: 33448 RVA: 0x000C163A File Offset: 0x000BF83A
		public virtual RemoveMailboxRelocationRequestCommand SetParameters(RemoveMailboxRelocationRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A3C RID: 2620
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170059E7 RID: 23015
			// (set) Token: 0x060082A9 RID: 33449 RVA: 0x000C1644 File Offset: 0x000BF844
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxRelocationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x170059E8 RID: 23016
			// (set) Token: 0x060082AA RID: 33450 RVA: 0x000C1662 File Offset: 0x000BF862
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170059E9 RID: 23017
			// (set) Token: 0x060082AB RID: 33451 RVA: 0x000C1675 File Offset: 0x000BF875
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170059EA RID: 23018
			// (set) Token: 0x060082AC RID: 33452 RVA: 0x000C168D File Offset: 0x000BF88D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170059EB RID: 23019
			// (set) Token: 0x060082AD RID: 33453 RVA: 0x000C16A5 File Offset: 0x000BF8A5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170059EC RID: 23020
			// (set) Token: 0x060082AE RID: 33454 RVA: 0x000C16BD File Offset: 0x000BF8BD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170059ED RID: 23021
			// (set) Token: 0x060082AF RID: 33455 RVA: 0x000C16D5 File Offset: 0x000BF8D5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170059EE RID: 23022
			// (set) Token: 0x060082B0 RID: 33456 RVA: 0x000C16ED File Offset: 0x000BF8ED
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000A3D RID: 2621
		public class MigrationRequestQueueParameters : ParametersBase
		{
			// Token: 0x170059EF RID: 23023
			// (set) Token: 0x060082B2 RID: 33458 RVA: 0x000C170D File Offset: 0x000BF90D
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x170059F0 RID: 23024
			// (set) Token: 0x060082B3 RID: 33459 RVA: 0x000C1720 File Offset: 0x000BF920
			public virtual Guid RequestGuid
			{
				set
				{
					base.PowerSharpParameters["RequestGuid"] = value;
				}
			}

			// Token: 0x170059F1 RID: 23025
			// (set) Token: 0x060082B4 RID: 33460 RVA: 0x000C1738 File Offset: 0x000BF938
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170059F2 RID: 23026
			// (set) Token: 0x060082B5 RID: 33461 RVA: 0x000C174B File Offset: 0x000BF94B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170059F3 RID: 23027
			// (set) Token: 0x060082B6 RID: 33462 RVA: 0x000C1763 File Offset: 0x000BF963
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170059F4 RID: 23028
			// (set) Token: 0x060082B7 RID: 33463 RVA: 0x000C177B File Offset: 0x000BF97B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170059F5 RID: 23029
			// (set) Token: 0x060082B8 RID: 33464 RVA: 0x000C1793 File Offset: 0x000BF993
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170059F6 RID: 23030
			// (set) Token: 0x060082B9 RID: 33465 RVA: 0x000C17AB File Offset: 0x000BF9AB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170059F7 RID: 23031
			// (set) Token: 0x060082BA RID: 33466 RVA: 0x000C17C3 File Offset: 0x000BF9C3
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000A3E RID: 2622
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170059F8 RID: 23032
			// (set) Token: 0x060082BC RID: 33468 RVA: 0x000C17E3 File Offset: 0x000BF9E3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170059F9 RID: 23033
			// (set) Token: 0x060082BD RID: 33469 RVA: 0x000C17F6 File Offset: 0x000BF9F6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170059FA RID: 23034
			// (set) Token: 0x060082BE RID: 33470 RVA: 0x000C180E File Offset: 0x000BFA0E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170059FB RID: 23035
			// (set) Token: 0x060082BF RID: 33471 RVA: 0x000C1826 File Offset: 0x000BFA26
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170059FC RID: 23036
			// (set) Token: 0x060082C0 RID: 33472 RVA: 0x000C183E File Offset: 0x000BFA3E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170059FD RID: 23037
			// (set) Token: 0x060082C1 RID: 33473 RVA: 0x000C1856 File Offset: 0x000BFA56
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170059FE RID: 23038
			// (set) Token: 0x060082C2 RID: 33474 RVA: 0x000C186E File Offset: 0x000BFA6E
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
