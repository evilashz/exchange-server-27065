using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200014F RID: 335
	public class GetFfoMigrationReportCommand : SyntheticCommandWithPipelineInput<FfoMigrationReport, FfoMigrationReport>
	{
		// Token: 0x06002136 RID: 8502 RVA: 0x00042B26 File Offset: 0x00040D26
		private GetFfoMigrationReportCommand() : base("Get-FfoMigrationReport")
		{
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x00042B33 File Offset: 0x00040D33
		public GetFfoMigrationReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x00042B42 File Offset: 0x00040D42
		public virtual GetFfoMigrationReportCommand SetParameters(GetFfoMigrationReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000150 RID: 336
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000A51 RID: 2641
			// (set) Token: 0x06002139 RID: 8505 RVA: 0x00042B4C File Offset: 0x00040D4C
			public virtual int Page
			{
				set
				{
					base.PowerSharpParameters["Page"] = value;
				}
			}

			// Token: 0x17000A52 RID: 2642
			// (set) Token: 0x0600213A RID: 8506 RVA: 0x00042B64 File Offset: 0x00040D64
			public virtual int PageSize
			{
				set
				{
					base.PowerSharpParameters["PageSize"] = value;
				}
			}

			// Token: 0x17000A53 RID: 2643
			// (set) Token: 0x0600213B RID: 8507 RVA: 0x00042B7C File Offset: 0x00040D7C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000A54 RID: 2644
			// (set) Token: 0x0600213C RID: 8508 RVA: 0x00042B9A File Offset: 0x00040D9A
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17000A55 RID: 2645
			// (set) Token: 0x0600213D RID: 8509 RVA: 0x00042BAD File Offset: 0x00040DAD
			public virtual string ProbeTag
			{
				set
				{
					base.PowerSharpParameters["ProbeTag"] = value;
				}
			}

			// Token: 0x17000A56 RID: 2646
			// (set) Token: 0x0600213E RID: 8510 RVA: 0x00042BC0 File Offset: 0x00040DC0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000A57 RID: 2647
			// (set) Token: 0x0600213F RID: 8511 RVA: 0x00042BD8 File Offset: 0x00040DD8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000A58 RID: 2648
			// (set) Token: 0x06002140 RID: 8512 RVA: 0x00042BF0 File Offset: 0x00040DF0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000A59 RID: 2649
			// (set) Token: 0x06002141 RID: 8513 RVA: 0x00042C08 File Offset: 0x00040E08
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
