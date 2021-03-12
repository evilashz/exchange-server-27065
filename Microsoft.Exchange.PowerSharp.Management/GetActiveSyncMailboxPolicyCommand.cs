using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200019F RID: 415
	public class GetActiveSyncMailboxPolicyCommand : SyntheticCommandWithPipelineInput<ActiveSyncMailboxPolicy, ActiveSyncMailboxPolicy>
	{
		// Token: 0x06002405 RID: 9221 RVA: 0x0004642F File Offset: 0x0004462F
		private GetActiveSyncMailboxPolicyCommand() : base("Get-ActiveSyncMailboxPolicy")
		{
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x0004643C File Offset: 0x0004463C
		public GetActiveSyncMailboxPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x0004644B File Offset: 0x0004464B
		public virtual GetActiveSyncMailboxPolicyCommand SetParameters(GetActiveSyncMailboxPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x00046455 File Offset: 0x00044655
		public virtual GetActiveSyncMailboxPolicyCommand SetParameters(GetActiveSyncMailboxPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001A0 RID: 416
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000C80 RID: 3200
			// (set) Token: 0x06002409 RID: 9225 RVA: 0x0004645F File Offset: 0x0004465F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000C81 RID: 3201
			// (set) Token: 0x0600240A RID: 9226 RVA: 0x0004647D File Offset: 0x0004467D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000C82 RID: 3202
			// (set) Token: 0x0600240B RID: 9227 RVA: 0x00046490 File Offset: 0x00044690
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000C83 RID: 3203
			// (set) Token: 0x0600240C RID: 9228 RVA: 0x000464A8 File Offset: 0x000446A8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000C84 RID: 3204
			// (set) Token: 0x0600240D RID: 9229 RVA: 0x000464C0 File Offset: 0x000446C0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000C85 RID: 3205
			// (set) Token: 0x0600240E RID: 9230 RVA: 0x000464D8 File Offset: 0x000446D8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020001A1 RID: 417
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000C86 RID: 3206
			// (set) Token: 0x06002410 RID: 9232 RVA: 0x000464F8 File Offset: 0x000446F8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000C87 RID: 3207
			// (set) Token: 0x06002411 RID: 9233 RVA: 0x00046516 File Offset: 0x00044716
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000C88 RID: 3208
			// (set) Token: 0x06002412 RID: 9234 RVA: 0x00046534 File Offset: 0x00044734
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000C89 RID: 3209
			// (set) Token: 0x06002413 RID: 9235 RVA: 0x00046547 File Offset: 0x00044747
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000C8A RID: 3210
			// (set) Token: 0x06002414 RID: 9236 RVA: 0x0004655F File Offset: 0x0004475F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000C8B RID: 3211
			// (set) Token: 0x06002415 RID: 9237 RVA: 0x00046577 File Offset: 0x00044777
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000C8C RID: 3212
			// (set) Token: 0x06002416 RID: 9238 RVA: 0x0004658F File Offset: 0x0004478F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
