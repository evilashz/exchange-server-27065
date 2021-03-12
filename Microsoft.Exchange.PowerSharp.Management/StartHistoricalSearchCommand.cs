using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000175 RID: 373
	public class StartHistoricalSearchCommand : SyntheticCommandWithPipelineInput<HistoricalSearch, HistoricalSearch>
	{
		// Token: 0x0600228F RID: 8847 RVA: 0x000445F6 File Offset: 0x000427F6
		private StartHistoricalSearchCommand() : base("Start-HistoricalSearch")
		{
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x00044603 File Offset: 0x00042803
		public StartHistoricalSearchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x00044612 File Offset: 0x00042812
		public virtual StartHistoricalSearchCommand SetParameters(StartHistoricalSearchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000176 RID: 374
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000B5E RID: 2910
			// (set) Token: 0x06002292 RID: 8850 RVA: 0x0004461C File Offset: 0x0004281C
			public virtual HistoricalSearchReportType ReportType
			{
				set
				{
					base.PowerSharpParameters["ReportType"] = value;
				}
			}

			// Token: 0x17000B5F RID: 2911
			// (set) Token: 0x06002293 RID: 8851 RVA: 0x00044634 File Offset: 0x00042834
			public virtual string ReportTitle
			{
				set
				{
					base.PowerSharpParameters["ReportTitle"] = value;
				}
			}

			// Token: 0x17000B60 RID: 2912
			// (set) Token: 0x06002294 RID: 8852 RVA: 0x00044647 File Offset: 0x00042847
			public virtual MultiValuedProperty<string> NotifyAddress
			{
				set
				{
					base.PowerSharpParameters["NotifyAddress"] = value;
				}
			}

			// Token: 0x17000B61 RID: 2913
			// (set) Token: 0x06002295 RID: 8853 RVA: 0x0004465A File Offset: 0x0004285A
			public virtual DateTime StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17000B62 RID: 2914
			// (set) Token: 0x06002296 RID: 8854 RVA: 0x00044672 File Offset: 0x00042872
			public virtual DateTime EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17000B63 RID: 2915
			// (set) Token: 0x06002297 RID: 8855 RVA: 0x0004468A File Offset: 0x0004288A
			public virtual string DeliveryStatus
			{
				set
				{
					base.PowerSharpParameters["DeliveryStatus"] = value;
				}
			}

			// Token: 0x17000B64 RID: 2916
			// (set) Token: 0x06002298 RID: 8856 RVA: 0x0004469D File Offset: 0x0004289D
			public virtual MultiValuedProperty<string> SenderAddress
			{
				set
				{
					base.PowerSharpParameters["SenderAddress"] = value;
				}
			}

			// Token: 0x17000B65 RID: 2917
			// (set) Token: 0x06002299 RID: 8857 RVA: 0x000446B0 File Offset: 0x000428B0
			public virtual MultiValuedProperty<string> RecipientAddress
			{
				set
				{
					base.PowerSharpParameters["RecipientAddress"] = value;
				}
			}

			// Token: 0x17000B66 RID: 2918
			// (set) Token: 0x0600229A RID: 8858 RVA: 0x000446C3 File Offset: 0x000428C3
			public virtual string OriginalClientIP
			{
				set
				{
					base.PowerSharpParameters["OriginalClientIP"] = value;
				}
			}

			// Token: 0x17000B67 RID: 2919
			// (set) Token: 0x0600229B RID: 8859 RVA: 0x000446D6 File Offset: 0x000428D6
			public virtual MultiValuedProperty<string> MessageID
			{
				set
				{
					base.PowerSharpParameters["MessageID"] = value;
				}
			}

			// Token: 0x17000B68 RID: 2920
			// (set) Token: 0x0600229C RID: 8860 RVA: 0x000446E9 File Offset: 0x000428E9
			public virtual MultiValuedProperty<Guid> DLPPolicy
			{
				set
				{
					base.PowerSharpParameters["DLPPolicy"] = value;
				}
			}

			// Token: 0x17000B69 RID: 2921
			// (set) Token: 0x0600229D RID: 8861 RVA: 0x000446FC File Offset: 0x000428FC
			public virtual MultiValuedProperty<Guid> TransportRule
			{
				set
				{
					base.PowerSharpParameters["TransportRule"] = value;
				}
			}

			// Token: 0x17000B6A RID: 2922
			// (set) Token: 0x0600229E RID: 8862 RVA: 0x0004470F File Offset: 0x0004290F
			public virtual CultureInfo Locale
			{
				set
				{
					base.PowerSharpParameters["Locale"] = value;
				}
			}

			// Token: 0x17000B6B RID: 2923
			// (set) Token: 0x0600229F RID: 8863 RVA: 0x00044722 File Offset: 0x00042922
			public virtual MessageDirection Direction
			{
				set
				{
					base.PowerSharpParameters["Direction"] = value;
				}
			}

			// Token: 0x17000B6C RID: 2924
			// (set) Token: 0x060022A0 RID: 8864 RVA: 0x0004473A File Offset: 0x0004293A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000B6D RID: 2925
			// (set) Token: 0x060022A1 RID: 8865 RVA: 0x00044758 File Offset: 0x00042958
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000B6E RID: 2926
			// (set) Token: 0x060022A2 RID: 8866 RVA: 0x00044770 File Offset: 0x00042970
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000B6F RID: 2927
			// (set) Token: 0x060022A3 RID: 8867 RVA: 0x00044788 File Offset: 0x00042988
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000B70 RID: 2928
			// (set) Token: 0x060022A4 RID: 8868 RVA: 0x000447A0 File Offset: 0x000429A0
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
