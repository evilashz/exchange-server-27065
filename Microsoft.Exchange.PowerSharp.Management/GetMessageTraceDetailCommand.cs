using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000167 RID: 359
	public class GetMessageTraceDetailCommand : SyntheticCommandWithPipelineInput<MessageTraceDetail, MessageTraceDetail>
	{
		// Token: 0x0600222E RID: 8750 RVA: 0x00043E71 File Offset: 0x00042071
		private GetMessageTraceDetailCommand() : base("Get-MessageTraceDetail")
		{
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x00043E7E File Offset: 0x0004207E
		public GetMessageTraceDetailCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x00043E8D File Offset: 0x0004208D
		public virtual GetMessageTraceDetailCommand SetParameters(GetMessageTraceDetailCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000168 RID: 360
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000B19 RID: 2841
			// (set) Token: 0x06002231 RID: 8753 RVA: 0x00043E97 File Offset: 0x00042097
			public virtual Guid MessageTraceId
			{
				set
				{
					base.PowerSharpParameters["MessageTraceId"] = value;
				}
			}

			// Token: 0x17000B1A RID: 2842
			// (set) Token: 0x06002232 RID: 8754 RVA: 0x00043EAF File Offset: 0x000420AF
			public virtual string MessageId
			{
				set
				{
					base.PowerSharpParameters["MessageId"] = value;
				}
			}

			// Token: 0x17000B1B RID: 2843
			// (set) Token: 0x06002233 RID: 8755 RVA: 0x00043EC2 File Offset: 0x000420C2
			public virtual string SenderAddress
			{
				set
				{
					base.PowerSharpParameters["SenderAddress"] = value;
				}
			}

			// Token: 0x17000B1C RID: 2844
			// (set) Token: 0x06002234 RID: 8756 RVA: 0x00043ED5 File Offset: 0x000420D5
			public virtual string RecipientAddress
			{
				set
				{
					base.PowerSharpParameters["RecipientAddress"] = value;
				}
			}

			// Token: 0x17000B1D RID: 2845
			// (set) Token: 0x06002235 RID: 8757 RVA: 0x00043EE8 File Offset: 0x000420E8
			public virtual MultiValuedProperty<string> Action
			{
				set
				{
					base.PowerSharpParameters["Action"] = value;
				}
			}

			// Token: 0x17000B1E RID: 2846
			// (set) Token: 0x06002236 RID: 8758 RVA: 0x00043EFB File Offset: 0x000420FB
			public virtual MultiValuedProperty<string> Event
			{
				set
				{
					base.PowerSharpParameters["Event"] = value;
				}
			}

			// Token: 0x17000B1F RID: 2847
			// (set) Token: 0x06002237 RID: 8759 RVA: 0x00043F0E File Offset: 0x0004210E
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17000B20 RID: 2848
			// (set) Token: 0x06002238 RID: 8760 RVA: 0x00043F26 File Offset: 0x00042126
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17000B21 RID: 2849
			// (set) Token: 0x06002239 RID: 8761 RVA: 0x00043F3E File Offset: 0x0004213E
			public virtual int Page
			{
				set
				{
					base.PowerSharpParameters["Page"] = value;
				}
			}

			// Token: 0x17000B22 RID: 2850
			// (set) Token: 0x0600223A RID: 8762 RVA: 0x00043F56 File Offset: 0x00042156
			public virtual int PageSize
			{
				set
				{
					base.PowerSharpParameters["PageSize"] = value;
				}
			}

			// Token: 0x17000B23 RID: 2851
			// (set) Token: 0x0600223B RID: 8763 RVA: 0x00043F6E File Offset: 0x0004216E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000B24 RID: 2852
			// (set) Token: 0x0600223C RID: 8764 RVA: 0x00043F8C File Offset: 0x0004218C
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17000B25 RID: 2853
			// (set) Token: 0x0600223D RID: 8765 RVA: 0x00043F9F File Offset: 0x0004219F
			public virtual string ProbeTag
			{
				set
				{
					base.PowerSharpParameters["ProbeTag"] = value;
				}
			}

			// Token: 0x17000B26 RID: 2854
			// (set) Token: 0x0600223E RID: 8766 RVA: 0x00043FB2 File Offset: 0x000421B2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000B27 RID: 2855
			// (set) Token: 0x0600223F RID: 8767 RVA: 0x00043FCA File Offset: 0x000421CA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000B28 RID: 2856
			// (set) Token: 0x06002240 RID: 8768 RVA: 0x00043FE2 File Offset: 0x000421E2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000B29 RID: 2857
			// (set) Token: 0x06002241 RID: 8769 RVA: 0x00043FFA File Offset: 0x000421FA
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
