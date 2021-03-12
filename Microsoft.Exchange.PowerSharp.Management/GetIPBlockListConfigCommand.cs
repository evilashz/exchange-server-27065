using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200075F RID: 1887
	public class GetIPBlockListConfigCommand : SyntheticCommandWithPipelineInput<IPBlockListConfig, IPBlockListConfig>
	{
		// Token: 0x0600600B RID: 24587 RVA: 0x000942A4 File Offset: 0x000924A4
		private GetIPBlockListConfigCommand() : base("Get-IPBlockListConfig")
		{
		}

		// Token: 0x0600600C RID: 24588 RVA: 0x000942B1 File Offset: 0x000924B1
		public GetIPBlockListConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600600D RID: 24589 RVA: 0x000942C0 File Offset: 0x000924C0
		public virtual GetIPBlockListConfigCommand SetParameters(GetIPBlockListConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000760 RID: 1888
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003D06 RID: 15622
			// (set) Token: 0x0600600E RID: 24590 RVA: 0x000942CA File Offset: 0x000924CA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003D07 RID: 15623
			// (set) Token: 0x0600600F RID: 24591 RVA: 0x000942DD File Offset: 0x000924DD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003D08 RID: 15624
			// (set) Token: 0x06006010 RID: 24592 RVA: 0x000942F5 File Offset: 0x000924F5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003D09 RID: 15625
			// (set) Token: 0x06006011 RID: 24593 RVA: 0x0009430D File Offset: 0x0009250D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003D0A RID: 15626
			// (set) Token: 0x06006012 RID: 24594 RVA: 0x00094325 File Offset: 0x00092525
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
