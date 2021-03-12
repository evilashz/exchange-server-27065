using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000F1 RID: 241
	internal enum LdapError
	{
		// Token: 0x040004E8 RID: 1256
		Success,
		// Token: 0x040004E9 RID: 1257
		OperationsError,
		// Token: 0x040004EA RID: 1258
		ProtocolError,
		// Token: 0x040004EB RID: 1259
		TimelimitExceeded,
		// Token: 0x040004EC RID: 1260
		SizelimitExceeded,
		// Token: 0x040004ED RID: 1261
		CompareFalse,
		// Token: 0x040004EE RID: 1262
		CompareTrue,
		// Token: 0x040004EF RID: 1263
		AuthMethodNotSupported,
		// Token: 0x040004F0 RID: 1264
		StrongAuthRequired,
		// Token: 0x040004F1 RID: 1265
		ReferralV2,
		// Token: 0x040004F2 RID: 1266
		PartialResults = 9,
		// Token: 0x040004F3 RID: 1267
		Referral,
		// Token: 0x040004F4 RID: 1268
		AdminLimitExceeded,
		// Token: 0x040004F5 RID: 1269
		UnavailableCritExtension,
		// Token: 0x040004F6 RID: 1270
		ConfidentialityRequired,
		// Token: 0x040004F7 RID: 1271
		SaslBindInProgress,
		// Token: 0x040004F8 RID: 1272
		NoSuchAttribute = 16,
		// Token: 0x040004F9 RID: 1273
		UndefinedType,
		// Token: 0x040004FA RID: 1274
		InappropriateMatching,
		// Token: 0x040004FB RID: 1275
		ConstraintViolation,
		// Token: 0x040004FC RID: 1276
		AttributeOrValueExists,
		// Token: 0x040004FD RID: 1277
		InvalidSyntax,
		// Token: 0x040004FE RID: 1278
		NoSuchObject = 32,
		// Token: 0x040004FF RID: 1279
		AliasProblem,
		// Token: 0x04000500 RID: 1280
		InvalidDnSyntax,
		// Token: 0x04000501 RID: 1281
		IsLeaf,
		// Token: 0x04000502 RID: 1282
		AliasDerefProblem,
		// Token: 0x04000503 RID: 1283
		InappropriateAuth = 48,
		// Token: 0x04000504 RID: 1284
		InvalidCredentials,
		// Token: 0x04000505 RID: 1285
		InsufficientRights,
		// Token: 0x04000506 RID: 1286
		Busy,
		// Token: 0x04000507 RID: 1287
		Unavailable,
		// Token: 0x04000508 RID: 1288
		UnwillingToPerform,
		// Token: 0x04000509 RID: 1289
		LoopDetect,
		// Token: 0x0400050A RID: 1290
		SortControlMissing = 60,
		// Token: 0x0400050B RID: 1291
		OffsetRangeError,
		// Token: 0x0400050C RID: 1292
		NamingViolation = 64,
		// Token: 0x0400050D RID: 1293
		ObjectClassViolation,
		// Token: 0x0400050E RID: 1294
		NotAllowedOnNonleaf,
		// Token: 0x0400050F RID: 1295
		NotAllowedOnRdn,
		// Token: 0x04000510 RID: 1296
		AlreadyExists,
		// Token: 0x04000511 RID: 1297
		NoObjectClassMods,
		// Token: 0x04000512 RID: 1298
		ResultsTooLarge,
		// Token: 0x04000513 RID: 1299
		AffectsMultipleDsas,
		// Token: 0x04000514 RID: 1300
		VirtualListViewError = 76,
		// Token: 0x04000515 RID: 1301
		Other = 80,
		// Token: 0x04000516 RID: 1302
		ServerDown,
		// Token: 0x04000517 RID: 1303
		LocalError,
		// Token: 0x04000518 RID: 1304
		EncodingError,
		// Token: 0x04000519 RID: 1305
		DecodingError,
		// Token: 0x0400051A RID: 1306
		Timeout,
		// Token: 0x0400051B RID: 1307
		AuthUnknown,
		// Token: 0x0400051C RID: 1308
		FilterError,
		// Token: 0x0400051D RID: 1309
		UserCancelled,
		// Token: 0x0400051E RID: 1310
		ParamError,
		// Token: 0x0400051F RID: 1311
		NoMemory,
		// Token: 0x04000520 RID: 1312
		ConnectError,
		// Token: 0x04000521 RID: 1313
		NotSupported,
		// Token: 0x04000522 RID: 1314
		NoResultsReturned = 94,
		// Token: 0x04000523 RID: 1315
		ControlNotFound = 93,
		// Token: 0x04000524 RID: 1316
		MoreResultsToReturn = 95,
		// Token: 0x04000525 RID: 1317
		ClientLoop,
		// Token: 0x04000526 RID: 1318
		ReferralLimitExceeded,
		// Token: 0x04000527 RID: 1319
		SendTimeOut = 112
	}
}
