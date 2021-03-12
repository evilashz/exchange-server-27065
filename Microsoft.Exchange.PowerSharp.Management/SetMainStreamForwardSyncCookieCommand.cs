using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000143 RID: 323
	public class SetMainStreamForwardSyncCookieCommand : SyntheticCommandWithPipelineInputNoOutput<ServiceInstanceId>
	{
		// Token: 0x06002087 RID: 8327 RVA: 0x00041D1D File Offset: 0x0003FF1D
		private SetMainStreamForwardSyncCookieCommand() : base("Set-MainStreamForwardSyncCookie")
		{
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x00041D2A File Offset: 0x0003FF2A
		public SetMainStreamForwardSyncCookieCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x00041D39 File Offset: 0x0003FF39
		public virtual SetMainStreamForwardSyncCookieCommand SetParameters(SetMainStreamForwardSyncCookieCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000144 RID: 324
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170009BA RID: 2490
			// (set) Token: 0x0600208A RID: 8330 RVA: 0x00041D43 File Offset: 0x0003FF43
			public virtual ServiceInstanceId ServiceInstanceId
			{
				set
				{
					base.PowerSharpParameters["ServiceInstanceId"] = value;
				}
			}

			// Token: 0x170009BB RID: 2491
			// (set) Token: 0x0600208B RID: 8331 RVA: 0x00041D56 File Offset: 0x0003FF56
			public virtual int RollbackTimeIntervalMinutes
			{
				set
				{
					base.PowerSharpParameters["RollbackTimeIntervalMinutes"] = value;
				}
			}

			// Token: 0x170009BC RID: 2492
			// (set) Token: 0x0600208C RID: 8332 RVA: 0x00041D6E File Offset: 0x0003FF6E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170009BD RID: 2493
			// (set) Token: 0x0600208D RID: 8333 RVA: 0x00041D86 File Offset: 0x0003FF86
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170009BE RID: 2494
			// (set) Token: 0x0600208E RID: 8334 RVA: 0x00041D9E File Offset: 0x0003FF9E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170009BF RID: 2495
			// (set) Token: 0x0600208F RID: 8335 RVA: 0x00041DB6 File Offset: 0x0003FFB6
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
