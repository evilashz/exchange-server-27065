using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000372 RID: 882
	public class SetManagementScopeCommand : SyntheticCommandWithPipelineInputNoOutput<ManagementScope>
	{
		// Token: 0x060037ED RID: 14317 RVA: 0x000606C7 File Offset: 0x0005E8C7
		private SetManagementScopeCommand() : base("Set-ManagementScope")
		{
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x000606D4 File Offset: 0x0005E8D4
		public SetManagementScopeCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x000606E3 File Offset: 0x0005E8E3
		public virtual SetManagementScopeCommand SetParameters(SetManagementScopeCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x000606ED File Offset: 0x0005E8ED
		public virtual SetManagementScopeCommand SetParameters(SetManagementScopeCommand.RecipientFilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x000606F7 File Offset: 0x0005E8F7
		public virtual SetManagementScopeCommand SetParameters(SetManagementScopeCommand.ServerFilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x00060701 File Offset: 0x0005E901
		public virtual SetManagementScopeCommand SetParameters(SetManagementScopeCommand.DatabaseFilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x0006070B File Offset: 0x0005E90B
		public virtual SetManagementScopeCommand SetParameters(SetManagementScopeCommand.PartnerFilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000373 RID: 883
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001CC2 RID: 7362
			// (set) Token: 0x060037F4 RID: 14324 RVA: 0x00060715 File Offset: 0x0005E915
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001CC3 RID: 7363
			// (set) Token: 0x060037F5 RID: 14325 RVA: 0x0006072D File Offset: 0x0005E92D
			public virtual ManagementScopeIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17001CC4 RID: 7364
			// (set) Token: 0x060037F6 RID: 14326 RVA: 0x00060740 File Offset: 0x0005E940
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001CC5 RID: 7365
			// (set) Token: 0x060037F7 RID: 14327 RVA: 0x00060753 File Offset: 0x0005E953
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001CC6 RID: 7366
			// (set) Token: 0x060037F8 RID: 14328 RVA: 0x00060766 File Offset: 0x0005E966
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001CC7 RID: 7367
			// (set) Token: 0x060037F9 RID: 14329 RVA: 0x0006077E File Offset: 0x0005E97E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001CC8 RID: 7368
			// (set) Token: 0x060037FA RID: 14330 RVA: 0x00060796 File Offset: 0x0005E996
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001CC9 RID: 7369
			// (set) Token: 0x060037FB RID: 14331 RVA: 0x000607AE File Offset: 0x0005E9AE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001CCA RID: 7370
			// (set) Token: 0x060037FC RID: 14332 RVA: 0x000607C6 File Offset: 0x0005E9C6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000374 RID: 884
		public class RecipientFilterParameters : ParametersBase
		{
			// Token: 0x17001CCB RID: 7371
			// (set) Token: 0x060037FE RID: 14334 RVA: 0x000607E6 File Offset: 0x0005E9E6
			public virtual string RecipientRestrictionFilter
			{
				set
				{
					base.PowerSharpParameters["RecipientRestrictionFilter"] = value;
				}
			}

			// Token: 0x17001CCC RID: 7372
			// (set) Token: 0x060037FF RID: 14335 RVA: 0x000607F9 File Offset: 0x0005E9F9
			public virtual string RecipientRoot
			{
				set
				{
					base.PowerSharpParameters["RecipientRoot"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17001CCD RID: 7373
			// (set) Token: 0x06003800 RID: 14336 RVA: 0x00060817 File Offset: 0x0005EA17
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001CCE RID: 7374
			// (set) Token: 0x06003801 RID: 14337 RVA: 0x0006082F File Offset: 0x0005EA2F
			public virtual ManagementScopeIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17001CCF RID: 7375
			// (set) Token: 0x06003802 RID: 14338 RVA: 0x00060842 File Offset: 0x0005EA42
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001CD0 RID: 7376
			// (set) Token: 0x06003803 RID: 14339 RVA: 0x00060855 File Offset: 0x0005EA55
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001CD1 RID: 7377
			// (set) Token: 0x06003804 RID: 14340 RVA: 0x00060868 File Offset: 0x0005EA68
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001CD2 RID: 7378
			// (set) Token: 0x06003805 RID: 14341 RVA: 0x00060880 File Offset: 0x0005EA80
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001CD3 RID: 7379
			// (set) Token: 0x06003806 RID: 14342 RVA: 0x00060898 File Offset: 0x0005EA98
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001CD4 RID: 7380
			// (set) Token: 0x06003807 RID: 14343 RVA: 0x000608B0 File Offset: 0x0005EAB0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001CD5 RID: 7381
			// (set) Token: 0x06003808 RID: 14344 RVA: 0x000608C8 File Offset: 0x0005EAC8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000375 RID: 885
		public class ServerFilterParameters : ParametersBase
		{
			// Token: 0x17001CD6 RID: 7382
			// (set) Token: 0x0600380A RID: 14346 RVA: 0x000608E8 File Offset: 0x0005EAE8
			public virtual string ServerRestrictionFilter
			{
				set
				{
					base.PowerSharpParameters["ServerRestrictionFilter"] = value;
				}
			}

			// Token: 0x17001CD7 RID: 7383
			// (set) Token: 0x0600380B RID: 14347 RVA: 0x000608FB File Offset: 0x0005EAFB
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001CD8 RID: 7384
			// (set) Token: 0x0600380C RID: 14348 RVA: 0x00060913 File Offset: 0x0005EB13
			public virtual ManagementScopeIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17001CD9 RID: 7385
			// (set) Token: 0x0600380D RID: 14349 RVA: 0x00060926 File Offset: 0x0005EB26
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001CDA RID: 7386
			// (set) Token: 0x0600380E RID: 14350 RVA: 0x00060939 File Offset: 0x0005EB39
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001CDB RID: 7387
			// (set) Token: 0x0600380F RID: 14351 RVA: 0x0006094C File Offset: 0x0005EB4C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001CDC RID: 7388
			// (set) Token: 0x06003810 RID: 14352 RVA: 0x00060964 File Offset: 0x0005EB64
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001CDD RID: 7389
			// (set) Token: 0x06003811 RID: 14353 RVA: 0x0006097C File Offset: 0x0005EB7C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001CDE RID: 7390
			// (set) Token: 0x06003812 RID: 14354 RVA: 0x00060994 File Offset: 0x0005EB94
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001CDF RID: 7391
			// (set) Token: 0x06003813 RID: 14355 RVA: 0x000609AC File Offset: 0x0005EBAC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000376 RID: 886
		public class DatabaseFilterParameters : ParametersBase
		{
			// Token: 0x17001CE0 RID: 7392
			// (set) Token: 0x06003815 RID: 14357 RVA: 0x000609CC File Offset: 0x0005EBCC
			public virtual string DatabaseRestrictionFilter
			{
				set
				{
					base.PowerSharpParameters["DatabaseRestrictionFilter"] = value;
				}
			}

			// Token: 0x17001CE1 RID: 7393
			// (set) Token: 0x06003816 RID: 14358 RVA: 0x000609DF File Offset: 0x0005EBDF
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001CE2 RID: 7394
			// (set) Token: 0x06003817 RID: 14359 RVA: 0x000609F7 File Offset: 0x0005EBF7
			public virtual ManagementScopeIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17001CE3 RID: 7395
			// (set) Token: 0x06003818 RID: 14360 RVA: 0x00060A0A File Offset: 0x0005EC0A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001CE4 RID: 7396
			// (set) Token: 0x06003819 RID: 14361 RVA: 0x00060A1D File Offset: 0x0005EC1D
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001CE5 RID: 7397
			// (set) Token: 0x0600381A RID: 14362 RVA: 0x00060A30 File Offset: 0x0005EC30
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001CE6 RID: 7398
			// (set) Token: 0x0600381B RID: 14363 RVA: 0x00060A48 File Offset: 0x0005EC48
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001CE7 RID: 7399
			// (set) Token: 0x0600381C RID: 14364 RVA: 0x00060A60 File Offset: 0x0005EC60
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001CE8 RID: 7400
			// (set) Token: 0x0600381D RID: 14365 RVA: 0x00060A78 File Offset: 0x0005EC78
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001CE9 RID: 7401
			// (set) Token: 0x0600381E RID: 14366 RVA: 0x00060A90 File Offset: 0x0005EC90
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000377 RID: 887
		public class PartnerFilterParameters : ParametersBase
		{
			// Token: 0x17001CEA RID: 7402
			// (set) Token: 0x06003820 RID: 14368 RVA: 0x00060AB0 File Offset: 0x0005ECB0
			public virtual string PartnerDelegatedTenantRestrictionFilter
			{
				set
				{
					base.PowerSharpParameters["PartnerDelegatedTenantRestrictionFilter"] = value;
				}
			}

			// Token: 0x17001CEB RID: 7403
			// (set) Token: 0x06003821 RID: 14369 RVA: 0x00060AC3 File Offset: 0x0005ECC3
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001CEC RID: 7404
			// (set) Token: 0x06003822 RID: 14370 RVA: 0x00060ADB File Offset: 0x0005ECDB
			public virtual ManagementScopeIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17001CED RID: 7405
			// (set) Token: 0x06003823 RID: 14371 RVA: 0x00060AEE File Offset: 0x0005ECEE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001CEE RID: 7406
			// (set) Token: 0x06003824 RID: 14372 RVA: 0x00060B01 File Offset: 0x0005ED01
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001CEF RID: 7407
			// (set) Token: 0x06003825 RID: 14373 RVA: 0x00060B14 File Offset: 0x0005ED14
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001CF0 RID: 7408
			// (set) Token: 0x06003826 RID: 14374 RVA: 0x00060B2C File Offset: 0x0005ED2C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001CF1 RID: 7409
			// (set) Token: 0x06003827 RID: 14375 RVA: 0x00060B44 File Offset: 0x0005ED44
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001CF2 RID: 7410
			// (set) Token: 0x06003828 RID: 14376 RVA: 0x00060B5C File Offset: 0x0005ED5C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001CF3 RID: 7411
			// (set) Token: 0x06003829 RID: 14377 RVA: 0x00060B74 File Offset: 0x0005ED74
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
