using System;
using System.IO;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000225 RID: 549
	internal sealed class UMRecyclerConfig
	{
		// Token: 0x06000FD8 RID: 4056 RVA: 0x000473CD File Offset: 0x000455CD
		private UMRecyclerConfig()
		{
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x000473D5 File Offset: 0x000455D5
		internal static string TempFilePath
		{
			get
			{
				return UMRecyclerConfig.tempFilePath;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x000473DC File Offset: 0x000455DC
		internal static string CertFileName
		{
			get
			{
				return UMRecyclerConfig.certFileName;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x000473E3 File Offset: 0x000455E3
		internal static string Tempdir
		{
			get
			{
				return UMRecyclerConfig.tempDir;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x000473EA File Offset: 0x000455EA
		internal static int DaysBeforeCertExpiryForAlert
		{
			get
			{
				return UMRecyclerConfig.daysBeforeCertExpiryForAlert;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x000473F1 File Offset: 0x000455F1
		internal static int SubsequentAlertIntervalAfterFirstAlert
		{
			get
			{
				return UMRecyclerConfig.subsequentAlertIntervalAfterFirstAlert;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x000473F8 File Offset: 0x000455F8
		internal static UMStartupMode UMStartupType
		{
			get
			{
				return UMRecyclerConfig.umStartupType;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x000473FF File Offset: 0x000455FF
		internal static int TcpListeningPort
		{
			get
			{
				return UMRecyclerConfig.tcpPort;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x00047406 File Offset: 0x00045606
		internal static int TlsListeningPort
		{
			get
			{
				return UMRecyclerConfig.tlsPort;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x0004740D File Offset: 0x0004560D
		// (set) Token: 0x06000FE2 RID: 4066 RVA: 0x00047414 File Offset: 0x00045614
		internal static double MaxPrivateBytesPercent
		{
			get
			{
				return UMRecyclerConfig.maxPrivateBytesPercent;
			}
			set
			{
				UMRecyclerConfig.maxPrivateBytesPercent = value;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x0004741C File Offset: 0x0004561C
		// (set) Token: 0x06000FE4 RID: 4068 RVA: 0x00047423 File Offset: 0x00045623
		internal static ulong MaxTempDirSize
		{
			get
			{
				return UMRecyclerConfig.maxTempDirSize;
			}
			set
			{
				UMRecyclerConfig.maxTempDirSize = value;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x0004742B File Offset: 0x0004562B
		// (set) Token: 0x06000FE6 RID: 4070 RVA: 0x00047432 File Offset: 0x00045632
		internal static int MaxCallsBeforeRecycle
		{
			get
			{
				return UMRecyclerConfig.maxCallsBeforeRecycle;
			}
			set
			{
				UMRecyclerConfig.maxCallsBeforeRecycle = value;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x0004743A File Offset: 0x0004563A
		// (set) Token: 0x06000FE8 RID: 4072 RVA: 0x00047441 File Offset: 0x00045641
		internal static ulong RecycleInterval
		{
			get
			{
				return UMRecyclerConfig.recycleInterval;
			}
			set
			{
				UMRecyclerConfig.recycleInterval = value;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x00047449 File Offset: 0x00045649
		// (set) Token: 0x06000FEA RID: 4074 RVA: 0x00047450 File Offset: 0x00045650
		internal static int Worker1SipPortNumber
		{
			get
			{
				return UMRecyclerConfig.worker1SipPortNumber;
			}
			set
			{
				UMRecyclerConfig.worker1SipPortNumber = value;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x00047458 File Offset: 0x00045658
		// (set) Token: 0x06000FEC RID: 4076 RVA: 0x0004745F File Offset: 0x0004565F
		internal static int Worker2SipPortNumber
		{
			get
			{
				return UMRecyclerConfig.worker2SipPortNumber;
			}
			set
			{
				UMRecyclerConfig.worker2SipPortNumber = value;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x00047467 File Offset: 0x00045667
		// (set) Token: 0x06000FEE RID: 4078 RVA: 0x0004746E File Offset: 0x0004566E
		internal static int HeartBeatInterval
		{
			get
			{
				return UMRecyclerConfig.heartBeatInterval;
			}
			set
			{
				UMRecyclerConfig.heartBeatInterval = value;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x00047476 File Offset: 0x00045676
		// (set) Token: 0x06000FF0 RID: 4080 RVA: 0x0004747D File Offset: 0x0004567D
		internal static int MaxHeartBeatFailures
		{
			get
			{
				return UMRecyclerConfig.maxHeartBeatFailures;
			}
			set
			{
				UMRecyclerConfig.maxHeartBeatFailures = value;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x00047485 File Offset: 0x00045685
		// (set) Token: 0x06000FF2 RID: 4082 RVA: 0x0004748C File Offset: 0x0004568C
		internal static int PingInterval
		{
			get
			{
				return UMRecyclerConfig.pingInterval;
			}
			set
			{
				UMRecyclerConfig.pingInterval = value;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x00047494 File Offset: 0x00045694
		// (set) Token: 0x06000FF4 RID: 4084 RVA: 0x0004749B File Offset: 0x0004569B
		internal static int ResourceMonitorInterval
		{
			get
			{
				return UMRecyclerConfig.resourceMonitorInterval;
			}
			set
			{
				UMRecyclerConfig.resourceMonitorInterval = value;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x000474A3 File Offset: 0x000456A3
		// (set) Token: 0x06000FF6 RID: 4086 RVA: 0x000474AA File Offset: 0x000456AA
		internal static int ThrashCountMaximum
		{
			get
			{
				return UMRecyclerConfig.thrashCountMaximum;
			}
			set
			{
				UMRecyclerConfig.thrashCountMaximum = value;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x000474B2 File Offset: 0x000456B2
		// (set) Token: 0x06000FF8 RID: 4088 RVA: 0x000474B9 File Offset: 0x000456B9
		internal static int StartupTime
		{
			get
			{
				return UMRecyclerConfig.startupTime;
			}
			set
			{
				UMRecyclerConfig.startupTime = value;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x000474C1 File Offset: 0x000456C1
		// (set) Token: 0x06000FFA RID: 4090 RVA: 0x000474C8 File Offset: 0x000456C8
		internal static int RetireTime
		{
			get
			{
				return UMRecyclerConfig.retireTime;
			}
			set
			{
				UMRecyclerConfig.retireTime = value;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x000474D0 File Offset: 0x000456D0
		// (set) Token: 0x06000FFC RID: 4092 RVA: 0x000474D7 File Offset: 0x000456D7
		internal static int HeartBeatResponseTime
		{
			get
			{
				return UMRecyclerConfig.heartBeatResponseTime;
			}
			set
			{
				UMRecyclerConfig.heartBeatResponseTime = value;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x000474DF File Offset: 0x000456DF
		internal static int AlertIntervalAfterStartupModeChanged
		{
			get
			{
				return UMRecyclerConfig.alertIntervalAfterStartupModeChanged;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x000474E6 File Offset: 0x000456E6
		internal static bool UseDataCenterActiveManagerRouting
		{
			get
			{
				return UMRecyclerConfig.useDataCenterActiveManagerRouting;
			}
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x000474ED File Offset: 0x000456ED
		internal static void Init()
		{
			UMRecyclerConfig.Init(new UMServiceADSettings());
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x000474FC File Offset: 0x000456FC
		internal static void Init(UMADSettings adSettings)
		{
			try
			{
				AppConfig instance = AppConfig.Instance;
				UMRecyclerConfig.retireTime = 1800;
				UMRecyclerConfig.worker1SipPortNumber = instance.Recycler.WorkerSIPPort;
				UMRecyclerConfig.maxPrivateBytesPercent = (double)instance.Recycler.MaxPrivateBytesPercent;
				UMRecyclerConfig.maxTempDirSize = (ulong)((long)Math.Max(0, instance.Recycler.MaxTempDirSize));
				UMRecyclerConfig.recycleInterval = (ulong)((long)Math.Max(0, instance.Recycler.RecycleInterval));
				UMRecyclerConfig.heartBeatInterval = instance.Recycler.HeartBeatInterval;
				UMRecyclerConfig.maxHeartBeatFailures = instance.Recycler.MaxHeartBeatFailures;
				UMRecyclerConfig.resourceMonitorInterval = instance.Recycler.ResourceMonitorInterval;
				UMRecyclerConfig.thrashCountMaximum = instance.Recycler.ThrashCountMaximum;
				UMRecyclerConfig.startupTime = instance.Recycler.StartupTime;
				UMRecyclerConfig.maxCallsBeforeRecycle = instance.Recycler.MaxCallsBeforeRecycle;
				UMRecyclerConfig.heartBeatResponseTime = instance.Recycler.HeartBeatResponseTime;
				UMRecyclerConfig.pingInterval = instance.Recycler.PingInterval;
				UMRecyclerConfig.subsequentAlertIntervalAfterFirstAlert = instance.Recycler.SubsequentAlertIntervalAfterFirstAlertForCert;
				UMRecyclerConfig.daysBeforeCertExpiryForAlert = instance.Recycler.DaysBeforeCertExpiryForAlert;
				UMRecyclerConfig.certFileName = Path.Combine(Utils.GetExchangeDirectory(), instance.Recycler.CertFileName);
				UMRecyclerConfig.alertIntervalAfterStartupModeChanged = instance.Recycler.AlertIntervalAfterStartupModeChanged;
				UMRecyclerConfig.useDataCenterActiveManagerRouting = instance.Recycler.UseDataCenterActiveManagerRouting;
				UMRecyclerConfig.DetermineUMStartupMode(adSettings);
				UMRecyclerConfig.tempFilePath = Path.Combine(Utils.GetExchangeDirectory(), "UnifiedMessaging\\temp\\UMTempFiles");
				UMRecyclerConfig.tempDir = "UnifiedMessaging\\temp\\UMTempFiles";
			}
			catch (Exception ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "Failed to Initialize ; error = {0}", new object[]
				{
					ex.ToString()
				});
				throw new UnableToInitializeResourceException("MSExchangeUM.config", ex);
			}
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x000476B8 File Offset: 0x000458B8
		private static void DetermineUMStartupMode(UMADSettings adSettings)
		{
			UMRecyclerConfig.tcpPort = adSettings.SipTcpListeningPort;
			UMRecyclerConfig.tlsPort = adSettings.SipTlsListeningPort;
			UMRecyclerConfig.umStartupType = adSettings.UMStartupMode;
			UMRecyclerConfig.worker2SipPortNumber = UMRecyclerConfig.worker1SipPortNumber + 2;
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "UMRecyclerConfig.DetermineUMStartupMode: TcpListeningPort = {0} TlsListeningPort = {1} StartupMode = {2}", new object[]
			{
				UMRecyclerConfig.tcpPort,
				UMRecyclerConfig.tlsPort,
				UMRecyclerConfig.umStartupType
			});
		}

		// Token: 0x04000B5D RID: 2909
		private static double maxPrivateBytesPercent;

		// Token: 0x04000B5E RID: 2910
		private static ulong maxTempDirSize;

		// Token: 0x04000B5F RID: 2911
		private static ulong recycleInterval;

		// Token: 0x04000B60 RID: 2912
		private static int heartBeatInterval;

		// Token: 0x04000B61 RID: 2913
		private static int maxHeartBeatFailures;

		// Token: 0x04000B62 RID: 2914
		private static int worker1SipPortNumber;

		// Token: 0x04000B63 RID: 2915
		private static int worker2SipPortNumber;

		// Token: 0x04000B64 RID: 2916
		private static int resourceMonitorInterval;

		// Token: 0x04000B65 RID: 2917
		private static int thrashCountMaximum;

		// Token: 0x04000B66 RID: 2918
		private static int startupTime;

		// Token: 0x04000B67 RID: 2919
		private static int retireTime;

		// Token: 0x04000B68 RID: 2920
		private static int pingInterval;

		// Token: 0x04000B69 RID: 2921
		private static int alertIntervalAfterStartupModeChanged;

		// Token: 0x04000B6A RID: 2922
		private static int maxCallsBeforeRecycle;

		// Token: 0x04000B6B RID: 2923
		private static int heartBeatResponseTime;

		// Token: 0x04000B6C RID: 2924
		private static string tempFilePath;

		// Token: 0x04000B6D RID: 2925
		private static string certFileName;

		// Token: 0x04000B6E RID: 2926
		private static string tempDir;

		// Token: 0x04000B6F RID: 2927
		private static int tcpPort = -1;

		// Token: 0x04000B70 RID: 2928
		private static int tlsPort = -1;

		// Token: 0x04000B71 RID: 2929
		private static UMStartupMode umStartupType;

		// Token: 0x04000B72 RID: 2930
		private static int subsequentAlertIntervalAfterFirstAlert;

		// Token: 0x04000B73 RID: 2931
		private static int daysBeforeCertExpiryForAlert;

		// Token: 0x04000B74 RID: 2932
		private static bool useDataCenterActiveManagerRouting;
	}
}
