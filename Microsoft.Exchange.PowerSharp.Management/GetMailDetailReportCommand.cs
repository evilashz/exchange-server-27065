using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000155 RID: 341
	public class GetMailDetailReportCommand : SyntheticCommandWithPipelineInput<MailDetailReport, MailDetailReport>
	{
		// Token: 0x06002174 RID: 8564 RVA: 0x00042FF5 File Offset: 0x000411F5
		private GetMailDetailReportCommand() : base("Get-MailDetailReport")
		{
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x00043002 File Offset: 0x00041202
		public GetMailDetailReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x00043011 File Offset: 0x00041211
		public virtual GetMailDetailReportCommand SetParameters(GetMailDetailReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000156 RID: 342
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000A83 RID: 2691
			// (set) Token: 0x06002177 RID: 8567 RVA: 0x0004301B File Offset: 0x0004121B
			public virtual MultiValuedProperty<string> EventType
			{
				set
				{
					base.PowerSharpParameters["EventType"] = value;
				}
			}

			// Token: 0x17000A84 RID: 2692
			// (set) Token: 0x06002178 RID: 8568 RVA: 0x0004302E File Offset: 0x0004122E
			public virtual MultiValuedProperty<string> MessageId
			{
				set
				{
					base.PowerSharpParameters["MessageId"] = value;
				}
			}

			// Token: 0x17000A85 RID: 2693
			// (set) Token: 0x06002179 RID: 8569 RVA: 0x00043041 File Offset: 0x00041241
			public virtual MultiValuedProperty<Guid> MessageTraceId
			{
				set
				{
					base.PowerSharpParameters["MessageTraceId"] = value;
				}
			}

			// Token: 0x17000A86 RID: 2694
			// (set) Token: 0x0600217A RID: 8570 RVA: 0x00043054 File Offset: 0x00041254
			public virtual MultiValuedProperty<Fqdn> Domain
			{
				set
				{
					base.PowerSharpParameters["Domain"] = value;
				}
			}

			// Token: 0x17000A87 RID: 2695
			// (set) Token: 0x0600217B RID: 8571 RVA: 0x00043067 File Offset: 0x00041267
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17000A88 RID: 2696
			// (set) Token: 0x0600217C RID: 8572 RVA: 0x0004307F File Offset: 0x0004127F
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17000A89 RID: 2697
			// (set) Token: 0x0600217D RID: 8573 RVA: 0x00043097 File Offset: 0x00041297
			public virtual MultiValuedProperty<string> Direction
			{
				set
				{
					base.PowerSharpParameters["Direction"] = value;
				}
			}

			// Token: 0x17000A8A RID: 2698
			// (set) Token: 0x0600217E RID: 8574 RVA: 0x000430AA File Offset: 0x000412AA
			public virtual string AggregateBy
			{
				set
				{
					base.PowerSharpParameters["AggregateBy"] = value;
				}
			}

			// Token: 0x17000A8B RID: 2699
			// (set) Token: 0x0600217F RID: 8575 RVA: 0x000430BD File Offset: 0x000412BD
			public virtual int Page
			{
				set
				{
					base.PowerSharpParameters["Page"] = value;
				}
			}

			// Token: 0x17000A8C RID: 2700
			// (set) Token: 0x06002180 RID: 8576 RVA: 0x000430D5 File Offset: 0x000412D5
			public virtual int PageSize
			{
				set
				{
					base.PowerSharpParameters["PageSize"] = value;
				}
			}

			// Token: 0x17000A8D RID: 2701
			// (set) Token: 0x06002181 RID: 8577 RVA: 0x000430ED File Offset: 0x000412ED
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000A8E RID: 2702
			// (set) Token: 0x06002182 RID: 8578 RVA: 0x0004310B File Offset: 0x0004130B
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17000A8F RID: 2703
			// (set) Token: 0x06002183 RID: 8579 RVA: 0x0004311E File Offset: 0x0004131E
			public virtual string ProbeTag
			{
				set
				{
					base.PowerSharpParameters["ProbeTag"] = value;
				}
			}

			// Token: 0x17000A90 RID: 2704
			// (set) Token: 0x06002184 RID: 8580 RVA: 0x00043131 File Offset: 0x00041331
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000A91 RID: 2705
			// (set) Token: 0x06002185 RID: 8581 RVA: 0x00043149 File Offset: 0x00041349
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000A92 RID: 2706
			// (set) Token: 0x06002186 RID: 8582 RVA: 0x00043161 File Offset: 0x00041361
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000A93 RID: 2707
			// (set) Token: 0x06002187 RID: 8583 RVA: 0x00043179 File Offset: 0x00041379
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
