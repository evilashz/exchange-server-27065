using System;
using System.Management.Automation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002FD RID: 765
	public class PreserveAppSettingsCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x06003336 RID: 13110 RVA: 0x0005A4D4 File Offset: 0x000586D4
		private PreserveAppSettingsCommand() : base("Preserve-AppSettings")
		{
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x0005A4E1 File Offset: 0x000586E1
		public PreserveAppSettingsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x0005A4F0 File Offset: 0x000586F0
		public virtual PreserveAppSettingsCommand SetParameters(PreserveAppSettingsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002FE RID: 766
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170018F5 RID: 6389
			// (set) Token: 0x06003339 RID: 13113 RVA: 0x0005A4FA File Offset: 0x000586FA
			public virtual string RoleInstallPath
			{
				set
				{
					base.PowerSharpParameters["RoleInstallPath"] = value;
				}
			}

			// Token: 0x170018F6 RID: 6390
			// (set) Token: 0x0600333A RID: 13114 RVA: 0x0005A50D File Offset: 0x0005870D
			public virtual string ConfigFileName
			{
				set
				{
					base.PowerSharpParameters["ConfigFileName"] = value;
				}
			}

			// Token: 0x170018F7 RID: 6391
			// (set) Token: 0x0600333B RID: 13115 RVA: 0x0005A520 File Offset: 0x00058720
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170018F8 RID: 6392
			// (set) Token: 0x0600333C RID: 13116 RVA: 0x0005A538 File Offset: 0x00058738
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170018F9 RID: 6393
			// (set) Token: 0x0600333D RID: 13117 RVA: 0x0005A550 File Offset: 0x00058750
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170018FA RID: 6394
			// (set) Token: 0x0600333E RID: 13118 RVA: 0x0005A568 File Offset: 0x00058768
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
