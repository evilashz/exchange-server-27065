using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000125 RID: 293
	public class GetMsoTenantSyncRequestCommand : SyntheticCommandWithPipelineInput<MsoTenantCookieContainer, MsoTenantCookieContainer>
	{
		// Token: 0x06001FC2 RID: 8130 RVA: 0x00040E11 File Offset: 0x0003F011
		private GetMsoTenantSyncRequestCommand() : base("Get-MsoTenantSyncRequest")
		{
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x00040E1E File Offset: 0x0003F01E
		public GetMsoTenantSyncRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x00040E2D File Offset: 0x0003F02D
		public virtual GetMsoTenantSyncRequestCommand SetParameters(GetMsoTenantSyncRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x00040E37 File Offset: 0x0003F037
		public virtual GetMsoTenantSyncRequestCommand SetParameters(GetMsoTenantSyncRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000126 RID: 294
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000931 RID: 2353
			// (set) Token: 0x06001FC6 RID: 8134 RVA: 0x00040E41 File Offset: 0x0003F041
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000932 RID: 2354
			// (set) Token: 0x06001FC7 RID: 8135 RVA: 0x00040E54 File Offset: 0x0003F054
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000933 RID: 2355
			// (set) Token: 0x06001FC8 RID: 8136 RVA: 0x00040E6C File Offset: 0x0003F06C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000934 RID: 2356
			// (set) Token: 0x06001FC9 RID: 8137 RVA: 0x00040E84 File Offset: 0x0003F084
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000935 RID: 2357
			// (set) Token: 0x06001FCA RID: 8138 RVA: 0x00040E9C File Offset: 0x0003F09C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000127 RID: 295
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000936 RID: 2358
			// (set) Token: 0x06001FCC RID: 8140 RVA: 0x00040EBC File Offset: 0x0003F0BC
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000937 RID: 2359
			// (set) Token: 0x06001FCD RID: 8141 RVA: 0x00040EDA File Offset: 0x0003F0DA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000938 RID: 2360
			// (set) Token: 0x06001FCE RID: 8142 RVA: 0x00040EED File Offset: 0x0003F0ED
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000939 RID: 2361
			// (set) Token: 0x06001FCF RID: 8143 RVA: 0x00040F05 File Offset: 0x0003F105
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700093A RID: 2362
			// (set) Token: 0x06001FD0 RID: 8144 RVA: 0x00040F1D File Offset: 0x0003F11D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700093B RID: 2363
			// (set) Token: 0x06001FD1 RID: 8145 RVA: 0x00040F35 File Offset: 0x0003F135
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
