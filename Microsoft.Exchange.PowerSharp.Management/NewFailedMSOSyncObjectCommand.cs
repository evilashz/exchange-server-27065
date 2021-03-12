using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200012A RID: 298
	public class NewFailedMSOSyncObjectCommand : SyntheticCommandWithPipelineInputNoOutput<SyncObjectId>
	{
		// Token: 0x06001FDC RID: 8156 RVA: 0x00040FF6 File Offset: 0x0003F1F6
		private NewFailedMSOSyncObjectCommand() : base("New-FailedMSOSyncObject")
		{
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x00041003 File Offset: 0x0003F203
		public NewFailedMSOSyncObjectCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x00041012 File Offset: 0x0003F212
		public virtual NewFailedMSOSyncObjectCommand SetParameters(NewFailedMSOSyncObjectCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200012B RID: 299
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000941 RID: 2369
			// (set) Token: 0x06001FDF RID: 8159 RVA: 0x0004101C File Offset: 0x0003F21C
			public virtual SyncObjectId ObjectId
			{
				set
				{
					base.PowerSharpParameters["ObjectId"] = value;
				}
			}

			// Token: 0x17000942 RID: 2370
			// (set) Token: 0x06001FE0 RID: 8160 RVA: 0x0004102F File Offset: 0x0003F22F
			public virtual ServiceInstanceId ServiceInstanceId
			{
				set
				{
					base.PowerSharpParameters["ServiceInstanceId"] = value;
				}
			}

			// Token: 0x17000943 RID: 2371
			// (set) Token: 0x06001FE1 RID: 8161 RVA: 0x00041042 File Offset: 0x0003F242
			public virtual bool IsTemporary
			{
				set
				{
					base.PowerSharpParameters["IsTemporary"] = value;
				}
			}

			// Token: 0x17000944 RID: 2372
			// (set) Token: 0x06001FE2 RID: 8162 RVA: 0x0004105A File Offset: 0x0003F25A
			public virtual bool IsIncrementalOnly
			{
				set
				{
					base.PowerSharpParameters["IsIncrementalOnly"] = value;
				}
			}

			// Token: 0x17000945 RID: 2373
			// (set) Token: 0x06001FE3 RID: 8163 RVA: 0x00041072 File Offset: 0x0003F272
			public virtual bool IsLinkRelated
			{
				set
				{
					base.PowerSharpParameters["IsLinkRelated"] = value;
				}
			}

			// Token: 0x17000946 RID: 2374
			// (set) Token: 0x06001FE4 RID: 8164 RVA: 0x0004108A File Offset: 0x0003F28A
			public virtual bool IsTenantWideDivergence
			{
				set
				{
					base.PowerSharpParameters["IsTenantWideDivergence"] = value;
				}
			}

			// Token: 0x17000947 RID: 2375
			// (set) Token: 0x06001FE5 RID: 8165 RVA: 0x000410A2 File Offset: 0x0003F2A2
			public virtual bool IsValidationDivergence
			{
				set
				{
					base.PowerSharpParameters["IsValidationDivergence"] = value;
				}
			}

			// Token: 0x17000948 RID: 2376
			// (set) Token: 0x06001FE6 RID: 8166 RVA: 0x000410BA File Offset: 0x0003F2BA
			public virtual bool IsRetriable
			{
				set
				{
					base.PowerSharpParameters["IsRetriable"] = value;
				}
			}

			// Token: 0x17000949 RID: 2377
			// (set) Token: 0x06001FE7 RID: 8167 RVA: 0x000410D2 File Offset: 0x0003F2D2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700094A RID: 2378
			// (set) Token: 0x06001FE8 RID: 8168 RVA: 0x000410EA File Offset: 0x0003F2EA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700094B RID: 2379
			// (set) Token: 0x06001FE9 RID: 8169 RVA: 0x00041102 File Offset: 0x0003F302
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700094C RID: 2380
			// (set) Token: 0x06001FEA RID: 8170 RVA: 0x0004111A File Offset: 0x0003F31A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700094D RID: 2381
			// (set) Token: 0x06001FEB RID: 8171 RVA: 0x00041132 File Offset: 0x0003F332
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
