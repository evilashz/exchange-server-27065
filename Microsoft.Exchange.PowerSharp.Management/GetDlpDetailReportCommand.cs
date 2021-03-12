using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200014B RID: 331
	public class GetDlpDetailReportCommand : SyntheticCommandWithPipelineInput<DlpDetailReport, DlpDetailReport>
	{
		// Token: 0x06002103 RID: 8451 RVA: 0x00042733 File Offset: 0x00040933
		private GetDlpDetailReportCommand() : base("Get-DlpDetailReport")
		{
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x00042740 File Offset: 0x00040940
		public GetDlpDetailReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x0004274F File Offset: 0x0004094F
		public virtual GetDlpDetailReportCommand SetParameters(GetDlpDetailReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200014C RID: 332
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000A26 RID: 2598
			// (set) Token: 0x06002106 RID: 8454 RVA: 0x00042759 File Offset: 0x00040959
			public virtual MultiValuedProperty<string> DlpPolicy
			{
				set
				{
					base.PowerSharpParameters["DlpPolicy"] = value;
				}
			}

			// Token: 0x17000A27 RID: 2599
			// (set) Token: 0x06002107 RID: 8455 RVA: 0x0004276C File Offset: 0x0004096C
			public virtual MultiValuedProperty<string> TransportRule
			{
				set
				{
					base.PowerSharpParameters["TransportRule"] = value;
				}
			}

			// Token: 0x17000A28 RID: 2600
			// (set) Token: 0x06002108 RID: 8456 RVA: 0x0004277F File Offset: 0x0004097F
			public virtual MultiValuedProperty<string> EventType
			{
				set
				{
					base.PowerSharpParameters["EventType"] = value;
				}
			}

			// Token: 0x17000A29 RID: 2601
			// (set) Token: 0x06002109 RID: 8457 RVA: 0x00042792 File Offset: 0x00040992
			public virtual MultiValuedProperty<string> Actor
			{
				set
				{
					base.PowerSharpParameters["Actor"] = value;
				}
			}

			// Token: 0x17000A2A RID: 2602
			// (set) Token: 0x0600210A RID: 8458 RVA: 0x000427A5 File Offset: 0x000409A5
			public virtual MultiValuedProperty<string> Source
			{
				set
				{
					base.PowerSharpParameters["Source"] = value;
				}
			}

			// Token: 0x17000A2B RID: 2603
			// (set) Token: 0x0600210B RID: 8459 RVA: 0x000427B8 File Offset: 0x000409B8
			public virtual MultiValuedProperty<Guid> MessageTraceId
			{
				set
				{
					base.PowerSharpParameters["MessageTraceId"] = value;
				}
			}

			// Token: 0x17000A2C RID: 2604
			// (set) Token: 0x0600210C RID: 8460 RVA: 0x000427CB File Offset: 0x000409CB
			public virtual MultiValuedProperty<Fqdn> Domain
			{
				set
				{
					base.PowerSharpParameters["Domain"] = value;
				}
			}

			// Token: 0x17000A2D RID: 2605
			// (set) Token: 0x0600210D RID: 8461 RVA: 0x000427DE File Offset: 0x000409DE
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17000A2E RID: 2606
			// (set) Token: 0x0600210E RID: 8462 RVA: 0x000427F6 File Offset: 0x000409F6
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17000A2F RID: 2607
			// (set) Token: 0x0600210F RID: 8463 RVA: 0x0004280E File Offset: 0x00040A0E
			public virtual MultiValuedProperty<string> Direction
			{
				set
				{
					base.PowerSharpParameters["Direction"] = value;
				}
			}

			// Token: 0x17000A30 RID: 2608
			// (set) Token: 0x06002110 RID: 8464 RVA: 0x00042821 File Offset: 0x00040A21
			public virtual MultiValuedProperty<string> MessageId
			{
				set
				{
					base.PowerSharpParameters["MessageId"] = value;
				}
			}

			// Token: 0x17000A31 RID: 2609
			// (set) Token: 0x06002111 RID: 8465 RVA: 0x00042834 File Offset: 0x00040A34
			public virtual MultiValuedProperty<string> SenderAddress
			{
				set
				{
					base.PowerSharpParameters["SenderAddress"] = value;
				}
			}

			// Token: 0x17000A32 RID: 2610
			// (set) Token: 0x06002112 RID: 8466 RVA: 0x00042847 File Offset: 0x00040A47
			public virtual MultiValuedProperty<string> RecipientAddress
			{
				set
				{
					base.PowerSharpParameters["RecipientAddress"] = value;
				}
			}

			// Token: 0x17000A33 RID: 2611
			// (set) Token: 0x06002113 RID: 8467 RVA: 0x0004285A File Offset: 0x00040A5A
			public virtual MultiValuedProperty<string> Action
			{
				set
				{
					base.PowerSharpParameters["Action"] = value;
				}
			}

			// Token: 0x17000A34 RID: 2612
			// (set) Token: 0x06002114 RID: 8468 RVA: 0x0004286D File Offset: 0x00040A6D
			public virtual int Page
			{
				set
				{
					base.PowerSharpParameters["Page"] = value;
				}
			}

			// Token: 0x17000A35 RID: 2613
			// (set) Token: 0x06002115 RID: 8469 RVA: 0x00042885 File Offset: 0x00040A85
			public virtual int PageSize
			{
				set
				{
					base.PowerSharpParameters["PageSize"] = value;
				}
			}

			// Token: 0x17000A36 RID: 2614
			// (set) Token: 0x06002116 RID: 8470 RVA: 0x0004289D File Offset: 0x00040A9D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000A37 RID: 2615
			// (set) Token: 0x06002117 RID: 8471 RVA: 0x000428BB File Offset: 0x00040ABB
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17000A38 RID: 2616
			// (set) Token: 0x06002118 RID: 8472 RVA: 0x000428CE File Offset: 0x00040ACE
			public virtual string ProbeTag
			{
				set
				{
					base.PowerSharpParameters["ProbeTag"] = value;
				}
			}

			// Token: 0x17000A39 RID: 2617
			// (set) Token: 0x06002119 RID: 8473 RVA: 0x000428E1 File Offset: 0x00040AE1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000A3A RID: 2618
			// (set) Token: 0x0600211A RID: 8474 RVA: 0x000428F9 File Offset: 0x00040AF9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000A3B RID: 2619
			// (set) Token: 0x0600211B RID: 8475 RVA: 0x00042911 File Offset: 0x00040B11
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000A3C RID: 2620
			// (set) Token: 0x0600211C RID: 8476 RVA: 0x00042929 File Offset: 0x00040B29
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
