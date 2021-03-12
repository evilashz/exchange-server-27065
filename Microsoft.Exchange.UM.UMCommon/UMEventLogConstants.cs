using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM
{
	// Token: 0x02000230 RID: 560
	public static class UMEventLogConstants
	{
		// Token: 0x040008D9 RID: 2265
		public const string EventSource = "MSExchange Unified Messaging";

		// Token: 0x040008DA RID: 2266
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMWorkerProcessStartSuccess = new ExEventLog.EventTuple(263144U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008DB RID: 2267
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMWorkerProcessStartFailure = new ExEventLog.EventTuple(3221488617U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008DC RID: 2268
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMWorkerProcessStopSuccess = new ExEventLog.EventTuple(263146U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008DD RID: 2269
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMWorkerProcessStopFailure = new ExEventLog.EventTuple(3221488619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008DE RID: 2270
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InboundCallParams = new ExEventLog.EventTuple(263148U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008DF RID: 2271
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OutboundCallParams = new ExEventLog.EventTuple(263149U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008E0 RID: 2272
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallReceived = new ExEventLog.EventTuple(263150U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008E1 RID: 2273
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallEndedByUser = new ExEventLog.EventTuple(263151U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008E2 RID: 2274
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FsmConfigurationError = new ExEventLog.EventTuple(3221488624U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008E3 RID: 2275
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FsmActivityStart = new ExEventLog.EventTuple(263153U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008E4 RID: 2276
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FsmConfigurationInitialized = new ExEventLog.EventTuple(263154U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008E5 RID: 2277
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PromptsPlayed = new ExEventLog.EventTuple(263155U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008E6 RID: 2278
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxLocked = new ExEventLog.EventTuple(263156U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008E7 RID: 2279
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogonDisconnect = new ExEventLog.EventTuple(263157U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008E8 RID: 2280
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ExceptionDuringCall = new ExEventLog.EventTuple(3221488630U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008E9 RID: 2281
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMUserEnabled = new ExEventLog.EventTuple(263159U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008EA RID: 2282
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMUserDisabled = new ExEventLog.EventTuple(263160U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008EB RID: 2283
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMUserUnlocked = new ExEventLog.EventTuple(263161U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008EC RID: 2284
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMUserPasswordChanged = new ExEventLog.EventTuple(263162U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008ED RID: 2285
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidChecksum = new ExEventLog.EventTuple(3221488635U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008EE RID: 2286
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DirectorySearchResults = new ExEventLog.EventTuple(263164U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008EF RID: 2287
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallRejected = new ExEventLog.EventTuple(3221488637U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008F0 RID: 2288
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UMUserNotConfiguredForFax = new ExEventLog.EventTuple(2147746825U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040008F1 RID: 2289
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SuccessfulLogon = new ExEventLog.EventTuple(263180U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008F2 RID: 2290
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMServiceStartSuccess = new ExEventLog.EventTuple(263181U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008F3 RID: 2291
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMServiceStartFailure = new ExEventLog.EventTuple(3221488654U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008F4 RID: 2292
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMServiceStopSuccess = new ExEventLog.EventTuple(263183U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008F5 RID: 2293
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMWorkerProcessExited = new ExEventLog.EventTuple(1074005014U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008F6 RID: 2294
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMWorkerProcessRequestedRecycle = new ExEventLog.EventTuple(1074005015U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008F7 RID: 2295
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMWorkerProcessNoProcessData = new ExEventLog.EventTuple(1074005016U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008F8 RID: 2296
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecycledMaxCallsExceeded = new ExEventLog.EventTuple(1074005017U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008F9 RID: 2297
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WatsoningDueToRecycling = new ExEventLog.EventTuple(1074005018U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008FA RID: 2298
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecycledMaxMemoryPressureExceeded = new ExEventLog.EventTuple(1074005019U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008FB RID: 2299
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecycledMaxUptimeExceeded = new ExEventLog.EventTuple(1074005020U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008FC RID: 2300
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecycledMaxHeartBeatsMissedExceeded = new ExEventLog.EventTuple(1074005021U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008FD RID: 2301
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_KilledMaxStartuptimeExceeded = new ExEventLog.EventTuple(1074005022U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008FE RID: 2302
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_KilledMaxRetireTimeExceeded = new ExEventLog.EventTuple(1074005023U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040008FF RID: 2303
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMServiceStateChange = new ExEventLog.EventTuple(1074005024U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000900 RID: 2304
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMWorkerProcessUnhandledException = new ExEventLog.EventTuple(3221488673U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000901 RID: 2305
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_KilledCouldntEstablishControlChannel = new ExEventLog.EventTuple(1074005027U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000902 RID: 2306
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NewDialPlanCreated = new ExEventLog.EventTuple(263204U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000903 RID: 2307
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NewIPGatewayCreated = new ExEventLog.EventTuple(263205U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000904 RID: 2308
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NewHuntGroupCreated = new ExEventLog.EventTuple(263206U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000905 RID: 2309
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DialPlanRemoved = new ExEventLog.EventTuple(263207U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000906 RID: 2310
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IPGatewayRemoved = new ExEventLog.EventTuple(263208U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000907 RID: 2311
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HuntGroupRemoved = new ExEventLog.EventTuple(263209U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000908 RID: 2312
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMServerEnabled = new ExEventLog.EventTuple(263210U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000909 RID: 2313
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IPGatewayEnabled = new ExEventLog.EventTuple(263211U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400090A RID: 2314
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMServerDisabled = new ExEventLog.EventTuple(263212U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400090B RID: 2315
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IPGatewayDisabled = new ExEventLog.EventTuple(263213U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400090C RID: 2316
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutoAttendantCreated = new ExEventLog.EventTuple(263214U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400090D RID: 2317
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutoAttendantEnabled = new ExEventLog.EventTuple(263215U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400090E RID: 2318
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutoAttendantDisabled = new ExEventLog.EventTuple(263216U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400090F RID: 2319
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallTransfer = new ExEventLog.EventTuple(263217U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000910 RID: 2320
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_KillWorkItemAndMoveToBadVMFolder = new ExEventLog.EventTuple(3221488690U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000911 RID: 2321
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PlayOnPhoneRequest = new ExEventLog.EventTuple(263219U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000912 RID: 2322
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OutDialingRulesFailure = new ExEventLog.EventTuple(263220U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000913 RID: 2323
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DisconnectRequest = new ExEventLog.EventTuple(263221U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000914 RID: 2324
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ExExceptionDuringCall = new ExEventLog.EventTuple(2147746870U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000915 RID: 2325
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PlatformException = new ExEventLog.EventTuple(2147746871U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000916 RID: 2326
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_QuotaExceededFailedSubmit = new ExEventLog.EventTuple(2147746872U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000917 RID: 2327
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FailedSubmitSincePipelineIsFull = new ExEventLog.EventTuple(2147746873U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000918 RID: 2328
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMClientAccessError = new ExEventLog.EventTuple(2147746875U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000919 RID: 2329
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallEndedByApplication = new ExEventLog.EventTuple(263228U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400091A RID: 2330
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OutdialingConfigurationWarning = new ExEventLog.EventTuple(2147746877U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400091B RID: 2331
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutoAttendantNoGrammarFileWarning = new ExEventLog.EventTuple(2147746878U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400091C RID: 2332
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OutboundCallFailed = new ExEventLog.EventTuple(2147746879U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400091D RID: 2333
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecycledMaxTempDirSizeExceeded = new ExEventLog.EventTuple(1074005060U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400091E RID: 2334
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DialPlanCustomPromptUploadSucceeded = new ExEventLog.EventTuple(1074005062U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400091F RID: 2335
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DialPlanCustomPromptUploadFailed = new ExEventLog.EventTuple(2147746887U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000920 RID: 2336
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DialPlanCustomPromptCacheUpdated = new ExEventLog.EventTuple(1074005064U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000921 RID: 2337
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DialPlanDeleteContentFailed = new ExEventLog.EventTuple(2147746889U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000922 RID: 2338
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutoAttendantCustomPromptUploadSucceeded = new ExEventLog.EventTuple(1074005066U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000923 RID: 2339
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutoAttendantCustomPromptUploadFailed = new ExEventLog.EventTuple(2147746891U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000924 RID: 2340
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutoAttendantCustomPromptCacheUpdate = new ExEventLog.EventTuple(1074005068U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000925 RID: 2341
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AutoAttendantDeleteContentFailed = new ExEventLog.EventTuple(2147746893U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000926 RID: 2342
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ContactsNoGrammarFileWarning = new ExEventLog.EventTuple(2147746894U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000927 RID: 2343
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMInvalidSchema = new ExEventLog.EventTuple(3221488719U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000928 RID: 2344
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ADTransientError = new ExEventLog.EventTuple(2147746898U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000929 RID: 2345
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ADPermanentError = new ExEventLog.EventTuple(3221488723U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400092A RID: 2346
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADDataError = new ExEventLog.EventTuple(2147746900U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400092B RID: 2347
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidExtensionInCall = new ExEventLog.EventTuple(2147746901U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400092C RID: 2348
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnabletoRegisterForDialPlanADNotifications = new ExEventLog.EventTuple(2147746902U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400092D RID: 2349
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnabletoRegisterForIPGatewayADNotifications = new ExEventLog.EventTuple(2147746903U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400092E RID: 2350
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceCertificateDetails = new ExEventLog.EventTuple(1074005080U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400092F RID: 2351
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_IncomingTLSCallFailure = new ExEventLog.EventTuple(3221488729U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000930 RID: 2352
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StartingMode = new ExEventLog.EventTuple(1074005082U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000931 RID: 2353
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IncorrectPeers = new ExEventLog.EventTuple(2147746907U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000932 RID: 2354
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StoppingListeningforCertificateChange = new ExEventLog.EventTuple(1074005084U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000933 RID: 2355
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StartedListeningWithNewCertificate = new ExEventLog.EventTuple(1074005085U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000934 RID: 2356
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMWorkerProcessRecycledToChangeCerts = new ExEventLog.EventTuple(1074005087U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000935 RID: 2357
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CertificateNearExpiry = new ExEventLog.EventTuple(2147746912U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000936 RID: 2358
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnabletoRegisterForServerADNotifications = new ExEventLog.EventTuple(2147746913U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000937 RID: 2359
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnabletoRegisterForAutoAttendantADNotifications = new ExEventLog.EventTuple(2147746914U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000938 RID: 2360
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CertificateExpiryIsGood = new ExEventLog.EventTuple(1074005091U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000939 RID: 2361
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NoPeersFound = new ExEventLog.EventTuple(2147746916U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400093A RID: 2362
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMClientAccessCertDetails = new ExEventLog.EventTuple(1074005093U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400093B RID: 2363
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MSSIncomingTLSCallFailure = new ExEventLog.EventTuple(2147746918U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400093C RID: 2364
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AcmConversionFailed = new ExEventLog.EventTuple(2147746919U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400093D RID: 2365
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AAMissingOperatorExtension = new ExEventLog.EventTuple(3221488744U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400093E RID: 2366
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptedConfiguration = new ExEventLog.EventTuple(2147746926U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400093F RID: 2367
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptedPIN = new ExEventLog.EventTuple(3221488751U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000940 RID: 2368
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallTransferFailed = new ExEventLog.EventTuple(2147746928U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000941 RID: 2369
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSpnRegistrationFailure = new ExEventLog.EventTuple(3221488754U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000942 RID: 2370
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DisconnectOnUMIPGatewayDisabledImmediate = new ExEventLog.EventTuple(263286U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000943 RID: 2371
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DisconnectOnUMServerDisabledImmediate = new ExEventLog.EventTuple(263287U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000944 RID: 2372
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADNotificationProcessingError = new ExEventLog.EventTuple(2147746936U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000945 RID: 2373
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnabletoRegisterForHuntGroupADNotifications = new ExEventLog.EventTuple(2147746937U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000946 RID: 2374
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToResolveVoicemailCaller = new ExEventLog.EventTuple(2147746938U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000947 RID: 2375
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PingResponseFailure = new ExEventLog.EventTuple(2147746943U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000948 RID: 2376
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidSipHeader = new ExEventLog.EventTuple(2147746944U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000949 RID: 2377
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SpeechAAGrammarEntryFormatErrors = new ExEventLog.EventTuple(2147746946U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400094A RID: 2378
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AALanguageNotFound = new ExEventLog.EventTuple(2147746948U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400094B RID: 2379
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SpeechGrammarFilterListSchemaFailureWarning = new ExEventLog.EventTuple(2147746949U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400094C RID: 2380
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SpeechGrammarFilterListInvalidWarning = new ExEventLog.EventTuple(2147746950U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400094D RID: 2381
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SystemError = new ExEventLog.EventTuple(2147746951U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400094E RID: 2382
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AACustomPromptFileMissing = new ExEventLog.EventTuple(2147746952U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400094F RID: 2383
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DialPlanCustomPromptFileMissing = new ExEventLog.EventTuple(2147746953U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000950 RID: 2384
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallToUnusableAA = new ExEventLog.EventTuple(2147746956U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000951 RID: 2385
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WNoPeersFound = new ExEventLog.EventTuple(2147746957U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000952 RID: 2386
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WADAccessError = new ExEventLog.EventTuple(2147746958U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000953 RID: 2387
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallData = new ExEventLog.EventTuple(263312U, 6, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000954 RID: 2388
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DivertedExtensionNotProvisioned = new ExEventLog.EventTuple(2147746961U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000955 RID: 2389
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallDataCallAnswer = new ExEventLog.EventTuple(263314U, 6, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000956 RID: 2390
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallDataSubscriberAccess = new ExEventLog.EventTuple(263315U, 6, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000957 RID: 2391
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallDataAutoAttendant = new ExEventLog.EventTuple(263316U, 6, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000958 RID: 2392
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallDataOutbound = new ExEventLog.EventTuple(263317U, 6, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000959 RID: 2393
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RPCRequestError = new ExEventLog.EventTuple(2147746966U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400095A RID: 2394
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DuplicatePeersFound = new ExEventLog.EventTuple(2147746967U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400095B RID: 2395
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MissingDialGroupEntry = new ExEventLog.EventTuple(2147746968U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400095C RID: 2396
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EventNotifSessionInvalidFormat = new ExEventLog.EventTuple(2147746969U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400095D RID: 2397
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EventNotifSessionSignalingError = new ExEventLog.EventTuple(2147746970U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400095E RID: 2398
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PersonalContactsSearchPlatformFailure = new ExEventLog.EventTuple(2147746971U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400095F RID: 2399
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GALSearchPlatformFailure = new ExEventLog.EventTuple(2147746972U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000960 RID: 2400
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMNumberNotConfiguredForFax = new ExEventLog.EventTuple(2147746974U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000961 RID: 2401
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMServiceUnhandledException = new ExEventLog.EventTuple(3221488799U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000962 RID: 2402
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SamePeerInTwoModes = new ExEventLog.EventTuple(2147746976U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000963 RID: 2403
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToGetSocket = new ExEventLog.EventTuple(3221488804U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000964 RID: 2404
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiagnosticResponseSequence = new ExEventLog.EventTuple(263333U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000965 RID: 2405
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ContactResolutionTemporarilyDisabled = new ExEventLog.EventTuple(2147746983U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000966 RID: 2406
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallDataFax = new ExEventLog.EventTuple(263336U, 6, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000967 RID: 2407
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LangPackDirectoryNotFound = new ExEventLog.EventTuple(2147746985U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000968 RID: 2408
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidPeerDNSHostName = new ExEventLog.EventTuple(2147746986U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000969 RID: 2409
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProcessingSipHeaderForCalleeInfo = new ExEventLog.EventTuple(263444U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400096A RID: 2410
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OutDialingRequest = new ExEventLog.EventTuple(263445U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400096B RID: 2411
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindMeFailedSinceMaximumCallsLimitReached = new ExEventLog.EventTuple(2147747094U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400096C RID: 2412
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindMeOutDialingRulesFailure = new ExEventLog.EventTuple(263447U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400096D RID: 2413
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindMeInvalidPhoneNumber = new ExEventLog.EventTuple(263448U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400096E RID: 2414
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallAnsweredByPAA = new ExEventLog.EventTuple(263449U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400096F RID: 2415
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TranscriptionNotAttemptedDueToThrottling = new ExEventLog.EventTuple(2147747098U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000970 RID: 2416
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TranscriptionAttemptedButCancelled = new ExEventLog.EventTuple(2147747099U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000971 RID: 2417
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptedPAAStore = new ExEventLog.EventTuple(2147747100U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000972 RID: 2418
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NoOutboundGatewaysForDialPlanWithId = new ExEventLog.EventTuple(3221488926U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000973 RID: 2419
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToConnectToMailbox = new ExEventLog.EventTuple(2147747103U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000974 RID: 2420
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToRetrieveMailboxData = new ExEventLog.EventTuple(2147747104U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000975 RID: 2421
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TimedOutRetrievingMailboxData = new ExEventLog.EventTuple(2147747105U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000976 RID: 2422
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ExternalFqdnDetected = new ExEventLog.EventTuple(263458U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000977 RID: 2423
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallRedirectedToServer = new ExEventLog.EventTuple(263461U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000978 RID: 2424
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PlatformTlsException = new ExEventLog.EventTuple(2147747111U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000979 RID: 2425
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TimedOutEvaluatingPAA = new ExEventLog.EventTuple(2147747113U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400097A RID: 2426
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UserNotEnabledForPlayOnPhone = new ExEventLog.EventTuple(263466U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400097B RID: 2427
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VirtualNumberCall = new ExEventLog.EventTuple(263468U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400097C RID: 2428
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VirtualNumberCallBlocked = new ExEventLog.EventTuple(263469U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400097D RID: 2429
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AACustomPromptInvalid = new ExEventLog.EventTuple(3221488942U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400097E RID: 2430
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DialPlanCustomPromptInvalid = new ExEventLog.EventTuple(3221488943U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400097F RID: 2431
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MserveLookup = new ExEventLog.EventTuple(263472U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000980 RID: 2432
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MserveLookupError = new ExEventLog.EventTuple(3221488945U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000981 RID: 2433
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MserveLookupTargetForest = new ExEventLog.EventTuple(263475U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000982 RID: 2434
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TranscriptionWordCounts = new ExEventLog.EventTuple(263477U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000983 RID: 2435
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_KillWorkItemAndDelete = new ExEventLog.EventTuple(3221488951U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000984 RID: 2436
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AAPlayOnPhoneRequest = new ExEventLog.EventTuple(263483U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000985 RID: 2437
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AAOutDialingRulesFailure = new ExEventLog.EventTuple(2147747132U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000986 RID: 2438
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AAOutDialingFailure = new ExEventLog.EventTuple(2147747133U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000987 RID: 2439
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MwiMessageDeliverySucceeded = new ExEventLog.EventTuple(1074005311U, 7, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000988 RID: 2440
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MwiMessageDeliveryFailed = new ExEventLog.EventTuple(2147747136U, 7, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000989 RID: 2441
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MwiSyncMailboxFailed = new ExEventLog.EventTuple(2147747137U, 7, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400098A RID: 2442
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MwiQueryDatabaseFailed = new ExEventLog.EventTuple(2147747138U, 7, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400098B RID: 2443
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MwiTextMessageSent = new ExEventLog.EventTuple(1074005315U, 7, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400098C RID: 2444
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoPRequestError = new ExEventLog.EventTuple(2147747140U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400098D RID: 2445
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallRouterUnableToMapGatewayToForest = new ExEventLog.EventTuple(3221488967U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400098E RID: 2446
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallRouterRedirectedTenantGatewayCall = new ExEventLog.EventTuple(1074005320U, 8, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400098F RID: 2447
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMPartnerMessageSucceeded = new ExEventLog.EventTuple(1074005323U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000990 RID: 2448
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UMPartnerMessageServerFailed = new ExEventLog.EventTuple(2147747148U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000991 RID: 2449
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UMPartnerMessageNoServersAvailable = new ExEventLog.EventTuple(2147747149U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000992 RID: 2450
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMPartnerMessageEventSkippedError = new ExEventLog.EventTuple(3221488974U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000993 RID: 2451
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMPartnerMessageRpcRequestError = new ExEventLog.EventTuple(2147747151U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000994 RID: 2452
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MwiMessageDeliveryFailedToUM = new ExEventLog.EventTuple(2147747152U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000995 RID: 2453
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SipPeersUnhealthy = new ExEventLog.EventTuple(2147747192U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000996 RID: 2454
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SipPeersHealthy = new ExEventLog.EventTuple(263545U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000997 RID: 2455
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LegacyServerNotFoundInDialPlan = new ExEventLog.EventTuple(3221489018U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000998 RID: 2456
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LegacyServerNotRunningInDialPlan = new ExEventLog.EventTuple(3221489019U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000999 RID: 2457
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMServerNotFoundInSite = new ExEventLog.EventTuple(3221489020U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400099A RID: 2458
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToResolveCallerToSubscriber = new ExEventLog.EventTuple(2147747198U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400099B RID: 2459
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxAccessFailure = new ExEventLog.EventTuple(3221489023U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400099C RID: 2460
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CasToUmRpcFailure = new ExEventLog.EventTuple(3221489024U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400099D RID: 2461
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple____CasToUmRpcSuccess___ = new ExEventLog.EventTuple(3221489025U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400099E RID: 2462
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMMwiAssistantStarted = new ExEventLog.EventTuple(1074005379U, 7, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400099F RID: 2463
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMServiceCallRejected = new ExEventLog.EventTuple(3221489028U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009A0 RID: 2464
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UMServiceLowOnResources = new ExEventLog.EventTuple(3221489029U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009A1 RID: 2465
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OutboundCallFailedForOnPremiseGateway = new ExEventLog.EventTuple(2147747207U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009A2 RID: 2466
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FaxTransferFailure = new ExEventLog.EventTuple(3221489032U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009A3 RID: 2467
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UserFaxServerSetupFailure = new ExEventLog.EventTuple(3221489033U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009A4 RID: 2468
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FaxPartnerHasServerError = new ExEventLog.EventTuple(3221489034U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009A5 RID: 2469
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMStartupModeChanged = new ExEventLog.EventTuple(2147747211U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009A6 RID: 2470
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RMSIntepersonalSendFailure = new ExEventLog.EventTuple(3221489036U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009A7 RID: 2471
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RMSCallAnsweringSendFailure = new ExEventLog.EventTuple(3221489037U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009A8 RID: 2472
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RMSReadFailure = new ExEventLog.EventTuple(3221489038U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009A9 RID: 2473
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PipeLineError = new ExEventLog.EventTuple(3221489039U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009AA RID: 2474
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SipPeerCacheRefreshed = new ExEventLog.EventTuple(1074005392U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009AB RID: 2475
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FindUMIPGatewayInAD = new ExEventLog.EventTuple(1074005393U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009AC RID: 2476
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidClientCertificate = new ExEventLog.EventTuple(2147747218U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009AD RID: 2477
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMServiceFatalException = new ExEventLog.EventTuple(3221489043U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009AE RID: 2478
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FAXRequestIsNotAcceptable = new ExEventLog.EventTuple(2147747220U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009AF RID: 2479
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TranscriptionPartnerFailure = new ExEventLog.EventTuple(2147747221U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009B0 RID: 2480
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMKillCurrentProcess = new ExEventLog.EventTuple(3221489046U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009B1 RID: 2481
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToSaveCDR = new ExEventLog.EventTuple(3221489047U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009B2 RID: 2482
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToFindUMReportData = new ExEventLog.EventTuple(3221489048U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009B3 RID: 2483
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FatalErrorDuringAggregation = new ExEventLog.EventTuple(3221489049U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009B4 RID: 2484
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PermanentErrorDuringAggregation = new ExEventLog.EventTuple(3221489050U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009B5 RID: 2485
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PipelineStalled = new ExEventLog.EventTuple(3221489051U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009B6 RID: 2486
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CannotSetExtendedProp = new ExEventLog.EventTuple(2147747228U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009B7 RID: 2487
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HeuristicallyChosenSIPProxy = new ExEventLog.EventTuple(1074005405U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009B8 RID: 2488
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MRASMediaEstablishedStatusFailed = new ExEventLog.EventTuple(2147747230U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009B9 RID: 2489
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MRASCredentialsAcquisitionFailed = new ExEventLog.EventTuple(2147747231U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009BA RID: 2490
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MRASResourceAllocationFailed = new ExEventLog.EventTuple(2147747232U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009BB RID: 2491
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SIPProxyDetails = new ExEventLog.EventTuple(1074005409U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009BC RID: 2492
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CallRejectedSinceGatewayDisabled = new ExEventLog.EventTuple(3221489058U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009BD RID: 2493
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToFindEDiscoveryMailbox = new ExEventLog.EventTuple(3221489059U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009BE RID: 2494
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OCSUserNotProvisioned = new ExEventLog.EventTuple(2147747236U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009BF RID: 2495
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DialPlanOrAutoAttendantNotProvisioned = new ExEventLog.EventTuple(2147747237U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009C0 RID: 2496
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PipelineWorkItemSLAFailure = new ExEventLog.EventTuple(3221489062U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009C1 RID: 2497
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMServiceSocketShutdown = new ExEventLog.EventTuple(2147747239U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009C2 RID: 2498
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMServiceSocketOpen = new ExEventLog.EventTuple(1074005416U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009C3 RID: 2499
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MsOrganizationNotAuthoritativeDomain = new ExEventLog.EventTuple(3221489065U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009C4 RID: 2500
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MobileSpeechRecoRPCGeneralUnexpectedFailure = new ExEventLog.EventTuple(3221489216U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009C5 RID: 2501
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MobileSpeechRecoRPCUnexpectedFailure = new ExEventLog.EventTuple(3221489217U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009C6 RID: 2502
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MobileSpeechRecoRPCFailure = new ExEventLog.EventTuple(3221489218U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009C7 RID: 2503
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MobileSpeechRecoRPCSuccess = new ExEventLog.EventTuple(263747U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009C8 RID: 2504
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MobileSpeechRecoAddRecoRequestRPCParams = new ExEventLog.EventTuple(263748U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009C9 RID: 2505
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MobileSpeechRecoRecognizeRPCParams = new ExEventLog.EventTuple(263749U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009CA RID: 2506
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MobileSpeechRecoClientRPCFailure = new ExEventLog.EventTuple(3221489222U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009CB RID: 2507
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MobileSpeechRecoClientRPCSuccess = new ExEventLog.EventTuple(263751U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009CC RID: 2508
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MobileSpeechRecoClientAddRecoRequestRPCParams = new ExEventLog.EventTuple(263752U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009CD RID: 2509
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MobileSpeechRecoClientRecognizeRPCParams = new ExEventLog.EventTuple(263753U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009CE RID: 2510
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMGrammarFetcherError = new ExEventLog.EventTuple(3221489226U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009CF RID: 2511
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMGrammarFetcherSuccess = new ExEventLog.EventTuple(1074005579U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009D0 RID: 2512
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GrammarGenerationStarted = new ExEventLog.EventTuple(1074005581U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009D1 RID: 2513
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GrammarGenerationSuccessful = new ExEventLog.EventTuple(1074005582U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009D2 RID: 2514
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GrammarGenerationFailed = new ExEventLog.EventTuple(3221489231U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009D3 RID: 2515
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GrammarGenerationMissingCulture = new ExEventLog.EventTuple(3221489232U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009D4 RID: 2516
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GrammarGeneratorCouldntFindUser = new ExEventLog.EventTuple(3221489233U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009D5 RID: 2517
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GrammarGenerationCouldntFindSystemMailbox = new ExEventLog.EventTuple(3221489234U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009D6 RID: 2518
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MobileSpeechRecoLoadGrammarFailure = new ExEventLog.EventTuple(3221489236U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009D7 RID: 2519
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMCallRouterSocketShutdown = new ExEventLog.EventTuple(2147747413U, 8, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009D8 RID: 2520
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallRoutedSuccessfully = new ExEventLog.EventTuple(263766U, 8, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009D9 RID: 2521
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GrammarGenerationCleanupFailed = new ExEventLog.EventTuple(3221489239U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009DA RID: 2522
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GrammarFetcherCleanupFailed = new ExEventLog.EventTuple(3221489240U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009DB RID: 2523
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToCreateDirectoryProcessorDirectory = new ExEventLog.EventTuple(3221489241U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009DC RID: 2524
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CopyADToFileStarted = new ExEventLog.EventTuple(1074005597U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009DD RID: 2525
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CopyADToFileCompleted = new ExEventLog.EventTuple(1074005598U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009DE RID: 2526
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GrammarGenerationWritingGrammarEntriesStarted = new ExEventLog.EventTuple(1074005599U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009DF RID: 2527
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GrammarGenerationWritingGrammarEntriesCompleted = new ExEventLog.EventTuple(1074005600U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009E0 RID: 2528
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnabletoRegisterForCallRouterSettingsADNotifications = new ExEventLog.EventTuple(2147747425U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009E1 RID: 2529
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DynamicDirectoryGrammarGenerationFailure = new ExEventLog.EventTuple(3221489250U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009E2 RID: 2530
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DialPlanDefaultLanguageNotFound = new ExEventLog.EventTuple(2147747427U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009E3 RID: 2531
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceRequestRejected = new ExEventLog.EventTuple(2147747428U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009E4 RID: 2532
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UserNotificationProxied = new ExEventLog.EventTuple(263781U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009E5 RID: 2533
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UserNotificationFailed = new ExEventLog.EventTuple(3221489254U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009E6 RID: 2534
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LoadNormalizationCacheFailed = new ExEventLog.EventTuple(2147747431U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009E7 RID: 2535
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SaveNormalizationCacheFailed = new ExEventLog.EventTuple(2147747432U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009E8 RID: 2536
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DirectoryProcessorStarted = new ExEventLog.EventTuple(1074005609U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009E9 RID: 2537
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DirectoryProcessorCompleted = new ExEventLog.EventTuple(1074005610U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009EA RID: 2538
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallRouterCertificateNearExpiry = new ExEventLog.EventTuple(2147747435U, 8, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009EB RID: 2539
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallRouterCertificateExpiryIsGood = new ExEventLog.EventTuple(1074005612U, 8, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009EC RID: 2540
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallRouterCertificateDetails = new ExEventLog.EventTuple(1074005613U, 8, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009ED RID: 2541
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallRouterStartingMode = new ExEventLog.EventTuple(1074005614U, 8, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009EE RID: 2542
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallRouterCallRejected = new ExEventLog.EventTuple(3221489263U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009EF RID: 2543
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallRouterSocketOpen = new ExEventLog.EventTuple(1074005616U, 8, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009F0 RID: 2544
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CallRouterIncomingTLSCallFailure = new ExEventLog.EventTuple(2147747441U, 8, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009F1 RID: 2545
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CallRouterInboundCallParams = new ExEventLog.EventTuple(263794U, 8, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009F2 RID: 2546
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OptionsMessageRejected = new ExEventLog.EventTuple(2147747443U, 8, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009F3 RID: 2547
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CopyADToFileFailed = new ExEventLog.EventTuple(3221489268U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009F4 RID: 2548
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GrammarGenerationSkippedNoADFile = new ExEventLog.EventTuple(3221489269U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009F5 RID: 2549
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DirectoryProcessorTaskThrewException = new ExEventLog.EventTuple(2147747446U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009F6 RID: 2550
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DtmfMapGenerationStarted = new ExEventLog.EventTuple(1074005623U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009F7 RID: 2551
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DtmfMapGenerationSuccessful = new ExEventLog.EventTuple(1074005624U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009F8 RID: 2552
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DtmfMapUpdateFailed = new ExEventLog.EventTuple(2147747449U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009F9 RID: 2553
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DtmfMapGenerationSkippedNoADFile = new ExEventLog.EventTuple(3221489274U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009FA RID: 2554
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SpeechRecoRequestParams = new ExEventLog.EventTuple(1074005627U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009FB RID: 2555
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidSpeechRecoRequest = new ExEventLog.EventTuple(2147747452U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040009FC RID: 2556
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SpeechRecoRequestCompleted = new ExEventLog.EventTuple(1074005629U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009FD RID: 2557
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SpeechRecoRequestFailed = new ExEventLog.EventTuple(3221489278U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009FE RID: 2558
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StartFindInGALSpeechRecoRequestParams = new ExEventLog.EventTuple(1074005631U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040009FF RID: 2559
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StartFindInGALSpeechRecoRequestSuccess = new ExEventLog.EventTuple(1074005632U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A00 RID: 2560
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StartFindInGALSpeechRecoRequestFailed = new ExEventLog.EventTuple(3221489281U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A01 RID: 2561
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CompleteFindInGALSpeechRecoRequestParams = new ExEventLog.EventTuple(1074005634U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A02 RID: 2562
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CompleteFindInGALSpeechRecoRequestSuccess = new ExEventLog.EventTuple(1074005635U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A03 RID: 2563
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CompleteFindInGALSpeechRecoRequestFailed = new ExEventLog.EventTuple(3221489284U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A04 RID: 2564
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MobileSpeechRecoClientStartFindInGALRequestParams = new ExEventLog.EventTuple(1074005637U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A05 RID: 2565
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MobileSpeechRecoClientCompleteFindInGALRequestParams = new ExEventLog.EventTuple(1074005638U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A06 RID: 2566
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MobileSpeechRecoClientFindInGALResult = new ExEventLog.EventTuple(1074005639U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A07 RID: 2567
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReadLastSuccessRunIDFailed = new ExEventLog.EventTuple(1074005641U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A08 RID: 2568
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DirectoryProcessorInitialStepEncounteredException = new ExEventLog.EventTuple(3221489290U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A09 RID: 2569
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LoadDtmfMapGenerationMetadataFailed = new ExEventLog.EventTuple(2147747467U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A0A RID: 2570
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SaveDtmfMapGenerationMetadataFailed = new ExEventLog.EventTuple(2147747468U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A0B RID: 2571
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMMailboxCmdletError = new ExEventLog.EventTuple(3221489293U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A0C RID: 2572
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GrammarFileUploadToSystemMailboxFailed = new ExEventLog.EventTuple(3221489294U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A0D RID: 2573
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GrammarMailboxNotFound = new ExEventLog.EventTuple(2147747471U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A0E RID: 2574
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SetUMGrammarReadyFlagFailed = new ExEventLog.EventTuple(3221489296U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A0F RID: 2575
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MobileSpeechRecoTimeout = new ExEventLog.EventTuple(2147747473U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A10 RID: 2576
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UploadNormalizationCacheFailed = new ExEventLog.EventTuple(2147747474U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A11 RID: 2577
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DownloadNormalizationCacheFailed = new ExEventLog.EventTuple(2147747475U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A12 RID: 2578
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SipPeerCertificateSubjectName = new ExEventLog.EventTuple(3221489300U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A13 RID: 2579
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UploadDtmfMapMetadataFailed = new ExEventLog.EventTuple(2147747477U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A14 RID: 2580
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DownloadDtmfMapMetadataFailed = new ExEventLog.EventTuple(2147747478U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A15 RID: 2581
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MobileSpeechRecoClientAsyncCallTimedOut = new ExEventLog.EventTuple(3221489303U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A16 RID: 2582
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToAccessOrganizationMailbox = new ExEventLog.EventTuple(3221489304U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A17 RID: 2583
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SetScaleOutCapabilityFailed = new ExEventLog.EventTuple(3221489305U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A18 RID: 2584
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GrammarFileMaxEntriesExceeded = new ExEventLog.EventTuple(2147747482U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A19 RID: 2585
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GrammarFileMaxCountExceeded = new ExEventLog.EventTuple(2147747483U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A1A RID: 2586
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MRASMediaChannelEstablishFailed = new ExEventLog.EventTuple(2147747484U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000A1B RID: 2587
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WatsoningDueToTimeout = new ExEventLog.EventTuple(2147747485U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000A1C RID: 2588
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WatsoningDueToWorkerProcessNotTerminating = new ExEventLog.EventTuple(2147747486U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000231 RID: 561
		private enum Category : short
		{
			// Token: 0x04000A1E RID: 2590
			UMWorkerProcess = 1,
			// Token: 0x04000A1F RID: 2591
			UMCore,
			// Token: 0x04000A20 RID: 2592
			UMManagement,
			// Token: 0x04000A21 RID: 2593
			UMService,
			// Token: 0x04000A22 RID: 2594
			UMClientAccess,
			// Token: 0x04000A23 RID: 2595
			UMCallData,
			// Token: 0x04000A24 RID: 2596
			MWI_General,
			// Token: 0x04000A25 RID: 2597
			UMCallRouter
		}

		// Token: 0x02000232 RID: 562
		internal enum Message : uint
		{
			// Token: 0x04000A27 RID: 2599
			UMWorkerProcessStartSuccess = 263144U,
			// Token: 0x04000A28 RID: 2600
			UMWorkerProcessStartFailure = 3221488617U,
			// Token: 0x04000A29 RID: 2601
			UMWorkerProcessStopSuccess = 263146U,
			// Token: 0x04000A2A RID: 2602
			UMWorkerProcessStopFailure = 3221488619U,
			// Token: 0x04000A2B RID: 2603
			InboundCallParams = 263148U,
			// Token: 0x04000A2C RID: 2604
			OutboundCallParams,
			// Token: 0x04000A2D RID: 2605
			CallReceived,
			// Token: 0x04000A2E RID: 2606
			CallEndedByUser,
			// Token: 0x04000A2F RID: 2607
			FsmConfigurationError = 3221488624U,
			// Token: 0x04000A30 RID: 2608
			FsmActivityStart = 263153U,
			// Token: 0x04000A31 RID: 2609
			FsmConfigurationInitialized,
			// Token: 0x04000A32 RID: 2610
			PromptsPlayed,
			// Token: 0x04000A33 RID: 2611
			MailboxLocked,
			// Token: 0x04000A34 RID: 2612
			LogonDisconnect,
			// Token: 0x04000A35 RID: 2613
			ExceptionDuringCall = 3221488630U,
			// Token: 0x04000A36 RID: 2614
			UMUserEnabled = 263159U,
			// Token: 0x04000A37 RID: 2615
			UMUserDisabled,
			// Token: 0x04000A38 RID: 2616
			UMUserUnlocked,
			// Token: 0x04000A39 RID: 2617
			UMUserPasswordChanged,
			// Token: 0x04000A3A RID: 2618
			InvalidChecksum = 3221488635U,
			// Token: 0x04000A3B RID: 2619
			DirectorySearchResults = 263164U,
			// Token: 0x04000A3C RID: 2620
			CallRejected = 3221488637U,
			// Token: 0x04000A3D RID: 2621
			UMUserNotConfiguredForFax = 2147746825U,
			// Token: 0x04000A3E RID: 2622
			SuccessfulLogon = 263180U,
			// Token: 0x04000A3F RID: 2623
			UMServiceStartSuccess,
			// Token: 0x04000A40 RID: 2624
			UMServiceStartFailure = 3221488654U,
			// Token: 0x04000A41 RID: 2625
			UMServiceStopSuccess = 263183U,
			// Token: 0x04000A42 RID: 2626
			UMWorkerProcessExited = 1074005014U,
			// Token: 0x04000A43 RID: 2627
			UMWorkerProcessRequestedRecycle,
			// Token: 0x04000A44 RID: 2628
			UMWorkerProcessNoProcessData,
			// Token: 0x04000A45 RID: 2629
			RecycledMaxCallsExceeded,
			// Token: 0x04000A46 RID: 2630
			WatsoningDueToRecycling,
			// Token: 0x04000A47 RID: 2631
			RecycledMaxMemoryPressureExceeded,
			// Token: 0x04000A48 RID: 2632
			RecycledMaxUptimeExceeded,
			// Token: 0x04000A49 RID: 2633
			RecycledMaxHeartBeatsMissedExceeded,
			// Token: 0x04000A4A RID: 2634
			KilledMaxStartuptimeExceeded,
			// Token: 0x04000A4B RID: 2635
			KilledMaxRetireTimeExceeded,
			// Token: 0x04000A4C RID: 2636
			UMServiceStateChange,
			// Token: 0x04000A4D RID: 2637
			UMWorkerProcessUnhandledException = 3221488673U,
			// Token: 0x04000A4E RID: 2638
			KilledCouldntEstablishControlChannel = 1074005027U,
			// Token: 0x04000A4F RID: 2639
			NewDialPlanCreated = 263204U,
			// Token: 0x04000A50 RID: 2640
			NewIPGatewayCreated,
			// Token: 0x04000A51 RID: 2641
			NewHuntGroupCreated,
			// Token: 0x04000A52 RID: 2642
			DialPlanRemoved,
			// Token: 0x04000A53 RID: 2643
			IPGatewayRemoved,
			// Token: 0x04000A54 RID: 2644
			HuntGroupRemoved,
			// Token: 0x04000A55 RID: 2645
			UMServerEnabled,
			// Token: 0x04000A56 RID: 2646
			IPGatewayEnabled,
			// Token: 0x04000A57 RID: 2647
			UMServerDisabled,
			// Token: 0x04000A58 RID: 2648
			IPGatewayDisabled,
			// Token: 0x04000A59 RID: 2649
			AutoAttendantCreated,
			// Token: 0x04000A5A RID: 2650
			AutoAttendantEnabled,
			// Token: 0x04000A5B RID: 2651
			AutoAttendantDisabled,
			// Token: 0x04000A5C RID: 2652
			CallTransfer,
			// Token: 0x04000A5D RID: 2653
			KillWorkItemAndMoveToBadVMFolder = 3221488690U,
			// Token: 0x04000A5E RID: 2654
			PlayOnPhoneRequest = 263219U,
			// Token: 0x04000A5F RID: 2655
			OutDialingRulesFailure,
			// Token: 0x04000A60 RID: 2656
			DisconnectRequest,
			// Token: 0x04000A61 RID: 2657
			ExExceptionDuringCall = 2147746870U,
			// Token: 0x04000A62 RID: 2658
			PlatformException,
			// Token: 0x04000A63 RID: 2659
			QuotaExceededFailedSubmit,
			// Token: 0x04000A64 RID: 2660
			FailedSubmitSincePipelineIsFull,
			// Token: 0x04000A65 RID: 2661
			UMClientAccessError = 2147746875U,
			// Token: 0x04000A66 RID: 2662
			CallEndedByApplication = 263228U,
			// Token: 0x04000A67 RID: 2663
			OutdialingConfigurationWarning = 2147746877U,
			// Token: 0x04000A68 RID: 2664
			AutoAttendantNoGrammarFileWarning,
			// Token: 0x04000A69 RID: 2665
			OutboundCallFailed,
			// Token: 0x04000A6A RID: 2666
			RecycledMaxTempDirSizeExceeded = 1074005060U,
			// Token: 0x04000A6B RID: 2667
			DialPlanCustomPromptUploadSucceeded = 1074005062U,
			// Token: 0x04000A6C RID: 2668
			DialPlanCustomPromptUploadFailed = 2147746887U,
			// Token: 0x04000A6D RID: 2669
			DialPlanCustomPromptCacheUpdated = 1074005064U,
			// Token: 0x04000A6E RID: 2670
			DialPlanDeleteContentFailed = 2147746889U,
			// Token: 0x04000A6F RID: 2671
			AutoAttendantCustomPromptUploadSucceeded = 1074005066U,
			// Token: 0x04000A70 RID: 2672
			AutoAttendantCustomPromptUploadFailed = 2147746891U,
			// Token: 0x04000A71 RID: 2673
			AutoAttendantCustomPromptCacheUpdate = 1074005068U,
			// Token: 0x04000A72 RID: 2674
			AutoAttendantDeleteContentFailed = 2147746893U,
			// Token: 0x04000A73 RID: 2675
			ContactsNoGrammarFileWarning,
			// Token: 0x04000A74 RID: 2676
			UMInvalidSchema = 3221488719U,
			// Token: 0x04000A75 RID: 2677
			ADTransientError = 2147746898U,
			// Token: 0x04000A76 RID: 2678
			ADPermanentError = 3221488723U,
			// Token: 0x04000A77 RID: 2679
			ADDataError = 2147746900U,
			// Token: 0x04000A78 RID: 2680
			InvalidExtensionInCall,
			// Token: 0x04000A79 RID: 2681
			UnabletoRegisterForDialPlanADNotifications,
			// Token: 0x04000A7A RID: 2682
			UnabletoRegisterForIPGatewayADNotifications,
			// Token: 0x04000A7B RID: 2683
			ServiceCertificateDetails = 1074005080U,
			// Token: 0x04000A7C RID: 2684
			IncomingTLSCallFailure = 3221488729U,
			// Token: 0x04000A7D RID: 2685
			StartingMode = 1074005082U,
			// Token: 0x04000A7E RID: 2686
			IncorrectPeers = 2147746907U,
			// Token: 0x04000A7F RID: 2687
			StoppingListeningforCertificateChange = 1074005084U,
			// Token: 0x04000A80 RID: 2688
			StartedListeningWithNewCertificate,
			// Token: 0x04000A81 RID: 2689
			UMWorkerProcessRecycledToChangeCerts = 1074005087U,
			// Token: 0x04000A82 RID: 2690
			CertificateNearExpiry = 2147746912U,
			// Token: 0x04000A83 RID: 2691
			UnabletoRegisterForServerADNotifications,
			// Token: 0x04000A84 RID: 2692
			UnabletoRegisterForAutoAttendantADNotifications,
			// Token: 0x04000A85 RID: 2693
			CertificateExpiryIsGood = 1074005091U,
			// Token: 0x04000A86 RID: 2694
			NoPeersFound = 2147746916U,
			// Token: 0x04000A87 RID: 2695
			UMClientAccessCertDetails = 1074005093U,
			// Token: 0x04000A88 RID: 2696
			MSSIncomingTLSCallFailure = 2147746918U,
			// Token: 0x04000A89 RID: 2697
			AcmConversionFailed,
			// Token: 0x04000A8A RID: 2698
			AAMissingOperatorExtension = 3221488744U,
			// Token: 0x04000A8B RID: 2699
			CorruptedConfiguration = 2147746926U,
			// Token: 0x04000A8C RID: 2700
			CorruptedPIN = 3221488751U,
			// Token: 0x04000A8D RID: 2701
			CallTransferFailed = 2147746928U,
			// Token: 0x04000A8E RID: 2702
			SmtpSpnRegistrationFailure = 3221488754U,
			// Token: 0x04000A8F RID: 2703
			DisconnectOnUMIPGatewayDisabledImmediate = 263286U,
			// Token: 0x04000A90 RID: 2704
			DisconnectOnUMServerDisabledImmediate,
			// Token: 0x04000A91 RID: 2705
			ADNotificationProcessingError = 2147746936U,
			// Token: 0x04000A92 RID: 2706
			UnabletoRegisterForHuntGroupADNotifications,
			// Token: 0x04000A93 RID: 2707
			UnableToResolveVoicemailCaller,
			// Token: 0x04000A94 RID: 2708
			PingResponseFailure = 2147746943U,
			// Token: 0x04000A95 RID: 2709
			InvalidSipHeader,
			// Token: 0x04000A96 RID: 2710
			SpeechAAGrammarEntryFormatErrors = 2147746946U,
			// Token: 0x04000A97 RID: 2711
			AALanguageNotFound = 2147746948U,
			// Token: 0x04000A98 RID: 2712
			SpeechGrammarFilterListSchemaFailureWarning,
			// Token: 0x04000A99 RID: 2713
			SpeechGrammarFilterListInvalidWarning,
			// Token: 0x04000A9A RID: 2714
			SystemError,
			// Token: 0x04000A9B RID: 2715
			AACustomPromptFileMissing,
			// Token: 0x04000A9C RID: 2716
			DialPlanCustomPromptFileMissing,
			// Token: 0x04000A9D RID: 2717
			CallToUnusableAA = 2147746956U,
			// Token: 0x04000A9E RID: 2718
			WNoPeersFound,
			// Token: 0x04000A9F RID: 2719
			WADAccessError,
			// Token: 0x04000AA0 RID: 2720
			CallData = 263312U,
			// Token: 0x04000AA1 RID: 2721
			DivertedExtensionNotProvisioned = 2147746961U,
			// Token: 0x04000AA2 RID: 2722
			CallDataCallAnswer = 263314U,
			// Token: 0x04000AA3 RID: 2723
			CallDataSubscriberAccess,
			// Token: 0x04000AA4 RID: 2724
			CallDataAutoAttendant,
			// Token: 0x04000AA5 RID: 2725
			CallDataOutbound,
			// Token: 0x04000AA6 RID: 2726
			RPCRequestError = 2147746966U,
			// Token: 0x04000AA7 RID: 2727
			DuplicatePeersFound,
			// Token: 0x04000AA8 RID: 2728
			MissingDialGroupEntry,
			// Token: 0x04000AA9 RID: 2729
			EventNotifSessionInvalidFormat,
			// Token: 0x04000AAA RID: 2730
			EventNotifSessionSignalingError,
			// Token: 0x04000AAB RID: 2731
			PersonalContactsSearchPlatformFailure,
			// Token: 0x04000AAC RID: 2732
			GALSearchPlatformFailure,
			// Token: 0x04000AAD RID: 2733
			UMNumberNotConfiguredForFax = 2147746974U,
			// Token: 0x04000AAE RID: 2734
			UMServiceUnhandledException = 3221488799U,
			// Token: 0x04000AAF RID: 2735
			SamePeerInTwoModes = 2147746976U,
			// Token: 0x04000AB0 RID: 2736
			UnableToGetSocket = 3221488804U,
			// Token: 0x04000AB1 RID: 2737
			DiagnosticResponseSequence = 263333U,
			// Token: 0x04000AB2 RID: 2738
			ContactResolutionTemporarilyDisabled = 2147746983U,
			// Token: 0x04000AB3 RID: 2739
			CallDataFax = 263336U,
			// Token: 0x04000AB4 RID: 2740
			LangPackDirectoryNotFound = 2147746985U,
			// Token: 0x04000AB5 RID: 2741
			InvalidPeerDNSHostName,
			// Token: 0x04000AB6 RID: 2742
			ProcessingSipHeaderForCalleeInfo = 263444U,
			// Token: 0x04000AB7 RID: 2743
			OutDialingRequest,
			// Token: 0x04000AB8 RID: 2744
			FindMeFailedSinceMaximumCallsLimitReached = 2147747094U,
			// Token: 0x04000AB9 RID: 2745
			FindMeOutDialingRulesFailure = 263447U,
			// Token: 0x04000ABA RID: 2746
			FindMeInvalidPhoneNumber,
			// Token: 0x04000ABB RID: 2747
			CallAnsweredByPAA,
			// Token: 0x04000ABC RID: 2748
			TranscriptionNotAttemptedDueToThrottling = 2147747098U,
			// Token: 0x04000ABD RID: 2749
			TranscriptionAttemptedButCancelled,
			// Token: 0x04000ABE RID: 2750
			CorruptedPAAStore,
			// Token: 0x04000ABF RID: 2751
			NoOutboundGatewaysForDialPlanWithId = 3221488926U,
			// Token: 0x04000AC0 RID: 2752
			FailedToConnectToMailbox = 2147747103U,
			// Token: 0x04000AC1 RID: 2753
			FailedToRetrieveMailboxData,
			// Token: 0x04000AC2 RID: 2754
			TimedOutRetrievingMailboxData,
			// Token: 0x04000AC3 RID: 2755
			ExternalFqdnDetected = 263458U,
			// Token: 0x04000AC4 RID: 2756
			CallRedirectedToServer = 263461U,
			// Token: 0x04000AC5 RID: 2757
			PlatformTlsException = 2147747111U,
			// Token: 0x04000AC6 RID: 2758
			TimedOutEvaluatingPAA = 2147747113U,
			// Token: 0x04000AC7 RID: 2759
			UserNotEnabledForPlayOnPhone = 263466U,
			// Token: 0x04000AC8 RID: 2760
			VirtualNumberCall = 263468U,
			// Token: 0x04000AC9 RID: 2761
			VirtualNumberCallBlocked,
			// Token: 0x04000ACA RID: 2762
			AACustomPromptInvalid = 3221488942U,
			// Token: 0x04000ACB RID: 2763
			DialPlanCustomPromptInvalid,
			// Token: 0x04000ACC RID: 2764
			MserveLookup = 263472U,
			// Token: 0x04000ACD RID: 2765
			MserveLookupError = 3221488945U,
			// Token: 0x04000ACE RID: 2766
			MserveLookupTargetForest = 263475U,
			// Token: 0x04000ACF RID: 2767
			TranscriptionWordCounts = 263477U,
			// Token: 0x04000AD0 RID: 2768
			KillWorkItemAndDelete = 3221488951U,
			// Token: 0x04000AD1 RID: 2769
			AAPlayOnPhoneRequest = 263483U,
			// Token: 0x04000AD2 RID: 2770
			AAOutDialingRulesFailure = 2147747132U,
			// Token: 0x04000AD3 RID: 2771
			AAOutDialingFailure,
			// Token: 0x04000AD4 RID: 2772
			MwiMessageDeliverySucceeded = 1074005311U,
			// Token: 0x04000AD5 RID: 2773
			MwiMessageDeliveryFailed = 2147747136U,
			// Token: 0x04000AD6 RID: 2774
			MwiSyncMailboxFailed,
			// Token: 0x04000AD7 RID: 2775
			MwiQueryDatabaseFailed,
			// Token: 0x04000AD8 RID: 2776
			MwiTextMessageSent = 1074005315U,
			// Token: 0x04000AD9 RID: 2777
			PoPRequestError = 2147747140U,
			// Token: 0x04000ADA RID: 2778
			CallRouterUnableToMapGatewayToForest = 3221488967U,
			// Token: 0x04000ADB RID: 2779
			CallRouterRedirectedTenantGatewayCall = 1074005320U,
			// Token: 0x04000ADC RID: 2780
			UMPartnerMessageSucceeded = 1074005323U,
			// Token: 0x04000ADD RID: 2781
			UMPartnerMessageServerFailed = 2147747148U,
			// Token: 0x04000ADE RID: 2782
			UMPartnerMessageNoServersAvailable,
			// Token: 0x04000ADF RID: 2783
			UMPartnerMessageEventSkippedError = 3221488974U,
			// Token: 0x04000AE0 RID: 2784
			UMPartnerMessageRpcRequestError = 2147747151U,
			// Token: 0x04000AE1 RID: 2785
			MwiMessageDeliveryFailedToUM,
			// Token: 0x04000AE2 RID: 2786
			SipPeersUnhealthy = 2147747192U,
			// Token: 0x04000AE3 RID: 2787
			SipPeersHealthy = 263545U,
			// Token: 0x04000AE4 RID: 2788
			LegacyServerNotFoundInDialPlan = 3221489018U,
			// Token: 0x04000AE5 RID: 2789
			LegacyServerNotRunningInDialPlan,
			// Token: 0x04000AE6 RID: 2790
			UMServerNotFoundInSite,
			// Token: 0x04000AE7 RID: 2791
			UnableToResolveCallerToSubscriber = 2147747198U,
			// Token: 0x04000AE8 RID: 2792
			MailboxAccessFailure = 3221489023U,
			// Token: 0x04000AE9 RID: 2793
			CasToUmRpcFailure,
			// Token: 0x04000AEA RID: 2794
			___CasToUmRpcSuccess___,
			// Token: 0x04000AEB RID: 2795
			UMMwiAssistantStarted = 1074005379U,
			// Token: 0x04000AEC RID: 2796
			UMServiceCallRejected = 3221489028U,
			// Token: 0x04000AED RID: 2797
			UMServiceLowOnResources,
			// Token: 0x04000AEE RID: 2798
			OutboundCallFailedForOnPremiseGateway = 2147747207U,
			// Token: 0x04000AEF RID: 2799
			FaxTransferFailure = 3221489032U,
			// Token: 0x04000AF0 RID: 2800
			UserFaxServerSetupFailure,
			// Token: 0x04000AF1 RID: 2801
			FaxPartnerHasServerError,
			// Token: 0x04000AF2 RID: 2802
			UMStartupModeChanged = 2147747211U,
			// Token: 0x04000AF3 RID: 2803
			RMSIntepersonalSendFailure = 3221489036U,
			// Token: 0x04000AF4 RID: 2804
			RMSCallAnsweringSendFailure,
			// Token: 0x04000AF5 RID: 2805
			RMSReadFailure,
			// Token: 0x04000AF6 RID: 2806
			PipeLineError,
			// Token: 0x04000AF7 RID: 2807
			SipPeerCacheRefreshed = 1074005392U,
			// Token: 0x04000AF8 RID: 2808
			FindUMIPGatewayInAD,
			// Token: 0x04000AF9 RID: 2809
			InvalidClientCertificate = 2147747218U,
			// Token: 0x04000AFA RID: 2810
			UMServiceFatalException = 3221489043U,
			// Token: 0x04000AFB RID: 2811
			FAXRequestIsNotAcceptable = 2147747220U,
			// Token: 0x04000AFC RID: 2812
			TranscriptionPartnerFailure,
			// Token: 0x04000AFD RID: 2813
			UMKillCurrentProcess = 3221489046U,
			// Token: 0x04000AFE RID: 2814
			UnableToSaveCDR,
			// Token: 0x04000AFF RID: 2815
			UnableToFindUMReportData,
			// Token: 0x04000B00 RID: 2816
			FatalErrorDuringAggregation,
			// Token: 0x04000B01 RID: 2817
			PermanentErrorDuringAggregation,
			// Token: 0x04000B02 RID: 2818
			PipelineStalled,
			// Token: 0x04000B03 RID: 2819
			CannotSetExtendedProp = 2147747228U,
			// Token: 0x04000B04 RID: 2820
			HeuristicallyChosenSIPProxy = 1074005405U,
			// Token: 0x04000B05 RID: 2821
			MRASMediaEstablishedStatusFailed = 2147747230U,
			// Token: 0x04000B06 RID: 2822
			MRASCredentialsAcquisitionFailed,
			// Token: 0x04000B07 RID: 2823
			MRASResourceAllocationFailed,
			// Token: 0x04000B08 RID: 2824
			SIPProxyDetails = 1074005409U,
			// Token: 0x04000B09 RID: 2825
			CallRejectedSinceGatewayDisabled = 3221489058U,
			// Token: 0x04000B0A RID: 2826
			UnableToFindEDiscoveryMailbox,
			// Token: 0x04000B0B RID: 2827
			OCSUserNotProvisioned = 2147747236U,
			// Token: 0x04000B0C RID: 2828
			DialPlanOrAutoAttendantNotProvisioned,
			// Token: 0x04000B0D RID: 2829
			PipelineWorkItemSLAFailure = 3221489062U,
			// Token: 0x04000B0E RID: 2830
			UMServiceSocketShutdown = 2147747239U,
			// Token: 0x04000B0F RID: 2831
			UMServiceSocketOpen = 1074005416U,
			// Token: 0x04000B10 RID: 2832
			MsOrganizationNotAuthoritativeDomain = 3221489065U,
			// Token: 0x04000B11 RID: 2833
			MobileSpeechRecoRPCGeneralUnexpectedFailure = 3221489216U,
			// Token: 0x04000B12 RID: 2834
			MobileSpeechRecoRPCUnexpectedFailure,
			// Token: 0x04000B13 RID: 2835
			MobileSpeechRecoRPCFailure,
			// Token: 0x04000B14 RID: 2836
			MobileSpeechRecoRPCSuccess = 263747U,
			// Token: 0x04000B15 RID: 2837
			MobileSpeechRecoAddRecoRequestRPCParams,
			// Token: 0x04000B16 RID: 2838
			MobileSpeechRecoRecognizeRPCParams,
			// Token: 0x04000B17 RID: 2839
			MobileSpeechRecoClientRPCFailure = 3221489222U,
			// Token: 0x04000B18 RID: 2840
			MobileSpeechRecoClientRPCSuccess = 263751U,
			// Token: 0x04000B19 RID: 2841
			MobileSpeechRecoClientAddRecoRequestRPCParams,
			// Token: 0x04000B1A RID: 2842
			MobileSpeechRecoClientRecognizeRPCParams,
			// Token: 0x04000B1B RID: 2843
			UMGrammarFetcherError = 3221489226U,
			// Token: 0x04000B1C RID: 2844
			UMGrammarFetcherSuccess = 1074005579U,
			// Token: 0x04000B1D RID: 2845
			GrammarGenerationStarted = 1074005581U,
			// Token: 0x04000B1E RID: 2846
			GrammarGenerationSuccessful,
			// Token: 0x04000B1F RID: 2847
			GrammarGenerationFailed = 3221489231U,
			// Token: 0x04000B20 RID: 2848
			GrammarGenerationMissingCulture,
			// Token: 0x04000B21 RID: 2849
			GrammarGeneratorCouldntFindUser,
			// Token: 0x04000B22 RID: 2850
			GrammarGenerationCouldntFindSystemMailbox,
			// Token: 0x04000B23 RID: 2851
			MobileSpeechRecoLoadGrammarFailure = 3221489236U,
			// Token: 0x04000B24 RID: 2852
			UMCallRouterSocketShutdown = 2147747413U,
			// Token: 0x04000B25 RID: 2853
			CallRoutedSuccessfully = 263766U,
			// Token: 0x04000B26 RID: 2854
			GrammarGenerationCleanupFailed = 3221489239U,
			// Token: 0x04000B27 RID: 2855
			GrammarFetcherCleanupFailed,
			// Token: 0x04000B28 RID: 2856
			UnableToCreateDirectoryProcessorDirectory,
			// Token: 0x04000B29 RID: 2857
			CopyADToFileStarted = 1074005597U,
			// Token: 0x04000B2A RID: 2858
			CopyADToFileCompleted,
			// Token: 0x04000B2B RID: 2859
			GrammarGenerationWritingGrammarEntriesStarted,
			// Token: 0x04000B2C RID: 2860
			GrammarGenerationWritingGrammarEntriesCompleted,
			// Token: 0x04000B2D RID: 2861
			UnabletoRegisterForCallRouterSettingsADNotifications = 2147747425U,
			// Token: 0x04000B2E RID: 2862
			DynamicDirectoryGrammarGenerationFailure = 3221489250U,
			// Token: 0x04000B2F RID: 2863
			DialPlanDefaultLanguageNotFound = 2147747427U,
			// Token: 0x04000B30 RID: 2864
			ServiceRequestRejected,
			// Token: 0x04000B31 RID: 2865
			UserNotificationProxied = 263781U,
			// Token: 0x04000B32 RID: 2866
			UserNotificationFailed = 3221489254U,
			// Token: 0x04000B33 RID: 2867
			LoadNormalizationCacheFailed = 2147747431U,
			// Token: 0x04000B34 RID: 2868
			SaveNormalizationCacheFailed,
			// Token: 0x04000B35 RID: 2869
			DirectoryProcessorStarted = 1074005609U,
			// Token: 0x04000B36 RID: 2870
			DirectoryProcessorCompleted,
			// Token: 0x04000B37 RID: 2871
			CallRouterCertificateNearExpiry = 2147747435U,
			// Token: 0x04000B38 RID: 2872
			CallRouterCertificateExpiryIsGood = 1074005612U,
			// Token: 0x04000B39 RID: 2873
			CallRouterCertificateDetails,
			// Token: 0x04000B3A RID: 2874
			CallRouterStartingMode,
			// Token: 0x04000B3B RID: 2875
			CallRouterCallRejected = 3221489263U,
			// Token: 0x04000B3C RID: 2876
			CallRouterSocketOpen = 1074005616U,
			// Token: 0x04000B3D RID: 2877
			CallRouterIncomingTLSCallFailure = 2147747441U,
			// Token: 0x04000B3E RID: 2878
			CallRouterInboundCallParams = 263794U,
			// Token: 0x04000B3F RID: 2879
			OptionsMessageRejected = 2147747443U,
			// Token: 0x04000B40 RID: 2880
			CopyADToFileFailed = 3221489268U,
			// Token: 0x04000B41 RID: 2881
			GrammarGenerationSkippedNoADFile,
			// Token: 0x04000B42 RID: 2882
			DirectoryProcessorTaskThrewException = 2147747446U,
			// Token: 0x04000B43 RID: 2883
			DtmfMapGenerationStarted = 1074005623U,
			// Token: 0x04000B44 RID: 2884
			DtmfMapGenerationSuccessful,
			// Token: 0x04000B45 RID: 2885
			DtmfMapUpdateFailed = 2147747449U,
			// Token: 0x04000B46 RID: 2886
			DtmfMapGenerationSkippedNoADFile = 3221489274U,
			// Token: 0x04000B47 RID: 2887
			SpeechRecoRequestParams = 1074005627U,
			// Token: 0x04000B48 RID: 2888
			InvalidSpeechRecoRequest = 2147747452U,
			// Token: 0x04000B49 RID: 2889
			SpeechRecoRequestCompleted = 1074005629U,
			// Token: 0x04000B4A RID: 2890
			SpeechRecoRequestFailed = 3221489278U,
			// Token: 0x04000B4B RID: 2891
			StartFindInGALSpeechRecoRequestParams = 1074005631U,
			// Token: 0x04000B4C RID: 2892
			StartFindInGALSpeechRecoRequestSuccess,
			// Token: 0x04000B4D RID: 2893
			StartFindInGALSpeechRecoRequestFailed = 3221489281U,
			// Token: 0x04000B4E RID: 2894
			CompleteFindInGALSpeechRecoRequestParams = 1074005634U,
			// Token: 0x04000B4F RID: 2895
			CompleteFindInGALSpeechRecoRequestSuccess,
			// Token: 0x04000B50 RID: 2896
			CompleteFindInGALSpeechRecoRequestFailed = 3221489284U,
			// Token: 0x04000B51 RID: 2897
			MobileSpeechRecoClientStartFindInGALRequestParams = 1074005637U,
			// Token: 0x04000B52 RID: 2898
			MobileSpeechRecoClientCompleteFindInGALRequestParams,
			// Token: 0x04000B53 RID: 2899
			MobileSpeechRecoClientFindInGALResult,
			// Token: 0x04000B54 RID: 2900
			ReadLastSuccessRunIDFailed = 1074005641U,
			// Token: 0x04000B55 RID: 2901
			DirectoryProcessorInitialStepEncounteredException = 3221489290U,
			// Token: 0x04000B56 RID: 2902
			LoadDtmfMapGenerationMetadataFailed = 2147747467U,
			// Token: 0x04000B57 RID: 2903
			SaveDtmfMapGenerationMetadataFailed,
			// Token: 0x04000B58 RID: 2904
			UMMailboxCmdletError = 3221489293U,
			// Token: 0x04000B59 RID: 2905
			GrammarFileUploadToSystemMailboxFailed,
			// Token: 0x04000B5A RID: 2906
			GrammarMailboxNotFound = 2147747471U,
			// Token: 0x04000B5B RID: 2907
			SetUMGrammarReadyFlagFailed = 3221489296U,
			// Token: 0x04000B5C RID: 2908
			MobileSpeechRecoTimeout = 2147747473U,
			// Token: 0x04000B5D RID: 2909
			UploadNormalizationCacheFailed,
			// Token: 0x04000B5E RID: 2910
			DownloadNormalizationCacheFailed,
			// Token: 0x04000B5F RID: 2911
			SipPeerCertificateSubjectName = 3221489300U,
			// Token: 0x04000B60 RID: 2912
			UploadDtmfMapMetadataFailed = 2147747477U,
			// Token: 0x04000B61 RID: 2913
			DownloadDtmfMapMetadataFailed,
			// Token: 0x04000B62 RID: 2914
			MobileSpeechRecoClientAsyncCallTimedOut = 3221489303U,
			// Token: 0x04000B63 RID: 2915
			UnableToAccessOrganizationMailbox,
			// Token: 0x04000B64 RID: 2916
			SetScaleOutCapabilityFailed,
			// Token: 0x04000B65 RID: 2917
			GrammarFileMaxEntriesExceeded = 2147747482U,
			// Token: 0x04000B66 RID: 2918
			GrammarFileMaxCountExceeded,
			// Token: 0x04000B67 RID: 2919
			MRASMediaChannelEstablishFailed,
			// Token: 0x04000B68 RID: 2920
			WatsoningDueToTimeout,
			// Token: 0x04000B69 RID: 2921
			WatsoningDueToWorkerProcessNotTerminating
		}
	}
}
