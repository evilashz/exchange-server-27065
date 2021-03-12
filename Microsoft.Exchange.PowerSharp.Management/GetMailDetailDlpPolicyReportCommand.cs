using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000151 RID: 337
	public class GetMailDetailDlpPolicyReportCommand : SyntheticCommandWithPipelineInput<MailDetailDlpPolicyReport, MailDetailDlpPolicyReport>
	{
		// Token: 0x06002143 RID: 8515 RVA: 0x00042C28 File Offset: 0x00040E28
		private GetMailDetailDlpPolicyReportCommand() : base("Get-MailDetailDlpPolicyReport")
		{
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x00042C35 File Offset: 0x00040E35
		public GetMailDetailDlpPolicyReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x00042C44 File Offset: 0x00040E44
		public virtual GetMailDetailDlpPolicyReportCommand SetParameters(GetMailDetailDlpPolicyReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000152 RID: 338
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000A5A RID: 2650
			// (set) Token: 0x06002146 RID: 8518 RVA: 0x00042C4E File Offset: 0x00040E4E
			public virtual MultiValuedProperty<string> DlpPolicy
			{
				set
				{
					base.PowerSharpParameters["DlpPolicy"] = value;
				}
			}

			// Token: 0x17000A5B RID: 2651
			// (set) Token: 0x06002147 RID: 8519 RVA: 0x00042C61 File Offset: 0x00040E61
			public virtual MultiValuedProperty<string> TransportRule
			{
				set
				{
					base.PowerSharpParameters["TransportRule"] = value;
				}
			}

			// Token: 0x17000A5C RID: 2652
			// (set) Token: 0x06002148 RID: 8520 RVA: 0x00042C74 File Offset: 0x00040E74
			public virtual MultiValuedProperty<string> EventType
			{
				set
				{
					base.PowerSharpParameters["EventType"] = value;
				}
			}

			// Token: 0x17000A5D RID: 2653
			// (set) Token: 0x06002149 RID: 8521 RVA: 0x00042C87 File Offset: 0x00040E87
			public virtual MultiValuedProperty<Guid> MessageTraceId
			{
				set
				{
					base.PowerSharpParameters["MessageTraceId"] = value;
				}
			}

			// Token: 0x17000A5E RID: 2654
			// (set) Token: 0x0600214A RID: 8522 RVA: 0x00042C9A File Offset: 0x00040E9A
			public virtual MultiValuedProperty<Fqdn> Domain
			{
				set
				{
					base.PowerSharpParameters["Domain"] = value;
				}
			}

			// Token: 0x17000A5F RID: 2655
			// (set) Token: 0x0600214B RID: 8523 RVA: 0x00042CAD File Offset: 0x00040EAD
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17000A60 RID: 2656
			// (set) Token: 0x0600214C RID: 8524 RVA: 0x00042CC5 File Offset: 0x00040EC5
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17000A61 RID: 2657
			// (set) Token: 0x0600214D RID: 8525 RVA: 0x00042CDD File Offset: 0x00040EDD
			public virtual MultiValuedProperty<string> Direction
			{
				set
				{
					base.PowerSharpParameters["Direction"] = value;
				}
			}

			// Token: 0x17000A62 RID: 2658
			// (set) Token: 0x0600214E RID: 8526 RVA: 0x00042CF0 File Offset: 0x00040EF0
			public virtual MultiValuedProperty<string> MessageId
			{
				set
				{
					base.PowerSharpParameters["MessageId"] = value;
				}
			}

			// Token: 0x17000A63 RID: 2659
			// (set) Token: 0x0600214F RID: 8527 RVA: 0x00042D03 File Offset: 0x00040F03
			public virtual MultiValuedProperty<string> SenderAddress
			{
				set
				{
					base.PowerSharpParameters["SenderAddress"] = value;
				}
			}

			// Token: 0x17000A64 RID: 2660
			// (set) Token: 0x06002150 RID: 8528 RVA: 0x00042D16 File Offset: 0x00040F16
			public virtual MultiValuedProperty<string> RecipientAddress
			{
				set
				{
					base.PowerSharpParameters["RecipientAddress"] = value;
				}
			}

			// Token: 0x17000A65 RID: 2661
			// (set) Token: 0x06002151 RID: 8529 RVA: 0x00042D29 File Offset: 0x00040F29
			public virtual MultiValuedProperty<string> Action
			{
				set
				{
					base.PowerSharpParameters["Action"] = value;
				}
			}

			// Token: 0x17000A66 RID: 2662
			// (set) Token: 0x06002152 RID: 8530 RVA: 0x00042D3C File Offset: 0x00040F3C
			public virtual int Page
			{
				set
				{
					base.PowerSharpParameters["Page"] = value;
				}
			}

			// Token: 0x17000A67 RID: 2663
			// (set) Token: 0x06002153 RID: 8531 RVA: 0x00042D54 File Offset: 0x00040F54
			public virtual int PageSize
			{
				set
				{
					base.PowerSharpParameters["PageSize"] = value;
				}
			}

			// Token: 0x17000A68 RID: 2664
			// (set) Token: 0x06002154 RID: 8532 RVA: 0x00042D6C File Offset: 0x00040F6C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000A69 RID: 2665
			// (set) Token: 0x06002155 RID: 8533 RVA: 0x00042D8A File Offset: 0x00040F8A
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17000A6A RID: 2666
			// (set) Token: 0x06002156 RID: 8534 RVA: 0x00042D9D File Offset: 0x00040F9D
			public virtual string ProbeTag
			{
				set
				{
					base.PowerSharpParameters["ProbeTag"] = value;
				}
			}

			// Token: 0x17000A6B RID: 2667
			// (set) Token: 0x06002157 RID: 8535 RVA: 0x00042DB0 File Offset: 0x00040FB0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000A6C RID: 2668
			// (set) Token: 0x06002158 RID: 8536 RVA: 0x00042DC8 File Offset: 0x00040FC8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000A6D RID: 2669
			// (set) Token: 0x06002159 RID: 8537 RVA: 0x00042DE0 File Offset: 0x00040FE0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000A6E RID: 2670
			// (set) Token: 0x0600215A RID: 8538 RVA: 0x00042DF8 File Offset: 0x00040FF8
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
