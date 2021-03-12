using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D6D RID: 3437
	public class GetSyncDistributionGroupCommand : SyntheticCommandWithPipelineInput<SyncDistributionGroup, SyncDistributionGroup>
	{
		// Token: 0x0600B771 RID: 46961 RVA: 0x00107CDE File Offset: 0x00105EDE
		private GetSyncDistributionGroupCommand() : base("Get-SyncDistributionGroup")
		{
		}

		// Token: 0x0600B772 RID: 46962 RVA: 0x00107CEB File Offset: 0x00105EEB
		public GetSyncDistributionGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B773 RID: 46963 RVA: 0x00107CFA File Offset: 0x00105EFA
		public virtual GetSyncDistributionGroupCommand SetParameters(GetSyncDistributionGroupCommand.CookieSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B774 RID: 46964 RVA: 0x00107D04 File Offset: 0x00105F04
		public virtual GetSyncDistributionGroupCommand SetParameters(GetSyncDistributionGroupCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B775 RID: 46965 RVA: 0x00107D0E File Offset: 0x00105F0E
		public virtual GetSyncDistributionGroupCommand SetParameters(GetSyncDistributionGroupCommand.ManagedBySetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B776 RID: 46966 RVA: 0x00107D18 File Offset: 0x00105F18
		public virtual GetSyncDistributionGroupCommand SetParameters(GetSyncDistributionGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B777 RID: 46967 RVA: 0x00107D22 File Offset: 0x00105F22
		public virtual GetSyncDistributionGroupCommand SetParameters(GetSyncDistributionGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D6E RID: 3438
		public class CookieSetParameters : ParametersBase
		{
			// Token: 0x17008850 RID: 34896
			// (set) Token: 0x0600B778 RID: 46968 RVA: 0x00107D2C File Offset: 0x00105F2C
			public virtual byte Cookie
			{
				set
				{
					base.PowerSharpParameters["Cookie"] = value;
				}
			}

			// Token: 0x17008851 RID: 34897
			// (set) Token: 0x0600B779 RID: 46969 RVA: 0x00107D44 File Offset: 0x00105F44
			public virtual int Pages
			{
				set
				{
					base.PowerSharpParameters["Pages"] = value;
				}
			}

			// Token: 0x17008852 RID: 34898
			// (set) Token: 0x0600B77A RID: 46970 RVA: 0x00107D5C File Offset: 0x00105F5C
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17008853 RID: 34899
			// (set) Token: 0x0600B77B RID: 46971 RVA: 0x00107D74 File Offset: 0x00105F74
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x17008854 RID: 34900
			// (set) Token: 0x0600B77C RID: 46972 RVA: 0x00107D8C File Offset: 0x00105F8C
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17008855 RID: 34901
			// (set) Token: 0x0600B77D RID: 46973 RVA: 0x00107D9F File Offset: 0x00105F9F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008856 RID: 34902
			// (set) Token: 0x0600B77E RID: 46974 RVA: 0x00107DBD File Offset: 0x00105FBD
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17008857 RID: 34903
			// (set) Token: 0x0600B77F RID: 46975 RVA: 0x00107DD0 File Offset: 0x00105FD0
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008858 RID: 34904
			// (set) Token: 0x0600B780 RID: 46976 RVA: 0x00107DEE File Offset: 0x00105FEE
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17008859 RID: 34905
			// (set) Token: 0x0600B781 RID: 46977 RVA: 0x00107E01 File Offset: 0x00106001
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700885A RID: 34906
			// (set) Token: 0x0600B782 RID: 46978 RVA: 0x00107E14 File Offset: 0x00106014
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700885B RID: 34907
			// (set) Token: 0x0600B783 RID: 46979 RVA: 0x00107E2C File Offset: 0x0010602C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700885C RID: 34908
			// (set) Token: 0x0600B784 RID: 46980 RVA: 0x00107E44 File Offset: 0x00106044
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700885D RID: 34909
			// (set) Token: 0x0600B785 RID: 46981 RVA: 0x00107E5C File Offset: 0x0010605C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000D6F RID: 3439
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x1700885E RID: 34910
			// (set) Token: 0x0600B787 RID: 46983 RVA: 0x00107E7C File Offset: 0x0010607C
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700885F RID: 34911
			// (set) Token: 0x0600B788 RID: 46984 RVA: 0x00107E94 File Offset: 0x00106094
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17008860 RID: 34912
			// (set) Token: 0x0600B789 RID: 46985 RVA: 0x00107EAC File Offset: 0x001060AC
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17008861 RID: 34913
			// (set) Token: 0x0600B78A RID: 46986 RVA: 0x00107EC4 File Offset: 0x001060C4
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17008862 RID: 34914
			// (set) Token: 0x0600B78B RID: 46987 RVA: 0x00107ED7 File Offset: 0x001060D7
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x17008863 RID: 34915
			// (set) Token: 0x0600B78C RID: 46988 RVA: 0x00107EEA File Offset: 0x001060EA
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17008864 RID: 34916
			// (set) Token: 0x0600B78D RID: 46989 RVA: 0x00107F02 File Offset: 0x00106102
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x17008865 RID: 34917
			// (set) Token: 0x0600B78E RID: 46990 RVA: 0x00107F1A File Offset: 0x0010611A
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17008866 RID: 34918
			// (set) Token: 0x0600B78F RID: 46991 RVA: 0x00107F2D File Offset: 0x0010612D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008867 RID: 34919
			// (set) Token: 0x0600B790 RID: 46992 RVA: 0x00107F4B File Offset: 0x0010614B
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17008868 RID: 34920
			// (set) Token: 0x0600B791 RID: 46993 RVA: 0x00107F5E File Offset: 0x0010615E
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008869 RID: 34921
			// (set) Token: 0x0600B792 RID: 46994 RVA: 0x00107F7C File Offset: 0x0010617C
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700886A RID: 34922
			// (set) Token: 0x0600B793 RID: 46995 RVA: 0x00107F8F File Offset: 0x0010618F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700886B RID: 34923
			// (set) Token: 0x0600B794 RID: 46996 RVA: 0x00107FA2 File Offset: 0x001061A2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700886C RID: 34924
			// (set) Token: 0x0600B795 RID: 46997 RVA: 0x00107FBA File Offset: 0x001061BA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700886D RID: 34925
			// (set) Token: 0x0600B796 RID: 46998 RVA: 0x00107FD2 File Offset: 0x001061D2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700886E RID: 34926
			// (set) Token: 0x0600B797 RID: 46999 RVA: 0x00107FEA File Offset: 0x001061EA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000D70 RID: 3440
		public class ManagedBySetParameters : ParametersBase
		{
			// Token: 0x1700886F RID: 34927
			// (set) Token: 0x0600B799 RID: 47001 RVA: 0x0010800A File Offset: 0x0010620A
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17008870 RID: 34928
			// (set) Token: 0x0600B79A RID: 47002 RVA: 0x00108022 File Offset: 0x00106222
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17008871 RID: 34929
			// (set) Token: 0x0600B79B RID: 47003 RVA: 0x0010803A File Offset: 0x0010623A
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17008872 RID: 34930
			// (set) Token: 0x0600B79C RID: 47004 RVA: 0x00108052 File Offset: 0x00106252
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17008873 RID: 34931
			// (set) Token: 0x0600B79D RID: 47005 RVA: 0x00108065 File Offset: 0x00106265
			public virtual string ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = ((value != null) ? new GeneralRecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17008874 RID: 34932
			// (set) Token: 0x0600B79E RID: 47006 RVA: 0x00108083 File Offset: 0x00106283
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17008875 RID: 34933
			// (set) Token: 0x0600B79F RID: 47007 RVA: 0x0010809B File Offset: 0x0010629B
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x17008876 RID: 34934
			// (set) Token: 0x0600B7A0 RID: 47008 RVA: 0x001080B3 File Offset: 0x001062B3
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17008877 RID: 34935
			// (set) Token: 0x0600B7A1 RID: 47009 RVA: 0x001080C6 File Offset: 0x001062C6
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008878 RID: 34936
			// (set) Token: 0x0600B7A2 RID: 47010 RVA: 0x001080E4 File Offset: 0x001062E4
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17008879 RID: 34937
			// (set) Token: 0x0600B7A3 RID: 47011 RVA: 0x001080F7 File Offset: 0x001062F7
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700887A RID: 34938
			// (set) Token: 0x0600B7A4 RID: 47012 RVA: 0x00108115 File Offset: 0x00106315
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700887B RID: 34939
			// (set) Token: 0x0600B7A5 RID: 47013 RVA: 0x00108128 File Offset: 0x00106328
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700887C RID: 34940
			// (set) Token: 0x0600B7A6 RID: 47014 RVA: 0x0010813B File Offset: 0x0010633B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700887D RID: 34941
			// (set) Token: 0x0600B7A7 RID: 47015 RVA: 0x00108153 File Offset: 0x00106353
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700887E RID: 34942
			// (set) Token: 0x0600B7A8 RID: 47016 RVA: 0x0010816B File Offset: 0x0010636B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700887F RID: 34943
			// (set) Token: 0x0600B7A9 RID: 47017 RVA: 0x00108183 File Offset: 0x00106383
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000D71 RID: 3441
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17008880 RID: 34944
			// (set) Token: 0x0600B7AB RID: 47019 RVA: 0x001081A3 File Offset: 0x001063A3
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17008881 RID: 34945
			// (set) Token: 0x0600B7AC RID: 47020 RVA: 0x001081BB File Offset: 0x001063BB
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17008882 RID: 34946
			// (set) Token: 0x0600B7AD RID: 47021 RVA: 0x001081D3 File Offset: 0x001063D3
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17008883 RID: 34947
			// (set) Token: 0x0600B7AE RID: 47022 RVA: 0x001081EB File Offset: 0x001063EB
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17008884 RID: 34948
			// (set) Token: 0x0600B7AF RID: 47023 RVA: 0x001081FE File Offset: 0x001063FE
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DistributionGroupIdParameter(value) : null);
				}
			}

			// Token: 0x17008885 RID: 34949
			// (set) Token: 0x0600B7B0 RID: 47024 RVA: 0x0010821C File Offset: 0x0010641C
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17008886 RID: 34950
			// (set) Token: 0x0600B7B1 RID: 47025 RVA: 0x00108234 File Offset: 0x00106434
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x17008887 RID: 34951
			// (set) Token: 0x0600B7B2 RID: 47026 RVA: 0x0010824C File Offset: 0x0010644C
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17008888 RID: 34952
			// (set) Token: 0x0600B7B3 RID: 47027 RVA: 0x0010825F File Offset: 0x0010645F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008889 RID: 34953
			// (set) Token: 0x0600B7B4 RID: 47028 RVA: 0x0010827D File Offset: 0x0010647D
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700888A RID: 34954
			// (set) Token: 0x0600B7B5 RID: 47029 RVA: 0x00108290 File Offset: 0x00106490
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700888B RID: 34955
			// (set) Token: 0x0600B7B6 RID: 47030 RVA: 0x001082AE File Offset: 0x001064AE
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700888C RID: 34956
			// (set) Token: 0x0600B7B7 RID: 47031 RVA: 0x001082C1 File Offset: 0x001064C1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700888D RID: 34957
			// (set) Token: 0x0600B7B8 RID: 47032 RVA: 0x001082D4 File Offset: 0x001064D4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700888E RID: 34958
			// (set) Token: 0x0600B7B9 RID: 47033 RVA: 0x001082EC File Offset: 0x001064EC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700888F RID: 34959
			// (set) Token: 0x0600B7BA RID: 47034 RVA: 0x00108304 File Offset: 0x00106504
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008890 RID: 34960
			// (set) Token: 0x0600B7BB RID: 47035 RVA: 0x0010831C File Offset: 0x0010651C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000D72 RID: 3442
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17008891 RID: 34961
			// (set) Token: 0x0600B7BD RID: 47037 RVA: 0x0010833C File Offset: 0x0010653C
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17008892 RID: 34962
			// (set) Token: 0x0600B7BE RID: 47038 RVA: 0x00108354 File Offset: 0x00106554
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x17008893 RID: 34963
			// (set) Token: 0x0600B7BF RID: 47039 RVA: 0x0010836C File Offset: 0x0010656C
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17008894 RID: 34964
			// (set) Token: 0x0600B7C0 RID: 47040 RVA: 0x0010837F File Offset: 0x0010657F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008895 RID: 34965
			// (set) Token: 0x0600B7C1 RID: 47041 RVA: 0x0010839D File Offset: 0x0010659D
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17008896 RID: 34966
			// (set) Token: 0x0600B7C2 RID: 47042 RVA: 0x001083B0 File Offset: 0x001065B0
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008897 RID: 34967
			// (set) Token: 0x0600B7C3 RID: 47043 RVA: 0x001083CE File Offset: 0x001065CE
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17008898 RID: 34968
			// (set) Token: 0x0600B7C4 RID: 47044 RVA: 0x001083E1 File Offset: 0x001065E1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008899 RID: 34969
			// (set) Token: 0x0600B7C5 RID: 47045 RVA: 0x001083F4 File Offset: 0x001065F4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700889A RID: 34970
			// (set) Token: 0x0600B7C6 RID: 47046 RVA: 0x0010840C File Offset: 0x0010660C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700889B RID: 34971
			// (set) Token: 0x0600B7C7 RID: 47047 RVA: 0x00108424 File Offset: 0x00106624
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700889C RID: 34972
			// (set) Token: 0x0600B7C8 RID: 47048 RVA: 0x0010843C File Offset: 0x0010663C
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
