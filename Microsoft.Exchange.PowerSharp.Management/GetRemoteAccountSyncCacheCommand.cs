using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Aggregation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E5B RID: 3675
	public class GetRemoteAccountSyncCacheCommand : SyntheticCommandWithPipelineInput<SubscriptionsCache, SubscriptionsCache>
	{
		// Token: 0x0600D99E RID: 55710 RVA: 0x00134E23 File Offset: 0x00133023
		private GetRemoteAccountSyncCacheCommand() : base("Get-RemoteAccountSyncCache")
		{
		}

		// Token: 0x0600D99F RID: 55711 RVA: 0x00134E30 File Offset: 0x00133030
		public GetRemoteAccountSyncCacheCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D9A0 RID: 55712 RVA: 0x00134E3F File Offset: 0x0013303F
		public virtual GetRemoteAccountSyncCacheCommand SetParameters(GetRemoteAccountSyncCacheCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D9A1 RID: 55713 RVA: 0x00134E49 File Offset: 0x00133049
		public virtual GetRemoteAccountSyncCacheCommand SetParameters(GetRemoteAccountSyncCacheCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E5C RID: 3676
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A8A1 RID: 43169
			// (set) Token: 0x0600D9A2 RID: 55714 RVA: 0x00134E53 File Offset: 0x00133053
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new CacheIdParameter(value) : null);
				}
			}

			// Token: 0x1700A8A2 RID: 43170
			// (set) Token: 0x0600D9A3 RID: 55715 RVA: 0x00134E71 File Offset: 0x00133071
			public virtual bool ValidateCache
			{
				set
				{
					base.PowerSharpParameters["ValidateCache"] = value;
				}
			}

			// Token: 0x1700A8A3 RID: 43171
			// (set) Token: 0x0600D9A4 RID: 55716 RVA: 0x00134E89 File Offset: 0x00133089
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A8A4 RID: 43172
			// (set) Token: 0x0600D9A5 RID: 55717 RVA: 0x00134E9C File Offset: 0x0013309C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A8A5 RID: 43173
			// (set) Token: 0x0600D9A6 RID: 55718 RVA: 0x00134EB4 File Offset: 0x001330B4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A8A6 RID: 43174
			// (set) Token: 0x0600D9A7 RID: 55719 RVA: 0x00134ECC File Offset: 0x001330CC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A8A7 RID: 43175
			// (set) Token: 0x0600D9A8 RID: 55720 RVA: 0x00134EE4 File Offset: 0x001330E4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A8A8 RID: 43176
			// (set) Token: 0x0600D9A9 RID: 55721 RVA: 0x00134EFC File Offset: 0x001330FC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E5D RID: 3677
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A8A9 RID: 43177
			// (set) Token: 0x0600D9AB RID: 55723 RVA: 0x00134F1C File Offset: 0x0013311C
			public virtual bool ValidateCache
			{
				set
				{
					base.PowerSharpParameters["ValidateCache"] = value;
				}
			}

			// Token: 0x1700A8AA RID: 43178
			// (set) Token: 0x0600D9AC RID: 55724 RVA: 0x00134F34 File Offset: 0x00133134
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A8AB RID: 43179
			// (set) Token: 0x0600D9AD RID: 55725 RVA: 0x00134F47 File Offset: 0x00133147
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A8AC RID: 43180
			// (set) Token: 0x0600D9AE RID: 55726 RVA: 0x00134F5F File Offset: 0x0013315F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A8AD RID: 43181
			// (set) Token: 0x0600D9AF RID: 55727 RVA: 0x00134F77 File Offset: 0x00133177
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A8AE RID: 43182
			// (set) Token: 0x0600D9B0 RID: 55728 RVA: 0x00134F8F File Offset: 0x0013318F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A8AF RID: 43183
			// (set) Token: 0x0600D9B1 RID: 55729 RVA: 0x00134FA7 File Offset: 0x001331A7
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
