using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200019C RID: 412
	public class RemoveActiveSyncMailboxPolicyCommand : SyntheticCommandWithPipelineInput<ActiveSyncMailboxPolicy, ActiveSyncMailboxPolicy>
	{
		// Token: 0x060023EE RID: 9198 RVA: 0x0004625B File Offset: 0x0004445B
		private RemoveActiveSyncMailboxPolicyCommand() : base("Remove-ActiveSyncMailboxPolicy")
		{
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x00046268 File Offset: 0x00044468
		public RemoveActiveSyncMailboxPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x00046277 File Offset: 0x00044477
		public virtual RemoveActiveSyncMailboxPolicyCommand SetParameters(RemoveActiveSyncMailboxPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x00046281 File Offset: 0x00044481
		public virtual RemoveActiveSyncMailboxPolicyCommand SetParameters(RemoveActiveSyncMailboxPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200019D RID: 413
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000C6F RID: 3183
			// (set) Token: 0x060023F2 RID: 9202 RVA: 0x0004628B File Offset: 0x0004448B
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000C70 RID: 3184
			// (set) Token: 0x060023F3 RID: 9203 RVA: 0x000462A3 File Offset: 0x000444A3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000C71 RID: 3185
			// (set) Token: 0x060023F4 RID: 9204 RVA: 0x000462B6 File Offset: 0x000444B6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000C72 RID: 3186
			// (set) Token: 0x060023F5 RID: 9205 RVA: 0x000462CE File Offset: 0x000444CE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000C73 RID: 3187
			// (set) Token: 0x060023F6 RID: 9206 RVA: 0x000462E6 File Offset: 0x000444E6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000C74 RID: 3188
			// (set) Token: 0x060023F7 RID: 9207 RVA: 0x000462FE File Offset: 0x000444FE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000C75 RID: 3189
			// (set) Token: 0x060023F8 RID: 9208 RVA: 0x00046316 File Offset: 0x00044516
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000C76 RID: 3190
			// (set) Token: 0x060023F9 RID: 9209 RVA: 0x0004632E File Offset: 0x0004452E
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200019E RID: 414
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000C77 RID: 3191
			// (set) Token: 0x060023FB RID: 9211 RVA: 0x0004634E File Offset: 0x0004454E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000C78 RID: 3192
			// (set) Token: 0x060023FC RID: 9212 RVA: 0x0004636C File Offset: 0x0004456C
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000C79 RID: 3193
			// (set) Token: 0x060023FD RID: 9213 RVA: 0x00046384 File Offset: 0x00044584
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000C7A RID: 3194
			// (set) Token: 0x060023FE RID: 9214 RVA: 0x00046397 File Offset: 0x00044597
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000C7B RID: 3195
			// (set) Token: 0x060023FF RID: 9215 RVA: 0x000463AF File Offset: 0x000445AF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000C7C RID: 3196
			// (set) Token: 0x06002400 RID: 9216 RVA: 0x000463C7 File Offset: 0x000445C7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000C7D RID: 3197
			// (set) Token: 0x06002401 RID: 9217 RVA: 0x000463DF File Offset: 0x000445DF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000C7E RID: 3198
			// (set) Token: 0x06002402 RID: 9218 RVA: 0x000463F7 File Offset: 0x000445F7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000C7F RID: 3199
			// (set) Token: 0x06002403 RID: 9219 RVA: 0x0004640F File Offset: 0x0004460F
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
