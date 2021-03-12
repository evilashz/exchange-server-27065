using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000545 RID: 1349
	public class GetMailboxServerRedundancyCommand : SyntheticCommandWithPipelineInput<ServerRedundancy, ServerRedundancy>
	{
		// Token: 0x060047D8 RID: 18392 RVA: 0x00074A37 File Offset: 0x00072C37
		private GetMailboxServerRedundancyCommand() : base("Get-MailboxServerRedundancy")
		{
		}

		// Token: 0x060047D9 RID: 18393 RVA: 0x00074A44 File Offset: 0x00072C44
		public GetMailboxServerRedundancyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060047DA RID: 18394 RVA: 0x00074A53 File Offset: 0x00072C53
		public virtual GetMailboxServerRedundancyCommand SetParameters(GetMailboxServerRedundancyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060047DB RID: 18395 RVA: 0x00074A5D File Offset: 0x00072C5D
		public virtual GetMailboxServerRedundancyCommand SetParameters(GetMailboxServerRedundancyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000546 RID: 1350
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002907 RID: 10503
			// (set) Token: 0x060047DC RID: 18396 RVA: 0x00074A67 File Offset: 0x00072C67
			public virtual ServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002908 RID: 10504
			// (set) Token: 0x060047DD RID: 18397 RVA: 0x00074A7A File Offset: 0x00072C7A
			public virtual DatabaseAvailabilityGroupIdParameter DatabaseAvailabilityGroup
			{
				set
				{
					base.PowerSharpParameters["DatabaseAvailabilityGroup"] = value;
				}
			}

			// Token: 0x17002909 RID: 10505
			// (set) Token: 0x060047DE RID: 18398 RVA: 0x00074A8D File Offset: 0x00072C8D
			public virtual ServerIdParameter ServerToContact
			{
				set
				{
					base.PowerSharpParameters["ServerToContact"] = value;
				}
			}

			// Token: 0x1700290A RID: 10506
			// (set) Token: 0x060047DF RID: 18399 RVA: 0x00074AA0 File Offset: 0x00072CA0
			public virtual int TimeoutInSeconds
			{
				set
				{
					base.PowerSharpParameters["TimeoutInSeconds"] = value;
				}
			}

			// Token: 0x1700290B RID: 10507
			// (set) Token: 0x060047E0 RID: 18400 RVA: 0x00074AB8 File Offset: 0x00072CB8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700290C RID: 10508
			// (set) Token: 0x060047E1 RID: 18401 RVA: 0x00074ACB File Offset: 0x00072CCB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700290D RID: 10509
			// (set) Token: 0x060047E2 RID: 18402 RVA: 0x00074AE3 File Offset: 0x00072CE3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700290E RID: 10510
			// (set) Token: 0x060047E3 RID: 18403 RVA: 0x00074AFB File Offset: 0x00072CFB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700290F RID: 10511
			// (set) Token: 0x060047E4 RID: 18404 RVA: 0x00074B13 File Offset: 0x00072D13
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000547 RID: 1351
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002910 RID: 10512
			// (set) Token: 0x060047E6 RID: 18406 RVA: 0x00074B33 File Offset: 0x00072D33
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002911 RID: 10513
			// (set) Token: 0x060047E7 RID: 18407 RVA: 0x00074B46 File Offset: 0x00072D46
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002912 RID: 10514
			// (set) Token: 0x060047E8 RID: 18408 RVA: 0x00074B5E File Offset: 0x00072D5E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002913 RID: 10515
			// (set) Token: 0x060047E9 RID: 18409 RVA: 0x00074B76 File Offset: 0x00072D76
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002914 RID: 10516
			// (set) Token: 0x060047EA RID: 18410 RVA: 0x00074B8E File Offset: 0x00072D8E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
