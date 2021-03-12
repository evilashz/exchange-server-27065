using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000CF6 RID: 3318
	public class DisableMailUserCommand : SyntheticCommandWithPipelineInput<MailUserIdParameter, MailUserIdParameter>
	{
		// Token: 0x0600AEAE RID: 44718 RVA: 0x000FC4DF File Offset: 0x000FA6DF
		private DisableMailUserCommand() : base("Disable-MailUser")
		{
		}

		// Token: 0x0600AEAF RID: 44719 RVA: 0x000FC4EC File Offset: 0x000FA6EC
		public DisableMailUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600AEB0 RID: 44720 RVA: 0x000FC4FB File Offset: 0x000FA6FB
		public virtual DisableMailUserCommand SetParameters(DisableMailUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AEB1 RID: 44721 RVA: 0x000FC505 File Offset: 0x000FA705
		public virtual DisableMailUserCommand SetParameters(DisableMailUserCommand.ArchiveParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000CF7 RID: 3319
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700807B RID: 32891
			// (set) Token: 0x0600AEB2 RID: 44722 RVA: 0x000FC50F File Offset: 0x000FA70F
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x1700807C RID: 32892
			// (set) Token: 0x0600AEB3 RID: 44723 RVA: 0x000FC527 File Offset: 0x000FA727
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700807D RID: 32893
			// (set) Token: 0x0600AEB4 RID: 44724 RVA: 0x000FC53F File Offset: 0x000FA73F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700807E RID: 32894
			// (set) Token: 0x0600AEB5 RID: 44725 RVA: 0x000FC55D File Offset: 0x000FA75D
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x1700807F RID: 32895
			// (set) Token: 0x0600AEB6 RID: 44726 RVA: 0x000FC575 File Offset: 0x000FA775
			public virtual SwitchParameter PreventRecordingPreviousDatabase
			{
				set
				{
					base.PowerSharpParameters["PreventRecordingPreviousDatabase"] = value;
				}
			}

			// Token: 0x17008080 RID: 32896
			// (set) Token: 0x0600AEB7 RID: 44727 RVA: 0x000FC58D File Offset: 0x000FA78D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008081 RID: 32897
			// (set) Token: 0x0600AEB8 RID: 44728 RVA: 0x000FC5A0 File Offset: 0x000FA7A0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008082 RID: 32898
			// (set) Token: 0x0600AEB9 RID: 44729 RVA: 0x000FC5B8 File Offset: 0x000FA7B8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008083 RID: 32899
			// (set) Token: 0x0600AEBA RID: 44730 RVA: 0x000FC5D0 File Offset: 0x000FA7D0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008084 RID: 32900
			// (set) Token: 0x0600AEBB RID: 44731 RVA: 0x000FC5E8 File Offset: 0x000FA7E8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008085 RID: 32901
			// (set) Token: 0x0600AEBC RID: 44732 RVA: 0x000FC600 File Offset: 0x000FA800
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17008086 RID: 32902
			// (set) Token: 0x0600AEBD RID: 44733 RVA: 0x000FC618 File Offset: 0x000FA818
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000CF8 RID: 3320
		public class ArchiveParameters : ParametersBase
		{
			// Token: 0x17008087 RID: 32903
			// (set) Token: 0x0600AEBF RID: 44735 RVA: 0x000FC638 File Offset: 0x000FA838
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17008088 RID: 32904
			// (set) Token: 0x0600AEC0 RID: 44736 RVA: 0x000FC650 File Offset: 0x000FA850
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17008089 RID: 32905
			// (set) Token: 0x0600AEC1 RID: 44737 RVA: 0x000FC668 File Offset: 0x000FA868
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700808A RID: 32906
			// (set) Token: 0x0600AEC2 RID: 44738 RVA: 0x000FC680 File Offset: 0x000FA880
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700808B RID: 32907
			// (set) Token: 0x0600AEC3 RID: 44739 RVA: 0x000FC69E File Offset: 0x000FA89E
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x1700808C RID: 32908
			// (set) Token: 0x0600AEC4 RID: 44740 RVA: 0x000FC6B6 File Offset: 0x000FA8B6
			public virtual SwitchParameter PreventRecordingPreviousDatabase
			{
				set
				{
					base.PowerSharpParameters["PreventRecordingPreviousDatabase"] = value;
				}
			}

			// Token: 0x1700808D RID: 32909
			// (set) Token: 0x0600AEC5 RID: 44741 RVA: 0x000FC6CE File Offset: 0x000FA8CE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700808E RID: 32910
			// (set) Token: 0x0600AEC6 RID: 44742 RVA: 0x000FC6E1 File Offset: 0x000FA8E1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700808F RID: 32911
			// (set) Token: 0x0600AEC7 RID: 44743 RVA: 0x000FC6F9 File Offset: 0x000FA8F9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008090 RID: 32912
			// (set) Token: 0x0600AEC8 RID: 44744 RVA: 0x000FC711 File Offset: 0x000FA911
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008091 RID: 32913
			// (set) Token: 0x0600AEC9 RID: 44745 RVA: 0x000FC729 File Offset: 0x000FA929
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008092 RID: 32914
			// (set) Token: 0x0600AECA RID: 44746 RVA: 0x000FC741 File Offset: 0x000FA941
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17008093 RID: 32915
			// (set) Token: 0x0600AECB RID: 44747 RVA: 0x000FC759 File Offset: 0x000FA959
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
