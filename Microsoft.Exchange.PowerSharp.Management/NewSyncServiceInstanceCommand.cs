using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000130 RID: 304
	public class NewSyncServiceInstanceCommand : SyntheticCommandWithPipelineInput<SyncServiceInstance, SyncServiceInstance>
	{
		// Token: 0x06002006 RID: 8198 RVA: 0x0004133D File Offset: 0x0003F53D
		private NewSyncServiceInstanceCommand() : base("New-SyncServiceInstance")
		{
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x0004134A File Offset: 0x0003F54A
		public NewSyncServiceInstanceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x00041359 File Offset: 0x0003F559
		public virtual NewSyncServiceInstanceCommand SetParameters(NewSyncServiceInstanceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000131 RID: 305
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700095F RID: 2399
			// (set) Token: 0x06002009 RID: 8201 RVA: 0x00041363 File Offset: 0x0003F563
			public virtual ServiceInstanceId Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000960 RID: 2400
			// (set) Token: 0x0600200A RID: 8202 RVA: 0x00041376 File Offset: 0x0003F576
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17000961 RID: 2401
			// (set) Token: 0x0600200B RID: 8203 RVA: 0x00041389 File Offset: 0x0003F589
			public virtual Version MinVersion
			{
				set
				{
					base.PowerSharpParameters["MinVersion"] = value;
				}
			}

			// Token: 0x17000962 RID: 2402
			// (set) Token: 0x0600200C RID: 8204 RVA: 0x0004139C File Offset: 0x0003F59C
			public virtual Version MaxVersion
			{
				set
				{
					base.PowerSharpParameters["MaxVersion"] = value;
				}
			}

			// Token: 0x17000963 RID: 2403
			// (set) Token: 0x0600200D RID: 8205 RVA: 0x000413AF File Offset: 0x0003F5AF
			public virtual int ActiveInstanceSleepInterval
			{
				set
				{
					base.PowerSharpParameters["ActiveInstanceSleepInterval"] = value;
				}
			}

			// Token: 0x17000964 RID: 2404
			// (set) Token: 0x0600200E RID: 8206 RVA: 0x000413C7 File Offset: 0x0003F5C7
			public virtual int PassiveInstanceSleepInterval
			{
				set
				{
					base.PowerSharpParameters["PassiveInstanceSleepInterval"] = value;
				}
			}

			// Token: 0x17000965 RID: 2405
			// (set) Token: 0x0600200F RID: 8207 RVA: 0x000413DF File Offset: 0x0003F5DF
			public virtual bool IsEnabled
			{
				set
				{
					base.PowerSharpParameters["IsEnabled"] = value;
				}
			}

			// Token: 0x17000966 RID: 2406
			// (set) Token: 0x06002010 RID: 8208 RVA: 0x000413F7 File Offset: 0x0003F5F7
			public virtual Version NewTenantMinVersion
			{
				set
				{
					base.PowerSharpParameters["NewTenantMinVersion"] = value;
				}
			}

			// Token: 0x17000967 RID: 2407
			// (set) Token: 0x06002011 RID: 8209 RVA: 0x0004140A File Offset: 0x0003F60A
			public virtual Version NewTenantMaxVersion
			{
				set
				{
					base.PowerSharpParameters["NewTenantMaxVersion"] = value;
				}
			}

			// Token: 0x17000968 RID: 2408
			// (set) Token: 0x06002012 RID: 8210 RVA: 0x0004141D File Offset: 0x0003F61D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000969 RID: 2409
			// (set) Token: 0x06002013 RID: 8211 RVA: 0x00041430 File Offset: 0x0003F630
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700096A RID: 2410
			// (set) Token: 0x06002014 RID: 8212 RVA: 0x00041448 File Offset: 0x0003F648
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700096B RID: 2411
			// (set) Token: 0x06002015 RID: 8213 RVA: 0x00041460 File Offset: 0x0003F660
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700096C RID: 2412
			// (set) Token: 0x06002016 RID: 8214 RVA: 0x00041478 File Offset: 0x0003F678
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700096D RID: 2413
			// (set) Token: 0x06002017 RID: 8215 RVA: 0x00041490 File Offset: 0x0003F690
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
