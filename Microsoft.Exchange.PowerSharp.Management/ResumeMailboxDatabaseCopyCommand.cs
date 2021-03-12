using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200056D RID: 1389
	public class ResumeMailboxDatabaseCopyCommand : SyntheticCommandWithPipelineInputNoOutput<DatabaseCopyIdParameter>
	{
		// Token: 0x06004924 RID: 18724 RVA: 0x00076404 File Offset: 0x00074604
		private ResumeMailboxDatabaseCopyCommand() : base("Resume-MailboxDatabaseCopy")
		{
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x00076411 File Offset: 0x00074611
		public ResumeMailboxDatabaseCopyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004926 RID: 18726 RVA: 0x00076420 File Offset: 0x00074620
		public virtual ResumeMailboxDatabaseCopyCommand SetParameters(ResumeMailboxDatabaseCopyCommand.DisableReplayLagParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004927 RID: 18727 RVA: 0x0007642A File Offset: 0x0007462A
		public virtual ResumeMailboxDatabaseCopyCommand SetParameters(ResumeMailboxDatabaseCopyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004928 RID: 18728 RVA: 0x00076434 File Offset: 0x00074634
		public virtual ResumeMailboxDatabaseCopyCommand SetParameters(ResumeMailboxDatabaseCopyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200056E RID: 1390
		public class DisableReplayLagParameters : ParametersBase
		{
			// Token: 0x17002A03 RID: 10755
			// (set) Token: 0x06004929 RID: 18729 RVA: 0x0007643E File Offset: 0x0007463E
			public virtual DatabaseCopyIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002A04 RID: 10756
			// (set) Token: 0x0600492A RID: 18730 RVA: 0x00076451 File Offset: 0x00074651
			public virtual SwitchParameter DisableReplayLag
			{
				set
				{
					base.PowerSharpParameters["DisableReplayLag"] = value;
				}
			}

			// Token: 0x17002A05 RID: 10757
			// (set) Token: 0x0600492B RID: 18731 RVA: 0x00076469 File Offset: 0x00074669
			public virtual string DisableReplayLagReason
			{
				set
				{
					base.PowerSharpParameters["DisableReplayLagReason"] = value;
				}
			}

			// Token: 0x17002A06 RID: 10758
			// (set) Token: 0x0600492C RID: 18732 RVA: 0x0007647C File Offset: 0x0007467C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002A07 RID: 10759
			// (set) Token: 0x0600492D RID: 18733 RVA: 0x0007648F File Offset: 0x0007468F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002A08 RID: 10760
			// (set) Token: 0x0600492E RID: 18734 RVA: 0x000764A7 File Offset: 0x000746A7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002A09 RID: 10761
			// (set) Token: 0x0600492F RID: 18735 RVA: 0x000764BF File Offset: 0x000746BF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002A0A RID: 10762
			// (set) Token: 0x06004930 RID: 18736 RVA: 0x000764D7 File Offset: 0x000746D7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002A0B RID: 10763
			// (set) Token: 0x06004931 RID: 18737 RVA: 0x000764EF File Offset: 0x000746EF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200056F RID: 1391
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002A0C RID: 10764
			// (set) Token: 0x06004933 RID: 18739 RVA: 0x0007650F File Offset: 0x0007470F
			public virtual DatabaseCopyIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002A0D RID: 10765
			// (set) Token: 0x06004934 RID: 18740 RVA: 0x00076522 File Offset: 0x00074722
			public virtual SwitchParameter ReplicationOnly
			{
				set
				{
					base.PowerSharpParameters["ReplicationOnly"] = value;
				}
			}

			// Token: 0x17002A0E RID: 10766
			// (set) Token: 0x06004935 RID: 18741 RVA: 0x0007653A File Offset: 0x0007473A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002A0F RID: 10767
			// (set) Token: 0x06004936 RID: 18742 RVA: 0x0007654D File Offset: 0x0007474D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002A10 RID: 10768
			// (set) Token: 0x06004937 RID: 18743 RVA: 0x00076565 File Offset: 0x00074765
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002A11 RID: 10769
			// (set) Token: 0x06004938 RID: 18744 RVA: 0x0007657D File Offset: 0x0007477D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002A12 RID: 10770
			// (set) Token: 0x06004939 RID: 18745 RVA: 0x00076595 File Offset: 0x00074795
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002A13 RID: 10771
			// (set) Token: 0x0600493A RID: 18746 RVA: 0x000765AD File Offset: 0x000747AD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000570 RID: 1392
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002A14 RID: 10772
			// (set) Token: 0x0600493C RID: 18748 RVA: 0x000765CD File Offset: 0x000747CD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002A15 RID: 10773
			// (set) Token: 0x0600493D RID: 18749 RVA: 0x000765E0 File Offset: 0x000747E0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002A16 RID: 10774
			// (set) Token: 0x0600493E RID: 18750 RVA: 0x000765F8 File Offset: 0x000747F8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002A17 RID: 10775
			// (set) Token: 0x0600493F RID: 18751 RVA: 0x00076610 File Offset: 0x00074810
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002A18 RID: 10776
			// (set) Token: 0x06004940 RID: 18752 RVA: 0x00076628 File Offset: 0x00074828
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002A19 RID: 10777
			// (set) Token: 0x06004941 RID: 18753 RVA: 0x00076640 File Offset: 0x00074840
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
