using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Audio;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200000F RID: 15
	internal class AppConfig
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00005A6C File Offset: 0x00003C6C
		private AppConfig()
		{
			this.appConfiguration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap
			{
				ExeConfigFilename = Path.Combine(Utils.GetExchangeDirectory(), "bin\\MSExchangeUM.config")
			}, ConfigurationUserLevel.None);
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00005B4B File Offset: 0x00003D4B
		public static AppConfig Instance
		{
			get
			{
				return AppConfig.instance.Value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00005B57 File Offset: 0x00003D57
		public AppConfig.ServiceConfig Service
		{
			get
			{
				return this.serviceConfig.Value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00005B64 File Offset: 0x00003D64
		public AppConfig.RecyclerConfig Recycler
		{
			get
			{
				return this.recyclerConfig.Value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00005B71 File Offset: 0x00003D71
		public AppConfig.GrammarDirConfig GrammarDirectory
		{
			get
			{
				return this.grammarDirConfig.Value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00005B7E File Offset: 0x00003D7E
		public AppConfig.WaveDirConfig WaveDirectory
		{
			get
			{
				return this.waveDirConfig.Value;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005B92 File Offset: 0x00003D92
		private static Lazy<AppConfig> AppConfigInitializer()
		{
			return new Lazy<AppConfig>(() => new AppConfig(), true);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005BB8 File Offset: 0x00003DB8
		internal static string GetSetting(string label)
		{
			string result = null;
			KeyValueConfigurationElement keyValueConfigurationElement = AppConfig.Instance.appConfiguration.AppSettings.Settings[label];
			if (keyValueConfigurationElement != null)
			{
				result = keyValueConfigurationElement.Value;
			}
			return result;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005BED File Offset: 0x00003DED
		internal static void ClearCache()
		{
			AppConfig.instance = AppConfig.AppConfigInitializer();
		}

		// Token: 0x0400002A RID: 42
		internal const string ConfigFileName = "MSExchangeUM.config";

		// Token: 0x0400002B RID: 43
		internal const int DatacenterDaysBeforeCertExpiryForAlert = 7;

		// Token: 0x0400002C RID: 44
		internal const int DatacenterSubsequentAlertIntervalAfterFirstAlertForCert = 1;

		// Token: 0x0400002D RID: 45
		private static Lazy<AppConfig> instance = AppConfig.AppConfigInitializer();

		// Token: 0x0400002E RID: 46
		private Configuration appConfiguration;

		// Token: 0x0400002F RID: 47
		private Lazy<AppConfig.ServiceConfig> serviceConfig = new Lazy<AppConfig.ServiceConfig>(() => AppConfig.ServiceConfig.Load(), true);

		// Token: 0x04000030 RID: 48
		private Lazy<AppConfig.RecyclerConfig> recyclerConfig = new Lazy<AppConfig.RecyclerConfig>(() => AppConfig.RecyclerConfig.Load(), true);

		// Token: 0x04000031 RID: 49
		private Lazy<AppConfig.GrammarDirConfig> grammarDirConfig = new Lazy<AppConfig.GrammarDirConfig>(() => AppConfig.GrammarDirConfig.Load(), true);

		// Token: 0x04000032 RID: 50
		private Lazy<AppConfig.WaveDirConfig> waveDirConfig = new Lazy<AppConfig.WaveDirConfig>(() => AppConfig.WaveDirConfig.Load(AppConfig.Instance.Service), true);

		// Token: 0x02000010 RID: 16
		internal class ServiceConfig
		{
			// Token: 0x06000110 RID: 272 RVA: 0x00005C05 File Offset: 0x00003E05
			private ServiceConfig()
			{
			}

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x06000111 RID: 273 RVA: 0x00005C0D File Offset: 0x00003E0D
			// (set) Token: 0x06000112 RID: 274 RVA: 0x00005C15 File Offset: 0x00003E15
			public bool EnableTemporaryTTS { get; private set; }

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x06000113 RID: 275 RVA: 0x00005C1E File Offset: 0x00003E1E
			// (set) Token: 0x06000114 RID: 276 RVA: 0x00005C26 File Offset: 0x00003E26
			public bool EnableTranscriptionWhitespace { get; private set; }

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x06000115 RID: 277 RVA: 0x00005C2F File Offset: 0x00003E2F
			// (set) Token: 0x06000116 RID: 278 RVA: 0x00005C37 File Offset: 0x00003E37
			public TimeSpan MessageTranscriptionTimeout { get; private set; }

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x06000117 RID: 279 RVA: 0x00005C40 File Offset: 0x00003E40
			// (set) Token: 0x06000118 RID: 280 RVA: 0x00005C48 File Offset: 0x00003E48
			public TimeSpan TranscriptionMaximumMessageLength { get; private set; }

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x06000119 RID: 281 RVA: 0x00005C51 File Offset: 0x00003E51
			// (set) Token: 0x0600011A RID: 282 RVA: 0x00005C59 File Offset: 0x00003E59
			public TimeSpan TranscriptionMaximumBacklogPerCore { get; private set; }

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x0600011B RID: 283 RVA: 0x00005C62 File Offset: 0x00003E62
			// (set) Token: 0x0600011C RID: 284 RVA: 0x00005C6A File Offset: 0x00003E6A
			public TimeSpan PickupDirectoryPollingPeriod { get; private set; }

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x0600011D RID: 285 RVA: 0x00005C73 File Offset: 0x00003E73
			// (set) Token: 0x0600011E RID: 286 RVA: 0x00005C7B File Offset: 0x00003E7B
			public int MaxMessagesPerCore { get; private set; }

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x0600011F RID: 287 RVA: 0x00005C84 File Offset: 0x00003E84
			// (set) Token: 0x06000120 RID: 288 RVA: 0x00005C8C File Offset: 0x00003E8C
			public TimeSpan CallAnswerMailboxDataTimeoutThreshold { get; private set; }

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x06000121 RID: 289 RVA: 0x00005C95 File Offset: 0x00003E95
			// (set) Token: 0x06000122 RID: 290 RVA: 0x00005C9D File Offset: 0x00003E9D
			public TimeSpan CallAnswerMailboxDataTimeout { get; private set; }

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x06000123 RID: 291 RVA: 0x00005CA6 File Offset: 0x00003EA6
			// (set) Token: 0x06000124 RID: 292 RVA: 0x00005CAE File Offset: 0x00003EAE
			public string FiniteStateMachinePath { get; private set; }

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x06000125 RID: 293 RVA: 0x00005CB7 File Offset: 0x00003EB7
			// (set) Token: 0x06000126 RID: 294 RVA: 0x00005CBF File Offset: 0x00003EBF
			public string PromptDirectory { get; private set; }

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x06000127 RID: 295 RVA: 0x00005CC8 File Offset: 0x00003EC8
			// (set) Token: 0x06000128 RID: 296 RVA: 0x00005CD0 File Offset: 0x00003ED0
			public bool EnableSpeechRecognitionOverride { get; private set; }

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x06000129 RID: 297 RVA: 0x00005CD9 File Offset: 0x00003ED9
			// (set) Token: 0x0600012A RID: 298 RVA: 0x00005CE1 File Offset: 0x00003EE1
			public double NormalizationLevelDB { get; private set; }

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x0600012B RID: 299 RVA: 0x00005CEA File Offset: 0x00003EEA
			// (set) Token: 0x0600012C RID: 300 RVA: 0x00005CF2 File Offset: 0x00003EF2
			public double NoiseFloorLevelDB { get; private set; }

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x0600012D RID: 301 RVA: 0x00005CFB File Offset: 0x00003EFB
			// (set) Token: 0x0600012E RID: 302 RVA: 0x00005D03 File Offset: 0x00003F03
			public bool EnableCallerIdDisplayNameResolution { get; private set; }

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x0600012F RID: 303 RVA: 0x00005D0C File Offset: 0x00003F0C
			// (set) Token: 0x06000130 RID: 304 RVA: 0x00005D14 File Offset: 0x00003F14
			public bool GenerateWatsonsForPipelineCleanup { get; private set; }

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x06000131 RID: 305 RVA: 0x00005D1D File Offset: 0x00003F1D
			// (set) Token: 0x06000132 RID: 306 RVA: 0x00005D25 File Offset: 0x00003F25
			public int LanguageAutoDetectionMinLength { get; private set; }

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x06000133 RID: 307 RVA: 0x00005D2E File Offset: 0x00003F2E
			// (set) Token: 0x06000134 RID: 308 RVA: 0x00005D36 File Offset: 0x00003F36
			public int LanguageAutoDetectionMaxLength { get; private set; }

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x06000135 RID: 309 RVA: 0x00005D3F File Offset: 0x00003F3F
			// (set) Token: 0x06000136 RID: 310 RVA: 0x00005D47 File Offset: 0x00003F47
			public G711Format G711EncodingFormat { get; private set; }

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x06000137 RID: 311 RVA: 0x00005D50 File Offset: 0x00003F50
			// (set) Token: 0x06000138 RID: 312 RVA: 0x00005D58 File Offset: 0x00003F58
			public int PipelineScaleFactorCPU { get; private set; }

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x06000139 RID: 313 RVA: 0x00005D61 File Offset: 0x00003F61
			// (set) Token: 0x0600013A RID: 314 RVA: 0x00005D69 File Offset: 0x00003F69
			public int PipelineScaleFactorNetworkBound { get; private set; }

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x0600013B RID: 315 RVA: 0x00005D72 File Offset: 0x00003F72
			// (set) Token: 0x0600013C RID: 316 RVA: 0x00005D7A File Offset: 0x00003F7A
			public int MaxRPCThreadsPerServer { get; private set; }

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x0600013D RID: 317 RVA: 0x00005D83 File Offset: 0x00003F83
			// (set) Token: 0x0600013E RID: 318 RVA: 0x00005D8B File Offset: 0x00003F8B
			public int MaxMessagesPerMailboxServer { get; private set; }

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x0600013F RID: 319 RVA: 0x00005D94 File Offset: 0x00003F94
			// (set) Token: 0x06000140 RID: 320 RVA: 0x00005D9C File Offset: 0x00003F9C
			public bool EnableRemoteGatewayAutomation { get; private set; }

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000141 RID: 321 RVA: 0x00005DA5 File Offset: 0x00003FA5
			// (set) Token: 0x06000142 RID: 322 RVA: 0x00005DAD File Offset: 0x00003FAD
			public string AutomationServiceAddress { get; private set; }

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x06000143 RID: 323 RVA: 0x00005DB6 File Offset: 0x00003FB6
			// (set) Token: 0x06000144 RID: 324 RVA: 0x00005DBE File Offset: 0x00003FBE
			public int AutomationServiceTcpPort { get; private set; }

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x06000145 RID: 325 RVA: 0x00005DC7 File Offset: 0x00003FC7
			// (set) Token: 0x06000146 RID: 326 RVA: 0x00005DCF File Offset: 0x00003FCF
			public PlatformType PlatformType { get; private set; }

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x06000147 RID: 327 RVA: 0x00005DD8 File Offset: 0x00003FD8
			// (set) Token: 0x06000148 RID: 328 RVA: 0x00005DE0 File Offset: 0x00003FE0
			public TimeSpan PipelineStallCheckThreshold { get; private set; }

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x06000149 RID: 329 RVA: 0x00005DE9 File Offset: 0x00003FE9
			// (set) Token: 0x0600014A RID: 330 RVA: 0x00005DF1 File Offset: 0x00003FF1
			public bool EnableWatsonOnPipelineStall { get; private set; }

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x0600014B RID: 331 RVA: 0x00005DFA File Offset: 0x00003FFA
			// (set) Token: 0x0600014C RID: 332 RVA: 0x00005E02 File Offset: 0x00004002
			public bool CDRLoggingEnabled { get; private set; }

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x0600014D RID: 333 RVA: 0x00005E0B File Offset: 0x0000400B
			// (set) Token: 0x0600014E RID: 334 RVA: 0x00005E13 File Offset: 0x00004013
			public int MaxCDRMessagesInPipeline { get; private set; }

			// Token: 0x17000060 RID: 96
			// (get) Token: 0x0600014F RID: 335 RVA: 0x00005E1C File Offset: 0x0000401C
			// (set) Token: 0x06000150 RID: 336 RVA: 0x00005E24 File Offset: 0x00004024
			public bool EnableG723 { get; private set; }

			// Token: 0x17000061 RID: 97
			// (get) Token: 0x06000151 RID: 337 RVA: 0x00005E2D File Offset: 0x0000402D
			// (set) Token: 0x06000152 RID: 338 RVA: 0x00005E35 File Offset: 0x00004035
			public bool EnableRTAudio { get; private set; }

			// Token: 0x17000062 RID: 98
			// (get) Token: 0x06000153 RID: 339 RVA: 0x00005E3E File Offset: 0x0000403E
			// (set) Token: 0x06000154 RID: 340 RVA: 0x00005E46 File Offset: 0x00004046
			public int TopNGrammarThreshold { get; private set; }

			// Token: 0x17000063 RID: 99
			// (get) Token: 0x06000155 RID: 341 RVA: 0x00005E4F File Offset: 0x0000404F
			// (set) Token: 0x06000156 RID: 342 RVA: 0x00005E57 File Offset: 0x00004057
			public int MinimumRtpPort { get; private set; }

			// Token: 0x17000064 RID: 100
			// (get) Token: 0x06000157 RID: 343 RVA: 0x00005E60 File Offset: 0x00004060
			// (set) Token: 0x06000158 RID: 344 RVA: 0x00005E68 File Offset: 0x00004068
			public int MaximumRtpPort { get; private set; }

			// Token: 0x17000065 RID: 101
			// (get) Token: 0x06000159 RID: 345 RVA: 0x00005E71 File Offset: 0x00004071
			// (set) Token: 0x0600015A RID: 346 RVA: 0x00005E79 File Offset: 0x00004079
			public bool CallRejectionLoggingEnabled { get; private set; }

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x0600015B RID: 347 RVA: 0x00005E82 File Offset: 0x00004082
			// (set) Token: 0x0600015C RID: 348 RVA: 0x00005E8A File Offset: 0x0000408A
			public bool StatisticsLoggingEnabled { get; private set; }

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x0600015D RID: 349 RVA: 0x00005E93 File Offset: 0x00004093
			// (set) Token: 0x0600015E RID: 350 RVA: 0x00005E9B File Offset: 0x0000409B
			public int StatisticsLoggingMaxDirectorySize { get; private set; }

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x0600015F RID: 351 RVA: 0x00005EA4 File Offset: 0x000040A4
			// (set) Token: 0x06000160 RID: 352 RVA: 0x00005EAC File Offset: 0x000040AC
			public int StatisticsLoggingMaxFileSize { get; private set; }

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x06000161 RID: 353 RVA: 0x00005EB5 File Offset: 0x000040B5
			// (set) Token: 0x06000162 RID: 354 RVA: 0x00005EBD File Offset: 0x000040BD
			public bool IntraSiteLoadBalancingEnabled { get; private set; }

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x06000163 RID: 355 RVA: 0x00005EC6 File Offset: 0x000040C6
			// (set) Token: 0x06000164 RID: 356 RVA: 0x00005ECE File Offset: 0x000040CE
			public int MaxMobileSpeechRecoRequestsPerCore { get; private set; }

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x06000165 RID: 357 RVA: 0x00005ED7 File Offset: 0x000040D7
			// (set) Token: 0x06000166 RID: 358 RVA: 0x00005EDF File Offset: 0x000040DF
			public int RecipientStartThrottlingThresholdPercent { get; private set; }

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x06000167 RID: 359 RVA: 0x00005EE8 File Offset: 0x000040E8
			// (set) Token: 0x06000168 RID: 360 RVA: 0x00005EF0 File Offset: 0x000040F0
			public int RecipientThrottlingPercent { get; private set; }

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x06000169 RID: 361 RVA: 0x00005EF9 File Offset: 0x000040F9
			// (set) Token: 0x0600016A RID: 362 RVA: 0x00005F01 File Offset: 0x00004101
			public bool SkipCertPHeaderCheckforActiveMonitoring { get; private set; }

			// Token: 0x0600016B RID: 363 RVA: 0x00005F0C File Offset: 0x0000410C
			public static AppConfig.ServiceConfig Load()
			{
				return new AppConfig.ServiceConfig
				{
					EnableTemporaryTTS = SafeConvert.ToBoolean(AppConfig.GetSetting("EnableTemporaryTTS"), true),
					EnableTranscriptionWhitespace = SafeConvert.ToBoolean(AppConfig.GetSetting("EnableTranscriptionWhitespace"), false),
					MessageTranscriptionTimeout = SafeConvert.ToTimeSpan(AppConfig.GetSetting("MessageTranscriptionTimeout"), TimeSpan.FromSeconds(0.0), TimeSpan.FromSeconds(600.0), TimeSpan.FromSeconds(180.0)),
					TranscriptionMaximumMessageLength = SafeConvert.ToTimeSpan(AppConfig.GetSetting("TranscriptionMaximumMessageLength"), TimeSpan.FromSeconds(0.0), TimeSpan.FromSeconds(600.0), TimeSpan.FromSeconds(75.0)),
					TranscriptionMaximumBacklogPerCore = SafeConvert.ToTimeSpan(AppConfig.GetSetting("TranscriptionMaximumBacklogPerCore"), TimeSpan.FromSeconds(0.0), TimeSpan.FromSeconds(3600.0), TimeSpan.FromSeconds(300.0)),
					PickupDirectoryPollingPeriod = SafeConvert.ToTimeSpan(AppConfig.GetSetting("PickupDirectoryPollingPeriod"), TimeSpan.FromSeconds(5.0), TimeSpan.FromSeconds(60.0), TimeSpan.FromSeconds(30.0)),
					MaxMessagesPerCore = SafeConvert.ToInt32(AppConfig.GetSetting("MaxMessagesPerCore"), 1, 1024, 100),
					CallAnswerMailboxDataTimeoutThreshold = SafeConvert.ToTimeSpan(AppConfig.GetSetting("CallAnswerMailboxDataTimeoutThreshold"), TimeSpan.FromMilliseconds(250.0), TimeSpan.FromMilliseconds(10000.0), TimeSpan.FromMilliseconds(4000.0)),
					CallAnswerMailboxDataTimeout = SafeConvert.ToTimeSpan(AppConfig.GetSetting("CallAnswerMailboxDataTimeout"), TimeSpan.FromMilliseconds(500.0), TimeSpan.FromMilliseconds(20000.0), TimeSpan.FromMilliseconds(6000.0)),
					FiniteStateMachinePath = SafeConvert.ToString(AppConfig.GetSetting("FiniteStateMachinePath"), "UnifiedMessaging\\root.fsm"),
					PromptDirectory = SafeConvert.ToString(AppConfig.GetSetting("PromptDirectory"), "UnifiedMessaging\\prompts"),
					EnableSpeechRecognitionOverride = SafeConvert.ToBoolean(AppConfig.GetSetting("EnableSpeechRecognitionOverride"), false),
					NormalizationLevelDB = SafeConvert.ToDouble(AppConfig.GetSetting("NormalizationLevelDB"), -25.0, 0.0, -18.0),
					NoiseFloorLevelDB = SafeConvert.ToDouble(AppConfig.GetSetting("NoiseFloorLevelDB"), -100.0, 0.0, -78.0),
					EnableCallerIdDisplayNameResolution = SafeConvert.ToBoolean(AppConfig.GetSetting("EnableCallerIdDisplayNameResolution"), true),
					GenerateWatsonsForPipelineCleanup = SafeConvert.ToBoolean(AppConfig.GetSetting("GenerateWatsonsForPipelineCleanup"), false),
					LanguageAutoDetectionMinLength = SafeConvert.ToInt32(AppConfig.GetSetting("LanguageAutoDetectionMinLength"), -1, 1048576, 64),
					LanguageAutoDetectionMaxLength = SafeConvert.ToInt32(AppConfig.GetSetting("LanguageAutoDetectionMaxLength"), 255, 1048576, 2048),
					G711EncodingFormat = SafeConvert.ToEnum<G711Format>(AppConfig.GetSetting("G711EncodingFormat"), G711Format.MULAW),
					PipelineScaleFactorCPU = SafeConvert.ToInt32(AppConfig.GetSetting("PipelineScaleFactorCPU"), 1, 64, 1),
					PipelineScaleFactorNetworkBound = SafeConvert.ToInt32(AppConfig.GetSetting("PipelineScaleFactorNetworkBound"), 1, 64, 4),
					MaxRPCThreadsPerServer = SafeConvert.ToInt32(AppConfig.GetSetting("MaxRPCThreadsPerServer"), 1, 64, 4),
					MaxMessagesPerMailboxServer = SafeConvert.ToInt32(AppConfig.GetSetting("MaxMessagesPerMailboxServer"), 1, 100, 100),
					EnableRemoteGatewayAutomation = SafeConvert.ToBoolean(AppConfig.GetSetting("EnableRemoteGatewayAutomation"), false),
					AutomationServiceAddress = SafeConvert.ToString(AppConfig.GetSetting("AutomationServiceAddress"), null),
					AutomationServiceTcpPort = SafeConvert.ToInt32(AppConfig.GetSetting("AutomationServiceTcpPort"), 0, int.MaxValue, 7001),
					PlatformType = SafeConvert.ToEnum<PlatformType>(AppConfig.GetSetting("Platform"), PlatformType.MSS),
					PipelineStallCheckThreshold = SafeConvert.ToTimeSpan(AppConfig.GetSetting("PipelineStallCheckThreshold"), TimeSpan.Zero, TimeSpan.FromDays(30.0), TimeSpan.FromMinutes(30.0)),
					EnableWatsonOnPipelineStall = SafeConvert.ToBoolean(AppConfig.GetSetting("EnableWatsonOnPipelineStall"), true),
					CDRLoggingEnabled = SafeConvert.ToBoolean(AppConfig.GetSetting("CDRLoggingEnabled"), true),
					MaxCDRMessagesInPipeline = SafeConvert.ToInt32(AppConfig.GetSetting("MaxCDRMessagesInPipeline"), 1, 1024, 100),
					EnableG723 = SafeConvert.ToBoolean(AppConfig.GetSetting("EnableG723"), true),
					EnableRTAudio = SafeConvert.ToBoolean(AppConfig.GetSetting("EnableRTAudio"), true),
					TopNGrammarThreshold = SafeConvert.ToInt32(AppConfig.GetSetting("TopNGrammarThreshold"), 1, 1073741823, 20),
					MinimumRtpPort = SafeConvert.ToInt32(AppConfig.GetSetting("MinimumRtpPort"), 1025, 65535, 1025),
					MaximumRtpPort = SafeConvert.ToInt32(AppConfig.GetSetting("MaximumRtpPort"), 1025, 65535, 65535),
					CallRejectionLoggingEnabled = SafeConvert.ToBoolean(AppConfig.GetSetting("CallRejectionLoggingEnabled"), true),
					StatisticsLoggingEnabled = SafeConvert.ToBoolean(AppConfig.GetSetting("StatisticsLoggingEnabled"), true),
					StatisticsLoggingMaxDirectorySize = SafeConvert.ToInt32(AppConfig.GetSetting("StatisticsLoggingMaxDirectorySize"), 1, 10, 4),
					StatisticsLoggingMaxFileSize = SafeConvert.ToInt32(AppConfig.GetSetting("StatisticsLoggingMaxFileSize"), 1, 100, 10),
					IntraSiteLoadBalancingEnabled = SafeConvert.ToBoolean(AppConfig.GetSetting("IntraSiteLoadBalancingEnabled"), true),
					MaxMobileSpeechRecoRequestsPerCore = SafeConvert.ToInt32(AppConfig.GetSetting("MaxMobileSpeechRecoRequestsPerCore"), 1, 25, 10),
					RecipientStartThrottlingThresholdPercent = SafeConvert.ToInt32(AppConfig.GetSetting("RecipientStartThrottlingThresholdPercent"), 0, 100, 50),
					RecipientThrottlingPercent = SafeConvert.ToInt32(AppConfig.GetSetting("RecipientThrottlingPercent"), 0, 100, 10),
					SkipCertPHeaderCheckforActiveMonitoring = SafeConvert.ToBoolean(AppConfig.GetSetting("SkipCertPHeaderCheckforActiveMonitoring"), true)
				};
			}
		}

		// Token: 0x02000011 RID: 17
		internal class GrammarDirConfig
		{
			// Token: 0x0600016C RID: 364 RVA: 0x000064C0 File Offset: 0x000046C0
			private GrammarDirConfig()
			{
			}

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x0600016D RID: 365 RVA: 0x000064C8 File Offset: 0x000046C8
			// (set) Token: 0x0600016E RID: 366 RVA: 0x000064D0 File Offset: 0x000046D0
			public string GrammarDir { get; private set; }

			// Token: 0x1700006F RID: 111
			// (get) Token: 0x0600016F RID: 367 RVA: 0x000064D9 File Offset: 0x000046D9
			// (set) Token: 0x06000170 RID: 368 RVA: 0x000064E1 File Offset: 0x000046E1
			public Dictionary<CultureInfo, string> GrammarCultureToSubDirectoryMap { get; private set; }

			// Token: 0x06000171 RID: 369 RVA: 0x000064EC File Offset: 0x000046EC
			public static AppConfig.GrammarDirConfig Load()
			{
				AppConfig.GrammarDirConfig grammarDirConfig = new AppConfig.GrammarDirConfig();
				grammarDirConfig.GrammarCultureToSubDirectoryMap = new Dictionary<CultureInfo, string>();
				string exchangeDirectory = Utils.GetExchangeDirectory();
				grammarDirConfig.GrammarDir = Path.Combine(exchangeDirectory, "UnifiedMessaging\\grammars");
				return grammarDirConfig;
			}
		}

		// Token: 0x02000012 RID: 18
		internal class WaveDirConfig
		{
			// Token: 0x06000172 RID: 370 RVA: 0x00006522 File Offset: 0x00004722
			private WaveDirConfig()
			{
			}

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x06000173 RID: 371 RVA: 0x0000652A File Offset: 0x0000472A
			// (set) Token: 0x06000174 RID: 372 RVA: 0x00006532 File Offset: 0x00004732
			public string WaveDir { get; private set; }

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x06000175 RID: 373 RVA: 0x0000653B File Offset: 0x0000473B
			// (set) Token: 0x06000176 RID: 374 RVA: 0x00006543 File Offset: 0x00004743
			public Dictionary<CultureInfo, string> PromptCultureToSubDirectoryMap { get; private set; }

			// Token: 0x06000177 RID: 375 RVA: 0x0000654C File Offset: 0x0000474C
			public static AppConfig.WaveDirConfig Load(AppConfig.ServiceConfig serviceConfig)
			{
				ValidateArgument.NotNull(serviceConfig, "serviceConfig");
				AppConfig.WaveDirConfig waveDirConfig = new AppConfig.WaveDirConfig();
				waveDirConfig.PromptCultureToSubDirectoryMap = new Dictionary<CultureInfo, string>();
				string exchangeDirectory = Utils.GetExchangeDirectory();
				waveDirConfig.WaveDir = Path.Combine(exchangeDirectory, serviceConfig.PromptDirectory);
				return waveDirConfig;
			}
		}

		// Token: 0x02000013 RID: 19
		internal class RecyclerConfig
		{
			// Token: 0x06000178 RID: 376 RVA: 0x0000658E File Offset: 0x0000478E
			private RecyclerConfig()
			{
			}

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x06000179 RID: 377 RVA: 0x00006596 File Offset: 0x00004796
			// (set) Token: 0x0600017A RID: 378 RVA: 0x0000659E File Offset: 0x0000479E
			public int WorkerSIPPort { get; private set; }

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x0600017B RID: 379 RVA: 0x000065A7 File Offset: 0x000047A7
			// (set) Token: 0x0600017C RID: 380 RVA: 0x000065AF File Offset: 0x000047AF
			public int MaxPrivateBytesPercent { get; private set; }

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x0600017D RID: 381 RVA: 0x000065B8 File Offset: 0x000047B8
			// (set) Token: 0x0600017E RID: 382 RVA: 0x000065C0 File Offset: 0x000047C0
			public int MaxTempDirSize { get; private set; }

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x0600017F RID: 383 RVA: 0x000065C9 File Offset: 0x000047C9
			// (set) Token: 0x06000180 RID: 384 RVA: 0x000065D1 File Offset: 0x000047D1
			public int RecycleInterval { get; private set; }

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x06000181 RID: 385 RVA: 0x000065DA File Offset: 0x000047DA
			// (set) Token: 0x06000182 RID: 386 RVA: 0x000065E2 File Offset: 0x000047E2
			public int HeartBeatInterval { get; private set; }

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x06000183 RID: 387 RVA: 0x000065EB File Offset: 0x000047EB
			// (set) Token: 0x06000184 RID: 388 RVA: 0x000065F3 File Offset: 0x000047F3
			public int MaxHeartBeatFailures { get; private set; }

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x06000185 RID: 389 RVA: 0x000065FC File Offset: 0x000047FC
			// (set) Token: 0x06000186 RID: 390 RVA: 0x00006604 File Offset: 0x00004804
			public int ResourceMonitorInterval { get; private set; }

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x06000187 RID: 391 RVA: 0x0000660D File Offset: 0x0000480D
			// (set) Token: 0x06000188 RID: 392 RVA: 0x00006615 File Offset: 0x00004815
			public int ThrashCountMaximum { get; private set; }

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x06000189 RID: 393 RVA: 0x0000661E File Offset: 0x0000481E
			// (set) Token: 0x0600018A RID: 394 RVA: 0x00006626 File Offset: 0x00004826
			public int StartupTime { get; private set; }

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x0600018B RID: 395 RVA: 0x0000662F File Offset: 0x0000482F
			// (set) Token: 0x0600018C RID: 396 RVA: 0x00006637 File Offset: 0x00004837
			public int MaxCallsBeforeRecycle { get; private set; }

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x0600018D RID: 397 RVA: 0x00006640 File Offset: 0x00004840
			// (set) Token: 0x0600018E RID: 398 RVA: 0x00006648 File Offset: 0x00004848
			public int HeartBeatResponseTime { get; private set; }

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x0600018F RID: 399 RVA: 0x00006651 File Offset: 0x00004851
			// (set) Token: 0x06000190 RID: 400 RVA: 0x00006659 File Offset: 0x00004859
			public int PingInterval { get; private set; }

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x06000191 RID: 401 RVA: 0x00006662 File Offset: 0x00004862
			// (set) Token: 0x06000192 RID: 402 RVA: 0x0000666A File Offset: 0x0000486A
			public int AlertIntervalAfterStartupModeChanged { get; private set; }

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x06000193 RID: 403 RVA: 0x00006673 File Offset: 0x00004873
			// (set) Token: 0x06000194 RID: 404 RVA: 0x0000667B File Offset: 0x0000487B
			public bool UseDataCenterActiveManagerRouting { get; private set; }

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x06000195 RID: 405 RVA: 0x00006684 File Offset: 0x00004884
			// (set) Token: 0x06000196 RID: 406 RVA: 0x0000668C File Offset: 0x0000488C
			public int DaysBeforeCertExpiryForAlert { get; private set; }

			// Token: 0x17000081 RID: 129
			// (get) Token: 0x06000197 RID: 407 RVA: 0x00006695 File Offset: 0x00004895
			// (set) Token: 0x06000198 RID: 408 RVA: 0x0000669D File Offset: 0x0000489D
			public int SubsequentAlertIntervalAfterFirstAlertForCert { get; private set; }

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x06000199 RID: 409 RVA: 0x000066A6 File Offset: 0x000048A6
			// (set) Token: 0x0600019A RID: 410 RVA: 0x000066AE File Offset: 0x000048AE
			public string CertFileName { get; private set; }

			// Token: 0x0600019B RID: 411 RVA: 0x000066B8 File Offset: 0x000048B8
			public static AppConfig.RecyclerConfig Load()
			{
				AppConfig.RecyclerConfig recyclerConfig = new AppConfig.RecyclerConfig();
				recyclerConfig.WorkerSIPPort = SafeConvert.ToInt32(AppConfig.GetSetting("WorkerSIPPort"), 1, int.MaxValue, 5065);
				recyclerConfig.MaxPrivateBytesPercent = SafeConvert.ToInt32(AppConfig.GetSetting("MaxPrivateBytesPercent"), 0, 100, 80);
				recyclerConfig.MaxTempDirSize = SafeConvert.ToInt32(AppConfig.GetSetting("MaxTempDirSize"), 0, int.MaxValue, 0);
				recyclerConfig.RecycleInterval = SafeConvert.ToInt32(AppConfig.GetSetting("RecycleInterval"), 0, int.MaxValue, 604800);
				recyclerConfig.HeartBeatInterval = SafeConvert.ToInt32(AppConfig.GetSetting("HeartBeatInterval"), 0, 600, 90);
				recyclerConfig.MaxHeartBeatFailures = SafeConvert.ToInt32(AppConfig.GetSetting("MaxHeartBeatFailures"), 0, 1, 1);
				recyclerConfig.ResourceMonitorInterval = SafeConvert.ToInt32(AppConfig.GetSetting("ResourceMonitorInterval"), 0, 3600, 600);
				recyclerConfig.ThrashCountMaximum = SafeConvert.ToInt32(AppConfig.GetSetting("ThrashCountMaximum"), 0, 100, 5);
				recyclerConfig.StartupTime = SafeConvert.ToInt32(AppConfig.GetSetting("StartupTime"), 120, 1200, 240);
				recyclerConfig.MaxCallsBeforeRecycle = SafeConvert.ToInt32(AppConfig.GetSetting("MaxCallsBeforeRecycle"), 0, 1048576, 50000);
				recyclerConfig.HeartBeatResponseTime = SafeConvert.ToInt32(AppConfig.GetSetting("HeartBeatResponseTime"), 0, 120, 60);
				recyclerConfig.PingInterval = SafeConvert.ToInt32(AppConfig.GetSetting("PingInterval"), 0, 3600, 120);
				recyclerConfig.AlertIntervalAfterStartupModeChanged = SafeConvert.ToInt32(AppConfig.GetSetting("AlertIntervalAfterStartupModeChanged"), 0, int.MaxValue, 600);
				if (CommonConstants.UseDataCenterCallRouting)
				{
					recyclerConfig.DaysBeforeCertExpiryForAlert = 7;
					recyclerConfig.SubsequentAlertIntervalAfterFirstAlertForCert = 1;
				}
				else
				{
					recyclerConfig.DaysBeforeCertExpiryForAlert = SafeConvert.ToInt32(AppConfig.GetSetting("DaysBeforeCertExpiryForAlert"), 1, 30, 30);
					recyclerConfig.SubsequentAlertIntervalAfterFirstAlertForCert = SafeConvert.ToInt32(AppConfig.GetSetting("SubsequentAlertIntervalAfterFirstAlertForCert"), 1, 30, 1);
				}
				recyclerConfig.CertFileName = SafeConvert.ToString(AppConfig.GetSetting("CertFileName"), "UnifiedMessaging\\UMServiceCertificate.cer");
				if (CommonConstants.UseDataCenterCallRouting)
				{
					recyclerConfig.UseDataCenterActiveManagerRouting = SafeConvert.ToBoolean(AppConfig.GetSetting("UseDataCenterActiveManagerRouting"), false);
				}
				return recyclerConfig;
			}
		}
	}
}
