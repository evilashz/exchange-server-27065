using System;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Common.Registry;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000082 RID: 130
	internal static class RegistryParameters
	{
		// Token: 0x06000369 RID: 873 RVA: 0x0000DDF0 File Offset: 0x0000BFF0
		internal static void TestLoadRegistryParameters()
		{
			RegistryParameters.s_parameters.LoadInitialValues();
			RegistryParameters.TestLoadRegistryValues();
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000DE01 File Offset: 0x0000C001
		internal static int BcsCheckToDisable
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("BcsCheckToDisable");
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000DE12 File Offset: 0x0000C012
		internal static int ListMdbStatusRpcTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ListMdbStatusRpcTimeoutInSec");
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000DE23 File Offset: 0x0000C023
		internal static int MdbStatusFetcherServerUpTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MdbStatusFetcherServerUpTimeoutInSec");
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000DE34 File Offset: 0x0000C034
		internal static ExDateTime StoreKillBugcheckDisabledTime
		{
			get
			{
				return DateTimeHelper.ToLocalExDateTime(RegistryParameters.s_parameters.GetValue<DateTime>("StoreKillBugcheckDisabledTime"));
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000DE4A File Offset: 0x0000C04A
		internal static ClusterNotifyFlags NetworkClusterNotificationMask
		{
			get
			{
				RegistryParameters.LoadRegistryValues();
				return RegistryParameters.m_networkClusterNotificationMask;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000DE56 File Offset: 0x0000C056
		internal static long BootTimeCookie
		{
			get
			{
				RegistryParameters.LoadRegistryValues();
				return RegistryParameters.m_bootTimeCookie;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000DE62 File Offset: 0x0000C062
		internal static long BootTimeFswCookie
		{
			get
			{
				RegistryParameters.LoadRegistryValues();
				return RegistryParameters.m_bootTimeFswCookie;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000DE6E File Offset: 0x0000C06E
		internal static WatchdogAction FailureItemWatchdogAction
		{
			get
			{
				RegistryParameters.LoadRegistryValues();
				return RegistryParameters.m_failureItemWatchdogAction;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000DE7A File Offset: 0x0000C07A
		internal static bool EnableKernelWatchdogTimer
		{
			get
			{
				RegistryParameters.LoadRegistryValues();
				return RegistryParameters.m_enableKernelWatchdogTimer;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000DE86 File Offset: 0x0000C086
		internal static bool IsTransientFailoverSuppressionEnabled
		{
			get
			{
				return RegistryParameters.TransientFailoverSuppressionDelayInSec > 0;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000DEBC File Offset: 0x0000C0BC
		internal static int SelfDismountAllDelayInSec
		{
			get
			{
				int delayInSec = RegistryParameters.TransientFailoverSuppressionDelayInSec + 60;
				RegistryParameters.TryGetRegistryParameters("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", delegate(IRegistryKey key)
				{
					delayInSec = (int)key.GetValue("SelfDismountAllDelayInSec", delayInSec);
				});
				return delayInSec;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000DF20 File Offset: 0x0000C120
		internal static ExDateTime AmRemoteSiteCheckDisabledTime
		{
			get
			{
				string disabledTime = null;
				RegistryParameters.TryGetRegistryParameters("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", delegate(IRegistryKey key)
				{
					disabledTime = (string)key.GetValue(RegistryParameters.AmRemoteSiteCheckDisabledTimeKey, string.Empty);
				});
				ExDateTime result;
				if (ExDateTime.TryParse(disabledTime, out result))
				{
					return result;
				}
				return ExDateTime.MinValue;
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000DF66 File Offset: 0x0000C166
		private static void LoadRegistryValues()
		{
			RegistryParameters.LoadRegistryValues(false);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000DF6E File Offset: 0x0000C16E
		private static void TestLoadRegistryValues()
		{
			RegistryParameters.LoadRegistryValues(true);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000DF78 File Offset: 0x0000C178
		internal static void TryGetRegistryParameters(string registryKey, Action<IRegistryKey> operation)
		{
			Exception ex;
			using (IRegistryKey registryKey2 = SharedDependencies.RegistryKeyProvider.TryOpenKey(registryKey, ref ex))
			{
				if (ex == null)
				{
					operation(registryKey2);
				}
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000DFBC File Offset: 0x0000C1BC
		private static WatchdogAction GetFailureItemWatchdogAction(IRegistryKey key)
		{
			RegistryParameters.m_failureItemWatchdogAction = (WatchdogAction)((int)key.GetValue("FailureItemWatchdogAction", (int)RegistryParameters.m_failureItemWatchdogAction));
			return RegistryParameters.m_failureItemWatchdogAction;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000DFEB File Offset: 0x0000C1EB
		internal static WatchdogAction GetFailureItemWatchdogAction()
		{
			RegistryParameters.TryGetRegistryParameters("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", delegate(IRegistryKey key)
			{
				RegistryParameters.GetFailureItemWatchdogAction(key);
			});
			return RegistryParameters.m_failureItemWatchdogAction;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000E048 File Offset: 0x0000C248
		internal static int GetApiLatencyInSec(string apiName)
		{
			string propertyName = "ApiDelayLatencyInSec_" + apiName;
			int latency = 0;
			RegistryParameters.TryGetRegistryParameters("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", delegate(IRegistryKey key)
			{
				latency = (int)key.GetValue(propertyName, latency);
			});
			return latency;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000E0BC File Offset: 0x0000C2BC
		internal static int GetApiSimulatedErrorCode(string apiName)
		{
			string propertyName = "ApiSimulatedErrorCode_" + apiName;
			int errorCode = 0;
			RegistryParameters.TryGetRegistryParameters("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", delegate(IRegistryKey key)
			{
				errorCode = (int)key.GetValue(propertyName, errorCode);
			});
			return errorCode;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000E103 File Offset: 0x0000C303
		internal static bool GetIsKillClusterServiceOnClusApiHang()
		{
			return RegistryParameters.IsKillClusterServiceOnClusApiHang;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000E10A File Offset: 0x0000C30A
		internal static bool GetIsLogApiLatencyFailure()
		{
			return RegistryParameters.IsLogApiLatencyFailure;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000E111 File Offset: 0x0000C311
		internal static int GetTestWithFakeNetwork()
		{
			return RegistryParameters.TestWithFakeNetwork;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000E118 File Offset: 0x0000C318
		internal static bool IsManagedStore()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS"))
			{
				if (registryKey != null)
				{
					string text = registryKey.GetValue("ImagePath") as string;
					if (text != null)
					{
						text = text.Trim(new char[]
						{
							'"'
						});
						return text.EndsWith("microsoft.exchange.store.exe", StringComparison.OrdinalIgnoreCase) || text.EndsWith("microsoft.exchange.store.service.exe", StringComparison.OrdinalIgnoreCase);
					}
				}
			}
			return false;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000E237 File Offset: 0x0000C437
		private static void LoadRegistryValues(bool forceReload)
		{
			if (!forceReload && RegistryParameters.m_gLoadedRegistryValues)
			{
				return;
			}
			RegistryParameters.TryGetRegistryParameters("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", delegate(IRegistryKey key)
			{
				RegistryParameters.m_networkClusterNotificationMask = (ClusterNotifyFlags)key.GetValue("NetworkClusterNotificationMask", RegistryParameters.m_networkClusterNotificationMask);
				RegistryParameters.m_bootTimeCookie = RegistryParameters.GetRegistryParameterLong(key, "BootTimeCookie", RegistryParameters.m_bootTimeCookie, 0L, null);
				RegistryParameters.m_bootTimeFswCookie = RegistryParameters.GetRegistryParameterLong(key, "BootTimeFswCookie", RegistryParameters.m_bootTimeFswCookie, 0L, null);
				RegistryParameters.GetFailureItemWatchdogAction(key);
				RegistryParameters.m_enableKernelWatchdogTimer = ((int)key.GetValue("78341438-9b4a-4554-bbff-fd3ac2b5bbe3", 0) > 0);
				RegistryParameters.m_gLoadedRegistryValues = true;
			});
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000E26C File Offset: 0x0000C46C
		private static int GetRegistryParameterInt(IRegistryKey key, string paramName, int defaultValue, int minimumValue)
		{
			return RegistryParameters.GetRegistryParameterInt(key, paramName, defaultValue, minimumValue, null);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000E28C File Offset: 0x0000C48C
		private static int GetRegistryParameterInt(IRegistryKey key, string paramName, int defaultValue, int minimumValue, int? maximumValue)
		{
			int num = (int)key.GetValue(paramName, defaultValue);
			if (num < minimumValue)
			{
				num = minimumValue;
			}
			else if (maximumValue != null && num > maximumValue.Value)
			{
				num = maximumValue.Value;
			}
			return num;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000E2D0 File Offset: 0x0000C4D0
		private static long GetRegistryParameterLong(IRegistryKey key, string paramName, long defaultValue, long minimumValue, long? maximumValue)
		{
			object value = key.GetValue(paramName, defaultValue);
			long num = (long)value;
			if (num < minimumValue)
			{
				num = minimumValue;
			}
			else if (maximumValue != null && num > maximumValue.Value)
			{
				num = maximumValue.Value;
			}
			return num;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000E316 File Offset: 0x0000C516
		internal static int AcllDismountOrKillTimeoutInSec2
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AcllDismountOrKillTimeoutInSec2");
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000E327 File Offset: 0x0000C527
		internal static int AcllLockAutoReleaseAfterDurationMs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AcllLockAutoReleaseAfterDurationMs");
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000E338 File Offset: 0x0000C538
		internal static int AcllSuspendLockTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AcllSuspendLockTimeoutInSec");
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000E349 File Offset: 0x0000C549
		internal static int ADConfigRefreshDefaultTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ADConfigRefreshDefaultTimeoutInSec");
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000E35A File Offset: 0x0000C55A
		internal static int AdObjectCacheHitTtlInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AdObjectCacheHitTtlInSec");
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000E36B File Offset: 0x0000C56B
		internal static int AdObjectCacheMissTtlInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AdObjectCacheMissTtlInSec");
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000E37C File Offset: 0x0000C57C
		internal static int ADReplicationSleepInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ADReplicationSleepInSec");
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000E38D File Offset: 0x0000C58D
		internal static int AmConfigObjectDisposeDelayInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AmConfigObjectDisposeDelayInSec");
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000E39E File Offset: 0x0000C59E
		internal static int AmDeferredDatabaseStateRestorerIntervalInMSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AmDeferredDatabaseStateRestorerIntervalInMSec");
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000E3AF File Offset: 0x0000C5AF
		internal static bool AmDisableBatchOperations
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("AmDisableBatchOperations");
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000E3C0 File Offset: 0x0000C5C0
		internal static int AmDismountOrKillTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AmDismountOrKillTimeoutInSec");
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000E3D1 File Offset: 0x0000C5D1
		internal static bool AmEnableCrimsonLoggingPeriodicEventProcessing
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("AmEnableCrimsonLoggingPeriodicEventProcessing");
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000E3E2 File Offset: 0x0000C5E2
		internal static int AmPerfCounterUpdateIntervalInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AmPerfCounterUpdateIntervalInSec");
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000E3F3 File Offset: 0x0000C5F3
		internal static bool AmPeriodicDatabaseAnalyzerEnabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("AmPeriodicDatabaseAnalyzerEnabled");
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000E404 File Offset: 0x0000C604
		internal static int AmPeriodicDatabaseAnalyzerIntervalInMSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AmPeriodicDatabaseAnalyzerIntervalInMSec");
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000E415 File Offset: 0x0000C615
		internal static int AmPeriodicRoleReportingIntervalInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AmPeriodicRoleReportingIntervalInSec");
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000E426 File Offset: 0x0000C626
		internal static int AmRemoteSiteCheckAlertTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AmRemoteSiteCheckAlertTimeoutInSec");
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000E437 File Offset: 0x0000C637
		internal static int AmServerNameCacheTTLInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AmServerNameCacheTTLInSec");
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000E448 File Offset: 0x0000C648
		internal static int AmSystemEventAssertOnHangTimeoutInMSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AmSystemEventAssertOnHangTimeoutInMSec");
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000E459 File Offset: 0x0000C659
		internal static int AmSystemManagerEventWaitTimeoutInMSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AmSystemManagerEventWaitTimeoutInMSec");
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000E46A File Offset: 0x0000C66A
		internal static bool AutoDagUseServerConfiguredProperty
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("AutoDagUseServerConfiguredProperty");
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000E47B File Offset: 0x0000C67B
		internal static int AutoReseedCiBehindBacklog
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedCiBehindBacklog");
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000E48C File Offset: 0x0000C68C
		internal static bool AutoReseedCiBehindDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("AutoReseedCiBehindDisabled");
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000E49D File Offset: 0x0000C69D
		internal static int AutoReseedCiBehindRetryCount
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedCiBehindRetryCount");
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000E4AE File Offset: 0x0000C6AE
		internal static bool AutoReseedCiFailedSuspendedDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("AutoReseedCiFailedSuspendedDisabled");
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000E4BF File Offset: 0x0000C6BF
		internal static int AutoReseedCiMaxConcurrentSeeds
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedCiMaxConcurrentSeeds");
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000E4D0 File Offset: 0x0000C6D0
		internal static int AutoReseedCiPeriodicIntervalInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedCiPeriodicIntervalInSecs");
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000E4E1 File Offset: 0x0000C6E1
		internal static bool AutoReseedCiRebuildFailedSuspendedDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("AutoReseedCiRebuildFailedSuspendedDisabled");
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000E4F2 File Offset: 0x0000C6F2
		internal static int AutoReseedCiRebuildFailedSuspendedPeriodicIntervalInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedCiRebuildFailedSuspendedPeriodicIntervalInSecs");
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000E503 File Offset: 0x0000C703
		internal static int AutoReseedCiCatalogOnUpgradeIntervalInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedCiCatalogOnUpgradeIntervalInSecs");
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000E514 File Offset: 0x0000C714
		internal static int AutoReseedCiRebuildFailedSuspendedSuppressionInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedCiRebuildFailedSuspendedSuppressionInSecs");
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000E525 File Offset: 0x0000C725
		internal static int AutoReseedCiRebuildFailedSuspendedThrottlingIntervalInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedCiRebuildFailedSuspendedThrottlingIntervalInSecs");
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000E536 File Offset: 0x0000C736
		internal static int AutoReseedCiSuppressionInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedCiSuppressionInSecs");
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000E547 File Offset: 0x0000C747
		internal static int AutoReseedCiThrottlingIntervalInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedCiThrottlingIntervalInSecs");
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000E558 File Offset: 0x0000C758
		internal static bool AutoReseedCiUpgradeDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("AutoReseedCiUpgradeDisabled");
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000E569 File Offset: 0x0000C769
		internal static int AutoReseedDbFailedAssignSpareRetryCountMax
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedDbFailedAssignSpareRetryCountMax");
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000E57A File Offset: 0x0000C77A
		internal static int AutoReseedDbFailedInPlaceReseedDelayInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedDbFailedInPlaceReseedDelayInSecs");
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000E58B File Offset: 0x0000C78B
		internal static int AutoReseedDbFailedPeriodicIntervalInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedDbFailedPeriodicIntervalInSecs");
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000E59C File Offset: 0x0000C79C
		internal static int AutoReseedDbFailedReseedRetryCountMax
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedDbFailedReseedRetryCountMax");
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000E5AD File Offset: 0x0000C7AD
		internal static int AutoReseedDbFailedResumeRetryCountMax
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedDbFailedResumeRetryCountMax");
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000E5BE File Offset: 0x0000C7BE
		internal static bool AutoReseedDbFailedSuspendedDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("AutoReseedDbFailedSuspendedDisabled");
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000E5CF File Offset: 0x0000C7CF
		internal static int AutoReseedDbFailedSuspendedPeriodicIntervalInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedDbFailedSuspendedPeriodicIntervalInSecs");
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000E5E0 File Offset: 0x0000C7E0
		internal static int AutoReseedDbFailedSuspendedSuppressionInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedDbFailedSuspendedSuppressionInSecs");
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000E5F1 File Offset: 0x0000C7F1
		internal static int AutoReseedDbFailedSuspendedThrottlingIntervalInSecs_Reseed
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedDbFailedSuspendedThrottlingIntervalInSecs_Reseed");
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000E602 File Offset: 0x0000C802
		internal static int AutoReseedDbFailedSuspendedThrottlingIntervalInSecs_Resume
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedDbFailedSuspendedThrottlingIntervalInSecs_Resume");
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000E613 File Offset: 0x0000C813
		internal static bool AutoReseedDbFailedSuspendedUseNeighborsForDbGroups
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("AutoReseedDbFailedSuspendedUseNeighborsForDbGroups");
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000E624 File Offset: 0x0000C824
		internal static int AutoReseedDbFailedSuspendedWorkflowResetIntervalInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedDbFailedSuspendedWorkflowResetIntervalInSecs");
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000E635 File Offset: 0x0000C835
		internal static bool AutoReseedDbFailedWorkflowDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("AutoReseedDbFailedWorkflowDisabled");
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000E646 File Offset: 0x0000C846
		internal static int AutoReseedDbHealthySuppressionInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedDbHealthySuppressionInSecs");
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000E657 File Offset: 0x0000C857
		internal static int AutoReseedDbMaxConcurrentSeeds
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedDbMaxConcurrentSeeds");
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000E668 File Offset: 0x0000C868
		internal static bool AutoReseedDbNeverMountedDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("AutoReseedDbNeverMountedDisabled");
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000E679 File Offset: 0x0000C879
		internal static int AutoReseedDbNeverMountedThrottlingIntervalInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedDbNeverMountedThrottlingIntervalInSecs");
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000E68A File Offset: 0x0000C88A
		internal static bool AutoReseedManagerDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("AutoReseedManagerDisabled");
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000E69B File Offset: 0x0000C89B
		internal static int AutoReseedManagerPollerIntervalInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedManagerPollerIntervalInSecs");
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000E6AC File Offset: 0x0000C8AC
		internal static int AutoReseedVolumeAssignmentCacheTTLInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoReseedVolumeAssignmentCacheTTLInSecs");
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000E6BD File Offset: 0x0000C8BD
		internal static int BCSGetCopyStatusRPCTimeoutInMSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("BCSGetCopyStatusRPCTimeoutInMSec");
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000E6CE File Offset: 0x0000C8CE
		internal static int BcsTotalQueueMaxThreshold
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("BcsTotalQueueMaxThreshold");
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000E6DF File Offset: 0x0000C8DF
		internal static int CheckCatalogReadyIntervalInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("CheckCatalogReadyIntervalInSec");
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000E6F0 File Offset: 0x0000C8F0
		internal static int CICurrentnessThresholdInSeconds
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("CICurrentnessThresholdInSeconds");
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000E701 File Offset: 0x0000C901
		internal static int CISuspendResumeTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("CISuspendResumeTimeoutInSec");
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000E712 File Offset: 0x0000C912
		internal static int ClusApiHangActionLatencyAllowedInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ClusApiHangActionLatencyAllowedInSec");
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000E723 File Offset: 0x0000C923
		internal static int ClusApiHangReportLongLatencyDurationInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ClusApiHangReportLongLatencyDurationInSec");
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000E734 File Offset: 0x0000C934
		internal static int ClusApiLatencyAllowedInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ClusApiLatencyAllowedInSec");
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000E745 File Offset: 0x0000C945
		internal static int ClusdbHungNodesConfirmDurationInMSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ClusdbHungNodesConfirmDurationInMSec");
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000E756 File Offset: 0x0000C956
		internal static int ClusterBatchWriterIntervalInMsec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ClusterBatchWriterIntervalInMsec");
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000E767 File Offset: 0x0000C967
		internal static int ConfigInitializedCheckTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ConfigInitializedCheckTimeoutInSec");
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000E778 File Offset: 0x0000C978
		internal static int ConfigUpdaterTimerIntervalSlow
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ConfigUpdaterTimerIntervalSlow");
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000E789 File Offset: 0x0000C989
		internal static int CopyStatusPollerIntervalInMsec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("CopyStatusPollerIntervalInMsec");
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000E79A File Offset: 0x0000C99A
		internal static int CopyQueueAlertThreshold
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("CopyQueueAlertThreshold");
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000E7AB File Offset: 0x0000C9AB
		internal static bool CopyStatusClientCachingDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("CopyStatusClientCachingDisabled");
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000E7BC File Offset: 0x0000C9BC
		internal static int CorruptLogRequiredRange
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("CorruptLogRequiredRange");
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000E7CD File Offset: 0x0000C9CD
		internal static int CrimsonPeriodicLoggingIntervalInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("CrimsonPeriodicLoggingIntervalInSec");
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000E7DE File Offset: 0x0000C9DE
		internal static int DatabaseCheckInspectorQueueLengthFailedThreshold
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseCheckInspectorQueueLengthFailedThreshold");
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000E7EF File Offset: 0x0000C9EF
		internal static int DatabaseCheckInspectorQueueLengthWarningThreshold
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseCheckInspectorQueueLengthWarningThreshold");
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000E800 File Offset: 0x0000CA00
		internal static int DatabaseHealthCheckAtLeastNCopies
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckAtLeastNCopies");
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000E811 File Offset: 0x0000CA11
		internal static int DatabaseHealthCheckGreenPeriodicIntervalInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckGreenPeriodicIntervalInSec");
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000E822 File Offset: 0x0000CA22
		internal static int DatabaseHealthCheckGreenTransitionSuppressionInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckGreenTransitionSuppressionInSec");
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000E833 File Offset: 0x0000CA33
		internal static int DatabaseHealthCheckRedPeriodicIntervalInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckRedPeriodicIntervalInSec");
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000E844 File Offset: 0x0000CA44
		internal static int DatabaseHealthCheckRedTransitionSuppressionInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckRedTransitionSuppressionInSec");
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000E855 File Offset: 0x0000CA55
		internal static int DatabaseHealthCheckOneCopyGreenTransitionSuppressionInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckOneCopyGreenTransitionSuppressionInSec");
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000E866 File Offset: 0x0000CA66
		internal static string DatabaseHealthCheckSkipDatabasesRegex
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<string>("DatabaseHealthCheckSkipDatabasesRegex");
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000E877 File Offset: 0x0000CA77
		internal static int DatabaseHealthCheckStaleStatusGreenPeriodicIntervalInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckStaleStatusGreenPeriodicIntervalInSec");
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000E888 File Offset: 0x0000CA88
		internal static int DatabaseHealthCheckStaleStatusGreenTransitionSuppressionInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckStaleStatusGreenTransitionSuppressionInSec");
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000E899 File Offset: 0x0000CA99
		internal static int DatabaseHealthCheckStaleStatusRedPeriodicIntervalInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckStaleStatusRedPeriodicIntervalInSec");
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000E8AA File Offset: 0x0000CAAA
		internal static int DatabaseHealthCheckStaleStatusRedTransitionSuppressionInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckStaleStatusRedTransitionSuppressionInSec");
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000E8BB File Offset: 0x0000CABB
		internal static int DatabaseHealthCheckStaleStatusServerLevelMinStaleCopies
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckStaleStatusServerLevelMinStaleCopies");
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000E8CC File Offset: 0x0000CACC
		internal static int DatabaseHealthCheckStaleStatusServerLevelRedTransitionSuppressionInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckStaleStatusServerLevelRedTransitionSuppressionInSec");
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000E8DD File Offset: 0x0000CADD
		internal static int DatabaseHealthCheckTwoCopyGreenPeriodicIntervalInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckTwoCopyGreenPeriodicIntervalInSec");
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000E8EE File Offset: 0x0000CAEE
		internal static int DatabaseHealthCheckTwoCopyGreenTransitionSuppressionInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckTwoCopyGreenTransitionSuppressionInSec");
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000E8FF File Offset: 0x0000CAFF
		internal static int DatabaseHealthCheckTwoCopyRedPeriodicIntervalInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckTwoCopyRedPeriodicIntervalInSec");
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000E910 File Offset: 0x0000CB10
		internal static int DatabaseHealthCheckTwoCopyRedTransitionSuppressionInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckTwoCopyRedTransitionSuppressionInSec");
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000E921 File Offset: 0x0000CB21
		internal static int DatabaseHealthCheckDelayInRaisingDatabasePotentialRedundancyAlertDueToServiceStartInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckDelayInRaisingDatabasePotentialRedundancyAlertDueToServiceStartInSec");
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000E932 File Offset: 0x0000CB32
		internal static int DatabaseHealthCheckCopyConnectedErrorThresholdInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckCopyConnectedErrorThresholdInSec");
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000E943 File Offset: 0x0000CB43
		internal static int DatabaseHealthCheckPotentialOneCopyTotalPassiveCopiesRequiredMin
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckPotentialOneCopyTotalPassiveCopiesRequiredMin");
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000E954 File Offset: 0x0000CB54
		internal static int DatabaseHealthCheckPotentialOneCopyRedTransitionSuppressionInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckPotentialOneCopyRedTransitionSuppressionInSec");
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000E965 File Offset: 0x0000CB65
		internal static int DatabaseHealthCheckPotentialOneCopyGreenTransitionSuppressionInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckPotentialOneCopyGreenTransitionSuppressionInSec");
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000E976 File Offset: 0x0000CB76
		internal static int DatabaseHealthCheckPotentialOneCopyRedPeriodicIntervalInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckPotentialOneCopyRedPeriodicIntervalInSec");
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000E987 File Offset: 0x0000CB87
		internal static int DatabaseHealthCheckServerLevelPotentialOneCopyRedTransitionSuppressionInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckServerLevelPotentialOneCopyRedTransitionSuppressionInSec");
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000E998 File Offset: 0x0000CB98
		internal static int DatabaseHealthCheckServerLevelPotentialOneCopyGreenTransitionSuppressionInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckServerLevelPotentialOneCopyGreenTransitionSuppressionInSec");
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000E9A9 File Offset: 0x0000CBA9
		internal static int DatabaseHealthCheckServerLevelPotentialOneCopyRedPeriodicIntervalInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthCheckServerLevelPotentialOneCopyRedPeriodicIntervalInSec");
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000E9BA File Offset: 0x0000CBBA
		internal static bool DatabaseHealthCheckSiteAlertsDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DatabaseHealthCheckSiteAlertsDisabled");
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000E9CB File Offset: 0x0000CBCB
		internal static bool DatabaseHealthMonitorDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DatabaseHealthMonitorDisabled");
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000E9DC File Offset: 0x0000CBDC
		internal static int DatabaseHealthMonitorPeriodicIntervalInMsec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseHealthMonitorPeriodicIntervalInMsec");
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000E9ED File Offset: 0x0000CBED
		internal static bool DatabaseHealthTrackerDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DatabaseHealthTrackerDisabled");
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000E9FE File Offset: 0x0000CBFE
		internal static bool DatabaseStateTrackerDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DatabaseStateTrackerDisabled");
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000EA0F File Offset: 0x0000CC0F
		internal static int DatabaseStateTrackerInitTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseStateTrackerInitTimeoutInSec");
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000EA20 File Offset: 0x0000CC20
		internal static int DatabaseType
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DatabaseType");
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000EA31 File Offset: 0x0000CC31
		internal static int DbQueueMgrStopLimitInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DbQueueMgrStopLimitInSecs");
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000EA42 File Offset: 0x0000CC42
		internal static bool DisableActivationDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DisableActivationDisabled");
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000EA53 File Offset: 0x0000CC53
		internal static int DisableBugcheckOnHungIo
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DisableBugcheckOnHungIo");
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000EA64 File Offset: 0x0000CC64
		internal static bool DisableDatabaseScan
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DisableDatabaseScan");
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000EA75 File Offset: 0x0000CC75
		internal static bool DisableEdbLogDirectoryCreation
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DisableEdbLogDirectoryCreation");
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000EA86 File Offset: 0x0000CC86
		internal static bool DisableFailureItemProcessing
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DisableFailureItemProcessing");
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000EA97 File Offset: 0x0000CC97
		internal static bool DisableGranularReplication
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DisableGranularReplication");
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000EAA8 File Offset: 0x0000CCA8
		internal static bool DisableGranularReplicationOverflow
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DisableGranularReplicationOverflow");
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000EAB9 File Offset: 0x0000CCB9
		internal static bool DisableISeedStreamingPageReader
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DisableISeedStreamingPageReader");
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000EACA File Offset: 0x0000CCCA
		internal static bool DisableJetFailureItemPublish
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DisableJetFailureItemPublish");
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000EADB File Offset: 0x0000CCDB
		internal static bool DisableNetworkSigning
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DisableNetworkSigning");
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000EAEC File Offset: 0x0000CCEC
		internal static bool DisablePriorityBoost
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DisablePriorityBoost");
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000EAFD File Offset: 0x0000CCFD
		internal static bool DisableSetBrokenFailureItemSuppression
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DisableSetBrokenFailureItemSuppression");
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000EB0E File Offset: 0x0000CD0E
		internal static bool DisableSocketStream
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DisableSocketStream");
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000EB1F File Offset: 0x0000CD1F
		internal static bool DisableSourceLogVerifier
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DisableSourceLogVerifier");
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000EB30 File Offset: 0x0000CD30
		internal static int DiskReclaimerDelayedStartInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DiskReclaimerDelayedStartInSecs");
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000EB41 File Offset: 0x0000CD41
		internal static bool DiskReclaimerDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DiskReclaimerDisabled");
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000EB52 File Offset: 0x0000CD52
		internal static int DiskReclaimerPollerIntervalInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DiskReclaimerPollerIntervalInSecs");
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000EB63 File Offset: 0x0000CD63
		internal static int DiskReclaimerSpareDelayInSecs_Long
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DiskReclaimerSpareDelayInSecs_Long");
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0000EB74 File Offset: 0x0000CD74
		internal static int DiskReclaimerSpareDelayInSecs_Medium
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DiskReclaimerSpareDelayInSecs_Medium");
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000EB85 File Offset: 0x0000CD85
		internal static int DiskReclaimerSpareDelayInSecs_Short
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DiskReclaimerSpareDelayInSecs_Short");
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000EB96 File Offset: 0x0000CD96
		internal static int DumpsterInfoCacheTTLInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DumpsterInfoCacheTTLInSec");
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000EBA7 File Offset: 0x0000CDA7
		internal static int DumpsterRedeliveryEndBufferSeconds
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DumpsterRedeliveryEndBufferSeconds");
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000EBB8 File Offset: 0x0000CDB8
		internal static int DumpsterRedeliveryExpirationDurationInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DumpsterRedeliveryExpirationDurationInSecs");
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000EBC9 File Offset: 0x0000CDC9
		internal static bool DumpsterRedeliveryIgnoreBackoff
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DumpsterRedeliveryIgnoreBackoff");
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000EBDA File Offset: 0x0000CDDA
		internal static int DumpsterRedeliveryManagerTimerIntervalInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DumpsterRedeliveryManagerTimerIntervalInSecs");
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000EBEB File Offset: 0x0000CDEB
		internal static int DumpsterRedeliveryMaxTimeRangeInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DumpsterRedeliveryMaxTimeRangeInSecs");
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000EBFC File Offset: 0x0000CDFC
		internal static int DumpsterRedeliveryPrimaryRetryDurationInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DumpsterRedeliveryPrimaryRetryDurationInSecs");
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000EC0D File Offset: 0x0000CE0D
		internal static int DumpsterRedeliveryStartBufferSeconds
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DumpsterRedeliveryStartBufferSeconds");
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000EC1E File Offset: 0x0000CE1E
		internal static int DumpsterRpcTimeoutInMSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DumpsterRpcTimeoutInMSecs");
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000EC2F File Offset: 0x0000CE2F
		internal static int EnableNetworkChecksums
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("EnableNetworkChecksums");
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000EC40 File Offset: 0x0000CE40
		internal static bool EnableSupportApi
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("EnableSupportApi");
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000EC51 File Offset: 0x0000CE51
		internal static bool EnableV1IncReseed
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("EnableV1IncReseed");
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000EC62 File Offset: 0x0000CE62
		internal static bool EnableVssWriter
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("EnableVssWriter");
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000EC73 File Offset: 0x0000CE73
		internal static bool EnableWatsonDumpOnTooMuchMemory
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("EnableWatsonDumpOnTooMuchMemory");
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000EC84 File Offset: 0x0000CE84
		internal static bool EnforceDbFolderUnderMountPoint
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("EnforceDbFolderUnderMountPoint");
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000EC95 File Offset: 0x0000CE95
		internal static int ExtraReplayLagAllowedMinutes
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ExtraReplayLagAllowedMinutes");
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000ECA6 File Offset: 0x0000CEA6
		internal static int FailureItemHangDetectionIntervalInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("FailureItemHangDetectionIntervalInSec");
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000ECB7 File Offset: 0x0000CEB7
		internal static int FailureItemManagerDatabaseListUpdaterIntervalInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("FailureItemManagerDatabaseListUpdaterIntervalInSec");
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000ECC8 File Offset: 0x0000CEC8
		internal static int FailureItemLocalDatabaseOperationTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("FailureItemLocalDatabaseOperationTimeoutInSec");
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000ECD9 File Offset: 0x0000CED9
		internal static int FailureItemProcessingAllowedLatencyInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("FailureItemProcessingAllowedLatencyInSec");
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000ECEA File Offset: 0x0000CEEA
		internal static int FailureItemProcessingDelayInMSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("FailureItemProcessingDelayInMSec");
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000ECFB File Offset: 0x0000CEFB
		internal static int FailureItemStromCoolingDurationInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("FailureItemStromCoolingDurationInSec");
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000ED0C File Offset: 0x0000CF0C
		internal static int FailureItemWatchdogEngageDurationInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("FailureItemWatchdogEngageDurationInSec");
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000ED1D File Offset: 0x0000CF1D
		internal static int FileInUseRetryLimitInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("FileInUseRetryLimitInSecs");
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000ED2E File Offset: 0x0000CF2E
		internal static bool FilesystemMaintainsOrder
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("FilesystemMaintainsOrder");
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000ED3F File Offset: 0x0000CF3F
		internal static int FullServerReseedRetryIntervalInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("FullServerReseedRetryIntervalInSec");
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000ED50 File Offset: 0x0000CF50
		internal static bool GetActiveCopiesForDatabaseAvailabilityGroupUseCache
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("GetActiveCopiesForDatabaseAvailabilityGroupUseCache");
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000ED61 File Offset: 0x0000CF61
		internal static int GetCopyStatusRpcCacheTTLInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("GetCopyStatusRpcCacheTTLInSec");
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000ED72 File Offset: 0x0000CF72
		internal static int GetCopyStatusServerCachedEntryStaleTimeoutSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("GetCopyStatusServerCachedEntryStaleTimeoutSec");
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000ED83 File Offset: 0x0000CF83
		internal static bool GetCopyStatusServerTimeoutEnabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("GetCopyStatusServerTimeoutEnabled");
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000ED94 File Offset: 0x0000CF94
		internal static int GetCopyStatusServerTimeoutSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("GetCopyStatusServerTimeoutSec");
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000EDA5 File Offset: 0x0000CFA5
		internal static int GetMailboxDatabaseCopyStatusRPCTimeoutInMSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("GetMailboxDatabaseCopyStatusRPCTimeoutInMSec");
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000EDB6 File Offset: 0x0000CFB6
		internal static bool HealthStateTrackerDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("HealthStateTrackerDisabled");
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000EDC7 File Offset: 0x0000CFC7
		internal static int HealthStateTrackerLookupDurationInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("HealthStateTrackerLookupDurationInSec");
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000EDD8 File Offset: 0x0000CFD8
		internal static int HighAvailabilityWebServiceMexPort
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("HighAvailabilityWebServiceMexPort");
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000EDE9 File Offset: 0x0000CFE9
		internal static int HighAvailabilityWebServicePort
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("HighAvailabilityWebServicePort");
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0000EDFA File Offset: 0x0000CFFA
		internal static int HungCopyLimitInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("HungCopyLimitInSec");
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000EE0B File Offset: 0x0000D00B
		internal static bool IgnoreCatalogHealthSetByCI
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("IgnoreCatalogHealthSetByCI");
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000EE1C File Offset: 0x0000D01C
		internal static int IncSeedThreshold
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("IncSeedThreshold");
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000EE2D File Offset: 0x0000D02D
		internal static int IOBufferPoolPreallocationOverride
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("IOBufferPoolPreallocationOverride");
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0000EE3E File Offset: 0x0000D03E
		internal static int IOSizeInBytes
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("IOSizeInBytes");
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000EE4F File Offset: 0x0000D04F
		internal static bool IsApiLatencyTestEnabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("IsApiLatencyTestEnabled");
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000EE60 File Offset: 0x0000D060
		internal static bool IsKillClusterServiceOnClusApiHang
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("IsKillClusterServiceOnClusApiHang");
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000EE71 File Offset: 0x0000D071
		internal static bool IsLogApiLatencyFailure
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("IsLogApiLatencyFailure");
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000EE82 File Offset: 0x0000D082
		internal static bool KillStoreInsteadOfWatsonOnTimeout
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("KillStoreInsteadOfWatsonOnTimeout");
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000EE93 File Offset: 0x0000D093
		internal static int LastLogUpdateThresholdInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LastLogUpdateThresholdInSec");
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000EEA4 File Offset: 0x0000D0A4
		internal static bool ListMdbStatusMonitorDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("ListMdbStatusMonitorDisabled");
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000EEB5 File Offset: 0x0000D0B5
		internal static int ListMdbStatusRecoveryLimitInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ListMdbStatusRecoveryLimitInSec");
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000EEC6 File Offset: 0x0000D0C6
		internal static int ListMdbStatusFailureSuppressionWindowInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ListMdbStatusFailureSuppressionWindowInSec");
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000EED7 File Offset: 0x0000D0D7
		internal static int LogCopierHungIoLimitInMsec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogCopierHungIoLimitInMsec");
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		internal static int LogCopierStalledToFailedThresholdInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogCopierStalledToFailedThresholdInSecs");
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000EEF9 File Offset: 0x0000D0F9
		internal static int LogCopyBufferedIo
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogCopyBufferedIo");
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000EF0A File Offset: 0x0000D10A
		internal static int LogCopyDelayInMsec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogCopyDelayInMsec");
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000EF1B File Offset: 0x0000D11B
		internal static int LogCopyNetworkTransferSize
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogCopyNetworkTransferSize");
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0000EF2C File Offset: 0x0000D12C
		internal static int LogCopyPull
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogCopyPull");
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000EF3D File Offset: 0x0000D13D
		internal static int LogInspectorDelayInMsec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogInspectorDelayInMsec");
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000EF4E File Offset: 0x0000D14E
		internal static int LogInspectorReadSize
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogInspectorReadSize");
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000EF5F File Offset: 0x0000D15F
		internal static int LogReplayerDelayInMsec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogReplayerDelayInMsec");
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000EF70 File Offset: 0x0000D170
		internal static int LogReplayerIdleStoreRpcIntervalInMSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogReplayerIdleStoreRpcIntervalInMSecs");
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000EF81 File Offset: 0x0000D181
		internal static int LogReplayerMaximumLogsForReplayLag
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogReplayerMaximumLogsForReplayLag");
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000EF92 File Offset: 0x0000D192
		internal static int LogReplayerMaxLogsToScanInOneIteration
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogReplayerMaxLogsToScanInOneIteration");
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000EFA3 File Offset: 0x0000D1A3
		internal static int LogReplayerPauseDurationInMSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogReplayerPauseDurationInMSecs");
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000EFB4 File Offset: 0x0000D1B4
		internal static int LogReplayerResumeThreshold
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogReplayerResumeThreshold");
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000EFC5 File Offset: 0x0000D1C5
		internal static int LogReplayerScanMoreLogsWhenReplayWithinThreshold
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogReplayerScanMoreLogsWhenReplayWithinThreshold");
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000EFD6 File Offset: 0x0000D1D6
		internal static int LogReplayerSuspendThreshold
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogReplayerSuspendThreshold");
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000EFE7 File Offset: 0x0000D1E7
		internal static int LogReplayQueueHighPlayDownDisableSuppressionWindowInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogReplayQueueHighPlayDownDisableSuppressionWindowInSecs");
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0000EFF8 File Offset: 0x0000D1F8
		internal static int LogReplayQueueHighPlayDownEnableSuppressionWindowInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogReplayQueueHighPlayDownEnableSuppressionWindowInSecs");
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000F009 File Offset: 0x0000D209
		internal static int LogShipACLLTimeoutInMsec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogShipACLLTimeoutInMsec");
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0000F01A File Offset: 0x0000D21A
		internal static int LogShipCompressionDisable
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogShipCompressionDisable");
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0000F02B File Offset: 0x0000D22B
		internal static int LogShipTimeoutInMsec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogShipTimeoutInMsec");
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000F03C File Offset: 0x0000D23C
		internal static int LogTruncationExtendedPreservation
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogTruncationExtendedPreservation");
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000F04D File Offset: 0x0000D24D
		internal static bool LogTruncationKeepAllLogsForLagCopy
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("LogTruncationKeepAllLogsForLagCopy");
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0000F05E File Offset: 0x0000D25E
		internal static int LogTruncationOpenContextTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogTruncationOpenContextTimeoutInSec");
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000F06F File Offset: 0x0000D26F
		internal static int LogTruncationTimerDuration
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("LogTruncationTimerDuration");
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000F080 File Offset: 0x0000D280
		internal static int MajorityDecisionRpcTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MajorityDecisionRpcTimeoutInSec");
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000F091 File Offset: 0x0000D291
		internal static int MaxADReplicationWaitInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MaxADReplicationWaitInSec");
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0000F0A2 File Offset: 0x0000D2A2
		internal static int MaxAutoDatabaseMountDial
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MaxAutoDatabaseMountDial");
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0000F0B3 File Offset: 0x0000D2B3
		internal static int MaxBlockModeConsumerDepthInBytes
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MaxBlockModeConsumerDepthInBytes");
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0000F0C4 File Offset: 0x0000D2C4
		internal static int MaximumGCHandleCount
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MaximumGCHandleCount");
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0000F0D5 File Offset: 0x0000D2D5
		internal static int MaximumProcessHandleCount
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MaximumProcessHandleCount");
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0000F0E6 File Offset: 0x0000D2E6
		internal static int MaximumProcessPrivateMemoryMB
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MaximumProcessPrivateMemoryMB");
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0000F0F7 File Offset: 0x0000D2F7
		internal static int MaxLogFilesToSeed
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MaxLogFilesToSeed");
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000F108 File Offset: 0x0000D308
		internal static int MemoryLimitBaseInMB
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MemoryLimitBaseInMB");
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0000F119 File Offset: 0x0000D319
		internal static int MemoryLimitPerDBInMB
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MemoryLimitPerDBInMB");
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000F12A File Offset: 0x0000D32A
		internal static int MaximumProcessThreadCount
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MaximumProcessThreadCount");
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000F13B File Offset: 0x0000D33B
		internal static int MaximumRpcThreadCount
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MaximumRpcThreadCount");
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0000F14C File Offset: 0x0000D34C
		internal static int MdbStatusFetcherServerDownTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MdbStatusFetcherServerDownTimeoutInSec");
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0000F15D File Offset: 0x0000D35D
		internal static int StartupLogScanTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("StartupLogScanTimeoutInSec");
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000F16E File Offset: 0x0000D36E
		internal static int MdbStatusFetcherTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MdbStatusFetcherTimeoutInSec");
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000F17F File Offset: 0x0000D37F
		internal static bool MonitorGCHandleCount
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("MonitorGCHandleCount");
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000F190 File Offset: 0x0000D390
		internal static int MonitoringADConfigManagerIntervalInMsec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MonitoringADConfigManagerIntervalInMsec");
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000F1A1 File Offset: 0x0000D3A1
		internal static int MonitoringADConfigStaleTimeoutLongInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MonitoringADConfigStaleTimeoutLongInSec");
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000F1B2 File Offset: 0x0000D3B2
		internal static int MonitoringADConfigStaleTimeoutShortInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MonitoringADConfigStaleTimeoutShortInSec");
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000F1C3 File Offset: 0x0000D3C3
		internal static int MonitoringADGetConfigTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MonitoringADGetConfigTimeoutInSec");
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000F1D4 File Offset: 0x0000D3D4
		internal static bool MonitoringComponentDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("MonitoringComponentDisabled");
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000F1E5 File Offset: 0x0000D3E5
		internal static int MonitoringDHTInitLastUpdateTimeDiffInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MonitoringDHTInitLastUpdateTimeDiffInSec");
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000F1F6 File Offset: 0x0000D3F6
		internal static int MonitoringDHTMissingObjectCleanupAgeThresholdInDays
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MonitoringDHTMissingObjectCleanupAgeThresholdInDays");
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000F207 File Offset: 0x0000D407
		internal static int MonitoringDHTPeriodicIntervalInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MonitoringDHTPeriodicIntervalInSec");
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000F218 File Offset: 0x0000D418
		internal static int MonitoringDHTPrimaryPeriodicSuppressionInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MonitoringDHTPrimaryPeriodicSuppressionInSec");
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000F229 File Offset: 0x0000D429
		internal static int MonitoringDHTPrimaryPublishPeriodicSuppressionInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MonitoringDHTPrimaryPublishPeriodicSuppressionInSec");
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000F23A File Offset: 0x0000D43A
		internal static int MonitoringDHTPrimaryTransitionSuppressionInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MonitoringDHTPrimaryTransitionSuppressionInSec");
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000F24B File Offset: 0x0000D44B
		internal static int MonitoringWebServicePort
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MonitoringWebServicePort");
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0000F25C File Offset: 0x0000D45C
		internal static int MonitoringWebServiceClientOpenTimeoutInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MonitoringWebServiceClientOpenTimeoutInSecs");
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0000F26D File Offset: 0x0000D46D
		internal static int MonitoringWebServiceClientCloseTimeoutInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MonitoringWebServiceClientCloseTimeoutInSecs");
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000F27E File Offset: 0x0000D47E
		internal static int MonitoringWebServiceClientSendTimeoutInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MonitoringWebServiceClientSendTimeoutInSecs");
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0000F28F File Offset: 0x0000D48F
		internal static int MonitoringWebServiceClientReceiveTimeoutInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MonitoringWebServiceClientReceiveTimeoutInSecs");
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000F2A0 File Offset: 0x0000D4A0
		internal static int MountTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("MountTimeoutInSec");
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0000F2B1 File Offset: 0x0000D4B1
		internal static int NetworkManagerStartupTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("NetworkManagerStartupTimeoutInSec");
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0000F2C2 File Offset: 0x0000D4C2
		internal static int NetworkStatusPollingPeriodInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("NetworkStatusPollingPeriodInSecs");
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0000F2D3 File Offset: 0x0000D4D3
		internal static int NodeActionDelayBetweenIterationsInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("NodeActionDelayBetweenIterationsInSec");
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0000F2E4 File Offset: 0x0000D4E4
		internal static int NodeActionInProgressWaitDurationInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("NodeActionInProgressWaitDurationInSec");
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0000F2F5 File Offset: 0x0000D4F5
		internal static int NodeActionNodeStateJoiningWaitDurationInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("NodeActionNodeStateJoiningWaitDurationInSec");
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0000F306 File Offset: 0x0000D506
		internal static int NumThreadsPerPamDbOperation
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("NumThreadsPerPamDbOperation");
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000F317 File Offset: 0x0000D517
		internal static int OpenClusterTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("OpenClusterTimeoutInSec");
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000F328 File Offset: 0x0000D528
		internal static int OnReplDownConfirmDurationBeforeFailoverInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("OnReplDownConfirmDurationBeforeFailoverInSecs");
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0000F339 File Offset: 0x0000D539
		internal static int OnReplDownDurationBetweenFailoversInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("OnReplDownDurationBetweenFailoversInSecs");
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000F34A File Offset: 0x0000D54A
		internal static int OnReplDownMaxAllowedFailoversAcrossDagInADay
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("OnReplDownMaxAllowedFailoversAcrossDagInADay");
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0000F35B File Offset: 0x0000D55B
		internal static int OnReplDownMaxAllowedFailoversPerNodeInADay
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("OnReplDownMaxAllowedFailoversPerNodeInADay");
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000F36C File Offset: 0x0000D56C
		internal static bool OnReplDownFailoverEnabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("OnReplDownFailoverEnabled");
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0000F37D File Offset: 0x0000D57D
		internal static int PamLastLogRpcTimeoutInMsec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("PamLastLogRpcTimeoutInMsec");
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000F38E File Offset: 0x0000D58E
		internal static int PamLastLogUpdaterIntervalInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("PamLastLogUpdaterIntervalInSec");
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0000F39F File Offset: 0x0000D59F
		internal static int PamMonitorCheckPeriodInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("PamMonitorCheckPeriodInSec");
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
		internal static int PamMonitorMoveClusterGroupTimeout
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("PamMonitorMoveClusterGroupTimeout");
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000F3C1 File Offset: 0x0000D5C1
		internal static int PamMonitorRecoveryDurationInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("PamMonitorRecoveryDurationInSec");
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000F3D2 File Offset: 0x0000D5D2
		internal static int PamMonitorRoleCheckTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("PamMonitorRoleCheckTimeoutInSec");
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000F3E3 File Offset: 0x0000D5E3
		internal static int PamToSamDismountRpcTimeoutMediumInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("PamToSamDismountRpcTimeoutMediumInSec");
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000F3F4 File Offset: 0x0000D5F4
		internal static int PamToSamDismountRpcTimeoutShortInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("PamToSamDismountRpcTimeoutShortInSec");
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0000F405 File Offset: 0x0000D605
		internal static int PerfCounterUpdateIntervalInMSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("PerfCounterUpdateIntervalInMSec");
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000F416 File Offset: 0x0000D616
		internal static int QueryLogRangeTimeoutInMsec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("QueryLogRangeTimeoutInMsec");
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000F427 File Offset: 0x0000D627
		internal static int RegistryMonitorPollingIntervalInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("RegistryMonitorPollingIntervalInSec");
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0000F438 File Offset: 0x0000D638
		internal static int RemoteClusterCallTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("RemoteClusterCallTimeoutInSec");
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0000F449 File Offset: 0x0000D649
		internal static int RemoteDataProviderSelfCheckInterval
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("RemoteDataProviderSelfCheckInterval");
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0000F45A File Offset: 0x0000D65A
		internal static int RemoteRegistryTimeoutInMsec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("RemoteRegistryTimeoutInMsec");
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0000F46B File Offset: 0x0000D66B
		internal static bool ReplayLagManagerDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("ReplayLagManagerDisabled");
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000F47C File Offset: 0x0000D67C
		internal static int ReplayLagManagerDisableLagSuppressionWindowInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ReplayLagManagerDisableLagSuppressionWindowInSecs");
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000F48D File Offset: 0x0000D68D
		internal static int ReplayLagManagerEnableLagSuppressionWindowInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ReplayLagManagerEnableLagSuppressionWindowInSecs");
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000F49E File Offset: 0x0000D69E
		internal static int ReplayLagManagerNumAvailableCopies
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ReplayLagManagerNumAvailableCopies");
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000F4AF File Offset: 0x0000D6AF
		internal static int ReplayLagManagerPollerIntervalInMsec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ReplayLagManagerPollerIntervalInMsec");
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
		internal static int ReplayLagLowSpacePlaydownThresholdInMB
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ReplayLagLowSpacePlaydownThresholdInMB");
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0000F4D1 File Offset: 0x0000D6D1
		internal static int ReplayServiceDiagnosticsIntervalMsec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ReplayServiceDiagnosticsIntervalMsec");
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000F4E2 File Offset: 0x0000D6E2
		internal static int ReplayQueueAlertThreshold
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ReplayQueueAlertThreshold");
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0000F4F3 File Offset: 0x0000D6F3
		internal static int ReplicaInstanceManagerNumThreadsPerDbCopy
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ReplicaInstanceManagerNumThreadsPerDbCopy");
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000F504 File Offset: 0x0000D704
		internal static int ReplicaProgressNumberOfLogsThreshold
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ReplicaProgressNumberOfLogsThreshold");
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0000F515 File Offset: 0x0000D715
		internal static int RpcKillServiceTimeoutInMSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("RpcKillServiceTimeoutInMSec");
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0000F526 File Offset: 0x0000D726
		internal static int SeedCatalogProgressIntervalInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("SeedCatalogProgressIntervalInSec");
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0000F537 File Offset: 0x0000D737
		internal static int SeederInstanceStaleDuration
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("SeederInstanceStaleDuration");
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0000F548 File Offset: 0x0000D748
		internal static int SeedingNetworkTimeoutInMsec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("SeedingNetworkTimeoutInMsec");
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000F559 File Offset: 0x0000D759
		internal static int SeedingNetworkTransferSize
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("SeedingNetworkTransferSize");
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0000F56A File Offset: 0x0000D76A
		internal static int SkipIncReseedPagePatch
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("SkipIncReseedPagePatch");
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0000F57B File Offset: 0x0000D77B
		internal static int SkippedLogsDeleteAfterAgeInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("SkippedLogsDeleteAfterAgeInSecs");
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0000F58C File Offset: 0x0000D78C
		internal static int SkippedLogsDeletionIntervalSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("SkippedLogsDeletionIntervalSecs");
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000F59D File Offset: 0x0000D79D
		internal static int SlowIoThresholdInMs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("SlowIoThresholdInMs");
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0000F5AE File Offset: 0x0000D7AE
		internal static int SpaceMonitorActionSuppressionWindowInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("SpaceMonitorActionSuppressionWindowInSecs");
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000F5BF File Offset: 0x0000D7BF
		internal static bool SpaceMonitorDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("SpaceMonitorDisabled");
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0000F5D0 File Offset: 0x0000D7D0
		internal static int SpaceMonitorCopyQueueThreshold
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("SpaceMonitorCopyQueueThreshold");
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0000F5E1 File Offset: 0x0000D7E1
		internal static int SpaceMonitorReplayQueueThreshold
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("SpaceMonitorReplayQueueThreshold");
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0000F5F2 File Offset: 0x0000D7F2
		internal static int SpaceMonitorLowSpaceThresholdInMB
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("SpaceMonitorLowSpaceThresholdInMB");
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000F603 File Offset: 0x0000D803
		internal static int SpaceMonitorMinHealthyCount
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("SpaceMonitorMinHealthyCount");
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000F614 File Offset: 0x0000D814
		internal static int SpaceMonitorPollerIntervalInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("SpaceMonitorPollerIntervalInSec");
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0000F625 File Offset: 0x0000D825
		internal static int StoreCrashControlCodeAckTimeoutInMSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("StoreCrashControlCodeAckTimeoutInMSec");
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0000F636 File Offset: 0x0000D836
		internal static int StoreKillBugcheckTimeoutInMSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("StoreKillBugcheckTimeoutInMSec");
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0000F647 File Offset: 0x0000D847
		internal static int StoreRpcConnectivityTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("StoreRpcConnectivityTimeoutInSec");
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0000F658 File Offset: 0x0000D858
		internal static int StoreRpcGenericTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("StoreRpcGenericTimeoutInSec");
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000F669 File Offset: 0x0000D869
		internal static int StoreWatsonDumpTimeoutInMSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("StoreWatsonDumpTimeoutInMSec");
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000F67A File Offset: 0x0000D87A
		internal static int SuspendLockTimeoutInMsec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("SuspendLockTimeoutInMsec");
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0000F68B File Offset: 0x0000D88B
		internal static int TcpChannelIdleLimitInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("TcpChannelIdleLimitInSec");
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000F69C File Offset: 0x0000D89C
		internal static int TestDelayCatalogSeedSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("TestDelayCatalogSeedSec");
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000F6AD File Offset: 0x0000D8AD
		internal static int TestDisableWatson
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("TestDisableWatson");
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000F6BE File Offset: 0x0000D8BE
		internal static int TestMemoryLeak
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("TestMemoryLeak");
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0000F6CF File Offset: 0x0000D8CF
		internal static int TestServiceStartupDelay
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("TestServiceStartupDelay");
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0000F6E0 File Offset: 0x0000D8E0
		internal static int TestStoreConnectivityTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("TestStoreConnectivityTimeoutInSec");
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0000F6F1 File Offset: 0x0000D8F1
		internal static int TestWithFakeNetwork
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("TestWithFakeNetwork");
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000F702 File Offset: 0x0000D902
		internal static int TransientFailoverSuppressionDelayInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("TransientFailoverSuppressionDelayInSec");
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000F713 File Offset: 0x0000D913
		internal static bool TreatLogCopyPartnerAsDownlevel
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("TreatLogCopyPartnerAsDownlevel");
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0000F724 File Offset: 0x0000D924
		internal static bool UnboundedDatalossDisableClusterInput
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("UnboundedDatalossDisableClusterInput");
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000F735 File Offset: 0x0000D935
		internal static bool UnboundedDatalossDisableReplicationInput
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("UnboundedDatalossDisableReplicationInput");
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000F746 File Offset: 0x0000D946
		internal static int UnboundedDatalossSafeGuardDurationInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("UnboundedDatalossSafeGuardDurationInSec");
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000F757 File Offset: 0x0000D957
		internal static int WaitForCatalogReadyTimeoutInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("WaitForCatalogReadyTimeoutInSec");
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000F768 File Offset: 0x0000D968
		internal static int WatchDogTimeoutForWatsonDumpInSec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("WatchDogTimeoutForWatsonDumpInSec");
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0000F779 File Offset: 0x0000D979
		internal static bool WatsonOnBlockModeConsumerOverflow
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("WatsonOnBlockModeConsumerOverflow");
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0000F78A File Offset: 0x0000D98A
		internal static bool WcfEnableMexEndpoint
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("WcfEnableMexEndpoint");
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x0000F79B File Offset: 0x0000D99B
		internal static int WcfMaxConcurrentCalls
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("WcfMaxConcurrentCalls");
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0000F7AC File Offset: 0x0000D9AC
		internal static int WcfMaxConcurrentInstances
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("WcfMaxConcurrentInstances");
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x0000F7BD File Offset: 0x0000D9BD
		internal static int WcfMaxConcurrentSessions
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("WcfMaxConcurrentSessions");
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0000F7CE File Offset: 0x0000D9CE
		internal static int ClusdbPeriodicCleanupStartDelayInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ClusdbPeriodicCleanupStartDelayInSecs");
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0000F7DF File Offset: 0x0000D9DF
		internal static int ClusdbPeriodicCleanupIntervalInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("ClusdbPeriodicCleanupIntervalInSecs");
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000F7F0 File Offset: 0x0000D9F0
		internal static bool BitlockerWin8EmptyUsedOnlyDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("BitlockerWin8EmptyUsedOnlyDisabled");
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0000F801 File Offset: 0x0000DA01
		internal static bool BitlockerWin7EmptyFullVolumeDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("BitlockerWin7EmptyFullVolumeDisabled");
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0000F812 File Offset: 0x0000DA12
		internal static bool BitlockerWin8UsedOnlyDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("BitlockerWin8UsedOnlyDisabled");
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0000F823 File Offset: 0x0000DA23
		internal static bool BitlockerFeatureDisabled
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("BitlockerFeatureDisabled");
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0000F834 File Offset: 0x0000DA34
		internal static int DistributedStorePerfTrackerFlushInMs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DistributedStorePerfTrackerFlushInMs");
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0000F845 File Offset: 0x0000DA45
		internal static int DistributedStoreApiExecutionPeriodicLogDurationInMs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DistributedStoreApiExecutionPeriodicLogDurationInMs");
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000F856 File Offset: 0x0000DA56
		internal static bool DistributedStoreIsLogShadowApiResult
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DistributedStoreIsLogShadowApiResult");
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0000F867 File Offset: 0x0000DA67
		internal static bool DistributedStoreIsLogApiSuccess
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DistributedStoreIsLogApiSuccess");
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0000F878 File Offset: 0x0000DA78
		internal static bool DistributedStoreIsLogApiExecutionCallstack
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DistributedStoreIsLogApiExecutionCallstack");
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0000F889 File Offset: 0x0000DA89
		internal static int DistributedStoreShadowMaxAllowedWriteQueueLength
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DistributedStoreShadowMaxAllowedWriteQueueLength");
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000F89A File Offset: 0x0000DA9A
		internal static int DistributedStoreShadowMaxAllowedReadQueueLength
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DistributedStoreShadowMaxAllowedReadQueueLength");
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0000F8AB File Offset: 0x0000DAAB
		internal static int AutoMounterFirstStartupDelayInMsec
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("AutoMounterFirstStartupDelayInMsec");
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000F8BC File Offset: 0x0000DABC
		internal static bool DistributedStoreDisableDualClientMode
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DistributedStoreDisableDualClientMode");
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0000F8CD File Offset: 0x0000DACD
		internal static bool DisableDxStoreManager
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DisableDxStoreManager");
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000F8DE File Offset: 0x0000DADE
		internal static int DistributedStoreConsistencyCheckPeriodicIntervalInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DistributedStoreConsistencyCheckPeriodicIntervalInSecs");
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0000F8EF File Offset: 0x0000DAEF
		internal static int DistributedStoreConsistencyVerifyIntervalInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DistributedStoreConsistencyVerifyIntervalInSecs");
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000F900 File Offset: 0x0000DB00
		internal static int DistributedStoreConsistencyStartupDelayInSecs
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DistributedStoreConsistencyStartupDelayInSecs");
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0000F911 File Offset: 0x0000DB11
		internal static bool DistributedStoreDisableDxStoreFixUp
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DistributedStoreDisableDxStoreFixUp");
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000F922 File Offset: 0x0000DB22
		internal static int DistributedStoreDiffReportVerboseFlags
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DistributedStoreDiffReportVerboseFlags");
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000F933 File Offset: 0x0000DB33
		internal static int DistributedStoreDiffVerboseReportMaxCharsPerLine
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DistributedStoreDiffVerboseReportMaxCharsPerLine");
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0000F944 File Offset: 0x0000DB44
		internal static int DistributedStoreDagVersionCheckerDurationInSeconds
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DistributedStoreDagVersionCheckerDurationInSeconds");
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0000F955 File Offset: 0x0000DB55
		internal static int DistributedStoreStartupMinimumRequiredVersionAcrossDag
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<int>("DistributedStoreStartupMinimumRequiredVersionAcrossDag");
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0000F966 File Offset: 0x0000DB66
		internal static bool DistributedStoreIsLogPerformanceForSingleStore
		{
			get
			{
				return RegistryParameters.s_parameters.GetValue<bool>("DistributedStoreIsLogPerformanceForSingleStore");
			}
		}

		// Token: 0x04000294 RID: 660
		internal const string StoreKillBugcheckDisabledTimeKey = "StoreKillBugcheckDisabledTime";

		// Token: 0x04000295 RID: 661
		private const WatchdogAction DefaultFailureItemWatchdogAction = WatchdogAction.BugCheck;

		// Token: 0x04000296 RID: 662
		private const string EnableKernelWatchdogTimerPropertyGuid = "78341438-9b4a-4554-bbff-fd3ac2b5bbe3";

		// Token: 0x04000297 RID: 663
		internal const int PamMonitorRecoveryDurationInSecMinimum = 5;

		// Token: 0x04000298 RID: 664
		internal const int JetDatabaseType = 0;

		// Token: 0x04000299 RID: 665
		internal const int SqlDatabaseType = 1;

		// Token: 0x0400029A RID: 666
		internal const string RegistryKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters";

		// Token: 0x0400029B RID: 667
		internal const string StoreServiceRegistryKey = "SYSTEM\\CurrentControlSet\\services\\MSExchangeIS\\ParametersSystem";

		// Token: 0x0400029C RID: 668
		private static readonly RegistryParameterValues s_parameters = new RegistryParameterValues();

		// Token: 0x0400029D RID: 669
		private static ClusterNotifyFlags m_networkClusterNotificationMask = ~(ClusterNotifyFlags.CLUSTER_CHANGE_NODE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_NAME | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_ATTRIBUTES | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_VALUE | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_SUBTREE | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_TYPE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_TYPE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_TYPE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_RECONNECT | ClusterNotifyFlags.CLUSTER_CHANGE_QUORUM_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_PROPERTY);

		// Token: 0x0400029E RID: 670
		private static long m_bootTimeCookie;

		// Token: 0x0400029F RID: 671
		private static long m_bootTimeFswCookie;

		// Token: 0x040002A0 RID: 672
		private static WatchdogAction m_failureItemWatchdogAction = WatchdogAction.BugCheck;

		// Token: 0x040002A1 RID: 673
		private static bool m_enableKernelWatchdogTimer = false;

		// Token: 0x040002A2 RID: 674
		internal static string AmRemoteSiteCheckDisabledTimeKey = "AmRemoteSiteCheckDisabledTime";

		// Token: 0x040002A3 RID: 675
		private static bool m_gLoadedRegistryValues = false;
	}
}
