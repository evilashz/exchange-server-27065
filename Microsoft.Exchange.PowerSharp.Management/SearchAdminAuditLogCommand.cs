using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200000A RID: 10
	public class SearchAdminAuditLogCommand : SyntheticCommandWithPipelineInput<AdminAuditLogConfig, AdminAuditLogConfig>
	{
		// Token: 0x06001478 RID: 5240 RVA: 0x0003254C File Offset: 0x0003074C
		private SearchAdminAuditLogCommand() : base("Search-AdminAuditLog")
		{
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x00032559 File Offset: 0x00030759
		public SearchAdminAuditLogCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x00032568 File Offset: 0x00030768
		public virtual SearchAdminAuditLogCommand SetParameters(SearchAdminAuditLogCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00032572 File Offset: 0x00030772
		public virtual SearchAdminAuditLogCommand SetParameters(SearchAdminAuditLogCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200000B RID: 11
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700001D RID: 29
			// (set) Token: 0x0600147C RID: 5244 RVA: 0x0003257C File Offset: 0x0003077C
			public virtual MultiValuedProperty<string> Cmdlets
			{
				set
				{
					base.PowerSharpParameters["Cmdlets"] = value;
				}
			}

			// Token: 0x1700001E RID: 30
			// (set) Token: 0x0600147D RID: 5245 RVA: 0x0003258F File Offset: 0x0003078F
			public virtual MultiValuedProperty<string> Parameters
			{
				set
				{
					base.PowerSharpParameters["Parameters"] = value;
				}
			}

			// Token: 0x1700001F RID: 31
			// (set) Token: 0x0600147E RID: 5246 RVA: 0x000325A2 File Offset: 0x000307A2
			public virtual ExDateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17000020 RID: 32
			// (set) Token: 0x0600147F RID: 5247 RVA: 0x000325BA File Offset: 0x000307BA
			public virtual ExDateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17000021 RID: 33
			// (set) Token: 0x06001480 RID: 5248 RVA: 0x000325D2 File Offset: 0x000307D2
			public virtual bool? ExternalAccess
			{
				set
				{
					base.PowerSharpParameters["ExternalAccess"] = value;
				}
			}

			// Token: 0x17000022 RID: 34
			// (set) Token: 0x06001481 RID: 5249 RVA: 0x000325EA File Offset: 0x000307EA
			public virtual int ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17000023 RID: 35
			// (set) Token: 0x06001482 RID: 5250 RVA: 0x00032602 File Offset: 0x00030802
			public virtual MultiValuedProperty<string> ObjectIds
			{
				set
				{
					base.PowerSharpParameters["ObjectIds"] = value;
				}
			}

			// Token: 0x17000024 RID: 36
			// (set) Token: 0x06001483 RID: 5251 RVA: 0x00032615 File Offset: 0x00030815
			public virtual MultiValuedProperty<SecurityPrincipalIdParameter> UserIds
			{
				set
				{
					base.PowerSharpParameters["UserIds"] = value;
				}
			}

			// Token: 0x17000025 RID: 37
			// (set) Token: 0x06001484 RID: 5252 RVA: 0x00032628 File Offset: 0x00030828
			public virtual bool? IsSuccess
			{
				set
				{
					base.PowerSharpParameters["IsSuccess"] = value;
				}
			}

			// Token: 0x17000026 RID: 38
			// (set) Token: 0x06001485 RID: 5253 RVA: 0x00032640 File Offset: 0x00030840
			public virtual int StartIndex
			{
				set
				{
					base.PowerSharpParameters["StartIndex"] = value;
				}
			}

			// Token: 0x17000027 RID: 39
			// (set) Token: 0x06001486 RID: 5254 RVA: 0x00032658 File Offset: 0x00030858
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000028 RID: 40
			// (set) Token: 0x06001487 RID: 5255 RVA: 0x00032676 File Offset: 0x00030876
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000029 RID: 41
			// (set) Token: 0x06001488 RID: 5256 RVA: 0x00032689 File Offset: 0x00030889
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700002A RID: 42
			// (set) Token: 0x06001489 RID: 5257 RVA: 0x000326A1 File Offset: 0x000308A1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700002B RID: 43
			// (set) Token: 0x0600148A RID: 5258 RVA: 0x000326B9 File Offset: 0x000308B9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700002C RID: 44
			// (set) Token: 0x0600148B RID: 5259 RVA: 0x000326D1 File Offset: 0x000308D1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200000C RID: 12
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700002D RID: 45
			// (set) Token: 0x0600148D RID: 5261 RVA: 0x000326F1 File Offset: 0x000308F1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700002E RID: 46
			// (set) Token: 0x0600148E RID: 5262 RVA: 0x00032704 File Offset: 0x00030904
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700002F RID: 47
			// (set) Token: 0x0600148F RID: 5263 RVA: 0x0003271C File Offset: 0x0003091C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000030 RID: 48
			// (set) Token: 0x06001490 RID: 5264 RVA: 0x00032734 File Offset: 0x00030934
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000031 RID: 49
			// (set) Token: 0x06001491 RID: 5265 RVA: 0x0003274C File Offset: 0x0003094C
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
