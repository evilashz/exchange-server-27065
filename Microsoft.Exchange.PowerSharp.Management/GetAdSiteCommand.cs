using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000881 RID: 2177
	public class GetAdSiteCommand : SyntheticCommandWithPipelineInput<ADSite, ADSite>
	{
		// Token: 0x06006CAA RID: 27818 RVA: 0x000A49F7 File Offset: 0x000A2BF7
		private GetAdSiteCommand() : base("Get-AdSite")
		{
		}

		// Token: 0x06006CAB RID: 27819 RVA: 0x000A4A04 File Offset: 0x000A2C04
		public GetAdSiteCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006CAC RID: 27820 RVA: 0x000A4A13 File Offset: 0x000A2C13
		public virtual GetAdSiteCommand SetParameters(GetAdSiteCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006CAD RID: 27821 RVA: 0x000A4A1D File Offset: 0x000A2C1D
		public virtual GetAdSiteCommand SetParameters(GetAdSiteCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000882 RID: 2178
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004761 RID: 18273
			// (set) Token: 0x06006CAE RID: 27822 RVA: 0x000A4A27 File Offset: 0x000A2C27
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004762 RID: 18274
			// (set) Token: 0x06006CAF RID: 27823 RVA: 0x000A4A3A File Offset: 0x000A2C3A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004763 RID: 18275
			// (set) Token: 0x06006CB0 RID: 27824 RVA: 0x000A4A52 File Offset: 0x000A2C52
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004764 RID: 18276
			// (set) Token: 0x06006CB1 RID: 27825 RVA: 0x000A4A6A File Offset: 0x000A2C6A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004765 RID: 18277
			// (set) Token: 0x06006CB2 RID: 27826 RVA: 0x000A4A82 File Offset: 0x000A2C82
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000883 RID: 2179
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004766 RID: 18278
			// (set) Token: 0x06006CB4 RID: 27828 RVA: 0x000A4AA2 File Offset: 0x000A2CA2
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AdSiteIdParameter(value) : null);
				}
			}

			// Token: 0x17004767 RID: 18279
			// (set) Token: 0x06006CB5 RID: 27829 RVA: 0x000A4AC0 File Offset: 0x000A2CC0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004768 RID: 18280
			// (set) Token: 0x06006CB6 RID: 27830 RVA: 0x000A4AD3 File Offset: 0x000A2CD3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004769 RID: 18281
			// (set) Token: 0x06006CB7 RID: 27831 RVA: 0x000A4AEB File Offset: 0x000A2CEB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700476A RID: 18282
			// (set) Token: 0x06006CB8 RID: 27832 RVA: 0x000A4B03 File Offset: 0x000A2D03
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700476B RID: 18283
			// (set) Token: 0x06006CB9 RID: 27833 RVA: 0x000A4B1B File Offset: 0x000A2D1B
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
