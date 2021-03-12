using System;
using System.Management.Automation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200007B RID: 123
	public class SetInstallPathInAppConfigCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x0600185B RID: 6235 RVA: 0x00037453 File Offset: 0x00035653
		private SetInstallPathInAppConfigCommand() : base("Set-InstallPathInAppConfig")
		{
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00037460 File Offset: 0x00035660
		public SetInstallPathInAppConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x0003746F File Offset: 0x0003566F
		public virtual SetInstallPathInAppConfigCommand SetParameters(SetInstallPathInAppConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200007C RID: 124
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700031E RID: 798
			// (set) Token: 0x0600185E RID: 6238 RVA: 0x00037479 File Offset: 0x00035679
			public virtual string ExchangeInstallPath
			{
				set
				{
					base.PowerSharpParameters["ExchangeInstallPath"] = value;
				}
			}

			// Token: 0x1700031F RID: 799
			// (set) Token: 0x0600185F RID: 6239 RVA: 0x0003748C File Offset: 0x0003568C
			public virtual string ReplacementString
			{
				set
				{
					base.PowerSharpParameters["ReplacementString"] = value;
				}
			}

			// Token: 0x17000320 RID: 800
			// (set) Token: 0x06001860 RID: 6240 RVA: 0x0003749F File Offset: 0x0003569F
			public virtual string ConfigFileAbsolutePath
			{
				set
				{
					base.PowerSharpParameters["ConfigFileAbsolutePath"] = value;
				}
			}

			// Token: 0x17000321 RID: 801
			// (set) Token: 0x06001861 RID: 6241 RVA: 0x000374B2 File Offset: 0x000356B2
			public virtual string ConfigFileRelativePath
			{
				set
				{
					base.PowerSharpParameters["ConfigFileRelativePath"] = value;
				}
			}

			// Token: 0x17000322 RID: 802
			// (set) Token: 0x06001862 RID: 6242 RVA: 0x000374C5 File Offset: 0x000356C5
			public virtual string ConfigFileName
			{
				set
				{
					base.PowerSharpParameters["ConfigFileName"] = value;
				}
			}

			// Token: 0x17000323 RID: 803
			// (set) Token: 0x06001863 RID: 6243 RVA: 0x000374D8 File Offset: 0x000356D8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000324 RID: 804
			// (set) Token: 0x06001864 RID: 6244 RVA: 0x000374F0 File Offset: 0x000356F0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000325 RID: 805
			// (set) Token: 0x06001865 RID: 6245 RVA: 0x00037508 File Offset: 0x00035708
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000326 RID: 806
			// (set) Token: 0x06001866 RID: 6246 RVA: 0x00037520 File Offset: 0x00035720
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
