using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200057A RID: 1402
	public class StartDatabaseAvailabilityGroupCommand : SyntheticCommandWithPipelineInputNoOutput<DatabaseAvailabilityGroupIdParameter>
	{
		// Token: 0x060049CE RID: 18894 RVA: 0x000771DD File Offset: 0x000753DD
		private StartDatabaseAvailabilityGroupCommand() : base("Start-DatabaseAvailabilityGroup")
		{
		}

		// Token: 0x060049CF RID: 18895 RVA: 0x000771EA File Offset: 0x000753EA
		public StartDatabaseAvailabilityGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060049D0 RID: 18896 RVA: 0x000771F9 File Offset: 0x000753F9
		public virtual StartDatabaseAvailabilityGroupCommand SetParameters(StartDatabaseAvailabilityGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060049D1 RID: 18897 RVA: 0x00077203 File Offset: 0x00075403
		public virtual StartDatabaseAvailabilityGroupCommand SetParameters(StartDatabaseAvailabilityGroupCommand.MailboxSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060049D2 RID: 18898 RVA: 0x0007720D File Offset: 0x0007540D
		public virtual StartDatabaseAvailabilityGroupCommand SetParameters(StartDatabaseAvailabilityGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200057B RID: 1403
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002A93 RID: 10899
			// (set) Token: 0x060049D3 RID: 18899 RVA: 0x00077217 File Offset: 0x00075417
			public virtual DatabaseAvailabilityGroupIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002A94 RID: 10900
			// (set) Token: 0x060049D4 RID: 18900 RVA: 0x0007722A File Offset: 0x0007542A
			public virtual string ActiveDirectorySite
			{
				set
				{
					base.PowerSharpParameters["ActiveDirectorySite"] = ((value != null) ? new AdSiteIdParameter(value) : null);
				}
			}

			// Token: 0x17002A95 RID: 10901
			// (set) Token: 0x060049D5 RID: 18901 RVA: 0x00077248 File Offset: 0x00075448
			public virtual SwitchParameter ConfigurationOnly
			{
				set
				{
					base.PowerSharpParameters["ConfigurationOnly"] = value;
				}
			}

			// Token: 0x17002A96 RID: 10902
			// (set) Token: 0x060049D6 RID: 18902 RVA: 0x00077260 File Offset: 0x00075460
			public virtual SwitchParameter QuorumOnly
			{
				set
				{
					base.PowerSharpParameters["QuorumOnly"] = value;
				}
			}

			// Token: 0x17002A97 RID: 10903
			// (set) Token: 0x060049D7 RID: 18903 RVA: 0x00077278 File Offset: 0x00075478
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002A98 RID: 10904
			// (set) Token: 0x060049D8 RID: 18904 RVA: 0x0007728B File Offset: 0x0007548B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002A99 RID: 10905
			// (set) Token: 0x060049D9 RID: 18905 RVA: 0x000772A3 File Offset: 0x000754A3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002A9A RID: 10906
			// (set) Token: 0x060049DA RID: 18906 RVA: 0x000772BB File Offset: 0x000754BB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002A9B RID: 10907
			// (set) Token: 0x060049DB RID: 18907 RVA: 0x000772D3 File Offset: 0x000754D3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002A9C RID: 10908
			// (set) Token: 0x060049DC RID: 18908 RVA: 0x000772EB File Offset: 0x000754EB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200057C RID: 1404
		public class MailboxSetParameters : ParametersBase
		{
			// Token: 0x17002A9D RID: 10909
			// (set) Token: 0x060049DE RID: 18910 RVA: 0x0007730B File Offset: 0x0007550B
			public virtual DatabaseAvailabilityGroupIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002A9E RID: 10910
			// (set) Token: 0x060049DF RID: 18911 RVA: 0x0007731E File Offset: 0x0007551E
			public virtual MailboxServerIdParameter MailboxServer
			{
				set
				{
					base.PowerSharpParameters["MailboxServer"] = value;
				}
			}

			// Token: 0x17002A9F RID: 10911
			// (set) Token: 0x060049E0 RID: 18912 RVA: 0x00077331 File Offset: 0x00075531
			public virtual SwitchParameter ConfigurationOnly
			{
				set
				{
					base.PowerSharpParameters["ConfigurationOnly"] = value;
				}
			}

			// Token: 0x17002AA0 RID: 10912
			// (set) Token: 0x060049E1 RID: 18913 RVA: 0x00077349 File Offset: 0x00075549
			public virtual SwitchParameter QuorumOnly
			{
				set
				{
					base.PowerSharpParameters["QuorumOnly"] = value;
				}
			}

			// Token: 0x17002AA1 RID: 10913
			// (set) Token: 0x060049E2 RID: 18914 RVA: 0x00077361 File Offset: 0x00075561
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002AA2 RID: 10914
			// (set) Token: 0x060049E3 RID: 18915 RVA: 0x00077374 File Offset: 0x00075574
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002AA3 RID: 10915
			// (set) Token: 0x060049E4 RID: 18916 RVA: 0x0007738C File Offset: 0x0007558C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002AA4 RID: 10916
			// (set) Token: 0x060049E5 RID: 18917 RVA: 0x000773A4 File Offset: 0x000755A4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002AA5 RID: 10917
			// (set) Token: 0x060049E6 RID: 18918 RVA: 0x000773BC File Offset: 0x000755BC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002AA6 RID: 10918
			// (set) Token: 0x060049E7 RID: 18919 RVA: 0x000773D4 File Offset: 0x000755D4
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200057D RID: 1405
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002AA7 RID: 10919
			// (set) Token: 0x060049E9 RID: 18921 RVA: 0x000773F4 File Offset: 0x000755F4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002AA8 RID: 10920
			// (set) Token: 0x060049EA RID: 18922 RVA: 0x00077407 File Offset: 0x00075607
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002AA9 RID: 10921
			// (set) Token: 0x060049EB RID: 18923 RVA: 0x0007741F File Offset: 0x0007561F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002AAA RID: 10922
			// (set) Token: 0x060049EC RID: 18924 RVA: 0x00077437 File Offset: 0x00075637
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002AAB RID: 10923
			// (set) Token: 0x060049ED RID: 18925 RVA: 0x0007744F File Offset: 0x0007564F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002AAC RID: 10924
			// (set) Token: 0x060049EE RID: 18926 RVA: 0x00077467 File Offset: 0x00075667
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
