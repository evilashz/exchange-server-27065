using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B6F RID: 2927
	public class NewUMIPGatewayCommand : SyntheticCommandWithPipelineInput<UMIPGateway, UMIPGateway>
	{
		// Token: 0x06008DCC RID: 36300 RVA: 0x000CFBF3 File Offset: 0x000CDDF3
		private NewUMIPGatewayCommand() : base("New-UMIPGateway")
		{
		}

		// Token: 0x06008DCD RID: 36301 RVA: 0x000CFC00 File Offset: 0x000CDE00
		public NewUMIPGatewayCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008DCE RID: 36302 RVA: 0x000CFC0F File Offset: 0x000CDE0F
		public virtual NewUMIPGatewayCommand SetParameters(NewUMIPGatewayCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B70 RID: 2928
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170062A7 RID: 25255
			// (set) Token: 0x06008DCF RID: 36303 RVA: 0x000CFC19 File Offset: 0x000CDE19
			public virtual UMSmartHost Address
			{
				set
				{
					base.PowerSharpParameters["Address"] = value;
				}
			}

			// Token: 0x170062A8 RID: 25256
			// (set) Token: 0x06008DD0 RID: 36304 RVA: 0x000CFC2C File Offset: 0x000CDE2C
			public virtual IPAddressFamily IPAddressFamily
			{
				set
				{
					base.PowerSharpParameters["IPAddressFamily"] = value;
				}
			}

			// Token: 0x170062A9 RID: 25257
			// (set) Token: 0x06008DD1 RID: 36305 RVA: 0x000CFC44 File Offset: 0x000CDE44
			public virtual string UMDialPlan
			{
				set
				{
					base.PowerSharpParameters["UMDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170062AA RID: 25258
			// (set) Token: 0x06008DD2 RID: 36306 RVA: 0x000CFC62 File Offset: 0x000CDE62
			public virtual UMGlobalCallRoutingScheme GlobalCallRoutingScheme
			{
				set
				{
					base.PowerSharpParameters["GlobalCallRoutingScheme"] = value;
				}
			}

			// Token: 0x170062AB RID: 25259
			// (set) Token: 0x06008DD3 RID: 36307 RVA: 0x000CFC7A File Offset: 0x000CDE7A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170062AC RID: 25260
			// (set) Token: 0x06008DD4 RID: 36308 RVA: 0x000CFC98 File Offset: 0x000CDE98
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170062AD RID: 25261
			// (set) Token: 0x06008DD5 RID: 36309 RVA: 0x000CFCAB File Offset: 0x000CDEAB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170062AE RID: 25262
			// (set) Token: 0x06008DD6 RID: 36310 RVA: 0x000CFCBE File Offset: 0x000CDEBE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170062AF RID: 25263
			// (set) Token: 0x06008DD7 RID: 36311 RVA: 0x000CFCD6 File Offset: 0x000CDED6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170062B0 RID: 25264
			// (set) Token: 0x06008DD8 RID: 36312 RVA: 0x000CFCEE File Offset: 0x000CDEEE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170062B1 RID: 25265
			// (set) Token: 0x06008DD9 RID: 36313 RVA: 0x000CFD06 File Offset: 0x000CDF06
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170062B2 RID: 25266
			// (set) Token: 0x06008DDA RID: 36314 RVA: 0x000CFD1E File Offset: 0x000CDF1E
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
