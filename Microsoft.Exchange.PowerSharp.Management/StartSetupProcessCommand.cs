using System;
using System.Management.Automation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000079 RID: 121
	public class StartSetupProcessCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x0600184C RID: 6220 RVA: 0x00037327 File Offset: 0x00035527
		private StartSetupProcessCommand() : base("Start-SetupProcess")
		{
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x00037334 File Offset: 0x00035534
		public StartSetupProcessCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x00037343 File Offset: 0x00035543
		public virtual StartSetupProcessCommand SetParameters(StartSetupProcessCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200007A RID: 122
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000313 RID: 787
			// (set) Token: 0x0600184F RID: 6223 RVA: 0x0003734D File Offset: 0x0003554D
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000314 RID: 788
			// (set) Token: 0x06001850 RID: 6224 RVA: 0x00037360 File Offset: 0x00035560
			public virtual string Args
			{
				set
				{
					base.PowerSharpParameters["Args"] = value;
				}
			}

			// Token: 0x17000315 RID: 789
			// (set) Token: 0x06001851 RID: 6225 RVA: 0x00037373 File Offset: 0x00035573
			public virtual int Timeout
			{
				set
				{
					base.PowerSharpParameters["Timeout"] = value;
				}
			}

			// Token: 0x17000316 RID: 790
			// (set) Token: 0x06001852 RID: 6226 RVA: 0x0003738B File Offset: 0x0003558B
			public virtual int IgnoreExitCode
			{
				set
				{
					base.PowerSharpParameters["IgnoreExitCode"] = value;
				}
			}

			// Token: 0x17000317 RID: 791
			// (set) Token: 0x06001853 RID: 6227 RVA: 0x000373A3 File Offset: 0x000355A3
			public virtual uint RetryCount
			{
				set
				{
					base.PowerSharpParameters["RetryCount"] = value;
				}
			}

			// Token: 0x17000318 RID: 792
			// (set) Token: 0x06001854 RID: 6228 RVA: 0x000373BB File Offset: 0x000355BB
			public virtual uint RetryDelay
			{
				set
				{
					base.PowerSharpParameters["RetryDelay"] = value;
				}
			}

			// Token: 0x17000319 RID: 793
			// (set) Token: 0x06001855 RID: 6229 RVA: 0x000373D3 File Offset: 0x000355D3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700031A RID: 794
			// (set) Token: 0x06001856 RID: 6230 RVA: 0x000373EB File Offset: 0x000355EB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700031B RID: 795
			// (set) Token: 0x06001857 RID: 6231 RVA: 0x00037403 File Offset: 0x00035603
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700031C RID: 796
			// (set) Token: 0x06001858 RID: 6232 RVA: 0x0003741B File Offset: 0x0003561B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700031D RID: 797
			// (set) Token: 0x06001859 RID: 6233 RVA: 0x00037433 File Offset: 0x00035633
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
