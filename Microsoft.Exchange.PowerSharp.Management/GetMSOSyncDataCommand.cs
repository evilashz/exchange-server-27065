using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200006C RID: 108
	public class GetMSOSyncDataCommand : SyntheticCommandWithPipelineInput<ADRawEntry, ADRawEntry>
	{
		// Token: 0x060017E3 RID: 6115 RVA: 0x00036B15 File Offset: 0x00034D15
		private GetMSOSyncDataCommand() : base("Get-MSOSyncData")
		{
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00036B22 File Offset: 0x00034D22
		public GetMSOSyncDataCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00036B31 File Offset: 0x00034D31
		public virtual GetMSOSyncDataCommand SetParameters(GetMSOSyncDataCommand.ObjectFullSyncInitialCallParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x00036B3B File Offset: 0x00034D3B
		public virtual GetMSOSyncDataCommand SetParameters(GetMSOSyncDataCommand.TenantFullSyncInitialCallParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x00036B45 File Offset: 0x00034D45
		public virtual GetMSOSyncDataCommand SetParameters(GetMSOSyncDataCommand.IncrementalSyncParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x00036B4F File Offset: 0x00034D4F
		public virtual GetMSOSyncDataCommand SetParameters(GetMSOSyncDataCommand.ObjectFullSyncInitialCallFromMergeSyncParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00036B59 File Offset: 0x00034D59
		public virtual GetMSOSyncDataCommand SetParameters(GetMSOSyncDataCommand.ObjectFullSyncInitialCallFromTenantFullSyncParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00036B63 File Offset: 0x00034D63
		public virtual GetMSOSyncDataCommand SetParameters(GetMSOSyncDataCommand.ObjectFullSyncSubsequentCallParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00036B6D File Offset: 0x00034D6D
		public virtual GetMSOSyncDataCommand SetParameters(GetMSOSyncDataCommand.TenantFullSyncSubsequentCallParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00036B77 File Offset: 0x00034D77
		public virtual GetMSOSyncDataCommand SetParameters(GetMSOSyncDataCommand.MergeInitialCallParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00036B81 File Offset: 0x00034D81
		public virtual GetMSOSyncDataCommand SetParameters(GetMSOSyncDataCommand.MergeSubsequentCallParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00036B8B File Offset: 0x00034D8B
		public virtual GetMSOSyncDataCommand SetParameters(GetMSOSyncDataCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200006D RID: 109
		public class ObjectFullSyncInitialCallParameterSetParameters : ParametersBase
		{
			// Token: 0x170002C4 RID: 708
			// (set) Token: 0x060017EF RID: 6127 RVA: 0x00036B95 File Offset: 0x00034D95
			public virtual byte Cookie
			{
				set
				{
					base.PowerSharpParameters["Cookie"] = value;
				}
			}

			// Token: 0x170002C5 RID: 709
			// (set) Token: 0x060017F0 RID: 6128 RVA: 0x00036BAD File Offset: 0x00034DAD
			public virtual SyncObjectId ObjectIds
			{
				set
				{
					base.PowerSharpParameters["ObjectIds"] = value;
				}
			}

			// Token: 0x170002C6 RID: 710
			// (set) Token: 0x060017F1 RID: 6129 RVA: 0x00036BC0 File Offset: 0x00034DC0
			public virtual BackSyncOptions SyncOptions
			{
				set
				{
					base.PowerSharpParameters["SyncOptions"] = value;
				}
			}

			// Token: 0x170002C7 RID: 711
			// (set) Token: 0x060017F2 RID: 6130 RVA: 0x00036BD8 File Offset: 0x00034DD8
			public virtual ServiceInstanceId ServiceInstance
			{
				set
				{
					base.PowerSharpParameters["ServiceInstance"] = value;
				}
			}

			// Token: 0x170002C8 RID: 712
			// (set) Token: 0x060017F3 RID: 6131 RVA: 0x00036BEB File Offset: 0x00034DEB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170002C9 RID: 713
			// (set) Token: 0x060017F4 RID: 6132 RVA: 0x00036C03 File Offset: 0x00034E03
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170002CA RID: 714
			// (set) Token: 0x060017F5 RID: 6133 RVA: 0x00036C1B File Offset: 0x00034E1B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170002CB RID: 715
			// (set) Token: 0x060017F6 RID: 6134 RVA: 0x00036C33 File Offset: 0x00034E33
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200006E RID: 110
		public class TenantFullSyncInitialCallParameterSetParameters : ParametersBase
		{
			// Token: 0x170002CC RID: 716
			// (set) Token: 0x060017F8 RID: 6136 RVA: 0x00036C53 File Offset: 0x00034E53
			public virtual byte Cookie
			{
				set
				{
					base.PowerSharpParameters["Cookie"] = value;
				}
			}

			// Token: 0x170002CD RID: 717
			// (set) Token: 0x060017F9 RID: 6137 RVA: 0x00036C6B File Offset: 0x00034E6B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170002CE RID: 718
			// (set) Token: 0x060017FA RID: 6138 RVA: 0x00036C89 File Offset: 0x00034E89
			public virtual ServiceInstanceId ServiceInstance
			{
				set
				{
					base.PowerSharpParameters["ServiceInstance"] = value;
				}
			}

			// Token: 0x170002CF RID: 719
			// (set) Token: 0x060017FB RID: 6139 RVA: 0x00036C9C File Offset: 0x00034E9C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170002D0 RID: 720
			// (set) Token: 0x060017FC RID: 6140 RVA: 0x00036CB4 File Offset: 0x00034EB4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170002D1 RID: 721
			// (set) Token: 0x060017FD RID: 6141 RVA: 0x00036CCC File Offset: 0x00034ECC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170002D2 RID: 722
			// (set) Token: 0x060017FE RID: 6142 RVA: 0x00036CE4 File Offset: 0x00034EE4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200006F RID: 111
		public class IncrementalSyncParameterSetParameters : ParametersBase
		{
			// Token: 0x170002D3 RID: 723
			// (set) Token: 0x06001800 RID: 6144 RVA: 0x00036D04 File Offset: 0x00034F04
			public virtual byte Cookie
			{
				set
				{
					base.PowerSharpParameters["Cookie"] = value;
				}
			}

			// Token: 0x170002D4 RID: 724
			// (set) Token: 0x06001801 RID: 6145 RVA: 0x00036D1C File Offset: 0x00034F1C
			public virtual ServiceInstanceId ServiceInstance
			{
				set
				{
					base.PowerSharpParameters["ServiceInstance"] = value;
				}
			}

			// Token: 0x170002D5 RID: 725
			// (set) Token: 0x06001802 RID: 6146 RVA: 0x00036D2F File Offset: 0x00034F2F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170002D6 RID: 726
			// (set) Token: 0x06001803 RID: 6147 RVA: 0x00036D47 File Offset: 0x00034F47
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170002D7 RID: 727
			// (set) Token: 0x06001804 RID: 6148 RVA: 0x00036D5F File Offset: 0x00034F5F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170002D8 RID: 728
			// (set) Token: 0x06001805 RID: 6149 RVA: 0x00036D77 File Offset: 0x00034F77
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000070 RID: 112
		public class ObjectFullSyncInitialCallFromMergeSyncParameterSetParameters : ParametersBase
		{
			// Token: 0x170002D9 RID: 729
			// (set) Token: 0x06001807 RID: 6151 RVA: 0x00036D97 File Offset: 0x00034F97
			public virtual SyncObjectId ObjectIds
			{
				set
				{
					base.PowerSharpParameters["ObjectIds"] = value;
				}
			}

			// Token: 0x170002DA RID: 730
			// (set) Token: 0x06001808 RID: 6152 RVA: 0x00036DAA File Offset: 0x00034FAA
			public virtual BackSyncOptions SyncOptions
			{
				set
				{
					base.PowerSharpParameters["SyncOptions"] = value;
				}
			}

			// Token: 0x170002DB RID: 731
			// (set) Token: 0x06001809 RID: 6153 RVA: 0x00036DC2 File Offset: 0x00034FC2
			public virtual byte MergePageTokenContext
			{
				set
				{
					base.PowerSharpParameters["MergePageTokenContext"] = value;
				}
			}

			// Token: 0x170002DC RID: 732
			// (set) Token: 0x0600180A RID: 6154 RVA: 0x00036DDA File Offset: 0x00034FDA
			public virtual ServiceInstanceId ServiceInstance
			{
				set
				{
					base.PowerSharpParameters["ServiceInstance"] = value;
				}
			}

			// Token: 0x170002DD RID: 733
			// (set) Token: 0x0600180B RID: 6155 RVA: 0x00036DED File Offset: 0x00034FED
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170002DE RID: 734
			// (set) Token: 0x0600180C RID: 6156 RVA: 0x00036E05 File Offset: 0x00035005
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170002DF RID: 735
			// (set) Token: 0x0600180D RID: 6157 RVA: 0x00036E1D File Offset: 0x0003501D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170002E0 RID: 736
			// (set) Token: 0x0600180E RID: 6158 RVA: 0x00036E35 File Offset: 0x00035035
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000071 RID: 113
		public class ObjectFullSyncInitialCallFromTenantFullSyncParameterSetParameters : ParametersBase
		{
			// Token: 0x170002E1 RID: 737
			// (set) Token: 0x06001810 RID: 6160 RVA: 0x00036E55 File Offset: 0x00035055
			public virtual SyncObjectId ObjectIds
			{
				set
				{
					base.PowerSharpParameters["ObjectIds"] = value;
				}
			}

			// Token: 0x170002E2 RID: 738
			// (set) Token: 0x06001811 RID: 6161 RVA: 0x00036E68 File Offset: 0x00035068
			public virtual BackSyncOptions SyncOptions
			{
				set
				{
					base.PowerSharpParameters["SyncOptions"] = value;
				}
			}

			// Token: 0x170002E3 RID: 739
			// (set) Token: 0x06001812 RID: 6162 RVA: 0x00036E80 File Offset: 0x00035080
			public virtual byte TenantFullSyncPageTokenContext
			{
				set
				{
					base.PowerSharpParameters["TenantFullSyncPageTokenContext"] = value;
				}
			}

			// Token: 0x170002E4 RID: 740
			// (set) Token: 0x06001813 RID: 6163 RVA: 0x00036E98 File Offset: 0x00035098
			public virtual ServiceInstanceId ServiceInstance
			{
				set
				{
					base.PowerSharpParameters["ServiceInstance"] = value;
				}
			}

			// Token: 0x170002E5 RID: 741
			// (set) Token: 0x06001814 RID: 6164 RVA: 0x00036EAB File Offset: 0x000350AB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170002E6 RID: 742
			// (set) Token: 0x06001815 RID: 6165 RVA: 0x00036EC3 File Offset: 0x000350C3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170002E7 RID: 743
			// (set) Token: 0x06001816 RID: 6166 RVA: 0x00036EDB File Offset: 0x000350DB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170002E8 RID: 744
			// (set) Token: 0x06001817 RID: 6167 RVA: 0x00036EF3 File Offset: 0x000350F3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000072 RID: 114
		public class ObjectFullSyncSubsequentCallParameterSetParameters : ParametersBase
		{
			// Token: 0x170002E9 RID: 745
			// (set) Token: 0x06001819 RID: 6169 RVA: 0x00036F13 File Offset: 0x00035113
			public virtual byte ObjectFullSyncPageToken
			{
				set
				{
					base.PowerSharpParameters["ObjectFullSyncPageToken"] = value;
				}
			}

			// Token: 0x170002EA RID: 746
			// (set) Token: 0x0600181A RID: 6170 RVA: 0x00036F2B File Offset: 0x0003512B
			public virtual ServiceInstanceId ServiceInstance
			{
				set
				{
					base.PowerSharpParameters["ServiceInstance"] = value;
				}
			}

			// Token: 0x170002EB RID: 747
			// (set) Token: 0x0600181B RID: 6171 RVA: 0x00036F3E File Offset: 0x0003513E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170002EC RID: 748
			// (set) Token: 0x0600181C RID: 6172 RVA: 0x00036F56 File Offset: 0x00035156
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170002ED RID: 749
			// (set) Token: 0x0600181D RID: 6173 RVA: 0x00036F6E File Offset: 0x0003516E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170002EE RID: 750
			// (set) Token: 0x0600181E RID: 6174 RVA: 0x00036F86 File Offset: 0x00035186
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000073 RID: 115
		public class TenantFullSyncSubsequentCallParameterSetParameters : ParametersBase
		{
			// Token: 0x170002EF RID: 751
			// (set) Token: 0x06001820 RID: 6176 RVA: 0x00036FA6 File Offset: 0x000351A6
			public virtual byte TenantFullSyncPageToken
			{
				set
				{
					base.PowerSharpParameters["TenantFullSyncPageToken"] = value;
				}
			}

			// Token: 0x170002F0 RID: 752
			// (set) Token: 0x06001821 RID: 6177 RVA: 0x00036FBE File Offset: 0x000351BE
			public virtual ServiceInstanceId ServiceInstance
			{
				set
				{
					base.PowerSharpParameters["ServiceInstance"] = value;
				}
			}

			// Token: 0x170002F1 RID: 753
			// (set) Token: 0x06001822 RID: 6178 RVA: 0x00036FD1 File Offset: 0x000351D1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170002F2 RID: 754
			// (set) Token: 0x06001823 RID: 6179 RVA: 0x00036FE9 File Offset: 0x000351E9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170002F3 RID: 755
			// (set) Token: 0x06001824 RID: 6180 RVA: 0x00037001 File Offset: 0x00035201
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170002F4 RID: 756
			// (set) Token: 0x06001825 RID: 6181 RVA: 0x00037019 File Offset: 0x00035219
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000074 RID: 116
		public class MergeInitialCallParameterSetParameters : ParametersBase
		{
			// Token: 0x170002F5 RID: 757
			// (set) Token: 0x06001827 RID: 6183 RVA: 0x00037039 File Offset: 0x00035239
			public virtual byte MergeTenantFullSyncPageToken
			{
				set
				{
					base.PowerSharpParameters["MergeTenantFullSyncPageToken"] = value;
				}
			}

			// Token: 0x170002F6 RID: 758
			// (set) Token: 0x06001828 RID: 6184 RVA: 0x00037051 File Offset: 0x00035251
			public virtual byte MergeIncrementalSyncCookie
			{
				set
				{
					base.PowerSharpParameters["MergeIncrementalSyncCookie"] = value;
				}
			}

			// Token: 0x170002F7 RID: 759
			// (set) Token: 0x06001829 RID: 6185 RVA: 0x00037069 File Offset: 0x00035269
			public virtual ServiceInstanceId ServiceInstance
			{
				set
				{
					base.PowerSharpParameters["ServiceInstance"] = value;
				}
			}

			// Token: 0x170002F8 RID: 760
			// (set) Token: 0x0600182A RID: 6186 RVA: 0x0003707C File Offset: 0x0003527C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170002F9 RID: 761
			// (set) Token: 0x0600182B RID: 6187 RVA: 0x00037094 File Offset: 0x00035294
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170002FA RID: 762
			// (set) Token: 0x0600182C RID: 6188 RVA: 0x000370AC File Offset: 0x000352AC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170002FB RID: 763
			// (set) Token: 0x0600182D RID: 6189 RVA: 0x000370C4 File Offset: 0x000352C4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000075 RID: 117
		public class MergeSubsequentCallParameterSetParameters : ParametersBase
		{
			// Token: 0x170002FC RID: 764
			// (set) Token: 0x0600182F RID: 6191 RVA: 0x000370E4 File Offset: 0x000352E4
			public virtual byte MergePageToken
			{
				set
				{
					base.PowerSharpParameters["MergePageToken"] = value;
				}
			}

			// Token: 0x170002FD RID: 765
			// (set) Token: 0x06001830 RID: 6192 RVA: 0x000370FC File Offset: 0x000352FC
			public virtual ServiceInstanceId ServiceInstance
			{
				set
				{
					base.PowerSharpParameters["ServiceInstance"] = value;
				}
			}

			// Token: 0x170002FE RID: 766
			// (set) Token: 0x06001831 RID: 6193 RVA: 0x0003710F File Offset: 0x0003530F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170002FF RID: 767
			// (set) Token: 0x06001832 RID: 6194 RVA: 0x00037127 File Offset: 0x00035327
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000300 RID: 768
			// (set) Token: 0x06001833 RID: 6195 RVA: 0x0003713F File Offset: 0x0003533F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000301 RID: 769
			// (set) Token: 0x06001834 RID: 6196 RVA: 0x00037157 File Offset: 0x00035357
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000076 RID: 118
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000302 RID: 770
			// (set) Token: 0x06001836 RID: 6198 RVA: 0x00037177 File Offset: 0x00035377
			public virtual ServiceInstanceId ServiceInstance
			{
				set
				{
					base.PowerSharpParameters["ServiceInstance"] = value;
				}
			}

			// Token: 0x17000303 RID: 771
			// (set) Token: 0x06001837 RID: 6199 RVA: 0x0003718A File Offset: 0x0003538A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000304 RID: 772
			// (set) Token: 0x06001838 RID: 6200 RVA: 0x000371A2 File Offset: 0x000353A2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000305 RID: 773
			// (set) Token: 0x06001839 RID: 6201 RVA: 0x000371BA File Offset: 0x000353BA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000306 RID: 774
			// (set) Token: 0x0600183A RID: 6202 RVA: 0x000371D2 File Offset: 0x000353D2
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
