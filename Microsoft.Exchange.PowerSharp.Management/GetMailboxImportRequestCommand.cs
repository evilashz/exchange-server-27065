using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A1A RID: 2586
	public class GetMailboxImportRequestCommand : SyntheticCommandWithPipelineInput<MailboxImportRequest, MailboxImportRequest>
	{
		// Token: 0x0600814F RID: 33103 RVA: 0x000BFA7A File Offset: 0x000BDC7A
		private GetMailboxImportRequestCommand() : base("Get-MailboxImportRequest")
		{
		}

		// Token: 0x06008150 RID: 33104 RVA: 0x000BFA87 File Offset: 0x000BDC87
		public GetMailboxImportRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008151 RID: 33105 RVA: 0x000BFA96 File Offset: 0x000BDC96
		public virtual GetMailboxImportRequestCommand SetParameters(GetMailboxImportRequestCommand.FilteringParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008152 RID: 33106 RVA: 0x000BFAA0 File Offset: 0x000BDCA0
		public virtual GetMailboxImportRequestCommand SetParameters(GetMailboxImportRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008153 RID: 33107 RVA: 0x000BFAAA File Offset: 0x000BDCAA
		public virtual GetMailboxImportRequestCommand SetParameters(GetMailboxImportRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A1B RID: 2587
		public class FilteringParameters : ParametersBase
		{
			// Token: 0x170058D4 RID: 22740
			// (set) Token: 0x06008154 RID: 33108 RVA: 0x000BFAB4 File Offset: 0x000BDCB4
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x170058D5 RID: 22741
			// (set) Token: 0x06008155 RID: 33109 RVA: 0x000BFAD2 File Offset: 0x000BDCD2
			public virtual RequestStatus Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x170058D6 RID: 22742
			// (set) Token: 0x06008156 RID: 33110 RVA: 0x000BFAEA File Offset: 0x000BDCEA
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x170058D7 RID: 22743
			// (set) Token: 0x06008157 RID: 33111 RVA: 0x000BFAFD File Offset: 0x000BDCFD
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170058D8 RID: 22744
			// (set) Token: 0x06008158 RID: 33112 RVA: 0x000BFB10 File Offset: 0x000BDD10
			public virtual bool Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x170058D9 RID: 22745
			// (set) Token: 0x06008159 RID: 33113 RVA: 0x000BFB28 File Offset: 0x000BDD28
			public virtual bool HighPriority
			{
				set
				{
					base.PowerSharpParameters["HighPriority"] = value;
				}
			}

			// Token: 0x170058DA RID: 22746
			// (set) Token: 0x0600815A RID: 33114 RVA: 0x000BFB40 File Offset: 0x000BDD40
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x170058DB RID: 22747
			// (set) Token: 0x0600815B RID: 33115 RVA: 0x000BFB53 File Offset: 0x000BDD53
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170058DC RID: 22748
			// (set) Token: 0x0600815C RID: 33116 RVA: 0x000BFB71 File Offset: 0x000BDD71
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170058DD RID: 22749
			// (set) Token: 0x0600815D RID: 33117 RVA: 0x000BFB84 File Offset: 0x000BDD84
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170058DE RID: 22750
			// (set) Token: 0x0600815E RID: 33118 RVA: 0x000BFB97 File Offset: 0x000BDD97
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170058DF RID: 22751
			// (set) Token: 0x0600815F RID: 33119 RVA: 0x000BFBAF File Offset: 0x000BDDAF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170058E0 RID: 22752
			// (set) Token: 0x06008160 RID: 33120 RVA: 0x000BFBC7 File Offset: 0x000BDDC7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170058E1 RID: 22753
			// (set) Token: 0x06008161 RID: 33121 RVA: 0x000BFBDF File Offset: 0x000BDDDF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170058E2 RID: 22754
			// (set) Token: 0x06008162 RID: 33122 RVA: 0x000BFBF7 File Offset: 0x000BDDF7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A1C RID: 2588
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170058E3 RID: 22755
			// (set) Token: 0x06008164 RID: 33124 RVA: 0x000BFC17 File Offset: 0x000BDE17
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170058E4 RID: 22756
			// (set) Token: 0x06008165 RID: 33125 RVA: 0x000BFC35 File Offset: 0x000BDE35
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170058E5 RID: 22757
			// (set) Token: 0x06008166 RID: 33126 RVA: 0x000BFC48 File Offset: 0x000BDE48
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxImportRequestIdParameter(value) : null);
				}
			}

			// Token: 0x170058E6 RID: 22758
			// (set) Token: 0x06008167 RID: 33127 RVA: 0x000BFC66 File Offset: 0x000BDE66
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170058E7 RID: 22759
			// (set) Token: 0x06008168 RID: 33128 RVA: 0x000BFC79 File Offset: 0x000BDE79
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170058E8 RID: 22760
			// (set) Token: 0x06008169 RID: 33129 RVA: 0x000BFC91 File Offset: 0x000BDE91
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170058E9 RID: 22761
			// (set) Token: 0x0600816A RID: 33130 RVA: 0x000BFCA9 File Offset: 0x000BDEA9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170058EA RID: 22762
			// (set) Token: 0x0600816B RID: 33131 RVA: 0x000BFCC1 File Offset: 0x000BDEC1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170058EB RID: 22763
			// (set) Token: 0x0600816C RID: 33132 RVA: 0x000BFCD9 File Offset: 0x000BDED9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A1D RID: 2589
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170058EC RID: 22764
			// (set) Token: 0x0600816E RID: 33134 RVA: 0x000BFCF9 File Offset: 0x000BDEF9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170058ED RID: 22765
			// (set) Token: 0x0600816F RID: 33135 RVA: 0x000BFD0C File Offset: 0x000BDF0C
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170058EE RID: 22766
			// (set) Token: 0x06008170 RID: 33136 RVA: 0x000BFD24 File Offset: 0x000BDF24
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170058EF RID: 22767
			// (set) Token: 0x06008171 RID: 33137 RVA: 0x000BFD3C File Offset: 0x000BDF3C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170058F0 RID: 22768
			// (set) Token: 0x06008172 RID: 33138 RVA: 0x000BFD54 File Offset: 0x000BDF54
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170058F1 RID: 22769
			// (set) Token: 0x06008173 RID: 33139 RVA: 0x000BFD6C File Offset: 0x000BDF6C
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
