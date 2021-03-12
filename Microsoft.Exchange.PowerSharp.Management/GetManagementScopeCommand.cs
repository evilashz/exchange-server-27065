using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200036F RID: 879
	public class GetManagementScopeCommand : SyntheticCommandWithPipelineInput<ManagementScope, ManagementScope>
	{
		// Token: 0x060037D6 RID: 14294 RVA: 0x000604F2 File Offset: 0x0005E6F2
		private GetManagementScopeCommand() : base("Get-ManagementScope")
		{
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x000604FF File Offset: 0x0005E6FF
		public GetManagementScopeCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x0006050E File Offset: 0x0005E70E
		public virtual GetManagementScopeCommand SetParameters(GetManagementScopeCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x00060518 File Offset: 0x0005E718
		public virtual GetManagementScopeCommand SetParameters(GetManagementScopeCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000370 RID: 880
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001CB1 RID: 7345
			// (set) Token: 0x060037DA RID: 14298 RVA: 0x00060522 File Offset: 0x0005E722
			public virtual SwitchParameter Orphan
			{
				set
				{
					base.PowerSharpParameters["Orphan"] = value;
				}
			}

			// Token: 0x17001CB2 RID: 7346
			// (set) Token: 0x060037DB RID: 14299 RVA: 0x0006053A File Offset: 0x0005E73A
			public virtual bool Exclusive
			{
				set
				{
					base.PowerSharpParameters["Exclusive"] = value;
				}
			}

			// Token: 0x17001CB3 RID: 7347
			// (set) Token: 0x060037DC RID: 14300 RVA: 0x00060552 File Offset: 0x0005E752
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001CB4 RID: 7348
			// (set) Token: 0x060037DD RID: 14301 RVA: 0x00060570 File Offset: 0x0005E770
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001CB5 RID: 7349
			// (set) Token: 0x060037DE RID: 14302 RVA: 0x00060583 File Offset: 0x0005E783
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001CB6 RID: 7350
			// (set) Token: 0x060037DF RID: 14303 RVA: 0x0006059B File Offset: 0x0005E79B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001CB7 RID: 7351
			// (set) Token: 0x060037E0 RID: 14304 RVA: 0x000605B3 File Offset: 0x0005E7B3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001CB8 RID: 7352
			// (set) Token: 0x060037E1 RID: 14305 RVA: 0x000605CB File Offset: 0x0005E7CB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000371 RID: 881
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001CB9 RID: 7353
			// (set) Token: 0x060037E3 RID: 14307 RVA: 0x000605EB File Offset: 0x0005E7EB
			public virtual ManagementScopeIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17001CBA RID: 7354
			// (set) Token: 0x060037E4 RID: 14308 RVA: 0x000605FE File Offset: 0x0005E7FE
			public virtual SwitchParameter Orphan
			{
				set
				{
					base.PowerSharpParameters["Orphan"] = value;
				}
			}

			// Token: 0x17001CBB RID: 7355
			// (set) Token: 0x060037E5 RID: 14309 RVA: 0x00060616 File Offset: 0x0005E816
			public virtual bool Exclusive
			{
				set
				{
					base.PowerSharpParameters["Exclusive"] = value;
				}
			}

			// Token: 0x17001CBC RID: 7356
			// (set) Token: 0x060037E6 RID: 14310 RVA: 0x0006062E File Offset: 0x0005E82E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001CBD RID: 7357
			// (set) Token: 0x060037E7 RID: 14311 RVA: 0x0006064C File Offset: 0x0005E84C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001CBE RID: 7358
			// (set) Token: 0x060037E8 RID: 14312 RVA: 0x0006065F File Offset: 0x0005E85F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001CBF RID: 7359
			// (set) Token: 0x060037E9 RID: 14313 RVA: 0x00060677 File Offset: 0x0005E877
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001CC0 RID: 7360
			// (set) Token: 0x060037EA RID: 14314 RVA: 0x0006068F File Offset: 0x0005E88F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001CC1 RID: 7361
			// (set) Token: 0x060037EB RID: 14315 RVA: 0x000606A7 File Offset: 0x0005E8A7
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
