using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200074C RID: 1868
	public class GetIPAllowListConfigCommand : SyntheticCommandWithPipelineInput<IPAllowListConfig, IPAllowListConfig>
	{
		// Token: 0x06005F87 RID: 24455 RVA: 0x0009388F File Offset: 0x00091A8F
		private GetIPAllowListConfigCommand() : base("Get-IPAllowListConfig")
		{
		}

		// Token: 0x06005F88 RID: 24456 RVA: 0x0009389C File Offset: 0x00091A9C
		public GetIPAllowListConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005F89 RID: 24457 RVA: 0x000938AB File Offset: 0x00091AAB
		public virtual GetIPAllowListConfigCommand SetParameters(GetIPAllowListConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200074D RID: 1869
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003CA8 RID: 15528
			// (set) Token: 0x06005F8A RID: 24458 RVA: 0x000938B5 File Offset: 0x00091AB5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003CA9 RID: 15529
			// (set) Token: 0x06005F8B RID: 24459 RVA: 0x000938C8 File Offset: 0x00091AC8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003CAA RID: 15530
			// (set) Token: 0x06005F8C RID: 24460 RVA: 0x000938E0 File Offset: 0x00091AE0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003CAB RID: 15531
			// (set) Token: 0x06005F8D RID: 24461 RVA: 0x000938F8 File Offset: 0x00091AF8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003CAC RID: 15532
			// (set) Token: 0x06005F8E RID: 24462 RVA: 0x00093910 File Offset: 0x00091B10
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
