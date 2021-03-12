using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x0200014E RID: 334
	[DataContract]
	public enum CARatingMovieEntry
	{
		// Token: 0x0400055D RID: 1373
		[EnumMember]
		DontAllow,
		// Token: 0x0400055E RID: 1374
		[EnumMember]
		AllowAll = 1000,
		// Token: 0x0400055F RID: 1375
		[EnumMember]
		USRatingG = 100,
		// Token: 0x04000560 RID: 1376
		[EnumMember]
		USRatingPG = 200,
		// Token: 0x04000561 RID: 1377
		[EnumMember]
		USRatingPG13 = 300,
		// Token: 0x04000562 RID: 1378
		[EnumMember]
		USRatingR = 400,
		// Token: 0x04000563 RID: 1379
		[EnumMember]
		USRatingNC17 = 500,
		// Token: 0x04000564 RID: 1380
		[EnumMember]
		AURatingG = 100,
		// Token: 0x04000565 RID: 1381
		[EnumMember]
		AURatingPG = 200,
		// Token: 0x04000566 RID: 1382
		[EnumMember]
		AURatingM = 350,
		// Token: 0x04000567 RID: 1383
		[EnumMember]
		AURatingMA15plus = 375,
		// Token: 0x04000568 RID: 1384
		[EnumMember]
		AURatingR18plus = 400,
		// Token: 0x04000569 RID: 1385
		[EnumMember]
		CARatingG = 100,
		// Token: 0x0400056A RID: 1386
		[EnumMember]
		CARatingPG = 200,
		// Token: 0x0400056B RID: 1387
		[EnumMember]
		CARating14A = 325,
		// Token: 0x0400056C RID: 1388
		[EnumMember]
		CARating18A = 400,
		// Token: 0x0400056D RID: 1389
		[EnumMember]
		CARatingR = 500,
		// Token: 0x0400056E RID: 1390
		[EnumMember]
		DERatingab0Jahren = 75,
		// Token: 0x0400056F RID: 1391
		[EnumMember]
		DERatingab6Jahren = 100,
		// Token: 0x04000570 RID: 1392
		[EnumMember]
		DERatingab12Jahren = 200,
		// Token: 0x04000571 RID: 1393
		[EnumMember]
		DERatingab16Jahren = 500,
		// Token: 0x04000572 RID: 1394
		[EnumMember]
		DERatingab18Jahren = 600,
		// Token: 0x04000573 RID: 1395
		[EnumMember]
		FRRating10minus = 100,
		// Token: 0x04000574 RID: 1396
		[EnumMember]
		FRRating12minus = 200,
		// Token: 0x04000575 RID: 1397
		[EnumMember]
		FRRating16minus = 500,
		// Token: 0x04000576 RID: 1398
		[EnumMember]
		FRRating18minus = 600,
		// Token: 0x04000577 RID: 1399
		[EnumMember]
		IERatingG = 100,
		// Token: 0x04000578 RID: 1400
		[EnumMember]
		IERatingPG = 200,
		// Token: 0x04000579 RID: 1401
		[EnumMember]
		IERating12 = 300,
		// Token: 0x0400057A RID: 1402
		[EnumMember]
		IERating15 = 350,
		// Token: 0x0400057B RID: 1403
		[EnumMember]
		IERating16 = 375,
		// Token: 0x0400057C RID: 1404
		[EnumMember]
		IERating18 = 400,
		// Token: 0x0400057D RID: 1405
		[EnumMember]
		JPRatingG = 100,
		// Token: 0x0400057E RID: 1406
		[EnumMember]
		JPRatingPG12 = 200,
		// Token: 0x0400057F RID: 1407
		[EnumMember]
		JPRatingRdash15 = 300,
		// Token: 0x04000580 RID: 1408
		[EnumMember]
		JPRatingRdash18 = 400,
		// Token: 0x04000581 RID: 1409
		[EnumMember]
		NZRatingG = 100,
		// Token: 0x04000582 RID: 1410
		[EnumMember]
		NZRatingPG = 200,
		// Token: 0x04000583 RID: 1411
		[EnumMember]
		NZRatingM = 300,
		// Token: 0x04000584 RID: 1412
		[EnumMember]
		NZRatingR13 = 325,
		// Token: 0x04000585 RID: 1413
		[EnumMember]
		NZRatingR15 = 350,
		// Token: 0x04000586 RID: 1414
		[EnumMember]
		NZRatingR16 = 375,
		// Token: 0x04000587 RID: 1415
		[EnumMember]
		NZRatingR18 = 400,
		// Token: 0x04000588 RID: 1416
		[EnumMember]
		NZRatingR = 500,
		// Token: 0x04000589 RID: 1417
		[EnumMember]
		NZRatingRP16 = 600,
		// Token: 0x0400058A RID: 1418
		[EnumMember]
		GBRatingU = 100,
		// Token: 0x0400058B RID: 1419
		[EnumMember]
		GBRatingUc = 150,
		// Token: 0x0400058C RID: 1420
		[EnumMember]
		GBRatingPG = 200,
		// Token: 0x0400058D RID: 1421
		[EnumMember]
		GBRating12 = 300,
		// Token: 0x0400058E RID: 1422
		[EnumMember]
		GBRating12A = 325,
		// Token: 0x0400058F RID: 1423
		[EnumMember]
		GBRating15 = 350,
		// Token: 0x04000590 RID: 1424
		[EnumMember]
		GBRating18 = 400
	}
}
