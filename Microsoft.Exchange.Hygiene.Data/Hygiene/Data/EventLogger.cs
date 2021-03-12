using System;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicySync;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000083 RID: 131
	internal static class EventLogger
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000FA77 File Offset: 0x0000DC77
		public static ExEventLog Logger
		{
			get
			{
				return EventLogger.logger;
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000FA80 File Offset: 0x0000DC80
		public static void LogRetry(string database, string correlationId, int retryCount, Exception exception)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_WebstoreDataProviderRetry, new object[]
			{
				database + ":" + correlationId,
				retryCount,
				exception
			});
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000FABC File Offset: 0x0000DCBC
		public static void LogMaxRetry(string database, string correlationId, int maximumRetryCount, Exception exception)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_WebstoreDataProviderMaxRetry, new object[]
			{
				database + ":" + correlationId,
				maximumRetryCount,
				exception
			});
			string notificationReason = string.Format("The Webstore Data Provider for query '{0}' reached the maximum retry limit: {1} time(s). Error: {2}", database + ":" + correlationId, maximumRetryCount, exception);
			EventNotificationItem.Publish(ExchangeComponent.Dal.Name, "FfoWebstoreDataProvider.MaxRetry." + database, null, notificationReason, ResultSeverityLevel.Error, false);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000FB34 File Offset: 0x0000DD34
		public static void LogSlowResponse(string database, string correlationId, TimeSpan slowResponseThreshold, TimeSpan elapsedTime)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_WebstoreDataProviderSlowResponse, new object[]
			{
				database + ":" + correlationId,
				slowResponseThreshold,
				elapsedTime
			});
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000FB74 File Offset: 0x0000DD74
		public static void LogFatalError(string database, string correlationId, Exception exception)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_WebstoreDataProviderCallFailure, new object[]
			{
				database + ":" + correlationId,
				exception
			});
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000FBA8 File Offset: 0x0000DDA8
		public static void LogNoConnectionAvailException(Exception exception)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_WebstoreDataProviderConnectionError, new object[]
			{
				exception
			});
			string notificationReason = string.Format("The Webstore Data provider is encountered a no connection exception and failing over to the database in other Datacenter: {0}.", exception);
			EventNotificationItem.Publish(ExchangeComponent.Dal.Name, "FfoWebstoreDataProvider.CrossDatacenterFailover", null, notificationReason, ResultSeverityLevel.Error, false);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000FBF0 File Offset: 0x0000DDF0
		public static void LogMetadataRefreshException(Exception exception)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_WebstoreDataProviderMetadataRefreshError, new object[]
			{
				exception
			});
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000FC14 File Offset: 0x0000DE14
		public static void LogCorruptDataIgnored(Type queryType, Exception exception)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_WebstoreDataProviderCorruptDataIgnored, new object[]
			{
				queryType.Name,
				exception
			});
			EventNotificationItem.Publish(ExchangeComponent.Dal.Name, "WebstoreDataProvider.CorruptDataIgnored", null, string.Format("The Webstore Data Provider ignored corrupt data processing a query for {0} that triggered the following exception: {1}", queryType.Name, exception), ResultSeverityLevel.Error, false);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000FC68 File Offset: 0x0000DE68
		public static void LogCacheProviderRetry(string cacheName, int retryCount, Exception exception)
		{
			EventLogger.LogPeriodicEvent(FfoHygineDataProviderEventLogConstants.Tuple_CacheDataProviderRetry, exception.Message, new object[]
			{
				retryCount,
				exception,
				cacheName
			});
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000FCA0 File Offset: 0x0000DEA0
		public static void LogCacheProviderMaxRetry(string cacheName, int maximumRetryCount, Exception exception, bool transientError = false)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_CacheDataProviderMaxRetry, new object[]
			{
				maximumRetryCount,
				exception,
				cacheName
			});
			if (transientError)
			{
				string notificationReason = string.Format("The Cache Data Provider reached the maximum retry limit for transient error: {0} time(s) for cache {2}. Error: {1}", maximumRetryCount, exception, cacheName);
				EventNotificationItem.Publish(ExchangeComponent.Dal.Name, "FfoCacheDataProvider.MaxRetry.Transient", null, notificationReason, ResultSeverityLevel.Warning, false);
				return;
			}
			string notificationReason2 = string.Format("The Cache Data Provider reached the maximum retry limit: {0} time(s) for cache {2}. Error: {1}", maximumRetryCount, exception, cacheName);
			EventNotificationItem.Publish(ExchangeComponent.Dal.Name, "FfoCacheDataProvider.MaxRetry", null, notificationReason2, ResultSeverityLevel.Warning, false);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000FD2C File Offset: 0x0000DF2C
		public static void LogCacheProviderSlowResponse(string cacheName, TimeSpan slowResponseThreshold, TimeSpan elapsedTime)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_CacheDataProviderSlowResponse, new object[]
			{
				slowResponseThreshold,
				elapsedTime,
				cacheName
			});
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000FD64 File Offset: 0x0000DF64
		public static void LogCacheProviderUnhandledException(string cacheName, Exception exception)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_CacheDataProviderCallFailure, new object[]
			{
				exception,
				cacheName
			});
			string notificationReason = string.Format("The Cache Data Provider fatally failed for cache {1} with error: {0}.", exception, cacheName);
			EventNotificationItem.Publish(ExchangeComponent.Dal.Name, "FfoCacheDataProvider.UnhandledException", null, notificationReason, ResultSeverityLevel.Error, false);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000FDB0 File Offset: 0x0000DFB0
		public static void LogCompositeDataProviderDatabaseFailover(string objectType, Exception exception)
		{
			EventLogger.LogPeriodicEvent(FfoHygineDataProviderEventLogConstants.Tuple_CompositeDataProviderDatabaseFailover, exception.Message, new object[]
			{
				objectType,
				exception
			});
			string notificationReason = string.Format("The Composite Data provider is encountered a permanent cache exception and failing over to the Database for type {0}: {1}.", objectType, exception);
			EventNotificationItem.Publish(ExchangeComponent.Dal.Name, "FfoCacheDataProvider.DatabaseFailover", null, notificationReason, ResultSeverityLevel.Error, false);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000FE04 File Offset: 0x0000E004
		public static void LogCompositeDataProviderCacheFailover(string objectType, Exception exception)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_CompositeDataProviderCacheFailover, new object[]
			{
				objectType,
				exception
			});
			string notificationReason = string.Format("The Composite Data provider is encountered a permanent DAL exception and failing over to the Cache for type {0}: {1}.", objectType, exception);
			EventNotificationItem.Publish(ExchangeComponent.Dal.Name, "FfoCacheDataProvider.CacheFailover", null, notificationReason, ResultSeverityLevel.Error, false);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000FE50 File Offset: 0x0000E050
		public static void LogCacheSerializerPackObjectFallback(string cacheName, Exception exception)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_CacheSerializerPackObjectFallback, new object[]
			{
				cacheName,
				exception
			});
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000FE78 File Offset: 0x0000E078
		public static void LogCacheSerializerPackObjectFailure(string cacheName, Exception exception)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_CacheSerializerPackObjectFailure, new object[]
			{
				cacheName,
				exception
			});
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000FEA0 File Offset: 0x0000E0A0
		public static void LogCacheSerializerUnpackObjectFailure(string cacheName, Exception exception)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_CacheSerializerUnpackObjectFailure, new object[]
			{
				cacheName,
				exception
			});
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000FEC8 File Offset: 0x0000E0C8
		public static void LogCompositeDataProviderCacheUnhealthy(string cacheName)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_CompositeDataProviderCacheUnhealthy, new object[]
			{
				cacheName
			});
			string notificationReason = string.Format("The Cache Data Provider determined priming info to be unhealthy for type {0}.", cacheName);
			EventNotificationItem.Publish(ExchangeComponent.Dal.Name, "FfoCacheDataProvider.GetPrimingInfoUnhealthy", null, notificationReason, ResultSeverityLevel.Error, false);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000FF10 File Offset: 0x0000E110
		public static void LogCacheDataProviderGetPrimingInfoFailure(string cacheName, Exception exception)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_CacheDataProviderGetPrimingInfoFailure, new object[]
			{
				cacheName,
				exception
			});
			string notificationReason = string.Format("The Cache Data Provider unable to determine priming info for type {0}. Error: {1}.", cacheName, exception);
			EventNotificationItem.Publish(ExchangeComponent.Dal.Name, "FfoCacheDataProvider.GetPrimingInfoFailure", null, notificationReason, ResultSeverityLevel.Error, false);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000FF5C File Offset: 0x0000E15C
		public static void LogDomainCacheTrackingError(params object[] messageArgs)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_DomainCacheTrackingError, messageArgs);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000FF6C File Offset: 0x0000E16C
		public static void LogBloomFilterDataProviderLoadedNewFile(Type dataType, string newFilePath)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_BloomFilterDataProviderLoadedNewFile, new object[]
			{
				dataType.Name,
				newFilePath
			});
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000FF98 File Offset: 0x0000E198
		public static void LogBloomFilterDataProviderNoNewFileFound(Type dataType, DateTime lastUpdateTime)
		{
			EventLogger.LogPeriodicEvent(FfoHygineDataProviderEventLogConstants.Tuple_BloomFilterDataProviderNoNewFileFound, dataType.Name, new object[]
			{
				dataType.Name,
				lastUpdateTime
			});
			if (DateTime.UtcNow - lastUpdateTime > EventLogger.newFileTimeSpan)
			{
				EventNotificationItem.Publish(ExchangeComponent.Dal.Name, "CompositeDataProvider.NoNewBloomFileForExtendedTime", null, string.Format("The CompositeDataProvider has not been able to swap to a new {0} bloom filter file. The last file was loaded at {1}", dataType.Name, lastUpdateTime), ResultSeverityLevel.Error, false);
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00010014 File Offset: 0x0000E214
		public static void LogBloomFilterDataProviderFailureLoadingFile(Type dataType, Exception ex)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_BloomFilterDataProviderFailureLoadingFile, new object[]
			{
				dataType.Name,
				ex
			});
			EventNotificationItem.Publish(ExchangeComponent.Dal.Name, "CompositeDataProvider.ErrorLoadingNewBloomFile", null, string.Format("The CompositeDataProvider encountered an error swapping to a new {0} bloom filter file: {1}", dataType.Name, ex), ResultSeverityLevel.Error, false);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00010068 File Offset: 0x0000E268
		public static void LogStaleTracerTokenDetected(Type dataType, string expectedTracer)
		{
			EventLogger.LogPeriodicEvent(FfoHygineDataProviderEventLogConstants.Tuple_BloomFilterDataProviderStaleTracerTokenDetected, dataType.Name + expectedTracer, new object[]
			{
				dataType.Name,
				expectedTracer
			});
			EventNotificationItem.Publish(ExchangeComponent.Dal.Name, "CompositeDataProvider.StaleTracerTokenDetected", null, string.Format("The CompositeDataProvider encountered a stale tracer token in the {0} bloom filter file. Expected to find key '{1}'.", dataType.Name, expectedTracer), ResultSeverityLevel.Error, false);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000100C8 File Offset: 0x0000E2C8
		public static void LogInMemoryCachePrimingComplete(string cache, int configCount, int tenantCount)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_InMemoryCachePrimingComplete, new object[]
			{
				cache,
				configCount,
				tenantCount
			});
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00010100 File Offset: 0x0000E300
		public static void LogInMemoryCacheTransientErrorEncountered(string cache, TransientDALException transientError)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_InMemoryCacheTransientErrorEncountered, new object[]
			{
				cache,
				transientError
			});
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00010128 File Offset: 0x0000E328
		public static void LogInMemoryCacheFatalErrorEncounteredDuringPriming(string cache, Exception fatalError)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_InMemoryCachePrimingComplete, new object[]
			{
				cache,
				fatalError
			});
			EventNotificationItem.Publish(ExchangeComponent.Dal.Name, "InMemoryCache.FatalErrorDuringPriming", null, string.Format("The in-memory cache for {0} data encountered a fatal error during priming of tenant data: {1}", cache, fatalError), ResultSeverityLevel.Error, false);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00010174 File Offset: 0x0000E374
		public static void LogInMemoryCacheFatalErrorEncounteredDuringRefresh(string cache, object itemKey, Exception fatalError)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_InMemoryCacheFatalErrorEncounteredDuringRefresh, new object[]
			{
				cache,
				itemKey,
				fatalError
			});
			EventNotificationItem.Publish(ExchangeComponent.Dal.Name, "InMemoryCache.FatalErrorDuringRefresh", null, string.Format("The in-memory cache for {0} data encountered a fatal error during refresh of the item with key {1}: {2}", cache, itemKey, fatalError), ResultSeverityLevel.Error, false);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000101C3 File Offset: 0x0000E3C3
		public static void LogConnectorPrimingIterationBeginning()
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_InMemoryConnectorCachePrimingIterationBeginning, new object[0]);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000101D5 File Offset: 0x0000E3D5
		public static void LogConnectorPrimingIterationComplete()
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_InMemoryConnectorCachePrimingIterationComplete, new object[0]);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000101E7 File Offset: 0x0000E3E7
		public static void LogPolicySyncWebserviceInitialized()
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_PolicySyncWebserviceInitialized, new object[0]);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000101FC File Offset: 0x0000E3FC
		public static void LogPolicySyncWebserviceTransientException(string operation, Workload workload, string objectType, SyncCallerContext callerContext, Exception transientException)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_PolicySyncTransientException, new object[]
			{
				operation,
				workload,
				objectType,
				callerContext,
				transientException
			});
			EventNotificationItem.Publish(ExchangeComponent.Dal.Name, "PolicySyncWS.TransientException", null, string.Format("The PolicySync Webservice encountered a transient error (operation: {0}, workload: {1}, type: {2}, context: {3}): {4}", new object[]
			{
				operation,
				workload,
				objectType,
				callerContext,
				transientException
			}), ResultSeverityLevel.Error, false);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00010278 File Offset: 0x0000E478
		public static void LogPolicySyncWebservicePermanentException(string operation, Workload workload, string objectType, SyncCallerContext callerContext, Exception permanentException)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_PolicySyncPermanentException, new object[]
			{
				operation,
				workload,
				objectType,
				callerContext,
				permanentException
			});
			EventNotificationItem.Publish(ExchangeComponent.Dal.Name, "PolicySyncWS.PermanentException", null, string.Format("The PolicySync Webservice encountered a permanent error (operation: {0}, workload: {1}, type: {2}, context: {3}): {4}", new object[]
			{
				operation,
				workload,
				objectType,
				callerContext,
				permanentException
			}), ResultSeverityLevel.Error, false);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000102F4 File Offset: 0x0000E4F4
		public static void LogPolicySyncWebserviceUnhandledException(string operation, Workload workload, string objectType, SyncCallerContext callerContext, Exception unhandledException)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_PolicySyncUnhandledException, new object[]
			{
				operation,
				workload,
				objectType,
				callerContext,
				unhandledException
			});
			EventNotificationItem.Publish(ExchangeComponent.Dal.Name, "PolicySyncWS.UnhandledException", null, string.Format("The PolicySync Webservice encountered an unhandled error (operation: {0}, workload: {1}, type: {2}, context: {3}): {4}", new object[]
			{
				operation,
				workload,
				objectType,
				callerContext,
				unhandledException
			}), ResultSeverityLevel.Error, false);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00010370 File Offset: 0x0000E570
		public static void LogPolicySyncWebserviceTenantNotFound(string operation, Workload workload, string objectType, SyncCallerContext callerContext, Exception exception)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_PolicySyncTenantNotFound, new object[]
			{
				operation,
				workload,
				objectType,
				callerContext,
				exception
			});
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x000103AC File Offset: 0x0000E5AC
		public static void LogPolicySyncWebserviceGlsError(string operation, Workload workload, string objectType, SyncCallerContext callerContext, Exception exception)
		{
			EventNotificationItem.Publish(ExchangeComponent.Dal.Name, "PolicySyncWS.GlsError", null, string.Format("The PolicySync Webservice encountered a Gls error (operation: {0}, workload: {1}, type: {2}, context: {3}): {4}", new object[]
			{
				operation,
				workload,
				objectType,
				callerContext,
				exception
			}), ResultSeverityLevel.Error, false);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000103FC File Offset: 0x0000E5FC
		public static void LogPolicySyncWebserviceUnauthorizedAccess(string operation, Workload workload, string objectType, SyncCallerContext callerContext, Exception exception)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_PolicySyncUnauthorizedAccess, new object[]
			{
				operation,
				workload,
				objectType,
				callerContext,
				exception
			});
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00010438 File Offset: 0x0000E638
		public static void LogPartitionMapDatabaseReadError(Exception ex, string localFileName)
		{
			EventLogger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_ErrorReadingPartitionMapFromDB, new object[]
			{
				ex.ToString(),
				localFileName
			});
			string notificationReason = string.Format("Error {0} reading Partition Map from DB. Reading Partition Map from Local File {1}", ex, localFileName);
			EventNotificationItem.Publish(ExchangeComponent.Dal.Name, "FfoWebstoreDataProvider.PartitionMapDBRead", null, notificationReason, ResultSeverityLevel.Error, false);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001048C File Offset: 0x0000E68C
		public static string LogPeriodicalKey(int minute)
		{
			DateTime utcNow = DateTime.UtcNow;
			return string.Format("{0}-{1} {2}:{3}", new object[]
			{
				utcNow.Day,
				utcNow.Month,
				utcNow.Hour,
				utcNow.Minute / minute
			});
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000104EF File Offset: 0x0000E6EF
		private static void LogEvent(ExEventLog.EventTuple tuple, params object[] messageArgs)
		{
			EventLogger.LogPeriodicEvent(tuple, null, messageArgs);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00010510 File Offset: 0x0000E710
		private static void LogPeriodicEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			EventLogger.logger.LogEvent(tuple, periodicKey, messageArgs.Select(delegate(object arg)
			{
				if (!(arg is Exception))
				{
					return arg;
				}
				return EventLogger.TrimException((Exception)arg);
			}).ToArray<object>());
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00010547 File Offset: 0x0000E747
		private static object TrimException(Exception exception)
		{
			return new string(exception.ToString().Take(31766).ToArray<char>());
		}

		// Token: 0x0400032B RID: 811
		public const string LogAlways = null;

		// Token: 0x0400032C RID: 812
		public const int LogEveryXMinutes = 1;

		// Token: 0x0400032D RID: 813
		private static TimeSpan newFileTimeSpan = TimeSpan.FromHours(18.0);

		// Token: 0x0400032E RID: 814
		private static Guid componentGuid = Guid.Parse("{4B65DA35-2EAC-4452-B7B7-375D986BCA91}");

		// Token: 0x0400032F RID: 815
		private static ExEventLog logger = new ExEventLog(EventLogger.componentGuid, "FfoDataService");
	}
}
