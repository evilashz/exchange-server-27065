using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E08 RID: 3592
	public class GetUMMailboxCommand : SyntheticCommandWithPipelineInput<MailboxIdParameter, MailboxIdParameter>
	{
		// Token: 0x0600D5B6 RID: 54710 RVA: 0x0012FB98 File Offset: 0x0012DD98
		private GetUMMailboxCommand() : base("Get-UMMailbox")
		{
		}

		// Token: 0x0600D5B7 RID: 54711 RVA: 0x0012FBA5 File Offset: 0x0012DDA5
		public GetUMMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D5B8 RID: 54712 RVA: 0x0012FBB4 File Offset: 0x0012DDB4
		public virtual GetUMMailboxCommand SetParameters(GetUMMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D5B9 RID: 54713 RVA: 0x0012FBBE File Offset: 0x0012DDBE
		public virtual GetUMMailboxCommand SetParameters(GetUMMailboxCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D5BA RID: 54714 RVA: 0x0012FBC8 File Offset: 0x0012DDC8
		public virtual GetUMMailboxCommand SetParameters(GetUMMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E09 RID: 3593
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A55F RID: 42335
			// (set) Token: 0x0600D5BB RID: 54715 RVA: 0x0012FBD2 File Offset: 0x0012DDD2
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700A560 RID: 42336
			// (set) Token: 0x0600D5BC RID: 54716 RVA: 0x0012FBE5 File Offset: 0x0012DDE5
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A561 RID: 42337
			// (set) Token: 0x0600D5BD RID: 54717 RVA: 0x0012FC03 File Offset: 0x0012DE03
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700A562 RID: 42338
			// (set) Token: 0x0600D5BE RID: 54718 RVA: 0x0012FC16 File Offset: 0x0012DE16
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700A563 RID: 42339
			// (set) Token: 0x0600D5BF RID: 54719 RVA: 0x0012FC29 File Offset: 0x0012DE29
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A564 RID: 42340
			// (set) Token: 0x0600D5C0 RID: 54720 RVA: 0x0012FC47 File Offset: 0x0012DE47
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A565 RID: 42341
			// (set) Token: 0x0600D5C1 RID: 54721 RVA: 0x0012FC5F File Offset: 0x0012DE5F
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700A566 RID: 42342
			// (set) Token: 0x0600D5C2 RID: 54722 RVA: 0x0012FC72 File Offset: 0x0012DE72
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A567 RID: 42343
			// (set) Token: 0x0600D5C3 RID: 54723 RVA: 0x0012FC8A File Offset: 0x0012DE8A
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700A568 RID: 42344
			// (set) Token: 0x0600D5C4 RID: 54724 RVA: 0x0012FCA2 File Offset: 0x0012DEA2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A569 RID: 42345
			// (set) Token: 0x0600D5C5 RID: 54725 RVA: 0x0012FCB5 File Offset: 0x0012DEB5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A56A RID: 42346
			// (set) Token: 0x0600D5C6 RID: 54726 RVA: 0x0012FCCD File Offset: 0x0012DECD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A56B RID: 42347
			// (set) Token: 0x0600D5C7 RID: 54727 RVA: 0x0012FCE5 File Offset: 0x0012DEE5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A56C RID: 42348
			// (set) Token: 0x0600D5C8 RID: 54728 RVA: 0x0012FCFD File Offset: 0x0012DEFD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000E0A RID: 3594
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x1700A56D RID: 42349
			// (set) Token: 0x0600D5CA RID: 54730 RVA: 0x0012FD1D File Offset: 0x0012DF1D
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x1700A56E RID: 42350
			// (set) Token: 0x0600D5CB RID: 54731 RVA: 0x0012FD30 File Offset: 0x0012DF30
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700A56F RID: 42351
			// (set) Token: 0x0600D5CC RID: 54732 RVA: 0x0012FD43 File Offset: 0x0012DF43
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A570 RID: 42352
			// (set) Token: 0x0600D5CD RID: 54733 RVA: 0x0012FD61 File Offset: 0x0012DF61
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700A571 RID: 42353
			// (set) Token: 0x0600D5CE RID: 54734 RVA: 0x0012FD74 File Offset: 0x0012DF74
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700A572 RID: 42354
			// (set) Token: 0x0600D5CF RID: 54735 RVA: 0x0012FD87 File Offset: 0x0012DF87
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A573 RID: 42355
			// (set) Token: 0x0600D5D0 RID: 54736 RVA: 0x0012FDA5 File Offset: 0x0012DFA5
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A574 RID: 42356
			// (set) Token: 0x0600D5D1 RID: 54737 RVA: 0x0012FDBD File Offset: 0x0012DFBD
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700A575 RID: 42357
			// (set) Token: 0x0600D5D2 RID: 54738 RVA: 0x0012FDD0 File Offset: 0x0012DFD0
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A576 RID: 42358
			// (set) Token: 0x0600D5D3 RID: 54739 RVA: 0x0012FDE8 File Offset: 0x0012DFE8
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700A577 RID: 42359
			// (set) Token: 0x0600D5D4 RID: 54740 RVA: 0x0012FE00 File Offset: 0x0012E000
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A578 RID: 42360
			// (set) Token: 0x0600D5D5 RID: 54741 RVA: 0x0012FE13 File Offset: 0x0012E013
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A579 RID: 42361
			// (set) Token: 0x0600D5D6 RID: 54742 RVA: 0x0012FE2B File Offset: 0x0012E02B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A57A RID: 42362
			// (set) Token: 0x0600D5D7 RID: 54743 RVA: 0x0012FE43 File Offset: 0x0012E043
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A57B RID: 42363
			// (set) Token: 0x0600D5D8 RID: 54744 RVA: 0x0012FE5B File Offset: 0x0012E05B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000E0B RID: 3595
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A57C RID: 42364
			// (set) Token: 0x0600D5DA RID: 54746 RVA: 0x0012FE7B File Offset: 0x0012E07B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A57D RID: 42365
			// (set) Token: 0x0600D5DB RID: 54747 RVA: 0x0012FE99 File Offset: 0x0012E099
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700A57E RID: 42366
			// (set) Token: 0x0600D5DC RID: 54748 RVA: 0x0012FEAC File Offset: 0x0012E0AC
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A57F RID: 42367
			// (set) Token: 0x0600D5DD RID: 54749 RVA: 0x0012FECA File Offset: 0x0012E0CA
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700A580 RID: 42368
			// (set) Token: 0x0600D5DE RID: 54750 RVA: 0x0012FEDD File Offset: 0x0012E0DD
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700A581 RID: 42369
			// (set) Token: 0x0600D5DF RID: 54751 RVA: 0x0012FEF0 File Offset: 0x0012E0F0
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A582 RID: 42370
			// (set) Token: 0x0600D5E0 RID: 54752 RVA: 0x0012FF0E File Offset: 0x0012E10E
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A583 RID: 42371
			// (set) Token: 0x0600D5E1 RID: 54753 RVA: 0x0012FF26 File Offset: 0x0012E126
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700A584 RID: 42372
			// (set) Token: 0x0600D5E2 RID: 54754 RVA: 0x0012FF39 File Offset: 0x0012E139
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A585 RID: 42373
			// (set) Token: 0x0600D5E3 RID: 54755 RVA: 0x0012FF51 File Offset: 0x0012E151
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700A586 RID: 42374
			// (set) Token: 0x0600D5E4 RID: 54756 RVA: 0x0012FF69 File Offset: 0x0012E169
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A587 RID: 42375
			// (set) Token: 0x0600D5E5 RID: 54757 RVA: 0x0012FF7C File Offset: 0x0012E17C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A588 RID: 42376
			// (set) Token: 0x0600D5E6 RID: 54758 RVA: 0x0012FF94 File Offset: 0x0012E194
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A589 RID: 42377
			// (set) Token: 0x0600D5E7 RID: 54759 RVA: 0x0012FFAC File Offset: 0x0012E1AC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A58A RID: 42378
			// (set) Token: 0x0600D5E8 RID: 54760 RVA: 0x0012FFC4 File Offset: 0x0012E1C4
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
