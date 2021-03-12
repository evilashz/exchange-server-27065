using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000004 RID: 4
	public class GetAdminAuditLogConfigCommand : SyntheticCommandWithPipelineInput<AdminAuditLogConfig, AdminAuditLogConfig>
	{
		// Token: 0x06001450 RID: 5200 RVA: 0x00032234 File Offset: 0x00030434
		private GetAdminAuditLogConfigCommand() : base("Get-AdminAuditLogConfig")
		{
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x00032241 File Offset: 0x00030441
		public GetAdminAuditLogConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x00032250 File Offset: 0x00030450
		public virtual GetAdminAuditLogConfigCommand SetParameters(GetAdminAuditLogConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0003225A File Offset: 0x0003045A
		public virtual GetAdminAuditLogConfigCommand SetParameters(GetAdminAuditLogConfigCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000005 RID: 5
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000001 RID: 1
			// (set) Token: 0x06001454 RID: 5204 RVA: 0x00032264 File Offset: 0x00030464
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17000002 RID: 2
			// (set) Token: 0x06001455 RID: 5205 RVA: 0x0003227C File Offset: 0x0003047C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000003 RID: 3
			// (set) Token: 0x06001456 RID: 5206 RVA: 0x0003228F File Offset: 0x0003048F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000004 RID: 4
			// (set) Token: 0x06001457 RID: 5207 RVA: 0x000322A7 File Offset: 0x000304A7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000005 RID: 5
			// (set) Token: 0x06001458 RID: 5208 RVA: 0x000322BF File Offset: 0x000304BF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000006 RID: 6
			// (set) Token: 0x06001459 RID: 5209 RVA: 0x000322D7 File Offset: 0x000304D7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000006 RID: 6
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000007 RID: 7
			// (set) Token: 0x0600145B RID: 5211 RVA: 0x000322F7 File Offset: 0x000304F7
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000008 RID: 8
			// (set) Token: 0x0600145C RID: 5212 RVA: 0x00032315 File Offset: 0x00030515
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17000009 RID: 9
			// (set) Token: 0x0600145D RID: 5213 RVA: 0x0003232D File Offset: 0x0003052D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700000A RID: 10
			// (set) Token: 0x0600145E RID: 5214 RVA: 0x00032340 File Offset: 0x00030540
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700000B RID: 11
			// (set) Token: 0x0600145F RID: 5215 RVA: 0x00032358 File Offset: 0x00030558
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700000C RID: 12
			// (set) Token: 0x06001460 RID: 5216 RVA: 0x00032370 File Offset: 0x00030570
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700000D RID: 13
			// (set) Token: 0x06001461 RID: 5217 RVA: 0x00032388 File Offset: 0x00030588
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
