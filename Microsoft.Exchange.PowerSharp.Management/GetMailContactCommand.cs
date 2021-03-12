using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000CEA RID: 3306
	public class GetMailContactCommand : SyntheticCommandWithPipelineInput<MailContact, MailContact>
	{
		// Token: 0x0600ADAA RID: 44458 RVA: 0x000FAF8F File Offset: 0x000F918F
		private GetMailContactCommand() : base("Get-MailContact")
		{
		}

		// Token: 0x0600ADAB RID: 44459 RVA: 0x000FAF9C File Offset: 0x000F919C
		public GetMailContactCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600ADAC RID: 44460 RVA: 0x000FAFAB File Offset: 0x000F91AB
		public virtual GetMailContactCommand SetParameters(GetMailContactCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600ADAD RID: 44461 RVA: 0x000FAFB5 File Offset: 0x000F91B5
		public virtual GetMailContactCommand SetParameters(GetMailContactCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600ADAE RID: 44462 RVA: 0x000FAFBF File Offset: 0x000F91BF
		public virtual GetMailContactCommand SetParameters(GetMailContactCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000CEB RID: 3307
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17007F8F RID: 32655
			// (set) Token: 0x0600ADAF RID: 44463 RVA: 0x000FAFC9 File Offset: 0x000F91C9
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x17007F90 RID: 32656
			// (set) Token: 0x0600ADB0 RID: 44464 RVA: 0x000FAFE1 File Offset: 0x000F91E1
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x17007F91 RID: 32657
			// (set) Token: 0x0600ADB1 RID: 44465 RVA: 0x000FAFF9 File Offset: 0x000F91F9
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17007F92 RID: 32658
			// (set) Token: 0x0600ADB2 RID: 44466 RVA: 0x000FB00C File Offset: 0x000F920C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007F93 RID: 32659
			// (set) Token: 0x0600ADB3 RID: 44467 RVA: 0x000FB02A File Offset: 0x000F922A
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17007F94 RID: 32660
			// (set) Token: 0x0600ADB4 RID: 44468 RVA: 0x000FB03D File Offset: 0x000F923D
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17007F95 RID: 32661
			// (set) Token: 0x0600ADB5 RID: 44469 RVA: 0x000FB050 File Offset: 0x000F9250
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007F96 RID: 32662
			// (set) Token: 0x0600ADB6 RID: 44470 RVA: 0x000FB06E File Offset: 0x000F926E
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17007F97 RID: 32663
			// (set) Token: 0x0600ADB7 RID: 44471 RVA: 0x000FB086 File Offset: 0x000F9286
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17007F98 RID: 32664
			// (set) Token: 0x0600ADB8 RID: 44472 RVA: 0x000FB099 File Offset: 0x000F9299
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17007F99 RID: 32665
			// (set) Token: 0x0600ADB9 RID: 44473 RVA: 0x000FB0B1 File Offset: 0x000F92B1
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17007F9A RID: 32666
			// (set) Token: 0x0600ADBA RID: 44474 RVA: 0x000FB0C9 File Offset: 0x000F92C9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007F9B RID: 32667
			// (set) Token: 0x0600ADBB RID: 44475 RVA: 0x000FB0DC File Offset: 0x000F92DC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007F9C RID: 32668
			// (set) Token: 0x0600ADBC RID: 44476 RVA: 0x000FB0F4 File Offset: 0x000F92F4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007F9D RID: 32669
			// (set) Token: 0x0600ADBD RID: 44477 RVA: 0x000FB10C File Offset: 0x000F930C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007F9E RID: 32670
			// (set) Token: 0x0600ADBE RID: 44478 RVA: 0x000FB124 File Offset: 0x000F9324
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000CEC RID: 3308
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x17007F9F RID: 32671
			// (set) Token: 0x0600ADC0 RID: 44480 RVA: 0x000FB144 File Offset: 0x000F9344
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x17007FA0 RID: 32672
			// (set) Token: 0x0600ADC1 RID: 44481 RVA: 0x000FB157 File Offset: 0x000F9357
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x17007FA1 RID: 32673
			// (set) Token: 0x0600ADC2 RID: 44482 RVA: 0x000FB16F File Offset: 0x000F936F
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x17007FA2 RID: 32674
			// (set) Token: 0x0600ADC3 RID: 44483 RVA: 0x000FB187 File Offset: 0x000F9387
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17007FA3 RID: 32675
			// (set) Token: 0x0600ADC4 RID: 44484 RVA: 0x000FB19A File Offset: 0x000F939A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007FA4 RID: 32676
			// (set) Token: 0x0600ADC5 RID: 44485 RVA: 0x000FB1B8 File Offset: 0x000F93B8
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17007FA5 RID: 32677
			// (set) Token: 0x0600ADC6 RID: 44486 RVA: 0x000FB1CB File Offset: 0x000F93CB
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17007FA6 RID: 32678
			// (set) Token: 0x0600ADC7 RID: 44487 RVA: 0x000FB1DE File Offset: 0x000F93DE
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007FA7 RID: 32679
			// (set) Token: 0x0600ADC8 RID: 44488 RVA: 0x000FB1FC File Offset: 0x000F93FC
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17007FA8 RID: 32680
			// (set) Token: 0x0600ADC9 RID: 44489 RVA: 0x000FB214 File Offset: 0x000F9414
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17007FA9 RID: 32681
			// (set) Token: 0x0600ADCA RID: 44490 RVA: 0x000FB227 File Offset: 0x000F9427
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17007FAA RID: 32682
			// (set) Token: 0x0600ADCB RID: 44491 RVA: 0x000FB23F File Offset: 0x000F943F
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17007FAB RID: 32683
			// (set) Token: 0x0600ADCC RID: 44492 RVA: 0x000FB257 File Offset: 0x000F9457
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007FAC RID: 32684
			// (set) Token: 0x0600ADCD RID: 44493 RVA: 0x000FB26A File Offset: 0x000F946A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007FAD RID: 32685
			// (set) Token: 0x0600ADCE RID: 44494 RVA: 0x000FB282 File Offset: 0x000F9482
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007FAE RID: 32686
			// (set) Token: 0x0600ADCF RID: 44495 RVA: 0x000FB29A File Offset: 0x000F949A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007FAF RID: 32687
			// (set) Token: 0x0600ADD0 RID: 44496 RVA: 0x000FB2B2 File Offset: 0x000F94B2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000CED RID: 3309
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17007FB0 RID: 32688
			// (set) Token: 0x0600ADD2 RID: 44498 RVA: 0x000FB2D2 File Offset: 0x000F94D2
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailContactIdParameter(value) : null);
				}
			}

			// Token: 0x17007FB1 RID: 32689
			// (set) Token: 0x0600ADD3 RID: 44499 RVA: 0x000FB2F0 File Offset: 0x000F94F0
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x17007FB2 RID: 32690
			// (set) Token: 0x0600ADD4 RID: 44500 RVA: 0x000FB308 File Offset: 0x000F9508
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x17007FB3 RID: 32691
			// (set) Token: 0x0600ADD5 RID: 44501 RVA: 0x000FB320 File Offset: 0x000F9520
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17007FB4 RID: 32692
			// (set) Token: 0x0600ADD6 RID: 44502 RVA: 0x000FB333 File Offset: 0x000F9533
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007FB5 RID: 32693
			// (set) Token: 0x0600ADD7 RID: 44503 RVA: 0x000FB351 File Offset: 0x000F9551
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17007FB6 RID: 32694
			// (set) Token: 0x0600ADD8 RID: 44504 RVA: 0x000FB364 File Offset: 0x000F9564
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17007FB7 RID: 32695
			// (set) Token: 0x0600ADD9 RID: 44505 RVA: 0x000FB377 File Offset: 0x000F9577
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007FB8 RID: 32696
			// (set) Token: 0x0600ADDA RID: 44506 RVA: 0x000FB395 File Offset: 0x000F9595
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17007FB9 RID: 32697
			// (set) Token: 0x0600ADDB RID: 44507 RVA: 0x000FB3AD File Offset: 0x000F95AD
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17007FBA RID: 32698
			// (set) Token: 0x0600ADDC RID: 44508 RVA: 0x000FB3C0 File Offset: 0x000F95C0
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17007FBB RID: 32699
			// (set) Token: 0x0600ADDD RID: 44509 RVA: 0x000FB3D8 File Offset: 0x000F95D8
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17007FBC RID: 32700
			// (set) Token: 0x0600ADDE RID: 44510 RVA: 0x000FB3F0 File Offset: 0x000F95F0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007FBD RID: 32701
			// (set) Token: 0x0600ADDF RID: 44511 RVA: 0x000FB403 File Offset: 0x000F9603
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007FBE RID: 32702
			// (set) Token: 0x0600ADE0 RID: 44512 RVA: 0x000FB41B File Offset: 0x000F961B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007FBF RID: 32703
			// (set) Token: 0x0600ADE1 RID: 44513 RVA: 0x000FB433 File Offset: 0x000F9633
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007FC0 RID: 32704
			// (set) Token: 0x0600ADE2 RID: 44514 RVA: 0x000FB44B File Offset: 0x000F964B
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
