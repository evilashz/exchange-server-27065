using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A13 RID: 2579
	public class SetMailboxExportRequestCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxExportRequestIdParameter>
	{
		// Token: 0x06008110 RID: 33040 RVA: 0x000BF56A File Offset: 0x000BD76A
		private SetMailboxExportRequestCommand() : base("Set-MailboxExportRequest")
		{
		}

		// Token: 0x06008111 RID: 33041 RVA: 0x000BF577 File Offset: 0x000BD777
		public SetMailboxExportRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008112 RID: 33042 RVA: 0x000BF586 File Offset: 0x000BD786
		public virtual SetMailboxExportRequestCommand SetParameters(SetMailboxExportRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008113 RID: 33043 RVA: 0x000BF590 File Offset: 0x000BD790
		public virtual SetMailboxExportRequestCommand SetParameters(SetMailboxExportRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008114 RID: 33044 RVA: 0x000BF59A File Offset: 0x000BD79A
		public virtual SetMailboxExportRequestCommand SetParameters(SetMailboxExportRequestCommand.RehomeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A14 RID: 2580
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170058A3 RID: 22691
			// (set) Token: 0x06008115 RID: 33045 RVA: 0x000BF5A4 File Offset: 0x000BD7A4
			public virtual PSCredential RemoteCredential
			{
				set
				{
					base.PowerSharpParameters["RemoteCredential"] = value;
				}
			}

			// Token: 0x170058A4 RID: 22692
			// (set) Token: 0x06008116 RID: 33046 RVA: 0x000BF5B7 File Offset: 0x000BD7B7
			public virtual Fqdn RemoteHostName
			{
				set
				{
					base.PowerSharpParameters["RemoteHostName"] = value;
				}
			}

			// Token: 0x170058A5 RID: 22693
			// (set) Token: 0x06008117 RID: 33047 RVA: 0x000BF5CA File Offset: 0x000BD7CA
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x170058A6 RID: 22694
			// (set) Token: 0x06008118 RID: 33048 RVA: 0x000BF5E2 File Offset: 0x000BD7E2
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x170058A7 RID: 22695
			// (set) Token: 0x06008119 RID: 33049 RVA: 0x000BF5FA File Offset: 0x000BD7FA
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x170058A8 RID: 22696
			// (set) Token: 0x0600811A RID: 33050 RVA: 0x000BF612 File Offset: 0x000BD812
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x170058A9 RID: 22697
			// (set) Token: 0x0600811B RID: 33051 RVA: 0x000BF625 File Offset: 0x000BD825
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x170058AA RID: 22698
			// (set) Token: 0x0600811C RID: 33052 RVA: 0x000BF63D File Offset: 0x000BD83D
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x170058AB RID: 22699
			// (set) Token: 0x0600811D RID: 33053 RVA: 0x000BF655 File Offset: 0x000BD855
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x170058AC RID: 22700
			// (set) Token: 0x0600811E RID: 33054 RVA: 0x000BF66D File Offset: 0x000BD86D
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x170058AD RID: 22701
			// (set) Token: 0x0600811F RID: 33055 RVA: 0x000BF685 File Offset: 0x000BD885
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxExportRequestIdParameter(value) : null);
				}
			}

			// Token: 0x170058AE RID: 22702
			// (set) Token: 0x06008120 RID: 33056 RVA: 0x000BF6A3 File Offset: 0x000BD8A3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170058AF RID: 22703
			// (set) Token: 0x06008121 RID: 33057 RVA: 0x000BF6B6 File Offset: 0x000BD8B6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170058B0 RID: 22704
			// (set) Token: 0x06008122 RID: 33058 RVA: 0x000BF6CE File Offset: 0x000BD8CE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170058B1 RID: 22705
			// (set) Token: 0x06008123 RID: 33059 RVA: 0x000BF6E6 File Offset: 0x000BD8E6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170058B2 RID: 22706
			// (set) Token: 0x06008124 RID: 33060 RVA: 0x000BF6FE File Offset: 0x000BD8FE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170058B3 RID: 22707
			// (set) Token: 0x06008125 RID: 33061 RVA: 0x000BF716 File Offset: 0x000BD916
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A15 RID: 2581
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170058B4 RID: 22708
			// (set) Token: 0x06008127 RID: 33063 RVA: 0x000BF736 File Offset: 0x000BD936
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxExportRequestIdParameter(value) : null);
				}
			}

			// Token: 0x170058B5 RID: 22709
			// (set) Token: 0x06008128 RID: 33064 RVA: 0x000BF754 File Offset: 0x000BD954
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170058B6 RID: 22710
			// (set) Token: 0x06008129 RID: 33065 RVA: 0x000BF767 File Offset: 0x000BD967
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170058B7 RID: 22711
			// (set) Token: 0x0600812A RID: 33066 RVA: 0x000BF77F File Offset: 0x000BD97F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170058B8 RID: 22712
			// (set) Token: 0x0600812B RID: 33067 RVA: 0x000BF797 File Offset: 0x000BD997
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170058B9 RID: 22713
			// (set) Token: 0x0600812C RID: 33068 RVA: 0x000BF7AF File Offset: 0x000BD9AF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170058BA RID: 22714
			// (set) Token: 0x0600812D RID: 33069 RVA: 0x000BF7C7 File Offset: 0x000BD9C7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A16 RID: 2582
		public class RehomeParameters : ParametersBase
		{
			// Token: 0x170058BB RID: 22715
			// (set) Token: 0x0600812F RID: 33071 RVA: 0x000BF7E7 File Offset: 0x000BD9E7
			public virtual SwitchParameter RehomeRequest
			{
				set
				{
					base.PowerSharpParameters["RehomeRequest"] = value;
				}
			}

			// Token: 0x170058BC RID: 22716
			// (set) Token: 0x06008130 RID: 33072 RVA: 0x000BF7FF File Offset: 0x000BD9FF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxExportRequestIdParameter(value) : null);
				}
			}

			// Token: 0x170058BD RID: 22717
			// (set) Token: 0x06008131 RID: 33073 RVA: 0x000BF81D File Offset: 0x000BDA1D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170058BE RID: 22718
			// (set) Token: 0x06008132 RID: 33074 RVA: 0x000BF830 File Offset: 0x000BDA30
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170058BF RID: 22719
			// (set) Token: 0x06008133 RID: 33075 RVA: 0x000BF848 File Offset: 0x000BDA48
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170058C0 RID: 22720
			// (set) Token: 0x06008134 RID: 33076 RVA: 0x000BF860 File Offset: 0x000BDA60
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170058C1 RID: 22721
			// (set) Token: 0x06008135 RID: 33077 RVA: 0x000BF878 File Offset: 0x000BDA78
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170058C2 RID: 22722
			// (set) Token: 0x06008136 RID: 33078 RVA: 0x000BF890 File Offset: 0x000BDA90
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
