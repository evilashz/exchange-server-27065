using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000552 RID: 1362
	public class NewDatabaseAvailabilityGroupConfigurationCommand : SyntheticCommandWithPipelineInput<DatabaseAvailabilityGroupConfiguration, DatabaseAvailabilityGroupConfiguration>
	{
		// Token: 0x06004856 RID: 18518 RVA: 0x00075422 File Offset: 0x00073622
		private NewDatabaseAvailabilityGroupConfigurationCommand() : base("New-DatabaseAvailabilityGroupConfiguration")
		{
		}

		// Token: 0x06004857 RID: 18519 RVA: 0x0007542F File Offset: 0x0007362F
		public NewDatabaseAvailabilityGroupConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004858 RID: 18520 RVA: 0x0007543E File Offset: 0x0007363E
		public virtual NewDatabaseAvailabilityGroupConfigurationCommand SetParameters(NewDatabaseAvailabilityGroupConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000553 RID: 1363
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700296B RID: 10603
			// (set) Token: 0x06004859 RID: 18521 RVA: 0x00075448 File Offset: 0x00073648
			public virtual int ServersPerDag
			{
				set
				{
					base.PowerSharpParameters["ServersPerDag"] = value;
				}
			}

			// Token: 0x1700296C RID: 10604
			// (set) Token: 0x0600485A RID: 18522 RVA: 0x00075460 File Offset: 0x00073660
			public virtual int DatabasesPerServer
			{
				set
				{
					base.PowerSharpParameters["DatabasesPerServer"] = value;
				}
			}

			// Token: 0x1700296D RID: 10605
			// (set) Token: 0x0600485B RID: 18523 RVA: 0x00075478 File Offset: 0x00073678
			public virtual int DatabasesPerVolume
			{
				set
				{
					base.PowerSharpParameters["DatabasesPerVolume"] = value;
				}
			}

			// Token: 0x1700296E RID: 10606
			// (set) Token: 0x0600485C RID: 18524 RVA: 0x00075490 File Offset: 0x00073690
			public virtual int CopiesPerDatabase
			{
				set
				{
					base.PowerSharpParameters["CopiesPerDatabase"] = value;
				}
			}

			// Token: 0x1700296F RID: 10607
			// (set) Token: 0x0600485D RID: 18525 RVA: 0x000754A8 File Offset: 0x000736A8
			public virtual int MinCopiesPerDatabaseForMonitoring
			{
				set
				{
					base.PowerSharpParameters["MinCopiesPerDatabaseForMonitoring"] = value;
				}
			}

			// Token: 0x17002970 RID: 10608
			// (set) Token: 0x0600485E RID: 18526 RVA: 0x000754C0 File Offset: 0x000736C0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002971 RID: 10609
			// (set) Token: 0x0600485F RID: 18527 RVA: 0x000754D3 File Offset: 0x000736D3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002972 RID: 10610
			// (set) Token: 0x06004860 RID: 18528 RVA: 0x000754E6 File Offset: 0x000736E6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002973 RID: 10611
			// (set) Token: 0x06004861 RID: 18529 RVA: 0x000754FE File Offset: 0x000736FE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002974 RID: 10612
			// (set) Token: 0x06004862 RID: 18530 RVA: 0x00075516 File Offset: 0x00073716
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002975 RID: 10613
			// (set) Token: 0x06004863 RID: 18531 RVA: 0x0007552E File Offset: 0x0007372E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002976 RID: 10614
			// (set) Token: 0x06004864 RID: 18532 RVA: 0x00075546 File Offset: 0x00073746
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
