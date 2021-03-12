using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200000D RID: 13
	public class SetAdminAuditLogConfigCommand : SyntheticCommandWithPipelineInputNoOutput<AdminAuditLogConfig>
	{
		// Token: 0x06001493 RID: 5267 RVA: 0x0003276C File Offset: 0x0003096C
		private SetAdminAuditLogConfigCommand() : base("Set-AdminAuditLogConfig")
		{
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x00032779 File Offset: 0x00030979
		public SetAdminAuditLogConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x00032788 File Offset: 0x00030988
		public virtual SetAdminAuditLogConfigCommand SetParameters(SetAdminAuditLogConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x00032792 File Offset: 0x00030992
		public virtual SetAdminAuditLogConfigCommand SetParameters(SetAdminAuditLogConfigCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200000E RID: 14
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000032 RID: 50
			// (set) Token: 0x06001497 RID: 5271 RVA: 0x0003279C File Offset: 0x0003099C
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17000033 RID: 51
			// (set) Token: 0x06001498 RID: 5272 RVA: 0x000327B4 File Offset: 0x000309B4
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000034 RID: 52
			// (set) Token: 0x06001499 RID: 5273 RVA: 0x000327CC File Offset: 0x000309CC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000035 RID: 53
			// (set) Token: 0x0600149A RID: 5274 RVA: 0x000327DF File Offset: 0x000309DF
			public virtual bool AdminAuditLogEnabled
			{
				set
				{
					base.PowerSharpParameters["AdminAuditLogEnabled"] = value;
				}
			}

			// Token: 0x17000036 RID: 54
			// (set) Token: 0x0600149B RID: 5275 RVA: 0x000327F7 File Offset: 0x000309F7
			public virtual AuditLogLevel LogLevel
			{
				set
				{
					base.PowerSharpParameters["LogLevel"] = value;
				}
			}

			// Token: 0x17000037 RID: 55
			// (set) Token: 0x0600149C RID: 5276 RVA: 0x0003280F File Offset: 0x00030A0F
			public virtual bool TestCmdletLoggingEnabled
			{
				set
				{
					base.PowerSharpParameters["TestCmdletLoggingEnabled"] = value;
				}
			}

			// Token: 0x17000038 RID: 56
			// (set) Token: 0x0600149D RID: 5277 RVA: 0x00032827 File Offset: 0x00030A27
			public virtual MultiValuedProperty<string> AdminAuditLogCmdlets
			{
				set
				{
					base.PowerSharpParameters["AdminAuditLogCmdlets"] = value;
				}
			}

			// Token: 0x17000039 RID: 57
			// (set) Token: 0x0600149E RID: 5278 RVA: 0x0003283A File Offset: 0x00030A3A
			public virtual MultiValuedProperty<string> AdminAuditLogParameters
			{
				set
				{
					base.PowerSharpParameters["AdminAuditLogParameters"] = value;
				}
			}

			// Token: 0x1700003A RID: 58
			// (set) Token: 0x0600149F RID: 5279 RVA: 0x0003284D File Offset: 0x00030A4D
			public virtual MultiValuedProperty<string> AdminAuditLogExcludedCmdlets
			{
				set
				{
					base.PowerSharpParameters["AdminAuditLogExcludedCmdlets"] = value;
				}
			}

			// Token: 0x1700003B RID: 59
			// (set) Token: 0x060014A0 RID: 5280 RVA: 0x00032860 File Offset: 0x00030A60
			public virtual EnhancedTimeSpan AdminAuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["AdminAuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x1700003C RID: 60
			// (set) Token: 0x060014A1 RID: 5281 RVA: 0x00032878 File Offset: 0x00030A78
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700003D RID: 61
			// (set) Token: 0x060014A2 RID: 5282 RVA: 0x0003288B File Offset: 0x00030A8B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700003E RID: 62
			// (set) Token: 0x060014A3 RID: 5283 RVA: 0x000328A3 File Offset: 0x00030AA3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700003F RID: 63
			// (set) Token: 0x060014A4 RID: 5284 RVA: 0x000328BB File Offset: 0x00030ABB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000040 RID: 64
			// (set) Token: 0x060014A5 RID: 5285 RVA: 0x000328D3 File Offset: 0x00030AD3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000041 RID: 65
			// (set) Token: 0x060014A6 RID: 5286 RVA: 0x000328EB File Offset: 0x00030AEB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200000F RID: 15
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000042 RID: 66
			// (set) Token: 0x060014A8 RID: 5288 RVA: 0x0003290B File Offset: 0x00030B0B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000043 RID: 67
			// (set) Token: 0x060014A9 RID: 5289 RVA: 0x00032929 File Offset: 0x00030B29
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17000044 RID: 68
			// (set) Token: 0x060014AA RID: 5290 RVA: 0x00032941 File Offset: 0x00030B41
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000045 RID: 69
			// (set) Token: 0x060014AB RID: 5291 RVA: 0x00032959 File Offset: 0x00030B59
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000046 RID: 70
			// (set) Token: 0x060014AC RID: 5292 RVA: 0x0003296C File Offset: 0x00030B6C
			public virtual bool AdminAuditLogEnabled
			{
				set
				{
					base.PowerSharpParameters["AdminAuditLogEnabled"] = value;
				}
			}

			// Token: 0x17000047 RID: 71
			// (set) Token: 0x060014AD RID: 5293 RVA: 0x00032984 File Offset: 0x00030B84
			public virtual AuditLogLevel LogLevel
			{
				set
				{
					base.PowerSharpParameters["LogLevel"] = value;
				}
			}

			// Token: 0x17000048 RID: 72
			// (set) Token: 0x060014AE RID: 5294 RVA: 0x0003299C File Offset: 0x00030B9C
			public virtual bool TestCmdletLoggingEnabled
			{
				set
				{
					base.PowerSharpParameters["TestCmdletLoggingEnabled"] = value;
				}
			}

			// Token: 0x17000049 RID: 73
			// (set) Token: 0x060014AF RID: 5295 RVA: 0x000329B4 File Offset: 0x00030BB4
			public virtual MultiValuedProperty<string> AdminAuditLogCmdlets
			{
				set
				{
					base.PowerSharpParameters["AdminAuditLogCmdlets"] = value;
				}
			}

			// Token: 0x1700004A RID: 74
			// (set) Token: 0x060014B0 RID: 5296 RVA: 0x000329C7 File Offset: 0x00030BC7
			public virtual MultiValuedProperty<string> AdminAuditLogParameters
			{
				set
				{
					base.PowerSharpParameters["AdminAuditLogParameters"] = value;
				}
			}

			// Token: 0x1700004B RID: 75
			// (set) Token: 0x060014B1 RID: 5297 RVA: 0x000329DA File Offset: 0x00030BDA
			public virtual MultiValuedProperty<string> AdminAuditLogExcludedCmdlets
			{
				set
				{
					base.PowerSharpParameters["AdminAuditLogExcludedCmdlets"] = value;
				}
			}

			// Token: 0x1700004C RID: 76
			// (set) Token: 0x060014B2 RID: 5298 RVA: 0x000329ED File Offset: 0x00030BED
			public virtual EnhancedTimeSpan AdminAuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["AdminAuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x1700004D RID: 77
			// (set) Token: 0x060014B3 RID: 5299 RVA: 0x00032A05 File Offset: 0x00030C05
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700004E RID: 78
			// (set) Token: 0x060014B4 RID: 5300 RVA: 0x00032A18 File Offset: 0x00030C18
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700004F RID: 79
			// (set) Token: 0x060014B5 RID: 5301 RVA: 0x00032A30 File Offset: 0x00030C30
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000050 RID: 80
			// (set) Token: 0x060014B6 RID: 5302 RVA: 0x00032A48 File Offset: 0x00030C48
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000051 RID: 81
			// (set) Token: 0x060014B7 RID: 5303 RVA: 0x00032A60 File Offset: 0x00030C60
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000052 RID: 82
			// (set) Token: 0x060014B8 RID: 5304 RVA: 0x00032A78 File Offset: 0x00030C78
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
