using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200016F RID: 367
	public class GetOutboundConnectorReportCommand : SyntheticCommandWithPipelineInput<OutboundConnectorReport, OutboundConnectorReport>
	{
		// Token: 0x0600226D RID: 8813 RVA: 0x00044363 File Offset: 0x00042563
		private GetOutboundConnectorReportCommand() : base("Get-OutboundConnectorReport")
		{
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x00044370 File Offset: 0x00042570
		public GetOutboundConnectorReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x0004437F File Offset: 0x0004257F
		public virtual GetOutboundConnectorReportCommand SetParameters(GetOutboundConnectorReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000170 RID: 368
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000B48 RID: 2888
			// (set) Token: 0x06002270 RID: 8816 RVA: 0x00044389 File Offset: 0x00042589
			public virtual Fqdn Domain
			{
				set
				{
					base.PowerSharpParameters["Domain"] = value;
				}
			}

			// Token: 0x17000B49 RID: 2889
			// (set) Token: 0x06002271 RID: 8817 RVA: 0x0004439C File Offset: 0x0004259C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000B4A RID: 2890
			// (set) Token: 0x06002272 RID: 8818 RVA: 0x000443BA File Offset: 0x000425BA
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17000B4B RID: 2891
			// (set) Token: 0x06002273 RID: 8819 RVA: 0x000443CD File Offset: 0x000425CD
			public virtual string ProbeTag
			{
				set
				{
					base.PowerSharpParameters["ProbeTag"] = value;
				}
			}

			// Token: 0x17000B4C RID: 2892
			// (set) Token: 0x06002274 RID: 8820 RVA: 0x000443E0 File Offset: 0x000425E0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000B4D RID: 2893
			// (set) Token: 0x06002275 RID: 8821 RVA: 0x000443F8 File Offset: 0x000425F8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000B4E RID: 2894
			// (set) Token: 0x06002276 RID: 8822 RVA: 0x00044410 File Offset: 0x00042610
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000B4F RID: 2895
			// (set) Token: 0x06002277 RID: 8823 RVA: 0x00044428 File Offset: 0x00042628
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
