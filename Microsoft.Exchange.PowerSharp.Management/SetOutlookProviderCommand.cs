using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004FE RID: 1278
	public class SetOutlookProviderCommand : SyntheticCommandWithPipelineInputNoOutput<OutlookProvider>
	{
		// Token: 0x0600459E RID: 17822 RVA: 0x00071E39 File Offset: 0x00070039
		private SetOutlookProviderCommand() : base("Set-OutlookProvider")
		{
		}

		// Token: 0x0600459F RID: 17823 RVA: 0x00071E46 File Offset: 0x00070046
		public SetOutlookProviderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060045A0 RID: 17824 RVA: 0x00071E55 File Offset: 0x00070055
		public virtual SetOutlookProviderCommand SetParameters(SetOutlookProviderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060045A1 RID: 17825 RVA: 0x00071E5F File Offset: 0x0007005F
		public virtual SetOutlookProviderCommand SetParameters(SetOutlookProviderCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004FF RID: 1279
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700275B RID: 10075
			// (set) Token: 0x060045A2 RID: 17826 RVA: 0x00071E69 File Offset: 0x00070069
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700275C RID: 10076
			// (set) Token: 0x060045A3 RID: 17827 RVA: 0x00071E7C File Offset: 0x0007007C
			public virtual string CertPrincipalName
			{
				set
				{
					base.PowerSharpParameters["CertPrincipalName"] = value;
				}
			}

			// Token: 0x1700275D RID: 10077
			// (set) Token: 0x060045A4 RID: 17828 RVA: 0x00071E8F File Offset: 0x0007008F
			public virtual string Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700275E RID: 10078
			// (set) Token: 0x060045A5 RID: 17829 RVA: 0x00071EA2 File Offset: 0x000700A2
			public virtual int TTL
			{
				set
				{
					base.PowerSharpParameters["TTL"] = value;
				}
			}

			// Token: 0x1700275F RID: 10079
			// (set) Token: 0x060045A6 RID: 17830 RVA: 0x00071EBA File Offset: 0x000700BA
			public virtual OutlookProviderFlags OutlookProviderFlags
			{
				set
				{
					base.PowerSharpParameters["OutlookProviderFlags"] = value;
				}
			}

			// Token: 0x17002760 RID: 10080
			// (set) Token: 0x060045A7 RID: 17831 RVA: 0x00071ED2 File Offset: 0x000700D2
			public virtual string RequiredClientVersions
			{
				set
				{
					base.PowerSharpParameters["RequiredClientVersions"] = value;
				}
			}

			// Token: 0x17002761 RID: 10081
			// (set) Token: 0x060045A8 RID: 17832 RVA: 0x00071EE5 File Offset: 0x000700E5
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002762 RID: 10082
			// (set) Token: 0x060045A9 RID: 17833 RVA: 0x00071EF8 File Offset: 0x000700F8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002763 RID: 10083
			// (set) Token: 0x060045AA RID: 17834 RVA: 0x00071F10 File Offset: 0x00070110
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002764 RID: 10084
			// (set) Token: 0x060045AB RID: 17835 RVA: 0x00071F28 File Offset: 0x00070128
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002765 RID: 10085
			// (set) Token: 0x060045AC RID: 17836 RVA: 0x00071F40 File Offset: 0x00070140
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002766 RID: 10086
			// (set) Token: 0x060045AD RID: 17837 RVA: 0x00071F58 File Offset: 0x00070158
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000500 RID: 1280
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002767 RID: 10087
			// (set) Token: 0x060045AF RID: 17839 RVA: 0x00071F78 File Offset: 0x00070178
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OutlookProviderIdParameter(value) : null);
				}
			}

			// Token: 0x17002768 RID: 10088
			// (set) Token: 0x060045B0 RID: 17840 RVA: 0x00071F96 File Offset: 0x00070196
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002769 RID: 10089
			// (set) Token: 0x060045B1 RID: 17841 RVA: 0x00071FA9 File Offset: 0x000701A9
			public virtual string CertPrincipalName
			{
				set
				{
					base.PowerSharpParameters["CertPrincipalName"] = value;
				}
			}

			// Token: 0x1700276A RID: 10090
			// (set) Token: 0x060045B2 RID: 17842 RVA: 0x00071FBC File Offset: 0x000701BC
			public virtual string Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700276B RID: 10091
			// (set) Token: 0x060045B3 RID: 17843 RVA: 0x00071FCF File Offset: 0x000701CF
			public virtual int TTL
			{
				set
				{
					base.PowerSharpParameters["TTL"] = value;
				}
			}

			// Token: 0x1700276C RID: 10092
			// (set) Token: 0x060045B4 RID: 17844 RVA: 0x00071FE7 File Offset: 0x000701E7
			public virtual OutlookProviderFlags OutlookProviderFlags
			{
				set
				{
					base.PowerSharpParameters["OutlookProviderFlags"] = value;
				}
			}

			// Token: 0x1700276D RID: 10093
			// (set) Token: 0x060045B5 RID: 17845 RVA: 0x00071FFF File Offset: 0x000701FF
			public virtual string RequiredClientVersions
			{
				set
				{
					base.PowerSharpParameters["RequiredClientVersions"] = value;
				}
			}

			// Token: 0x1700276E RID: 10094
			// (set) Token: 0x060045B6 RID: 17846 RVA: 0x00072012 File Offset: 0x00070212
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700276F RID: 10095
			// (set) Token: 0x060045B7 RID: 17847 RVA: 0x00072025 File Offset: 0x00070225
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002770 RID: 10096
			// (set) Token: 0x060045B8 RID: 17848 RVA: 0x0007203D File Offset: 0x0007023D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002771 RID: 10097
			// (set) Token: 0x060045B9 RID: 17849 RVA: 0x00072055 File Offset: 0x00070255
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002772 RID: 10098
			// (set) Token: 0x060045BA RID: 17850 RVA: 0x0007206D File Offset: 0x0007026D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002773 RID: 10099
			// (set) Token: 0x060045BB RID: 17851 RVA: 0x00072085 File Offset: 0x00070285
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
