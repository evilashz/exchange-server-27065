using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E26 RID: 3622
	public class GetUserCommand : SyntheticCommandWithPipelineInput<UserIdParameter, UserIdParameter>
	{
		// Token: 0x0600D71D RID: 55069 RVA: 0x00131980 File Offset: 0x0012FB80
		private GetUserCommand() : base("Get-User")
		{
		}

		// Token: 0x0600D71E RID: 55070 RVA: 0x0013198D File Offset: 0x0012FB8D
		public GetUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D71F RID: 55071 RVA: 0x0013199C File Offset: 0x0012FB9C
		public virtual GetUserCommand SetParameters(GetUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D720 RID: 55072 RVA: 0x001319A6 File Offset: 0x0012FBA6
		public virtual GetUserCommand SetParameters(GetUserCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D721 RID: 55073 RVA: 0x001319B0 File Offset: 0x0012FBB0
		public virtual GetUserCommand SetParameters(GetUserCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E27 RID: 3623
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A68A RID: 42634
			// (set) Token: 0x0600D722 RID: 55074 RVA: 0x001319BA File Offset: 0x0012FBBA
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x1700A68B RID: 42635
			// (set) Token: 0x0600D723 RID: 55075 RVA: 0x001319D2 File Offset: 0x0012FBD2
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x1700A68C RID: 42636
			// (set) Token: 0x0600D724 RID: 55076 RVA: 0x001319EA File Offset: 0x0012FBEA
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x1700A68D RID: 42637
			// (set) Token: 0x0600D725 RID: 55077 RVA: 0x00131A02 File Offset: 0x0012FC02
			public virtual NetID ConsumerNetID
			{
				set
				{
					base.PowerSharpParameters["ConsumerNetID"] = value;
				}
			}

			// Token: 0x1700A68E RID: 42638
			// (set) Token: 0x0600D726 RID: 55078 RVA: 0x00131A15 File Offset: 0x0012FC15
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x1700A68F RID: 42639
			// (set) Token: 0x0600D727 RID: 55079 RVA: 0x00131A2D File Offset: 0x0012FC2D
			public virtual SwitchParameter SoftDeletedUser
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedUser"] = value;
				}
			}

			// Token: 0x1700A690 RID: 42640
			// (set) Token: 0x0600D728 RID: 55080 RVA: 0x00131A45 File Offset: 0x0012FC45
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700A691 RID: 42641
			// (set) Token: 0x0600D729 RID: 55081 RVA: 0x00131A58 File Offset: 0x0012FC58
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A692 RID: 42642
			// (set) Token: 0x0600D72A RID: 55082 RVA: 0x00131A76 File Offset: 0x0012FC76
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700A693 RID: 42643
			// (set) Token: 0x0600D72B RID: 55083 RVA: 0x00131A89 File Offset: 0x0012FC89
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700A694 RID: 42644
			// (set) Token: 0x0600D72C RID: 55084 RVA: 0x00131A9C File Offset: 0x0012FC9C
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A695 RID: 42645
			// (set) Token: 0x0600D72D RID: 55085 RVA: 0x00131ABA File Offset: 0x0012FCBA
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A696 RID: 42646
			// (set) Token: 0x0600D72E RID: 55086 RVA: 0x00131AD2 File Offset: 0x0012FCD2
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700A697 RID: 42647
			// (set) Token: 0x0600D72F RID: 55087 RVA: 0x00131AE5 File Offset: 0x0012FCE5
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A698 RID: 42648
			// (set) Token: 0x0600D730 RID: 55088 RVA: 0x00131AFD File Offset: 0x0012FCFD
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700A699 RID: 42649
			// (set) Token: 0x0600D731 RID: 55089 RVA: 0x00131B15 File Offset: 0x0012FD15
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A69A RID: 42650
			// (set) Token: 0x0600D732 RID: 55090 RVA: 0x00131B28 File Offset: 0x0012FD28
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A69B RID: 42651
			// (set) Token: 0x0600D733 RID: 55091 RVA: 0x00131B40 File Offset: 0x0012FD40
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A69C RID: 42652
			// (set) Token: 0x0600D734 RID: 55092 RVA: 0x00131B58 File Offset: 0x0012FD58
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A69D RID: 42653
			// (set) Token: 0x0600D735 RID: 55093 RVA: 0x00131B70 File Offset: 0x0012FD70
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000E28 RID: 3624
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x1700A69E RID: 42654
			// (set) Token: 0x0600D737 RID: 55095 RVA: 0x00131B90 File Offset: 0x0012FD90
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x1700A69F RID: 42655
			// (set) Token: 0x0600D738 RID: 55096 RVA: 0x00131BA3 File Offset: 0x0012FDA3
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x1700A6A0 RID: 42656
			// (set) Token: 0x0600D739 RID: 55097 RVA: 0x00131BBB File Offset: 0x0012FDBB
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x1700A6A1 RID: 42657
			// (set) Token: 0x0600D73A RID: 55098 RVA: 0x00131BD3 File Offset: 0x0012FDD3
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x1700A6A2 RID: 42658
			// (set) Token: 0x0600D73B RID: 55099 RVA: 0x00131BEB File Offset: 0x0012FDEB
			public virtual NetID ConsumerNetID
			{
				set
				{
					base.PowerSharpParameters["ConsumerNetID"] = value;
				}
			}

			// Token: 0x1700A6A3 RID: 42659
			// (set) Token: 0x0600D73C RID: 55100 RVA: 0x00131BFE File Offset: 0x0012FDFE
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x1700A6A4 RID: 42660
			// (set) Token: 0x0600D73D RID: 55101 RVA: 0x00131C16 File Offset: 0x0012FE16
			public virtual SwitchParameter SoftDeletedUser
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedUser"] = value;
				}
			}

			// Token: 0x1700A6A5 RID: 42661
			// (set) Token: 0x0600D73E RID: 55102 RVA: 0x00131C2E File Offset: 0x0012FE2E
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700A6A6 RID: 42662
			// (set) Token: 0x0600D73F RID: 55103 RVA: 0x00131C41 File Offset: 0x0012FE41
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A6A7 RID: 42663
			// (set) Token: 0x0600D740 RID: 55104 RVA: 0x00131C5F File Offset: 0x0012FE5F
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700A6A8 RID: 42664
			// (set) Token: 0x0600D741 RID: 55105 RVA: 0x00131C72 File Offset: 0x0012FE72
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700A6A9 RID: 42665
			// (set) Token: 0x0600D742 RID: 55106 RVA: 0x00131C85 File Offset: 0x0012FE85
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A6AA RID: 42666
			// (set) Token: 0x0600D743 RID: 55107 RVA: 0x00131CA3 File Offset: 0x0012FEA3
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A6AB RID: 42667
			// (set) Token: 0x0600D744 RID: 55108 RVA: 0x00131CBB File Offset: 0x0012FEBB
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700A6AC RID: 42668
			// (set) Token: 0x0600D745 RID: 55109 RVA: 0x00131CCE File Offset: 0x0012FECE
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A6AD RID: 42669
			// (set) Token: 0x0600D746 RID: 55110 RVA: 0x00131CE6 File Offset: 0x0012FEE6
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700A6AE RID: 42670
			// (set) Token: 0x0600D747 RID: 55111 RVA: 0x00131CFE File Offset: 0x0012FEFE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A6AF RID: 42671
			// (set) Token: 0x0600D748 RID: 55112 RVA: 0x00131D11 File Offset: 0x0012FF11
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A6B0 RID: 42672
			// (set) Token: 0x0600D749 RID: 55113 RVA: 0x00131D29 File Offset: 0x0012FF29
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A6B1 RID: 42673
			// (set) Token: 0x0600D74A RID: 55114 RVA: 0x00131D41 File Offset: 0x0012FF41
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A6B2 RID: 42674
			// (set) Token: 0x0600D74B RID: 55115 RVA: 0x00131D59 File Offset: 0x0012FF59
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000E29 RID: 3625
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A6B3 RID: 42675
			// (set) Token: 0x0600D74D RID: 55117 RVA: 0x00131D79 File Offset: 0x0012FF79
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x1700A6B4 RID: 42676
			// (set) Token: 0x0600D74E RID: 55118 RVA: 0x00131D97 File Offset: 0x0012FF97
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x1700A6B5 RID: 42677
			// (set) Token: 0x0600D74F RID: 55119 RVA: 0x00131DAF File Offset: 0x0012FFAF
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x1700A6B6 RID: 42678
			// (set) Token: 0x0600D750 RID: 55120 RVA: 0x00131DC7 File Offset: 0x0012FFC7
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x1700A6B7 RID: 42679
			// (set) Token: 0x0600D751 RID: 55121 RVA: 0x00131DDF File Offset: 0x0012FFDF
			public virtual NetID ConsumerNetID
			{
				set
				{
					base.PowerSharpParameters["ConsumerNetID"] = value;
				}
			}

			// Token: 0x1700A6B8 RID: 42680
			// (set) Token: 0x0600D752 RID: 55122 RVA: 0x00131DF2 File Offset: 0x0012FFF2
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x1700A6B9 RID: 42681
			// (set) Token: 0x0600D753 RID: 55123 RVA: 0x00131E0A File Offset: 0x0013000A
			public virtual SwitchParameter SoftDeletedUser
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedUser"] = value;
				}
			}

			// Token: 0x1700A6BA RID: 42682
			// (set) Token: 0x0600D754 RID: 55124 RVA: 0x00131E22 File Offset: 0x00130022
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700A6BB RID: 42683
			// (set) Token: 0x0600D755 RID: 55125 RVA: 0x00131E35 File Offset: 0x00130035
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A6BC RID: 42684
			// (set) Token: 0x0600D756 RID: 55126 RVA: 0x00131E53 File Offset: 0x00130053
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700A6BD RID: 42685
			// (set) Token: 0x0600D757 RID: 55127 RVA: 0x00131E66 File Offset: 0x00130066
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700A6BE RID: 42686
			// (set) Token: 0x0600D758 RID: 55128 RVA: 0x00131E79 File Offset: 0x00130079
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A6BF RID: 42687
			// (set) Token: 0x0600D759 RID: 55129 RVA: 0x00131E97 File Offset: 0x00130097
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A6C0 RID: 42688
			// (set) Token: 0x0600D75A RID: 55130 RVA: 0x00131EAF File Offset: 0x001300AF
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700A6C1 RID: 42689
			// (set) Token: 0x0600D75B RID: 55131 RVA: 0x00131EC2 File Offset: 0x001300C2
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A6C2 RID: 42690
			// (set) Token: 0x0600D75C RID: 55132 RVA: 0x00131EDA File Offset: 0x001300DA
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700A6C3 RID: 42691
			// (set) Token: 0x0600D75D RID: 55133 RVA: 0x00131EF2 File Offset: 0x001300F2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A6C4 RID: 42692
			// (set) Token: 0x0600D75E RID: 55134 RVA: 0x00131F05 File Offset: 0x00130105
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A6C5 RID: 42693
			// (set) Token: 0x0600D75F RID: 55135 RVA: 0x00131F1D File Offset: 0x0013011D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A6C6 RID: 42694
			// (set) Token: 0x0600D760 RID: 55136 RVA: 0x00131F35 File Offset: 0x00130135
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A6C7 RID: 42695
			// (set) Token: 0x0600D761 RID: 55137 RVA: 0x00131F4D File Offset: 0x0013014D
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
