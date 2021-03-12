using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DE4 RID: 3556
	public class GetSyncUserCommand : SyntheticCommandWithPipelineInput<NonMailEnabledUserIdParameter, NonMailEnabledUserIdParameter>
	{
		// Token: 0x0600D42F RID: 54319 RVA: 0x0012DBAB File Offset: 0x0012BDAB
		private GetSyncUserCommand() : base("Get-SyncUser")
		{
		}

		// Token: 0x0600D430 RID: 54320 RVA: 0x0012DBB8 File Offset: 0x0012BDB8
		public GetSyncUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D431 RID: 54321 RVA: 0x0012DBC7 File Offset: 0x0012BDC7
		public virtual GetSyncUserCommand SetParameters(GetSyncUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D432 RID: 54322 RVA: 0x0012DBD1 File Offset: 0x0012BDD1
		public virtual GetSyncUserCommand SetParameters(GetSyncUserCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D433 RID: 54323 RVA: 0x0012DBDB File Offset: 0x0012BDDB
		public virtual GetSyncUserCommand SetParameters(GetSyncUserCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DE5 RID: 3557
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A420 RID: 42016
			// (set) Token: 0x0600D434 RID: 54324 RVA: 0x0012DBE5 File Offset: 0x0012BDE5
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x1700A421 RID: 42017
			// (set) Token: 0x0600D435 RID: 54325 RVA: 0x0012DBFD File Offset: 0x0012BDFD
			public virtual SwitchParameter SoftDeletedUser
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedUser"] = value;
				}
			}

			// Token: 0x1700A422 RID: 42018
			// (set) Token: 0x0600D436 RID: 54326 RVA: 0x0012DC15 File Offset: 0x0012BE15
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700A423 RID: 42019
			// (set) Token: 0x0600D437 RID: 54327 RVA: 0x0012DC28 File Offset: 0x0012BE28
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A424 RID: 42020
			// (set) Token: 0x0600D438 RID: 54328 RVA: 0x0012DC46 File Offset: 0x0012BE46
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700A425 RID: 42021
			// (set) Token: 0x0600D439 RID: 54329 RVA: 0x0012DC59 File Offset: 0x0012BE59
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700A426 RID: 42022
			// (set) Token: 0x0600D43A RID: 54330 RVA: 0x0012DC6C File Offset: 0x0012BE6C
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A427 RID: 42023
			// (set) Token: 0x0600D43B RID: 54331 RVA: 0x0012DC8A File Offset: 0x0012BE8A
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A428 RID: 42024
			// (set) Token: 0x0600D43C RID: 54332 RVA: 0x0012DCA2 File Offset: 0x0012BEA2
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700A429 RID: 42025
			// (set) Token: 0x0600D43D RID: 54333 RVA: 0x0012DCB5 File Offset: 0x0012BEB5
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A42A RID: 42026
			// (set) Token: 0x0600D43E RID: 54334 RVA: 0x0012DCCD File Offset: 0x0012BECD
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700A42B RID: 42027
			// (set) Token: 0x0600D43F RID: 54335 RVA: 0x0012DCE5 File Offset: 0x0012BEE5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A42C RID: 42028
			// (set) Token: 0x0600D440 RID: 54336 RVA: 0x0012DCF8 File Offset: 0x0012BEF8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A42D RID: 42029
			// (set) Token: 0x0600D441 RID: 54337 RVA: 0x0012DD10 File Offset: 0x0012BF10
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A42E RID: 42030
			// (set) Token: 0x0600D442 RID: 54338 RVA: 0x0012DD28 File Offset: 0x0012BF28
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A42F RID: 42031
			// (set) Token: 0x0600D443 RID: 54339 RVA: 0x0012DD40 File Offset: 0x0012BF40
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000DE6 RID: 3558
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x1700A430 RID: 42032
			// (set) Token: 0x0600D445 RID: 54341 RVA: 0x0012DD60 File Offset: 0x0012BF60
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x1700A431 RID: 42033
			// (set) Token: 0x0600D446 RID: 54342 RVA: 0x0012DD73 File Offset: 0x0012BF73
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x1700A432 RID: 42034
			// (set) Token: 0x0600D447 RID: 54343 RVA: 0x0012DD8B File Offset: 0x0012BF8B
			public virtual SwitchParameter SoftDeletedUser
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedUser"] = value;
				}
			}

			// Token: 0x1700A433 RID: 42035
			// (set) Token: 0x0600D448 RID: 54344 RVA: 0x0012DDA3 File Offset: 0x0012BFA3
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700A434 RID: 42036
			// (set) Token: 0x0600D449 RID: 54345 RVA: 0x0012DDB6 File Offset: 0x0012BFB6
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A435 RID: 42037
			// (set) Token: 0x0600D44A RID: 54346 RVA: 0x0012DDD4 File Offset: 0x0012BFD4
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700A436 RID: 42038
			// (set) Token: 0x0600D44B RID: 54347 RVA: 0x0012DDE7 File Offset: 0x0012BFE7
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700A437 RID: 42039
			// (set) Token: 0x0600D44C RID: 54348 RVA: 0x0012DDFA File Offset: 0x0012BFFA
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A438 RID: 42040
			// (set) Token: 0x0600D44D RID: 54349 RVA: 0x0012DE18 File Offset: 0x0012C018
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A439 RID: 42041
			// (set) Token: 0x0600D44E RID: 54350 RVA: 0x0012DE30 File Offset: 0x0012C030
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700A43A RID: 42042
			// (set) Token: 0x0600D44F RID: 54351 RVA: 0x0012DE43 File Offset: 0x0012C043
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A43B RID: 42043
			// (set) Token: 0x0600D450 RID: 54352 RVA: 0x0012DE5B File Offset: 0x0012C05B
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700A43C RID: 42044
			// (set) Token: 0x0600D451 RID: 54353 RVA: 0x0012DE73 File Offset: 0x0012C073
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A43D RID: 42045
			// (set) Token: 0x0600D452 RID: 54354 RVA: 0x0012DE86 File Offset: 0x0012C086
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A43E RID: 42046
			// (set) Token: 0x0600D453 RID: 54355 RVA: 0x0012DE9E File Offset: 0x0012C09E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A43F RID: 42047
			// (set) Token: 0x0600D454 RID: 54356 RVA: 0x0012DEB6 File Offset: 0x0012C0B6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A440 RID: 42048
			// (set) Token: 0x0600D455 RID: 54357 RVA: 0x0012DECE File Offset: 0x0012C0CE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000DE7 RID: 3559
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A441 RID: 42049
			// (set) Token: 0x0600D457 RID: 54359 RVA: 0x0012DEEE File Offset: 0x0012C0EE
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new NonMailEnabledUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700A442 RID: 42050
			// (set) Token: 0x0600D458 RID: 54360 RVA: 0x0012DF0C File Offset: 0x0012C10C
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x1700A443 RID: 42051
			// (set) Token: 0x0600D459 RID: 54361 RVA: 0x0012DF24 File Offset: 0x0012C124
			public virtual SwitchParameter SoftDeletedUser
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedUser"] = value;
				}
			}

			// Token: 0x1700A444 RID: 42052
			// (set) Token: 0x0600D45A RID: 54362 RVA: 0x0012DF3C File Offset: 0x0012C13C
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700A445 RID: 42053
			// (set) Token: 0x0600D45B RID: 54363 RVA: 0x0012DF4F File Offset: 0x0012C14F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A446 RID: 42054
			// (set) Token: 0x0600D45C RID: 54364 RVA: 0x0012DF6D File Offset: 0x0012C16D
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700A447 RID: 42055
			// (set) Token: 0x0600D45D RID: 54365 RVA: 0x0012DF80 File Offset: 0x0012C180
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700A448 RID: 42056
			// (set) Token: 0x0600D45E RID: 54366 RVA: 0x0012DF93 File Offset: 0x0012C193
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A449 RID: 42057
			// (set) Token: 0x0600D45F RID: 54367 RVA: 0x0012DFB1 File Offset: 0x0012C1B1
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A44A RID: 42058
			// (set) Token: 0x0600D460 RID: 54368 RVA: 0x0012DFC9 File Offset: 0x0012C1C9
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700A44B RID: 42059
			// (set) Token: 0x0600D461 RID: 54369 RVA: 0x0012DFDC File Offset: 0x0012C1DC
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A44C RID: 42060
			// (set) Token: 0x0600D462 RID: 54370 RVA: 0x0012DFF4 File Offset: 0x0012C1F4
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700A44D RID: 42061
			// (set) Token: 0x0600D463 RID: 54371 RVA: 0x0012E00C File Offset: 0x0012C20C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A44E RID: 42062
			// (set) Token: 0x0600D464 RID: 54372 RVA: 0x0012E01F File Offset: 0x0012C21F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A44F RID: 42063
			// (set) Token: 0x0600D465 RID: 54373 RVA: 0x0012E037 File Offset: 0x0012C237
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A450 RID: 42064
			// (set) Token: 0x0600D466 RID: 54374 RVA: 0x0012E04F File Offset: 0x0012C24F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A451 RID: 42065
			// (set) Token: 0x0600D467 RID: 54375 RVA: 0x0012E067 File Offset: 0x0012C267
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
