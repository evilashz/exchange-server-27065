using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200003B RID: 59
	public class GetActiveSyncOrganizationSettingsCommand : SyntheticCommandWithPipelineInput<ActiveSyncOrganizationSettings, ActiveSyncOrganizationSettings>
	{
		// Token: 0x06001612 RID: 5650 RVA: 0x000345CB File Offset: 0x000327CB
		private GetActiveSyncOrganizationSettingsCommand() : base("Get-ActiveSyncOrganizationSettings")
		{
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x000345D8 File Offset: 0x000327D8
		public GetActiveSyncOrganizationSettingsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x000345E7 File Offset: 0x000327E7
		public virtual GetActiveSyncOrganizationSettingsCommand SetParameters(GetActiveSyncOrganizationSettingsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x000345F1 File Offset: 0x000327F1
		public virtual GetActiveSyncOrganizationSettingsCommand SetParameters(GetActiveSyncOrganizationSettingsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200003C RID: 60
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000155 RID: 341
			// (set) Token: 0x06001616 RID: 5654 RVA: 0x000345FB File Offset: 0x000327FB
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000156 RID: 342
			// (set) Token: 0x06001617 RID: 5655 RVA: 0x00034619 File Offset: 0x00032819
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000157 RID: 343
			// (set) Token: 0x06001618 RID: 5656 RVA: 0x0003462C File Offset: 0x0003282C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000158 RID: 344
			// (set) Token: 0x06001619 RID: 5657 RVA: 0x00034644 File Offset: 0x00032844
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000159 RID: 345
			// (set) Token: 0x0600161A RID: 5658 RVA: 0x0003465C File Offset: 0x0003285C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700015A RID: 346
			// (set) Token: 0x0600161B RID: 5659 RVA: 0x00034674 File Offset: 0x00032874
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200003D RID: 61
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700015B RID: 347
			// (set) Token: 0x0600161D RID: 5661 RVA: 0x00034694 File Offset: 0x00032894
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ActiveSyncOrganizationSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x1700015C RID: 348
			// (set) Token: 0x0600161E RID: 5662 RVA: 0x000346B2 File Offset: 0x000328B2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700015D RID: 349
			// (set) Token: 0x0600161F RID: 5663 RVA: 0x000346D0 File Offset: 0x000328D0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700015E RID: 350
			// (set) Token: 0x06001620 RID: 5664 RVA: 0x000346E3 File Offset: 0x000328E3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700015F RID: 351
			// (set) Token: 0x06001621 RID: 5665 RVA: 0x000346FB File Offset: 0x000328FB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000160 RID: 352
			// (set) Token: 0x06001622 RID: 5666 RVA: 0x00034713 File Offset: 0x00032913
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000161 RID: 353
			// (set) Token: 0x06001623 RID: 5667 RVA: 0x0003472B File Offset: 0x0003292B
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
