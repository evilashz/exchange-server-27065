using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A49 RID: 2633
	public class GetMailboxRestoreRequestCommand : SyntheticCommandWithPipelineInput<MailboxRestoreRequest, MailboxRestoreRequest>
	{
		// Token: 0x06008313 RID: 33555 RVA: 0x000C1ED4 File Offset: 0x000C00D4
		private GetMailboxRestoreRequestCommand() : base("Get-MailboxRestoreRequest")
		{
		}

		// Token: 0x06008314 RID: 33556 RVA: 0x000C1EE1 File Offset: 0x000C00E1
		public GetMailboxRestoreRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008315 RID: 33557 RVA: 0x000C1EF0 File Offset: 0x000C00F0
		public virtual GetMailboxRestoreRequestCommand SetParameters(GetMailboxRestoreRequestCommand.FilteringParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008316 RID: 33558 RVA: 0x000C1EFA File Offset: 0x000C00FA
		public virtual GetMailboxRestoreRequestCommand SetParameters(GetMailboxRestoreRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008317 RID: 33559 RVA: 0x000C1F04 File Offset: 0x000C0104
		public virtual GetMailboxRestoreRequestCommand SetParameters(GetMailboxRestoreRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A4A RID: 2634
		public class FilteringParameters : ParametersBase
		{
			// Token: 0x17005A3A RID: 23098
			// (set) Token: 0x06008318 RID: 33560 RVA: 0x000C1F0E File Offset: 0x000C010E
			public virtual DatabaseIdParameter SourceDatabase
			{
				set
				{
					base.PowerSharpParameters["SourceDatabase"] = value;
				}
			}

			// Token: 0x17005A3B RID: 23099
			// (set) Token: 0x06008319 RID: 33561 RVA: 0x000C1F21 File Offset: 0x000C0121
			public virtual string TargetMailbox
			{
				set
				{
					base.PowerSharpParameters["TargetMailbox"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005A3C RID: 23100
			// (set) Token: 0x0600831A RID: 33562 RVA: 0x000C1F3F File Offset: 0x000C013F
			public virtual RequestStatus Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17005A3D RID: 23101
			// (set) Token: 0x0600831B RID: 33563 RVA: 0x000C1F57 File Offset: 0x000C0157
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005A3E RID: 23102
			// (set) Token: 0x0600831C RID: 33564 RVA: 0x000C1F6A File Offset: 0x000C016A
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005A3F RID: 23103
			// (set) Token: 0x0600831D RID: 33565 RVA: 0x000C1F7D File Offset: 0x000C017D
			public virtual bool Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005A40 RID: 23104
			// (set) Token: 0x0600831E RID: 33566 RVA: 0x000C1F95 File Offset: 0x000C0195
			public virtual bool HighPriority
			{
				set
				{
					base.PowerSharpParameters["HighPriority"] = value;
				}
			}

			// Token: 0x17005A41 RID: 23105
			// (set) Token: 0x0600831F RID: 33567 RVA: 0x000C1FAD File Offset: 0x000C01AD
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005A42 RID: 23106
			// (set) Token: 0x06008320 RID: 33568 RVA: 0x000C1FC0 File Offset: 0x000C01C0
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005A43 RID: 23107
			// (set) Token: 0x06008321 RID: 33569 RVA: 0x000C1FDE File Offset: 0x000C01DE
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17005A44 RID: 23108
			// (set) Token: 0x06008322 RID: 33570 RVA: 0x000C1FF1 File Offset: 0x000C01F1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005A45 RID: 23109
			// (set) Token: 0x06008323 RID: 33571 RVA: 0x000C2004 File Offset: 0x000C0204
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17005A46 RID: 23110
			// (set) Token: 0x06008324 RID: 33572 RVA: 0x000C201C File Offset: 0x000C021C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005A47 RID: 23111
			// (set) Token: 0x06008325 RID: 33573 RVA: 0x000C2034 File Offset: 0x000C0234
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005A48 RID: 23112
			// (set) Token: 0x06008326 RID: 33574 RVA: 0x000C204C File Offset: 0x000C024C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005A49 RID: 23113
			// (set) Token: 0x06008327 RID: 33575 RVA: 0x000C2064 File Offset: 0x000C0264
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A4B RID: 2635
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005A4A RID: 23114
			// (set) Token: 0x06008329 RID: 33577 RVA: 0x000C2084 File Offset: 0x000C0284
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005A4B RID: 23115
			// (set) Token: 0x0600832A RID: 33578 RVA: 0x000C20A2 File Offset: 0x000C02A2
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17005A4C RID: 23116
			// (set) Token: 0x0600832B RID: 33579 RVA: 0x000C20B5 File Offset: 0x000C02B5
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxRestoreRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005A4D RID: 23117
			// (set) Token: 0x0600832C RID: 33580 RVA: 0x000C20D3 File Offset: 0x000C02D3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005A4E RID: 23118
			// (set) Token: 0x0600832D RID: 33581 RVA: 0x000C20E6 File Offset: 0x000C02E6
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17005A4F RID: 23119
			// (set) Token: 0x0600832E RID: 33582 RVA: 0x000C20FE File Offset: 0x000C02FE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005A50 RID: 23120
			// (set) Token: 0x0600832F RID: 33583 RVA: 0x000C2116 File Offset: 0x000C0316
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005A51 RID: 23121
			// (set) Token: 0x06008330 RID: 33584 RVA: 0x000C212E File Offset: 0x000C032E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005A52 RID: 23122
			// (set) Token: 0x06008331 RID: 33585 RVA: 0x000C2146 File Offset: 0x000C0346
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A4C RID: 2636
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005A53 RID: 23123
			// (set) Token: 0x06008333 RID: 33587 RVA: 0x000C2166 File Offset: 0x000C0366
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005A54 RID: 23124
			// (set) Token: 0x06008334 RID: 33588 RVA: 0x000C2179 File Offset: 0x000C0379
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17005A55 RID: 23125
			// (set) Token: 0x06008335 RID: 33589 RVA: 0x000C2191 File Offset: 0x000C0391
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005A56 RID: 23126
			// (set) Token: 0x06008336 RID: 33590 RVA: 0x000C21A9 File Offset: 0x000C03A9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005A57 RID: 23127
			// (set) Token: 0x06008337 RID: 33591 RVA: 0x000C21C1 File Offset: 0x000C03C1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005A58 RID: 23128
			// (set) Token: 0x06008338 RID: 33592 RVA: 0x000C21D9 File Offset: 0x000C03D9
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
