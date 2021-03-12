using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B51 RID: 2897
	public class GetUMIPGatewayCommand : SyntheticCommandWithPipelineInput<UMIPGateway, UMIPGateway>
	{
		// Token: 0x06008CEC RID: 36076 RVA: 0x000CEA49 File Offset: 0x000CCC49
		private GetUMIPGatewayCommand() : base("Get-UMIPGateway")
		{
		}

		// Token: 0x06008CED RID: 36077 RVA: 0x000CEA56 File Offset: 0x000CCC56
		public GetUMIPGatewayCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008CEE RID: 36078 RVA: 0x000CEA65 File Offset: 0x000CCC65
		public virtual GetUMIPGatewayCommand SetParameters(GetUMIPGatewayCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008CEF RID: 36079 RVA: 0x000CEA6F File Offset: 0x000CCC6F
		public virtual GetUMIPGatewayCommand SetParameters(GetUMIPGatewayCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B52 RID: 2898
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006203 RID: 25091
			// (set) Token: 0x06008CF0 RID: 36080 RVA: 0x000CEA79 File Offset: 0x000CCC79
			public virtual SwitchParameter IncludeSimulator
			{
				set
				{
					base.PowerSharpParameters["IncludeSimulator"] = value;
				}
			}

			// Token: 0x17006204 RID: 25092
			// (set) Token: 0x06008CF1 RID: 36081 RVA: 0x000CEA91 File Offset: 0x000CCC91
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006205 RID: 25093
			// (set) Token: 0x06008CF2 RID: 36082 RVA: 0x000CEAAF File Offset: 0x000CCCAF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006206 RID: 25094
			// (set) Token: 0x06008CF3 RID: 36083 RVA: 0x000CEAC2 File Offset: 0x000CCCC2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006207 RID: 25095
			// (set) Token: 0x06008CF4 RID: 36084 RVA: 0x000CEADA File Offset: 0x000CCCDA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006208 RID: 25096
			// (set) Token: 0x06008CF5 RID: 36085 RVA: 0x000CEAF2 File Offset: 0x000CCCF2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006209 RID: 25097
			// (set) Token: 0x06008CF6 RID: 36086 RVA: 0x000CEB0A File Offset: 0x000CCD0A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000B53 RID: 2899
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700620A RID: 25098
			// (set) Token: 0x06008CF8 RID: 36088 RVA: 0x000CEB2A File Offset: 0x000CCD2A
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMIPGatewayIdParameter(value) : null);
				}
			}

			// Token: 0x1700620B RID: 25099
			// (set) Token: 0x06008CF9 RID: 36089 RVA: 0x000CEB48 File Offset: 0x000CCD48
			public virtual SwitchParameter IncludeSimulator
			{
				set
				{
					base.PowerSharpParameters["IncludeSimulator"] = value;
				}
			}

			// Token: 0x1700620C RID: 25100
			// (set) Token: 0x06008CFA RID: 36090 RVA: 0x000CEB60 File Offset: 0x000CCD60
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700620D RID: 25101
			// (set) Token: 0x06008CFB RID: 36091 RVA: 0x000CEB7E File Offset: 0x000CCD7E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700620E RID: 25102
			// (set) Token: 0x06008CFC RID: 36092 RVA: 0x000CEB91 File Offset: 0x000CCD91
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700620F RID: 25103
			// (set) Token: 0x06008CFD RID: 36093 RVA: 0x000CEBA9 File Offset: 0x000CCDA9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006210 RID: 25104
			// (set) Token: 0x06008CFE RID: 36094 RVA: 0x000CEBC1 File Offset: 0x000CCDC1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006211 RID: 25105
			// (set) Token: 0x06008CFF RID: 36095 RVA: 0x000CEBD9 File Offset: 0x000CCDD9
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
