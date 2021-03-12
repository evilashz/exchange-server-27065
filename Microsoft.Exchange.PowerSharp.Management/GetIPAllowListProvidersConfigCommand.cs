using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000753 RID: 1875
	public class GetIPAllowListProvidersConfigCommand : SyntheticCommandWithPipelineInput<IPAllowListProviderConfig, IPAllowListProviderConfig>
	{
		// Token: 0x06005FAE RID: 24494 RVA: 0x00093B75 File Offset: 0x00091D75
		private GetIPAllowListProvidersConfigCommand() : base("Get-IPAllowListProvidersConfig")
		{
		}

		// Token: 0x06005FAF RID: 24495 RVA: 0x00093B82 File Offset: 0x00091D82
		public GetIPAllowListProvidersConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005FB0 RID: 24496 RVA: 0x00093B91 File Offset: 0x00091D91
		public virtual GetIPAllowListProvidersConfigCommand SetParameters(GetIPAllowListProvidersConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000754 RID: 1876
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003CC1 RID: 15553
			// (set) Token: 0x06005FB1 RID: 24497 RVA: 0x00093B9B File Offset: 0x00091D9B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003CC2 RID: 15554
			// (set) Token: 0x06005FB2 RID: 24498 RVA: 0x00093BAE File Offset: 0x00091DAE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003CC3 RID: 15555
			// (set) Token: 0x06005FB3 RID: 24499 RVA: 0x00093BC6 File Offset: 0x00091DC6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003CC4 RID: 15556
			// (set) Token: 0x06005FB4 RID: 24500 RVA: 0x00093BDE File Offset: 0x00091DDE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003CC5 RID: 15557
			// (set) Token: 0x06005FB5 RID: 24501 RVA: 0x00093BF6 File Offset: 0x00091DF6
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
