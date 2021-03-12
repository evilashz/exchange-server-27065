using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000010 RID: 16
	public class WriteAdminAuditLogCommand : SyntheticCommandWithPipelineInput<AdminAuditLogConfig, AdminAuditLogConfig>
	{
		// Token: 0x060014BA RID: 5306 RVA: 0x00032A98 File Offset: 0x00030C98
		private WriteAdminAuditLogCommand() : base("Write-AdminAuditLog")
		{
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x00032AA5 File Offset: 0x00030CA5
		public WriteAdminAuditLogCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x00032AB4 File Offset: 0x00030CB4
		public virtual WriteAdminAuditLogCommand SetParameters(WriteAdminAuditLogCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x00032ABE File Offset: 0x00030CBE
		public virtual WriteAdminAuditLogCommand SetParameters(WriteAdminAuditLogCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000011 RID: 17
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000053 RID: 83
			// (set) Token: 0x060014BE RID: 5310 RVA: 0x00032AC8 File Offset: 0x00030CC8
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17000054 RID: 84
			// (set) Token: 0x060014BF RID: 5311 RVA: 0x00032ADB File Offset: 0x00030CDB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000055 RID: 85
			// (set) Token: 0x060014C0 RID: 5312 RVA: 0x00032AEE File Offset: 0x00030CEE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000056 RID: 86
			// (set) Token: 0x060014C1 RID: 5313 RVA: 0x00032B06 File Offset: 0x00030D06
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000057 RID: 87
			// (set) Token: 0x060014C2 RID: 5314 RVA: 0x00032B1E File Offset: 0x00030D1E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000058 RID: 88
			// (set) Token: 0x060014C3 RID: 5315 RVA: 0x00032B36 File Offset: 0x00030D36
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000059 RID: 89
			// (set) Token: 0x060014C4 RID: 5316 RVA: 0x00032B4E File Offset: 0x00030D4E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000012 RID: 18
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700005A RID: 90
			// (set) Token: 0x060014C6 RID: 5318 RVA: 0x00032B6E File Offset: 0x00030D6E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700005B RID: 91
			// (set) Token: 0x060014C7 RID: 5319 RVA: 0x00032B8C File Offset: 0x00030D8C
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x1700005C RID: 92
			// (set) Token: 0x060014C8 RID: 5320 RVA: 0x00032B9F File Offset: 0x00030D9F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700005D RID: 93
			// (set) Token: 0x060014C9 RID: 5321 RVA: 0x00032BB2 File Offset: 0x00030DB2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700005E RID: 94
			// (set) Token: 0x060014CA RID: 5322 RVA: 0x00032BCA File Offset: 0x00030DCA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700005F RID: 95
			// (set) Token: 0x060014CB RID: 5323 RVA: 0x00032BE2 File Offset: 0x00030DE2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000060 RID: 96
			// (set) Token: 0x060014CC RID: 5324 RVA: 0x00032BFA File Offset: 0x00030DFA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000061 RID: 97
			// (set) Token: 0x060014CD RID: 5325 RVA: 0x00032C12 File Offset: 0x00030E12
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
