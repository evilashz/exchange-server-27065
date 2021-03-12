using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Migration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000262 RID: 610
	public class GetMigrationConfigCommand : SyntheticCommandWithPipelineInput<MigrationConfig, MigrationConfig>
	{
		// Token: 0x06002CF6 RID: 11510 RVA: 0x00052244 File Offset: 0x00050444
		private GetMigrationConfigCommand() : base("Get-MigrationConfig")
		{
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x00052251 File Offset: 0x00050451
		public GetMigrationConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x00052260 File Offset: 0x00050460
		public virtual GetMigrationConfigCommand SetParameters(GetMigrationConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x0005226A File Offset: 0x0005046A
		public virtual GetMigrationConfigCommand SetParameters(GetMigrationConfigCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000263 RID: 611
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170013EB RID: 5099
			// (set) Token: 0x06002CFA RID: 11514 RVA: 0x00052274 File Offset: 0x00050474
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170013EC RID: 5100
			// (set) Token: 0x06002CFB RID: 11515 RVA: 0x00052292 File Offset: 0x00050492
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170013ED RID: 5101
			// (set) Token: 0x06002CFC RID: 11516 RVA: 0x000522A5 File Offset: 0x000504A5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170013EE RID: 5102
			// (set) Token: 0x06002CFD RID: 11517 RVA: 0x000522BD File Offset: 0x000504BD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170013EF RID: 5103
			// (set) Token: 0x06002CFE RID: 11518 RVA: 0x000522D5 File Offset: 0x000504D5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170013F0 RID: 5104
			// (set) Token: 0x06002CFF RID: 11519 RVA: 0x000522ED File Offset: 0x000504ED
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000264 RID: 612
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170013F1 RID: 5105
			// (set) Token: 0x06002D01 RID: 11521 RVA: 0x0005230D File Offset: 0x0005050D
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MigrationConfigIdParameter(value) : null);
				}
			}

			// Token: 0x170013F2 RID: 5106
			// (set) Token: 0x06002D02 RID: 11522 RVA: 0x0005232B File Offset: 0x0005052B
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170013F3 RID: 5107
			// (set) Token: 0x06002D03 RID: 11523 RVA: 0x00052349 File Offset: 0x00050549
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170013F4 RID: 5108
			// (set) Token: 0x06002D04 RID: 11524 RVA: 0x0005235C File Offset: 0x0005055C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170013F5 RID: 5109
			// (set) Token: 0x06002D05 RID: 11525 RVA: 0x00052374 File Offset: 0x00050574
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170013F6 RID: 5110
			// (set) Token: 0x06002D06 RID: 11526 RVA: 0x0005238C File Offset: 0x0005058C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170013F7 RID: 5111
			// (set) Token: 0x06002D07 RID: 11527 RVA: 0x000523A4 File Offset: 0x000505A4
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
