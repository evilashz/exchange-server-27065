using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000007 RID: 7
	public class RemoveAdminAuditLogConfigCommand : SyntheticCommandWithPipelineInput<AdminAuditLogConfig, AdminAuditLogConfig>
	{
		// Token: 0x06001463 RID: 5219 RVA: 0x000323A8 File Offset: 0x000305A8
		private RemoveAdminAuditLogConfigCommand() : base("Remove-AdminAuditLogConfig")
		{
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x000323B5 File Offset: 0x000305B5
		public RemoveAdminAuditLogConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x000323C4 File Offset: 0x000305C4
		public virtual RemoveAdminAuditLogConfigCommand SetParameters(RemoveAdminAuditLogConfigCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x000323CE File Offset: 0x000305CE
		public virtual RemoveAdminAuditLogConfigCommand SetParameters(RemoveAdminAuditLogConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000008 RID: 8
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700000E RID: 14
			// (set) Token: 0x06001467 RID: 5223 RVA: 0x000323D8 File Offset: 0x000305D8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AdminAuditLogIdParameter(value) : null);
				}
			}

			// Token: 0x1700000F RID: 15
			// (set) Token: 0x06001468 RID: 5224 RVA: 0x000323F6 File Offset: 0x000305F6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000010 RID: 16
			// (set) Token: 0x06001469 RID: 5225 RVA: 0x00032409 File Offset: 0x00030609
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000011 RID: 17
			// (set) Token: 0x0600146A RID: 5226 RVA: 0x00032421 File Offset: 0x00030621
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000012 RID: 18
			// (set) Token: 0x0600146B RID: 5227 RVA: 0x00032439 File Offset: 0x00030639
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000013 RID: 19
			// (set) Token: 0x0600146C RID: 5228 RVA: 0x00032451 File Offset: 0x00030651
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000014 RID: 20
			// (set) Token: 0x0600146D RID: 5229 RVA: 0x00032469 File Offset: 0x00030669
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000015 RID: 21
			// (set) Token: 0x0600146E RID: 5230 RVA: 0x00032481 File Offset: 0x00030681
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000009 RID: 9
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000016 RID: 22
			// (set) Token: 0x06001470 RID: 5232 RVA: 0x000324A1 File Offset: 0x000306A1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000017 RID: 23
			// (set) Token: 0x06001471 RID: 5233 RVA: 0x000324B4 File Offset: 0x000306B4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000018 RID: 24
			// (set) Token: 0x06001472 RID: 5234 RVA: 0x000324CC File Offset: 0x000306CC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000019 RID: 25
			// (set) Token: 0x06001473 RID: 5235 RVA: 0x000324E4 File Offset: 0x000306E4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700001A RID: 26
			// (set) Token: 0x06001474 RID: 5236 RVA: 0x000324FC File Offset: 0x000306FC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700001B RID: 27
			// (set) Token: 0x06001475 RID: 5237 RVA: 0x00032514 File Offset: 0x00030714
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700001C RID: 28
			// (set) Token: 0x06001476 RID: 5238 RVA: 0x0003252C File Offset: 0x0003072C
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
