using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mapi;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000227 RID: 551
	public class GetStoreUsageStatisticsCommand : SyntheticCommandWithPipelineInput<MailboxResourceMonitor, MailboxResourceMonitor>
	{
		// Token: 0x06002A78 RID: 10872 RVA: 0x0004EE4C File Offset: 0x0004D04C
		private GetStoreUsageStatisticsCommand() : base("Get-StoreUsageStatistics")
		{
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x0004EE59 File Offset: 0x0004D059
		public GetStoreUsageStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x0004EE68 File Offset: 0x0004D068
		public virtual GetStoreUsageStatisticsCommand SetParameters(GetStoreUsageStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x0004EE72 File Offset: 0x0004D072
		public virtual GetStoreUsageStatisticsCommand SetParameters(GetStoreUsageStatisticsCommand.AuditLogParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x0004EE7C File Offset: 0x0004D07C
		public virtual GetStoreUsageStatisticsCommand SetParameters(GetStoreUsageStatisticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x0004EE86 File Offset: 0x0004D086
		public virtual GetStoreUsageStatisticsCommand SetParameters(GetStoreUsageStatisticsCommand.DatabaseParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x0004EE90 File Offset: 0x0004D090
		public virtual GetStoreUsageStatisticsCommand SetParameters(GetStoreUsageStatisticsCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000228 RID: 552
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170011E3 RID: 4579
			// (set) Token: 0x06002A7F RID: 10879 RVA: 0x0004EE9A File Offset: 0x0004D09A
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170011E4 RID: 4580
			// (set) Token: 0x06002A80 RID: 10880 RVA: 0x0004EEAD File Offset: 0x0004D0AD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170011E5 RID: 4581
			// (set) Token: 0x06002A81 RID: 10881 RVA: 0x0004EEC0 File Offset: 0x0004D0C0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170011E6 RID: 4582
			// (set) Token: 0x06002A82 RID: 10882 RVA: 0x0004EED8 File Offset: 0x0004D0D8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170011E7 RID: 4583
			// (set) Token: 0x06002A83 RID: 10883 RVA: 0x0004EEF0 File Offset: 0x0004D0F0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170011E8 RID: 4584
			// (set) Token: 0x06002A84 RID: 10884 RVA: 0x0004EF08 File Offset: 0x0004D108
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000229 RID: 553
		public class AuditLogParameters : ParametersBase
		{
			// Token: 0x170011E9 RID: 4585
			// (set) Token: 0x06002A86 RID: 10886 RVA: 0x0004EF28 File Offset: 0x0004D128
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new GeneralMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170011EA RID: 4586
			// (set) Token: 0x06002A87 RID: 10887 RVA: 0x0004EF46 File Offset: 0x0004D146
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170011EB RID: 4587
			// (set) Token: 0x06002A88 RID: 10888 RVA: 0x0004EF59 File Offset: 0x0004D159
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170011EC RID: 4588
			// (set) Token: 0x06002A89 RID: 10889 RVA: 0x0004EF6C File Offset: 0x0004D16C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170011ED RID: 4589
			// (set) Token: 0x06002A8A RID: 10890 RVA: 0x0004EF84 File Offset: 0x0004D184
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170011EE RID: 4590
			// (set) Token: 0x06002A8B RID: 10891 RVA: 0x0004EF9C File Offset: 0x0004D19C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170011EF RID: 4591
			// (set) Token: 0x06002A8C RID: 10892 RVA: 0x0004EFB4 File Offset: 0x0004D1B4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200022A RID: 554
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170011F0 RID: 4592
			// (set) Token: 0x06002A8E RID: 10894 RVA: 0x0004EFD4 File Offset: 0x0004D1D4
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new GeneralMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170011F1 RID: 4593
			// (set) Token: 0x06002A8F RID: 10895 RVA: 0x0004EFF2 File Offset: 0x0004D1F2
			public virtual ServerIdParameter CopyOnServer
			{
				set
				{
					base.PowerSharpParameters["CopyOnServer"] = value;
				}
			}

			// Token: 0x170011F2 RID: 4594
			// (set) Token: 0x06002A90 RID: 10896 RVA: 0x0004F005 File Offset: 0x0004D205
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170011F3 RID: 4595
			// (set) Token: 0x06002A91 RID: 10897 RVA: 0x0004F018 File Offset: 0x0004D218
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170011F4 RID: 4596
			// (set) Token: 0x06002A92 RID: 10898 RVA: 0x0004F02B File Offset: 0x0004D22B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170011F5 RID: 4597
			// (set) Token: 0x06002A93 RID: 10899 RVA: 0x0004F043 File Offset: 0x0004D243
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170011F6 RID: 4598
			// (set) Token: 0x06002A94 RID: 10900 RVA: 0x0004F05B File Offset: 0x0004D25B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170011F7 RID: 4599
			// (set) Token: 0x06002A95 RID: 10901 RVA: 0x0004F073 File Offset: 0x0004D273
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200022B RID: 555
		public class DatabaseParameters : ParametersBase
		{
			// Token: 0x170011F8 RID: 4600
			// (set) Token: 0x06002A97 RID: 10903 RVA: 0x0004F093 File Offset: 0x0004D293
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170011F9 RID: 4601
			// (set) Token: 0x06002A98 RID: 10904 RVA: 0x0004F0A6 File Offset: 0x0004D2A6
			public virtual ServerIdParameter CopyOnServer
			{
				set
				{
					base.PowerSharpParameters["CopyOnServer"] = value;
				}
			}

			// Token: 0x170011FA RID: 4602
			// (set) Token: 0x06002A99 RID: 10905 RVA: 0x0004F0B9 File Offset: 0x0004D2B9
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170011FB RID: 4603
			// (set) Token: 0x06002A9A RID: 10906 RVA: 0x0004F0CC File Offset: 0x0004D2CC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170011FC RID: 4604
			// (set) Token: 0x06002A9B RID: 10907 RVA: 0x0004F0DF File Offset: 0x0004D2DF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170011FD RID: 4605
			// (set) Token: 0x06002A9C RID: 10908 RVA: 0x0004F0F7 File Offset: 0x0004D2F7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170011FE RID: 4606
			// (set) Token: 0x06002A9D RID: 10909 RVA: 0x0004F10F File Offset: 0x0004D30F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170011FF RID: 4607
			// (set) Token: 0x06002A9E RID: 10910 RVA: 0x0004F127 File Offset: 0x0004D327
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200022C RID: 556
		public class ServerParameters : ParametersBase
		{
			// Token: 0x17001200 RID: 4608
			// (set) Token: 0x06002AA0 RID: 10912 RVA: 0x0004F147 File Offset: 0x0004D347
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17001201 RID: 4609
			// (set) Token: 0x06002AA1 RID: 10913 RVA: 0x0004F15A File Offset: 0x0004D35A
			public virtual SwitchParameter IncludePassive
			{
				set
				{
					base.PowerSharpParameters["IncludePassive"] = value;
				}
			}

			// Token: 0x17001202 RID: 4610
			// (set) Token: 0x06002AA2 RID: 10914 RVA: 0x0004F172 File Offset: 0x0004D372
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17001203 RID: 4611
			// (set) Token: 0x06002AA3 RID: 10915 RVA: 0x0004F185 File Offset: 0x0004D385
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001204 RID: 4612
			// (set) Token: 0x06002AA4 RID: 10916 RVA: 0x0004F198 File Offset: 0x0004D398
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001205 RID: 4613
			// (set) Token: 0x06002AA5 RID: 10917 RVA: 0x0004F1B0 File Offset: 0x0004D3B0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001206 RID: 4614
			// (set) Token: 0x06002AA6 RID: 10918 RVA: 0x0004F1C8 File Offset: 0x0004D3C8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001207 RID: 4615
			// (set) Token: 0x06002AA7 RID: 10919 RVA: 0x0004F1E0 File Offset: 0x0004D3E0
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
