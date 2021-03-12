using System;
using System.Management.Automation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000634 RID: 1588
	public class GetExchangeServerAccessLicenseUserCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x060050AD RID: 20653 RVA: 0x0007FC94 File Offset: 0x0007DE94
		private GetExchangeServerAccessLicenseUserCommand() : base("Get-ExchangeServerAccessLicenseUser")
		{
		}

		// Token: 0x060050AE RID: 20654 RVA: 0x0007FCA1 File Offset: 0x0007DEA1
		public GetExchangeServerAccessLicenseUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060050AF RID: 20655 RVA: 0x0007FCB0 File Offset: 0x0007DEB0
		public virtual GetExchangeServerAccessLicenseUserCommand SetParameters(GetExchangeServerAccessLicenseUserCommand.LicenseNameParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000635 RID: 1589
		public class LicenseNameParameters : ParametersBase
		{
			// Token: 0x17002FFE RID: 12286
			// (set) Token: 0x060050B0 RID: 20656 RVA: 0x0007FCBA File Offset: 0x0007DEBA
			public virtual string LicenseName
			{
				set
				{
					base.PowerSharpParameters["LicenseName"] = value;
				}
			}

			// Token: 0x17002FFF RID: 12287
			// (set) Token: 0x060050B1 RID: 20657 RVA: 0x0007FCCD File Offset: 0x0007DECD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003000 RID: 12288
			// (set) Token: 0x060050B2 RID: 20658 RVA: 0x0007FCE5 File Offset: 0x0007DEE5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003001 RID: 12289
			// (set) Token: 0x060050B3 RID: 20659 RVA: 0x0007FCFD File Offset: 0x0007DEFD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003002 RID: 12290
			// (set) Token: 0x060050B4 RID: 20660 RVA: 0x0007FD15 File Offset: 0x0007DF15
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
