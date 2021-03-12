using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000239 RID: 569
	public static class FfoHygineDataProviderEventLogConstants
	{
		// Token: 0x04000BA7 RID: 2983
		public const string EventSource = "FfoDataService";

		// Token: 0x04000BA8 RID: 2984
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WebServiceEndpointConfigLoaded = new ExEventLog.EventTuple(1073742824U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BA9 RID: 2985
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WebServiceEndpointConfigLoadFailed = new ExEventLog.EventTuple(3221488619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BAA RID: 2986
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WebServiceInvalidServiceTag = new ExEventLog.EventTuple(3221488622U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BAB RID: 2987
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WebServiceRetry = new ExEventLog.EventTuple(2147484658U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BAC RID: 2988
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WebServiceMaxRetry = new ExEventLog.EventTuple(3221226483U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BAD RID: 2989
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WebServiceSlowResponse = new ExEventLog.EventTuple(2147484660U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BAE RID: 2990
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WebServiceFailure = new ExEventLog.EventTuple(3221226485U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BAF RID: 2991
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WebServiceOperationNotAllowed = new ExEventLog.EventTuple(3221488630U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BB0 RID: 2992
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WebServiceTypeNotAllowed = new ExEventLog.EventTuple(3221488631U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BB1 RID: 2993
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WebServiceFailedToExtractTenantId = new ExEventLog.EventTuple(2147746808U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BB2 RID: 2994
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WebServiceUnhandledExceptionDeterminingTenantRegion = new ExEventLog.EventTuple(3221488633U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BB3 RID: 2995
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PolicySyncWebserviceInitialized = new ExEventLog.EventTuple(1073742924U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BB4 RID: 2996
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PolicySyncTransientException = new ExEventLog.EventTuple(2147484749U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BB5 RID: 2997
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PolicySyncPermanentException = new ExEventLog.EventTuple(3221226574U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BB6 RID: 2998
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PolicySyncUnhandledException = new ExEventLog.EventTuple(3221226575U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BB7 RID: 2999
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PolicySyncTenantNotFound = new ExEventLog.EventTuple(2147484752U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BB8 RID: 3000
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PolicySyncUnauthorizedAccess = new ExEventLog.EventTuple(2147484753U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BB9 RID: 3001
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WebstoreDataProviderRetry = new ExEventLog.EventTuple(2147485148U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BBA RID: 3002
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WebstoreDataProviderMaxRetry = new ExEventLog.EventTuple(3221226973U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BBB RID: 3003
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WebstoreDataProviderCallFailure = new ExEventLog.EventTuple(3221226974U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BBC RID: 3004
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WebstoreDataProviderSlowResponse = new ExEventLog.EventTuple(2147485151U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BBD RID: 3005
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WebstoreDataProviderFindPageStoredProcMissing = new ExEventLog.EventTuple(2147485152U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BBE RID: 3006
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CachingSprocCriticalError = new ExEventLog.EventTuple(3221226977U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BBF RID: 3007
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WebstoreDataProviderConnectionError = new ExEventLog.EventTuple(3221226978U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BC0 RID: 3008
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WebstoreDataProviderMetadataRefreshError = new ExEventLog.EventTuple(3221226979U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BC1 RID: 3009
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WebstoreDataProviderCorruptDataIgnored = new ExEventLog.EventTuple(3221226980U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BC2 RID: 3010
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CacheDataProviderRetry = new ExEventLog.EventTuple(2147485249U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BC3 RID: 3011
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CacheDataProviderMaxRetry = new ExEventLog.EventTuple(2147485250U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BC4 RID: 3012
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CacheDataProviderCallFailure = new ExEventLog.EventTuple(3221227075U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BC5 RID: 3013
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CacheDataProviderSlowResponse = new ExEventLog.EventTuple(2147485252U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BC6 RID: 3014
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CompositeDataProviderDatabaseFailover = new ExEventLog.EventTuple(3221227077U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BC7 RID: 3015
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CompositeDataProviderCacheFailover = new ExEventLog.EventTuple(3221227078U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BC8 RID: 3016
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CacheSerializerPackObjectFallback = new ExEventLog.EventTuple(3221227079U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BC9 RID: 3017
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CacheSerializerPackObjectFailure = new ExEventLog.EventTuple(3221227080U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BCA RID: 3018
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CacheSerializerUnpackObjectFailure = new ExEventLog.EventTuple(3221227081U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BCB RID: 3019
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CacheDataProviderGetPrimingInfoFailure = new ExEventLog.EventTuple(3221227082U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BCC RID: 3020
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CompositeDataProviderCacheUnhealthy = new ExEventLog.EventTuple(3221227083U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BCD RID: 3021
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BloomFilterDataProviderLoadedNewFile = new ExEventLog.EventTuple(1073743524U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BCE RID: 3022
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_BloomFilterDataProviderNoNewFileFound = new ExEventLog.EventTuple(1073743525U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BCF RID: 3023
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BloomFilterDataProviderFailureLoadingFile = new ExEventLog.EventTuple(3221227174U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BD0 RID: 3024
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_BloomFilterDataProviderStaleTracerTokenDetected = new ExEventLog.EventTuple(2147485351U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BD1 RID: 3025
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InMemoryCachePrimingComplete = new ExEventLog.EventTuple(1073743625U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BD2 RID: 3026
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InMemoryCacheTransientErrorEncountered = new ExEventLog.EventTuple(2147485450U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BD3 RID: 3027
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InMemoryCacheFatalErrorEncounteredDuringPriming = new ExEventLog.EventTuple(3221227275U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BD4 RID: 3028
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InMemoryCacheFatalErrorEncounteredDuringRefresh = new ExEventLog.EventTuple(3221227276U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BD5 RID: 3029
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InMemoryConnectorCachePrimingIterationBeginning = new ExEventLog.EventTuple(1073743629U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BD6 RID: 3030
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InMemoryConnectorCachePrimingIterationComplete = new ExEventLog.EventTuple(1073743630U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BD7 RID: 3031
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DalWSServerUnknowError = new ExEventLog.EventTuple(3221489617U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BD8 RID: 3032
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PerfmonInstanceErrorFatal = new ExEventLog.EventTuple(3221490617U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BD9 RID: 3033
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DalWebServiceStarted = new ExEventLog.EventTuple(1073746824U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BDA RID: 3034
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DalWebServiceStopped = new ExEventLog.EventTuple(1073746825U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BDB RID: 3035
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StoreProcReturnedSuccessfully = new ExEventLog.EventTuple(1073748828U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BDC RID: 3036
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StoreProcInvoked = new ExEventLog.EventTuple(1073748827U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BDD RID: 3037
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DalExceptionThrown = new ExEventLog.EventTuple(2147490650U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BDE RID: 3038
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnsupportedFFOAPICalled = new ExEventLog.EventTuple(1073748825U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BDF RID: 3039
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AuditUserIdentityMissing = new ExEventLog.EventTuple(2147490653U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BE0 RID: 3040
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnsupportedQueryFilter = new ExEventLog.EventTuple(2147490654U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BE1 RID: 3041
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DomainCacheTrackingError = new ExEventLog.EventTuple(3221232479U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BE2 RID: 3042
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TransientExceptionWhenQueryGLS = new ExEventLog.EventTuple(3221495617U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BE3 RID: 3043
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnknownExceptionWhenQueryGLS = new ExEventLog.EventTuple(3221495618U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BE4 RID: 3044
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToRetrieveRegionTagWhenQueryGLS = new ExEventLog.EventTuple(3221495619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000BE5 RID: 3045
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PermanentExceptionWhenQueryGLS = new ExEventLog.EventTuple(3221495620U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BE6 RID: 3046
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorReadingPartitionMapFromDB = new ExEventLog.EventTuple(3221495621U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0200023A RID: 570
		private enum Category : short
		{
			// Token: 0x04000BE8 RID: 3048
			General = 1
		}

		// Token: 0x0200023B RID: 571
		internal enum Message : uint
		{
			// Token: 0x04000BEA RID: 3050
			WebServiceEndpointConfigLoaded = 1073742824U,
			// Token: 0x04000BEB RID: 3051
			WebServiceEndpointConfigLoadFailed = 3221488619U,
			// Token: 0x04000BEC RID: 3052
			WebServiceInvalidServiceTag = 3221488622U,
			// Token: 0x04000BED RID: 3053
			WebServiceRetry = 2147484658U,
			// Token: 0x04000BEE RID: 3054
			WebServiceMaxRetry = 3221226483U,
			// Token: 0x04000BEF RID: 3055
			WebServiceSlowResponse = 2147484660U,
			// Token: 0x04000BF0 RID: 3056
			WebServiceFailure = 3221226485U,
			// Token: 0x04000BF1 RID: 3057
			WebServiceOperationNotAllowed = 3221488630U,
			// Token: 0x04000BF2 RID: 3058
			WebServiceTypeNotAllowed,
			// Token: 0x04000BF3 RID: 3059
			WebServiceFailedToExtractTenantId = 2147746808U,
			// Token: 0x04000BF4 RID: 3060
			WebServiceUnhandledExceptionDeterminingTenantRegion = 3221488633U,
			// Token: 0x04000BF5 RID: 3061
			PolicySyncWebserviceInitialized = 1073742924U,
			// Token: 0x04000BF6 RID: 3062
			PolicySyncTransientException = 2147484749U,
			// Token: 0x04000BF7 RID: 3063
			PolicySyncPermanentException = 3221226574U,
			// Token: 0x04000BF8 RID: 3064
			PolicySyncUnhandledException,
			// Token: 0x04000BF9 RID: 3065
			PolicySyncTenantNotFound = 2147484752U,
			// Token: 0x04000BFA RID: 3066
			PolicySyncUnauthorizedAccess,
			// Token: 0x04000BFB RID: 3067
			WebstoreDataProviderRetry = 2147485148U,
			// Token: 0x04000BFC RID: 3068
			WebstoreDataProviderMaxRetry = 3221226973U,
			// Token: 0x04000BFD RID: 3069
			WebstoreDataProviderCallFailure,
			// Token: 0x04000BFE RID: 3070
			WebstoreDataProviderSlowResponse = 2147485151U,
			// Token: 0x04000BFF RID: 3071
			WebstoreDataProviderFindPageStoredProcMissing,
			// Token: 0x04000C00 RID: 3072
			CachingSprocCriticalError = 3221226977U,
			// Token: 0x04000C01 RID: 3073
			WebstoreDataProviderConnectionError,
			// Token: 0x04000C02 RID: 3074
			WebstoreDataProviderMetadataRefreshError,
			// Token: 0x04000C03 RID: 3075
			WebstoreDataProviderCorruptDataIgnored,
			// Token: 0x04000C04 RID: 3076
			CacheDataProviderRetry = 2147485249U,
			// Token: 0x04000C05 RID: 3077
			CacheDataProviderMaxRetry,
			// Token: 0x04000C06 RID: 3078
			CacheDataProviderCallFailure = 3221227075U,
			// Token: 0x04000C07 RID: 3079
			CacheDataProviderSlowResponse = 2147485252U,
			// Token: 0x04000C08 RID: 3080
			CompositeDataProviderDatabaseFailover = 3221227077U,
			// Token: 0x04000C09 RID: 3081
			CompositeDataProviderCacheFailover,
			// Token: 0x04000C0A RID: 3082
			CacheSerializerPackObjectFallback,
			// Token: 0x04000C0B RID: 3083
			CacheSerializerPackObjectFailure,
			// Token: 0x04000C0C RID: 3084
			CacheSerializerUnpackObjectFailure,
			// Token: 0x04000C0D RID: 3085
			CacheDataProviderGetPrimingInfoFailure,
			// Token: 0x04000C0E RID: 3086
			CompositeDataProviderCacheUnhealthy,
			// Token: 0x04000C0F RID: 3087
			BloomFilterDataProviderLoadedNewFile = 1073743524U,
			// Token: 0x04000C10 RID: 3088
			BloomFilterDataProviderNoNewFileFound,
			// Token: 0x04000C11 RID: 3089
			BloomFilterDataProviderFailureLoadingFile = 3221227174U,
			// Token: 0x04000C12 RID: 3090
			BloomFilterDataProviderStaleTracerTokenDetected = 2147485351U,
			// Token: 0x04000C13 RID: 3091
			InMemoryCachePrimingComplete = 1073743625U,
			// Token: 0x04000C14 RID: 3092
			InMemoryCacheTransientErrorEncountered = 2147485450U,
			// Token: 0x04000C15 RID: 3093
			InMemoryCacheFatalErrorEncounteredDuringPriming = 3221227275U,
			// Token: 0x04000C16 RID: 3094
			InMemoryCacheFatalErrorEncounteredDuringRefresh,
			// Token: 0x04000C17 RID: 3095
			InMemoryConnectorCachePrimingIterationBeginning = 1073743629U,
			// Token: 0x04000C18 RID: 3096
			InMemoryConnectorCachePrimingIterationComplete,
			// Token: 0x04000C19 RID: 3097
			DalWSServerUnknowError = 3221489617U,
			// Token: 0x04000C1A RID: 3098
			PerfmonInstanceErrorFatal = 3221490617U,
			// Token: 0x04000C1B RID: 3099
			DalWebServiceStarted = 1073746824U,
			// Token: 0x04000C1C RID: 3100
			DalWebServiceStopped,
			// Token: 0x04000C1D RID: 3101
			StoreProcReturnedSuccessfully = 1073748828U,
			// Token: 0x04000C1E RID: 3102
			StoreProcInvoked = 1073748827U,
			// Token: 0x04000C1F RID: 3103
			DalExceptionThrown = 2147490650U,
			// Token: 0x04000C20 RID: 3104
			UnsupportedFFOAPICalled = 1073748825U,
			// Token: 0x04000C21 RID: 3105
			AuditUserIdentityMissing = 2147490653U,
			// Token: 0x04000C22 RID: 3106
			UnsupportedQueryFilter,
			// Token: 0x04000C23 RID: 3107
			DomainCacheTrackingError = 3221232479U,
			// Token: 0x04000C24 RID: 3108
			TransientExceptionWhenQueryGLS = 3221495617U,
			// Token: 0x04000C25 RID: 3109
			UnknownExceptionWhenQueryGLS,
			// Token: 0x04000C26 RID: 3110
			FailedToRetrieveRegionTagWhenQueryGLS,
			// Token: 0x04000C27 RID: 3111
			PermanentExceptionWhenQueryGLS,
			// Token: 0x04000C28 RID: 3112
			ErrorReadingPartitionMapFromDB
		}
	}
}
