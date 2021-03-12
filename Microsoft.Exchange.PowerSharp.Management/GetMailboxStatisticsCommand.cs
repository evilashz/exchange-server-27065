using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mapi;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000221 RID: 545
	public class GetMailboxStatisticsCommand : SyntheticCommandWithPipelineInput<MailboxStatistics, MailboxStatistics>
	{
		// Token: 0x06002A33 RID: 10803 RVA: 0x0004E8AE File Offset: 0x0004CAAE
		private GetMailboxStatisticsCommand() : base("Get-MailboxStatistics")
		{
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x0004E8BB File Offset: 0x0004CABB
		public GetMailboxStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x0004E8CA File Offset: 0x0004CACA
		public virtual GetMailboxStatisticsCommand SetParameters(GetMailboxStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x0004E8D4 File Offset: 0x0004CAD4
		public virtual GetMailboxStatisticsCommand SetParameters(GetMailboxStatisticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x0004E8DE File Offset: 0x0004CADE
		public virtual GetMailboxStatisticsCommand SetParameters(GetMailboxStatisticsCommand.AuditLogParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x0004E8E8 File Offset: 0x0004CAE8
		public virtual GetMailboxStatisticsCommand SetParameters(GetMailboxStatisticsCommand.DatabaseParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x0004E8F2 File Offset: 0x0004CAF2
		public virtual GetMailboxStatisticsCommand SetParameters(GetMailboxStatisticsCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000222 RID: 546
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170011AA RID: 4522
			// (set) Token: 0x06002A3A RID: 10810 RVA: 0x0004E8FC File Offset: 0x0004CAFC
			public virtual SwitchParameter IncludeMoveHistory
			{
				set
				{
					base.PowerSharpParameters["IncludeMoveHistory"] = value;
				}
			}

			// Token: 0x170011AB RID: 4523
			// (set) Token: 0x06002A3B RID: 10811 RVA: 0x0004E914 File Offset: 0x0004CB14
			public virtual SwitchParameter IncludeMoveReport
			{
				set
				{
					base.PowerSharpParameters["IncludeMoveReport"] = value;
				}
			}

			// Token: 0x170011AC RID: 4524
			// (set) Token: 0x06002A3C RID: 10812 RVA: 0x0004E92C File Offset: 0x0004CB2C
			public virtual SwitchParameter NoADLookup
			{
				set
				{
					base.PowerSharpParameters["NoADLookup"] = value;
				}
			}

			// Token: 0x170011AD RID: 4525
			// (set) Token: 0x06002A3D RID: 10813 RVA: 0x0004E944 File Offset: 0x0004CB44
			public virtual SwitchParameter IncludeQuarantineDetails
			{
				set
				{
					base.PowerSharpParameters["IncludeQuarantineDetails"] = value;
				}
			}

			// Token: 0x170011AE RID: 4526
			// (set) Token: 0x06002A3E RID: 10814 RVA: 0x0004E95C File Offset: 0x0004CB5C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170011AF RID: 4527
			// (set) Token: 0x06002A3F RID: 10815 RVA: 0x0004E96F File Offset: 0x0004CB6F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170011B0 RID: 4528
			// (set) Token: 0x06002A40 RID: 10816 RVA: 0x0004E987 File Offset: 0x0004CB87
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170011B1 RID: 4529
			// (set) Token: 0x06002A41 RID: 10817 RVA: 0x0004E99F File Offset: 0x0004CB9F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170011B2 RID: 4530
			// (set) Token: 0x06002A42 RID: 10818 RVA: 0x0004E9B7 File Offset: 0x0004CBB7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000223 RID: 547
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170011B3 RID: 4531
			// (set) Token: 0x06002A44 RID: 10820 RVA: 0x0004E9D7 File Offset: 0x0004CBD7
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170011B4 RID: 4532
			// (set) Token: 0x06002A45 RID: 10821 RVA: 0x0004E9EF File Offset: 0x0004CBEF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new GeneralMailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x170011B5 RID: 4533
			// (set) Token: 0x06002A46 RID: 10822 RVA: 0x0004EA0D File Offset: 0x0004CC0D
			public virtual ServerIdParameter CopyOnServer
			{
				set
				{
					base.PowerSharpParameters["CopyOnServer"] = value;
				}
			}

			// Token: 0x170011B6 RID: 4534
			// (set) Token: 0x06002A47 RID: 10823 RVA: 0x0004EA20 File Offset: 0x0004CC20
			public virtual SwitchParameter IncludeMoveHistory
			{
				set
				{
					base.PowerSharpParameters["IncludeMoveHistory"] = value;
				}
			}

			// Token: 0x170011B7 RID: 4535
			// (set) Token: 0x06002A48 RID: 10824 RVA: 0x0004EA38 File Offset: 0x0004CC38
			public virtual SwitchParameter IncludeMoveReport
			{
				set
				{
					base.PowerSharpParameters["IncludeMoveReport"] = value;
				}
			}

			// Token: 0x170011B8 RID: 4536
			// (set) Token: 0x06002A49 RID: 10825 RVA: 0x0004EA50 File Offset: 0x0004CC50
			public virtual SwitchParameter NoADLookup
			{
				set
				{
					base.PowerSharpParameters["NoADLookup"] = value;
				}
			}

			// Token: 0x170011B9 RID: 4537
			// (set) Token: 0x06002A4A RID: 10826 RVA: 0x0004EA68 File Offset: 0x0004CC68
			public virtual SwitchParameter IncludeQuarantineDetails
			{
				set
				{
					base.PowerSharpParameters["IncludeQuarantineDetails"] = value;
				}
			}

			// Token: 0x170011BA RID: 4538
			// (set) Token: 0x06002A4B RID: 10827 RVA: 0x0004EA80 File Offset: 0x0004CC80
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170011BB RID: 4539
			// (set) Token: 0x06002A4C RID: 10828 RVA: 0x0004EA93 File Offset: 0x0004CC93
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170011BC RID: 4540
			// (set) Token: 0x06002A4D RID: 10829 RVA: 0x0004EAAB File Offset: 0x0004CCAB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170011BD RID: 4541
			// (set) Token: 0x06002A4E RID: 10830 RVA: 0x0004EAC3 File Offset: 0x0004CCC3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170011BE RID: 4542
			// (set) Token: 0x06002A4F RID: 10831 RVA: 0x0004EADB File Offset: 0x0004CCDB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000224 RID: 548
		public class AuditLogParameters : ParametersBase
		{
			// Token: 0x170011BF RID: 4543
			// (set) Token: 0x06002A51 RID: 10833 RVA: 0x0004EAFB File Offset: 0x0004CCFB
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x170011C0 RID: 4544
			// (set) Token: 0x06002A52 RID: 10834 RVA: 0x0004EB13 File Offset: 0x0004CD13
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new GeneralMailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x170011C1 RID: 4545
			// (set) Token: 0x06002A53 RID: 10835 RVA: 0x0004EB31 File Offset: 0x0004CD31
			public virtual SwitchParameter IncludeMoveHistory
			{
				set
				{
					base.PowerSharpParameters["IncludeMoveHistory"] = value;
				}
			}

			// Token: 0x170011C2 RID: 4546
			// (set) Token: 0x06002A54 RID: 10836 RVA: 0x0004EB49 File Offset: 0x0004CD49
			public virtual SwitchParameter IncludeMoveReport
			{
				set
				{
					base.PowerSharpParameters["IncludeMoveReport"] = value;
				}
			}

			// Token: 0x170011C3 RID: 4547
			// (set) Token: 0x06002A55 RID: 10837 RVA: 0x0004EB61 File Offset: 0x0004CD61
			public virtual SwitchParameter NoADLookup
			{
				set
				{
					base.PowerSharpParameters["NoADLookup"] = value;
				}
			}

			// Token: 0x170011C4 RID: 4548
			// (set) Token: 0x06002A56 RID: 10838 RVA: 0x0004EB79 File Offset: 0x0004CD79
			public virtual SwitchParameter IncludeQuarantineDetails
			{
				set
				{
					base.PowerSharpParameters["IncludeQuarantineDetails"] = value;
				}
			}

			// Token: 0x170011C5 RID: 4549
			// (set) Token: 0x06002A57 RID: 10839 RVA: 0x0004EB91 File Offset: 0x0004CD91
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170011C6 RID: 4550
			// (set) Token: 0x06002A58 RID: 10840 RVA: 0x0004EBA4 File Offset: 0x0004CDA4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170011C7 RID: 4551
			// (set) Token: 0x06002A59 RID: 10841 RVA: 0x0004EBBC File Offset: 0x0004CDBC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170011C8 RID: 4552
			// (set) Token: 0x06002A5A RID: 10842 RVA: 0x0004EBD4 File Offset: 0x0004CDD4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170011C9 RID: 4553
			// (set) Token: 0x06002A5B RID: 10843 RVA: 0x0004EBEC File Offset: 0x0004CDEC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000225 RID: 549
		public class DatabaseParameters : ParametersBase
		{
			// Token: 0x170011CA RID: 4554
			// (set) Token: 0x06002A5D RID: 10845 RVA: 0x0004EC0C File Offset: 0x0004CE0C
			public virtual StoreMailboxIdParameter StoreMailboxIdentity
			{
				set
				{
					base.PowerSharpParameters["StoreMailboxIdentity"] = value;
				}
			}

			// Token: 0x170011CB RID: 4555
			// (set) Token: 0x06002A5E RID: 10846 RVA: 0x0004EC1F File Offset: 0x0004CE1F
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170011CC RID: 4556
			// (set) Token: 0x06002A5F RID: 10847 RVA: 0x0004EC32 File Offset: 0x0004CE32
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170011CD RID: 4557
			// (set) Token: 0x06002A60 RID: 10848 RVA: 0x0004EC45 File Offset: 0x0004CE45
			public virtual ServerIdParameter CopyOnServer
			{
				set
				{
					base.PowerSharpParameters["CopyOnServer"] = value;
				}
			}

			// Token: 0x170011CE RID: 4558
			// (set) Token: 0x06002A61 RID: 10849 RVA: 0x0004EC58 File Offset: 0x0004CE58
			public virtual SwitchParameter IncludeMoveHistory
			{
				set
				{
					base.PowerSharpParameters["IncludeMoveHistory"] = value;
				}
			}

			// Token: 0x170011CF RID: 4559
			// (set) Token: 0x06002A62 RID: 10850 RVA: 0x0004EC70 File Offset: 0x0004CE70
			public virtual SwitchParameter IncludeMoveReport
			{
				set
				{
					base.PowerSharpParameters["IncludeMoveReport"] = value;
				}
			}

			// Token: 0x170011D0 RID: 4560
			// (set) Token: 0x06002A63 RID: 10851 RVA: 0x0004EC88 File Offset: 0x0004CE88
			public virtual SwitchParameter NoADLookup
			{
				set
				{
					base.PowerSharpParameters["NoADLookup"] = value;
				}
			}

			// Token: 0x170011D1 RID: 4561
			// (set) Token: 0x06002A64 RID: 10852 RVA: 0x0004ECA0 File Offset: 0x0004CEA0
			public virtual SwitchParameter IncludeQuarantineDetails
			{
				set
				{
					base.PowerSharpParameters["IncludeQuarantineDetails"] = value;
				}
			}

			// Token: 0x170011D2 RID: 4562
			// (set) Token: 0x06002A65 RID: 10853 RVA: 0x0004ECB8 File Offset: 0x0004CEB8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170011D3 RID: 4563
			// (set) Token: 0x06002A66 RID: 10854 RVA: 0x0004ECCB File Offset: 0x0004CECB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170011D4 RID: 4564
			// (set) Token: 0x06002A67 RID: 10855 RVA: 0x0004ECE3 File Offset: 0x0004CEE3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170011D5 RID: 4565
			// (set) Token: 0x06002A68 RID: 10856 RVA: 0x0004ECFB File Offset: 0x0004CEFB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170011D6 RID: 4566
			// (set) Token: 0x06002A69 RID: 10857 RVA: 0x0004ED13 File Offset: 0x0004CF13
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000226 RID: 550
		public class ServerParameters : ParametersBase
		{
			// Token: 0x170011D7 RID: 4567
			// (set) Token: 0x06002A6B RID: 10859 RVA: 0x0004ED33 File Offset: 0x0004CF33
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170011D8 RID: 4568
			// (set) Token: 0x06002A6C RID: 10860 RVA: 0x0004ED46 File Offset: 0x0004CF46
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170011D9 RID: 4569
			// (set) Token: 0x06002A6D RID: 10861 RVA: 0x0004ED59 File Offset: 0x0004CF59
			public virtual SwitchParameter IncludePassive
			{
				set
				{
					base.PowerSharpParameters["IncludePassive"] = value;
				}
			}

			// Token: 0x170011DA RID: 4570
			// (set) Token: 0x06002A6E RID: 10862 RVA: 0x0004ED71 File Offset: 0x0004CF71
			public virtual SwitchParameter IncludeMoveHistory
			{
				set
				{
					base.PowerSharpParameters["IncludeMoveHistory"] = value;
				}
			}

			// Token: 0x170011DB RID: 4571
			// (set) Token: 0x06002A6F RID: 10863 RVA: 0x0004ED89 File Offset: 0x0004CF89
			public virtual SwitchParameter IncludeMoveReport
			{
				set
				{
					base.PowerSharpParameters["IncludeMoveReport"] = value;
				}
			}

			// Token: 0x170011DC RID: 4572
			// (set) Token: 0x06002A70 RID: 10864 RVA: 0x0004EDA1 File Offset: 0x0004CFA1
			public virtual SwitchParameter NoADLookup
			{
				set
				{
					base.PowerSharpParameters["NoADLookup"] = value;
				}
			}

			// Token: 0x170011DD RID: 4573
			// (set) Token: 0x06002A71 RID: 10865 RVA: 0x0004EDB9 File Offset: 0x0004CFB9
			public virtual SwitchParameter IncludeQuarantineDetails
			{
				set
				{
					base.PowerSharpParameters["IncludeQuarantineDetails"] = value;
				}
			}

			// Token: 0x170011DE RID: 4574
			// (set) Token: 0x06002A72 RID: 10866 RVA: 0x0004EDD1 File Offset: 0x0004CFD1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170011DF RID: 4575
			// (set) Token: 0x06002A73 RID: 10867 RVA: 0x0004EDE4 File Offset: 0x0004CFE4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170011E0 RID: 4576
			// (set) Token: 0x06002A74 RID: 10868 RVA: 0x0004EDFC File Offset: 0x0004CFFC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170011E1 RID: 4577
			// (set) Token: 0x06002A75 RID: 10869 RVA: 0x0004EE14 File Offset: 0x0004D014
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170011E2 RID: 4578
			// (set) Token: 0x06002A76 RID: 10870 RVA: 0x0004EE2C File Offset: 0x0004D02C
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
