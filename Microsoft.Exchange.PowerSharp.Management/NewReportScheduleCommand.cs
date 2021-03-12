using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200017F RID: 383
	public class NewReportScheduleCommand : SyntheticCommandWithPipelineInput<ReportSchedule, ReportSchedule>
	{
		// Token: 0x060022CE RID: 8910 RVA: 0x00044AD0 File Offset: 0x00042CD0
		private NewReportScheduleCommand() : base("New-ReportSchedule")
		{
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x00044ADD File Offset: 0x00042CDD
		public NewReportScheduleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x00044AEC File Offset: 0x00042CEC
		public virtual NewReportScheduleCommand SetParameters(NewReportScheduleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000180 RID: 384
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000B89 RID: 2953
			// (set) Token: 0x060022D1 RID: 8913 RVA: 0x00044AF6 File Offset: 0x00042CF6
			public virtual string DeliveryStatus
			{
				set
				{
					base.PowerSharpParameters["DeliveryStatus"] = value;
				}
			}

			// Token: 0x17000B8A RID: 2954
			// (set) Token: 0x060022D2 RID: 8914 RVA: 0x00044B09 File Offset: 0x00042D09
			public virtual ReportDirection Direction
			{
				set
				{
					base.PowerSharpParameters["Direction"] = value;
				}
			}

			// Token: 0x17000B8B RID: 2955
			// (set) Token: 0x060022D3 RID: 8915 RVA: 0x00044B21 File Offset: 0x00042D21
			public virtual MultiValuedProperty<Guid> DLPPolicy
			{
				set
				{
					base.PowerSharpParameters["DLPPolicy"] = value;
				}
			}

			// Token: 0x17000B8C RID: 2956
			// (set) Token: 0x060022D4 RID: 8916 RVA: 0x00044B34 File Offset: 0x00042D34
			public virtual string Domain
			{
				set
				{
					base.PowerSharpParameters["Domain"] = value;
				}
			}

			// Token: 0x17000B8D RID: 2957
			// (set) Token: 0x060022D5 RID: 8917 RVA: 0x00044B47 File Offset: 0x00042D47
			public virtual DateTime EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17000B8E RID: 2958
			// (set) Token: 0x060022D6 RID: 8918 RVA: 0x00044B5F File Offset: 0x00042D5F
			public virtual CultureInfo Locale
			{
				set
				{
					base.PowerSharpParameters["Locale"] = value;
				}
			}

			// Token: 0x17000B8F RID: 2959
			// (set) Token: 0x060022D7 RID: 8919 RVA: 0x00044B72 File Offset: 0x00042D72
			public virtual MultiValuedProperty<Guid> MalwareName
			{
				set
				{
					base.PowerSharpParameters["MalwareName"] = value;
				}
			}

			// Token: 0x17000B90 RID: 2960
			// (set) Token: 0x060022D8 RID: 8920 RVA: 0x00044B85 File Offset: 0x00042D85
			public virtual MultiValuedProperty<string> MessageID
			{
				set
				{
					base.PowerSharpParameters["MessageID"] = value;
				}
			}

			// Token: 0x17000B91 RID: 2961
			// (set) Token: 0x060022D9 RID: 8921 RVA: 0x00044B98 File Offset: 0x00042D98
			public virtual MultiValuedProperty<string> NotifyAddress
			{
				set
				{
					base.PowerSharpParameters["NotifyAddress"] = value;
				}
			}

			// Token: 0x17000B92 RID: 2962
			// (set) Token: 0x060022DA RID: 8922 RVA: 0x00044BAB File Offset: 0x00042DAB
			public virtual string OriginalClientIP
			{
				set
				{
					base.PowerSharpParameters["OriginalClientIP"] = value;
				}
			}

			// Token: 0x17000B93 RID: 2963
			// (set) Token: 0x060022DB RID: 8923 RVA: 0x00044BBE File Offset: 0x00042DBE
			public virtual MultiValuedProperty<string> RecipientAddress
			{
				set
				{
					base.PowerSharpParameters["RecipientAddress"] = value;
				}
			}

			// Token: 0x17000B94 RID: 2964
			// (set) Token: 0x060022DC RID: 8924 RVA: 0x00044BD1 File Offset: 0x00042DD1
			public virtual ReportRecurrence Recurrence
			{
				set
				{
					base.PowerSharpParameters["Recurrence"] = value;
				}
			}

			// Token: 0x17000B95 RID: 2965
			// (set) Token: 0x060022DD RID: 8925 RVA: 0x00044BE9 File Offset: 0x00042DE9
			public virtual string ReportTitle
			{
				set
				{
					base.PowerSharpParameters["ReportTitle"] = value;
				}
			}

			// Token: 0x17000B96 RID: 2966
			// (set) Token: 0x060022DE RID: 8926 RVA: 0x00044BFC File Offset: 0x00042DFC
			public virtual ScheduleReportType ReportType
			{
				set
				{
					base.PowerSharpParameters["ReportType"] = value;
				}
			}

			// Token: 0x17000B97 RID: 2967
			// (set) Token: 0x060022DF RID: 8927 RVA: 0x00044C14 File Offset: 0x00042E14
			public virtual MultiValuedProperty<string> SenderAddress
			{
				set
				{
					base.PowerSharpParameters["SenderAddress"] = value;
				}
			}

			// Token: 0x17000B98 RID: 2968
			// (set) Token: 0x060022E0 RID: 8928 RVA: 0x00044C27 File Offset: 0x00042E27
			public virtual ReportSeverity Severity
			{
				set
				{
					base.PowerSharpParameters["Severity"] = value;
				}
			}

			// Token: 0x17000B99 RID: 2969
			// (set) Token: 0x060022E1 RID: 8929 RVA: 0x00044C3F File Offset: 0x00042E3F
			public virtual DateTime StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17000B9A RID: 2970
			// (set) Token: 0x060022E2 RID: 8930 RVA: 0x00044C57 File Offset: 0x00042E57
			public virtual MultiValuedProperty<Guid> TransportRule
			{
				set
				{
					base.PowerSharpParameters["TransportRule"] = value;
				}
			}

			// Token: 0x17000B9B RID: 2971
			// (set) Token: 0x060022E3 RID: 8931 RVA: 0x00044C6A File Offset: 0x00042E6A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000B9C RID: 2972
			// (set) Token: 0x060022E4 RID: 8932 RVA: 0x00044C88 File Offset: 0x00042E88
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000B9D RID: 2973
			// (set) Token: 0x060022E5 RID: 8933 RVA: 0x00044CA0 File Offset: 0x00042EA0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000B9E RID: 2974
			// (set) Token: 0x060022E6 RID: 8934 RVA: 0x00044CB8 File Offset: 0x00042EB8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000B9F RID: 2975
			// (set) Token: 0x060022E7 RID: 8935 RVA: 0x00044CD0 File Offset: 0x00042ED0
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
