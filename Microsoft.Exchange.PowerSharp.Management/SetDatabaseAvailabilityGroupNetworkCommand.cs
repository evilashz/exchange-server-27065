using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000577 RID: 1399
	public class SetDatabaseAvailabilityGroupNetworkCommand : SyntheticCommandWithPipelineInputNoOutput<DatabaseAvailabilityGroupNetwork>
	{
		// Token: 0x060049B1 RID: 18865 RVA: 0x00076FA2 File Offset: 0x000751A2
		private SetDatabaseAvailabilityGroupNetworkCommand() : base("Set-DatabaseAvailabilityGroupNetwork")
		{
		}

		// Token: 0x060049B2 RID: 18866 RVA: 0x00076FAF File Offset: 0x000751AF
		public SetDatabaseAvailabilityGroupNetworkCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060049B3 RID: 18867 RVA: 0x00076FBE File Offset: 0x000751BE
		public virtual SetDatabaseAvailabilityGroupNetworkCommand SetParameters(SetDatabaseAvailabilityGroupNetworkCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060049B4 RID: 18868 RVA: 0x00076FC8 File Offset: 0x000751C8
		public virtual SetDatabaseAvailabilityGroupNetworkCommand SetParameters(SetDatabaseAvailabilityGroupNetworkCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000578 RID: 1400
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002A7C RID: 10876
			// (set) Token: 0x060049B5 RID: 18869 RVA: 0x00076FD2 File Offset: 0x000751D2
			public virtual DatabaseAvailabilityGroupSubnetId Subnets
			{
				set
				{
					base.PowerSharpParameters["Subnets"] = value;
				}
			}

			// Token: 0x17002A7D RID: 10877
			// (set) Token: 0x060049B6 RID: 18870 RVA: 0x00076FE5 File Offset: 0x000751E5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002A7E RID: 10878
			// (set) Token: 0x060049B7 RID: 18871 RVA: 0x00076FF8 File Offset: 0x000751F8
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002A7F RID: 10879
			// (set) Token: 0x060049B8 RID: 18872 RVA: 0x0007700B File Offset: 0x0007520B
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17002A80 RID: 10880
			// (set) Token: 0x060049B9 RID: 18873 RVA: 0x0007701E File Offset: 0x0007521E
			public virtual bool ReplicationEnabled
			{
				set
				{
					base.PowerSharpParameters["ReplicationEnabled"] = value;
				}
			}

			// Token: 0x17002A81 RID: 10881
			// (set) Token: 0x060049BA RID: 18874 RVA: 0x00077036 File Offset: 0x00075236
			public virtual bool IgnoreNetwork
			{
				set
				{
					base.PowerSharpParameters["IgnoreNetwork"] = value;
				}
			}

			// Token: 0x17002A82 RID: 10882
			// (set) Token: 0x060049BB RID: 18875 RVA: 0x0007704E File Offset: 0x0007524E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002A83 RID: 10883
			// (set) Token: 0x060049BC RID: 18876 RVA: 0x00077066 File Offset: 0x00075266
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002A84 RID: 10884
			// (set) Token: 0x060049BD RID: 18877 RVA: 0x0007707E File Offset: 0x0007527E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002A85 RID: 10885
			// (set) Token: 0x060049BE RID: 18878 RVA: 0x00077096 File Offset: 0x00075296
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002A86 RID: 10886
			// (set) Token: 0x060049BF RID: 18879 RVA: 0x000770AE File Offset: 0x000752AE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000579 RID: 1401
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002A87 RID: 10887
			// (set) Token: 0x060049C1 RID: 18881 RVA: 0x000770CE File Offset: 0x000752CE
			public virtual DatabaseAvailabilityGroupNetworkIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002A88 RID: 10888
			// (set) Token: 0x060049C2 RID: 18882 RVA: 0x000770E1 File Offset: 0x000752E1
			public virtual DatabaseAvailabilityGroupSubnetId Subnets
			{
				set
				{
					base.PowerSharpParameters["Subnets"] = value;
				}
			}

			// Token: 0x17002A89 RID: 10889
			// (set) Token: 0x060049C3 RID: 18883 RVA: 0x000770F4 File Offset: 0x000752F4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002A8A RID: 10890
			// (set) Token: 0x060049C4 RID: 18884 RVA: 0x00077107 File Offset: 0x00075307
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002A8B RID: 10891
			// (set) Token: 0x060049C5 RID: 18885 RVA: 0x0007711A File Offset: 0x0007531A
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17002A8C RID: 10892
			// (set) Token: 0x060049C6 RID: 18886 RVA: 0x0007712D File Offset: 0x0007532D
			public virtual bool ReplicationEnabled
			{
				set
				{
					base.PowerSharpParameters["ReplicationEnabled"] = value;
				}
			}

			// Token: 0x17002A8D RID: 10893
			// (set) Token: 0x060049C7 RID: 18887 RVA: 0x00077145 File Offset: 0x00075345
			public virtual bool IgnoreNetwork
			{
				set
				{
					base.PowerSharpParameters["IgnoreNetwork"] = value;
				}
			}

			// Token: 0x17002A8E RID: 10894
			// (set) Token: 0x060049C8 RID: 18888 RVA: 0x0007715D File Offset: 0x0007535D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002A8F RID: 10895
			// (set) Token: 0x060049C9 RID: 18889 RVA: 0x00077175 File Offset: 0x00075375
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002A90 RID: 10896
			// (set) Token: 0x060049CA RID: 18890 RVA: 0x0007718D File Offset: 0x0007538D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002A91 RID: 10897
			// (set) Token: 0x060049CB RID: 18891 RVA: 0x000771A5 File Offset: 0x000753A5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002A92 RID: 10898
			// (set) Token: 0x060049CC RID: 18892 RVA: 0x000771BD File Offset: 0x000753BD
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
