using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.InfoWorker.Common.OOF;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000479 RID: 1145
	public class SetMailboxAutoReplyConfigurationCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxAutoReplyConfiguration>
	{
		// Token: 0x060040F9 RID: 16633 RVA: 0x0006C0E6 File Offset: 0x0006A2E6
		private SetMailboxAutoReplyConfigurationCommand() : base("Set-MailboxAutoReplyConfiguration")
		{
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x0006C0F3 File Offset: 0x0006A2F3
		public SetMailboxAutoReplyConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x0006C102 File Offset: 0x0006A302
		public virtual SetMailboxAutoReplyConfigurationCommand SetParameters(SetMailboxAutoReplyConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x0006C10C File Offset: 0x0006A30C
		public virtual SetMailboxAutoReplyConfigurationCommand SetParameters(SetMailboxAutoReplyConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200047A RID: 1146
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170023C0 RID: 9152
			// (set) Token: 0x060040FD RID: 16637 RVA: 0x0006C116 File Offset: 0x0006A316
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170023C1 RID: 9153
			// (set) Token: 0x060040FE RID: 16638 RVA: 0x0006C134 File Offset: 0x0006A334
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170023C2 RID: 9154
			// (set) Token: 0x060040FF RID: 16639 RVA: 0x0006C14C File Offset: 0x0006A34C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170023C3 RID: 9155
			// (set) Token: 0x06004100 RID: 16640 RVA: 0x0006C15F File Offset: 0x0006A35F
			public virtual OofState AutoReplyState
			{
				set
				{
					base.PowerSharpParameters["AutoReplyState"] = value;
				}
			}

			// Token: 0x170023C4 RID: 9156
			// (set) Token: 0x06004101 RID: 16641 RVA: 0x0006C177 File Offset: 0x0006A377
			public virtual DateTime EndTime
			{
				set
				{
					base.PowerSharpParameters["EndTime"] = value;
				}
			}

			// Token: 0x170023C5 RID: 9157
			// (set) Token: 0x06004102 RID: 16642 RVA: 0x0006C18F File Offset: 0x0006A38F
			public virtual ExternalAudience ExternalAudience
			{
				set
				{
					base.PowerSharpParameters["ExternalAudience"] = value;
				}
			}

			// Token: 0x170023C6 RID: 9158
			// (set) Token: 0x06004103 RID: 16643 RVA: 0x0006C1A7 File Offset: 0x0006A3A7
			public virtual string ExternalMessage
			{
				set
				{
					base.PowerSharpParameters["ExternalMessage"] = value;
				}
			}

			// Token: 0x170023C7 RID: 9159
			// (set) Token: 0x06004104 RID: 16644 RVA: 0x0006C1BA File Offset: 0x0006A3BA
			public virtual string InternalMessage
			{
				set
				{
					base.PowerSharpParameters["InternalMessage"] = value;
				}
			}

			// Token: 0x170023C8 RID: 9160
			// (set) Token: 0x06004105 RID: 16645 RVA: 0x0006C1CD File Offset: 0x0006A3CD
			public virtual DateTime StartTime
			{
				set
				{
					base.PowerSharpParameters["StartTime"] = value;
				}
			}

			// Token: 0x170023C9 RID: 9161
			// (set) Token: 0x06004106 RID: 16646 RVA: 0x0006C1E5 File Offset: 0x0006A3E5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170023CA RID: 9162
			// (set) Token: 0x06004107 RID: 16647 RVA: 0x0006C1FD File Offset: 0x0006A3FD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170023CB RID: 9163
			// (set) Token: 0x06004108 RID: 16648 RVA: 0x0006C215 File Offset: 0x0006A415
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170023CC RID: 9164
			// (set) Token: 0x06004109 RID: 16649 RVA: 0x0006C22D File Offset: 0x0006A42D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170023CD RID: 9165
			// (set) Token: 0x0600410A RID: 16650 RVA: 0x0006C245 File Offset: 0x0006A445
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200047B RID: 1147
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170023CE RID: 9166
			// (set) Token: 0x0600410C RID: 16652 RVA: 0x0006C265 File Offset: 0x0006A465
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170023CF RID: 9167
			// (set) Token: 0x0600410D RID: 16653 RVA: 0x0006C27D File Offset: 0x0006A47D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170023D0 RID: 9168
			// (set) Token: 0x0600410E RID: 16654 RVA: 0x0006C290 File Offset: 0x0006A490
			public virtual OofState AutoReplyState
			{
				set
				{
					base.PowerSharpParameters["AutoReplyState"] = value;
				}
			}

			// Token: 0x170023D1 RID: 9169
			// (set) Token: 0x0600410F RID: 16655 RVA: 0x0006C2A8 File Offset: 0x0006A4A8
			public virtual DateTime EndTime
			{
				set
				{
					base.PowerSharpParameters["EndTime"] = value;
				}
			}

			// Token: 0x170023D2 RID: 9170
			// (set) Token: 0x06004110 RID: 16656 RVA: 0x0006C2C0 File Offset: 0x0006A4C0
			public virtual ExternalAudience ExternalAudience
			{
				set
				{
					base.PowerSharpParameters["ExternalAudience"] = value;
				}
			}

			// Token: 0x170023D3 RID: 9171
			// (set) Token: 0x06004111 RID: 16657 RVA: 0x0006C2D8 File Offset: 0x0006A4D8
			public virtual string ExternalMessage
			{
				set
				{
					base.PowerSharpParameters["ExternalMessage"] = value;
				}
			}

			// Token: 0x170023D4 RID: 9172
			// (set) Token: 0x06004112 RID: 16658 RVA: 0x0006C2EB File Offset: 0x0006A4EB
			public virtual string InternalMessage
			{
				set
				{
					base.PowerSharpParameters["InternalMessage"] = value;
				}
			}

			// Token: 0x170023D5 RID: 9173
			// (set) Token: 0x06004113 RID: 16659 RVA: 0x0006C2FE File Offset: 0x0006A4FE
			public virtual DateTime StartTime
			{
				set
				{
					base.PowerSharpParameters["StartTime"] = value;
				}
			}

			// Token: 0x170023D6 RID: 9174
			// (set) Token: 0x06004114 RID: 16660 RVA: 0x0006C316 File Offset: 0x0006A516
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170023D7 RID: 9175
			// (set) Token: 0x06004115 RID: 16661 RVA: 0x0006C32E File Offset: 0x0006A52E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170023D8 RID: 9176
			// (set) Token: 0x06004116 RID: 16662 RVA: 0x0006C346 File Offset: 0x0006A546
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170023D9 RID: 9177
			// (set) Token: 0x06004117 RID: 16663 RVA: 0x0006C35E File Offset: 0x0006A55E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170023DA RID: 9178
			// (set) Token: 0x06004118 RID: 16664 RVA: 0x0006C376 File Offset: 0x0006A576
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
