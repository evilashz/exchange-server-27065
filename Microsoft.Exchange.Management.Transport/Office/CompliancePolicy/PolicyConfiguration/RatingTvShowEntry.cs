using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x02000148 RID: 328
	[DataContract]
	public enum RatingTvShowEntry
	{
		// Token: 0x04000516 RID: 1302
		[EnumMember]
		DontAllow,
		// Token: 0x04000517 RID: 1303
		[EnumMember]
		AllowAll = 1000,
		// Token: 0x04000518 RID: 1304
		[EnumMember]
		USRatingTVY = 100,
		// Token: 0x04000519 RID: 1305
		[EnumMember]
		USRatingTVY7 = 200,
		// Token: 0x0400051A RID: 1306
		[EnumMember]
		USRatingTVG = 300,
		// Token: 0x0400051B RID: 1307
		[EnumMember]
		USRatingTVPG = 400,
		// Token: 0x0400051C RID: 1308
		[EnumMember]
		USRatingTV14 = 500,
		// Token: 0x0400051D RID: 1309
		[EnumMember]
		USRatingTVMA = 600,
		// Token: 0x0400051E RID: 1310
		[EnumMember]
		AURatingP = 100,
		// Token: 0x0400051F RID: 1311
		[EnumMember]
		AURatingC = 200,
		// Token: 0x04000520 RID: 1312
		[EnumMember]
		AURatingG = 300,
		// Token: 0x04000521 RID: 1313
		[EnumMember]
		AURatingPG = 400,
		// Token: 0x04000522 RID: 1314
		[EnumMember]
		AURatingM = 500,
		// Token: 0x04000523 RID: 1315
		[EnumMember]
		AURatingMA15plus = 550,
		// Token: 0x04000524 RID: 1316
		[EnumMember]
		AURatingAv15plus = 575,
		// Token: 0x04000525 RID: 1317
		[EnumMember]
		CARatingC = 100,
		// Token: 0x04000526 RID: 1318
		[EnumMember]
		CARatingC8 = 200,
		// Token: 0x04000527 RID: 1319
		[EnumMember]
		CARatingG = 300,
		// Token: 0x04000528 RID: 1320
		[EnumMember]
		CARatingPG = 400,
		// Token: 0x04000529 RID: 1321
		[EnumMember]
		CARating14plus = 500,
		// Token: 0x0400052A RID: 1322
		[EnumMember]
		CARating18plus = 600,
		// Token: 0x0400052B RID: 1323
		[EnumMember]
		DERatingab0Jahren = 75,
		// Token: 0x0400052C RID: 1324
		[EnumMember]
		DERatingab6Jahren = 100,
		// Token: 0x0400052D RID: 1325
		[EnumMember]
		DERatingab12Jahren = 200,
		// Token: 0x0400052E RID: 1326
		[EnumMember]
		DERatingab16Jahren = 500,
		// Token: 0x0400052F RID: 1327
		[EnumMember]
		DERatingab18Jahren = 600,
		// Token: 0x04000530 RID: 1328
		[EnumMember]
		FRRating10minus = 100,
		// Token: 0x04000531 RID: 1329
		[EnumMember]
		FRRating12minus = 200,
		// Token: 0x04000532 RID: 1330
		[EnumMember]
		FRRating16minus = 500,
		// Token: 0x04000533 RID: 1331
		[EnumMember]
		FRRating18minus = 600,
		// Token: 0x04000534 RID: 1332
		[EnumMember]
		IERatingGA = 100,
		// Token: 0x04000535 RID: 1333
		[EnumMember]
		IERatingCh = 200,
		// Token: 0x04000536 RID: 1334
		[EnumMember]
		IERatingYA = 400,
		// Token: 0x04000537 RID: 1335
		[EnumMember]
		IERatingPS = 500,
		// Token: 0x04000538 RID: 1336
		[EnumMember]
		IERatingMA = 600,
		// Token: 0x04000539 RID: 1337
		[EnumMember]
		JPRatingExplicitAllowed = 500,
		// Token: 0x0400053A RID: 1338
		[EnumMember]
		NZRatingG = 200,
		// Token: 0x0400053B RID: 1339
		[EnumMember]
		NZRatingPGR = 400,
		// Token: 0x0400053C RID: 1340
		[EnumMember]
		NZRatingAO = 600,
		// Token: 0x0400053D RID: 1341
		[EnumMember]
		GBRatingCaution = 500
	}
}
