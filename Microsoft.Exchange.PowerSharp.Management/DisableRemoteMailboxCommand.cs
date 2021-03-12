using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D4D RID: 3405
	public class DisableRemoteMailboxCommand : SyntheticCommandWithPipelineInput<RemoteMailboxIdParameter, RemoteMailboxIdParameter>
	{
		// Token: 0x0600B413 RID: 46099 RVA: 0x0010366C File Offset: 0x0010186C
		private DisableRemoteMailboxCommand() : base("Disable-RemoteMailbox")
		{
		}

		// Token: 0x0600B414 RID: 46100 RVA: 0x00103679 File Offset: 0x00101879
		public DisableRemoteMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B415 RID: 46101 RVA: 0x00103688 File Offset: 0x00101888
		public virtual DisableRemoteMailboxCommand SetParameters(DisableRemoteMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B416 RID: 46102 RVA: 0x00103692 File Offset: 0x00101892
		public virtual DisableRemoteMailboxCommand SetParameters(DisableRemoteMailboxCommand.ArchiveParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D4E RID: 3406
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17008532 RID: 34098
			// (set) Token: 0x0600B417 RID: 46103 RVA: 0x0010369C File Offset: 0x0010189C
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17008533 RID: 34099
			// (set) Token: 0x0600B418 RID: 46104 RVA: 0x001036B4 File Offset: 0x001018B4
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RemoteMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008534 RID: 34100
			// (set) Token: 0x0600B419 RID: 46105 RVA: 0x001036D2 File Offset: 0x001018D2
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x17008535 RID: 34101
			// (set) Token: 0x0600B41A RID: 46106 RVA: 0x001036EA File Offset: 0x001018EA
			public virtual SwitchParameter PreventRecordingPreviousDatabase
			{
				set
				{
					base.PowerSharpParameters["PreventRecordingPreviousDatabase"] = value;
				}
			}

			// Token: 0x17008536 RID: 34102
			// (set) Token: 0x0600B41B RID: 46107 RVA: 0x00103702 File Offset: 0x00101902
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008537 RID: 34103
			// (set) Token: 0x0600B41C RID: 46108 RVA: 0x00103715 File Offset: 0x00101915
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008538 RID: 34104
			// (set) Token: 0x0600B41D RID: 46109 RVA: 0x0010372D File Offset: 0x0010192D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008539 RID: 34105
			// (set) Token: 0x0600B41E RID: 46110 RVA: 0x00103745 File Offset: 0x00101945
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700853A RID: 34106
			// (set) Token: 0x0600B41F RID: 46111 RVA: 0x0010375D File Offset: 0x0010195D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700853B RID: 34107
			// (set) Token: 0x0600B420 RID: 46112 RVA: 0x00103775 File Offset: 0x00101975
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700853C RID: 34108
			// (set) Token: 0x0600B421 RID: 46113 RVA: 0x0010378D File Offset: 0x0010198D
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000D4F RID: 3407
		public class ArchiveParameters : ParametersBase
		{
			// Token: 0x1700853D RID: 34109
			// (set) Token: 0x0600B423 RID: 46115 RVA: 0x001037AD File Offset: 0x001019AD
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x1700853E RID: 34110
			// (set) Token: 0x0600B424 RID: 46116 RVA: 0x001037C5 File Offset: 0x001019C5
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700853F RID: 34111
			// (set) Token: 0x0600B425 RID: 46117 RVA: 0x001037DD File Offset: 0x001019DD
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RemoteMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008540 RID: 34112
			// (set) Token: 0x0600B426 RID: 46118 RVA: 0x001037FB File Offset: 0x001019FB
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x17008541 RID: 34113
			// (set) Token: 0x0600B427 RID: 46119 RVA: 0x00103813 File Offset: 0x00101A13
			public virtual SwitchParameter PreventRecordingPreviousDatabase
			{
				set
				{
					base.PowerSharpParameters["PreventRecordingPreviousDatabase"] = value;
				}
			}

			// Token: 0x17008542 RID: 34114
			// (set) Token: 0x0600B428 RID: 46120 RVA: 0x0010382B File Offset: 0x00101A2B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008543 RID: 34115
			// (set) Token: 0x0600B429 RID: 46121 RVA: 0x0010383E File Offset: 0x00101A3E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008544 RID: 34116
			// (set) Token: 0x0600B42A RID: 46122 RVA: 0x00103856 File Offset: 0x00101A56
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008545 RID: 34117
			// (set) Token: 0x0600B42B RID: 46123 RVA: 0x0010386E File Offset: 0x00101A6E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008546 RID: 34118
			// (set) Token: 0x0600B42C RID: 46124 RVA: 0x00103886 File Offset: 0x00101A86
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008547 RID: 34119
			// (set) Token: 0x0600B42D RID: 46125 RVA: 0x0010389E File Offset: 0x00101A9E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17008548 RID: 34120
			// (set) Token: 0x0600B42E RID: 46126 RVA: 0x001038B6 File Offset: 0x00101AB6
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
