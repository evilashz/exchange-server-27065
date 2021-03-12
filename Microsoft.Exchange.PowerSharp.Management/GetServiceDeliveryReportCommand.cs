using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000171 RID: 369
	public class GetServiceDeliveryReportCommand : SyntheticCommandWithPipelineInput<ServiceDeliveryReport, ServiceDeliveryReport>
	{
		// Token: 0x06002279 RID: 8825 RVA: 0x00044448 File Offset: 0x00042648
		private GetServiceDeliveryReportCommand() : base("Get-ServiceDeliveryReport")
		{
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x00044455 File Offset: 0x00042655
		public GetServiceDeliveryReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x00044464 File Offset: 0x00042664
		public virtual GetServiceDeliveryReportCommand SetParameters(GetServiceDeliveryReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000172 RID: 370
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000B50 RID: 2896
			// (set) Token: 0x0600227C RID: 8828 RVA: 0x0004446E File Offset: 0x0004266E
			public virtual SmtpAddress Recipient
			{
				set
				{
					base.PowerSharpParameters["Recipient"] = value;
				}
			}

			// Token: 0x17000B51 RID: 2897
			// (set) Token: 0x0600227D RID: 8829 RVA: 0x00044486 File Offset: 0x00042686
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000B52 RID: 2898
			// (set) Token: 0x0600227E RID: 8830 RVA: 0x000444A4 File Offset: 0x000426A4
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17000B53 RID: 2899
			// (set) Token: 0x0600227F RID: 8831 RVA: 0x000444B7 File Offset: 0x000426B7
			public virtual string ProbeTag
			{
				set
				{
					base.PowerSharpParameters["ProbeTag"] = value;
				}
			}

			// Token: 0x17000B54 RID: 2900
			// (set) Token: 0x06002280 RID: 8832 RVA: 0x000444CA File Offset: 0x000426CA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000B55 RID: 2901
			// (set) Token: 0x06002281 RID: 8833 RVA: 0x000444E2 File Offset: 0x000426E2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000B56 RID: 2902
			// (set) Token: 0x06002282 RID: 8834 RVA: 0x000444FA File Offset: 0x000426FA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000B57 RID: 2903
			// (set) Token: 0x06002283 RID: 8835 RVA: 0x00044512 File Offset: 0x00042712
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
