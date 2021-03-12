using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000542 RID: 1346
	public class GetMailboxDatabaseRedundancyCommand : SyntheticCommandWithPipelineInput<DatabaseRedundancy, DatabaseRedundancy>
	{
		// Token: 0x060047C4 RID: 18372 RVA: 0x000748C0 File Offset: 0x00072AC0
		private GetMailboxDatabaseRedundancyCommand() : base("Get-MailboxDatabaseRedundancy")
		{
		}

		// Token: 0x060047C5 RID: 18373 RVA: 0x000748CD File Offset: 0x00072ACD
		public GetMailboxDatabaseRedundancyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060047C6 RID: 18374 RVA: 0x000748DC File Offset: 0x00072ADC
		public virtual GetMailboxDatabaseRedundancyCommand SetParameters(GetMailboxDatabaseRedundancyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060047C7 RID: 18375 RVA: 0x000748E6 File Offset: 0x00072AE6
		public virtual GetMailboxDatabaseRedundancyCommand SetParameters(GetMailboxDatabaseRedundancyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000543 RID: 1347
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170028F9 RID: 10489
			// (set) Token: 0x060047C8 RID: 18376 RVA: 0x000748F0 File Offset: 0x00072AF0
			public virtual DatabaseIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170028FA RID: 10490
			// (set) Token: 0x060047C9 RID: 18377 RVA: 0x00074903 File Offset: 0x00072B03
			public virtual DatabaseAvailabilityGroupIdParameter DatabaseAvailabilityGroup
			{
				set
				{
					base.PowerSharpParameters["DatabaseAvailabilityGroup"] = value;
				}
			}

			// Token: 0x170028FB RID: 10491
			// (set) Token: 0x060047CA RID: 18378 RVA: 0x00074916 File Offset: 0x00072B16
			public virtual ServerIdParameter ServerToContact
			{
				set
				{
					base.PowerSharpParameters["ServerToContact"] = value;
				}
			}

			// Token: 0x170028FC RID: 10492
			// (set) Token: 0x060047CB RID: 18379 RVA: 0x00074929 File Offset: 0x00072B29
			public virtual int TimeoutInSeconds
			{
				set
				{
					base.PowerSharpParameters["TimeoutInSeconds"] = value;
				}
			}

			// Token: 0x170028FD RID: 10493
			// (set) Token: 0x060047CC RID: 18380 RVA: 0x00074941 File Offset: 0x00072B41
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170028FE RID: 10494
			// (set) Token: 0x060047CD RID: 18381 RVA: 0x00074954 File Offset: 0x00072B54
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170028FF RID: 10495
			// (set) Token: 0x060047CE RID: 18382 RVA: 0x0007496C File Offset: 0x00072B6C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002900 RID: 10496
			// (set) Token: 0x060047CF RID: 18383 RVA: 0x00074984 File Offset: 0x00072B84
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002901 RID: 10497
			// (set) Token: 0x060047D0 RID: 18384 RVA: 0x0007499C File Offset: 0x00072B9C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000544 RID: 1348
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002902 RID: 10498
			// (set) Token: 0x060047D2 RID: 18386 RVA: 0x000749BC File Offset: 0x00072BBC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002903 RID: 10499
			// (set) Token: 0x060047D3 RID: 18387 RVA: 0x000749CF File Offset: 0x00072BCF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002904 RID: 10500
			// (set) Token: 0x060047D4 RID: 18388 RVA: 0x000749E7 File Offset: 0x00072BE7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002905 RID: 10501
			// (set) Token: 0x060047D5 RID: 18389 RVA: 0x000749FF File Offset: 0x00072BFF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002906 RID: 10502
			// (set) Token: 0x060047D6 RID: 18390 RVA: 0x00074A17 File Offset: 0x00072C17
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
