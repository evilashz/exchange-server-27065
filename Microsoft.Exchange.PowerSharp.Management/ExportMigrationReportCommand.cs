using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Migration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200025A RID: 602
	public class ExportMigrationReportCommand : SyntheticCommandWithPipelineInput<MigrationReport, MigrationReport>
	{
		// Token: 0x06002CA6 RID: 11430 RVA: 0x00051B72 File Offset: 0x0004FD72
		private ExportMigrationReportCommand() : base("Export-MigrationReport")
		{
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x00051B7F File Offset: 0x0004FD7F
		public ExportMigrationReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x00051B8E File Offset: 0x0004FD8E
		public virtual ExportMigrationReportCommand SetParameters(ExportMigrationReportCommand.StreamBasedParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x00051B98 File Offset: 0x0004FD98
		public virtual ExportMigrationReportCommand SetParameters(ExportMigrationReportCommand.PagedParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x00051BA2 File Offset: 0x0004FDA2
		public virtual ExportMigrationReportCommand SetParameters(ExportMigrationReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200025B RID: 603
		public class StreamBasedParameters : ParametersBase
		{
			// Token: 0x170013AB RID: 5035
			// (set) Token: 0x06002CAB RID: 11435 RVA: 0x00051BAC File Offset: 0x0004FDAC
			public virtual Stream CsvStream
			{
				set
				{
					base.PowerSharpParameters["CsvStream"] = value;
				}
			}

			// Token: 0x170013AC RID: 5036
			// (set) Token: 0x06002CAC RID: 11436 RVA: 0x00051BBF File Offset: 0x0004FDBF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MigrationReportIdParameter(value) : null);
				}
			}

			// Token: 0x170013AD RID: 5037
			// (set) Token: 0x06002CAD RID: 11437 RVA: 0x00051BDD File Offset: 0x0004FDDD
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170013AE RID: 5038
			// (set) Token: 0x06002CAE RID: 11438 RVA: 0x00051BFB File Offset: 0x0004FDFB
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170013AF RID: 5039
			// (set) Token: 0x06002CAF RID: 11439 RVA: 0x00051C19 File Offset: 0x0004FE19
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170013B0 RID: 5040
			// (set) Token: 0x06002CB0 RID: 11440 RVA: 0x00051C2C File Offset: 0x0004FE2C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170013B1 RID: 5041
			// (set) Token: 0x06002CB1 RID: 11441 RVA: 0x00051C44 File Offset: 0x0004FE44
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170013B2 RID: 5042
			// (set) Token: 0x06002CB2 RID: 11442 RVA: 0x00051C5C File Offset: 0x0004FE5C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170013B3 RID: 5043
			// (set) Token: 0x06002CB3 RID: 11443 RVA: 0x00051C74 File Offset: 0x0004FE74
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170013B4 RID: 5044
			// (set) Token: 0x06002CB4 RID: 11444 RVA: 0x00051C8C File Offset: 0x0004FE8C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200025C RID: 604
		public class PagedParameters : ParametersBase
		{
			// Token: 0x170013B5 RID: 5045
			// (set) Token: 0x06002CB6 RID: 11446 RVA: 0x00051CAC File Offset: 0x0004FEAC
			public virtual int StartingRowIndex
			{
				set
				{
					base.PowerSharpParameters["StartingRowIndex"] = value;
				}
			}

			// Token: 0x170013B6 RID: 5046
			// (set) Token: 0x06002CB7 RID: 11447 RVA: 0x00051CC4 File Offset: 0x0004FEC4
			public virtual int RowCount
			{
				set
				{
					base.PowerSharpParameters["RowCount"] = value;
				}
			}

			// Token: 0x170013B7 RID: 5047
			// (set) Token: 0x06002CB8 RID: 11448 RVA: 0x00051CDC File Offset: 0x0004FEDC
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MigrationReportIdParameter(value) : null);
				}
			}

			// Token: 0x170013B8 RID: 5048
			// (set) Token: 0x06002CB9 RID: 11449 RVA: 0x00051CFA File Offset: 0x0004FEFA
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170013B9 RID: 5049
			// (set) Token: 0x06002CBA RID: 11450 RVA: 0x00051D18 File Offset: 0x0004FF18
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170013BA RID: 5050
			// (set) Token: 0x06002CBB RID: 11451 RVA: 0x00051D36 File Offset: 0x0004FF36
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170013BB RID: 5051
			// (set) Token: 0x06002CBC RID: 11452 RVA: 0x00051D49 File Offset: 0x0004FF49
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170013BC RID: 5052
			// (set) Token: 0x06002CBD RID: 11453 RVA: 0x00051D61 File Offset: 0x0004FF61
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170013BD RID: 5053
			// (set) Token: 0x06002CBE RID: 11454 RVA: 0x00051D79 File Offset: 0x0004FF79
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170013BE RID: 5054
			// (set) Token: 0x06002CBF RID: 11455 RVA: 0x00051D91 File Offset: 0x0004FF91
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170013BF RID: 5055
			// (set) Token: 0x06002CC0 RID: 11456 RVA: 0x00051DA9 File Offset: 0x0004FFA9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200025D RID: 605
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170013C0 RID: 5056
			// (set) Token: 0x06002CC2 RID: 11458 RVA: 0x00051DC9 File Offset: 0x0004FFC9
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170013C1 RID: 5057
			// (set) Token: 0x06002CC3 RID: 11459 RVA: 0x00051DE7 File Offset: 0x0004FFE7
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170013C2 RID: 5058
			// (set) Token: 0x06002CC4 RID: 11460 RVA: 0x00051E05 File Offset: 0x00050005
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170013C3 RID: 5059
			// (set) Token: 0x06002CC5 RID: 11461 RVA: 0x00051E18 File Offset: 0x00050018
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170013C4 RID: 5060
			// (set) Token: 0x06002CC6 RID: 11462 RVA: 0x00051E30 File Offset: 0x00050030
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170013C5 RID: 5061
			// (set) Token: 0x06002CC7 RID: 11463 RVA: 0x00051E48 File Offset: 0x00050048
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170013C6 RID: 5062
			// (set) Token: 0x06002CC8 RID: 11464 RVA: 0x00051E60 File Offset: 0x00050060
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170013C7 RID: 5063
			// (set) Token: 0x06002CC9 RID: 11465 RVA: 0x00051E78 File Offset: 0x00050078
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
