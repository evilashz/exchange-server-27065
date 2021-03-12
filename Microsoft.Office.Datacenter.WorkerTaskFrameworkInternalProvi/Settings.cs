using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using Microsoft.Win32;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x0200000B RID: 11
	public static class Settings
	{
		// Token: 0x0600008C RID: 140 RVA: 0x0000478C File Offset: 0x0000298C
		static Settings()
		{
			Settings.uniformWorkload = Settings.workload;
			Settings.workload.Value = ((Settings.workload == "EXO") ? "Exchange" : Settings.workload);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000539C File Offset: 0x0000359C
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000053A8 File Offset: 0x000035A8
		public static string HttpProxyAvailabilityGroup
		{
			get
			{
				return Settings.httpProxyAvailabilityGroup;
			}
			set
			{
				Settings.httpProxyAvailabilityGroup.Value = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000053B5 File Offset: 0x000035B5
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000053C1 File Offset: 0x000035C1
		public static string DefaultResultsLogPath
		{
			get
			{
				return Settings.defaultResultsLogPath;
			}
			set
			{
				Settings.defaultResultsLogPath.Value = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000053CE File Offset: 0x000035CE
		// (set) Token: 0x06000092 RID: 146 RVA: 0x000053DA File Offset: 0x000035DA
		public static string DefaultTraceLogPath
		{
			get
			{
				return Settings.defaultTraceLogPath;
			}
			set
			{
				Settings.defaultTraceLogPath.Value = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000053E7 File Offset: 0x000035E7
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000053F3 File Offset: 0x000035F3
		public static bool IsResultsLoggingEnabled
		{
			get
			{
				return Settings.isResultsLoggingEnabled;
			}
			set
			{
				Settings.isResultsLoggingEnabled.Value = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00005400 File Offset: 0x00003600
		// (set) Token: 0x06000096 RID: 150 RVA: 0x0000540C File Offset: 0x0000360C
		public static bool IsTraceLoggingEnabled
		{
			get
			{
				return Settings.isTraceLoggingEnabled;
			}
			set
			{
				Settings.isTraceLoggingEnabled.Value = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00005419 File Offset: 0x00003619
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00005425 File Offset: 0x00003625
		public static int MaxLogAge
		{
			get
			{
				return Settings.maxLogAge;
			}
			set
			{
				Settings.maxLogAge.Value = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00005432 File Offset: 0x00003632
		// (set) Token: 0x0600009A RID: 154 RVA: 0x0000543E File Offset: 0x0000363E
		public static int MaxResultsLogDirectorySizeInBytes
		{
			get
			{
				return Settings.maxResultsLogDirectorySizeInBytes;
			}
			set
			{
				Settings.maxResultsLogDirectorySizeInBytes.Value = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000544B File Offset: 0x0000364B
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00005457 File Offset: 0x00003657
		public static int MaxResultsLogFileSizeInBytes
		{
			get
			{
				return Settings.maxResultsLogFileSizeInBytes;
			}
			set
			{
				Settings.maxResultsLogFileSizeInBytes.Value = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00005464 File Offset: 0x00003664
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00005470 File Offset: 0x00003670
		public static long MaxTraceLogsDirectorySizeInBytes
		{
			get
			{
				return Settings.maxTraceLogsDirectorySizeInBytes;
			}
			set
			{
				Settings.maxTraceLogsDirectorySizeInBytes.Value = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000547D File Offset: 0x0000367D
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00005489 File Offset: 0x00003689
		public static long MaxTraceLogFileSizeInBytes
		{
			get
			{
				return Settings.maxTraceLogFileSizeInBytes;
			}
			set
			{
				Settings.maxTraceLogFileSizeInBytes.Value = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00005496 File Offset: 0x00003696
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x000054A2 File Offset: 0x000036A2
		public static int MaxPersistentStateDirectorySizeInBytes
		{
			get
			{
				return Settings.maxPersistentStateDirectorySizeInBytes;
			}
			set
			{
				Settings.maxPersistentStateDirectorySizeInBytes.Value = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000054AF File Offset: 0x000036AF
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x000054BB File Offset: 0x000036BB
		public static int MaxPersistentStateFileSizeInBytes
		{
			get
			{
				return Settings.maxPersistentStateFileSizeInBytes;
			}
			set
			{
				Settings.maxPersistentStateFileSizeInBytes.Value = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000054C8 File Offset: 0x000036C8
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x000054D4 File Offset: 0x000036D4
		public static int ResultsLogBufferSizeInBytes
		{
			get
			{
				return Settings.resultsLogBufferSizeInBytes;
			}
			set
			{
				Settings.resultsLogBufferSizeInBytes.Value = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000054E1 File Offset: 0x000036E1
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x000054ED File Offset: 0x000036ED
		public static int ResultsLogFlushIntervalInMinutes
		{
			get
			{
				return Settings.resultsLogFlushIntervalInMinutes;
			}
			set
			{
				Settings.resultsLogFlushIntervalInMinutes.Value = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000054FA File Offset: 0x000036FA
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00005506 File Offset: 0x00003706
		public static int TraceLogBufferSizeInBytes
		{
			get
			{
				return Settings.traceLogBufferSizeInBytes;
			}
			set
			{
				Settings.traceLogBufferSizeInBytes.Value = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00005513 File Offset: 0x00003713
		// (set) Token: 0x060000AC RID: 172 RVA: 0x0000551F File Offset: 0x0000371F
		public static int TraceLogFlushIntervalInMinutes
		{
			get
			{
				return Settings.traceLogFlushIntervalInMinutes;
			}
			set
			{
				Settings.traceLogFlushIntervalInMinutes.Value = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000552C File Offset: 0x0000372C
		public static string OverrideRegistryPath
		{
			get
			{
				return Settings.registryPath;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00005533 File Offset: 0x00003733
		// (set) Token: 0x060000AF RID: 175 RVA: 0x0000553F File Offset: 0x0000373F
		public static string SqlConnectionString
		{
			get
			{
				return Settings.connectionString;
			}
			set
			{
				Settings.connectionString.Value = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000554C File Offset: 0x0000374C
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00005558 File Offset: 0x00003758
		public static string TopologyConnectionString
		{
			get
			{
				return Settings.topologyConnectionString;
			}
			set
			{
				Settings.topologyConnectionString.Value = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00005565 File Offset: 0x00003765
		public static string SmtpServerName
		{
			get
			{
				return Settings.smtpServerName;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00005571 File Offset: 0x00003771
		public static int SmtpServerPort
		{
			get
			{
				return Settings.smtpServerPort;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000557D File Offset: 0x0000377D
		public static string SenderMailAddress
		{
			get
			{
				return Settings.sendMailAddress;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00005589 File Offset: 0x00003789
		public static int ThrottleAmount
		{
			get
			{
				return Settings.throttleAmount;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00005595 File Offset: 0x00003795
		public static int WaitForWorkAmount
		{
			get
			{
				return Settings.waitForWorkAmount;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000055A1 File Offset: 0x000037A1
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000055AD File Offset: 0x000037AD
		public static int MaxWorkitemBatchSize
		{
			get
			{
				return Settings.maxWorkitemBatchSize;
			}
			set
			{
				Settings.maxWorkitemBatchSize.Value = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000055BA File Offset: 0x000037BA
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000055C6 File Offset: 0x000037C6
		public static int MaxRunningTasks
		{
			get
			{
				return Settings.maxRunningTasks;
			}
			set
			{
				Settings.maxRunningTasks.Value = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000055D3 File Offset: 0x000037D3
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000055DF File Offset: 0x000037DF
		public static int WaitAmountBeforeRestartRequest
		{
			get
			{
				return Settings.waitAmountBeforeRestartRequest;
			}
			set
			{
				Settings.waitAmountBeforeRestartRequest.Value = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000055EC File Offset: 0x000037EC
		public static string HostedServiceName
		{
			get
			{
				return Settings.hostedServiceName;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000055F8 File Offset: 0x000037F8
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00005604 File Offset: 0x00003804
		public static string InstanceName
		{
			get
			{
				return Settings.instanceName;
			}
			set
			{
				Settings.instanceName.Value = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00005611 File Offset: 0x00003811
		public static string DeploymentName
		{
			get
			{
				return Settings.deploymentName;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000561D File Offset: 0x0000381D
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00005629 File Offset: 0x00003829
		public static int DeploymentId
		{
			get
			{
				return Settings.deploymentId;
			}
			set
			{
				Settings.deploymentId.Value = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00005636 File Offset: 0x00003836
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00005642 File Offset: 0x00003842
		public static string Scope
		{
			get
			{
				return Settings.scope;
			}
			internal set
			{
				Settings.scope.Value = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000564F File Offset: 0x0000384F
		public static string UniformWorkload
		{
			get
			{
				return Settings.uniformWorkload;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005656 File Offset: 0x00003856
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00005662 File Offset: 0x00003862
		public static string Workload
		{
			get
			{
				return Settings.workload;
			}
			internal set
			{
				Settings.workload.Value = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000566F File Offset: 0x0000386F
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x0000567B File Offset: 0x0000387B
		public static string WorkloadVersion
		{
			get
			{
				return Settings.workloadVersion;
			}
			internal set
			{
				Settings.workloadVersion.Value = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00005688 File Offset: 0x00003888
		public static string OverrideProbeExecutionLocation
		{
			get
			{
				return Settings.overrideProbeExecutionLocation;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005694 File Offset: 0x00003894
		// (set) Token: 0x060000CC RID: 204 RVA: 0x000056A0 File Offset: 0x000038A0
		public static string Environment
		{
			get
			{
				return Settings.environment;
			}
			internal set
			{
				Settings.environment.Value = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000056AD File Offset: 0x000038AD
		public static string EscalationEndpoint
		{
			get
			{
				return Settings.escalationEndpoint;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000056B9 File Offset: 0x000038B9
		// (set) Token: 0x060000CF RID: 207 RVA: 0x000056C5 File Offset: 0x000038C5
		public static string FileStorageLocation
		{
			get
			{
				return Settings.fileLocation;
			}
			set
			{
				Settings.fileLocation.Value = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000056D2 File Offset: 0x000038D2
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x000056DE File Offset: 0x000038DE
		public static int WorkItemRetrievalDelay
		{
			get
			{
				return Settings.workItemRetrievalDelay;
			}
			set
			{
				Settings.workItemRetrievalDelay.Value = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000056EB File Offset: 0x000038EB
		public static int? ServiceRestartDelay
		{
			get
			{
				return Settings.serviceRestartDelay;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000056F7 File Offset: 0x000038F7
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00005703 File Offset: 0x00003903
		public static bool IsUseCurrentUserHiveForManagedAvailability
		{
			get
			{
				return Settings.isUseCurrentUserHiveForManagedAvailability;
			}
			set
			{
				Settings.isUseCurrentUserHiveForManagedAvailability.Value = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00005710 File Offset: 0x00003910
		public static RegistryKey ManagedAvailabilityServiceRegistryHive
		{
			get
			{
				if (Settings.IsUseCurrentUserHiveForManagedAvailability)
				{
					return Registry.CurrentUser;
				}
				return Registry.LocalMachine;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005724 File Offset: 0x00003924
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x0000572B File Offset: 0x0000392B
		public static string MachineName
		{
			get
			{
				return Settings.machineName;
			}
			set
			{
				Settings.machineName = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005733 File Offset: 0x00003933
		public static int ResultHistoryWindowInMinutes
		{
			get
			{
				return Settings.resultHistoryWindowInMinutes;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000573F File Offset: 0x0000393F
		// (set) Token: 0x060000DA RID: 218 RVA: 0x0000574B File Offset: 0x0000394B
		public static int ProbeResultHistoryWindowSize
		{
			get
			{
				return Settings.probeResultHistoryWindowSize;
			}
			internal set
			{
				Settings.probeResultHistoryWindowSize.Value = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00005758 File Offset: 0x00003958
		public static int MonitorResultHistoryWindowSize
		{
			get
			{
				return Settings.monitorResultHistoryWindowSize;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005764 File Offset: 0x00003964
		public static int ResponderResultHistoryWindowSize
		{
			get
			{
				return Settings.responderResultHistoryWindowSize;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00005770 File Offset: 0x00003970
		public static int MaintenanceResultHistoryWindowSize
		{
			get
			{
				return Settings.maintenanceResultHistoryWindowSize;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000577C File Offset: 0x0000397C
		public static int QuarantineHours
		{
			get
			{
				return Settings.quarantineHours;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00005788 File Offset: 0x00003988
		public static int MaxPoisonCount
		{
			get
			{
				return Settings.maxPoisonCount;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005794 File Offset: 0x00003994
		public static int NonRecurrentRetryIntervalSeconds
		{
			get
			{
				return Settings.nonRecurrentRetryIntervalSeconds;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000057A0 File Offset: 0x000039A0
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x000057AC File Offset: 0x000039AC
		public static bool IsProductionEnvironment
		{
			get
			{
				return Settings.isProductionEnvironment;
			}
			internal set
			{
				Settings.isProductionEnvironment.Value = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000057B9 File Offset: 0x000039B9
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x000057C5 File Offset: 0x000039C5
		public static int BulkInsertBatchSize
		{
			get
			{
				return Settings.bulkInsertBatchSize;
			}
			set
			{
				Settings.bulkInsertBatchSize.Value = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000057D2 File Offset: 0x000039D2
		public static int ConsecutiveMaintenanceFailureThreshold
		{
			get
			{
				return Settings.consecutiveMaintenanceFailureThreshold;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x000057DE File Offset: 0x000039DE
		public static bool UseE14MonitoringTenant
		{
			get
			{
				return Settings.useE14MonitoringTenant;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000057EA File Offset: 0x000039EA
		public static bool TracingCredentials
		{
			get
			{
				return Settings.tracingCredentials;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000057F6 File Offset: 0x000039F6
		public static int MaxObservers
		{
			get
			{
				return Settings.maxObservers;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00005802 File Offset: 0x00003A02
		public static int MaxSubjects
		{
			get
			{
				return Settings.maxSubjects;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000580E File Offset: 0x00003A0E
		public static int MaxRequestObservers
		{
			get
			{
				return Settings.maxRequestObservers;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000EB RID: 235 RVA: 0x0000581A File Offset: 0x00003A1A
		public static int MaxZombieSubjects
		{
			get
			{
				return Settings.maxZombieSubjects;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00005826 File Offset: 0x00003A26
		public static int MaxMdbPerCasServer
		{
			get
			{
				return Settings.maxMdbPerCasServer;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00005832 File Offset: 0x00003A32
		// (set) Token: 0x060000EE RID: 238 RVA: 0x0000583E File Offset: 0x00003A3E
		public static bool EnableStreamInsightPush
		{
			get
			{
				return Settings.enableStreamInsightPush;
			}
			set
			{
				Settings.enableStreamInsightPush.Value = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000584B File Offset: 0x00003A4B
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00005857 File Offset: 0x00003A57
		public static string StreamInsightServerAddress
		{
			get
			{
				return Settings.streamInsightServerAddress;
			}
			set
			{
				Settings.streamInsightServerAddress.Value = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00005864 File Offset: 0x00003A64
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00005870 File Offset: 0x00003A70
		public static string StreamInsightXamServerName
		{
			get
			{
				return Settings.streamInsightXamServerName;
			}
			set
			{
				Settings.streamInsightXamServerName.Value = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000587D File Offset: 0x00003A7D
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00005889 File Offset: 0x00003A89
		public static string StreamInsightXamDatabaseName
		{
			get
			{
				return Settings.streamInsightXamDatabaseName;
			}
			set
			{
				Settings.streamInsightXamDatabaseName.Value = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005896 File Offset: 0x00003A96
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x000058A2 File Offset: 0x00003AA2
		public static bool EnableOAuth
		{
			get
			{
				return Settings.enableOAuth;
			}
			set
			{
				Settings.enableOAuth.Value = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000058AF File Offset: 0x00003AAF
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x000058BB File Offset: 0x00003ABB
		public static string OAuthTenantName
		{
			get
			{
				return Settings.authTenantName;
			}
			set
			{
				Settings.authTenantName.Value = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000058C8 File Offset: 0x00003AC8
		// (set) Token: 0x060000FA RID: 250 RVA: 0x000058D4 File Offset: 0x00003AD4
		public static string OAuthAppId
		{
			get
			{
				return Settings.authAppId;
			}
			set
			{
				Settings.authAppId.Value = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000058E1 File Offset: 0x00003AE1
		// (set) Token: 0x060000FC RID: 252 RVA: 0x000058ED File Offset: 0x00003AED
		public static string OAuthSymmetricKey
		{
			get
			{
				return Settings.authSymmetricKey;
			}
			set
			{
				Settings.authSymmetricKey.Value = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000058FA File Offset: 0x00003AFA
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00005906 File Offset: 0x00003B06
		public static string OAuthCertificateName
		{
			get
			{
				return Settings.authCertificateName;
			}
			set
			{
				Settings.authCertificateName.Value = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00005913 File Offset: 0x00003B13
		// (set) Token: 0x06000100 RID: 256 RVA: 0x0000591F File Offset: 0x00003B1F
		public static string KeynoteDataFeedBaseUrl
		{
			get
			{
				return Settings.keynoteDataFeedBaseUrl;
			}
			set
			{
				Settings.keynoteDataFeedBaseUrl.Value = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000592C File Offset: 0x00003B2C
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00005938 File Offset: 0x00003B38
		public static string KeynoteDataPulseBaseUrl
		{
			get
			{
				return Settings.keynoteDataPulseBaseUrl;
			}
			set
			{
				Settings.keynoteDataPulseBaseUrl.Value = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00005945 File Offset: 0x00003B45
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00005951 File Offset: 0x00003B51
		public static string KeynoteDataFeedAgreementIdUserMapping
		{
			get
			{
				return Settings.keynoteDataFeedAgreementIdUserMapping;
			}
			set
			{
				Settings.keynoteDataFeedAgreementIdUserMapping.Value = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000595E File Offset: 0x00003B5E
		// (set) Token: 0x06000106 RID: 262 RVA: 0x0000596A File Offset: 0x00003B6A
		public static string KeynoteDataFeedBiz40AgreementIdUserMapping
		{
			get
			{
				return Settings.keynoteDataFeedBiz40AgreementIdUserMapping;
			}
			set
			{
				Settings.keynoteDataFeedBiz40AgreementIdUserMapping.Value = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005977 File Offset: 0x00003B77
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00005983 File Offset: 0x00003B83
		public static string KeynoteDataPulseAgreementIdUserMapping
		{
			get
			{
				return Settings.keynoteDataPulseAgreementIdUserMapping;
			}
			set
			{
				Settings.keynoteDataPulseAgreementIdUserMapping.Value = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005990 File Offset: 0x00003B90
		// (set) Token: 0x0600010A RID: 266 RVA: 0x0000599C File Offset: 0x00003B9C
		public static string KeynoteDataPulseBiz40AgreementIdUserMapping
		{
			get
			{
				return Settings.keynoteDataPulseBiz40AgreementIdUserMapping;
			}
			set
			{
				Settings.keynoteDataPulseBiz40AgreementIdUserMapping.Value = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000059A9 File Offset: 0x00003BA9
		// (set) Token: 0x0600010C RID: 268 RVA: 0x000059B5 File Offset: 0x00003BB5
		public static string KeynoteCredentials
		{
			get
			{
				return Settings.keynoteCredentials;
			}
			set
			{
				Settings.keynoteCredentials.Value = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000059C2 File Offset: 0x00003BC2
		// (set) Token: 0x0600010E RID: 270 RVA: 0x000059CE File Offset: 0x00003BCE
		public static string RemotePowershellCertSubject
		{
			get
			{
				return Settings.remotePowershellCertSubject;
			}
			set
			{
				Settings.remotePowershellCertSubject.Value = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000059DB File Offset: 0x00003BDB
		// (set) Token: 0x06000110 RID: 272 RVA: 0x000059E7 File Offset: 0x00003BE7
		public static bool RunningAsConsoleHost
		{
			get
			{
				return Settings.runningAsConsoleHost;
			}
			set
			{
				Settings.runningAsConsoleHost.Value = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000059F4 File Offset: 0x00003BF4
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00005A00 File Offset: 0x00003C00
		public static int NumberOfLastProbeResults
		{
			get
			{
				return Settings.numberOfLastProbeResults;
			}
			set
			{
				Settings.numberOfLastProbeResults.Value = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00005A0D File Offset: 0x00003C0D
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00005A19 File Offset: 0x00003C19
		public static int NumberOfLastMonitorResults
		{
			get
			{
				return Settings.numberOfLastMonitorResults;
			}
			set
			{
				Settings.numberOfLastMonitorResults.Value = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00005A26 File Offset: 0x00003C26
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00005A32 File Offset: 0x00003C32
		public static int NumberOfLastResponderResults
		{
			get
			{
				return Settings.numberOfLastResponderResults;
			}
			set
			{
				Settings.numberOfLastResponderResults.Value = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00005A3F File Offset: 0x00003C3F
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00005A4B File Offset: 0x00003C4B
		public static int NumberOfLastMaintenanceResults
		{
			get
			{
				return Settings.numberOfLastMaintenanceResults;
			}
			set
			{
				Settings.numberOfLastMaintenanceResults.Value = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00005A58 File Offset: 0x00003C58
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00005A64 File Offset: 0x00003C64
		public static int BatchManagerBatchSize
		{
			get
			{
				return Settings.batchManagerBatchSize;
			}
			set
			{
				Settings.batchManagerBatchSize.Value = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00005A71 File Offset: 0x00003C71
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00005A7D File Offset: 0x00003C7D
		public static int BatchManagerBatchWaitTimeSeconds
		{
			get
			{
				return Settings.batchManagerBatchWaitTimeSeconds;
			}
			set
			{
				Settings.batchManagerBatchWaitTimeSeconds.Value = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00005A8A File Offset: 0x00003C8A
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00005A96 File Offset: 0x00003C96
		public static bool UseTransactionsInWorkItemGeneration
		{
			get
			{
				return Settings.useTransactionsInWorkItemGeneration;
			}
			set
			{
				Settings.useTransactionsInWorkItemGeneration.Value = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00005AA3 File Offset: 0x00003CA3
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00005AAF File Offset: 0x00003CAF
		public static bool IsPersistentStateEnabled
		{
			get
			{
				return Settings.isPersistentStateEnabled;
			}
			set
			{
				Settings.isPersistentStateEnabled.Value = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00005ABC File Offset: 0x00003CBC
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00005AC8 File Offset: 0x00003CC8
		public static bool IsCancelWorkItemsOnQuitRequestFeatureEnabled
		{
			get
			{
				return Settings.isCancelWorkItemsOnQuitRequestFeatureEnabled;
			}
			set
			{
				Settings.isCancelWorkItemsOnQuitRequestFeatureEnabled.Value = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00005AD5 File Offset: 0x00003CD5
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00005AE1 File Offset: 0x00003CE1
		public static string ProbeSvcAddress
		{
			get
			{
				return Settings.probeSvcAddress;
			}
			set
			{
				Settings.probeSvcAddress.Value = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00005AEE File Offset: 0x00003CEE
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00005AFA File Offset: 0x00003CFA
		public static string AccessControlHostName
		{
			get
			{
				return Settings.accessControlHostName;
			}
			set
			{
				Settings.accessControlHostName.Value = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00005B07 File Offset: 0x00003D07
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00005B13 File Offset: 0x00003D13
		public static string AccessControlNamespace
		{
			get
			{
				return Settings.accessControlNamespace;
			}
			set
			{
				Settings.accessControlNamespace.Value = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00005B20 File Offset: 0x00003D20
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00005B2C File Offset: 0x00003D2C
		public static string AccessControlSigningCertificateFilePath
		{
			get
			{
				return Settings.accessControlSigningCertificateFilePath;
			}
			set
			{
				Settings.accessControlSigningCertificateFilePath.Value = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00005B39 File Offset: 0x00003D39
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00005B45 File Offset: 0x00003D45
		public static string ProbeSvcClientPassword
		{
			get
			{
				return Settings.probeSvcClientPassword;
			}
			set
			{
				Settings.probeSvcClientPassword.Value = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00005B52 File Offset: 0x00003D52
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00005B5E File Offset: 0x00003D5E
		public static string ProbeSvcClientUsername
		{
			get
			{
				return Settings.probeSvcClientUsername;
			}
			set
			{
				Settings.probeSvcClientUsername.Value = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00005B6B File Offset: 0x00003D6B
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00005B77 File Offset: 0x00003D77
		public static string ProbeSvcCertificateFilePath
		{
			get
			{
				return Settings.probeSvcCertificateFilePath;
			}
			set
			{
				Settings.probeSvcCertificateFilePath.Value = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00005B84 File Offset: 0x00003D84
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00005B90 File Offset: 0x00003D90
		public static string ProbeSvcCertificateDN
		{
			get
			{
				return Settings.probeSvcCertificateDN;
			}
			set
			{
				Settings.probeSvcCertificateDN.Value = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005B9D File Offset: 0x00003D9D
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00005BA9 File Offset: 0x00003DA9
		public static bool UseSynchronousContinuationForWorkitemResults
		{
			get
			{
				return Settings.useSynchronousContinuationForWorkitemResults;
			}
			set
			{
				Settings.useSynchronousContinuationForWorkitemResults.Value = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00005BB6 File Offset: 0x00003DB6
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00005BC2 File Offset: 0x00003DC2
		public static bool CalculateTimeoutFromBeginningOfExecution
		{
			get
			{
				return Settings.calculateTimeoutFromBeginningOfExecution;
			}
			set
			{
				Settings.calculateTimeoutFromBeginningOfExecution.Value = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00005BCF File Offset: 0x00003DCF
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00005BDB File Offset: 0x00003DDB
		public static string StaticMonitoringPassword
		{
			get
			{
				return Settings.staticMonitoringPassword;
			}
			set
			{
				Settings.staticMonitoringPassword.Value = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00005BE8 File Offset: 0x00003DE8
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00005BF4 File Offset: 0x00003DF4
		public static string SqlReadOnlyUser
		{
			get
			{
				return Settings.sqlReadOnlyUser;
			}
			set
			{
				Settings.sqlReadOnlyUser.Value = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00005C01 File Offset: 0x00003E01
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00005C0D File Offset: 0x00003E0D
		public static string SqlReadOnlyPassword
		{
			get
			{
				return Settings.sqlReadOnlyPassword;
			}
			set
			{
				Settings.sqlReadOnlyPassword.Value = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00005C1A File Offset: 0x00003E1A
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00005C26 File Offset: 0x00003E26
		public static string DataInsightSqlReadonlyUsername
		{
			get
			{
				return Settings.dataInsightSqlReadonlyUsername;
			}
			set
			{
				Settings.dataInsightSqlReadonlyUsername.Value = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00005C33 File Offset: 0x00003E33
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00005C3F File Offset: 0x00003E3F
		public static string DataInsightSqlReadonlyPassword
		{
			get
			{
				return Settings.dataInsightSqlReadonlyPassword;
			}
			set
			{
				Settings.dataInsightSqlReadonlyPassword.Value = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00005C4C File Offset: 0x00003E4C
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00005C58 File Offset: 0x00003E58
		public static string SqlReadWriteUser
		{
			get
			{
				return Settings.sqlReadWriteUser;
			}
			set
			{
				Settings.sqlReadWriteUser.Value = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00005C65 File Offset: 0x00003E65
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00005C71 File Offset: 0x00003E71
		public static string SqlReadWritePassword
		{
			get
			{
				return Settings.sqlReadWritePassword;
			}
			set
			{
				Settings.sqlReadWritePassword.Value = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00005C7E File Offset: 0x00003E7E
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00005C8A File Offset: 0x00003E8A
		public static string OpticsServer
		{
			get
			{
				return Settings.opticsServer;
			}
			set
			{
				Settings.opticsServer.Value = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00005C97 File Offset: 0x00003E97
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00005CA3 File Offset: 0x00003EA3
		public static string SystemMonitoringInstance
		{
			get
			{
				return Settings.systemMonitoringInstance;
			}
			set
			{
				Settings.systemMonitoringInstance.Value = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00005CB0 File Offset: 0x00003EB0
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00005CBC File Offset: 0x00003EBC
		public static string OrcaVdir
		{
			get
			{
				return Settings.orcaVdir;
			}
			set
			{
				Settings.orcaVdir.Value = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00005CC9 File Offset: 0x00003EC9
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00005CD5 File Offset: 0x00003ED5
		public static string ORCAUrlSuffix
		{
			get
			{
				return Settings.orcaUrlSuffix;
			}
			set
			{
				Settings.orcaUrlSuffix.Value = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00005CE2 File Offset: 0x00003EE2
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00005CEE File Offset: 0x00003EEE
		public static string ORCAATMUrl
		{
			get
			{
				return Settings.orcaATMUrl;
			}
			set
			{
				Settings.orcaATMUrl.Value = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00005CFB File Offset: 0x00003EFB
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00005D07 File Offset: 0x00003F07
		public static string OrcaClientCredential
		{
			get
			{
				return Settings.orcaClientCredential;
			}
			set
			{
				Settings.orcaClientCredential.Value = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00005D14 File Offset: 0x00003F14
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00005D20 File Offset: 0x00003F20
		public static int CafeMailboxes
		{
			get
			{
				return Settings.cafeMailboxes;
			}
			set
			{
				if (value < 1)
				{
					Settings.cafeMailboxes.Value = 1;
					return;
				}
				if (value > Settings.MaxMdbPerCasServer)
				{
					Settings.cafeMailboxes.Value = Settings.MaxMdbPerCasServer;
					return;
				}
				Settings.cafeMailboxes.Value = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00005D55 File Offset: 0x00003F55
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00005D61 File Offset: 0x00003F61
		public static int MaintenanceTimeoutWatsonHours
		{
			get
			{
				return Settings.maintenanceTimeoutWatsonHours;
			}
			set
			{
				Settings.maintenanceTimeoutWatsonHours.Value = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00005D6E File Offset: 0x00003F6E
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00005D7A File Offset: 0x00003F7A
		public static bool IsCortex
		{
			get
			{
				return Settings.isCortex;
			}
			set
			{
				Settings.isCortex.Value = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00005D87 File Offset: 0x00003F87
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00005D93 File Offset: 0x00003F93
		public static string AzureTableConnectionString
		{
			get
			{
				return Settings.azureTableConnectionString;
			}
			set
			{
				Settings.azureTableConnectionString.Value = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00005DA0 File Offset: 0x00003FA0
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00005DAC File Offset: 0x00003FAC
		public static int MaxNumberOfWorkUnits
		{
			get
			{
				return Settings.maxNumberOfWorkUnits;
			}
			set
			{
				Settings.maxNumberOfWorkUnits.Value = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00005DB9 File Offset: 0x00003FB9
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00005DC5 File Offset: 0x00003FC5
		public static int MaxWorkUnitCost
		{
			get
			{
				return Settings.maxWorkUnitCost;
			}
			set
			{
				Settings.maxWorkUnitCost.Value = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00005DD2 File Offset: 0x00003FD2
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00005DDE File Offset: 0x00003FDE
		public static int MaxRecoveryAttempts
		{
			get
			{
				return Settings.maxRecoveryAttempts;
			}
			set
			{
				Settings.maxRecoveryAttempts.Value = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00005DEB File Offset: 0x00003FEB
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00005DF7 File Offset: 0x00003FF7
		public static int RecoveryThrottleTimeInSeconds
		{
			get
			{
				return Settings.recoveryThrottleTimeInSeconds;
			}
			set
			{
				Settings.recoveryThrottleTimeInSeconds.Value = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00005E04 File Offset: 0x00004004
		public static int NumberOfHistoricalTables
		{
			get
			{
				return Settings.numberOfHistoricalTables;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00005E0B File Offset: 0x0000400B
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00005E17 File Offset: 0x00004017
		public static bool RestartOnPoisonedWorkItem
		{
			get
			{
				return Settings.restartOnPoisonedWorkItem;
			}
			set
			{
				Settings.restartOnPoisonedWorkItem.Value = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00005E24 File Offset: 0x00004024
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00005E30 File Offset: 0x00004030
		public static string DataPartition
		{
			get
			{
				return Settings.dataPartition;
			}
			set
			{
				Settings.dataPartition.Value = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00005E3D File Offset: 0x0000403D
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00005E49 File Offset: 0x00004049
		public static string CortexDataPartitionRingKey
		{
			get
			{
				return Settings.cortexDataPartitionRingKey;
			}
			set
			{
				Settings.cortexDataPartitionRingKey.Value = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00005E56 File Offset: 0x00004056
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00005E62 File Offset: 0x00004062
		public static string OrcaHubConnectionString
		{
			get
			{
				return Settings.orcaHubConnectionString;
			}
			set
			{
				Settings.orcaHubConnectionString.Value = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00005E6F File Offset: 0x0000406F
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00005E7B File Offset: 0x0000407B
		public static string OrcaSpokeConnectionString
		{
			get
			{
				return Settings.orcaSpokeConnectionString;
			}
			set
			{
				Settings.orcaSpokeConnectionString.Value = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00005E88 File Offset: 0x00004088
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00005E94 File Offset: 0x00004094
		public static string OrcaSpokeIdentity
		{
			get
			{
				return Settings.orcaSpokeIdentity;
			}
			set
			{
				Settings.orcaSpokeIdentity.Value = value;
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005EA4 File Offset: 0x000040A4
		public static void ApplyOverride(string name, string value)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("name need be specified");
			}
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(Settings.registryPath))
			{
				registryKey.SetValue(name, value);
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005EF8 File Offset: 0x000040F8
		public static void RemoveOverride(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("name need be specified");
			}
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(Settings.registryPath, true))
			{
				registryKey.DeleteSubKey(name, false);
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005F50 File Offset: 0x00004150
		public static void RemoveAllOverrides()
		{
			Registry.LocalMachine.DeleteSubKeyTree(Settings.registryPath, false);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005F64 File Offset: 0x00004164
		internal static void RegisterTableMapping(Type tableType)
		{
			string tableName = Settings.GetTableName(tableType);
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			for (int i = 0; i < Settings.numberOfHistoricalTables; i++)
			{
				dictionary.Add(i, string.Format("{0}_{1}", tableName, i));
			}
			Settings.tableMapping.Add(tableType, dictionary);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005FB4 File Offset: 0x000041B4
		internal static string GetTableName(Type type)
		{
			string result = Settings.GetTableMapping(type);
			int tableInstance = Settings.GetTableInstance();
			Dictionary<int, string> dictionary;
			if (Settings.tableMapping.TryGetValue(type, out dictionary))
			{
				dictionary.TryGetValue(tableInstance, out result);
			}
			return result;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005FE8 File Offset: 0x000041E8
		internal static int GetTableInstance(int targetHour)
		{
			return targetHour / Settings.ProbeResultHistoryWindowSize % Settings.numberOfHistoricalTables;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006004 File Offset: 0x00004204
		internal static int GetTableInstance()
		{
			return Settings.GetTableInstance(DateTime.UtcNow.Hour);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006024 File Offset: 0x00004224
		internal static int GetNextTableInstance()
		{
			int tableInstance = Settings.GetTableInstance();
			return (tableInstance + 1) % Settings.numberOfHistoricalTables;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006044 File Offset: 0x00004244
		internal static string GetTableMapping(Type tableType)
		{
			TableAttribute tableAttribute = (TableAttribute)tableType.GetCustomAttributes(typeof(TableAttribute), false).Single<object>();
			return tableAttribute.Name ?? tableType.Name;
		}

		// Token: 0x0400001C RID: 28
		private static readonly string registryPath = string.Format("SOFTWARE\\Microsoft\\ExchangeServer\\{0}\\WorkerTaskFramework\\Configuration", "v15");

		// Token: 0x0400001D RID: 29
		private static readonly Func<string, int> Int32Converter = (string s) => Convert.ToInt32(s);

		// Token: 0x0400001E RID: 30
		private static readonly Func<string, uint> UInt32Converter = (string s) => Convert.ToUInt32(s);

		// Token: 0x0400001F RID: 31
		private static readonly Func<string, long> Int64Converter = (string s) => Convert.ToInt64(s);

		// Token: 0x04000020 RID: 32
		private static readonly Func<string, bool> BooleanConverter = (string s) => Convert.ToBoolean(s);

		// Token: 0x04000021 RID: 33
		private static readonly Func<string, string> NoOpConverter = (string s) => s;

		// Token: 0x04000022 RID: 34
		private static int numberOfHistoricalTables = 3;

		// Token: 0x04000023 RID: 35
		private static Dictionary<Type, Dictionary<int, string>> tableMapping = new Dictionary<Type, Dictionary<int, string>>();

		// Token: 0x04000024 RID: 36
		private static OverridableSetting<string> fileLocation = new OverridableSetting<string>("FileStorageLocation", null, Settings.NoOpConverter, false);

		// Token: 0x04000025 RID: 37
		private static OverridableSetting<string> connectionString = new OverridableSetting<string>("SqlConnectionString", null, Settings.NoOpConverter, false);

		// Token: 0x04000026 RID: 38
		private static OverridableSetting<string> topologyConnectionString = new OverridableSetting<string>("TopologyConnectionString", null, Settings.NoOpConverter, false);

		// Token: 0x04000027 RID: 39
		private static OverridableSetting<bool> isUseCurrentUserHiveForManagedAvailability = new OverridableSetting<bool>("IsUseCurrentUserHiveForManagedAvailability", false, Settings.BooleanConverter, false);

		// Token: 0x04000028 RID: 40
		private static string machineName = System.Environment.MachineName;

		// Token: 0x04000029 RID: 41
		private static OverridableSetting<int> throttleAmount = new OverridableSetting<int>("ThrottleAmountInMilliSeconds", 50, Settings.Int32Converter, true);

		// Token: 0x0400002A RID: 42
		private static OverridableSetting<int> waitForWorkAmount = new OverridableSetting<int>("WaitForWorkAmountInMilliSeconds", 25, Settings.Int32Converter, true);

		// Token: 0x0400002B RID: 43
		private static OverridableSetting<int> maxWorkitemBatchSize = new OverridableSetting<int>("MaxWorkitemBatchSize", 5, Settings.Int32Converter, true);

		// Token: 0x0400002C RID: 44
		private static OverridableSetting<int> maxRunningTasks = new OverridableSetting<int>("MaxRunningTasks", 10, Settings.Int32Converter, true);

		// Token: 0x0400002D RID: 45
		private static OverridableSetting<int> waitAmountBeforeRestartRequest = new OverridableSetting<int>("WaitAmountBeforeRestartRequest", 10000, Settings.Int32Converter, true);

		// Token: 0x0400002E RID: 46
		private static OverridableSetting<int> workItemRetrievalDelay = new OverridableSetting<int>("WorkItemRetrievalDelay", 100, Settings.Int32Converter, true);

		// Token: 0x0400002F RID: 47
		private static OverridableSetting<int> resultHistoryWindowInMinutes = new OverridableSetting<int>("ResultHistoryWindowInMinutes", 60, Settings.Int32Converter, true);

		// Token: 0x04000030 RID: 48
		private static OverridableSetting<int> probeResultHistoryWindowSize = new OverridableSetting<int>("ProbeResultHistoryWindowSize", 30, Settings.Int32Converter, true);

		// Token: 0x04000031 RID: 49
		private static OverridableSetting<int> monitorResultHistoryWindowSize = new OverridableSetting<int>("MonitorResultHistoryWindowSize", 30, Settings.Int32Converter, true);

		// Token: 0x04000032 RID: 50
		private static OverridableSetting<int> responderResultHistoryWindowSize = new OverridableSetting<int>("ResponderResultHistoryWindowSize", 30, Settings.Int32Converter, true);

		// Token: 0x04000033 RID: 51
		private static OverridableSetting<int> maintenanceResultHistoryWindowSize = new OverridableSetting<int>("MaintenanceResultHistoryWindowSize", 30, Settings.Int32Converter, true);

		// Token: 0x04000034 RID: 52
		private static OverridableSetting<string> smtpServerName = new OverridableSetting<string>("SmtpServerName", null, Settings.NoOpConverter, true);

		// Token: 0x04000035 RID: 53
		private static OverridableSetting<int> smtpServerPort = new OverridableSetting<int>("Port", 25, Settings.Int32Converter, true);

		// Token: 0x04000036 RID: 54
		private static OverridableSetting<string> sendMailAddress = new OverridableSetting<string>("SenderMailAddress", null, Settings.NoOpConverter, true);

		// Token: 0x04000037 RID: 55
		private static OverridableSetting<string> instanceName = new OverridableSetting<string>("InstanceName", string.Empty, Settings.NoOpConverter, true);

		// Token: 0x04000038 RID: 56
		private static OverridableSetting<string> hostedServiceName = new OverridableSetting<string>("HostedServiceName", null, Settings.NoOpConverter, false);

		// Token: 0x04000039 RID: 57
		private static OverridableSetting<string> deploymentName = new OverridableSetting<string>("DeploymentName", null, Settings.NoOpConverter, false);

		// Token: 0x0400003A RID: 58
		private static OverridableSetting<int> deploymentId = new OverridableSetting<int>("DeploymentId", 0, Settings.Int32Converter, false);

		// Token: 0x0400003B RID: 59
		private static OverridableSetting<string> scope = new OverridableSetting<string>("Scope", null, Settings.NoOpConverter, false);

		// Token: 0x0400003C RID: 60
		private static OverridableSetting<string> workload = new OverridableSetting<string>("Workload", null, Settings.NoOpConverter, false);

		// Token: 0x0400003D RID: 61
		private static string uniformWorkload;

		// Token: 0x0400003E RID: 62
		private static OverridableSetting<string> workloadVersion = new OverridableSetting<string>("ExchangeVersion", null, Settings.NoOpConverter, false);

		// Token: 0x0400003F RID: 63
		private static OverridableSetting<string> environment = new OverridableSetting<string>("Environment", null, Settings.NoOpConverter, false);

		// Token: 0x04000040 RID: 64
		private static OverridableSetting<string> overrideProbeExecutionLocation = new OverridableSetting<string>("OverrideProbeExecutionLocation", null, Settings.NoOpConverter, false);

		// Token: 0x04000041 RID: 65
		private static OverridableSetting<string> escalationEndpoint = new OverridableSetting<string>("EscalationEndpoint", null, Settings.NoOpConverter, false);

		// Token: 0x04000042 RID: 66
		private static OverridableSetting<int> quarantineHours = new OverridableSetting<int>("QuarantineHours", 24, Settings.Int32Converter, true);

		// Token: 0x04000043 RID: 67
		private static OverridableSetting<int> maxPoisonCount = new OverridableSetting<int>("MaxPoisonCount", 5, Settings.Int32Converter, true);

		// Token: 0x04000044 RID: 68
		private static OverridableSetting<int> nonRecurrentRetryIntervalSeconds = new OverridableSetting<int>("NonRecurrentRetryIntervalSeconds", 5, Settings.Int32Converter, true);

		// Token: 0x04000045 RID: 69
		private static OverridableSetting<int> bulkInsertBatchSize = new OverridableSetting<int>("BulkInsertBatchSize", 500, Settings.Int32Converter, false);

		// Token: 0x04000046 RID: 70
		private static OverridableSetting<int> consecutiveMaintenanceFailureThreshold = new OverridableSetting<int>("ConsecutiveMaintenanceFailureThreshold", 3, Settings.Int32Converter, true);

		// Token: 0x04000047 RID: 71
		private static OverridableSetting<int?> serviceRestartDelay = new OverridableSetting<int?>("ServiceRestartDelay", null, (string s) => new int?(Convert.ToInt32(s)), true);

		// Token: 0x04000048 RID: 72
		private static OverridableSetting<bool> isProductionEnvironment = new OverridableSetting<bool>("IsProductionEnvironment", true, Settings.BooleanConverter, false);

		// Token: 0x04000049 RID: 73
		private static OverridableSetting<bool> tracingCredentials = new OverridableSetting<bool>("TracingCredentials", false, Settings.BooleanConverter, true);

		// Token: 0x0400004A RID: 74
		private static OverridableSetting<bool> useE14MonitoringTenant = new OverridableSetting<bool>("UseE14MonitoringTenant", false, Settings.BooleanConverter, true);

		// Token: 0x0400004B RID: 75
		private static OverridableSetting<int> maxObservers = new OverridableSetting<int>("MaxObservers", 3, Settings.Int32Converter, true);

		// Token: 0x0400004C RID: 76
		private static OverridableSetting<int> maxSubjects = new OverridableSetting<int>("MaxSubjects", 10, Settings.Int32Converter, true);

		// Token: 0x0400004D RID: 77
		private static OverridableSetting<int> maxRequestObservers = new OverridableSetting<int>("MaxRequestObservers", 10, Settings.Int32Converter, true);

		// Token: 0x0400004E RID: 78
		private static OverridableSetting<int> maxZombieSubjects = new OverridableSetting<int>("MaxZombieSubjects", 20, Settings.Int32Converter, true);

		// Token: 0x0400004F RID: 79
		private static OverridableSetting<int> maxMdbPerCasServer = new OverridableSetting<int>("MaxMdbPerCasServer", 200, Settings.Int32Converter, true);

		// Token: 0x04000050 RID: 80
		private static OverridableSetting<string> httpProxyAvailabilityGroup = new OverridableSetting<string>("HttpProxyAvailabilityGroup", null, Settings.NoOpConverter, true);

		// Token: 0x04000051 RID: 81
		private static OverridableSetting<bool> isResultsLoggingEnabled = new OverridableSetting<bool>("IsResultsLoggingEnabled", false, Settings.BooleanConverter, true);

		// Token: 0x04000052 RID: 82
		private static OverridableSetting<bool> isTraceLoggingEnabled = new OverridableSetting<bool>("IsTraceLoggingEnabled", false, Settings.BooleanConverter, true);

		// Token: 0x04000053 RID: 83
		private static OverridableSetting<string> defaultResultsLogPath = new OverridableSetting<string>("DefaultResultsLogPath", null, Settings.NoOpConverter, true);

		// Token: 0x04000054 RID: 84
		private static OverridableSetting<string> defaultTraceLogPath = new OverridableSetting<string>("DefaultTraceLogPath", null, Settings.NoOpConverter, true);

		// Token: 0x04000055 RID: 85
		private static OverridableSetting<int> maxLogAge = new OverridableSetting<int>("MaxLogAge", 7, Settings.Int32Converter, true);

		// Token: 0x04000056 RID: 86
		private static OverridableSetting<int> maxResultsLogDirectorySizeInBytes = new OverridableSetting<int>("MaxResultsLogDirectorySizeInBytes", 209715200, Settings.Int32Converter, true);

		// Token: 0x04000057 RID: 87
		private static OverridableSetting<int> maxResultsLogFileSizeInBytes = new OverridableSetting<int>("MaxResultsLogFileSizeInBytes", 104857600, Settings.Int32Converter, true);

		// Token: 0x04000058 RID: 88
		private static OverridableSetting<long> maxTraceLogsDirectorySizeInBytes = new OverridableSetting<long>("MaxTraceLogDirectorySizeInBytes", (long)((ulong)int.MinValue), Settings.Int64Converter, true);

		// Token: 0x04000059 RID: 89
		private static OverridableSetting<long> maxTraceLogFileSizeInBytes = new OverridableSetting<long>("MaxTraceLogFileSizeInBytes", 268435456L, Settings.Int64Converter, true);

		// Token: 0x0400005A RID: 90
		private static OverridableSetting<int> maxPersistentStateDirectorySizeInBytes = new OverridableSetting<int>("MaxPersistentStateDirectorySizeInBytes", 209715200, Settings.Int32Converter, true);

		// Token: 0x0400005B RID: 91
		private static OverridableSetting<int> maxPersistentStateFileSizeInBytes = new OverridableSetting<int>("MaxProbePersistentStateFileSizeInBytes", 52428800, Settings.Int32Converter, true);

		// Token: 0x0400005C RID: 92
		private static OverridableSetting<int> resultsLogBufferSizeInBytes = new OverridableSetting<int>("ResultsLogBufferSizeInBytes", 524288, Settings.Int32Converter, true);

		// Token: 0x0400005D RID: 93
		private static OverridableSetting<int> resultsLogFlushIntervalInMinutes = new OverridableSetting<int>("ResultsLogFlushIntervalInMinutes", 15, Settings.Int32Converter, true);

		// Token: 0x0400005E RID: 94
		private static OverridableSetting<int> traceLogBufferSizeInBytes = new OverridableSetting<int>("TraceLogBufferSizeInBytes", 1024, Settings.Int32Converter, true);

		// Token: 0x0400005F RID: 95
		private static OverridableSetting<int> traceLogFlushIntervalInMinutes = new OverridableSetting<int>("TraceLogFlushIntervalInMinutes", 1, Settings.Int32Converter, true);

		// Token: 0x04000060 RID: 96
		private static OverridableSetting<bool> enableStreamInsightPush = new OverridableSetting<bool>("EnableStreamInsightPush", false, Settings.BooleanConverter, true);

		// Token: 0x04000061 RID: 97
		private static OverridableSetting<string> streamInsightServerAddress = new OverridableSetting<string>("StreamInsightServerAddress", null, Settings.NoOpConverter, true);

		// Token: 0x04000062 RID: 98
		private static OverridableSetting<bool> enableOAuth = new OverridableSetting<bool>("EnableOAuth", false, Settings.BooleanConverter, true);

		// Token: 0x04000063 RID: 99
		private static OverridableSetting<string> authTenantName = new OverridableSetting<string>("OAuthTenantName", null, Settings.NoOpConverter, true);

		// Token: 0x04000064 RID: 100
		private static OverridableSetting<string> authAppId = new OverridableSetting<string>("OAuthAppId", null, Settings.NoOpConverter, true);

		// Token: 0x04000065 RID: 101
		private static OverridableSetting<string> authSymmetricKey = new OverridableSetting<string>("OAuthSymmetricKey", null, Settings.NoOpConverter, true);

		// Token: 0x04000066 RID: 102
		private static OverridableSetting<string> authCertificateName = new OverridableSetting<string>("OAuthCertificateName", null, Settings.NoOpConverter, true);

		// Token: 0x04000067 RID: 103
		private static OverridableSetting<string> streamInsightXamServerName = new OverridableSetting<string>("StreamInsightXamServerName", null, Settings.NoOpConverter, true);

		// Token: 0x04000068 RID: 104
		private static OverridableSetting<string> streamInsightXamDatabaseName = new OverridableSetting<string>("StreamInsightXamServerName", null, Settings.NoOpConverter, true);

		// Token: 0x04000069 RID: 105
		private static OverridableSetting<string> keynoteDataFeedBaseUrl = new OverridableSetting<string>("KeynoteDataFeedBaseUrl", null, Settings.NoOpConverter, true);

		// Token: 0x0400006A RID: 106
		private static OverridableSetting<string> keynoteDataPulseBaseUrl = new OverridableSetting<string>("KeynoteDataPulseBaseUrl", null, Settings.NoOpConverter, true);

		// Token: 0x0400006B RID: 107
		private static OverridableSetting<string> keynoteDataFeedAgreementIdUserMapping = new OverridableSetting<string>("KeynoteDataFeedAgreementIdUserMapping", null, Settings.NoOpConverter, true);

		// Token: 0x0400006C RID: 108
		private static OverridableSetting<string> keynoteDataFeedBiz40AgreementIdUserMapping = new OverridableSetting<string>("KeynoteDataFeedBiz40AgreementIdUserMapping", null, Settings.NoOpConverter, true);

		// Token: 0x0400006D RID: 109
		private static OverridableSetting<string> keynoteDataPulseAgreementIdUserMapping = new OverridableSetting<string>("KeynoteDataPulseAgreementIdUserMapping", null, Settings.NoOpConverter, true);

		// Token: 0x0400006E RID: 110
		private static OverridableSetting<string> keynoteDataPulseBiz40AgreementIdUserMapping = new OverridableSetting<string>("KeynoteDataPulseBiz40AgreementIdUserMapping", null, Settings.NoOpConverter, true);

		// Token: 0x0400006F RID: 111
		private static OverridableSetting<string> keynoteCredentials = new OverridableSetting<string>("KeynoteCredentials", null, Settings.NoOpConverter, true);

		// Token: 0x04000070 RID: 112
		private static OverridableSetting<string> remotePowershellCertSubject = new OverridableSetting<string>("RemotePowershellCertSubject", null, Settings.NoOpConverter, true);

		// Token: 0x04000071 RID: 113
		private static OverridableSetting<int> numberOfLastProbeResults = new OverridableSetting<int>("numberOfLastProbeResults", 5, Settings.Int32Converter, true);

		// Token: 0x04000072 RID: 114
		private static OverridableSetting<int> numberOfLastMonitorResults = new OverridableSetting<int>("numberOfLastMonitorResults", 1, Settings.Int32Converter, true);

		// Token: 0x04000073 RID: 115
		private static OverridableSetting<int> numberOfLastResponderResults = new OverridableSetting<int>("numberOfLastResponderResults", 5, Settings.Int32Converter, true);

		// Token: 0x04000074 RID: 116
		private static OverridableSetting<int> numberOfLastMaintenanceResults = new OverridableSetting<int>("numberOfLastMaintenanceResults", 5, Settings.Int32Converter, true);

		// Token: 0x04000075 RID: 117
		private static OverridableSetting<bool> isPersistentStateEnabled = new OverridableSetting<bool>("IsPersistentStateEnabled", false, Settings.BooleanConverter, true);

		// Token: 0x04000076 RID: 118
		private static OverridableSetting<bool> isCancelWorkItemsOnQuitRequestFeatureEnabled = new OverridableSetting<bool>("IsCancelWorkItemsOnQuitRequestFeatureEnabled", true, Settings.BooleanConverter, true);

		// Token: 0x04000077 RID: 119
		private static OverridableSetting<string> probeSvcAddress = new OverridableSetting<string>("ProbeSvcAddress", null, Settings.NoOpConverter, true);

		// Token: 0x04000078 RID: 120
		private static OverridableSetting<string> accessControlHostName = new OverridableSetting<string>("AccessControlHostName", null, Settings.NoOpConverter, true);

		// Token: 0x04000079 RID: 121
		private static OverridableSetting<string> accessControlNamespace = new OverridableSetting<string>("AccessControlNamespace", null, Settings.NoOpConverter, true);

		// Token: 0x0400007A RID: 122
		private static OverridableSetting<string> accessControlSigningCertificateFilePath = new OverridableSetting<string>("AccessControlSigningCertificateFilePath", null, Settings.NoOpConverter, true);

		// Token: 0x0400007B RID: 123
		private static OverridableSetting<string> probeSvcCertificateFilePath = new OverridableSetting<string>("ProbeSvcCertificateFilePath", null, Settings.NoOpConverter, true);

		// Token: 0x0400007C RID: 124
		private static OverridableSetting<string> probeSvcCertificateDN = new OverridableSetting<string>("ProbeSvcCertificateDN", null, Settings.NoOpConverter, true);

		// Token: 0x0400007D RID: 125
		private static OverridableSetting<string> probeSvcClientUsername = new OverridableSetting<string>("ProbeSvcClientUsername", null, Settings.NoOpConverter, true);

		// Token: 0x0400007E RID: 126
		private static OverridableSetting<string> probeSvcClientPassword = new OverridableSetting<string>("ProbeSvcClientPassword", null, Settings.NoOpConverter, true);

		// Token: 0x0400007F RID: 127
		private static OverridableSetting<bool> useSynchronousContinuationForWorkitemResults = new OverridableSetting<bool>("UseSynchronousContinuationForWorkitemResults", true, Settings.BooleanConverter, true);

		// Token: 0x04000080 RID: 128
		private static OverridableSetting<bool> calculateTimeoutFromBeginningOfExecution = new OverridableSetting<bool>("CalculateTimeoutFromBeginningOfExecution", true, Settings.BooleanConverter, true);

		// Token: 0x04000081 RID: 129
		private static OverridableSetting<string> staticMonitoringPassword = new OverridableSetting<string>("StaticMonitoringPassword", null, Settings.NoOpConverter, true);

		// Token: 0x04000082 RID: 130
		private static OverridableSetting<bool> runningAsConsoleHost = new OverridableSetting<bool>("RunningAsConsoleHost", false, Settings.BooleanConverter, true);

		// Token: 0x04000083 RID: 131
		private static OverridableSetting<string> sqlReadOnlyUser = new OverridableSetting<string>("SqlReadOnlyUser", null, Settings.NoOpConverter, true);

		// Token: 0x04000084 RID: 132
		private static OverridableSetting<string> sqlReadOnlyPassword = new OverridableSetting<string>("SqlReadOnlyPassword", null, Settings.NoOpConverter, true);

		// Token: 0x04000085 RID: 133
		private static OverridableSetting<string> dataInsightSqlReadonlyUsername = new OverridableSetting<string>("DiSqlReadonlyUsername", null, Settings.NoOpConverter, true);

		// Token: 0x04000086 RID: 134
		private static OverridableSetting<string> dataInsightSqlReadonlyPassword = new OverridableSetting<string>("DiSqlReadonlyPassword", null, Settings.NoOpConverter, true);

		// Token: 0x04000087 RID: 135
		private static OverridableSetting<string> sqlReadWriteUser = new OverridableSetting<string>("SqlReadWriteUser", null, Settings.NoOpConverter, true);

		// Token: 0x04000088 RID: 136
		private static OverridableSetting<string> sqlReadWritePassword = new OverridableSetting<string>("SqlReadWritePassword", null, Settings.NoOpConverter, true);

		// Token: 0x04000089 RID: 137
		private static OverridableSetting<string> opticsServer = new OverridableSetting<string>("OpticsServer", null, Settings.NoOpConverter, true);

		// Token: 0x0400008A RID: 138
		private static OverridableSetting<string> systemMonitoringInstance = new OverridableSetting<string>("SystemMonitoringInstance", null, Settings.NoOpConverter, true);

		// Token: 0x0400008B RID: 139
		private static OverridableSetting<string> orcaVdir = new OverridableSetting<string>("OrcaVirtualDirectory", null, Settings.NoOpConverter, true);

		// Token: 0x0400008C RID: 140
		private static OverridableSetting<string> orcaUrlSuffix = new OverridableSetting<string>("ORCAUrlSuffix", null, Settings.NoOpConverter, true);

		// Token: 0x0400008D RID: 141
		private static OverridableSetting<string> orcaATMUrl = new OverridableSetting<string>("ORCAATMUrl", null, Settings.NoOpConverter, true);

		// Token: 0x0400008E RID: 142
		private static OverridableSetting<string> orcaClientCredential = new OverridableSetting<string>("OrcaClientCredential", null, Settings.NoOpConverter, true);

		// Token: 0x0400008F RID: 143
		private static OverridableSetting<int> batchManagerBatchSize = new OverridableSetting<int>("BatchManagerBatchSize", 1, Settings.Int32Converter, true);

		// Token: 0x04000090 RID: 144
		private static OverridableSetting<int> batchManagerBatchWaitTimeSeconds = new OverridableSetting<int>("BatchManagerBatchWaitTimeSeconds", 60, Settings.Int32Converter, true);

		// Token: 0x04000091 RID: 145
		private static OverridableSetting<bool> useTransactionsInWorkItemGeneration = new OverridableSetting<bool>("UseTransactionsInWorkItemGeneration", true, Settings.BooleanConverter, true);

		// Token: 0x04000092 RID: 146
		private static OverridableSetting<int> cafeMailboxes = new OverridableSetting<int>("CafeMailboxes", 10, Settings.Int32Converter, true);

		// Token: 0x04000093 RID: 147
		private static OverridableSetting<int> maintenanceTimeoutWatsonHours = new OverridableSetting<int>("MaintenanceTimeoutWatsonHours", 0, Settings.Int32Converter, true);

		// Token: 0x04000094 RID: 148
		private static OverridableSetting<bool> isCortex = new OverridableSetting<bool>("IsCortex", false, Settings.BooleanConverter, false);

		// Token: 0x04000095 RID: 149
		private static OverridableSetting<string> azureTableConnectionString = new OverridableSetting<string>("DataConnectionString", null, Settings.NoOpConverter, false);

		// Token: 0x04000096 RID: 150
		private static OverridableSetting<int> maxNumberOfWorkUnits = new OverridableSetting<int>("MaxNumberOfWorkUnits", int.MaxValue, Settings.Int32Converter, true);

		// Token: 0x04000097 RID: 151
		private static OverridableSetting<int> maxWorkUnitCost = new OverridableSetting<int>("MaxWorkUnitCost", int.MaxValue, Settings.Int32Converter, true);

		// Token: 0x04000098 RID: 152
		private static OverridableSetting<int> maxRecoveryAttempts = new OverridableSetting<int>("MaxRecoveryAttempts", 5, Settings.Int32Converter, true);

		// Token: 0x04000099 RID: 153
		private static OverridableSetting<int> recoveryThrottleTimeInSeconds = new OverridableSetting<int>("RecoveryThrottleTimeInSeconds", 900, Settings.Int32Converter, true);

		// Token: 0x0400009A RID: 154
		private static OverridableSetting<bool> restartOnPoisonedWorkItem = new OverridableSetting<bool>("RestartOnPoisonedWorkItem", true, Settings.BooleanConverter, false);

		// Token: 0x0400009B RID: 155
		private static OverridableSetting<string> dataPartition = new OverridableSetting<string>("DataPartition", null, Settings.NoOpConverter, false);

		// Token: 0x0400009C RID: 156
		private static OverridableSetting<string> cortexDataPartitionRingKey = new OverridableSetting<string>("CortexDataPartitionRingKey", null, Settings.NoOpConverter, false);

		// Token: 0x0400009D RID: 157
		private static OverridableSetting<string> orcaHubConnectionString = new OverridableSetting<string>("OrcaHubConnectionString", null, Settings.NoOpConverter, false);

		// Token: 0x0400009E RID: 158
		private static OverridableSetting<string> orcaSpokeConnectionString = new OverridableSetting<string>("OrcaSpokeConnectionString", null, Settings.NoOpConverter, false);

		// Token: 0x0400009F RID: 159
		private static OverridableSetting<string> orcaSpokeIdentity = new OverridableSetting<string>("OrcaSpokeIdentity", null, Settings.NoOpConverter, false);
	}
}
