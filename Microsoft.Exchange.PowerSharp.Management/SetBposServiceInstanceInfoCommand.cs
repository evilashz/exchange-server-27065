using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200013E RID: 318
	public class SetBposServiceInstanceInfoCommand : SyntheticCommandWithPipelineInputNoOutput<ServiceInstanceId>
	{
		// Token: 0x06002064 RID: 8292 RVA: 0x00041A65 File Offset: 0x0003FC65
		private SetBposServiceInstanceInfoCommand() : base("Set-BposServiceInstanceInfo")
		{
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x00041A72 File Offset: 0x0003FC72
		public SetBposServiceInstanceInfoCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x00041A81 File Offset: 0x0003FC81
		public virtual SetBposServiceInstanceInfoCommand SetParameters(SetBposServiceInstanceInfoCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200013F RID: 319
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170009A1 RID: 2465
			// (set) Token: 0x06002067 RID: 8295 RVA: 0x00041A8B File Offset: 0x0003FC8B
			public virtual ServiceInstanceId Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170009A2 RID: 2466
			// (set) Token: 0x06002068 RID: 8296 RVA: 0x00041A9E File Offset: 0x0003FC9E
			public virtual Uri BackSyncUrl
			{
				set
				{
					base.PowerSharpParameters["BackSyncUrl"] = value;
				}
			}

			// Token: 0x170009A3 RID: 2467
			// (set) Token: 0x06002069 RID: 8297 RVA: 0x00041AB1 File Offset: 0x0003FCB1
			public virtual bool SupportsAuthorityTransfer
			{
				set
				{
					base.PowerSharpParameters["SupportsAuthorityTransfer"] = value;
				}
			}

			// Token: 0x170009A4 RID: 2468
			// (set) Token: 0x0600206A RID: 8298 RVA: 0x00041AC9 File Offset: 0x0003FCC9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170009A5 RID: 2469
			// (set) Token: 0x0600206B RID: 8299 RVA: 0x00041AE1 File Offset: 0x0003FCE1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170009A6 RID: 2470
			// (set) Token: 0x0600206C RID: 8300 RVA: 0x00041AF9 File Offset: 0x0003FCF9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170009A7 RID: 2471
			// (set) Token: 0x0600206D RID: 8301 RVA: 0x00041B11 File Offset: 0x0003FD11
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170009A8 RID: 2472
			// (set) Token: 0x0600206E RID: 8302 RVA: 0x00041B29 File Offset: 0x0003FD29
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
