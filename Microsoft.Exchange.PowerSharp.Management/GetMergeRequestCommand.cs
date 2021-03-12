using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009E7 RID: 2535
	public class GetMergeRequestCommand : SyntheticCommandWithPipelineInput<MergeRequest, MergeRequest>
	{
		// Token: 0x06007F3C RID: 32572 RVA: 0x000BCF7B File Offset: 0x000BB17B
		private GetMergeRequestCommand() : base("Get-MergeRequest")
		{
		}

		// Token: 0x06007F3D RID: 32573 RVA: 0x000BCF88 File Offset: 0x000BB188
		public GetMergeRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007F3E RID: 32574 RVA: 0x000BCF97 File Offset: 0x000BB197
		public virtual GetMergeRequestCommand SetParameters(GetMergeRequestCommand.FilteringParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007F3F RID: 32575 RVA: 0x000BCFA1 File Offset: 0x000BB1A1
		public virtual GetMergeRequestCommand SetParameters(GetMergeRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007F40 RID: 32576 RVA: 0x000BCFAB File Offset: 0x000BB1AB
		public virtual GetMergeRequestCommand SetParameters(GetMergeRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009E8 RID: 2536
		public class FilteringParameters : ParametersBase
		{
			// Token: 0x17005727 RID: 22311
			// (set) Token: 0x06007F41 RID: 32577 RVA: 0x000BCFB5 File Offset: 0x000BB1B5
			public virtual string TargetMailbox
			{
				set
				{
					base.PowerSharpParameters["TargetMailbox"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005728 RID: 22312
			// (set) Token: 0x06007F42 RID: 32578 RVA: 0x000BCFD3 File Offset: 0x000BB1D3
			public virtual string SourceMailbox
			{
				set
				{
					base.PowerSharpParameters["SourceMailbox"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005729 RID: 22313
			// (set) Token: 0x06007F43 RID: 32579 RVA: 0x000BCFF1 File Offset: 0x000BB1F1
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700572A RID: 22314
			// (set) Token: 0x06007F44 RID: 32580 RVA: 0x000BD00F File Offset: 0x000BB20F
			public virtual RequestStatus Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x1700572B RID: 22315
			// (set) Token: 0x06007F45 RID: 32581 RVA: 0x000BD027 File Offset: 0x000BB227
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x1700572C RID: 22316
			// (set) Token: 0x06007F46 RID: 32582 RVA: 0x000BD03A File Offset: 0x000BB23A
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700572D RID: 22317
			// (set) Token: 0x06007F47 RID: 32583 RVA: 0x000BD04D File Offset: 0x000BB24D
			public virtual bool Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x1700572E RID: 22318
			// (set) Token: 0x06007F48 RID: 32584 RVA: 0x000BD065 File Offset: 0x000BB265
			public virtual bool HighPriority
			{
				set
				{
					base.PowerSharpParameters["HighPriority"] = value;
				}
			}

			// Token: 0x1700572F RID: 22319
			// (set) Token: 0x06007F49 RID: 32585 RVA: 0x000BD07D File Offset: 0x000BB27D
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005730 RID: 22320
			// (set) Token: 0x06007F4A RID: 32586 RVA: 0x000BD090 File Offset: 0x000BB290
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005731 RID: 22321
			// (set) Token: 0x06007F4B RID: 32587 RVA: 0x000BD0AE File Offset: 0x000BB2AE
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17005732 RID: 22322
			// (set) Token: 0x06007F4C RID: 32588 RVA: 0x000BD0C1 File Offset: 0x000BB2C1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005733 RID: 22323
			// (set) Token: 0x06007F4D RID: 32589 RVA: 0x000BD0D4 File Offset: 0x000BB2D4
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17005734 RID: 22324
			// (set) Token: 0x06007F4E RID: 32590 RVA: 0x000BD0EC File Offset: 0x000BB2EC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005735 RID: 22325
			// (set) Token: 0x06007F4F RID: 32591 RVA: 0x000BD104 File Offset: 0x000BB304
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005736 RID: 22326
			// (set) Token: 0x06007F50 RID: 32592 RVA: 0x000BD11C File Offset: 0x000BB31C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005737 RID: 22327
			// (set) Token: 0x06007F51 RID: 32593 RVA: 0x000BD134 File Offset: 0x000BB334
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020009E9 RID: 2537
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005738 RID: 22328
			// (set) Token: 0x06007F53 RID: 32595 RVA: 0x000BD154 File Offset: 0x000BB354
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005739 RID: 22329
			// (set) Token: 0x06007F54 RID: 32596 RVA: 0x000BD172 File Offset: 0x000BB372
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700573A RID: 22330
			// (set) Token: 0x06007F55 RID: 32597 RVA: 0x000BD185 File Offset: 0x000BB385
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MergeRequestIdParameter(value) : null);
				}
			}

			// Token: 0x1700573B RID: 22331
			// (set) Token: 0x06007F56 RID: 32598 RVA: 0x000BD1A3 File Offset: 0x000BB3A3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700573C RID: 22332
			// (set) Token: 0x06007F57 RID: 32599 RVA: 0x000BD1B6 File Offset: 0x000BB3B6
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700573D RID: 22333
			// (set) Token: 0x06007F58 RID: 32600 RVA: 0x000BD1CE File Offset: 0x000BB3CE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700573E RID: 22334
			// (set) Token: 0x06007F59 RID: 32601 RVA: 0x000BD1E6 File Offset: 0x000BB3E6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700573F RID: 22335
			// (set) Token: 0x06007F5A RID: 32602 RVA: 0x000BD1FE File Offset: 0x000BB3FE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005740 RID: 22336
			// (set) Token: 0x06007F5B RID: 32603 RVA: 0x000BD216 File Offset: 0x000BB416
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020009EA RID: 2538
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005741 RID: 22337
			// (set) Token: 0x06007F5D RID: 32605 RVA: 0x000BD236 File Offset: 0x000BB436
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005742 RID: 22338
			// (set) Token: 0x06007F5E RID: 32606 RVA: 0x000BD249 File Offset: 0x000BB449
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17005743 RID: 22339
			// (set) Token: 0x06007F5F RID: 32607 RVA: 0x000BD261 File Offset: 0x000BB461
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005744 RID: 22340
			// (set) Token: 0x06007F60 RID: 32608 RVA: 0x000BD279 File Offset: 0x000BB479
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005745 RID: 22341
			// (set) Token: 0x06007F61 RID: 32609 RVA: 0x000BD291 File Offset: 0x000BB491
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005746 RID: 22342
			// (set) Token: 0x06007F62 RID: 32610 RVA: 0x000BD2A9 File Offset: 0x000BB4A9
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
