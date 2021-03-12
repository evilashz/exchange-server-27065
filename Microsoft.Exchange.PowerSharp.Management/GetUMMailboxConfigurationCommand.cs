using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B54 RID: 2900
	public class GetUMMailboxConfigurationCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06008D01 RID: 36097 RVA: 0x000CEBF9 File Offset: 0x000CCDF9
		private GetUMMailboxConfigurationCommand() : base("Get-UMMailboxConfiguration")
		{
		}

		// Token: 0x06008D02 RID: 36098 RVA: 0x000CEC06 File Offset: 0x000CCE06
		public GetUMMailboxConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008D03 RID: 36099 RVA: 0x000CEC15 File Offset: 0x000CCE15
		public virtual GetUMMailboxConfigurationCommand SetParameters(GetUMMailboxConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008D04 RID: 36100 RVA: 0x000CEC1F File Offset: 0x000CCE1F
		public virtual GetUMMailboxConfigurationCommand SetParameters(GetUMMailboxConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B55 RID: 2901
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006212 RID: 25106
			// (set) Token: 0x06008D05 RID: 36101 RVA: 0x000CEC29 File Offset: 0x000CCE29
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006213 RID: 25107
			// (set) Token: 0x06008D06 RID: 36102 RVA: 0x000CEC3C File Offset: 0x000CCE3C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006214 RID: 25108
			// (set) Token: 0x06008D07 RID: 36103 RVA: 0x000CEC54 File Offset: 0x000CCE54
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006215 RID: 25109
			// (set) Token: 0x06008D08 RID: 36104 RVA: 0x000CEC6C File Offset: 0x000CCE6C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006216 RID: 25110
			// (set) Token: 0x06008D09 RID: 36105 RVA: 0x000CEC84 File Offset: 0x000CCE84
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000B56 RID: 2902
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006217 RID: 25111
			// (set) Token: 0x06008D0B RID: 36107 RVA: 0x000CECA4 File Offset: 0x000CCEA4
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006218 RID: 25112
			// (set) Token: 0x06008D0C RID: 36108 RVA: 0x000CECC2 File Offset: 0x000CCEC2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006219 RID: 25113
			// (set) Token: 0x06008D0D RID: 36109 RVA: 0x000CECD5 File Offset: 0x000CCED5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700621A RID: 25114
			// (set) Token: 0x06008D0E RID: 36110 RVA: 0x000CECED File Offset: 0x000CCEED
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700621B RID: 25115
			// (set) Token: 0x06008D0F RID: 36111 RVA: 0x000CED05 File Offset: 0x000CCF05
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700621C RID: 25116
			// (set) Token: 0x06008D10 RID: 36112 RVA: 0x000CED1D File Offset: 0x000CCF1D
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
