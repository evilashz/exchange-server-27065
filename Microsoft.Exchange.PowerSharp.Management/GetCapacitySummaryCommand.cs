using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.MailboxLoadBalanceClient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000201 RID: 513
	public class GetCapacitySummaryCommand : SyntheticCommandWithPipelineInput<CapacitySummary, CapacitySummary>
	{
		// Token: 0x0600290E RID: 10510 RVA: 0x0004D115 File Offset: 0x0004B315
		private GetCapacitySummaryCommand() : base("Get-CapacitySummary")
		{
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x0004D122 File Offset: 0x0004B322
		public GetCapacitySummaryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x0004D131 File Offset: 0x0004B331
		public virtual GetCapacitySummaryCommand SetParameters(GetCapacitySummaryCommand.DatabaseSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x0004D13B File Offset: 0x0004B33B
		public virtual GetCapacitySummaryCommand SetParameters(GetCapacitySummaryCommand.DagSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x0004D145 File Offset: 0x0004B345
		public virtual GetCapacitySummaryCommand SetParameters(GetCapacitySummaryCommand.ForestSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x0004D14F File Offset: 0x0004B34F
		public virtual GetCapacitySummaryCommand SetParameters(GetCapacitySummaryCommand.ServerSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000202 RID: 514
		public class DatabaseSetParameters : ParametersBase
		{
			// Token: 0x170010C5 RID: 4293
			// (set) Token: 0x06002914 RID: 10516 RVA: 0x0004D159 File Offset: 0x0004B359
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170010C6 RID: 4294
			// (set) Token: 0x06002915 RID: 10517 RVA: 0x0004D16C File Offset: 0x0004B36C
			public virtual SwitchParameter Refresh
			{
				set
				{
					base.PowerSharpParameters["Refresh"] = value;
				}
			}

			// Token: 0x170010C7 RID: 4295
			// (set) Token: 0x06002916 RID: 10518 RVA: 0x0004D184 File Offset: 0x0004B384
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170010C8 RID: 4296
			// (set) Token: 0x06002917 RID: 10519 RVA: 0x0004D19C File Offset: 0x0004B39C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170010C9 RID: 4297
			// (set) Token: 0x06002918 RID: 10520 RVA: 0x0004D1B4 File Offset: 0x0004B3B4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170010CA RID: 4298
			// (set) Token: 0x06002919 RID: 10521 RVA: 0x0004D1CC File Offset: 0x0004B3CC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000203 RID: 515
		public class DagSetParameters : ParametersBase
		{
			// Token: 0x170010CB RID: 4299
			// (set) Token: 0x0600291B RID: 10523 RVA: 0x0004D1EC File Offset: 0x0004B3EC
			public virtual DatabaseAvailabilityGroupIdParameter DatabaseAvailabilityGroup
			{
				set
				{
					base.PowerSharpParameters["DatabaseAvailabilityGroup"] = value;
				}
			}

			// Token: 0x170010CC RID: 4300
			// (set) Token: 0x0600291C RID: 10524 RVA: 0x0004D1FF File Offset: 0x0004B3FF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170010CD RID: 4301
			// (set) Token: 0x0600291D RID: 10525 RVA: 0x0004D217 File Offset: 0x0004B417
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170010CE RID: 4302
			// (set) Token: 0x0600291E RID: 10526 RVA: 0x0004D22F File Offset: 0x0004B42F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170010CF RID: 4303
			// (set) Token: 0x0600291F RID: 10527 RVA: 0x0004D247 File Offset: 0x0004B447
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000204 RID: 516
		public class ForestSetParameters : ParametersBase
		{
			// Token: 0x170010D0 RID: 4304
			// (set) Token: 0x06002921 RID: 10529 RVA: 0x0004D267 File Offset: 0x0004B467
			public virtual SwitchParameter Forest
			{
				set
				{
					base.PowerSharpParameters["Forest"] = value;
				}
			}

			// Token: 0x170010D1 RID: 4305
			// (set) Token: 0x06002922 RID: 10530 RVA: 0x0004D27F File Offset: 0x0004B47F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170010D2 RID: 4306
			// (set) Token: 0x06002923 RID: 10531 RVA: 0x0004D297 File Offset: 0x0004B497
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170010D3 RID: 4307
			// (set) Token: 0x06002924 RID: 10532 RVA: 0x0004D2AF File Offset: 0x0004B4AF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170010D4 RID: 4308
			// (set) Token: 0x06002925 RID: 10533 RVA: 0x0004D2C7 File Offset: 0x0004B4C7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000205 RID: 517
		public class ServerSetParameters : ParametersBase
		{
			// Token: 0x170010D5 RID: 4309
			// (set) Token: 0x06002927 RID: 10535 RVA: 0x0004D2E7 File Offset: 0x0004B4E7
			public virtual SwitchParameter Refresh
			{
				set
				{
					base.PowerSharpParameters["Refresh"] = value;
				}
			}

			// Token: 0x170010D6 RID: 4310
			// (set) Token: 0x06002928 RID: 10536 RVA: 0x0004D2FF File Offset: 0x0004B4FF
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170010D7 RID: 4311
			// (set) Token: 0x06002929 RID: 10537 RVA: 0x0004D312 File Offset: 0x0004B512
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170010D8 RID: 4312
			// (set) Token: 0x0600292A RID: 10538 RVA: 0x0004D32A File Offset: 0x0004B52A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170010D9 RID: 4313
			// (set) Token: 0x0600292B RID: 10539 RVA: 0x0004D342 File Offset: 0x0004B542
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170010DA RID: 4314
			// (set) Token: 0x0600292C RID: 10540 RVA: 0x0004D35A File Offset: 0x0004B55A
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
