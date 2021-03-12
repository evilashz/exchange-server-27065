using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200056A RID: 1386
	public class RestoreDatabaseAvailabilityGroupCommand : SyntheticCommandWithPipelineInput<DatabaseAvailabilityGroup, DatabaseAvailabilityGroup>
	{
		// Token: 0x06004907 RID: 18695 RVA: 0x000761B3 File Offset: 0x000743B3
		private RestoreDatabaseAvailabilityGroupCommand() : base("Restore-DatabaseAvailabilityGroup")
		{
		}

		// Token: 0x06004908 RID: 18696 RVA: 0x000761C0 File Offset: 0x000743C0
		public RestoreDatabaseAvailabilityGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004909 RID: 18697 RVA: 0x000761CF File Offset: 0x000743CF
		public virtual RestoreDatabaseAvailabilityGroupCommand SetParameters(RestoreDatabaseAvailabilityGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600490A RID: 18698 RVA: 0x000761D9 File Offset: 0x000743D9
		public virtual RestoreDatabaseAvailabilityGroupCommand SetParameters(RestoreDatabaseAvailabilityGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200056B RID: 1387
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170029EC RID: 10732
			// (set) Token: 0x0600490B RID: 18699 RVA: 0x000761E3 File Offset: 0x000743E3
			public virtual string ActiveDirectorySite
			{
				set
				{
					base.PowerSharpParameters["ActiveDirectorySite"] = ((value != null) ? new AdSiteIdParameter(value) : null);
				}
			}

			// Token: 0x170029ED RID: 10733
			// (set) Token: 0x0600490C RID: 18700 RVA: 0x00076201 File Offset: 0x00074401
			public virtual SwitchParameter UsePrimaryWitnessServer
			{
				set
				{
					base.PowerSharpParameters["UsePrimaryWitnessServer"] = value;
				}
			}

			// Token: 0x170029EE RID: 10734
			// (set) Token: 0x0600490D RID: 18701 RVA: 0x00076219 File Offset: 0x00074419
			public virtual FileShareWitnessServerName AlternateWitnessServer
			{
				set
				{
					base.PowerSharpParameters["AlternateWitnessServer"] = value;
				}
			}

			// Token: 0x170029EF RID: 10735
			// (set) Token: 0x0600490E RID: 18702 RVA: 0x0007622C File Offset: 0x0007442C
			public virtual NonRootLocalLongFullPath AlternateWitnessDirectory
			{
				set
				{
					base.PowerSharpParameters["AlternateWitnessDirectory"] = value;
				}
			}

			// Token: 0x170029F0 RID: 10736
			// (set) Token: 0x0600490F RID: 18703 RVA: 0x0007623F File Offset: 0x0007443F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170029F1 RID: 10737
			// (set) Token: 0x06004910 RID: 18704 RVA: 0x00076252 File Offset: 0x00074452
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170029F2 RID: 10738
			// (set) Token: 0x06004911 RID: 18705 RVA: 0x0007626A File Offset: 0x0007446A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170029F3 RID: 10739
			// (set) Token: 0x06004912 RID: 18706 RVA: 0x00076282 File Offset: 0x00074482
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170029F4 RID: 10740
			// (set) Token: 0x06004913 RID: 18707 RVA: 0x0007629A File Offset: 0x0007449A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170029F5 RID: 10741
			// (set) Token: 0x06004914 RID: 18708 RVA: 0x000762B2 File Offset: 0x000744B2
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170029F6 RID: 10742
			// (set) Token: 0x06004915 RID: 18709 RVA: 0x000762CA File Offset: 0x000744CA
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200056C RID: 1388
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170029F7 RID: 10743
			// (set) Token: 0x06004917 RID: 18711 RVA: 0x000762EA File Offset: 0x000744EA
			public virtual DatabaseAvailabilityGroupIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170029F8 RID: 10744
			// (set) Token: 0x06004918 RID: 18712 RVA: 0x000762FD File Offset: 0x000744FD
			public virtual string ActiveDirectorySite
			{
				set
				{
					base.PowerSharpParameters["ActiveDirectorySite"] = ((value != null) ? new AdSiteIdParameter(value) : null);
				}
			}

			// Token: 0x170029F9 RID: 10745
			// (set) Token: 0x06004919 RID: 18713 RVA: 0x0007631B File Offset: 0x0007451B
			public virtual SwitchParameter UsePrimaryWitnessServer
			{
				set
				{
					base.PowerSharpParameters["UsePrimaryWitnessServer"] = value;
				}
			}

			// Token: 0x170029FA RID: 10746
			// (set) Token: 0x0600491A RID: 18714 RVA: 0x00076333 File Offset: 0x00074533
			public virtual FileShareWitnessServerName AlternateWitnessServer
			{
				set
				{
					base.PowerSharpParameters["AlternateWitnessServer"] = value;
				}
			}

			// Token: 0x170029FB RID: 10747
			// (set) Token: 0x0600491B RID: 18715 RVA: 0x00076346 File Offset: 0x00074546
			public virtual NonRootLocalLongFullPath AlternateWitnessDirectory
			{
				set
				{
					base.PowerSharpParameters["AlternateWitnessDirectory"] = value;
				}
			}

			// Token: 0x170029FC RID: 10748
			// (set) Token: 0x0600491C RID: 18716 RVA: 0x00076359 File Offset: 0x00074559
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170029FD RID: 10749
			// (set) Token: 0x0600491D RID: 18717 RVA: 0x0007636C File Offset: 0x0007456C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170029FE RID: 10750
			// (set) Token: 0x0600491E RID: 18718 RVA: 0x00076384 File Offset: 0x00074584
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170029FF RID: 10751
			// (set) Token: 0x0600491F RID: 18719 RVA: 0x0007639C File Offset: 0x0007459C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002A00 RID: 10752
			// (set) Token: 0x06004920 RID: 18720 RVA: 0x000763B4 File Offset: 0x000745B4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002A01 RID: 10753
			// (set) Token: 0x06004921 RID: 18721 RVA: 0x000763CC File Offset: 0x000745CC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002A02 RID: 10754
			// (set) Token: 0x06004922 RID: 18722 RVA: 0x000763E4 File Offset: 0x000745E4
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
