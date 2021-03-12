using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B49 RID: 2889
	public class GetUMCallSummaryReportCommand : SyntheticCommandWithPipelineInput<MailboxIdParameter, MailboxIdParameter>
	{
		// Token: 0x06008CB7 RID: 36023 RVA: 0x000CE5FA File Offset: 0x000CC7FA
		private GetUMCallSummaryReportCommand() : base("Get-UMCallSummaryReport")
		{
		}

		// Token: 0x06008CB8 RID: 36024 RVA: 0x000CE607 File Offset: 0x000CC807
		public GetUMCallSummaryReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008CB9 RID: 36025 RVA: 0x000CE616 File Offset: 0x000CC816
		public virtual GetUMCallSummaryReportCommand SetParameters(GetUMCallSummaryReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B4A RID: 2890
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170061DE RID: 25054
			// (set) Token: 0x06008CBA RID: 36026 RVA: 0x000CE620 File Offset: 0x000CC820
			public virtual string UMDialPlan
			{
				set
				{
					base.PowerSharpParameters["UMDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170061DF RID: 25055
			// (set) Token: 0x06008CBB RID: 36027 RVA: 0x000CE63E File Offset: 0x000CC83E
			public virtual string UMIPGateway
			{
				set
				{
					base.PowerSharpParameters["UMIPGateway"] = ((value != null) ? new UMIPGatewayIdParameter(value) : null);
				}
			}

			// Token: 0x170061E0 RID: 25056
			// (set) Token: 0x06008CBC RID: 36028 RVA: 0x000CE65C File Offset: 0x000CC85C
			public virtual GroupBy GroupBy
			{
				set
				{
					base.PowerSharpParameters["GroupBy"] = value;
				}
			}

			// Token: 0x170061E1 RID: 25057
			// (set) Token: 0x06008CBD RID: 36029 RVA: 0x000CE674 File Offset: 0x000CC874
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170061E2 RID: 25058
			// (set) Token: 0x06008CBE RID: 36030 RVA: 0x000CE692 File Offset: 0x000CC892
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170061E3 RID: 25059
			// (set) Token: 0x06008CBF RID: 36031 RVA: 0x000CE6A5 File Offset: 0x000CC8A5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170061E4 RID: 25060
			// (set) Token: 0x06008CC0 RID: 36032 RVA: 0x000CE6BD File Offset: 0x000CC8BD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170061E5 RID: 25061
			// (set) Token: 0x06008CC1 RID: 36033 RVA: 0x000CE6D5 File Offset: 0x000CC8D5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170061E6 RID: 25062
			// (set) Token: 0x06008CC2 RID: 36034 RVA: 0x000CE6ED File Offset: 0x000CC8ED
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
