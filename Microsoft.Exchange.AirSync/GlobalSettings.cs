using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000BE RID: 190
	internal class GlobalSettings
	{
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x0003B61E File Offset: 0x0003981E
		// (set) Token: 0x06000AB0 RID: 2736 RVA: 0x0003B625 File Offset: 0x00039825
		public static GlobalSettings.OnLoadConfigSettingDelegate OnLoadConfigSetting { get; set; }

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0003B62D File Offset: 0x0003982D
		static GlobalSettings()
		{
			CertificateValidationManager.RegisterCallback("AirSync", new RemoteCertificateValidationCallback(ProxyHandler.SslCertificateValidationCallback));
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0003B659 File Offset: 0x00039859
		public static void Clear()
		{
			GlobalSettings.settings.Clear();
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0003B668 File Offset: 0x00039868
		public static bool Clear(GlobalSettingsPropertyDefinition propDef)
		{
			object obj;
			return GlobalSettings.settings.TryRemove(propDef, out obj);
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x0003B684 File Offset: 0x00039884
		public static SyncLog SyncLog
		{
			get
			{
				if (GlobalSettings.syncLog == null)
				{
					lock (GlobalSettings.syncLogCreationLock)
					{
						if (GlobalSettings.syncLog == null)
						{
							SyncLogConfiguration syncLogConfiguration = new SyncLogConfiguration();
							syncLogConfiguration.Enabled = GlobalSettings.SyncLogEnabled;
							syncLogConfiguration.LogFilePrefix = "AirSync";
							syncLogConfiguration.LogComponent = "AirSync";
							string text = GlobalSettings.SyncLogDirectory;
							if (string.IsNullOrEmpty(text))
							{
								text = Path.Combine(Path.GetTempPath(), "CasSyncLogs");
							}
							syncLogConfiguration.LogFilePath = text;
							syncLogConfiguration.SyncLoggingLevel = SyncLoggingLevel.Information;
							GlobalSettings.syncLog = new SyncLog(syncLogConfiguration);
						}
					}
				}
				return GlobalSettings.syncLog;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x0003B734 File Offset: 0x00039934
		internal static bool UseTestBudget
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.UseTestBudget);
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x0003B740 File Offset: 0x00039940
		internal static bool WriteProtocolLogDiagnostics
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.WriteProtocolLogDiagnostics);
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x0003B74C File Offset: 0x0003994C
		internal static int HangingSyncHintCacheSize
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.HangingSyncHintCacheSize);
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x0003B758 File Offset: 0x00039958
		internal static TimeSpan HangingSyncHintCacheTimeout
		{
			get
			{
				return GlobalSettings.GetSetting<TimeSpan>(GlobalSettingsSchema.HangingSyncHintCacheTimeout);
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0003B764 File Offset: 0x00039964
		internal static TimeSpan DeviceClassCacheMaxStartDelay
		{
			get
			{
				return GlobalSettings.GetSetting<TimeSpan>(GlobalSettingsSchema.DeviceClassCacheMaxStartDelay);
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0003B770 File Offset: 0x00039970
		internal static int DeviceClassCacheMaxADUploadCount
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.DeviceClassCacheMaxADUploadCount);
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0003B77C File Offset: 0x0003997C
		internal static int DeviceClassCachePerOrgRefreshInterval
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.DeviceClassCachePerOrgRefreshInterval);
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0003B788 File Offset: 0x00039988
		internal static int RequestCacheMaxCount
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.RequestCacheMaxCount);
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0003B794 File Offset: 0x00039994
		internal static int RequestCacheTimeInterval
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.RequestCacheTimeInterval);
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0003B7A0 File Offset: 0x000399A0
		internal static bool SyncLogEnabled
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.SyncLogEnabled);
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0003B7AC File Offset: 0x000399AC
		internal static string SyncLogDirectory
		{
			get
			{
				return GlobalSettings.GetSetting<string>(GlobalSettingsSchema.SyncLogDirectory);
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0003B7B8 File Offset: 0x000399B8
		internal static int DeviceClassPerOrgMaxADCount
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.DeviceClassPerOrgMaxADCount);
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0003B7C4 File Offset: 0x000399C4
		internal static int DeviceClassCacheADCleanupInterval
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.DeviceClassCacheADCleanupInterval);
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0003B7D0 File Offset: 0x000399D0
		internal static string[] DeviceTypesSupportingRedirect
		{
			get
			{
				return GlobalSettings.GetSetting<string[]>(GlobalSettingsSchema.DeviceTypesSupportingRedirect);
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0003B7DC File Offset: 0x000399DC
		internal static string MDMActivationUrl
		{
			get
			{
				return GlobalSettings.GetSetting<string>(GlobalSettingsSchema.MdmActivationUrl);
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0003B7E8 File Offset: 0x000399E8
		internal static Uri MDMComplianceStatusUrl
		{
			get
			{
				return GlobalSettings.GetSetting<Uri>(GlobalSettingsSchema.MdmComplianceStatusUrl);
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0003B7F4 File Offset: 0x000399F4
		internal static Uri MDMEnrollmentUrl
		{
			get
			{
				return GlobalSettings.GetSetting<Uri>(GlobalSettingsSchema.MdmEnrollmentUrl);
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0003B800 File Offset: 0x00039A00
		internal static Uri MdmEnrollmentUrlWithBasicSteps
		{
			get
			{
				return GlobalSettings.GetSetting<Uri>(GlobalSettingsSchema.MdmEnrollmentUrlWithBasicSteps);
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0003B80C File Offset: 0x00039A0C
		internal static string MdmActivationUrlWithBasicSteps
		{
			get
			{
				return GlobalSettings.GetSetting<string>(GlobalSettingsSchema.MdmActivationUrlWithBasicSteps);
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x0003B818 File Offset: 0x00039A18
		internal static string ADRegistrationServiceUrl
		{
			get
			{
				return GlobalSettings.GetSetting<string>(GlobalSettingsSchema.ADRegistrationServiceUrl);
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0003B824 File Offset: 0x00039A24
		internal static List<string> DeviceTypesWithBasicMDMNotification
		{
			get
			{
				return GlobalSettings.GetSetting<List<string>>(GlobalSettingsSchema.DeviceTypesWithBasicMDMNotification);
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x0003B830 File Offset: 0x00039A30
		internal static List<string> DeviceTypesToParseOSVersion
		{
			get
			{
				return GlobalSettings.GetSetting<List<string>>(GlobalSettingsSchema.DeviceTypesToParseOSVersion);
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0003B83C File Offset: 0x00039A3C
		internal static TimeSpan NegativeDeviceStatusCacheExpirationInterval
		{
			get
			{
				return GlobalSettings.GetSetting<TimeSpan>(GlobalSettingsSchema.NegativeDeviceStatusCacheExpirationInterval);
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0003B848 File Offset: 0x00039A48
		internal static TimeSpan DeviceStatusCacheExpirationInterval
		{
			get
			{
				return GlobalSettings.GetSetting<TimeSpan>(GlobalSettingsSchema.DeviceStatusCacheExpirationInterval);
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0003B854 File Offset: 0x00039A54
		internal static bool DisableDeviceHealthStatusCache
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.DisableAadClientCache);
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x0003B860 File Offset: 0x00039A60
		internal static bool DisableAadClientCache
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.DisableAadClientCache);
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0003B86C File Offset: 0x00039A6C
		internal static List<string> ValidSingleNamespaceUrls
		{
			get
			{
				return GlobalSettings.GetSetting<List<string>>(GlobalSettingsSchema.ValidSingleNamespaceUrls);
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0003B878 File Offset: 0x00039A78
		internal static int MaxRetrievedItems
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MaxRetrievedItems);
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x0003B884 File Offset: 0x00039A84
		internal static int MaxNoOfItemsMove
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MaxNoOfItemsMove);
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0003B890 File Offset: 0x00039A90
		internal static int MaxWindowSize
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MaxWindowSize);
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0003B89C File Offset: 0x00039A9C
		internal static TimeSpan BudgetBackOffMinThreshold
		{
			get
			{
				return GlobalSettings.GetSetting<TimeSpan>(GlobalSettingsSchema.BudgetBackOffMinThreshold);
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0003B8A8 File Offset: 0x00039AA8
		internal static double AutoblockBackOffMediumThreshold
		{
			get
			{
				return GlobalSettings.GetSetting<double>(GlobalSettingsSchema.AutoblockBackOffMediumThreshold);
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0003B8B4 File Offset: 0x00039AB4
		internal static double AutoblockBackOffHighThreshold
		{
			get
			{
				return GlobalSettings.GetSetting<double>(GlobalSettingsSchema.AutoblockBackOffHighThreshold);
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0003B8C0 File Offset: 0x00039AC0
		internal static int MaxNumberOfClientOperations
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MaxNumberOfClientOperations);
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x0003B8CC File Offset: 0x00039ACC
		internal static int MinRedirectProtocolVersion
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MinRedirectProtocolVersion);
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0003B8D8 File Offset: 0x00039AD8
		internal static TimeSpan ADCacheRefreshInterval
		{
			get
			{
				return GlobalSettings.GetSetting<TimeSpan>(GlobalSettingsSchema.ADCacheRefreshInterval);
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0003B8E4 File Offset: 0x00039AE4
		internal static TimeSpan MaxCleanUpExecutionTime
		{
			get
			{
				return GlobalSettings.GetSetting<TimeSpan>(GlobalSettingsSchema.MaxCleanUpExecutionTime);
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0003B8F0 File Offset: 0x00039AF0
		internal static TimeSpan VdirCacheTimeoutSeconds
		{
			get
			{
				return GlobalSettings.GetSetting<TimeSpan>(GlobalSettingsSchema.VdirCacheTimeoutSeconds);
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0003B8FC File Offset: 0x00039AFC
		internal static TimeSpan EventQueuePollingInterval
		{
			get
			{
				return GlobalSettings.GetSetting<TimeSpan>(GlobalSettingsSchema.EventQueuePollingInterval);
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0003B908 File Offset: 0x00039B08
		internal static bool AllowInternalUntrustedCerts
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.AllowInternalUntrustedCerts);
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0003B914 File Offset: 0x00039B14
		internal static bool AllowProxyingWithoutSsl
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.AllowProxyingWithoutSsl);
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0003B920 File Offset: 0x00039B20
		internal static int HDPhotoCacheMaxSize
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.HDPhotoCacheMaxSize);
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0003B92C File Offset: 0x00039B2C
		internal static TimeSpan MaxRequestExecutionTime
		{
			get
			{
				return GlobalSettings.GetSetting<TimeSpan>(GlobalSettingsSchema.MaxRequestExecutionTime);
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0003B938 File Offset: 0x00039B38
		internal static TimeSpan HDPhotoCacheExpirationTimeOut
		{
			get
			{
				return GlobalSettings.GetSetting<TimeSpan>(GlobalSettingsSchema.HDPhotoCacheExpirationTimeOut);
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0003B944 File Offset: 0x00039B44
		internal static UserPhotoResolution HDPhotoDefaultSupportedResolution
		{
			get
			{
				return GlobalSettings.GetSetting<UserPhotoResolution>(GlobalSettingsSchema.HDPhotoDefaultSupportedResolution);
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0003B950 File Offset: 0x00039B50
		internal static int HDPhotoMaxNumberOfRequestsToProcess
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.HDPhotoMaxNumberOfRequestsToProcess);
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0003B95C File Offset: 0x00039B5C
		internal static int BackOffErrorWindow
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.BackOffErrorWindow);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0003B968 File Offset: 0x00039B68
		internal static int BackOffThreshold
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.BackOffThreshold);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x0003B974 File Offset: 0x00039B74
		internal static int BackOffTimeOut
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.BackOffTimeOut);
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0003B980 File Offset: 0x00039B80
		internal static string BadItemEmailToText
		{
			get
			{
				return GlobalSettings.GetSetting<string>(GlobalSettingsSchema.BadItemEmailToText);
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x0003B98C File Offset: 0x00039B8C
		internal static bool BadItemIncludeEmailToText
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.BadItemIncludeEmailToText);
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x0003B998 File Offset: 0x00039B98
		internal static bool BadItemIncludeStackTrace
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.BadItemIncludeStackTrace);
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0003B9A4 File Offset: 0x00039BA4
		internal static bool BlockLegacyMailboxes
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.BlockLegacyMailboxes);
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0003B9B0 File Offset: 0x00039BB0
		internal static bool SkipAzureADCall
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.SkipAzureADCall);
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x0003B9BC File Offset: 0x00039BBC
		internal static bool BlockNewMailboxes
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.BlockNewMailboxes);
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x0003B9C8 File Offset: 0x00039BC8
		internal static string BootstrapCABForWM61HostingURL
		{
			get
			{
				return GlobalSettings.GetSetting<string>(GlobalSettingsSchema.BootstrapCABForWM61HostingURL);
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x0003B9D4 File Offset: 0x00039BD4
		internal static string MobileUpdateInformationURL
		{
			get
			{
				return GlobalSettings.GetSetting<string>(GlobalSettingsSchema.MobileUpdateInformationURL);
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x0003B9E0 File Offset: 0x00039BE0
		internal static int BootstrapMailDeliveryDelay
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.BootstrapMailDeliveryDelay);
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x0003B9EC File Offset: 0x00039BEC
		internal static bool DisableCaching
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.DisableCaching);
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x0003B9F8 File Offset: 0x00039BF8
		internal static bool EnableCredentialRequest
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.EnableCredentialRequest);
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x0003BA04 File Offset: 0x00039C04
		internal static bool EnableMailboxLoggingVerboseMode
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.EnableMailboxLoggingVerboseMode);
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0003BA10 File Offset: 0x00039C10
		internal static int HeartbeatAlertThreshold
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.HeartbeatAlertThreshold);
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x0003BA1C File Offset: 0x00039C1C
		internal static int HeartbeatSampleSize
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.HeartbeatSampleSize);
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x0003BA28 File Offset: 0x00039C28
		internal static TimeSpan ADCacheExpirationTimeout
		{
			get
			{
				return GlobalSettings.GetSetting<TimeSpan>(GlobalSettingsSchema.ADCacheExpirationTimeout);
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x0003BA34 File Offset: 0x00039C34
		internal static int ADCacheMaxOrgCount
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.ADCacheMaxOrgCount);
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x0003BA40 File Offset: 0x00039C40
		internal static TimeSpan MailboxSearchTimeout
		{
			get
			{
				return GlobalSettings.GetSetting<TimeSpan>(GlobalSettingsSchema.MailboxSearchTimeout);
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0003BA4C File Offset: 0x00039C4C
		public static int MailboxSessionCacheInitialSize
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MailboxSessionCacheInitialSize);
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x0003BA58 File Offset: 0x00039C58
		public static int MailboxSessionCacheMaxSize
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MailboxSessionCacheMaxSize);
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0003BA64 File Offset: 0x00039C64
		public static TimeSpan MailboxSessionCacheTimeout
		{
			get
			{
				return GlobalSettings.GetSetting<TimeSpan>(GlobalSettingsSchema.MailboxSessionCacheTimeout);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x0003BA70 File Offset: 0x00039C70
		internal static TimeSpan MailboxSearchTimeoutNoContentIndexing
		{
			get
			{
				return GlobalSettings.GetSetting<TimeSpan>(GlobalSettingsSchema.MailboxSearchTimeoutNoContentIndexing);
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x0003BA7C File Offset: 0x00039C7C
		internal static int MaxClientSentBadItems
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MaxClientSentBadItems);
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x0003BA88 File Offset: 0x00039C88
		internal static int MaxCollectionsToLog
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MaxCollectionsToLog);
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0003BA94 File Offset: 0x00039C94
		internal static int MaxDocumentDataSize
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MaxDocumentDataSize);
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x0003BAA0 File Offset: 0x00039CA0
		internal static int MaxDocumentLibrarySearchResults
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MaxDocumentLibrarySearchResults);
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0003BAAC File Offset: 0x00039CAC
		internal static int MaxGALSearchResults
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MaxGALSearchResults);
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x0003BAB8 File Offset: 0x00039CB8
		internal static HeartBeatInterval HeartbeatInterval
		{
			get
			{
				return GlobalSettings.GetSetting<HeartBeatInterval>(GlobalSettingsSchema.HeartBeatInterval);
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x0003BAC4 File Offset: 0x00039CC4
		internal static int MaxMailboxSearchResults
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MaxMailboxSearchResults);
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x0003BAD0 File Offset: 0x00039CD0
		internal static int MaxNumOfFolders
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MaxNumOfFolders);
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x0003BADC File Offset: 0x00039CDC
		internal static int MaxRequestsQueued
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MaxRequestsQueued);
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x0003BAE8 File Offset: 0x00039CE8
		internal static int MaxSizeOfMailboxLog
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MaxSizeOfMailboxLog);
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x0003BAF4 File Offset: 0x00039CF4
		internal static int MaxNoOfPartnershipToAutoClean
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MaxNoOfPartnershipToAutoClean);
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x0003BB00 File Offset: 0x00039D00
		internal static int MaxSMimeADDistributionListExpansion
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MaxSMimeADDistributionListExpansion);
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0003BB0C File Offset: 0x00039D0C
		internal static bool IrmEnabled
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.IrmEnabled);
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x0003BB18 File Offset: 0x00039D18
		internal static int MaxRmsTemplates
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MaxRmsTemplates);
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x0003BB24 File Offset: 0x00039D24
		internal static TimeSpan NegativeRmsTemplateCacheExpirationInterval
		{
			get
			{
				return GlobalSettings.GetSetting<TimeSpan>(GlobalSettingsSchema.NegativeRmsTemplateCacheExpirationInterval);
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x0003BB30 File Offset: 0x00039D30
		internal static int MaxWorkerThreadsPerProc
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MaxWorkerThreadsPerProc);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0003BB3C File Offset: 0x00039D3C
		internal static int NumOfQueuedMailboxLogEntries
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.NumOfQueuedMailboxLogEntries);
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x0003BB48 File Offset: 0x00039D48
		internal static int ProxyConnectionPoolConnectionLimit
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.ProxyConnectionPoolConnectionLimit);
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x0003BB54 File Offset: 0x00039D54
		internal static TimeSpan ProxyHandlerLongTimeout
		{
			get
			{
				return GlobalSettings.GetSetting<TimeSpan>(GlobalSettingsSchema.ProxyHandlerLongTimeout);
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x0003BB60 File Offset: 0x00039D60
		internal static TimeSpan ProxyHandlerShortTimeout
		{
			get
			{
				return GlobalSettings.GetSetting<TimeSpan>(GlobalSettingsSchema.ProxyHandlerShortTimeout);
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0003BB6C File Offset: 0x00039D6C
		internal static int EarlyCompletionTolerance
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.EarlyCompletionTolerance);
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0003BB78 File Offset: 0x00039D78
		internal static int EarlyWakeupBufferTime
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.EarlyWakeupBufferTime);
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0003BB84 File Offset: 0x00039D84
		internal static string[] ProxyHeaders
		{
			get
			{
				return GlobalSettings.GetSetting<string[]>(GlobalSettingsSchema.ProxyHeaders);
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0003BB90 File Offset: 0x00039D90
		internal static string ProxyVirtualDirectory
		{
			get
			{
				return GlobalSettings.GetSetting<string>(GlobalSettingsSchema.ProxyVirtualDirectory);
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x0003BB9C File Offset: 0x00039D9C
		internal static string SchemaDirectory
		{
			get
			{
				return GlobalSettings.GetSetting<string>(GlobalSettingsSchema.SchemaDirectory);
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0003BBA8 File Offset: 0x00039DA8
		internal static bool SendWatsonReport
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.SendWatsonReport);
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0003BBB4 File Offset: 0x00039DB4
		internal static bool FullServerVersion
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.FullServerVersion);
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0003BBC0 File Offset: 0x00039DC0
		internal static int ErrorResponseDelay
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.ErrorResponseDelay);
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0003BBCC File Offset: 0x00039DCC
		internal static int MaxThrottlingDelay
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MaxThrottlingDelay);
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0003BBD8 File Offset: 0x00039DD8
		internal static List<string> SupportedIPMTypes
		{
			get
			{
				return GlobalSettings.GetSetting<List<string>>(GlobalSettingsSchema.SupportedIPMTypes);
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0003BBE4 File Offset: 0x00039DE4
		internal static MultiValuedProperty<string> RemoteDocumentsAllowedServers
		{
			get
			{
				return GlobalSettings.GetSetting<MultiValuedProperty<string>>(GlobalSettingsSchema.RemoteDocumentsAllowedServers);
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0003BBF0 File Offset: 0x00039DF0
		internal static RemoteDocumentsActions? RemoteDocumentsActionForUnknownServers
		{
			get
			{
				return GlobalSettings.GetSetting<RemoteDocumentsActions?>(GlobalSettingsSchema.RemoteDocumentsActionForUnknownServers);
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0003BBFC File Offset: 0x00039DFC
		internal static MultiValuedProperty<string> RemoteDocumentsBlockedServers
		{
			get
			{
				return GlobalSettings.GetSetting<MultiValuedProperty<string>>(GlobalSettingsSchema.RemoteDocumentsBlockedServers);
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0003BC08 File Offset: 0x00039E08
		internal static MultiValuedProperty<string> RemoteDocumentsInternalDomainSuffixList
		{
			get
			{
				return GlobalSettings.GetSetting<MultiValuedProperty<string>>(GlobalSettingsSchema.RemoteDocumentsInternalDomainSuffixList);
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0003BC14 File Offset: 0x00039E14
		internal static int UpgradeGracePeriod
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.UpgradeGracePeriod);
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0003BC20 File Offset: 0x00039E20
		internal static int DeviceDiscoveryPeriod
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.DeviceDiscoveryPeriod);
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0003BC2C File Offset: 0x00039E2C
		internal static bool Validate
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.SchemaValidate);
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0003BC38 File Offset: 0x00039E38
		internal static bool IsMultiTenancyEnabled
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.IsMultiTenancyEnabled);
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0003BC44 File Offset: 0x00039E44
		internal static bool IsWindowsLiveIDEnabled
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.IsWindowsLiveIDEnabled);
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0003BC50 File Offset: 0x00039E50
		internal static bool IsGCCEnabled
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.IsGCCEnabled);
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0003BC5C File Offset: 0x00039E5C
		internal static bool AreGccStoredSecretKeysValid
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.AreGccStoredSecretKeysValid);
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0003BC68 File Offset: 0x00039E68
		internal static bool IsPartnerHostedOnly
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.IsPartnerHostedOnly);
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x0003BC74 File Offset: 0x00039E74
		internal static string ExternalProxy
		{
			get
			{
				return GlobalSettings.GetSetting<string>(GlobalSettingsSchema.ExternalProxy);
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0003BC80 File Offset: 0x00039E80
		internal static bool ClientAccessRulesLogPeriodicEvent
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.ClientAccessRulesLogPeriodicEvent);
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0003BC8C File Offset: 0x00039E8C
		internal static double ClientAccessRulesLatencyThreshold
		{
			get
			{
				return GlobalSettings.GetSetting<double>(GlobalSettingsSchema.ClientAccessRulesLatencyThreshold);
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0003BC98 File Offset: 0x00039E98
		public static int DeviceBehaviorCacheInitialSize
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.DeviceBehaviorCacheInitialSize);
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0003BCA4 File Offset: 0x00039EA4
		public static int DeviceBehaviorCacheMaxSize
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.DeviceBehaviorCacheMaxSize);
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0003BCB0 File Offset: 0x00039EB0
		public static int DeviceBehaviorCacheTimeout
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.DeviceBehaviorCacheTimeout);
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0003BCBC File Offset: 0x00039EBC
		public static bool WriteActivityContextDiagnostics
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.WriteActivityContextDiagnostics);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0003BCC8 File Offset: 0x00039EC8
		public static bool WriteBudgetDiagnostics
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.WriteBudgetDiagnostics);
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0003BCD4 File Offset: 0x00039ED4
		public static bool OnlyOrganizersCanSendMeetingChanges
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.OnlyOrganizersCanSendMeetingChanges);
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0003BCE0 File Offset: 0x00039EE0
		internal static bool AutoBlockWriteToAd
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.AutoBlockWriteToAd);
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0003BCEC File Offset: 0x00039EEC
		internal static int AutoBlockADWriteDelay
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.AutoBlockADWriteDelay);
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0003BCF8 File Offset: 0x00039EF8
		internal static int ADDataSyncInterval
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.ADDataSyncInterval);
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0003BD04 File Offset: 0x00039F04
		public static bool WriteExceptionDiagnostics
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.WriteExceptionDiagnostics);
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x0003BD10 File Offset: 0x00039F10
		public static bool LogCompressedExceptionDetails
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.LogCompressedExceptionDetails);
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0003BD1C File Offset: 0x00039F1C
		public static bool IncludeRequestInWatson
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.IncludeRequestInWatson);
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0003BD28 File Offset: 0x00039F28
		public static TimeSpan MeetingOrganizerCleanupTime
		{
			get
			{
				return GlobalSettings.GetSetting<TimeSpan>(GlobalSettingsSchema.MeetingOrganizerCleanupTime);
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x0003BD34 File Offset: 0x00039F34
		public static TimeSpan MeetingOrganizerEntryLiveTime
		{
			get
			{
				return GlobalSettings.GetSetting<TimeSpan>(GlobalSettingsSchema.MeetingOrganizerEntryLiveTime);
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x0003BD40 File Offset: 0x00039F40
		public static bool TimeTrackingEnabled
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.TimeTrackingEnabled);
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x0003BD4C File Offset: 0x00039F4C
		internal static GlobalSettings.DirectPushEnabled AllowDirectPush
		{
			get
			{
				return GlobalSettings.GetSetting<GlobalSettings.DirectPushEnabled>(GlobalSettingsSchema.AllowDirectPush);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x0003BD58 File Offset: 0x00039F58
		internal static int MinGALSearchLength
		{
			get
			{
				return GlobalSettings.GetSetting<int>(GlobalSettingsSchema.MinGALSearchLength);
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0003BD64 File Offset: 0x00039F64
		internal static bool EnableV160
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.EnableV160);
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x0003BD70 File Offset: 0x00039F70
		internal static TimeSpan MaxBackOffDuration
		{
			get
			{
				return GlobalSettings.GetSetting<TimeSpan>(GlobalSettingsSchema.MaxBackoffDuration);
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0003BD7C File Offset: 0x00039F7C
		internal static bool AddBackOffReasonHeader
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.AddBackOffReasonHeader);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0003BD88 File Offset: 0x00039F88
		internal static bool AllowFlightingOverrides
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.AllowFlightingOverrides);
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0003BD94 File Offset: 0x00039F94
		internal static bool GetGoidFromCalendarItemForMeetingResponse
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.GetGoidFromCalendarItemForMeetingResponse);
			}
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0003BDAD File Offset: 0x00039FAD
		internal static T GetSetting<T>(GlobalSettingsPropertyDefinition propDef)
		{
			return (T)((object)GlobalSettings.settings.GetOrAdd(propDef, (GlobalSettingsPropertyDefinition propDef2) => GlobalSettings.LoadSetting<T>(propDef2)));
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0003BDD3 File Offset: 0x00039FD3
		internal static object GetSetting(GlobalSettingsPropertyDefinition propDef)
		{
			return GlobalSettings.settings.GetOrAdd(propDef, (GlobalSettingsPropertyDefinition propDef2) => GlobalSettings.LoadSetting(propDef2));
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0003BE00 File Offset: 0x0003A000
		internal static void ForceLoadAllSettings()
		{
			foreach (GlobalSettingsPropertyDefinition propDef in GlobalSettingsSchema.AllProperties)
			{
				GlobalSettings.GetSetting(propDef);
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0003BE4C File Offset: 0x0003A04C
		internal static bool DisableCharsetDetectionInCopyMessageContents
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.DisableCharsetDetectionInCopyMessageContents);
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0003BE58 File Offset: 0x0003A058
		internal static bool UseOAuthMasterSidForSecurityContext
		{
			get
			{
				return GlobalSettings.GetSetting<bool>(GlobalSettingsSchema.UseOAuthMasterSidForSecurityContext);
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0003BE64 File Offset: 0x0003A064
		private static object LoadSetting(GlobalSettingsPropertyDefinition propDef)
		{
			object obj = propDef.Getter(propDef);
			PropertyConstraintViolationError propertyConstraintViolationError = propDef.Validate(obj);
			if (propertyConstraintViolationError != null)
			{
				AirSyncDiagnostics.LogEvent(AirSyncEventLogConstants.Tuple_GlobalValueOutOfRange, new string[]
				{
					propDef.Name,
					propDef.ReadConstraint.ToString(),
					obj.ToString(),
					propDef.DefaultValue.ToString()
				});
				return propDef.DefaultValue;
			}
			return obj;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0003BED4 File Offset: 0x0003A0D4
		private static T LoadSetting<T>(GlobalSettingsPropertyDefinition propDef)
		{
			if (propDef.Type != typeof(T))
			{
				throw new ArgumentException(string.Format("Property {0} is not of the correct type {1}, but is a {2} property", propDef.Name, typeof(T).Name, propDef.Type.Name));
			}
			object obj = propDef.Getter(propDef);
			PropertyConstraintViolationError propertyConstraintViolationError = propDef.Validate(obj);
			if (propertyConstraintViolationError != null)
			{
				AirSyncDiagnostics.LogEvent(AirSyncEventLogConstants.Tuple_GlobalValueOutOfRange, new string[]
				{
					propDef.Name,
					propDef.ReadConstraint.ToString(),
					obj.ToString(),
					propDef.DefaultValue.ToString()
				});
				return (T)((object)propDef.DefaultValue);
			}
			return (T)((object)obj);
		}

		// Token: 0x04000644 RID: 1604
		internal const string CertificateValidationComponentId = "AirSync";

		// Token: 0x04000645 RID: 1605
		private const string CasSyncLogsDirectory = "CasSyncLogs";

		// Token: 0x04000646 RID: 1606
		private const string SyncLogComponentName = "AirSync";

		// Token: 0x04000647 RID: 1607
		private static ConcurrentDictionary<GlobalSettingsPropertyDefinition, object> settings = new ConcurrentDictionary<GlobalSettingsPropertyDefinition, object>();

		// Token: 0x04000648 RID: 1608
		private static SyncLog syncLog;

		// Token: 0x04000649 RID: 1609
		private static object syncLogCreationLock = new object();

		// Token: 0x020000BF RID: 191
		public enum DirectPushEnabled
		{
			// Token: 0x0400064D RID: 1613
			Off,
			// Token: 0x0400064E RID: 1614
			On,
			// Token: 0x0400064F RID: 1615
			OnWithAddressCheck
		}

		// Token: 0x020000C0 RID: 192
		// (Invoke) Token: 0x06000B49 RID: 2889
		public delegate bool OnLoadConfigSettingDelegate(GlobalSettingsPropertyDefinition propDef, out string value);
	}
}
