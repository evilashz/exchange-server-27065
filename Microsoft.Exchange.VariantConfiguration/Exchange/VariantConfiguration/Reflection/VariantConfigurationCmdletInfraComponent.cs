using System;
using Microsoft.Exchange.Flighting;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x020000FF RID: 255
	public sealed class VariantConfigurationCmdletInfraComponent : VariantConfigurationComponent
	{
		// Token: 0x06000B0F RID: 2831 RVA: 0x00019EB8 File Offset: 0x000180B8
		internal VariantConfigurationCmdletInfraComponent() : base("CmdletInfra")
		{
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "New-TransportRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "ReportingWebService", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "PrePopulateCacheForMailboxBasedOnDatabase", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Set-MailboxImportRequest", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Set-HoldComplianceRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-HistoricalSearch", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-SPOOneDriveForBusinessFileActivityReport", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Remove-DataClassification", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "SetPasswordWithoutOldPassword", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "New-AuditConfigurationPolicy", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "New-MailboxSearch", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "SiteMailboxCheckSharePointUrlAgainstTrustedHosts", typeof(IFeature), true));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Set-DataClassification", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "New-AuditConfigurationRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "LimitNameMaxlength", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "CmdletMonitoring", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Set-ReportSchedule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-HoldComplianceRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "GlobalAddressListAttrbutes", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-ComplianceSearch", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Add-Mailbox", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Install-UnifiedCompliancePrerequisite", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "ServiceAccountForest", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-SPOSkyDriveProStorageReport", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "InactiveMailbox", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "New-DlpComplianceRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Remove-HoldComplianceRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Psws", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Remove-ReportSchedule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-ClientAccessRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "SetDefaultProhibitSendReceiveQuota", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Set-Mailbox", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-ExternalActivityReport", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-DlpCompliancePolicy", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "ReportToOriginator", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Set-MailUser", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "New-MailboxExportRequest", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Test-ClientAccessRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-ExternalActivitySummaryReport", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "New-ClientAccessRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-ExternalActivityByUserReport", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-FolderMoveRequest", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Stop-HistoricalSearch", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "New-DlpCompliancePolicy", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Set-DeviceConfigurationRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "WinRMExchangeDataUseTypeNamedPipe", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-ReportScheduleHistory", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Remove-FolderMoveRequest", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "RecoverMailBox", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "SiteMailboxProvisioningInExecutingUserOUEnabled", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-ListedIPWrapper", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Set-ClientAccessRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-ExternalActivityByDomainReport", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "New-DeviceConfigurationRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-CsClientDeviceReport", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-DlpComplianceRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-DeviceConfigurationRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Remove-HoldCompliancePolicy", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "ShowFismaBanner", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "UseDatabaseQuotaDefaults", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-AuditConfigurationRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "WriteEventLogInEnglish", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Set-DlpCompliancePolicy", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "SupportOptimizedFilterOnlyInDDG", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Remove-ComplianceSearch", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "DepthTwoTypeEntry", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Set-AuditConfig", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "New-DataClassification", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "New-MigrationEndpoint", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Set-MailboxExportRequest", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "ValidateExternalEmailAddressInAcceptedDomain", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Enable-EOPMailUser", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-OMEConfiguration", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "New-FolderMoveRequest", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "EmailAddressPolicy", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "SkipPiiRedactionForForestWideObject", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-PartnerClientExpiringSubscriptionReport", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "PiiRedaction", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "ValidateFilteringOnlyUser", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "SoftDeleteObject", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Set-MailboxSearch", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-SPOOneDriveForBusinessUserStatisticsReport", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Set-FolderMoveRequest", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Add-DelistIP", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "GenerateNewExternalDirectoryObjectId", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "New-ComplianceSearch", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "IncludeFBOnlyForCalendarContributor", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "ValidateEnableRoomMailboxAccount", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Set-DlpComplianceRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Remove-DlpComplianceRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "PswsCmdletProxy", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Set-HoldCompliancePolicy", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "LegacyRegCodeSupport", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Set-OMEConfiguration", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-SPOActiveUserReport", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Remove-AuditConfigurationRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-SPOSkyDriveProDeployedReport", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Set-TransportRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "New-Fingerprint", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-ReputationOverride", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "New-ReportSchedule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "New-Mailbox", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "InstallModernGroupsAddressList", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "GenericExchangeSnapin", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Set-MigrationBatch", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Remove-AuditConfigurationPolicy", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Set-AuditConfigurationRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Remove-ClientAccessRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "OverWriteElcMailboxFlags", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "MaxAddressBookPolicies", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Start-ComplianceSearch", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Test-MigrationServerAvailability", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "WinRMExchangeDataUseAuthenticationType", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "RpsClientAccessRulesEnabled", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Stop-ComplianceSearch", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Resume-FolderMoveRequest", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Remove-DlpCompliancePolicy", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Remove-Mailbox", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-SPOTeamSiteDeployedReport", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "New-HoldComplianceRule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "PswsClientAccessRulesEnabled", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Remove-ReputationOverride", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-AuditConfigurationPolicy", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-DnsBlocklistInfo", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-FolderMoveRequestStatistics", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Start-HistoricalSearch", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "CheckForDedicatedTenantAdminRoleNamePrefix", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Suspend-FolderMoveRequest", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "New-MailboxImportRequest", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "New-MigrationBatch", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Set-ComplianceSearch", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-SPOTeamSiteStorageReport", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-HoldCompliancePolicy", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-DlpSensitiveInformationType", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-ReportScheduleList", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-Mailbox", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-SPOTenantStorageMetricReport", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "New-MailUser", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-ReportSchedule", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "SetActiveArchiveStatus", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "Get-AuditConfig", typeof(ICmdletSettings), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "WsSecuritySymmetricAndX509Cert", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "ProxyDllUpdate", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CmdletInfra.settings.ini", "New-HoldCompliancePolicy", typeof(ICmdletSettings), false));
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0001B0D0 File Offset: 0x000192D0
		public VariantConfigurationSection NewTransportRule
		{
			get
			{
				return base["NewTransportRule"];
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0001B0DD File Offset: 0x000192DD
		public VariantConfigurationSection ReportingWebService
		{
			get
			{
				return base["ReportingWebService"];
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0001B0EA File Offset: 0x000192EA
		public VariantConfigurationSection PrePopulateCacheForMailboxBasedOnDatabase
		{
			get
			{
				return base["PrePopulateCacheForMailboxBasedOnDatabase"];
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x0001B0F7 File Offset: 0x000192F7
		public VariantConfigurationSection SetMailboxImportRequest
		{
			get
			{
				return base["SetMailboxImportRequest"];
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0001B104 File Offset: 0x00019304
		public VariantConfigurationSection SetHoldComplianceRule
		{
			get
			{
				return base["SetHoldComplianceRule"];
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0001B111 File Offset: 0x00019311
		public VariantConfigurationSection GetHistoricalSearch
		{
			get
			{
				return base["GetHistoricalSearch"];
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0001B11E File Offset: 0x0001931E
		public VariantConfigurationSection GetSPOOneDriveForBusinessFileActivityReport
		{
			get
			{
				return base["GetSPOOneDriveForBusinessFileActivityReport"];
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0001B12B File Offset: 0x0001932B
		public VariantConfigurationSection RemoveDataClassification
		{
			get
			{
				return base["RemoveDataClassification"];
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0001B138 File Offset: 0x00019338
		public VariantConfigurationSection SetPasswordWithoutOldPassword
		{
			get
			{
				return base["SetPasswordWithoutOldPassword"];
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0001B145 File Offset: 0x00019345
		public VariantConfigurationSection NewAuditConfigurationPolicy
		{
			get
			{
				return base["NewAuditConfigurationPolicy"];
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0001B152 File Offset: 0x00019352
		public VariantConfigurationSection NewMailboxSearch
		{
			get
			{
				return base["NewMailboxSearch"];
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0001B15F File Offset: 0x0001935F
		public VariantConfigurationSection SiteMailboxCheckSharePointUrlAgainstTrustedHosts
		{
			get
			{
				return base["SiteMailboxCheckSharePointUrlAgainstTrustedHosts"];
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0001B16C File Offset: 0x0001936C
		public VariantConfigurationSection SetDataClassification
		{
			get
			{
				return base["SetDataClassification"];
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0001B179 File Offset: 0x00019379
		public VariantConfigurationSection NewAuditConfigurationRule
		{
			get
			{
				return base["NewAuditConfigurationRule"];
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0001B186 File Offset: 0x00019386
		public VariantConfigurationSection LimitNameMaxlength
		{
			get
			{
				return base["LimitNameMaxlength"];
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0001B193 File Offset: 0x00019393
		public VariantConfigurationSection CmdletMonitoring
		{
			get
			{
				return base["CmdletMonitoring"];
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0001B1A0 File Offset: 0x000193A0
		public VariantConfigurationSection SetReportSchedule
		{
			get
			{
				return base["SetReportSchedule"];
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0001B1AD File Offset: 0x000193AD
		public VariantConfigurationSection GetHoldComplianceRule
		{
			get
			{
				return base["GetHoldComplianceRule"];
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0001B1BA File Offset: 0x000193BA
		public VariantConfigurationSection GlobalAddressListAttrbutes
		{
			get
			{
				return base["GlobalAddressListAttrbutes"];
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0001B1C7 File Offset: 0x000193C7
		public VariantConfigurationSection GetComplianceSearch
		{
			get
			{
				return base["GetComplianceSearch"];
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0001B1D4 File Offset: 0x000193D4
		public VariantConfigurationSection AddMailbox
		{
			get
			{
				return base["AddMailbox"];
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x0001B1E1 File Offset: 0x000193E1
		public VariantConfigurationSection InstallUnifiedCompliancePrerequisite
		{
			get
			{
				return base["InstallUnifiedCompliancePrerequisite"];
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0001B1EE File Offset: 0x000193EE
		public VariantConfigurationSection ServiceAccountForest
		{
			get
			{
				return base["ServiceAccountForest"];
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0001B1FB File Offset: 0x000193FB
		public VariantConfigurationSection GetSPOSkyDriveProStorageReport
		{
			get
			{
				return base["GetSPOSkyDriveProStorageReport"];
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0001B208 File Offset: 0x00019408
		public VariantConfigurationSection InactiveMailbox
		{
			get
			{
				return base["InactiveMailbox"];
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0001B215 File Offset: 0x00019415
		public VariantConfigurationSection NewDlpComplianceRule
		{
			get
			{
				return base["NewDlpComplianceRule"];
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0001B222 File Offset: 0x00019422
		public VariantConfigurationSection RemoveHoldComplianceRule
		{
			get
			{
				return base["RemoveHoldComplianceRule"];
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0001B22F File Offset: 0x0001942F
		public VariantConfigurationSection Psws
		{
			get
			{
				return base["Psws"];
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0001B23C File Offset: 0x0001943C
		public VariantConfigurationSection RemoveReportSchedule
		{
			get
			{
				return base["RemoveReportSchedule"];
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0001B249 File Offset: 0x00019449
		public VariantConfigurationSection GetClientAccessRule
		{
			get
			{
				return base["GetClientAccessRule"];
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0001B256 File Offset: 0x00019456
		public VariantConfigurationSection SetDefaultProhibitSendReceiveQuota
		{
			get
			{
				return base["SetDefaultProhibitSendReceiveQuota"];
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0001B263 File Offset: 0x00019463
		public VariantConfigurationSection SetMailbox
		{
			get
			{
				return base["SetMailbox"];
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0001B270 File Offset: 0x00019470
		public VariantConfigurationSection GetExternalActivityReport
		{
			get
			{
				return base["GetExternalActivityReport"];
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0001B27D File Offset: 0x0001947D
		public VariantConfigurationSection GetDlpCompliancePolicy
		{
			get
			{
				return base["GetDlpCompliancePolicy"];
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x0001B28A File Offset: 0x0001948A
		public VariantConfigurationSection ReportToOriginator
		{
			get
			{
				return base["ReportToOriginator"];
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0001B297 File Offset: 0x00019497
		public VariantConfigurationSection SetMailUser
		{
			get
			{
				return base["SetMailUser"];
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0001B2A4 File Offset: 0x000194A4
		public VariantConfigurationSection NewMailboxExportRequest
		{
			get
			{
				return base["NewMailboxExportRequest"];
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x0001B2B1 File Offset: 0x000194B1
		public VariantConfigurationSection TestClientAccessRule
		{
			get
			{
				return base["TestClientAccessRule"];
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x0001B2BE File Offset: 0x000194BE
		public VariantConfigurationSection GetExternalActivitySummaryReport
		{
			get
			{
				return base["GetExternalActivitySummaryReport"];
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x0001B2CB File Offset: 0x000194CB
		public VariantConfigurationSection NewClientAccessRule
		{
			get
			{
				return base["NewClientAccessRule"];
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x0001B2D8 File Offset: 0x000194D8
		public VariantConfigurationSection GetExternalActivityByUserReport
		{
			get
			{
				return base["GetExternalActivityByUserReport"];
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0001B2E5 File Offset: 0x000194E5
		public VariantConfigurationSection GetFolderMoveRequest
		{
			get
			{
				return base["GetFolderMoveRequest"];
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x0001B2F2 File Offset: 0x000194F2
		public VariantConfigurationSection StopHistoricalSearch
		{
			get
			{
				return base["StopHistoricalSearch"];
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0001B2FF File Offset: 0x000194FF
		public VariantConfigurationSection NewDlpCompliancePolicy
		{
			get
			{
				return base["NewDlpCompliancePolicy"];
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0001B30C File Offset: 0x0001950C
		public VariantConfigurationSection SetDeviceConfigurationRule
		{
			get
			{
				return base["SetDeviceConfigurationRule"];
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0001B319 File Offset: 0x00019519
		public VariantConfigurationSection WinRMExchangeDataUseTypeNamedPipe
		{
			get
			{
				return base["WinRMExchangeDataUseTypeNamedPipe"];
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x0001B326 File Offset: 0x00019526
		public VariantConfigurationSection GetReportScheduleHistory
		{
			get
			{
				return base["GetReportScheduleHistory"];
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0001B333 File Offset: 0x00019533
		public VariantConfigurationSection RemoveFolderMoveRequest
		{
			get
			{
				return base["RemoveFolderMoveRequest"];
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x0001B340 File Offset: 0x00019540
		public VariantConfigurationSection RecoverMailBox
		{
			get
			{
				return base["RecoverMailBox"];
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0001B34D File Offset: 0x0001954D
		public VariantConfigurationSection SiteMailboxProvisioningInExecutingUserOUEnabled
		{
			get
			{
				return base["SiteMailboxProvisioningInExecutingUserOUEnabled"];
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0001B35A File Offset: 0x0001955A
		public VariantConfigurationSection GetListedIPWrapper
		{
			get
			{
				return base["GetListedIPWrapper"];
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x0001B367 File Offset: 0x00019567
		public VariantConfigurationSection SetClientAccessRule
		{
			get
			{
				return base["SetClientAccessRule"];
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0001B374 File Offset: 0x00019574
		public VariantConfigurationSection GetExternalActivityByDomainReport
		{
			get
			{
				return base["GetExternalActivityByDomainReport"];
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0001B381 File Offset: 0x00019581
		public VariantConfigurationSection NewDeviceConfigurationRule
		{
			get
			{
				return base["NewDeviceConfigurationRule"];
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0001B38E File Offset: 0x0001958E
		public VariantConfigurationSection GetCsClientDeviceReport
		{
			get
			{
				return base["GetCsClientDeviceReport"];
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x0001B39B File Offset: 0x0001959B
		public VariantConfigurationSection GetDlpComplianceRule
		{
			get
			{
				return base["GetDlpComplianceRule"];
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0001B3A8 File Offset: 0x000195A8
		public VariantConfigurationSection GetDeviceConfigurationRule
		{
			get
			{
				return base["GetDeviceConfigurationRule"];
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x0001B3B5 File Offset: 0x000195B5
		public VariantConfigurationSection RemoveHoldCompliancePolicy
		{
			get
			{
				return base["RemoveHoldCompliancePolicy"];
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x0001B3C2 File Offset: 0x000195C2
		public VariantConfigurationSection ShowFismaBanner
		{
			get
			{
				return base["ShowFismaBanner"];
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x0001B3CF File Offset: 0x000195CF
		public VariantConfigurationSection UseDatabaseQuotaDefaults
		{
			get
			{
				return base["UseDatabaseQuotaDefaults"];
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0001B3DC File Offset: 0x000195DC
		public VariantConfigurationSection GetAuditConfigurationRule
		{
			get
			{
				return base["GetAuditConfigurationRule"];
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x0001B3E9 File Offset: 0x000195E9
		public VariantConfigurationSection WriteEventLogInEnglish
		{
			get
			{
				return base["WriteEventLogInEnglish"];
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0001B3F6 File Offset: 0x000195F6
		public VariantConfigurationSection SetDlpCompliancePolicy
		{
			get
			{
				return base["SetDlpCompliancePolicy"];
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0001B403 File Offset: 0x00019603
		public VariantConfigurationSection SupportOptimizedFilterOnlyInDDG
		{
			get
			{
				return base["SupportOptimizedFilterOnlyInDDG"];
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0001B410 File Offset: 0x00019610
		public VariantConfigurationSection RemoveComplianceSearch
		{
			get
			{
				return base["RemoveComplianceSearch"];
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0001B41D File Offset: 0x0001961D
		public VariantConfigurationSection DepthTwoTypeEntry
		{
			get
			{
				return base["DepthTwoTypeEntry"];
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0001B42A File Offset: 0x0001962A
		public VariantConfigurationSection SetAuditConfig
		{
			get
			{
				return base["SetAuditConfig"];
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x0001B437 File Offset: 0x00019637
		public VariantConfigurationSection NewDataClassification
		{
			get
			{
				return base["NewDataClassification"];
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0001B444 File Offset: 0x00019644
		public VariantConfigurationSection NewMigrationEndpoint
		{
			get
			{
				return base["NewMigrationEndpoint"];
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0001B451 File Offset: 0x00019651
		public VariantConfigurationSection SetMailboxExportRequest
		{
			get
			{
				return base["SetMailboxExportRequest"];
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0001B45E File Offset: 0x0001965E
		public VariantConfigurationSection ValidateExternalEmailAddressInAcceptedDomain
		{
			get
			{
				return base["ValidateExternalEmailAddressInAcceptedDomain"];
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0001B46B File Offset: 0x0001966B
		public VariantConfigurationSection EnableEOPMailUser
		{
			get
			{
				return base["EnableEOPMailUser"];
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x0001B478 File Offset: 0x00019678
		public VariantConfigurationSection GetOMEConfiguration
		{
			get
			{
				return base["GetOMEConfiguration"];
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0001B485 File Offset: 0x00019685
		public VariantConfigurationSection NewFolderMoveRequest
		{
			get
			{
				return base["NewFolderMoveRequest"];
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x0001B492 File Offset: 0x00019692
		public VariantConfigurationSection EmailAddressPolicy
		{
			get
			{
				return base["EmailAddressPolicy"];
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x0001B49F File Offset: 0x0001969F
		public VariantConfigurationSection SkipPiiRedactionForForestWideObject
		{
			get
			{
				return base["SkipPiiRedactionForForestWideObject"];
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0001B4AC File Offset: 0x000196AC
		public VariantConfigurationSection GetPartnerClientExpiringSubscriptionReport
		{
			get
			{
				return base["GetPartnerClientExpiringSubscriptionReport"];
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x0001B4B9 File Offset: 0x000196B9
		public VariantConfigurationSection PiiRedaction
		{
			get
			{
				return base["PiiRedaction"];
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0001B4C6 File Offset: 0x000196C6
		public VariantConfigurationSection ValidateFilteringOnlyUser
		{
			get
			{
				return base["ValidateFilteringOnlyUser"];
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x0001B4D3 File Offset: 0x000196D3
		public VariantConfigurationSection SoftDeleteObject
		{
			get
			{
				return base["SoftDeleteObject"];
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0001B4E0 File Offset: 0x000196E0
		public VariantConfigurationSection SetMailboxSearch
		{
			get
			{
				return base["SetMailboxSearch"];
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x0001B4ED File Offset: 0x000196ED
		public VariantConfigurationSection GetSPOOneDriveForBusinessUserStatisticsReport
		{
			get
			{
				return base["GetSPOOneDriveForBusinessUserStatisticsReport"];
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0001B4FA File Offset: 0x000196FA
		public VariantConfigurationSection SetFolderMoveRequest
		{
			get
			{
				return base["SetFolderMoveRequest"];
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x0001B507 File Offset: 0x00019707
		public VariantConfigurationSection AddDelistIP
		{
			get
			{
				return base["AddDelistIP"];
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x0001B514 File Offset: 0x00019714
		public VariantConfigurationSection GenerateNewExternalDirectoryObjectId
		{
			get
			{
				return base["GenerateNewExternalDirectoryObjectId"];
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0001B521 File Offset: 0x00019721
		public VariantConfigurationSection NewComplianceSearch
		{
			get
			{
				return base["NewComplianceSearch"];
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x0001B52E File Offset: 0x0001972E
		public VariantConfigurationSection IncludeFBOnlyForCalendarContributor
		{
			get
			{
				return base["IncludeFBOnlyForCalendarContributor"];
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x0001B53B File Offset: 0x0001973B
		public VariantConfigurationSection ValidateEnableRoomMailboxAccount
		{
			get
			{
				return base["ValidateEnableRoomMailboxAccount"];
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0001B548 File Offset: 0x00019748
		public VariantConfigurationSection SetDlpComplianceRule
		{
			get
			{
				return base["SetDlpComplianceRule"];
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0001B555 File Offset: 0x00019755
		public VariantConfigurationSection RemoveDlpComplianceRule
		{
			get
			{
				return base["RemoveDlpComplianceRule"];
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0001B562 File Offset: 0x00019762
		public VariantConfigurationSection PswsCmdletProxy
		{
			get
			{
				return base["PswsCmdletProxy"];
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x0001B56F File Offset: 0x0001976F
		public VariantConfigurationSection SetHoldCompliancePolicy
		{
			get
			{
				return base["SetHoldCompliancePolicy"];
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0001B57C File Offset: 0x0001977C
		public VariantConfigurationSection LegacyRegCodeSupport
		{
			get
			{
				return base["LegacyRegCodeSupport"];
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0001B589 File Offset: 0x00019789
		public VariantConfigurationSection SetOMEConfiguration
		{
			get
			{
				return base["SetOMEConfiguration"];
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0001B596 File Offset: 0x00019796
		public VariantConfigurationSection GetSPOActiveUserReport
		{
			get
			{
				return base["GetSPOActiveUserReport"];
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x0001B5A3 File Offset: 0x000197A3
		public VariantConfigurationSection RemoveAuditConfigurationRule
		{
			get
			{
				return base["RemoveAuditConfigurationRule"];
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0001B5B0 File Offset: 0x000197B0
		public VariantConfigurationSection GetSPOSkyDriveProDeployedReport
		{
			get
			{
				return base["GetSPOSkyDriveProDeployedReport"];
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x0001B5BD File Offset: 0x000197BD
		public VariantConfigurationSection SetTransportRule
		{
			get
			{
				return base["SetTransportRule"];
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0001B5CA File Offset: 0x000197CA
		public VariantConfigurationSection NewFingerprint
		{
			get
			{
				return base["NewFingerprint"];
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0001B5D7 File Offset: 0x000197D7
		public VariantConfigurationSection GetReputationOverride
		{
			get
			{
				return base["GetReputationOverride"];
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0001B5E4 File Offset: 0x000197E4
		public VariantConfigurationSection NewReportSchedule
		{
			get
			{
				return base["NewReportSchedule"];
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0001B5F1 File Offset: 0x000197F1
		public VariantConfigurationSection NewMailbox
		{
			get
			{
				return base["NewMailbox"];
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0001B5FE File Offset: 0x000197FE
		public VariantConfigurationSection InstallModernGroupsAddressList
		{
			get
			{
				return base["InstallModernGroupsAddressList"];
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0001B60B File Offset: 0x0001980B
		public VariantConfigurationSection GenericExchangeSnapin
		{
			get
			{
				return base["GenericExchangeSnapin"];
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0001B618 File Offset: 0x00019818
		public VariantConfigurationSection SetMigrationBatch
		{
			get
			{
				return base["SetMigrationBatch"];
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x0001B625 File Offset: 0x00019825
		public VariantConfigurationSection RemoveAuditConfigurationPolicy
		{
			get
			{
				return base["RemoveAuditConfigurationPolicy"];
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x0001B632 File Offset: 0x00019832
		public VariantConfigurationSection SetAuditConfigurationRule
		{
			get
			{
				return base["SetAuditConfigurationRule"];
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x0001B63F File Offset: 0x0001983F
		public VariantConfigurationSection RemoveClientAccessRule
		{
			get
			{
				return base["RemoveClientAccessRule"];
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0001B64C File Offset: 0x0001984C
		public VariantConfigurationSection OverWriteElcMailboxFlags
		{
			get
			{
				return base["OverWriteElcMailboxFlags"];
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x0001B659 File Offset: 0x00019859
		public VariantConfigurationSection MaxAddressBookPolicies
		{
			get
			{
				return base["MaxAddressBookPolicies"];
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x0001B666 File Offset: 0x00019866
		public VariantConfigurationSection StartComplianceSearch
		{
			get
			{
				return base["StartComplianceSearch"];
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x0001B673 File Offset: 0x00019873
		public VariantConfigurationSection TestMigrationServerAvailability
		{
			get
			{
				return base["TestMigrationServerAvailability"];
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x0001B680 File Offset: 0x00019880
		public VariantConfigurationSection WinRMExchangeDataUseAuthenticationType
		{
			get
			{
				return base["WinRMExchangeDataUseAuthenticationType"];
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x0001B68D File Offset: 0x0001988D
		public VariantConfigurationSection RpsClientAccessRulesEnabled
		{
			get
			{
				return base["RpsClientAccessRulesEnabled"];
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0001B69A File Offset: 0x0001989A
		public VariantConfigurationSection StopComplianceSearch
		{
			get
			{
				return base["StopComplianceSearch"];
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x0001B6A7 File Offset: 0x000198A7
		public VariantConfigurationSection ResumeFolderMoveRequest
		{
			get
			{
				return base["ResumeFolderMoveRequest"];
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x0001B6B4 File Offset: 0x000198B4
		public VariantConfigurationSection RemoveDlpCompliancePolicy
		{
			get
			{
				return base["RemoveDlpCompliancePolicy"];
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x0001B6C1 File Offset: 0x000198C1
		public VariantConfigurationSection RemoveMailbox
		{
			get
			{
				return base["RemoveMailbox"];
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0001B6CE File Offset: 0x000198CE
		public VariantConfigurationSection GetSPOTeamSiteDeployedReport
		{
			get
			{
				return base["GetSPOTeamSiteDeployedReport"];
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0001B6DB File Offset: 0x000198DB
		public VariantConfigurationSection NewHoldComplianceRule
		{
			get
			{
				return base["NewHoldComplianceRule"];
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0001B6E8 File Offset: 0x000198E8
		public VariantConfigurationSection PswsClientAccessRulesEnabled
		{
			get
			{
				return base["PswsClientAccessRulesEnabled"];
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0001B6F5 File Offset: 0x000198F5
		public VariantConfigurationSection RemoveReputationOverride
		{
			get
			{
				return base["RemoveReputationOverride"];
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0001B702 File Offset: 0x00019902
		public VariantConfigurationSection GetAuditConfigurationPolicy
		{
			get
			{
				return base["GetAuditConfigurationPolicy"];
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0001B70F File Offset: 0x0001990F
		public VariantConfigurationSection GetDnsBlocklistInfo
		{
			get
			{
				return base["GetDnsBlocklistInfo"];
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0001B71C File Offset: 0x0001991C
		public VariantConfigurationSection GetFolderMoveRequestStatistics
		{
			get
			{
				return base["GetFolderMoveRequestStatistics"];
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0001B729 File Offset: 0x00019929
		public VariantConfigurationSection StartHistoricalSearch
		{
			get
			{
				return base["StartHistoricalSearch"];
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x0001B736 File Offset: 0x00019936
		public VariantConfigurationSection CheckForDedicatedTenantAdminRoleNamePrefix
		{
			get
			{
				return base["CheckForDedicatedTenantAdminRoleNamePrefix"];
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0001B743 File Offset: 0x00019943
		public VariantConfigurationSection SuspendFolderMoveRequest
		{
			get
			{
				return base["SuspendFolderMoveRequest"];
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0001B750 File Offset: 0x00019950
		public VariantConfigurationSection NewMailboxImportRequest
		{
			get
			{
				return base["NewMailboxImportRequest"];
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x0001B75D File Offset: 0x0001995D
		public VariantConfigurationSection NewMigrationBatch
		{
			get
			{
				return base["NewMigrationBatch"];
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0001B76A File Offset: 0x0001996A
		public VariantConfigurationSection SetComplianceSearch
		{
			get
			{
				return base["SetComplianceSearch"];
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x0001B777 File Offset: 0x00019977
		public VariantConfigurationSection GetSPOTeamSiteStorageReport
		{
			get
			{
				return base["GetSPOTeamSiteStorageReport"];
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x0001B784 File Offset: 0x00019984
		public VariantConfigurationSection GetHoldCompliancePolicy
		{
			get
			{
				return base["GetHoldCompliancePolicy"];
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0001B791 File Offset: 0x00019991
		public VariantConfigurationSection GetDlpSensitiveInformationType
		{
			get
			{
				return base["GetDlpSensitiveInformationType"];
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0001B79E File Offset: 0x0001999E
		public VariantConfigurationSection GetReportScheduleList
		{
			get
			{
				return base["GetReportScheduleList"];
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0001B7AB File Offset: 0x000199AB
		public VariantConfigurationSection GetMailbox
		{
			get
			{
				return base["GetMailbox"];
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0001B7B8 File Offset: 0x000199B8
		public VariantConfigurationSection GetSPOTenantStorageMetricReport
		{
			get
			{
				return base["GetSPOTenantStorageMetricReport"];
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0001B7C5 File Offset: 0x000199C5
		public VariantConfigurationSection NewMailUser
		{
			get
			{
				return base["NewMailUser"];
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0001B7D2 File Offset: 0x000199D2
		public VariantConfigurationSection GetReportSchedule
		{
			get
			{
				return base["GetReportSchedule"];
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0001B7DF File Offset: 0x000199DF
		public VariantConfigurationSection SetActiveArchiveStatus
		{
			get
			{
				return base["SetActiveArchiveStatus"];
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x0001B7EC File Offset: 0x000199EC
		public VariantConfigurationSection GetAuditConfig
		{
			get
			{
				return base["GetAuditConfig"];
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x0001B7F9 File Offset: 0x000199F9
		public VariantConfigurationSection WsSecuritySymmetricAndX509Cert
		{
			get
			{
				return base["WsSecuritySymmetricAndX509Cert"];
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0001B806 File Offset: 0x00019A06
		public VariantConfigurationSection ProxyDllUpdate
		{
			get
			{
				return base["ProxyDllUpdate"];
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0001B813 File Offset: 0x00019A13
		public VariantConfigurationSection NewHoldCompliancePolicy
		{
			get
			{
				return base["NewHoldCompliancePolicy"];
			}
		}
	}
}
