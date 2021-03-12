using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000135 RID: 309
	public class RemoveMSOFullSyncObjectRequestCommand : SyntheticCommandWithPipelineInput<FullSyncObjectRequest, FullSyncObjectRequest>
	{
		// Token: 0x0600202E RID: 8238 RVA: 0x0004165E File Offset: 0x0003F85E
		private RemoveMSOFullSyncObjectRequestCommand() : base("Remove-MSOFullSyncObjectRequest")
		{
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x0004166B File Offset: 0x0003F86B
		public RemoveMSOFullSyncObjectRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x0004167A File Offset: 0x0003F87A
		public virtual RemoveMSOFullSyncObjectRequestCommand SetParameters(RemoveMSOFullSyncObjectRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000136 RID: 310
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700097D RID: 2429
			// (set) Token: 0x06002031 RID: 8241 RVA: 0x00041684 File Offset: 0x0003F884
			public virtual SyncObjectId Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700097E RID: 2430
			// (set) Token: 0x06002032 RID: 8242 RVA: 0x00041697 File Offset: 0x0003F897
			public virtual ServiceInstanceId ServiceInstanceId
			{
				set
				{
					base.PowerSharpParameters["ServiceInstanceId"] = value;
				}
			}

			// Token: 0x1700097F RID: 2431
			// (set) Token: 0x06002033 RID: 8243 RVA: 0x000416AA File Offset: 0x0003F8AA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000980 RID: 2432
			// (set) Token: 0x06002034 RID: 8244 RVA: 0x000416C2 File Offset: 0x0003F8C2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000981 RID: 2433
			// (set) Token: 0x06002035 RID: 8245 RVA: 0x000416DA File Offset: 0x0003F8DA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000982 RID: 2434
			// (set) Token: 0x06002036 RID: 8246 RVA: 0x000416F2 File Offset: 0x0003F8F2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000983 RID: 2435
			// (set) Token: 0x06002037 RID: 8247 RVA: 0x0004170A File Offset: 0x0003F90A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000984 RID: 2436
			// (set) Token: 0x06002038 RID: 8248 RVA: 0x00041722 File Offset: 0x0003F922
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
