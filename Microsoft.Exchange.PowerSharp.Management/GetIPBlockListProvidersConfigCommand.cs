using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000766 RID: 1894
	public class GetIPBlockListProvidersConfigCommand : SyntheticCommandWithPipelineInput<IPBlockListProviderConfig, IPBlockListProviderConfig>
	{
		// Token: 0x06006034 RID: 24628 RVA: 0x000945B0 File Offset: 0x000927B0
		private GetIPBlockListProvidersConfigCommand() : base("Get-IPBlockListProvidersConfig")
		{
		}

		// Token: 0x06006035 RID: 24629 RVA: 0x000945BD File Offset: 0x000927BD
		public GetIPBlockListProvidersConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006036 RID: 24630 RVA: 0x000945CC File Offset: 0x000927CC
		public virtual GetIPBlockListProvidersConfigCommand SetParameters(GetIPBlockListProvidersConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000767 RID: 1895
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003D21 RID: 15649
			// (set) Token: 0x06006037 RID: 24631 RVA: 0x000945D6 File Offset: 0x000927D6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003D22 RID: 15650
			// (set) Token: 0x06006038 RID: 24632 RVA: 0x000945E9 File Offset: 0x000927E9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003D23 RID: 15651
			// (set) Token: 0x06006039 RID: 24633 RVA: 0x00094601 File Offset: 0x00092801
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003D24 RID: 15652
			// (set) Token: 0x0600603A RID: 24634 RVA: 0x00094619 File Offset: 0x00092819
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003D25 RID: 15653
			// (set) Token: 0x0600603B RID: 24635 RVA: 0x00094631 File Offset: 0x00092831
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
