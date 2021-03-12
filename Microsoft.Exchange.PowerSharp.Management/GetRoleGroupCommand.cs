using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000337 RID: 823
	public class GetRoleGroupCommand : SyntheticCommandWithPipelineInput<ADGroup, ADGroup>
	{
		// Token: 0x060035AE RID: 13742 RVA: 0x0005D7FE File Offset: 0x0005B9FE
		private GetRoleGroupCommand() : base("Get-RoleGroup")
		{
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x0005D80B File Offset: 0x0005BA0B
		public GetRoleGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x0005D81A File Offset: 0x0005BA1A
		public virtual GetRoleGroupCommand SetParameters(GetRoleGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x0005D824 File Offset: 0x0005BA24
		public virtual GetRoleGroupCommand SetParameters(GetRoleGroupCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x0005D82E File Offset: 0x0005BA2E
		public virtual GetRoleGroupCommand SetParameters(GetRoleGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000338 RID: 824
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001AF9 RID: 6905
			// (set) Token: 0x060035B3 RID: 13747 RVA: 0x0005D838 File Offset: 0x0005BA38
			public virtual SwitchParameter ShowPartnerLinked
			{
				set
				{
					base.PowerSharpParameters["ShowPartnerLinked"] = value;
				}
			}

			// Token: 0x17001AFA RID: 6906
			// (set) Token: 0x060035B4 RID: 13748 RVA: 0x0005D850 File Offset: 0x0005BA50
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x17001AFB RID: 6907
			// (set) Token: 0x060035B5 RID: 13749 RVA: 0x0005D868 File Offset: 0x0005BA68
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17001AFC RID: 6908
			// (set) Token: 0x060035B6 RID: 13750 RVA: 0x0005D87B File Offset: 0x0005BA7B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001AFD RID: 6909
			// (set) Token: 0x060035B7 RID: 13751 RVA: 0x0005D899 File Offset: 0x0005BA99
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17001AFE RID: 6910
			// (set) Token: 0x060035B8 RID: 13752 RVA: 0x0005D8AC File Offset: 0x0005BAAC
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17001AFF RID: 6911
			// (set) Token: 0x060035B9 RID: 13753 RVA: 0x0005D8BF File Offset: 0x0005BABF
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001B00 RID: 6912
			// (set) Token: 0x060035BA RID: 13754 RVA: 0x0005D8D7 File Offset: 0x0005BAD7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001B01 RID: 6913
			// (set) Token: 0x060035BB RID: 13755 RVA: 0x0005D8EA File Offset: 0x0005BAEA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001B02 RID: 6914
			// (set) Token: 0x060035BC RID: 13756 RVA: 0x0005D902 File Offset: 0x0005BB02
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001B03 RID: 6915
			// (set) Token: 0x060035BD RID: 13757 RVA: 0x0005D91A File Offset: 0x0005BB1A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001B04 RID: 6916
			// (set) Token: 0x060035BE RID: 13758 RVA: 0x0005D932 File Offset: 0x0005BB32
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000339 RID: 825
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x17001B05 RID: 6917
			// (set) Token: 0x060035C0 RID: 13760 RVA: 0x0005D952 File Offset: 0x0005BB52
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17001B06 RID: 6918
			// (set) Token: 0x060035C1 RID: 13761 RVA: 0x0005D96A File Offset: 0x0005BB6A
			public virtual SwitchParameter ShowPartnerLinked
			{
				set
				{
					base.PowerSharpParameters["ShowPartnerLinked"] = value;
				}
			}

			// Token: 0x17001B07 RID: 6919
			// (set) Token: 0x060035C2 RID: 13762 RVA: 0x0005D982 File Offset: 0x0005BB82
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x17001B08 RID: 6920
			// (set) Token: 0x060035C3 RID: 13763 RVA: 0x0005D99A File Offset: 0x0005BB9A
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17001B09 RID: 6921
			// (set) Token: 0x060035C4 RID: 13764 RVA: 0x0005D9AD File Offset: 0x0005BBAD
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001B0A RID: 6922
			// (set) Token: 0x060035C5 RID: 13765 RVA: 0x0005D9CB File Offset: 0x0005BBCB
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17001B0B RID: 6923
			// (set) Token: 0x060035C6 RID: 13766 RVA: 0x0005D9DE File Offset: 0x0005BBDE
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17001B0C RID: 6924
			// (set) Token: 0x060035C7 RID: 13767 RVA: 0x0005D9F1 File Offset: 0x0005BBF1
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001B0D RID: 6925
			// (set) Token: 0x060035C8 RID: 13768 RVA: 0x0005DA09 File Offset: 0x0005BC09
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001B0E RID: 6926
			// (set) Token: 0x060035C9 RID: 13769 RVA: 0x0005DA1C File Offset: 0x0005BC1C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001B0F RID: 6927
			// (set) Token: 0x060035CA RID: 13770 RVA: 0x0005DA34 File Offset: 0x0005BC34
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001B10 RID: 6928
			// (set) Token: 0x060035CB RID: 13771 RVA: 0x0005DA4C File Offset: 0x0005BC4C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001B11 RID: 6929
			// (set) Token: 0x060035CC RID: 13772 RVA: 0x0005DA64 File Offset: 0x0005BC64
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200033A RID: 826
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001B12 RID: 6930
			// (set) Token: 0x060035CE RID: 13774 RVA: 0x0005DA84 File Offset: 0x0005BC84
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17001B13 RID: 6931
			// (set) Token: 0x060035CF RID: 13775 RVA: 0x0005DA9C File Offset: 0x0005BC9C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleGroupIdParameter(value) : null);
				}
			}

			// Token: 0x17001B14 RID: 6932
			// (set) Token: 0x060035D0 RID: 13776 RVA: 0x0005DABA File Offset: 0x0005BCBA
			public virtual SwitchParameter ShowPartnerLinked
			{
				set
				{
					base.PowerSharpParameters["ShowPartnerLinked"] = value;
				}
			}

			// Token: 0x17001B15 RID: 6933
			// (set) Token: 0x060035D1 RID: 13777 RVA: 0x0005DAD2 File Offset: 0x0005BCD2
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x17001B16 RID: 6934
			// (set) Token: 0x060035D2 RID: 13778 RVA: 0x0005DAEA File Offset: 0x0005BCEA
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17001B17 RID: 6935
			// (set) Token: 0x060035D3 RID: 13779 RVA: 0x0005DAFD File Offset: 0x0005BCFD
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001B18 RID: 6936
			// (set) Token: 0x060035D4 RID: 13780 RVA: 0x0005DB1B File Offset: 0x0005BD1B
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17001B19 RID: 6937
			// (set) Token: 0x060035D5 RID: 13781 RVA: 0x0005DB2E File Offset: 0x0005BD2E
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17001B1A RID: 6938
			// (set) Token: 0x060035D6 RID: 13782 RVA: 0x0005DB41 File Offset: 0x0005BD41
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001B1B RID: 6939
			// (set) Token: 0x060035D7 RID: 13783 RVA: 0x0005DB59 File Offset: 0x0005BD59
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001B1C RID: 6940
			// (set) Token: 0x060035D8 RID: 13784 RVA: 0x0005DB6C File Offset: 0x0005BD6C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001B1D RID: 6941
			// (set) Token: 0x060035D9 RID: 13785 RVA: 0x0005DB84 File Offset: 0x0005BD84
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001B1E RID: 6942
			// (set) Token: 0x060035DA RID: 13786 RVA: 0x0005DB9C File Offset: 0x0005BD9C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001B1F RID: 6943
			// (set) Token: 0x060035DB RID: 13787 RVA: 0x0005DBB4 File Offset: 0x0005BDB4
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
