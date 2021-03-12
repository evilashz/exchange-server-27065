using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200012C RID: 300
	public class NewMSOFullSyncObjectRequestCommand : SyntheticCommandWithPipelineInput<FullSyncObjectRequest, FullSyncObjectRequest>
	{
		// Token: 0x06001FED RID: 8173 RVA: 0x00041152 File Offset: 0x0003F352
		private NewMSOFullSyncObjectRequestCommand() : base("New-MSOFullSyncObjectRequest")
		{
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x0004115F File Offset: 0x0003F35F
		public NewMSOFullSyncObjectRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x0004116E File Offset: 0x0003F36E
		public virtual NewMSOFullSyncObjectRequestCommand SetParameters(NewMSOFullSyncObjectRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200012D RID: 301
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700094E RID: 2382
			// (set) Token: 0x06001FF0 RID: 8176 RVA: 0x00041178 File Offset: 0x0003F378
			public virtual SyncObjectId ObjectId
			{
				set
				{
					base.PowerSharpParameters["ObjectId"] = value;
				}
			}

			// Token: 0x1700094F RID: 2383
			// (set) Token: 0x06001FF1 RID: 8177 RVA: 0x0004118B File Offset: 0x0003F38B
			public virtual ServiceInstanceId ServiceInstanceId
			{
				set
				{
					base.PowerSharpParameters["ServiceInstanceId"] = value;
				}
			}

			// Token: 0x17000950 RID: 2384
			// (set) Token: 0x06001FF2 RID: 8178 RVA: 0x0004119E File Offset: 0x0003F39E
			public virtual FullSyncObjectRequestOptions Options
			{
				set
				{
					base.PowerSharpParameters["Options"] = value;
				}
			}

			// Token: 0x17000951 RID: 2385
			// (set) Token: 0x06001FF3 RID: 8179 RVA: 0x000411B6 File Offset: 0x0003F3B6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000952 RID: 2386
			// (set) Token: 0x06001FF4 RID: 8180 RVA: 0x000411CE File Offset: 0x0003F3CE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000953 RID: 2387
			// (set) Token: 0x06001FF5 RID: 8181 RVA: 0x000411E6 File Offset: 0x0003F3E6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000954 RID: 2388
			// (set) Token: 0x06001FF6 RID: 8182 RVA: 0x000411FE File Offset: 0x0003F3FE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000955 RID: 2389
			// (set) Token: 0x06001FF7 RID: 8183 RVA: 0x00041216 File Offset: 0x0003F416
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
