using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000232 RID: 562
	public struct MapiNetTags
	{
		// Token: 0x04000CD9 RID: 3289
		public const int MapiAttach = 10;

		// Token: 0x04000CDA RID: 3290
		public const int MapiCollector = 11;

		// Token: 0x04000CDB RID: 3291
		public const int MapiContainer = 12;

		// Token: 0x04000CDC RID: 3292
		public const int MapiErrorNotification = 13;

		// Token: 0x04000CDD RID: 3293
		public const int MapiExtendedNotification = 14;

		// Token: 0x04000CDE RID: 3294
		public const int MapiFolder = 15;

		// Token: 0x04000CDF RID: 3295
		public const int MapiIStream = 16;

		// Token: 0x04000CE0 RID: 3296
		public const int MapiMessage = 17;

		// Token: 0x04000CE1 RID: 3297
		public const int MapiModifyTable = 18;

		// Token: 0x04000CE2 RID: 3298
		public const int MapiNewMailNotification = 19;

		// Token: 0x04000CE3 RID: 3299
		public const int MapiNotification = 20;

		// Token: 0x04000CE4 RID: 3300
		public const int MapiObjectNotification = 21;

		// Token: 0x04000CE5 RID: 3301
		public const int MapiProp = 22;

		// Token: 0x04000CE6 RID: 3302
		public const int MapiStatusObjectNotification = 23;

		// Token: 0x04000CE7 RID: 3303
		public const int MapiStore = 24;

		// Token: 0x04000CE8 RID: 3304
		public const int MapiStream = 25;

		// Token: 0x04000CE9 RID: 3305
		public const int MapiSynchroniser = 26;

		// Token: 0x04000CEA RID: 3306
		public const int MapiTable = 27;

		// Token: 0x04000CEB RID: 3307
		public const int MapiTableNotification = 28;

		// Token: 0x04000CEC RID: 3308
		public const int MapiUnk = 29;

		// Token: 0x04000CED RID: 3309
		public const int NamedProp = 30;

		// Token: 0x04000CEE RID: 3310
		public const int NotificationHelper = 31;

		// Token: 0x04000CEF RID: 3311
		public const int ManifestCallbackHelper = 32;

		// Token: 0x04000CF0 RID: 3312
		public const int DisposableRef = 33;

		// Token: 0x04000CF1 RID: 3313
		public const int HierarchyManifestCallbackHelper = 34;

		// Token: 0x04000CF2 RID: 3314
		public const int FaultInjection = 35;

		// Token: 0x04000CF3 RID: 3315
		public const int MapiFxCollector = 36;

		// Token: 0x04000CF4 RID: 3316
		public const int FxProxyHelper = 37;

		// Token: 0x04000CF5 RID: 3317
		public const int ModuleInitDeinit = 60;

		// Token: 0x04000CF6 RID: 3318
		public const int RPCManagedCallstacks = 70;

		// Token: 0x04000CF7 RID: 3319
		public const int TraceCrossServerCalls = 80;

		// Token: 0x04000CF8 RID: 3320
		public const int tagInformation = 400;

		// Token: 0x04000CF9 RID: 3321
		public const int tagError = 401;

		// Token: 0x04000CFA RID: 3322
		public const int tagMostError = 402;

		// Token: 0x04000CFB RID: 3323
		public const int tagHcotTable = 410;

		// Token: 0x04000CFC RID: 3324
		public const int tagMspCsCheck = 420;

		// Token: 0x04000CFD RID: 3325
		public const int tagMspCsTrace = 421;

		// Token: 0x04000CFE RID: 3326
		public const int tagMspCsBlock = 422;

		// Token: 0x04000CFF RID: 3327
		public const int tagMspCsAssert = 423;

		// Token: 0x04000D00 RID: 3328
		public const int tagThreadErrorContext = 430;

		// Token: 0x04000D01 RID: 3329
		public const int tagSmallBuff = 440;

		// Token: 0x04000D02 RID: 3330
		public const int tagRpcCalls = 441;

		// Token: 0x04000D03 RID: 3331
		public const int tagRops = 442;

		// Token: 0x04000D04 RID: 3332
		public const int tagRpcRawBuffer = 443;

		// Token: 0x04000D05 RID: 3333
		public const int tagRpcExceptions = 444;

		// Token: 0x04000D06 RID: 3334
		public const int tagTableMethods = 460;

		// Token: 0x04000D07 RID: 3335
		public const int tagForceGhosted = 500;

		// Token: 0x04000D08 RID: 3336
		public const int tagDontReuseRpc = 501;

		// Token: 0x04000D09 RID: 3337
		public const int tagCnctConnect = 520;

		// Token: 0x04000D0A RID: 3338
		public const int tagDelegatedAuth = 521;

		// Token: 0x04000D0B RID: 3339
		public const int tagCxhPool = 522;

		// Token: 0x04000D0C RID: 3340
		public const int tagDiagnosticContext = 538;

		// Token: 0x04000D0D RID: 3341
		public const int tagLocation = 539;

		// Token: 0x04000D0E RID: 3342
		public const int tagUnkRelease = 540;

		// Token: 0x04000D0F RID: 3343
		public const int tagMoveMailbox = 541;

		// Token: 0x04000D10 RID: 3344
		public const int tagLocalDirectory = 565;

		// Token: 0x04000D11 RID: 3345
		public const int tagXTCFailure = 580;

		// Token: 0x04000D12 RID: 3346
		public const int tagXTCPolling = 581;

		// Token: 0x04000D13 RID: 3347
		public const int tagEseBack = 700;

		// Token: 0x04000D14 RID: 3348
		public const int tagFaultInjection = 906;

		// Token: 0x04000D15 RID: 3349
		public static Guid guid = new Guid("82914ab6-016b-442c-8e49-2562a4333be0");
	}
}
