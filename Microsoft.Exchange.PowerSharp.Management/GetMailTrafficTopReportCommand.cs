using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000163 RID: 355
	public class GetMailTrafficTopReportCommand : SyntheticCommandWithPipelineInput<MailTrafficTopReport, MailTrafficTopReport>
	{
		// Token: 0x06002204 RID: 8708 RVA: 0x00043B24 File Offset: 0x00041D24
		private GetMailTrafficTopReportCommand() : base("Get-MailTrafficTopReport")
		{
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x00043B31 File Offset: 0x00041D31
		public GetMailTrafficTopReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x00043B40 File Offset: 0x00041D40
		public virtual GetMailTrafficTopReportCommand SetParameters(GetMailTrafficTopReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000164 RID: 356
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000AF7 RID: 2807
			// (set) Token: 0x06002207 RID: 8711 RVA: 0x00043B4A File Offset: 0x00041D4A
			public virtual MultiValuedProperty<string> EventType
			{
				set
				{
					base.PowerSharpParameters["EventType"] = value;
				}
			}

			// Token: 0x17000AF8 RID: 2808
			// (set) Token: 0x06002208 RID: 8712 RVA: 0x00043B5D File Offset: 0x00041D5D
			public virtual MultiValuedProperty<string> SummarizeBy
			{
				set
				{
					base.PowerSharpParameters["SummarizeBy"] = value;
				}
			}

			// Token: 0x17000AF9 RID: 2809
			// (set) Token: 0x06002209 RID: 8713 RVA: 0x00043B70 File Offset: 0x00041D70
			public virtual MultiValuedProperty<Fqdn> Domain
			{
				set
				{
					base.PowerSharpParameters["Domain"] = value;
				}
			}

			// Token: 0x17000AFA RID: 2810
			// (set) Token: 0x0600220A RID: 8714 RVA: 0x00043B83 File Offset: 0x00041D83
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17000AFB RID: 2811
			// (set) Token: 0x0600220B RID: 8715 RVA: 0x00043B9B File Offset: 0x00041D9B
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17000AFC RID: 2812
			// (set) Token: 0x0600220C RID: 8716 RVA: 0x00043BB3 File Offset: 0x00041DB3
			public virtual MultiValuedProperty<string> Direction
			{
				set
				{
					base.PowerSharpParameters["Direction"] = value;
				}
			}

			// Token: 0x17000AFD RID: 2813
			// (set) Token: 0x0600220D RID: 8717 RVA: 0x00043BC6 File Offset: 0x00041DC6
			public virtual string AggregateBy
			{
				set
				{
					base.PowerSharpParameters["AggregateBy"] = value;
				}
			}

			// Token: 0x17000AFE RID: 2814
			// (set) Token: 0x0600220E RID: 8718 RVA: 0x00043BD9 File Offset: 0x00041DD9
			public virtual int Page
			{
				set
				{
					base.PowerSharpParameters["Page"] = value;
				}
			}

			// Token: 0x17000AFF RID: 2815
			// (set) Token: 0x0600220F RID: 8719 RVA: 0x00043BF1 File Offset: 0x00041DF1
			public virtual int PageSize
			{
				set
				{
					base.PowerSharpParameters["PageSize"] = value;
				}
			}

			// Token: 0x17000B00 RID: 2816
			// (set) Token: 0x06002210 RID: 8720 RVA: 0x00043C09 File Offset: 0x00041E09
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000B01 RID: 2817
			// (set) Token: 0x06002211 RID: 8721 RVA: 0x00043C27 File Offset: 0x00041E27
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17000B02 RID: 2818
			// (set) Token: 0x06002212 RID: 8722 RVA: 0x00043C3A File Offset: 0x00041E3A
			public virtual string ProbeTag
			{
				set
				{
					base.PowerSharpParameters["ProbeTag"] = value;
				}
			}

			// Token: 0x17000B03 RID: 2819
			// (set) Token: 0x06002213 RID: 8723 RVA: 0x00043C4D File Offset: 0x00041E4D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000B04 RID: 2820
			// (set) Token: 0x06002214 RID: 8724 RVA: 0x00043C65 File Offset: 0x00041E65
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000B05 RID: 2821
			// (set) Token: 0x06002215 RID: 8725 RVA: 0x00043C7D File Offset: 0x00041E7D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000B06 RID: 2822
			// (set) Token: 0x06002216 RID: 8726 RVA: 0x00043C95 File Offset: 0x00041E95
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
