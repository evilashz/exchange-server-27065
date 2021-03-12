using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.UM.UMPhoneSession;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B9F RID: 2975
	public class StopUMPhoneSessionCommand : SyntheticCommandWithPipelineInput<UMPhoneSession, UMPhoneSession>
	{
		// Token: 0x06009038 RID: 36920 RVA: 0x000D2ED4 File Offset: 0x000D10D4
		private StopUMPhoneSessionCommand() : base("Stop-UMPhoneSession")
		{
		}

		// Token: 0x06009039 RID: 36921 RVA: 0x000D2EE1 File Offset: 0x000D10E1
		public StopUMPhoneSessionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600903A RID: 36922 RVA: 0x000D2EF0 File Offset: 0x000D10F0
		public virtual StopUMPhoneSessionCommand SetParameters(StopUMPhoneSessionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600903B RID: 36923 RVA: 0x000D2EFA File Offset: 0x000D10FA
		public virtual StopUMPhoneSessionCommand SetParameters(StopUMPhoneSessionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BA0 RID: 2976
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170064B3 RID: 25779
			// (set) Token: 0x0600903C RID: 36924 RVA: 0x000D2F04 File Offset: 0x000D1104
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170064B4 RID: 25780
			// (set) Token: 0x0600903D RID: 36925 RVA: 0x000D2F17 File Offset: 0x000D1117
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170064B5 RID: 25781
			// (set) Token: 0x0600903E RID: 36926 RVA: 0x000D2F2F File Offset: 0x000D112F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170064B6 RID: 25782
			// (set) Token: 0x0600903F RID: 36927 RVA: 0x000D2F47 File Offset: 0x000D1147
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170064B7 RID: 25783
			// (set) Token: 0x06009040 RID: 36928 RVA: 0x000D2F5F File Offset: 0x000D115F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170064B8 RID: 25784
			// (set) Token: 0x06009041 RID: 36929 RVA: 0x000D2F77 File Offset: 0x000D1177
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170064B9 RID: 25785
			// (set) Token: 0x06009042 RID: 36930 RVA: 0x000D2F8F File Offset: 0x000D118F
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000BA1 RID: 2977
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170064BA RID: 25786
			// (set) Token: 0x06009044 RID: 36932 RVA: 0x000D2FAF File Offset: 0x000D11AF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMPhoneSessionIdentityParameter(value) : null);
				}
			}

			// Token: 0x170064BB RID: 25787
			// (set) Token: 0x06009045 RID: 36933 RVA: 0x000D2FCD File Offset: 0x000D11CD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170064BC RID: 25788
			// (set) Token: 0x06009046 RID: 36934 RVA: 0x000D2FE0 File Offset: 0x000D11E0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170064BD RID: 25789
			// (set) Token: 0x06009047 RID: 36935 RVA: 0x000D2FF8 File Offset: 0x000D11F8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170064BE RID: 25790
			// (set) Token: 0x06009048 RID: 36936 RVA: 0x000D3010 File Offset: 0x000D1210
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170064BF RID: 25791
			// (set) Token: 0x06009049 RID: 36937 RVA: 0x000D3028 File Offset: 0x000D1228
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170064C0 RID: 25792
			// (set) Token: 0x0600904A RID: 36938 RVA: 0x000D3040 File Offset: 0x000D1240
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170064C1 RID: 25793
			// (set) Token: 0x0600904B RID: 36939 RVA: 0x000D3058 File Offset: 0x000D1258
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
