using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ForwardSyncTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200012E RID: 302
	public class NewMsoTenantSyncRequestCommand : SyntheticCommandWithPipelineInput<MsoTenantSyncRequest, MsoTenantSyncRequest>
	{
		// Token: 0x06001FF9 RID: 8185 RVA: 0x00041236 File Offset: 0x0003F436
		private NewMsoTenantSyncRequestCommand() : base("New-MsoTenantSyncRequest")
		{
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x00041243 File Offset: 0x0003F443
		public NewMsoTenantSyncRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x00041252 File Offset: 0x0003F452
		public virtual NewMsoTenantSyncRequestCommand SetParameters(NewMsoTenantSyncRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200012F RID: 303
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000956 RID: 2390
			// (set) Token: 0x06001FFC RID: 8188 RVA: 0x0004125C File Offset: 0x0003F45C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000957 RID: 2391
			// (set) Token: 0x06001FFD RID: 8189 RVA: 0x0004127A File Offset: 0x0003F47A
			public virtual SwitchParameter Full
			{
				set
				{
					base.PowerSharpParameters["Full"] = value;
				}
			}

			// Token: 0x17000958 RID: 2392
			// (set) Token: 0x06001FFE RID: 8190 RVA: 0x00041292 File Offset: 0x0003F492
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000959 RID: 2393
			// (set) Token: 0x06001FFF RID: 8191 RVA: 0x000412AA File Offset: 0x0003F4AA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700095A RID: 2394
			// (set) Token: 0x06002000 RID: 8192 RVA: 0x000412BD File Offset: 0x0003F4BD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700095B RID: 2395
			// (set) Token: 0x06002001 RID: 8193 RVA: 0x000412D5 File Offset: 0x0003F4D5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700095C RID: 2396
			// (set) Token: 0x06002002 RID: 8194 RVA: 0x000412ED File Offset: 0x0003F4ED
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700095D RID: 2397
			// (set) Token: 0x06002003 RID: 8195 RVA: 0x00041305 File Offset: 0x0003F505
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700095E RID: 2398
			// (set) Token: 0x06002004 RID: 8196 RVA: 0x0004131D File Offset: 0x0003F51D
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
