using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000165 RID: 357
	public class GetMessageTraceCommand : SyntheticCommandWithPipelineInput<MessageTrace, MessageTrace>
	{
		// Token: 0x06002218 RID: 8728 RVA: 0x00043CB5 File Offset: 0x00041EB5
		private GetMessageTraceCommand() : base("Get-MessageTrace")
		{
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x00043CC2 File Offset: 0x00041EC2
		public GetMessageTraceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x00043CD1 File Offset: 0x00041ED1
		public virtual GetMessageTraceCommand SetParameters(GetMessageTraceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000166 RID: 358
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000B07 RID: 2823
			// (set) Token: 0x0600221B RID: 8731 RVA: 0x00043CDB File Offset: 0x00041EDB
			public virtual MultiValuedProperty<string> MessageId
			{
				set
				{
					base.PowerSharpParameters["MessageId"] = value;
				}
			}

			// Token: 0x17000B08 RID: 2824
			// (set) Token: 0x0600221C RID: 8732 RVA: 0x00043CEE File Offset: 0x00041EEE
			public virtual MultiValuedProperty<string> SenderAddress
			{
				set
				{
					base.PowerSharpParameters["SenderAddress"] = value;
				}
			}

			// Token: 0x17000B09 RID: 2825
			// (set) Token: 0x0600221D RID: 8733 RVA: 0x00043D01 File Offset: 0x00041F01
			public virtual MultiValuedProperty<string> RecipientAddress
			{
				set
				{
					base.PowerSharpParameters["RecipientAddress"] = value;
				}
			}

			// Token: 0x17000B0A RID: 2826
			// (set) Token: 0x0600221E RID: 8734 RVA: 0x00043D14 File Offset: 0x00041F14
			public virtual MultiValuedProperty<string> Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17000B0B RID: 2827
			// (set) Token: 0x0600221F RID: 8735 RVA: 0x00043D27 File Offset: 0x00041F27
			public virtual Guid? MessageTraceId
			{
				set
				{
					base.PowerSharpParameters["MessageTraceId"] = value;
				}
			}

			// Token: 0x17000B0C RID: 2828
			// (set) Token: 0x06002220 RID: 8736 RVA: 0x00043D3F File Offset: 0x00041F3F
			public virtual string ToIP
			{
				set
				{
					base.PowerSharpParameters["ToIP"] = value;
				}
			}

			// Token: 0x17000B0D RID: 2829
			// (set) Token: 0x06002221 RID: 8737 RVA: 0x00043D52 File Offset: 0x00041F52
			public virtual string FromIP
			{
				set
				{
					base.PowerSharpParameters["FromIP"] = value;
				}
			}

			// Token: 0x17000B0E RID: 2830
			// (set) Token: 0x06002222 RID: 8738 RVA: 0x00043D65 File Offset: 0x00041F65
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17000B0F RID: 2831
			// (set) Token: 0x06002223 RID: 8739 RVA: 0x00043D7D File Offset: 0x00041F7D
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17000B10 RID: 2832
			// (set) Token: 0x06002224 RID: 8740 RVA: 0x00043D95 File Offset: 0x00041F95
			public virtual int Page
			{
				set
				{
					base.PowerSharpParameters["Page"] = value;
				}
			}

			// Token: 0x17000B11 RID: 2833
			// (set) Token: 0x06002225 RID: 8741 RVA: 0x00043DAD File Offset: 0x00041FAD
			public virtual int PageSize
			{
				set
				{
					base.PowerSharpParameters["PageSize"] = value;
				}
			}

			// Token: 0x17000B12 RID: 2834
			// (set) Token: 0x06002226 RID: 8742 RVA: 0x00043DC5 File Offset: 0x00041FC5
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000B13 RID: 2835
			// (set) Token: 0x06002227 RID: 8743 RVA: 0x00043DE3 File Offset: 0x00041FE3
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17000B14 RID: 2836
			// (set) Token: 0x06002228 RID: 8744 RVA: 0x00043DF6 File Offset: 0x00041FF6
			public virtual string ProbeTag
			{
				set
				{
					base.PowerSharpParameters["ProbeTag"] = value;
				}
			}

			// Token: 0x17000B15 RID: 2837
			// (set) Token: 0x06002229 RID: 8745 RVA: 0x00043E09 File Offset: 0x00042009
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000B16 RID: 2838
			// (set) Token: 0x0600222A RID: 8746 RVA: 0x00043E21 File Offset: 0x00042021
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000B17 RID: 2839
			// (set) Token: 0x0600222B RID: 8747 RVA: 0x00043E39 File Offset: 0x00042039
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000B18 RID: 2840
			// (set) Token: 0x0600222C RID: 8748 RVA: 0x00043E51 File Offset: 0x00042051
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
