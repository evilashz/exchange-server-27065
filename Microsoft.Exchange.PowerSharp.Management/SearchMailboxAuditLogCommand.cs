using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200018C RID: 396
	public class SearchMailboxAuditLogCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06002337 RID: 9015 RVA: 0x0004530A File Offset: 0x0004350A
		private SearchMailboxAuditLogCommand() : base("Search-MailboxAuditLog")
		{
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x00045317 File Offset: 0x00043517
		public SearchMailboxAuditLogCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x00045326 File Offset: 0x00043526
		public virtual SearchMailboxAuditLogCommand SetParameters(SearchMailboxAuditLogCommand.MultipleMailboxesSearchParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x00045330 File Offset: 0x00043530
		public virtual SearchMailboxAuditLogCommand SetParameters(SearchMailboxAuditLogCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x0004533A File Offset: 0x0004353A
		public virtual SearchMailboxAuditLogCommand SetParameters(SearchMailboxAuditLogCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200018D RID: 397
		public class MultipleMailboxesSearchParameters : ParametersBase
		{
			// Token: 0x17000BD8 RID: 3032
			// (set) Token: 0x0600233C RID: 9020 RVA: 0x00045344 File Offset: 0x00043544
			public virtual MultiValuedProperty<MailboxIdParameter> Mailboxes
			{
				set
				{
					base.PowerSharpParameters["Mailboxes"] = value;
				}
			}

			// Token: 0x17000BD9 RID: 3033
			// (set) Token: 0x0600233D RID: 9021 RVA: 0x00045357 File Offset: 0x00043557
			public virtual MultiValuedProperty<AuditScopes> LogonTypes
			{
				set
				{
					base.PowerSharpParameters["LogonTypes"] = value;
				}
			}

			// Token: 0x17000BDA RID: 3034
			// (set) Token: 0x0600233E RID: 9022 RVA: 0x0004536A File Offset: 0x0004356A
			public virtual MultiValuedProperty<MailboxAuditOperations> Operations
			{
				set
				{
					base.PowerSharpParameters["Operations"] = value;
				}
			}

			// Token: 0x17000BDB RID: 3035
			// (set) Token: 0x0600233F RID: 9023 RVA: 0x0004537D File Offset: 0x0004357D
			public virtual bool? ExternalAccess
			{
				set
				{
					base.PowerSharpParameters["ExternalAccess"] = value;
				}
			}

			// Token: 0x17000BDC RID: 3036
			// (set) Token: 0x06002340 RID: 9024 RVA: 0x00045395 File Offset: 0x00043595
			public virtual int ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17000BDD RID: 3037
			// (set) Token: 0x06002341 RID: 9025 RVA: 0x000453AD File Offset: 0x000435AD
			public virtual ExDateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17000BDE RID: 3038
			// (set) Token: 0x06002342 RID: 9026 RVA: 0x000453C5 File Offset: 0x000435C5
			public virtual ExDateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17000BDF RID: 3039
			// (set) Token: 0x06002343 RID: 9027 RVA: 0x000453DD File Offset: 0x000435DD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000BE0 RID: 3040
			// (set) Token: 0x06002344 RID: 9028 RVA: 0x000453F0 File Offset: 0x000435F0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000BE1 RID: 3041
			// (set) Token: 0x06002345 RID: 9029 RVA: 0x00045408 File Offset: 0x00043608
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000BE2 RID: 3042
			// (set) Token: 0x06002346 RID: 9030 RVA: 0x00045420 File Offset: 0x00043620
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000BE3 RID: 3043
			// (set) Token: 0x06002347 RID: 9031 RVA: 0x00045438 File Offset: 0x00043638
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200018E RID: 398
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000BE4 RID: 3044
			// (set) Token: 0x06002349 RID: 9033 RVA: 0x00045458 File Offset: 0x00043658
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000BE5 RID: 3045
			// (set) Token: 0x0600234A RID: 9034 RVA: 0x00045476 File Offset: 0x00043676
			public virtual MultiValuedProperty<AuditScopes> LogonTypes
			{
				set
				{
					base.PowerSharpParameters["LogonTypes"] = value;
				}
			}

			// Token: 0x17000BE6 RID: 3046
			// (set) Token: 0x0600234B RID: 9035 RVA: 0x00045489 File Offset: 0x00043689
			public virtual MultiValuedProperty<MailboxAuditOperations> Operations
			{
				set
				{
					base.PowerSharpParameters["Operations"] = value;
				}
			}

			// Token: 0x17000BE7 RID: 3047
			// (set) Token: 0x0600234C RID: 9036 RVA: 0x0004549C File Offset: 0x0004369C
			public virtual bool? ExternalAccess
			{
				set
				{
					base.PowerSharpParameters["ExternalAccess"] = value;
				}
			}

			// Token: 0x17000BE8 RID: 3048
			// (set) Token: 0x0600234D RID: 9037 RVA: 0x000454B4 File Offset: 0x000436B4
			public virtual int ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17000BE9 RID: 3049
			// (set) Token: 0x0600234E RID: 9038 RVA: 0x000454CC File Offset: 0x000436CC
			public virtual ExDateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17000BEA RID: 3050
			// (set) Token: 0x0600234F RID: 9039 RVA: 0x000454E4 File Offset: 0x000436E4
			public virtual ExDateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17000BEB RID: 3051
			// (set) Token: 0x06002350 RID: 9040 RVA: 0x000454FC File Offset: 0x000436FC
			public virtual SwitchParameter ShowDetails
			{
				set
				{
					base.PowerSharpParameters["ShowDetails"] = value;
				}
			}

			// Token: 0x17000BEC RID: 3052
			// (set) Token: 0x06002351 RID: 9041 RVA: 0x00045514 File Offset: 0x00043714
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17000BED RID: 3053
			// (set) Token: 0x06002352 RID: 9042 RVA: 0x00045532 File Offset: 0x00043732
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000BEE RID: 3054
			// (set) Token: 0x06002353 RID: 9043 RVA: 0x00045545 File Offset: 0x00043745
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000BEF RID: 3055
			// (set) Token: 0x06002354 RID: 9044 RVA: 0x0004555D File Offset: 0x0004375D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000BF0 RID: 3056
			// (set) Token: 0x06002355 RID: 9045 RVA: 0x00045575 File Offset: 0x00043775
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000BF1 RID: 3057
			// (set) Token: 0x06002356 RID: 9046 RVA: 0x0004558D File Offset: 0x0004378D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200018F RID: 399
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000BF2 RID: 3058
			// (set) Token: 0x06002358 RID: 9048 RVA: 0x000455AD File Offset: 0x000437AD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000BF3 RID: 3059
			// (set) Token: 0x06002359 RID: 9049 RVA: 0x000455C0 File Offset: 0x000437C0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000BF4 RID: 3060
			// (set) Token: 0x0600235A RID: 9050 RVA: 0x000455D8 File Offset: 0x000437D8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000BF5 RID: 3061
			// (set) Token: 0x0600235B RID: 9051 RVA: 0x000455F0 File Offset: 0x000437F0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000BF6 RID: 3062
			// (set) Token: 0x0600235C RID: 9052 RVA: 0x00045608 File Offset: 0x00043808
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
