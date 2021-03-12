using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000157 RID: 343
	public class GetMailDetailSpamReportCommand : SyntheticCommandWithPipelineInput<MailDetailSpamReport, MailDetailSpamReport>
	{
		// Token: 0x06002189 RID: 8585 RVA: 0x00043199 File Offset: 0x00041399
		private GetMailDetailSpamReportCommand() : base("Get-MailDetailSpamReport")
		{
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x000431A6 File Offset: 0x000413A6
		public GetMailDetailSpamReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x000431B5 File Offset: 0x000413B5
		public virtual GetMailDetailSpamReportCommand SetParameters(GetMailDetailSpamReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000158 RID: 344
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000A94 RID: 2708
			// (set) Token: 0x0600218C RID: 8588 RVA: 0x000431BF File Offset: 0x000413BF
			public virtual MultiValuedProperty<string> EventType
			{
				set
				{
					base.PowerSharpParameters["EventType"] = value;
				}
			}

			// Token: 0x17000A95 RID: 2709
			// (set) Token: 0x0600218D RID: 8589 RVA: 0x000431D2 File Offset: 0x000413D2
			public virtual MultiValuedProperty<Guid> MessageTraceId
			{
				set
				{
					base.PowerSharpParameters["MessageTraceId"] = value;
				}
			}

			// Token: 0x17000A96 RID: 2710
			// (set) Token: 0x0600218E RID: 8590 RVA: 0x000431E5 File Offset: 0x000413E5
			public virtual MultiValuedProperty<Fqdn> Domain
			{
				set
				{
					base.PowerSharpParameters["Domain"] = value;
				}
			}

			// Token: 0x17000A97 RID: 2711
			// (set) Token: 0x0600218F RID: 8591 RVA: 0x000431F8 File Offset: 0x000413F8
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17000A98 RID: 2712
			// (set) Token: 0x06002190 RID: 8592 RVA: 0x00043210 File Offset: 0x00041410
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17000A99 RID: 2713
			// (set) Token: 0x06002191 RID: 8593 RVA: 0x00043228 File Offset: 0x00041428
			public virtual MultiValuedProperty<string> Direction
			{
				set
				{
					base.PowerSharpParameters["Direction"] = value;
				}
			}

			// Token: 0x17000A9A RID: 2714
			// (set) Token: 0x06002192 RID: 8594 RVA: 0x0004323B File Offset: 0x0004143B
			public virtual MultiValuedProperty<string> MessageId
			{
				set
				{
					base.PowerSharpParameters["MessageId"] = value;
				}
			}

			// Token: 0x17000A9B RID: 2715
			// (set) Token: 0x06002193 RID: 8595 RVA: 0x0004324E File Offset: 0x0004144E
			public virtual MultiValuedProperty<string> SenderAddress
			{
				set
				{
					base.PowerSharpParameters["SenderAddress"] = value;
				}
			}

			// Token: 0x17000A9C RID: 2716
			// (set) Token: 0x06002194 RID: 8596 RVA: 0x00043261 File Offset: 0x00041461
			public virtual MultiValuedProperty<string> RecipientAddress
			{
				set
				{
					base.PowerSharpParameters["RecipientAddress"] = value;
				}
			}

			// Token: 0x17000A9D RID: 2717
			// (set) Token: 0x06002195 RID: 8597 RVA: 0x00043274 File Offset: 0x00041474
			public virtual MultiValuedProperty<string> Action
			{
				set
				{
					base.PowerSharpParameters["Action"] = value;
				}
			}

			// Token: 0x17000A9E RID: 2718
			// (set) Token: 0x06002196 RID: 8598 RVA: 0x00043287 File Offset: 0x00041487
			public virtual int Page
			{
				set
				{
					base.PowerSharpParameters["Page"] = value;
				}
			}

			// Token: 0x17000A9F RID: 2719
			// (set) Token: 0x06002197 RID: 8599 RVA: 0x0004329F File Offset: 0x0004149F
			public virtual int PageSize
			{
				set
				{
					base.PowerSharpParameters["PageSize"] = value;
				}
			}

			// Token: 0x17000AA0 RID: 2720
			// (set) Token: 0x06002198 RID: 8600 RVA: 0x000432B7 File Offset: 0x000414B7
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000AA1 RID: 2721
			// (set) Token: 0x06002199 RID: 8601 RVA: 0x000432D5 File Offset: 0x000414D5
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17000AA2 RID: 2722
			// (set) Token: 0x0600219A RID: 8602 RVA: 0x000432E8 File Offset: 0x000414E8
			public virtual string ProbeTag
			{
				set
				{
					base.PowerSharpParameters["ProbeTag"] = value;
				}
			}

			// Token: 0x17000AA3 RID: 2723
			// (set) Token: 0x0600219B RID: 8603 RVA: 0x000432FB File Offset: 0x000414FB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000AA4 RID: 2724
			// (set) Token: 0x0600219C RID: 8604 RVA: 0x00043313 File Offset: 0x00041513
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000AA5 RID: 2725
			// (set) Token: 0x0600219D RID: 8605 RVA: 0x0004332B File Offset: 0x0004152B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000AA6 RID: 2726
			// (set) Token: 0x0600219E RID: 8606 RVA: 0x00043343 File Offset: 0x00041543
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
