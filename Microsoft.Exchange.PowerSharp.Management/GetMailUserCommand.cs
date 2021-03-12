using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000CFD RID: 3325
	public class GetMailUserCommand : SyntheticCommandWithPipelineInput<MailUserIdParameter, MailUserIdParameter>
	{
		// Token: 0x0600AF14 RID: 44820 RVA: 0x000FCD7A File Offset: 0x000FAF7A
		private GetMailUserCommand() : base("Get-MailUser")
		{
		}

		// Token: 0x0600AF15 RID: 44821 RVA: 0x000FCD87 File Offset: 0x000FAF87
		public GetMailUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600AF16 RID: 44822 RVA: 0x000FCD96 File Offset: 0x000FAF96
		public virtual GetMailUserCommand SetParameters(GetMailUserCommand.DatabaseParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AF17 RID: 44823 RVA: 0x000FCDA0 File Offset: 0x000FAFA0
		public virtual GetMailUserCommand SetParameters(GetMailUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AF18 RID: 44824 RVA: 0x000FCDAA File Offset: 0x000FAFAA
		public virtual GetMailUserCommand SetParameters(GetMailUserCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AF19 RID: 44825 RVA: 0x000FCDB4 File Offset: 0x000FAFB4
		public virtual GetMailUserCommand SetParameters(GetMailUserCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000CFE RID: 3326
		public class DatabaseParameters : ParametersBase
		{
			// Token: 0x170080D3 RID: 32979
			// (set) Token: 0x0600AF1A RID: 44826 RVA: 0x000FCDBE File Offset: 0x000FAFBE
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x170080D4 RID: 32980
			// (set) Token: 0x0600AF1B RID: 44827 RVA: 0x000FCDD1 File Offset: 0x000FAFD1
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x170080D5 RID: 32981
			// (set) Token: 0x0600AF1C RID: 44828 RVA: 0x000FCDE9 File Offset: 0x000FAFE9
			public virtual SwitchParameter SoftDeletedMailUser
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailUser"] = value;
				}
			}

			// Token: 0x170080D6 RID: 32982
			// (set) Token: 0x0600AF1D RID: 44829 RVA: 0x000FCE01 File Offset: 0x000FB001
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170080D7 RID: 32983
			// (set) Token: 0x0600AF1E RID: 44830 RVA: 0x000FCE14 File Offset: 0x000FB014
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170080D8 RID: 32984
			// (set) Token: 0x0600AF1F RID: 44831 RVA: 0x000FCE32 File Offset: 0x000FB032
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170080D9 RID: 32985
			// (set) Token: 0x0600AF20 RID: 44832 RVA: 0x000FCE45 File Offset: 0x000FB045
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170080DA RID: 32986
			// (set) Token: 0x0600AF21 RID: 44833 RVA: 0x000FCE58 File Offset: 0x000FB058
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170080DB RID: 32987
			// (set) Token: 0x0600AF22 RID: 44834 RVA: 0x000FCE76 File Offset: 0x000FB076
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170080DC RID: 32988
			// (set) Token: 0x0600AF23 RID: 44835 RVA: 0x000FCE8E File Offset: 0x000FB08E
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170080DD RID: 32989
			// (set) Token: 0x0600AF24 RID: 44836 RVA: 0x000FCEA1 File Offset: 0x000FB0A1
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170080DE RID: 32990
			// (set) Token: 0x0600AF25 RID: 44837 RVA: 0x000FCEB9 File Offset: 0x000FB0B9
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170080DF RID: 32991
			// (set) Token: 0x0600AF26 RID: 44838 RVA: 0x000FCED1 File Offset: 0x000FB0D1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170080E0 RID: 32992
			// (set) Token: 0x0600AF27 RID: 44839 RVA: 0x000FCEE4 File Offset: 0x000FB0E4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170080E1 RID: 32993
			// (set) Token: 0x0600AF28 RID: 44840 RVA: 0x000FCEFC File Offset: 0x000FB0FC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170080E2 RID: 32994
			// (set) Token: 0x0600AF29 RID: 44841 RVA: 0x000FCF14 File Offset: 0x000FB114
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170080E3 RID: 32995
			// (set) Token: 0x0600AF2A RID: 44842 RVA: 0x000FCF2C File Offset: 0x000FB12C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000CFF RID: 3327
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170080E4 RID: 32996
			// (set) Token: 0x0600AF2C RID: 44844 RVA: 0x000FCF4C File Offset: 0x000FB14C
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x170080E5 RID: 32997
			// (set) Token: 0x0600AF2D RID: 44845 RVA: 0x000FCF64 File Offset: 0x000FB164
			public virtual SwitchParameter SoftDeletedMailUser
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailUser"] = value;
				}
			}

			// Token: 0x170080E6 RID: 32998
			// (set) Token: 0x0600AF2E RID: 44846 RVA: 0x000FCF7C File Offset: 0x000FB17C
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170080E7 RID: 32999
			// (set) Token: 0x0600AF2F RID: 44847 RVA: 0x000FCF8F File Offset: 0x000FB18F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170080E8 RID: 33000
			// (set) Token: 0x0600AF30 RID: 44848 RVA: 0x000FCFAD File Offset: 0x000FB1AD
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170080E9 RID: 33001
			// (set) Token: 0x0600AF31 RID: 44849 RVA: 0x000FCFC0 File Offset: 0x000FB1C0
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170080EA RID: 33002
			// (set) Token: 0x0600AF32 RID: 44850 RVA: 0x000FCFD3 File Offset: 0x000FB1D3
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170080EB RID: 33003
			// (set) Token: 0x0600AF33 RID: 44851 RVA: 0x000FCFF1 File Offset: 0x000FB1F1
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170080EC RID: 33004
			// (set) Token: 0x0600AF34 RID: 44852 RVA: 0x000FD009 File Offset: 0x000FB209
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170080ED RID: 33005
			// (set) Token: 0x0600AF35 RID: 44853 RVA: 0x000FD01C File Offset: 0x000FB21C
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170080EE RID: 33006
			// (set) Token: 0x0600AF36 RID: 44854 RVA: 0x000FD034 File Offset: 0x000FB234
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170080EF RID: 33007
			// (set) Token: 0x0600AF37 RID: 44855 RVA: 0x000FD04C File Offset: 0x000FB24C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170080F0 RID: 33008
			// (set) Token: 0x0600AF38 RID: 44856 RVA: 0x000FD05F File Offset: 0x000FB25F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170080F1 RID: 33009
			// (set) Token: 0x0600AF39 RID: 44857 RVA: 0x000FD077 File Offset: 0x000FB277
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170080F2 RID: 33010
			// (set) Token: 0x0600AF3A RID: 44858 RVA: 0x000FD08F File Offset: 0x000FB28F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170080F3 RID: 33011
			// (set) Token: 0x0600AF3B RID: 44859 RVA: 0x000FD0A7 File Offset: 0x000FB2A7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000D00 RID: 3328
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x170080F4 RID: 33012
			// (set) Token: 0x0600AF3D RID: 44861 RVA: 0x000FD0C7 File Offset: 0x000FB2C7
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x170080F5 RID: 33013
			// (set) Token: 0x0600AF3E RID: 44862 RVA: 0x000FD0DA File Offset: 0x000FB2DA
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x170080F6 RID: 33014
			// (set) Token: 0x0600AF3F RID: 44863 RVA: 0x000FD0F2 File Offset: 0x000FB2F2
			public virtual SwitchParameter SoftDeletedMailUser
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailUser"] = value;
				}
			}

			// Token: 0x170080F7 RID: 33015
			// (set) Token: 0x0600AF40 RID: 44864 RVA: 0x000FD10A File Offset: 0x000FB30A
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170080F8 RID: 33016
			// (set) Token: 0x0600AF41 RID: 44865 RVA: 0x000FD11D File Offset: 0x000FB31D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170080F9 RID: 33017
			// (set) Token: 0x0600AF42 RID: 44866 RVA: 0x000FD13B File Offset: 0x000FB33B
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170080FA RID: 33018
			// (set) Token: 0x0600AF43 RID: 44867 RVA: 0x000FD14E File Offset: 0x000FB34E
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170080FB RID: 33019
			// (set) Token: 0x0600AF44 RID: 44868 RVA: 0x000FD161 File Offset: 0x000FB361
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170080FC RID: 33020
			// (set) Token: 0x0600AF45 RID: 44869 RVA: 0x000FD17F File Offset: 0x000FB37F
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170080FD RID: 33021
			// (set) Token: 0x0600AF46 RID: 44870 RVA: 0x000FD197 File Offset: 0x000FB397
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170080FE RID: 33022
			// (set) Token: 0x0600AF47 RID: 44871 RVA: 0x000FD1AA File Offset: 0x000FB3AA
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170080FF RID: 33023
			// (set) Token: 0x0600AF48 RID: 44872 RVA: 0x000FD1C2 File Offset: 0x000FB3C2
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17008100 RID: 33024
			// (set) Token: 0x0600AF49 RID: 44873 RVA: 0x000FD1DA File Offset: 0x000FB3DA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008101 RID: 33025
			// (set) Token: 0x0600AF4A RID: 44874 RVA: 0x000FD1ED File Offset: 0x000FB3ED
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008102 RID: 33026
			// (set) Token: 0x0600AF4B RID: 44875 RVA: 0x000FD205 File Offset: 0x000FB405
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008103 RID: 33027
			// (set) Token: 0x0600AF4C RID: 44876 RVA: 0x000FD21D File Offset: 0x000FB41D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008104 RID: 33028
			// (set) Token: 0x0600AF4D RID: 44877 RVA: 0x000FD235 File Offset: 0x000FB435
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000D01 RID: 3329
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17008105 RID: 33029
			// (set) Token: 0x0600AF4F RID: 44879 RVA: 0x000FD255 File Offset: 0x000FB455
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17008106 RID: 33030
			// (set) Token: 0x0600AF50 RID: 44880 RVA: 0x000FD273 File Offset: 0x000FB473
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x17008107 RID: 33031
			// (set) Token: 0x0600AF51 RID: 44881 RVA: 0x000FD28B File Offset: 0x000FB48B
			public virtual SwitchParameter SoftDeletedMailUser
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailUser"] = value;
				}
			}

			// Token: 0x17008108 RID: 33032
			// (set) Token: 0x0600AF52 RID: 44882 RVA: 0x000FD2A3 File Offset: 0x000FB4A3
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17008109 RID: 33033
			// (set) Token: 0x0600AF53 RID: 44883 RVA: 0x000FD2B6 File Offset: 0x000FB4B6
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700810A RID: 33034
			// (set) Token: 0x0600AF54 RID: 44884 RVA: 0x000FD2D4 File Offset: 0x000FB4D4
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700810B RID: 33035
			// (set) Token: 0x0600AF55 RID: 44885 RVA: 0x000FD2E7 File Offset: 0x000FB4E7
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700810C RID: 33036
			// (set) Token: 0x0600AF56 RID: 44886 RVA: 0x000FD2FA File Offset: 0x000FB4FA
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700810D RID: 33037
			// (set) Token: 0x0600AF57 RID: 44887 RVA: 0x000FD318 File Offset: 0x000FB518
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700810E RID: 33038
			// (set) Token: 0x0600AF58 RID: 44888 RVA: 0x000FD330 File Offset: 0x000FB530
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700810F RID: 33039
			// (set) Token: 0x0600AF59 RID: 44889 RVA: 0x000FD343 File Offset: 0x000FB543
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17008110 RID: 33040
			// (set) Token: 0x0600AF5A RID: 44890 RVA: 0x000FD35B File Offset: 0x000FB55B
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17008111 RID: 33041
			// (set) Token: 0x0600AF5B RID: 44891 RVA: 0x000FD373 File Offset: 0x000FB573
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008112 RID: 33042
			// (set) Token: 0x0600AF5C RID: 44892 RVA: 0x000FD386 File Offset: 0x000FB586
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008113 RID: 33043
			// (set) Token: 0x0600AF5D RID: 44893 RVA: 0x000FD39E File Offset: 0x000FB59E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008114 RID: 33044
			// (set) Token: 0x0600AF5E RID: 44894 RVA: 0x000FD3B6 File Offset: 0x000FB5B6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008115 RID: 33045
			// (set) Token: 0x0600AF5F RID: 44895 RVA: 0x000FD3CE File Offset: 0x000FB5CE
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
