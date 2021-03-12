using System;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Win32;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000020 RID: 32
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ServiceConfiguration
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00003DB4 File Offset: 0x00001FB4
		internal ServiceConfiguration(ConfigurationPropertyBag propertyBag)
		{
			propertyBag.Freeze();
			this.propertyBag = propertyBag;
			this.idleConnectionCheckPeriod = propertyBag.Get<TimeSpan>(ServiceConfiguration.Schema.IdleConnectionCheckPeriod);
			this.logConnectionLatencyCheckPeriod = propertyBag.Get<TimeSpan>(ServiceConfiguration.Schema.LogConnectionLatencyCheckPeriod);
			this.maintenanceJobTimerCheckPeriod = propertyBag.Get<TimeSpan>(ServiceConfiguration.Schema.MaintenanceJobTimerCheckPeriod);
			this.shareConnections = propertyBag.Get<bool>(ServiceConfiguration.Schema.ShareConnections);
			this.enableExMonTestMode = propertyBag.Get<bool>(ServiceConfiguration.Schema.EnableExMonTestMode);
			this.fastTransferMaxRequests = propertyBag.Get<int>(ServiceConfiguration.Schema.FastTransferMaxRequests);
			this.fastTransferBackoffInterval = propertyBag.Get<int>(ServiceConfiguration.Schema.FastTransferBackoffInterval);
			this.fastTransferBackoffRetryCount = propertyBag.Get<int>(ServiceConfiguration.Schema.FastTransferBackoffRetryCount);
			this.rpcPollsMax = propertyBag.Get<TimeSpan>(ServiceConfiguration.Schema.RpcPollsMax);
			this.rpcRetryCount = propertyBag.Get<int>(ServiceConfiguration.Schema.RpcRetryCount);
			this.rpcRetryDelay = propertyBag.Get<TimeSpan>(ServiceConfiguration.Schema.RpcRetryDelay);
			this.maxRandomAdditionalRpcRetryDelay = propertyBag.Get<TimeSpan>(ServiceConfiguration.Schema.MaxRandomAdditionalRpcRetryDelay);
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00003EA0 File Offset: 0x000020A0
		public TimeSpan ADUserDataCacheTimeout
		{
			get
			{
				return this.propertyBag.Get<TimeSpan>(ServiceConfiguration.Schema.ADUserDataCacheTimeout);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003EB2 File Offset: 0x000020B2
		public bool CanServicePrivateLogons
		{
			get
			{
				return this.propertyBag.Get<bool>(ServiceConfiguration.Schema.IsRpcClientAccessObjectPresent) && this.propertyBag.Get<bool>(ServiceConfiguration.Schema.IsClientAccessRole);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00003ED8 File Offset: 0x000020D8
		public bool CanServicePublicLogons
		{
			get
			{
				return this.propertyBag.Get<bool>(ServiceConfiguration.Schema.IsRpcClientAccessObjectPresent) && this.propertyBag.Get<bool>(ServiceConfiguration.Schema.IsClientAccessRole);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003EFE File Offset: 0x000020FE
		public bool CanServiceRecoveryDatabaseLogons
		{
			get
			{
				return this.propertyBag.Get<bool>(ServiceConfiguration.Schema.IsMailboxRole);
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003F10 File Offset: 0x00002110
		public bool IsClientVersionAllowed(MapiVersion version)
		{
			MapiVersionRanges mapiVersionRanges = this.propertyBag.Get<MapiVersionRanges>(ServiceConfiguration.Schema.BlockedClientVersions);
			return mapiVersionRanges == null || !mapiVersionRanges.IsIncluded(version);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003F40 File Offset: 0x00002140
		public bool IsClientVersionAllowedInForest(MapiVersion requestedClientVersion)
		{
			ClientVersionCollection clientVersionCollection = this.propertyBag.Get<ClientVersionCollection>(ServiceConfiguration.Schema.RequiredVersionCollection);
			if (clientVersionCollection == null)
			{
				return true;
			}
			ushort[] array = requestedClientVersion.ToQuartet();
			Version version = new Version((int)array[0], (int)array[1], (int)array[2], (int)array[3]);
			return version < ServiceConfiguration.MinimumForcedUpgradeClientVersion || clientVersionCollection.IsClientVersionSufficient(version);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00003F93 File Offset: 0x00002193
		public bool IsEncryptionRequired
		{
			get
			{
				return this.propertyBag.Get<bool>(ServiceConfiguration.Schema.IsEncryptionRequired);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00003FA5 File Offset: 0x000021A5
		public bool IsServiceEnabled
		{
			get
			{
				return this.CanServicePrivateLogons || this.CanServicePublicLogons;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00003FB7 File Offset: 0x000021B7
		public bool IsDisabledOnMailboxRole
		{
			get
			{
				return this.propertyBag.Get<bool>(ServiceConfiguration.Schema.IsMailboxRole) && !this.propertyBag.Get<bool>(ServiceConfiguration.Schema.CanRunOnMailboxRole);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00003FE0 File Offset: 0x000021E0
		public bool LogEveryConfigurationUpdate
		{
			get
			{
				return this.propertyBag.Get<bool>(ServiceConfiguration.Schema.LogEveryConfigurationUpdate);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00003FF2 File Offset: 0x000021F2
		public int MaximumConnections
		{
			get
			{
				return this.propertyBag.Get<int>(ServiceConfiguration.Schema.MaximumConnections);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004004 File Offset: 0x00002204
		public TimeSpan IdleConnectionCheckPeriod
		{
			get
			{
				return this.idleConnectionCheckPeriod;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000400C File Offset: 0x0000220C
		public TimeSpan LogConnectionLatencyCheckPeriod
		{
			get
			{
				return this.logConnectionLatencyCheckPeriod;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004014 File Offset: 0x00002214
		public TimeSpan MaintenanceJobTimerCheckPeriod
		{
			get
			{
				return this.maintenanceJobTimerCheckPeriod;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x0000401C File Offset: 0x0000221C
		public TimeSpan WaitBetweenTcpConnectToFindIfRpcServiceResponsive
		{
			get
			{
				return this.propertyBag.Get<TimeSpan>(ServiceConfiguration.Schema.WaitBetweenTcpConnectToFindIfRpcServiceResponsive);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000402E File Offset: 0x0000222E
		public Fqdn ThisServerFqdn
		{
			get
			{
				return this.propertyBag.Get<Fqdn>(ServiceConfiguration.Schema.ThisServerFqdn);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004040 File Offset: 0x00002240
		public LegacyDN ThisServerLegacyDN
		{
			get
			{
				return this.propertyBag.Get<LegacyDN>(ServiceConfiguration.Schema.ThisServerLegacyDN);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004052 File Offset: 0x00002252
		public bool ShareConnections
		{
			get
			{
				return this.shareConnections;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000405A File Offset: 0x0000225A
		public bool EnableExMonTestMode
		{
			get
			{
				return this.enableExMonTestMode;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004062 File Offset: 0x00002262
		public bool EnablePreferredSiteEnforcement
		{
			get
			{
				return this.propertyBag.Get<bool>(ServiceConfiguration.Schema.EnablePreferredSiteEnforcement);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004074 File Offset: 0x00002274
		public int FastTransferMaxRequests
		{
			get
			{
				return this.fastTransferMaxRequests;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000407C File Offset: 0x0000227C
		public int FastTransferBackoffInterval
		{
			get
			{
				return this.fastTransferBackoffInterval;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00004084 File Offset: 0x00002284
		public int FastTransferBackoffRetryCount
		{
			get
			{
				return this.fastTransferBackoffRetryCount;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000408C File Offset: 0x0000228C
		public TimeSpan RpcPollsMax
		{
			get
			{
				return this.rpcPollsMax;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00004094 File Offset: 0x00002294
		public int RpcRetryCount
		{
			get
			{
				return this.rpcRetryCount;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000409C File Offset: 0x0000229C
		public TimeSpan RpcRetryDelay
		{
			get
			{
				return this.rpcRetryDelay;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000040A4 File Offset: 0x000022A4
		public TimeSpan MaxRandomAdditionalRpcRetryDelay
		{
			get
			{
				return this.maxRandomAdditionalRpcRetryDelay;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000040AC File Offset: 0x000022AC
		public bool EnableWebServicesEndpoint
		{
			get
			{
				return this.propertyBag.Get<bool>(ServiceConfiguration.Schema.IsXtcEnabled) || this.propertyBag.Get<bool>(ServiceConfiguration.Schema.EnableWebServicesEndpoint);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000040D2 File Offset: 0x000022D2
		public bool EnableSmartConnectionTearDown
		{
			get
			{
				return this.propertyBag.Get<bool>(ServiceConfiguration.Schema.EnableSmartConnectionTearDown);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000040E4 File Offset: 0x000022E4
		public bool EnableBlockInsufficientClientVersions
		{
			get
			{
				return this.propertyBag.Get<bool>(ServiceConfiguration.Schema.EnableBlockInsufficientClientVersions);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000040F6 File Offset: 0x000022F6
		public bool EnableWebServicesOrganizationRelationshipCheck
		{
			get
			{
				return this.propertyBag.Get<bool>(ServiceConfiguration.Schema.IsXtcEnabled);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00004108 File Offset: 0x00002308
		public bool AvailabilityServiceCallsDisabled
		{
			get
			{
				return this.propertyBag.Get<bool>(ServiceConfiguration.Schema.AvailabilityServiceCallsDisabled);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000411A File Offset: 0x0000231A
		public bool TMPublishEnabled
		{
			get
			{
				return this.propertyBag.Get<bool>(ServiceConfiguration.Schema.TMPublishEnabled);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000FC RID: 252 RVA: 0x0000412C File Offset: 0x0000232C
		public bool TMOAuthEnabled
		{
			get
			{
				return this.propertyBag.Get<bool>(ServiceConfiguration.Schema.TMOAuthEnabled);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000413E File Offset: 0x0000233E
		public bool TMPublishHttpDebugEnabled
		{
			get
			{
				return this.propertyBag.Get<bool>(ServiceConfiguration.Schema.TMPublishHttpDebugEnabled);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004150 File Offset: 0x00002350
		public TimeSpan TMPublishRequestTimeout
		{
			get
			{
				return this.propertyBag.Get<TimeSpan>(ServiceConfiguration.Schema.TMPublishRequestTimeout);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00004162 File Offset: 0x00002362
		public ICredentials TMPublishCredential
		{
			get
			{
				return this.propertyBag.Get<ICredentials>(ServiceConfiguration.Schema.TMPublishCredential);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004174 File Offset: 0x00002374
		public bool TMUseMockSharePointOperation
		{
			get
			{
				return this.propertyBag.Get<bool>(ServiceConfiguration.Schema.TMUseMockSharePointOperation);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00004186 File Offset: 0x00002386
		public int TMPublishConcurrentOperationLimit
		{
			get
			{
				return this.propertyBag.Get<int>(ServiceConfiguration.Schema.TMPublishConcurrentOperationLimit);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004198 File Offset: 0x00002398
		public MapiVersion TMRequiredMAPIClientVersion
		{
			get
			{
				return this.propertyBag.Get<MapiVersion>(ServiceConfiguration.Schema.TMRequiredMAPIClientVersion);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000041AA File Offset: 0x000023AA
		public int MaximumRpcTasks
		{
			get
			{
				return this.propertyBag.Get<int>(ServiceConfiguration.Schema.MaximumRpcTasks);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000041BC File Offset: 0x000023BC
		public int MaximumRpcThreads
		{
			get
			{
				return this.propertyBag.Get<int>(ServiceConfiguration.Schema.MaximumRpcThreads);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000041CE File Offset: 0x000023CE
		public int MinimumRpcThreads
		{
			get
			{
				return Math.Min(this.propertyBag.Get<int>(ServiceConfiguration.Schema.MinimumRpcThreads), this.MaximumRpcThreads);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000041EB File Offset: 0x000023EB
		public int MaximumWebServiceTasks
		{
			get
			{
				return this.propertyBag.Get<int>(ServiceConfiguration.Schema.MaximumWebServiceTasks);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000041FD File Offset: 0x000023FD
		public int MaximumWebServiceThreads
		{
			get
			{
				return this.propertyBag.Get<int>(ServiceConfiguration.Schema.MaximumWebServiceThreads);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000420F File Offset: 0x0000240F
		public int MinimumWebServiceThreads
		{
			get
			{
				return Math.Min(this.propertyBag.Get<int>(ServiceConfiguration.Schema.MinimumWebServiceThreads), this.MaximumWebServiceThreads);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000422C File Offset: 0x0000242C
		public int MaximumRpcHttpConnectionRegistrationTasks
		{
			get
			{
				return this.propertyBag.Get<int>(ServiceConfiguration.Schema.MaximumRpcHttpConnectionRegistrationTasks);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000423E File Offset: 0x0000243E
		public int MaximumRpcHttpConnectionRegistrationThreads
		{
			get
			{
				return this.propertyBag.Get<int>(ServiceConfiguration.Schema.MaximumRpcHttpConnectionRegistrationThreads);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00004250 File Offset: 0x00002450
		public int MinimumRpcHttpConnectionRegistrationThreads
		{
			get
			{
				return Math.Min(this.propertyBag.Get<int>(ServiceConfiguration.Schema.MinimumRpcHttpConnectionRegistrationThreads), this.MaximumRpcHttpConnectionRegistrationThreads);
			}
		}

		// Token: 0x04000053 RID: 83
		public const string RpcClientAccessServiceName = "MSExchangeRPC";

		// Token: 0x04000054 RID: 84
		public const int DefaultMaximumRpcTasks = 5000;

		// Token: 0x04000055 RID: 85
		public const int DefaultMaximumRpcThreads = 250;

		// Token: 0x04000056 RID: 86
		public const int DefaultMaximumRpcThreadProcessorFactor = 3;

		// Token: 0x04000057 RID: 87
		public const int DefaultMinimumRpcThreadProcessorFactor = 1;

		// Token: 0x04000058 RID: 88
		public const int DefaultMinimumRpcThreads = 4;

		// Token: 0x04000059 RID: 89
		public const int DefaultMaximumWebServiceTasks = 1000;

		// Token: 0x0400005A RID: 90
		public const int DefaultMaximumWebServiceThreads = 100;

		// Token: 0x0400005B RID: 91
		public const int DefaultMaximumWebServiceThreadProcessorFactor = 3;

		// Token: 0x0400005C RID: 92
		public const int DefaultMinimumWebServiceThreads = 2;

		// Token: 0x0400005D RID: 93
		public const int DefaultMaximumRpcHttpConnectionRegistrationTasks = 5000;

		// Token: 0x0400005E RID: 94
		public const int DefaultMaximumRpcHttpConnectionRegistrationThreads = 32;

		// Token: 0x0400005F RID: 95
		public const int DefaultMaximumRpcHttpConnectionRegistrationThreadProcessorFactor = 2;

		// Token: 0x04000060 RID: 96
		public const int DefaultMinimumRpcHttpConnectionRegistrationThreadProcessorFactor = 1;

		// Token: 0x04000061 RID: 97
		public const string RegKeyPathExchangeServer = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15";

		// Token: 0x04000062 RID: 98
		public const string RegKeyNameSIEngineURI = "SIEngineURI";

		// Token: 0x04000063 RID: 99
		public const string RegKeyNameSIUploaderQueueSize = "SIUploaderQueueSize";

		// Token: 0x04000064 RID: 100
		internal const int RpcRetryCountDefault = 6;

		// Token: 0x04000065 RID: 101
		public static Guid ComponentGuid = new Guid("53F12A79-F089-4312-9285-8CFDC77FB0A9");

		// Token: 0x04000066 RID: 102
		public static string Component = "RpcClientAccess";

		// Token: 0x04000067 RID: 103
		public static bool StreamInsightUploaderEnabled = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).RpcClientAccess.StreamInsightUploader.Enabled;

		// Token: 0x04000068 RID: 104
		public static string StreamInsightEngineURI = Convert.ToString(Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15", "SIEngineURI", "http://xsi-exo-pre-momt.cloudapp.net:10200/momt/dcs/"));

		// Token: 0x04000069 RID: 105
		public static int StreamInsightUploaderQueueSize = Convert.ToInt32(Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15", "SIUploaderQueueSize", 200));

		// Token: 0x0400006A RID: 106
		internal static readonly TimeSpan RpcPollsMaxDefault = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400006B RID: 107
		internal static readonly TimeSpan RpcRetryDelayDefault = TimeSpan.FromSeconds(10.0);

		// Token: 0x0400006C RID: 108
		internal static readonly TimeSpan MaxRandomAdditionalRpcRetryDelayDefault = TimeSpan.FromSeconds(10.0);

		// Token: 0x0400006D RID: 109
		private static readonly Version MinimumForcedUpgradeClientVersion = new Version(15, 0, 4569, 0);

		// Token: 0x0400006E RID: 110
		private readonly ConfigurationPropertyBag propertyBag;

		// Token: 0x0400006F RID: 111
		private readonly TimeSpan idleConnectionCheckPeriod;

		// Token: 0x04000070 RID: 112
		private readonly TimeSpan logConnectionLatencyCheckPeriod;

		// Token: 0x04000071 RID: 113
		private readonly TimeSpan maintenanceJobTimerCheckPeriod;

		// Token: 0x04000072 RID: 114
		private readonly bool shareConnections;

		// Token: 0x04000073 RID: 115
		private readonly bool enableExMonTestMode;

		// Token: 0x04000074 RID: 116
		private readonly int fastTransferMaxRequests;

		// Token: 0x04000075 RID: 117
		private readonly int fastTransferBackoffInterval;

		// Token: 0x04000076 RID: 118
		private readonly int fastTransferBackoffRetryCount;

		// Token: 0x04000077 RID: 119
		private readonly TimeSpan rpcPollsMax;

		// Token: 0x04000078 RID: 120
		private readonly int rpcRetryCount;

		// Token: 0x04000079 RID: 121
		private readonly TimeSpan rpcRetryDelay;

		// Token: 0x0400007A RID: 122
		private readonly TimeSpan maxRandomAdditionalRpcRetryDelay;

		// Token: 0x02000021 RID: 33
		internal class Schema : ConfigurationSchema<ServiceConfiguration.Schema>
		{
			// Token: 0x0600010D RID: 269 RVA: 0x000047B8 File Offset: 0x000029B8
			static Schema()
			{
				bool defaultValue = ServiceConfiguration.Schema.IsXtcEnabled.DefaultValue;
			}

			// Token: 0x0400007B RID: 123
			private const string ParametersRegistryPath = "SYSTEM\\CurrentControlSet\\Services\\MSExchangeRPC\\ParametersSystem";

			// Token: 0x0400007C RID: 124
			private const string ADUserDataCacheTimeoutRegistryValueName = "ADUserDataCacheTimeout";

			// Token: 0x0400007D RID: 125
			private const string ExecutionFlagsRegistryValueName = "ExecutionFlags";

			// Token: 0x0400007E RID: 126
			private const string IdleConnectionCheckPeriodRegistryValueName = "IdleConnectionCheckPeriod";

			// Token: 0x0400007F RID: 127
			private const string LogConnectionLatencyCheckPeriodRegistryValueName = "LogConnectionLatencyCheckPeriod";

			// Token: 0x04000080 RID: 128
			private const string MaintenanceJobTimerCheckPeriodRegistryValueName = "MaintenanceJobTimerCheckPeriod";

			// Token: 0x04000081 RID: 129
			private const string WaitBetweenTcpConnectToFindIfRpcServiceResponsiveRegistryValueName = "WaitBetweenTcpConnectToFindIfRpcServiceResponsive";

			// Token: 0x04000082 RID: 130
			private const string LogEveryConfigurationUpdateValueName = "LogEveryConfigurationUpdate";

			// Token: 0x04000083 RID: 131
			private const string ShareConnectionsRegistryValueName = "ShareConnections";

			// Token: 0x04000084 RID: 132
			private const string EnableExMonTestModeRegistryValueName = "EnableExMonTestMode";

			// Token: 0x04000085 RID: 133
			private const string TMPublishEnabledRegistryValueName = "TMPublishEnabled";

			// Token: 0x04000086 RID: 134
			private const string TMOAuthEnabledRegistryValueName = "TMOAuthEnabled";

			// Token: 0x04000087 RID: 135
			private const string TMPublishHttpDebugEnabledRegistryValueName = "TMPublishHttpDebugEnabled";

			// Token: 0x04000088 RID: 136
			private const string TMPublishRequestTimeoutRegistryValueName = "TMPublishRequestTimeout";

			// Token: 0x04000089 RID: 137
			private const string TMPublishCredentialRegistryValueName = "TMPublishCredential";

			// Token: 0x0400008A RID: 138
			private const string TMUseMockSharePointOperationRegistryValue = "TMUseMockSharePointOperation";

			// Token: 0x0400008B RID: 139
			private const string TMPublishConcurrentOperationLimitRegistryValueName = "TMPublishConcurrentOperationLimit";

			// Token: 0x0400008C RID: 140
			private const string TMRequiredMAPIClientVersionRegistryValueName = "TMRequiredMAPIClientVersion";

			// Token: 0x0400008D RID: 141
			private const string EnablePreferredSiteEnforcementRegistryValueName = "EnablePreferredSiteEnforcement";

			// Token: 0x0400008E RID: 142
			private const string FastTransferBackoffIntervalRegistryValueName = "FXGetBuffer BackOff Constant";

			// Token: 0x0400008F RID: 143
			private const string FastTransferBackoffRetryCountRegistryValueName = "FXGetBuffer Retry Count";

			// Token: 0x04000090 RID: 144
			private const string FastTransferMaxRequestsRegistryValueName = "Max FXGetBuffer Users";

			// Token: 0x04000091 RID: 145
			private const string RpcPollsMaxRegistryValueName = "Maximum Polling Frequency";

			// Token: 0x04000092 RID: 146
			private const string RpcRetryCountRegistryValueName = "RPC Retry Count";

			// Token: 0x04000093 RID: 147
			private const string RpcRetryDelayRegistryValueName = "RPC Retry Delay";

			// Token: 0x04000094 RID: 148
			private const string MaxRandomAdditionalRpcRetryDelayRegistryValueName = "Maximum Random Additional RPC Retry Delay";

			// Token: 0x04000095 RID: 149
			private const string EnableSmartConnectionTearDownValueName = "EnableSmartConnectionTearDown";

			// Token: 0x04000096 RID: 150
			private const string DisableAvailabilityService = "DisableAvailabilityServiceCalls";

			// Token: 0x04000097 RID: 151
			private const string EnableWebServicesEndpointRegistryValueName = "EnableWebServicesEndpoint";

			// Token: 0x04000098 RID: 152
			private const string MaximumRpcTasksRegistryValueName = "MaximumRpcTasks";

			// Token: 0x04000099 RID: 153
			private const string MaximumRpcThreadsRegistryValueName = "MaximumRpcThreads";

			// Token: 0x0400009A RID: 154
			private const string MinimumRpcThreadsRegistryValueName = "MinimumRpcThreads";

			// Token: 0x0400009B RID: 155
			private const string MaximumWebServiceTasksRegistryValueName = "MaximumWebServiceTasks";

			// Token: 0x0400009C RID: 156
			private const string MaximumWebServiceThreadsRegistryValueName = "MaximumWebServiceThreads";

			// Token: 0x0400009D RID: 157
			private const string MinimumWebServiceThreadsRegistryValueName = "MinimumWebServiceThreads";

			// Token: 0x0400009E RID: 158
			private const string MaximumRpcHttpConnectionRegistrationTasksRegistryValueName = "MaximumRpcHttpConnectionRegistrationTasks";

			// Token: 0x0400009F RID: 159
			private const string MaximumRpcHttpConnectionRegistrationThreadsRegistryValueName = "MaximumRpcHttpConnectionRegistrationThreads";

			// Token: 0x040000A0 RID: 160
			private const string MinimumRpcHttpConnectionRegistrationThreadsRegistryValueName = "MinimumRpcHttpConnectionRegistrationThreads";

			// Token: 0x040000A1 RID: 161
			private const string EnableBlockInsufficientClientVersionsRegistryValueName = "EnableBlockInsufficientClientVersions";

			// Token: 0x040000A2 RID: 162
			internal static ConfigurationSchema.Property<bool> IsXtcEnabled = ConfigurationSchema<ServiceConfiguration.Schema>.ConstantDataSource.Declare<bool>(delegate
			{
				VariantConfigurationSnapshot invariantNoFlightingSnapshot = VariantConfiguration.InvariantNoFlightingSnapshot;
				return invariantNoFlightingSnapshot.RpcClientAccess.XtcEndpoint.Enabled;
			});

			// Token: 0x040000A3 RID: 163
			private static readonly ConfigurationSchema.RegistryDataSource ParametersRegistryKey = new ConfigurationSchema.RegistryDataSource(ConfigurationSchema<ServiceConfiguration.Schema>.AllDataSources, "SYSTEM\\CurrentControlSet\\Services\\MSExchangeRPC\\ParametersSystem");

			// Token: 0x040000A4 RID: 164
			internal static readonly ConfigurationSchema.Property<TimeSpan> ADUserDataCacheTimeout = ConfigurationSchema.Property<TimeSpan>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "ADUserDataCacheTimeout", (int input) => input >= 0, (int input) => TimeSpan.FromSeconds((double)input), TimeSpan.FromMinutes(15.0));

			// Token: 0x040000A5 RID: 165
			internal static readonly ConfigurationSchema.Property<TimeSpan> WaitBetweenTcpConnectToFindIfRpcServiceResponsive = ConfigurationSchema.Property<TimeSpan>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "WaitBetweenTcpConnectToFindIfRpcServiceResponsive", (int input) => input >= 0, (int input) => TimeSpan.FromSeconds((double)input), TimeSpan.FromMinutes(1.0));

			// Token: 0x040000A6 RID: 166
			internal static readonly ConfigurationSchema.Property<bool> CanRunOnMailboxRole = ConfigurationSchema.Property<bool>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "ExecutionFlags", delegate(int input, out bool output)
			{
				output = ((input & 2) == 2);
				return EnumValidator.IsValidValue<ServiceConfiguration.Schema.ExecutionFlags>((ServiceConfiguration.Schema.ExecutionFlags)input);
			}, true);

			// Token: 0x040000A7 RID: 167
			internal static readonly ConfigurationSchema.Property<bool> LogEveryConfigurationUpdate = ConfigurationSchema.Property<bool>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "LogEveryConfigurationUpdate", (int value) => value == 0 || value == 1, (int value) => value == 1, false);

			// Token: 0x040000A8 RID: 168
			internal static readonly ConfigurationSchema.Property<TimeSpan> IdleConnectionCheckPeriod = ConfigurationSchema.Property<TimeSpan>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "IdleConnectionCheckPeriod", (int input) => input >= 0, (int input) => TimeSpan.FromSeconds((double)input), TimeSpan.FromMinutes(5.0));

			// Token: 0x040000A9 RID: 169
			internal static readonly ConfigurationSchema.Property<TimeSpan> LogConnectionLatencyCheckPeriod = ConfigurationSchema.Property<TimeSpan>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "LogConnectionLatencyCheckPeriod", (int input) => input >= 0, (int input) => TimeSpan.FromMinutes((double)input), TimeSpan.FromMinutes(15.0));

			// Token: 0x040000AA RID: 170
			internal static readonly ConfigurationSchema.Property<TimeSpan> MaintenanceJobTimerCheckPeriod = ConfigurationSchema.Property<TimeSpan>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "MaintenanceJobTimerCheckPeriod", (int input) => input >= 0, (int input) => TimeSpan.FromSeconds((double)input), TimeSpan.FromMinutes(1.0));

			// Token: 0x040000AB RID: 171
			internal static readonly ConfigurationSchema.Property<bool> ShareConnections = ConfigurationSchema.Property<bool>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "ShareConnections", (int value) => value == 0 || value == 1, (int value) => value == 1, true);

			// Token: 0x040000AC RID: 172
			internal static readonly ConfigurationSchema.Property<bool> EnableExMonTestMode = ConfigurationSchema.Property<bool>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "EnableExMonTestMode", (int value) => value == 0 || value == 1, (int value) => value == 1, false);

			// Token: 0x040000AD RID: 173
			internal static readonly ConfigurationSchema.Property<bool> TMPublishEnabled = ConfigurationSchema.Property<bool>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "TMPublishEnabled", (int value) => value == 0 || value == 1, (int value) => value == 1, true);

			// Token: 0x040000AE RID: 174
			internal static readonly ConfigurationSchema.Property<bool> TMOAuthEnabled = ConfigurationSchema.Property<bool>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "TMOAuthEnabled", (int value) => value == 0 || value == 1, (int value) => value == 1, true);

			// Token: 0x040000AF RID: 175
			internal static readonly ConfigurationSchema.Property<bool> TMPublishHttpDebugEnabled = ConfigurationSchema.Property<bool>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "TMPublishHttpDebugEnabled", (int value) => value == 0 || value == 1, (int value) => value == 1, false);

			// Token: 0x040000B0 RID: 176
			internal static readonly ConfigurationSchema.Property<TimeSpan> TMPublishRequestTimeout = ConfigurationSchema.Property<TimeSpan>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "TMPublishRequestTimeout", (int value) => value >= 0, (int input) => TimeSpan.FromSeconds((double)input), TimeSpan.FromSeconds(120.0));

			// Token: 0x040000B1 RID: 177
			internal static readonly ConfigurationSchema.Property<bool> TMUseMockSharePointOperation = ConfigurationSchema.Property<bool>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "TMUseMockSharePointOperation", (int value) => value == 0 || value == 1, (int value) => value == 1, false);

			// Token: 0x040000B2 RID: 178
			internal static readonly ConfigurationSchema.Property<int> TMPublishConcurrentOperationLimit = ConfigurationSchema.Property<int>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "TMPublishConcurrentOperationLimit", (int input) => input >= 0, (int input) => input, 20);

			// Token: 0x040000B3 RID: 179
			internal static readonly ConfigurationSchema.Property<ICredentials> TMPublishCredential = ConfigurationSchema.Property<ICredentials>.Declare<string, object, string>(ServiceConfiguration.Schema.ParametersRegistryKey, "TMPublishCredential", delegate(string input, out ICredentials output)
			{
				output = null;
				if (string.IsNullOrEmpty(input))
				{
					return false;
				}
				string[] array = input.Split(new char[]
				{
					';'
				});
				if (array == null || array.Length != 3)
				{
					return false;
				}
				output = new NetworkCredential(array[0], array[1], array[2]);
				return true;
			}, CredentialCache.DefaultCredentials);

			// Token: 0x040000B4 RID: 180
			internal static readonly ConfigurationSchema.Property<MapiVersion> TMRequiredMAPIClientVersion = ConfigurationSchema.Property<MapiVersion>.Declare<string, object, string>(ServiceConfiguration.Schema.ParametersRegistryKey, "TMRequiredMAPIClientVersion", delegate(string input, out MapiVersion output)
			{
				output = MapiVersion.Min;
				bool result;
				try
				{
					output = MapiVersion.Parse(input);
					result = true;
				}
				catch (FormatException)
				{
					result = false;
				}
				catch (ArgumentOutOfRangeException)
				{
					result = false;
				}
				return result;
			}, MapiVersion.Outlook15);

			// Token: 0x040000B5 RID: 181
			internal static readonly ConfigurationSchema.Property<bool> EnablePreferredSiteEnforcement = ConfigurationSchema.Property<bool>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "EnablePreferredSiteEnforcement", (int value) => value == 0 || value == 1, (int value) => value == 1, false);

			// Token: 0x040000B6 RID: 182
			internal static readonly ConfigurationSchema.Property<int> FastTransferMaxRequests = ConfigurationSchema.Property<int>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "Max FXGetBuffer Users", (int input) => input >= 0, (int input) => input, 15);

			// Token: 0x040000B7 RID: 183
			internal static readonly ConfigurationSchema.Property<int> FastTransferBackoffInterval = ConfigurationSchema.Property<int>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "FXGetBuffer BackOff Constant", (int input) => input >= 0 && input <= 10000, (int input) => input, 500);

			// Token: 0x040000B8 RID: 184
			internal static readonly ConfigurationSchema.Property<int> FastTransferBackoffRetryCount = ConfigurationSchema.Property<int>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "FXGetBuffer Retry Count", (int input) => input >= 0 && input <= 100, (int input) => input, 10);

			// Token: 0x040000B9 RID: 185
			internal static readonly ConfigurationSchema.Property<TimeSpan> RpcPollsMax = ConfigurationSchema.Property<TimeSpan>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "Maximum Polling Frequency", (int input) => input >= 5000 && input <= 120000, (int input) => TimeSpan.FromMilliseconds((double)input), ServiceConfiguration.RpcPollsMaxDefault);

			// Token: 0x040000BA RID: 186
			internal static readonly ConfigurationSchema.Property<int> RpcRetryCount = ConfigurationSchema.Property<int>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "RPC Retry Count", (int input) => input >= 0 && input <= 100, (int input) => input, 6);

			// Token: 0x040000BB RID: 187
			internal static readonly ConfigurationSchema.Property<TimeSpan> RpcRetryDelay = ConfigurationSchema.Property<TimeSpan>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "RPC Retry Delay", (int input) => input >= 100 && input <= 120000, (int input) => TimeSpan.FromMilliseconds((double)input), ServiceConfiguration.RpcRetryDelayDefault);

			// Token: 0x040000BC RID: 188
			internal static readonly ConfigurationSchema.Property<TimeSpan> MaxRandomAdditionalRpcRetryDelay = ConfigurationSchema.Property<TimeSpan>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "Maximum Random Additional RPC Retry Delay", (int input) => input >= 0 && input <= 120000, (int input) => TimeSpan.FromMilliseconds((double)input), ServiceConfiguration.MaxRandomAdditionalRpcRetryDelayDefault);

			// Token: 0x040000BD RID: 189
			internal static readonly ConfigurationSchema.Property<bool> EnableSmartConnectionTearDown = ConfigurationSchema.Property<bool>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "EnableSmartConnectionTearDown", (int value) => value == 0 || value == 1, (int value) => value == 1, true);

			// Token: 0x040000BE RID: 190
			internal static readonly ConfigurationSchema.Property<bool> EnableBlockInsufficientClientVersions = ConfigurationSchema.Property<bool>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "EnableBlockInsufficientClientVersions", (int value) => value == 0 || value == 1, (int value) => value == 1, false);

			// Token: 0x040000BF RID: 191
			internal static readonly ConfigurationSchema.Property<bool> EnableWebServicesEndpoint = ConfigurationSchema.Property<bool>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "EnableWebServicesEndpoint", (int value) => value == 0 || value == 1, (int value) => value == 1, false);

			// Token: 0x040000C0 RID: 192
			internal static readonly ConfigurationSchema.Property<bool> AvailabilityServiceCallsDisabled = ConfigurationSchema.Property<bool>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "DisableAvailabilityServiceCalls", (int value) => value == 0 || value == 1, (int value) => value == 1, false);

			// Token: 0x040000C1 RID: 193
			internal static readonly ConfigurationSchema.Property<int> MaximumRpcTasks = ConfigurationSchema.Property<int>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "MaximumRpcTasks", (int input) => input >= 100 && input <= 50000, (int input) => input, 5000);

			// Token: 0x040000C2 RID: 194
			internal static readonly ConfigurationSchema.Property<int> MaximumRpcThreads = ConfigurationSchema.Property<int>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "MaximumRpcThreads", (int input) => input >= 1 && input <= 500, (int input) => input, Math.Min(Environment.ProcessorCount * 3, 250));

			// Token: 0x040000C3 RID: 195
			internal static readonly ConfigurationSchema.Property<int> MinimumRpcThreads = ConfigurationSchema.Property<int>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "MinimumRpcThreads", (int input) => input >= 1 && input <= 500, (int input) => input, Environment.ProcessorCount);

			// Token: 0x040000C4 RID: 196
			internal static readonly ConfigurationSchema.Property<int> MaximumWebServiceTasks = ConfigurationSchema.Property<int>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "MaximumWebServiceTasks", (int input) => input >= 100 && input <= 50000, (int input) => input, 1000);

			// Token: 0x040000C5 RID: 197
			internal static readonly ConfigurationSchema.Property<int> MaximumWebServiceThreads = ConfigurationSchema.Property<int>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "MaximumWebServiceThreads", (int input) => input >= 1 && input <= 500, (int input) => input, Math.Min(Environment.ProcessorCount * 3, 100));

			// Token: 0x040000C6 RID: 198
			internal static readonly ConfigurationSchema.Property<int> MinimumWebServiceThreads = ConfigurationSchema.Property<int>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "MinimumWebServiceThreads", (int input) => input >= 1 && input <= 500, (int input) => input, 2);

			// Token: 0x040000C7 RID: 199
			internal static readonly ConfigurationSchema.Property<int> MaximumRpcHttpConnectionRegistrationTasks = ConfigurationSchema.Property<int>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "MaximumRpcHttpConnectionRegistrationTasks", (int input) => input >= 100 && input <= 50000, (int input) => input, 5000);

			// Token: 0x040000C8 RID: 200
			internal static readonly ConfigurationSchema.Property<int> MaximumRpcHttpConnectionRegistrationThreads = ConfigurationSchema.Property<int>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "MaximumRpcHttpConnectionRegistrationThreads", (int input) => input >= 1 && input <= 500, (int input) => input, Math.Min(Environment.ProcessorCount * 2, 32));

			// Token: 0x040000C9 RID: 201
			internal static readonly ConfigurationSchema.Property<int> MinimumRpcHttpConnectionRegistrationThreads = ConfigurationSchema.Property<int>.Declare<string, object, int>(ServiceConfiguration.Schema.ParametersRegistryKey, "MinimumRpcHttpConnectionRegistrationThreads", (int input) => input >= 1 && input <= 500, (int input) => input, Environment.ProcessorCount);

			// Token: 0x040000CA RID: 202
			private static readonly ConfigurationSchema.DirectoryDataSource<Server> LocalServerObject = new ConfigurationSchema.DirectoryDataSource<Server>(ConfigurationSchema<ServiceConfiguration.Schema>.AllDataSources, (ITopologyConfigurationSession session) => LocalServer.GetServer(), (ITopologyConfigurationSession session) => LocalServer.GetServer().Id);

			// Token: 0x040000CB RID: 203
			internal static readonly ConfigurationSchema.Property<bool> IsClientAccessRole = ServiceConfiguration.Schema.LocalServerObject.DeclareProperty<bool>(ServerSchema.IsClientAccessServer);

			// Token: 0x040000CC RID: 204
			internal static readonly ConfigurationSchema.Property<bool> IsMailboxRole = ConfigurationSchema.Property<bool>.Declare<ADPropertyDefinition, object>(ServiceConfiguration.Schema.LocalServerObject, ServerSchema.IsMailboxServer, true);

			// Token: 0x040000CD RID: 205
			internal static readonly ConfigurationSchema.Property<Fqdn> ThisServerFqdn = ConfigurationSchema.Property<Fqdn>.Declare<ADPropertyDefinition, object, string>(ServiceConfiguration.Schema.LocalServerObject, ServerSchema.Fqdn, new ConfigurationSchema.TryConvert<string, Fqdn>(Fqdn.TryParse), null);

			// Token: 0x040000CE RID: 206
			internal static readonly ConfigurationSchema.Property<LegacyDN> ThisServerLegacyDN = ConfigurationSchema.Property<LegacyDN>.Declare<ADPropertyDefinition, object, string>(ServiceConfiguration.Schema.LocalServerObject, ServerSchema.ExchangeLegacyDN, new ConfigurationSchema.TryConvert<string, LegacyDN>(LegacyDN.TryParse), null);

			// Token: 0x040000CF RID: 207
			private static readonly ConfigurationSchema.DirectoryDataSource<ExchangeRpcClientAccess> RpcClientAccessObject = new ConfigurationSchema.DirectoryDataSource<ExchangeRpcClientAccess>(ConfigurationSchema<ServiceConfiguration.Schema>.AllDataSources, (ITopologyConfigurationSession session) => session.Read<ExchangeRpcClientAccess>(ExchangeRpcClientAccess.FromServerId(LocalServer.GetServer().Id)), (ITopologyConfigurationSession session) => LocalServer.GetServer().Id);

			// Token: 0x040000D0 RID: 208
			internal static readonly ConfigurationSchema.Property<bool> IsRpcClientAccessObjectPresent = ConfigurationSchema.Property<bool>.Declare<ConfigurationSchema.DirectoryDataSource<ExchangeRpcClientAccess>>(ServiceConfiguration.Schema.RpcClientAccessObject, (ConfigurationSchema.DirectoryDataSource<ExchangeRpcClientAccess> dataSource, object context) => dataSource.CanQueryData(context), false);

			// Token: 0x040000D1 RID: 209
			internal static readonly ConfigurationSchema.Property<bool> IsEncryptionRequired = ServiceConfiguration.Schema.RpcClientAccessObject.DeclareProperty<bool>(ExchangeRpcClientAccessSchema.IsEncryptionRequired);

			// Token: 0x040000D2 RID: 210
			internal static readonly ConfigurationSchema.Property<int> MaximumConnections = ServiceConfiguration.Schema.RpcClientAccessObject.DeclareProperty<int>(ExchangeRpcClientAccessSchema.MaximumConnections);

			// Token: 0x040000D3 RID: 211
			internal static readonly ConfigurationSchema.Property<MapiVersionRanges> BlockedClientVersions = ConfigurationSchema.Property<MapiVersionRanges>.Declare<ADPropertyDefinition, object, string>(ServiceConfiguration.Schema.RpcClientAccessObject, ExchangeRpcClientAccessSchema.BlockedClientVersions, delegate(string blockedClientVersions, out MapiVersionRanges mapiVersionRanges)
			{
				bool result;
				try
				{
					mapiVersionRanges = new MapiVersionRanges(blockedClientVersions);
					result = true;
				}
				catch (FormatException innerException)
				{
					throw new ConfigurationSchema.LoadException(innerException);
				}
				catch (ArgumentException innerException2)
				{
					throw new ConfigurationSchema.LoadException(innerException2);
				}
				return result;
			}, new MapiVersionRanges(null));

			// Token: 0x040000D4 RID: 212
			private static readonly ConfigurationSchema.DirectoryDataSource<OutlookProvider> OutlookProviderObject = new ConfigurationSchema.DirectoryDataSource<OutlookProvider>(ConfigurationSchema<ServiceConfiguration.Schema>.AllDataSources, (ITopologyConfigurationSession session) => session.Read<OutlookProvider>(OutlookProvider.GetParentContainer(session).GetChildId("EXCH")), delegate(ITopologyConfigurationSession session)
			{
				ADObjectId parentContainer = OutlookProvider.GetParentContainer(session);
				if (parentContainer != null)
				{
					OutlookProvider outlookProvider = session.Read<OutlookProvider>(parentContainer.GetChildId("EXCH"));
					if (outlookProvider != null)
					{
						return outlookProvider.Id;
					}
				}
				return null;
			});

			// Token: 0x040000D5 RID: 213
			internal static readonly ConfigurationSchema.Property<ClientVersionCollection> RequiredVersionCollection = ConfigurationSchema.Property<ClientVersionCollection>.Declare<ADPropertyDefinition, object, ClientVersionCollection>(ServiceConfiguration.Schema.OutlookProviderObject, OutlookProviderSchema.RequiredClientVersions, delegate(object input, out ClientVersionCollection output)
			{
				output = (input as ClientVersionCollection);
				return true;
			}, new ConfigurationSchema.TryConvert<ClientVersionCollection, ClientVersionCollection>(ConfigurationSchema.Property.Identical<ClientVersionCollection>), null);

			// Token: 0x02000022 RID: 34
			[Flags]
			private enum ExecutionFlags
			{
				// Token: 0x0400012A RID: 298
				None = 0,
				// Token: 0x0400012B RID: 299
				Unused = 1,
				// Token: 0x0400012C RID: 300
				CanRunOnMailboxRole = 2
			}
		}
	}
}
