using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200079F RID: 1951
	public class GetSenderIdConfigCommand : SyntheticCommandWithPipelineInput<SenderIdConfig, SenderIdConfig>
	{
		// Token: 0x0600621A RID: 25114 RVA: 0x00096C0C File Offset: 0x00094E0C
		private GetSenderIdConfigCommand() : base("Get-SenderIdConfig")
		{
		}

		// Token: 0x0600621B RID: 25115 RVA: 0x00096C19 File Offset: 0x00094E19
		public GetSenderIdConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600621C RID: 25116 RVA: 0x00096C28 File Offset: 0x00094E28
		public virtual GetSenderIdConfigCommand SetParameters(GetSenderIdConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007A0 RID: 1952
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003E95 RID: 16021
			// (set) Token: 0x0600621D RID: 25117 RVA: 0x00096C32 File Offset: 0x00094E32
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003E96 RID: 16022
			// (set) Token: 0x0600621E RID: 25118 RVA: 0x00096C45 File Offset: 0x00094E45
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003E97 RID: 16023
			// (set) Token: 0x0600621F RID: 25119 RVA: 0x00096C5D File Offset: 0x00094E5D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003E98 RID: 16024
			// (set) Token: 0x06006220 RID: 25120 RVA: 0x00096C75 File Offset: 0x00094E75
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003E99 RID: 16025
			// (set) Token: 0x06006221 RID: 25121 RVA: 0x00096C8D File Offset: 0x00094E8D
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
