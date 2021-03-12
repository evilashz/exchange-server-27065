using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AE3 RID: 2787
	public class GetCmdletExtensionAgentCommand : SyntheticCommandWithPipelineInput<CmdletExtensionAgent, CmdletExtensionAgent>
	{
		// Token: 0x0600898D RID: 35213 RVA: 0x000CA5A8 File Offset: 0x000C87A8
		private GetCmdletExtensionAgentCommand() : base("Get-CmdletExtensionAgent")
		{
		}

		// Token: 0x0600898E RID: 35214 RVA: 0x000CA5B5 File Offset: 0x000C87B5
		public GetCmdletExtensionAgentCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600898F RID: 35215 RVA: 0x000CA5C4 File Offset: 0x000C87C4
		public virtual GetCmdletExtensionAgentCommand SetParameters(GetCmdletExtensionAgentCommand.FiltersParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008990 RID: 35216 RVA: 0x000CA5CE File Offset: 0x000C87CE
		public virtual GetCmdletExtensionAgentCommand SetParameters(GetCmdletExtensionAgentCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008991 RID: 35217 RVA: 0x000CA5D8 File Offset: 0x000C87D8
		public virtual GetCmdletExtensionAgentCommand SetParameters(GetCmdletExtensionAgentCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AE4 RID: 2788
		public class FiltersParameters : ParametersBase
		{
			// Token: 0x17005F80 RID: 24448
			// (set) Token: 0x06008992 RID: 35218 RVA: 0x000CA5E2 File Offset: 0x000C87E2
			public virtual string Assembly
			{
				set
				{
					base.PowerSharpParameters["Assembly"] = value;
				}
			}

			// Token: 0x17005F81 RID: 24449
			// (set) Token: 0x06008993 RID: 35219 RVA: 0x000CA5F5 File Offset: 0x000C87F5
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17005F82 RID: 24450
			// (set) Token: 0x06008994 RID: 35220 RVA: 0x000CA60D File Offset: 0x000C880D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005F83 RID: 24451
			// (set) Token: 0x06008995 RID: 35221 RVA: 0x000CA620 File Offset: 0x000C8820
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005F84 RID: 24452
			// (set) Token: 0x06008996 RID: 35222 RVA: 0x000CA638 File Offset: 0x000C8838
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005F85 RID: 24453
			// (set) Token: 0x06008997 RID: 35223 RVA: 0x000CA650 File Offset: 0x000C8850
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005F86 RID: 24454
			// (set) Token: 0x06008998 RID: 35224 RVA: 0x000CA668 File Offset: 0x000C8868
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000AE5 RID: 2789
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005F87 RID: 24455
			// (set) Token: 0x0600899A RID: 35226 RVA: 0x000CA688 File Offset: 0x000C8888
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005F88 RID: 24456
			// (set) Token: 0x0600899B RID: 35227 RVA: 0x000CA69B File Offset: 0x000C889B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005F89 RID: 24457
			// (set) Token: 0x0600899C RID: 35228 RVA: 0x000CA6B3 File Offset: 0x000C88B3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005F8A RID: 24458
			// (set) Token: 0x0600899D RID: 35229 RVA: 0x000CA6CB File Offset: 0x000C88CB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005F8B RID: 24459
			// (set) Token: 0x0600899E RID: 35230 RVA: 0x000CA6E3 File Offset: 0x000C88E3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000AE6 RID: 2790
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005F8C RID: 24460
			// (set) Token: 0x060089A0 RID: 35232 RVA: 0x000CA703 File Offset: 0x000C8903
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new CmdletExtensionAgentIdParameter(value) : null);
				}
			}

			// Token: 0x17005F8D RID: 24461
			// (set) Token: 0x060089A1 RID: 35233 RVA: 0x000CA721 File Offset: 0x000C8921
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005F8E RID: 24462
			// (set) Token: 0x060089A2 RID: 35234 RVA: 0x000CA734 File Offset: 0x000C8934
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005F8F RID: 24463
			// (set) Token: 0x060089A3 RID: 35235 RVA: 0x000CA74C File Offset: 0x000C894C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005F90 RID: 24464
			// (set) Token: 0x060089A4 RID: 35236 RVA: 0x000CA764 File Offset: 0x000C8964
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005F91 RID: 24465
			// (set) Token: 0x060089A5 RID: 35237 RVA: 0x000CA77C File Offset: 0x000C897C
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
