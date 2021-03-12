using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200059E RID: 1438
	public class RemoveExchangeSettingsCommand : SyntheticCommandWithPipelineInput<ExchangeSettings, ExchangeSettings>
	{
		// Token: 0x06004B0D RID: 19213 RVA: 0x00078A9D File Offset: 0x00076C9D
		private RemoveExchangeSettingsCommand() : base("Remove-ExchangeSettings")
		{
		}

		// Token: 0x06004B0E RID: 19214 RVA: 0x00078AAA File Offset: 0x00076CAA
		public RemoveExchangeSettingsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004B0F RID: 19215 RVA: 0x00078AB9 File Offset: 0x00076CB9
		public virtual RemoveExchangeSettingsCommand SetParameters(RemoveExchangeSettingsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004B10 RID: 19216 RVA: 0x00078AC3 File Offset: 0x00076CC3
		public virtual RemoveExchangeSettingsCommand SetParameters(RemoveExchangeSettingsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200059F RID: 1439
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002B8A RID: 11146
			// (set) Token: 0x06004B11 RID: 19217 RVA: 0x00078ACD File Offset: 0x00076CCD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002B8B RID: 11147
			// (set) Token: 0x06004B12 RID: 19218 RVA: 0x00078AE0 File Offset: 0x00076CE0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002B8C RID: 11148
			// (set) Token: 0x06004B13 RID: 19219 RVA: 0x00078AF8 File Offset: 0x00076CF8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002B8D RID: 11149
			// (set) Token: 0x06004B14 RID: 19220 RVA: 0x00078B10 File Offset: 0x00076D10
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002B8E RID: 11150
			// (set) Token: 0x06004B15 RID: 19221 RVA: 0x00078B28 File Offset: 0x00076D28
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002B8F RID: 11151
			// (set) Token: 0x06004B16 RID: 19222 RVA: 0x00078B40 File Offset: 0x00076D40
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002B90 RID: 11152
			// (set) Token: 0x06004B17 RID: 19223 RVA: 0x00078B58 File Offset: 0x00076D58
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005A0 RID: 1440
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002B91 RID: 11153
			// (set) Token: 0x06004B19 RID: 19225 RVA: 0x00078B78 File Offset: 0x00076D78
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x17002B92 RID: 11154
			// (set) Token: 0x06004B1A RID: 19226 RVA: 0x00078B96 File Offset: 0x00076D96
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002B93 RID: 11155
			// (set) Token: 0x06004B1B RID: 19227 RVA: 0x00078BA9 File Offset: 0x00076DA9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002B94 RID: 11156
			// (set) Token: 0x06004B1C RID: 19228 RVA: 0x00078BC1 File Offset: 0x00076DC1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002B95 RID: 11157
			// (set) Token: 0x06004B1D RID: 19229 RVA: 0x00078BD9 File Offset: 0x00076DD9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002B96 RID: 11158
			// (set) Token: 0x06004B1E RID: 19230 RVA: 0x00078BF1 File Offset: 0x00076DF1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002B97 RID: 11159
			// (set) Token: 0x06004B1F RID: 19231 RVA: 0x00078C09 File Offset: 0x00076E09
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002B98 RID: 11160
			// (set) Token: 0x06004B20 RID: 19232 RVA: 0x00078C21 File Offset: 0x00076E21
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
