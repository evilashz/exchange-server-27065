using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000535 RID: 1333
	public class GetDatabaseAvailabilityGroupCommand : SyntheticCommandWithPipelineInput<DatabaseAvailabilityGroup, DatabaseAvailabilityGroup>
	{
		// Token: 0x06004767 RID: 18279 RVA: 0x000741B6 File Offset: 0x000723B6
		private GetDatabaseAvailabilityGroupCommand() : base("Get-DatabaseAvailabilityGroup")
		{
		}

		// Token: 0x06004768 RID: 18280 RVA: 0x000741C3 File Offset: 0x000723C3
		public GetDatabaseAvailabilityGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004769 RID: 18281 RVA: 0x000741D2 File Offset: 0x000723D2
		public virtual GetDatabaseAvailabilityGroupCommand SetParameters(GetDatabaseAvailabilityGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x000741DC File Offset: 0x000723DC
		public virtual GetDatabaseAvailabilityGroupCommand SetParameters(GetDatabaseAvailabilityGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000536 RID: 1334
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170028B6 RID: 10422
			// (set) Token: 0x0600476B RID: 18283 RVA: 0x000741E6 File Offset: 0x000723E6
			public virtual SwitchParameter Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x170028B7 RID: 10423
			// (set) Token: 0x0600476C RID: 18284 RVA: 0x000741FE File Offset: 0x000723FE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170028B8 RID: 10424
			// (set) Token: 0x0600476D RID: 18285 RVA: 0x00074211 File Offset: 0x00072411
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170028B9 RID: 10425
			// (set) Token: 0x0600476E RID: 18286 RVA: 0x00074229 File Offset: 0x00072429
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170028BA RID: 10426
			// (set) Token: 0x0600476F RID: 18287 RVA: 0x00074241 File Offset: 0x00072441
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170028BB RID: 10427
			// (set) Token: 0x06004770 RID: 18288 RVA: 0x00074259 File Offset: 0x00072459
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000537 RID: 1335
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170028BC RID: 10428
			// (set) Token: 0x06004772 RID: 18290 RVA: 0x00074279 File Offset: 0x00072479
			public virtual DatabaseAvailabilityGroupIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170028BD RID: 10429
			// (set) Token: 0x06004773 RID: 18291 RVA: 0x0007428C File Offset: 0x0007248C
			public virtual SwitchParameter Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x170028BE RID: 10430
			// (set) Token: 0x06004774 RID: 18292 RVA: 0x000742A4 File Offset: 0x000724A4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170028BF RID: 10431
			// (set) Token: 0x06004775 RID: 18293 RVA: 0x000742B7 File Offset: 0x000724B7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170028C0 RID: 10432
			// (set) Token: 0x06004776 RID: 18294 RVA: 0x000742CF File Offset: 0x000724CF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170028C1 RID: 10433
			// (set) Token: 0x06004777 RID: 18295 RVA: 0x000742E7 File Offset: 0x000724E7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170028C2 RID: 10434
			// (set) Token: 0x06004778 RID: 18296 RVA: 0x000742FF File Offset: 0x000724FF
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
