using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B69 RID: 2921
	public class NewUMHuntGroupCommand : SyntheticCommandWithPipelineInput<UMHuntGroup, UMHuntGroup>
	{
		// Token: 0x06008D9C RID: 36252 RVA: 0x000CF823 File Offset: 0x000CDA23
		private NewUMHuntGroupCommand() : base("New-UMHuntGroup")
		{
		}

		// Token: 0x06008D9D RID: 36253 RVA: 0x000CF830 File Offset: 0x000CDA30
		public NewUMHuntGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008D9E RID: 36254 RVA: 0x000CF83F File Offset: 0x000CDA3F
		public virtual NewUMHuntGroupCommand SetParameters(NewUMHuntGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B6A RID: 2922
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006283 RID: 25219
			// (set) Token: 0x06008D9F RID: 36255 RVA: 0x000CF849 File Offset: 0x000CDA49
			public virtual string PilotIdentifier
			{
				set
				{
					base.PowerSharpParameters["PilotIdentifier"] = value;
				}
			}

			// Token: 0x17006284 RID: 25220
			// (set) Token: 0x06008DA0 RID: 36256 RVA: 0x000CF85C File Offset: 0x000CDA5C
			public virtual string UMDialPlan
			{
				set
				{
					base.PowerSharpParameters["UMDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17006285 RID: 25221
			// (set) Token: 0x06008DA1 RID: 36257 RVA: 0x000CF87A File Offset: 0x000CDA7A
			public virtual string UMIPGateway
			{
				set
				{
					base.PowerSharpParameters["UMIPGateway"] = ((value != null) ? new UMIPGatewayIdParameter(value) : null);
				}
			}

			// Token: 0x17006286 RID: 25222
			// (set) Token: 0x06008DA2 RID: 36258 RVA: 0x000CF898 File Offset: 0x000CDA98
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006287 RID: 25223
			// (set) Token: 0x06008DA3 RID: 36259 RVA: 0x000CF8B6 File Offset: 0x000CDAB6
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006288 RID: 25224
			// (set) Token: 0x06008DA4 RID: 36260 RVA: 0x000CF8C9 File Offset: 0x000CDAC9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006289 RID: 25225
			// (set) Token: 0x06008DA5 RID: 36261 RVA: 0x000CF8DC File Offset: 0x000CDADC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700628A RID: 25226
			// (set) Token: 0x06008DA6 RID: 36262 RVA: 0x000CF8F4 File Offset: 0x000CDAF4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700628B RID: 25227
			// (set) Token: 0x06008DA7 RID: 36263 RVA: 0x000CF90C File Offset: 0x000CDB0C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700628C RID: 25228
			// (set) Token: 0x06008DA8 RID: 36264 RVA: 0x000CF924 File Offset: 0x000CDB24
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700628D RID: 25229
			// (set) Token: 0x06008DA9 RID: 36265 RVA: 0x000CF93C File Offset: 0x000CDB3C
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
