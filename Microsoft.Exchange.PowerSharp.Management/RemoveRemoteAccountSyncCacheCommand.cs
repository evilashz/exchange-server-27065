using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Aggregation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E79 RID: 3705
	public class RemoveRemoteAccountSyncCacheCommand : SyntheticCommandWithPipelineInput<SubscriptionsCache, SubscriptionsCache>
	{
		// Token: 0x0600DA99 RID: 55961 RVA: 0x00136215 File Offset: 0x00134415
		private RemoveRemoteAccountSyncCacheCommand() : base("Remove-RemoteAccountSyncCache")
		{
		}

		// Token: 0x0600DA9A RID: 55962 RVA: 0x00136222 File Offset: 0x00134422
		public RemoveRemoteAccountSyncCacheCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600DA9B RID: 55963 RVA: 0x00136231 File Offset: 0x00134431
		public virtual RemoveRemoteAccountSyncCacheCommand SetParameters(RemoveRemoteAccountSyncCacheCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DA9C RID: 55964 RVA: 0x0013623B File Offset: 0x0013443B
		public virtual RemoveRemoteAccountSyncCacheCommand SetParameters(RemoveRemoteAccountSyncCacheCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E7A RID: 3706
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A960 RID: 43360
			// (set) Token: 0x0600DA9D RID: 55965 RVA: 0x00136245 File Offset: 0x00134445
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A961 RID: 43361
			// (set) Token: 0x0600DA9E RID: 55966 RVA: 0x00136258 File Offset: 0x00134458
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A962 RID: 43362
			// (set) Token: 0x0600DA9F RID: 55967 RVA: 0x00136270 File Offset: 0x00134470
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A963 RID: 43363
			// (set) Token: 0x0600DAA0 RID: 55968 RVA: 0x00136288 File Offset: 0x00134488
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A964 RID: 43364
			// (set) Token: 0x0600DAA1 RID: 55969 RVA: 0x001362A0 File Offset: 0x001344A0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A965 RID: 43365
			// (set) Token: 0x0600DAA2 RID: 55970 RVA: 0x001362B8 File Offset: 0x001344B8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A966 RID: 43366
			// (set) Token: 0x0600DAA3 RID: 55971 RVA: 0x001362D0 File Offset: 0x001344D0
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000E7B RID: 3707
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A967 RID: 43367
			// (set) Token: 0x0600DAA5 RID: 55973 RVA: 0x001362F0 File Offset: 0x001344F0
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new CacheIdParameter(value) : null);
				}
			}

			// Token: 0x1700A968 RID: 43368
			// (set) Token: 0x0600DAA6 RID: 55974 RVA: 0x0013630E File Offset: 0x0013450E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A969 RID: 43369
			// (set) Token: 0x0600DAA7 RID: 55975 RVA: 0x00136321 File Offset: 0x00134521
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A96A RID: 43370
			// (set) Token: 0x0600DAA8 RID: 55976 RVA: 0x00136339 File Offset: 0x00134539
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A96B RID: 43371
			// (set) Token: 0x0600DAA9 RID: 55977 RVA: 0x00136351 File Offset: 0x00134551
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A96C RID: 43372
			// (set) Token: 0x0600DAAA RID: 55978 RVA: 0x00136369 File Offset: 0x00134569
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A96D RID: 43373
			// (set) Token: 0x0600DAAB RID: 55979 RVA: 0x00136381 File Offset: 0x00134581
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A96E RID: 43374
			// (set) Token: 0x0600DAAC RID: 55980 RVA: 0x00136399 File Offset: 0x00134599
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
