using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Aggregation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E99 RID: 3737
	public class SetRemoteAccountSyncCacheCommand : SyntheticCommandWithPipelineInputNoOutput<SubscriptionsCache>
	{
		// Token: 0x0600DBCB RID: 56267 RVA: 0x00137B4E File Offset: 0x00135D4E
		private SetRemoteAccountSyncCacheCommand() : base("Set-RemoteAccountSyncCache")
		{
		}

		// Token: 0x0600DBCC RID: 56268 RVA: 0x00137B5B File Offset: 0x00135D5B
		public SetRemoteAccountSyncCacheCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600DBCD RID: 56269 RVA: 0x00137B6A File Offset: 0x00135D6A
		public virtual SetRemoteAccountSyncCacheCommand SetParameters(SetRemoteAccountSyncCacheCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DBCE RID: 56270 RVA: 0x00137B74 File Offset: 0x00135D74
		public virtual SetRemoteAccountSyncCacheCommand SetParameters(SetRemoteAccountSyncCacheCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E9A RID: 3738
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700AA52 RID: 43602
			// (set) Token: 0x0600DBCF RID: 56271 RVA: 0x00137B7E File Offset: 0x00135D7E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AA53 RID: 43603
			// (set) Token: 0x0600DBD0 RID: 56272 RVA: 0x00137B91 File Offset: 0x00135D91
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AA54 RID: 43604
			// (set) Token: 0x0600DBD1 RID: 56273 RVA: 0x00137BA9 File Offset: 0x00135DA9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AA55 RID: 43605
			// (set) Token: 0x0600DBD2 RID: 56274 RVA: 0x00137BC1 File Offset: 0x00135DC1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AA56 RID: 43606
			// (set) Token: 0x0600DBD3 RID: 56275 RVA: 0x00137BD9 File Offset: 0x00135DD9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AA57 RID: 43607
			// (set) Token: 0x0600DBD4 RID: 56276 RVA: 0x00137BF1 File Offset: 0x00135DF1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E9B RID: 3739
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700AA58 RID: 43608
			// (set) Token: 0x0600DBD6 RID: 56278 RVA: 0x00137C11 File Offset: 0x00135E11
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new CacheIdParameter(value) : null);
				}
			}

			// Token: 0x1700AA59 RID: 43609
			// (set) Token: 0x0600DBD7 RID: 56279 RVA: 0x00137C2F File Offset: 0x00135E2F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AA5A RID: 43610
			// (set) Token: 0x0600DBD8 RID: 56280 RVA: 0x00137C42 File Offset: 0x00135E42
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AA5B RID: 43611
			// (set) Token: 0x0600DBD9 RID: 56281 RVA: 0x00137C5A File Offset: 0x00135E5A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AA5C RID: 43612
			// (set) Token: 0x0600DBDA RID: 56282 RVA: 0x00137C72 File Offset: 0x00135E72
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AA5D RID: 43613
			// (set) Token: 0x0600DBDB RID: 56283 RVA: 0x00137C8A File Offset: 0x00135E8A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AA5E RID: 43614
			// (set) Token: 0x0600DBDC RID: 56284 RVA: 0x00137CA2 File Offset: 0x00135EA2
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
