using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000571 RID: 1393
	public class SetDatabaseAvailabilityGroupCommand : SyntheticCommandWithPipelineInputNoOutput<DatabaseAvailabilityGroup>
	{
		// Token: 0x06004943 RID: 18755 RVA: 0x00076660 File Offset: 0x00074860
		private SetDatabaseAvailabilityGroupCommand() : base("Set-DatabaseAvailabilityGroup")
		{
		}

		// Token: 0x06004944 RID: 18756 RVA: 0x0007666D File Offset: 0x0007486D
		public SetDatabaseAvailabilityGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004945 RID: 18757 RVA: 0x0007667C File Offset: 0x0007487C
		public virtual SetDatabaseAvailabilityGroupCommand SetParameters(SetDatabaseAvailabilityGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004946 RID: 18758 RVA: 0x00076686 File Offset: 0x00074886
		public virtual SetDatabaseAvailabilityGroupCommand SetParameters(SetDatabaseAvailabilityGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000572 RID: 1394
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002A1A RID: 10778
			// (set) Token: 0x06004947 RID: 18759 RVA: 0x00076690 File Offset: 0x00074890
			public virtual FileShareWitnessServerName WitnessServer
			{
				set
				{
					base.PowerSharpParameters["WitnessServer"] = value;
				}
			}

			// Token: 0x17002A1B RID: 10779
			// (set) Token: 0x06004948 RID: 18760 RVA: 0x000766A3 File Offset: 0x000748A3
			public virtual ushort ReplicationPort
			{
				set
				{
					base.PowerSharpParameters["ReplicationPort"] = value;
				}
			}

			// Token: 0x17002A1C RID: 10780
			// (set) Token: 0x06004949 RID: 18761 RVA: 0x000766BB File Offset: 0x000748BB
			public virtual DatabaseAvailabilityGroup.NetworkOption NetworkCompression
			{
				set
				{
					base.PowerSharpParameters["NetworkCompression"] = value;
				}
			}

			// Token: 0x17002A1D RID: 10781
			// (set) Token: 0x0600494A RID: 18762 RVA: 0x000766D3 File Offset: 0x000748D3
			public virtual DatabaseAvailabilityGroup.NetworkOption NetworkEncryption
			{
				set
				{
					base.PowerSharpParameters["NetworkEncryption"] = value;
				}
			}

			// Token: 0x17002A1E RID: 10782
			// (set) Token: 0x0600494B RID: 18763 RVA: 0x000766EB File Offset: 0x000748EB
			public virtual bool ManualDagNetworkConfiguration
			{
				set
				{
					base.PowerSharpParameters["ManualDagNetworkConfiguration"] = value;
				}
			}

			// Token: 0x17002A1F RID: 10783
			// (set) Token: 0x0600494C RID: 18764 RVA: 0x00076703 File Offset: 0x00074903
			public virtual SwitchParameter DiscoverNetworks
			{
				set
				{
					base.PowerSharpParameters["DiscoverNetworks"] = value;
				}
			}

			// Token: 0x17002A20 RID: 10784
			// (set) Token: 0x0600494D RID: 18765 RVA: 0x0007671B File Offset: 0x0007491B
			public virtual SwitchParameter AllowCrossSiteRpcClientAccess
			{
				set
				{
					base.PowerSharpParameters["AllowCrossSiteRpcClientAccess"] = value;
				}
			}

			// Token: 0x17002A21 RID: 10785
			// (set) Token: 0x0600494E RID: 18766 RVA: 0x00076733 File Offset: 0x00074933
			public virtual NonRootLocalLongFullPath WitnessDirectory
			{
				set
				{
					base.PowerSharpParameters["WitnessDirectory"] = value;
				}
			}

			// Token: 0x17002A22 RID: 10786
			// (set) Token: 0x0600494F RID: 18767 RVA: 0x00076746 File Offset: 0x00074946
			public virtual FileShareWitnessServerName AlternateWitnessServer
			{
				set
				{
					base.PowerSharpParameters["AlternateWitnessServer"] = value;
				}
			}

			// Token: 0x17002A23 RID: 10787
			// (set) Token: 0x06004950 RID: 18768 RVA: 0x00076759 File Offset: 0x00074959
			public virtual NonRootLocalLongFullPath AlternateWitnessDirectory
			{
				set
				{
					base.PowerSharpParameters["AlternateWitnessDirectory"] = value;
				}
			}

			// Token: 0x17002A24 RID: 10788
			// (set) Token: 0x06004951 RID: 18769 RVA: 0x0007676C File Offset: 0x0007496C
			public virtual DatacenterActivationModeOption DatacenterActivationMode
			{
				set
				{
					base.PowerSharpParameters["DatacenterActivationMode"] = value;
				}
			}

			// Token: 0x17002A25 RID: 10789
			// (set) Token: 0x06004952 RID: 18770 RVA: 0x00076784 File Offset: 0x00074984
			public virtual IPAddress DatabaseAvailabilityGroupIpAddresses
			{
				set
				{
					base.PowerSharpParameters["DatabaseAvailabilityGroupIpAddresses"] = value;
				}
			}

			// Token: 0x17002A26 RID: 10790
			// (set) Token: 0x06004953 RID: 18771 RVA: 0x00076797 File Offset: 0x00074997
			public virtual DatabaseAvailabilityGroupConfigurationIdParameter DagConfiguration
			{
				set
				{
					base.PowerSharpParameters["DagConfiguration"] = value;
				}
			}

			// Token: 0x17002A27 RID: 10791
			// (set) Token: 0x06004954 RID: 18772 RVA: 0x000767AA File Offset: 0x000749AA
			public virtual SwitchParameter SkipDagValidation
			{
				set
				{
					base.PowerSharpParameters["SkipDagValidation"] = value;
				}
			}

			// Token: 0x17002A28 RID: 10792
			// (set) Token: 0x06004955 RID: 18773 RVA: 0x000767C2 File Offset: 0x000749C2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002A29 RID: 10793
			// (set) Token: 0x06004956 RID: 18774 RVA: 0x000767D5 File Offset: 0x000749D5
			public virtual int AutoDagDatabaseCopiesPerDatabase
			{
				set
				{
					base.PowerSharpParameters["AutoDagDatabaseCopiesPerDatabase"] = value;
				}
			}

			// Token: 0x17002A2A RID: 10794
			// (set) Token: 0x06004957 RID: 18775 RVA: 0x000767ED File Offset: 0x000749ED
			public virtual int AutoDagDatabaseCopiesPerVolume
			{
				set
				{
					base.PowerSharpParameters["AutoDagDatabaseCopiesPerVolume"] = value;
				}
			}

			// Token: 0x17002A2B RID: 10795
			// (set) Token: 0x06004958 RID: 18776 RVA: 0x00076805 File Offset: 0x00074A05
			public virtual int AutoDagTotalNumberOfDatabases
			{
				set
				{
					base.PowerSharpParameters["AutoDagTotalNumberOfDatabases"] = value;
				}
			}

			// Token: 0x17002A2C RID: 10796
			// (set) Token: 0x06004959 RID: 18777 RVA: 0x0007681D File Offset: 0x00074A1D
			public virtual int AutoDagTotalNumberOfServers
			{
				set
				{
					base.PowerSharpParameters["AutoDagTotalNumberOfServers"] = value;
				}
			}

			// Token: 0x17002A2D RID: 10797
			// (set) Token: 0x0600495A RID: 18778 RVA: 0x00076835 File Offset: 0x00074A35
			public virtual NonRootLocalLongFullPath AutoDagDatabasesRootFolderPath
			{
				set
				{
					base.PowerSharpParameters["AutoDagDatabasesRootFolderPath"] = value;
				}
			}

			// Token: 0x17002A2E RID: 10798
			// (set) Token: 0x0600495B RID: 18779 RVA: 0x00076848 File Offset: 0x00074A48
			public virtual NonRootLocalLongFullPath AutoDagVolumesRootFolderPath
			{
				set
				{
					base.PowerSharpParameters["AutoDagVolumesRootFolderPath"] = value;
				}
			}

			// Token: 0x17002A2F RID: 10799
			// (set) Token: 0x0600495C RID: 18780 RVA: 0x0007685B File Offset: 0x00074A5B
			public virtual bool AutoDagAllServersInstalled
			{
				set
				{
					base.PowerSharpParameters["AutoDagAllServersInstalled"] = value;
				}
			}

			// Token: 0x17002A30 RID: 10800
			// (set) Token: 0x0600495D RID: 18781 RVA: 0x00076873 File Offset: 0x00074A73
			public virtual bool AutoDagAutoReseedEnabled
			{
				set
				{
					base.PowerSharpParameters["AutoDagAutoReseedEnabled"] = value;
				}
			}

			// Token: 0x17002A31 RID: 10801
			// (set) Token: 0x0600495E RID: 18782 RVA: 0x0007688B File Offset: 0x00074A8B
			public virtual bool AutoDagDiskReclaimerEnabled
			{
				set
				{
					base.PowerSharpParameters["AutoDagDiskReclaimerEnabled"] = value;
				}
			}

			// Token: 0x17002A32 RID: 10802
			// (set) Token: 0x0600495F RID: 18783 RVA: 0x000768A3 File Offset: 0x00074AA3
			public virtual bool AutoDagBitlockerEnabled
			{
				set
				{
					base.PowerSharpParameters["AutoDagBitlockerEnabled"] = value;
				}
			}

			// Token: 0x17002A33 RID: 10803
			// (set) Token: 0x06004960 RID: 18784 RVA: 0x000768BB File Offset: 0x00074ABB
			public virtual bool AutoDagFIPSCompliant
			{
				set
				{
					base.PowerSharpParameters["AutoDagFIPSCompliant"] = value;
				}
			}

			// Token: 0x17002A34 RID: 10804
			// (set) Token: 0x06004961 RID: 18785 RVA: 0x000768D3 File Offset: 0x00074AD3
			public virtual bool ReplayLagManagerEnabled
			{
				set
				{
					base.PowerSharpParameters["ReplayLagManagerEnabled"] = value;
				}
			}

			// Token: 0x17002A35 RID: 10805
			// (set) Token: 0x06004962 RID: 18786 RVA: 0x000768EB File Offset: 0x00074AEB
			public virtual ByteQuantifiedSize? MailboxLoadBalanceMaximumEdbFileSize
			{
				set
				{
					base.PowerSharpParameters["MailboxLoadBalanceMaximumEdbFileSize"] = value;
				}
			}

			// Token: 0x17002A36 RID: 10806
			// (set) Token: 0x06004963 RID: 18787 RVA: 0x00076903 File Offset: 0x00074B03
			public virtual int? MailboxLoadBalanceRelativeLoadCapacity
			{
				set
				{
					base.PowerSharpParameters["MailboxLoadBalanceRelativeLoadCapacity"] = value;
				}
			}

			// Token: 0x17002A37 RID: 10807
			// (set) Token: 0x06004964 RID: 18788 RVA: 0x0007691B File Offset: 0x00074B1B
			public virtual int? MailboxLoadBalanceOverloadedThreshold
			{
				set
				{
					base.PowerSharpParameters["MailboxLoadBalanceOverloadedThreshold"] = value;
				}
			}

			// Token: 0x17002A38 RID: 10808
			// (set) Token: 0x06004965 RID: 18789 RVA: 0x00076933 File Offset: 0x00074B33
			public virtual int? MailboxLoadBalanceUnderloadedThreshold
			{
				set
				{
					base.PowerSharpParameters["MailboxLoadBalanceUnderloadedThreshold"] = value;
				}
			}

			// Token: 0x17002A39 RID: 10809
			// (set) Token: 0x06004966 RID: 18790 RVA: 0x0007694B File Offset: 0x00074B4B
			public virtual bool MailboxLoadBalanceEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxLoadBalanceEnabled"] = value;
				}
			}

			// Token: 0x17002A3A RID: 10810
			// (set) Token: 0x06004967 RID: 18791 RVA: 0x00076963 File Offset: 0x00074B63
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002A3B RID: 10811
			// (set) Token: 0x06004968 RID: 18792 RVA: 0x0007697B File Offset: 0x00074B7B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002A3C RID: 10812
			// (set) Token: 0x06004969 RID: 18793 RVA: 0x00076993 File Offset: 0x00074B93
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002A3D RID: 10813
			// (set) Token: 0x0600496A RID: 18794 RVA: 0x000769AB File Offset: 0x00074BAB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002A3E RID: 10814
			// (set) Token: 0x0600496B RID: 18795 RVA: 0x000769C3 File Offset: 0x00074BC3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000573 RID: 1395
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002A3F RID: 10815
			// (set) Token: 0x0600496D RID: 18797 RVA: 0x000769E3 File Offset: 0x00074BE3
			public virtual DatabaseAvailabilityGroupIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002A40 RID: 10816
			// (set) Token: 0x0600496E RID: 18798 RVA: 0x000769F6 File Offset: 0x00074BF6
			public virtual FileShareWitnessServerName WitnessServer
			{
				set
				{
					base.PowerSharpParameters["WitnessServer"] = value;
				}
			}

			// Token: 0x17002A41 RID: 10817
			// (set) Token: 0x0600496F RID: 18799 RVA: 0x00076A09 File Offset: 0x00074C09
			public virtual ushort ReplicationPort
			{
				set
				{
					base.PowerSharpParameters["ReplicationPort"] = value;
				}
			}

			// Token: 0x17002A42 RID: 10818
			// (set) Token: 0x06004970 RID: 18800 RVA: 0x00076A21 File Offset: 0x00074C21
			public virtual DatabaseAvailabilityGroup.NetworkOption NetworkCompression
			{
				set
				{
					base.PowerSharpParameters["NetworkCompression"] = value;
				}
			}

			// Token: 0x17002A43 RID: 10819
			// (set) Token: 0x06004971 RID: 18801 RVA: 0x00076A39 File Offset: 0x00074C39
			public virtual DatabaseAvailabilityGroup.NetworkOption NetworkEncryption
			{
				set
				{
					base.PowerSharpParameters["NetworkEncryption"] = value;
				}
			}

			// Token: 0x17002A44 RID: 10820
			// (set) Token: 0x06004972 RID: 18802 RVA: 0x00076A51 File Offset: 0x00074C51
			public virtual bool ManualDagNetworkConfiguration
			{
				set
				{
					base.PowerSharpParameters["ManualDagNetworkConfiguration"] = value;
				}
			}

			// Token: 0x17002A45 RID: 10821
			// (set) Token: 0x06004973 RID: 18803 RVA: 0x00076A69 File Offset: 0x00074C69
			public virtual SwitchParameter DiscoverNetworks
			{
				set
				{
					base.PowerSharpParameters["DiscoverNetworks"] = value;
				}
			}

			// Token: 0x17002A46 RID: 10822
			// (set) Token: 0x06004974 RID: 18804 RVA: 0x00076A81 File Offset: 0x00074C81
			public virtual SwitchParameter AllowCrossSiteRpcClientAccess
			{
				set
				{
					base.PowerSharpParameters["AllowCrossSiteRpcClientAccess"] = value;
				}
			}

			// Token: 0x17002A47 RID: 10823
			// (set) Token: 0x06004975 RID: 18805 RVA: 0x00076A99 File Offset: 0x00074C99
			public virtual NonRootLocalLongFullPath WitnessDirectory
			{
				set
				{
					base.PowerSharpParameters["WitnessDirectory"] = value;
				}
			}

			// Token: 0x17002A48 RID: 10824
			// (set) Token: 0x06004976 RID: 18806 RVA: 0x00076AAC File Offset: 0x00074CAC
			public virtual FileShareWitnessServerName AlternateWitnessServer
			{
				set
				{
					base.PowerSharpParameters["AlternateWitnessServer"] = value;
				}
			}

			// Token: 0x17002A49 RID: 10825
			// (set) Token: 0x06004977 RID: 18807 RVA: 0x00076ABF File Offset: 0x00074CBF
			public virtual NonRootLocalLongFullPath AlternateWitnessDirectory
			{
				set
				{
					base.PowerSharpParameters["AlternateWitnessDirectory"] = value;
				}
			}

			// Token: 0x17002A4A RID: 10826
			// (set) Token: 0x06004978 RID: 18808 RVA: 0x00076AD2 File Offset: 0x00074CD2
			public virtual DatacenterActivationModeOption DatacenterActivationMode
			{
				set
				{
					base.PowerSharpParameters["DatacenterActivationMode"] = value;
				}
			}

			// Token: 0x17002A4B RID: 10827
			// (set) Token: 0x06004979 RID: 18809 RVA: 0x00076AEA File Offset: 0x00074CEA
			public virtual IPAddress DatabaseAvailabilityGroupIpAddresses
			{
				set
				{
					base.PowerSharpParameters["DatabaseAvailabilityGroupIpAddresses"] = value;
				}
			}

			// Token: 0x17002A4C RID: 10828
			// (set) Token: 0x0600497A RID: 18810 RVA: 0x00076AFD File Offset: 0x00074CFD
			public virtual DatabaseAvailabilityGroupConfigurationIdParameter DagConfiguration
			{
				set
				{
					base.PowerSharpParameters["DagConfiguration"] = value;
				}
			}

			// Token: 0x17002A4D RID: 10829
			// (set) Token: 0x0600497B RID: 18811 RVA: 0x00076B10 File Offset: 0x00074D10
			public virtual SwitchParameter SkipDagValidation
			{
				set
				{
					base.PowerSharpParameters["SkipDagValidation"] = value;
				}
			}

			// Token: 0x17002A4E RID: 10830
			// (set) Token: 0x0600497C RID: 18812 RVA: 0x00076B28 File Offset: 0x00074D28
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002A4F RID: 10831
			// (set) Token: 0x0600497D RID: 18813 RVA: 0x00076B3B File Offset: 0x00074D3B
			public virtual int AutoDagDatabaseCopiesPerDatabase
			{
				set
				{
					base.PowerSharpParameters["AutoDagDatabaseCopiesPerDatabase"] = value;
				}
			}

			// Token: 0x17002A50 RID: 10832
			// (set) Token: 0x0600497E RID: 18814 RVA: 0x00076B53 File Offset: 0x00074D53
			public virtual int AutoDagDatabaseCopiesPerVolume
			{
				set
				{
					base.PowerSharpParameters["AutoDagDatabaseCopiesPerVolume"] = value;
				}
			}

			// Token: 0x17002A51 RID: 10833
			// (set) Token: 0x0600497F RID: 18815 RVA: 0x00076B6B File Offset: 0x00074D6B
			public virtual int AutoDagTotalNumberOfDatabases
			{
				set
				{
					base.PowerSharpParameters["AutoDagTotalNumberOfDatabases"] = value;
				}
			}

			// Token: 0x17002A52 RID: 10834
			// (set) Token: 0x06004980 RID: 18816 RVA: 0x00076B83 File Offset: 0x00074D83
			public virtual int AutoDagTotalNumberOfServers
			{
				set
				{
					base.PowerSharpParameters["AutoDagTotalNumberOfServers"] = value;
				}
			}

			// Token: 0x17002A53 RID: 10835
			// (set) Token: 0x06004981 RID: 18817 RVA: 0x00076B9B File Offset: 0x00074D9B
			public virtual NonRootLocalLongFullPath AutoDagDatabasesRootFolderPath
			{
				set
				{
					base.PowerSharpParameters["AutoDagDatabasesRootFolderPath"] = value;
				}
			}

			// Token: 0x17002A54 RID: 10836
			// (set) Token: 0x06004982 RID: 18818 RVA: 0x00076BAE File Offset: 0x00074DAE
			public virtual NonRootLocalLongFullPath AutoDagVolumesRootFolderPath
			{
				set
				{
					base.PowerSharpParameters["AutoDagVolumesRootFolderPath"] = value;
				}
			}

			// Token: 0x17002A55 RID: 10837
			// (set) Token: 0x06004983 RID: 18819 RVA: 0x00076BC1 File Offset: 0x00074DC1
			public virtual bool AutoDagAllServersInstalled
			{
				set
				{
					base.PowerSharpParameters["AutoDagAllServersInstalled"] = value;
				}
			}

			// Token: 0x17002A56 RID: 10838
			// (set) Token: 0x06004984 RID: 18820 RVA: 0x00076BD9 File Offset: 0x00074DD9
			public virtual bool AutoDagAutoReseedEnabled
			{
				set
				{
					base.PowerSharpParameters["AutoDagAutoReseedEnabled"] = value;
				}
			}

			// Token: 0x17002A57 RID: 10839
			// (set) Token: 0x06004985 RID: 18821 RVA: 0x00076BF1 File Offset: 0x00074DF1
			public virtual bool AutoDagDiskReclaimerEnabled
			{
				set
				{
					base.PowerSharpParameters["AutoDagDiskReclaimerEnabled"] = value;
				}
			}

			// Token: 0x17002A58 RID: 10840
			// (set) Token: 0x06004986 RID: 18822 RVA: 0x00076C09 File Offset: 0x00074E09
			public virtual bool AutoDagBitlockerEnabled
			{
				set
				{
					base.PowerSharpParameters["AutoDagBitlockerEnabled"] = value;
				}
			}

			// Token: 0x17002A59 RID: 10841
			// (set) Token: 0x06004987 RID: 18823 RVA: 0x00076C21 File Offset: 0x00074E21
			public virtual bool AutoDagFIPSCompliant
			{
				set
				{
					base.PowerSharpParameters["AutoDagFIPSCompliant"] = value;
				}
			}

			// Token: 0x17002A5A RID: 10842
			// (set) Token: 0x06004988 RID: 18824 RVA: 0x00076C39 File Offset: 0x00074E39
			public virtual bool ReplayLagManagerEnabled
			{
				set
				{
					base.PowerSharpParameters["ReplayLagManagerEnabled"] = value;
				}
			}

			// Token: 0x17002A5B RID: 10843
			// (set) Token: 0x06004989 RID: 18825 RVA: 0x00076C51 File Offset: 0x00074E51
			public virtual ByteQuantifiedSize? MailboxLoadBalanceMaximumEdbFileSize
			{
				set
				{
					base.PowerSharpParameters["MailboxLoadBalanceMaximumEdbFileSize"] = value;
				}
			}

			// Token: 0x17002A5C RID: 10844
			// (set) Token: 0x0600498A RID: 18826 RVA: 0x00076C69 File Offset: 0x00074E69
			public virtual int? MailboxLoadBalanceRelativeLoadCapacity
			{
				set
				{
					base.PowerSharpParameters["MailboxLoadBalanceRelativeLoadCapacity"] = value;
				}
			}

			// Token: 0x17002A5D RID: 10845
			// (set) Token: 0x0600498B RID: 18827 RVA: 0x00076C81 File Offset: 0x00074E81
			public virtual int? MailboxLoadBalanceOverloadedThreshold
			{
				set
				{
					base.PowerSharpParameters["MailboxLoadBalanceOverloadedThreshold"] = value;
				}
			}

			// Token: 0x17002A5E RID: 10846
			// (set) Token: 0x0600498C RID: 18828 RVA: 0x00076C99 File Offset: 0x00074E99
			public virtual int? MailboxLoadBalanceUnderloadedThreshold
			{
				set
				{
					base.PowerSharpParameters["MailboxLoadBalanceUnderloadedThreshold"] = value;
				}
			}

			// Token: 0x17002A5F RID: 10847
			// (set) Token: 0x0600498D RID: 18829 RVA: 0x00076CB1 File Offset: 0x00074EB1
			public virtual bool MailboxLoadBalanceEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxLoadBalanceEnabled"] = value;
				}
			}

			// Token: 0x17002A60 RID: 10848
			// (set) Token: 0x0600498E RID: 18830 RVA: 0x00076CC9 File Offset: 0x00074EC9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002A61 RID: 10849
			// (set) Token: 0x0600498F RID: 18831 RVA: 0x00076CE1 File Offset: 0x00074EE1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002A62 RID: 10850
			// (set) Token: 0x06004990 RID: 18832 RVA: 0x00076CF9 File Offset: 0x00074EF9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002A63 RID: 10851
			// (set) Token: 0x06004991 RID: 18833 RVA: 0x00076D11 File Offset: 0x00074F11
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002A64 RID: 10852
			// (set) Token: 0x06004992 RID: 18834 RVA: 0x00076D29 File Offset: 0x00074F29
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
