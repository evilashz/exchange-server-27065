using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002BB RID: 699
	public class GetRecipientStatisticsReportCommand : SyntheticCommandWithPipelineInput<OrganizationIdParameter, OrganizationIdParameter>
	{
		// Token: 0x0600310F RID: 12559 RVA: 0x000579E2 File Offset: 0x00055BE2
		private GetRecipientStatisticsReportCommand() : base("Get-RecipientStatisticsReport")
		{
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x000579EF File Offset: 0x00055BEF
		public GetRecipientStatisticsReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x000579FE File Offset: 0x00055BFE
		public virtual GetRecipientStatisticsReportCommand SetParameters(GetRecipientStatisticsReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002BC RID: 700
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001752 RID: 5970
			// (set) Token: 0x06003112 RID: 12562 RVA: 0x00057A08 File Offset: 0x00055C08
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001753 RID: 5971
			// (set) Token: 0x06003113 RID: 12563 RVA: 0x00057A26 File Offset: 0x00055C26
			public virtual Fqdn ReportingServer
			{
				set
				{
					base.PowerSharpParameters["ReportingServer"] = value;
				}
			}

			// Token: 0x17001754 RID: 5972
			// (set) Token: 0x06003114 RID: 12564 RVA: 0x00057A39 File Offset: 0x00055C39
			public virtual string ReportingDatabase
			{
				set
				{
					base.PowerSharpParameters["ReportingDatabase"] = value;
				}
			}

			// Token: 0x17001755 RID: 5973
			// (set) Token: 0x06003115 RID: 12565 RVA: 0x00057A4C File Offset: 0x00055C4C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001756 RID: 5974
			// (set) Token: 0x06003116 RID: 12566 RVA: 0x00057A5F File Offset: 0x00055C5F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001757 RID: 5975
			// (set) Token: 0x06003117 RID: 12567 RVA: 0x00057A77 File Offset: 0x00055C77
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001758 RID: 5976
			// (set) Token: 0x06003118 RID: 12568 RVA: 0x00057A8F File Offset: 0x00055C8F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001759 RID: 5977
			// (set) Token: 0x06003119 RID: 12569 RVA: 0x00057AA7 File Offset: 0x00055CA7
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
