using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000574 RID: 1396
	public class SetDatabaseAvailabilityGroupConfigurationCommand : SyntheticCommandWithPipelineInputNoOutput<DatabaseAvailabilityGroupConfiguration>
	{
		// Token: 0x06004994 RID: 18836 RVA: 0x00076D49 File Offset: 0x00074F49
		private SetDatabaseAvailabilityGroupConfigurationCommand() : base("Set-DatabaseAvailabilityGroupConfiguration")
		{
		}

		// Token: 0x06004995 RID: 18837 RVA: 0x00076D56 File Offset: 0x00074F56
		public SetDatabaseAvailabilityGroupConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004996 RID: 18838 RVA: 0x00076D65 File Offset: 0x00074F65
		public virtual SetDatabaseAvailabilityGroupConfigurationCommand SetParameters(SetDatabaseAvailabilityGroupConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004997 RID: 18839 RVA: 0x00076D6F File Offset: 0x00074F6F
		public virtual SetDatabaseAvailabilityGroupConfigurationCommand SetParameters(SetDatabaseAvailabilityGroupConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000575 RID: 1397
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002A65 RID: 10853
			// (set) Token: 0x06004998 RID: 18840 RVA: 0x00076D79 File Offset: 0x00074F79
			public virtual int ServersPerDag
			{
				set
				{
					base.PowerSharpParameters["ServersPerDag"] = value;
				}
			}

			// Token: 0x17002A66 RID: 10854
			// (set) Token: 0x06004999 RID: 18841 RVA: 0x00076D91 File Offset: 0x00074F91
			public virtual int DatabasesPerServer
			{
				set
				{
					base.PowerSharpParameters["DatabasesPerServer"] = value;
				}
			}

			// Token: 0x17002A67 RID: 10855
			// (set) Token: 0x0600499A RID: 18842 RVA: 0x00076DA9 File Offset: 0x00074FA9
			public virtual int DatabasesPerVolume
			{
				set
				{
					base.PowerSharpParameters["DatabasesPerVolume"] = value;
				}
			}

			// Token: 0x17002A68 RID: 10856
			// (set) Token: 0x0600499B RID: 18843 RVA: 0x00076DC1 File Offset: 0x00074FC1
			public virtual int CopiesPerDatabase
			{
				set
				{
					base.PowerSharpParameters["CopiesPerDatabase"] = value;
				}
			}

			// Token: 0x17002A69 RID: 10857
			// (set) Token: 0x0600499C RID: 18844 RVA: 0x00076DD9 File Offset: 0x00074FD9
			public virtual int MinCopiesPerDatabaseForMonitoring
			{
				set
				{
					base.PowerSharpParameters["MinCopiesPerDatabaseForMonitoring"] = value;
				}
			}

			// Token: 0x17002A6A RID: 10858
			// (set) Token: 0x0600499D RID: 18845 RVA: 0x00076DF1 File Offset: 0x00074FF1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002A6B RID: 10859
			// (set) Token: 0x0600499E RID: 18846 RVA: 0x00076E04 File Offset: 0x00075004
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002A6C RID: 10860
			// (set) Token: 0x0600499F RID: 18847 RVA: 0x00076E1C File Offset: 0x0007501C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002A6D RID: 10861
			// (set) Token: 0x060049A0 RID: 18848 RVA: 0x00076E34 File Offset: 0x00075034
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002A6E RID: 10862
			// (set) Token: 0x060049A1 RID: 18849 RVA: 0x00076E4C File Offset: 0x0007504C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002A6F RID: 10863
			// (set) Token: 0x060049A2 RID: 18850 RVA: 0x00076E64 File Offset: 0x00075064
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000576 RID: 1398
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002A70 RID: 10864
			// (set) Token: 0x060049A4 RID: 18852 RVA: 0x00076E84 File Offset: 0x00075084
			public virtual DatabaseAvailabilityGroupConfigurationIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002A71 RID: 10865
			// (set) Token: 0x060049A5 RID: 18853 RVA: 0x00076E97 File Offset: 0x00075097
			public virtual int ServersPerDag
			{
				set
				{
					base.PowerSharpParameters["ServersPerDag"] = value;
				}
			}

			// Token: 0x17002A72 RID: 10866
			// (set) Token: 0x060049A6 RID: 18854 RVA: 0x00076EAF File Offset: 0x000750AF
			public virtual int DatabasesPerServer
			{
				set
				{
					base.PowerSharpParameters["DatabasesPerServer"] = value;
				}
			}

			// Token: 0x17002A73 RID: 10867
			// (set) Token: 0x060049A7 RID: 18855 RVA: 0x00076EC7 File Offset: 0x000750C7
			public virtual int DatabasesPerVolume
			{
				set
				{
					base.PowerSharpParameters["DatabasesPerVolume"] = value;
				}
			}

			// Token: 0x17002A74 RID: 10868
			// (set) Token: 0x060049A8 RID: 18856 RVA: 0x00076EDF File Offset: 0x000750DF
			public virtual int CopiesPerDatabase
			{
				set
				{
					base.PowerSharpParameters["CopiesPerDatabase"] = value;
				}
			}

			// Token: 0x17002A75 RID: 10869
			// (set) Token: 0x060049A9 RID: 18857 RVA: 0x00076EF7 File Offset: 0x000750F7
			public virtual int MinCopiesPerDatabaseForMonitoring
			{
				set
				{
					base.PowerSharpParameters["MinCopiesPerDatabaseForMonitoring"] = value;
				}
			}

			// Token: 0x17002A76 RID: 10870
			// (set) Token: 0x060049AA RID: 18858 RVA: 0x00076F0F File Offset: 0x0007510F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002A77 RID: 10871
			// (set) Token: 0x060049AB RID: 18859 RVA: 0x00076F22 File Offset: 0x00075122
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002A78 RID: 10872
			// (set) Token: 0x060049AC RID: 18860 RVA: 0x00076F3A File Offset: 0x0007513A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002A79 RID: 10873
			// (set) Token: 0x060049AD RID: 18861 RVA: 0x00076F52 File Offset: 0x00075152
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002A7A RID: 10874
			// (set) Token: 0x060049AE RID: 18862 RVA: 0x00076F6A File Offset: 0x0007516A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002A7B RID: 10875
			// (set) Token: 0x060049AF RID: 18863 RVA: 0x00076F82 File Offset: 0x00075182
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
