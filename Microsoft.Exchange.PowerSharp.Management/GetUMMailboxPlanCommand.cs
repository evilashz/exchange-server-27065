using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E0C RID: 3596
	public class GetUMMailboxPlanCommand : SyntheticCommandWithPipelineInput<MailboxPlanIdParameter, MailboxPlanIdParameter>
	{
		// Token: 0x0600D5EA RID: 54762 RVA: 0x0012FFE4 File Offset: 0x0012E1E4
		private GetUMMailboxPlanCommand() : base("Get-UMMailboxPlan")
		{
		}

		// Token: 0x0600D5EB RID: 54763 RVA: 0x0012FFF1 File Offset: 0x0012E1F1
		public GetUMMailboxPlanCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D5EC RID: 54764 RVA: 0x00130000 File Offset: 0x0012E200
		public virtual GetUMMailboxPlanCommand SetParameters(GetUMMailboxPlanCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D5ED RID: 54765 RVA: 0x0013000A File Offset: 0x0012E20A
		public virtual GetUMMailboxPlanCommand SetParameters(GetUMMailboxPlanCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E0D RID: 3597
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A58B RID: 42379
			// (set) Token: 0x0600D5EE RID: 54766 RVA: 0x00130014 File Offset: 0x0012E214
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700A58C RID: 42380
			// (set) Token: 0x0600D5EF RID: 54767 RVA: 0x00130027 File Offset: 0x0012E227
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A58D RID: 42381
			// (set) Token: 0x0600D5F0 RID: 54768 RVA: 0x00130045 File Offset: 0x0012E245
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700A58E RID: 42382
			// (set) Token: 0x0600D5F1 RID: 54769 RVA: 0x00130058 File Offset: 0x0012E258
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700A58F RID: 42383
			// (set) Token: 0x0600D5F2 RID: 54770 RVA: 0x0013006B File Offset: 0x0012E26B
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A590 RID: 42384
			// (set) Token: 0x0600D5F3 RID: 54771 RVA: 0x00130089 File Offset: 0x0012E289
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A591 RID: 42385
			// (set) Token: 0x0600D5F4 RID: 54772 RVA: 0x001300A1 File Offset: 0x0012E2A1
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700A592 RID: 42386
			// (set) Token: 0x0600D5F5 RID: 54773 RVA: 0x001300B4 File Offset: 0x0012E2B4
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A593 RID: 42387
			// (set) Token: 0x0600D5F6 RID: 54774 RVA: 0x001300CC File Offset: 0x0012E2CC
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700A594 RID: 42388
			// (set) Token: 0x0600D5F7 RID: 54775 RVA: 0x001300E4 File Offset: 0x0012E2E4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A595 RID: 42389
			// (set) Token: 0x0600D5F8 RID: 54776 RVA: 0x001300F7 File Offset: 0x0012E2F7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A596 RID: 42390
			// (set) Token: 0x0600D5F9 RID: 54777 RVA: 0x0013010F File Offset: 0x0012E30F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A597 RID: 42391
			// (set) Token: 0x0600D5FA RID: 54778 RVA: 0x00130127 File Offset: 0x0012E327
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A598 RID: 42392
			// (set) Token: 0x0600D5FB RID: 54779 RVA: 0x0013013F File Offset: 0x0012E33F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000E0E RID: 3598
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A599 RID: 42393
			// (set) Token: 0x0600D5FD RID: 54781 RVA: 0x0013015F File Offset: 0x0012E35F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x1700A59A RID: 42394
			// (set) Token: 0x0600D5FE RID: 54782 RVA: 0x0013017D File Offset: 0x0012E37D
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700A59B RID: 42395
			// (set) Token: 0x0600D5FF RID: 54783 RVA: 0x00130190 File Offset: 0x0012E390
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A59C RID: 42396
			// (set) Token: 0x0600D600 RID: 54784 RVA: 0x001301AE File Offset: 0x0012E3AE
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700A59D RID: 42397
			// (set) Token: 0x0600D601 RID: 54785 RVA: 0x001301C1 File Offset: 0x0012E3C1
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700A59E RID: 42398
			// (set) Token: 0x0600D602 RID: 54786 RVA: 0x001301D4 File Offset: 0x0012E3D4
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A59F RID: 42399
			// (set) Token: 0x0600D603 RID: 54787 RVA: 0x001301F2 File Offset: 0x0012E3F2
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A5A0 RID: 42400
			// (set) Token: 0x0600D604 RID: 54788 RVA: 0x0013020A File Offset: 0x0012E40A
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700A5A1 RID: 42401
			// (set) Token: 0x0600D605 RID: 54789 RVA: 0x0013021D File Offset: 0x0012E41D
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A5A2 RID: 42402
			// (set) Token: 0x0600D606 RID: 54790 RVA: 0x00130235 File Offset: 0x0012E435
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700A5A3 RID: 42403
			// (set) Token: 0x0600D607 RID: 54791 RVA: 0x0013024D File Offset: 0x0012E44D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A5A4 RID: 42404
			// (set) Token: 0x0600D608 RID: 54792 RVA: 0x00130260 File Offset: 0x0012E460
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A5A5 RID: 42405
			// (set) Token: 0x0600D609 RID: 54793 RVA: 0x00130278 File Offset: 0x0012E478
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A5A6 RID: 42406
			// (set) Token: 0x0600D60A RID: 54794 RVA: 0x00130290 File Offset: 0x0012E490
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A5A7 RID: 42407
			// (set) Token: 0x0600D60B RID: 54795 RVA: 0x001302A8 File Offset: 0x0012E4A8
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
