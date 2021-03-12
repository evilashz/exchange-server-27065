using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200059C RID: 1436
	public class NewExchangeSettingsCommand : SyntheticCommandWithPipelineInput<ExchangeSettings, ExchangeSettings>
	{
		// Token: 0x06004B01 RID: 19201 RVA: 0x000789B9 File Offset: 0x00076BB9
		private NewExchangeSettingsCommand() : base("New-ExchangeSettings")
		{
		}

		// Token: 0x06004B02 RID: 19202 RVA: 0x000789C6 File Offset: 0x00076BC6
		public NewExchangeSettingsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004B03 RID: 19203 RVA: 0x000789D5 File Offset: 0x00076BD5
		public virtual NewExchangeSettingsCommand SetParameters(NewExchangeSettingsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200059D RID: 1437
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002B82 RID: 11138
			// (set) Token: 0x06004B04 RID: 19204 RVA: 0x000789DF File Offset: 0x00076BDF
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002B83 RID: 11139
			// (set) Token: 0x06004B05 RID: 19205 RVA: 0x000789F7 File Offset: 0x00076BF7
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002B84 RID: 11140
			// (set) Token: 0x06004B06 RID: 19206 RVA: 0x00078A0A File Offset: 0x00076C0A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002B85 RID: 11141
			// (set) Token: 0x06004B07 RID: 19207 RVA: 0x00078A1D File Offset: 0x00076C1D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002B86 RID: 11142
			// (set) Token: 0x06004B08 RID: 19208 RVA: 0x00078A35 File Offset: 0x00076C35
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002B87 RID: 11143
			// (set) Token: 0x06004B09 RID: 19209 RVA: 0x00078A4D File Offset: 0x00076C4D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002B88 RID: 11144
			// (set) Token: 0x06004B0A RID: 19210 RVA: 0x00078A65 File Offset: 0x00076C65
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002B89 RID: 11145
			// (set) Token: 0x06004B0B RID: 19211 RVA: 0x00078A7D File Offset: 0x00076C7D
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
