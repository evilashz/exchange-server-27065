using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000113 RID: 275
	public class GetFailedMSOSyncObjectCommand : SyntheticCommandWithPipelineInput<FailedMSOSyncObject, FailedMSOSyncObject>
	{
		// Token: 0x06001F3D RID: 7997 RVA: 0x000403C7 File Offset: 0x0003E5C7
		private GetFailedMSOSyncObjectCommand() : base("Get-FailedMSOSyncObject")
		{
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x000403D4 File Offset: 0x0003E5D4
		public GetFailedMSOSyncObjectCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x000403E3 File Offset: 0x0003E5E3
		public virtual GetFailedMSOSyncObjectCommand SetParameters(GetFailedMSOSyncObjectCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x000403ED File Offset: 0x0003E5ED
		public virtual GetFailedMSOSyncObjectCommand SetParameters(GetFailedMSOSyncObjectCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000114 RID: 276
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170008D0 RID: 2256
			// (set) Token: 0x06001F41 RID: 8001 RVA: 0x000403F7 File Offset: 0x0003E5F7
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170008D1 RID: 2257
			// (set) Token: 0x06001F42 RID: 8002 RVA: 0x0004040A File Offset: 0x0003E60A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170008D2 RID: 2258
			// (set) Token: 0x06001F43 RID: 8003 RVA: 0x00040422 File Offset: 0x0003E622
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170008D3 RID: 2259
			// (set) Token: 0x06001F44 RID: 8004 RVA: 0x0004043A File Offset: 0x0003E63A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170008D4 RID: 2260
			// (set) Token: 0x06001F45 RID: 8005 RVA: 0x00040452 File Offset: 0x0003E652
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000115 RID: 277
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170008D5 RID: 2261
			// (set) Token: 0x06001F47 RID: 8007 RVA: 0x00040472 File Offset: 0x0003E672
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new FailedMSOSyncObjectIdParameter(value) : null);
				}
			}

			// Token: 0x170008D6 RID: 2262
			// (set) Token: 0x06001F48 RID: 8008 RVA: 0x00040490 File Offset: 0x0003E690
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170008D7 RID: 2263
			// (set) Token: 0x06001F49 RID: 8009 RVA: 0x000404A3 File Offset: 0x0003E6A3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170008D8 RID: 2264
			// (set) Token: 0x06001F4A RID: 8010 RVA: 0x000404BB File Offset: 0x0003E6BB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170008D9 RID: 2265
			// (set) Token: 0x06001F4B RID: 8011 RVA: 0x000404D3 File Offset: 0x0003E6D3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170008DA RID: 2266
			// (set) Token: 0x06001F4C RID: 8012 RVA: 0x000404EB File Offset: 0x0003E6EB
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
