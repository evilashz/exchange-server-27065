using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200014D RID: 333
	public class GetDlpDetectionsReportCommand : SyntheticCommandWithPipelineInput<DlpReport, DlpReport>
	{
		// Token: 0x0600211E RID: 8478 RVA: 0x00042949 File Offset: 0x00040B49
		private GetDlpDetectionsReportCommand() : base("Get-DlpDetectionsReport")
		{
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x00042956 File Offset: 0x00040B56
		public GetDlpDetectionsReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x00042965 File Offset: 0x00040B65
		public virtual GetDlpDetectionsReportCommand SetParameters(GetDlpDetectionsReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200014E RID: 334
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000A3D RID: 2621
			// (set) Token: 0x06002121 RID: 8481 RVA: 0x0004296F File Offset: 0x00040B6F
			public virtual MultiValuedProperty<string> EventType
			{
				set
				{
					base.PowerSharpParameters["EventType"] = value;
				}
			}

			// Token: 0x17000A3E RID: 2622
			// (set) Token: 0x06002122 RID: 8482 RVA: 0x00042982 File Offset: 0x00040B82
			public virtual MultiValuedProperty<string> DlpPolicy
			{
				set
				{
					base.PowerSharpParameters["DlpPolicy"] = value;
				}
			}

			// Token: 0x17000A3F RID: 2623
			// (set) Token: 0x06002123 RID: 8483 RVA: 0x00042995 File Offset: 0x00040B95
			public virtual MultiValuedProperty<string> TransportRule
			{
				set
				{
					base.PowerSharpParameters["TransportRule"] = value;
				}
			}

			// Token: 0x17000A40 RID: 2624
			// (set) Token: 0x06002124 RID: 8484 RVA: 0x000429A8 File Offset: 0x00040BA8
			public virtual MultiValuedProperty<string> Action
			{
				set
				{
					base.PowerSharpParameters["Action"] = value;
				}
			}

			// Token: 0x17000A41 RID: 2625
			// (set) Token: 0x06002125 RID: 8485 RVA: 0x000429BB File Offset: 0x00040BBB
			public virtual MultiValuedProperty<string> SummarizeBy
			{
				set
				{
					base.PowerSharpParameters["SummarizeBy"] = value;
				}
			}

			// Token: 0x17000A42 RID: 2626
			// (set) Token: 0x06002126 RID: 8486 RVA: 0x000429CE File Offset: 0x00040BCE
			public virtual MultiValuedProperty<string> Source
			{
				set
				{
					base.PowerSharpParameters["Source"] = value;
				}
			}

			// Token: 0x17000A43 RID: 2627
			// (set) Token: 0x06002127 RID: 8487 RVA: 0x000429E1 File Offset: 0x00040BE1
			public virtual MultiValuedProperty<Fqdn> Domain
			{
				set
				{
					base.PowerSharpParameters["Domain"] = value;
				}
			}

			// Token: 0x17000A44 RID: 2628
			// (set) Token: 0x06002128 RID: 8488 RVA: 0x000429F4 File Offset: 0x00040BF4
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17000A45 RID: 2629
			// (set) Token: 0x06002129 RID: 8489 RVA: 0x00042A0C File Offset: 0x00040C0C
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17000A46 RID: 2630
			// (set) Token: 0x0600212A RID: 8490 RVA: 0x00042A24 File Offset: 0x00040C24
			public virtual MultiValuedProperty<string> Direction
			{
				set
				{
					base.PowerSharpParameters["Direction"] = value;
				}
			}

			// Token: 0x17000A47 RID: 2631
			// (set) Token: 0x0600212B RID: 8491 RVA: 0x00042A37 File Offset: 0x00040C37
			public virtual string AggregateBy
			{
				set
				{
					base.PowerSharpParameters["AggregateBy"] = value;
				}
			}

			// Token: 0x17000A48 RID: 2632
			// (set) Token: 0x0600212C RID: 8492 RVA: 0x00042A4A File Offset: 0x00040C4A
			public virtual int Page
			{
				set
				{
					base.PowerSharpParameters["Page"] = value;
				}
			}

			// Token: 0x17000A49 RID: 2633
			// (set) Token: 0x0600212D RID: 8493 RVA: 0x00042A62 File Offset: 0x00040C62
			public virtual int PageSize
			{
				set
				{
					base.PowerSharpParameters["PageSize"] = value;
				}
			}

			// Token: 0x17000A4A RID: 2634
			// (set) Token: 0x0600212E RID: 8494 RVA: 0x00042A7A File Offset: 0x00040C7A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000A4B RID: 2635
			// (set) Token: 0x0600212F RID: 8495 RVA: 0x00042A98 File Offset: 0x00040C98
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17000A4C RID: 2636
			// (set) Token: 0x06002130 RID: 8496 RVA: 0x00042AAB File Offset: 0x00040CAB
			public virtual string ProbeTag
			{
				set
				{
					base.PowerSharpParameters["ProbeTag"] = value;
				}
			}

			// Token: 0x17000A4D RID: 2637
			// (set) Token: 0x06002131 RID: 8497 RVA: 0x00042ABE File Offset: 0x00040CBE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000A4E RID: 2638
			// (set) Token: 0x06002132 RID: 8498 RVA: 0x00042AD6 File Offset: 0x00040CD6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000A4F RID: 2639
			// (set) Token: 0x06002133 RID: 8499 RVA: 0x00042AEE File Offset: 0x00040CEE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000A50 RID: 2640
			// (set) Token: 0x06002134 RID: 8500 RVA: 0x00042B06 File Offset: 0x00040D06
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
