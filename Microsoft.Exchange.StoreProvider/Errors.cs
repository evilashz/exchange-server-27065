using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000029 RID: 41
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class Errors
	{
		// Token: 0x0400012D RID: 301
		internal const int MapiCallFailed = -2147467259;

		// Token: 0x0400012E RID: 302
		internal const int MapiNotEnoughMemory = -2147024882;

		// Token: 0x0400012F RID: 303
		internal const int MapiInvalidParameter = -2147024809;

		// Token: 0x04000130 RID: 304
		internal const int MapiInterfaceNotSupported = -2147467262;

		// Token: 0x04000131 RID: 305
		internal const int MapiNoAccess = -2147024891;

		// Token: 0x04000132 RID: 306
		internal const int MapiNoSupport = -2147221246;

		// Token: 0x04000133 RID: 307
		internal const int MapiBadCharWidth = -2147221245;

		// Token: 0x04000134 RID: 308
		internal const int MapiStringTooLong = -2147221243;

		// Token: 0x04000135 RID: 309
		internal const int MapiUnknownFlags = -2147221242;

		// Token: 0x04000136 RID: 310
		internal const int MapiInvalidEntryId = -2147221241;

		// Token: 0x04000137 RID: 311
		internal const int MapiInvalidObject = -2147221240;

		// Token: 0x04000138 RID: 312
		internal const int MapiObjectChanged = -2147221239;

		// Token: 0x04000139 RID: 313
		internal const int MapiObjectDeleted = -2147221238;

		// Token: 0x0400013A RID: 314
		internal const int MapiBusy = -2147221237;

		// Token: 0x0400013B RID: 315
		internal const int MapiNotEnoughDisk = -2147221235;

		// Token: 0x0400013C RID: 316
		internal const int MapiNotEnoughResources = -2147221234;

		// Token: 0x0400013D RID: 317
		internal const int MapiNotFound = -2147221233;

		// Token: 0x0400013E RID: 318
		internal const int MapiVersion = -2147221232;

		// Token: 0x0400013F RID: 319
		internal const int MapiLogonFailed = -2147221231;

		// Token: 0x04000140 RID: 320
		internal const int MapiSessionLimit = -2147221230;

		// Token: 0x04000141 RID: 321
		internal const int MapiUserCancel = -2147221229;

		// Token: 0x04000142 RID: 322
		internal const int MapiUnableToAbort = -2147221228;

		// Token: 0x04000143 RID: 323
		internal const int MapiNetworkError = -2147221227;

		// Token: 0x04000144 RID: 324
		internal const int MapiDiskError = -2147221226;

		// Token: 0x04000145 RID: 325
		internal const int MapiTooComplex = -2147221225;

		// Token: 0x04000146 RID: 326
		internal const int MapiBadColumn = -2147221224;

		// Token: 0x04000147 RID: 327
		internal const int MapiExtendedError = -2147221223;

		// Token: 0x04000148 RID: 328
		internal const int MapiComputed = -2147221222;

		// Token: 0x04000149 RID: 329
		internal const int MapiCorruptData = -2147221221;

		// Token: 0x0400014A RID: 330
		internal const int MapiUnconfigured = -2147221220;

		// Token: 0x0400014B RID: 331
		internal const int MapiFailOneProvider = -2147221219;

		// Token: 0x0400014C RID: 332
		internal const int MapiUnknownCpid = -2147221218;

		// Token: 0x0400014D RID: 333
		internal const int MapiUnknownLcid = -2147221217;

		// Token: 0x0400014E RID: 334
		internal const int MapiPasswordChangeRequired = -2147221216;

		// Token: 0x0400014F RID: 335
		internal const int MapiPasswordExpired = -2147221215;

		// Token: 0x04000150 RID: 336
		internal const int MapiInvalidWorkstationAccount = -2147221214;

		// Token: 0x04000151 RID: 337
		internal const int MapiInvalidAccessTime = -2147221213;

		// Token: 0x04000152 RID: 338
		internal const int MapiAccountDisabled = -2147221212;

		// Token: 0x04000153 RID: 339
		internal const int MapiConflict = -2147221211;

		// Token: 0x04000154 RID: 340
		internal const int MapiEndOfSession = -2147220992;

		// Token: 0x04000155 RID: 341
		internal const int MapiUnknownEntryId = -2147220991;

		// Token: 0x04000156 RID: 342
		internal const int MapiMissingRequiredColumn = -2147220990;

		// Token: 0x04000157 RID: 343
		internal const int MapiBadValue = -2147220735;

		// Token: 0x04000158 RID: 344
		internal const int MapiInvalidType = -2147220734;

		// Token: 0x04000159 RID: 345
		internal const int MapiTypeNoSupport = -2147220733;

		// Token: 0x0400015A RID: 346
		internal const int MapiUnexpectedType = -2147220732;

		// Token: 0x0400015B RID: 347
		internal const int MapiTooBig = -2147220731;

		// Token: 0x0400015C RID: 348
		internal const int MapiDeclineCopy = -2147220730;

		// Token: 0x0400015D RID: 349
		internal const int MapiUnexpectedId = -2147220729;

		// Token: 0x0400015E RID: 350
		internal const int MapiUnableToComplete = -2147220480;

		// Token: 0x0400015F RID: 351
		internal const int MapiTimeout = -2147220479;

		// Token: 0x04000160 RID: 352
		internal const int MapiTableEmpty = -2147220478;

		// Token: 0x04000161 RID: 353
		internal const int MapiTableTooBig = -2147220477;

		// Token: 0x04000162 RID: 354
		internal const int MapiInvalidBookmark = -2147220475;

		// Token: 0x04000163 RID: 355
		internal const int MapiWait = -2147220224;

		// Token: 0x04000164 RID: 356
		internal const int MapiCancel = -2147220223;

		// Token: 0x04000165 RID: 357
		internal const int MapiNotMe = -2147220222;

		// Token: 0x04000166 RID: 358
		internal const int MapiCorruptStore = -2147219968;

		// Token: 0x04000167 RID: 359
		internal const int MapiNotInQueue = -2147219967;

		// Token: 0x04000168 RID: 360
		internal const int MapiNoSuppress = -2147219966;

		// Token: 0x04000169 RID: 361
		internal const int MapiCollision = -2147219964;

		// Token: 0x0400016A RID: 362
		internal const int MapiNotInitialized = -2147219963;

		// Token: 0x0400016B RID: 363
		internal const int MapiNonStandard = -2147219962;

		// Token: 0x0400016C RID: 364
		internal const int MapiNoRecipients = -2147219961;

		// Token: 0x0400016D RID: 365
		internal const int MapiSubmitted = -2147219960;

		// Token: 0x0400016E RID: 366
		internal const int MapiHasFolders = -2147219959;

		// Token: 0x0400016F RID: 367
		internal const int MapiHasMessages = -2147219958;

		// Token: 0x04000170 RID: 368
		internal const int MapiFolderCycle = -2147219957;

		// Token: 0x04000171 RID: 369
		internal const int MapiRecursionLimit = -2147219956;

		// Token: 0x04000172 RID: 370
		internal const int MapiDataLoss = -2147220347;

		// Token: 0x04000173 RID: 371
		internal const int MapiTooManyRecips = -1073478950;

		// Token: 0x04000174 RID: 372
		internal const int MapiLockIdLimit = -2147219955;

		// Token: 0x04000175 RID: 373
		internal const int MapiTooManyMountedDatabases = -2147219954;

		// Token: 0x04000176 RID: 374
		internal const int MapiPartialItem = -2147219834;

		// Token: 0x04000177 RID: 375
		internal const int MapiAmbiguousRecip = -2147219712;

		// Token: 0x04000178 RID: 376
		internal const int MapiNamedPropQuotaExceeded = -2147219200;

		// Token: 0x04000179 RID: 377
		internal const int SyncObjectDeleted = -2147219456;

		// Token: 0x0400017A RID: 378
		internal const int SyncIgnore = -2147219455;

		// Token: 0x0400017B RID: 379
		internal const int SyncConflict = -2147219454;

		// Token: 0x0400017C RID: 380
		internal const int SyncNoParent = -2147219453;

		// Token: 0x0400017D RID: 381
		internal const int SyncIncest = -2147219452;

		// Token: 0x0400017E RID: 382
		internal const int SyncUnsynchronized = -2147219451;

		// Token: 0x0400017F RID: 383
		internal const int ErrorCanNotComplete = -2147023893;

		// Token: 0x04000180 RID: 384
		internal const int ErrorPathNotFound = -2147024893;

		// Token: 0x04000181 RID: 385
		internal const int ErrorInsufficientBuffer = -2147024774;

		// Token: 0x04000182 RID: 386
		internal const int ErrorCanceled = -2147023673;

		// Token: 0x04000183 RID: 387
		internal const int FailCallback = -2147220967;

		// Token: 0x04000184 RID: 388
		internal const int StgInsufficientMemory = -2147287032;

		// Token: 0x04000185 RID: 389
		internal const int StgInvalidParameter = -2147286953;

		// Token: 0x04000186 RID: 390
		internal const int StgLockViolation = -2147287007;

		// Token: 0x04000187 RID: 391
		internal const int StreamSizeError = -2147286928;

		// Token: 0x04000188 RID: 392
		internal const int StreamSeekError = -2147287015;

		// Token: 0x04000189 RID: 393
		internal const int Win32ErrorDiskFull = -2147024784;
	}
}
