using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000550 RID: 1360
	public class NewDatabaseAvailabilityGroupCommand : SyntheticCommandWithPipelineInput<DatabaseAvailabilityGroup, DatabaseAvailabilityGroup>
	{
		// Token: 0x06004846 RID: 18502 RVA: 0x000752F2 File Offset: 0x000734F2
		private NewDatabaseAvailabilityGroupCommand() : base("New-DatabaseAvailabilityGroup")
		{
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x000752FF File Offset: 0x000734FF
		public NewDatabaseAvailabilityGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004848 RID: 18504 RVA: 0x0007530E File Offset: 0x0007350E
		public virtual NewDatabaseAvailabilityGroupCommand SetParameters(NewDatabaseAvailabilityGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000551 RID: 1361
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700295F RID: 10591
			// (set) Token: 0x06004849 RID: 18505 RVA: 0x00075318 File Offset: 0x00073518
			public virtual FileShareWitnessServerName WitnessServer
			{
				set
				{
					base.PowerSharpParameters["WitnessServer"] = value;
				}
			}

			// Token: 0x17002960 RID: 10592
			// (set) Token: 0x0600484A RID: 18506 RVA: 0x0007532B File Offset: 0x0007352B
			public virtual NonRootLocalLongFullPath WitnessDirectory
			{
				set
				{
					base.PowerSharpParameters["WitnessDirectory"] = value;
				}
			}

			// Token: 0x17002961 RID: 10593
			// (set) Token: 0x0600484B RID: 18507 RVA: 0x0007533E File Offset: 0x0007353E
			public virtual ThirdPartyReplicationMode ThirdPartyReplication
			{
				set
				{
					base.PowerSharpParameters["ThirdPartyReplication"] = value;
				}
			}

			// Token: 0x17002962 RID: 10594
			// (set) Token: 0x0600484C RID: 18508 RVA: 0x00075356 File Offset: 0x00073556
			public virtual IPAddress DatabaseAvailabilityGroupIpAddresses
			{
				set
				{
					base.PowerSharpParameters["DatabaseAvailabilityGroupIpAddresses"] = value;
				}
			}

			// Token: 0x17002963 RID: 10595
			// (set) Token: 0x0600484D RID: 18509 RVA: 0x00075369 File Offset: 0x00073569
			public virtual DatabaseAvailabilityGroupConfigurationIdParameter DagConfiguration
			{
				set
				{
					base.PowerSharpParameters["DagConfiguration"] = value;
				}
			}

			// Token: 0x17002964 RID: 10596
			// (set) Token: 0x0600484E RID: 18510 RVA: 0x0007537C File Offset: 0x0007357C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002965 RID: 10597
			// (set) Token: 0x0600484F RID: 18511 RVA: 0x0007538F File Offset: 0x0007358F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002966 RID: 10598
			// (set) Token: 0x06004850 RID: 18512 RVA: 0x000753A2 File Offset: 0x000735A2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002967 RID: 10599
			// (set) Token: 0x06004851 RID: 18513 RVA: 0x000753BA File Offset: 0x000735BA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002968 RID: 10600
			// (set) Token: 0x06004852 RID: 18514 RVA: 0x000753D2 File Offset: 0x000735D2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002969 RID: 10601
			// (set) Token: 0x06004853 RID: 18515 RVA: 0x000753EA File Offset: 0x000735EA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700296A RID: 10602
			// (set) Token: 0x06004854 RID: 18516 RVA: 0x00075402 File Offset: 0x00073602
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
