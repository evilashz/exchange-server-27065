using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200099E RID: 2462
	public class UpdateActiveSyncVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADMobileVirtualDirectory, ADMobileVirtualDirectory>
	{
		// Token: 0x06007C06 RID: 31750 RVA: 0x000B8C66 File Offset: 0x000B6E66
		private UpdateActiveSyncVirtualDirectoryCommand() : base("Update-ActiveSyncVirtualDirectory")
		{
		}

		// Token: 0x06007C07 RID: 31751 RVA: 0x000B8C73 File Offset: 0x000B6E73
		public UpdateActiveSyncVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007C08 RID: 31752 RVA: 0x000B8C82 File Offset: 0x000B6E82
		public virtual UpdateActiveSyncVirtualDirectoryCommand SetParameters(UpdateActiveSyncVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007C09 RID: 31753 RVA: 0x000B8C8C File Offset: 0x000B6E8C
		public virtual UpdateActiveSyncVirtualDirectoryCommand SetParameters(UpdateActiveSyncVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200099F RID: 2463
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005483 RID: 21635
			// (set) Token: 0x06007C0A RID: 31754 RVA: 0x000B8C96 File Offset: 0x000B6E96
			public virtual string ActiveSyncServer
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncServer"] = value;
				}
			}

			// Token: 0x17005484 RID: 21636
			// (set) Token: 0x06007C0B RID: 31755 RVA: 0x000B8CA9 File Offset: 0x000B6EA9
			public virtual bool MobileClientCertificateProvisioningEnabled
			{
				set
				{
					base.PowerSharpParameters["MobileClientCertificateProvisioningEnabled"] = value;
				}
			}

			// Token: 0x17005485 RID: 21637
			// (set) Token: 0x06007C0C RID: 31756 RVA: 0x000B8CC1 File Offset: 0x000B6EC1
			public virtual bool BadItemReportingEnabled
			{
				set
				{
					base.PowerSharpParameters["BadItemReportingEnabled"] = value;
				}
			}

			// Token: 0x17005486 RID: 21638
			// (set) Token: 0x06007C0D RID: 31757 RVA: 0x000B8CD9 File Offset: 0x000B6ED9
			public virtual bool SendWatsonReport
			{
				set
				{
					base.PowerSharpParameters["SendWatsonReport"] = value;
				}
			}

			// Token: 0x17005487 RID: 21639
			// (set) Token: 0x06007C0E RID: 31758 RVA: 0x000B8CF1 File Offset: 0x000B6EF1
			public virtual string MobileClientCertificateAuthorityURL
			{
				set
				{
					base.PowerSharpParameters["MobileClientCertificateAuthorityURL"] = value;
				}
			}

			// Token: 0x17005488 RID: 21640
			// (set) Token: 0x06007C0F RID: 31759 RVA: 0x000B8D04 File Offset: 0x000B6F04
			public virtual string MobileClientCertTemplateName
			{
				set
				{
					base.PowerSharpParameters["MobileClientCertTemplateName"] = value;
				}
			}

			// Token: 0x17005489 RID: 21641
			// (set) Token: 0x06007C10 RID: 31760 RVA: 0x000B8D17 File Offset: 0x000B6F17
			public virtual ClientCertAuthTypes ClientCertAuth
			{
				set
				{
					base.PowerSharpParameters["ClientCertAuth"] = value;
				}
			}

			// Token: 0x1700548A RID: 21642
			// (set) Token: 0x06007C11 RID: 31761 RVA: 0x000B8D2F File Offset: 0x000B6F2F
			public virtual bool BasicAuthEnabled
			{
				set
				{
					base.PowerSharpParameters["BasicAuthEnabled"] = value;
				}
			}

			// Token: 0x1700548B RID: 21643
			// (set) Token: 0x06007C12 RID: 31762 RVA: 0x000B8D47 File Offset: 0x000B6F47
			public virtual bool WindowsAuthEnabled
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthEnabled"] = value;
				}
			}

			// Token: 0x1700548C RID: 21644
			// (set) Token: 0x06007C13 RID: 31763 RVA: 0x000B8D5F File Offset: 0x000B6F5F
			public virtual bool CompressionEnabled
			{
				set
				{
					base.PowerSharpParameters["CompressionEnabled"] = value;
				}
			}

			// Token: 0x1700548D RID: 21645
			// (set) Token: 0x06007C14 RID: 31764 RVA: 0x000B8D77 File Offset: 0x000B6F77
			public virtual RemoteDocumentsActions? RemoteDocumentsActionForUnknownServers
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsActionForUnknownServers"] = value;
				}
			}

			// Token: 0x1700548E RID: 21646
			// (set) Token: 0x06007C15 RID: 31765 RVA: 0x000B8D8F File Offset: 0x000B6F8F
			public virtual MultiValuedProperty<string> RemoteDocumentsAllowedServers
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsAllowedServers"] = value;
				}
			}

			// Token: 0x1700548F RID: 21647
			// (set) Token: 0x06007C16 RID: 31766 RVA: 0x000B8DA2 File Offset: 0x000B6FA2
			public virtual MultiValuedProperty<string> RemoteDocumentsBlockedServers
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsBlockedServers"] = value;
				}
			}

			// Token: 0x17005490 RID: 21648
			// (set) Token: 0x06007C17 RID: 31767 RVA: 0x000B8DB5 File Offset: 0x000B6FB5
			public virtual MultiValuedProperty<string> RemoteDocumentsInternalDomainSuffixList
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsInternalDomainSuffixList"] = value;
				}
			}

			// Token: 0x17005491 RID: 21649
			// (set) Token: 0x06007C18 RID: 31768 RVA: 0x000B8DC8 File Offset: 0x000B6FC8
			public virtual bool InstallIsapiFilter
			{
				set
				{
					base.PowerSharpParameters["InstallIsapiFilter"] = value;
				}
			}

			// Token: 0x17005492 RID: 21650
			// (set) Token: 0x06007C19 RID: 31769 RVA: 0x000B8DE0 File Offset: 0x000B6FE0
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x17005493 RID: 21651
			// (set) Token: 0x06007C1A RID: 31770 RVA: 0x000B8DF8 File Offset: 0x000B6FF8
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x17005494 RID: 21652
			// (set) Token: 0x06007C1B RID: 31771 RVA: 0x000B8E0B File Offset: 0x000B700B
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x17005495 RID: 21653
			// (set) Token: 0x06007C1C RID: 31772 RVA: 0x000B8E1E File Offset: 0x000B701E
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x17005496 RID: 21654
			// (set) Token: 0x06007C1D RID: 31773 RVA: 0x000B8E31 File Offset: 0x000B7031
			public virtual MultiValuedProperty<AuthenticationMethod> InternalAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["InternalAuthenticationMethods"] = value;
				}
			}

			// Token: 0x17005497 RID: 21655
			// (set) Token: 0x06007C1E RID: 31774 RVA: 0x000B8E44 File Offset: 0x000B7044
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x17005498 RID: 21656
			// (set) Token: 0x06007C1F RID: 31775 RVA: 0x000B8E57 File Offset: 0x000B7057
			public virtual MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["ExternalAuthenticationMethods"] = value;
				}
			}

			// Token: 0x17005499 RID: 21657
			// (set) Token: 0x06007C20 RID: 31776 RVA: 0x000B8E6A File Offset: 0x000B706A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700549A RID: 21658
			// (set) Token: 0x06007C21 RID: 31777 RVA: 0x000B8E7D File Offset: 0x000B707D
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700549B RID: 21659
			// (set) Token: 0x06007C22 RID: 31778 RVA: 0x000B8E90 File Offset: 0x000B7090
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700549C RID: 21660
			// (set) Token: 0x06007C23 RID: 31779 RVA: 0x000B8EA8 File Offset: 0x000B70A8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700549D RID: 21661
			// (set) Token: 0x06007C24 RID: 31780 RVA: 0x000B8EC0 File Offset: 0x000B70C0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700549E RID: 21662
			// (set) Token: 0x06007C25 RID: 31781 RVA: 0x000B8ED8 File Offset: 0x000B70D8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020009A0 RID: 2464
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700549F RID: 21663
			// (set) Token: 0x06007C27 RID: 31783 RVA: 0x000B8EF8 File Offset: 0x000B70F8
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170054A0 RID: 21664
			// (set) Token: 0x06007C28 RID: 31784 RVA: 0x000B8F0B File Offset: 0x000B710B
			public virtual string ActiveSyncServer
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncServer"] = value;
				}
			}

			// Token: 0x170054A1 RID: 21665
			// (set) Token: 0x06007C29 RID: 31785 RVA: 0x000B8F1E File Offset: 0x000B711E
			public virtual bool MobileClientCertificateProvisioningEnabled
			{
				set
				{
					base.PowerSharpParameters["MobileClientCertificateProvisioningEnabled"] = value;
				}
			}

			// Token: 0x170054A2 RID: 21666
			// (set) Token: 0x06007C2A RID: 31786 RVA: 0x000B8F36 File Offset: 0x000B7136
			public virtual bool BadItemReportingEnabled
			{
				set
				{
					base.PowerSharpParameters["BadItemReportingEnabled"] = value;
				}
			}

			// Token: 0x170054A3 RID: 21667
			// (set) Token: 0x06007C2B RID: 31787 RVA: 0x000B8F4E File Offset: 0x000B714E
			public virtual bool SendWatsonReport
			{
				set
				{
					base.PowerSharpParameters["SendWatsonReport"] = value;
				}
			}

			// Token: 0x170054A4 RID: 21668
			// (set) Token: 0x06007C2C RID: 31788 RVA: 0x000B8F66 File Offset: 0x000B7166
			public virtual string MobileClientCertificateAuthorityURL
			{
				set
				{
					base.PowerSharpParameters["MobileClientCertificateAuthorityURL"] = value;
				}
			}

			// Token: 0x170054A5 RID: 21669
			// (set) Token: 0x06007C2D RID: 31789 RVA: 0x000B8F79 File Offset: 0x000B7179
			public virtual string MobileClientCertTemplateName
			{
				set
				{
					base.PowerSharpParameters["MobileClientCertTemplateName"] = value;
				}
			}

			// Token: 0x170054A6 RID: 21670
			// (set) Token: 0x06007C2E RID: 31790 RVA: 0x000B8F8C File Offset: 0x000B718C
			public virtual ClientCertAuthTypes ClientCertAuth
			{
				set
				{
					base.PowerSharpParameters["ClientCertAuth"] = value;
				}
			}

			// Token: 0x170054A7 RID: 21671
			// (set) Token: 0x06007C2F RID: 31791 RVA: 0x000B8FA4 File Offset: 0x000B71A4
			public virtual bool BasicAuthEnabled
			{
				set
				{
					base.PowerSharpParameters["BasicAuthEnabled"] = value;
				}
			}

			// Token: 0x170054A8 RID: 21672
			// (set) Token: 0x06007C30 RID: 31792 RVA: 0x000B8FBC File Offset: 0x000B71BC
			public virtual bool WindowsAuthEnabled
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthEnabled"] = value;
				}
			}

			// Token: 0x170054A9 RID: 21673
			// (set) Token: 0x06007C31 RID: 31793 RVA: 0x000B8FD4 File Offset: 0x000B71D4
			public virtual bool CompressionEnabled
			{
				set
				{
					base.PowerSharpParameters["CompressionEnabled"] = value;
				}
			}

			// Token: 0x170054AA RID: 21674
			// (set) Token: 0x06007C32 RID: 31794 RVA: 0x000B8FEC File Offset: 0x000B71EC
			public virtual RemoteDocumentsActions? RemoteDocumentsActionForUnknownServers
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsActionForUnknownServers"] = value;
				}
			}

			// Token: 0x170054AB RID: 21675
			// (set) Token: 0x06007C33 RID: 31795 RVA: 0x000B9004 File Offset: 0x000B7204
			public virtual MultiValuedProperty<string> RemoteDocumentsAllowedServers
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsAllowedServers"] = value;
				}
			}

			// Token: 0x170054AC RID: 21676
			// (set) Token: 0x06007C34 RID: 31796 RVA: 0x000B9017 File Offset: 0x000B7217
			public virtual MultiValuedProperty<string> RemoteDocumentsBlockedServers
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsBlockedServers"] = value;
				}
			}

			// Token: 0x170054AD RID: 21677
			// (set) Token: 0x06007C35 RID: 31797 RVA: 0x000B902A File Offset: 0x000B722A
			public virtual MultiValuedProperty<string> RemoteDocumentsInternalDomainSuffixList
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsInternalDomainSuffixList"] = value;
				}
			}

			// Token: 0x170054AE RID: 21678
			// (set) Token: 0x06007C36 RID: 31798 RVA: 0x000B903D File Offset: 0x000B723D
			public virtual bool InstallIsapiFilter
			{
				set
				{
					base.PowerSharpParameters["InstallIsapiFilter"] = value;
				}
			}

			// Token: 0x170054AF RID: 21679
			// (set) Token: 0x06007C37 RID: 31799 RVA: 0x000B9055 File Offset: 0x000B7255
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x170054B0 RID: 21680
			// (set) Token: 0x06007C38 RID: 31800 RVA: 0x000B906D File Offset: 0x000B726D
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x170054B1 RID: 21681
			// (set) Token: 0x06007C39 RID: 31801 RVA: 0x000B9080 File Offset: 0x000B7280
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x170054B2 RID: 21682
			// (set) Token: 0x06007C3A RID: 31802 RVA: 0x000B9093 File Offset: 0x000B7293
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x170054B3 RID: 21683
			// (set) Token: 0x06007C3B RID: 31803 RVA: 0x000B90A6 File Offset: 0x000B72A6
			public virtual MultiValuedProperty<AuthenticationMethod> InternalAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["InternalAuthenticationMethods"] = value;
				}
			}

			// Token: 0x170054B4 RID: 21684
			// (set) Token: 0x06007C3C RID: 31804 RVA: 0x000B90B9 File Offset: 0x000B72B9
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x170054B5 RID: 21685
			// (set) Token: 0x06007C3D RID: 31805 RVA: 0x000B90CC File Offset: 0x000B72CC
			public virtual MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["ExternalAuthenticationMethods"] = value;
				}
			}

			// Token: 0x170054B6 RID: 21686
			// (set) Token: 0x06007C3E RID: 31806 RVA: 0x000B90DF File Offset: 0x000B72DF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170054B7 RID: 21687
			// (set) Token: 0x06007C3F RID: 31807 RVA: 0x000B90F2 File Offset: 0x000B72F2
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170054B8 RID: 21688
			// (set) Token: 0x06007C40 RID: 31808 RVA: 0x000B9105 File Offset: 0x000B7305
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170054B9 RID: 21689
			// (set) Token: 0x06007C41 RID: 31809 RVA: 0x000B911D File Offset: 0x000B731D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170054BA RID: 21690
			// (set) Token: 0x06007C42 RID: 31810 RVA: 0x000B9135 File Offset: 0x000B7335
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170054BB RID: 21691
			// (set) Token: 0x06007C43 RID: 31811 RVA: 0x000B914D File Offset: 0x000B734D
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
