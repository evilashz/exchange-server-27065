using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200053E RID: 1342
	public class GetMailboxDatabaseCopyStatusCommand : SyntheticCommandWithPipelineInput<DatabaseCopyStatusEntry, DatabaseCopyStatusEntry>
	{
		// Token: 0x0600479E RID: 18334 RVA: 0x000745B7 File Offset: 0x000727B7
		private GetMailboxDatabaseCopyStatusCommand() : base("Get-MailboxDatabaseCopyStatus")
		{
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x000745C4 File Offset: 0x000727C4
		public GetMailboxDatabaseCopyStatusCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060047A0 RID: 18336 RVA: 0x000745D3 File Offset: 0x000727D3
		public virtual GetMailboxDatabaseCopyStatusCommand SetParameters(GetMailboxDatabaseCopyStatusCommand.ExplicitServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x000745DD File Offset: 0x000727DD
		public virtual GetMailboxDatabaseCopyStatusCommand SetParameters(GetMailboxDatabaseCopyStatusCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x000745E7 File Offset: 0x000727E7
		public virtual GetMailboxDatabaseCopyStatusCommand SetParameters(GetMailboxDatabaseCopyStatusCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200053F RID: 1343
		public class ExplicitServerParameters : ParametersBase
		{
			// Token: 0x170028DB RID: 10459
			// (set) Token: 0x060047A3 RID: 18339 RVA: 0x000745F1 File Offset: 0x000727F1
			public virtual MailboxServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170028DC RID: 10460
			// (set) Token: 0x060047A4 RID: 18340 RVA: 0x00074604 File Offset: 0x00072804
			public virtual SwitchParameter Active
			{
				set
				{
					base.PowerSharpParameters["Active"] = value;
				}
			}

			// Token: 0x170028DD RID: 10461
			// (set) Token: 0x060047A5 RID: 18341 RVA: 0x0007461C File Offset: 0x0007281C
			public virtual SwitchParameter ConnectionStatus
			{
				set
				{
					base.PowerSharpParameters["ConnectionStatus"] = value;
				}
			}

			// Token: 0x170028DE RID: 10462
			// (set) Token: 0x060047A6 RID: 18342 RVA: 0x00074634 File Offset: 0x00072834
			public virtual SwitchParameter ExtendedErrorInfo
			{
				set
				{
					base.PowerSharpParameters["ExtendedErrorInfo"] = value;
				}
			}

			// Token: 0x170028DF RID: 10463
			// (set) Token: 0x060047A7 RID: 18343 RVA: 0x0007464C File Offset: 0x0007284C
			public virtual SwitchParameter UseServerCache
			{
				set
				{
					base.PowerSharpParameters["UseServerCache"] = value;
				}
			}

			// Token: 0x170028E0 RID: 10464
			// (set) Token: 0x060047A8 RID: 18344 RVA: 0x00074664 File Offset: 0x00072864
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170028E1 RID: 10465
			// (set) Token: 0x060047A9 RID: 18345 RVA: 0x00074677 File Offset: 0x00072877
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170028E2 RID: 10466
			// (set) Token: 0x060047AA RID: 18346 RVA: 0x0007468F File Offset: 0x0007288F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170028E3 RID: 10467
			// (set) Token: 0x060047AB RID: 18347 RVA: 0x000746A7 File Offset: 0x000728A7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170028E4 RID: 10468
			// (set) Token: 0x060047AC RID: 18348 RVA: 0x000746BF File Offset: 0x000728BF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000540 RID: 1344
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170028E5 RID: 10469
			// (set) Token: 0x060047AE RID: 18350 RVA: 0x000746DF File Offset: 0x000728DF
			public virtual SwitchParameter Active
			{
				set
				{
					base.PowerSharpParameters["Active"] = value;
				}
			}

			// Token: 0x170028E6 RID: 10470
			// (set) Token: 0x060047AF RID: 18351 RVA: 0x000746F7 File Offset: 0x000728F7
			public virtual SwitchParameter ConnectionStatus
			{
				set
				{
					base.PowerSharpParameters["ConnectionStatus"] = value;
				}
			}

			// Token: 0x170028E7 RID: 10471
			// (set) Token: 0x060047B0 RID: 18352 RVA: 0x0007470F File Offset: 0x0007290F
			public virtual SwitchParameter ExtendedErrorInfo
			{
				set
				{
					base.PowerSharpParameters["ExtendedErrorInfo"] = value;
				}
			}

			// Token: 0x170028E8 RID: 10472
			// (set) Token: 0x060047B1 RID: 18353 RVA: 0x00074727 File Offset: 0x00072927
			public virtual SwitchParameter UseServerCache
			{
				set
				{
					base.PowerSharpParameters["UseServerCache"] = value;
				}
			}

			// Token: 0x170028E9 RID: 10473
			// (set) Token: 0x060047B2 RID: 18354 RVA: 0x0007473F File Offset: 0x0007293F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170028EA RID: 10474
			// (set) Token: 0x060047B3 RID: 18355 RVA: 0x00074752 File Offset: 0x00072952
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170028EB RID: 10475
			// (set) Token: 0x060047B4 RID: 18356 RVA: 0x0007476A File Offset: 0x0007296A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170028EC RID: 10476
			// (set) Token: 0x060047B5 RID: 18357 RVA: 0x00074782 File Offset: 0x00072982
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170028ED RID: 10477
			// (set) Token: 0x060047B6 RID: 18358 RVA: 0x0007479A File Offset: 0x0007299A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000541 RID: 1345
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170028EE RID: 10478
			// (set) Token: 0x060047B8 RID: 18360 RVA: 0x000747BA File Offset: 0x000729BA
			public virtual SwitchParameter Local
			{
				set
				{
					base.PowerSharpParameters["Local"] = value;
				}
			}

			// Token: 0x170028EF RID: 10479
			// (set) Token: 0x060047B9 RID: 18361 RVA: 0x000747D2 File Offset: 0x000729D2
			public virtual DatabaseCopyIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170028F0 RID: 10480
			// (set) Token: 0x060047BA RID: 18362 RVA: 0x000747E5 File Offset: 0x000729E5
			public virtual SwitchParameter Active
			{
				set
				{
					base.PowerSharpParameters["Active"] = value;
				}
			}

			// Token: 0x170028F1 RID: 10481
			// (set) Token: 0x060047BB RID: 18363 RVA: 0x000747FD File Offset: 0x000729FD
			public virtual SwitchParameter ConnectionStatus
			{
				set
				{
					base.PowerSharpParameters["ConnectionStatus"] = value;
				}
			}

			// Token: 0x170028F2 RID: 10482
			// (set) Token: 0x060047BC RID: 18364 RVA: 0x00074815 File Offset: 0x00072A15
			public virtual SwitchParameter ExtendedErrorInfo
			{
				set
				{
					base.PowerSharpParameters["ExtendedErrorInfo"] = value;
				}
			}

			// Token: 0x170028F3 RID: 10483
			// (set) Token: 0x060047BD RID: 18365 RVA: 0x0007482D File Offset: 0x00072A2D
			public virtual SwitchParameter UseServerCache
			{
				set
				{
					base.PowerSharpParameters["UseServerCache"] = value;
				}
			}

			// Token: 0x170028F4 RID: 10484
			// (set) Token: 0x060047BE RID: 18366 RVA: 0x00074845 File Offset: 0x00072A45
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170028F5 RID: 10485
			// (set) Token: 0x060047BF RID: 18367 RVA: 0x00074858 File Offset: 0x00072A58
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170028F6 RID: 10486
			// (set) Token: 0x060047C0 RID: 18368 RVA: 0x00074870 File Offset: 0x00072A70
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170028F7 RID: 10487
			// (set) Token: 0x060047C1 RID: 18369 RVA: 0x00074888 File Offset: 0x00072A88
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170028F8 RID: 10488
			// (set) Token: 0x060047C2 RID: 18370 RVA: 0x000748A0 File Offset: 0x00072AA0
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
