using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200015B RID: 347
	public class GetMailFilterListReportCommand : SyntheticCommandWithPipelineInput<MailFilterListReport, MailFilterListReport>
	{
		// Token: 0x060021B8 RID: 8632 RVA: 0x00043540 File Offset: 0x00041740
		private GetMailFilterListReportCommand() : base("Get-MailFilterListReport")
		{
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x0004354D File Offset: 0x0004174D
		public GetMailFilterListReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x0004355C File Offset: 0x0004175C
		public virtual GetMailFilterListReportCommand SetParameters(GetMailFilterListReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200015C RID: 348
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000ABB RID: 2747
			// (set) Token: 0x060021BB RID: 8635 RVA: 0x00043566 File Offset: 0x00041766
			public virtual MultiValuedProperty<Fqdn> Domain
			{
				set
				{
					base.PowerSharpParameters["Domain"] = value;
				}
			}

			// Token: 0x17000ABC RID: 2748
			// (set) Token: 0x060021BC RID: 8636 RVA: 0x00043579 File Offset: 0x00041779
			public virtual MultiValuedProperty<string> SelectionTarget
			{
				set
				{
					base.PowerSharpParameters["SelectionTarget"] = value;
				}
			}

			// Token: 0x17000ABD RID: 2749
			// (set) Token: 0x060021BD RID: 8637 RVA: 0x0004358C File Offset: 0x0004178C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000ABE RID: 2750
			// (set) Token: 0x060021BE RID: 8638 RVA: 0x000435AA File Offset: 0x000417AA
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17000ABF RID: 2751
			// (set) Token: 0x060021BF RID: 8639 RVA: 0x000435BD File Offset: 0x000417BD
			public virtual string ProbeTag
			{
				set
				{
					base.PowerSharpParameters["ProbeTag"] = value;
				}
			}

			// Token: 0x17000AC0 RID: 2752
			// (set) Token: 0x060021C0 RID: 8640 RVA: 0x000435D0 File Offset: 0x000417D0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000AC1 RID: 2753
			// (set) Token: 0x060021C1 RID: 8641 RVA: 0x000435E8 File Offset: 0x000417E8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000AC2 RID: 2754
			// (set) Token: 0x060021C2 RID: 8642 RVA: 0x00043600 File Offset: 0x00041800
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000AC3 RID: 2755
			// (set) Token: 0x060021C3 RID: 8643 RVA: 0x00043618 File Offset: 0x00041818
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
