using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D76 RID: 3446
	public class UpdateSyncDistributionGroupMemberCommand : SyntheticCommandWithPipelineInputNoOutput<DistributionGroupIdParameter>
	{
		// Token: 0x0600B7E3 RID: 47075 RVA: 0x00108660 File Offset: 0x00106860
		private UpdateSyncDistributionGroupMemberCommand() : base("Update-SyncDistributionGroupMember")
		{
		}

		// Token: 0x0600B7E4 RID: 47076 RVA: 0x0010866D File Offset: 0x0010686D
		public UpdateSyncDistributionGroupMemberCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B7E5 RID: 47077 RVA: 0x0010867C File Offset: 0x0010687C
		public virtual UpdateSyncDistributionGroupMemberCommand SetParameters(UpdateSyncDistributionGroupMemberCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B7E6 RID: 47078 RVA: 0x00108686 File Offset: 0x00106886
		public virtual UpdateSyncDistributionGroupMemberCommand SetParameters(UpdateSyncDistributionGroupMemberCommand.ReplaceParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B7E7 RID: 47079 RVA: 0x00108690 File Offset: 0x00106890
		public virtual UpdateSyncDistributionGroupMemberCommand SetParameters(UpdateSyncDistributionGroupMemberCommand.AddOrRemoveParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B7E8 RID: 47080 RVA: 0x0010869A File Offset: 0x0010689A
		public virtual UpdateSyncDistributionGroupMemberCommand SetParameters(UpdateSyncDistributionGroupMemberCommand.RawMembersParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D77 RID: 3447
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170088B0 RID: 34992
			// (set) Token: 0x0600B7E9 RID: 47081 RVA: 0x001086A4 File Offset: 0x001068A4
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DistributionGroupIdParameter(value) : null);
				}
			}

			// Token: 0x170088B1 RID: 34993
			// (set) Token: 0x0600B7EA RID: 47082 RVA: 0x001086C2 File Offset: 0x001068C2
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x170088B2 RID: 34994
			// (set) Token: 0x0600B7EB RID: 47083 RVA: 0x001086DA File Offset: 0x001068DA
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x170088B3 RID: 34995
			// (set) Token: 0x0600B7EC RID: 47084 RVA: 0x001086F2 File Offset: 0x001068F2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170088B4 RID: 34996
			// (set) Token: 0x0600B7ED RID: 47085 RVA: 0x00108705 File Offset: 0x00106905
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170088B5 RID: 34997
			// (set) Token: 0x0600B7EE RID: 47086 RVA: 0x0010871D File Offset: 0x0010691D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170088B6 RID: 34998
			// (set) Token: 0x0600B7EF RID: 47087 RVA: 0x00108735 File Offset: 0x00106935
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170088B7 RID: 34999
			// (set) Token: 0x0600B7F0 RID: 47088 RVA: 0x0010874D File Offset: 0x0010694D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170088B8 RID: 35000
			// (set) Token: 0x0600B7F1 RID: 47089 RVA: 0x00108765 File Offset: 0x00106965
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D78 RID: 3448
		public class ReplaceParameters : ParametersBase
		{
			// Token: 0x170088B9 RID: 35001
			// (set) Token: 0x0600B7F3 RID: 47091 RVA: 0x00108785 File Offset: 0x00106985
			public virtual MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>> Members
			{
				set
				{
					base.PowerSharpParameters["Members"] = value;
				}
			}

			// Token: 0x170088BA RID: 35002
			// (set) Token: 0x0600B7F4 RID: 47092 RVA: 0x00108798 File Offset: 0x00106998
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DistributionGroupIdParameter(value) : null);
				}
			}

			// Token: 0x170088BB RID: 35003
			// (set) Token: 0x0600B7F5 RID: 47093 RVA: 0x001087B6 File Offset: 0x001069B6
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x170088BC RID: 35004
			// (set) Token: 0x0600B7F6 RID: 47094 RVA: 0x001087CE File Offset: 0x001069CE
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x170088BD RID: 35005
			// (set) Token: 0x0600B7F7 RID: 47095 RVA: 0x001087E6 File Offset: 0x001069E6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170088BE RID: 35006
			// (set) Token: 0x0600B7F8 RID: 47096 RVA: 0x001087F9 File Offset: 0x001069F9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170088BF RID: 35007
			// (set) Token: 0x0600B7F9 RID: 47097 RVA: 0x00108811 File Offset: 0x00106A11
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170088C0 RID: 35008
			// (set) Token: 0x0600B7FA RID: 47098 RVA: 0x00108829 File Offset: 0x00106A29
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170088C1 RID: 35009
			// (set) Token: 0x0600B7FB RID: 47099 RVA: 0x00108841 File Offset: 0x00106A41
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170088C2 RID: 35010
			// (set) Token: 0x0600B7FC RID: 47100 RVA: 0x00108859 File Offset: 0x00106A59
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D79 RID: 3449
		public class AddOrRemoveParameters : ParametersBase
		{
			// Token: 0x170088C3 RID: 35011
			// (set) Token: 0x0600B7FE RID: 47102 RVA: 0x00108879 File Offset: 0x00106A79
			public virtual MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>> AddedMembers
			{
				set
				{
					base.PowerSharpParameters["AddedMembers"] = value;
				}
			}

			// Token: 0x170088C4 RID: 35012
			// (set) Token: 0x0600B7FF RID: 47103 RVA: 0x0010888C File Offset: 0x00106A8C
			public virtual MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>> RemovedMembers
			{
				set
				{
					base.PowerSharpParameters["RemovedMembers"] = value;
				}
			}

			// Token: 0x170088C5 RID: 35013
			// (set) Token: 0x0600B800 RID: 47104 RVA: 0x0010889F File Offset: 0x00106A9F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DistributionGroupIdParameter(value) : null);
				}
			}

			// Token: 0x170088C6 RID: 35014
			// (set) Token: 0x0600B801 RID: 47105 RVA: 0x001088BD File Offset: 0x00106ABD
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x170088C7 RID: 35015
			// (set) Token: 0x0600B802 RID: 47106 RVA: 0x001088D5 File Offset: 0x00106AD5
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x170088C8 RID: 35016
			// (set) Token: 0x0600B803 RID: 47107 RVA: 0x001088ED File Offset: 0x00106AED
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170088C9 RID: 35017
			// (set) Token: 0x0600B804 RID: 47108 RVA: 0x00108900 File Offset: 0x00106B00
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170088CA RID: 35018
			// (set) Token: 0x0600B805 RID: 47109 RVA: 0x00108918 File Offset: 0x00106B18
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170088CB RID: 35019
			// (set) Token: 0x0600B806 RID: 47110 RVA: 0x00108930 File Offset: 0x00106B30
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170088CC RID: 35020
			// (set) Token: 0x0600B807 RID: 47111 RVA: 0x00108948 File Offset: 0x00106B48
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170088CD RID: 35021
			// (set) Token: 0x0600B808 RID: 47112 RVA: 0x00108960 File Offset: 0x00106B60
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D7A RID: 3450
		public class RawMembersParameters : ParametersBase
		{
			// Token: 0x170088CE RID: 35022
			// (set) Token: 0x0600B80A RID: 47114 RVA: 0x00108980 File Offset: 0x00106B80
			public virtual MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>> RawMembers
			{
				set
				{
					base.PowerSharpParameters["RawMembers"] = value;
				}
			}

			// Token: 0x170088CF RID: 35023
			// (set) Token: 0x0600B80B RID: 47115 RVA: 0x00108993 File Offset: 0x00106B93
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DistributionGroupIdParameter(value) : null);
				}
			}

			// Token: 0x170088D0 RID: 35024
			// (set) Token: 0x0600B80C RID: 47116 RVA: 0x001089B1 File Offset: 0x00106BB1
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x170088D1 RID: 35025
			// (set) Token: 0x0600B80D RID: 47117 RVA: 0x001089C9 File Offset: 0x00106BC9
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x170088D2 RID: 35026
			// (set) Token: 0x0600B80E RID: 47118 RVA: 0x001089E1 File Offset: 0x00106BE1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170088D3 RID: 35027
			// (set) Token: 0x0600B80F RID: 47119 RVA: 0x001089F4 File Offset: 0x00106BF4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170088D4 RID: 35028
			// (set) Token: 0x0600B810 RID: 47120 RVA: 0x00108A0C File Offset: 0x00106C0C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170088D5 RID: 35029
			// (set) Token: 0x0600B811 RID: 47121 RVA: 0x00108A24 File Offset: 0x00106C24
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170088D6 RID: 35030
			// (set) Token: 0x0600B812 RID: 47122 RVA: 0x00108A3C File Offset: 0x00106C3C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170088D7 RID: 35031
			// (set) Token: 0x0600B813 RID: 47123 RVA: 0x00108A54 File Offset: 0x00106C54
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
