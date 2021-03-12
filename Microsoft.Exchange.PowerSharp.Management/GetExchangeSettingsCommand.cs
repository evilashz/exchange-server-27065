using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000599 RID: 1433
	public class GetExchangeSettingsCommand : SyntheticCommandWithPipelineInput<ExchangeSettings, ExchangeSettings>
	{
		// Token: 0x06004AD8 RID: 19160 RVA: 0x00078679 File Offset: 0x00076879
		private GetExchangeSettingsCommand() : base("Get-ExchangeSettings")
		{
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x00078686 File Offset: 0x00076886
		public GetExchangeSettingsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004ADA RID: 19162 RVA: 0x00078695 File Offset: 0x00076895
		public virtual GetExchangeSettingsCommand SetParameters(GetExchangeSettingsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004ADB RID: 19163 RVA: 0x0007869F File Offset: 0x0007689F
		public virtual GetExchangeSettingsCommand SetParameters(GetExchangeSettingsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200059A RID: 1434
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002B5F RID: 11103
			// (set) Token: 0x06004ADC RID: 19164 RVA: 0x000786A9 File Offset: 0x000768A9
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17002B60 RID: 11104
			// (set) Token: 0x06004ADD RID: 19165 RVA: 0x000786C1 File Offset: 0x000768C1
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17002B61 RID: 11105
			// (set) Token: 0x06004ADE RID: 19166 RVA: 0x000786D4 File Offset: 0x000768D4
			public virtual string ConfigName
			{
				set
				{
					base.PowerSharpParameters["ConfigName"] = value;
				}
			}

			// Token: 0x17002B62 RID: 11106
			// (set) Token: 0x06004ADF RID: 19167 RVA: 0x000786E7 File Offset: 0x000768E7
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17002B63 RID: 11107
			// (set) Token: 0x06004AE0 RID: 19168 RVA: 0x000786FA File Offset: 0x000768FA
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17002B64 RID: 11108
			// (set) Token: 0x06004AE1 RID: 19169 RVA: 0x0007870D File Offset: 0x0007690D
			public virtual string Process
			{
				set
				{
					base.PowerSharpParameters["Process"] = value;
				}
			}

			// Token: 0x17002B65 RID: 11109
			// (set) Token: 0x06004AE2 RID: 19170 RVA: 0x00078720 File Offset: 0x00076920
			public virtual Guid User
			{
				set
				{
					base.PowerSharpParameters["User"] = value;
				}
			}

			// Token: 0x17002B66 RID: 11110
			// (set) Token: 0x06004AE3 RID: 19171 RVA: 0x00078738 File Offset: 0x00076938
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002B67 RID: 11111
			// (set) Token: 0x06004AE4 RID: 19172 RVA: 0x00078756 File Offset: 0x00076956
			public virtual string GenericScopeName
			{
				set
				{
					base.PowerSharpParameters["GenericScopeName"] = value;
				}
			}

			// Token: 0x17002B68 RID: 11112
			// (set) Token: 0x06004AE5 RID: 19173 RVA: 0x00078769 File Offset: 0x00076969
			public virtual string GenericScopeValue
			{
				set
				{
					base.PowerSharpParameters["GenericScopeValue"] = value;
				}
			}

			// Token: 0x17002B69 RID: 11113
			// (set) Token: 0x06004AE6 RID: 19174 RVA: 0x0007877C File Offset: 0x0007697C
			public virtual string GenericScopes
			{
				set
				{
					base.PowerSharpParameters["GenericScopes"] = value;
				}
			}

			// Token: 0x17002B6A RID: 11114
			// (set) Token: 0x06004AE7 RID: 19175 RVA: 0x0007878F File Offset: 0x0007698F
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002B6B RID: 11115
			// (set) Token: 0x06004AE8 RID: 19176 RVA: 0x000787A7 File Offset: 0x000769A7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002B6C RID: 11116
			// (set) Token: 0x06004AE9 RID: 19177 RVA: 0x000787BA File Offset: 0x000769BA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002B6D RID: 11117
			// (set) Token: 0x06004AEA RID: 19178 RVA: 0x000787D2 File Offset: 0x000769D2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002B6E RID: 11118
			// (set) Token: 0x06004AEB RID: 19179 RVA: 0x000787EA File Offset: 0x000769EA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002B6F RID: 11119
			// (set) Token: 0x06004AEC RID: 19180 RVA: 0x00078802 File Offset: 0x00076A02
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200059B RID: 1435
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002B70 RID: 11120
			// (set) Token: 0x06004AEE RID: 19182 RVA: 0x00078822 File Offset: 0x00076A22
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x17002B71 RID: 11121
			// (set) Token: 0x06004AEF RID: 19183 RVA: 0x00078840 File Offset: 0x00076A40
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17002B72 RID: 11122
			// (set) Token: 0x06004AF0 RID: 19184 RVA: 0x00078858 File Offset: 0x00076A58
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17002B73 RID: 11123
			// (set) Token: 0x06004AF1 RID: 19185 RVA: 0x0007886B File Offset: 0x00076A6B
			public virtual string ConfigName
			{
				set
				{
					base.PowerSharpParameters["ConfigName"] = value;
				}
			}

			// Token: 0x17002B74 RID: 11124
			// (set) Token: 0x06004AF2 RID: 19186 RVA: 0x0007887E File Offset: 0x00076A7E
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17002B75 RID: 11125
			// (set) Token: 0x06004AF3 RID: 19187 RVA: 0x00078891 File Offset: 0x00076A91
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17002B76 RID: 11126
			// (set) Token: 0x06004AF4 RID: 19188 RVA: 0x000788A4 File Offset: 0x00076AA4
			public virtual string Process
			{
				set
				{
					base.PowerSharpParameters["Process"] = value;
				}
			}

			// Token: 0x17002B77 RID: 11127
			// (set) Token: 0x06004AF5 RID: 19189 RVA: 0x000788B7 File Offset: 0x00076AB7
			public virtual Guid User
			{
				set
				{
					base.PowerSharpParameters["User"] = value;
				}
			}

			// Token: 0x17002B78 RID: 11128
			// (set) Token: 0x06004AF6 RID: 19190 RVA: 0x000788CF File Offset: 0x00076ACF
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002B79 RID: 11129
			// (set) Token: 0x06004AF7 RID: 19191 RVA: 0x000788ED File Offset: 0x00076AED
			public virtual string GenericScopeName
			{
				set
				{
					base.PowerSharpParameters["GenericScopeName"] = value;
				}
			}

			// Token: 0x17002B7A RID: 11130
			// (set) Token: 0x06004AF8 RID: 19192 RVA: 0x00078900 File Offset: 0x00076B00
			public virtual string GenericScopeValue
			{
				set
				{
					base.PowerSharpParameters["GenericScopeValue"] = value;
				}
			}

			// Token: 0x17002B7B RID: 11131
			// (set) Token: 0x06004AF9 RID: 19193 RVA: 0x00078913 File Offset: 0x00076B13
			public virtual string GenericScopes
			{
				set
				{
					base.PowerSharpParameters["GenericScopes"] = value;
				}
			}

			// Token: 0x17002B7C RID: 11132
			// (set) Token: 0x06004AFA RID: 19194 RVA: 0x00078926 File Offset: 0x00076B26
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002B7D RID: 11133
			// (set) Token: 0x06004AFB RID: 19195 RVA: 0x0007893E File Offset: 0x00076B3E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002B7E RID: 11134
			// (set) Token: 0x06004AFC RID: 19196 RVA: 0x00078951 File Offset: 0x00076B51
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002B7F RID: 11135
			// (set) Token: 0x06004AFD RID: 19197 RVA: 0x00078969 File Offset: 0x00076B69
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002B80 RID: 11136
			// (set) Token: 0x06004AFE RID: 19198 RVA: 0x00078981 File Offset: 0x00076B81
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002B81 RID: 11137
			// (set) Token: 0x06004AFF RID: 19199 RVA: 0x00078999 File Offset: 0x00076B99
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
