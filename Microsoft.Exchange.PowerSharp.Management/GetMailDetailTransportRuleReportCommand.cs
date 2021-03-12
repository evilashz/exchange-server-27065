using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000159 RID: 345
	public class GetMailDetailTransportRuleReportCommand : SyntheticCommandWithPipelineInput<MailDetailTransportRuleReport, MailDetailTransportRuleReport>
	{
		// Token: 0x060021A0 RID: 8608 RVA: 0x00043363 File Offset: 0x00041563
		private GetMailDetailTransportRuleReportCommand() : base("Get-MailDetailTransportRuleReport")
		{
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x00043370 File Offset: 0x00041570
		public GetMailDetailTransportRuleReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x0004337F File Offset: 0x0004157F
		public virtual GetMailDetailTransportRuleReportCommand SetParameters(GetMailDetailTransportRuleReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200015A RID: 346
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000AA7 RID: 2727
			// (set) Token: 0x060021A3 RID: 8611 RVA: 0x00043389 File Offset: 0x00041589
			public virtual MultiValuedProperty<string> TransportRule
			{
				set
				{
					base.PowerSharpParameters["TransportRule"] = value;
				}
			}

			// Token: 0x17000AA8 RID: 2728
			// (set) Token: 0x060021A4 RID: 8612 RVA: 0x0004339C File Offset: 0x0004159C
			public virtual MultiValuedProperty<string> EventType
			{
				set
				{
					base.PowerSharpParameters["EventType"] = value;
				}
			}

			// Token: 0x17000AA9 RID: 2729
			// (set) Token: 0x060021A5 RID: 8613 RVA: 0x000433AF File Offset: 0x000415AF
			public virtual MultiValuedProperty<Guid> MessageTraceId
			{
				set
				{
					base.PowerSharpParameters["MessageTraceId"] = value;
				}
			}

			// Token: 0x17000AAA RID: 2730
			// (set) Token: 0x060021A6 RID: 8614 RVA: 0x000433C2 File Offset: 0x000415C2
			public virtual MultiValuedProperty<Fqdn> Domain
			{
				set
				{
					base.PowerSharpParameters["Domain"] = value;
				}
			}

			// Token: 0x17000AAB RID: 2731
			// (set) Token: 0x060021A7 RID: 8615 RVA: 0x000433D5 File Offset: 0x000415D5
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17000AAC RID: 2732
			// (set) Token: 0x060021A8 RID: 8616 RVA: 0x000433ED File Offset: 0x000415ED
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17000AAD RID: 2733
			// (set) Token: 0x060021A9 RID: 8617 RVA: 0x00043405 File Offset: 0x00041605
			public virtual MultiValuedProperty<string> Direction
			{
				set
				{
					base.PowerSharpParameters["Direction"] = value;
				}
			}

			// Token: 0x17000AAE RID: 2734
			// (set) Token: 0x060021AA RID: 8618 RVA: 0x00043418 File Offset: 0x00041618
			public virtual MultiValuedProperty<string> MessageId
			{
				set
				{
					base.PowerSharpParameters["MessageId"] = value;
				}
			}

			// Token: 0x17000AAF RID: 2735
			// (set) Token: 0x060021AB RID: 8619 RVA: 0x0004342B File Offset: 0x0004162B
			public virtual MultiValuedProperty<string> SenderAddress
			{
				set
				{
					base.PowerSharpParameters["SenderAddress"] = value;
				}
			}

			// Token: 0x17000AB0 RID: 2736
			// (set) Token: 0x060021AC RID: 8620 RVA: 0x0004343E File Offset: 0x0004163E
			public virtual MultiValuedProperty<string> RecipientAddress
			{
				set
				{
					base.PowerSharpParameters["RecipientAddress"] = value;
				}
			}

			// Token: 0x17000AB1 RID: 2737
			// (set) Token: 0x060021AD RID: 8621 RVA: 0x00043451 File Offset: 0x00041651
			public virtual MultiValuedProperty<string> Action
			{
				set
				{
					base.PowerSharpParameters["Action"] = value;
				}
			}

			// Token: 0x17000AB2 RID: 2738
			// (set) Token: 0x060021AE RID: 8622 RVA: 0x00043464 File Offset: 0x00041664
			public virtual int Page
			{
				set
				{
					base.PowerSharpParameters["Page"] = value;
				}
			}

			// Token: 0x17000AB3 RID: 2739
			// (set) Token: 0x060021AF RID: 8623 RVA: 0x0004347C File Offset: 0x0004167C
			public virtual int PageSize
			{
				set
				{
					base.PowerSharpParameters["PageSize"] = value;
				}
			}

			// Token: 0x17000AB4 RID: 2740
			// (set) Token: 0x060021B0 RID: 8624 RVA: 0x00043494 File Offset: 0x00041694
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000AB5 RID: 2741
			// (set) Token: 0x060021B1 RID: 8625 RVA: 0x000434B2 File Offset: 0x000416B2
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17000AB6 RID: 2742
			// (set) Token: 0x060021B2 RID: 8626 RVA: 0x000434C5 File Offset: 0x000416C5
			public virtual string ProbeTag
			{
				set
				{
					base.PowerSharpParameters["ProbeTag"] = value;
				}
			}

			// Token: 0x17000AB7 RID: 2743
			// (set) Token: 0x060021B3 RID: 8627 RVA: 0x000434D8 File Offset: 0x000416D8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000AB8 RID: 2744
			// (set) Token: 0x060021B4 RID: 8628 RVA: 0x000434F0 File Offset: 0x000416F0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000AB9 RID: 2745
			// (set) Token: 0x060021B5 RID: 8629 RVA: 0x00043508 File Offset: 0x00041708
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000ABA RID: 2746
			// (set) Token: 0x060021B6 RID: 8630 RVA: 0x00043520 File Offset: 0x00041720
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
