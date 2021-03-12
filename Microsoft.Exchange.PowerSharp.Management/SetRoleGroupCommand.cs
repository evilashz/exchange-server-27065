using System;
using System.Management.Automation;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200033B RID: 827
	public class SetRoleGroupCommand : SyntheticCommandWithPipelineInputNoOutput<RoleGroup>
	{
		// Token: 0x060035DD RID: 13789 RVA: 0x0005DBD4 File Offset: 0x0005BDD4
		private SetRoleGroupCommand() : base("Set-RoleGroup")
		{
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x0005DBE1 File Offset: 0x0005BDE1
		public SetRoleGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x0005DBF0 File Offset: 0x0005BDF0
		public virtual SetRoleGroupCommand SetParameters(SetRoleGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x0005DBFA File Offset: 0x0005BDFA
		public virtual SetRoleGroupCommand SetParameters(SetRoleGroupCommand.ExchangeDatacenterCrossForestParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x0005DC04 File Offset: 0x0005BE04
		public virtual SetRoleGroupCommand SetParameters(SetRoleGroupCommand.crossforestParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200033C RID: 828
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001B20 RID: 6944
			// (set) Token: 0x060035E2 RID: 13794 RVA: 0x0005DC0E File Offset: 0x0005BE0E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleGroupIdParameter(value) : null);
				}
			}

			// Token: 0x17001B21 RID: 6945
			// (set) Token: 0x060035E3 RID: 13795 RVA: 0x0005DC2C File Offset: 0x0005BE2C
			public virtual MultiValuedProperty<SecurityPrincipalIdParameter> ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = value;
				}
			}

			// Token: 0x17001B22 RID: 6946
			// (set) Token: 0x060035E4 RID: 13796 RVA: 0x0005DC3F File Offset: 0x0005BE3F
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17001B23 RID: 6947
			// (set) Token: 0x060035E5 RID: 13797 RVA: 0x0005DC52 File Offset: 0x0005BE52
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x17001B24 RID: 6948
			// (set) Token: 0x060035E6 RID: 13798 RVA: 0x0005DC6A File Offset: 0x0005BE6A
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17001B25 RID: 6949
			// (set) Token: 0x060035E7 RID: 13799 RVA: 0x0005DC7D File Offset: 0x0005BE7D
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001B26 RID: 6950
			// (set) Token: 0x060035E8 RID: 13800 RVA: 0x0005DC95 File Offset: 0x0005BE95
			public virtual Guid ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17001B27 RID: 6951
			// (set) Token: 0x060035E9 RID: 13801 RVA: 0x0005DCAD File Offset: 0x0005BEAD
			public virtual Guid WellKnownObjectGuid
			{
				set
				{
					base.PowerSharpParameters["WellKnownObjectGuid"] = value;
				}
			}

			// Token: 0x17001B28 RID: 6952
			// (set) Token: 0x060035EA RID: 13802 RVA: 0x0005DCC5 File Offset: 0x0005BEC5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001B29 RID: 6953
			// (set) Token: 0x060035EB RID: 13803 RVA: 0x0005DCD8 File Offset: 0x0005BED8
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001B2A RID: 6954
			// (set) Token: 0x060035EC RID: 13804 RVA: 0x0005DCEB File Offset: 0x0005BEEB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001B2B RID: 6955
			// (set) Token: 0x060035ED RID: 13805 RVA: 0x0005DD03 File Offset: 0x0005BF03
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001B2C RID: 6956
			// (set) Token: 0x060035EE RID: 13806 RVA: 0x0005DD1B File Offset: 0x0005BF1B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001B2D RID: 6957
			// (set) Token: 0x060035EF RID: 13807 RVA: 0x0005DD33 File Offset: 0x0005BF33
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001B2E RID: 6958
			// (set) Token: 0x060035F0 RID: 13808 RVA: 0x0005DD4B File Offset: 0x0005BF4B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200033D RID: 829
		public class ExchangeDatacenterCrossForestParameterSetParameters : ParametersBase
		{
			// Token: 0x17001B2F RID: 6959
			// (set) Token: 0x060035F2 RID: 13810 RVA: 0x0005DD6B File Offset: 0x0005BF6B
			public virtual SecurityIdentifier LinkedForeignGroupSid
			{
				set
				{
					base.PowerSharpParameters["LinkedForeignGroupSid"] = value;
				}
			}

			// Token: 0x17001B30 RID: 6960
			// (set) Token: 0x060035F3 RID: 13811 RVA: 0x0005DD7E File Offset: 0x0005BF7E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleGroupIdParameter(value) : null);
				}
			}

			// Token: 0x17001B31 RID: 6961
			// (set) Token: 0x060035F4 RID: 13812 RVA: 0x0005DD9C File Offset: 0x0005BF9C
			public virtual MultiValuedProperty<SecurityPrincipalIdParameter> ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = value;
				}
			}

			// Token: 0x17001B32 RID: 6962
			// (set) Token: 0x060035F5 RID: 13813 RVA: 0x0005DDAF File Offset: 0x0005BFAF
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17001B33 RID: 6963
			// (set) Token: 0x060035F6 RID: 13814 RVA: 0x0005DDC2 File Offset: 0x0005BFC2
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x17001B34 RID: 6964
			// (set) Token: 0x060035F7 RID: 13815 RVA: 0x0005DDDA File Offset: 0x0005BFDA
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17001B35 RID: 6965
			// (set) Token: 0x060035F8 RID: 13816 RVA: 0x0005DDED File Offset: 0x0005BFED
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001B36 RID: 6966
			// (set) Token: 0x060035F9 RID: 13817 RVA: 0x0005DE05 File Offset: 0x0005C005
			public virtual Guid ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17001B37 RID: 6967
			// (set) Token: 0x060035FA RID: 13818 RVA: 0x0005DE1D File Offset: 0x0005C01D
			public virtual Guid WellKnownObjectGuid
			{
				set
				{
					base.PowerSharpParameters["WellKnownObjectGuid"] = value;
				}
			}

			// Token: 0x17001B38 RID: 6968
			// (set) Token: 0x060035FB RID: 13819 RVA: 0x0005DE35 File Offset: 0x0005C035
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001B39 RID: 6969
			// (set) Token: 0x060035FC RID: 13820 RVA: 0x0005DE48 File Offset: 0x0005C048
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001B3A RID: 6970
			// (set) Token: 0x060035FD RID: 13821 RVA: 0x0005DE5B File Offset: 0x0005C05B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001B3B RID: 6971
			// (set) Token: 0x060035FE RID: 13822 RVA: 0x0005DE73 File Offset: 0x0005C073
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001B3C RID: 6972
			// (set) Token: 0x060035FF RID: 13823 RVA: 0x0005DE8B File Offset: 0x0005C08B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001B3D RID: 6973
			// (set) Token: 0x06003600 RID: 13824 RVA: 0x0005DEA3 File Offset: 0x0005C0A3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001B3E RID: 6974
			// (set) Token: 0x06003601 RID: 13825 RVA: 0x0005DEBB File Offset: 0x0005C0BB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200033E RID: 830
		public class crossforestParameters : ParametersBase
		{
			// Token: 0x17001B3F RID: 6975
			// (set) Token: 0x06003603 RID: 13827 RVA: 0x0005DEDB File Offset: 0x0005C0DB
			public virtual string LinkedForeignGroup
			{
				set
				{
					base.PowerSharpParameters["LinkedForeignGroup"] = ((value != null) ? new UniversalSecurityGroupIdParameter(value) : null);
				}
			}

			// Token: 0x17001B40 RID: 6976
			// (set) Token: 0x06003604 RID: 13828 RVA: 0x0005DEF9 File Offset: 0x0005C0F9
			public virtual string LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x17001B41 RID: 6977
			// (set) Token: 0x06003605 RID: 13829 RVA: 0x0005DF0C File Offset: 0x0005C10C
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x17001B42 RID: 6978
			// (set) Token: 0x06003606 RID: 13830 RVA: 0x0005DF1F File Offset: 0x0005C11F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleGroupIdParameter(value) : null);
				}
			}

			// Token: 0x17001B43 RID: 6979
			// (set) Token: 0x06003607 RID: 13831 RVA: 0x0005DF3D File Offset: 0x0005C13D
			public virtual MultiValuedProperty<SecurityPrincipalIdParameter> ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = value;
				}
			}

			// Token: 0x17001B44 RID: 6980
			// (set) Token: 0x06003608 RID: 13832 RVA: 0x0005DF50 File Offset: 0x0005C150
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17001B45 RID: 6981
			// (set) Token: 0x06003609 RID: 13833 RVA: 0x0005DF63 File Offset: 0x0005C163
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x17001B46 RID: 6982
			// (set) Token: 0x0600360A RID: 13834 RVA: 0x0005DF7B File Offset: 0x0005C17B
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17001B47 RID: 6983
			// (set) Token: 0x0600360B RID: 13835 RVA: 0x0005DF8E File Offset: 0x0005C18E
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001B48 RID: 6984
			// (set) Token: 0x0600360C RID: 13836 RVA: 0x0005DFA6 File Offset: 0x0005C1A6
			public virtual Guid ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17001B49 RID: 6985
			// (set) Token: 0x0600360D RID: 13837 RVA: 0x0005DFBE File Offset: 0x0005C1BE
			public virtual Guid WellKnownObjectGuid
			{
				set
				{
					base.PowerSharpParameters["WellKnownObjectGuid"] = value;
				}
			}

			// Token: 0x17001B4A RID: 6986
			// (set) Token: 0x0600360E RID: 13838 RVA: 0x0005DFD6 File Offset: 0x0005C1D6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001B4B RID: 6987
			// (set) Token: 0x0600360F RID: 13839 RVA: 0x0005DFE9 File Offset: 0x0005C1E9
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001B4C RID: 6988
			// (set) Token: 0x06003610 RID: 13840 RVA: 0x0005DFFC File Offset: 0x0005C1FC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001B4D RID: 6989
			// (set) Token: 0x06003611 RID: 13841 RVA: 0x0005E014 File Offset: 0x0005C214
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001B4E RID: 6990
			// (set) Token: 0x06003612 RID: 13842 RVA: 0x0005E02C File Offset: 0x0005C22C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001B4F RID: 6991
			// (set) Token: 0x06003613 RID: 13843 RVA: 0x0005E044 File Offset: 0x0005C244
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001B50 RID: 6992
			// (set) Token: 0x06003614 RID: 13844 RVA: 0x0005E05C File Offset: 0x0005C25C
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
