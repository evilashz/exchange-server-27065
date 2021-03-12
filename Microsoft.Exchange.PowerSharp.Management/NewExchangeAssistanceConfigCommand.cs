using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200060A RID: 1546
	public class NewExchangeAssistanceConfigCommand : SyntheticCommandWithPipelineInput<ExchangeAssistance, ExchangeAssistance>
	{
		// Token: 0x06004F76 RID: 20342 RVA: 0x0007E4E6 File Offset: 0x0007C6E6
		private NewExchangeAssistanceConfigCommand() : base("New-ExchangeAssistanceConfig")
		{
		}

		// Token: 0x06004F77 RID: 20343 RVA: 0x0007E4F3 File Offset: 0x0007C6F3
		public NewExchangeAssistanceConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004F78 RID: 20344 RVA: 0x0007E502 File Offset: 0x0007C702
		public virtual NewExchangeAssistanceConfigCommand SetParameters(NewExchangeAssistanceConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200060B RID: 1547
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002F1B RID: 12059
			// (set) Token: 0x06004F79 RID: 20345 RVA: 0x0007E50C File Offset: 0x0007C70C
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17002F1C RID: 12060
			// (set) Token: 0x06004F7A RID: 20346 RVA: 0x0007E524 File Offset: 0x0007C724
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002F1D RID: 12061
			// (set) Token: 0x06004F7B RID: 20347 RVA: 0x0007E542 File Offset: 0x0007C742
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002F1E RID: 12062
			// (set) Token: 0x06004F7C RID: 20348 RVA: 0x0007E555 File Offset: 0x0007C755
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002F1F RID: 12063
			// (set) Token: 0x06004F7D RID: 20349 RVA: 0x0007E56D File Offset: 0x0007C76D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002F20 RID: 12064
			// (set) Token: 0x06004F7E RID: 20350 RVA: 0x0007E585 File Offset: 0x0007C785
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002F21 RID: 12065
			// (set) Token: 0x06004F7F RID: 20351 RVA: 0x0007E59D File Offset: 0x0007C79D
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
