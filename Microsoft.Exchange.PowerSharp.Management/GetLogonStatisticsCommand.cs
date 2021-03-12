using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mapi;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200021B RID: 539
	public class GetLogonStatisticsCommand : SyntheticCommandWithPipelineInput<LogonStatistics, LogonStatistics>
	{
		// Token: 0x06002A07 RID: 10759 RVA: 0x0004E559 File Offset: 0x0004C759
		private GetLogonStatisticsCommand() : base("Get-LogonStatistics")
		{
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x0004E566 File Offset: 0x0004C766
		public GetLogonStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x0004E575 File Offset: 0x0004C775
		public virtual GetLogonStatisticsCommand SetParameters(GetLogonStatisticsCommand.AuditLogParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x0004E57F File Offset: 0x0004C77F
		public virtual GetLogonStatisticsCommand SetParameters(GetLogonStatisticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x0004E589 File Offset: 0x0004C789
		public virtual GetLogonStatisticsCommand SetParameters(GetLogonStatisticsCommand.DatabaseParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x0004E593 File Offset: 0x0004C793
		public virtual GetLogonStatisticsCommand SetParameters(GetLogonStatisticsCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x0004E59D File Offset: 0x0004C79D
		public virtual GetLogonStatisticsCommand SetParameters(GetLogonStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200021C RID: 540
		public class AuditLogParameters : ParametersBase
		{
			// Token: 0x1700118A RID: 4490
			// (set) Token: 0x06002A0E RID: 10766 RVA: 0x0004E5A7 File Offset: 0x0004C7A7
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new LogonableObjectIdParameter(value) : null);
				}
			}

			// Token: 0x1700118B RID: 4491
			// (set) Token: 0x06002A0F RID: 10767 RVA: 0x0004E5C5 File Offset: 0x0004C7C5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700118C RID: 4492
			// (set) Token: 0x06002A10 RID: 10768 RVA: 0x0004E5D8 File Offset: 0x0004C7D8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700118D RID: 4493
			// (set) Token: 0x06002A11 RID: 10769 RVA: 0x0004E5F0 File Offset: 0x0004C7F0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700118E RID: 4494
			// (set) Token: 0x06002A12 RID: 10770 RVA: 0x0004E608 File Offset: 0x0004C808
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700118F RID: 4495
			// (set) Token: 0x06002A13 RID: 10771 RVA: 0x0004E620 File Offset: 0x0004C820
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200021D RID: 541
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001190 RID: 4496
			// (set) Token: 0x06002A15 RID: 10773 RVA: 0x0004E640 File Offset: 0x0004C840
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new LogonableObjectIdParameter(value) : null);
				}
			}

			// Token: 0x17001191 RID: 4497
			// (set) Token: 0x06002A16 RID: 10774 RVA: 0x0004E65E File Offset: 0x0004C85E
			public virtual ServerIdParameter CopyOnServer
			{
				set
				{
					base.PowerSharpParameters["CopyOnServer"] = value;
				}
			}

			// Token: 0x17001192 RID: 4498
			// (set) Token: 0x06002A17 RID: 10775 RVA: 0x0004E671 File Offset: 0x0004C871
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001193 RID: 4499
			// (set) Token: 0x06002A18 RID: 10776 RVA: 0x0004E684 File Offset: 0x0004C884
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001194 RID: 4500
			// (set) Token: 0x06002A19 RID: 10777 RVA: 0x0004E69C File Offset: 0x0004C89C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001195 RID: 4501
			// (set) Token: 0x06002A1A RID: 10778 RVA: 0x0004E6B4 File Offset: 0x0004C8B4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001196 RID: 4502
			// (set) Token: 0x06002A1B RID: 10779 RVA: 0x0004E6CC File Offset: 0x0004C8CC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200021E RID: 542
		public class DatabaseParameters : ParametersBase
		{
			// Token: 0x17001197 RID: 4503
			// (set) Token: 0x06002A1D RID: 10781 RVA: 0x0004E6EC File Offset: 0x0004C8EC
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17001198 RID: 4504
			// (set) Token: 0x06002A1E RID: 10782 RVA: 0x0004E6FF File Offset: 0x0004C8FF
			public virtual ServerIdParameter CopyOnServer
			{
				set
				{
					base.PowerSharpParameters["CopyOnServer"] = value;
				}
			}

			// Token: 0x17001199 RID: 4505
			// (set) Token: 0x06002A1F RID: 10783 RVA: 0x0004E712 File Offset: 0x0004C912
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700119A RID: 4506
			// (set) Token: 0x06002A20 RID: 10784 RVA: 0x0004E725 File Offset: 0x0004C925
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700119B RID: 4507
			// (set) Token: 0x06002A21 RID: 10785 RVA: 0x0004E73D File Offset: 0x0004C93D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700119C RID: 4508
			// (set) Token: 0x06002A22 RID: 10786 RVA: 0x0004E755 File Offset: 0x0004C955
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700119D RID: 4509
			// (set) Token: 0x06002A23 RID: 10787 RVA: 0x0004E76D File Offset: 0x0004C96D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200021F RID: 543
		public class ServerParameters : ParametersBase
		{
			// Token: 0x1700119E RID: 4510
			// (set) Token: 0x06002A25 RID: 10789 RVA: 0x0004E78D File Offset: 0x0004C98D
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700119F RID: 4511
			// (set) Token: 0x06002A26 RID: 10790 RVA: 0x0004E7A0 File Offset: 0x0004C9A0
			public virtual SwitchParameter IncludePassive
			{
				set
				{
					base.PowerSharpParameters["IncludePassive"] = value;
				}
			}

			// Token: 0x170011A0 RID: 4512
			// (set) Token: 0x06002A27 RID: 10791 RVA: 0x0004E7B8 File Offset: 0x0004C9B8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170011A1 RID: 4513
			// (set) Token: 0x06002A28 RID: 10792 RVA: 0x0004E7CB File Offset: 0x0004C9CB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170011A2 RID: 4514
			// (set) Token: 0x06002A29 RID: 10793 RVA: 0x0004E7E3 File Offset: 0x0004C9E3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170011A3 RID: 4515
			// (set) Token: 0x06002A2A RID: 10794 RVA: 0x0004E7FB File Offset: 0x0004C9FB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170011A4 RID: 4516
			// (set) Token: 0x06002A2B RID: 10795 RVA: 0x0004E813 File Offset: 0x0004CA13
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000220 RID: 544
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170011A5 RID: 4517
			// (set) Token: 0x06002A2D RID: 10797 RVA: 0x0004E833 File Offset: 0x0004CA33
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170011A6 RID: 4518
			// (set) Token: 0x06002A2E RID: 10798 RVA: 0x0004E846 File Offset: 0x0004CA46
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170011A7 RID: 4519
			// (set) Token: 0x06002A2F RID: 10799 RVA: 0x0004E85E File Offset: 0x0004CA5E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170011A8 RID: 4520
			// (set) Token: 0x06002A30 RID: 10800 RVA: 0x0004E876 File Offset: 0x0004CA76
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170011A9 RID: 4521
			// (set) Token: 0x06002A31 RID: 10801 RVA: 0x0004E88E File Offset: 0x0004CA8E
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
