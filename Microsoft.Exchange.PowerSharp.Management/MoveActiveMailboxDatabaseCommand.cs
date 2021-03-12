using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200054B RID: 1355
	public class MoveActiveMailboxDatabaseCommand : SyntheticCommandWithPipelineInput<Database, Database>
	{
		// Token: 0x060047FF RID: 18431 RVA: 0x00074D17 File Offset: 0x00072F17
		private MoveActiveMailboxDatabaseCommand() : base("Move-ActiveMailboxDatabase")
		{
		}

		// Token: 0x06004800 RID: 18432 RVA: 0x00074D24 File Offset: 0x00072F24
		public MoveActiveMailboxDatabaseCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004801 RID: 18433 RVA: 0x00074D33 File Offset: 0x00072F33
		public virtual MoveActiveMailboxDatabaseCommand SetParameters(MoveActiveMailboxDatabaseCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004802 RID: 18434 RVA: 0x00074D3D File Offset: 0x00072F3D
		public virtual MoveActiveMailboxDatabaseCommand SetParameters(MoveActiveMailboxDatabaseCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004803 RID: 18435 RVA: 0x00074D47 File Offset: 0x00072F47
		public virtual MoveActiveMailboxDatabaseCommand SetParameters(MoveActiveMailboxDatabaseCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004804 RID: 18436 RVA: 0x00074D51 File Offset: 0x00072F51
		public virtual MoveActiveMailboxDatabaseCommand SetParameters(MoveActiveMailboxDatabaseCommand.ActivatePreferredParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200054C RID: 1356
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002922 RID: 10530
			// (set) Token: 0x06004805 RID: 18437 RVA: 0x00074D5B File Offset: 0x00072F5B
			public virtual MailboxServerIdParameter ActivateOnServer
			{
				set
				{
					base.PowerSharpParameters["ActivateOnServer"] = value;
				}
			}

			// Token: 0x17002923 RID: 10531
			// (set) Token: 0x06004806 RID: 18438 RVA: 0x00074D6E File Offset: 0x00072F6E
			public virtual DatabaseIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002924 RID: 10532
			// (set) Token: 0x06004807 RID: 18439 RVA: 0x00074D81 File Offset: 0x00072F81
			public virtual DatabaseMountDialOverride MountDialOverride
			{
				set
				{
					base.PowerSharpParameters["MountDialOverride"] = value;
				}
			}

			// Token: 0x17002925 RID: 10533
			// (set) Token: 0x06004808 RID: 18440 RVA: 0x00074D99 File Offset: 0x00072F99
			public virtual SwitchParameter SkipActiveCopyChecks
			{
				set
				{
					base.PowerSharpParameters["SkipActiveCopyChecks"] = value;
				}
			}

			// Token: 0x17002926 RID: 10534
			// (set) Token: 0x06004809 RID: 18441 RVA: 0x00074DB1 File Offset: 0x00072FB1
			public virtual SwitchParameter SkipClientExperienceChecks
			{
				set
				{
					base.PowerSharpParameters["SkipClientExperienceChecks"] = value;
				}
			}

			// Token: 0x17002927 RID: 10535
			// (set) Token: 0x0600480A RID: 18442 RVA: 0x00074DC9 File Offset: 0x00072FC9
			public virtual SwitchParameter SkipHealthChecks
			{
				set
				{
					base.PowerSharpParameters["SkipHealthChecks"] = value;
				}
			}

			// Token: 0x17002928 RID: 10536
			// (set) Token: 0x0600480B RID: 18443 RVA: 0x00074DE1 File Offset: 0x00072FE1
			public virtual SwitchParameter SkipLagChecks
			{
				set
				{
					base.PowerSharpParameters["SkipLagChecks"] = value;
				}
			}

			// Token: 0x17002929 RID: 10537
			// (set) Token: 0x0600480C RID: 18444 RVA: 0x00074DF9 File Offset: 0x00072FF9
			public virtual SwitchParameter SkipMaximumActiveDatabasesChecks
			{
				set
				{
					base.PowerSharpParameters["SkipMaximumActiveDatabasesChecks"] = value;
				}
			}

			// Token: 0x1700292A RID: 10538
			// (set) Token: 0x0600480D RID: 18445 RVA: 0x00074E11 File Offset: 0x00073011
			public virtual SwitchParameter TerminateOnWarning
			{
				set
				{
					base.PowerSharpParameters["TerminateOnWarning"] = value;
				}
			}

			// Token: 0x1700292B RID: 10539
			// (set) Token: 0x0600480E RID: 18446 RVA: 0x00074E29 File Offset: 0x00073029
			public virtual string MoveComment
			{
				set
				{
					base.PowerSharpParameters["MoveComment"] = value;
				}
			}

			// Token: 0x1700292C RID: 10540
			// (set) Token: 0x0600480F RID: 18447 RVA: 0x00074E3C File Offset: 0x0007303C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700292D RID: 10541
			// (set) Token: 0x06004810 RID: 18448 RVA: 0x00074E4F File Offset: 0x0007304F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700292E RID: 10542
			// (set) Token: 0x06004811 RID: 18449 RVA: 0x00074E67 File Offset: 0x00073067
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700292F RID: 10543
			// (set) Token: 0x06004812 RID: 18450 RVA: 0x00074E7F File Offset: 0x0007307F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002930 RID: 10544
			// (set) Token: 0x06004813 RID: 18451 RVA: 0x00074E97 File Offset: 0x00073097
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002931 RID: 10545
			// (set) Token: 0x06004814 RID: 18452 RVA: 0x00074EAF File Offset: 0x000730AF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200054D RID: 1357
		public class ServerParameters : ParametersBase
		{
			// Token: 0x17002932 RID: 10546
			// (set) Token: 0x06004816 RID: 18454 RVA: 0x00074ECF File Offset: 0x000730CF
			public virtual MailboxServerIdParameter ActivateOnServer
			{
				set
				{
					base.PowerSharpParameters["ActivateOnServer"] = value;
				}
			}

			// Token: 0x17002933 RID: 10547
			// (set) Token: 0x06004817 RID: 18455 RVA: 0x00074EE2 File Offset: 0x000730E2
			public virtual MailboxServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17002934 RID: 10548
			// (set) Token: 0x06004818 RID: 18456 RVA: 0x00074EF5 File Offset: 0x000730F5
			public virtual DatabaseMountDialOverride MountDialOverride
			{
				set
				{
					base.PowerSharpParameters["MountDialOverride"] = value;
				}
			}

			// Token: 0x17002935 RID: 10549
			// (set) Token: 0x06004819 RID: 18457 RVA: 0x00074F0D File Offset: 0x0007310D
			public virtual SwitchParameter SkipActiveCopyChecks
			{
				set
				{
					base.PowerSharpParameters["SkipActiveCopyChecks"] = value;
				}
			}

			// Token: 0x17002936 RID: 10550
			// (set) Token: 0x0600481A RID: 18458 RVA: 0x00074F25 File Offset: 0x00073125
			public virtual SwitchParameter SkipClientExperienceChecks
			{
				set
				{
					base.PowerSharpParameters["SkipClientExperienceChecks"] = value;
				}
			}

			// Token: 0x17002937 RID: 10551
			// (set) Token: 0x0600481B RID: 18459 RVA: 0x00074F3D File Offset: 0x0007313D
			public virtual SwitchParameter SkipHealthChecks
			{
				set
				{
					base.PowerSharpParameters["SkipHealthChecks"] = value;
				}
			}

			// Token: 0x17002938 RID: 10552
			// (set) Token: 0x0600481C RID: 18460 RVA: 0x00074F55 File Offset: 0x00073155
			public virtual SwitchParameter SkipLagChecks
			{
				set
				{
					base.PowerSharpParameters["SkipLagChecks"] = value;
				}
			}

			// Token: 0x17002939 RID: 10553
			// (set) Token: 0x0600481D RID: 18461 RVA: 0x00074F6D File Offset: 0x0007316D
			public virtual SwitchParameter SkipMaximumActiveDatabasesChecks
			{
				set
				{
					base.PowerSharpParameters["SkipMaximumActiveDatabasesChecks"] = value;
				}
			}

			// Token: 0x1700293A RID: 10554
			// (set) Token: 0x0600481E RID: 18462 RVA: 0x00074F85 File Offset: 0x00073185
			public virtual SwitchParameter TerminateOnWarning
			{
				set
				{
					base.PowerSharpParameters["TerminateOnWarning"] = value;
				}
			}

			// Token: 0x1700293B RID: 10555
			// (set) Token: 0x0600481F RID: 18463 RVA: 0x00074F9D File Offset: 0x0007319D
			public virtual string MoveComment
			{
				set
				{
					base.PowerSharpParameters["MoveComment"] = value;
				}
			}

			// Token: 0x1700293C RID: 10556
			// (set) Token: 0x06004820 RID: 18464 RVA: 0x00074FB0 File Offset: 0x000731B0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700293D RID: 10557
			// (set) Token: 0x06004821 RID: 18465 RVA: 0x00074FC3 File Offset: 0x000731C3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700293E RID: 10558
			// (set) Token: 0x06004822 RID: 18466 RVA: 0x00074FDB File Offset: 0x000731DB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700293F RID: 10559
			// (set) Token: 0x06004823 RID: 18467 RVA: 0x00074FF3 File Offset: 0x000731F3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002940 RID: 10560
			// (set) Token: 0x06004824 RID: 18468 RVA: 0x0007500B File Offset: 0x0007320B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002941 RID: 10561
			// (set) Token: 0x06004825 RID: 18469 RVA: 0x00075023 File Offset: 0x00073223
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200054E RID: 1358
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002942 RID: 10562
			// (set) Token: 0x06004827 RID: 18471 RVA: 0x00075043 File Offset: 0x00073243
			public virtual DatabaseMountDialOverride MountDialOverride
			{
				set
				{
					base.PowerSharpParameters["MountDialOverride"] = value;
				}
			}

			// Token: 0x17002943 RID: 10563
			// (set) Token: 0x06004828 RID: 18472 RVA: 0x0007505B File Offset: 0x0007325B
			public virtual SwitchParameter SkipActiveCopyChecks
			{
				set
				{
					base.PowerSharpParameters["SkipActiveCopyChecks"] = value;
				}
			}

			// Token: 0x17002944 RID: 10564
			// (set) Token: 0x06004829 RID: 18473 RVA: 0x00075073 File Offset: 0x00073273
			public virtual SwitchParameter SkipClientExperienceChecks
			{
				set
				{
					base.PowerSharpParameters["SkipClientExperienceChecks"] = value;
				}
			}

			// Token: 0x17002945 RID: 10565
			// (set) Token: 0x0600482A RID: 18474 RVA: 0x0007508B File Offset: 0x0007328B
			public virtual SwitchParameter SkipHealthChecks
			{
				set
				{
					base.PowerSharpParameters["SkipHealthChecks"] = value;
				}
			}

			// Token: 0x17002946 RID: 10566
			// (set) Token: 0x0600482B RID: 18475 RVA: 0x000750A3 File Offset: 0x000732A3
			public virtual SwitchParameter SkipLagChecks
			{
				set
				{
					base.PowerSharpParameters["SkipLagChecks"] = value;
				}
			}

			// Token: 0x17002947 RID: 10567
			// (set) Token: 0x0600482C RID: 18476 RVA: 0x000750BB File Offset: 0x000732BB
			public virtual SwitchParameter SkipMaximumActiveDatabasesChecks
			{
				set
				{
					base.PowerSharpParameters["SkipMaximumActiveDatabasesChecks"] = value;
				}
			}

			// Token: 0x17002948 RID: 10568
			// (set) Token: 0x0600482D RID: 18477 RVA: 0x000750D3 File Offset: 0x000732D3
			public virtual SwitchParameter TerminateOnWarning
			{
				set
				{
					base.PowerSharpParameters["TerminateOnWarning"] = value;
				}
			}

			// Token: 0x17002949 RID: 10569
			// (set) Token: 0x0600482E RID: 18478 RVA: 0x000750EB File Offset: 0x000732EB
			public virtual string MoveComment
			{
				set
				{
					base.PowerSharpParameters["MoveComment"] = value;
				}
			}

			// Token: 0x1700294A RID: 10570
			// (set) Token: 0x0600482F RID: 18479 RVA: 0x000750FE File Offset: 0x000732FE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700294B RID: 10571
			// (set) Token: 0x06004830 RID: 18480 RVA: 0x00075111 File Offset: 0x00073311
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700294C RID: 10572
			// (set) Token: 0x06004831 RID: 18481 RVA: 0x00075129 File Offset: 0x00073329
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700294D RID: 10573
			// (set) Token: 0x06004832 RID: 18482 RVA: 0x00075141 File Offset: 0x00073341
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700294E RID: 10574
			// (set) Token: 0x06004833 RID: 18483 RVA: 0x00075159 File Offset: 0x00073359
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700294F RID: 10575
			// (set) Token: 0x06004834 RID: 18484 RVA: 0x00075171 File Offset: 0x00073371
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200054F RID: 1359
		public class ActivatePreferredParameters : ParametersBase
		{
			// Token: 0x17002950 RID: 10576
			// (set) Token: 0x06004836 RID: 18486 RVA: 0x00075191 File Offset: 0x00073391
			public virtual MailboxServerIdParameter ActivatePreferredOnServer
			{
				set
				{
					base.PowerSharpParameters["ActivatePreferredOnServer"] = value;
				}
			}

			// Token: 0x17002951 RID: 10577
			// (set) Token: 0x06004837 RID: 18487 RVA: 0x000751A4 File Offset: 0x000733A4
			public virtual DatabaseMountDialOverride MountDialOverride
			{
				set
				{
					base.PowerSharpParameters["MountDialOverride"] = value;
				}
			}

			// Token: 0x17002952 RID: 10578
			// (set) Token: 0x06004838 RID: 18488 RVA: 0x000751BC File Offset: 0x000733BC
			public virtual SwitchParameter SkipActiveCopyChecks
			{
				set
				{
					base.PowerSharpParameters["SkipActiveCopyChecks"] = value;
				}
			}

			// Token: 0x17002953 RID: 10579
			// (set) Token: 0x06004839 RID: 18489 RVA: 0x000751D4 File Offset: 0x000733D4
			public virtual SwitchParameter SkipClientExperienceChecks
			{
				set
				{
					base.PowerSharpParameters["SkipClientExperienceChecks"] = value;
				}
			}

			// Token: 0x17002954 RID: 10580
			// (set) Token: 0x0600483A RID: 18490 RVA: 0x000751EC File Offset: 0x000733EC
			public virtual SwitchParameter SkipHealthChecks
			{
				set
				{
					base.PowerSharpParameters["SkipHealthChecks"] = value;
				}
			}

			// Token: 0x17002955 RID: 10581
			// (set) Token: 0x0600483B RID: 18491 RVA: 0x00075204 File Offset: 0x00073404
			public virtual SwitchParameter SkipLagChecks
			{
				set
				{
					base.PowerSharpParameters["SkipLagChecks"] = value;
				}
			}

			// Token: 0x17002956 RID: 10582
			// (set) Token: 0x0600483C RID: 18492 RVA: 0x0007521C File Offset: 0x0007341C
			public virtual SwitchParameter SkipMaximumActiveDatabasesChecks
			{
				set
				{
					base.PowerSharpParameters["SkipMaximumActiveDatabasesChecks"] = value;
				}
			}

			// Token: 0x17002957 RID: 10583
			// (set) Token: 0x0600483D RID: 18493 RVA: 0x00075234 File Offset: 0x00073434
			public virtual SwitchParameter TerminateOnWarning
			{
				set
				{
					base.PowerSharpParameters["TerminateOnWarning"] = value;
				}
			}

			// Token: 0x17002958 RID: 10584
			// (set) Token: 0x0600483E RID: 18494 RVA: 0x0007524C File Offset: 0x0007344C
			public virtual string MoveComment
			{
				set
				{
					base.PowerSharpParameters["MoveComment"] = value;
				}
			}

			// Token: 0x17002959 RID: 10585
			// (set) Token: 0x0600483F RID: 18495 RVA: 0x0007525F File Offset: 0x0007345F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700295A RID: 10586
			// (set) Token: 0x06004840 RID: 18496 RVA: 0x00075272 File Offset: 0x00073472
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700295B RID: 10587
			// (set) Token: 0x06004841 RID: 18497 RVA: 0x0007528A File Offset: 0x0007348A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700295C RID: 10588
			// (set) Token: 0x06004842 RID: 18498 RVA: 0x000752A2 File Offset: 0x000734A2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700295D RID: 10589
			// (set) Token: 0x06004843 RID: 18499 RVA: 0x000752BA File Offset: 0x000734BA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700295E RID: 10590
			// (set) Token: 0x06004844 RID: 18500 RVA: 0x000752D2 File Offset: 0x000734D2
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
