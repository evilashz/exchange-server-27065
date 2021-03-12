using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008C9 RID: 2249
	public class SetAdSiteCommand : SyntheticCommandWithPipelineInputNoOutput<ADSite>
	{
		// Token: 0x06007065 RID: 28773 RVA: 0x000A98A3 File Offset: 0x000A7AA3
		private SetAdSiteCommand() : base("Set-AdSite")
		{
		}

		// Token: 0x06007066 RID: 28774 RVA: 0x000A98B0 File Offset: 0x000A7AB0
		public SetAdSiteCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007067 RID: 28775 RVA: 0x000A98BF File Offset: 0x000A7ABF
		public virtual SetAdSiteCommand SetParameters(SetAdSiteCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007068 RID: 28776 RVA: 0x000A98C9 File Offset: 0x000A7AC9
		public virtual SetAdSiteCommand SetParameters(SetAdSiteCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008CA RID: 2250
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004A8C RID: 19084
			// (set) Token: 0x06007069 RID: 28777 RVA: 0x000A98D3 File Offset: 0x000A7AD3
			public virtual MultiValuedProperty<AdSiteIdParameter> ResponsibleForSites
			{
				set
				{
					base.PowerSharpParameters["ResponsibleForSites"] = value;
				}
			}

			// Token: 0x17004A8D RID: 19085
			// (set) Token: 0x0600706A RID: 28778 RVA: 0x000A98E6 File Offset: 0x000A7AE6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004A8E RID: 19086
			// (set) Token: 0x0600706B RID: 28779 RVA: 0x000A98F9 File Offset: 0x000A7AF9
			public virtual bool HubSiteEnabled
			{
				set
				{
					base.PowerSharpParameters["HubSiteEnabled"] = value;
				}
			}

			// Token: 0x17004A8F RID: 19087
			// (set) Token: 0x0600706C RID: 28780 RVA: 0x000A9911 File Offset: 0x000A7B11
			public virtual bool InboundMailEnabled
			{
				set
				{
					base.PowerSharpParameters["InboundMailEnabled"] = value;
				}
			}

			// Token: 0x17004A90 RID: 19088
			// (set) Token: 0x0600706D RID: 28781 RVA: 0x000A9929 File Offset: 0x000A7B29
			public virtual int PartnerId
			{
				set
				{
					base.PowerSharpParameters["PartnerId"] = value;
				}
			}

			// Token: 0x17004A91 RID: 19089
			// (set) Token: 0x0600706E RID: 28782 RVA: 0x000A9941 File Offset: 0x000A7B41
			public virtual int MinorPartnerId
			{
				set
				{
					base.PowerSharpParameters["MinorPartnerId"] = value;
				}
			}

			// Token: 0x17004A92 RID: 19090
			// (set) Token: 0x0600706F RID: 28783 RVA: 0x000A9959 File Offset: 0x000A7B59
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004A93 RID: 19091
			// (set) Token: 0x06007070 RID: 28784 RVA: 0x000A9971 File Offset: 0x000A7B71
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004A94 RID: 19092
			// (set) Token: 0x06007071 RID: 28785 RVA: 0x000A9989 File Offset: 0x000A7B89
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004A95 RID: 19093
			// (set) Token: 0x06007072 RID: 28786 RVA: 0x000A99A1 File Offset: 0x000A7BA1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004A96 RID: 19094
			// (set) Token: 0x06007073 RID: 28787 RVA: 0x000A99B9 File Offset: 0x000A7BB9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020008CB RID: 2251
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004A97 RID: 19095
			// (set) Token: 0x06007075 RID: 28789 RVA: 0x000A99D9 File Offset: 0x000A7BD9
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AdSiteIdParameter(value) : null);
				}
			}

			// Token: 0x17004A98 RID: 19096
			// (set) Token: 0x06007076 RID: 28790 RVA: 0x000A99F7 File Offset: 0x000A7BF7
			public virtual MultiValuedProperty<AdSiteIdParameter> ResponsibleForSites
			{
				set
				{
					base.PowerSharpParameters["ResponsibleForSites"] = value;
				}
			}

			// Token: 0x17004A99 RID: 19097
			// (set) Token: 0x06007077 RID: 28791 RVA: 0x000A9A0A File Offset: 0x000A7C0A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004A9A RID: 19098
			// (set) Token: 0x06007078 RID: 28792 RVA: 0x000A9A1D File Offset: 0x000A7C1D
			public virtual bool HubSiteEnabled
			{
				set
				{
					base.PowerSharpParameters["HubSiteEnabled"] = value;
				}
			}

			// Token: 0x17004A9B RID: 19099
			// (set) Token: 0x06007079 RID: 28793 RVA: 0x000A9A35 File Offset: 0x000A7C35
			public virtual bool InboundMailEnabled
			{
				set
				{
					base.PowerSharpParameters["InboundMailEnabled"] = value;
				}
			}

			// Token: 0x17004A9C RID: 19100
			// (set) Token: 0x0600707A RID: 28794 RVA: 0x000A9A4D File Offset: 0x000A7C4D
			public virtual int PartnerId
			{
				set
				{
					base.PowerSharpParameters["PartnerId"] = value;
				}
			}

			// Token: 0x17004A9D RID: 19101
			// (set) Token: 0x0600707B RID: 28795 RVA: 0x000A9A65 File Offset: 0x000A7C65
			public virtual int MinorPartnerId
			{
				set
				{
					base.PowerSharpParameters["MinorPartnerId"] = value;
				}
			}

			// Token: 0x17004A9E RID: 19102
			// (set) Token: 0x0600707C RID: 28796 RVA: 0x000A9A7D File Offset: 0x000A7C7D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004A9F RID: 19103
			// (set) Token: 0x0600707D RID: 28797 RVA: 0x000A9A95 File Offset: 0x000A7C95
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004AA0 RID: 19104
			// (set) Token: 0x0600707E RID: 28798 RVA: 0x000A9AAD File Offset: 0x000A7CAD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004AA1 RID: 19105
			// (set) Token: 0x0600707F RID: 28799 RVA: 0x000A9AC5 File Offset: 0x000A7CC5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004AA2 RID: 19106
			// (set) Token: 0x06007080 RID: 28800 RVA: 0x000A9ADD File Offset: 0x000A7CDD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
