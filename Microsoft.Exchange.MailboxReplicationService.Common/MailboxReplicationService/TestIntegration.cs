using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001A9 RID: 425
	internal class TestIntegration : TestIntegrationBase
	{
		// Token: 0x06000FD6 RID: 4054 RVA: 0x00025B24 File Offset: 0x00023D24
		public TestIntegration(bool autoRefresh = false) : base("SOFTWARE\\Microsoft\\Exchange_Test\\v15\\Migration", autoRefresh)
		{
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x00025B32 File Offset: 0x00023D32
		public static TestIntegration Instance
		{
			get
			{
				return TestIntegration.instance;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00025B39 File Offset: 0x00023D39
		public bool DisableRetriesOnTransientFailures
		{
			get
			{
				return base.GetFlagValue("DisableRetriesOnTransientFailures");
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x00025B46 File Offset: 0x00023D46
		public bool UseRemoteForSource
		{
			get
			{
				return base.GetFlagValue("UseRemoteForSource");
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00025B53 File Offset: 0x00023D53
		public bool UseRemoteForDestination
		{
			get
			{
				return base.GetFlagValue("UseRemoteForDestination");
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x00025B60 File Offset: 0x00023D60
		public bool UseTcpForRemoteMoves
		{
			get
			{
				return base.GetFlagValue("UseTcpForRemoteMoves");
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00025B6D File Offset: 0x00023D6D
		public bool UseHttpsForLocalMoves
		{
			get
			{
				return base.GetFlagValue("UseHttpsForLocalMoves");
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x00025B7A File Offset: 0x00023D7A
		public bool SkipWordBreaking
		{
			get
			{
				return base.GetFlagValue("SkipWordBreaking");
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x00025B87 File Offset: 0x00023D87
		public bool ForcePreFinalSyncDataProcessing
		{
			get
			{
				return base.GetFlagValue("ForcePreFinalSyncDataProcessing");
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x00025B94 File Offset: 0x00023D94
		public bool DisableDataGuaranteeCheckPeriod
		{
			get
			{
				return base.GetFlagValue("DisableDataGuaranteeCheckPeriod");
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x00025BA1 File Offset: 0x00023DA1
		public bool AllowRemoteArchivesInEnt
		{
			get
			{
				return base.GetFlagValue("AllowRemoteArchivesInEnt");
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00025BAE File Offset: 0x00023DAE
		public bool MicroDelayEnabled
		{
			get
			{
				return base.GetFlagValue("MicroDelayEnabled", true);
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x00025BBC File Offset: 0x00023DBC
		public bool BypassResourceReservation
		{
			get
			{
				return base.GetFlagValue("BypassResourceReservation");
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x00025BCC File Offset: 0x00023DCC
		public int MaxTombstoneRetries
		{
			get
			{
				return base.GetIntValue("MaxTombstoneRetries", 180, 0, 1000);
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x00025BF1 File Offset: 0x00023DF1
		public int MaxReportEntryCount
		{
			get
			{
				return base.GetIntValue("MaxReportEntryCount", 10000, -1, int.MaxValue);
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x00025C09 File Offset: 0x00023E09
		public int MaxOpenConnectionsPerPublicFolderMigration
		{
			get
			{
				return base.GetIntValue("MaxOpenConnectionsPerPublicFolderMigration", 500, 1, 1000);
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x00025C21 File Offset: 0x00023E21
		public int LargeDataLossThreshold
		{
			get
			{
				return base.GetIntValue("LargeDataLossThreshold", 50, 0, int.MaxValue);
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x00025C36 File Offset: 0x00023E36
		public bool UseLegacyCheckForHaCiHealthQuery
		{
			get
			{
				return base.GetFlagValue("UseLegacyCheckForHaCiHealthQuery");
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x00025C43 File Offset: 0x00023E43
		public bool DoNotAutomaticallyMarkRehome
		{
			get
			{
				return base.GetFlagValue("DoNotAutomaticallyMarkRehome");
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x00025C50 File Offset: 0x00023E50
		public int InjectMissingItems
		{
			get
			{
				return base.GetIntValue("InjectMissingItems", 0, 0, int.MaxValue);
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x00025C64 File Offset: 0x00023E64
		public bool ClassifyBadItemFaults
		{
			get
			{
				return base.GetFlagValue("ClassifyBadItemFaults");
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x00025C71 File Offset: 0x00023E71
		public string RoutingCookie
		{
			get
			{
				return base.GetStrValue("RoutingCookieName");
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x00025C7E File Offset: 0x00023E7E
		public bool AllowRemoteLegacyMovesWithE15
		{
			get
			{
				return base.GetFlagValue("AllowRemoteLegacyMovesWithE15");
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x00025C8B File Offset: 0x00023E8B
		public bool DisableRemoteHostNameBlacklisting
		{
			get
			{
				return base.GetFlagValue("DisableRemoteHostNameBlacklisting");
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x00025C98 File Offset: 0x00023E98
		public TimeSpan RemoteMailboxConnectionTimeout
		{
			get
			{
				return TimeSpan.FromSeconds((double)base.GetIntValue("RemoteMailboxConnectionTimeoutSecs", 22, 0, int.MaxValue));
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x00025CB3 File Offset: 0x00023EB3
		public TimeSpan RemoteMailboxCallTimeout
		{
			get
			{
				return TimeSpan.FromSeconds((double)base.GetIntValue("RemoteMailboxCallTimeoutSecs", 7200, 0, int.MaxValue));
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x00025CD1 File Offset: 0x00023ED1
		public TimeSpan LocalMailboxConnectionTimeout
		{
			get
			{
				return TimeSpan.FromSeconds((double)base.GetIntValue("LocalMailboxConnectionTimeoutSecs", 15, 0, int.MaxValue));
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x00025CEC File Offset: 0x00023EEC
		public TimeSpan LocalMailboxCallTimeout
		{
			get
			{
				return TimeSpan.FromSeconds((double)base.GetIntValue("LocalMailboxCallTimeoutSecs", 7200, 0, int.MaxValue));
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x00025D0A File Offset: 0x00023F0A
		public TimeSpan ProxyClientPingInterval
		{
			get
			{
				return TimeSpan.FromSeconds((double)base.GetIntValue("ProxyClientPingInterval", 180, 0, int.MaxValue));
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x00025D28 File Offset: 0x00023F28
		public bool SkipMrsProxyValidation
		{
			get
			{
				return base.GetFlagValue("SkipMrsProxyValidation");
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x00025D35 File Offset: 0x00023F35
		public int UpgradeSourceUserWhileOnboarding
		{
			get
			{
				return base.GetIntValue("UpgradeSourceUserWhileOnboarding", 0, -1, 1);
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x00025D45 File Offset: 0x00023F45
		public int FolderBatchSize
		{
			get
			{
				return base.GetIntValue("FolderBatchSize", 100, 1, int.MaxValue);
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x00025D5A File Offset: 0x00023F5A
		public bool DoE15HaCiHealthCheckForJobPickup
		{
			get
			{
				return base.GetFlagValue("DoE15HaCiHealthCheckForJobPickup");
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x00025D67 File Offset: 0x00023F67
		public bool InjectCrash
		{
			get
			{
				return base.GetFlagValue("InjectCrash");
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x00025D74 File Offset: 0x00023F74
		public bool InjectCorruptSyncState
		{
			get
			{
				return base.GetFlagValue("InjectCorruptSyncState");
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x00025D81 File Offset: 0x00023F81
		public bool InjectTransientExceptionAfterFolderDataCopy
		{
			get
			{
				return base.GetFlagValue("InjectTransientExceptionAfterFolderDataCopy");
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x00025D8E File Offset: 0x00023F8E
		public bool LogContentDetails
		{
			get
			{
				return base.GetFlagValue("LogContentDetails");
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x00025D9B File Offset: 0x00023F9B
		public bool DoNotUnlockTargetMailbox
		{
			get
			{
				return base.GetFlagValue("DoNotUnlockTargetMailbox");
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06000FFC RID: 4092 RVA: 0x00025DA8 File Offset: 0x00023FA8
		public bool UpdateMoveRequestFailsAfterStampingHomeMdb
		{
			get
			{
				return base.GetFlagValue("UpdateMoveRequestFailsAfterStampingHomeMdb");
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x00025DB5 File Offset: 0x00023FB5
		public bool AssumeWLMUnhealthyForReservations
		{
			get
			{
				return base.GetFlagValue("AssumeWLMUnhealthyForReservations");
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x00025DC2 File Offset: 0x00023FC2
		public bool DisableFolderCreationBlockFeature
		{
			get
			{
				return base.GetFlagValue("DisableFolderCreationBlockFeature");
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x00025DCF File Offset: 0x00023FCF
		public bool AbortConnectionDuringFX
		{
			get
			{
				return base.GetFlagValue("AbortConnectionDuringFX");
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x00025DDC File Offset: 0x00023FDC
		public Guid RemoteExchangeGuidOverride
		{
			get
			{
				return base.GetGuidValue("RemoteExchangeGuidOverride");
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x00025DE9 File Offset: 0x00023FE9
		public Guid RemoteArchiveGuidOverride
		{
			get
			{
				return base.GetGuidValue("RemoteArchiveGuidOverride");
			}
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00025DF8 File Offset: 0x00023FF8
		public Unlimited<EnhancedTimeSpan> GetCompletedRequestAgeLimit(TimeSpan defaultValue)
		{
			int intValue = base.GetIntValue("DefaultCompletedRequestAgeLimitOverride", (int)defaultValue.TotalHours, -1, int.MaxValue);
			if (intValue < 0)
			{
				return Unlimited<EnhancedTimeSpan>.UnlimitedValue;
			}
			return new Unlimited<EnhancedTimeSpan>(TimeSpan.FromHours((double)intValue));
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x00025E3A File Offset: 0x0002403A
		public int IntroduceFailureAfterCopyingHighWatermarkNTimes
		{
			get
			{
				return base.GetIntValue("IntroduceFailureAfterCopyingHighWatermarkNTimes", 0, 0, 100);
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x00025E4B File Offset: 0x0002404B
		public bool CheckInitialProvisioningForMoves
		{
			get
			{
				return base.GetFlagValue("CheckInitialProvisioningForMoves", ConfigBase<MRSConfigSchema>.GetConfig<bool>("CheckInitialProvisioningForMoves"));
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x00025E62 File Offset: 0x00024062
		public bool InjectUmmEndProcessingFailure
		{
			get
			{
				return base.GetFlagValue("InjectUmmEndProcessingFailure");
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x00025E6F File Offset: 0x0002406F
		public string EasAutodiscoverUrlOverride
		{
			get
			{
				return base.GetStrValue("EasAutodiscoverUrlOverride");
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x00025E7C File Offset: 0x0002407C
		public bool ProtocolTest
		{
			get
			{
				return base.GetFlagValue("ProtocolTest");
			}
		}

		// Token: 0x040008FD RID: 2301
		public const string RegKeyName = "SOFTWARE\\Microsoft\\Exchange_Test\\v15\\Migration";

		// Token: 0x040008FE RID: 2302
		public const string PostponeEnumerateFolderMessagesName = "PostponeEnumerateFolderMessages";

		// Token: 0x040008FF RID: 2303
		public const string PostponeSyncName = "PostponeSync";

		// Token: 0x04000900 RID: 2304
		public const string PostponeFinalSyncName = "PostponeFinalSync";

		// Token: 0x04000901 RID: 2305
		public const string PostponeResumeAccessToMailboxName = "PostponeResumeAccessToMailbox";

		// Token: 0x04000902 RID: 2306
		public const string PostponeWriteMessagesName = "PostponeWriteMessages";

		// Token: 0x04000903 RID: 2307
		public const string PostponeCleanupName = "PostponeCleanup";

		// Token: 0x04000904 RID: 2308
		public const string BreakpointBeforeUMM = "BreakpointBeforeUMM";

		// Token: 0x04000905 RID: 2309
		public const string BreakpointAfterUMM = "BreakpointAfterUMM";

		// Token: 0x04000906 RID: 2310
		public const string BreakpointRelinquish = "BreakpointRelinquish";

		// Token: 0x04000907 RID: 2311
		public const string BreakpointJobStalledDueToHACIHealth = "BreakpointStalledDueToHACIHealth";

		// Token: 0x04000908 RID: 2312
		public const string BreakpointBeforeConnect = "BreakpointBeforeConnect";

		// Token: 0x04000909 RID: 2313
		public const string DontPickupJobsName = "DontPickupJobs";

		// Token: 0x0400090A RID: 2314
		public const string DisableRetriesOnTransientFailuresName = "DisableRetriesOnTransientFailures";

		// Token: 0x0400090B RID: 2315
		public const string UseRemoteForSourceName = "UseRemoteForSource";

		// Token: 0x0400090C RID: 2316
		public const string UseRemoteForDestinationName = "UseRemoteForDestination";

		// Token: 0x0400090D RID: 2317
		public const string UseTcpForRemoteMovesName = "UseTcpForRemoteMoves";

		// Token: 0x0400090E RID: 2318
		public const string UseHttpsForLocalMovesName = "UseHttpsForLocalMoves";

		// Token: 0x0400090F RID: 2319
		public const string SkipWordBreakingName = "SkipWordBreaking";

		// Token: 0x04000910 RID: 2320
		public const string ForcePreFinalSyncDataProcessingName = "ForcePreFinalSyncDataProcessing";

		// Token: 0x04000911 RID: 2321
		public const string ReplayRpcConfigOverrideFrequency = "ReplayRpcConfigOverrideFrequency";

		// Token: 0x04000912 RID: 2322
		public const string DisableDataGuaranteeCheckPeriodName = "DisableDataGuaranteeCheckPeriod";

		// Token: 0x04000913 RID: 2323
		public const string DataGuaranteeTimeoutOverrideSecsName = "DataGuaranteeTimeoutOverrideSecs";

		// Token: 0x04000914 RID: 2324
		public const string AllowRemoteArchivesInEntName = "AllowRemoteArchivesInEnt";

		// Token: 0x04000915 RID: 2325
		public const string MaxReportEntryCountName = "MaxReportEntryCount";

		// Token: 0x04000916 RID: 2326
		public const string DefaultCompletedRequestAgeLimitOverrideName = "DefaultCompletedRequestAgeLimitOverride";

		// Token: 0x04000917 RID: 2327
		public const string MaxTombstoneRetriesName = "MaxTombstoneRetries";

		// Token: 0x04000918 RID: 2328
		public const string MaxOpenConnectionsPerPublicFolderMigrationName = "MaxOpenConnectionsPerPublicFolderMigration";

		// Token: 0x04000919 RID: 2329
		public const string LargeDataLossThresholdName = "LargeDataLossThreshold";

		// Token: 0x0400091A RID: 2330
		public const string UseLegacyCheckForHaCiHealthQueryName = "UseLegacyCheckForHaCiHealthQuery";

		// Token: 0x0400091B RID: 2331
		public const string DoNotAutomaticallyMarkRehomeName = "DoNotAutomaticallyMarkRehome";

		// Token: 0x0400091C RID: 2332
		public const string RoutingCookieName = "RoutingCookieName";

		// Token: 0x0400091D RID: 2333
		public const string AllowRemoteLegacyMovesWithE15Name = "AllowRemoteLegacyMovesWithE15";

		// Token: 0x0400091E RID: 2334
		public const string DisableRemoteHostNameBlacklistingName = "DisableRemoteHostNameBlacklisting";

		// Token: 0x0400091F RID: 2335
		public const string RemoteMailboxConnectionTimeoutName = "RemoteMailboxConnectionTimeoutSecs";

		// Token: 0x04000920 RID: 2336
		public const string RemoteMailboxCallTimeoutName = "RemoteMailboxCallTimeoutSecs";

		// Token: 0x04000921 RID: 2337
		public const string LocalMailboxConnectionTimeoutName = "LocalMailboxConnectionTimeoutSecs";

		// Token: 0x04000922 RID: 2338
		public const string LocalMailboxCallTimeoutName = "LocalMailboxCallTimeoutSecs";

		// Token: 0x04000923 RID: 2339
		public const string ProxyClientPingIntervalName = "ProxyClientPingInterval";

		// Token: 0x04000924 RID: 2340
		public const string InjectMissingItemsName = "InjectMissingItems";

		// Token: 0x04000925 RID: 2341
		public const string ClassifyBadItemFaultsName = "ClassifyBadItemFaults";

		// Token: 0x04000926 RID: 2342
		public const string MicroDelayEnabledName = "MicroDelayEnabled";

		// Token: 0x04000927 RID: 2343
		public const string BypassResourceReservationName = "BypassResourceReservation";

		// Token: 0x04000928 RID: 2344
		public const string SkipMrsProxyValidationName = "SkipMrsProxyValidation";

		// Token: 0x04000929 RID: 2345
		public const string FolderBatchSizeName = "FolderBatchSize";

		// Token: 0x0400092A RID: 2346
		public const string UpgradeSourceUserWhileOnboardingName = "UpgradeSourceUserWhileOnboarding";

		// Token: 0x0400092B RID: 2347
		public const string DoE15HaCiHealthCheckForJobPickupName = "DoE15HaCiHealthCheckForJobPickup";

		// Token: 0x0400092C RID: 2348
		public const string InjectCrashName = "InjectCrash";

		// Token: 0x0400092D RID: 2349
		public const string InjectCorruptSyncStateName = "InjectCorruptSyncState";

		// Token: 0x0400092E RID: 2350
		public const string InjectTransientExceptionAfterFolderDataCopyName = "InjectTransientExceptionAfterFolderDataCopy";

		// Token: 0x0400092F RID: 2351
		public const string FolderNameToInjectTransientException = "FolderToInjectTransientException";

		// Token: 0x04000930 RID: 2352
		public const string InjectNFaultsPostMoveUpdateSourceMailboxName = "InjectNFaultsPostMoveUpdateSourceMailbox";

		// Token: 0x04000931 RID: 2353
		public const string LogContentDetailsName = "LogContentDetails";

		// Token: 0x04000932 RID: 2354
		public const string DoNotUnlockTargetMailboxName = "DoNotUnlockTargetMailbox";

		// Token: 0x04000933 RID: 2355
		public const string UpdateMoveRequestFailsAfterStampingHomeMdbName = "UpdateMoveRequestFailsAfterStampingHomeMdb";

		// Token: 0x04000934 RID: 2356
		public const string AssumeWLMUnhealthyForReservationsName = "AssumeWLMUnhealthyForReservations";

		// Token: 0x04000935 RID: 2357
		public const string DisableFolderCreationBlockFeatureName = "DisableFolderCreationBlockFeature";

		// Token: 0x04000936 RID: 2358
		public const string AbortConnectionDuringFXName = "AbortConnectionDuringFX";

		// Token: 0x04000937 RID: 2359
		public const string IntroduceFailureAfterCopyingHighWatermarkNTimesName = "IntroduceFailureAfterCopyingHighWatermarkNTimes";

		// Token: 0x04000938 RID: 2360
		public const string PostponeCompleteName = "PostponeComplete";

		// Token: 0x04000939 RID: 2361
		public const string CheckInitialProvisioningForMovesName = "CheckInitialProvisioningForMoves";

		// Token: 0x0400093A RID: 2362
		public const string InjectUmmEndProcessingFailureName = "InjectUmmEndProcessingFailure";

		// Token: 0x0400093B RID: 2363
		public const string SimulatePushMoveName = "SimulatePushMove";

		// Token: 0x0400093C RID: 2364
		public const string RemoteExchangeGuidOverrideName = "RemoteExchangeGuidOverride";

		// Token: 0x0400093D RID: 2365
		public const string RemoteArchiveGuidOverrideName = "RemoteArchiveGuidOverride";

		// Token: 0x0400093E RID: 2366
		public const string InjectNFaultsTargetConnectivityVerificationName = "InjectNFaultsTargetConnectivityVerification";

		// Token: 0x0400093F RID: 2367
		public const string EasAutodiscoverUrlOverrideName = "EasAutodiscoverUrlOverride";

		// Token: 0x04000940 RID: 2368
		public const string ProtocolTestName = "ProtocolTest";

		// Token: 0x04000941 RID: 2369
		public const int DefaultMaxOpenConnectionsPerPublicFolderMigration = 500;

		// Token: 0x04000942 RID: 2370
		public const int DefaultLargeDataLossThreshold = 50;

		// Token: 0x04000943 RID: 2371
		public const int DefaultRemoteMailboxConnectionTimeout = 22;

		// Token: 0x04000944 RID: 2372
		public const int DefaultRemoteMailboxCallTimeout = 7200;

		// Token: 0x04000945 RID: 2373
		public const int DefaultLocalMailboxConnectionTimeout = 15;

		// Token: 0x04000946 RID: 2374
		public const int DefaultLocalMailboxCallTimeout = 7200;

		// Token: 0x04000947 RID: 2375
		public const int DefaultProxyClientPingInterval = 180;

		// Token: 0x04000948 RID: 2376
		public const int DefaultFolderBatchSize = 100;

		// Token: 0x04000949 RID: 2377
		public static readonly LocalizedString FaultInjectionExceptionMessage = new LocalizedString("Injecting Fault for Testing...");

		// Token: 0x0400094A RID: 2378
		private static readonly TestIntegration instance = new TestIntegration(true);
	}
}
